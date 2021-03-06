ALTER PROCEDURE [dbo].[S_DASHBOARD_GETTOPFIVECAMPAIGNS]
@AccountID UNIQUEIDENTIFIER
AS
BEGIN

SELECT TOP 5 
dbo.fn_Lead_PadID(C.Id) Id,
C.CampaignUID,
CS.SearchUID,
CAST(C.CampaignUID AS VARCHAR(50))+'|'+CAST(CS.SearchUID AS VARCHAR(50)) CommandArg,
CASE C.CampaignStatus WHEN 0 THEN 'Draft' WHEN 1 THEN 'Submitted' END [Status],
C.CampaignName [Name],
U.FirstName +' '+ U.LastName [Owner],
DATEADD(HH,8,C.CreatedDate) [Created],
DATEADD(HH,8,E.datesent) [Sent], 
DATEADD(HH,8,E.datecompleted) [Finished]
FROM Campaign C WITH (NOLOCK)
INNER JOIN [User] U WITH (NOLOCK)
ON C.AccountID=U.AccountID AND C.CreatedBy=U.UserId
INNER JOIN ContactSearch CS WITH (NOLOCK)
ON C.AccountID=CS.ACCOUNTID AND C.SearchID=CS.Id
LEFT JOIN Email_Job_Queue E WITH (NOLOCK)
ON C.Id=E.CampaignId
WHERE C.AccountId=@AccountId AND C.Deleted=0 AND C.HideInSearch=0
ORDER BY Id DESC
END

--exec S_DASHBOARD_GETTOPFIVECAMPAIGNS '6388071F-36BB-4823-A3ED-8770ADAE0F51'