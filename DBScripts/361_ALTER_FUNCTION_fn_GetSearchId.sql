-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: August 16, 2020
-- Description:	Get Search ID
-- Modified: 01-04-2021, remove createdby parameter
-- =============================================
ALTER FUNCTION [dbo].[fn_GetSearchId]
(
	@SearchGUID UNIQUEIDENTIFIER,
	@AccountID UNIQUEIDENTIFIER
)
RETURNS INT
AS
BEGIN
	DECLARE @SearchID INT

	SELECT @SearchID=ID FROM ContactSearch WITH (NOLOCK) WHERE SEARCHUID=@SearchGUID AND AccountID=@AccountID

	-- Return the result of the function
	RETURN @SearchID

END
