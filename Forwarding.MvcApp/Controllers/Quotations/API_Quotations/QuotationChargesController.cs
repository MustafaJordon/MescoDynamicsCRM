using Forwarding.BLL.Utilities;
using Forwarding.MvcApp.Controllers.Operations.API_Operations;
using Forwarding.MvcApp.Entities.Operations;
using Forwarding.MvcApp.Entities.Quotations;
using Forwarding.MvcApp.Models.Administration.Security.Generated;
using Forwarding.MvcApp.Models.Administration.Settings.Generated;
using Forwarding.MvcApp.Models.LocalEmails.Generated;
using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;
using Forwarding.MvcApp.Models.MasterData.Others.Generated;
using Forwarding.MvcApp.Models.MasterData.Partners.Generated;
using Forwarding.MvcApp.Models.NoAccess.Generated;
using Forwarding.MvcApp.Models.Operations.Operations.Generated;
using Forwarding.MvcApp.Models.PricingModule.PricingTab;
using Forwarding.MvcApp.Models.Quotations.Quotations.Generated;
using System;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebMatrix.WebData;

namespace Forwarding.MvcApp.Controllers.Quotations.API_Quotations
{
    public class QuotationChargesController : ApiController
    {
        //[Route("/api/Quotations/LoadAll")]
        [HttpGet, HttpPost]
        public Object[] LoadAll(string pWhereClause)
        {
            Exception checkException = null;
            #region DepartmentCharge Case
            Int32 _RowCount = 0;
            CDefaults objCDefaults = new CDefaults();
            CUsers objCUsers = new CUsers();
            objCDefaults.GetListPaging(999999, 1, "WHERE 1=1", "ID", out _RowCount);
            objCUsers.GetListPaging(999999, 1, "WHERE ID=" + WebSecurity.CurrentUserId, "ID", out _RowCount);
            if (!objCUsers.lstCVarUsers[0].IsAccessAllCharges)
                pWhereClause += " AND ChargeTypeID IN (SELECT ChargeTypeID FROM DepartmentCharge WHERE DepartmentID=" + objCUsers.lstCVarUsers[0].DepartmentID + ") \n";
            #endregion DepartmentCharge Case
            CvwQuotationCharges objCvwQuotationCharges = new CvwQuotationCharges();
            checkException = objCvwQuotationCharges.GetListPaging(5000, 1, pWhereClause, "ID", out _RowCount);
            CNoAccessMeasurements objCNoAccessMeasurements = new CNoAccessMeasurements();
            checkException = objCNoAccessMeasurements.GetList("WHERE IsInactive=0"); // rest of conditions for is used in FCL,.....
            CContainerTypes objCContainerTypes = new CContainerTypes();
            checkException = objCContainerTypes.GetList("ORDER BY Code");
            CPackageTypes objCPackageTypes = new CPackageTypes();
            checkException = objCPackageTypes.GetList("ORDER BY Name");

            var a = checkException;
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] {
                serializer.Serialize(objCvwQuotationCharges.lstCVarvwQuotationCharges)
                , new JavaScriptSerializer().Serialize(objCNoAccessMeasurements.lstCVarNoAccessMeasurements)
                , new JavaScriptSerializer().Serialize(objCContainerTypes.lstCVarContainerTypes) //pData[2]
                , new JavaScriptSerializer().Serialize(objCPackageTypes.lstCVarPackageTypes) //pData[3]
            };
        }

        //// [Route("/api/QuotationCharges/LoadWithPaging/{pPageNumber}/{pPageSize}")]
        //[HttpGet, HttpPost]
        //public Object[] LoadWithPaging(Int32 pPageNumber, Int32 pPageSize, string pSearchKey)
        //{
        //    CvwQuotationCharges objCvwQuotationCharges = new CvwQuotationCharges();
        //    //objCvwQuotationCharges.GetList(string.Empty); //GetList() fn loads without paging
        //    Int32 _RowCount = objCvwQuotationCharges.lstCVarvwQuotationCharges.Count;

        //    pSearchKey = (pSearchKey == null ? "" : pSearchKey.Trim().ToUpper());
        //    string whereClause = " Where ChargeTypeCode LIKE '%" + pSearchKey + "%' ";

        //    objCvwQuotationCharges.GetListPaging(pPageSize, pPageNumber, whereClause, " ChargeTypeCode ", out _RowCount);
        //    return new Object[] { new JavaScriptSerializer().Serialize(objCvwQuotationCharges.lstCVarvwQuotationCharges), _RowCount };
        //}

        [HttpGet, HttpPost]
        public object[] LoadWithWhereClause(Int32 pPageNumber, Int32 pPageSize, string pWhereClause)
        {
            #region DepartmentCharge Case
            Int32 _RowCount = 0;
            CDefaults objCDefaults = new CDefaults();
            CUsers objCUsers = new CUsers();
            objCDefaults.GetListPaging(999999, 1, "WHERE 1=1", "ID", out _RowCount);
            objCUsers.GetListPaging(999999, 1, "WHERE ID=" + WebSecurity.CurrentUserId, "ID", out _RowCount);
            if (!objCUsers.lstCVarUsers[0].IsAccessAllCharges)
                pWhereClause += " AND ChargeTypeID IN (SELECT ChargeTypeID FROM DepartmentCharge WHERE DepartmentID=" + objCUsers.lstCVarUsers[0].DepartmentID + ") \n";
            #endregion DepartmentCharge Case

