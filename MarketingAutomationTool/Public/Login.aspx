<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MarketingAutomationTool.Public.Login" %>

<!doctype html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="../icons/widget1.png"/>

    <title>Marketing Automation Tool - Login</title>

    <!-- Bootstrap CSS -->
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
    <!-- Custom styles -->
	<link rel="stylesheet" href="../Styles/login.css" />
   
	<script src="../jQuery/jquery-3.3.1.min.js" type="text/javascript"></script>
      <script type="text/javascript">
        
          function ValidateRequiredFields() {          
              if (document.getElementById('<%=txtLoginName.ClientID%>').value == '') {
                  return false;
              }

              if (document.getElementById('<%=txtPassword.ClientID%>').value == '') {
                  return false;
              }

              return true;
        }

        

        function Validate() {
            /*bootstrap validation: shows the error messages for required fields; already prevents form submission of a required field is blank*/
            var forms = document.getElementById('dvFormValidate');            
            forms.classList.add('was-validated');

            var formIsValid = ValidateRequiredFields();
            
            return formIsValid;
        }
    </script>
  </head>

  <body>
    <form runat="server" >
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
				      <div class="form-signin">
                          <div id="dvFormValidate" class="needs-validation" novalidate>
					         <h5 class="center-block text-center" >Login</h5>
					         <br/>
					              <label for="txtLoginName" class="sr-only" >Username</label>
                                  <asp:TextBox ID="txtLoginName" runat="server" class="form-control" placeholder="Please enter UserName"  required ></asp:TextBox>
                                  <div class="invalid-feedback" id="dvUserName">
                                       Please enter UserName.
                                  </div>
					              <label for="txtPassword" class="sr-only" >Password</label>
                                  <asp:TextBox ID="txtPassword" runat="server" class="form-control" placeholder="Please enter Password"  TextMode="Password" type="password" required></asp:TextBox>
                                  <div class="invalid-feedback" id="dvPassword">
                                       Please enter Password.
                                  </div>
					           <br/>
					          <div class="center-block text-center">
                                <asp:LinkButton ID="lnkForgotUserName" runat="server"  Text="Forgot Username?" OnClick="lnkForgotUserName_Click"></asp:LinkButton>&nbsp;|&nbsp;<asp:LinkButton ID="lnkForgotPassword" runat="server"  Text="Forgot Password?" OnClick="lnkForgotPassword_Click"></asp:LinkButton>
					          </div>
					          <br/>
                              <asp:LinkButton ID="btnSignIn" runat="server" Text="Sign in" class="btn btn-lg btn-primary btn-block" OnClick="btnSignIn_Click" OnClientClick="return Validate();"></asp:LinkButton>
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
