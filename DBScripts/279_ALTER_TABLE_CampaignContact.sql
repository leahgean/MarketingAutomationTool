USE [DB_A6C9C7_MarketingAutoDB]
GO

ALTER TABLE [dbo].[CampaignContact] DROP CONSTRAINT [DF__CampaignC__Creat__34C9A528]
GO


ALTER TABLE [dbo].[CampaignContact] ADD  DEFAULT (GETUTCDATE()) FOR [CreatedDate]
GO


