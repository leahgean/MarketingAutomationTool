ALTER PROCEDURE [dbo].[S_ContactSearch_GetRecentSearches]
@ACCOUNTID AS UNIQUEIDENTIFIER,
@CREATEDBY AS UNIQUEIDENTIFIER
AS
BEGIN

DECLARE @ContactSearch AS TABLE
(
	[ID] [int]  NOT NULL,
	[SEARCHUID] [uniqueidentifier] NOT NULL,
	CreatedDate DATETIME NOT NULL,
	ModifiedDate DATETIME NULL
)

INSERT INTO @ContactSearch
SELECT TOP 5 C.ID, C.SEARCHUID, C.CreatedDate, C.ModifiedDate
FROM ContactSearch C WITH (NOLOCK)
WHERE AccountId=@AccountID AND CreatedBy=@CreatedBy
ORDER BY ISNULL(C.ModifiedDate, C.CreatedDate) DESC

SELECT 'Search ' + CAST([ID] AS NVARCHAR(20)) [Name],*
FROM @ContactSearch

END

--exec S_ContactSearch_GetRecentSearches '6388071F-36BB-4823-A3ED-8770ADAE0F51','E0B31172-B6DA-4D4E-BFE2-F32274D03FF1'