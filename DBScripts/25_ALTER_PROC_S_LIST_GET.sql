-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: Jan-1-2020
-- Description:	Gets group list by page size
---- =============================================
ALTER PROCEDURE dbo.S_LIST_GET
@AccountID AS UNIQUEIDENTIFIER,
@IsDeleted BIT,
@PageNum INT,
@MaxRows INT,
@SortBy VARCHAR(50),
@SortDirection VARCHAR(10),
@MinItem INT OUT,
@MaxItem INT OUT,
@TotalRows INT OUT
AS
BEGIN

SET NOCOUNT ON 
	
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

	DECLARE @MAINSQL AS NVARCHAR(MAX) = 'SELECT ID, AccountID, ListName, ListDescription, DateCreated, CreatedBy, ModifiedDate, ModifiedBy, DateDeleted, IsDeleted, ROW_NUMBER() OVER (ORDER BY ' + @SortBy + ' ' + @SortDirection + ') AS RowNum FROM dbo.ContactList WITH (NOLOCK) WHERE AccountId =' + '''' + CAST(@AccountID AS NVARCHAR(MAX)) + '''' + ' AND IsDeleted=' + CAST(@IsDeleted AS NVARCHAR(MAX))

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
