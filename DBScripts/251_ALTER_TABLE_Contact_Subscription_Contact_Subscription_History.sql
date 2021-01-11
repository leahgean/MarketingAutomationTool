ALTER TABLE Contact_Subscription
ADD CampaignID INT NULL REFERENCES Campaign(Id)


ALTER TABLE Contact_Subscription_History
ADD CampaignID INT NULL REFERENCES Campaign(Id)