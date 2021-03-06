ALTER PROCEDURE [dbo].[S_CONTACTSEARCH_GETEXCLUDELIST]
@SearchID INT,
@AccountID UNIQUEIDENTIFIER,
@CreatedBy UNIQUEIDENTIFIER,
@MaxRows INT,
@SortBy VARCHAR(50),
@SortDirection VARCHAR(10),
@ExcludeList NVARCHAR(MAX),
@PageNum INT,
@NewPageNum INT OUTPUT,
@MinItem INT OUTPUT,
@MaxItem INT OUTPUT,
@TotalRows INT OUTPUT,
@MaxPages DECIMAL OUTPUT
AS
BEGIN
	
	DECLARE @SQL NVARCHAR(MAX)=''
	DECLARE @SEARCHCRITERIA NVARCHAR(MAX)=''
	DECLARE @MAINSQL NVARCHAR(MAX)=''

	SELECT @SQL='IF OBJECT_ID('+''''+'TempDB..'+'##tmp_ExcludeList_' + CAST(@SearchID AS NVARCHAR(20))+''''+','+''''+'U'+''''+') IS NOT NULL DROP TABLE '+ '##tmp_ContactSearch_' + CAST(@SearchID AS NVARCHAR(20))

	--PRINT(@SQL)
	EXEC(@SQL) 

	SELECT @SQL='SELECT CAST([Value] AS INT) [Value] ' +
					'INTO ##tmp_ExcludeList_' + CAST(@SearchID AS NVARCHAR(20)) + ' ' +
					'FROM dbo.Split('+'''' + @ExcludeList +''''+',''' +',' +''''+')'

	--PRINT(@SQL)
	EXEC(@SQL) 

	SET @MAINSQL='SELECT dbo.fn_Lead_PadID(C.ID) ID, C.ContactID, C.AccountID, C.FirstName, C.MiddleName, C.LastName, C.MobileNumber, C.EmailAddress, C.FacebookID, C.IsDeleted,CAST(ISNULL(C.IsDeleted,0) AS INT) IsDeletedNum, ROW_NUMBER () OVER ( ORDER BY '+ dbo.fnColumnName(@SortBy) + ' ' + @SortDirection + ' )  RowNum FROM ##tmp_ExcludeList_' + CAST(@SearchID AS NVARCHAR(20)) + ' X '
	SET @MAINSQL= @MAINSQL + 'LEFT JOIN ' + ' Contact C WITH (NOLOCK) '
	SET @MAINSQL= @MAINSQL + 'ON X.[Value] = C.[ID] ' 
	SET @MAINSQL= @MAINSQL + ' WHERE C.[ID] IS NOT NULL'

	--PRINT '@MAINSQL=' + @MAINSQL

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