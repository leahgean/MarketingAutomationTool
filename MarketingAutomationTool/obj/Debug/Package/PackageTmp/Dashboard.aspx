<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="MarketingAutomationTool.Dashboard" MasterPageFile="~/MATSite.Master" %>
<%@ Register Src="~/UserControls/NewEmailCampaign.ascx" TagName="NewEmailCampaign" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server"> 
    <title>Marketing Automation Tool - Dashboard</title>
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
				  <a class="breadcrumb-item" href="/Dashboard.aspx">Home</a>
				  <span class="breadcrumb-item active">Dashboard</span>
				</nav>
			</div>
</asp:Content>  
<asp:Content ID="Content2" ContentPlaceHolderID="cpBody" runat="server"> 
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 solocontent">
                        <div class="" role="alert" id="dvMessage" runat="server" style="display:none;">
                                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                        <span aria-hidden="true">&times;</span>
                                                      </button>
                                                </div>
						<div class="main_header">
							<h5 class="main_title">Dashboard</h5>
						</div>
						<div class="main_content">
							<div class="row">
                                <div class="col-lg-3 dashboard_left_box center-block text-center infoBox_lead">
										<h5 class="box">
											<span>
                                                <asp:Label ID="lblTotalLeads" runat="server" Text="0"></asp:Label>
											</span>
                                            <br/>
											<small>Total Leads</small>
										</h5>
								</div>
                                <div class="col-lg-3 dashboard_middle_box center-block text-center infoBox_lead">
										<h5 class="box">
											<span>
                                                <asp:Label ID="lblNewLeads" runat="server" Text="0"></asp:Label>
											</span>
                                            <br/>
											<small>New Leads</small>
										</h5>
								</div>
								<div class="col-lg-3 dashboard_middle_box center-block text-center infoBox_email">
										<h5 class="box">
											<span>
                                                <asp:Label ID="lblEmailsSent" runat="server" Text="0"></asp:Label>
											</span>
                                            <br/>
											<small>Emails Sent</small>
										</h5>
								</div>
                                <div class="col-lg-3 dashboard_right_box center-block text-center infoBox_email">
										<h5 class="box">
											<span>
                                                <asp:Label ID="lblUniqueOpens" runat="server" Text="0"></asp:Label>
											</span><br/>
											<small>Unique Opens</small>
										</h5>
								</div>
							</div>
							<div class="row">
                                
								<div class="col-lg-3 dashboard_left_box center-block text-center infoBox_email">
										<h5 class="box">
											<span>
                                                <asp:Label ID="lblTotalOpens" runat="server" Text="0"></asp:Label>
											</span>
                                            <br/>
											<small>Total Opens</small>
										</h5>
								</div>
								<div class="col-lg-3 dashboard_middle_box center-block text-center infoBox_email">
										<h5 class="box">
											<span>
                                                <asp:Label ID="lblUniqueClicks" runat="server" Text="0"></asp:Label>
											</span><br/>
											<small>Unique Clicks</small>
										</h5>
								</div>
								<div class="col-lg-3 dashboard_middle_box center-block text-center infoBox_email">
										<h5 class="box">
											<span>
                                                 <asp:Label ID="lblTotalClicks" runat="server" Text="0"></asp:Label>
											</span><br/>
											<small>Total Clicks</small>
										</h5>
								</div>
                                <div class="col-lg-3 dashboard_right_box center-block text-center infoBox_email">
										<h5 class="box">
											<span>
                                                 <asp:Label ID="lblUnsubscribe" runat="server" Text="0"></asp:Label>
											</span><br/>
											<small>Unsubscribe</small>
										</h5>
								</div>
							</div>
							
							<div class="row">
								<div class="col-lg-6 lefttable">
									<div class="row">
                                        <div class="col-lg-12">
										<h5 class="dashboard_title">&nbsp;Leads</h5>
										<asp:GridView ID="grdLeads" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" GridLines="None" EmptyDataText="No Data Available" ShowHeaderWhenEmpty="True" OnRowCommand="grdLeads_RowCommand"  >
                                            <Columns>
                                                <asp:TemplateField HeaderText="Lead ID">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkLeadID" runat="server" Text='<%# Bind("ContactIDText") %>' CommandName="SELECT" CommandArgument='<%# Bind("ContactGUID") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Firstname">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFirstname" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lastname">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLastname" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Mobile">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("MobileNumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email Address">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                              
                                                </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                        </asp:GridView>
										<div class="form-actions text-right dashboardspace dashboard_table_viewall">
                                            <asp:Button ID="btnViewLeads" runat="server" Text="View All" class="btn btn-large btn-primary pull-right dashboard_button" OnClick="btnViewLeads_Click"  />
										</div>
                                        </div>
									</div>
								</div>
								<div class="col-lg-6 righttable">
									<div class="row">
                                        <div class="col-lg-12">
										<h5 class="text-left dashboard_title">&nbsp;Campaigns</h5>
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
                                                <asp:TemplateField HeaderText="Created">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCreated" runat="server" Text='<%# Bind("Created","{0:dd MMM yyyy hh:mm:ss tt}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Finished">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFinished" runat="server" Text='<%# Bind("Finished","{0:dd MMM yyyy hh:mm:ss tt}") %>'></asp:Label>                                                               
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                        </asp:GridView>
										<div class="form-actions text-right dashboardspace dashboard_table_viewall">
                                            <asp:Button ID="btnViewAllCampaigns" runat="server" Text="View All" class="btn btn-large btn-primary pull-right dashboard_button" OnClick="btnViewAllCampaigns_Click" />
										</div>
                                        </div>
									</div>
								</div>
							</div>
						</div>
					</div>
</asp:Content>  

