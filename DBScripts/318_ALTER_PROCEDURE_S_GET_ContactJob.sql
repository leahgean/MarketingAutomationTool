---- =============================================
---- Author:		Leah Gean Diopenes
---- Create date: 18-April-2020
---- Description:	Get ContactJob

---- =============================================

ALTER PROCEDURE [dbo].[S_GET_ContactJob]
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
      ,DATEADD(HH,8,DateCreated) DateCreated
      ,DATEADD(HH,8,JobStarted) JobStarted
      ,DATEADD(HH,8,JobFinished) JobFinished
      ,Error
      ,TotalContacts
      ,UploadedContacts
      ,CurrentRowParsedInExcel
FROM ContactJob
WHERE JobId=@JobId AND AccountID=@AccountID

END