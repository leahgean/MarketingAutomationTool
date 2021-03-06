
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
      ,DATEADD(HH,8,CreatedDate) CreatedDate
      ,CreatedBy
      ,DATEADD(HH,8,ModifiedDate) ModifiedDate
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
	  ,DATEADD(HH,8,DeletedDate) DeletedDate 
  FROM dbo.Contact WITH (NOLOCK)
  WHERE ContactID = @ContactID AND AccountID = @AccountID

END 
