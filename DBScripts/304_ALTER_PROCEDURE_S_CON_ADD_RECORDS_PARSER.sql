-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: 19-Jan-2020
-- Description:	Insert parsed records from csv or txt file into con_contacts
-- =============================================
ALTER PROCEDURE [dbo].[S_CON_ADD_RECORDS_PARSER] 
 @job_id INT
AS
begin
	if(isnull(@job_id,0) = 0)
		return;

	declare @Err int
	declare @Msg varchar(50)

	declare @sql varchar(max)
	declare @TABLE_NAME VARCHAR(100)

	declare @ACCOUNT_ID INT
	declare @FIRST_NAME NVARCHAR(100)
	declare @LAST_NAME NVARCHAR(100)
	declare @Email NVARCHAR(250)
	declare @Mobile VARCHAR(100)
	declare @ID bigint

	IF OBJECT_ID ('tmp_Contact_job_result', 'U') IS NOT NULL
		DROP TABLE dbo.tmp_Contact_job_result;

	SET @TABLE_NAME = 'TMP_CON_CONTACT_JOB_' + CAST(@JOB_ID AS VARCHAR)

	set @SQL = 'select ACCOUNT_ID,FIRST_NAME,LAST_NAME,EMAIL,MOBILE into DBO.tmp_Contact_job_result from ' + @TABLE_NAME
	PRINT @SQL
	EXEC(@SQL)

	SELECT * FROM DBO.tmp_Contact_job_result

	DECLARE CALC cursor
	FAST_FORWARD
	For
	select * from dbo.tmp_Contact_job_result
	Open CALC;

	fetch from CALC
	into @ACCOUNT_ID, @FIRST_NAME, @LAST_NAME, @EMAIL, @MOBILE;

	WHILE @@FETCH_STATUS = 0
	BEGIN	
		--[01]
		exec S_CON_CONTACT_INSERT_PARSER 
			@ACCOUNT_ID, 
			@FIRST_NAME,
			@LAST_NAME,
			@EMAIL, 
			@MOBILE , 
			0, 
			'BULK_UPLOAD',
			NULL,
			NULL,
			0, 
			0, 
			NULL, 
			@ID OUTPUT

		set @Err=@@error
		if @err <> 0 
		begin
			set @Msg='FAILED TO CREATE CONTACT'			
			ROLLBACK TRANSACTION
			CLOSE CALC;
			DEALLOCATE CALC;
			raiserror(@Msg,16,1)			
		end		

		fetch NEXT from CALC
		into @ACCOUNT_ID, @FIRST_NAME, @LAST_NAME, @EMAIL, @MOBILE;
	END					

	CLOSE CALC;
	DEALLOCATE CALC;
	DROP TABLE tmp_Contact_job_result
end

