USE MarketingAutomationTool
GO
/****** Object:  UserDefinedFunction [dbo].[isUniqueEmail]    Script Date: 2/26/2020 2:12:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: 26-Feb-2020
-- Description:	Check whether the email is unique or not in con_contacts table
-- =============================================


CREATE FUNCTION [dbo].[isUniqueEmail] (@Account_Id UNIQUEIDENTIFIER, @strEmail varchar(60))  
RETURNS varchar(30)
AS  
BEGIN 
DECLARE @strEmail1 varchar(60)
DECLARE @ID UNIQUEIDENTIFIER
DECLARE @S varchar(3)
DECLARE @return bit
	IF @strEmail IS NULL 
		RETURN NULL

	SET @return = 1

	SELECT TOP 1 @ID = ContactID FROM Contact WITH (NOLOCK) WHERE AccountID=@Account_Id  and EmailAddress = @strEmail
	IF @ID IS NOT NULL
	begin
		SET @return = 0
		RETURN @return
	end

	RETURN @return
END

/*
select [dbo].[isUniqueEmail]('CCF4D547-C9EC-42B4-97DF-B12752734323','vaibhav@selectbytes.com') as 'IsUnique'

*/

