USE [MarketingAutomationTool]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Country_GetCountryId]    Script Date: 8/4/2020 6:25:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_Country_GetCountryName]
(
@CountryId INT
)
RETURNS INT
AS
BEGIN

	DECLARE @CountryName NVARCHAR(50)=''

	IF((@CountryId IS NOT NULL) AND (@CountryId<>0))
	BEGIN
		SELECT @CountryName=CountryName 
		FROM Country 
		WHERE CountryName=@CountryId
	END
	

	RETURN @CountryName
END

