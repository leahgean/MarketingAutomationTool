CREATE FULLTEXT INDEX ON [dbo].[vSearchCampaignMessage]  
    (CampaignName,   
    [Subject],
	SenderEmail,
	SenderName,
	SEARCH_FIELDS)
    KEY INDEX [PK_vSearchCampaignMessage]  
    ON [FullTextCatalog];  
GO 