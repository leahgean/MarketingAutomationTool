CREATE PROCEDURE [dbo].[S_LEAD_UNSUBSCRIBE]
@AccountID UNIQUEIDENTIFIER,
@ContactID UNIQUEIDENTIFIER,
@ModifiedBy UNIQUEIDENTIFIER 
AS
BEGIN



	UPDATE dbo.Contact
	SET SubscribedToEmail=0,
		UnsubscribedToEmailDate=GETDATE(),
		ModifiedBy=@ModifiedBy,
		ModifiedDate = GETDATE()
	WHERE AccountID=@AccountID AND ContactID = @ContactID

     		
END
 
