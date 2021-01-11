CREATE PROCEDURE S_Campaign_GetAllSubmittedCampaigns
@AccountID UNIQUEIDENTIFIER
AS
BEGIN
	SELECT C.Id CampaignId, C.CampaignName
	FROM Campaign C WITH (NOLOCK) 
	INNER JOIN [Message] M WITH (NOLOCK) 
	ON C.AccountID=M.AccountId AND C.MessageId=M.ID 
	INNER JOIN Email_Job_Queue JQ WITH (NOLOCK) 
	ON C.Id=JQ.CampaignID 
	INNER JOIN Email_Job_Queue_History JQH WITH (NOLOCK) 
	ON JQ.Id=JQH.JobId
	WHERE C.AccountId=@AccountID AND C.CampaignStatus=1 AND C.Deleted=0 AND M.Deleted=0 
	GROUP BY  C.Id,C.CampaignName, M.Id,JQ.Id
	HAVING MAX(JQH.[Status])=3
	ORDER BY C.CampaignName
END

--EXEC S_Campaign_GetAllSubmittedCampaigns '6388071F-36BB-4823-A3ED-8770ADAE0F51'