            Exception checkException = null;
            CvwQuotationCharges objCvwQuotationCharges = new CvwQuotationCharges();
            //pSearchKey here is the where clause
            checkException = objCvwQuotationCharges.GetListPaging(pPageSize, pPageNumber, pWhereClause, "ID", out _RowCount);
            var a = checkException;
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] { serializer.Serialize(objCvwQuotationCharges.lstCVarvwQuotationCharges), _RowCount };
        }

        [HttpGet, HttpPost]
        public bool InsertList(Int64 pQuotationRouteID, string pSelectedIDs)
        {
            int constLCLShipmentType = 2;
            int _RowCount = 0;
            bool _result = false;
            string pWhereClause = "";

            CvwQuotationRoute objCvwQuotationRoute = new CvwQuotationRoute();
            objCvwQuotationRoute.GetListPaging(999999, 1, "WHERE ID=" + pQuotationRouteID, "ID", out _RowCount);
            decimal _Quantity =
                (objCvwQuotationRoute.lstCVarvwQuotationRoute[0].TransportType == 2 || objCvwQuotationRoute.lstCVarvwQuotationRoute[0].ShipmentType == constLCLShipmentType) && objCvwQuotationRoute.lstCVarvwQuotationRoute[0].ChargeableWeight > 0
                ? objCvwQuotationRoute.lstCVarvwQuotationRoute[0].ChargeableWeight
                : 1;

            //building the where clause to select the rows from ChargeTypes
            foreach (var currentID in pSelectedIDs.Split(','))
            {
                //i am sure i ve at least 1 selectedID isa
                pWhereClause += (pWhereClause == "" ? " WHERE ID = " + currentID.ToString()
                    : " OR ID = " + currentID.ToString());
            }

            //those 2 lines are to get the Charge types from DB
            CDefaults objCDefaults = new CDefaults();
            objCDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa

            //those 2 lines are to get the Charge types from DB
            CvwChargeTypes objCvwChargeTypes = new CvwChargeTypes();
            //objCvwChargeTypes.GetList(pWhereClause);

            objCvwChargeTypes.GetListPaging(1500, 1, pWhereClause, " Name ", out _RowCount);

            CQuotationCharges objCQuotationCharges = new CQuotationCharges();

            foreach (var rowChargeType in objCvwChargeTypes.lstCVarvwChargeTypes)
            {
                CVarQuotationCharges objCVarQuotationCharges = new CVarQuotationCharges();

                objCVarQuotationCharges.QuotationRouteID = pQuotationRouteID;
                objCVarQuotationCharges.ChargeTypeID = rowChargeType.ID;
                objCVarQuotationCharges.MeasurementID = rowChargeType.MeasurementID;

                objCVarQuotationCharges.CostQuantity = _Quantity;
                objCVarQuotationCharges.CostCurrencyID = objCDefaults.lstCVarDefaults[0].CurrencyID;
                objCVarQuotationCharges.CostExchangeRate = 1;
                objCVarQuotationCharges.SaleQuantity = _Quantity;
                objCVarQuotationCharges.SaleCurrencyID = objCDefaults.lstCVarDefaults[0].CurrencyID;
                objCVarQuotationCharges.POrC = 0;
                objCVarQuotationCharges.SaleExchangeRate = 1;
                objCVarQuotationCharges.Notes = rowChargeType.Notes;

                objCVarQuotationCharges.CreatorUserID = objCVarQuotationCharges.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarQuotationCharges.CreationDate = objCVarQuotationCharges.ModificationDate = DateTime.Now;

                objCQuotationCharges.lstCVarQuotationCharges.Add(objCVarQuotationCharges);
            }
            var checkException = objCQuotationCharges.SaveMethod(objCQuotationCharges.lstCVarQuotationCharges);
            if (checkException == null)
                _result = true;
            return _result;
        }

        // [Route("/api/QuotationCharges/Update/{pID}/{pCode}/{pName}/{pLocalName}")]
        [HttpGet, HttpPost]
        public bool Update(Int64 pID, Int64 pQuotationRouteID, Int32 pChargeTypeID, Int32 pMeasurementID, Int32 pContainerTypeID, Int32 pDemurrageDays
        , Int32 pPackageTypeID, decimal pCostQuantity, Decimal pCostPrice, Decimal pCostAmount
        , Int32 pCostCurrencyID, Decimal pCostExchangeRate, decimal pSaleQuantity, Decimal pSalePrice, Decimal pSaleAmount
        , Int32 pSaleCurrencyID, Int32 pPOrC, Decimal pSaleExchangeRate, Int32 pOperationPartnerTypeID, Int32 pPartnerTypeID, Int32 pPartnerID
        , Int32 pViewOrder, string pNotes,Int32 pSupplierSiteID, Decimal pAdditionalAmount)
        {
            bool _result = false;
            CVarQuotationCharges objCVarQuotationCharges = new CVarQuotationCharges();

            //the next 4 lines to get the CreatorUserID and CreationDate and set them as they were entered before
            CQuotationCharges objCGetCreationInformation = new CQuotationCharges();
            objCGetCreationInformation.GetItem(pID);
            objCVarQuotationCharges.CreatorUserID = objCGetCreationInformation.lstCVarQuotationCharges[0].CreatorUserID;
            objCVarQuotationCharges.CreationDate = objCGetCreationInformation.lstCVarQuotationCharges[0].CreationDate;
            objCVarQuotationCharges.PricingID = objCGetCreationInformation.lstCVarQuotationCharges[0].PricingID;

            objCVarQuotationCharges.ID = pID;

            objCVarQuotationCharges.QuotationRouteID = pQuotationRouteID;
            objCVarQuotationCharges.ChargeTypeID = pChargeTypeID;
            objCVarQuotationCharges.MeasurementID = pMeasurementID;
            objCVarQuotationCharges.ContainerTypeID = pContainerTypeID;
            objCVarQuotationCharges.DemurrageDays = pDemurrageDays;
            objCVarQuotationCharges.PackageTypeID = pPackageTypeID;
            objCVarQuotationCharges.CostQuantity = pCostQuantity;
            objCVarQuotationCharges.CostPrice = pCostPrice;
            objCVarQuotationCharges.CostAmount = pCostAmount;
            objCVarQuotationCharges.AdditionalAmount = pAdditionalAmount;
            objCVarQuotationCharges.CostCurrencyID = pCostCurrencyID;
            objCVarQuotationCharges.CostExchangeRate = pCostExchangeRate;
            objCVarQuotationCharges.SaleQuantity = pSaleQuantity;
            objCVarQuotationCharges.SalePrice = pSalePrice;
            objCVarQuotationCharges.SaleAmount = pSaleAmount;
            objCVarQuotationCharges.SaleCurrencyID = pSaleCurrencyID;
            objCVarQuotationCharges.POrC = pPOrC;
            objCVarQuotationCharges.SaleExchangeRate = pSaleExchangeRate;
            objCVarQuotationCharges.ViewOrder = pViewOrder;

            objCVarQuotationCharges.Notes = pNotes;

            objCVarQuotationCharges.OperationPartnerTypeID = pOperationPartnerTypeID;
            objCVarQuotationCharges.CustomerID = GetPartnerID(1, pPartnerTypeID, pPartnerID);
            objCVarQuotationCharges.AgentID = GetPartnerID(2, pPartnerTypeID, pPartnerID);
            objCVarQuotationCharges.ShippingAgentID = GetPartnerID(3, pPartnerTypeID, pPartnerID);
            objCVarQuotationCharges.CustomsClearanceAgentID = GetPartnerID(4, pPartnerTypeID, pPartnerID);
            objCVarQuotationCharges.ShippingLineID = GetPartnerID(5, pPartnerTypeID, pPartnerID);
            objCVarQuotationCharges.AirlineID = GetPartnerID(6, pPartnerTypeID, pPartnerID);
            objCVarQuotationCharges.TruckerID = GetPartnerID(7, pPartnerTypeID, pPartnerID);
            objCVarQuotationCharges.SupplierID = GetPartnerID(8, pPartnerTypeID, pPartnerID);
            objCVarQuotationCharges.SupplierSiteID = pSupplierSiteID;
            objCVarQuotationCharges.ModificatorUserID = WebSecurity.CurrentUserId;
            objCVarQuotationCharges.ModificationDate = DateTime.Now;

            CQuotationCharges objCQuotationCharges = new CQuotationCharges();
            objCQuotationCharges.lstCVarQuotationCharges.Add(objCVarQuotationCharges);
            Exception checkException = objCQuotationCharges.SaveMethod(objCQuotationCharges.lstCVarQuotationCharges);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
                _result = true;
            return _result;
        }

        //update multi row with values 
        [HttpGet, HttpPost]
        public object[] UpdateList([FromBody]UpdateListParamteres updateListParamteres)
        {
            bool _result = false;
            string updateClause = "";
            CvwQuotationCharges objCvwQuotationCharges = new CvwQuotationCharges();

            int NumberOfRows = updateListParamteres.pSelectedIDsToUpdate.Split(',').Length;

            for (int i = 0; i < NumberOfRows; i++)
            {
                updateClause = " MeasurementID = " + (updateListParamteres.pMeasurementIDList.Split(',')[i] == "0" ? " NULL " : updateListParamteres.pMeasurementIDList.Split(',')[i]);
                updateClause += " , CostQuantity = " + updateListParamteres.pQuantityList.Split(',')[i];
                updateClause += " , CostPrice = " + updateListParamteres.pCostPriceList.Split(',')[i];
                updateClause += " , CostAmount = " + updateListParamteres.pCostAmountList.Split(',')[i];
                updateClause += " , SalePrice = " + updateListParamteres.pSalePriceList.Split(',')[i];
                updateClause += " , SaleAmount = " + updateListParamteres.pSaleAmountList.Split(',')[i];
                updateClause += " , CostCurrencyID = " + (updateListParamteres.pCostCurrencyList.Split(',')[i] == "0" ? " NULL " : updateListParamteres.pCostCurrencyList.Split(',')[i]);
                updateClause += " , CostExchangeRate = " + updateListParamteres.pCostExchangeRateList.Split(',')[i];
                updateClause += " , SaleCurrencyID = " + (updateListParamteres.pSaleCurrencyList.Split(',')[i] == "0" ? " NULL " : updateListParamteres.pSaleCurrencyList.Split(',')[i]);
                updateClause += " , POrC = " + (updateListParamteres.pPOrCList.Split(',')[i] == "0" ? " NULL " : updateListParamteres.pPOrCList.Split(',')[i]);
                updateClause += " , SaleExchangeRate = " + updateListParamteres.pSaleExchangeRateList.Split(',')[i];
                updateClause += " , ContainerTypeID = " + (updateListParamteres.pContainerTypeIDList.Split(',')[i] == "0" ? " NULL " : updateListParamteres.pContainerTypeIDList.Split(',')[i]);
                updateClause += " , PackageTypeID = " + (updateListParamteres.pPackageTypeIDList.Split(',')[i] == "0" ? " NULL " : updateListParamteres.pPackageTypeIDList.Split(',')[i]);
                updateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                updateClause += " , ModificationDate = GETDATE() ";

                updateClause += " WHERE ID = " + updateListParamteres.pSelectedIDsToUpdate.Split(',')[i];

                CQuotationCharges objCQuotationCharges = new CQuotationCharges();
                Exception checkException = objCQuotationCharges.UpdateList(updateClause);
                if (checkException == null)
                {
                    _result = true;
                    string pWhereClauseQuotationCharge = "WHERE QuotationRouteID = " + updateListParamteres.pQuotationRouteID.ToString();
                    #region DepartmentCharge Case
                    Int32 _RowCount = 0;
                    CDefaults objCDefaults = new CDefaults();
                    CUsers objCUsers = new CUsers();
                    objCDefaults.GetListPaging(999999, 1, "WHERE 1=1", "ID", out _RowCount);
                    objCUsers.GetListPaging(999999, 1, "WHERE ID=" + WebSecurity.CurrentUserId, "ID", out _RowCount);
                    if (!objCUsers.lstCVarUsers[0].IsAccessAllCharges)
                        pWhereClauseQuotationCharge += " AND ChargeTypeID IN (SELECT ChargeTypeID FROM DepartmentCharge WHERE DepartmentID=" + objCUsers.lstCVarUsers[0].DepartmentID + ") \n";
                    #endregion DepartmentCharge Case

                    objCvwQuotationCharges.GetListPaging(1000, 1, pWhereClauseQuotationCharge, "ID", out _RowCount);
                }
            }

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                _result
                , _result ? serializer.Serialize(objCvwQuotationCharges.lstCVarvwQuotationCharges) : null
            };
        }


        [HttpGet, HttpPost]
        public object[] ApplyQuotationChargesToOperation(Int64 pGeneratingQRID, Int64 pOperationID)
        {
            string pReturnedMessage = "";
            Exception checkException = null;
            int _RowCount = 0;

            CQuotationCharges objCQuotationCharges = new CQuotationCharges();
            CPayables objCPayables = new CPayables();
            CReceivables objCReceivables = new CReceivables();
            string updateList = "";

            // Insert into Payables any Charges that doesn't exist
            checkException = objCQuotationCharges.GetListPaging(999, 1, "WHERE QuotationRouteID=" + pGeneratingQRID + " AND ChargeTypeID NOT IN (SELECT ChargeTypeID FROM Payables WHERE OperationID="+ pOperationID + ")", "ID", out _RowCount);
            if (_RowCount > 0)
            {
                string pSelectedIDs = "";
                int i = 0;
                for (i = 0; i < _RowCount; i++)
                {
                    pSelectedIDs += (pSelectedIDs == "" ? "" : ",") + objCQuotationCharges.lstCVarQuotationCharges[i].ChargeTypeID;
                }
                var PayablesController = new PayablesController();
                bool Result = PayablesController.InsertListWithoutValues(pOperationID, pSelectedIDs, pGeneratingQRID, 0, 0, 0);

            }
            // Insert into Receivables any Charges that doesn't exist
            checkException = objCQuotationCharges.GetListPaging(999, 1, "WHERE QuotationRouteID=" + pGeneratingQRID + " AND ChargeTypeID NOT IN (SELECT ChargeTypeID FROM Receivables WHERE OperationID=" + pOperationID + ")", "ID", out _RowCount);
            if (_RowCount > 0)
            {
                string pSelectedIDs = "";
                int i = 0;
                for (i = 0; i < _RowCount; i++)
                {
                    pSelectedIDs += (pSelectedIDs == "" ? "" : ",") + objCQuotationCharges.lstCVarQuotationCharges[i].ChargeTypeID;
                }
                var ReceivablesController = new ReceivablesController();
                bool Result = ReceivablesController.InsertListWithoutValues(pOperationID, pSelectedIDs, pGeneratingQRID, 0, 0, 0);

            }

            // Update All
            checkException = objCQuotationCharges.GetListPaging(999, 1, "WHERE QuotationRouteID=" + pGeneratingQRID, "ID", out _RowCount);
            for (int i = 0; i < objCQuotationCharges.lstCVarQuotationCharges.Count; i++)
            {
                #region Payables
                string PayablePOrC = objCQuotationCharges.lstCVarQuotationCharges[i].POrC == 0 ? "NULL" : objCQuotationCharges.lstCVarQuotationCharges[i].POrC.ToString();

                updateList = "QuotationCost=" + objCQuotationCharges.lstCVarQuotationCharges[i].CostAmount + " \n";

                updateList += ",Quantity=" + objCQuotationCharges.lstCVarQuotationCharges[i].CostQuantity + " \n";
                updateList += ",CostPrice=" + objCQuotationCharges.lstCVarQuotationCharges[i].CostPrice + " \n";
                updateList += ",InitialSalePrice=" + objCQuotationCharges.lstCVarQuotationCharges[i].SalePrice + " \n";
                updateList += ",CostAmount=" + objCQuotationCharges.lstCVarQuotationCharges[i].CostAmount + " \n";
                updateList += ",POrC=" + PayablePOrC + " \n";
                updateList += ",TaxTypeID=null,TaxPercentage=null,TaxAmount=null,DiscountTypeID=null,DiscountPercentage=null,DiscountAmount=null" + " \n";
                updateList += ",Notes=N'Updated from quotations'" + " \n";
                updateList += ",ModificatorUserID=" + WebSecurity.CurrentUserId + " \n";
                updateList += ",ModificationDate=GETDATE()" + " \n";
                updateList += "WHERE IsApproved=0 AND IsDeleted=0 AND OperationID=" + pOperationID + " AND GeneratingQRID=" + pGeneratingQRID + " \n";
                updateList += "AND AccNoteID IS NULL " + " \n";
                updateList += "AND ChargeTypeID=" + objCQuotationCharges.lstCVarQuotationCharges[i].ChargeTypeID + " \n";
                checkException = objCPayables.UpdateList(updateList);
                #endregion Payables

                #region Receivables
                updateList = "Quantity=" + objCQuotationCharges.lstCVarQuotationCharges[i].SaleQuantity + " \n";
                updateList += ",SalePrice=" + objCQuotationCharges.lstCVarQuotationCharges[i].SalePrice + " \n";
                updateList += ",SaleAmount=" + objCQuotationCharges.lstCVarQuotationCharges[i].SaleAmount + " \n";
                updateList += ",TaxeTypeID=null,TaxPercentage=null,TaxAmount=null,DiscountTypeID=null,DiscountPercentage=null,DiscountAmount=null" + " \n";
                updateList += ",Notes=N'Updated from quotations'" + " \n";
                updateList += ",ModificatorUserID=" + WebSecurity.CurrentUserId + " \n";
                updateList += ",ModificationDate=GETDATE()" + " \n";
                updateList += "WHERE InvoiceID IS NULL AND OperationID=" + pOperationID + " AND GeneratingQRID=" + pGeneratingQRID + " \n";
                updateList += "AND AccNoteID IS NULL " + " \n";
                updateList += "AND ChargeTypeID=" + objCQuotationCharges.lstCVarQuotationCharges[i].ChargeTypeID + " \n";
                checkException = objCReceivables.UpdateList(updateList);
                #endregion Receivables
            }
            return new object[]
            {
                pReturnedMessage
            };
        }

        [HttpGet, HttpPost]
        public object[] ApplyQuotationRoutingsToOperation(Int64 pGeneratingQRID, Int64 pOperationID_Routings)
        {
            string pReturnedMessage = "";
            Exception checkException = null;
            int _RowCount = 0;
            //var MainCarraigeRoutingTypeID = 30;

            CQuotationRoute objCQuotationRoute = new CQuotationRoute();
            COperations objCOperations = new COperations();
            CRoutings objCRoutings = new CRoutings();
            string updateClause = "";
            checkException = objCQuotationRoute.GetListPaging(999, 1, "WHERE ID=" + pGeneratingQRID, "ID", out _RowCount);
            checkException = objCOperations.UpdateList("POLCountryID=" + objCQuotationRoute.lstCVarQuotationRoute[0].POLCountryID + ",POL=" + objCQuotationRoute.lstCVarQuotationRoute[0].POL + ",PODCountryID=" + objCQuotationRoute.lstCVarQuotationRoute[0].PODCountryID + ",POD=" + objCQuotationRoute.lstCVarQuotationRoute[0].POD + " WHERE (ID=" + pOperationID_Routings + " OR MasterOperationID=" + pOperationID_Routings + ")");
            updateClause = "POLCountryID=" + objCQuotationRoute.lstCVarQuotationRoute[0].POLCountryID + ",POL=" + objCQuotationRoute.lstCVarQuotationRoute[0].POL + ",PODCountryID=" + objCQuotationRoute.lstCVarQuotationRoute[0].PODCountryID + ",POD=" + objCQuotationRoute.lstCVarQuotationRoute[0].POD + " \n ";
            updateClause += " WHERE (OperationID=" + pOperationID_Routings + " OR OperationID IN (SELECT ID FROM Operations WHERE MasterOperationID=" + pOperationID_Routings + ")" + ")";
            //updateClause += " WHERE (OperationID=" + pOperationID_Routings + " OR OperationID IN (SELECT ID FROM Operations WHERE MasterOperationID=" + pOperationID_Routings + ")" + ")" + " AND RoutingTypeID=" + MainCarraigeRoutingTypeID;
            checkException = objCRoutings.UpdateList(updateClause);
            return new object[]
            {
                pReturnedMessage
            };
        }

        [HttpGet, HttpPost]
        public object[] CopyCharge(Int64 pChargeIDToCopy, Int32 pNumberOfDuplicates)
        {
            CvwQuotationCharges objCvwQuotationCharges = new CvwQuotationCharges();
            CQuotationCharges objCQuotationCharges = new CQuotationCharges();
            Int64 pQuotationRouteID = 0;
            int _RowCount = 0;
            objCvwQuotationCharges.GetListPaging(999999, 1, "WHERE ID=" + pChargeIDToCopy, "ID", out _RowCount);
            pQuotationRouteID = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].QuotationRouteID;
            for (int i = 0; i < pNumberOfDuplicates; i++)
            {
                CVarQuotationCharges objCVarQuotationCharges = new CVarQuotationCharges();
                objCVarQuotationCharges.QuotationRouteID = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].QuotationRouteID;
                objCVarQuotationCharges.ChargeTypeID = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].ChargeTypeID;
                objCVarQuotationCharges.MeasurementID = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].MeasurementID;
                objCVarQuotationCharges.CostQuantity = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].CostQuantity;
                objCVarQuotationCharges.CostPrice = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].CostPrice;
                objCVarQuotationCharges.CostAmount = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].CostAmount;
                objCVarQuotationCharges.CostCurrencyID = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].CostCurrencyID;
                objCVarQuotationCharges.CostExchangeRate = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].CostExchangeRate;
                objCVarQuotationCharges.SaleQuantity = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].SaleQuantity;
                objCVarQuotationCharges.SalePrice = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].SalePrice;
                objCVarQuotationCharges.SaleAmount = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].SaleAmount;
                objCVarQuotationCharges.SaleCurrencyID = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].SaleCurrencyID;
                objCVarQuotationCharges.POrC = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].POrC;
                objCVarQuotationCharges.SaleExchangeRate = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].SaleExchangeRate;
                objCVarQuotationCharges.OperationPartnerTypeID = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].OperationPartnerTypeID;
                objCVarQuotationCharges.CustomerID = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].CustomerID;
                objCVarQuotationCharges.AgentID = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].AgentID;
                objCVarQuotationCharges.ShippingAgentID = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].ShippingAgentID;
                objCVarQuotationCharges.CustomsClearanceAgentID = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].CustomsClearanceAgentID;
                objCVarQuotationCharges.ShippingLineID = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].ShippingLineID;
                objCVarQuotationCharges.AirlineID = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].AirlineID;
                objCVarQuotationCharges.TruckerID = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].TruckerID;
                objCVarQuotationCharges.SupplierID = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].SupplierID;
                objCVarQuotationCharges.ViewOrder = objCvwQuotationCharges.lstCVarvwQuotationCharges[0].ViewOrder;
                objCVarQuotationCharges.Notes = "COPIED";

                objCVarQuotationCharges.CreatorUserID = objCVarQuotationCharges.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarQuotationCharges.CreationDate = objCVarQuotationCharges.ModificationDate = DateTime.Now;

                objCQuotationCharges.lstCVarQuotationCharges.Add(objCVarQuotationCharges);
            }
            objCQuotationCharges.SaveMethod(objCQuotationCharges.lstCVarQuotationCharges);

            objCvwQuotationCharges.GetListPaging(999999, 1, "WHERE QuotationRouteID=" + pQuotationRouteID, "ID", out _RowCount);
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                serializer.Serialize(objCvwQuotationCharges.lstCVarvwQuotationCharges)
            };
        }

        [HttpGet, HttpPost]
        public bool Delete(String pQuotationChargesIDs)
        {
            bool _result = false;
            CQuotationCharges objCQuotationCharges = new CQuotationCharges();
            CQuotationRouteLog objCQuotationRouteLog = new CQuotationRouteLog();
            foreach (var currentID in pQuotationChargesIDs.Split(','))
            {
                objCQuotationCharges.lstDeletedCPKQuotationCharges.Add(new CPKQuotationCharges() { ID = Int64.Parse(currentID.Trim()) });
            }

            Exception checkException = objCQuotationCharges.DeleteItem(objCQuotationCharges.lstDeletedCPKQuotationCharges);
            objCQuotationRouteLog.UpdateList("UserID=" + WebSecurity.CurrentUserId.ToString() + ", UserName=N'" + WebSecurity.CurrentUserName + "'"
                                        + " WHERE ActionOnRowID IN (" + pQuotationChargesIDs + ") AND ActionOnRowID IN(" + pQuotationChargesIDs.ToString() + ")"
                                        + " AND ActionType='D' AND UserID IS NULL AND LogFor = " + 10);

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
        public bool ApplyDefaultQuotationCharges(Int64 pQuotationRouteID, string pWhereClause)
        {
            bool _result = false;
            int _RowCount = 0;
            CQuotationCharges objCQuotationCharges = new CQuotationCharges();
            Exception checkException = objCQuotationCharges.DeleteList(" WHERE QuotationRouteID = " + pQuotationRouteID.ToString());
            if (checkException == null)
            {
                //those 2 lines are to get the Charge types from DB
                CDefaults objCDefaults = new CDefaults();
                objCDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa

                //those 2 lines are to get the default charge types from DB
                CvwChargeTypes objCvwChargeTypes = new CvwChargeTypes();
                //objCvwChargeTypes.GetList(pWhereClause);
                objCvwChargeTypes.GetListPaging(1500, 1, pWhereClause, " ChargeTypeName ", out _RowCount);

                foreach (var rowChargeType in objCvwChargeTypes.lstCVarvwChargeTypes)
                {
                    CVarQuotationCharges objCVarQuotationCharges = new CVarQuotationCharges();

                    objCVarQuotationCharges.QuotationRouteID = pQuotationRouteID;
                    objCVarQuotationCharges.ChargeTypeID = rowChargeType.ID;
                    objCVarQuotationCharges.MeasurementID = rowChargeType.MeasurementID;

                    objCVarQuotationCharges.CostQuantity = 1;
                    objCVarQuotationCharges.CostCurrencyID = objCDefaults.lstCVarDefaults[0].CurrencyID;
                    objCVarQuotationCharges.CostExchangeRate = 1;
                    objCVarQuotationCharges.SaleCurrencyID = objCDefaults.lstCVarDefaults[0].CurrencyID;
                    objCVarQuotationCharges.POrC = 0;
                    objCVarQuotationCharges.SaleExchangeRate = 1;
                    objCVarQuotationCharges.Notes = rowChargeType.Notes;
                    objCVarQuotationCharges.CreatorUserID = objCVarQuotationCharges.ModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarQuotationCharges.CreationDate = objCVarQuotationCharges.ModificationDate = DateTime.Now;

                    objCQuotationCharges.lstCVarQuotationCharges.Add(objCVarQuotationCharges);
                }
                checkException = objCQuotationCharges.SaveMethod(objCQuotationCharges.lstCVarQuotationCharges);
                if (checkException == null)
                    _result = true;
            }
            return _result;
        }
        [HttpGet, HttpPost]
        public object[] ApplyDefaultTemplateCharges(Int64 pQuotationRouteID, string pTemplateID)
        {
            bool _result = false;
            Exception checkException = null;
            int _RowCount = 0;
            //Delete old charges
            CQuotationCharges objCQuotationCharges = new CQuotationCharges();
            checkException = objCQuotationCharges.DeleteList(" WHERE QuotationRouteID = " + pQuotationRouteID.ToString());

            CvwQuotationCharges objCvwQuotationCharges = new CvwQuotationCharges();
            if (checkException == null)
            {
                //those 2 lines are to get the Currency from DB
                CDefaults objCDefaults = new CDefaults();
                objCDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa

                //those 2 lines are to get the default charge types from DB
                CvwChargeTypes objCvwChargeTypes = new CvwChargeTypes();
                //objCvwChargeTypes.GetList(pWhereClause);
                objCvwChargeTypes.GetListPaging(1500, 1, "WHERE TemplateID=" + pTemplateID.ToString(), "ChargeTypeName", out _RowCount);

                foreach (var rowChargeType in objCvwChargeTypes.lstCVarvwChargeTypes)
                {
                    CVarQuotationCharges objCVarQuotationCharges = new CVarQuotationCharges();

                    objCVarQuotationCharges.QuotationRouteID = pQuotationRouteID;
                    objCVarQuotationCharges.ChargeTypeID = rowChargeType.ID;
                    objCVarQuotationCharges.MeasurementID = rowChargeType.MeasurementID;

                    objCVarQuotationCharges.CostQuantity = 1;
                    objCVarQuotationCharges.CostCurrencyID = objCDefaults.lstCVarDefaults[0].CurrencyID;
                    objCVarQuotationCharges.CostExchangeRate = 1;
                    objCVarQuotationCharges.SaleCurrencyID = objCDefaults.lstCVarDefaults[0].CurrencyID;
                    objCVarQuotationCharges.POrC = 0;
                    objCVarQuotationCharges.SaleExchangeRate = 1;
                    objCVarQuotationCharges.Notes = rowChargeType.Notes;
                    objCVarQuotationCharges.CreatorUserID = objCVarQuotationCharges.ModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarQuotationCharges.CreationDate = objCVarQuotationCharges.ModificationDate = DateTime.Now;

                    objCQuotationCharges.lstCVarQuotationCharges.Add(objCVarQuotationCharges);
                }
                checkException = objCQuotationCharges.SaveMethod(objCQuotationCharges.lstCVarQuotationCharges);
                if (checkException == null)
                {
                    _result = true;
                    objCvwQuotationCharges.GetListPaging(9999, 1, "WHERE QuotationRouteID=" + pQuotationRouteID.ToString(), "ID", out _RowCount);
                }
            }
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[]{
                _result
                , checkException == null ? serializer.Serialize(objCvwQuotationCharges.lstCVarvwQuotationCharges): null
            };
        }
        [HttpGet, HttpPost]
        public object[] SendQuotationChargesMessage(string pSubject, string pBody, Int64 pConfirmedQuotationRouteID)
        {
            string _MessageReturned = "";
            string _MailMessageReturned = "";
            string pAlarmReceiversIDs = "";
            Exception checkException = null;
            int _RowCount = 0;
            CDefaults objCDefaults = new CDefaults();
            checkException = objCDefaults.GetListPaging(999999, 1, "WHERE 1=1", "ID", out _RowCount);
            CvwQuotationRoute objCvwQuotationRoute = new CvwQuotationRoute();
            CQuotationRoute objCQuotationRoute = new CQuotationRoute();
            checkException = objCQuotationRoute.UpdateList("IsChargesConfirmed=1, ChargesConfirmingUserID=" + WebSecurity.CurrentUserId + " WHERE ID IN(" + pConfirmedQuotationRouteID + ")");
            checkException = objCvwQuotationRoute.GetListPaging(999999, 1, "WHERE ID=" + pConfirmedQuotationRouteID, "ID", out _RowCount);
            #region Sending Alarm
            //if (pAlarmReceiversIDs != null)
            {
                CVarEmail objCVarEmail = new CVarEmail();
                CEmail objCEmail = new CEmail();
                CEmailReceiver objCEmailReceiver = new CEmailReceiver();
                objCVarEmail.Subject = pSubject;
                objCVarEmail.Body = pBody;
                objCVarEmail.QuotationRouteID = 0;
                objCVarEmail.SenderUserID = WebSecurity.CurrentUserId;
                objCVarEmail.SendingDate = DateTime.Now;
                //objCVarEmail.QuotationRequestID = objCQuotationRoute.lstCVarQuotationRoute[0].QuotationID;
                //objCVarEmail.QuotationRouteRequestID = pQuotationRouteID;
                objCEmail.lstCVarEmail.Add(objCVarEmail);
                checkException = objCEmail.SaveMethod(objCEmail.lstCVarEmail);
                if (checkException == null) //add to EmailReceivers
                {
                    pAlarmReceiversIDs = objCvwQuotationRoute.lstCVarvwQuotationRoute[0].CreatorUserID.ToString();
                    var pArrayOfReceiversIDs = pAlarmReceiversIDs.Split(',');
                    var NoOfReceivers = pArrayOfReceiversIDs.Length;
                    for (int i = 0; i < NoOfReceivers; i++)
                    {
                        CVarEmailReceiver objCVarEmailReceiver = new CVarEmailReceiver();
                        objCVarEmailReceiver.EmailID = objCVarEmail.ID;
                        objCVarEmailReceiver.ReceiverUserID = int.Parse(pArrayOfReceiversIDs[i]);
                        objCVarEmailReceiver.ReceivingDate = DateTime.Parse("01-01-1900");
                        objCVarEmailReceiver.IsAlarm = true;

                        objCEmailReceiver.lstCVarEmailReceiver.Add(objCVarEmailReceiver);
                    }
                    checkException = objCEmailReceiver.SaveMethod(objCEmailReceiver.lstCVarEmailReceiver);
                    objCEmailReceiver.UpdateList("IsAlarm=0 WHERE ID IN (SELECT ID FROM vwEmailReceiver WHERE QuotationRouteRequestID=" + pConfirmedQuotationRouteID + " AND ReceiverUserID=" + WebSecurity.CurrentUserId + ")");
                }
            }
            #endregion Sending Alarm
            #region Send Email
            if (pAlarmReceiversIDs != "" && objCDefaults.lstCVarDefaults[0].Email != "0" && objCDefaults.lstCVarDefaults[0].Email != ""
                && objCDefaults.lstCVarDefaults[0].Email_Password != "0" && objCDefaults.lstCVarDefaults[0].Email_Password != ""
                && objCDefaults.lstCVarDefaults[0].Email_DisplayName != "0" && objCDefaults.lstCVarDefaults[0].Email_DisplayName != ""
                && objCDefaults.lstCVarDefaults[0].Email_Host != "0" && objCDefaults.lstCVarDefaults[0].Email_Host != ""
                && objCDefaults.lstCVarDefaults[0].Email_Port != 0 && objCDefaults.lstCVarDefaults[0].IsDepartmentOption
                && objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "GBL" && 1 == 2)
            {
                CUsers objCUsers = new CUsers();
                checkException = objCUsers.GetList("WHERE IsNull(CustomerID , 0) = 0 AND IsInactive=0 AND Email<>'' AND Email<>'0' AND ID IN(" + pAlarmReceiversIDs + ")");

                string FromMail = objCDefaults.lstCVarDefaults[0].Email;
                bool _boolEmailFound = false;
                var mail = new MailMessage();
                //SmtpClient Client = new SmtpClient("mail.sharksbay.com", 587);
                SmtpClient SmtpServer = new SmtpClient(objCDefaults.lstCVarDefaults[0].Email_Host, objCDefaults.lstCVarDefaults[0].Email_Port);
                SmtpServer.UseDefaultCredentials = true;
                SmtpServer.Credentials = new System.Net.NetworkCredential(objCDefaults.lstCVarDefaults[0].Email, CEncryptDecrypt.Decrypt(objCDefaults.lstCVarDefaults[0].Email_Password, true));

                //SmtpClient SmtpServer = new SmtpClient();
                //SmtpClient SmtpServer = new SmtpClient("smtpout.secureserver.net", SmtpServer.Port);
                mail.From = new MailAddress(objCDefaults.lstCVarDefaults[0].Email, objCDefaults.lstCVarDefaults[0].Email_DisplayName);
                for (int i = 0; i < objCUsers.lstCVarUsers.Count; i++)
                    if (objCUsers.lstCVarUsers[i].Email != "" && objCUsers.lstCVarUsers[i].Email != "0")
                    {
                        _boolEmailFound = true;
                        mail.To.Add(objCUsers.lstCVarUsers[i].Email);
                    }
                //mail.CC.Add(CC);
                mail.Subject = pSubject;
                mail.Body = "<b>Sender : " + WebSecurity.CurrentUserName + "</b><br>";
                mail.Body += pBody;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment(pathString));
                //mail.Attachments.Add(new Attachment("C:\\Users\\Sherif\\Desktop\\CompanyLogo.jpg"));
                //SmtpServer.Port = 25;
                //SmtpServer.Port = Convert.ToInt32(ConfigurationSettings.AppSettings["PORT"].ToString());
                //SmtpServer.Port = Convert.ToInt32(Configuration!System.Configuration.ConfigurationManager.AppSettings["PORT"].ToString());
                SmtpServer.EnableSsl = objCDefaults.lstCVarDefaults[0].Email_IsSSL;
                if (_boolEmailFound)
                    try
                    {
                        SmtpServer.Send(mail);
                    }
                    catch (Exception ex)
                    {
                        _MailMessageReturned = ex.Message;
                    }
            }
            #endregion Send Email
            return new object[]
            {
                _MessageReturned
            };
        }
        #region OperationPayablesAndReceivables
        [HttpGet, HttpPost]
        public object[] GetOperationPayablesAndReceivables(Int64 pOperationID, Int64 pQuotationRouteID, Int64 pOperationVehicleID,Int64 pTruckingOrderID, string pCodeSearch)
        {
            bool _result = false;

            string pWhereClausePayables = "WHERE OperationID=" + pOperationID.ToString() + (pQuotationRouteID == 0 ? " " : " AND GeneratingQRID=" + pQuotationRouteID.ToString()) + (pOperationVehicleID == 0 ? " " : " AND OperationVehicleID=" + pOperationVehicleID.ToString()) + (pTruckingOrderID == 0 ? " " : " AND TruckingOrderID=" + pTruckingOrderID.ToString()) ;
            string pWhereClauseReceivables = "WHERE OperationID=" + pOperationID.ToString() + " AND IsDeleted=0 " + (pQuotationRouteID == 0 ? " " : " AND GeneratingQRID=" + pQuotationRouteID.ToString()) + (pOperationVehicleID == 0 ? " " : " AND OperationVehicleID=" + pOperationVehicleID.ToString()) + (pTruckingOrderID == 0 ? " " : " AND TruckingOrderID=" + pTruckingOrderID.ToString());

            #region DepartmentCharge Case
            Int32 _RowCount = 0;
            CDefaults objCDefaults = new CDefaults();
            CUsers objCUsers = new CUsers();
            objCDefaults.GetListPaging(999999, 1, "WHERE 1=1", "ID", out _RowCount);
            objCUsers.GetListPaging(999999, 1, "WHERE ID=" + WebSecurity.CurrentUserId, "ID", out _RowCount);
            if (!objCUsers.lstCVarUsers[0].IsAccessAllCharges) {
                pWhereClausePayables += " AND ChargeTypeID IN (SELECT ChargeTypeID FROM DepartmentCharge WHERE DepartmentID=" + objCUsers.lstCVarUsers[0].DepartmentID + ") \n";
                pWhereClauseReceivables += " AND ChargeTypeID IN (SELECT ChargeTypeID FROM DepartmentCharge WHERE DepartmentID=" + objCUsers.lstCVarUsers[0].DepartmentID + ") \n";
            }
            #endregion DepartmentCharge Case

            #region Add Code Filter
            if (pCodeSearch != "0")
            {
                pWhereClausePayables += " AND ChargeTypeCode LIKE N'%" + pCodeSearch + "%'" + " \n";
                pWhereClauseReceivables += " AND ChargeTypeCode LIKE N'%" + pCodeSearch + "%'" + " \n";
            }
            #endregion Add Code Filter
            Exception checkException = new Exception();
            CvwPayables objCvwPayables = new CvwPayables();
            CvwReceivables objCvwReceivables = new CvwReceivables();
            checkException = objCvwPayables.GetListPaging(1000, 1, pWhereClausePayables, "ChargeTypeName", out _RowCount);
            checkException = objCvwReceivables.GetListPaging(1000, 1, pWhereClauseReceivables, "ChargeTypeName", out _RowCount);
            if (checkException == null)
                _result = true;
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[]
            {
                _result
                , _result ? serializer.Serialize(objCvwPayables.lstCVarvwPayables) : null //pData[1]
                , _result ? serializer.Serialize(objCvwReceivables.lstCVarvwReceivables) : null //pData[2]
            };
        }
        #endregion OperationPayablesAndReceivables

        #region Pricing
        
        [HttpGet, HttpPost]
        public Object[] Pricing_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned(bool pIsReturnObjectArray, Int32 pPricingTypeID, Int32 pPageNumber, Int32 pPageSize, string pWhereClause, string pOrderBy)
        {
            int _RowCount = 0;
            
            CvwPricing objCvwPricing = new CvwPricing();
            CPricingCharge objCPricingCharge = new CPricingCharge();
            
            string pPricingIDs = "";
            //int constPricingOcean = 10;
            //int constPricingAir = 20;
            //int constPricingInland = 30;
            //int constPricingCustomsClearance = 40;
            //int constPricingGeneral = 50;

            objCvwPricing.GetListPaging(pPageSize, pPageNumber, pWhereClause, pOrderBy, out _RowCount);

            #region Get PricingIDs to be used in PricingCharges GetList as whereClause
            if (objCvwPricing.lstCVarvwPricing.Count > 0) //otherwise return the empty PricingCharge
            {
                pPricingIDs = objCvwPricing.lstCVarvwPricing[0].ID.ToString();
                for (int i = 1; i < objCvwPricing.lstCVarvwPricing.Count; i++)
                    pPricingIDs += "," + objCvwPricing.lstCVarvwPricing[i].ID.ToString();
                objCPricingCharge.GetListPaging(1000, 1, "WHERE PricingID IN (" + pPricingIDs + ")", "ID", out _RowCount);
            }
            #endregion Get PricingIDs to get PricingCharges
            
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] {
                serializer.Serialize(objCvwPricing.lstCVarvwPricing)
                , serializer.Serialize(objCPricingCharge.lstCVarPricingCharge) //data[1]
            };
        }

        [HttpGet, HttpPost]
        public object[] FillPricingChargesModal(Int32 pPricingTypeID, Int32 pMoveTypeID)
        {
            int _RowCount = 0;
            CDefaults objCDefaults = new CDefaults();
            objCDefaults.GetListPaging(999999, 1, "WHERE 1=1", "ID", out _RowCount);
            CMoveTypes objCMoveTypes = new CMoveTypes();
            objCMoveTypes.GetListPaging(999999, 1, "WHERE ID=" + pMoveTypeID, "ID", out _RowCount);

            CvwContainerTypes objCvwContainerTypes = new CvwContainerTypes();
            objCvwContainerTypes.GetList("ORDER BY Code");
            CTruckers objCTruckers = new CTruckers();
            objCTruckers.GetListPaging(9999, 1, "WHERE 1=1", "Name", out _RowCount);
            CvwPricingSettings objCvwPricingSettings = new CvwPricingSettings();
            //objCvwPricingSettings.GetListPaging(100, 1, "WHERE PricingTypeID=" + pPricingTypeID.ToString(), "ChargeTypeName", out _RowCount);
            objCvwPricingSettings.GetListPaging(100, 1, "WHERE 1=1", "ChargeTypeName", out _RowCount);

            return new Object[] {
                new JavaScriptSerializer().Serialize(objCvwContainerTypes.lstCVarvwContainerTypes) //pContainerTypes = pData[0]
                , new JavaScriptSerializer().Serialize(objCTruckers.lstCVarTruckers) //pTruckers = pData[1]
                , new JavaScriptSerializer().Serialize(objCvwPricingSettings.lstCVarvwPricingSettings) //pPricingSettings = pData[2]
                , pMoveTypeID == 0 ? null : new JavaScriptSerializer().Serialize(objCMoveTypes.lstCVarMoveTypes[0]) //pServiceScope = pData[3];
            };

        }

        [HttpGet, HttpPost]
        public object[] AddSelectedPricingCharges(Int64 pQuotationRouteID, string pSelectedPricingIDs, Int32 pProfitType, decimal pProfitAmount)
        {
            Exception checkException = null;
            int _RowCount = 0;
            CPricing objCPricing = new CPricing();
            CPricingCharge objCPricingCharge = new CPricingCharge();
            CQuotationCharges objCQuotationCharges = new CQuotationCharges();
            CvwQuotationCharges objCvwQuotationCharges = new CvwQuotationCharges();
            CDefaults objCDefaults = new CDefaults();
            objCDefaults.GetListPaging(99999, 1, "WHERE 1=1", "ID", out _RowCount);

            int constPricingOcean = 10;
            int constPricingAir = 20;
            int constPricingInland = 30;
            int constPricingCustomsClearance = 40;
            //int constPricingGeneral = 50;
            var constCustomsClearanceAgentOperationPartnerTypeID = 8;
            var constShippingLineOperationPartnerTypeID = 9;
            var constAirineOperationPartnerTypeID = 10;
            var constTruckerOperationPartnerTypeID = 11;

            checkException = objCPricing.GetListPaging(99999, 1, ("WHERE ID IN (" + pSelectedPricingIDs + ")"), "ID", out _RowCount);
            foreach (var rowPricing in objCPricing.lstCVarPricing)
            {
                CvwCurrencies objCvwCurrencies = new CvwCurrencies();
                objCvwCurrencies.GetList("WHERE ID=" + rowPricing.CurrencyID);
                checkException = objCPricingCharge.GetListPaging(99999, 1, ("WHERE (ISNULL(CostPrice,0)<>0 OR ISNULL(SalePrice,0)<>0) AND ChargeTypeID IN (SELECT ChargeTypeID FROM PricingSettings WHERE PricingTypeID=" + rowPricing.PricingTypeID + ") AND PricingID = " + rowPricing.ID), "ID", out _RowCount);
                foreach (var rowPricingCharge in objCPricingCharge.lstCVarPricingCharge)
                {
                    decimal decSalePrice = 0;
                    decimal decCostPrice = rowPricingCharge.CostPrice;
                    if (pProfitType != 30/*No Sale Price*/)
                        decSalePrice = pProfitType == 10
                                                ? (decCostPrice * pProfitAmount / 100 + decCostPrice) //percentge
                                                : (decCostPrice + pProfitAmount);
                    CVarQuotationCharges objCVarQuotationCharges = new CVarQuotationCharges();
                    objCVarQuotationCharges.QuotationRouteID = pQuotationRouteID;
                    objCVarQuotationCharges.ChargeTypeID = rowPricingCharge.ChargeTypeID;
                    objCVarQuotationCharges.MeasurementID = 3; //fixed

                    objCVarQuotationCharges.CostQuantity = 1;
                    objCVarQuotationCharges.CostPrice = decCostPrice;
                    objCVarQuotationCharges.CostAmount = decCostPrice;
                    objCVarQuotationCharges.CostCurrencyID = rowPricing.CurrencyID;
                    objCVarQuotationCharges.CostExchangeRate = objCvwCurrencies.lstCVarvwCurrencies[0].CurrentExchangeRate;

                    objCVarQuotationCharges.SaleQuantity = 1;
                    objCVarQuotationCharges.SalePrice = objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "GBL" ? 0 : decSalePrice; //coz sale is not entered here
                    objCVarQuotationCharges.SaleAmount = objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "GBL" ? 0 : decSalePrice;
                    objCVarQuotationCharges.SaleCurrencyID = rowPricing.CurrencyID;
                    objCVarQuotationCharges.POrC = 0;
                    objCVarQuotationCharges.SaleExchangeRate = objCvwCurrencies.lstCVarvwCurrencies[0].CurrentExchangeRate;

                    if (rowPricing.PricingTypeID == constPricingAir)
                    {
                        objCVarQuotationCharges.OperationPartnerTypeID = constAirineOperationPartnerTypeID;
                        objCVarQuotationCharges.AirlineID = rowPricing.AirlineID;
                    }
                    else if (rowPricing.PricingTypeID == constPricingOcean)
                    {
                        objCVarQuotationCharges.OperationPartnerTypeID = constShippingLineOperationPartnerTypeID;
                        objCVarQuotationCharges.ShippingLineID = rowPricing.ShippingLineID;
                    }
                    else if (rowPricing.PricingTypeID == constPricingInland)
                    {
                        objCVarQuotationCharges.OperationPartnerTypeID = constTruckerOperationPartnerTypeID;
                        objCVarQuotationCharges.TruckerID = rowPricing.TruckerID;
                    }
                    else if (rowPricing.PricingTypeID == constPricingCustomsClearance)
                    {
                        objCVarQuotationCharges.OperationPartnerTypeID = constCustomsClearanceAgentOperationPartnerTypeID;
                        objCVarQuotationCharges.CustomsClearanceAgentID = rowPricing.CCAID;
                    }

                    objCVarQuotationCharges.ContainerTypeID = rowPricing.EquipmentID;
                    objCVarQuotationCharges.PricingID = rowPricing.ID;
                    objCVarQuotationCharges.Notes = "0";

                    objCVarQuotationCharges.CreatorUserID = objCVarQuotationCharges.ModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarQuotationCharges.CreationDate = objCVarQuotationCharges.ModificationDate = DateTime.Now;

                    objCQuotationCharges.lstCVarQuotationCharges.Add(objCVarQuotationCharges);
                    checkException = objCQuotationCharges.SaveMethod(objCQuotationCharges.lstCVarQuotationCharges);
                }
            }
            checkException = objCvwQuotationCharges.GetListPaging(99999, 1, "WHERE QuotationRouteID=" + pQuotationRouteID, "ID", out _RowCount);
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                    serializer.Serialize(objCvwQuotationCharges.lstCVarvwQuotationCharges)
            };
        }
        
        #endregion Pricing

        public Int32 GetPartnerID(Int32 pCheckedPartnerTypeID, Int32 pReceivedPartnerTypeID, Int32 pPartnerID)
        {
            if (pCheckedPartnerTypeID == pReceivedPartnerTypeID)
                return pPartnerID;
            else
                return 0;

        }
    }
    public class UpdateListParamteres
    {
        public Int64 pQuotationRouteID { get; set; }
        public string pSelectedIDsToUpdate { get; set; }
        public string pMeasurementIDList { get; set; }
        public string pQuantityList { get; set; }
        public string pCostPriceList { get; set; }
        public string pCostAmountList { get; set; }
        public string pSalePriceList { get; set; }
        public string pSaleAmountList { get; set; }
        public string pCostCurrencyList { get; set; }
        public string pCostExchangeRateList { get; set; }
        public string pSaleCurrencyList { get; set; }
        public string pPOrCList { get; set; }
        public string pSaleExchangeRateList { get; set; }
        public string pPackageTypeIDList { get; set; }
        public string pContainerTypeIDList { get; set; }
    }
}
