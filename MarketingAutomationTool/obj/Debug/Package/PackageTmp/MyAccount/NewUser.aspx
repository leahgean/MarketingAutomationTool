<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewUser.aspx.cs" Inherits="MarketingAutomationTool.MyAccount.NewUser" MasterPageFile="~/MATSite.Master" %>
<%@ Register Src="~/UserControls/LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/NewEmailCampaign.ascx" TagName="NewEmailCampaign" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
	<link rel="stylesheet" href="../dcjqaccordion-master/css/dcaccordion.css" />
	
	<script src="../bootstrap/assets/js/vendor/jquery-slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="../bootstrap/assets/js/vendor/popper.min.js" integrity="sha384-cs/chFZiN24E4KMATLdqdvsezGxaGsi4hLGOzlXwp5UZB1LY//20VyM2taTB4QvJ" crossorigin="anonymous"></script>
    <script src="../bootstrap/js/bootstrap.min.js" integrity="sha384-uefMccjFJAIv6A+rW+L4AHf99KvxDjWSu1z9VI8SKNVmz4sk7buKt/6v9KI65qnm" crossorigin="anonymous"></script>
	<script src="../jQuery/jquery-3.3.1.min.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.cookie.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.hoverIntent.minified.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.dcjqaccordion.2.7.min.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/accordionmenu.js"type="text/javascript"></script>
     <script type="text/javascript">
        function validateEmail(email) {
              var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
              return re.test(email);
        }

        function ValidateRequiredFields() {
           
            if ($('#<%=txtUserName.ClientID%>').val() == '') return false;
            if ($('#<%=txtFirstName.ClientID%>').val() == '') return false;
            if ($('#<%=txtLastName.ClientID%>').val() == '') return false;
            if ($('#<%=txtPhoneNo.ClientID%>').val() == '') return false;

            if ($('#<%=txtEmailAddress.ClientID%>').val() == '') {
                return false;
            }
            else {
                return ValidateEmailAddress();
            }
            if ($('#<%=txtAddress.ClientID%>').val() == '') return false;
            if ($('#<%=txtCity.ClientID%>').val() == '') return false;
            if ($('#<%=txtState.ClientID%>').val() == '') return false;
            if ($('#<%=txtPostalZip.ClientID%>').val() == '') return false;
            if ($('#<%=ddlCountry.ClientID%>').val() == '') return false;

            return true;
            
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
            /*bootstrap validation: shows the error messages for required fields; already prevents form submission of a required field is blank*/
            var forms = document.getElementById('dvFormValidate');            
            forms.classList.add('was-validated');

            if (ValidateRequiredFields()) {
                return CheckEmailAddress();
            }
            else {
                return false;
            }
            
        }
    </script>
	<link href="../dcjqaccordion-master/css/skins/graphite.css" rel="stylesheet" type="text/css" />
    <title>Marketing Automation Tool - New User</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpShortCut" runat="server"> 
     <uc:NewEmailCampaign runat="server" id="NewEmailCampaign"></uc:NewEmailCampaign>
</asp:Content>  
<asp:Content ID="Content4" ContentPlaceHolderID="cpTabHeaderBottom" runat="server"> 
    <div class="container-fluid">
        &nbsp;
    </div>
</asp:Content>  
<asp:Content ID="Content5" ContentPlaceHolderID="cpRowCrumbs" runat="server"> 
    <div class="container-fluid">
        <nav class="breadcrumb">
	        <asp:LinkButton ID="lnkBreadHome" runat="server" CssClass="breadcrumb-item" OnClick="lnkBreadHome_Click" Text="Home"></asp:LinkButton>
            <a class="breadcrumb-item" href="/MyAccount/MyAccount.aspx">My Account</a>
            <span class="breadcrumb-item active">New User</span>
	    </nav>
    </div>
