

CREATE PROCEDURE [dbo].[News_Create]
	@NewsID Int out,
	@LangID Int,
	@Type Int,
	@Title NVarChar(256),
	@Details NVarChar = NULL,
	@PhotoFile VarChar(64) = NULL,
	@PhotoName NVarChar(128) = NULL,
	@AttachFile VarChar(64) = NULL,
	@AttachName NVarChar(128) = NULL,
	@EndDate DateTime2 = NULL,
	@InsertDate DateTime2,
	@LastModfiedDate DateTime2 = NULL
AS
	INSERT INTO dbo.[News] 
	(
		[LangID],
		[Type],
		[Title],
		[Details],
		[PhotoFile],
		[PhotoName],
		[AttachFile],
		[AttachName],
		[EndDate],
		[InsertDate],
		[LastModfiedDate]
	)
	Values(
		@LangID,
		@Type,
		@Title,
		@Details,
		@PhotoFile,
		@PhotoName,
		@AttachFile,
		@AttachName,
		@EndDate,
		@InsertDate,
		@LastModfiedDate)

	
	Set @NewsID  = @@Identity
GO

CREATE PROCEDURE [dbo].[News_Update]
	@NewsID Int,
	@LangID Int,
	@Type Int,
	@Title NVarChar(256),
	@Details NVarChar = NULL,
	@PhotoFile VarChar(64) = NULL,
	@PhotoName NVarChar(128) = NULL,
	@AttachFile VarChar(64) = NULL,
	@AttachName NVarChar(128) = NULL,
	@EndDate DateTime2 = NULL,
	@InsertDate DateTime2,
	@LastModfiedDate DateTime2 = NULL
AS
	UPDATE dbo.[News] 
	SET

		[LangID]=@LangID,
		[Type]=@Type,
		[Title]=@Title,
		[Details]=@Details,
		[PhotoFile]=@PhotoFile,
		[PhotoName]=@PhotoName,
		[AttachFile]=@AttachFile,
		[AttachName]=@AttachName,
		[EndDate]=@EndDate,
		[InsertDate]=@InsertDate,
		[LastModfiedDate]=@LastModfiedDate

	
	Where [NewsID]  = @NewsID
GO

CREATE PROCEDURE [dbo].[News_Delete]
	@NewsID Int
AS
	DELETE FROM dbo.[News] 
	Where [NewsID]  = @NewsID
GO

CREATE PROCEDURE [dbo].[News_Get]
	@NewsID Int = NULL
AS
	SELECT * FROM dbo.[News] 
	Where (@NewsID IS NULL OR [NewsID]  = @NewsID)
GO
Create PROCEDURE [dbo].[News_GetPageByPage]
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
	SELECT NewsID FROM [dbo].[News] 
	-------------------------------------------
	Where '+@Conditions+
	@OrderByStatement
	/**************************************************************************************************************/
	print (@SqlStatement)
	EXEC (@sqlStatement)
	-------------------------------------------
	SELECT @TotalRecords= @@ROWCOUNT
	-------------------------------------------
	SELECT    [News].*
	FROM         #PageIndexTable inner join  [dbo].News
	on [dbo].News.NewsID =#PageIndexTable.ID
	WHERE  #PageIndexTable.IndexId >= @PageLowerBound AND #PageIndexTable.IndexId <= @PageUpperBound
	-------------------------------------------
	/**************************************************************************************************************/
End

GO
