<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountDetails.aspx.cs" Inherits="MarketingAutomationTool.MyAccount.AccountDetails" MasterPageFile="~/MATSite.Master" %>
<%@ Register Src="~/UserControls/LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pager.ascx" TagName="Pager" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/NewEmailCampaign.ascx" TagName="NewEmailCampaign" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
	<link rel="stylesheet" href="../dcjqaccordion-master/css/dcaccordion.css" />
	
	<script src="../jQuery/jquery-3.3.1.min.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.cookie.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.hoverIntent.minified.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.dcjqaccordion.2.7.min.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/accordionmenu.js"type="text/javascript"></script>
	<link href="../dcjqaccordion-master/css/skins/graphite.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            if ($("#<%= hdnTabSelected.ClientID%>").val() != '') {
                if ($("#<%= hdnTabSelected.ClientID%>").val() == 'lnkStatus') {
                     $("#lnkStatus").addClass("active");
			         $("#tabStatus").addClass("active");
			         $("#lnkUsers").removeClass("active");
			         $("#tabUsers").removeClass("active");
			         $("#lnkMore").removeClass("active");
                     $("#tabMore").removeClass("active");
                }
                else  if ($("#<%= hdnTabSelected.ClientID%>").val() == 'lnkUsers') {
                    $("#lnkStatus").removeClass("active");
			        $("#tabStatus").removeClass("active");
			        $("#lnkUsers").addClass("active");
			        $("#tabUsers").addClass("active");
			        $("#lnkMore").removeClass("active");
                    $("#tabMore").removeClass("active");
                }
                else  if ($("#<%= hdnTabSelected.ClientID%>").val() == 'lnkMore') {
                     $("#lnkStatus").removeClass("active");
			         $("#tabStatus").removeClass("active");
			         $("#lnkUsers").removeClass("active");
			         $("#tabUsers").removeClass("active");
			         $("#lnkMore").addClass("active");
                     $("#tabMore").addClass("active");
                }
            }
	
		    $("#lnkStatus").click(function(){
			    $("#lnkStatus").addClass("active");
			    $("#tabStatus").addClass("active");
			    $("#lnkUsers").removeClass("active");
			    $("#tabUsers").removeClass("active");
			    $("#lnkMore").removeClass("active");
                $("#tabMore").removeClass("active");
                $("#<%= hdnTabSelected.ClientID%>").val("lnkStatus");
		    });
		
		    $("#lnkUsers").click(function(){
			    $("#lnkStatus").removeClass("active");
			    $("#tabStatus").removeClass("active");
			    $("#lnkUsers").addClass("active");
			    $("#tabUsers").addClass("active");
			    $("#lnkMore").removeClass("active");
                $("#tabMore").removeClass("active");
                $("#<%= hdnTabSelected.ClientID%>").val("lnkUsers");
		    });
		
		    $("#lnkMore").click(function(){
			    $("#lnkStatus").removeClass("active");
			    $("#tabStatus").removeClass("active");
			    $("#lnkUsers").removeClass("active");
			    $("#tabUsers").removeClass("active");
			    $("#lnkMore").addClass("active");
                $("#tabMore").addClass("active");
                $("#<%= hdnTabSelected.ClientID%>").val("lnkMore");
            });
        });

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode >= 48 && charCode <= 57)||charCode==45)
                return true;

            return false;
        }
    </script>

    <title>Marketing Automation Tool - Account Details</title>
   
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
		    <span class="breadcrumb-item active">Account Details</span>
	    </nav>
    </div>
</asp:Content>  

