using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;
using Forwarding.MvcApp.Models.NoAccess.Generated;
using Forwarding.MvcApp.Models.Warehousing.MasterData.Generated;
using Forwarding.MvcApp.Models.Warehousing.Transactions.Generated;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebMatrix.WebData;

namespace Forwarding.MvcApp.Controllers.Warehousing.API_Reports
{
    public class ContractController : ApiController
    {
        #region Contract

        [HttpGet, HttpPost]
        public object[] LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned(bool pIsLoadArrayOfObjects, string pLanguage, Int32 pPageNumber, Int32 pPageSize, string pWhereClause, string pOrderBy)
        {
            int _RowCount = 0;
            int constAreaUnitTypeIDForM2 = 90;
            int constVolumeUnitTypeIDForM3 = 100;
            Exception checkException = null;
            CvwNoAccessUnit objCvwNoAccessUnit = new CvwNoAccessUnit();
            CNoAccessContractDetailsType objCNoAccessContractDetailsType = new CNoAccessContractDetailsType();
            CNoAccessContractDetailsQuantityUnit objCNoAccessContractDetailsQuantityUnit = new CNoAccessContractDetailsQuantityUnit();
            CWH_Warehouse objCWH_Warehouse = new CWH_Warehouse();
            CvwWH_Contract objCvwWH_Contract = new CvwWH_Contract();
            CChargeTypes objCChargeTypes = new CChargeTypes();
            if (pIsLoadArrayOfObjects)
            {
                checkException = objCWH_Warehouse.GetList("ORDER BY Name");
                checkException = objCNoAccessContractDetailsType.GetList("ORDER BY ID");
                checkException = objCNoAccessContractDetailsQuantityUnit.GetList("ORDER BY ID");
                checkException = objCvwNoAccessUnit.GetList("WHERE ID IN (" + constAreaUnitTypeIDForM2 + "," + constVolumeUnitTypeIDForM3 + ")");
                checkException = objCChargeTypes.GetListPaging(99999, 1, "WHERE 1=1", "Name", out _RowCount);
            }
            checkException = objCvwWH_Contract.GetListPaging(pPageSize, pPageNumber, pWhereClause, pOrderBy, out _RowCount);
            
            return new object[] {
                new JavaScriptSerializer().Serialize(objCvwWH_Contract.lstCVarvwWH_Contract)
                , _RowCount
                , new JavaScriptSerializer().Serialize(objCvwNoAccessUnit.lstCVarvwNoAccessUnit) //pStorageUnit=pData[2]
                , new JavaScriptSerializer().Serialize(objCWH_Warehouse.lstCVarWH_Warehouse) //pWarehouse=pData[3]
                , new JavaScriptSerializer().Serialize(objCChargeTypes.lstCVarChargeTypes) //pChargeType=pData[4]
                , new JavaScriptSerializer().Serialize(objCNoAccessContractDetailsQuantityUnit.lstCVarNoAccessContractDetailsQuantityUnit) //pContractDetailsQuantityUnit=pData[5]
                , new JavaScriptSerializer().Serialize(objCNoAccessContractDetailsType.lstCVarNoAccessContractDetailsType) //pContractDetailsType=pData[6]
            };
        }

        [HttpGet, HttpPost]
        public object[] LoadHeaderWithDetails(Int32 pHeaderID)
        {
            CvwWH_Contract objCvwWH_Contract = new CvwWH_Contract();
            CvwWH_ContractDetails objCvwWH_ContractDetails = new CvwWH_ContractDetails();
            Exception checkException = null;
            int _RowCount = 0;
            checkException = objCvwWH_Contract.GetListPaging(99999, 1, "WHERE ID=" + pHeaderID.ToString(), "ID", out _RowCount);
            checkException = objCvwWH_ContractDetails.GetListPaging(99999, 1, "WHERE ContractID=" + pHeaderID.ToString(), "ID", out _RowCount);
            return new object[] {
                new JavaScriptSerializer().Serialize(objCvwWH_Contract.lstCVarvwWH_Contract[0]) //pData[0]
                , new JavaScriptSerializer().Serialize(objCvwWH_ContractDetails.lstCVarvwWH_ContractDetails) //pData[1]
            };
        }

