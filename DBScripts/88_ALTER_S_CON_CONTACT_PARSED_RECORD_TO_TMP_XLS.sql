USE [MarketingAutomationTool]
GO
/****** Object:  StoredProcedure [dbo].[S_CON_CONTACT_PARSED_RECORD_TO_TMP_XLS]    Script Date: 4/13/2020 4:12:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--USE [MarketingAutomationTool]
--GO
--/****** Object:  StoredProcedure [dbo].[S_CON_CONTACT_PARSED_RECORD_TO_TMP_XLS]    Script Date: 3/9/2020 3:49:29 AM ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO

---- =============================================
---- Author:		Leah Gean Diopenes
---- Create date: 29-Feb-2020
---- Description:	INSERT Parsed RECORDs INTO Temporary table

---- =============================================

ALTER PROCEDURE [dbo].[S_CON_CONTACT_PARSED_RECORD_TO_TMP_XLS] 
	@JOB_ID INT,
	@ACCOUNT_ID UNIQUEIDENTIFIER,
	@FIRST_NAME NVARCHAR(100),
	@LAST_NAME NVARCHAR(100),
	@EMAIL NVARCHAR(250),
	@MOBILE VARCHAR(100),
	@TITLE NVARCHAR(50),
	@SUBSCRIBEDTOEMAIL BIT,
	@CONTACTTYPE INT,
	@CONTACTSTATUS INT,
	@LEADSTATUS INT,
	@GENDER CHAR(1),
	@COMPANY_NAME NVARCHAR(100),
	@WEBSITE NVARCHAR(250),
	@POSITION_TITLE NVARCHAR(50),
	@PHONE_NO VARCHAR(100),
	@ADDRESS1 NVARCHAR(100),
	@ADDRESS2 NVARCHAR(100),
	@CITY NVARCHAR(50),
	@STATE NVARCHAR(50),
	@COUNTRYID INT,
	@POSTALCODE NVARCHAR(20),
	@SUBSCRIBEDTOEMAILVIA VARCHAR(50),
	@CREATEDBY UNIQUEIDENTIFIER,
	@SUBSCRIBEDTOIPADDRESS VARCHAR(15),
	@FORMID INT,
	@CONTACTLISTID INT,
	@FILTERID INT
AS
BEGIN
	declare @sql varchar(max)
	declare @TABLE_NAME VARCHAR(100)

	SET @TABLE_NAME = 'TMP_CONTACT_JOB_' + CAST(@JOB_ID AS VARCHAR)

	--CREATE THE TABLE DEFINATION
	IF OBJECT_ID (@TABLE_NAME, 'U') IS NULL
	BEGIN
		--[01]-- added the new fields for the table
		SET @SQL ='CREATE TABLE ' + @TABLE_NAME + 
				'(
					[ID] [BIGINT] NOT NULL PRIMARY KEY IDENTITY,
					[ACCOUNT_ID] UNIQUEIDENTIFIER NOT NULL,
					[FIRST_NAME] [NVARCHAR](100) NULL,
					[LAST_NAME] [NVARCHAR](100) NOT NULL,
					[EMAIL] [NVARCHAR](250) NOT NULL,
					[MOBILE] [VARCHAR](100) NULL,
					[TITLE] NVARCHAR(50),
					[SUBSCRIBEDTOEMAIL] BIT,
					[CONTACTTYPE] INT,
					[CONTACTSTATUS] INT,
					[LEADSTATUS] INT,
					[GENDER] CHAR(1),
					[COMPANY_NAME] NVARCHAR(100),
					[WEBSITE] NVARCHAR(250),
					[POSITION_TITLE] NVARCHAR(50),
					[PHONE_NO] [VARCHAR](100),
					[ADDRESS1] NVARCHAR(100),
					[ADDRESS2] NVARCHAR(100),
					[CITY] NVARCHAR(50),
					[STATE] NVARCHAR(50),
					[COUNTRYID] INT,
					[POSTALCODE] NVARCHAR(20),
					[SUBSCRIBEDTOEMAILVIA] VARCHAR(50),
					[CREATEDBY] UNIQUEIDENTIFIER,
					[SUBSCRIBEDTOIPADDRESS] VARCHAR(15),
					[FORMID] INT,
					[CONTACTLISTID] INT,
					[FILTERID] INT,
					[ISUPLOADED] BIT NULL,
					[ERROR] NVARCHAR(MAX) NULL
				)'
		PRINT @SQL
		EXEC(@SQL)
	END

	--[01]--
	--added the new fields
	SET @SQL = 'INSERT INTO ' +  @TABLE_NAME + ' (
									ACCOUNT_ID,
									FIRST_NAME,
									LAST_NAME,
									EMAIL,
									MOBILE,
									TITLE,
									SUBSCRIBEDTOEMAIL,
									CONTACTTYPE,
									CONTACTSTATUS,
									LEADSTATUS,
									GENDER,
									COMPANY_NAME,
									WEBSITE,
									POSITION_TITLE,
									PHONE_NO,
									ADDRESS1,
									ADDRESS2,
									CITY,
									STATE,
									COUNTRYID,
									POSTALCODE,
									SUBSCRIBEDTOEMAILVIA,
									CREATEDBY,
									SUBSCRIBEDTOIPADDRESS,
									FORMID,
									CONTACTLISTID,
									FILTERID
									)'
	SET @SQL = @SQL + ' VALUES( '+'''' + CAST(@ACCOUNT_ID AS VARCHAR(50)) +''''+ 
					+ ',' + '''' + REPLACE(@FIRST_NAME,'''','''''') 
					+ ''',' + '''' + REPLACE(@LAST_NAME,'''','''''') 
					+ ''',' + '''' + @EMAIL 
					+ ''',' +'''' + ISNULL(@MOBILE,'')
					+ ''',' + '''' + ISNULL(@TITLE,'')
					+ ''',' +  ISNULL(CAST(@SUBSCRIBEDTOEMAIL AS NVARCHAR(5)),1)
					+ ',' +  ISNULL(CAST(@CONTACTTYPE AS NVARCHAR(5)),'NULL')
					+ ',' +  ISNULL(CAST(@CONTACTSTATUS AS NVARCHAR(5)),'NULL')
					+ ',' +  ISNULL(CAST(@LEADSTATUS AS NVARCHAR(5)),'NULL')
					+ ',' + '''' +  ISNULL(@GENDER,'')
					+ ''',' + '''' +  REPLACE(ISNULL(@COMPANY_NAME,''),'''','''''') 
					+ ''',' + '''' +  REPLACE(ISNULL(@WEBSITE,''),'''','''''') 
					+ ''',' + '''' +  REPLACE(ISNULL(@POSITION_TITLE,''),'''','''''') 
					+ ''',' + '''' +  ISNULL(@PHONE_NO,'')
					+ ''',' + '''' +  REPLACE(ISNULL(@ADDRESS1,''),'''','''''') 
					+ ''',' + '''' +  REPLACE(ISNULL(@ADDRESS2,''),'''','''''') 
					+ ''',' + '''' +  REPLACE(ISNULL(@CITY,''),'''','''''') 
					+ ''',' + '''' +  REPLACE(ISNULL(@STATE,''),'''','''''') 
					+ ''',' +  ISNULL(CAST(@COUNTRYID AS NVARCHAR(5)),'NULL')
					+ ',' + '''' +  ISNULL(@POSTALCODE,'')
					+ ''',' + '''' +  ISNULL(@SUBSCRIBEDTOEMAILVIA,'')
					+ ''',' + '''' + CAST(@CREATEDBY AS VARCHAR(50)) +
					+ ''',' + '''' +  ISNULL(@SUBSCRIBEDTOIPADDRESS,'')
					+ ''',' +  ISNULL(CAST(@FORMID AS NVARCHAR(5)),'NULL')
					+ ',' +  ISNULL(CAST(@CONTACTLISTID AS NVARCHAR(5)),'NULL')
					+ ',' + ISNULL(CAST(@FILTERID AS NVARCHAR(5)),'NULL')
					+ ')'

	PRINT @SQL
	EXEC(@SQL)
	
	return @@identity
END

/*
exec S_CON_CONTACT_PARSED_RECORD_TO_TMP_XLS 5,'ccf4d547-c9ec-42b4-97df-b12752734323','Andrew','Killick','akillick@mailinator.com','','',1,NULL,NULL,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'1f3d066a-d769-460d-878d-4e4359ba0f72',NULL,NULL,NULL,NULL
SELECT * FROM TMP_CONTACT_JOB_5
DROP TABLE TMP_CONTACT_JOB_5

*/

--select * from Account