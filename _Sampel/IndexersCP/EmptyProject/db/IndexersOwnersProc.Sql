

CREATE PROCEDURE [dbo].[IndexersOwners_Create]
	@OwnerID Int out,
	@Name VarChar(64)
AS
	INSERT INTO dbo.[IndexersOwners] 
	(
		[Name]
	)
	Values(
		@Name)

	
	Set @OwnerID  = @@Identity
GO

CREATE PROCEDURE [dbo].[IndexersOwners_Update]
	@OwnerID Int,
	@Name VarChar(64)
AS
	UPDATE dbo.[IndexersOwners] 
	SET

		[Name]=@Name

	
	Where [OwnerID]  = @OwnerID
GO

CREATE PROCEDURE [dbo].[IndexersOwners_Delete]
	@OwnerID Int
AS
	DELETE FROM dbo.[IndexersOwners] 
	Where [OwnerID]  = @OwnerID
GO

CREATE PROCEDURE [dbo].[IndexersOwners_Get]
	@OwnerID Int = NULL
AS
	SELECT * FROM dbo.[IndexersOwners] 
	Where (@OwnerID IS NULL OR [OwnerID]  = @OwnerID)
GO
Create PROCEDURE [dbo].[IndexersOwners_GetPageByPage]
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
	SELECT OwnerID FROM [dbo].[IndexersOwners] 
	-------------------------------------------
	Where '+@Conditions+
	@OrderByStatement
	/**************************************************************************************************************/
	print (@SqlStatement)
	EXEC (@sqlStatement)
	-------------------------------------------
	SELECT @TotalRecords= @@ROWCOUNT
	-------------------------------------------
	SELECT    [IndexersOwners].*
	FROM         #PageIndexTable inner join  [dbo].IndexersOwners
	on [dbo].IndexersOwners.OwnerID =#PageIndexTable.ID
	WHERE  #PageIndexTable.IndexId >= @PageLowerBound AND #PageIndexTable.IndexId <= @PageUpperBound
	-------------------------------------------
	/**************************************************************************************************************/
End

GO
