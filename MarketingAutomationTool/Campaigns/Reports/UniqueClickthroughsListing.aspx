<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UniqueClickthroughsListing.aspx.cs" Inherits="MarketingAutomationTool.Campaigns.Reports.UniqueClickthroughsListing" MasterPageFile="~/MATSite.Master" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/NewEmailCampaign.ascx" TagName="NewEmailCampaign" TagPrefix="uc" %>
<%@ Register Src="~/Campaigns/UserControls/TopMenu.ascx" TagName="TopMenu" TagPrefix="uc" %>
<%@ Register Src="~/Campaigns/UserControls/LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc" %>
<%@ Register Src="~/Campaigns/UserControls/NewEmailCampaign.ascx" TagName="NewEmailCampaignRightMenu" TagPrefix="uc" %>
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

            /*Report Parameters*/
             $("#lnkParameterFilter").click(function () {
                $("#paramCollapsePanel").toggleClass("show");
            });

            if ($('#<%=hdnDateSelected.ClientID%>').val() == '') {
                $('#<%=txtStartDate.ClientID%>').val(startDate());
                $('#<%=txtEndDate.ClientID%>').val(endDate());
            }
            /**/
        });

      

        /*begin-SideBar-begin*/ 
        function LeftSearchConfirmDelete() {
            return window.confirm('Are you sure you want to delete this campaign?');
        }
        /*end-SideBar-end*/
        /*begin-Validation-begin*/
        function Validate() {
           $('#<%=hdnDateSelected.ClientID%>').val('1');

            if ($("#<%= txtStartDate.ClientID%>").val() != '') {
                if ($("#<%= txtEndDate.ClientID%>").val() == '') {
                    $("#invalidEndDate").text('Please input Date Sent End Date.');
                    $("#invalidEndDate").show();
                    $("#<%=txtEndDate.ClientID%>").css({ "border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)" });
                    return false;
                }
                else {
                    $("#invalidEndDate").text('');
                    $("#invalidEndDate").hide();
                    $("#<%=txtEndDate.ClientID%>").css({ "border": "1px solid #ced4da", "box-shadow": "0 0 0 .2rem rgba(206, 212, 218)" });
                }
            }

            if ($("#<%= txtEndDate.ClientID%>").val() != '') {
                if ($("#<%= txtStartDate.ClientID%>").val() == '') {
                    $("#invalidStartDate").text('Please input Date Sent Start Date.');
                    $("#invalidStartDate").show();
                    $("#<%=txtStartDate.ClientID%>").css({ "border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)" });
                    return false;
                }
                else {
                    $("#invalidStartDate").text('');
                    $("#invalidStartDate").hide();
                    $("#<%=txtStartDate.ClientID%>").css({ "border": "1px solid #ced4da", "box-shadow": "0 0 0 .2rem rgba(206, 212, 218)" });
                }
            }

            if (($("#<%= txtStartDate.ClientID%>").val() != '') && ($("#<%= txtEndDate.ClientID%>").val() != '')) {
                 var dStartDate = new Date($('#<%=txtStartDate.ClientID%>').val());
                 var dEndDate = new Date($('#<%=txtEndDate.ClientID%>').val());

                if (dStartDate > dEndDate) {

                    $("#invalidStartDate").text('Date Sent Start Date cannot be greater than Date Sent End Date');
                    $("#invalidStartDate").show();
                    $("#<%=txtStartDate.ClientID%>").css({ "border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)" });
                    return false;
                }
                else {
                     $("#invalidStartDate").text('');
                    $("#invalidStartDate").hide();
                    $("#<%=txtStartDate.ClientID%>").css({ "border": "1px solid #ced4da", "box-shadow": "0 0 0 .2rem rgba(206, 212, 218)" });
                }
            }

            if (($("#<%= txtStartDate.ClientID%>").val() == '') && ($("#<%= txtEndDate.ClientID%>").val() == '')) {
                if ($("#<%= ddlCampaign.ClientID%>").val() == '') {
                     $("#invalidCampaign").text('Please select Campaign or input Date Sent Start and End Date.');
                    $("#invalidCampaign").show();
                    $("#<%=ddlCampaign.ClientID%>").css({ "border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)" });
                    return false;
                }
                 else {
                    $("#invalidCampaign").text('');
                    $("#invalidCampaign").hide();
                    $("#<%=ddlCampaign.ClientID%>").css({ "border": "1px solid #ced4da", "box-shadow": "0 0 0 .2rem rgba(206, 212, 218)" });

                }
            }
            return true;
        }
        /*end-Validation-end*/

        /*begin-Default Date Sent Start and End Date-begin*/
        function startDate()
        {
            var endDate = new Date();
            endDate.setDate(endDate.getDate() - 7);
            var dYear = endDate.getFullYear().toString();
            var dMonth = (endDate.getMonth() + 1).toString();
            var dDate = endDate.getDate().toString();
            var dFullDate = '';

            if (dMonth.length == 1) {
                dMonth = '0' + dMonth;
            };

             if (dDate.length == 1) {
                dDate = '0' + dDate;
            };

            dFullDate = dYear + '-' + dMonth + '-' + dDate;
            return dFullDate;
        }
        
        function endDate()
        {
            var d = new Date();
            var dYear = d.getFullYear().toString();
            var dMonth = (d.getMonth() + 1).toString();
            var dDate = d.getDate().toString();
            var dFullDate = '';

            if (dMonth.length == 1) {
                dMonth = '0' + dMonth;
            };

             if (dDate.length == 1) {
                dDate = '0' + dDate;
            };

            dFullDate = dYear + '-' + dMonth + '-' + dDate;
            return dFullDate;
        }
        /*end-Default Date Sent Start and End Date-end*/
      
    </script>
    <link href="../../dcjqaccordion-master/css/skins/graphite.css" rel="stylesheet" type="text/css" />
     <title>Marketing Automation Tool - Campaign Reports - Unique Clickthroughs Listing</title>
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
				  <asp:LinkButton ID="lnkCampaignReports" runat="server" CssClass="breadcrumb-item"  Text="Reports" OnClick="lnkCampaignReports_Click"  ></asp:LinkButton>
                    <span class="breadcrumb-item active">Unique Clickthroughs Listing</span>
				</nav>
			</div>
