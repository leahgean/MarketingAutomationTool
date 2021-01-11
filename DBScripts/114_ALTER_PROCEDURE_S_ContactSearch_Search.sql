ALTER PROCEDURE [dbo].[S_ContactSearch_Search]
@SearchID INT,
@AccountId UNIQUEIDENTIFIER,
@CreatedBy UNIQUEIDENTIFIER,
@PageNum INT,
@MaxRows INT,
@SortBy VARCHAR(50),
@SortDirection VARCHAR(10),
@MinItem INT OUT,
@MaxItem INT OUT,
@TotalRows INT OUT
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
	(@SEARCHKEY='middlename') OR
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
			SET @SEARCHPHRASE = @SEARCHPHRASE + dbo.fnColumnName(@SEARCHKEY) + ' ' + @SEARCHOPERATOR + ' '''+'%'+@SEARCHVALUE+'%'+''''


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
			SET @SEARCHPHRASE = @SEARCHPHRASE+ dbo.fnColumnName(@SEARCHKEY) + ' ' + @SEARCHOPERATOR 

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
			SET @SEARCHPHRASE = @SEARCHPHRASE + dbo.fnColumnName(@SEARCHKEY) + ' ' + @SEARCHOPERATOR + ' '''+@SEARCHVALUE+''''

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
			SET @SEARCHPHRASE = @SEARCHPHRASE + dbo.fnColumnName(@SEARCHKEY) + ' ' + @SEARCHOPERATOR 

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

			IF (@SEARCHOPERATOR='LIKE') 
			BEGIN
				SET @SEARCHOPERATOR='='
			END
			ELSE IF (@SEARCHOPERATOR='NOT LIKE')
			BEGIN
				SET @SEARCHOPERATOR='<>'
			END

			SET @SEARCHPHRASE = @SEARCHPHRASE + dbo.fnColumnName(@SEARCHKEY) + ' ' + @SEARCHOPERATOR + ' ' + @SEARCHVALUE

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

--PRINT @SEARCHCRITERIA
IF (@SEARCHCRITERIA<>'')
BEGIN
SET @SEARCHCRITERIA = @SEARCHCRITERIA + ' AND ' 
END

SET @SEARCHCRITERIA = @SEARCHCRITERIA + ' AccountID='+''''+CAST(@AccountId AS NVARCHAR(50))+''' AND IsDeleted=0'

IF (@SEARCHCRITERIA<>'')
BEGIN

	DECLARE @StartPage INT =1
	DECLARE @EndPage INT = ((1 + @MaxRows)-1)
	
	IF @PageNum = 1
	BEGIN
		SET @StartPage  =1
	END
	ELSE
	BEGIN
		SET @StartPage = ((@PageNum-1) * @MaxRows) +1
	END

	SET @EndPage = ((@StartPage + @MaxRows)-1)

	SET @MAINSQL='SELECT dbo.fn_Lead_PadID(ID) ID, ContactID, AccountID, FirstName, MiddleName, LastName,MobileNumber, EmailAddress,FacebookID, ROW_NUMBER ( )   OVER ( ORDER BY '+ @SortBy + ' ' + @SortDirection + ' )  RowNum FROM Contact WITH (NOLOCK) WHERE ' + @SEARCHCRITERIA
	--PRINT '@MAINSQL=' + @MAINSQL


	SET @SQL = 'SELECT @COUNT=COUNT(*) FROM (' + @MAINSQL + ') X'

	EXEC SP_EXECUTESQL @SQL, N'@COUNT INT OUTPUT', @TotalRows OUTPUT
	--PRINT '@TotalRows=' + CAST(@TotalRows AS NVARCHAR(5))

	SET @SQL = 'SELECT * ' +
	'FROM '+
	'( ' +
		@MAINSQL +
	') sub ' +
	'WHERE RowNum >= ' + CAST(@StartPage AS NVARCHAR(10)) + ' AND RowNum <= ' + CAST(@EndPage AS NVARCHAR(10)) 


	SET @MinItem = @StartPage

	IF @EndPage > @TotalRows
	BEGIN
		SET @MaxItem = @TotalRows
	END
	ELSE
	BEGIN
		SET @MaxItem = @EndPage
	END

	--PRINT '@SQL=' + @SQL

	EXEC(@SQL)

END


END