<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewExistingImportJobs.aspx.cs" Inherits="MarketingAutomationTool.Leads.ViewExistingImportJobs" MasterPageFile="~/MATSite.Master" %>
<%@ Register Src="~/Leads/UserControls/Lead.ascx" TagName="LeadTopMenu" TagPrefix="uc" %>
<%@ Register Src="~/Leads/UserControls/LeftSearch.ascx" TagName="LeftSearch" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="uc" %>
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

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode >= 48 && charCode <= 57)||charCode==45)
                return true;

            return false;
        }

        function ConfirmDelete() {
            return window.confirm("Are you sure you want to delete this lead?");
        }
    </script>
	<link href="../dcjqaccordion-master/css/skins/graphite.css" rel="stylesheet" type="text/css" />
    <title>Marketing Automation Tool - View Existing Import Jobs</title>
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
				  <asp:LinkButton ID="lnkBreadHome" runat="server" CssClass="breadcrumb-item"  Text="Home" OnClick="lnkBreadHome_Click" ></asp:LinkButton>
				  <a class="breadcrumb-item" href="Lead.aspx">Leads</a>
                  <span class="breadcrumb-item active">Existing Imports</span>
				</nav>
			</div>
</asp:Content>  
<asp:Content ID="Content5" ContentPlaceHolderID="cpBody" runat="server"> 
    <uc:LeftSearch runat="server" id="LeadLeftSearch"></uc:LeftSearch>
				<div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 middlecontent">
					<div class="page_header">
						<h5 class="page_title">Existing Upload Jobs</h5>
					</div>
					<div class="page_content">
						<div class="container-fluid">
									<div class="form-horizontal">
										<div class="container">
											<div class="row">
												<div class="col-lg-12">
                                                    <asp:GridView ID="grdList" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" GridLines="None" EmptyDataText="No Data Available" ShowHeaderWhenEmpty="True" OnRowCommand="grdList_RowCommand"  >
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Job Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblJobName" runat="server" Text='<%# Bind("JobName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date Created">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDateCreated" runat="server" Text='<%# Bind("DateCreated","{0:dd MMM yyyy hh:mm:ss tt}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Owner">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOwner" runat="server" Text='<%# Bind("CreatedByName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Uploaded File">
                                                                <ItemTemplate>
                                                                     <a href="/HttpHandler/Download.ashx?file=uploadfile&id=<%# Eval("JobId") %>&acct=<%# Eval("AccountID") %>"  target="_blank"><%# Eval("OriginalFileName") %></a>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Job Started">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblJobStarted" runat="server"  Text='<%# Bind("JobStarted","{0:dd MMM yyyy hh:mm:ss tt}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Job Finished">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblJobFinished" runat="server" Text='<%# Bind("JobFinished","{0:dd MMM yyyy hh:mm:ss tt}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Leads">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTotalContacts" runat="server" Text='<%# Bind("TotalContacts") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Uploaded Leads">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUploadedContacts" runat="server" Text='<%# Bind("UploadedContacts") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            
                                                        </Columns>
                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                    </asp:GridView>
												</div>
											</div>
                                            <div class="row" > 
                                                <uc:pager runat="server" id="Pager"></uc:pager>
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

