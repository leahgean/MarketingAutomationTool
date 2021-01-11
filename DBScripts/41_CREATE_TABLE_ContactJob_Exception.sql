USE MarketingAutomationTool

/****** Object:  Table [dbo].[CON_CONTACT_JOB_EXCEPTION]    Script Date: 2/26/2020 1:38:39 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ContactJob_Exception](
	[JE_ID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[JOB_ID] [int] NOT NULL REFERENCES ContactJob(JobId),
	[REC_NO] [int] NULL,
	[ERR_CODE] [varchar](50) NULL,
	[DESCRIPTION] [varchar](500) NULL,
	[DATE_CREATED] [smalldatetime] NOT NULL DEFAULT (GETDATE())
) 

