USE [DB_A6C9C7_MarketingAutoDB]
GO

ALTER TABLE [dbo].[CampaignsOpened] DROP CONSTRAINT [DF__Campaigns__Creat__2CF37936]
GO

ALTER TABLE [dbo].[CampaignsOpened] ADD  DEFAULT (GETUTCDATE()) FOR [CreatedDate]
GO

