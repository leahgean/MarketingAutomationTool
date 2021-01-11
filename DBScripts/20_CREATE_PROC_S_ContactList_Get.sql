USE [MarketingAutomationTool]
GO

CREATE PROCEDURE [dbo].[S_ContactList_Get]
@AccountID UNIQUEIDENTIFIER,
@Id INT
AS
BEGIN

SELECT 
	ID,
	AccountID, 
	ListName,
	ListDescription,
	DateCreated,
	CreatedBy,
	ModifiedDate,
	ModifiedBy
FROM ContactList WITH (NOLOCK)
WHERE Id = @Id AND AccountId = @AccountID
           
END