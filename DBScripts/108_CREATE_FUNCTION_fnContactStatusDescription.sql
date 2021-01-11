-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: August 4, 2020
-- Description:	Replace with user-friendly description
-- =============================================
CREATE FUNCTION [dbo].[fnContactStatusDescription]
(
	@ContactStatus INT
)
RETURNS NVARCHAR(20)
AS
BEGIN
	DECLARE @ContactStatusDescription AS NVARCHAR(20)=''

	IF (@ContactStatus=1)
	BEGIN
		SET @ContactStatusDescription='Not Confirmed'
	END
	ELSE IF (@ContactStatus=2)
	BEGIN
		SET @ContactStatusDescription='Confirmed'
	END
	ELSE IF (@ContactStatus=3)
	BEGIN
		SET @ContactStatusDescription='Active'
	END
	ELSE IF (@ContactStatus=4)
	BEGIN
		SET @ContactStatusDescription='Inactive'
	END
	

	RETURN @ContactStatusDescription

END
