-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: 08-November-2020
-- Description:	Lists leads (not deleted and subscribed)
-- =============================================
CREATE PROCEDURE [dbo].[S_LeadReports_LeadsListing]
	@FROM DATETIME = NULL,
	@TO DATETIME = NULL,
	@SOURCE VARCHAR(50)= NULL,
	@TYPE_ID TINYINT = NULL,
	@STATUS_ID TINYINT = NULL,
	@ACCOUNT_ID UNIQUEIDENTIFIER
AS
BEGIN

	DECLARE @T_NEWCONTACTS TABLE (FirstName NVARCHAR(100), MiddleName NVARCHAR(100), LastName NVARCHAR(100), MobileNumber VARCHAR(100),EmailAddress NVARCHAR(250), PhoneNumber VARCHAR(100), DateCreated DATE, IsDeleted VARCHAR(50), SubscribedToEmail VARCHAR(5))
	
	DECLARE @CRITERIA AS NVARCHAR(MAX)
	SET @CRITERIA = ''
	
	
	IF @ACCOUNT_ID IS NULL
		RETURN -60187
	
	
	
	/*Create Criteria*/
	SET @CRITERIA = 'c.IsDeleted = 0 AND cs.SUBSCRIBED_DATE IS NOT NULL AND c.AccountID = ' + '''' + CAST(@ACCOUNT_ID AS VARCHAR(50)) + ''''+ ' AND '
	
	IF (@FROM IS NOT NULL)
		SET @CRITERIA = @CRITERIA + 'cs.DATE_CREATED >= ' + '''' + CAST(CAST(@FROM AS DATE) AS VARCHAR(20)) + ''' AND '

	IF (@TO IS NOT NULL)
		SET @CRITERIA = @CRITERIA + 'cs.DATE_CREATED < ' + '''' + CAST(DATEADD(d, 1, @TO) AS VARCHAR(20)) + ''' AND '

	IF (@SOURCE IS NOT NULL)
		SET @CRITERIA = @CRITERIA + 'UPPER(cs.SUBSCRIBED_VIA) = ' + '''' + UPPER(@SOURCE) + ''' AND '

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
	INSERT INTO  @T_NEWCONTACTS (FirstName,MiddleName,LastName,MobileNumber,EmailAddress,PhoneNumber,DateCreated,IsDeleted,SubscribedToEmail) 	   
	EXEC('SELECT c.FirstName, c.MiddleName, c.LastName, c.MobileNumber, c.EmailAddress, c.PhoneNumber, cs.DATE_CREATED DateCreated, CASE c.IsDeleted WHEN 1 THEN '+''''+'Yes'+''''+' ELSE '+''''+'No'+''''+' END IsDeleted, CASE c.SubscribedToEmail WHEN 1 THEN '+''''+'Yes'+''''+ ' ELSE '+'''' + 'No' +'''' + ' END SubscribedToEmail ' +
	      ' FROM Contact_Subscription cs INNER JOIN Contact c ON cs.CONTACT_ID = c.ContactID WHERE ' 
	       + @CRITERIA +
		   ' ORDER BY  cs.DATE_CREATED  DESC')
   
	SELECT *
	FROM  @T_NEWCONTACTS T2
	ORDER BY DateCreated DESC
	
END



--DECLARE	@FROM DATETIME = '11/01/2019'
--DECLARE	@TO DATETIME = '11/08/2020'
--DECLARE	@SOURCE VARCHAR(50)= NULL
--DECLARE	@TYPE_ID TINYINT = NULL
--DECLARE	@STATUS_ID TINYINT = NULL
--DECLARE	@ACCOUNT_ID UNIQUEIDENTIFIER='6388071F-36BB-4823-A3ED-8770ADAE0F51'


--EXEC [dbo].[S_LeadReports_LeadsListing] @FROM,@TO,@SOURCE,@TYPE_ID,@STATUS_ID,@ACCOUNT_ID
