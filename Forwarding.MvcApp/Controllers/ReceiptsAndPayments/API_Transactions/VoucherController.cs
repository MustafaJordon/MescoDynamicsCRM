using Forwarding.MvcApp.Models.Accounting.MasterData.Generated;
using Forwarding.MvcApp.Models.Accounting.Transactions.Generated;
using Forwarding.MvcApp.Models.Administration.Settings.Generated;
using Forwarding.MvcApp.Models.Customized;
using Forwarding.MvcApp.Models.MasterData.BanksAccountsAndTreasuries.Generated;
using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;
using Forwarding.MvcApp.Models.MasterData.Partners.Generated;
using Forwarding.MvcApp.Models.NoAccess.Generated;
using Forwarding.MvcApp.Models.OperAcc.Customized;
using Forwarding.MvcApp.Models.OperAcc.Generated;
using Forwarding.MvcApp.Models.Operations.Operations.Generated;
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

using Forwarding.MvcApp.Models.SC.MasterData.Generated;
using Forwarding.MvcApp.Models.SC.Transactions.Generated;
using Forwarding.MvcApp.Models.SC.Transactions.Customized;
using MoreLinq;
using Forwarding.MvcApp.Models.ReceiptsAndPayments.Transactions.Customized;
using LogisticsWeb;
using Forwarding.MvcApp.Models.ReceiptsAndPayments.Custodies.Generated;
using Forwarding.MvcApp.Models.Administration.DisbursementLink.Generated;
using Forwarding.MvcApp.Models.SL.SL_MasterData.Generated;
using Forwarding.MvcApp.Models.Accounting.MasterData.Customized;
using Forwarding.MvcApp.Models.Administration.Security.Generated;

namespace Forwarding.MvcApp.Controllers.ReceiptsAndPayments.API_Transactions
{
    public class VoucherController : ApiController
    {
        [HttpGet, HttpPost]
        public object[] FillCombo(Int32 PoperationID, Int32 ptype, string pOrderBy)
        {
            int _RowCount = 0;
            Exception checkException = null;

            CvwA_PaymentRequestHouse objCvwA_PaymentRequestHouse = new CvwA_PaymentRequestHouse();
            CvwA_PaymentRequestCertificateNumber objCvwA_PaymentRequestCertificateNumber = new CvwA_PaymentRequestCertificateNumber();
            CvwA_VoucherBranches objCCvwA_VoucherBranches = new CvwA_VoucherBranches();
            CvwA_PaymentRequestTruckingOrder objCCvwA_PaymentRequestTruckingOrder = new CvwA_PaymentRequestTruckingOrder();

            CvwDefaults objCvwDefaults = new CvwDefaults();
            objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa

            string CompanyName = objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName;
            if (ptype == 1)
            {
                if (PoperationID == 0)
                {
                    objCvwA_PaymentRequestHouse.GetListPaging(999999, 1, ("where MasterOperationID IS NOT NULL"), pOrderBy, out _RowCount);
                }
                else
                {
                    objCvwA_PaymentRequestHouse.GetListPaging(999999, 1, ("where MasterOperationID = " + PoperationID), pOrderBy, out _RowCount);
                }


                var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
                return new object[] {
                 serializer.Serialize(objCvwA_PaymentRequestHouse.lstCVarvwA_PaymentRequestHouse)//pData[0]
                };
            }
            //else if (ptype == 2)
            //{
            //    if (PoperationID == 0)
            //    {
            //        objCvwA_PaymentRequestCertificateNumber.GetListPaging(999999, 1, ("where 1=1"), pOrderBy, out _RowCount);
            //    }
            //    else
            //    {
            //        objCvwA_PaymentRequestCertificateNumber.GetListPaging(999999, 1, ("where OperationID = " + PoperationID), pOrderBy, out _RowCount);
            //    }

            //    var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            //    return new object[] {
            //     serializer.Serialize(objCvwA_PaymentRequestCertificateNumber.lstCVarvwA_PaymentRequestCertificateNumber)//pData[0]
            //    };

            //}
            else if (ptype == 2)
            {
                if (PoperationID == 0)
                {
                    objCCvwA_PaymentRequestTruckingOrder.GetListPaging(999999, 1, ("where 1=1"), pOrderBy, out _RowCount);
                }
                else
                {
                    objCCvwA_PaymentRequestTruckingOrder.GetListPaging(999999, 1, ("where OperationID = " + PoperationID), pOrderBy, out _RowCount);
                }

                var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
                return new object[] {
                 serializer.Serialize(objCCvwA_PaymentRequestTruckingOrder.lstCVarvwA_PaymentRequestTruckingOrder)//pData[0]
                };
            }
            else if (ptype == 3)
            {
                if (PoperationID == 0)
                {
                    if (CompanyName == "SAF")
                    {
                        objCCvwA_VoucherBranches.GetListPaging(999999, 1, ("where isnull(isDepartement,0)=1"), pOrderBy, out _RowCount);

                    }
                    else
                    {
                        objCCvwA_VoucherBranches.GetListPaging(999999, 1, ("where 1=1"), pOrderBy, out _RowCount);
                    }
                   
                }
                else
                {
                    if (CompanyName == "SAF")
                    {
                        objCCvwA_VoucherBranches.GetListPaging(999999, 1, ("where isnull(isDepartement,0)=1 and OperationID like  '%" + PoperationID + "%'"), pOrderBy, out _RowCount);

                    }
                    else
                    {
                        objCCvwA_VoucherBranches.GetListPaging(999999, 1, ("where OperationID like  '%" + PoperationID + "%'"), pOrderBy, out _RowCount);

                    }
                }

                var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
                return new object[] {
                 serializer.Serialize(objCCvwA_VoucherBranches.lstCVarvwA_VoucherBranches)//pData[0]
                };
            }
            else
            {
                var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
                return new object[] {
                _RowCount //pData[1]
                };
            }


        }
        [HttpGet, HttpPost]
        public Object[] GetOperationID(string TransID, Int32 ptype)
        {
            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
            string OperationID = "0";
            if (ptype == 1)
            {
                OperationID = objCCustomizedDBCall.CallStringFunction("SELECT TOP 1 o.MasterOperationID FROM Operations AS o WHERE  o.MasterOperationID IS NOT NULL AND o.ID = " + TransID);
            }
            else if (ptype == 2)
            {
                OperationID = objCCustomizedDBCall.CallStringFunction("SELECT TOP 1 R.OperationID FROM Routings AS r WHERE r.RoutingTypeID=60 AND r.ID= " + TransID);
            }
            else if (ptype == 3)
            {
                OperationID = objCCustomizedDBCall.CallStringFunction("SELECT top 1 o.ID FROM Operations AS o  WHERE o.BranchID= " + TransID);

            }

            return new Object[] { OperationID };

        }
        [HttpGet, HttpPost]
        public Object[] GetSalesIDBySupAccountID(string pSupAccountID)
        {
            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
            string SalesManID = "0";


            SalesManID = objCCustomizedDBCall.CallStringFunction("SELECT isnull(scsm.salesmanID,0) AS salesmanID FROM Customers AS c JOIN SL_CustomerSalesMan AS scsm ON scsm.ClientID = c.ID WHERE scsm.isDefault=1 AND c.SubAccountID= " + pSupAccountID);

            


            return new Object[] { SalesManID };

        }

        [HttpGet, HttpPost] 
        public object[] LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned(bool pIsLoadArrayOfObjects, string pLanguage, Int32 pPageNumber, Int32 pPageSize, string pWhereClause, string pOrderBy)
        {
            int _RowCount = 0;
            CvwA_Accounts objCvwA_Accounts = new CvwA_Accounts();
            CvwA_CostCenters objCvwA_CostCenters = new CvwA_CostCenters();
            CTreasury objCSafes = new CTreasury();
            CTaxeTypes objCTaxes = new CTaxeTypes();
            CBankAccount objCBank = new CBankAccount();
            CBranches objCBranches = new CBranches();
            CvwDefaults objCvwDefaults = new CvwDefaults();
            objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa

            string CompanyName = objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName;
            // CvwOperationsForCombo objCOperations = new CvwOperationsForCombo();
            CvwOperations_Vouchers objCOperations = new CvwOperations_Vouchers();
            string pSafesWhereClause = " WHERE 1=1 ";
            //LinkUserAndSafes
            CSystemOptions objCSystemOptions = new CSystemOptions();
            objCSystemOptions.GetItem(55);

            CDAS_vwDisbursementJobs objCvwA_DAS_DisbursementJobs = new CDAS_vwDisbursementJobs();

            if (objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName == "COS" || objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName == "MAR")
                objCvwA_DAS_DisbursementJobs.GetListPaging(9999, 1, "WHERE 1=1", "JobNumber", out _RowCount);

            CvwA_PaymentRequestHouse objCvwA_PaymentRequestHouse = new CvwA_PaymentRequestHouse();
            CvwA_PaymentRequestCertificateNumber objCvwA_PaymentRequestCertificateNumber = new CvwA_PaymentRequestCertificateNumber();
            CvwA_PaymentRequestTruckingOrder objCCvwA_PaymentRequestTruckingOrder = new CvwA_PaymentRequestTruckingOrder();
            CSL_SalesMan objCSalesMan = new CSL_SalesMan();

            CvwChargeTypesWithMinimalColumns objCvwChargeTypesWithMinimalColumns = new CvwChargeTypesWithMinimalColumns();

            if (objCSystemOptions.lstCVarSystemOptions[0].OptionValue)
            {
                // pWhereClause += " = " + WebSecurity.CurrentUserId;
                pWhereClause = " INNER JOIN VW_Sec_UserSafes US ON SafeID = US._SafeID " + pWhereClause + " AND US._UserID=" + WebSecurity.CurrentUserId;

                pSafesWhereClause = " JOIN VW_Sec_UserSafes USF ON ID =  USF._SafeID " + pSafesWhereClause + "  AND USF._UserID=" + WebSecurity.CurrentUserId;
            }

            
            CvwUserForms objCvwUserForms = new CvwUserForms();
            if (pIsLoadArrayOfObjects)
            {
                objCvwA_Accounts.GetListPaging(9999, 1, "WHERE IsMain=0", "Name, Code", out _RowCount);
                //objCvwA_CostCenters.GetListPaging(9999, 1, "WHERE IsMain=0", "Name, Code", out _RowCount);
                
                if (CompanyName == "BED")
                {
                    objCvwA_CostCenters.GetListPaging(9999, 1, "WHERE IsMain=0  and id NOT IN ( SELECT a.CostCenterID FROM A_LinkOperationWithCostCenter a JOIN Operations AS o ON o.ID=a.OperationID WHERE o.OperationStageID IN(110,120))", "Name, Code", out _RowCount);
                }
                else
                {
                    objCvwA_CostCenters.GetListPaging(9999, 1, "WHERE IsMain=0", "Name, Code", out _RowCount);
                }


                objCSafes.GetListPaging(9999, 1, pSafesWhereClause, "Name", out _RowCount);
                objCTaxes.GetListPaging(9999, 1, "WHERE 1=1", "Name", out _RowCount);
                objCBank.GetListPaging(9999, 1, "WHERE 1=1", "Name", out _RowCount);
                if (CompanyName == "SAF")
                {
                    objCBranches.GetListPaging(9999, 1, "WHERE isnull(isDepartement,0)=1", "Name", out _RowCount);

                }
                else
                {
                    objCBranches.GetListPaging(9999, 1, "WHERE 1=1", "Name", out _RowCount);
                }
                objCOperations.GetListPaging(999999, 1, "WHERE 1=1", "ID DESC", out _RowCount);
                // objCOperations.GetListPaging(999999, 1, "WHERE BLType<>2", "ID DESC", out _RowCount);
                objCvwA_PaymentRequestCertificateNumber.GetListPaging(999999, 1, "WHERE 1=1 AND Name IS NOT NULL", pOrderBy, out _RowCount);
                objCvwA_PaymentRequestHouse.GetListPaging(999999, 1, "WHERE 1=1 AND NAME IS NOT NULL AND MasterOperationID IS NOT null  ", pOrderBy, out _RowCount); // AND CreatorUserID = " + WebSecurity.CurrentUserId
                objCCvwA_PaymentRequestTruckingOrder.GetListPaging(999999, 1, "WHERE 1=1 AND Name IS NOT NULL", pOrderBy, out _RowCount);

                objCSalesMan.GetListPaging(9999, 1, "WHERE 1=1", "Name", out _RowCount);

                objCvwChargeTypesWithMinimalColumns.GetList("Where IsGeneralChargeType=1 ");

                objCvwUserForms.GetList("Where ImageName in ('CashIssueVoucher','CashReceivingVoucher','ChequeIssueVoucher','ChequeReceivingVoucher') and UserID= " + WebSecurity.CurrentUserId);

            }



            CvwA_Voucher objCvwA_Voucher = new CvwA_Voucher();
            objCvwA_Voucher.GetListPaging(pPageSize, pPageNumber, pWhereClause, pOrderBy, out _RowCount);

          

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };


            return new object[] {
                 serializer.Serialize(objCvwA_Voucher.lstCVarvwA_Voucher)
                , _RowCount
                , pIsLoadArrayOfObjects ? serializer.Serialize(objCvwA_Accounts.lstCVarvwA_Accounts) : null //pAccount = pData[2]
                , pIsLoadArrayOfObjects ? serializer.Serialize(objCSafes.lstCVarTreasury) : null //pSafe = pData[3]
                , pIsLoadArrayOfObjects ? serializer.Serialize(objCTaxes.lstCVarTaxeTypes) : null //pTax = pData[4]
                , pIsLoadArrayOfObjects ? serializer.Serialize(objCvwA_CostCenters.lstCVarvwA_CostCenters) : null //pCostCenter = pData[5]
                , pIsLoadArrayOfObjects ? serializer.Serialize(objCBank.lstCVarBankAccount) : null //pBank = pData[6]
                , pIsLoadArrayOfObjects ? serializer.Serialize(objCBranches.lstCVarBranches) : null //pBranches = pData[7]
                , pIsLoadArrayOfObjects ? serializer.Serialize(objCOperations.lstCVarvwOperations_Vouchers) : null //pOperations = pData[8]
                , pIsLoadArrayOfObjects ? serializer.Serialize(objCvwA_DAS_DisbursementJobs.lstCVarDAS_vwDisbursementJobs) : null //pOperations = pData[9]
                , pIsLoadArrayOfObjects ? serializer.Serialize(objCvwA_PaymentRequestHouse.lstCVarvwA_PaymentRequestHouse) : null //pOperations = pData[10]
                , pIsLoadArrayOfObjects ? serializer.Serialize(objCvwA_PaymentRequestCertificateNumber.lstCVarvwA_PaymentRequestCertificateNumber) : null //pOperations = pData[11]
                , pIsLoadArrayOfObjects ? serializer.Serialize(objCSalesMan.lstCVarSL_SalesMan) : null //pOperations = pData[12]
                , pIsLoadArrayOfObjects ? serializer.Serialize(objCCvwA_PaymentRequestTruckingOrder.lstCVarvwA_PaymentRequestTruckingOrder) : null //pOperations = pData[13]
                , pIsLoadArrayOfObjects ? serializer.Serialize(objCvwChargeTypesWithMinimalColumns.lstCVarvwChargeTypesWithMinimalColumns) : null  //hargeTypes = pData[14]
                , pIsLoadArrayOfObjects ? serializer.Serialize(objCvwUserForms.lstCVarvwUserForms) : null  //objCvwUserForms = pData[15] 


            };
        }

        [HttpGet, HttpPost]
        public Object[] GetCode(Int32 pSafeID, Int32 pBankID, DateTime pDate, Int32 pVoucherType)
        {
            int constVoucherChequeOut = 40;
            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
            string pNewCode = "";
            string pNewChequeCode = "";

            if (pBankID != 0) //ChequeVoucher
            {
                pNewCode = objCCustomizedDBCall.A_ChequeVoucher_GetCodeByBank("A_ChequeVoucher_GetCodeByBank", pDate, pBankID, pVoucherType,pSafeID);
                if (pVoucherType == constVoucherChequeOut)
                    pNewChequeCode = objCCustomizedDBCall.A_ChequeVoucher_GetNextChequeNo("A_ChequeVoucher_GetNextChequeNo", pBankID, pVoucherType);
            }
            else if (pSafeID != 0) //CashVoucher
                pNewCode = objCCustomizedDBCall.A_CashVoucher_GetCode_BySafeCode("A_CashVoucher_GetCode_BySafeCode", pSafeID, pDate, pVoucherType);
            return new object[] {
                pNewCode //pNewCode = pData[0]
                , pNewChequeCode //pNewChequeCode = pData[1]
            };
        }

        [HttpGet, HttpPost]
        public Object[] GetSafeBalance(Int32 pSafeID, DateTime pToDate, Int32 pCurrID)
        {
            Cvw_Current_SafeBalanceAllCurrencies objCvw_Current_SafeBalanceAllCurrencie = new Cvw_Current_SafeBalanceAllCurrencies();
            objCvw_Current_SafeBalanceAllCurrencie.GetList(pSafeID, pToDate, pCurrID, true);



            return new object[] {
               new JavaScriptSerializer().Serialize(objCvw_Current_SafeBalanceAllCurrencie.lstCVarvw_Current_SafeBalanceAllCurrencies[0])
            };
        }

        [HttpGet, HttpPost]
        public object[] GetJournalVouchersDetails(Int32 pPageNumber, Int32 pPageSize, string pWhereClauseVoucherDetails, string pOrderBy)
        {
            int _RowCount = 0;
            CvwA_VoucherDetails objCvwA_VoucherDetails = new CvwA_VoucherDetails();
            objCvwA_VoucherDetails.GetListPaging(pPageSize, pPageNumber, pWhereClauseVoucherDetails, pOrderBy, out _RowCount);

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };

