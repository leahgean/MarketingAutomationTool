USE [MarketingAutomationTool]
GO

/****** Object:  View [dbo].[vSearchContact]    Script Date: 12/24/2019 3:12:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER VIEW [dbo].[vSearchContact]
WITH SCHEMABINDING
AS
SELECT 
ID,
LastName,
FirstName,
EmailAddress,
MobileNumber,
ContactID ContactGUID,
IsDeleted,
DeletedDate,
ContactType,
LeadStatus,
ContactStatus,
ISNULL(FirstName, '') + ' ' + ISNULL(LastName, '') + ' ' + ISNULL(EmailAddress, '') + ' ' + ISNULL(MobileNumber, '') AS SEARCH_FIELDS
FROM dbo.[Contact] 


GO


