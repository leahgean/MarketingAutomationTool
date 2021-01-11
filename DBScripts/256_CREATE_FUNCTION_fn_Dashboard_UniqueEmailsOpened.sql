-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: October 26, 2020
-- Description:	Get count of new leads
-- =============================================
CREATE FUNCTION [dbo].[fn_Dashboard_UniqueEmailsOpened]
(
	@AccountID UNIQUEIDENTIFIER
)
RETURNS INT
AS
BEGIN
	DECLARE @Count INT

	SELECT @Count=COUNT(*)
	FROM
	(
		SELECT CO.AccountID,CO.CampaignID,CO.ContactID
		FROM Campaign C WITH (NOLOCK)
		INNER JOIN CampaignsOpened CO WITH (NOLOCK)
		ON C.AccountId=CO.AccountID AND C.CampaignUId=CO.CampaignId
		WHERE C.AccountId=@AccountID
		AND C.Deleted=0
		AND C.HideInSearch=0
		AND C.CampaignStatus=1--Submitted
		GROUP BY CO.AccountID,CO.CampaignID,CO.ContactID
	) X

	-- Return the result of the function
	RETURN @Count

END