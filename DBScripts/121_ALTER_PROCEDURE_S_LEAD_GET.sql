USE [MarketingAutomationTool]
GO
/****** Object:  StoredProcedure [dbo].[S_LEAD_GET]    Script Date: 8/10/2020 8:20:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[S_LEAD_GET](@ContactID UNIQUEIDENTIFIER, @AccountID UNIQUEIDENTIFIER)
AS
BEGIN


SELECT ContactID
      ,AccountID
      ,FirstName
      ,MiddleName
      ,LastName
      ,MobileNumber
      ,PhoneNumber
      ,EmailAddress
      ,Address1
      ,Address2
      ,CountryId
      ,ZipCode
      ,CompanyName
      ,CreatedDate
      ,CreatedBy
      ,ModifiedDate
      ,ModifiedBy
      ,WebSite
      ,Position
      ,FacebookID
      ,ID
      ,LeadStatus
      ,ContactStatus
      ,ContactType
      ,Title
      ,Gender
      ,City
      ,[State]
      ,SubscribedToEmail
      ,UseforTesting
	  ,IsDeleted
  FROM dbo.Contact WITH (NOLOCK)
  WHERE ContactID = @ContactID AND AccountID = @AccountID

END 
