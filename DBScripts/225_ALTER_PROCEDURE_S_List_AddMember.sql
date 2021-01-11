ALTER PROCEDURE [dbo].[S_List_AddMember]
@SearchID INT,
@AccountId UNIQUEIDENTIFIER,
@CreatedBy UNIQUEIDENTIFIER,
@ExcludeList NVARCHAR(MAX),
@ContactListID INT
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
DECLARE @UsesContactList BIT=0

INSERT INTO @ContactSearch(SEARCHKEY,SEARCHOPERATOR,SEARCHVALUE,SEARCHLOGICALOPERATOR)
SELECT SEARCHKEY,SEARCHOPERATOR,SEARCHVALUE,SEARCHLOGICALOPERATOR
FROM ContactSearchFields 
WHERE SearchId=@SearchID 
ORDER BY ID

SET @ID=1

SELECT @LASTID=MAX(ID) FROM @ContactSearch

IF (@ExcludeList IS NOT NULL)
BEGIN

SELECT @SQL='IF OBJECT_ID('+''''+'TempDB..'+'##tmp_AddMemberExcludeList_' + CAST(@SearchID AS NVARCHAR(20))+''''+','+''''+'U'+''''+') IS NOT NULL DROP TABLE '+ '##tmp_AddMemberExcludeList_' + CAST(@SearchID AS NVARCHAR(20))

--PRINT(@SQL)
EXEC(@SQL) 


SELECT @SQL='SELECT CAST([Value] AS INT) [Value] ' +
'INTO ##tmp_AddMemberExcludeList_' + CAST(@SearchID AS NVARCHAR(20)) + ' ' +
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
		ELSE IF (@SEARCHKEY='contactlist')
		BEGIN
				IF (@SEARCHVALUE <> '')
				BEGIN
					SET @UsesContactList=1

					IF (@SEARCHVALUE<>'-1')
					BEGIN
						SET @SEARCHPHRASE = @SEARCHPHRASE + dbo.fnColumnName(@SEARCHKEY) + ' ' + @SEARCHOPERATOR + ' ' + @SEARCHVALUE
					END
					ELSE
					BEGIN
						--Not in lists
						SET @SEARCHPHRASE = @SEARCHPHRASE + dbo.fnColumnName(@SEARCHKEY) + ' IS NULL'
					END
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
	SET @SEARCHCRITERIA = '('+ @SEARCHCRITERIA + ')' + ' AND ' 
END

SET @SEARCHCRITERIA = @SEARCHCRITERIA + ' C.AccountID='+''''+CAST(@AccountId AS NVARCHAR(50))+''''

--PRINT @SEARCHCRITERIA
IF (@SEARCHCRITERIA<>'')
BEGIN

	--Create IncludeList table
	SELECT @SQL='IF OBJECT_ID('+''''+'TempDB..'+'##tmp_AddMemberIncludeList_' + CAST(@SearchID AS NVARCHAR(20))+''''+','+''''+'U'+''''+') IS NOT NULL DROP TABLE '+ '##tmp_AddMemberIncludeList_' + CAST(@SearchID AS NVARCHAR(20))
	--PRINT(@SQL)
	EXEC(@SQL) 

	SELECT @SQL='CREATE TABLE ##tmp_AddMemberIncludeList_' + CAST(@SearchID AS NVARCHAR(20))+'(ContactListID INT, ContactId UNIQUEIDENTIFIER, CreatedBy UNIQUEIDENTIFIER, AccountID UNIQUEIDENTIFIER)'
	--PRINT(@SQL)
	EXEC(@SQL) 

	SET @MAINSQL = 'INSERT INTO ##tmp_AddMemberIncludeList_' + CAST(@SearchID AS NVARCHAR(20))+'(ContactListID, ContactID, CreatedBy, AccountID) '
	SET @MAINSQL= @MAINSQL+'SELECT '+CAST(@ContactListID AS NVARCHAR(20))+' ContactListID, C.ContactID , '+''''+CAST(@CreatedBy AS NVARCHAR(50))+''''+' CreatedBy,'+''''+CAST(@AccountId AS NVARCHAR(50))+''''+' AccountId FROM Contact C WITH (NOLOCK) '
	IF (@ExcludeList IS NOT NULL)
	BEGIN
		SET @MAINSQL= @MAINSQL + 'LEFT JOIN ' + '##tmp_AddMemberExcludeList_' + CAST(@SearchID AS NVARCHAR(20)) + ' X '
		SET @MAINSQL= @MAINSQL + 'ON C.[ID]=X.[Value] ' 	
	END

	IF (@UsesContactList=1)
	BEGIN
		SET @MAINSQL= @MAINSQL + 'LEFT JOIN ContactListContacts CLC '
		SET @MAINSQL= @MAINSQL + 'ON C.ContactID=CLC.ContactID AND C.AccountID=CLC.AccountId ' 

	END

	SET @MAINSQL= @MAINSQL + 'WHERE ' + @SEARCHCRITERIA

	IF (@ExcludeList IS NOT NULL)
	BEGIN
		SET @MAINSQL= @MAINSQL + ' AND X.[Value] IS NULL'
	END

	--Create IncludeList 
	--PRINT @MAINSQL
	EXEC(@MAINSQL)


	BEGIN TRY
		BEGIN TRAN

		--Update previously added contactlist contacts
		UPDATE ContactListContacts
		SET ModifiedDate=GETDATE(),
			ModifiedBy=@CreatedBy
		WHERE ContactListID=@ContactListID AND AccountID=@AccountID

		--Add to ContactList records in IncludeList not yet in ContactList
		SET @MAINSQL = 'INSERT INTO ContactListContacts(ContactListID, ContactID, CreatedBy, AccountID) '
		SET @MAINSQL= @MAINSQL+'SELECT X.ContactListID, X.ContactID , X.CreatedBy,X.AccountId '
		SET @MAINSQL= @MAINSQL+'FROM ##tmp_AddMemberIncludeList_'+ CAST(@SearchID AS NVARCHAR(20)) +' X '
		SET @MAINSQL= @MAINSQL+'LEFT JOIN ContactListContacts CC WITH (NOLOCK) '
		SET @MAINSQL= @MAINSQL+'ON X.ContactId = CC.ContactID AND CC.ContactListId='+CAST(@ContactListID AS NVARCHAR(20))+ ' '
		SET @MAINSQL= @MAINSQL+'WHERE CC.ID IS NULL '

		--PRINT @MAINSQL
		EXEC(@MAINSQL)

		--Update ContactList ModifiedDate
		UPDATE ContactList
		SET ModifiedDate=GETDATE(),
			ModifiedBy=@CreatedBy
		WHERE ID=@ContactListID AND AccountID=@AccountID
		
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		DECLARE @ERROR_MESSAGE NVARCHAR(MAX)=''
		DECLARE @ERROR_SEVERITY NVARCHAR(50)=''
		SELECT @ERROR_MESSAGE=ERROR_MESSAGE()
		SELECT @ERROR_SEVERITY=ERROR_SEVERITY()
		RAISERROR(@ERROR_MESSAGE,@ERROR_SEVERITY,1)

	END CATCH
END


END

--INSERT INTO ContactListContacts(ContactListID, ContactID, CreatedBy, AccountID) 
--SELECT X.ContactListID, X.ContactID , X.CreatedBy,X.AccountId 
--FROM ##tmp_AddMemberIncludeList_1303 X 
--LEFT JOIN ContactListContacts CC WITH (NOLOCK) 
--ON X.ContactId = CC.ContactID 
--AND CC.ContactListId=4 
--WHERE CC.ID IS NULL 

