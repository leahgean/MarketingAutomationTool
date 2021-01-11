USE [DB_A6C9C7_MarketingAutoDB]
GO

ALTER TABLE [dbo].[CampaignClickThroughs] DROP CONSTRAINT [DF__CampaignC__Creat__7E388A4D]
GO



ALTER TABLE [dbo].[CampaignClickThroughs] ADD  DEFAULT (GETUTCDATE()) FOR [CreatedDate]
GO

