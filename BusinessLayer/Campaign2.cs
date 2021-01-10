using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Linq;
using DataModels;

namespace BusinessLayer
{
    public class Campaign2
    {
        public CampaignModel GetCampaign(Guid AccountId, Guid CampaignUID, Guid UserId)
        {
            DataAccessLayer.Controller.Campaign2 cCampaign = new DataAccessLayer.Controller.Campaign2();
            CampaignModel mCam= cCampaign.GetCampaign(AccountId, CampaignUID, UserId);
            return mCam;
        }

        public bool InsertEmailCampaignsSent(DataModels.EmailCampaignsSent pECS)
        {
            DataAccessLayer.Controller.Campaign2 cCampaign = new DataAccessLayer.Controller.Campaign2();
            bool blnResult = cCampaign.InsertEmailCampaignsSent(pECS);
            return blnResult;
        }

        public bool InsertUnsubscribe(DataModels.CampaignUnsubscribes pCU)
        {
            DataAccessLayer.Controller.Campaign2 cCampaign = new DataAccessLayer.Controller.Campaign2();
            bool blnResult = cCampaign.InsertUnsubscribe(pCU);
            return blnResult;
        }


        public bool InsertClickthroughs(DataModels.CampaignClickthroughs pCC)
        {
            DataAccessLayer.Controller.Campaign2 cCampaign = new DataAccessLayer.Controller.Campaign2();
            bool blnResult = cCampaign.InsertClickthroughs(pCC);
            return blnResult;
        }

        public bool InsertEmailsOpened(DataModels.CampaignOpened pCO)
        {
            DataAccessLayer.Controller.Campaign2 cCampaign = new DataAccessLayer.Controller.Campaign2();
            bool blnResult = cCampaign.InsertEmailsOpened(pCO);
            return blnResult;
        }

        public int? SelectCampaignId(Guid AccountId, Guid CampaignId, Guid UserId)
        {
            DataAccessLayer.Controller.Campaign2 cCampaign = new DataAccessLayer.Controller.Campaign2();
            return cCampaign.SelectCampaignId(AccountId, CampaignId, UserId);
        }


        public int? SelectMessageId(Guid AccountId, Guid CampaignId, Guid UserId)
        {
            DataAccessLayer.Controller.Campaign2 cCampaign = new DataAccessLayer.Controller.Campaign2();
            return cCampaign.SelectMessageId(AccountId,  CampaignId, UserId);
        }

        public string GetCampaignName(int CampaignId, Guid AccountId, Guid UserId)
        {
            DataAccessLayer.Controller.Campaign2 cCampaign = new DataAccessLayer.Controller.Campaign2();
            return cCampaign.GetCampaignName(CampaignId, AccountId, UserId);
        }
    }
}
