USE [MarketingAutomationTool]
GO
/****** Object:  StoredProcedure [dbo].[S_LeadReports_NewLeadsStatistics]    Script Date: 11/8/2020 4:16:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: 02-November-2020
-- Description:	Gets number of leads (not deleted and subscribed)
-- =============================================
ALTER PROCEDURE [dbo].[S_LeadReports_NewLeadsStatistics]
	@FROM DATETIME = NULL,
	@TO DATETIME = NULL,
	@SOURCE VARCHAR(50)= NULL,
	@TYPE_ID TINYINT = NULL,
	@STATUS_ID TINYINT = NULL,
	@ACCOUNT_ID UNIQUEIDENTIFIER,
	@TOTALLEADCNT INT OUTPUT
AS
BEGIN

	DECLARE @T_TIME TABLE ([DATE] DATETIME NULL)
	DECLARE @T_NEWCONTACTS TABLE ([DATE] DATETIME NULL, [COUNT] INT NULL)
	
	DECLARE @CRITERIA AS NVARCHAR(MAX)
	SET @CRITERIA = ''
	
	DECLARE @RUNDATE AS DATETIME
	
	IF @ACCOUNT_ID IS NULL
		RETURN -60187
	
	/*Populate All Dates table*/	
	SET @RUNDATE = CAST(@FROM AS DATE)
	
	WHILE (@RUNDATE <= CAST(@TO AS DATE))
	BEGIN
		INSERT @T_TIME
		SELECT @RUNDATE
		
		SET @RUNDATE = DATEADD(D, 1, @RUNDATE)
	END
	
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
	INSERT INTO  @T_NEWCONTACTS  	   
	EXEC('SELECT CAST(cs.DATE_CREATED as DATE) DATE, COUNT(*) NEW_CONTACTS
	       FROM Contact_Subscription cs INNER JOIN Contact c ON cs.CONTACT_ID = c.ContactID WHERE ' 
	       + @CRITERIA +
		   ' GROUP BY CAST(cs.DATE_CREATED as DATE)
		   ORDER BY CAST(cs.DATE_CREATED as DATE) DESC')


		   
	SELECT @TOTALLEADCNT=SUM(ISNULL(T2.[COUNT],0))
	FROM  @T_NEWCONTACTS T2
	
	/*Query All Data*/	   
	SELECT T1.[DATE], ISNULL([COUNT],0) AS COUNT
	FROM @T_TIME T1 
	LEFT JOIN @T_NEWCONTACTS T2
	ON T1.[DATE] = T2.[DATE]
	ORDER BY T1.[DATE] DESC
END
