---- =============================================
---- Author:		LGD
---- Create date: 24/12/2019
---- Description:	lead quick search.
---- =============================================
CREATE PROCEDURE [dbo].[S_Lead_QuickSearch]
@Account_ID UNIQUEIDENTIFIER,
@Search nvarchar(250),		-- search criteria. it may contain AND, OR and AND NOT operators
@Status_ID int = NULL,		-- null = any status, 0	= draft, 3 = submitted, 4 = sent
@Deleted bit = 0			-- returns deleted messages. normally returns the undeleted ones
AS
BEGIN
DECLARE @Sql nvarchar(500)
DECLARE @Value nvarchar(100)
DECLARE @Value_ext nvarchar(100)
DECLARE @Search_rebuilt nvarchar(300)
	SET NOCOUNT ON;
	
	IF @Search IS NOT NULL AND UPPER(LTRIM(RTRIM(@Search))) <> '' AND UPPER(LTRIM(RTRIM(@Search))) <> 'OR'
	BEGIN
		SET @Search = REPLACE(@Search, '"', '')
	
		SELECT idx, value
		INTO #tmp_select
		FROM dbo.Split(@Search, ' ')
		
		-- re-build search string
		SET @Search_rebuilt = ''	
		SET @value_ext = ''
		
		DECLARE tmp_select CURSOR LOCAL STATIC FORWARD_ONLY
			FOR 
				SELECT value 
				FROM #tmp_select
				ORDER BY idx		
				
		OPEN tmp_select
		FETCH NEXT FROM tmp_select INTO @value
		
		WHILE @@FETCH_STATUS = 0
		BEGIN		
			IF @value = 'AND' OR @value = 'OR'
			BEGIN
				IF RIGHT(@Search_rebuilt, 4) = ' OR '
					SET @Search_rebuilt = LEFT(@Search_rebuilt, LEN(@Search_rebuilt) - 3)			
				SET @value_ext = ' ' + @value + ' '
			END
			ELSE 
			BEGIN
				IF @value = 'NOT'
				BEGIN	
					IF RIGHT(@Search_rebuilt, 4) = ' OR '
						SET @Search_rebuilt = LEFT(@Search_rebuilt, LEN(@Search_rebuilt) - 3)	
									
					IF @value_ext = ' AND '
					BEGIN 			
						SET @value_ext = ' NOT '						
					END
					ELSE 
					BEGIN
					-- or not is not supported. ignoring "not"
						IF @value_ext = ' OR ' SET @value_ext = ''
					END
				END
				ELSE
				BEGIN		
					SET @value_ext = '"' + @value + '" or ' 
				END
			END
			
			SET @Search_rebuilt = @Search_rebuilt + @value_ext
			
			FETCH NEXT FROM tmp_select INTO @value			
		END

		IF RIGHT(@Search_rebuilt, 4) = ' OR '
			SET @Search_rebuilt = LEFT(@Search_rebuilt, LEN(@Search_rebuilt) - 3)
			
		CLOSE tmp_select
		DEALLOCATE tmp_select
		
		DROP TABLE #tmp_select
				
		SET @Sql = N'SELECT ID, FirstName, LastName, EmailAddress, MobileNumber, ID, ContactGUID
						FROM vSearchContact cc  
						WHERE CONTAINS (cc.SEARCH_FIELDS, ''' + REPLACE(@Search_rebuilt, '''', '''''') + ''') AND 
						cc.AccountID = ' + '''' + CAST(@Account_ID AS NVARCHAR(40)) +''''+ ' AND '
		
		IF @Status_ID IS NOT NULL
			SET @Sql = @Sql + N'cc.ContactStatus = ' + CAST(@Status_ID AS NVARCHAR) + ' AND '
		
		IF @Deleted = 1
			SET @Sql = @Sql + N'cc.DeletedDate IS NOT NULL AND '
		ELSE
			SET @Sql = @Sql + N'cc.DeletedDate IS NULL AND '	 
		
		SET @Sql = LEFT(@Sql, LEN(@Sql) - 4)
		SET @Sql = @Sql + ' ORDER BY cc.LastName, cc.FirstName'
	END
	ELSE
	BEGIN
		SET @Sql = N'SELECT ID, FirstName, LastName, EmailAddress, MobileNumber, ID, ContactGUID 
						FROM vSearchContact cc 
						WHERE cc.AccountID = ' +'''' + CAST(@Account_ID AS NVARCHAR(40))  + ''''+' AND '
		
		IF @Status_ID IS NOT NULL
			SET @Sql = @Sql + N'cc.ContactStatus = ' + CAST(@Status_ID AS NVARCHAR) + ' AND '
		
		if @Deleted = 1
			SET @Sql = @Sql + N'cc.DeletedDate IS NOT NULL AND '
		ELSE
			SET @Sql = @Sql + N'cc.DeletedDate IS NULL AND '	 
		
		SET @Sql = LEFT(@Sql, LEN(@Sql) - 4)
		SET @Sql = @Sql + ' ORDER BY cc.LastName, cc.FirstName'		
	END
	
	PRINT @sql
	
BEGIN TRY	
	EXEC(@sql)
END TRY
	
BEGIN CATCH
	SELECT CAST(ERROR_MESSAGE() AS VARCHAR(100)) AS ERROR
	RETURN ERROR_NUMBER()   
END CATCH
	
	RETURN 0
END