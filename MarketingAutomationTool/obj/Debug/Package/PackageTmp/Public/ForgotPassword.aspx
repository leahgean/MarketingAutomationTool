<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="MarketingAutomationTool.Public.ForgotPassword" %>

<html lang="en">
  <head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="/icons/widget1.png">

    <title>Marketing Automation Tool - Forgot Password</title>

    <!-- Bootstrap CSS -->
    <link href="/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
	
    <!-- Custom styles -->
	<link rel="stylesheet" href="/styles/login.css"/>

    <!-- Scripts -->
      <script type="text/javascript">
          function ValidateRequiredFields() {
              if (document.getElementById('<%=txtUserName.ClientID%>').value == '') {
                  return false;
              }
              else {
                  return true;
              }
          }

            function Validate() {
                /*bootstrap validation: shows the error messages for required fields; already prevents form submission of a required field is blank*/
                var forms = document.getElementById('dvFormValidate');            
                forms.classList.add('was-validated');

                return ValidateRequiredFields();
            }
    </script>
  </head>

  <body>
      <form id="form1" runat="server">
	<div class="container-fluid">
		<div class="row topline">
			<div class="col-lg-12">
			</div>
		</div>
		<div class="row topspace">
			<div class="col-lg-12">
			</div>
		</div>
		<div class="row">
			<div class="col-lg-3">
				&nbsp;
			</div>
			<div class="col-lg-6">
                <div runat="server" id="dvAlert" role="alert">
                </div>
				<div class="loginheader">
					<div class="center-block text-center">
						<img class="mb-4" src="/images/marketingautomationtool4.png" alt="" />
					</div>
				</div>
				<div class="logincontent">
                     <div class="needs-validation" id="dvFormValidate" novalidate>
				  <div class="form-signin">
					 <h5 class="center-block text-center" >Retrieve Password</h5>
					 <br/>
					  <label for="inputEmail" class="sr-only">UserName:</label>
                      <asp:TextBox ID="txtUserName"  CssClass="form-control" placeholder="Please enter UserName" MaxLength="50" runat="server" required autofocus ></asp:TextBox>
                      <div class="invalid-feedback">
                           Please enter UserName.
                      </div>
				  </div>
				  <div class="form-horizontal forgot_button_panel">
						<div class="container">
							<div class="row">
								<div class="form-group form-group-sm col-sm-12 forgot_button_panel_col">
									<div class="float-right mt-2">
                                                <asp:LinkButton ID="lnkBack"  CssClass="btn btn-light" Text="Back" runat="server" OnClick="lnkBack_Click" formnovalidate></asp:LinkButton>&nbsp;&nbsp;
                                                <asp:LinkButton ID="lnkRetrieve" CssClass="btn btn-primary" Text="Retrieve Password" runat="server" OnClick="lnkRetrieve_Click" OnClientClick="return Validate();"></asp:LinkButton>
									</div>
								</div>
							</div>
						</div>
					</div>
                    </div>
				</div>
			</div>
			<div class="col-lg-3">
				&nbsp;
			</div>
		</div>
		<div class="row bottomline">
			<div class="col-lg-12">
			</div>
		</div>
		
	</div>
	<footer class="footer">
		<div class="container-fluid">
			<span class="text-muted">Marketing Automation Tool © 2018-2021</span>
		</div>
	</footer>
          </form>
  </body>
</html>

