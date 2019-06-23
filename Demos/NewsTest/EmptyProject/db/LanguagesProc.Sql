

CREATE PROCEDURE [dbo].[Languages_Create]
	@LangID Int,
	@Name NVarChar(46),
	@Code VarChar(4) = NULL,
	@LocalizationCode VarChar(8) = NULL
AS
	INSERT INTO dbo.[Languages] 
	(
		[LangID],
		[Name],
		[Code],
		[LocalizationCode]
	)
	Values(
		@LangID,
		@Name,
		@Code,
		@LocalizationCode)

	
GO

CREATE PROCEDURE [dbo].[Languages_Update]
	@LangID Int,
	@Name NVarChar(46),
	@Code VarChar(4) = NULL,
	@LocalizationCode VarChar(8) = NULL
AS
	UPDATE dbo.[Languages] 
	SET

		[Name]=@Name,
		[Code]=@Code,
		[LocalizationCode]=@LocalizationCode

	
	Where [LangID]  = @LangID
GO

CREATE PROCEDURE [dbo].[Languages_Delete]
	@LangID Int
AS
	DELETE FROM dbo.[Languages] 
	Where [LangID]  = @LangID
GO

CREATE PROCEDURE [dbo].[Languages_Get]
	@LangID Int = NULL
AS
	SELECT * FROM dbo.[Languages] 
	Where (@LangID IS NULL OR [LangID]  = @LangID)
GO
Create PROCEDURE [dbo].[Languages_GetPageByPage]
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
	SELECT LangID FROM [dbo].[Languages] 
	-------------------------------------------
	Where '+@Conditions+
	@OrderByStatement
	/**************************************************************************************************************/
	print (@SqlStatement)
	EXEC (@sqlStatement)
	-------------------------------------------
	SELECT @TotalRecords= @@ROWCOUNT
	-------------------------------------------
	SELECT    [Languages].*
	FROM         #PageIndexTable inner join  [dbo].Languages
	on [dbo].Languages.LangID =#PageIndexTable.ID
	WHERE  #PageIndexTable.IndexId >= @PageLowerBound AND #PageIndexTable.IndexId <= @PageUpperBound
	-------------------------------------------
	/**************************************************************************************************************/
End

GO
