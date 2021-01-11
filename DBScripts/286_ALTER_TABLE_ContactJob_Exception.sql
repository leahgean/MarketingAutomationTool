USE [DB_A6C9C7_MarketingAutoDB]
GO

ALTER TABLE [dbo].[ContactJob_Exception] DROP CONSTRAINT [DF__ContactJo__DATE___5E74FADA]
GO



ALTER TABLE [dbo].[ContactJob_Exception] ADD  DEFAULT (GETUTCDATE()) FOR [DATE_CREATED]
GO

