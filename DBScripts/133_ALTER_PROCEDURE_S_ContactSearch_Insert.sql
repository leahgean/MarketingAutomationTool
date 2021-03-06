ALTER PROCEDURE [dbo].[S_ContactSearch_Insert]
@AccountId UNIQUEIDENTIFIER,
@CreatedBy UNIQUEIDENTIFIER,
@SearchJsonString NVARCHAR(MAX),
@Id INT OUTPUT,
@SearchUID UNIQUEIDENTIFIER OUTPUT
AS
BEGIN

INSERT INTO ContactSearch(ACCOUNTID, CREATEDBY, SearchJsonString)
VALUES(@AccountId,@CreatedBy, @SearchJsonString)

SELECT @Id= SCOPE_IDENTITY()

SELECT @SearchUID=SearchUID FROM ContactSearch WHERE Id=@Id

END