ALTER PROCEDURE [dbo].[S_CREATE_ACCOUNT]
@AccountName NVARCHAR(50),
@CompanyWebsite VARCHAR(400),
@CompanyPhone NVARCHAR(100),
@FaxNo VARCHAR(100),
@CompanyEmail NVARCHAR(250),
@Address NVARCHAR(250),
@City NVARCHAR(100),
@State NVARCHAR(100), 
@ZipCode NVARCHAR(20),
@CountryId INT,
@UserName NVARCHAR(50),
@Password NVARCHAR(128),
@FirstName NVARCHAR(100),
@MiddleName NVARCHAR(100),
@LastName NVARCHAR(100),
@MobileNumber VARCHAR(100),
@PhoneNumber VARCHAR(100),
@EmailAddress NVARCHAR(250),
@IsAdminUser BIT,
@IsActiveUser BIT,
@IsOwnerUser BIT,
@IsSuperAdminUser BIT,
@PasswordSalt NVARCHAR(128),
@CreatedBy UNIQUEIDENTIFIER,
@IsActiveAccount BIT,
@IsSuperAdminAccount BIT,
@CreatedFromIP VARCHAR(15)
AS
BEGIN
    DECLARE @AccountID AS UNIQUEIDENTIFIER
	DECLARE @UserID AS UNIQUEIDENTIFIER

	SELECT  @AccountID = NEWID()
	SELECT @UserID = NEWID()

	PRINT 'Account'
	INSERT INTO Account(AccountID, AccountName,CompanyWebsite,CompanyPhone,FaxNo,CompanyEmail,[Address],City,[State],ZipCode,CountryId,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive, IsSuperAdmin, CreatedFromIP)
	VALUES(@AccountID, @AccountName,@CompanyWebsite,@CompanyPhone,@FaxNo,@CompanyEmail,@Address,@City,@State, @ZipCode,@CountryId,GETUTCDATE(),@CreatedBy,NULL,NULL,@IsActiveAccount,@IsSuperAdminAccount, @CreatedFromIP)

	PRINT 'User'
	INSERT INTO [User](AccountID,UserID, UserName,[Password],FirstName,MiddleName,LastName,MobileNumber,PhoneNumber,EmailAddress,[Address],City,[State],ZipCode,CountryId,PasswordQuestion,PasswordAnswer,LastLoginDate,LastLogoutDate,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsAdmin,IsActive,PasswordSalt, IsOwner, IsSuperAdminUser)
	VALUES(@AccountID, @UserID, @UserName, @Password, @FirstName, @MiddleName, @LastName, @MobileNumber, @PhoneNumber, @EmailAddress,@Address, @City,@State,@ZipCode, @CountryId, NULL, NULL,NULL,NULL,GETUTCDATE(),@CreatedBy, NULL,NULL,@IsAdminUser,@IsActiveUser,@PasswordSalt, @IsOwnerUser, @IsSuperAdminUser)

	PRINT 'Account_Status_History'
	INSERT INTO Account_Status_History(AccountID,[Status],[DateChanged],[IP],[ChangedBy])
     VALUES(@AccountID,'ACT',GETUTCDATE(), @CreatedFromIP,@CreatedBy)




END
