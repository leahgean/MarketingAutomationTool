<%@ Page Title="" Language="C#" MasterPageFile="~/MATSite.Master" AutoEventWireup="true" CodeBehind="MyAccount.aspx.cs" Inherits="MarketingAutomationTool.MyAccount.MyAccount" %>
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
	<script src="../dcjqaccordion-master/accordionmenu.js"   type="text/javascript"></script>
    <script type="text/javascript">
        $( document ).ready(function() {
	
		    $("#lnkAccountDetails").click(function(){
			    $("#lnkAccountDetails").addClass("active");
			    $("#tab_account").addClass("active");
			    $("#lnkCreatorOwner").removeClass("active");
			    $("#tab_creatorowner").removeClass("active");
			    $("#lnkAccountContacts").removeClass("active");
			    $("#tab_accountcontacts").removeClass("active");
			    $("#lnkPreferences").removeClass("active");
			    $("#tab_preferences").removeClass("active");
		    });
		
		    $("#lnkCreatorOwner").click(function(){
			    $("#lnkAccountDetails").removeClass("active");
			    $("#tab_account").removeClass("active");
			    $("#lnkCreatorOwner").addClass("active");
			    $("#tab_creatorowner").addClass("active");
			    $("#lnkAccountContacts").removeClass("active");
			    $("#tab_accountcontacts").removeClass("active");
			    $("#lnkPreferences").removeClass("active");
			    $("#tab_preferences").removeClass("active");
		    });
		
		    $("#lnkAccountContacts").click(function(){
			    $("#lnkAccountDetails").removeClass("active");
			    $("#tab_account").removeClass("active");
			    $("#lnkCreatorOwner").removeClass("active");
			    $("#tab_creatorowner").removeClass("active");
			    $("#lnkAccountContacts").addClass("active");
			    $("#tab_accountcontacts").addClass("active");
			    $("#lnkPreferences").removeClass("active");
			    $("#tab_preferences").removeClass("active");
		    });
		
		    $("#lnkPreferences").click(function(){
			    $("#lnkAccountDetails").removeClass("active");
			    $("#tab_account").removeClass("active");
			    $("#lnkCreatorOwner").removeClass("active");
			    $("#tab_creatorowner").removeClass("active");
			    $("#lnkAccountContacts").removeClass("active");
			    $("#tab_accountcontacts").removeClass("active");
			    $("#lnkPreferences").addClass("active");
			    $("#tab_preferences").addClass("active");
            });

	    });

        function ValidateRequiredFields() {
            var cnt = 0;
            var message = "<ul class='orderedlist'>";
            if ($('#<%=txtAddress.ClientID%>').val() == '') {
                message += "<li><strong>Account Details</strong> Address is a required field.</li>"
                cnt++;
            }
            
            if ($('#<%=txtCity.ClientID%>').val() == '') {
                message += "<li><strong>Account Details</strong> City is a required field.</li>"
                cnt++;
            }
            if ($('#<%=txtState.ClientID%>').val() == '') {
                message += "<li><strong>Account Details</strong> State is a required field.</li>"
                cnt++;
            }
            if ($('#<%=txtPostCode.ClientID%>').val() == '') {
                message += "<li><strong>Account Details</strong> Postcode is a required field.</li>"
                cnt++;
            }
            if ($('#<%=ddlCountry.ClientID%>').val() == '') {
                message += "<li><strong>Account Details</strong> Country is a required field.</li>"
                cnt++;
            }
            if ($('#<%=txtBillingContactName.ClientID%>').val() == '') {
                 message += "<li><strong>Account Contacts</strong> Billing Contact Name is a required field.</li>"
                            cnt++;
            }
            if ($('#<%=txtBillingContactEmailAddress.ClientID%>').val() == '') {
                 message += "<li><strong>Account Contacts</strong> Billing Contact Email Address is a required field.</li>"
                            cnt++;
            }
            if ($('#<%=txtMarketingContactName.ClientID%>').val() == '') {
                 message += "<li><strong>Account Contacts</strong> Marketing Contact Name is a required field.</li>"
                            cnt++;
            }
            if ($('#<%=txtMarketingContanctEmailAddress.ClientID%>').val() == '') {
                 message += "<li><strong>Account Contacts</strong> Marketing Contact Email Address is a required field.</li>"
                            cnt++;
            }
            if ($('#<%=txtTechnicalContactName.ClientID%>').val() == '') {
                 message += "<li><strong>Account Contacts</strong> Technical Contact Name is a required field.</li>"
                            cnt++;
            }
            if ($('#<%=txtTechnicalContactEmailAddress.ClientID%>').val() == '') {
                 message += "<li><strong>Account Contacts</strong> Technical Contact Email Address is a required field.</li>"
                            cnt++;
            }
            if ($('#<%=txtAdminContactName.ClientID%>').val() == '') {
                 message += "<li><strong>Account Contacts</strong> Admin Contact Name is a required field.</li>"
                            cnt++;
            }
            if ($('#<%=txtAdminContactEmailAddress.ClientID%>').val() == '') {
                 message += "<li><strong>Account Contacts</strong> Admin Contact Email Address is a required field.</li>"
                            cnt++;
            }
            if ($('#<%=txtTestEmailAddress1.ClientID%>').val() == '') {
                 message += "<li><strong>Preferences</strong> Test Email Address 1 is a required field.</li>"
                            cnt++;
            }
            if ($('#<%=txtTestEmailAddress2.ClientID%>').val() == '') {
                 message += "<li><strong>Preferences</strong> Test Email Address 2 is a required field.</li>"
                            cnt++;
            }
            if ($('#<%=txtTestEmailAddress3.ClientID%>').val() == '') {
                 message += "<li><strong>Preferences</strong> Test Email Address 3 is a required field.</li>"
                            cnt++;
            }
            message += "</ul>";

            if (cnt > 0) {
                $("#<%= dvMessage.ClientID%>").html(message);
                $("#<%= dvMessage.ClientID%>").addClass("alert alert-danger");
                $("#<%= dvMessage.ClientID%>").css({"display": "block"});

            }
            else {
                 $("#<%= dvMessage.ClientID%>").html("");
                $("#<%= dvMessage.ClientID%>").removeClass("alert alert-danger");
                $("#<%= dvMessage.ClientID%>").css({"display": "none"});
            }

            return (cnt==0);
            
        }

        function Validate() {
            /*bootstrap validation: shows the error messages for required fields; already prevents form submission of a required field is blank*/
            var forms = document.getElementById('dvFormValidate');            
            forms.classList.add('was-validated');

           return ValidateRequiredFields()
        }
    </script>
	<link href="../dcjqaccordion-master/css/skins/graphite.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/override.css" rel="stylesheet" type="text/css" />
    <title>Marketing Automation Tool - My Account</title>
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
            <span class="breadcrumb-item active">My Account</span>
	    </nav>
    </div>
