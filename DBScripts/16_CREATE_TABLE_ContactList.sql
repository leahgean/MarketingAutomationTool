USE MarketingAutomationTool
GO

CREATE TABLE ContactList(
	ID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	AccountID UNIQUEIDENTIFIER NULL,
	ListName NVARCHAR(100) NOT NULL,
	ListDescription NVARCHAR(500) NULL,
	DateCreated DATETIME NOT NULL DEFAULT(GETDATE()),
	CreatedBy UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [User](UserID) NOT NULL,
	ModifiedDate DATETIME  NULL,
	ModifiedBy UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [User](UserID)  NULL
)

GO

CREATE TABLE ContactListFilter(
	ID BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	FilterName VARCHAR(50) NOT NULL,
	FilterCriteria TEXT NOT NULL,
	FilterType VARCHAR(20) NOT NULL,
	CreatedBy UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [User](UserID) NOT NULL,
	DateCreated DATETIME NOT NULL DEFAULT(GETDATE()),
	ModifiedBy UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [User](UserID) NULL,
	ModifiedDate DATETIME  NULL,
	ContactListId INT FOREIGN KEY REFERENCES ContactList(ID) NOT NULL,
	IsGlobal BIT NOT NULL,
	DeleteFilter BIT NULL,
	SearchType NVARCHAR(20) NULL
)

GO

CREATE TABLE ContactListContacts(
	ID INT IDENTITY(1,1) NOT NULL,
	ContactListID INT FOREIGN KEY REFERENCES ContactList(ID) NOT NULL,
	ContactID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Contact(ContactID) NOT NULL,
	CreatedBy UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [User](UserID) NOT NULL,
	DateCreated DATETIME NOT NULL DEFAULT(GETDATE()),
	ModifiedBy UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [User](UserID) NULL,
	ModifiedDate DATETIME  NULL
 )