CREATE PROCEDURE S_EMAIL_JOB_QUEUE_HISTORY_INSERT
@JobId INT,
@STATUS TINYINT
AS
BEGIN

INSERT INTO [dbo].[Email_Job_Queue_History]
           ([JobId]
           ,[STATUS])
     VALUES
           (@JobId, 
           @STATUS)
END


