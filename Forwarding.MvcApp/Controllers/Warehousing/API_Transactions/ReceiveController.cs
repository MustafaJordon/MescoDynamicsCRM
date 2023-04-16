using Forwarding.MvcApp.Entities.Warehousing;
using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;
using Forwarding.MvcApp.Models.NoAccess.Generated;
using Forwarding.MvcApp.Models.Operations.Operations.Generated;
using Forwarding.MvcApp.Models.Warehousing.MasterData.Generated;
using Forwarding.MvcApp.Models.Warehousing.Transactions.Generated;
using System;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebMatrix.WebData;

namespace Forwarding.MvcApp.Controllers.Warehousing.API_Reports
{
    public class ReceiveController : ApiController
    {
        #region Receive
        [HttpGet, HttpPost]
        public object[] LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned(bool pIsLoadArrayOfObjects, string pLanguage, Int32 pPageNumber, Int32 pPageSize, string pWhereClause, string pOrderBy)
        {
            int _RowCount = 0;
            Exception checkException = null;
            CvwWH_Receive objCvwWH_Receive = new CvwWH_Receive();
            CWH_Warehouse objCWH_Warehouse = new CWH_Warehouse();
            CPurchaseItem objCPurchaseItem = new CPurchaseItem();
            CNoAccessReceiveDetailsStatus objCNoAccessReceiveDetailsStatus = new CNoAccessReceiveDetailsStatus();
            if (pIsLoadArrayOfObjects)
            {
                checkException = objCWH_Warehouse.GetList("ORDER BY Name");
                //checkException = objCPurchaseItem.GetList("ORDER BY Code"); //to improve performance
                checkException = objCNoAccessReceiveDetailsStatus.GetList("WHERE IsInactive=0");
            }
            #region Get Minimal Data
            var pPurchaseItemList = objCPurchaseItem.lstCVarPurchaseItem
                .Select(s => new {
                    ID = s.ID
                    ,
                    Code = s.Code
                    ,
                    Name = s.Name
                })
                .ToList();
            #endregion Get Minimal Data
            checkException = objCvwWH_Receive.GetListPaging(pPageSize, pPageNumber, pWhereClause, pOrderBy, out _RowCount);
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                new JavaScriptSerializer().Serialize(objCvwWH_Receive.lstCVarvwWH_Receive)
                , _RowCount
                , serializer.Serialize(objCWH_Warehouse.lstCVarWH_Warehouse) //pWarehouse=pData[2]
                , serializer.Serialize(objCPurchaseItem.lstCVarPurchaseItem) //pPurchaseItem=pData[3]
                , serializer.Serialize(objCNoAccessReceiveDetailsStatus.lstCVarNoAccessReceiveDetailsStatus) //pWarehouse=pData[4]
            };
        }

        [HttpGet, HttpPost]
        public object[] LoadHeaderWithDetails(Int64 pHeaderID)
        {
            CvwWH_Receive objCvwWH_Receive = new CvwWH_Receive();
            CvwWH_ReceiveDetails objCvwWH_ReceiveDetails = new CvwWH_ReceiveDetails();
            Exception checkException = null;
            int _RowCount = 0;
            checkException = objCvwWH_Receive.GetListPaging(99999, 1, "WHERE ID=" + pHeaderID.ToString(), "ID", out _RowCount);
            checkException = objCvwWH_ReceiveDetails.GetListPaging(99999, 1, "WHERE ReceiveID=" + pHeaderID.ToString(), "ID", out _RowCount);
            return new object[] {
                new JavaScriptSerializer().Serialize(objCvwWH_Receive.lstCVarvwWH_Receive[0]) //pData[0]
                , new JavaScriptSerializer().Serialize(objCvwWH_ReceiveDetails.lstCVarvwWH_ReceiveDetails) //pData[1]
            };
        }

