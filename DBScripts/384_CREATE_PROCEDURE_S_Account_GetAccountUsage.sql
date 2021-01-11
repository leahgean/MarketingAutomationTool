CREATE PROCEDURE S_Account_GetAccountUsage
@AccountID UNIQUEIDENTIFIER,
@Year INT=2021
AS
BEGIN

DECLARE @Contact AS TABLE(YearNum INT, MonthNum INT, Cnt INT)
DECLARE @Campaign AS TABLE(YearNum INT, MonthNum INT, Cnt INT)
DECLARE @AllMonths AS TABLE(YearNum INT, MonthNum INT, [Month] NVARCHAR(30))
DECLARE @Month AS  INT =0
DECLARE @EndMonth AS INT =0

INSERT INTO @Contact(YearNum, MonthNum, Cnt)
SELECT YEAR(DATEADD(HH,8,C.CreatedDate)) YearNum, MONTH(DATEADD(HH,8,C.CreatedDate)) MonthNum, COUNT(*) [Count]
FROM Contact C WITH (NOLOCK)
INNER JOIN Contact_Subscription CS WITH (NOLOCK)
ON C.ContactID= CS.CONTACT_ID
WHERE C.AccountID=@AccountID AND YEAR(DATEADD(HH,8,C.CreatedDate))=@Year
GROUP BY YEAR(DATEADD(HH,8,C.CreatedDate)), MONTH(DATEADD(HH,8,C.CreatedDate))
ORDER BY YEAR(DATEADD(HH,8,C.CreatedDate)), MONTH(DATEADD(HH,8,C.CreatedDate))

INSERT INTO @Campaign(YearNum, MonthNum, Cnt)
SELECT YEAR(DATEADD(HH,8,C.CreatedDate)) YearNum, MONTH(DATEADD(HH,8,C.CreatedDate)) MonthNum, COUNT(*) [Count]
FROM Campaign C WITH (NOLOCK)
WHERE C.AccountID=@AccountID AND YEAR(DATEADD(HH,8,C.CreatedDate))=@Year
GROUP BY YEAR(DATEADD(HH,8,C.CreatedDate)), MONTH(DATEADD(HH,8,C.CreatedDate))
ORDER BY YEAR(DATEADD(HH,8,C.CreatedDate)), MONTH(DATEADD(HH,8,C.CreatedDate))

SET @Month=1
SET @EndMonth=12

IF (@Year = YEAR(DATEADD(HH,8,GETUTCDATE())))
BEGIN
	SET @EndMonth=MONTH(DATEADD(HH,8,GETUTCDATE()))
END

WHILE (@Month<=@EndMonth)
BEGIN
	INSERT INTO @AllMonths(YearNum,MonthNum,[Month])
	VALUES(@Year,@Month,FORMAT(CAST(CAST(@Month AS NVARCHAR(5))+'/1/'+CAST(@Year AS NVARCHAR(5)) AS DATETIME), 'MMMM', 'en-US'))

	SET @Month=@Month+1
END


SELECT A.MonthNum MonthNumber, A.[Month],ISNULL(C.Cnt,0) ContactsLeads, ISNULL(Ca.Cnt,0) Email
FROM @AllMonths A
LEFT JOIN @Contact C
ON A.YearNum=C.YearNum AND A.MonthNum=C.MonthNum
LEFT JOIN @Campaign Ca
ON A.YearNum=Ca.YearNum AND A.MonthNum=Ca.MonthNum
ORDER BY A.YearNum, A.MonthNum

END

--EXEC S_Account_GetAccountUsage '6388071F-36BB-4823-A3ED-8770ADAE0F51',2020



