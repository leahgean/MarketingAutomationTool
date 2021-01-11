CREATE PROCEDURE S_Campaigns_EmailStats
@AccountId UNIQUEIDENTIFIER,
@StartDate DATETIME, 
@EndDate DATETIME,
@CampaignId INT
AS
BEGIN
DECLARE @SQL AS NVARCHAR(MAX)=''
DECLARE @SQLSelect AS NVARCHAR(MAX)=''
DECLARE @SQLPart1 AS NVARCHAR(MAX)=''
DECLARE @SQLPart2 AS NVARCHAR(MAX)=''
DECLARE @SQLCriteria AS NVARCHAR(MAX)=''
DECLARE @SQLAddedTable AS NVARCHAR(MAX)=''

DECLARE @EmailsSent AS TABLE(
CampaignId INT, 
CampaignName NVARCHAR(128),
MessageId INT,
JobId INT,
JobStatus INT,
CampaignCreatedDate DATETIME,
Cnt INT)

DECLARE @EmailsOpened AS TABLE(
CampaignId INT, 
CampaignName NVARCHAR(128),
MessageId INT,
JobId INT,
JobStatus INT,
CampaignCreatedDate DATETIME,
Cnt INT)

DECLARE @UniqueEmailsOpened AS TABLE(
CampaignId INT, 
CampaignName NVARCHAR(128),
MessageId INT,
JobId INT,
JobStatus INT,
CampaignCreatedDate DATETIME,
Cnt INT)


DECLARE @TotalClickthroughs AS TABLE(
CampaignId INT, 
CampaignName NVARCHAR(128),
MessageId INT,
JobId INT,
JobStatus INT,
CampaignCreatedDate DATETIME,
Cnt INT)


