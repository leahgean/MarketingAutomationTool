ALTER PROCEDURE [dbo].[S_USERDETAILS_UPDATE]
@AccountId UNIQUEIDENTIFIER,
@UserID UNIQUEIDENTIFIER,
@FirstName NVARCHAR(100),
@LastName NVARCHAR(100),
@Position  NVARCHAR(128),
@EmailAddress NVARCHAR(250),
@Mobile VARCHAR(100),
@Enabled BIT,
@ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN

	DECLARE @IsAdmin AS BIT = 0
	DECLARE @IsOwner AS BIT = 0

	UPDATE [User] 
	SET FirstName=@FirstName, 
	LastName = @LastName, 
	Position=@Position, 
	EmailAddress=@EmailAddress, 
	MobileNumber=@Mobile,
	IsActive=@Enabled,
	ModifiedDate=GETUTCDATE(), 
	ModifiedBy=@ModifiedBy  
	WHERE AccountID=@AccountId AND UserID=@UserID

	SELECT @IsAdmin=IsAdmin, @IsOwner=IsOwner
	FROM [User]
	WHERE AccountID=@AccountId AND UserID=@UserID

	IF (@IsAdmin = 1 AND @IsOwner= 1)
	BEGIN
		UPDATE Account
		SET CompanyEmail = @EmailAddress
		WHERE AccountID = @AccountID
	END
END