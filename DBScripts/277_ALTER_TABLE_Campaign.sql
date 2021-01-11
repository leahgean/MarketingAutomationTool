USE [DB_A6C9C7_MarketingAutoDB]
GO

ALTER TABLE [dbo].[Campaign] DROP CONSTRAINT [DF__Campaign__Create__1FCE8842]
GO

ALTER TABLE [dbo].[Campaign] ADD  DEFAULT (GETUTCDATE()) FOR [CreatedDate]
GO


