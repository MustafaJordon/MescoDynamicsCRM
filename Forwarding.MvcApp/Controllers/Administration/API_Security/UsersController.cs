using Forwarding.BLL.Utilities;
using Forwarding.MvcApp.Models.Administration.Security.Generated;
using Forwarding.MvcApp.Models.Administration.Settings.Customized;
using Forwarding.MvcApp.Models.Administration.Settings.Generated;
using Forwarding.MvcApp.Models.FA.Generated;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebMatrix.WebData;

//the update method is in a standalone webapicontroller coz i had problems to make the 2 posts on the same controller
namespace Forwarding.MvcApp.Controllers.Administration.API_Security
{
    public class UsersController : ApiController
    {
        [HttpGet]
        public object LoadAllIDs(string pWhereClause)
        {
            CvwUsers objCvwUsers = new CvwUsers();
            objCvwUsers.GetList(pWhereClause);
            string IDs = string.Join(",", objCvwUsers.lstCVarvwUsers.Select(a => a.ID));
            return IDs;
        }
        //[Route("/api/Users/LoadAll")]
        [HttpGet, HttpPost]
        //sherif: to be used in select boxes
        public Object[] LoadAll(string pWhereClause)
        {
            CvwUsers objCvwUsers = new CvwUsers();
            objCvwUsers.GetList(pWhereClause);
            return new Object[] { new JavaScriptSerializer().Serialize(objCvwUsers.lstCVarvwUsers) };
        }
        [HttpGet, HttpPost]
        //sherif: to be used in select boxes
        public Object[] LoadAllUserSalesmen(string pWhereClause)
        {
            CvwUsersSalesmen objCvwUsersSalesmen = new CvwUsersSalesmen();
            objCvwUsersSalesmen.GetList(pWhereClause);
            return new Object[] { new JavaScriptSerializer().Serialize(objCvwUsersSalesmen.lstCVarvwUsersSalesmen) };
        }
        [HttpGet, HttpPost]
        public Object[] LoadWithPaging(Int32 pPageNumber, Int32 pPageSize, string pSearchKey)
        {
            CvwUsers objCvwUsers = new CvwUsers();
            //objCvwUsers.GetList(string.Empty);
            Int32 _RowCount = 0;// objCvwUsers.lstCVarUsers.Count;

            pSearchKey = (pSearchKey == null ? "" : pSearchKey.Trim().ToUpper());
            string whereClause = " Where ( Username LIKE '%" + pSearchKey + "%' "
            + " OR Name LIKE '%" + pSearchKey + "%' "
            + " OR LocalName LIKE '%" + pSearchKey + "%' "
            + " OR BranchName LIKE '%" + pSearchKey + "%' "
            + " OR DepartmentName LIKE '%" + pSearchKey + "%' "
            + " OR RoleName Like '%" + pSearchKey + "%' ) AND IsNull( CustomerID , 0 ) = 0";

            objCvwUsers.GetListPaging(pPageSize, pPageNumber, whereClause, " Username ", out _RowCount);

            return new Object[] { new JavaScriptSerializer().Serialize(objCvwUsers.lstCVarvwUsers), _RowCount };
        }

