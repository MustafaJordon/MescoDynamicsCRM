using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;
using Forwarding.MvcApp.Models.Warehousing.MasterData.Generated;
using Forwarding.MvcApp.Models.Warehousing.Reports.Generated;
using Forwarding.MvcApp.Models.Warehousing.Transactions.Generated;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebMatrix.WebData;

namespace Forwarding.MvcApp.Controllers.Warehousing.API_Reports
{
    public class InventoryController : ApiController
    {
        [HttpGet, HttpPost]
        public object[] LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned(bool pIsLoadArrayOfObjects, string pLanguage, Int32 pPageNumber, Int32 pPageSize, string pWhereClause, string pOrderBy)
        {
            CWH_Warehouse objCWH_Warehouse = new CWH_Warehouse();
            CvwPurchaseItem objCvwPurchaseItem = new CvwPurchaseItem();
            CvwWH_RowLocation objCvwWH_RowLocation = new CvwWH_RowLocation();
            CvwWH_ReceiveDetails objCvwWH_ReceiveDetails = new CvwWH_ReceiveDetails();
            int _RowCount = 0;
            Exception checkException = null;
            if (pIsLoadArrayOfObjects)
            {
                checkException = objCWH_Warehouse.GetList("ORDER BY Name");
                //checkException = objCvwPurchaseItem.GetList("ORDER BY Code");
                checkException = objCvwWH_ReceiveDetails.GetListPaging(999999, 1, "WHERE Quantity>PickedQuantity AND IsFinalized=1 " + (WebSecurity.CurrentUserName == "BG EGYPT" ? (" AND CustomerName=N'" + WebSecurity.CurrentUserName) + "'" : ""), "ID", out _RowCount); 
                if (objCWH_Warehouse.lstCVarWH_Warehouse.Count > 0)
                    checkException = objCvwWH_RowLocation.GetList("ORDER BY Code");
            }
            var pLocationList = objCvwWH_RowLocation.lstCVarvwWH_RowLocation
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Code = s.Code
                })
                //.Distinct()
                .OrderBy(o => o.Code).ToList();
            var pCustomerList = objCvwWH_ReceiveDetails.lstCVarvwWH_ReceiveDetails
                .Select(s => new
                {
                    ID = s.CustomerID
                    ,
                    Name = s.CustomerName
                })
                .Distinct().OrderBy(o => o.Name).ToList();
            CvwWH_Inventory objCvwWH_Inventory = new CvwWH_Inventory();
            objCvwWH_Inventory.GetListPaging(pPageSize, pPageNumber, pWhereClause + (WebSecurity.CurrentUserName == "BG EGYPT" ? (" AND CustomerName=N'" + WebSecurity.CurrentUserName) + "'" : ""), pOrderBy, out _RowCount);
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                new JavaScriptSerializer().Serialize(objCvwWH_Inventory.lstCVarvwWH_Inventory)
                , _RowCount
                , new JavaScriptSerializer().Serialize(objCWH_Warehouse.lstCVarWH_Warehouse) //pData[2]
                , serializer.Serialize(objCvwPurchaseItem.lstCVarvwPurchaseItem) //pData[3]
                , serializer.Serialize(pLocationList) //pData[4]
                , serializer.Serialize(pCustomerList) //pData[5]
            };
        }

        [HttpGet, HttpPost]
        public object[] GetPrintedData(string pLanguage, Int32 pPageNumber, Int32 pPageSize, string pWhereClause, string pOrderBy, bool pIsShowSerial)
        {
            int _RowCount = 0;
            Exception checkException = null;
            CvwWH_Inventory objCvwWH_Inventory = new CvwWH_Inventory();
            CvwWH_InventoryWithSerial objCvwWH_InventoryWithSerial = new CvwWH_InventoryWithSerial();
            if (pIsShowSerial)
                checkException = objCvwWH_InventoryWithSerial.GetListPaging(pPageSize, pPageNumber, pWhereClause + (WebSecurity.CurrentUserName == "BG EGYPT" ? (" AND CustomerName=N'" + WebSecurity.CurrentUserName) + "'" : ""), pOrderBy, out _RowCount);
            else
                checkException = objCvwWH_Inventory.GetListPaging(pPageSize, pPageNumber, pWhereClause + (WebSecurity.CurrentUserName == "BG EGYPT" ? (" AND CustomerName=N'" + WebSecurity.CurrentUserName) + "'" : ""), pOrderBy, out _RowCount);
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                pIsShowSerial ? serializer.Serialize(objCvwWH_InventoryWithSerial.lstCVarvwWH_InventoryWithSerial) 
                              : serializer.Serialize(objCvwWH_Inventory.lstCVarvwWH_Inventory)
                , _RowCount
            };
        }

    }
}
