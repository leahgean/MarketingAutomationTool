---- =============================================
---- Author:		Leah Gean Diopenes
---- Create date: 18-April-2020
---- Description:	Get ContactJob

---- =============================================

CREATE PROCEDURE S_GET_ContactJob
@JobId INT,
@AccountID UNIQUEIDENTIFIER
AS
BEGIN
SELECT JobId
      ,JobName
      ,JobStatusId
      ,AccountID
      ,CreatedBy
      ,FileFormat
      ,OriginalFileName
      ,[FileName]
      ,ContactListId
      ,DateCreated
      ,JobStarted
      ,JobFinished
      ,Error
      ,TotalContacts
      ,UploadedContacts
      ,CurrentRowParsedInExcel
FROM ContactJob
WHERE JobId=@JobId AND AccountID=@AccountID

END