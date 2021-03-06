ALTER PROCEDURE [dbo].[S_CAMPAIGN_DELETE]
@AccountUID UNIQUEIDENTIFIER,
@CampaignUID UNIQUEIDENTIFIER,
@DeletedBy UNIQUEIDENTIFIER
AS
BEGIN


DECLARE @MessageId INT
SELECT @MessageId=MessageId FROM Campaign WITH (NOLOCK) WHERE AccountId=@AccountUID AND CampaignUId=@CampaignUID

UPDATE Campaign
SET Deleted=1,
	DeletedDate=GETUTCDATE(),
	DeletedBy=@DeletedBy,
	ModifiedDate=GETUTCDATE(),
	ModifiedBy=@DeletedBy
WHERE AccountId=@AccountUID AND CampaignUID=@CampaignUID


UPDATE [Message]
SET Deleted=1,
	DeletedDate=GETUTCDATE(),
	DeletedBy=@DeletedBy,
	ModifiedDate=GETUTCDATE(),
	ModifiedBy=@DeletedBy
WHERE AccountId=@AccountUID AND Id=@MessageId



END