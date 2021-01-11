CREATE PROCEDURE S_Campaigns_TotalOpensListing
@AccountId UNIQUEIDENTIFIER,
@StartDate DATETIME, 
@EndDate DATETIME,
@CampaignId INT=NULL
AS
BEGIN
DECLARE @SQL AS NVARCHAR(MAX)=''
DECLARE @SQLSelect AS NVARCHAR(MAX)=''
DECLARE @SQLPart1 AS NVARCHAR(MAX)=''
DECLARE @SQLPart2 AS NVARCHAR(MAX)=''
DECLARE @SQLCriteria AS NVARCHAR(MAX)=''
DECLARE @SQLAddedTable AS NVARCHAR(MAX)=''

DECLARE @TotalOpens AS TABLE(
CampaignId INT, 
CampaignName NVARCHAR(128),
MessageId INT,
JobId INT,
JobStatus INT,
CampaignCreatedDate DATETIME,
ContactId UNIQUEIDENTIFIER,
FirstName NVARCHAR(100), 
LastName NVARCHAR(100), 
EmailAddress NVARCHAR(250), 
OpenedDate DATETIME)


	SET @SQLSelect='SELECT C.Id CampaignId, C.CampaignName,M.Id MessageId,JQ.Id JobId,JQH.[Status], C.CreatedDate, CO.ContactId, CT.FirstName, CT.LastName, CT.EmailAddress, CO.CreatedDate OpenedDate '
	
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
	'ON C.AccountId=CS.AccountUID AND C.Id=CS.CampaignId ' +
	'INNER JOIN CampaignsOpened CO WITH (NOLOCK) '+
	'ON C.AccountId=CS.AccountUID AND C.CampaignUID=CO.CampaignId AND CS.ContactID=CO.ContactID '+
	'INNER JOIN Contact CT WITH (NOLOCK) ' +
	'ON CO.AccountID=CT.AccountID AND CO.ContactID=CT.ContactID '
	
	SET @SQLCriteria='WHERE C.AccountId='+''''+CAST(@AccountId AS NVARCHAR(50))+''''+' AND C.CampaignStatus=1 AND C.Deleted=0 AND M.Deleted=0 '
	
	
	IF (@StartDate IS NOT NULL)
		SET @SQLCriteria=@SQLCriteria+'AND CAST(DATEADD(HH,8,CS.CreatedDate) AS DATE)>= '+ ''''+CAST(CAST(@StartDate AS DATE) AS NVARCHAR(20))+'''' 
	
	IF (@EndDate IS NOT NULL)	
		SET @SQLCriteria=@SQLCriteria+' AND CAST(DATEADD(HH,8,CS.CreatedDate) AS DATE)<= '+ ''''+CAST(CAST(@EndDate AS DATE) AS NVARCHAR(20))+'''' 

	IF (@CampaignId IS NOT NULL)	
		SET @SQLCriteria=@SQLCriteria+' AND C.Id= '+ CAST(@CampaignId AS NVARCHAR(20))

	SET @SQLPart2='ORDER BY C.CreatedDate, CO.CreatedDate'

	/*EmailsSent*/
	SET @SQL=@SQLSelect+@SQLPart1+@SQLCriteria+@SQLPart2

	--PRINT @SQL
	
	INSERT INTO @TotalOpens(CampaignId,CampaignName,MessageId,JobId,JobStatus,CampaignCreatedDate,ContactId,FirstName, LastName, EmailAddress, OpenedDate)
	EXEC(@SQL)

	SELECT *
	FROM @TotalOpens

END



--EXEC S_Campaigns_TotalOpensListing '6388071F-36BB-4823-A3ED-8770ADAE0F51','1/2/2021','1/6/2021',28
--EXEC S_Campaigns_TotalOpensListing '6388071F-36BB-4823-A3ED-8770ADAE0F51',NULL,NULL,NULL
--EXEC S_Campaigns_TotalOpensListing '6388071F-36BB-4823-A3ED-8770ADAE0F51',NULL,NULL,28






