USE [DB_A6C9C7_MarketingAutoDB]
GO

ALTER TABLE [dbo].[ContactList] DROP CONSTRAINT [DF__ContactLi__DateC__658C0CBD]
GO


ALTER TABLE [dbo].[ContactList] ADD  DEFAULT (GETUTCDATE()) FOR [DateCreated]
GO


