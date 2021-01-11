CREATE PROCEDURE S_LIST_GET
@AccountID UNIQUEIDENTIFIER,
@IsDeleted BIT
AS
BEGIN

SELECT [ID]
      ,[AccountID]
      ,[ListName]
      ,[ListDescription]
      ,[DateCreated]
      ,[CreatedBy]
      ,[ModifiedDate]
      ,[ModifiedBy]
      ,[DateDeleted]
      ,[IsDeleted]
  FROM [dbo].[ContactList]
WHERE AccountId = @AccountID AND IsDeleted=@IsDeleted

END




