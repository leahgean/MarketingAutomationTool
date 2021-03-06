-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: October 29, 2020
-- Description:	Get Search ID for ViewAll
-- =============================================
CREATE FUNCTION [dbo].[fn_GetSearchId_ViewAll]
(
	@AccountID UNIQUEIDENTIFIER
)
RETURNS INT
AS
BEGIN
	DECLARE @SearchID INT

	SELECT @SearchID=ID FROM ContactSearch WITH (NOLOCK) WHERE  AccountID=@AccountID AND SearchType='VIEWALL'

	-- Return the result of the function
	RETURN @SearchID

END
