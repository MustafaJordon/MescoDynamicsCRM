using Forwarding.MvcApp.Entities.Operations;
using System;
using System.Web.Http;
using System.Web.Script.Serialization;
using Forwarding.MvcApp.Models.ContainerFreightStation.Transactions;
using WebMatrix.WebData;
using Forwarding.MvcApp.Models.Administration.Settings.Generated;
using Forwarding.MvcApp.Models.Operations.Operations.Generated;
using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;
using Forwarding.MvcApp.Models.ContainerFreightStation.Tariff;
using Forwarding.MvcApp.Models.Administration.Security.Generated;

using System.Linq;
using Forwarding.MvcApp.Models.MasterData.Partners.Generated;
using Forwarding.MvcApp.Models.MasterData.BanksAccountsAndTreasuries.Generated;
using Forwarding.MvcApp.Models.Customized;


namespace Forwarding.MvcApp.Controllers.ContainerFreightStation.Transactions
{
    public class WH_CFS_InvoicesController : ApiController
    {
        [HttpGet, HttpPost]
        public Object[] WH_CFS_Invoices_LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned(Int32 pPageNumber, Int32 pPageSize, string pWhereClause, string pOrderBy)
        {
            bool _result = false;
            Exception checkException = null;
            int _RowCount = 0;

            CvwWH_CFS_GateInInventory objCvwWH_CFS_GateInInventory = new CvwWH_CFS_GateInInventory();

            pWhereClause = (pWhereClause == null ? "" : pWhereClause.Trim().ToUpper());

            checkException = objCvwWH_CFS_GateInInventory.GetListPaging(pPageSize, pPageNumber, pWhereClause, pOrderBy, out _RowCount);

            if (checkException == null)
            {
                _result = true;
            }
            return new Object[] {
                _result
                , _RowCount
                ,new JavaScriptSerializer().Serialize(objCvwWH_CFS_GateInInventory.lstCVarvwWH_CFS_GateInInventory) //0
                };
        }

        [HttpGet, HttpPost]
        public object[] WH_CFS_Invoices_LoadItem(string pInventoryID)
        {
            bool _result = false;
            Exception checkException = null;
            Int32 _RowCount = 0;

            CvwWH_CFS_GateInInventory objCvwWH_CFS_GateInInventory = new CvwWH_CFS_GateInInventory();

            CWH_CSL_Tariff objCWH_CSL_Tariff = new CWH_CSL_Tariff();

            CSecUserCustomizedTabs objCSecUserCustomizedTabs = new CSecUserCustomizedTabs();

            // getting operation receivebales user preivilges
            objCSecUserCustomizedTabs.GetList(" Where SecCustomizedTabID = 60 and UserID = " + WebSecurity.CurrentUserId.ToString());

            checkException = objCvwWH_CFS_GateInInventory.GetList("WHERE InventoryID = " + pInventoryID);

            if (checkException == null)
            {
                _result = true;
                _RowCount = objCvwWH_CFS_GateInInventory.lstCVarvwWH_CFS_GateInInventory.Count;

                objCWH_CSL_Tariff.GetList(" Where IsDefault = 1 and WH_WarehouseID = " + objCvwWH_CFS_GateInInventory.lstCVarvwWH_CFS_GateInInventory[0].WarehouseID);

            }

            var serializer = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            return new object[] {
                _result
                ,serializer.Serialize(_RowCount == 0 ? null :objCvwWH_CFS_GateInInventory.lstCVarvwWH_CFS_GateInInventory[0]) //data[1]
                ,(objCWH_CSL_Tariff.lstCVarWH_CSL_Tariff.Count == 0 ? 0 :objCWH_CSL_Tariff.lstCVarWH_CSL_Tariff[0].InvoiceTypeID) //data[2]
                ,objCSecUserCustomizedTabs.lstCVarSecUserCustomizedTabs.Count > 0 ? serializer.Serialize(objCSecUserCustomizedTabs.lstCVarSecUserCustomizedTabs):null

            };
        }


        [HttpGet, HttpPost]
        public object[] WH_CFS_Invoices_Update(string pInventoryID, string pStorageEndDate, string pKalmarOnCount, string pKalmarOffCount)
        {
            bool _result = false;
            string UpdateStr = "";

            CWH_Inventory objCWH_Inventory = new CWH_Inventory();

            UpdateStr = " UpdatedBy =" + WebSecurity.CurrentUserId.ToString() + ",UpdatedAt=GetDate()";
            UpdateStr += " ,StorageEndDate = '" + pStorageEndDate + "'";
            UpdateStr += " ,KalmarOnCount = " + pKalmarOnCount;
            UpdateStr += " ,KalmarOffCount = " + pKalmarOffCount;
            UpdateStr += " where ID = " + pInventoryID;

            Exception checkException = objCWH_Inventory.UpdateList(UpdateStr);

            if (checkException == null)
            {
                _result = true;
            }

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                _result //pData[0]
            };
        }

        [HttpGet, HttpPost] //pID : is the InvoiceID
        public object[] Report_Invoice(string pWhereClause, Int64 pID, int pInvoiceReportTypeID, bool pIsPrintWithoutValidation, Int32 pBankTemplateID, bool pIsOriginalChassisItems) 
        {
            bool RecordsExist = true;
            string strExportedFileName = "";
            string MissingMandatoryFields = "";//MissingMandatoryFields is the message returned
            var MainRoutingTypeID = 30;
            var TruckingOrderRoutingTypeID = 60;
            var CustomsClearanceRoutingTypeID = 70;

            Int64 pELIInvoicePrefix = 0;
            CvwWH_CFS_Invoices objCvwWH_CFS_Invoices = new CvwWH_CFS_Invoices();
            CvwOperationPartners objCvwOperationPartners = new CvwOperationPartners();
            CCustomers objCCustomers = new CCustomers();
            CAgents objCAgents = new CAgents();
            Int32 _RowCount = 0;
            Int32 _tempRowCount = 0;
            CDefaults objCDefaults = new CDefaults();