        [HttpGet, HttpPost]
        public object[] Save(Int64 pID, Int32 pWarehouseID, Int32 pCustomerID, string pReceiveDate, string pETD
            , string pETA, string pArrivalDate, Int32 pStatusID, bool pIsFinalized, string pFinalizeDate, string pNotes)
        {
            string _ReturnedMessage = "";
            Exception checkException = null;
            CWH_Receive objCWH_Receive = new CWH_Receive();
            CvwWH_Receive objCvwWH_Receive = new CvwWH_Receive();
            CWH_ReceiveDetails objCWH_ReceiveDetails = new CWH_ReceiveDetails();
            CvwWH_ReceiveDetails objCvwWH_ReceiveDetails = new CvwWH_ReceiveDetails();
            CVarWH_Receive objCVarWH_Receive = new CVarWH_Receive();
            int _RowCount = 0;
            string _UpdateClause = "";
            #region Insert
            if (pID == 0) //Insert
            {
                objCVarWH_Receive.Code = "0";
                objCVarWH_Receive.WarehouseID = pWarehouseID;
                objCVarWH_Receive.CustomerID = pCustomerID;
                objCVarWH_Receive.ReceiveDate = DateTime.ParseExact(pReceiveDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture);
                objCVarWH_Receive.ETD = objCVarWH_Receive.ReceiveDate; //DateTime.ParseExact(pETD + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture);
                objCVarWH_Receive.ETA = objCVarWH_Receive.ReceiveDate; //DateTime.ParseExact(pETA + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture);
                objCVarWH_Receive.ArrivalDate = objCVarWH_Receive.ReceiveDate; //DateTime.ParseExact(pArrivalDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture);
                objCVarWH_Receive.StatusID = pStatusID;
                objCVarWH_Receive.IsFinalized = pIsFinalized;
                objCVarWH_Receive.FinalizeDate = DateTime.ParseExact(pFinalizeDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture);
                objCVarWH_Receive.Notes = pNotes;

                objCVarWH_Receive.InvoiceID = 0;
                objCVarWH_Receive.OperationID = 0;

                objCVarWH_Receive.CreatorUserID = objCVarWH_Receive.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarWH_Receive.CreationDate = objCVarWH_Receive.ModificationDate = DateTime.Now;
                objCWH_Receive.lstCVarWH_Receive.Add(objCVarWH_Receive);
                checkException = objCWH_Receive.SaveMethod(objCWH_Receive.lstCVarWH_Receive);
                if (checkException == null)
                    pID = objCVarWH_Receive.ID;
                else
                    _ReturnedMessage = checkException.Message;
            }
            #endregion Insert
            #region Update
            else //update
            {
                _UpdateClause = pWarehouseID == 0 ? "WarehouseID=null" : ("WarehouseID=" + pWarehouseID) + " \n";
                _UpdateClause += pCustomerID == 0 ? ",CustomerID=null" : (",CustomerID=" + pCustomerID) + " \n";
                _UpdateClause += pReceiveDate == "01/01/1900" ? " ,ReceiveDate = NULL " : (" ,ReceiveDate = '" + (DateTime.ParseExact(pReceiveDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd") + "'");
                _UpdateClause += pReceiveDate == "01/01/1900" ? " ,ETD = NULL " : (" ,ETD = '" + (DateTime.ParseExact(pReceiveDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd") + "'"); //pETD == "01/01/1900" ? " ,ETD = NULL " : (" ,ETD = '" + (DateTime.ParseExact(pETD + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd") + "'");
                _UpdateClause += pReceiveDate == "01/01/1900" ? " ,ETA = NULL " : (" ,ETA = '" + (DateTime.ParseExact(pReceiveDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd") + "'"); //pETA == "01/01/1900" ? " ,ETA = NULL " : (" ,ETA = '" + (DateTime.ParseExact(pETA + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd") + "'");
                _UpdateClause += pReceiveDate == "01/01/1900" ? " ,ArrivalDate = NULL " : (" ,ArrivalDate = '" + (DateTime.ParseExact(pReceiveDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd") + "'"); //pArrivalDate == "01/01/1900" ? " ,ArrivalDate = NULL " : (" ,ArrivalDate = '" + (DateTime.ParseExact(pArrivalDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd") + "'");
                ////Set from the Finalize btn or when update details for putaway
                //_UpdateClause += pStatusID == 0 ? ",StatusID=null" : (",StatusID=" + pStatusID) + " \n";
                //_UpdateClause += " , IsFinalized = " + (pIsFinalized ? "1" : "0") + " \n";
                //_UpdateClause += pFinalizeDate == "01/01/1900" ? " ,FinalizeDate = NULL " : (" ,FinalizeDate = '" + (DateTime.ParseExact(pFinalizeDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd") + "'");
                _UpdateClause += pNotes == "0" ? ",Notes=null" : (",Notes=N'" + pNotes + "'") + " \n";
                
                _UpdateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                _UpdateClause += " , ModificationDate = GETDATE() ";
                _UpdateClause += " WHERE ID = " + pID.ToString();

                checkException = objCWH_Receive.UpdateList(_UpdateClause);
                if (checkException == null)
                {
                    //i think done in details and not header
                    //TODO Update location if used or not used
                    //checkException = objCvwWH_ReceiveDetails.GetListPaging(99999, 1, "WHERE ReceiveID=" + objCVarWH_Receive.ID, "Code", out _RowCount);
                }
                else
                    _ReturnedMessage = checkException.Message;
            }
            #endregion Update
            if (_ReturnedMessage == "")
                checkException = objCvwWH_Receive.GetListPaging(1, 1, "WHERE ID=" + pID.ToString(), "ID", out _RowCount);
            return new object[] {
                _ReturnedMessage
                , pID //pData[1]
                , new JavaScriptSerializer().Serialize(objCvwWH_Receive.lstCVarvwWH_Receive[0]) //pData[2]
                //, new JavaScriptSerializer().Serialize(objCvwWH_ReceiveDetails.lstCVarvwWH_ReceiveDetails) //pData[3]
            };
        }

        [HttpGet, HttpPost]
        public bool Delete(String pReceiveIDs)
        {
            bool _result = false;
            CWH_Receive objCWH_Receive = new CWH_Receive();
            foreach (var currentID in pReceiveIDs.Split(','))
            {
                objCWH_Receive.lstDeletedCPKWH_Receive.Add(new CPKWH_Receive() { ID = Int64.Parse(currentID.Trim()) });
            }

            Exception checkException = objCWH_Receive.DeleteItem(objCWH_Receive.lstDeletedCPKWH_Receive);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("DELETE")) // some or all of the Receives were not deleted due to dependencies
                    _result = false;
            }
            else //deleted successfully
                _result = true;
            return _result;
        }

        [HttpGet, HttpPost]
        public object[] Finalize(Int64 pFinalizedReceiveID)
        {
            CWH_Receive objCWH_Receive = new CWH_Receive();
            CvwWH_Receive objCvwWH_Receive = new CvwWH_Receive();
            int constReceiveStatusFinalized = 30;
            string _ReturnedMessage = "";
            Exception checkException = null;
            checkException = objCWH_Receive.UpdateList("IsFinalized=1, StatusID=" + constReceiveStatusFinalized 
                + ",FinalizeDate=GETDATE() WHERE ID=" + pFinalizedReceiveID.ToString());
            if (checkException == null)
            {
                //TODO: throw into operations
            }
            else
                _ReturnedMessage = checkException.Message;
            return new object[] {
                _ReturnedMessage
            };
        }

        [HttpGet, HttpPost]
        public object[] SetPutawayStatus(Int64 pReceiveIDToSet)
        {
            CWH_ReceiveDetails objCWH_ReceiveDetails = new CWH_ReceiveDetails();
            CWH_Receive objCWH_Receive = new CWH_Receive();
            int _RowCount = 0;
            int constReceiveStatusInProgress = 10;
            int constReceiveStatusPutaway = 20;
            //int constReceiveDetailsStatusPending = 10;
            //int constReceiveDetailsStatusPutaway = 40;
            Exception checkException = null;
            checkException = objCWH_ReceiveDetails.GetListPaging(99999, 1, "WHERE ReceiveID=" + pReceiveIDToSet.ToString(), "ID", out _RowCount);
            if (objCWH_ReceiveDetails.lstCVarWH_ReceiveDetails.Count == 0) //no details so InProgress
                objCWH_Receive.UpdateList("StatusID=" + constReceiveStatusInProgress + " WHERE ID=" + pReceiveIDToSet.ToString());
            else
            {
                checkException = objCWH_ReceiveDetails.GetListPaging(99999, 1, "WHERE ReceiveID=" + pReceiveIDToSet.ToString() + " AND LocationID IS NULL ", "ID", out _RowCount);
                if (objCWH_ReceiveDetails.lstCVarWH_ReceiveDetails.Count > 0)
                    objCWH_Receive.UpdateList("StatusID=" + constReceiveStatusInProgress + " WHERE ID=" + pReceiveIDToSet.ToString());

                else //if finalized then no save coz it will be disabled
                    objCWH_Receive.UpdateList("StatusID=" + constReceiveStatusPutaway + " WHERE ID=" + pReceiveIDToSet.ToString());
            }
            return new object[] {
            };
        }
        
        #endregion Receive
        #region ReceiveDetails
        [HttpGet, HttpPost]
        public object[] ReceiveDetails_LoadAll(bool pIsLoadArrayOfObjects, string pLanguage, Int32 pPageNumber, Int32 pPageSize, string pWhereClauseReceiveDetails, string pOrderBy)
        {
            int _RowCount = 0;
            Exception checkException = null;
            CvwWH_ReceiveDetails objvwCWH_ReceiveDetails = new CvwWH_ReceiveDetails();
            if (pIsLoadArrayOfObjects)
            {
            }
            checkException = objvwCWH_ReceiveDetails.GetListPaging(pPageSize, pPageNumber, pWhereClauseReceiveDetails, pOrderBy, out _RowCount);
            //var pDistinctReceives = objvwCWH_ReceiveDetails.lstCVarvwWH_ReceiveDetails
            //    .GroupBy(g => g.ReceiveID)
            //    .Select(s => new
            //    {
            //        ID = s.First().ID
            //        ,
            //        Code = s.First().ReceiveCode
            //    });
            return new object[] {
                //new JavaScriptSerializer().Serialize(pDistinctReceives.ToList())
                new JavaScriptSerializer().Serialize(objvwCWH_ReceiveDetails.lstCVarvwWH_ReceiveDetails)
                , _RowCount
            };
        }

        [HttpGet, HttpPost]
        public object[] ReceiveDetails_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned(bool pIsLoadArrayOfObjects, string pLanguage, Int32 pPageNumber, Int32 pPageSize, string pWhereClauseForPDI, string pOrderBy)
        {
            int _RowCount = 0;
            int _RowCount2 = 0;
            Exception checkException = null;
            CvwWH_ReceiveDetails objCvwWH_ReceiveDetails = new CvwWH_ReceiveDetails();
            CWH_Warehouse objCWH_Warehouse = new CWH_Warehouse();
            CvwOperationsToRestoreInvoices objCOperations = new CvwOperationsToRestoreInvoices();
            CvwWH_ReceiveDetails objCvwWH_ReceiveDetails_GetPurchaseItems = new CvwWH_ReceiveDetails();
            CvwWH_ReceiveDetails objCvwWH_ReceiveDetails_GetCustomers = new CvwWH_ReceiveDetails();
            CPurchaseItem objCPurchaseItem_All = new CPurchaseItem();

            if (pIsLoadArrayOfObjects)
            {
                checkException = objCWH_Warehouse.GetListPaging(99999, 1, "WHERE 1=1", "Name", out _RowCount2);
                checkException = objCPurchaseItem_All.GetListPaging(99999, 1, "WHERE IsVehicle=0 AND IsFlexi=0", "Code,Name", out _RowCount2);
                checkException = objCOperations.GetList("WHERE BLType <> 2 ORDER BY ID DESC");
                checkException = objCvwWH_ReceiveDetails_GetCustomers.GetListPaging(999999, 1, "WHERE Quantity>PickedQuantity AND IsFinalized=1", "ID", out _RowCount);
                checkException = objCvwWH_ReceiveDetails_GetPurchaseItems.GetListPaging(999999, 1, "WHERE isnull(OperationVehicleID,0)=0 AND (Quantity>PickedQuantity) AND IsFinalized=1", "ID", out _RowCount2);
            }
            var pCustomerList = objCvwWH_ReceiveDetails_GetCustomers.lstCVarvwWH_ReceiveDetails
                            .Select(s => new
                            {
                                ID = s.CustomerID
                                ,
                                Name = s.CustomerName
                            })
                            .Distinct().OrderBy(o => o.Name).ToList();
            var pPurchaseItemList = objCvwWH_ReceiveDetails_GetPurchaseItems.lstCVarvwWH_ReceiveDetails
                .Select(s => new
                {
                    ID = s.PurchaseItemID
                    ,
                    Code = s.PurchaseItemCode
                    ,
                    Name = s.PurchaseItemName
                })
                .Distinct().OrderBy(o => o.Code).ToList();
            var pPurchaseItem_AllList = objCPurchaseItem_All.lstCVarPurchaseItem
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Code = s.Code
                    ,
                    Name = s.Name
                })
                .Distinct().OrderBy(o => o.Code).ToList();
            var pOperationsList = objCOperations.lstCVarvwOperationsToRestoreInvoices
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Code = s.EffectiveOperationCode
                }).ToList();

            checkException = objCvwWH_ReceiveDetails.GetListPaging(pPageSize, pPageNumber, pWhereClauseForPDI, pOrderBy, out _RowCount);

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[]
            {
                new JavaScriptSerializer().Serialize(objCvwWH_ReceiveDetails.lstCVarvwWH_ReceiveDetails)
                , _RowCount
                , new JavaScriptSerializer().Serialize(objCWH_Warehouse.lstCVarWH_Warehouse) //pWarehouse=pData[2]
                , serializer.Serialize(pPurchaseItemList) //pPurchaseItem=pData[3]
                , new JavaScriptSerializer().Serialize(pCustomerList) //pCustomer=pData[4]
                , new JavaScriptSerializer().Serialize(pOperationsList) //pOperation=pData[5]
                , serializer.Serialize(pPurchaseItem_AllList) //pPurchaseItem_All=pData[6]
            };
        }

