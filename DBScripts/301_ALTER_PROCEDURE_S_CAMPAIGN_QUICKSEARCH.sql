-- =============================================
-- Author:		LGD
-- Create date: 12/12/2020
-- Description:	email quick search. 
-- =============================================
ALTER PROCEDURE [dbo].[S_CAMPAIGN_QUICKSEARCH]
	@AccountId UNIQUEIDENTIFIER,
	@search nvarchar(250),		-- search criteria. it may contain AND, OR and AND NOT operators
	@status_ID int = null,		-- null = any status, 0	= draft, 1 = submitted
	@deleted bit = 0,			-- returns deleted messages. normally returns the undeleted ones
	@includehidden bit=0
AS
BEGIN
DECLARE @sql NVARCHAR(MAX)
DECLARE @value NVARCHAR(100)
DECLARE @value_ext NVARCHAR(100)
DECLARE @search_rebuilt NVARCHAR(300)
	SET NOCOUNT ON
	
	IF @search IS NOT NULL AND LTRIM(RTRIM(@search)) <> '' AND LTRIM(RTRIM(@search)) <> 'or'
	BEGIN
		SET @search = REPLACE(@search, '"', '')
	
		SELECT idx, value
		INTO #tmp_select
		FROM dbo.Split(@search, ' ')
		
		-- re-build search string
		SET @search_rebuilt = ''	
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
			SET @value = LTRIM(RTRIM(@value))
				
			IF @value = 'and' or @value = 'or'
			BEGIN
				IF LTRIM(RTRIM(@value_ext)) = 'or' OR LTRIM(RTRIM(@value_ext)) = 'and' OR LTRIM(RTRIM(@value_ext)) = 'not'
					SET @value_ext = ''
				ELSE
				BEGIN
					IF RIGHT(@search_rebuilt, 4) = ' or '
						SET @search_rebuilt = LEFT(@search_rebuilt, LEN(@search_rebuilt) - 3)	
										
					SET @value_ext = ' ' + @value + ' '
				END
			END
			ELSE 
			BEGIN
				IF @value = 'not'
				BEGIN	
					IF LTRIM(RTRIM(@value_ext)) = 'and'
					BEGIN 		
						IF RIGHT(@search_rebuilt, 4) = ' or '
							SET @search_rebuilt = LEFT(@search_rebuilt, LEN(@search_rebuilt) - 3)	
											
						SET @value_ext = ' not '						
					END
					ELSE 
					BEGIN
					-- or not is not supported. ignoring "not"
						IF LTRIM(RTRIM(@value_ext)) = 'or' OR LTRIM(RTRIM(@value_ext)) = 'and' OR LTRIM(RTRIM(@value_ext)) = 'not' OR LTRIM(RTRIM(@value_ext)) = ''
							SET @value_ext = ''
					END
				END
				ELSE
				BEGIN		
					SET @value_ext = '"' + @value + '" or ' 
				END
			END
			
			SET @search_rebuilt = @search_rebuilt + @value_ext		
			
			IF LTRIM(RTRIM(@value_ext)) <> 'and'
				SET @value_ext = ''
			
			FETCH NEXT FROM tmp_select INTO @value			
		END

		CLOSE tmp_select
		DEALLOCATE tmp_select
		
		DROP TABLE #tmp_select

		IF RIGHT(@search_rebuilt, 4) = ' or '
			SET @search_rebuilt = LEFT(@search_rebuilt, LEN(@search_rebuilt) - 3)			
				
		SET @sql = N'SELECT Id, CampaignUID, CampaignName, [Subject], SenderEmail, SenderName, SEARCHUID, DATEADD(HH,8,CreatedDate) CreatedDate, Deleted '+
						'FROM vSearchCampaignMessage cm '+
						'WHERE CONTAINS (cm.SEARCH_FIELDS, ''' + REPLACE(@search_rebuilt, '''', '''''') + ''') AND '+
						'cm.AccountId = ' + '''' + CAST(@AccountId as nvarchar(50)) + '''' + ' '
						
		
		IF @status_ID IS NOT NULL
			SET @sql = @sql + N'AND cm.CampaignStatus = ' + CAST(@status_ID AS NVARCHAR(10)) + ' '
		
		IF @deleted IS NOT NULL
			SET @sql = @sql + N'AND cm.Deleted = ' + CAST(@deleted AS NVARCHAR(1)) + ' '

		IF @includehidden IS NOT NULL
		BEGIN
			IF (@includehidden=0)
			BEGIN
				SET @sql = @sql + N'AND cm.HideInSearch = 0 '
			END
		END

		SET @sql = @sql + 'ORDER BY cm.CampaignStatus, cm.CreatedDate DESC'
	END
	ELSE
	BEGIN
		SET @sql = N'SELECT Id, CampaignUID, CampaignName, [Subject], SenderEmail, SenderName, SEARCHUID,DATEADD(HH,8,CreatedDate) CreatedDate, Deleted '+
						'FROM vSearchCampaignMessage cm '+
						'WHERE cm.AccountId = ' + '''' + CAST(@AccountId as nvarchar(50)) + '''' + ' '
		
		IF @status_ID IS NOT NULL
			SET @sql = @sql + N'AND cm.CampaignStatus = ' + CAST(@status_ID AS NVARCHAR(10)) + ' '
		
		IF @deleted IS NOT NULL
			SET @sql = @sql + N'AND cm.Deleted = ' + CAST(@deleted AS NVARCHAR(1)) + ' '

		IF @includehidden IS NOT NULL
		BEGIN
			IF (@includehidden=0)
			BEGIN
				SET @sql = @sql + N'AND cm.HideInSearch = 0 '
			END
		END
		
		SET @sql = @sql + 'ORDER BY cm.CampaignStatus, cm.CreatedDate DESC'	
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