        [HttpGet, HttpPost]
        public object[] Save(Int32 pID, Int32 pWarehouseID, Int32 pCustomerID, string pFromDate
            , string pToDate, decimal pStorageLimit, Int32 pStorageUnitID, bool pIsByPallet, Int32 pNumberOfPallets
            , Int32 pCurrencyID, string pNotes, bool pIsFinalized)
        {
            string _ReturnedMessage = "";
            Exception checkException = null;
            CWH_Contract objCWH_Contract = new CWH_Contract();
            CvwWH_Contract objCvwWH_Contract = new CvwWH_Contract();
            CWH_ContractDetails objCWH_ContractDetails = new CWH_ContractDetails();
            CvwWH_ContractDetails objCvwWH_ContractDetails = new CvwWH_ContractDetails();
            CVarWH_Contract objCVarWH_Contract = new CVarWH_Contract();
            string _FromDate = (DateTime.ParseExact(pFromDate + " 00.00.00.000", "d/M/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd 11:11:11.000");
            string _ToDate = (DateTime.ParseExact(pFromDate + " 00.00.00.000", "d/M/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd 11:11:11.000");
            int _RowCount = 0;
            string _UpdateClause = "";
            string _WhereClause = "";
            #region check period is not overlapped
            _WhereClause = "WHERE ID<>" + pID + " AND CustomerID=" + pCustomerID + " AND (('" + _FromDate + "' BETWEEN FromDate AND ToDate) OR ( '" + _ToDate + "' BETWEEN FromDate AND ToDate))";
            checkException = objCWH_Contract.GetList(_WhereClause);
            if (objCWH_Contract.lstCVarWH_Contract.Count > 0)
            {
                _ReturnedMessage = "The contract date overlaps with contract " + objCWH_Contract.lstCVarWH_Contract[0].Code;
            }
            #endregion check period is not overlapped
            #region Insert
            else if (pID == 0) //Insert
            {
                objCVarWH_Contract.Code = "0";
                objCVarWH_Contract.WarehouseID = pWarehouseID;
                objCVarWH_Contract.CustomerID = pCustomerID;
                objCVarWH_Contract.FromDate = DateTime.ParseExact(pFromDate + " 00.00.00.000", "d/M/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture);
                objCVarWH_Contract.ToDate = DateTime.ParseExact(pToDate + " 00.00.00.000", "d/M/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture).AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(998);
                objCVarWH_Contract.StorageLimit = pStorageLimit;
                objCVarWH_Contract.StorageUnitID = pStorageUnitID;
                objCVarWH_Contract.IsByPallet = pIsByPallet;
                objCVarWH_Contract.NumberOfPallets = pNumberOfPallets;
                objCVarWH_Contract.CurrencyID = pCurrencyID;
                objCVarWH_Contract.Notes = pNotes;
                objCVarWH_Contract.IsFinalized = pIsFinalized;
                
                objCVarWH_Contract.CreatorUserID = objCVarWH_Contract.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarWH_Contract.CreationDate = objCVarWH_Contract.ModificationDate = DateTime.Now;
                objCWH_Contract.lstCVarWH_Contract.Add(objCVarWH_Contract);
                checkException = objCWH_Contract.SaveMethod(objCWH_Contract.lstCVarWH_Contract);
                if (checkException == null)
                    pID = objCVarWH_Contract.ID;
                else
                    _ReturnedMessage = checkException.Message;
            }
            #endregion Insert
            #region Update
            else //update
            {
                _UpdateClause = pWarehouseID == 0 ? "WarehouseID=null" : ("WarehouseID=" + pWarehouseID) + " \n";
                _UpdateClause += pCustomerID == 0 ? ",CustomerID=null" : (",CustomerID=" + pCustomerID) + " \n";
                _UpdateClause += pFromDate == "0" ? " ,FromDate = NULL " : (" ,FromDate = '" + (DateTime.ParseExact(pFromDate + " 00.00.00.000", "d/M/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd 00:00:00.000") + "'");
                _UpdateClause += pToDate == "0" ? " ,ToDate = NULL " : (" ,ToDate = '" + (DateTime.ParseExact(pToDate + " 00.00.00.000", "d/M/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture)).ToString("yyyyMMdd 23:59:59.998") + "'");
                _UpdateClause += pStorageLimit == 0 ? ",StorageLimit=null" : (",StorageLimit=" + pStorageLimit) + " \n";
                _UpdateClause += pStorageUnitID == 0 ? ",StorageUnitID=null" : (",StorageUnitID=" + pStorageUnitID) + " \n";
                _UpdateClause += " , IsByPallet = " + (pIsByPallet ? "1" : "0") + " \n";
                _UpdateClause += pNumberOfPallets == 0 ? ",NumberOfPallets=null" : (",NumberOfPallets=" + pNumberOfPallets) + " \n";
                _UpdateClause += pCurrencyID == 0 ? ",CurrencyID=null" : (",CurrencyID=" + pCurrencyID) + " \n";
                _UpdateClause += pNotes == "0" ? ",Notes=null" : (",Notes=N'" + pNotes + "'") + " \n";
                _UpdateClause += " , IsFinalized = " + (pIsFinalized ? "1" : "0") + " \n";
                _UpdateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                _UpdateClause += " , ModificationDate = GETDATE() ";
                _UpdateClause += " WHERE ID = " + pID.ToString();

                checkException = objCWH_Contract.UpdateList(_UpdateClause);
                if (checkException != null)
                    _ReturnedMessage = checkException.Message;
            }
            #endregion Update
            if (_ReturnedMessage == "")
                objCvwWH_Contract.GetListPaging(1, 1, "WHERE ID=" + pID.ToString(), "ID", out _RowCount);
            return new object[] {
                _ReturnedMessage
                , pID //pData[1]
                , _ReturnedMessage != "" ? null: new JavaScriptSerializer().Serialize(objCvwWH_Contract.lstCVarvwWH_Contract[0]) //pData[2]
                , _ReturnedMessage != "" ? null : new JavaScriptSerializer().Serialize(objCvwWH_ContractDetails.lstCVarvwWH_ContractDetails) //pData[3]
            };
        }

        [HttpGet, HttpPost]
        public object[] Finalize(Int64 pFinalizedContractID)
        {
            string strMessage = "";
            Exception checkException = null;
            CWH_Contract objCWH_Contract = new CWH_Contract();
            checkException = objCWH_Contract.UpdateList("IsFinalized=1 WHERE ID=" + pFinalizedContractID);
            if (checkException != null)
                strMessage = checkException.Message;
            return new object[] {
                strMessage
            };
        }

        [HttpGet, HttpPost]
        public bool Delete(String pContractIDs)
        {
            bool _result = false;
            CWH_Contract objCWH_Contract = new CWH_Contract();
            foreach (var currentID in pContractIDs.Split(','))
            {
                objCWH_Contract.lstDeletedCPKWH_Contract.Add(new CPKWH_Contract() { ID = Int32.Parse(currentID.Trim()) });
            }

            Exception checkException = objCWH_Contract.DeleteItem(objCWH_Contract.lstDeletedCPKWH_Contract);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("DELETE")) // some or all of the Contracts were not deleted due to dependencies
                    _result = false;
            }
            else //deleted successfully
                _result = true;
            return _result;
        }

        #endregion Contract

        #region ContractDetails
        [HttpGet, HttpPost]
        public object[] ContractDetails_Save(Int64 pContractDetailsID, Int32 pContractID, Int32 pChargeTypeID
            , decimal pQuantity, int pQuantityUnitID, decimal pRate, decimal pMinimumCharge, decimal pAdditionalRate
            , Int32 pTypeID, decimal pCost)
        {
            Exception checkException = null;
            CVarWH_ContractDetails objCVarWH_ContractDetails = new CVarWH_ContractDetails();
            CWH_ContractDetails objCWH_ContractDetails = new CWH_ContractDetails();
            CvwWH_ContractDetails objCvwWH_ContractDetails = new CvwWH_ContractDetails();

            objCVarWH_ContractDetails.ID = pContractDetailsID;
            objCVarWH_ContractDetails.ContractID = pContractID;
            objCVarWH_ContractDetails.ChargeTypeID = pChargeTypeID;
            objCVarWH_ContractDetails.Quantity = pQuantity;
            objCVarWH_ContractDetails.QuantityUnitID = pQuantityUnitID;
            objCVarWH_ContractDetails.Rate = pRate;
            objCVarWH_ContractDetails.MinimumCharge = pMinimumCharge;
            objCVarWH_ContractDetails.AdditionalRate = pAdditionalRate;
            objCVarWH_ContractDetails.TypeID = pTypeID;
            objCVarWH_ContractDetails.Cost = pCost;
            
            if (pContractDetailsID != 0) //update so save original creator & creation date
            {
                CWH_ContractDetails objCGetCreationInformation = new CWH_ContractDetails();
                objCGetCreationInformation.GetItem(pContractDetailsID);
                objCVarWH_ContractDetails.CreatorUserID = objCGetCreationInformation.lstCVarWH_ContractDetails[0].CreatorUserID;
                objCVarWH_ContractDetails.CreationDate = objCGetCreationInformation.lstCVarWH_ContractDetails[0].CreationDate;
            }
            else
            {
                objCVarWH_ContractDetails.CreatorUserID = WebSecurity.CurrentUserId;
                objCVarWH_ContractDetails.CreationDate = DateTime.Now;
            }
            objCVarWH_ContractDetails.ModificatorUserID = WebSecurity.CurrentUserId;
            objCVarWH_ContractDetails.ModificationDate = DateTime.Now;

            objCWH_ContractDetails.lstCVarWH_ContractDetails.Add(objCVarWH_ContractDetails);
            checkException = objCWH_ContractDetails.SaveMethod(objCWH_ContractDetails.lstCVarWH_ContractDetails);

            checkException = objCvwWH_ContractDetails.GetList("WHERE ContractID=" + pContractID.ToString() + " ORDER BY ID");
            return new object[] {
                checkException == null ? true : false
                , new JavaScriptSerializer().Serialize(objCvwWH_ContractDetails.lstCVarvwWH_ContractDetails)
            };
        }

        [HttpGet, HttpPost]
        public object[] ContractDetails_Delete(string pContractDetailsIDsToDelete, Int64 pContractID)
        {
            Exception checkException = null;
            CWH_ContractDetails objCWH_ContractDetails = new CWH_ContractDetails();
            CvwWH_ContractDetails objCvwWH_ContractDetails = new CvwWH_ContractDetails();
            checkException = objCWH_ContractDetails.DeleteList("WHERE ID IN (" + pContractDetailsIDsToDelete + ")");
            objCvwWH_ContractDetails.GetList("WHERE ContractID=" + pContractID.ToString() + " ORDER By ID");
            return new object[] {
                checkException == null ? true : false
                , new JavaScriptSerializer().Serialize(objCvwWH_ContractDetails.lstCVarvwWH_ContractDetails)
            };
        }

        #endregion ContractDetails
    }
}
