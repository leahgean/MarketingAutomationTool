ALTER TABLE CampaignContact
ADD CreatedBy UNIQUEIDENTIFIER NOT NULL REFERENCES [User](UserId)

ALTER TABLE CampaignContact
ADD CreatedDate DATETIME NOT NULL DEFAULT(GETDATE())

ALTER TABLE CampaignContact
ADD ModifiedBy UNIQUEIDENTIFIER NULL REFERENCES [User](UserId)

ALTER TABLE CampaignContact
ADD ModifiedDate DATETIME NULL 


