<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftSearch.ascx.cs" Inherits="MarketingAutomationTool.Leads.UserControls.LeftSearch" %>
 <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 leftmenu">
					<div class="container-fluid">
					<div class="searchcontainer">
						<div class="searchbar">
							<label class="col-form-label searchlabel" >Search:</label>
							<asp:TextBox ID="txtSearchKey" runat="server" CssClass="form-control searchkeytext" placeholder="Search Key"></asp:TextBox>
                            <div class="col-lg-12 searchoptionsbar">
									<div class="row">
										<div class="col-lg-6 searchoptionsdiv">
											<a href="#" id="lnkFilter" class="searchoptionslink" >Search Options</a>
										</div>
										<div class="col-lg-6 searchoptionsbuttondiv">
											<asp:Button ID="btnSearch" runat="server" Text="Search"  Cssclass="btn btn-light searchoptionsbutton" OnClick="btnSearch_Click" formnovalidate/>
										</div>
									</div>
							</div>
						</div>
						<div class="collapse hiddenpanel" id="collapsePanel">
							<div class="row">
								<div class="col-lg-12 contactstatusdiv">
                                    <asp:DropDownList Cssclass="form-control contactstatus" ID="ddlStatus" runat="server">
                                        <asp:ListItem Value="">All</asp:ListItem>
                                        <asp:ListItem Value="1">Not Confirmed</asp:ListItem>
                                        <asp:ListItem Value="2">Confirmed</asp:ListItem>
                                        <asp:ListItem Value="3">Active</asp:ListItem>
                                        <asp:ListItem Value="4">Inactive</asp:ListItem>

                                    </asp:DropDownList>
								</div>
							</div>
							<div class="row">
								<div class="col-lg-12 contactstatusdiv">
									<div class="form-check">
									  <asp:CheckBox ID="chkDeletedOnly" runat="server" CssClass="form-check-input searchdeletedleadscb" />
									  <label class="form-check-label searchdeletedleads" for="defaultCheck1">
										Search deleted leads only
									  </label>
									</div>
								</div>
							</div>
						</div>
						<asp:ListView ID="rvContact" runat="server" ItemPlaceholderID="liContacts" OnItemDataBound="rvContact_ItemDataBound" EnableViewState="false" OnPagePropertiesChanging="rvContact_PagePropertiesChanging" OnItemCommand="rvContact_ItemCommand" OnItemDeleting="rvContact_ItemDeleting">
                            <LayoutTemplate>
		                        <div class="sidescroller">
			                        <ul class="contacts-list">
				                        <li runat="server" id="liContacts"></li>
			                        </ul>
		                        </div>
                            </LayoutTemplate>
	                        <ItemTemplate>
	                        <li>
		                        <div class="contactdetails-hdr">
			                        <span class="contactname">
				                        <a><%# Eval("LastName") %>, <%# Eval("FirstName") %></a>
			                        </span>
			                        <small><span class="badge badge-secondary" ><%# Eval("ContactID") %></span><%# Eval("EmailAddress") %></small>
		                        </div>
		                        <div class="contactdetail-icons">
			                        <div class="contactdetail-options">
				                        <a href="/Leads/EditLead.aspx?Guid=<%# Eval("ContactGUID") %>" rel="tooltip" class="sideMenu" data-placement="top" title="Edit Lead"><i class="fas fa-edit"></i>
				                        </a>
                                        <asp:PlaceHolder ID="plcDelete" runat="server">
					                        <asp:LinkButton ID="lnkDelete" runat="server" rel="tooltip" class="sideMenu" data-placement="top"  title="Delete Lead" CommandArgument='<%# Eval("ContactGUID") %>' CommandName="DELETE" OnClientClick="return ConfirmDelete();" ><i class="fas fa-trash"></i></asp:LinkButton>
                                            <a href="#" runat="server" id="lnkNoDeleteLC" rel="tooltip" class="sideMenu" data-placement="top" title="Deleted Lead" onclick="return false;"><i class="fas fa-trash-alt" id="trashicondisabled" style="color:#CCC;"></i></a>
                                            <asp:HiddenField ID="hdnDeletedLC" runat="server" Value='<%# Eval("IsDeleted") %>' />  
				                        </asp:PlaceHolder>
			                        <div class="clear"></div>
			                        </div>
		                        </div>
	                        </li>					
	                        </ItemTemplate>
                        </asp:ListView>
                        <br/>
                         <div>
                                    <asp:DataPager ID="dpContact" runat="server" PagedControlID="rvContact" >
                                         <Fields>
                                             <asp:NumericPagerField ButtonCount="5"  />
                                         </Fields>
                                     </asp:DataPager>
                                </div>
						</div>
					</div>
				</div>
