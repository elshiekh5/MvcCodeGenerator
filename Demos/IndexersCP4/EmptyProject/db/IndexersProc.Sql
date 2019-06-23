

CREATE PROCEDURE [dbo].[Indexers_Create]
	@IndexerID Int out,
	@Name VarChar(64),
	@OwnerID Int
AS
	INSERT INTO dbo.[Indexers] 
	(
		[Name],
		[OwnerID]
	)
	Values(
		@Name,
		@OwnerID)

	
	Set @IndexerID  = @@Identity
GO

CREATE PROCEDURE [dbo].[Indexers_Update]
	@IndexerID Int,
	@Name VarChar(64),
	@OwnerID Int
AS
	UPDATE dbo.[Indexers] 
	SET

		[Name]=@Name,
		[OwnerID]=@OwnerID

	
	Where [IndexerID]  = @IndexerID
GO

CREATE PROCEDURE [dbo].[Indexers_Delete]
	@IndexerID Int
AS
	DELETE FROM dbo.[Indexers] 
	Where [IndexerID]  = @IndexerID
GO

CREATE PROCEDURE [dbo].[Indexers_Get]
	@IndexerID Int = NULL
AS
	SELECT * FROM dbo.[Indexers] 
	Where (@IndexerID IS NULL OR [IndexerID]  = @IndexerID)
GO
Create PROCEDURE [dbo].[Indexers_GetPageByPage]
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
	SELECT IndexerID FROM [dbo].[Indexers] 
	-------------------------------------------
	Where '+@Conditions+
	@OrderByStatement
	/**************************************************************************************************************/
	print (@SqlStatement)
	EXEC (@sqlStatement)
	-------------------------------------------
	SELECT @TotalRecords= @@ROWCOUNT
	-------------------------------------------
	SELECT    [Indexers].*
	FROM         #PageIndexTable inner join  [dbo].Indexers
	on [dbo].Indexers.IndexerID =#PageIndexTable.ID
	WHERE  #PageIndexTable.IndexId >= @PageLowerBound AND #PageIndexTable.IndexId <= @PageUpperBound
	-------------------------------------------
	/**************************************************************************************************************/
End

GO