        [HttpGet]
        public void Insert()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("ConnectionString", "Users", "ID", "Username", autoCreateTables: false);
            }
        }

        [HttpPost]
        public object[] Insert([FromBody] InsertUserData insertUserData)
        {
            bool _result = false;
            try
            {
                WebSecurity.CreateUserAndAccount(insertUserData.pUsername, insertUserData.pPassword
                    , new
                    {
                        Name = insertUserData.pName,
                        LocalName = insertUserData.pLocalName,
                        BranchID = insertUserData.pBranchID,
                        DepartmentID = insertUserData.pDepartmentID,
                        Email = insertUserData.pEmail,
                        Phone1 = insertUserData.pPhone1,
                        Phone2 = insertUserData.pPhone2,
                        Mobile1 = insertUserData.pMobile1,
                        Address = insertUserData.pAddress,
                        Password = "",
                        IsInactive = insertUserData.pIsInactive,
                        Notes = insertUserData.pNotes,
                        IsSalesman = insertUserData.pIsSalesman,
                        IsAccessAllCharges = insertUserData.pIsAccessAllCharges,
                        IsHideOthersRecords = insertUserData.pIsHideOthersRecords,
                        ExpirationDate = insertUserData.pExpirationDate,
                        CreatorUserID = WebSecurity.CurrentUserId,
                        CreationDate = DateTime.Now,
                        ModificatorUserID = WebSecurity.CurrentUserId,
                        ModificationDate = DateTime.Now,
                        HeartBeatDate = DateTime.Parse("01-01-2015")
                        ,
                        Email_DisplayName = insertUserData.pEmail_DisplayName,
                        Email_Footer = insertUserData.pEmail_Footer,
                        Email_Header = insertUserData.pEmail_Header,
                        Email_Host = insertUserData.pEmail_Host,
                        Email_IsSSL = bool.Parse(insertUserData.pEmail_IsSSL),
                        Email_Password = (insertUserData.pEmail_Password == "0" || insertUserData.pEmail_Password == "" || insertUserData.pEmail_Password == null) ? "0" : CEncryptDecrypt.Encrypt(insertUserData.pEmail_Password, true),
                        Email_Port = int.Parse(insertUserData.pEmail_Port) 
                        //,
                        //CustomerID = 0 //---- 10/10/2020

                    }, false);



            }
            catch (MembershipCreateUserException e)
            {
                if (e.StatusCode.ToString() == "DuplicateUserName")
                    _result = false;
                else
                { //because i have another exception because of the trigger
                    _result = true;
                    int _ID = WebSecurity.GetUserId(insertUserData.pUsername);

                    CInsertUserSalesmenList objCInsertUserSalesmenList = new CInsertUserSalesmenList();
                    var exc = objCInsertUserSalesmenList.InsertUserSalesmenList(_ID, insertUserData.pUserSalesmen);


                    //if (exc == null)
                    //    _result = true;
                    //else
                    //    _result = false;
                }
            }

            return new object[] { _result };
        }

        [HttpPost]
        //[ActionName("Update")]
        public object[] Update([FromBody] UpdateUserData updateUserData)
        {
            bool _result = false;
            CVarUsers objCVarUsers = new CVarUsers();
            
            int _RowCount = 0;
            CUsers objCUser_temp = new CUsers();
            objCUser_temp.GetListPaging(999999, 1, "WHERE ID = " + updateUserData.pID  + "", "ID", out _RowCount);
            var _UserEmailPassword = objCUser_temp.lstCVarUsers[0].Email_Password;
            
            //the next 4 lines to get the CreatorUserID and CreationDate and set them as they were entered before
            CUsers objCGetCreationInformation = new CUsers();
            objCGetCreationInformation.GetItem(updateUserData.pID);
            objCVarUsers.CreatorUserID = objCGetCreationInformation.lstCVarUsers[0].CreatorUserID;
            objCVarUsers.CreationDate = objCGetCreationInformation.lstCVarUsers[0].CreationDate;
            objCVarUsers.HeartBeatDate = objCGetCreationInformation.lstCVarUsers[0].HeartBeatDate;

            objCVarUsers.ID = updateUserData.pID;

            objCVarUsers.Name = updateUserData.pName;
            objCVarUsers.LocalName = updateUserData.pLocalName;
            objCVarUsers.BranchID = updateUserData.pBranchID;
            objCVarUsers.DepartmentID = updateUserData.pDepartmentID;
            objCVarUsers.RoleID = updateUserData.pRoleID;
            objCVarUsers.Email = updateUserData.pEmail;
            objCVarUsers.Phone1 = updateUserData.pPhone1;
            objCVarUsers.Phone2 = updateUserData.pPhone2;
            objCVarUsers.Mobile1 = updateUserData.pMobile1;
            objCVarUsers.Address = updateUserData.pAddress;
            objCVarUsers.Username = (updateUserData.pUsername == null ? "" : updateUserData.pUsername.Trim().ToUpper());
            objCVarUsers.Password = "";
            objCVarUsers.ExpirationDate = updateUserData.pExpirationDate;

            objCVarUsers.IsInactive = updateUserData.pIsInactive;
            objCVarUsers.Notes = (updateUserData.pNotes == null ? "" : updateUserData.pNotes.Trim().ToUpper());
            objCVarUsers.IsSalesman = updateUserData.pIsSalesman;


            objCVarUsers.IsAccessAllCharges = updateUserData.pIsAccessAllCharges;
            objCVarUsers.IsHideOthersRecords = updateUserData.pIsHideOthersRecords;

            objCVarUsers.Email_DisplayName = updateUserData.pEmail_DisplayName;
            objCVarUsers.Email_Footer = updateUserData.pEmail_Footer;
            objCVarUsers.Email_Header = updateUserData.pEmail_Header;
            objCVarUsers.Email_Host = updateUserData.pEmail_Host;
            objCVarUsers.Email_IsSSL = bool.Parse(updateUserData.pEmail_IsSSL);
            objCVarUsers.CustomerID = 0;//---- 10/10/2020

            if (updateUserData.pEmail_Password == "0" || updateUserData.pEmail_Password == "" || updateUserData.pEmail_Password == null)
            {
                objCVarUsers.Email_Password = (_UserEmailPassword == "0" || _UserEmailPassword == "" || _UserEmailPassword == null) ? "0" : _UserEmailPassword ;
            }
            else
            {
                objCVarUsers.Email_Password =  CEncryptDecrypt.Encrypt(updateUserData.pEmail_Password, true);
            }






            objCVarUsers.Email_Port = int.Parse(updateUserData.pEmail_Port);







            objCVarUsers.ModificatorUserID = WebSecurity.CurrentUserId;
            objCVarUsers.ModificationDate = DateTime.Now;

            CUsers objCUsers = new CUsers();
            objCUsers.lstCVarUsers.Add(objCVarUsers);
            Exception checkException = objCUsers.SaveMethod(objCUsers.lstCVarUsers);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
            //Resetting the Password in case of update success
            {
                _result = true;
                if (updateUserData.pPassword != "")
                {
                    WebSecurity.ResetPassword(WebSecurity.GeneratePasswordResetToken(updateUserData.pUsername), updateUserData.pPassword);
                    if (updateUserData.pID == updateUserData.pLoggedUserID)
                        WebSecurity.Login(updateUserData.pUsername.Trim().ToUpper(), updateUserData.pPassword); //to handle case of changing username for the logged user
                }


                CInsertUserSalesmenList objCInsertUserSalesmenList = new CInsertUserSalesmenList();
                var exc = objCInsertUserSalesmenList.InsertUserSalesmenList(updateUserData.pID, updateUserData.pUserSalesmen);


                //if (exc == null)
                //    _result = true;
                //else
                //    _result = false;

            }
            return new object[] { _result, objCUsers.lstCVarUsers[0].ID };
        }



        // [Route("/api/Users/DeleteByID/{pID}")]
        //[HttpGet, HttpPost]
        //public void DeleteByID(Int32 pID)
        //{
        //    CUsers objCUsers = new CUsers();
        //    objCUsers.lstDeletedCPKUsers.Add(new CPKUsers() { ID = pID });
        //    objCUsers.DeleteItem(objCUsers.lstDeletedCPKUsers);
        //}

        // [Route("api/Users/Delete/{pUsersIDs}")]

        [HttpGet, HttpPost]
        public bool Delete(String pUsersIDs)
        {
            bool _result = false;
            CUsers objCUsers = new CUsers();
            foreach (var currentID in pUsersIDs.Split(','))
            {
                objCUsers.lstDeletedCPKUsers.Add(new CPKUsers() { ID = Int32.Parse(currentID.Trim()) });
            }

            Exception checkException = objCUsers.DeleteItem(objCUsers.lstDeletedCPKUsers);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("DELETE")) // some or all of the rows were not deleted due to dependencies
                    _result = false;
            }
            else //deleted successfully
                _result = true;

            //delete from webpages_Membership to have the same records like Users
            Cwebpages_Membership objCwebpages_Membership = new Cwebpages_Membership();
            objCwebpages_Membership.DeleteList(" WHERE UserId NOT IN (SELECT ID FROM Users) ");

            return _result;
        }

        [HttpPost]
        public object[] ChangePassword([FromBody] ChangePassword changePassword)
        {
            bool _result = false;
            bool isPasswordCorrect = WebSecurity.Login(WebSecurity.CurrentUserName, changePassword.pOldPassword, false);
            if (isPasswordCorrect)
                _result = WebSecurity.ResetPassword(WebSecurity.GeneratePasswordResetToken(WebSecurity.CurrentUserName), changePassword.pNewPassword);
            return new object[] {
                _result
            };
        }




        [HttpGet, HttpPost]
        public Object[] GetUserBranches(string pUserID)
        {
            var serialize = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            CvwFA_UserBranches cvwFA_UserBranches = new CvwFA_UserBranches();
            cvwFA_UserBranches.GetList("where UserID = " + pUserID + "");

            CBranches cBranches = new CBranches();
            cBranches.GetList("where 1 = 1");



            return new Object[] {serialize.Serialize(cvwFA_UserBranches.lstCVarvwFA_UserBranches)
                ,
                serialize.Serialize(cBranches.lstCVarBranches)
            };
        }
        [HttpPost]
        //[ActionName("Update")]
        public object[] UpdateUserBranches([FromBody] UserBranches model)
        {
            bool _result = false;
            CVarBranches cVarBranches = new CVarBranches();

            int _RowCount = 0;

            CInsertUserBranches cInsertUserBranches = new CInsertUserBranches();

            var UserID = int.Parse(model.pID);
            var checkException = cInsertUserBranches.InsertUserBranches(UserID, model.pBranchesIDs);



            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
            //Resetting the Password in case of update success
            {


                _result = true;

            }
            return new object[] { _result, model.pID };
        }
        
    }
    public class UserBranches
    {
        public string pID { get; set; }
        public string pBranchesIDs { get; set; }
    }
    public class InsertUserData

    {
        public string pName { get; set; }
        public string pLocalName { get; set; }
        public string pBranchID { get; set; }
        public string pDepartmentID { get; set; }
        public string pEmail { get; set; }
        public string pPhone1 { get; set; }
        public string pPhone2 { get; set; }
        public string pMobile1 { get; set; }
        public string pAddress { get; set; }

        public string pUsername { get; set; }
        public string pPassword { get; set; }
        public string pExpirationDate { get; set; }
        public string pIsInactive { get; set; }
        public string pNotes { get; set; }
        public string pIsSalesman { get; set; }

        public string pEmail_Password { get; set; }
        public string pEmail_DisplayName { get; set; }
        public string pEmail_Host { get; set; }
        public string pEmail_Port { get; set; }
        public string pEmail_IsSSL { get; set; }
        public string pEmail_Header { get; set; }
        public string pEmail_Footer { get; set; }
        public string pUserSalesmen { get; set; }

        public bool pIsAccessAllCharges { get; set; }
        public bool pIsHideOthersRecords { get; set; }
    }

    public class UpdateUserData
    {
        public int pID { get; set; }
        public int pLoggedUserID { get; set; }

        public string pName { get; set; }
        public string pLocalName { get; set; }
        public int pBranchID { get; set; }
        public int pDepartmentID { get; set; }
        public int pRoleID { get; set; }
        public string pEmail { get; set; }
        public string pPhone1 { get; set; }
        public string pPhone2 { get; set; }
        public string pMobile1 { get; set; }
        public string pAddress { get; set; }

        public string pUsername { get; set; }
        public string pPassword { get; set; }
        public DateTime pExpirationDate { get; set; }
        public bool pIsInactive { get; set; }
        public string pNotes { get; set; }
        public bool pIsSalesman { get; set; }



        public string pEmail_Password { get; set; }
        public string pEmail_DisplayName { get; set; }
        public string pEmail_Host { get; set; }
        public string pEmail_Port { get; set; }
        public string pEmail_IsSSL { get; set; }
        public string pEmail_Header { get; set; }
        public string pEmail_Footer { get; set; }
        public string pUserSalesmen { get; set; }

        public bool pIsAccessAllCharges { get; set; }
        public bool pIsHideOthersRecords { get; set; }
    }

    public class ChangePassword
    {
        public string pOldPassword { get; set; }
        public string pNewPassword { get; set; }
    }
}
