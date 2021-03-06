USE MarketingAutomationTool
GO

-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: 2-March-2020
-- Description:	Searches for chached objects
-- =============================================

CREATE PROC [dbo].[ENT_CACHE_SEARCH] 
@ACCOUNT_ID VARCHAR(64), 
@USER_ID VARCHAR(64),  
@TYPE TINYINT, 
@REFLECTION_TYPE VARCHAR(MAX), 
@CACHE_KEY VARCHAR(128)

AS  

SELECT * FROM dbo.ENT_CACHE WITH(NOLOCK) 

WHERE 

((@ACCOUNT_ID IS NULL) OR (@ACCOUNT_ID IS NOT NULL AND ACCOUNT_ID = @ACCOUNT_ID)) AND
((@USER_ID IS NULL) OR (@USER_ID IS NOT NULL AND [USER_ID] = @USER_ID )) AND
((@TYPE IS NULL) OR (@TYPE IS NOT NULL AND TYPE = @TYPE )) AND
((@REFLECTION_TYPE IS NULL) OR (@REFLECTION_TYPE IS NOT NULL AND REFLECTION_TYPE= @REFLECTION_TYPE)) AND
((@CACHE_KEY IS NULL) OR (@CACHE_KEY IS NOT NULL AND CACHE_KEY = @CACHE_KEY ))



-- exec ENT_CACHE_SEARCH 