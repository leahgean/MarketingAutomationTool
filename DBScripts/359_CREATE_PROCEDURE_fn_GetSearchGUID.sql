-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: January 4, 2021
-- Description:	Get Search GUID
-- =============================================
CREATE FUNCTION [dbo].[fn_GetSearchGUID]
(
	@SearchID INT,
	@AccountID UNIQUEIDENTIFIER
)
RETURNS UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @SearchGUID UNIQUEIDENTIFIER

	SELECT @SearchGUID=SEARCHUID FROM ContactSearch WITH (NOLOCK) WHERE ID=@SearchID AND AccountID=@AccountID

	-- Return the result of the function
	RETURN @SearchGUID

END


