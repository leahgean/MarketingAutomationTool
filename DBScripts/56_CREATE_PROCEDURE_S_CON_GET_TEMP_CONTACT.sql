-- =============================================
-- Author	: Leah Gean Diopenes
-- =============================================
ALTER PROCEDURE [dbo].[S_CON_GET_TEMP_CONTACT] 
 @job_id INT
AS
begin
	SET NOCOUNT ON
	
	declare @TABLE_NAME VARCHAR(100)
	declare @sql varchar(max)
	
	if(isnull(@job_id,0) = 0)
		return;
	
	DECLARE @TBL TABLE (ID INT)		
	SET @TABLE_NAME = 'TMP_CONTACT_JOB_' + CAST(@JOB_ID AS VARCHAR)
	
	set @SQL = 'SELECT ID FROM ' + @TABLE_NAME --[04]
				+ ' WHERE (ISUPLOADED IS NULL OR ISUPLOADED = 0)'
	PRINT @SQL
	
	INSERT INTO @TBL(ID)
	EXEC(@SQL)
	SELECT * FROM @TBL
	
end
