<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeadLists.ascx.cs" Inherits="MarketingAutomationTool.Leads.UserControls.LeadLists" %>
<li class="<%= liLeadListsClass %>"><a href="#"  class="<%= aLeadListsClass %>">List</a>
								<ul>
									<li>
										<asp:LinkButton ID="lnkCreateNewList" runat="server" Text="Create New List" OnClick="lnkCreateNewList_Click"></asp:LinkButton>
									</li>
									<li>
										<asp:LinkButton ID="lnkBrowseList" runat="server" Text="Browse List" OnClick="lnkBrowseList_Click"></asp:LinkButton>
									</li>
                                    <li runat="server" id="liAddMember">
                                        <asp:LinkButton ID="lnkAddMember" runat="server" Text="Add Member" OnClick="lnkAddMember_Click"  ></asp:LinkButton>
									</li>
								</ul>
							</li>