</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="cpBody" runat="server">
    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 leftmenu">
			<uc:LeftMenu runat="server" id="ucLeftMenu"></uc:LeftMenu>
				</div>
				<div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 middlecontent">
					<div class="page_header">
							<h5 class="page_title">New User</h5>
					</div>
					<div class="page_content">
						<div class="container-fluid">
									<div class="form-horizontal">
										<div class="container">
                                            <div class="needs-validation" id="dvFormValidate" novalidate>
                                                <div class="" role="alert" id="dvMessage" runat="server" style="display:none;">
                                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                        <span aria-hidden="true">&times;</span>
                                                      </button>
                                                </div>
											    <div class="form-group row required">
                                                    <label for="txtUserName" class="col-sm-2 col-form-label">User Name:</label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Username" MaxLength="50" required></asp:TextBox>
                                                        <div class="invalid-feedback">
                                                          Please enter UserName.
                                                        </div>
                                                    </div>
                                                </div> 
                                                <div class="form-group row required">
                                                    <label for="txtFirstName" class="col-sm-2 col-form-label">First Name:</label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="First Name" MaxLength="100" required></asp:TextBox>
                                                         <div class="invalid-feedback">
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
                                                          <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Last Name" MaxLength="100" required></asp:TextBox>
                                                          <div class="invalid-feedback">
                                                          Please enter Last Name.
                                                        </div> 
                                                    </div>
                                                </div>
                                                 <div class="form-group row required">
                                                    <label for="txtPhoneNo" class="col-sm-2 col-form-label">Phone No:</label>
                                                    <div class="col-sm-10">
                                                            <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control" placeholder="Phone No" MaxLength="100" required></asp:TextBox>
                                                            <div class="invalid-feedback">
                                                                Please enter Phone No.
                                                            </div> 
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
                                                            <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="form-control" placeholder="Email Address" MaxLength="250" required></asp:TextBox>
                                                            <div class="invalid-feedback" id="invalidEmail">
                                                                Please enter Email Address.
                                                            </div> 
                                                    </div>
                                                </div>
                                                <div class="form-group row required">
                                                    <label for="txtAddress" class="col-sm-2 col-form-label">Address:</label>
                                                    <div class="col-sm-10">
                                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Address" MaxLength="250" required></asp:TextBox>
                                                            <div class="invalid-feedback">
                                                                Please enter Address.
                                                            </div> 
                                                    </div>
                                                </div> 
                                                 <div class="form-group row required">
                                                    <label for="txtCity" class="col-sm-2 col-form-label">City/Town/Suburb:</label>
                                                    <div class="col-sm-10">
                                                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" placeholder="City/Town/Suburb" required></asp:TextBox>
                                                            <div class="invalid-feedback">
                                                                Please enter City.
                                                            </div> 
                                                    </div>
                                                </div>
                                                 <div class="form-group row required">
                                                    <label for="txtState" class="col-sm-2 col-form-label">State:</label>
                                                    <div class="col-sm-10">
                                                           <asp:TextBox ID="txtState" runat="server" CssClass="form-control" placeholder="State/Region" required></asp:TextBox>
                                                            <div class="invalid-feedback">
                                                                Please enter State/Region.
                                                            </div>
                                                    </div>
                                                </div>
                                                 <div class="form-group row required">
                                                    <label for="txtPostalZip" class="col-sm-2 col-form-label">Postal/Zip Code:</label>
                                                    <div class="col-sm-10">
                                                            <asp:TextBox ID="txtPostalZip" runat="server" CssClass="form-control" placeholder="Postal/Zip Code" required></asp:TextBox>
                                                             <div class="invalid-feedback">
                                                                Please enter Postal/Zip Code.
                                                            </div>
                                                    </div>
                                                </div>
                                                 <div class="form-group row required">
                                                    <label for="ddlCountry" class="col-sm-2 col-form-label">Country:</label>
                                                    <div class="col-sm-10">
                                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" placeholder="Country" required></asp:DropDownList>
                                                            <div class="invalid-feedback">
                                                                Please select Country.
                                                            </div>
                                                     </div>
                                                </div>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<div class="col-sm-12 button_panel">
														<div class="float-right mt-2">
                                                            <asp:Button ID="btnSave" runat="server" Text="Save"  class="btn btn-primary" OnClick="btnSave_Click" OnClientClick="return Validate();" />
                                                            <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-light" OnClick="btnReset_Click" formnovalidate />
														</div>
													</div>
													</div>
												</div>
											</div>
                                         </div>
										</div>
									</div>
						</div>
					</div>
						
					
				</div>
				<div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 rightmenu">
				<div class="container-fluid">
					<div class="graphite accordion-container-right">
						<ul class="accordion" id="accordion-right">
							<li class="dcjq-current-parent"><a href="#"  class="dcjq-parent active">Shortcuts</a>
								<ul>
									<li>
										<a href="/MyAccount/LoginHistory.aspx">View Login History</a>
									</li>
								</ul>
							</li>
							</ul>
					</div>
				</div>	
				</div>

</asp:Content>