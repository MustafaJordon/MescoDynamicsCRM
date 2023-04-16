using System;
using Forwarding.MvcApp.Models.Accounting.MasterData.Generated;
using Forwarding.MvcApp.Models.Accounting.Reports.Customized;
using Forwarding.MvcApp.Models.Administration.Settings.Generated;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;
using Forwarding.MvcApp.Models.Accounting.Transactions.Generated;

namespace Forwarding.MvcApp.Controllers.Accounting.API_Reports
{
    public class AccountLedgerByCurrencyController : ApiController
    {

        [HttpGet, HttpPost]
        public object[] FillSearchControls()
        {
            int _RowCount = 0;
 
            CCurrencies objCCurrency = new CCurrencies();
            objCCurrency.GetListPaging(9999, 1, "WHERE 1=1", "Code", out _RowCount);

            CvwA_Accounts objCvwA_Accounts = new CvwA_Accounts();
            objCvwA_Accounts.GetListPaging(9999, 1, "WHERE IsMain=0", "Name, Code", out _RowCount);

            CA_JournalTypes objCA_JournalTypes = new CA_JournalTypes();
            objCA_JournalTypes.GetListPaging(9999, 1, "WHERE 1=1", "Name", out _RowCount);

            CvwA_Branches objCvwA_Branches = new CvwA_Branches();
            CvwA_CostCenters objCvwA_CostCenters = new CvwA_CostCenters();

            objCvwA_Branches.GetListPaging(9999, 1, "WHERE 1=1", "Name, Code", out _RowCount);
            objCvwA_CostCenters.GetListPaging(9999, 1, "WHERE IsMain=0", "Name", out _RowCount);

            return new object[] {   new JavaScriptSerializer().Serialize(objCCurrency.lstCVarCurrencies)
                , new JavaScriptSerializer().Serialize(objCvwA_Accounts.lstCVarvwA_Accounts)
                , new JavaScriptSerializer().Serialize(objCA_JournalTypes.lstCVarA_JournalTypes)
                , new JavaScriptSerializer().Serialize(objCvwA_Branches.lstCVarvwA_Branches)
                , new JavaScriptSerializer().Serialize(objCvwA_CostCenters.lstCVarvwA_CostCenters)

            };
        }


       // pPostStatus: 0:All 1:Posted 2:Unposted
        [HttpGet, HttpPost]
        public object[] GetPrintedData(string pAccountIDList, string pCostCenterIDList, string pJournalTypeIDList
            , string pFromDate, string pToDate, int pPostStatus, int pCurr,string pBranche_IDs)
        {
            int _RowCount = 0;
            Exception checkException = null;
            //CDefaults objCDefaults = new CDefaults();
            //objCDefaults.GetList("WHERE 1=1");

            CvwDefaults objCvwRptHeaderDefaultTable = new CvwDefaults();
            objCvwRptHeaderDefaultTable.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount);
            pJournalTypeIDList = pJournalTypeIDList == "-1" ? "-1" : ("," + pJournalTypeIDList + ",");
            CvwStructure_SP_A_Ledger_Rep_ForCurr objCvwStructure_SP_A_Ledger_Rep_ForCurr = new CvwStructure_SP_A_Ledger_Rep_ForCurr();
            CvwStructure_SP_A_Ledger_Rep_ForCurr_CostCenter objCvwStructure_SP_A_Ledger_Rep_ForCurr_CostCenter = new CvwStructure_SP_A_Ledger_Rep_ForCurr_CostCenter();
            //CvwStructure_Rep_A_Ledger_Rep_E objCvwStructure_Rep_A_Ledger_Rep_E_CombinedByCostCenter = new CvwStructure_Rep_A_Ledger_Rep_E();
            if (pCostCenterIDList == "0")
            {
                checkException = objCvwStructure_SP_A_Ledger_Rep_ForCurr.GetList(
                    "-1" //pJV_IDs
                    , "," + pAccountIDList + "," //pAccount_IDs
                    , DateTime.ParseExact(pFromDate + " 00:00:00.000", "dd/MM/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture)
                    , DateTime.ParseExact(pToDate + " 23:59:59.000", "dd/MM/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture)
                    , -1 //pPosted
                    , false //pShowRevaluateEntry
                    , -1 //IsDoc
                    , pJournalTypeIDList
                    , pCurr
                    , "," + pCostCenterIDList + ","
                    , "," + pBranche_IDs + ","
        );
            }
            else
            {
                checkException = objCvwStructure_SP_A_Ledger_Rep_ForCurr_CostCenter.GetList(
                            "-1" //pJV_IDs
                            , "," + pAccountIDList + "," //pAccount_IDs
                            , DateTime.ParseExact(pFromDate + " 00:00:00.000", "dd/MM/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture)
                            , DateTime.ParseExact(pToDate + " 23:59:59.000", "dd/MM/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture)
                            , -1 //pPosted
                            , false //pShowRevaluateEntry
                            , -1 //IsDoc
                            , pJournalTypeIDList
                            , pCurr
                            , "," + pCostCenterIDList + ","
                            , "," + pBranche_IDs + ","
                        );
            }

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                //new JavaScriptSerializer().Serialize(objCDefaults.lstCVarDefaults[0]) //pDefaultsHeader = pData[0]
                new JavaScriptSerializer().Serialize(objCvwRptHeaderDefaultTable.lstCVarvwDefaults[0]) //pDefaultsHeader = pData[0]
                , pCostCenterIDList == "0" ?     serializer.Serialize(objCvwStructure_SP_A_Ledger_Rep_ForCurr.lstCVarvwStructure_SP_A_Ledger_Rep_ForCurr)
                : serializer.Serialize(objCvwStructure_SP_A_Ledger_Rep_ForCurr_CostCenter.lstCVarvwStructure_SP_A_Ledger_Rep_ForCurr_CostCenter) //pAccountLedger = pData[1]
            };
        }
    }
}
