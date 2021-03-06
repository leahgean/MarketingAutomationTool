ALTER PROCEDURE [dbo].[S_LEAD_UPDATELEAD]
@AccountID UNIQUEIDENTIFIER,
@ContactID UNIQUEIDENTIFIER,
@Title NVARCHAR(50),
@FirstName NVARCHAR(100),
@MiddleName NVARCHAR(100),
@LastName NVARCHAR(100),
@CompanyName NVARCHAR(100),
@WebSite NVARCHAR(250),
@Position NVARCHAR(50),
@ContactType INT,
@LeadStatus INT,
@ContactStatus INT,
@Gender CHAR(1),
@EmailAddress NVARCHAR(250),
@PhoneNumber VARCHAR(100),
@MobileNumber VARCHAR(100),
@FacebookID NVARCHAR(50),
@Address1 NVARCHAR(250),
@Address2 NVARCHAR(250),
@City NVARCHAR(50),
@State NVARCHAR(50),
@CountryId INT,
@ZipCode NVARCHAR(20),
@SubscribedToEmail BIT,
@UseForTesting BIT,
@ModifiedBy UNIQUEIDENTIFIER 
AS
BEGIN

DECLARE @UnsubscribedToEmailDate DATETIME=NULL
IF (@SubscribedToEmail=0)
BEGIN
	SET @UnsubscribedToEmailDate=GETDATE()
END

UPDATE dbo.Contact
	SET Title=@Title,
		FirstName=@FirstName,
		MiddleName=@MiddleName,
		LastName=@LastName,
		CompanyName=@CompanyName,
		WebSite=@WebSite,
		Position=@Position,
		ContactType=@ContactType,
		LeadStatus=@LeadStatus,
		ContactStatus=@ContactStatus,
		Gender=@Gender,
		EmailAddress=@EmailAddress,
		PhoneNumber=@PhoneNumber,
		MobileNumber=@MobileNumber,
		FacebookID=@FacebookID,
		Address1=@Address1,
		Address2=@Address2,
		City=@City,
		[State]=@State,
		CountryId=@CountryId,
		ZipCode=@ZipCode,
		SubscribedToEmail=@SubscribedToEmail,
		UnsubscribedToEmailDate=@UnsubscribedToEmailDate,
		UseForTesting=@UseForTesting,
		ModifiedBy=@ModifiedBy,
		ModifiedDate = GETDATE()
	WHERE AccountID=@AccountID AND ContactID = @ContactID

     		
END
 