            objCDefaults.GetListPaging(999999, 1, "WHERE 1=1", "ID", out _tempRowCount);
            objCvwWH_CFS_Invoices.GetListPaging(99999, 1, pWhereClause, "ID DESC", out _RowCount);
            objCvwOperationPartners.GetListPaging(1, 1, "WHERE ID=" + objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].OperationPartnerID, "ID", out _RowCount);
            string _InvoiceTypeCode = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].InvoiceTypeCode;
           
            #region Recalculate Total Sum
            CInvoices objCInvoices = new CInvoices();

            CReceivables objCReceivables = new CReceivables();
            Exception checkException = null;
            string pUpdateClause = "";

            #region update exchange rate for receivables at server side to fix any connection problem
            pUpdateClause = " ExchangeRate = (SELECT ExchangeRate FROM Invoices WHERE ID=" + pID.ToString() + ")";
            #region ensure receivables are correct
            pUpdateClause += " , AmountWithoutVAT = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2)" + " \n";
            pUpdateClause += " , TaxPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID)" + " \n";
            pUpdateClause += " , TaxAmount = ROUND( (ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID),2)" + " \n";
            pUpdateClause += " , DiscountPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID)" + " \n";
            pUpdateClause += " , DiscountAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),2)" + " \n";
            pUpdateClause += " , SaleAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2)" + " \n"
                          + " + (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID),0), 2))  " + " \n"
                          + " - (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),0), 2))" + " \n";
            #endregion ensure receivables are correct
            pUpdateClause += " WHERE " + (_InvoiceTypeCode == "DRAFT" ? "DraftInvoiceID=" : "InvoiceID=") + pID.ToString();
            checkException = objCReceivables.UpdateList(pUpdateClause);
            #endregion update exchange rate for receivables at server side to fix any connection problem
            
            #region Update Invoice totals at server side to fix any connection problem
            //SET AmountWithoutVAT
            pUpdateClause = " AmountWithoutVAT = ROUND((SELECT SUM(ISNULL(SalePrice,0) * ISNULL(Quantity,1)) - ISNULL(FixedDiscount,0) FROM Receivables WHERE " + (objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].InvoiceTypeCode == "DRAFT" ? "DraftInvoiceID" : "InvoiceID") + " = " + pID + "),2)";
            pUpdateClause += " WHERE ID = " + pID;
            checkException = objCInvoices.UpdateList(pUpdateClause);
            //SET Tax, Discount & Total Amount after setting the AmountWithVAT
            pUpdateClause = " TaxAmount = ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(TaxPercentage, 0) / 100),2)";
            pUpdateClause += " , DiscountAmount = ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2)";
            if (objCDefaults.lstCVarDefaults[0].IsTaxOnItems)
                pUpdateClause += " , Amount = - ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2) + ROUND((SELECT SUM(ISNULL(SaleAmount,0)) - ISNULL(FixedDiscount,0) FROM Receivables WHERE " + (objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].InvoiceTypeCode == "DRAFT" ? "DraftInvoiceID" : "InvoiceID") + " = " + pID + "),2)";
            else
                pUpdateClause += " , Amount = ROUND((ISNULL(AmountWithoutVAT, 0) + ROUND( (ISNULL(AmountWithoutVAT, 0) * ISNULL(TaxPercentage, 0) / 100),2) - ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2)),2)";
            pUpdateClause += " WHERE ID = " + pID;
            checkException = objCInvoices.UpdateList(pUpdateClause);
            #endregion Update Invoice totals at server side to fix any connection problem

            #endregion Recalculate Total Sum
            
            #region Get Client Data
            if (objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].GeneralPartnerTypeCode == "CUSTOMERS")
                objCCustomers.GetListPaging(999999, 1, "WHERE ID=" + objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].PartnerID, "ID", out _tempRowCount);
            else if (objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].GeneralPartnerTypeCode == "AGENTS")
                objCAgents.GetListPaging(999999, 1, "WHERE ID=" + objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].PartnerID, "ID", out _tempRowCount);
            #endregion Get Client Data
            CvwWH_CFS_Invoices objCvwFirstInvoiceInOperation_ELI = new CvwWH_CFS_Invoices(); //get the first serial/operation of type INVOICE Elite

            CvwReceivables objCvwReceivables = new CvwReceivables();
            if (pIsOriginalChassisItems)
                objCvwReceivables.GetListPaging(99999, 1, " WHERE InvoiceID_3PL=" + pID, "ViewOrder, ChargeTypeName", out _RowCount);
            else
                objCvwReceivables.GetListPaging(99999, 1, " WHERE " + (_InvoiceTypeCode == "DRAFT" ? "DraftInvoiceID=" : "InvoiceID=") + pID, "ViewOrder, ChargeTypeName", out _RowCount);

            CvwRoutings objCFleetOrder = new CvwRoutings();
            if (objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].IsFleet
                && objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].CancelledInvoiceID > 0)
                objCFleetOrder.GetListPaging(999999, 1, ("WHERE InvoiceID=" + objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].CancelledInvoiceID), "ID", out _RowCount);
            else
                objCFleetOrder.GetListPaging(999999, 1, ("WHERE InvoiceID=" + objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].ID), "ID", out _RowCount);
            CvwRoutings objCTruckingOrder = new CvwRoutings();
            objCTruckingOrder.GetListPaging(9999, 1, ("WHERE OperationID=" + objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].OperationID + " AND IsFleet=0 AND RoutingTypeID=" + TruckingOrderRoutingTypeID), "ID", out _RowCount);
            CvwRoutings objCCustomsClearance = new CvwRoutings();
            //objCCustomsClearance.GetListPaging(9999, 1, ("WHERE OperationID=" + objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].OperationID + " OR MasterOperationID=" + objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].OperationID + " AND RoutingTypeID=" + CustomsClearanceRoutingTypeID), "ID", out _RowCount);
            objCCustomsClearance.GetListPaging(9999, 1, ("WHERE OperationID=" + objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].OperationID + " AND RoutingTypeID=" + CustomsClearanceRoutingTypeID), "ID", out _RowCount);
            CvwRoutings objCvwRoutings = new CvwRoutings();
            objCvwRoutings.GetListPaging(99999, 1, " WHERE OperationID = " + objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].OperationID.ToString() + " AND RoutingTypeID = " + MainRoutingTypeID.ToString(), "ID", out _RowCount);
            var pDeliveryOrderNumber = objCvwRoutings.lstCVarvwRoutings.Count > 0 ? objCvwRoutings.lstCVarvwRoutings[0].DeliveryOrderNumber : "";

            COperationContainersAndPackages objCOperationContainersAndPackages = new COperationContainersAndPackages();
            objCOperationContainersAndPackages.GetListPaging(99999, 1, " WHERE TankOrFlexiNumber IS NOT NULL AND OperationID = " + objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].OperationID, "ID", out _RowCount);
            var pTankOrFlexiNumbers = "";
            for (int i = 0; i < objCOperationContainersAndPackages.lstCVarOperationContainersAndPackages.Count; i++)
                pTankOrFlexiNumbers += pTankOrFlexiNumbers == ""
                    ? objCOperationContainersAndPackages.lstCVarOperationContainersAndPackages[i].TankOrFlexiNumber
                    : ("," + objCOperationContainersAndPackages.lstCVarOperationContainersAndPackages[i].TankOrFlexiNumber);

            var pBankDetailsTemplate = "";
            CBankTemplate objCBankTemplate = new CBankTemplate();
            if (pBankTemplateID != 0)
            {
                objCBankTemplate.GetList("WHERE ID=" + pBankTemplateID.ToString());
                pBankDetailsTemplate = objCBankTemplate.lstCVarBankTemplate[0].Subject == "0" ? "" : objCBankTemplate.lstCVarBankTemplate[0].Subject;
            }
            //i am sure i have just 1 row coz the where clause used the ID
            if (objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices.Count > 0)
            {
                RecordsExist = false;
                int constBLTypeMaster = 3;
                int constOceanTransportType = 1;
                int constInlandTransportType = 3;

                #region setting the header invoice parameters
                var pInvoiceDate = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].InvoiceDate;
                var pInvoiceDueDate = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].DueDate;
                var pPaymentTerm = (objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].PaymentTermCode == "0" ? "" : objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].PaymentTermCode);
                var pInvoiceNumber = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].InvoiceNumber;
                var pPartnerName = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].PartnerName;
                var pStreetLine1 = (objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].StreetLine1 == "0" ? "N/A" : objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].StreetLine1);
                var pStreetLine2 = (objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].StreetLine2 == "0" ? "" : objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].StreetLine2);
                var pCityName = (objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].CityName == "0" ? "" : objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].CityName);
                var pCountryName = (objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].CountryName == "0" ? "" : objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].CountryName);
                var pOperationCode = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].OperationCode;
                var pLineName = (objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].LineName == "0" ? "" : objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].LineName);
                var pVesselName = (objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].VesselName == "0" ? "" : objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].VesselName);
                var pVoyageOrTruckNumber = (objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].VoyageOrTruckNumber == "0" ? "" : objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].VoyageOrTruckNumber);
                var pPOLCode = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].POLCode;
                var pPODCode = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].PODCode;
                var pMasterBL = (objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].MasterBL == "0" ? "" : objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].MasterBL);
                var pHouseNumber = (objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].HouseNumber == "0" ? "" : objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].HouseNumber);
                var pMasterOperationCode = (objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].HouseNumber == "0" ? "" : objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].MasterOperationCode);
                var pOperationID = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].OperationID;
                var pPOLName = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].POLName;
                var pPODName = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].PODName;
                var pTaxTypeName = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].TaxTypeName;
                var pTaxAmount = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].TaxAmount;
                var pDiscountTypeName = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].DiscountTypeName;
                var pDiscountAmount = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].DiscountAmount;
                var pVATNumber = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].VATNumber == "0" ? "N/A" : objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].VATNumber;
                var pLeftSignature = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].LeftSignature;
                var pMiddleSignature = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].MiddleSignature;
                var pRightSignature = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].RightSignature;
                var pGRT = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].GRT;
                var pDWT = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].DWT;
                var pNRT = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].NRT;
                var pLOA = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].LOA;
                var pInvoiceTypeCode = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].InvoiceTypeCode;

                var pContainerNumbers = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].HouseBillContainers ;
                var pEntryDate = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].EntryDate;
                var pStorageEndDate = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].StorageEndDate;
                var pStorageDays = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].StorageDays;
                var pStorageRate = objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].StorageRate;
                #endregion

                CvwAddresses objCvwAddresses = new CvwAddresses();
                objCvwAddresses.GetList("WHERE ID = " + objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].AddressID.ToString());
                var pClientStreetLine1 = ""; var pClientStreetLine2 = "";
                var pClientCityName = ""; var pClientCountryName = "";
                if (objCvwAddresses.lstCVarvwAddresses.Count > 0)
                {
                    pClientStreetLine1 = objCvwAddresses.lstCVarvwAddresses[0].StreetLine1 == "0" ? "" : objCvwAddresses.lstCVarvwAddresses[0].StreetLine1;
                    pClientStreetLine2 = objCvwAddresses.lstCVarvwAddresses[0].StreetLine2 == "0" ? "" : objCvwAddresses.lstCVarvwAddresses[0].StreetLine2;
                    pClientCityName = objCvwAddresses.lstCVarvwAddresses[0].CityName == "0" ? "" : objCvwAddresses.lstCVarvwAddresses[0].CityName;
                    pClientCountryName = objCvwAddresses.lstCVarvwAddresses[0].CountryName == "0" ? "" : objCvwAddresses.lstCVarvwAddresses[0].CountryName;
                }
                CvwOperations objCvwOperations = new CvwOperations();
                objCvwOperations.GetList(" WHERE ID = " + pOperationID);
                CvwOperations objCvwMasterOperationHeader = new CvwOperations();
                if (objCvwOperations.lstCVarvwOperations[0].MasterOperationID != 0)
                    objCvwMasterOperationHeader.GetList(" WHERE ID = " + objCvwOperations.lstCVarvwOperations[0].MasterOperationID);

                objCvwFirstInvoiceInOperation_ELI.GetListPaging(1, 1, "WHERE IsDeleted=0 AND (OperationID=" + objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].OperationID + " OR OperationID=" + objCvwOperations.lstCVarvwOperations[0].MasterOperationID + ")", "ID", out _RowCount);
                if (objCvwFirstInvoiceInOperation_ELI.lstCVarvwWH_CFS_Invoices.Count > 0)
                    pELIInvoicePrefix = objCvwFirstInvoiceInOperation_ELI.lstCVarvwWH_CFS_Invoices[0].InvoiceNumber;

                var pContainerTypes = objCvwOperations.lstCVarvwOperations[0].ContainerTypes;
                // get container number from invoices veiw
                //var pContainerNumbers = objCvwOperations.lstCVarvwOperations[0].ContainerNumbers == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].ContainerNumbers;
                var pPackageTypes = objCvwOperations.lstCVarvwOperations[0].PackageTypes;
                var pCustomerReference = objCvwOperations.lstCVarvwOperations[0].CustomerReference;
                var pHouseBLs = objCvwOperations.lstCVarvwOperations[0].HouseBLs;//used incase the invoice is created for the master operation and holds all the HBL Nos on that operation
                if (objCvwOperations.lstCVarvwOperations[0].TransportType == 2)
                {
                    CvwOperations objCvwMasterOperation = new CvwOperations();
                    objCvwMasterOperation.GetList(" WHERE MasterOperationID = " + pOperationID);
                    if (objCvwMasterOperation.lstCVarvwOperations.Count > 0)
                        pHouseBLs = objCvwMasterOperation.lstCVarvwOperations[0].MAWBSuffix;
                }

                var pCBM = objCvwOperations.lstCVarvwOperations[0].VolumeSum;
                var pGrossWeightSum = objCvwOperations.lstCVarvwOperations[0].GrossWeightSum;
                var pShipmentTypeCode = objCvwOperations.lstCVarvwOperations[0].ShipmentTypeCode == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].ShipmentTypeCode;
                var pIncotermName = objCvwOperations.lstCVarvwOperations[0].IncotermName == "0" ? "" : objCvwOperations.lstCVarvwOperations[0].IncotermName;
                var pShipperName = objCvwOperations.lstCVarvwOperations[0].ShipperName == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].ShipperName;
                var pConsigneeName = objCvwOperations.lstCVarvwOperations[0].ConsigneeName == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].ConsigneeName;
                var pCommodityName = (objCvwOperations.lstCVarvwOperations[0].CommodityName == "0" ? "" : objCvwOperations.lstCVarvwOperations[0].CommodityName);
                var pPOrC = objCvwOperations.lstCVarvwOperations[0].POrCCode.ToString() == "0" ? "" : objCvwOperations.lstCVarvwOperations[0].POrCCode;
                var pMoveType = objCvwOperations.lstCVarvwOperations[0].MoveTypeName.ToString() == "0" ? "" : objCvwOperations.lstCVarvwOperations[0].MoveTypeName;
                var pBLType = objCvwOperations.lstCVarvwOperations[0].BLType;
                var pTransportType = objCvwOperations.lstCVarvwOperations[0].TransportType;
                var pActualDeparture = objCvwOperations.lstCVarvwOperations[0].ActualDeparture;
                var pETA = objCvwOperations.lstCVarvwOperations[0].ETAPOLDate;
                var pETD = objCvwOperations.lstCVarvwOperations[0].ExpectedDeparture;
                var pETAPOD = objCvwOperations.lstCVarvwOperations[0].ExpectedArrival;
                var pSalesman = objCvwOperations.lstCVarvwOperations[0].Salesman;
                var pDescriptionOfGoods = objCvwOperations.lstCVarvwOperations[0].Notes == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].Notes;
                var pVGM = objCvwOperations.lstCVarvwOperations[0].VGMSum;
                var pNumberOfPackages = objCvwOperations.lstCVarvwOperations[0].PackageTypesOnContainersTotals == "0"
                                        ? objCvwOperations.lstCVarvwOperations[0].PackageTypes
                                        : objCvwOperations.lstCVarvwOperations[0].PackageTypesOnContainersTotals;

                CvwDefaults objCvwDefaults = new CvwDefaults();
                objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa
                var pTaxNumber = objCvwDefaults.lstCVarvwDefaults[0].TaxNumber;
                var pAccountName = objCvwDefaults.lstCVarvwDefaults[0].AccountName;
                var pBankName = objCvwDefaults.lstCVarvwDefaults[0].BankName;
                var pBankAddress = objCvwDefaults.lstCVarvwDefaults[0].BankAddress;
                var pSwiftCode = objCvwDefaults.lstCVarvwDefaults[0].SwiftCode;
                var pAccountNumber = objCvwDefaults.lstCVarvwDefaults[0].AccountNumber;
                var pAddressLine1 = objCvwDefaults.lstCVarvwDefaults[0].AddressLine1 == "0" ? "" : objCvwDefaults.lstCVarvwDefaults[0].AddressLine1;
                var pAddressLine2 = objCvwDefaults.lstCVarvwDefaults[0].AddressLine2 == "0" ? "" : objCvwDefaults.lstCVarvwDefaults[0].AddressLine2;
                var pAddressLine3 = objCvwDefaults.lstCVarvwDefaults[0].AddressLine3 == "0" ? "" : objCvwDefaults.lstCVarvwDefaults[0].AddressLine3;
                var pPhones = objCvwDefaults.lstCVarvwDefaults[0].Phones == "0" ? "" : objCvwDefaults.lstCVarvwDefaults[0].Phones;
                var pFaxes = objCvwDefaults.lstCVarvwDefaults[0].Faxes == "0" ? "" : objCvwDefaults.lstCVarvwDefaults[0].Faxes;

                #region Checking mandatoryfields for printing this reports
                if (pMasterBL == "" && pTransportType != constInlandTransportType)
                    MissingMandatoryFields += (MissingMandatoryFields == "" ? "MBL Number" : ", MBL Number");
                if (pContainerTypes == "0" && pPackageTypes == "0")
                    MissingMandatoryFields += (MissingMandatoryFields == "" ? "Cargo" : ", Cargo");
                if (pCommodityName == "")
                    MissingMandatoryFields += (MissingMandatoryFields == "" ? "Commodity" : ", Commodity");
                if (pPOrC == "")
                    MissingMandatoryFields += (MissingMandatoryFields == "" ? "Freight type (P/C)" : ", Freight type (P/C)");
                if (pIncotermName == "")
                    MissingMandatoryFields += (MissingMandatoryFields == "" ? "Incoterm" : ", Incoterm");
                if (pGrossWeightSum == 0)
                    MissingMandatoryFields += (MissingMandatoryFields == "" ? "Gross weight" : ", Gross weight");
                if (pMoveType == "" && pBLType != constBLTypeMaster)
                    MissingMandatoryFields += (MissingMandatoryFields == "" ? "Service scope" : ", Service scope");
                if (pVesselName == "" && pTransportType == constOceanTransportType)
                    MissingMandatoryFields += (MissingMandatoryFields == "" ? "Vessel" : ", Vessel");
                if (pVoyageOrTruckNumber == "" && pTransportType == constOceanTransportType)
                    MissingMandatoryFields += (MissingMandatoryFields == "" ? "Voyage number" : ", Voyage number");
                if (pActualDeparture.Equals(DateTime.Parse("01-01-1900")) && pTransportType != constInlandTransportType)
                    MissingMandatoryFields += (MissingMandatoryFields == "" ? "Actual departure date" : ", Actual departure date");

                if (MissingMandatoryFields == "" || pIsPrintWithoutValidation)
                    RecordsExist = true;
                else
                {
                    RecordsExist = false;
                    MissingMandatoryFields = "You are missing " + MissingMandatoryFields + ".";
                }
                #endregion Checking mandatoryfields for printing this reports

                var pReceivableList = objCvwReceivables.lstCVarvwReceivables
                .GroupBy(g => new { g.CurrencyCode, g.ChargeTypeCode, g.Quantity, g.Notes })
                .Select(s => new
                {
                    ChargeTypeName = objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "SEF" ? s.First().ChargeTypeName : s.First().ChargeTypeCode
                    ,
                    Notes = s.First().Notes
                    ,
                    Quantity = s.First().Quantity
                    ,
                    SalePrice = s.Sum(i => i.SalePrice)
                    ,
                    AmountWithoutVAT = s.Sum(i => i.AmountWithoutVAT)
                    ,
                    TaxAmount = s.Sum(i => i.TaxAmount)
                    ,
                    DiscountAmount = s.Sum(i => i.DiscountAmount)
                    ,
                    SaleAmount = s.Sum(i => i.SaleAmount)
                    ,
                    CurrencyCode = s.First().CurrencyCode
                })
                .Distinct()
                //.OrderBy(o => o.Name)
                .ToList();

                var pFleetOrdersList = objCFleetOrder.lstCVarvwRoutings
                .GroupBy(g => new { g.POLName, g.PODName, g.DivisionID, g.Sale })
                .Select(s => new
                {
                    ChargeTypeName = s.First().POLName + " --> " + s.First().PODName
                    ,
                    DivisionName = s.First().DivisionName
                    ,
                    Notes = ""//s.First().Notes
                    ,
                    Quantity = s.Count()
                    ,
                    SalePrice = s.First().Sale//s.Sum(i => i.Sale) / s.Count()
                    ,
                    AmountWithoutVAT = s.Sum(i => i.Sale)
                    ,
                    TaxAmount = pReceivableList.Count > 0 ? ((s.Sum(i => i.Sale) * objCvwReceivables.lstCVarvwReceivables[0].TaxPercentage) / 100) : 0
                    ,
                    DiscountAmount = 0 //s.Sum(i => i.DiscountAmount)
                    ,
                    SaleAmount = 0 //Calculate in printing as AmountWithoutVAT + TaxAmount
                    //,
                    //CurrencyCode = s.First().CurrencyCode
                })
                //.Distinct()
                //.OrderBy(o => o.Name)
                .ToList();

                //Image QR
                CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
                object QRImage = null;

                if (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "WAV" || objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "SEC")
                    QRImage = objCCustomizedDBCall.GetImageByInvoiceID(objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].ID.ToString());


                ////i am sending data from receivables view and not invoices
                //strExportedFileName = ExportReport(objCvwReceivables.lstCVarvwReceivables, pReportName, pReportNameWithoutExtension, pInvoiceReportTypeID
                //    , new Object[] { pInvoiceDate, pInvoiceDueDate, pPaymentTerm, pInvoiceNumber, pPartnerName, pStreetLine1
                //        , pStreetLine2, pCityName, pCountryName, pOperationCode, pLineName, pVesselName, pVoyageOrTruckNumber
                //        , pPOLCode, pPODCode, pMasterBL, pHouseNumber }); //ParameterFields

                var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
                return new object[] {
                    RecordsExist, strExportedFileName
                    ,objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "ACS" || objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "SEF" || objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "NIS"
                        ? serializer.Serialize(pReceivableList)
                        : (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "SWI"
                            ? serializer.Serialize(objCvwReceivables.lstCVarvwReceivables.OrderBy(o => o.ID))
                            : serializer.Serialize(objCvwReceivables.lstCVarvwReceivables.OrderBy(o => o.ViewOrder))
                          )
                    , pContainerTypes, pHouseNumber, pMasterOperationCode, pTaxNumber
                    , Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pInvoiceDate), pInvoiceNumber, pAccountName, pBankName,pBankAddress,pSwiftCode
                    , pAccountNumber, pMasterBL, pPackageTypes, pCustomerReference, MissingMandatoryFields
                    , Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pInvoiceDueDate), pPOLName, pPODName, pHouseBLs
                    , pTaxTypeName, pTaxAmount, pDiscountTypeName, pDiscountAmount, pAddressLine1, pAddressLine2, pAddressLine3
                    , pPhones, pFaxes, pCBM, pGrossWeightSum, pClientStreetLine1, pClientStreetLine2
                    , pClientCityName, pClientCountryName, pShipmentTypeCode, pIncotermName
                    , pShipperName, pConsigneeName, pVesselName, Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pETA)
                    , Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pETD), pContainerNumbers, pSalesman, pVATNumber
                    , pDescriptionOfGoods, pVGM, pNumberOfPackages, pETAPOD.ToShortDateString(), pLeftSignature, pMiddleSignature, pRightSignature
                    , pGRT, pDWT, pNRT, pLOA, pInvoiceTypeCode, pBankDetailsTemplate
                    , new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations[0])
                    , new JavaScriptSerializer().Serialize(objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0]), pDeliveryOrderNumber
                    , objCvwMasterOperationHeader.lstCVarvwOperations.Count > 0 ? new JavaScriptSerializer().Serialize(objCvwMasterOperationHeader.lstCVarvwOperations[0]) : null
                    , new JavaScriptSerializer().Serialize(objCvwDefaults.lstCVarvwDefaults[0])
                    , objCvwRoutings.lstCVarvwRoutings.Count > 0 ? new JavaScriptSerializer().Serialize(objCvwRoutings.lstCVarvwRoutings[0]) : null
                    , pELIInvoicePrefix
                    , objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].GeneralPartnerTypeCode == "CUSTOMERS" ? new JavaScriptSerializer().Serialize(objCCustomers.lstCVarCustomers[0])
                        : (objCvwWH_CFS_Invoices.lstCVarvwWH_CFS_Invoices[0].GeneralPartnerTypeCode == "AGENTS" ? new JavaScriptSerializer().Serialize(objCAgents.lstCVarAgents[0])
                        : null)
                    , pTankOrFlexiNumbers
                    , new JavaScriptSerializer().Serialize(objCTruckingOrder.lstCVarvwRoutings)
                    , serializer.Serialize(pFleetOrdersList)
                    , objCCustomsClearance.lstCVarvwRoutings.Count == 0 ? null : new JavaScriptSerializer().Serialize(objCCustomsClearance.lstCVarvwRoutings[0])
                    , objCvwOperationPartners.lstCVarvwOperationPartners.Count == 0 ? null : new JavaScriptSerializer().Serialize(objCvwOperationPartners.lstCVarvwOperationPartners[0])
                    , QRImage //QRImage
                    ,Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pEntryDate)
                    ,Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pStorageEndDate)  
                    ,pStorageDays ,pStorageRate
                };
            }
            else
            {
                RecordsExist = false;
                MissingMandatoryFields = "No records are found.";
                return new object[] { RecordsExist };
            }
        }

        #region R E C E I V A B L E S  F U N C T I O N S


        [HttpGet, HttpPost]
        public bool Receivables_InsertListWithoutValues(Int64 pOperationID, Int64 pOperationContainersAndPackagesID, Int64 pHouseBillID, string pSelectedIDs)
        {
            bool _result = false;
            string pWhereClause = "";
            int _RowCount = 0;
            int _CurrencyID = 0;
            decimal _ExchangeRate = 1;
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

            #region set CurrencyID and ExchangeRate in case of AWB or not

            COperations objCOperations = new COperations();
            objCOperations.GetListPaging(99999, 1, "WHERE ID=" + pOperationID.ToString(), "ID", out _RowCount);

            CvwCurrencyDetails objCvwCurrencyDetails = new CvwCurrencyDetails();
            if (objCOperations.lstCVarOperations[0].IsAWB)
            {
                string _OpenDate = "19000101";
                _OpenDate = objCOperations.lstCVarOperations[0].OpenDate.Year.ToString() + objCOperations.lstCVarOperations[0].OpenDate.Month.ToString().PadLeft(2, '0') + objCOperations.lstCVarOperations[0].OpenDate.Day.ToString().PadLeft(2, '0');
                objCvwCurrencyDetails.GetList("WHERE ID=" + objCOperations.lstCVarOperations[0].CurrencyID
                    + " AND '" + _OpenDate + "' >= DateAdd(hh, 0, DateDiff(dd, 0, FromDate)) "
                    + " AND '" + _OpenDate + "' <= DateAdd(minute,59,DateAdd(hh, 23, DateDiff(dd, 0, ToDate))) "
                    );
                if (objCvwCurrencyDetails.lstCVarvwCurrencyDetails.Count > 0)
                {
                    _CurrencyID = objCvwCurrencyDetails.lstCVarvwCurrencyDetails[0].ID;
                    _ExchangeRate = objCvwCurrencyDetails.lstCVarvwCurrencyDetails[0].ExchangeRate;
                }
                else //Exchange Rate is not entered for the operation currency open date so put the default
                {
                    _CurrencyID = objCDefaults.lstCVarDefaults[0].CurrencyID;
                    _ExchangeRate = 1;
                }
            }
            else //not AWB
            {
                _CurrencyID = objCDefaults.lstCVarDefaults[0].CurrencyID;
                _ExchangeRate = 1;
            }
            #endregion set CurrencyID and ExchangeRate in case of AWB or not

            //those 2 lines are to get the Charge types from DB
            CChargeTypes objCChargeTypes = new CChargeTypes();
            //objCChargeTypes.GetList(pWhereClause);
            objCChargeTypes.GetListPaging(1500, 1, pWhereClause, "ID", out _RowCount);

            CReceivables objCReceivables = new CReceivables();

            foreach (var rowChargeType in objCChargeTypes.lstCVarChargeTypes)
            {
                CVarReceivables objCVarReceivables = new CVarReceivables();

                if (objCDefaults.lstCVarDefaults[0].IsTaxOnItems)
                    objCVarReceivables.TaxeTypeID = rowChargeType.TaxeTypeID;

                objCVarReceivables.OperationID = pOperationID;
                objCVarReceivables.ChargeTypeID = rowChargeType.ID;
                objCVarReceivables.MeasurementID = rowChargeType.MeasurementID;
                objCVarReceivables.Quantity = 1;
                objCVarReceivables.ExchangeRate = _ExchangeRate;
                objCVarReceivables.CurrencyID = _CurrencyID;
                //objCVarReceivables.GeneratingQRID = pQuotationRouteID;
                objCVarReceivables.HouseBillID = pHouseBillID;
                objCVarReceivables.Notes = rowChargeType.Notes;

                objCVarReceivables.IssueDate = DateTime.Now;
                objCVarReceivables.OperationContainersAndPackagesID = pOperationContainersAndPackagesID;

                objCVarReceivables.PreviousCutOffDate = DateTime.Parse("01/01/1900");
                objCVarReceivables.CutOffDate = DateTime.Parse("01/01/1900");

                objCVarReceivables.CreatorUserID = objCVarReceivables.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarReceivables.ModificationDate = objCVarReceivables.CreationDate = DateTime.Now;

                objCVarReceivables.ReceiptNo = "";
                objCVarReceivables.ReceiptDate = DateTime.Parse("01/01/1900");

                objCReceivables.lstCVarReceivables.Add(objCVarReceivables);
            }
            var checkException = objCReceivables.SaveMethod(objCReceivables.lstCVarReceivables);
            if (checkException == null)
                _result = true;
            return _result;
        }

        [HttpGet, HttpPost]
        public object[] Receivables_LoadWithWhereClause(Int32 pPageNumber, Int32 pPageSize, string pWhereClause)
        {
            CvwReceivables objCvwReceivables = new CvwReceivables();
            //objCvwReceivables.GetList(string.Empty); //GetList() fn loads without paging
            Int32 _RowCount = objCvwReceivables.lstCVarvwReceivables.Count;
            //pSearchKey here is the where clause
            objCvwReceivables.GetListPaging(pPageSize, pPageNumber, pWhereClause, " ChargeTypeName ", out _RowCount);
            return new Object[] { new JavaScriptSerializer().Serialize(objCvwReceivables.lstCVarvwReceivables), _RowCount };
        }

        [HttpGet, HttpPost]
        public bool Receivables_Delete(String pReceivablesIDs, Int64 pOperationID)
        {
            bool _result = true;
            Exception checkException = null;
            CReceivables objCReceivables = new CReceivables();
            CPayables objCPayables = new CPayables();
            COperationLog objCOperationLog = new COperationLog();

            foreach (var currentID in pReceivablesIDs.Split(','))
            {
                checkException = objCReceivables.DeleteList("WHERE ID=" + currentID);
                if (checkException == null)
                {
                    objCOperationLog.UpdateList("UserID=" + WebSecurity.CurrentUserId.ToString() + ", UserName=N'" + WebSecurity.CurrentUserName + "'"
                                                + " WHERE ActionOnRowID IN (" + pReceivablesIDs + ") AND OperationID=" + pOperationID.ToString()
                                                + " AND LogFor = " + 20);

                    objCPayables.UpdateList("ReceivableID=NULL WHERE ReceivableID=" + currentID);
                }
                else
                    _result = false;
            }
            return _result;
        }

        [HttpGet, HttpPost]
        public bool Receivables_Update(Int64 pID, Int32 pOperationID, Int32 pChargeTypeID, int pPOrC, Int32 pSupplierID, Int32 pMeasurementID
                                        , Int32 pContainerTypeID, decimal pQuantity, Decimal pCostPrice, Decimal pCostAmount, Decimal pSalePrice
                                        , Decimal pAmountWithoutVAT, Int32 pTaxTypeID, Decimal pTaxPercentage, Decimal pTaxAmount, Decimal pSaleAmount
                                        , Decimal pExchangeRate, Int32 pCurrencyID, string pNotes, DateTime pIssueDate)
        {
            bool _result = false;
            CVarReceivables objCVarReceivables = new CVarReceivables();
            Exception checkException = null;
            //the next 4 lines to get the CreatorUserID and CreationDate and set them as they were entered before
            CReceivables objCGetCreationInformation = new CReceivables();
            objCGetCreationInformation.GetItem(pID);
            if (objCGetCreationInformation.lstCVarReceivables[0].InvoiceID == 0 && objCGetCreationInformation.lstCVarReceivables[0].DraftInvoiceID == 0)
            {
                objCVarReceivables.CreatorUserID = objCGetCreationInformation.lstCVarReceivables[0].CreatorUserID;
                objCVarReceivables.CreationDate = objCGetCreationInformation.lstCVarReceivables[0].CreationDate;
                objCVarReceivables.GeneratingQRID = objCGetCreationInformation.lstCVarReceivables[0].GeneratingQRID;
                objCVarReceivables.AccNoteID = objCGetCreationInformation.lstCVarReceivables[0].AccNoteID;
                objCVarReceivables.OperationContainersAndPackagesID = objCGetCreationInformation.lstCVarReceivables[0].OperationContainersAndPackagesID;
                objCVarReceivables.HouseBillID = objCGetCreationInformation.lstCVarReceivables[0].HouseBillID;

                objCVarReceivables.ID = pID;

                objCVarReceivables.OperationID = pOperationID;
                objCVarReceivables.ChargeTypeID = pChargeTypeID;
                objCVarReceivables.POrC = pPOrC;
                objCVarReceivables.MeasurementID = pMeasurementID;
                objCVarReceivables.ContainerTypeID = pContainerTypeID;
                objCVarReceivables.SupplierID = pSupplierID;
                objCVarReceivables.Quantity = pQuantity;
                objCVarReceivables.CostPrice = pCostPrice;
                objCVarReceivables.CostAmount = pCostAmount;
                objCVarReceivables.SalePrice = pSalePrice;

                objCVarReceivables.AmountWithoutVAT = pQuantity * pSalePrice;
                objCVarReceivables.TaxeTypeID = pTaxTypeID;
                objCVarReceivables.TaxPercentage = pTaxPercentage;
                objCVarReceivables.TaxAmount = pTaxAmount;

                objCVarReceivables.SaleAmount = pSaleAmount;
                objCVarReceivables.ExchangeRate = pExchangeRate;
                objCVarReceivables.CurrencyID = pCurrencyID;
                objCVarReceivables.Notes = (pNotes == null ? "0" : pNotes);

                objCVarReceivables.IssueDate = pIssueDate;

                objCVarReceivables.PreviousCutOffDate = DateTime.Parse("01/01/1900");
                objCVarReceivables.CutOffDate = DateTime.Parse("01/01/1900");

                objCVarReceivables.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarReceivables.ModificationDate = DateTime.Now;

                objCVarReceivables.ReceiptNo = "";
                objCVarReceivables.ReceiptDate = DateTime.Parse("01/01/1900");

                CReceivables objCReceivables = new CReceivables();
                objCReceivables.lstCVarReceivables.Add(objCVarReceivables);
                //if (objCGetCreationInformation.lstCVarReceivables[0].InvoiceID == 0 && objCGetCreationInformation.lstCVarReceivables[0].AccNoteID == 0)
                checkException = objCReceivables.SaveMethod(objCReceivables.lstCVarReceivables);
            }
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
        public object[] Receivables_CopyReceivable(Int64 pReceivableIDToCopy, Int32 pNumberOfDuplicates)
        {
            CvwReceivables objCvwReceivables = new CvwReceivables();
            CReceivables objCReceivables = new CReceivables();
            Int64 pOperationID = 0;
            Int64 pOperationContainersAndPackagesID = 0;
            Int64 pHouseBillID = 0;
            int _RowCount = 0;
            objCvwReceivables.GetListPaging(999999, 1, "WHERE ID=" + pReceivableIDToCopy, "ID", out _RowCount);

            pOperationID = objCvwReceivables.lstCVarvwReceivables[0].OperationID;
            pOperationContainersAndPackagesID = objCvwReceivables.lstCVarvwReceivables[0].OperationContainersAndPackagesID;
            pHouseBillID = objCvwReceivables.lstCVarvwReceivables[0].HouseBillID;

            for (int i = 0; i < pNumberOfDuplicates; i++)
            {
                CVarReceivables objCVarReceivables = new CVarReceivables();
                objCVarReceivables.OperationID = objCvwReceivables.lstCVarvwReceivables[0].OperationID;
                objCVarReceivables.ChargeTypeID = objCvwReceivables.lstCVarvwReceivables[0].ChargeTypeID;
                objCVarReceivables.POrC = objCvwReceivables.lstCVarvwReceivables[0].POrC;
                objCVarReceivables.SupplierID = objCvwReceivables.lstCVarvwReceivables[0].SupplierID;
                objCVarReceivables.MeasurementID = objCvwReceivables.lstCVarvwReceivables[0].MeasurementID;
                objCVarReceivables.ContainerTypeID = objCvwReceivables.lstCVarvwReceivables[0].ContainerTypeID;
                objCVarReceivables.PackageTypeID = objCvwReceivables.lstCVarvwReceivables[0].PackageTypeID;
                objCVarReceivables.Quantity = objCvwReceivables.lstCVarvwReceivables[0].Quantity;
                objCVarReceivables.CostPrice = objCvwReceivables.lstCVarvwReceivables[0].CostPrice;
                objCVarReceivables.CostAmount = objCvwReceivables.lstCVarvwReceivables[0].CostAmount;
                objCVarReceivables.SalePrice = objCvwReceivables.lstCVarvwReceivables[0].SalePrice;

                objCVarReceivables.AmountWithoutVAT = objCvwReceivables.lstCVarvwReceivables[0].Quantity * objCvwReceivables.lstCVarvwReceivables[0].SalePrice;
                objCVarReceivables.TaxeTypeID = objCvwReceivables.lstCVarvwReceivables[0].TaxTypeID;
                objCVarReceivables.TaxPercentage = objCvwReceivables.lstCVarvwReceivables[0].TaxPercentage;
                objCVarReceivables.TaxAmount = objCvwReceivables.lstCVarvwReceivables[0].TaxAmount;

                objCVarReceivables.SaleAmount = objCvwReceivables.lstCVarvwReceivables[0].SaleAmount;
                objCVarReceivables.InvoiceID = 0;
                objCVarReceivables.AccNoteID = 0;
                objCVarReceivables.ExchangeRate = objCvwReceivables.lstCVarvwReceivables[0].ExchangeRate;
                objCVarReceivables.CurrencyID = objCvwReceivables.lstCVarvwReceivables[0].CurrencyID;
                objCVarReceivables.GeneratingQRID = 0;
                objCVarReceivables.Notes = "COPIED";

                objCVarReceivables.IssueDate = DateTime.Now;
                objCVarReceivables.OperationContainersAndPackagesID = 0;

                objCVarReceivables.ViewOrder = objCvwReceivables.lstCVarvwReceivables[0].ViewOrder;
                objCVarReceivables.IsDeleted = false;

                objCVarReceivables.CreatorUserID = objCVarReceivables.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarReceivables.CreationDate = objCVarReceivables.ModificationDate = DateTime.Now;
                objCVarReceivables.PayableID = 0;
                objCVarReceivables.HouseBillID = objCvwReceivables.lstCVarvwReceivables[0].HouseBillID;
                objCVarReceivables.OperationContainersAndPackagesID = objCvwReceivables.lstCVarvwReceivables[0].OperationContainersAndPackagesID;

                objCVarReceivables.PreviousCutOffDate = DateTime.Parse("01/01/1900");
                objCVarReceivables.CutOffDate = DateTime.Parse("01/01/1900");

                objCReceivables.lstCVarReceivables.Add(objCVarReceivables);
            }
            objCReceivables.SaveMethod(objCReceivables.lstCVarReceivables);

            string strWhere = "WHERE IsDeleted=0 AND (OperationID=" + pOperationID + " OR MasterOperationID=" + pOperationID + ")";
            strWhere += " and (OperationContainersAndPackagesID = " + pOperationContainersAndPackagesID + " ) ";

            if (pHouseBillID > 0)
            {
                strWhere += " and (HouseBillID = " + pHouseBillID + " ) ";
            }
            else
            {
                strWhere += " and (HouseBillID is null ) ";
            }

            objCvwReceivables.GetListPaging(999999, 1, strWhere, "ChargeTypeName", out _RowCount);
            return new object[] {
                new JavaScriptSerializer().Serialize(objCvwReceivables.lstCVarvwReceivables)
            };
        }

        #endregion


        #region O T H E R  F U N C T I O N S


        [HttpGet, HttpPost]
        public Object[] GenerateReceivables(string pInventoryID, bool pIsConsol)
        {
            bool _result = false;
            Exception checkException = null;

            CWH_Inventory objCWH_Inventory = new CWH_Inventory();

            // Getting Locations list
            checkException = objCWH_Inventory.GenerateReceivables(pInventoryID, WebSecurity.CurrentUserId, pIsConsol);

            if (checkException == null)
            {
                _result = true;
            }

            return new Object[]
            {
                _result // 0
            };
        }

        #endregion
    }
}
