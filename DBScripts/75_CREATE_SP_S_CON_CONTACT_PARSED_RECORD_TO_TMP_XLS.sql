USE [MarketingAutomationTool]
GO
/****** Object:  StoredProcedure [dbo].[S_CON_CONTACT_PARSED_RECORD_TO_TMP_XLS]    Script Date: 3/9/2020 1:33:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: 29-Feb-2020
-- Description:	INSERT Parsed RECORDs INTO Temporary table

-- =============================================

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
					+ ',' + '''' + @FIRST_NAME 
					+ ''',' + '''' + @LAST_NAME
					+ ''',' + '''' + @EMAIL 
					+ ''',' + '''' + @MOBILE
					+ ''',' + '''' + @TITLE
					+ ''',' +  CAST(@SUBSCRIBEDTOEMAIL AS NVARCHAR(5))
					+ ',' +  CAST(@CONTACTTYPE AS NVARCHAR(5))
					+ ',' +  CAST(@CONTACTSTATUS AS NVARCHAR(5))
					+ ',' +  CAST(@LEADSTATUS AS NVARCHAR(5))
					+ ''',' + '''' + @GENDER
					+ ''',' + '''' + @COMPANY_NAME
					+ ''',' + '''' + @WEBSITE
					+ ''',' + '''' + @POSITION_TITLE
					+ ''',' + '''' + @PHONE_NO
					+ ''',' + '''' + @ADDRESS1
					+ ''',' + '''' + @ADDRESS2
					+ ''',' + '''' + @CITY
					+ ''',' + '''' + @STATE
					+ ''',' +  CAST(@COUNTRYID AS NVARCHAR(5))
					+ ',' + '''' + @POSTALCODE
					+ ''',' + '''' + @SUBSCRIBEDTOEMAILVIA
					+ ''',' + '''' + CAST(@CREATEDBY AS VARCHAR(50)) +
					+ ''',' + '''' + @SUBSCRIBEDTOIPADDRESS
					+ ''',' +  CAST(@FORMID AS NVARCHAR(5))
					+ ',' +  CAST(@CONTACTLISTID AS NVARCHAR(5))
					+ ',' + CAST(@FILTERID AS NVARCHAR(5))
					+ ''')'

	PRINT @SQL
	EXEC(@SQL)
	
	return @@identity
END

/*
EXEC S_CON_CONTACT_PARSED_RECORD_TO_TMP 8,'CCF4D547-C9EC-42B4-97DF-B12752734323','VAIBHAV','PATEL','VAIBHAV@SELECTBYTES.COM','61 12345'
SELECT * FROM TMP_CON_CONTACT_JOB_8

*/

--select * from Account