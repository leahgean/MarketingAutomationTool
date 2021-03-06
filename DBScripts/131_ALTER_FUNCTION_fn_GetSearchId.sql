-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: August 16, 2020
-- Description:	Get Search ID
-- =============================================
ALTER FUNCTION [dbo].[fn_GetSearchId]
(
	@SearchGUID UNIQUEIDENTIFIER,
	@AccountID UNIQUEIDENTIFIER,
	@CreatedBy UNIQUEIDENTIFIER
)
RETURNS INT
AS
BEGIN
	DECLARE @SearchID INT

	SELECT @SearchID=ID FROM ContactSearch WITH (NOLOCK) WHERE SEARCHUID=@SearchGUID AND CreatedBy=@CreatedBy AND AccountID=@AccountID

	-- Return the result of the function
	RETURN @SearchID

END
