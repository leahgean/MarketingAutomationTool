CREATE FUNCTION [dbo].[fn_ContactSearch_GetSearchJsonString]
(
@SearchID INT,
@AccountID UNIQUEIDENTIFIER,
@UserID UNIQUEIDENTIFIER
)
RETURNS NVARCHAR(MAX)
BEGIN

DECLARE @Result AS NVARCHAR(MAX)=''

SELECT @Result=SearchJsonString
FROM ContactSearch WITH (NOLOCK)
WHERE ID=@SearchID AND ACCOUNTID=@AccountID AND CreatedBy=@UserID

RETURN @Result


END