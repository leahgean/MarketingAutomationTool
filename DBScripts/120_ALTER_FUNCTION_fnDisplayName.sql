USE [MarketingAutomationTool]
GO
/****** Object:  UserDefinedFunction [dbo].[fnDisplayName]    Script Date: 8/9/2020 4:49:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: August 4, 2020
-- Description:	Replace with user-friendly name
-- =============================================
ALTER FUNCTION [dbo].[fnDisplayName]
(
	@ColumnName NVARCHAR(250)
)
RETURNS NVARCHAR(250)
AS
BEGIN
	DECLARE @DisplayName AS NVARCHAR(250)=''

	IF (@ColumnName='firstname')
	BEGIN
		SET @DisplayName='First Name'
	END
	ELSE IF (@ColumnName='middlename')
	BEGIN
		SET @DisplayName='Middle Name'
	END
	ELSE IF (@ColumnName='lastname')
	BEGIN
		SET @DisplayName='Last Name'
	END
	ELSE IF (@ColumnName='email')
	BEGIN
		SET @DisplayName='Email'
	END
	ELSE IF (@ColumnName='companyname')
	BEGIN
		SET @DisplayName='Company Name'
	END
	ELSE IF (@ColumnName='position')
	BEGIN
		SET @DisplayName='Position'
	END
	ELSE IF (@ColumnName='website')
	BEGIN
		SET @DisplayName='Website'
	END
	ELSE IF (@ColumnName='mobile')
	BEGIN
		SET @DisplayName='Mobile'
	END
	ELSE IF (@ColumnName='phoneno')
	BEGIN
		SET @DisplayName='Phone No'
	END
	ELSE IF (@ColumnName='address')
	BEGIN
		SET @DisplayName='Address'
	END
	ELSE IF (@ColumnName='city')
	BEGIN
		SET @DisplayName='City'
	END
	ELSE IF (@ColumnName='state')
	BEGIN
		SET @DisplayName='State'
	END
	ELSE IF (@ColumnName='gender')
	BEGIN
		SET @DisplayName='Gender'
	END
	ELSE IF (@ColumnName='country')
	BEGIN
		SET @DisplayName='Country'
	END
	ELSE IF (@ColumnName='contacttype')
	BEGIN
		SET @DisplayName='Type'
	END
	ELSE IF (@ColumnName='contactstatus')
	BEGIN
		SET @DisplayName='Status'
	END
	ELSE IF (@ColumnName='subscribedtoemail')
	BEGIN
		SET @DisplayName='Email Subscription'
	END
	ELSE IF (@ColumnName='isdeleted')
	BEGIN
		SET @DisplayName='Deleted'
	END
	ELSE IF (@ColumnName='usefortesting')
	BEGIN
		SET @DisplayName='Used For Testing'
	END

	RETURN @DisplayName

END
