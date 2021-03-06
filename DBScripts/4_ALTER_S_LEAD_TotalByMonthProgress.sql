ALTER PROCEDURE [dbo].[S_LEAD_TotalByMonthProgress]
@AccountID UNIQUEIDENTIFIER,
@Year INT
AS
BEGIN


SELECT ROW_NUMBER () OVER ( ORDER BY MONTH(C.CreatedDate) ) ID, 
MONTH(C.CreatedDate) [MONTHNUM],
FORMAT(C.CreatedDate, 'MMMM', 'en-US') [MONTH], 
COUNT(C.ContactID) [COUNT]
FROM Contact C WITH (NOLOCK)
LEFT JOIN Country SC WITH (NOLOCK)
ON C.CountryId = SC.CountryId
WHERE AccountID=@AccountID AND YEAR(CreatedDate)=@Year
GROUP BY MONTH(CreatedDate), FORMAT(CreatedDate, 'MMMM', 'en-US')
ORDER BY  MONTH(CreatedDate)
END