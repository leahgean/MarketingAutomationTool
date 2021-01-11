ALTER TABLE Contact_Subscription
ALTER COLUMN SUBSCRIBED_VIA NVARCHAR(100)

ALTER TABLE Contact_Subscription
ALTER COLUMN UNSUBSCRIBED_VIA NVARCHAR(100)


ALTER TABLE Contact_Subscription_History
ALTER COLUMN ACTION_BY UNIQUEIDENTIFIER


ALTER TABLE Contact_Subscription_History
ALTER COLUMN ACTION_VIA NVARCHAR(100)


ALTER TABLE Contact_Subscription_History
ALTER COLUMN COUNTRY_CODE INT


ALTER TABLE Contact_Subscription_History
ALTER COLUMN CITY_NAME NVARCHAR(50)


ALTER TABLE Contact_Subscription_History
ALTER COLUMN REGION_NAME NVARCHAR(50)

ALTER TABLE Contact_Subscription
ALTER COLUMN SUBSCRIBED_VIA VARCHAR(50) NULL


ALTER TABLE Contact_Subscription
ADD SUBSCRIBED_DATE SMALLDATETIME NULL

