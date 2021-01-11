USE [DB_A6C9C7_MarketingAutoDB]
GO

ALTER TABLE [dbo].[Message] DROP CONSTRAINT [DF__Message__Created__1368B15D]
GO

ALTER TABLE [dbo].[Message] ADD  DEFAULT (GETUTCDATE()) FOR [CreatedDate]
GO


