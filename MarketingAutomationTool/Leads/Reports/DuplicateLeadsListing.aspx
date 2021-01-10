<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DuplicateLeadsListing.aspx.cs" Inherits="MarketingAutomationTool.Leads.Reports.DuplicateLeadsListing" MasterPageFile="~/MATSite.Master" EnableEventValidation="false"  %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Src="~/Leads/UserControls/Lead.ascx" TagName="LeadTopMenu" TagPrefix="uc" %>
<%@ Register Src="~/Leads/UserControls/LeftSearch.ascx" TagName="LeftSearch" TagPrefix="uc" %>
<%@ Register Src="~/Leads/UserControls/ViewRecentSearches.ascx" TagName="ViewRecentSearches" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/NewEmailCampaign.ascx" TagName="NewEmailCampaign" TagPrefix="uc" %>
<%@ Register Src="~/Leads/UserControls/Shortcuts.ascx" TagName="Shortcuts" TagPrefix="uc" %>
<%@ Register Src="~/Leads/UserControls/LeadLists.ascx" TagName="LeadLists" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server"> 
    <link rel="stylesheet" href="../../dcjqaccordion-master/css/dcaccordion.css" />

    <script src="../../jQuery/jquery-3.3.1.min.js" type="text/javascript"></script>
	<script src="../../dcjqaccordion-master/js/jquery.cookie.js" type="text/javascript"></script>
	<script src="../../dcjqaccordion-master/js/jquery.hoverIntent.minified.js" type="text/javascript"></script>
	<script src="../../dcjqaccordion-master/js/jquery.dcjqaccordion.2.7.min.js" type="text/javascript"></script>
	<script src="../../dcjqaccordion-master/accordionmenu.js" type="text/javascript"></script>
	
	<script type="text/javascript">

        $(document).ready(function(){
            $("#lnkFilter").click(function () {
                $("#collapsePanel").toggleClass("show");
            });

            $("#lnkParameterFilter").click(function () {
                $("#paramCollapsePanel").toggleClass("show");
            });

            $("#btnClose").click(function () {
                $("#paramCollapsePanel").toggleClass("show");
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
		
	
        function ConfirmDelete() {
            return window.confirm("Are you sure you want to delete this lead?");
        }

    </script>
	<link href="../../dcjqaccordion-master/css/skins/graphite.css" rel="stylesheet" type="text/css" />
    <title>Marketing Automation Tool - Duplicate Leads Listing</title>
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
				  <asp:LinkButton ID="lnkLeads" runat="server" CssClass="breadcrumb-item"  Text="Leads" OnClick="lnkLeads_Click"></asp:LinkButton>
                  <asp:LinkButton ID="lnkReports" runat="server" CssClass="breadcrumb-item"  Text="Reports" OnClick="lnkReports_Click" ></asp:LinkButton>
                  <span class="breadcrumb-item active">Duplicate Leads Listing</span>
				</nav>
			</div>
</asp:Content>  
<asp:Content ID="Content5" ContentPlaceHolderID="cpBody" runat="server"> 
    <uc:LeftSearch runat="server" id="LeadLeftSearch"></uc:LeftSearch>
				<div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 middlecontent">
					<div class="page_header">
                        <div class="form-horizontal">
							<div class="container">
								<div class="row">
									<h5 class="page_title">Duplicate Leads Listing</h5>
								</div>
							</div>
						</div>
					</div>
					<div class="page_content">
						<div class="container-fluid">
							<div class="form-horizontal">
							    <div class="container">
							        
                                    <div class="row" > 
                                        <div class="col-lg-1">
                                        &nbsp;
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="500px" Width="680px"></rsweb:ReportViewer>
                                        </div>
                                        <div class="col-lg-1">
                                        &nbsp;
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
    <input type="hidden" id="hdnDateSelected" name="hdnDateSelected" runat="server"/>
</asp:Content>