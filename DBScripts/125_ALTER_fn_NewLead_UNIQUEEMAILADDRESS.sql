ALTER FUNCTION [dbo].[fn_NewLead_UNIQUEEMAILADDRESS]
(
@EmailAddress NVARCHAR(250),
@AccountId UNIQUEIDENTIFIER,
@ContactID UNIQUEIDENTIFIER
)
RETURNS BIT
AS
BEGIN

	DECLARE @Contact_EmailCount INT = 0
	DECLARE @EmailIsUnique BIT = NULL
	
	IF (@ContactID IS NULL)
	BEGIN
		SELECT @Contact_EmailCount=COUNT(*) FROM Contact WHERE EmailAddress = @EmailAddress AND AccountId = @AccountId AND IsDeleted=0

		IF (@Contact_EmailCount = 0)
		BEGIN
			SET @EmailIsUnique = 1
		END
		ELSE
		BEGIN
			SET @EmailIsUnique= 0
		END
	END
	ELSE
	BEGIN
		SELECT @Contact_EmailCount=COUNT(*) FROM Contact WHERE EmailAddress = @EmailAddress AND AccountId = @AccountId AND ContactID <> @ContactID AND IsDeleted=0

		IF (@Contact_EmailCount = 0)
		BEGIN
			SET @EmailIsUnique = 1
		END
		ELSE
		BEGIN
			SET @EmailIsUnique= 0
		END
	END

	RETURN @EmailIsUnique
END