-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: 08-November-2020
-- Description:	Lists deleted leads 
-- =============================================
ALTER PROCEDURE [dbo].[S_LeadReports_DeletedLeadsListing]
	@FROM DATETIME = NULL,
	@TO DATETIME = NULL,
	@TYPE_ID TINYINT = NULL,
	@STATUS_ID TINYINT = NULL,
	@ACCOUNT_ID UNIQUEIDENTIFIER,
	@TOTALLEADCNT INT OUTPUT
AS
BEGIN

	DECLARE @T_NEWCONTACTS TABLE (FirstName NVARCHAR(100), MiddleName NVARCHAR(100), LastName NVARCHAR(100), MobileNumber VARCHAR(100),EmailAddress NVARCHAR(250), PhoneNumber VARCHAR(100), DeletedDate DATETIME)
	
	DECLARE @CRITERIA AS NVARCHAR(MAX)
	SET @CRITERIA = ''
	
	IF @ACCOUNT_ID IS NULL
		RETURN -60187
	
	/*Create Criteria*/
	SET @CRITERIA = 'c.IsDeleted = 1 AND c.AccountID = ' + '''' + CAST(@ACCOUNT_ID AS VARCHAR(50)) + ''''+ ' AND '
	
	IF (@FROM IS NOT NULL)
		SET @CRITERIA = @CRITERIA + 'DATEADD(HH,8,c.DeletedDate) >= ' + '''' + CAST(CAST(@FROM AS DATE) AS VARCHAR(20)) + ''' AND '

	IF (@TO IS NOT NULL)
		SET @CRITERIA = @CRITERIA + 'DATEADD(HH,8,c.DeletedDate) < ' + '''' + CAST(DATEADD(d, 1, @TO) AS VARCHAR(20)) + ''' AND '

	IF (@TYPE_ID IS NOT NULL)
		SET @CRITERIA = @CRITERIA + 'c.ContactType = ' + '''' + CAST(@TYPE_ID AS VARCHAR(10)) + ''' AND '

	IF (@TYPE_ID=1)
	BEGIN
		IF (@STATUS_ID IS NOT NULL)
			SET @CRITERIA = @CRITERIA + 'c.LeadStatus = ' + '''' + CAST(@STATUS_ID AS VARCHAR(10)) + ''' AND '
	END
	ELSE IF (@TYPE_ID=2)
	BEGIN
		IF (@STATUS_ID IS NOT NULL)
			SET @CRITERIA = @CRITERIA + 'c.ContactStatus = ' + '''' + CAST(@STATUS_ID AS VARCHAR(10)) + ''' AND '
	END
	
	SET @CRITERIA = LEFT(@CRITERIA, LEN(@CRITERIA) - 4)	   
	
	/*Query New Contacts Count*/
	INSERT INTO  @T_NEWCONTACTS (FirstName,MiddleName,LastName,MobileNumber,EmailAddress,PhoneNumber,DeletedDate) 	   
	EXEC('SELECT c.FirstName, c.MiddleName, c.LastName, c.MobileNumber, c.EmailAddress, c.PhoneNumber, DATEADD(HH,8,c.DeletedDate) DeletedDate ' +
	      ' FROM Contact c WHERE ' 
	       + @CRITERIA +
		   ' ORDER BY  DATEADD(HH,8,c.DeletedDate)  DESC')

	SELECT @TOTALLEADCNT=COUNT(*)
	FROM  @T_NEWCONTACTS T2
   
	SELECT *
	FROM  @T_NEWCONTACTS T2
	ORDER BY DeletedDate DESC
	
END



--DECLARE	@FROM DATETIME = '11/01/2019'
--DECLARE	@TO DATETIME = '12/28/2020'
--DECLARE	@TYPE_ID TINYINT = NULL
--DECLARE	@STATUS_ID TINYINT = NULL
--DECLARE	@ACCOUNT_ID UNIQUEIDENTIFIER='6388071F-36BB-4823-A3ED-8770ADAE0F51'
--DECLARE @TOTALLEADCNT INT=0


--EXEC [dbo].[S_LeadReports_DeletedLeadsListing] @FROM,@TO,@TYPE_ID,@STATUS_ID,@ACCOUNT_ID, @TOTALLEADCNT OUTPUT
--SELECT @TOTALLEADCNT