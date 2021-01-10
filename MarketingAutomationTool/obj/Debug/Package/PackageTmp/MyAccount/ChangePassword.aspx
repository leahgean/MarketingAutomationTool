<%@ Page Title="" Language="C#" MasterPageFile="~/MATSite.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="MarketingAutomationTool.MyAccount.ChangePassword" %>
<%@ Register Src="~/UserControls/LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/NewEmailCampaign.ascx" TagName="NewEmailCampaign" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
    <link rel="stylesheet" href="../dcjqaccordion-master/css/dcaccordion.css"/>
	
	<script src="../bootstrap/assets/js/vendor/jquery-slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="../bootstrap/assets/js/vendor/popper.min.js" integrity="sha384-cs/chFZiN24E4KMATLdqdvsezGxaGsi4hLGOzlXwp5UZB1LY//20VyM2taTB4QvJ" crossorigin="anonymous"></script>
    <script src="../bootstrap/js/bootstrap.min.js" integrity="sha384-uefMccjFJAIv6A+rW+L4AHf99KvxDjWSu1z9VI8SKNVmz4sk7buKt/6v9KI65qnm" crossorigin="anonymous"></script>
	<script src="../jQuery/jquery-3.3.1.min.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.cookie.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.hoverIntent.minified.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.dcjqaccordion.2.7.min.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/accordionmenu.js"   type="text/javascript"></script>
    <script type="text/javascript">
        function validateEmail(email) {
              var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
              return re.test(email);
        }

        function ValidateRequiredFields() {
           
            if ($('#<%=txtOldPassword.ClientID%>').val() == '') return false;
            if ($('#<%=txtNewPassword.ClientID%>').val() == '') return false;
            if ($('#<%=txtConfirmNewPassword.ClientID%>').val() == '') return false;
            return true;
            
        }

       

        function Validate() {
            /*bootstrap validation: shows the error messages for required fields; already prevents form submission of a required field is blank*/
            var forms = document.getElementById('dvFormValidate');            
            forms.classList.add('was-validated');

            return ValidateRequiredFields();
            
            
        }
    </script>
	<link href="../dcjqaccordion-master/css/skins/graphite.css" rel="stylesheet" type="text/css" />
    <title>Marketing Automation Tool - Change Password</title>
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
            <span class="breadcrumb-item active">Change Password</span>
	    </nav>
    </div>
</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="cpBody" runat="server">
    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 leftmenu">
             <uc:LeftMenu runat="server" id="ucLeftMenu"></uc:LeftMenu>
				
				</div>
				<div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 middlecontent">
					<div class="page_header">
						<h5 class="page_title">Change Password</h5>
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
											    <div class="row">
												    <div class="form-group form-group-sm col-sm-12">
													    <div class="row">
													    <label class="col-form-label col-sm-2" >User Name:</label>
													    <div class="col-sm-10">
														    <label class="col-form-label"  ><%= UserName%></label>
													    </div>
													    </div>
												    </div>
											    </div>
											    <div class="row">
													    <div class="form-group form-group-sm col-sm-12">
														    <div class="row">
															    <label class="col-form-label col-sm-2" >Old Password:</label>
															    <div class="col-sm-10">
                                                                     <asp:TextBox ID="txtOldPassword" runat="server" CssClass="form-control" placeholder="Old Password" MaxLength="10" TextMode="Password" required ></asp:TextBox>
															            <div class="invalid-feedback">
                                                                            Please enter Old Password.
                                                                        </div>
                                                                </div>
														    </div>
													    </div>
											    </div>
											    <div class="row">
													    <div class="form-group form-group-sm col-sm-12">
														    <div class="row">
															    <label class="col-form-label col-sm-2" >New Password:</label>
															    <div class="col-sm-10">
                                                                    <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" placeholder="New Password" MaxLength="10" TextMode="Password" required></asp:TextBox>
															         <div class="invalid-feedback">
                                                                            Please enter New Password.
                                                                        </div>
                                                                </div>
														    </div>
													    </div>
											    </div>
											    <div class="row">
													    <div class="form-group form-group-sm col-sm-12">
														    <div class="row">
															    <label class="col-form-label col-sm-2" >Confirm New Password:</label>
															    <div class="col-sm-10">
																     <asp:TextBox ID="txtConfirmNewPassword" runat="server" CssClass="form-control" placeholder="Confirm New Password" MaxLength="10" TextMode="Password" required></asp:TextBox>
															         <div class="invalid-feedback">
                                                                            Please confirm New Password.
                                                                        </div>
                                                                </div>
														    </div>
													    </div>
											    </div>
											    <div class="row">
														    <div class="form-group form-group-sm col-sm-12">
															    <div class="row">
															    <div class="col-sm-12 button_panel">
																    <div class="float-right mt-2">
																	   <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary"  OnClientClick="return Validate();" OnClick="btnSave_Click"  />
																	    <asp:Button ID="btnClear" runat="server" Text="Clear" class="btn btn-light"   formnovalidate OnClick="btnClear_Click"   />
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
										    <a href="/MyAccount/UserDetails.aspx?c=4&userid=<%= LoggedUserId %>">View User Details</a>
									    </li>
								    </ul>
							    </li>
							    </ul>
					    </div>
				    </div>	
				</div>
</asp:Content>
