ALTER PROCEDURE [dbo].[S_ContactJob_Insert]
@JobName VARCHAR(50),
@JobStatusId TINYINT,
@AccountID UNIQUEIDENTIFIER,
@CreatedBy UNIQUEIDENTIFIER,
@FileFormat VARCHAR(5),
@OriginalFileName VARCHAR(150),
@FileName VARCHAR(150),
@ContactListId INT,
@IPAddress VARCHAR(15)
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
           ,ContactListId
		   ,IPAddress)
     VALUES
           (@JobName
           ,@JobStatusId
           ,@AccountID
		   ,@CreatedBy
           ,@FileFormat
           ,@OriginalFileName
           ,@FileName
           ,@ContactListId
		   ,@IPAddress)
END


