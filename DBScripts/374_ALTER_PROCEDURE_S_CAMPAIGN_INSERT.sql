ALTER PROCEDURE [dbo].[S_CAMPAIGN_INSERT]
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
@CampaignId INT OUTPUT,
@CampaignUID UNIQUEIDENTIFIER OUTPUT
AS
BEGIN

	IF (@CampaignStatus=0)--Draft
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
				   ,SearchId
				   ,ExcludeList
				   ,CreatedBy
				   ,IPAddress)
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
					@SearchId,
					@ExcludeList,
					@CreatedBy,
					@IPAddress)
	END
	ELSE IF (@CampaignStatus=1)--Submitted
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
			   ,SearchId
			   ,ExcludeList
			   ,CreatedBy
			   ,IPAddress
			   ,SubmittedDate
			   ,SubmittedBy)
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
				@SearchId,
				@ExcludeList,
				@CreatedBy,
				@IPAddress,
				GETUTCDATE(),
				@CreatedBy)
	END

	SET @CampaignId=SCOPE_IDENTITY()
	SELECT @CampaignUID=CampaignUID FROM Campaign WITH (NOLOCK) WHERE AccountId=@AccountID AND Id=@CampaignId
END
