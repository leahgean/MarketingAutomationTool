
CREATE FUNCTION [dbo].[fn_Country_GetCountryId]
(
@CountryName NVARCHAR(50)
)
RETURNS INT
AS
BEGIN

	DECLARE @CountryId INT

	SELECT @CountryId=CountryId 
	FROM Country 
	WHERE CountryName=@CountryName

	RETURN @CountryId
END

