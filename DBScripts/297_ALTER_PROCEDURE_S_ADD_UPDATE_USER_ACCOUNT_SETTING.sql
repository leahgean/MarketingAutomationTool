ALTER PROCEDURE [dbo].[S_ADD_UPDATE_USER_ACCOUNT_SETTING]
(
	@Account_Id UNIQUEIDENTIFIER,
	@User_Id UNIQUEIDENTIFIER,
	@Setting_Category NVARCHAR(64),
	@Setting_Key NVARCHAR(256),
	@Setting_Value NVARCHAR(MAX),
	@Sort_Order INT,
	@Built_In BIT
)
AS
BEGIN


DECLARE @SettingCount AS INT

SELECT @SettingCount = COUNT(*) 
FROM User_Account_Setting UA WITH (NOLOCK) 
WHERE UA.Account_Id = @Account_Id 
AND UA.User_Id = @User_Id 
AND Setting_Category = @Setting_Category 
AND Sort_Order = @Sort_Order

	IF (@SettingCount=0)
	BEGIN

		INSERT INTO User_Account_Setting
				   (Account_Id
				   ,[User_Id]
				   ,Setting_Category
				   ,Setting_Key
				   ,Setting_Value
				   ,Sort_Order
				   ,Date_Modified
				   ,Modified_By
				   ,Date_Created
				   ,Created_By
				   ,Built_In)
		 VALUES
				   (@Account_Id
				   ,@User_Id
				   ,@Setting_Category
				   ,@Setting_Key
				   ,@Setting_Value
				   ,@Sort_Order
				   ,GETUTCDATE()
				   ,@User_Id
				   ,GETUTCDATE()
				   ,@User_Id
				   ,@Built_In)
	END
	ELSE
	BEGIN
		UPDATE User_Account_Setting
		SET Setting_Key = @Setting_Key,
			Setting_Value = @Setting_Value,
			Date_Modified = GETUTCDATE(),
			Modified_By = @User_Id,
			Built_In = @Built_In
		WHERE Account_Id = @Account_Id 
			AND [User_Id] = @User_Id 
			AND Setting_Category = @Setting_Category 
			AND Sort_Order = @Sort_Order

	END
END
