using Forwarding.MvcApp.Models.CRM.CRM_SalesMenTarget.Generated;
using Forwarding.MvcApp.Models.CRM.CRM_Sources.Generated;
using Forwarding.MvcApp.Models.MasterData.Locations.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebMatrix.WebData;
using Forwarding.MvcApp.Models.CRM.CRM_Actions.Generated;
using Forwarding.MvcApp.Models.Administration.Security.Generated;
using Forwarding.MvcApp.Models.CRM.CRM_ActionStatues.Generated;
using System.Globalization;
using Forwarding.MvcApp.Models.CRM.CRM_SalesMenTargetDetails.Generated;
using System.Net.Mail;

namespace Forwarding.MvcApp.Controllers.CRM.CRM_SalesMenTarget
{
    public class CRM_SalesMenTargetController : ApiController
    {
        //[Route("/api/CRM_SalesMenTarget/LoadAll")]
        [HttpGet, HttpPost]
        //sherif: to be used in select boxes
        public Object[] LoadAll(string pWhereClause)
        {
            CCRM_SalesMenTarget objCCRM_SalesMenTarget = new CCRM_SalesMenTarget();
            objCCRM_SalesMenTarget.GetList(pWhereClause);
            return new Object[] { new JavaScriptSerializer().Serialize(objCCRM_SalesMenTarget.lstCVarCRM_SalesMenTarget) };
        }

        
        [HttpGet, HttpPost]
        public Object[] GetDetailsData(int pID)
        {
            CCRM_SalesMenTargetDetails objCCRM_SalesMenTargetDetails = new CCRM_SalesMenTargetDetails();
            objCCRM_SalesMenTargetDetails.GetList(" Where CRM_SalesMenTargetID = "+ pID);
            return new Object[] { new JavaScriptSerializer().Serialize(objCCRM_SalesMenTargetDetails.lstCVarCRM_SalesMenTargetDetails) };
        }


        [HttpGet, HttpPost]
        //sherif: to be used in select boxes
        public Object[] IntializeData()
        {
            CUsers cUsers = new CUsers();
            cUsers.GetList("where IsNull(CustomerID , 0) = 0 AND 1 = 1");

            return new Object[] {
                new JavaScriptSerializer().Serialize(cUsers.lstCVarUsers) 
                };
        }


        


               [HttpGet, HttpPost]
        //sherif: to be used in select boxes
        public Object[] LoadActions(string pWhereClause)
        {
            //CCRM_Actions objActions = new CCRM_Actions();
            CCRM_Actions cActions = new CCRM_Actions();
            //CCRM_ActionStatues cCRM_ActionStatues = new CCRM_ActionStatues(); 
            ////--------------------------------------------
            //objActions.GetList("where 1 = 1");
            cActions.GetList(pWhereClause);
            //cCRM_ActionStatues.GetList("where 1 = 1");

            ////-------------------------------------------

            return new Object[] 
            {
                new JavaScriptSerializer().Serialize(cActions.lstCVarCRM_Actions)
                };
        }




        // [Route("/api/CRM_SalesMenTarget/LoadWithPaging/{pPageNumber}/{pPageSize}")]
        [HttpGet, HttpPost]
        public Object[] LoadWithPaging(Int32 pPageNumber, Int32 pPageSize, string pSearchKey)
        {
            CCRM_SalesMenTarget objCvwCRM_SalesMenTarget = new CCRM_SalesMenTarget();
            //objCvwCRM_SalesMenTarget.GetList(string.Empty);
            Int32 _RowCount = 0;// objCvwCRM_SalesMenTarget.lstCVarCRM_SalesMenTarget.Count;
            
            pSearchKey = (pSearchKey == null ? "" : pSearchKey.Trim().ToUpper());
            string whereClause = " Where Code LIKE '%" + pSearchKey + "%' "
                + " OR Name LIKE '%" + pSearchKey + "%' ";

            objCvwCRM_SalesMenTarget.GetListPaging(pPageSize, pPageNumber, whereClause, " Name ", out _RowCount);

            CCRM_Actions objCCRM_Actions = new CCRM_Actions();
            objCCRM_Actions.GetList(" Where 1=1");

            return new Object[] { new JavaScriptSerializer().Serialize(objCvwCRM_SalesMenTarget.lstCVarCRM_SalesMenTarget), _RowCount
            ,new JavaScriptSerializer().Serialize(objCCRM_Actions.lstCVarCRM_Actions)};
        }

        // [Route("/api/CRM_SalesMenTarget/Insert/{pCode}/{pModificatorUser}/{pLocalName}}")]

