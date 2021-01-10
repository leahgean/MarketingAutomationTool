<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultScoring.aspx.cs" Inherits="MarketingAutomationTool.MyAccount.DefaultScoring" MasterPageFile="~/MATSite.Master" %>
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
	<script type="text/javascript">
        $(document).ready(function () {
	
		    $("#lnkForm").click(function(){
			    $("#lnkForm").addClass("active");
			    $("#tabForm").addClass("active");
			    $("#lnkEmail").removeClass("active");
			    $("#tabEmail").removeClass("active");
			    $("#lnkSMS").removeClass("active");
			    $("#tabSMS").removeClass("active");
		    });
		
		    $("#lnkEmail").click(function(){
			    $("#lnkForm").removeClass("active");
			    $("#tabForm").removeClass("active");
			    $("#lnkEmail").addClass("active");
			    $("#tabEmail").addClass("active");
			    $("#lnkSMS").removeClass("active");
			    $("#tabSMS").removeClass("active");
		    });
		
		    $("#lnkSMS").click(function(){
			    $("#lnkForm").removeClass("active");
			    $("#tabForm").removeClass("active");
			    $("#lnkEmail").removeClass("active");
			    $("#tabEmail").removeClass("active");
			    $("#lnkSMS").addClass("active");
			    $("#tabSMS").addClass("active");
            });
        });

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode >= 48 && charCode <= 57)||charCode==45)
                return true;

            return false;
        }

        function PrintScoreContent() {
            var str = "<h2>Default Scoring</h2>" +
                "<table cellspacing='5' cellpadding='5'>" +
                <%--"<tr><td colspan='2'><strong>Form</strong></td></tr>" +
                "<tr><td>Submitting Form</td><td>" + $('#<%=txtFormSubmittingForm.ClientID%>').val() + "</td></tr>" +
                "<tr><td>Confirmation request email bounces</td><td>" + $('#<%=txtFormConfReqEmBo.ClientID%>').val() + "</td></tr>" +
                "<tr><td>Unsubscribe from confirmation email</td><td>" + $('#<%=txtFormUnsubsFromConfEm.ClientID%>').val() + "</td></tr>" +
                "<tr><td>Acknowledgement email bounces</td><td>" + $('#<%=txtFormAckEmBounces.ClientID%>').val() + "</td></tr>" +
                "<tr><td>First open acknowledgement email</td><td>" + $('#<%=txtFormFirstOpenAckEm.ClientID%>').val() + "</td></tr>" +
                "<tr><td>Subsequent open</td><td>" + $('#<%=txtFormSubsequentOpen.ClientID%>').val() + "</td></tr>" +
                "<tr><td>Unsubscribe from acknowledgement email</td><td>" + $('#<%=txtFormUnsubscribeFromAckEm.ClientID%>').val() + "</td></tr>" +
                "<tr><td>First click on a link in acknowledgement</td><td>" + $('#<%=txtFormFirstClickAck.ClientID%>').val() + "</td></tr>" +
                "<tr><td>Subsequent clicks</td><td>" + $('#<%=txtFormSubsequenctClicks.ClientID%>').val() + "</td></tr>" +--%>
                "<tr><td colspan='2'><strong>Email</strong></td></tr>" +
                "<tr><td>Message bounces</td><td>" + $('#<%=txtEmailMessageBounces.ClientID%>').val() + "</td></tr>" +
                "<tr><td>Unsubscribe from email</td><td>" + $('#<%=txtEmailUnsubscribeEmail.ClientID%>').val() + "</td></tr>" +
                "<tr><td>First click on a link in email</td><td>" + $('#<%=txtEmailFirstClick.ClientID%>').val() + "</td></tr>" +
                "<tr><td>Subsequent clicks</td><td>" + $('#<%=txtEmailSubsequentClicks.ClientID%>').val() + "</td></tr>"
               <%-- "<tr><td colspan='2'><strong>SMS</strong></td></tr>" +
                "<tr><td>Message bounces</td><td>" + $('#<%=txtSMSMessageBounces.ClientID%>').val() + "</td></tr>" +
                "<tr><td>Unsubscribe after SMS</td><td>" + $('#<%=txtSMSUnsubscribeAfterSMS.ClientID%>').val() + "</td></tr>"--%>

            return str;
        }

         function CallPrint(strid) {
            var prtContent = PrintScoreContent();
            var WinPrint = window.open('print.htm', 'PrintUsersWindow', 'letf=0,top=0,width=800%,height=600,toolbar=1,scrollbars=1,status=1');
            WinPrint.document.write(prtContent);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }

        function validatesignedint(score) {
              var re = /^(\+|-)?\d+$/;
              return re.test(score);
        }

        function ValidateRequiredFields() {
            var cnt = 0;
            var message = "<ul class='orderedlist'>";
            <%--if ($('#<%=txtFormSubmittingForm.ClientID%>').val() == '') {
                message += "<li><strong>Form</strong> Submitting Form score is a required field.</li>"
                cnt++;
            }
            else {
                if (!validatesignedint($('#<%=txtFormSubmittingForm.ClientID%>').val()))
                {
                        $("#invFormSubmittingForm").text('Submitting Form score should be a whole number. e.g. 1 or -1');
                        $("#invFormSubmittingForm").show();
                        $("#<%=txtFormSubmittingForm.ClientID%>").css({"border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)"});
                        message += "<li><strong>Form</strong> Submitting Form score should be a whole number. e.g. 1 or -1</li>"
                        cnt++;
                }
            }
            
            if ($('#<%=txtFormConfReqEmBo.ClientID%>').val() == '') {
                message += "<li><strong>Form</strong> Confirmation request email bounces score is a required field.</li>"
                cnt++;
            }
            else {
                if (!validatesignedint($('#<%=txtFormConfReqEmBo.ClientID%>').val()))
                {
                        $("#invFormConfReEmBo").text('Confirmation request email bounces score should be a whole number. e.g. 1 or -1');
                        $("#invFormConfReEmBo").show();
                        $("#<%=txtFormConfReqEmBo.ClientID%>").css({"border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)"});
                        message += "<li><strong>Form</strong> Confirmation request email bounces score should be a whole number. e.g. 1 or -1</li>"
                        cnt++;
                }
            }

            if ($('#<%=txtFormUnsubsFromConfEm.ClientID%>').val() == '') {
                message += "<li><strong>Form</strong> Unsubscribe from confirmation email score is a required field.</li>"
                cnt++;
            }
            else {
                if (!validatesignedint($('#<%=txtFormUnsubsFromConfEm.ClientID%>').val()))
                {
                        $("#invFormUnFrCoEm").text('Unsubscribe from confirmation email score should be a whole number. e.g. 1 or -1');
                        $("#invFormUnFrCoEm").show();
                        $("#<%=txtFormUnsubsFromConfEm.ClientID%>").css({"border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)"});
                        message += "<li><strong>Form</strong> Unsubscribe from confirmation email score should be a whole number. e.g. 1 or -1</li>"
                        cnt++;
                }
            }

            if ($('#<%=txtFormAckEmBounces.ClientID%>').val() == '') {
                message += "<li><strong>Form</strong> Acknowledgement email bounces score is a required field.</li>"
                cnt++;
            }
           else {
                if (!validatesignedint($('#<%=txtFormAckEmBounces.ClientID%>').val()))
                {
                        $("#invFormAckEmBo").text('Acknowledgement email bounces score should be a whole number. e.g. 1 or -1');
                        $("#invFormAckEmBo").show();
                        $("#<%=txtFormAckEmBounces.ClientID%>").css({"border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)"});
                        message += "<li><strong>Form</strong> Acknowledgement email bounces score should be a whole number. e.g. 1 or -1</li>"
                        cnt++;
                }
            }

            if ($('#<%=txtFormFirstOpenAckEm.ClientID%>').val() == '') {
                message += "<li><strong>Form</strong> First open acknowledgement email score is a required field.</li>"
                cnt++;
            }
            else {
                if (!validatesignedint($('#<%=txtFormFirstOpenAckEm.ClientID%>').val()))
                {
                        $("#invFormFiOpAckEm").text('First open acknowledgement email score should be a whole number. e.g. 1 or -1');
                        $("#invFormFiOpAckEm").show();
                        $("#<%=txtFormFirstOpenAckEm.ClientID%>").css({"border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)"});
                        message += "<li><strong>Form</strong> First open acknowledgement email score should be a whole number. e.g. 1 or -1</li>"
                        cnt++;
                }
            }

            if ($('#<%=txtFormSubsequentOpen.ClientID%>').val() == '') {
                 message += "<li><strong>Form</strong> Subsequent open score is a required field.</li>"
                            cnt++;
            }
           else {
                if (!validatesignedint($('#<%=txtFormSubsequentOpen.ClientID%>').val()))
                {
                        $("#invFormSubOp").text('Subsequent open score should be a whole number. e.g. 1 or -1');
                        $("#invFormSubOp").show();
                        $("#<%=txtFormSubsequentOpen.ClientID%>").css({"border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)"});
                        message += "<li><strong>Form</strong> Subsequent open score should be a whole number. e.g. 1 or -1</li>"
                        cnt++;
                }
            }

            if ($('#<%=txtFormUnsubscribeFromAckEm.ClientID%>').val() == '') {
                 message += "<li><strong>Form</strong> Unsubscribe from acknowledgement email score is a required field.</li>"
                 cnt++;
            }
           else {
                if (!validatesignedint($('#<%=txtFormUnsubscribeFromAckEm.ClientID%>').val()))
                {
                        $("#invFormUnFrAckEm").text('Unsubscribe from acknowledgement email score should be a whole number. e.g. 1 or -1');
                        $("#invFormUnFrAckEm").show();
                        $("#<%=txtFormUnsubscribeFromAckEm.ClientID%>").css({"border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)"});
                        message += "<li><strong>Form</strong> Unsubscribe from acknowledgement email score should be a whole number. e.g. 1 or -1</li>"
                        cnt++;
                }
            }

            if ($('#<%=txtFormFirstClickAck.ClientID%>').val() == '') {
                 message += "<li><strong>Form</strong> First click on a link in acknowledgement email score is a required field.</li>"
                            cnt++;
            }
            else {
                if (!validatesignedint($('#<%=txtFormFirstClickAck.ClientID%>').val()))
                {
                        $("#invFormFiClOnALiInAckEm").text('First click on a link in acknowledgement email score should be a whole number. e.g. 1 or -1');
                        $("#invFormFiClOnALiInAckEm").show();
                        $("#<%=txtFormFirstClickAck.ClientID%>").css({"border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)"});
                        message += "<li><strong>Form</strong> First click on a link in acknowledgement email score should be a whole number. e.g. 1 or -1</li>"
                        cnt++;
                }
            }

            if ($('#<%=txtFormSubsequenctClicks.ClientID%>').val() == '') {
                 message += "<li><strong>Form</strong> Subsequent clicks score is a required field.</li>"
                            cnt++;
            }
            else {
                if (!validatesignedint($('#<%=txtFormSubsequenctClicks.ClientID%>').val()))
                {
                        $("#invFormSubCli").text('Subsequent clicks score should be a whole number. e.g. 1 or -1');
                        $("#invFormSubCli").show();
                        $("#<%=txtFormSubsequenctClicks.ClientID%>").css({"border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)"});
                        message += "<li><strong>Form</strong> Subsequent clicks score should be a whole number. e.g. 1 or -1</li>"
                        cnt++;
                }
            }--%>

            if ($('#<%=txtEmailMessageBounces.ClientID%>').val() == '') {
                 message += "<li><strong>Email</strong> Message bounces score is a required field.</li>"
                            cnt++;
            }
           else {
                if (!validatesignedint($('#<%=txtEmailMessageBounces.ClientID%>').val()))
                {
                        $("#invEmailMeBo").text('Message bounces score should be a whole number. e.g. 1 or -1');
                        $("#invEmailMeBo").show();
                        $("#<%=txtEmailMessageBounces.ClientID%>").css({"border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)"});
                        message += "<li><strong>Email</strong> Message bounces score should be a whole number. e.g. 1 or -1</li>"
                        cnt++;
                }
            }

            if ($('#<%=txtEmailUnsubscribeEmail.ClientID%>').val() == '') {
                 message += "<li><strong>Email</strong> Unsubscribe from email score is a required field.</li>"
                            cnt++;
            }
           else {
                if (!validatesignedint($('#<%=txtEmailUnsubscribeEmail.ClientID%>').val()))
                {
                        $("#invEmailUnFrEm").text('Unsubscribe from email score should be a whole number. e.g. 1 or -1');
                        $("#invEmailUnFrEm").show();
                        $("#<%=txtEmailUnsubscribeEmail.ClientID%>").css({"border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)"});
                        message += "<li><strong>Email</strong> Unsubscribe from email score should be a whole number. e.g. 1 or -1</li>"
                        cnt++;
                }
            }

            if ($('#<%=txtEmailFirstClick.ClientID%>').val() == '') {
                 message += "<li><strong>Email</strong> First click on a link in email score is a required field.</li>"
                            cnt++;
            }
            else {
                if (!validatesignedint($('#<%=txtEmailFirstClick.ClientID%>').val()))
                {
                        $("#invEmailFiCliOnALiInEm").text('First click on a link in email score should be a whole number. e.g. 1 or -1');
                        $("#invEmailFiCliOnALiInEm").show();
                        $("#<%=txtEmailFirstClick.ClientID%>").css({"border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)"});
                        message += "<li><strong>Email</strong> First click on a link in email score should be a whole number. e.g. 1 or -1</li>"
                        cnt++;
                }
            }

            if ($('#<%=txtEmailSubsequentClicks.ClientID%>').val() == '') {
                 message += "<li><strong>Email</strong> Subsequent clicks score is a required field.</li>"
                            cnt++;
            }
            else {
                if (!validatesignedint($('#<%=txtEmailSubsequentClicks.ClientID%>').val()))
                {
                        $("#invEmailSubCli").text('Subsequent clicks score should be a whole number. e.g. 1 or -1');
                        $("#invEmailSubCli").show();
                        $("#<%=txtEmailSubsequentClicks.ClientID%>").css({"border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)"});
                        message += "<li><strong>Email</strong> Subsequent clicks score should be a whole number. e.g. 1 or -1</li>"
                        cnt++;
                }
            }

            <%--if ($('#<%=txtSMSMessageBounces.ClientID%>').val() == '') {
                 message += "<li><strong>SMS</strong> Message bounces score is a required field.</li>"
                 cnt++;
            }
            else {
                if (!validatesignedint($('#<%=txtSMSMessageBounces.ClientID%>').val()))
                {
                        $("#invSMSMeBo").text('Message bounces score should be a whole number. e.g. 1 or -1');
                        $("#invSMSMeBo").show();
                        $("#<%=txtSMSMessageBounces.ClientID%>").css({"border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)"});
                        message += "<li><strong>SMS</strong> Message bounces score should be a whole number. e.g. 1 or -1</li>"
                        cnt++;
                }
            }

            if ($('#<%=txtSMSUnsubscribeAfterSMS.ClientID%>').val() == '') {
                 message += "<li><strong>SMS</strong> Unsubscribe after SMS score is a required field.</li>"
                            cnt++;
            }
            else {
                if (!validatesignedint($('#<%=txtSMSUnsubscribeAfterSMS.ClientID%>').val()))
                {
                        $("#invSMSUnAfSMS").text('Unsubscribe after SMS score should be a whole number. e.g. 1 or -1');
                        $("#invSMSUnAfSMS").show();
                        $("#<%=txtSMSUnsubscribeAfterSMS.ClientID%>").css({"border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)"});
                        message += "<li><strong>SMS</strong> Unsubscribe after SMS score should be a whole number. e.g. 1 or -1</li>"
                        cnt++;
                }
            }--%>

            message += "</ul>";

            if (cnt > 0) {
                $("#<%= dvMessage.ClientID%>").html(message);
                $("#<%= dvMessage.ClientID%>").addClass("alert alert-danger");
                $("#<%= dvMessage.ClientID%>").css({"display": "block"});

            }
            else {
                 $("#<%= dvMessage.ClientID%>").html("");
                $("#<%= dvMessage.ClientID%>").removeClass("alert alert-danger");
                $("#<%= dvMessage.ClientID%>").css({"display": "none"});
            }

            return (cnt==0);
            
        }

        function Validate() {
            /*bootstrap validation: shows the error messages for required fields; already prevents form submission of a required field is blank*/
            var forms = document.getElementById('dvFormValidate');            
            forms.classList.add('was-validated');

           return ValidateRequiredFields()
        }
    </script>
    <link href="../dcjqaccordion-master/css/skins/graphite.css" rel="stylesheet" type="text/css" />
    <title>Marketing Automation Tool - Default Scoring</title>
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
            <span class="breadcrumb-item active">Default Scoring</span>
	    </nav>
    </div>
