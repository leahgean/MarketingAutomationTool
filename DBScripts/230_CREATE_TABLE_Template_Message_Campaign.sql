CREATE TABLE Template
(
Id INT IDENTITY(1,1) PRIMARY KEY,
TemplateUID UNIQUEIDENTIFIER DEFAULT(NEWID()) NOT NULL,
AccountId UNIQUEIDENTIFIER REFERENCES Account(AccountID) NOT NULL,
MessageBody NTEXT NOT NULL,
CreatedDate DATETIME DEFAULT(GETDATE()) NOT NULL,
CreatedBy UNIQUEIDENTIFIER REFERENCES [User](UserId) NOT NULL,
ModifiedDate DATETIME,
ModifiedBy UNIQUEIDENTIFIER REFERENCES [User](UserId),
Deleted BIT,
DeletedDate DATETIME,
DeletedBy UNIQUEIDENTIFIER REFERENCES [User](UserId)
)


CREATE TABLE [Message]
(
Id INT IDENTITY(1,1) PRIMARY KEY,
MessageUID UNIQUEIDENTIFIER DEFAULT(NEWID()) NOT NULL,
AccountId UNIQUEIDENTIFIER REFERENCES Account(AccountID) NOT NULL,
MessageFormat INT REFERENCES CampaignEmailFormat(Id) NOT NULL,
Entity NVARCHAR(10),--EMAIL, FORM
[Subject] NVARCHAR(250) NOT NULL,
SenderName NVARCHAR(256) NOT NULL,
SenderEmail NVARCHAR(250) NOT NULL,
MessageBody NTEXT NOT NULL,
CreatedDate DATETIME DEFAULT(GETDATE()) NOT NULL,
CreatedBy UNIQUEIDENTIFIER REFERENCES [User](UserId) NOT NULL,
ModifiedDate DATETIME,
ModifiedBy UNIQUEIDENTIFIER REFERENCES [User](UserId),
Deleted BIT,
DeletedDate DATETIME,
DeletedBy UNIQUEIDENTIFIER REFERENCES [User](UserId)
)

CREATE TABLE Campaign
(
Id INT IDENTITY(1,1) PRIMARY KEY,
CampaignUID UNIQUEIDENTIFIER DEFAULT(NEWID()),
AccountId UNIQUEIDENTIFIER REFERENCES Account(AccountID) NOT NULL,
CampaignName NVARCHAR(128) NOT NULL,
CampaignType INT REFERENCES CampaignType(Id) NOT NULL,
CampaignFormat INT REFERENCES CampaignEmailFormat(Id) NOT NULL,
CampaignDescription NVARCHAR(250) NULL,
MessageId INT REFERENCES Message(Id) NULL,
UseBounceAddressInFromField BIT,
HideInSearch BIT DEFAULT(0),
CampaignStatus INT NOT NULL, --0-Draft, --1-Submitted
TemplateId INT NULL REFERENCES Template(Id),
CreatedDate DATETIME DEFAULT(GETDATE()) NOT NULL,
CreatedBy UNIQUEIDENTIFIER REFERENCES [User](UserId) NOT NULL,
ModifiedDate DATETIME,
ModifiedBy UNIQUEIDENTIFIER REFERENCES [User](UserId),
Deleted BIT,
DeletedDate DATETIME,
DeletedBy UNIQUEIDENTIFIER REFERENCES [User](UserId)
)