        [HttpGet, HttpPost]
        //public async System.Threading.Tasks.Task<bool> Insert(
        public bool Insert(
           string pID, string pSalesRepID, string pActionTypeID, string pWeekendDays, string pVacationsCount, string pNotes,
           string pDetailsID, string pFromDate, string pToDate, string pTarget, string pPerDay,
           string pIsActionsTarget, string pForMonth, string pForPeriod, string pTotalTarget,
           string pNotesDetails, string pTargetPeriod, string pActionID)
        {
            bool _result = false;
            Exception checkException1 = null;

            if (!pWeekendDays.Contains("-"))
            {
                pWeekendDays = pWeekendDays + "-" + " ";

            }
            CVarCRM_SalesMenTarget objCVarCRM_SalesMenTarget = new CVarCRM_SalesMenTarget();
            objCVarCRM_SalesMenTarget.ID = int.Parse(pID);
            objCVarCRM_SalesMenTarget.CreationDate = DateTime.Now;
            objCVarCRM_SalesMenTarget.CreatorUserID = WebSecurity.CurrentUserId;
            objCVarCRM_SalesMenTarget.ModificationDate = DateTime.Now;
            objCVarCRM_SalesMenTarget.ModifatorUserID = WebSecurity.CurrentUserId;

            objCVarCRM_SalesMenTarget.SalesRepID = pSalesRepID == null ? 0 : int.Parse(pSalesRepID);
            objCVarCRM_SalesMenTarget.ActionTypeID = pActionTypeID == null ? 0 : int.Parse(pActionTypeID);
            objCVarCRM_SalesMenTarget.WeekendDays = pWeekendDays == null ? "0" : pWeekendDays;
            objCVarCRM_SalesMenTarget.VacationsCount = pVacationsCount == null ? 0 : int.Parse(pVacationsCount);
            objCVarCRM_SalesMenTarget.Notes = pNotes == null ? "0" : pNotes.ToUpper();




            CCRM_SalesMenTarget objCCRM_SalesMenTarget = new CCRM_SalesMenTarget();
            objCCRM_SalesMenTarget.lstCVarCRM_SalesMenTarget.Add(objCVarCRM_SalesMenTarget);
            Exception checkException = objCCRM_SalesMenTarget.SaveMethod(objCCRM_SalesMenTarget.lstCVarCRM_SalesMenTarget);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
                _result = true;

            if (pActionID != null)
            {
                string[] arrActionID = pActionID.Split(',');//                 
                string[] arrpForPeriod = pForPeriod.Split(',');
                string[] arrpForMonth = pForMonth.Split(',');
                string[] arrpPerDay = pPerDay.Split(',');
                string[] arrpNotesDetails = pNotesDetails.Split(',');
                string[] arrpIsActionsTarget = pIsActionsTarget.Split(',');
                string[] arrpTotalTarget = pTotalTarget.Split(',');
                string[] arrpTargetPeriod = pTargetPeriod.Split(',');
                string[] arrpTarget = pTarget.Split(',');
                string[] arrpFromDate = pFromDate.Split(',');
                string[] arrpToDate = pToDate.Split(',');
                string[] arrpDetailsID = pDetailsID.Split(',');
                string[] arrpActionID = pActionID.Split(',');
                CCRM_SalesMenTargetDetails objCCRM_SalesMenTargetDetails = new CCRM_SalesMenTargetDetails();
                if (_result == true)
                {
                    int NumOfRows = pDetailsID.Split(',').Length;

                    for (int i = 0; i < NumOfRows; i++)
                    {
                        CVarCRM_SalesMenTargetDetails objCVarCRM_SalesMenTargetDetails = new CVarCRM_SalesMenTargetDetails();
                        try
                        {


                            objCVarCRM_SalesMenTargetDetails.ID = int.Parse(arrpDetailsID[i]);
                            objCVarCRM_SalesMenTargetDetails.FromDate = arrpFromDate[i] == null ? DateTime.ParseExact("01/01/1900" + " 00:00:00.000", "MM/dd/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture) : DateTime.ParseExact(arrpFromDate[i] + " 00:00:00.000", "MM/dd/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);
                            objCVarCRM_SalesMenTargetDetails.ToDate = arrpToDate[i] == null ? DateTime.ParseExact("01/01/1900" + " 00:00:00.000", "MM/dd/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture) : DateTime.ParseExact(arrpToDate[i] + " 23:59:59.000", "MM/dd/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);
                            objCVarCRM_SalesMenTargetDetails.Target = arrpTarget[i] == null ? 0 : int.Parse(arrpTarget[i]);
                            objCVarCRM_SalesMenTargetDetails.TargetPeriod = arrpTargetPeriod[i] == null ? 0 : int.Parse(arrpTargetPeriod[i]);
                            objCVarCRM_SalesMenTargetDetails.CRM_SalesMenTargetID = objCVarCRM_SalesMenTarget.ID;//pCRM_SalesMenTargetID == null ? 0 : int.Parse(pCRM_SalesMenTargetID); This id of the header
                            objCVarCRM_SalesMenTargetDetails.TotalTarget = arrpTotalTarget[i] == null ? 0 : int.Parse(arrpTotalTarget[i]);
                            objCVarCRM_SalesMenTargetDetails.IsActionsTarget = arrpIsActionsTarget[0] == null ? true : Convert.ToBoolean(arrpIsActionsTarget[0]);
                            objCVarCRM_SalesMenTargetDetails.Amount = 0;
                            objCVarCRM_SalesMenTargetDetails.AllAmount = 0;
                            objCVarCRM_SalesMenTargetDetails.Notes = arrpNotesDetails[i] == null ? "0" : arrpNotesDetails[i];
                            objCVarCRM_SalesMenTargetDetails.PerDay = arrpPerDay[i] == null ? 0 : (arrpPerDay[i] == "" ? 0 : decimal.Parse(arrpPerDay[i]));
                            objCVarCRM_SalesMenTargetDetails.PerMonth = arrpForMonth[i] == null ? 0 : (arrpForMonth[i] == "" ? 0 : decimal.Parse(arrpForMonth[i]));
                            objCVarCRM_SalesMenTargetDetails.DaysCount = arrpForPeriod[i] == null ? 0 : (arrpForPeriod[i] == "" ? 0 : int.Parse(arrpForPeriod[i]));
                            objCVarCRM_SalesMenTargetDetails.CreationDate = DateTime.Now;
                            objCVarCRM_SalesMenTargetDetails.CreatorUserID = WebSecurity.CurrentUserId;
                            objCVarCRM_SalesMenTargetDetails.ModificationDate = DateTime.Now;
                            objCVarCRM_SalesMenTargetDetails.ModifatorUserID = WebSecurity.CurrentUserId;
                            objCVarCRM_SalesMenTargetDetails.ActionID = int.Parse(arrpActionID[i]);
                            objCCRM_SalesMenTargetDetails.lstCVarCRM_SalesMenTargetDetails.Add(objCVarCRM_SalesMenTargetDetails);
                        }
                        catch (Exception ex)
                        { }
                    }
                    checkException1 = objCCRM_SalesMenTargetDetails.SaveMethod(objCCRM_SalesMenTargetDetails.lstCVarCRM_SalesMenTargetDetails);
                    if (checkException1 != null) // an exception is caught in the model
                    {
                        _result = false;
                    }
                    else
                        _result = true;

                }
                if (_result)
                {
                    var SalesMenTargetDetails = objCCRM_SalesMenTargetDetails.lstCVarCRM_SalesMenTargetDetails;
                    var SalesMenTargetDetailsIDs = "";
                    for (int i = 0; i < SalesMenTargetDetails.Count; i++)
                    {
                        SalesMenTargetDetailsIDs += SalesMenTargetDetails[i].ID + ",";
                    }

                    CCRM_SalesMenTargetDetails CRM_SalesMenTargetDetails = new CCRM_SalesMenTargetDetails();
                    CRM_SalesMenTargetDetails.GetList(" Where CRM_SalesMenTargetID = " + objCVarCRM_SalesMenTarget.ID + " AND ID not in (" + SalesMenTargetDetailsIDs.Substring(0, (SalesMenTargetDetailsIDs.Length - 1)) + ")");
                    CCRM_SalesMenTargetDetails objCRM_SalesMenTargetDetailsDelete = new CCRM_SalesMenTargetDetails();

                    foreach (var currentID in CRM_SalesMenTargetDetails.lstCVarCRM_SalesMenTargetDetails)
                    {
                        objCRM_SalesMenTargetDetailsDelete.lstDeletedCPKCRM_SalesMenTargetDetails.Add(new CPKCRM_SalesMenTargetDetails() { ID = currentID.ID });
                    }

                    Exception checkExceptionDelete = objCRM_SalesMenTargetDetailsDelete.DeleteItem(objCRM_SalesMenTargetDetailsDelete.lstDeletedCPKCRM_SalesMenTargetDetails);



                }
            }

            
            //#region Send Email
            ////if (_result) //Send an E-mail Sherif
            ////try
            ////{
            ////    string FromMail = "noreply-Rename@istegy.com";
            ////    bool _boolEmailFound = false;
            ////        SmtpClient SmtpServer = new SmtpClient();
            ////        MailMessage mail = new MailMessage();

            ////    SmtpServer.UseDefaultCredentials = true;
            ////    mail.From = new MailAddress(FromMail);

            ////    _boolEmailFound = true;
            ////    mail.To.Add("mohamedkewila94@gmail.com");

            ////    mail.Subject = "subject Sherif ooooooooooo";
            ////    mail.Body = "body Sherif ooooooooooo";

            ////    SmtpServer.Host = "smtpout.secureserver.net";
            ////    SmtpServer.Credentials = new System.Net.NetworkCredential(FromMail, "N@123456");
            ////    SmtpServer.EnableSsl = true;//false
            ////    if (_boolEmailFound)
            ////        try
            ////        {
            ////            SmtpServer.Send(mail);
            ////        }
            ////        catch
            ////        {
            ////        }
            ////}
            ////catch (Exception ex)
            ////{
            ////    //_result = false;
            ////}
            ////=================================================
            //try
            //{
            //    MailMessage message = new System.Net.Mail.MailMessage("noreply-Rename@istegy.com", "mohamedkewila94@gmail.com");
            //    string fromEmail = "noreply-Rename@istegy.com";
            //    string fromPW = "mypw";
            //    string toEmail = "mohamedkewila94@gmail.com";
            //    message.From = new MailAddress(fromEmail);
            //    message.To.Add(toEmail);
            //    message.Subject = "Hellooooooooooo";
            //    message.Body = "Helloooo Bob ";
            //    message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            //    //using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
            //    SmtpClient mySmtpClient = new SmtpClient("my.smtp.exampleserver.net");
            //    {
            //        mySmtpClient.EnableSsl = true;
            //        mySmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //        mySmtpClient.UseDefaultCredentials = false;
            //        mySmtpClient.Credentials = new System.Net.NetworkCredential(fromEmail, "N@123456"); //new NetworkCredential(fromEmail, fromPW);

            //        mySmtpClient.Send(message.From.ToString(), message.To.ToString(),
            //                        message.Subject, message.Body);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //_result = false;
            //}
            //#endregion Send Email



            //try
            //{
            //    string FromMail = "noreply-Rename@istegy.com";
            //    bool _boolEmailFound = false;
            //    MailMessage mail = new MailMessage();
            //    SmtpClient SmtpServer = new SmtpClient();
            //    SmtpServer.UseDefaultCredentials = true;
            //    mail.From = new MailAddress(FromMail);

            //    _boolEmailFound = true;
            //    mail.To.Add("mohamedkewila94@gmail.com");

            //    //mail.CC.Add(CC);
            //    mail.Subject = "subject";
            //    mail.Body = "body";
            //    //mail.Attachments.Add(new Attachment("C:\\Users\\Sherif\\Desktop\\CompanyLogo.jpg"));
            //    //SmtpServer.Port = 25;
            //    //SmtpServer.Port = Convert.ToInt32(ConfigurationSettings.AppSettings["PORT"].ToString());
            //    //SmtpServer.Port = Convert.ToInt32(Configuration!System.Configuration.ConfigurationManager.AppSettings["PORT"].ToString());
            //    SmtpServer.Host = "smtpout.secureserver.net";
            //    SmtpServer.Credentials = new System.Net.NetworkCredential(FromMail, "N@123456");
            //    SmtpServer.EnableSsl = true;//false
            //    if (_boolEmailFound)
            //        try
            //        {
            //            SmtpServer.Send(mail);
            //        }
            //        catch
            //        {
            //        }
            //}
            //catch (Exception ex)
            //{
            //    _result = false;
            //}




            //{ //Outlook kelany

            //    string recipient = "mohamedkewila94@gmail.com";
            //    string subject = "Sending Mail";
            //    string body1 = "http://forwardingist.istegy.com/";
            //    try
            //    {
            //        var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            //        var message = new MailMessage();
            //        message.To.Add(new MailAddress("mohamedkewila94@gmail.com"));  // replace with valid value 
            //        message.From = new MailAddress("mohamedkewila_2419@outlook.com");  // replace with valid value
            //        message.Subject = "Your email subject";
            //        message.Body = string.Format(body, "Mohamed Kewila", "mohamedkewila_2419@outlook.com", "http://forwardingist.istegy.com/");
            //        message.IsBodyHtml = true;

            //        using (var smtp = new SmtpClient())
            //        {
            //            var credential = new NetworkCredential
            //            {
            //                UserName = "mohamedkewila_2419@outlook.com",  // replace with valid value
            //                Password = "mbakcisasu01114513549"  // replace with valid value
            //            };
            //            smtp.Credentials = credential;
            //            smtp.Host = "smtp-mail.outlook.com";
            //            smtp.Port = 587;
            //            smtp.EnableSsl = true;
            //            await smtp.SendMailAsync(message);
            //            _result = true;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        _result = false;
            //    }

            //}

            return _result;
        }

