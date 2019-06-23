

CREATE PROCEDURE [dbo].[Schools_Create]
	@SchoolID Int,
	@Name NVarChar(265) = NULL
AS
	INSERT INTO dbo.[Schools] 
	(
		[SchoolID],
		[Name]
	)
	Values(
		@SchoolID,
		@Name)

	
GO

CREATE PROCEDURE [dbo].[Schools_Update]
	@SchoolID Int,
	@Name NVarChar(265) = NULL
AS
	UPDATE dbo.[Schools] 
	SET

		[Name]=@Name

	
	Where [SchoolID]  = @SchoolID
GO

CREATE PROCEDURE [dbo].[Schools_Delete]
	@SchoolID Int
AS
	DELETE FROM dbo.[Schools] 
	Where [SchoolID]  = @SchoolID
GO

CREATE PROCEDURE [dbo].[Schools_Get]
	@SchoolID Int = NULL
AS
	SELECT * FROM dbo.[Schools] 
	Where (@SchoolID IS NULL OR [SchoolID]  = @SchoolID)
GO
Create PROCEDURE [dbo].[Schools_GetPageByPage]
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
	SELECT SchoolID FROM [dbo].[Schools] 
	-------------------------------------------
	Where '+@Conditions+
	@OrderByStatement
	/**************************************************************************************************************/
	print (@SqlStatement)
	EXEC (@sqlStatement)
	-------------------------------------------
	SELECT @TotalRecords= @@ROWCOUNT
	-------------------------------------------
	SELECT    [Schools].*
	FROM         #PageIndexTable inner join  [dbo].Schools
	on [dbo].Schools.SchoolID =#PageIndexTable.ID
	WHERE  #PageIndexTable.IndexId >= @PageLowerBound AND #PageIndexTable.IndexId <= @PageUpperBound
	-------------------------------------------
	/**************************************************************************************************************/
End

GO
