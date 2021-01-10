<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pager.ascx.cs" Inherits="MarketingAutomationTool.UserControls.Pager" %>
<div class="col-sm-3" style="padding-left:0px;" >
													<div class="form-inline">
														<div class="form-group">
															<label class="col-form-label">Show Rows</label>&nbsp;&nbsp;
                                                            <asp:DropDownList ID="ddlMaxRows" runat="server" class="form-control" style="width:60px;">
                                                                <asp:ListItem>5</asp:ListItem>
                                                                <asp:ListItem>10</asp:ListItem>
                                                                <asp:ListItem>15</asp:ListItem>
                                                            </asp:DropDownList>&nbsp;&nbsp;
                                                            <asp:Button ID="btnGo1" runat="server" Text="Go" class="btn btn-default" OnClick="btnGo_Click" />&nbsp;&nbsp;
															
														</div>
													</div>
												</div>
												<div class="col-sm-6" style="padding-left:50px;" >
													<div class="form-inline">
														<div class="form-group">
                                                        <asp:Button ID="btnFirst" runat="server" Text="<<" class="btn btn-default" OnClick="btnFirst_Click" />&nbsp;&nbsp;
														<asp:Button ID="btnPrev" runat="server" Text="<" class="btn btn-default" OnClick="btnPrev_Click" />&nbsp;&nbsp;
														<label class="col-form-label">Page:</label>&nbsp;&nbsp;
                                                        <asp:TextBox ID="txtPageNum" runat="server" class="form-control" style="width:50px;" onkeypress="return isNumberKey(event)" ></asp:TextBox>&nbsp;&nbsp;
														<label class="col-form-label">of</label>&nbsp;&nbsp;
														<label class="col-form-label"  id="lblTotalPages"><%= MaxPages%></label>&nbsp;&nbsp;
														<asp:Button ID="btnNext" runat="server" Text=">" class="btn btn-default" OnClick="btnNext_Click" />&nbsp;&nbsp;
														<asp:Button ID="btnLast" runat="server" Text=">>" class="btn btn-default" OnClick="btnLast_Click" />&nbsp;&nbsp;
														<asp:Button ID="btnGo" runat="server" Text="Go" class="btn btn-default" OnClick="btnGo_Click" />&nbsp;&nbsp;
													</div>
													</div>
												</div>
												<div class="col-sm-3 text-right" style="padding-right:0px;">
													<div class="form-group">
														<label class="col-form-label">Item</label>&nbsp;
														<label class="col-form-label"  id="lblMin"><%= MinItem%></label>&nbsp;
														<label class="col-form-label">of</label>&nbsp;
														<label class="col-form-label" id="lblMax"><%= MaxItem%></label>&nbsp;
														<label class="col-form-label">of</label>&nbsp;
														<label class="col-form-label" ><%= TotalRows%></label>&nbsp;
													</div>
												</div>