</asp:Content> 
<asp:Content ID="Content5" ContentPlaceHolderID="cpBody" runat="server"> 
    	 <uc:LeftMenu runat="server" id="LeadLeftSearch"></uc:LeftMenu>
    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 middlecontent">
					<div class="page_header">
                        <div class="form-horizontal">
							<div class="container">
								<div class="row">
									<h5 class="page_title">Unique Clickthroughs Listing</h5>
									 <a  href="#" id="lnkParameterFilter"  style="position: absolute; right: 20px; top:38px;">
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
							        <div class="collapse" id="paramCollapsePanel">
                                                    <h6>Filter</h6><br/>
													<div class="row">
														<div class="form-group form-group-sm col-sm-12">
															<div class="row">
															    <label class="col-form-label col-sm-2" >Date Sent:</label>
															    <div class="col-sm-4">
																    <div class="input-group mb-3">
																      <div class="input-group-prepend">
																	    <span class="input-group-text" id="basic-addon1"><i class="far fa-calendar-alt"></i></span>
																      </div>
																       <asp:TextBox ID="txtStartDate" type="date"  runat="server" class="form-control" placeholder="Date From"  aria-describedby="basic-addon1"></asp:TextBox>
                                                                        <div id="invalidStartDate" class="invalid-feedback">
                	                                                        Please input Date Sent Start Date.
                                                                        </div>
																    </div>
															    </div>
                                                                <label class="col-form-label col-sm-2" >To:</label>
															    <div class="col-sm-4">
																    <div class="input-group mb-3">
																      <div class="input-group-prepend">
																	    <span class="input-group-text" id="basic-addon1"><i class="far fa-calendar-alt"></i></span>
																      </div>
																       <asp:TextBox ID="txtEndDate" type="date"  runat="server" class="form-control" placeholder="Date To"  aria-describedby="basic-addon1"></asp:TextBox>
																        <div id="invalidEndDate" class="invalid-feedback">
                	                                                        Please input Date Sent End Date.
                                                                        </div>
                                                                    </div>
															    </div>
															</div>
														</div>
													</div>
                                                    <div class="row">
														<div class="form-group form-group-sm col-sm-12">
															<div class="row">
															<label class="col-form-label col-sm-2" >Campaign:</label>
															<div class="col-sm-10">
                                                                 <asp:DropDownList ID="ddlCampaign" class="form-control" runat="server" >
                                                                </asp:DropDownList>
                                                                <div id="invalidCampaign" class="invalid-feedback">
                	                                                        Please select Campaign or input Date Sent Start and End Date.
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
                                                                    <asp:Button ID="btnFilter" runat="server" Text="Apply Filters" class="btn btn-primary"  OnClientClick="return Validate();" OnClick="btnFilter_Click"   />
                                                                    &nbsp;&nbsp;
                                                                    <input type="button" id="btnClose" name="btnClose" value="Close" class="btn btn-light"/>
														        </div>
													        </div>
													        </div>
												        </div>
											        </div>
                                    </div>
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

      <uc:NewEmailCampaignRightMenu runat="server" id="NewEmailCampaignRightMenu"></uc:NewEmailCampaignRightMenu>	
   
	<input type="hidden" id="hdnDateSelected" name="hdnDateSelected" runat="server"/>
</asp:Content>  