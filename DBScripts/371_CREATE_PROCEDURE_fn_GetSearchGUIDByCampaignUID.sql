-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: January 4, 2021
-- Description:	Get Search GUID
-- =============================================
CREATE FUNCTION [dbo].[fn_GetSearchGUIDByCampaignUID]
(
	@CampaignUID UNIQUEIDENTIFIER,
	@AccountID UNIQUEIDENTIFIER
)
RETURNS UNIQUEIDENTIFIER
AS
BEGIN

	DECLARE @SearchGUID UNIQUEIDENTIFIER

	SELECT @SearchGUID=ISNULL(CS.SEARCHUID,'') 
	FROM Campaign C WITH (NOLOCK)
	INNER JOIN ContactSearch CS WITH (NOLOCK)
	ON C.AccountId=CS.ACCOUNTID AND C.SearchID=CS.Id
	WHERE C.AccountId=@AccountID AND C.CampaignUID=@CampaignUID

	-- Return the result of the function
	RETURN @SearchGUID

END


