<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FacebookLogin.aspx.cs" Inherits="MarketingAutomationTool.MyAccount.FacebookLogin" MasterPageFile="~/MATSite.Master" %>
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
    <title>Marketing Automation Tool - Facebook Login</title>
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
            <span class="breadcrumb-item active">Facebook Login</span>
	    </nav>
    </div>
</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="cpBody" runat="server">
<div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 leftmenu">
			<uc:LeftMenu runat="server" id="ucLeftMenu"></uc:LeftMenu>	
				</div>
				<div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 middlecontent">
					<div class="page_header">
						<div class="form-horizontal">
							<div class="container">
								<div class="row">
									<h5 class="page_title">Facebook Login</h5>
								</div>
							</div>
						</div>
					</div>
					<div class="page_content">
						<div class="container-fluid">
								<div class="form-horizontal">
										<div class="container">
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Login State:</label>
													<div class="col-sm-10">
														<label class="col-form-label" ><%=LoginState %></label>
													</div>
													</div>
												</div>
											</div>
                                            <div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Facebook Id:</label>
													<div class="col-sm-10">
                                                        <label class="col-form-label" id="lblFacebookId" ><%=FBId %></label>
													</div>
													</div>
												</div>
											</div>
                                            <div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Facebook Name:</label>
													<div class="col-sm-10">
                                                        <label class="col-form-label" id="lblFacebookName" ><%= FBName %></label>

													</div>
													</div>
												</div>
											</div>
                                            <div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Facebook Email:</label>
													<div class="col-sm-10">
                                                        <label class="col-form-label" id="lblFacebookEmail" ><%= FBEmail%></label>

													</div>
													</div>
												</div>
											</div>
                                             <div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Facebook Picture URL:</label>
													<div class="col-sm-10">
                                                        <label class="col-form-label" id="lblFacebookPictureURL" ><%= FBPictureURL%></label>

													</div>
													</div>
												</div>
											</div>
                                            <div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<label class="col-form-label col-sm-2" >Facebook Image:</label>
													<div class="col-sm-10">
                                                        <asp:Image ID="imgFB" runat="server" AlternateText="Facebook Image" CssClass="img-fluid img-thumbnail"  GenerateEmptyAlternateText="True" />
                                                        
													</div>
													</div>
												</div>
											</div>
											<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<%--<div class="row">--%>
														<div class="col-sm-12">
                                                            <div class="alert alert-primary" role="alert" runat="server" id="dvMessage">
                                                                
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
																<asp:Button ID="btnFBLogin" runat="server" Text="Login to Facebook or Change Facebook Login" OnClick="btnFBLogin_Click"  CssClass="btn btn-primary"/>
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
							<li class="dcjq-current-parent"><a href="#"  class="dcjq-parent active">Shortcuts</a>
								<ul>
									<li>
                                        <asp:LinkButton ID="lnkLoginToFacebook" runat="server" Text="Login to Facebook or Change Facebook Login" OnClick="lnkLoginToFacebook_Click"></asp:LinkButton>
									</li>
                                    <li>
                                        <asp:LinkButton ID="lnkImportFBContacts" runat="server" Text="Import Facebook Contacts" OnClick="lnkImportFBContacts_Click" ></asp:LinkButton>
									</li>
								</ul>
							</li>
							</ul>
					</div>
				</div>	
				</div>
</asp:Content>