            return new object[] {
                  serializer.Serialize(objCvwA_VoucherDetails.lstCVarvwA_VoucherDetails)
                , _RowCount
            };
        }

        [HttpPost]
        public object[] VoucherHeader_Save([FromBody] ParamVoucherHeader_Save paramVoucherHeader_Save)
        {
            string VCODE = "0";
            string pMessageReturned = "";
            string pUpdateClause = "";
            Exception checkException = null;
            CA_Fiscal_Year objCA_Fiscal_Year = new CA_Fiscal_Year();
            CA_Fiscal_Year_Period objCA_Fiscal_Year_Period = new CA_Fiscal_Year_Period();
            CFreezeSystemTransactions objCFreezeSystemTransactions = new CFreezeSystemTransactions();
            CA_Voucher objCA_Voucher = new CA_Voucher();
            CVarA_Voucher objCVarA_Voucher = new CVarA_Voucher();
            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
            checkException = objCA_Fiscal_Year.GetList("WHERE Confirmed=1 AND Fiscal_Year_Name=N'" + paramVoucherHeader_Save.pVoucherDate.Year + "'");
            checkException = objCA_Fiscal_Year_Period.GetList("WHERE '" + paramVoucherHeader_Save.pVoucherDate.ToString("yyyy/MM/dd") + "' BETWEEN From_Date AND To_Date and Closed=1");

            checkException = objCFreezeSystemTransactions.GetList("WHERE TransType='All' AND ('" + paramVoucherHeader_Save.pVoucherDate.ToString("yyyy/MM/dd") + "' BETWEEN FromDate AND ToDate)");
            #region Check FiscalYear is Confirmed and NOT Closed
            if (objCA_Fiscal_Year.lstCVarA_Fiscal_Year.Count == 0)
                pMessageReturned = "This fiscal year is not confirmed.";
            else if (objCA_Fiscal_Year_Period.lstCVarA_Fiscal_Year_Period.Count > 0)
                pMessageReturned = "This fiscal year is closed.";
            #endregion Check FiscalYear is Confirmed and NOT Closed
            #region Check VoucherCode
            //if (objCA_Voucher.lstCVarA_Voucher.Count > 0)
            //    pMessageReturned = "This code already exists for this year.";
            #endregion Check VoucherCode
            #region Check Period is Not Frozen
            if (objCFreezeSystemTransactions.lstCVarFreezeSystemTransactions.Count > 0)
                pMessageReturned = "The transactions for this date is frozen.";
            #endregion Check Period is Not Frozen
            #region Check If Posted
            if (paramVoucherHeader_Save.pID != 0)
            {
                objCA_Voucher.GetList("where ID= " + paramVoucherHeader_Save.pID);
                bool Posted = objCA_Voucher.lstCVarA_Voucher[0].Posted;
                if (Posted)
                    pMessageReturned = "Voucher Already Posted.";
            }
            #endregion

            checkException = objCA_Voucher.GetList("WHERE ID<>" + paramVoucherHeader_Save.pID + "AND Code=N'" + paramVoucherHeader_Save.pCode + "' AND DATEPART(yyyy, VoucherDate)=" + paramVoucherHeader_Save.pVoucherDate.Year + " AND VoucherType=" + paramVoucherHeader_Save.pVoucherType);

            // if return row then code is not unique
            if (objCA_Voucher.lstCVarA_Voucher.Count > 0 && paramVoucherHeader_Save.pID != 0)
                pMessageReturned = "This code already exists for this year.";

             if(pMessageReturned == "")
            {
                #region Save Header
                if (paramVoucherHeader_Save.pID == 0) //insert header
                {
                    VCODE = Convert.ToString(GetCode(paramVoucherHeader_Save.pSafeID, paramVoucherHeader_Save.pBankID, paramVoucherHeader_Save.pVoucherDate, paramVoucherHeader_Save.pVoucherType)[0]);
                    objCVarA_Voucher.Code = VCODE;
                    objCVarA_Voucher.VoucherDate = paramVoucherHeader_Save.pVoucherDate;
                    objCVarA_Voucher.SafeID = paramVoucherHeader_Save.pSafeID;
                    objCVarA_Voucher.CurrencyID = paramVoucherHeader_Save.pCurrencyID;
                    objCVarA_Voucher.ExchangeRate = paramVoucherHeader_Save.pExchangeRate;
                    objCVarA_Voucher.ChargedPerson = paramVoucherHeader_Save.pChargedPerson;
                    objCVarA_Voucher.Notes = paramVoucherHeader_Save.pNotes;
                    objCVarA_Voucher.TaxID = paramVoucherHeader_Save.pTaxID;
                    objCVarA_Voucher.TaxValue = paramVoucherHeader_Save.pTaxValue;
                    objCVarA_Voucher.TaxSign = paramVoucherHeader_Save.pTaxSign;
                    objCVarA_Voucher.TaxID2 = paramVoucherHeader_Save.pTaxID2;
                    objCVarA_Voucher.TaxValue2 = paramVoucherHeader_Save.pTaxValue2;
                    objCVarA_Voucher.TaxSign2 = paramVoucherHeader_Save.pTaxSign2;
                    objCVarA_Voucher.Total = paramVoucherHeader_Save.pTotal;
                    objCVarA_Voucher.TotalAfterTax = paramVoucherHeader_Save.pTotalAfterTax;
                    //objCVarA_Voucher.Approved = false;
                    //objCVarA_Voucher.Posted = false;
                    objCVarA_Voucher.IsAGInvoice = paramVoucherHeader_Save.pIsAGInvoice;
                    objCVarA_Voucher.AGInvType_ID = paramVoucherHeader_Save.pAGInvType_ID;
                    objCVarA_Voucher.Inv_No = paramVoucherHeader_Save.pInv_No;
                    objCVarA_Voucher.InvoiceID = paramVoucherHeader_Save.pInvoiceID;
                    ////Set from posting screen
                    //objCVarA_Voucher.JVID1 = pJVID1;
                    //objCVarA_Voucher.JVID2 = pJVID2;
                    //objCVarA_Voucher.JVID3 = pJVID3;
                    //objCVarA_Voucher.JVID4 = pJVID4;
                    objCVarA_Voucher.SalesManID = paramVoucherHeader_Save.pSalesManID;
                    objCVarA_Voucher.forwOperationID = paramVoucherHeader_Save.pforwOperationID;
                    objCVarA_Voucher.IsCustomClearance = paramVoucherHeader_Save.pIsCustomClearance;
                    objCVarA_Voucher.TransType_ID = paramVoucherHeader_Save.pTransType_ID;
                    objCVarA_Voucher.VoucherType = paramVoucherHeader_Save.pVoucherType;
                    objCVarA_Voucher.IsCash = paramVoucherHeader_Save.pIsCash;
                    objCVarA_Voucher.IsCheque = paramVoucherHeader_Save.pIsCheque;
                    objCVarA_Voucher.PrintDate = paramVoucherHeader_Save.pPrintDate;
                    objCVarA_Voucher.ChequeNo = paramVoucherHeader_Save.pChequeNo;
                    objCVarA_Voucher.ChequeDate = paramVoucherHeader_Save.pChequeDate;
                    objCVarA_Voucher.BankID = paramVoucherHeader_Save.pBankID;
                    objCVarA_Voucher.DisbursementJob_ID = paramVoucherHeader_Save.pDisbursementJob_ID;
                    objCVarA_Voucher.SL_SalesManID = paramVoucherHeader_Save.pSL_SalesManID;
                    objCVarA_Voucher.Bill_ID = 0;
                    objCVarA_Voucher.IBAN = "0";
                    objCVarA_Voucher.ReferenceNo = "0";
                    objCVarA_Voucher.DepositeDate = Convert.ToDateTime("1/1/1900");
                    objCVarA_Voucher.TransferDate = Convert.ToDateTime("1/1/1900");

                    objCVarA_Voucher.PaidAmount = 0;
                    objCVarA_Voucher.RemainAmount = paramVoucherHeader_Save.pTotalAfterTax;
                    objCVarA_Voucher.isTransfer = false;


                    objCVarA_Voucher.OtherSideBankName = paramVoucherHeader_Save.pOtherSideBankName;
                    objCVarA_Voucher.CollectionDate = paramVoucherHeader_Save.pCollectionDate;
                    objCVarA_Voucher.CollectionExpense = paramVoucherHeader_Save.pCollectionExpense;
                    objCA_Voucher.lstCVarA_Voucher.Add(objCVarA_Voucher);
                    checkException = objCA_Voucher.SaveMethod(objCA_Voucher.lstCVarA_Voucher);

                    

                    if (checkException == null)
                    {
                        objCCustomizedDBCall.SP_Trans_Log("SP_Trans_Log", WebSecurity.CurrentUserId, "A_Voucher", objCVarA_Voucher.ID, "I");
                        paramVoucherHeader_Save.pID = objCVarA_Voucher.ID;
                    }
                    else
                        pMessageReturned = checkException.Message;
                }
                else //update header
                {
                    VCODE = paramVoucherHeader_Save.pCode;
                    pUpdateClause = "Code=N'" + paramVoucherHeader_Save.pCode + "'" + "\n";
                    pUpdateClause += ",VoucherDate=N'" + paramVoucherHeader_Save.pVoucherDate.ToString("yyyyMMdd") + "'" + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pSafeID == 0 ? (",SafeID=NULL") : (",SafeID=" + paramVoucherHeader_Save.pSafeID)) + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pCurrencyID == 0 ? (",CurrencyID=NULL") : (",CurrencyID=" + paramVoucherHeader_Save.pCurrencyID)) + "\n";
                    pUpdateClause += ",ExchangeRate=" + paramVoucherHeader_Save.pExchangeRate + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pChargedPerson == "0" ? (",ChargedPerson=NULL") : (",ChargedPerson=N'" + paramVoucherHeader_Save.pChargedPerson + "'")) + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pNotes == "0" ? (",Notes=NULL") : (",Notes=N'" + paramVoucherHeader_Save.pNotes + "'")) + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pTaxID == 0 ? (",TaxID=NULL") : (",TaxID=" + paramVoucherHeader_Save.pTaxID)) + "\n";
                    pUpdateClause += ",TaxValue=" + paramVoucherHeader_Save.pTaxValue + "\n";
                    pUpdateClause += ",TaxSign=" + paramVoucherHeader_Save.pTaxSign + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pTaxID2 == 0 ? (",TaxID2=NULL") : (",TaxID2=" + paramVoucherHeader_Save.pTaxID2)) + "\n";
                    pUpdateClause += ",TaxValue2=" + paramVoucherHeader_Save.pTaxValue2 + "\n";
                    pUpdateClause += ",TaxSign2=" + paramVoucherHeader_Save.pTaxSign2 + "\n";
                    pUpdateClause += ",Total=" + paramVoucherHeader_Save.pTotal + "\n";
                    pUpdateClause += ",TotalAfterTax=" + paramVoucherHeader_Save.pTotalAfterTax + "\n";
                    pUpdateClause += ",IsAGInvoice=" + (paramVoucherHeader_Save.pIsAGInvoice ? "1" : "0") + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pAGInvType_ID == 0 ? (",AGInvType_ID=NULL") : (",AGInvType_ID=" + paramVoucherHeader_Save.pAGInvType_ID)) + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pInv_No == 0 ? (",Inv_No=NULL") : (",Inv_No=N'" + paramVoucherHeader_Save.pInv_No + "'")) + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pInvoiceID == 0 ? (",InvoiceID=NULL") : (",InvoiceID=" + paramVoucherHeader_Save.pInvoiceID)) + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pJVID1 == 0 ? (",JVID1=NULL") : (",JVID1=" + paramVoucherHeader_Save.pJVID1)) + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pJVID2 == 0 ? (",JVID2=NULL") : (",JVID2=" + paramVoucherHeader_Save.pJVID2)) + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pJVID3 == 0 ? (",JVID3=NULL") : (",JVID3=" + paramVoucherHeader_Save.pJVID3)) + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pJVID4 == 0 ? (",JVID4=NULL") : (",JVID4=" + paramVoucherHeader_Save.pJVID4)) + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pSalesManID == 0 ? (",SalesManID=NULL") : (",SalesManID=" + paramVoucherHeader_Save.pSalesManID)) + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pforwOperationID == 0 ? (",forwOperationID=NULL") : (",forwOperationID=" + paramVoucherHeader_Save.pforwOperationID)) + "\n";
                    pUpdateClause += ",IsCustomClearance=" + (paramVoucherHeader_Save.pIsCustomClearance ? "1" : "0") + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pTransType_ID == 0 ? (",TransType_ID=NULL") : (",TransType_ID=" + paramVoucherHeader_Save.pTransType_ID)) + "\n";
                    pUpdateClause += ",VoucherType=" + paramVoucherHeader_Save.pVoucherType + "\n";
                    pUpdateClause += ",IsCash=" + (paramVoucherHeader_Save.pIsCash ? "1" : "0") + "\n";
                    pUpdateClause += ",IsCheque=" + (paramVoucherHeader_Save.pIsCheque ? "1" : "0") + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pPrintDate.ToString("yyyy/MM/dd") == "1900/01/01" ? ",PrintDate=NULL" : (",PrintDate=N'" + paramVoucherHeader_Save.pPrintDate.ToString("yyyy/MM/dd") + "'")) + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pChequeNo == "0" ? (",ChequeNo=NULL") : (",ChequeNo=N'" + paramVoucherHeader_Save.pChequeNo + "'")) + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pChequeDate.ToString("yyyy/MM/dd") == "1900/01/01" ? ",ChequeDate=NULL" : (",ChequeDate=N'" + paramVoucherHeader_Save.pChequeDate.ToString("yyyy/MM/dd") + "'")) + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pBankID == 0 ? (",BankID=NULL") : (",BankID=" + paramVoucherHeader_Save.pBankID)) + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pDisbursementJob_ID == 0 ? (",DisbursementJob_ID=NULL") : (",DisbursementJob_ID=" + paramVoucherHeader_Save.pDisbursementJob_ID)) + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pSL_SalesManID == 0 ? (",SL_SalesManID=NULL") : (",SL_SalesManID=" + paramVoucherHeader_Save.pSL_SalesManID)) + "\n";


                    pUpdateClause += (paramVoucherHeader_Save.pOtherSideBankName == "0" ? (",OtherSideBankName=NULL") : (",OtherSideBankName=N'" + paramVoucherHeader_Save.pOtherSideBankName + "'")) + "\n";
                    pUpdateClause += (paramVoucherHeader_Save.pCollectionDate.ToString("yyyy/MM/dd") == "1900/01/01" ? ",CollectionDate=NULL" : (",CollectionDate=N'" + paramVoucherHeader_Save.pCollectionDate.ToString("yyyy/MM/dd") + "'")) + "\n";
                    pUpdateClause += ",CollectionExpense=" + paramVoucherHeader_Save.pCollectionExpense + "\n";
                    pUpdateClause += "WHERE ID=" + paramVoucherHeader_Save.pID + "\n";
                    checkException = objCA_Voucher.UpdateList(pUpdateClause);
                    if (checkException == null)
                        objCCustomizedDBCall.SP_Trans_Log("SP_Trans_Log", WebSecurity.CurrentUserId, "A_Voucher", paramVoucherHeader_Save.pID, "U");
                    else
                        pMessageReturned = checkException.Message;
                }
              
                #endregion Save Header
            }

            return new object[] {
                pMessageReturned, paramVoucherHeader_Save.pID
            };
        }
        //[HttpPost]
        //public object[] VoucherHeader_Save([FromBody] ParamVoucherHeader_Save paramVoucherHeader_Save)
        //{
        //    string VCODE = "0";
        //    string pMessageReturned = "";
        //    string pUpdateClause = "";
        //    Exception checkException = null;
        //    CA_Fiscal_Year objCA_Fiscal_Year = new CA_Fiscal_Year();
        //    CA_Fiscal_Year_Period objCA_Fiscal_Year_Period = new CA_Fiscal_Year_Period();
        //    CFreezeSystemTransactions objCFreezeSystemTransactions = new CFreezeSystemTransactions();
        //    CA_Voucher objCA_Voucher = new CA_Voucher();
        //    CVarA_Voucher objCVarA_Voucher = new CVarA_Voucher();
        //    CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
        //    checkException = objCA_Fiscal_Year.GetList("WHERE Confirmed=1 AND Fiscal_Year_Name=N'" + paramVoucherHeader_Save.pVoucherDate.Year + "'");
        //    checkException = objCA_Fiscal_Year_Period.GetList("WHERE '" + paramVoucherHeader_Save.pVoucherDate.ToString("yyyy/MM/dd") + "' BETWEEN From_Date AND To_Date and Closed=1");

        //    checkException = objCFreezeSystemTransactions.GetList("WHERE TransType='All' AND ('" + paramVoucherHeader_Save.pVoucherDate.ToString("yyyy/MM/dd") + "' BETWEEN FromDate AND ToDate)");
        //    #region Check FiscalYear is Confirmed and NOT Closed
        //    if (objCA_Fiscal_Year.lstCVarA_Fiscal_Year.Count == 0)
        //        pMessageReturned = "This fiscal year is not confirmed.";
        //    else if (objCA_Fiscal_Year_Period.lstCVarA_Fiscal_Year_Period.Count > 0)
        //        pMessageReturned = "This fiscal year is closed.";
        //    #endregion Check FiscalYear is Confirmed and NOT Closed
        //    #region Check VoucherCode
        //    //if (objCA_Voucher.lstCVarA_Voucher.Count > 0)
        //    //    pMessageReturned = "This code already exists for this year.";
        //    #endregion Check VoucherCode
        //    #region Check Period is Not Frozen
        //    if (objCFreezeSystemTransactions.lstCVarFreezeSystemTransactions.Count > 0)
        //        pMessageReturned = "The transactions for this date is frozen.";
        //    #endregion Check Period is Not Frozen
        //    #region Check If Posted
        //    if (paramVoucherHeader_Save.pID != 0)
        //    {
        //        objCA_Voucher.GetList("where ID= " + paramVoucherHeader_Save.pID);
        //        bool Posted = objCA_Voucher.lstCVarA_Voucher[0].Posted;
        //        if (Posted)
        //            pMessageReturned = "Voucher Already Posted.";
        //    }
        //    #endregion

        //    checkException = objCA_Voucher.GetList("WHERE ID<>" + paramVoucherHeader_Save.pID + "AND Code=N'" + paramVoucherHeader_Save.pCode + "' AND DATEPART(yyyy, VoucherDate)=" + paramVoucherHeader_Save.pVoucherDate.Year + " AND VoucherType=" + paramVoucherHeader_Save.pVoucherType);

        //    // if return row then code is not unique
        //    if (objCA_Voucher.lstCVarA_Voucher.Count > 0 && paramVoucherHeader_Save.pID != 0)
        //        pMessageReturned = "This code already exists for this year.";

        //    #region Always Update coz i insert first detail 1st
        //    if (pMessageReturned == "")
        //    {

        //        if (paramVoucherHeader_Save.pID != 0)
        //            VCODE = paramVoucherHeader_Save.pCode;
        //        else
        //            VCODE = Convert.ToString(GetCode(paramVoucherHeader_Save.pSafeID, paramVoucherHeader_Save.pBankID, paramVoucherHeader_Save.pVoucherDate, paramVoucherHeader_Save.pVoucherType)[0]);


        //        pUpdateClause = "Code=N'" + VCODE + "'" + "\n";
        //        pUpdateClause += ",VoucherDate=N'" + paramVoucherHeader_Save.pVoucherDate.ToString("yyyyMMdd") + "'" + "\n";
        //        pUpdateClause += (paramVoucherHeader_Save.pSafeID == 0 ? (",SafeID=NULL") : (",SafeID=" + paramVoucherHeader_Save.pSafeID)) + "\n";
        //        pUpdateClause += (paramVoucherHeader_Save.pCurrencyID == 0 ? (",CurrencyID=NULL") : (",CurrencyID=" + paramVoucherHeader_Save.pCurrencyID)) + "\n";
        //        pUpdateClause += ",ExchangeRate=" + paramVoucherHeader_Save.pExchangeRate + "\n";
        //        pUpdateClause += (paramVoucherHeader_Save.pChargedPerson == "0" ? (",ChargedPerson=NULL") : (",ChargedPerson=N'" + paramVoucherHeader_Save.pChargedPerson + "'")) + "\n";
        //        pUpdateClause += (paramVoucherHeader_Save.pNotes == "0" ? (",Notes=NULL") : (",Notes=N'" + paramVoucherHeader_Save.pNotes + "'")) + "\n";
        //        pUpdateClause += (paramVoucherHeader_Save.pTaxID == 0 ? (",TaxID=NULL") : (",TaxID=" + paramVoucherHeader_Save.pTaxID)) + "\n";
        //        pUpdateClause += ",TaxValue=" + paramVoucherHeader_Save.pTaxValue + "\n";
        //        pUpdateClause += ",TaxSign=" + paramVoucherHeader_Save.pTaxSign + "\n";
        //        pUpdateClause += (paramVoucherHeader_Save.pTaxID2 == 0 ? (",TaxID2=NULL") : (",TaxID2=" + paramVoucherHeader_Save.pTaxID2)) + "\n";
        //        pUpdateClause += ",TaxValue2=" + paramVoucherHeader_Save.pTaxValue2 + "\n";
        //        pUpdateClause += ",TaxSign2=" + paramVoucherHeader_Save.pTaxSign2 + "\n";
        //        pUpdateClause += ",Total=" + paramVoucherHeader_Save.pTotal + "\n";
        //        pUpdateClause += ",TotalAfterTax=" + paramVoucherHeader_Save.pTotalAfterTax + "\n";
        //        pUpdateClause += ",IsAGInvoice=" + (paramVoucherHeader_Save.pIsAGInvoice ? "1" : "0") + "\n";
        //        pUpdateClause += (paramVoucherHeader_Save.pAGInvType_ID == 0 ? (",AGInvType_ID=NULL") : (",AGInvType_ID=" + paramVoucherHeader_Save.pAGInvType_ID)) + "\n";
        //        pUpdateClause += (paramVoucherHeader_Save.pInv_No == 0 ? (",Inv_No=NULL") : (",Inv_No=N'" + paramVoucherHeader_Save.pInv_No + "'")) + "\n";
        //        pUpdateClause += (paramVoucherHeader_Save.pInvoiceID == 0 ? (",InvoiceID=NULL") : (",InvoiceID=" + paramVoucherHeader_Save.pInvoiceID)) + "\n";
        //        ////Set from posting screen
        //        //pUpdateClause += (pJVID1 == 0 ? (",JVID1=NULL") : (",JVID1=" + pJVID1)) + "\n";
        //        //pUpdateClause += (pJVID2 == 0 ? (",JVID2=NULL") : (",JVID2=" + pJVID2)) + "\n";
        //        //pUpdateClause += (pJVID3 == 0 ? (",JVID3=NULL") : (",JVID3=" + pJVID3)) + "\n";
        //        //pUpdateClause += (pJVID4 == 0 ? (",JVID4=NULL") : (",JVID4=" + pJVID4)) + "\n";
        //        pUpdateClause += (paramVoucherHeader_Save.pSalesManID == 0 ? (",SalesManID=NULL") : (",SalesManID=" + paramVoucherHeader_Save.pSalesManID)) + "\n";
        //        pUpdateClause += (paramVoucherHeader_Save.pforwOperationID == 0 ? (",forwOperationID=NULL") : (",forwOperationID=" + paramVoucherHeader_Save.pforwOperationID)) + "\n";
        //        pUpdateClause += ",IsCustomClearance=" + (paramVoucherHeader_Save.pIsCustomClearance ? "1" : "0") + "\n";
        //        pUpdateClause += (paramVoucherHeader_Save.pTransType_ID == 0 ? (",TransType_ID=NULL") : (",TransType_ID=" + paramVoucherHeader_Save.pTransType_ID)) + "\n";
        //        pUpdateClause += ",VoucherType=" + paramVoucherHeader_Save.pVoucherType + "\n";
        //        pUpdateClause += ",IsCash=" + (paramVoucherHeader_Save.pIsCash ? "1" : "0") + "\n";
        //        pUpdateClause += ",IsCheque=" + (paramVoucherHeader_Save.pIsCheque ? "1" : "0") + "\n";
        //        pUpdateClause += (paramVoucherHeader_Save.pPrintDate.ToString("yyyy/MM/dd") == "1900/01/01" ? ",PrintDate=NULL" : (",PrintDate=N'" + paramVoucherHeader_Save.pPrintDate.ToString("yyyy/MM/dd") + "'")) + "\n";
        //        pUpdateClause += (paramVoucherHeader_Save.pChequeNo == "0" ? (",ChequeNo=NULL") : (",ChequeNo=N'" + paramVoucherHeader_Save.pChequeNo + "'")) + "\n";
        //        pUpdateClause += (paramVoucherHeader_Save.pChequeDate.ToString("yyyy/MM/dd") == "1900/01/01" ? ",ChequeDate=NULL" : (",ChequeDate=N'" + paramVoucherHeader_Save.pChequeDate.ToString("yyyy/MM/dd") + "'")) + "\n";
        //        pUpdateClause += (paramVoucherHeader_Save.pBankID == 0 ? (",BankID=NULL") : (",BankID=" + paramVoucherHeader_Save.pBankID)) + "\n";
        //        pUpdateClause += (paramVoucherHeader_Save.pOtherSideBankName == "0" ? (",OtherSideBankName=NULL") : (",OtherSideBankName=N'" + paramVoucherHeader_Save.pOtherSideBankName + "'")) + "\n";
        //        pUpdateClause += (paramVoucherHeader_Save.pCollectionDate.ToString("yyyy/MM/dd") == "1900/01/01" ? ",CollectionDate=NULL" : (",CollectionDate=N'" + paramVoucherHeader_Save.pCollectionDate.ToString("yyyy/MM/dd") + "'")) + "\n";
        //        pUpdateClause += ",CollectionExpense=" + paramVoucherHeader_Save.pCollectionExpense + "\n";
        //        pUpdateClause += "WHERE ID=" + paramVoucherHeader_Save.pID + "\n";
        //        checkException = objCA_Voucher.UpdateList(pUpdateClause);
        //        if (checkException == null)
        //            objCCustomizedDBCall.SP_Trans_Log("SP_Trans_Log", WebSecurity.CurrentUserId, "A_Voucher", paramVoucherHeader_Save.pID, (paramVoucherHeader_Save.pID == 0 ? "I" : "U"));
        //        else
        //            pMessageReturned = checkException.Message;
        //    }
        //    #endregion Always Update coz i insert first detail 1st
        //    return new object[] {
        //        pMessageReturned
        //    };
        //}

        [HttpGet, HttpPost]
        public void Voucher_SetTotal(Int64 pVoucherID)
        {
            //Total = Sum of all rows + Sum of positive Taxes
            //TotalAfterTax = For Out-Vouchers then (Total - Sum of -ve Taxes)
            int constVoucherCashIn = 10;
            //int constVoucherCashOut = 20;
            int constVoucherChequeIn = 30;
            //int constVoucherChequeOut = 40;

            CA_Voucher objCA_Voucher = new CA_Voucher();
            CA_VoucherDetails objCA_VoucherDetails = new CA_VoucherDetails();
            objCA_Voucher.GetList("WHERE ID=" + pVoucherID);
            objCA_VoucherDetails.GetList("WHERE VoucherID=" + pVoucherID);
            decimal pTaxValue = objCA_Voucher.lstCVarA_Voucher[0].TaxValue * objCA_Voucher.lstCVarA_Voucher[0].TaxSign;
            decimal pTaxValue2 = objCA_Voucher.lstCVarA_Voucher[0].TaxValue2 * objCA_Voucher.lstCVarA_Voucher[0].TaxSign2;
            decimal pTotal = 0;
            decimal pTotalAfterTax = 0;
            pTotal = objCA_VoucherDetails.lstCVarA_VoucherDetails.Sum(s => s.Value);
            pTotalAfterTax = pTotal;
            pTotal += (pTaxValue > 0 ? pTaxValue : 0) + (pTaxValue2 > 0 ? pTaxValue2 : 0);
            //if (objCA_Voucher.lstCVarA_Voucher[0].VoucherType == constVoucherCashIn || objCA_Voucher.lstCVarA_Voucher[0].VoucherType == constVoucherChequeIn)
            pTotalAfterTax += pTaxValue + pTaxValue2;
            //else //OutVouchers so TotalAfterTax is remove just -ve Taxes
            //    pTotalAfterTax = (pTaxValue < 0 ? pTaxValue : 0) + (pTaxValue2 < 0 ? pTaxValue2 : 0);
            //if (pTotal >= pTotalAfterTax)
            objCA_Voucher.UpdateList("Total=" + pTotal + " ,TotalAfterTax=" + pTotalAfterTax + " WHERE ID=" + pVoucherID);
            //else
            //    objCA_Voucher.UpdateList("Total=" + pTotalAfterTax + " ,TotalAfterTax=" + pTotal + " WHERE ID=" + pVoucherID);
        }

        [HttpGet, HttpPost]
        public Object[] GetInvoiceAccounts(string pInvoiceIDs)
        {
             string IDs =  pInvoiceIDs.TrimEnd(',');

            Int32 _RowCount = 0;
            CvwA_InvoiceItemsAccounts objCvwA_InvoiceItemsAccounts = new CvwA_InvoiceItemsAccounts();
            objCvwA_InvoiceItemsAccounts.GetListPaging(10000, 1, "WHERE InvoiceID in ( " + IDs + ")", "InvoiceID", out _RowCount);
  
       
            return new Object[] {
                new JavaScriptSerializer().Serialize(objCvwA_InvoiceItemsAccounts.lstCVarvwA_InvoiceItemsAccounts) };
        }



        [HttpGet, HttpPost]
        public Object[] ARAllocation_Partners_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned(bool pIsLoadArrayOfObjects, Int32 pPageNumber, Int32 pPageSize, string pWhereClauseAllocation_Partners, string pOrderBy)
        {
            bool _result = false;
            Exception checkException = null;
            CvwAccPartners objCvwAccPartners = new CvwAccPartners();
            CvwAccPartners objCvwAccPartnersAll = new CvwAccPartners(); //For the select box
            CNoAccessPartnerTypes objCPartnerTypes = new CNoAccessPartnerTypes();
            int _RowCount = 0;
            //checkException = objCvwAccPartnersAll.GetList("ORDER BY PartnerTypeID, Name");
            checkException = objCvwAccPartners.GetListPaging(pPageSize, pPageNumber, pWhereClauseAllocation_Partners, pOrderBy, out _RowCount);
            checkException = objCPartnerTypes.GetList("ORDER BY Code");
            if (checkException == null)
            {
                _result = true;
            }
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                _result ? new JavaScriptSerializer().Serialize(objCvwAccPartners.lstCVarvwAccPartners) : null
                , _RowCount
                , pIsLoadArrayOfObjects || _result ? serializer.Serialize(objCvwAccPartnersAll.lstCVarvwAccPartners) : null
                , pIsLoadArrayOfObjects || _result ? serializer.Serialize(objCPartnerTypes.lstCVarNoAccessPartnerTypes) : null
            };
        }

        [HttpGet, HttpPost]
        public Object[] ARAllocation_Partners_PartnerTypeChanged(Int32 pPartnerTypeID, string pWhereClauseAllocation_Partners, string pOrderBy, Int32 pPageNumber, Int32 pPageSize)
        {
            Exception checkException = null;
            CvwAccPartners objCvwAccPartners = new CvwAccPartners();
            CvwAccPartners objCvwAccPartnersAll = new CvwAccPartners(); //For the select box
            int _RowCount = 0;

            if (pPartnerTypeID != 0)
                checkException = objCvwAccPartnersAll.GetListPaging(10000, 1, "WHERE PartnerTypeID=" + pPartnerTypeID, "Name", out _RowCount);
            checkException = objCvwAccPartners.GetListPaging(pPageSize, pPageNumber, pWhereClauseAllocation_Partners, pOrderBy, out _RowCount);
            return new object[]
            {
                new JavaScriptSerializer().Serialize(objCvwAccPartners.lstCVarvwAccPartners)
                , _RowCount
                , new JavaScriptSerializer().Serialize(objCvwAccPartnersAll.lstCVarvwAccPartners)
            };
        }






        //[HttpGet, HttpPost]
        //public Object[] FillInvoices()
        //{
        //    CvwInvoices objCvwInvoices = new CvwInvoices();
        //    int _RowCount = 0;
        //        var   pWhereClause = " WHERE IsDeleted = 0 AND IsApproved = 1 ";
        //      //  pWhereClause = "WHERE IsDeleted = 0 AND IsApproved = 1 ";
        //       // if(pPartnerID.Trim() != "0")
        //        //pWhereClause += " AND PartnerID = " + pPartnerID;
        //        pWhereClause += " AND PartnerTypeID = " + 1 + " ";
        //        pWhereClause += " AND InvoiceStatus IN (N'UnPaid' , N'Partially Paid') ";
        //      var  checkException = objCvwInvoices.GetListPaging(999999, 1, pWhereClause, "CreationDate DESC", out _RowCount);

        //    return new Object[]
        //    {
        //      new JavaScriptSerializer().Serialize(objCvwInvoices.lstCVarvwInvoices)

        //    };
        //}


        [HttpGet, HttpPost]
        public object[] ARAllocation_FillAllocationData(Int32 pPartnerID, Int32 pPartnerTypeID, Int32 pAllocationType,
            string pSearchText, string pCurrencyID, bool pIsCash)
        {
            bool _result = false;
            Exception checkException = null;
            string pWhereClause = "";
            int _RowCount = 0;
            int currency = int.Parse(pCurrencyID);
            int constTransactionOpenCreditBalance = 2; //OpenCreditBalance
            int constTransactionOpenDebitBalance = 5; //OpenDebitBalance
            int constTransactionARPayment = 10; //Credit
            int constTransactionAPPayment = 20;
            int constTransactionInvoiceApproval = 30; //Debit
            int constTransactionReceivableAllocation = 40; //Invoice
            int constTransactionCreditTransfer = 50; //CreditTransfer
            int constTransactionDebitTransfer = 60; //DebitTransfer
            int constTransactionPayableApproval = 70; //Op.Payable (Credit For Supplier)
            int constTransactionPayableAllocatedFromCustody = 85; //Credit
            int constTransactionPayableAllocation = 80; //Payable
            int constTransactionDebitNote = 90; //DebitNote (i.e. Receivables) inv
            int constTransactionCreditNote = 100; //CreditNote (i.e. Payables)

            int constCustodyPartnerTypeID = 20;

            int? AccountID = -1;
            int? SubAccountID = -1;
            if (!pIsCash)
            {
                var Account_SubAccountIDs = GetAccount_SubAccountForPartener(pPartnerID, pPartnerTypeID);
                AccountID = Account_SubAccountIDs[0];
                SubAccountID = Account_SubAccountIDs[1];
            }


            CDefaults objCDefaults = new CDefaults();
            objCDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa
            //CvwAccUnAllocatedPartnerBalance objCvwAccUnAllocatedPartnerBalance = new CvwAccUnAllocatedPartnerBalance();
            CvwAccPartnerBalance objCvwAvailableAmounts = new CvwAccPartnerBalance();

            CvwInvoices objCvwInvoices = new CvwInvoices();
            CvwPayables objCvwPayables = new CvwPayables();
            if (!pIsCash)
            {
                pWhereClause = "WHERE PartnerID = " + pPartnerID.ToString();
                pWhereClause += " AND PartnerTypeID = " + pPartnerTypeID.ToString();
            }

            //checkException = objCvwAccUnAllocatedPartnerBalance.GetList(pWhereClause);
            #region Get Availabe Amounts that can be allocated
            checkException = objCvwAvailableAmounts.GetListPaging(9999999, 1, pWhereClause, "ID", out _RowCount);
            //var pAvailableAmounts = objCvwAvailableAmounts.lstCVarvwAccPartnerBalance //constTransactionAPPayment=20
            //    .GroupBy(g => g.CurrencyID)
            //    .Select(g => new
            //    {
            //        AvailableBalance = g.Sum(s => (s.TransactionType == constTransactionAPPayment || s.TransactionType == constTransactionPayableAllocation
            //                                       || s.TransactionType == constTransactionOpenCreditBalance
            //                                       || s.TransactionType == constTransactionOpenDebitBalance
            //                                       || s.TransactionType == constTransactionCreditTransfer || s.TransactionType == constTransactionDebitTransfer
            //                                       || s.TransactionType == constTransactionARPayment || s.TransactionType == constTransactionReceivableAllocation
            //                                       || s.TransactionType == constTransactionDebitNote
            //                                       || s.TransactionType == constTransactionCreditNote
            //                                       ? s.CreditAmount - s.DebitAmount
            //                                       : 0))
            //        ,
            //        CurrencyID = g.First().CurrencyID
            //        ,
            //        CurrencyCode = g.First().CurrencyCode
            //    }).OrderBy(o => o.CurrencyCode);

            string ptxtAvailableBalance = "";
            //if (pAvailableAmounts.Count() > 0)
            //    if (pAvailableAmounts.ElementAt(0).AvailableBalance != 0)
            //        ptxtAvailableBalance = (pAllocationType == constTransactionReceivableAllocation ? decimal.Round(pAvailableAmounts.ElementAt(0).AvailableBalance, 3).ToString() : (-1 * (decimal.Round(pAvailableAmounts.ElementAt(0).AvailableBalance, 3))).ToString()
            //            + " " + pAvailableAmounts.ElementAt(0).CurrencyCode);
            //for (int i = 1; i < pAvailableAmounts.Count(); i++)
            //{
            //    if (pAvailableAmounts.ElementAt(i).AvailableBalance != 0)
            //        ptxtAvailableBalance += " , " + (pAllocationType == constTransactionReceivableAllocation ? decimal.Round(pAvailableAmounts.ElementAt(i).AvailableBalance, 3).ToString() : (-1 * (decimal.Round(pAvailableAmounts.ElementAt(i).AvailableBalance, 3))).ToString()
            //            + " " + pAvailableAmounts.ElementAt(i).CurrencyCode);
            //}
            CA_InvoiceAllocation_GetPartnerBalance cA_InvoiceAllocation_GetPartnerBalance = new CA_InvoiceAllocation_GetPartnerBalance();
            cA_InvoiceAllocation_GetPartnerBalance.GetList(pPartnerID, pPartnerTypeID, pAllocationType);
            var pAvailableAmounts = cA_InvoiceAllocation_GetPartnerBalance.lstCVarA_InvoiceAllocation_GetPartnerBalance;

            if (pAvailableAmounts != null && pAvailableAmounts.Count() > 0)
            {

                foreach (var item in pAvailableAmounts)
                {
                    ptxtAvailableBalance += (item.AvailableBalance + " , ");

                }

            }


            #endregion Get Availabe Amounts that can be allocated
            if (pAllocationType == constTransactionReceivableAllocation)
            {
                #region Invoices
                pWhereClause = "WHERE IsDeleted = 0  ";
                if (!pIsCash)
                {
                    pWhereClause += "AND IsApproved = 1";
                    pWhereClause += " AND PartnerID = " + pPartnerID.ToString();
                    pWhereClause += " AND PartnerTypeID = " + pPartnerTypeID.ToString();
                }
                else
                {
                    pWhereClause += "AND IsApproved = 0";
                    pWhereClause += "AND PaymentTermCode ='CASH'";
                }

                pWhereClause += " AND InvoiceStatus IN (N'UnPaid' , N'Partially Paid') ";
                pWhereClause += " AND CurrencyID = " + currency + "";
                pWhereClause += " AND InvoiceTypeCode <> N'Draft'  ";
                if (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "ELI")
                    pWhereClause += " AND year(InvoiceDate) > 2020 ";

                // if(pSearchText != null)
                //{
                //    pWhereClause += " AND (";
                //    pWhereClause += "       OperationCode like N'%" + pSearchText + "%'";
                //    pWhereClause += "    OR ConcatenatedInvoiceNumber like N'%" + pSearchText + "%'";
                //    //pWhereClause += "    OR PartnerName like N'%" + pSearchText + "%'"; //i am already allocating for 1 partner
                //    pWhereClause += "     )";
                //}
                checkException = objCvwInvoices.GetListPaging(999999, 1, pWhereClause, "CreationDate DESC", out _RowCount);
                #endregion Invoices
            }
            else if (pAllocationType == constTransactionPayableAllocation)
            {
                #region Payables
                pWhereClause = "WHERE IsDeleted = 0 AND IsApproved = 1 ";
                if (pPartnerTypeID == constCustodyPartnerTypeID) //if Custody for Payables , i can allocate for other suppliers
                    pWhereClause += " AND PartnerSupplierID IS NOT NULL ";
                else
                {
                    pWhereClause += " AND PartnerSupplierID = " + pPartnerID.ToString();
                    pWhereClause += " AND SupplierPartnerTypeID = " + pPartnerTypeID.ToString();
                }
                pWhereClause += " AND PayableStatus IN (N'UnPaid' , N'Partially Paid') ";
                if (pSearchText != null)
                {
                    pWhereClause += " AND (";
                    pWhereClause += "       OperationCode like N'%" + pSearchText + "%'";
                    pWhereClause += "    OR SupplierInvoiceNo like N'%" + pSearchText + "%'";
                    pWhereClause += "    OR ChargeTypeCode like N'%" + pSearchText + "%'";
                    pWhereClause += "    OR PartnerSupplierName like N'%" + pSearchText + "%'";
                    pWhereClause += "     )";
                }
                if (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "ELI")
                    pWhereClause += " AND year(IssueDate) > 2020 ";

                checkException = objCvwPayables.GetListPaging(1000, 1, pWhereClause, "CreationDate DESC", out _RowCount);
                #endregion Payables
            }
            if (checkException == null)
            {
                _result = true;
            }
            CvwPayablesAllocationsItems objCvwPayablesAllocationsItems = new CvwPayablesAllocationsItems();

            string pWhere = "Where PartnerSupplierID = " + pPartnerID.ToString() + "  AND SupplierPartnerTypeID = " + pPartnerTypeID.ToString() + " AND CurrencyID = " + currency + "";
            if (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "ELI")
            {
                pWhere  += " AND year(IssueDate) > 2020 ";
            }
            if ( pSearchText != null)
            {
                pWhere += pSearchText;
            }
            objCvwPayablesAllocationsItems.GetList(pWhere);

            //pWhereClause += " AND CurrencyID = " + currency + "";
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                _result
                , pAllocationType == constTransactionReceivableAllocation ? serializer.Serialize(objCvwInvoices.lstCVarvwInvoices) : null //pData[1]
                //, _result ? new JavaScriptSerializer().Serialize(objCvwAccUnAllocatedPartnerBalance.lstCVarvwAccUnAllocatedPartnerBalance) : null //pData[2]
                , _result ? new JavaScriptSerializer().Serialize(pAvailableAmounts) : null //pData[2]
                , pAllocationType == constTransactionPayableAllocation ?  serializer.Serialize(objCvwPayables.lstCVarvwPayables) : null //pData[3]
                , ptxtAvailableBalance //pData[4]
                , AccountID
                , SubAccountID
                , serializer.Serialize(objCvwPayablesAllocationsItems.lstCVarvwPayablesAllocationsItems)//7
            };
        }


        [HttpGet, HttpPost]
        public object[] OfficialAllocation_FillAllocationData(Int32 pPartnerID, Int32 pPartnerTypeID, Int32 pAllocationType,
            string pSearchText, string pCurrencyID)
        {
            bool _result = false;
            Exception checkException = null;
            string pWhereClause = "";
            int _RowCount = 0;
            int currency = int.Parse(pCurrencyID);
        
            int constTransactionReceivableAllocation = 40; //Invoice        
            int constTransactionPayableAllocation = 80; //Payable
         
            int constCustodyPartnerTypeID = 20;

            int? AccountID = -1;
            int? SubAccountID = -1;

            var Account_SubAccountIDs = GetAccount_SubAccountForPartener(pPartnerID, pPartnerTypeID);
            AccountID = Account_SubAccountIDs[0];
            SubAccountID = Account_SubAccountIDs[1];



            CDefaults objCDefaults = new CDefaults();
            objCDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa
            //CvwAccUnAllocatedPartnerBalance objCvwAccUnAllocatedPartnerBalance = new CvwAccUnAllocatedPartnerBalance();
            CvwAccPartnerBalance objCvwAvailableAmounts = new CvwAccPartnerBalance();

            CvwInvoices objCvwInvoices = new CvwInvoices();
            CvwPayables objCvwPayables = new CvwPayables();
          
           pWhereClause = "WHERE PartnerID = " + pPartnerID.ToString();
           pWhereClause += " AND PartnerTypeID = " + pPartnerTypeID.ToString();



            #region Get Availabe Amounts that can be allocated
            checkException = objCvwAvailableAmounts.GetListPaging(9999999, 1, pWhereClause, "ID", out _RowCount);


            string ptxtAvailableBalance = "";

            CA_InvoiceAllocation_GetPartnerBalance cA_InvoiceAllocation_GetPartnerBalance = new CA_InvoiceAllocation_GetPartnerBalance();
            cA_InvoiceAllocation_GetPartnerBalance.GetList(pPartnerID, pPartnerTypeID, pAllocationType);
            var pAvailableAmounts = cA_InvoiceAllocation_GetPartnerBalance.lstCVarA_InvoiceAllocation_GetPartnerBalance;

            if (pAvailableAmounts != null && pAvailableAmounts.Count() > 0)
            {

                foreach (var item in pAvailableAmounts)
                {
                    ptxtAvailableBalance += (item.AvailableBalance + " , ");

                }

            }


            #endregion Get Availabe Amounts that can be allocated

                #region Payables
                pWhereClause = "WHERE IsDeleted = 0 AND IsApproved = 1 ";
                if (pPartnerTypeID == constCustodyPartnerTypeID) //if Custody for Payables , i can allocate for other suppliers
                    pWhereClause += " AND PartnerSupplierID IS NOT NULL ";
                else
                {
                // pWhereClause += " AND PartnerSupplierID = " + pPartnerID.ToString();
                // pWhereClause += " AND SupplierPartnerTypeID = " + pPartnerTypeID.ToString();
                   pWhereClause += " And ClientID = " + pPartnerID.ToString();
            }
               // pWhereClause += " AND PayableStatus IN (N'UnPaid' , N'Partially Paid') ";
                if (pSearchText != null)
                {
                    pWhereClause += " AND (";
                    pWhereClause += "       OperationCode like N'%" + pSearchText + "%'";
                    pWhereClause += "    OR SupplierInvoiceNo like N'%" + pSearchText + "%'";
                    pWhereClause += "    OR ChargeTypeCode like N'%" + pSearchText + "%'";
                    pWhereClause += "    OR PartnerSupplierName like N'%" + pSearchText + "%'";
                    pWhereClause += "     )";
                }
                if (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "ELI")
                    pWhereClause += " AND year(IssueDate) > 2020 ";

             pWhereClause += " AND(IsOfficial = 1 AND (isnull(CostAmount,0) - isnull(OfficialAmountPaid,0)) > 0)";

            checkException = objCvwPayables.GetListPaging(1000, 1, pWhereClause, "CreationDate DESC", out _RowCount);
                #endregion Payables

            if (checkException == null)
            {
                _result = true;
            }
            CvwPayablesAllocationsItems objCvwPayablesAllocationsItems = new CvwPayablesAllocationsItems();

            string pWhere = "Where PartnerSupplierID = " + pPartnerID.ToString() + "  AND SupplierPartnerTypeID = " + pPartnerTypeID.ToString() + " AND CurrencyID = " + currency + "";
            if (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "ELI")
            {
                pWhere += " AND year(IssueDate) > 2020 ";
            }
            if (pSearchText != null)
            {
                pWhere += pSearchText;
            }
            objCvwPayablesAllocationsItems.GetList(pWhere);

            //pWhereClause += " AND CurrencyID = " + currency + "";
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                _result
                , pAllocationType == constTransactionReceivableAllocation ? serializer.Serialize(objCvwInvoices.lstCVarvwInvoices) : null //pData[1]
                //, _result ? new JavaScriptSerializer().Serialize(objCvwAccUnAllocatedPartnerBalance.lstCVarvwAccUnAllocatedPartnerBalance) : null //pData[2]
                , _result ? new JavaScriptSerializer().Serialize(pAvailableAmounts) : null //pData[2]
                , pAllocationType == constTransactionPayableAllocation ?  serializer.Serialize(objCvwPayables.lstCVarvwPayables) : null //pData[3]
                , ptxtAvailableBalance //pData[4]
                , AccountID
                , SubAccountID
                , serializer.Serialize(objCvwPayablesAllocationsItems.lstCVarvwPayablesAllocationsItems)//7
            };
        }





        [HttpGet, HttpPost]
        public object[] ARAllocation_Save(Int32 pPartnerID, Int32 pPartnerTypeID, string pPartnerName, Int32 pBranchID, string pAllocationItemsIDs, string pInvoiceNumbers, string pPartnerIDList, string pPartnerTypeIDList, string pCharge, string pOperationCode
            , string pAmounts, string pItemCurrencyIDs, string pBalanceCurrencyIDs, string pItemCurrencyCodes
            , string pBalanceCurrencyCodes, string pExchangeRates, string pBalCurLocalExRates, string pInvCurLocalExRates, Int32 pTransactionType)
        {
            bool _result = false;
            string pUpdateList = "";
            Exception checkException = null;
            int constTransactionReceivableAllocation = 40; //InvoiceAllocation
            int constTransactionPayableAllocation = 80; //Op.Payable (Allocation)
            int constTransactionPayableAllocatedFromCustody = 85;
            int constTransactionCreditTransfer = 50;
            int constTransactionDebitTransfer = 60;
            CVarAccInvoicePaymentDetails objCVarAccInvoicePaymentDetails = new CVarAccInvoicePaymentDetails();
            CAccInvoicePaymentDetails objCAccInvoicePaymentDetails = new CAccInvoicePaymentDetails();
            CInvoices objCInvoices = new CInvoices();
            CPayables objCPayables = new CPayables();
            CAccPartnerBalance objCAccPartnerBalance = new CAccPartnerBalance();
            Int32 NumberOfInvoicesAllocated = pAllocationItemsIDs.Split(',').Count();
            var ArrAllocationItemsIDs = pAllocationItemsIDs.Split(',');
            var ArrInvoiceNumbers = pInvoiceNumbers.Split(',');
            var ArrPartnerID = pPartnerIDList.Split(',');
            var ArrPartnerTypeID = pPartnerTypeIDList.Split(',');
            var ArrCharge = pCharge.Split(',');
            var ArrOperationCode = pOperationCode.Split(',');
            var ArrAmounts = pAmounts.Split(',');
            var ArrItemCurrencyIDs = pItemCurrencyIDs.Split(',');
            var ArrBalanceCurrencyIDs = pBalanceCurrencyIDs.Split(',');
            var ArrItemCurrencyCodes = pItemCurrencyCodes.Split(',');
            var ArrBalanceCurrencyCodes = pBalanceCurrencyCodes.Split(',');
            var ArrExchangeRates = pExchangeRates.Split(',');
            var ArrBalCurLocalExRates = pBalCurLocalExRates.Split(',');
            var ArrInvCurLocalExRates = pInvCurLocalExRates.Split(',');
            //---
            //var AccInvoicePaymentDetailsIDs = "";
            var AccPartnerBalanceIDs = "";
            //---
            for (int i = 0; i < NumberOfInvoicesAllocated; i++)
            {
                #region Add InvoicePaymentDetails if its Receivable
                if (pTransactionType == constTransactionReceivableAllocation)
                {
                    if (objCAccInvoicePaymentDetails.lstCVarAccInvoicePaymentDetails.Count > 0)
                    {
                        objCAccInvoicePaymentDetails.lstCVarAccInvoicePaymentDetails.RemoveAt(0);
                        objCVarAccInvoicePaymentDetails.ID = 0;
                    }
                    objCVarAccInvoicePaymentDetails.InvoicePaymentNumber = "0";
                    objCVarAccInvoicePaymentDetails.BranchID = pBranchID;
                    objCVarAccInvoicePaymentDetails.InvoiceID = Int64.Parse(ArrAllocationItemsIDs[i]);
                    objCVarAccInvoicePaymentDetails.Amount = decimal.Parse(ArrAmounts[i]);
                    objCVarAccInvoicePaymentDetails.CurrencyID = int.Parse(ArrItemCurrencyIDs[i]); //int.Parse(ArrBalanceCurrencyIDs[i]);
                    //objCVarAccInvoicePaymentDetails.ExchangeRate = decimal.Parse(ArrExchangeRates[i]); //may be not used
                    //objCVarAccInvoicePaymentDetails.ExchangeRate = decimal.Parse(ArrInvCurLocalExRates[i]); //may be not used
                    objCVarAccInvoicePaymentDetails.ExchangeRate = 1; //may be not used
                    objCVarAccInvoicePaymentDetails.Notes = "0";
                    objCVarAccInvoicePaymentDetails.CreatorUserID = objCVarAccInvoicePaymentDetails.ModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarAccInvoicePaymentDetails.CreationDate = objCVarAccInvoicePaymentDetails.ModificationDate = DateTime.Now;
                    objCAccInvoicePaymentDetails.lstCVarAccInvoicePaymentDetails.Add(objCVarAccInvoicePaymentDetails);
                    checkException = objCAccInvoicePaymentDetails.SaveMethod(objCAccInvoicePaymentDetails.lstCVarAccInvoicePaymentDetails);

                    ////---added by MOSTAA
                    //if(checkException.Message != null)
                    //{
                    //    AccInvoicePaymentDetailsIDs += "," + objCVarAccInvoicePaymentDetails.ID;

                    //}
                    ////---

                }
                #endregion Add InvoicePaymentDetails if its Receivable
                //Add Credit-Debit transactions to partner balance According to currency
                #region allocating balance currency and allocation item currency are the same so insert just debit(for receivable)/credit(for payable) row
                if (ArrItemCurrencyIDs[i] == ArrBalanceCurrencyIDs[i]
                    //&& (
                    //       Int16.Parse(ArrPartnerID[i]) != pPartnerID
                    //    || Int16.Parse(ArrPartnerTypeID[i]) != pPartnerTypeID
                    //    )
                    )
                {
                    CVarAccPartnerBalance objCVarAccPartnerBalance = new CVarAccPartnerBalance();
                    if (pTransactionType == constTransactionReceivableAllocation)
                    {
                        objCVarAccPartnerBalance.InvoiceID = Int64.Parse(ArrAllocationItemsIDs[i]);
                        objCVarAccPartnerBalance.InvoicePaymentDetailsID = objCVarAccInvoicePaymentDetails.ID;
                        objCVarAccPartnerBalance.DebitAmount = decimal.Parse(ArrAmounts[i]);
                        objCVarAccPartnerBalance.Notes = "Allocating " + decimal.Round(decimal.Parse(ArrAmounts[i]), 3).ToString() + " " + ArrItemCurrencyCodes[i] + " for Inv " + ArrInvoiceNumbers[i] + ".";
                    }
                    else if (pTransactionType == constTransactionPayableAllocation)
                    {
                        objCVarAccPartnerBalance.OperationPayableID = Int64.Parse(ArrAllocationItemsIDs[i]);
                        objCVarAccPartnerBalance.CreditAmount = decimal.Parse(ArrAmounts[i]);
                        objCVarAccPartnerBalance.Notes = "Payable Allocation(" + ArrCharge[i] + ") - (Op: " + ArrOperationCode[i] + "): Allocating " + decimal.Round(decimal.Parse(ArrAmounts[i]), 3).ToString() + " " + ArrItemCurrencyCodes[i] + " for Supplier Invoice " + ArrInvoiceNumbers[i] + ".";
                    }
                    objCVarAccPartnerBalance.PartnerTypeID = pPartnerTypeID;
                    objCVarAccPartnerBalance.CustomerID = GetPartnerID(1, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalance.AgentID = GetPartnerID(2, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalance.ShippingAgentID = GetPartnerID(3, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalance.CustomsClearanceAgentID = GetPartnerID(4, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalance.ShippingLineID = GetPartnerID(5, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalance.AirlineID = GetPartnerID(6, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalance.TruckerID = GetPartnerID(7, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalance.SupplierID = GetPartnerID(8, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalance.CustodyID = GetPartnerID(20, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalance.CurrencyID = int.Parse(ArrBalanceCurrencyIDs[i]);
                    objCVarAccPartnerBalance.ExchangeRate = 1;// decimal.Parse(ArrExchangeRates[i]); ///////////////////////////////////
                    //objCVarAccPartnerBalance.BalCurLocalExRate = decimal.Parse(ArrBalCurLocalExRates[i]); ///////////////////////////////////
                    objCVarAccPartnerBalance.BalCurLocalExRate = decimal.Parse(ArrInvCurLocalExRates[i]); ///////////////////////////////////
                    objCVarAccPartnerBalance.InvCurLocalExRate = decimal.Parse(ArrInvCurLocalExRates[i]); ///////////////////////////////////
                    objCVarAccPartnerBalance.TransactionType = pTransactionType;
                    objCVarAccPartnerBalance.CreatorUserID = objCVarAccPartnerBalance.mModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarAccPartnerBalance.CreationDate = objCVarAccPartnerBalance.ModificationDate = DateTime.Now;
                    objCVarAccPartnerBalance.JVID = 0;
                    objCAccPartnerBalance.lstCVarAccPartnerBalance.Add(objCVarAccPartnerBalance);
                    checkException = objCAccPartnerBalance.SaveMethod(objCAccPartnerBalance.lstCVarAccPartnerBalance);


                }
                #endregion allocating when balance currency and invoice currency are the same so insert just debit row
                #region allocating when balance currency and invoice currency are diff so insert debit and credit row for conversion
                else if (ArrItemCurrencyIDs[i] != ArrBalanceCurrencyIDs[i]) //different currencies (insert Debit & Credit Rows for Converting Currency then a row for allocation)
                {
                    //conversion debit row
                    CVarAccPartnerBalance objCVarAccPartnerBalanceDebit = new CVarAccPartnerBalance();
                    if (pTransactionType == constTransactionReceivableAllocation)
                    {
                        objCVarAccPartnerBalanceDebit.InvoiceID = Int64.Parse(ArrAllocationItemsIDs[i]);
                        objCVarAccPartnerBalanceDebit.InvoicePaymentDetailsID = objCVarAccInvoicePaymentDetails.ID;
                        objCVarAccPartnerBalanceDebit.DebitAmount = decimal.Parse(ArrAmounts[i]) / decimal.Parse(ArrExchangeRates[i]);
                        objCVarAccPartnerBalanceDebit.TransactionType = constTransactionDebitTransfer; //its conversion but i want this row to appear
                        objCVarAccPartnerBalanceDebit.Notes = "Transferring " + decimal.Round((decimal.Parse(ArrAmounts[i]) / decimal.Parse(ArrExchangeRates[i])), 3).ToString() + " " + ArrBalanceCurrencyCodes[i] + " to " + decimal.Round(decimal.Parse(ArrAmounts[i]), 3).ToString() + " " + ArrItemCurrencyCodes[i] + " for inv " + ArrInvoiceNumbers[i] + ".";
                    }
                    else if (pTransactionType == constTransactionPayableAllocation)
                    {
                        objCVarAccPartnerBalanceDebit.OperationPayableID = Int64.Parse(ArrAllocationItemsIDs[i]);
                        objCVarAccPartnerBalanceDebit.CreditAmount = decimal.Parse(ArrAmounts[i]) / decimal.Parse(ArrExchangeRates[i]);
                        objCVarAccPartnerBalanceDebit.TransactionType = constTransactionCreditTransfer; //its conversion but i want this row to appear
                        objCVarAccPartnerBalanceDebit.Notes = "Transferring " + decimal.Round((decimal.Parse(ArrAmounts[i]) / decimal.Parse(ArrExchangeRates[i])), 3).ToString() + " " + ArrBalanceCurrencyCodes[i] + " to " + decimal.Round(decimal.Parse(ArrAmounts[i]), 3).ToString() + " " + ArrItemCurrencyCodes[i] + " for Supplier Invoice " + ArrInvoiceNumbers[i] + " - Op Code(" + ArrOperationCode[i] + ")" + " - Payable(" + ArrCharge[i] + ")" + ".";
                    }
                    objCVarAccPartnerBalanceDebit.PartnerTypeID = pPartnerTypeID;
                    objCVarAccPartnerBalanceDebit.CustomerID = GetPartnerID(1, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalanceDebit.AgentID = GetPartnerID(2, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalanceDebit.ShippingAgentID = GetPartnerID(3, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalanceDebit.CustomsClearanceAgentID = GetPartnerID(4, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalanceDebit.ShippingLineID = GetPartnerID(5, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalanceDebit.AirlineID = GetPartnerID(6, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalanceDebit.TruckerID = GetPartnerID(7, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalanceDebit.SupplierID = GetPartnerID(8, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalanceDebit.CustodyID = GetPartnerID(20, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalanceDebit.CurrencyID = int.Parse(ArrBalanceCurrencyIDs[i]);
                    objCVarAccPartnerBalanceDebit.ExchangeRate = decimal.Parse(ArrExchangeRates[i]); ///////////////////////////////////
                    objCVarAccPartnerBalanceDebit.BalCurLocalExRate = decimal.Parse(ArrBalCurLocalExRates[i]); ///////////////////////////////////
                    objCVarAccPartnerBalanceDebit.InvCurLocalExRate = decimal.Parse(ArrInvCurLocalExRates[i]); ///////////////////////////////////
                    objCVarAccPartnerBalanceDebit.CreatorUserID = objCVarAccPartnerBalanceDebit.ModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarAccPartnerBalanceDebit.CreationDate = objCVarAccPartnerBalanceDebit.ModificationDate = DateTime.Now;
                    objCVarAccPartnerBalanceDebit.JVID = 0;
                    objCAccPartnerBalance.lstCVarAccPartnerBalance.Add(objCVarAccPartnerBalanceDebit);
                    checkException = objCAccPartnerBalance.SaveMethod(objCAccPartnerBalance.lstCVarAccPartnerBalance);
                    ////---added by MOSTAA
                    if (checkException == null)
                    {
                        AccPartnerBalanceIDs += "," + objCVarAccPartnerBalanceDebit.ID;

                    }
                    ////---
                    objCAccPartnerBalance.lstCVarAccPartnerBalance.Remove(objCVarAccPartnerBalanceDebit);

                    CVarAccPartnerBalance objCVarAccPartnerBalanceCredit = new CVarAccPartnerBalance();
                    if (pTransactionType == constTransactionReceivableAllocation)
                    {
                        objCVarAccPartnerBalanceCredit.InvoiceID = Int64.Parse(ArrAllocationItemsIDs[i]);
                        objCVarAccPartnerBalanceCredit.InvoicePaymentDetailsID = objCVarAccInvoicePaymentDetails.ID;
                        objCVarAccPartnerBalanceCredit.TransactionType = constTransactionCreditTransfer;
                        objCVarAccPartnerBalanceCredit.DebitAmount = decimal.Parse(ArrAmounts[i]);
                        objCVarAccPartnerBalanceCredit.Notes = "Receiving " + decimal.Round((decimal.Parse(ArrAmounts[i])), 3).ToString() + " " + ArrItemCurrencyCodes[i] + " from the " + ArrBalanceCurrencyCodes[i] + " balance for inv " + ArrInvoiceNumbers[i] + ".";
                    }
                    else if (pTransactionType == constTransactionPayableAllocation) //its opposite for Receivable
                    {
                        objCVarAccPartnerBalanceCredit.OperationPayableID = Int64.Parse(ArrAllocationItemsIDs[i]);
                        objCVarAccPartnerBalanceCredit.TransactionType = constTransactionDebitTransfer;
                        objCVarAccPartnerBalanceCredit.CreditAmount = decimal.Parse(ArrAmounts[i]);
                        objCVarAccPartnerBalanceCredit.Notes = "Receiving " + decimal.Round((decimal.Parse(ArrAmounts[i])), 3).ToString() + " " + ArrItemCurrencyCodes[i] + " from the " + ArrBalanceCurrencyCodes[i] + " for Supplier Invoice " + ArrInvoiceNumbers[i] + " - Op Code(" + ArrOperationCode[i] + ")" + " - Payable(" + ArrCharge[i] + ")" + ".";
                    }
                    objCVarAccPartnerBalanceCredit.PartnerTypeID = pPartnerTypeID;
                    objCVarAccPartnerBalanceCredit.CustomerID = GetPartnerID(1, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalanceCredit.AgentID = GetPartnerID(2, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalanceCredit.ShippingAgentID = GetPartnerID(3, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalanceCredit.CustomsClearanceAgentID = GetPartnerID(4, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalanceCredit.ShippingLineID = GetPartnerID(5, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalanceCredit.AirlineID = GetPartnerID(6, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalanceCredit.TruckerID = GetPartnerID(7, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalanceCredit.SupplierID = GetPartnerID(8, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalanceCredit.CustodyID = GetPartnerID(20, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalanceCredit.CurrencyID = int.Parse(ArrItemCurrencyIDs[i]);
                    objCVarAccPartnerBalanceCredit.ExchangeRate = 0; ///////////////////////////////////
                    //objCVarAccPartnerBalanceCredit.BalCurLocalExRate = decimal.Parse(ArrBalCurLocalExRates[i]); ///////////////////////////////////
                    objCVarAccPartnerBalanceCredit.BalCurLocalExRate = decimal.Parse(ArrInvCurLocalExRates[i]); ///////here is the currency of the invoice////////////////////////////
                    objCVarAccPartnerBalanceCredit.InvCurLocalExRate = decimal.Parse(ArrInvCurLocalExRates[i]); ///////////////////////////////////
                    objCVarAccPartnerBalanceCredit.CreatorUserID = objCVarAccPartnerBalanceCredit.ModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarAccPartnerBalanceCredit.CreationDate = objCVarAccPartnerBalanceCredit.ModificationDate = DateTime.Now;
                    objCVarAccPartnerBalanceCredit.JVID = 0;
                    objCAccPartnerBalance.lstCVarAccPartnerBalance.Add(objCVarAccPartnerBalanceCredit);
                    checkException = objCAccPartnerBalance.SaveMethod(objCAccPartnerBalance.lstCVarAccPartnerBalance);
                    //Allocation row
                    objCAccPartnerBalance.lstCVarAccPartnerBalance.Remove(objCVarAccPartnerBalanceCredit);
                    CVarAccPartnerBalance objCVarAccPartnerBalance = new CVarAccPartnerBalance();
                    if (pTransactionType == constTransactionReceivableAllocation)
                    {
                        objCVarAccPartnerBalance.InvoiceID = Int64.Parse(ArrAllocationItemsIDs[i]);
                        objCVarAccPartnerBalance.InvoicePaymentDetailsID = objCVarAccInvoicePaymentDetails.ID;
                        objCVarAccPartnerBalance.Notes = "Allocating transferred " + ArrAmounts[i] + " " + ArrItemCurrencyCodes[i] + " from " + ArrBalanceCurrencyCodes[i] + " balance for inv " + ArrInvoiceNumbers[i] + ".";
                        objCVarAccPartnerBalance.CreditAmount = decimal.Parse(ArrAmounts[i]);
                    }
                    else if (pTransactionType == constTransactionPayableAllocation) //its opposite for Receivable
                    {
                        objCVarAccPartnerBalance.OperationPayableID = Int64.Parse(ArrAllocationItemsIDs[i]);
                        objCVarAccPartnerBalance.Notes = "Allocating transferred " + ArrAmounts[i] + " " + ArrItemCurrencyCodes[i] + " from " + ArrBalanceCurrencyCodes[i] + " balance for Supplier Invoice " + ArrInvoiceNumbers[i] + " - Op Code(" + ArrOperationCode[i] + ")" + " - Payable(" + ArrCharge[i] + ")" + ".";
                        objCVarAccPartnerBalance.DebitAmount = decimal.Parse(ArrAmounts[i]);
                    }
                    objCVarAccPartnerBalance.PartnerTypeID = pPartnerTypeID;
                    objCVarAccPartnerBalance.CustomerID = GetPartnerID(1, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalance.AgentID = GetPartnerID(2, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalance.ShippingAgentID = GetPartnerID(3, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalance.CustomsClearanceAgentID = GetPartnerID(4, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalance.ShippingLineID = GetPartnerID(5, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalance.AirlineID = GetPartnerID(6, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalance.TruckerID = GetPartnerID(7, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalance.SupplierID = GetPartnerID(8, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalance.CustodyID = GetPartnerID(20, pPartnerTypeID, pPartnerID);
                    objCVarAccPartnerBalance.CurrencyID = int.Parse(ArrItemCurrencyIDs[i]);
                    objCVarAccPartnerBalance.ExchangeRate = 0; // decimal.Parse(ArrBalCurLocalExRates[i]); ///////////////////////////////////
                    //objCVarAccPartnerBalance.BalCurLocalExRate = decimal.Parse(ArrBalCurLocalExRates[i]); ///////////////////////////////////
                    objCVarAccPartnerBalance.BalCurLocalExRate = decimal.Parse(ArrInvCurLocalExRates[i]); ///////here is the currency of the invoice////////////////////////////
                    objCVarAccPartnerBalance.InvCurLocalExRate = decimal.Parse(ArrInvCurLocalExRates[i]); ///////////////////////////////////
                    objCVarAccPartnerBalance.TransactionType = pTransactionType;
                    objCVarAccPartnerBalance.CreatorUserID = objCVarAccPartnerBalance.mModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarAccPartnerBalance.CreationDate = objCVarAccPartnerBalance.ModificationDate = DateTime.Now;
                    objCVarAccPartnerBalance.JVID = 0;
                    objCAccPartnerBalance.lstCVarAccPartnerBalance.Add(objCVarAccPartnerBalance);
                    checkException = objCAccPartnerBalance.SaveMethod(objCAccPartnerBalance.lstCVarAccPartnerBalance);
                }
                #endregion allocating when balance currency and invoice currency are diff so insert debit and credit row for conversion
                #region just in case of Custody settlement for another supplier
                if (pTransactionType == constTransactionPayableAllocation
                    && (Int16.Parse(ArrPartnerTypeID[i]) != pPartnerTypeID
                        || Int16.Parse(ArrPartnerID[i]) != pPartnerID
                       )
                    ) //settle for supplier too (currency is considered to be the same in all cases because the money transfer is already done from the balance amount)
                {
                    CVarAccPartnerBalance objCVarAccPartnerBalance = new CVarAccPartnerBalance();
                    objCVarAccPartnerBalance.OperationPayableID = Int64.Parse(ArrAllocationItemsIDs[i]);
                    objCVarAccPartnerBalance.PartnerTypeID = Int16.Parse(ArrPartnerTypeID[i]);
                    objCVarAccPartnerBalance.CustomerID = GetPartnerID(1, Int16.Parse(ArrPartnerTypeID[i]), Int16.Parse(ArrPartnerID[i]));
                    objCVarAccPartnerBalance.AgentID = GetPartnerID(2, Int16.Parse(ArrPartnerTypeID[i]), Int16.Parse(ArrPartnerID[i]));
                    objCVarAccPartnerBalance.ShippingAgentID = GetPartnerID(3, Int16.Parse(ArrPartnerTypeID[i]), Int16.Parse(ArrPartnerID[i]));
                    objCVarAccPartnerBalance.CustomsClearanceAgentID = GetPartnerID(4, Int16.Parse(ArrPartnerTypeID[i]), Int16.Parse(ArrPartnerID[i]));
                    objCVarAccPartnerBalance.ShippingLineID = GetPartnerID(5, Int16.Parse(ArrPartnerTypeID[i]), Int16.Parse(ArrPartnerID[i]));
                    objCVarAccPartnerBalance.AirlineID = GetPartnerID(6, Int16.Parse(ArrPartnerTypeID[i]), Int16.Parse(ArrPartnerID[i]));
                    objCVarAccPartnerBalance.TruckerID = GetPartnerID(7, Int16.Parse(ArrPartnerTypeID[i]), Int16.Parse(ArrPartnerID[i]));
                    objCVarAccPartnerBalance.SupplierID = GetPartnerID(8, Int16.Parse(ArrPartnerTypeID[i]), Int16.Parse(ArrPartnerID[i]));
                    objCVarAccPartnerBalance.CustodyID = GetPartnerID(20, Int16.Parse(ArrPartnerTypeID[i]), Int16.Parse(ArrPartnerID[i]));
                    objCVarAccPartnerBalance.DebitAmount = decimal.Parse(ArrAmounts[i]);

                    objCVarAccPartnerBalance.CurrencyID = int.Parse(ArrItemCurrencyIDs[i]);
                    objCVarAccPartnerBalance.ExchangeRate = 1;// decimal.Parse(ArrExchangeRates[i]); ///////////////////////////////////
                    //objCVarAccPartnerBalance.BalCurLocalExRate = decimal.Parse(ArrBalCurLocalExRates[i]); ///////////////////////////////////
                    objCVarAccPartnerBalance.BalCurLocalExRate = decimal.Parse(ArrInvCurLocalExRates[i]); ///////////////////////////////////
                    objCVarAccPartnerBalance.InvCurLocalExRate = decimal.Parse(ArrInvCurLocalExRates[i]); ///////////////////////////////////
                    objCVarAccPartnerBalance.TransactionType = constTransactionPayableAllocatedFromCustody;
                    objCVarAccPartnerBalance.Notes = "Payable Allocation(" + ArrCharge[i] + ") - (Op: " + ArrOperationCode[i] + ") BY Custody(" + pPartnerName + "): Allocating " + decimal.Round(decimal.Parse(ArrAmounts[i]), 3).ToString() + " " + ArrItemCurrencyCodes[i] + " for Supplier Invoice " + ArrInvoiceNumbers[i] + ".";

                    objCVarAccPartnerBalance.CreatorUserID = objCVarAccPartnerBalance.mModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarAccPartnerBalance.CreationDate = objCVarAccPartnerBalance.ModificationDate = DateTime.Now;
                    objCAccPartnerBalance.lstCVarAccPartnerBalance.Add(objCVarAccPartnerBalance);
                    checkException = objCAccPartnerBalance.SaveMethod(objCAccPartnerBalance.lstCVarAccPartnerBalance);
                }
                #endregion just in case of Custody settlement for another supplier
                #region Update Paid & Remaining Amount
                if (checkException == null)
                {
                    _result = true;
                    //Save Amount Paid & Remaining Amounts
                    if (pTransactionType == constTransactionReceivableAllocation)
                    {
                        pUpdateList = "PaidAmount = (ISNULL(PaidAmount,0) + " + decimal.Parse(ArrAmounts[i]).ToString() + ")";
                        pUpdateList += " , RemainingAmount = (ISNULL(RemainingAmount,0) - " + decimal.Parse(ArrAmounts[i]).ToString() + ")";
                        pUpdateList += " WHERE ID = " + Int64.Parse(ArrAllocationItemsIDs[i]);
                        checkException = objCInvoices.UpdateList(pUpdateList);
                    }
                    else if (pTransactionType == constTransactionPayableAllocation)
                    {
                        pUpdateList = "PaidAmount = (ISNULL(PaidAmount,0) + " + decimal.Parse(ArrAmounts[i]).ToString() + ")";
                        //pUpdateList += " , RemainingAmount = (ISNULL(CostAmount,0) - " + decimal.Parse(ArrAmounts[i]).ToString() + ")";
                        pUpdateList += " WHERE ID = " + Int64.Parse(ArrAllocationItemsIDs[i]);
                        checkException = objCPayables.UpdateList(pUpdateList);
                    }
                }
                #endregion Update Paid & Remaining Amount
            } //EOF for (int i = 0; i < NumberOfInvoicesAllocated; i++)



            if (AccPartnerBalanceIDs.Trim() != "")
            {
                CA_InvoiceAllocation_CreateJV cA_InvoiceAllocation_CreateJV = new CA_InvoiceAllocation_CreateJV();
                var accPartnerBalanceIDs = (AccPartnerBalanceIDs + ",");
                cA_InvoiceAllocation_CreateJV.GetList(accPartnerBalanceIDs,",0,", ",0,", WebSecurity.CurrentUserId);
                //cA_InvoiceAllocation_CreateJV.GetList(accPartnerBalanceIDs, WebSecurity.CurrentUserId);
            }




            return new object[] {
                _result
            };
        }
        public Int32 GetPartnerID(Int32 pCheckedPartnerTypeID, Int32 pReceivedPartnerTypeID, Int32 pPartnerID)
        {
            if (pCheckedPartnerTypeID == pReceivedPartnerTypeID)
                return pPartnerID;
            else
                return 0;

        }

        public List<int?> GetAccount_SubAccountForPartener(int PartenerID, int PartenerTypeID)
        {
            var A_SA = new List<int?>(2) { null, null };
            // 0 Account
            // 1 SubAccount
            switch (PartenerTypeID)
            {
                case 1: //CUSTOMERS
                    {
                        CCustomers partener = new CCustomers();
                        partener.GetList("Where ID = " + PartenerID + "");
                        A_SA[0] = partener.lstCVarCustomers[0].AccountID;
                        A_SA[1] = partener.lstCVarCustomers[0].SubAccountID;
                        break;
                    }
                case 2: //AGENTS
                    {
                        CAgents partener = new CAgents();
                        partener.GetList("Where ID = " + PartenerID + "");
                        A_SA[0] = partener.lstCVarAgents[0].AccountID;
                        A_SA[1] = partener.lstCVarAgents[0].SubAccountID;
                        break;
                    }
                case 3: //SHIPPING AGENTS
                    {
                        CShippingAgents partener = new CShippingAgents();
                        partener.GetList("Where ID = " + PartenerID + "");
                        A_SA[0] = partener.lstCVarShippingAgents[0].AccountID;
                        A_SA[1] = partener.lstCVarShippingAgents[0].SubAccountID;

                        break;
                    }
                case 4: //CUSTOMS CLEARANCE AGENTS
                    {
                        CCustomsClearanceAgents partener = new CCustomsClearanceAgents();
                        partener.GetList("Where ID = " + PartenerID + "");
                        A_SA[0] = partener.lstCVarCustomsClearanceAgents[0].AccountID;
                        A_SA[1] = partener.lstCVarCustomsClearanceAgents[0].SubAccountID;
                        break;
                    }
                case 5: //SHIPPINGLINES
                    {
                        CShippingLines partener = new CShippingLines();
                        partener.GetList("Where ID = " + PartenerID + "");
                        A_SA[0] = partener.lstCVarShippingLines[0].AccountID;
                        A_SA[1] = partener.lstCVarShippingLines[0].SubAccountID;
                        break;
                    }
                case 6: //AIRLINES
                    {
                        CAirlines partener = new CAirlines();
                        partener.GetList("Where ID = " + PartenerID + "");
                        A_SA[0] = partener.lstCVarAirlines[0].AccountID;
                        A_SA[1] = partener.lstCVarAirlines[0].SubAccountID;
                        break;
                    }
                case 7: //TRUCKERS
                    {
                        CTruckers partener = new CTruckers();
                        partener.GetList("Where ID = " + PartenerID + "");
                        A_SA[0] = partener.lstCVarTruckers[0].AccountID;
                        A_SA[1] = partener.lstCVarTruckers[0].SubAccountID;
                        break;
                    }
                case 8: //SUPPLIERS
                    {
                        CSuppliers partener = new CSuppliers();
                        partener.GetList("Where ID = " + PartenerID + "");
                        A_SA[0] = partener.lstCVarSuppliers[0].AccountID;
                        A_SA[1] = partener.lstCVarSuppliers[0].SubAccountID;
                        break;
                    }
                case 20: //CUSTODIES
                    {
                        CCustody partener = new CCustody();
                        partener.GetList("Where ID = " + PartenerID + "");
                        A_SA[0] = partener.lstCVarCustody[0].AccountID;
                        A_SA[1] = partener.lstCVarCustody[0].SubAccountID;
                        break;
                    }
                default:
                    {
                        A_SA[0] = null;
                        A_SA[1] = null;

                    }
                    break;





            }

            return A_SA;
        }


        [HttpGet, HttpPost]
        [AllowAnonymous]
        public object[] InsertOperationsPayment([FromBody]string pItemss)
        {
            var _result = false;
            // Deserialize List -------------------------------------------------------------------------------
            var Listobj = new JavaScriptSerializer().Deserialize<List<CVarA_VoucherOperationsPayment>>(pItemss);
            CA_VoucherOperationsPayment cA_VoucherOperationsPayment = new CA_VoucherOperationsPayment();
            var checkException = cA_VoucherOperationsPayment.SaveMethod(Listobj);
            //------------------------------
            if (checkException == null)
                _result = true;

            return new object[] {
                _result , pItemss
            };
        }
        public object[] InsertA_VoucherInvoicesPayment([FromBody]string pItems)
        {
            var _result = false;
            // Deserialize List -------------------------------------------------------------------------------
            var Listobj = new JavaScriptSerializer().Deserialize<List<CVarA_VoucherInvoicesPayment>>(pItems);
            CA_VoucherInvoicesPayment cA_VoucherInvoicesPayment = new CA_VoucherInvoicesPayment();
            var checkException = cA_VoucherInvoicesPayment.SaveMethod(Listobj);
            //------------------------------
            if (checkException == null)
                _result = true;

            return new object[] {
                _result , pItems
            };
        }

        public object[] InsertA_VoucherPayableClientPayment([FromBody]string pItemsPayables)
        {
            var _result = false;
            // Deserialize List -------------------------------------------------------------------------------
            var Listobj = new JavaScriptSerializer().Deserialize<List<CVarA_VoucherPayableClientPayment>>(pItemsPayables);
            CA_VoucherPayableClientPayment cA_VoucherPayableClientPayment = new CA_VoucherPayableClientPayment();
            var checkException = cA_VoucherPayableClientPayment.SaveMethod(Listobj);
            //------------------------------
            if (checkException == null)
            {
                _result = true;
                for (int i = 0; i < Listobj.Count; i++)
                {
                    CPayables objCPayables = new CPayables();
                    checkException = objCPayables.UpdateList("  OfficialAmountPaid = isnull(OfficialAmountPaid,0) + " + Listobj[i].DueAmount + " Where ID= " + Listobj[i].PayableID);
                }
            }

            if (checkException != null)
                _result = false;

            return new object[] {
                _result , pItemsPayables
            };
        }

        public object[] InsertA_VoucherPayableAllocationPayment([FromBody]string pItems)
        {
            var _result = false;
            // Deserialize List -------------------------------------------------------------------------------
            var Listobj = new JavaScriptSerializer().Deserialize<List<CVarA_VoucherPayableAllocationPayment>>(pItems);
            CA_VoucherPayableAllocationPayment cA_VoucherPayableAllocationPayment = new CA_VoucherPayableAllocationPayment();
            var checkException = cA_VoucherPayableAllocationPayment.SaveMethod(Listobj);
            //------------------------------
            if (checkException == null)
                _result = true;

            return new object[] {
                _result , pItems
            };
        }


        //[HttpGet, HttpPost]
        //    [AllowAnonymous]
        //    public object[] InsertItems([FromBody]string pItems)
        //    {
        //      //  log.Debug("Reveived value: " + value);
        //        var  _result = false;

        //        if (pItems != null)
        //            _result = false;
        //        // Deserialize List -------------------------------------------------------------------------------
        //        //var Listobj = new JavaScriptSerializer().Deserialize<List<CVarA_VoucherInvoicesPayment>>(pItems);
        //        // CA_VoucherInvoicesPayment cA_VoucherInvoicesPayment = new CA_VoucherInvoicesPayment();
        //        //var checkException = cA_VoucherInvoicesPayment.SaveMethod(Listobj);



        //        return new object[] {
        //            _result , pItems
        //        };
        //    }





        [HttpGet, HttpPost]
        public string CheckIsForInvoice(string pVoucherID)
        {
            Exception checkException = null;
            //var ID = int.Parse(pVoucherID);
            CA_VoucherInvoicesPayment cA_VoucherInvoicesPayment = new CA_VoucherInvoicesPayment();
           // checkException =  cA_VoucherInvoicesPayment.GetItem(Convert.ToInt32(pVoucherID));
            checkException = cA_VoucherInvoicesPayment.GetList(" Where VoucherID=" + pVoucherID);


             var _CanEdit = "1";
            if (cA_VoucherInvoicesPayment.lstCVarA_VoucherInvoicesPayment.Count > 0)
            {
                CInvoices cInvoice = new CInvoices();
                cInvoice.GetList("where ID = " + cA_VoucherInvoicesPayment.lstCVarA_VoucherInvoicesPayment[0].InvoiceID);

                if(cInvoice.lstCVarInvoices[0].PaymentTermID != 6)
                   _CanEdit = "0";
            }


            return _CanEdit;

        }




        [HttpGet, HttpPost]
       public Object[] GetPrintedData(Int64 pVoucherIDToPrint)
        {
            Int32 _RowCount = 0;
            CvwA_Voucher objCvwA_Voucher = new CvwA_Voucher();
            CvwA_VoucherDetails objCvwA_VoucherDetails = new CvwA_VoucherDetails();
            CvwVoucherOperationsPayment objCvwVoucherOperationsPayment = new CvwVoucherOperationsPayment();
            CvwA_InvoiceItemsByVoucher objInvItem = new CvwA_InvoiceItemsByVoucher();
        
            CvwDefaults objCvwDefaults = new CvwDefaults();
            objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa

            string CompanyName = objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName;

            object[] objPost =new object[2];
            string pMessage = "";
            if (CompanyName == "SAF" || CompanyName == "SWI")
            {
                objPost = SetPostField(pVoucherIDToPrint.ToString(), DateTime.Now, "1", false);
                pMessage = objPost[1].ToString();

                if (pMessage == "" )
                {
                    objCvwA_Voucher.GetListPaging(1, 1, "WHERE ID = " + pVoucherIDToPrint.ToString(), "ID", out _RowCount);
                    objCvwA_VoucherDetails.GetListPaging(100, 1, "WHERE  VoucherID = " + pVoucherIDToPrint.ToString(), "ID", out _RowCount);
                    objCvwVoucherOperationsPayment.GetList(pVoucherIDToPrint.ToString());
                    objInvItem.GetListPaging(100, 1, "WHERE  VoucherID = " + pVoucherIDToPrint.ToString(), "VoucherID", out _RowCount);
                }
            }
            else
            {
                objCvwA_Voucher.GetListPaging(1, 1, "WHERE ID = " + pVoucherIDToPrint.ToString(), "ID", out _RowCount);
                objCvwA_VoucherDetails.GetListPaging(100, 1, "WHERE  VoucherID = " + pVoucherIDToPrint.ToString(), "ID", out _RowCount);
                objCvwVoucherOperationsPayment.GetList(pVoucherIDToPrint.ToString());
                objInvItem.GetListPaging(100, 1, "WHERE  VoucherID = " + pVoucherIDToPrint.ToString(), "VoucherID", out _RowCount);
            }
   
            

            return new Object[] {
                new JavaScriptSerializer().Serialize(objCvwA_Voucher.lstCVarvwA_Voucher[0]) //var pVoucherHeader = pData[0];
                , new JavaScriptSerializer().Serialize(objCvwA_VoucherDetails.lstCVarvwA_VoucherDetails) //var pVoucherDetails = pData[1];
                , new JavaScriptSerializer().Serialize(objCvwVoucherOperationsPayment.lstCVarvwVoucherOperationsPayment)
                , new JavaScriptSerializer().Serialize(objInvItem.lstCVarvwA_InvoiceItemsByVoucher) //pBank = pData[3]

            };
        }

        [HttpGet, HttpPost]
        public Object[] GetPrintedDataByDate(DateTime pFromDate , DateTime pToDate , Int32 pVoucherTypeID)
        {
            Int32 _RowCount = 0;
            CvwA_Voucher objCvwA_Voucher = new CvwA_Voucher();
            CvwA_VoucherDetails objCvwA_VoucherDetails = new CvwA_VoucherDetails();
            CvwVoucherOperationsPayment objCvwVoucherOperationsPayment = new CvwVoucherOperationsPayment();
            CvwA_InvoiceItemsByVoucher objInvItem = new CvwA_InvoiceItemsByVoucher();

            CvwDefaults objCvwDefaults = new CvwDefaults();
            objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa

            string CompanyName = objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName;

            Exception checkException = null;
            object[] objPost = new object[2];
            string pMessage = "";

            checkException= objCvwA_Voucher.GetListPaging(1000, 1, "WHERE VoucherType = " + pVoucherTypeID.ToString()
                    + " and VoucherDate between '" + pFromDate.ToString("yyyy/MM/dd") + "' and '" + pToDate.ToString("yyyy/MM/dd 23:59:59") + "'", "ID", out _RowCount);

            checkException= objCvwA_VoucherDetails.GetListPaging(9999, 1, "WHERE  VoucherID in ( " + " select V.ID from A_Voucher V where VoucherType = " 
                + pVoucherTypeID.ToString() + " and VoucherDate between '" + pFromDate.ToString("yyyy/MM/dd") + "' and '" + pToDate.ToString("yyyy/MM/dd 23:59:59") + "'"
                 + ")", "ID", out _RowCount);
               // objCvwVoucherOperationsPayment.GetList(pVoucherIDToPrint.ToString());
               // objInvItem.GetListPaging(100, 1, "WHERE  VoucherID = " + pVoucherIDToPrint.ToString(), "VoucherID", out _RowCount);




            return new Object[] {
                new JavaScriptSerializer().Serialize(objCvwA_Voucher.lstCVarvwA_Voucher) //var pVoucherHeader = pData[0];
                , new JavaScriptSerializer().Serialize(objCvwA_VoucherDetails.lstCVarvwA_VoucherDetails) //var pVoucherDetails = pData[1];
                , new JavaScriptSerializer().Serialize(objCvwVoucherOperationsPayment.lstCVarvwVoucherOperationsPayment)
                , new JavaScriptSerializer().Serialize(objInvItem.lstCVarvwA_InvoiceItemsByVoucher) //pBank = pData[3]

            };
        }

        [HttpGet, HttpPost]
        public object[] Delete(string pDeletedIDs, bool pCheckFiscalClosed)
        {
            bool _result = true;
            Exception checkException = null;
            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
            CAccPartnerBalance objCAccPartnerBalanceDebit = new CAccPartnerBalance();
            CA_Voucher objCA_Voucher = new CA_Voucher();
            CA_VoucherDetails objCA_VoucherDetails = new CA_VoucherDetails();
            CA_VoucherOperationsPayment objCA_VoucherOperationsPayment = new CA_VoucherOperationsPayment();
            CA_Fiscal_Year objCA_Fiscal_Year = new CA_Fiscal_Year();
            CA_VoucherInvoicesPayment cA_VoucherInvoicesPayment = new CA_VoucherInvoicesPayment();
            CA_Fiscal_Year_Period objCA_Fiscal_Year_Period = new CA_Fiscal_Year_Period();
            CFreezeSystemTransactions objCFreezeSystemTransactions = new CFreezeSystemTransactions();
            CSL_InvoiceJVs objCSL_InvoiceJVs = new CSL_InvoiceJVs();
            checkException = objCA_Voucher.GetList("WHERE ID IN (" + pDeletedIDs + ")");
            int NumberOfSelectedRows = pDeletedIDs.Split(',').Length;
            var ArrDeletedIDs = pDeletedIDs.Split(',');
            for (int i = 0; i < NumberOfSelectedRows; i++)
            {
                DateTime pVoucherDate = objCA_Voucher.lstCVarA_Voucher[i].VoucherDate;
                checkException = objCSL_InvoiceJVs.GetList("WHERE VoucherID=" + ArrDeletedIDs[i]);
                checkException = cA_VoucherInvoicesPayment.GetList("WHERE VoucherID=" + ArrDeletedIDs[i]);
                checkException = objCA_Fiscal_Year_Period.GetList("WHERE '" + pVoucherDate.ToString("yyyy/MM/dd") + "' BETWEEN From_Date AND To_Date and Closed=1");
                checkException = objCFreezeSystemTransactions.GetList("WHERE TransType='All' AND ('" + pVoucherDate.ToString("yyyy/MM/dd") + "' BETWEEN FromDate AND ToDate)");

                if (objCA_Voucher.lstCVarA_Voucher[i].Posted == false &&
                    objCSL_InvoiceJVs.lstCVarSL_InvoiceJVs.Count == 0 //if count>0 then this Voucher is generated from invoice posting
                                                                      //&&
                                                                      //cA_VoucherInvoicesPayment.lstCVarA_VoucherInvoicesPayment.Count == 0
                    &&
                    objCFreezeSystemTransactions.lstCVarFreezeSystemTransactions.Count == 0
                    &&
                    (objCA_Fiscal_Year_Period.lstCVarA_Fiscal_Year_Period.Count == 0 || !pCheckFiscalClosed)) //not closed nor frozen period so delete
                {
                    var pInvoiceIDs = string.Join(",", cA_VoucherInvoicesPayment.lstCVarA_VoucherInvoicesPayment.Select(x => x.InvoiceID).ToList());
                    var pDueAmounts = string.Join(",", cA_VoucherInvoicesPayment.lstCVarA_VoucherInvoicesPayment.Select(x => x.DueAmount).ToList());
                    ARUpdateCashInvoicePaidAfterDelete(pInvoiceIDs, pDueAmounts);

                    var pAccPartnerBalanceIDs = string.Join(",", cA_VoucherInvoicesPayment.lstCVarA_VoucherInvoicesPayment.Select(x => x.AccPartnerBalanceID).ToList());
                    checkException = cA_VoucherInvoicesPayment.DeleteList("where VoucherID = " + ArrDeletedIDs[i]);
                    if (pAccPartnerBalanceIDs != "")
                    {
                        foreach (var CurrentID in pAccPartnerBalanceIDs.Split(','))
                        {
                            checkException = objCAccPartnerBalanceDebit.DeleteList("where ID =" + CurrentID);
                        }
                    }
                    checkException = objCA_VoucherOperationsPayment.DeleteList("where VoucherID=" + ArrDeletedIDs[i]);
                    checkException = objCA_VoucherDetails.DeleteList("WHERE VoucherID=" + ArrDeletedIDs[i]);

                    if (checkException == null)
                    {
                        CPayables objCPayables = new CPayables();
                        CA_VoucherPayableClientPayment objCA_VoucherPayableClientPayment = new CA_VoucherPayableClientPayment();

                        checkException = objCA_VoucherPayableClientPayment.GetList("WHERE VoucherID=" + ArrDeletedIDs[i]);

                        var pPayableIDs = string.Join(",", objCA_VoucherPayableClientPayment.lstCVarA_VoucherPayableClientPayment.Select(x => x.PayableID).ToList());
                        var pPayableDueAmounts = string.Join(",", objCA_VoucherPayableClientPayment.lstCVarA_VoucherPayableClientPayment.Select(x => x.DueAmount).ToList());

                        Int32 NumberOfPayablesAllocated = pPayableIDs.Split(',').Count();
                        var ArrAllocationItemsIDs = pPayableIDs.Split(',');
                        var ArrAmounts = pPayableDueAmounts.Split(',');

                        checkException = cA_VoucherInvoicesPayment.GetList("WHERE VoucherID=" + ArrDeletedIDs[i]);

                        string pUpdateList = "";
                        for (int k = 0; k < NumberOfPayablesAllocated; k++)
                        {
                            if (checkException == null && ArrAllocationItemsIDs[k] != "")
                            {
                                _result = true;

                                pUpdateList = "OfficialAmountPaid = (ISNULL(OfficialAmountPaid,0) - " + decimal.Parse(ArrAmounts[k]).ToString() + ")";
                                pUpdateList += " WHERE ID = " + Int64.Parse(ArrAllocationItemsIDs[k]);
                                checkException = objCPayables.UpdateList(pUpdateList);

                            }
                        }

                        checkException = objCA_VoucherPayableClientPayment.DeleteList("where VoucherID=" + ArrDeletedIDs[i]);
                    }



                    //CA_PaymentRequest cA_PaymentRequest = new CA_PaymentRequest();
                    //checkException = cA_PaymentRequest.UpdateList(" VoucherID = null where VoucherID =" + ArrDeletedIDs[i] + " ");

                    checkException = objCA_Voucher.DeleteList("WHERE ID=" + ArrDeletedIDs[i]);



                    objCCustomizedDBCall.SP_Trans_Log("SP_Trans_Log", WebSecurity.CurrentUserId, "A_Voucher", Int64.Parse(ArrDeletedIDs[i]), "D");
                }
                else
                    _result = false;
            }
            return new object[] {
                _result
            };
        }
        [HttpGet, HttpPost]
        public object[] DeleteCashVoucher(string pDeletedIDs, bool pCheckFiscalClosed,string FormName)
        {
            bool _result = true;
            Exception checkException = null;
            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
            CAccPartnerBalance objCAccPartnerBalanceDebit = new CAccPartnerBalance();
            CA_Voucher objCA_Voucher = new CA_Voucher();
            CA_VoucherDetails objCA_VoucherDetails = new CA_VoucherDetails();
            CA_VoucherOperationsPayment objCA_VoucherOperationsPayment = new CA_VoucherOperationsPayment();
            CA_PayablesAllocation objCA_PayablesAllocation = new CA_PayablesAllocation();
            CA_VoucherPayableAllocationPayment objCA_VoucherPayableAllocationPayment = new CA_VoucherPayableAllocationPayment();
            CA_Fiscal_Year objCA_Fiscal_Year = new CA_Fiscal_Year();
            CA_VoucherInvoicesPayment cA_VoucherInvoicesPayment = new CA_VoucherInvoicesPayment();
            CA_Fiscal_Year_Period objCA_Fiscal_Year_Period = new CA_Fiscal_Year_Period();
            CFreezeSystemTransactions objCFreezeSystemTransactions = new CFreezeSystemTransactions();
            CSL_InvoiceJVs objCSL_InvoiceJVs = new CSL_InvoiceJVs();
            checkException = objCA_Voucher.GetList("WHERE ID IN (" + pDeletedIDs + ")");
            int NumberOfSelectedRows = pDeletedIDs.Split(',').Length;
            var ArrDeletedIDs = pDeletedIDs.Split(',');
            for (int i = 0; i < NumberOfSelectedRows; i++)
            {
                DateTime pVoucherDate = objCA_Voucher.lstCVarA_Voucher[i].VoucherDate;
                checkException = objCSL_InvoiceJVs.GetList("WHERE VoucherID=" + ArrDeletedIDs[i]);
                checkException = cA_VoucherInvoicesPayment.GetList("WHERE VoucherID=" + ArrDeletedIDs[i]);
                checkException = objCA_Fiscal_Year_Period.GetList("WHERE '" + pVoucherDate.ToString("yyyy/MM/dd") + "' BETWEEN From_Date AND To_Date and Closed=1");
                checkException = objCFreezeSystemTransactions.GetList("WHERE TransType='All' AND ('" + pVoucherDate.ToString("yyyy/MM/dd") + "' BETWEEN FromDate AND ToDate)");

                if (objCA_Voucher.lstCVarA_Voucher[i].Posted == false &&
                    objCSL_InvoiceJVs.lstCVarSL_InvoiceJVs.Count == 0 //if count>0 then this Voucher is generated from invoice posting
                                                                      //&&
                                                                      //cA_VoucherInvoicesPayment.lstCVarA_VoucherInvoicesPayment.Count == 0
                    &&
                    objCFreezeSystemTransactions.lstCVarFreezeSystemTransactions.Count == 0
                    &&
                    (objCA_Fiscal_Year_Period.lstCVarA_Fiscal_Year_Period.Count == 0 || !pCheckFiscalClosed)) //not closed nor frozen period so delete
                {
                    var pInvoiceIDs = string.Join(",", cA_VoucherInvoicesPayment.lstCVarA_VoucherInvoicesPayment.Select(x => x.InvoiceID).ToList());
                    var pDueAmounts = string.Join(",", cA_VoucherInvoicesPayment.lstCVarA_VoucherInvoicesPayment.Select(x => x.DueAmount).ToList());
                    //ARUpdateCashInvoicePaidAfterDelete(pInvoiceIDs, pDueAmounts);

                    var pAccPartnerBalanceIDs = string.Join(",", cA_VoucherInvoicesPayment.lstCVarA_VoucherInvoicesPayment.Select(x => x.AccPartnerBalanceID).ToList());
                    //checkException = cA_VoucherInvoicesPayment.DeleteList("where VoucherID = " + ArrDeletedIDs[i]);
                    //if (pAccPartnerBalanceIDs != "")
                    //{
                    //    foreach (var CurrentID in pAccPartnerBalanceIDs.Split(','))
                    //    {
                    //        checkException = objCAccPartnerBalanceDebit.DeleteList("where ID =" + CurrentID);
                    //    }
                    //}
                    checkException = objCA_VoucherOperationsPayment.DeleteList("where VoucherID=" + ArrDeletedIDs[i]);
                    checkException = objCA_VoucherDetails.DeleteList("WHERE VoucherID=" + ArrDeletedIDs[i]);
                    checkException = objCA_Voucher.DeleteList("WHERE ID=" + ArrDeletedIDs[i]);

                    objCA_VoucherPayableAllocationPayment.GetList(" Where VoucherID = "+ ArrDeletedIDs[i]);
                    string pUpdateList = "";
                    CPayables objCPayables = new CPayables();
                    

    

                    for (int j=0; j< objCA_VoucherPayableAllocationPayment.lstCVarA_VoucherPayableAllocationPayment.Count;j++)
                    {
                        checkException = objCA_PayablesAllocation.GetList("where ID=" + objCA_VoucherPayableAllocationPayment.lstCVarA_VoucherPayableAllocationPayment[j].A_PayablesAllocationID);

                        for (int l = 0; l < objCA_PayablesAllocation.lstCVarA_PayablesAllocation.Count; l++)
                        {
                            pUpdateList = "PaidAmount = (ISNULL(PaidAmount,0) - " + objCA_PayablesAllocation.lstCVarA_PayablesAllocation[l].AmountDue.ToString() + ")";
                            //pUpdateList += " , RemainingAmount = (ISNULL(CostAmount,0) - " + decimal.Parse(ArrAmounts[i]).ToString() + ")";
                            pUpdateList += " WHERE ID = " + objCA_PayablesAllocation.lstCVarA_PayablesAllocation[l].PayableID.ToString();
                            checkException = objCPayables.UpdateList(pUpdateList);

                        }

                        checkException = objCA_PayablesAllocation.DeleteList("WHERE ID=" + objCA_VoucherPayableAllocationPayment.lstCVarA_VoucherPayableAllocationPayment[j].A_PayablesAllocationID);
                    }
                    checkException = objCA_VoucherPayableAllocationPayment.DeleteList(" Where VoucherID = " + ArrDeletedIDs[i]);


                    objCCustomizedDBCall.SP_Trans_Log("SP_Trans_Log", WebSecurity.CurrentUserId, "A_Voucher", Int64.Parse(ArrDeletedIDs[i]), "D");
                }
                else
                    _result = false;
            }
            return new object[] {
                _result
            };
        }

        [HttpGet, HttpPost]
        public object[] ARUpdateCashInvoicePaidAfterDelete(string pInvoiceIDs, string pDueAmounts)
        {
            bool _result = false;
            string pUpdateList = "";
            Exception checkException = null;
            var ArrAmounts = pDueAmounts.Split(',');
            var ArrAllocationItemsIDs = pInvoiceIDs.Split(',');
            Int32 NumberOfInvoicesAllocated = pInvoiceIDs.Split(',').Count();
            CInvoices objCInvoices = new CInvoices();

            for (int i = 0; i < NumberOfInvoicesAllocated; i++)
            {
                if (checkException == null && ArrAllocationItemsIDs[i] != "")
                {
                    _result = true;

                    pUpdateList = "PaidAmount = (ISNULL(PaidAmount,0) - " + decimal.Parse(ArrAmounts[i]).ToString() + ")";
                    pUpdateList += " , RemainingAmount = (ISNULL(RemainingAmount,0) + " + decimal.Parse(ArrAmounts[i]).ToString() + ")";
                    pUpdateList += " WHERE ID = " + Int64.Parse(ArrAllocationItemsIDs[i]);
                    checkException = objCInvoices.UpdateList(pUpdateList);

                }
            }
            return new object[] {
                _result
            };
        }

        #region Details
        [HttpPost]
        public object[] VoucherDetails_Save([FromBody] ParamVoucherDetails_Save paramVoucherDetails_Save)
        {

            string VCODE = "0";
            string pMessageReturned = "";
            string pUpdateClause = "";
            Exception checkException = null;
            CA_Fiscal_Year objCA_Fiscal_Year = new CA_Fiscal_Year();
            CA_Fiscal_Year_Period objCA_Fiscal_Year_Period = new CA_Fiscal_Year_Period();
            CFreezeSystemTransactions objCFreezeSystemTransactions = new CFreezeSystemTransactions();
            CA_Voucher objCA_Voucher = new CA_Voucher();
            CA_VoucherDetails objCA_VoucherDetails = new CA_VoucherDetails();
            CvwA_VoucherDetails objCvwA_VoucherDetails = new CvwA_VoucherDetails();
            CVarA_Voucher objCVarA_Voucher = new CVarA_Voucher();
            CVarA_VoucherDetails objCVarA_VoucherDetails = new CVarA_VoucherDetails();
            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
            checkException = objCA_Fiscal_Year.GetList("WHERE Confirmed=1 AND Fiscal_Year_Name=N'" + paramVoucherDetails_Save.pVoucherDate.Year + "'");
            checkException = objCA_Fiscal_Year_Period.GetList("WHERE '" + paramVoucherDetails_Save.pVoucherDate.ToString("yyyy/MM/dd") + "' BETWEEN From_Date AND To_Date and Closed=1");

            checkException = objCFreezeSystemTransactions.GetList("WHERE TransType='All' AND ('" + paramVoucherDetails_Save.pVoucherDate.ToString("yyyy/MM/dd") + "' BETWEEN FromDate AND ToDate)");
            #region Check FiscalYear is Confirmed and NOT Closed
            if (objCA_Fiscal_Year.lstCVarA_Fiscal_Year.Count == 0)
                pMessageReturned = "This fiscal year is not confirmed.";
            else if (objCA_Fiscal_Year_Period.lstCVarA_Fiscal_Year_Period.Count > 0)
                pMessageReturned = "This fiscal year is closed.";
            #endregion Check FiscalYear is Confirmed and NOT Closed
            #region Check VoucherCode
            //if (objCA_Voucher.lstCVarA_Voucher.Count > 0)
            //    pMessageReturned = "This code already exists for this year.";
            #endregion Check VoucherCode
            #region Check Period is Not Frozen
            if (objCFreezeSystemTransactions.lstCVarFreezeSystemTransactions.Count > 0)
                pMessageReturned = "The transactions for this date is frozen.";
            #endregion Check Period is Not Frozen
            #region Check If Posted
            if (paramVoucherDetails_Save.pID != 0)
            {
                objCA_Voucher.GetList("where ID= " + paramVoucherDetails_Save.pID);
                bool Posted = objCA_Voucher.lstCVarA_Voucher[0].Posted;
                if (Posted)
                    pMessageReturned = "Voucher Already Posted.";
            }
            #endregion

            checkException = objCA_Voucher.GetList("WHERE ID<>" + paramVoucherDetails_Save.pID + "AND Code=N'" + paramVoucherDetails_Save.pCode + "' AND DATEPART(yyyy, VoucherDate)=" + paramVoucherDetails_Save.pVoucherDate.Year + " AND VoucherType=" + paramVoucherDetails_Save.pVoucherType); //if return row then code is not unique
            if (objCA_Voucher.lstCVarA_Voucher.Count > 0 && paramVoucherDetails_Save.pID != 0)
                pMessageReturned = "This code already exists for this year.";
            #region Save
            if (pMessageReturned == "")
            {
                #region Save Header
                if (paramVoucherDetails_Save.pID == 0) //insert header
                {

                    //if (paramVoucherDetails_Save.pCode != ""//objCA_Voucher.lstCVarA_Voucher.Count > 0
                    //    )
                    //    VCODE = paramVoucherDetails_Save.pCode;                
                    //  else
                    VCODE = Convert.ToString(GetCode(paramVoucherDetails_Save.pSafeID, paramVoucherDetails_Save.pBankID, paramVoucherDetails_Save.pVoucherDate, paramVoucherDetails_Save.pVoucherType)[0]);

                    objCVarA_Voucher.Code = VCODE;
                    objCVarA_Voucher.VoucherDate = paramVoucherDetails_Save.pVoucherDate;
                    objCVarA_Voucher.SafeID = paramVoucherDetails_Save.pSafeID;
                    objCVarA_Voucher.CurrencyID = paramVoucherDetails_Save.pCurrencyID;
                    objCVarA_Voucher.ExchangeRate = paramVoucherDetails_Save.pExchangeRate;
                    objCVarA_Voucher.ChargedPerson = paramVoucherDetails_Save.pChargedPerson;
                    objCVarA_Voucher.Notes = paramVoucherDetails_Save.pNotes;
                    objCVarA_Voucher.TaxID = paramVoucherDetails_Save.pTaxID;
                    objCVarA_Voucher.TaxValue = paramVoucherDetails_Save.pTaxValue;
                    objCVarA_Voucher.TaxSign = paramVoucherDetails_Save.pTaxSign;
                    objCVarA_Voucher.TaxID2 = paramVoucherDetails_Save.pTaxID2;
                    objCVarA_Voucher.TaxValue2 = paramVoucherDetails_Save.pTaxValue2;
                    objCVarA_Voucher.TaxSign2 = paramVoucherDetails_Save.pTaxSign2;
                    objCVarA_Voucher.Total = paramVoucherDetails_Save.pTotal;
                    objCVarA_Voucher.TotalAfterTax = paramVoucherDetails_Save.pTotalAfterTax;
                    //objCVarA_Voucher.Approved = false;
                    //objCVarA_Voucher.Posted = false;
                    objCVarA_Voucher.IsAGInvoice = paramVoucherDetails_Save.pIsAGInvoice;
                    objCVarA_Voucher.AGInvType_ID = paramVoucherDetails_Save.pAGInvType_ID;
                    objCVarA_Voucher.Inv_No = paramVoucherDetails_Save.pInv_No;
                    objCVarA_Voucher.InvoiceID = paramVoucherDetails_Save.pInvoiceID;
                    ////Set from posting screen
                    //objCVarA_Voucher.JVID1 = pJVID1;
                    //objCVarA_Voucher.JVID2 = pJVID2;
                    //objCVarA_Voucher.JVID3 = pJVID3;
                    //objCVarA_Voucher.JVID4 = pJVID4;
                    objCVarA_Voucher.SalesManID = paramVoucherDetails_Save.pSalesManID;
                    objCVarA_Voucher.forwOperationID = paramVoucherDetails_Save.pforwOperationID;
                    objCVarA_Voucher.IsCustomClearance = paramVoucherDetails_Save.pIsCustomClearance;
                    objCVarA_Voucher.TransType_ID = paramVoucherDetails_Save.pTransType_ID;
                    objCVarA_Voucher.VoucherType = paramVoucherDetails_Save.pVoucherType;
                    objCVarA_Voucher.IsCash = paramVoucherDetails_Save.pIsCash;
                    objCVarA_Voucher.IsCheque = paramVoucherDetails_Save.pIsCheque;
                    objCVarA_Voucher.PrintDate = paramVoucherDetails_Save.pPrintDate;
                    objCVarA_Voucher.ChequeNo = paramVoucherDetails_Save.pChequeNo;
                    objCVarA_Voucher.ChequeDate = paramVoucherDetails_Save.pChequeDate;
                    objCVarA_Voucher.BankID = paramVoucherDetails_Save.pBankID;
                    objCVarA_Voucher.DisbursementJob_ID = paramVoucherDetails_Save.pDisbursementJob_ID;
                    objCVarA_Voucher.SL_SalesManID = paramVoucherDetails_Save.pSL_SalesManID;
                    objCVarA_Voucher.IBAN = "0";
                    objCVarA_Voucher.ReferenceNo = "0";
                    objCVarA_Voucher.DepositeDate = Convert.ToDateTime("1/1/1900");
                    objCVarA_Voucher.TransferDate = Convert.ToDateTime("1/1/1900");

                    objCVarA_Voucher.PaidAmount = 0;
                    objCVarA_Voucher.RemainAmount = paramVoucherDetails_Save.pTotalAfterTax;
                    objCVarA_Voucher.isTransfer = false;

                    objCVarA_Voucher.OtherSideBankName = paramVoucherDetails_Save.pOtherSideBankName;
                    objCVarA_Voucher.CollectionDate = paramVoucherDetails_Save.pCollectionDate;
                    objCVarA_Voucher.CollectionExpense = paramVoucherDetails_Save.pCollectionExpense;
                    objCA_Voucher.lstCVarA_Voucher.Add(objCVarA_Voucher);
                    checkException = objCA_Voucher.SaveMethod(objCA_Voucher.lstCVarA_Voucher);
                    if (checkException == null)
                    {
                        objCCustomizedDBCall.SP_Trans_Log("SP_Trans_Log", WebSecurity.CurrentUserId, "A_Voucher", objCVarA_Voucher.ID, "I");
                        paramVoucherDetails_Save.pID = objCVarA_Voucher.ID;
                    }
                    else
                        pMessageReturned = checkException.Message;
                }
                else //update header
                {
                    VCODE = paramVoucherDetails_Save.pCode;
                    pUpdateClause = "Code=N'" + paramVoucherDetails_Save.pCode + "'" + "\n";
                    pUpdateClause += ",VoucherDate=N'" + paramVoucherDetails_Save.pVoucherDate.ToString("yyyyMMdd") + "'" + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pSafeID == 0 ? (",SafeID=NULL") : (",SafeID=" + paramVoucherDetails_Save.pSafeID)) + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pCurrencyID == 0 ? (",CurrencyID=NULL") : (",CurrencyID=" + paramVoucherDetails_Save.pCurrencyID)) + "\n";
                    pUpdateClause += ",ExchangeRate=" + paramVoucherDetails_Save.pExchangeRate + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pChargedPerson == "0" ? (",ChargedPerson=NULL") : (",ChargedPerson=N'" + paramVoucherDetails_Save.pChargedPerson + "'")) + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pNotes == "0" ? (",Notes=NULL") : (",Notes=N'" + paramVoucherDetails_Save.pNotes + "'")) + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pTaxID == 0 ? (",TaxID=NULL") : (",TaxID=" + paramVoucherDetails_Save.pTaxID)) + "\n";
                    pUpdateClause += ",TaxValue=" + paramVoucherDetails_Save.pTaxValue + "\n";
                    pUpdateClause += ",TaxSign=" + paramVoucherDetails_Save.pTaxSign + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pTaxID2 == 0 ? (",TaxID2=NULL") : (",TaxID2=" + paramVoucherDetails_Save.pTaxID2)) + "\n";
                    pUpdateClause += ",TaxValue2=" + paramVoucherDetails_Save.pTaxValue2 + "\n";
                    pUpdateClause += ",TaxSign2=" + paramVoucherDetails_Save.pTaxSign2 + "\n";
                    pUpdateClause += ",Total=" + paramVoucherDetails_Save.pTotal + "\n";
                    pUpdateClause += ",TotalAfterTax=" + paramVoucherDetails_Save.pTotalAfterTax + "\n";
                    pUpdateClause += ",IsAGInvoice=" + (paramVoucherDetails_Save.pIsAGInvoice ? "1" : "0") + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pAGInvType_ID == 0 ? (",AGInvType_ID=NULL") : (",AGInvType_ID=" + paramVoucherDetails_Save.pAGInvType_ID)) + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pInv_No == 0 ? (",Inv_No=NULL") : (",Inv_No=N'" + paramVoucherDetails_Save.pInv_No + "'")) + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pInvoiceID == 0 ? (",InvoiceID=NULL") : (",InvoiceID=" + paramVoucherDetails_Save.pInvoiceID)) + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pJVID1 == 0 ? (",JVID1=NULL") : (",JVID1=" + paramVoucherDetails_Save.pJVID1)) + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pJVID2 == 0 ? (",JVID2=NULL") : (",JVID2=" + paramVoucherDetails_Save.pJVID2)) + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pJVID3 == 0 ? (",JVID3=NULL") : (",JVID3=" + paramVoucherDetails_Save.pJVID3)) + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pJVID4 == 0 ? (",JVID4=NULL") : (",JVID4=" + paramVoucherDetails_Save.pJVID4)) + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pSalesManID == 0 ? (",SalesManID=NULL") : (",SalesManID=" + paramVoucherDetails_Save.pSalesManID)) + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pforwOperationID == 0 ? (",forwOperationID=NULL") : (",forwOperationID=" + paramVoucherDetails_Save.pforwOperationID)) + "\n";
                    pUpdateClause += ",IsCustomClearance=" + (paramVoucherDetails_Save.pIsCustomClearance ? "1" : "0") + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pTransType_ID == 0 ? (",TransType_ID=NULL") : (",TransType_ID=" + paramVoucherDetails_Save.pTransType_ID)) + "\n";
                    pUpdateClause += ",VoucherType=" + paramVoucherDetails_Save.pVoucherType + "\n";
                    pUpdateClause += ",IsCash=" + (paramVoucherDetails_Save.pIsCash ? "1" : "0") + "\n";
                    pUpdateClause += ",IsCheque=" + (paramVoucherDetails_Save.pIsCheque ? "1" : "0") + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pPrintDate.ToString("yyyy/MM/dd") == "1900/01/01" ? ",PrintDate=NULL" : (",PrintDate=N'" + paramVoucherDetails_Save.pPrintDate.ToString("yyyy/MM/dd") + "'")) + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pChequeNo == "0" ? (",ChequeNo=NULL") : (",ChequeNo=N'" + paramVoucherDetails_Save.pChequeNo + "'")) + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pChequeDate.ToString("yyyy/MM/dd") == "1900/01/01" ? ",ChequeDate=NULL" : (",ChequeDate=N'" + paramVoucherDetails_Save.pChequeDate.ToString("yyyy/MM/dd") + "'")) + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pBankID == 0 ? (",BankID=NULL") : (",BankID=" + paramVoucherDetails_Save.pBankID)) + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pDisbursementJob_ID == 0 ? (",DisbursementJob_ID=NULL") : (",DisbursementJob_ID=" + paramVoucherDetails_Save.pDisbursementJob_ID)) + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pSL_SalesManID == 0 ? (",SL_SalesManID=NULL") : (",SL_SalesManID=" + paramVoucherDetails_Save.pSL_SalesManID)) + "\n";


                    pUpdateClause += (paramVoucherDetails_Save.pOtherSideBankName == "0" ? (",OtherSideBankName=NULL") : (",OtherSideBankName=N'" + paramVoucherDetails_Save.pOtherSideBankName + "'")) + "\n";
                    pUpdateClause += (paramVoucherDetails_Save.pCollectionDate.ToString("yyyy/MM/dd") == "1900/01/01" ? ",CollectionDate=NULL" : (",CollectionDate=N'" + paramVoucherDetails_Save.pCollectionDate.ToString("yyyy/MM/dd") + "'")) + "\n";
                    pUpdateClause += ",CollectionExpense=" + paramVoucherDetails_Save.pCollectionExpense + "\n";
                    pUpdateClause += "WHERE ID=" + paramVoucherDetails_Save.pID + "\n";
                    checkException = objCA_Voucher.UpdateList(pUpdateClause);
                    if (checkException == null)
                        objCCustomizedDBCall.SP_Trans_Log("SP_Trans_Log", WebSecurity.CurrentUserId, "A_Voucher", paramVoucherDetails_Save.pID, "U");
                    else
                        pMessageReturned = checkException.Message;
                }
                #endregion Save Header
                #region Save Details
                if (pMessageReturned == "")
                {
                    objCVarA_VoucherDetails.ID = paramVoucherDetails_Save.pDetailsID;
                    objCVarA_VoucherDetails.VoucherID = paramVoucherDetails_Save.pID;
                    objCVarA_VoucherDetails.Value = paramVoucherDetails_Save.pValue;
                    objCVarA_VoucherDetails.Description = paramVoucherDetails_Save.pDescription;
                    objCVarA_VoucherDetails.AccountID = paramVoucherDetails_Save.pAccountID;
                    objCVarA_VoucherDetails.SubAccountID = paramVoucherDetails_Save.pSubAccountID;
                    objCVarA_VoucherDetails.CostCenterID = paramVoucherDetails_Save.pCostCenterID;
                    objCVarA_VoucherDetails.IsDocumented = paramVoucherDetails_Save.pIsDocumented;
                    objCVarA_VoucherDetails.InvoiceID = paramVoucherDetails_Save.pInvoiceID;
                    objCVarA_VoucherDetails.VoucherType = paramVoucherDetails_Save.pVoucherType;
                    objCVarA_VoucherDetails.Job_ID = 0;
                    objCVarA_VoucherDetails.OperationID = paramVoucherDetails_Save.pOperationID;
                    objCVarA_VoucherDetails.HouseID = paramVoucherDetails_Save.pHouseID;
                    objCVarA_VoucherDetails.BranchID = paramVoucherDetails_Save.pBranchID;


                    objCA_VoucherDetails.lstCVarA_VoucherDetails.Add(objCVarA_VoucherDetails);
                    checkException = objCA_VoucherDetails.SaveMethod(objCA_VoucherDetails.lstCVarA_VoucherDetails);
                    if (checkException == null)
                    {
                        objCCustomizedDBCall.SP_Trans_Log("SP_Trans_Log", WebSecurity.CurrentUserId, "A_VoucherDetails", objCVarA_VoucherDetails.ID, (paramVoucherDetails_Save.pDetailsID == 0 ? "I" : "U"));
                        Voucher_SetTotal(paramVoucherDetails_Save.pID);
                        //objCA_Voucher.UpdateList("Total=(SELECT SUM(Value) FROM A_VoucherDetails WHERE VoucherID=" + pID + ") "
                        //                         + ",TotalAfterTax=(SELECT SUM(Value) FROM A_VoucherDetails WHERE VoucherID=" + pID + ") + TaxValue*ISNULL(TaxSign,0) + TaxValue2*ISNULL(TaxSign2,0)"
                        //                         + " WHERE ID=" + pID);
                        objCvwA_VoucherDetails.GetList("WHERE VoucherID=" + paramVoucherDetails_Save.pID);
                    }
                    else
                        pMessageReturned = checkException.Message;
                }
                #endregion Save Details
            }
            #endregion Save

            return new object[] {
                pMessageReturned
                , paramVoucherDetails_Save.pID //pVoucherID = pData[1]
                , pMessageReturned == "" ? new JavaScriptSerializer().Serialize(objCvwA_VoucherDetails.lstCVarvwA_VoucherDetails) : null //pDetails = pData[2]
                , VCODE //pCode pData[3]
            };
        }

        [HttpGet, HttpPost]
        [AllowAnonymous]
        public object[] InsertItems([FromBody]string pItems)
        {
            // Insert Items to Invoices 
            var obj = new JavaScriptSerializer().Deserialize<List<object>>(pItems);
            var Obj_List_Items = obj[0];

            var _result = false;
            var serialize = new JavaScriptSerializer();
            var Details = serialize.Deserialize<List<CVarA_VoucherDetails>>(serialize.Serialize(Obj_List_Items));
            Exception checkException = new Exception();
            CA_VoucherDetails cA_VoucherDetails = new CA_VoucherDetails();
            if (Details != null && Details.Count > 0)
            {
                checkException = cA_VoucherDetails.SaveMethod(Details);
                var DetailsIDs = String.Join(",", Details.Select(x => x.ID).ToList());
                cA_VoucherDetails.DeleteList("where VoucherID = " + Details[0].VoucherID + " and ID Not IN(" + DetailsIDs + ")");
            }
            else
            {
                cA_VoucherDetails.DeleteList("where VoucherID = " + Details[0].InvoiceID);
            }
            //*********************

            var message = "";

            if (checkException != null)
            {
                message = "Please Insert Correct Data";
            }
            else
            {
                _result = true;
                message = "Done";

            }

            return new object[] {
                _result , message
            };

        }
        [HttpGet, HttpPost]
        public object[] Details_Delete(string pDeletedDetailsIDs, Int32 pVoucherID)
        {
            string pMessageReturned = "";
            Exception checkException = null;
            CA_VoucherDetails objCA_VoucherDetails = new CA_VoucherDetails();
            CA_Voucher objCA_Voucher = new CA_Voucher();
            CvwA_VoucherDetails objCvwA_VoucherDetails = new CvwA_VoucherDetails();
            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
            bool _result = true;
            objCA_Voucher.GetList("WHERE ID=" + pVoucherID);

            DateTime pVoucherDate = objCA_Voucher.lstCVarA_Voucher[0].VoucherDate;
            CA_Fiscal_Year objCA_Fiscal_Year = new CA_Fiscal_Year();
            CA_Fiscal_Year_Period objCA_Fiscal_Year_Period = new CA_Fiscal_Year_Period();
            CFreezeSystemTransactions objCFreezeSystemTransactions = new CFreezeSystemTransactions();
            checkException = objCA_Fiscal_Year_Period.GetList("WHERE '" + pVoucherDate.ToString("yyyy/MM/dd") + "' BETWEEN From_Date AND To_Date and Closed=1");
            checkException = objCFreezeSystemTransactions.GetList("WHERE TransType='All' AND ('" + pVoucherDate.ToString("yyyy/MM/dd") + "' BETWEEN FromDate AND ToDate)");
            #region Check FiscalYear is Confirmed and NOT Closed
            if (objCA_Fiscal_Year_Period.lstCVarA_Fiscal_Year_Period.Count > 0)
                pMessageReturned = "This fiscal year is closed.";
            #endregion Check FiscalYear is Confirmed and NOT Closed
            #region Check Period is Not Frozen
            if (objCFreezeSystemTransactions.lstCVarFreezeSystemTransactions.Count > 0)
                pMessageReturned = "The transactions for this date is frozen.";
            #endregion Check Period is Not Frozen

            if (pMessageReturned == "")
            {
                foreach (var currentID in pDeletedDetailsIDs.Split(','))
                {
                    objCA_VoucherDetails.lstDeletedCPKA_VoucherDetails.Add(new CPKA_VoucherDetails() { ID = Int64.Parse(currentID.Trim()) });
                    checkException = objCA_VoucherDetails.DeleteItem(objCA_VoucherDetails.lstDeletedCPKA_VoucherDetails);
                    if (checkException != null)
                        _result = false;
                    else
                        objCCustomizedDBCall.SP_Trans_Log("SP_Trans_Log", WebSecurity.CurrentUserId, "A_VoucherDetails", Int64.Parse(currentID), "D");
                }
                Voucher_SetTotal(pVoucherID);
                //objCA_Voucher.UpdateList("Total=(SELECT SUM(Value) FROM A_VoucherDetails WHERE VoucherID=" + pVoucherID + ") "
                //                      + ",TotalAfterTax=(SELECT SUM(Value) FROM A_VoucherDetails WHERE VoucherID=" + pVoucherID + ") + TaxValue*ISNULL(TaxSign,0) + TaxValue2*ISNULL(TaxSign2,0)"
                //                      + " WHERE ID=" + pVoucherID);
                objCCustomizedDBCall.SP_Trans_Log("SP_Trans_Log", WebSecurity.CurrentUserId, "A_Voucher", pVoucherID, "U");
                objCvwA_VoucherDetails.GetList("WHERE VoucherID=" + pVoucherID);
            }
            return new object[]
            {
                _result
                , new JavaScriptSerializer().Serialize(objCvwA_VoucherDetails.lstCVarvwA_VoucherDetails)
                , pMessageReturned
            };
        }
        #endregion Details

        #region ApprovingAndPosting

        [HttpGet, HttpPost]
        public object[] ChequeStatus_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned(bool pIsLoadArrayOfObjects, string pLanguage, Int32 pPageNumber, Int32 pPageSize, string pWhereClauseChequeStatus, string pOrderBy)
        {
            int _RowCount = 0;

            //LinkUserAndSafes
            string pSafesWhereClause = " WHERE 1=1 ";
            CSystemOptions objCSystemOptions = new CSystemOptions();
            objCSystemOptions.GetItem(55);
            if (objCSystemOptions.lstCVarSystemOptions[0].OptionValue)
            {

                pWhereClauseChequeStatus = " INNER JOIN VW_Sec_UserSafes US ON SafeID = US._SafeID " + pWhereClauseChequeStatus + " AND US._UserID=" + WebSecurity.CurrentUserId;
                pSafesWhereClause = " JOIN VW_Sec_UserSafes USF ON ID =  USF._SafeID " + pSafesWhereClause + "  AND USF._UserID=" + WebSecurity.CurrentUserId;
            }

            CTreasury objCSafes = new CTreasury();
            CBankAccount objCBank = new CBankAccount();
            if (pIsLoadArrayOfObjects)
            {
                objCSafes.GetListPaging(9999, 1, pSafesWhereClause, "Name", out _RowCount);
                objCBank.GetListPaging(9999, 1, "WHERE 1=1", "Name", out _RowCount);
            }
            CvwA_ChequeStatus objCvwA_ChequeStatus = new CvwA_ChequeStatus();
            objCvwA_ChequeStatus.GetListPaging(pPageSize, pPageNumber, pWhereClauseChequeStatus, pOrderBy, out _RowCount);

            return new object[] {
                new JavaScriptSerializer().Serialize(objCvwA_ChequeStatus.lstCVarvwA_ChequeStatus)
                , _RowCount
                , pIsLoadArrayOfObjects ? new JavaScriptSerializer().Serialize(objCSafes.lstCVarTreasury) : null //pSafe = pData[2]
                , pIsLoadArrayOfObjects ? new JavaScriptSerializer().Serialize(objCBank.lstCVarBankAccount) : null //pBank = pData[3]
            };
        }

        [HttpGet, HttpPost]
        public object[] SetPostField(string pSelectedIDs, DateTime pGivenDate, string pValue, bool pUseGivenDate)
        {
            string pMessageReturned = "";
            int constVoucherCashIn = 10;
            int constVoucherCashOut = 20;
            int constVoucherChequeIn = 30;
            int constVoucherChequeOut = 40;
            bool _result = true;
            Exception checkException = null;
            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
            CA_Voucher objCA_Voucher = new CA_Voucher();
            CA_Fiscal_Year_Period objCA_Fiscal_Year_Period = new CA_Fiscal_Year_Period();
            CFreezeSystemTransactions objCFreezeSystemTransactions = new CFreezeSystemTransactions();
            checkException = objCA_Voucher.GetList("WHERE ID IN (" + pSelectedIDs + ")");
            int NumberOfSelectedRows = pSelectedIDs.Split(',').Length;
            var ArrSelectedIDs = pSelectedIDs.Split(',');
            for (int i = 0; i < NumberOfSelectedRows; i++)
            {
                DateTime pVoucherDate = objCA_Voucher.lstCVarA_Voucher[i].VoucherDate;
                checkException = objCA_Fiscal_Year_Period.GetList("WHERE '" + pVoucherDate.ToString("yyyy/MM/dd") + "' BETWEEN From_Date AND To_Date and Closed=1");
                checkException = objCFreezeSystemTransactions.GetList("WHERE TransType='All' AND ('" + pVoucherDate.ToString("yyyy/MM/dd") + "' BETWEEN FromDate AND ToDate)");
                if (objCFreezeSystemTransactions.lstCVarFreezeSystemTransactions.Count == 0
                    && objCA_Fiscal_Year_Period.lstCVarA_Fiscal_Year_Period.Count == 0) //not closed and date not frozen
                {
                    if (pValue == "1") //Post
                    {
                        if (objCA_Voucher.lstCVarA_Voucher[i].VoucherType == constVoucherCashIn) //CashIn posting
                            checkException = objCCustomizedDBCall.A_CashInVouchers_Posting("A_CashInVouchers_Posting", "," + objCA_Voucher.lstCVarA_Voucher[i].ID.ToString() + ",", (pUseGivenDate ? pGivenDate : objCA_Voucher.lstCVarA_Voucher[i].VoucherDate), WebSecurity.CurrentUserId);
                        else if (objCA_Voucher.lstCVarA_Voucher[i].VoucherType == constVoucherCashOut) //CashOut posting
                            checkException = objCCustomizedDBCall.A_CashOutVouchers_Posting("A_CashOutVouchers_Posting", "," + objCA_Voucher.lstCVarA_Voucher[i].ID.ToString() + ",", (pUseGivenDate ? pGivenDate : objCA_Voucher.lstCVarA_Voucher[i].VoucherDate), WebSecurity.CurrentUserId);
                        if (objCA_Voucher.lstCVarA_Voucher[i].VoucherType == constVoucherChequeIn) //ChequeIn posting
                            checkException = objCCustomizedDBCall.A_ChequeInVouchers_Posting("A_ChequeInVouchers_Posting", "," + objCA_Voucher.lstCVarA_Voucher[i].ID.ToString() + ",", (pUseGivenDate ? pGivenDate : objCA_Voucher.lstCVarA_Voucher[i].VoucherDate), WebSecurity.CurrentUserId);
                        else if (objCA_Voucher.lstCVarA_Voucher[i].VoucherType == constVoucherChequeOut) //ChequeOut posting
                            checkException = objCCustomizedDBCall.A_ChequeOutVouchers_Posting("A_ChequeOutVouchers_Posting", "," + objCA_Voucher.lstCVarA_Voucher[i].ID.ToString() + ",", (pUseGivenDate ? pGivenDate : objCA_Voucher.lstCVarA_Voucher[i].VoucherDate), WebSecurity.CurrentUserId);
                        CA_AccreditationMovement objCA_AccreditationMovement = new CA_AccreditationMovement();
                        string PaymentRequestID = objCCustomizedDBCall.CallStringFunction(" SELECT TOP 1 id FROM A_PaymentRequest AS apr WHERE apr.VoucherID =" + objCA_Voucher.lstCVarA_Voucher[i].ID.ToString());
                        if (PaymentRequestID != "") {
                            checkException = objCA_AccreditationMovement.UpdateList("TreasuryReferences=" + WebSecurity.CurrentUserId + " WHERE PaymentRequestID=" + PaymentRequestID);
                        }else
                        {
                            string user_id = objCCustomizedDBCall.CallStringFunction("   SELECT TOP 1 user_id FROM Trans_Log WHERE Table_Name ='A_Voucher' AND Row_ID =" + objCA_Voucher.lstCVarA_Voucher[i].ID.ToString());

                            CVarA_AccreditationMovement objVarA_AccreditationMovement = new CVarA_AccreditationMovement();
                            objVarA_AccreditationMovement.mPaymentRequestID = 0;
                            objVarA_AccreditationMovement.mRequestCreator = 0;
                            objVarA_AccreditationMovement.mTechnicalDirector = 0;
                            objVarA_AccreditationMovement.mCovenantAccountant = 0;
                            objVarA_AccreditationMovement.mSecretaryTreasury = Convert.ToInt32(user_id);
                            objVarA_AccreditationMovement.mTreasuryReferences = WebSecurity.CurrentUserId;
                            objVarA_AccreditationMovement.mVoucherID = objCA_Voucher.lstCVarA_Voucher[i].ID;
                            objVarA_AccreditationMovement.mIsChanges = true;
                            objCA_AccreditationMovement.lstCVarA_AccreditationMovement.Add(objVarA_AccreditationMovement);

                            checkException = objCA_AccreditationMovement.SaveMethod(objCA_AccreditationMovement.lstCVarA_AccreditationMovement);
                        }
                    }
                    else if (pValue == "0") //Unpost
                    {
                        if (objCA_Voucher.lstCVarA_Voucher[i].VoucherType == constVoucherCashIn || objCA_Voucher.lstCVarA_Voucher[i].VoucherType == constVoucherCashOut) //Cash
                            checkException = objCCustomizedDBCall.A_CashVouchers_UnPosted_ByID("A_CashVouchers_UnPosted_ByID", "," + ArrSelectedIDs[i] + "," ,  WebSecurity.CurrentUserId);
                        else //Cheque
                            checkException = objCCustomizedDBCall.A_ChequeVouchers_UnPosted_ByID("A_ChequeVouchers_UnPosted_ByID", "," + ArrSelectedIDs[i] + ",", WebSecurity.CurrentUserId);
                    }
                    if (checkException != null)
                        pMessageReturned = checkException.Message;
                    if (pMessageReturned != "")
                        _result = false;

                }
                else
                {
                    pMessageReturned = checkException.Message;
                    _result = false;
                }

            }

            return new object[] {
                _result ,pMessageReturned
            };
        }
        [HttpGet, HttpPost]
        public object[] SetPostFieldTax(string pSelectedIDs, DateTime pGivenDate, string pValue, bool pUseGivenDate)
        {
            string pMessageReturned = "";
            int constVoucherCashIn = 10;
            int constVoucherCashOut = 20;
            int constVoucherChequeIn = 30;
            int constVoucherChequeOut = 40;
            bool _result = true;
            Exception checkException = null;
            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
            CA_Voucher objCA_Voucher = new CA_Voucher();
            CA_Fiscal_Year_Period objCA_Fiscal_Year_Period = new CA_Fiscal_Year_Period();
            CFreezeSystemTransactions objCFreezeSystemTransactions = new CFreezeSystemTransactions();
            checkException = objCA_Voucher.GetList("WHERE ID IN (" + pSelectedIDs + ")");
            int NumberOfSelectedRows = pSelectedIDs.Split(',').Length;
            var ArrSelectedIDs = pSelectedIDs.Split(',');
            CTaxLink objCTaxLink = new CTaxLink();
            CTaxLink objCTaxLinkVoucher = new CTaxLink();

            for (int i = 0; i < NumberOfSelectedRows; i++)
            {
                CA_VoucherTax objCA_VoucherTax = new CA_VoucherTax();
                CVarA_VoucherTax objCVarA_VoucherTax = new CVarA_VoucherTax();
                objCTaxLink.GetList("WHERE Notes='A_voucher' and jvid is null and OriginID=" + ArrSelectedIDs[i]);
                objCTaxLinkVoucher.GetList("WHERE Notes='A_voucher' and jvid is not null and OriginID=" + ArrSelectedIDs[i]);

                //  DateTime pVoucherDate = objCA_Voucher.lstCVarA_Voucher[i].VoucherDate;

                checkException = objCA_Voucher.GetList("WHERE ID=" + ArrSelectedIDs[i]);

                if (pValue == "1" && objCTaxLink.lstCVarTaxLink.Count == 0)
                {
                    if (objCA_Voucher.lstCVarA_Voucher.Count > 0)
                    {
                        #region Save Header
                        CTreasury objCGetCreationInformation = new CTreasury();
                        CTreasuryTAX objCTreasuryTAX = new CTreasuryTAX();
                        objCGetCreationInformation.GetItem(objCA_Voucher.lstCVarA_Voucher[0].SafeID);
                        if (objCGetCreationInformation.lstCVarTreasury.Count > 0)
                        {
                            objCTreasuryTAX.GetList("WHERE Name=N'" + objCGetCreationInformation.lstCVarTreasury[0].Name + "'");

                        }

                        CBankAccount objCGetCreationInformationBank = new CBankAccount();
                        objCGetCreationInformationBank.GetItem(objCA_Voucher.lstCVarA_Voucher[0].BankID);
                        if (objCGetCreationInformationBank.lstCVarBankAccount.Count > 0)
                        {
                            objCGetCreationInformationBank.GetList("WHERE Name=N'" + objCGetCreationInformationBank.lstCVarBankAccount[0].Name + "'");

                        }

                        objCVarA_VoucherTax.Code = objCA_Voucher.lstCVarA_Voucher[0].Code;
                        objCVarA_VoucherTax.VoucherDate = objCA_Voucher.lstCVarA_Voucher[0].VoucherDate;
                        objCVarA_VoucherTax.SafeID = objCTreasuryTAX.lstCVarTreasuryTAX.Count > 0 ? objCTreasuryTAX.lstCVarTreasuryTAX[0].ID : 0;
                        objCVarA_VoucherTax.CurrencyID = objCA_Voucher.lstCVarA_Voucher[0].CurrencyID;
                        objCVarA_VoucherTax.ExchangeRate = objCA_Voucher.lstCVarA_Voucher[0].ExchangeRate;
                        objCVarA_VoucherTax.ChargedPerson = objCA_Voucher.lstCVarA_Voucher[0].ChargedPerson;
                        objCVarA_VoucherTax.Notes = objCA_Voucher.lstCVarA_Voucher[0].Notes;
                        objCVarA_VoucherTax.TaxID = objCA_Voucher.lstCVarA_Voucher[0].TaxID;
                        objCVarA_VoucherTax.TaxValue = objCA_Voucher.lstCVarA_Voucher[0].TaxValue;
                        objCVarA_VoucherTax.TaxSign = objCA_Voucher.lstCVarA_Voucher[0].TaxSign;
                        objCVarA_VoucherTax.TaxID2 = objCA_Voucher.lstCVarA_Voucher[0].TaxID2;
                        objCVarA_VoucherTax.TaxValue2 = objCA_Voucher.lstCVarA_Voucher[0].TaxValue2;
                        objCVarA_VoucherTax.TaxSign2 = objCA_Voucher.lstCVarA_Voucher[0].TaxSign2;
                        objCVarA_VoucherTax.Total = objCA_Voucher.lstCVarA_Voucher[0].Total;
                        objCVarA_VoucherTax.TotalAfterTax = objCA_Voucher.lstCVarA_Voucher[0].TotalAfterTax;
                        //objCVarA_Voucher.Approved = false;
                        //objCVarA_Voucher.Posted = false;
                        objCVarA_VoucherTax.IsAGInvoice = objCA_Voucher.lstCVarA_Voucher[0].IsAGInvoice;
                        objCVarA_VoucherTax.AGInvType_ID = objCA_Voucher.lstCVarA_Voucher[0].AGInvType_ID;
                        objCVarA_VoucherTax.Inv_No = objCA_Voucher.lstCVarA_Voucher[0].Inv_No;
                        objCVarA_VoucherTax.InvoiceID = objCA_Voucher.lstCVarA_Voucher[0].InvoiceID;
                        ////Set from posting screen
                        //objCVarA_Voucher.JVID1 = pJVID1;
                        //objCVarA_Voucher.JVID2 = pJVID2;
                        //objCVarA_Voucher.JVID3 = pJVID3;
                        //objCVarA_Voucher.JVID4 = pJVID4;
                        objCVarA_VoucherTax.SalesManID = objCA_Voucher.lstCVarA_Voucher[0].SalesManID;
                        objCVarA_VoucherTax.forwOperationID = objCA_Voucher.lstCVarA_Voucher[0].forwOperationID;
                        objCVarA_VoucherTax.IsCustomClearance = objCA_Voucher.lstCVarA_Voucher[0].IsCustomClearance;
                        objCVarA_VoucherTax.TransType_ID = objCA_Voucher.lstCVarA_Voucher[0].TransType_ID;
                        objCVarA_VoucherTax.VoucherType = objCA_Voucher.lstCVarA_Voucher[0].VoucherType;
                        objCVarA_VoucherTax.IsCash = objCA_Voucher.lstCVarA_Voucher[0].IsCash;
                        objCVarA_VoucherTax.IsCheque = objCA_Voucher.lstCVarA_Voucher[0].IsCheque;
                        objCVarA_VoucherTax.PrintDate = objCA_Voucher.lstCVarA_Voucher[0].PrintDate;
                        objCVarA_VoucherTax.ChequeNo = objCA_Voucher.lstCVarA_Voucher[0].ChequeNo;
                        objCVarA_VoucherTax.ChequeDate = objCA_Voucher.lstCVarA_Voucher[0].ChequeDate;
                        objCVarA_VoucherTax.BankID = objCGetCreationInformationBank.lstCVarBankAccount.Count > 0 ? objCGetCreationInformationBank.lstCVarBankAccount[0].ID : 0;
                        objCVarA_VoucherTax.DisbursementJob_ID = objCA_Voucher.lstCVarA_Voucher[0].DisbursementJob_ID;
                        objCVarA_VoucherTax.SL_SalesManID = objCA_Voucher.lstCVarA_Voucher[0].SL_SalesManID;
                        objCVarA_VoucherTax.Bill_ID = 0;
                        objCVarA_VoucherTax.IBAN = "0";
                        objCVarA_VoucherTax.ReferenceNo = "0";
                        objCVarA_VoucherTax.DepositeDate = Convert.ToDateTime("1/1/1900");
                        objCVarA_VoucherTax.TransferDate = Convert.ToDateTime("1/1/1900");

                        objCVarA_VoucherTax.PaidAmount = 0;
                        objCVarA_VoucherTax.RemainAmount = objCA_Voucher.lstCVarA_Voucher[0].RemainAmount;
                        objCVarA_VoucherTax.isTransfer = false;


                        objCVarA_VoucherTax.OtherSideBankName = objCA_Voucher.lstCVarA_Voucher[0].OtherSideBankName;
                        objCVarA_VoucherTax.CollectionDate = objCA_Voucher.lstCVarA_Voucher[0].CollectionDate;
                        objCVarA_VoucherTax.CollectionExpense = objCA_Voucher.lstCVarA_Voucher[0].CollectionExpense;
                        objCA_VoucherTax.lstCVarA_Voucher.Add(objCVarA_VoucherTax);
                        checkException = objCA_VoucherTax.SaveMethod(objCA_VoucherTax.lstCVarA_Voucher);



                        if (checkException == null && checkException == null)
                        {
                            objCCustomizedDBCall.SP_Trans_Log("SP_Trans_Log", WebSecurity.CurrentUserId, "A_VoucherTax", objCVarA_VoucherTax.ID, "I");

                            #region Details
                            CA_VoucherDetails objCA_VoucherDetails = new CA_VoucherDetails();
                            CVarA_VoucherDetails objCVarA_VoucherDetails = new CVarA_VoucherDetails();

                            CA_VoucherDetailsTax objCA_VoucherDetailsTax = new CA_VoucherDetailsTax();
                            CVarA_VoucherDetailsTax objCVarA_VoucherDetailsTax = new CVarA_VoucherDetailsTax();

                            checkException = objCA_VoucherDetails.GetList("WHERE VoucherID=" + ArrSelectedIDs[i]);
                            if (objCA_VoucherDetails.lstCVarA_VoucherDetails.Count > 0)
                            {
                                for (int k = 0; k < objCA_VoucherDetails.lstCVarA_VoucherDetails.Count; k++)
                                {
                                    int _RowCount = 0;
                                    int AccountID = 0;
                                    int SubAccountID = 0;
                                    //Account
                                    CA_Accounts objCACA_AccountsAccountID = new CA_Accounts(); //get the parent details
                                    checkException = objCACA_AccountsAccountID.GetListPaging(9999, 1, "WHERE ID = " + objCA_VoucherDetails.lstCVarA_VoucherDetails[k].AccountID, "ID", out _RowCount);
                                    CA_AccountsTAX objCAccountsTAX = new CA_AccountsTAX(); //get the parent details
                                    CA_SubAccountsTAX objCA_SubAccountsTAXO = new CA_SubAccountsTAX(); //get the parent details
                                    if (objCACA_AccountsAccountID.lstCVarA_Accounts.Count > 0)
                                    {
                                        checkException = objCAccountsTAX.GetListPaging(9999, 1, "WHERE Account_Name = N'" + objCACA_AccountsAccountID.lstCVarA_Accounts[0].Account_Name + "'", "ID", out _RowCount);
                                        if (objCAccountsTAX.lstCVarA_Accounts.Count > 0)
                                        {
                                            AccountID = objCAccountsTAX.lstCVarA_Accounts[0].ID;
                                        }

                                    }
                                    //SubAccountID_Return
                                    CA_SubAccounts objCA_SubAccountsSubAccountID = new CA_SubAccounts(); //get the parent details
                                    checkException = objCA_SubAccountsSubAccountID.GetListPaging(9999, 1, "WHERE ID = " + objCA_VoucherDetails.lstCVarA_VoucherDetails[k].SubAccountID, "ID", out _RowCount);
                                    if (objCA_SubAccountsSubAccountID.lstCVarA_SubAccounts.Count > 0)
                                    {
                                        checkException = objCA_SubAccountsTAXO.GetListPaging(9999, 1, "WHERE SubAccount_Name = N'" + objCA_SubAccountsSubAccountID.lstCVarA_SubAccounts[0].SubAccount_Name + "'", "ID", out _RowCount);
                                        if (objCA_SubAccountsTAXO.lstCVarA_SubAccounts.Count > 0)
                                        {
                                            SubAccountID = objCA_SubAccountsTAXO.lstCVarA_SubAccounts[0].ID;

                                        }
                                    }

                                    objCVarA_VoucherDetailsTax.ID = 0;
                                    objCVarA_VoucherDetailsTax.VoucherID = objCVarA_VoucherTax.ID;
                                    objCVarA_VoucherDetailsTax.Value = objCA_VoucherDetails.lstCVarA_VoucherDetails[k].Value;
                                    objCVarA_VoucherDetailsTax.Description = objCA_VoucherDetails.lstCVarA_VoucherDetails[k].Description;
                                    objCVarA_VoucherDetailsTax.AccountID = AccountID;
                                    objCVarA_VoucherDetailsTax.SubAccountID = SubAccountID;
                                    objCVarA_VoucherDetailsTax.CostCenterID = 0;
                                    objCVarA_VoucherDetailsTax.IsDocumented = objCA_VoucherDetails.lstCVarA_VoucherDetails[k].IsDocumented;
                                    objCVarA_VoucherDetailsTax.InvoiceID = 0;
                                    objCVarA_VoucherDetailsTax.VoucherType = objCA_VoucherDetails.lstCVarA_VoucherDetails[k].VoucherType;
                                    objCVarA_VoucherDetailsTax.Job_ID = 0;
                                    objCVarA_VoucherDetailsTax.OperationID = 0;
                                    objCVarA_VoucherDetailsTax.HouseID = 0;
                                    objCVarA_VoucherDetailsTax.BranchID = 0;


                                    objCA_VoucherDetailsTax.lstCVarA_VoucherDetails.Add(objCVarA_VoucherDetailsTax);
                                    checkException = objCA_VoucherDetailsTax.SaveMethod(objCA_VoucherDetailsTax.lstCVarA_VoucherDetails);
                                }

                                if (checkException == null)
                                {
                                    objCCustomizedDBCall.SP_Trans_Log("SP_Trans_Log", WebSecurity.CurrentUserId, "A_VoucherDetailsTax", objCVarA_VoucherDetailsTax.ID, "I");
                                    //link
                                    objCCustomizedDBCall.ExecuteQuery_DataTable("insertTaxLink " + ArrSelectedIDs[i] + "," + objCVarA_VoucherTax.ID + "," + "A_Voucher");

                                    #region Post
                                    if (pValue == "1") //Post
                                    {
                                        if (objCA_Voucher.lstCVarA_Voucher[0].VoucherType == constVoucherCashIn) //CashIn posting
                                            checkException = objCCustomizedDBCall.A_CashInVouchers_Posting("A_CashInVouchers_PostingTax", "," + objCVarA_VoucherTax.ID.ToString() + ",", (pUseGivenDate ? pGivenDate : objCA_Voucher.lstCVarA_Voucher[0].VoucherDate), WebSecurity.CurrentUserId);
                                        else if (objCA_Voucher.lstCVarA_Voucher[0].VoucherType == constVoucherCashOut) //CashOut posting
                                            checkException = objCCustomizedDBCall.A_CashOutVouchers_Posting("A_CashOutVouchers_PostingTax", "," + objCVarA_VoucherTax.ID.ToString() + ",", (pUseGivenDate ? pGivenDate : objCA_Voucher.lstCVarA_Voucher[0].VoucherDate), WebSecurity.CurrentUserId);
                                        if (objCA_Voucher.lstCVarA_Voucher[0].VoucherType == constVoucherChequeIn) //ChequeIn posting
                                            checkException = objCCustomizedDBCall.A_ChequeInVouchers_Posting("A_ChequeInVouchers_PostingTax", "," + objCVarA_VoucherTax.ID.ToString() + ",", (pUseGivenDate ? pGivenDate : objCA_Voucher.lstCVarA_Voucher[0].VoucherDate), WebSecurity.CurrentUserId);
                                        else if (objCA_Voucher.lstCVarA_Voucher[0].VoucherType == constVoucherChequeOut) //ChequeOut posting
                                            checkException = objCCustomizedDBCall.A_ChequeOutVouchers_Posting("A_ChequeOutVouchers_PostingTax", "," + objCVarA_VoucherTax.ID.ToString() + ",", (pUseGivenDate ? pGivenDate : objCA_Voucher.lstCVarA_Voucher[0].VoucherDate), WebSecurity.CurrentUserId);

                                    }

                                    #endregion
                                }

                            }


                            #endregion
                        }
                        else
                            pMessageReturned = checkException.Message;


                        #endregion Save Header
                    }
                }

                else if (pValue == "1" && objCTaxLink.lstCVarTaxLink.Count > 0)
                {
                    if (objCA_Voucher.lstCVarA_Voucher[0].VoucherType == constVoucherCashIn) //CashIn posting
                        checkException = objCCustomizedDBCall.A_CashInVouchers_Posting("A_CashInVouchers_PostingTax", "," + (objCTaxLink.lstCVarTaxLink.Count > 0 ? objCTaxLink.lstCVarTaxLink[0].TaxID : 0) + ",", (pUseGivenDate ? pGivenDate : objCA_Voucher.lstCVarA_Voucher[0].VoucherDate), WebSecurity.CurrentUserId);
                    else if (objCA_Voucher.lstCVarA_Voucher[0].VoucherType == constVoucherCashOut) //CashOut posting
                        checkException = objCCustomizedDBCall.A_CashOutVouchers_Posting("A_CashOutVouchers_PostingTax", "," + (objCTaxLink.lstCVarTaxLink.Count > 0 ? objCTaxLink.lstCVarTaxLink[0].TaxID : 0) + ",", (pUseGivenDate ? pGivenDate : objCA_Voucher.lstCVarA_Voucher[0].VoucherDate), WebSecurity.CurrentUserId);
                    if (objCA_Voucher.lstCVarA_Voucher[0].VoucherType == constVoucherChequeIn) //ChequeIn posting
                        checkException = objCCustomizedDBCall.A_ChequeInVouchers_Posting("A_ChequeInVouchers_PostingTax", "," + (objCTaxLink.lstCVarTaxLink.Count > 0 ? objCTaxLink.lstCVarTaxLink[0].TaxID : 0) + ",", (pUseGivenDate ? pGivenDate : objCA_Voucher.lstCVarA_Voucher[0].VoucherDate), WebSecurity.CurrentUserId);
                    else if (objCA_Voucher.lstCVarA_Voucher[0].VoucherType == constVoucherChequeOut) //ChequeOut posting
                        checkException = objCCustomizedDBCall.A_ChequeOutVouchers_Posting("A_ChequeOutVouchers_PostingTax", "," + (objCTaxLink.lstCVarTaxLink.Count > 0 ? objCTaxLink.lstCVarTaxLink[0].TaxID : 0) + ",", (pUseGivenDate ? pGivenDate : objCA_Voucher.lstCVarA_Voucher[0].VoucherDate), WebSecurity.CurrentUserId);

                }
                else if (pValue == "0" && objCTaxLinkVoucher.lstCVarTaxLink.Count > 0) //Unpost
                {
                    if (objCA_Voucher.lstCVarA_Voucher[0].VoucherType == constVoucherCashIn || objCA_Voucher.lstCVarA_Voucher[0].VoucherType == constVoucherCashOut) //Cash
                        checkException = objCCustomizedDBCall.A_CashVouchers_UnPosted_ByID("A_CashVouchers_UnPosted_ByIDTax", "," + (objCTaxLinkVoucher.lstCVarTaxLink.Count > 0 ? objCTaxLinkVoucher.lstCVarTaxLink[0].TaxID : 0) + ",", WebSecurity.CurrentUserId);
                    else //Cheque
                        checkException = objCCustomizedDBCall.A_ChequeVouchers_UnPosted_ByID("A_ChequeVouchers_UnPosted_ByIDTax", "," + (objCTaxLinkVoucher.lstCVarTaxLink.Count > 0 ? objCTaxLinkVoucher.lstCVarTaxLink[0].TaxID : 0) + ",", WebSecurity.CurrentUserId);
                }



            }
            return new object[] {
                _result ,pMessageReturned
            };
        }

        [HttpGet, HttpPost]
        public object[] PostingReceivablePayableNotes_Post(string pSelectedReceivablePayableNotesIDs, string pVoucherIDList, string pSafeIDList, string pJVDateList)
        {
            //int constVoucherCashIn = 10;
            //int constVoucherCashOut = 20;
            int constVoucherChequeIn = 30;
            int constVoucherChequeOut = 40;
            bool _result = true;
            Exception checkException = null;
            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
            CA_Voucher objCA_Voucher = new CA_Voucher();
            CA_Fiscal_Year_Period objCA_Fiscal_Year_Period = new CA_Fiscal_Year_Period();
            CFreezeSystemTransactions objCFreezeSystemTransactions = new CFreezeSystemTransactions();
            CA_ChequeStatus objCA_ChequeStatus = new CA_ChequeStatus();
            //checkException = objCA_Voucher.GetList("WHERE ID IN (" + pVoucherIDList + ")");
            int NumberOfSelectedRows = pSelectedReceivablePayableNotesIDs.Split(',').Length;
            var ArrChequeStatusIDList = pSelectedReceivablePayableNotesIDs.Split(',');
            var ArrVoucherIDsList = pVoucherIDList.Split(',');
            var ArrJVDateList = pJVDateList.Split(',');
            var ArrSafeIDList = pSafeIDList.Split(',');
            for (int i = 0; i < NumberOfSelectedRows; i++)
            {
                checkException = objCA_Voucher.GetList("WHERE ID = (" + ArrVoucherIDsList[i] + ")"); //i put it here to handle order of retrieving
                DateTime pVoucherDate = objCA_Voucher.lstCVarA_Voucher[0].VoucherDate;
                checkException = objCA_Fiscal_Year_Period.GetList("WHERE '" + pVoucherDate.ToString("yyyy/MM/dd") + "' BETWEEN From_Date AND To_Date and Closed=1");
                checkException = objCFreezeSystemTransactions.GetList("WHERE TransType='All' AND ('" + pVoucherDate.ToString("yyyy/MM/dd") + "' BETWEEN FromDate AND ToDate)");
                if (objCFreezeSystemTransactions.lstCVarFreezeSystemTransactions.Count == 0
                    /*&& objCA_Fiscal_Year_Period.lstCVarA_Fiscal_Year_Period.Count == 0*/) //not closed and date not frozen
                {
                    if (objCA_Voucher.lstCVarA_Voucher[0].VoucherType == constVoucherChequeIn) //Receivable Notes posting
                    {
                        objCCustomizedDBCall.A_ChequeStatus_PostingReceivablePayableNotes("A_ChequeStatus_PostReceivable", Int64.Parse(ArrVoucherIDsList[i]), DateTime.ParseExact(ArrJVDateList[i], "dd/MM/yyyy", CultureInfo.InvariantCulture), WebSecurity.CurrentUserId);
                        if (ArrSafeIDList[i] != "0")
                            objCA_ChequeStatus.UpdateList("ToSafe=0, "/*Set to 0 in this stage in the original app.!!*/ + "SafeID=" + ArrSafeIDList[i] + " WHERE ID=" + ArrChequeStatusIDList[i]);
                    }
                    else if (objCA_Voucher.lstCVarA_Voucher[0].VoucherType == constVoucherChequeOut) //Payable Notes posting
                        objCCustomizedDBCall.A_ChequeStatus_PostingReceivablePayableNotes("A_ChequeStatus_PostPayable", Int64.Parse(ArrVoucherIDsList[i]), DateTime.ParseExact(ArrJVDateList[i], "dd/MM/yyyy", CultureInfo.InvariantCulture), WebSecurity.CurrentUserId);
                }
                else
                    _result = false;
            }
            return new object[] {
                _result
            };
        }

        //pOption 1:Approve 2:Returned
        [HttpGet, HttpPost]
        public object[] PostingUnderCollectNotes_ApproveOrReturn(Int32 pOption
            , string pSelectedReceivablePayableNotesIDs, string pVoucherIDList, string pSafeIDList, string pJVDateList
            , string pBankIDList, string pCollectionExpensesList, string pSafeAccountIDList, string pBankAccountIDList
            , string pAmountList, string pBankNotesReceivableUnderCollectionList, string pBankNotesPayableUnderCollectionList
            , string pBankCollectionExpensesIDsList, string pBankInJournalTypeIDList, string pBankOutJournalTypeIDList
            , string pBankNotesReceivableList, string pBankNotesPayableList)
        {
            //int constVoucherCashIn = 10;
            //int constVoucherCashOut = 20;
            int constVoucherChequeIn = 30;
            int constVoucherChequeOut = 40;
            bool _result = true;
            Exception checkException = null;
            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
            CA_Voucher objCA_Voucher = new CA_Voucher();
            CvwA_Voucher objCvwA_Voucher = new CvwA_Voucher();
            CA_Fiscal_Year_Period objCA_Fiscal_Year_Period = new CA_Fiscal_Year_Period();
            CFreezeSystemTransactions objCFreezeSystemTransactions = new CFreezeSystemTransactions();
            CA_ChequeStatus objCA_ChequeStatus = new CA_ChequeStatus();
            CA_JV objCA_JV = new CA_JV();
            CvwA_JV objCOriginalvwA_JV = new CvwA_JV(); //The first JV created
            CA_JVDetails objCA_JVDetails = new CA_JVDetails();
            CJVDefaults objCJVDefaults = new CJVDefaults();
            string BranchID = objCCustomizedDBCall.CallStringFunction("SELECT u.BranchID FROM  Users AS u WHERE u.ID=  " + WebSecurity.CurrentUserId);
            checkException = objCJVDefaults.GetList("WHERE TransTypeID IN (16,17,19)");
            int pJournalID16 = objCJVDefaults.lstCVarJVDefaults.Where(w => w.TransTypeID == 16).Last().JournalTypeID;
            int pJournalID17 = objCJVDefaults.lstCVarJVDefaults.Where(w => w.TransTypeID == 17).Last().JournalTypeID;
            int pJournalID19 = objCJVDefaults.lstCVarJVDefaults.Where(w => w.TransTypeID == 19).Last().JournalTypeID;
            int pJVTypeID16 = objCJVDefaults.lstCVarJVDefaults.Where(w => w.TransTypeID == 16).Last().JVTypeID;
            int pJVTypeID17 = objCJVDefaults.lstCVarJVDefaults.Where(w => w.TransTypeID == 17).Last().JVTypeID;
            int pJVTypeID19 = objCJVDefaults.lstCVarJVDefaults.Where(w => w.TransTypeID == 19).Last().JVTypeID;
            //checkException = objCA_Voucher.GetList("WHERE ID IN (" + pVoucherIDList + ")");
            int NumberOfSelectedRows = pSelectedReceivablePayableNotesIDs.Split(',').Length;
            var ArrChequeStatusIDList = pSelectedReceivablePayableNotesIDs.Split(',');
            var ArrVoucherIDsList = pVoucherIDList.Split(',');
            var ArrJVDateList = pJVDateList.Split(',');
            var ArrSafeIDList = pSafeIDList.Split(',');
            var ArrBankIDList = pBankIDList.Split(',');
            var ArrSafeAccountIDList = pSafeAccountIDList.Split(',');
            var ArrBankAccountIDList = pBankAccountIDList.Split(',');
            var ArrCollectionExpensesList = pCollectionExpensesList.Split(',');
            var ArrAmountList = pAmountList.Split(',');
            var ArrBankNotesReceivableUnderCollectionList = pBankNotesReceivableUnderCollectionList.Split(',');
            var ArrBankNotesPayableUnderCollectionList = pBankNotesPayableUnderCollectionList.Split(',');
            var ArrBankCollectionExpensesIDsList = pBankCollectionExpensesIDsList.Split(',');
            var ArrBankInJournalTypeIDList = pBankInJournalTypeIDList.Split(',');
            var ArrBankOutJournalTypeIDList = pBankOutJournalTypeIDList.Split(',');
            var ArrBankNotesReceivableList = pBankNotesReceivableList.Split(',');
            var ArrBankNotesPayable = pBankNotesPayableList.Split(',');
            for (int i = 0; i < NumberOfSelectedRows; i++)
            {
                CVarA_JV objCVarA_JV = new CVarA_JV();
                objCVarA_JV.ID = 0;
                objCA_JV.lstCVarA_JV.RemoveAll(r => 1 == 1); //to empty the list in case its filled inside the loop
                objCA_JVDetails.lstCVarA_JVDetails.RemoveAll(r => 1 == 1); //to empty the list in case its filled inside the loop
                DateTime pJVDate = DateTime.ParseExact(ArrJVDateList[i], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string pJVDateInyyyyMMddFormat = pJVDate.ToString("yyyyMMdd");
                checkException = objCvwA_Voucher.GetList("WHERE ID = " + ArrVoucherIDsList[i] + ""); //i put it here to handle order of retrieving
                DateTime pVoucherDate = objCvwA_Voucher.lstCVarvwA_Voucher[0].VoucherDate;
                int pTransType_ID = objCvwA_Voucher.lstCVarvwA_Voucher[0].TransType_ID;
                checkException = objCA_Fiscal_Year_Period.GetList("WHERE '" + pVoucherDate.ToString("yyyy/MM/dd") + "' BETWEEN From_Date AND To_Date and Closed=1");
                checkException = objCFreezeSystemTransactions.GetList("WHERE TransType='All' AND ('" + pVoucherDate.ToString("yyyy/MM/dd") + "' BETWEEN FromDate AND ToDate)");
                if (objCFreezeSystemTransactions.lstCVarFreezeSystemTransactions.Count == 0
                    /*&& objCA_Fiscal_Year_Period.lstCVarA_Fiscal_Year_Period.Count == 0*/) //not closed and date not frozen
                {
                    checkException = objCOriginalvwA_JV.GetList("WHERE ID=" + objCvwA_Voucher.lstCVarvwA_Voucher[0].JVID1);
                    decimal pTaxValue1 = objCvwA_Voucher.lstCVarvwA_Voucher[0].TaxValue;
                    bool pIsDebitAccount1 = objCvwA_Voucher.lstCVarvwA_Voucher[0].IsDebitAccount;
                    decimal pTaxValue2 = objCvwA_Voucher.lstCVarvwA_Voucher[0].TaxValue2;
                    decimal pTaxValue = 0;
                    bool pIsDebitAccount2 = objCvwA_Voucher.lstCVarvwA_Voucher[0].IsDebitAccount2;
                    int pCurrencyID = objCvwA_Voucher.lstCVarvwA_Voucher[0].CurrencyID;
                    decimal pExchangeRate = decimal.Parse(objCCustomizedDBCall.CallStringFunction("SELECT dbo.Get_Exch_Rate (" + pCurrencyID + ",'" + Forwarding.MvcApp.Helpers.DateFunctionsController.GetYYYYMMDDFormat(ArrJVDateList[i], 1) + "')"));
                    #region Post (Collected or ToSafe)
                    if (pOption == 1) //Post
                    {
                        #region Income
                        if (objCvwA_Voucher.lstCVarvwA_Voucher[0].VoucherType == constVoucherChequeIn)
                        {
                            objCA_Voucher.GetList(" WHERE ID = " + ArrVoucherIDsList[i].ToString());

                            decimal pTotalAmount = 0;
                            pTaxValue = (pIsDebitAccount1 ? pTaxValue1 : 0) + (pIsDebitAccount2 ? pTaxValue2 : 0);
                            if (objCA_Voucher.lstCVarA_Voucher.Count > 0 && objCA_Voucher.lstCVarA_Voucher[0].JVID2 == 0)
                                pTotalAmount = decimal.Parse(ArrAmountList[i]) - pTaxValue ;
                            else pTotalAmount = decimal.Parse(ArrAmountList[i]);

                            #region A_JVDetails Row 1
                            CVarA_JVDetails objCVarA_JVDetails1 = new CVarA_JVDetails();
                            //objCVarA_JVDetails.JV_ID = objCVarA_JV.ID;
                            objCVarA_JVDetails1.Account_ID = (ArrSafeAccountIDList[i] == "0" ? int.Parse(ArrBankAccountIDList[i]) : int.Parse(ArrSafeAccountIDList[i]));
                            //objCVarA_JVDetails.SubAccount_ID = 0;
                            //objCVarA_JVDetails.CostCenter_ID = 0;
                            objCVarA_JVDetails1.Debit = pTotalAmount;//- decimal.Parse(ArrCollectionExpensesList[i]);
                            objCVarA_JVDetails1.Credit = 0;
                            objCVarA_JVDetails1.Currency_ID = pCurrencyID;
                            objCVarA_JVDetails1.ExchangeRate = pExchangeRate;
                            objCVarA_JVDetails1.LocalDebit = objCVarA_JVDetails1.Debit * pExchangeRate;
                            objCVarA_JVDetails1.LocalCredit = 0;
                            objCVarA_JVDetails1.Description = objCvwA_Voucher.lstCVarvwA_Voucher[0].Notes;//"0";
                            objCVarA_JVDetails1.Branch_ID = Convert.ToInt32(BranchID);
                            objCA_JVDetails.lstCVarA_JVDetails.Add(objCVarA_JVDetails1);



                            #endregion A_JVDetails Row 1
                            #region A_JVDetails Row 2
                            CVarA_JVDetails objCVarA_JVDetails2 = new CVarA_JVDetails();
                            //objCVarA_JVDetails2.JV_ID = objCVarA_JV.ID;
                            objCVarA_JVDetails2.Account_ID = int.Parse(ArrBankNotesReceivableUnderCollectionList[i]);
                            //objCVarA_JVDetails2.SubAccount_ID = 0;
                            //objCVarA_JVDetails2.CostCenter_ID = 0;
                            objCVarA_JVDetails2.Debit = 0;
                            objCVarA_JVDetails2.Credit = pTotalAmount;
                            objCVarA_JVDetails2.Currency_ID = pCurrencyID;
                            objCVarA_JVDetails2.ExchangeRate = pExchangeRate;
                            objCVarA_JVDetails2.LocalDebit = 0;
                            objCVarA_JVDetails2.LocalCredit = pTotalAmount * pExchangeRate;
                            objCVarA_JVDetails2.Description = objCvwA_Voucher.lstCVarvwA_Voucher[0].Notes; //"0";
                            objCVarA_JVDetails2.Branch_ID = Convert.ToInt32(BranchID);
                            objCA_JVDetails.lstCVarA_JVDetails.Add(objCVarA_JVDetails2);
                            #endregion A_JVDetails Row 2
                            #region A_JVDetails Row 3 //if Collection Expense exists
                            if (ArrCollectionExpensesList[i] != "0")
                            {
                                CVarA_JVDetails objCVarA_JVdetails3 = new CVarA_JVDetails();
                                //objCVarA_JVdetails3.JV_ID = objCVarA_JV.ID;
                                objCVarA_JVdetails3.Account_ID = int.Parse(ArrBankCollectionExpensesIDsList[i]);
                                //objCVarA_JVdetails3.SubAccount_ID = 0;
                                //objCVarA_JVdetails3.CostCenter_ID = 0;
                                objCVarA_JVdetails3.Debit = decimal.Parse(ArrCollectionExpensesList[i]);
                                objCVarA_JVdetails3.Credit = 0;
                                objCVarA_JVdetails3.Currency_ID = pCurrencyID;
                                objCVarA_JVdetails3.ExchangeRate = pExchangeRate;
                                objCVarA_JVdetails3.LocalDebit = decimal.Parse(ArrCollectionExpensesList[i]) * pExchangeRate;
                                objCVarA_JVdetails3.LocalCredit = 0;
                                objCVarA_JVdetails3.Description = "0";
                                objCVarA_JVdetails3.Branch_ID = Convert.ToInt32(BranchID);

                                objCA_JVDetails.lstCVarA_JVDetails.Add(objCVarA_JVdetails3);


                                CVarA_JVDetails objCVarA_JVdetails4 = new CVarA_JVDetails();
                                //objCVarA_JVdetails3.JV_ID = objCVarA_JV.ID;
                                objCVarA_JVdetails4.Account_ID = (ArrSafeAccountIDList[i] == "0" ? int.Parse(ArrBankAccountIDList[i]) : int.Parse(ArrSafeAccountIDList[i]));
                                //objCVarA_JVdetails3.SubAccount_ID = 0;
                                //objCVarA_JVdetails3.CostCenter_ID = 0;
                                objCVarA_JVdetails4.Debit = 0 ;
                                objCVarA_JVdetails4.Credit = decimal.Parse(ArrCollectionExpensesList[i]);
                                objCVarA_JVdetails4.Currency_ID = pCurrencyID;
                                objCVarA_JVdetails4.ExchangeRate = pExchangeRate;
                                objCVarA_JVdetails4.LocalDebit = 0;
                                objCVarA_JVdetails4.LocalCredit = decimal.Parse(ArrCollectionExpensesList[i]) * pExchangeRate;
                                objCVarA_JVdetails4.Description = "0";
                                objCVarA_JVdetails4.Branch_ID = Convert.ToInt32(BranchID);

                                objCA_JVDetails.lstCVarA_JVDetails.Add(objCVarA_JVdetails4);
                            }
                            #endregion A_JVDetails Row 3 //if Collection Expense exists
                            #region Save JV Header And Details
                            int pJournal_ID = (ArrSafeIDList[i] != "0"
                                              ? pJournalID17
                                              : (
                                                    ArrBankIDList[i] == "0"
                                                    ? pJournalID17
                                                    : int.Parse(ArrBankInJournalTypeIDList[i])
                                                )
                                           );
                            string pNewJVCode = objCCustomizedDBCall.SP_A_JV_Get_Code("SP_A_JV_Get_Code", pJVDate, WebSecurity.CurrentUserId, pJournal_ID);
                            if (objCA_JVDetails.lstCVarA_JVDetails.Count == 2)
                            {
                                if (objCA_JVDetails.lstCVarA_JVDetails[0].Account_ID != objCA_JVDetails.lstCVarA_JVDetails[1].Account_ID)
                                {
                                    decimal TotalDebit = objCA_JVDetails.lstCVarA_JVDetails.Sum(s => s.LocalDebit);
                                    objCVarA_JV.JVNo = pNewJVCode;
                                    objCVarA_JV.JVDate = pJVDate;
                                    objCVarA_JV.TotalDebit = TotalDebit;// - pTaxValue;
                                    objCVarA_JV.TotalCredit = objCVarA_JV.TotalDebit;
                                    objCVarA_JV.Journal_ID = pJournal_ID;
                                    objCVarA_JV.JVType_ID = pJVTypeID17;
                                    objCVarA_JV.ReceiptNo = objCOriginalvwA_JV.lstCVarvwA_JV[0].ReceiptNo;
                                    objCVarA_JV.RemarksHeader = objCOriginalvwA_JV.lstCVarvwA_JV[0].RemarksHeader;
                                    objCVarA_JV.Deleted = false;
                                    objCVarA_JV.Posted = true;
                                    objCVarA_JV.User_ID = WebSecurity.CurrentUserId;
                                    objCVarA_JV.TransType_ID = pTransType_ID;
                                    objCVarA_JV.IsSysJv = true;
                                    objCA_JV.lstCVarA_JV.Add(objCVarA_JV);
                                    checkException = objCA_JV.SaveMethod(objCA_JV.lstCVarA_JV);
                                    if (checkException == null)
                                    {
                                        objCCustomizedDBCall.SP_Trans_Log("SP_Trans_Log", WebSecurity.CurrentUserId, "A_JV", objCVarA_JV.ID, "I");
                                    }
                                    else
                                        _result = false;
                                }
                            }
                            else
                            {
                                objCVarA_JV.JVNo = pNewJVCode;
                                objCVarA_JV.JVDate = pJVDate;
                                objCVarA_JV.TotalDebit = objCOriginalvwA_JV.lstCVarvwA_JV[0].TotalDebit - pTaxValue + decimal.Parse(ArrCollectionExpensesList[i]) * pExchangeRate;
                                objCVarA_JV.TotalCredit = objCVarA_JV.TotalDebit;
                                objCVarA_JV.Journal_ID = pJournal_ID;
                                objCVarA_JV.JVType_ID = pJVTypeID17;
                                objCVarA_JV.ReceiptNo = objCOriginalvwA_JV.lstCVarvwA_JV[0].ReceiptNo;
                                objCVarA_JV.RemarksHeader = objCOriginalvwA_JV.lstCVarvwA_JV[0].RemarksHeader;
                                objCVarA_JV.Deleted = false;
                                objCVarA_JV.Posted = true;
                                objCVarA_JV.User_ID = WebSecurity.CurrentUserId;
                                objCVarA_JV.TransType_ID = pTransType_ID;
                                objCVarA_JV.IsSysJv = true;
                                objCA_JV.lstCVarA_JV.Add(objCVarA_JV);
                                checkException = objCA_JV.SaveMethod(objCA_JV.lstCVarA_JV);
                                if (checkException == null)
                                {
                                    objCCustomizedDBCall.SP_Trans_Log("SP_Trans_Log", WebSecurity.CurrentUserId, "A_JV", objCVarA_JV.ID, "I");
                                }
                                else
                                    _result = false;
                            }
                            if (_result)
                            {
                                for (int j = 0; j < objCA_JVDetails.lstCVarA_JVDetails.Count; j++)
                                    objCA_JVDetails.lstCVarA_JVDetails[j].JV_ID = objCVarA_JV.ID;
                                checkException = objCA_JVDetails.SaveMethod(objCA_JVDetails.lstCVarA_JVDetails);
                                checkException = objCA_Voucher.UpdateList("JVID3=" + objCVarA_JV.ID
                                            + " ,CollectionDate='" + pJVDateInyyyyMMddFormat + "'"
                                            + " ,CollectionExpense=" + ArrCollectionExpensesList[i]
                                            + " ,BankID=" + ArrBankIDList[i]
                                            + " WHERE ID=" + ArrVoucherIDsList[i]);
                                checkException = objCA_ChequeStatus.UpdateList("Type=1"
                                    + " ,BankID=" + ArrBankIDList[i]
                                    + " ,JVID=NULL , Posted=1, UnderCollection=0"
                                    + " ,Collected=" + (ArrSafeIDList[i] == "0" ? "1" : "1")
                                    + " ,Returned=0, ToSafe=" + (ArrSafeIDList[i] == "0" ? "0" : "1")
                                    + " ,SafeID=" + (ArrSafeIDList[i] == "0" ? "null" : ArrSafeIDList[i])
                                    //+ " ,DueDate='" + DateTime.ParseExact(ArrDueDateList[i], "dd/MM/yyyy", CultureInfo.InvariantCulture) + "'"
                                    + " WHERE ID=" + ArrChequeStatusIDList[i]);
                            }
                            #endregion Save JVHeader And Details
                        }
                        #endregion Income
                        #region Outcome
                        else if (objCvwA_Voucher.lstCVarvwA_Voucher[0].VoucherType == constVoucherChequeOut)
                        {
                            decimal pTotalAmount = 0;
                            pTaxValue = pTaxValue1 + pTaxValue2; //(pIsDebitAccount1 ? pTaxValue1 : 0) + (pIsDebitAccount2 ? pTaxValue2 : 0);
                            pExchangeRate = objCvwA_Voucher.lstCVarvwA_Voucher[0].ExchangeRate;

                            pTotalAmount = decimal.Parse(ArrAmountList[i]) - pTaxValue;

                            #region A_JVDetails Row 1
                            CVarA_JVDetails objCVarA_JVDetails1 = new CVarA_JVDetails();
                            //objCVarA_JVDetails.JV_ID = objCVarA_JV.ID;
                            objCVarA_JVDetails1.Account_ID = int.Parse(ArrBankAccountIDList[i]);
                            //objCVarA_JVDetails.SubAccount_ID = 0;
                            //objCVarA_JVDetails.CostCenter_ID = 0;
                            objCVarA_JVDetails1.Debit = 0;
                            objCVarA_JVDetails1.Credit = pTotalAmount + +decimal.Parse(ArrCollectionExpensesList[i]);  //decimal.Parse(ArrAmountList[i]) + decimal.Parse(ArrCollectionExpensesList[i]);
                            objCVarA_JVDetails1.Currency_ID = pCurrencyID;
                            objCVarA_JVDetails1.ExchangeRate = pExchangeRate;
                            objCVarA_JVDetails1.LocalDebit = 0;
                            objCVarA_JVDetails1.LocalCredit = objCVarA_JVDetails1.Credit * pExchangeRate;
                            objCVarA_JVDetails1.Description = objCvwA_Voucher.lstCVarvwA_Voucher[0].Notes; // "0";
                            objCVarA_JVDetails1.Branch_ID = Convert.ToInt32(BranchID);

                            objCA_JVDetails.lstCVarA_JVDetails.Add(objCVarA_JVDetails1);
                            #endregion A_JVDetails Row 1
                            #region A_JVDetails Row 2 //if Collection Expense exists
                            if (ArrCollectionExpensesList[i] != "0")
                            {
                                CVarA_JVDetails objCVarA_JVDetails2 = new CVarA_JVDetails();
                                //objCVarA_JVDetails2.JV_ID = objCVarA_JV.ID;
                                objCVarA_JVDetails2.Account_ID = int.Parse(ArrBankCollectionExpensesIDsList[i]);
                                //objCVarA_JVDetails2.SubAccount_ID = 0;
                                //objCVarA_JVDetails2.CostCenter_ID = 0;
                                objCVarA_JVDetails2.Debit = decimal.Parse(ArrCollectionExpensesList[i]);
                                objCVarA_JVDetails2.Credit = 0;
                                objCVarA_JVDetails2.Currency_ID = pCurrencyID;
                                objCVarA_JVDetails2.ExchangeRate = pExchangeRate;
                                objCVarA_JVDetails2.LocalDebit = objCVarA_JVDetails2.Debit * pExchangeRate;
                                objCVarA_JVDetails2.LocalCredit = 0;
                                objCVarA_JVDetails2.Description = "0";
                                objCVarA_JVDetails2.Branch_ID = Convert.ToInt32(BranchID);

                                objCA_JVDetails.lstCVarA_JVDetails.Add(objCVarA_JVDetails2);
                            }
                            #endregion A_JVDetails Row 2 //if Collection Expense exists
                            #region A_JVDetails Row 3 //BankAccount
                            CVarA_JVDetails objCVarA_JVDetails3 = new CVarA_JVDetails();
                            //objCVarA_JVDetails3.JV_ID = objCVarA_JV.ID;
                            objCVarA_JVDetails3.Account_ID = int.Parse(ArrBankNotesPayableUnderCollectionList[i]);
                            //objCVarA_JVDetails3.SubAccount_ID = 0;
                            //objCVarA_JVDetails3.CostCenter_ID = 0;
                            objCVarA_JVDetails3.Debit = pTotalAmount;//;decimal.Parse(ArrAmountList[i]);
                            objCVarA_JVDetails3.Credit = 0;
                            objCVarA_JVDetails3.Currency_ID = pCurrencyID;
                            objCVarA_JVDetails3.ExchangeRate = pExchangeRate;
                            objCVarA_JVDetails3.Debit = pTotalAmount;//;decimal.Parse(ArrAmountList[i]);
                            objCVarA_JVDetails3.LocalDebit = objCVarA_JVDetails3.Debit * pExchangeRate;   //decimal.Parse(ArrAmountList[i]) * pExchangeRate;
                            objCVarA_JVDetails3.LocalCredit = 0;
                            objCVarA_JVDetails3.Description = "0";
                            objCVarA_JVDetails3.Branch_ID = Convert.ToInt32(BranchID);

                            objCA_JVDetails.lstCVarA_JVDetails.Add(objCVarA_JVDetails3);
                            #endregion A_JVDetails Row 3  //BankAccount
                            #region Save JV Header And Details
                            int pJournal_ID = (
                                                    ArrBankOutJournalTypeIDList[i] == "0"
                                                    ? pJournalID19
                                                    : int.Parse(ArrBankOutJournalTypeIDList[i])
                                           );
                            string pNewJVCode = objCCustomizedDBCall.SP_A_JV_Get_Code("SP_A_JV_Get_Code", pJVDate, WebSecurity.CurrentUserId, pJournal_ID);
                            decimal TotalDebit = objCA_JVDetails.lstCVarA_JVDetails.Sum(s => s.LocalDebit);
                            decimal TotalCredit = objCA_JVDetails.lstCVarA_JVDetails.Sum(s => s.LocalCredit);
                            if (objCA_JVDetails.lstCVarA_JVDetails.Count == 2)
                            {
                                if (objCA_JVDetails.lstCVarA_JVDetails[0].Account_ID != objCA_JVDetails.lstCVarA_JVDetails[1].Account_ID)
                                {
                                    objCVarA_JV.JVNo = pNewJVCode;
                                    objCVarA_JV.JVDate = pJVDate;
                                    objCVarA_JV.TotalDebit = TotalDebit;
                                    objCVarA_JV.TotalCredit = TotalCredit;
                                    objCVarA_JV.Journal_ID = pJournal_ID;
                                    objCVarA_JV.JVType_ID = pJVTypeID19;
                                    objCVarA_JV.ReceiptNo = objCOriginalvwA_JV.lstCVarvwA_JV[0].ReceiptNo;
                                    objCVarA_JV.RemarksHeader = objCOriginalvwA_JV.lstCVarvwA_JV[0].RemarksHeader;
                                    objCVarA_JV.Deleted = false;
                                    objCVarA_JV.Posted = true;
                                    objCVarA_JV.User_ID = WebSecurity.CurrentUserId;
                                    objCVarA_JV.TransType_ID = pTransType_ID;
                                    objCVarA_JV.IsSysJv = true;
                                    objCA_JV.lstCVarA_JV.Add(objCVarA_JV);
                                    checkException = objCA_JV.SaveMethod(objCA_JV.lstCVarA_JV);
                                    if (checkException == null)
                                    {
                                        objCCustomizedDBCall.SP_Trans_Log("SP_Trans_Log", WebSecurity.CurrentUserId, "A_JV", objCVarA_JV.ID, "I");
                                    }
                                    else
                                        _result = false;
                                }
                            }
                            else
                            {
                                objCVarA_JV.JVNo = pNewJVCode;
                                objCVarA_JV.JVDate = pJVDate;
                                objCVarA_JV.TotalDebit = TotalDebit;
                                objCVarA_JV.TotalCredit = TotalCredit;
                                objCVarA_JV.Journal_ID = pJournalID19;
                                objCVarA_JV.JVType_ID = pJVTypeID19;
                                objCVarA_JV.ReceiptNo = objCOriginalvwA_JV.lstCVarvwA_JV[0].ReceiptNo;
                                objCVarA_JV.RemarksHeader = objCOriginalvwA_JV.lstCVarvwA_JV[0].RemarksHeader;
                                objCVarA_JV.Deleted = false;
                                objCVarA_JV.Posted = true;
                                objCVarA_JV.User_ID = WebSecurity.CurrentUserId;
                                objCVarA_JV.TransType_ID = pTransType_ID;
                                objCVarA_JV.IsSysJv = true;
                                objCA_JV.lstCVarA_JV.Add(objCVarA_JV);
                                checkException = objCA_JV.SaveMethod(objCA_JV.lstCVarA_JV);
                                if (checkException == null)
                                {
                                    objCCustomizedDBCall.SP_Trans_Log("SP_Trans_Log", WebSecurity.CurrentUserId, "A_JV", objCVarA_JV.ID, "I");
                                }
                                else
                                    _result = false;
                            }
                            if (_result)
                            {
                                for (int j = 0; j < objCA_JVDetails.lstCVarA_JVDetails.Count; j++)
                                    objCA_JVDetails.lstCVarA_JVDetails[j].JV_ID = objCVarA_JV.ID;
                                checkException = objCA_JVDetails.SaveMethod(objCA_JVDetails.lstCVarA_JVDetails);
                                checkException = objCA_Voucher.UpdateList("JVID3=" + objCVarA_JV.ID
                                            + " ,CollectionDate='" + pJVDateInyyyyMMddFormat + "'"
                                              + " ,CollectionExpense=" + ArrCollectionExpensesList[i]
                                            + " WHERE ID=" + ArrVoucherIDsList[i]);
                                checkException = objCA_ChequeStatus.UpdateList("Type=1"
                                    + " ,BankID=" + ArrBankIDList[i]
                                    + " ,JVID=NULL , Posted=1, UnderCollection=1"
                                    + " ,Collected=1"
                                    + " ,Returned=0, ToSafe=0"
                                    + " ,SafeID=null"
                                    //+ " ,DueDate='" + DateTime.ParseExact(ArrDueDateList[i], "dd/MM/yyyy", CultureInfo.InvariantCulture) + "'"
                                    + " WHERE ID=" + ArrChequeStatusIDList[i]);
                            }
                            #endregion Save JVHeader And Details
                        }
                        #endregion Outcome
                    }
                    #endregion Post (Collected or ToSafe)
                    #region Returned
                    else if (pOption == 0) //Returned
                    {
                        #region Income
                        if (objCvwA_Voucher.lstCVarvwA_Voucher[0].VoucherType == constVoucherChequeIn)
                        {
                            pTaxValue = (pIsDebitAccount1 ? pTaxValue1 : 0) + (pIsDebitAccount2 ? pTaxValue2 : 0);
                            #region A_JVDetails Row 1
                            CVarA_JVDetails objCVarA_JVDetails1 = new CVarA_JVDetails();
                            //objCVarA_JVDetails.JV_ID = objCVarA_JV.ID;
                            objCVarA_JVDetails1.Account_ID = int.Parse(ArrBankNotesReceivableList[i]);
                            //objCVarA_JVDetails.SubAccount_ID = 0;
                            //objCVarA_JVDetails.CostCenter_ID = 0;
                            objCVarA_JVDetails1.Debit = decimal.Parse(ArrAmountList[i]);
                            objCVarA_JVDetails1.Credit = 0;
                            objCVarA_JVDetails1.Currency_ID = pCurrencyID;
                            objCVarA_JVDetails1.ExchangeRate = pExchangeRate;
                            objCVarA_JVDetails1.LocalDebit = objCVarA_JVDetails1.Debit * pExchangeRate;
                            objCVarA_JVDetails1.LocalCredit = 0;
                            objCVarA_JVDetails1.Description = "0";
                            objCA_JVDetails.lstCVarA_JVDetails.Add(objCVarA_JVDetails1);
                            #endregion A_JVDetails Row 1
                            #region A_JVDetails Row 2
                            CVarA_JVDetails objCVarA_JVDetails2 = new CVarA_JVDetails();
                            //objCVarA_JVDetails2.JV_ID = objCVarA_JV.ID;
                            objCVarA_JVDetails2.Account_ID = int.Parse(ArrBankNotesReceivableUnderCollectionList[i]);
                            //objCVarA_JVDetails2.SubAccount_ID = 0;
                            //objCVarA_JVDetails2.CostCenter_ID = 0;
                            objCVarA_JVDetails2.Debit = 0;
                            objCVarA_JVDetails2.Credit = objCVarA_JVDetails1.Debit;
                            objCVarA_JVDetails2.Currency_ID = pCurrencyID;
                            objCVarA_JVDetails2.ExchangeRate = pExchangeRate;
                            objCVarA_JVDetails2.LocalDebit = 0;
                            objCVarA_JVDetails2.LocalCredit = objCVarA_JVDetails1.LocalDebit;
                            objCVarA_JVDetails2.Description = "0";
                            objCA_JVDetails.lstCVarA_JVDetails.Add(objCVarA_JVDetails2);
                            #endregion A_JVDetails Row 2
                            #region Save JV Header And Details
                            string pNewJVCode = objCCustomizedDBCall.SP_A_JV_Get_Code("SP_A_JV_Get_Code", pJVDate, WebSecurity.CurrentUserId, pJournalID16);
                            if (objCA_JVDetails.lstCVarA_JVDetails[0].Account_ID != objCA_JVDetails.lstCVarA_JVDetails[1].Account_ID)
                            {
                                objCVarA_JV.JVNo = pNewJVCode;
                                objCVarA_JV.JVDate = pJVDate;
                                objCVarA_JV.TotalDebit = objCOriginalvwA_JV.lstCVarvwA_JV[0].TotalDebit - pTaxValue;
                                objCVarA_JV.TotalCredit = objCOriginalvwA_JV.lstCVarvwA_JV[0].TotalCredit - pTaxValue;
                                objCVarA_JV.Journal_ID = pJournalID16;
                                objCVarA_JV.JVType_ID = pJVTypeID16;
                                objCVarA_JV.ReceiptNo = objCOriginalvwA_JV.lstCVarvwA_JV[0].ReceiptNo;
                                objCVarA_JV.RemarksHeader = objCOriginalvwA_JV.lstCVarvwA_JV[0].RemarksHeader;
                                objCVarA_JV.Deleted = false;
                                objCVarA_JV.Posted = true;
                                objCVarA_JV.User_ID = WebSecurity.CurrentUserId;
                                objCVarA_JV.TransType_ID = pTransType_ID;
                                objCVarA_JV.IsSysJv = true;
                                objCA_JV.lstCVarA_JV.Add(objCVarA_JV);
                                checkException = objCA_JV.SaveMethod(objCA_JV.lstCVarA_JV);
                                if (checkException == null)
                                {
                                    objCCustomizedDBCall.SP_Trans_Log("SP_Trans_Log", WebSecurity.CurrentUserId, "A_JV", objCVarA_JV.ID, "I");
                                    for (int j = 0; j < objCA_JVDetails.lstCVarA_JVDetails.Count; j++)
                                        objCA_JVDetails.lstCVarA_JVDetails[j].JV_ID = objCVarA_JV.ID;
                                    checkException = objCA_JVDetails.SaveMethod(objCA_JVDetails.lstCVarA_JVDetails);
                                    checkException = objCA_Voucher.UpdateList("JVID3=" + objCVarA_JV.ID
                                                //+ " ,CollectionDate='" + pJVDateInyyyyMMddFormat + "'"
                                                //+ " ,CollectionExpense=" + ArrCollectionExpensesList[i]
                                                //+ " ,BankID=" + ArrBankIDList[i]
                                                + " WHERE ID=" + ArrVoucherIDsList[i]);
                                }
                                else
                                    _result = false;
                            } //of if (objCA_JVDetails.lstCVarA_JVDetails[0].Account_ID != objCA_JVDetails.lstCVarA_JVDetails[1].Account_ID)
                            //(Add Last JV with opposite details for the first JV) Adding last JV to close the Notes Receivable
                            if (_result)
                            {
                                objCA_JVDetails.GetList("WHERE JV_ID=" + objCvwA_Voucher.lstCVarvwA_Voucher[0].JVID1);
                                for (int j = 0; j < objCA_JVDetails.lstCVarA_JVDetails.Count; j++) //i swap the previous details
                                {
                                    decimal tempVar = 0;
                                    tempVar = objCA_JVDetails.lstCVarA_JVDetails[j].Debit;
                                    objCA_JVDetails.lstCVarA_JVDetails[j].Debit = objCA_JVDetails.lstCVarA_JVDetails[j].Credit;
                                    objCA_JVDetails.lstCVarA_JVDetails[j].Credit = tempVar;
                                    tempVar = objCA_JVDetails.lstCVarA_JVDetails[j].LocalDebit;
                                    objCA_JVDetails.lstCVarA_JVDetails[j].LocalDebit = objCA_JVDetails.lstCVarA_JVDetails[j].LocalCredit;
                                    objCA_JVDetails.lstCVarA_JVDetails[j].LocalCredit = tempVar;
                                    objCA_JVDetails.lstCVarA_JVDetails[j].ID = 0;
                                }
                                objCA_JV.lstCVarA_JV.RemoveAll(r => 1 == 1); //to empty the list 
                                pNewJVCode = objCCustomizedDBCall.SP_A_JV_Get_Code("SP_A_JV_Get_Code", pJVDate, WebSecurity.CurrentUserId, pJournalID16);
                                objCVarA_JV.ID = 0;
                                objCVarA_JV.JVNo = pNewJVCode;
                                objCVarA_JV.JVDate = pJVDate;
                                objCVarA_JV.TotalDebit = objCOriginalvwA_JV.lstCVarvwA_JV[0].TotalDebit;
                                objCVarA_JV.TotalCredit = objCOriginalvwA_JV.lstCVarvwA_JV[0].TotalCredit;
                                objCVarA_JV.Journal_ID = pJournalID16;
                                objCVarA_JV.JVType_ID = pJVTypeID16;
                                objCVarA_JV.ReceiptNo = objCOriginalvwA_JV.lstCVarvwA_JV[0].ReceiptNo;
                                objCVarA_JV.RemarksHeader = objCOriginalvwA_JV.lstCVarvwA_JV[0].RemarksHeader;
                                objCVarA_JV.Deleted = false;
                                objCVarA_JV.Posted = true;
                                objCVarA_JV.User_ID = WebSecurity.CurrentUserId;
                                objCVarA_JV.TransType_ID = pTransType_ID;
                                objCVarA_JV.IsSysJv = true;
                                objCA_JV.lstCVarA_JV.Add(objCVarA_JV);
                                checkException = objCA_JV.SaveMethod(objCA_JV.lstCVarA_JV);
                                if (checkException == null)
                                {
                                    objCCustomizedDBCall.SP_Trans_Log("SP_Trans_Log", WebSecurity.CurrentUserId, "A_JV", objCVarA_JV.ID, "I");
                                    for (int j = 0; j < objCA_JVDetails.lstCVarA_JVDetails.Count; j++)
                                        objCA_JVDetails.lstCVarA_JVDetails[j].JV_ID = objCVarA_JV.ID;
                                    checkException = objCA_JVDetails.SaveMethod(objCA_JVDetails.lstCVarA_JVDetails);
                                    checkException = objCA_Voucher.UpdateList("JVID4=" + objCVarA_JV.ID
                                                //+ " ,CollectionDate='" + pJVDateInyyyyMMddFormat + "'"
                                                //+ " ,CollectionExpense=" + ArrCollectionExpensesList[i]
                                                //+ " ,BankID=" + ArrBankIDList[i]
                                                + " WHERE ID=" + ArrVoucherIDsList[i]);
                                    checkException = objCA_ChequeStatus.UpdateList("Type=1"
                                        + " ,BankID=" + ArrBankIDList[i]
                                        + " ,JVID=NULL , Posted=1, UnderCollection=0"
                                        + " ,Collected=0 ,Returned=1, ToSafe=0"
                                        + " ,SafeID=NULL"
                                        //+ " ,DueDate='" + DateTime.ParseExact(ArrDueDateList[i], "dd/MM/yyyy", CultureInfo.InvariantCulture) + "'"
                                        + " WHERE ID=" + ArrChequeStatusIDList[i]);
                                }
                                else
                                    _result = false;
                            }
                            #endregion Save JV Header And Details
                        }
                        #endregion Income
                        #region Outcome
                        else if (objCvwA_Voucher.lstCVarvwA_Voucher[0].VoucherType == constVoucherChequeOut)
                        {
                            pTaxValue = (!pIsDebitAccount1 ? pTaxValue1 : 0) + (!pIsDebitAccount2 ? pTaxValue2 : 0);
                            #region A_JVDetails Row 1
                            CVarA_JVDetails objCVarA_JVDetails1 = new CVarA_JVDetails();
                            //objCVarA_JVDetails.JV_ID = objCVarA_JV.ID;
                            objCVarA_JVDetails1.Account_ID = int.Parse(ArrBankNotesPayableUnderCollectionList[i]);
                            //objCVarA_JVDetails.SubAccount_ID = 0;
                            //objCVarA_JVDetails.CostCenter_ID = 0;
                            objCVarA_JVDetails1.Debit = decimal.Parse(ArrAmountList[i]);
                            objCVarA_JVDetails1.Credit = 0;
                            objCVarA_JVDetails1.Currency_ID = pCurrencyID;
                            objCVarA_JVDetails1.ExchangeRate = pExchangeRate;
                            objCVarA_JVDetails1.LocalDebit = objCVarA_JVDetails1.Debit * pExchangeRate;
                            objCVarA_JVDetails1.LocalCredit = 0;
                            objCVarA_JVDetails1.Description = "0";
                            objCA_JVDetails.lstCVarA_JVDetails.Add(objCVarA_JVDetails1);
                            #endregion A_JVDetails Row 1
                            #region A_JVDetails Row 2
                            CVarA_JVDetails objCVarA_JVDetails2 = new CVarA_JVDetails();
                            //objCVarA_JVDetails2.JV_ID = objCVarA_JV.ID;
                            objCVarA_JVDetails2.Account_ID = int.Parse(ArrBankNotesPayable[i]);
                            //objCVarA_JVDetails2.SubAccount_ID = 0;
                            //objCVarA_JVDetails2.CostCenter_ID = 0;
                            objCVarA_JVDetails2.Debit = objCVarA_JVDetails1.Credit;
                            objCVarA_JVDetails2.Credit = objCVarA_JVDetails1.Debit;
                            objCVarA_JVDetails2.Currency_ID = pCurrencyID;
                            objCVarA_JVDetails2.ExchangeRate = pExchangeRate;
                            objCVarA_JVDetails2.LocalDebit = objCVarA_JVDetails1.LocalCredit;
                            objCVarA_JVDetails2.LocalCredit = objCVarA_JVDetails1.LocalDebit;
                            objCVarA_JVDetails2.Description = "0";
                            objCA_JVDetails.lstCVarA_JVDetails.Add(objCVarA_JVDetails2);
                            #endregion A_JVDetails Row 2
                            #region Save JV Header And Details
                            string pNewJVCode = objCCustomizedDBCall.SP_A_JV_Get_Code("SP_A_JV_Get_Code", pJVDate, WebSecurity.CurrentUserId, pJournalID16);
                            if (objCA_JVDetails.lstCVarA_JVDetails[0].Account_ID != objCA_JVDetails.lstCVarA_JVDetails[1].Account_ID)
                            {
                                objCVarA_JV.JVNo = pNewJVCode;
                                objCVarA_JV.JVDate = pJVDate;
                                objCVarA_JV.TotalDebit = objCOriginalvwA_JV.lstCVarvwA_JV[0].TotalDebit + decimal.Parse(ArrCollectionExpensesList[i]) - pTaxValue;
                                objCVarA_JV.TotalCredit = objCOriginalvwA_JV.lstCVarvwA_JV[0].TotalCredit + decimal.Parse(ArrCollectionExpensesList[i]) - pTaxValue;
                                objCVarA_JV.Journal_ID = pJournalID16;
                                objCVarA_JV.JVType_ID = pJVTypeID16;
                                objCVarA_JV.ReceiptNo = objCOriginalvwA_JV.lstCVarvwA_JV[0].ReceiptNo;
                                objCVarA_JV.RemarksHeader = objCOriginalvwA_JV.lstCVarvwA_JV[0].RemarksHeader;
                                objCVarA_JV.Deleted = false;
                                objCVarA_JV.Posted = true;
                                objCVarA_JV.User_ID = WebSecurity.CurrentUserId;
                                objCVarA_JV.TransType_ID = pTransType_ID;
                                objCVarA_JV.IsSysJv = true;
                                objCA_JV.lstCVarA_JV.Add(objCVarA_JV);
                                checkException = objCA_JV.SaveMethod(objCA_JV.lstCVarA_JV);
                                if (checkException == null)
                                {
                                    objCCustomizedDBCall.SP_Trans_Log("SP_Trans_Log", WebSecurity.CurrentUserId, "A_JV", objCVarA_JV.ID, "I");
                                    for (int j = 0; j < objCA_JVDetails.lstCVarA_JVDetails.Count; j++)
                                        objCA_JVDetails.lstCVarA_JVDetails[j].JV_ID = objCVarA_JV.ID;
                                    checkException = objCA_JVDetails.SaveMethod(objCA_JVDetails.lstCVarA_JVDetails);
                                    checkException = objCA_Voucher.UpdateList("JVID3=" + objCVarA_JV.ID
                                                //+ " ,CollectionDate='" + pJVDateInyyyyMMddFormat + "'"
                                                //+ " ,CollectionExpense=" + ArrCollectionExpensesList[i]
                                                //+ " ,BankID=" + ArrBankIDList[i]
                                                + " WHERE ID=" + ArrVoucherIDsList[i]);
                                }
                                else
                                    _result = false;
                            } //of if (objCA_JVDetails.lstCVarA_JVDetails[0].Account_ID != objCA_JVDetails.lstCVarA_JVDetails[1].Account_ID)
                            //(Add Last JV with opposite details for the first JV) Adding last JV to close the Notes Receivable
                            if (_result)
                            {
                                objCA_JVDetails.GetList("WHERE JV_ID=" + objCvwA_Voucher.lstCVarvwA_Voucher[0].JVID1);
                                for (int j = 0; j < objCA_JVDetails.lstCVarA_JVDetails.Count; j++) //i swap the previous details
                                {
                                    decimal tempVar = 0;
                                    tempVar = objCA_JVDetails.lstCVarA_JVDetails[j].Debit;
                                    objCA_JVDetails.lstCVarA_JVDetails[j].Debit = objCA_JVDetails.lstCVarA_JVDetails[j].Credit;
                                    objCA_JVDetails.lstCVarA_JVDetails[j].Credit = tempVar;
                                    tempVar = objCA_JVDetails.lstCVarA_JVDetails[j].LocalDebit;
                                    objCA_JVDetails.lstCVarA_JVDetails[j].LocalDebit = objCA_JVDetails.lstCVarA_JVDetails[j].LocalCredit;
                                    objCA_JVDetails.lstCVarA_JVDetails[j].LocalCredit = tempVar;
                                    objCA_JVDetails.lstCVarA_JVDetails[j].ID = 0;
                                }
                                objCA_JV.lstCVarA_JV.RemoveAll(r => 1 == 1); //to empty the list 
                                pNewJVCode = objCCustomizedDBCall.SP_A_JV_Get_Code("SP_A_JV_Get_Code", pJVDate, WebSecurity.CurrentUserId, pJournalID16);
                                objCVarA_JV.ID = 0;
                                objCVarA_JV.JVNo = pNewJVCode;
                                objCVarA_JV.JVDate = pJVDate;
                                objCVarA_JV.TotalDebit = objCOriginalvwA_JV.lstCVarvwA_JV[0].TotalDebit;
                                objCVarA_JV.TotalCredit = objCOriginalvwA_JV.lstCVarvwA_JV[0].TotalCredit;
                                objCVarA_JV.Journal_ID = pJournalID16;
                                objCVarA_JV.JVType_ID = pJVTypeID16;
                                objCVarA_JV.ReceiptNo = objCOriginalvwA_JV.lstCVarvwA_JV[0].ReceiptNo;
                                objCVarA_JV.RemarksHeader = objCOriginalvwA_JV.lstCVarvwA_JV[0].RemarksHeader;
                                objCVarA_JV.Deleted = false;
                                objCVarA_JV.Posted = true;
                                objCVarA_JV.User_ID = WebSecurity.CurrentUserId;
                                objCVarA_JV.TransType_ID = pTransType_ID;
                                objCVarA_JV.IsSysJv = true;
                                objCA_JV.lstCVarA_JV.Add(objCVarA_JV);
                                checkException = objCA_JV.SaveMethod(objCA_JV.lstCVarA_JV);
                                if (checkException == null)
                                {
                                    objCCustomizedDBCall.SP_Trans_Log("SP_Trans_Log", WebSecurity.CurrentUserId, "A_JV", objCVarA_JV.ID, "I");
                                    for (int j = 0; j < objCA_JVDetails.lstCVarA_JVDetails.Count; j++)
                                        objCA_JVDetails.lstCVarA_JVDetails[j].JV_ID = objCVarA_JV.ID;
                                    checkException = objCA_JVDetails.SaveMethod(objCA_JVDetails.lstCVarA_JVDetails);
                                    checkException = objCA_Voucher.UpdateList("JVID4=" + objCVarA_JV.ID
                                                //+ " ,CollectionDate='" + pJVDateInyyyyMMddFormat + "'"
                                                //+ " ,CollectionExpense=" + ArrCollectionExpensesList[i]
                                                //+ " ,BankID=" + ArrBankIDList[i]
                                                + " WHERE ID=" + ArrVoucherIDsList[i]);
                                    checkException = objCA_ChequeStatus.UpdateList("Type=1"
                                        + " ,BankID=" + ArrBankIDList[i]
                                        + " ,JVID=NULL , Posted=1, UnderCollection=0"
                                        + " ,Collected=0 ,Returned=1, ToSafe=0"
                                        + " ,SafeID=NULL"
                                        //+ " ,DueDate='" + DateTime.ParseExact(ArrDueDateList[i], "dd/MM/yyyy", CultureInfo.InvariantCulture) + "'"
                                        + " WHERE ID=" + ArrChequeStatusIDList[i]);
                                }
                                else
                                    _result = false;
                            }
                            #endregion Save JV Header And Details
                        }
                        #endregion Outcome
                    }
                    #endregion Return
                }
                else
                    _result = false; //Frozen or closed period
            }
            return new object[] {
                _result
            };
        }

        #endregion ApprovingAndPosting
    }
    public class ParamVoucherHeader_Save
    {
        public Int64 pID { get; set; }
        public string pCode { get; set; }
        public DateTime pVoucherDate { get; set; }
        public Int32 pSafeID { get; set; }
        public Int32 pCurrencyID { get; set; }
        public decimal pExchangeRate { get; set; }
        public string pChargedPerson { get; set; }
        public string pNotes { get; set; }
        public Int32 pTaxID { get; set; }
        public decimal pTaxValue { get; set; }
        public Int32 pTaxSign { get; set; }
        public Int32 pTaxID2 { get; set; }
        public decimal pTaxValue2 { get; set; }
        public Int32 pTaxSign2 { get; set; }
        public decimal pTotal { get; set; }
        public decimal pTotalAfterTax { get; set; }
        public bool pIsAGInvoice { get; set; }
        public Int32 pAGInvType_ID { get; set; }
        public Int32 pInv_No { get; set; }
        public Int64 pInvoiceID { get; set; }
        public Int64 pJVID1 { get; set; }
        public Int64 pJVID2 { get; set; }
        public Int64 pJVID3 { get; set; }
        public Int64 pJVID4 { get; set; }
        public Int32 pSalesManID { get; set; }
        public Int32 pforwOperationID { get; set; }
        public bool pIsCustomClearance { get; set; }
        public Int32 pTransType_ID { get; set; }
        public Int32 pVoucherType { get; set; }
        public bool pIsCash { get; set; }
        public bool pIsCheque { get; set; }
        public DateTime pPrintDate { get; set; }
        public string pChequeNo { get; set; }
        public DateTime pChequeDate { get; set; }
        public Int32 pBankID { get; set; }
        public Int32 pDisbursementJob_ID { get; set; }
        public Int32 pSL_SalesManID { get; set; }


        public string pOtherSideBankName { get; set; }
        public DateTime pCollectionDate { get; set; }
        public decimal pCollectionExpense { get; set; }
    }
    public class ParamVoucherDetails_Save
    {
        public Int64 pID { get; set; }
        public string pCode { get; set; }
        public DateTime pVoucherDate { get; set; }
        public Int32 pSafeID { get; set; }
        public Int32 pCurrencyID { get; set; }
        public decimal pExchangeRate { get; set; }
        public string pChargedPerson { get; set; }

        public string pNotes { get; set; }
        public Int32 pTaxID { get; set; }
        public decimal pTaxValue { get; set; }
        public Int32 pTaxSign { get; set; }
        public Int32 pTaxID2 { get; set; }
        public decimal pTaxValue2 { get; set; }
        public Int32 pTaxSign2 { get; set; }
        public decimal pTotal { get; set; }
        public decimal pTotalAfterTax { get; set; }
        public bool pIsAGInvoice { get; set; }
        public Int32 pAGInvType_ID { get; set; }
        public Int32 pInv_No { get; set; }
        public Int64 pInvoiceID { get; set; }
        public Int64 pJVID1 { get; set; }
        public Int64 pJVID2 { get; set; }
        public Int64 pJVID3 { get; set; }
        public Int64 pJVID4 { get; set; }
        public Int32 pSalesManID { get; set; }
        public Int32 pforwOperationID { get; set; }
        public bool pIsCustomClearance { get; set; }
        public Int32 pTransType_ID { get; set; }
        public Int32 pVoucherType { get; set; }
        public bool pIsCash { get; set; }
        public bool pIsCheque { get; set; }
        public DateTime pPrintDate { get; set; }
        public string pChequeNo { get; set; }
        public DateTime pChequeDate { get; set; }
        public Int32 pBankID { get; set; }
        public string pOtherSideBankName { get; set; }
        public DateTime pCollectionDate { get; set; }
        public decimal pCollectionExpense { get; set; }
        //Details Data
        public Int64 pDetailsID { get; set; }
        public decimal pValue { get; set; }
        public string pDescription { get; set; }
        public Int32 pAccountID { get; set; }
        public Int32 pSubAccountID { get; set; }
        public Int32 pCostCenterID { get; set; }
        public bool pIsDocumented { get; set; }
        public Int32 pDetailsInvoiceID { get; set; }
        public Int32 pDisbursementJob_ID { get; set; }
        public Int32 pSL_SalesManID { get; set; }

        public Int32 pOperationID { get; set; }
        public Int32 pHouseID { get; set; }
        public Int32 pBranchID { get; set; }
        public Int32 pTruckingOrderID { get; set; }


        

    }


    public class CArray
    {
        public List<CVarA_VoucherInvoicesPayment> LA_VoucherInvoicesPayment { get; set; }
    }
}
