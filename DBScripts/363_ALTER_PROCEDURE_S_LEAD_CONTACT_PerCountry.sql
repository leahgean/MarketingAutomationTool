ALTER PROCEDURE [dbo].[S_LEAD_CONTACT_PerCountry]
 @AccountID UNIQUEIDENTIFIER
AS
BEGIN

DECLARE @Count AS INT =0

SELECT @Count=COUNT(*)
FROM
(
	SELECT ISNULL(C.CountryID,0) CountryID, ISNULL(SC.CountryName,'NOT SPECIFIED') [Country], COUNT(C.ContactID) [Count]
	FROM Contact C WITH (NOLOCK)
	LEFT JOIN Country SC WITH (NOLOCK)
	ON C.CountryID = SC.CountryID
	WHERE C.AccountID=@AccountID AND YEAR(DATEADD(HH,8,C.CreatedDate)) <= YEAR(DATEADD(HH,8,GETUTCDATE()))
	GROUP BY C.CountryId , SC.CountryName
) sub

IF (@COUNT > 5)
BEGIN

	SELECT  TopCountries.CountryID, TopCountries.[Country], TopCountries.[Count], TopCountries.RN
	FROM
	(
		SELECT ISNULL(C.CountryID,0) CountryID, ISNULL(SC.CountryName,'NOT SPECIFIED') [Country], COUNT(C.ContactID) [Count],ROW_NUMBER() OVER(ORDER BY COUNT(C.ContactID) DESC) RN
		FROM Contact C WITH (NOLOCK)
		LEFT JOIN Country SC WITH (NOLOCK)
		ON C.CountryID = SC.CountryID
		WHERE C.AccountID=@AccountID AND YEAR(DATEADD(HH,8,C.CreatedDate)) <=  YEAR(DATEADD(HH,8,GETUTCDATE()))
		GROUP BY C.CountryId , SC.CountryName
	) TopCountries
	WHERE TopCountries.RN <=4
	UNION
	SELECT -1 CountryID, 'ALL OTHER COUNTRIES' [Country], SUM(Others.[Count]) [Count], 6 RN
	FROM
	(
		SELECT ISNULL(C.CountryID,0) CountryID, ISNULL(SC.CountryName,'NOT SPECIFIED') [Country], COUNT(C.ContactID) [Count],ROW_NUMBER() OVER(ORDER BY COUNT(C.ContactID) DESC) RN
		FROM Contact C WITH (NOLOCK)
		LEFT JOIN Country SC WITH (NOLOCK)
		ON C.CountryID = SC.CountryID
		WHERE C.AccountID=@AccountID AND YEAR(DATEADD(HH,8,C.CreatedDate)) <=  YEAR(DATEADD(HH,8,GETUTCDATE()))
		GROUP BY C.CountryId , SC.CountryName
	) Others
	WHERE Others.RN >4
	ORDER BY TopCountries.RN

END
ELSE
BEGIN
	SELECT ISNULL(C.CountryID,0) CountryID, ISNULL(SC.CountryName,'NOT SPECIFIED') [Country], COUNT(C.ContactID) [Count]
	FROM Contact C WITH (NOLOCK)
	LEFT JOIN Country SC WITH (NOLOCK)
	ON C.CountryID = SC.CountryID
	WHERE C.AccountID=@AccountID AND YEAR(DATEADD(HH,8,C.CreatedDate)) <=  YEAR(DATEADD(HH,8,GETUTCDATE()))
	GROUP BY C.CountryId , SC.CountryName
	ORDER BY SC.CountryName
END

END


--exec S_LEAD_CONTACT_PerCountry '6388071F-36BB-4823-A3ED-8770ADAE0F51'