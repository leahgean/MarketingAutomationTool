-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: Jan-1-2020
-- Description:	Gets group list by page size
---- =============================================
ALTER PROCEDURE dbo.S_LIST_GET
@AccountID AS UNIQUEIDENTIFIER,
@IsDeleted BIT,
@SortColumn VARCHAR(50),
@SortOrder VARCHAR(3),
@PageSize INT,
@Page INT,
@TotalCount INT OUTPUT,
@PageCount INT OUTPUT
AS
BEGIN

SET NOCOUNT ON 
	
	DECLARE 
	@SQL VARCHAR(MAX),
	@TABLE_NAME VARCHAR(MAX),
	@COUNTSQL NVARCHAR(MAX),
	@PARMDEFINITION NVARCHAR(500),
	@LASTPAGE INT
	
	DECLARE @RESULT_TABLE AS TABLE(ID INT, AccountID UNIQUEIDENTIFIER, ListName NVARCHAR(100), ListDescription NVARCHAR(500), DateCreated DATETIME, CreatedBy UNIQUEIDENTIFIER, ModifiedDate DATETIME, ModifiedBy UNIQUEIDENTIFIER, DateDeleted DATETIME, IsDeleted BIT, ROWNUMBER INT)
	
	SET @SQL = 'SELECT ID, AccountID, ListName, ListDescription, DateCreated, CreatedBy, ModifiedDate, ModifiedBy, DateDeleted, IsDeleted, ROW_NUMBER() OVER (ORDER BY ' + @SortColumn + ' ' + @SortOrder + ') AS ROWNUMBER FROM dbo.ContactList WHERE AccountId =' + '''' + CAST(@AccountID AS NVARCHAR(MAX)) + '''' + ' AND IsDeleted=' + CAST(@IsDeleted AS NVARCHAR(MAX))
	
	
	SET @COUNTSQL = 'SELECT @Total = COUNT(*) FROM (' + @SQL + ') t ' 
		
	SET @PARMDEFINITION = N'@TOTAL INT OUTPUT'
	EXECUTE sp_executesql @COUNTSQL,
	@PARMDEFINITION,
	@TOTAL = @TOTALCOUNT OUTPUT
	
	SET @LastPage = CEILING(CAST(@TOTALCOUNT AS FLOAT)/CAST(@PageSize AS FLOAT))
	IF (@Page > @LASTPAGE)
		SET @Page = @LASTPAGE
	
	SET @COUNTSQL = 'SELECT @CURPAGETOTAL = COUNT(*) FROM (' + @SQL + ') t ' +
					' WHERE ROWNUMBER BETWEEN ' + CAST(((@Page - 1)* @PageSize) + 1 AS VARCHAR) + 
					' AND ' + CAST((@Page * @PageSize) AS VARCHAR)
					
	
	SET @PARMDEFINITION = N'@CurPageTotal INT OUTPUT'
	EXECUTE sp_executesql @COUNTSQL,
	@PARMDEFINITION,
	@CURPAGETOTAL = @PageCOUNT OUTPUT
	
	SET @SQL = 'SELECT ID, AccountID, ListName, ListDescription, DateCreated, CreatedBy, ModifiedDate, ModifiedBy, DateDeleted,IsDeleted, ROWNUMBER FROM (' + @SQL + ') t ' +
					' WHERE ROWNUMBER BETWEEN ' + CAST(((@Page - 1)* @PageSize) + 1 AS VARCHAR) + 
					' AND ' + CAST((@Page * @PageSize) AS VARCHAR)
	--PRINT @SQL

	INSERT INTO @RESULT_TABLE(ID, AccountID, ListName, ListDescription, DateCreated, CreatedBy, ModifiedDate, ModifiedBy, DateDeleted, IsDeleted, ROWNUMBER)
	EXEC(@SQL)
	
	SELECT ID, AccountID, ListName, ListDescription, DateCreated, CreatedBy, ModifiedDate, ModifiedBy, DateDeleted, IsDeleted, ROWNUMBER
	FROM @RESULT_TABLE

END




