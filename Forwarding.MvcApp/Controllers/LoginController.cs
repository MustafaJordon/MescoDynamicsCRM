using Forwarding.BLL.Utilities;
using Forwarding.MvcApp.Models.Administration.Security.Generated;
using Forwarding.MvcApp.Models.Administration.Settings.Generated;
using System;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace Forwarding.MvcApp.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            ////////var passwordtoken = WebSecurity.GeneratePasswordResetToken("Admin");
            ////////WebSecurity.ResetPassword(passwordtoken, "123456");
            //Session["UserID"] = "1"; // ex. 
            return View();
        }

        public ActionResult Logout()
        {
            CUsers objCUsers = new CUsers();
            objCUsers.UpdateList("HeartBeatDate='20150101' WHERE ID=" + WebSecurity.CurrentUserId);

            WebSecurity.Logout();

            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
            ////Response.Redirect("Login/Index");
            ////return View();
            //Session.Abandon();

            //// clear authentication cookie
            //HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            //cookie1.Expires = DateTime.Now.AddYears(-1);
            //Response.Cookies.Add(cookie1);

            //// clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            //HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            //cookie2.Expires = DateTime.Now.AddYears(-1);
            //Response.Cookies.Add(cookie2);

            return View("Index");
        }

        [HttpGet]
        public ActionResult CheckIsValidUser()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("ConnectionString", "Users", "ID", "Username", autoCreateTables: false);
            }
            return View();
        }

        [HttpPost]
        public string CheckIsValidUser(String pUserName, String pPassword, string pUrl)
        {
            if (pUrl == "PageDirectly")
            {
                Session["PageDirectly"] = "1";
            }
            else
                Session["PageDirectly"] = "0";
            PageDirectly objPageDirectly = new PageDirectly();
            objPageDirectly._PageDirectly = "1";

            //var session = HttpContext. Current.Session;

            string result = "Enter valid username.";
            // if the user is inactive then set success to false to prevent him from logging
            //if (pUserName != null && pUserName != "")
            //{
            int userID = WebSecurity.GetUserId(pUserName.Trim().ToUpper());
            if (userID != -1) //i.e. user exists
            {
                int _RowCount = 0;
                CDefaults objCDefaults = new CDefaults();
                objCDefaults.GetListPaging(999999, 1, "WHERE 1=1", "ID", out _RowCount);
                CvwUsers objCvwUsers = new CvwUsers();
                objCvwUsers.GetList("WHERE ID = " + userID.ToString());
                bool isInactive = objCvwUsers.lstCVarvwUsers[0].IsInactive;
                int UserCustomerID = objCvwUsers.lstCVarvwUsers[0].CustomerID;
                if (!isInactive)
                {
                    //WebSecurity.ResetPassword(WebSecurity.GeneratePasswordResetToken("ADMIN"), "123456");
                    int _DaysToExpire = 0;
                    if (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "GBL")
                        _DaysToExpire = (DateTime.Parse("02/02/2024") - DateTime.Now).Days;

                    bool isPasswordCorrect = WebSecurity.Login(pUserName, pPassword, false);
                    if (isPasswordCorrect)
                    {
                        if (userID == 4) //admin user id
                            result = "";
                        else if (_DaysToExpire < 0)
                            result = "Please, Contact IST.";
                        else if ((objCvwUsers.lstCVarvwUsers[0].NumberOfLoggedUsers >= objCvwUsers.lstCVarvwUsers[0].NumberOfAllowedSessions) && UserCustomerID == 0)
                            result = "Number of allowed sessions is reached.";
                        //else if (objCvwUsers.lstCVarvwUsers[0].LastUpdateHeartBeatInSecs < 300) //updated less than 5 minutes
                        //    result = "This user is already logged.";
                        else
                            result = "";
                    }
                    else
                        result = "Invalid password.";
                }
                //// 0 if equal, -ve if later, +ve if earlier
                //if (DateTime.Compare(DateTime.Now, objCUsers.lstCVarUsers[0].ExpirationDate) > 0)
                //    success = false;
                //else
                //{
                //    success = ;
                //}
            }
            else
                result = "Invalid username.";
            //}
            return result;
        }

        [HttpPost]
        public string ResetPassword(String pUserNameToReset, string pUrl)
        {
            if (pUrl == "PageDirectly")
            {
                Session["PageDirectly"] = "1";
            }
            else
                Session["PageDirectly"] = "0";
            PageDirectly objPageDirectly = new PageDirectly();
            objPageDirectly._PageDirectly = "1";

            //var session = HttpContext. Current.Session;
            string _MessageReturned = "New password is sent to your email.";
            int _RowCount = 0;
            Exception checkException = null;
            int _UserID = WebSecurity.GetUserId(pUserNameToReset.Trim().ToUpper());
            CDefaults objCDefaults = new CDefaults();
            checkException = objCDefaults.GetListPaging(999999, 1, "WHERE 1=1", "ID", out _RowCount);
            CUsers objCUsers = new CUsers();
            
            if (_UserID == -1)
                _MessageReturned = "Invalid Username";
            else if (objCDefaults.lstCVarDefaults[0].Email == "0"
                        //|| objCDefaults.lstCVarDefaults[0].Email_DisplayName == "0" || objCDefaults.lstCVarDefaults[0].Email_DisplayName == ""
                        || objCDefaults.lstCVarDefaults[0].Email_Host == "0" || objCDefaults.lstCVarDefaults[0].Email_Host == ""
                        || objCDefaults.lstCVarDefaults[0].Email_Port == 0)
                _MessageReturned = "Contact your administrator to enter valid company email settings in the Defaults screen.";
            else
            {
                checkException = objCUsers.GetListPaging(999999, 1, "WHERE ID=" + _UserID, "ID", out _RowCount);
                if (objCUsers.lstCVarUsers[0].Email == "0" || objCUsers.lstCVarUsers[0].Email == "")
                    _MessageReturned = "Sorry, you do not have a valid email address in your account details.";
                else
                {
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*_";
                    var stringChars = new char[15];
                    var random = new Random();
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }
                    var _NewPassword = new String(stringChars);
                    var x = _NewPassword;
                    WebSecurity.ResetPassword(WebSecurity.GeneratePasswordResetToken(pUserNameToReset), _NewPassword);

                    #region Send Email
                    var mail = new MailMessage();
                    //SmtpClient Client = new SmtpClient("mail.sharksbay.com", 587);
                    SmtpClient SmtpServer = new SmtpClient(objCDefaults.lstCVarDefaults[0].Email_Host, objCDefaults.lstCVarDefaults[0].Email_Port);
                    SmtpServer.UseDefaultCredentials = true;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(objCDefaults.lstCVarDefaults[0].Email, CEncryptDecrypt.Decrypt(objCDefaults.lstCVarDefaults[0].Email_Password, true));

                    //SmtpClient SmtpServer = new SmtpClient();
                    //SmtpClient SmtpServer = new SmtpClient("smtpout.secureserver.net", SmtpServer.Port);
                    mail.From = new MailAddress(objCDefaults.lstCVarDefaults[0].Email, objCDefaults.lstCVarDefaults[0].Email_DisplayName);
                    for (int i = 0; i < objCUsers.lstCVarUsers.Count; i++)
                        if (objCUsers.lstCVarUsers[i].Email != "" && objCUsers.lstCVarUsers[i].Email != "0")
                        {
                            mail.To.Add(objCUsers.lstCVarUsers[i].Email);
                            //if (WebSecurity.CurrentUserName == "ADMIN")
                            //    mail.Bcc.Add("sherif@istegy.com");
                        }
                    //mail.CC.Add(CC);
                    mail.Subject = "IST Password Reset.";
                    mail.Body = "<b>Your new password is : " + _NewPassword + "</b><br>";
                    mail.IsBodyHtml = true;
                    //mail.Attachments.Add(new Attachment(pathString));
                    //mail.Attachments.Add(new Attachment("C:\\Users\\Sherif\\Desktop\\CompanyLogo.jpg"));
                    //SmtpServer.Port = 25;
                    //SmtpServer.Port = Convert.ToInt32(ConfigurationSettings.AppSettings["PORT"].ToString());
                    //SmtpServer.Port = Convert.ToInt32(Configuration!System.Configuration.ConfigurationManager.AppSettings["PORT"].ToString());
                    SmtpServer.EnableSsl = objCDefaults.lstCVarDefaults[0].Email_IsSSL;
                    try
                    {
                        SmtpServer.Send(mail);
                    }
                    catch (Exception ex)
                    {
                        _MessageReturned = ex.Message;
                    }
                    #endregion  Send Email
                }
            }
            return _MessageReturned;
        }


        //[HttpGet]
        //public ActionResult Insert()
        //{
        //    if (!WebSecurity.Initialized)
        //    {
        //        WebSecurity.InitializeDatabaseConnection("ConnectionString", "Users", "ID", "Username", autoCreateTables: false);
        //    }
        //    return View();
        //}

        //[HttpPost]
        //public bool Insert(string pUsername, string pPassword, bool pIsInactive, string pNotes = "", bool pIsSystemUser = false)
        //{
        //    bool _result = false;
        //    try
        //    {
        //        WebSecurity.CreateUserAndAccount(pUsername, pPassword, new { Password = "", IsInactive = pIsInactive, Notes = pNotes, IsSystemUser = pIsSystemUser, CreatorUserID = WebSecurity.CurrentUserId, CreationDate = DateTime.Now, ModificatorUserID = WebSecurity.CurrentUserId, ModificationDate = DateTime.Now }, false);
        //        _result = true;
        //    }
        //    catch (MembershipCreateUserException e)
        //    {
        //        if (e.StatusCode.ToString() == "DuplicateUserName")
        //            _result = false;
        //        else //because i have another exception because of the trigger
        //            _result = true;
        //    }

        //    //var membership = (SimpleMembershipProvider)Membership.Provider;
        //    ////This will delete the user information from Users
        //    //bool UserDeleted = membership.DeleteUser(pUsername, true);

        //    //if (UserDeleted)
        //    //{

        //    //    try
        //    //    {
        //    //        //This will delete the user information from webpages_membership
        //    //        bool webpages_membershipDeleted = membership.DeleteAccount(pUsername);

        //    //    }
        //    //    catch (Exception e) { }

        //    //    //FormsAuthentication.SignOut();
        //    //    //FormsAuthentication.RedirectToLoginPage();
        //    //}
        //    ////Response.Redirect("~/account/login");
        //    return _result;
        //}

        //    [HttpPost]
        //    public bool Update(Int32 pID, string pUsername, string pPassword, bool pIsInactive, string pNotes = "", bool pIsSystemUser = false)
        //    {
        //        bool _result = false;

        //        CVarUsers objCVarUsers = new CVarUsers();

        //        //the next 4 lines to get the CreatorUserID and CreationDate and set them as they were entered before
        //        CUsers objCGetCreationInformation = new CUsers();
        //        objCGetCreationInformation.GetItem(pID);
        //        objCVarUsers.CreatorUserID = objCGetCreationInformation.lstCVarUsers[0].CreatorUserID;
        //        objCVarUsers.CreationDate = objCGetCreationInformation.lstCVarUsers[0].CreationDate;

        //        objCVarUsers.ID = pID;

        //        objCVarUsers.Username = (pUsername == null ? "" : pUsername.Trim().ToUpper());
        //        //objCVarUsers.Password = (pPassword == null ? "" : Forwarding.BLL.Utilities.CEncryptDecrypt.Encrypt(pPassword.Trim(), true));
        //        objCVarUsers.Password = "";

        //        objCVarUsers.IsInactive = pIsInactive;
        //        objCVarUsers.Notes = (pNotes == null ? "" : pNotes.Trim().ToUpper());
        //        objCVarUsers.IsSystemUser = pIsSystemUser;

        //        objCVarUsers.ModificatorUserID = WebSecurity.CurrentUserId;
        //        objCVarUsers.ModificationDate = DateTime.Now;

        //        CUsers objCUsers = new CUsers();
        //        objCUsers.lstCVarUsers.Add(objCVarUsers);
        //        Exception checkException = objCUsers.SaveMethod(objCUsers.lstCVarUsers);
        //        if (checkException != null) // an exception is caught in the model
        //        {
        //            if (checkException.Message.Contains("UNIQUE"))
        //                _result = false;
        //        }
        //        else //not unique
        //            //Resetting the Password in case of update success
        //            WebSecurity.ResetPassword(WebSecurity.GeneratePasswordResetToken(pUsername), pPassword);
        //            _result = true;
        //        return _result;
        //    }
    }

    public class PageDirectly
    {

        internal String m_PageDirectly;
        public String _PageDirectly
        {
            get { return m_PageDirectly; }
            set { m_PageDirectly = value; }
        }
    }
}