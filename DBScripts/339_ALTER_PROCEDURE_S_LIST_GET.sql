-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: Jan-1-2020
-- Description:	Gets group list by page size
---- =============================================
ALTER PROCEDURE [dbo].[S_LIST_GET]
@AccountID AS UNIQUEIDENTIFIER,
@IsDeleted BIT,
@MaxRows INT,
@SortBy VARCHAR(50),
@SortDirection VARCHAR(10),
@PageNum INT,
@NewPageNum INT OUTPUT,
@MinItem INT OUT,
@MaxItem INT OUT,
@TotalRows INT OUT,
@MaxPages DECIMAL OUTPUT
AS
BEGIN

SET NOCOUNT ON 
	
	DECLARE @StartPage INT =1
	DECLARE @EndPage INT = ((1 + @MaxRows)-1)
	DECLARE @SQL AS NVARCHAR(MAX) = ''
	DECLARE @MAINSQL AS NVARCHAR(MAX) =''

	SET @MAINSQL = 'SELECT ID, AccountID, ListName, ListDescription, DATEADD(HH,8,DateCreated) DateCreated, CreatedBy, DATEADD(HH,8,ModifiedDate) ModifiedDate, ModifiedBy, DATEADD(HH,8,DateDeleted) DateDeleted, IsDeleted, ROW_NUMBER() OVER (ORDER BY ' + @SortBy + ' ' + @SortDirection + ') AS RowNum FROM dbo.ContactList WITH (NOLOCK) WHERE AccountId =' + '''' + CAST(@AccountID AS NVARCHAR(MAX)) + '''' + ' AND IsDeleted=' + CAST(@IsDeleted AS NVARCHAR(MAX))
	
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
