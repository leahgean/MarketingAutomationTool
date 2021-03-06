CREATE PROCEDURE [dbo].[S_ContactList_GetRemovedListMembers]
@ContactListID INT,
@AccountId UNIQUEIDENTIFIER,
@MaxRows INT,
@SortBy VARCHAR(50),
@SortDirection VARCHAR(10),
@RemoveList NVARCHAR(MAX),
@PageNum INT,
@NewPageNum INT OUTPUT,
@MinItem INT OUTPUT,
@MaxItem INT OUTPUT,
@TotalRows INT OUTPUT,
@MaxPages DECIMAL OUTPUT
AS
BEGIN

	DECLARE @SQL NVARCHAR(MAX)=''
	DECLARE @MAINSQL NVARCHAR(MAX)=''

	IF (@RemoveList IS NOT NULL)
	BEGIN

		SELECT @SQL='IF OBJECT_ID('+''''+'TempDB..'+'##tmp_RLM_RemoveList_' + CAST(@ContactListID AS NVARCHAR(20))+''''+','+''''+'U'+''''+') IS NOT NULL DROP TABLE '+ '##tmp_RLM_RemoveList_' + CAST(@ContactListID AS NVARCHAR(20))
		--PRINT(@SQL)
		EXEC(@SQL) 

		SELECT @SQL='SELECT CAST([Value] AS INT) [Value] ' +
						'INTO ##tmp_RLM_RemoveList_' + CAST(@ContactListID AS NVARCHAR(20)) + ' ' +
						'FROM dbo.Split('+'''' + @RemoveList +''''+',''' +',' +''''+')'
		--PRINT(@SQL)
		EXEC(@SQL)
	END

	SELECT @MAINSQL='SELECT  dbo.fn_Lead_PadID(C.ID) ID, C.ContactID, C.AccountID, C.FirstName, C.LastName, C.MobileNumber, C.EmailAddress, C.FacebookID, C.IsDeleted, C.SubscribedToEmail, ROW_NUMBER() OVER(ORDER BY '+dbo.fnColumnName(@SortBy) + ' ' + @SortDirection + ') RowNum '+
	'FROM ##tmp_RLM_RemoveList_' + CAST(@ContactListID AS NVARCHAR(20)) + ' R ' +
	'INNER JOIN Contact C WITH (NOLOCK) '+
	'ON R.[Value]=C.ID AND C.AccountId= '+''''+ CAST(@AccountId AS NVARCHAR(50))+'''' 

	--PRINT(@SQL)

	SET @SQL = 'SELECT @COUNT=COUNT(*) FROM (' + @MAINSQL + ') sub'

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
	
	DECLARE @StartPage INT =1
	DECLARE @EndPage INT = ((1 + @MaxRows)-1)
	
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

	--PRINT '@SQL=' + @SQL

	EXEC(@SQL)


END


