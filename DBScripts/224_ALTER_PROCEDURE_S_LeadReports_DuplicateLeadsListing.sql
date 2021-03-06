-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: 08-November-2020
-- Description:	Lists unsubcribed leads 
-- =============================================
CREATE PROCEDURE [dbo].[S_LeadReports_DuplicateLeadsListing]
	@ACCOUNT_ID UNIQUEIDENTIFIER,
	@TOTALLEADCNT INT OUTPUT
AS
BEGIN

	SELECT @TOTALLEADCNT=COUNT(*)
	FROM
	(
	SELECT  EmailAddress, AccountId, COUNT(*) CNT
	FROM Contact WITH (NOLOCK)
	WHERE AccountId='6388071F-36BB-4823-A3ED-8770ADAE0F51' AND IsDeleted=0
	GROUP BY EmailAddress, AccountId
	HAVING COUNT(*)>1
	) D
	INNER JOIN Contact C WITH (NOLOCK)
	ON D.AccountId=C.AccountID AND D.EmailAddress = C.EmailAddress


	SELECT C.FirstName, C.LastName, C.EmailAddress, C.CreatedDate, C.ImportSource
	FROM
	(
	SELECT  EmailAddress, AccountId, COUNT(*) CNT
	FROM Contact WITH (NOLOCK)
	WHERE AccountId='6388071F-36BB-4823-A3ED-8770ADAE0F51' AND IsDeleted=0
	GROUP BY EmailAddress, AccountId
	HAVING COUNT(*)>1
	) D
	INNER JOIN Contact C WITH (NOLOCK)
	ON D.AccountId=C.AccountID AND D.EmailAddress = C.EmailAddress
	ORDER BY   C.EmailAddress,C.CreatedDate DESC, C.FirstName, C.LastName
	
END

--DECLARE @TOTALLEADCNT INT 
--EXEC [dbo].[S_LeadReports_DuplicateLeadsListing] '6388071F-36BB-4823-A3ED-8770ADAE0F51', @TOTALLEADCNT OUTPUT