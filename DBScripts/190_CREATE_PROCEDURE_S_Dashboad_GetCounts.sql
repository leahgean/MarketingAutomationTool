CREATE PROCEDURE S_Dashboad_GetCounts
@AccountId UNIQUEIDENTIFIER
AS
BEGIN


DECLARE @TotalLeads INT=0
DECLARE @NewLeads INT=0
DECLARE @EmailsSent INT=0
DECLARE @UniqueOpens INT=0
DECLARE @TotalOpens INT=0
DECLARE @UniqueClicks INT=0
DECLARE @TotalClicks INT=0
DECLARE @Unsubscribe INT=0

SELECT @TotalLeads=dbo.fn_Dashboard_TotalLeads(@AccountId)
SELECT @NewLeads=dbo.fn_Dashboard_NewLeads(@AccountId)
SELECT @Unsubscribe=dbo.fn_Dashboard_UnSubscribes(@AccountId)

SELECT @TotalLeads TotalLeads,
@NewLeads NewLeads,
@EmailsSent EmailsSent,
@UniqueOpens UniqueOpens,
@TotalOpens TotalOpens,
@UniqueClicks UniqueClicks,
@TotalClicks TotalClicks,
@Unsubscribe Unsubscribe

END


--DECLARE @AccountId UNIQUEIDENTIFIER='6388071F-36BB-4823-A3ED-8770ADAE0F51'
--EXEC S_Dashboad_GetCounts @AccountId

