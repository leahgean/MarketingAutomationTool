CREATE FUNCTION [dbo].[fn_NewList_UniqueListName]
(
@ListName NVARCHAR(100),
@AccountId UNIQUEIDENTIFIER,
@ContactListId INT
)
RETURNS BIT
AS
BEGIN

	DECLARE @ListNameCount INT = 0
	DECLARE @ListNameIsUnique BIT = NULL
	
	
	IF (@ContactListId IS NULL)
	BEGIN
		SELECT @ListNameCount=COUNT(*) FROM ContactList WHERE ListName = @ListName AND AccountId = @AccountId

		IF (@ListNameCount = 0)
		BEGIN
			SET @ListNameIsUnique = 1
		END
		ELSE
		BEGIN
			SET @ListNameIsUnique= 0
		END
	END
	ELSE
	BEGIN
		SELECT @ListNameCount=COUNT(*) FROM ContactList WHERE ListName = @ListName AND AccountId = @AccountId AND Id <> @ContactListId

		IF (@ListNameCount = 0)
		BEGIN
			SET @ListNameIsUnique = 1
		END
		ELSE
		BEGIN
			SET @ListNameIsUnique= 0
		END
	END
	

	RETURN @ListNameIsUnique
END