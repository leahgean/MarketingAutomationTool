<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeadsListing.aspx.cs" Inherits="MarketingAutomationTool.Leads.Reports.LeadsListing" MasterPageFile="~/MATSite.Master" EnableEventValidation="false"  %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Src="~/Leads/UserControls/Lead.ascx" TagName="LeadTopMenu" TagPrefix="uc" %>
<%@ Register Src="~/Leads/UserControls/LeftSearch.ascx" TagName="LeftSearch" TagPrefix="uc" %>
<%@ Register Src="~/Leads/UserControls/ViewRecentSearches.ascx" TagName="ViewRecentSearches" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/NewEmailCampaign.ascx" TagName="NewEmailCampaign" TagPrefix="uc" %>
<%@ Register Src="~/Leads/UserControls/Shortcuts.ascx" TagName="Shortcuts" TagPrefix="uc" %>
<%@ Register Src="~/Leads/UserControls/LeadLists.ascx" TagName="LeadLists" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server"> 
    <link rel="stylesheet" href="../../dcjqaccordion-master/css/dcaccordion.css" />

    <script src="../../jQuery/jquery-3.3.1.min.js" type="text/javascript"></script>
	<script src="../../dcjqaccordion-master/js/jquery.cookie.js" type="text/javascript"></script>
	<script src="../../dcjqaccordion-master/js/jquery.hoverIntent.minified.js" type="text/javascript"></script>
	<script src="../../dcjqaccordion-master/js/jquery.dcjqaccordion.2.7.min.js" type="text/javascript"></script>
	<script src="../../dcjqaccordion-master/accordionmenu.js" type="text/javascript"></script>
	
	<script type="text/javascript">

        $(document).ready(function(){
            $("#lnkFilter").click(function () {
                $("#collapsePanel").toggleClass("show");
            });

            $("#lnkParameterFilter").click(function () {
                $("#paramCollapsePanel").toggleClass("show");
            });

            $("#btnClose").click(function () {
                $("#paramCollapsePanel").toggleClass("show");
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

             //contactStatusJsonList
            var contactStatusJsonList = {"All Types" : 
            [
                    {"Value" : "","Text" : "All Statuses"},
                    { "Value": "1", "Text": "Not Confirmed" },
                    { "Value": "2", "Text": "Confirmed" },
                    {"Value" : "3","Text" : "Active"},
                    {"Value" : "4","Text" : "Inactive"}
            ],
            "Lead": 
            [
                    {"Value" : "","Text" : "All Applicable Statuses"},
                    { "Value": "1", "Text": "Not Confirmed" },
                    {"Value" : "2","Text" : "Confirmed"}
            ],
            "Contact" : 
            [
                    {"Value" : "","Text" : "All Applicable Statuses"},
                    {"Value" : "3","Text" : "Active"},
                    {"Value" : "4","Text" : "Inactive"}
            ],
            };

           //Populate ddlStatus
           var updateContactStatus = function(contacttype) {
               var listItems = "";

               for (var i = 0; i < contactStatusJsonList[contacttype].length; i++) {
                    listItems += "<option value='" + contactStatusJsonList[contacttype][i].Value + "'>" + contactStatusJsonList[contacttype][i].Text + "</option>";
               }
               $("#<%= ddlStatus.ClientID%>").html(listItems); 
            }


            //ddlType onChange event
            $("#<%= ddlType.ClientID%>").on('change',function(){
                var selectedType = $('#<%= ddlType.ClientID%> option:selected').text();
                updateContactStatus(selectedType);
                 $('#<%= hdnStatus.ClientID%>').val($('#<%= ddlStatus.ClientID%> option:selected').val());
            });  

            //ddlStatus onchange event
             $("#<%= ddlStatus.ClientID%>").on('change',function(){
                $('#<%= hdnStatus.ClientID%>').val($('#<%= ddlStatus.ClientID%> option:selected').val());
            });  

             //populate contact status on load
            updateContactStatus($('#<%= ddlType.ClientID%> option:selected').text());
            if ($('#<%= hdnStatus.ClientID%>').val() != '') {
                $('#<%= ddlStatus.ClientID%>').val($('#<%= hdnStatus.ClientID%>').val());
            }

            if ($('#<%=hdnDateSelected.ClientID%>').val() == '') {
                $('#<%=txtStartDate.ClientID%>').val(startDate());
                $('#<%=txtEndDate.ClientID%>').val(endDate());
            }

        });
		
	
        function ConfirmDelete() {
            return window.confirm("Are you sure you want to delete this lead?");
        }

         
        function ValidateRequiredFields() {
            $('#<%=hdnDateSelected.ClientID%>').val('1');

            if ($('#<%=txtStartDate.ClientID%>').val() == '') {
                 $("#invalidStartDate").text('Please input Date Created Start Date.');
                $("#invalidStartDate").show();
                $("#<%=txtStartDate.ClientID%>").css({"border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)"});
                return false;
            }
            if ($('#<%=txtEndDate.ClientID%>').val() == '') return false;

            var dStartDate = new Date($('#<%=txtStartDate.ClientID%>').val());
            var dEndDate = new Date($('#<%=txtEndDate.ClientID%>').val());

            if (dStartDate > dEndDate) {

                $("#invalidStartDate").text('Date Created Start Date cannot be greater than Date Created End Date');
                $("#invalidStartDate").show();
                $("#<%=txtStartDate.ClientID%>").css({"border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)"});
                return false;
            }

            return true;
        }

        function Validate() {
            /*bootstrap validation: shows the error messages for required fields; already prevents form submission of a required field is blank*/
            var forms = document.getElementById('dvFormValidate');            
            forms.classList.add('was-validated');

           return ValidateRequiredFields();
        }

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

    </script>
	<link href="../../dcjqaccordion-master/css/skins/graphite.css" rel="stylesheet" type="text/css" />
    <title>Marketing Automation Tool - Leads Listing</title>
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
				  <asp:LinkButton ID="lnkLeads" runat="server" CssClass="breadcrumb-item"  Text="Leads" OnClick="lnkLeads_Click"></asp:LinkButton>
                  <asp:LinkButton ID="lnkReports" runat="server" CssClass="breadcrumb-item"  Text="Reports" OnClick="lnkReports_Click" ></asp:LinkButton>
                  <span class="breadcrumb-item active">Leads Listing</span>
				</nav>
			</div>
</asp:Content>  
<asp:Content ID="Content5" ContentPlaceHolderID="cpBody" runat="server"> 
    <uc:LeftSearch runat="server" id="LeadLeftSearch"></uc:LeftSearch>
				<div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 middlecontent">
					<div class="page_header">
                        <div class="form-horizontal">
							<div class="container">
								<div class="row">
									<h5 class="page_title">Leads Listing</h5>
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
                                                    <div class="needs-validation" id="dvFormValidate" novalidate>
													<div class="row">
														<div class="form-group form-group-sm col-sm-12 required">
															<div class="row">
															    <label class="col-form-label col-sm-2" >Date Created:</label>
															    <div class="col-sm-4">
																    <div class="input-group mb-3">
																      <div class="input-group-prepend">
																	    <span class="input-group-text" id="basic-addon1"><i class="far fa-calendar-alt"></i></span>
																      </div>
																       <asp:TextBox ID="txtStartDate" type="date"  runat="server" class="form-control" placeholder="Date From"  aria-describedby="basic-addon1" required></asp:TextBox>
                                                                        <div id="invalidStartDate" class="invalid-feedback">
                	                                                        Please input Date Created Start Date.
                                                                        </div>
																    </div>
															    </div>
                                                                <label class="col-form-label col-sm-2" >To:</label>
															    <div class="col-sm-4">
																    <div class="input-group mb-3">
																      <div class="input-group-prepend">
																	    <span class="input-group-text" id="basic-addon1"><i class="far fa-calendar-alt"></i></span>
																      </div>
																       <asp:TextBox ID="txtEndDate" type="date"  runat="server" class="form-control" placeholder="Date To"  aria-describedby="basic-addon1" required></asp:TextBox>
																        <div id="invalidEndDate" class="invalid-feedback">
                	                                                        Please input Date Created End Date.
                                                                        </div>
                                                                    </div>
															    </div>
															</div>
														</div>
													</div>
                                                    <div class="row">
														<div class="form-group form-group-sm col-sm-12">
															<div class="row">
															<label class="col-form-label col-sm-2" >Source:</label>
															<div class="col-sm-10">
                                                                <asp:DropDownList ID="ddlSource" runat="server" class="form-control"></asp:DropDownList>
															</div>
															</div>
														</div>
													</div>
                                                    <div class="row">
														<div class="form-group form-group-sm col-sm-12">
															<div class="row">
															<label class="col-form-label col-sm-2" >Type:</label>
															<div class="col-sm-10">
                                                                 <asp:DropDownList ID="ddlType" class="form-control" runat="server" >
                                                                        <asp:ListItem Value="" Text="All Types"></asp:ListItem>
                                                                        <asp:ListItem Value="1" Text="Lead"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="Contact"></asp:ListItem>
                                                                </asp:DropDownList>
															</div>
															</div>
														</div>
													</div>
                                                     <div class="row">
														<div class="form-group form-group-sm col-sm-12">
															<div class="row">
															<label class="col-form-label col-sm-2" >Status:</label>
															<div class="col-sm-10">
                                                                 <asp:DropDownList ID="ddlStatus" class="form-control" runat="server" >
                                                                </asp:DropDownList>
                                                                <input type="hidden" runat="server" id="hdnStatus"/>
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
    <input type="hidden" id="hdnDateSelected" name="hdnDateSelected" runat="server"/>
</asp:Content>  




