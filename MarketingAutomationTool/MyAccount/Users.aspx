<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="MarketingAutomationTool.MyAccount.Users" MasterPageFile="~/MATSite.Master"  %>
<%@ Register Src="~/UserControls/LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="uc" %>
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
    <script type="text/javascript">

        $(document).ready(function(){
            $("#lnkFilter").click(function () {
                $("#collapsePanel").toggleClass("show");
              });
        });

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode >= 48 && charCode <= 57)||charCode==45)
                return true;

            return false;
        }
        
    </script>
	<link href="../dcjqaccordion-master/css/skins/graphite.css" rel="stylesheet" type="text/css" />
    <title>Marketing Automation Tool - Users</title>
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
            <span class="breadcrumb-item active">Users</span>
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
									<h5 class="page_title">Users</h5>
									 <a  href="#" id="lnkFilter"  style="position: absolute; right: 20px; top:38px;">
										Filter <i class="fas fa-filter" style="color:#000;"></i> 
									 </a>
								</div>
							</div>
						</div>
					</div>
					<div class="page_content">
						<div class="container-fluid">
								<div class="form-horizontal">
										<div class="container">
												<div class="collapse" id="collapsePanel">
													<h6>Filter</h6></br>
													<div class="row">
														<div class="form-group form-group-sm col-sm-12">
															<div class="row">
															<label class="col-form-label col-sm-2" >First Name:</label>
															<div class="col-sm-10">
                                                                <asp:TextBox ID="txtFirstName" runat="server" class="form-control" placeholder="First Name" MaxLength="100"></asp:TextBox>
															</div>
															</div>
														</div>
													</div>
													<div class="row">
														<div class="form-group form-group-sm col-sm-12">
															<div class="row">
															<label class="col-form-label col-sm-2" >Last Name:</label>
															<div class="col-sm-10">
                                                                <asp:TextBox ID="txtLastName" runat="server" class="form-control" placeholder="Last Name" MaxLength="100"></asp:TextBox>
															</div>
															</div>
														</div>
													</div>
													<div class="row">
														<div class="form-group form-group-sm col-sm-12">
															<div class="row">
															<label class="col-form-label col-sm-2" >Username:</label>
															<div class="col-sm-10">
                                                                 <asp:TextBox ID="txtUserName" runat="server" class="form-control" placeholder="Username" MaxLength="50"></asp:TextBox>
															</div>
															</div>
														</div>
													</div>
													<div class="row">
												<div class="form-group form-group-sm col-sm-12">
													<div class="row">
													<div class="col-sm-12 button_panel">
														<div class="float-right mt-2">
                                                            <asp:Button ID="btnFilter" runat="server" Text="Filter" class="btn btn-primary" OnClick="btnFilter_Click"  />
														</div>
													</div>
													</div>
												</div>
											</div>
												</div>
											<div class="row">
                                                <div class="col-lg-12">
                                                    <asp:GridView ID="grdUsers" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" GridLines="None" EmptyDataText="No Data Available" ShowHeaderWhenEmpty="True" OnRowCommand="grdUsers_RowCommand"  >
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Username">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkUserName" runat="server" Text='<%# Bind("UserName") %>' CommandName="SELECT" CommandArgument='<%# Bind("UserID") %>'></asp:LinkButton>
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
                                                </div>
											</div>
											<div class="row" >
												    <uc:Pager runat="server" id="Pager"></uc:Pager>
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
                        <% if (IsSuperAdminUser) {%>
				        <a href="/MyAccount/Accounts.aspx?c=2">View Accounts</a>
                        <%}  else {%>
                        <a href="/MyAccount/MyAccount.aspx">View My Account Details</a>
                        <%} %>
				    </li>
			    </ul>
		    </li>
			</ul>
		</div>
	</div>	
</div>
</asp:Content>