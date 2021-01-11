USE [DB_A6C9C7_MarketingAutoDB]
GO

ALTER TABLE [dbo].[ContactSearchExportFiles] DROP CONSTRAINT [DF__ContactSe__Creat__783FB9D5]
GO

ALTER TABLE [dbo].[ContactSearchExportFiles] ADD  DEFAULT (GETUTCDATE()) FOR [CreatedDate]
GO


