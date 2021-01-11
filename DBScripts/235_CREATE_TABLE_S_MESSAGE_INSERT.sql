CREATE PROCEDURE S_MESSAGE_INSERT
@AccountId UNIQUEIDENTIFIER,
@MessageFormat INT,
@Entity NVARCHAR(10),
@Subject NVARCHAR(250),
@SenderName NVARCHAR(256),
@SenderEmail NVARCHAR(250),
@MessageBody NTEXT,
@CreatedBy UNIQUEIDENTIFIER,
@MessageId INT OUTPUT,
@MessageUID UNIQUEIDENTIFIER OUTPUT
AS
BEGIN

INSERT INTO [Message]
           (AccountId
           ,MessageFormat
           ,Entity
           ,Subject
           ,SenderName
           ,SenderEmail
           ,MessageBody
           ,CreatedBy)
     VALUES
           (@AccountId,
           @MessageFormat,
           @Entity,
           @Subject,
           @SenderName,
           @SenderEmail,
           @MessageBody,
           @CreatedBy)

SET @MessageId=SCOPE_IDENTITY()
SELECT @MessageUID=MessageUID FROM [Message] WITH (NOLOCK) WHERE AccountId=@AccountId AND Id=@MessageId

END

