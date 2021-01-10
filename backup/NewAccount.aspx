<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewAccount.aspx.cs" Inherits="MarketingAutomationTool.NewAccount" MasterPageFile="~/MATSite.Master" %>
<%@ Register Src="~/UserControls/LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/RightMenu.ascx" TagName="RightMenu" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server"> 
    <link rel="stylesheet" href="dcjqaccordion-master/css/dcaccordion.css" />
	
	<script src="bootstrap/assets/js/vendor/jquery-slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="bootstrap/assets/js/vendor/popper.min.js" integrity="sha384-cs/chFZiN24E4KMATLdqdvsezGxaGsi4hLGOzlXwp5UZB1LY//20VyM2taTB4QvJ" crossorigin="anonymous"></script>
    <script src="bootstrap/js/bootstrap.min.js" integrity="sha384-uefMccjFJAIv6A+rW+L4AHf99KvxDjWSu1z9VI8SKNVmz4sk7buKt/6v9KI65qnm" crossorigin="anonymous"></script>
	<script src="jQuery/jquery-3.3.1.min.js" type="text/javascript"></script>
	<script src="dcjqaccordion-master/js/jquery.cookie.js" type="text/javascript"></script>
	<script src="dcjqaccordion-master/js/jquery.hoverIntent.minified.js" type="text/javascript"></script>
	<script src="dcjqaccordion-master/js/jquery.dcjqaccordion.2.7.min.js" type="text/javascript"></script>
	<script src="dcjqaccordion-master/accordionmenu.js"   type="text/javascript"></script>
    <script type="text/javascript">
        function validateEmail(email) {
              var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
              return re.test(email);
        }

        function ValidateUserName() {
            if ($('#<%=txtUserName.ClientID%>').val() == '') {
                $("#invalidUserName").show();
                $("#<%=txtUserName.ClientID%>").css({ "border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)" });
                return false;   
            }
            else {
                $("#invalidUserName").hide();
                $("#<%=txtUserName.ClientID%>").css({ "border": "1px solid #ced4da", "box-shadow": "none" });
                return true;
            }
        }

        function ValidateRequiredFields() {

            var errorCount = 0;
            if (!ValidateUserName()) errorCount++;

            

            if ($('#<%=txtFirstName.ClientID%>').val() == '') {
                $("#invalidFirstName").show();
                $("#<%=txtFirstName.ClientID%>").css({ "border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)" });
                errorCount++;    
            }
            else {
                $("#invalidFirstName").hide();
                $("#<%=txtFirstName.ClientID%>").css({ "border": "1px solid #ced4da", "box-shadow": "none" });
            }

            if ($('#<%=txtLastName.ClientID%>').val() == '') {
                $("#invalidLastName").show();
                $("#<%=txtLastName.ClientID%>").css({ "border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)" });
                errorCount++;    
            }
            else {
                $("#invalidLastName").hide();
                $("#<%=txtLastName.ClientID%>").css({ "border": "1px solid #ced4da", "box-shadow": "none" });
            }

            if ($('#<%=txtPhoneNo.ClientID%>').val() == '') {
                $("#invalidPhoneNo").show();
                $("#<%=txtPhoneNo.ClientID%>").css({ "border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)" });
                errorCount++;    
            }
            else {
                $("#invalidPhoneNo").hide();
                $("#<%=txtPhoneNo.ClientID%>").css({ "border": "1px solid #ced4da", "box-shadow": "none" });
            }

            if ($('#<%=txtEmailAddress.ClientID%>').val() == ''){
                $("#invalidEmail").show();
                $("#<%=txtEmailAddress.ClientID%>").css({ "border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)" });
                errorCount++;    
            }
            else {
                $("#invalidEmail").hide();
                $("#<%=txtEmailAddress.ClientID%>").css({ "border": "1px solid #ced4da", "box-shadow": "none" });
                if (!ValidateEmailAddress()) errorCount++;
            }

            if ($('#<%=txtCompanyName.ClientID%>').val() == '') {
                $("#invalidCompanyName").show();
                $("#<%=txtCompanyName.ClientID%>").css({ "border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)" });
                errorCount++;    
            }
            else {
                $("#invalidCompanyName").hide();
                $("#<%=txtCompanyName.ClientID%>").css({ "border": "1px solid #ced4da", "box-shadow": "none" });
            }
            if ($('#<%=txtAddress.ClientID%>').val() == '') {
                $("#invalidAddress").show();
                $("#<%=txtAddress.ClientID%>").css({ "border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)" });
                errorCount++;    
            }
            else {
                $("#invalidAddress").hide();
                $("#<%=txtAddress.ClientID%>").css({ "border": "1px solid #ced4da", "box-shadow": "none" });
            }

            if ($('#<%=txtCity.ClientID%>').val() == '') {
                $("#invalidCity").show();
                $("#<%=txtCity.ClientID%>").css({ "border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)" });
                errorCount++;    
            }
            else {
                $("#invalidCity").hide();
                $("#<%=txtCity.ClientID%>").css({ "border": "1px solid #ced4da", "box-shadow": "none" });
            }
            if ($('#<%=txtState.ClientID%>').val() == '') {
                $("#invalidState").show();
                $("#<%=txtState.ClientID%>").css({ "border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)" });
                errorCount++;    
            }
            else {
                $("#invalidState").hide();
                $("#<%=txtState.ClientID%>").css({ "border": "1px solid #ced4da", "box-shadow": "none" });
            }

            if ($('#<%=txtPostalZip.ClientID%>').val() == '') {
                $("#invalidZipCode").show();
                $("#<%=txtPostalZip.ClientID%>").css({ "border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)" });
                errorCount++;    
            }
            else {
                $("#invalidZipCode").hide();
                $("#<%=txtPostalZip.ClientID%>").css({ "border": "1px solid #ced4da", "box-shadow": "none" });
            }

            if ($('#<%=ddlCountry.ClientID%>').val() == '') {
                $("#invalidCountry").show();
                $("#<%=ddlCountry.ClientID%>").css({ "border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)" });
                errorCount++;    
            }
            else {
                $("#invalidCountry").hide();
                $("#<%=ddlCountry.ClientID%>").css({ "border": "1px solid #ced4da", "box-shadow": "none" });
            }

            return  (errorCount > 0 ? false : true);
            
            
        }

        function ValidateEmailAddress() {
                    if (validateEmail($('#<%=txtEmailAddress.ClientID%>').val()))
                    {
                         return true;
                    }
                    else {
                        $("#invalidEmail").text('Please enter a valid Email Address. e.g. example@mail.com');
                        $("#invalidEmail").show();
                        $("#<%=txtEmailAddress.ClientID%>").css({"border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)"});
                        return false;
                    }
        }

        function Validate() {
            
            if (ValidateRequiredFields()) {
                return CheckEmailAddress();
            }
            else {
                return false;
            } 
        }

        
        $(function() {
            $('#<%=txtUserName.ClientID%>').focusout(function(){
                ValidateUserName();
            });
        });
     

    </script>
	<link href="dcjqaccordion-master/css/skins/graphite.css" rel="stylesheet" type="text/css" />
    <title>Marketing Automation Tool - My Account</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cpBody" runat="server">
         <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 leftmenu">
             <uc:LeftMenu runat="server" id="ucLeftMenu"></uc:LeftMenu>
				
				</div>
				<div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 middlecontent">
					<div class="page_header">
						<h5 class="page_title">New Account</h5>
					</div>
					<div class="page_content">
						<div class="container-fluid">
							<div class="tab-content">
								<div class="tab-pane active" id="newaccount">
									<div class="form-horizontal">
										<div class="container">
                                            <!--div class="needs-validation" id="dvFormValidate" novalidate=""-->
                                                <div class="" role="alert" id="dvMessage" runat="server" style="display:none;">
                                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                        <span aria-hidden="true">&times;</span>
                                                      </button>
                                                </div>
											    <legend class="legendstyle">Account Administrator</legend>
                                                <div class="form-group row required">
                                                    <label for="txtUserName" class="col-sm-2 col-form-label">User Name:</label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Username" MaxLength="50" ></asp:TextBox>
                                                        <div class="invalid-feedback" id="invalidUserName">
                                                          Please enter UserName.
                                                        </div>
                                                    </div>
                                                </div>
											    <legend class="legendstyle">User Details</legend>
                                                 <div class="form-group row required">
                                                    <label for="txtFirstName" class="col-sm-2 col-form-label">First Name:</label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="First Name" MaxLength="100" ></asp:TextBox>
                                                         <div class="invalid-feedback" id="invalidFirstName">
                                                          Please enter First Name.
                                                         </div> 
                                                    </div>
                                                </div>
                                                 <div class="form-group row">
                                                    <label for="txtMiddleName" class="col-sm-2 col-form-label">Middle Name:</label>
                                                    <div class="col-sm-10">
                                                           <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control" placeholder="Middle Name" MaxLength="100" ></asp:TextBox> 
                                                    </div>
                                                </div>
                                                <div class="form-group row required">
                                                    <label for="txtLastName" class="col-sm-2 col-form-label">Last Name:</label>
                                                    <div class="col-sm-10">
                                                          <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Last Name" MaxLength="100" ></asp:TextBox>
                                                          <div class="invalid-feedback" id="invalidLastName">
                                                          Please enter Last Name.
                                                        </div> 
                                                    </div>
                                                </div>
                                                 <div class="form-group row required">
                                                    <label for="txtPhoneNo" class="col-sm-2 col-form-label">Phone No:</label>
                                                    <div class="col-sm-10">
                                                            <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control" placeholder="Phone No" MaxLength="100" ></asp:TextBox>
                                                            <div class="invalid-feedback" id="invalidPhoneNo">
                                                                Please enter Phone No.
                                                            </div> 
                                                    </div>
                                                </div>
                                                 <div class="form-group row">
                                                    <label for="txtFaxNo" class="col-sm-2 col-form-label">Fax:</label>
                                                    <div class="col-sm-10">
                                                            <asp:TextBox ID="txtFaxNo" runat="server" CssClass="form-control" placeholder="Fax No" MaxLength="100" ></asp:TextBox>
                                                    </div>
                                                </div>
                                                 <div class="form-group row">
                                                    <label for="txtMobileNo" class="col-sm-2 col-form-label">Mobile No:</label>
                                                    <div class="col-sm-10">
                                                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" placeholder="Mobile No" MaxLength="100" ></asp:TextBox>
                                                    </div>
                                                </div>
                                                 <div class="form-group row required">
                                                    <label for="txEmailAddress" class="col-sm-2 col-form-label">Email Address:</label>
                                                    <div class="col-sm-10">
                                                            <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="form-control" placeholder="Email Address" MaxLength="250" ></asp:TextBox>
                                                            <div class="invalid-feedback" id="invalidEmail">
                                                                Please enter Email Address.
                                                            </div> 
                                                    </div>
                                                </div>
											
											    <legend class="legendstyle">Company Details</legend>
											     <div class="form-group row required">
                                                    <label for="txtCompanyName" class="col-sm-2 col-form-label">Company Name:</label>
                                                    <div class="col-sm-10">
                                                             <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" placeholder="Company Name" MaxLength="50"  ></asp:TextBox>
                                                            <div class="invalid-feedback" id="invalidCompany">
                                                                Please enter Company Name.
                                                             </div> 
                                                    </div>
                                                </div>
                                                 <div class="form-group row required">
                                                    <label for="txtAddress" class="col-sm-2 col-form-label">Address:</label>
                                                    <div class="col-sm-10">
                                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Address" MaxLength="250" ></asp:TextBox>
                                                            <div class="invalid-feedback" id="invalidAddress">
                                                                Please enter Address.
                                                            </div> 
                                                    </div>
                                                </div> 
                                                 <div class="form-group row required">
                                                    <label for="txtCity" class="col-sm-2 col-form-label">City/Town/Suburb:</label>
                                                    <div class="col-sm-10">
                                                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" placeholder="City/Town/Suburb" ></asp:TextBox>
                                                            <div class="invalid-feedback" id="invalidCity">
                                                                Please enter City.
                                                            </div> 
                                                    </div>
                                                </div>
                                                 <div class="form-group row required">
                                                    <label for="txtState" class="col-sm-2 col-form-label">State:</label>
                                                    <div class="col-sm-10">
                                                           <asp:TextBox ID="txtState" runat="server" CssClass="form-control" placeholder="State/Region" ></asp:TextBox>
                                                            <div class="invalid-feedback" id="invalidState">
                                                                Please enter State/Region.
                                                            </div>
                                                    </div>
                                                </div>
                                                 <div class="form-group row required">
                                                    <label for="txtPostalZip" class="col-sm-2 col-form-label">Postal/Zip Code:</label>
                                                    <div class="col-sm-10">
                                                            <asp:TextBox ID="txtPostalZip" runat="server" CssClass="form-control" placeholder="Postal/Zip Code" ></asp:TextBox>
                                                             <div class="invalid-feedback" id="invalidZipCode">
                                                                Please enter Postal/Zip Code.
                                                            </div>
                                                    </div>
                                                </div>
                                                 <div class="form-group row required">
                                                    <label for="ddlCountry" class="col-sm-2 col-form-label">Country:</label>
                                                    <div class="col-sm-10">
                                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" placeholder="Country" ></asp:DropDownList>
                                                            <div class="invalid-feedback" id="invalidCountry">
                                                                Please select Country.
                                                            </div>
                                                     </div>
                                                </div>
                                                 <div class="form-group row">
                                                    <label for="txtWebSite" class="col-sm-2 col-form-label">Website:</label>
                                                    <div class="col-sm-10">
                                                            <asp:TextBox ID="txtWebSite" runat="server" CssClass="form-control" placeholder="Website" MaxLength="400" ></asp:TextBox>
                                                    </div>
                                                </div>
											    <div class="row">
														    <div class="form-group form-group-sm col-sm-12">
															    <div class="row">
															    <div class="col-sm-12 button_panel">
																    <div class="float-right mt-2"> 
                                                                        <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary" OnClick="btnSave_Click" OnClientClick="return Validate();" />
																	    <asp:Button ID="btnClear" runat="server" Text="Clear" class="btn btn-light" OnClick="btnClear_Click"   />
																    </div>
															    </div>
															    </div>
														    </div>
											    </div>
                                            <!--/div-->
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
				<div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 rightmenu">
                     <uc:RightMenu runat="server" id="ucRightMenu"></uc:RightMenu>
				</div>
</asp:Content>
