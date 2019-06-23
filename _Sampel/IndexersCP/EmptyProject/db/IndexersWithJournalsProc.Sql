

CREATE PROCEDURE [dbo].[IndexersWithJournals_Create]
	@ID Int out,
	@IndexerID Int,
	@JournalCode VarChar(20),
	@SubmissionDate Date = NULL,
	@AcceptanceDate Date = NULL,
	@RejectionDate Date = NULL,
	@NextEvaRound NVarChar(128) = NULL,
	@NoofEvaRound Int = NULL,
	@Notes NVarChar(2000) = NULL,
	@StatusID Int
AS
	INSERT INTO dbo.[IndexersWithJournals] 
	(
		[IndexerID],
		[JournalCode],
		[SubmissionDate],
		[AcceptanceDate],
		[RejectionDate],
		[NextEvaRound],
		[NoofEvaRound],
		[Notes],
		[StatusID]
	)
	Values(
		@IndexerID,
		@JournalCode,
		@SubmissionDate,
		@AcceptanceDate,
		@RejectionDate,
		@NextEvaRound,
		@NoofEvaRound,
		@Notes,
		@StatusID)

	
	Set @ID  = @@Identity
GO

CREATE PROCEDURE [dbo].[IndexersWithJournals_Update]
	@ID Int,
	@IndexerID Int,
	@JournalCode VarChar(20),
	@SubmissionDate Date = NULL,
	@AcceptanceDate Date = NULL,
	@RejectionDate Date = NULL,
	@NextEvaRound NVarChar(128) = NULL,
	@NoofEvaRound Int = NULL,
	@Notes NVarChar(2000) = NULL,
	@StatusID Int
AS
	UPDATE dbo.[IndexersWithJournals] 
	SET

		[IndexerID]=@IndexerID,
		[JournalCode]=@JournalCode,
		[SubmissionDate]=@SubmissionDate,
		[AcceptanceDate]=@AcceptanceDate,
		[RejectionDate]=@RejectionDate,
		[NextEvaRound]=@NextEvaRound,
		[NoofEvaRound]=@NoofEvaRound,
		[Notes]=@Notes,
		[StatusID]=@StatusID

	
	Where [ID]  = @ID
GO

CREATE PROCEDURE [dbo].[IndexersWithJournals_Delete]
	@ID Int
AS
	DELETE FROM dbo.[IndexersWithJournals] 
	Where [ID]  = @ID
GO

CREATE PROCEDURE [dbo].[IndexersWithJournals_Get]
	@ID Int = NULL
AS
	SELECT * FROM dbo.[IndexersWithJournals] 
	Where (@ID IS NULL OR [ID]  = @ID)
GO
Create PROCEDURE [dbo].[IndexersWithJournals_GetPageByPage]
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
	SELECT ID FROM [dbo].[IndexersWithJournals] 
	-------------------------------------------
	Where '+@Conditions+
	@OrderByStatement
	/**************************************************************************************************************/
	print (@SqlStatement)
	EXEC (@sqlStatement)
	-------------------------------------------
	SELECT @TotalRecords= @@ROWCOUNT
	-------------------------------------------
	SELECT    [IndexersWithJournals].*
	FROM         #PageIndexTable inner join  [dbo].IndexersWithJournals
	on [dbo].IndexersWithJournals.ID =#PageIndexTable.ID
	WHERE  #PageIndexTable.IndexId >= @PageLowerBound AND #PageIndexTable.IndexId <= @PageUpperBound
	-------------------------------------------
	/**************************************************************************************************************/
End

GO
