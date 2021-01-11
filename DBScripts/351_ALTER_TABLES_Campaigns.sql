ALTER TABLE Campaign
ADD IPAddress NVARCHAR(20)


ALTER TABLE EmailCampaignsSent
ADD IPAddress NVARCHAR(20)


ALTER TABLE CampaignClickThroughs
ADD IPAddress NVARCHAR(20)


ALTER TABLE CampaignUnsubscribes
ADD IPAddress NVARCHAR(20)


ALTER TABLE CampaignsOpened
ADD IPAddress NVARCHAR(20)


