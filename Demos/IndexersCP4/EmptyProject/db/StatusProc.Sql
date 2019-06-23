

CREATE PROCEDURE [dbo].[Status_Create]
	@StatusID Int,
	@Title VarChar(64)
AS
	INSERT INTO dbo.[Status] 
	(
		[StatusID],
		[Title]
	)
	Values(
		@StatusID,
		@Title)

	
GO

CREATE PROCEDURE [dbo].[Status_Update]
	@StatusID Int,
	@Title VarChar(64)
AS
	UPDATE dbo.[Status] 
	SET

		[Title]=@Title

	
	Where [StatusID]  = @StatusID
GO

CREATE PROCEDURE [dbo].[Status_Delete]
	@StatusID Int
AS
	DELETE FROM dbo.[Status] 
	Where [StatusID]  = @StatusID
GO

CREATE PROCEDURE [dbo].[Status_Get]
	@StatusID Int = NULL
AS
	SELECT * FROM dbo.[Status] 
	Where (@StatusID IS NULL OR [StatusID]  = @StatusID)
GO
Create PROCEDURE [dbo].[Status_GetPageByPage]
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
	SELECT StatusID FROM [dbo].[Status] 
	-------------------------------------------
	Where '+@Conditions+
	@OrderByStatement
	/**************************************************************************************************************/
	print (@SqlStatement)
	EXEC (@sqlStatement)
	-------------------------------------------
	SELECT @TotalRecords= @@ROWCOUNT
	-------------------------------------------
	SELECT    [Status].*
	FROM         #PageIndexTable inner join  [dbo].Status
	on [dbo].Status.StatusID =#PageIndexTable.ID
	WHERE  #PageIndexTable.IndexId >= @PageLowerBound AND #PageIndexTable.IndexId <= @PageUpperBound
	-------------------------------------------
	/**************************************************************************************************************/
End

GO
