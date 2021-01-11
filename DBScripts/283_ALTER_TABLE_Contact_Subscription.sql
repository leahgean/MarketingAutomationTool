USE [DB_A6C9C7_MarketingAutoDB]
GO

ALTER TABLE [dbo].[Contact_Subscription] DROP CONSTRAINT [DF__Contact_S__DATE___7CF981FA]
GO


ALTER TABLE [dbo].[Contact_Subscription] ADD  DEFAULT (GETUTCDATE()) FOR [DATE_CREATED]
GO


