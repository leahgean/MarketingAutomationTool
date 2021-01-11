CREATE FUNCTION fn_ContactSearchExportFiles_GetFile
(
@AccountID UNIQUEIDENTIFIER,
@SearchID INT,
@FileTimeStamp NVARCHAR(250)
)
RETURNS NVARCHAR(250)
AS
BEGIN

DECLARE @FileName AS NVARCHAR(250)=''

SELECT @FileName=[FileName]
FROM ContactSearchExportFiles WITH (NOLOCK)
WHERE AccountID=@AccountID AND SearchID=@SearchID AND FileTimeStamp=@FileTimeStamp

RETURN @FileName
END