

CREATE PROCEDURE [dbo].[JournalsOwners_Create]
	@JournalOwner Int out,
	@OwnerName VarChar(64) = NULL
AS
	INSERT INTO dbo.[JournalsOwners] 
	(
		[OwnerName]
	)
	Values(
		@OwnerName)

	
	Set @JournalOwner  = @@Identity
GO

CREATE PROCEDURE [dbo].[JournalsOwners_Update]
	@JournalOwner Int,
	@OwnerName VarChar(64) = NULL
AS
	UPDATE dbo.[JournalsOwners] 
	SET

		[OwnerName]=@OwnerName

	
	Where [JournalOwner]  = @JournalOwner
GO

CREATE PROCEDURE [dbo].[JournalsOwners_Delete]
	@JournalOwner Int
AS
	DELETE FROM dbo.[JournalsOwners] 
	Where [JournalOwner]  = @JournalOwner
GO

CREATE PROCEDURE [dbo].[JournalsOwners_Get]
	@JournalOwner Int = NULL
AS
	SELECT * FROM dbo.[JournalsOwners] 
	Where (@JournalOwner IS NULL OR [JournalOwner]  = @JournalOwner)
GO
Create PROCEDURE [dbo].[JournalsOwners_GetPageByPage]
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
		ID Int
	)
	----------------------------------------------------------
	Set @SqlStatement='-------------------------------------------
	-------------------------------------------
	INSERT INTO #PageIndexTable (ID)
	SELECT JournalOwner FROM [dbo].[JournalsOwners] 
	-------------------------------------------
	Where '+@Conditions+
	@OrderByStatement
	/**************************************************************************************************************/
	print (@SqlStatement)
	EXEC (@sqlStatement)
	-------------------------------------------
	SELECT @TotalRecords= @@ROWCOUNT
	-------------------------------------------
	SELECT    [JournalsOwners].*
	FROM         #PageIndexTable inner join  [dbo].JournalsOwners
	on [dbo].JournalsOwners.JournalOwner =#PageIndexTable.ID
	WHERE  #PageIndexTable.IndexId >= @PageLowerBound AND #PageIndexTable.IndexId <= @PageUpperBound
	-------------------------------------------
	/**************************************************************************************************************/
End

GO
