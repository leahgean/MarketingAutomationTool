CREATE PROCEDURE S_GET_LISTNAMES
@AccountID UNIQUEIDENTIFIER,
@IsDeleted BIT
AS
BEGIN

SELECT ID, ListName
FROM ContactList WITH (NOLOCK)
WHERE AccountID = @AccountID AND
IsDeleted = @IsDeleted

END