DECLARE @UniqueClickthroughs AS TABLE(
CampaignId INT, 
CampaignName NVARCHAR(128),
MessageId INT,
JobId INT,
JobStatus INT,
CampaignCreatedDate DATETIME,
Cnt INT)

	SET @SQLSelect='SELECT C.Id, C.CampaignName,M.Id,JQ.Id,JQH.[Status], C.CreatedDate, Count(CS.ContactId) EmailsSent '
	
	SET @SQLPart1='FROM Campaign C WITH (NOLOCK) '+
	'INNER JOIN [Message] M WITH (NOLOCK) '+
	'ON C.AccountID=M.AccountId AND C.MessageId=M.ID '+
	'INNER JOIN Email_Job_Queue JQ WITH (NOLOCK) '+
	'ON C.Id=JQ.CampaignID '+
	'INNER JOIN '+
	'( '+
	'	SELECT C.Id CampaignId, M.Id MessageId,JQ.Id JobId, MAX(JQH.[Status]) [Status] '+
	'	FROM Campaign C WITH (NOLOCK) '+
	'	INNER JOIN [Message] M WITH (NOLOCK) '+
	'	ON C.AccountID=M.AccountId AND C.MessageId=M.ID '+
	'	INNER JOIN Email_Job_Queue JQ WITH (NOLOCK) '+
	'	ON C.Id=JQ.CampaignID '+
	'	INNER JOIN Email_Job_Queue_History JQH WITH (NOLOCK) '+
	'	ON JQ.Id=JQH.JobId '+
	'	WHERE C.AccountId='+''''+CAST(@AccountId AS NVARCHAR(50))+''''+ ' AND C.CampaignStatus=1 AND C.Deleted=0 AND M.Deleted=0 '+
	'	GROUP BY  C.Id, M.Id,JQ.Id '+
	'	HAVING MAX(JQH.[Status])=3 '+
	')JQH '+
	'ON C.Id=JQH.CampaignId AND M.Id=JQH.MessageId AND JQ.Id=JQH.JobId '+
	'INNER JOIN EmailCampaignsSent CS WITH (NOLOCK) '+
	'ON C.AccountId=CS.AccountUID AND C.Id=CS.CampaignId '
	
	SET @SQLCriteria='WHERE C.AccountId='+''''+CAST(@AccountId AS NVARCHAR(50))+''''+' AND C.CampaignStatus=1 AND C.Deleted=0 AND M.Deleted=0 '
	
	
	IF (@StartDate IS NOT NULL)
		SET @SQLCriteria=@SQLCriteria+'AND CAST(DATEADD(HH,8,CS.CreatedDate) AS DATE)>= '+ ''''+CAST(CAST(@StartDate AS DATE) AS NVARCHAR(20))+'''' 
	
	IF (@EndDate IS NOT NULL)	
		SET @SQLCriteria=@SQLCriteria+' AND CAST(DATEADD(HH,8,CS.CreatedDate) AS DATE)<= '+ ''''+CAST(CAST(@EndDate AS DATE) AS NVARCHAR(20))+'''' 

	IF (@CampaignId IS NOT NULL)	
		SET @SQLCriteria=@SQLCriteria+' AND C.Id= '+ CAST(@CampaignId AS NVARCHAR(20))

	SET @SQLPart2=' GROUP BY C.Id,C.CampaignName,M.Id,JQ.Id,JQH.[Status], C.CreatedDate '+
	'ORDER BY C.CreatedDate'

	/*EmailsSent*/
	SET @SQL=@SQLSelect+@SQLPart1+@SQLCriteria+@SQLPart2

	--PRINT @SQL
	
	INSERT INTO @EmailsSent(CampaignId,CampaignName,MessageId,JobId,JobStatus,CampaignCreatedDate,Cnt)
	EXEC(@SQL)

	/*TotalOpens*/
	SET @SQLSelect='SELECT C.Id, C.CampaignName,M.Id,JQ.Id,JQH.[Status], C.CreatedDate, Count(CO.ContactID) TotalOpens '

	SET @SQLAddedTable='INNER JOIN CampaignsOpened CO WITH (NOLOCK) '+
	'ON C.AccountId=CS.AccountUID AND C.CampaignUID=CO.CampaignId AND CS.ContactID=CO.ContactID '

	SET @SQL=@SQLSelect+@SQLPart1+@SQLAddedTable+@SQLCriteria+@SQLPart2

	INSERT INTO @EmailsOpened(CampaignId,CampaignName,MessageId,JobId,JobStatus,CampaignCreatedDate,Cnt)
	EXEC(@SQL)

	/*UniqueOpens*/
	SET @SQLSelect='SELECT C.Id, C.CampaignName,M.Id,JQ.Id,JQH.[Status], C.CreatedDate, Count(DISTINCT CO.ContactID) UniqueOpens '

	SET @SQLAddedTable='INNER JOIN CampaignsOpened CO WITH (NOLOCK) '+
	'ON C.AccountId=CS.AccountUID AND C.CampaignUID=CO.CampaignId AND CS.ContactID=CO.ContactID '

	SET @SQL=@SQLSelect+@SQLPart1+@SQLAddedTable+@SQLCriteria+@SQLPart2

	INSERT INTO @UniqueEmailsOpened(CampaignId,CampaignName,MessageId,JobId,JobStatus,CampaignCreatedDate,Cnt)
	EXEC(@SQL)

	/*TotalClickthroughs*/
	SET @SQLSelect='SELECT C.Id, C.CampaignName,M.Id,JQ.Id,JQH.[Status], C.CreatedDate, Count(CT.ContactID) TotalClickthroughs '

	SET @SQLAddedTable='INNER JOIN CampaignClickThroughs CT WITH (NOLOCK) '+
	'ON C.AccountId=CS.AccountUID AND C.CampaignUID=CT.CampaignId AND CS.ContactID=CT.ContactID '

	SET @SQL=@SQLSelect+@SQLPart1+@SQLAddedTable+@SQLCriteria+@SQLPart2

	INSERT INTO @TotalClickthroughs(CampaignId,CampaignName,MessageId,JobId,JobStatus,CampaignCreatedDate,Cnt)
	EXEC(@SQL)

	/*UniqueClickthroughs*/	
	SET @SQLSelect='SELECT C.Id, C.CampaignName,M.Id,JQ.Id,JQH.[Status], C.CreatedDate, Count(DISTINCT CT.ContactID) UniqueClickthroughs '

	SET @SQLAddedTable= 'INNER JOIN CampaignClickThroughs CT WITH (NOLOCK) '+
	'ON C.AccountId=CS.AccountUID AND C.CampaignUID=CT.CampaignId AND CS.ContactID=CT.ContactID '

	SET @SQL=@SQLSelect+@SQLPart1+@SQLAddedTable+@SQLCriteria+@SQLPart2

	INSERT INTO @UniqueClickthroughs(CampaignId,CampaignName,MessageId,JobId,JobStatus,CampaignCreatedDate,Cnt)
	EXEC(@SQL)

	SELECT ES.CampaignId,ES.CampaignName,ES.MessageId,ES.JobId,DATEADD(HH,8,ES.CampaignCreatedDate) CampaignCreatedDate,ISNULL(ES.Cnt,0) EmailsSent, ISNULL(EO.Cnt,0) TotalOpens,ISNULL(UO.Cnt,0) UniqueOpens,ISNULL(TC.Cnt,0) TotalClicks,ISNULL(UC.Cnt,0) UniqueClicks
	FROM @EmailsSent ES
	LEFT JOIN @EmailsOpened EO
	ON ES.CampaignId=EO.CampaignId
	LEFT JOIN @UniqueEmailsOpened UO
	ON ES.CampaignId=UO.CampaignId
	LEFT JOIN @TotalClickthroughs TC
	ON ES.CampaignId=TC.CampaignId
	LEFT JOIN @UniqueClickthroughs UC
	ON ES.CampaignId=UC.CampaignId
	ORDER BY ES.CampaignCreatedDate
END


--SELECT * FROM EmailCampaignsSent WITH (NOLOCK)


--EXEC S_Campaigns_EmailStats '6388071F-36BB-4823-A3ED-8770ADAE0F51','1/2/2021','1/6/2021',28
--EXEC S_Campaigns_EmailStats '6388071F-36BB-4823-A3ED-8770ADAE0F51',NULL,NULL,NULL
--EXEC S_Campaigns_EmailStats '6388071F-36BB-4823-A3ED-8770ADAE0F51',NULL,NULL,28



