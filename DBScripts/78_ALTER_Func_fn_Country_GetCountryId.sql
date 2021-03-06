ALTER FUNCTION [dbo].[fn_Country_GetCountryId]
(
@CountryName NVARCHAR(50)
)
RETURNS INT
AS
BEGIN

	DECLARE @CountryId INT=NULL

	IF((@CountryName IS NOT NULL) AND (@CountryName<>''))
	BEGIN
		SELECT @CountryId=CountryId 
		FROM Country 
		WHERE CountryName=@CountryName
	END
	

	RETURN @CountryId
END

