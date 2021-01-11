
CREATE VIEW [dbo].[vSearchCampaignMessage]
with schemabinding
AS
SELECT    C.Id, 
		  C.CampaignName,
		  C.CampaignStatus ,
		  M.[Subject], 
		  M.SenderEmail, 
		  M.SenderName, 
		  C.DeletedDate, 
		  C.CreatedDate, 
		  C.CreatedBy, 
		  ISNULL(C.CampaignName, '') + ' ' + ISNULL(M.[Subject], '')    + ' ' + ISNULL(M.SenderEmail, '') + ' ' + ISNULL(M.SenderName, '') AS SEARCH_FIELDS
FROM  dbo.Campaign C WITH (NOLOCK) 
INNER JOIN dbo.[Message] M WITH (NOLOCK)
ON C.AccountId = M.AccountId AND C.MessageId=M.Id

GO


