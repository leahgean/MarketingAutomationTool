using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enterprise.Library;

namespace BLL.Messages
{
    public class MessageEO : BaseEO
    {
        public Result LoadByEntityID(int? accountID, int entityID)
        {
            Result result = new Result();
            try
            {
                DAL.Messages.MessageData msgData = DAL.Facade.CreateMessageDataObject(null);
                SingleResult<COM_MESSAGE> msgResult = msgData.SelectByEntityID(accountID, entityID);
                if (msgResult.Successful)
                    LoadProperties(msgResult.Entity);
                else
                {
                    result.UpdateError(msgResult);
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.UpdateError(ex);
            }
            return result;
        }
    }
}
