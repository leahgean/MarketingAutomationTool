USE MarketingAutomationTool
GO

/****** Object:  Table [dbo].[HTML_TEMPLATE]    Script Date: 3/2/2020 11:52:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HTML_TEMPLATE](
	[HTML_TEMPLATE_ID] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ACCOUNT_ID] UNIQUEIDENTIFIER NULL REFERENCES Account(AccountID),
	[USER_ID] [uniqueidentifier] NOT NULL REFERENCES [User](UserID),
	[TEMPLATE_BODY] [nvarchar](max) NULL,
	[DATE_CREATED] [datetime] NULL DEFAULT (getdate()),
	[POSITION] [nvarchar](10) NULL,
	[DATE_DELETED] [datetime] NULL,
	[DELETE_USER_ID] [uniqueidentifier] NULL,
)

