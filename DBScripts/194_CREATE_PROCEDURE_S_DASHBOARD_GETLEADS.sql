CREATE PROCEDURE [dbo].[S_DASHBOARD_GETLEADS]
@AccountID UNIQUEIDENTIFIER
AS
BEGIN
	SELECT TOP 5 ID ContactID, dbo.fn_Lead_PadID(ID) ContactIDText, ContactID ContactGUID, FirstName, LastName, EmailAddress, MobileNumber 
	FROM Contact WITH (NOLOCK)
    WHERE AccountID=@AccountID AND IsDeleted=0
	ORDER BY CreatedDate DESC
END

--EXEC S_DASHBOARD_GETLEADS '6388071F-36BB-4823-A3ED-8770ADAE0F51'
