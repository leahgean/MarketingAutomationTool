CREATE  TABLE EmailCampaignsSent
(
Id INT IDENTITY PRIMARY KEY,
AccountUID UNIQUEIDENTIFIER NOT NULL REFERENCES Account(AccountId), 
CampaignId INT NOT NULL REFERENCES Campaign(Id),
MessageId INT NOT NULL REFERENCES [Message](Id),
ContactID UNIQUEIDENTIFIER NOT NULL REFERENCES Contact(ContactId),
EmailAddress NVARCHAR(250),
EmailSent BIT,
ErrorMessage NVARCHAR(MAX),
CreatedDate DATETIME NOT NULL DEFAULT(GETDATE()),
CreatedBy UNIQUEIDENTIFIER NOT NULL REFERENCES [User](UserId),
ModifiedDate DATETIME NULL,
ModifiedBy UNIQUEIDENTIFIER REFERENCES [User](UserId)  

)