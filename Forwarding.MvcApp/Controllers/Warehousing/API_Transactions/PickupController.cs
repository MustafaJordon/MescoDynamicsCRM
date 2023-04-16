using Forwarding.MvcApp.Entities.Warehousing;
using Forwarding.MvcApp.Models.Administration.Settings.Generated;
using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;
using Forwarding.MvcApp.Models.MasterData.Partners.Generated;
using Forwarding.MvcApp.Models.Operations.Operations.Generated;
using Forwarding.MvcApp.Models.Warehousing.MasterData.Generated;
using Forwarding.MvcApp.Models.Warehousing.Reports.Generated;
using Forwarding.MvcApp.Models.Warehousing.Transactions.Generated;
using System;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebMatrix.WebData;

namespace Forwarding.MvcApp.Controllers.Warehousing.API_Reports
{
    public class PickupController : ApiController
    {
        #region Pickup
        [HttpGet, HttpPost]
        public object[] LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned(bool pIsLoadArrayOfObjects, string pLanguage, Int32 pPageNumber, Int32 pPageSize, string pWhereClause, string pOrderBy)
        {
            int _RowCount = 0;
            Exception checkException = null;
            CvwWH_Pickup objCvwWH_Pickup = new CvwWH_Pickup();
            CWH_Warehouse objCWH_Warehouse = new CWH_Warehouse();
            CvwWH_ReceiveDetails objCvwWH_ReceiveDetails = new CvwWH_ReceiveDetails();
            CvwOperationsToRestoreInvoices objCOperations = new CvwOperationsToRestoreInvoices();
            if (pIsLoadArrayOfObjects)
            {
                checkException = objCWH_Warehouse.GetList("ORDER BY Name");
                checkException = objCvwWH_ReceiveDetails.GetListPaging(999999, 1, "WHERE Quantity>PickedQuantity AND IsFinalized=1", "ID", out _RowCount);
                checkException = objCOperations.GetList("WHERE BLType <> 2");
            }
            var pCustomerList = objCvwWH_ReceiveDetails.lstCVarvwWH_ReceiveDetails
                .Select(s => new
                {
                    ID = s.CustomerID
                    ,
                    Name = s.CustomerName
                })
                .Distinct().OrderBy(o => o.Name).ToList();
            var pOperationsList = objCOperations.lstCVarvwOperationsToRestoreInvoices
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Code = s.EffectiveOperationCode
                }).ToList().OrderByDescending(o => o.ID);
            checkException = objCvwWH_Pickup.GetListPaging(pPageSize, pPageNumber, pWhereClause, pOrderBy, out _RowCount);

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                new JavaScriptSerializer().Serialize(objCvwWH_Pickup.lstCVarvwWH_Pickup)
                , _RowCount
                , new JavaScriptSerializer().Serialize(objCWH_Warehouse.lstCVarWH_Warehouse) //pWarehouse=pData[2]
                , serializer.Serialize(pCustomerList) //pCustomer=pData[3]
                , serializer.Serialize(pOperationsList) //pOperation=pData[4]
            };
        }

        [HttpGet, HttpPost]
        public object[] LoadHeaderWithDetails(Int64 pHeaderID)
        {
            CvwWH_Pickup objCvwWH_Pickup = new CvwWH_Pickup();
            CContacts objCContacts = new CContacts();
            CvwWH_PickupDetails objCvwWH_PickupDetails = new CvwWH_PickupDetails();
            CvwWH_ReceiveDetails objCvwWH_ReceiveDetails = new CvwWH_ReceiveDetails();
            CDefaults objCDefaults = new CDefaults();
            Exception checkException = null;
            int _RowCount = 0;
            objCDefaults.GetListPaging(999999, 1, "WHERE 1=1", "ID", out _RowCount);
            checkException = objCvwWH_Pickup.GetListPaging(999999, 1, "WHERE ID=" + pHeaderID.ToString(), "ID", out _RowCount);
            checkException = objCvwWH_PickupDetails.GetListPaging(999999, 1, "WHERE PickupID=" + pHeaderID.ToString(), "ID", out _RowCount);
            #region Get Customers with available storage or the customer in the Pickup
            checkException = objCvwWH_ReceiveDetails.GetListPaging(999999, 1, "WHERE CustomerID=" + objCvwWH_Pickup.lstCVarvwWH_Pickup[0].CustomerID + " OR (Quantity>PickedQuantity AND IsFinalized=1)", "ID", out _RowCount);
            var pCustomerList = objCvwWH_ReceiveDetails.lstCVarvwWH_ReceiveDetails
                .Select(s => new
                {
                    ID = s.CustomerID
                    ,
                    Name = s.CustomerName
                })
                .Distinct().OrderBy(o => o.Name).ToList();
            #endregion Get Customers with available storage or the customer in the Pickup
            #region Get Products with available storage or products in the Pickup
            checkException = objCvwWH_ReceiveDetails.GetListPaging(999999, 1, "WHERE PurchaseItemID IN (SELECT PurchaseItemID FROM vwWH_PickupDetailsLocation WHERE PickupID=" + pHeaderID + ") OR (CustomerID=" + objCvwWH_Pickup.lstCVarvwWH_Pickup[0].CustomerID + " AND Quantity>PickedQuantity AND IsFinalized=1)", "ID", out _RowCount);
            var pPurchaseItemList = objCvwWH_ReceiveDetails.lstCVarvwWH_ReceiveDetails
                .Select(s => new
                {
                    ID = s.PurchaseItemID
                    ,
                    Code = s.PurchaseItemCode
                    ,
                    Name = s.PurchaseItemName
                })
                .Distinct().OrderBy(o => o.Code).ToList();
            #endregion Get Products with available storage or products in the Pickup
            #region Get PersonInCharge
            checkException = objCContacts.GetListPaging(999999, 1, "WHERE PartnerTypeID=1 AND PartnerID=" + objCvwWH_Pickup.lstCVarvwWH_Pickup[0].CustomerID, "Name", out _RowCount);
            #endregion Get PersonInCharge
            return new object[] {
                new JavaScriptSerializer().Serialize(objCvwWH_Pickup.lstCVarvwWH_Pickup[0]) //pData[0]
                , new JavaScriptSerializer().Serialize(objCvwWH_PickupDetails.lstCVarvwWH_PickupDetails) //pData[1]
                , new JavaScriptSerializer().Serialize(pCustomerList) //pCustomerList = pData[2]
                , new JavaScriptSerializer().Serialize(pPurchaseItemList) //pPurchaseItemList = pData[3]
                , new JavaScriptSerializer().Serialize(objCContacts.lstCVarContacts) //pPersonInChargeList = pData[4]
            };
        }

        [HttpGet, HttpPost]
        public object[] Save(Int64 pID, Int32 pWarehouseID, Int32 pCustomerID, Int32 pBillTo, Int32 pEndUserID
            , string pOrderDate, string pRequiredDate, bool pIsFinalized, string pFinalizeDate, string pNotes
            , Int64 pOperationID, string pRMANumber, Int64 pPersonInChargeID, Int64 pPDIReceiveDetailsID)
        {
            string strMessage = "";
            Exception checkException = null;
            CWH_Pickup objCWH_Pickup = new CWH_Pickup();
            CvwWH_Pickup objCvwWH_Pickup = new CvwWH_Pickup();
            CWH_PickupDetails objCWH_PickupDetails = new CWH_PickupDetails();
            CvwWH_PickupDetails objCvwWH_PickupDetails = new CvwWH_PickupDetails();
            CVarWH_Pickup objCVarWH_Pickup = new CVarWH_Pickup();
            int _RowCount = 0;
            string _UpdateClause = "";
            #region Insert
            if (pID == 0) //Insert
            {
                objCVarWH_Pickup.Code = "0";
                objCVarWH_Pickup.WarehouseID = pWarehouseID;
                objCVarWH_Pickup.CustomerID = pCustomerID;
                objCVarWH_Pickup.BillTo = pBillTo;
                objCVarWH_Pickup.EndUserID = pEndUserID;
                objCVarWH_Pickup.OrderDate = DateTime.ParseExact(pOrderDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture);
                objCVarWH_Pickup.RequiredDate = DateTime.ParseExact(pRequiredDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture);
                objCVarWH_Pickup.IsFinalized = pIsFinalized;
                objCVarWH_Pickup.FinalizeDate = DateTime.ParseExact(pFinalizeDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture);
                objCVarWH_Pickup.Notes = pNotes;
                objCVarWH_Pickup.InvoiceID = 0;
                objCVarWH_Pickup.OperationID = pOperationID;
                objCVarWH_Pickup.RMANumber = pRMANumber;
                objCVarWH_Pickup.PersonInChargeID = pPersonInChargeID;
                objCVarWH_Pickup.PDIReceiveDetailsID = pPDIReceiveDetailsID;

                objCVarWH_Pickup.CreatorUserID = objCVarWH_Pickup.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarWH_Pickup.CreationDate = objCVarWH_Pickup.ModificationDate = DateTime.Now;
                objCWH_Pickup.lstCVarWH_Pickup.Add(objCVarWH_Pickup);
                checkException = objCWH_Pickup.SaveMethod(objCWH_Pickup.lstCVarWH_Pickup);
                if (checkException == null)
                    pID = objCVarWH_Pickup.ID;
                else
                    strMessage = checkException.Message;
            }
            #endregion Insert
            #region Update
            else //update
            {
                _UpdateClause = pWarehouseID == 0 ? "WarehouseID=null" : ("WarehouseID=" + pWarehouseID) + " \n";
                _UpdateClause += pCustomerID == 0 ? ",CustomerID=null" : (",CustomerID=" + pCustomerID) + " \n";
                _UpdateClause += pBillTo == 0 ? ",BillTo=null" : (",BillTo=" + pBillTo) + " \n";
                _UpdateClause += pEndUserID == 0 ? ",EndUserID=null" : (",EndUserID=" + pEndUserID) + " \n";
                _UpdateClause += pOrderDate == "01/01/1900" ? " ,OrderDate = NULL " : (" ,OrderDate = '" + (DateTime.ParseExact(pOrderDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd") + "'");
                _UpdateClause += pRequiredDate == "01/01/1900" ? " ,RequiredDate = NULL " : (" ,RequiredDate = '" + (DateTime.ParseExact(pRequiredDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd") + "'");
                ////Set from the Finalize btn or when update details for putaway
                //_UpdateClause += pStatusID == 0 ? ",StatusID=null" : (",StatusID=" + pStatusID) + " \n";
                //_UpdateClause += " , IsFinalized = " + (pIsFinalized ? "1" : "0") + " \n";
                //_UpdateClause += pFinalizeDate == "01/01/1900" ? " ,FinalizeDate = NULL " : (" ,FinalizeDate = '" + (DateTime.ParseExact(pFinalizeDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd") + "'");
                _UpdateClause += pNotes == "0" ? ",Notes=null" : (",Notes=N'" + pNotes + "'") + " \n";
                _UpdateClause += pOperationID == 0 ? ",OperationID=null" : (",OperationID=" + pOperationID) + " \n";
                _UpdateClause += pRMANumber == "0" ? ",RMANumber=null" : (",RMANumber=N'" + pRMANumber + "'") + " \n";
                _UpdateClause += pPersonInChargeID == 0 ? ",PersonInChargeID=null" : (",PersonInChargeID=" + pPersonInChargeID) + " \n";
                _UpdateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                _UpdateClause += " , ModificationDate = GETDATE() ";
                _UpdateClause += " WHERE ID = " + pID.ToString();

                checkException = objCWH_Pickup.UpdateList(_UpdateClause);
                if (checkException == null)
                {
                    //i think done in details and not header
                    //TODO Update location if used or not used
                    //checkException = objCvwWH_PickupDetails.GetListPaging(99999, 1, "WHERE PickupID=" + objCVarWH_Pickup.ID, "Code", out _RowCount);
                }
                else
                    strMessage = checkException.Message;
            }
            #endregion Update
            if (strMessage == "")
                checkException = objCvwWH_Pickup.GetListPaging(1, 1, "WHERE ID=" + pID.ToString(), "ID", out _RowCount);
            return new object[] {
                strMessage
                , pID //pData[1]
                , new JavaScriptSerializer().Serialize(objCvwWH_Pickup.lstCVarvwWH_Pickup[0]) //pData[2]
                //, new JavaScriptSerializer().Serialize(objCvwWH_PickupDetails.lstCVarvwWH_PickupDetails) //pData[3]
            };
        }

        [HttpGet, HttpPost]
        public bool Delete(String pPickupIDs)
        {
            bool _result = false;
            CWH_Pickup objCWH_Pickup = new CWH_Pickup();
            foreach (var currentID in pPickupIDs.Split(','))
            {
                objCWH_Pickup.lstDeletedCPKWH_Pickup.Add(new CPKWH_Pickup() { ID = Int64.Parse(currentID.Trim()) });
            }

            Exception checkException = objCWH_Pickup.DeleteItem(objCWH_Pickup.lstDeletedCPKWH_Pickup);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("DELETE")) // some or all of the Pickups were not deleted due to dependencies
                    _result = false;
            }
            else //deleted successfully
                _result = true;
            return _result;
        }

        [HttpGet, HttpPost]
        public object[] Finalize(Int64 pFinalizedPickupID, Int32 pIsFinalize)
        {
            string strMessage = "";
            Exception checkException = null;
            string _PickedLocationIDList = "0";
            string _ReleasedLocationIDList = "0";
            int _RowCount = 0;
            CWH_Pickup objCWH_Pickup = new CWH_Pickup();
            CvwWH_PickupDetailsLocation objCvwWH_PickupDetailsLocation = new CvwWH_PickupDetailsLocation();
            CvwWH_Inventory objCvwWH_Inventory = new CvwWH_Inventory();
            CWH_RowLocation objCWH_RowLocation = new CWH_RowLocation();
            #region Finalize
            if (pIsFinalize == 1)
            {
                checkException = objCWH_Pickup.UpdateList("IsFinalized=1"
                    + ",FinalizeDate=GETDATE() WHERE ID=" + pFinalizedPickupID.ToString());
                if (checkException == null)
                {
                    #region Set IsUsed=0 in locations for released ones
                    checkException = objCvwWH_PickupDetailsLocation.GetListPaging(999999, 1, "WHERE PickupID=" + pFinalizedPickupID, "ID", out _RowCount);
                    for (int i = 0; i < objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation.Count; i++)
                        _PickedLocationIDList += "," + objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation[i].LocationID;
                    if (_PickedLocationIDList != "0")
                        checkException = objCvwWH_Inventory.GetListPaging(999999, 1, "WHERE AvailableQuantity=0 AND LocationID IN (" + _PickedLocationIDList + ")", "LocationID", out _RowCount);
                    for (int i = 0; i < objCvwWH_Inventory.lstCVarvwWH_Inventory.Count; i++)
                        _ReleasedLocationIDList += "," + objCvwWH_Inventory.lstCVarvwWH_Inventory[i].LocationID;
                    if (_ReleasedLocationIDList != "0")
                        checkException = objCWH_RowLocation.UpdateList("IsUsed=0 ,StatusID = 10 WHERE ID IN (" + _ReleasedLocationIDList + ")");
                    #endregion Set IsUsed=0 in locations for released ones
                }
            }
            #endregion Finalize
            #region UnFinalize
            else
            {
                checkException = objCWH_Pickup.UpdateList("IsFinalized=0"
                    + ",FinalizeDate=null WHERE ID=" + pFinalizedPickupID.ToString());
                if (checkException == null)
                {
                    #region Set IsUsed=0 in locations for released ones
                    checkException = objCvwWH_PickupDetailsLocation.GetListPaging(999999, 1, "WHERE PickupID=" + pFinalizedPickupID, "ID", out _RowCount);
                    for (int i = 0; i < objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation.Count; i++)
                        _PickedLocationIDList += "," + objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation[i].LocationID;
                    if (_PickedLocationIDList != "0")
                        checkException = objCvwWH_Inventory.GetListPaging(999999, 1, "WHERE AvailableQuantity>0 AND LocationID IN (" + _PickedLocationIDList + ")", "LocationID", out _RowCount);
                    for (int i = 0; i < objCvwWH_Inventory.lstCVarvwWH_Inventory.Count; i++)
                        _ReleasedLocationIDList += "," + objCvwWH_Inventory.lstCVarvwWH_Inventory[i].LocationID;
                    if (_ReleasedLocationIDList != "0")
                        checkException = objCWH_RowLocation.UpdateList("IsUsed=1 WHERE ID IN (" + _ReleasedLocationIDList + ")");
                    #endregion Set IsUsed=0 in locations for released ones
                }
            }
            #endregion UnFinalize
            return new object[] {
                strMessage
            };
        }

        [HttpGet, HttpPost]
        public object[] Print(Int64 pPrintedPickupID)
        {
            string _ReturnedMessage = "";
            Exception checkException = null;
            int _RowCount = 0;
            CvwWH_Pickup objCvwWH_Pickup = new CvwWH_Pickup();
            CvwWH_PickupDetailsLocation objCvwWH_PickupDetailsLocation = new CvwWH_PickupDetailsLocation();
            CvwWH_PickupDetailsLocationSerial objCvwWH_PickupDetailsLocationSerial = new CvwWH_PickupDetailsLocationSerial();
            checkException = objCvwWH_Pickup.GetList("WHERE ID=" + pPrintedPickupID);
            checkException = objCvwWH_PickupDetailsLocation.GetListPaging(999999, 1, "WHERE PickupID=" + pPrintedPickupID, "ID", out _RowCount);
            checkException = objCvwWH_PickupDetailsLocationSerial.GetListPaging(999999, 1, "WHERE PickupID=" + pPrintedPickupID, "ID", out _RowCount);
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                _ReturnedMessage //pData[0]
                , new JavaScriptSerializer().Serialize(objCvwWH_Pickup.lstCVarvwWH_Pickup[0]) //pData[1]
                , serializer.Serialize(objCvwWH_PickupDetailsLocationSerial.lstCVarvwWH_PickupDetailsLocationSerial) //pData[2]
            };
        }
        //not used till now
        [HttpGet, HttpPost]
        public string ValidatePickup(Int64 pPickupIDToValidate)
        {
            string _MessageReturned = "";
            int _RowCount = 0;
            Exception checkException = null;
            CvwWH_PickupDetails objCvwWH_PickupDetails = new CvwWH_PickupDetails();
            CvwWH_ReceiveDetails objCvwWH_ReceiveDetails = new CvwWH_ReceiveDetails();
            checkException = objCvwWH_PickupDetails.GetListPaging(999999, 1, "WHERE PickupID=" + pPickupIDToValidate, "ID", out _RowCount);
            for (int i = 0; i < objCvwWH_PickupDetails.lstCVarvwWH_PickupDetails.Count; i++)
            {
            }
            return _MessageReturned;
        }
        #endregion Pickup

        #region PickupDetails
        [HttpGet, HttpPost]
        public object[] PickupDetails_Save([FromBody] PickupDetails_SaveParamters pickupDetails_SaveParamters)
        {
            string _MessageReturned = "";
            Exception checkException = null;
            int _RowCount = 0;
            CVarWH_PickupDetails objCVarWH_PickupDetails = new CVarWH_PickupDetails();
            CWH_PickupDetails objCWH_PickupDetails = new CWH_PickupDetails();
            CvwWH_PickupDetails objCvwWH_PickupDetails = new CvwWH_PickupDetails();
            CWH_Pickup objCWH_Pickup = new CWH_Pickup();
            CvwWH_Pickup objCvwWH_Pickup = new CvwWH_Pickup();
            CWH_RowLocation objCWH_RowLocation = new CWH_RowLocation();
            Int64 pPickupDetailsID = 0;
            string _UpdateClause = "";
            #region Save PickupHeader
            _UpdateClause = pickupDetails_SaveParamters.pWarehouseID == 0 ? "WarehouseID=null" : ("WarehouseID=" + pickupDetails_SaveParamters.pWarehouseID) + " \n";
            _UpdateClause += pickupDetails_SaveParamters.pCustomerID == 0 ? ",CustomerID=null" : (",CustomerID=" + pickupDetails_SaveParamters.pCustomerID) + " \n";
            _UpdateClause += pickupDetails_SaveParamters.pBillTo == 0 ? ",BillTo=null" : (",BillTo=" + pickupDetails_SaveParamters.pBillTo) + " \n";
            _UpdateClause += pickupDetails_SaveParamters.pEndUserID == 0 ? ",EndUserID=null" : (",EndUserID=" + pickupDetails_SaveParamters.pEndUserID) + " \n";
            _UpdateClause += pickupDetails_SaveParamters.pOrderDate == "01/01/1900" ? " ,OrderDate = NULL " : (" ,OrderDate = '" + (DateTime.ParseExact(pickupDetails_SaveParamters.pOrderDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd") + "'");
            _UpdateClause += pickupDetails_SaveParamters.pRequiredDate == "01/01/1900" ? " ,RequiredDate = NULL " : (" ,RequiredDate = '" + (DateTime.ParseExact(pickupDetails_SaveParamters.pRequiredDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd") + "'");
            ////Set from the Finalize btn or when update details for putaway
            //_UpdateClause += pStatusID == 0 ? ",StatusID=null" : (",StatusID=" + pStatusID) + " \n";
            //_UpdateClause += " , IsFinalized = " + (pIsFinalized ? "1" : "0") + " \n";
            //_UpdateClause += pFinalizeDate == "01/01/1900" ? " ,FinalizeDate = NULL " : (" ,FinalizeDate = '" + (DateTime.ParseExact(pFinalizeDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd") + "'");
            _UpdateClause += pickupDetails_SaveParamters.pHeaderNotes == "0" ? ",Notes=null" : (",Notes=N'" + pickupDetails_SaveParamters.pHeaderNotes + "'") + " \n";
            _UpdateClause += pickupDetails_SaveParamters.pOperationID == 0 ? ",OperationID=null" : (",OperationID=" + pickupDetails_SaveParamters.pOperationID) + " \n";
            _UpdateClause += pickupDetails_SaveParamters.pPersonInChargeID == 0 ? ",PersonInChargeID=null" : (",PersonInChargeID=" + pickupDetails_SaveParamters.pPersonInChargeID) + " \n";
            _UpdateClause += pickupDetails_SaveParamters.pRMANumber == "0" ? ",RMANumber=null" : (",RMANumber=N'" + pickupDetails_SaveParamters.pRMANumber + "'") + " \n";
            _UpdateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
            _UpdateClause += " , ModificationDate = GETDATE() ";
            _UpdateClause += " WHERE ID = " + pickupDetails_SaveParamters.pPickupID;
            checkException = objCWH_Pickup.UpdateList(_UpdateClause);
            #endregion Save PickupHeader
            #region Save Details
            if (checkException == null)
            {
                objCVarWH_PickupDetails.ID = pickupDetails_SaveParamters.pPickupDetailsID;
                objCVarWH_PickupDetails.PickupID = pickupDetails_SaveParamters.pPickupID;
                objCVarWH_PickupDetails.PurchaseItemID = pickupDetails_SaveParamters.pPurchaseItemID;
                objCVarWH_PickupDetails.DemandedQuantity = pickupDetails_SaveParamters.pDemandedQuantity;
                objCVarWH_PickupDetails.Notes = pickupDetails_SaveParamters.pNotes;
                if (pickupDetails_SaveParamters.pPickupDetailsID != 0) //update so save original creator & creation date
                {
                    CWH_PickupDetails objCGetCreationInformation = new CWH_PickupDetails();
                    objCGetCreationInformation.GetItem(pickupDetails_SaveParamters.pPickupDetailsID);
                    objCVarWH_PickupDetails.CreatorUserID = objCGetCreationInformation.lstCVarWH_PickupDetails[0].CreatorUserID;
                    objCVarWH_PickupDetails.CreationDate = objCGetCreationInformation.lstCVarWH_PickupDetails[0].CreationDate;
                }
                else
                {
                    objCVarWH_PickupDetails.CreatorUserID = WebSecurity.CurrentUserId;
                    objCVarWH_PickupDetails.CreationDate = DateTime.Now;
                }
                objCVarWH_PickupDetails.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarWH_PickupDetails.ModificationDate = DateTime.Now;

                objCWH_PickupDetails.lstCVarWH_PickupDetails.Add(objCVarWH_PickupDetails);
                checkException = objCWH_PickupDetails.SaveMethod(objCWH_PickupDetails.lstCVarWH_PickupDetails);
                if (checkException == null)
                {
                    pPickupDetailsID = objCVarWH_PickupDetails.ID;
                    #region Save PickupDetailsLocations
                    var _ArrReceiveDetailsID = pickupDetails_SaveParamters.pReceiveDetailsIDList.Split(',');
                    int _ReceiveDetailsIDLength = _ArrReceiveDetailsID.Length;
                    var _ArrPickedQuantity = pickupDetails_SaveParamters.pReceiveDetailsPickedQuantityList.Split(',');
                    CWH_ReceiveDetails objCWH_ReceiveDetails = new CWH_ReceiveDetails();
                    CvwWH_ReceiveDetails objCvwWH_ReceiveDetails = new CvwWH_ReceiveDetails();
                    CvwWH_PickupDetailsLocation objCvwWH_PickupDetailsLocation = new CvwWH_PickupDetailsLocation();
                    for (int i = 0; i < _ReceiveDetailsIDLength; i++)
                    {
                        CWH_PickupDetailsLocation objCWH_PickupDetailsLocation = new CWH_PickupDetailsLocation();
                        CVarWH_PickupDetailsLocation objCVarWH_PickupDetailsLocation = new CVarWH_PickupDetailsLocation();
                        checkException = objCvwWH_ReceiveDetails.GetListPaging(999999, 1, "WHERE ID=" + _ArrReceiveDetailsID[i], "ID", out _RowCount);
                        checkException = objCWH_PickupDetailsLocation.GetListPaging(999999, 1, "WHERE PickupDetailsID=" + pickupDetails_SaveParamters.pPickupDetailsID + " AND ReceiveDetailsID=" + _ArrReceiveDetailsID[i], "ID", out _RowCount);
                        checkException = objCvwWH_PickupDetailsLocation.GetListPaging(999999, 1, "WHERE PickupDetailsID=" + pickupDetails_SaveParamters.pPickupDetailsID + " AND ReceiveDetailsID=" + _ArrReceiveDetailsID[i], "ID", out _RowCount);
                        CWH_ReceiveDetailsSerial objCWH_ReceiveDetailsSerial = new CWH_ReceiveDetailsSerial();
                        if (_RowCount > 0)
                        { //if number of checked serials is not equal to picked quantity or no serials then abort
                            int _TempRowCount = 0;
                            checkException = objCWH_ReceiveDetailsSerial.GetListPaging(999999, 1, "WHERE PickupDetailsLocationID=" + objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation[0].ID, "ID", out _TempRowCount);
                            if (objCWH_ReceiveDetailsSerial.lstCVarWH_ReceiveDetailsSerial.Count > 0
                                && decimal.Parse(_ArrPickedQuantity[i]) < objCWH_ReceiveDetailsSerial.lstCVarWH_ReceiveDetailsSerial.Count) 
                                _MessageReturned = "Please, checked number of selected serials for location " + objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation[0].LocationCode + ".";
                        }

                        if (decimal.Parse(_ArrPickedQuantity[i]) == 0 && _RowCount > 0 && _MessageReturned == "") //i have a picked item from location turned to 0 so delete
                        {
                            checkException = objCWH_PickupDetailsLocation.DeleteList("WHERE ID=" + objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation[0].ID);
                        }
                        else if (decimal.Parse(_ArrPickedQuantity[i]) > 0 && _MessageReturned =="") //i have quantity so either insert or update pickupLocation
                        {
                            if (objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation.Count == 0)
                            {
                                objCVarWH_PickupDetailsLocation.ID = 0;
                                objCVarWH_PickupDetailsLocation.CreatorUserID = WebSecurity.CurrentUserId;
                                objCVarWH_PickupDetailsLocation.CreationDate = objCVarWH_PickupDetailsLocation.ModificationDate = DateTime.Now;
                                //objCVarWH_PickupDetailsLocation.VehicleActionID = pVehicleActionID;
                                //objCVarWH_PickupDetailsLocation.PickedQuantity = decimal.Parse(_ArrPickedQuantity[i]);
                            }
                            else //Update
                            {
                                objCVarWH_PickupDetailsLocation.ID = objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation[0].ID;
                                objCVarWH_PickupDetailsLocation.CreatorUserID = objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation[0].CreatorUserID;
                                objCVarWH_PickupDetailsLocation.CreationDate = objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation[0].CreationDate;
                                objCVarWH_PickupDetailsLocation.VehicleActionID = objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation[0].VehicleActionID;
                                //objCVarWH_PickupDetailsLocation.PickedQuantity = objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation[0].PickedQuantity + decimal.Parse(_ArrPickedQuantity[i]);
                            }
                            objCVarWH_PickupDetailsLocation.PickedQuantity = decimal.Parse(_ArrPickedQuantity[i]);
                            objCVarWH_PickupDetailsLocation.PickupDetailsID = pPickupDetailsID;
                            objCVarWH_PickupDetailsLocation.ReceiveDetailsID = Int64.Parse(_ArrReceiveDetailsID[i]);
                            objCVarWH_PickupDetailsLocation.Notes = "0";
                            objCVarWH_PickupDetailsLocation.ModificatorUserID = WebSecurity.CurrentUserId;
                            objCVarWH_PickupDetailsLocation.ModificationDate = DateTime.Now;

                            //CWH_PickupDetailsLocation objCWH_PickupDetailsLocation = new CWH_PickupDetailsLocation();
                            objCWH_PickupDetailsLocation.lstCVarWH_PickupDetailsLocation.Add(objCVarWH_PickupDetailsLocation);
                            checkException = objCWH_PickupDetailsLocation.SaveMethod(objCWH_PickupDetailsLocation.lstCVarWH_PickupDetailsLocation);
                        } //else if (decimal.Parse(_ArrPickedQuantity[i]) > 0)
                        if (checkException != null)
                            _MessageReturned = checkException.Message;
                    } //for (int i=0;i<_ReceiveDetailsIDLength; i++) {

                    #endregion Save PickupDetailsLocations
                }
                checkException = objCvwWH_Pickup.GetList("WHERE ID=" + pickupDetails_SaveParamters.pPickupID.ToString());
                checkException = objCvwWH_PickupDetails.GetList("WHERE PickupID=" + pickupDetails_SaveParamters.pPickupID.ToString() + " ORDER BY ID");
            }
            #endregion Save Details

            return new object[] {
                checkException == null ? true : false
                , new JavaScriptSerializer().Serialize(objCvwWH_PickupDetails.lstCVarvwWH_PickupDetails) //pData[1]
                , new JavaScriptSerializer().Serialize(objCvwWH_Pickup.lstCVarvwWH_Pickup[0]) //pData[2]
                , pPickupDetailsID //pData[3]
                , _MessageReturned //pData[4]
            };
        }

        [HttpGet, HttpPost]
        public object[] PickupDetails_FillModalTables(Int64 pPickupID, Int64 pPurchaseItemID, string pWhereClauseReceiveDetails)
        {
            int _RowCount = 0;
            Exception checkException = null;
            Int64 pPickupDetailsID = 0;
            CvwWH_ReceiveDetails objCvwWH_ReceiveDetails = new CvwWH_ReceiveDetails();
            CvwWH_PickupDetailsLocation objCvwWH_PickupDetailsLocation = new CvwWH_PickupDetailsLocation();
            CvwWH_PickupDetails objCvwWH_PickupDetails = new CvwWH_PickupDetails();
            checkException = objCvwWH_PickupDetails.GetListPaging(99999, 1, "WHERE PickupID=" + pPickupID + " AND PurchaseItemID=" + pPurchaseItemID, "ID", out _RowCount);
            if (_RowCount > 0)
                pPickupDetailsID = objCvwWH_PickupDetails.lstCVarvwWH_PickupDetails[0].ID;
            checkException = objCvwWH_ReceiveDetails.GetListPaging(99999, 1, pWhereClauseReceiveDetails, "LocationCode", out _RowCount);
            checkException = objCvwWH_PickupDetailsLocation.GetListPaging(99999, 1, "WHERE PickupID=" + pPickupID + " AND PurchaseItemID=" + pPurchaseItemID, "LocationCode", out _RowCount);
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                serializer.Serialize(objCvwWH_ReceiveDetails.lstCVarvwWH_ReceiveDetails) //pData[0]
                , serializer.Serialize(objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation) //pData[1]
                , pPickupDetailsID == 0 ? null : serializer.Serialize(objCvwWH_PickupDetails.lstCVarvwWH_PickupDetails[0])
            };
        }

        [HttpGet, HttpPost]
        public object[] PickupDetails_GetAvailablePurchaseItemsForCustomer(Int32 pCustomerID, bool pIsExcludeVehicle)
        {
            CvwWH_ReceiveDetails objCvwWH_ReceiveDetails = new CvwWH_ReceiveDetails();
            Exception checkException = null;
            int _RowCount = 0;
            checkException = objCvwWH_ReceiveDetails.GetListPaging(999999, 1, "WHERE CustomerID=" + pCustomerID + " AND Quantity>PickedQuantity AND IsFinalized=1 " + (pIsExcludeVehicle ? " AND OperationVehicleID IS NULL " : ""), "ID", out _RowCount);
            var pPurchaseItemList = objCvwWH_ReceiveDetails.lstCVarvwWH_ReceiveDetails
                .Select(s => new
                {
                    ID = s.PurchaseItemID
                    ,
                    Code = s.PurchaseItemCode
                    ,
                    Name = s.PurchaseItemName
                })
                .Distinct().OrderBy(o => o.Code).ToList();
            return new object[] {
                new JavaScriptSerializer().Serialize(pPurchaseItemList) //pPurchaseItemList = pData[0]
            };
        }

        [HttpGet, HttpPost]
        public object[] PickupDetails_Delete(string pPickupDetailsIDsToDelete, Int64 pPickupID)
        {
            Exception checkException = null;
            bool _Result = true;
            CWH_PickupDetails objCWH_PickupDetails = new CWH_PickupDetails();
            CvwWH_PickupDetails objCvwWH_PickupDetails = new CvwWH_PickupDetails();
            CvwWH_Pickup objCvwWH_Pickup = new CvwWH_Pickup();
            int _NumberOfRecords = pPickupDetailsIDsToDelete.Split(',').Length;
            for (int i = 0; i < _NumberOfRecords; i++)
            {
                Int64 _PickupDetailsID = Int64.Parse(pPickupDetailsIDsToDelete.Split(',')[i]);
                checkException = objCWH_PickupDetails.DeleteList("WHERE ID=" + _PickupDetailsID.ToString());
                if (checkException != null)
                    _Result = false;
            }
            objCvwWH_PickupDetails.GetList("WHERE PickupID=" + pPickupID.ToString());
            objCvwWH_Pickup.GetList("WHERE ID=" + pPickupID.ToString());
            return new object[] {
                _Result
                , new JavaScriptSerializer().Serialize(objCvwWH_PickupDetails.lstCVarvwWH_PickupDetails) //pData[1]
                , new JavaScriptSerializer().Serialize(objCvwWH_Pickup.lstCVarvwWH_Pickup[0]) //pData[2]
            };
        }

        [HttpGet, HttpPost]
        public object[] PickupDetails_Export(Int32 pPageNumber, Int32 pPageSize, string pWhereClauseExportPickupDetails, string pOrderBy
            ,string pCalledFrom)
        {
            int _RowCount = 0;
            Exception checkException = null;
            CvwWH_PickupDetails objvwCWH_PickupDetails = new CvwWH_PickupDetails();
            CvwWH_PickupDetailsLocation objvwCWH_PickupDetailsLocation = new CvwWH_PickupDetailsLocation();
            object pPickupDetailsList = null;
            if (pCalledFrom == "WithLocationDetails")
            {
                checkException = objvwCWH_PickupDetailsLocation.GetListPaging(pPageSize, pPageNumber, pWhereClauseExportPickupDetails, pOrderBy, out _RowCount);
                pPickupDetailsList = objvwCWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation
                //.GroupBy(g => g.PickupID)
                .Select(s => new
                {
                    PickupCode = s.Code
                    ,
                    ProductCode = s.PurchaseItemCode//s.First().PurchaseItemCode
                    ,
                    ProductName = s.PurchaseItemName
                    ,
                    Quantity = s.PickedQuantity
                    ,
                    IsFinalized = s.IsFinalized
                    ,
                    Notes = s.Notes
                    ,
                    LocationCode = s.LocationCode
                    ,
                    PalletID = s.PalletID
                    ,
                    LotNo = s.LotNo //Serial
                }).ToList();
            }
            else
            {
                checkException = objvwCWH_PickupDetails.GetListPaging(pPageSize, pPageNumber, pWhereClauseExportPickupDetails, pOrderBy, out _RowCount);
                pPickupDetailsList = objvwCWH_PickupDetails.lstCVarvwWH_PickupDetails
                    //.GroupBy(g => g.PickupID)
                    .Select(s => new
                    {
                        PickupCode = s.Code
                        ,
                        ProductCode = s.PurchaseItemCode//s.First().PurchaseItemCode
                        ,
                        ProductName = s.PurchaseItemName
                        ,
                        Quantity = s.PickedQuantity
                        ,
                        IsFinalized = s.IsFinalized
                        ,
                        Notes = s.Notes
                    }).ToList();
            }
            return new object[] {
                new JavaScriptSerializer().Serialize(pPickupDetailsList)
            };
        }
        
        #endregion PickupDetails

        #region PickupDetailsSerial
        [HttpGet, HttpPost]
        public object[] PickupDetailsSerial_FillModal(Int64 pPickupDetailsLocationID, Int64 pReceiveDetailsID)
        {
            CWH_ReceiveDetailsSerial objCWH_ReceiveDetailsSerial = new CWH_ReceiveDetailsSerial();
            objCWH_ReceiveDetailsSerial.GetList("WHERE ReceiveDetailsID="+pReceiveDetailsID
                + " AND (PickupDetailsLocationID IS NULL OR PickupDetailsLocationID= "+pPickupDetailsLocationID+ ")"
                + " ORDER BY Serial");
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                serializer.Serialize(objCWH_ReceiveDetailsSerial.lstCVarWH_ReceiveDetailsSerial)
            };
        }
        [HttpGet, HttpPost]
        public object[] PickupDetailsSerial_Save([FromBody] PickupDetailsSerial_SaveParameters pickupDetailsSerial_SaveParameters)
        {
            string _MessageReturned = "";
            Exception checkException = null;
            CWH_ReceiveDetailsSerial objCWH_ReceiveDetailsSerial = new CWH_ReceiveDetailsSerial();
            Int64 _PickupDetailsLocationID = pickupDetailsSerial_SaveParameters.pPickupDetailsLocationID;
            checkException = objCWH_ReceiveDetailsSerial.UpdateList("PickupDetailsLocationID=NULL WHERE PickupDetailsLocationID=" + pickupDetailsSerial_SaveParameters.pPickupDetailsLocationID);
            if (checkException == null)
                checkException = objCWH_ReceiveDetailsSerial.UpdateList("PickupDetailsLocationID=" + _PickupDetailsLocationID + " WHERE ID IN (" + pickupDetailsSerial_SaveParameters.pReceiveDetailsSerialIDList + ")");
            else
                _MessageReturned = checkException.Message;
            return new object[] {
                _MessageReturned
            };
        }
        #endregion PickupDetailsSerial

        #region PickupDetailsLocation
        [HttpGet, HttpPost]
        public object PickupDetailsLocation_PrintPDI(string pWhereClausePickupDetailsLocation)
        {
            string _ReturnedMessage = "";
            int _RowCount = 0;
            Exception checkException = null;

            CvwWH_PickupDetailsLocation objCvwWH_PickupDetailsLocation = new CvwWH_PickupDetailsLocation();
            checkException = objCvwWH_PickupDetailsLocation.GetListPaging(999999, 1, pWhereClausePickupDetailsLocation, "ID", out _RowCount);

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[]
            {
                _ReturnedMessage
                , serializer.Serialize(objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation)
            };
        }
        #endregion PickupDetailsLocation

        #region ImportPickupFromExcel
        [HttpGet, HttpPost]
        public object[] PickupDetails_ImportFromExcel([FromBody] PickupExcelParam pickupExcelParam)
        {
            string pReturnedMessage = "";
            Exception checkException = null;
            int _RowCount = 0;

            var arrPurchaseItemCode = pickupExcelParam.pPurchaseItemCodeList.Split(',');
            var arrFromLocationCode = pickupExcelParam.pFromLocationList.Split(',');
            var arrQuantity = pickupExcelParam.pQuantityList.Split(',');
            int numberOfRows = arrPurchaseItemCode.Length;
            Int64 pPickupDetailsID = 0;
            Int64 pReceiveDetailsID = 0;
            string pWhereClause = "";
            string pUpdateClause = "";

            CvwWH_ReceiveDetails objCvwWH_ReceiveDetails = new CvwWH_ReceiveDetails();
            CvwWH_RowLocation objCvwWH_RowLocation = new CvwWH_RowLocation();
            CPurchaseItem objCPurchaseItem = new CPurchaseItem();
            CWH_Pickup objCWH_Pickup = new CWH_Pickup();
            CVarWH_PickupDetails objCVarWH_PickupDetails = new CVarWH_PickupDetails();
            #region check eligibility [1st iteration / it doesn't handle if the quantity exceeded in multiple rows so check again in second iteration]
            for (int i = 0; i < numberOfRows; i++)
            {
                #region Check data is written correctly
                checkException = objCPurchaseItem.GetList("WHERE Code=N'" + arrPurchaseItemCode[i] + "'");
                checkException = objCvwWH_RowLocation.GetList("WHERE WarehouseID=" + pickupExcelParam.pWarehouseID + " AND Code=N'" + arrFromLocationCode[i] + "'");
                if (objCvwWH_RowLocation.lstCVarvwWH_RowLocation.Count == 0
                    || objCPurchaseItem.lstCVarPurchaseItem.Count == 0)
                {
                    pReturnedMessage = "Please check item and locations at row " + (i + 2);
                    return new object[] { pReturnedMessage };
                }
                #endregion Check data is written correctly
                #region check ReceiveDetails is found
                else
                {
                    pReceiveDetailsID = PickupCheckEnoughQuantity(pickupExcelParam.pCustomerID
                        , objCvwWH_RowLocation.lstCVarvwWH_RowLocation[0].ID
                        , objCPurchaseItem.lstCVarPurchaseItem[0].ID, int.Parse(arrQuantity[i]));
                    if (pReceiveDetailsID == 0)
                    {
                        pReturnedMessage = "Please, check row " + (i + 2);
                        return new object[] { pReturnedMessage };
                    }
                }
                #endregion check ReceiveDetails is found
            } //for (int i = 0; i < numberOfRows; i++)
            #endregion check eligibility

            #region Save Header
            pUpdateClause = "CustomerID=" + pickupExcelParam.pCustomerID + " \n";
            pUpdateClause += ",WarehouseID=" + pickupExcelParam.pWarehouseID + " \n";
            pUpdateClause += "WHERE ID=" + pickupExcelParam.pPickupID + " \n";
            checkException = objCWH_Pickup.UpdateList(pUpdateClause);
            #endregion Save Header

            #region Picking
            for (int i = 0; i < numberOfRows; i++)
            {
                checkException = objCPurchaseItem.GetList("WHERE Code=N'" + arrPurchaseItemCode[i] + "'");
                checkException = objCvwWH_RowLocation.GetList("WHERE WarehouseID=" + pickupExcelParam.pWarehouseID + " AND Code=N'" + arrFromLocationCode[i] + "'");
                Int64 currentPurchaseItemID = objCPurchaseItem.lstCVarPurchaseItem[0].ID;
                Int32 currentLocationID = objCvwWH_RowLocation.lstCVarvwWH_RowLocation[0].ID;

                #region check current quantity
                pReceiveDetailsID = PickupCheckEnoughQuantity(pickupExcelParam.pCustomerID
                    , objCvwWH_RowLocation.lstCVarvwWH_RowLocation[0].ID
                    , objCPurchaseItem.lstCVarPurchaseItem[0].ID, int.Parse(arrQuantity[i]));
                if (pReceiveDetailsID == 0)
                {
                    pReturnedMessage = "Stopped at row " + (i + 2) + ". Previous rows are saved, please remove them and check quantities for the rest.";
                    return new object[] { pReturnedMessage };
                }
                #endregion check current quantity

                #region Row is OK so get needed data
                pWhereClause = "WHERE PickupID=" + pickupExcelParam.pPickupID + " \n";
                pWhereClause += "AND PurchaseItemID=" + currentPurchaseItemID + " \n";
                CWH_PickupDetails objCWH_PickupDetails = new CWH_PickupDetails();
                checkException = objCWH_PickupDetails.GetList(pWhereClause);
                
                if (objCWH_PickupDetails.lstCVarWH_PickupDetails.Count == 0) //Insert
                    pPickupDetailsID = 0;
                else //Update
                    pPickupDetailsID = objCWH_PickupDetails.lstCVarWH_PickupDetails[0].ID;
                #endregion Row is OK

                #region SavePickupDetailsHeader
                objCVarWH_PickupDetails.ID = pPickupDetailsID;
                objCVarWH_PickupDetails.PickupID = pickupExcelParam.pPickupID;
                objCVarWH_PickupDetails.PurchaseItemID = currentPurchaseItemID;
                if (pPickupDetailsID != 0) //update so save original creator & creation date
                {
                    CWH_PickupDetails objCGetCreationInformation = new CWH_PickupDetails();
                    objCGetCreationInformation.GetItem(pPickupDetailsID);
                    objCVarWH_PickupDetails.DemandedQuantity = (objCVarWH_PickupDetails.DemandedQuantity + int.Parse(arrQuantity[i]));
                    objCVarWH_PickupDetails.Notes = objCGetCreationInformation.lstCVarWH_PickupDetails[0].Notes;
                    objCVarWH_PickupDetails.CreatorUserID = objCGetCreationInformation.lstCVarWH_PickupDetails[0].CreatorUserID;
                    objCVarWH_PickupDetails.CreationDate = objCGetCreationInformation.lstCVarWH_PickupDetails[0].CreationDate;
                }
                else
                {
                    objCVarWH_PickupDetails.DemandedQuantity = int.Parse(arrQuantity[i]);
                    objCVarWH_PickupDetails.Notes = "0";
                    objCVarWH_PickupDetails.CreatorUserID = WebSecurity.CurrentUserId;
                    objCVarWH_PickupDetails.CreationDate = DateTime.Now;
                }
                objCVarWH_PickupDetails.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarWH_PickupDetails.ModificationDate = DateTime.Now;
                CWH_PickupDetails objCWH_PickupDetails_Save = new CWH_PickupDetails();
                objCWH_PickupDetails_Save.lstCVarWH_PickupDetails.Add(objCVarWH_PickupDetails);
                checkException = objCWH_PickupDetails_Save.SaveMethod(objCWH_PickupDetails_Save.lstCVarWH_PickupDetails);
                #endregion SavePickupDetailsHeader

                #region SavePickupDetailsLocation
                if (checkException == null)
                {
                    pPickupDetailsID = objCVarWH_PickupDetails.ID;
                    CWH_ReceiveDetails objCWH_ReceiveDetails = new CWH_ReceiveDetails();

                    CvwWH_PickupDetailsLocation objCvwWH_PickupDetailsLocation = new CvwWH_PickupDetailsLocation();
                    //for (int i = 0; i < _ReceiveDetailsIDLength; i++)
                    {
                        CWH_PickupDetailsLocation objCWH_PickupDetailsLocation = new CWH_PickupDetailsLocation();
                        CVarWH_PickupDetailsLocation objCVarWH_PickupDetailsLocation = new CVarWH_PickupDetailsLocation();
                        checkException = objCvwWH_ReceiveDetails.GetListPaging(999999, 1, "WHERE ID=" + pReceiveDetailsID, "ID", out _RowCount);
                        checkException = objCWH_PickupDetailsLocation.GetListPaging(999999, 1, "WHERE PickupDetailsID=" + pPickupDetailsID + " AND ReceiveDetailsID=" + pReceiveDetailsID, "ID", out _RowCount);
                        checkException = objCvwWH_PickupDetailsLocation.GetListPaging(999999, 1, "WHERE PickupDetailsID=" + pPickupDetailsID + " AND ReceiveDetailsID=" + pReceiveDetailsID, "ID", out _RowCount);
                        CWH_ReceiveDetailsSerial objCWH_ReceiveDetailsSerial = new CWH_ReceiveDetailsSerial();
                        if (_RowCount > 0)
                        { //if number of checked serials is not equal to picked quantity or no serials then abort
                            int _TempRowCount = 0;
                            checkException = objCWH_ReceiveDetailsSerial.GetListPaging(999999, 1, "WHERE PickupDetailsLocationID=" + objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation[0].ID, "ID", out _TempRowCount);
                            if (objCWH_ReceiveDetailsSerial.lstCVarWH_ReceiveDetailsSerial.Count > 0
                                && decimal.Parse(arrQuantity[i]) < objCWH_ReceiveDetailsSerial.lstCVarWH_ReceiveDetailsSerial.Count)
                            {
                                pReturnedMessage = "Please, checked number of selected serials for location " + objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation[0].LocationCode + " at row " + (i + 2);
                                return new object[] { pReturnedMessage };
                            }
                        }

                        if (decimal.Parse(arrQuantity[i]) == 0 && _RowCount > 0 && pReturnedMessage == "") //i have a picked item from location turned to 0 so delete
                        {
                            checkException = objCWH_PickupDetailsLocation.DeleteList("WHERE ID=" + objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation[0].ID);
                        }
                        else if (decimal.Parse(arrQuantity[i]) > 0 && pReturnedMessage == "") //i have quantity so either insert or update pickupLocation
                        {
                            if (objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation.Count == 0)
                            {
                                objCVarWH_PickupDetailsLocation.ID = 0;
                                objCVarWH_PickupDetailsLocation.CreatorUserID = WebSecurity.CurrentUserId;
                                objCVarWH_PickupDetailsLocation.CreationDate = objCVarWH_PickupDetailsLocation.ModificationDate = DateTime.Now;
                                //objCVarWH_PickupDetailsLocation.VehicleActionID = pVehicleActionID;
                                //objCVarWH_PickupDetailsLocation.PickedQuantity = decimal.Parse(_ArrPickedQuantity[i]);
                            }
                            else //Update
                            {
                                objCVarWH_PickupDetailsLocation.ID = objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation[0].ID;
                                objCVarWH_PickupDetailsLocation.CreatorUserID = objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation[0].CreatorUserID;
                                objCVarWH_PickupDetailsLocation.CreationDate = objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation[0].CreationDate;
                                objCVarWH_PickupDetailsLocation.VehicleActionID = objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation[0].VehicleActionID;
                                //objCVarWH_PickupDetailsLocation.PickedQuantity = objCvwWH_PickupDetailsLocation.lstCVarvwWH_PickupDetailsLocation[0].PickedQuantity + decimal.Parse(_ArrPickedQuantity[i]);
                            }
                            objCVarWH_PickupDetailsLocation.PickedQuantity = decimal.Parse(arrQuantity[i]);
                            objCVarWH_PickupDetailsLocation.PickupDetailsID = pPickupDetailsID;
                            objCVarWH_PickupDetailsLocation.ReceiveDetailsID = pReceiveDetailsID;
                            objCVarWH_PickupDetailsLocation.Notes = "0";
                            objCVarWH_PickupDetailsLocation.ModificatorUserID = WebSecurity.CurrentUserId;
                            objCVarWH_PickupDetailsLocation.ModificationDate = DateTime.Now;

                            //CWH_PickupDetailsLocation objCWH_PickupDetailsLocation = new CWH_PickupDetailsLocation();
                            objCWH_PickupDetailsLocation.lstCVarWH_PickupDetailsLocation.Add(objCVarWH_PickupDetailsLocation);
                            checkException = objCWH_PickupDetailsLocation.SaveMethod(objCWH_PickupDetailsLocation.lstCVarWH_PickupDetailsLocation);
                        } //else if (decimal.Parse(_ArrPickedQuantity[i]) > 0)
                        if (checkException != null)
                            pReturnedMessage = checkException.Message;
                    } //for (int i=0;i<_ReceiveDetailsIDLength; i++) {

                }

                #endregion SavePickupDetailsLocation

            } //for (int i = 0; i < numberOfRows; i++)
            #endregion Picking

            return new object[] {
                pReturnedMessage
            };
        }
        #endregion ImportPickupFromExcel

        #region Static Functions
        //[HttpGet, HttpPost]
        public static Int64 PickupCheckEnoughQuantity(Int32 pCustomerID, Int32 pLocationID
            , Int64 pPurchaseItemID, int pQuantity)
        {
            //string pReturnedMessage = "";
            string pWhereClause = "";
            int _RowCount = 0;
            Int64 pReceiveDetailsID = 0;
            Exception checkException = null;
            CvwWH_ReceiveDetails objCvwWH_ReceiveDetails = new CvwWH_ReceiveDetails();
            pWhereClause = "WHERE IsFinalized=1 AND OperationVehicleID IS NULL" + " \n";
            pWhereClause += "AND LocationID=" + pLocationID + " \n";
            pWhereClause += "AND PurchaseItemID=" + pPurchaseItemID + " \n";
            pWhereClause += "AND CustomerID=" + pCustomerID + " \n";
            //pWhereClause += "AND WarehouseID=" + pickupExcelParam.pWarehouseID + " \n"; //already LocationID fulfills
            pWhereClause += "AND (Quantity - FinalizedPickedQuantity) > " + pQuantity + " \n";

            checkException = objCvwWH_ReceiveDetails.GetListPaging(1, 1, pWhereClause, "ID", out _RowCount);
            if (_RowCount > 0)
                pReceiveDetailsID = objCvwWH_ReceiveDetails.lstCVarvwWH_ReceiveDetails[0].ID;
            return pReceiveDetailsID;
        }
        #endregion Static Functions
    }

    #region POST Parameters
    public class PickupDetails_SaveParamters
    {
        //HeaderParameters
        public Int64 pPickupID { get; set; }
        public Int32 pWarehouseID { get; set; }
        public Int32 pCustomerID { get; set; }
        public Int32 pBillTo { get; set; }
        public Int32 pEndUserID { get; set; }
        public string pOrderDate { get; set; }
        public string pRequiredDate { get; set; }
        public bool pIsFinalized { get; set; }
        public string pFinalizeDate { get; set; }
        public string pHeaderNotes { get; set; }
        public Int64 pOperationID { get; set; }
        public string pRMANumber { get; set; }
        public Int32 pPersonInChargeID { get; set; }
        //PickupDetailsParameters
        public Int64 pPickupDetailsID { get; set; }
        public Int64 pPurchaseItemID { get; set; }
        public Decimal pDemandedQuantity { get; set; }
        public string pNotes { get; set; }
        //PickupDetailsLocationsParameters
        public string pReceiveDetailsIDList { get; set; }
        public string pReceiveDetailsPickedQuantityList { get; set; }
    }
    public class PickupDetailsSerial_SaveParameters
    {
        public Int64 pPickupDetailsLocationID { get; set; }
        public Int64 pReceiveDetailsID { get; set; }
        public string pReceiveDetailsSerialIDList { get; set; }
    }
    public class PickupExcelParam
    {
        //public string pCustomerNameList { get; set; }
        public Int64 pPickupID;
        public Int32 pWarehouseID;
        public Int32 pCustomerID;
        public string pPurchaseItemCodeList { get; set; }
        public string pFromLocationList { get; set; }
        public string pQuantityList { get; set; }
    }
    #endregion POST Parameters
}
