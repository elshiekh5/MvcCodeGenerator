

CREATE PROCEDURE [dbo].[SchoolsToNews_Create]
	@ShoolsToNewsID Int,
	@SchoolID Int,
	@NewsID Int
AS
	INSERT INTO dbo.[SchoolsToNews] 
	(
		[ShoolsToNewsID],
		[SchoolID],
		[NewsID]
	)
	Values(
		@ShoolsToNewsID,
		@SchoolID,
		@NewsID)

	
GO

CREATE PROCEDURE [dbo].[SchoolsToNews_Update]
	@ShoolsToNewsID Int,
	@SchoolID Int,
	@NewsID Int
AS
	UPDATE dbo.[SchoolsToNews] 
	SET

		[SchoolID]=@SchoolID,
		[NewsID]=@NewsID

	
	Where [ShoolsToNewsID]  = @ShoolsToNewsID
GO

CREATE PROCEDURE [dbo].[SchoolsToNews_Delete]
	@ShoolsToNewsID Int
AS
	DELETE FROM dbo.[SchoolsToNews] 
	Where [ShoolsToNewsID]  = @ShoolsToNewsID
GO

CREATE PROCEDURE [dbo].[SchoolsToNews_Get]
	@ShoolsToNewsID Int = NULL
AS
	SELECT * FROM dbo.[SchoolsToNews] 
	Where (@ShoolsToNewsID IS NULL OR [ShoolsToNewsID]  = @ShoolsToNewsID)
GO
Create PROCEDURE [dbo].[SchoolsToNews_GetPageByPage]
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
	SELECT ShoolsToNewsID FROM [dbo].[SchoolsToNews] 
	-------------------------------------------
	Where '+@Conditions+
	@OrderByStatement
	/**************************************************************************************************************/
	print (@SqlStatement)
	EXEC (@sqlStatement)
	-------------------------------------------
	SELECT @TotalRecords= @@ROWCOUNT
	-------------------------------------------
	SELECT    [SchoolsToNews].*
	FROM         #PageIndexTable inner join  [dbo].SchoolsToNews
	on [dbo].SchoolsToNews.ShoolsToNewsID =#PageIndexTable.ID
	WHERE  #PageIndexTable.IndexId >= @PageLowerBound AND #PageIndexTable.IndexId <= @PageUpperBound
	-------------------------------------------
	/**************************************************************************************************************/
End

GO
