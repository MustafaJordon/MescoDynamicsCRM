using Forwarding.MvcApp.Entities.Operations;
using Forwarding.MvcApp.Models.Accounting.MasterData.Generated;
using Forwarding.MvcApp.Models.Accounting.Transactions.Generated;
using Forwarding.MvcApp.Models.Administration.Settings.Generated;
using Forwarding.MvcApp.Models.Customized;
using Forwarding.MvcApp.Models.ReceiptsAndPayments.YardLinkTank.Customized;
using Forwarding.MvcApp.Models.ReceiptsAndPayments.YardLinkTank.Generated;

using Forwarding.MvcApp.Models.MasterData.BanksAccountsAndTreasuries.Generated;
using Forwarding.MvcApp.Models.MasterData.Others.Generated;
using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;

using Forwarding.MvcApp.Models.ReceiptsAndPayments.Transactions.Generated;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebMatrix.WebData;
using Forwarding.MvcApp.Models.Operations.Operations.Generated;
using Forwarding.MvcApp.Models.CustomizedSPCall;
using Forwarding.MvcApp.Models.ReceiptsAndPayments.ShipLink.Generated;

namespace Forwarding.MvcApp.Controllers.ReceiptsAndPayments.YardLinkTank
{
    public class YardLinkTankCreditPostingController : ApiController
    {
        [HttpGet, HttpPost]
        public object[] LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned(bool pIsLoadArrayOfObjects, string pLanguage, Int32 pPageNumber, Int32 pPageSize, string pWhereClause, string pOrderBy)
        {
            int _RowCount = 0;
            CvwA_Accounts objCvwA_Accounts = new CvwA_Accounts();
            CvwSL_InvoiceType objCInvoiceType = new CvwSL_InvoiceType();
            CTreasury objCSafes = new CTreasury();
            CvwVessels objCvwVessel = new CvwVessels();

            if (pIsLoadArrayOfObjects)
            {
                objCvwA_Accounts.GetListPaging(9999, 1, "WHERE IsMain=0", "Name, Code", out _RowCount);
                objCInvoiceType.GetList("WHERE 1=1 ORDER BY Name");
                objCSafes.GetList("WHERE 1=1 ORDER BY Name");
                objCvwVessel.GetList("WHERE 1=1 ORDER BY Name");
            }
            CvwCreditHeaderYardLinkTank objCvwSL_InvoiceHeader_SelectNotPosted = new CvwCreditHeaderYardLinkTank();
            objCvwSL_InvoiceHeader_SelectNotPosted.GetListPaging(pPageSize, pPageNumber, pWhereClause, pOrderBy, out _RowCount);

            return new object[] {
                new JavaScriptSerializer().Serialize(objCvwSL_InvoiceHeader_SelectNotPosted.lstCVarvwCreditHeaderYardLinkTank)
                , _RowCount
                , new JavaScriptSerializer().Serialize(objCvwA_Accounts.lstCVarvwA_Accounts)
                , new JavaScriptSerializer().Serialize(objCInvoiceType.lstCVarvwSL_InvoiceType) //pInvoiceType = pData[3]
                , new JavaScriptSerializer().Serialize(objCSafes.lstCVarTreasury) //pSafe = pData[4]
                , new JavaScriptSerializer().Serialize(objCvwVessel.lstCVarvwVessels) //pVessel = pData[5]
            };
        }

