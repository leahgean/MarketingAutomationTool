USE [MarketingAutomationTool]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Country_GetCountryName]    Script Date: 8/9/2020 11:57:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[fn_Country_GetCountryName]
(
@CountryId INT
)
RETURNS NVARCHAR(50)
AS
BEGIN

	DECLARE @CountryName NVARCHAR(50)=''

	IF((@CountryId IS NOT NULL) AND (@CountryId<>0))
	BEGIN
		SELECT @CountryName=CountryName 
		FROM Country 
		WHERE CountryId=@CountryId
	END
	

	RETURN @CountryName
END