        [HttpGet, HttpPost]
        public object[] ReceiveDetails_Export(Int32 pPageNumber, Int32 pPageSize, string pWhereClauseExportReceiveDetails, string pOrderBy)
        {
            int _RowCount = 0;
            Exception checkException = null;
            CvwWH_ReceiveDetails objvwCWH_ReceiveDetails = new CvwWH_ReceiveDetails();
            checkException = objvwCWH_ReceiveDetails.GetListPaging(pPageSize, pPageNumber, pWhereClauseExportReceiveDetails, pOrderBy, out _RowCount);
            var pReceiveDetailsList = objvwCWH_ReceiveDetails.lstCVarvwWH_ReceiveDetails
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ReceiveCode = s.ReceiveCode
                    ,
                    ProductCode = s.PurchaseItemCode//s.First().PurchaseItemCode
                    ,
                    ProductName = s.PurchaseItemName
                    ,
                    PartNumber = s.PartNumber
                    ,
                    Quantity = s.Quantity
                    ,
                    AreaName = s.AreaName
                    ,
                    LocationCode = s.LocationCode
                    ,
                    PalletID = s.PalletID
                    ,
                    Status = s.StatusName
                    ,
                    ReceiveDate = s.ReceiveDate.ToShortDateString()
                    ,
                    Notes = s.Notes
                    
