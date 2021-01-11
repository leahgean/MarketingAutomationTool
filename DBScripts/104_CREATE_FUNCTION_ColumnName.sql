-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: August 4, 2020
-- Description:	Replace with actual column name
-- =============================================
CREATE FUNCTION fnColumnName
(
	@CodeName NVARCHAR(250)
)
RETURNS NVARCHAR(250)
AS
BEGIN
	DECLARE @ColumnName AS NVARCHAR(250)=''

	IF (@CodeName='firstname')
	BEGIN
		SET @ColumnName='FirstName'
	END
	ELSE IF (@CodeName='lastname')
	BEGIN
		SET @ColumnName='LastName'
	END
	ELSE IF (@CodeName='email')
	BEGIN
		SET @ColumnName='EmailAddress'
	END
	ELSE IF (@CodeName='companyname')
	BEGIN
		SET @ColumnName='CompanyName'
	END
	ELSE IF (@CodeName='position')
	BEGIN
		SET @ColumnName='Position'
	END
	ELSE IF (@CodeName='website')
	BEGIN
		SET @ColumnName='WebSite'
	END
	ELSE IF (@CodeName='mobile')
	BEGIN
		SET @ColumnName='MobileNumber'
	END
	ELSE IF (@CodeName='phoneno')
	BEGIN
		SET @ColumnName='PhoneNumber'
	END
	ELSE IF (@CodeName='address')
	BEGIN
		SET @ColumnName='Address1 +' + ' ' + '+ Address2'
	END
	ELSE IF (@CodeName='city')
	BEGIN
		SET @ColumnName='City'
	END
	ELSE IF (@CodeName='state')
	BEGIN
		SET @ColumnName='State'
	END
	ELSE IF (@CodeName='gender')
	BEGIN
		SET @ColumnName='Gender'
	END
	ELSE IF (@CodeName='country')
	BEGIN
		SET @ColumnName='CountryId'
	END
	ELSE IF (@CodeName='contacttype')
	BEGIN
		SET @ColumnName='ContactType'
	END
	ELSE IF (@CodeName='contactstatus')
	BEGIN
		SET @ColumnName='ContactStatus'
	END
	ELSE IF (@CodeName='subscribedtoemail')
	BEGIN
		SET @ColumnName='SubscribedToEmail'
	END

	RETURN @ColumnName

END
GO

