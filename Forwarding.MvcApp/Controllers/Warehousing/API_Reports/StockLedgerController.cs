using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;
using Forwarding.MvcApp.Models.MasterData.Partners.Generated;
using Forwarding.MvcApp.Models.Warehousing.Reports.Customized;
using Forwarding.MvcApp.Models.Warehousing.Reports.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Forwarding.MvcApp.Controllers.Warehousing.API_Reports
{
    public class StockLedgerController : ApiController
    {
        [HttpGet, HttpPost]//pID : is the InvoiceID
        public object[] GetStatisticsFilter()
        {
            int _RowCount = 0;
            Exception checkException = null;
            CPurchaseItem objCPurchaseItem = new CPurchaseItem();
            checkException = objCPurchaseItem.GetListPaging(999999, 1, "WHERE 1=1", "Code", out _RowCount);
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                serializer.Serialize(objCPurchaseItem.lstCVarPurchaseItem)
            };
        }
        [HttpGet, HttpPost]
        public object[] LoadData(DateTime pFrom, DateTime pTo, int pCustomerID, int pPurchaseItemID)
        {
            CRepvwWH_ProductLog objCRepvwWH_ProductLog = new CRepvwWH_ProductLog();
            objCRepvwWH_ProductLog.GetList(pFrom, pTo, pCustomerID);
            if (pPurchaseItemID > 0)
            {
                objCRepvwWH_ProductLog.lstCVarRepvwWH_ProductLog = objCRepvwWH_ProductLog.lstCVarRepvwWH_ProductLog.Where(x => x.PurchaseItemID == pPurchaseItemID).ToList();
            }
            CRepvwWH_ProductLogDetails objCRepvwWH_ProductLogDetails = new CRepvwWH_ProductLogDetails();
            objCRepvwWH_ProductLogDetails.GetList(pFrom, pTo, pCustomerID);
            if (pPurchaseItemID > 0)
            {
                objCRepvwWH_ProductLogDetails.lstCVarRepvwWH_ProductLogDetails = objCRepvwWH_ProductLogDetails.lstCVarRepvwWH_ProductLogDetails.Where(x => x.PurchaseItemID == pPurchaseItemID).ToList();
            }
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[]
            {
                 serializer.Serialize(objCRepvwWH_ProductLog.lstCVarRepvwWH_ProductLog)
                ,serializer.Serialize(objCRepvwWH_ProductLogDetails.lstCVarRepvwWH_ProductLogDetails)
            };
        }

    }
}
