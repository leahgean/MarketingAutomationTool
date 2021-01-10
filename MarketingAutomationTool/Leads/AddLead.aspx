<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddLead.aspx.cs" Inherits="MarketingAutomationTool.Leads.AddLead" MasterPageFile="~/MATSite.Master" EnableEventValidation="false" %>
<%@ Register Src="~/Leads/UserControls/Lead.ascx" TagName="LeadTopMenu" TagPrefix="uc" %>
<%@ Register Src="~/Leads/UserControls/LeftSearch.ascx" TagName="LeftSearch" TagPrefix="uc" %>
<%@ Register Src="~/Leads/UserControls/ViewRecentSearches.ascx" TagName="ViewRecentSearches" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/NewEmailCampaign.ascx" TagName="NewEmailCampaign" TagPrefix="uc" %>
<%@ Register Src="~/Leads/UserControls/Shortcuts.ascx" TagName="Shortcuts" TagPrefix="uc" %>
<%@ Register Src="~/Leads/UserControls/LeadLists.ascx" TagName="LeadLists" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server"> 
    <link rel="stylesheet" href="../dcjqaccordion-master/css/dcaccordion.css" />

   <script src="../jQuery/jquery-3.3.1.min.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.cookie.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.hoverIntent.minified.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.dcjqaccordion.2.7.min.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/accordionmenu.js" type="text/javascript"></script>
	
	<script type="text/javascript">

        $(document).ready(function(){
            $("#lnkFilter").click(function () {
                $("#collapsePanel").toggleClass("show");
             });
 
			if ($('.sidescroller').length) {
				$('.sidescroller').css('min-height', '360px');
				$('.sidescroller').css('width', 'auto');
			}
			
			$(".contacts-list li").hover(
				function () { $(this).children('.contactdetail-icons').show(); },
				function () 
				{ 
					$(this).children('.contactdetail-icons').hide(); 
					$('.sideMenu').tooltip('hide');
				}
			);
			
			if ($('.sideMenu').length) {
				try {
                    $('.sideMenu').tooltip();
                }
                catch (err) {

                };
            };

            //contactStatusJsonList
            var contactStatusJsonList = {"Lead" : 
            [
                    {"Value" : "1","Text" : "Not Confirmed"},
                    {"Value" : "2","Text" : "Confirmed"}
            ],
            "Contact" : 
            [
                    {"Value" : "3","Text" : "Active"},
                    {"Value" : "4","Text" : "Inactive"}
            ],
            };

           //Populate ddlStatus
           var updateContactStatus = function(contacttype) {
                var listItems= "";
                for (var i = 0; i < contactStatusJsonList[contacttype].length; i++){
                    listItems+= "<option value='" + contactStatusJsonList[contacttype][i].Value + "'>" + contactStatusJsonList[contacttype][i].Text + "</option>";
                }
                $("#<%= ddlStatus.ClientID%>").html(listItems);
            }

            //ddlType onChange event
            $("#<%= ddlType.ClientID%>").on('change',function(){
                var selectedType = $('#<%= ddlType.ClientID%> option:selected').text();
                updateContactStatus(selectedType);
                 $('#<%= hdnStatus.ClientID%>').val($('#<%= ddlStatus.ClientID%> option:selected').val());
            });  

            //ddlStatus onchange event
             $("#<%= ddlStatus.ClientID%>").on('change',function(){
                $('#<%= hdnStatus.ClientID%>').val($('#<%= ddlStatus.ClientID%> option:selected').val());
            });  

             //populate contact status on load
            updateContactStatus($('#<%= ddlType.ClientID%> option:selected').text());
            $('#<%= hdnStatus.ClientID%>').val($('#<%= ddlStatus.ClientID%> option:selected').val());
			
        });

        function validateEmail(email) {
              var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
              return re.test(email);
        }

        function ValidateEmailAddress() {
                    if (validateEmail($('#<%=txtEmail.ClientID%>').val()))
                    {
                         return true;
                    }
                    else {
                        $("#invalidEmail").text('Please enter a valid Email Address. e.g. example@mail.com');
                        $("#invalidEmail").show();
                        $("#<%=txtEmail.ClientID%>").css({"border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)"});
                        return false;
                    }
        }

        function ValidateRequiredFields() {
           
            if ($('#<%=txtFirstName.ClientID%>').val() == '') return false;
            if ($('#<%=txtLastName.ClientID%>').val() == '') return false;

            if ($('#<%=txtEmail.ClientID%>').val() == '') {
                return false; 
            }
            else {
                return ValidateEmailAddress();
            }
            
            return true;
            
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

        function ConfirmDelete() {
            return window.confirm("Are you sure you want to delete this lead?");
        }
    </script>
	<link href="../dcjqaccordion-master/css/skins/graphite.css" rel="stylesheet" type="text/css" />
    <title>Marketing Automation Tool - Add Lead</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpShortCut" runat="server"> 
   <uc:NewEmailCampaign runat="server" id="NewEmailCampaign"></uc:NewEmailCampaign>
</asp:Content>  
<asp:Content ID="Content3" ContentPlaceHolderID="cpTabHeaderBottom" runat="server"> 
    <uc:LeadTopMenu runat="server" id="ucLeadTopMenu"></uc:LeadTopMenu>
</asp:Content>  
<asp:Content ID="Content4" ContentPlaceHolderID="cpRowCrumbs" runat="server"> 
   <div class="container-fluid">
				<nav class="breadcrumb">
				  <asp:LinkButton ID="lnkBreadHome" runat="server" CssClass="breadcrumb-item" OnClick="lnkBreadHome_Click" Text="Home"></asp:LinkButton>
				  <a class="breadcrumb-item" href="Lead.aspx">Leads</a>
                  <span class="breadcrumb-item active">Add New Lead</span>
				</nav>
			</div>
</asp:Content>  
<asp:Content ID="Content5" ContentPlaceHolderID="cpBody" runat="server"> 
    <uc:LeftSearch runat="server" id="LeadLeftSearch"></uc:LeftSearch>
				<div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 middlecontent">
					<div class="page_header">
						<h5 class="page_title">New Lead</h5>
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
								            <legend class="legendstyle">General Information</legend>
                                            <div class="form-group row">
                                                    <label for="ddlTitle" class="col-sm-2 col-form-label">Title:</label>
                                                    <div class="col-sm-10">
                                                         <asp:DropDownList ID="ddlTitle" class="form-control" runat="server" style="width:180px">
                                                                    <asp:ListItem Value="" Text="Please select Title"></asp:ListItem>
                                                                    <asp:ListItem Value="Mr" Text="Mr"></asp:ListItem>
                                                                    <asp:ListItem Value="Ms" Text="Ms"></asp:ListItem>
                                                                    <asp:ListItem Value="Mrs" Text="Mrs"></asp:ListItem>
                                                                    <asp:ListItem Value="Dr" Text="Dr"></asp:ListItem>
                                                                    <asp:ListItem Value="Engr" Text="Engr">Engr</asp:ListItem>
                                                                </asp:DropDownList>
                                                    </div>
                                             </div>
                                            <div class="form-group row required">
                                                    <label for="txtFirstName" class="col-sm-2 col-form-label">First Name:</label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox runat="server" ID="txtFirstName" class="form-control" placeholder="First Name" MaxLength="100" required></asp:TextBox>
                                                                <div id="invalidFirstName" class="invalid-feedback">
                                                                  Please enter First Name.
                                                                </div>
                                                    </div>
                                            </div>
                                            <div class="form-group row required">
                                                    <label for="txtFirstName" class="col-sm-2 col-form-label">Middle Name:</label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox runat="server" ID="txtMiddleName" class="form-control" placeholder="Middle Name" MaxLength="100" ></asp:TextBox>
                                                                <div class="invalid-feedback">
                                                                  Please enter Middle Name.
                                                                </div>
                                                    </div>
                                            </div>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="form-group row required">
															<label class="col-form-label col-sm-2" >Last Name:</label>
															<div class="col-sm-10">
																<asp:TextBox runat="server" ID="txtLastName" class="form-control" placeholder="Last Name" MaxLength="100" required ></asp:TextBox>
                                                                <div id="invalidLastName" class="invalid-feedback">
                                                                  Please enter Last Name.
                                                                </div>
															</div>
														</div>
													</div>
											</div>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >Company:</label>
															<div class="col-sm-10">
                                                                <asp:TextBox runat="server" ID="txtCompany" class="form-control" placeholder="Company" MaxLength="100" ></asp:TextBox>
															</div>
														</div>
													</div>
											</div>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >Website:</label>
															<div class="col-sm-10">
                                                                <asp:TextBox runat="server" ID="txtWebsite" class="form-control" placeholder="Website" MaxLength="100" ></asp:TextBox>
															</div>
														</div>
													</div>
											</div>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >Position:</label>
															<div class="col-sm-10">
                                                                <asp:TextBox runat="server" ID="txtPosition" class="form-control" placeholder="Position" MaxLength="50" ></asp:TextBox>
														    </div>
														</div>
													</div>
											</div>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >Type:</label>
															<div class="col-sm-10">
                                                                <asp:DropDownList ID="ddlType" class="form-control" runat="server" style="width:180px">
                                                                    <asp:ListItem Value="1" Text="Lead"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="Contact"></asp:ListItem>
                                                                </asp:DropDownList>
															</div>
														</div>
													</div>
											</div>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >Status:</label>
															<div class="col-sm-10">
                                                                <asp:DropDownList ID="ddlStatus" class="form-control" runat="server" style="width:180px">
                                                                </asp:DropDownList>
                                                                <input type="hidden" runat="server" id="hdnStatus"/>
															</div>
														</div>
													</div>
											</div>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >Gender:</label>
															<div class="col-sm-10">
                                                                 <asp:DropDownList ID="ddlGender" class="form-control" runat="server" style="width:180px">
                                                                    <asp:ListItem Value="" Text="Please select Gender"></asp:ListItem>
                                                                    <asp:ListItem Value="M" Text="Male"></asp:ListItem>
                                                                    <asp:ListItem Value="F" Text="Female"></asp:ListItem>
                                                                </asp:DropDownList>
															</div>
														</div>
													</div>
											</div>
											<legend class="legendstyle">Lead Information</legend>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="form-group row required">
															<label class="col-form-label col-sm-2" >Email Address:</label>
															<div class="col-sm-10">
																 <asp:TextBox runat="server" ID="txtEmail" class="form-control" placeholder="Email Address" MaxLength="256" required></asp:TextBox>
                                                                <div id="invalidEmail" class="invalid-feedback">
                                                                  Please enter Email Address.
                                                                </div>
															</div>
														</div>
													</div>
											</div>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >Phone No:</label>
															<div class="col-sm-10">
																<div class="input-group mb-3">
																			<div class="input-group-prepend">
																			  <span class="input-group-text">+</span>
																			</div>
																			 <asp:TextBox runat="server" ID="txtPhoneNo" class="form-control" placeholder="Phone No" MaxLength="20" ></asp:TextBox>
																		</div>
															</div>
														</div>
													</div>
											</div>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >Mobile:</label>
															<div class="col-sm-10">
																<div class="input-group mb-3">
																			<div class="input-group-prepend">
																			  <span class="input-group-text">+</span>
																			</div>
                                                                            <asp:TextBox runat="server" ID="txtMobile" class="form-control" placeholder="Mobile" MaxLength="20" ></asp:TextBox>
																		</div>
															</div>
														</div>
													</div>
											</div>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >Facebook ID:</label>
															<div class="col-sm-10">
																<div class="input-group mb-3">
																			<div class="input-group-prepend">
																			  <span class="input-group-text">https://facebook.com/</span>
																			</div>
																			<asp:TextBox runat="server" ID="txtFacebookID" class="form-control" placeholder="Facebook ID" MaxLength="50" ></asp:TextBox>
																		</div>
															</div>
														</div>
													</div>
											</div>
											<legend class="legendstyle">Address Information</legend>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >Address 1:</label>
															<div class="col-sm-10">
																 <asp:TextBox runat="server" ID="txtAddress1" class="form-control" placeholder="Address 1" MaxLength="250" ></asp:TextBox>
															</div>
														</div>
													</div>
											</div>
                                         <div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >Address 2:</label>
															<div class="col-sm-10">
																 <asp:TextBox runat="server" ID="txtAddress2" class="form-control" placeholder="Address 2" MaxLength="250" ></asp:TextBox>
															</div>
														</div>
													</div>
											</div>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >City:</label>
															<div class="col-sm-10">
                                                                 <asp:TextBox runat="server" ID="txtCity" class="form-control" placeholder="City" MaxLength="50" ></asp:TextBox>
															</div>
														</div>
													</div>
											</div>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >State/Region:</label>
															<div class="col-sm-10">
																 <asp:TextBox runat="server" ID="txtState" class="form-control" placeholder="State/Region" MaxLength="50" ></asp:TextBox>
															</div>
														</div>
													</div>
											</div>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >Country:</label>
															<div class="col-sm-10">
                                                                <asp:DropDownList ID="ddlCountry" class="form-control" runat="server" style="width:300px">
                                                                </asp:DropDownList>
															</div>
														</div>
													</div>
											</div>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >Postcode:</label>
															<div class="col-sm-10">
                                                                 <asp:TextBox runat="server" ID="txtPostcode" class="form-control" placeholder="Postcode" MaxLength="20" ></asp:TextBox>
															</div>
														</div>
													</div>
											</div>
											<legend class="legendstyle">Current Subscription</legend>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >Subscribed To:</label>
															<div class="col-sm-10">
																<div class="form-check">
																  <label class="col-form-label col-sm-10" >
                                                                      <asp:CheckBox ID="cbSubscribedToEmail"  class="form-check-input" runat="server" Checked="true" />Email
																  </label>
																</div>
															</div>
														</div>
													</div>
											</div>
											<legend class="legendstyle">Test Lead</legend>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
														<div class="col-sm-2">
																&nbsp;
															</div>
															<div class="col-sm-10">
																<div class="form-check">
																  <label class="col-form-label col-sm-10" >
                                                                      <asp:CheckBox ID="chkUseForTesting"  class="form-check-input" runat="server" />Use for Testing
																  </label>
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
                                                                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return Validate();" />
                                                                    <asp:Button ID="btnCancel" class="btn btn-light" runat="server" Text="Cancel" OnClick="btnCancel_Click" formnovalidate  />
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
							<uc:Shortcuts runat="server" id="Shortcuts"></uc:Shortcuts>
                            <uc:ViewRecentSearches runat="server" id="ViewRecentSearches"></uc:ViewRecentSearches>
							<uc:LeadLists runat="server" id="LeadLists"></uc:LeadLists>
						</ul>
					</div>
				</div>	
				</div>					
</asp:Content>  
