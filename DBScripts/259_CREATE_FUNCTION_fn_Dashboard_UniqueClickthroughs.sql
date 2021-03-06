-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: December 12, 2020
-- Description:	Get count of unique clickthroughs
-- =============================================
CREATE FUNCTION [dbo].[fn_Dashboard_UniqueClickthroughs]
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
	SELECT CO.AccountID, CO.CampaignID, CO.ContactId,CO.Link
	FROM Campaign C WITH (NOLOCK)
	INNER JOIN CampaignClickThroughs CO WITH (NOLOCK)
	ON C.AccountId=CO.AccountID AND C.CampaignUId=CO.CampaignId
	WHERE C.AccountId=@AccountID
	AND C.Deleted=0
	AND C.HideInSearch=0
	AND C.CampaignStatus=1--Submitted
	GROUP BY CO.AccountID, CO.CampaignID, CO.ContactId, CO.Link
	) X
	-- Return the result of the function
	RETURN @Count

END