        // [Route("/api/CRM_SalesMenTarget/Update/{pID}/{pCode}/{pName}/{pLocalName}")]
        [HttpGet, HttpPost]
        public bool Update(string pID  ,   string pSalesRepID,
       string pActionTypeID,
       string pWeekendDays,
       string pVacationsCount,
       string pNotes)
        {
            bool _result = false;


            if(!pWeekendDays.Contains("-"))
            {
                pWeekendDays = pWeekendDays + "-" + " ";

            }


            CVarCRM_SalesMenTarget objCVarCRM_SalesMenTarget = new CVarCRM_SalesMenTarget();

            //the next 4 lines to get the CreatorUserID and CreationDate and set them as they were entered before
            CCRM_SalesMenTarget objCGetCreationInformation = new CCRM_SalesMenTarget();
            objCGetCreationInformation.GetItem(int.Parse(pID));
            objCVarCRM_SalesMenTarget.CreatorUserID = objCGetCreationInformation.lstCVarCRM_SalesMenTarget[0].CreatorUserID;
            objCVarCRM_SalesMenTarget.CreationDate = objCGetCreationInformation.lstCVarCRM_SalesMenTarget[0].CreationDate;
                
            objCVarCRM_SalesMenTarget.ID = int.Parse( pID );

            objCVarCRM_SalesMenTarget.SalesRepID = pSalesRepID == null ? 0 : int.Parse(pSalesRepID);
            objCVarCRM_SalesMenTarget.ActionTypeID = pActionTypeID == null ? 0 : int.Parse(pActionTypeID);
            objCVarCRM_SalesMenTarget.WeekendDays = pWeekendDays == null ? "0" : pWeekendDays;
            objCVarCRM_SalesMenTarget.VacationsCount = pVacationsCount == null ? 0 : int.Parse(pVacationsCount);
            objCVarCRM_SalesMenTarget.Notes = pNotes == null ? "0" : pNotes.ToUpper();




            objCVarCRM_SalesMenTarget.ModifatorUserID = WebSecurity.CurrentUserId;
            objCVarCRM_SalesMenTarget.ModificationDate = DateTime.Now;

            CCRM_SalesMenTarget objCCRM_SalesMenTarget = new CCRM_SalesMenTarget();
            objCCRM_SalesMenTarget.lstCVarCRM_SalesMenTarget.Add(objCVarCRM_SalesMenTarget);
            Exception checkException = objCCRM_SalesMenTarget.SaveMethod(objCCRM_SalesMenTarget.lstCVarCRM_SalesMenTarget);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
                _result = true;
            return _result;
        }

