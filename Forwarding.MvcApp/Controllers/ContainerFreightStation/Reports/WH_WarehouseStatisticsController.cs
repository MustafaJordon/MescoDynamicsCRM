using Forwarding.MvcApp.Models.Administration.Security.Generated;
using Forwarding.MvcApp.Models.Administration.Settings.Generated;
using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;
using Forwarding.MvcApp.Models.MasterData.Locations.Generated;
using Forwarding.MvcApp.Models.MasterData.Others.Generated;
using Forwarding.MvcApp.Models.MasterData.Partners.Generated;
using Forwarding.MvcApp.Models.NoAccess.Generated;
using Forwarding.MvcApp.Models.Operations.Operations.Customized;
using System;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Linq;
using Forwarding.MvcApp.Models.Operations.Operations.Generated;
using Forwarding.MvcApp.Models.Warehousing.MasterData.Generated;
using Forwarding.MvcApp.Models.ContainerFreightStation.Reports;

namespace Forwarding.MvcApp.Controllers.ContainerFreightStation.Reports
{
    public class WH_WarehouseStatisticsController : ApiController
    {
        [HttpGet, HttpPost]//pID : is the InvoiceID
        public object[] FillComboBoxs()
        {
            int _RowCount = 0;
            CvwUsers objCvwUsers = new CvwUsers();
            objCvwUsers.GetList(" WHERE IsNull(CustomerID , 0) = 0 AND 1=1 ORDER BY Name ");


            CShippingLines objCShippingLines = new CShippingLines();
            objCShippingLines.GetList(" WHERE 1=1 ORDER BY Name ");

            CVessels objCVessels = new CVessels();
            objCVessels.GetList(" WHERE 1=1 ORDER BY Name ");

            CCommodities objCCommodities = new CCommodities();
            objCCommodities.GetList("ORDER BY Name");

            CContainerTypes objCContainerTypes = new CContainerTypes();
            objCContainerTypes.GetList(" where 1=1 ");

            var pContainerTypesList = objCContainerTypes.lstCVarContainerTypes
                .Select(ss => new
                {
                    ID = ss.ID,
                    Name = ss.Name
                })
                .Distinct().OrderBy(o => o.Name).ToList();

            CWH_Warehouse objCWH_Warehouse = new CWH_Warehouse();
            objCWH_Warehouse.GetList(" where 1=1 ");
            var pWarehouseList = objCWH_Warehouse.lstCVarWH_Warehouse
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                })
                .Distinct().OrderBy(o => o.Name).ToList();

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                new JavaScriptSerializer().Serialize(objCvwUsers.lstCVarvwUsers)//data[0]
                , new JavaScriptSerializer().Serialize(pContainerTypesList)//data[1]
                , new JavaScriptSerializer().Serialize(pWarehouseList) // data[2]
                , new JavaScriptSerializer().Serialize(objCShippingLines.lstCVarShippingLines)//data[3]
                , new JavaScriptSerializer().Serialize(objCVessels.lstCVarVessels)//data[4]
                , new JavaScriptSerializer().Serialize(objCCommodities.lstCVarCommodities)//data[5]

            };
        }


        [HttpGet, HttpPost]
        public object[] LoadData(string pWhereClause)
        {
            bool pRecordsExist = false;
            Exception checkException = null;
            int _RowCount = 0;

            CvwWH_CFS_WarehouseStatistics objCvwWH_CFS_WarehouseStatistics = new CvwWH_CFS_WarehouseStatistics();
            checkException = objCvwWH_CFS_WarehouseStatistics.GetListPaging(99999, 1, pWhereClause, "OperationID", out _RowCount);

            #region Get Container Totals
            //string strOperationIDs = "0";
            //var _OperationIDList = objCvwOperationsStatistics.lstCVarvwOperationsStatistics.Select(s => new
            //{
            //    OperationID = s.ID
            //}).Distinct();
            //int _NumberOfOperations = _OperationIDList.Count();
            //for (int i = 0; i < _NumberOfOperations; i++)
            //    strOperationIDs += "," + _OperationIDList.ElementAt(i).OperationID;
            //CvwOperationContainersAndPackages objCvwOperationContainersAndPackages = new CvwOperationContainersAndPackages();
            //checkException = objCvwOperationContainersAndPackages.GetListPaging(999999, 1, "WHERE ContainerTypeID IS NOT NULL AND (OperationID IN (" + strOperationIDs + ") OR MasterOperationID IN (" + strOperationIDs + "))", "ID", out _RowCount);
            //var pContainerTotals = objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages
            //    .GroupBy(g => new { g.ContainerTypeID, g.ContainerTypeCode })
            //    .Select(g => new
            //    {
            //        Quantity = g.Sum(s => (s.Quantity == 0 ? 1: s.Quantity))
            //        ,
            //        ContainerTypeCode = g.First().ContainerTypeCode
            //    });
            //string strContainerTotals = "";
            //for (int i = 0; i < pContainerTotals.Count(); i++)
            //    strContainerTotals += (strContainerTotals == "" ? "" : ", ") + pContainerTotals.ElementAt(i).Quantity + "x" + pContainerTotals.ElementAt(i).ContainerTypeCode;
            #endregion Get Container Totals



            if (objCvwWH_CFS_WarehouseStatistics.lstCVarvwWH_CFS_WarehouseStatistics.Count > 0 && checkException == null)
                pRecordsExist = true;

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[]
            {
                pRecordsExist //data[0]
                , serializer.Serialize(objCvwWH_CFS_WarehouseStatistics.lstCVarvwWH_CFS_WarehouseStatistics) //data[1]
                //, strContainerTotals //data[2]
            };
        }
    }
}
