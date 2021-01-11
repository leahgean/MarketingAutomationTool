-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: Dec-13-2020
-- Description:	Gets group list by page size
---- =============================================
ALTER PROCEDURE [dbo].[S_CAMPAIGN_GETALL]
@AccountID AS UNIQUEIDENTIFIER,
@Deleted BIT,
@HideInSearch BIT,
@MaxRows INT,
@SortBy VARCHAR(50),
@SortDirection VARCHAR(10),
@PageNum INT,
@NewPageNum INT OUTPUT,
@MinItem INT OUTPUT,
@MaxItem INT OUTPUT,
@TotalRows INT OUTPUT,
@MaxPages DECIMAL OUTPUT
AS
BEGIN

SET NOCOUNT ON 
	
	DECLARE @StartPage INT =1
	DECLARE @EndPage INT = ((1 + @MaxRows)-1)
	DECLARE @SQL AS NVARCHAR(MAX) = ''
	DECLARE @MAINSQL AS NVARCHAR(MAX) =''

	SET @MAINSQL = 	'SELECT '+
	'dbo.fn_Lead_PadID(C.Id) Id, '+
	'C.CampaignUID, '+
	'CS.SearchUID, '+
	'CAST(C.CampaignUID AS VARCHAR(50))+'+''''+'|'+''''+'+CAST(CS.SearchUID AS VARCHAR(50)) CommandArg, '+
	'CASE C.CampaignStatus WHEN 0 THEN '+''''+'Draft'+''''+' WHEN 1 THEN '+''''+'Submitted'+''''+' END [Status], '+
	'C.CampaignName [Name], '+
	'U.FirstName +'+''''+' '+''''+ '+ U.LastName [Owner], '+
	'C.Deleted, '+
	'C.HideInSearch, '+
	'CASE C.Deleted WHEN 1 THEN ' +''''+'Yes'+''''+ 'ELSE '+''''+ 'No'+''''+'END DeletedText, '+
	'CASE C.HideInSearch WHEN 1 THEN ' +''''+'Yes'+''''+ 'ELSE '+''''+ 'No'+''''+'END HideInSearchText, '+
	'C.CreatedDate [Created], '+
	'C.CreatedDate [Sent], '+--Replace with value in Job
	'C.CreatedDate [Finished], '+
	'C.DeletedDate [DeletedDate], '+
	'ROW_NUMBER() OVER (ORDER BY ' + @SortBy + ' ' + @SortDirection + ') AS RowNum '+
	'FROM Campaign C WITH (NOLOCK) '+
	'INNER JOIN [User] U WITH (NOLOCK) '+
	'ON C.AccountID=U.AccountID AND C.CreatedBy=U.UserId '+
	'INNER JOIN ContactSearch CS WITH (NOLOCK) '+
	'ON C.AccountID=CS.ACCOUNTID AND C.SearchID=CS.Id '+
	'WHERE C.AccountId= ' + ''''+CAST(@AccountID AS VARCHAR(50))+''''

	IF @Deleted IS NOT NULL
		SET @MAINSQL=' AND C.Deleted=' + CAST(@Deleted AS VARCHAR(5))


	IF @HideInSearch IS NOT NULL
		SET @MAINSQL=' AND C.HideInSearch=' + CAST(@HideInSearch AS VARCHAR(5))


	--PRINT @SQL

	SET @SQL = 'SELECT @COUNT=COUNT(*) FROM (' + @MAINSQL + ') X'

	EXEC SP_EXECUTESQL @SQL, N'@COUNT INT OUTPUT', @TotalRows OUTPUT
	--PRINT '@TotalRows=' + CAST(@TotalRows AS NVARCHAR(5))

	SET @MaxPages=CEILING(CAST(@TotalRows AS DECIMAL)/CAST(@MaxRows AS DECIMAL)) 
	--PRINT '@MaxPages=' + CAST(@MaxPages AS NVARCHAR(5))

	IF (@PageNum> @MaxPages)
	BEGIN
		SET @PageNum=@PageNum-1

		IF (@PageNum =0)
		BEGIN
			SET @PageNum=1
		END
	END


	IF @PageNum = 1
	BEGIN
		SET @StartPage  =1
	END
	ELSE
	BEGIN
		SET @StartPage = ((@PageNum-1) * @MaxRows) +1
	END

	SET @EndPage = ((@StartPage + @MaxRows)-1)


	SET @SQL = 'SELECT * ' +
	'FROM '+
	'( ' +
		@MAINSQL +
	') sub ' +
	'WHERE RowNum >= ' + CAST(@StartPage AS NVARCHAR(10)) + ' AND RowNum <= ' + CAST(@EndPage AS NVARCHAR(10)) 


	SET @NewPageNum=@PageNum
	SET @MinItem = @StartPage

	IF @EndPage > @TotalRows
	BEGIN
		SET @MaxItem = @TotalRows
	END
	ELSE
	BEGIN
		SET @MaxItem = @EndPage
	END

	--PRINT @SQL

	EXEC(@SQL)

END


--DECLARE @NewPageNum INT
--DECLARE @MinItem INT
--DECLARE @MaxItem INT
--DECLARE @TotalRows INT
--DECLARE @MaxPages DECIMAL
--exec S_CAMPAIGN_GETALL '6388071F-36BB-4823-A3ED-8770ADAE0F51',NULL,NULL,100,'C.Id','DESC',1, @NewPageNum OUTPUT,
--@MinItem OUTPUT,
--@MaxItem OUTPUT,
--@TotalRows OUTPUT,
--@MaxPages OUTPUT
