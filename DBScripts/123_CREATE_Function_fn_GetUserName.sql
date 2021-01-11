-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: August 11, 2020
-- Description:	Get Username
-- =============================================
CREATE FUNCTION fn_GetUserName
(
	@UserId UNIQUEIDENTIFIER,
	@AccountId UNIQUEIDENTIFIER
)
RETURNS NVARCHAR(201)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @UserName NVARCHAR(201)=''

	SELECT @UserName = FirstName + ' ' + LastName
	FROM [User] WITH (NOLOCK)
	WHERE UserId = @UserId AND AccountId=@AccountId

	RETURN @UserName

END


