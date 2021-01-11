USE [AllMessages_Tracking]
GO
/****** Object:  UserDefinedFunction [dbo].[fnSplit]    Script Date: 2/10/2020 1:39:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fnSplit](
    @sInputList VARCHAR(8000) -- List of delimited items
  , @sDelimiter VARCHAR(8000) = ',' -- delimiter that separates items
) RETURNS @List TABLE (item VARCHAR(8000), SORT_ORDER int)

BEGIN
DECLARE @sItem VARCHAR(8000), @i int
select @i=0
WHILE CHARINDEX(@sDelimiter,@sInputList,0) <> 0
 BEGIN
	 SELECT
	  @sItem=RTRIM(LTRIM(SUBSTRING(@sInputList,1,CHARINDEX(@sDelimiter,@sInputList,0)-1))),
	  @sInputList=RTRIM(LTRIM(SUBSTRING(@sInputList,CHARINDEX(@sDelimiter,@sInputList,0)+LEN(@sDelimiter),LEN(@sInputList))))
	 
	 IF LEN(@sItem) > 0
		BEGIN
			select @i=@i+1 
			INSERT INTO @List SELECT @sItem, @i
		END
 END

IF LEN(@sInputList) > 0
		BEGIN
			select @i=@i+1 
            INSERT INTO @List SELECT @sInputList, @i  -- Put the last item in
		END

RETURN
END

use AllMessages_Tracking
SELECT *  
FROM [dbo].[fnSplit]('','.')

SELECT AllMessages_Tracking.dbo.Dot2LongIP('192.168.1.10')

SELECT * FROM SYS_ip2location_db3 with (nolock)   where 3232235786>=ip_from AND 3232235786<=ip_to