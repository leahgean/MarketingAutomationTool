CREATE PROCEDURE S_ContactList_Delete
@ID INT,
@AccountID UNIQUEIDENTIFIER,
@ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
UPDATE ContactList
SET IsDeleted=1, DateDeleted=GETDATE(), ModifiedBy=@ModifiedBy
WHERE AccountID=@AccountID AND ID=@ID
END