                    ,
                    BatchNumber = s.BatchNumber
                    ,
                    ExpirationDate = s.ExpirationDate.ToShortDateString()
                    ,
                    ImportedBy = s.ImportedBy
                    ,
                    WeightInTons = s.WeightInTons
                }).ToList();
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                serializer.Serialize(pReceiveDetailsList)
            };
        }

        [HttpGet, HttpPost]
        public object[] ReceiveDetails_Save(Int64 pReceiveDetailsID, Int64 pReceiveID, string pBarCode, Int64 pPurchaseItemID
            , decimal pQuantity, decimal pExpectedQuantity, decimal pSplitQuantity, Int32 pLocationID, string pPalletID, string pNotes
            , string pBatchNumber, string pExpirationDate, string pImportedBy, decimal pWeightInTons
            //Header
            , string pReceiveDate, int pStatusID, Int32 pOldLocationID, DateTime pExpireDate, string pLotNo)
        {
            Exception checkException = null;
            CVarWH_ReceiveDetails objCVarWH_ReceiveDetails = new CVarWH_ReceiveDetails();
            CWH_ReceiveDetails objCWH_ReceiveDetails = new CWH_ReceiveDetails();
            CvwWH_ReceiveDetails objCvwWH_ReceiveDetails = new CvwWH_ReceiveDetails();
            CWH_Receive objCWH_Receive = new CWH_Receive();
            CvwWH_Receive objCvwWH_Receive = new CvwWH_Receive();
            CWH_RowLocation objCWH_RowLocation = new CWH_RowLocation();
            int _RowCount = 0;

            #region Save
            objCVarWH_ReceiveDetails.ID = pReceiveDetailsID;
            objCVarWH_ReceiveDetails.ReceiveID = pReceiveID;
            objCVarWH_ReceiveDetails.BarCode = pBarCode;
            objCVarWH_ReceiveDetails.PurchaseItemID = pPurchaseItemID;
            objCVarWH_ReceiveDetails.Quantity = pExpectedQuantity;
            objCVarWH_ReceiveDetails.ExpectedQuantity = pExpectedQuantity;
            objCVarWH_ReceiveDetails.SplitQuantity = pSplitQuantity;
            objCVarWH_ReceiveDetails.LocationID = pLocationID;
            objCVarWH_ReceiveDetails.PalletID = pPalletID;
            objCVarWH_ReceiveDetails.Notes = pNotes;
            objCVarWH_ReceiveDetails.ReceiveDate = DateTime.ParseExact(pReceiveDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture);
            objCVarWH_ReceiveDetails.StatusID = pStatusID;
            objCVarWH_ReceiveDetails.ExpireDate = pExpireDate;// DateTime.Parse(pExpireDate.ToString());
            objCVarWH_ReceiveDetails.LotNo = pLotNo;
            if (pReceiveDetailsID != 0) //update so save original creator & creation date
            {
                CWH_ReceiveDetails objCGetCreationInformation = new CWH_ReceiveDetails();
                objCGetCreationInformation.GetItem(pReceiveDetailsID);
                
                objCVarWH_ReceiveDetails.IsPickupAddedToInvoice = objCGetCreationInformation.lstCVarWH_ReceiveDetails[0].IsPickupAddedToInvoice;
                objCVarWH_ReceiveDetails.PickupWithoutInvoiceDate = objCGetCreationInformation.lstCVarWH_ReceiveDetails[0].PickupWithoutInvoiceDate;

                objCVarWH_ReceiveDetails.IsExcluded = objCGetCreationInformation.lstCVarWH_ReceiveDetails[0].IsExcluded;
                objCVarWH_ReceiveDetails.OperationVehicleID = objCGetCreationInformation.lstCVarWH_ReceiveDetails[0].OperationVehicleID;
                objCVarWH_ReceiveDetails.CreatorUserID = objCGetCreationInformation.lstCVarWH_ReceiveDetails[0].CreatorUserID;
                objCVarWH_ReceiveDetails.CreationDate = objCGetCreationInformation.lstCVarWH_ReceiveDetails[0].CreationDate;
            }
            else //Insert
            {
                objCVarWH_ReceiveDetails.IsPickupAddedToInvoice = false;
                objCVarWH_ReceiveDetails.PickupWithoutInvoiceDate = DateTime.Parse("01/01/1900");

                objCVarWH_ReceiveDetails.IsExcluded = false;
                objCVarWH_ReceiveDetails.OperationVehicleID = 0;
                objCVarWH_ReceiveDetails.CreatorUserID = WebSecurity.CurrentUserId;
                objCVarWH_ReceiveDetails.CreationDate = DateTime.Now;
            }

            objCVarWH_ReceiveDetails.BatchNumber = pBatchNumber;
            objCVarWH_ReceiveDetails.ExpirationDate = DateTime.ParseExact(pExpirationDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture);
            objCVarWH_ReceiveDetails.ImportedBy = pImportedBy;
            objCVarWH_ReceiveDetails.WeightInTons = pWeightInTons;

            objCVarWH_ReceiveDetails.ModificatorUserID = WebSecurity.CurrentUserId;
            objCVarWH_ReceiveDetails.ModificationDate = DateTime.Now;

            objCWH_ReceiveDetails.lstCVarWH_ReceiveDetails.Add(objCVarWH_ReceiveDetails);
            checkException = objCWH_ReceiveDetails.SaveMethod(objCWH_ReceiveDetails.lstCVarWH_ReceiveDetails);
            #endregion Save
            
            #region Set Locations IsUsed
            if (checkException == null)
            {
                checkException = objCWH_RowLocation.UpdateList("IsUsed=0 ,StatusID = 10 WHERE ID=" + pOldLocationID.ToString());
                checkException = objCWH_RowLocation.UpdateList("IsUsed=1 ,StatusID = 20 WHERE ID=" + pLocationID.ToString());
            }
            #endregion Set Locations IsUsed
            
            #region Set ReceiveStatus if needed
            SetPutawayStatus(pReceiveID);
            #endregion Set ReceiveStatus if needed

            checkException = objCvwWH_Receive.GetList("WHERE ID=" + pReceiveID.ToString());
            checkException = objCvwWH_ReceiveDetails.GetListPaging(999999, 1, "WHERE ReceiveID=" + pReceiveID.ToString(), "ID", out _RowCount);
            return new object[] {
                checkException == null ? true : false
                , new JavaScriptSerializer().Serialize(objCvwWH_ReceiveDetails.lstCVarvwWH_ReceiveDetails) //pData[1]
                , new JavaScriptSerializer().Serialize(objCvwWH_Receive.lstCVarvwWH_Receive[0]) //pData[2]
            };
        }

        [HttpGet, HttpPost]
        public object[] ReceiveDetails_ImportFromExcel([FromBody] InsertList insertList)
        {
            Exception checkException = null;
            CPurchaseItem objCPurchaseItem = new CPurchaseItem();
            CWH_ReceiveDetails objCWH_ReceiveDetails = new CWH_ReceiveDetails();
            CvwWH_ReceiveDetails objCvwWH_ReceiveDetails = new CvwWH_ReceiveDetails();
            CvwWH_Receive objCvwWH_Receive = new CvwWH_Receive();
            var constReceiveDetailsStatusPending = 10;
            var constReceiveDetailsStatusPutAway = 40;
            int _RowCount = 0;
            string _ReturnedMessage = "";
            var _ArrBarCode = insertList.pBarCodeList.Split(',');
            var _ArrPurchaseItemCode = insertList.pPurchaseItemCodeList.Split(',');
            var _ArrQuantity = insertList.pQuantityList.Split(',');
            var _ArrPalletID = insertList.pPalletIDList.Split(',');
            var _ArrReceiveDate = insertList.pReceiveDateList.Split(',');
            var _ArrLocationCode = insertList.pLocationCodeList.Split(',');
            var _ArrNotes = insertList.pNotesList.Split(',');
            var _ArrSerial = insertList.pSerialList.Split(',');

            var _ArrBatchNumber = insertList.pBatchNumberList.Split(',');
            var _ArrExpirationDate = insertList.pExpirationDateList.Split(',');
            var _ArrImportedBy = insertList.pImportedByList.Split(',');
            var _ArrWeightInTons = insertList.pWeightInTonsList.Split(',');

            checkException = objCvwWH_Receive.GetList("WHERE ID=" + insertList.pReceiveID.ToString());
            int _WarehouseID = objCvwWH_Receive.lstCVarvwWH_Receive[0].WarehouseID;

            #region Save
            for (int i = 0; i < _ArrQuantity.Length && _ReturnedMessage == ""; i++)
            {
                objCPurchaseItem.GetListPaging(1, 1, "WHERE Code=N'" + _ArrPurchaseItemCode[i] + "'", "ID", out _RowCount);
                if (objCPurchaseItem.lstCVarPurchaseItem.Count == 0)
                    _ReturnedMessage = "Product '" + _ArrPurchaseItemCode[i] + "' is not found.";
                else if (_ArrQuantity[i] == "0")
                    _ReturnedMessage = "Product '" + _ArrPurchaseItemCode[i] + "' is having 0 quantity.";
                else
                {
                    CvwWH_RowLocation objCvwWH_RowLocation = new CvwWH_RowLocation();
                    if (_ArrLocationCode[i] != "0")
                        checkException = objCvwWH_RowLocation.GetListPaging(1, 1, "WHERE Code=N'" + _ArrLocationCode[i] + "' AND WarehouseID=" + _WarehouseID, "ID", out _RowCount);
                    int _LocationID = objCvwWH_RowLocation.lstCVarvwWH_RowLocation.Count == 0 ? 0 : objCvwWH_RowLocation.lstCVarvwWH_RowLocation[0].ID;

                    CVarWH_ReceiveDetails objCVarWH_ReceiveDetails = new CVarWH_ReceiveDetails();
                    objCVarWH_ReceiveDetails.ID = 0;
                    objCVarWH_ReceiveDetails.ReceiveID = insertList.pReceiveID;
                    objCVarWH_ReceiveDetails.BarCode = _ArrBarCode[i];
                    objCVarWH_ReceiveDetails.PurchaseItemID = objCPurchaseItem.lstCVarPurchaseItem[0].ID;
                    objCVarWH_ReceiveDetails.Quantity = decimal.Parse(_ArrQuantity[i]);
                    objCVarWH_ReceiveDetails.ExpectedQuantity = decimal.Parse(_ArrQuantity[i]);
                    objCVarWH_ReceiveDetails.SplitQuantity = 0;
                    objCVarWH_ReceiveDetails.LocationID = _LocationID;
                    objCVarWH_ReceiveDetails.PalletID = _ArrPalletID[i];
                    objCVarWH_ReceiveDetails.Notes = _ArrNotes[i];
                    objCVarWH_ReceiveDetails.ReceiveDate = DateTime.ParseExact(_ArrReceiveDate[i] + " 00.00.00.000", "d/M/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture);
                    objCVarWH_ReceiveDetails.StatusID = _LocationID == 0 ? constReceiveDetailsStatusPending : constReceiveDetailsStatusPutAway;
                    objCVarWH_ReceiveDetails.ExpireDate = DateTime.Parse("01/01/1900");//DateTime.Parse(pExpireDate.ToString());
                    objCVarWH_ReceiveDetails.LotNo = _ArrSerial[i];

                    objCVarWH_ReceiveDetails.BatchNumber = _ArrBatchNumber[i];
                    objCVarWH_ReceiveDetails.ExpirationDate = _ArrExpirationDate[i] == "0" ? DateTime.Parse("01/01/1900") : DateTime.ParseExact(_ArrExpirationDate[i] + " 00.00.00.000", "d/M/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture);
                    objCVarWH_ReceiveDetails.ImportedBy = _ArrImportedBy[i];
                    objCVarWH_ReceiveDetails.WeightInTons = decimal.Parse(_ArrWeightInTons[i]);

                    objCVarWH_ReceiveDetails.IsPickupAddedToInvoice = false;
                    objCVarWH_ReceiveDetails.PickupWithoutInvoiceDate = DateTime.Parse("01/01/1900");

                    objCVarWH_ReceiveDetails.IsExcluded = false;
                    objCVarWH_ReceiveDetails.OperationVehicleID = 0;
                    objCVarWH_ReceiveDetails.CreatorUserID = WebSecurity.CurrentUserId;
                    objCVarWH_ReceiveDetails.CreationDate = DateTime.Now;
                    objCVarWH_ReceiveDetails.ModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarWH_ReceiveDetails.ModificationDate = DateTime.Now;

                    objCWH_ReceiveDetails.lstCVarWH_ReceiveDetails.Add(objCVarWH_ReceiveDetails);
                }
            } //for (int i = 0; i < _ArrQuantity.Count; i++)
            if (_ReturnedMessage == "")
                checkException = objCWH_ReceiveDetails.SaveMethod(objCWH_ReceiveDetails.lstCVarWH_ReceiveDetails);
            if (checkException != null)
                _ReturnedMessage = checkException.Message;
            #endregion Save

            #region Set Locations IsUsed
            //if (checkException == null)
            //{
            //    checkException = objCWH_RowLocation.UpdateList("IsUsed=0 ,StatusID = 10 WHERE ID=" + pOldLocationID.ToString());
            //    checkException = objCWH_RowLocation.UpdateList("IsUsed=1 ,StatusID = 20 WHERE ID=" + pLocationID.ToString());
            //}
            #endregion Set Locations IsUsed

            #region Set ReceiveStatus if needed
            SetPutawayStatus(insertList.pReceiveID);
            #endregion Set ReceiveStatus if needed

            checkException = objCvwWH_Receive.GetList("WHERE ID=" + insertList.pReceiveID.ToString());
            checkException = objCvwWH_ReceiveDetails.GetListPaging(999999, 1, "WHERE ReceiveID=" + insertList.pReceiveID.ToString(), "ID", out _RowCount);
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                _ReturnedMessage
                , serializer.Serialize(objCvwWH_ReceiveDetails.lstCVarvwWH_ReceiveDetails) //pData[1]
                , new JavaScriptSerializer().Serialize(objCvwWH_Receive.lstCVarvwWH_Receive[0]) //pData[2]
            };
        }

        [HttpGet, HttpPost]
        public object[] ReceiveDetails_AddVehicle(Int64 pReceiveID, Int32 pWarehouseID, string pChassisNumber)
        {
            string _MessageReturned = "";
            Exception checkException = null;
            int _RowCount = 0;

            var constReceiveDetailsStatusPending = 10;
            //var constReceiveDetailsStatusHeld = 20;
            //var constReceiveDetailsStatusDamaged = 30;
            //var constReceiveDetailsStatusPutaway = 40;

            CvwWH_Receive objCvwWH_Receive = new CvwWH_Receive();
            CvwVehicleAction objCvwVehicleAction = new CvwVehicleAction();
            CVarWH_ReceiveDetails objCVarWH_ReceiveDetails = new CVarWH_ReceiveDetails();
            CWH_ReceiveDetails objCWH_ReceiveDetails = new CWH_ReceiveDetails();
            CvwWH_ReceiveDetails objCvwWH_ReceiveDetails = new CvwWH_ReceiveDetails();
            string _WhereClause = "WHERE 1=1 ";
            _WhereClause += " AND ChassisNumber=N'" + pChassisNumber + "'" + " \n";
            _WhereClause += " AND ToWarehouseID=" + pWarehouseID + " \n";
            _WhereClause += " AND ChassisNumber NOT IN (SELECT ChassisNumber FROM vwWH_ReceiveDetails WHERE WarehouseID=" + pWarehouseID  + "  AND ChassisNumber IS NOT NULL AND IsFinalized=0)" + " \n";
            checkException = objCvwVehicleAction.GetListPaging(999999, 1, _WhereClause, "ID", out _RowCount);

            #region Save
            if (objCvwVehicleAction.lstCVarvwVehicleAction.Count > 0)
            {
                objCVarWH_ReceiveDetails.ID = 0;
                objCVarWH_ReceiveDetails.ReceiveID = pReceiveID;
                objCVarWH_ReceiveDetails.BarCode = "0";
                objCVarWH_ReceiveDetails.PurchaseItemID = objCvwVehicleAction.lstCVarvwVehicleAction[0].PurchaseItemID;
                objCVarWH_ReceiveDetails.Quantity = 1;
                objCVarWH_ReceiveDetails.ExpectedQuantity = 1;
                objCVarWH_ReceiveDetails.SplitQuantity = 0;
                objCVarWH_ReceiveDetails.LocationID = 0;
                objCVarWH_ReceiveDetails.PalletID = "0";
                objCVarWH_ReceiveDetails.Notes = "0";
                objCVarWH_ReceiveDetails.ReceiveDate = DateTime.Now; //DateTime.ParseExact(pReceiveDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture);
                objCVarWH_ReceiveDetails.StatusID = constReceiveDetailsStatusPending;
                objCVarWH_ReceiveDetails.ExpireDate = DateTime.Parse("01/01/1900");// DateTime.Parse(pExpireDate.ToString());
                objCVarWH_ReceiveDetails.LotNo = "0";
                objCVarWH_ReceiveDetails.IsExcluded = false;
                objCVarWH_ReceiveDetails.OperationVehicleID = objCvwVehicleAction.lstCVarvwVehicleAction[0].OperationVehicleID;

                objCVarWH_ReceiveDetails.IsPickupAddedToInvoice = false;
                objCVarWH_ReceiveDetails.PickupWithoutInvoiceDate = DateTime.Parse("01/01/1900");

                objCVarWH_ReceiveDetails.CreatorUserID = WebSecurity.CurrentUserId;
                objCVarWH_ReceiveDetails.CreationDate = DateTime.Now;
                objCVarWH_ReceiveDetails.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarWH_ReceiveDetails.ModificationDate = DateTime.Now;

                objCWH_ReceiveDetails.lstCVarWH_ReceiveDetails.Add(objCVarWH_ReceiveDetails);
                checkException = objCWH_ReceiveDetails.SaveMethod(objCWH_ReceiveDetails.lstCVarWH_ReceiveDetails);

                //Ensure that ware house is as added
                CWH_Receive objCWH_Receive = new CWH_Receive();
                checkException = objCWH_Receive.UpdateList("WarehouseID=" + pWarehouseID + " WHERE ID=" + pReceiveID);
            }
            else
                _MessageReturned = "Check that item exists and sent to the warehouse.";
            #endregion Save

            #region Get Returned Data
            checkException = objCvwWH_ReceiveDetails.GetListPaging(999999, 1, "WHERE ReceiveID=" + pReceiveID, "ID", out _RowCount);
            #endregion Get Returned Data
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[]
            {
                _MessageReturned
                , serializer.Serialize(objCvwWH_ReceiveDetails.lstCVarvwWH_ReceiveDetails) //pData[1]
            };
        }

        [HttpGet, HttpPost]
        public object[] ReceiveDetails_Delete([FromBody] DeleteList deleteList)
        {
            Exception checkException = null;
            bool _Result = true;
            int _RowCount = 0;
            CWH_RowLocation objCWH_RowLocation = new CWH_RowLocation();
            CWH_ReceiveDetails objCWH_ReceiveDetails = new CWH_ReceiveDetails();
            CvwWH_ReceiveDetails objCvwWH_ReceiveDetails = new CvwWH_ReceiveDetails();
            CvwWH_Receive objCvwWH_Receive = new CvwWH_Receive();
            COperationVehicle objCOperationVehicle = new COperationVehicle();
            int _NumberOfRecords = deleteList.pReceiveDetailsIDsToDelete.Split(',').Length;
            for (int i = 0; i < _NumberOfRecords; i++)
            {
                Int64 _ReceiveDetailsID = Int64.Parse(deleteList.pReceiveDetailsIDsToDelete.Split(',')[i]);
                objCWH_ReceiveDetails.GetItem(_ReceiveDetailsID);
                checkException = objCOperationVehicle.UpdateList("IsSentToWarehouse=0 WHERE ID=" + objCWH_ReceiveDetails.lstCVarWH_ReceiveDetails[0].OperationVehicleID);
                int _LocationID = objCWH_ReceiveDetails.lstCVarWH_ReceiveDetails[0].LocationID;
                checkException = objCWH_ReceiveDetails.DeleteList("WHERE ID=" + _ReceiveDetailsID.ToString());
                if (checkException == null)
                    objCWH_RowLocation.UpdateList("IsUsed=0,StatusID = 10 WHERE ID=" + _LocationID.ToString());
                else
                    _Result = false;
            }
            SetPutawayStatus(deleteList.pReceiveID);
            objCvwWH_ReceiveDetails.GetListPaging(99999, 1, "WHERE ReceiveID=" + deleteList.pReceiveID, "ID", out _RowCount);
            objCvwWH_Receive.GetList("WHERE ID=" + deleteList.pReceiveID.ToString());
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                _Result
                , serializer.Serialize(objCvwWH_ReceiveDetails.lstCVarvwWH_ReceiveDetails) //pData[1]
                , new JavaScriptSerializer().Serialize(objCvwWH_Receive.lstCVarvwWH_Receive[0]) //pData[2]
            };
        }
        
        #endregion ReceiveDetails
        #region ReceiveDetailsSerial
        [HttpGet, HttpPost]
        public object[] ReceiveDetailsSerial_LoadAll(string pWhereClauseReceiveDetailsSerial)
        {
            CWH_ReceiveDetailsSerial objCWH_ReceiveDetailsSerial = new CWH_ReceiveDetailsSerial();
            objCWH_ReceiveDetailsSerial.GetList(pWhereClauseReceiveDetailsSerial);
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                serializer.Serialize(objCWH_ReceiveDetailsSerial.lstCVarWH_ReceiveDetailsSerial)
            };
        }
        [HttpGet, HttpPost]
        public object[] ReceiveDetailsSerial_GenerateSerial(Int64 pReceiveDetailsIDForSerialGenerate, Int32 pQuantity)
        {
            Exception checkException = null;
            CWH_ReceiveDetailsSerial objCWH_ReceiveDetailsSerial = new CWH_ReceiveDetailsSerial();
            checkException = objCWH_ReceiveDetailsSerial.DeleteList("WHERE ReceiveDetailsID=" + pReceiveDetailsIDForSerialGenerate);
            for (int i = 0; i < pQuantity; i++)
            {
                CVarWH_ReceiveDetailsSerial objCVarWH_ReceiveDetailsSerial = new CVarWH_ReceiveDetailsSerial();
                objCVarWH_ReceiveDetailsSerial.ReceiveDetailsID = pReceiveDetailsIDForSerialGenerate;
                objCVarWH_ReceiveDetailsSerial.Serial = (i + 1).ToString();
                objCVarWH_ReceiveDetailsSerial.PickupDetailsLocationID = 0;
                objCVarWH_ReceiveDetailsSerial.LotNumber = "0";
                objCVarWH_ReceiveDetailsSerial.Notes = "0";
                objCWH_ReceiveDetailsSerial.lstCVarWH_ReceiveDetailsSerial.Add(objCVarWH_ReceiveDetailsSerial);
            }
            checkException = objCWH_ReceiveDetailsSerial.SaveMethod(objCWH_ReceiveDetailsSerial.lstCVarWH_ReceiveDetailsSerial);
            objCWH_ReceiveDetailsSerial.GetList("WHERE ReceiveDetailsID=" + pReceiveDetailsIDForSerialGenerate + " ORDER BY Serial");
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                serializer.Serialize(objCWH_ReceiveDetailsSerial.lstCVarWH_ReceiveDetailsSerial)
            };
        }
        [HttpGet, HttpPost]
        public object[] ReceiveDetailsSerial_Save([FromBody] ReceiveDetailsSerial_SaveParameters receiveDetailsSerial_SaveParameters)
        {
            string _MessageReturned = "";
            Exception checkException = null;
            CWH_ReceiveDetailsSerial objCWH_ReceiveDetailsSerial = new CWH_ReceiveDetailsSerial();
            var _ArrID = receiveDetailsSerial_SaveParameters.pSelectedIDsToSave.Split(',');
            var _ArrIsInsert = receiveDetailsSerial_SaveParameters.pIsInsertList.Split(',');
            var _ArrSerialList = receiveDetailsSerial_SaveParameters.pSerialList.Split(',');
            var _ArrPickupDetailsLocationIDList = receiveDetailsSerial_SaveParameters.pPickupDetailsLocationIDList.Split(',');
            var _ArrVehicleList = receiveDetailsSerial_SaveParameters.pVehicleList.Split(',');
            var _ArrMotorNoList = receiveDetailsSerial_SaveParameters.pMotorNoList.Split(',');
            int _NumberOfRecords = _ArrID.Length;
            for (int i = 0; i < _NumberOfRecords; i++)
            {
                //if (_ArrSerialList[i] != "0")
                if ((receiveDetailsSerial_SaveParameters.pFlag == "ReceiveDetailsSerial_FillModal" && _ArrSerialList[i] != "0") ||
                    (receiveDetailsSerial_SaveParameters.pFlag == "ReceiveDetailsVehicle_FillModal" && (_ArrVehicleList[i] != "0" || _ArrMotorNoList[i] != "0")))
                {
                    CVarWH_ReceiveDetailsSerial objCVarWH_ReceiveDetailsSerial = new CVarWH_ReceiveDetailsSerial();
                    objCVarWH_ReceiveDetailsSerial.ID = _ArrIsInsert[i] == "1" ? 0 : Int64.Parse(_ArrID[i]);
                    objCVarWH_ReceiveDetailsSerial.ReceiveDetailsID = receiveDetailsSerial_SaveParameters.pReceiveDetailsID;
                    objCVarWH_ReceiveDetailsSerial.Serial = _ArrSerialList[i];
                    objCVarWH_ReceiveDetailsSerial.PickupDetailsLocationID = Int64.Parse(_ArrPickupDetailsLocationIDList[i]);
                    objCVarWH_ReceiveDetailsSerial.Notes = "0";
                    objCVarWH_ReceiveDetailsSerial.Vehicle = _ArrVehicleList[i];
                    objCVarWH_ReceiveDetailsSerial.MotorNo = _ArrMotorNoList[i];
                    objCVarWH_ReceiveDetailsSerial.LotNumber = "0";
                    objCWH_ReceiveDetailsSerial.lstCVarWH_ReceiveDetailsSerial.Add(objCVarWH_ReceiveDetailsSerial);
                }
            }
            checkException = objCWH_ReceiveDetailsSerial.SaveMethod(objCWH_ReceiveDetailsSerial.lstCVarWH_ReceiveDetailsSerial);
            //if (checkException != null)
            //    _MessageReturned = checkException.Message;
            if (checkException != null)
                if (checkException.ToString().Contains("unique") == true)
                    _MessageReturned = receiveDetailsSerial_SaveParameters.pFlag == "ReceiveDetailsSerial_FillModal" ? "Serial is duplicated" :
                        receiveDetailsSerial_SaveParameters.pFlag == "ReceiveDetailsVehicle_FillModal" ? " Vehicle and Mottor No are duplicated " :
                        "Please revise data";
                else
                    _MessageReturned = checkException.Message;
            return new object[] {
                _MessageReturned
            };
        }
        //[HttpGet, HttpPost]
        //public object[] ReceiveDetailsSerial_SaveFromPickup([FromBody] ReceiveDetailsSerial_SaveParametersFromPickup receiveDetailsSerial_SaveParameters)
        //{
        //    string _RejectedSerials = "";
        //    string _MessageReturned = "";
        //    Exception checkException = null;
        //    CWH_ReceiveDetailsSerial objCWH_ReceiveDetailsSerial = new CWH_ReceiveDetailsSerial();
        //    var _ArrSerialList = receiveDetailsSerial_SaveParameters.pSerialList.Split(',');
        //    Int64 _ReceiveDetailsID = receiveDetailsSerial_SaveParameters.pReceiveDetailsID;
        //    Int64 _PickupDetailsLocationID = receiveDetailsSerial_SaveParameters.pPickupDetailsLocationID;
        //    int _NumberOfRecords = _ArrSerialList.Length;
        //    for (int i = 0; i < _NumberOfRecords; i++)
        //    {
        //        CWH_ReceiveDetailsSerial objCWH_ReceiveDetailsSerial_Temp = new CWH_ReceiveDetailsSerial();
        //        objCWH_ReceiveDetailsSerial_Temp.GetList("WHERE ReceiveDetailsID=" + _ReceiveDetailsID + " AND Serial=" + _ArrSerialList[i]);
        //        if (objCWH_ReceiveDetailsSerial_Temp.lstCVarWH_ReceiveDetailsSerial.Count == 1)
        //            checkException = objCWH_ReceiveDetailsSerial.UpdateList("PickupDetailsLocationID=" + _PickupDetailsLocationID + " WHERE ReceiveDetailsID=" + _ReceiveDetailsID + " AND Serial=" + _ArrSerialList[i]);
        //        else
        //            _RejectedSerials += _RejectedSerials == "" ? _ArrSerialList[i] : (", " + _ArrSerialList[i]);
        //    }
        //    _MessageReturned = _RejectedSerials == "" ? "" : ("Serials: " + _RejectedSerials + " rejected.");
        //    return new object[] {
        //        _MessageReturned
        //    };
        //}
        [HttpGet, HttpPost]
        public object[] ReceiveDetailsSerial_DeleteList([FromBody] DeleteList_ReceiveDetailsSerial deleteList_ReceiveDetailsSerial)
        {
            string _MessageReturned = "";
            Exception checkException = null;
            CWH_ReceiveDetailsSerial objCWH_ReceiveDetailsSerial = new CWH_ReceiveDetailsSerial();
            checkException = objCWH_ReceiveDetailsSerial.DeleteList("WHERE ID IN (" + deleteList_ReceiveDetailsSerial.pReceiveDetailsSerialIDsDeleted + ")");
            
            //objCWH_ReceiveDetailsSerial.GetList("WHERE ReceiveDetailsID=" + pReceiveDetailsID);
            if (deleteList_ReceiveDetailsSerial.pFlag == "ReceiveDetailsSerial_FillModal")
                objCWH_ReceiveDetailsSerial.GetList("WHERE  Serial is not null and ReceiveDetailsID=" + deleteList_ReceiveDetailsSerial.pReceiveDetailsID);
            else if (deleteList_ReceiveDetailsSerial.pFlag == "ReceiveDetailsVehicle_FillModal")
                objCWH_ReceiveDetailsSerial.GetList("WHERE  Vehicle is not null and ReceiveDetailsID=" + deleteList_ReceiveDetailsSerial.pReceiveDetailsID);

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue};
            return new object[] {
                _MessageReturned
                , serializer.Serialize(objCWH_ReceiveDetailsSerial.lstCVarWH_ReceiveDetailsSerial)
            };
        }
        [HttpGet, HttpPost]
        public object[] ReceiveDetailsSerial_DeleteFromPickup([FromBody]DeleteList_ReceiveDetailsSerialFromPickup deleteList_ReceiveDetailsSerialFromPickup) {
            string _MessageReturned = "";
            Exception checkException = null;
            CWH_ReceiveDetailsSerial objCWH_ReceiveDetailsSerial = new CWH_ReceiveDetailsSerial();
            checkException = objCWH_ReceiveDetailsSerial.UpdateList("PickupDetailsLocationID=NULL WHERE ID IN (" + deleteList_ReceiveDetailsSerialFromPickup.pReceiveDetailsSerialIDsDeletedFromPickup + ") AND PickupDetailsLocationID=" + deleteList_ReceiveDetailsSerialFromPickup.pPickupDetailsLocationID);
            objCWH_ReceiveDetailsSerial.GetList("WHERE PickupDetailsLocationID=" + deleteList_ReceiveDetailsSerialFromPickup.pPickupDetailsLocationID);
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue};
            return new object[] {
                _MessageReturned
                , serializer.Serialize(objCWH_ReceiveDetailsSerial.lstCVarWH_ReceiveDetailsSerial)
            };
        }
        #endregion ReceiveDetailsSerial
    }
    public class ReceiveDetailsSerial_SaveParameters
    {
        public Int64 pReceiveDetailsID { get; set; }
        public string pSelectedIDsToSave { get; set; }
        public string pIsInsertList { get; set; }
        public string pSerialList { get; set; }
        public string pPickupDetailsLocationIDList { get; set; }
        public string pVehicleList { get; set; }
        public string pMotorNoList { get; set; }
        public string pLotNoList { get; set; }
        public string pExpireDateList { get; set; }
        public string pFlag { get; set; }
    }
    public class InsertList
    {
        public Int64 pReceiveID { get; set; }
        public string pBarCodeList { get; set; }
        public string pPurchaseItemCodeList { get; set; }
        public string pQuantityList { get; set; }
        public string pPalletIDList { get; set; }
        public string pReceiveDateList { get; set; }
        public string pLocationCodeList { get; set; }
        public string pNotesList { get; set; }
        public string pSerialList { get; set; }

        public string pBatchNumberList { get; set; }
        public string pExpirationDateList { get; set; }
        public string pImportedByList { get; set; }
        public string pWeightInTonsList { get; set; }
    }
    public class DeleteList
    {
        public string pReceiveDetailsIDsToDelete { get; set; }
        public Int64 pReceiveID { get; set; }
    }
    public class DeleteList_ReceiveDetailsSerial
    {
        public string pReceiveDetailsSerialIDsDeleted { get; set; }
        public Int64 pReceiveDetailsID { get; set; }
        public string pFlag { get; set; }
    }
    public class DeleteList_ReceiveDetailsSerialFromPickup
    {
        public string pReceiveDetailsSerialIDsDeletedFromPickup { get; set; }
        public Int64 pPickupDetailsLocationID { get; set; }
    }
    //public class ReceiveDetailsSerial_SaveParametersFromPickup
    //{
    //    public Int64 pPickupDetailsLocationID { get; set; }
    //    public Int64 pReceiveDetailsID { get; set; }
    //    public string pSerialList { get; set; }
    //}
}
