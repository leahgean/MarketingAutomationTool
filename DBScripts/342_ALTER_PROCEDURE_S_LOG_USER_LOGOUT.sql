ALTER PROCEDURE [dbo].[S_LOG_USER_LOGOUT]
@User_Id UNIQUEIDENTIFIER,
@Session_Id VARCHAR(50)

AS
BEGIN

DECLARE @LastLogOutDate AS DATETIME 
SET @LastLogOutDate = GETUTCDATE()

UPDATE User_Access_Log 
SET LOG_OUT=@LastLogOutDate
WHERE Session_Id=@Session_Id AND User_Id=@User_Id



UPDATE [User] 
SET LastLogoutDate=@LastLogOutDate
WHERE  UserID=@User_Id

END


