USE [MarketingAutomationTool]
GO

CREATE PROCEDURE [dbo].[S_ContactList_Update]
@AccountID UNIQUEIDENTIFIER,
@ListName NVARCHAR(100),
@ListDescription NVARCHAR(500),
@ModifiedBy UNIQUEIDENTIFIER,
@Id INT
AS
BEGIN

UPDATE ContactList
SET ListName = @ListName
    ,ListDescription=@ListDescription
    ,ModifiedDate=GETDATE()
    ,ModifiedBy=@ModifiedBy
WHERE Id = @Id AND AccountId = @AccountID
           
END