-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: October 26, 2020
-- Description:	Get count of unsubscribes
-- =============================================
CREATE FUNCTION [dbo].[fn_Dashboard_UnSubscribes]
(
	@AccountID UNIQUEIDENTIFIER
)
RETURNS INT
AS
BEGIN
	DECLARE @Count INT

	SELECT @Count=COUNT(*)
	FROM Contact WITH (NOLOCK) 
	WHERE AccountID=@AccountID AND 
			IsDeleted=0 
			AND SubscribedToEmail=0

	-- Return the result of the function
	RETURN @Count

END