        [HttpGet, HttpPost]
        public object[] GetRevenueItemsModalData(string pSelectedIDsToGetRevenueItems)
        {
            Exception checkException = new Exception();
            string ShipLinkItemsIDs = "0,-1"; //will hold the IDs to be retrieved for the RevenueItems
            int pVoyageAccountID = 0;
            int _RowCount = 0;
            CvwLinkedSubAccounts objCvwLinkedSubAccounts = new CvwLinkedSubAccounts();
            CvwSL_InvoiceDetailsANDInvDetailsDemStrExtra_ShippingItemsIDs objCvwSL_InvoiceDetailsANDInvDetailsDemStrExtra_ShippingItemsIDs = new CvwSL_InvoiceDetailsANDInvDetailsDemStrExtra_ShippingItemsIDs();
            checkException = objCvwSL_InvoiceDetailsANDInvDetailsDemStrExtra_ShippingItemsIDs.GetList("WHERE InvoiceHeaderID IN (" + pSelectedIDsToGetRevenueItems + ")");
            CvwSL_ShippingItemsLinking objCvwSL_ShippingItemsLinking = new CvwSL_ShippingItemsLinking();
            for (int i = 0; i < objCvwSL_InvoiceDetailsANDInvDetailsDemStrExtra_ShippingItemsIDs.lstCVarvwSL_InvoiceDetailsANDInvDetailsDemStrExtra_ShippingItemsIDs.Count; i++)
                ShipLinkItemsIDs += "," + objCvwSL_InvoiceDetailsANDInvDetailsDemStrExtra_ShippingItemsIDs.lstCVarvwSL_InvoiceDetailsANDInvDetailsDemStrExtra_ShippingItemsIDs[i].ItemID.ToString();
            if (ShipLinkItemsIDs != "")
                checkException = objCvwSL_ShippingItemsLinking.GetList("WHERE ShiplinkItemID IN (" + ShipLinkItemsIDs + ")");
            if (objCvwSL_ShippingItemsLinking.lstCVarvwSL_ShippingItemsLinking.Count > 0)
            {
                pVoyageAccountID = objCvwSL_ShippingItemsLinking.lstCVarvwSL_ShippingItemsLinking[0].VoyageAccountID;
                checkException = objCvwLinkedSubAccounts.GetListPaging(9999, 1, "WHERE IsMain=0 AND Account_ID=" + pVoyageAccountID, "Name, Code", out _RowCount);
            }
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                serializer.Serialize(objCvwSL_ShippingItemsLinking.lstCVarvwSL_ShippingItemsLinking)
                , pVoyageAccountID
                , serializer.Serialize(objCvwLinkedSubAccounts.lstCVarvwLinkedSubAccounts)
            };
        }

        [HttpGet, HttpPost]
        public object[] Post(string pPostedSelectedIDs, bool pPost)
        {
            Exception checkException = null;
            string strMessage = "";

            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();