</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="cpBody" runat="server">
    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 leftmenu">
             <uc:LeftMenu runat="server" id="ucLeftMenu"></uc:LeftMenu>
				
				</div>
				<div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 middlecontent">
					<div class="page_header">
							<h5 class="page_title">Account Details</h5>
					</div>
					<div class="page_content">
						<div class="container-fluid">
                            <div class="" role="alert" id="dvMessage" runat="server" style="display:none;">
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                             </div>
							<ul class="nav nav-tabs" id="secondtab">
							  <li class="nav-item">
								<a class="nav-link active" href="#" id="lnkAccountDetails" >Account Details</a>
							  </li>
							  <li class="nav-item">
								<a class="nav-link" href="#"  id="lnkCreatorOwner">Creator/Owner</a>
							  </li>
							  <li class="nav-item">
								<a class="nav-link"  href="#" id="lnkAccountContacts" >Account Contacts</a>
							  </li>
							  <li class="nav-item">
								<a class="nav-link"  href="#" id="lnkPreferences">Preferences</a>
							  </li>
							</ul>
                            <div class="needs-validation" id="dvFormValidate" novalidate>
							<div class="tab-content">
								<div class="tab-pane active" id="tab_account">
									<div class="form-horizontal">
										<div class="container">
                                            <!--div class="needs-validation" id="dvFormValidate" novalidate-->
											    <div class="row">
												    <div class="form-group form-group-sm col-sm-12">
													    <div class="row">
													    <label class="col-form-label col-sm-2" >Company Name:</label>
													    <div class="col-sm-10">
                                                            <Label ID="lblCompanyName" runat="server" class="col-form-label"></Label>
													    </div>
													    </div>
												    </div>
											    </div>
											    <div class="row">
												    <div class="form-group form-group-sm col-sm-12">
													    <div class="row">
													    <label class="col-form-label col-sm-2" >Registration No:</label>
													    <div class="col-sm-10">
                                                            <asp:TextBox ID="txtRegistrationNo" runat="server"  placeholder="Registration No."  class="form-control"></asp:TextBox>
													    </div>
													    </div>
												    </div>
											    </div>
											    <div class="row">
												    <div class="form-group form-group-sm col-sm-12">
													    <div class="row">
													    <label class="col-form-label col-sm-2" >Address:</label>
													    <div class="col-sm-10">
														     <asp:TextBox ID="txtAddress" runat="server"  placeholder="Address"  class="form-control" required></asp:TextBox>
                                                             <div class="invalid-feedback">
                                                              Please enter Address.
                                                             </div>
													    </div>
													    </div>
												    </div>
											    </div>
											    <div class="row">
												    <div class="form-group form-group-sm col-sm-12">
													    <div class="row">
													    <label class="col-form-label col-sm-2" >City:</label>
													    <div class="col-sm-10">
                                                            <asp:TextBox ID="txtCity" runat="server"  placeholder="City"  class="form-control" required></asp:TextBox>
													        <div class="invalid-feedback">
                                                              Please enter City.
                                                             </div>
                                                        </div>
													    </div>
												    </div>
											    </div>
											    <div class="row">
												    <div class="form-group form-group-sm col-sm-12">
													    <div class="row">
													    <label class="col-form-label col-sm-2" >State:</label>
													    <div class="col-sm-10">
                                                            <asp:TextBox ID="txtState" runat="server"  placeholder="State"  class="form-control" required></asp:TextBox>
													        <div class="invalid-feedback">
                                                              Please enter State.
                                                             </div>
                                                        </div>
													    </div>
												    </div>
											    </div>
											    <div class="row">
												    <div class="form-group form-group-sm col-sm-12">
													    <div class="row">
													    <label class="col-form-label col-sm-2" >Postcode:</label>
													    <div class="col-sm-10">
                                                            <asp:TextBox ID="txtPostCode" runat="server"  placeholder="Postcode"  class="form-control" required></asp:TextBox>
													        <div class="invalid-feedback">
                                                              Please enter PostCode.
                                                             </div>
                                                        </div>
													    </div>
												    </div>
											    </div>
											    <div class="row">
												    <div class="form-group form-group-sm col-sm-12">
													    <div class="row">
													    <label class="col-form-label col-sm-2" >Country:</label>
													    <div class="col-sm-10">
														    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" placeholder="Country" required></asp:DropDownList>
													        <div class="invalid-feedback">
                                                              Please select Country.
                                                             </div>
                                                        </div>
													    </div>
												    </div>
											    </div>
											    <div class="row">
												    <div class="form-group form-group-sm col-sm-12">
													    <div class="row">
													    <label class="col-form-label col-sm-2" >Website:</label>
													    <div class="col-sm-10">
                                                            <asp:TextBox ID="txtWebSite" runat="server"  placeholder="Website"  class="form-control"></asp:TextBox>
													    </div>
													    </div>
												    </div>
											    </div>
											    <div class="row">
												    <div class="form-group form-group-sm col-sm-12">
													    <div class="row">
													    <label class="col-form-label col-sm-2" >Personalized URL:</label>
													    <div class="col-sm-10">
                                                             <Label ID="lblPersonalizedURL" runat="server" class="col-form-label"></Label>
													    </div>
													    </div>
												    </div>
											    </div>
                                            <!--/div-->
										</div>
									</div>
								</div>
                                <div class="tab-pane" id="tab_creatorowner">
									<div class="form-horizontal">
										<div class="container">
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >First Name:</label>
													<div class="col-sm-10">
                                                        <Label ID="lblCreatorOwnerFirstName" runat="server" class="col-form-label"></Label>
													</div>
													</div>
												</div>
											</div>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Last Name:</label>
													<div class="col-sm-10">
                                                        <Label ID="lblCreatorOwnerLastName" runat="server" class="col-form-label"></Label>
													</div>
													</div>
												</div>
											</div>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Address:</label>
													<div class="col-sm-10">
                                                        <Label ID="lblCreatorOwnerAddress" runat="server" class="col-form-label"></Label>
													</div>
													</div>
												</div>
											</div>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Position:</label>
													<div class="col-sm-10">
                                                        <Label ID="lblCreatorOwnerPosition" runat="server" class="col-form-label"></Label>
													</div>
													</div>
												</div>
											</div>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Email Address:</label>
													<div class="col-sm-10">
                                                        <Label ID="lblCreatorOwnerEmailAddress" runat="server" class="col-form-label"></Label>
													</div>
													</div>
												</div>
											</div>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Mobile No:</label>
													<div class="col-sm-10">
                                                        <Label ID="lblCreatorOwnerMobileNo" runat="server" class="col-form-label"></Label>
													</div>
													</div>
												</div>
											</div>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Phone Number:</label>
													<div class="col-sm-10">
                                                         <Label ID="lblCreatorOwnerPhoneNumber" runat="server" class="col-form-label"></Label>
													</div>
													</div>
												</div>
											</div>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Username:</label>
													<div class="col-sm-10">
                                                         <Label ID="lblCreatorOwnerUserName" runat="server" class="col-form-label"></Label>
													</div>
													</div>
												</div>
											</div>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Last Login:</label>
													<div class="col-sm-10">
                                                          <Label ID="lblLastLoginDate" runat="server" class="col-form-label"></Label>
													</div>
													</div>
												</div>
											</div>
										</div>
									</div>
								</div>
								<div class="tab-pane" id="tab_accountcontacts">
									<div class="form-horizontal">
										<div class="container">
											<legend class="legendstyle">Billing Contact</legend>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Name:</label>
													<div class="col-sm-10">
                                                        <asp:TextBox ID="txtBillingContactName" runat="server"  placeholder="Name"  class="form-control" required></asp:TextBox>
                                                        <div class="invalid-feedback">
                                                              Please enter Billing Contact Name.
                                                        </div>
													</div>
													</div>
												</div>
											</div>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Email Address:</label>
													<div class="col-sm-10">
                                                        <asp:TextBox ID="txtBillingContactEmailAddress" runat="server"  placeholder="Email Address"  class="form-control" required></asp:TextBox>
                                                        <div class="invalid-feedback">
                                                              Please enter Billing Email Address.
                                                        </div>
													</div>
													</div>
												</div>
											</div>
											<legend class="legendstyle">Marketing Contact</legend>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Name:</label>
													<div class="col-sm-10">
                                                         <asp:TextBox ID="txtMarketingContactName" runat="server"  placeholder="Name"  class="form-control" required></asp:TextBox>
                                                         <div class="invalid-feedback">
                                                              Please enter Marketing Contact Name.
                                                          </div>
													</div>
													</div>
												</div>
											</div>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Email Address:</label>
													<div class="col-sm-10">
                                                         <asp:TextBox ID="txtMarketingContanctEmailAddress" runat="server"  placeholder="Email Address"  class="form-control" required></asp:TextBox>
                                                        <div class="invalid-feedback">
                                                              Please enter Marketing Email Address.
                                                        </div>
													</div>
													</div>
												</div>
											</div>
											<legend class="legendstyle">Technical Contact</legend>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Name:</label>
													<div class="col-sm-10">
                                                        <asp:TextBox ID="txtTechnicalContactName" runat="server"  placeholder="Name"  class="form-control" required></asp:TextBox>
                                                        <div class="invalid-feedback">
                                                              Please enter Technical Contact Name.
                                                        </div>
													</div>
													</div>
												</div>
											</div>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Email Address:</label>
													<div class="col-sm-10">
														  <asp:TextBox ID="txtTechnicalContactEmailAddress" runat="server"  placeholder="Email Address"  class="form-control" required></asp:TextBox>
                                                         <div class="invalid-feedback">
                                                              Please enter Technical Contact Email Address.
                                                        </div>
													</div>
													</div>
												</div>
											</div>
											<legend class="legendstyle">Admin Contact</legend>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Name:</label>
													<div class="col-sm-10">
                                                         <asp:TextBox ID="txtAdminContactName" runat="server"  placeholder="Name"  class="form-control" required></asp:TextBox>
                                                         <div class="invalid-feedback">
                                                              Please enter Admin Contact Name.
                                                        </div>
													</div>
													</div>
												</div>
											</div>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Email Address:</label>
													<div class="col-sm-10">
                                                         <asp:TextBox ID="txtAdminContactEmailAddress" runat="server"  placeholder="Email Address"  class="form-control" required></asp:TextBox>
                                                        <div class="invalid-feedback">
                                                              Please enter Admin Contact Email Address.
                                                        </div>
													</div>
													</div>
												</div>
											</div>
										</div>
									</div>
								</div>
								<div class="tab-pane" id="tab_preferences">
									<div class="form-horizontal">
										<div class="container">
											<legend class="legendstyle">Test Email Addresses</legend>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Email Address 1:</label>
													<div class="col-sm-10">
                                                        <asp:TextBox ID="txtTestEmailAddress1" runat="server"  placeholder="Email Address 1"  class="form-control" required></asp:TextBox>
                                                        <div class="invalid-feedback">
                                                              Please enter Test Email Address 1.
                                                        </div>
													</div>
													</div>
												</div>
											</div>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Email Address 2:</label>
													<div class="col-sm-10">
                                                        <asp:TextBox ID="txtTestEmailAddress2" runat="server"  placeholder="Email Address 2"  class="form-control" required></asp:TextBox>
                                                        <div class="invalid-feedback">
                                                              Please enter Test Email Address 2.
                                                        </div>
													</div>
													</div>
												</div>
											</div>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Email Address 3:</label>
													<div class="col-sm-10">
                                                        <asp:TextBox ID="txtTestEmailAddress3" runat="server"  placeholder="Email Address 3"  class="form-control" required></asp:TextBox>
                                                        <div class="invalid-feedback">
                                                              Please enter Test Email Address 3.
                                                        </div>
													</div>
													</div>
												</div>
											</div>
											<div style="display:none;">
                                                <legend class="legendstyle">Test Mobile Numbers</legend>
											    <div class="row">
												    <div class="form-group form-group-sm col-sm-12">
													    <div class="row">
													    <label class="col-form-label col-sm-2" >Mobile Number 1:</label>
													    <div class="col-sm-10">
																     <div class="input-group mb-3">
																	    <div class="input-group-prepend">
																	      <span class="input-group-text">+</span>
																	    </div>
                                                                         <asp:TextBox ID="txtTestMobile1" runat="server"  placeholder="Mobile No 1"  class="form-control"></asp:TextBox>
																    </div>
															    </div>
													    </div>
												    </div>
											    </div>
											    <div class="row">
												    <div class="form-group form-group-sm col-sm-12">
													    <div class="row">
													    <label class="col-form-label col-sm-2" >Mobile Number 2:</label>
													    <div class="col-sm-10">
																     <div class="input-group mb-3">
																	    <div class="input-group-prepend">
																	      <span class="input-group-text">+</span>
																	    </div>
                                                                         <asp:TextBox ID="txtTestMobile2" runat="server"  placeholder="Mobile No 2"  class="form-control"></asp:TextBox>
																    </div>
															    </div>
													    </div>
												    </div>
											    </div>
											    <div class="row">
												    <div class="form-group form-group-sm col-sm-12">
													    <div class="row">
													    <label class="col-form-label col-sm-2" >Mobile Number 3:</label>
													    <div class="col-sm-10">
																     <div class="input-group mb-3">
																	    <div class="input-group-prepend">
																	      <span class="input-group-text">+</span>
																	    </div>
                                                                          <asp:TextBox ID="txtTestMobile3" runat="server"  placeholder="Mobile No 3"  class="form-control"></asp:TextBox>
																    </div>
															    </div>
													    </div>
												    </div>
											    </div>
                                            </div>
										</div>
									</div>
								</div>
                                <div class="form-horizontal">
                                    <div class="container">
                                        <div class="row">
												    <div class="form-group form-group-sm col-sm-12">
													    <div class="row">
													    <div class="col-sm-12 button_panel">
														    <div class="float-right mt-2">
                                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"  OnClick="btnSave_Click" OnClientClick="return Validate();" />
															    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-light" OnClick="btnReset_Click" formnovalidate />
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
				</div>
				<div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 rightmenu">
                      <div class="container-fluid">
					    <div class="graphite accordion-container-right">
						    <ul class="accordion" id="accordion-right">
							    <li class="dcjq-current-parent"><a href="#"  class="dcjq-parent active">Shortcuts</a>
								    <ul>
									    <li>
										    <asp:LinkButton ID="lnkViewUsers" runat="server" Text="View Users" OnClick="lnkViewUsers_Click"></asp:LinkButton>
									    </li>
								    </ul>
							    </li>
							    </ul>
					    </div>
				    </div>	
				</div>
</asp:Content>
