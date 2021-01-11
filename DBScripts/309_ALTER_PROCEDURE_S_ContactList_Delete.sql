ALTER PROCEDURE [dbo].[S_ContactList_Delete]
@ID INT,
@AccountID UNIQUEIDENTIFIER,
@ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
UPDATE ContactList
SET IsDeleted=1, DateDeleted=GETUTCDATE(), ModifiedBy=@ModifiedBy
WHERE AccountID=@AccountID AND ID=@ID
END


