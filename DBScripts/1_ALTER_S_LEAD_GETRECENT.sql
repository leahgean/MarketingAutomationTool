USE [MarketingAutomationTool]
GO
/****** Object:  StoredProcedure [dbo].[S_LEAD_GETRECENT]    Script Date: 11/18/2019 1:12:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[S_LEAD_GETRECENT]
@AccountID UNIQUEIDENTIFIER
AS
BEGIN
SELECT  ID ContactID, dbo.fn_Lead_PadID(ID) ContactIDText, ContactID ContactGUID, FirstName, LastName, EmailAddress, MobileNumber
FROM Contact WITH (NOLOCK)
WHERE (CAST(CreatedDate AS DATE) BETWEEN (DATEADD(DAY, -7, CAST(GETDATE() AS DATE))) AND CAST(GETDATE() AS DATE)) AND AccountID=@AccountID
ORDER BY CreatedDate DESC
END


