USE [DB_A6C9C7_MarketingAutoDB]
GO

ALTER TABLE [dbo].[ContactSearchFields] DROP CONSTRAINT [DF__ContactSe__CREAT__230A1C49]
GO

ALTER TABLE [dbo].[ContactSearchFields] ADD  DEFAULT (getutcdate()) FOR [CREATEDDATE]
GO


