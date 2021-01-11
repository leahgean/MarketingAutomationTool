USE [DB_A6C9C7_MarketingAutoDB]
GO

ALTER TABLE [dbo].[Contact_Subscription_History] DROP CONSTRAINT [DF__Contact_S__ACTIO__086B34A6]
GO


ALTER TABLE [dbo].[Contact_Subscription_History] ADD  DEFAULT (GETUTCDATE()) FOR [ACTION_TIME]
GO
