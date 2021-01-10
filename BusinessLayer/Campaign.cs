using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Campaign
    {
        public bool CreateCampaign(Guid AccountId,
                   Guid? in_CampaignUID,
                   int? in_MessageID,
                   string CampaignName,
                   int CampaignType,
                   int CampaignFormat,
                   string CampaignDescription,
                   bool UseBounceAddressInFromField,
                   bool HideInSearch,
                   int CampaignStatus,
                   int? TemplateId,
                   int SendingOption,
                   DateTime? SendingSchedule,
                   string Entity,
                   string Subject,
                   string SenderName,
                   string SenderEmail,
                   string MessageBody,
                   string SearchId,
                   string ExcludeList,
                   Guid CreatedBy,
                   string IPAddress,
                   int EmailJobStatus,
                   out int out_CampaignId,
                   out Guid? out_CampaignUID,
                   out int out_MessageId,
                   out Guid? out_MessageUID)
        {
            DataAccessLayer.Controller.Campaign cCampaign = new DataAccessLayer.Controller.Campaign();
            return cCampaign.CreateCampaign(AccountId, in_CampaignUID, in_MessageID, CampaignName, CampaignType, CampaignFormat, CampaignDescription, UseBounceAddressInFromField, HideInSearch, CampaignStatus, TemplateId, SendingOption, SendingSchedule, Entity, Subject, SenderName, SenderEmail, MessageBody, SearchId, ExcludeList, CreatedBy, IPAddress, EmailJobStatus, out out_CampaignId, out out_CampaignUID, out out_MessageId, out out_MessageUID);
        }

        public DataTable GetCampaignsForSideBar(Guid AccountID, string SearchText, int? CampaignStatus, bool? isDeleted, bool? isHiddenFromSearch)
        {
            DataAccessLayer.Controller.Campaign cCampaign = new DataAccessLayer.Controller.Campaign();
            return cCampaign.GetCampaignsForSideBar(AccountID, SearchText,CampaignStatus,isDeleted, isHiddenFromSearch);
        }


        public bool DeleteCampaign(Guid AccountID,
        Guid CampaignUID,
        Guid DeletedBy)
        {
            DataAccessLayer.Controller.Campaign cCampaign = new DataAccessLayer.Controller.Campaign();
            return cCampaign.DeleteCampaign(AccountID, CampaignUID, DeletedBy);
        }

        public DataTable GetAllCampaigns(Guid AccountID, bool? Deleted, bool? HideInSearch, Guid CreatedBy, int MaxRows, string SortBy, string SortDirection, int PageNum, out int NewPageNum, out int MinItem, out int MaxItem, out int TotalRows, out decimal MaxPages)
        {
            DataAccessLayer.Controller.Campaign cCampaign = new DataAccessLayer.Controller.Campaign();
            return cCampaign.GetAllCampaigns(AccountID, Deleted, HideInSearch, CreatedBy, MaxRows, SortBy, SortDirection, PageNum, out NewPageNum, out MinItem, out MaxItem, out TotalRows, out MaxPages);
        }

        public DataTable GetCampaignStats(Guid AccountID)
        {
            DataAccessLayer.Controller.Campaign cCampaign = new DataAccessLayer.Controller.Campaign();
            return cCampaign.GetCampaignStats(AccountID);
        }

        public DataTable GetCampaignUniqueStats(Guid AccountID)
        {
            DataAccessLayer.Controller.Campaign cCampaign = new DataAccessLayer.Controller.Campaign();
            return cCampaign.GetCampaignUniqueStats(AccountID);
        }

        public DataTable GetCampaignReportGetAllSubmittedCampaigns(Guid AccountID)
        {
            DataAccessLayer.Controller.Campaign cCampaign = new DataAccessLayer.Controller.Campaign();
            return cCampaign.GetCampaignReportGetAllSubmittedCampaigns(AccountID);
        }
        public DataTable GetCampaignReportEmailStats(Guid AccountID, DateTime? StartDate, DateTime? EndDate, int? CampaignId)
        {
            DataAccessLayer.Controller.Campaign cCampaign = new DataAccessLayer.Controller.Campaign();
            return cCampaign.GetCampaignReportEmailStats(AccountID, StartDate, EndDate, CampaignId);
        }

        public DataTable GetCampaignReportEmailsSentListing(Guid AccountID, DateTime? StartDate, DateTime? EndDate, int? CampaignId)
        {
            DataAccessLayer.Controller.Campaign cCampaign = new DataAccessLayer.Controller.Campaign();
            return cCampaign.GetCampaignReportEmailsSentListing(AccountID, StartDate, EndDate, CampaignId);
        }

        public DataTable GetCampaignReportTotalClicksListing(Guid AccountID, DateTime? StartDate, DateTime? EndDate, int? CampaignId)
        {
            DataAccessLayer.Controller.Campaign cCampaign = new DataAccessLayer.Controller.Campaign();
            return cCampaign.GetCampaignReportTotalClicksListing(AccountID, StartDate, EndDate, CampaignId);
        }

        public DataTable GetCampaignReportUniqueClicksListing(Guid AccountID, DateTime? StartDate, DateTime? EndDate, int? CampaignId)
        {
            DataAccessLayer.Controller.Campaign cCampaign = new DataAccessLayer.Controller.Campaign();
            return cCampaign.GetCampaignReportUniqueClicksListing(AccountID, StartDate, EndDate, CampaignId);
        }

        public DataTable GetCampaignReportTotalOpensListing(Guid AccountID, DateTime? StartDate, DateTime? EndDate, int? CampaignId)
        {
            DataAccessLayer.Controller.Campaign cCampaign = new DataAccessLayer.Controller.Campaign();
            return cCampaign.GetCampaignReportTotalOpensListing(AccountID, StartDate, EndDate, CampaignId);
        }

        public DataTable GetCampaignReportUniqueOpensListing(Guid AccountID, DateTime? StartDate, DateTime? EndDate, int? CampaignId)
        {
            DataAccessLayer.Controller.Campaign cCampaign = new DataAccessLayer.Controller.Campaign();
            return cCampaign.GetCampaignReportUniqueOpensListing(AccountID, StartDate, EndDate, CampaignId);
        }
    }
}
