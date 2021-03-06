USE [MarketingAutomationTool]
GO
/****** Object:  StoredProcedure [dbo].[S_ContactSearch_GetRecentSearches]    Script Date: 9/6/2020 6:07:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[S_ContactSearch_GetRecentSearches]
@ACCOUNTID AS UNIQUEIDENTIFIER,
@CREATEDBY AS UNIQUEIDENTIFIER
AS
BEGIN

DECLARE @ContactSearch AS TABLE
(
	[ID] [int]  NOT NULL,
	[SEARCHUID] [uniqueidentifier] NOT NULL
)

INSERT INTO @ContactSearch
SELECT TOP 5 C.ID, C.SEARCHUID
FROM ContactSearch C WITH (NOLOCK)
WHERE AccountId=@AccountID AND CreatedBy=@CreatedBy
ORDER BY C.ID DESC

SELECT 'Recent Search ' + CAST(ROW_NUMBER() OVER(ORDER BY ID ) AS NVARCHAR(5)) [Name],*
FROM @ContactSearch
ORDER BY ID DESC

END

--exec S_ContactSearch_GetRecentSearches '6388071F-36BB-4823-A3ED-8770ADAE0F51','E0B31172-B6DA-4D4E-BFE2-F32274D03FF1'