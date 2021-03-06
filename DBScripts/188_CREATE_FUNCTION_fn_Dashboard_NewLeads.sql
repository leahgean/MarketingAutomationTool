-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: October 26, 2020
-- Description:	Get count of new leads
-- =============================================
CREATE FUNCTION [dbo].[fn_Dashboard_NewLeads]
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
			AND DATEDIFF(DAY,CreatedDate,GETDATE())<=7

	-- Return the result of the function
	RETURN @Count

END

