<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Lead.ascx.cs" Inherits="MarketingAutomationTool.Leads.UserControls.Lead" %>
<div class="col-lg-8 divsubheadershortcut">
			
			<asp:LinkButton ID="lnkNewLead" runat="server" class="subheadershortcut" Text="New Lead" OnClick="lnkNewLead_Click" ></asp:LinkButton>
			&nbsp;|&nbsp;
            <asp:LinkButton ID="lnkSearch" runat="server"  Text="Search" class="subheadershortcut" OnClick="lnkSearch_Click"></asp:LinkButton>
			&nbsp;|&nbsp;
			<div class="dropdowngroup">
			<a href="#" class="dropdown-toggle" data-toggle="dropdown">Import</a>
			<div class="dropdown-menu">
                <asp:LinkButton ID="lnkImportLeads" runat="server" class="dropdown-item" Text="Excel" OnClick="lnkImportLeads_Click"></asp:LinkButton>
				<asp:LinkButton ID="lnkFacebook" runat="server" class="dropdown-item" Text="Facebook" OnClick="lnkFacebook_Click" Visible="false"></asp:LinkButton>
                <asp:LinkButton ID="lnkViewExistingImportJob" runat="server" class="dropdown-item" Text="Existing Import" OnClick="lnkViewExistingImportJob_Click"   ></asp:LinkButton>
			</div>
			</div>
			&nbsp;|&nbsp;
			<asp:LinkButton ID="lnkExport" runat="server" class="subheadershortcut" Text="Export" OnClick="lnkExport_Click"></asp:LinkButton>
			
			</div>
			<div class="col-lg-4 text-right divsubheadershortcut">
			<a href="#" class="dropdown-toggle" data-toggle="dropdown">Lead Reports</a>
			<div class="dropdown-menu">
                <asp:LinkButton ID="lnkNewLeadsStatistics" runat="server" class="dropdown-item" Text="Leads Statistics" OnClick="lnkNewLeadsStatistics_Click"   ></asp:LinkButton>
				<asp:LinkButton ID="lnkLeadsListing" runat="server" class="dropdown-item" Text="Leads Listing" OnClick="lnkLeadsListing_Click"    ></asp:LinkButton>
                <asp:LinkButton ID="lnkDeletedLeads" runat="server" class="dropdown-item" Text="Deleted Leads Listing" OnClick="lnkDeletedLeads_Click"></asp:LinkButton>
                <asp:LinkButton ID="lnkUnsubscribedLeads" runat="server" class="dropdown-item" Text="Unsubscribed Leads Listing" OnClick="lnkUnsubscribedLeads_Click"></asp:LinkButton>
                <asp:LinkButton ID="lnkDuplicateLeads" runat="server" class="dropdown-item" Text="Duplicate Leads Listing" OnClick="lnkDuplicateLeads_Click"></asp:LinkButton>
				<div class="dropdown-divider"></div>
                <asp:LinkButton ID="lnkViewAllReports" runat="server" class="dropdown-item" Text="View All Reports" OnClick="lnkViewAllReports_Click"    ></asp:LinkButton>
			</div>
			</div>