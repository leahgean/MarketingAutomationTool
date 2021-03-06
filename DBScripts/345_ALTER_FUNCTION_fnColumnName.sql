-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: August 4, 2020
-- Description:	Replace with actual column name
-- =============================================
ALTER FUNCTION [dbo].[fnColumnName]
(
	@CodeName NVARCHAR(250)
)
RETURNS NVARCHAR(250)
AS
BEGIN
	DECLARE @ColumnName AS NVARCHAR(250)=''

	SELECT @CodeName=LOWER(@CodeName)

	IF (@CodeName='id')
	BEGIN
		SET @ColumnName='C.Id'
	END
	ELSE IF (@CodeName='firstname')
	BEGIN
		SET @ColumnName='C.FirstName'
	END
	ELSE IF (@CodeName='middlename')
	BEGIN
		SET @ColumnName='C.MiddleName'
	END
	ELSE IF (@CodeName='lastname')
	BEGIN
		SET @ColumnName='C.LastName'
	END
	ELSE IF (@CodeName='email')
	BEGIN
		SET @ColumnName='C.EmailAddress'
	END
	ELSE IF (@CodeName='companyname')
	BEGIN
		SET @ColumnName='C.CompanyName'
	END
	ELSE IF (@CodeName='position')
	BEGIN
		SET @ColumnName='C.Position'
	END
	ELSE IF (@CodeName='website')
	BEGIN
		SET @ColumnName='C.WebSite'
	END
	ELSE IF (@CodeName='mobile')
	BEGIN
		SET @ColumnName='C.MobileNumber'
	END
	ELSE IF (@CodeName='phoneno')
	BEGIN
		SET @ColumnName='C.PhoneNumber'
	END
	ELSE IF (@CodeName='address')
	BEGIN
		SET @ColumnName='C.Address1 +' + ' ' + '+ C.Address2'
	END
	ELSE IF (@CodeName='city')
	BEGIN
		SET @ColumnName='C.City'
	END
	ELSE IF (@CodeName='state')
	BEGIN
		SET @ColumnName='C.State'
	END
	ELSE IF (@CodeName='gender')
	BEGIN
		SET @ColumnName='C.Gender'
	END
	ELSE IF (@CodeName='country')
	BEGIN
		SET @ColumnName='C.CountryId'
	END
	ELSE IF (@CodeName='contacttype')
	BEGIN
		SET @ColumnName='C.ContactType'
	END
	ELSE IF (@CodeName='contactstatus')
	BEGIN
		SET @ColumnName='C.ContactStatus'
	END
	ELSE IF (@CodeName='subscribedtoemail')
	BEGIN
		SET @ColumnName='C.SubscribedToEmail'
	END
	ELSE IF (@CodeName='isdeleted')
	BEGIN
		SET @ColumnName='C.IsDeleted'
	END
	ELSE IF (@CodeName='usefortesting')
	BEGIN
		SET @ColumnName='C.UseforTesting'
	END
	ELSE IF (@CodeName='createddate')
	BEGIN
		SET @ColumnName='DATEADD(HH,8,C.CreatedDate)'
	END
	ELSE IF (@CodeName='contactlist')
	BEGIN
		SET @ColumnName='CLC.ContactListID'
	END

	RETURN @ColumnName

END
