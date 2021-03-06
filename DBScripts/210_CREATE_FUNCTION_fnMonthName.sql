-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: August 4, 2020
-- Description:	Replace with actual column name
-- =============================================
CREATE FUNCTION [dbo].[fnMonthName]
(
	@MonthNum INT
)
RETURNS NVARCHAR(250)
AS
BEGIN
	DECLARE @MonthName AS VARCHAR(20)=''

	IF (@MonthNum=1)
		RETURN 'January'
	ELSE IF (@MonthNum=2)
		RETURN 'February'
	ELSE IF (@MonthNum=3)
		RETURN 'March'
	ELSE IF (@MonthNum=4)
		RETURN 'April'
	ELSE IF (@MonthNum=5)
		RETURN 'May'
	ELSE IF (@MonthNum=6)
		RETURN 'June'
	ELSE IF (@MonthNum=7)
		RETURN 'July'
	ELSE IF (@MonthNum=8)
		RETURN 'August'
	ELSE IF (@MonthNum=9)
		RETURN 'September'
	ELSE IF (@MonthNum=10)
		RETURN 'October'
	ELSE IF (@MonthNum=11)
		RETURN 'November'
	ELSE IF (@MonthNum=12)
		RETURN 'December'
	
	RETURN @MonthName

END
