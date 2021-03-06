ALTER PROCEDURE [dbo].[S_LEAD_GETRECENT]
@AccountID UNIQUEIDENTIFIER,
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

	DECLARE @StartPage INT =1
	DECLARE @EndPage INT = ((1 + @MaxRows)-1)
	DECLARE @SQL AS NVARCHAR(MAX) = ''
	DECLARE @MAINSQL AS NVARCHAR(MAX) =''


	SET @MAINSQL = 'SELECT ID ContactID, dbo.fn_Lead_PadID(ID) ContactIDText, ContactID ContactGUID, FirstName, LastName, EmailAddress, MobileNumber, ROW_NUMBER() OVER(ORDER BY ' + @SortBy + ' ' + @SortDirection + ') AS RowNum ' +
				   'FROM Contact WITH (NOLOCK) ' +
                   'WHERE (CAST(DATEADD(HH,8,CreatedDate) AS DATE) BETWEEN (DATEADD(DAY, -7, CAST(DATEADD(HH,8,GETUTCDATE()) AS DATE))) AND CAST(DATEADD(HH,8,GETUTCDATE()) AS DATE)) AND AccountID='+'''' +  CAST(@AccountID AS NVARCHAR(MAX)) + '''' 

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

--DECLARE @AccountID UNIQUEIDENTIFIER='6388071F-36BB-4823-A3ED-8770ADAE0F51'
--DECLARE @MaxRows INT=10
--DECLARE @SortBy VARCHAR(50)='CreatedDate'
--DECLARE @SortDirection VARCHAR(10)='DESC'
--DECLARE @PageNum INT=1
--DECLARE @NewPageNum INT=0
--DECLARE @MinItem INT=0
--DECLARE @MaxItem INT=0
--DECLARE @TotalRows INT=0
--DECLARE @MaxPages DECIMAL=0
--EXEC S_LEAD_GETRECENT @AccountID, @MaxRows, @SortBy, @SortDirection, @PageNum, @NewPageNum OUTPUT, @MinItem OUTPUT, @MaxItem OUTPUT, @TotalRows OUTPUT, @MaxPages OUTPUT