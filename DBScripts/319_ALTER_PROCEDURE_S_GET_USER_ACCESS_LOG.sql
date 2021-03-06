ALTER PROCEDURE [dbo].[S_GET_USER_ACCESS_LOG]
@UserId UNIQUEIDENTIFIER,
@LoginDateTimeFrom DATETIME,
@LoginDateTimeTo DATETIME,
@LogoutDateTimeFrom DATETIME,
@LogoutDateTimeTo DATETIME,
@IPAddress VARCHAR(15),
@PageNum INT,
@MaxRows INT,
@MinItem INT OUT,
@MaxItem INT OUT,
@TotalRows INT OUT
AS
BEGIN
	DECLARE @StartPage INT =1
	DECLARE @EndPage INT = ((1 + @MaxRows)-1)
	DECLARE @SQL AS NVARCHAR(MAX) = ''



	IF @PageNum = 1
	BEGIN
		SET @StartPage  =1
	END
	ELSE
	BEGIN
		SET @StartPage = ((@PageNum-1) * @MaxRows) +1
	
	END

	SET @EndPage = ((@StartPage + @MaxRows)-1)



	DECLARE @MAINSQL AS NVARCHAR(MAX) = 'SELECT *, ROW_NUMBER() OVER(ORDER BY Log_In DESC, Log_Out DESC) RowNum ' +
		'FROM User_Access_Log  WITH (NOLOCK) ' +
		'WHERE [User_Id]=' + '''' +  CAST(@UserId AS NVARCHAR(50)) +  ''''

	IF (@LoginDateTimeFrom IS NOT NULL)
	BEGIN
		SET @MAINSQL = @MAINSQL + ' AND DATEADD(HH,8,Log_In) >=' + '''' +  CAST(@LoginDateTimeFrom AS NVARCHAR(30)) + ''''
	END


	IF (@LoginDateTimeTo IS NOT NULL)
	BEGIN
		SET @MAINSQL = @MAINSQL + ' AND DATEADD(HH,8,Log_In) <=' + '''' +  CAST(@LoginDateTimeTo AS NVARCHAR(30)) + ''''
	END


	IF (@LogoutDateTimeFrom IS NOT NULL)
	BEGIN
		SET @MAINSQL = @MAINSQL + ' AND DATEADD(HH,8,Log_Out) >=' + '''' +  CAST(@LogoutDateTimeFrom AS NVARCHAR(30)) + ''''
	END


	IF (@LogoutDateTimeTo IS NOT NULL)
	BEGIN
		SET @MAINSQL = @MAINSQL + ' AND DATEADD(HH,8,Log_Out) <=' + '''' +  CAST(@LogoutDateTimeTo AS NVARCHAR(30)) + ''''
	END
		
	IF ((@IPAddress IS NOT NULL) AND (@IPAddress <> ''))
	BEGIN
		SET @MAINSQL = @MAINSQL + ' AND IP_Address =' + '''' +  @IPAddress + ''''
	END

	PRINT @SQL

	SET @SQL = 'SELECT @COUNT=COUNT(*) FROM (' + @MAINSQL + ') X'

	EXEC SP_EXECUTESQL @SQL, N'@COUNT INT OUTPUT', @TotalRows OUTPUT


	SET @SQL = 'SELECT * ' +
	'FROM '+
	'( ' +
		@MAINSQL +
	') sub ' +
	'WHERE RowNum >= ' + CAST(@StartPage AS NVARCHAR(10)) + ' AND RowNum <= ' + CAST(@EndPage AS NVARCHAR(10)) 


	SET @MinItem = @StartPage

	IF @EndPage > @TotalRows
	BEGIN
		SET @MaxItem = @TotalRows
	END
	ELSE
	BEGIN
		SET @MaxItem = @EndPage
	END

	PRINT @SQL

	EXEC(@SQL)
END




