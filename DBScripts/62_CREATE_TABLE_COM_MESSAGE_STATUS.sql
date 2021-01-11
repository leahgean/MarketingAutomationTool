USE MarketingAutomationTool
GO

/****** Object:  Table [dbo].[COM_MESSAGE_STATUS]    Script Date: 3/2/2020 11:50:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[COM_MESSAGE_STATUS](
	[STATUS_ID] [tinyint] NOT NULL PRIMARY KEY,
	[STATUS_NAME] [nvarchar](50) NULL
)

