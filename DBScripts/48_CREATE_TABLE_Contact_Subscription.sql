USE [MarketingAutomationTool]


CREATE TABLE [dbo].[Contact_Subscription](
	[CONTACT_ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY REFERENCES Contact(ContactID),
	[DATE_CREATED] [smalldatetime] NOT NULL DEFAULT (GETDATE()),
	[LAST_UPDATED] [smalldatetime] NOT NULL,
	[SUBSCRIBED_VIA] [varchar](50) NOT NULL,
	[SUBSCRIBED_BY] [uniqueidentifier] NULL,
	[SUBSCRIBE_IP_ADDRESS] [varchar](15) NULL,
	[UNSUBSCRIBED_DATE] [smalldatetime] NULL,
	[UNSUBSCRIBED_VIA] [varchar](50) NULL,
	[UNSUBSCRIBED_BY] [uniqueidentifier] NULL,
	[UNSUBSCRIBE_IP_ADDRESS] [varchar](15) NULL,
	[BOUNCES_COUNT] [smallint] NULL DEFAULT ((0)),
	[FORM_ID] [int] NULL,
	[UNSUBSCRIBED_SMS_DATE] [datetime] NULL
) 

