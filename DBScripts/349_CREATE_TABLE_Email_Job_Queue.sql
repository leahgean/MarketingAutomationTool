CREATE TABLE [dbo].[Email_Job_Queue](
	[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[MessageId] [int] NOT NULL REFERENCES [Message](Id),
	[CampaignId] INT NOT NULL REFERENCES [Campaign](Id), 
	[CreatedBy] [UNIQUEIDENTIFIER] NOT NULL REFERENCES [User](UserId),
	[CreatedDate] [DATETIME] NOT NULL  DEFAULT (GETUTCDATE()),
	[IPAddress] [nvarchar](20) NOT NULL,
	[Schedule] [DATETIME] NULL,
	[bUpdate] [bit] NULL,

) 
GO

