-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: August 4, 2020
-- Description:	Replace with user-friendly description
-- =============================================
CREATE FUNCTION [dbo].[fnContactTypeDescription]
(
	@ContactType INT
)
RETURNS NVARCHAR(20)
AS
BEGIN
	DECLARE @ContactTypeDescription AS NVARCHAR(20)=''

	IF (@ContactType=1)
	BEGIN
		SET @ContactTypeDescription='Lead'
	END
	ELSE IF (@ContactType=2)
	BEGIN
		SET @ContactTypeDescription='Contact'
	END
	

	RETURN @ContactTypeDescription

END
