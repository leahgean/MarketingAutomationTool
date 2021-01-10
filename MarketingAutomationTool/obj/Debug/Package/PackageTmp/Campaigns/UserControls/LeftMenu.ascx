<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenu.ascx.cs" Inherits="MarketingAutomationTool.Campaigns.UserControls.LeftMenu" %>
<div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 leftmenu">
					<div class="container-fluid">
					<div class="searchcontainer">
						<div class="searchbar">
							<label class="col-form-label searchlabel" >Search:</label>
                            <asp:TextBox ID="txtSearchKey" class="form-control searchkeytext" runat="server" placeholder="Search Key" MaxLength="250"></asp:TextBox>
                            <div class="col-lg-12 searchoptionsbar">
									<div class="row">
										<div class="col-lg-6 searchoptionsdiv">
											<a href="#" id="lnkFilter" class="searchoptionslink">Search Options</a>
										</div>
										<div class="col-lg-6 searchoptionsbuttondiv">
											<asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-light searchoptionsbutton" OnClick="btnSearch_Click" formnovalidate></asp:Button>
										</div>
									</div>
							</div>
						</div>
						<div class="collapse hiddenpanel show" id="collapsePanel">
                            <div class="row">
								<div class="col-lg-12 contactstatusdiv">
                                    <asp:DropDownList Cssclass="form-control contactstatus" ID="ddlStatus" runat="server">
                                        <asp:ListItem Value="">All</asp:ListItem>
                                        <asp:ListItem Value="0">Draft</asp:ListItem>
                                        <asp:ListItem Value="1">Submitted</asp:ListItem>
                                    </asp:DropDownList>
								</div>
							</div>
							<div class="row">
								<div class="col-lg-12 contactstatusdiv">
									<div class="form-check">
									  <asp:CheckBox ID="chkDeletedCampaigns" runat="server" class="form-check-input searchdeletedleadscb"  />
									  <label class="form-check-label searchdeletedleads" for="chkDeletedCampaigns">
										Search deleted campaigns only
									  </label>
									</div>
								</div>
							</div>
							<div class="row">
								<div class="col-lg-12 contactstatusdiv">
									<div class="form-check">
                                      <asp:CheckBox ID="chkIncludeHidden" runat="server"  class="form-check-input searchdeletedleadscb" />
									  <label class="form-check-label searchdeletedleads" for="chkIncludeHidden">
										Include hidden campaigns
									  </label>
									</div>
								</div>
							</div>
						</div>
                        <asp:ListView ID="rvCampaign" runat="server" ItemPlaceholderID="liCampaigns" EnableViewState="false" OnItemCommand="rvCampaign_ItemCommand" OnItemDataBound="rvCampaign_ItemDataBound" OnItemDeleting="rvCampaign_ItemDeleting" OnPagePropertiesChanging="rvCampaign_PagePropertiesChanging" >
                            <LayoutTemplate>
		                        <div class="sidescroller">
			                        <ul class="contacts-list">
				                        <li runat="server" id="liCampaigns"></li>
			                        </ul>
		                        </div>
                            </LayoutTemplate>
	                        <ItemTemplate>
	                        <li>
		                        <div class="contactdetails-hdr">
			                        <span class="contactname">
				                        <a><%# Eval("CampaignName") %></a>
			                        </span>
			                        <small><span class="badge badge-secondary" ><%# Eval("CreatedDate","{0:dd MMM yyyy}") %></span></small>
		                        </div>
		                        <div class="contactdetail-icons">
			                        <div class="contactdetail-options">
                                         <asp:LinkButton ID="lnkEdit" runat="server" rel="tooltip" class="sideMenu" data-placement="top"  title="Edit Campaign" CommandArgument='<%# Eval("CampaignUID") %>' CommandName="EDIT"  ><i class="fas fa-edit"></i></asp:LinkButton>
                                        <asp:PlaceHolder ID="plcDelete" runat="server">
					                        <asp:LinkButton ID="lnkDelete" runat="server" rel="tooltip" class="sideMenu" data-placement="top"  title="Delete Campaign" CommandArgument='<%# Eval("CampaignUID") %>' CommandName="DELETE" OnClientClick="return LeftSearchConfirmDelete();" ><i class="fas fa-trash"></i></asp:LinkButton>
                                            <a href="#" runat="server" id="lnkNoDeleteLC" rel="tooltip" class="sideMenu" data-placement="top" title="Deleted Campaign" onclick="return false;"><i class="fas fa-trash-alt" id="trashicondisabled" style="color:#CCC;"></i></a>
                                            <asp:HiddenField ID="hdnDeletedLC" runat="server" Value='<%# Eval("Deleted") %>' />  
				                        </asp:PlaceHolder>
                                        <asp:LinkButton ID="lnkCopy" runat="server" rel="tooltip" class="sideMenu" data-placement="top"  title="Copy Campaign" CommandArgument='<%# Eval("CampaignUID") %>' CommandName="COPY" OnClientClick="return LeftSearchConfirmCopy();" ><i class="fas fa-copy"></i></asp:LinkButton>
			                        <div class="clear"></div>
			                        </div>
		                        </div>
	                        </li>					
	                        </ItemTemplate>
                        </asp:ListView>
						<br/>
						<div>
                                    <asp:DataPager ID="dpCampaign" runat="server" PagedControlID="rvCampaign" >
                                         <Fields>
                                             <asp:NumericPagerField ButtonCount="5"  />
                                         </Fields>
                                     </asp:DataPager>
                                </div>
						</div>
					</div>
				</div>