CREATE PROCEDURE S_List_ModifyMembers
@ContactListID INT,
@AccountID UNIQUEIDENTIFIER,
@ModifiedBy UNIQUEIDENTIFIER,
@RemoveList NVARCHAR(MAX)
AS
BEGIN


	BEGIN TRY  
		BEGIN TRAN
			DECLARE @SQL AS NVARCHAR(MAX)=''

			IF (@RemoveList IS NOT NULL)
			BEGIN
				SELECT @SQL='IF OBJECT_ID('+''''+'TempDB..'+'##tmp_MLM_RemoveList_' + CAST(@ContactListID AS NVARCHAR(20))+''''+','+''''+'U'+''''+') IS NOT NULL DROP TABLE '+ '##tmp_MLM_RemoveList_' + CAST(@ContactListID AS NVARCHAR(20))
				--PRINT(@SQL)
				EXEC(@SQL) 

				SELECT @SQL='SELECT CAST([Value] AS INT) [Value] ' +
							'INTO ##tmp_MLM_RemoveList_' + CAST(@ContactListID AS NVARCHAR(20)) + ' ' +
							'FROM dbo.Split('+'''' + @RemoveList +''''+',''' +',' +''''+')'
				--PRINT(@SQL)
				EXEC(@SQL)
			END

			SELECT @SQL='DELETE CC '+
						'FROM ContactListContacts CC '+
						'INNER JOIN Contact C WITH (NOLOCK) ' +
						'ON CC.ContactID=C.ContactID AND CC.AccountId=C.AccountID '+
						'INNER JOIN ##tmp_MLM_RemoveList_' + CAST(@ContactListID AS NVARCHAR(50)) + ' X '+
						'ON C.Id=X.[Value] '+
						'WHERE CC.ContactListID='+CAST(@ContactListID AS NVARCHAR(50))+' AND C.AccountID='+''''+CAST(@AccountID AS NVARCHAR(50))+''''

			--PRINT(@SQL)
			EXEC(@SQL)

			UPDATE ContactListContacts
			SET ModifiedDate=GETDATE(),
				ModifiedBy=@ModifiedBy
			WHERE ContactListID=@ContactListID AND AccountID=@AccountID

			UPDATE ContactList
			SET ModifiedDate=GETDATE(),
				ModifiedBy=@ModifiedBy
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