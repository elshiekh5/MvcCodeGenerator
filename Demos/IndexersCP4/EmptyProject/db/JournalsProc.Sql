

CREATE PROCEDURE [dbo].[Journals_Create]
	@JournalCode VarChar(20),
	@FullTitle VarChar(255),
	@ShortTitle VarChar(255) = NULL,
	@JournalSubCode VarChar(20),
	@JournalOwner Int,
	@Note VarChar = NULL
AS
	INSERT INTO dbo.[Journals] 
	(
		[JournalCode],
		[FullTitle],
		[ShortTitle],
		[JournalSubCode],
		[JournalOwner],
		[Note]
	)
	Values(
		@JournalCode,
		@FullTitle,
		@ShortTitle,
		@JournalSubCode,
		@JournalOwner,
		@Note)

	
GO

CREATE PROCEDURE [dbo].[Journals_Update]
	@JournalCode VarChar(20),
	@FullTitle VarChar(255),
	@ShortTitle VarChar(255) = NULL,
	@JournalSubCode VarChar(20),
	@JournalOwner Int,
	@Note VarChar = NULL
AS
	UPDATE dbo.[Journals] 
	SET

		[FullTitle]=@FullTitle,
		[ShortTitle]=@ShortTitle,
		[JournalSubCode]=@JournalSubCode,
		[JournalOwner]=@JournalOwner,
		[Note]=@Note

	
	Where [JournalCode]  = @JournalCode
GO

CREATE PROCEDURE [dbo].[Journals_Delete]
	@JournalCode VarChar(20)
AS
	DELETE FROM dbo.[Journals] 
	Where [JournalCode]  = @JournalCode
GO

CREATE PROCEDURE [dbo].[Journals_Get]
	@JournalCode VarChar(20) = NULL
AS
	SELECT * FROM dbo.[Journals] 
	Where (@JournalCode IS NULL OR [JournalCode]  = @JournalCode)
GO
Create PROCEDURE [dbo].[Journals_GetPageByPage]
	@PageIndex int,
	@PageSize  int,
	@SortName  nvarchar(128) =null,
	@SortOrder  nvarchar(128) =null,
	@QType  nvarchar(128) =null,
	@Query  nvarchar(128) =null,
	@TotalRecords   int out
As
BEGIN
	/***************************************************************************************************************/
													/*Conditions Regions*/
	/***************************************************************************************************************/
	declare @Conditions varchar(Max) 
	declare @OrderByStatement varchar(Max) 
	Set @Conditions =' (0=0) ';
	Set @OrderByStatement =' ';
	----------------------------------------------------------------------------------------------------------
	IF @QType is not null And @Query is not null 
		Set @Conditions = @Conditions + ' And  ( '+@QType+' Like ''%'+@Query+''' )'
	/***************************************************************************************************************/
													/*Order BY Regions*/
	/***************************************************************************************************************/
	IF @SortName is not null and @SortOrder is not null 
		Set @OrderByStatement = ' ORDER BY  '+@SortName+' '+ @SortOrder

	/***************************************************************************************************************/
													/*Sql Statement*/
	/***************************************************************************************************************/
	
	declare @SqlStatement nvarchar(Max)
	----------------------------------------------------------
	DECLARE @PageLowerBound int
	SET @PageIndex = @PageIndex-1
	DECLARE @PageUpperBound int
	SET @PageLowerBound = @PageSize * @PageIndex
	SET @PageUpperBound = @PageSize - 1 + @PageLowerBound
	----------------------------------------------------------
	-- Create a temp table TO store the SELECT results
	CREATE TABLE #PageIndexTable
	(
		IndexId int IDENTITY (0, 1) NOT NULL,
		ID VarChar(20)
	)
	----------------------------------------------------------
	Set @SqlStatement='-------------------------------------------
	-------------------------------------------
	INSERT INTO #PageIndexTable (ID)
	SELECT JournalCode FROM [dbo].[Journals] 
	-------------------------------------------
	Where '+@Conditions+
	@OrderByStatement
	/**************************************************************************************************************/
	print (@SqlStatement)
	EXEC (@sqlStatement)
	-------------------------------------------
	SELECT @TotalRecords= @@ROWCOUNT
	-------------------------------------------
	SELECT    [Journals].*
	FROM         #PageIndexTable inner join  [dbo].Journals
	on [dbo].Journals.JournalCode =#PageIndexTable.ID
	WHERE  #PageIndexTable.IndexId >= @PageLowerBound AND #PageIndexTable.IndexId <= @PageUpperBound
	-------------------------------------------
	/**************************************************************************************************************/
End

GO
