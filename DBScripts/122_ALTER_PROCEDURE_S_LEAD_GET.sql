USE [MarketingAutomationTool]
GO
/****** Object:  StoredProcedure [dbo].[S_LEAD_GET]    Script Date: 8/11/2020 7:19:19 AM ******/
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
	  ,DeletedDate
  FROM dbo.Contact WITH (NOLOCK)
  WHERE ContactID = @ContactID AND AccountID = @AccountID

END 
