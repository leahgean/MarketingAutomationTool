ALTER PROCEDURE [dbo].[S_ContactList_Get]
@AccountID UNIQUEIDENTIFIER,
@Id INT
AS
BEGIN

SELECT 
	ID,
	AccountID, 
	ListName,
	ListDescription,
	DATEADD(HH,8,DateCreated) DateCreated,
	CreatedBy,
	DATEADD(HH,8,ModifiedDate) ModifiedDate,
	ModifiedBy
FROM ContactList WITH (NOLOCK)
WHERE Id = @Id AND AccountId = @AccountID
           
END