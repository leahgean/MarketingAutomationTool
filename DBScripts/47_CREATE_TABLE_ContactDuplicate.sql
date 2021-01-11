USE MarketingAutomationTool
GO

CREATE TABLE [dbo].[ContactDuplicate]
(
	[ContactDuplicateID] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ContactID] BIGINT NOT NULL,
	[ExistingContactID] BIGINT NOT NULL
)

