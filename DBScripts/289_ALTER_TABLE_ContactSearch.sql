USE [DB_A6C9C7_MarketingAutoDB]
GO

ALTER TABLE [dbo].[ContactSearch] DROP CONSTRAINT [DF__ContactSe__CREAT__188C8DD6]
GO


ALTER TABLE [dbo].[ContactSearch] ADD  DEFAULT (GETUTCDATE()) FOR [CREATEDDATE]
GO
