<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenu.ascx.cs" Inherits="MarketingAutomationTool.UserControls.LeftMenu" %>
<div class="container-fluid">
    <div class="graphite accordion-container-left">
						<ul class="accordion" id="accordion-left">
							<li id="liMyAccount" class="<%= liAccountClass%>">
                                <a href="#" id="lnkMyAccount" class="<%= lnkAccountClass%>">My Account</a>
								<ul>
									<li>
										<a href="../MyAccount/MyAccount.aspx">Account Details</a>
									</li>
									<li>
										<a href="../MyAccount/DefaultScoring.aspx">Default Scoring</a>
									</li>
									<li>
										<a href="../MyAccount/LoginHistory.aspx">Login History</a>
									</li>
									<li style="display:none;"><a href="../MyAccount/FacebookLogin.aspx">Facebook Login</a>
									</li>
								</ul>
							</li>
                            <li runat="server" id="liManageAccounts" class="<%= liManageAccountClass%>"  >
								<a href="#" id="lnkManageAccounts" class="<%= lnkManageAccountClass%>">Manage Accounts</a>
								<ul>
									<li>
                                        <a href="../MyAccount/NewAccount.aspx?c=2">New Account</a>
									</li>
									<li>
										<a href="../MyAccount/Accounts.aspx?c=2">View Accounts</a>
									</li>	
								</ul>
							</li>
                             <li runat="server" id="liManageUsers" class="<%= liManageUsersClass%>">
								<a href="#" id="lnkManageUsers" class="<%= lnkManageUsersClass%>">Manage Users</a>
								<ul>
                                    <li><a href="../MyAccount/NewUser.aspx?c=3">New User</a>
									</li>
									<li>
										<a href="../MyAccount/Users.aspx?c=3">View Users</a>
									</li>	
								</ul>
							</li>
							<li runat="server" id="liCurrentUser" class="<%= liCurrentUserClass%>">
								<a href="#" id="lnkCurrentUser" class="<%= lnkCurrentUserClass%>">Current User</a>
								<ul>
									<li><a href="../MyAccount/UserDetails.aspx?c=4&userid=<%= LoggedUserID%>">User Details</a>
									</li>
									<li>
										<a href="../MyAccount/ChangePassword.aspx?c=4">Change Password</a>
									</li>	
								</ul>
							</li>
							</ul>
					</div>
				</div>