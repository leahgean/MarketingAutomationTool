USE MarketingAutomationTool
GO

CREATE TABLE [dbo].[SYS_LANGUAGE_RESOURCE](
	[RL_ID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[LANG_ID] [varchar](3) NULL,
	[RES_ID] [int] NOT NULL,
	[RES_VAL] [nvarchar](max) NOT NULL,
	[PARENT_ID] [int] NULL 
)
	