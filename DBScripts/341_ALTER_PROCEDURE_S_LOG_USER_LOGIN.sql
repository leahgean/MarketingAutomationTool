ALTER PROCEDURE [dbo].[S_LOG_USER_LOGIN]
@UserId UNIQUEIDENTIFIER,
@IP_Address VARCHAR(15),
@SessionId VARCHAR(50)

AS
BEGIN

DECLARE @LastLoginDate AS DATETIME 
SET @LastLoginDate = GETUTCDATE()

INSERT INTO User_Access_Log ([User_Id],IP_Address,Log_In,Log_Out,Session_Id) 
VALUES(@UserId,@IP_Address,@LastLoginDate, NULL,@SessionId)

UPDATE [User]
Set LastLoginDate = @LastLoginDate
WHERE UserId = @UserId

END


