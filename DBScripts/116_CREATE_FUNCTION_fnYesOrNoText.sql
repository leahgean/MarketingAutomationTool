USE [MarketingAutomationTool]
GO
/****** Object:  UserDefinedFunction [dbo].[fnTrueOrFalseText]    Script Date: 8/9/2020 4:34:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: August 9, 2020
-- Description:	Replace with text
-- =============================================
CREATE FUNCTION [dbo].[fnYesOrNoText]
(
	@Value INT
)
RETURNS NVARCHAR(20)
AS
BEGIN
	DECLARE @Text AS NVARCHAR(20)=''

	IF (@Value=0)
	BEGIN
		SET @Text='No'
	END
	ELSE IF (@Value=1)
	BEGIN
		SET @Text='Yes'
	END

	RETURN @Text

END
