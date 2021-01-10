<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAll.aspx.cs" Inherits="MarketingAutomationTool.Campaigns.ViewAll" MasterPageFile="~/MATSite.Master" %>
<%@ Register Src="~/UserControls/NewEmailCampaign.ascx" TagName="NewEmailCampaign" TagPrefix="uc" %>
<%@ Register Src="~/Campaigns/UserControls/TopMenu.ascx" TagName="TopMenu" TagPrefix="uc" %>
<%@ Register Src="~/Campaigns/UserControls/LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="uc" %>
<%@ Register Src="~/Campaigns/UserControls/NewEmailCampaign.ascx" TagName="NewEmailCampaignRightMenu" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
     <link rel="stylesheet" href="../dcjqaccordion-master/css/dcaccordion.css" />
    <script src="../jQuery/jquery-3.3.1.min.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.cookie.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.hoverIntent.minified.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.dcjqaccordion.2.7.min.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/accordionmenu.js" type="text/javascript"></script>
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

      

        /*begin-SideBar-begin*/ 
        function LeftSearchConfirmDelete() {
            return window.confirm('Are you sure you want to delete this campaign?');
        }
        /*end-SideBar-end*/
      
    </script>
    <link href="../dcjqaccordion-master/css/skins/graphite.css" rel="stylesheet" type="text/css" />
     <title>Marketing Automation Tool - Campaign Reports - View All</title>
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
							<h5 class="page_title">View All Campaigns</h5>
					</div>
					<div class="page_content">
						<div class="container-fluid">
							<div class="col-lg-12">
										<asp:GridView ID="grdCampaigns" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" GridLines="None" EmptyDataText="No Data Available" ShowHeaderWhenEmpty="True" OnRowCommand="grdCampaigns_RowCommand"  >
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkID" runat="server" Text='<%# Bind("ID") %>' CommandName="SELECT" CommandArgument='<%# Bind("CommandArg") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Owner">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOwner" runat="server" Text='<%# Bind("Owner") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hidden From Search">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHidden" runat="server" Text='<%# Bind("HideInSearchText") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCreated" runat="server" Text='<%# Bind("Created","{0:dd MMM yyyy hh:mm:ss tt}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Sent">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSent" runat="server" Text='<%# Bind("Sent","{0:dd MMM yyyy hh:mm:ss tt}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Finished">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFinished" runat="server" Text='<%# Bind("Finished","{0:dd MMM yyyy hh:mm:ss tt}") %>'></asp:Label>                                                               
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Deleted">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeleted" runat="server" Text='<%# Bind("DeletedDate","{0:dd MMM yyyy hh:mm:ss tt}") %>'></asp:Label>                                                               
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                        </asp:GridView>
                                <br/>
											<div class="row" > 
												 <uc:Pager runat="server" id="Pager"></uc:Pager>
											</div>
							</div>
						</div>
					</div>
						
					
				</div>

      <uc:NewEmailCampaignRightMenu runat="server" id="NewEmailCampaignRightMenu"></uc:NewEmailCampaignRightMenu>	
   
	
</asp:Content>  
