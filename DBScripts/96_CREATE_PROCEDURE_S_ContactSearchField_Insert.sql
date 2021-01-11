CREATE PROCEDURE S_ContactSearchField_Insert
@SearchID INT,
@AccountId UNIQUEIDENTIFIER,
@CreatedBy UNIQUEIDENTIFIER,
@SearchKey NVARCHAR(250),
@SearchOperator NVARCHAR(20),
@SearchValue NVARCHAR(MAX),
@SearchLogicalOperator NVARCHAR(5)

AS
BEGIN



INSERT INTO ContactSearchFields(SearchId,AccountID, CreatedBy, SearchKey, SearchOperator,SearchValue,SearchLogicalOperator)
VALUES(@SearchId,@AccountId,@CreatedBy,@SearchKey,@SearchOperator,@SearchValue,@SearchLogicalOperator)


END


