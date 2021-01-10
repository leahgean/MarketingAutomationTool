<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotUserName.aspx.cs" Inherits="MarketingAutomationTool.Public.ForgotUserName" %>

<!doctype html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="/icons/widget1.png">

    <title>Marketing Automation Tool - Forgot UserName</title>

    <!-- Bootstrap CSS -->
    <link href="/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
	

    <!-- Custom styles -->
	<link rel="stylesheet" href="/styles/login.css"/>

    <!-- Scripts -->
    <script src="/jQuery/jquery-3.3.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">

           function validateEmail(email) {
              var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
              return re.test(email);
           }

           function ValidateEmailAddress() {
                    if (validateEmail($('#<%=txtEmailAddress.ClientID%>').val()))
                    {
                         return true;
                    }
                    else {
                        $("#invalidEmail").text('Please enter a valid Email Address. e.g. example@mail.com');
                        $("#invalidEmail").show();
                        $("#<%=txtEmailAddress.ClientID%>").css({"border": "1px solid #dc3545", "box-shadow": "0 0 0 .2rem rgba(220,53,69,.25)"});
                        return false;
                    }
           }

          function ValidateRequiredFields() {          
              if (document.getElementById('<%=txtEmailAddress.ClientID%>').value == '') {
                  return false;
              }
              else {
                  return ValidateEmailAddress();
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
					         <h5 class="center-block text-center" >Retrieve UserName</h5>
					         <br/>
					          <label for="inputEmail" class="sr-only">Email:</label>
                              <asp:TextBox ID="txtEmailAddress"  CssClass="form-control" placeholder="Please enter Email Address"  runat="server" MaxLength="250" required autofocus></asp:TextBox>
                              <div id="invalidEmail" class="invalid-feedback">
                                   Please enter Email Address.
                              </div>
				        </div>
				        <div class="form-horizontal forgot_button_panel">
						    <div class="container">
							    <div class="row">
								    <div class="form-group form-group-sm col-sm-12 forgot_button_panel_col">
									    <div class="float-right mt-2">
												    <asp:LinkButton ID="lnkBack"  CssClass="btn btn-light" Text="Back" runat="server" OnClick="lnkBack_Click" formnovalidate></asp:LinkButton>&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnkRetrieve" CssClass="btn btn-primary" Text="Retrieve UserName" runat="server" OnClick="lnkRetrieve_Click" OnClientClick="return Validate();"></asp:LinkButton>
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

