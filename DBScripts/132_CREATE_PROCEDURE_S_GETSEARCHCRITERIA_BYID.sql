SELECT *
FROM ContactSearchFields WITH (NOLOCK)
WHERE SearchId =@SearchID AND AccountId=@AccountId AND CreatedBy=@CreatedBy
ORDER BY ID