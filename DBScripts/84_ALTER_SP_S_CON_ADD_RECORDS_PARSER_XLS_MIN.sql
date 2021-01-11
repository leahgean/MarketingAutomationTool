USE [MarketingAutomationTool]
GO
/****** Object:  StoredProcedure [dbo].[S_CON_ADD_RECORDS_PARSER_XLS_MIN]    Script Date: 3/9/2020 3:33:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
	
	IF(ISNULL(@job_id,0) = 0)
		RETURN

		DECLARE @Err int
		DECLARE @Msg varchar(50)

		DECLARE @sql varchar(max)
		DECLARE @TABLE_NAME VARCHAR(100)

		DECLARE @AccountID UNIQUEIDENTIFIER
		DECLARE @FirstName NVARCHAR(100)
		DECLARE @LastName NVARCHAR(100)
		DECLARE @EmailAddress NVARCHAR(250)
		DECLARE @MobileNumber VARCHAR(100)
		DECLARE @SubscribedToEmail BIT
		DECLARE @ContactType INT
		DECLARE @ContactStatus INT
		DECLARE @LeadStatus INT
		DECLARE @Gender CHAR(1)
		DECLARE @Title NVARCHAR(50)
		DECLARE @CompanyName NVARCHAR(100)
		DECLARE @WebSite NVARCHAR(250)
		DECLARE @Position NVARCHAR(250)
		DECLARE @PhoneNumber VARCHAR(100)
		DECLARE @Address1 NVARCHAR(250)
		DECLARE @Address2 NVARCHAR(250)
		DECLARE @City NVARCHAR(50)
		DECLARE @State NVARCHAR(50)
		DECLARE @CountryId INT
		DECLARE @ZipCode NVARCHAR(20)
		DECLARE @SubscribedToEmailVia VARCHAR(50)
		DECLARE @CreatedBy UNIQUEIDENTIFIER
		DECLARE @SubscribedToEmailIPAddress VARCHAR(15)
		DECLARE @FormId	INT
		DECLARE @ContactListId INT
		DECLARE @FilterId BIGINT
		DECLARE @PreventDuplicateEmail BIT=0
		DECLARE @ID_Contact BIGINT 

		IF OBJECT_ID ('tmp_Contact_job_result', 'U') IS NOT NULL
			DROP TABLE dbo.tmp_Contact_job_result;

		SET @TABLE_NAME = 'TMP_CONTACT_JOB_' + CAST(@JOB_ID AS VARCHAR)
		--[02] -- more values 
		--[03] -- ADDED ID 
		set @SQL = 'SELECT 
					NEWID() AS id,
					ACCOUNT_ID,
					FIRST_NAME,
					LAST_NAME,
					EMAIL,
					MOBILE,
					TITLE,
					SUBSCRIBEDTOEMAIL,
					CONTACTTYPE,
					CONTACTSTATUS,
					LEADSTATUS,
					GENDER,
					COMPANY_NAME,
					WEBSITE,
					POSITION_TITLE,
					PHONE_NO,
					ADDRESS1,
					ADDRESS2,
					CITY,
					STATE,
					COUNTRYID,
					POSTALCODE,
					SUBSCRIBEDTOEMAILVIA,
					CREATEDBY,
					SUBSCRIBEDTOIPADDRESS,
					FORMID,
					CONTACTLISTID,
					FILTERID
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
		BEGIN TRY  
		   --[03] USE LOOP INSTEAD OF CURSOR
				WHILE EXISTS(SELECT TOP 1 1 FROM tmp_Contact_job_result)
				BEGIN
					select top 1 @UID = id from tmp_Contact_job_result
					SELECT @AccountID = ACCOUNT_ID, 
						@FirstName = FIRST_NAME, 
						@LastName = LAST_NAME, 
						@EmailAddress = EMAIL, 
						@MobileNumber = MOBILE,  
						@Title = TITLE, 
						@SubscribedToEmail=SUBSCRIBEDTOEMAIL,
						@ContactType=CONTACTTYPE,
						@ContactStatus=CONTACTSTATUS,
						@LeadStatus=LEADSTATUS,
						@Gender = GENDER, 
						@CompanyName=COMPANY_NAME,
						@WebSite = WEBSITE,
						@Position = POSITION_TITLE,
						@PhoneNumber = PHONE_NO,  
						@Address1 = ADDRESS1,
						@Address2 = ADDRESS2,
						@City = CITY,
						@State=[STATE],
						@CountryId=COUNTRYID,
						@ZipCode = POSTALCODE,
						@SubscribedToEmailVia=SUBSCRIBEDTOEMAILVIA,
						@CreatedBy=CREATEDBY,
						@SubscribedToEmailIPAddress=SUBSCRIBEDTOIPADDRESS,
						@FormId=FORMID,
						@ContactListID=CONTACTLISTID,
						@FilterID=FILTERID
							from tmp_Contact_job_result where  id = @UID
	
						insert into #res
						exec S_CON_CONTACT_INSERT
								@AccountID,
								@FirstName,
								@LastName,
								@EmailAddress,
								@MobileNumber,
								@SubscribedToEmail, 
								@ContactType,
								@ContactStatus, 
								@LeadStatus,
								@Gender, 
								@Title,
								@CompanyName,
								@WebSite,
								@Position,
								@PhoneNumber,
								@Address1,
								@Address2, 
								@City,
								@State,
								@CountryId, 
								@ZipCode,
								@SubscribedToEmailVia,
								@CreatedBy,
								@SubscribedToEmailIPAddress,
								@FormId,
								@ContactListId,
								@FilterId,
								@PreventDuplicateEmail,
								@ID_Contact OUTPUT
   
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
		END TRY 
		BEGIN CATCH  
				ROLLBACK
		END CATCH
		
	
END


