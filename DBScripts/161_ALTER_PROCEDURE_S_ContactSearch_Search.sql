ALTER PROCEDURE [dbo].[S_ContactSearch_Search]
@SearchID INT,
@AccountId UNIQUEIDENTIFIER,
@CreatedBy UNIQUEIDENTIFIER,
@MaxRows INT,
@SortBy VARCHAR(50),
@SortDirection VARCHAR(10),
@ExcludeList NVARCHAR(MAX),
@PageNum INT,
@NewPageNum INT OUTPUT,
@MinItem INT OUTPUT,
@MaxItem INT OUTPUT,
@TotalRows INT OUTPUT,
@MaxPages DECIMAL OUTPUT
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

IF (@ExcludeList IS NOT NULL)
BEGIN

SELECT @SQL='IF OBJECT_ID('+''''+'TempDB..'+'##tmp_ContactSearch_' + CAST(@SearchID AS NVARCHAR(20))+''''+','+''''+'U'+''''+') IS NOT NULL DROP TABLE '+ '##tmp_ContactSearch_' + CAST(@SearchID AS NVARCHAR(20))

--PRINT(@SQL)
EXEC(@SQL) 

SELECT @SQL='SELECT CAST([Value] AS INT) [Value] ' +
'INTO ##tmp_ContactSearch_' + CAST(@SearchID AS NVARCHAR(20)) + ' ' +
'FROM dbo.Split('+'''' + @ExcludeList +''''+',''' +',' +''''+')'

--PRINT(@SQL)
EXEC(@SQL) 

END


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

	IF (@SEARCHKEY<>'ALL')
	BEGIN
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
		(@SEARCHKEY='subscribedtoemail') OR
		(@SEARCHKEY='isdeleted') OR
		(@SEARCHKEY='usefortesting') 
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

				IF (@SEARCHVALUE <> '')
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
				ELSE
				BEGIN
					SET @SEARCHPHRASE = '' --clear the open parenthesis, fields not included if blank
				END
				

			END

		END

		SET @SEARCHCRITERIA = @SEARCHCRITERIA + @SEARCHPHRASE
	END

	SET @ID = @ID+1

END

--PRINT @SEARCHCRITERIA
IF (@SEARCHCRITERIA<>'')
BEGIN
	--REMOVE EXTRA LOGICAL OPERATOR
	SET @SEARCHCRITERIA = RTRIM(LTRIM(@SEARCHCRITERIA))

	IF (SUBSTRING(@SEARCHCRITERIA,LEN(@SEARCHCRITERIA)-2,3)='AND')
	BEGIN
		SET @SEARCHCRITERIA=SUBSTRING(@SEARCHCRITERIA,1,LEN(@SEARCHCRITERIA)-4)
	END
	ELSE IF (SUBSTRING(@SEARCHCRITERIA,LEN(@SEARCHCRITERIA)-2,2)='OR')
	BEGIN
		SET @SEARCHCRITERIA=SUBSTRING(@SEARCHCRITERIA,1,LEN(@SEARCHCRITERIA)-3)
	END

	--ADD AND to filter by account id
	SET @SEARCHCRITERIA ='('+ @SEARCHCRITERIA+')' + ' AND ' 
END

--PRINT @SEARCHCRITERIA

SET @SEARCHCRITERIA = @SEARCHCRITERIA + ' C.AccountID='+''''+CAST(@AccountId AS NVARCHAR(50))+''''
--PRINT @SEARCHCRITERIA

IF (@SEARCHCRITERIA<>'')
BEGIN

	SET @MAINSQL='SELECT dbo.fn_Lead_PadID(C.ID) ID, C.ContactID, C.AccountID, C.FirstName, C.MiddleName, C.LastName, C.MobileNumber, C.EmailAddress, C.FacebookID, C.IsDeleted,CAST(ISNULL(C.IsDeleted,0) AS INT) IsDeletedNum, ROW_NUMBER () OVER ( ORDER BY '+ dbo.fnColumnName(@SortBy) + ' ' + @SortDirection + ' )  RowNum FROM Contact C WITH (NOLOCK) '
	IF (@ExcludeList IS NOT NULL)
	BEGIN
		SET @MAINSQL= @MAINSQL + 'LEFT JOIN ' + '##tmp_ContactSearch_' + CAST(@SearchID AS NVARCHAR(20)) + ' X '
		SET @MAINSQL= @MAINSQL + 'ON C.[ID]=X.[Value] ' 
	END
	SET @MAINSQL= @MAINSQL + 'WHERE ' + @SEARCHCRITERIA

	IF (@ExcludeList IS NOT NULL)
	BEGIN
		SET @MAINSQL= @MAINSQL + ' AND X.[Value] IS NULL'

	END
	--PRINT '@MAINSQL=' + @MAINSQL


	SET @SQL = 'SELECT @COUNT=COUNT(*) FROM (' + @MAINSQL + ') sub'

	EXEC SP_EXECUTESQL @SQL, N'@COUNT INT OUTPUT', @TotalRows OUTPUT
	--PRINT '@TotalRows=' + CAST(@TotalRows AS NVARCHAR(5))

	SET @MaxPages=CEILING(CAST(@TotalRows AS DECIMAL)/CAST(@MaxRows AS DECIMAL)) 
	--PRINT '@MaxPages=' + CAST(@MaxPages AS NVARCHAR(5))

	IF (@PageNum> @MaxPages)
	BEGIN
		SET @PageNum=@PageNum-1

		IF (@PageNum =0)
		BEGIN
			SET @PageNum=1
		END
	END
	
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
	
	SET @SQL = 'SELECT * ' +
	'FROM '+
	'( ' +
		@MAINSQL +
	') sub ' +
	'WHERE RowNum >= ' + CAST(@StartPage AS NVARCHAR(10)) + ' AND RowNum <= ' + CAST(@EndPage AS NVARCHAR(10)) 

	SET @NewPageNum=@PageNum
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
