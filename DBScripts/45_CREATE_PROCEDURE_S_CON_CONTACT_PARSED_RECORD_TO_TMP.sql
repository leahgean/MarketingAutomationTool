USE MarketingAutomationTool
GO
/****** Object:  StoredProcedure [dbo].[S_CON_CONTACT_PARSED_RECORD_TO_TMP]    Script Date: 2/29/2020 2:12:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Leah Gean Diopenes
-- Create date: 29-Jan-2020
-- Description:	INSERT Parsed RECORDs INTO Temporary table
-- =============================================

CREATE PROCEDURE [dbo].[S_CON_CONTACT_PARSED_RECORD_TO_TMP] 
	@JOB_ID INT,
	@ACCOUNT_ID UNIQUEIDENTIFIER,
	@FIRST_NAME NVARCHAR(100),
	@LAST_NAME NVARCHAR(100),
	@Email NVARCHAR(250),
	@Mobile VARCHAR(100)
AS
BEGIN
	declare @sql varchar(max)
	declare @TABLE_NAME VARCHAR(100)

	SET @TABLE_NAME = 'TMP_CON_CONTACT_JOB_' + CAST(@JOB_ID AS VARCHAR)

	--CREATE THE TABLE DEFINATION
	IF OBJECT_ID (@TABLE_NAME, 'U') IS NULL
	BEGIN
		SET @SQL ='CREATE TABLE ' + @TABLE_NAME + 
				'(
					[ID] [BIGINT] NOT NULL PRIMARY KEY IDENTITY,
					[ACCOUNT_ID] [UNIQUEIDENTIFIER] NOT NULL,
					[FIRST_NAME] [NVARCHAR](100) NULL,
					[LAST_NAME] [NVARCHAR](100) NOT NULL,
					[EMAIL] [NVARCHAR](256) NOT NULL,
					[MOBILE] [VARCHAR](50) NULL
				)'
		PRINT @SQL
		EXEC(@SQL)
	END


	SET @SQL = 'INSERT INTO ' +  @TABLE_NAME + ' (ACCOUNT_ID, FIRST_NAME, LAST_NAME, EMAIL, MOBILE)'
	SET @SQL = @SQL + ' VALUES( ' +''''+ CAST(@ACCOUNT_ID AS NVARCHAR(50)) +''''+ ',' + '''' + @FIRST_NAME + ''',' + '''' + @LAST_NAME + ''',' + '''' + @EMAIL + ''',' + '''' + @MOBILE + ''')'

	PRINT @SQL
	EXEC(@SQL)
	
	RETURN SCOPE_IDENTITY()
END

/*
EXEC S_CON_CONTACT_PARSED_RECORD_TO_TMP 1,'CCF4D547-C9EC-42B4-97DF-B12752734323','VAIBHAV','PATEL','VAIBHAV@SELECTBYTES.COM','61 12345'
SELECT * FROM TMP_CON_CONTACT_JOB_1

DROP TABLE TMP_CON_CONTACT_JOB_1

*/

select *from account