using Forwarding.MvcApp.Models.Administration.Security.Generated;
using Forwarding.MvcApp.Models.MasterData.Others.Generated;
using Forwarding.MvcApp.Models.MasterData.Partners.Generated;
using Forwarding.MvcApp.Models.ReceiptsAndPayments.Custodies.Generated;
using Forwarding.MvcApp.Models.Operations.Operations.Generated;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebMatrix.WebData;
using System.IO;
using Forwarding.MvcApp.Models.NoAccess.Generated;
using Forwarding.MvcApp.Models.Administration.Settings.Generated;
using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;
using Forwarding.MvcApp.Models.MasterData.Locations.Generated;
using System.Globalization;
using Forwarding.MvcApp.Models.MasterData.BanksAccountsAndTreasuries.Generated;
using System.Net.Mail;
using Forwarding.BLL.Utilities;
using Forwarding.MvcApp.Models.Accounting.MasterData.Generated;
using Forwarding.MvcApp.Models.Customized;
using Forwarding.MvcApp.Models.CustomizedSPCall;
using Forwarding.MvcApp.Entities.Operations;
using Forwarding.MvcApp.Common;
using Forwarding.MvcApp.Models.XML;

namespace Forwarding.MvcApp.Controllers.Operations.API_Operations
{
    public class OperationsController : ApiController
    {
        [HttpGet, HttpPost]
        public Object[] LoadAll(string pWhereClause)
        {
            // Creates an instance of your JavaScriptSerializer & Setting the MaxJsonLength to handle the case of large data returned
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            int _RowCount = 0;
            CvwOperationsWithMinimalColumns objCvwOperations = new CvwOperationsWithMinimalColumns();
            objCvwOperations.GetListPaging(99999, 1, pWhereClause, "ID DESC", out _RowCount);
            return new Object[] { serializer.Serialize(objCvwOperations.lstCVarvwOperationsWithMinimalColumns), _RowCount };
        }

        [HttpGet, HttpPost]
        public Object[] LoadWithParameters(Int32 pPageNumber, Int32 pPageSize, string pWhereClause, string pOrderBy)
        {
            // Creates an instance of your JavaScriptSerializer & Setting the MaxJsonLength to handle the case of large data returned
            int _RowCount = 0;
            CvwOperations objCvwOperations = new CvwOperations();
            objCvwOperations.GetListPaging(pPageSize, pPageNumber, pWhereClause, pOrderBy, out _RowCount);
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] {
                serializer.Serialize(objCvwOperations.lstCVarvwOperations)
                , _RowCount
            };
        }

        [HttpGet, HttpPost]
        public Object[] LoadCargoProperties(Int64 pOperationIDForCargo, Int64 pMasterOperationID, string pOrderBy)
        {
            // Creates an instance of your JavaScriptSerializer & Setting the MaxJsonLength to handle the case of large data returned
            int _RowCount = 0;
            COperations objCOperations = new COperations();
            objCOperations.GetListPaging(999999, 1, "WHERE ID=" + pOperationIDForCargo, pOrderBy, out _RowCount);
            CvwOperationContainersAndPackages objCvwOperationContainersAndPackages = new CvwOperationContainersAndPackages();
            if (objCOperations.lstCVarOperations[0].MasterOperationID > 0)
                objCvwOperationContainersAndPackages.GetListPaging(999999, 1, "WHERE ContainerTypeID IS NOT NULL AND OperationID=" + pMasterOperationID, "ContainerNumber", out _RowCount);
            CPackageTypes objCPackageTypes = new CPackageTypes();
            objCPackageTypes.GetList("ORDER BY Name");
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] {
                serializer.Serialize(objCOperations.lstCVarOperations[0]) //pData[0]
                , serializer.Serialize(objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages) //pData[1]
                , serializer.Serialize(objCPackageTypes.lstCVarPackageTypes) //pData[2]
            };
        }

        [HttpGet, HttpPost]
        public Object[] LoadAllForCombo(Int32 pPageSize, string pWhereClauseForCombo, string pOrderBy)
        {
            // Creates an instance of your JavaScriptSerializer & Setting the MaxJsonLength to handle the case of large data returned
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            int _RowCount = 0;

            var constOperationsFormID = 29;//OperationsManagement
            //var constQuotationsFormID = 28;//QuotationsManagement
            CvwUserForms objCvwUserForms = new CvwUserForms();
            objCvwUserForms.GetList("WHERE UserID=" + WebSecurity.CurrentUserId.ToString() + " AND FormID=" + constOperationsFormID.ToString());
            bool _IsHideOthersRecords = objCvwUserForms.lstCVarvwUserForms[0].HideOthersRecords;
            if (_IsHideOthersRecords)
                pWhereClauseForCombo += " AND CreatorUserID=" + WebSecurity.CurrentUserId;
            //pWhereClauseForCombo += " AND (CreatorUserID=" + WebSecurity.CurrentUserId + " OR SalesmanID=" + WebSecurity.CurrentUserId + ")";

            CvwOperationsForCombo objCvwOperations = new CvwOperationsForCombo();
            objCvwOperations.GetListPaging(pPageSize, 1, pWhereClauseForCombo, pOrderBy, out _RowCount);
            return new Object[] { serializer.Serialize(objCvwOperations.lstCVarvwOperationsForCombo), _RowCount };
        }

        [HttpGet, HttpPost]
        public Object[] LoadOperationsToRestoreInvoices(Int32 pPageSize, string pWhereClauseToGetOperationsToRestoreInvoices, string pOrderBy)
        {
            // Creates an instance of your JavaScriptSerializer & Setting the MaxJsonLength to handle the case of large data returned
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            int _RowCount = 0;

            //var constOperationsFormID = 29;//OperationsManagement
            ////var constQuotationsFormID = 28;//QuotationsManagement
            //CUserForms objCUserForms = new CUserForms();
            //objCUserForms.GetList("WHERE UserID=" + WebSecurity.CurrentUserId.ToString() + " AND FormID=" + constOperationsFormID.ToString());
            //bool _IsHideOthersRecords = objCUserForms.lstCVarUserForms[0].HideOthersRecords;
            //if (_IsHideOthersRecords)
            //    pWhereClauseToGetOperationsToRestoreInvoices += " AND CreatorUserID=" + WebSecurity.CurrentUserId;

            CvwOperationsToRestoreInvoices objCvwOperations = new CvwOperationsToRestoreInvoices();
            objCvwOperations.GetListPaging(pPageSize, 1, pWhereClauseToGetOperationsToRestoreInvoices, pOrderBy, out _RowCount);
            return new Object[] { serializer.Serialize(objCvwOperations.lstCVarvwOperationsToRestoreInvoices), _RowCount };
        }

        [HttpGet, HttpPost] //called with operations management page
        public object[] LoadWithWhereClause(bool pIsLoadArrayOfObjects, Int32 pPageNumber, Int32 pPageSize
            , string pWhereClause, bool pIsBindTableRows, string pWhereClause_Routings, string pOrderBy)
        {
            var constOperationsFormID = 29;//OperationsManagement
            //var constQuotationsFormID = 28;//QuotationsManagement
            Int32 _RowCount = 0;
            Exception checkException = null;
            string pOperationIDs = "";
            #region Get OperationDs from Routings to improve performance
            CRoutings objCRoutings = new CRoutings();
            if (pWhereClause_Routings != "0")
                checkException = objCRoutings.GetListPaging(999999, 1, pWhereClause_Routings, "ID", out _RowCount);
            if (objCRoutings.lstCVarRoutings.Count > 0)
                for (int i = 0; i < objCRoutings.lstCVarRoutings.Count; i++)
                    pOperationIDs += (pOperationIDs == "" ? objCRoutings.lstCVarRoutings[i].OperationID.ToString() : ("," + objCRoutings.lstCVarRoutings[0].OperationID.ToString()));
            if (pOperationIDs != "")
                pWhereClause += " AND ID IN (" + pOperationIDs + ")";
            #endregion Get OperationDs from Routings to improve performance

            CvwUserForms objCvwUserForms = new CvwUserForms();
            objCvwUserForms.GetList("WHERE UserID=" + WebSecurity.CurrentUserId.ToString() + " AND FormID=" + constOperationsFormID.ToString());
            bool _IsHideOthersRecords = objCvwUserForms.lstCVarvwUserForms[0].HideOthersRecords;

            #region RoleUsersShareRecords
            bool _IsUsersShareRecords = objCvwUserForms.lstCVarvwUserForms[0].IsUsersShareRecords;
            int _RoleID = objCvwUserForms.lstCVarvwUserForms[0].RoleID;
            if (_IsUsersShareRecords)
                pWhereClause += " AND (CreatorRoleID=" + _RoleID + " OR SalesmanRoleID=" + _RoleID + " OR OperationManRoleID=" + _RoleID + ")";
            else if (_IsHideOthersRecords)
                pWhereClause += " AND (CreatorUserID=" + WebSecurity.CurrentUserId + " OR SalesmanID=" + WebSecurity.CurrentUserId + ")";
            #endregion RoleUsersShareRecords

            CvwOperations objCvwOperations = new CvwOperations();
            CvwOperationsForBindTableRows objCvwOperationsForBindTableRows = new CvwOperationsForBindTableRows();
            //if (pIsBindTableRows)
            //    checkException = objCvwOperationsForBindTableRows.GetListPaging(pPageSize, pPageNumber, pWhereClause, (pOrderBy == null ? "OpenYear DESC, CodeSerial DESC " : pOrderBy), out _RowCount);
            //else
            //    checkException = objCvwOperations.GetListPaging(pPageSize, pPageNumber, pWhereClause, (pOrderBy == null ? "OpenYear DESC, CodeSerial DESC " : pOrderBy), out _RowCount);
            if (pIsBindTableRows)
                checkException = objCvwOperationsForBindTableRows.GetListPaging(pPageSize, pPageNumber, pWhereClause, (pOrderBy == null ? "LEFT(Code, 3) DESC, CodeSerial DESC " : pOrderBy), out _RowCount);
            else
                checkException = objCvwOperations.GetListPaging(pPageSize, pPageNumber, pWhereClause, (pOrderBy == null ? "LEFT(Code, 3) DESC, CodeSerial DESC " : pOrderBy), out _RowCount);


            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] {
                pIsBindTableRows ?
                    new JavaScriptSerializer().Serialize(objCvwOperationsForBindTableRows.lstCVarvwOperationsForBindTableRows)
                    : new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations)
                , _RowCount
                , null //////pIsLoadArrayOfObjects ? serializer.Serialize(objCvwCustomers.lstCVarvwCustomersWithMinimalColumns) : "" //pData[2]
                , null //pIsLoadArrayOfObjects ? new JavaScriptSerializer().Serialize(objCAgents.lstCVarvwAgentsForCombo) : null //pData[3]
                , null //pIsLoadArrayOfObjects ? new JavaScriptSerializer().Serialize(pVesselList) : "" //pData[4]
                , null //pIsLoadArrayOfObjects ? new JavaScriptSerializer().Serialize(pContainerTypeList) : "" //pData[5]
                , null //pIsLoadArrayOfObjects ? new JavaScriptSerializer().Serialize(pCountryList) : "" //pData[6]
                , null //pIsLoadArrayOfObjects ? new JavaScriptSerializer().Serialize(pMoveTypeList) : "" //pData[7]
                , null //pIsLoadArrayOfObjects ? new JavaScriptSerializer().Serialize(pShippingLineList) : "" //pData[8]
                , null //pIsLoadArrayOfObjects ? new JavaScriptSerializer().Serialize(pTruckerList) : "" //pData[9]
                , null //pIsLoadArrayOfObjects ? new JavaScriptSerializer().Serialize(pUserList) : "" //pData[10]
                , null //pIsLoadArrayOfObjects ? new JavaScriptSerializer().Serialize(objCCommodities.lstCVarCommodities) : "" //pData[11]
                , null //pIsLoadArrayOfObjects ? new JavaScriptSerializer().Serialize(objCTypeOfStock.lstCVarTypeOfStock) : "" //pData[12]
                , null //pIsLoadArrayOfObjects ? new JavaScriptSerializer().Serialize(pCustomsClearanceAgentList) : "" //pData[13]
                , null //pIsLoadArrayOfObjects ? new JavaScriptSerializer().Serialize(objCAirlines.lstCVarvwAirlinesWithMinimalColumns) : "" //pData[14]
            };
        }


        [HttpGet, HttpPost]
        public object[] LoadFilters(string pDummyParameter)
        {
            int _RowCount = 0;
            CDefaults objCDefaults = new CDefaults();
            objCDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa
            string _UnEditableCompanyName = objCDefaults.lstCVarDefaults[0].UnEditableCompanyName;
            CvwAgentsForCombo objCAgents = new CvwAgentsForCombo();
            CVessels objCVessels = new CVessels();
            CMoveTypes objCMoveTypes = new CMoveTypes();
            CContainerTypes objCContainerTypes = new CContainerTypes();
            CCountries objCCountries = new CCountries();
            CShippingLines objCShippingLines = new CShippingLines();
            CvwAirlinesWithMinimalColumns objCAirlines = new CvwAirlinesWithMinimalColumns();
            CTruckers objCTruckers = new CTruckers();
            CUsers objCUsers = new CUsers();
            CCustomsClearanceAgents objCCustomsClearanceAgents = new CCustomsClearanceAgents();
            CCommodities objCCommodities = new CCommodities();
            CTypeOfStock objCTypeOfStock = new CTypeOfStock();

            if (_UnEditableCompanyName != "EGL")
                objCAgents.GetList("ORDER BY Name");
            objCVessels.GetList("ORDER BY Name");
            objCMoveTypes.GetList("WHERE 1=1 ORDER BY Name");
            objCContainerTypes.GetList("ORDER BY Code");
            objCCountries.GetList("ORDER BY Name");
            objCShippingLines.GetList("ORDER BY Name");
            objCAirlines.GetList("ORDER BY Name");
            objCTruckers.GetList("ORDER BY Name");


            var CurrentUserID = WebSecurity.CurrentUserId;
            if (objCDefaults.lstCVarDefaults[0].ShowUserSalesmen == true)
                objCUsers.GetList(" Where IsNull(CustomerID , 0) = 0 AND IsNull( (SELECT COUNT(us.ID) FROM dbo.UserSalesmen AS us WHERE us.UserID = " + CurrentUserID + " AND us.SalesManID = dbo.Users.ID  ) , 0 ) > 0 ORDER BY Name");
            else
                objCUsers.GetList(" where IsNull(CustomerID , 0) = 0 AND 1 = 1 ORDER BY Name");


            objCCustomsClearanceAgents.GetList(" WHERE 1=1 ORDER BY Name ");
            objCCommodities.GetList(" WHERE 1=1 ORDER BY Name ");
            objCTypeOfStock.GetList(" WHERE 1=1 ORDER BY Name ");
            #region Get Lists with minimal columns
            var pVesselList = objCVessels.lstCVarVessels
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                }).ToList();
            var pMoveTypeList = objCMoveTypes.lstCVarMoveTypes
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                }).ToList();
            var pContainerTypeList = objCContainerTypes.lstCVarContainerTypes
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Code = s.Code
                }).ToList();
            var pCountryList = objCCountries.lstCVarCountries
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                }).ToList();
            var pShippingLineList = objCShippingLines.lstCVarShippingLines
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                }).ToList();
            var pTruckerList = objCTruckers.lstCVarTruckers
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                }).ToList();
            var pUserList = objCUsers.lstCVarUsers
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                    ,
                    IsSalesman = s.IsSalesman
                }).ToList();
            var pCustomsClearanceAgentList = objCCustomsClearanceAgents.lstCVarCustomsClearanceAgents
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                }).ToList();
            #endregion Get Lists with minimal columns
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                serializer.Serialize(objCAgents.lstCVarvwAgentsForCombo) //pData[0]
                , new JavaScriptSerializer().Serialize(pVesselList) //pData[1]
                , new JavaScriptSerializer().Serialize(pContainerTypeList) //pData[2]
                , new JavaScriptSerializer().Serialize(pCountryList) //pData[3]
                , new JavaScriptSerializer().Serialize(pMoveTypeList) //pData[4]
                , new JavaScriptSerializer().Serialize(pShippingLineList) //pData[5]
                , new JavaScriptSerializer().Serialize(pTruckerList) //pData[6]
                , new JavaScriptSerializer().Serialize(pUserList) //pData[7]
                , new JavaScriptSerializer().Serialize(objCCommodities.lstCVarCommodities) //pData[8]
                , new JavaScriptSerializer().Serialize(objCTypeOfStock.lstCVarTypeOfStock) //pData[9]
                , new JavaScriptSerializer().Serialize(pCustomsClearanceAgentList) //pData[10]
                , new JavaScriptSerializer().Serialize(objCAirlines.lstCVarvwAirlinesWithMinimalColumns) //pData[11]
            };
        }

        [HttpGet, HttpPost] //called with operation edit page and load all its details
        public object[] LoadOperationWithDetails(Int32 pPageNumber, Int32 pPageSize, string pWhereClause1, Int64 pOperationID, int pOperationFormID)
        {
            int CancelledQuoteAndOperStageID = 110;
            //int ClosedQuoteAndOperStageID = 120;
            CvwOperations objCvwOperations = new CvwOperations();
            Int32 _RowCount = objCvwOperations.lstCVarvwOperations.Count;
            objCvwOperations.GetListPaging(pPageSize, pPageNumber, pWhereClause1, " ID DESC ", out _RowCount);

            #region Data for select Lists
            CNoAccessQuoteAndOperStages objCNoAccessQuoteAndOperStages = new CNoAccessQuoteAndOperStages();
            objCNoAccessQuoteAndOperStages.GetList(" WHERE IsOperationStage = 1 AND IsInActive = 0 ORDER BY ViewOrder ");

            CNetwork objCNetwork = new CNetwork();
            objCNetwork.GetList("WHERE ID IN (SELECT DISTINCT NetworkID FROM AgentNetwork WHERE AgentID=" + objCvwOperations.lstCVarvwOperations[0].AgentID + ")");

            CBranches objCBranches = new CBranches();
            objCBranches.GetList(" WHERE 1=1 ORDER BY Name ");
            CUsers objCUsers = new CUsers();
            objCUsers.GetList(" WHERE IsNull(CustomerID , 0) = 0 AND IsInactive=0 OR ID=" + objCvwOperations.lstCVarvwOperations[0].SalesmanID + " ORDER BY Name ");
            CIncoterms objCIncoterms = new CIncoterms();
            objCIncoterms.GetList(" WHERE 1=1 ORDER BY Name ");
            CNoAccessFreightTypes objCNoAccessFreightTypes = new CNoAccessFreightTypes();
            objCNoAccessFreightTypes.GetList(" WHERE 1=1 ORDER BY Name ");

            var pWhereClauseMoveTypes = " WHERE 1=1 ";
            pWhereClauseMoveTypes += (objCvwOperations.lstCVarvwOperations[0].TransportType == 1 ? " AND IsOcean = 1 " : "");
            pWhereClauseMoveTypes += (objCvwOperations.lstCVarvwOperations[0].TransportType == 2 ? " AND IsAir = 1 " : "");
            pWhereClauseMoveTypes += (objCvwOperations.lstCVarvwOperations[0].TransportType == 3 ? " AND IsInland = 1 " : "");
            pWhereClauseMoveTypes += " ORDER BY Name ";
            CMoveTypes objCMoveTypes = new CMoveTypes();
            objCMoveTypes.GetList(pWhereClauseMoveTypes);
            CCommodities objCCommodities = new CCommodities();
            objCCommodities.GetList(" WHERE 1=1 ORDER BY Name ");
            CInvoiceTypes objCInvoiceTypes = new CInvoiceTypes();
            objCInvoiceTypes.GetList(" WHERE 1=1 AND Code<>N'CREDITMEMO' ORDER BY Name ");

            #endregion Data for select Lists

            #region Get Lists with minimal columns
            var pIncotermList = objCIncoterms.lstCVarIncoterms
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                }).ToList();
            var pUserList = objCUsers.lstCVarUsers
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                    ,
                    IsSalesman = s.IsSalesman
                }).ToList();
            var pBranchList = objCBranches.lstCVarBranches
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                }).ToList();
            var pMoveTypeList = objCMoveTypes.lstCVarMoveTypes
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                }).ToList();
            var pCommodityList = objCCommodities.lstCVarCommodities
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                }).ToList();
            #endregion Get Lists with minimal columns

            #region get privileges to decide what data to retrieve

            CvwSecUserCustomizedTabs objCvwSecUserCustomizedTabs = new CvwSecUserCustomizedTabs();
            objCvwSecUserCustomizedTabs.GetList(" WHERE UserID = " + WebSecurity.CurrentUserId.ToString() + " AND FormID = " + pOperationFormID.ToString());

            //TabName is used as Module name
            bool GeneralOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "General")).CanView;
            bool PartnersOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "Partners")).CanView;
            bool PackagesOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "Packages")).CanView;
            bool RoutingOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "Routing")).CanView;
            bool PayablesOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "Payables")).CanView;
            bool ReceivablesOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "Receivables")).CanView;
            bool DraftInvoiceOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "DraftInvoice")).CanView;
            bool InvoicesOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "Invoices")).CanView;
            bool AccNotesOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "AccNotes")).CanView;
            bool PurchaseInvoiceOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "PurchaseInvoice")).CanView;
            bool DocsOutOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "DocsOut")).CanView;
            bool DocsInOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "DocsIn")).CanView;
            bool MasterOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "Master")).CanView;
            bool ShipmentsOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "Shipments")).CanView;
            bool TrackingOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "Tracking")).CanView;
            bool VehicleOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "Vehicle")).CanView;

            #endregion get privileges to decide what data to retrieve

            //CvwOperationPartners objCvwOperationPartners = new CvwOperationPartners();
            //objCvwOperationPartners.GetList(" WHERE OperationID = " + pOperationID.ToString() + " ORDER BY ViewOrder, ID ");

            //var pDocsWhereClause = " WHERE 1=1 ";
            //pDocsWhereClause += (objCvwOperations.lstCVarvwOperations[0].DirectionType == 1 ? " AND IsImport = 1 " : "");
            //pDocsWhereClause += (objCvwOperations.lstCVarvwOperations[0].DirectionType == 2 ? " AND IsExport = 1 " : "");
            //pDocsWhereClause += (objCvwOperations.lstCVarvwOperations[0].DirectionType == 3 ? " AND IsDomestic = 1 " : "");
            //pDocsWhereClause += (objCvwOperations.lstCVarvwOperations[0].TransportType == 1 ? " AND IsOcean = 1 " : "");
            //pDocsWhereClause += (objCvwOperations.lstCVarvwOperations[0].TransportType == 2 ? " AND IsAir = 1 " : "");
            //pDocsWhereClause += (objCvwOperations.lstCVarvwOperations[0].TransportType == 3 ? " AND IsInland = 1 " : "");
            //pDocsWhereClause += " AND IsDocOut = 1 ";
            //pDocsWhereClause += " ORDER BY TableOrViewName ";//the ID is used as the ViewOrder(its not AutoIdentity)

            //CvwOperationContainersAndPackages objCvwOperationContainersAndPackages = new CvwOperationContainersAndPackages();
            //if (PackagesOperationsCanView || ShipmentsOperationsCanView)
            //    //objCvwOperationContainersAndPackages.GetList(" WHERE OperationID = " + pOperationID.ToString());
            //    objCvwOperationContainersAndPackages.GetListPaging(99999,1, " WHERE OperationID = " + pOperationID.ToString(), "ID", out _RowCount);
            //CvwRoutings objCvwRoutings = new CvwRoutings();
            //if (RoutingOperationsCanView)
            //    //objCvwRoutings.GetList(" WHERE OperationID = " + pOperationID.ToString() + " ORDER BY RoutingTypeID");
            //    objCvwRoutings.GetListPaging(9999, 1, " WHERE OperationID = " + pOperationID.ToString(), "RoutingTypeID",out _RowCount);

            //CvwPayables objCvwPayables = new CvwPayables();
            //if (PayablesOperationsCanView || ReceivablesOperationsCanView || InvoicesOperationsCanView || AccNotesOperationsCanView || DocsOutOperationsCanView)
            //    objCvwPayables.GetListPaging(999999, 1, " WHERE OperationID = " + pOperationID.ToString() + " OR MasterOperationID = " + pOperationID.ToString() /* AND IsDeleted = 0 */, "ChargeTypeName", out _RowCount);

            //CvwReceivables objCvwReceivables = new CvwReceivables();
            //if (PayablesOperationsCanView || ReceivablesOperationsCanView || InvoicesOperationsCanView || AccNotesOperationsCanView || DocsOutOperationsCanView)
            //    //objCvwReceivables.GetList(" WHERE (OperationID = " + pOperationID.ToString() + " OR MasterOperationID = " + pOperationID.ToString() + ") AND IsDeleted = 0 ORDER BY ChargeTypeName");
            //    objCvwReceivables.GetListPaging(99999, 1, " WHERE (OperationID = " + pOperationID.ToString() + " OR MasterOperationID = " + pOperationID.ToString() + ") AND IsDeleted = 0", " ChargeTypeName ", out _RowCount);

            //CvwInvoices objCvwInvoices = new CvwInvoices();
            //if (PayablesOperationsCanView || ReceivablesOperationsCanView || InvoicesOperationsCanView || AccNotesOperationsCanView || DocsOutOperationsCanView)
            //    //objCvwInvoices.GetList(" WHERE (OperationID = " + pOperationID.ToString() + " OR MasterOperationID = " + pOperationID.ToString() + ") AND IsDeleted = 0 ");
            //    objCvwInvoices.GetListPaging(99999, 1, " WHERE (OperationID = " + pOperationID.ToString() + " OR MasterOperationID = " + pOperationID.ToString() + ") AND IsDeleted = 0 ", "ID DESC", out _RowCount);

            //CvwAccNote objCvwAccNotes = new CvwAccNote();
            //if (PayablesOperationsCanView || ReceivablesOperationsCanView || AccNotesOperationsCanView || AccNotesOperationsCanView || DocsOutOperationsCanView)
            //    //objCvwAccNotes.GetList(" WHERE (OperationID = " + pOperationID.ToString() + " OR MasterOperationID = " + pOperationID.ToString() + ") AND IsDeleted = 0 ");
            //    objCvwAccNotes.GetListPaging(1500, 1, " WHERE (OperationID = " + pOperationID.ToString() + " OR MasterOperationID = " + pOperationID.ToString() + ") AND IsDeleted = 0 ", "ID", out _RowCount);

            //CvwPurchaseInvoice objCvwPurchaseInvoice = new CvwPurchaseInvoice();
            //if (PurchaseInvoiceOperationsCanView)
            //    //objCvwPurchaseInvoice.GetList(" WHERE (OperationID = " + pOperationID.ToString() + " OR MasterOperationID = " + pOperationID.ToString() + ") AND IsDeleted = 0 ");
            //    objCvwPurchaseInvoice.GetListPaging(99999, 1, " WHERE (OperationID = " + pOperationID.ToString() + " OR MasterOperationID = " + pOperationID.ToString() + ") AND IsDeleted = 0 ", "ID", out _RowCount);

            //CDocumentTypes objCDocsOut = new CDocumentTypes();
            //if (DocsOutOperationsCanView)
            //    objCDocsOut.GetList(pDocsWhereClause);

            //todo
            //if (DocsInOperationsCanView)
            //get files from ~/DocsInFiles/Op-Code

            //CvwOperations objCvwMaster = new CvwOperations();
            //if ((MasterOperationsCanView) && objCvwOperations.lstCVarvwOperations[0].BLType == 2) //this means house so get master
            //    objCvwMaster.GetList(" WHERE ID = " + objCvwOperations.lstCVarvwOperations[0].MasterOperationID.ToString());

            //CvwOperations objCvwHouses = new CvwOperations();
            //if ((ShipmentsOperationsCanView || PackagesOperationsCanView) && objCvwOperations.lstCVarvwOperations[0].BLType == 3) //this means master so get houses
            //{
            //    var pHousesWhereClause = " WHERE 1=1 ";
            //    pHousesWhereClause += " AND BLType = " + "2";
            //    ////I disabled the next line coz i stopped connecting/disconnecting
            //    //pHousesWhereClause += " AND POL = " + objCvwOperations.lstCVarvwOperations[0].POL + " AND POD = " + objCvwOperations.lstCVarvwOperations[0].POD;
            //    pHousesWhereClause += " AND TransportType = " + objCvwOperations.lstCVarvwOperations[0].TransportType;
            //    pHousesWhereClause += " AND DirectionType = " + objCvwOperations.lstCVarvwOperations[0].DirectionType;
            //    if (objCvwOperations.lstCVarvwOperations[0].ShipmentType == 1/*constFCLShipmentType*/ 
            //        || objCvwOperations.lstCVarvwOperations[0].ShipmentType == 2/*constLCLShipmentType*/ 
            //        || objCvwOperations.lstCVarvwOperations[0].ShipmentType == 3/*constFTLShipmentType*/ 
            //        || objCvwOperations.lstCVarvwOperations[0].ShipmentType == 4/*constLTLShipmentType*/)
            //        pHousesWhereClause += " AND ShipmentType = " + objCvwOperations.lstCVarvwOperations[0].ShipmentType;
            //    else if (objCvwOperations.lstCVarvwOperations[0].ShipmentType == 5)//Consolidation Shipment() i.e. ShipmentType = 5
            //    {
            //        if (objCvwOperations.lstCVarvwOperations[0].TransportType == 1/*OceanTransportType*/)
            //            pHousesWhereClause += " AND ShipmentType = " + "2 "; //LCL
            //        else
            //            if (objCvwOperations.lstCVarvwOperations[0].TransportType == 3) //Inland
            //                pHousesWhereClause += " AND ShipmentType = " + "4 "; //LTL
            //    }
            //    else // ShipmentType = 0 (null) incase of Air
            //        pHousesWhereClause += " AND (ShipmentType IS NULL OR ShipmentType = 0) "; //Air
            //    //pHousesWhereClause += " AND (MasterOperationID IS NULL OR MasterOperationID = " + pOperationID.ToString() + " ) ";
            //    pHousesWhereClause += " AND (MasterOperationID = " + pOperationID.ToString() + " ) ";
            //    pHousesWhereClause += " ORDER BY ID DESC ";

            //    objCvwHouses.GetList(pHousesWhereClause);
            //}

            //to fill the select list containing master and houses for the DocsOut
            //var objMasterAndHouses = new JavaScriptSerializer().Serialize(objCvwHouses.lstCVarvwOperations.FindAll(c => (c.MasterOperationID == pOperationID) || (c.ID == pOperationID)));
            CvwOperations objMasterAndHouses = new CvwOperations();
            if (DocsOutOperationsCanView || InvoicesOperationsCanView || AccNotesOperationsCanView || ShipmentsOperationsCanView)
            {
                objMasterAndHouses.GetList(" WHERE ID = " + pOperationID.ToString() + " OR MasterOperationID = " + pOperationID.ToString());
                //objMasterAndHouses.lstCVarvwOperations.Add(objCvwOperations.lstCVarvwOperations[0]);
                //for (int i = 0; i < objCvwHouses.lstCVarvwOperations.Count(); i++)
                //    objMasterAndHouses.lstCVarvwOperations.Add(objCvwHouses.lstCVarvwOperations[i]);
            }

            //CvwOperationTracking objCvwTracking = new CvwOperationTracking();
            //if (TrackingOperationsCanView)
            //    objCvwTracking.GetList(" WHERE (OperationID = " + pOperationID.ToString() + " OR MasterOperationID = " + pOperationID.ToString() + ") ORDER BY ViewOrder");

            string[] pDocsInFileNames = null;
            //var strFolderPath = HttpContext.Current.Server.MapPath("~/DocsInFiles/") + objCvwOperations.lstCVarvwOperations[0].Code;
            var strFolderPath = HttpContext.Current.Server.MapPath("~/DocsInFiles/" + objCvwOperations.lstCVarvwOperations[0].CreationDate.Year.ToString() + "/") + objCvwOperations.lstCVarvwOperations[0].Code;
            if (DocsInOperationsCanView && Directory.Exists(strFolderPath))
            {
                //to get filenames on a directory
                pDocsInFileNames = Directory.GetFiles(strFolderPath);
                for (int i = 0; i < pDocsInFileNames.Length; i++)
                    pDocsInFileNames[i] = pDocsInFileNames[i].Substring(pDocsInFileNames[i].LastIndexOf('\\') + 1);
                //var filePath = files[0].Substring(0, files[0].LastIndexOf('\\'));
                //var firstFileName = files[0].Substring(files[0].LastIndexOf('/') + 1);
            }
            bool pIsOperationClosed = (objCvwOperations.lstCVarvwOperations[0].CloseDate < DateTime.Now && objCvwOperations.lstCVarvwOperations[0].OperationStageID != CancelledQuoteAndOperStageID);//2nd condition coz with time closedate passes but the operation is still cancelled

            CvwOperationPartners objCvwOperationPartners = new CvwOperationPartners();
            objCvwOperationPartners.GetList(" Where OperationID = " + pOperationID + " AND PartnerTypeID = 8 ");

            CSuppliers objCSuppliers = new CSuppliers();
            objCSuppliers.GetListPaging(999999, 1, " Where ID IN (SELECT SupplierID FROM OperationPartners Where OperationID = " + pOperationID + ")", "Name", out _RowCount);

            return new Object[] { new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations), _RowCount
                , null //new JavaScriptSerializer().Serialize(objCvwOperationPartners.lstCVarvwOperationPartners) //data[2]
                , null //new JavaScriptSerializer().Serialize(objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages) //data[3]
                , null //new JavaScriptSerializer().Serialize(objCvwRoutings.lstCVarvwRoutings) //data[4]
                , null //new JavaScriptSerializer().Serialize(objCvwPayables.lstCVarvwPayables) //data[5]
                , null //new JavaScriptSerializer().Serialize(objCvwReceivables.lstCVarvwReceivables) //data[6]
                , null //new JavaScriptSerializer().Serialize(objCvwInvoices.lstCVarvwInvoices) //data[7]
                , null //new JavaScriptSerializer().Serialize(objCDocsOut.lstCVarDocumentTypes) //data[8]
                , null //new JavaScriptSerializer().Serialize(objCvwMaster.lstCVarvwOperations) //data[9] : i commented it coz i dont open house so i dont draw it
                , null //new JavaScriptSerializer().Serialize(objCvwHouses.lstCVarvwOperations) //data[10]
                , new JavaScriptSerializer().Serialize(objMasterAndHouses.lstCVarvwOperations) //data[11]
                , pIsOperationClosed //data[12],
                , new JavaScriptSerializer().Serialize(pDocsInFileNames) //data[13]
                , new JavaScriptSerializer().Serialize(objCNoAccessQuoteAndOperStages.lstCVarNoAccessQuoteAndOperStages)//data[14]
                , new JavaScriptSerializer().Serialize(pBranchList)//data[15]
                , new JavaScriptSerializer().Serialize(pUserList)//data[16]
                , new JavaScriptSerializer().Serialize(pIncotermList)//data[17]
                , new JavaScriptSerializer().Serialize(objCNoAccessFreightTypes.lstCVarNoAccessFreightTypes)//data[18]
                , new JavaScriptSerializer().Serialize(pMoveTypeList)//data[19]
                , new JavaScriptSerializer().Serialize(pCommodityList)//data[20]
                , new JavaScriptSerializer().Serialize(objCInvoiceTypes.lstCVarInvoiceTypes)//data[21]
                , null //new JavaScriptSerializer().Serialize(objCvwAccNotes.lstCVarvwAccNote) //data[22]
                , null //new JavaScriptSerializer().Serialize(objCvwTracking.lstCVarvwOperationTracking) //data[23]
                , new JavaScriptSerializer().Serialize(objCNetwork.lstCVarNetwork)//data[24]
                , null //new JavaScriptSerializer().Serialize(objCvwPurchaseInvoice.lstCVarvwPurchaseInvoice) //data[25]
                , new JavaScriptSerializer().Serialize(objCSuppliers.lstCVarSuppliers)
            };
        }

        [HttpGet, HttpPost] // find the number of unapproved invoices, accnotes, nonzero payables
        public int FindNumberOfUnApproved_Invoices_AccNotes_Payables(Int64 pOperationID)
        {
            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
            string NumberOfInvoices = objCCustomizedDBCall.CallStringFunction("select COUNT(*) from Invoices WHERE ISNULL(IsApproved,0)=0 AND OperationID =" + pOperationID);
            string NumberOfAccNotes = objCCustomizedDBCall.CallStringFunction("select COUNT(*) from AccNote WHERE ISNULL(IsApproved,0)=0 AND OperationID =" + pOperationID);
            string NumberOfPayables = objCCustomizedDBCall.CallStringFunction("select COUNT(*) from Payables WHERE ISNULL(IsApproved,0)=0 AND CostPrice<>0 AND OperationID =" + pOperationID);

            return (int.Parse(NumberOfInvoices) + int.Parse(NumberOfAccNotes) + int.Parse(NumberOfPayables));
        }

        [HttpGet, HttpPost]
        public string[] FindCertificateNumberAndCertificateDate(Int64 pOperationID)
        {
            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
            string CertificateNumber = objCCustomizedDBCall.CallStringFunction("SELECT CertificateNumber FROM Routings WHERE OperationID="+ pOperationID + " AND RoutingTypeID=70");
            string CertificateDate = objCCustomizedDBCall.CallStringFunction("SELECT CertificateDate FROM Routings WHERE OperationID=" + pOperationID + " AND RoutingTypeID=70");

            string[] CertificateData = new string[2];
            CertificateData[0] = CertificateNumber;
            CertificateData[1] = CertificateDate;

            return (CertificateData);
        }

        [HttpGet, HttpPost] //called with operation edit page and load all its details
        public object[] LoadOperationWithDetails_ForEdit(Int32 pPageNumber, Int32 pPageSize, string pWhereClause_ForEdit, Int64 pOperationID, int pOperationFormID)
        {
            int CancelledQuoteAndOperStageID = 110;
            //int ClosedQuoteAndOperStageID = 120;
            CvwOperations objCvwOperations = new CvwOperations();
            Int32 _RowCount = objCvwOperations.lstCVarvwOperations.Count;
            Exception checkException = null;
            //checkException = objCvwOperations.GetListPaging(pPageSize, pPageNumber, pWhereClause_ForEdit, " ID DESC ", out _RowCount);
            checkException = objCvwOperations.GetList(pWhereClause_ForEdit);

            #region Data for select Lists
            CNoAccessQuoteAndOperStages objCNoAccessQuoteAndOperStages = new CNoAccessQuoteAndOperStages();
            objCNoAccessQuoteAndOperStages.GetList(" WHERE IsOperationStage = 1 AND IsInActive = 0 ORDER BY ViewOrder ");

            CNetwork objCNetwork = new CNetwork();
            objCNetwork.GetList("WHERE ID IN (SELECT DISTINCT NetworkID FROM AgentNetwork WHERE AgentID=" + objCvwOperations.lstCVarvwOperations[0].AgentID + ")");

            CBranches objCBranches = new CBranches();
            objCBranches.GetList(" WHERE 1=1 ORDER BY Name ");
            CUsers objCUsers = new CUsers();
            objCUsers.GetList(" WHERE IsNull(CustomerID , 0) = 0 AND IsInactive=0 OR ID=" + objCvwOperations.lstCVarvwOperations[0].SalesmanID + " ORDER BY Name ");
            CIncoterms objCIncoterms = new CIncoterms();
            objCIncoterms.GetList(" WHERE 1=1 ORDER BY Name ");
            CNoAccessFreightTypes objCNoAccessFreightTypes = new CNoAccessFreightTypes();
            objCNoAccessFreightTypes.GetList(" WHERE 1=1 ORDER BY Name ");

            var pWhereClauseMoveTypes = " WHERE 1=1 ";
            pWhereClauseMoveTypes += (objCvwOperations.lstCVarvwOperations[0].TransportType == 1 ? " AND IsOcean = 1 " : "");
            pWhereClauseMoveTypes += (objCvwOperations.lstCVarvwOperations[0].TransportType == 2 ? " AND IsAir = 1 " : "");
            pWhereClauseMoveTypes += (objCvwOperations.lstCVarvwOperations[0].TransportType == 3 ? " AND IsInland = 1 " : "");
            pWhereClauseMoveTypes += " ORDER BY Name ";
            CMoveTypes objCMoveTypes = new CMoveTypes();
            objCMoveTypes.GetList(pWhereClauseMoveTypes);
            CCommodities objCCommodities = new CCommodities();
            objCCommodities.GetList(" WHERE 1=1 ORDER BY Name ");
            CInvoiceTypes objCInvoiceTypes = new CInvoiceTypes();
            objCInvoiceTypes.GetList(" WHERE 1=1 AND Code<>N'CREDITMEMO' ORDER BY Name ");

            CVessels objCVessels = new CVessels();
            objCVessels.GetList("Where 1=1");
            #endregion Data for select Lists

            #region Get Lists with minimal columns
            var pIncotermList = objCIncoterms.lstCVarIncoterms
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                }).ToList();
            var pUserList = objCUsers.lstCVarUsers
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                    ,
                    IsSalesman = s.IsSalesman
                }).ToList();
            var pBranchList = objCBranches.lstCVarBranches
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                }).ToList();
            var pMoveTypeList = objCMoveTypes.lstCVarMoveTypes
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                }).ToList();
            var pCommodityList = objCCommodities.lstCVarCommodities
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                    ,
                    Code = s.Code
                }).ToList();
            #endregion Get Lists with minimal columns

            int total = 0;
            var CvwAddresses = new CvwAddresses();
            CvwAddresses.GetListPaging(99, 1, "WHERE PartnerID=" + objCvwOperations.lstCVarvwOperations[0].ClientID, "ID", out total);

            string ClientPickUpAddress = "";
            string ClientDeliveryAddress = "";
            string ClientOtherAddress = "";

            foreach(var address in CvwAddresses.lstCVarvwAddresses)
            {
                if (address.AddressTypeID == 3)
                    ClientPickUpAddress = address.StreetLine1;
                else if (address.AddressTypeID == 4)
                    ClientDeliveryAddress = address.StreetLine1;
                else if (address.AddressTypeID == 6)
                    ClientOtherAddress = address.StreetLine1;
            }

            CvwContacts CvwContacts = new CvwContacts();
            CvwContacts.GetListPaging(99, 1, "WHERE PartnerID=" + objCvwOperations.lstCVarvwOperations[0].ClientID, "ID", out total);
            string ClientContactDetails = "";
            if (total == 1)
                ClientContactDetails = CvwContacts.lstCVarvwContacts[0].Name  + " - " + CvwContacts.lstCVarvwContacts[0].Phone1 + " - " + CvwContacts.lstCVarvwContacts[0].Email;


                #region get privileges to decide what data to retrieve

            CvwSecUserCustomizedTabs objCvwSecUserCustomizedTabs = new CvwSecUserCustomizedTabs();
            //objCvwSecUserCustomizedTabs.GetList(" WHERE UserID = " + WebSecurity.CurrentUserId.ToString() + " AND FormID = " + pOperationFormID.ToString());

            ////TabName is used as Module name
            //bool GeneralOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "General")).CanView;
            //bool PartnersOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "Partners")).CanView;
            //bool PackagesOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "Packages")).CanView;
            //bool RoutingOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "Routing")).CanView;
            //bool PayablesOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "Payables")).CanView;
            //bool ReceivablesOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "Receivables")).CanView;
            //bool DraftInvoiceOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "DraftInvoice")).CanView;
            //bool InvoicesOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "Invoices")).CanView;
            //bool AccNotesOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "AccNotes")).CanView;
            //bool PurchaseInvoiceOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "PurchaseInvoice")).CanView;
            //bool DocsOutOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "DocsOut")).CanView;
            //bool DocsInOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "DocsIn")).CanView;
            //bool MasterOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "Master")).CanView;
            //bool ShipmentsOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "Shipments")).CanView;
            //bool TrackingOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "Tracking")).CanView;
            //bool VehicleOperationsCanView = objCvwSecUserCustomizedTabs.lstCVarvwSecUserCustomizedTabs.Find(c => (c.TabCode == "Vehicle")).CanView;

            #endregion get privileges to decide what data to retrieve

            string[] pDocsInFileNames = null;
            //var strFolderPath = HttpContext.Current.Server.MapPath("~/DocsInFiles/") + objCvwOperations.lstCVarvwOperations[0].Code;
            var strFolderPath = HttpContext.Current.Server.MapPath("~/DocsInFiles/" + objCvwOperations.lstCVarvwOperations[0].CreationDate.Year.ToString() + "/") + objCvwOperations.lstCVarvwOperations[0].Code;
            if (Directory.Exists(strFolderPath))
            {
                //to get filenames on a directory
                pDocsInFileNames = Directory.GetFiles(strFolderPath);
                for (int i = 0; i < pDocsInFileNames.Length; i++)
                    pDocsInFileNames[i] = pDocsInFileNames[i].Substring(pDocsInFileNames[i].LastIndexOf('\\') + 1);
                //var filePath = files[0].Substring(0, files[0].LastIndexOf('\\'));
                //var firstFileName = files[0].Substring(files[0].LastIndexOf('/') + 1);
            }
            bool pIsOperationClosed = (objCvwOperations.lstCVarvwOperations[0].CloseDate < DateTime.Now && objCvwOperations.lstCVarvwOperations[0].OperationStageID != CancelledQuoteAndOperStageID);//2nd condition coz with time closedate passes but the operation is still cancelled

            CSuppliers objCSuppliers = new CSuppliers();
            objCSuppliers.GetListPaging(999999, 1, " Where ID IN (SELECT SupplierID FROM OperationPartners Where OperationID = " + pOperationID + ")", "Name", out _RowCount);

            CvwOperationsWithMinimalColumns objMasterAndHouses = new CvwOperationsWithMinimalColumns();
            if (objCvwOperations.lstCVarvwOperations[0].BLType == 3) //constMasterBLType
            {
                objMasterAndHouses.GetList(" WHERE ID = " + pOperationID.ToString() + " OR MasterOperationID = " + pOperationID.ToString());
                //objMasterAndHouses.lstCVarvwOperations.Add(objCvwOperations.lstCVarvwOperations[0]);
                //for (int i = 0; i < objCvwHouses.lstCVarvwOperations.Count(); i++)
                //    objMasterAndHouses.lstCVarvwOperations.Add(objCvwHouses.lstCVarvwOperations[i]);
            }
            
            return new Object[] { new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations[0])
                , _RowCount
                , pIsOperationClosed //data[2],
                , new JavaScriptSerializer().Serialize(pDocsInFileNames) //data[3]
                , new JavaScriptSerializer().Serialize(objCNoAccessQuoteAndOperStages.lstCVarNoAccessQuoteAndOperStages) //data[4]
                , new JavaScriptSerializer().Serialize(pBranchList) //data[5]
                , new JavaScriptSerializer().Serialize(pUserList) //data[6]
                , new JavaScriptSerializer().Serialize(pIncotermList) //data[7]
                , new JavaScriptSerializer().Serialize(objCNoAccessFreightTypes.lstCVarNoAccessFreightTypes) //data[8]
                , new JavaScriptSerializer().Serialize(pMoveTypeList) //data[9]
                , new JavaScriptSerializer().Serialize(pCommodityList) //data[10]
                , new JavaScriptSerializer().Serialize(objCInvoiceTypes.lstCVarInvoiceTypes) //data[11]
                , new JavaScriptSerializer().Serialize(objCNetwork.lstCVarNetwork) //data[12]
                , new JavaScriptSerializer().Serialize(objCSuppliers.lstCVarSuppliers) //data[13]
                , new JavaScriptSerializer().Serialize(objMasterAndHouses.lstCVarvwOperationsWithMinimalColumns) //data[14]
                , new JavaScriptSerializer().Serialize(objCVessels.lstCVarVessels) //data[15]
                , ClientPickUpAddress //data[16]
                , ClientDeliveryAddress //data[17]
                , ClientOtherAddress //data[18]
                , ClientContactDetails //data[19]
            };
        }

        #region GuaranteeLetter
        [HttpGet, HttpPost]
        public Object[] LoadGuaranteeLetterData(Int64 pOperationIDForGuaranteeLetter)
        {
            // Creates an instance of your JavaScriptSerializer & Setting the MaxJsonLength to handle the case of large data returned
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            int _RowCount = 0;
            CBankAccount objCBankAccount = new CBankAccount();
            CvwOperations objCvwOperations = new CvwOperations();
            objCvwOperations.GetListPaging(99999, 1, "WHERE ID=" + pOperationIDForGuaranteeLetter, "ID DESC", out _RowCount);
            objCBankAccount.GetList("ORDER BY Name");
            return new Object[] {
                serializer.Serialize(objCvwOperations.lstCVarvwOperations[0])
                , serializer.Serialize(objCBankAccount.lstCVarBankAccount)
            };
        }

        [HttpGet, HttpPost]
        public Object[] SaveGuaranteeLetter(Int64 pOperationIDForGuaranteeLetter, string pGuaranteeLetterNumber
            , string pGuaranteeLetterDate, string pGuaranteeLetterAmount, string pSupplierReference, string pPONumber
            , Int32 pBankAccountID, string pGuaranteeLetterNotes)
        {
            string _ReturnedMessage = "";
            string _UpdateClause = "";
            Exception checkException = null;
            COperations objCOperations = new COperations();
            _UpdateClause = " GuaranteeLetterNumber = " + (pGuaranteeLetterNumber == "0" ? " NULL " : "'" + pGuaranteeLetterNumber + "' ") + "\n";
            _UpdateClause += pGuaranteeLetterDate == "0" ? " ,GuaranteeLetterDate = NULL " : (" ,GuaranteeLetterDate = '" + (DateTime.ParseExact(pGuaranteeLetterDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd") + "'") + "\n";
            _UpdateClause += " , GuaranteeLetterAmount = " + (pGuaranteeLetterAmount == "0" ? " NULL " : "'" + pGuaranteeLetterAmount + "' ") + "\n";
            _UpdateClause += " , BankAccountID = " + (pBankAccountID == 0 ? " NULL " : "'" + pBankAccountID + "' ") + "\n";
            _UpdateClause += " , SupplierReference = " + (pSupplierReference == "0" ? " NULL " : "'" + pSupplierReference + "' ") + "\n";
            _UpdateClause += " , PONumber = " + (pPONumber == "0" ? " NULL " : "'" + pPONumber + "' ") + "\n";
            _UpdateClause += " , GuaranteeLetterNotes = " + (pGuaranteeLetterNotes == "0" ? " NULL " : "N'" + pGuaranteeLetterNotes + "' ") + "\n";
            _UpdateClause += "WHERE ID=" + pOperationIDForGuaranteeLetter + "\n";
            checkException = objCOperations.UpdateList(_UpdateClause);
            if (checkException != null)
                _ReturnedMessage = checkException.Message;
            return new object[] {
                _ReturnedMessage
            };
        }
        #endregion GuaranteeLetter

        [HttpGet, HttpPost]
        public object[] Insert([FromBody] InsertOperationData insertOperationData)
        {
            string _MessageReturned = "";
            bool _result = false;
            int MainCarraigeRoutingTypeID = 30;
            COperations objCOperations = new COperations();
            int _RowCount = 0;
            Int64 OperationID = 0;
            Int64 RoutingID = 0;
            Int32 POrC = 0;
            Int64 OperationContainersAndPackagesID = 0;
            Int64 PayableID = 0;
            Int64 OperationPartners = 0;
            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
            var CancelledTransportOrderID = 80;
            CvwDefaults objCvwDefaults = new CvwDefaults();
            objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa
            //COperationPartners objCOperationPartnersForTax = new COperationPartners();

            CAirlines objCAirlines = new CAirlines();
            if (int.Parse(insertOperationData.pAirlineID) > 0)
            {
                objCAirlines.GetList("WHERE ID=" + insertOperationData.pAirlineID);
                insertOperationData.pMAWBSuffix = insertOperationData.pMasterBL;
                insertOperationData.pMasterBL = objCAirlines.lstCVarAirlines[0].Prefix + "-" + insertOperationData.pMasterBL;
            }

            COperations objCOperations_CheckUniqueness = new COperations();
            if (!objCvwDefaults.lstCVarvwDefaults[0].IsRepeatMBL && insertOperationData.pMasterBL != "0" && insertOperationData.pMasterBL != "")
                objCOperations_CheckUniqueness.GetListPaging(999999, 1, "WHERE OperationStageID<>" + CancelledTransportOrderID + " AND TransportType=" + insertOperationData.pTransportType + " AND DirectionType=" + insertOperationData.pDirectionType + "  AND MasterBL IS NOT NULL AND MasterBL<>N'N/A' AND MasterBL NOT LIKE '%-0' AND MasterBL=N'" + insertOperationData.pMasterBL + "'", "ID", out _RowCount);
            if (objCOperations_CheckUniqueness.lstCVarOperations.Count > 0 && objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName != "BOM")
                _MessageReturned = "Operation " + objCOperations_CheckUniqueness.lstCVarOperations[0].Code + " has the same Master B/L No.";
            #region Check House Uiqueness
            else
            {
                if ((objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName == "EGL" || objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName == "SUN")
                    && insertOperationData.pIsShipment)
                    objCOperations_CheckUniqueness.GetListPaging(999999, 1, "WHERE HouseNumber IS NOT NULL AND HouseNumber<>N'' AND HouseNumber<>N'N/A' AND HouseNumber=N'" + insertOperationData.pHouseNumber + "'", "ID", out _RowCount);
                if (objCOperations_CheckUniqueness.lstCVarOperations.Count > 0)
                    _MessageReturned = "This house number already exists.";
            }
            #endregion Check House Uiqueness
            if (_MessageReturned == "")
            {
                CVarOperations objCVarOperations = new CVarOperations();
                int constShippingLineOperationPartnerTypeID = 9;
                int constAirineOperationPartnerTypeID = 10;
                int constTruckerOperationPartnerTypeID = 11;

                #region Operation Header
                objCVarOperations.MasterBL = insertOperationData.pMasterBL;
                objCVarOperations.MAWBSuffix = insertOperationData.pMAWBSuffix;
                objCVarOperations.BLDate = DateTime.Parse("01-01-1900");
                objCVarOperations.HBLDate = DateTime.Parse("01-01-1900");
                objCVarOperations.PODate = DateTime.Parse("01-01-1900");


                objCVarOperations.Code = insertOperationData.pCode;
                objCVarOperations.HouseNumber = insertOperationData.pIsShipment ? insertOperationData.pHouseNumber : "0";//if HouseNumber is not null then its entered manually
                objCVarOperations.BranchID = int.Parse(insertOperationData.pBranchID);
                objCVarOperations.SalesmanID = int.Parse(insertOperationData.pSalesmanID);
                objCVarOperations.BLType = int.Parse(insertOperationData.pBLType);
                objCVarOperations.BLTypeIconName = insertOperationData.pBLTypeIconName;
                objCVarOperations.BLTypeIconStyle = insertOperationData.pBLTypeIconStyle;
                objCVarOperations.DirectionType = int.Parse(insertOperationData.pDirectionType);
                objCVarOperations.DirectionIconName = insertOperationData.pDirectionIconName;
                objCVarOperations.DirectionIconStyle = insertOperationData.pDirectionIconStyle;
                objCVarOperations.TransportType = int.Parse(insertOperationData.pTransportType);
                objCVarOperations.TransportIconName = insertOperationData.pTransportIconName;
                objCVarOperations.TransportIconStyle = insertOperationData.pTransportIconStyle;
                objCVarOperations.ShipmentType = int.Parse(insertOperationData.pShipmentType);
                objCVarOperations.ShipperID = int.Parse(insertOperationData.pShipperID);
                objCVarOperations.ShipperAddressID = Int64.Parse(insertOperationData.pShipperAddressID);
                objCVarOperations.ShipperContactID = Int64.Parse(insertOperationData.pShipperContactID);
                objCVarOperations.ConsigneeID = int.Parse(insertOperationData.pConsigneeID);
                objCVarOperations.ConsigneeAddressID = Int64.Parse(insertOperationData.pConsigneeAddressID);
                objCVarOperations.ConsigneeContactID = Int64.Parse(insertOperationData.pConsigneeContactID);
                objCVarOperations.AgentID = int.Parse(insertOperationData.pAgentID);
                objCVarOperations.AgentAddressID = Int64.Parse(insertOperationData.pAgentAddressID);
                objCVarOperations.AgentContactID = Int64.Parse(insertOperationData.pAgentContactID);
                objCVarOperations.IncotermID = int.Parse(insertOperationData.pIncotermID);
                objCVarOperations.POrC = insertOperationData.pPOrC;
                objCVarOperations.MoveTypeID = int.Parse(insertOperationData.pMoveTypeID);
                objCVarOperations.CommodityID = int.Parse(insertOperationData.pCommodityID);
                objCVarOperations.CommodityID2 = 0;
                objCVarOperations.CommodityID3 = 0;
                objCVarOperations.TransientTime = int.Parse(insertOperationData.pTransientTime);
                //objCVarOperations.OpenDate = DateTime.Parse(insertOperationData.pOpenDate);
                objCVarOperations.OpenDate = insertOperationData.pOpenDate;
                objCVarOperations.CloseDate = insertOperationData.pCloseDate; //DateTime.Parse("01-01-1900");
                objCVarOperations.CutOffDate = DateTime.Parse(insertOperationData.pCutOffDate);
                objCVarOperations.IncludePickup = (insertOperationData.pIncludePickup == "True" ? true : false);
                objCVarOperations.PickupCityID = int.Parse(insertOperationData.pPickupCityID);
                objCVarOperations.PickupAddressID = int.Parse(insertOperationData.pPickupAddressID);
                objCVarOperations.POLCountryID = int.Parse(insertOperationData.pPOLCountryID);
                objCVarOperations.POL = int.Parse(insertOperationData.pPOL);
                objCVarOperations.PODCountryID = int.Parse(insertOperationData.pPODCountryID);
                objCVarOperations.POD = int.Parse(insertOperationData.pPOD);
                objCVarOperations.PickupAddress = "0"; //updated from main route
                objCVarOperations.DeliveryAddress = "0"; //updated from main route
                objCVarOperations.ShippingLineID = int.Parse(insertOperationData.pShippingLineID);
                objCVarOperations.AirlineID = int.Parse(insertOperationData.pAirlineID);
                objCVarOperations.TruckerID = int.Parse(insertOperationData.pTruckerID);
                objCVarOperations.IncludeDelivery = (insertOperationData.pIncludeDelivery == "True" ? true : false);
                objCVarOperations.DeliveryZipCode = insertOperationData.pDeliveryZipCode;
                objCVarOperations.DeliveryCityID = int.Parse(insertOperationData.pDeliveryCityID);
                objCVarOperations.DeliveryCountryID = int.Parse(insertOperationData.pDeliveryCountryID);
                objCVarOperations.GrossWeight = decimal.Parse(insertOperationData.pGrossWeight);
                //objCVarOperations.Volume = decimal.Parse(insertOperationData.pVolume);
                objCVarOperations.ChargeableWeight = decimal.Parse(insertOperationData.pChargeableWeight);
                //objCVarOperations.NumberOfPackages = int.Parse(insertOperationData.pNumberOfPackages);
                objCVarOperations.IsDangerousGoods = (insertOperationData.pIsDangerousGoods == "True" ? true : false);
                objCVarOperations.Notes = insertOperationData.pNotes;
                objCVarOperations.CustomerReference = insertOperationData.pCustomerReference;
                objCVarOperations.SupplierReference = insertOperationData.pSupplierReference;
                objCVarOperations.PONumber = insertOperationData.pPONumber;
                objCVarOperations.POValue = "0";
                objCVarOperations.ReleaseNumber = "0";
                objCVarOperations.DispatchNumber = "0";
                objCVarOperations.BusinessUnit = "0";
                objCVarOperations.Form13Number = "0";


                objCVarOperations.BookingNumber = insertOperationData.pHouseNumber;
                objCVarOperations.AgreedRate = insertOperationData.pAgreedRate;
                objCVarOperations.OperationStageID = int.Parse(insertOperationData.pOperationStageID);
                objCVarOperations.NumberOfHousesConnected = 0;

                objCVarOperations.IsDelivered = insertOperationData.pIsDelivered;
                objCVarOperations.IsTrucking = insertOperationData.pIsTrucking;
                objCVarOperations.IsInsurance = insertOperationData.pIsInsurance;
                objCVarOperations.IsClearance = insertOperationData.pIsClearance;
                objCVarOperations.IsGenset = insertOperationData.pIsGenset;
                objCVarOperations.IsCourrier = insertOperationData.pIsCourrier;
                objCVarOperations.IsTelexRelease = insertOperationData.pIsTelexRelease;

                objCVarOperations.ConsigneeID2 = insertOperationData.pConsigneeID2;
                objCVarOperations.ReleaseDate = DateTime.ParseExact(insertOperationData.pReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                objCVarOperations.Form13Date = DateTime.Parse("01-01-1900");

                objCVarOperations.ClearanceApprovalDate = DateTime.Parse("01-01-1900");
                objCVarOperations.TruckingApprovalDate = DateTime.Parse("01-01-1900");
                objCVarOperations.FreightApprovalDate = DateTime.Parse("01-01-1900");

                objCVarOperations.ShippedOnBoardDate = DateTime.Parse("01-01-1900");
                objCVarOperations.FreightPayableAt = "0";
                objCVarOperations.CertificateNumber = insertOperationData.pCertificateNumber;
                objCVarOperations.CountryOfOrigin = insertOperationData.pCountryOfOrigin;
                objCVarOperations.InvoiceValue = insertOperationData.pInvoiceValue;
                objCVarOperations.NumberOfOriginalBills = 0;

                #region AirAgents (Venus fields A.Medra)
                objCVarOperations.BLDate = DateTime.ParseExact(insertOperationData.pBLDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                objCVarOperations.MAWBStockID = insertOperationData.pMAWBStockID;
                objCVarOperations.TypeOfStockID = insertOperationData.pTypeOfStockID;
                objCVarOperations.FlightNo = insertOperationData.pFlightNo;
                objCVarOperations.POrC = insertOperationData.pPOrC;
                objCVarOperations.IsAWB = insertOperationData.pIsAWB;
                //Fields not in insert
                objCVarOperations.AirLineID1 = 0;
                objCVarOperations.FlightNo1 = "0";
                objCVarOperations.FlightDate1 = DateTime.Parse("01/01/1900");
                objCVarOperations.AirLineID2 = 0;
                objCVarOperations.FlightNo2 = "0";
                objCVarOperations.FlightDate2 = DateTime.Parse("01/01/1900");
                objCVarOperations.AirLineID3 = 0;
                objCVarOperations.FlightNo3 = "0";
                objCVarOperations.FlightDate3 = DateTime.Parse("01/01/1900");

                objCVarOperations.UNOrID = "0";
                objCVarOperations.ProperShippingName = "0";
                objCVarOperations.ClassOrDivision = "0";
                objCVarOperations.PackingGroup = "0";
                objCVarOperations.QuantityAndTypeOfPacking = "0";
                objCVarOperations.PackingInstruction = "0";
                objCVarOperations.ShippingDeclarationAuthorization = "0";
                objCVarOperations.Barcode = "0";

                objCVarOperations.GuaranteeLetterNumber = "0";
                objCVarOperations.GuaranteeLetterDate = DateTime.Parse("01/01/1900");
                objCVarOperations.GuaranteeLetterAmount = "0";
                objCVarOperations.GuaranteeLetterSupplierInvoiceNumber = "0";
                objCVarOperations.BankAccountID = 0;
                objCVarOperations.GuaranteeLetterNotes = "0";

                objCVarOperations.HandlingInformation = "0";
                objCVarOperations.AmountOfInsurance = "0";
                objCVarOperations.DeclaredValueForCarriage = "0";
                objCVarOperations.WeightCharge = 0;
                objCVarOperations.ValuationCharge = 0;
                objCVarOperations.OtherChargesDueAgent = 0;
                objCVarOperations.OtherCharges = "0";
                objCVarOperations.CurrencyID = insertOperationData.pCurrencyID == 0 ? objCvwDefaults.lstCVarvwDefaults[0].CurrencyID : insertOperationData.pCurrencyID;
                objCVarOperations.AccountingInformation = "0";
                objCVarOperations.ReferenceNumber = "0";
                objCVarOperations.OptionalShippingInformation = "0";
                objCVarOperations.CHGSCode = "0";
                objCVarOperations.WT_VALL_Other = "0";
                objCVarOperations.DeclaredValueForCustoms = "0";
                objCVarOperations.Tax = 0;
                objCVarOperations.OtherChargesDueCarrier = 0;
                objCVarOperations.WT_VALL = "0";
                objCVarOperations.MarksAndNumbers = "0";
                objCVarOperations.Description = "0";
                #endregion Venus fields A.Medra

                objCVarOperations.CreatorUserID = objCVarOperations.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarOperations.CreationDate = (insertOperationData.pOpenDate == null ? DateTime.Parse("01-01-1900") : insertOperationData.pOpenDate);// DateTime.Now;
                objCVarOperations.ModificationDate = DateTime.Now;

                objCVarOperations.DismissalPermissionSerial = "0";
                objCVarOperations.DeliveryOrderSerial = "0";

                objCVarOperations.eFBLID = "0";
                objCVarOperations.eFBLStatus = 0;

                objCVarOperations.ACIDNumber = insertOperationData.pACIDNumber;
                objCVarOperations.ACIDDetails = insertOperationData.pACIDNumberDetails;
                objCVarOperations.UNNumber = insertOperationData.pUNNumber;
                objCVarOperations.IMOClass = insertOperationData.pIMOClass;
                objCVarOperations.VesselID = insertOperationData.pVesselID;
                objCVarOperations.HouseParentID = 0;





                objCOperations.lstCVarOperations.Add(objCVarOperations);
                Exception checkException = objCOperations.SaveMethod(objCOperations.lstCVarOperations);


                // save a log when inserting a house
                if (checkException == null && int.Parse(insertOperationData.pBLType) == 2)
                {
                    checkException = SaveOperationLog(objCOperations.lstCVarOperations[0].ID);
                }

                #endregion Operation Header
                if (checkException != null) // an exception is caught in the model
                {
                    if (checkException.Message.Contains("UNIQUE"))
                        _result = false;
                }
                else
                {
                    OperationID = objCVarOperations.ID;
                    POrC = objCVarOperations.POrC;

                    _result = true;
                    if (!insertOperationData.pIsShipment)
                        Operations_EmailNotification(objCVarOperations.ID);
                    #region DeliveryOrderSerial
                    if (insertOperationData.pBLType != "3") //not master
                    {
                        COperations objCOperationsTemp = new COperations();
                        //objCOperationsTemp.UpdateList("DeliveryOrderSerial=(SELECT ltrim((ISNULL(DeliveryOrderSerial,0) + 1)) + '-' + RIGHT('00'+CAST(MONTH(GETDATE()) AS VARCHAR(3)),2) + '-' + ltrim(YEAR(GETDate())) FROM Serials WHERE Year=YEAR(GETDate())) WHERE ID=" + pOperationID + "");
                        objCOperationsTemp.UpdateList("DeliveryOrderSerial=(SELECT ltrim((ISNULL(DeliveryOrderSerial,0) + 1)) + '/' + ltrim(YEAR(GETDate())) FROM Serials WHERE Year=YEAR(GETDate())) WHERE ID=" + objCVarOperations.ID + "");
                        CSerials objCSerials = new CSerials();
                        objCSerials.UpdateList("DeliveryOrderSerial=ISNULL(DeliveryOrderSerial,0) + 1 WHERE Year=YEAR(GETDate())");
                    }
                    #endregion DeliveryOrderSerial
                    //COPY Partners To OperationPartners
                    #region Copy Operation Partners (at this point its just either a shipper or a consignee or an agent and empty notify)
                    COperationPartners objCOperationPartners = new COperationPartners();

                    CVarOperationPartners objCVarOperationPartners = new CVarOperationPartners();
                    CContacts objCContacts = new CContacts();//to save a contact by default

                    //to save a contact by default //PartnerTypeID = 2 for Agents
                    objCContacts.GetList(" WHERE PartnerTypeID = 2 AND PartnerID = " + insertOperationData.pAgentID.ToString());

                    CVarOperationPartners objCVarOperationAgentPartner = new CVarOperationPartners();
                    objCVarOperationAgentPartner.OperationID = objCOperations.lstCVarOperations[0].ID;
                    objCVarOperationAgentPartner.OperationPartnerTypeID = 6; //Agent
                    objCVarOperationAgentPartner.AgentID = int.Parse(insertOperationData.pAgentID);
                    objCVarOperationAgentPartner.ContactID = (objCContacts.lstCVarContacts.Count > 0 ? objCContacts.lstCVarContacts[0].ID : 0);
                    objCVarOperationAgentPartner.IsOperationClient = false;
                    objCVarOperationAgentPartner.CreatorUserID = objCVarOperationAgentPartner.ModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarOperationAgentPartner.CreationDate = objCVarOperationAgentPartner.ModificationDate = DateTime.Now;
                    objCOperationPartners.lstCVarOperationPartners.Add(objCVarOperationAgentPartner);


                    //to save a contact by default //PartnerTypeID = 1 for Customer
                    objCContacts.GetList(" WHERE PartnerTypeID = 1 AND PartnerID = " + insertOperationData.pConsigneeID.ToString());

                    CVarOperationPartners objCVarOperationConsigneePartner = new CVarOperationPartners();
                    objCVarOperationConsigneePartner.OperationID = objCOperations.lstCVarOperations[0].ID;
                    objCVarOperationConsigneePartner.OperationPartnerTypeID = 2;
                    objCVarOperationConsigneePartner.CustomerID = int.Parse(insertOperationData.pConsigneeID);
                    objCVarOperationConsigneePartner.ContactID = (objCContacts.lstCVarContacts.Count > 0 ? objCContacts.lstCVarContacts[0].ID : 0);
                    objCVarOperationConsigneePartner.IsOperationClient = false;
                    objCVarOperationConsigneePartner.CreatorUserID = objCVarOperationConsigneePartner.ModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarOperationConsigneePartner.CreationDate = objCVarOperationConsigneePartner.ModificationDate = DateTime.Now;
                    objCOperationPartners.lstCVarOperationPartners.Add(objCVarOperationConsigneePartner);

                    //to save a contact by default //PartnerTypeID = 1 for Customer
                    objCContacts.GetList(" WHERE PartnerTypeID = 1 AND PartnerID = " + insertOperationData.pShipperID.ToString());

                    CVarOperationPartners objCVarOperationShipperPartner = new CVarOperationPartners();
                    objCVarOperationShipperPartner.OperationID = objCOperations.lstCVarOperations[0].ID;
                    objCVarOperationShipperPartner.OperationPartnerTypeID = 1; // export or domestic (shipper)
                    objCVarOperationShipperPartner.CustomerID = int.Parse(insertOperationData.pShipperID);
                    objCVarOperationShipperPartner.ContactID = (objCContacts.lstCVarContacts.Count > 0 ? objCContacts.lstCVarContacts[0].ID : 0);
                    objCVarOperationShipperPartner.IsOperationClient = false;
                    objCVarOperationShipperPartner.CreatorUserID = objCVarOperationShipperPartner.ModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarOperationShipperPartner.CreationDate = objCVarOperationShipperPartner.ModificationDate = DateTime.Now;
                    objCOperationPartners.lstCVarOperationPartners.Add(objCVarOperationShipperPartner);

                    objCContacts.GetList(" WHERE PartnerTypeID = 1 AND PartnerID = " + insertOperationData.pNotifyID.ToString());

                    CVarOperationPartners objCVarOperationNotifyPartner = new CVarOperationPartners();
                    objCVarOperationNotifyPartner.OperationID = objCOperations.lstCVarOperations[0].ID;
                    objCVarOperationNotifyPartner.OperationPartnerTypeID = 4;//Notify1
                    objCVarOperationNotifyPartner.CustomerID = Int32.Parse(insertOperationData.pNotifyID); // it will be set as null in DB
                    objCVarOperationNotifyPartner.ContactID = (objCContacts.lstCVarContacts.Count > 0 ? objCContacts.lstCVarContacts[0].ID : 0);
                    objCVarOperationNotifyPartner.IsOperationClient = false;
                    objCVarOperationNotifyPartner.CreatorUserID = objCVarOperationNotifyPartner.ModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarOperationNotifyPartner.CreationDate = objCVarOperationNotifyPartner.ModificationDate = DateTime.Now;
                    objCOperationPartners.lstCVarOperationPartners.Add(objCVarOperationNotifyPartner);

                    if (Int32.Parse(insertOperationData.pShippingLineID) > 0)
                    {
                        objCContacts.GetList(" WHERE PartnerTypeID = " + constShippingLineOperationPartnerTypeID + " AND PartnerID = " + insertOperationData.pNotifyID.ToString());
                        CVarOperationPartners objCVarOperationShippingLinePartner = new CVarOperationPartners();
                        objCVarOperationShippingLinePartner.OperationID = objCOperations.lstCVarOperations[0].ID;
                        objCVarOperationShippingLinePartner.OperationPartnerTypeID = constShippingLineOperationPartnerTypeID;
                        objCVarOperationShippingLinePartner.ShippingLineID = Int32.Parse(insertOperationData.pShippingLineID); // it will be set as null in DB
                        objCVarOperationShippingLinePartner.ContactID = (objCContacts.lstCVarContacts.Count > 0 ? objCContacts.lstCVarContacts[0].ID : 0);
                        objCVarOperationShippingLinePartner.IsOperationClient = false;
                        objCVarOperationShippingLinePartner.CreatorUserID = objCVarOperationShippingLinePartner.ModificatorUserID = WebSecurity.CurrentUserId;
                        objCVarOperationShippingLinePartner.CreationDate = objCVarOperationShippingLinePartner.ModificationDate = DateTime.Now;
                        objCOperationPartners.lstCVarOperationPartners.Add(objCVarOperationShippingLinePartner);
                    }

                    if (Int32.Parse(insertOperationData.pAirlineID) > 0)
                    {
                        objCContacts.GetList(" WHERE PartnerTypeID = " + constAirineOperationPartnerTypeID + " AND PartnerID = " + insertOperationData.pNotifyID.ToString());
                        CVarOperationPartners objCVarOperationAirlinePartner = new CVarOperationPartners();
                        objCVarOperationAirlinePartner.OperationID = objCOperations.lstCVarOperations[0].ID;
                        objCVarOperationAirlinePartner.OperationPartnerTypeID = constAirineOperationPartnerTypeID;
                        objCVarOperationAirlinePartner.AirlineID = Int32.Parse(insertOperationData.pAirlineID); // it will be set as null in DB
                        objCVarOperationAirlinePartner.ContactID = (objCContacts.lstCVarContacts.Count > 0 ? objCContacts.lstCVarContacts[0].ID : 0);
                        objCVarOperationAirlinePartner.IsOperationClient = false;
                        objCVarOperationAirlinePartner.CreatorUserID = objCVarOperationAirlinePartner.ModificatorUserID = WebSecurity.CurrentUserId;
                        objCVarOperationAirlinePartner.CreationDate = objCVarOperationAirlinePartner.ModificationDate = DateTime.Now;
                        objCOperationPartners.lstCVarOperationPartners.Add(objCVarOperationAirlinePartner);
                    }

                    if (Int32.Parse(insertOperationData.pTruckerID) > 0)
                    {
                        objCContacts.GetList(" WHERE PartnerTypeID = " + constTruckerOperationPartnerTypeID + " AND PartnerID = " + insertOperationData.pNotifyID.ToString());
                        CVarOperationPartners objCVarOperationTruckerPartner = new CVarOperationPartners();
                        objCVarOperationTruckerPartner.OperationID = objCOperations.lstCVarOperations[0].ID;
                        objCVarOperationTruckerPartner.OperationPartnerTypeID = constTruckerOperationPartnerTypeID;
                        objCVarOperationTruckerPartner.TruckerID = Int32.Parse(insertOperationData.pTruckerID); // it will be set as null in DB
                        objCVarOperationTruckerPartner.ContactID = (objCContacts.lstCVarContacts.Count > 0 ? objCContacts.lstCVarContacts[0].ID : 0);
                        objCVarOperationTruckerPartner.IsOperationClient = false;
                        objCVarOperationTruckerPartner.CreatorUserID = objCVarOperationTruckerPartner.ModificatorUserID = WebSecurity.CurrentUserId;
                        objCVarOperationTruckerPartner.CreationDate = objCVarOperationTruckerPartner.ModificationDate = DateTime.Now;
                        objCOperationPartners.lstCVarOperationPartners.Add(objCVarOperationTruckerPartner);
                    }

                    checkException = objCOperationPartners.SaveMethod(objCOperationPartners.lstCVarOperationPartners);
                    OperationPartners = objCOperationPartners.lstCVarOperationPartners[0].ID;

                    #endregion

                    //COPY Routings To Routings
                    //MainCarraige has ID = 30
                    #region Copy Operation Routings (at this point its just Main Carraige)
                    CVarRoutings objCVarRoutings = new CVarRoutings();

                    objCVarRoutings.OperationID = objCOperations.lstCVarOperations[0].ID;
                    objCVarRoutings.TransportType = objCOperations.lstCVarOperations[0].TransportType;
                    objCVarRoutings.TransportIconName = objCOperations.lstCVarOperations[0].TransportIconName;
                    objCVarRoutings.TransportIconStyle = objCOperations.lstCVarOperations[0].TransportIconStyle;
                    objCVarRoutings.RoutingTypeID = MainCarraigeRoutingTypeID; //Main Carraige
                    objCVarRoutings.POLCountryID = objCOperations.lstCVarOperations[0].POLCountryID;
                    objCVarRoutings.POL = objCOperations.lstCVarOperations[0].POL;
                    objCVarRoutings.PODCountryID = objCOperations.lstCVarOperations[0].PODCountryID;
                    objCVarRoutings.POD = objCOperations.lstCVarOperations[0].POD;
                    objCVarRoutings.ETAPOLDate = DateTime.Parse("01-01-1900");
                    objCVarRoutings.ATAPOLDate = DateTime.Parse("01-01-1900");
                    //objCVarRoutings.ActualDeparture = DateTime.ParseExact(insertOperationData.pExpectedDeparture, "dd/MM/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);
                    objCVarRoutings.ExpectedDeparture = DateTime.ParseExact(insertOperationData.pExpectedDeparture, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    objCVarRoutings.ActualDeparture = DateTime.Parse("01-01-1900");
                    objCVarRoutings.ExpectedArrival = DateTime.ParseExact(insertOperationData.pExpectedArrival, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    objCVarRoutings.ActualArrival = DateTime.Parse("01-01-1900");
                    objCVarRoutings.VesselID = insertOperationData.pVesselID;
                    objCVarRoutings.VoyageOrTruckNumber = insertOperationData.pVoyageOrTruckNumber;
                    objCVarRoutings.RoadNumber = "0";
                    objCVarRoutings.DeliveryOrderNumber = "0";
                    objCVarRoutings.WareHouse = "0";
                    objCVarRoutings.WareHouseLocation = "0";
                    objCVarRoutings.Notes = "";

                    if (insertOperationData.pTransportType == "1") //Ocean
                    {
                        objCVarRoutings.ShippingLineID = objCOperations.lstCVarOperations[0].ShippingLineID;
                        objCVarRoutings.AirlineID = 0;
                        objCVarRoutings.TruckerID = 0;
                    }
                    else if (insertOperationData.pTransportType == "2") //Air
                    {
                        objCVarRoutings.ShippingLineID = 0;
                        objCVarRoutings.AirlineID = objCOperations.lstCVarOperations[0].AirlineID;
                        objCVarRoutings.TruckerID = 0;
                    }
                    else //Inland , TransportType = 3
                    {
                        objCVarRoutings.ShippingLineID = 0;
                        objCVarRoutings.AirlineID = 0;
                        objCVarRoutings.TruckerID = objCOperations.lstCVarOperations[0].TruckerID;
                    }

                    objCVarRoutings.GensetSupplierID = 0;
                    objCVarRoutings.CCAID = 0;
                    objCVarRoutings.Quantity = "0";
                    objCVarRoutings.ContactPerson = "0";
                    objCVarRoutings.PickupAddress = "0";
                    objCVarRoutings.DeliveryAddress = "0";
                    objCVarRoutings.GateInPortID = 0;
                    objCVarRoutings.GateOutPortID = 0;
                    objCVarRoutings.GateInDate = DateTime.Parse("01/01/1900");

                    #region TransportOrder
                    objCVarRoutings.CustomerID = 0;
                    objCVarRoutings.SubContractedCustomerID = 0;
                    objCVarRoutings.Cost = 0;
                    objCVarRoutings.Sale = 0;
                    objCVarRoutings.IsFleet = false;
                    objCVarRoutings.CommodityID = 0;
                    objCVarRoutings.LoadingDate = DateTime.Parse("01/01/1900");
                    objCVarRoutings.LoadingReference = "0";
                    objCVarRoutings.UnloadingDate = DateTime.Parse("01/01/1900");
                    objCVarRoutings.UnloadingReference = "0";
                    objCVarRoutings.UnloadingTime = "0";
                    #endregion TransportOrder

                    objCVarRoutings.GateOutDate = DateTime.Parse("01/01/1900");
                    objCVarRoutings.StuffingDate = DateTime.Parse("01/01/1900");
                    objCVarRoutings.DeliveryDate = DateTime.Parse("01/01/1900");
                    objCVarRoutings.BookingNumber = "0";
                    objCVarRoutings.Delays = "0";
                    objCVarRoutings.DriverName = "0";
                    objCVarRoutings.DriverPhones = "0";
                    objCVarRoutings.PowerFromGateInTillActualSailing = "0";
                    objCVarRoutings.ContactPersonPhones = "0";
                    objCVarRoutings.LoadingTime = "0";

                    #region CustomsClearance
                    objCVarRoutings.CCAFreight = 0;
                    objCVarRoutings.CCAFOB = 0;
                    objCVarRoutings.CCACFValue = 0;
                    objCVarRoutings.CCAInvoiceNumber = "0";

                    objCVarRoutings.CCAInsurance = "0";
                    objCVarRoutings.CCADischargeValue = "0";
                    objCVarRoutings.CCAAcceptedValue = "0";
                    objCVarRoutings.CCAImportValue = "0";
                    objCVarRoutings.CCADocumentReceiveDate = DateTime.Parse("01/01/1900");
                    objCVarRoutings.CCAExchangeRate = "0";
                    objCVarRoutings.CCAVATCertificateNumber = "0";
                    objCVarRoutings.CCAVATCertificateValue = "0";
                    objCVarRoutings.CCACommercialProfitCertificateNumber = "0";
                    objCVarRoutings.CCAOthers = "0";
                    objCVarRoutings.CCASpendDate = DateTime.Parse("01/01/1900");
                    objCVarRoutings.OffloadingDate = DateTime.Parse("01/01/1900");

                    objCVarRoutings.CertificateNumber = "0";
                    objCVarRoutings.CertificateValue = "0";
                    objCVarRoutings.CertificateDate = DateTime.Parse("01/01/1900");
                    objCVarRoutings.QasimaNumber = "0";
                    objCVarRoutings.QasimaDate = DateTime.Parse("01/01/1900");
                    objCVarRoutings.Match = false;
                    objCVarRoutings.SalesDateReceived = DateTime.Parse("01/01/1900");
                    objCVarRoutings.CommerceDateReceived = DateTime.Parse("01/01/1900");
                    objCVarRoutings.InspectionDateReceived = DateTime.Parse("01/01/1900");
                    objCVarRoutings.FinishDateReceived = DateTime.Parse("01/01/1900");
                    objCVarRoutings.SalesDateDelivered = DateTime.Parse("01/01/1900");
                    objCVarRoutings.CommerceDateDelivered = DateTime.Parse("01/01/1900");
                    objCVarRoutings.InspectionDateDelivered = DateTime.Parse("01/01/1900");
                    objCVarRoutings.FinishDateDelivered = DateTime.Parse("01/01/1900");
                    objCVarRoutings.CCAllowTemporaryDelivered = DateTime.Parse("01/01/1900");
                    objCVarRoutings.CCAllowTemporaryReceived = DateTime.Parse("01/01/1900");
                    objCVarRoutings.CCDropBackDelivered = DateTime.Parse("01/01/1900");
                    objCVarRoutings.CCDropBackReceived = DateTime.Parse("01/01/1900");
                    objCVarRoutings.CC_ClearanceTypeID = 0;
                    objCVarRoutings.CC_CustomItemsID = 0;
                    objCVarRoutings.CCReleaseNo = "0";
                    #endregion CustomsClearance
                    objCVarRoutings.BillNumber = "0";
                    objCVarRoutings.TruckingOrderCode = "0";

                    objCVarRoutings.CreatorUserID = objCVarRoutings.ModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarRoutings.ModificationDate = objCVarRoutings.CreationDate = DateTime.Now;
                    
                    CRoutings objCRoutings = new CRoutings();
                    objCRoutings.lstCVarRoutings.Add(objCVarRoutings);
                    objCRoutings.SaveMethod(objCRoutings.lstCVarRoutings);
                    RoutingID = objCVarRoutings.ID;
                    #endregion

                    #region AddContainers if exists
                    if (insertOperationData.pContainerTypeID > 0 && insertOperationData.pNumberOfContainers > 0)
                    {
                        COperationContainersAndPackages objCOperationContainersAndPackages = new COperationContainersAndPackages();
                        for (int z = 0; z < insertOperationData.pNumberOfContainers; z++)
                        {
                            CVarOperationContainersAndPackages objCVarOperationContainersAndPackages = new CVarOperationContainersAndPackages();

                            objCVarOperationContainersAndPackages.OperationID = objCOperations.lstCVarOperations[0].ID;
                            objCVarOperationContainersAndPackages.ContainerTypeID = insertOperationData.pContainerTypeID;
                            //objCVarOperationContainersAndPackages.Length = pLength;
                            //objCVarOperationContainersAndPackages.Width = pWidth;
                            //objCVarOperationContainersAndPackages.Height = pHeight;
                            //objCVarOperationContainersAndPackages.Volume = Decimal.Parse(insertOperationData.pVolume);
                            //objCVarOperationContainersAndPackages.VolumetricWeight = pVolumetricWeight;
                            //objCVarOperationContainersAndPackages.NetWeight = Decimal.Parse(insertOperationData.pNetWeight);
                            //objCVarOperationContainersAndPackages.GrossWeight = Decimal.Parse(insertOperationData.pGrossWeight);
                            //objCVarOperationContainersAndPackages.ChargeableWeight = pChargeableWeight;
                            //if (int.Parse(insertOperationData.pShipmentType) == 1 || int.Parse(insertOperationData.pShipmentType) == 3)//FCL or FTL (i.e. container load)
                            //{
                            //    objCVarOperationContainersAndPackages.NumberOfPackagesOnContainer = int.Parse(insertOperationData.pNumberOfPackages);
                            //    objCVarOperationContainersAndPackages.PackageTypeIDOnContainer = int.Parse(insertOperationData.pPackageTypeID);
                            //}
                            //else //air or (lcl or ltl)
                            //{
                            //    objCVarOperationContainersAndPackages.Quantity = int.Parse(insertOperationData.pNumberOfPackages);
                            //    objCVarOperationContainersAndPackages.PackageTypeID = int.Parse(insertOperationData.pPackageTypeID);
                            //}

                            objCVarOperationContainersAndPackages.ContainerNumber = "0";
                            objCVarOperationContainersAndPackages.CarrierSeal = "0";
                            objCVarOperationContainersAndPackages.ShipperSeal = "0";
                            //objCVarOperationContainersAndPackages.TareWeight = pTareWeight;
                            //objCVarOperationContainersAndPackages.IsReefer = pIsReefer;
                            //objCVarOperationContainersAndPackages.IsNOR = pIsNOR;
                            //objCVarOperationContainersAndPackages.MinTemp = pMinTemp;
                            //objCVarOperationContainersAndPackages.MaxTemp = pMaxTemp;
                            //objCVarOperationContainersAndPackages.IsIMO = pIsIMO;
                            //objCVarOperationContainersAndPackages.IMOClass = pIMOClass;
                            //objCVarOperationContainersAndPackages.UNNumber = pUNNumber;
                            //objCVarOperationContainersAndPackages.FlashPoint = pFlashPoint;
                            objCVarOperationContainersAndPackages.DescriptionOfGoods = "0"; // insertOperationData.pDescriptionOfGoods;
                            objCVarOperationContainersAndPackages.MarksAndNumbers = "0";
                            objCVarOperationContainersAndPackages.LotNumber = "0";
                            //objCVarOperationContainersAndPackages.PackageTypeIDOnContainer = pPackageTypeIDOnContainer;
                            //objCVarOperationContainersAndPackages.NumberOfPackagesOnContainer = pNumberOfPackagesOnContainer;
                            #region ContainerTracking
                            objCVarOperationContainersAndPackages.GateOutPortID = 0;
                            objCVarOperationContainersAndPackages.GateInPortID = 0;
                            objCVarOperationContainersAndPackages.GateOutDate = DateTime.Parse("01/01/1900");
                            objCVarOperationContainersAndPackages.StuffingDate = DateTime.Parse("01/01/1900");
                            objCVarOperationContainersAndPackages.LoadingDate = DateTime.Parse("01/01/1900");
                            objCVarOperationContainersAndPackages.GateOutAndLoadingDatesDifference = 0;
                            objCVarOperationContainersAndPackages.Factory = "0";
                            objCVarOperationContainersAndPackages.ExportBLNumber = "0";
                            objCVarOperationContainersAndPackages.ImportBLNumber = "0";
                            objCVarOperationContainersAndPackages.IsLoaded = false;
                            objCVarOperationContainersAndPackages.IsTracked = false;
                            objCVarOperationContainersAndPackages.IsOwnedByCompany = false;
                            objCVarOperationContainersAndPackages.TrailerID = 0;
                            objCVarOperationContainersAndPackages.DriverID = 0;
                            objCVarOperationContainersAndPackages.DriverAssistantID = 0;
                            objCVarOperationContainersAndPackages.SupplierTrailerName = "0";
                            objCVarOperationContainersAndPackages.SupplierDriverName = "0";
                            objCVarOperationContainersAndPackages.SupplierDriverAssistantName = "0";
                            #endregion ContainerTracking
                            #region AirAgents columns
                            objCVarOperationContainersAndPackages.Rate = 0;
                            objCVarOperationContainersAndPackages.IsAsAgreed = false;
                            objCVarOperationContainersAndPackages.IsMinimum = false;
                            objCVarOperationContainersAndPackages.WeightUnit = "0";
                            objCVarOperationContainersAndPackages.RateClass = "0";
                            #endregion AirAgents columns

                            #region Tank
                            objCVarOperationContainersAndPackages.TankOrFlexiNumber = "0";
                            objCVarOperationContainersAndPackages.OperatorID = 0;

                            objCVarOperationContainersAndPackages.IsFull = false;
                            objCVarOperationContainersAndPackages.ExitDate = DateTime.Parse("01/01/1900");
                            objCVarOperationContainersAndPackages.ReturnDate = DateTime.Parse("01/01/1900");
                            objCVarOperationContainersAndPackages.FreeDays = 0;
                            objCVarOperationContainersAndPackages.DayValue = 0;
                            #endregion Tank
                            #region Yard
                            objCVarOperationContainersAndPackages.YardEIRNumber = 0;
                            objCVarOperationContainersAndPackages.YardEIRNumberOut = 0;
                            objCVarOperationContainersAndPackages.YardInDate = DateTime.Parse("01/01/1900");
                            objCVarOperationContainersAndPackages.YardInTime = 0;
                            objCVarOperationContainersAndPackages.YardOutDate = DateTime.Parse("01/01/1900");
                            objCVarOperationContainersAndPackages.YardOutTime = 0;
                            objCVarOperationContainersAndPackages.YardLocationID = 0;
                            objCVarOperationContainersAndPackages.YardIsIn = 0;
                            #endregion Yard

                            objCVarOperationContainersAndPackages.CreatorUserID = objCVarOperationContainersAndPackages.ModificatorUserID = WebSecurity.CurrentUserId;
                            objCVarOperationContainersAndPackages.CreationDate = objCVarOperationContainersAndPackages.ModificationDate = DateTime.Now;

                            objCOperationContainersAndPackages.lstCVarOperationContainersAndPackages.Add(objCVarOperationContainersAndPackages);
                        }
                        checkException = objCOperationContainersAndPackages.SaveMethod(objCOperationContainersAndPackages.lstCVarOperationContainersAndPackages);
                        OperationContainersAndPackagesID = objCOperationContainersAndPackages.lstCVarOperationContainersAndPackages[0].ID;
                    }
                    if (insertOperationData.pContainerTypeID2 > 0 && insertOperationData.pNumberOfContainers2 > 0)
                    {
                        COperationContainersAndPackages objCOperationContainersAndPackages = new COperationContainersAndPackages();
                        for (int z = 0; z < insertOperationData.pNumberOfContainers2; z++)
                        {
                            CVarOperationContainersAndPackages objCVarOperationContainersAndPackages = new CVarOperationContainersAndPackages();

                            objCVarOperationContainersAndPackages.OperationID = objCOperations.lstCVarOperations[0].ID;
                            objCVarOperationContainersAndPackages.ContainerTypeID = insertOperationData.pContainerTypeID2;
                            //objCVarOperationContainersAndPackages.Length = pLength;
                            //objCVarOperationContainersAndPackages.Width = pWidth;
                            //objCVarOperationContainersAndPackages.Height = pHeight;
                            //objCVarOperationContainersAndPackages.Volume = Decimal.Parse(insertOperationData.pVolume);
                            //objCVarOperationContainersAndPackages.VolumetricWeight = pVolumetricWeight;
                            //objCVarOperationContainersAndPackages.NetWeight = Decimal.Parse(insertOperationData.pNetWeight);
                            //objCVarOperationContainersAndPackages.GrossWeight = Decimal.Parse(insertOperationData.pGrossWeight);
                            //objCVarOperationContainersAndPackages.ChargeableWeight = pChargeableWeight;
                            //if (int.Parse(insertOperationData.pShipmentType) == 1 || int.Parse(insertOperationData.pShipmentType) == 3)//FCL or FTL (i.e. container load)
                            //{
                            //    objCVarOperationContainersAndPackages.NumberOfPackagesOnContainer = int.Parse(insertOperationData.pNumberOfPackages);
                            //    objCVarOperationContainersAndPackages.PackageTypeIDOnContainer = int.Parse(insertOperationData.pPackageTypeID);
                            //}
                            //else //air or (lcl or ltl)
                            //{
                            //    objCVarOperationContainersAndPackages.Quantity = int.Parse(insertOperationData.pNumberOfPackages);
                            //    objCVarOperationContainersAndPackages.PackageTypeID = int.Parse(insertOperationData.pPackageTypeID);
                            //}

                            objCVarOperationContainersAndPackages.ContainerNumber = "0";
                            objCVarOperationContainersAndPackages.CarrierSeal = "0";
                            objCVarOperationContainersAndPackages.ShipperSeal = "0";
                            //objCVarOperationContainersAndPackages.TareWeight = pTareWeight;
                            //objCVarOperationContainersAndPackages.IsReefer = pIsReefer;
                            //objCVarOperationContainersAndPackages.IsNOR = pIsNOR;
                            //objCVarOperationContainersAndPackages.MinTemp = pMinTemp;
                            //objCVarOperationContainersAndPackages.MaxTemp = pMaxTemp;
                            //objCVarOperationContainersAndPackages.IsIMO = pIsIMO;
                            //objCVarOperationContainersAndPackages.IMOClass = pIMOClass;
                            //objCVarOperationContainersAndPackages.UNNumber = pUNNumber;
                            //objCVarOperationContainersAndPackages.FlashPoint = pFlashPoint;
                            objCVarOperationContainersAndPackages.DescriptionOfGoods = "0"; // insertOperationData.pDescriptionOfGoods;
                            objCVarOperationContainersAndPackages.MarksAndNumbers = "0";
                            objCVarOperationContainersAndPackages.LotNumber = "0";
                            //objCVarOperationContainersAndPackages.PackageTypeIDOnContainer = pPackageTypeIDOnContainer;
                            //objCVarOperationContainersAndPackages.NumberOfPackagesOnContainer = pNumberOfPackagesOnContainer;
                            #region ContainerTracking
                            objCVarOperationContainersAndPackages.GateOutPortID = 0;
                            objCVarOperationContainersAndPackages.GateInPortID = 0;
                            objCVarOperationContainersAndPackages.GateOutDate = DateTime.Parse("01/01/1900");
                            objCVarOperationContainersAndPackages.StuffingDate = DateTime.Parse("01/01/1900");
                            objCVarOperationContainersAndPackages.LoadingDate = DateTime.Parse("01/01/1900");
                            objCVarOperationContainersAndPackages.GateOutAndLoadingDatesDifference = 0;
                            objCVarOperationContainersAndPackages.Factory = "0";
                            objCVarOperationContainersAndPackages.ExportBLNumber = "0";
                            objCVarOperationContainersAndPackages.ImportBLNumber = "0";
                            objCVarOperationContainersAndPackages.IsLoaded = false;
                            objCVarOperationContainersAndPackages.IsTracked = false;
                            objCVarOperationContainersAndPackages.IsOwnedByCompany = false;
                            objCVarOperationContainersAndPackages.TrailerID = 0;
                            objCVarOperationContainersAndPackages.DriverID = 0;
                            objCVarOperationContainersAndPackages.DriverAssistantID = 0;
                            objCVarOperationContainersAndPackages.SupplierTrailerName = "0";
                            objCVarOperationContainersAndPackages.SupplierDriverName = "0";
                            objCVarOperationContainersAndPackages.SupplierDriverAssistantName = "0";
                            #endregion ContainerTracking
                            #region AirAgents columns
                            objCVarOperationContainersAndPackages.Rate = 0;
                            objCVarOperationContainersAndPackages.IsAsAgreed = false;
                            objCVarOperationContainersAndPackages.IsMinimum = false;
                            objCVarOperationContainersAndPackages.WeightUnit = "0";
                            objCVarOperationContainersAndPackages.RateClass = "0";
                            #endregion AirAgents columns

                            #region Tank
                            objCVarOperationContainersAndPackages.TankOrFlexiNumber = "0";
                            objCVarOperationContainersAndPackages.OperatorID = 0;

                            objCVarOperationContainersAndPackages.IsFull = false;
                            objCVarOperationContainersAndPackages.ExitDate = DateTime.Parse("01/01/1900");
                            objCVarOperationContainersAndPackages.ReturnDate = DateTime.Parse("01/01/1900");
                            objCVarOperationContainersAndPackages.FreeDays = 0;
                            objCVarOperationContainersAndPackages.DayValue = 0;
                            #endregion Tank
                            #region Yard
                            objCVarOperationContainersAndPackages.YardEIRNumber = 0;
                            objCVarOperationContainersAndPackages.YardEIRNumberOut = 0;
                            objCVarOperationContainersAndPackages.YardInDate = DateTime.Parse("01/01/1900");
                            objCVarOperationContainersAndPackages.YardInTime = 0;
                            objCVarOperationContainersAndPackages.YardOutDate = DateTime.Parse("01/01/1900");
                            objCVarOperationContainersAndPackages.YardOutTime = 0;
                            objCVarOperationContainersAndPackages.YardLocationID = 0;
                            objCVarOperationContainersAndPackages.YardIsIn = 0;
                            #endregion Yard

                            objCVarOperationContainersAndPackages.CreatorUserID = objCVarOperationContainersAndPackages.ModificatorUserID = WebSecurity.CurrentUserId;
                            objCVarOperationContainersAndPackages.CreationDate = objCVarOperationContainersAndPackages.ModificationDate = DateTime.Now;

                            objCOperationContainersAndPackages.lstCVarOperationContainersAndPackages.Add(objCVarOperationContainersAndPackages);
                        }
                        checkException = objCOperationContainersAndPackages.SaveMethod(objCOperationContainersAndPackages.lstCVarOperationContainersAndPackages);
                        OperationContainersAndPackagesID = objCOperationContainersAndPackages.lstCVarOperationContainersAndPackages[0].ID;
                    }
                    if (insertOperationData.pContainerTypeID3 > 0 && insertOperationData.pNumberOfContainers3 > 0)
                    {
                        COperationContainersAndPackages objCOperationContainersAndPackages = new COperationContainersAndPackages();
                        for (int z = 0; z < insertOperationData.pNumberOfContainers3; z++)
                        {
                            CVarOperationContainersAndPackages objCVarOperationContainersAndPackages = new CVarOperationContainersAndPackages();

                            objCVarOperationContainersAndPackages.OperationID = objCOperations.lstCVarOperations[0].ID;
                            objCVarOperationContainersAndPackages.ContainerTypeID = insertOperationData.pContainerTypeID3;
                            //objCVarOperationContainersAndPackages.Length = pLength;
                            //objCVarOperationContainersAndPackages.Width = pWidth;
                            //objCVarOperationContainersAndPackages.Height = pHeight;
                            //objCVarOperationContainersAndPackages.Volume = Decimal.Parse(insertOperationData.pVolume);
                            //objCVarOperationContainersAndPackages.VolumetricWeight = pVolumetricWeight;
                            //objCVarOperationContainersAndPackages.NetWeight = Decimal.Parse(insertOperationData.pNetWeight);
                            //objCVarOperationContainersAndPackages.GrossWeight = Decimal.Parse(insertOperationData.pGrossWeight);
                            //objCVarOperationContainersAndPackages.ChargeableWeight = pChargeableWeight;
                            //if (int.Parse(insertOperationData.pShipmentType) == 1 || int.Parse(insertOperationData.pShipmentType) == 3)//FCL or FTL (i.e. container load)
                            //{
                            //    objCVarOperationContainersAndPackages.NumberOfPackagesOnContainer = int.Parse(insertOperationData.pNumberOfPackages);
                            //    objCVarOperationContainersAndPackages.PackageTypeIDOnContainer = int.Parse(insertOperationData.pPackageTypeID);
                            //}
                            //else //air or (lcl or ltl)
                            //{
                            //    objCVarOperationContainersAndPackages.Quantity = int.Parse(insertOperationData.pNumberOfPackages);
                            //    objCVarOperationContainersAndPackages.PackageTypeID = int.Parse(insertOperationData.pPackageTypeID);
                            //}

                            objCVarOperationContainersAndPackages.ContainerNumber = "0";
                            objCVarOperationContainersAndPackages.CarrierSeal = "0";
                            objCVarOperationContainersAndPackages.ShipperSeal = "0";
                            //objCVarOperationContainersAndPackages.TareWeight = pTareWeight;
                            //objCVarOperationContainersAndPackages.IsReefer = pIsReefer;
                            //objCVarOperationContainersAndPackages.IsNOR = pIsNOR;
                            //objCVarOperationContainersAndPackages.MinTemp = pMinTemp;
                            //objCVarOperationContainersAndPackages.MaxTemp = pMaxTemp;
                            //objCVarOperationContainersAndPackages.IsIMO = pIsIMO;
                            //objCVarOperationContainersAndPackages.IMOClass = pIMOClass;
                            //objCVarOperationContainersAndPackages.UNNumber = pUNNumber;
                            //objCVarOperationContainersAndPackages.FlashPoint = pFlashPoint;
                            objCVarOperationContainersAndPackages.DescriptionOfGoods = "0"; // insertOperationData.pDescriptionOfGoods;
                            objCVarOperationContainersAndPackages.MarksAndNumbers = "0";
                            objCVarOperationContainersAndPackages.LotNumber = "0";
                            //objCVarOperationContainersAndPackages.PackageTypeIDOnContainer = pPackageTypeIDOnContainer;
                            //objCVarOperationContainersAndPackages.NumberOfPackagesOnContainer = pNumberOfPackagesOnContainer;
                            #region ContainerTracking
                            objCVarOperationContainersAndPackages.GateOutPortID = 0;
                            objCVarOperationContainersAndPackages.GateInPortID = 0;
                            objCVarOperationContainersAndPackages.GateOutDate = DateTime.Parse("01/01/1900");
                            objCVarOperationContainersAndPackages.StuffingDate = DateTime.Parse("01/01/1900");
                            objCVarOperationContainersAndPackages.LoadingDate = DateTime.Parse("01/01/1900");
                            objCVarOperationContainersAndPackages.GateOutAndLoadingDatesDifference = 0;
                            objCVarOperationContainersAndPackages.Factory = "0";
                            objCVarOperationContainersAndPackages.ExportBLNumber = "0";
                            objCVarOperationContainersAndPackages.ImportBLNumber = "0";
                            objCVarOperationContainersAndPackages.IsLoaded = false;
                            objCVarOperationContainersAndPackages.IsTracked = false;
                            objCVarOperationContainersAndPackages.IsOwnedByCompany = false;
                            objCVarOperationContainersAndPackages.TrailerID = 0;
                            objCVarOperationContainersAndPackages.DriverID = 0;
                            objCVarOperationContainersAndPackages.DriverAssistantID = 0;
                            objCVarOperationContainersAndPackages.SupplierTrailerName = "0";
                            objCVarOperationContainersAndPackages.SupplierDriverName = "0";
                            objCVarOperationContainersAndPackages.SupplierDriverAssistantName = "0";
                            #endregion ContainerTracking
                            #region AirAgents columns
                            objCVarOperationContainersAndPackages.Rate = 0;
                            objCVarOperationContainersAndPackages.IsAsAgreed = false;
                            objCVarOperationContainersAndPackages.IsMinimum = false;
                            objCVarOperationContainersAndPackages.WeightUnit = "0";
                            objCVarOperationContainersAndPackages.RateClass = "0";
                            #endregion AirAgents columns

                            #region Tank
                            objCVarOperationContainersAndPackages.TankOrFlexiNumber = "0";
                            objCVarOperationContainersAndPackages.OperatorID = 0;

                            objCVarOperationContainersAndPackages.IsFull = false;
                            objCVarOperationContainersAndPackages.ExitDate = DateTime.Parse("01/01/1900");
                            objCVarOperationContainersAndPackages.ReturnDate = DateTime.Parse("01/01/1900");
                            objCVarOperationContainersAndPackages.FreeDays = 0;
                            objCVarOperationContainersAndPackages.DayValue = 0;
                            #endregion Tank
                            #region Yard
                            objCVarOperationContainersAndPackages.YardEIRNumber = 0;
                            objCVarOperationContainersAndPackages.YardEIRNumberOut = 0;
                            objCVarOperationContainersAndPackages.YardInDate = DateTime.Parse("01/01/1900");
                            objCVarOperationContainersAndPackages.YardInTime = 0;
                            objCVarOperationContainersAndPackages.YardOutDate = DateTime.Parse("01/01/1900");
                            objCVarOperationContainersAndPackages.YardOutTime = 0;
                            objCVarOperationContainersAndPackages.YardLocationID = 0;
                            objCVarOperationContainersAndPackages.YardIsIn = 0;
                            #endregion Yard

                            objCVarOperationContainersAndPackages.CreatorUserID = objCVarOperationContainersAndPackages.ModificatorUserID = WebSecurity.CurrentUserId;
                            objCVarOperationContainersAndPackages.CreationDate = objCVarOperationContainersAndPackages.ModificationDate = DateTime.Now;

                            objCOperationContainersAndPackages.lstCVarOperationContainersAndPackages.Add(objCVarOperationContainersAndPackages);
                        }
                        checkException = objCOperationContainersAndPackages.SaveMethod(objCOperationContainersAndPackages.lstCVarOperationContainersAndPackages);
                        OperationContainersAndPackagesID = objCOperationContainersAndPackages.lstCVarOperationContainersAndPackages[0].ID;
                    }
                    #endregion

                    #region Copy default AirLineChargeTypes
                    if (insertOperationData.pIsAWB)
                    {
                        CAirLineChargeTypes objCAirLineChargeTypes = new CAirLineChargeTypes();
                        objCAirLineChargeTypes.GetList("WHERE AirLineId=" + insertOperationData.pAirlineID + " AND IsDefault=1");
                        for (int i = 0; i < objCAirLineChargeTypes.lstCVarAirLineChargeTypes.Count; i++)
                        {
                            CVarPayables objCVarPayables = new CVarPayables();
                            objCVarPayables.OperationID = objCVarOperations.ID;
                            objCVarPayables.ChargeTypeID = objCAirLineChargeTypes.lstCVarAirLineChargeTypes[i].ChargeTypeID;
                            objCVarPayables.POrC = objCVarOperations.POrC;
                            objCVarPayables.SupplierOperationPartnerID = 0;
                            objCVarPayables.Quantity = 1;
                            objCVarPayables.CostPrice = 0;
                            objCVarPayables.AmountWithoutVAT = 0;
                            objCVarPayables.TaxTypeID = 0;
                            objCVarPayables.TaxPercentage = 0;
                            objCVarPayables.TaxAmount = 0;
                            objCVarPayables.DiscountTypeID = 0;
                            objCVarPayables.DiscountPercentage = 0;
                            objCVarPayables.DiscountAmount = 0;
                            objCVarPayables.CostAmount = 0;
                            objCVarPayables.PaidAmount = 0;
                            objCVarPayables.RemainingAmount = 0;
                            objCVarPayables.InitialSalePrice = 0;
                            objCVarPayables.SupplierInvoiceNo = "0";
                            objCVarPayables.EntryDate = DateTime.Now;
                            objCVarPayables.BillID = 0;

                            objCVarPayables.IssueDate = DateTime.Now;
                            objCVarPayables.OperationContainersAndPackagesID = 0;

                            objCVarPayables.ExchangeRate = 1;
                            objCVarPayables.CurrencyID = objCvwDefaults.lstCVarvwDefaults[0].CurrencyID;
                            objCVarPayables.GeneratingQRID = 0;
                            objCVarPayables.Notes = "0";
                            objCVarPayables.CustodyID = 0;
                            objCVarPayables.SupplierReceiptNo = "0";
                            objCVarPayables.AccNoteID = 0;
                            objCVarPayables.IsDeleted = false;
                            objCVarPayables.IsApproved = false;
                            objCVarPayables.ApprovingUserID = 0;
                            objCVarPayables.CreatorUserID = objCVarPayables.ModificatorUserID = WebSecurity.CurrentUserId;
                            objCVarPayables.CreationDate = objCVarPayables.ModificationDate = DateTime.Now;
                            objCVarPayables.JVID = 0;
                            objCVarPayables.BillTo = 0;
                            objCVarPayables.ReceivableID = 0;
                            objCVarPayables.TaxAmount = 0;

                            CPayables objCPayables = new CPayables();
                            objCPayables.lstCVarPayables.Add(objCVarPayables);
                            objCPayables.SaveMethod(objCPayables.lstCVarPayables);
                            PayableID = objCVarPayables.ID;

                        } //for (int i = 0; i < objCAirLineChargeTypes.lstCVarAirLineChargeTypes.Count; i++)
                    }
                    #endregion Copy default AirLineChargeTypes
                }

                ////i cancelled it from here to save overload(i already check while uploading)
                //#region create Operations Folder to upload DocsIn
                //objCOperations.GetItem(objCOperations.lstCVarOperations[0].ID);

                ////create new directory
                ////string filePath = System.IO.Path.Combine(HttpRuntime.AppDomainAppPath, "DocsInFiles/" + objCOperations.lstCVarOperations[0].Code);
                //string strNewFolderPath = HttpContext.Current.Server.MapPath("~/") + "DocsInFiles/" + objCOperations.lstCVarOperations[0].Code;
                //if (!Directory.Exists(strNewFolderPath)) 
                //    Directory.CreateDirectory(strNewFolderPath);

                //#endregion create Operations Folder to upload DocsIn


                #region CreateCostCenter
                CSystemOptions objCSystemOptions = new CSystemOptions();
                objCSystemOptions.GetList("Where OptionID=94");
                if (objCSystemOptions.lstCVarSystemOptions.Count > 0 && objCSystemOptions.lstCVarSystemOptions[0].OptionValue
                  && objCOperations.lstCVarOperations[0].BLType != 2)
                {
                    string CostCenterNumberParent = "";
                    Int32 pID = 0;
                    //CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
                    CA_CostCenters objCA_CostCentersParent = new CA_CostCenters();
                    objCA_CostCentersParent.GetList("where ( CostCenterName=N'عمليات' or CostCenterName='Operations' ) and Parent_ID is null ");
                    if (objCA_CostCentersParent.lstCVarA_CostCenters.Count > 0)
                    {
                        pID = objCA_CostCentersParent.lstCVarA_CostCenters[0].ID;
                        CostCenterNumberParent = objCA_CostCentersParent.lstCVarA_CostCenters[0].RealCostCenterCode;
                    }

                    else
                    {

                        string pNewCodePartner = objCCustomizedDBCall.CallStringFunction("SELECT [dbo].A_CostCenters_GetCode(" + (pID == 0 ? "null" : pID.ToString()) + ") AS Code");


                        CVarA_CostCenters objCVarA_CostCenters = new CVarA_CostCenters();
                        objCVarA_CostCenters.CostCenterNumber = pNewCodePartner.PadRight(12, '0');
                        objCVarA_CostCenters.CostCenterName = "Operations";
                        objCVarA_CostCenters.Parent_ID = 0;
                        objCVarA_CostCenters.IsMain = true;
                        objCVarA_CostCenters.CCLevel = 1;
                        objCVarA_CostCenters.RealCostCenterCode = pNewCodePartner;
                        objCVarA_CostCenters.User_ID = WebSecurity.CurrentUserId;
                        objCVarA_CostCenters.Type_ID = 0;
                        objCVarA_CostCenters.IsClosed = false;
                        objCVarA_CostCenters.SubAccountGroupID = 0;
                        objCVarA_CostCenters.EmployeesCount = 0;
                        objCA_CostCentersParent.lstCVarA_CostCenters.Add(objCVarA_CostCenters);
                        checkException = objCA_CostCentersParent.SaveMethod(objCA_CostCentersParent.lstCVarA_CostCenters);

                        pID = objCA_CostCentersParent.lstCVarA_CostCenters[0].ID;
                        CostCenterNumberParent = objCA_CostCentersParent.lstCVarA_CostCenters[0].RealCostCenterCode;
                    }

                    CA_CostCenters objCA_CostCenters = new CA_CostCenters();
                    checkException = objCA_CostCenters.GetListPaging(1, 1, "WHERE ID = " + pID.ToString(), "ID", out _RowCount);
                    string pNewCode = objCCustomizedDBCall.CallStringFunction("SELECT [dbo].A_CostCenters_GetCode(" + (pID == 0 ? "null" : pID.ToString()) + ") AS Code");

                    CvwOperations objCvwOperations = new CvwOperations();
                    for (int k = 0; k < 2; k++)
                    {
                        objCvwOperations.GetList("WHERE ID=" + objCOperations.lstCVarOperations[0].ID);
                        if (objCvwOperations.lstCVarvwOperations[0].CodeSerial == 0)
                            k = 0;
                    }

                    string pNewCodeNew = CostCenterNumberParent + pNewCode;
                    CVarA_CostCenters objCVarA_CostCentersChild = new CVarA_CostCenters();
                    objCVarA_CostCentersChild.CostCenterNumber = pNewCodeNew.PadRight(12, '0'); ;
                    objCVarA_CostCentersChild.CostCenterName = objCvwOperations.lstCVarvwOperations[0].Code + " - " + objCvwOperations.lstCVarvwOperations[0].ClientName;
                    objCVarA_CostCentersChild.Parent_ID = pID;
                    objCVarA_CostCentersChild.IsMain = false;
                    objCVarA_CostCentersChild.CCLevel = 2;
                    objCVarA_CostCentersChild.RealCostCenterCode = pNewCodeNew;
                    objCVarA_CostCentersChild.User_ID = WebSecurity.CurrentUserId;
                    objCVarA_CostCentersChild.Type_ID = 0;
                    objCVarA_CostCentersChild.IsClosed = false;
                    objCVarA_CostCentersChild.SubAccountGroupID = 0;
                    objCVarA_CostCentersChild.EmployeesCount = 0;
                    objCA_CostCentersParent.lstCVarA_CostCenters.Add(objCVarA_CostCentersChild);
                    checkException = objCA_CostCentersParent.SaveMethod(objCA_CostCentersParent.lstCVarA_CostCenters);


                    //Link Oeation With CostCenter Add by ahmed maher
                    int _RowCount2 = 0;
                    objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount2); //i am sure i ve just one row isa
                    string CompanyName2 = objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName;
                    if (CompanyName2 == "BED")
                    {
                        CVarA_LinkOperationWithCostCenter objCVarA_LinkOperationWithCostCenter = new CVarA_LinkOperationWithCostCenter();

                        objCVarA_LinkOperationWithCostCenter.OperationID = objCOperations.lstCVarOperations[0].ID;
                        objCVarA_LinkOperationWithCostCenter.CostCenterID = objCVarA_CostCentersChild.ID;

                        CA_LinkOperationWithCostCenter objA_LinkOperationWithCostCenter = new CA_LinkOperationWithCostCenter();
                        objA_LinkOperationWithCostCenter.lstCVarA_LinkOperationWithCostCenter.Add(objCVarA_LinkOperationWithCostCenter);
                        checkException = objA_LinkOperationWithCostCenter.SaveMethod(objA_LinkOperationWithCostCenter.lstCVarA_LinkOperationWithCostCenter);

                    }

                }


                #endregion
            }


            string CompanyName = objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName;
            #region TaxLink
            if ((CompanyName == "CHM" || CompanyName == "OCE") && _MessageReturned == "")
            {
                COperationsTAX objCOperationsTax = new COperationsTAX();
                int _RowCount2 = 0;


                objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa

                CAirlinesTAX objCAirlinesTax = new CAirlinesTAX();
                if (int.Parse(insertOperationData.pAirlineID) > 0)
                {
                    objCAirlinesTax.GetList("WHERE ID=" + insertOperationData.pAirlineID);
                    insertOperationData.pMAWBSuffix = insertOperationData.pMasterBL;
                    insertOperationData.pMasterBL = objCAirlinesTax.lstCVarAirlinesTAX[0].Prefix + "-" + insertOperationData.pMasterBL;
                }

                COperationsTAX objCOperations_CheckUniquenessTax = new COperationsTAX();
                if (!objCvwDefaults.lstCVarvwDefaults[0].IsRepeatMBL)
                    objCOperations_CheckUniquenessTax.GetListPaging(999999, 1, "WHERE OperationStageID<>" + CancelledTransportOrderID + " AND TransportType=" + insertOperationData.pTransportType + " AND DirectionType=" + insertOperationData.pDirectionType + "  AND MasterBL IS NOT NULL AND MasterBL<>N'N/A' AND MasterBL NOT LIKE '%-0' AND MasterBL=N'" + insertOperationData.pMasterBL + "'", "ID", out _RowCount);
                if (objCOperations_CheckUniquenessTax.lstCVarOperations.Count > 0 && objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName != "BOM")
                    _MessageReturned = "Operation " + objCOperations_CheckUniquenessTax.lstCVarOperations[0].Code + " has the same Master B/L No.";
                #region Check House Uiqueness
                else
                {
                    if ((objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName == "EGL" || objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName == "SUN")
                        && insertOperationData.pIsShipment)
                        objCOperations_CheckUniquenessTax.GetListPaging(999999, 1, "WHERE HouseNumber IS NOT NULL AND HouseNumber<>N'' AND HouseNumber<>N'N/A' AND HouseNumber=N'" + insertOperationData.pHouseNumber + "'", "ID", out _RowCount);
                    if (objCOperations_CheckUniquenessTax.lstCVarOperations.Count > 0)
                        _MessageReturned = "This house number already exists.";
                }
                #endregion Check House Uiqueness
                if (_MessageReturned == "")
                {
                    CVarOperationsTAX objCVarOperationsTax = new CVarOperationsTAX();
                    int constShippingLineOperationPartnerTypeID = 9;
                    int constAirineOperationPartnerTypeID = 10;
                    int constTruckerOperationPartnerTypeID = 11;

                    #region Operation Header
                    objCVarOperationsTax.MasterBL = insertOperationData.pMasterBL;
                    objCVarOperationsTax.MAWBSuffix = insertOperationData.pMAWBSuffix;
                    objCVarOperationsTax.BLDate = DateTime.Parse("01-01-1900");
                    objCVarOperationsTax.HBLDate = DateTime.Parse("01-01-1900");
                    objCVarOperationsTax.PODate = DateTime.Parse("01-01-1900");

                    objCVarOperationsTax.Code = insertOperationData.pCode;
                    objCVarOperationsTax.HouseNumber = insertOperationData.pIsShipment ? insertOperationData.pHouseNumber : "0";//if HouseNumber is not null then its entered manually
                    objCVarOperationsTax.BranchID = int.Parse(insertOperationData.pBranchID);
                    objCVarOperationsTax.SalesmanID = int.Parse(insertOperationData.pSalesmanID);
                    objCVarOperationsTax.BLType = int.Parse(insertOperationData.pBLType);
                    objCVarOperationsTax.BLTypeIconName = insertOperationData.pBLTypeIconName;
                    objCVarOperationsTax.BLTypeIconStyle = insertOperationData.pBLTypeIconStyle;
                    objCVarOperationsTax.DirectionType = int.Parse(insertOperationData.pDirectionType);
                    objCVarOperationsTax.DirectionIconName = insertOperationData.pDirectionIconName;
                    objCVarOperationsTax.DirectionIconStyle = insertOperationData.pDirectionIconStyle;
                    objCVarOperationsTax.TransportType = int.Parse(insertOperationData.pTransportType);
                    objCVarOperationsTax.TransportIconName = insertOperationData.pTransportIconName;
                    objCVarOperationsTax.TransportIconStyle = insertOperationData.pTransportIconStyle;
                    objCVarOperationsTax.ShipmentType = int.Parse(insertOperationData.pShipmentType);
                    objCVarOperationsTax.ShipperID = int.Parse(insertOperationData.pShipperID);
                    objCVarOperationsTax.ShipperAddressID = Int64.Parse(insertOperationData.pShipperAddressID);
                    objCVarOperationsTax.ShipperContactID = Int64.Parse(insertOperationData.pShipperContactID);
                    objCVarOperationsTax.ConsigneeID = int.Parse(insertOperationData.pConsigneeID);
                    objCVarOperationsTax.ConsigneeAddressID = Int64.Parse(insertOperationData.pConsigneeAddressID);
                    objCVarOperationsTax.ConsigneeContactID = Int64.Parse(insertOperationData.pConsigneeContactID);
                    objCVarOperationsTax.AgentID = int.Parse(insertOperationData.pAgentID);
                    objCVarOperationsTax.AgentAddressID = Int64.Parse(insertOperationData.pAgentAddressID);
                    objCVarOperationsTax.AgentContactID = Int64.Parse(insertOperationData.pAgentContactID);
                    objCVarOperationsTax.IncotermID = int.Parse(insertOperationData.pIncotermID);
                    objCVarOperationsTax.POrC = insertOperationData.pPOrC;
                    objCVarOperationsTax.MoveTypeID = int.Parse(insertOperationData.pMoveTypeID);
                    objCVarOperationsTax.CommodityID = int.Parse(insertOperationData.pCommodityID);
                    objCVarOperationsTax.TransientTime = int.Parse(insertOperationData.pTransientTime);
                    //objCVarOperations.OpenDate = DateTime.Parse(insertOperationData.pOpenDate);
                    objCVarOperationsTax.OpenDate = insertOperationData.pOpenDate;
                    objCVarOperationsTax.CloseDate = insertOperationData.pCloseDate; //DateTime.Parse("01-01-1900");
                    objCVarOperationsTax.CutOffDate = DateTime.Parse(insertOperationData.pCutOffDate);
                    objCVarOperationsTax.IncludePickup = (insertOperationData.pIncludePickup == "True" ? true : false);
                    objCVarOperationsTax.PickupCityID = int.Parse(insertOperationData.pPickupCityID);
                    objCVarOperationsTax.PickupAddressID = int.Parse(insertOperationData.pPickupAddressID);
                    objCVarOperationsTax.POLCountryID = int.Parse(insertOperationData.pPOLCountryID);
                    objCVarOperationsTax.POL = int.Parse(insertOperationData.pPOL);
                    objCVarOperationsTax.PODCountryID = int.Parse(insertOperationData.pPODCountryID);
                    objCVarOperationsTax.POD = int.Parse(insertOperationData.pPOD);
                    objCVarOperationsTax.PickupAddress = "0"; //updated from main route
                    objCVarOperationsTax.DeliveryAddress = "0"; //updated from main route
                    objCVarOperationsTax.ShippingLineID = int.Parse(insertOperationData.pShippingLineID);
                    objCVarOperationsTax.AirlineID = int.Parse(insertOperationData.pAirlineID);
                    objCVarOperationsTax.TruckerID = int.Parse(insertOperationData.pTruckerID);
                    objCVarOperationsTax.IncludeDelivery = (insertOperationData.pIncludeDelivery == "True" ? true : false);
                    objCVarOperationsTax.DeliveryZipCode = insertOperationData.pDeliveryZipCode;
                    objCVarOperationsTax.DeliveryCityID = int.Parse(insertOperationData.pDeliveryCityID);
                    objCVarOperationsTax.DeliveryCountryID = int.Parse(insertOperationData.pDeliveryCountryID);
                    objCVarOperationsTax.GrossWeight = decimal.Parse(insertOperationData.pGrossWeight);
                    //objCVarOperations.Volume = decimal.Parse(insertOperationData.pVolume);
                    objCVarOperationsTax.ChargeableWeight = decimal.Parse(insertOperationData.pChargeableWeight);
                    //objCVarOperations.NumberOfPackages = int.Parse(insertOperationData.pNumberOfPackages);
                    objCVarOperationsTax.IsDangerousGoods = (insertOperationData.pIsDangerousGoods == "True" ? true : false);
                    objCVarOperationsTax.Notes = insertOperationData.pNotes;
                    objCVarOperationsTax.CustomerReference = insertOperationData.pCustomerReference;
                    objCVarOperationsTax.SupplierReference = insertOperationData.pSupplierReference;
                    objCVarOperationsTax.PONumber = insertOperationData.pPONumber;
                    objCVarOperationsTax.POValue = "0";
                    objCVarOperationsTax.ReleaseNumber = "0";
                    objCVarOperationsTax.DispatchNumber = "0";
                    objCVarOperationsTax.BusinessUnit = "0";
                    objCVarOperationsTax.Form13Number = "0";
                    objCVarOperationsTax.ACIDNumber = "0";
                    objCVarOperationsTax.AgreedRate = insertOperationData.pAgreedRate;
                    objCVarOperationsTax.OperationStageID = int.Parse(insertOperationData.pOperationStageID);
                    objCVarOperationsTax.NumberOfHousesConnected = 0;

                    objCVarOperationsTax.IsDelivered = insertOperationData.pIsDelivered;
                    objCVarOperationsTax.IsTrucking = insertOperationData.pIsTrucking;
                    objCVarOperationsTax.IsInsurance = insertOperationData.pIsInsurance;
                    objCVarOperationsTax.IsClearance = insertOperationData.pIsClearance;
                    objCVarOperationsTax.IsGenset = insertOperationData.pIsGenset;
                    objCVarOperationsTax.IsCourrier = insertOperationData.pIsCourrier;
                    objCVarOperationsTax.IsTelexRelease = insertOperationData.pIsTelexRelease;

                    objCVarOperationsTax.ConsigneeID2 = insertOperationData.pConsigneeID2;
                    objCVarOperationsTax.ReleaseDate = DateTime.ParseExact(insertOperationData.pReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    objCVarOperationsTax.ClearanceApprovalDate = DateTime.Parse("01-01-1900");
                    objCVarOperationsTax.TruckingApprovalDate = DateTime.Parse("01-01-1900");
                    objCVarOperationsTax.FreightApprovalDate = DateTime.Parse("01-01-1900");

                    objCVarOperationsTax.ShippedOnBoardDate = DateTime.Parse("01-01-1900");
                    objCVarOperationsTax.FreightPayableAt = "0";
                    objCVarOperationsTax.CertificateNumber = insertOperationData.pCertificateNumber; 
                    objCVarOperationsTax.CountryOfOrigin = insertOperationData.pCountryOfOrigin;
                    objCVarOperationsTax.InvoiceValue = insertOperationData.pInvoiceValue;
                    objCVarOperationsTax.NumberOfOriginalBills = 0;

                    #region AirAgents (Venus fields A.Medra)
                    objCVarOperationsTax.BLDate = DateTime.ParseExact(insertOperationData.pBLDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    objCVarOperationsTax.MAWBStockID = insertOperationData.pMAWBStockID;
                    objCVarOperationsTax.TypeOfStockID = insertOperationData.pTypeOfStockID;
                    objCVarOperationsTax.FlightNo = insertOperationData.pFlightNo;
                    objCVarOperationsTax.POrC = insertOperationData.pPOrC;
                    objCVarOperationsTax.IsAWB = insertOperationData.pIsAWB;
                    //Fields not in insert
                    objCVarOperationsTax.AirLineID1 = 0;
                    objCVarOperationsTax.FlightNo1 = "0";
                    objCVarOperationsTax.FlightDate1 = DateTime.Parse("01/01/1900");
                    objCVarOperationsTax.AirLineID2 = 0;
                    objCVarOperationsTax.FlightNo2 = "0";
                    objCVarOperationsTax.FlightDate2 = DateTime.Parse("01/01/1900");
                    objCVarOperationsTax.AirLineID3 = 0;
                    objCVarOperationsTax.FlightNo3 = "0";
                    objCVarOperationsTax.FlightDate3 = DateTime.Parse("01/01/1900");

                    objCVarOperationsTax.UNOrID = "0";
                    objCVarOperationsTax.ProperShippingName = "0";
                    objCVarOperationsTax.ClassOrDivision = "0";
                    objCVarOperationsTax.PackingGroup = "0";
                    objCVarOperationsTax.QuantityAndTypeOfPacking = "0";
                    objCVarOperationsTax.PackingInstruction = "0";
                    objCVarOperationsTax.ShippingDeclarationAuthorization = "0";
                    objCVarOperationsTax.Barcode = "0";

                    objCVarOperationsTax.GuaranteeLetterNumber = "0";
                    objCVarOperationsTax.GuaranteeLetterDate = DateTime.Parse("01/01/1900");
                    objCVarOperationsTax.GuaranteeLetterAmount = "0";
                    objCVarOperationsTax.GuaranteeLetterSupplierInvoiceNumber = "0";
                    objCVarOperationsTax.BankAccountID = 0;
                    objCVarOperationsTax.GuaranteeLetterNotes = "0";

                    objCVarOperationsTax.HandlingInformation = "0";
                    objCVarOperationsTax.AmountOfInsurance = "0";
                    objCVarOperationsTax.DeclaredValueForCarriage = "0";
                    objCVarOperationsTax.WeightCharge = 0;
                    objCVarOperationsTax.ValuationCharge = 0;
                    objCVarOperationsTax.OtherChargesDueAgent = 0;
                    objCVarOperationsTax.OtherCharges = "0";
                    objCVarOperationsTax.CurrencyID = insertOperationData.pCurrencyID == 0 ? objCvwDefaults.lstCVarvwDefaults[0].CurrencyID : insertOperationData.pCurrencyID;
                    objCVarOperationsTax.AccountingInformation = "0";
                    objCVarOperationsTax.ReferenceNumber = "0";
                    objCVarOperationsTax.OptionalShippingInformation = "0";
                    objCVarOperationsTax.CHGSCode = "0";
                    objCVarOperationsTax.WT_VALL_Other = "0";
                    objCVarOperationsTax.DeclaredValueForCustoms = "0";
                    objCVarOperationsTax.Tax = 0;
                    objCVarOperationsTax.OtherChargesDueCarrier = 0;
                    objCVarOperationsTax.WT_VALL = "0";
                    objCVarOperationsTax.MarksAndNumbers = "0";
                    objCVarOperationsTax.Description = "0";
                    #endregion Venus fields A.Medra

                    objCVarOperationsTax.CreatorUserID = objCVarOperationsTax.ModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarOperationsTax.CreationDate = (insertOperationData.pOpenDate == null ? DateTime.Parse("01-01-1900") : insertOperationData.pOpenDate);// DateTime.Now;
                    objCVarOperationsTax.ModificationDate = DateTime.Now;

                    objCVarOperationsTax.DismissalPermissionSerial = "0";
                    objCVarOperationsTax.DeliveryOrderSerial = "0";

                    objCVarOperationsTax.eFBLID = "0";
                    objCVarOperationsTax.eFBLStatus = 0;

                    objCOperationsTax.lstCVarOperations.Add(objCVarOperationsTax);
                    Exception checkException = objCOperationsTax.SaveMethod(objCOperationsTax.lstCVarOperations);
                    #endregion Operation Header
                    if (checkException != null) // an exception is caught in the model
                    {
                        if (checkException.Message.Contains("UNIQUE"))
                            _result = false;
                    }
                    else
                    {
                        _result = true;
                        if (!insertOperationData.pIsShipment)
                            Operations_EmailNotification(objCVarOperationsTax.ID);
                        #region DeliveryOrderSerial
                        CSerialsTax objCSerialsTax = new CSerialsTax();
                        if (insertOperationData.pBLType != "3") //not master
                        {
                            COperationsTAX objCOperationsTempTax = new COperationsTAX();
                            //objCOperationsTemp.UpdateList("DeliveryOrderSerial=(SELECT ltrim((ISNULL(DeliveryOrderSerial,0) + 1)) + '-' + RIGHT('00'+CAST(MONTH(GETDATE()) AS VARCHAR(3)),2) + '-' + ltrim(YEAR(GETDate())) FROM Serials WHERE Year=YEAR(GETDate())) WHERE ID=" + pOperationID + "");
                            objCOperationsTempTax.UpdateList("DeliveryOrderSerial=(SELECT ltrim((ISNULL(DeliveryOrderSerial,0) + 1)) + '/' + ltrim(YEAR(GETDate())) FROM Serials WHERE Year=YEAR(GETDate())) WHERE ID=" + OperationID + "");

                            objCSerialsTax.UpdateList("DeliveryOrderSerial=ISNULL(DeliveryOrderSerial,0) + 1 WHERE Year=YEAR(GETDate())");

                        }
                        CSerials objCSerialsOrigin = new CSerials();

                        objCSerialsOrigin.GetList("WHERE Year=YEAR(GETDate())");
                        if (objCSerialsOrigin.lstCVarSerials.Count > 0)
                        {
                            objCSerialsTax.UpdateList("OperationSerial=" + objCSerialsOrigin.lstCVarSerials[0].OperationSerial + " WHERE Year=YEAR(GETDate())");
                        }
                        //link
                        objCCustomizedDBCall.ExecuteQuery_DataTable("insertTaxLink " + OperationID + "," + objCVarOperationsTax.ID + "," + "Operations");
                        #endregion DeliveryOrderSerial
                        //COPY Partners To OperationPartners
                        #region Copy Operation Partners (at this point its just either a shipper or a consignee or an agent and empty notify)
                        COperationPartnersTAX objCOperationPartnersTax = new COperationPartnersTAX();
                        CVarOperationPartnersTAX objCVarOperationPartnersTax = new CVarOperationPartnersTAX();
                        CContacts objCContacts = new CContacts();//to save a contact by default

                        //to save a contact by default //PartnerTypeID = 2 for Agents
                        objCContacts.GetList(" WHERE PartnerTypeID = 2 AND PartnerID = " + insertOperationData.pAgentID.ToString());

                        CVarOperationPartnersTAX objCVarOperationAgentPartnerTax = new CVarOperationPartnersTAX();
                        objCVarOperationAgentPartnerTax.OperationID = objCVarOperationsTax.ID;
                        objCVarOperationAgentPartnerTax.OperationPartnerTypeID = 6; //Agent
                        objCVarOperationAgentPartnerTax.AgentID = int.Parse(insertOperationData.pAgentID);
                        objCVarOperationAgentPartnerTax.ContactID = (objCContacts.lstCVarContacts.Count > 0 ? objCContacts.lstCVarContacts[0].ID : 0);
                        objCVarOperationAgentPartnerTax.IsOperationClient = false;
                        objCVarOperationAgentPartnerTax.CreatorUserID = objCVarOperationAgentPartnerTax.ModificatorUserID = WebSecurity.CurrentUserId;
                        objCVarOperationAgentPartnerTax.CreationDate = objCVarOperationAgentPartnerTax.ModificationDate = DateTime.Now;
                        objCOperationPartnersTax.lstCVarOperationPartnersTAX.Add(objCVarOperationAgentPartnerTax);

                        //to save a contact by default //PartnerTypeID = 1 for Customer
                        objCContacts.GetList(" WHERE PartnerTypeID = 1 AND PartnerID = " + insertOperationData.pConsigneeID.ToString());

                        CVarOperationPartnersTAX objCVarOperationConsigneePartnerTax = new CVarOperationPartnersTAX();
                        objCVarOperationConsigneePartnerTax.OperationID = objCVarOperationsTax.ID;
                        objCVarOperationConsigneePartnerTax.OperationPartnerTypeID = 2;
                        objCVarOperationConsigneePartnerTax.CustomerID = int.Parse(insertOperationData.pConsigneeID);
                        objCVarOperationConsigneePartnerTax.ContactID = (objCContacts.lstCVarContacts.Count > 0 ? objCContacts.lstCVarContacts[0].ID : 0);
                        objCVarOperationConsigneePartnerTax.IsOperationClient = false;
                        objCVarOperationConsigneePartnerTax.CreatorUserID = objCVarOperationConsigneePartnerTax.ModificatorUserID = WebSecurity.CurrentUserId;
                        objCVarOperationConsigneePartnerTax.CreationDate = objCVarOperationConsigneePartnerTax.ModificationDate = DateTime.Now;
                        objCOperationPartnersTax.lstCVarOperationPartnersTAX.Add(objCVarOperationConsigneePartnerTax);


                        //to save a contact by default //PartnerTypeID = 1 for Customer
                        objCContacts.GetList(" WHERE PartnerTypeID = 1 AND PartnerID = " + insertOperationData.pShipperID.ToString());

                        CVarOperationPartnersTAX objCVarOperationShipperPartnerTax = new CVarOperationPartnersTAX();
                        objCVarOperationShipperPartnerTax.OperationID = objCVarOperationsTax.ID;
                        objCVarOperationShipperPartnerTax.OperationPartnerTypeID = 1; // export or domestic (shipper)
                        objCVarOperationShipperPartnerTax.CustomerID = int.Parse(insertOperationData.pShipperID);
                        objCVarOperationShipperPartnerTax.ContactID = (objCContacts.lstCVarContacts.Count > 0 ? objCContacts.lstCVarContacts[0].ID : 0);
                        objCVarOperationShipperPartnerTax.IsOperationClient = false;
                        objCVarOperationShipperPartnerTax.CreatorUserID = objCVarOperationShipperPartnerTax.ModificatorUserID = WebSecurity.CurrentUserId;
                        objCVarOperationShipperPartnerTax.CreationDate = objCVarOperationShipperPartnerTax.ModificationDate = DateTime.Now;
                        objCOperationPartnersTax.lstCVarOperationPartnersTAX.Add(objCVarOperationShipperPartnerTax);

                        objCContacts.GetList(" WHERE PartnerTypeID = 1 AND PartnerID = " + insertOperationData.pNotifyID.ToString());

                        CVarOperationPartnersTAX objCVarOperationNotifyPartnerTax = new CVarOperationPartnersTAX();
                        objCVarOperationNotifyPartnerTax.OperationID = objCVarOperationsTax.ID;
                        objCVarOperationNotifyPartnerTax.OperationPartnerTypeID = 4;//Notify1
                        objCVarOperationNotifyPartnerTax.CustomerID = Int32.Parse(insertOperationData.pNotifyID); // it will be set as null in DB
                        objCVarOperationNotifyPartnerTax.ContactID = (objCContacts.lstCVarContacts.Count > 0 ? objCContacts.lstCVarContacts[0].ID : 0);
                        objCVarOperationNotifyPartnerTax.IsOperationClient = false;
                        objCVarOperationNotifyPartnerTax.CreatorUserID = objCVarOperationNotifyPartnerTax.ModificatorUserID = WebSecurity.CurrentUserId;
                        objCVarOperationNotifyPartnerTax.CreationDate = objCVarOperationNotifyPartnerTax.ModificationDate = DateTime.Now;
                        objCOperationPartnersTax.lstCVarOperationPartnersTAX.Add(objCVarOperationNotifyPartnerTax);

                        if (Int32.Parse(insertOperationData.pShippingLineID) > 0)
                        {
                            objCContacts.GetList(" WHERE PartnerTypeID = " + constShippingLineOperationPartnerTypeID + " AND PartnerID = " + insertOperationData.pNotifyID.ToString());
                            CVarOperationPartnersTAX objCVarOperationShippingLinePartnerTax = new CVarOperationPartnersTAX();
                            objCVarOperationShippingLinePartnerTax.OperationID = objCVarOperationsTax.ID;
                            objCVarOperationShippingLinePartnerTax.OperationPartnerTypeID = constShippingLineOperationPartnerTypeID;
                            objCVarOperationShippingLinePartnerTax.ShippingLineID = Int32.Parse(insertOperationData.pShippingLineID); // it will be set as null in DB
                            objCVarOperationShippingLinePartnerTax.ContactID = (objCContacts.lstCVarContacts.Count > 0 ? objCContacts.lstCVarContacts[0].ID : 0);
                            objCVarOperationShippingLinePartnerTax.IsOperationClient = false;
                            objCVarOperationShippingLinePartnerTax.CreatorUserID = objCVarOperationShippingLinePartnerTax.ModificatorUserID = WebSecurity.CurrentUserId;
                            objCVarOperationShippingLinePartnerTax.CreationDate = objCVarOperationShippingLinePartnerTax.ModificationDate = DateTime.Now;
                            objCOperationPartnersTax.lstCVarOperationPartnersTAX.Add(objCVarOperationShippingLinePartnerTax);
                        }

                        if (Int32.Parse(insertOperationData.pAirlineID) > 0)
                        {
                            objCContacts.GetList(" WHERE PartnerTypeID = " + constAirineOperationPartnerTypeID + " AND PartnerID = " + insertOperationData.pNotifyID.ToString());
                            CVarOperationPartnersTAX objCVarOperationAirlinePartnerTax = new CVarOperationPartnersTAX();
                            objCVarOperationAirlinePartnerTax.OperationID = objCVarOperationsTax.ID;
                            objCVarOperationAirlinePartnerTax.OperationPartnerTypeID = constAirineOperationPartnerTypeID;
                            objCVarOperationAirlinePartnerTax.AirlineID = Int32.Parse(insertOperationData.pAirlineID); // it will be set as null in DB
                            objCVarOperationAirlinePartnerTax.ContactID = (objCContacts.lstCVarContacts.Count > 0 ? objCContacts.lstCVarContacts[0].ID : 0);
                            objCVarOperationAirlinePartnerTax.IsOperationClient = false;
                            objCVarOperationAirlinePartnerTax.CreatorUserID = objCVarOperationAirlinePartnerTax.ModificatorUserID = WebSecurity.CurrentUserId;
                            objCVarOperationAirlinePartnerTax.CreationDate = objCVarOperationAirlinePartnerTax.ModificationDate = DateTime.Now;
                            objCOperationPartnersTax.lstCVarOperationPartnersTAX.Add(objCVarOperationAirlinePartnerTax);
                        }

                        if (Int32.Parse(insertOperationData.pTruckerID) > 0)
                        {
                            objCContacts.GetList(" WHERE PartnerTypeID = " + constTruckerOperationPartnerTypeID + " AND PartnerID = " + insertOperationData.pNotifyID.ToString());
                            CVarOperationPartnersTAX objCVarOperationTruckerPartnerTax = new CVarOperationPartnersTAX();
                            objCVarOperationTruckerPartnerTax.OperationID = objCVarOperationsTax.ID;
                            objCVarOperationTruckerPartnerTax.OperationPartnerTypeID = constTruckerOperationPartnerTypeID;
                            objCVarOperationTruckerPartnerTax.TruckerID = Int32.Parse(insertOperationData.pTruckerID); // it will be set as null in DB
                            objCVarOperationTruckerPartnerTax.ContactID = (objCContacts.lstCVarContacts.Count > 0 ? objCContacts.lstCVarContacts[0].ID : 0);
                            objCVarOperationTruckerPartnerTax.IsOperationClient = false;
                            objCVarOperationTruckerPartnerTax.CreatorUserID = objCVarOperationTruckerPartnerTax.ModificatorUserID = WebSecurity.CurrentUserId;
                            objCVarOperationTruckerPartnerTax.CreationDate = objCVarOperationTruckerPartnerTax.ModificationDate = DateTime.Now;
                            objCOperationPartnersTax.lstCVarOperationPartnersTAX.Add(objCVarOperationTruckerPartnerTax);
                        }

                        checkException = objCOperationPartnersTax.SaveMethod(objCOperationPartnersTax.lstCVarOperationPartnersTAX);

                        COperationPartners objCOperationPartnersOrigin = new COperationPartners();
                        objCOperationPartnersOrigin.GetList(" WHERE OperationID = " + OperationID + "order by id");
                        COperationPartnersTAX objCOperationPartnersForTax = new COperationPartnersTAX();
                        objCOperationPartnersForTax.GetList(" WHERE OperationID = " + objCVarOperationsTax.ID + "order by id");


                        for (int i = 0; i < objCOperationPartnersOrigin.lstCVarOperationPartners.Count; i++)
                        {
                            for (int j = 0; j < objCOperationPartnersForTax.lstCVarOperationPartnersTAX.Count; j++)
                            {
                                if (i == j)
                                {
                                    objCCustomizedDBCall.ExecuteQuery_DataTable("insertTaxLink " + objCOperationPartnersOrigin.lstCVarOperationPartners[j].ID + "," + (objCOperationPartnersForTax.lstCVarOperationPartnersTAX[i].ID) + "," + "OperationPartners");

                                }
                                //link

                            }
                        }
                        #endregion

                        //COPY Routings To Routings
                        //MainCarraige has ID = 30
                        #region Copy Operation Routings (at this point its just Main Carraige)
                        CVarRoutingsTAX objCVarRoutingsTax = new CVarRoutingsTAX();

                        objCVarRoutingsTax.OperationID = objCVarOperationsTax.ID;
                        objCVarRoutingsTax.TransportType = objCOperations.lstCVarOperations[0].TransportType;
                        objCVarRoutingsTax.TransportIconName = objCOperations.lstCVarOperations[0].TransportIconName;
                        objCVarRoutingsTax.TransportIconStyle = objCOperations.lstCVarOperations[0].TransportIconStyle;
                        objCVarRoutingsTax.RoutingTypeID = MainCarraigeRoutingTypeID; //Main Carraige
                        objCVarRoutingsTax.POLCountryID = objCOperations.lstCVarOperations[0].POLCountryID;
                        objCVarRoutingsTax.POL = objCOperations.lstCVarOperations[0].POL;
                        objCVarRoutingsTax.PODCountryID = objCOperations.lstCVarOperations[0].PODCountryID;
                        objCVarRoutingsTax.POD = objCOperations.lstCVarOperations[0].POD;
                        objCVarRoutingsTax.ETAPOLDate = DateTime.Parse("01-01-1900");
                        objCVarRoutingsTax.ATAPOLDate = DateTime.Parse("01-01-1900");
                        //objCVarRoutings.ActualDeparture = DateTime.ParseExact(insertOperationData.pExpectedDeparture, "dd/MM/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);
                        objCVarRoutingsTax.ExpectedDeparture = DateTime.ParseExact(insertOperationData.pExpectedDeparture, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        objCVarRoutingsTax.ActualDeparture = DateTime.Parse("01-01-1900");
                        objCVarRoutingsTax.ExpectedArrival = DateTime.ParseExact(insertOperationData.pExpectedArrival, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        objCVarRoutingsTax.ActualArrival = DateTime.Parse("01-01-1900");
                        objCVarRoutingsTax.VesselID = insertOperationData.pVesselID;
                        objCVarRoutingsTax.VoyageOrTruckNumber = insertOperationData.pVoyageOrTruckNumber;
                        objCVarRoutingsTax.RoadNumber = "0";
                        objCVarRoutingsTax.DeliveryOrderNumber = "0";
                        objCVarRoutingsTax.WareHouse = "0";
                        objCVarRoutingsTax.WareHouseLocation = "0";
                        objCVarRoutingsTax.Notes = "";

                        if (insertOperationData.pTransportType == "1") //Ocean
                        {
                            objCVarRoutingsTax.ShippingLineID = objCOperations.lstCVarOperations[0].ShippingLineID;
                            objCVarRoutingsTax.AirlineID = 0;
                            objCVarRoutingsTax.TruckerID = 0;
                        }
                        else if (insertOperationData.pTransportType == "2") //Air
                        {
                            objCVarRoutingsTax.ShippingLineID = 0;
                            objCVarRoutingsTax.AirlineID = objCOperations.lstCVarOperations[0].AirlineID;
                            objCVarRoutingsTax.TruckerID = 0;
                        }
                        else //Inland , TransportType = 3
                        {
                            objCVarRoutingsTax.ShippingLineID = 0;
                            objCVarRoutingsTax.AirlineID = 0;
                            objCVarRoutingsTax.TruckerID = objCOperations.lstCVarOperations[0].TruckerID;
                        }

                        objCVarRoutingsTax.GensetSupplierID = 0;
                        objCVarRoutingsTax.CCAID = 0;
                        objCVarRoutingsTax.Quantity = "0";
                        objCVarRoutingsTax.ContactPerson = "0";
                        objCVarRoutingsTax.PickupAddress = "0";
                        objCVarRoutingsTax.DeliveryAddress = "0";
                        objCVarRoutingsTax.GateInPortID = 0;
                        objCVarRoutingsTax.GateOutPortID = 0;
                        objCVarRoutingsTax.GateInDate = DateTime.Parse("01/01/1900");

                        #region TransportOrder
                        objCVarRoutingsTax.CustomerID = 0;
                        objCVarRoutingsTax.SubContractedCustomerID = 0;
                        objCVarRoutingsTax.Cost = 0;
                        objCVarRoutingsTax.Sale = 0;
                        objCVarRoutingsTax.IsFleet = false;
                        objCVarRoutingsTax.CommodityID = 0;
                        objCVarRoutingsTax.LoadingDate = DateTime.Parse("01/01/1900");
                        objCVarRoutingsTax.LoadingReference = "0";
                        objCVarRoutingsTax.UnloadingDate = DateTime.Parse("01/01/1900");
                        objCVarRoutingsTax.UnloadingReference = "0";
                        objCVarRoutingsTax.UnloadingTime = "0";
                        #endregion TransportOrder

                        objCVarRoutingsTax.GateOutDate = DateTime.Parse("01/01/1900");
                        objCVarRoutingsTax.StuffingDate = DateTime.Parse("01/01/1900");
                        objCVarRoutingsTax.DeliveryDate = DateTime.Parse("01/01/1900");
                        objCVarRoutingsTax.BookingNumber = "0";
                        objCVarRoutingsTax.Delays = "0";
                        objCVarRoutingsTax.DriverName = "0";
                        objCVarRoutingsTax.DriverPhones = "0";
                        objCVarRoutingsTax.PowerFromGateInTillActualSailing = "0";
                        objCVarRoutingsTax.ContactPersonPhones = "0";
                        objCVarRoutingsTax.LoadingTime = "0";

                        #region CustomsClearance
                        objCVarRoutingsTax.CCAFreight = 0;
                        objCVarRoutingsTax.CCAFOB = 0;
                        objCVarRoutingsTax.CCACFValue = 0;
                        objCVarRoutingsTax.CCAInvoiceNumber = "0";

                        objCVarRoutingsTax.CCAInsurance = "0";
                        objCVarRoutingsTax.CCADischargeValue = "0";
                        objCVarRoutingsTax.CCAAcceptedValue = "0";
                        objCVarRoutingsTax.CCAImportValue = "0";
                        objCVarRoutingsTax.CCADocumentReceiveDate = DateTime.Parse("01/01/1900");
                        objCVarRoutingsTax.CCAExchangeRate = "0";
                        objCVarRoutingsTax.CCAVATCertificateNumber = "0";
                        objCVarRoutingsTax.CCAVATCertificateValue = "0";
                        objCVarRoutingsTax.CCACommercialProfitCertificateNumber = "0";
                        objCVarRoutingsTax.CCAOthers = "0";
                        objCVarRoutingsTax.CCASpendDate = DateTime.Parse("01/01/1900");
                        objCVarRoutingsTax.OffloadingDate = DateTime.Parse("01/01/1900");

                        objCVarRoutingsTax.CertificateNumber = "0";
                        objCVarRoutingsTax.CertificateValue = "0";
                        objCVarRoutingsTax.CertificateDate = DateTime.Parse("01/01/1900");
                        objCVarRoutingsTax.QasimaNumber = "0";
                        objCVarRoutingsTax.QasimaDate = DateTime.Parse("01/01/1900");
                        objCVarRoutingsTax.SalesDateReceived = DateTime.Parse("01/01/1900");
                        objCVarRoutingsTax.CommerceDateReceived = DateTime.Parse("01/01/1900");
                        objCVarRoutingsTax.InspectionDateReceived = DateTime.Parse("01/01/1900");
                        objCVarRoutingsTax.FinishDateReceived = DateTime.Parse("01/01/1900");
                        objCVarRoutingsTax.SalesDateDelivered = DateTime.Parse("01/01/1900");
                        objCVarRoutingsTax.CommerceDateDelivered = DateTime.Parse("01/01/1900");
                        objCVarRoutingsTax.InspectionDateDelivered = DateTime.Parse("01/01/1900");
                        objCVarRoutingsTax.FinishDateDelivered = DateTime.Parse("01/01/1900");
                        objCVarRoutingsTax.CCAllowTemporaryDelivered = DateTime.Parse("01/01/1900");
                        objCVarRoutingsTax.CCAllowTemporaryReceived = DateTime.Parse("01/01/1900");
                        objCVarRoutingsTax.CCDropBackDelivered = DateTime.Parse("01/01/1900");
                        objCVarRoutingsTax.CCDropBackReceived = DateTime.Parse("01/01/1900");
                        objCVarRoutingsTax.CC_ClearanceTypeID = 0;
                        objCVarRoutingsTax.CCReleaseNo = "0";
                        #endregion CustomsClearance
                        objCVarRoutingsTax.BillNumber = "0";
                        objCVarRoutingsTax.TruckingOrderCode = "0";

                        objCVarRoutingsTax.CreatorUserID = objCVarRoutingsTax.ModificatorUserID = WebSecurity.CurrentUserId;
                        objCVarRoutingsTax.ModificationDate = objCVarRoutingsTax.CreationDate = DateTime.Now;

                        CRoutingsTAX objCRoutingsTax = new CRoutingsTAX();
                        objCRoutingsTax.lstCVarRoutings.Add(objCVarRoutingsTax);
                        objCRoutingsTax.SaveMethod(objCRoutingsTax.lstCVarRoutings);
                        //link
                        objCCustomizedDBCall.ExecuteQuery_DataTable("insertTaxLink " + RoutingID + "," + objCVarRoutingsTax.ID + "," + "Routings");

                        #endregion

                        #region AddContainers if exists
                        if (insertOperationData.pContainerTypeID > 0 && insertOperationData.pNumberOfContainers > 0)
                        {
                            COperationContainersAndPackagesTAX objCOperationContainersAndPackagesTax = new COperationContainersAndPackagesTAX();
                            for (int z = 0; z < insertOperationData.pNumberOfContainers; z++)
                            {
                                CVarOperationContainersAndPackagesTAX objCVarOperationContainersAndPackagesTax = new CVarOperationContainersAndPackagesTAX();

                                objCVarOperationContainersAndPackagesTax.OperationID = objCVarOperationsTax.ID;
                                objCVarOperationContainersAndPackagesTax.ContainerTypeID = insertOperationData.pContainerTypeID;
                                //objCVarOperationContainersAndPackages.Length = pLength;
                                //objCVarOperationContainersAndPackages.Width = pWidth;
                                //objCVarOperationContainersAndPackages.Height = pHeight;
                                //objCVarOperationContainersAndPackages.Volume = Decimal.Parse(insertOperationData.pVolume);
                                //objCVarOperationContainersAndPackages.VolumetricWeight = pVolumetricWeight;
                                //objCVarOperationContainersAndPackages.NetWeight = Decimal.Parse(insertOperationData.pNetWeight);
                                //objCVarOperationContainersAndPackages.GrossWeight = Decimal.Parse(insertOperationData.pGrossWeight);
                                //objCVarOperationContainersAndPackages.ChargeableWeight = pChargeableWeight;
                                //if (int.Parse(insertOperationData.pShipmentType) == 1 || int.Parse(insertOperationData.pShipmentType) == 3)//FCL or FTL (i.e. container load)
                                //{
                                //    objCVarOperationContainersAndPackages.NumberOfPackagesOnContainer = int.Parse(insertOperationData.pNumberOfPackages);
                                //    objCVarOperationContainersAndPackages.PackageTypeIDOnContainer = int.Parse(insertOperationData.pPackageTypeID);
                                //}
                                //else //air or (lcl or ltl)
                                //{
                                //    objCVarOperationContainersAndPackages.Quantity = int.Parse(insertOperationData.pNumberOfPackages);
                                //    objCVarOperationContainersAndPackages.PackageTypeID = int.Parse(insertOperationData.pPackageTypeID);
                                //}

                                objCVarOperationContainersAndPackagesTax.ContainerNumber = "0";
                                objCVarOperationContainersAndPackagesTax.CarrierSeal = "0";
                                objCVarOperationContainersAndPackagesTax.ShipperSeal = "0";
                                //objCVarOperationContainersAndPackages.TareWeight = pTareWeight;
                                //objCVarOperationContainersAndPackages.IsReefer = pIsReefer;
                                //objCVarOperationContainersAndPackages.IsNOR = pIsNOR;
                                //objCVarOperationContainersAndPackages.MinTemp = pMinTemp;
                                //objCVarOperationContainersAndPackages.MaxTemp = pMaxTemp;
                                //objCVarOperationContainersAndPackages.IsIMO = pIsIMO;
                                //objCVarOperationContainersAndPackages.IMOClass = pIMOClass;
                                //objCVarOperationContainersAndPackages.UNNumber = pUNNumber;
                                //objCVarOperationContainersAndPackages.FlashPoint = pFlashPoint;
                                objCVarOperationContainersAndPackagesTax.DescriptionOfGoods = "0"; // insertOperationData.pDescriptionOfGoods;
                                objCVarOperationContainersAndPackagesTax.MarksAndNumbers = "0";
                                objCVarOperationContainersAndPackagesTax.LotNumber = "0";
                                //objCVarOperationContainersAndPackages.PackageTypeIDOnContainer = pPackageTypeIDOnContainer;
                                //objCVarOperationContainersAndPackages.NumberOfPackagesOnContainer = pNumberOfPackagesOnContainer;
                                #region ContainerTracking
                                objCVarOperationContainersAndPackagesTax.GateOutPortID = 0;
                                objCVarOperationContainersAndPackagesTax.GateInPortID = 0;
                                objCVarOperationContainersAndPackagesTax.GateOutDate = DateTime.Parse("01/01/1900");
                                objCVarOperationContainersAndPackagesTax.StuffingDate = DateTime.Parse("01/01/1900");
                                objCVarOperationContainersAndPackagesTax.LoadingDate = DateTime.Parse("01/01/1900");
                                objCVarOperationContainersAndPackagesTax.GateOutAndLoadingDatesDifference = 0;
                                objCVarOperationContainersAndPackagesTax.Factory = "0";
                                objCVarOperationContainersAndPackagesTax.ExportBLNumber = "0";
                                objCVarOperationContainersAndPackagesTax.ImportBLNumber = "0";
                                objCVarOperationContainersAndPackagesTax.IsLoaded = false;
                                objCVarOperationContainersAndPackagesTax.IsTracked = false;
                                objCVarOperationContainersAndPackagesTax.IsOwnedByCompany = false;
                                objCVarOperationContainersAndPackagesTax.TrailerID = 0;
                                objCVarOperationContainersAndPackagesTax.DriverID = 0;
                                objCVarOperationContainersAndPackagesTax.DriverAssistantID = 0;
                                objCVarOperationContainersAndPackagesTax.SupplierTrailerName = "0";
                                objCVarOperationContainersAndPackagesTax.SupplierDriverName = "0";
                                objCVarOperationContainersAndPackagesTax.SupplierDriverAssistantName = "0";
                                #endregion ContainerTracking
                                #region AirAgents columns
                                objCVarOperationContainersAndPackagesTax.Rate = 0;
                                objCVarOperationContainersAndPackagesTax.IsAsAgreed = false;
                                objCVarOperationContainersAndPackagesTax.IsMinimum = false;
                                objCVarOperationContainersAndPackagesTax.WeightUnit = "0";
                                objCVarOperationContainersAndPackagesTax.RateClass = "0";
                                #endregion AirAgents columns

                                #region Tank
                                objCVarOperationContainersAndPackagesTax.TankOrFlexiNumber = "0";
                                objCVarOperationContainersAndPackagesTax.OperatorID = 0;

                                objCVarOperationContainersAndPackagesTax.IsFull = false;
                                objCVarOperationContainersAndPackagesTax.ExitDate = DateTime.Parse("01/01/1900");
                                objCVarOperationContainersAndPackagesTax.ReturnDate = DateTime.Parse("01/01/1900");
                                objCVarOperationContainersAndPackagesTax.FreeDays = 0;
                                objCVarOperationContainersAndPackagesTax.DayValue = 0;
                                #endregion Tank
                                #region Yard
                                objCVarOperationContainersAndPackagesTax.YardEIRNumber = 0;
                                objCVarOperationContainersAndPackagesTax.YardEIRNumberOut = 0;
                                objCVarOperationContainersAndPackagesTax.YardInDate = DateTime.Parse("01/01/1900");
                                objCVarOperationContainersAndPackagesTax.YardInTime = 0;
                                objCVarOperationContainersAndPackagesTax.YardOutDate = DateTime.Parse("01/01/1900");
                                objCVarOperationContainersAndPackagesTax.YardOutTime = 0;
                                objCVarOperationContainersAndPackagesTax.YardLocationID = 0;
                                objCVarOperationContainersAndPackagesTax.YardIsIn = 0;
                                #endregion Yard

                                objCVarOperationContainersAndPackagesTax.CreatorUserID = objCVarOperationContainersAndPackagesTax.ModificatorUserID = WebSecurity.CurrentUserId;
                                objCVarOperationContainersAndPackagesTax.CreationDate = objCVarOperationContainersAndPackagesTax.ModificationDate = DateTime.Now;

                                objCOperationContainersAndPackagesTax.lstCVarOperationContainersAndPackages.Add(objCVarOperationContainersAndPackagesTax);
                            }
                            checkException = objCOperationContainersAndPackagesTax.SaveMethod(objCOperationContainersAndPackagesTax.lstCVarOperationContainersAndPackages);
                            if (checkException == null)
                            {
                                //link
                                objCCustomizedDBCall.ExecuteQuery_DataTable("insertTaxLink " + OperationContainersAndPackagesID + "," + objCOperationContainersAndPackagesTax.lstCVarOperationContainersAndPackages[0].ID + "," + "OperationContainersAndPackages");

                            }
                        }
                        if (insertOperationData.pContainerTypeID2 > 0 && insertOperationData.pNumberOfContainers2 > 0)
                        {
                            COperationContainersAndPackagesTAX objCOperationContainersAndPackagesTax = new COperationContainersAndPackagesTAX();
                            for (int z = 0; z < insertOperationData.pNumberOfContainers2; z++)
                            {
                                CVarOperationContainersAndPackagesTAX objCVarOperationContainersAndPackagesTax = new CVarOperationContainersAndPackagesTAX();

                                objCVarOperationContainersAndPackagesTax.OperationID = objCOperations.lstCVarOperations[0].ID;
                                objCVarOperationContainersAndPackagesTax.ContainerTypeID = insertOperationData.pContainerTypeID2;
                                //objCVarOperationContainersAndPackages.Length = pLength;
                                //objCVarOperationContainersAndPackages.Width = pWidth;
                                //objCVarOperationContainersAndPackages.Height = pHeight;
                                //objCVarOperationContainersAndPackages.Volume = Decimal.Parse(insertOperationData.pVolume);
                                //objCVarOperationContainersAndPackages.VolumetricWeight = pVolumetricWeight;
                                //objCVarOperationContainersAndPackages.NetWeight = Decimal.Parse(insertOperationData.pNetWeight);
                                //objCVarOperationContainersAndPackages.GrossWeight = Decimal.Parse(insertOperationData.pGrossWeight);
                                //objCVarOperationContainersAndPackages.ChargeableWeight = pChargeableWeight;
                                //if (int.Parse(insertOperationData.pShipmentType) == 1 || int.Parse(insertOperationData.pShipmentType) == 3)//FCL or FTL (i.e. container load)
                                //{
                                //    objCVarOperationContainersAndPackages.NumberOfPackagesOnContainer = int.Parse(insertOperationData.pNumberOfPackages);
                                //    objCVarOperationContainersAndPackages.PackageTypeIDOnContainer = int.Parse(insertOperationData.pPackageTypeID);
                                //}
                                //else //air or (lcl or ltl)
                                //{
                                //    objCVarOperationContainersAndPackages.Quantity = int.Parse(insertOperationData.pNumberOfPackages);
                                //    objCVarOperationContainersAndPackages.PackageTypeID = int.Parse(insertOperationData.pPackageTypeID);
                                //}

                                objCVarOperationContainersAndPackagesTax.ContainerNumber = "0";
                                objCVarOperationContainersAndPackagesTax.CarrierSeal = "0";
                                objCVarOperationContainersAndPackagesTax.ShipperSeal = "0";
                                //objCVarOperationContainersAndPackages.TareWeight = pTareWeight;
                                //objCVarOperationContainersAndPackages.IsReefer = pIsReefer;
                                //objCVarOperationContainersAndPackages.IsNOR = pIsNOR;
                                //objCVarOperationContainersAndPackages.MinTemp = pMinTemp;
                                //objCVarOperationContainersAndPackages.MaxTemp = pMaxTemp;
                                //objCVarOperationContainersAndPackages.IsIMO = pIsIMO;
                                //objCVarOperationContainersAndPackages.IMOClass = pIMOClass;
                                //objCVarOperationContainersAndPackages.UNNumber = pUNNumber;
                                //objCVarOperationContainersAndPackages.FlashPoint = pFlashPoint;
                                objCVarOperationContainersAndPackagesTax.DescriptionOfGoods = "0"; // insertOperationData.pDescriptionOfGoods;
                                objCVarOperationContainersAndPackagesTax.MarksAndNumbers = "0";
                                objCVarOperationContainersAndPackagesTax.LotNumber = "0";
                                //objCVarOperationContainersAndPackages.PackageTypeIDOnContainer = pPackageTypeIDOnContainer;
                                //objCVarOperationContainersAndPackages.NumberOfPackagesOnContainer = pNumberOfPackagesOnContainer;
                                #region ContainerTracking
                                objCVarOperationContainersAndPackagesTax.GateOutPortID = 0;
                                objCVarOperationContainersAndPackagesTax.GateInPortID = 0;
                                objCVarOperationContainersAndPackagesTax.GateOutDate = DateTime.Parse("01/01/1900");
                                objCVarOperationContainersAndPackagesTax.StuffingDate = DateTime.Parse("01/01/1900");
                                objCVarOperationContainersAndPackagesTax.LoadingDate = DateTime.Parse("01/01/1900");
                                objCVarOperationContainersAndPackagesTax.GateOutAndLoadingDatesDifference = 0;
                                objCVarOperationContainersAndPackagesTax.Factory = "0";
                                objCVarOperationContainersAndPackagesTax.ExportBLNumber = "0";
                                objCVarOperationContainersAndPackagesTax.ImportBLNumber = "0";
                                objCVarOperationContainersAndPackagesTax.IsLoaded = false;
                                objCVarOperationContainersAndPackagesTax.IsTracked = false;
                                objCVarOperationContainersAndPackagesTax.IsOwnedByCompany = false;
                                objCVarOperationContainersAndPackagesTax.TrailerID = 0;
                                objCVarOperationContainersAndPackagesTax.DriverID = 0;
                                objCVarOperationContainersAndPackagesTax.DriverAssistantID = 0;
                                objCVarOperationContainersAndPackagesTax.SupplierTrailerName = "0";
                                objCVarOperationContainersAndPackagesTax.SupplierDriverName = "0";
                                objCVarOperationContainersAndPackagesTax.SupplierDriverAssistantName = "0";
                                #endregion ContainerTracking
                                #region AirAgents columns
                                objCVarOperationContainersAndPackagesTax.Rate = 0;
                                objCVarOperationContainersAndPackagesTax.IsAsAgreed = false;
                                objCVarOperationContainersAndPackagesTax.IsMinimum = false;
                                objCVarOperationContainersAndPackagesTax.WeightUnit = "0";
                                objCVarOperationContainersAndPackagesTax.RateClass = "0";
                                #endregion AirAgents columns

                                #region Tank
                                objCVarOperationContainersAndPackagesTax.TankOrFlexiNumber = "0";
                                objCVarOperationContainersAndPackagesTax.OperatorID = 0;

                                objCVarOperationContainersAndPackagesTax.IsFull = false;
                                objCVarOperationContainersAndPackagesTax.ExitDate = DateTime.Parse("01/01/1900");
                                objCVarOperationContainersAndPackagesTax.ReturnDate = DateTime.Parse("01/01/1900");
                                objCVarOperationContainersAndPackagesTax.FreeDays = 0;
                                objCVarOperationContainersAndPackagesTax.DayValue = 0;
                                #endregion Tank
                                #region Yard
                                objCVarOperationContainersAndPackagesTax.YardEIRNumber = 0;
                                objCVarOperationContainersAndPackagesTax.YardEIRNumberOut = 0;
                                objCVarOperationContainersAndPackagesTax.YardInDate = DateTime.Parse("01/01/1900");
                                objCVarOperationContainersAndPackagesTax.YardInTime = 0;
                                objCVarOperationContainersAndPackagesTax.YardOutDate = DateTime.Parse("01/01/1900");
                                objCVarOperationContainersAndPackagesTax.YardOutTime = 0;
                                objCVarOperationContainersAndPackagesTax.YardLocationID = 0;
                                objCVarOperationContainersAndPackagesTax.YardIsIn = 0;
                                #endregion Yard

                                objCVarOperationContainersAndPackagesTax.CreatorUserID = objCVarOperationContainersAndPackagesTax.ModificatorUserID = WebSecurity.CurrentUserId;
                                objCVarOperationContainersAndPackagesTax.CreationDate = objCVarOperationContainersAndPackagesTax.ModificationDate = DateTime.Now;

                                objCOperationContainersAndPackagesTax.lstCVarOperationContainersAndPackages.Add(objCVarOperationContainersAndPackagesTax);
                            }
                            checkException = objCOperationContainersAndPackagesTax.SaveMethod(objCOperationContainersAndPackagesTax.lstCVarOperationContainersAndPackages);
                            if (checkException == null)
                            {
                                //link
                                objCCustomizedDBCall.ExecuteQuery_DataTable("insertTaxLink " + OperationContainersAndPackagesID + "," + objCOperationContainersAndPackagesTax.lstCVarOperationContainersAndPackages[0].ID + "," + "OperationContainersAndPackages");

                            }
                        }
                        if (insertOperationData.pContainerTypeID3 > 0 && insertOperationData.pNumberOfContainers3 > 0)
                        {
                            COperationContainersAndPackagesTAX objCOperationContainersAndPackagesTax = new COperationContainersAndPackagesTAX();
                            for (int z = 0; z < insertOperationData.pNumberOfContainers3; z++)
                            {
                                CVarOperationContainersAndPackagesTAX objCVarOperationContainersAndPackagesTax = new CVarOperationContainersAndPackagesTAX();

                                objCVarOperationContainersAndPackagesTax.OperationID = objCOperations.lstCVarOperations[0].ID;
                                objCVarOperationContainersAndPackagesTax.ContainerTypeID = insertOperationData.pContainerTypeID3;
                                //objCVarOperationContainersAndPackages.Length = pLength;
                                //objCVarOperationContainersAndPackages.Width = pWidth;
                                //objCVarOperationContainersAndPackages.Height = pHeight;
                                //objCVarOperationContainersAndPackages.Volume = Decimal.Parse(insertOperationData.pVolume);
                                //objCVarOperationContainersAndPackages.VolumetricWeight = pVolumetricWeight;
                                //objCVarOperationContainersAndPackages.NetWeight = Decimal.Parse(insertOperationData.pNetWeight);
                                //objCVarOperationContainersAndPackages.GrossWeight = Decimal.Parse(insertOperationData.pGrossWeight);
                                //objCVarOperationContainersAndPackages.ChargeableWeight = pChargeableWeight;
                                //if (int.Parse(insertOperationData.pShipmentType) == 1 || int.Parse(insertOperationData.pShipmentType) == 3)//FCL or FTL (i.e. container load)
                                //{
                                //    objCVarOperationContainersAndPackages.NumberOfPackagesOnContainer = int.Parse(insertOperationData.pNumberOfPackages);
                                //    objCVarOperationContainersAndPackages.PackageTypeIDOnContainer = int.Parse(insertOperationData.pPackageTypeID);
                                //}
                                //else //air or (lcl or ltl)
                                //{
                                //    objCVarOperationContainersAndPackages.Quantity = int.Parse(insertOperationData.pNumberOfPackages);
                                //    objCVarOperationContainersAndPackages.PackageTypeID = int.Parse(insertOperationData.pPackageTypeID);
                                //}

                                objCVarOperationContainersAndPackagesTax.ContainerNumber = "0";
                                objCVarOperationContainersAndPackagesTax.CarrierSeal = "0";
                                objCVarOperationContainersAndPackagesTax.ShipperSeal = "0";
                                //objCVarOperationContainersAndPackages.TareWeight = pTareWeight;
                                //objCVarOperationContainersAndPackages.IsReefer = pIsReefer;
                                //objCVarOperationContainersAndPackages.IsNOR = pIsNOR;
                                //objCVarOperationContainersAndPackages.MinTemp = pMinTemp;
                                //objCVarOperationContainersAndPackages.MaxTemp = pMaxTemp;
                                //objCVarOperationContainersAndPackages.IsIMO = pIsIMO;
                                //objCVarOperationContainersAndPackages.IMOClass = pIMOClass;
                                //objCVarOperationContainersAndPackages.UNNumber = pUNNumber;
                                //objCVarOperationContainersAndPackages.FlashPoint = pFlashPoint;
                                objCVarOperationContainersAndPackagesTax.DescriptionOfGoods = "0"; // insertOperationData.pDescriptionOfGoods;
                                objCVarOperationContainersAndPackagesTax.MarksAndNumbers = "0";
                                objCVarOperationContainersAndPackagesTax.LotNumber = "0";
                                //objCVarOperationContainersAndPackages.PackageTypeIDOnContainer = pPackageTypeIDOnContainer;
                                //objCVarOperationContainersAndPackages.NumberOfPackagesOnContainer = pNumberOfPackagesOnContainer;
                                #region ContainerTracking
                                objCVarOperationContainersAndPackagesTax.GateOutPortID = 0;
                                objCVarOperationContainersAndPackagesTax.GateInPortID = 0;
                                objCVarOperationContainersAndPackagesTax.GateOutDate = DateTime.Parse("01/01/1900");
                                objCVarOperationContainersAndPackagesTax.StuffingDate = DateTime.Parse("01/01/1900");
                                objCVarOperationContainersAndPackagesTax.LoadingDate = DateTime.Parse("01/01/1900");
                                objCVarOperationContainersAndPackagesTax.GateOutAndLoadingDatesDifference = 0;
                                objCVarOperationContainersAndPackagesTax.Factory = "0";
                                objCVarOperationContainersAndPackagesTax.ExportBLNumber = "0";
                                objCVarOperationContainersAndPackagesTax.ImportBLNumber = "0";
                                objCVarOperationContainersAndPackagesTax.IsLoaded = false;
                                objCVarOperationContainersAndPackagesTax.IsTracked = false;
                                objCVarOperationContainersAndPackagesTax.IsOwnedByCompany = false;
                                objCVarOperationContainersAndPackagesTax.TrailerID = 0;
                                objCVarOperationContainersAndPackagesTax.DriverID = 0;
                                objCVarOperationContainersAndPackagesTax.DriverAssistantID = 0;
                                objCVarOperationContainersAndPackagesTax.SupplierTrailerName = "0";
                                objCVarOperationContainersAndPackagesTax.SupplierDriverName = "0";
                                objCVarOperationContainersAndPackagesTax.SupplierDriverAssistantName = "0";
                                #endregion ContainerTracking
                                #region AirAgents columns
                                objCVarOperationContainersAndPackagesTax.Rate = 0;
                                objCVarOperationContainersAndPackagesTax.IsAsAgreed = false;
                                objCVarOperationContainersAndPackagesTax.IsMinimum = false;
                                objCVarOperationContainersAndPackagesTax.WeightUnit = "0";
                                objCVarOperationContainersAndPackagesTax.RateClass = "0";
                                #endregion AirAgents columns

                                #region Tank
                                objCVarOperationContainersAndPackagesTax.TankOrFlexiNumber = "0";
                                objCVarOperationContainersAndPackagesTax.OperatorID = 0;

                                objCVarOperationContainersAndPackagesTax.IsFull = false;
                                objCVarOperationContainersAndPackagesTax.ExitDate = DateTime.Parse("01/01/1900");
                                objCVarOperationContainersAndPackagesTax.ReturnDate = DateTime.Parse("01/01/1900");
                                objCVarOperationContainersAndPackagesTax.FreeDays = 0;
                                objCVarOperationContainersAndPackagesTax.DayValue = 0;
                                #endregion Tank
                                #region Yard
                                objCVarOperationContainersAndPackagesTax.YardEIRNumber = 0;
                                objCVarOperationContainersAndPackagesTax.YardEIRNumberOut = 0;
                                objCVarOperationContainersAndPackagesTax.YardInDate = DateTime.Parse("01/01/1900");
                                objCVarOperationContainersAndPackagesTax.YardInTime = 0;
                                objCVarOperationContainersAndPackagesTax.YardOutDate = DateTime.Parse("01/01/1900");
                                objCVarOperationContainersAndPackagesTax.YardOutTime = 0;
                                objCVarOperationContainersAndPackagesTax.YardLocationID = 0;
                                objCVarOperationContainersAndPackagesTax.YardIsIn = 0;
                                #endregion Yard

                                objCVarOperationContainersAndPackagesTax.CreatorUserID = objCVarOperationContainersAndPackagesTax.ModificatorUserID = WebSecurity.CurrentUserId;
                                objCVarOperationContainersAndPackagesTax.CreationDate = objCVarOperationContainersAndPackagesTax.ModificationDate = DateTime.Now;

                                objCOperationContainersAndPackagesTax.lstCVarOperationContainersAndPackages.Add(objCVarOperationContainersAndPackagesTax);
                            }
                            checkException = objCOperationContainersAndPackagesTax.SaveMethod(objCOperationContainersAndPackagesTax.lstCVarOperationContainersAndPackages);
                            if (checkException == null)
                            {
                                //link
                                objCCustomizedDBCall.ExecuteQuery_DataTable("insertTaxLink " + OperationContainersAndPackagesID + "," + objCOperationContainersAndPackagesTax.lstCVarOperationContainersAndPackages[0].ID + "," + "OperationContainersAndPackages");

                            }
                        }
                        #endregion

                        #region Copy default AirLineChargeTypes
                        if (insertOperationData.pIsAWB)
                        {
                            CAirLineChargeTypes objCAirLineChargeTypes = new CAirLineChargeTypes();
                            objCAirLineChargeTypes.GetList("WHERE AirLineId=" + insertOperationData.pAirlineID + " AND IsDefault=1");
                            for (int i = 0; i < objCAirLineChargeTypes.lstCVarAirLineChargeTypes.Count; i++)
                            {
                                CVarPayablesTAX objCVarPayablesTax = new CVarPayablesTAX();
                                objCVarPayablesTax.OperationID = OperationID;
                                objCVarPayablesTax.ChargeTypeID = objCAirLineChargeTypes.lstCVarAirLineChargeTypes[i].ChargeTypeID;
                                objCVarPayablesTax.POrC = POrC;
                                objCVarPayablesTax.SupplierOperationPartnerID = 0;
                                objCVarPayablesTax.Quantity = 1;
                                objCVarPayablesTax.CostPrice = 0;
                                objCVarPayablesTax.AmountWithoutVAT = 0;
                                objCVarPayablesTax.TaxTypeID = 0;
                                objCVarPayablesTax.TaxPercentage = 0;
                                objCVarPayablesTax.TaxAmount = 0;
                                objCVarPayablesTax.DiscountTypeID = 0;
                                objCVarPayablesTax.DiscountPercentage = 0;
                                objCVarPayablesTax.DiscountAmount = 0;
                                objCVarPayablesTax.CostAmount = 0;
                                objCVarPayablesTax.PaidAmount = 0;
                                objCVarPayablesTax.RemainingAmount = 0;
                                objCVarPayablesTax.InitialSalePrice = 0;
                                objCVarPayablesTax.SupplierInvoiceNo = "0";
                                objCVarPayablesTax.EntryDate = DateTime.Now;
                                objCVarPayablesTax.BillID = 0;

                                objCVarPayablesTax.IssueDate = DateTime.Now;
                                objCVarPayablesTax.OperationContainersAndPackagesID = 0;

                                objCVarPayablesTax.ExchangeRate = 1;
                                objCVarPayablesTax.CurrencyID = objCvwDefaults.lstCVarvwDefaults[0].CurrencyID;
                                objCVarPayablesTax.GeneratingQRID = 0;
                                objCVarPayablesTax.Notes = "0";
                                objCVarPayablesTax.CustodyID = 0;
                                objCVarPayablesTax.SupplierReceiptNo = "0";
                                objCVarPayablesTax.AccNoteID = 0;
                                objCVarPayablesTax.IsDeleted = false;
                                objCVarPayablesTax.IsApproved = false;
                                objCVarPayablesTax.ApprovingUserID = 0;
                                objCVarPayablesTax.CreatorUserID = objCVarPayablesTax.ModificatorUserID = WebSecurity.CurrentUserId;
                                objCVarPayablesTax.CreationDate = objCVarPayablesTax.ModificationDate = DateTime.Now;
                                objCVarPayablesTax.JVID = 0;
                                objCVarPayablesTax.BillTo = 0;
                                objCVarPayablesTax.ReceivableID = 0;
                                objCVarPayablesTax.TaxAmount = 0;

                                CPayablesTAX objCPayablesTax = new CPayablesTAX();
                                objCPayablesTax.lstCVarPayables.Add(objCVarPayablesTax);
                                objCPayablesTax.SaveMethod(objCPayablesTax.lstCVarPayables);

                                if (checkException == null)
                                {
                                    //link
                                    objCCustomizedDBCall.ExecuteQuery_DataTable("insertTaxLink " + PayableID + "," + objCVarPayablesTax.ID + "," + "OperationContainersAndPackages");

                                }
                            } //for (int i = 0; i < objCAirLineChargeTypes.lstCVarAirLineChargeTypes.Count; i++)
                        }
                        #endregion Copy default AirLineChargeTypes
                    }

                    ////i cancelled it from here to save overload(i already check while uploading)
                    //#region create Operations Folder to upload DocsIn
                    //objCOperations.GetItem(objCOperations.lstCVarOperations[0].ID);

                    ////create new directory
                    ////string filePath = System.IO.Path.Combine(HttpRuntime.AppDomainAppPath, "DocsInFiles/" + objCOperations.lstCVarOperations[0].Code);
                    //string strNewFolderPath = HttpContext.Current.Server.MapPath("~/") + "DocsInFiles/" + objCOperations.lstCVarOperations[0].Code;
                    //if (!Directory.Exists(strNewFolderPath)) 
                    //    Directory.CreateDirectory(strNewFolderPath);

                    //#endregion create Operations Folder to upload DocsIn
                    objCOperationsTax.GetList("WHERE ID=" + objCOperationsTax.lstCVarOperations[0].ID);

                    #region CreateCostCenter
                    CSystemOptions objCSystemOptions = new CSystemOptions();
                    objCSystemOptions.GetList("Where OptionID=94");
                    if (objCSystemOptions.lstCVarSystemOptions.Count > 0 && objCSystemOptions.lstCVarSystemOptions[0].OptionValue
                          && objCOperations.lstCVarOperations[0].BLType != 2
                        )
                    {
                        string CostCenterNumberParent = "";
                        Int32 pID = 0;
                        //CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
                        CA_CostCentersTAX objCA_CostCentersParentTax = new CA_CostCentersTAX();
                        objCA_CostCentersParentTax.GetList("where ( CostCenterName=N'عمليات' or CostCenterName='Operations' ) and Parent_ID is null ");
                        if (objCA_CostCentersParentTax.lstCVarA_CostCenters.Count > 0)
                        {
                            pID = objCA_CostCentersParentTax.lstCVarA_CostCenters[0].ID;
                            CostCenterNumberParent = objCA_CostCentersParentTax.lstCVarA_CostCenters[0].RealCostCenterCode;
                        }

                        else
                        {

                            string pNewCodePartner = objCCustomizedDBCall.CallStringFunction("SELECT [dbo].A_CostCenters_GetCodeTax(" + (pID == 0 ? "null" : pID.ToString()) + ") AS Code");


                            CVarA_CostCentersTAX objCVarA_CostCentersTax = new CVarA_CostCentersTAX();
                            objCVarA_CostCentersTax.CostCenterNumber = pNewCodePartner.PadRight(12, '0');
                            objCVarA_CostCentersTax.CostCenterName = "Operations";
                            objCVarA_CostCentersTax.Parent_ID = 0;
                            objCVarA_CostCentersTax.IsMain = true;
                            objCVarA_CostCentersTax.CCLevel = 1;
                            objCVarA_CostCentersTax.RealCostCenterCode = pNewCodePartner;
                            objCVarA_CostCentersTax.User_ID = WebSecurity.CurrentUserId;
                            objCVarA_CostCentersTax.Type_ID = 0;
                            objCVarA_CostCentersTax.IsClosed = false;
                            objCVarA_CostCentersTax.SubAccountGroupID = 0;
                            objCVarA_CostCentersTax.EmployeesCount = 0;
                            objCA_CostCentersParentTax.lstCVarA_CostCenters.Add(objCVarA_CostCentersTax);
                            checkException = objCA_CostCentersParentTax.SaveMethod(objCA_CostCentersParentTax.lstCVarA_CostCenters);

                            pID = objCA_CostCentersParentTax.lstCVarA_CostCenters[0].ID;
                            CostCenterNumberParent = objCA_CostCentersParentTax.lstCVarA_CostCenters[0].RealCostCenterCode;
                        }

                        CA_CostCentersTAX objCA_CostCentersTax = new CA_CostCentersTAX();
                        checkException = objCA_CostCentersTax.GetListPaging(1, 1, "WHERE ID = " + pID.ToString(), "ID", out _RowCount);
                        string pNewCode = objCCustomizedDBCall.CallStringFunction("SELECT [dbo].A_CostCenters_GetCode(" + (pID == 0 ? "null" : pID.ToString()) + ") AS Code");

                        string pNewCodeNew = CostCenterNumberParent + pNewCode;
                        CVarA_CostCentersTAX objCVarA_CostCentersChildTax = new CVarA_CostCentersTAX();
                        objCVarA_CostCentersChildTax.CostCenterNumber = pNewCodeNew.PadRight(12, '0'); ;
                        objCVarA_CostCentersChildTax.CostCenterName = objCOperations.lstCVarOperations[0].Code;
                        objCVarA_CostCentersChildTax.Parent_ID = pID;
                        objCVarA_CostCentersChildTax.IsMain = false;
                        objCVarA_CostCentersChildTax.CCLevel = 2;
                        objCVarA_CostCentersChildTax.RealCostCenterCode = pNewCodeNew;
                        objCVarA_CostCentersChildTax.User_ID = WebSecurity.CurrentUserId;
                        objCVarA_CostCentersChildTax.Type_ID = 0;
                        objCVarA_CostCentersChildTax.IsClosed = false;
                        objCVarA_CostCentersChildTax.SubAccountGroupID = 0;
                        objCVarA_CostCentersChildTax.EmployeesCount = 0;
                        objCA_CostCentersParentTax.lstCVarA_CostCenters.Add(objCVarA_CostCentersChildTax);
                        checkException = objCA_CostCentersParentTax.SaveMethod(objCA_CostCentersParentTax.lstCVarA_CostCenters);

                        //Link Oeation With CostCenter Add by ahmed maher
                        objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount2); //i am sure i ve just one row isa
                        string CompanyName2 = objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName;
                        if (CompanyName2 == "BED")
                        {
                            CVarA_LinkOperationWithCostCenter objCVarA_LinkOperationWithCostCenter = new CVarA_LinkOperationWithCostCenter();

                            objCVarA_LinkOperationWithCostCenter.OperationID = objCOperations.lstCVarOperations[0].ID;
                            objCVarA_LinkOperationWithCostCenter.CostCenterID = objCVarA_CostCentersChildTax.ID;

                            CA_LinkOperationWithCostCenter objA_LinkOperationWithCostCenter = new CA_LinkOperationWithCostCenter();
                            objA_LinkOperationWithCostCenter.lstCVarA_LinkOperationWithCostCenter.Add(objCVarA_LinkOperationWithCostCenter);
                            checkException = objA_LinkOperationWithCostCenter.SaveMethod(objA_LinkOperationWithCostCenter.lstCVarA_LinkOperationWithCostCenter);
                        }
                    }


                    #endregion
                }

            }


            #endregion

            return new Object[] { _result
                , _MessageReturned == "" ? objCOperations.lstCVarOperations[0].ID : 0
                , _MessageReturned == "" ? objCOperations.lstCVarOperations[0].HouseNumber : null //pData[2]
                , _MessageReturned //pData[3]
            };
        }

        [HttpGet, HttpPost]
        public object[] Update([FromBody] UpdateOperationData updateOperationData)
        {
            bool _result = false;
            //int constClosedStageID = 120;
            string updateClause = "";
            COperations objCOperations = new COperations();
            string _MessageReturned = "";
            CDefaults objCDefaults = new CDefaults();
            int _RowCount = 0;
            objCDefaults.GetListPaging(1, 1, "", "ID", out _RowCount);
            #region Cheque HouseNumber uniqueness
            COperations objCOperations_CheckUniqueness = new COperations();
            if (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "EGL" || objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "SUN")
                objCOperations_CheckUniqueness.GetListPaging(999999, 1, "WHERE HouseNumber IS NOT NULL AND HouseNumber<>N'' AND HouseNumber<>N'N/A' AND HouseNumber=N'" + updateOperationData.pHouseNumber + "' AND ID<>" + updateOperationData.pID, "ID", out _RowCount);
            if (objCOperations_CheckUniqueness.lstCVarOperations.Count > 0)
                _MessageReturned = "This house number already exists.";
            #endregion Cheque HouseNumber uniqueness
            if (_MessageReturned == "")
            {
                //updateClause += (updateOperationData.pOpenDate == null ? " OpenDate = NULL " : " OpenDate = '" + Forwarding.MvcApp.Helpers.GetYYYYMMDDController.GetYYYYMMDDFormat(updateOperationData.pOpenDate.ToShortDateString(), 1) + "'");
                updateClause += (updateOperationData.pOpenDate == "" ? " OpenDate = NULL " : " OpenDate = '" + Forwarding.MvcApp.Helpers.DateFunctionsController.GetYYYYMMDDFormat(updateOperationData.pOpenDate, 2) + "'");
                //updateClause += (updateOperationData.pCloseDate == "" ? " , CloseDate = NULL " : " , CloseDate = '" + Forwarding.MvcApp.Helpers.DateFunctionsController.GetYYYYMMDDFormat(updateOperationData.pCloseDate, 2) + "'");
                updateClause += " , BranchID = " + (updateOperationData.pBranchID == 0 ? " NULL " : updateOperationData.pBranchID.ToString());
                updateClause += " , SalesmanID = " + (updateOperationData.pSalesmanID == 0 ? " NULL " : updateOperationData.pSalesmanID.ToString());
                updateClause += " , OperationManID = " + (updateOperationData.pOperationManID == 0 ? " NULL " : updateOperationData.pOperationManID.ToString());
                updateClause += " , IncotermID = " + (updateOperationData.pIncotermID == 0 ? " NULL " : updateOperationData.pIncotermID.ToString());
                updateClause += " , POrC = " + (updateOperationData.pPOrC == 0 ? " NULL " : updateOperationData.pPOrC.ToString());
                updateClause += " , CommodityID = " + (updateOperationData.pCommodityID == 0 ? " NULL " : updateOperationData.pCommodityID.ToString());
                updateClause += " , CommodityID2 = " + (updateOperationData.pCommodityID2 == 0 ? " NULL " : updateOperationData.pCommodityID2.ToString());
                updateClause += " , CommodityID3 = " + (updateOperationData.pCommodityID3 == 0 ? " NULL " : updateOperationData.pCommodityID3.ToString());
                //updateClause += " , NumberOfPackages = " + updateOperationData.pNumberOfPackages.ToString();
                //updateClause += " , ChargeableWeight = " + updateOperationData.pChargeableWeight.ToString();

                updateClause += " , MoveTypeID = " + (updateOperationData.pMoveTypeID == 0 ? " NULL " : updateOperationData.pMoveTypeID.ToString());
                updateClause += " , TransientTime = " + (updateOperationData.pTransientTime == 0 ? " NULL " : updateOperationData.pTransientTime.ToString());
                updateClause += " , HouseNumber = " + (updateOperationData.pHouseNumber == "0" ? " NULL " : "'" + updateOperationData.pHouseNumber + "'");
                ////OperationStageID: set just in case of closing date < TodaysDate
                //updateClause += (updateOperationData.pCloseDateAsDateTime < DateTime.Now ? " , OperationStageID = " + constClosedStageID.ToString() : "");
                updateClause += " , CustomerReference = " + (updateOperationData.pCustomerReference == "0" ? " NULL " : "N'" + updateOperationData.pCustomerReference + "' ");
                updateClause += " , SupplierReference = " + (updateOperationData.pSupplierReference == "0" ? " NULL " : "N'" + updateOperationData.pSupplierReference + "' ");

                updateClause += " , PONumber = " + (updateOperationData.pPONumber == "0" ? " NULL " : "N'" + updateOperationData.pPONumber + "' ");
                updateClause += updateOperationData.pPODate == "0" ? " ,PODate = NULL " : (" ,PODate = '" + (DateTime.ParseExact(updateOperationData.pPODate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd") + "'");
                updateClause += " , POValue = " + (updateOperationData.pPOValue == "0" ? " NULL " : "N'" + updateOperationData.pPOValue + "' ");
                updateClause += " , ReleaseNumber = " + (updateOperationData.pReleaseNumber == "0" ? " NULL " : "N'" + updateOperationData.pReleaseNumber + "' ");
                updateClause += updateOperationData.pReleaseDate == "0" ? " ,ReleaseDate = NULL " : (" ,ReleaseDate = '" + (DateTime.ParseExact(updateOperationData.pReleaseDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd") + "'");
                updateClause += updateOperationData.pForm13Date == "0" ? " ,Form13Date = NULL " : (" ,Form13Date = '" + (DateTime.ParseExact(updateOperationData.pForm13Date + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd") + "'");
                updateClause += " , ReleaseValue = " + (updateOperationData.pReleaseValue == 0 ? " NULL " : "N'" + updateOperationData.pReleaseValue + "' ");
                updateClause += " , DispatchNumber = " + (updateOperationData.pDispatchNumber == "0" ? " NULL " : "N'" + updateOperationData.pDispatchNumber + "' ");
                updateClause += " , BusinessUnit = " + (updateOperationData.pBusinessUnit == "0" ? " NULL " : "N'" + updateOperationData.pBusinessUnit + "' ");
                updateClause += " , Form13Number = " + (updateOperationData.pForm13Number == "0" ? " NULL " : "N'" + updateOperationData.pForm13Number + "' ");
                updateClause += " , ACIDNumber = " + (updateOperationData.pACIDNumber == "0" ? " NULL " : "N'" + updateOperationData.pACIDNumber + "' ");
                updateClause += " , ACIDDetails = " + (updateOperationData.pACIDNumberDetails == "0" ? " NULL " : "N'" + updateOperationData.pACIDNumberDetails + "' ");
                updateClause += " , IMOClass = " + (updateOperationData.pIMOClass == Decimal.Zero ? " NULL " : updateOperationData.pIMOClass.ToString());
                updateClause += " , UNNumber = " + (updateOperationData.pUNNumber == 0 ? " NULL " : updateOperationData.pUNNumber.ToString());
                updateClause += " , VesselID = " + (updateOperationData.pVesselID == 0 ? " NULL " : updateOperationData.pVesselID.ToString());
                updateClause += " , BookingNumber = " + (updateOperationData.pBookingNumber == "0" ? " NULL " : "N'" + updateOperationData.pBookingNumber + "' ");

                updateClause += " , AgreedRate = " + (updateOperationData.pAgreedRate == "0" ? " NULL " : "'" + updateOperationData.pAgreedRate + "' ");
                updateClause += " , Notes = " + (updateOperationData.pNotes.Trim() == "" ? " NULL " : "N'" + updateOperationData.pNotes.ToUpper().Trim() + "' ");
                updateClause += updateOperationData.pCutOffDate == "0" ? " ,CutOffDate = NULL " : (" ,CutOffDate = '" + (DateTime.ParseExact(updateOperationData.pCutOffDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd") + "'");

                updateClause += " , IsDelivered = " + (updateOperationData.pIsDelivered ? "1" : "0");
                updateClause += " , IsTrucking = " + (updateOperationData.pIsTrucking ? "1" : "0");
                updateClause += " , IsInsurance = " + (updateOperationData.pIsInsurance ? "1" : "0");
                updateClause += " , IsClearance = " + (updateOperationData.pIsClearance ? "1" : "0"); //Updated from clearance and used as IsVesselArrived
                updateClause += " , IsGenset = " + (updateOperationData.pIsGenset ? "1" : "0");
                updateClause += " , IsCourrier = " + (updateOperationData.pIsCourrier ? "1" : "0");
                updateClause += " , IsTelexRelease = " + (updateOperationData.pIsTelexRelease ? "1" : "0");
                updateClause += " , NetworkID = " + (updateOperationData.pNetworkID == 0 ? " NULL " : updateOperationData.pNetworkID.ToString());
                updateClause += " , NumberOfOriginalBills = " + (updateOperationData.pNumberOfOriginalBills == 0 ? " NULL " : "'" + updateOperationData.pNumberOfOriginalBills + "' ");

                updateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                ////next line to be deleted
                //updateClause += (updateOperationData.pOpenDate == "" ? " ,CreationDate = NULL " : " ,CreationDate = '" + Forwarding.MvcApp.Helpers.DateFunctionsController.GetYYYYMMDDFormat(updateOperationData.pOpenDate, 2) + "'");
                updateClause += " , ModificationDate = GETDATE() ";

                updateClause += " WHERE ID = " + updateOperationData.pID.ToString();
                Exception checkException = objCOperations.UpdateList(updateClause);
                if (checkException != null) // an exception is caught in the model
                {
                    if (checkException.Message.Contains("UNIQUE"))
                        _result = false;
                }
                else
                {//not unique
                    _result = true;
                    Operations_EmailNotification(updateOperationData.pID);
                }



            } //if (_MessageReturned == "")
              //Account

            int _RowCount2 = 0;
            Int32 SubAccountID_Return = 0;
            Int32 SubAccountID_Revenue = 0;
            Int32 SubAccountID_Expense = 0;

            Int32 supGroupID = 0;
            Int32 AccountID_Return = 0;
            Int32 AccountID_Revenue = 0;
            Int32 AccountID_Expense = 0;


            CvwDefaults objCvwDefaults = new CvwDefaults();
            objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount2); //i am sure i ve just one row isa
            string CompanyName = objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName;


            if (CompanyName == "CHM" || CompanyName == "OCE")
            {
                CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
                string OperationID = "";
                if (CompanyName == "CHM")
                {
                    OperationID = objCCustomizedDBCall.CallStringFunctionByMultiReturn("select top 1 TaxID from ForwardingTransChemTax.dbo.TaxLink WHERE notes='Operations' and OriginID=" + updateOperationData.pID.ToString());

                }
                else if (CompanyName == "OCE")
                {
                    OperationID = objCCustomizedDBCall.CallStringFunctionByMultiReturn("select top 1 TaxID from ForwardingTROTax.dbo.TaxLink WHERE notes='Operations' and OriginID= " + updateOperationData.pID.ToString());

                }

                COperationsTAX objCOperationsTax = new COperationsTAX();

                objCDefaults.GetListPaging(1, 1, "", "ID", out _RowCount);
                #region Cheque HouseNumber uniqueness
                COperationsTAX objCOperations_CheckUniquenessTax = new COperationsTAX();
                if (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "EGL" || objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "SUN")
                    objCOperations_CheckUniquenessTax.GetListPaging(999999, 1, "WHERE HouseNumber IS NOT NULL AND HouseNumber<>N'' AND HouseNumber<>N'N/A' AND HouseNumber=N'" + updateOperationData.pHouseNumber + "' AND ID<>" + updateOperationData.pID, "ID", out _RowCount);
                if (objCOperations_CheckUniquenessTax.lstCVarOperations.Count > 0)
                    _MessageReturned = "This house number already exists.";
                #endregion Cheque HouseNumber uniqueness
                if (_MessageReturned == "")
                {
                    updateClause = "";

                    //updateClause += (updateOperationData.pOpenDate == null ? " OpenDate = NULL " : " OpenDate = '" + Forwarding.MvcApp.Helpers.GetYYYYMMDDController.GetYYYYMMDDFormat(updateOperationData.pOpenDate.ToShortDateString(), 1) + "'");
                    updateClause += (updateOperationData.pOpenDate == "" ? " OpenDate = NULL " : " OpenDate = '" + Forwarding.MvcApp.Helpers.DateFunctionsController.GetYYYYMMDDFormat(updateOperationData.pOpenDate, 2) + "'");
                    //updateClause += (updateOperationData.pCloseDate == "" ? " , CloseDate = NULL " : " , CloseDate = '" + Forwarding.MvcApp.Helpers.DateFunctionsController.GetYYYYMMDDFormat(updateOperationData.pCloseDate, 2) + "'");
                    updateClause += " , BranchID = " + (updateOperationData.pBranchID == 0 ? " NULL " : updateOperationData.pBranchID.ToString());
                    updateClause += " , SalesmanID = " + (updateOperationData.pSalesmanID == 0 ? " NULL " : updateOperationData.pSalesmanID.ToString());
                    updateClause += " , OperationManID = " + (updateOperationData.pOperationManID == 0 ? " NULL " : updateOperationData.pOperationManID.ToString());
                    updateClause += " , IncotermID = " + (updateOperationData.pIncotermID == 0 ? " NULL " : updateOperationData.pIncotermID.ToString());
                    updateClause += " , POrC = " + (updateOperationData.pPOrC == 0 ? " NULL " : updateOperationData.pPOrC.ToString());
                    updateClause += " , CommodityID = " + (updateOperationData.pCommodityID == 0 ? " NULL " : updateOperationData.pCommodityID.ToString());
                    //updateClause += " , NumberOfPackages = " + updateOperationData.pNumberOfPackages.ToString();
                    //updateClause += " , ChargeableWeight = " + updateOperationData.pChargeableWeight.ToString();

                    updateClause += " , MoveTypeID = " + (updateOperationData.pMoveTypeID == 0 ? " NULL " : updateOperationData.pMoveTypeID.ToString());
                    updateClause += " , TransientTime = " + (updateOperationData.pTransientTime == 0 ? " NULL " : updateOperationData.pTransientTime.ToString());
                    updateClause += " , HouseNumber = " + (updateOperationData.pHouseNumber == "0" ? " NULL " : "'" + updateOperationData.pHouseNumber + "'");
                    ////OperationStageID: set just in case of closing date < TodaysDate
                    //updateClause += (updateOperationData.pCloseDateAsDateTime < DateTime.Now ? " , OperationStageID = " + constClosedStageID.ToString() : "");
                    updateClause += " , CustomerReference = " + (updateOperationData.pCustomerReference == "0" ? " NULL " : "N'" + updateOperationData.pCustomerReference + "' ");
                    updateClause += " , SupplierReference = " + (updateOperationData.pSupplierReference == "0" ? " NULL " : "N'" + updateOperationData.pSupplierReference + "' ");

                    updateClause += " , PONumber = " + (updateOperationData.pPONumber == "0" ? " NULL " : "N'" + updateOperationData.pPONumber + "' ");
                    updateClause += updateOperationData.pPODate == "0" ? " ,PODate = NULL " : (" ,PODate = '" + (DateTime.ParseExact(updateOperationData.pPODate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd") + "'");
                    updateClause += " , POValue = " + (updateOperationData.pPOValue == "0" ? " NULL " : "N'" + updateOperationData.pPOValue + "' ");
                    updateClause += " , ReleaseNumber = " + (updateOperationData.pReleaseNumber == "0" ? " NULL " : "N'" + updateOperationData.pReleaseNumber + "' ");
                    updateClause += updateOperationData.pReleaseDate == "0" ? " ,ReleaseDate = NULL " : (" ,ReleaseDate = '" + (DateTime.ParseExact(updateOperationData.pReleaseDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd") + "'");
                    updateClause += " , ReleaseValue = " + (updateOperationData.pReleaseValue == 0 ? " NULL " : "N'" + updateOperationData.pReleaseValue + "' ");
                    updateClause += " , DispatchNumber = " + (updateOperationData.pDispatchNumber == "0" ? " NULL " : "N'" + updateOperationData.pDispatchNumber + "' ");
                    updateClause += " , BusinessUnit = " + (updateOperationData.pBusinessUnit == "0" ? " NULL " : "N'" + updateOperationData.pBusinessUnit + "' ");
                    updateClause += " , Form13Number = " + (updateOperationData.pForm13Number == "0" ? " NULL " : "N'" + updateOperationData.pForm13Number + "' ");
                    updateClause += " , ACIDNumber = " + (updateOperationData.pACIDNumber == "0" ? " NULL " : "N'" + updateOperationData.pACIDNumber + "' ");

                    updateClause += " , AgreedRate = " + (updateOperationData.pAgreedRate == "0" ? " NULL " : "'" + updateOperationData.pAgreedRate + "' ");
                    updateClause += " , Notes = " + (updateOperationData.pNotes.Trim() == "" ? " NULL " : "N'" + updateOperationData.pNotes.ToUpper().Trim() + "' ");
                    updateClause += updateOperationData.pCutOffDate == "0" ? " ,CutOffDate = NULL " : (" ,CutOffDate = '" + (DateTime.ParseExact(updateOperationData.pCutOffDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd") + "'");

                    updateClause += " , IsDelivered = " + (updateOperationData.pIsDelivered ? "1" : "0");
                    updateClause += " , IsTrucking = " + (updateOperationData.pIsTrucking ? "1" : "0");
                    updateClause += " , IsInsurance = " + (updateOperationData.pIsInsurance ? "1" : "0");
                    updateClause += " , IsClearance = " + (updateOperationData.pIsClearance ? "1" : "0"); //Updated from clearance and used as IsVesselArrived
                    updateClause += " , IsGenset = " + (updateOperationData.pIsGenset ? "1" : "0");
                    updateClause += " , IsCourrier = " + (updateOperationData.pIsCourrier ? "1" : "0");
                    updateClause += " , IsTelexRelease = " + (updateOperationData.pIsTelexRelease ? "1" : "0");
                    updateClause += " , NetworkID = " + (updateOperationData.pNetworkID == 0 ? " NULL " : updateOperationData.pNetworkID.ToString());
                    updateClause += " , NumberOfOriginalBills = " + (updateOperationData.pNumberOfOriginalBills == 0 ? " NULL " : "'" + updateOperationData.pNumberOfOriginalBills + "' ");

                    updateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                    ////next line to be deleted
                    //updateClause += (updateOperationData.pOpenDate == "" ? " ,CreationDate = NULL " : " ,CreationDate = '" + Forwarding.MvcApp.Helpers.DateFunctionsController.GetYYYYMMDDFormat(updateOperationData.pOpenDate, 2) + "'");
                    updateClause += " , ModificationDate = GETDATE() ";

                    updateClause += " WHERE ID = " + OperationID;
                    Exception checkException = objCOperationsTax.UpdateList(updateClause);
                    if (checkException != null) // an exception is caught in the model
                    {
                        if (checkException.Message.Contains("UNIQUE"))
                            _result = false;
                    }
                    else
                    {//not unique
                        _result = true;
                        Operations_EmailNotification(updateOperationData.pID);
                    }



                } //if (_MessageReturned == "")
            }
            return new Object[] {
                _result
                , updateOperationData.pID
                , _MessageReturned //pData[2] 
            };
        }

        [HttpGet, HttpPost]
        public object[] ApproveService(Int64 pOperationID, string pServiceToApprove, Int32 pIsApprove)
        {
            string _ReturnedMessage = "";
            string _UpdateClause = "";
            int _RowCount = 0;
            Exception checkException = null;
            COperations objCOperations = new COperations();

            _UpdateClause = "Is" + pServiceToApprove + "Approved = " + pIsApprove + " \n";
            _UpdateClause += ", " + pServiceToApprove + "ApprovalDate = GETDATE()" + " \n";
            _UpdateClause += "WHERE ID=" + pOperationID;
            checkException = objCOperations.UpdateList(_UpdateClause);
            checkException = objCOperations.GetListPaging(999999, 1, "WHERE ID=" + pOperationID, "ID", out _RowCount);
            var serilizer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[]
            {
                _ReturnedMessage
                , serilizer.Serialize(objCOperations.lstCVarOperations[0])
            };
        }
        [HttpGet, HttpPost]
        public object[] ChangeOperationType(Int64 pOperationID, string pNewOperationCode, int pBLType, string pBLTypeIconName, string pBLTypeIconStyle, int pDirectionType, string pDirectionIconName, string pDirectionIconStyle, int pTransportType, string pTransportIconName, string pTransportIconStyle, int pShipmentType)
        {
            Exception checkException = null;
            Int16 MainCarraigeRoutingTypeID = 30;

            int constImportDirectionType = 1;
            int constMasterBLType = 3;

            string pUpdateClause = "";
            pUpdateClause += " Code = '" + pNewOperationCode + "' ";
            pUpdateClause += " , BLType = '" + pBLType.ToString() + "' ";

            //if (pBLType == constMasterBLType) //client is agent
            //{
            //    pUpdateClause += " , ConsigneeID = NULL ";
            //    pUpdateClause += " , ConsigneeContactID = NULL ";
            //    pUpdateClause += " , ShipperID = NULL ";
            //    pUpdateClause += " , ShipperContactID = NULL ";
            //}
            //else if (pBLType != constMasterBLType && pDirectionType == constImportDirectionType) //client is consignee
            //{
            //    pUpdateClause += " , ShipperID = NULL ";
            //    pUpdateClause += " , ShipperContactID = NULL ";
            //    pUpdateClause += " , AgentID = NULL ";
            //    pUpdateClause += " , AgentContactID = NULL ";
            //    pUpdateClause += " , NetworkID = NULL ";
            //}
            //else //client is shipper
            //{
            //    pUpdateClause += " , ConsigneeID = NULL ";
            //    pUpdateClause += " , ConsigneeContactID = NULL ";
            //    pUpdateClause += " , AgentID = NULL ";
            //    pUpdateClause += " , AgentContactID = NULL ";
            //    pUpdateClause += " , NetworkID = NULL ";
            //}
            pUpdateClause += " , BLTypeIconName = '" + pBLTypeIconName + "' ";
            pUpdateClause += " , BLTypeIconStyle = '" + pBLTypeIconStyle + "' ";
            pUpdateClause += " , DirectionType = '" + pDirectionType.ToString() + "' ";
            pUpdateClause += " , DirectionIconName = '" + pDirectionIconName + "' ";
            pUpdateClause += " , DirectionIconStyle = '" + pDirectionIconStyle + "' ";
            pUpdateClause += " , TransportType = '" + pTransportType.ToString() + "' ";
            pUpdateClause += " , TransportIconName = '" + pTransportIconName + "' ";
            pUpdateClause += " , TransportIconStyle = '" + pTransportIconStyle + "' ";
            pUpdateClause += " , ShipmentType = '" + pShipmentType.ToString() + "' ";
            pUpdateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
            pUpdateClause += " , ModificationDate = GETDATE() ";
            pUpdateClause += " WHERE ID = " + pOperationID.ToString();

            COperations objCOperations = new COperations();
            checkException = objCOperations.UpdateList(pUpdateClause);
            if (checkException == null)
            {
                #region Reset Operation Routing to allow changing the line 

                pUpdateClause = " TransportType = '" + pTransportType.ToString() + "' ";
                pUpdateClause += " , TransportIconName = '" + pTransportIconName + "' ";
                pUpdateClause += " , TransportIconStyle = '" + pTransportIconStyle + "' ";
                if (pTransportType == 1) //Ocean
                {
                    pUpdateClause += " , AirlineID = null";
                    pUpdateClause += " , TruckerID = null ";
                }
                else if (pTransportType == 2) //Air
                {
                    pUpdateClause += " , VesselID = null";
                    pUpdateClause += " , ShippingLineID = null ";
                    pUpdateClause += " , TruckerID = null ";
                }
                else if (pTransportType == 3) //Inland
                {
                    pUpdateClause += " , VesselID = null";
                    pUpdateClause += " , ShippingLineID = null ";
                    pUpdateClause += " , AirlineID = null ";
                }
                pUpdateClause += " WHERE OperationID = " + pOperationID.ToString() + " AND RoutingTypeID = " + MainCarraigeRoutingTypeID.ToString();

                CRoutings objCRoutings = new CRoutings();
                checkException = objCRoutings.UpdateList(pUpdateClause);
                #endregion
            }

            return new object[] { };
        }

        [HttpGet, HttpPost]
        public object[] CopyOperation(Int64 pOperationToCopyID, bool pIncludePayables, bool pIncludeReceivables)
        {
            COperations objCOperationToCopy = new COperations();
            COperations objCOperations = new COperations();
            objCOperationToCopy.GetItem(pOperationToCopyID);
            CCustomers objCCustomers = new CCustomers();
            string pWhereClauseCurrencyDetails = "";
            CvwCurrencyDetails objCvwCurrencyDetails = new CvwCurrencyDetails();
            int _RowCount = 0;
            CDefaults objCDefaults = new CDefaults();
            objCDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa
            int DaysBeforeClose = 14;//default for default which is not handled
            if (objCOperationToCopy.lstCVarOperations[0].DirectionType == 1/*Import*/ && objCOperationToCopy.lstCVarOperations[0].TransportType == 1/*Ocean*/) //ImportOceanDays
                DaysBeforeClose = objCDefaults.lstCVarDefaults[0].ImportOceanDays;
            else if (objCOperationToCopy.lstCVarOperations[0].DirectionType == 1/*Import*/ && objCOperationToCopy.lstCVarOperations[0].TransportType == 2/*Air*/) //ImportAirDays
                DaysBeforeClose = objCDefaults.lstCVarDefaults[0].ImportAirDays;
            else if (objCOperationToCopy.lstCVarOperations[0].DirectionType == 1/*Import*/ && objCOperationToCopy.lstCVarOperations[0].TransportType == 3/*Inland*/) //ImportInlandDays
                DaysBeforeClose = objCDefaults.lstCVarDefaults[0].ImportInlandDays;
            else if (objCOperationToCopy.lstCVarOperations[0].DirectionType == 2/*Export*/ && objCOperationToCopy.lstCVarOperations[0].TransportType == 1/*Ocean*/) //ExportOceanDays
                DaysBeforeClose = objCDefaults.lstCVarDefaults[0].ExportOceanDays;
            else if (objCOperationToCopy.lstCVarOperations[0].DirectionType == 2/*Export*/ && objCOperationToCopy.lstCVarOperations[0].TransportType == 2/*Air*/) //ExportAirDays
                DaysBeforeClose = objCDefaults.lstCVarDefaults[0].ExportAirDays;
            else if (objCOperationToCopy.lstCVarOperations[0].DirectionType == 2/*Export*/ && objCOperationToCopy.lstCVarOperations[0].TransportType == 3/*Inland*/) //ExportInlandDays
                DaysBeforeClose = objCDefaults.lstCVarDefaults[0].ExportInlandDays;
            else if (objCOperationToCopy.lstCVarOperations[0].DirectionType == 2/*Domestic*/ && objCOperationToCopy.lstCVarOperations[0].TransportType == 1/*Ocean*/) //DomesticOceanDays
                DaysBeforeClose = objCDefaults.lstCVarDefaults[0].DomesticOceanDays;
            else if (objCOperationToCopy.lstCVarOperations[0].DirectionType == 2/*Domestic*/ && objCOperationToCopy.lstCVarOperations[0].TransportType == 2/*Air*/) //DomesticAirDays
                DaysBeforeClose = objCDefaults.lstCVarDefaults[0].DomesticAirDays;
            else if (objCOperationToCopy.lstCVarOperations[0].DirectionType == 2/*Domestic*/ && objCOperationToCopy.lstCVarOperations[0].TransportType == 3/*Inland*/) //DomesticInlandDays
                DaysBeforeClose = objCDefaults.lstCVarDefaults[0].DomesticInlandDays;

            bool _result = true;
            int MainCarraigeRoutingTypeID = 30;
            int OpenQuoteAndOperStageID = 60;
            CVarOperations objCVarOperations = new CVarOperations();

            objCCustomers.GetListPaging(1, 1, "WHERE IsInactive=1 and ID IN (" + objCOperationToCopy.lstCVarOperations[0].ShipperID + "," + objCOperationToCopy.lstCVarOperations[0].ConsigneeID + ")", "ID", out _RowCount);
            if (objCCustomers.lstCVarCustomers.Count > 0)
            {
                _result = false;
                //strMessageReturned = "This quotation has an inactive partner";
            }
            else
            {
                #region Copying the operation itself
                objCVarOperations.Code = "O" + DateTime.Now.Year.ToString().Substring(2, 2) + "-"
                    + (objCOperationToCopy.lstCVarOperations[0].DirectionType == 1 ? "IMP" : (objCOperationToCopy.lstCVarOperations[0].DirectionType == 2 ? "EXP" : "DOM")) + "-"
                    + (objCOperationToCopy.lstCVarOperations[0].TransportType == 1 ? "OC" : (objCOperationToCopy.lstCVarOperations[0].TransportType == 2 ? "AI" : "IN")) + "-"
                    ;
                objCVarOperations.HouseNumber = "0";//if HouseNumber is not null then its entered manually
                objCVarOperations.BranchID = objCOperationToCopy.lstCVarOperations[0].BranchID;
                objCVarOperations.SalesmanID = objCOperationToCopy.lstCVarOperations[0].SalesmanID;
                objCVarOperations.BLType = objCOperationToCopy.lstCVarOperations[0].BLType;
                objCVarOperations.BLTypeIconName = objCOperationToCopy.lstCVarOperations[0].BLTypeIconName;
                objCVarOperations.BLTypeIconStyle = objCOperationToCopy.lstCVarOperations[0].BLTypeIconStyle;
                objCVarOperations.DirectionType = objCOperationToCopy.lstCVarOperations[0].DirectionType;
                objCVarOperations.DirectionIconName = objCOperationToCopy.lstCVarOperations[0].DirectionIconName;
                objCVarOperations.DirectionIconStyle = objCOperationToCopy.lstCVarOperations[0].DirectionIconStyle;
                objCVarOperations.TransportType = objCOperationToCopy.lstCVarOperations[0].TransportType;
                objCVarOperations.TransportIconName = objCOperationToCopy.lstCVarOperations[0].TransportIconName;
                objCVarOperations.TransportIconStyle = objCOperationToCopy.lstCVarOperations[0].TransportIconStyle;
                objCVarOperations.ShipmentType = objCOperationToCopy.lstCVarOperations[0].ShipmentType;
                objCVarOperations.MasterBL = "0";
                objCVarOperations.MAWBSuffix = "0";
                objCVarOperations.BLDate = DateTime.Parse("01-01-1900");
                objCVarOperations.HBLDate = DateTime.Parse("01-01-1900");
                objCVarOperations.ReleaseDate = DateTime.Parse("01-01-1900");
                objCVarOperations.Form13Date = DateTime.Parse("01-01-1900");
                objCVarOperations.PODate = DateTime.Parse("01-01-1900");

                objCVarOperations.ClearanceApprovalDate = DateTime.Parse("01-01-1900");
                objCVarOperations.TruckingApprovalDate = DateTime.Parse("01-01-1900");
                objCVarOperations.FreightApprovalDate = DateTime.Parse("01-01-1900");

                objCVarOperations.ShippedOnBoardDate = DateTime.Parse("01-01-1900");
                objCVarOperations.FreightPayableAt = "0";
                objCVarOperations.CertificateNumber = "0";
                objCVarOperations.CountryOfOrigin = "0";
                objCVarOperations.InvoiceValue = "0";
                objCVarOperations.NumberOfOriginalBills = 0;

                objCVarOperations.ShipperID = objCOperationToCopy.lstCVarOperations[0].ShipperID;
                objCVarOperations.ShipperAddressID = objCOperationToCopy.lstCVarOperations[0].ShipperAddressID;
                objCVarOperations.ShipperContactID = objCOperationToCopy.lstCVarOperations[0].ShipperContactID;
                objCVarOperations.ConsigneeID = objCOperationToCopy.lstCVarOperations[0].ConsigneeID;
                objCVarOperations.ConsigneeAddressID = objCOperationToCopy.lstCVarOperations[0].ConsigneeAddressID;
                objCVarOperations.ConsigneeContactID = objCOperationToCopy.lstCVarOperations[0].ConsigneeContactID;
                objCVarOperations.AgentID = objCOperationToCopy.lstCVarOperations[0].AgentID;
                objCVarOperations.AgentAddressID = objCOperationToCopy.lstCVarOperations[0].AgentAddressID;
                objCVarOperations.AgentContactID = objCOperationToCopy.lstCVarOperations[0].AgentContactID;
                objCVarOperations.IncotermID = objCOperationToCopy.lstCVarOperations[0].IncotermID;
                objCVarOperations.POrC = objCOperationToCopy.lstCVarOperations[0].POrC;
                objCVarOperations.MoveTypeID = objCOperationToCopy.lstCVarOperations[0].MoveTypeID;
                objCVarOperations.CommodityID = objCOperationToCopy.lstCVarOperations[0].CommodityID;
                objCVarOperations.CommodityID2 = objCOperationToCopy.lstCVarOperations[0].CommodityID2;
                objCVarOperations.CommodityID3 = objCOperationToCopy.lstCVarOperations[0].CommodityID3;
                objCVarOperations.TransientTime = objCOperationToCopy.lstCVarOperations[0].TransientTime;
                objCVarOperations.OpenDate = DateTime.Now;
                objCVarOperations.CloseDate = DateTime.Now.AddDays(DaysBeforeClose); //DateTime.Parse("01-01-1900");
                objCVarOperations.CutOffDate = DateTime.Parse("01-01-1900");
                objCVarOperations.IncludePickup = objCOperationToCopy.lstCVarOperations[0].IncludePickup;
                objCVarOperations.PickupCityID = objCOperationToCopy.lstCVarOperations[0].PickupCityID;
                objCVarOperations.PickupAddressID = objCOperationToCopy.lstCVarOperations[0].PickupAddressID;
                objCVarOperations.POLCountryID = objCOperationToCopy.lstCVarOperations[0].POLCountryID;
                objCVarOperations.POL = objCOperationToCopy.lstCVarOperations[0].POL;
                objCVarOperations.PODCountryID = objCOperationToCopy.lstCVarOperations[0].PODCountryID;
                objCVarOperations.POD = objCOperationToCopy.lstCVarOperations[0].POD;
                objCVarOperations.PickupAddress = objCOperationToCopy.lstCVarOperations[0].PickupAddress;
                objCVarOperations.DeliveryAddress = objCOperationToCopy.lstCVarOperations[0].DeliveryAddress;
                objCVarOperations.ShippingLineID = objCOperationToCopy.lstCVarOperations[0].ShippingLineID;
                objCVarOperations.AirlineID = objCOperationToCopy.lstCVarOperations[0].AirlineID;
                objCVarOperations.TruckerID = objCOperationToCopy.lstCVarOperations[0].TruckerID;
                objCVarOperations.IncludeDelivery = objCOperationToCopy.lstCVarOperations[0].IncludeDelivery;
                objCVarOperations.DeliveryZipCode = objCOperationToCopy.lstCVarOperations[0].DeliveryZipCode;
                objCVarOperations.DeliveryCityID = objCOperationToCopy.lstCVarOperations[0].DeliveryCityID;
                objCVarOperations.DeliveryCountryID = objCOperationToCopy.lstCVarOperations[0].DeliveryCountryID;
                //objCVarOperations.GrossWeight = decimal.Parse(insertOperationData.pGrossWeight);
                //objCVarOperations.Volume = decimal.Parse(insertOperationData.pVolume);
                //objCVarOperations.ChargeableWeight = decimal.Parse(insertOperationData.pChargeableWeight);
                //objCVarOperations.NumberOfPackages = int.Parse(insertOperationData.pNumberOfPackages);
                objCVarOperations.IsDangerousGoods = objCOperationToCopy.lstCVarOperations[0].IsDangerousGoods;
                objCVarOperations.Notes = objCOperationToCopy.lstCVarOperations[0].Notes;
                objCVarOperations.CustomerReference = "0";
                objCVarOperations.SupplierReference = "0";
                objCVarOperations.PONumber = "0";
                objCVarOperations.POValue = "0";
                objCVarOperations.ReleaseNumber = "0";
                objCVarOperations.DispatchNumber = "0";
                objCVarOperations.BusinessUnit = "0";
                objCVarOperations.Form13Number = "0";
                objCVarOperations.AgreedRate = "0";
                objCVarOperations.IsDelivered = objCOperationToCopy.lstCVarOperations[0].IsDelivered;
                objCVarOperations.IsTrucking = objCOperationToCopy.lstCVarOperations[0].IsTrucking;
                objCVarOperations.IsInsurance = objCOperationToCopy.lstCVarOperations[0].IsInsurance;
                objCVarOperations.IsClearance = objCOperationToCopy.lstCVarOperations[0].IsClearance;
                objCVarOperations.IsGenset = objCOperationToCopy.lstCVarOperations[0].IsGenset;
                objCVarOperations.IsCourrier = objCOperationToCopy.lstCVarOperations[0].IsCourrier;
                objCVarOperations.IsTelexRelease = objCOperationToCopy.lstCVarOperations[0].IsTelexRelease;
                objCVarOperations.OperationStageID = OpenQuoteAndOperStageID;
                objCVarOperations.NumberOfHousesConnected = 0;
                objCVarOperations.CreatorUserID = objCVarOperations.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarOperations.CreationDate = objCVarOperations.ModificationDate = DateTime.Now;

                #region Venus fields A.Medra
                objCVarOperations.BLDate = DateTime.Parse("01/01/1900");
                objCVarOperations.MAWBStockID = 0;
                objCVarOperations.TypeOfStockID = 0;
                objCVarOperations.FlightNo = "0";
                objCVarOperations.POrC = 0;
                objCVarOperations.IsAWB = objCOperationToCopy.lstCVarOperations[0].IsAWB;
                //Fields not in insert
                objCVarOperations.AirLineID1 = 0;
                objCVarOperations.FlightNo1 = "0";
                objCVarOperations.FlightDate1 = DateTime.Parse("01/01/1900");
                objCVarOperations.AirLineID2 = 0;
                objCVarOperations.FlightNo2 = "0";
                objCVarOperations.FlightDate2 = DateTime.Parse("01/01/1900");
                objCVarOperations.AirLineID3 = 0;
                objCVarOperations.FlightNo3 = "0";
                objCVarOperations.FlightDate3 = DateTime.Parse("01/01/1900");

                objCVarOperations.UNOrID = "0";
                objCVarOperations.ProperShippingName = "0";
                objCVarOperations.ClassOrDivision = "0";
                objCVarOperations.PackingGroup = "0";
                objCVarOperations.QuantityAndTypeOfPacking = "0";
                objCVarOperations.PackingInstruction = "0";
                objCVarOperations.ShippingDeclarationAuthorization = "0";
                objCVarOperations.Barcode = "0";

                objCVarOperations.GuaranteeLetterNumber = "0";
                objCVarOperations.GuaranteeLetterDate = DateTime.Parse("01/01/1900");
                objCVarOperations.GuaranteeLetterAmount = "0";
                objCVarOperations.GuaranteeLetterSupplierInvoiceNumber = "0";
                objCVarOperations.BankAccountID = 0;
                objCVarOperations.GuaranteeLetterNotes = "0";

                objCVarOperations.HandlingInformation = "0";
                objCVarOperations.AmountOfInsurance = "0";
                objCVarOperations.DeclaredValueForCarriage = "0";
                objCVarOperations.WeightCharge = 0;
                objCVarOperations.ValuationCharge = 0;
                objCVarOperations.OtherChargesDueAgent = 0;
                objCVarOperations.OtherCharges = "0";
                objCVarOperations.CurrencyID = objCOperationToCopy.lstCVarOperations[0].CurrencyID;
                objCVarOperations.AccountingInformation = "0";
                objCVarOperations.ReferenceNumber = "0";
                objCVarOperations.OptionalShippingInformation = "0";
                objCVarOperations.CHGSCode = "0";
                objCVarOperations.WT_VALL_Other = "0";
                objCVarOperations.DeclaredValueForCustoms = "0";
                objCVarOperations.Tax = 0;
                objCVarOperations.OtherChargesDueCarrier = 0;
                objCVarOperations.WT_VALL = "0";
                objCVarOperations.MarksAndNumbers = "0";
                objCVarOperations.Description = "0";
                objCVarOperations.IsAWB = objCOperationToCopy.lstCVarOperations[0].IsAWB;
                #endregion Venus fields A.Medra

                objCVarOperations.DismissalPermissionSerial = "0";
                objCVarOperations.DeliveryOrderSerial = "0";

                objCVarOperations.eFBLID = "0";
                objCVarOperations.eFBLStatus = 0;

                objCVarOperations.IMOClass = Decimal.Zero;
                objCVarOperations.UNNumber = 0;
                objCVarOperations.VesselID = 0;
                objCVarOperations.BookingNumber = "0";
                objCVarOperations.ACIDNumber = "0";
                objCVarOperations.ACIDDetails = "0";
                objCVarOperations.HouseParentID = 0;

                objCOperations.lstCVarOperations.Add(objCVarOperations);
                Exception checkException = objCOperations.SaveMethod(objCOperations.lstCVarOperations);

                #endregion copying the operation itself

                #region Copy OperationPartners //COPY Partners To OperationPartners
                COperationPartners objCOperationPartnersToCopy = new COperationPartners();
                objCOperationPartnersToCopy.GetList(" WHERE OperationID = " + pOperationToCopyID.ToString());

                COperationPartners objCOperationPartners = new COperationPartners();
                for (int i = 0; i < objCOperationPartnersToCopy.lstCVarOperationPartners.Count; i++)
                {
                    CVarOperationPartners objCVarOperationPartners = new CVarOperationPartners();
                    objCVarOperationPartners.OperationID = objCOperations.lstCVarOperations[0].ID;
                    objCVarOperationPartners.OperationPartnerTypeID = objCOperationPartnersToCopy.lstCVarOperationPartners[i].OperationPartnerTypeID;
                    objCVarOperationPartners.CustomerID = objCOperationPartnersToCopy.lstCVarOperationPartners[i].CustomerID;
                    objCVarOperationPartners.AgentID = objCOperationPartnersToCopy.lstCVarOperationPartners[i].AgentID;
                    objCVarOperationPartners.ShippingAgentID = objCOperationPartnersToCopy.lstCVarOperationPartners[i].ShippingAgentID;
                    objCVarOperationPartners.CustomsClearanceAgentID = objCOperationPartnersToCopy.lstCVarOperationPartners[i].CustomsClearanceAgentID;
                    objCVarOperationPartners.ShippingLineID = objCOperationPartnersToCopy.lstCVarOperationPartners[i].ShippingLineID;
                    objCVarOperationPartners.AirlineID = objCOperationPartnersToCopy.lstCVarOperationPartners[i].AirlineID;
                    objCVarOperationPartners.TruckerID = objCOperationPartnersToCopy.lstCVarOperationPartners[i].TruckerID;
                    objCVarOperationPartners.SupplierID = objCOperationPartnersToCopy.lstCVarOperationPartners[i].SupplierID;
                    objCVarOperationPartners.ContactID = objCOperationPartnersToCopy.lstCVarOperationPartners[i].ContactID;
                    objCVarOperationPartners.IsOperationClient = objCOperationPartnersToCopy.lstCVarOperationPartners[i].IsOperationClient;
                    objCVarOperationPartners.CreatorUserID = objCVarOperationPartners.ModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarOperationPartners.CreationDate = objCVarOperationPartners.ModificationDate = DateTime.Now;
                    objCOperationPartners.lstCVarOperationPartners.Add(objCVarOperationPartners);
                }
                objCOperationPartners.SaveMethod(objCOperationPartners.lstCVarOperationPartners);

                #endregion Copy Operation Partners

                #region Copy OperationRoutings
                CRoutings objCRoutingsToCopy = new CRoutings();
                int _TempRowCount;
                //objCRoutingsToCopy.GetList(" WHERE OperationID = " + pOperationToCopyID.ToString());
                objCRoutingsToCopy.GetListPaging(999999, 1, " WHERE RoutingTypeID=" + MainCarraigeRoutingTypeID + " AND OperationID = " + pOperationToCopyID.ToString(), "ID", out _TempRowCount);

                CRoutings objCRoutings = new CRoutings();
                for (int i = 0; i < objCRoutingsToCopy.lstCVarRoutings.Count; i++)
                {
                    CVarRoutings objCVarRoutings = new CVarRoutings();
                    objCVarRoutings.OperationID = objCOperations.lstCVarOperations[0].ID;
                    objCVarRoutings.RoutingTypeID = objCRoutingsToCopy.lstCVarRoutings[i].RoutingTypeID;
                    objCVarRoutings.TransportType = objCRoutingsToCopy.lstCVarRoutings[i].TransportType;
                    objCVarRoutings.TransportIconName = objCRoutingsToCopy.lstCVarRoutings[i].TransportIconName;
                    objCVarRoutings.TransportIconStyle = objCRoutingsToCopy.lstCVarRoutings[i].TransportIconStyle;
                    objCVarRoutings.POLCountryID = objCRoutingsToCopy.lstCVarRoutings[i].POLCountryID;
                    objCVarRoutings.POL = objCRoutingsToCopy.lstCVarRoutings[i].POL;
                    objCVarRoutings.PODCountryID = objCRoutingsToCopy.lstCVarRoutings[i].PODCountryID;
                    objCVarRoutings.POD = objCRoutingsToCopy.lstCVarRoutings[i].POD;
                    objCVarRoutings.ETAPOLDate = objCRoutingsToCopy.lstCVarRoutings[i].ETAPOLDate;
                    objCVarRoutings.ATAPOLDate = objCRoutingsToCopy.lstCVarRoutings[i].ATAPOLDate;
                    objCVarRoutings.ExpectedDeparture = objCRoutingsToCopy.lstCVarRoutings[i].ExpectedDeparture;
                    objCVarRoutings.ActualDeparture = objCRoutingsToCopy.lstCVarRoutings[i].ActualDeparture;
                    objCVarRoutings.ExpectedArrival = objCRoutingsToCopy.lstCVarRoutings[i].ExpectedArrival;
                    objCVarRoutings.ActualArrival = objCRoutingsToCopy.lstCVarRoutings[i].ActualArrival;
                    objCVarRoutings.ShippingLineID = objCRoutingsToCopy.lstCVarRoutings[i].ShippingLineID;
                    objCVarRoutings.AirlineID = objCRoutingsToCopy.lstCVarRoutings[i].AirlineID;
                    objCVarRoutings.TruckerID = objCRoutingsToCopy.lstCVarRoutings[i].TruckerID;
                    objCVarRoutings.VesselID = objCRoutingsToCopy.lstCVarRoutings[i].VesselID;
                    objCVarRoutings.VoyageOrTruckNumber = objCRoutingsToCopy.lstCVarRoutings[i].VoyageOrTruckNumber;

                    objCVarRoutings.GensetSupplierID = 0; //objCRoutingsToCopy.lstCVarRoutings[i].GensetSupplierID;
                    objCVarRoutings.CCAID = 0; //objCRoutingsToCopy.lstCVarRoutings[i].CCAID;
                    objCVarRoutings.Quantity = "0"; //objCRoutingsToCopy.lstCVarRoutings[i].Quantity;
                    objCVarRoutings.ContactPerson = "0"; //objCRoutingsToCopy.lstCVarRoutings[i].ContactPerson;
                    objCVarRoutings.PickupAddress = "0"; //objCRoutingsToCopy.lstCVarRoutings[i].PickupAddress;
                    objCVarRoutings.DeliveryAddress = "0"; //objCRoutingsToCopy.lstCVarRoutings[i].DeliveryAddress;
                    objCVarRoutings.GateInPortID = 0; //objCRoutingsToCopy.lstCVarRoutings[i].GateInPortID;
                    objCVarRoutings.GateOutPortID = 0; //objCRoutingsToCopy.lstCVarRoutings[i].GateOutPortID;
                    objCVarRoutings.GateInDate = DateTime.Parse("01/01/1900"); //objCRoutingsToCopy.lstCVarRoutings[i].GateInDate;

                    #region TransportOrder
                    objCVarRoutings.CustomerID = 0;
                    objCVarRoutings.SubContractedCustomerID = 0;
                    objCVarRoutings.Cost = 0;
                    objCVarRoutings.Sale = 0;
                    objCVarRoutings.IsFleet = false;
                    objCVarRoutings.CommodityID = 0;
                    objCVarRoutings.LoadingDate = DateTime.Parse("01/01/1900");
                    objCVarRoutings.LoadingReference = "0";
                    objCVarRoutings.UnloadingDate = DateTime.Parse("01/01/1900");
                    objCVarRoutings.UnloadingReference = "0";
                    objCVarRoutings.UnloadingTime = "0";
                    #endregion TransportOrder

                    objCVarRoutings.GateOutDate = DateTime.Parse("01/01/1900"); //objCRoutingsToCopy.lstCVarRoutings[i].GateOutDate;
                    objCVarRoutings.StuffingDate = DateTime.Parse("01/01/1900"); //objCRoutingsToCopy.lstCVarRoutings[i].StuffingDate;
                    objCVarRoutings.DeliveryDate = DateTime.Parse("01/01/1900"); //objCRoutingsToCopy.lstCVarRoutings[i].DeliveryDate;
                    objCVarRoutings.BookingNumber = "0"; // objCRoutingsToCopy.lstCVarRoutings[i].BookingNumber;
                    objCVarRoutings.Delays = "0";
                    objCVarRoutings.DriverName = "0";
                    objCVarRoutings.DriverPhones = "0";
                    objCVarRoutings.PowerFromGateInTillActualSailing = "0"; //objCRoutingsToCopy.lstCVarRoutings[i].PowerFromGateInTillActualSailing;
                    objCVarRoutings.ContactPersonPhones = "0"; //objCRoutingsToCopy.lstCVarRoutings[i].ContactPersonPhones;
                    objCVarRoutings.LoadingTime = "0"; //objCRoutingsToCopy.lstCVarRoutings[i].LoadingTime;

                    #region CustomsClearance
                    objCVarRoutings.CCAFreight = 0;
                    objCVarRoutings.CCAFOB = 0;
                    objCVarRoutings.CCACFValue = 0;
                    objCVarRoutings.CCAInvoiceNumber = "0";

                    objCVarRoutings.CCAInsurance = "0";
                    objCVarRoutings.CCADischargeValue = "0";
                    objCVarRoutings.CCAAcceptedValue = "0";
                    objCVarRoutings.CCAImportValue = "0";
                    objCVarRoutings.CCADocumentReceiveDate = DateTime.Parse("01/01/1900");
                    objCVarRoutings.CCAExchangeRate = "0";
                    objCVarRoutings.CCAVATCertificateNumber = "0";
                    objCVarRoutings.CCAVATCertificateValue = "0";
                    objCVarRoutings.CCACommercialProfitCertificateNumber = "0";
                    objCVarRoutings.CCAOthers = "0";
                    objCVarRoutings.CCASpendDate = DateTime.Parse("01/01/1900");
                    objCVarRoutings.OffloadingDate = DateTime.Parse("01/01/1900");

                    objCVarRoutings.CertificateNumber = "0";
                    objCVarRoutings.CertificateValue = "0";
                    objCVarRoutings.CertificateDate = DateTime.Parse("01/01/1900");
                    objCVarRoutings.QasimaNumber = "0";
                    objCVarRoutings.QasimaDate = DateTime.Parse("01/01/1900");
                    objCVarRoutings.Match = false;
                    objCVarRoutings.SalesDateReceived = DateTime.Parse("01/01/1900");
                    objCVarRoutings.CommerceDateReceived = DateTime.Parse("01/01/1900");
                    objCVarRoutings.InspectionDateReceived = DateTime.Parse("01/01/1900");
                    objCVarRoutings.FinishDateReceived = DateTime.Parse("01/01/1900");
                    objCVarRoutings.SalesDateDelivered = DateTime.Parse("01/01/1900");
                    objCVarRoutings.CommerceDateDelivered = DateTime.Parse("01/01/1900");
                    objCVarRoutings.InspectionDateDelivered = DateTime.Parse("01/01/1900");
                    objCVarRoutings.FinishDateDelivered = DateTime.Parse("01/01/1900");

                    objCVarRoutings.CCAllowTemporaryDelivered = DateTime.Parse("01/01/1900");
                    objCVarRoutings.CCAllowTemporaryReceived = DateTime.Parse("01/01/1900");
                    objCVarRoutings.CCDropBackDelivered = DateTime.Parse("01/01/1900");
                    objCVarRoutings.CCDropBackReceived = DateTime.Parse("01/01/1900");
                    objCVarRoutings.CC_ClearanceTypeID = 0;
                    objCVarRoutings.CC_CustomItemsID = 0;
                    objCVarRoutings.CCReleaseNo = "0";
                    #endregion CustomsClearance

                    objCVarRoutings.BillNumber = "0";
                    objCVarRoutings.TruckingOrderCode = "0";

                    objCVarRoutings.RoadNumber = "0";
                    objCVarRoutings.DeliveryOrderNumber = "0";
                    objCVarRoutings.WareHouse = "0";
                    objCVarRoutings.WareHouseLocation = "0";

                    objCVarRoutings.Notes = "0"; // objCRoutingsToCopy.lstCVarRoutings[i].Notes;

                    objCVarRoutings.CreatorUserID = objCVarRoutings.ModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarRoutings.CreationDate = objCVarRoutings.ModificationDate = DateTime.Now;
                    objCRoutings.lstCVarRoutings.Add(objCVarRoutings);
                }
                objCRoutings.SaveMethod(objCRoutings.lstCVarRoutings);
                #endregion Copy OperationRoutings

                #region Copy Payables if requested
                if (pIncludePayables)
                {
                    CPayables objCPayablesToCopy = new CPayables();
                    objCPayablesToCopy.GetListPaging(999999, 1, " WHERE OperationID = " + pOperationToCopyID.ToString()
                        + " AND IsDeleted=0 AND OperationContainersAndPackagesID IS NULL"
                        , "ID", out _TempRowCount); //Don't get containerTracking Payables&Receivables

                    CPayables objCPayables = new CPayables();
                    for (int i = 0; i < objCPayablesToCopy.lstCVarPayables.Count; i++)
                    {
                        pWhereClauseCurrencyDetails = "WHERE ID=" + objCPayablesToCopy.lstCVarPayables[i].CurrencyID
                            + " AND GETDATE() >= DateAdd(hh, 0, DateDiff(dd, 0, FromDate)) "
                            + " AND GETDATE() <= DateAdd(minute,59,DateAdd(hh, 23, DateDiff(dd, 0, ToDate))) "
                            + " ORDER BY CODE";
                        objCvwCurrencyDetails.GetList(pWhereClauseCurrencyDetails);
                        if (objCvwCurrencyDetails.lstCVarvwCurrencyDetails.Count > 0)
                        {
                            CVarPayables objCVarPayables = new CVarPayables();
                            objCVarPayables.OperationID = objCOperations.lstCVarOperations[0].ID;
                            objCVarPayables.ChargeTypeID = objCPayablesToCopy.lstCVarPayables[i].ChargeTypeID;
                            objCVarPayables.POrC = objCPayablesToCopy.lstCVarPayables[i].POrC;
                            objCVarPayables.SupplierOperationPartnerID = objCPayablesToCopy.lstCVarPayables[i].SupplierOperationPartnerID;
                            objCVarPayables.ContainerTypeID = objCPayablesToCopy.lstCVarPayables[i].ContainerTypeID;
                            objCVarPayables.MeasurementID = objCPayablesToCopy.lstCVarPayables[i].MeasurementID;
                            objCVarPayables.Quantity = objCPayablesToCopy.lstCVarPayables[i].Quantity;
                            objCVarPayables.CostPrice = objCPayablesToCopy.lstCVarPayables[i].CostPrice;
                            objCVarPayables.CostAmount = objCPayablesToCopy.lstCVarPayables[i].CostAmount;
                            objCVarPayables.AmountWithoutVAT = objCPayablesToCopy.lstCVarPayables[i].AmountWithoutVAT;
                            objCVarPayables.InitialSalePrice = objCPayablesToCopy.lstCVarPayables[i].InitialSalePrice;
                            objCVarPayables.SupplierInvoiceNo = objCPayablesToCopy.lstCVarPayables[i].SupplierInvoiceNo;
                            objCVarPayables.SupplierReceiptNo = objCPayablesToCopy.lstCVarPayables[i].SupplierReceiptNo;
                            objCVarPayables.EntryDate = objCPayablesToCopy.lstCVarPayables[i].EntryDate;
                            objCVarPayables.BillID = 0;

                            objCVarPayables.IssueDate = objCPayablesToCopy.lstCVarPayables[i].IssueDate;
                            objCVarPayables.OperationContainersAndPackagesID = 0;

                            objCVarPayables.ExchangeRate = objCvwCurrencyDetails.lstCVarvwCurrencyDetails[0].ExchangeRate; //objCPayablesToCopy.lstCVarPayables[i].ExchangeRate;
                            objCVarPayables.CurrencyID = objCPayablesToCopy.lstCVarPayables[i].CurrencyID;
                            objCVarPayables.Notes = objCPayablesToCopy.lstCVarPayables[i].Notes;
                            objCVarPayables.CreatorUserID = objCVarPayables.ModificatorUserID = WebSecurity.CurrentUserId;
                            objCVarPayables.CreationDate = objCVarPayables.ModificationDate = DateTime.Now;
                            objCPayables.lstCVarPayables.Add(objCVarPayables);
                        } //if (objCvwCurrencyDetails.lstCVarvwCurrencyDetails.Count > 0)
                    } //for (int i = 0; i < objCPayablesToCopy.lstCVarPayables.Count; i++)
                    objCPayables.SaveMethod(objCPayables.lstCVarPayables);
                } //if (pIncludePayables)
                #endregion Copy Payables if requested

                #region Copy Receivables if requested
                if (pIncludeReceivables)
                {
                    CReceivables objCReceivablesToCopy = new CReceivables();
                    objCReceivablesToCopy.GetListPaging(999999, 1, " WHERE OperationID = " + pOperationToCopyID.ToString()
                        + " AND IsDeleted=0 AND OperationContainersAndPackagesID IS NULL ", "ID", out _TempRowCount);

                    CReceivables objCReceivables = new CReceivables();
                    for (int i = 0; i < objCReceivablesToCopy.lstCVarReceivables.Count; i++)
                    {
                        pWhereClauseCurrencyDetails = "WHERE ID=" + objCReceivablesToCopy.lstCVarReceivables[i].CurrencyID
                            + " AND GETDATE() >= DateAdd(hh, 0, DateDiff(dd, 0, FromDate)) "
                            + " AND GETDATE() <= DateAdd(minute,59,DateAdd(hh, 23, DateDiff(dd, 0, ToDate))) "
                            + " ORDER BY CODE";
                        objCvwCurrencyDetails.GetList(pWhereClauseCurrencyDetails);
                        if (objCvwCurrencyDetails.lstCVarvwCurrencyDetails.Count > 0)
                        {
                            CVarReceivables objCVarReceivables = new CVarReceivables();
                            objCVarReceivables.OperationID = objCOperations.lstCVarOperations[0].ID;
                            objCVarReceivables.ChargeTypeID = objCReceivablesToCopy.lstCVarReceivables[i].ChargeTypeID;
                            objCVarReceivables.POrC = objCReceivablesToCopy.lstCVarReceivables[i].POrC;
                            objCVarReceivables.SupplierID = objCReceivablesToCopy.lstCVarReceivables[i].SupplierID;
                            objCVarReceivables.MeasurementID = objCReceivablesToCopy.lstCVarReceivables[i].MeasurementID;

                            objCVarReceivables.IssueDate = DateTime.Now;
                            objCVarReceivables.OperationContainersAndPackagesID = 0;

                            objCVarReceivables.ContainerTypeID = objCReceivablesToCopy.lstCVarReceivables[i].ContainerTypeID;
                            objCVarReceivables.PackageTypeID = objCReceivablesToCopy.lstCVarReceivables[i].PackageTypeID;
                            objCVarReceivables.Quantity = objCReceivablesToCopy.lstCVarReceivables[i].Quantity == 0 ? 1 : objCReceivablesToCopy.lstCVarReceivables[i].Quantity;
                            objCVarReceivables.CostPrice = objCReceivablesToCopy.lstCVarReceivables[i].CostPrice;
                            objCVarReceivables.CostAmount = objCReceivablesToCopy.lstCVarReceivables[i].CostAmount;
                            objCVarReceivables.SalePrice = objCReceivablesToCopy.lstCVarReceivables[i].SalePrice;
                            objCVarReceivables.AmountWithoutVAT = Math.Round((objCVarReceivables.Quantity * objCVarReceivables.SalePrice), 2);
                            objCVarReceivables.SaleAmount = objCReceivablesToCopy.lstCVarReceivables[i].SaleAmount;
                            objCVarReceivables.TaxeTypeID = objCReceivablesToCopy.lstCVarReceivables[i].TaxeTypeID;
                            objCVarReceivables.ExchangeRate = objCvwCurrencyDetails.lstCVarvwCurrencyDetails[0].ExchangeRate; //objCReceivablesToCopy.lstCVarReceivables[i].ExchangeRate;
                            objCVarReceivables.CurrencyID = objCReceivablesToCopy.lstCVarReceivables[i].CurrencyID;
                            objCVarReceivables.Notes = objCReceivablesToCopy.lstCVarReceivables[i].Notes;
                            objCVarReceivables.ViewOrder = objCReceivablesToCopy.lstCVarReceivables[i].ViewOrder;
                            objCVarReceivables.IsDeleted = false;

                            objCVarReceivables.PreviousCutOffDate = DateTime.Parse("01/01/1900");
                            objCVarReceivables.CutOffDate = DateTime.Parse("01/01/1900");
                            objCVarReceivables.ReceiptDate = DateTime.Parse("01/01/1900");
                            objCVarReceivables.ReceiptNo = "";

                            objCVarReceivables.CreatorUserID = objCVarReceivables.ModificatorUserID = WebSecurity.CurrentUserId;
                            objCVarReceivables.CreationDate = objCVarReceivables.ModificationDate = DateTime.Now;
                            objCReceivables.lstCVarReceivables.Add(objCVarReceivables);
                        } //if (objCvwCurrencyDetails.lstCVarvwCurrencyDetails.Count > 0)
                    } //for (int i = 0; i < objCReceivablesToCopy.lstCVarReceivables.Count; i++)
                    objCReceivables.SaveMethod(objCReceivables.lstCVarReceivables);
                } //if (pIncludeReceivables)
                #endregion Copy Receivables if requested

                Operations_EmailNotification(objCOperations.lstCVarOperations[0].ID);

                #region CreateCostCenter
                CSystemOptions objCSystemOptions = new CSystemOptions();
                objCSystemOptions.GetList("Where OptionID=94");
                if (objCSystemOptions.lstCVarSystemOptions.Count > 0 && objCSystemOptions.lstCVarSystemOptions[0].OptionValue
                      && objCOperations.lstCVarOperations[0].BLType != 2)
                {
                    string CostCenterNumberParent = "";
                    Int32 pID = 0;
                    CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
                    CA_CostCenters objCA_CostCentersParent = new CA_CostCenters();
                    objCA_CostCentersParent.GetList("where ( CostCenterName=N'عمليات' or CostCenterName='Operations' ) and Parent_ID is null ");
                    if (objCA_CostCentersParent.lstCVarA_CostCenters.Count > 0)
                    {
                        pID = objCA_CostCentersParent.lstCVarA_CostCenters[0].ID;
                        CostCenterNumberParent = objCA_CostCentersParent.lstCVarA_CostCenters[0].RealCostCenterCode;
                    }

                    else
                    {

                        string pNewCodePartner = objCCustomizedDBCall.CallStringFunction("SELECT [dbo].A_CostCenters_GetCode(" + (pID == 0 ? "null" : pID.ToString()) + ") AS Code");


                        CVarA_CostCenters objCVarA_CostCenters = new CVarA_CostCenters();
                        objCVarA_CostCenters.CostCenterNumber = pNewCodePartner.PadRight(12, '0');
                        objCVarA_CostCenters.CostCenterName = "Operations";
                        objCVarA_CostCenters.Parent_ID = 0;
                        objCVarA_CostCenters.IsMain = true;
                        objCVarA_CostCenters.CCLevel = 1;
                        objCVarA_CostCenters.RealCostCenterCode = pNewCodePartner;
                        objCVarA_CostCenters.User_ID = WebSecurity.CurrentUserId;
                        objCVarA_CostCenters.Type_ID = 0;
                        objCVarA_CostCenters.IsClosed = false;
                        objCVarA_CostCenters.SubAccountGroupID = 0;
                        objCVarA_CostCenters.EmployeesCount = 0;
                        objCA_CostCentersParent.lstCVarA_CostCenters.Add(objCVarA_CostCenters);
                        checkException = objCA_CostCentersParent.SaveMethod(objCA_CostCentersParent.lstCVarA_CostCenters);

                        pID = objCA_CostCentersParent.lstCVarA_CostCenters[0].ID;
                        CostCenterNumberParent = objCA_CostCentersParent.lstCVarA_CostCenters[0].RealCostCenterCode;
                    }

                    CA_CostCenters objCA_CostCenters = new CA_CostCenters();
                    checkException = objCA_CostCenters.GetListPaging(1, 1, "WHERE ID = " + pID.ToString(), "ID", out _RowCount);
                    string pNewCode = objCCustomizedDBCall.CallStringFunction("SELECT [dbo].A_CostCenters_GetCode(" + (pID == 0 ? "null" : pID.ToString()) + ") AS Code");

                    CvwOperations objCvwOperations = new CvwOperations();
                    objCvwOperations.GetList("WHERE ID=" + objCOperations.lstCVarOperations[0].ID);
                    string pNewCodeNew = CostCenterNumberParent + pNewCode;
                    CVarA_CostCenters objCVarA_CostCentersChild = new CVarA_CostCenters();
                    objCVarA_CostCentersChild.CostCenterNumber = pNewCodeNew.PadRight(12, '0'); ;
                    objCVarA_CostCentersChild.CostCenterName = objCvwOperations.lstCVarvwOperations[0].Code + " - " + objCvwOperations.lstCVarvwOperations[0].ClientName;
                    objCVarA_CostCentersChild.Parent_ID = pID;
                    objCVarA_CostCentersChild.IsMain = false;
                    objCVarA_CostCentersChild.CCLevel = 2;
                    objCVarA_CostCentersChild.RealCostCenterCode = pNewCodeNew;
                    objCVarA_CostCentersChild.User_ID = WebSecurity.CurrentUserId;
                    objCVarA_CostCentersChild.Type_ID = 0;
                    objCVarA_CostCentersChild.IsClosed = false;
                    objCVarA_CostCentersChild.SubAccountGroupID = 0;
                    objCVarA_CostCentersChild.EmployeesCount = 0;
                    objCA_CostCentersParent.lstCVarA_CostCenters.Add(objCVarA_CostCentersChild);
                    checkException = objCA_CostCentersParent.SaveMethod(objCA_CostCentersParent.lstCVarA_CostCenters);

                    //Link Oeation With CostCenter Add by ahmed maher
                    int _RowCount2 = 0;
                    CvwDefaults objCvwDefaults = new CvwDefaults();
                    objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount2); //i am sure i ve just one row isa
                    string CompanyName2 = objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName;
                    if (CompanyName2 == "BED")
                    {
                        CVarA_LinkOperationWithCostCenter objCVarA_LinkOperationWithCostCenter = new CVarA_LinkOperationWithCostCenter();

                        objCVarA_LinkOperationWithCostCenter.OperationID = objCOperations.lstCVarOperations[0].ID;
                        objCVarA_LinkOperationWithCostCenter.CostCenterID = objCVarA_CostCentersChild.ID;

                        CA_LinkOperationWithCostCenter objA_LinkOperationWithCostCenter = new CA_LinkOperationWithCostCenter();
                        objA_LinkOperationWithCostCenter.lstCVarA_LinkOperationWithCostCenter.Add(objCVarA_LinkOperationWithCostCenter);
                        checkException = objA_LinkOperationWithCostCenter.SaveMethod(objA_LinkOperationWithCostCenter.lstCVarA_LinkOperationWithCostCenter);
                    }

                }


                #endregion
            }
            return new object[] { _result, _result ? objCOperations.lstCVarOperations[0].ID : 0 };
        }

        [HttpGet, HttpPost]
        public object[] Delete(String pDeletedOperationsIDs)
        {
            bool _result = false;
            COperations objCOperations = new COperations();
            foreach (var currentID in pDeletedOperationsIDs.Split(','))
            {
                objCOperations.lstDeletedCPKOperations.Add(new CPKOperations() { ID = Int32.Parse(currentID.Trim()) });
            }

            Exception checkException = objCOperations.DeleteItem(objCOperations.lstDeletedCPKOperations);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("DELETE")) // some or all of the rows were not deleted due to dependencies
                    _result = false;
            }
            else //deleted successfully
                _result = true;
            return new object[] {
                _result ? "" : checkException.Message
            };
        }

        //Set OperationStage
        [HttpGet, HttpPost]
        public object[] SetOperationStage(Int64 pID, int pOperationStageID, string pCloseDate/*used just in case of ReOpening an Operation, its in yyyyMMdd Format*/)
        {
            string updateClause = "";
            string pReturnedMessage = "";
            int ClosedQuoteAndOperStageID = 120;
            int OpenQuoteAndOperStageID = 60;
            var CancelledQuoteAndOperStageID = 110;
            CPayables objCPayables = new CPayables();
            CReceivables objCReceivables = new CReceivables();
            int _RowCount = 0;
            if (pOperationStageID == CancelledQuoteAndOperStageID)
            {
                objCPayables.GetListPaging(1, 1, "WHERE IsDeleted=0 AND OperationID=" + pID, "ID", out _RowCount);
                objCReceivables.GetListPaging(1, 1, "WHERE IsDeleted=0 AND OperationID=" + pID, "ID", out _RowCount);
            }
            if (objCReceivables.lstCVarReceivables.Count > 0 || objCPayables.lstCVarPayables.Count > 0)
                pReturnedMessage = "Please, check there is not payables nor receivables.";
            else
            {
                updateClause = " OperationStageID = " + pOperationStageID.ToString();
                updateClause += (pOperationStageID == ClosedQuoteAndOperStageID ? " , CloseDate = GETDATE() " : "");//pOperationStageID == 110 i.e. Closed
                updateClause += (pOperationStageID == OpenQuoteAndOperStageID ? " , CloseDate = '" + pCloseDate + "'" : "");//pOperationStageID == 110 i.e. Closed
                updateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                updateClause += " , ModificationDate = GETDATE() ";
                updateClause += "            WHERE ID = " + pID.ToString() + " OR MasterOperationID=" + pID.ToString();

                COperations objCOperations = new COperations();
                Exception checkException = objCOperations.UpdateList(updateClause);
                if (checkException != null)
                    pReturnedMessage = checkException.Message;
                else
                {
                    CDefaults objCDefaults = new CDefaults();
                    objCDefaults.GetListPaging(1, 1, "", "ID", out _RowCount);
                    if (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "UTS" && pOperationStageID == ClosedQuoteAndOperStageID)
                    {
                        CCallCustomizedSP objCCallCustomizedSP = new CCallCustomizedSP();
                        checkException = objCCallCustomizedSP.CallCustomizedSP("ERP_ForwWeb_PostingCloseOperation", pID, WebSecurity.CurrentUserId, true, 0);
                    }
                }
            }
            return new object[] {
                pReturnedMessage
            };
        }
        [HttpGet, HttpPost]
        public object[] SetOperationStage_ByDates(string pOperationIDs_ToSet, int pOperationStageID, string pCloseDate/*its in yyyyMMdd Format*/)
        {
            string updateClause = "";
            string pReturnedMessage = "";
            int ClosedQuoteAndOperStageID = 120;
            int OpenQuoteAndOperStageID = 60;
            var CancelledQuoteAndOperStageID = 110;
            CPayables objCPayables = new CPayables();
            CReceivables objCReceivables = new CReceivables();
            int _RowCount = 0;
            //if (pOperationStageID == CancelledQuoteAndOperStageID)
            //{
            //    objCPayables.GetListPaging(1, 1, "WHERE IsDeleted=0 AND OperationID=" + pID, "ID", out _RowCount);
            //    objCReceivables.GetListPaging(1, 1, "WHERE IsDeleted=0 AND OperationID=" + pID, "ID", out _RowCount);
            //}
            //if (objCReceivables.lstCVarReceivables.Count > 0 || objCPayables.lstCVarPayables.Count > 0)
            //    pReturnedMessage = "Please, check there is not payables nor receivables.";
            //else
            {
                updateClause = " OperationStageID = " + pOperationStageID.ToString();
                updateClause += (pOperationStageID == ClosedQuoteAndOperStageID ? " , CloseDate = GETDATE() " : "");//pOperationStageID == 110 i.e. Closed
                updateClause += (pOperationStageID == OpenQuoteAndOperStageID ? " , CloseDate = '" + pCloseDate + "'" : "");//pOperationStageID == 110 i.e. Closed
                updateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                updateClause += " , ModificationDate = GETDATE() ";
                updateClause += "            WHERE ID IN (" + pOperationIDs_ToSet.ToString() + ") OR MasterOperationID IN (" + pOperationIDs_ToSet.ToString() + ")";

                COperations objCOperations = new COperations();
                Exception checkException = objCOperations.UpdateList(updateClause);
                if (checkException != null)
                    pReturnedMessage = checkException.Message;
                else
                {
                    CDefaults objCDefaults = new CDefaults();
                    objCDefaults.GetListPaging(1, 1, "", "ID", out _RowCount);
                }
            }
            return new object[] {
                pReturnedMessage
            };
        }

        //Connect Or Disconnect House to Master
        [HttpGet, HttpPost]
        public object[] ConnectOrDisconnect(Int64 pMasterOperationID, bool pIsHouseConnected, Int64 pMasterOperationIDFieldInHouse, Int64 pHouseOperationID, Int64 pHouseParentID)
        {
            bool _result = false;
            string pReturnedMessage = "";
            int _RowCount = 0;
            int constConsolidationShipmentType = 5;
            int constLCLShipmentType = 2;

            Exception checkException = null;
            COperations objCOperations = new COperations();
            CContainerPackages objCContainerPackages = new CContainerPackages();//to delete house containers and packages from master
            COperationContainersAndPackages objCOperationContainersAndPackages = new COperationContainersAndPackages();//to delete house containers and packages from master
            CPayables objCPayables = new CPayables();
            CAccNote objCAccNote = new CAccNote();
            CInvoices objCInvoices = new CInvoices();

            checkException = objCOperations.GetListPaging(1, 1, "WHERE ID=" + pMasterOperationID, "ID", out _RowCount);
            COperations checkIsHouseParent = new COperations();
            checkIsHouseParent.GetListPaging(1, 1, "WHERE HouseParentID=" + pHouseOperationID, "ID", out _RowCount);


            objCPayables.GetListPaging(999999, 1, "WHERE IsDeleted=0 AND OperationID=" + pHouseOperationID + " OR BillID=" + pHouseOperationID, "ID", out _RowCount);
            objCInvoices.GetListPaging(999999, 1, "WHERE IsDeleted=0 AND OperationID=" + pHouseOperationID, "ID", out _RowCount);
            objCAccNote.GetListPaging(999999, 1, "WHERE IsDeleted=0 AND OperationID=" + pHouseOperationID, "ID", out _RowCount);
            if (objCInvoices.lstCVarInvoices.Count > 0 || objCAccNote.lstCVarAccNote.Count > 0 || objCPayables.lstCVarPayables.Count > 0)
                pReturnedMessage = "Please, check that no invoices, payables or notes are added.";
            else if (checkIsHouseParent.lstCVarOperations.Count > 0)
            {
                pReturnedMessage = " House has child HBL. Remove  it first";
            }
            else
            {
                //if (CheckIsHouseSuitableForMaster(pMasterOperationID, pHouseOperationID)) // to make sure that properties of both are not changed in another session
                {
                    #region Delete OperationContainersAndPackages / ContainerPackages from Master
                    string pWhereClause = " WHERE HouseOperationID = " + pHouseOperationID.ToString();
                    checkException = objCContainerPackages.DeleteList(pWhereClause);
                    checkException = objCOperationContainersAndPackages.DeleteList(pWhereClause);
                    #endregion
                    #region Update the House Operation
                    string updateClause = "";
                    updateClause += " MasterOperationID = " + (pIsHouseConnected ? pMasterOperationIDFieldInHouse.ToString() : " NULL ");
                    if (!pIsHouseConnected) //Disconnect
                        updateClause += " , HouseParentID = NULL " + " \n";
                    // updateClause += " , HouseNumber = NULL " + " \n";
                    else //Connect
                    {
                        updateClause += " , POL=" + objCOperations.lstCVarOperations[0].POL + " \n";
                        updateClause += " , POD=" + objCOperations.lstCVarOperations[0].POD + " \n";
                        updateClause += " , POLCountryID=" + objCOperations.lstCVarOperations[0].POLCountryID + " \n";
                        updateClause += " , PODCountryID=" + objCOperations.lstCVarOperations[0].PODCountryID + " \n";

                        updateClause += " , BLTypeIconName=N'" + objCOperations.lstCVarOperations[0].BLTypeIconName + "' \n";
                        updateClause += " , BLTypeIconStyle=N'" + objCOperations.lstCVarOperations[0].BLTypeIconStyle + "' \n";
                        updateClause += " , DirectionType=N'" + objCOperations.lstCVarOperations[0].DirectionType + "' \n";
                        updateClause += " , DirectionIconName=N'" + objCOperations.lstCVarOperations[0].DirectionIconName + "' \n";
                        updateClause += " , DirectionIconStyle=N'" + objCOperations.lstCVarOperations[0].DirectionIconStyle + "' \n";
                        updateClause += " , TransportType=N'" + objCOperations.lstCVarOperations[0].TransportType + "' \n";
                        updateClause += " , TransportIconName=N'" + objCOperations.lstCVarOperations[0].TransportIconName + "' \n";
                        updateClause += " , TransportIconStyle=N'" + objCOperations.lstCVarOperations[0].TransportIconStyle + "' \n";
                        updateClause += " , ShipmentType=N'" + (objCOperations.lstCVarOperations[0].ShipmentType == constConsolidationShipmentType ? constLCLShipmentType : objCOperations.lstCVarOperations[0].ShipmentType) + "' \n";
                        updateClause += " , HouseParentID = " + (pHouseParentID == 0 ? " Null" : pHouseParentID.ToString());

                    }
                    updateClause += " , PlacedOnOperationContainersAndPackagesID = NULL "; //wether connected or disconnected its set to null as i delete all its Containers/Packages from Master
                    updateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                    updateClause += " , ModificationDate = GETDATE() ";
                    updateClause += "            WHERE ID = " + pHouseOperationID.ToString();

                    checkException = objCOperations.UpdateList(updateClause);
                    if (checkException == null)
                        _result = true;
                    else
                        _result = false;
                    #endregion
                    #region update the Master Operation to set NumberOfHousesConnected Field in DB
                    _result = false;

                    updateClause = " NumberOfHousesConnected = (select COUNT(ID) from Operations where MasterOperationID=" + pMasterOperationID.ToString() + ")"; //+(pIsHouseConnected ? " ISNULL(NumberOfHousesConnected, 0) + 1 " : " ISNULL(NumberOfHousesConnected, 0) - 1 ");
                    updateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                    updateClause += " , ModificationDate = GETDATE() ";
                    updateClause += "            WHERE ID = " + pMasterOperationID.ToString();

                    checkException = objCOperations.UpdateList(updateClause);
                    if (checkException == null)
                        _result = true;
                    else
                        _result = false;
                    #endregion
                }
                //else //not suitable (probably changed in another session)
                //    strMsgReturned = "This house operation is not suitable. The operation might be changed in another session. Please, refresh to reload available shipments.";
            }
            return new Object[] { _result, pReturnedMessage };
        }
        //Connect Or Disconnect Houses to Master
        [HttpGet, HttpPost]
        public object[] ConnectOrDisconnectMultiple(Int64 pMasterOperationID, bool pIsHouseConnected, Int64 pMasterOperationIDFieldInHouse, string pHouseOperationsIDs, Int64 pHouseParentID)
        {
            string[] OperationIDsArray = pHouseOperationsIDs.Split(',');
            int NumberOfOperations = OperationIDsArray.Length;

            object[] arr = new object[NumberOfOperations];

            for (int i = 0; i < NumberOfOperations; i++)
            {
                arr[i] = ConnectOrDisconnect(pMasterOperationID, pIsHouseConnected, pMasterOperationIDFieldInHouse, Int64.Parse(OperationIDsArray[i]), pHouseParentID);
            }

            return arr;
        }

        [HttpGet, HttpPost]
        public object[] Shipment_Update(long pShipmentID, int pBranchID, int pSalesmanID, string pOpenDate,
            string pHouseNumber, int pPickupCityID
            , int pDeliveryCityID, int pShipperID, int pConsigneeID, int pAgentID, int pNotifyID, int pIncotermID,
            int pMoveTypeID, string pNotes
            , int pCommodityID, string pCustomerReference, string pSupplierReference, string pPONumber
            , int pPOrC, string pDeliveryOrderNumber, string pDeliveryDate, bool pIsDelivered, bool pIsReceivedFromShipper, int pConsigneeID2,
            string pReleaseDate
            , string pBLDate, string pShippedOnBoardDate, string pFreightPayableAt, string pACIDNumber,
            int pNumberOfOriginalBills
            , string pCertificateNumber, string pCountryOfOrigin, string pInvoiceValue, int pCurrencyID,
            string pBookingNumber, string pACIDNumberDetails, decimal pIMOClass, int pUNNumber, int pVesselID, int pOperationManID = -1
            , int pPOLCountryID = 0, int pPOL = 0, int pPODCountryID = 0, int pPOD = 0)
        {
            Exception checkException = null;
            COperations objCOperations = new COperations();
            COperationPartners objCOperationPartners = new COperationPartners();
            CRoutings objCRoutings = new CRoutings();
            var MainCarraigeRoutingTypeID = 30;

            var constCustomerPartnerTypeID = 1;
            var constAgentPartnerTypeID = 2;

            var constShipperOperationPartnerTypeID = 1;
            var constConsigneeOperationPartnerTypeID = 2;
            var constNotify1OperationPartnerTypeID = 4;
            var constAgentOperationPartnerTypeID = 6;

            string pUpdateClause = "";
            string _MessageReturned = "";
            int _RowCount = 0;
            CDefaults objCDefaults = new CDefaults();
            objCDefaults.GetListPaging(1, 1, "", "ID", out _RowCount);
            #region Cheque HouseNumber uniqueness
            COperations objCOperations_CheckUniqueness = new COperations();
            if (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "EGL" || objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "SUN")
                objCOperations_CheckUniqueness.GetListPaging(999999, 1, "WHERE HouseNumber IS NOT NULL AND HouseNumber<>N'' AND HouseNumber<>N'N/A' AND HouseNumber=N'" + pHouseNumber + "' AND ID<>" + pShipmentID, "ID", out _RowCount);
            if (objCOperations_CheckUniqueness.lstCVarOperations.Count > 0)
                _MessageReturned = "This house number already exists.";
            #endregion Cheque HouseNumber uniqueness
            if (_MessageReturned == "")
            {
                pUpdateClause += " BranchID=" + (pBranchID == 0 ? "NULL" : pBranchID.ToString()) + " \n";
                pUpdateClause += ", SalesmanID=" + pSalesmanID + " \n";
                pUpdateClause += ", OpenDate='" + Forwarding.MvcApp.Helpers.DateFunctionsController.GetYYYYMMDDFormat(pOpenDate, 2) + "' \n";
                pUpdateClause += ", HouseNumber=" + (pHouseNumber == "0" ? "NULL" : ("'" + pHouseNumber + "'")) + " \n";
                pUpdateClause += ", PickupCityID=" + (pPickupCityID == 0 ? "NULL" : pPickupCityID.ToString()) + " \n";
                pUpdateClause += ", DeliveryCityID=" + (pDeliveryCityID == 0 ? "NULL" : pDeliveryCityID.ToString()) + " \n";
                pUpdateClause += ", ShipperID=" + (pShipperID == 0 ? "NULL" : pShipperID.ToString()) + " \n";
                pUpdateClause += ", ConsigneeID=" + (pConsigneeID == 0 ? "NULL" : pConsigneeID.ToString()) + " \n";
                pUpdateClause += ", AgentID=" + (pAgentID == 0 ? "NULL" : pAgentID.ToString()) + " \n";
                pUpdateClause += ", IncotermID=" + (pIncotermID == 0 ? "NULL" : pIncotermID.ToString()) + " \n";
                pUpdateClause += ", MoveTypeID=" + (pMoveTypeID == 0 ? "NULL" : pMoveTypeID.ToString()) + " \n";
                pUpdateClause += ", Notes=" + (pNotes == "0" ? "NULL" : ("N'" + pNotes + "'")) + " \n";

                pUpdateClause += ", CommodityID=" + (pCommodityID == 0 ? "NULL" : pCommodityID.ToString()) + " \n";
                pUpdateClause += ", CustomerReference=" + (pCustomerReference == "0" ? "NULL" : ("'" + pCustomerReference + "'")) + " \n";
                pUpdateClause += ", SupplierReference=" + (pSupplierReference == "0" ? "NULL" : ("'" + pSupplierReference + "'")) + " \n";
                pUpdateClause += ", PONumber=" + (pPONumber == "0" ? "NULL" : ("'" + pPONumber + "'")) + " \n";
                pUpdateClause += ", POrC=" + (pPOrC == 0 ? "NULL" : pPOrC.ToString()) + " \n";

                pUpdateClause += ", IsDelivered = " + (pIsDelivered ? "1" : "0") + " \n";
                pUpdateClause += ", IsReceivedFromShipper = " + (pIsReceivedFromShipper ? "1" : "0") + " \n";
                pUpdateClause += ", ConsigneeID2=" + (pConsigneeID2 == 0 ? "NULL" : pConsigneeID2.ToString()) + " \n";
                pUpdateClause += (pReleaseDate == "01/01/1900" ? ", ReleaseDate = NULL " : ", ReleaseDate = '" + Forwarding.MvcApp.Helpers.DateFunctionsController.GetYYYYMMDDFormat(pReleaseDate, 2) + "'") + " \n";
                pUpdateClause += (pBLDate == "01/01/1900" ? ", BLDate = NULL " : ", BLDate = '" + Forwarding.MvcApp.Helpers.DateFunctionsController.GetYYYYMMDDFormat(pBLDate, 2) + "'") + " \n";
                pUpdateClause += (pShippedOnBoardDate == "01/01/1900" ? ", ShippedOnBoardDate = NULL " : ", ShippedOnBoardDate = '" + Forwarding.MvcApp.Helpers.DateFunctionsController.GetYYYYMMDDFormat(pShippedOnBoardDate, 2) + "'") + " \n";
                pUpdateClause += ", FreightPayableAt=" + (pFreightPayableAt == "0" ? "NULL" : ("N'" + pFreightPayableAt + "'")) + " \n";
                pUpdateClause += ", ACIDNumber=" + (pACIDNumber == "0" ? "NULL" : ("N'" + pACIDNumber + "'")) + " \n";
                pUpdateClause += ", ACIDDetails=" + (pACIDNumberDetails == "0" ? "NULL" : ("N'" + pACIDNumberDetails + "'")) + " \n";
                pUpdateClause += ", BookingNumber=" + (pBookingNumber == "0" ? "NULL" : ("N'" + pBookingNumber + "'")) + " \n";
                pUpdateClause += ", NumberOfOriginalBills=" + (pNumberOfOriginalBills == 0 ? "NULL" : pNumberOfOriginalBills.ToString()) + " \n";
                pUpdateClause += ", CertificateNumber=" + (pCertificateNumber == "0" ? "NULL" : ("N'" + pCertificateNumber + "'")) + " \n";
                pUpdateClause += ", CountryOfOrigin=" + (pCountryOfOrigin == "0" ? "NULL" : ("N'" + pCountryOfOrigin + "'")) + " \n";
                pUpdateClause += ", InvoiceValue=" + (pInvoiceValue == "0" ? "NULL" : ("N'" + pInvoiceValue + "'")) + " \n";
                pUpdateClause += ", CurrencyID=" + (pCurrencyID == 0 ? "NULL" : pCurrencyID.ToString()) + " \n";

                pUpdateClause += " , IMOClass = " + (pIMOClass == Decimal.Zero ? " NULL " : pIMOClass.ToString());
                pUpdateClause += " , UNNumber = " + (pUNNumber == 0 ? " NULL " : pUNNumber.ToString());
                pUpdateClause += " , VesselID = " + (pVesselID == 0 ? " NULL " : pVesselID.ToString());
                pUpdateClause += pOperationManID == -1 ? "" : (" , OperationManID = " + pOperationManID.ToString());

                pUpdateClause += pPOLCountryID == 0 ? "" : (" , POLCountryID = " + pPOLCountryID.ToString());
                pUpdateClause += pPOL == 0 ? "" : (" , POL = " + pPOL.ToString());
                pUpdateClause += pPODCountryID == 0 ? "" : (" , PODCountryID = " + pPODCountryID.ToString());
                pUpdateClause += pPOD == 0 ? "" : (" , POD = " + pPOD.ToString());

                pUpdateClause += " WHERE ID=" + pShipmentID.ToString() + " \n";
                checkException = objCOperations.UpdateList(pUpdateClause);
                #region Update Partners
                //Update Shipper
                pUpdateClause = "CustomerID=" + (pShipperID == 0 ? "NULL" : pShipperID.ToString()) + "\n";
                pUpdateClause += ", ContactID=(select min(ID) from Contacts where PartnerTypeID=" + constCustomerPartnerTypeID + " and PartnerID=" + pShipperID + ")" + "\n";
                pUpdateClause += " WHERE OperationID=" + pShipmentID.ToString() + " AND OperationPartnerTypeID=" + constShipperOperationPartnerTypeID;
                checkException = objCOperationPartners.UpdateList(pUpdateClause);
                //Update Consignee
                pUpdateClause = "CustomerID=" + (pConsigneeID == 0 ? "NULL" : pConsigneeID.ToString()) + "\n";
                pUpdateClause += ", ContactID=(select min(ID) from Contacts where PartnerTypeID=" + constCustomerPartnerTypeID + " and PartnerID=" + pConsigneeID + ")" + "\n";
                pUpdateClause += " WHERE OperationID=" + pShipmentID.ToString() + " AND OperationPartnerTypeID=" + constConsigneeOperationPartnerTypeID;
                checkException = objCOperationPartners.UpdateList(pUpdateClause);
                //Update Agent
                pUpdateClause = "AgentID=" + (pAgentID == 0 ? "NULL" : pAgentID.ToString()) + "\n";
                pUpdateClause += ", ContactID=(select min(ID) from Contacts where PartnerTypeID=" + constAgentPartnerTypeID + " and PartnerID=" + pAgentID + ")" + "\n";
                pUpdateClause += " WHERE OperationID=" + pShipmentID.ToString() + " AND OperationPartnerTypeID=" + constAgentOperationPartnerTypeID;
                checkException = objCOperationPartners.UpdateList(pUpdateClause);
                //Update Notify
                pUpdateClause = "CustomerID=" + (pNotifyID == 0 ? "NULL" : pNotifyID.ToString()) + "\n";
                pUpdateClause += ", ContactID=(select min(ID) from Contacts where PartnerTypeID=" + constCustomerPartnerTypeID + " and PartnerID=" + pNotifyID + ")" + "\n";
                pUpdateClause += " WHERE OperationID=" + pShipmentID.ToString() + " AND OperationPartnerTypeID=" + constNotify1OperationPartnerTypeID;
                checkException = objCOperationPartners.UpdateList(pUpdateClause);
                #endregion Update Partners

                #region Update MainRoute
                pUpdateClause = "DeliveryOrderNumber=" + (pDeliveryOrderNumber == "0" ? "NULL" : ("N'" + pDeliveryOrderNumber.ToString() + "'")) + "\n";
                pUpdateClause += pDeliveryDate == ", DeliveryDate=NULL" ? "" : (", DeliveryDate='" + Forwarding.MvcApp.Helpers.DateFunctionsController.GetYYYYMMDDFormat(pDeliveryDate, 2)) + "' \n";
                pUpdateClause += " WHERE OperationID=" + pShipmentID.ToString() + " AND RoutingTypeID=" + MainCarraigeRoutingTypeID.ToString();
                checkException = objCRoutings.UpdateList(pUpdateClause);

                if (checkException == null)
                {
                    checkException = SaveOperationLog(pShipmentID);
                }
                #endregion Update MainRoute
            } //if (_MessageReturned == "")
            return new object[] {
                checkException == null ? true : false
                , _MessageReturned //pData[1]
            };
        }

        public Exception SaveOperationLog(long pOperationID)
        {
            Exception checkException;
            int TotalRecords = 0;

            var UpdatedHouse = new CvwOperations();
            UpdatedHouse.GetListPaging(9, 1, " WHERE ID= " + pOperationID.ToString(), " ID ", out TotalRecords);
            var obj = UpdatedHouse.lstCVarvwOperations[0];
            checkException = Logging.Save<CVarvwOperations>(ref obj, Convert.ToInt32(UpdatedHouse.lstCVarvwOperations[0].ID));

            return checkException;
        }

        [HttpGet, HttpPost]
        public object[] Shipment_Copy(Int64 pShipmentToCopyID, bool pIncludePayables, bool pIncludeReceivables, Int32 pNumberOfCopies)
        {
            string pReturnedMessage = "";
            int _RowCount = 0;
            Int64 _CurrentHouseNumber = 0;
            Exception checkException = null;
            int MainCarraigeRoutingTypeID = 30;

            COperations objCOperationToCopy = new COperations();
            objCOperationToCopy.GetItem(pShipmentToCopyID);
            int _TempRowCount;
            COperationPartners objCOperationPartnersToCopy = new COperationPartners();
            objCOperationPartnersToCopy.GetList(" WHERE OperationID = " + pShipmentToCopyID.ToString());
            CRoutings objCRoutingsToCopy = new CRoutings();
            objCRoutingsToCopy.GetListPaging(999999, 1, " WHERE RoutingTypeID=" + MainCarraigeRoutingTypeID + " AND OperationID = " + pShipmentToCopyID.ToString(), "ID", out _TempRowCount);
            COperationContainersAndPackages objCOperationContainersAndPackagesToCopy = new COperationContainersAndPackages();
            objCOperationContainersAndPackagesToCopy.GetListPaging(999999, 1, " WHERE OperationID = " + pShipmentToCopyID.ToString(), "ID", out _TempRowCount);

            COperations objCOperationToGetHousNumber = new COperations();
            checkException = objCOperationToGetHousNumber.GetListPaging(999999, 1, "WHERE ISNUMERIC(HouseNumber)=1 AND MasterOperationID=" + objCOperationToCopy.lstCVarOperations[0].MasterOperationID, "CAST(HouseNumber as bigint) DESC", out _RowCount);
            if (objCOperationToGetHousNumber.lstCVarOperations.Count > 0)
                _CurrentHouseNumber = Int64.Parse(objCOperationToGetHousNumber.lstCVarOperations[0].HouseNumber);

            for (int x = 0; x < pNumberOfCopies; x++)
            {
                string pWhereClauseCurrencyDetails = "";
                CvwCurrencyDetails objCvwCurrencyDetails = new CvwCurrencyDetails();
                //CDefaults objCDefaults = new CDefaults();
                //objCDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa
                //int DaysBeforeClose = 14;//default for default which is not handled
                //if (objCOperationToCopy.lstCVarOperations[0].DirectionType == 1/*Import*/ && objCOperationToCopy.lstCVarOperations[0].TransportType == 1/*Ocean*/) //ImportOceanDays
                //    DaysBeforeClose = objCDefaults.lstCVarDefaults[0].ImportOceanDays;
                //else if (objCOperationToCopy.lstCVarOperations[0].DirectionType == 1/*Import*/ && objCOperationToCopy.lstCVarOperations[0].TransportType == 2/*Air*/) //ImportAirDays
                //    DaysBeforeClose = objCDefaults.lstCVarDefaults[0].ImportAirDays;
                //else if (objCOperationToCopy.lstCVarOperations[0].DirectionType == 1/*Import*/ && objCOperationToCopy.lstCVarOperations[0].TransportType == 3/*Inland*/) //ImportInlandDays
                //    DaysBeforeClose = objCDefaults.lstCVarDefaults[0].ImportInlandDays;
                //else if (objCOperationToCopy.lstCVarOperations[0].DirectionType == 2/*Export*/ && objCOperationToCopy.lstCVarOperations[0].TransportType == 1/*Ocean*/) //ExportOceanDays
                //    DaysBeforeClose = objCDefaults.lstCVarDefaults[0].ExportOceanDays;
                //else if (objCOperationToCopy.lstCVarOperations[0].DirectionType == 2/*Export*/ && objCOperationToCopy.lstCVarOperations[0].TransportType == 2/*Air*/) //ExportAirDays
                //    DaysBeforeClose = objCDefaults.lstCVarDefaults[0].ExportAirDays;
                //else if (objCOperationToCopy.lstCVarOperations[0].DirectionType == 2/*Export*/ && objCOperationToCopy.lstCVarOperations[0].TransportType == 3/*Inland*/) //ExportInlandDays
                //    DaysBeforeClose = objCDefaults.lstCVarDefaults[0].ExportInlandDays;
                //else if (objCOperationToCopy.lstCVarOperations[0].DirectionType == 2/*Domestic*/ && objCOperationToCopy.lstCVarOperations[0].TransportType == 1/*Ocean*/) //DomesticOceanDays
                //    DaysBeforeClose = objCDefaults.lstCVarDefaults[0].DomesticOceanDays;
                //else if (objCOperationToCopy.lstCVarOperations[0].DirectionType == 2/*Domestic*/ && objCOperationToCopy.lstCVarOperations[0].TransportType == 2/*Air*/) //DomesticAirDays
                //    DaysBeforeClose = objCDefaults.lstCVarDefaults[0].DomesticAirDays;
                //else if (objCOperationToCopy.lstCVarOperations[0].DirectionType == 2/*Domestic*/ && objCOperationToCopy.lstCVarOperations[0].TransportType == 3/*Inland*/) //DomesticInlandDays
                //    DaysBeforeClose = objCDefaults.lstCVarDefaults[0].DomesticInlandDays;

                //bool _result = true;

                CVarOperations objCVarOperations = new CVarOperations();
                #region Copying the operation itself
                //objCVarOperations.Code = "O" + DateTime.Now.Year.ToString().Substring(2, 2) + "-"
                //    + (objCOperationToCopy.lstCVarOperations[0].DirectionType == 1 ? "IMP" : (objCOperationToCopy.lstCVarOperations[0].DirectionType == 2 ? "EXP" : "DOM")) + "-"
                //    + (objCOperationToCopy.lstCVarOperations[0].TransportType == 1 ? "OC" : (objCOperationToCopy.lstCVarOperations[0].TransportType == 2 ? "AI" : "IN")) + "-"
                //    ;
                objCVarOperations.Code = "0";
                objCVarOperations.HouseNumber = (++_CurrentHouseNumber).ToString();
                objCVarOperations.MasterOperationID = objCOperationToCopy.lstCVarOperations[0].MasterOperationID;
                objCVarOperations.BranchID = objCOperationToCopy.lstCVarOperations[0].BranchID;
                objCVarOperations.SalesmanID = objCOperationToCopy.lstCVarOperations[0].SalesmanID;
                objCVarOperations.BLType = objCOperationToCopy.lstCVarOperations[0].BLType;
                objCVarOperations.BLTypeIconName = objCOperationToCopy.lstCVarOperations[0].BLTypeIconName;
                objCVarOperations.BLTypeIconStyle = objCOperationToCopy.lstCVarOperations[0].BLTypeIconStyle;
                objCVarOperations.DirectionType = objCOperationToCopy.lstCVarOperations[0].DirectionType;
                objCVarOperations.DirectionIconName = objCOperationToCopy.lstCVarOperations[0].DirectionIconName;
                objCVarOperations.DirectionIconStyle = objCOperationToCopy.lstCVarOperations[0].DirectionIconStyle;
                objCVarOperations.TransportType = objCOperationToCopy.lstCVarOperations[0].TransportType;
                objCVarOperations.TransportIconName = objCOperationToCopy.lstCVarOperations[0].TransportIconName;
                objCVarOperations.TransportIconStyle = objCOperationToCopy.lstCVarOperations[0].TransportIconStyle;
                objCVarOperations.ShipmentType = objCOperationToCopy.lstCVarOperations[0].ShipmentType;
                objCVarOperations.MasterBL = objCOperationToCopy.lstCVarOperations[0].MasterBL;
                objCVarOperations.MAWBSuffix = objCOperationToCopy.lstCVarOperations[0].MAWBSuffix;
                objCVarOperations.BLDate = objCOperationToCopy.lstCVarOperations[0].BLDate;
                objCVarOperations.HBLDate = objCOperationToCopy.lstCVarOperations[0].HBLDate;
                objCVarOperations.ReleaseDate = objCOperationToCopy.lstCVarOperations[0].ReleaseDate;
                objCVarOperations.Form13Date = objCOperationToCopy.lstCVarOperations[0].Form13Date;
                objCVarOperations.PODate = objCOperationToCopy.lstCVarOperations[0].PODate;

                objCVarOperations.ClearanceApprovalDate = objCOperationToCopy.lstCVarOperations[0].ClearanceApprovalDate;
                objCVarOperations.TruckingApprovalDate = objCOperationToCopy.lstCVarOperations[0].TruckingApprovalDate;
                objCVarOperations.FreightApprovalDate = objCOperationToCopy.lstCVarOperations[0].FreightApprovalDate;

                objCVarOperations.ShippedOnBoardDate = objCOperationToCopy.lstCVarOperations[0].ShippedOnBoardDate;
                objCVarOperations.FreightPayableAt = objCOperationToCopy.lstCVarOperations[0].FreightPayableAt;
                objCVarOperations.CertificateNumber = "0";
                objCVarOperations.CountryOfOrigin = objCOperationToCopy.lstCVarOperations[0].CountryOfOrigin;
                objCVarOperations.InvoiceValue = "0";
                objCVarOperations.NumberOfOriginalBills = 0;

                objCVarOperations.ShipperID = objCOperationToCopy.lstCVarOperations[0].ShipperID;
                objCVarOperations.ShipperAddressID = objCOperationToCopy.lstCVarOperations[0].ShipperAddressID;
                objCVarOperations.ShipperContactID = objCOperationToCopy.lstCVarOperations[0].ShipperContactID;
                objCVarOperations.ConsigneeID = objCOperationToCopy.lstCVarOperations[0].ConsigneeID;
                objCVarOperations.ConsigneeAddressID = objCOperationToCopy.lstCVarOperations[0].ConsigneeAddressID;
                objCVarOperations.ConsigneeContactID = objCOperationToCopy.lstCVarOperations[0].ConsigneeContactID;
                objCVarOperations.AgentID = objCOperationToCopy.lstCVarOperations[0].AgentID;
                objCVarOperations.AgentAddressID = objCOperationToCopy.lstCVarOperations[0].AgentAddressID;
                objCVarOperations.AgentContactID = objCOperationToCopy.lstCVarOperations[0].AgentContactID;
                objCVarOperations.IncotermID = objCOperationToCopy.lstCVarOperations[0].IncotermID;
                objCVarOperations.POrC = objCOperationToCopy.lstCVarOperations[0].POrC;
                objCVarOperations.MoveTypeID = objCOperationToCopy.lstCVarOperations[0].MoveTypeID;
                objCVarOperations.CommodityID = objCOperationToCopy.lstCVarOperations[0].CommodityID;
                objCVarOperations.CommodityID2 = objCOperationToCopy.lstCVarOperations[0].CommodityID2;
                objCVarOperations.CommodityID3 = objCOperationToCopy.lstCVarOperations[0].CommodityID3;
                objCVarOperations.TransientTime = objCOperationToCopy.lstCVarOperations[0].TransientTime;
                objCVarOperations.OpenDate = DateTime.Now;
                objCVarOperations.CloseDate = objCOperationToCopy.lstCVarOperations[0].CloseDate; //DateTime.Parse("01-01-1900");
                objCVarOperations.CutOffDate = DateTime.Parse("01-01-1900");
                objCVarOperations.IncludePickup = objCOperationToCopy.lstCVarOperations[0].IncludePickup;
                objCVarOperations.PickupCityID = objCOperationToCopy.lstCVarOperations[0].PickupCityID;
                objCVarOperations.PickupAddressID = objCOperationToCopy.lstCVarOperations[0].PickupAddressID;
                objCVarOperations.POLCountryID = objCOperationToCopy.lstCVarOperations[0].POLCountryID;
                objCVarOperations.POL = objCOperationToCopy.lstCVarOperations[0].POL;
                objCVarOperations.PODCountryID = objCOperationToCopy.lstCVarOperations[0].PODCountryID;
                objCVarOperations.POD = objCOperationToCopy.lstCVarOperations[0].POD;
                objCVarOperations.PickupAddress = objCOperationToCopy.lstCVarOperations[0].PickupAddress;
                objCVarOperations.DeliveryAddress = objCOperationToCopy.lstCVarOperations[0].DeliveryAddress;
                objCVarOperations.ShippingLineID = objCOperationToCopy.lstCVarOperations[0].ShippingLineID;
                objCVarOperations.AirlineID = objCOperationToCopy.lstCVarOperations[0].AirlineID;
                objCVarOperations.TruckerID = objCOperationToCopy.lstCVarOperations[0].TruckerID;
                objCVarOperations.IncludeDelivery = objCOperationToCopy.lstCVarOperations[0].IncludeDelivery;
                objCVarOperations.DeliveryZipCode = objCOperationToCopy.lstCVarOperations[0].DeliveryZipCode;
                objCVarOperations.DeliveryCityID = objCOperationToCopy.lstCVarOperations[0].DeliveryCityID;
                objCVarOperations.DeliveryCountryID = objCOperationToCopy.lstCVarOperations[0].DeliveryCountryID;
                objCVarOperations.IsDangerousGoods = objCOperationToCopy.lstCVarOperations[0].IsDangerousGoods;
                objCVarOperations.Notes = objCOperationToCopy.lstCVarOperations[0].Notes;
                objCVarOperations.CustomerReference = "0";
                objCVarOperations.SupplierReference = "0";
                objCVarOperations.PONumber = "0";
                objCVarOperations.POValue = "0";
                objCVarOperations.ReleaseNumber = "0";
                objCVarOperations.DispatchNumber = "0";
                objCVarOperations.BusinessUnit = "0";
                objCVarOperations.Form13Number = "0";
                objCVarOperations.AgreedRate = "0";
                objCVarOperations.IsDelivered = objCOperationToCopy.lstCVarOperations[0].IsDelivered;
                objCVarOperations.IsTrucking = objCOperationToCopy.lstCVarOperations[0].IsTrucking;
                objCVarOperations.IsInsurance = objCOperationToCopy.lstCVarOperations[0].IsInsurance;
                objCVarOperations.IsClearance = objCOperationToCopy.lstCVarOperations[0].IsClearance;
                objCVarOperations.IsGenset = objCOperationToCopy.lstCVarOperations[0].IsGenset;
                objCVarOperations.IsCourrier = objCOperationToCopy.lstCVarOperations[0].IsCourrier;
                objCVarOperations.IsTelexRelease = objCOperationToCopy.lstCVarOperations[0].IsTelexRelease;
                objCVarOperations.OperationStageID = objCOperationToCopy.lstCVarOperations[0].OperationStageID;
                objCVarOperations.NumberOfHousesConnected = 0;
                objCVarOperations.CreatorUserID = objCVarOperations.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarOperations.CreationDate = objCVarOperations.ModificationDate = DateTime.Now;

                #region Venus fields A.Medra
                objCVarOperations.BLDate = objCOperationToCopy.lstCVarOperations[0].BLDate;
                objCVarOperations.MAWBStockID = 0;
                objCVarOperations.TypeOfStockID = 0;
                objCVarOperations.FlightNo = "0";
                objCVarOperations.IsAWB = objCOperationToCopy.lstCVarOperations[0].IsAWB;
                //Fields not in insert
                objCVarOperations.AirLineID1 = 0;
                objCVarOperations.FlightNo1 = "0";
                objCVarOperations.FlightDate1 = objCOperationToCopy.lstCVarOperations[0].FlightDate1;
                objCVarOperations.AirLineID2 = 0;
                objCVarOperations.FlightNo2 = "0";
                objCVarOperations.FlightDate2 = objCOperationToCopy.lstCVarOperations[0].FlightDate2;
                objCVarOperations.AirLineID3 = 0;
                objCVarOperations.FlightNo3 = "0";
                objCVarOperations.FlightDate3 = objCOperationToCopy.lstCVarOperations[0].FlightDate3;

                objCVarOperations.UNOrID = "0";
                objCVarOperations.ProperShippingName = "0";
                objCVarOperations.ClassOrDivision = "0";
                objCVarOperations.PackingGroup = "0";
                objCVarOperations.QuantityAndTypeOfPacking = "0";
                objCVarOperations.PackingInstruction = "0";
                objCVarOperations.ShippingDeclarationAuthorization = "0";
                objCVarOperations.Barcode = "0";

                objCVarOperations.GuaranteeLetterNumber = "0";
                objCVarOperations.GuaranteeLetterDate = DateTime.Parse("01/01/1900");
                objCVarOperations.GuaranteeLetterAmount = "0";
                objCVarOperations.GuaranteeLetterSupplierInvoiceNumber = "0";
                objCVarOperations.BankAccountID = 0;
                objCVarOperations.GuaranteeLetterNotes = "0";

                objCVarOperations.HandlingInformation = "0";
                objCVarOperations.AmountOfInsurance = "0";
                objCVarOperations.DeclaredValueForCarriage = "0";
                objCVarOperations.WeightCharge = 0;
                objCVarOperations.ValuationCharge = 0;
                objCVarOperations.OtherChargesDueAgent = 0;
                objCVarOperations.OtherCharges = "0";
                objCVarOperations.CurrencyID = objCOperationToCopy.lstCVarOperations[0].CurrencyID;
                objCVarOperations.AccountingInformation = "0";
                objCVarOperations.ReferenceNumber = "0";
                objCVarOperations.OptionalShippingInformation = "0";
                objCVarOperations.CHGSCode = "0";
                objCVarOperations.WT_VALL_Other = "0";
                objCVarOperations.DeclaredValueForCustoms = "0";
                objCVarOperations.Tax = 0;
                objCVarOperations.OtherChargesDueCarrier = 0;
                objCVarOperations.WT_VALL = "0";
                objCVarOperations.IsAWB = objCOperationToCopy.lstCVarOperations[0].IsAWB;
                #endregion Venus fields A.Medra

                #region Cargo
                objCVarOperations.TareWeight = objCOperationToCopy.lstCVarOperations[0].TareWeight;
                objCVarOperations.GrossWeight = objCOperationToCopy.lstCVarOperations[0].GrossWeight;
                objCVarOperations.VGM = objCOperationToCopy.lstCVarOperations[0].VGM;
                objCVarOperations.NetWeight = objCOperationToCopy.lstCVarOperations[0].NetWeight;
                objCVarOperations.Volume = objCOperationToCopy.lstCVarOperations[0].Volume;
                objCVarOperations.ChargeableWeight = objCOperationToCopy.lstCVarOperations[0].ChargeableWeight;
                objCVarOperations.VolumetricWeight = objCOperationToCopy.lstCVarOperations[0].VolumetricWeight;
                objCVarOperations.NumberOfPackages = objCOperationToCopy.lstCVarOperations[0].NumberOfPackages;
                objCVarOperations.PackageTypeID = objCOperationToCopy.lstCVarOperations[0].PackageTypeID;
                objCVarOperations.MarksAndNumbers = objCOperationToCopy.lstCVarOperations[0].MarksAndNumbers;
                objCVarOperations.Description = objCOperationToCopy.lstCVarOperations[0].Description;
                #endregion Cargo

                objCVarOperations.DismissalPermissionSerial = "0";
                objCVarOperations.DeliveryOrderSerial = "0";

                objCVarOperations.eFBLID = "0";
                objCVarOperations.eFBLStatus = 0;

                objCVarOperations.IMOClass = Decimal.Zero;
                objCVarOperations.UNNumber = 0;
                objCVarOperations.VesselID = 0;
                objCVarOperations.BookingNumber = "0";
                objCVarOperations.ACIDNumber = "0";
                objCVarOperations.ACIDDetails = "0";
                objCVarOperations.HouseParentID = 0;

                COperations objCOperations = new COperations();
                objCOperations.lstCVarOperations.Add(objCVarOperations);
                checkException = objCOperations.SaveMethod(objCOperations.lstCVarOperations);

                #region CreateCostCenter
                CSystemOptions objCSystemOptions = new CSystemOptions();
                objCSystemOptions.GetList("Where OptionID=94");
                if (objCSystemOptions.lstCVarSystemOptions.Count > 0 && objCSystemOptions.lstCVarSystemOptions[0].OptionValue
                      && objCOperations.lstCVarOperations[0].BLType != 2)
                {
                    string CostCenterNumberParent = "";
                    Int32 pID = 0;
                    CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
                    CA_CostCenters objCA_CostCentersParent = new CA_CostCenters();
                    objCA_CostCentersParent.GetList("where ( CostCenterName=N'عمليات' or CostCenterName='Operations' ) and Parent_ID is null ");
                    if (objCA_CostCentersParent.lstCVarA_CostCenters.Count > 0)
                    {
                        pID = objCA_CostCentersParent.lstCVarA_CostCenters[0].ID;
                        CostCenterNumberParent = objCA_CostCentersParent.lstCVarA_CostCenters[0].RealCostCenterCode;
                    }

                    else
                    {

                        string pNewCodePartner = objCCustomizedDBCall.CallStringFunction("SELECT [dbo].A_CostCenters_GetCode(" + (pID == 0 ? "null" : pID.ToString()) + ") AS Code");


                        CVarA_CostCenters objCVarA_CostCenters = new CVarA_CostCenters();
                        objCVarA_CostCenters.CostCenterNumber = pNewCodePartner.PadRight(12, '0');
                        objCVarA_CostCenters.CostCenterName = "Operations";
                        objCVarA_CostCenters.Parent_ID = 0;
                        objCVarA_CostCenters.IsMain = true;
                        objCVarA_CostCenters.CCLevel = 1;
                        objCVarA_CostCenters.RealCostCenterCode = pNewCodePartner;
                        objCVarA_CostCenters.User_ID = WebSecurity.CurrentUserId;
                        objCVarA_CostCenters.Type_ID = 0;
                        objCVarA_CostCenters.IsClosed = false;
                        objCVarA_CostCenters.SubAccountGroupID = 0;
                        objCVarA_CostCenters.EmployeesCount = 0;
                        objCA_CostCentersParent.lstCVarA_CostCenters.Add(objCVarA_CostCenters);
                        checkException = objCA_CostCentersParent.SaveMethod(objCA_CostCentersParent.lstCVarA_CostCenters);

                        pID = objCA_CostCentersParent.lstCVarA_CostCenters[0].ID;
                        CostCenterNumberParent = objCA_CostCentersParent.lstCVarA_CostCenters[0].RealCostCenterCode;
                    }

                    CA_CostCenters objCA_CostCenters = new CA_CostCenters();
                    checkException = objCA_CostCenters.GetListPaging(1, 1, "WHERE ID = " + pID.ToString(), "ID", out _RowCount);
                    string pNewCode = objCCustomizedDBCall.CallStringFunction("SELECT [dbo].A_CostCenters_GetCode(" + (pID == 0 ? "null" : pID.ToString()) + ") AS Code");

                    CvwOperations objCvwOperations = new CvwOperations();
                    objCvwOperations.GetList("WHERE ID=" + objCOperations.lstCVarOperations[0].ID);
                    string pNewCodeNew = CostCenterNumberParent + pNewCode;
                    CVarA_CostCenters objCVarA_CostCentersChild = new CVarA_CostCenters();
                    objCVarA_CostCentersChild.CostCenterNumber = pNewCodeNew.PadRight(12, '0'); ;
                    objCVarA_CostCentersChild.CostCenterName = objCvwOperations.lstCVarvwOperations[0].Code + " - " + objCvwOperations.lstCVarvwOperations[0].ClientName;
                    objCVarA_CostCentersChild.Parent_ID = pID;
                    objCVarA_CostCentersChild.IsMain = false;
                    objCVarA_CostCentersChild.CCLevel = 2;
                    objCVarA_CostCentersChild.RealCostCenterCode = pNewCodeNew;
                    objCVarA_CostCentersChild.User_ID = WebSecurity.CurrentUserId;
                    objCVarA_CostCentersChild.Type_ID = 0;
                    objCVarA_CostCentersChild.IsClosed = false;
                    objCVarA_CostCentersChild.SubAccountGroupID = 0;
                    objCVarA_CostCentersChild.EmployeesCount = 0;
                    objCA_CostCentersParent.lstCVarA_CostCenters.Add(objCVarA_CostCentersChild);
                    checkException = objCA_CostCentersParent.SaveMethod(objCA_CostCentersParent.lstCVarA_CostCenters);

                    //Link Oeation With CostCenter Add by ahmed maher
                    int _RowCount2 = 0;
                    CvwDefaults objCvwDefaults = new CvwDefaults();
                    objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount2); //i am sure i ve just one row isa
                    string CompanyName2 = objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName;
                    if (CompanyName2 == "BED")
                    {
                        CVarA_LinkOperationWithCostCenter objCVarA_LinkOperationWithCostCenter = new CVarA_LinkOperationWithCostCenter();

                        objCVarA_LinkOperationWithCostCenter.OperationID = objCOperations.lstCVarOperations[0].ID;
                        objCVarA_LinkOperationWithCostCenter.CostCenterID = objCVarA_CostCentersChild.ID;

                        CA_LinkOperationWithCostCenter objA_LinkOperationWithCostCenter = new CA_LinkOperationWithCostCenter();
                        objA_LinkOperationWithCostCenter.lstCVarA_LinkOperationWithCostCenter.Add(objCVarA_LinkOperationWithCostCenter);
                        checkException = objA_LinkOperationWithCostCenter.SaveMethod(objA_LinkOperationWithCostCenter.lstCVarA_LinkOperationWithCostCenter);
                    }

                }


                #endregion
                #endregion copying the operation itself

                #region Copy OperationPartners //COPY Partners To OperationPartners
                COperationPartners objCOperationPartners = new COperationPartners();
                for (int i = 0; i < objCOperationPartnersToCopy.lstCVarOperationPartners.Count; i++)
                {
                    CVarOperationPartners objCVarOperationPartners = new CVarOperationPartners();
                    objCVarOperationPartners.OperationID = objCOperations.lstCVarOperations[0].ID;
                    objCVarOperationPartners.OperationPartnerTypeID = objCOperationPartnersToCopy.lstCVarOperationPartners[i].OperationPartnerTypeID;
                    objCVarOperationPartners.CustomerID = objCOperationPartnersToCopy.lstCVarOperationPartners[i].CustomerID;
                    objCVarOperationPartners.AgentID = objCOperationPartnersToCopy.lstCVarOperationPartners[i].AgentID;
                    objCVarOperationPartners.ShippingAgentID = objCOperationPartnersToCopy.lstCVarOperationPartners[i].ShippingAgentID;
                    objCVarOperationPartners.CustomsClearanceAgentID = objCOperationPartnersToCopy.lstCVarOperationPartners[i].CustomsClearanceAgentID;
                    objCVarOperationPartners.ShippingLineID = objCOperationPartnersToCopy.lstCVarOperationPartners[i].ShippingLineID;
                    objCVarOperationPartners.AirlineID = objCOperationPartnersToCopy.lstCVarOperationPartners[i].AirlineID;
                    objCVarOperationPartners.TruckerID = objCOperationPartnersToCopy.lstCVarOperationPartners[i].TruckerID;
                    objCVarOperationPartners.SupplierID = objCOperationPartnersToCopy.lstCVarOperationPartners[i].SupplierID;
                    objCVarOperationPartners.ContactID = objCOperationPartnersToCopy.lstCVarOperationPartners[i].ContactID;
                    objCVarOperationPartners.IsOperationClient = objCOperationPartnersToCopy.lstCVarOperationPartners[i].IsOperationClient;
                    objCVarOperationPartners.CreatorUserID = objCVarOperationPartners.ModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarOperationPartners.CreationDate = objCVarOperationPartners.ModificationDate = DateTime.Now;
                    objCOperationPartners.lstCVarOperationPartners.Add(objCVarOperationPartners);
                }
                objCOperationPartners.SaveMethod(objCOperationPartners.lstCVarOperationPartners);
                #endregion Copy Operation Partners

                #region Copy OperationRoutings
                CRoutings objCRoutings = new CRoutings();
                for (int i = 0; i < objCRoutingsToCopy.lstCVarRoutings.Count; i++)
                {
                    CVarRoutings objCVarRoutings = new CVarRoutings();
                    objCVarRoutings.OperationID = objCOperations.lstCVarOperations[0].ID;
                    objCVarRoutings.RoutingTypeID = objCRoutingsToCopy.lstCVarRoutings[i].RoutingTypeID;
                    objCVarRoutings.TransportType = objCRoutingsToCopy.lstCVarRoutings[i].TransportType;
                    objCVarRoutings.TransportIconName = objCRoutingsToCopy.lstCVarRoutings[i].TransportIconName;
                    objCVarRoutings.TransportIconStyle = objCRoutingsToCopy.lstCVarRoutings[i].TransportIconStyle;
                    objCVarRoutings.POLCountryID = objCRoutingsToCopy.lstCVarRoutings[i].POLCountryID;
                    objCVarRoutings.POL = objCRoutingsToCopy.lstCVarRoutings[i].POL;
                    objCVarRoutings.PODCountryID = objCRoutingsToCopy.lstCVarRoutings[i].PODCountryID;
                    objCVarRoutings.POD = objCRoutingsToCopy.lstCVarRoutings[i].POD;
                    objCVarRoutings.ETAPOLDate = objCRoutingsToCopy.lstCVarRoutings[i].ETAPOLDate;
                    objCVarRoutings.ATAPOLDate = objCRoutingsToCopy.lstCVarRoutings[i].ATAPOLDate;
                    objCVarRoutings.ExpectedDeparture = objCRoutingsToCopy.lstCVarRoutings[i].ExpectedDeparture;
                    objCVarRoutings.ActualDeparture = objCRoutingsToCopy.lstCVarRoutings[i].ActualDeparture;
                    objCVarRoutings.ExpectedArrival = objCRoutingsToCopy.lstCVarRoutings[i].ExpectedArrival;
                    objCVarRoutings.ActualArrival = objCRoutingsToCopy.lstCVarRoutings[i].ActualArrival;
                    objCVarRoutings.ShippingLineID = objCRoutingsToCopy.lstCVarRoutings[i].ShippingLineID;
                    objCVarRoutings.AirlineID = objCRoutingsToCopy.lstCVarRoutings[i].AirlineID;
                    objCVarRoutings.TruckerID = objCRoutingsToCopy.lstCVarRoutings[i].TruckerID;
                    objCVarRoutings.VesselID = objCRoutingsToCopy.lstCVarRoutings[i].VesselID;
                    objCVarRoutings.VoyageOrTruckNumber = objCRoutingsToCopy.lstCVarRoutings[i].VoyageOrTruckNumber;

                    objCVarRoutings.GensetSupplierID = 0; //objCRoutingsToCopy.lstCVarRoutings[i].GensetSupplierID;
                    objCVarRoutings.CCAID = 0; //objCRoutingsToCopy.lstCVarRoutings[i].CCAID;
                    objCVarRoutings.Quantity = "0"; //objCRoutingsToCopy.lstCVarRoutings[i].Quantity;
                    objCVarRoutings.ContactPerson = "0"; //objCRoutingsToCopy.lstCVarRoutings[i].ContactPerson;
                    objCVarRoutings.PickupAddress = "0"; //objCRoutingsToCopy.lstCVarRoutings[i].PickupAddress;
                    objCVarRoutings.DeliveryAddress = "0"; //objCRoutingsToCopy.lstCVarRoutings[i].DeliveryAddress;
                    objCVarRoutings.GateInPortID = 0; //objCRoutingsToCopy.lstCVarRoutings[i].GateInPortID;
                    objCVarRoutings.GateOutPortID = 0; //objCRoutingsToCopy.lstCVarRoutings[i].GateOutPortID;
                    objCVarRoutings.GateInDate = DateTime.Parse("01/01/1900"); //objCRoutingsToCopy.lstCVarRoutings[i].GateInDate;

                    #region TransportOrder
                    objCVarRoutings.CustomerID = 0;
                    objCVarRoutings.SubContractedCustomerID = 0;
                    objCVarRoutings.Cost = 0;
                    objCVarRoutings.Sale = 0;
                    objCVarRoutings.IsFleet = false;
                    objCVarRoutings.CommodityID = 0;
                    objCVarRoutings.LoadingDate = DateTime.Parse("01/01/1900");
                    objCVarRoutings.LoadingReference = "0";
                    objCVarRoutings.UnloadingDate = DateTime.Parse("01/01/1900");
                    objCVarRoutings.UnloadingReference = "0";
                    objCVarRoutings.UnloadingTime = "0";
                    #endregion TransportOrder

                    objCVarRoutings.GateOutDate = DateTime.Parse("01/01/1900"); //objCRoutingsToCopy.lstCVarRoutings[i].GateOutDate;
                    objCVarRoutings.StuffingDate = DateTime.Parse("01/01/1900"); //objCRoutingsToCopy.lstCVarRoutings[i].StuffingDate;
                    objCVarRoutings.DeliveryDate = DateTime.Parse("01/01/1900"); //objCRoutingsToCopy.lstCVarRoutings[i].DeliveryDate;
                    objCVarRoutings.BookingNumber = "0"; // objCRoutingsToCopy.lstCVarRoutings[i].BookingNumber;
                    objCVarRoutings.Delays = "0";
                    objCVarRoutings.DriverName = "0";
                    objCVarRoutings.DriverPhones = "0";
                    objCVarRoutings.PowerFromGateInTillActualSailing = "0"; //objCRoutingsToCopy.lstCVarRoutings[i].PowerFromGateInTillActualSailing;
                    objCVarRoutings.ContactPersonPhones = "0"; //objCRoutingsToCopy.lstCVarRoutings[i].ContactPersonPhones;
                    objCVarRoutings.LoadingTime = "0"; //objCRoutingsToCopy.lstCVarRoutings[i].LoadingTime;

                    #region CustomsClearance
                    objCVarRoutings.CCAFreight = 0;
                    objCVarRoutings.CCAFOB = 0;
                    objCVarRoutings.CCACFValue = 0;
                    objCVarRoutings.CCAInvoiceNumber = "0";

                    objCVarRoutings.CCAInsurance = "0";
                    objCVarRoutings.CCADischargeValue = "0";
                    objCVarRoutings.CCAAcceptedValue = "0";
                    objCVarRoutings.CCAImportValue = "0";
                    objCVarRoutings.CCADocumentReceiveDate = DateTime.Parse("01/01/1900");
                    objCVarRoutings.CCAExchangeRate = "0";
                    objCVarRoutings.CCAVATCertificateNumber = "0";
                    objCVarRoutings.CCAVATCertificateValue = "0";
                    objCVarRoutings.CCACommercialProfitCertificateNumber = "0";
                    objCVarRoutings.CCAOthers = "0";
                    objCVarRoutings.CCASpendDate = DateTime.Parse("01/01/1900");
                    objCVarRoutings.OffloadingDate = DateTime.Parse("01/01/1900");

                    objCVarRoutings.CertificateNumber = "0";
                    objCVarRoutings.CertificateValue = "0";
                    objCVarRoutings.CertificateDate = DateTime.Parse("01/01/1900");
                    objCVarRoutings.QasimaNumber = "0";
                    objCVarRoutings.QasimaDate = DateTime.Parse("01/01/1900");
                    objCVarRoutings.Match = false;
                    objCVarRoutings.SalesDateReceived = DateTime.Parse("01/01/1900");
                    objCVarRoutings.CommerceDateReceived = DateTime.Parse("01/01/1900");
                    objCVarRoutings.InspectionDateReceived = DateTime.Parse("01/01/1900");
                    objCVarRoutings.FinishDateReceived = DateTime.Parse("01/01/1900");
                    objCVarRoutings.SalesDateDelivered = DateTime.Parse("01/01/1900");
                    objCVarRoutings.CommerceDateDelivered = DateTime.Parse("01/01/1900");
                    objCVarRoutings.InspectionDateDelivered = DateTime.Parse("01/01/1900");
                    objCVarRoutings.FinishDateDelivered = DateTime.Parse("01/01/1900");

                    objCVarRoutings.CCAllowTemporaryDelivered = DateTime.Parse("01/01/1900");
                    objCVarRoutings.CCAllowTemporaryReceived = DateTime.Parse("01/01/1900");
                    objCVarRoutings.CCDropBackDelivered = DateTime.Parse("01/01/1900");
                    objCVarRoutings.CCDropBackReceived = DateTime.Parse("01/01/1900");
                    objCVarRoutings.CC_ClearanceTypeID = 0;
                    objCVarRoutings.CC_CustomItemsID = 0;
                    objCVarRoutings.CCReleaseNo = "0";
                    #endregion CustomsClearance

                    objCVarRoutings.BillNumber = "0";
                    objCVarRoutings.TruckingOrderCode = "0";

                    objCVarRoutings.RoadNumber = "0";
                    objCVarRoutings.DeliveryOrderNumber = "0";
                    objCVarRoutings.WareHouse = "0";
                    objCVarRoutings.WareHouseLocation = "0";

                    objCVarRoutings.Notes = "0"; // objCRoutingsToCopy.lstCVarRoutings[i].Notes;

                    objCVarRoutings.CreatorUserID = objCVarRoutings.ModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarRoutings.CreationDate = objCVarRoutings.ModificationDate = DateTime.Now;
                    objCRoutings.lstCVarRoutings.Add(objCVarRoutings);
                }
                objCRoutings.SaveMethod(objCRoutings.lstCVarRoutings);
                #endregion Copy OperationRoutings

                #region Copy OperationContainersAndPackages
                //COperationContainersAndPackages objCOperationContainersAndPackages = new COperationContainersAndPackages();
                //for (int i = 0; i < objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages.Count; i++)
                //{
                //    CVarOperationContainersAndPackages objCVarOperationContainersAndPackages = new CVarOperationContainersAndPackages();
                //    objCVarOperationContainersAndPackages.OperationID = objCOperations.lstCVarOperations[0].ID;
                //    #region AirAgents columns
                //    objCVarOperationContainersAndPackages.Rate = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].Rate;
                //    objCVarOperationContainersAndPackages.IsAsAgreed = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].IsAsAgreed;
                //    objCVarOperationContainersAndPackages.IsMinimum = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].IsMinimum;
                //    objCVarOperationContainersAndPackages.WeightUnit = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].WeightUnit;
                //    objCVarOperationContainersAndPackages.RateClass = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].RateClass;

                //    #endregion AirAgents columns

                //    #region ContainerTracking
                //    objCVarOperationContainersAndPackages.GateOutPortID = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].GateOutPortID;
                //    objCVarOperationContainersAndPackages.GateInPortID = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].GateInPortID;
                //    objCVarOperationContainersAndPackages.GateOutDate = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].GateOutDate;
                //    objCVarOperationContainersAndPackages.StuffingDate = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].StuffingDate;
                //    objCVarOperationContainersAndPackages.LoadingDate = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].LoadingDate;
                //    objCVarOperationContainersAndPackages.GateOutAndLoadingDatesDifference = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].GateOutAndLoadingDatesDifference;
                //    objCVarOperationContainersAndPackages.Factory = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].Factory;
                //    objCVarOperationContainersAndPackages.ExportBLNumber = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].ExportBLNumber;
                //    objCVarOperationContainersAndPackages.ImportBLNumber = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].ImportBLNumber;
                //    objCVarOperationContainersAndPackages.IsTracked = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].IsTracked;
                //    objCVarOperationContainersAndPackages.IsOwnedByCompany = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].IsOwnedByCompany;
                //    objCVarOperationContainersAndPackages.TrailerID = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].TrailerID;
                //    objCVarOperationContainersAndPackages.DriverID = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].DriverID;
                //    objCVarOperationContainersAndPackages.DriverAssistantID = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].DriverAssistantID;
                //    objCVarOperationContainersAndPackages.SupplierTrailerName = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].SupplierTrailerName;
                //    objCVarOperationContainersAndPackages.SupplierDriverName = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].SupplierDriverName;
                //    objCVarOperationContainersAndPackages.SupplierDriverAssistantName = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].SupplierDriverAssistantName;

                //    objCVarOperationContainersAndPackages.WeightUnit = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].WeightUnit;
                //    objCVarOperationContainersAndPackages.RateClass = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].RateClass;
                //    #endregion ContainerTracking

                //    #region Tank
                //    objCVarOperationContainersAndPackages.TankOrFlexiNumber = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].TankOrFlexiNumber;
                //    objCVarOperationContainersAndPackages.OperatorID = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].OperatorID;

                //    objCVarOperationContainersAndPackages.IsFull = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].IsFull;
                //    objCVarOperationContainersAndPackages.ExitDate = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].ExitDate;
                //    objCVarOperationContainersAndPackages.ReturnDate = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].ReturnDate;
                //    objCVarOperationContainersAndPackages.FreeDays = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].FreeDays;
                //    objCVarOperationContainersAndPackages.DayValue = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].DayValue;
                //    #endregion Tank
                //    #region Yard
                //    objCVarOperationContainersAndPackages.YardEIRNumber = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].YardEIRNumber;
                //    objCVarOperationContainersAndPackages.YardEIRNumberOut = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].YardEIRNumberOut;
                //    objCVarOperationContainersAndPackages.YardInDate = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].YardInDate;
                //    objCVarOperationContainersAndPackages.YardInTime = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].YardInTime;
                //    objCVarOperationContainersAndPackages.YardOutDate = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].YardOutDate;
                //    objCVarOperationContainersAndPackages.YardOutTime = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].YardOutTime;
                //    objCVarOperationContainersAndPackages.YardLocationID = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].YardLocationID;
                //    objCVarOperationContainersAndPackages.YardIsIn = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].YardIsIn;
                //    #endregion Yard

                //    objCVarOperationContainersAndPackages.ContainerTypeID = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].ContainerTypeID;
                //    objCVarOperationContainersAndPackages.PackageTypeID = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].PackageTypeID;
                //    objCVarOperationContainersAndPackages.Length = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].Length;
                //    objCVarOperationContainersAndPackages.Width = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].Width;
                //    objCVarOperationContainersAndPackages.Height = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].Height;
                //    objCVarOperationContainersAndPackages.Volume = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].Volume;
                //    objCVarOperationContainersAndPackages.VolumetricWeight = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].VolumetricWeight;
                //    objCVarOperationContainersAndPackages.NetWeight = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].NetWeight;
                //    objCVarOperationContainersAndPackages.GrossWeight = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].GrossWeight;
                //    objCVarOperationContainersAndPackages.ChargeableWeight = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].ChargeableWeight;
                //    objCVarOperationContainersAndPackages.Quantity = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].Quantity;

                //    objCVarOperationContainersAndPackages.ContainerNumber = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].ContainerNumber;
                //    objCVarOperationContainersAndPackages.CarrierSeal = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].CarrierSeal;
                //    objCVarOperationContainersAndPackages.ShipperSeal = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].ShipperSeal;
                //    objCVarOperationContainersAndPackages.TareWeight = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].TareWeight;
                //    objCVarOperationContainersAndPackages.VGM = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].VGM;
                //    objCVarOperationContainersAndPackages.IsReefer = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].IsReefer;
                //    objCVarOperationContainersAndPackages.IsNOR = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].IsNOR;
                //    objCVarOperationContainersAndPackages.IsSentToWarehouse = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].IsSentToWarehouse;
                //    objCVarOperationContainersAndPackages.IsLoaded = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].IsLoaded;
                //    objCVarOperationContainersAndPackages.MinTemp = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].MinTemp;
                //    objCVarOperationContainersAndPackages.MaxTemp = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].MaxTemp;
                //    objCVarOperationContainersAndPackages.Humidity = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].Humidity;
                //    objCVarOperationContainersAndPackages.Ventilation = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].Ventilation;
                //    objCVarOperationContainersAndPackages.LotNumber = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].LotNumber;
                //    objCVarOperationContainersAndPackages.IsIMO = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].IsIMO;
                //    objCVarOperationContainersAndPackages.IMOClass = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].IMOClass;
                //    objCVarOperationContainersAndPackages.UNNumber = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].UNNumber;
                //    objCVarOperationContainersAndPackages.FlashPoint = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].FlashPoint;
                //    objCVarOperationContainersAndPackages.DescriptionOfGoods = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].DescriptionOfGoods;
                //    objCVarOperationContainersAndPackages.MarksAndNumbers = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].MarksAndNumbers;
                //    objCVarOperationContainersAndPackages.PackageTypeIDOnContainer = 0;
                //    objCVarOperationContainersAndPackages.NumberOfPackagesOnContainer = objCOperationContainersAndPackagesToCopy.lstCVarOperationContainersAndPackages[i].NumberOfPackagesOnContainer;

                //    objCVarOperationContainersAndPackages.CreatorUserID = objCVarOperationContainersAndPackages.ModificatorUserID = WebSecurity.CurrentUserId;
                //    objCVarOperationContainersAndPackages.CreationDate = objCVarOperationContainersAndPackages.ModificationDate = DateTime.Now;
                //    objCOperationContainersAndPackages.lstCVarOperationContainersAndPackages.Add(objCVarOperationContainersAndPackages);
                //}
                //objCOperationContainersAndPackages.SaveMethod(objCOperationContainersAndPackages.lstCVarOperationContainersAndPackages);
                #endregion Copy OperationContainersAndPackages

                #region Copy Payables if requested
                if (pIncludePayables)
                {
                    CPayables objCPayablesToCopy = new CPayables();
                    objCPayablesToCopy.GetListPaging(999999, 1, " WHERE OperationID = " + pShipmentToCopyID.ToString()
                        + " AND IsDeleted=0 AND OperationContainersAndPackagesID IS NULL"
                        , "ID", out _TempRowCount); //Don't get containerTracking Payables&Receivables

                    CPayables objCPayables = new CPayables();
                    for (int i = 0; i < objCPayablesToCopy.lstCVarPayables.Count; i++)
                    {
                        pWhereClauseCurrencyDetails = "WHERE ID=" + objCPayablesToCopy.lstCVarPayables[i].CurrencyID
                            + " AND GETDATE() >= DateAdd(hh, 0, DateDiff(dd, 0, FromDate)) "
                            + " AND GETDATE() <= DateAdd(minute,59,DateAdd(hh, 23, DateDiff(dd, 0, ToDate))) "
                            + " ORDER BY CODE";
                        objCvwCurrencyDetails.GetList(pWhereClauseCurrencyDetails);
                        if (objCvwCurrencyDetails.lstCVarvwCurrencyDetails.Count > 0)
                        {
                            CVarPayables objCVarPayables = new CVarPayables();
                            objCVarPayables.OperationID = objCOperations.lstCVarOperations[0].ID;
                            objCVarPayables.ChargeTypeID = objCPayablesToCopy.lstCVarPayables[i].ChargeTypeID;
                            objCVarPayables.POrC = objCPayablesToCopy.lstCVarPayables[i].POrC;
                            objCVarPayables.SupplierOperationPartnerID = objCPayablesToCopy.lstCVarPayables[i].SupplierOperationPartnerID;
                            objCVarPayables.ContainerTypeID = objCPayablesToCopy.lstCVarPayables[i].ContainerTypeID;
                            objCVarPayables.MeasurementID = objCPayablesToCopy.lstCVarPayables[i].MeasurementID;
                            objCVarPayables.Quantity = objCPayablesToCopy.lstCVarPayables[i].Quantity;
                            objCVarPayables.CostPrice = objCPayablesToCopy.lstCVarPayables[i].CostPrice;
                            objCVarPayables.CostAmount = objCPayablesToCopy.lstCVarPayables[i].CostAmount;
                            objCVarPayables.AmountWithoutVAT = objCPayablesToCopy.lstCVarPayables[i].AmountWithoutVAT;
                            objCVarPayables.InitialSalePrice = objCPayablesToCopy.lstCVarPayables[i].InitialSalePrice;
                            objCVarPayables.SupplierInvoiceNo = objCPayablesToCopy.lstCVarPayables[i].SupplierInvoiceNo;
                            objCVarPayables.SupplierReceiptNo = objCPayablesToCopy.lstCVarPayables[i].SupplierReceiptNo;
                            objCVarPayables.EntryDate = objCPayablesToCopy.lstCVarPayables[i].EntryDate;
                            objCVarPayables.BillID = 0;

                            objCVarPayables.IssueDate = objCPayablesToCopy.lstCVarPayables[i].IssueDate;
                            objCVarPayables.OperationContainersAndPackagesID = 0;

                            objCVarPayables.ExchangeRate = objCvwCurrencyDetails.lstCVarvwCurrencyDetails[0].ExchangeRate; //objCPayablesToCopy.lstCVarPayables[i].ExchangeRate;
                            objCVarPayables.CurrencyID = objCPayablesToCopy.lstCVarPayables[i].CurrencyID;
                            objCVarPayables.Notes = objCPayablesToCopy.lstCVarPayables[i].Notes;
                            objCVarPayables.CreatorUserID = objCVarPayables.ModificatorUserID = WebSecurity.CurrentUserId;
                            objCVarPayables.CreationDate = objCVarPayables.ModificationDate = DateTime.Now;
                            objCPayables.lstCVarPayables.Add(objCVarPayables);
                        } //if (objCvwCurrencyDetails.lstCVarvwCurrencyDetails.Count > 0)
                    } //for (int i = 0; i < objCPayablesToCopy.lstCVarPayables.Count; i++)
                    objCPayables.SaveMethod(objCPayables.lstCVarPayables);
                } //if (pIncludePayables)
                #endregion Copy Payables if requested

                #region Copy Receivables if requested
                if (pIncludeReceivables)
                {
                    CReceivables objCReceivablesToCopy = new CReceivables();
                    objCReceivablesToCopy.GetListPaging(999999, 1, " WHERE OperationID = " + pShipmentToCopyID.ToString()
                        + " AND IsDeleted=0 AND OperationContainersAndPackagesID IS NULL ", "ID", out _TempRowCount);

                    CReceivables objCReceivables = new CReceivables();
                    for (int i = 0; i < objCReceivablesToCopy.lstCVarReceivables.Count; i++)
                    {
                        pWhereClauseCurrencyDetails = "WHERE ID=" + objCReceivablesToCopy.lstCVarReceivables[i].CurrencyID
                            + " AND GETDATE() >= DateAdd(hh, 0, DateDiff(dd, 0, FromDate)) "
                            + " AND GETDATE() <= DateAdd(minute,59,DateAdd(hh, 23, DateDiff(dd, 0, ToDate))) "
                            + " ORDER BY CODE";
                        objCvwCurrencyDetails.GetList(pWhereClauseCurrencyDetails);
                        if (objCvwCurrencyDetails.lstCVarvwCurrencyDetails.Count > 0)
                        {
                            CVarReceivables objCVarReceivables = new CVarReceivables();
                            objCVarReceivables.OperationID = objCOperations.lstCVarOperations[0].ID;
                            objCVarReceivables.ChargeTypeID = objCReceivablesToCopy.lstCVarReceivables[i].ChargeTypeID;
                            objCVarReceivables.POrC = objCReceivablesToCopy.lstCVarReceivables[i].POrC;
                            objCVarReceivables.SupplierID = objCReceivablesToCopy.lstCVarReceivables[i].SupplierID;
                            objCVarReceivables.MeasurementID = objCReceivablesToCopy.lstCVarReceivables[i].MeasurementID;

                            objCVarReceivables.IssueDate = DateTime.Now;
                            objCVarReceivables.OperationContainersAndPackagesID = 0;

                            objCVarReceivables.ContainerTypeID = objCReceivablesToCopy.lstCVarReceivables[i].ContainerTypeID;
                            objCVarReceivables.PackageTypeID = objCReceivablesToCopy.lstCVarReceivables[i].PackageTypeID;
                            objCVarReceivables.Quantity = objCReceivablesToCopy.lstCVarReceivables[i].Quantity == 0 ? 1 : objCReceivablesToCopy.lstCVarReceivables[i].Quantity;
                            objCVarReceivables.CostPrice = objCReceivablesToCopy.lstCVarReceivables[i].CostPrice;
                            objCVarReceivables.CostAmount = objCReceivablesToCopy.lstCVarReceivables[i].CostAmount;
                            objCVarReceivables.SalePrice = objCReceivablesToCopy.lstCVarReceivables[i].SalePrice;
                            objCVarReceivables.AmountWithoutVAT = Math.Round((objCVarReceivables.Quantity * objCVarReceivables.SalePrice), 2);
                            objCVarReceivables.SaleAmount = objCReceivablesToCopy.lstCVarReceivables[i].SaleAmount;
                            objCVarReceivables.TaxeTypeID = objCReceivablesToCopy.lstCVarReceivables[i].TaxeTypeID;
                            objCVarReceivables.ExchangeRate = objCvwCurrencyDetails.lstCVarvwCurrencyDetails[0].ExchangeRate; //objCReceivablesToCopy.lstCVarReceivables[i].ExchangeRate;
                            objCVarReceivables.CurrencyID = objCReceivablesToCopy.lstCVarReceivables[i].CurrencyID;
                            objCVarReceivables.Notes = objCReceivablesToCopy.lstCVarReceivables[i].Notes;
                            objCVarReceivables.ViewOrder = objCReceivablesToCopy.lstCVarReceivables[i].ViewOrder;
                            objCVarReceivables.IsDeleted = false;

                            objCVarReceivables.PreviousCutOffDate = DateTime.Parse("01/01/1900");
                            objCVarReceivables.CutOffDate = DateTime.Parse("01/01/1900");
                            objCVarReceivables.ReceiptDate = DateTime.Parse("01/01/1900");
                            objCVarReceivables.ReceiptNo = "";

                            objCVarReceivables.CreatorUserID = objCVarReceivables.ModificatorUserID = WebSecurity.CurrentUserId;
                            objCVarReceivables.CreationDate = objCVarReceivables.ModificationDate = DateTime.Now;
                            objCReceivables.lstCVarReceivables.Add(objCVarReceivables);
                        } //if (objCvwCurrencyDetails.lstCVarvwCurrencyDetails.Count > 0)
                    } //for (int i = 0; i < objCReceivablesToCopy.lstCVarReceivables.Count; i++)
                    objCReceivables.SaveMethod(objCReceivables.lstCVarReceivables);
                } //if (pIncludeReceivables)
                #endregion Copy Receivables if requested
            }
            //Operations_EmailNotification(objCOperations.lstCVarOperations[0].ID);
            #region Returned Data
            CvwOperationsWithMinimalColumns objCvwShipment = new CvwOperationsWithMinimalColumns();
            checkException = objCvwShipment.GetListPaging(99999, 1, "WHERE MasterOperationID=" + objCOperationToCopy.lstCVarOperations[0].MasterOperationID, "ID DESC", out _RowCount);
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            #endregion Returned Data
            return new object[] {
                pReturnedMessage
                , serializer.Serialize(objCvwShipment.lstCVarvwOperationsWithMinimalColumns)
            };
        }

        [HttpGet, HttpPost]
        public object[] Shipment_SetCertificateNumber([FromBody] SetCertificateNumberData setCertificateNumberData)
        {
            string _ReturnedMessage = "";
            Exception checkException = null;
            COperations objCOperations = new COperations();
            string pUpdateList = "";
            var _ArrOperationIDList = setCertificateNumberData.pOperationIDList.Split(',');
            var _ArrCertificateNumberList = setCertificateNumberData.pCertificateNumberList.Split(',');
            var _ArrCountryOfOriginList = setCertificateNumberData.pCountryOfOriginList.Split(',');
            var _ArrInvoiceValueList = setCertificateNumberData.pInvoiceValueList.Split(',');
            var _ArrCommodityIDList = setCertificateNumberData.pCommodityIDList.Split(',');
            var _ArrCurrencyIDList = setCertificateNumberData.pCurrencyIDList.Split(',');
            int _NumberOfRecords = _ArrOperationIDList.Length;
            for (int i = 1; i < _NumberOfRecords; i++)
            {
                pUpdateList = "CertificateNumber=" + (_ArrCertificateNumberList[i] == "0" ? "NULL" : ("N'" + _ArrCertificateNumberList[i] + "'")) + " \n";
                pUpdateList += ",CountryOfOrigin=" + (_ArrCountryOfOriginList[i] == "0" ? "NULL" : ("N'" + _ArrCountryOfOriginList[i] + "'")) + " \n";
                pUpdateList += ",InvoiceValue=" + (_ArrInvoiceValueList[i] == "0" ? "NULL" : ("N'" + _ArrInvoiceValueList[i] + "'")) + " \n";
                pUpdateList += ",CommodityID=" + (_ArrCommodityIDList[i] == "0" ? "NULL" : ("N'" + _ArrCommodityIDList[i] + "'")) + " \n";
                pUpdateList += ",CurrencyID=" + (_ArrCurrencyIDList[i] == "0" ? "NULL" : ("N'" + _ArrCurrencyIDList[i] + "'")) + " \n";
                pUpdateList += " WHERE ID=" + _ArrOperationIDList[i] + " \n";
                checkException = objCOperations.UpdateList(pUpdateList);
            }
            if (checkException != null)
                _ReturnedMessage = checkException.Message;
            return new object[]
            {
                _ReturnedMessage
            };
        }

        [HttpGet, HttpPost]
        public object[] ShipmentDates_SaveList([FromBody] SetShipmentDatesData setShipmentDatesData)
        {
            string _ReturnedMessage = "";
            Exception checkException = null;
            CRoutings objCRoutings = new CRoutings();
            COperations objCOperations = new COperations();
            string pUpdateList = "";
            var _ArrOperationIDList = setShipmentDatesData.pOperationIDList.Split(',');
            var _ArrExpectedDepartureList = setShipmentDatesData.pExpectedDepartureList.Split(',');
            var _ArrActualDepartureList = setShipmentDatesData.pActualDepartureList.Split(',');
            var _ArrExpectedArrivalList = setShipmentDatesData.pExpectedArrivalList.Split(',');
            var _ArrActualArrivalList = setShipmentDatesData.pActualArrivalList.Split(',');
            var _ArrIsClearanceList = setShipmentDatesData.pIsClearanceList.Split(',');
            int _NumberOfRecords = _ArrOperationIDList.Length;
            for (int i = 0; i < _NumberOfRecords; i++)
            {
                pUpdateList = " ExpectedDeparture='" + (_ArrExpectedDepartureList[i]) + "' \n";
                pUpdateList += " , ActualDeparture='" + (_ArrActualDepartureList[i]) + "' \n";
                pUpdateList += " , ExpectedArrival='" + (_ArrExpectedArrivalList[i]) + "' \n";
                pUpdateList += " , ActualArrival='" + (_ArrActualArrivalList[i]) + "' \n";
                pUpdateList += " , IsClearance='" + (_ArrIsClearanceList[i]) + "' \n";
                pUpdateList += " WHERE OperationID=" + _ArrOperationIDList[i] + " AND RoutingTypeID = 30 \n";
                checkException = objCRoutings.UpdateList(pUpdateList);

                pUpdateList = " IsClearance=" + (_ArrIsClearanceList[i] == "true" ? "1" : "0") + " \n";
                pUpdateList += " WHERE ID=" + _ArrOperationIDList[i] + " \n";
                checkException = objCOperations.UpdateList(pUpdateList);

            }


            if (checkException != null)
                _ReturnedMessage = checkException.Message;
            return new object[]
            {
                _ReturnedMessage
            };
        }


        [HttpGet, HttpPost]
        public object[] Certificate_GetCertificateHousesAndGrossWeight(Int64 pOperationIDToGetCertificates)
        {
            string pReturnedMessage = "";
            Exception checkException = null;
            int _RowCount = 0;
            CvwOperations objCvwOperations = new CvwOperations();
            checkException = objCvwOperations.GetListPaging(999999, 1, "WHERE MasterOperationID=" + pOperationIDToGetCertificates, "ID", out _RowCount);
            var pDistinctCertificates = objCvwOperations.lstCVarvwOperations
                .GroupBy(g => new { g.CertificateNumber })
                .Select(s => new
                {
                    CertificateNumber = s.First().CertificateNumber
                    ,
                    CertificateWeight = s.Sum(g => g.GrossWeightSum)
                })
                .ToList();
            var pHouses = objCvwOperations.lstCVarvwOperations
                //.GroupBy(g => new { g.CertificateNumber })
                .Select(s => new
                {
                    CertificateNumber = s.CertificateNumber
                    ,
                    HouseNumber = s.HouseNumber
                })
                .ToList();
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[]
            {
                pReturnedMessage
                , serializer.Serialize(pDistinctCertificates) //pData[1]
                , serializer.Serialize(pHouses) //pData[2]
            };
        }

        [HttpGet, HttpPost]
        public object[] FillShipmentControls(Int64 pMasterOperationID, Int64 pShipmentID)
        {
            Exception checkException = null;
            int _RowCount = 0;
            int pNotifyID = 0;
            int constNotify1OperationPartnerTypeID = 4;
            int MainCarraigeRoutingTypeID = 30;
            Int64 pConnectedToOperationID = 0; //if (new shipment or connected shipment) then =pMasterOperation else 0

            CDefaults objCDefaults = new CDefaults();
            objCDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount);
            int pDefaultCountryID = objCDefaults.lstCVarDefaults[0].DefaultCountryID;
            CCountries objCCountries = new CCountries();
            checkException = objCCountries.GetListPaging(999999, 1, "WHERE 1=1", "Name", out _RowCount);
            CIncoterms objCIncoterms = new CIncoterms();
            checkException = objCIncoterms.GetListPaging(999999, 1, "WHERE 1=1", "Name", out _RowCount);
            CMoveTypes objCMoveTypes = new CMoveTypes();
            checkException = objCMoveTypes.GetListPaging(999999, 1, "WHERE 1=1", "Name", out _RowCount);
            CNoAccessFreightTypes objCNoAccessFreightTypes = new CNoAccessFreightTypes();
            checkException = objCNoAccessFreightTypes.GetListPaging(999999, 1, "WHERE 1=1", "Name", out _RowCount);
            CvwPayables objCvwPayables = new CvwPayables();
            CvwMAWBStock objMbStck = new CvwMAWBStock();

            CvwOperations objCvwOperationsMaster = new CvwOperations();
            objCvwOperationsMaster.GetListPaging(1, 1, "WHERE ID=" + pMasterOperationID.ToString(), "ID", out _RowCount);
            bool pIsAWB = false;
            if (pMasterOperationID != 0)
            {
                pIsAWB = objCvwOperationsMaster.lstCVarvwOperations[0].IsAWB;
            }

            CvwOperations objCvwOperations = new CvwOperations();
            if (pShipmentID > 0) //this means update(already exists)
                checkException = objCvwOperations.GetListPaging(1, 1, "WHERE ID=" + pShipmentID.ToString(), "ID", out _RowCount);
            if (objCvwOperations.lstCVarvwOperations.Count > 0)
                pConnectedToOperationID = objCvwOperations.lstCVarvwOperations[0].MasterOperationID; //of not connected then 0
            else //new Shipment to Connected direclty
                pConnectedToOperationID = pMasterOperationID;

            CvwCustomersWithMinimalColumns objCvwCustomersWithMinimalColumns = new CvwCustomersWithMinimalColumns();
            //checkException = objCvwCustomersWithMinimalColumns.GetListPaging(10000, 1, "WHERE 1=1", "Name", out _RowCount);

            CvwAgentsForCombo objCAgents = new CvwAgentsForCombo();
            checkException = objCAgents.GetListPaging(99999, 1, "WHERE 1=1", "Name", out _RowCount);

            CvwOperationContainersAndPackages objCvwOperationContainersAndPackages = new CvwOperationContainersAndPackages();
            objCvwOperationContainersAndPackages.GetListPaging(10000, 1, "WHERE OperationID=" + pShipmentID.ToString(), "PackageTypeName", out _RowCount);

            CPackageTypes objCPackageTypes = new CPackageTypes();
            objCPackageTypes.GetList("ORDER BY Name");

            CPorts objCPortOfDestination = new CPorts(); //Port of destination
            CPorts objCPortOfPickup = new CPorts(); //Port of destination
            if (pShipmentID == 0)
            {
                if (pMasterOperationID != 0)
                {
                    objCPortOfDestination.GetList("WHERE CountryID=" + objCvwOperationsMaster.lstCVarvwOperations[0].PODCountryID
                    + (pIsAWB ? " AND IsAir=1 " : ""));
                    objCPortOfPickup.GetList("WHERE CountryID=" + objCvwOperationsMaster.lstCVarvwOperations[0].POLCountryID
                        + (pIsAWB ? " AND IsAir=1 " : ""));
                }
                else
                {
                    objCPortOfDestination.GetList("WHERE 1=1"
                        + (pIsAWB ? " AND IsAir=1 " : ""));
                    objCPortOfPickup.GetList("WHERE 1=1"
                        + (pIsAWB ? " AND IsAir=1 " : ""));
                }

            }
            else if (pShipmentID > 0)
            {
                objCPortOfDestination.GetList("WHERE CountryID=" + (objCvwOperations.lstCVarvwOperations[0].DeliveryCountryID == 0 ? objCvwOperations.lstCVarvwOperations[0].PODCountryID : objCvwOperations.lstCVarvwOperations[0].DeliveryCountryID)
                    + (pIsAWB ? " AND IsAir=1 " : ""));
                objCPortOfPickup.GetList("WHERE CountryID=" + (objCvwOperations.lstCVarvwOperations[0].PickupCountryID == 0 ? objCvwOperations.lstCVarvwOperations[0].POLCountryID : objCvwOperations.lstCVarvwOperations[0].PickupCountryID)
                    + (pIsAWB ? " AND IsAir=1 " : ""));
            }
            else if (pIsAWB && pDefaultCountryID != 0) //new AWB house
                objCPortOfDestination.GetList("WHERE CountryID=" + pDefaultCountryID.ToString() + " AND IsAir=1 ");

            CvwOperationContainersAndPackages objContainers = new CvwOperationContainersAndPackages();//get containers only if its connected to the master
            //objContainers.GetListPaging(1000, 1, "WHERE OperationID=" + pConnectedToOperationID, "ContainerTypeCode, ContainerNumber", out _RowCount);
            objContainers.GetListPaging(1000, 1, "WHERE OperationID=" + pConnectedToOperationID, "ID", out _RowCount);

            COperationPartners objCOperationPartners = new COperationPartners();
            objCOperationPartners.GetList("WHERE OperationID=" + pShipmentID + " AND OperationPartnerTypeID=" + constNotify1OperationPartnerTypeID);
            if (objCOperationPartners.lstCVarOperationPartners.Count > 0)
                pNotifyID = objCOperationPartners.lstCVarOperationPartners[0].CustomerID;

            CRoutings objCRoutings = new CRoutings();
            objCRoutings.GetListPaging(1000, 1, "WHERE OperationID=" + pShipmentID + " AND RoutingTypeID=" + MainCarraigeRoutingTypeID, "ID", out _RowCount);

            CAirlines objCAirline = new CAirlines(); //Port of destination
            checkException = objCAirline.GetListPaging(100000, 1, "WHERE 1=1 ", "Name", out _RowCount);

            CVessels objCVessels = new CVessels();
            objCVessels.GetList("Where 1=1");
            if (pMasterOperationID != 0)
            {
                if (objCvwOperationsMaster.lstCVarvwOperations[0].TransportType == 2)
                { //(pIsAWB) {
                    objCvwPayables.GetListPaging(999999, 1, " WHERE OperationID = " + pShipmentID.ToString() /* AND IsDeleted = 0 */, "ID"/*"ChargeTypeName"*/, out _RowCount);
                    if (objCvwOperations.lstCVarvwOperations.Count > 0)
                    {
                        objMbStck.GetList(" where id=" + objCvwOperations.lstCVarvwOperations[0].MAWBStockID);
                    }
                }
            }

            #region Minimize
            var pCountryList = objCCountries.lstCVarCountries
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                })
                .ToList();
            #endregion Minimize

            int CancelledQuoteAndOperStageID = 110;
            bool pIsOperationClosed = false;
            if (objCvwOperations.lstCVarvwOperations.Count > 0)
            {
                pIsOperationClosed = (objCvwOperations.lstCVarvwOperations[0].CloseDate < DateTime.Now && objCvwOperations.lstCVarvwOperations[0].OperationStageID != CancelledQuoteAndOperStageID);//2nd condition coz with time closedate passes but the operation is still cancelled
            }


            CUsers objCUsers = new CUsers();
            var CurrentUserID = WebSecurity.CurrentUserId;
            if (objCDefaults.lstCVarDefaults[0].ShowUserSalesmen == true)
                objCUsers.GetList(" Where IsNull(CustomerID , 0) = 0 AND IsNull( (SELECT COUNT(us.ID) FROM dbo.UserSalesmen AS us WHERE us.UserID = " + CurrentUserID + " AND us.SalesManID = dbo.Users.ID  ) , 0 ) > 0 ORDER BY Name");
            else
                objCUsers.GetList(" where IsNull(CustomerID , 0) = 0 AND 1 = 1 ORDER BY Name");

            var pUserList = objCUsers.lstCVarUsers.Select(s => new
            {
                ID = s.ID
                                ,
                Name = s.Name
                                ,
                IsSalesman = s.IsSalesman
            }).ToList();



            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                pShipmentID > 0 ? serializer.Serialize(objCvwOperations.lstCVarvwOperations[0]) : null //pData[0]
                , serializer.Serialize(objCvwCustomersWithMinimalColumns.lstCVarvwCustomersWithMinimalColumns) //pData[1]
                , serializer.Serialize(objCAgents.lstCVarvwAgentsForCombo) //pData[2]
                , serializer.Serialize(objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages) //pData[3]
                , serializer.Serialize(objCPackageTypes.lstCVarPackageTypes) //pData[4]
                , serializer.Serialize(objCPortOfDestination.lstCVarPorts) //pData[5]
                , serializer.Serialize(objContainers.lstCVarvwOperationContainersAndPackages) //pData[6]
                , pNotifyID //pData[7]
                , objCRoutings.lstCVarRoutings.Count > 0 ? serializer.Serialize(objCRoutings.lstCVarRoutings[0]) : null  //pData[7]
                , serializer.Serialize(objCAirline.lstCVarAirlines) //pData[9]
                , pShipmentID > 0 ? serializer.Serialize(objCvwPayables.lstCVarvwPayables) : null //pData[10]
                , serializer.Serialize(objMbStck.lstCVarvwMAWBStock) //pData[11]
                , serializer.Serialize(objCPortOfPickup.lstCVarPorts) //pData[12]
                , serializer.Serialize(pCountryList) //pData[13]
                , serializer.Serialize(objCIncoterms.lstCVarIncoterms) //pData[14]
                , serializer.Serialize(objCMoveTypes.lstCVarMoveTypes) //pData[15]
                , serializer.Serialize(objCNoAccessFreightTypes.lstCVarNoAccessFreightTypes) //pData[16]
                , pIsOperationClosed //pData[17]
                , serializer.Serialize(objCVessels.lstCVarVessels) //pData[18]
                , serializer.Serialize(pUserList) //pData[19]
            };
        }

        //to make sure that House and master are suitable (i.e. not changed in another session)
        [HttpGet, HttpPost]
        public bool CheckIsHouseSuitableForMaster(Int64 pMasterOperationID, Int64 pHouseOperationID)
        {
            bool _result = true;
            COperations objCMasterOperation = new COperations();
            COperations objCHouseOperation = new COperations();
            int _RowCount = 0;
            objCMasterOperation.GetListPaging(1, 1, " WHERE ID = " + pMasterOperationID, "ID", out _RowCount);
            objCHouseOperation.GetListPaging(1, 1, " WHERE ID = " + pHouseOperationID, "ID", out _RowCount);
            if (
                    (
                        objCMasterOperation.lstCVarOperations[0].POL != objCHouseOperation.lstCVarOperations[0].POL
                        || objCMasterOperation.lstCVarOperations[0].POD != objCHouseOperation.lstCVarOperations[0].POD
                        || (objCHouseOperation.lstCVarOperations[0].MasterOperationID != pMasterOperationID && objCHouseOperation.lstCVarOperations[0].MasterOperationID != 0)
                    )
                    && (!objCHouseOperation.lstCVarOperations[0].IsAWB && !objCMasterOperation.lstCVarOperations[0].IsAWB) //coz in AWB houses could be different
                )
                _result = false;

            return _result;
        }


        #region Static function for sending email notifictions
        [HttpGet]
        public static void Operations_EmailNotification(Int64 pOperationID)
        {
            int _RowCount = 0;
            string _MessageReturned = "";
            CvwDefaults objCvwDefaults = new CvwDefaults();
            objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa
            CvwOperations objCvwOperations = new CvwOperations();
            //CvwRoutings objCvwRoutings_Main = new CvwRoutings();
            CRoutings objCRoutings_TruckingOrder = new CRoutings();
            CRoutings objCRoutings_CustomsClearance = new CRoutings();
            CvwServiceDepartment objCvwServiceDepartment = new CvwServiceDepartment();
            CvwOperationEmailSent objCvwOperationEmailSent = new CvwOperationEmailSent();
            COperationEmailSent objCOperationEmailSent = new COperationEmailSent();
            string _WhereClause = "";
            Exception checkException = null;

            if (objCvwDefaults.lstCVarvwDefaults[0].IsDepartmentOption && 1 == 2)
            {
                checkException = objCvwOperations.GetListPaging(1, 1, "WHERE ID=" + pOperationID, "ID", out _RowCount);
                //checkException = objCvwRoutings_Main.GetListPaging(999999, 1, "WHERE OperationID=" + pOperationID + " AND RoutingTypeID=30", "ID", out _RowCount);
                checkException = objCRoutings_TruckingOrder.GetListPaging(1, 1, "WHERE OperationID=" + pOperationID + " AND RoutingTypeID=60", "ID DESC", out _RowCount);
                checkException = objCRoutings_CustomsClearance.GetListPaging(1, 1, "WHERE OperationID=" + pOperationID + " AND RoutingTypeID=70", "ID DESC", out _RowCount);
                #region Delete this region
                //for (int i = 0; i < objCvwServiceDepartment.lstCVarvwServiceDepartment.Count; i++)
                //{
                //    _DepartmentIDInsertList += "," + objCvwServiceDepartment.lstCVarvwServiceDepartment[i].DepartmentID;
                //}
                #endregion Delete this region
                #region Insert into OperationEmailSent Departments which need to receive notifications and has email
                _WhereClause = "WHERE MoveTypeID=" + objCvwOperations.lstCVarvwOperations[0].MoveTypeID + "\n";
                _WhereClause += "AND (OpenDate=1 OR CloseDate=1 OR CutOffDate=1 OR PODate=1 OR ReleaseDate=1 OR ETAPOLDate=1 OR ATAPOLDate=1 OR ExpectedDeparture=1 OR ActualDeparture=1 OR ExpectedArrival=1 OR ActualArrival=1 OR GateInDate=1 OR GateOutDate=1 OR StuffingDate=1 OR DeliveryDate=1 OR CertificateDate=1 OR QasimaDate=1)" + "\n";
                _WhereClause += "AND Email <> '' AND Email IS NOT NULL" + " \n"; //To get only departments with email addresses saved.
                _WhereClause += "AND DepartmentID NOT IN(SELECT DepartmentID FROM OperationEmailSent WHERE OperationID=" + pOperationID + ")" + "\n"; //To get first the rows need to be inserted in the OperationEmailSent table
                checkException = objCvwServiceDepartment.GetListPaging(999999, 1, _WhereClause, "ID", out _RowCount);
                for (int i = 0; i < objCvwServiceDepartment.lstCVarvwServiceDepartment.Count; i++)
                {
                    CVarOperationEmailSent objCVarOperationEmailSent = new CVarOperationEmailSent();
                    objCVarOperationEmailSent.DepartmentID = objCvwServiceDepartment.lstCVarvwServiceDepartment[i].DepartmentID;
                    objCVarOperationEmailSent.OperationID = pOperationID;
                    objCVarOperationEmailSent.OpenDate = DateTime.Parse("01-01-1900");
                    objCVarOperationEmailSent.CloseDate = DateTime.Parse("01-01-1900");
                    objCVarOperationEmailSent.CutOffDate = DateTime.Parse("01-01-1900");
                    objCVarOperationEmailSent.PODate = DateTime.Parse("01-01-1900");
                    objCVarOperationEmailSent.ReleaseDate = DateTime.Parse("01-01-1900");
                    objCVarOperationEmailSent.ETAPOLDate = DateTime.Parse("01-01-1900");
                    objCVarOperationEmailSent.ATAPOLDate = DateTime.Parse("01-01-1900");
                    objCVarOperationEmailSent.ExpectedDeparture = DateTime.Parse("01-01-1900");
                    objCVarOperationEmailSent.ActualDeparture = DateTime.Parse("01-01-1900");
                    objCVarOperationEmailSent.ExpectedArrival = DateTime.Parse("01-01-1900");
                    objCVarOperationEmailSent.ActualArrival = DateTime.Parse("01-01-1900");
                    objCVarOperationEmailSent.GateInDate = DateTime.Parse("01-01-1900");
                    objCVarOperationEmailSent.GateOutDate = DateTime.Parse("01-01-1900");
                    objCVarOperationEmailSent.StuffingDate = DateTime.Parse("01-01-1900");
                    objCVarOperationEmailSent.DeliveryDate = DateTime.Parse("01-01-1900");
                    objCVarOperationEmailSent.CertificateDate = DateTime.Parse("01-01-1900");
                    objCVarOperationEmailSent.QasimaDate = DateTime.Parse("01-01-1900");

                    objCOperationEmailSent.lstCVarOperationEmailSent.Add(objCVarOperationEmailSent);
                }
                checkException = objCOperationEmailSent.SaveMethod(objCOperationEmailSent.lstCVarOperationEmailSent);
                #endregion Insert into OperationEmailSent Departments which need to receive notifications and has email
                #region Get Receiving Departments
                //At this point I am sure that departments for that opertion are all inserted in OperationEmailSent
                _WhereClause = "WHERE OperationID=" + pOperationID + "\n";
                _WhereClause += "AND (" + "\n";
                _WhereClause += "   (IsOpenDate=1 AND OpenDate<>CurrentOpenDate)" + "\n";
                _WhereClause += "   OR (IsCloseDate=1 AND CloseDate<>CurrentCloseDate)" + "\n";
                _WhereClause += "   OR (IsCutOffDate=1 AND CutOffDate<>CurrentCutOffDate)" + "\n";
                _WhereClause += "   OR (IsPODate=1 AND PODate<>CurrentPODate)" + "\n";
                _WhereClause += "   OR (IsReleaseDate=1 AND ReleaseDate<>CurrentReleaseDate)" + "\n";
                _WhereClause += "   OR (IsETAPOLDate=1 AND ETAPOLDate<>CurrentETAPOLDate)" + "\n";
                _WhereClause += "   OR (IsATAPOLDate=1 AND ATAPOLDate<>CurrentATAPOLDate)" + "\n";
                _WhereClause += "   OR (IsExpectedDeparture=1 AND ExpectedDeparture<>CurrentExpectedDeparture)" + "\n";
                _WhereClause += "   OR (IsActualDeparture=1 AND ActualDeparture<>CurrentActualDeparture)" + "\n";
                _WhereClause += "   OR (IsExpectedArrival=1 AND ExpectedArrival<>CurrentExpectedArrival)" + "\n";
                _WhereClause += "   OR (IsActualArrival=1 AND ActualArrival<>CurrentActualArrival)" + "\n";
                _WhereClause += "   OR (IsGateInDate=1 AND GateInDate<>CurrentGateInDate)" + "\n";
                _WhereClause += "   OR (IsGateOutDate=1 AND GateOutDate<>CurrentGateOutDate)" + "\n";
                _WhereClause += "   OR (IsStuffingDate=1 AND StuffingDate<>CurrentStuffingDate)" + "\n";
                _WhereClause += "   OR (IsDeliveryDate=1 AND DeliveryDate<>CurrentDeliveryDate)" + "\n";
                _WhereClause += "   OR (IsCertificateDate=1 AND CertificateDate<>CurrentCertificateDate)" + "\n";
                _WhereClause += "   OR (IsQasimaDate=1 AND QasimaDate<>CurrentQasimaDate)" + "\n";
                _WhereClause += "   )" + "\n";
                checkException = objCvwOperationEmailSent.GetListPaging(999999, 1, _WhereClause, "ID", out _RowCount);
                #endregion Get Receiving Departments
                #region Send Email
                bool _boolEmailFound = false;
                CUsers objCUser_Sender = new CUsers();
                checkException = objCUser_Sender.GetListPaging(999999, 1, "WHERE ID=" + WebSecurity.CurrentUserId, "ID", out _RowCount);
                //if (objCUser_Sender.lstCVarUsers[0].Email != "0" && objCUser_Sender.lstCVarUsers[0].Email != ""
                //        && objCUser_Sender.lstCVarUsers[0].Email_Password != "0" && objCUser_Sender.lstCVarUsers[0].Email_Password != ""
                //        && objCUser_Sender.lstCVarUsers[0].Email_DisplayName != "0" && objCUser_Sender.lstCVarUsers[0].Email_DisplayName != ""
                //        && objCUser_Sender.lstCVarUsers[0].Email_Host != "0" && objCUser_Sender.lstCVarUsers[0].Email_Host != ""
                //        && objCUser_Sender.lstCVarUsers[0].Email_Port != 0)
                if (objCvwDefaults.lstCVarvwDefaults[0].Email != "0" && objCvwDefaults.lstCVarvwDefaults[0].Email != ""
                        && objCvwDefaults.lstCVarvwDefaults[0].Email_Password != "0" && objCvwDefaults.lstCVarvwDefaults[0].Email_Password != ""
                        && objCvwDefaults.lstCVarvwDefaults[0].Email_DisplayName != "0" && objCvwDefaults.lstCVarvwDefaults[0].Email_DisplayName != ""
                        && objCvwDefaults.lstCVarvwDefaults[0].Email_Host != "0" && objCvwDefaults.lstCVarvwDefaults[0].Email_Host != ""
                        && objCvwDefaults.lstCVarvwDefaults[0].Email_Port != 0 && objCvwDefaults.lstCVarvwDefaults[0].IsDepartmentOption)
                {
                    string subject = "Operation '" + objCvwOperations.lstCVarvwOperations[0].EffectiveOperationCode;
                    string body = "<b>Sender : " + objCUser_Sender.lstCVarUsers[0].Email_DisplayName + "</b><br>";
                    body += "<b>Operation '" + objCvwOperations.lstCVarvwOperations[0].EffectiveOperationCode + "</b><br>";
                    body += "Client : " + objCvwOperations.lstCVarvwOperations[0].ClientName + "<br>";
                    body += "Service : " + objCvwOperations.lstCVarvwOperations[0].MoveTypeName + "<br>";
                    body += "Line : " + objCvwOperations.lstCVarvwOperations[0].LineName + "<br>";
                    body += "POL : " + objCvwOperations.lstCVarvwOperations[0].POLName + "<br>";
                    body += "POD : " + objCvwOperations.lstCVarvwOperations[0].PODName + "<br>";
                    body += "Open Date : " + (objCvwOperations.lstCVarvwOperations[0].OpenDate.ToShortDateString() == "1/1/1900" ? "Not Specified" : objCvwOperations.lstCVarvwOperations[0].OpenDate.ToLongDateString()) + "<br>";
                    body += "Close Date : " + (objCvwOperations.lstCVarvwOperations[0].CloseDate.ToShortDateString() == "1/1/1900" ? "Not Specified" : objCvwOperations.lstCVarvwOperations[0].CloseDate.ToLongDateString()) + "<br>";
                    body += "Cut-Off Date : " + (objCvwOperations.lstCVarvwOperations[0].CutOffDate.ToShortDateString() == "1/1/1900" ? "Not Specified" : objCvwOperations.lstCVarvwOperations[0].CutOffDate.ToLongDateString()) + "<br>";
                    body += "PO Date : " + (objCvwOperations.lstCVarvwOperations[0].PODate.ToShortDateString() == "1/1/1900" ? "Not Specified" : objCvwOperations.lstCVarvwOperations[0].PODate.ToLongDateString()) + "<br>";
                    body += "Release Date : " + (objCvwOperations.lstCVarvwOperations[0].ReleaseDate.ToShortDateString() == "1/1/1900" ? "Not Specified" : objCvwOperations.lstCVarvwOperations[0].ReleaseDate.ToLongDateString()) + "<br>";
                    body += "ETA POL Date : " + (objCvwOperations.lstCVarvwOperations[0].ETAPOLDate.ToShortDateString() == "1/1/1900" ? "Not Specified" : objCvwOperations.lstCVarvwOperations[0].ETAPOLDate.ToLongDateString()) + "<br>";
                    body += "ATA POL Date : " + (objCvwOperations.lstCVarvwOperations[0].ATAPOLDate.ToShortDateString() == "1/1/1900" ? "Not Specified" : objCvwOperations.lstCVarvwOperations[0].ATAPOLDate.ToLongDateString()) + "<br>";
                    body += "Expected Departure : " + (objCvwOperations.lstCVarvwOperations[0].ExpectedDeparture.ToShortDateString() == "1/1/1900" ? "Not Specified" : objCvwOperations.lstCVarvwOperations[0].ExpectedDeparture.ToLongDateString()) + "<br>";
                    body += "Actual Departure : " + (objCvwOperations.lstCVarvwOperations[0].ActualDeparture.ToShortDateString() == "1/1/1900" ? "Not Specified" : objCvwOperations.lstCVarvwOperations[0].ActualDeparture.ToLongDateString()) + "<br>";
                    body += "Expected Arrival : " + (objCvwOperations.lstCVarvwOperations[0].ExpectedArrival.ToShortDateString() == "1/1/1900" ? "Not Specified" : objCvwOperations.lstCVarvwOperations[0].ExpectedArrival.ToLongDateString()) + "<br>";
                    body += "Actual Arrival : " + (objCvwOperations.lstCVarvwOperations[0].ActualArrival.ToShortDateString() == "1/1/1900" ? "Not Specified" : objCvwOperations.lstCVarvwOperations[0].ActualArrival.ToLongDateString()) + "<br>";
                    if (objCRoutings_TruckingOrder.lstCVarRoutings.Count > 0)
                    {
                        body += "<br>Gate-In Date : " + (objCRoutings_TruckingOrder.lstCVarRoutings[0].GateInDate.ToShortDateString() == "1/1/1900" ? "Not Specified" : objCRoutings_TruckingOrder.lstCVarRoutings[0].GateInDate.ToLongDateString()) + "<br>";
                        body += "GateOut Date : " + (objCRoutings_TruckingOrder.lstCVarRoutings[0].GateOutDate.ToShortDateString() == "1/1/1900" ? "Not Specified" : objCRoutings_TruckingOrder.lstCVarRoutings[0].GateOutDate.ToLongDateString()) + "<br>";
                        body += "Stuffing Date : " + (objCRoutings_TruckingOrder.lstCVarRoutings[0].StuffingDate.ToShortDateString() == "1/1/1900" ? "Not Specified" : objCRoutings_TruckingOrder.lstCVarRoutings[0].StuffingDate.ToLongDateString()) + "<br>";
                        body += "Delivery Date : " + (objCRoutings_TruckingOrder.lstCVarRoutings[0].DeliveryDate.ToShortDateString() == "1/1/1900" ? "Not Specified" : objCRoutings_TruckingOrder.lstCVarRoutings[0].DeliveryDate.ToLongDateString()) + "<br>";
                    }
                    if (objCRoutings_CustomsClearance.lstCVarRoutings.Count > 0)
                    {
                        body += "<br>Certificate Date : " + (objCRoutings_CustomsClearance.lstCVarRoutings[0].CertificateDate.ToShortDateString() == "1/1/1900" ? "Not Specified" : objCRoutings_CustomsClearance.lstCVarRoutings[0].CertificateDate.ToLongDateString()) + "<br>";
                        body += "Qasima Date : " + (objCRoutings_CustomsClearance.lstCVarRoutings[0].QasimaDate.ToShortDateString() == "1/1/1900" ? "Not Specified" : objCRoutings_CustomsClearance.lstCVarRoutings[0].QasimaDate.ToLongDateString()) + "<br>";
                    }
                    #region Send
                    string FromMail = objCvwDefaults.lstCVarvwDefaults[0].Email;
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(objCvwDefaults.lstCVarvwDefaults[0].Email_Host, objCvwDefaults.lstCVarvwDefaults[0].Email_Port);
                    SmtpServer.UseDefaultCredentials = true;
                    mail.From = new MailAddress(FromMail);
                    for (int i = 0; i < objCvwOperationEmailSent.lstCVarvwOperationEmailSent.Count; i++)
                    {
                        if (objCvwOperationEmailSent.lstCVarvwOperationEmailSent[i].DepartmentEmail != "0"
                            && objCvwOperationEmailSent.lstCVarvwOperationEmailSent[i].DepartmentEmail != "")
                        {
                            _boolEmailFound = true;
                            mail.To.Add(objCvwOperationEmailSent.lstCVarvwOperationEmailSent[i].DepartmentEmail);
                        }
                    }
                    mail.IsBodyHtml = true;
                    mail.Subject = subject;
                    mail.Body = body;
                    SmtpServer.Host = objCvwDefaults.lstCVarvwDefaults[0].Email_Host;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(FromMail, CEncryptDecrypt.Decrypt(objCvwDefaults.lstCVarvwDefaults[0].Email_Password, true));
                    SmtpServer.EnableSsl = objCvwDefaults.lstCVarvwDefaults[0].Email_IsSSL;
                    if (_boolEmailFound)
                        try
                        {
                            SmtpServer.Send(mail);
                        }
                        catch (Exception ex)
                        {
                            _MessageReturned = ex.Message;
                        }
                    #endregion Send
                    #region Update OperationEmailSent
                    if (_MessageReturned == "")
                    {
                        string _UpdateClause = "";
                        _UpdateClause = "OpenDate= (SELECT OpenDate FROM vwOperations WHERE ID=" + pOperationID + ")" + " \n";
                        _UpdateClause += ",CloseDate= (SELECT CloseDate FROM vwOperations WHERE ID=" + pOperationID + ")" + " \n";
                        _UpdateClause += ",CutOffDate= (SELECT CutOffDate FROM vwOperations WHERE ID=" + pOperationID + ")" + " \n";
                        _UpdateClause += ",PODate= (SELECT PODate FROM vwOperations WHERE ID=" + pOperationID + ")" + " \n";
                        _UpdateClause += ",ReleaseDate= (SELECT ReleaseDate FROM vwOperations WHERE ID=" + pOperationID + ")" + " \n";
                        _UpdateClause += ",ETAPOLDate= (SELECT ETAPOLDate FROM vwOperations WHERE ID=" + pOperationID + ")" + " \n";
                        _UpdateClause += ",ATAPOLDate= (SELECT ATAPOLDate FROM vwOperations WHERE ID=" + pOperationID + ")" + " \n";
                        _UpdateClause += ",ExpectedDeparture= (SELECT ExpectedDeparture FROM vwOperations WHERE ID=" + pOperationID + ")" + " \n";
                        _UpdateClause += ",ActualDeparture= (SELECT ActualDeparture FROM vwOperations WHERE ID=" + pOperationID + ")" + " \n";
                        _UpdateClause += ",ExpectedArrival= (SELECT ExpectedArrival FROM vwOperations WHERE ID=" + pOperationID + ")" + " \n";
                        //Routing TruckingOrder
                        _UpdateClause += ",GateInDate= (SELECT Top 1 GateInDate FROM Routings WHERE RoutingTypeID=60 AND OperationID=" + pOperationID + ")" + " \n";
                        _UpdateClause += ",GateOutDate= (SELECT Top 1 GateOutDate FROM Routings WHERE RoutingTypeID=60 AND OperationID=" + pOperationID + ")" + " \n";
                        _UpdateClause += ",StuffingDate= (SELECT Top 1 StuffingDate FROM Routings WHERE RoutingTypeID=60 AND OperationID=" + pOperationID + ")" + " \n";
                        _UpdateClause += ",DeliveryDate= (SELECT Top 1 DeliveryDate FROM Routings WHERE RoutingTypeID=60 AND OperationID=" + pOperationID + ")" + " \n";
                        //Routing CustomsClearance
                        _UpdateClause += ",CertificateDate= (SELECT TOP 1 CertificateDate FROM Routings WHERE RoutingTypeID=70 AND OperationID=" + pOperationID + ")" + " \n";
                        _UpdateClause += ",QasimaDate= (SELECT TOP 1 QasimaDate FROM Routings WHERE RoutingTypeID=70 AND OperationID=" + pOperationID + ")" + " \n";
                        _UpdateClause += "WHERE OperationID=" + pOperationID + " \n";
                        checkException = objCOperationEmailSent.UpdateList(_UpdateClause);
                    }
                    #endregion Update OperationEmailSent
                }
                #endregion Send Email

            } //if (objCvwDefaults.lstCVarvwDefaults[0].IsDepartmentOption)
        }
        #endregion Static function for sending email notifictions

        #region OperationPayablesAndReceivables
        [HttpGet, HttpPost]
        public object[] GetContainerTrackingPayablesAndReceivables(Int64 pOperationID, Int64 pOperationContainersAndPackagesID)
        {
            bool _result = false;
            Exception checkException = new Exception();
            CvwPayables objCvwPayables = new CvwPayables();
            CvwReceivables objCvwReceivables = new CvwReceivables();
            Int32 _RowCount = 0;
            checkException = objCvwPayables.GetListPaging(1000, 1, "WHERE OperationID=" + pOperationID.ToString() + " AND OperationContainersAndPackagesID=" + pOperationContainersAndPackagesID, "ChargeTypeName", out _RowCount);
            checkException = objCvwReceivables.GetListPaging(1000, 1, "WHERE OperationID=" + pOperationID.ToString() + " AND IsDeleted=0 AND OperationContainersAndPackagesID=" + pOperationContainersAndPackagesID, "ChargeTypeName", out _RowCount);
            if (checkException == null)
                _result = true;
            return new object[]
            {
                _result
                , _result ? new JavaScriptSerializer().Serialize(objCvwPayables.lstCVarvwPayables) : null //pData[1]
                , _result ? new JavaScriptSerializer().Serialize(objCvwReceivables.lstCVarvwReceivables) : null //pData[2]
            };
        }
        #endregion OperationPayablesAndReceivables

        #region AWB
        [HttpGet, HttpPost]
        public object[] InsertShipmentAWB([FromBody] InsertShipmentAWBData insertShipmentAWBData)
        {
            bool _result = true;
            int MainCarraigeRoutingTypeID = 30;
            CVarOperations objCVarOperations = new CVarOperations();
            string pUpdateClause = "";
            CvwOperations objCMaster = new CvwOperations();
            CvwOperations objCHouses = new CvwOperations();
            int _RowCount = 0;

            objCVarOperations.Code = insertShipmentAWBData.pCode;
            objCVarOperations.HouseNumber = insertShipmentAWBData.pIsShipment ? insertShipmentAWBData.pHouseNumber : insertShipmentAWBData.pMAWBSuffix;//if HouseNumber is not null then its entered manually
            objCVarOperations.BranchID = int.Parse(insertShipmentAWBData.pBranchID);
            objCVarOperations.SalesmanID = int.Parse(insertShipmentAWBData.pSalesmanID);
            objCVarOperations.BLType = int.Parse(insertShipmentAWBData.pBLType);
            objCVarOperations.BLTypeIconName = insertShipmentAWBData.pBLTypeIconName;
            objCVarOperations.BLTypeIconStyle = insertShipmentAWBData.pBLTypeIconStyle;
            objCVarOperations.DirectionType = int.Parse(insertShipmentAWBData.pDirectionType);
            objCVarOperations.DirectionIconName = insertShipmentAWBData.pDirectionIconName;
            objCVarOperations.DirectionIconStyle = insertShipmentAWBData.pDirectionIconStyle;
            objCVarOperations.TransportType = int.Parse(insertShipmentAWBData.pTransportType);
            objCVarOperations.TransportIconName = insertShipmentAWBData.pTransportIconName;
            objCVarOperations.TransportIconStyle = insertShipmentAWBData.pTransportIconStyle;
            objCVarOperations.ShipmentType = int.Parse(insertShipmentAWBData.pShipmentType);
            objCVarOperations.MasterBL = insertShipmentAWBData.pMasterBL;
            objCVarOperations.ShipperID = int.Parse(insertShipmentAWBData.pShipperID);
            objCVarOperations.ShipperAddressID = Int64.Parse(insertShipmentAWBData.pShipperAddressID);
            objCVarOperations.ShipperContactID = Int64.Parse(insertShipmentAWBData.pShipperContactID);
            objCVarOperations.ConsigneeID = insertShipmentAWBData.pConsigneeID == "" ? 0 : int.Parse(insertShipmentAWBData.pConsigneeID);
            objCVarOperations.ConsigneeAddressID = Int64.Parse(insertShipmentAWBData.pConsigneeAddressID);
            objCVarOperations.ConsigneeContactID = Int64.Parse(insertShipmentAWBData.pConsigneeContactID);
            objCVarOperations.AgentID = insertShipmentAWBData.pAgentID == "" ? 0 : int.Parse(insertShipmentAWBData.pAgentID);
            objCVarOperations.AgentAddressID = Int64.Parse(insertShipmentAWBData.pAgentAddressID);
            objCVarOperations.AgentContactID = Int64.Parse(insertShipmentAWBData.pAgentContactID);
            objCVarOperations.ConsigneeID2 = 0;
            objCVarOperations.IncotermID = int.Parse(insertShipmentAWBData.pIncotermID);
            objCVarOperations.POrC = insertShipmentAWBData.pPOrC;
            objCVarOperations.MoveTypeID = int.Parse(insertShipmentAWBData.pMoveTypeID);
            objCVarOperations.CommodityID = int.Parse(insertShipmentAWBData.pCommodityID);
            objCVarOperations.CommodityID2 = 0;
            objCVarOperations.CommodityID3 = 0;
            objCVarOperations.TransientTime = int.Parse(insertShipmentAWBData.pTransientTime);
            //objCVarOperations.OpenDate = DateTime.Parse(insertOperationData.pOpenDate);
            objCVarOperations.OpenDate = DateTime.ParseExact(insertShipmentAWBData.pOpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objCVarOperations.CloseDate = DateTime.ParseExact(insertShipmentAWBData.pCloseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objCVarOperations.CutOffDate = DateTime.Parse(insertShipmentAWBData.pCutOffDate);
            objCVarOperations.ReleaseDate = DateTime.Parse("01/01/1900");
            objCVarOperations.Form13Date = DateTime.Parse("01/01/1900");

            objCVarOperations.ClearanceApprovalDate = DateTime.Parse("01-01-1900");
            objCVarOperations.TruckingApprovalDate = DateTime.Parse("01-01-1900");
            objCVarOperations.FreightApprovalDate = DateTime.Parse("01-01-1900");




            objCVarOperations.IncludePickup = (insertShipmentAWBData.pIncludePickup == "True" ? true : false);
            objCVarOperations.PickupCityID = int.Parse(insertShipmentAWBData.pPickupCityID);
            objCVarOperations.PickupAddressID = int.Parse(insertShipmentAWBData.pPickupAddressID);
            objCVarOperations.POLCountryID = int.Parse(insertShipmentAWBData.pPOLCountryID);
            objCVarOperations.POL = int.Parse(insertShipmentAWBData.pPOL);
            objCVarOperations.PODCountryID = int.Parse(insertShipmentAWBData.pPODCountryID);
            objCVarOperations.POD = insertShipmentAWBData.pPOD == "" ? 0 : int.Parse(insertShipmentAWBData.pPOD);
            objCVarOperations.ShippingLineID = int.Parse(insertShipmentAWBData.pShippingLineID);
            objCVarOperations.AirlineID = int.Parse(insertShipmentAWBData.pAirlineID);
            objCVarOperations.TruckerID = int.Parse(insertShipmentAWBData.pTruckerID);
            objCVarOperations.IncludeDelivery = (insertShipmentAWBData.pIncludeDelivery == "True" ? true : false);
            objCVarOperations.DeliveryZipCode = insertShipmentAWBData.pDeliveryZipCode;
            objCVarOperations.DeliveryCityID = int.Parse(insertShipmentAWBData.pDeliveryCityID);
            objCVarOperations.DeliveryCountryID = int.Parse(insertShipmentAWBData.pDeliveryCountryID);
            //objCVarOperations.GrossWeight = decimal.Parse(insertOperationData.pGrossWeight);
            //objCVarOperations.Volume = decimal.Parse(insertOperationData.pVolume);
            objCVarOperations.IsDangerousGoods = (insertShipmentAWBData.pIsDangerousGoods == "True" ? true : false);
            objCVarOperations.Notes = (insertShipmentAWBData.pNotes == null ? "" : insertShipmentAWBData.pNotes.Trim().ToUpper());//used as the customer reference entered in General tab
            objCVarOperations.OperationStageID = int.Parse(insertShipmentAWBData.pOperationStageID);
            objCVarOperations.NumberOfHousesConnected = 0;
            objCVarOperations.MAWBStockID = insertShipmentAWBData.pMAWBStockID;
            objCVarOperations.MasterOperationID = insertShipmentAWBData.pMasterOperationID;
            objCVarOperations.MAWBSuffix = insertShipmentAWBData.pMAWBSuffix;
            objCVarOperations.Via1 = insertShipmentAWBData.pVia1;
            objCVarOperations.Via2 = insertShipmentAWBData.pVia2;
            objCVarOperations.Via3 = insertShipmentAWBData.pVia3;
            objCVarOperations.AirLineID1 = insertShipmentAWBData.pAirLineID1;
            objCVarOperations.FlightNo1 = insertShipmentAWBData.pFlightNo1;
            objCVarOperations.FlightDate1 = DateTime.ParseExact(insertShipmentAWBData.pFlightDate1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objCVarOperations.AirLineID2 = insertShipmentAWBData.pAirLineID2;
            objCVarOperations.FlightNo2 = insertShipmentAWBData.pFlightNo2;
            objCVarOperations.FlightDate2 = DateTime.ParseExact(insertShipmentAWBData.pFlightDate2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objCVarOperations.AirLineID3 = insertShipmentAWBData.pAirLineID3;
            objCVarOperations.FlightNo3 = insertShipmentAWBData.pFlightNo3;
            objCVarOperations.FlightDate3 = DateTime.ParseExact(insertShipmentAWBData.pFlightDate3, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            objCVarOperations.UNOrID = insertShipmentAWBData.pUNOrID;
            objCVarOperations.ProperShippingName = insertShipmentAWBData.pProperShippingName;
            objCVarOperations.ClassOrDivision = insertShipmentAWBData.pClassOrDivision;
            objCVarOperations.PackingGroup = insertShipmentAWBData.pPackingGroup;
            objCVarOperations.QuantityAndTypeOfPacking = insertShipmentAWBData.pQuantityAndTypeOfPacking;
            objCVarOperations.PackingInstruction = insertShipmentAWBData.pPackingInstruction;
            objCVarOperations.ShippingDeclarationAuthorization = insertShipmentAWBData.pShippingDeclarationAuthorization;
            objCVarOperations.Barcode = insertShipmentAWBData.pBarcode;

            objCVarOperations.GuaranteeLetterNumber = "0";
            objCVarOperations.GuaranteeLetterDate = DateTime.Parse("01/01/1900");
            objCVarOperations.GuaranteeLetterAmount = "0";
            objCVarOperations.GuaranteeLetterSupplierInvoiceNumber = "0";
            objCVarOperations.BankAccountID = 0;
            objCVarOperations.GuaranteeLetterNotes = "0";

            objCVarOperations.HandlingInformation = insertShipmentAWBData.pHandlingInformation;
            objCVarOperations.Description = insertShipmentAWBData.pDescription;
            objCVarOperations.AmountOfInsurance = insertShipmentAWBData.pAmountOfInsurance;
            objCVarOperations.BLDate = DateTime.ParseExact(insertShipmentAWBData.pBLDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objCVarOperations.DeclaredValueForCarriage = insertShipmentAWBData.pDeclaredValueForCarriage;
            objCVarOperations.WeightCharge = insertShipmentAWBData.pWeightCharge;
            objCVarOperations.ValuationCharge = insertShipmentAWBData.pValuationCharge;
            objCVarOperations.Tax = insertShipmentAWBData.pTax;
            objCVarOperations.OtherChargesDueAgent = insertShipmentAWBData.pOtherChargesDueAgent;
            objCVarOperations.OtherChargesDueCarrier = insertShipmentAWBData.pOtherChargesDueCarrier;
            objCVarOperations.OtherCharges = insertShipmentAWBData.pOtherCharges;
            objCVarOperations.CurrencyID = insertShipmentAWBData.pCurrencyID;
            objCVarOperations.AccountingInformation = insertShipmentAWBData.pAccountingInformation;
            objCVarOperations.ReferenceNumber = insertShipmentAWBData.pReferenceNumber;
            objCVarOperations.OptionalShippingInformation = insertShipmentAWBData.pOptionalShippingInformation;
            objCVarOperations.CHGSCode = insertShipmentAWBData.pCHGSCode;
            objCVarOperations.WT_VALL = insertShipmentAWBData.pWT_VALL;
            objCVarOperations.WT_VALL_Other = insertShipmentAWBData.pWT_VALL_Other;
            objCVarOperations.DeclaredValueForCustoms = Convert.ToString(insertShipmentAWBData.pDeclaredValueForCustoms);
            //objCVarAirOperations.ChargeableWeight = decimal.Parse(InsertShipmentAWBData.pChargeableWeight);
            objCVarOperations.ChargeableWeight = (insertShipmentAWBData.pWeightCharge);
            objCVarOperations.TypeOfStockID = insertShipmentAWBData.pTypeOfStockID;
            objCVarOperations.FlightNo = insertShipmentAWBData.pFlightNo;
            // add column with auto value********************
            //objCVarAirOperations.BLDate = DateTime.Parse("01-01-1900");
            objCVarOperations.HBLDate = DateTime.Parse("01-01-1900");
            objCVarOperations.PickupAddress = "0"; //updated from main route
            objCVarOperations.DeliveryAddress = "0"; //updated from main route
            objCVarOperations.CustomerReference = "0";
            objCVarOperations.SupplierReference = "0";
            objCVarOperations.PONumber = "0";
            objCVarOperations.POValue = "0";
            objCVarOperations.AgreedRate = "0";
            objCVarOperations.ACIDNumber = "0";
            objCVarOperations.ACIDDetails = "0";
            objCVarOperations.BookingNumber = "0";


            objCVarOperations.UNNumber = insertShipmentAWBData.pUNNumber;
            objCVarOperations.IMOClass = insertShipmentAWBData.pIMOClass;
            objCVarOperations.VesselID = insertShipmentAWBData.pVesselID;
            objCVarOperations.BookingNumber = "0";
            objCVarOperations.ACIDNumber = "0";
            objCVarOperations.ACIDDetails = "0";
            objCVarOperations.HouseParentID = 0;

            objCVarOperations.IsAWB = insertShipmentAWBData.pIsAWB;
            objCVarOperations.CreatorUserID = objCVarOperations.ModificatorUserID = WebSecurity.CurrentUserId;
            objCVarOperations.CreationDate = DateTime.ParseExact(insertShipmentAWBData.pOpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objCVarOperations.ModificationDate = DateTime.Now;
            // ************************************************

            COperations objCOperations = new COperations();
            objCOperations.lstCVarOperations.Add(objCVarOperations);
            Exception checkException = objCOperations.SaveMethod(objCOperations.lstCVarOperations);

            #region CreateCostCenter
            CSystemOptions objCSystemOptions = new CSystemOptions();
            objCSystemOptions.GetList("Where OptionID=94");
            if (objCSystemOptions.lstCVarSystemOptions.Count > 0 && objCSystemOptions.lstCVarSystemOptions[0].OptionValue
                  && objCOperations.lstCVarOperations[0].BLType != 2)
            {
                string CostCenterNumberParent = "";
                Int32 pID = 0;
                CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
                CA_CostCenters objCA_CostCentersParent = new CA_CostCenters();
                objCA_CostCentersParent.GetList("where ( CostCenterName=N'عمليات' or CostCenterName='Operations' ) and Parent_ID is null ");
                if (objCA_CostCentersParent.lstCVarA_CostCenters.Count > 0)
                {
                    pID = objCA_CostCentersParent.lstCVarA_CostCenters[0].ID;
                    CostCenterNumberParent = objCA_CostCentersParent.lstCVarA_CostCenters[0].RealCostCenterCode;
                }

                else
                {

                    string pNewCodePartner = objCCustomizedDBCall.CallStringFunction("SELECT [dbo].A_CostCenters_GetCode(" + (pID == 0 ? "null" : pID.ToString()) + ") AS Code");


                    CVarA_CostCenters objCVarA_CostCenters = new CVarA_CostCenters();
                    objCVarA_CostCenters.CostCenterNumber = pNewCodePartner.PadRight(12, '0');
                    objCVarA_CostCenters.CostCenterName = "Operations";
                    objCVarA_CostCenters.Parent_ID = 0;
                    objCVarA_CostCenters.IsMain = true;
                    objCVarA_CostCenters.CCLevel = 1;
                    objCVarA_CostCenters.RealCostCenterCode = pNewCodePartner;
                    objCVarA_CostCenters.User_ID = WebSecurity.CurrentUserId;
                    objCVarA_CostCenters.Type_ID = 0;
                    objCVarA_CostCenters.IsClosed = false;
                    objCVarA_CostCenters.SubAccountGroupID = 0;
                    objCVarA_CostCenters.EmployeesCount = 0;
                    objCA_CostCentersParent.lstCVarA_CostCenters.Add(objCVarA_CostCenters);
                    checkException = objCA_CostCentersParent.SaveMethod(objCA_CostCentersParent.lstCVarA_CostCenters);

                    pID = objCA_CostCentersParent.lstCVarA_CostCenters[0].ID;
                    CostCenterNumberParent = objCA_CostCentersParent.lstCVarA_CostCenters[0].RealCostCenterCode;
                }

                CA_CostCenters objCA_CostCenters = new CA_CostCenters();
                checkException = objCA_CostCenters.GetListPaging(1, 1, "WHERE ID = " + pID.ToString(), "ID", out _RowCount);
                string pNewCode = objCCustomizedDBCall.CallStringFunction("SELECT [dbo].A_CostCenters_GetCode(" + (pID == 0 ? "null" : pID.ToString()) + ") AS Code");

                CvwOperations objCvwOperations = new CvwOperations();
                objCvwOperations.GetList("WHERE ID=" + objCOperations.lstCVarOperations[0].ID);
                string pNewCodeNew = CostCenterNumberParent + pNewCode;
                CVarA_CostCenters objCVarA_CostCentersChild = new CVarA_CostCenters();
                objCVarA_CostCentersChild.CostCenterNumber = pNewCodeNew.PadRight(12, '0'); ;
                objCVarA_CostCentersChild.CostCenterName = objCvwOperations.lstCVarvwOperations[0].Code + " - " + objCvwOperations.lstCVarvwOperations[0].ClientName;
                objCVarA_CostCentersChild.Parent_ID = pID;
                objCVarA_CostCentersChild.IsMain = false;
                objCVarA_CostCentersChild.CCLevel = 2;
                objCVarA_CostCentersChild.RealCostCenterCode = pNewCodeNew;
                objCVarA_CostCentersChild.User_ID = WebSecurity.CurrentUserId;
                objCVarA_CostCentersChild.Type_ID = 0;
                objCVarA_CostCentersChild.IsClosed = false;
                objCVarA_CostCentersChild.SubAccountGroupID = 0;
                objCVarA_CostCentersChild.EmployeesCount = 0;
                objCA_CostCentersParent.lstCVarA_CostCenters.Add(objCVarA_CostCentersChild);
                checkException = objCA_CostCentersParent.SaveMethod(objCA_CostCentersParent.lstCVarA_CostCenters);

                //Link Oeation With CostCenter Add by ahmed maher
                int _RowCount2 = 0;
                CvwDefaults objCvwDefaults = new CvwDefaults();
                objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount2); //i am sure i ve just one row isa
                string CompanyName2 = objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName;
                if (CompanyName2 == "BED")
                {
                    CVarA_LinkOperationWithCostCenter objCVarA_LinkOperationWithCostCenter = new CVarA_LinkOperationWithCostCenter();

                    objCVarA_LinkOperationWithCostCenter.OperationID = objCOperations.lstCVarOperations[0].ID;
                    objCVarA_LinkOperationWithCostCenter.CostCenterID = objCVarA_CostCentersChild.ID;

                    CA_LinkOperationWithCostCenter objA_LinkOperationWithCostCenter = new CA_LinkOperationWithCostCenter();
                    objA_LinkOperationWithCostCenter.lstCVarA_LinkOperationWithCostCenter.Add(objCVarA_LinkOperationWithCostCenter);
                    checkException = objA_LinkOperationWithCostCenter.SaveMethod(objA_LinkOperationWithCostCenter.lstCVarA_LinkOperationWithCostCenter);
                }

            }


            #endregion
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
            {
                _result = true;
                //COPY Partners To OperationPartners
                #region Copy Operation Partners (at this point its just either a shipper or a consignee or an agent and empty notify)
                COperationPartners objCOperationPartners = new COperationPartners();
                CVarOperationPartners objCVarOperationPartners = new CVarOperationPartners();
                CContacts objCContacts = new CContacts();//to save a contact by default

                //to save a contact by default //PartnerTypeID = 2 for Agents
                checkException = objCContacts.GetList(" WHERE PartnerTypeID = 2 AND PartnerID = " + insertShipmentAWBData.pAgentID.ToString());

                CVarOperationPartners objCVarOperationAgentPartner = new CVarOperationPartners();
                objCVarOperationAgentPartner.OperationID = objCOperations.lstCVarOperations[0].ID;
                objCVarOperationAgentPartner.OperationPartnerTypeID = 6; //Agent
                objCVarOperationAgentPartner.AgentID = int.Parse(insertShipmentAWBData.pAgentID);
                objCVarOperationAgentPartner.ContactID = (objCContacts.lstCVarContacts.Count > 0 ? objCContacts.lstCVarContacts[0].ID : 0);
                objCVarOperationAgentPartner.IsOperationClient = false;
                objCVarOperationAgentPartner.CreatorUserID = objCVarOperationAgentPartner.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarOperationAgentPartner.CreationDate = objCVarOperationAgentPartner.ModificationDate = DateTime.Now;
                objCOperationPartners.lstCVarOperationPartners.Add(objCVarOperationAgentPartner);

                //to save a contact by default //PartnerTypeID = 1 for Customer
                checkException = objCContacts.GetList(" WHERE PartnerTypeID = 1 AND PartnerID = " + insertShipmentAWBData.pConsigneeID.ToString());

                CVarOperationPartners objCVarOperationConsigneePartner = new CVarOperationPartners();
                objCVarOperationConsigneePartner.OperationID = objCOperations.lstCVarOperations[0].ID;
                objCVarOperationConsigneePartner.OperationPartnerTypeID = 2;
                objCVarOperationConsigneePartner.CustomerID = int.Parse(insertShipmentAWBData.pConsigneeID);
                objCVarOperationConsigneePartner.ContactID = (objCContacts.lstCVarContacts.Count > 0 ? objCContacts.lstCVarContacts[0].ID : 0);
                objCVarOperationConsigneePartner.IsOperationClient = false;
                objCVarOperationConsigneePartner.CreatorUserID = objCVarOperationConsigneePartner.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarOperationConsigneePartner.CreationDate = objCVarOperationConsigneePartner.ModificationDate = DateTime.Now;
                objCOperationPartners.lstCVarOperationPartners.Add(objCVarOperationConsigneePartner);


                //to save a contact by default //PartnerTypeID = 1 for Customer
                checkException = objCContacts.GetList(" WHERE PartnerTypeID = 1 AND PartnerID = " + insertShipmentAWBData.pShipperID.ToString());

                CVarOperationPartners objCVarOperationShipperPartner = new CVarOperationPartners();
                objCVarOperationShipperPartner.OperationID = objCOperations.lstCVarOperations[0].ID;
                objCVarOperationShipperPartner.OperationPartnerTypeID = 1; // export or domestic (shipper)
                objCVarOperationShipperPartner.CustomerID = int.Parse(insertShipmentAWBData.pShipperID);
                objCVarOperationShipperPartner.ContactID = (objCContacts.lstCVarContacts.Count > 0 ? objCContacts.lstCVarContacts[0].ID : 0);
                objCVarOperationShipperPartner.IsOperationClient = false;
                objCVarOperationShipperPartner.CreatorUserID = objCVarOperationShipperPartner.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarOperationShipperPartner.CreationDate = objCVarOperationShipperPartner.ModificationDate = DateTime.Now;
                objCOperationPartners.lstCVarOperationPartners.Add(objCVarOperationShipperPartner);

                checkException = objCContacts.GetList(" WHERE PartnerTypeID = 1 AND PartnerID = " + insertShipmentAWBData.pNotifyID.ToString());

                CVarOperationPartners objCVarOperationNotifyPartner = new CVarOperationPartners();
                objCVarOperationNotifyPartner.OperationID = objCOperations.lstCVarOperations[0].ID;
                objCVarOperationNotifyPartner.OperationPartnerTypeID = 4;//Notify1
                objCVarOperationNotifyPartner.CustomerID = Int32.Parse(insertShipmentAWBData.pNotifyID); // it will be set as null in DB
                objCVarOperationNotifyPartner.ContactID = (objCContacts.lstCVarContacts.Count > 0 ? objCContacts.lstCVarContacts[0].ID : 0);
                objCVarOperationNotifyPartner.IsOperationClient = false;
                objCVarOperationNotifyPartner.CreatorUserID = objCVarOperationNotifyPartner.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarOperationNotifyPartner.CreationDate = objCVarOperationNotifyPartner.ModificationDate = DateTime.Now;
                objCOperationPartners.lstCVarOperationPartners.Add(objCVarOperationNotifyPartner);

                objCOperationPartners.SaveMethod(objCOperationPartners.lstCVarOperationPartners);
                #endregion

                //COPY Routings To Routings
                //MainCarraige has ID = 30
                #region Copy Operation Routings (at this point its just Main Carraige)
                CVarRoutings objCVarRoutings = new CVarRoutings();

                objCVarRoutings.OperationID = objCOperations.lstCVarOperations[0].ID;
                objCVarRoutings.TransportType = objCOperations.lstCVarOperations[0].TransportType;
                objCVarRoutings.TransportIconName = objCOperations.lstCVarOperations[0].TransportIconName;
                objCVarRoutings.TransportIconStyle = objCOperations.lstCVarOperations[0].TransportIconStyle;
                objCVarRoutings.RoutingTypeID = MainCarraigeRoutingTypeID; //Main Carraige
                objCVarRoutings.POLCountryID = objCOperations.lstCVarOperations[0].POLCountryID;
                objCVarRoutings.POL = objCOperations.lstCVarOperations[0].POL;
                objCVarRoutings.PODCountryID = objCOperations.lstCVarOperations[0].PODCountryID;
                objCVarRoutings.POD = objCOperations.lstCVarOperations[0].POD;
                objCVarRoutings.ETAPOLDate = DateTime.Parse("01-01-1900");
                objCVarRoutings.ATAPOLDate = DateTime.Parse("01-01-1900");
                //objCVarRoutings.ActualDeparture = DateTime.ParseExact(insertShipmentAWBData.pExpectedDeparture, "dd/MM/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);
                objCVarRoutings.ExpectedDeparture = DateTime.Parse("01-01-1900"); //DateTime.ParseExact(insertShipmentAWBData.pExpectedDeparture, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                objCVarRoutings.ActualDeparture = DateTime.Parse("01-01-1900");
                objCVarRoutings.ExpectedArrival = DateTime.Parse("01-01-1900"); //DateTime.ParseExact(insertShipmentAWBData.pExpectedArrival, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                objCVarRoutings.ActualArrival = DateTime.Parse("01-01-1900");
                objCVarRoutings.VesselID = 0;
                objCVarRoutings.VoyageOrTruckNumber = "0";
                objCVarRoutings.RoadNumber = "0";
                objCVarRoutings.DeliveryOrderNumber = "0";
                objCVarRoutings.WareHouse = "0";
                objCVarRoutings.WareHouseLocation = "0";
                objCVarRoutings.Notes = "";

                if (insertShipmentAWBData.pTransportType == "1") //Ocean
                {
                    objCVarRoutings.ShippingLineID = objCOperations.lstCVarOperations[0].ShippingLineID;
                    objCVarRoutings.AirlineID = 0;
                    objCVarRoutings.TruckerID = 0;
                }
                else if (insertShipmentAWBData.pTransportType == "2") //Air
                {
                    objCVarRoutings.ShippingLineID = 0;
                    objCVarRoutings.AirlineID = objCOperations.lstCVarOperations[0].AirlineID;
                    objCVarRoutings.TruckerID = 0;
                }
                else //Inland , TransportType = 3
                {
                    objCVarRoutings.ShippingLineID = 0;
                    objCVarRoutings.AirlineID = 0;
                    objCVarRoutings.TruckerID = objCOperations.lstCVarOperations[0].TruckerID;
                }

                objCVarRoutings.GensetSupplierID = 0;
                objCVarRoutings.CCAID = 0;
                objCVarRoutings.Quantity = "0";
                objCVarRoutings.ContactPerson = "0";
                objCVarRoutings.PickupAddress = "0";
                objCVarRoutings.DeliveryAddress = "0";
                objCVarRoutings.GateInPortID = 0;
                objCVarRoutings.GateOutPortID = 0;
                objCVarRoutings.GateInDate = DateTime.Parse("01/01/1900");

                #region TransportOrder
                objCVarRoutings.CustomerID = 0;
                objCVarRoutings.SubContractedCustomerID = 0;
                objCVarRoutings.Cost = 0;
                objCVarRoutings.Sale = 0;
                objCVarRoutings.IsFleet = false;
                objCVarRoutings.CommodityID = 0;
                objCVarRoutings.LoadingDate = DateTime.Parse("01/01/1900");
                objCVarRoutings.LoadingReference = "0";
                objCVarRoutings.UnloadingDate = DateTime.Parse("01/01/1900");
                objCVarRoutings.UnloadingReference = "0";
                objCVarRoutings.UnloadingTime = "0";
                #endregion TransportOrder

                objCVarRoutings.GateOutDate = DateTime.Parse("01/01/1900");
                objCVarRoutings.StuffingDate = DateTime.Parse("01/01/1900");
                objCVarRoutings.DeliveryDate = DateTime.Parse("01/01/1900");
                objCVarRoutings.BookingNumber = "0";
                objCVarRoutings.Delays = "0";
                objCVarRoutings.DriverName = "0";
                objCVarRoutings.DriverPhones = "0";
                objCVarRoutings.PowerFromGateInTillActualSailing = "0";
                objCVarRoutings.ContactPersonPhones = "0";
                objCVarRoutings.LoadingTime = "0";

                #region CustomsClearance
                objCVarRoutings.CCAFreight = 0;
                objCVarRoutings.CCAFOB = 0;
                objCVarRoutings.CCACFValue = 0;
                objCVarRoutings.CCAInvoiceNumber = "0";

                objCVarRoutings.CCAInsurance = "0";
                objCVarRoutings.CCADischargeValue = "0";
                objCVarRoutings.CCAAcceptedValue = "0";
                objCVarRoutings.CCAImportValue = "0";
                objCVarRoutings.CCADocumentReceiveDate = DateTime.Parse("01/01/1900");
                objCVarRoutings.CCAExchangeRate = "0";
                objCVarRoutings.CCAVATCertificateNumber = "0";
                objCVarRoutings.CCAVATCertificateValue = "0";
                objCVarRoutings.CCACommercialProfitCertificateNumber = "0";
                objCVarRoutings.CCAOthers = "0";
                objCVarRoutings.CCASpendDate = DateTime.Parse("01/01/1900");
                objCVarRoutings.OffloadingDate = DateTime.Parse("01/01/1900");

                objCVarRoutings.CertificateNumber = "0";
                objCVarRoutings.CertificateValue = "0";
                objCVarRoutings.CertificateDate = DateTime.Parse("01/01/1900");
                objCVarRoutings.QasimaNumber = "0";
                objCVarRoutings.QasimaDate = DateTime.Parse("01/01/1900");
                objCVarRoutings.Match = false;
                objCVarRoutings.SalesDateReceived = DateTime.Parse("01/01/1900");
                objCVarRoutings.CommerceDateReceived = DateTime.Parse("01/01/1900");
                objCVarRoutings.InspectionDateReceived = DateTime.Parse("01/01/1900");
                objCVarRoutings.FinishDateReceived = DateTime.Parse("01/01/1900");
                objCVarRoutings.SalesDateDelivered = DateTime.Parse("01/01/1900");
                objCVarRoutings.CommerceDateDelivered = DateTime.Parse("01/01/1900");
                objCVarRoutings.InspectionDateDelivered = DateTime.Parse("01/01/1900");
                objCVarRoutings.FinishDateDelivered = DateTime.Parse("01/01/1900");

                objCVarRoutings.CCAllowTemporaryDelivered = DateTime.Parse("01/01/1900");
                objCVarRoutings.CCAllowTemporaryReceived = DateTime.Parse("01/01/1900");
                objCVarRoutings.CCDropBackDelivered = DateTime.Parse("01/01/1900");
                objCVarRoutings.CCDropBackReceived = DateTime.Parse("01/01/1900");
                objCVarRoutings.CC_ClearanceTypeID = 0;
                objCVarRoutings.CC_CustomItemsID = 0;
                objCVarRoutings.CCReleaseNo = "0";
                #endregion CustomsClearance

                objCVarRoutings.BillNumber = "0";
                objCVarRoutings.TruckingOrderCode = "0";

                objCVarRoutings.CreatorUserID = objCVarRoutings.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarRoutings.ModificationDate = objCVarRoutings.CreationDate = DateTime.Now;

                CRoutings objCRoutings = new CRoutings();
                objCRoutings.lstCVarRoutings.Add(objCVarRoutings);
                objCRoutings.SaveMethod(objCRoutings.lstCVarRoutings);
                #endregion

                #region Copy default AirLineChargeTypes
                CAirLineChargeTypes objCAirLineChargeTypes = new CAirLineChargeTypes();
                objCAirLineChargeTypes.GetList("WHERE AirLineId=" + insertShipmentAWBData.pAirlineID + " AND IsDefault=1");
                for (int i = 0; i < objCAirLineChargeTypes.lstCVarAirLineChargeTypes.Count; i++)
                {
                    CVarPayables objCVarPayables = new CVarPayables();
                    objCVarPayables.OperationID = objCVarOperations.ID;
                    objCVarPayables.ChargeTypeID = objCAirLineChargeTypes.lstCVarAirLineChargeTypes[i].ChargeTypeID;
                    objCVarPayables.POrC = objCVarOperations.POrC;
                    objCVarPayables.SupplierOperationPartnerID = 0;
                    objCVarPayables.Quantity = 1;
                    objCVarPayables.CostPrice = 0;
                    objCVarPayables.AmountWithoutVAT = 0;
                    objCVarPayables.TaxTypeID = 0;
                    objCVarPayables.TaxPercentage = 0;
                    objCVarPayables.TaxAmount = 0;
                    objCVarPayables.DiscountTypeID = 0;
                    objCVarPayables.DiscountPercentage = 0;
                    objCVarPayables.DiscountAmount = 0;
                    objCVarPayables.CostAmount = 0;
                    objCVarPayables.PaidAmount = 0;
                    objCVarPayables.RemainingAmount = 0;
                    objCVarPayables.InitialSalePrice = 0;
                    objCVarPayables.SupplierInvoiceNo = "0";
                    objCVarPayables.EntryDate = DateTime.Now;
                    objCVarPayables.BillID = 0;

                    objCVarPayables.IssueDate = DateTime.Now;
                    objCVarPayables.OperationContainersAndPackagesID = 0;

                    objCVarPayables.ExchangeRate = insertShipmentAWBData.pExchangeRate;
                    objCVarPayables.CurrencyID = insertShipmentAWBData.pCurrencyID;
                    objCVarPayables.GeneratingQRID = 0;
                    objCVarPayables.Notes = "0";
                    objCVarPayables.CustodyID = 0;
                    objCVarPayables.SupplierReceiptNo = "0";
                    objCVarPayables.AccNoteID = 0;
                    objCVarPayables.IsDeleted = false;
                    objCVarPayables.IsApproved = false;
                    objCVarPayables.ApprovingUserID = 0;
                    objCVarPayables.CreatorUserID = objCVarPayables.ModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarPayables.CreationDate = objCVarPayables.ModificationDate = DateTime.Now;
                    objCVarPayables.JVID = 0;
                    objCVarPayables.BillTo = 0;
                    objCVarPayables.ReceivableID = 0;
                    objCVarPayables.TaxAmount = 0;
                    CPayables objCPayables = new CPayables();
                    objCPayables.lstCVarPayables.Add(objCVarPayables);
                    objCPayables.SaveMethod(objCPayables.lstCVarPayables);
                }
                #endregion Copy default AirLineChargeTypes

                #region update the Master Operation to set NumberOfHousesConnected Field in DB

                pUpdateClause = " NumberOfHousesConnected = (select COUNT(ID) from Operations where MasterOperationID=" + insertShipmentAWBData.pMasterOperationID.ToString() + ")"; //+(pIsHouseConnected ? " ISNULL(NumberOfHousesConnected, 0) + 1 " : " ISNULL(NumberOfHousesConnected, 0) - 1 ");
                pUpdateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                pUpdateClause += " , ModificationDate = GETDATE() ";
                pUpdateClause += "            WHERE ID = " + insertShipmentAWBData.pMasterOperationID.ToString();
                checkException = objCOperations.UpdateList(pUpdateClause);

                #endregion update the Master Operation to set NumberOfHousesConnected Field in DB
            }

            ////i cancelled it from here to save overload(i already check while uploading)
            //#region create Operations Folder to upload DocsIn
            //objCOperations.GetItem(objCOperations.lstCVarOperations[0].ID);

            ////create new directory
            ////string filePath = System.IO.Path.Combine(HttpRuntime.AppDomainAppPath, "DocsInFiles/" + objCOperations.lstCVarOperations[0].Code);
            //string strNewFolderPath = HttpContext.Current.Server.MapPath("~/") + "DocsInFiles/" + objCOperations.lstCVarOperations[0].Code;
            //if (!Directory.Exists(strNewFolderPath)) 
            //    Directory.CreateDirectory(strNewFolderPath);

            //#endregion create Operations Folder to upload DocsIn

            checkException = objCMaster.GetListPaging(99999, 1, ("WHERE ID=" + insertShipmentAWBData.pMasterOperationID), "ID", out _RowCount);
            checkException = objCHouses.GetListPaging(99999, 1, ("WHERE MasterOperationID=" + insertShipmentAWBData.pMasterOperationID), "ID", out _RowCount);
            return new Object[] { checkException==null ? true : false
                , new JavaScriptSerializer().Serialize(objCMaster.lstCVarvwOperations[0]) //pData[1]
                , new JavaScriptSerializer().Serialize(objCHouses.lstCVarvwOperations) //pData[2]
                , objCVarOperations.ID  //pData[3]
            };
        }

        [HttpGet, HttpPost]
        public object[] UpdateShipmentAWB([FromBody] UpdateShipmentAWBData updateShipmentAWBData)
        {
            Exception checkException = null;
            COperations objCOperations = new COperations();
            CvwOperations objCMaster = new CvwOperations();
            CvwOperations objCHouses = new CvwOperations();
            int _RowCount = 0;
            COperationPartners objCOperationPartners = new COperationPartners();
            CRoutings objCRoutings = new CRoutings();
            var MainCarraigeRoutingTypeID = 30;

            var constCustomerPartnerTypeID = 1;
            var constAgentPartnerTypeID = 2;

            var constShipperOperationPartnerTypeID = 1;
            var constConsigneeOperationPartnerTypeID = 2;
            var constNotify1OperationPartnerTypeID = 4;
            var constAgentOperationPartnerTypeID = 6;

            string pUpdateClause = "";

            //pUpdateClause += " HouseNumber=" + (updateShipmentAWBData.pHouseNumber == "0" ? "NULL" : ("'" + updateShipmentAWBData.pHouseNumber + "'")) + " \n";
            //pUpdateClause += ", MasterBL=" + (updateShipmentAWBData.pMasterBL == "0" ? "NULL" : ("'" + updateShipmentAWBData.pMasterBL + "'")) + " \n";
            pUpdateClause += " ShipperID=" + (updateShipmentAWBData.pShipperID == 0 ? "NULL" : updateShipmentAWBData.pShipperID.ToString()) + " \n";
            pUpdateClause += ", ConsigneeID=" + (updateShipmentAWBData.pConsigneeID == 0 ? "NULL" : updateShipmentAWBData.pConsigneeID.ToString()) + " \n";
            pUpdateClause += ", AgentID=" + (updateShipmentAWBData.pAgentID == 0 ? "NULL" : updateShipmentAWBData.pAgentID.ToString()) + " \n";
            pUpdateClause += ", POrC=" + (updateShipmentAWBData.pPOrC == 0 ? "NULL" : updateShipmentAWBData.pPOrC.ToString()) + " \n";
            pUpdateClause += ", MoveTypeID=" + (updateShipmentAWBData.pMoveTypeID == 0 ? "NULL" : updateShipmentAWBData.pMoveTypeID.ToString()) + " \n";
            pUpdateClause += ", CommodityID=" + (updateShipmentAWBData.pCommodityID == 0 ? "NULL" : updateShipmentAWBData.pCommodityID.ToString()) + " \n";
            pUpdateClause += ", POLCountryID=" + (updateShipmentAWBData.pPOLCountryID == 0 ? "NULL" : updateShipmentAWBData.pPOLCountryID.ToString()) + " \n";
            pUpdateClause += ", POL=" + (updateShipmentAWBData.pPOL == 0 ? "NULL" : updateShipmentAWBData.pPOL.ToString()) + " \n";
            pUpdateClause += ", PODCountryID=" + (updateShipmentAWBData.pPODCountryID == 0 ? "NULL" : updateShipmentAWBData.pPODCountryID.ToString()) + " \n";
            pUpdateClause += ", POD=" + (updateShipmentAWBData.pPOD == 0 ? "NULL" : updateShipmentAWBData.pPOD.ToString()) + " \n";
            pUpdateClause += ", AirlineID=" + (updateShipmentAWBData.pAirlineID == 0 ? "NULL" : updateShipmentAWBData.pAirlineID.ToString()) + " \n";
            pUpdateClause += ", MAWBStockID=" + (updateShipmentAWBData.pMAWBStockID == 0 ? "NULL" : updateShipmentAWBData.pMAWBStockID.ToString()) + " \n";
            pUpdateClause += ", MAWBSuffix=" + (updateShipmentAWBData.pMAWBSuffix == "0" ? "NULL" : "N'" + updateShipmentAWBData.pMAWBSuffix.ToString() + "'") + " \n";
            if (updateShipmentAWBData.pMAWBSuffix == "0")
                pUpdateClause += ", MasterBL=NULL";
            else
                pUpdateClause += ", MasterBL=(SELECT Prefix from Airlines WHERE ID=" + updateShipmentAWBData.pAirlineID + ")+ '-" + updateShipmentAWBData.pMAWBSuffix + "'" + " \n";
            pUpdateClause += ", Via1=" + (updateShipmentAWBData.pVia1 == 0 ? "NULL" : updateShipmentAWBData.pVia1.ToString()) + " \n";
            pUpdateClause += ", Via2=" + (updateShipmentAWBData.pVia2 == 0 ? "NULL" : updateShipmentAWBData.pVia2.ToString()) + " \n";
            pUpdateClause += ", Via3=" + (updateShipmentAWBData.pVia3 == 0 ? "NULL" : updateShipmentAWBData.pVia3.ToString()) + " \n";
            pUpdateClause += ", AirLineID1=" + (updateShipmentAWBData.pAirLineID1 == 0 ? "NULL" : updateShipmentAWBData.pAirLineID1.ToString()) + " \n";
            pUpdateClause += ", AirLineID2=" + (updateShipmentAWBData.pAirLineID2 == 0 ? "NULL" : updateShipmentAWBData.pAirLineID2.ToString()) + " \n";
            pUpdateClause += ", AirLineID3=" + (updateShipmentAWBData.pAirLineID3 == 0 ? "NULL" : updateShipmentAWBData.pAirLineID3.ToString()) + " \n";
            pUpdateClause += ", FlightNo1=" + (updateShipmentAWBData.pFlightNo1 == "0" ? "NULL" : "N'" + updateShipmentAWBData.pFlightNo1.ToString() + "'") + " \n";
            pUpdateClause += ", FlightNo2=" + (updateShipmentAWBData.pFlightNo2 == "0" ? "NULL" : "N'" + updateShipmentAWBData.pFlightNo2.ToString() + "'") + " \n";
            pUpdateClause += ", FlightNo3=" + (updateShipmentAWBData.pFlightNo3 == "0" ? "NULL" : "N'" + updateShipmentAWBData.pFlightNo3.ToString() + "'") + " \n";
            pUpdateClause += ", FlightDate1=" + (updateShipmentAWBData.pFlightDate1 == "0" ? "NULL" : ("'" + DateTime.ParseExact(updateShipmentAWBData.pFlightDate1 + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture).ToString("yyyyMMdd") + "'"));
            pUpdateClause += ", FlightDate2=" + (updateShipmentAWBData.pFlightDate2 == "0" ? "NULL" : ("'" + DateTime.ParseExact(updateShipmentAWBData.pFlightDate2 + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture).ToString("yyyyMMdd") + "'"));
            pUpdateClause += ", FlightDate3=" + (updateShipmentAWBData.pFlightDate3 == "0" ? "NULL" : ("'" + DateTime.ParseExact(updateShipmentAWBData.pFlightDate3 + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture).ToString("yyyyMMdd") + "'"));

            pUpdateClause += " , IMOClass = " + (updateShipmentAWBData.pIMOClass == Decimal.Zero ? " NULL " : updateShipmentAWBData.pIMOClass.ToString());
            pUpdateClause += " , UNNumber = " + (updateShipmentAWBData.pUNNumber == 0 ? " NULL " : updateShipmentAWBData.pUNNumber.ToString());
            pUpdateClause += " , VesselID = " + (updateShipmentAWBData.pVesselID == 0 ? " NULL " : updateShipmentAWBData.pVesselID.ToString());

            pUpdateClause += ", UNOrID=" + (updateShipmentAWBData.pUNOrID == "0" ? "NULL" : "N'" + updateShipmentAWBData.pUNOrID.ToString() + "'") + " \n";
            pUpdateClause += ", ProperShippingName=" + (updateShipmentAWBData.pProperShippingName == "0" ? "NULL" : "N'" + updateShipmentAWBData.pProperShippingName.ToString() + "'") + " \n";
            pUpdateClause += ", ClassOrDivision=" + (updateShipmentAWBData.pClassOrDivision == "0" ? "NULL" : "N'" + updateShipmentAWBData.pClassOrDivision.ToString() + "'") + " \n";
            pUpdateClause += ", PackingGroup=" + (updateShipmentAWBData.pPackingGroup == "0" ? "NULL" : "N'" + updateShipmentAWBData.pPackingGroup.ToString() + "'") + " \n";
            pUpdateClause += ", QuantityAndTypeOfPacking=" + (updateShipmentAWBData.pQuantityAndTypeOfPacking == "0" ? "NULL" : "N'" + updateShipmentAWBData.pQuantityAndTypeOfPacking.ToString() + "'") + " \n";
            pUpdateClause += ", PackingInstruction=" + (updateShipmentAWBData.pPackingInstruction == "0" ? "NULL" : "N'" + updateShipmentAWBData.pPackingInstruction.ToString() + "'") + " \n";
            pUpdateClause += ", ShippingDeclarationAuthorization=" + (updateShipmentAWBData.pShippingDeclarationAuthorization == "0" ? "NULL" : "N'" + updateShipmentAWBData.pShippingDeclarationAuthorization.ToString() + "'") + " \n";
            pUpdateClause += ", Barcode=" + (updateShipmentAWBData.pBarcode == "0" ? "NULL" : "N'" + updateShipmentAWBData.pBarcode.ToString() + "'") + " \n";

            pUpdateClause += ", HandlingInformation=" + (updateShipmentAWBData.pHandlingInformation == "0" ? "NULL" : "N'" + updateShipmentAWBData.pHandlingInformation.ToString() + "'") + " \n";
            pUpdateClause += ", Description=" + (updateShipmentAWBData.pDescription == "0" ? "NULL" : "N'" + updateShipmentAWBData.pDescription.ToString() + "'") + " \n";
            pUpdateClause += ", AmountOfInsurance=" + (updateShipmentAWBData.pAmountOfInsurance == "0" ? "NULL" : "N'" + updateShipmentAWBData.pAmountOfInsurance.ToString() + "'") + " \n";
            pUpdateClause += ", BLDate=" + (updateShipmentAWBData.pBLDate == "0" ? "NULL" : ("'" + DateTime.ParseExact(updateShipmentAWBData.pBLDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture).ToString("yyyyMMdd") + "'"));
            pUpdateClause += ", DeclaredValueForCarriage=" + (updateShipmentAWBData.pDeclaredValueForCarriage == "0" ? "NULL" : "N'" + updateShipmentAWBData.pDeclaredValueForCarriage.ToString() + "'") + " \n";
            pUpdateClause += ", WeightCharge=" + (updateShipmentAWBData.pWeightCharge == 0 ? "NULL" : "N'" + updateShipmentAWBData.pWeightCharge.ToString() + "'") + " \n";
            pUpdateClause += ", ValuationCharge=" + (updateShipmentAWBData.pValuationCharge == 0 ? "NULL" : "N'" + updateShipmentAWBData.pValuationCharge.ToString() + "'") + " \n";
            pUpdateClause += ", Tax=" + (updateShipmentAWBData.pTax == 0 ? "NULL" : "N'" + updateShipmentAWBData.pTax.ToString() + "'") + " \n";
            pUpdateClause += ", OtherChargesDueAgent=" + (updateShipmentAWBData.pOtherChargesDueAgent == 0 ? "NULL" : "N'" + updateShipmentAWBData.pOtherChargesDueAgent.ToString() + "'") + " \n";
            pUpdateClause += ", OtherChargesDueCarrier=" + (updateShipmentAWBData.pOtherChargesDueCarrier == 0 ? "NULL" : "N'" + updateShipmentAWBData.pOtherChargesDueCarrier.ToString() + "'") + " \n";
            pUpdateClause += ", OtherCharges=" + (updateShipmentAWBData.pOtherCharges == "0" ? "NULL" : "N'" + updateShipmentAWBData.pOtherCharges.ToString() + "'") + " \n";
            pUpdateClause += ", CurrencyID=" + (updateShipmentAWBData.pCurrencyID == 0 ? "NULL" : updateShipmentAWBData.pCurrencyID.ToString()) + " \n";
            pUpdateClause += ", AccountingInformation=" + (updateShipmentAWBData.pAccountingInformation == "0" ? "NULL" : "N'" + updateShipmentAWBData.pAccountingInformation.ToString() + "'") + " \n";
            pUpdateClause += ", ReferenceNumber=" + (updateShipmentAWBData.pReferenceNumber == "0" ? "NULL" : "N'" + updateShipmentAWBData.pReferenceNumber.ToString() + "'") + " \n";
            pUpdateClause += ", OptionalShippingInformation=" + (updateShipmentAWBData.pOptionalShippingInformation == "0" ? "NULL" : "N'" + updateShipmentAWBData.pOptionalShippingInformation.ToString() + "'") + " \n";
            pUpdateClause += ", CHGSCode=" + (updateShipmentAWBData.pCHGSCode == "0" ? "NULL" : "N'" + updateShipmentAWBData.pCHGSCode.ToString() + "'") + " \n";
            pUpdateClause += ", WT_VALL=" + (updateShipmentAWBData.pWT_VALL == "0" ? "NULL" : "N'" + updateShipmentAWBData.pWT_VALL.ToString() + "'") + " \n";
            pUpdateClause += ", WT_VALL_Other=" + (updateShipmentAWBData.pWT_VALL_Other == "0" ? "NULL" : "N'" + updateShipmentAWBData.pWT_VALL_Other.ToString() + "'") + " \n";
            pUpdateClause += ", DeclaredValueForCustoms=" + (updateShipmentAWBData.pDeclaredValueForCustoms == "0" ? "NULL" : "N'" + updateShipmentAWBData.pDeclaredValueForCustoms.ToString() + "'") + " \n";
            //pUpdateClause += ", ChargeableWeight=" + (updateShipmentAWBData.pChargeableWeight == "0" ? "NULL" : "N'" + updateShipmentAWBData.pChargeableWeight.ToString() + "'") + " \n";
            pUpdateClause += ", TypeOfStockID=" + (updateShipmentAWBData.pTypeOfStockID == 0 ? "NULL" : "N'" + updateShipmentAWBData.pTypeOfStockID.ToString() + "'") + " \n";
            pUpdateClause += ", FlightNo=" + (updateShipmentAWBData.pFlightNo == "0" ? "NULL" : "N'" + updateShipmentAWBData.pFlightNo.ToString() + "'") + " \n";
            pUpdateClause += ", IsAWB = " + (updateShipmentAWBData.pIsAWB ? "1" : "0") + " \n";
            pUpdateClause += " WHERE ID=" + updateShipmentAWBData.pOperationID.ToString() + " \n";
            checkException = objCOperations.UpdateList(pUpdateClause);

            //Update ports in the houses
            pUpdateClause = " POLCountryID = " + (updateShipmentAWBData.pPOLCountryID == 0 ? "null" : updateShipmentAWBData.pPOLCountryID.ToString());
            pUpdateClause += " , PODCountryID = " + (updateShipmentAWBData.pPODCountryID == 0 ? "null" : updateShipmentAWBData.pPODCountryID.ToString());
            pUpdateClause += " , POL = " + (updateShipmentAWBData.pPOL == 0 ? "null" : updateShipmentAWBData.pPOL.ToString());
            pUpdateClause += " , POD = " + (updateShipmentAWBData.pPOD == 0 ? "null" : updateShipmentAWBData.pPOD.ToString());
            pUpdateClause += " , AirlineID = " + (updateShipmentAWBData.pAirlineID == 0 ? "null" : updateShipmentAWBData.pAirlineID.ToString());
            pUpdateClause += " WHERE MasterOperationID = " + updateShipmentAWBData.pOperationID.ToString();
            checkException = objCOperations.UpdateList(pUpdateClause);

            #region Update Partners
            //Update Shipper
            pUpdateClause = "CustomerID=" + (updateShipmentAWBData.pShipperID == 0 ? "NULL" : updateShipmentAWBData.pShipperID.ToString()) + "\n";
            pUpdateClause += ", ContactID=(select min(ID) from Contacts where PartnerTypeID=" + constCustomerPartnerTypeID + " and PartnerID=" + updateShipmentAWBData.pShipperID + ")" + "\n";
            pUpdateClause += " WHERE OperationID=" + updateShipmentAWBData.pOperationID.ToString() + " AND OperationPartnerTypeID=" + constShipperOperationPartnerTypeID;
            checkException = objCOperationPartners.UpdateList(pUpdateClause);
            //Update Consignee
            pUpdateClause = "CustomerID=" + (updateShipmentAWBData.pConsigneeID == 0 ? "NULL" : updateShipmentAWBData.pConsigneeID.ToString()) + "\n";
            pUpdateClause += ", ContactID=(select min(ID) from Contacts where PartnerTypeID=" + constCustomerPartnerTypeID + " and PartnerID=" + updateShipmentAWBData.pConsigneeID + ")" + "\n";
            pUpdateClause += " WHERE OperationID=" + updateShipmentAWBData.pOperationID.ToString() + " AND OperationPartnerTypeID=" + constConsigneeOperationPartnerTypeID;
            checkException = objCOperationPartners.UpdateList(pUpdateClause);
            //Update Agent
            pUpdateClause = "AgentID=" + (updateShipmentAWBData.pAgentID == 0 ? "NULL" : updateShipmentAWBData.pAgentID.ToString()) + "\n";
            pUpdateClause += ", ContactID=(select min(ID) from Contacts where PartnerTypeID=" + constAgentPartnerTypeID + " and PartnerID=" + updateShipmentAWBData.pAgentID + ")" + "\n";
            pUpdateClause += " WHERE OperationID=" + updateShipmentAWBData.pOperationID.ToString() + " AND OperationPartnerTypeID=" + constAgentOperationPartnerTypeID;
            checkException = objCOperationPartners.UpdateList(pUpdateClause);
            //Update Notify
            pUpdateClause = "CustomerID=" + (updateShipmentAWBData.pNotifyID == 0 ? "NULL" : updateShipmentAWBData.pNotifyID.ToString()) + "\n";
            pUpdateClause += ", ContactID=(select min(ID) from Contacts where PartnerTypeID=" + constCustomerPartnerTypeID + " and PartnerID=" + updateShipmentAWBData.pNotifyID + ")" + "\n";
            pUpdateClause += " WHERE OperationID=" + updateShipmentAWBData.pOperationID.ToString() + " AND OperationPartnerTypeID=" + constNotify1OperationPartnerTypeID;
            checkException = objCOperationPartners.UpdateList(pUpdateClause);
            #endregion Update Partners

            #region Update MainRoute
            pUpdateClause = " POLCountryID=" + (updateShipmentAWBData.pPOLCountryID == 0 ? "NULL" : updateShipmentAWBData.pPOLCountryID.ToString()) + " \n";
            pUpdateClause += ", POL=" + (updateShipmentAWBData.pPOL == 0 ? "NULL" : updateShipmentAWBData.pPOL.ToString()) + " \n";
            pUpdateClause += ", PODCountryID=" + (updateShipmentAWBData.pPODCountryID == 0 ? "NULL" : updateShipmentAWBData.pPODCountryID.ToString()) + " \n";
            pUpdateClause += ", POD=" + (updateShipmentAWBData.pPOD == 0 ? "NULL" : updateShipmentAWBData.pPOD.ToString()) + " \n";
            pUpdateClause += ", AirlineID=" + (updateShipmentAWBData.pAirlineID == 0 ? "NULL" : updateShipmentAWBData.pAirlineID.ToString()) + " \n";
            pUpdateClause += " WHERE OperationID in (SELECT ID FROM Operations WHERE MasterOperationID IN (" + updateShipmentAWBData.pOperationID.ToString() + "))" + " AND RoutingTypeID=" + MainCarraigeRoutingTypeID.ToString() + " \n";
            checkException = objCRoutings.UpdateList(pUpdateClause);
            #endregion Update MainRoute

            #region Update Payables Currency & ExchangeRate
            pUpdateClause = " CurrencyID=" + (updateShipmentAWBData.pCurrencyID == 0 ? "NULL" : updateShipmentAWBData.pCurrencyID.ToString()) + " \n";
            pUpdateClause += ", ExchangeRate=" + (updateShipmentAWBData.pExchangeRate == 0 ? "0" : updateShipmentAWBData.pExchangeRate.ToString()) + " \n";
            pUpdateClause += " WHERE OperationID=" + updateShipmentAWBData.pOperationID.ToString();
            CPayables objCPayables = new CPayables();
            checkException = objCPayables.UpdateList(pUpdateClause);
            #endregion Update Payables Currency & ExchangeRate

            checkException = objCMaster.GetListPaging(99999, 1, ("WHERE ID=" + updateShipmentAWBData.pMasterOperationID), "ID", out _RowCount);
            checkException = objCHouses.GetListPaging(99999, 1, ("WHERE MasterOperationID=" + updateShipmentAWBData.pMasterOperationID), "ID", out _RowCount);
            return new Object[] { checkException==null ? true : false
                , new JavaScriptSerializer().Serialize(objCMaster.lstCVarvwOperations[0]) //pData[1]
                , new JavaScriptSerializer().Serialize(objCHouses.lstCVarvwOperations) //pData[2]
            };
        }
        #endregion AWB

        [HttpGet, HttpPost]
        public Object[] GetMasterOperationsByCode(string term)
        {
            var pOperationCode = term;

            var srialize = new JavaScriptSerializer();
            srialize.MaxJsonLength = int.MaxValue;

            COperations objCOperations = new COperations();
            Int32 _RowCount = 1;
            string whereClause = " Where CodeSerial = " + pOperationCode + " AND BLType=" + 3;
            objCOperations.GetListPaging(10000, 1, whereClause, "ID", out _RowCount);

            return new Object[]
            {
                srialize.Serialize(objCOperations.lstCVarOperations), //0
            };


        }

        
        //[HttpPost()]
        //public string UploadFiles()
        //{
        //    int iUploadedCnt = 0;

        //    // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
        //    string sPath = "";
        //    sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/DocsInFiles/");
        //    //sPath = HttpContext.Current.Server.MapPath("~/") + "DocsInFiles";
        //    System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

        //    // CHECK THE FILE COUNT.
        //    for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
        //    {
        //        System.Web.HttpPostedFile hpf = hfc[iCnt];

        //        if (hpf.ContentLength > 0)
        //        {
        //            // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
        //            if (!System.IO.File.Exists(sPath + Path.GetFileName(hpf.FileName)))
        //            {
        //                // SAVE THE FILES IN THE FOLDER.
        //                hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
        //                iUploadedCnt = iUploadedCnt + 1;
        //            }
        //        }
        //    }

        //    // RETURN A MESSAGE (OPTIONAL).
        //    if (iUploadedCnt > 0)
        //    {
        //        return iUploadedCnt + " Files Uploaded Successfully";
        //    }
        //    else
        //    {
        //        return "Upload Failed";
        //    }
        //}

    }
    public class SetCertificateNumberData
    {
        public string pOperationIDList { get; set; }
        public string pCertificateNumberList { get; set; }
        public string pCountryOfOriginList { get; set; }
        public string pInvoiceValueList { get; set; }
        public string pCommodityIDList { get; set; }
        public string pCurrencyIDList { get; set; }
    }

    public class SetShipmentDatesData
    {
        public string pOperationIDList { get; set; }
        public string pExpectedDepartureList { get; set; }
        public string pActualDepartureList { get; set; }
        public string pExpectedArrivalList { get; set; }
        public string pActualArrivalList { get; set; }
        public string pIsClearanceList { get; set; }
    }
    public class InsertOperationData
    {
        public bool pIsShipment { get; set; }
        public string pCode { get; set; }
        public string pHouseNumber { get; set; }
        public string pBranchID { get; set; }
        public string pSalesmanID { get; set; }
        public string pBLTypeIconName { get; set; }
        public string pBLTypeIconStyle { get; set; }
        public string pBLType { get; set; }
        public string pDirectionType { get; set; }
        public string pDirectionIconName { get; set; }
        public string pDirectionIconStyle { get; set; }
        public string pTransportType { get; set; }
        public string pTransportIconName { get; set; }
        public string pTransportIconStyle { get; set; }
        public string pShipmentType { get; set; }
        public string pMasterBL { get; set; }
        public string pShipperID { get; set; }
        public string pShipperAddressID { get; set; }
        public string pShipperContactID { get; set; }
        public string pConsigneeID { get; set; }
        public string pConsigneeAddressID { get; set; }
        public string pConsigneeContactID { get; set; }
        public string pNotifyID { get; set; }
        public string pAgentID { get; set; }
        public string pAgentAddressID { get; set; }
        public string pAgentContactID { get; set; }
        public string pIncotermID { get; set; }
        public int pPOrC { get; set; }
        public string pMoveTypeID { get; set; }
        public string pCommodityID { get; set; }
        public string pTransientTime { get; set; }
        //public string pOpenDate { get; set; }
        public DateTime pOpenDate { get; set; }
        public DateTime pCloseDate { get; set; }
        public string pCutOffDate { get; set; }
        public string pIncludePickup { get; set; }
        public string pPickupCityID { get; set; }
        public string pPickupAddressID { get; set; }
        public string pPOLCountryID { get; set; }
        public string pPOL { get; set; }
        public string pPODCountryID { get; set; }
        public string pPOD { get; set; }
        public string pShippingLineID { get; set; }
        public string pAirlineID { get; set; }
        public string pTruckerID { get; set; }
        public string pIncludeDelivery { get; set; }
        public string pDeliveryZipCode { get; set; }
        public string pDeliveryCityID { get; set; }
        public string pDeliveryCountryID { get; set; }
        public string pNetWeight { get; set; }
        public string pGrossWeight { get; set; }
        public string pVolume { get; set; }
        public string pChargeableWeight { get; set; }
        public string pPackageTypeID { get; set; }
        public string pNumberOfPackages { get; set; }
        public string pIsDangerousGoods { get; set; }
        public string pNotes { get; set; }
        public string pCustomerReference { get; set; }
        public string pSupplierReference { get; set; }
        public string pPONumber { get; set; }
        public string pAgreedRate { get; set; }
        public string pOperationStageID { get; set; }
        public string pNumberOfHousesConnected { get; set; }
        /**********************    TruckingOrder fields*********************************/
        public bool pIsDelivered { get; set; }
        public bool pIsTrucking { get; set; }
        public bool pIsInsurance { get; set; }
        public bool pIsClearance { get; set; }
        public bool pIsGenset { get; set; }
        public bool pIsCourrier { get; set; }
        public bool pIsTelexRelease { get; set; }
        /**********************EOF TruckingOrder fields*********************************/
        public string pExpectedDeparture { get; set; }
        public string pExpectedArrival { get; set; }
        public string pVoyageOrTruckNumber { get; set; }
        public Int32 pVesselID { get; set; }
        public Int32 pContainerTypeID { get; set; }
        public Int32 pNumberOfContainers { get; set; }
        public Int32 pContainerTypeID2 { get; set; }
        public Int32 pNumberOfContainers2 { get; set; }
        public Int32 pContainerTypeID3 { get; set; }
        public Int32 pNumberOfContainers3 { get; set; }
        public Int32 pConsigneeID2 { get; set; }
        public string pReleaseDate { get; set; }
        /**********************Venus parameters(A.Medra)*********************************/
        public string pBLDate { get; set; }
        public Int32 pMAWBStockID { get; set; }
        public Int32 pTypeOfStockID { get; set; }
        public string pFlightNo { get; set; }
        public string pMAWBSuffix { get; set; }
        public bool pIsAWB { get; set; }
        //Clearance
        public string pCertificateNumber { get; set; }
        public string pCountryOfOrigin { get; set; }
        public string pInvoiceValue { get; set; }
        public Int32 pCurrencyID { get; set; }
        public string pACIDNumber { get; set; }
        public string pACIDNumberDetails { get; set; }
        public string pBookingNumber { get; set; }
        public int pUNNumber { get; set; }
        public decimal pIMOClass { get; set; }
    }
    public class UpdateOperationData
    {
        public Int64 pID { get; set; }

        public Int64 pQuotationRouteID { get; set; } //has value incase of built on a Quotation
        public int pCodeSerial { get; set; }
        //public string pCode { get; set; }
        public string pHouseNumber { get; set; }
        public int pBranchID { get; set; }
        public int pSalesmanID { get; set; }
        public int pOperationManID { get; set; }
        public int pBLType { get; set; }
        public string pBLTypeIconName { get; set; }
        public string pBLTypeIconStyle { get; set; }
        public int pDirectionType { get; set; }
        public string pDirectionIconName { get; set; }
        public string pDirectionIconStyle { get; set; }
        public int pTransportType { get; set; }
        public string pTransportIconName { get; set; }
        public string pTransportIconStyle { get; set; }
        public int pShipmentType { get; set; }
        public string pMasterBL { get; set; }
        public string pMAWBSuffix { get; set; }
        public DateTime pBLDate { get; set; }
        public DateTime pHBLDate { get; set; }
        public int pVia1 { get; set; }
        public int pVia2 { get; set; }
        public int pVia3 { get; set; }
        public Int64 pMAWBStockID { get; set; }
        public int pShipperID { get; set; }
        public Int64 pShipperAddressID { get; set; }
        public Int64 pShipperContactID { get; set; }
        public int pConsigneeID { get; set; }
        public Int64 pConsigneeAddressID { get; set; }
        public Int64 pConsigneeContactID { get; set; }
        public int pAgentID { get; set; }
        public Int64 pAgentAddressID { get; set; }
        public Int64 pAgentContactID { get; set; }
        public int pIncotermID { get; set; }
        public int pPOrC { get; set; }
        public int pMoveTypeID { get; set; }
        public int pCommodityID { get; set; }
        public int pCommodityID2 { get; set; }
        public int pCommodityID3 { get; set; }
        public int pTransientTime { get; set; }
        public string pOpenDate { get; set; }
        public string pCloseDate { get; set; }
        public DateTime pCloseDateAsDateTime { get; set; }/////////////////////////////////////////////////////////
        public string pCutOffDate { get; set; }
        public bool pIncludePickup { get; set; }
        public int pPickupCityID { get; set; }
        public int pPickupAddressID { get; set; }
        public int pPOLCountryID { get; set; }
        public int pPOL { get; set; }
        public int pPODCountryID { get; set; }
        public int pPOD { get; set; }
        public int pShippingLineID { get; set; }
        public int pAirlineID { get; set; }
        public int pTruckerID { get; set; }
        public bool pIncludeDelivery { get; set; }
        public string pDeliveryZipCode { get; set; }
        public int pDeliveryCityID { get; set; }
        public int pDeliveryCountryID { get; set; }
        public Decimal pGrossWeight { get; set; }
        public Decimal pVolume { get; set; }
        public Decimal pChargeableWeight { get; set; }
        public int pNumberOfPackages { get; set; }
        public bool pIsDangerousGoods { get; set; }
        public string pNotes { get; set; }
        public string pCustomerReference { get; set; }
        public string pSupplierReference { get; set; }

        public string pPONumber { get; set; }
        public string pPODate { get; set; }
        public string pPOValue { get; set; }
        public string pReleaseNumber { get; set; }
        public string pReleaseDate { get; set; }
        public string pForm13Date { get; set; }
        public decimal pReleaseValue { get; set; }
        public string pDispatchNumber { get; set; }
        public string pBusinessUnit { get; set; }
        public string pForm13Number { get; set; }
        public string pACIDNumber { get; set; }
        public string pACIDNumberDetails { get; set; }
        public string pBookingNumber { get; set; }
        public int pUNNumber { get; set; }
        public decimal pIMOClass { get; set; }

        public string pAgreedRate { get; set; }
        public int pOperationStageID { get; set; }
        public int pMasterOperationID { get; set; }
        public int pNumberOfHousesConnected { get; set; }

        /**********************    TruckingOrder fields*********************************/
        public bool pIsDelivered { get; set; }
        public bool pIsTrucking { get; set; }
        public bool pIsInsurance { get; set; }
        public bool pIsClearance { get; set; }
        public bool pIsGenset { get; set; }
        public bool pIsCourrier { get; set; }
        public bool pIsTelexRelease { get; set; }
        /**********************EOF TruckingOrder fields*********************************/
        public int pNetworkID { get; set; }
        public int pNumberOfOriginalBills { get; set; }
        public int pVesselID { get; set; }
    }
    public class InsertShipmentAWBData
    {
        public bool pIsShipment { get; set; }
        public int pCodeSerial { get; set; }
        public string pCode { get; set; }
        public string pHouseNumber { get; set; }
        public string pBranchID { get; set; }
        public string pSalesmanID { get; set; }
        public string pBLType { get; set; }
        public string pBLTypeIconName { get; set; }
        public string pBLTypeIconStyle { get; set; }
        public string pDirectionType { get; set; }
        public string pDirectionIconName { get; set; }
        public string pDirectionIconStyle { get; set; }
        public string pTransportType { get; set; }
        public string pTransportIconName { get; set; }
        public string pTransportIconStyle { get; set; }
        public string pShipmentType { get; set; }
        public string pMasterBL { get; set; }
        public string pShipperID { get; set; }
        public string pShipperAddressID { get; set; }
        public string pShipperContactID { get; set; }
        public string pConsigneeID { get; set; }
        public string pConsigneeAddressID { get; set; }
        public string pConsigneeContactID { get; set; }
        public string pNotifyID { get; set; }
        public string pAgentID { get; set; }
        public string pAgentAddressID { get; set; }
        public string pAgentContactID { get; set; }
        public string pIncotermID { get; set; }
        public Int32 pPOrC { get; set; }
        public string pMoveTypeID { get; set; }
        public string pCommodityID { get; set; }
        public string pTransientTime { get; set; }
        //public string pOpenDate { get; set; }
        public string pOpenDate { get; set; }
        public string pCloseDate { get; set; }
        public string pCutOffDate { get; set; }
        public string pIncludePickup { get; set; }
        public string pPickupCityID { get; set; }
        public string pPickupAddressID { get; set; }
        public string pPOLCountryID { get; set; }
        public string pPOL { get; set; }
        public string pPODCountryID { get; set; }
        public string pPOD { get; set; }
        public string pShippingLineID { get; set; }
        public string pAirlineID { get; set; }
        public string pTruckerID { get; set; }
        public string pIncludeDelivery { get; set; }
        public string pDeliveryZipCode { get; set; }
        public string pDeliveryCityID { get; set; }
        public string pDeliveryCountryID { get; set; }
        public string pNetWeight { get; set; }
        public string pGrossWeight { get; set; }
        public string pVolume { get; set; }
        public string pPackageTypeID { get; set; }
        public string pNumberOfPackages { get; set; }
        public string pIsDangerousGoods { get; set; }
        public string pNotes { get; set; }
        public string pOperationStageID { get; set; }
        public string pNumberOfHousesConnected { get; set; }
        public Int64 pMAWBStockID { get; set; }
        public Int64 pMasterOperationID { get; set; }
        public string pMAWBSuffix { get; set; }
        public Int32 pVia1 { get; set; }
        public Int32 pVia2 { get; set; }
        public Int32 pVia3 { get; set; }
        public Int32 pAirLineID1 { get; set; }
        public String pFlightNo1 { get; set; }
        public string pFlightDate1 { get; set; }
        public Int32 pAirLineID2 { get; set; }
        public String pFlightNo2 { get; set; }
        public string pFlightDate2 { get; set; }
        public Int32 pAirLineID3 { get; set; }
        public String pFlightNo3 { get; set; }
        public string pFlightDate3 { get; set; }

        public String pUNOrID { get; set; }
        public String pProperShippingName { get; set; }
        public String pClassOrDivision { get; set; }
        public String pPackingGroup { get; set; }
        public String pQuantityAndTypeOfPacking { get; set; }
        public String pPackingInstruction { get; set; }
        public String pShippingDeclarationAuthorization { get; set; }
        public String pBarcode { get; set; }

        public String pHandlingInformation { get; set; }
        public String pDescription { get; set; }
        public String pAmountOfInsurance { get; set; }
        public string pBLDate { get; set; }
        public String pDeclaredValueForCarriage { get; set; }
        public Decimal pWeightCharge { get; set; }
        public Decimal pValuationCharge { get; set; }
        public Decimal pTax { get; set; }
        public Decimal pOtherChargesDueAgent { get; set; }
        public Decimal pOtherChargesDueCarrier { get; set; }
        public String pOtherCharges { get; set; }
        public Int32 pCurrencyID { get; set; }
        public Decimal pExchangeRate { get; set; }
        public String pAccountingInformation { get; set; }
        public String pReferenceNumber { get; set; }
        public String pOptionalShippingInformation { get; set; }
        public String pCHGSCode { get; set; }
        public String pWT_VALL { get; set; }
        public String pWT_VALL_Other { get; set; }
        public String pDeclaredValueForCustoms { get; set; }
        public string pChargeableWeight { get; set; }
        public Int32 pTypeOfStockID { get; set; }
        public String pFlightNo { get; set; }

        public bool pIsAWB { get; set; }
        public int pUNNumber { get; set; }
        public decimal pIMOClass { get; set; }
        public int pVesselID { get; set; }
    }
    public class UpdateShipmentAWBData
    {
        public Int64 pOperationID { get; set; }
        public bool pIsShipment { get; set; }
        public string pHouseNumber { get; set; }
        //public string pBranchID { get; set; }
        //public string pSalesmanID { get; set; }

        public string pMasterBL { get; set; }
        public Int32 pShipperID { get; set; }
        public Int32 pConsigneeID { get; set; }
        public Int32 pAgentID { get; set; }
        public Int32 pNotifyID { get; set; }
        public Int32 pPOrC { get; set; }
        public Int32 pMoveTypeID { get; set; }
        public Int32 pCommodityID { get; set; }
        public Int32 pPOLCountryID { get; set; }
        public Int32 pPOL { get; set; }
        public Int32 pPODCountryID { get; set; }
        public Int32 pPOD { get; set; }
        public Int32 pAirlineID { get; set; }
        public Int32 pOperationStageID { get; set; }
        public Int64 pMAWBStockID { get; set; }
        public Int64 pMasterOperationID { get; set; }
        public string pMAWBSuffix { get; set; }
        public Int32 pVia1 { get; set; }
        public Int32 pVia2 { get; set; }
        public Int32 pVia3 { get; set; }
        public Int32 pAirLineID1 { get; set; }
        public String pFlightNo1 { get; set; }
        public string pFlightDate1 { get; set; }
        public Int32 pAirLineID2 { get; set; }
        public String pFlightNo2 { get; set; }
        public string pFlightDate2 { get; set; }
        public Int32 pAirLineID3 { get; set; }
        public String pFlightNo3 { get; set; }
        public string pFlightDate3 { get; set; }

        public String pUNOrID { get; set; }
        public String pProperShippingName { get; set; }
        public String pClassOrDivision { get; set; }
        public String pPackingGroup { get; set; }
        public String pQuantityAndTypeOfPacking { get; set; }
        public String pPackingInstruction { get; set; }
        public String pShippingDeclarationAuthorization { get; set; }
        public String pBarcode { get; set; }

        public String pHandlingInformation { get; set; }
        public String pDescription { get; set; }
        public String pAmountOfInsurance { get; set; }
        public string pBLDate { get; set; }
        public String pDeclaredValueForCarriage { get; set; }
        public Decimal pWeightCharge { get; set; }
        public Decimal pValuationCharge { get; set; }
        public Decimal pTax { get; set; }
        public Decimal pOtherChargesDueAgent { get; set; }
        public Decimal pOtherChargesDueCarrier { get; set; }
        public String pOtherCharges { get; set; }
        public Int32 pCurrencyID { get; set; }
        public Decimal pExchangeRate { get; set; }
        public String pAccountingInformation { get; set; }
        public String pReferenceNumber { get; set; }
        public String pOptionalShippingInformation { get; set; }
        public String pCHGSCode { get; set; }
        public String pWT_VALL { get; set; }
        public String pWT_VALL_Other { get; set; }
        public String pDeclaredValueForCustoms { get; set; }
        public string pChargeableWeight { get; set; }
        public Int32 pTypeOfStockID { get; set; }
        public String pFlightNo { get; set; }

        public bool pIsAWB { get; set; }
        public decimal pIMOClass { get; set; }
        public int pUNNumber { get; set; }
        public int pVesselID { get; set; }
    }
}
