CREATE PROCEDURE S_CONTACTSEARCH_GETEXCLUDELIST
@AccountID UNIQUEIDENTIFIER,
@ExcludeList NVARCHAR(MAX)
AS
BEGIN

	DECLARE @ExcludeListIds TABLE ([ID] VARCHAR(8000))

	INSERT INTO @ExcludeListIds(ID)
	SELECT [Value]
	FROM dbo.Split(@ExcludeList,',')
	
	SELECT *
	FROM @ExcludeListIds E 
	INNER JOIN Contact C WITH (NOLOCK)
	ON E.ID=C.ID
	WHERE C.AccountID =@AccountID

END

--EXEC S_CONTACTSEARCH_GETEXCLUDELIST '6388071f-36bb-4823-a3ed-8770adae0f51', '000357,000154,000363,000513,000350'

