<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CampaignBuilder.aspx.cs" Inherits="MarketingAutomationTool.Campaigns.CampaignBuilder" MasterPageFile="~/MATSite.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HtmlEditor.Sanitizer" TagPrefix="ajaxToolkit" %>

<%@ Register Src="~/UserControls/NewEmailCampaign.ascx" TagName="NewEmailCampaign" TagPrefix="uc" %>
<%@ Register Src="~/Campaigns/UserControls/TopMenu.ascx" TagName="TopMenu" TagPrefix="uc" %>
<%@ Register Src="~/Campaigns/UserControls/LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server"> 
    <link rel="stylesheet" href="../dcjqaccordion-master/css/dcaccordion.css" />
     <style>
    
        .ajax__html_editor_extender_container
        {
	        width: 100%  !important;
	        height:400px !important;
        }
        .ajax__html_editor_extender_texteditor
        {
	        height:350px !important;
        }

        #txtBody_HtmlEditorExtender_btnCancel{
            padding-left:6px !important;
        }

    </style>
    <script src="../jQuery/jquery-3.3.1.min.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.cookie.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.hoverIntent.minified.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/js/jquery.dcjqaccordion.2.7.min.js" type="text/javascript"></script>
	<script src="../dcjqaccordion-master/accordionmenu.js" type="text/javascript"></script>
    <script type="text/javascript">

        var currentTab = 0; // stepwizard global var currentTab

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
			
			$(".delete").click(function () {
                window.confirm('Are you sure you want to delete this record?');
            });
             /*end --left search -- end*/

            /*begin -- stepwizard -- begin*/
            currentTab = 0; // Current tab is set to be the first tab (0)
            showTab(currentTab); // Display the current tab
            /*end -- stepwizard -- end*/

             /*begin -- campaigndetails -- begin*/
            $("#<%=txtCampaignDescription.ClientID%>").keydown(function() {
                LimitText($("#<%=txtCampaignDescription.ClientID%>"),250);
            });

            $("#<%=txtCampaignDescription.ClientID%>").keyup(function() {
                LimitText($("#<%=txtCampaignDescription.ClientID%>"),250);
            });

            $("#<%=chkUseBounceAddress.ClientID%>").change(function () {
                if (this.checked) {
                    $("#<%=hdnbouncenamelastvalue.ClientID%>").val($("#<%=txtSenderName.ClientID%>").val());
                    $("#<%=hdnemailaddresslastvalue.ClientID%>").val($("#<%=txtSenderEmail.ClientID%>").val());

                    $("#<%=txtSenderName.ClientID%>").val($("#<%=hdnbouncename.ClientID%>").val());
                    $("#<%=txtSenderEmail.ClientID%>").val($("#<%=hdnemailaddress.ClientID%>").val());
                }
                else {
                    $("#<%=txtSenderName.ClientID%>").val($("#<%=hdnbouncenamelastvalue.ClientID%>").val());
                    $("#<%=txtSenderEmail.ClientID%>").val($("#<%=hdnemailaddresslastvalue.ClientID%>").val());
                }
            });

            $("#<%= ddlDatabaseField.ClientID%>").on('change', function () {
                if ($("#<%=ddlDatabaseField.ClientID%>").val() != '') {
                    var sCampaignSubject = $("#<%=txtCampaignSubject.ClientID%>").val();
                    var sDatabaseField = $("#<%=ddlDatabaseField.ClientID%>").val();
                    var sNewCampaignSubject = sCampaignSubject.concat(' ', sDatabaseField);

                    if (sNewCampaignSubject.length > 250) {
                        $("#invalidSubject").text('Subject length exceeds 250 characters.');
                        $("#invalidSubject").show();
                        $("#<%=txtCampaignSubject.ClientID%>").css({ "border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)" });
                        $("#<%=txtCampaignSubject.ClientID%>").focus();
                    }
                    else {
                        $("#<%=txtCampaignSubject.ClientID%>").val(sCampaignSubject.concat(' ', sDatabaseField));
                        $("#<%=txtCampaignSubject.ClientID%>").focus();
                    }
                }
            });  

            
            /*end -- campaigndetails -- end*/


        });

        function showTab(n) {
          // This function will display the specified tab of the form ...
          var x = document.getElementsByClassName("campaigntab");
          x[n].style.display = "block";
          // ... and fix the Previous/Next buttons:
          // ... and run a function that displays the correct step indicator:
          fixStepIndicator(n)
        }

        function nextPrev(n) {
          // This function will figure out which tab to display
          var x = document.getElementsByClassName("campaigntab");

          // Hide the current tab:
          x[currentTab].style.display = "none";
          // Increase or decrease the current tab by 1:
          currentTab = currentTab + n;
          // if you have reached the end of the form... :
 
          // Otherwise, display the correct tab:
          showTab(currentTab);
        }

        function fixStepIndicator(n) {
          // This function removes the "active" class of all steps...
          var i, x = document.getElementsByClassName("step");
          for (i = 0; i < x.length; i++) {
  
	        x[i].className = x[i].className.replace(" prev", "");
            x[i].className = x[i].className.replace(" active", "");
          }
          //... and adds the "active" class to the current step:
          for (i = 0; i < n; i++) {
  
             x[i].className += " prev";
          }
  
          x[n].className += " active";
        }

        function LimitText(textfield,maxlength) {
            var text = textfield.val();
            if (text.trim().length > maxlength) {
                   textfield.val(text.substring(0,maxlength));
            }
        }

        function validateEmail(email) {
              var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
              return re.test(email);
        }

        function ValidateEmailAddress() {
                    if (validateEmail($('#<%=txtSenderEmail.ClientID%>').val()))
                    {
                         return true;
                    }
                    else {
                        $("#invalidSenderEmail").text('Please enter a valid Sender Email Address. e.g. example@mail.com');
                        $("#invalidSenderEmail").show();
                        $("#<%=txtSenderEmail.ClientID%>").css({"border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)"});
                        return false;
                    }
        }

         function ValidateRequiredFields() {
           
            if ($('#<%=txtCampaignName.ClientID%>').val() == '') return false;
            if ($('#<%=txtCampaignSubject.ClientID%>').val() == '') return false;
             if ($('#<%=txtSenderEmail.ClientID%>').val() == '') {
                 return false;
             }
             else {
                 return ValidateEmailAddress();
             }
            if ($('#<%=txtSenderName.ClientID%>').val() == '') return false;
                       
            return true;
            
        }

        function ValidateCampaignDetails(n) {
            var forms = document.getElementById('dvFormValidate');            
            forms.classList.add('was-validated');


            if (ValidateRequiredFields()) {
                ShowEditorBasedOnCampaignFormat();
                nextPrev(n);
            }
        
            return false;
        }


        function ShowEditorBasedOnCampaignFormat() {
             if ($("#<%= ddlCampaignFormat.ClientID%>").val() == '1') {
                    $("#dvHTML").show();
                    $("#dvText").hide();
                }
                else {
                    $("#dvHTML").hide();
                    $("#dvText").show();
                }
        }
    </script>
    <link href="../dcjqaccordion-master/css/skins/graphite.css" rel="stylesheet" type="text/css" />
    <title>Marketing Automation Tool - Campaign Builder</title>
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
				   <asp:LinkButton ID="lnkBreadHome" runat="server" CssClass="breadcrumb-item"  Text="Home" OnClick="lnkBreadHome_Click"></asp:LinkButton>
				  <span class="breadcrumb-item active">Campaigns</span>
				</nav>
			</div>
