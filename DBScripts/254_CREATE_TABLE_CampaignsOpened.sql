CREATE TABLE CampaignsOpened
(
ID INT IDENTITY(1,1) PRIMARY KEY,
AccountID UNIQUEIDENTIFIER  REFERENCES Account(AccountID) NOT NULL,
CampaignID UNIQUEIDENTIFIER NOT NULL,
ContactID UNIQUEIDENTIFIER REFERENCES Contact(ContactId) NOT NULL,
CreatedDate DATETIME NOT NULL Default(GETDATE()),
CreatedBy UNIQUEIDENTIFIER REFERENCES Contact(ContactId) NOT NULL
)