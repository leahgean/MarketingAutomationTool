-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: December 6, 2020
-- Description:	Get count of new leads
-- =============================================
CREATE FUNCTION [dbo].[fn_Dashboard_EmailsSent]
(
	@AccountID UNIQUEIDENTIFIER
)
RETURNS INT
AS
BEGIN
	DECLARE @Count INT

	SELECT @Count=COUNT(E.EmailAddress) 
	FROM Campaign C WITH (NOLOCK)
	INNER JOIN EmailCampaignsSent E WITH (NOLOCK)
	ON C.AccountId=E.AccountUID AND C.Id=E.CampaignId
	WHERE C.AccountId=@AccountID
	AND C.Deleted=0
	AND C.HideInSearch=0
	AND C.CampaignStatus=1--Submitted
	AND E.EmailSent=1

	-- Return the result of the function
	RETURN @Count

END

