CREATE PROCEDURE S_ContactList_Insert
@AccountID UNIQUEIDENTIFIER,
@ListName NVARCHAR(100),
@ListDescription NVARCHAR(500),
@CreatedBy UNIQUEIDENTIFIER,
@Id INT OUTPUT
AS
BEGIN

INSERT INTO [dbo].[ContactList]
           ([AccountID]
           ,[ListName]
           ,[ListDescription]
           ,[DateCreated]
           ,[CreatedBy]
           ,[ModifiedDate]
           ,[ModifiedBy])
     VALUES
           (@AccountID
           ,@ListName
           ,@ListDescription
           ,GETDATE()
           ,@CreatedBy
           ,NULL
           ,NULL)

		   SELECT @Id=SCOPE_IDENTITY()
END