        // [Route("/api/CRM_SalesMenTarget/DeleteByID/{pID}")]
        //[HttpGet, HttpPost]
        //public void DeleteByID(Int32 pID)
        //{
        //    CCRM_SalesMenTarget objCCRM_SalesMenTarget = new CCRM_SalesMenTarget();
        //    objCCRM_SalesMenTarget.lstDeletedCPKCRM_SalesMenTarget.Add(new CPKCRM_SalesMenTarget() { ID = pID });
        //    objCCRM_SalesMenTarget.DeleteItem(objCCRM_SalesMenTarget.lstDeletedCPKCRM_SalesMenTarget);
        //}

        // [Route("api/CRM_SalesMenTarget/Delete/{pCRM_SalesMenTargetIDs}")]
        [HttpGet, HttpPost]
        public bool Delete(String pCRM_SalesMenTargetIDs)
        {
            bool _result = false;
            CCRM_SalesMenTarget objCCRM_SalesMenTarget = new CCRM_SalesMenTarget();
            foreach (var currentID in pCRM_SalesMenTargetIDs.Split(','))
            {
                objCCRM_SalesMenTarget.lstDeletedCPKCRM_SalesMenTarget.Add(new CPKCRM_SalesMenTarget() { ID = Int32.Parse(currentID.Trim()) });
            }

            Exception checkException = objCCRM_SalesMenTarget.DeleteItem(objCCRM_SalesMenTarget.lstDeletedCPKCRM_SalesMenTarget);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("DELETE")) // some or all of the rows were not deleted due to dependencies
                    _result = false;
            }
            else //deleted successfully
                _result = true;
            return _result;
        }
    }
}
