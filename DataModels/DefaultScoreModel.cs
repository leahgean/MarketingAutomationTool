using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
   public class DefaultScoreModel
    {
        public int Id;
        public Guid Account_Id;
        public short Form_Submit;
        public short Form_Confirmation_Email_Bounce;
        public short Form_Unsubscribe_From_Confirmation_Email;
        public short Form_Acknowledgment_Email_Bounce;
        public short Form_Acknowledgment_Email_First_Open;
        public short Form_Subsequent_Open;
        public short Form_Unsubscribe_From_Acknowledgment_Email;
        public short Form_Click_Link_On_Acknowledgment_Email;
        public short Form_Subsequent_click;
        public short Email_Bounce;
        public short Email_Unsubscribe;
        public short Email_Click_Link_First;
        public short Email_Subsequent_Click;
        public short SMS_Bounce;
        public short SMS_Unsubscribe;

    }
}
