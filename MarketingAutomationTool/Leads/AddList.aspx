<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddList.aspx.cs" Inherits="MarketingAutomationTool.Leads.AddList" MasterPageFile="~/MATSite.Master" %>
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
        $(document).ready(function () {
            $("#lnkFilter").click(function () {
                $("#collapsePanel").toggleClass("show");
            });

            if ($('.sidescroller').length) {
                $('.sidescroller').css('min-height', '360px');
                $('.sidescroller').css('width', 'auto');
            }

            $(".contacts-list li").hover(
                function () { $(this).children('.contactdetail-icons').show(); },
                function () {
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

            $("#<%=txtDescription.ClientID%>").keydown(function() {
                LimitText($("#<%=txtDescription.ClientID%>"),500);
            });

            $("#<%=txtDescription.ClientID%>").keyup(function() {
                LimitText($("#<%=txtDescription.ClientID%>"),500);
            });
        });

        function LimitText(textfield,maxlength) {
            var text = textfield.val();
            if (text.trim().length > maxlength) {
                    textfield.val(text.substring(0,maxlength));
            }
        }

        function ValidateRequiredFields() {

            if ($('#<%=txtName.ClientID%>').val() == '') return false;
            return true;

        }

        function Validate() {
            /*bootstrap validation: shows the error messages for required fields; already prevents form submission of a required field is blank*/
            var forms = document.getElementById('dvFormValidate');
            forms.classList.add('was-validated');

            return ValidateRequiredFields();
        }

        function ConfirmDelete() {
            return window.confirm("Are you sure you want to delete this lead?");
        }
	</script>
	<link href="../dcjqaccordion-master/css/skins/graphite.css" rel="stylesheet" type="text/css" />
    <title>Marketing Automation Tool - Add List</title>
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
				  <asp:LinkButton ID="lnkBreadHome" runat="server" CssClass="breadcrumb-item"  Text="Home" OnClick="lnkBreadHome_Click"></asp:LinkButton>
				  <a class="breadcrumb-item" href="Lead.aspx">Leads</a>
                  <span class="breadcrumb-item active">Add New List</span>
				</nav>
			</div>
</asp:Content>  
<asp:Content ID="Content5" ContentPlaceHolderID="cpBody" runat="server"> 
    <uc:LeftSearch runat="server" id="LeadLeftSearch"></uc:LeftSearch>
				<div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 middlecontent">
					<div class="page_header">
						<h5 class="page_title">List : New List</h5>
					</div>
					<div class="page_content">
						<div class="container-fluid">
							<ul class="nav nav-tabs" id="secondtab">
							  <li class="nav-item">
								<a class="nav-link active" href="#" id="lnkListInformation" >List Information</a>
							  </li>
							</ul>
							<div class="tab-content">
							<div class="tab-pane active" id="tab_listinfo">
									<div class="form-horizontal">
										<div class="container">
                                            <div class="needs-validation" id="dvFormValidate" novalidate>
	                                            <div class="" role="alert" id="dvMessage" runat="server" style="display:none;">
        	                                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                	                                            <span aria-hidden="true">&times;</span>
                                                            </button>
	                                            </div>
											    <legend class="legendstyle">List Information</legend>
                                                <div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="form-group row required">
															<label class="col-form-label col-sm-2" >Name:</label>
															<div class="col-sm-10">
                                                                <asp:TextBox ID="txtName" runat="server" class="form-control" placeholder="Name"  MaxLength="100"  required></asp:TextBox>
                                                                <div id="invalidName" class="invalid-feedback">
                                                                  Please enter Name.
                                                                </div>
															</div>
														</div>
													</div>
											    </div>
                                                <div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="form-group row">
															<label class="col-form-label col-sm-2" >Description:</label>
															<div class="col-sm-10">
                                                                 <asp:TextBox ID="txtDescription" runat="server" class="form-control" placeholder="Description"  MaxLength="500" TextMode="MultiLine" Height="125px" ></asp:TextBox>
															</div>
														</div>
													</div>
											    </div>
											    <div class="row">
												    <div class="form-group form-group-sm col-sm-12">
													    <div class="row">
													    <div class="col-sm-12 button_panel">
														    <div class="float-right mt-2">
                                                                <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text ="Save" OnClientClick="return Validate();" OnClick="btnSave_Click" />
                                                                <asp:Button ID="btnCancel" runat="server" class="btn btn-light" Text="Reset" OnClick="btnCancel_Click" formnovalidate />
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


