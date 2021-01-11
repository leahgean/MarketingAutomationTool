USE [DB_A6C9C7_MarketingAutoDB]
GO

ALTER TABLE [dbo].[ContactListContacts] DROP CONSTRAINT [DF__ContactLi__DateC__71F1E3A2]
GO


ALTER TABLE [dbo].[ContactListContacts] ADD  DEFAULT (GETUTCDATE()) FOR [DateCreated]
GO

