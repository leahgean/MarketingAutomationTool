USE MarketingAutomationTool
GO

/****** Object:  Table [dbo].[COM_CHANNEL]    Script Date: 3/2/2020 11:48:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[COM_CHANNEL](
	[CHANNEL_ID] [int] NOT NULL PRIMARY KEY,
	[CHANNEL_NAME] [nvarchar](100) NOT NULL,
 )


