<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportLeads.aspx.cs" Inherits="MarketingAutomationTool.Leads.ImportLeads" MasterPageFile="~/MATSite.Master"%>
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

        });

        function ValidateRequiredFields() {
           
            if ($('#<%=txtJobName.ClientID%>').val() == '') return false;

            if ($('#<%=fleUpload.ClientID%>').val() == '') {
                $("#invalidFile").text('Please select a valid excel file.');
                $("#invalidFile").show();
                $("#<%=fleUpload.ClientID%>").css({ "border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)" });
                return false;
            }
            else {
                $("#invalidFile").text('');
                $("#invalidFile").hide();
                $("#<%=fleUpload.ClientID%>").css({ "border-style": "none" });
            }       
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
    <title>Marketing Automation Tool - Import Leads From Excel</title>
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
                  <span class="breadcrumb-item active">Import Leads From Excel</span>
				</nav>
			</div>
</asp:Content>  
<asp:Content ID="Content5" ContentPlaceHolderID="cpBody" runat="server"> 
    <uc:LeftSearch runat="server" id="LeadLeftSearch"></uc:LeftSearch>
				<div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 middlecontent">
					<div class="page_header">
						<h5 class="page_title">Import Leads/Contacts</h5>
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
                                                    <label for="txtJobName" class="col-sm-2 col-form-label">Job Name:</label>
                                                    <div class="col-sm-10">
                                                        <asp:TextBox ID="txtJobName" runat="server" class="form-control" placeholder="Job Name" MaxLength="50" required></asp:TextBox>
                                                                <div class="invalid-feedback">
                                                                  Please enter valid Job Name.
                                                                </div>
                                                    </div>
                                            </div>
                                            <div class="form-group row required">
                                                    <label for="fleUpload" class="col-sm-2 col-form-label">Select file to upload:</label>
                                                    <div class="col-sm-10">
                                                        <asp:FileUpload ID="fleUpload" runat="server" class="mat-form-fileupload" required/>
                                                                <div id="invalidFile" class="invalid-feedback">
                                                                  Please select a valid file.
                                                                </div>
                                                    </div>
                                            </div>
                                            <div class="form-group row">
                                                    <label for="ddlList" class="col-sm-2 col-form-label">Add to a list:</label>
                                                    <div class="col-sm-10">
                                                        <asp:DropDownList ID="ddlList" runat="server" class="form-control"></asp:DropDownList>
                                                    </div>
                                            </div>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<div class="col-sm-12 button_panel">
														<div class="float-right mt-2">
                                                            <asp:Button ID="btnUpload" runat="server" Text="Upload" class="btn btn-primary" OnClick="btnUpload_Click" OnClientClick="return Validate();" />
															<asp:Button ID="btnViewExistingJobs" runat="server" Text="View Existing Jobs" class="btn btn-light" OnClick="btnViewExistingJobs_Click" formnovalidate />
														</div>
													</div>
													</div>
												</div>
											</div>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<div class="col-sm-12 card bg-light mb-3">
														  <div class="card-body">
															<h6>Things to remember for a successful upload:</h6>
															<br/>
															<ul class="importreminders">
																<li>Use an import template. Click <a href="../HttpHandler/Download.ashx?file=contact_template">here</a> to download.
																</li>
																<li>Duplicate email addresses will not be added to the system.
																</li>
															</ul>
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
