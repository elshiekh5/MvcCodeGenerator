

CREATE PROCEDURE [dbo].[RoundsLog_Create]
	@RoundID Int out,
	@IndexerID Int,
	@JournalCode VarChar(20),
	@RoundDate Date,
	@StatusID Int,
	@NextEvaRound NVarChar(128) = NULL,
	@Notes NVarChar(2000) = NULL
AS
	INSERT INTO dbo.[RoundsLog] 
	(
		[IndexerID],
		[JournalCode],
		[RoundDate],
		[StatusID],
		[NextEvaRound],
		[Notes]
	)
	Values(
		@IndexerID,
		@JournalCode,
		@RoundDate,
		@StatusID,
		@NextEvaRound,
		@Notes)

	
	Set @RoundID  = @@Identity
GO

CREATE PROCEDURE [dbo].[RoundsLog_Update]
	@RoundID Int,
	@IndexerID Int,
	@JournalCode VarChar(20),
	@RoundDate Date,
	@StatusID Int,
	@NextEvaRound NVarChar(128) = NULL,
	@Notes NVarChar(2000) = NULL
AS
	UPDATE dbo.[RoundsLog] 
	SET

		[IndexerID]=@IndexerID,
		[JournalCode]=@JournalCode,
		[RoundDate]=@RoundDate,
		[StatusID]=@StatusID,
		[NextEvaRound]=@NextEvaRound,
		[Notes]=@Notes

	
	Where [RoundID]  = @RoundID
GO

CREATE PROCEDURE [dbo].[RoundsLog_Delete]
	@RoundID Int
AS
	DELETE FROM dbo.[RoundsLog] 
	Where [RoundID]  = @RoundID
GO

CREATE PROCEDURE [dbo].[RoundsLog_Get]
	@RoundID Int = NULL
AS
	SELECT * FROM dbo.[RoundsLog] 
	Where (@RoundID IS NULL OR [RoundID]  = @RoundID)
GO
Create PROCEDURE [dbo].[RoundsLog_GetPageByPage]
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
	SELECT RoundID FROM [dbo].[RoundsLog] 
	-------------------------------------------
	Where '+@Conditions+
	@OrderByStatement
	/**************************************************************************************************************/
	print (@SqlStatement)
	EXEC (@sqlStatement)
	-------------------------------------------
	SELECT @TotalRecords= @@ROWCOUNT
	-------------------------------------------
	SELECT    [RoundsLog].*
	FROM         #PageIndexTable inner join  [dbo].RoundsLog
	on [dbo].RoundsLog.RoundID =#PageIndexTable.ID
	WHERE  #PageIndexTable.IndexId >= @PageLowerBound AND #PageIndexTable.IndexId <= @PageUpperBound
	-------------------------------------------
	/**************************************************************************************************************/
End

GO
