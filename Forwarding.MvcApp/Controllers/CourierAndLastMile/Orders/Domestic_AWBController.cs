using Forwarding.MvcApp.Models.Accounting.MasterData.Generated;
using Forwarding.MvcApp.Models.Accounting.Transactions.Generated;
using Forwarding.MvcApp.Models.Administration.Security.Generated;
using Forwarding.MvcApp.Models.Administration.Settings.Generated;
using Forwarding.MvcApp.Models.Customized;
using Forwarding.MvcApp.Models.MasterData.BanksAccountsAndTreasuries.Generated;
using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;
using Forwarding.MvcApp.Models.MasterData.Locations.Generated;
using Forwarding.MvcApp.Models.MasterData.Others.Generated;
using Forwarding.MvcApp.Models.MasterData.Partners.Generated;
using Forwarding.MvcApp.Models.NoAccess.Generated;
using Forwarding.MvcApp.Models.Operations.Operations.Generated;
using Forwarding.MvcApp.Models.Warehousing.MasterData.Generated;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebMatrix.WebData;
using Forwarding.MvcApp.Models.MasterData.Others.Generated;
using MoreLinq;

namespace Forwarding.MvcApp.Controllers.Accounting.API_Transactions
{
    public class Domestic_AWBController : ApiController
    {
        [HttpGet, HttpPost]
        public object[] LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned(bool pIsLoadArrayOfObjects, string pLanguage, Int32 pPageNumber, Int32 pPageSize, string pWhereClause, string pOrderBy)
        {
            int _RowCount = 0;
            if (pOrderBy == "SecondTime")
            {
                pOrderBy = " ID DESC";

                CvwLM_Domestic_AWB CobjvwLM_Domestic_AWB = new CvwLM_Domestic_AWB();
                CobjvwLM_Domestic_AWB.GetListPaging(pPageSize, pPageNumber, pWhereClause, pOrderBy, out _RowCount);

                var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
                return new object[] {
                new JavaScriptSerializer().Serialize(CobjvwLM_Domestic_AWB.lstCVarvwLM_Domestic_AWB)
                , _RowCount
                };
            }
            else
            {
                CCities objCCities = new CCities();
                CPorts objCPorts = new CPorts();
                CTrackingStage objCTrackingStage = new CTrackingStage();
                CWH_MainWarehouses objCWH_MainWarehouses = new CWH_MainWarehouses();
                CPackageTypes objCPackageTypes = new CPackageTypes();
                CLM_Rates objCLM_Rates = new CLM_Rates();
                CCustody objCCustody = new CCustody();

                objCCities.GetListPaging(9999, 1, "WHERE 1=1", "Name, Code", out _RowCount);
                objCPorts.GetListPaging(9999, 1, "WHERE 1=1  and IsFactories=1", "Name, Code", out _RowCount);
                objCTrackingStage.GetList(" Where 1=1 order by ViewOrder");
                objCWH_MainWarehouses.GetList(" Where 1=1");
                objCPackageTypes.GetList(" Where 1=1");
                objCLM_Rates.GetList(" Where 1=1");
                objCCustody.GetList(" Where 1=1");

                CvwLM_Domestic_AWB CobjvwLM_Domestic_AWB = new CvwLM_Domestic_AWB();
                CobjvwLM_Domestic_AWB.GetListPaging(pPageSize, pPageNumber, pWhereClause, pOrderBy, out _RowCount);

                var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
                return new object[] {
                new JavaScriptSerializer().Serialize(CobjvwLM_Domestic_AWB.lstCVarvwLM_Domestic_AWB)
                , _RowCount
                , pIsLoadArrayOfObjects ?  serializer.Serialize(objCCities.lstCVarCities) : null
                , pIsLoadArrayOfObjects ?  serializer.Serialize(objCPorts.lstCVarPorts) : null
                , serializer.Serialize(objCTrackingStage.lstCVarTrackingStage)
                , serializer.Serialize(objCWH_MainWarehouses.lstCVarWH_MainWarehouses)
                , serializer.Serialize(objCPackageTypes.lstCVarPackageTypes)//6
                , serializer.Serialize(objCLM_Rates.lstCVarLM_Rates)//7
                , serializer.Serialize(objCCustody.lstCVarCustody)//8
                
                };
            }

        }

        [HttpGet, HttpPost]
        public object[] RunnerTransactionLoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned(bool pIsLoadArrayOfObjects, string pLanguage, Int32 pPageNumber, Int32 pPageSize, string pWhereClause, string pOrderBy,int pTrackingStageID)
        {
            int _RowCount = 0;
            if (pOrderBy == "SecondTime")
            {
                CTrackingStage objCTrackingStage = new CTrackingStage();
                if(pTrackingStageID > 0)
                    objCTrackingStage.GetList("Where ID = " + pTrackingStageID);
                
                pOrderBy = " ID DESC";
                if (objCTrackingStage.lstCVarTrackingStage[0].IsShipper == true && objCTrackingStage.lstCVarTrackingStage[0].IsConsignee == true)
                    pWhereClause += "   AND ( RunnerFromShipperID= (SELECT ID FROM Custody AS c WHERE c.UserID = " + WebSecurity.CurrentUserId + ")  OR RunnerToConsigneeID= (SELECT ID FROM Custody AS c WHERE c.UserID = " + WebSecurity.CurrentUserId + ")  )";
                else if (objCTrackingStage.lstCVarTrackingStage[0].IsShipper == true)
                    pWhereClause += "   AND RunnerFromShipperID= (SELECT ID FROM Custody AS c WHERE c.UserID = " + WebSecurity.CurrentUserId + ")";
                else if (objCTrackingStage.lstCVarTrackingStage[0].IsConsignee == true)
                    pWhereClause += "   AND RunnerToConsigneeID= (SELECT ID FROM Custody AS c WHERE c.UserID = " + WebSecurity.CurrentUserId + ")";
                CvwLM_Domestic_AWB CobjvwLM_Domestic_AWB = new CvwLM_Domestic_AWB();
                CobjvwLM_Domestic_AWB.GetListPaging(10000, pPageNumber, pWhereClause, pOrderBy, out _RowCount);
                
                    string AWBIDs = string.Join(",", CobjvwLM_Domestic_AWB.lstCVarvwLM_Domestic_AWB.Select(x => x.ID));
                //int RowsCount = CobjvwLM_Domestic_AWB.lstCVarvwLM_Domestic_AWB.Count;

                var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
                return new object[] {
                new JavaScriptSerializer().Serialize(CobjvwLM_Domestic_AWB.lstCVarvwLM_Domestic_AWB)
                , _RowCount
                , AWBIDs
                };
            }
            else
            {
                CCities objCCities = new CCities();
                CPorts objCPorts = new CPorts();
                CTrackingStage objCTrackingStage = new CTrackingStage();
                CWH_MainWarehouses objCWH_MainWarehouses = new CWH_MainWarehouses();

                objCCities.GetListPaging(9999, 1, "WHERE 1=1", "Name, Code", out _RowCount);
                objCPorts.GetListPaging(9999, 1, "WHERE 1=1 and IsFactories=1", "Name, Code", out _RowCount);
                objCTrackingStage.GetList(" Where 1=1 order by ViewOrder");
                objCWH_MainWarehouses.GetList(" Where 1=1");

                CvwLM_Domestic_AWB CobjvwLM_Domestic_AWB = new CvwLM_Domestic_AWB();
                //CobjvwLM_Domestic_AWB.GetListPaging(pPageSize, pPageNumber, pWhereClause, pOrderBy, out _RowCount);

                var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
                return new object[] {
                new JavaScriptSerializer().Serialize(CobjvwLM_Domestic_AWB.lstCVarvwLM_Domestic_AWB)
                , _RowCount
                , pIsLoadArrayOfObjects ?  serializer.Serialize(objCCities.lstCVarCities) : null
                , pIsLoadArrayOfObjects ?  serializer.Serialize(objCPorts.lstCVarPorts) : null
                , serializer.Serialize(objCTrackingStage.lstCVarTrackingStage)
                , serializer.Serialize(objCWH_MainWarehouses.lstCVarWH_MainWarehouses)
                };
            }

        }
        