            bool _result = false;
            //  checkException = objCA_Fiscal_Year_Period.GetList("WHERE '" + DateTime.ParseExact(pJVDate + " 00:00:00.000", "dd/MM/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture).ToString() + "' BETWEEN From_Date AND To_Date and Closed=1");
            if (pPost == false)
            {


                int NumberOfInvoicesDeleted = pPostedSelectedIDs.Split(',').Length;
                for (int i = 0; i < NumberOfInvoicesDeleted; i++)
                {
                    objCCustomizedDBCall.ExecuteQuery_DataTable("A_UnApproveCreditYardTank " + int.Parse(pPostedSelectedIDs.Split(',')[i]));
                }

            }
            else
            {
                if (strMessage == "")
                {
                    CvwStructure_SP_SL_Validate_ShippingLink_ERP_InvoicesBy_IDs objCvwStructure_SP_SL_Validate_ShippingLink_ERP_InvoicesBy_IDs = new CvwStructure_SP_SL_Validate_ShippingLink_ERP_InvoicesBy_IDs();
                    CJVDefaults objCJVDefaults = new CJVDefaults();
                    CvwDefaults objCvwDefaults = new CvwDefaults();
                    int CurrencyID = 0;
                    int _RowCount = 0;

                    decimal ExchangeRate = 0;
                    string UnEditableCompanyName;
                    var ArrSelectedIDs = pPostedSelectedIDs.Split(',');
                    // checkException = objCvwDefaults.GetList("WHERE 1=1");
                    objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount);
                    UnEditableCompanyName = objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName;
                    checkException = objCJVDefaults.GetList("WHERE TransTypeID IN (27)");
                    checkException = objCvwStructure_SP_SL_Validate_ShippingLink_ERP_InvoicesBy_IDs.GetListCreditDebit("," + pPostedSelectedIDs + ",");
                    string BranchID = objCCustomizedDBCall.CallStringFunction("SELECT u.BranchID as BranshID FROM  Users AS u WHERE u.ID= " + WebSecurity.CurrentUserId);


                    Cvw_ClientsIDByYardLinkTankCreditNoteID objCvw_ClientsIDByYardInvoiceID = new Cvw_ClientsIDByYardLinkTankCreditNoteID();
                    CvwCreditHeaderYardLinkTank objCvw_InvoiceHeaderYardByInvoiceID = new CvwCreditHeaderYardLinkTank();



                    #region Validate Linking
                    if (objCvwStructure_SP_SL_Validate_ShippingLink_ERP_InvoicesBy_IDs.lstCVarvwStructure_SP_SL_Validate_ShippingLink_ERP_InvoicesBy_IDs.Count > 0)
                        for (int j = 0; j < objCvwStructure_SP_SL_Validate_ShippingLink_ERP_InvoicesBy_IDs.lstCVarvwStructure_SP_SL_Validate_ShippingLink_ERP_InvoicesBy_IDs.Count; j++)
                        {
                            strMessage += (j + 1).ToString() + " - "
                                + objCvwStructure_SP_SL_Validate_ShippingLink_ERP_InvoicesBy_IDs.lstCVarvwStructure_SP_SL_Validate_ShippingLink_ERP_InvoicesBy_IDs[j].ItemTypeEn
                                + ": "
                                + objCvwStructure_SP_SL_Validate_ShippingLink_ERP_InvoicesBy_IDs.lstCVarvwStructure_SP_SL_Validate_ShippingLink_ERP_InvoicesBy_IDs[j].ItemName
                                + "\n";
                        }
                    #endregion Validate Linking
                    if (strMessage == "")
                    {

                        if (pPostedSelectedIDs != null)
                        {
                            int NumberOfInvoices = pPostedSelectedIDs.Split(',').Length;

                            for (int i = 0; i < NumberOfInvoices; i++)
                            {

                                objCvw_ClientsIDByYardInvoiceID.GetList("WHERE CreditNoteID =" + int.Parse(pPostedSelectedIDs.Split(',')[i]));

                                CLinkYardLinkTankForwCreditNote objCLinkYardForwInvoices2 = new CLinkYardLinkTankForwCreditNote();
                                objCLinkYardForwInvoices2.GetList("WHERE YardCreditID =" + int.Parse(pPostedSelectedIDs.Split(',')[i]));


                                if (objCLinkYardForwInvoices2.lstCVarLinkYardLinkTankForwCreditNote.Count == 0)
                                {

                                    #region invoice header
                                    objCvw_InvoiceHeaderYardByInvoiceID.GetList("WHERE ID =" + int.Parse(pPostedSelectedIDs.Split(',')[i]));
                                    CVarAccNote objCVarAccNote = new CVarAccNote();
                                    if (objCvw_InvoiceHeaderYardByInvoiceID.lstCVarvwCreditHeaderYardLinkTank.Count > 0 && strMessage == "")
                                    {
                                        #region Save InvoiceHeader

                                        objCVarAccNote.Code =objCvw_InvoiceHeaderYardByInvoiceID.lstCVarvwCreditHeaderYardLinkTank[0].CrdtSerial.ToString();
                                        objCVarAccNote.NoteType = 100;
                                        objCVarAccNote.NoteDate = objCvw_InvoiceHeaderYardByInvoiceID.lstCVarvwCreditHeaderYardLinkTank[0].CrdtDate;
                                        objCVarAccNote.OperationID = objCvw_InvoiceHeaderYardByInvoiceID.lstCVarvwCreditHeaderYardLinkTank[0].OperationID;
                                        objCVarAccNote.OperationPartnerID = objCvw_InvoiceHeaderYardByInvoiceID.lstCVarvwCreditHeaderYardLinkTank[0].OperationPartnerID;
                                        objCVarAccNote.InvoiceID = objCvw_InvoiceHeaderYardByInvoiceID.lstCVarvwCreditHeaderYardLinkTank[0].ForInvoiceID;
                                        objCVarAccNote.AddressID = objCvw_InvoiceHeaderYardByInvoiceID.lstCVarvwCreditHeaderYardLinkTank[0].AddressID;
                                        objCVarAccNote.PrintedAddress = "0";
                                        objCVarAccNote.CurrencyID = objCvw_InvoiceHeaderYardByInvoiceID.lstCVarvwCreditHeaderYardLinkTank[0].CurrencyID;
                                        objCVarAccNote.ExchangeRate = objCvw_InvoiceHeaderYardByInvoiceID.lstCVarvwCreditHeaderYardLinkTank[0].Exchangerate;
                                        objCVarAccNote.AmountWithoutVAT = objCvw_InvoiceHeaderYardByInvoiceID.lstCVarvwCreditHeaderYardLinkTank[0].Total;
                                        objCVarAccNote.TaxTypeID = objCvw_InvoiceHeaderYardByInvoiceID.lstCVarvwCreditHeaderYardLinkTank[0].CrdtSalaTax >0 ? 2 :0;
                                        objCVarAccNote.TaxPercentage = objCvw_InvoiceHeaderYardByInvoiceID.lstCVarvwCreditHeaderYardLinkTank[0].CrdtSalaTax > 0 ? 14 : 0;
                                        objCVarAccNote.TaxAmount = objCvw_InvoiceHeaderYardByInvoiceID.lstCVarvwCreditHeaderYardLinkTank[0].CrdtSalaTax;
                                        objCVarAccNote.DiscountTypeID = 0;
                                        objCVarAccNote.DiscountPercentage = 0;
                                        objCVarAccNote.DiscountAmount = 0;
                                        objCVarAccNote.Amount = objCvw_InvoiceHeaderYardByInvoiceID.lstCVarvwCreditHeaderYardLinkTank[0].TotalAfterTax;
                                        //objCVarAccNote.PaidAmount = pPaidAmount;
                                        //objCVarAccNote.RemainingAmount = pRemainingAmount;
                                        objCVarAccNote.NoteStatusID = 1;
                                        objCVarAccNote.Remarks = "0";
                                        objCVarAccNote.CreatorUserID = objCVarAccNote.ModificatorUserID = WebSecurity.CurrentUserId;
                                        objCVarAccNote.CreationDate = objCVarAccNote.ModificationDate = DateTime.Now;
                                        CAccNote objCAccNote = new CAccNote();
                                        objCAccNote.lstCVarAccNote.Add(objCVarAccNote);
                                        checkException = objCAccNote.SaveMethod(objCAccNote.lstCVarAccNote);
                                        if (checkException != null) // an exception is caught in the model
                                        {
                                            strMessage = checkException.Message;
                                            _result = false;
                                        }
                                        #endregion Save InvoiceHeader
                                    }

                                    #endregion

                                    #region recivale
                                    Cvw_CreditHeaderDetailsYardLinkTankByInvoiceID objCvw_InvoiceHeaderDetailsYardByInvoiceID = new Cvw_CreditHeaderDetailsYardLinkTankByInvoiceID();
                                    objCvw_InvoiceHeaderDetailsYardByInvoiceID.GetList("WHERE CreditID =" + int.Parse(pPostedSelectedIDs.Split(',')[i]));
                                    if (objCvw_InvoiceHeaderDetailsYardByInvoiceID.lstCVarvw_CreditHeaderDetailsYardLinkTankByInvoiceID.Count > 0 && strMessage == "")
                                    {
                                        for (int y = 0; y < objCvw_InvoiceHeaderDetailsYardByInvoiceID.lstCVarvw_CreditHeaderDetailsYardLinkTankByInvoiceID.Count; y++)
                                        {

                                            CVarPayables objCVarPayables = new CVarPayables();
                                            //the next 4 lines to get the CreatorUserID and CreationDate and set them as they were entered before
                                            objCVarPayables.CreatorUserID = objCVarAccNote.ModificatorUserID = WebSecurity.CurrentUserId;
                                            objCVarPayables.CreationDate = objCvw_InvoiceHeaderYardByInvoiceID.lstCVarvwCreditHeaderYardLinkTank[0].CrdtDate;
                                            objCVarPayables.GeneratingQRID = 0;
                                            objCVarPayables.ApprovingUserID = 0;
                                            objCVarPayables.CustodyID = 0;
                                            objCVarPayables.SupplierReceiptNo = "0";
                                            objCVarPayables.PaidAmount = 0;
                                            objCVarPayables.RemainingAmount = objCvw_InvoiceHeaderDetailsYardByInvoiceID.lstCVarvw_CreditHeaderDetailsYardLinkTankByInvoiceID[y].InvoiceItemsTotal;
                                            objCVarPayables.AccNoteID = objCVarAccNote.ID;
                                            objCVarPayables.JVID = 0;
                                            objCVarPayables.JVID2 = 0;
                                            objCVarPayables.TransactionID = 0;
                                            objCVarPayables.QuotationCost = 0;
                                            objCVarPayables.IsNeglectLimit = false;
                                            objCVarPayables.OfficialAmountPaid = 0;
                                            objCVarPayables.PricingID = 0;
                                            objCVarPayables.OperationVehicleID = 0;
                                            objCVarPayables.OperationContainersAndPackagesID = 0;
                                            objCVarPayables.TruckingOrderID = 0;


                                            objCVarPayables.IsApproved = false;
                                          
                                            objCVarPayables.ID = 0;
                                            objCVarPayables.OperationID = objCvw_InvoiceHeaderYardByInvoiceID.lstCVarvwCreditHeaderYardLinkTank[0].OperationID;
                                            objCVarPayables.ChargeTypeID = objCvw_InvoiceHeaderDetailsYardByInvoiceID.lstCVarvw_CreditHeaderDetailsYardLinkTankByInvoiceID[y].ChargeTypeID;
                                            objCVarPayables.POrC = 0;
                                            objCVarPayables.ContainerTypeID = 0;
                                            objCVarPayables.MeasurementID = objCvw_InvoiceHeaderDetailsYardByInvoiceID.lstCVarvw_CreditHeaderDetailsYardLinkTankByInvoiceID[y].MeasurementID;
                                            objCVarPayables.SupplierOperationPartnerID = objCvw_InvoiceHeaderYardByInvoiceID.lstCVarvwCreditHeaderYardLinkTank[0].OperationPartnerID;
                                            objCVarPayables.Quantity = 1;
                                            objCVarPayables.CostPrice = objCvw_InvoiceHeaderDetailsYardByInvoiceID.lstCVarvw_CreditHeaderDetailsYardLinkTankByInvoiceID[y].InvoiceItemsTotal;

                                            objCVarPayables.AmountWithoutVAT = objCvw_InvoiceHeaderDetailsYardByInvoiceID.lstCVarvw_CreditHeaderDetailsYardLinkTankByInvoiceID[y].InvoiceItemsTotal;
                                            objCVarPayables.TaxTypeID = 0;
                                            objCVarPayables.TaxPercentage = 0;
                                            objCVarPayables.TaxAmount = 0;
                                            objCVarPayables.DiscountTypeID = 0;
                                            objCVarPayables.DiscountPercentage = 0;
                                            objCVarPayables.DiscountAmount = 0;

                                            objCVarPayables.CostAmount = objCvw_InvoiceHeaderDetailsYardByInvoiceID.lstCVarvw_CreditHeaderDetailsYardLinkTankByInvoiceID[y].InvoiceItemsTotal; //total w VAT and Discount
                                            objCVarPayables.InitialSalePrice = 0;
                                            objCVarPayables.SupplierInvoiceNo = "0";
                                            objCVarPayables.SupplierReceiptNo = "0";
                                            objCVarPayables.EntryDate = objCvw_InvoiceHeaderDetailsYardByInvoiceID.lstCVarvw_CreditHeaderDetailsYardLinkTankByInvoiceID[y].CreditIssueDate;
                                            objCVarPayables.BillID = 0;
                                            objCVarPayables.IssueDate = objCvw_InvoiceHeaderDetailsYardByInvoiceID.lstCVarvw_CreditHeaderDetailsYardLinkTankByInvoiceID[y].CreditIssueDate;

                                            objCVarPayables.ExchangeRate = objCvw_InvoiceHeaderYardByInvoiceID.lstCVarvwCreditHeaderYardLinkTank[0].Exchangerate;
                                            objCVarPayables.CurrencyID = objCvw_InvoiceHeaderYardByInvoiceID.lstCVarvwCreditHeaderYardLinkTank[0].CurrencyID;
                                            objCVarPayables.BillTo = 0;
                                            objCVarPayables.Notes = "";

                                            objCVarPayables.ModificatorUserID = WebSecurity.CurrentUserId;
                                            objCVarPayables.ModificationDate = DateTime.Now;

                                            objCVarPayables.SupplierSiteID = 0;

                                            CPayables objCPayables = new CPayables();
                                            objCPayables.lstCVarPayables.Add(objCVarPayables);
                                            checkException = objCPayables.SaveMethod(objCPayables.lstCVarPayables);
                                            if (checkException != null) // an exception is caught in the model
                                            {
                                                strMessage = checkException.Message;
                                                _result = false;
                                            }


                                        }
                                    }
                                    #endregion

                                    #region post 
                                    if (checkException == null && strMessage == "") // an exception is caught in the model
                                    {
                                        CVarLinkYardLinkTankForwCreditNote objvarCLinkYardForwInvoices = new CVarLinkYardLinkTankForwCreditNote();
                                        #region Link
                                        if (strMessage == "" && checkException == null)
                                        {


                                            objvarCLinkYardForwInvoices.ID = 0;
                                            objvarCLinkYardForwInvoices.ForwCreditID = Convert.ToInt32(objCVarAccNote.ID);
                                            objvarCLinkYardForwInvoices.YardCreditID = int.Parse(pPostedSelectedIDs.Split(',')[i]);
                                            objvarCLinkYardForwInvoices.UserID = WebSecurity.CurrentUserId;

                                            CLinkYardLinkTankForwCreditNote objCLinkYardForwInvoices = new CLinkYardLinkTankForwCreditNote();
                                            objCLinkYardForwInvoices.lstCVarLinkYardLinkTankForwCreditNote.Add(objvarCLinkYardForwInvoices);
                                            checkException = objCLinkYardForwInvoices.SaveMethod(objCLinkYardForwInvoices.lstCVarLinkYardLinkTankForwCreditNote);
                                            if (checkException != null) // an exception is caught in the model
                                            {
                                                strMessage = checkException.Message;
                                                _result = false;
                                            }
                                            else
                                            {
                                                _result = true;
                                            }
                                        }


                                        #endregion


                                        CCallCustomizedSP objCCallCustomizedSP = new CCallCustomizedSP();
                                        checkException = objCCallCustomizedSP.CallCustomizedSP("ERP_ForwWeb_PostingCreditNote", objCVarAccNote.ID , WebSecurity.CurrentUserId, pPost, 0);

                                        if (checkException == null) // an exception is caught in the model
                                        {
                                            string JVID = "";
                                            JVID = objCCustomizedDBCall.CallStringFunction("SELECT JVID FROM AccNote  WHERE ID = " + objCVarAccNote.ID);
                                            objCCustomizedDBCall.CallStringFunction("update YardTransOcean.dbo.CrdtNt set CrdtIsPosted=1, JVID=" + JVID + " WHERE ID = " + int.Parse(pPostedSelectedIDs.Split(',')[i]));
                                            _result = true;
                                        }
                                        else
                                        {
                                            _result = false;
                                            strMessage = checkException.Message;
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    CCallCustomizedSP objCCallCustomizedSP = new CCallCustomizedSP();
                                    checkException = objCCallCustomizedSP.CallCustomizedSP("ERP_ForwWeb_PostingCreditNote", objCLinkYardForwInvoices2.lstCVarLinkYardLinkTankForwCreditNote[0].ForwCreditID, WebSecurity.CurrentUserId, pPost, 0);

                                    if (checkException == null) // an exception is caught in the model
                                    {
                                        string JVID = "";
                                        JVID = objCCustomizedDBCall.CallStringFunction("SELECT JVID FROM AccNote  WHERE ID = " + objCLinkYardForwInvoices2.lstCVarLinkYardLinkTankForwCreditNote[0].ForwCreditID);
                                        objCCustomizedDBCall.CallStringFunction("update YardTransOcean.dbo.CrdtNt set CrdtIsPosted=1, JVID=" + JVID + " WHERE ID = " + int.Parse(pPostedSelectedIDs.Split(',')[i]));
                                        _result = true;
                                    }
                                    else
                                    {
                                        _result = false;
                                        strMessage = checkException.Message;
                                    }
                                }


                                //checkException = objCvwStructure_SP_SL_Validate_ShippingLink_ERP_InvoicesBy_IDs.GetListPosting( int.Parse(pPostedSelectedIDs.Split(',')[i]), WebSecurity.CurrentUserId, pPost);
                                //    if (checkException != null)
                                //    {
                                //        strMessage = checkException.Message;
                                //    }

                            }
                        }
                    }
                }
            }
            return new object[] {
                strMessage
            };
        }

    }
}



