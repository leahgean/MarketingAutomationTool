CREATE PROCEDURE S_LEAD_ADDContactSubscriptionHistory
@CONTACT_ID UNIQUEIDENTIFIER,
@EMAIL_ADDRESS NVARCHAR(250),
@ACTION_ID SMALLINT,
@ACTION_BY UNIQUEIDENTIFIER,
@ACTION_VIA VARCHAR(50),
@IP_ADDRESS VARCHAR(15),
@MESSAGE_ID INT,
@COUNTRY_CODE CHAR(3),
@CITY_NAME NVARCHAR(128),
@REGION_NAME NVARCHAR(128)
AS
BEGIN
INSERT INTO Contact_Subscription_History
           (CONTACT_ID
           ,EMAIL_ADDRESS
           ,ACTION_ID
           ,ACTION_TIME
           ,ACTION_BY
           ,ACTION_VIA
           ,IP_ADDRESS
           ,MESSAGE_ID
           ,COUNTRY_CODE
           ,CITY_NAME
           ,REGION_NAME)
     VALUES
           (@CONTACT_ID
           ,@EMAIL_ADDRESS
           ,@ACTION_ID
           ,GETDATE()
           ,@ACTION_BY
           ,@ACTION_VIA
           ,@IP_ADDRESS
           ,@MESSAGE_ID
           ,@COUNTRY_CODE
           ,@CITY_NAME
           ,@REGION_NAME)
END

--select * from Contact_Subscription_History
