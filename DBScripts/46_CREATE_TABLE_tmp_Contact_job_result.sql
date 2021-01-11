USE MarketingAutomationTool
GO

/****** Object:  Table [dbo].[tmp_Contact_job_result]    Script Date: 2/29/2020 2:56:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE [dbo].[tmp_Contact_job_result](
	[id] [uniqueidentifier] NULL,
	[ACCOUNT_ID] UNIQUEIDENTIFIER NOT NULL,
	[FIRST_NAME] [nvarchar](100) NULL,
	[LAST_NAME] [nvarchar](100) NOT NULL,
	[EMAIL] [nvarchar](256) NOT NULL,
	[MOBILE] [varchar](50) NULL,
	[TITLE] [nvarchar](10) NULL,
	[COMPANY_NAME] [nvarchar](100) NULL,
	[WEBSITE] [nvarchar](250) NULL,
	[POSITION_TITLE] [nvarchar](50) NULL,
	[PHONE_NO] [varchar](50) NULL,
	[ADDRESS] [nvarchar](100) NULL,
	[GENDER] [char](1) NULL,
	[POSTALCODE] [nvarchar](20) NULL
) ON [PRIMARY]
GO


