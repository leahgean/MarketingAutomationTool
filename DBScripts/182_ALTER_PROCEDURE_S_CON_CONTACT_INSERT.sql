-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: 09-Mar-2020
-- Description:	INSERT CONTACT
-- =============================================
ALTER PROCEDURE [dbo].[S_CON_CONTACT_INSERT]
	@AccountID UNIQUEIDENTIFIER,
	@FirstName NVARCHAR(100),
	@LastName NVARCHAR(100),
	@EmailAddress NVARCHAR(250),
	@MobileNumber VARCHAR(100),
	@SubscribedToEmail BIT, 
	@ContactType INT,
	@ContactStatus INT, 
	@LeadStatus  INT,
	@Gender CHAR(1), 
	@Title NVARCHAR(50),
	@CompanyName NVARCHAR(100),
	@WebSite NVARCHAR(250),
	@Position NVARCHAR(250),
	@PhoneNumber VARCHAR(100),
	@Address1 NVARCHAR(250),
	@Address2 NVARCHAR(250), 
	@City NVARCHAR(50),
	@State NVARCHAR(50),
	@CountryId INT, 
	@ZipCode NVARCHAR(20),
	@SubscribedToEmailVia VARCHAR(50),
	@CreatedBy UNIQUEIDENTIFIER,
	@SubscribedToEmailIPAddress VARCHAR(15),
	@FormId	INT,
	@ContactListId INT,
	@FilterId BIGINT,
	@PreventDuplicateEmail BIT = NULL,
	@ID BIGINT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Err AS INT
	DECLARE @Msg AS VARCHAR(400)	
	DECLARE @sql VARCHAR(MAX)
	DECLARE @DuplicateID BIGINT 
	DECLARE @ContactID UNIQUEIDENTIFIER

	-- FINDING FIRST EXISTING CONTACT WITH SAME EMAIL ADDRESS
	SELECT TOP 1 @DuplicateID  = ID  
	FROM Contact WHERE EmailAddress= @EmailAddress AND AccountID = @AccountID
	
	IF ( NOT @DuplicateID  IS NULL)
	BEGIN
		IF(ISNULL(@PreventDuplicateEmail,1) = 1)
			BEGIN
				SELECT 'Record could not be added. A contact with this email address already exists.'		
				RETURN 
			END
	END
	
	BEGIN TRANSACTION

		IF (@MobileNumber='')
			SET @MobileNumber = NULL
		IF (@Gender='')
			SET @Gender = NULL
		IF (@Title='')
			SET @Title = NULL
		IF (@CompanyName='')
			SET @CompanyName = NULL
		IF (@WebSite='')
			SET @WebSite = NULL
		IF (@Position='')
			SET @Position = NULL
		IF (@PhoneNumber='')
			SET @PhoneNumber = NULL	
		IF (@Address1='')
			SET @Address1 = NULL
		IF (@Address2='')
			SET @Address2 = NULL
		IF (@City='')
			SET @City = NULL
		IF (@State='')
			SET @State = NULL
		IF (@ZipCode='')
			SET @ZipCode = NULL
		IF (@SubscribedToEmailIPAddress='')
			SET @SubscribedToEmailIPAddress= NULL
				
		INSERT INTO Contact(ContactID,AccountID,FirstName,LastName,EmailAddress,MobileNumber,SubscribedToEmail, ContactType,ContactStatus, LeadStatus,Gender, Title,CompanyName,WebSite,Position,PhoneNumber,Address1,Address2, City,[State],CountryId, ZipCode,CreatedBy, ImportDate,ImportSource,ImportBy )
		VALUES(NEWID(),@AccountID,@FirstName,@LastName,@EmailAddress,@MobileNumber,@SubscribedToEmail, @ContactType,@ContactStatus,@LeadStatus, @Gender, @Title,@CompanyName,@WebSite,@Position,@PhoneNumber,@Address1,@Address2, @City,@State,@CountryId, @ZipCode, @CreatedBy, GETDATE(),@SubscribedToEmailVia,@CreatedBy)
		
		
		SET @Err=@@error
		IF @err <> 0 begin
			SET @Msg='Contact could not be added.'
			ROLLBACK TRANSACTION
			GOTO Error
		END
	
		SET @ID = @@identity

		SELECT @ContactID =ContactID FROM Contact WHERE Id = @ID

		-- LOGGING DUPLICATE CONTACT ID	
		IF(@PreventDuplicateEmail = 0 AND NOT @DuplicateID  IS NULL)
			BEGIN
				INSERT INTO ContactDuplicate(ContactID,ExistingContactID) VALUES(@ID,@DuplicateID )
			END
		
		INSERT INTO Contact_Subscription(CONTACT_ID,DATE_CREATED,LAST_UPDATED,SUBSCRIBED_VIA,SUBSCRIBED_BY,SUBSCRIBE_IP_ADDRESS, FORM_ID)
		VALUES(@ContactID,GETDATE(),GETDATE(),@SubscribedToEmailVia,@CreatedBy,@SubscribedToEmailIPAddress, @FormId)
	
	
		INSERT INTO Contact_Subscription_History(CONTACT_ID,EMAIL_ADDRESS,ACTION_ID,ACTION_TIME,ACTION_BY,ACTION_VIA,IP_ADDRESS,MESSAGE_ID,COUNTRY_CODE)
		VALUES(@ContactID,@EmailAddress,1,GETDATE(),@CreatedBy,@SubscribedToEmailVia,@SubscribedToEmailIPAddress,NULL,@CountryId)
				
		SET @Err=@@error
		IF @err <> 0 
		BEGIN
			SET @Msg='Contact could not be added.'
			ROLLBACK TRANSACTION
			GOTO Error
		END
		--if group id is not null add assign this CON_CONTACT to the group
	
		IF @ContactListId IS NOT NULL AND @ContactListId != 0
		BEGIN
			INSERT INTO ContactListContacts(ContactListID,ContactID)
			VALUES(@ContactListId, @ContactID)
		END

		SET @Err=@@error
		IF @err <> 0 BEGIN
			SET @Msg='Contact could not be added.'
			ROLLBACK TRANSACTION
			GOTO Error
		END

		-- If Filter_Id is not 0 add this contact to filter list
		IF(@FilterId != 0)
		BEGIN
			DECLARE @Table_Name VARCHAR(256)
			SELECT @Table_Name = dbo.fnGenerateContactListTableName('[ContactList' , @FilterID )+ ']'
			SET @sql = 'INSERT INTO ' + @Table_Name 
			SET @sql = @sql + ' VALUES(' + CAST(@ContactID AS VARCHAR) + ',0)'
			EXEC(@sql)
		END

		COMMIT TRANSACTION	
	
		SELECT ''
		RETURN

		ERROR:
			RAISERROR(@Msg,16,1)
END

/*
DECLARE @CONTACT_ID INT
EXEC S_CON_CONTACT_INSERT 8310,'Vaibhav', 'Patel','vaibhav@selectbytes.com','12345',0,'Website_AddNew','7ca8b012-3a03-4d0b-97b4-9785f6eaf9bf',null,1
SELECT @CONTACT_ID

declare @p11 bigint
set @p11=11
exec S_CON_CONTACT_INSERT @ACCOUNT_ID=8310,@FIRST_NAME='Vaibhav',@LAST_NAME='Patel',@EMAIL='vaibhav@selectbytes.com',@MOBILE='12345',@UNSUBSCRIBED=0,@SUBSCRIBED_VIA=NULL,@SUBSCRIBED_BY='7CA8B012-3A03-4D0B-97B4-9785F6EAF9BF',@NOTE=NULL,@GROUP_ID='2',@CON_CONTACT_ID=@p11 output
select @p11

select * from CON_CONTACT
select * from CON_CONTACT_details
select * from group_CON_CONTACT
select * from groups
select * from usr_user

Delete from CON_CONTACT where CON_CONTACT_id=5
delete from CON_CONTACT_details
delete from group_CON_CONTACT  


select * from CON_CONTACT where Contact_Id=19
*/
