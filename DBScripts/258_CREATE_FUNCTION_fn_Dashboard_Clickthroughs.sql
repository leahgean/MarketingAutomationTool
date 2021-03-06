-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: October 26, 2020
-- Description:	Get count of new leads
-- =============================================
CREATE FUNCTION [dbo].[fn_Dashboard_Clickthroughs]
(
	@AccountID UNIQUEIDENTIFIER
)
RETURNS INT
AS
BEGIN
	DECLARE @Count INT

	SELECT @Count=Count(CO.Id)
	FROM Campaign C WITH (NOLOCK)
	INNER JOIN CampaignClickThroughs CO WITH (NOLOCK)
	ON C.AccountId=CO.AccountID AND C.CampaignUId=CO.CampaignId
	WHERE C.AccountId=@AccountID
	AND C.Deleted=0
	AND C.HideInSearch=0
	AND C.CampaignStatus=1--Submitted

	-- Return the result of the function
	RETURN @Count

END

