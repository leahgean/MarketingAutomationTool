USE [DB_A6C9C7_MarketingAutoDB]
GO

ALTER TABLE [dbo].[Sys_Log] DROP CONSTRAINT [DF__Sys_Log__Date_Cr__5E3FF0B0]
GO

ALTER TABLE [dbo].[Sys_Log] ADD  DEFAULT (getutcdate()) FOR [Date_Created]
GO