</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="cpBody" runat="server">
     <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 leftmenu">
             <uc:LeftMenu runat="server" id="ucLeftMenu"></uc:LeftMenu>
				
				</div>
				<div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 middlecontent">
					<div class="page_header">
						<h5 class="page_title">Default Scoring</h5>
					</div>
					<div class="page_content">
						<div class="container-fluid">
                            <div class="" role="alert" id="dvMessage" runat="server" style="display:none;">
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                             </div>
							<ul class="nav nav-tabs" id="secondtab">
							  <li class="nav-item" style="display:none;">
								<a class="nav-link" href="#" id="lnkForm">Form</a>
							  </li>
							  <li class="nav-item">
								<a class="nav-link active" href="#" id="lnkEmail">Email</a>
							  </li>
							  <li class="nav-item" style="display:none;">
								<a class="nav-link"  href="#" id="lnkSMS">SMS</a>
							  </li>
							</ul>
                            <div class="needs-validation" id="dvFormValidate" novalidate>
							<div class="tab-content" id="dvTable">
								    <div class="tab-pane" id="tabForm" style="display:none;">
									    <div class="form-horizontal">
										    <div class="container">
											    <div class="row">
												    <table class="table table-striped">
											            <tbody>
												            <tr>
													            <td><label class="col-form-label" >Submitting Form</label></td>
													            <td>
                                                                    <asp:TextBox ID="txtFormSubmittingForm" runat="server" class="form-control"  style="width:50px;" maxlength="3" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                    <div class="invalid-feedback" id="invFormSubmittingForm">
                                                                         Please enter Submitting Form score.
                                                                    </div>
													            </td>
													
												            </tr>
												            <tr>
													            <td><label class="col-form-label" >Confirmation request email bounces</label></td>
													            <td>
                                                                    <asp:TextBox ID="txtFormConfReqEmBo" runat="server" class="form-control"  style="width:50px;" maxlength="3" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                     <div class="invalid-feedback" id="invFormConfReEmBo">
                                                                         Please enter Confirmation request email bounces score.
                                                                    </div>
													            </td>
													
												            </tr>
												            <tr>
													            <td><label class="col-form-label" >Unsubscribe from confirmation email</label></td>
													            <td>
                                                                    <asp:TextBox ID="txtFormUnsubsFromConfEm" runat="server" class="form-control"  style="width:50px;" maxlength="3" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                    <div class="invalid-feedback" id="invFormUnFrCoEm">
                                                                         Please enter Unsubscribe from confirmation email score.
                                                                    </div>
													            </td>
													
												            </tr>
												            <tr>
													            <td><label class="col-form-label" >Acknowledgement email bounces</label></td>
													            <td>
                                                                    <asp:TextBox ID="txtFormAckEmBounces" runat="server" class="form-control"  style="width:50px;" maxlength="3" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                    <div class="invalid-feedback" id="invFormAckEmBo">
                                                                         Please enter Acknowledgement email bounces score.
                                                                    </div>
													            </td>
													
												            </tr>
												            <tr>
													            <td><label class="col-form-label" >First open acknowledgement email</label></td>
													            <td>
                                                                    <asp:TextBox ID="txtFormFirstOpenAckEm" runat="server" class="form-control"  style="width:50px;" maxlength="3" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                     <div class="invalid-feedback" id="invFormFiOpAckEm">
                                                                         Please enter First open acknowledgement email score.
                                                                    </div>
													            </td>
													
												            </tr>
												            <tr>
													            <td><label class="col-form-label" >Subsequent open</label></td>
													            <td>
                                                                    <asp:TextBox ID="txtFormSubsequentOpen" runat="server" class="form-control"  style="width:50px;" maxlength="3" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                    <div class="invalid-feedback" id="invFormSubOp">
                                                                         Please enter Subsequent open score.
                                                                    </div>
													            </td>
													
												            </tr>
												            <tr>
													            <td><label class="col-form-label" >Unsubscribe from acknowledgement email</label></td>
													            <td>
                                                                    <asp:TextBox ID="txtFormUnsubscribeFromAckEm" runat="server" class="form-control"  style="width:50px;" maxlength="3" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                    <div class="invalid-feedback" id="invFormUnFrAckEm">
                                                                         Please enter Unsubscribe from acknowledgement email score.
                                                                    </div>
													            </td>
													
												            </tr>
												            <tr>
													            <td><label class="col-form-label" >First click on a link in acknowledgement email</label></td>
													            <td>
                                                                    <asp:TextBox ID="txtFormFirstClickAck" runat="server"  class="form-control" style="width:50px;" maxlength="3" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                    <div class="invalid-feedback" id="invFormFiClOnALiInAckEm">
                                                                         Please enter First click on a link in acknowledgement email score.
                                                                    </div>
													            </td>
													
												            </tr>
												            <tr>
													            <td><label class="col-form-label" >Subsequent clicks</label></td>
													            <td>
                                                                    <asp:TextBox ID="txtFormSubsequenctClicks" runat="server" class="form-control"  style="width:50px;" maxlength="3" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                     <div class="invalid-feedback" id="invFormSubCli">
                                                                         Please enter Subsequent clicks score.
                                                                    </div>
													            </td>
													
												            </tr>
											            </tbody>
										            </table>
											    </div>
										    </div>
									    </div>
								    </div>
                                    <div class="tab-pane active" id="tabEmail">
									    <div class="form-horizontal">
										    <div class="container">
											    <div class="row">
												    <table class="table table-striped">
											
											    <tbody>
												    <tr>
													    <td><label class="col-form-label" >Message bounces</label></td>
													    <td>
                                                            <asp:TextBox ID="txtEmailMessageBounces" runat="server" class="form-control"  style="width:50px;" maxlength="3" onkeypress="return isNumberKey(event)" required></asp:TextBox>
                                                             <div class="invalid-feedback" id="invEmailMeBo">
                                                                  Please enter Message bounces score.
                                                             </div>
													    </td>
													
												    </tr>
												    <tr>
													    <td><label class="col-form-label" >Unsubscribe from email</label></td>
													    <td>
                                                            <asp:TextBox ID="txtEmailUnsubscribeEmail" runat="server" class="form-control"  style="width:50px;" maxlength="3" onkeypress="return isNumberKey(event)" required></asp:TextBox>
                                                            <div class="invalid-feedback" id="invEmailUnFrEm">
                                                                  Please enter Unsubscribe from email score.
                                                             </div>
													    </td>
													
												    </tr>
												    <tr>
													    <td><label class="col-form-label" >First click on a link in email</label></td>
													    <td>
                                                            <asp:TextBox ID="txtEmailFirstClick" runat="server" class="form-control"  style="width:50px;" maxlength="3" onkeypress="return isNumberKey(event)" required></asp:TextBox>
                                                            <div class="invalid-feedback" id="invEmailFiCliOnALiInEm">
                                                                  Please enter First click on a link in email score.
                                                             </div>
													    </td>
													
												    </tr>
												    <tr>
													    <td><label class="col-form-label" >Subsequent clicks</label></td>
													    <td>
                                                            <asp:TextBox ID="txtEmailSubsequentClicks" runat="server" class="form-control"  style="width:50px;" maxlength="3" onkeypress="return isNumberKey(event)" required></asp:TextBox>
                                                            <div class="invalid-feedback" id="invEmailSubCli">
                                                                  Please enter Subsequent clicks score.
                                                             </div>
													    </td>
												    </tr>
											    </tbody>
										    </table>
											    </div>
										    </div>
									    </div>
								    </div>
                                    <div class="tab-pane" id="tabSMS" style="display:none;">
									    <div class="form-horizontal">
										    <div class="container">
											    <div class="row">
												    <table class="table table-striped">
											
											    <tbody>
												    <tr>
													    <td><label class="col-form-label" >Message bounces</label></td>
													    <td>
                                                            <asp:TextBox ID="txtSMSMessageBounces" runat="server" class="form-control"  style="width:50px;" maxlength="3" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                            <div class="invalid-feedback" id="invSMSMeBo">
                                                                  Please enter Message bounces score.
                                                             </div>
													    </td>
													
												    </tr>
												    <tr>
													    <td><label class="col-form-label" >Unsubscribe after SMS</label></td>
													    <td>
                                                            <asp:TextBox ID="txtSMSUnsubscribeAfterSMS" runat="server" class="form-control"  style="width:50px;" maxlength="3" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                            <div class="invalid-feedback" id="invSMSUnAfSMS">
                                                                  Please enter Unsubscribe after SMS score.
                                                             </div>
													    </td>
													
												    </tr>
											    </tbody>
										    </table>
											    </div>
										    </div>
									    </div>
								    </div>
                                 <div class="form-horizontal">
                                    <div class="container">
                                        <div class="row">
												    <div class="form-group form-group-sm col-sm-12">
													    <div class="row">
													    <div class="col-sm-12 button_panel">
														    <div class="float-right mt-2">
                                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" OnClientClick="return Validate();"  />
															    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-light" OnClick="btnReset_Click" formnovalidate />
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
					</div>
				</div>
				<div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 rightmenu">
                     <div class="container-fluid">
					    <div class="graphite accordion-container-right">
						    <ul class="accordion" id="accordion-right">
							    <li class="dcjq-current-parent"><a href="#"  class="dcjq-parent active">Shortcuts</a>
								    <ul>
									    <li>
                                            <asp:LinkButton ID="lnkSaveScore" runat="server" Text="Save Score" OnClick="lnkSaveScore_Click" OnClientClick="return Validate();"></asp:LinkButton>
									    </li>
								    </ul>
                                     <ul>
									    <li>
										    <a href="javascript:void(0);CallPrint('dvTable');">Print Score</a>
									    </li>
								    </ul>
							    </li>
							    </ul>
					    </div>
				    </div>	
				</div>
</asp:Content>


