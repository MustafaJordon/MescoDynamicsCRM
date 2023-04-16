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
using WebMatrix.WebData;

namespace Forwarding.MvcApp.Controllers.Reports.API_Statistics
{
    public class OperationsStatisticsController : ApiController
    {
        [HttpGet, HttpPost]//pID : is the InvoiceID
        public object[] GetStatisticsFilter()
        {
            int _RowCount = 0;
            CvwUsers objCvwUsers = new CvwUsers();
            objCvwUsers.GetList(" WHERE IsNull(CustomerID , 0) = 0 AND 1=1 ORDER BY Name ");

            CvwBranches objCvwBranches = new CvwBranches();
            objCvwBranches.GetList(" WHERE 1=1 ORDER BY Name ");

            CCustomers objCCustomers = new CCustomers();
            //objCCustomers.GetList(" WHERE 1=1 ORDER BY Name ");

            CvwAgentsForCombo objCvwAgentsForCombo = new CvwAgentsForCombo();
            objCvwAgentsForCombo.GetListPaging(9999, 1, "WHERE 1=1", "Name", out _RowCount);

            CvwCurrencies objCvwCurrencies = new CvwCurrencies();
            objCvwCurrencies.GetList(" WHERE 1=1 ORDER BY Code ");

            CNoAccessQuoteAndOperStages objCNoAccessQuoteAndOperStages = new CNoAccessQuoteAndOperStages();
            objCNoAccessQuoteAndOperStages.GetList(" WHERE IsOperationStage = 1 AND IsInActive = 0 ORDER BY ViewOrder ");

            CShippingLines objCShippingLines = new CShippingLines();
            objCShippingLines.GetList(" WHERE 1=1 ORDER BY Name ");

            CAirlines objCAirlines = new CAirlines();
            objCAirlines.GetList(" WHERE 1=1 ORDER BY Name ");

            CVessels objCVessels = new CVessels();
            objCVessels.GetList(" WHERE 1=1 ORDER BY Name ");

            CNetwork objCNetwork = new CNetwork();
            objCNetwork.GetList(" WHERE 1=1 ORDER BY Name ");

            CCommodities objCCommodities = new CCommodities();
            objCCommodities.GetList("ORDER BY Name");

            CTrackingStage objCTrackingStage = new CTrackingStage();
            objCTrackingStage.GetList("ORDER BY Name");

            CMoveTypes objCMoveTypes = new CMoveTypes();
            objCMoveTypes.GetList(" WHERE 1=1 ORDER BY Name ");

            CCountries objCCountries = new CCountries();
            objCCountries.GetList(" WHERE 1=1 ORDER BY Name ");

            CCustomsClearanceAgents objCCustomsClearanceAgents = new CCustomsClearanceAgents();
            objCCustomsClearanceAgents.GetList(" WHERE 1=1 ORDER BY Name ");



            CCC_ClearanceTypes cCC_ClearanceTypes = new CCC_ClearanceTypes();
            cCC_ClearanceTypes.GetList("WHERE 1=1 ");


            CTruckers cTruckers = new CTruckers();
            cTruckers.GetList(" WHERE 1=1 ORDER BY Name ");
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                new JavaScriptSerializer().Serialize(objCvwUsers.lstCVarvwUsers)//data[0]
                , new JavaScriptSerializer().Serialize(objCvwBranches.lstCVarvwBranches)//data[1]
                , new JavaScriptSerializer().Serialize(objCCustomers.lstCVarCustomers)//data[2]
                , new JavaScriptSerializer().Serialize(objCvwCurrencies.lstCVarvwCurrencies)//data[3]
                , new JavaScriptSerializer().Serialize(objCNoAccessQuoteAndOperStages.lstCVarNoAccessQuoteAndOperStages)//data[4]
                , new JavaScriptSerializer().Serialize(objCShippingLines.lstCVarShippingLines)//data[5]
                , new JavaScriptSerializer().Serialize(objCVessels.lstCVarVessels)//data[6]
                , new JavaScriptSerializer().Serialize(objCTrackingStage.lstCVarTrackingStage)//data[7]
                , new JavaScriptSerializer().Serialize(objCCommodities.lstCVarCommodities)//data[8]
                , serializer.Serialize(objCvwAgentsForCombo.lstCVarvwAgentsForCombo)//data[9]
                , new JavaScriptSerializer().Serialize(objCNetwork.lstCVarNetwork)//data[10]
                , new JavaScriptSerializer().Serialize(objCMoveTypes.lstCVarMoveTypes)//data[11]
                , new JavaScriptSerializer().Serialize(objCCountries.lstCVarCountries)//data[12]
                , serializer.Serialize(objCCustomsClearanceAgents.lstCVarCustomsClearanceAgents)//data[13]
                ,serializer.Serialize(cTruckers.lstCVarTruckers)//data[14]
                 ,serializer.Serialize(cCC_ClearanceTypes.lstCVarCC_ClearanceTypes)//data[15]
                 ,serializer.Serialize(objCAirlines.lstCVarAirlines)//data[16]
            };
        }

        [HttpGet, HttpPost]
        public object[] LoadData(string pWhereClause, bool pIsFilterInvoices, string pFromInvoiceDate, string pToInvoiceDate, bool pIsWithCurrenciesDetailed)
        {
            bool pRecordsExist = false;
            Exception checkException = null;
            int _RowCount = 0;
            var constOperationsFormID = 29;//OperationsManagement
            //var constQuotationsFormID = 28; //QuotationsManagement

