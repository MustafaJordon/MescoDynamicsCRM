using Forwarding.MvcApp.Models.Administration.Security.Generated;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebMatrix.WebData;

namespace Forwarding.MvcApp.Controllers.NoAccess.Security
{
    public class FormsController : ApiController
    {
        //[Route("/api/Security/Modules_LoadAll")]
        [HttpPost] // sherif: to load forms
        public Object[] Forms_LoadAll([FromBody] FormsData formsData)
        {
            Int32 _FormsRowCount, _GroupsRowCount = 0;
            //sherif: i changed here the httpverb to be get so i can send the pCultureID parameter from the js ajax
            //sherif: i didnt call SetLanguage fn cz it cant be seen here as this class is an ApiController
            Thread.CurrentThread.CurrentCulture = new CultureInfo(formsData.pCutlureID);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            //string EncryptedActiveGroupCode = Forwarding.BLL.Utilities.CEncryptDecrypt.Encrypt(pActiveGroup, true);
            //pWhereClause += " and GroupCode = '" + EncryptedActiveGroupCode + "'";
            formsData.pWhereClause = " WHERE UserID = " + WebSecurity.CurrentUserId.ToString();
            formsData.pWhereClause += " AND CanView = 1 AND IsInactive <> 1 ";
            CvwUserForms objCvwUserForms = new CvwUserForms();
            //objCForms.GetModules();
            objCvwUserForms.GetListPaging(999, 1, formsData.pWhereClause, formsData.pOrderBy, out _FormsRowCount);
            if (objCvwUserForms != null && objCvwUserForms.lstCVarvwUserForms.Count > 0)
                foreach (var currentForm in objCvwUserForms.lstCVarvwUserForms)
                {
                    //the next line is to get the decrypted Code (written in hl-menu-CODE) (dont use resources)
                    currentForm.DecryptedCode = Forwarding.BLL.Utilities.CEncryptDecrypt.Decrypt(currentForm.Code, true);
                    //the next 2 lines to get the decrypted name
                    var value = Forwarding.BLL.Utilities.CEncryptDecrypt.Decrypt(currentForm.Code, true);// currentForm.DecryptedName.ToString();
                    currentForm.DecryptedName = Forwarding.MvcApp.App_Resources.App_Resources.ResourceManager.GetString(value);
                    //the next 2 lines to get the decrypted description
                    value = Forwarding.BLL.Utilities.CEncryptDecrypt.Decrypt(currentForm.EncryptedDescription, true);
                    currentForm.DecryptedDescription = Forwarding.MvcApp.App_Resources.App_Resources.ResourceManager.GetString(value);
                    //the next 2 lines to get the decrypted group code
                    value = Forwarding.BLL.Utilities.CEncryptDecrypt.Decrypt(currentForm.GroupCode, true);
                    //i didnt get it from resources to be in english
                    currentForm.GroupCode = value;//Forwarding.MvcApp.App_Resources.App_Resources.ResourceManager.GetString(value);
                }
            //to get groups(tabs)
            CvwUserGroups objCvwUserGroups = new CvwUserGroups();
            formsData.pWhereClause = " WHERE ParentGroupID IS NOT NULL AND IsInactive<>1 AND UserID = " + WebSecurity.CurrentUserId.ToString();
            objCvwUserGroups.GetListPaging(999, 1, formsData.pWhereClause, " GroupOrderNo ", out _GroupsRowCount);
            if (objCvwUserGroups != null && objCvwUserGroups.lstCVarvwUserGroups.Count > 0)
                foreach (var currentGroup in objCvwUserGroups.lstCVarvwUserGroups)
                {
                    //i need the next line to set the groupcode as it is used in the fillforms fn
                    currentGroup.GroupCode = Forwarding.BLL.Utilities.CEncryptDecrypt.Decrypt(currentGroup.GroupCode, true);
                    var value = currentGroup.GroupCode.ToString();
                    currentGroup.GroupDecryptedName = Forwarding.MvcApp.App_Resources.App_Resources.ResourceManager.GetString(value);
                    //currentGroup.ParentGroupCode = Forwarding.BLL.Utilities.CEncryptDecrypt.Decrypt(currentGroup.ParentGroupCode, true);
                }
            return new Object[] { new JavaScriptSerializer().Serialize(objCvwUserForms.lstCVarvwUserForms), _FormsRowCount, 
                new JavaScriptSerializer().Serialize(objCvwUserGroups.lstCVarvwUserGroups), _GroupsRowCount, formsData.pActiveGroup };
        }
    }

    public class FormsData
    {
        public string pCutlureID { get; set; }
        public string pWhereClause { get; set; }
        public string pOrderBy { get; set; }
        public string pActiveGroup { get; set; }
    }
}

