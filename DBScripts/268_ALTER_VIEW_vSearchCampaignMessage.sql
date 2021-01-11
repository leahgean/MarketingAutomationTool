ALTER VIEW [dbo].[vSearchCampaignMessage]
with schemabinding
AS
SELECT    
		  C.Id,
		  C.CampaignUID,
		  C.AccountId,
		  C.CampaignName,
		  C.CampaignStatus,
		  M.[Subject], 
		  M.SenderEmail, 
		  M.SenderName, 
		  C.Deleted,
		  C.DeletedDate,
		  C.HideInSearch,
		  C.CreatedDate, 
		  C.CreatedBy, 
		  ISNULL(C.CampaignName, '') + ' ' + ISNULL(M.[Subject], '')    + ' ' + ISNULL(M.SenderEmail, '') + ' ' + ISNULL(M.SenderName, '') AS SEARCH_FIELDS
FROM  dbo.Campaign C 
INNER JOIN dbo.[Message] M 
ON C.AccountId = M.AccountId AND C.MessageId=M.Id

GO


