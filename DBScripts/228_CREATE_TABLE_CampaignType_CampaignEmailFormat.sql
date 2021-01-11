CREATE TABLE CampaignType
(
Id INT IDENTITY(1,1) PRIMARY KEY,
CampaignType VARCHAR(50)
)

INSERT INTO CampaignType(CampaignType)
VALUES('Email')


CREATE TABLE CampaignEmailFormat
(
Id INT IDENTITY(1,1) PRIMARY KEY,
CampaignFormat VARCHAR(50) NOT NULL,
CampaignFormatValue INT NOT NULL
)


INSERT INTO CampaignEmailFormat(CampaignFormat,CampaignFormatValue)
VALUES('Text',0)
INSERT INTO CampaignEmailFormat(CampaignFormat,CampaignFormatValue)
VALUES('HTML',1)