ALTER PROCEDURE [dbo].[S_CAMPAIGN_UPDATE]
@AccountId UNIQUEIDENTIFIER,
@CampaignName NVARCHAR(128),
@CampaignType INT,
@CampaignFormat INT,
@CampaignDescription NVARCHAR(250),
@MessageId INT,
@UseBounceAddressInFromField BIT,
@HideInSearch BIT,
@CampaignStatus INT,
@TemplateId INT,
@SendingOption INT,
@SendingSchedule DATETIME,
@SearchId INT,
@ExcludeList NVARCHAR(MAX),
@CreatedBy UNIQUEIDENTIFIER,
@IPAddress NVARCHAR(20),
@CampaignUID UNIQUEIDENTIFIER,
@CampaignId INT OUTPUT
AS
BEGIN
	IF (@CampaignStatus=0) --Draft
	BEGIN
		UPDATE Campaign
		SET CampaignName=@CampaignName,
			CampaignType=@CampaignType,
			CampaignFormat=@CampaignFormat,
			CampaignDescription=@CampaignDescription,
			MessageId=@MessageId,
			UseBounceAddressInFromField=@UseBounceAddressInFromField,
			HideInSearch=@HideInSearch,
			CampaignStatus=@CampaignStatus,
			TemplateId=@TemplateId,
			SendingOption=@SendingOption,
			SendingSchedule=@SendingSchedule,
			SearchId=@SearchId,
			ExcludeList=@ExcludeList,
			ModifiedBy=@CreatedBy,
			ModifiedDate=GETUTCDATE(),
			IPAddress=@IPAddress
		WHERE AccountId=@AccountId AND CampaignUID=@CampaignUID
	END
	ELSE IF (@CampaignStatus=1) --Submitted
	BEGIN
		UPDATE Campaign
		SET CampaignName=@CampaignName,
			CampaignType=@CampaignType,
			CampaignFormat=@CampaignFormat,
			CampaignDescription=@CampaignDescription,
			MessageId=@MessageId,
			UseBounceAddressInFromField=@UseBounceAddressInFromField,
			HideInSearch=@HideInSearch,
			CampaignStatus=@CampaignStatus,
			TemplateId=@TemplateId,
			SendingOption=@SendingOption,
			SendingSchedule=@SendingSchedule,
			SearchId=@SearchId,
			ExcludeList=@ExcludeList,
			ModifiedBy=@CreatedBy,
			ModifiedDate=GETUTCDATE(),
			IPAddress=@IPAddress,
			SubmittedDate=GETUTCDATE(),
			SubmittedBy=@CreatedBy
		WHERE AccountId=@AccountId AND CampaignUID=@CampaignUID
	END

	SELECT @CampaignId=Id FROM Campaign WITH (NOLOCK) WHERE AccountId=@AccountID AND CampaignUID=@CampaignUID
END
