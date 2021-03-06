ALTER PROCEDURE [dbo].[S_USER_CREATE]
@AccountID UNIQUEIDENTIFIER,
@UserName NVARCHAR(50),
@Password NVARCHAR(128),
@FirstName NVARCHAR(100),
@MiddleName NVARCHAR(100),
@LastName NVARCHAR(100),
@MobileNumber VARCHAR(100),
@PhoneNumber VARCHAR(100),
@EmailAddress NVARCHAR(250),
@Address NVARCHAR(250),
@City NVARCHAR(100),
@State NVARCHAR(100), 
@ZipCode NVARCHAR(20),
@CountryId INT,
@IsAdminUser BIT,
@IsActiveUser BIT,
@IsOwnerUser BIT,
@IsSuperAdminUser BIT,
@PasswordSalt NVARCHAR(128),
@CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
	
	DECLARE @UserID AS UNIQUEIDENTIFIER
	SELECT @UserID = NEWID()
	
	--PRINT 'User'
	INSERT INTO [User](AccountID,UserID, UserName,[Password],FirstName,MiddleName,LastName,MobileNumber,PhoneNumber,EmailAddress,[Address],City,[State],ZipCode,CountryId,PasswordQuestion,PasswordAnswer,LastLoginDate,LastLogoutDate,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsAdmin,IsActive,PasswordSalt, IsOwner, IsSuperAdminUser)
	VALUES(@AccountID, @UserID, @UserName, @Password, @FirstName, @MiddleName, @LastName, @MobileNumber, @PhoneNumber, @EmailAddress,@Address, @City,@State,@ZipCode, @CountryId, NULL, NULL,NULL,NULL,GETUTCDATE(),@CreatedBy, GETUTCDATE(),NULL,@IsAdminUser,@IsActiveUser,@PasswordSalt, @IsOwnerUser, @IsSuperAdminUser)


END