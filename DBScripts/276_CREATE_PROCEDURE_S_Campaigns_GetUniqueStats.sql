CREATE PROCEDURE S_Campaigns_GetUniqueStats
 @AccountID UNIQUEIDENTIFIER 
AS
BEGIN


DECLARE @EmailCampaignsSent AS TABLE([Year] INT, [Month] INT, [Count] INT)
DECLARE @EmailOpened AS TABLE([Year] INT, [Month] INT, [Count] INT)
DECLARE @EmailClickthroughs AS TABLE([Year] INT, [Month] INT, [Count] INT)
DECLARE @Months AS TABLE([Name] VARCHAR(50),[Order] INT, [Year] INT, [Month] INT,[Count] INT)

	INSERT INTO @EmailCampaignsSent([Year],[Month],[Count])
	SELECT YEAR(C.CreatedDate) [Year], MONTH(C.CreatedDate) [Month], COUNT(*) CNT
	FROM Campaign C WITH (NOLOCK)
	INNER JOIN EmailCampaignsSent E WITH (NOLOCK)
	ON C.AccountId=E.AccountUID AND C.Id=E.CampaignId
	WHERE C.AccountId=@AccountID
	AND C.Deleted=0
	AND C.HideInSearch=0
	AND C.CampaignStatus=1--Submitted
	AND E.EmailSent=1
	AND CAST(E.CreatedDate AS DATE)>=DATEADD(MONTH,-6,CAST(GETDATE() AS DATE)) AND CAST(E.CreatedDate AS DATE)<=CAST(GETDATE() AS DATE)
	GROUP BY YEAR(C.CreatedDate),MONTH(C.CreatedDate)

	INSERT INTO @EmailOpened([Year],[Month],[Count])
	SELECT YEAR(C.CreatedDate) [Year], MONTH(C.CreatedDate) [Month], COUNT(*) CNT
	FROM Campaign C WITH (NOLOCK)
	INNER JOIN EmailCampaignsSent E WITH (NOLOCK)
	ON C.AccountId=E.AccountUID AND C.Id=E.CampaignId
	INNER JOIN (
	
	
		SELECT CO.AccountID,CO.CampaignID,CO.ContactID
		FROM Campaign C WITH (NOLOCK)
		INNER JOIN CampaignsOpened CO WITH (NOLOCK)
		ON C.AccountId=CO.AccountID AND C.CampaignUId=CO.CampaignId
		WHERE C.AccountId=@AccountID
		AND C.Deleted=0
		AND C.HideInSearch=0
		AND C.CampaignStatus=1--Submitted
		GROUP BY CO.AccountID,CO.CampaignID,CO.ContactID
	) EO
	ON C.AccountId=EO.AccountID AND C.CampaignUID=EO.CampaignID AND E.ContactID=EO.ContactID
	WHERE C.AccountId=@AccountID
	AND C.Deleted=0
	AND C.HideInSearch=0
	AND C.CampaignStatus=1--Submitted
	AND CAST(E.CreatedDate AS DATE)>=DATEADD(MONTH,-6,CAST(GETDATE() AS DATE)) AND CAST(E.CreatedDate AS DATE)<=CAST(GETDATE() AS DATE)
	GROUP BY YEAR(C.CreatedDate),MONTH(C.CreatedDate)

	INSERT INTO @EmailClickthroughs([Year],[Month],[Count])
	SELECT YEAR(C.CreatedDate) [Year], MONTH(C.CreatedDate) [Month], COUNT(*) CNT
	FROM Campaign C WITH (NOLOCK)
	INNER JOIN EmailCampaignsSent E WITH (NOLOCK)
	ON C.AccountId=E.AccountUID AND C.Id=E.CampaignId
	INNER JOIN (
		SELECT CO.AccountID, CO.CampaignID, CO.ContactId,CO.Link
		FROM Campaign C WITH (NOLOCK)
		INNER JOIN CampaignClickThroughs CO WITH (NOLOCK)
		ON C.AccountId=CO.AccountID AND C.CampaignUId=CO.CampaignId
		WHERE C.AccountId=@AccountID
		AND C.Deleted=0
		AND C.HideInSearch=0
		AND C.CampaignStatus=1--Submitted
		GROUP BY CO.AccountID, CO.CampaignID, CO.ContactId, CO.Link
	) CO 
	ON C.AccountId=CO.AccountID AND C.CampaignUId=CO.CampaignId AND E.ContactID=CO.ContactID
	WHERE C.AccountId=@AccountID 
	AND C.Deleted=0
	AND C.HideInSearch=0
	AND C.CampaignStatus=1--Submitted
	GROUP BY YEAR(C.CreatedDate),MONTH(C.CreatedDate)

	INSERT INTO @Months([Name],[Order],[Year],[Month],[Count])
	SELECT 'Sent',1,[YEAR], [MONTH], [Count]
	FROM @EmailCampaignsSent ES
	UNION
	SELECT 'Opens',2, [YEAR], [MONTH],[Count]
	FROM @EmailOpened ES
	UNION
	SELECT 'Clicks',3, [YEAR], [MONTH],[Count]
	FROM @EmailClickthroughs ES

	SELECT CAST(CAST(M.[Month] AS VARCHAR(5))+'/1/'+CAST(M.[Year] AS VARCHAR(5)) AS DATE) [Date],DATENAME(MONTH,CAST(CAST(M.[Month] AS VARCHAR(5))+'/1/'+CAST(M.[Year] AS VARCHAR(5)) AS DATE)) [MonthName], *
	FROM @Months M
	ORDER BY CAST(CAST(M.[Month] AS VARCHAR(5))+'/1/'+CAST(M.[Year] AS VARCHAR(5)) AS DATE) , M.[Order]

END


--exec S_Campaigns_GetUniqueStats '6388071F-36BB-4823-A3ED-8770ADAE0F51' 
	

