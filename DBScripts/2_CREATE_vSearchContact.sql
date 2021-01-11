CREATE VIEW [dbo].[vSearchContact]
AS
SELECT ID, LastName, FirstName, EmailAddress, MobileNumber, ContactID ContactGUID,IsDeleted, DeletedDate, ContactType, LeadStatus, ContactStatus
FROM Contact WITH (NOLOCK)


