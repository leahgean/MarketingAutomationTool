CREATE PROCEDURE S_ContactListJob_GetExistingImports
@AccountID UNIQUEIDENTIFIER,
@PageNum INT,
@MaxRows INT,
@SortBy VARCHAR(50),
@SortDirection VARCHAR(10),
@MinItem INT OUT,
@MaxItem INT OUT,
@TotalRows INT OUT
AS
BEGIN


	DECLARE @StartPage INT =1
	DECLARE @EndPage INT = ((1 + @MaxRows)-1)
	DECLARE @SQL AS NVARCHAR(MAX) = ''
	DECLARE @AccountNameSQL AS NVARCHAR(MAX) = ''

	IF @PageNum = 1
	BEGIN
		SET @StartPage  =1
	END
	ELSE
	BEGIN
		SET @StartPage = ((@PageNum-1) * @MaxRows) +1
	
	END

	SET @EndPage = ((@StartPage + @MaxRows)-1)

	DECLARE @MAINSQL AS NVARCHAR(MAX) = 'SELECT C.JobId, C.JobName, C.JobStatusId, C.AccountID, C.CreatedBy, U.FirstName + ' + ''''+ '' + '''' + ' + U.LastName CreatedByName, C.FileFormat, C.OriginalFileName, C.[FileName], C.ContactListId, C.DateCreated, C.JobStarted, C.JobFinished, C.Error, C.TotalContacts, C.UploadedContacts, ROW_NUMBER() OVER (ORDER BY ' + @SortBy + ' ' + @SortDirection + ') AS RowNum FROM ContactListJob C WITH (NOLOCK) INNER JOIN [User] U WITH (NOLOCK) ON C.CreatedBy=U.UserId WHERE C.AccountID='+ ''''+ CAST(@AccountId AS NVARCHAR(50)) +''''

	SET @SQL = 'SELECT @COUNT=COUNT(*) FROM (' + @MAINSQL + ') X'

	EXEC SP_EXECUTESQL @SQL, N'@COUNT INT OUTPUT', @TotalRows OUTPUT

	SET @SQL = 'SELECT * ' +
	'FROM '+
	'( ' +
		@MAINSQL +
	') sub ' +
	'WHERE sub.RowNum >= ' + CAST(@StartPage AS NVARCHAR(10)) + ' AND sub.RowNum <= ' + CAST(@EndPage AS NVARCHAR(10)) 


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


DECLARE @MinItem INT=0
DECLARE @MaxItem INT=0 
DECLARE @TotalRows INT=0
exec S_ContactListJob_GetExistingImports 'CCF4D547-C9EC-42B4-97DF-B12752734323', 2,1,'DateCreated','DESC',@MinItem OUT,@MaxItem OUT,@TotalRows OUT


