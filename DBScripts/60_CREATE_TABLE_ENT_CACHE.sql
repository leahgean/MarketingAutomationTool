use MarketingAutomationTool

CREATE TABLE ENT_CACHE
(
CACHE_ID INT IDENTITY NOT NULL PRIMARY KEY,
ACCOUNT_ID VARCHAR(64),
[USER_ID] VARCHAR(64),
[TYPE] TINYINT NOT NULL,
REFLECTION_TYPE VARCHAR(MAX),
CACHE_KEY VARCHAR(128) NOT NULL,
CACHE_DATA NVARCHAR(MAX),
CACHE_DATA_BINARY VARBINARY(MAX),
DATE_CREATED DATETIME NOT NULL
)