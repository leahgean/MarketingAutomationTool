-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: 29-Feb-2020
-- Description:	Insert parsed records from csv or txt file into con_contacts
-- =============================================
CREATE PROCEDURE [dbo].[S_CON_ADD_RECORDS_PARSER_XLS] 
 @job_id INT
AS
begin
	SET NOCOUNT ON
	
	if(isnull(@job_id,0) = 0)
		return;

	declare @Err int
	declare @Msg varchar(50)

	declare @sql varchar(max)
	declare @TABLE_NAME VARCHAR(100)

	declare @ACCOUNT_ID UNIQUEIDENTIFIER
	declare @FIRST_NAME NVARCHAR(100)
	declare @LAST_NAME NVARCHAR(100)
	declare @Email NVARCHAR(250)
	declare @Mobile VARCHAR(100)
	declare @CONTACT_ID UNIQUEIDENTIFIER
	--[02]
	declare @TITLE nvarchar(10),
	@COMPANY_NAME nvarchar(100),
	@WEBSITE nvarchar(250),
	@POSITION_TITLE nvarchar(50),
	@PHONE_NO VARCHAR(100),
	@ADDRESS nvarchar(100),
	@GENDER char(1),
	@POSTALCODE nvarchar(20)

	IF OBJECT_ID ('tmp_Contact_job_result', 'U') IS NOT NULL
		DROP TABLE dbo.tmp_Contact_job_result;

	SET @TABLE_NAME = 'TMP_CONTACT_JOB_' + CAST(@JOB_ID AS VARCHAR)
	--[02] -- more values 
	--[03] -- ADDED ID 
	set @SQL = 'select NEWID() AS id,ACCOUNT_ID,FIRST_NAME,LAST_NAME,EMAIL,MOBILE,
				TITLE,		COMPANY_NAME,	WEBSITE,	POSITION_TITLE,		PHONE_NO,
				ADDRESS,	GENDER,			POSTALCODE 
				into DBO.tmp_Contact_job_result from ' + @TABLE_NAME
	PRINT @SQL
	EXEC(@SQL)
	--[03]
	CREATE TABLE #RES
	(
		RES VARCHAR(MAX)
	)
	--[03]
	DECLARE @id uniqueidentifier  
	--SELECT * FROM DBO.tmp_Contact_job_result
	--[03] 
	BEGIN TRANSACTION
	/*
	DECLARE CALC cursor
	FAST_FORWARD
	For
	select * from dbo.tmp_Contact_job_result
	Open CALC;

	fetch from CALC
	into @ACCOUNT_ID, @FIRST_NAME, @LAST_NAME, @EMAIL, @MOBILE,
		@TITLE,			@COMPANY_NAME ,			@WEBSITE,		@POSITION_TITLE,	@PHONE_NO,
		@ADDRESS,		@GENDER,				@POSTALCODE;
		
	WHILE @@FETCH_STATUS = 0
	BEGIN	
		--[01]--[02] change this to the latest version
		--[03]
		INSERT INTO #RES
		exec S_CON_CONTACT_INSERT
			@ACCOUNT_ID, 
			@FIRST_NAME,
			@LAST_NAME,
			@EMAIL, 
			@MOBILE , 
			0, 
			0,
			0,
			0,
			'BULK_UPLOAD',
			NULL,
			NULL,
			0, 
			0, -- abd: 04.18.2012 Filter_ID
			NULL, -- abd: 04.18.2012 - PREVENT_DUPLICATEE_MAIL ? Why not supplied here
			@CONTACT_ID OUTPUT,
			0,
			@TITLE,
			@COMPANY_NAME ,
			@WEBSITE,
			@POSITION_TITLE,
			@PHONE_NO,
			@ADDRESS,
			NULL,
			NULL,
			NULL,
			@GENDER,
			@POSTALCODE

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
		into @ACCOUNT_ID, @FIRST_NAME, @LAST_NAME, @EMAIL, @MOBILE,
			@TITLE,			@COMPANY_NAME ,			@WEBSITE,		@POSITION_TITLE,	@PHONE_NO,
			@ADDRESS,		@GENDER,				@POSTALCODE;
	END					

	CLOSE CALC;
	DEALLOCATE CALC;
	DROP TABLE tmp_Contact_job_result
	*/
	--[03] USE LOOP INSTEAD OF CURSOR
	WHILE EXISTS(SELECT TOP 1 1 FROM tmp_Contact_job_result)
	BEGIN
		select top 1 @id = id from tmp_Contact_job_result
		SELECT @ACCOUNT_ID = ACCOUNT_ID, @FIRST_NAME = FIRST_NAME, @LAST_NAME = LAST_NAME, @EMAIL = EMAIL, @MOBILE = MOBILE,  
			@TITLE = TITLE ,   @COMPANY_NAME =COMPANY_NAME  ,   @WEBSITE =WEBSITE ,  @POSITION_TITLE = POSITION_TITLE, @PHONE_NO = PHONE_NO,  
			@ADDRESS = ADDRESS,  @GENDER = GENDER,    @POSTALCODE = POSTALCODE
				from tmp_Contact_job_result where  id = @id
	
			insert into #res
			exec S_CON_CONTACT_INSERT  
					@ACCOUNT_ID,   
					@FIRST_NAME,  
					@LAST_NAME,  
					@EMAIL,   
					@MOBILE ,   
					0,   
   					0,  
   					0,  
   					0,  
   					'BULK_UPLOAD',  
   					NULL,  
					   					NULL,  
   					0,   
   					0, -- abd: 04.18.2012 Filter_ID  
   					NULL, -- abd: 04.18.2012 - PREVENT_DUPLICATEE_MAIL ? Why not supplied here  
   					@CONTACT_ID OUTPUT,  
   					0,  
					  @TITLE,  
   					@COMPANY_NAME ,  
   					@WEBSITE,  
   					@POSITION_TITLE,  
   					@PHONE_NO,  
   					@ADDRESS,  
   					NULL,  
   					NULL,  
   					NULL,  
   					@GENDER,  
   					@POSTALCODE 
   
			delete tmp_Contact_job_result where  id = @id
	
			/*print @count
			set @count = @count + 1 
			*/
			set @Err=@@error
			if @err <> 0 
			begin
				set @Msg='FAILED TO CREATE CONTACT'			
				ROLLBACK TRANSACTION
				CLOSE CALC;
				DEALLOCATE CALC;
				raiserror(@Msg,16,1)			
			end
		
	END
	COMMIT
end

