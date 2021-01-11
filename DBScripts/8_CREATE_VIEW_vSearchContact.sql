CREATE VIEW [dbo].[vSearchContact]
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
FROM Contact WITH (NOLOCK)


GO

