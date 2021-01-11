USE [DB_A6C9C7_MarketingAutoDB]
GO

ALTER TABLE [dbo].[CampaignUnsubscribes] DROP CONSTRAINT [DF__CampaignU__Creat__03F163A3]
GO


ALTER TABLE [dbo].[CampaignUnsubscribes] ADD  DEFAULT (GETUTCDATE()) FOR [CreatedDate]
GO

