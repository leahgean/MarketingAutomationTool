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
C.CreatedDate [Created],
C.CreatedDate [Sent], --Replace with value in Job
C.CreatedDate [Finished]
FROM Campaign C WITH (NOLOCK)
INNER JOIN [User] U WITH (NOLOCK)
ON C.AccountID=U.AccountID AND C.CreatedBy=U.UserId
INNER JOIN ContactSearch CS WITH (NOLOCK)
ON C.AccountID=CS.ACCOUNTID AND C.SearchID=CS.Id
WHERE C.AccountId=@AccountId AND C.Deleted=0 AND C.HideInSearch=0
ORDER BY Id DESC
END