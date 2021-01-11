-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: August 4, 2020
-- Description:	Replace with text
-- =============================================
CREATE FUNCTION [dbo].[fnTrueOrFalseText]
(
	@Value INT
)
RETURNS NVARCHAR(20)
AS
BEGIN
	DECLARE @Text AS NVARCHAR(20)=''

	IF (@Value=0)
	BEGIN
		SET @Text='False'
	END
	ELSE IF (@Value=1)
	BEGIN
		SET @Text='True'
	END

	RETURN @Text

END
