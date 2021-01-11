CREATE TABLE ContactSearchExportFiles
(Id INT IDENTITY,
SearchID INT NOT NULL REFERENCES ContactSearch(ID),
AccountID UNIQUEIDENTIFIER NOT NULL REFERENCES Account(AccountID),
CreatedBy UNIQUEIDENTIFIER NOT NULL  REFERENCES [User](UserId),
CreatedDate DATETIME NOT NULL DEFAULT(GETDATE()),
ModifiedBy UNIQUEIDENTIFIER NULL,
ModifiedDate DATETIME NULL,
FileTimeStamp NVARCHAR(250),
[FileName] NVARCHAR(250)
)
