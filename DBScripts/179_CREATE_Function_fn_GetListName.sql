-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: October 18, 2020
-- Description:	Get List Name
-- =============================================
CREATE FUNCTION [dbo].[fn_GetListName]
(
	@ListId INT,
	@AccountId UNIQUEIDENTIFIER
)
RETURNS NVARCHAR(100)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ListName NVARCHAR(100)=''

	SELECT @ListName = ISNULL(ListName,'')
	FROM ContactList WITH (NOLOCK)
	WHERE ID = @ListId AND AccountId=@AccountId

	RETURN @ListName

END


