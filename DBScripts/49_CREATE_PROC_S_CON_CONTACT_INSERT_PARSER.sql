-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: 29-Feb-2020
-- Description:	INSERT RECORD INTO CON_CONTACT, CON_CONTACT_details
-- =============================================
CREATE PROCEDURE [dbo].[S_CON_CONTACT_INSERT_PARSER] 
	@ACCOUNT_ID	UNIQUEIDENTIFIER,
	@FIRST_NAME	NVARCHAR(100),
	@LAST_NAME	NVARCHAR(100),
	@EMAIL	NVARCHAR(250),
	@MOBILE	VARCHAR(100),
	@SUBSCRIBEDTOEMAIL	bit,
	@SUBSCRIBED_VIA VARCHAR(50),
	@SUBSCRIBED_BY uniqueidentifier,
	@SUBSCRIBE_IP_ADDRESS VARCHAR(15),
	@GROUP_ID int,
	@FILTER_ID bigint,
	@PREVENT_DUPLICATEE_MAIL BIT = NULL,
	@ID BIGINT output
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Err AS INT
	DECLARE @Msg AS VARCHAR(400)	
	DECLARE @sql VARCHAR(MAX)
	DECLARE @DUPLICATE_ID BIGINT 
	DECLARE @CONTACT_ID UNIQUEIDENTIFIER

	-- FINDING FIRST EXISTING CONTACT WITH SAME EMAIL ADDRESS
	SELECT TOP 1 @DUPLICATE_ID  = ID  
	FROM Contact WHERE EmailAddress= @EMAIL AND AccountID = @ACCOUNT_ID
	
	IF ( NOT @DUPLICATE_ID  IS NULL)
	BEGIN
		IF(ISNULL(@PREVENT_DUPLICATEE_MAIL,1) = 1)
			BEGIN
				SELECT 'Record could not be added. A contact with this email address already exists.'		
				RETURN 
			END
	END
	
	BEGIN TRANSACTION
				
		INSERT INTO Contact(AccountID, FirstName, LastName, EmailAddress, MobileNumber, SubscribedToEmail)
		VALUES(@ACCOUNT_ID, @FIRST_NAME, @LAST_NAME, @EMAIL, @MOBILE, @SUBSCRIBEDTOEMAIL)	
		
		
		SET @Err=@@error
		IF @err <> 0 begin
			SET @Msg='Contact could not be added.'
			ROLLBACK TRANSACTION
			GOTO Error
		END
	
		SET @ID = SCOPE_IDENTITY()
		SELECT @CONTACT_ID = ContactID FROM Contact WITH (NOLOCK) WHERE Id=@Id

		-- LOGGING DUPLICATE CONTACT ID	
		IF(@PREVENT_DUPLICATEE_MAIL = 0 AND NOT @DUPLICATE_ID  IS NULL)
			BEGIN
				INSERT INTO ContactDuplicate (ContactID,ExistingContactID) VALUES(@ID,@DUPLICATE_ID )
			END
		
		
		INSERT INTO Contact_Subscription (CONTACT_ID,DATE_CREATED,LAST_UPDATED,SUBSCRIBED_VIA,SUBSCRIBED_BY,SUBSCRIBE_IP_ADDRESS)
		VALUES(@CONTACT_ID,GETDATE(),GETDATE(),@SUBSCRIBED_VIA,@SUBSCRIBED_BY,@SUBSCRIBE_IP_ADDRESS)
		
		
		INSERT INTO Contact_Subscription_History(CONTACT_ID,EMAIL_ADDRESS,ACTION_ID,ACTION_TIME,ACTION_BY,ACTION_VIA,IP_ADDRESS,MESSAGE_ID)
		VALUES(@CONTACT_ID,@EMAIL,1,GETDATE(),@SUBSCRIBED_BY,@SUBSCRIBED_VIA,@SUBSCRIBE_IP_ADDRESS,NULL)
				
		SET @Err=@@error
		IF @err <> 0 
		BEGIN
			SET @Msg='Contact could not be added.'
			ROLLBACK TRANSACTION
			GOTO Error
		END
		--if group id is not null add assign this CON_CONTACT to the group
	
		IF @GROUP_ID IS NOT NULL AND @GROUP_ID != 0
		BEGIN
			INSERT INTO ContactListContacts(ContactListID,ContactID,CreatedBy)
			VALUES(@GROUP_ID, @CONTACT_ID, @SUBSCRIBED_BY)
		END

		SET @Err=@@error
		IF @err <> 0 BEGIN
			SET @Msg='Contact could not be added.'
			ROLLBACK TRANSACTION
			GOTO Error
		END

		-- If Filter_Id is not 0 add this contact to filter list
		IF(@FILTER_ID != 0)
		BEGIN
			DECLARE @Table_Name VARCHAR(256)
			SELECT @Table_Name = dbo.fnGenerateContactListTableName('[ContactList' , @FILTER_ID )+ ']'
			SET @sql = 'INSERT INTO ' + @Table_Name 
			SET @sql = @sql + ' VALUES(' + CAST(@ID AS VARCHAR) + ',0)'
			EXEC(@sql)
		END

		COMMIT TRANSACTION	
	
		SELECT ''
		RETURN

		ERROR:
			RAISERROR(@Msg,16,1)
END

/*
DECLARE @ID INT
EXEC S_CON_CONTACT_INSERT 'CCF4D547-C9EC-42B4-97DF-B12752734323','Vaibhav', 'Patel','vaibhav@selectbytes.com','12345',0,'Website_AddNew','708AF094-B4B6-4A5C-AC8D-037DA1EA9966',null,1, @ID OUTPUT
SELECT @ID

declare @p11 bigint
set @p11=11
exec S_CON_CONTACT_INSERT @ACCOUNT_ID=8310,@FIRST_NAME='Vaibhav',@LAST_NAME='Patel',@EMAIL='vaibhav@selectbytes.com',@MOBILE='12345',@SUBSCRIBEDTOEMAIL=0,@SUBSCRIBED_VIA=NULL,@SUBSCRIBED_BY='7CA8B012-3A03-4D0B-97B4-9785F6EAF9BF',@NOTE=NULL,@GROUP_ID='2',@CON_CONTACT_ID=@p11 output
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


select * from Account
select * from Contact WITH (NOLOCK) WHERE AccountID='CCF4D547-C9EC-42B4-97DF-B12752734323'
