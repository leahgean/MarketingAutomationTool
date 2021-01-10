using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASPSnippets.FaceBookAPI;
using System.Web.Script.Serialization;
using DataModels;
using System.Configuration;

namespace MarketingAutomationTool.MyAccount
{
    public partial class FacebookLogin : System.Web.UI.Page
    {

        private DataModels.UserLogin userlogin
        {
            get
            {
                if (ViewState["loggeduser"] != null)
                {
                    return (DataModels.UserLogin)ViewState["loggeduser"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["loggeduser"] = value;
            }
        }


        public string LoginState
        {
            get
            {
                if (ViewState["LoginState"] != null)
                {
                    return ViewState["LoginState"].ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["LoginState"] = value;
            }
        }

        public string FBId
        {
            get
            {
                if (ViewState["FBId"] != null)
                {
                    return ViewState["FBId"].ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["FBId"] = value;
            }
        }

        public string FBUserName
        {
            get
            {
                if (ViewState["FBUserName"] != null)
                {
                    return ViewState["FBUserName"].ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["FBUserName"] = value;
            }
        }

        public string FBName
        {
            get
            {
                if (ViewState["FBName"] != null)
                {
                    return ViewState["FBName"].ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["FBName"] = value;
            }
        }


        public string FBEmail
        {
            get
            {
                if (ViewState["FBEmail"] != null)
                {
                    return ViewState["FBEmail"].ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["FBEmail"] = value;
            }
        }


        public string FBPictureURL
        {
            get
            {
                if (ViewState["FBPictureURL"] != null)
                {
                    return ViewState["FBPictureURL"].ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["FBPictureURL"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            userlogin = (DataModels.UserLogin)Session["loggeduser"];

            if (userlogin == null)
                Response.Redirect("~/Public/Login.aspx");

            FaceBookConnect.API_Key = ConfigurationManager.AppSettings["FB_API_Key"].ToString().Trim();
            FaceBookConnect.API_Secret = ConfigurationManager.AppSettings["FB_API_Secret"].ToString().Trim();

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["error"] == "access_denied")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('User has denied access.')", true);
                    LoginState = "Login Failed.";
                    return;
                }

                string code = Request.QueryString["code"];
                if (!string.IsNullOrEmpty(code))
                {

                    if (Session["FBAction"].ToString()=="LOGIN")
                    {
                        string data = FaceBookConnect.Fetch(code, "me?fields=id,name,email");
                        FaceBookUser faceBookUser = new JavaScriptSerializer().Deserialize<FaceBookUser>(data);
                        faceBookUser.Picture = new Picture();
                        faceBookUser.Picture.Data = new FaceBookUserPicture();
                        faceBookUser.Picture.Data.Url = string.Format("https://graph.facebook.com/{0}/picture", faceBookUser.Id);


                        LoginState = "Logged Successfully.";
                        FBId = faceBookUser.Id;
                        FBName = faceBookUser.Name;
                        FBEmail = faceBookUser.Email;
                        FBPictureURL = faceBookUser.PictureUrl;
                        imgFB.ImageUrl = faceBookUser.PictureUrl;
                        btnFBLogin.Text = "Change Facebook Login";
                        lnkLoginToFacebook.Text = "Change Facebook Login";
                        dvMessage.InnerText = "Click the button below to logout of Facebook and change Facebook login. Clicking the button below will open Facebook page. After logging out, close the browser and open Marketing Automation Tool in your browser again.";
                    }
                    else if (Session["FBAction"].ToString() == "IMPORT")
                    {
                        string data = FaceBookConnect.Fetch(code, "me?fields=taggable_friends");
                        FaceBookData facebookData = new JavaScriptSerializer().Deserialize<FaceBookData>(data);

                        List<FaceBookUser> lstFBContacts = new List<FaceBookUser>();
                        lstFBContacts= facebookData.Taggable_Friends.Data;
                    }

                }
                else
                {
                    btnFBLogin.Text = "Login to Facebook";
                    lnkLoginToFacebook.Text = "Login to Facebook";
                    dvMessage.InnerText = "Click the button below to login to your Company's Facebook Page";
                }
            }

        }

        protected void lnkBreadHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }

        private void FBLoginLogout()
        {
            string code = Request.QueryString["code"];
            if (!string.IsNullOrEmpty(code))
            {
                FaceBookConnect.Logout(code);
            }
            else
            {
                Session["FBAction"] = "LOGIN";
                FaceBookConnect.Authorize("user_photos,email", Request.Url.AbsoluteUri.Split('?')[0]);
            }
        }

        private void ImportFBContacts()
        {
            Session["FBAction"] = "IMPORT";
            FaceBookConnect.Authorize("user_photos,user_friends", Request.Url.AbsoluteUri);
        }

        protected void btnFBLogin_Click(object sender, EventArgs e)
        {
            FBLoginLogout();
        }

        protected void lnkLoginToFacebook_Click(object sender, EventArgs e)
        {
            FBLoginLogout();

        }

        protected void lnkImportFBContacts_Click(object sender, EventArgs e)
        {
            ImportFBContacts();
        }
    }
}