</asp:Content> 
<asp:Content ID="Content5" ContentPlaceHolderID="cpBody" runat="server"> 
          <uc:LeftMenu runat="server" id="LeadLeftSearch"></uc:LeftMenu>
    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 middlecontent">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
					<div class="page_header">
							<h5 class="page_title">Email Campaign Wizard</h5>
					</div>
					<div id="stepwizard" class="col-lg-12">
							<div style="text-align:center;">
							  <div class="step active"><div class="stepnum">1</div></div>
							  <div class="step"><div class="stepnum">2</div></div>
							  <div class="step"><div class="stepnum">3</div></div>
							  <div class="step"><div class="stepnum">4</div></div>
							</div>
					</div>
					<div class="page_content">
						<div class="container-fluid">
							<div class="form-horizontal">
									<div class="container">
									<div class="campaigntab">
                                         <div class="needs-validation" id="dvFormValidate" novalidate>
										<legend class="legendstyle">Step 1: Campaign Details</legend>
										 <div class="form-group row required">
													    <label class="col-form-label col-sm-2" >Campaign Name:</label>
													    <div class="col-sm-10">
														    <asp:TextBox ID="txtCampaignName" class="form-control"  runat="server" placeholder="Campaign Name" MaxLength="128" required ></asp:TextBox>
                                                             <div id="invalidFirstName" class="invalid-feedback">
                                                                  Please enter Campaign Name.
                                                             </div>
                                                        </div>
										</div>
										 <div class="form-group row">
													<label class="col-form-label col-sm-2" >Format:</label>
													<div class="col-sm-10">
                                                        <asp:DropDownList ID="ddlCampaignFormat" runat="server" class="form-control" style="width:150px">
                                                            <asp:ListItem Value="1">HTML</asp:ListItem>
                                                            <asp:ListItem Value="0">Text</asp:ListItem>
                                                        </asp:DropDownList>
													</div>
										</div>
										<div class="form-group row">
													<label class="col-form-label col-sm-2" >Description:</label>
													<div class="col-sm-10">
														<asp:TextBox ID="txtCampaignDescription" class="form-control"  runat="server" placeholder="Description" MaxLength="250" TextMode="MultiLine" ></asp:TextBox>
													</div>
										</div>
										<div class="form-group row required">
													<label class="col-form-label col-sm-2" >Subject:</label>
													<div class="col-sm-10">
														<asp:TextBox ID="txtCampaignSubject" class="form-control"  runat="server" placeholder="Subject" MaxLength="250" required  ></asp:TextBox>
													    <div id="invalidSubject" class="invalid-feedback">
                                                                  Please enter Subject.
                                                         </div>
                                                    </div>
										</div>
										<div class="row">
											<div class="form-group form-group-sm col-sm-12">
												<div class="row">
													<div class="col-sm-2">&nbsp;
													</div>
													<div class="col-sm-10">
													<div class="row">
														<div class="col-sm-4">
															<label class="col-form-label">Add database field to subject line:</label>
															
														</div>
														<div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlDatabaseField" runat="server" class="form-control" > 
                                                        </asp:DropDownList>
														</div>
														</div>
													</div>
												</div>
											</div>
										</div>
										<div class="form-group row required">
													<label class="col-form-label col-sm-2" >Sender Name:</label>
													<div class="col-sm-10">
														<asp:TextBox ID="txtSenderName" class="form-control"  runat="server" placeholder="Sender Name" MaxLength="256" required ></asp:TextBox>
													     <div id="invalidSenderName" class="invalid-feedback">
                                                                  Please enter Sender Name.
                                                         </div>
                                                    </div>
										</div>
										<div class="form-group row required">
													<label class="col-form-label col-sm-2" >Sender Email:</label>
													<div class="col-sm-10">
														<asp:TextBox ID="txtSenderEmail" class="form-control"  runat="server" placeholder="Sender Email" MaxLength="250" required ></asp:TextBox>
													    <div id="invalidSenderEmail" class="invalid-feedback">
                                                                  Please enter Sender Email.
                                                         </div>
                                                    </div>
										</div>
										<div class="row">
											<div class="form-group form-group-sm col-sm-12">
												<div class="row">
													<div class="col-sm-2">&nbsp;
													</div>
													<div class="col-sm-10">
														<label class="col-form-label">
                                                            <asp:CheckBox ID="chkUseBounceAddress" runat="server"  />&nbsp;Use bounce address in FROM field
														</label>
														
													</div>
												</div>
											</div>
										</div>
										<div class="row">
											<div class="form-group form-group-sm col-sm-12">
												<div class="row">
													<label class="col-form-label col-sm-2" >Hide in Search:</label>
													<div class="col-sm-10">
														<label class="col-form-label">
                                                            <asp:RadioButton ID="rdbYes" runat="server"  GroupName="grpHide" />&nbsp;Yes
														</label>
                                                        &nbsp;&nbsp;
														<label class="col-form-label">
                                                            <asp:RadioButton ID="rdbNo" runat="server"  GroupName="grpHide" Checked="true" />&nbsp;No
														</label>
													</div>
												</div>
											</div>
										</div>
										<div class="row">
														<div class="form-group form-group-sm col-sm-12">
															<div class="row">
															<div class="col-sm-12 button_panel">
																<div class="float-right mt-2">
                                                                    <asp:Button ID="btnMessage" class="btn btn-primary" runat="server" Text="Message &gt;" OnClientClick="return ValidateCampaignDetails(1);" />
																</div>
															</div>
															</div>
														</div>
											</div>
                                            </div>
										</div>
										<div class="campaigntab">
										<legend class="legendstyle">Step 2: Message Body</legend>
                                         <div class="row" id="dvHTML">
														<div class="form-group form-group-sm col-sm-12">
															<div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="containerClass">
                                                                     <asp:TextBox runat="server"  ID="txtBody" Columns="50" Rows="10" TextMode="MultiLine"></asp:TextBox>
                                                                     <ajaxToolkit:HtmlEditorExtender ID="txtBody_HtmlEditorExtender" runat="server" BehaviorID="txtBody_HtmlEditorExtender" TargetControlID="txtBody" DisplayPreviewTab="True" DisplaySourceTab="True" OnImageUploadComplete="txtBody_HtmlEditorExtender_ImageUploadComplete">
                                                                         <Toolbar> 
                                                                        <ajaxToolkit:Undo />
                                                                        <ajaxToolkit:Redo />
                                                                        <ajaxToolkit:Bold />
                                                                        <ajaxToolkit:Italic />
                                                                        <ajaxToolkit:Underline />
                                                                        <ajaxToolkit:StrikeThrough />
                                                                        <ajaxToolkit:Subscript />
                                                                        <ajaxToolkit:Superscript />
                                                                        <ajaxToolkit:JustifyLeft />
                                                                        <ajaxToolkit:JustifyCenter />
                                                                        <ajaxToolkit:JustifyRight />
                                                                        <ajaxToolkit:JustifyFull />
                                                                        <ajaxToolkit:InsertOrderedList />
                                                                        <ajaxToolkit:InsertUnorderedList />
                                                                        <ajaxToolkit:CreateLink />
                                                                        <ajaxToolkit:UnLink />
                                                                        <ajaxToolkit:RemoveFormat />
                                                                        <ajaxToolkit:SelectAll />
                                                                        <ajaxToolkit:UnSelect />
                                                                        <ajaxToolkit:Delete />
                                                                        <ajaxToolkit:Cut />
                                                                        <ajaxToolkit:Copy />
                                                                        <ajaxToolkit:Paste />
                                                                        <ajaxToolkit:BackgroundColorSelector />
                                                                        <ajaxToolkit:ForeColorSelector />
                                                                        <ajaxToolkit:FontNameSelector />
                                                                        <ajaxToolkit:FontSizeSelector />
                                                                        <ajaxToolkit:Indent />
                                                                        <ajaxToolkit:Outdent />
                                                                        <ajaxToolkit:InsertHorizontalRule />
                                                                        <ajaxToolkit:HorizontalSeparator />
                                                                        <ajaxToolkit:InsertImage AjaxFileUploadHandlerPath="../Public/Campaigns/Images" />
                                                                    </Toolbar>
                                                                     </ajaxToolkit:HtmlEditorExtender>
                                                                     </div>
                                
                                                                    <br/>
                                                                    <br/>
                                                                    <br/>
                                                                </div>
															</div>
														</div>
										</div>
                                            <div class="row" id="dvText">
														<div class="form-group form-group-sm col-sm-12">
															<div class="row">
															    <div class="col-sm-12">
                                                                    <asp:TextBox ID="txtTextEditor" runat="server" class="form-control"  TextMode="MultiLine" Height="350px" ></asp:TextBox>
                                                               
                                                                     <br/>
                                                                    <br/>
                                                                    <br/>    
                                                                </div>
															</div>
														</div>
										</div>
										<div class="row">
														<div class="form-group form-group-sm col-sm-12">
															<div class="row">
															<div class="col-sm-12 button_panel">
																<div class="float-left mt-2" style="padding-left: 5px;">
																	<button type="button" class="btn btn-light" onclick="nextPrev(-1)">&lt; Campaign Details</button>
																</div>
																<div class="float-right mt-2">
																	<button type="button" class="btn btn-primary" onclick="nextPrev(1)">Recipients &gt;</button>
																</div>
															</div>
															</div>
														</div>
										</div>
										</div>
										<div class="campaigntab">
										<legend class="legendstyle">Step 3: Recipients</legend>
										<div class="row">
														<div class="form-group form-group-sm col-sm-12">
															<div class="row">
															<div class="col-sm-12 button_panel">
																<div class="float-left mt-2" style="padding-left: 5px;">
																	<button type="button" class="btn btn-light" onclick="nextPrev(-1)">&lt; Message Body</button>
																</div>
																<div class="float-right mt-2">
																	<button type="button" class="btn btn-primary" onclick="nextPrev(1)">Send/Schedule Campaign &gt;</button>
																</div>
															</div>
															</div>
														</div>
										</div>
										</div>
										<div class="campaigntab">
										<legend class="legendstyle">Step 4: Send/Schedule Campaign</legend>
										<div class="row">
											<div class="form-group form-group-sm col-sm-12">
												<div class="row">
													<label class="col-form-label col-sm-2" >Sending Options:</label>
													<div class="col-sm-10">
														<select class="form-control" style="width:150px">
																	<option value="HTML">Send Now</option>
																	<option value="Text">Schedule</option>
														</select>
													</div>
												</div>
											</div>
										</div>
										<div class="row">
														<div class="form-group form-group-sm col-sm-12">
															<div class="row">
															<div class="col-sm-12 button_panel">
																<div class="float-left mt-2" style="padding-left: 5px;">
																	<button type="button" class="btn btn-light" onclick="nextPrev(-1)">&lt; Recipients</button>
																</div>
																<div class="float-right mt-2">
																	<button type="button" class="btn btn-primary">Send</button>
																</div>
															</div>
															</div>
														</div>
										</div>
										</div>
									</div>
							</div>
						
						
										
						</div>
						<br/>
					</div>
						
					
				</div>
    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 rightmenu">
				<div class="container-fluid">
					<div class="graphite accordion-container-right">
						<ul class="accordion" id="accordion-right">
							<li class="dcjq-current-parent"><a href="#"  class="dcjq-parent active">Shortcuts</a>
								<ul>
									<li>
                                        <asp:LinkButton ID="lnkSaveAsDraft" runat="server" OnClick="lnkSaveAsDraft_Click">Save As Draft</asp:LinkButton>
									</li>
								
								</ul>
							</li>
						</ul>
					</div>
				</div>	
				</div>
    <input type="hidden" id="hdnbouncename" name="hdnbouncename" runat="server"/>
    <input type="hidden" id="hdnemailaddress" name="hdnemailaddress" runat="server"/>
    <input type="hidden" id="hdnbouncenamelastvalue" name="hdnbouncenamelastvalue" runat="server"/>
    <input type="hidden" id="hdnemailaddresslastvalue" name="hdnemailaddresslastvalue" runat="server"/>
    
</asp:Content>  
