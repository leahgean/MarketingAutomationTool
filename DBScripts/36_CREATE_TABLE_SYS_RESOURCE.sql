USE MarketingAutomationTool

CREATE TABLE [dbo].[SYS_RESOURCE](
	[RES_ID] [int] NOT NULL PRIMARY KEY,
	[DESCRIPTION] [varchar](128) NOT NULL,
	[SectionName] [char](32) NULL,
	[FieldName] [char](64) NULL
) 

