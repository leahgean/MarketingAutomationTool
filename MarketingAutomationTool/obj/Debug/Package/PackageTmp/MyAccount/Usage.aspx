<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usage.aspx.cs" Inherits="MarketingAutomationTool.MyAccount.Usage" MasterPageFile="~/MATSite.Master" %>
<%@ Register Src="~/UserControls/LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/NewEmailCampaign.ascx" TagName="NewEmailCampaign" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
	<link rel="stylesheet" href="../dcjqaccordion-master/css/dcaccordion.css" />
	
	<script src="../bootstrap/assets/js/vendor/jquery-slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="../bootstrap/assets/js/vendor/popper.min.js" integrity="sha384-cs/chFZiN24E4KMATLdqdvsezGxaGsi4hLGOzlXwp5UZB1LY//20VyM2taTB4QvJ" crossorigin="anonymous"></script>
    <script src="../bootstrap/js/bootstrap.min.js" integrity="sha384-uefMccjFJAIv6A+rW+L4AHf99KvxDjWSu1z9VI8SKNVmz4sk7buKt/6v9KI65qnm" crossorigin="anonymous"></script>
	<script src="../jQuery/jquery-3.3.1.min.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.cookie.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.hoverIntent.minified.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.dcjqaccordion.2.7.min.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/accordionmenu.js"type="text/javascript"></script>
	<link href="../dcjqaccordion-master/css/skins/graphite.css" rel="stylesheet" type="text/css" />
    <title>Marketing Automation Tool - Account Usage</title>
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
            <span class="breadcrumb-item active">Usage</span>
	    </nav>
    </div>
</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="cpBody" runat="server">
    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 leftmenu">
			 <uc:LeftMenu runat="server" id="ucLeftMenu"></uc:LeftMenu>	
				</div>
				<div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 middlecontent">
					<div class="page_header">
						<h5 class="page_title">Account Usage</h5>
					</div>
					<div class="page_content">
						<div class="container-fluid">
									<div class="form-horizontal">
										<div class="container">
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >Name:</label>
															<div class="col-sm-10">
                                                                <label class="col-form-label" runat="server" id="lblAccountName"></label>
															</div>
														</div>
													</div>
											</div>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >Status:</label>
															<div class="col-sm-10">
																<label class="col-form-label" runat="server" id="lblStatus"></label>
															</div>
														</div>
													</div>
											</div>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >Date Created:</label>
															<div class="col-sm-10">
																<label class="col-form-label" runat="server" id="lblDateCreated"></label>
															</div>
														</div>
													</div>
											</div>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >Year:</label>
															<div class="col-sm-10">
                                                                <asp:DropDownList class="form-control" ID="ddlYear" runat="server" style="width:200px;" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" AutoPostBack="True" >

                                                                </asp:DropDownList>
															</div>
														</div>
													</div>
											</div>
											</br>
											<legend class="legendstyle">Usage of Records of <label class="col-form-label" runat="server" id="lblYearSelected"></label></legend>
											</br>
											<div class="row">
                                                <div class="col-lg-12">
                                                <asp:GridView ID="grdUsage" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" GridLines="None" EmptyDataText="No Data Available" ShowHeaderWhenEmpty="True"   >
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Month">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblMonth" runat="server" Text='<%# Bind("Month") %>' ></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Email">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Contact & Leads">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("ContactsLeads") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                                    </asp:GridView>
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
										<a href="/MyAccount/NewAccount.aspx?c=2">Add New Account</a>
									</li>
									<li>
										<a href="/MyAccount/AccountDetails.aspx?c=2&AccountId=<%= SelectedAccountid%>">View Account Details</a>
									</li>
									<li>
										<a href="/MyAccount/Accounts.aspx?c=2">View Accounts</a>
									</li>
								</ul>
							</li>
							</ul>
					</div>
				</div>	
				</div>		
</asp:Content>