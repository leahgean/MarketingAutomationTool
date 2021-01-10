using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Accounts
{
    public class MembershipEO : BaseEO
    {
        #region Properties

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public Guid ChangePassID { get; set; }

        //mgonzales 20130715 additional fields
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string PrefComm { get; set; }
        public bool Disabled { get; set; }
        //mgonzales 20130916
        public DateTime? LastLoginDate { get; set; }
        public string LastLoginDate_String
        {
            get
            {
                return LastLoginDate.HasValue ?
                    LastLoginDate.Value.ToString("dd MMM yyyy h:mm tt") : "";
            }
        }
        public string Country { get; set; }

        public string UserName
        {
            get
            {
                SB.User.D.User usr = new SB.User.D.User(SB.Common.D.Settings.ConnectionStringToComposeDB);
                return usr.GetUserName(this.ID);
            }
        }


        #endregion

        protected override Enterprise.Library.Result LoadCustomProperties(DAL.IEntity entity)
        {
            Result result = new Result();

            USR_MEMBERSHIP data = entity as USR_MEMBERSHIP;
            this.ID = data.UserId.ToString();
            this.FirstName = data.FIRST_NAME;
            this.LastName = data.LAST_NAME;
            this.Email = data.Email;
            this.Mobile = data.Mobile;
            this.ChangePassID = data.ChangePassID.Value;

            this.Position = data.POSITION;
            this.PhoneNumber = data.MAIN_CONTACT_NUMBER;
            this.Country = data.COUNTRY;

            //mgonzales 20130816
            // MON 19 Nov 2013
            // update how we get the last login date
            this.LastLoginDate = GetLastLogin(this.ID_AsGuid);

            //mgonzales 20130814
            this.UserID = data.UserId.ToString();

            //TODO : find a better way to do this
            MembershipData entityx = DAL.Facade.CreateMembershipDataObject(null); ;
            this.Disabled = entityx.IsDisabled(data.UserId);
            entityx = null;



            return result;
        }


        // MON 19 Nov 2013
        // this is to be used to get the last login date DUH!!! :D
        private DateTime? GetLastLogin(Guid p_gUserId)
        {
            SB.User.B.User user = new SB.User.B.User();
            DateTime? dt = user.GetLastLoginDate(p_gUserId);
            if (dt != null)
                return dt.Value;
            else
                return null;
        }


        protected override DAL.BaseEntity GetDataObject()
        {
            return Facade.CreateMembershipDataObject(null);
        }

        protected override Enterprise.Library.Result ApplyAccountIdToChilds()
        {
            return new Result();
        }

        protected override Enterprise.Library.Result ApplyIdToChilds()
        {
            return new Result();
        }

        protected override Enterprise.Library.Result LoadImp(int? accountID, string entityID)
        {
            Result result = new Result();
            MembershipData entity;
            try
            {
                //TODO Check how this goes
                entity = DAL.Facade.CreateMembershipDataObject(null);
                SingleResult<USR_MEMBERSHIP> entityResult = entity.SelectByID(accountID, entityID);
                if (entityResult.Successful)
                {
                    LoadProperties(entityResult.Entity);
                }
                else
                {
                    result.UpdateError(entityResult);
                    result.Message = string.Format("Loading ID={0} faild.", entityID);
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.UpdateError(ex);
            }
            finally
            {
                entity = null;
            }
            return result;
        }

        protected override Enterprise.Library.Result SaveImp(DAL.DataContext dataContext, int accountID, Guid? userId)
        {
            //throw new NotImplementedException();

            Result result = new Result();
            MembershipData entity = (MembershipData)GetDataObject();
            try
            {
                if (this.BOStatus != BOStatusEnum.IsNew)
                {
                    result = DAL.Facade.UpdateUserMembership(
                                                                UserID,
                                                                PhoneNumber,
                                                                Mobile,
                                                                Email,
                                                                FirstName,
                                                                LastName,
                                                                Position,
                                                                "E",
                                                                Disabled);

                }
                result.AddMessage("Account Updated.");
            }
            catch (Exception exp)
            {
                result.UpdateError(exp);
            }
            return result;
        }

        public override void Validate(ref Enterprise.Library.Result result)
        {
            //throw new NotImplementedException();
        }

        protected override Enterprise.Library.Result DeleteImp(DAL.DataContext dataContext, Guid? userId)
        {
            throw new NotImplementedException();
        }

        protected override void ValidateDelete(ref Enterprise.Library.Result result)
        {
            throw new NotImplementedException();
        }

        public override Enterprise.Library.Result Init()
        {
            throw new NotImplementedException();
        }

        protected override Enterprise.Library.Result CreateNewImp()
        {
            throw new NotImplementedException();
        }

        protected override string GetDisplayText()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }


        public Enterprise.Library.Result LoadByUserName(string userName)
        {
            Result result = new Result();
            try
            {
                MembershipData entity = DAL.Facade.CreateMembershipDataObject(null);
                SingleResult<USR_MEMBERSHIP> entityResult = entity.SelectByUserName(userName);
                if (entityResult.Successful)
                {
                    LoadProperties(entityResult.Entity);
                }
                else
                {
                    result.UpdateError(entityResult);
                    result.Message = string.Format("Loading UserName={0} faild.", userName);
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.UpdateError(ex);
            }
            return result;
        }

        public Enterprise.Library.Result LoadByChangePassID(string changePassID)
        {
            Result result = new Result();
            try
            {
                MembershipData entity = DAL.Facade.CreateMembershipDataObject(null);
                SingleResult<USR_MEMBERSHIP> entityResult = entity.SelectByChangePassID(changePassID);
                if (entityResult.Successful)
                {
                    LoadProperties(entityResult.Entity);
                }
                else
                {
                    result.UpdateError(entityResult);
                    result.Message = string.Format("Loading ChangePassID={0} faild.", changePassID);
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.UpdateError(ex);
            }
            return result;
        }

        public Enterprise.Library.Result GetInitialAccount(int p_iAccountId)
        {
            Result result = new Result();
            try
            {
                MembershipData entity = DAL.Facade.CreateMembershipDataObject(null);
                SingleResult<USR_MEMBERSHIP> entityResult = entity.SelectInitialAccount(p_iAccountId);
                if (entityResult.Successful)
                {
                    LoadProperties(entityResult.Entity);
                }
                else
                {
                    result.UpdateError(entityResult);
                    result.Message = string.Format("Loading Initial Account={0} failed.", p_iAccountId);
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.UpdateError(ex);
            }
            return result;
        }

        public string GetUserName(string p_sUserID)
        {
            DAL.Accounts.MembershipData entity;
            string UserName = "";

            try
            {
                //TODO Check how this goes
                entity = Facade.CreateMembershipDataObject(null);

                UserName = entity.GetUserName(p_sUserID);

            }
            catch (Exception ex)
            {
                //result.UpdateError(ex);
            }
            finally
            {
                entity = null;
            }
            return UserName;
        }

        //mgonzales 20130816
        /// <summary>
        /// Needed Data this.UserID, this.FirstName, this.LastName, this.Position, this.PrefComm, this.Email, this.Mobile, this.PhoneNumber
        /// </summary>
        /// <returns></returns>
        public Result UpdateMembership()
        {
            Result res = new Result();
            DAL.Accounts.MembershipData mem;
            try
            {
                mem = Facade.CreateMembershipDataObject(null);
                char pref = (string.IsNullOrEmpty(this.PrefComm) ? ' ' : this.PrefComm.ToCharArray()[0]);

                mem.UpdateMembership(new Guid(this.UserID), this.FirstName, this.LastName, this.Position, pref, this.Email, this.Mobile, this.PhoneNumber);


            }
            catch (Exception ex)
            {

                res.UpdateError(ex.Message);
            }

            finally
            {
                mem = null;
            }
            return res;
        }
    }
}
