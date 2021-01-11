USE [DB_A6C9C7_MarketingAutoDB]
GO

ALTER TABLE [dbo].[Contact] DROP CONSTRAINT [df_CreatedDate]
GO



ALTER TABLE [dbo].[Contact] ADD  CONSTRAINT [df_CreatedDate]  DEFAULT (GETUTCDATE()) FOR [CreatedDate]
GO





