CREATE TABLE [dbo].[Email_Job_Queue_History](
	[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[JobId] [INT] NOT NULL REFERENCES Email_Job_Queue(Id),
	[STATUS] [TINYINT] NOT NULL REFERENCES Email_Job_Status(Status_Id),
	[CreatedDate] [DATETIME] NOT NULL DEFAULT(GETUTCDATE())
 )
GO


