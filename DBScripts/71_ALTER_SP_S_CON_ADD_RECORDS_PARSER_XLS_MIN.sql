-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: 29-Feb-2020
-- Description:	Insert parsed records from csv or txt file into con_contacts
-- =============================================
ALTER PROCEDURE [dbo].[S_CON_ADD_RECORDS_PARSER_XLS_MIN] 
 @job_id INT,
 @id int
AS
begin
	SET NOCOUNT ON
	
	if(isnull(@job_id,0) = 0)
		return;

	declare @Err int
	declare @Msg varchar(50)

	declare @sql varchar(max)
	declare @TABLE_NAME VARCHAR(100)

	declare @AccountID UNIQUEIDENTIFIER
	declare @FirstName NVARCHAR(100)
	declare @LastName NVARCHAR(100)
	declare @EmailAddress NVARCHAR(250)
	declare @MobileNumber VARCHAR(100)
	--[02]
	declare @Title nvarchar(10),
	@CompanyName nvarchar(100),
	@WebSite nvarchar(250),
	@Position nvarchar(50),
	@PhoneNumber VARCHAR(100),
	@Address1 nvarchar(100),
	@Address2 nvarchar(100),
	@Gender char(1),
	@ZipCode nvarchar(20)

	IF OBJECT_ID ('tmp_Contact_job_result', 'U') IS NOT NULL
		DROP TABLE dbo.tmp_Contact_job_result;

	SET @TABLE_NAME = 'TMP_CONTACT_JOB_' + CAST(@JOB_ID AS VARCHAR)
	--[02] -- more values 
	--[03] -- ADDED ID 
	set @SQL = 'select NEWID() AS id,ACCOUNT_ID,FIRST_NAME,LAST_NAME,EMAIL,MOBILE,
				TITLE,		COMPANY_NAME,	WEBSITE,	POSITION_TITLE,		PHONE_NO,
				ADDRESS,	GENDER,			POSTALCODE 
				into DBO.tmp_Contact_job_result from ' + @TABLE_NAME --[04]
				+ ' WHERE (ISUPLOADED IS NULL OR ISUPLOADED = 0) AND ID = ' + CAST(@id AS VARCHAR)
	PRINT @SQL
	EXEC(@SQL)
	--[03]
	CREATE TABLE #RES
	(
		RES VARCHAR(MAX)
	)
	--[03]
	DECLARE @UID uniqueidentifier  
	--SELECT * FROM DBO.tmp_Contact_job_result
	--[03] 
	BEGIN TRANSACTION	
	--[03] USE LOOP INSTEAD OF CURSOR
	WHILE EXISTS(SELECT TOP 1 1 FROM tmp_Contact_job_result)
	BEGIN
		select top 1 @UID = id from tmp_Contact_job_result
		SELECT @AccountID = ACCOUNT_ID, @FirstName = FIRST_NAME, @LastName = LAST_NAME, @EmailAddress = EMAIL, @MobileNumber = MOBILE,  
			@Title = TITLE ,   @CompanyName =COMPANY_NAME  ,   @WebSite =WEBSITE ,  @Position = POSITION_TITLE, @PhoneNumber = PHONE_NO,  
			@Address1 = ADDRESS,  @Gender = GENDER,    @ZipCode = POSTALCODE
				from tmp_Contact_job_result where  id = @UID
	
			insert into #res
			exec S_CON_CONTACT_INSERT
					@AccountID,
					@FirstName,
					@LastName,
					@EmailAddress,
					@MobileNumber,
					1, 
					1,
					NULL, 
					NULL,
					@Gender, 
					@Title,
					@CompanyName,
					@WebSite,
					@Position,
					@PhoneNumber,
					@Address1,
					@Address2, 
					NULL,
					NULL,
					NULL, 
					@ZipCode,
					NULL,
					NULL,
					NULL,
					NULL,
					NULL,
					NULL,
					0,
					@ID OUTPUT
   
			delete tmp_Contact_job_result where  id = @UID
	
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
	
	DECLARE @RES VARCHAR(MAX)
	
	IF(EXISTS(SELECT TOP 1 1 FROM #RES))
		SELECT TOP 1 @RES = RES  FROM #RES
	
	set @SQL = 'UPDATE ' + @TABLE_NAME --[04]
				+ ' SET ISUPLOADED = 1,ERROR = ''' + @RES + ''' WHERE ID = ' + CAST(@id AS VARCHAR)
	PRINT @SQL
	EXEC(@SQL)
	
	COMMIT
end


