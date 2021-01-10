<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAll.aspx.cs" Inherits="MarketingAutomationTool.Campaigns.Reports.ViewAll" MasterPageFile="~/MATSite.Master" %>
<%@ Register Src="~/UserControls/NewEmailCampaign.ascx" TagName="NewEmailCampaign" TagPrefix="uc" %>
<%@ Register Src="~/Campaigns/UserControls/TopMenu.ascx" TagName="TopMenu" TagPrefix="uc" %>
<%@ Register Src="~/Campaigns/UserControls/LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
     <link rel="stylesheet" href="../../dcjqaccordion-master/css/dcaccordion.css" />
    <script src="../../jQuery/jquery-3.3.1.min.js" type="text/javascript"></script>
	<script src="../../dcjqaccordion-master/js/jquery.cookie.js" type="text/javascript"></script>
	<script src="../../dcjqaccordion-master/js/jquery.hoverIntent.minified.js" type="text/javascript"></script>
	<script src="../../dcjqaccordion-master/js/jquery.dcjqaccordion.2.7.min.js" type="text/javascript"></script>
	<script src="../../dcjqaccordion-master/accordionmenu.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            /*begin --left search -- begin*/
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
             /*end --left search -- end*/
        });

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode >= 48 && charCode <= 57)||charCode==45)
                return true;

            return false;
        }

        /*begin-SideBar-begin*/ 
        function LeftSearchConfirmDelete() {
            return window.confirm('Are you sure you want to delete this campaign?');
        }
        /*end-SideBar-end*/
      
    </script>
    <link href="../../dcjqaccordion-master/css/skins/graphite.css" rel="stylesheet" type="text/css" />
     <title>Marketing Automation Tool - View All Reports</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpShortCut" runat="server"> 
     <uc:NewEmailCampaign runat="server" id="NewEmailCampaign"></uc:NewEmailCampaign>
    </asp:Content>  
<asp:Content ID="Content3" ContentPlaceHolderID="cpTabHeaderBottom" runat="server"> 
      <uc:TopMenu runat="server" id="ucTopMenu"></uc:TopMenu>
</asp:Content>  
<asp:Content ID="Content4" ContentPlaceHolderID="cpRowCrumbs" runat="server"> 
     <div class="container-fluid">
				<nav class="breadcrumb">
				   <asp:LinkButton ID="lnkBreadHome" runat="server" CssClass="breadcrumb-item"  Text="Home" OnClick="lnkBreadHome_Click" ></asp:LinkButton>
                    <asp:LinkButton ID="lnkCampaigns" runat="server" CssClass="breadcrumb-item"  Text="Campaigns" OnClick="lnkCampaigns_Click" ></asp:LinkButton>
				  <span class="breadcrumb-item active">View All Reports</span>
				</nav>
			</div>
</asp:Content> 
<asp:Content ID="Content5" ContentPlaceHolderID="cpBody" runat="server"> 
    	 <uc:LeftMenu runat="server" id="LeadLeftSearch"></uc:LeftMenu>
    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 middlecontent">
					<div class="page_header">
							<h5 class="page_title">Reports</h5>
					</div>
					<div class="page_content">
						<div class="container-fluid">
							<div class="col-lg-12">
										<asp:GridView ID="grdCampaignsReports" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" GridLines="None" EmptyDataText="No Data Available" ShowHeaderWhenEmpty="True" OnRowCommand="grdCampaignsReports_RowCommand"  >
                                            <Columns>
                                                <asp:TemplateField HeaderText="Report Name">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkID" runat="server" Text='<%# Bind("ReportName") %>' CommandName="SELECT" CommandArgument='<%# Bind("Id") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle />
                                                </asp:TemplateField>
                                               
                                                </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                        </asp:GridView>
                                <br/>
											
							</div>
						</div>
					</div>
						
					
				</div>

    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 rightmenu">
				<div class="container-fluid">
					<div class="graphite accordion-container-right">
						<ul class="accordion" id="accordion-right">
							<li class="dcjq-current-parent"><a href="#"  class="dcjq-parent active">Create Campaign</a>
								<ul>
									<li>
										<a href="/Campaigns/CampaignBuilder2.aspx">Email</a>
									</li>
								
								</ul>
							</li>
						</ul>
					</div>
				</div>	
				</div>	
   
	
</asp:Content>  

