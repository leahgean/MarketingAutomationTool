USE [DB_A6C9C7_MarketingAutoDB]
GO

ALTER TABLE [dbo].[Template] DROP CONSTRAINT [DF__Template__Create__0AD36B5C]
GO

ALTER TABLE [dbo].[Template] ADD  DEFAULT (getutcdate()) FOR [CreatedDate]
GO


