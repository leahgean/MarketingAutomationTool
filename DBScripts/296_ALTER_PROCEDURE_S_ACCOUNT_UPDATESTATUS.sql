ALTER PROCEDURE [dbo].[S_ACCOUNT_UPDATESTATUS]
@Status CHAR(3),
@IP VARCHAR(15),
@AccountID UNIQUEIDENTIFIER,
@ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @StatusBit BIT
	DECLARE @IsActive_Account BIT

	SELECT @IsActive_Account = IsActive FROM Account WHERE AccountID = @AccountID

	IF (@Status = 'ACT')
		SET @StatusBit = 1
	ELSE
		SET @StatusBit = 0

	IF (@IsActive_Account <> @StatusBit)
	BEGIN
		UPDATE Account 
		SET IsActive = @StatusBit,
			ModifiedBy = @ModifiedBy,
			ModifiedDate = GETUTCDATE()
		WHERE AccountID = @AccountID

		INSERT INTO Account_Status_History(AccountID,[Status],DateChanged,[IP],[ChangedBy])
		VALUES(@AccountID,@Status, GETUTCDATE(),@IP, @ModifiedBy)
	END
END