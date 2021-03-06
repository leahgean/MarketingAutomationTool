-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: October 29, 2020
-- Description:	Get Search ID by Search Type
-- =============================================
CREATE FUNCTION [dbo].[fn_GetSearchId_BySearchType]
(
	@AccountID UNIQUEIDENTIFIER,
	@SearchType NVARCHAR(20)
)
RETURNS INT
AS
BEGIN
	DECLARE @SearchID INT

	SELECT @SearchID=ID FROM ContactSearch WITH (NOLOCK) WHERE  AccountID=@AccountID AND SearchType=@SearchType

	-- Return the result of the function
	RETURN @SearchID

END
