using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class DefaultScore
    {
        public DataModels.DefaultScoreModel GetDefaultScore(Guid AccountId)
        {
            DataAccessLayer.Controller.DefaultScore def = new DataAccessLayer.Controller.DefaultScore();
            DataModels.DefaultScoreModel defModel = def.GetDefaultScore(AccountId);
            return defModel;
        }

        public bool InsertDefaultScore(Guid AccountId, short Form_Submit, short Form_Confirmation_Email_Bounce, short Form_Unsubscribe_From_Confirmation_Email,  short Form_Acknowledgment_Email_Bounce, short Form_Acknowledgment_Email_First_Open, short Form_Subsequent_Open, short Form_Unsubscribe_From_Acknowledgment_Email, short Form_Click_Link_On_Acknowledgment_Email, short Form_Subsequent_click, short Email_Bounce, short Email_Unsubscribe, short Email_Click_Link_First, short Email_Subsequent_Click, short SMS_Bounce, short SMS_Unsubscribe)
        {
            DataAccessLayer.Controller.DefaultScore def = new DataAccessLayer.Controller.DefaultScore();
            bool result= def.AddDefaultScore(AccountId, Form_Submit, Form_Confirmation_Email_Bounce, Form_Unsubscribe_From_Confirmation_Email, Form_Acknowledgment_Email_Bounce, Form_Acknowledgment_Email_First_Open, Form_Subsequent_Open, Form_Unsubscribe_From_Acknowledgment_Email, Form_Click_Link_On_Acknowledgment_Email, Form_Subsequent_click, Email_Bounce, Email_Unsubscribe, Email_Click_Link_First, Email_Subsequent_Click, SMS_Bounce, SMS_Unsubscribe);
            return result;
        }


        public bool UpdateDefaultScore(int Id, Guid AccountId, short Form_Submit, short Form_Confirmation_Email_Bounce, short Form_Unsubscribe_From_Confirmation_Email,  short Form_Acknowledgment_Email_Bounce, short Form_Acknowledgment_Email_First_Open, short Form_Subsequent_Open, short Form_Unsubscribe_From_Acknowledgment_Email, short Form_Click_Link_On_Acknowledgment_Email, short Form_Subsequent_click, short Email_Bounce, short Email_Unsubscribe, short Email_Click_Link_First, short Email_Subsequent_Click, short SMS_Bounce, short SMS_Unsubscribe)
        {
            DataAccessLayer.Controller.DefaultScore def = new DataAccessLayer.Controller.DefaultScore();
            bool result = def.UpdateDefaultScore(Id,AccountId, Form_Submit, Form_Confirmation_Email_Bounce, Form_Unsubscribe_From_Confirmation_Email, Form_Acknowledgment_Email_Bounce, Form_Acknowledgment_Email_First_Open, Form_Subsequent_Open, Form_Unsubscribe_From_Acknowledgment_Email, Form_Click_Link_On_Acknowledgment_Email, Form_Subsequent_click, Email_Bounce, Email_Unsubscribe, Email_Click_Link_First, Email_Subsequent_Click, SMS_Bounce, SMS_Unsubscribe);
            return result;
        }
    }
}
