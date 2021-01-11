USE [MarketingAutomationTool]


CREATE TABLE [dbo].[ContactJob](
	[JobId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[JobName] [varchar](50) NOT NULL,
	[JobStatusId] [tinyint] NOT NULL REFERENCES Sys_Job_Status(STATUS_ID),
	[AccountID] [uniqueidentifier] NOT NULL REFERENCES Account(AccountID),
	[CreatedBy] [uniqueidentifier] NOT NULL REFERENCES [User](UserID),
	[FileFormat] [varchar](5) NOT NULL,
	[OriginalFileName] [varchar](150) NULL,
	[FileName] [varchar](150) NULL,
	[ContactListId] [int] NULL REFERENCES ContactList(ID) DEFAULT(NULL),
	[DateCreated] [smalldatetime] NOT NULL DEFAULT(GETDATE()),
	[JobStarted] [smalldatetime] NULL,
	[JobFinished] [smalldatetime] NULL,
	[Error] [text] NULL,
	[TotalContacts] [int] NULL,
	[UploadedContacts] [int] NULL
)
