CREATE PROCEDURE S_CAMPAIGN_INSERT
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
@CreatedBy UNIQUEIDENTIFIER,
@CampaignId INT OUTPUT,
@CampaignUID UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
INSERT INTO Campaign
           (AccountId
           ,CampaignName
           ,CampaignType
           ,CampaignFormat
           ,CampaignDescription
           ,MessageId
           ,UseBounceAddressInFromField
           ,HideInSearch
           ,CampaignStatus
           ,TemplateId
		   ,SendingOption
		   ,SendingSchedule
           ,CreatedBy)
     VALUES
           (@AccountId,
			@CampaignName,
			@CampaignType,
			@CampaignFormat,
			@CampaignDescription,
			@MessageId,
			@UseBounceAddressInFromField,
			@HideInSearch,
			@CampaignStatus,
			@TemplateId,
			@SendingOption,
			@SendingSchedule,
			@CreatedBy)

	SET @CampaignId=SCOPE_IDENTITY()
	SELECT @CampaignUID=CampaignUID FROM Campaign WITH (NOLOCK) WHERE AccountId=@AccountID AND Id=@CampaignId
END
