ALTER PROCEDURE [dbo].[S_ContactSearch_Update]
@AccountId UNIQUEIDENTIFIER,
@ModifiedBy UNIQUEIDENTIFIER,
@SearchJsonString NVARCHAR(MAX),
@Id INT
AS
BEGIN

	UPDATE ContactSearch
	SET SearchJsonString=@SearchJsonString,
		MODIFIEDBY=@ModifiedBy,
		MODIFIEDDATE=GETUTCDATE()
	WHERE Id=@Id AND AccountId=@AccountId

END