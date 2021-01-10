<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMenu.ascx.cs" Inherits="MarketingAutomationTool.Campaigns.UserControls.TopMenu" %>
<div class="col-lg-8 divsubheadershortcut">
			
			<div class="dropdowngroup">
			<a href="#" class="dropdown-toggle" data-toggle="dropdown">Email</a>
			<div class="dropdown-menu">
                <asp:LinkButton ID="lnkCreateNew" class="dropdown-item" runat="server" Text="Create New" OnClick="lnkCreateNew_Click"></asp:LinkButton>
				<asp:LinkButton ID="lnkBrowse" class="dropdown-item" runat="server" Text="Browse" OnClick="lnkBrowse_Click"></asp:LinkButton>
			</div>
			</div>
			
			
			</div>
			<div class="col-lg-4 text-right divsubheadershortcut">
			<a href="#" class="dropdown-toggle" data-toggle="dropdown">Campaign Reports</a>
			<div class="dropdown-menu">
                 <asp:LinkButton ID="lnkStatsByDate" class="dropdown-item" runat="server" Text="Email Campaign Statistics" OnClick="lnkStatsByDate_Click"></asp:LinkButton>
                 <asp:LinkButton ID="lnkEmailsSentListing" class="dropdown-item" runat="server" Text="Emails Sent Listing" OnClick="lnkEmailsSentListing_Click" ></asp:LinkButton>
                 <asp:LinkButton ID="lnkTotalOpensListing" class="dropdown-item" runat="server" Text="Total Opens Listing" OnClick="lnkTotalOpensListing_Click" ></asp:LinkButton>
				 <asp:LinkButton ID="lnkUniqueOpensListing" class="dropdown-item" runat="server" Text="Unique Opens Listing" OnClick="lnkUniqueOpensListing_Click" ></asp:LinkButton> 
                <asp:LinkButton ID="lnkTotalClicksListing" class="dropdown-item" runat="server" Text="Total Clickthroughs Listing" OnClick="lnkTotalClicksListing_Click" ></asp:LinkButton>
				 <asp:LinkButton ID="lnkUnqiueClicksListing" class="dropdown-item" runat="server" Text="Unique Clickthroughs Listing" OnClick="lnkUnqiueClicksListing_Click" ></asp:LinkButton> 

                <div class="dropdown-divider"></div>
                    <asp:LinkButton ID="lnkViewAllReports" class="dropdown-item" runat="server" Text="View All Reports" OnClick="lnkViewAllReports_Click"></asp:LinkButton>
			    </div>
			</div>