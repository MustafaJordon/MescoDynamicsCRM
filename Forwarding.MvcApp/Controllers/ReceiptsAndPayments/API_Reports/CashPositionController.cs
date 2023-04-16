//using Forwarding.MvcApp.Models.OperAcc.Generated;
using Forwarding.MvcApp.Models.Administration.Settings.Generated;
//using Forwarding.MvcApp.Models.MasterData.BanksAccountsAndTreasuries.Generated;
using Forwarding.MvcApp.Models.MasterData.Partners.Generated;
using Forwarding.MvcApp.Models.NoAccess.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;
using Forwarding.MvcApp.Models.Accounting.Reports.Customized;
using Forwarding.MvcApp.Models.MasterData.CashAndBanks.Generated;
using Shipping.MvcApp.Models.Accounting.Reports.Generated;
using Forwarding.MvcApp.Models.MasterData.BanksAccountsAndTreasuries.Generated;
using Forwarding.MvcApp.Models.ReceiptsAndPayments.Reports.Customized;
using System.Globalization;

namespace Forwarding.MvcApp.Controllers.ReceiptsAndPayments.API_Reports
{
    public class CashPositionController : ApiController
    {
        [HttpGet, HttpPost]
        public object[] FillFilter()
        {
            CvwBranches objCvwBranches = new CvwBranches();
            objCvwBranches.GetList(" WHERE 1=1 ORDER BY Name ");

            CBankAccount CBank = new CBankAccount();
            CBank.GetList(" WHERE 1=1 ORDER BY Name ");



            return new object[] { 
                new JavaScriptSerializer().Serialize(objCvwBranches.lstCVarvwBranches)//data[0]
                , new JavaScriptSerializer().Serialize(CBank.lstCVarBankAccount)//data[1]
               
            };
        }

        [HttpGet, HttpPost]
        public object[] GetPrintedData([FromBody] ParamGetPrintedDataBalance paramGetPrintedDataBalance)
        {
            int _RowCount = 0;
            Exception checkException = null;
            //CDefaults objCDefaults = new CDefaults();
            //objCDefaults.GetList("WHERE 1=1");
            CvwDefaults objCvwRptHeaderDefaultTable = new CvwDefaults();
            objCvwRptHeaderDefaultTable.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount);

            CvwStructure_Rep_A_CashPosition objCvwStructure_Rep_A_CashPosition = new CvwStructure_Rep_A_CashPosition();

            //if (!pIsGroupedByCostCenter)
            checkException = objCvwStructure_Rep_A_CashPosition.GetList(
                 paramGetPrintedDataBalance.pCurrencyID
                ,paramGetPrintedDataBalance.pIsCash
                  , DateTime.ParseExact(paramGetPrintedDataBalance.pDate + " 00:00:00.000", "dd/MM/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture)
                  , paramGetPrintedDataBalance.pHideZeroes
                );

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
            //new JavaScriptSerializer().Serialize(objCDefaults.lstCVarDefaults[0]) //pDefaultsHeader = pData[0]
            new JavaScriptSerializer().Serialize(objCvwRptHeaderDefaultTable.lstCVarvwDefaults[0])//pDefaultsHeader = pData[0]
            ,serializer.Serialize(objCvwStructure_Rep_A_CashPosition.lstCVarvwStructure_Rep_A_CashPosition)
        };
        }

    }
    public class ParamGetPrintedDataBalance
    {

        public int pCurrencyID { get; set; }
        public int pIsCash { get; set; }

        public string pDate { get; set; }

        public bool pHideZeroes { get; set; }

    }
}
