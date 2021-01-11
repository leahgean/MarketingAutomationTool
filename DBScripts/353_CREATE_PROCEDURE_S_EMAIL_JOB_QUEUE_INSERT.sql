CREATE PROCEDURE S_EMAIL_JOB_QUEUE_INSERT
@MessageId INT,
@CampaignId INT,
@CreatedBy UNIQUEIDENTIFIER,
@IPAddress NVARCHAR(20),
@Schedule DATETIME,
@bUpdate BIT,
@JobId INT OUTPUT
AS
BEGIN
INSERT INTO [dbo].[Email_Job_Queue]
           ([MessageId]
           ,[CampaignId]
           ,[CreatedBy]
           ,[IPAddress]
           ,[Schedule]
           ,[bUpdate])
VALUES
           (@MessageId, 
           @CampaignId,
           @CreatedBy,
           @IPAddress,
           @Schedule,
           @bUpdate)

SELECT @JobId = SCOPE_IDENTITY()
END


