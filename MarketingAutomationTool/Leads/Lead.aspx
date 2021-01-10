<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lead.aspx.cs" Inherits="MarketingAutomationTool.Leads.Lead" MasterPageFile="~/MATSite.Master" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/Leads/UserControls/Lead.ascx" TagName="LeadTopMenu" TagPrefix="uc" %>
<%@ Register Src="~/Leads/UserControls/LeftSearch.ascx" TagName="LeftSearch" TagPrefix="uc" %>
<%@ Register Src="~/Leads/UserControls/ViewRecentSearches.ascx" TagName="ViewRecentSearches" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="uc" %>
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
			  
			 $("#aClip").click(function() {
				$(this).toggleClass("active");
				$("#dvcontactstatchart").toggleClass("show");
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
    <title>Marketing Automation Tool - Leads</title>
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
				  <span class="breadcrumb-item active">Leads</span>
				</nav>
			</div>
</asp:Content>  
<asp:Content ID="Content5" ContentPlaceHolderID="cpBody" runat="server"> 
    <uc:LeftSearch runat="server" id="LeadLeftSearch"></uc:LeftSearch>
				<div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 middlecontent">
					<div class="page_header">
							<h5 class="page_title">Dashboard</h5>
					</div>
					<div class="page_content">
						<div class="container-fluid">
							<div class="page_header">
								<h6 class="page_title"><a id="aClip" class="hdrclp active">&nbsp;&nbsp;&nbsp;</a>&nbsp;Statistics</h6>
							</div>
							<div id="dvcontactstatchart" class="col-lg-12 collapse show">
								<div class="row">
                                    <div class="col-lg-12 text-center" >
                                    <asp:Chart ID="cContactProgress" runat="server">
                                        <Series>
                                            <asp:Series Name="Series1"></asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                        </ChartAreas>
                                        <Titles>
                                            <asp:Title Font="Arial, 14pt, style=Bold" Name="Title1" Text="Leads Added Per Month">
                                            </asp:Title>
                                        </Titles>
                                    </asp:Chart>
                                    </div>
								</div>
								<div class="row">
									 <div class="col-lg-12 text-center" >
                                    <asp:Chart ID="cContactPerCountry" runat="server" Width="399px">
                                        <Series>
                                            <asp:Series Name="Series1" ChartType="Pie"></asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1">
                                            </asp:ChartArea>
                                        </ChartAreas>
                                        <Titles>
                                            <asp:Title Font="Arial, 14pt, style=Bold" Name="Title1" Text="Percentage of Leads per Country">
                                            </asp:Title>
                                        </Titles>
                                    </asp:Chart>
                                    </div>
								</div>
							</div>
							<br/>
							<div class="page_header" >
								<h6 class="page_title">Recent</h6>
							</div>
							<div class="col-lg-12" id="RecentLeads">
                                <br/>
                                    <asp:GridView ID="grdContacts" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" GridLines="None" EmptyDataText="No Data Available" ShowHeaderWhenEmpty="True" OnRowCommand="grdContacts_RowCommand"  >
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Lead ID">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkLeadID" runat="server" Text='<%# Bind("ContactIDText") %>' CommandName="SELECT" CommandArgument='<%# Bind("ContactGUID") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="First Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Last Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle  />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Email Address">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Mobile Number">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("MobileNumber") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle  />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                    </asp:GridView>
                                <div class="row" > 
												 <uc:Pager runat="server" id="Pager"></uc:Pager>
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


