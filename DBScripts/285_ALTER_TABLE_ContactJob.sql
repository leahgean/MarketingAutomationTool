USE [DB_A6C9C7_MarketingAutoDB]
GO

ALTER TABLE [dbo].[ContactJob] DROP CONSTRAINT [DF__ContactJo__DateC__58BC2184]
GO



ALTER TABLE [dbo].[ContactJob] ADD  DEFAULT (GETUTCDATE()) FOR [DateCreated]
GO



