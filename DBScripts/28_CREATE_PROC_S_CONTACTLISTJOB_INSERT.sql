CREATE PROCEDURE S_ContactListJob_Insert
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

INSERT INTO ContactListJob
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


