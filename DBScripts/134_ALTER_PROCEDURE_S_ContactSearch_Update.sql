USE [MarketingAutomationTool]
GO
/****** Object:  StoredProcedure [dbo].[S_ContactSearch_Update]    Script Date: 9/6/2020 5:34:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[S_ContactSearch_Update]
@AccountId UNIQUEIDENTIFIER,
@ModifiedBy UNIQUEIDENTIFIER,
@SearchJsonString NVARCHAR(MAX),
@Id INT
AS
BEGIN

	UPDATE ContactSearch
	SET SearchJsonString=@SearchJsonString,
		MODIFIEDBY=@ModifiedBy,
		MODIFIEDDATE=GETDATE()
	WHERE Id=@Id AND AccountId=@AccountId

END