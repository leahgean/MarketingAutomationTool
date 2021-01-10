using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enterprise.Library;

namespace DAL
{
    public class MessageData : BaseData<COM_MESSAGE>
    {
        #region Constructors

        public MessageData(string connectionString)
            : base(connectionString)
        {
        }

        public MessageData(DataClasses1DataContext dataContext)
            : base(dataContext)
        {
        }

        #endregion

        public override Result Delete(string id, int? accountID, Guid userId)
        {
            Result result = new Result();
            try
            {
                DataClasses1DataContext dc = GetDataContext();
                dc.COM_MESSAGE_DELETE(accountID, int.Parse(id), userId);
            }
            catch (Exception ex)
            {
                result.UpdateError(ex);
            }
            return result;

        }

        public override MultipleResult<COM_MESSAGE> Select(int? accountID)
        {
            MultipleResult<COM_MESSAGE> result = new MultipleResult<COM_MESSAGE>();

            try
            {
                DataClasses1DataContext dc = GetDataContext();
                result.Entities = dc.COM_MESSAGE_SELECT(accountID.Value).ToList();
            }
            catch (Exception ex)
            {
                result.UpdateError(ex);
            }

            return result;
        }

        public override SingleResult<COM_MESSAGE> SelectByID(int? accountID, string id)
        {
            SingleResult<COM_MESSAGE> result = new SingleResult<COM_MESSAGE>();

            try
            {
                DataClasses1DataContext dc = GetDataContext();
                // MON 21 Nov 2013
                // ADDED THE ACCOUNT ID 
                result.Entity = dc.COM_MESSAGE_SELECT_BY_ID(int.Parse(id), accountID).SingleOrDefault();
                if (result.Entity == null)
                    result.UpdateError("Message is not found or message is deleted");

                /* removed this part.... i think a lot of modules will be affected by this move
                // MON 09 Oct 2013 added this validation just to be sure that the account is being used
                // put i here instead due to time constraint 
                if (result.Entity != null)
                {
                    if (!accountID.HasValue)
                    {
                        result.UpdateError("Account is not specified");
                    }
                    // MON 09 Oct 2013 now check if the user is a member of the same account as the creator of the message
                    if (result.Entity.USER_ID.HasValue)
                    {
                        DAL.User.User usr = new User.User(this.DataClasses1DataContext);
                        var usrAccountID = usr.GetUserAccountID(result.Entity.USER_ID.ToString());
                        if (accountID.Value != usrAccountID)
                        {
                            result.UpdateError("Message is not found or message is deleted");
                        }
                    }
                    
                }
                 */
            }
            catch (Exception ex)
            {
                result.UpdateError(ex);
            }

            return result;
        }

        public Result UpdateJob(int messageId, int channelId)
        {
            Result result = new Result();
            try
            {
                DataClasses1DataContext dc = GetDataContext();
                dc.COM_MESSAGE_UPDATE_JOB(messageId, channelId);
            }
            catch (Exception ex)
            {
                result.UpdateError(ex);
            }
            return result;

        }

        public SingleResult<COM_MESSAGE> SelectByEntityID(int? accountID, int entityId)
        {
            SingleResult<COM_MESSAGE> result = new SingleResult<COM_MESSAGE>();

            try
            {
                DataClasses1DataContext dc = GetDataContext();
                //Changed from single to first
                result.Entity = dc.COM_MESSAGE_SELECT_BY_ENTITYID(accountID, entityId).First();
                if (result.Entity == null)
                    result.UpdateError("Message is not found or message is deleted");
            }
            catch (Exception ex)
            {
                result.UpdateError(ex);
            }

            return result;
        }

