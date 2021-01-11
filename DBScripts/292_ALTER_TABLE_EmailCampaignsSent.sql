USE [DB_A6C9C7_MarketingAutoDB]
GO

ALTER TABLE [dbo].[EmailCampaignsSent] DROP CONSTRAINT [DF__EmailCamp__Creat__562A98F3]
GO

ALTER TABLE [dbo].[EmailCampaignsSent] ADD  DEFAULT (GETUTCDATE()) FOR [CreatedDate]
GO