            CvwUserForms objCvwUserForms = new CvwUserForms();
            objCvwUserForms.GetList("WHERE UserID=" + WebSecurity.CurrentUserId.ToString() + " AND FormID=" + constOperationsFormID.ToString());

            bool _IsHideOthersRecords = objCvwUserForms.lstCVarvwUserForms[0].HideOthersRecords;

            #region RoleUsersShareRecords
            bool _IsUsersShareRecords = objCvwUserForms.lstCVarvwUserForms[0].IsUsersShareRecords;
            int _RoleID = objCvwUserForms.lstCVarvwUserForms[0].RoleID;
            if (_IsUsersShareRecords)
                pWhereClause += " AND (CreatorRoleID=" + _RoleID + " OR SalesmanRoleID=" + _RoleID + " OR OperationManRoleID=" + _RoleID + ")";
            else if (_IsHideOthersRecords)
                pWhereClause += " AND (CreatorUserID=" + WebSecurity.CurrentUserId + " OR SalesmanID=" + WebSecurity.CurrentUserId + " OR OperationManID=" + WebSecurity.CurrentUserId + ")";
            #endregion RoleUsersShareRecords

            CvwOperationsStatistics objCvwOperationsStatistics = new CvwOperationsStatistics();

            if (pIsFilterInvoices)
                checkException = objCvwOperationsStatistics.GetListPaging_FilterInvoices(99999, 1, pWhereClause, "ID", pFromInvoiceDate, pToInvoiceDate, out _RowCount);
            else
                checkException = objCvwOperationsStatistics.GetListPaging(99999, 1, pWhereClause, "ID", out _RowCount);

            #region Get Container Totals
            string strOperationIDs = "0";
            var _OperationIDList = objCvwOperationsStatistics.lstCVarvwOperationsStatistics.Select(s => new
            {
                OperationID = s.ID
            }).Distinct();
            int _NumberOfOperations = _OperationIDList.Count();
            for (int i = 0; i < _NumberOfOperations; i++)
                strOperationIDs += "," + _OperationIDList.ElementAt(i).OperationID;
            CvwOperationContainersAndPackages objCvwOperationContainersAndPackages = new CvwOperationContainersAndPackages();
            checkException = objCvwOperationContainersAndPackages.GetListPaging(999999, 1, "WHERE ContainerTypeID IS NOT NULL AND (OperationID IN (" + strOperationIDs + ") OR MasterOperationID IN (" + strOperationIDs + "))", "ID", out _RowCount);
            var pContainerTotals = objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages
                .GroupBy(g => new { g.ContainerTypeID, g.ContainerTypeCode })
                .Select(g => new
                {
                    Quantity = g.Sum(s => (s.Quantity == 0 ? 1 : s.Quantity))
                    ,
                    ContainerTypeCode = g.First().ContainerTypeCode
                });
            string strContainerTotals = "";
            for (int i = 0; i < pContainerTotals.Count(); i++)
                strContainerTotals += (strContainerTotals == "" ? "" : ", ") + pContainerTotals.ElementAt(i).Quantity + "x" + pContainerTotals.ElementAt(i).ContainerTypeCode;
            #endregion Get Container Totals
            #region Get Incoterm Totals
            var pIncotermTotals = objCvwOperationsStatistics.lstCVarvwOperationsStatistics
                .GroupBy(g => new { g.IncotermID })
                .Select(g => new
                {
                    IncotermName = g.First().IncotermName
                    ,
                    Count = g.Count()
                }).OrderBy(o => o.IncotermName);
            string strIncotermTotals = "";
            for (int i = 0; i < pIncotermTotals.Count(); i++)
                strIncotermTotals += (strIncotermTotals == "" ? "" : ", ") + pIncotermTotals.ElementAt(i).Count + "x" + (pIncotermTotals.ElementAt(i).IncotermName == "0" ? "UnSpecified" : pIncotermTotals.ElementAt(i).IncotermName);
            #endregion Get Incoterm Totals

            #region vwOperationsStatisticsDetailedWithCurrencies
            CvwOperationsStatisticsDetailedWithCurrencies objCvwOperationsStatisticsDetailedWithCurrencies = new CvwOperationsStatisticsDetailedWithCurrencies();
            if (pIsWithCurrenciesDetailed)
            {
                objCvwOperationsStatisticsDetailedWithCurrencies.GetListPaging(999999, 1, "WHERE ID IN (" + strOperationIDs + ")", "ID", out _RowCount);
            }
            #endregion vwOperationsStatisticsDetailedWithCurrencies

            if (objCvwOperationsStatistics.lstCVarvwOperationsStatistics.Count > 0 && checkException == null)
                pRecordsExist = true;

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[]
            {
                pRecordsExist //data[0]
                , serializer.Serialize(objCvwOperationsStatistics.lstCVarvwOperationsStatistics) //data[1]
                , strContainerTotals //data[2]
                , strIncotermTotals //data[3]
                , pIsWithCurrenciesDetailed ? serializer.Serialize(objCvwOperationsStatisticsDetailedWithCurrencies.lstCVarvwOperationsStatisticsDetailedWithCurrencies) : "CurrenciesNotSelected" //data[4]
            };
        }
    }
}