        [HttpGet, HttpPost]
        public object[] ReloadAWBsTrackingStages(string pWhereClause)
        {
            int _RowCount = 0;
            CTrackingStage objCTrackingStage = new CTrackingStage();

            string pOrderBy = " ID DESC";
            //if (objCTrackingStage.lstCVarTrackingStage[0].IsShipper == true && objCTrackingStage.lstCVarTrackingStage[0].IsConsignee == true)
                pWhereClause += "   AND ( (  TrackingStageOrder < 4 and RunnerFromShipperID= (SELECT ID FROM Custody AS c WHERE c.UserID = " + WebSecurity.CurrentUserId + ") ) OR  (  TrackingStageOrder >= 4 and RunnerToConsigneeID= (SELECT ID FROM Custody AS c WHERE c.UserID = " + WebSecurity.CurrentUserId + ") ) )";
            //else if (objCTrackingStage.lstCVarTrackingStage[0].IsShipper == true)
            //    pWhereClause += "   AND RunnerFromShipperID= (SELECT ID FROM Custody AS c WHERE c.UserID = " + WebSecurity.CurrentUserId + ")";
            //else if (objCTrackingStage.lstCVarTrackingStage[0].IsConsignee == true)
            //    pWhereClause += "   AND RunnerToConsigneeID= (SELECT ID FROM Custody AS c WHERE c.UserID = " + WebSecurity.CurrentUserId + ")";
            CvwLM_Domestic_AWB CobjvwLM_Domestic_AWB = new CvwLM_Domestic_AWB();
            CobjvwLM_Domestic_AWB.GetListPaging(10000, 1, pWhereClause, pOrderBy, out _RowCount);
            var TrackingStage_AWBs = CobjvwLM_Domestic_AWB.lstCVarvwLM_Domestic_AWB.GroupBy(x => new { x.TrackingStageID, x.TrackingStageName, x.TrackingStageOrder })
                 .Select(g => new { g.Key.TrackingStageID, g.Key.TrackingStageName, g.Key.TrackingStageOrder, AWBsCount = g.Count() , AWBIDs = string.Join(",", g.Select(kvp => kvp.ID)) });

            string AWBIDs = string.Join(",", CobjvwLM_Domestic_AWB.lstCVarvwLM_Domestic_AWB.Select(x => x.ID));
            //int RowsCount = CobjvwLM_Domestic_AWB.lstCVarvwLM_Domestic_AWB.Count;

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
            new JavaScriptSerializer().Serialize(CobjvwLM_Domestic_AWB.lstCVarvwLM_Domestic_AWB)
            , _RowCount
            , AWBIDs
            , new JavaScriptSerializer().Serialize(TrackingStage_AWBs)//3
            };
            
        }


