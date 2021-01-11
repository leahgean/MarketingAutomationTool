CREATE PROCEDURE S_ContactSearchExportFiles_Insert
(
@SearchID INT,
@AccountID UNIQUEIDENTIFIER,
@CreatedBy UNIQUEIDENTIFIER,
@FileTimeStamp NVARCHAR(250),
@FileName NVARCHAR(250)
)
AS
BEGIN

	INSERT INTO ContactSearchExportFiles(SearchID,AccountID,CreatedBy,FileTimeStamp,[FileName])
	VALUES(@SearchID,@AccountID,@CreatedBy,@FileTimeStamp,@FileName)
END