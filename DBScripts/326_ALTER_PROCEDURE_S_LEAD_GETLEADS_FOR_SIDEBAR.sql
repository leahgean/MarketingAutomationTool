ALTER PROCEDURE [dbo].[S_LEAD_GETLEADS_FOR_SIDEBAR]
@AccountID UNIQUEIDENTIFIER,
@Status INT,
@Deleted BIT,
@SearchText NVARCHAR(250),
@PageNum INT
AS
BEGIN

DECLARE @MaxRows INT = 10
DECLARE @SQL AS NVARCHAR(MAX)=''
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

SET @SQL = 'SELECT LastName, FirstName, ID, dbo.fn_Lead_PadID(ID) ContactID, EmailAddress, MobileNumber, ContactID ContactGUID, ROW_NUMBER () OVER (ORDER BY LastName, FirstName)  RowNum ' +
'FROM Contact WITH (NOLOCK) ' +
'WHERE AccountID = ' + '''' + CAST(@AccountID AS NVARCHAR(50)) + ''' ' 
PRINT @SQL

IF (@Status IS NOT NULL)
BEGIN
	IF ((@Status = 1) OR (@Status = 2))
	BEGIN
		SET @SQL = @SQL + 'AND LeadStatus=' + CAST(@Status AS NVARCHAR(1)) + ' '
	END
	ELSE
	BEGIN
		SET @SQL = @SQL + 'AND ContactStatus=' + CAST(@Status AS NVARCHAR(1)) + ' '
	END
END
--PRINT @SQL

IF (@Deleted IS NOT NULL)
BEGIN
	SET @SQL = @SQL + 'AND IsDeleted=' + CAST(@Deleted AS NVARCHAR(1)) + ' '
END
PRINT @SQL
IF (@SearchText IS NOT NULL)
BEGIN
	SET @SQL = @SQL + 'AND (FirstName LIKE ' + '''' + '%' + @SearchText + '%'  + ''' ' + 'OR '
	SET @SQL = @SQL + ' LastName LIKE ' + '''' + '%' + @SearchText + '%' + + ''' ' + 'OR '
	SET @SQL = @SQL + ' EmailAddress LIKE ' + '''' + '%' + @SearchText + '%' + + ''' ' + 'OR '
	SET @SQL = @SQL + ' MobileNumber LIKE ' + '''' + '%' + @SearchText + '%' + + ''') ' 

END
--PRINT @SQL

	SET @SQL = 'SELECT * ' +
	'FROM '+
	'( ' +
		@SQL +
	') sub ' +
	'WHERE RowNum >= ' + CAST(@StartPage AS NVARCHAR(10)) + ' AND RowNum <= ' + CAST(@EndPage AS NVARCHAR(10)) 

	SET @SQL = @SQL + 'ORDER BY LastName, FirstName'

	--PRINT @SQL
	EXEC (@SQL)
END