USE [MarketingAutomationTool]
GO
/****** Object:  StoredProcedure [dbo].[S_ContactSearch_VisibleCriteria]    Script Date: 8/4/2020 6:58:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[S_ContactSearch_VisibleCriteria]
@SearchID INT
AS
BEGIN

DECLARE @ContactSearch TABLE(ID INT IDENTITY,
SEARCHKEY NVARCHAR(250) NOT NULL,
SEARCHOPERATOR NVARCHAR(20) NOT NULL,
SEARCHVALUE NVARCHAR(MAX) NOT NULL,
SEARCHLOGICALOPERATOR NVARCHAR(5) NOT NULL)

DECLARE @ID AS INT=0
DECLARE @LASTID AS INT=0
DECLARE @PREVSEARCHKEY NVARCHAR(250)=''
DECLARE @NEXTSEARCHKEY NVARCHAR(250)=''
DECLARE @SEARCHKEY NVARCHAR(250)=''
DECLARE @SEARCHOPERATOR NVARCHAR(20)=''
DECLARE @SEARCHVALUE NVARCHAR(MAX)=''
DECLARE @SEARCHLOGICALOPERATOR NVARCHAR(5)=''
DECLARE @SEARCHPHRASE NVARCHAR(MAX)=''
DECLARE @SEARCHCRITERIA NVARCHAR(MAX)=''
DECLARE @SQL NVARCHAR(MAX)=''
DECLARE @MAINSQL NVARCHAR(MAX)=''

INSERT INTO @ContactSearch(SEARCHKEY,SEARCHOPERATOR,SEARCHVALUE,SEARCHLOGICALOPERATOR)
SELECT SEARCHKEY,SEARCHOPERATOR,SEARCHVALUE,SEARCHLOGICALOPERATOR
FROM ContactSearchFields 
WHERE SearchId=@SearchID 
ORDER BY ID

SET @ID=1

SELECT @LASTID=MAX(ID) FROM @ContactSearch


WHILE EXISTS(SELECT SEARCHKEY, SEARCHOPERATOR, SEARCHVALUE,SEARCHLOGICALOPERATOR FROM @ContactSearch WHERE ID=@ID)
BEGIN

	SET @SEARCHPHRASE=''
	
	SELECT 
	@SEARCHKEY=SEARCHKEY,
	@SEARCHOPERATOR=SEARCHOPERATOR,
	@SEARCHVALUE=SEARCHVALUE,
	@SEARCHLOGICALOPERATOR=SEARCHLOGICALOPERATOR
	FROM @ContactSearch
	WHERE ID =@ID


	IF (@ID =1)
	BEGIN
		SET @SEARCHPHRASE='('
	END
	ELSE
	BEGIN
		SELECT @PREVSEARCHKEY=SEARCHKEY FROM @ContactSearch WHERE ID=@ID-1
		IF (@PREVSEARCHKEY<>@SEARCHKEY)
			SET @SEARCHPHRASE='('
	END

	IF (@SEARCHKEY='firstname') OR
	(@SEARCHKEY='lastname') OR
	(@SEARCHKEY='email') OR
	(@SEARCHKEY='companyname') OR
	(@SEARCHKEY='position') OR
	(@SEARCHKEY='website') OR
	(@SEARCHKEY='mobile') OR
	(@SEARCHKEY='phoneno') OR
	(@SEARCHKEY='address') OR
	(@SEARCHKEY='city') OR
	(@SEARCHKEY='state') OR
	(@SEARCHKEY='gender') 
	BEGIN
		IF ((@SEARCHOPERATOR='LIKE') OR (@SEARCHOPERATOR='NOT LIKE'))
		BEGIN
			SET @SEARCHPHRASE = @SEARCHPHRASE+ dbo.fnDisplayName(@SEARCHKEY) + ' ' + @SEARCHOPERATOR + ' '''+''+@SEARCHVALUE+''+''''


			IF (@ID=@LASTID)
			BEGIN
				SET @SEARCHPHRASE=@SEARCHPHRASE+')'
			END
			ELSE
			BEGIN
				SELECT @NEXTSEARCHKEY=SEARCHKEY FROM @ContactSearch WHERE ID=@ID+1
				IF (@NEXTSEARCHKEY<>@SEARCHKEY) SET @SEARCHPHRASE=@SEARCHPHRASE+')'
			END

			IF (@ID < @LASTID)
			BEGIN
				SET @SEARCHPHRASE = @SEARCHPHRASE + ' ' + @SEARCHLOGICALOPERATOR + ' '
			END
		END
		ELSE IF ((@SEARCHOPERATOR='IS NULL') OR (@SEARCHOPERATOR='IS NOT NULL'))
		BEGIN
			SET @SEARCHPHRASE = @SEARCHPHRASE+ dbo.fnDisplayName(@SEARCHKEY) + ' ' + @SEARCHOPERATOR 

			IF (@ID=@LASTID)
			BEGIN
				SET @SEARCHPHRASE=@SEARCHPHRASE+')'
			END
			ELSE
			BEGIN
				SELECT @NEXTSEARCHKEY=SEARCHKEY FROM @ContactSearch WHERE ID=@ID+1
				IF (@NEXTSEARCHKEY<>@SEARCHKEY) SET @SEARCHPHRASE=@SEARCHPHRASE+')'
			END

			IF (@ID < @LASTID)
			BEGIN
				SET @SEARCHPHRASE = @SEARCHPHRASE + ' ' + @SEARCHLOGICALOPERATOR + ' '
			END
		END
		ELSE
		BEGIN
			SET @SEARCHPHRASE = @SEARCHPHRASE + dbo.fnDisplayName(@SEARCHKEY) + ' ' + @SEARCHOPERATOR + ' '''+@SEARCHVALUE+''''

			IF (@ID=@LASTID)
			BEGIN
				SET @SEARCHPHRASE=@SEARCHPHRASE+')'
			END
			ELSE
			BEGIN
				SELECT @NEXTSEARCHKEY=SEARCHKEY FROM @ContactSearch WHERE ID=@ID+1
				IF (@NEXTSEARCHKEY<>@SEARCHKEY) SET @SEARCHPHRASE=@SEARCHPHRASE+')'
			END

			IF (@ID < @LASTID)
			BEGIN
				SET @SEARCHPHRASE = @SEARCHPHRASE + ' ' + @SEARCHLOGICALOPERATOR + ' '
			END
		END
	END
	ELSE IF (@SEARCHKEY='country') OR
	(@SEARCHKEY='contacttype') OR
	(@SEARCHKEY='contactstatus') OR
	(@SEARCHKEY='subscribedtoemail') 
	BEGIN

		IF ((@SEARCHOPERATOR='IS NULL') OR (@SEARCHOPERATOR='IS NOT NULL'))
		BEGIN
			SET @SEARCHPHRASE = @SEARCHPHRASE +dbo.fnDisplayName(@SEARCHKEY) + ' ' + @SEARCHOPERATOR 

			IF (@ID=@LASTID)
			BEGIN
				SET @SEARCHPHRASE=@SEARCHPHRASE+')'
			END
			ELSE
			BEGIN
				SELECT @NEXTSEARCHKEY=SEARCHKEY FROM @ContactSearch WHERE ID=@ID+1
				IF (@NEXTSEARCHKEY<>@SEARCHKEY) SET @SEARCHPHRASE=@SEARCHPHRASE+')'
			END


			IF (@ID < @LASTID)
			BEGIN
				SET @SEARCHPHRASE = @SEARCHPHRASE + ' ' + @SEARCHLOGICALOPERATOR + ' '
			END
		END
		ELSE
		BEGIN

			IF (@SEARCHKEY='country')
			BEGIN
				SET @SEARCHVALUE=''''+dbo.fn_Country_GetCountryName(CAST(@SEARCHVALUE AS INT))+''''
			END
			ELSE IF(@SEARCHKEY='contacttype')
			BEGIN
				SET @SEARCHVALUE=''''+dbo.fnContactTypeDescription(CAST(@SEARCHVALUE AS INT))+''''

			END
			ELSE IF (@SEARCHKEY='contactstatus') 
			BEGIN
				SET @SEARCHVALUE=''''+dbo.fnContactStatusDescription(CAST(@SEARCHVALUE AS INT))+''''
			END
			ELSE IF (@SEARCHKEY='subscribedtoemail')
			BEGIN
				SET @SEARCHVALUE=dbo.fnTrueOrFalseText(CAST(@SEARCHVALUE AS INT))
			END

			SET @SEARCHPHRASE = @SEARCHPHRASE + dbo.fnDisplayName(@SEARCHKEY) + ' ' + @SEARCHOPERATOR + ' ' +  @SEARCHVALUE 

			IF (@ID=@LASTID)
			BEGIN
				SET @SEARCHPHRASE=@SEARCHPHRASE+')'
			END
			ELSE
			BEGIN
				SELECT @NEXTSEARCHKEY=SEARCHKEY FROM @ContactSearch WHERE ID=@ID+1
				IF (@NEXTSEARCHKEY<>@SEARCHKEY) SET @SEARCHPHRASE=@SEARCHPHRASE+')'
			END

			IF (@ID < @LASTID)
			BEGIN
				SET @SEARCHPHRASE = @SEARCHPHRASE + ' ' + @SEARCHLOGICALOPERATOR + ' '
			END
		END

	END

	SET @SEARCHCRITERIA = @SEARCHCRITERIA + @SEARCHPHRASE

	SET @ID = @ID+1
END

PRINT @SEARCHCRITERIA
IF (@SEARCHCRITERIA<>'')
BEGIN
SET @SEARCHCRITERIA = @SEARCHCRITERIA + ' AND' 
END

SET @SEARCHCRITERIA = @SEARCHCRITERIA + ' IsDeleted=False'

PRINT @SEARCHCRITERIA


END