        // MON 20 Jan 2014
        // 
        public SingleResult<COM_MESSAGE> SelectByDeletedEntityID(int? accountID, int? entityId)
        {
            SingleResult<COM_MESSAGE> result = new SingleResult<COM_MESSAGE>();

            try
            {
                DataClasses1DataContext dc = GetDataContext();
                result.Entity = dc.COM_MESSAGE_SELECT_DELETED(entityId, accountID).SingleOrDefault();
                if (result.Entity == null)
                    result.UpdateError("Message is not found or message is deleted");
            }
            catch (Exception ex)
            {
                result.UpdateError(ex);
            }

            return result;
        }
        // MON 06 Dec 2013
        // this is for view all
        public List<MessageList> GetList(int? p_iAccountId, int? p_iChannelId, int? p_iIndex, int? p_iSize)
        {
            List<MessageList> lst = new List<MessageList>();
            try
            {

                int? iOut = 0;
                DataClasses1DataContext dt = GetDataContext();
                lst = dt.S_GET_MESSAGE_PAGING(p_iAccountId, p_iIndex, p_iSize, p_iChannelId, ref iOut).Select(e => new MessageList()
                {
                    AccountId = e.ACCOUNT_ID.GetValueOrDefault(0),
                    ChannelId = e.CHANNEL_ID.GetValueOrDefault(0),
                    Created = e.CREATED.Value,
                    Entity = e.ENTITY,
                    EntityId = e.ENTITY_ID.GetValueOrDefault(0),
                    MessageId = e.MESSAGE_ID.GetValueOrDefault(0),
                    Name = e.NAME,
                    Row = e.ROW.GetValueOrDefault(0),
                    StatusId = e.STATUS_ID.GetValueOrDefault(0),
                    StatusName = e.STATUS_NAME,
                    Subject = e.SUBJECT,
                    Submitted = e.SUBMITTED,
                    UserName = e.USERNAME
                }).ToList();

                this.RowCount = iOut.GetValueOrDefault(0);

            }
            catch (Exception)
            {

            }
            return lst;
        }
        // MON 06 Dec 2013
        // this is for the listing

        //Get message Account_id
        // We need the account id for the display of the message
        public int GetAccountId(int p_iMessageId)
        {
            DataClasses1DataContext dt = GetDataContext();
            try
            {
                return dt.COM_MESSAGEs.First(e => e.MESSAGE_ID == p_iMessageId).USR_USER.AccountId.GetValueOrDefault(0);
            }
            catch (Exception)
            {

                return 0;
            }
            finally
            {

            }
        }

        // MON 17 Mar 2014
        // set the hidden fields values
        public void SetHidden(int id, bool visibility)
        {
            DataClasses1DataContext dt = GetDataContext();
            try
            {
                var x = dt.COM_MESSAGEs.First(e => e.MESSAGE_ID == id);

                if (x == null) return;
                if (visibility)
                    x.HIDDEN = visibility;
                else
                    x.HIDDEN = null;

                dt.SubmitChanges();
            }
            catch (Exception)
            {

            }
            finally
            {

            }
        }

        // MON 05 May 2014
        // for getting the message subject line only
        public List<MessageInfo> GetMessagesMini(int accountid, int channelid)
        {
            List<MessageInfo> result = new List<MessageInfo>();

            try
            {
                DataClasses1DataContext dc = GetDataContext();
                result = dc.COM_MESSAGE_SELECT_MINI(accountid, channelid).Select(e => new MessageInfo()
                {
                    ChannelId = e.CHANNEL_ID,
                    MessageId = e.MESSAGE_ID,
                    Name = e.NAME,
                    Subject = e.SUBJECT,
                    From = e.FROM_ADDRESS
                }).ToList();
            }
            catch (Exception ex)
            {
                ///result.UpdateError(ex);
            }

            return result;
        }

        public System.Data.Linq.Binary GetTimeStamp(int msgId)
        {
            try
            {
                DataClasses1DataContext dc = GetDataContext();
                return dc.COM_MESSAGEs.First(e => e.MESSAGE_ID == msgId).VERSION;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public void UpdateToData(int msgId, string toName, string toAddress)
        {
            try
            {
                DataClasses1DataContext dc = GetDataContext();
                var dt = dc.COM_MESSAGEs.First(e => e.MESSAGE_ID == msgId);
                dt.TO_ADDRESS = toAddress;
                dt.TO_NAME = toName;
                dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // MON 16 Jun 2014
        // 
        public string GetMessageName(int msgId)
        {
            try
            {
                DataClasses1DataContext dc = GetDataContext();
                return dc.COM_MESSAGEs.First(e => e.MESSAGE_ID == msgId).NAME;
            }
            catch (Exception ex)
            {
                return string.Empty; ;
            }

        }
    }
    public class MessageList
    {
        public int Row { get; set; }
        public int AccountId { get; set; }
        public int MessageId { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public DateTime Created { get; set; }
        public string UserName { get; set; }
        public string Submitted { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int ChannelId { get; set; }
        public string Entity { get; set; }
        public int EntityId { get; set; }
    }
    public class MessageInfo
    {
        public int MessageId { get; set; }
        public int ChannelId { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public string From { get; set; }
    }
}
