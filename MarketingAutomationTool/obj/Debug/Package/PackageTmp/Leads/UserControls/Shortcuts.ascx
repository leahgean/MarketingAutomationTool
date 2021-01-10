<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Shortcuts.ascx.cs" Inherits="MarketingAutomationTool.Leads.UserControls.Shortcuts" %>
<li class="<%= liShortcutsClass %>"><a href="#"  class="<%=aShortcutsClass %>">Shortcuts</a>
								<ul>
									<li>
                                        <asp:LinkButton ID="lnkRunSearch" runat="server" Text="Run Search" OnClick="lnkRunSearch_Click"></asp:LinkButton>
									</li>
									<li>
                                        <asp:LinkButton ID="lnkAddNewLead" runat="server" Text="Add New Lead" OnClick="lnkAddNewLead_Click"></asp:LinkButton>
									</li>
								</ul>
							</li>