        [HttpGet, HttpPost]
        public Object[] LoadRunnerTransaction_AWBsByIDs(string pWhereClause)
        {
            CvwLM_Domestic_AWB objvwLM_Domestic_AWB = new CvwLM_Domestic_AWB();
            objvwLM_Domestic_AWB.GetList(pWhereClause);

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] { serializer.Serialize(objvwLM_Domestic_AWB.lstCVarvwLM_Domestic_AWB)  };
        }
        [HttpGet, HttpPost]
        public Object[] LoadCustomerWithFilter(string pWhereClause)
        {
            Exception checkException;
            CvwCustomers objCvwCustomers = new CvwCustomers();
            checkException = objCvwCustomers.GetList(pWhereClause);

            CvwAddresses objCvwAddresses = new CvwAddresses();
            objCvwAddresses.GetList(" Where PartnerID = " + objCvwCustomers.lstCVarvwCustomers[0].ID);
            CContacts objCContacts = new CContacts();
            objCContacts.GetList(" Where PartnerID = " + objCvwCustomers.lstCVarvwCustomers[0].ID);
            
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] {serializer.Serialize(objCvwCustomers.lstCVarvwCustomers),
                                 serializer.Serialize(objCvwAddresses.lstCVarvwAddresses) ,
                                 serializer.Serialize(objCContacts.lstCVarContacts)
            };
        }


        [HttpGet, HttpPost]
        public object[] LoadShipperAndConsignee(int pShipperID ,int pConsigneeID)
        {
            CvwCustomers CvwCustomers_Shipper = new CvwCustomers();
            CvwCustomers CvwCustomers_Consignee = new CvwCustomers();
            CvwCustomers_Shipper.GetList(" Where ID = " + pShipperID);
            CvwCustomers_Consignee.GetList(" Where ID = " + pConsigneeID);

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                  serializer.Serialize(CvwCustomers_Shipper.lstCVarvwCustomers)
                , serializer.Serialize(CvwCustomers_Consignee.lstCVarvwCustomers) 
            };
        }

        [HttpGet, HttpPost]
        public bool Save([FromBody] SaveParameters saveParameters)
        {
            bool _result = false;
            CLM_Domestic_AWB ExistsCLM_Domestic_AWB = new CLM_Domestic_AWB();
            CVarLM_Domestic_AWB objCVarLM_Domestic_AWB = new CVarLM_Domestic_AWB();
            if (saveParameters.pID > 0)
            {
                ExistsCLM_Domestic_AWB.GetList(" Where ID = " + saveParameters.pID);
                if (ExistsCLM_Domestic_AWB.lstCVarLM_Domestic_AWB.Count > 0)
                    objCVarLM_Domestic_AWB = ExistsCLM_Domestic_AWB.lstCVarLM_Domestic_AWB[0];
            }
            //pAWBNumber pCustomerShipperID pCustomerConsigneeID pActWgt pDimWgt pChgWgt pPcs pCODAmount pDescription pRemarks
            objCVarLM_Domestic_AWB.ID = saveParameters.pID;
            objCVarLM_Domestic_AWB.AWBNumber = saveParameters.pAWBNumber;
            objCVarLM_Domestic_AWB.ShipperID = saveParameters.pCustomerShipperID;
            objCVarLM_Domestic_AWB.ConsigneeID = 0;//saveParameters.pCustomerConsigneeID;
            objCVarLM_Domestic_AWB.ActWgt = saveParameters.pActWgt;
            objCVarLM_Domestic_AWB.DimWgt = saveParameters.pDimWgt;
            objCVarLM_Domestic_AWB.ChgWgt = saveParameters.pChgWgt;
            objCVarLM_Domestic_AWB.Pcs = saveParameters.pPcs;
            objCVarLM_Domestic_AWB.CODAmount = saveParameters.pCODAmount;
            objCVarLM_Domestic_AWB.Description = saveParameters.pDescription;
            objCVarLM_Domestic_AWB.Remarks = saveParameters.pRemarks;

            objCVarLM_Domestic_AWB.ConsigneeName = saveParameters.pConsigneeName;
            objCVarLM_Domestic_AWB.ConsigneeCityID = saveParameters.pConsigneeCityID;
            objCVarLM_Domestic_AWB.ConsigneeRegionID = saveParameters.pConsigneeRegionID;
            objCVarLM_Domestic_AWB.ConsigneePhone1 = saveParameters.pConsigneePhone1;
            objCVarLM_Domestic_AWB.ConsigneePhone2 = saveParameters.pConsigneePhone2;
            objCVarLM_Domestic_AWB.ConsigneeCompanyName = saveParameters.pConsigneeCompanyName;
            objCVarLM_Domestic_AWB.ConsigneeSenderName = saveParameters.pConsigneeSenderName;
            objCVarLM_Domestic_AWB.ConsigneeAccountNo = saveParameters.pConsigneeAccountNo;
            objCVarLM_Domestic_AWB.ConsigneeCity = saveParameters.pConsigneeCity;
            objCVarLM_Domestic_AWB.ConsigneeAddress = saveParameters.pConsigneeAddress;
            //ShipperName, ShipperCityID, ShipperRegionID, ShipperPhone1, ShipperPhone2, ShipperCompanyName, ShipperSenderName, ShipperAccountNo, ShipperCity, ShipperAddress
            objCVarLM_Domestic_AWB.ShipperName = saveParameters.pShipperName;
            objCVarLM_Domestic_AWB.ShipperCityID = saveParameters.pShipperCityID;
            objCVarLM_Domestic_AWB.ShipperRegionID = saveParameters.pShipperRegionID;
            objCVarLM_Domestic_AWB.ShipperPhone1 = saveParameters.pShipperPhone1;
            objCVarLM_Domestic_AWB.ShipperPhone2 = saveParameters.pShipperPhone2;
            objCVarLM_Domestic_AWB.ShipperCompanyName = saveParameters.pShipperCompanyName;
            objCVarLM_Domestic_AWB.ShipperSenderName = saveParameters.pShipperSenderName;
            objCVarLM_Domestic_AWB.ShipperAccountNo = saveParameters.pShipperAccountNo;
            objCVarLM_Domestic_AWB.ShipperCity = saveParameters.pShipperCity;
            objCVarLM_Domestic_AWB.ShipperAddress = saveParameters.pShipperAddress;

            objCVarLM_Domestic_AWB.PaymentTypeID = saveParameters.pPaymentTypeID;
            objCVarLM_Domestic_AWB.StoreID = saveParameters.pStoreID;
            objCVarLM_Domestic_AWB.PickupAddress = saveParameters.pPickupAddress;
            objCVarLM_Domestic_AWB.EstimatedReceivedDate_Custody = saveParameters.pEstimatedReceivedDate_Custody;
            objCVarLM_Domestic_AWB.ActualReceivedDate_Custody = saveParameters.pActualReceivedDate_Custody;
            objCVarLM_Domestic_AWB.EstimatedArrivalDateToStore = saveParameters.pEstimatedArrivalDateToStore;
            objCVarLM_Domestic_AWB.ActualArrivalDateToStore = saveParameters.pActualArrivalDateToStore;
            objCVarLM_Domestic_AWB.EstimatedDeliveryDateFrom = saveParameters.pEstimatedDeliveryDateFrom;
            objCVarLM_Domestic_AWB.EstimatedDeliveryDateTo = saveParameters.pEstimatedDeliveryDateTo;
            objCVarLM_Domestic_AWB.ActualDeliveryDate = saveParameters.pActualDeliveryDate;
            objCVarLM_Domestic_AWB.TrackingStageID = saveParameters.pTrackingStageID;
            
            objCVarLM_Domestic_AWB.RunnerFromShipperID = (saveParameters.pRunnerFromShipperID == null ? 0 : saveParameters.pRunnerFromShipperID);
            objCVarLM_Domestic_AWB.RunnerToConsigneeID = (saveParameters.pRunnerToConsigneeID == null ? 0 : saveParameters.pRunnerToConsigneeID);

            //pRateID pPackageTypeID pQuantity pSellingAmount pRateDetails
            objCVarLM_Domestic_AWB.RateID = saveParameters.pRateID;
            objCVarLM_Domestic_AWB.PackageTypeID = saveParameters.pPackageTypeID;
            objCVarLM_Domestic_AWB.Quantity = saveParameters.pQuantity;
            objCVarLM_Domestic_AWB.SellingAmount = saveParameters.pSellingAmount;
            objCVarLM_Domestic_AWB.CommodityFees = saveParameters.pCommodityFees;
            
            objCVarLM_Domestic_AWB.RateDetails = (saveParameters.pRateDetails == null ? "0" : saveParameters.pRateDetails);

            objCVarLM_Domestic_AWB.CreatorUserID = WebSecurity.CurrentUserId;
            objCVarLM_Domestic_AWB.creationDate = DateTime.Now;
            
            CLM_Domestic_AWB objLM_Domestic_AWB = new CLM_Domestic_AWB();
            objLM_Domestic_AWB.lstCVarLM_Domestic_AWB.Add(objCVarLM_Domestic_AWB);
            Exception checkException = objLM_Domestic_AWB.SaveMethod(objLM_Domestic_AWB.lstCVarLM_Domestic_AWB);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
                _result = true;
            return _result;
        }

        
        [HttpGet, HttpPost]
        public Object[] InsertCustomer([FromBody] CustomerParameters saveParameters)
        {
            var _result = true;

            #region Customer
            CCustomers objCCustomers = new CCustomers();
            CCustomers GetCustomer = new CCustomers();
            CVarCustomers objCVarCustomers = new CVarCustomers();
            objCVarCustomers.ID = 0;
            objCVarCustomers.Name = saveParameters.pName;
            objCVarCustomers.LocalName = saveParameters.pName;
            objCVarCustomers.CompanyName = saveParameters.pCompanyName;
            objCVarCustomers.SenderName = saveParameters.pSenderName;
            objCVarCustomers.CreationDate = DateTime.Now;
            objCVarCustomers.ModificationDate = DateTime.Now;
            objCVarCustomers.TimeLocked = DateTime.Parse("01/01/1900");

            objCVarCustomers.IsConsignee = false;
            objCVarCustomers.IsShipper = true;
            objCVarCustomers.IsInternalCustomer = false;
            objCVarCustomers.IsInactive = false;
            objCVarCustomers.IsConsolidatedInvoice = false;
            objCVarCustomers.ManagerRoleID = 5;
            objCVarCustomers.AdministratorRoleID = 1;
            objCVarCustomers.IsDeleted = false;
            objCVarCustomers.OriginalCMRbyPost = false;
            objCVarCustomers.IsExternal = false;
            
            objCVarCustomers.Website = "0";
            objCVarCustomers.Email = "0";
            objCVarCustomers.Notes = "0";
            objCVarCustomers.VATNumber = "0";
            objCVarCustomers.BankName = "0";
            objCVarCustomers.BankAddress = "0";
            objCVarCustomers.Swift = "0";
            objCVarCustomers.BankAccountNumber = "0";
            objCVarCustomers.IBANNumber = "0";
            objCVarCustomers.Address = "0";
            objCVarCustomers.PhonesAndFaxes = "0";
            objCVarCustomers.BillingDetails = "0";
            objCVarCustomers.ShippingDetails = "0";
            objCVarCustomers.AccountNo = saveParameters.pAccountNo;


            objCCustomers.lstCVarCustomers.Add(objCVarCustomers);
            Exception checkException = objCCustomers.SaveMethod(objCCustomers.lstCVarCustomers);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            #endregion

            #region Address
            if(objCCustomers.lstCVarCustomers.Count > 0)
            {
                CAddresses Addresses = new CAddresses();
                CVarAddresses CVarobjAddresses = new CVarAddresses();
                //pCity pAddress pCityID  pRegionID 
                CVarobjAddresses.ID = 0;
                CVarobjAddresses.PartnerTypeID = 1;
                CVarobjAddresses.PartnerID = objCCustomers.lstCVarCustomers[0].ID;
                CVarobjAddresses.CityID = saveParameters.pCityID;
                CVarobjAddresses.PortID = saveParameters.pRegionID;
                CVarobjAddresses.StreetLine1 = saveParameters.pCity + ""+ saveParameters.pAddress;
                
                CVarobjAddresses.StreetLine2 = "0";
                CVarobjAddresses.IsDeleted = false;
                CVarobjAddresses.CreationDate = DateTime.Now;
                CVarobjAddresses.ModificatorUserID = 0;
                CVarobjAddresses.ModificationDate = DateTime.Now;
                CVarobjAddresses.LockingUserID = 0;
                CVarobjAddresses.TimeLocked = DateTime.Parse("01/01/1900");
                CVarobjAddresses.BuildingNo = "0";
                CVarobjAddresses.FloorNo = "0";
                CVarobjAddresses.ApartmentNo = "0";
                CVarobjAddresses.Landmarks = "0";
                CVarobjAddresses.ZipCode = "0";
                CVarobjAddresses.PrintedAs = "0";

                Addresses.lstCVarAddresses.Add(CVarobjAddresses);
                checkException = Addresses.SaveMethod(Addresses.lstCVarAddresses);
            }
            #endregion

            #region Contact
            CContacts Contacts = new CContacts();
            CVarContacts CVarobjContacts = new CVarContacts();
            CVarobjContacts.ID = 0;
            CVarobjContacts.Name = saveParameters.pName;
            CVarobjContacts.LocalName = saveParameters.pName;
            CVarobjContacts.PartnerTypeID = 1;
            CVarobjContacts.PartnerID = objCCustomers.lstCVarCustomers[0].ID;
            CVarobjContacts.Phone1 = saveParameters.pPhone1;
            CVarobjContacts.Phone2 = saveParameters.pPhone2;
            CVarobjContacts.CreationDate = DateTime.Now;
            CVarobjContacts.ModificatorUserID = 0;
            CVarobjContacts.ModificationDate = DateTime.Now;
            CVarobjContacts.LockingUserID = 0;
            CVarobjContacts.TimeLocked = DateTime.Parse("01/01/1900");
            CVarobjContacts.IsDeleted = false;

            CVarobjContacts.Email = "0";
            CVarobjContacts.Mobile1 = "0";
            CVarobjContacts.Mobile2 = "0";
            CVarobjContacts.Fax = "0";
            CVarobjContacts.Notes = "0";

            Contacts.lstCVarContacts.Add(CVarobjContacts);
            checkException = Contacts.SaveMethod(Contacts.lstCVarContacts);
            #endregion

            CvwLM_Customers objCvwLM_Customers = new CvwLM_Customers();
            objCvwLM_Customers.GetList(" Where ID = "+ objCVarCustomers.ID);

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] { serializer.Serialize(objCvwLM_Customers.lstCVarvwLM_Customers), objCVarCustomers.ID };
        }
        
        [HttpGet, HttpPost]
        public Object[] Runner_Save([FromBody] Runner_Parameters saveParameters)
        {
            CvwLM_Domestic_AWB objvwLM_Domestic_AWB = new CvwLM_Domestic_AWB();
            var _result = true;
            CLM_Domestic_AWB ExistsCLM_Domestic_AWB = new CLM_Domestic_AWB();
            CVarLM_Domestic_AWB objCVarLM_Domestic_AWB = new CVarLM_Domestic_AWB();
            if (saveParameters.pID > 0)
            {
                ExistsCLM_Domestic_AWB.GetList(" Where ID = " + saveParameters.pID);
                if (ExistsCLM_Domestic_AWB.lstCVarLM_Domestic_AWB.Count > 0)
                    objCVarLM_Domestic_AWB = ExistsCLM_Domestic_AWB.lstCVarLM_Domestic_AWB[0];
            }
            objCVarLM_Domestic_AWB.RunnerFromShipperID = saveParameters.RunnerFromShipper;  
            objCVarLM_Domestic_AWB.RunnerToConsigneeID = saveParameters.RunnerToConsignee;  
            objCVarLM_Domestic_AWB.StoreID = saveParameters.pStoreID;  
            objCVarLM_Domestic_AWB.TrackingStageID = saveParameters.pTrackingStageID;  
            objCVarLM_Domestic_AWB.PickupAddress = saveParameters.pPickupAddress;  
            objCVarLM_Domestic_AWB.EstimatedReceivedDate_Custody = saveParameters.pEstimatedReceivedDate_Custody;  
            objCVarLM_Domestic_AWB.ActualReceivedDate_Custody = saveParameters.pActualReceivedDate_Custody;  
            objCVarLM_Domestic_AWB.EstimatedArrivalDateToStore = saveParameters.pEstimatedArrivalDateToStore;  
            objCVarLM_Domestic_AWB.ActualArrivalDateToStore = saveParameters.pActualArrivalDateToStore;  
            objCVarLM_Domestic_AWB.EstimatedDeliveryDateFrom = saveParameters.pEstimatedDeliveryDateFrom;  
            objCVarLM_Domestic_AWB.EstimatedDeliveryDateTo = saveParameters.pEstimatedDeliveryDateTo;  
            objCVarLM_Domestic_AWB.ActualDeliveryDate = saveParameters.pActualDeliveryDate;

            CLM_Domestic_AWB objLM_Domestic_AWB = new CLM_Domestic_AWB();
            objLM_Domestic_AWB.lstCVarLM_Domestic_AWB.Add(objCVarLM_Domestic_AWB);
            Exception checkException = objLM_Domestic_AWB.SaveMethod(objLM_Domestic_AWB.lstCVarLM_Domestic_AWB);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
            {
                _result = true;
                objvwLM_Domestic_AWB.GetList(" Where ID = "+ objCVarLM_Domestic_AWB.ID);
            }
               

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] { serializer.Serialize(objvwLM_Domestic_AWB.lstCVarvwLM_Domestic_AWB), _result };
        }

        [HttpGet, HttpPost]
        public Object[] RunnerWhenReceiveFromShipper_Save([FromBody] Runner_Parameters saveParameters)
        {
            CvwLM_Domestic_AWB objvwLM_Domestic_AWB = new CvwLM_Domestic_AWB();
            var _result = true;
            CLM_Domestic_AWB ExistsCLM_Domestic_AWB = new CLM_Domestic_AWB();
            CVarLM_Domestic_AWB objCVarLM_Domestic_AWB = new CVarLM_Domestic_AWB();
            if (saveParameters.pID > 0)
            {
                ExistsCLM_Domestic_AWB.GetList(" Where ID = " + saveParameters.pID);
                if (ExistsCLM_Domestic_AWB.lstCVarLM_Domestic_AWB.Count > 0)
                    objCVarLM_Domestic_AWB = ExistsCLM_Domestic_AWB.lstCVarLM_Domestic_AWB[0];
            }
            objCVarLM_Domestic_AWB.ActualReceivedDate_Custody = saveParameters.pActualReceivedDate_Custody;
            if (saveParameters.pRemarks != "0" && saveParameters.pRemarks != null)
                objCVarLM_Domestic_AWB.Remarks = ExistsCLM_Domestic_AWB.lstCVarLM_Domestic_AWB[0].Remarks + System.Environment.NewLine + ""+ WebSecurity.CurrentUserName + " : " + saveParameters.pRemarks;
            //objCVarLM_Domestic_AWB.Remarks =   WebSecurity.CurrentUserName + " : " + saveParameters.pRemarks;
            if(saveParameters.pActualReceivedDate_Custody > DateTime.Parse("01/01/2010"))
            {
                CTrackingStage TrackingStage = new CTrackingStage();
                TrackingStage.GetList(" Where ViewOrder = (select (ViewOrder+1) from TrackingStage where ID = " + objCVarLM_Domestic_AWB.TrackingStageID +" ) "); 
                if(TrackingStage.lstCVarTrackingStage.Count > 0)
                    objCVarLM_Domestic_AWB.TrackingStageID = TrackingStage.lstCVarTrackingStage[0].ID; 
            }
            CLM_Domestic_AWB objLM_Domestic_AWB = new CLM_Domestic_AWB();
            objLM_Domestic_AWB.lstCVarLM_Domestic_AWB.Add(objCVarLM_Domestic_AWB);
            Exception checkException = objLM_Domestic_AWB.SaveMethod(objLM_Domestic_AWB.lstCVarLM_Domestic_AWB);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
            {
                _result = true;
                objvwLM_Domestic_AWB.GetList(" Where ID = " + objCVarLM_Domestic_AWB.ID);
            }


            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] { serializer.Serialize(objvwLM_Domestic_AWB.lstCVarvwLM_Domestic_AWB), _result };
        }
        
        [HttpGet, HttpPost]
        public Object[] RunnerActualArrivalDateToStore_Save([FromBody] Runner_Parameters saveParameters)
        {
            CvwLM_Domestic_AWB objvwLM_Domestic_AWB = new CvwLM_Domestic_AWB();
            var _result = true;
            CLM_Domestic_AWB ExistsCLM_Domestic_AWB = new CLM_Domestic_AWB();
            CVarLM_Domestic_AWB objCVarLM_Domestic_AWB = new CVarLM_Domestic_AWB();
            if (saveParameters.pID > 0)
            {
                ExistsCLM_Domestic_AWB.GetList(" Where ID = " + saveParameters.pID);
                if (ExistsCLM_Domestic_AWB.lstCVarLM_Domestic_AWB.Count > 0)
                    objCVarLM_Domestic_AWB = ExistsCLM_Domestic_AWB.lstCVarLM_Domestic_AWB[0];
            }
            objCVarLM_Domestic_AWB.ActualArrivalDateToStore = saveParameters.pActualArrivalDateToStore;
            if (saveParameters.pRemarks != "0" && saveParameters.pRemarks != null)
                objCVarLM_Domestic_AWB.Remarks = ExistsCLM_Domestic_AWB.lstCVarLM_Domestic_AWB[0].Remarks + System.Environment.NewLine + "" + WebSecurity.CurrentUserName + " : " + saveParameters.pRemarks;
            //objCVarLM_Domestic_AWB.Remarks =   WebSecurity.CurrentUserName + " : " + saveParameters.pRemarks;
            if (saveParameters.pActualArrivalDateToStore > DateTime.Parse("01/01/2010"))
            {
                CTrackingStage TrackingStage = new CTrackingStage();
                TrackingStage.GetList(" Where ViewOrder = (select (ViewOrder+1) from TrackingStage where ID = " + objCVarLM_Domestic_AWB.TrackingStageID + " ) ");
                if (TrackingStage.lstCVarTrackingStage.Count > 0)
                    objCVarLM_Domestic_AWB.TrackingStageID = TrackingStage.lstCVarTrackingStage[0].ID;
            }
            CLM_Domestic_AWB objLM_Domestic_AWB = new CLM_Domestic_AWB();
            objLM_Domestic_AWB.lstCVarLM_Domestic_AWB.Add(objCVarLM_Domestic_AWB);
            Exception checkException = objLM_Domestic_AWB.SaveMethod(objLM_Domestic_AWB.lstCVarLM_Domestic_AWB);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
            {
                _result = true;
                objvwLM_Domestic_AWB.GetList(" Where ID = " + objCVarLM_Domestic_AWB.ID);
            }

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] { serializer.Serialize(objvwLM_Domestic_AWB.lstCVarvwLM_Domestic_AWB), _result };
        }

        [HttpGet, HttpPost]
        public Object[] RunnerActualDeliveryDate_Save([FromBody] Runner_Parameters saveParameters)
        {
            CvwLM_Domestic_AWB objvwLM_Domestic_AWB = new CvwLM_Domestic_AWB();
            var _result = true;
            CLM_Domestic_AWB ExistsCLM_Domestic_AWB = new CLM_Domestic_AWB();
            CVarLM_Domestic_AWB objCVarLM_Domestic_AWB = new CVarLM_Domestic_AWB();
            if (saveParameters.pID > 0)
            {
                ExistsCLM_Domestic_AWB.GetList(" Where ID = " + saveParameters.pID);
                if (ExistsCLM_Domestic_AWB.lstCVarLM_Domestic_AWB.Count > 0)
                    objCVarLM_Domestic_AWB = ExistsCLM_Domestic_AWB.lstCVarLM_Domestic_AWB[0];
            }
            objCVarLM_Domestic_AWB.ActualDeliveryDate = saveParameters.pActualDeliveryDate;
            if (saveParameters.pRemarks != "0" && saveParameters.pRemarks != null)
                objCVarLM_Domestic_AWB.Remarks = ExistsCLM_Domestic_AWB.lstCVarLM_Domestic_AWB[0].Remarks + System.Environment.NewLine + "" + WebSecurity.CurrentUserName + " : " + saveParameters.pRemarks;
            //objCVarLM_Domestic_AWB.Remarks =   WebSecurity.CurrentUserName + " : " + saveParameters.pRemarks;
            if (saveParameters.pActualDeliveryDate > DateTime.Parse("01/01/2010"))
            {
                CTrackingStage TrackingStage = new CTrackingStage();
                TrackingStage.GetList(" Where ViewOrder = (select (ViewOrder+1) from TrackingStage where ID = " + objCVarLM_Domestic_AWB.TrackingStageID + " ) ");
                if (TrackingStage.lstCVarTrackingStage.Count > 0)
                    objCVarLM_Domestic_AWB.TrackingStageID = TrackingStage.lstCVarTrackingStage[0].ID;
            }
            CLM_Domestic_AWB objLM_Domestic_AWB = new CLM_Domestic_AWB();
            objLM_Domestic_AWB.lstCVarLM_Domestic_AWB.Add(objCVarLM_Domestic_AWB);
            Exception checkException = objLM_Domestic_AWB.SaveMethod(objLM_Domestic_AWB.lstCVarLM_Domestic_AWB);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
            {
                _result = true;
                objvwLM_Domestic_AWB.GetList(" Where ID = " + objCVarLM_Domestic_AWB.ID);
            }

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] { serializer.Serialize(objvwLM_Domestic_AWB.lstCVarvwLM_Domestic_AWB), _result };
        }

        [HttpGet, HttpPost]
        public Object[] Runner_AWBTrackingStage_Save([FromBody] Runner_Parameters saveParameters)
        {
            CvwLM_Domestic_AWB objvwLM_Domestic_AWB = new CvwLM_Domestic_AWB();
            var _result = true;
            CLM_Domestic_AWB ExistsCLM_Domestic_AWB = new CLM_Domestic_AWB();
            CVarLM_Domestic_AWB objCVarLM_Domestic_AWB = new CVarLM_Domestic_AWB();
            if (saveParameters.pID > 0)
            {
                ExistsCLM_Domestic_AWB.GetList(" Where ID = " + saveParameters.pID);
                if (ExistsCLM_Domestic_AWB.lstCVarLM_Domestic_AWB.Count > 0)
                    objCVarLM_Domestic_AWB = ExistsCLM_Domestic_AWB.lstCVarLM_Domestic_AWB[0];
            }
            if(saveParameters.pRemarks != "0" && saveParameters.pRemarks != null)
                objCVarLM_Domestic_AWB.Remarks = ExistsCLM_Domestic_AWB.lstCVarLM_Domestic_AWB[0].Remarks + System.Environment.NewLine + "" + WebSecurity.CurrentUserName + " : " + saveParameters.pRemarks;

            if (saveParameters.pUpdateTrackingStageIdOnly == 1)
            {
                objCVarLM_Domestic_AWB.TrackingStageID = saveParameters.pTrackingStageID;
            }
            CLM_Domestic_AWB objLM_Domestic_AWB = new CLM_Domestic_AWB();
            objLM_Domestic_AWB.lstCVarLM_Domestic_AWB.Add(objCVarLM_Domestic_AWB);
            Exception checkException = objLM_Domestic_AWB.SaveMethod(objLM_Domestic_AWB.lstCVarLM_Domestic_AWB);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
            {
                _result = true;
                objvwLM_Domestic_AWB.GetList(" Where ID = " + objCVarLM_Domestic_AWB.ID);
            }

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] { serializer.Serialize(objvwLM_Domestic_AWB.lstCVarvwLM_Domestic_AWB), _result };
        }

        [HttpGet, HttpPost]
        public Object[] LoadShipperData(int pCustomerID)
        {
            CvwLM_Customers objCvwLM_Customers = new CvwLM_Customers();
            objCvwLM_Customers.GetList(" Where ID = " + pCustomerID);

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] { serializer.Serialize(objCvwLM_Customers.lstCVarvwLM_Customers)};
        }

        [HttpGet, HttpPost]
        public bool Delete(String pDomestic_AWBIDs)
        {
            bool _result = false;
            CLM_Domestic_AWB objCLM_Domestic_AWB = new CLM_Domestic_AWB();
            foreach (var Domestic_AWB in pDomestic_AWBIDs.Split(','))
            {
                objCLM_Domestic_AWB.lstDeletedCPKLM_Domestic_AWB.Add(new CPKLM_Domestic_AWB() { ID = Int32.Parse(Domestic_AWB.Trim()) });
            }

            Exception checkException = objCLM_Domestic_AWB.DeleteItem(objCLM_Domestic_AWB.lstDeletedCPKLM_Domestic_AWB);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("DELETE")) // some or all of the rows were not deleted due to dependencies
                    _result = false;
            }
            else //deleted successfully
                _result = true;
            return _result;
        }
        
        [HttpGet, HttpPost]
        public Object[] LoadSellingAmount(int pCustomerShipperID,int  pRegionIDFromShipper, int pRegionIDToConsignee, int pRateID,int pPackageTypeID,decimal pQuantity)
        {
            Exception checkException;
            CLM_RateRegions objCLM_RateRegions = new CLM_RateRegions();
            string pWhereClause = " Where 1=1 and RateID = " + pRateID + " AND RegionIDFrom = " + pRegionIDFromShipper + "  AND RegionIDTo = " + pRegionIDToConsignee + " AND ISNULL(PackageTypeID,0) = " + pPackageTypeID + "";
            //pWhereClause += " AND ( Quantity = "+pQuantity+")";
            checkException = objCLM_RateRegions.GetList(pWhereClause);

            var objCLM_RateRegionsResult = objCLM_RateRegions.lstCVarLM_RateRegions.Where(x => x.Quantity == pQuantity).OrderByDescending(x => x.Quantity).ToList();
            if(objCLM_RateRegionsResult.Count == 0)
            {
                objCLM_RateRegionsResult = objCLM_RateRegions.lstCVarLM_RateRegions.Where(x => x.Quantity <= pQuantity).OrderByDescending(x => x.Quantity).ToList();
            }
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] {serializer.Serialize(objCLM_RateRegions.lstCVarLM_RateRegions),
                                 serializer.Serialize(objCLM_RateRegionsResult)
            };
        }

        
        [HttpGet, HttpPost]
        public Object[] GetRunner_From_To(int AWBID)
        {
            CLM_Domestic_AWB objCLM_Domestic_AWB = new CLM_Domestic_AWB();
            objCLM_Domestic_AWB.GetList(" Where ID = "+ AWBID);

            CCustody objCCustody_FromShipper = new CCustody();
            CCustodyRegions objCCustodyRegions_FromShipper = new CCustodyRegions();

            CCustody objCCustody_ToConsignee = new CCustody();
            CCustodyRegions objCCustodyRegions_ToConsignee = new CCustodyRegions();

            if (objCLM_Domestic_AWB.lstCVarLM_Domestic_AWB.Count > 0 )
            {
                objCCustodyRegions_FromShipper.GetList(" Where PortID = " + objCLM_Domestic_AWB.lstCVarLM_Domestic_AWB[0].ShipperRegionID);
                objCCustodyRegions_ToConsignee.GetList(" Where PortID = " + objCLM_Domestic_AWB.lstCVarLM_Domestic_AWB[0].ConsigneeRegionID);
            }
            if(objCCustodyRegions_FromShipper.lstCVarCustodyRegions.Count > 0)
            {
                string Runners_FromShipper = string.Join(",", objCCustodyRegions_FromShipper.lstCVarCustodyRegions.Select(x => x.CustodyID).ToList());
                objCCustody_FromShipper.GetList(" Where ID IN(" + Runners_FromShipper + ")");
            }
            if (objCCustodyRegions_ToConsignee.lstCVarCustodyRegions.Count > 0)
            {
                string Runners_ToShipper = string.Join(",", objCCustodyRegions_ToConsignee.lstCVarCustodyRegions.Select(x => x.CustodyID).ToList());
                objCCustody_ToConsignee.GetList(" Where ID IN("+ Runners_ToShipper + ")");
            }

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] {
                serializer.Serialize(objCCustody_FromShipper.lstCVarCustody),
                serializer.Serialize(objCCustody_ToConsignee.lstCVarCustody)
            };
        }
        
        [HttpGet, HttpPost]
        public Object[] UpdateRunnersToAWBs(string pAWBsIDs, int pRunnerID)
        {
            CLM_Domestic_AWB objCLM_Domestic_AWB = new CLM_Domestic_AWB();
            Exception Ex =  objCLM_Domestic_AWB.UpdateList(" RunnerID = "+pRunnerID +" Where ID IN("+ pAWBsIDs +")");

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] {
                serializer.Serialize(objCLM_Domestic_AWB.lstCVarLM_Domestic_AWB)
                ,Ex.Message
            };
        }


        
        [HttpGet, HttpPost]
        public Object[] SaveAllBaseOnRows_Dispatcher(string pst_AWBIDs, string pst_TrackingStageIDs, string pst_RunnerFromIDs, string pst_RunnerToIDs,string pEstimatedReceivedDate_Custody, string pEstimatedArrivalDateToStore, string pEstimatedDeliveryDateFrom, string pEstimatedDeliveryDateTo)
        {
            Exception checkException = null;

            List<string> Arrst_AWBIDs = pst_AWBIDs.Split(',').ToList<string>();
            List<string> Arrst_TrackingStageIDs = pst_TrackingStageIDs.Split(',').ToList<string>();
            List<string> Arrst_RunnerFromIDs = pst_RunnerFromIDs.Split(',').ToList<string>();
            List<string> Arrst_RunnerToIDs = pst_RunnerToIDs.Split(',').ToList<string>();
           
            if(pst_AWBIDs.Length > 0)
            {
                for(int i=0;i<Arrst_AWBIDs.Count; i++)
                {
                    CLM_Domestic_AWB objCLM_Domestic_AWB = new CLM_Domestic_AWB();
                    string datesQuery = "";
                    //string dd = GetDateTimeyyyyMMddTime(pEstimatedReceivedDate_Custody);
                    // CONVERT(date, IssueDate) =  ' " + Forwarding.MvcApp.Helpers.DateFunctionsController.GetYYYYMMDDFormat(ItemDateFromExcel, 2)+ "' 
                    if (pEstimatedReceivedDate_Custody != "01/01/1900")//, DateTime pEstimatedArrivalDateToStore, DateTime pEstimatedDeliveryDateFrom, DateTime pEstimatedDeliveryDateTo)
                    {
                        //datesQuery = ",EstimatedReceivedDate_Custody ='" + pApplytxtEstimatedReceivedDate_Custody + "'";
                        datesQuery += ", EstimatedReceivedDate_Custody =  '" + GetDateTimeyyyyMMddTime(pEstimatedReceivedDate_Custody) + "' ";
                    }
                    if (pEstimatedArrivalDateToStore.ToString() != "01/01/1900")
                    {
                        //datesQuery = ",EstimatedArrivalDateToStore='" + pEstimatedArrivalDateToStore + "'";
                        datesQuery += ", EstimatedArrivalDateToStore =  '" + GetDateTimeyyyyMMddTime(pEstimatedArrivalDateToStore) + "' ";
                    }
                    if (pEstimatedDeliveryDateFrom.ToString() != "01/01/1900")
                    {
                        //datesQuery = ",EstimatedDeliveryDateFrom='" + pEstimatedDeliveryDateFrom + "'";
                        datesQuery += ", EstimatedDeliveryDateFrom =  '" + GetDateTimeyyyyMMddTime(pEstimatedDeliveryDateFrom) + "' ";
                    }
                    if (pEstimatedDeliveryDateTo.ToString() != "01/01/1900")
                    {
                        //datesQuery = ",EstimatedDeliveryDateTo='" + pEstimatedDeliveryDateTo + "'";
                        datesQuery += ", EstimatedDeliveryDateTo =  '" + GetDateTimeyyyyMMddTime(pEstimatedDeliveryDateTo) + "' ";
                    }
                    checkException = objCLM_Domestic_AWB.UpdateList("TrackingStageID=" + ( Arrst_TrackingStageIDs[i]=="0"?"NULL": Arrst_TrackingStageIDs[i]) + ",RunnerFromShipperID=" + (Arrst_RunnerFromIDs[i] == "0" ? "NULL" : Arrst_RunnerFromIDs[i]) + ",RunnerToConsigneeID=" + (Arrst_RunnerToIDs[i] == "0" ? "NULL" : Arrst_RunnerToIDs[i]) + " " + datesQuery + " WHERE ID=" + (Arrst_AWBIDs[i] == "0" ? "NULL" : Arrst_AWBIDs[i]) );
                }
            }
            
            return new Object[] {
              (checkException == null?"":checkException.Message)
            };
        }

        public string GetDateTimeyyyyMMddTime(string DT)
        {
            string StrDateTimeWithSpace = DT;
            string[] DtsWithSpace = StrDateTimeWithSpace.Split(' ');

            string StrDateTime = DtsWithSpace[0];
            string[] Dts = StrDateTime.Split('/');
            string returnDate = Dts[2] + "-" + Dts[0] + "-" + Dts[1];

            return returnDate + " "+DtsWithSpace[1];
        }


        //public Object[] SaveAllBaseOnRows_Runner(string pst_AWBIDs, string pst_TrackingStageIDs, string pst_ActualArrivalDateToStore, string pst_ActualReceivedDate_Custody, string pst_ActualDeliveryDate, string pst_Remarks)
        [HttpGet, HttpPost]
        public Object[] SaveAllBaseOnRows_Runner(string pst_AWBIDs, string pst_TrackingStageIDs, string pActualReceivedDate_Custody, string pActualArrivalDateToStore, string pActualDeliveryDate, string pst_Remarks)
        {
            Exception checkException = null;

            List<string> Arrst_AWBIDs = pst_AWBIDs.Split(',').ToList<string>();
            List<string> Arrst_TrackingStageIDs = pst_TrackingStageIDs.Split(',').ToList<string>();
            //List<string> Arrst_ActualArrivalDateToStore = pst_ActualArrivalDateToStore.Split(',').ToList<string>();

            string datesQuery = "";
            if (pActualReceivedDate_Custody != "01/01/1900")
            {
                datesQuery += " ActualReceivedDate_Custody =  '" + GetDateTimeyyyyMMddTime(pActualReceivedDate_Custody) + "' ,";
            }
            if (pActualArrivalDateToStore.ToString() != "01/01/1900")
            {
                datesQuery += " ActualArrivalDateToStore =  '" + GetDateTimeyyyyMMddTime(pActualArrivalDateToStore) + "' ,";
            }
            if (pActualDeliveryDate.ToString() != "01/01/1900")
            {
                datesQuery += "ActualDeliveryDate =  '" + GetDateTimeyyyyMMddTime(pActualDeliveryDate) + "' ,";
            }

            // List<string> Arrst_ActualReceivedDate_Custody = pst_ActualReceivedDate_Custody.Split(',').ToList<string>();
            //List<string> Arrst_ActualDeliveryDate = pst_ActualDeliveryDate.Split(',').ToList<string>();
            List<string> Arrst_Remarks = pst_Remarks.Split('+').ToList<string>();

            if (pst_AWBIDs.Length > 0)
            {
                for (int i = 0; i < Arrst_AWBIDs.Count; i++)
                {
                    CLM_Domestic_AWB objCLM_Domestic_AWB = new CLM_Domestic_AWB();
                    string UpdateQuery = "";


                    UpdateQuery += ""+(Arrst_TrackingStageIDs[i] == "0" ? "" : ("TrackingStageID=" + Arrst_TrackingStageIDs[i] + ",") ) + "" + (Arrst_Remarks[i] == "0" ? "" : ("Remarks='" + Arrst_Remarks[i] + "',") )+"" + datesQuery + "";
                    if (UpdateQuery.EndsWith(","))
                        UpdateQuery = UpdateQuery.Remove(UpdateQuery.Length - 1);
                    checkException = objCLM_Domestic_AWB.UpdateList(UpdateQuery + " WHERE ID=" + (Arrst_AWBIDs[i] == "0" ? "NULL" : Arrst_AWBIDs[i]));
                }
            }

            return new Object[] {
              (checkException == null?"":checkException.Message)
            };
        }
    }
    public class SaveParameters
    {
        public int pID { get; set; }
        //pAWBNumber pCustomerShipperID pCustomerConsigneeID pActWgt pDimWgt pChgWgt pPcs pCODAmount pDescription pRemarks
        public string pAWBNumber { get; set; }
        public int pCustomerShipperID { get; set; }
        public int pCustomerConsigneeID { get; set; }
        public decimal pActWgt { get; set; }
        public decimal pDimWgt { get; set; }
        public decimal pChgWgt { get; set; }
        public decimal pPcs { get; set; }
        public decimal pCODAmount { get; set; }
        public string pDescription { get; set; }
        public string pRemarks { get; set; }
        public string pConsigneeName { get; set; }
        public int pConsigneeCityID { get; set; }
        public int pConsigneeRegionID { get; set; }
        public string pConsigneePhone1 { get; set; }
        public string pConsigneePhone2 { get; set; }
        public string pConsigneeCompanyName { get; set; }
        public string pConsigneeSenderName { get; set; }
        public string pConsigneeAccountNo { get; set; }
        public string pConsigneeCity { get; set; }
        public string pConsigneeAddress { get; set; }

        public string pShipperName { get; set; }
        public int pShipperCityID { get; set; }
        public int pShipperRegionID { get; set; }
        public string pShipperPhone1 { get; set; }
        public string pShipperPhone2 { get; set; }
        public string pShipperCompanyName { get; set; }
        public string pShipperSenderName { get; set; }
        public string pShipperAccountNo { get; set; }
        public string pShipperCity { get; set; }
        public string pShipperAddress { get; set; }

        public int pPaymentTypeID { get; set; }
        public int pStoreID { get; set; }
        public String pPickupAddress { get; set; }
        public DateTime pEstimatedReceivedDate_Custody { get; set; }
        public DateTime pActualReceivedDate_Custody { get; set; }
        public DateTime pEstimatedArrivalDateToStore { get; set; }
        public DateTime pActualArrivalDateToStore { get; set; }
        public DateTime pEstimatedDeliveryDateFrom { get; set; }
        public DateTime pEstimatedDeliveryDateTo { get; set; }
        public DateTime pActualDeliveryDate { get; set; }
        public int pTrackingStageID { get; set; }
        public int pRunnerFromShipperID { get; set; }
        public int pRunnerToConsigneeID { get; set; }


        public int pRateID { get; set; }
        public int pPackageTypeID { get; set; }
        public decimal pQuantity { get; set; }
        public decimal pSellingAmount { get; set; }
        public string pRateDetails { get; set; }
        public decimal pCommodityFees { get; set; }
        
    }

    public class CustomerParameters
    {
        public string pName { get; set; }
        public int pCityID { get; set; }
        public int pRegionID { get; set; }
        public string pPhone1 { get; set; }
        public string pPhone2 { get; set; }
        public string pCompanyName { get; set; }
        public string pSenderName { get; set; }
        public string pAccountNo { get; set; }
        public string pCity { get; set; }
        public string pAddress { get; set; }
        
    }

    public class Runner_Parameters
    {
        public int pID { get; set; }
        public int RunnerFromShipper { get; set; }
        public int RunnerToConsignee { get; set; }
        public int pStoreID { get; set; }
        public int pTrackingStageID { get; set; }
        public string pPickupAddress { get; set; }
        public DateTime pEstimatedReceivedDate_Custody { get; set; }
        public DateTime pActualReceivedDate_Custody { get; set; }
        public DateTime pEstimatedArrivalDateToStore { get; set; }
        public DateTime pActualArrivalDateToStore { get; set; }
        public DateTime pEstimatedDeliveryDateFrom { get; set; }
        public DateTime pEstimatedDeliveryDateTo { get; set; }
        public DateTime pActualDeliveryDate { get; set; }
        public string pRemarks { get; set; }
        public int pUpdateTrackingStageIdOnly { get; set; }
    }

}
