CREATE PROCEDURE [dbo].[S_MESSAGE_UPDATE]
@AccountId UNIQUEIDENTIFIER,
@MessageFormat INT,
@Entity NVARCHAR(10),
@Subject NVARCHAR(250),
@SenderName NVARCHAR(256),
@SenderEmail NVARCHAR(250),
@MessageBody NTEXT,
@ModifiedBy UNIQUEIDENTIFIER,
@MessageId INT,
@MessageUID UNIQUEIDENTIFIER OUTPUT
AS
BEGIN

UPDATE [Message]
SET MessageFormat=@MessageFormat,
Entity=@Entity,
Subject=@Subject,
SenderName=@SenderName,
SenderEmail=@SenderEmail,
MessageBody=@MessageBody,
ModifiedBy=@ModifiedBy,
ModifiedDate=GETUTCDATE()
WHERE AccountId=@AccountId AND Id=@MessageId

SELECT @MessageUID=MessageUID FROM [Message] WITH (NOLOCK) WHERE AccountId=@AccountId AND Id=@MessageId

END