<asp:Content ID="Content2" ContentPlaceHolderID="cpBody" runat="server">
    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 leftmenu">
             <uc:LeftMenu runat="server" id="ucLeftMenu"></uc:LeftMenu>
    </div>
    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 middlecontent">
			<div class="page_header">
						<h5 class="page_title">Account Details</h5>
					</div>
					<div class="page_content">
						<div class="container-fluid">
									<div class="form-horizontal">
										<div class="container">
                                             <div class="" role="alert" id="dvMessage" runat="server" style="display:none;">
                                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                                </button>
                                             </div>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >Name:</label>
															<div class="col-sm-10">
																<label class="col-form-label" ID="lblAccountName" runat="server" ></label>
															</div>
														</div>
													</div>
											</div>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >Status:</label>
															<div class="col-sm-10">
																<label class="col-form-label"><input type="radio" name="optradio"  runat="server" id="rdActive">&nbsp;Active</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																<label class="col-form-label"><input type="radio" name="optradio" runat="server" id="rdInactive">&nbsp;Inactive</label>
															</div>
														</div>
													</div>
											</div>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >Date Created:</label>
															<div class="col-sm-10">
																<label class="col-form-label" ID="lblDateCreated" runat="server"></label>
															</div>
														</div>
													</div>
											</div>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
															<label class="col-form-label col-sm-2" >Signup IP:</label>
															<div class="col-sm-10">
																<label class="col-form-label" ID="lblSignupIP" runat="server" ></label>
															</div>
														</div>
													</div>
											</div>
											<div class="row">
												<div class="container-fluid">
                                                    <ul class="nav nav-tabs" id="secondtab">
													  <li class="nav-item">
														<a class="nav-link active" href="#" id="lnkStatus">Status</a>
													  </li>
													  <li class="nav-item">
														<a class="nav-link" href="#" id="lnkUsers">Users</a>
													  </li>
													  <li class="nav-item">
														<a class="nav-link" href="#" id="lnkMore">More</a>
													  </li>
												    </ul>
							                            <div class="tab-content" id="dvTable">
                                                               <div class="tab-pane active" id="tabStatus">
									                                <div class="form-horizontal">
										                                <div class="container">
											                                <div class="row">
                                                                                <div class="col-lg-12">
                                                                                    <asp:GridView ID="grdAccountStatusHistory" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" GridLines="None" EmptyDataText="No Data Available" ShowHeaderWhenEmpty="True" >
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Status">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>' ></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Date Changed">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("DateChanged","{0:dd MMM yyyy hh:mm:ss tt}") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="IP">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("IP") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle  />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Changed By">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("ChangedByName") %>'></asp:Label>
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
                                                            <div class="tab-pane" id="tabUsers">
									                                <div class="form-horizontal">
										                                <div class="container">
											                                <div class="row">
                                                                                <div class="col-lg-12">
                                                                                  <asp:GridView ID="grdUsers" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" GridLines="None" EmptyDataText="No Data Available" ShowHeaderWhenEmpty="True" OnRowCommand="grdUsers_RowCommand"   >
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
                                                             <div class="tab-pane" id="tabMore">
									                                <div class="form-horizontal">
										                                <div class="container">
											                                <div class="row">
                                                                                <div class="form-group form-group-sm col-sm-12">
																				    <div class="row">
																					    <label class="col-form-label col-sm-2" >User Name:</label>
																					    <div class="col-sm-10">
																						    <label class="col-form-label" runat="server" id="lblUserName" ></label>
																					    </div>
																				    </div>
																			    </div>
                                                                            </div>
                                                                            <legend class="legendstyle">User Details</legend>
																	        <div class="row">
																			        <div class="form-group form-group-sm col-sm-12">
																				        <div class="row">
																					        <label class="col-form-label col-sm-2" >First Name:</label>
																					        <div class="col-sm-10">
																						        <label class="col-form-label" runat="server" id="lblFirstName" ></label>
																					        </div>
																				        </div>
																			        </div>
																	        </div>
																	        <div class="row">
																			        <div class="form-group form-group-sm col-sm-12">
																				        <div class="row">
																					        <label class="col-form-label col-sm-2" >Last Name:</label>
																					        <div class="col-sm-10">
																						        <label class="col-form-label" runat="server" id="lblLastName" ></label>
																					        </div>
																				        </div>
																			        </div>
																	        </div>
																	        <div class="row">
																			        <div class="form-group form-group-sm col-sm-12">
																				        <div class="row">
																					        <label class="col-form-label col-sm-2" >Phone No:</label>
																					        <div class="col-sm-10">
																							        <label class="col-form-label" runat="server" id="lblPhoneNo" ></label>
																					        </div>
																				        </div>
																			        </div>
																	        </div>
																	        <div class="row">
																			        <div class="form-group form-group-sm col-sm-12">
																				        <div class="row">
																					        <label class="col-form-label col-sm-2" >Mobile No:</label>
																					        <div class="col-sm-10">
																							        <label class="col-form-label" runat="server" id="lblMobileNo" ></label>
																					        </div>
																				        </div>
																			        </div>
																	        </div>
																	        <div class="row">
																			        <div class="form-group form-group-sm col-sm-12">
																				        <div class="row">
																					        <label class="col-form-label col-sm-2" >Email Address:</label>
																					        <div class="col-sm-10">
																						         <label class="col-form-label"  runat="server" id="lblEmailAddress" ></label>
																					        </div>
																				        </div>
																			        </div>
																	        </div>
																	        <legend class="legendstyle">Company Details</legend>
																	        <div class="row">
																			        <div class="form-group form-group-sm col-sm-12">
																				        <div class="row">
																					        <label class="col-form-label col-sm-2" >Company Name:</label>
																					        <div class="col-sm-10">
																						         <label class="col-form-label" runat="server" id="lblCompanyName"></label>
																					        </div>
																				        </div>
																			        </div>
																	        </div>
																	        <div class="row">
																			        <div class="form-group form-group-sm col-sm-12">
																				        <div class="row">
																					        <label class="col-form-label col-sm-2" >Address:</label>
																					        <div class="col-sm-10">
																						         <label class="col-form-label" runat="server" id="lblAddress"></label>
																					        </div>
																				        </div>
																			        </div>
																	        </div>
																	        <div class="row">
																			        <div class="form-group form-group-sm col-sm-12">
																				        <div class="row">
																					        <label class="col-form-label col-sm-2" >City/Town/Suburb:</label>
																					        <div class="col-sm-10">
																						        <label class="col-form-label" runat="server" id="lblCityTownSuburb" ></label>
																					        </div>
																				        </div>
																			        </div>
																	        </div>
																	        <div class="row">
																			        <div class="form-group form-group-sm col-sm-12">
																				        <div class="row">
																					        <label class="col-form-label col-sm-2" >State:</label>
																					        <div class="col-sm-10">
																						         <label class="col-form-label" runat="server" id="lblState" ></label>
																					        </div>
																				        </div>
																			        </div>
																	        </div>
																	        <div class="row">
																			        <div class="form-group form-group-sm col-sm-12">
																				        <div class="row">
																					        <label class="col-form-label col-sm-2" >Postal/Zip Code:</label>
																					        <div class="col-sm-10">
																						         <label class="col-form-label"  runat="server" id="lblPostalZipCode" ></label>
																					        </div>
																				        </div>
																			        </div>
																	        </div>
																	        <div class="row">
																			        <div class="form-group form-group-sm col-sm-12">
																				        <div class="row">
																					        <label class="col-form-label col-sm-2" >Country:</label>
																					        <div class="col-sm-10">
																						         <label class="col-form-label" runat="server" id="lblCountry"></label>
																					        </div>
																				        </div>
																			        </div>
																	        </div>
																	        <div class="row">
																			        <div class="form-group form-group-sm col-sm-12">
																				        <div class="row">
																					        <label class="col-form-label col-sm-2" >Website:</label>
																					        <div class="col-sm-10">
																						        <label class="col-form-label" runat="server" id="lblWebSite" ></label>
																					        </div>
																				        </div>
																			        </div>
																	        </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                        </div>
												</div>
                                                <input type="hidden" id="hdnTabSelected" name="hdnTabSelected" runat="server" />
											</div>
											<div class="row">
													<div class="form-group form-group-sm col-sm-12">
														<div class="row">
														<div class="col-sm-12 button_panel">
															<div class="float-right mt-2">
                                                                <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary" OnClick="btnSave_Click" />
                                                                <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-light" OnClick="btnReset_Click" />
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
					<li class="dcjq-current-parent"><a href="#"  class="dcjq-parent active">Shortcuts</a>
						<ul>
									<li>
										<a href="/MyAccount/NewAccount.aspx?c=2">Add New Account</a>
									</li>
									<li>
										<a href="/MyAccount/Usage.aspx??c=2&accountid=<%=SelectedAccountid %>">View Account Usage</a>
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