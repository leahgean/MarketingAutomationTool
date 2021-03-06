USE [MarketingAutomationTool]
GO
/****** Object:  StoredProcedure [dbo].[S_ContactListJob_Insert]    Script Date: 2/25/2020 8:56:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[S_ContactJob_Insert]
@JobName VARCHAR(50),
@JobStatusId TINYINT,
@AccountID UNIQUEIDENTIFIER,
@CreatedBy UNIQUEIDENTIFIER,
@FileFormat VARCHAR(5),
@OriginalFileName VARCHAR(150),
@FileName VARCHAR(150),
@ContactListId INT
AS
BEGIN

INSERT INTO ContactJob
           (JobName
           ,JobStatusId
           ,AccountID
		   ,CreatedBy
           ,FileFormat
           ,OriginalFileName
           ,[FileName]
           ,ContactListId)
     VALUES
           (@JobName
           ,@JobStatusId
           ,@AccountID
		   ,@CreatedBy
           ,@FileFormat
           ,@OriginalFileName
           ,@FileName
           ,@ContactListId)
END


select * from contactjob

