USE [MarketingAutomationTool]
GO
/****** Object:  StoredProcedure [dbo].[S_CON_CONTACT_PARSED_RECORD_TO_TMP_XLS]    Script Date: 3/8/2020 1:47:41 PM ******/
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
	@Email NVARCHAR(250),
	@Mobile VARCHAR(100),
	--[01]--
	@TITLE nvarchar(10),
	@COMPANY_NAME nvarchar(100),
	@WEBSITE nvarchar(250),
	@POSITION_TITLE nvarchar(50),
	@PHONE_NO VARCHAR(100),
	@ADDRESS nvarchar(100),	
	@GENDER char(1),
	@POSTALCODE nvarchar(20),
	--[01]--
	@COUNTRYID VARCHAR(50),
	@CITY NVarChar(200),
	@STATE NVarChar(100)
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
					[EMAIL] [NVARCHAR](256) NOT NULL,
					[MOBILE] [VARCHAR](50) NULL,
					[TITLE] NVARCHAR(10),
					[COMPANY_NAME] NVARCHAR(100),
					[WEBSITE] NVARCHAR(250),
					[POSITION_TITLE] NVARCHAR(50),
					[PHONE_NO] [VARCHAR](50),
					[ADDRESS] NVARCHAR(100),	
					[GENDER] CHAR(1),
					[POSTALCODE] NVARCHAR(20),
					[COUNTRYID] VARCHAR(50),
					[CITY] NVARCHAR(200),
					[STATE] NVARCHAR(100),
					ISUPLOADED BIT,
					ERROR VARCHAR(100)
				)'
		PRINT @SQL
		EXEC(@SQL)
	END

	--[01]--
	--added the new fields
	SET @SQL = 'INSERT INTO ' +  @TABLE_NAME + ' (ACCOUNT_ID, FIRST_NAME, LAST_NAME, EMAIL, MOBILE, 
												TITLE,		COMPANY_NAME,	WEBSITE,	POSITION_TITLE,		PHONE_NO,
												ADDRESS,	GENDER,			POSTALCODE, COUNTRYID, CITY,STATE)'
	SET @SQL = @SQL + ' VALUES( '+'''' + CAST(@ACCOUNT_ID AS VARCHAR(50)) +''''+ 
					+ ',' + '''' + @FIRST_NAME 
					+ ''',' + '''' + @LAST_NAME 
					+ ''',' + '''' + @EMAIL 
					+ ''',' + '''' + @MOBILE
					--[01]-- added fields
					+ ''',' + '''' + @TITLE
					+ ''',' + '''' + @COMPANY_NAME
					+ ''',' + '''' + @WEBSITE
					+ ''',' + '''' + @POSITION_TITLE
					+ ''',' + '''' + @PHONE_NO
					+ ''',' + '''' + @ADDRESS
					+ ''',' + '''' + @GENDER
					--[01] last entry
					+ ''',' + '''' + @POSTALCODE
					+ ''',' + '''' + @COUNTRYID
					+ ''',' + '''' + @CITY
					+ ''',' + '''' + @STATE
					+ ''')'

	PRINT @SQL
	EXEC(@SQL)
	
	return @@identity
END

/*
EXEC S_CON_CONTACT_PARSED_RECORD_TO_TMP 8,'CCF4D547-C9EC-42B4-97DF-B12752734323','VAIBHAV','PATEL','VAIBHAV@SELECTBYTES.COM','61 12345'
SELECT * FROM TMP_CON_CONTACT_JOB_8

*/

select * from Account