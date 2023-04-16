using Forwarding.MvcApp.Entities.Operations;
using Forwarding.MvcApp.Models.Administration.Settings.Generated;
using Forwarding.MvcApp.Models.Customized;
using Forwarding.MvcApp.Models.MasterData.BanksAccountsAndTreasuries.Generated;
using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;
using Forwarding.MvcApp.Models.MasterData.Others.Generated;
using Forwarding.MvcApp.Models.MasterData.Partners.Generated;
using Forwarding.MvcApp.Models.NoAccess.Generated;
using Forwarding.MvcApp.Models.Operations.Operations.Customized;
using Forwarding.MvcApp.Models.Operations.Operations.Generated;
using Forwarding.MvcApp.Models.Quotations.Quotations.Generated;
using System;
using System.Drawing;
using System.Linq;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebMatrix.WebData;

namespace Forwarding.MvcApp.Controllers.Reports.API_Reports
{
    public class ReportsController : ApiController
    {

        [HttpGet, HttpPost]
        public object[] Report_Quotations(string pWhereClause, int pReportTypeID, string pDirectionType, string pTransportType, string pStage, string pSalesman, string pPOL, string pPOD, string pFromOpenDate, string pToOpenDate)
        {
            bool RecordsExist = true;
            var strExportedFileName = "";

            int _RowCount = 0;
            CvwQuotations objCvwQuotations = new CvwQuotations();
            objCvwQuotations.GetListPaging(3000, 1, pWhereClause, "ID DESC", out _RowCount);
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            if (objCvwQuotations.lstCVarvwQuotations.Count > 0)
            {
                RecordsExist = true;
                var pReportName = "Quotations.rpt";
                var pReportNameWithoutExtension = "Quotations"; //used to name the file to be exported by adding date and time to the file name then the extension
                //strExportedFileName = ExportReport(objCvwQuotations.lstCVarvwQuotations, pReportName, pReportNameWithoutExtension, pReportTypeID
                //    , new Object[] { pDirectionType, pTransportType, pStage, pSalesman, pPOL, pPOD, pFromOpenDate, pToOpenDate }); //ParameterFields
            }
            else
                RecordsExist = false;
            return new object[] { RecordsExist, strExportedFileName };
        }

        [HttpGet, HttpPost]
        public object[] Report_Operations(string pWhereClause, int pOperationsReportTypeID, string pDirectionType, string pTransportType, string pStage, string pSalesman, string pPOL, string pPOD, string pFromOpenDate, string pToOpenDate)
        {
            bool RecordsExist = true;
            var strExportedFileName = "";

            CvwOperations objCvwOperations = new CvwOperations();
            objCvwOperations.GetList(pWhereClause);
            if (objCvwOperations.lstCVarvwOperations.Count > 0)
            {
                RecordsExist = true;
                var pReportName = "Operations.rpt";
                var pReportNameWithoutExtension = "Operations"; //used to name the file to be exported by adding date and time to the file name then the extension
                //strExportedFileName = ExportReport(objCvwOperations.lstCVarvwOperations, pReportName, pReportNameWithoutExtension, pOperationsReportTypeID
                //    , new Object[] { pDirectionType, pTransportType, pStage, pSalesman, pPOL, pPOD, pFromOpenDate, pToOpenDate }); //ParameterFields
            }
            else
                RecordsExist = false;
            return new object[] { RecordsExist, strExportedFileName };
        }

        [HttpGet, HttpPost]
        public object[] Report_Invoice_Multiple(string pInvoiceIDsList, int pInvoiceReportTypeID, bool pIsPrintWithoutValidation
            , Int32 pBankTemplateID, bool pIsOriginalChassisItems)
        {
            string pWhereClause = "";
            string[] InvoiceIDsArray = pInvoiceIDsList.Split(',');
            int NumberOfInvoices = InvoiceIDsArray.Length;

            object[] arr = new object[NumberOfInvoices];

            for (int i = 0; i < NumberOfInvoices; i++)
            {
                pWhereClause = " WHERE ID = " + InvoiceIDsArray[i];
                arr[i] = Report_Invoice(pWhereClause, Int64.Parse(InvoiceIDsArray[i]), pInvoiceReportTypeID, pIsPrintWithoutValidation, pBankTemplateID, pIsOriginalChassisItems);

            }

            return arr;
        }

        [HttpGet, HttpPost] //pID : is the InvoiceID
        public object[] Report_Invoice(string pWhereClause, Int64 pID, int pInvoiceReportTypeID, bool pIsPrintWithoutValidation
            , Int32 pBankTemplateID, bool pIsOriginalChassisItems) //if pIsOriginalChassisItems where InvoiceID_3PL=InvoiceID
        {
            bool RecordsExist = true;
            string strExportedFileName = "";
            string MissingMandatoryFields = "";//MissingMandatoryFields is the message returned
            var MainRoutingTypeID = 30;
            var TruckingOrderRoutingTypeID = 60;
            var CustomsClearanceRoutingTypeID = 70;

            Int64 pELIInvoicePrefix = 0;
            CvwInvoices objCvwInvoices = new CvwInvoices();
            CvwOperationPartners objCvwOperationPartners = new CvwOperationPartners();
            CCustomers objCCustomers = new CCustomers();
            CAgents objCAgents = new CAgents();
            Int32 _RowCount = 0;
            Int32 _tempRowCount = 0;
            CDefaults objCDefaults = new CDefaults();
            objCDefaults.GetListPaging(999999, 1, "WHERE 1=1", "ID", out _tempRowCount);
            objCvwInvoices.GetListPaging(99999, 1, pWhereClause, "ID DESC", out _RowCount);
            objCvwOperationPartners.GetListPaging(1, 1, "WHERE ID=" + objCvwInvoices.lstCVarvwInvoices[0].OperationPartnerID, "ID", out _RowCount);
            string _InvoiceTypeCode = objCvwInvoices.lstCVarvwInvoices[0].InvoiceTypeCode;
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
            pUpdateClause = " AmountWithoutVAT = ROUND((SELECT SUM(ISNULL(SalePrice,0) * ISNULL(Quantity,1)) - ISNULL(FixedDiscount,0) FROM Receivables WHERE " + (objCvwInvoices.lstCVarvwInvoices[0].InvoiceTypeCode == "DRAFT" ? "DraftInvoiceID" : "InvoiceID") + " = " + pID + "),2)";
            pUpdateClause += " WHERE ID = " + pID;
            checkException = objCInvoices.UpdateList(pUpdateClause);
            //SET Tax, Discount & Total Amount after setting the AmountWithVAT
            pUpdateClause = " TaxAmount = ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(TaxPercentage, 0) / 100),2)";
            pUpdateClause += " , DiscountAmount = ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2)";
            if (objCDefaults.lstCVarDefaults[0].IsTaxOnItems)
                pUpdateClause += " , Amount = - ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2) + ROUND((SELECT SUM(ISNULL(SaleAmount,0)) - ISNULL(FixedDiscount,0) FROM Receivables WHERE " + (objCvwInvoices.lstCVarvwInvoices[0].InvoiceTypeCode == "DRAFT" ? "DraftInvoiceID" : "InvoiceID") + " = " + pID + "),2)";
            else
                pUpdateClause += " , Amount = ROUND((ISNULL(AmountWithoutVAT, 0) + ROUND( (ISNULL(AmountWithoutVAT, 0) * ISNULL(TaxPercentage, 0) / 100),2) - ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2)),2)";
            pUpdateClause += " WHERE ID = " + pID;
            checkException = objCInvoices.UpdateList(pUpdateClause);
            #endregion Update Invoice totals at server side to fix any connection problem
            
            #endregion Recalculate Total Sum
            #region Get Client Data
            if (objCvwInvoices.lstCVarvwInvoices[0].GeneralPartnerTypeCode == "CUSTOMERS")
                objCCustomers.GetListPaging(999999, 1, "WHERE ID=" + objCvwInvoices.lstCVarvwInvoices[0].PartnerID, "ID", out _tempRowCount);
            else if (objCvwInvoices.lstCVarvwInvoices[0].GeneralPartnerTypeCode == "AGENTS")
                objCAgents.GetListPaging(999999, 1, "WHERE ID=" + objCvwInvoices.lstCVarvwInvoices[0].PartnerID, "ID", out _tempRowCount);
            #endregion Get Client Data
            CvwInvoices objCvwFirstInvoiceInOperation_ELI = new CvwInvoices(); //get the first serial/operation of type INVOICE Elite

            CvwReceivables objCvwReceivables = new CvwReceivables();
            if (pIsOriginalChassisItems)
                objCvwReceivables.GetListPaging(99999, 1, " WHERE InvoiceID_3PL=" + pID, "ViewOrder, ChargeTypeName", out _RowCount);
            else
                objCvwReceivables.GetListPaging(99999, 1, " WHERE " + (_InvoiceTypeCode == "DRAFT" ? "DraftInvoiceID=" : "InvoiceID=") + pID, "ViewOrder, ChargeTypeName", out _RowCount);

            CvwRoutings objCFleetOrder = new CvwRoutings();
            if (objCvwInvoices.lstCVarvwInvoices[0].IsFleet 
                && objCvwInvoices.lstCVarvwInvoices[0].CancelledInvoiceID > 0)
                objCFleetOrder.GetListPaging(999999, 1, ("WHERE InvoiceID=" + objCvwInvoices.lstCVarvwInvoices[0].CancelledInvoiceID), "ID", out _RowCount);
            else
                objCFleetOrder.GetListPaging(999999, 1, ("WHERE InvoiceID=" + objCvwInvoices.lstCVarvwInvoices[0].ID), "ID", out _RowCount);
            CvwRoutings objCTruckingOrder = new CvwRoutings();
            objCTruckingOrder.GetListPaging(9999, 1, ("WHERE OperationID=" + objCvwInvoices.lstCVarvwInvoices[0].OperationID + " AND IsFleet=0 AND RoutingTypeID=" + TruckingOrderRoutingTypeID), "ID", out _RowCount);
            CvwRoutings objCCustomsClearance = new CvwRoutings();
            //objCCustomsClearance.GetListPaging(9999, 1, ("WHERE OperationID=" + objCvwInvoices.lstCVarvwInvoices[0].OperationID + " OR MasterOperationID=" + objCvwInvoices.lstCVarvwInvoices[0].OperationID + " AND RoutingTypeID=" + CustomsClearanceRoutingTypeID), "ID", out _RowCount);
            objCCustomsClearance.GetListPaging(9999, 1, ("WHERE OperationID=" + objCvwInvoices.lstCVarvwInvoices[0].OperationID + " AND RoutingTypeID=" + CustomsClearanceRoutingTypeID), "ID", out _RowCount);
            CvwRoutings objCvwRoutings = new CvwRoutings();
            objCvwRoutings.GetListPaging(99999, 1, " WHERE OperationID = " + objCvwInvoices.lstCVarvwInvoices[0].OperationID.ToString() + " AND RoutingTypeID = " + MainRoutingTypeID.ToString(), "ID", out _RowCount);
            var pDeliveryOrderNumber = objCvwRoutings.lstCVarvwRoutings[0].DeliveryOrderNumber;

            COperationContainersAndPackages objCOperationContainersAndPackages = new COperationContainersAndPackages();
            objCOperationContainersAndPackages.GetListPaging(99999, 1, " WHERE TankOrFlexiNumber IS NOT NULL AND OperationID = " + objCvwInvoices.lstCVarvwInvoices[0].OperationID, "ID", out _RowCount);
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
            if (objCvwInvoices.lstCVarvwInvoices.Count > 0)
            {
                RecordsExist = false;
                int constBLTypeMaster = 3;
                int constOceanTransportType = 1;
                int constInlandTransportType = 3;

                #region setting the header invoice parameters
                var pInvoiceDate = objCvwInvoices.lstCVarvwInvoices[0].InvoiceDate;
                var pInvoiceDueDate = objCvwInvoices.lstCVarvwInvoices[0].DueDate;
                var pPaymentTerm = (objCvwInvoices.lstCVarvwInvoices[0].PaymentTermCode == "0" ? "" : objCvwInvoices.lstCVarvwInvoices[0].PaymentTermCode);
                var pInvoiceNumber = objCvwInvoices.lstCVarvwInvoices[0].InvoiceNumber;
                var pPartnerName = objCvwInvoices.lstCVarvwInvoices[0].PartnerName;
                var pStreetLine1 = (objCvwInvoices.lstCVarvwInvoices[0].StreetLine1 == "0" ? "N/A" : objCvwInvoices.lstCVarvwInvoices[0].StreetLine1);
                var pStreetLine2 = (objCvwInvoices.lstCVarvwInvoices[0].StreetLine2 == "0" ? "" : objCvwInvoices.lstCVarvwInvoices[0].StreetLine2);
                var pCityName = (objCvwInvoices.lstCVarvwInvoices[0].CityName == "0" ? "" : objCvwInvoices.lstCVarvwInvoices[0].CityName);
                var pCountryName = (objCvwInvoices.lstCVarvwInvoices[0].CountryName == "0" ? "" : objCvwInvoices.lstCVarvwInvoices[0].CountryName);
                var pOperationCode = objCvwInvoices.lstCVarvwInvoices[0].OperationCode;
                var pLineName = (objCvwInvoices.lstCVarvwInvoices[0].LineName == "0" ? "" : objCvwInvoices.lstCVarvwInvoices[0].LineName);
                var pVesselName = (objCvwInvoices.lstCVarvwInvoices[0].VesselName == "0" ? "" : objCvwInvoices.lstCVarvwInvoices[0].VesselName);
                var pVoyageOrTruckNumber = (objCvwInvoices.lstCVarvwInvoices[0].VoyageOrTruckNumber == "0" ? "" : objCvwInvoices.lstCVarvwInvoices[0].VoyageOrTruckNumber);
                var pPOLCode = objCvwInvoices.lstCVarvwInvoices[0].POLCode;
                var pPODCode = objCvwInvoices.lstCVarvwInvoices[0].PODCode;
                var pMasterBL = (objCvwInvoices.lstCVarvwInvoices[0].MasterBL == "0" ? "" : objCvwInvoices.lstCVarvwInvoices[0].MasterBL);
                var pHouseNumber = (objCvwInvoices.lstCVarvwInvoices[0].HouseNumber == "0" ? "" : objCvwInvoices.lstCVarvwInvoices[0].HouseNumber);
                var pMasterOperationCode = (objCvwInvoices.lstCVarvwInvoices[0].HouseNumber == "0" ? "" : objCvwInvoices.lstCVarvwInvoices[0].MasterOperationCode);
                var pOperationID = objCvwInvoices.lstCVarvwInvoices[0].OperationID;
                var pPOLName = objCvwInvoices.lstCVarvwInvoices[0].POLName;
                var pPODName = objCvwInvoices.lstCVarvwInvoices[0].PODName;
                var pTaxTypeName = objCvwInvoices.lstCVarvwInvoices[0].TaxTypeName;
                var pTaxAmount = objCvwInvoices.lstCVarvwInvoices[0].TaxAmount;
                var pDiscountTypeName = objCvwInvoices.lstCVarvwInvoices[0].DiscountTypeName;
                var pDiscountAmount = objCvwInvoices.lstCVarvwInvoices[0].DiscountAmount;
                var pVATNumber = objCvwInvoices.lstCVarvwInvoices[0].VATNumber == "0" ? "N/A" : objCvwInvoices.lstCVarvwInvoices[0].VATNumber;
                var pLeftSignature = objCvwInvoices.lstCVarvwInvoices[0].LeftSignature;
                var pMiddleSignature = objCvwInvoices.lstCVarvwInvoices[0].MiddleSignature;
                var pRightSignature = objCvwInvoices.lstCVarvwInvoices[0].RightSignature;
                var pGRT = objCvwInvoices.lstCVarvwInvoices[0].GRT;
                var pDWT = objCvwInvoices.lstCVarvwInvoices[0].DWT;
                var pNRT = objCvwInvoices.lstCVarvwInvoices[0].NRT;
                var pLOA = objCvwInvoices.lstCVarvwInvoices[0].LOA;
                var pInvoiceTypeCode = objCvwInvoices.lstCVarvwInvoices[0].InvoiceTypeCode;
                #endregion

                CvwAddresses objCvwAddresses = new CvwAddresses();
                objCvwAddresses.GetList("WHERE ID = " + objCvwInvoices.lstCVarvwInvoices[0].AddressID.ToString());
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

                objCvwFirstInvoiceInOperation_ELI.GetListPaging(1, 1, "WHERE IsDeleted=0 AND (OperationID=" + objCvwInvoices.lstCVarvwInvoices[0].OperationID + " OR OperationID=" + objCvwOperations.lstCVarvwOperations[0].MasterOperationID + ")", "ID", out _RowCount);
                if (objCvwFirstInvoiceInOperation_ELI.lstCVarvwInvoices.Count > 0)
                    pELIInvoicePrefix = objCvwFirstInvoiceInOperation_ELI.lstCVarvwInvoices[0].InvoiceNumber;

                var pContainerTypes = objCvwOperations.lstCVarvwOperations[0].ContainerTypes;
                var pContainerNumbers = objCvwOperations.lstCVarvwOperations[0].ContainerNumbers == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].ContainerNumbers;
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
                objCvwDefaults.GetListPaging(1,1,"WHERE 1=1","ID",out _RowCount); //i am sure i ve just one row isa
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
                    TaxAmount = pReceivableList.Count > 0 ? ((s.Sum(i => i.Sale) * objCvwReceivables.lstCVarvwReceivables[0].TaxPercentage)/100) : 0
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
                    QRImage = objCCustomizedDBCall.GetImageByInvoiceID(objCvwInvoices.lstCVarvwInvoices[0].ID.ToString());


                ////i am sending data from receivables view and not invoices
                //strExportedFileName = ExportReport(objCvwReceivables.lstCVarvwReceivables, pReportName, pReportNameWithoutExtension, pInvoiceReportTypeID
                //    , new Object[] { pInvoiceDate, pInvoiceDueDate, pPaymentTerm, pInvoiceNumber, pPartnerName, pStreetLine1
                //        , pStreetLine2, pCityName, pCountryName, pOperationCode, pLineName, pVesselName, pVoyageOrTruckNumber
                //        , pPOLCode, pPODCode, pMasterBL, pHouseNumber }); //ParameterFields
                var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
                return new object[] {
                    RecordsExist, strExportedFileName
                    ,objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "ACS" || objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "DGL" || objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "SEF" || objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "NIS"
                        ? serializer.Serialize(pReceivableList)
                        : (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "SWI"
                            ? serializer.Serialize(objCvwReceivables.lstCVarvwReceivables.OrderBy(o => o.ID))
                            : serializer.Serialize(objCvwReceivables.lstCVarvwReceivables.OrderBy(o => o.ViewOrder))
                          )
                    , pContainerTypes, pHouseNumber, pMasterOperationCode, pTaxNumber
                    , /*pInvoiceDate.ToShortDateString()*/Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pInvoiceDate), pInvoiceNumber, pAccountName, pBankName,pBankAddress,pSwiftCode, pAccountNumber, pMasterBL, pPackageTypes
                    , pCustomerReference, MissingMandatoryFields, Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pInvoiceDueDate), pPOLName, pPODName, pHouseBLs
                    , pTaxTypeName, pTaxAmount, pDiscountTypeName, pDiscountAmount, pAddressLine1, pAddressLine2, pAddressLine3
                    , pPhones, pFaxes, pCBM, pGrossWeightSum, pClientStreetLine1, pClientStreetLine2
                    , pClientCityName, pClientCountryName, pShipmentTypeCode, pIncotermName
                    , pShipperName, pConsigneeName, pVesselName, Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pETA)
                    , Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pETD), pContainerNumbers, pSalesman, pVATNumber
                    , pDescriptionOfGoods, pVGM, pNumberOfPackages, pETAPOD.ToShortDateString(), pLeftSignature, pMiddleSignature, pRightSignature
                    , pGRT, pDWT, pNRT, pLOA, pInvoiceTypeCode, pBankDetailsTemplate
                    , new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations[0])
                    , new JavaScriptSerializer().Serialize(objCvwInvoices.lstCVarvwInvoices[0]), pDeliveryOrderNumber
                    , objCvwMasterOperationHeader.lstCVarvwOperations.Count > 0 ? new JavaScriptSerializer().Serialize(objCvwMasterOperationHeader.lstCVarvwOperations[0]) : null
                    , new JavaScriptSerializer().Serialize(objCvwDefaults.lstCVarvwDefaults[0])
                    , objCvwRoutings.lstCVarvwRoutings.Count > 0 ? new JavaScriptSerializer().Serialize(objCvwRoutings.lstCVarvwRoutings[0]) : null
                    , pELIInvoicePrefix
                    , objCvwInvoices.lstCVarvwInvoices[0].GeneralPartnerTypeCode == "CUSTOMERS" ? new JavaScriptSerializer().Serialize(objCCustomers.lstCVarCustomers[0])
                        : (objCvwInvoices.lstCVarvwInvoices[0].GeneralPartnerTypeCode == "AGENTS" ? new JavaScriptSerializer().Serialize(objCAgents.lstCVarAgents[0])
                        : null)
                    , pTankOrFlexiNumbers
                    , new JavaScriptSerializer().Serialize(objCTruckingOrder.lstCVarvwRoutings)
                    , serializer.Serialize(pFleetOrdersList)
                    , objCCustomsClearance.lstCVarvwRoutings.Count == 0 ? null : new JavaScriptSerializer().Serialize(objCCustomsClearance.lstCVarvwRoutings[0])
                    , objCvwOperationPartners.lstCVarvwOperationPartners.Count == 0 ? null : new JavaScriptSerializer().Serialize(objCvwOperationPartners.lstCVarvwOperationPartners[0])
                    , QRImage //QRImage
                };
            }
            else
            {
                RecordsExist = false;
                MissingMandatoryFields = "No records are found.";
                return new object[] { RecordsExist };
            }
        }

        [HttpGet, HttpPost]
        public object[] Report_AccNote(string pWhereClause, Int64 pAccNoteID, Int32 pAccNoteType, int pAccNoteReportTypeID, Int32 pBankTemplateID)
        {
            bool RecordsExist = true;
            string strExportedFileName = "";
            string MissingMandatoryFields = "";//MissingMandatoryFields is the message returned
            int constTransactionDebitNote = 90; //DebitNote (i.e. Receivables)
            int constTransactionCreditNote = 100; //CreditNote (i.e. Payables)
            int MainRoutingTypeID = 30;
            CvwReceivables objCvwReceivables = new CvwReceivables();
            CvwPayables objCvwPayables = new CvwPayables();

            CvwAccNote objCvwAccNotes = new CvwAccNote();
            //objCvwAccNotes.GetList(pWhereClause);
            Int32 _RowCount = 0;
            objCvwAccNotes.GetListPaging(1000, 1, pWhereClause, "ID DESC", out _RowCount);

            CvwRoutings objCvwRoutings = new CvwRoutings();
            objCvwRoutings.GetListPaging(9999, 1, " WHERE OperationID = " + objCvwAccNotes.lstCVarvwAccNote[0].OperationID.ToString() + " AND RoutingTypeID = " + MainRoutingTypeID.ToString(), "ID", out _RowCount);
            // i am sure i have just 1 row isa
            var pDeliveryOrderNumber = objCvwRoutings.lstCVarvwRoutings[0].DeliveryOrderNumber;
            var pBankDetailsTemplate = "";
            CBankTemplate objCBankTemplate = new CBankTemplate();
            if (pBankTemplateID != 0)
            {
                objCBankTemplate.GetList("WHERE ID=" + pBankTemplateID.ToString());
                pBankDetailsTemplate = objCBankTemplate.lstCVarBankTemplate[0].Subject == "0" ? "" : objCBankTemplate.lstCVarBankTemplate[0].Subject;
            }

            if (pAccNoteType == constTransactionDebitNote)
                objCvwReceivables.GetListPaging(1000, 1, " WHERE AccNoteID = " + pAccNoteID, "ViewOrder, ChargeTypeName", out _RowCount);
            else
                objCvwPayables.GetListPaging(1000, 1, " WHERE AccNoteID = " + pAccNoteID + " AND IsDeleted = 0 ", "ChargeTypeName", out _RowCount);

            //i am sure i have just 1 row coz the where clause used the ID
            if (objCvwAccNotes.lstCVarvwAccNote.Count > 0)
            {
                RecordsExist = false;
                var pReportName = "AccNote.rpt";
                var pReportNameWithoutExtension = "AccNote"; //used to name the file to be exported by adding date and time to the file name then the extension
                int constBLTypeMaster = 3;
                int constOceanTransportType = 1;

                #region setting the header AccNote parameters
                var pAccNoteDate = objCvwAccNotes.lstCVarvwAccNote[0].NoteDate;
                var pAccNoteRemarks = objCvwAccNotes.lstCVarvwAccNote[0].Remarks == "0" ? "" : objCvwAccNotes.lstCVarvwAccNote[0].Remarks;
                var pAccNoteCode = objCvwAccNotes.lstCVarvwAccNote[0].Code;
                var pPartnerName = objCvwAccNotes.lstCVarvwAccNote[0].PartnerName;
                var pStreetLine1 = (objCvwAccNotes.lstCVarvwAccNote[0].StreetLine1 == "0" ? "" : objCvwAccNotes.lstCVarvwAccNote[0].StreetLine1);
                var pStreetLine2 = (objCvwAccNotes.lstCVarvwAccNote[0].StreetLine2 == "0" ? "" : objCvwAccNotes.lstCVarvwAccNote[0].StreetLine2);
                var pCityName = (objCvwAccNotes.lstCVarvwAccNote[0].CityName == "0" ? "" : objCvwAccNotes.lstCVarvwAccNote[0].CityName);
                var pCountryName = (objCvwAccNotes.lstCVarvwAccNote[0].CountryName == "0" ? "" : objCvwAccNotes.lstCVarvwAccNote[0].CountryName);
                var pOperationCode = objCvwAccNotes.lstCVarvwAccNote[0].OperationCode;
                var pMasterBL = (objCvwAccNotes.lstCVarvwAccNote[0].MasterBL == "0" ? "" : objCvwAccNotes.lstCVarvwAccNote[0].MasterBL);
                var pHouseNumber = (objCvwAccNotes.lstCVarvwAccNote[0].HouseNumber == "0" ? "" : objCvwAccNotes.lstCVarvwAccNote[0].HouseNumber);
                var pMasterOperationCode = (objCvwAccNotes.lstCVarvwAccNote[0].HouseNumber == "0" ? "" : objCvwAccNotes.lstCVarvwAccNote[0].MasterOperationCode);
                var pOperationID = objCvwAccNotes.lstCVarvwAccNote[0].OperationID;
                var pTaxTypeName = objCvwAccNotes.lstCVarvwAccNote[0].TaxTypeName;
                var pTaxAmount = objCvwAccNotes.lstCVarvwAccNote[0].TaxAmount;
                var pDiscountTypeName = objCvwAccNotes.lstCVarvwAccNote[0].DiscountTypeName;
                var pDiscountAmount = objCvwAccNotes.lstCVarvwAccNote[0].DiscountAmount;
                var pInvoiceNumber = objCvwAccNotes.lstCVarvwAccNote[0].ConcatenatedInvoiceNumber;

                #endregion
                CvwAddresses objCvwAddresses = new CvwAddresses();
                objCvwAddresses.GetList("WHERE ID = " + objCvwAccNotes.lstCVarvwAccNote[0].AddressID.ToString());
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
                var pPOLName = objCvwOperations.lstCVarvwOperations[0].POLName;
                var pPODName = objCvwOperations.lstCVarvwOperations[0].PODName;
                var pContainerTypes = objCvwOperations.lstCVarvwOperations[0].ContainerTypes;
                var pPackageTypes = objCvwOperations.lstCVarvwOperations[0].PackageTypes;
                var pCustomerReference = objCvwOperations.lstCVarvwOperations[0].CustomerReference;
                var pHouseBLs = objCvwOperations.lstCVarvwOperations[0].HouseBLs;//used incase the AccNote is created for the master operation and holds all the HBL Nos on that operation
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
                if (pMasterBL == "")
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
                if (pActualDeparture.Equals(DateTime.Parse("01-01-1900")))
                    MissingMandatoryFields += (MissingMandatoryFields == "" ? "Actual departure date" : ", Actual departure date");

                if (MissingMandatoryFields == "")
                    RecordsExist = true;
                else
                {
                    RecordsExist = false;
                    MissingMandatoryFields = "You are missing " + MissingMandatoryFields + ".";
                }
                #endregion Checking mandatoryfields for printing this reports
                return new object[] {
                    RecordsExist, strExportedFileName
                    , pAccNoteType == constTransactionDebitNote ? new JavaScriptSerializer().Serialize(objCvwReceivables.lstCVarvwReceivables) : new JavaScriptSerializer().Serialize(objCvwPayables.lstCVarvwPayables)
                    , pContainerTypes, pHouseNumber, pMasterOperationCode, pTaxNumber
                    , /*pNoteDate.ToShortDateString()*/Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pAccNoteDate), pAccNoteCode, pAccountName, pBankName,pBankAddress,pSwiftCode, pAccountNumber, pMasterBL, pPackageTypes
                    , pCustomerReference, MissingMandatoryFields, pAccNoteRemarks, pPOLName, pPODName, pHouseBLs
                    , pTaxTypeName, pTaxAmount, pDiscountTypeName, pDiscountAmount, pAddressLine1, pAddressLine2, pAddressLine3
                    , pPhones, pFaxes, pCBM, pGrossWeightSum, pClientStreetLine1, pClientStreetLine2
                    , pClientCountryName, pClientCountryName, pShipmentTypeCode, pIncotermName
                    , pShipperName, pConsigneeName, pInvoiceNumber
                    , new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations[0])
                    , new JavaScriptSerializer().Serialize(objCvwAccNotes.lstCVarvwAccNote[0])
                    , Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pETA)
                    , pDeliveryOrderNumber, pBankDetailsTemplate
                };
            }
            else
            {
                RecordsExist = false;
                MissingMandatoryFields = "No records are found.";
                return new object[] { RecordsExist };
            }
        }

        [HttpGet, HttpPost]
        public object[] Report_DocsOut_Multiple(string pWhereClause, int pDocumentTypeID, string pOperationIDsList, int pReportTypeID, int pTruckingOrderID, string pSelectedPayableIDs, string pSelectedReceivableIDs, bool pWithVAT, Int32 pCommodityID)
        {

            string[] OperationIDsArray = pOperationIDsList.Split(',');
            int NumberOfOperations = OperationIDsArray.Length;

            object[] arr = new object[NumberOfOperations];

            for (int i = 0; i < NumberOfOperations; i++)
            {
                arr[i] = Report_DocsOut(pWhereClause, pDocumentTypeID, OperationIDsArray[i], pReportTypeID, pTruckingOrderID, pSelectedPayableIDs, pSelectedReceivableIDs, pWithVAT, pCommodityID);

            }

            return arr;
        }

        [HttpGet, HttpPost]
        public object[] Report_DocsOut(string pWhereClause, int pDocumentTypeID, string pOperationID, int pReportTypeID, int pTruckingOrderID, string pSelectedPayableIDs, string pSelectedReceivableIDs, bool pWithVAT, Int32 pCommodityID)
        {
            bool RecordsExist = true;
            CChargeTypeGroup objCChargeTypeGroup = new CChargeTypeGroup();
            objCChargeTypeGroup.GetList("ORDER BY Name");
            string pUserName = WebSecurity.CurrentUserName;
            var ShipperOperationParnterTypeID = 1;
            var ConsigneeOperationParnterTypeID = 2;
            var AgentOperationParnterTypeID = 6;
            var Notify1OperationParnterTypeID = 4;
            var Notify2OperationParnterTypeID = 5;
            var CCAOperationParnterTypeID = 8;
            var ShippingLineOperationPartnerTypeID = 9;
            var TruckerOperationParnterTypeID = 11;
            var SupplierOperationParnterTypeID = 12;
            var constMainAddressTypeID = 1;
            var MissingMandatoryFields = ""; //MissingMandatoryFields is the message returned
            var MainRoutingTypeID = 30;
            var TruckingOrderRoutingTypeID = 60;
            CDocumentTypes objCDocumentTypes = new CDocumentTypes();
            objCDocumentTypes.GetList(pWhereClause);
            // i am sure i have just 1 row isa
            var pReportName = objCDocumentTypes.lstCVarDocumentTypes[0].Name + ".rpt";
            var pReportNameWithoutExtension = objCDocumentTypes.lstCVarDocumentTypes[0].Name; //used to name the file to be exported by adding date and time to the file name then the extension
            var pDocumentISOCode = (objCDocumentTypes.lstCVarDocumentTypes[0].ISOCode == "0" ? "" : objCDocumentTypes.lstCVarDocumentTypes[0].ISOCode);
            var pIsPrintISOCode = objCDocumentTypes.lstCVarDocumentTypes[0].PrintISOCode;

            var strExportedFileName = "";
            int _RowCount = 0;

            #region OperationTracking Data
            CvwOperationTracking objCvwOperationTracking = new CvwOperationTracking();
            if (pReportName.ToUpper() == "OPERATION TRACKING.RPT")
                objCvwOperationTracking.GetListPaging(999999, 1, "WHERE OperationID=" + pOperationID, "TrackingDate", out _RowCount);
            #endregion OperationTracking Data

            #region vwHouses Data
            CvwOperations objCvwHouses = new CvwOperations();
            if (pReportName.ToUpper() == "MANIFEST.RPT" && pCommodityID != 0)
                objCvwHouses.GetListPaging(10000, 1, " WHERE MasterOperationID = " + pOperationID.ToString() +  " AND CommodityID=" + pCommodityID, "ID", out _RowCount);
            else
                objCvwHouses.GetListPaging(10000, 1, " WHERE MasterOperationID = " + pOperationID.ToString(), "ID", out _RowCount);
            #endregion vwHouses Data
            #region vwDefaults Data
            CvwDefaults objCvwDefaults = new CvwDefaults();
            objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa
            var pEnCompanyName = objCvwDefaults.lstCVarvwDefaults[0].CompanyName;
            var pArCompanyName = objCvwDefaults.lstCVarvwDefaults[0].CompanyLocalName;
            var pAddressLine1 = (objCvwDefaults.lstCVarvwDefaults[0].AddressLine1 == "0" ? "" : objCvwDefaults.lstCVarvwDefaults[0].AddressLine1);
            var pAddressLine2 = (objCvwDefaults.lstCVarvwDefaults[0].AddressLine2 == "0" ? "" : objCvwDefaults.lstCVarvwDefaults[0].AddressLine2);
            var pAddressLine3 = (objCvwDefaults.lstCVarvwDefaults[0].AddressLine3 == "0" ? "" : objCvwDefaults.lstCVarvwDefaults[0].AddressLine3);
            var pAddressLine4 = (objCvwDefaults.lstCVarvwDefaults[0].AddressLine4 == "0" ? "" : objCvwDefaults.lstCVarvwDefaults[0].AddressLine4);
            var pAddressLine5 = (objCvwDefaults.lstCVarvwDefaults[0].AddressLine5 == "0" ? "" : objCvwDefaults.lstCVarvwDefaults[0].AddressLine5);
            var pPhones = (objCvwDefaults.lstCVarvwDefaults[0].Phones == "0" ? "" : objCvwDefaults.lstCVarvwDefaults[0].Phones);
            var pFaxes = (objCvwDefaults.lstCVarvwDefaults[0].Faxes == "0" ? "" : objCvwDefaults.lstCVarvwDefaults[0].Faxes);
            var pEmail = (objCvwDefaults.lstCVarvwDefaults[0].Email == "0" ? "" : objCvwDefaults.lstCVarvwDefaults[0].Email);
            var pWebsite = (objCvwDefaults.lstCVarvwDefaults[0].Website == "0" ? "" : objCvwDefaults.lstCVarvwDefaults[0].Website);
            var pTaxNumber = objCvwDefaults.lstCVarvwDefaults[0].TaxNumber;
            #endregion vwDefaults Data
            #region vwOperationPartners Data
            var pShipperName = ""; var pShipperLocalName = ""; var pShipperContactName = ""; var pShipperPhones = ""; var pShipperFax = ""; var pShipperEmail = ""; var pShipperCountryName = ""; var pShipperCityName = ""; var pShipperAddressLine1 = ""; var pShipperAddressLine2 = "";//OperationPartnerTypeID = 1
            var pConsigneeName = ""; var pConsigneeLocalName = ""; var pConsigneeContactName = ""; var pConsigneePhones = ""; var pConsigneeFax = ""; var pConsigneeEmail = ""; var pConsigneeCountryName = ""; var pConsigneeCityName = ""; var pConsigneeAddressLine1 = ""; var pConsigneeAddressLine2 = ""; var pConsigneeAddressFromCustomers = "";//OperationPartnerTypeID = 2
            var pNotify1Name = ""; var pNotify1ContactName = ""; var pNotify1Phones = ""; var pNotify1Fax = ""; var pNotify1Email = ""; var pNotify1CountryName = ""; var pNotify1CityName = ""; var pNotify1AddressLine1 = ""; var pNotify1AddressLine2 = "";//OperationPartnerTypeID = 4
            var pNotify2Name = ""; var pNotify2ContactName = ""; var pNotify2Phones = ""; var pNotify2Fax = ""; var pNotify2Email = ""; var pNotify2CountryName = ""; var pNotify2CityName = ""; var pNotify2AddressLine1 = ""; var pNotify2AddressLine2 = "";//OperationPartnerTypeID = 5
            var pAgentName = ""; var pAgentContactName = ""; var pAgentPhones = ""; var pAgentFax = ""; var pAgentEmail = ""; var pAgentCountryName = ""; var pAgentCityName = ""; var pAgentAddressLine1 = ""; var pAgentAddressLine2 = "";
            var pCCAName = ""; var pCCAContactName = ""; var pCCAPhones = ""; var pCCAFax = ""; var pCCAEmail = ""; var pCCACountryName = ""; var pCCACityName = ""; var pCCAAddressLine1 = ""; var pCCAAddressLine2 = "";
            var pClientPhones = ""; var pClientFax = ""; var pClientAddressLine1 = ""; var pClientAddressLine2 = ""; var pClientCityName = ""; var pClientCountryName = "";
            var pTruckerName = "";
            var pTruckingOrderShippingLineName = "";
            var pSupplierName = "";
            var pShipperBankAccount = ""; var pConsigneeBankAccount = ""; //For AWB
            CCustomers objCCustomers = new CCustomers();
            CvwOperationPartners objCvwOperationPartners = new CvwOperationPartners();
            CVarvwOperationPartners objCShipper = new CVarvwOperationPartners();
            CVarvwOperationPartners objCConsignee = new CVarvwOperationPartners();
            CVarvwOperationPartners objCAgent = new CVarvwOperationPartners();
            CVarvwOperationPartners objCNotify1 = new CVarvwOperationPartners();

            objCvwOperationPartners.GetList(" WHERE OperationID = " + pOperationID.ToString() + " ORDER BY ViewOrder,ID DESC");
            foreach (var OperationPartner in objCvwOperationPartners.lstCVarvwOperationPartners)
            {
                if (OperationPartner.OperationPartnerTypeID == ShipperOperationParnterTypeID)
                {
                    objCShipper = OperationPartner;
                    pShipperName = (OperationPartner.PartnerName == "0" ? "" : OperationPartner.PartnerName);
                    pShipperLocalName = (OperationPartner.PartnerLocalName == "0" ? "" : OperationPartner.PartnerLocalName);
                    pShipperContactName = (OperationPartner.ContactName == "0" ? "" : OperationPartner.ContactName);
                    pShipperPhones = (OperationPartner.Phone1 == "0" ? "" : OperationPartner.Phone1)
                        + "   " + (OperationPartner.Phone2 == "0" ? "" : OperationPartner.Phone2)
                        + "   " + (OperationPartner.Mobile1 == "0" ? "" : OperationPartner.Mobile1);
                    pShipperFax = (OperationPartner.Fax == "0" ? "" : OperationPartner.Fax);
                    pShipperEmail = (OperationPartner.Email == "0" ? "" : OperationPartner.Email);
                    CvwAddresses objCvwShipperAddresses = new CvwAddresses();
                    objCvwShipperAddresses.GetList(" WHERE PartnerID = " + OperationPartner.PartnerID.ToString()
                                                + "  AND   PartnerTypeID = " + OperationPartner.PartnerTypeID
                                                + "  AND   AddressTypeID = " + constMainAddressTypeID.ToString()
                                                );
                    if (objCvwShipperAddresses.lstCVarvwAddresses.Count > 0)
                    {
                        pShipperCountryName = objCvwShipperAddresses.lstCVarvwAddresses[0].CountryName == "0" ? "" : objCvwShipperAddresses.lstCVarvwAddresses[0].CountryName;
                        pShipperCityName = objCvwShipperAddresses.lstCVarvwAddresses[0].CityName == "0" ? "" : objCvwShipperAddresses.lstCVarvwAddresses[0].CityName;
                        pShipperAddressLine1 = objCvwShipperAddresses.lstCVarvwAddresses[0].StreetLine1 == "0" ? "" : objCvwShipperAddresses.lstCVarvwAddresses[0].StreetLine1;
                        pShipperAddressLine2 = objCvwShipperAddresses.lstCVarvwAddresses[0].StreetLine2 == "0" ? "" : objCvwShipperAddresses.lstCVarvwAddresses[0].StreetLine2;
                        //if i have more than 1 shipper in the partners, then i will get the last one(but its used in AWB so its just shipper)
                        objCCustomers.GetList("WHERE ID=" + OperationPartner.PartnerID);
                        pShipperBankAccount = objCCustomers.lstCVarCustomers[0].BankAccountNumber == "0" ? "" : objCCustomers.lstCVarCustomers[0].BankAccountNumber;
                    }
                }
                else if (OperationPartner.OperationPartnerTypeID == ConsigneeOperationParnterTypeID)
                {
                    objCConsignee = OperationPartner;
                    pConsigneeName = (OperationPartner.PartnerName == "0" ? "" : OperationPartner.PartnerName);
                    pConsigneeLocalName = (OperationPartner.PartnerLocalName == "0" ? "" : OperationPartner.PartnerLocalName);
                    pConsigneeContactName = (OperationPartner.ContactName == "0" ? "" : OperationPartner.ContactName);
                    pConsigneePhones = (OperationPartner.Phone1 == "0" ? "" : OperationPartner.Phone1)
                        + "   " + (OperationPartner.Phone2 == "0" ? "" : OperationPartner.Phone2)
                        + "   " + (OperationPartner.Mobile1 == "0" ? "" : OperationPartner.Mobile1);
                    pConsigneeFax = (OperationPartner.Fax == "0" ? "" : OperationPartner.Fax);
                    pConsigneeEmail = (OperationPartner.Email == "0" ? "" : OperationPartner.Email);
                    CvwAddresses objCvwConsigneeAddresses = new CvwAddresses();
                    objCvwConsigneeAddresses.GetList(" WHERE PartnerID = " + OperationPartner.PartnerID.ToString()
                                                + "  AND   PartnerTypeID = " + OperationPartner.PartnerTypeID
                                                + "  AND   AddressTypeID = " + constMainAddressTypeID.ToString()
                                                );
                    if (objCvwConsigneeAddresses.lstCVarvwAddresses.Count > 0)
                    {
                        pConsigneeCountryName = objCvwConsigneeAddresses.lstCVarvwAddresses[0].CountryName == "0" ? "" : objCvwConsigneeAddresses.lstCVarvwAddresses[0].CountryName;
                        pConsigneeCityName = objCvwConsigneeAddresses.lstCVarvwAddresses[0].CityName == "0" ? "" : objCvwConsigneeAddresses.lstCVarvwAddresses[0].CityName;
                        pConsigneeAddressLine1 = objCvwConsigneeAddresses.lstCVarvwAddresses[0].StreetLine1 == "0" ? "" : objCvwConsigneeAddresses.lstCVarvwAddresses[0].StreetLine1;
                        pConsigneeAddressLine2 = objCvwConsigneeAddresses.lstCVarvwAddresses[0].StreetLine2 == "0" ? "" : objCvwConsigneeAddresses.lstCVarvwAddresses[0].StreetLine2;

                        objCCustomers.GetList("WHERE ID=" + OperationPartner.PartnerID);
                        pConsigneeAddressFromCustomers = objCCustomers.lstCVarCustomers[0].Address;
                        //if i have more than 1 Consignee in the partners, then i will get the last one(but its used in AWB so its just Consignee)
                        pConsigneeBankAccount = objCCustomers.lstCVarCustomers[0].BankAccountNumber == "0" ? "" : objCCustomers.lstCVarCustomers[0].BankAccountNumber;
                    }
                }
                else if (OperationPartner.OperationPartnerTypeID == Notify1OperationParnterTypeID)
                {
                    objCNotify1 = OperationPartner;
                    pNotify1Name = (OperationPartner.PartnerName == "0" ? "" : OperationPartner.PartnerName);
                    pNotify1ContactName = (OperationPartner.ContactName == "0" ? "" : OperationPartner.ContactName);
                    pNotify1Phones = (OperationPartner.Phone1 == "0" ? "" : OperationPartner.Phone1)
                        + "   " + (OperationPartner.Phone2 == "0" ? "" : OperationPartner.Phone2)
                        + "   " + (OperationPartner.Mobile1 == "0" ? "" : OperationPartner.Mobile1);
                    pNotify1Fax = (OperationPartner.Fax == "0" ? "" : OperationPartner.Fax);
                    pNotify1Email = (OperationPartner.Email == "0" ? "" : OperationPartner.Email);
                    CvwAddresses objCvwNotify1Addresses = new CvwAddresses();
                    objCvwNotify1Addresses.GetList(" WHERE PartnerID = " + OperationPartner.PartnerID.ToString()
                                                + "  AND   PartnerTypeID = " + OperationPartner.PartnerTypeID
                                                + "  AND   AddressTypeID = " + constMainAddressTypeID.ToString()
                                                );
                    if (objCvwNotify1Addresses.lstCVarvwAddresses.Count > 0)
                    {
                        pNotify1CountryName = objCvwNotify1Addresses.lstCVarvwAddresses[0].CountryName == "0" ? "" : objCvwNotify1Addresses.lstCVarvwAddresses[0].CountryName;
                        pNotify1CityName = objCvwNotify1Addresses.lstCVarvwAddresses[0].CityName == "0" ? "" : objCvwNotify1Addresses.lstCVarvwAddresses[0].CityName;
                        pNotify1AddressLine1 = objCvwNotify1Addresses.lstCVarvwAddresses[0].StreetLine1 == "0" ? "" : objCvwNotify1Addresses.lstCVarvwAddresses[0].StreetLine1;
                        pNotify1AddressLine2 = objCvwNotify1Addresses.lstCVarvwAddresses[0].StreetLine2 == "0" ? "" : objCvwNotify1Addresses.lstCVarvwAddresses[0].StreetLine2;
                    }
                }
                else if (OperationPartner.OperationPartnerTypeID == Notify2OperationParnterTypeID)
                {
                    pNotify2Name = (OperationPartner.PartnerName == "0" ? "" : OperationPartner.PartnerName);
                    pNotify2ContactName = (OperationPartner.ContactName == "0" ? "" : OperationPartner.ContactName);
                    pNotify2Phones = (OperationPartner.Phone1 == "0" ? "" : OperationPartner.Phone1)
                        + "   " + (OperationPartner.Phone2 == "0" ? "" : OperationPartner.Phone2)
                        + "   " + (OperationPartner.Mobile1 == "0" ? "" : OperationPartner.Mobile1);
                    pNotify2Fax = (OperationPartner.Fax == "0" ? "" : OperationPartner.Fax);
                    pNotify2Email = (OperationPartner.Email == "0" ? "" : OperationPartner.Email);
                    CvwAddresses objCvwNotify2Addresses = new CvwAddresses();
                    objCvwNotify2Addresses.GetList(" WHERE PartnerID = " + OperationPartner.PartnerID.ToString()
                                                + "  AND   PartnerTypeID = " + OperationPartner.PartnerTypeID
                                                + "  AND   AddressTypeID = " + constMainAddressTypeID.ToString()
                                                );
                    if (objCvwNotify2Addresses.lstCVarvwAddresses.Count > 0)
                    {
                        pNotify2CountryName = objCvwNotify2Addresses.lstCVarvwAddresses[0].CountryName == "0" ? "" : objCvwNotify2Addresses.lstCVarvwAddresses[0].CountryName;
                        pNotify2CityName = objCvwNotify2Addresses.lstCVarvwAddresses[0].CityName == "0" ? "" : objCvwNotify2Addresses.lstCVarvwAddresses[0].CityName;
                        pNotify2AddressLine1 = objCvwNotify2Addresses.lstCVarvwAddresses[0].StreetLine1 == "0" ? "" : objCvwNotify2Addresses.lstCVarvwAddresses[0].StreetLine1;
                        pNotify2AddressLine2 = objCvwNotify2Addresses.lstCVarvwAddresses[0].StreetLine2 == "0" ? "" : objCvwNotify2Addresses.lstCVarvwAddresses[0].StreetLine2;
                    }
                }
                else if (OperationPartner.OperationPartnerTypeID == AgentOperationParnterTypeID)
                {
                    objCAgent = OperationPartner;
                    pAgentName = (OperationPartner.PartnerName == "0" ? "" : OperationPartner.PartnerName);
                    pAgentContactName = (OperationPartner.ContactName == "0" ? "" : OperationPartner.ContactName);
                    pAgentPhones = (OperationPartner.Phone1 == "0" ? "" : OperationPartner.Phone1)
                        + "   " + (OperationPartner.Phone2 == "0" ? "" : OperationPartner.Phone2)
                        + "   " + (OperationPartner.Mobile1 == "0" ? "" : OperationPartner.Mobile1);
                    pAgentFax = (OperationPartner.Fax == "0" ? "" : OperationPartner.Fax);
                    pAgentEmail = (OperationPartner.Email == "0" ? "N/A" : OperationPartner.Email);
                    CvwAddresses objCvwAgentAddresses = new CvwAddresses();
                    objCvwAgentAddresses.GetList(" WHERE PartnerID = " + OperationPartner.PartnerID.ToString()
                                                + "  AND   PartnerTypeID = " + OperationPartner.PartnerTypeID
                                                + "  AND   AddressTypeID = " + constMainAddressTypeID.ToString()
                                                );
                    if (objCvwAgentAddresses.lstCVarvwAddresses.Count > 0)
                    {
                        pAgentCountryName = objCvwAgentAddresses.lstCVarvwAddresses[0].CountryName == "0" ? "" : objCvwAgentAddresses.lstCVarvwAddresses[0].CountryName;
                        pAgentCityName = objCvwAgentAddresses.lstCVarvwAddresses[0].CityName == "0" ? "" : objCvwAgentAddresses.lstCVarvwAddresses[0].CityName;
                        pAgentAddressLine1 = objCvwAgentAddresses.lstCVarvwAddresses[0].StreetLine1 == "0" ? "" : objCvwAgentAddresses.lstCVarvwAddresses[0].StreetLine1;
                        pAgentAddressLine2 = objCvwAgentAddresses.lstCVarvwAddresses[0].StreetLine2 == "0" ? "" : objCvwAgentAddresses.lstCVarvwAddresses[0].StreetLine2;
                    }
                }
                else if (OperationPartner.OperationPartnerTypeID == CCAOperationParnterTypeID)
                {
                    pCCAName = (OperationPartner.PartnerName == "0" ? "" : OperationPartner.PartnerName);
                    pCCAContactName = (OperationPartner.ContactName == "0" ? "" : OperationPartner.ContactName);
                    pCCAPhones = (OperationPartner.Phone1 == "0" ? "" : OperationPartner.Phone1)
                        + "   " + (OperationPartner.Phone2 == "0" ? "" : OperationPartner.Phone2)
                        + "   " + (OperationPartner.Mobile1 == "0" ? "" : OperationPartner.Mobile1);
                }

                else if (OperationPartner.OperationPartnerTypeID == TruckerOperationParnterTypeID)
                    pTruckerName = (OperationPartner.PartnerName == "0" ? "" : OperationPartner.PartnerName);
                else if (OperationPartner.OperationPartnerTypeID == SupplierOperationParnterTypeID)
                    pSupplierName = (OperationPartner.PartnerName == "0" ? "" : OperationPartner.PartnerName);
                else if (OperationPartner.OperationPartnerTypeID == ShippingLineOperationPartnerTypeID)
                    pTruckingOrderShippingLineName = OperationPartner.PartnerName == "0" ? "" : OperationPartner.PartnerName;

                #region set ClientData
                if (OperationPartner.IsOperationClient)
                {
                    pClientPhones = (OperationPartner.Phone1 == "0" ? "" : OperationPartner.Phone1)
                        + "   " + (OperationPartner.Phone2 == "0" ? "" : OperationPartner.Phone2)
                        + "   " + (OperationPartner.Mobile1 == "0" ? "" : OperationPartner.Mobile1);
                    pClientFax = (OperationPartner.Fax == "0" ? "" : OperationPartner.Fax);
                    CvwAddresses objCvwClientAddresses = new CvwAddresses();
                    objCvwClientAddresses.GetList(" WHERE PartnerID = " + OperationPartner.PartnerID.ToString()
                                                + "  AND   PartnerTypeID = " + OperationPartner.PartnerTypeID
                                                + "  AND   AddressTypeID = " + constMainAddressTypeID.ToString()
                                                );
                    if (objCvwClientAddresses.lstCVarvwAddresses.Count > 0)
                    {
                        pClientCountryName = objCvwClientAddresses.lstCVarvwAddresses[0].CountryName == "0" ? "" : objCvwClientAddresses.lstCVarvwAddresses[0].CountryName;
                        pClientCityName = objCvwClientAddresses.lstCVarvwAddresses[0].CityName == "0" ? "" : objCvwClientAddresses.lstCVarvwAddresses[0].CityName;
                        pClientAddressLine1 = objCvwClientAddresses.lstCVarvwAddresses[0].StreetLine1 == "0" ? "" : objCvwClientAddresses.lstCVarvwAddresses[0].StreetLine1;
                        pClientAddressLine2 = objCvwClientAddresses.lstCVarvwAddresses[0].StreetLine2 == "0" ? "" : objCvwClientAddresses.lstCVarvwAddresses[0].StreetLine2;
                    }
                }
                #endregion set ClientData
            }
            //var pShipperName = objCvwOperationPartners.lstCVarvwOperationPartner[0].CompanyName;
            //var pArCompanyName = objCvwOperationPartners.lstCVarvwOperationPartner[0].CompanyLocalName;
            //var pAddressLine1 = (objCvwOperationPartners.lstCVarvwOperationPartner[0].AddressLine1 == "0" ? "" : objCvwOperationPartner.lstCVarvwOperationPartner[0].AddressLine1);
            #endregion vwOperationPartners Data
            #region vwRoutings Data
            CvwRoutings objCvwRoutings = new CvwRoutings();
            //objCvwRoutings.GetList(" WHERE OperationID = " + pOperationID.ToString() + " AND RoutingTypeID = " + MainRoutingTypeID.ToString());
            objCvwRoutings.GetListPaging(9999, 1, " WHERE OperationID = " + pOperationID.ToString() + " AND RoutingTypeID = " + MainRoutingTypeID.ToString(), "ID", out _RowCount);
            // i am sure i have just 1 row isa
            var pVoyageOrTruckNumber = (objCvwRoutings.lstCVarvwRoutings[0].VoyageOrTruckNumber == "0" ? "N/A" : objCvwRoutings.lstCVarvwRoutings[0].VoyageOrTruckNumber);
            var pRoadNumber = objCvwRoutings.lstCVarvwRoutings[0].RoadNumber;
            var pDeliveryOrderNumber = objCvwRoutings.lstCVarvwRoutings[0].DeliveryOrderNumber;
            var pDeliveryDate = objCvwRoutings.lstCVarvwRoutings[0].DeliveryDate; //string Date
            //var pVesselName = (objCvwRoutings.lstCVarvwRoutings[0].VesselName == "0" ? "" : objCvwRoutings.lstCVarvwRoutings[0].VesselName);

            CvwRoutings objCvwRoutingsTruckingOrder = new CvwRoutings();
            objCvwRoutingsTruckingOrder.GetListPaging(999,1," Where ID = "+ pTruckingOrderID ," ID",out _RowCount);

            #endregion vwRoutings Data
            #region vwOperationContainersAndPackages Data
            //used as data source // i might have many rows
            CvwOperationContainersAndPackages objCvwOperationContainersAndPackages = new CvwOperationContainersAndPackages();
            objCvwOperationContainersAndPackages.GetListPaging(99999, 1, " WHERE OperationID = " + pOperationID.ToString(), "ID", out _RowCount);

            var pRoadOrContainerNumber = "0";
            if (objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages.Count > 0)
                pRoadOrContainerNumber = objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages[0].ContainerNumber == "0" ? "N/A" : objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages[0].ContainerNumber;
            //MarksAndNumbers are got from the Datasource Location (objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages)
            //objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages[0].MarksAndNumbers;
            #endregion vwOperationContainersAndPackages Data
            #region vwPaymentRequests/OperationPayables Data
            Forwarding.MvcApp.Models.Operations.Operations.Customized.CvwPaymentRequests objCvwPaymentRequests = new Forwarding.MvcApp.Models.Operations.Operations.Customized.CvwPaymentRequests();
            //objCvwPaymentRequests.GetList_Customized(" WHERE (OperationID = " + pOperationID.ToString()
            //    + " OR MasterOperationID = " + pOperationID.ToString() + ")\n"
            //    + (pSelectedPayableIDs == "0" || pReportName.ToUpper() != "PAYMENTREQUESTS.RPT" ? "" : (" AND (PayableID IN (" + pSelectedPayableIDs + ")) \n"))
            //    + " GROUP BY CurrencyCode , ChargeTypeName ");
            Forwarding.MvcApp.Models.Operations.Operations.Customized.CvwPayablesSubTotals objCvwPayablesSubTotals = new Forwarding.MvcApp.Models.Operations.Operations.Customized.CvwPayablesSubTotals();
            objCvwPayablesSubTotals.GetList_Customized(
                " WHERE (OperationID = " + pOperationID.ToString() + " OR MasterOperationID = " + pOperationID.ToString() + ")\n"
                + (pSelectedPayableIDs == "0" || pReportName.ToUpper() != "PAYMENTREQUESTS.RPT" ? "" : (" AND (PayableID IN (" + pSelectedPayableIDs + ")) \n"))
                + " GROUP BY CurrencyCode ORDER BY CurrencyCode ");
            //Set IsPrinted For PaymentReuest
            if (pReportName.ToUpper() == "PAYMENTREQUESTS.RPT")
            {
                CPayables objCPayables = new CPayables();
                objCPayables.UpdateList("IsPrinted=1 WHERE (OperationID = " + pOperationID.ToString() + ")\n"
                                        + (pSelectedPayableIDs == "0" ? "" : (" AND (ID IN (" + pSelectedPayableIDs + ")) \n"))
                                       );
            }
            #endregion vwPaymentRequests/OperationPayables Data

            #region vwOperations Data
            CvwOperations objCvwOperations = new CvwOperations();
            objCvwOperations.GetListPaging(999999, 1, " WHERE ID = " + pOperationID.ToString(), "ID", out _RowCount);
            CvwRoutings objCvwRoutingsMaster = new CvwRoutings();
            if (objCvwOperations.lstCVarvwOperations[0].MasterOperationID > 0)
                objCvwRoutingsMaster.GetListPaging(9999, 1, " WHERE OperationID = " + objCvwOperations.lstCVarvwOperations[0].MasterOperationID + " AND RoutingTypeID = " + MainRoutingTypeID.ToString(), "ID", out _RowCount);

            // i am sure i have just 1 row isa
            //if (objCvwOperations.lstCVarvwOperations.Count > 0)
            //{
            //RecordsExist = true;
            var pOperationCreatorName = objCvwOperations.lstCVarvwOperations[0].CreatorName;
            var pOperationCode = objCvwOperations.lstCVarvwOperations[0].Code;
            var pQuotationCode = (objCvwOperations.lstCVarvwOperations[0].QuotationCode == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].QuotationCode); //Offer No.
            var pDirectionType = objCvwOperations.lstCVarvwOperations[0].RepDirectionTypeShown;
            var pShipmentType = objCvwOperations.lstCVarvwOperations[0].ShipmentTypeCode;
            var pTransportType = objCvwOperations.lstCVarvwOperations[0].RepTransportTypeShown;
            var pBranchName = objCvwOperations.lstCVarvwOperations[0].BranchName;
            var pOpenDate = objCvwOperations.lstCVarvwOperations[0].OpenDate;
            var pBLDate = objCvwOperations.lstCVarvwOperations[0].BLDate;
            var pClientName = (objCvwOperations.lstCVarvwOperations[0].ClientName == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].ClientName);
            var pMoveTypeName = objCvwOperations.lstCVarvwOperations[0].MoveTypeName;
            var pAgreedRate = objCvwOperations.lstCVarvwOperations[0].AgreedRate;
            //if (objCvwOperations.lstCVarvwOperations[0].DirectionType == 1 && pClientCountryName == ""/*this condition applies when IsOperationClient is not set to default or no address is set to operation client*/)
            //{
            //    pClientAddressLine1 = pConsigneeAddressLine1;
            //    pClientAddressLine2 = pConsigneeAddressLine2;
            //    pClientCityName = pConsigneeCityName;
            //    pClientCountryName = pConsigneeCountryName;
            //    pClientPhones = pConsigneePhones;
            //    pClientFax = pConsigneeFax;
            //}
            //else if (pClientCountryName == ""/*this condition applies when IsOperationClient is not set to default or no address is set to operation client*/)
            //{
            //    pClientAddressLine1 = pShipperAddressLine1;
            //    pClientAddressLine2 = pShipperAddressLine2;
            //    pClientCityName = pShipperCityName;
            //    pClientCountryName = pShipperCountryName;
            //    pClientPhones = pShipperPhones;
            //    pClientFax = pShipperFax;
            //}
            var pContainerTypes = (objCvwOperations.lstCVarvwOperations[0].ContainerTypes == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].ContainerTypes);
            var pPackageTypesOnContainersTotals = objCvwOperations.lstCVarvwOperations[0].PackageTypesOnContainersTotals == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].PackageTypesOnContainersTotals;
            var pPackageTypes = (objCvwOperations.lstCVarvwOperations[0].PackageTypes == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].PackageTypes);
            var pCommodityName = (objCvwOperations.lstCVarvwOperations[0].CommodityName == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].CommodityName);
            var pPOL = objCvwOperations.lstCVarvwOperations[0].POLCode;
            var pPOD = objCvwOperations.lstCVarvwOperations[0].PODCode;
            var pPOLName = objCvwOperations.lstCVarvwOperations[0].POLName;
            var pPODName = objCvwOperations.lstCVarvwOperations[0].PODName;
            var pPOLCodeAndName = objCvwOperations.lstCVarvwOperations[0].POLName + " : " + pPOL;
            var pPODCodeAndName = objCvwOperations.lstCVarvwOperations[0].PODName + " : " + pPOD;
            var pLineName = (objCvwOperations.lstCVarvwOperations[0].LineName == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].LineName);
            var pLineLocalName = (objCvwOperations.lstCVarvwOperations[0].LineLocalName == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].LineLocalName);
            var pLineBankName = (objCvwOperations.lstCVarvwOperations[0].LineBankName == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].LineBankName);
            var pLineBankAccountNumber = (objCvwOperations.lstCVarvwOperations[0].LineBankAccountNumber == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].LineBankAccountNumber);
            var pVesselName = (objCvwOperations.lstCVarvwOperations[0].VesselName == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].VesselName);
            var pSalesman = (objCvwOperations.lstCVarvwOperations[0].Salesman == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].Salesman);
            //i got agent from partners instead from operations//var pAgentName = (objCvwOperations.lstCVarvwOperations[0].AgentName == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].AgentName);
            var pClientEmail = ""; //(objCvwOperations.lstCVarvwOperations[0].ClientEmail == "0" ? "" : objCvwOperations.lstCVarvwOperations[0].ClientEmail);
            var pIncotermName = (objCvwOperations.lstCVarvwOperations[0].IncotermName == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].IncotermName);
            var pETAPOLDate = objCvwOperations.lstCVarvwOperations[0].ETAPOLDate;
            var pATAPOLDate = objCvwOperations.lstCVarvwOperations[0].ATAPOLDate;
            var pActualDeparture = objCvwOperations.lstCVarvwOperations[0].ActualDeparture;
            var pActualArrival = objCvwOperations.lstCVarvwOperations[0].ActualArrival;
            var pExpectedArrival = objCvwOperations.lstCVarvwOperations[0].ExpectedArrival;
            var pExpectedDeparture = objCvwOperations.lstCVarvwOperations[0].ExpectedDeparture;
            var pMasterBL = (objCvwOperations.lstCVarvwOperations[0].MasterBL == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].MasterBL);
            var pHouseNumber = (objCvwOperations.lstCVarvwOperations[0].HouseNumber == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].HouseNumber);
            var pContainerNumbers = (objCvwOperations.lstCVarvwOperations[0].ContainerNumbers == "0" || objCvwOperations.lstCVarvwOperations[0].ContainerNumbers == "" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].ContainerNumbers);
            var pGrossWeightSum = objCvwOperations.lstCVarvwOperations[0].GrossWeightSum;
            var pVGMSum = objCvwOperations.lstCVarvwOperations[0].VGMSum;
            var pNetWeightSum = objCvwOperations.lstCVarvwOperations[0].NetWeightSum;
            var pDimensions = objCvwOperations.lstCVarvwOperations[0].Dimensions;
            var pVolumeSum = objCvwOperations.lstCVarvwOperations[0].VolumeSum;
            var pInvoiceNumbers = (objCvwOperations.lstCVarvwOperations[0].InvoiceNumbers == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].InvoiceNumbers);
            var pPOrC = objCvwOperations.lstCVarvwOperations[0].POrCCode;
            var pDescriptionOfGoods = (objCvwOperations.lstCVarvwOperations[0].Notes == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].Notes);
            var pTransientTime = objCvwOperations.lstCVarvwOperations[0].TransientTime;
            var pHouseBLs = (objCvwOperations.lstCVarvwOperations[0].HouseBLs == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].HouseBLs);
            var pHouseClients = (objCvwOperations.lstCVarvwOperations[0].HouseClients == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].HouseClients);
            var pRepBLTypeShown = objCvwOperations.lstCVarvwOperations[0].RepBLTypeShown;
            var pMasterOperationCode = objCvwOperations.lstCVarvwOperations[0].MasterOperationCode == "" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].MasterOperationCode;
            var pDeliveryCountryName = objCvwOperations.lstCVarvwOperations[0].DeliveryCountryName == "0" ? "" : objCvwOperations.lstCVarvwOperations[0].DeliveryCountryName;
            var pDeliveryCityName = objCvwOperations.lstCVarvwOperations[0].DeliveryCityName == "0" ? "" : objCvwOperations.lstCVarvwOperations[0].DeliveryCityName;
            var pPickupAddress = objCvwOperations.lstCVarvwOperations[0].PickupAddress == "0" ? "" : objCvwOperations.lstCVarvwOperations[0].PickupAddress;
            var pDeliveryAddress = objCvwOperations.lstCVarvwOperations[0].DeliveryAddress == "0" ? "" : objCvwOperations.lstCVarvwOperations[0].DeliveryAddress;
            var pCustomerReference = objCvwOperations.lstCVarvwOperations[0].CustomerReference == "0" ? "N/A" : objCvwOperations.lstCVarvwOperations[0].CustomerReference;
            bool pIsDelivered = objCvwOperations.lstCVarvwOperations[0].IsDelivered;
            bool pIsTrucking = objCvwOperations.lstCVarvwOperations[0].IsTrucking;
            bool pIsInsurance = objCvwOperations.lstCVarvwOperations[0].IsInsurance;
            bool pIsClearance = objCvwOperations.lstCVarvwOperations[0].IsClearance;
            bool pIsGenset = objCvwOperations.lstCVarvwOperations[0].IsGenset;
            bool pIsCourrier = objCvwOperations.lstCVarvwOperations[0].IsCourrier;
            bool pIsTelexRelease = objCvwOperations.lstCVarvwOperations[0].IsTelexRelease;
            var pNumberOfPackages = objCvwOperations.lstCVarvwOperations[0].PackageTypesOnContainersTotals == "0"
                                        ? objCvwOperations.lstCVarvwOperations[0].PackageTypes
                                        : objCvwOperations.lstCVarvwOperations[0].PackageTypesOnContainersTotals;
            var pPONumber = objCvwOperations.lstCVarvwOperations[0].PONumber;
            #endregion vwOperations Data
            #region vwPayables Data
            CvwPayables objCvwPayables = new CvwPayables();
            if (objCvwOperations.lstCVarvwOperations[0].IsAWB)
                objCvwPayables.GetListPaging(3000, 1, " WHERE OperationID = " + pOperationID.ToString(), "ChargeTypeName", out _RowCount);
            else
            {
                if (
                    (objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName.ToUpper() == "KDS")
                    && pReportName.ToUpper() == "OPERATIONSTATEMENT.RPT"
                    && pSelectedPayableIDs != "0" && pSelectedReceivableIDs != "0"
                ) //Kadmar select Operation Statement items manually
                    objCvwPayables.GetListPaging(99999, 1, " WHERE ID IN (" + pSelectedPayableIDs + ")", "ID", out _RowCount);
                else if (pReportName.ToUpper() != "LETTERRELEASE.RPT")
                    objCvwPayables.GetListPaging(99999, 1, " WHERE OperationID = " + pOperationID.ToString() + " OR MasterOperationID = " + pOperationID.ToString(), "ChargeTypeName", out _RowCount);
            }
            #endregion vwPayables Data

            #region vwReceivables Data
            CvwReceivables objCvwReceivables = new CvwReceivables();
            CvwInvoices objCvwInvoices = new CvwInvoices();
            if (
                    (objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName.ToUpper() == "KDS")
                    && pReportName.ToUpper() == "OPERATIONSTATEMENT.RPT"
                    && pSelectedPayableIDs != "0" && pSelectedReceivableIDs != "0"
                ) //Kadmar select Operation Statement items manually
                objCvwReceivables.GetListPaging(99999, 1, " WHERE ID IN (" + pSelectedReceivableIDs + ")", " ID ", out _RowCount);
            else if (pReportName.ToUpper() != "LETTERRELEASE.RPT")
                objCvwReceivables.GetListPaging(99999, 1, " WHERE IsDeleted=0 AND (OperationID = " + pOperationID + " OR MasterOperationID = " + pOperationID + ")" + (pReportName.ToUpper() == "OPERATIONSTATEMENT.RPT" ? " AND (InvoiceID IS NOT NULL OR AccNoteID IS NOT NULL) " : ""), " ID ", out _RowCount);
            #endregion vwReceivables Data

            #region vwInvoices Data
            //CInvoices objCInvoices = new CInvoices();
            //if (pReportName.ToUpper() == "OPERATIONSTATEMENT.RPT")
            //    objCvwInvoices.GetListPaging(99999, 1, " WHERE IsDeleted=0 AND (OperationID = " + pOperationID + " OR MasterOperationID = " + pOperationID + ")", " ID ", out _RowCount);
            #endregion vwInvoices Data

            #region vwProfitCurrencies Data
            //Forwarding.MvcApp.Models.Operations.Operations.Customized.CvwProfitCurrencies objCvwProfitCurrencies = new Forwarding.MvcApp.Models.Operations.Operations.Customized.CvwProfitCurrencies();
            //objCvwProfitCurrencies.GetList_Customized(" WHERE (OperationID = " + pOperationID + " OR MasterOperationID = " + pOperationID + ")\n"
            //    + (pReportName.ToUpper() == "OPERATIONSTATEMENT.RPT" ? " AND IsUsedInOperationStatement = 1 " : "") 
            //    + " GROUP BY ChargeTypeName, CurrencyCode ");

            CvwProfitCurrenciesWithMinimalColumns objCvwProfitCurrencies = new CvwProfitCurrenciesWithMinimalColumns();
            CvwProfitCurrenciesWithMinimalColumns_OperationStatement_CL objCvwProfitCurrencies_OperationStatement_CL = new CvwProfitCurrenciesWithMinimalColumns_OperationStatement_CL();

            //objCvwProfitCurrencies.GetListPaging
            //    //(" WHERE IsUsedInOperationStatement = 1  AND (OperationID = " + pOperationID + " OR MasterOperationID = " + pOperationID + ")\n"
            //    (999999, 1, " WHERE (OperationID = " + pOperationID + " OR MasterOperationID = " + pOperationID + ")\n"
            //        + (pReportName.ToUpper() == "OPERATIONSTATEMENT.RPT" ? " AND IsUsedInOperationStatement = 1 " : ""
            //        )
            //    //+ " GROUP BY OperationID,PayablesWithVAT,PayablesWithoutVAT,ReceivablesWithVAT,ReceivablesWithoutVAT,IsUsedInOperationStatement,MasterOperationID,ChargeTypeName,CurrencyCode,OperationCode,ClientName,POLCode,PODCode,ContainerTypes,ActualDeparture,OpenDate,BookingPartyName,BranchID,BranchName,ChargeTypeID,MoveTypeID,ExchangeRate,InvoiceNumbers,FirstInvoiceDate,POValue,PODate,ShipperName,ConsigneeName,MoveTypeName,OperationStageName "
            //    , "ChargeTypeName", out _RowCount
            //    );
            //objCvwProfitCurrencies.GetList(" WHERE (OperationID = " + pOperationID + " OR MasterOperationID=" + pOperationID + ")\n"
            if (pReportName.ToUpper() == "EXPECTEDPROFIT.RPT" || pReportName.ToUpper() == "OPERATIONSTATEMENT.RPT")
            {
                objCvwProfitCurrencies.GetList(" WHERE (OperationID = " + pOperationID + ")\n"
                        + (pReportName.ToUpper() == "OPERATIONSTATEMENT.RPT" ? " AND IsUsedInOperationStatement = 1 " : ""
                        )
                    //+ " GROUP BY OperationID,PayablesWithVAT,PayablesWithoutVAT,ReceivablesWithVAT,ReceivablesWithoutVAT,IsUsedInOperationStatement,MasterOperationID,ChargeTypeName,CurrencyCode,OperationCode,ClientName,POLCode,PODCode,ContainerTypes,ActualDeparture,OpenDate,BookingPartyName,BranchID,BranchName,ChargeTypeID,MoveTypeID,ExchangeRate,InvoiceNumbers,FirstInvoiceDate,POValue,PODate,ShipperName,ConsigneeName,MoveTypeName,OperationStageName "
                    );
                if (pShipmentType == "CONSOL" || objCvwOperations.lstCVarvwOperations[0].BLType == 2)
                    objCvwProfitCurrencies_OperationStatement_CL.GetList(" WHERE (OperationID = " + pOperationID + " OR BillID=" + pOperationID + ")\n"
                           + (pReportName.ToUpper() == "OPERATIONSTATEMENT.RPT" ? " AND IsUsedInOperationStatement = 1 " : ""
                           )
                       //+ " GROUP BY OperationID,PayablesWithVAT,PayablesWithoutVAT,ReceivablesWithVAT,ReceivablesWithoutVAT,IsUsedInOperationStatement,MasterOperationID,ChargeTypeName,CurrencyCode,OperationCode,ClientName,POLCode,PODCode,ContainerTypes,ActualDeparture,OpenDate,BookingPartyName,BranchID,BranchName,ChargeTypeID,MoveTypeID,ExchangeRate,InvoiceNumbers,FirstInvoiceDate,POValue,PODate,ShipperName,ConsigneeName,MoveTypeName,OperationStageName "
                       );
                //else if (objCvwOperations.lstCVarvwOperations[0].BLType == 2) //house
                //    objCvwProfitCurrencies_OperationStatement_CL.GetList(" WHERE (BillID = '" + pOperationID + "')\n"
                //           + (pReportName.ToUpper() == "OPERATIONSTATEMENT.RPT" ? " AND IsUsedInOperationStatement = 1 " : ""
                //           )
                //       );
            }

            //kk

            #region Override ProfitCurrencies in case Rec&Pay selected manually(KDS)
            if (
                    (objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName.ToUpper() == "KDS")
                    && pReportName.ToUpper() == "OPERATIONSTATEMENT.RPT"
                    && pSelectedPayableIDs != "0" && pSelectedReceivableIDs != "0"
                )
            {
                objCvwProfitCurrencies.lstCVarvwProfitCurrenciesWithMinimalColumns.Clear();
                for (int i = 0; i < objCvwPayables.lstCVarvwPayables.Count; i++)
                {
                    CVarvwProfitCurrenciesWithMinimalColumns objCVarvwProfitCurrencies = new CVarvwProfitCurrenciesWithMinimalColumns();
                    objCVarvwProfitCurrencies.ChargeTypeName = objCvwPayables.lstCVarvwPayables[i].ChargeTypeName;
                    objCVarvwProfitCurrencies.QuotationCost = objCvwPayables.lstCVarvwPayables[i].QuotationCost;
                    objCVarvwProfitCurrencies.PayablesWithVAT = objCvwPayables.lstCVarvwPayables[i].CostAmount;
                    objCVarvwProfitCurrencies.PayablesWithoutVAT = objCvwPayables.lstCVarvwPayables[i].AmountWithoutVAT;
                    objCVarvwProfitCurrencies.ReceivablesWithVAT = 0;
                    objCVarvwProfitCurrencies.ReceivablesWithoutVAT = 0;
                    objCVarvwProfitCurrencies.CurrencyCode = objCvwPayables.lstCVarvwPayables[i].CurrencyCode;
                    objCVarvwProfitCurrencies.AccNoteType = objCvwPayables.lstCVarvwPayables[i].AccNoteID != 0 ? "C" : "";
                    objCvwProfitCurrencies.lstCVarvwProfitCurrenciesWithMinimalColumns.Add(objCVarvwProfitCurrencies);
                }
                for (int i = 0; i < objCvwReceivables.lstCVarvwReceivables.Count; i++)
                {
                    CVarvwProfitCurrenciesWithMinimalColumns objCVarvwProfitCurrencies = new CVarvwProfitCurrenciesWithMinimalColumns();
                    objCVarvwProfitCurrencies.ChargeTypeName = objCvwReceivables.lstCVarvwReceivables[i].ChargeTypeName;
                    objCVarvwProfitCurrencies.ReceivablesWithVAT = objCvwReceivables.lstCVarvwReceivables[i].SaleAmount
                        + objCvwReceivables.lstCVarvwReceivables[i].SaleAmount * objCvwReceivables.lstCVarvwReceivables[i].TaxPercentage / 100
                        - objCvwReceivables.lstCVarvwReceivables[i].SaleAmount * objCvwReceivables.lstCVarvwReceivables[i].DiscountPercentage / 100;
                    objCVarvwProfitCurrencies.ReceivablesWithoutVAT = objCvwReceivables.lstCVarvwReceivables[i].SaleAmount;
                    objCVarvwProfitCurrencies.QuotationCost = 0;
                    objCVarvwProfitCurrencies.PayablesWithVAT = 0;
                    objCVarvwProfitCurrencies.PayablesWithoutVAT = 0;
                    objCVarvwProfitCurrencies.CurrencyCode = objCvwReceivables.lstCVarvwReceivables[i].CurrencyCode;
                    objCVarvwProfitCurrencies.AccNoteType = objCvwPayables.lstCVarvwPayables[i].AccNoteID != 0 ? "D" : "";
                    objCvwProfitCurrencies.lstCVarvwProfitCurrenciesWithMinimalColumns.Add(objCVarvwProfitCurrencies);
                }
            } //
            #endregion Override ProfitCurrencies in case Rec&Pay selected manually(KDS)

            var groupedProfitCurrencies = objCvwProfitCurrencies.lstCVarvwProfitCurrenciesWithMinimalColumns.GroupBy(g => new { g.ChargeTypeName, g.CurrencyCode, g.AccNoteType })
                .Select(g => new
                {
                    ChargeTypeName = g.First().ChargeTypeName
                    ,
                    QuotationCost = g.Sum(s => s.QuotationCost)
                    ,
                    PayablesWithVAT = g.Sum(s => s.PayablesWithVAT)
                    ,
                    PayablesWithoutVAT = g.Sum(s => s.PayablesWithoutVAT)
                    ,
                    ReceivablesWithVAT = g.Sum(s => s.ReceivablesWithVAT)
                    ,
                    ReceivablesWithoutVAT = g.Sum(s => s.ReceivablesWithoutVAT)
                    ,
                    CurrencyCode = g.First().CurrencyCode
                    ,
                    AccNoteType = g.First().AccNoteType
                });
            ////////////////////////////////////////////////////////////////////////////////
            //kk 23-12-2019
            var vwProfitCurrencies_OpStatement = objCvwProfitCurrencies_OperationStatement_CL.lstCVarvwProfitCurrenciesWithMinimalColumns_OperationStatement_CL.GroupBy(a => new { a.HouseNumber })
                .Select(a => new
                {
                    QuotationCost = a.Sum(s => s.QuotationCost)
                    ,
                    PayablesWithVAT = a.Sum(s => s.PayablesWithVAT)
                    ,
                    PayablesWithoutVAT = a.Sum(s => s.PayablesWithoutVAT)
                    ,
                    ReceivablesWithVAT = a.Sum(s => s.ReceivablesWithVAT)
                    ,
                    ReceivablesWithoutVAT = a.Sum(s => s.ReceivablesWithoutVAT)
                    ,
                    CurrencyCode = a.First().CurrencyCode
                    ,
                    HouseNumber = a.First().HouseNumber
                });
            
            //var vwProfitCurrencies_OpStatement_Invoice = objCvwProfitCurrencies_OperationStatement_CL.lstCVarvwProfitCurrenciesWithMinimalColumns_OperationStatement_CL.GroupBy(a => new { a.HouseNumber, a.InvoiceNumber, a.CurrencyCode, a.BillID })
            //    .Select(m => new
            //    {
            //        HouseNumber = m.First().HouseNumber
            //        ,
            //        InvoiceReceivableWithVAT = m.First().InvoiceName + "        " + m.First().InvoiceNumber + "        " + Math.Round((m.Sum(s => s.ReceivablesWithVAT)), 2) + "        " + m.First().CurrencyCode
            //        ,
            //        InvoiceReceivableWithoutVAT = m.First().InvoiceName + "        " + m.First().InvoiceNumber + "        " + Math.Round((m.Sum(s => s.ReceivablesWithoutVAT)), 2) + "        " + m.First().CurrencyCode   // m.Sum(s => s.ReceivablesWithoutVAT)
            //         ,
            //        InvoicePayableWithVAT = m.First().InvoiceName + "        " + m.First().InvoiceNumber + "        " + Math.Round((m.Sum(s => s.PayablesWithVAT)), 2) + "        " + m.First().CurrencyCode
            //        ,
            //        InvoicePayableWithoutVAT = m.First().InvoiceName + "        " + m.First().InvoiceNumber + "        " + Math.Round((m.Sum(s => s.PayablesWithoutVAT)), 2) + "        " + m.First().CurrencyCode   // m.Sum(s => s.ReceivablesWithoutVAT)
            //        ,
            //        BillID = m.First().BillID
            //    });
            var vwProfitCurrencies_OpStatement_Invoice = objCvwProfitCurrencies_OperationStatement_CL.lstCVarvwProfitCurrenciesWithMinimalColumns_OperationStatement_CL.GroupBy(a => new { a.HouseNumber, a.InvoiceNumber, a.CurrencyCode, a.BillID })
                .Select(m => new
                {
                    HouseNumber = m.First().HouseNumber
                    ,
                    InvoiceReceivableWithVAT = m.First().InvoiceName + "        " + Math.Round((m.Sum(s => s.ReceivablesWithVAT)), 2) + "        " + m.First().CurrencyCode
                    ,
                    InvoiceReceivableWithoutVAT = m.First().InvoiceName + "        " + Math.Round((m.Sum(s => s.ReceivablesWithoutVAT)), 2) + "        " + m.First().CurrencyCode   // m.Sum(s => s.ReceivablesWithoutVAT)
                     ,
                    QuotationCost = Math.Round((m.Sum(s => s.QuotationCost)), 2) + "        " + m.First().CurrencyCode
                    ,
                    InvoicePayableWithVAT = Math.Round((m.Sum(s => s.PayablesWithVAT)), 2) + "        " + m.First().CurrencyCode
                    ,
                    InvoicePayableWithoutVAT = Math.Round((m.Sum(s => s.PayablesWithoutVAT)), 2) + "        " + m.First().CurrencyCode   // m.Sum(s => s.ReceivablesWithoutVAT)
                    ,
                    BillID = m.First().BillID
                });
            var vwProfitCurrencies_OpStatement_OnlyMaster = objCvwProfitCurrencies_OperationStatement_CL
                .lstCVarvwProfitCurrenciesWithMinimalColumns_OperationStatement_CL.Where(s => s.BillID == 0)
                .GroupBy(a => new { a.CurrencyCode })
                .Select(a => new
                {
                    QuotationCost = Math.Round(a.Sum(s => s.QuotationCost)) + "        " + a.First().CurrencyCode
                    ,
                    PayablesWithVAT = Math.Round(a.Sum(s => s.PayablesWithVAT)) + "        " + a.First().CurrencyCode
                    ,
                    PayablesWithoutVAT = Math.Round(a.Sum(s => s.PayablesWithoutVAT)) + "        " + a.First().CurrencyCode
                    ,
                    ReceivablesWithVAT = a.First().InvoiceName + "        " + Math.Round(a.Sum(s => s.ReceivablesWithVAT)) + "        " + a.First().CurrencyCode
                    ,
                    ReceivablesWithoutVAT = a.First().InvoiceName + "        " + Math.Round(a.Sum(s => s.ReceivablesWithoutVAT)) + "        " + a.First().CurrencyCode
                    ,
                    CurrencyCode = a.First().CurrencyCode
                });
            //////////////////////////////////////////////////////////

            string pProfitCurrenciesSubtotal = "";
            string pDebitCurrenciesSubtotal = "";
            string pCreditCurrenciesSubtotal = "";
            string pPayablesCurrenciesSubtotal = "";
            string pReceivablesOrInvoicesCurrenciesSubtotal = "";
            decimal pPayablesInDefaultCurrency = 0;
            decimal pReceivablesInDefaultCurrency = 0;
            decimal pFixedDiscountInDefaultCurrency = 0;
            decimal pCreditInDefaultCurrency = 0;
            decimal pDebitInDefaultCurrency = 0;
            decimal pProfitInDefaultCurrency = 0;
            #endregion
            #region TruckingOrder
            var pTruckingOrderClientName = pDirectionType == "IMPORT"
                ? (pConsigneeLocalName == "" ? pConsigneeName : pConsigneeLocalName)
                : (pShipperLocalName == "" ? pShipperLocalName : pShipperLocalName);
            var pTruckingOrderClientAddress = pDirectionType == "IMPORT"
                ? pConsigneeAddressLine1 + (pConsigneeAddressLine2 == "" ? "" : (", " + pConsigneeAddressLine2))
                : pShipperAddressLine1 + (pShipperAddressLine2 == "" ? "" : (", " + pShipperAddressLine2));
            var pTruckingOrderClientContactName = pDirectionType == "IMPORT"
                ? pConsigneeContactName
                : pShipperContactName;
            var pTruckingOrderGensetSupplierName = "N/A";
            var pTruckingOrderCCAName = "N/A";
            var pTruckingOrderQuantity = "N/A";
            var pTruckingOrderContactPerson = "";
            var pTruckingOrderPickupAddress = "";
            var pTruckingOrderDeliveryAddress = "";
            var pTruckingOrderGateInPortName = "";
            var pTruckingOrderGateOutPortName = "";
            var pTruckingOrderGateInDate = "";
            var pTruckingOrderGateOutDate = "";
            var pTruckingOrderStuffingDate = "";
            var pTruckingOrderDeliveryDate = "";
            var pTruckingOrderBookingNumber = "";
            var pTruckingOrderDelays = "";
            var pTruckingOrderPowerFromGateInTillActualSailing = "";
            var pTruckingOrderTruckerName = "N/A";
            var pTruckingOrderContactPersonPhones = "";
            var pTruckingOrderLoadingTime = "";
            var pTruckingOrderNotes = "";

            CvwRoutings objCTruckingOrder = new CvwRoutings();
            objCTruckingOrder.GetListPaging(9999, 1, " WHERE OperationID IN (" + pOperationID.ToString() + "," + objCvwOperations.lstCVarvwOperations[0].MasterOperationID + ") AND RoutingTypeID = " + TruckingOrderRoutingTypeID.ToString()
                                        + (pTruckingOrderID == 0 ? "" : " AND ID=" + pTruckingOrderID.ToString())
            , "ID", out _RowCount);
            if (objCTruckingOrder.lstCVarvwRoutings.Count > 0)
            {
                pTruckingOrderGensetSupplierName = objCTruckingOrder.lstCVarvwRoutings[0].GensetSupplierLocalName == "0" ? "N/A" : objCTruckingOrder.lstCVarvwRoutings[0].GensetSupplierLocalName;
                pTruckingOrderCCAName = objCTruckingOrder.lstCVarvwRoutings[0].CCALocalName == "0" ? "N/A" : objCTruckingOrder.lstCVarvwRoutings[0].CCALocalName;
                pTruckingOrderQuantity = objCTruckingOrder.lstCVarvwRoutings[0].Quantity == "0" ? "N/A" : objCTruckingOrder.lstCVarvwRoutings[0].Quantity;
                pTruckingOrderContactPerson = objCTruckingOrder.lstCVarvwRoutings[0].ContactPerson == "0" ? "N/A" : objCTruckingOrder.lstCVarvwRoutings[0].ContactPerson;
                pTruckingOrderPickupAddress = objCTruckingOrder.lstCVarvwRoutings[0].PickupAddress == "0" ? "N/A" : objCTruckingOrder.lstCVarvwRoutings[0].PickupAddress;
                pTruckingOrderDeliveryAddress = objCTruckingOrder.lstCVarvwRoutings[0].DeliveryAddress == "0" ? "N/A" : objCTruckingOrder.lstCVarvwRoutings[0].DeliveryAddress;
                pTruckingOrderGateInPortName = objCTruckingOrder.lstCVarvwRoutings[0].GateInPortName == "0" ? "N/A" : objCTruckingOrder.lstCVarvwRoutings[0].GateInPortName;
                pTruckingOrderGateOutPortName = objCTruckingOrder.lstCVarvwRoutings[0].GateOutPortName == "0" ? "N/A" : objCTruckingOrder.lstCVarvwRoutings[0].GateOutPortName;
                pTruckingOrderGateInDate = objCTruckingOrder.lstCVarvwRoutings[0].GateInDate;
                pTruckingOrderGateOutDate = objCTruckingOrder.lstCVarvwRoutings[0].GateOutDate;
                pTruckingOrderStuffingDate = objCTruckingOrder.lstCVarvwRoutings[0].StuffingDate;
                pTruckingOrderDeliveryDate = objCTruckingOrder.lstCVarvwRoutings[0].DeliveryDate;
                pTruckingOrderBookingNumber = objCTruckingOrder.lstCVarvwRoutings[0].BookingNumber == "0" ? "N/A" : objCTruckingOrder.lstCVarvwRoutings[0].BookingNumber;
                pTruckingOrderDelays = objCTruckingOrder.lstCVarvwRoutings[0].Delays == "0" ? "N/A" : objCTruckingOrder.lstCVarvwRoutings[0].Delays;
                pTruckingOrderPowerFromGateInTillActualSailing = objCTruckingOrder.lstCVarvwRoutings[0].PowerFromGateInTillActualSailing == "0" ? "N/A" : objCTruckingOrder.lstCVarvwRoutings[0].PowerFromGateInTillActualSailing;
                pTruckingOrderTruckerName = objCTruckingOrder.lstCVarvwRoutings[0].TruckerLocalName == "0" ? "N/A" : objCTruckingOrder.lstCVarvwRoutings[0].TruckerName;
                pTruckingOrderContactPersonPhones = objCTruckingOrder.lstCVarvwRoutings[0].ContactPersonPhones == "0" ? "N/A" : objCTruckingOrder.lstCVarvwRoutings[0].ContactPersonPhones;
                pTruckingOrderLoadingTime = objCTruckingOrder.lstCVarvwRoutings[0].LoadingTime == "0" ? "N/A" : objCTruckingOrder.lstCVarvwRoutings[0].LoadingTime;
                pTruckingOrderNotes = objCTruckingOrder.lstCVarvwRoutings[0].Notes == "0" ? "N/A" : objCTruckingOrder.lstCVarvwRoutings[0].Notes;

                //pCCAPhones//pCCAContactName
                if (objCTruckingOrder.lstCVarvwRoutings[0].CCAID > 0)
                {
                    CContacts objCTruckingOrderCCAContacts = new CContacts();
                    objCTruckingOrderCCAContacts.GetList("WHERE PartnerTypeID=4 AND PartnerID=" + objCTruckingOrder.lstCVarvwRoutings[0].CCAID.ToString());
                    if (objCTruckingOrderCCAContacts.lstCVarContacts.Count > 0)
                    {
                        pCCAContactName = (objCTruckingOrderCCAContacts.lstCVarContacts[0].Name == "0" ? "" : objCTruckingOrderCCAContacts.lstCVarContacts[0].Name);
                        pCCAPhones = (objCTruckingOrderCCAContacts.lstCVarContacts[0].Phone1 == "0" ? "" : objCTruckingOrderCCAContacts.lstCVarContacts[0].Phone1)
                            + "   " + (objCTruckingOrderCCAContacts.lstCVarContacts[0].Phone2 == "0" ? "" : objCTruckingOrderCCAContacts.lstCVarContacts[0].Phone2)
                            + "   " + (objCTruckingOrderCCAContacts.lstCVarContacts[0].Mobile1 == "0" ? "" : objCTruckingOrderCCAContacts.lstCVarContacts[0].Mobile1);
                    }
                }
            }
            #endregion TruckingOrder

            #region return object to the fn according to Document Type
            #region OperationCover/OperationStatement/ExpectedProfit/OperationSummary/ProofOfDelivery
            if (pReportName.ToUpper() == "OPERATIONCOVER.RPT" || pReportName.ToUpper() == "OPERATIONSUMMARY.RPT" || pReportName.ToUpper() == "EXPECTEDPROFIT.RPT" || pReportName.ToUpper() == "OPERATIONSTATEMENT.RPT" || pReportName.ToUpper() == "PROOFOFDELIVERY.RPT")
            {
                #region Checking mandatoryfields for printing this reports
                //if (objCvwProfitCurrencies.lstCVarvwProfitCurrencies.Count == 0 )
                //    if (pEnCompanyName.Replace(" ", "").ToLower() == "fastfreightinternational" //check for all cases of fast freight coz they don't have operation cover with : notes
                //        || (pReportName.ToUpper() != "OPERATIONCOVER.RPT" && pEnCompanyName.Replace(" ", "").ToLower() != "fastfreightinternational")//for non-fastfreight companies exclude operation cover from check
                //        ) //Fast Freight Expected Profit is switched with OperationCover
                if (objCvwProfitCurrencies.lstCVarvwProfitCurrenciesWithMinimalColumns.Count == 0 && objCvwProfitCurrencies_OperationStatement_CL.lstCVarvwProfitCurrenciesWithMinimalColumns_OperationStatement_CL.Count == 0
                    && pReportName.ToUpper() != "OPERATIONCOVER.RPT" && pReportName.ToUpper() != "OPERATIONSUMMARY.RPT" && pReportName.ToUpper() != "PROOFOFDELIVERY.RPT")
                {
                    RecordsExist = false;
                    MissingMandatoryFields = "No records are found.";
                }
                #endregion Checking mandatoryfields for printing this reports
                #region ProfitCurrenciesSubtotal
                var objProfitCurrenciesSubtotal = objCvwProfitCurrencies.lstCVarvwProfitCurrenciesWithMinimalColumns
                    .GroupBy(d => d.CurrencyCode)
                                                   .Select(g => new
                                                   {
                                                       Profit = g.Sum(s => (pWithVAT
                                                                            ? (s.ReceivablesWithVAT - s.PayablesWithVAT)
                                                                            : (s.ReceivablesWithoutVAT - s.PayablesWithoutVAT))),
                                                       Currency = g.First().CurrencyCode
                                                   });
                foreach (var row in objProfitCurrenciesSubtotal)
                {
                    pProfitCurrenciesSubtotal +=
                        (pProfitCurrenciesSubtotal == ""
                        ? (Decimal.Round(row.Profit, 2) + " " + row.Currency)
                        : (", " + Decimal.Round(row.Profit, 2) + " " + row.Currency));
                }
                #endregion ProfitCurrenciesSubtotal
                #region PayablesCurrenciesSubtotal
                var objPayablesCurrenciesSubtotal = objCvwProfitCurrencies.lstCVarvwProfitCurrenciesWithMinimalColumns.GroupBy(d => d.CurrencyCode)
                                                   .Select(g => new
                                                   {
                                                       PayablesSubtotals = g.Sum(s => (pWithVAT ? s.PayablesWithVAT : s.PayablesWithoutVAT)),
                                                       Currency = g.First().CurrencyCode
                                                   });
                foreach (var row in objPayablesCurrenciesSubtotal)
                {
                    pPayablesCurrenciesSubtotal +=
                        (pPayablesCurrenciesSubtotal == ""
                        ? (Decimal.Round(row.PayablesSubtotals, 2) + " " + row.Currency)
                        : (", " + Decimal.Round(row.PayablesSubtotals, 2) + " " + row.Currency));
                }

                var groupedPayablesInDefaultCurrency = objCvwPayables.lstCVarvwPayables.GroupBy(g => g.CurrencyID)
                            .Select(g => new
                            {
                                Amount = g.Sum(s => ((pWithVAT ? s.CostAmount : s.AmountWithoutVAT) * s.ExchangeRate))
                            });
                foreach (var row in groupedPayablesInDefaultCurrency)
                    pPayablesInDefaultCurrency += row.Amount;
                #endregion PayablesCurrenciesSubtotal
                #region CreditCurrenciesSubtotal
                var objCreditCurrenciesSubtotal = objCvwProfitCurrencies.lstCVarvwProfitCurrenciesWithMinimalColumns.GroupBy(d => d.CurrencyCode)
                                                   .Select(g => new
                                                   {
                                                       CreditSubtotals = g.Sum(s => (s.AccNoteType == "" ? 0 : (pWithVAT ? s.PayablesWithVAT : s.PayablesWithoutVAT))),
                                                       Currency = g.First().CurrencyCode
                                                   });
                foreach (var row in objCreditCurrenciesSubtotal)
                {
                    pCreditCurrenciesSubtotal +=
                        (pCreditCurrenciesSubtotal == ""
                        ? (Decimal.Round(row.CreditSubtotals, 2) + " " + row.Currency)
                        : (", " + Decimal.Round(row.CreditSubtotals, 2) + " " + row.Currency));
                }
                var groupedCreditInDefaultCurrency = objCvwPayables.lstCVarvwPayables.GroupBy(g => g.CurrencyID)
                            .Select(g => new
                            {
                                Amount = g.Sum(s => ((s.AccNoteID == 0 || s.IsDeleted ? 0 : (pWithVAT ? s.CostAmount : s.AmountWithoutVAT) * s.ExchangeRate)))
                            });
                foreach (var row in groupedCreditInDefaultCurrency)
                    pCreditInDefaultCurrency += row.Amount;
                #endregion CreditCurrenciesSubtotal
                #region DebitCurrenciesSubtotal
                var objDebitCurrenciesSubtotal = objCvwProfitCurrencies.lstCVarvwProfitCurrenciesWithMinimalColumns.GroupBy(d => d.CurrencyCode)
                                                   .Select(g => new
                                                   {
                                                       DebitSubtotals = g.Sum(s => (s.AccNoteType == "" ? 0 : (pWithVAT ? s.ReceivablesWithVAT : s.ReceivablesWithoutVAT))),
                                                       Currency = g.First().CurrencyCode
                                                   });
                foreach (var row in objDebitCurrenciesSubtotal)
                {
                    pDebitCurrenciesSubtotal +=
                        (pDebitCurrenciesSubtotal == ""
                        ? (Decimal.Round(row.DebitSubtotals, 2) + " " + row.Currency)
                        : (", " + Decimal.Round(row.DebitSubtotals, 2) + " " + row.Currency));
                }

                var groupedDebitInDefaultCurrency = objCvwReceivables.lstCVarvwReceivables.GroupBy(g => g.CurrencyID)
                            .Select(g => new
                            {
                                Amount = g.Sum(s =>
                                (s.AccNoteID == 0 || s.IsDeleted ? 0
                                    : (
                                            pWithVAT
                                            //? (s.AmountWithoutVAT + s.AmountWithoutVAT * s.TaxPercentage / 100 - s.AmountWithoutVAT * s.DiscountPercentage / 100) * s.ExchangeRate
                                            ? s.SaleAmount * s.ExchangeRate
                                            : s.AmountWithoutVAT * s.ExchangeRate
                                       )
                                )
                            )
                            });
                foreach (var row in groupedDebitInDefaultCurrency)
                    pDebitInDefaultCurrency += row.Amount;
                #endregion DebitCurrenciesSubtotal
                #region ReceivablesOrInvoicesCurrenciesSubtotal
                var objReceivablesOrInvoicesCurrenciesSubtotal = objCvwProfitCurrencies.lstCVarvwProfitCurrenciesWithMinimalColumns.GroupBy(d => d.CurrencyCode)
                                                   .Select(g => new
                                                   {
                                                       ReceivablesOrInvoicesSubtotals = g.Sum(s => (pWithVAT ? s.ReceivablesWithVAT : s.ReceivablesWithoutVAT)),
                                                       Currency = g.First().CurrencyCode
                                                   });
                foreach (var row in objReceivablesOrInvoicesCurrenciesSubtotal)
                {
                    pReceivablesOrInvoicesCurrenciesSubtotal +=
                        (pReceivablesOrInvoicesCurrenciesSubtotal == ""
                        ? (Decimal.Round(row.ReceivablesOrInvoicesSubtotals, 2) + " " + row.Currency)
                        : (", " + Decimal.Round(row.ReceivablesOrInvoicesSubtotals, 2) + " " + row.Currency));
                }

                var groupedReceivablesInDefaultCurrency = objCvwReceivables.lstCVarvwReceivables.GroupBy(g => g.CurrencyID)
                            .Select(g => new
                            {
                                Amount = g.Sum(s => (
                                    pWithVAT
                                    //? (s.AmountWithoutVAT + s.AmountWithoutVAT * s.TaxPercentage / 100 - s.AmountWithoutVAT * s.DiscountPercentage / 100) * s.ExchangeRate
                                    ? s.SaleAmount * s.ExchangeRate
                                    : s.AmountWithoutVAT * s.ExchangeRate
                                                    )
                                              )
                            });
                foreach (var row in groupedReceivablesInDefaultCurrency)
                    pReceivablesInDefaultCurrency += row.Amount;

                //var groupedFixedDiscountInDefaultCurrency = objCInvoices.lstCVarInvoices.GroupBy(g => g.CurrencyID)
                //            .Select(g => new
                //            {
                //                Amount = g.Sum(s => (s.FixedDiscount * s.FixedDiscount))
                //            });
                
                var groupedFixedDiscountInDefaultCurrency = objCvwProfitCurrencies.lstCVarvwProfitCurrenciesWithMinimalColumns
                    .Where(w => w.ChargeTypeName == "Special Discount")
                    .GroupBy(g => g.CurrencyCode)
                    .Select(g => new
                    {
                        Amount = g.Sum(s => (s.ReceivablesWithoutVAT * s.ExchangeRate)) //pFixedDiscountInDefaultCurrency is -ve value
                    });

                foreach (var row in groupedFixedDiscountInDefaultCurrency)
                    pFixedDiscountInDefaultCurrency += row.Amount;
                #endregion ReceivablesOrInvoicesCurrenciesSubtotal

                pReceivablesInDefaultCurrency += pFixedDiscountInDefaultCurrency; //to subtract the special discount

                pProfitInDefaultCurrency = decimal.Round((pReceivablesInDefaultCurrency - pPayablesInDefaultCurrency), 2); //(pFixedDiscountInDefaultCurrency is -ve value)
                //strExportedFileName = ExportReport(objCvwProfitCurrencies.lstCVarvwProfitCurrencies, pReportName, pReportNameWithoutExtension, pReportTypeID
                //    , new Object[] { pOperationCode, pDirectionType, pShipmentType, pTransportType, pOpenDate, pClientName, pContainerTypes
                //    , pCarrier, pCommodityName, pPOL, pPOD, pVesselName, pSalesman, pAgentName, pIncotermName, pActualDeparture, pActualArrival
                //    , pMasterBL, pHouseNumber, pContainerNumbers, pInvoiceNumbers, pDocumentISOCode, pIsPrintISOCode, pGrossWeightSum, pUserName }); //ParameterFields

                return new object[] { RecordsExist, MissingMandatoryFields, pBranchName, pOperationCode, Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pOpenDate)
                , pClientName, pVolumeSum, pSalesman, pCommodityName, pGrossWeightSum, pPOLCodeAndName, pPODCodeAndName, pLineName, pAgentName
                , pIncotermName, Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pActualArrival), Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pActualDeparture), pMasterBL
                , pVesselName
                //, new JavaScriptSerializer().Serialize(objCvwProfitCurrencies.lstCVarvwProfitCurrencies)
                , objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName == "DGL"
                    ? new JavaScriptSerializer().Serialize(objCvwProfitCurrencies.lstCVarvwProfitCurrenciesWithMinimalColumns)
                    : new JavaScriptSerializer().Serialize(groupedProfitCurrencies)
                , pHouseBLs, pHouseClients, pInvoiceNumbers, pContainerNumbers, pClientEmail, pContainerTypes, pPackageTypes, pShipmentType
                , pProfitCurrenciesSubtotal, pHouseNumber, pRepBLTypeShown, pMasterOperationCode, pCCAName,pTruckerName
                , pPOLName, pPODName, pPayablesCurrenciesSubtotal, pReceivablesOrInvoicesCurrenciesSubtotal, pVGMSum
                , Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pExpectedDeparture)
                , Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pExpectedArrival)
                , pVoyageOrTruckNumber, pPOrC, pProfitInDefaultCurrency, pPayablesInDefaultCurrency, pReceivablesInDefaultCurrency
                , pPickupAddress, pDeliveryAddress, pShipperName, pBLDate.ToShortDateString(), pSupplierName
                , pIsDelivered, pIsTrucking,pIsInsurance, pIsClearance, pIsGenset, pTruckingOrderGateOutDate, pTruckingOrderStuffingDate,pTruckingOrderGateInDate
                , pTruckingOrderDelays, pTruckingOrderPowerFromGateInTillActualSailing, pTruckingOrderTruckerName, pTruckingOrderGensetSupplierName
                , pIsCourrier, "This was the pIsOBL field" /*IsOBL*/, pIsTelexRelease, Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pETAPOLDate)
                , Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pATAPOLDate)
                , pConsigneeName, pConsigneeContactName, pConsigneePhones, pConsigneeFax, pConsigneeEmail
                , pConsigneeAddressLine1, pConsigneeAddressLine2, pConsigneeCityName, pConsigneeCountryName
                , pClientAddressLine1, pClientAddressLine2, pClientCityName, pClientCountryName, pClientPhones, pClientFax
                , pShipperAddressLine1, pShipperAddressLine2, pShipperCityName, pShipperCountryName, pShipperPhones, pShipperFax, pShipperEmail
                , pNotify1Name, pNotify1ContactName, pNotify1Phones ,pNotify1Fax, pNotify1Email, pNotify1AddressLine1, pNotify1AddressLine2, pNotify1CityName, pNotify1CountryName
                , pDescriptionOfGoods, pRoadOrContainerNumber, pNumberOfPackages, pCustomerReference, pEnCompanyName
                , pAddressLine1, pAddressLine2, pAddressLine3, pAddressLine4, pAddressLine5, pPhones, pFaxes, pEmail, pWebsite
                , pAgentAddressLine1, pAgentAddressLine2, pAgentCityName, pAgentCountryName, pAgentPhones, pAgentFax, pMoveTypeName
                , new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations[0])
                , objCvwRoutings.lstCVarvwRoutings.Count > 0 ? new JavaScriptSerializer().Serialize(objCvwRoutings.lstCVarvwRoutings[0]) : null

                //kk 23-12-2019
                , new JavaScriptSerializer().Serialize(objCvwPayables.lstCVarvwPayables)//122
                , new JavaScriptSerializer().Serialize(objCvwReceivables.lstCVarvwReceivables)//123
                ,new JavaScriptSerializer().Serialize(vwProfitCurrencies_OpStatement)//124
                ,new JavaScriptSerializer().Serialize(objCvwProfitCurrencies_OperationStatement_CL.lstCVarvwProfitCurrenciesWithMinimalColumns_OperationStatement_CL.OrderBy(x => x.BillID).ThenBy(a => a.InvoiceNumber))//125
                ,new JavaScriptSerializer().Serialize(vwProfitCurrencies_OpStatement_Invoice.OrderBy(x => x.BillID))//126
                ,objCvwOperations.lstCVarvwOperations[0].BLType == 2 //house
                    ? new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations)
                    : new JavaScriptSerializer().Serialize(objCvwHouses.lstCVarvwOperations.OrderBy(a => a.ID))//127 //Never Change Order from here because kelany depends on it in operation statement
                ,new JavaScriptSerializer().Serialize(vwProfitCurrencies_OpStatement_OnlyMaster.ToList())//128
                , pCreditCurrenciesSubtotal, pCreditInDefaultCurrency
                , pDebitCurrenciesSubtotal, pDebitInDefaultCurrency
                };

            }
            #endregion OperationCover/OperationStatement/ExpectedProfit/OperationSummary/ProofOfDelivery
            #region PreShippingDeclaration And Final-ShippingDeclaration Reports
            else if (pReportName.ToUpper() == "PRESHIPPINGDECLARATION.RPT" || pReportName.ToUpper() == "SHIPPINGDECLARATION.RPT" || pReportName.ToUpper() == "DANGEROUSSHIPPINGDECLARATIONAIR.RPT")
            {
                #region Checking mandatoryfields for printing this reports
                if (pShipperName == "")
                    MissingMandatoryFields += (MissingMandatoryFields == "" ? "Shipper Name" : ", Shipper Name");
                if (pConsigneeName == "")
                    MissingMandatoryFields += (MissingMandatoryFields == "" ? "Consignee Name" : ", Consignee Name");
                //if (pActualDeparture.Equals(DateTime.Parse("01-01-1900")))
                //    MissingMandatoryFields += (MissingMandatoryFields == "" ? "Actual Departure Date" : ", Actual Departure Date");
                if (pPOL == "")
                    MissingMandatoryFields += (MissingMandatoryFields == "" ? "POL" : ", POL");
                if (pPOD == "")
                    MissingMandatoryFields += (MissingMandatoryFields == "" ? "POD" : ", POD");
                if (pReportName.ToUpper() == "SHIPPINGDECLARATION.RPT") //i have more mandatory fields in the final shipping declaration
                {
                    if (pTransportType == "OCEAN")
                        if (pVesselName == "")
                            MissingMandatoryFields += (MissingMandatoryFields == "" ? "Vessel Name" : ", Vessel Name");
                    if (objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages.Count < 1)
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "No Containers or Packages found" : ", No Containers or Packages found");
                }
                #endregion Checking mandatoryfields for printing this reports
                // to set @IsPreShipping
                var pIsPreShipping = (pReportName.ToUpper() == "PRESHIPPINGDECLARATION.RPT" ? true : false);
                //i am using 1 report for both PreShippingDeclaration and ShippingDeclaration so i called the report name ShippingDeclaration
                pReportName = "ShippingDeclaration.rpt";
                pReportNameWithoutExtension = "ShippingDeclaration"; //used to name the file to be exported by adding date and time to the file name then the extension
                if (MissingMandatoryFields == "")
                    //strExportedFileName = ExportReport(objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages, pReportName, pReportNameWithoutExtension, pReportTypeID
                    //, new Object[] { pIsPreShipping, pEnCompanyName, pArCompanyName, pPOrC, pDocumentISOCode, pIsPrintISOCode, pAddressLine1, pAddressLine2, pAddressLine3, pAddressLine4, pAddressLine5
                    //, pPhones, pFaxes, pEmail, pWebsite
                    //, pShipperName, pShipperContactName, pShipperPhones, pShipperFax, pShipperEmail
                    //, pConsigneeName, pConsigneeContactName, pConsigneePhones, pConsigneeFax, pConsigneeEmail
                    //, pNotify1Name, pNotify1ContactName, pNotify1Phones ,pNotify1Fax, pNotify1Email
                    //, pVoyageOrTruckNumber, pVesselName, pTransportType, pActualDeparture, pPOL, pPOD
                    //, pShipmentType, pContainerTypes }); //ParameterFields;
                    return new object[] { RecordsExist, MissingMandatoryFields, pOperationCode, pShipperName
                            , pShipperContactName, pShipperPhones, pShipperFax, pShipperEmail
                            , pConsigneeName, pConsigneeContactName, pConsigneePhones, pConsigneeFax, pConsigneeEmail
                            , pNotify1Name, pNotify1ContactName, pNotify1Phones ,pNotify1Fax, pNotify1Email
                            , pLineName, pVoyageOrTruckNumber, pVesselName, /*pActualDeparture.ToShortDateString()*/Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pActualDeparture), pPOLCodeAndName, pPODCodeAndName
                            , pEnCompanyName, pArCompanyName, pPOrC, pAddressLine1, pAddressLine2, pAddressLine3, pAddressLine4, pAddressLine5
                            , new JavaScriptSerializer().Serialize(objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages)
                            , pPhones, pFaxes, pEmail, pWebsite, pClientEmail, pContainerTypes, pPackageTypes, pShipmentType, pGrossWeightSum
                            , pDescriptionOfGoods, pRoadOrContainerNumber, pPackageTypesOnContainersTotals
                            , pShipperAddressLine1, pShipperAddressLine2, pShipperCityName, pShipperCountryName
                            , pConsigneeAddressLine1, pConsigneeAddressLine2, pConsigneeCityName, pConsigneeCountryName
                            , pNotify1AddressLine1, pNotify1AddressLine2, pNotify1CityName, pNotify1CountryName
                            , pNetWeightSum, pVGMSum, pDeliveryCountryName, pDeliveryCityName, pTruckingOrderBookingNumber
                            , Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pExpectedArrival)
                            , pVolumeSum
                            , Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pExpectedDeparture)
                            , Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pETAPOLDate)
                            , pMasterOperationCode, pCommodityName, pAgreedRate
                            , new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations[0])
                            , pNotify2Name, pNotify2ContactName, pNotify2Phones ,pNotify2Fax, pNotify2Email
                            , pNotify2AddressLine1, pNotify2AddressLine2, pNotify2CityName, pNotify2CountryName
                        };
                else
                {
                    RecordsExist = false;
                    MissingMandatoryFields = "You are missing " + MissingMandatoryFields + ".";
                    return new object[] { RecordsExist, MissingMandatoryFields, strExportedFileName };
                }
            }
            #endregion PreShippingDeclaration And ShippingDeclaration Reports
            #region ArrivalNotice Reports
            else if ((pReportName.ToUpper() == "ARRIVALNOTICE.RPT" && objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName != "DYN") 
                    || pReportName.ToUpper() == "AIRLABEL.RPT")
            {
                return new object[] { RecordsExist, MissingMandatoryFields, pBranchName/*i got it from DB for the case he changed it in operation w/o saving*/
                        , pEnCompanyName, pArCompanyName, pContainerNumbers, pGrossWeightSum, pVolumeSum
                        , pVesselName, pPOLCodeAndName, pPODCodeAndName, pLineName, Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pExpectedArrival), pClientName, pClientEmail
                        , objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages.Count > 0 ? objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages[0].DescriptionOfGoods : pDescriptionOfGoods
                        , Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pActualArrival)
                        , pMasterBL
                        , pShipperName, pShipperContactName, pShipperPhones, pShipperFax, pShipperEmail
                        , pConsigneeName, pConsigneeContactName, pConsigneePhones, pConsigneeFax, pConsigneeEmail
                        , pNotify1Name, pNotify1ContactName, pNotify1Phones ,pNotify1Fax, pNotify1Email
                        , pShipperAddressLine1, pShipperAddressLine2, pShipperCityName, pShipperCountryName
                        , pConsigneeAddressLine1, pConsigneeAddressLine2, pConsigneeCityName, pConsigneeCountryName
                        , pNotify1AddressLine1, pNotify1AddressLine2, pNotify1CityName, pNotify1CountryName
                        , pCommodityName, pCustomerReference, pCCAName, pNumberOfPackages, pVGMSum, pDeliveryCountryName, pDeliveryCityName, pTruckingOrderBookingNumber
                        , Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pExpectedDeparture), pVoyageOrTruckNumber
                        , Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pActualDeparture)
                        , pMasterOperationCode
                        , new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations[0])
                        , new JavaScriptSerializer().Serialize(objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages)
                };
            }
            #endregion ArrivalNotice Reports
            #region PaymentRequests Reports
            else if (pReportName.ToUpper() == "PAYMENTREQUESTS.RPT" || pReportName.ToUpper() == "OPERATIONPAYABLES.RPT")
            {
                #region Checking mandatoryfields for printing this reports
                //if (objCvwPaymentRequests.lstCVarvwPaymentRequests.Count == 0)
                //{
                //    RecordsExist = false;
                //    MissingMandatoryFields = "No payables are found.";
                //}


                #region Checking mandatoryfields for LAT
                //else 
                if (MissingMandatoryFields == "" && objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName == "LAT")
                {
                    if (pMasterBL == "N/A" && pTransportType != "INLAND")
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "MBL Number" : ", MBL Number");
                    if (pContainerTypes == "N/A" && pPackageTypes == "N/A")
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Cargo" : ", Cargo");
                    if (pCommodityName == "N/A")
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Commodity" : ", Commodity");
                    if (pPOrC == "0")
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Freight type (P/C)" : ", Freight type (P/C)");
                    if (pIncotermName == "N/A")
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Incoterm" : ", Incoterm");
                    if (pGrossWeightSum == 0)
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Gross weight" : ", Gross weight");
                    if (pMoveTypeName == "0" && pRepBLTypeShown != "MASTER")
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Service scope" : ", Service scope");
                    if (pVesselName == "N/A" && pTransportType == "OCEAN")
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Vessel" : ", Vessel");
                    if (pVoyageOrTruckNumber == "N/A" && pTransportType == "OCEAN")
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Voyage number" : ", Voyage number");
                    if (pActualDeparture.Equals(DateTime.Parse("01-01-1900")) && pTransportType != "INLAND")
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Actual departure date" : ", Actual departure date");
                }
                #endregion Checking mandatoryfields for LAT
                if (MissingMandatoryFields == "")
                    RecordsExist = true;
                else
                {
                    RecordsExist = false;
                    MissingMandatoryFields = "You are missing " + MissingMandatoryFields + ".";
                }

                #endregion Checking mandatoryfields for printing this reports
                //strExportedFileName = ExportReport(objCvwPayables.lstCVarvwPayables, pReportName, pReportNameWithoutExtension, pReportTypeID
                //    , new Object[] { pIsPrintISOCode, pDocumentISOCode, pBranchName, pEnCompanyName, pOperationCode
                //    , pDirectionType, pShipmentType, pTransportType, pClientName }); //ParameterFields
                return new object[] { RecordsExist, MissingMandatoryFields, pQuotationCode, new JavaScriptSerializer().Serialize(objCvwPaymentRequests.lstCVarvwPaymentRequests)
                    , pPOLCodeAndName, pPODCodeAndName, pVolumeSum, pLineName, Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pExpectedDeparture)
                    , pTransientTime, pCommodityName, pSalesman, pOperationCode, pClientEmail, objCvwPaymentRequests.lstCVarvwPaymentRequests.Count, pLineBankName, pLineBankAccountNumber
                    , new JavaScriptSerializer().Serialize(objCvwPayablesSubTotals.lstCVarvwPayablesSubTotals)
                    , pContainerTypes, pPackageTypes, new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations[0])
                    , objCvwRoutings.lstCVarvwRoutings.Count > 0 ? new JavaScriptSerializer().Serialize(objCvwRoutings.lstCVarvwRoutings[0]) : null
                    , objCvwRoutingsMaster.lstCVarvwRoutings.Count > 0 ? new JavaScriptSerializer().Serialize(objCvwRoutingsMaster.lstCVarvwRoutings[0]) : null
                    , new JavaScriptSerializer().Serialize(objCChargeTypeGroup.lstCVarChargeTypeGroup)
                    , new JavaScriptSerializer().Serialize(objCvwPayables.lstCVarvwPayables) //data[24]

                };
            }
            #endregion PaymentRequests Reports
            #region LetterRelease / ReservationLetter / DangerousGoodsLetter Reports
            else if (pReportName.ToUpper() == "LETTERRELEASE.RPT" || pReportName.ToUpper() == "RESERVATIONLETTER.RPT" || pReportName.ToUpper() == "DANGEROUSGOODSLETTER.RPT")
            {
                #region Set DismissalPermissionSerial (used as ReleaseLetterSerial for TEU)
                if (pReportName.ToUpper() == "LETTERRELEASE.RPT" && objCvwOperations.lstCVarvwOperations[0].DismissalPermissionSerial == "0"
                    && objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName == "TEU")
                {
                    COperations objCOperationsTemp = new COperations();
                    objCOperationsTemp.UpdateList("DismissalPermissionSerial=(SELECT ltrim((ISNULL(DismissalPermissionSerial,0) + 1)) + '-' + RIGHT('00'+CAST(MONTH(GETDATE()) AS VARCHAR(3)),2) + '-' + ltrim(YEAR(GETDate())) FROM Serials WHERE Year=YEAR(GETDate())) WHERE ID=" + pOperationID + "");
                    CSerials objCSerials = new Models.NoAccess.Generated.CSerials();
                    objCSerials.UpdateList("DismissalPermissionSerial=ISNULL(DismissalPermissionSerial,0) + 1 WHERE Year=YEAR(GETDate())");
                    objCvwOperations.GetListPaging(999999, 1, " WHERE ID = " + pOperationID, "ID", out _RowCount);
                }
                #endregion Set DismissalPermissionSerial (used as ReleaseLetterSerial for TEU)

                return new object[] {
                    RecordsExist, MissingMandatoryFields, pBranchName, pConsigneeName, pVesselName, pLineName
                    , pMasterBL, pHouseNumber, pPOLCodeAndName, pPODCodeAndName, pVoyageOrTruckNumber, Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pActualArrival)
                    , pContainerNumbers, pEnCompanyName, pClientEmail, pRoadOrContainerNumber, pClientName
                    , new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations[0])
                    , new JavaScriptSerializer().Serialize(objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages)
                    , pDeliveryOrderNumber, pDeliveryDate , pConsigneeAddressFromCustomers
                };
            }
            #endregion LetterRelease / ReservationLetter / DangerousGoodsLetter Reports
            #region DeliveryOrder Reports
            else if (pReportName.ToUpper() == "DELIVERYORDER.RPT")
            {
                #region Set DeliveryOrderSerial
                if (objCvwOperations.lstCVarvwOperations[0].DeliveryOrderSerial == "0" && objCvwOperations.lstCVarvwOperations[0].BLType != 3) //not master
                {
                    COperations objCOperationsTemp = new COperations();
                    //objCOperationsTemp.UpdateList("DeliveryOrderSerial=(SELECT ltrim((ISNULL(DeliveryOrderSerial,0) + 1)) + '-' + RIGHT('00'+CAST(MONTH(GETDATE()) AS VARCHAR(3)),2) + '-' + ltrim(YEAR(GETDate())) FROM Serials WHERE Year=YEAR(GETDate())) WHERE ID=" + pOperationID + "");
                    objCOperationsTemp.UpdateList("DeliveryOrderSerial=(SELECT ltrim((ISNULL(DeliveryOrderSerial,0) + 1)) + '/' + ltrim(YEAR(GETDate())) FROM Serials WHERE Year=YEAR(GETDate())) WHERE ID=" + pOperationID + "");
                    CSerials objCSerials = new Models.NoAccess.Generated.CSerials();
                    objCSerials.UpdateList("DeliveryOrderSerial=ISNULL(DeliveryOrderSerial,0) + 1 WHERE Year=YEAR(GETDate())");
                    objCvwOperations.GetListPaging(999999, 1, " WHERE ID = " + pOperationID, "ID", out _RowCount);
                }
                #endregion Set DeliveryOrderSerial
                return new object[] { RecordsExist, MissingMandatoryFields, pBranchName, pCommodityName, pVesselName, pLineName
                    , pMasterBL, pHouseNumber, pPOLCodeAndName, pPODCodeAndName, pVoyageOrTruckNumber, Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pActualArrival)
                    , new JavaScriptSerializer().Serialize(objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages), pClientEmail
                    , pEnCompanyName, pRoadOrContainerNumber, pShipmentType, pMasterOperationCode
                    , pShipperName, pShipperContactName, pShipperPhones, pShipperFax, pShipperEmail
                    , pConsigneeName, pConsigneeContactName, pConsigneePhones, pConsigneeFax, pConsigneeEmail
                    , pNotify1Name, pNotify1ContactName, pNotify1Phones ,pNotify1Fax, pNotify1Email
                    , pShipperAddressLine1, pShipperAddressLine2, pShipperCityName, pShipperCountryName
                    , pConsigneeAddressLine1, pConsigneeAddressLine2, pConsigneeCityName, pConsigneeCountryName
                    , pNotify1AddressLine1, pNotify1AddressLine2, pNotify1CityName, pNotify1CountryName
                    , pCustomerReference, pNumberOfPackages, pGrossWeightSum, pVolumeSum, pVGMSum, pDescriptionOfGoods
                    , new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations[0])
                    , pRoadNumber, pDeliveryOrderNumber
                    , objCvwRoutings.lstCVarvwRoutings.Count > 0 ? new JavaScriptSerializer().Serialize(objCvwRoutings.lstCVarvwRoutings[0]) : null
                    , objCvwRoutingsMaster.lstCVarvwRoutings.Count > 0 ? new JavaScriptSerializer().Serialize(objCvwRoutingsMaster.lstCVarvwRoutings[0]) : null
                    };
            }
            #endregion DeliveryOrder Reports
            #region ShippingAdvice Reports
            else if (pReportName.ToUpper() == "SHIPPINGADVICE.RPT" && objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName != "DYN")
            {
                #region Checking mandatoryfields for printing this reports
                if (pTransportType == "OCEAN" && pReportName.ToUpper() == "SHIPPINGADVICE.RPT") //don't check mandatory fields for ShipmentDetails
                {
                    if (pVesselName == "")
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Vessel Name" : ", Vessel Name");
                    if (pVoyageOrTruckNumber == "")
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Voyage Number" : ", Voyage Number");
                    if (pContainerNumbers == "")
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Container Number" : ", Container Number");
                    if (pDescriptionOfGoods == "")
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Goods Description" : ", Goods Description");
                    if (pGrossWeightSum == 0) //TGW
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Gross Weight" : ", Gross Weight");
                    if (pNetWeightSum == 0) //TNW
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Net Weight" : ", Net Weight");
                    if (pActualDeparture.Equals(DateTime.Parse("01-01-1900"))) //ATS
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Actual Departure Date" : ", Actual Departure Date");
                    if (MissingMandatoryFields != "")
                    {
                        RecordsExist = false;
                        MissingMandatoryFields = "You are missing " + MissingMandatoryFields + ".";
                    }
                }
                #endregion Checking mandatoryfields for printing this reports
                return new object[] { RecordsExist, MissingMandatoryFields, pOperationCode, pShipperName, pConsigneeName, pNotify1Name
                                        , Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pActualDeparture), Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pExpectedArrival), pPOLCodeAndName, pPODCodeAndName, pVoyageOrTruckNumber
                                        , pContainerNumbers, pDescriptionOfGoods, pDimensions, pVesselName, pGrossWeightSum, pNetWeightSum, pClientEmail, pLineName
                                        , pMasterBL, pBranchName, pTruckingOrderBookingNumber};
            }
            #endregion ShippingAdvice Reports
            #region ShipmentDetails Reports
            else if (pReportName.ToUpper() == "SHIPMENTDETAILS.RPT")
            {
                #region Checking mandatoryfields for printing this reports
                if (pTransportType == "OCEAN" && pReportName.ToUpper() == "SHIPPINGADVICE.RPT") //don't check mandatory fields for ShipmentDetails
                {
                    if (pVesselName == "")
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Vessel Name" : ", Vessel Name");
                    if (pVoyageOrTruckNumber == "")
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Voyage Number" : ", Voyage Number");
                    if (pContainerNumbers == "")
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Container Number" : ", Container Number");
                    if (pDescriptionOfGoods == "")
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Goods Description" : ", Goods Description");
                    if (pGrossWeightSum == 0) //TGW
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Gross Weight" : ", Gross Weight");
                    if (pNetWeightSum == 0) //TNW
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Net Weight" : ", Net Weight");
                    if (pActualDeparture.Equals(DateTime.Parse("01-01-1900"))) //ATS
                        MissingMandatoryFields += (MissingMandatoryFields == "" ? "Actual Departure Date" : ", Actual Departure Date");
                    if (MissingMandatoryFields != "")
                    {
                        RecordsExist = false;
                        MissingMandatoryFields = "You are missing " + MissingMandatoryFields + ".";
                    }
                }
                #endregion Checking mandatoryfields for printing this reports
                return new object[] { RecordsExist, MissingMandatoryFields, pOperationCode, pShipperName, pConsigneeName, pNotify1Name
                                        , Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pActualDeparture), Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pExpectedArrival), pPOLCodeAndName, pPODCodeAndName, pVoyageOrTruckNumber
                                        , pContainerNumbers, pDescriptionOfGoods, pDimensions, pVesselName, pGrossWeightSum, pNetWeightSum, pClientEmail, pLineName
                                        , pMasterBL, pBranchName, pTruckingOrderBookingNumber, pPickupAddress, pDeliveryAddress};
            }
            #endregion ShipmentDetails Reports
            #region BookingConfirmation/OPERATIONTRACKING.RPT Reports
            else if (pReportName.ToUpper() == "BOOKINGCONFIRMATION.RPT" || pReportName.ToUpper() == "OPERATION TRACKING.RPT"
                         || (objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName == "DYN" && pReportName.ToUpper() == "SHIPPINGADVICE.RPT")
                         || (objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName == "DYN" && pReportName.ToUpper() == "ARRIVALNOTICE.RPT")
                     )
                return new object[] { RecordsExist, MissingMandatoryFields, pOperationCode, pShipperName, pConsigneeName, pLineName
                    , Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pActualDeparture), Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pExpectedArrival), pPOLCodeAndName, pPODCodeAndName, pVoyageOrTruckNumber
                    , pContainerTypes, pDescriptionOfGoods, pVesselName, pGrossWeightSum, pPackageTypes, pPOrC, Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pExpectedDeparture)
                    , pClientEmail
                    , new JavaScriptSerializer().Serialize(objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages)
                    , new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations[0])
                    , new JavaScriptSerializer().Serialize(objCvwOperationTracking.lstCVarvwOperationTracking)
                    };
            #endregion BookingConfirmation/OPERATIONTRACKING.RPT Reports
            #region HBL Reports
            else if (pReportName.ToUpper() == "HBL.RPT")
            {
                #region Checking mandatoryfields for printing this reports
                if (pShipperName == "" && objCvwOperations.lstCVarvwOperations[0].DirectionType == 2)
                    MissingMandatoryFields += (MissingMandatoryFields == "" ? "Shipper Name" : ", Shipper Name");
                if (pConsigneeName == "" && !objCvwOperations.lstCVarvwOperations[0].IsAWB)
                    MissingMandatoryFields += (MissingMandatoryFields == "" ? "Consignee Name" : ", Consignee Name");
                //if (pActualDeparture.Equals(DateTime.Parse("01-01-1900")))
                //    MissingMandatoryFields += (MissingMandatoryFields == "" ? "Actual Departure Date" : ", Actual Departure Date");
                if (pPOL == "" && !objCvwOperations.lstCVarvwOperations[0].IsAWB)
                    MissingMandatoryFields += (MissingMandatoryFields == "" ? "POL" : ", POL");
                if (pPOD == "" && !objCvwOperations.lstCVarvwOperations[0].IsAWB)
                    MissingMandatoryFields += (MissingMandatoryFields == "" ? "POD" : ", POD");
                #endregion Checking mandatoryfields for printing this reports

                //kk:by kelany (it seems to be getting cargo on master when printing for house: ask why?)
                CvwOperationContainersAndPackages objCvwOperationContainersAndPackages_GLS = new CvwOperationContainersAndPackages();
                if (objCvwOperations.lstCVarvwOperations.Count > 0)
                    objCvwOperationContainersAndPackages_GLS.GetListPaging(99999, 1, " WHERE OperationID = " + objCvwOperations.lstCVarvwOperations[0].MasterOperationID.ToString(), "ID", out _RowCount);
                pReportName = "HBL.rpt";
                pReportNameWithoutExtension = "HBL"; //used to name the file to be exported by adding date and time to the file name then the extension
                if (MissingMandatoryFields == "")
                    return new object[] { RecordsExist, MissingMandatoryFields, pOperationCode, pShipperName
                            , pShipperContactName, pShipperPhones, pShipperFax, pShipperEmail
                            , pConsigneeName, pConsigneeContactName, pConsigneePhones, pConsigneeFax, pConsigneeEmail
                            , pNotify1Name, pNotify1ContactName, pNotify1Phones ,pNotify1Fax, pNotify1Email
                            , pLineName, pVoyageOrTruckNumber, pVesselName, /*pActualDeparture.ToShortDateString()*/Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pActualDeparture), pPOLCodeAndName, pPODCodeAndName
                            , pEnCompanyName, pArCompanyName, pPOrC, pAddressLine1, pAddressLine2, pAddressLine3, pAddressLine4, pAddressLine5
                            , new JavaScriptSerializer().Serialize(objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages)
                            , pPhones, pFaxes, pEmail, pWebsite, pClientEmail, pContainerTypes, pPackageTypes, pShipmentType, pGrossWeightSum
                            , pDescriptionOfGoods, pRoadOrContainerNumber, pPackageTypesOnContainersTotals
                            , pShipperAddressLine1, pShipperAddressLine2, pShipperCityName, pShipperCountryName
                            , pConsigneeAddressLine1, pConsigneeAddressLine2, pConsigneeCityName, pConsigneeCountryName
                            , pNotify1AddressLine1, pNotify1AddressLine2, pNotify1CityName, pNotify1CountryName
                            , pNetWeightSum, pVGMSum, pDeliveryCountryName, pDeliveryCityName, pTruckingOrderBookingNumber
                            , Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pExpectedArrival)
                            , pVolumeSum, pHouseNumber
                            , pAgentName, pAgentContactName, pAgentPhones, pAgentFax, pAgentEmail
                            , pAgentAddressLine1, pAgentAddressLine2, pAgentCityName, pAgentCountryName
                            , pPickupAddress, pDeliveryAddress, Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pETAPOLDate)
                            , pCustomerReference, pMasterBL, pRepBLTypeShown, pBranchName
                            , new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations[0])

                            , new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations) //pPrintedBills
                            , pShipperBankAccount, pConsigneeBankAccount
                            , new JavaScriptSerializer().Serialize(objCvwPayables.lstCVarvwPayables)
                            , new JavaScriptSerializer().Serialize(objCvwOperationContainersAndPackages_GLS.lstCVarvwOperationContainersAndPackages)
                            , pNotify2Name, pNotify2ContactName, pNotify2Phones ,pNotify2Fax, pNotify2Email
                            , pNotify2AddressLine1, pNotify2AddressLine2, pNotify2CityName, pNotify2CountryName
                            , new JavaScriptSerializer().Serialize(objCShipper)
                            , new JavaScriptSerializer().Serialize(objCConsignee)
                            , new JavaScriptSerializer().Serialize(objCAgent)
                            , new JavaScriptSerializer().Serialize(objCNotify1)
                        };
                else
                {
                    RecordsExist = false;
                    MissingMandatoryFields = "You are missing " + MissingMandatoryFields + ".";
                    return new object[] { RecordsExist, MissingMandatoryFields, strExportedFileName };
                }
            }
            #endregion HBL Reports
            #region TruckingOrder
            else if (pReportName.ToUpper() == "TRUCKINGORDER.RPT")
                return new object[] { RecordsExist, MissingMandatoryFields, pOperationCode, pTruckingOrderClientName
                , pTruckingOrderClientAddress, pTruckingOrderClientContactName, pTruckingOrderGensetSupplierName
                , pTruckingOrderCCAName, pTruckingOrderQuantity, pTruckingOrderGateInPortName, pTruckingOrderGateOutPortName
                , pTruckingOrderGateInDate, pTruckingOrderGateOutDate, pTruckingOrderStuffingDate, pTruckingOrderDeliveryDate
                , pTruckingOrderBookingNumber, pTruckingOrderDelays, pTruckingOrderPowerFromGateInTillActualSailing, (pTruckingOrderShippingLineName == "0" ? (pLineLocalName =="N/A"?pLineName:pLineLocalName) : pTruckingOrderShippingLineName)
                , pTruckingOrderTruckerName, pIsGenset, pTruckingOrderContactPerson, pTruckingOrderPickupAddress
                , pTruckingOrderDeliveryAddress, pCCAContactName, pCCAPhones
                , pTruckingOrderContactPersonPhones, pTruckingOrderLoadingTime, pTruckingOrderNotes
                , new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations[0])
                ,new JavaScriptSerializer().Serialize(objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages)
                //,pClientAddressLine1,pClientAddressLine2
                ,new JavaScriptSerializer().Serialize(objCvwRoutingsTruckingOrder.lstCVarvwRoutings)
                };

            #endregion TruckingOrder
            #region Manifest Reports
            else if (pReportName.ToUpper() == "MANIFEST.RPT")
                return new object[] { RecordsExist, MissingMandatoryFields
                    , new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations[0])
                    , new JavaScriptSerializer().Serialize(objCvwHouses.lstCVarvwOperations)
                    , new JavaScriptSerializer().Serialize(objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages)
                };

            #endregion Manifest Reports
            #region ManifestModify Reports
            else if (pReportName.ToUpper() == "MANIFESTMODIFY.RPT")
                return new object[] { RecordsExist, MissingMandatoryFields, pBranchName/*i got it from DB for the case he changed it in operation w/o saving*/
                    , pEnCompanyName, pArCompanyName, pContainerNumbers, pGrossWeightSum, pVolumeSum
                    , pVesselName, pPOLCodeAndName, pPODCodeAndName, pLineName, Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pExpectedArrival), pClientName, pClientEmail, pDescriptionOfGoods
                    , Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(pActualArrival)
                    , pMasterBL, pNumberOfPackages, pDeliveryAddress
                    , new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations[0])
                };

            #endregion ManifestModify Reports

            #region اذن تسليم / Route/DismissalPermission/ConsolidationLetter/ShippingOrder/اقرارHBL/اقرار المنافيست/اقرار مخزن/اقرار مستودعات/اقرار مفرقعات/اقرارالشبكه الالكترونيه

            else if (pReportName.ToUpper() == "ROUTE.RPT" || pReportName.ToUpper() == "DISMISSALPERMISSION.RPT" || pReportName.ToUpper() == "CONSOLIDATION LETTER.RPT" || pReportName.ToUpper() == "SHIPPING ORDER.RPT"
                || pReportName.ToUpper() == "اقرارHBL.RPT" || pReportName.ToUpper() == "اقرار المنافيست.RPT" || pReportName.ToUpper() == "اقرار مخزن.RPT"
                || pReportName.ToUpper() == "اقرار مستودعات.RPT" || pReportName.ToUpper() == "اقرار مفرقعات.RPT" || pReportName.ToUpper() == "اقرارالشبكه الالكترونيه.RPT"
                || pReportName.ToUpper() == "اذن تسليم.RPT" || pReportName.ToUpper() == "MEDCOVER.RPT")
            {
                #region Set DismissalPermissionSerial (used as ReleaseLetterSerial for TEU)
                if (pReportName.ToUpper() == "DISMISSALPERMISSION.RPT" && objCvwOperations.lstCVarvwOperations[0].DismissalPermissionSerial == "0"
                    && objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName != "TEU")
                {
                    COperations objCOperationsTemp = new COperations();
                    objCOperationsTemp.UpdateList("DismissalPermissionSerial=(SELECT ltrim((ISNULL(DismissalPermissionSerial,0) + 1)) + '-' + RIGHT('00'+CAST(MONTH(GETDATE()) AS VARCHAR(3)),2) + '-' + ltrim(YEAR(GETDate())) FROM Serials WHERE Year=YEAR(GETDate())) WHERE ID=" + pOperationID + "");
                    CSerials objCSerials = new Models.NoAccess.Generated.CSerials();
                    objCSerials.UpdateList("DismissalPermissionSerial=ISNULL(DismissalPermissionSerial,0) + 1 WHERE Year=YEAR(GETDate())");
                    objCvwOperations.GetListPaging(999999, 1, " WHERE ID = " + pOperationID, "ID", out _RowCount);
                }
                #endregion Set DismissalPermissionSerial (used as ReleaseLetterSerial for TEU)
                return new object[] {
                    new JavaScriptSerializer().Serialize(objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages)
                    , new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations[0])
                    , objCvwRoutings.lstCVarvwRoutings.Count > 0 ? new JavaScriptSerializer().Serialize(objCvwRoutings.lstCVarvwRoutings[0]) : null
                    , objCvwRoutingsMaster.lstCVarvwRoutings.Count > 0 ? new JavaScriptSerializer().Serialize(objCvwRoutingsMaster.lstCVarvwRoutings[0]) : null
                    , pSupplierName //data[4]
                };
            }
            #endregion  Route DismissalPermission/ConsolidationLetter/ShippingOrder
            #region اذن شحن
            else if (pReportName.ToUpper() == "اذن شحن.RPT")
            {
                CShippingPermissionSerials objCShippingPermissionSerials = new CShippingPermissionSerials();
                objCShippingPermissionSerials.GetList(" Where Operation = " + pOperationID);
                if (objCShippingPermissionSerials.lstCVarShippingPermissionSerials.Count == 0)
                {
                    objCShippingPermissionSerials.GetMaxSerial("SELECT (isnull(MAX(Serial),0))+1 Serial FROM [ShippingPermissionSerials] WHERE [year]=YEAR(GETDATE())");

                    CVarShippingPermissionSerials objCVarShippingPermissionSerials = new CVarShippingPermissionSerials();
                    objCVarShippingPermissionSerials.ID = 0;
                    objCVarShippingPermissionSerials.Serial = objCShippingPermissionSerials.lstCVarShippingPermissionSerials[0].Serial;
                    objCVarShippingPermissionSerials.Year = DateTime.Now.Year;
                    objCVarShippingPermissionSerials.Operation = Convert.ToInt32(pOperationID);
                    objCShippingPermissionSerials = new CShippingPermissionSerials();
                    objCShippingPermissionSerials.lstCVarShippingPermissionSerials.Add(objCVarShippingPermissionSerials);
                    objCShippingPermissionSerials.SaveMethod(objCShippingPermissionSerials.lstCVarShippingPermissionSerials);
                }

                objCShippingPermissionSerials.GetList(" Where Operation = " + pOperationID);
                CvwContainerPackages objCvwContainerPackages = new CvwContainerPackages();
                objCvwContainerPackages.GetList(" Where OperationID = " + pOperationID);

                return new object[] {
                    pVesselName,pPOLName,pPODName,pDescriptionOfGoods,pPackageTypes,
                    new JavaScriptSerializer().Serialize(objCvwOperationContainersAndPackages.lstCVarvwOperationContainersAndPackages),
                    new JavaScriptSerializer().Serialize(objCShippingPermissionSerials.lstCVarShippingPermissionSerials),
                    new JavaScriptSerializer().Serialize(objCvwContainerPackages.lstCVarvwContainerPackages),
                    new JavaScriptSerializer().Serialize(objCvwRoutings.lstCVarvwRoutings),
                    new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations[0])
                };
            }
            #endregion
            #endregion call ExportReport with suitable parametersaccording to docOut
            //}
            //else
            //    RecordsExist = false;
            //MissingMandatoryFields is the message returned
            return new object[] { RecordsExist, MissingMandatoryFields, strExportedFileName };
        }


        //[HttpGet, HttpPost]//pID : is the InvoiceID
        //public object[] Report_Invoice(string pWhereClause, Int64 pID, int pInvoiceReportTypeID)
        //{
        //    bool RecordsExist = true;
        //    var strExportedFileName = "";

        //    CvwInvoices objCvwInvoices = new CvwInvoices();
        //    objCvwInvoices.GetList(pWhereClause);
        //    CvwReceivables objCvwReceivables = new CvwReceivables();
        //    objCvwReceivables.GetList(" WHERE InvoiceID = " + pID);

        //    //i am sure i have just 1 row coz the where clause used the ID
        //    if (objCvwInvoices.lstCVarvwInvoices.Count > 0)
        //    {
        //        RecordsExist = true;
        //        var pReportName = "Invoice.rpt";
        //        var pReportNameWithoutExtension = "Invoice"; //used to name the file to be exported by adding date and time to the file name then the extension
        //        #region setting the header invoice parameters
        //        var pInvoiceDate = objCvwInvoices.lstCVarvwInvoices[0].InvoiceDate;
        //        var pDueDate = objCvwInvoices.lstCVarvwInvoices[0].DueDate;
        //        var pPaymentTerm = (objCvwInvoices.lstCVarvwInvoices[0].PaymentTermCode == "0" ? "" : objCvwInvoices.lstCVarvwInvoices[0].PaymentTermCode);
        //        var pInvoiceNumber = objCvwInvoices.lstCVarvwInvoices[0].InvoiceNumber;
        //        var pPartnerName = objCvwInvoices.lstCVarvwInvoices[0].PartnerName;
        //        var pStreetLine1 = (objCvwInvoices.lstCVarvwInvoices[0].StreetLine1 == "0" ? "" : objCvwInvoices.lstCVarvwInvoices[0].StreetLine1);
        //        var pStreetLine2 = (objCvwInvoices.lstCVarvwInvoices[0].StreetLine2 == "0" ? "" : objCvwInvoices.lstCVarvwInvoices[0].StreetLine2);
        //        var pCityName = (objCvwInvoices.lstCVarvwInvoices[0].CityName == "0" ? "" : objCvwInvoices.lstCVarvwInvoices[0].CityName);
        //        var pCountryName = (objCvwInvoices.lstCVarvwInvoices[0].CountryName == "0" ? "" : objCvwInvoices.lstCVarvwInvoices[0].CountryName);
        //        var pOperationCode = objCvwInvoices.lstCVarvwInvoices[0].OperationCode;
        //        var pLineName = (objCvwInvoices.lstCVarvwInvoices[0].LineName == "0" ? "" : objCvwInvoices.lstCVarvwInvoices[0].LineName);
        //        var pVesselName = (objCvwInvoices.lstCVarvwInvoices[0].VesselName == "0" ? "" : objCvwInvoices.lstCVarvwInvoices[0].VesselName);
        //        var pVoyageOrTruckNumber = (objCvwInvoices.lstCVarvwInvoices[0].VoyageOrTruckNumber == "0" ? "" : objCvwInvoices.lstCVarvwInvoices[0].VoyageOrTruckNumber);
        //        var pPOLCode = objCvwInvoices.lstCVarvwInvoices[0].POLCode;
        //        var pPODCode = objCvwInvoices.lstCVarvwInvoices[0].PODCode;
        //        var pMasterBL = (objCvwInvoices.lstCVarvwInvoices[0].MasterBL == "0" ? "" : objCvwInvoices.lstCVarvwInvoices[0].MasterBL);
        //        var pHouseNumber = (objCvwInvoices.lstCVarvwInvoices[0].HouseNumber == "0" ? "" : objCvwInvoices.lstCVarvwInvoices[0].HouseNumber);
        //        #endregion
        //        //i am sending data from receivables view and not invoices
        //        strExportedFileName = ExportReport(objCvwReceivables.lstCVarvwReceivables, pReportName, pReportNameWithoutExtension, pInvoiceReportTypeID
        //            , new Object[] { pInvoiceDate, pDueDate, pPaymentTerm, pInvoiceNumber, pPartnerName, pStreetLine1
        //                , pStreetLine2, pCityName, pCountryName, pOperationCode, pLineName, pVesselName, pVoyageOrTruckNumber
        //                , pPOLCode, pPODCode, pMasterBL, pHouseNumber }); //ParameterFields
        //    }
        //    else
        //        RecordsExist = false;
        //    return new object[] { RecordsExist, strExportedFileName };
        //}

        //    public string ExportReport(System.Collections.IEnumerable DataToExport, string pReportName, string pReportNameWithoutExtension, int pReportTypeID, object[] pParameterFields)
        //    {
        //        ReportDocument rd = new ReportDocument();
        //        string strPath = HttpContext.Current.Server.MapPath("~/") + "Reports//" + pReportName;
        //        rd.Load(strPath);
        //        rd.SetDataSource(DataToExport);
        //        #region Setting Filenames and Type
        //        //the next 2 lines are just to give a default value
        //        var tip = CrystalDecisions.Shared.ExportFormatType.Excel;
        //        //string strPDFFileName      = pReportNameWithoutExtension + DateTime.Now.ToString().Replace("/", "-").Replace(":", "-") + ".xls";
        //        string strExportedFileName = pReportNameWithoutExtension + DateTime.Now.ToString().Replace("/", "-").Replace(":", "-") + ".xls";
        //        if (pReportTypeID == 1) //1: Excel File
        //        {
        //            tip = CrystalDecisions.Shared.ExportFormatType.Excel;
        //            //strPDFFileName = pReportNameWithoutExtension + DateTime.Now.ToString().Replace("/", "-").Replace(":", "-") + ".xls";
        //            strExportedFileName = pReportNameWithoutExtension + DateTime.Now.ToString().Replace("/", "-").Replace(":", "-") + ".xls";
        //        }
        //        if (pReportTypeID == 2) //2: Excel File Without Formatting
        //        {
        //            tip = CrystalDecisions.Shared.ExportFormatType.ExcelRecord; //Data Only
        //            //strPDFFileName = pReportNameWithoutExtension + DateTime.Now.ToString().Replace("/", "-").Replace(":", "-") + ".xls";
        //            strExportedFileName = pReportNameWithoutExtension + DateTime.Now.ToString().Replace("/", "-").Replace(":", "-") + ".xls";
        //        }
        //        if (pReportTypeID == 3) //3: PDF File
        //        {
        //            tip = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
        //            //strPDFFileName = pReportNameWithoutExtension + DateTime.Now.ToString().Replace("/", "-").Replace(":", "-") + ".pdf";
        //            strExportedFileName = pReportNameWithoutExtension + DateTime.Now.ToString().Replace("/", "-").Replace(":", "-") + ".pdf";
        //        }
        //        if (pReportTypeID == 4) //4: Rich Text Format Document
        //        {
        //            tip = CrystalDecisions.Shared.ExportFormatType.RichText;
        //            //strPDFFileName = pReportNameWithoutExtension + DateTime.Now.ToString().Replace("/", "-").Replace(":", "-") + ".rtf";
        //            strExportedFileName = pReportNameWithoutExtension + DateTime.Now.ToString().Replace("/", "-").Replace(":", "-") + ".rtf";
        //        }
        //        if (pReportTypeID == 5) //5: Word Document
        //        {
        //            tip = CrystalDecisions.Shared.ExportFormatType.WordForWindows;
        //            //strPDFFileName = pReportNameWithoutExtension + DateTime.Now.ToString().Replace("/", "-").Replace(":", "-") + ".doc";
        //            strExportedFileName = pReportNameWithoutExtension + DateTime.Now.ToString().Replace("/", "-").Replace(":", "-") + ".doc";
        //        }
        //        #endregion Setting Filenames and Type
        //        #region Setting ParameterFields For Quotations Report
        //        if (pReportName == "Quotations.rpt")
        //        {
        //            rd.SetParameterValue("@DirectionType", pParameterFields[0]);
        //            rd.SetParameterValue("@TransportType", pParameterFields[1]);
        //            rd.SetParameterValue("@Stage", pParameterFields[2]);
        //            rd.SetParameterValue("@Salesman", pParameterFields[3]);
        //            rd.SetParameterValue("@POL", pParameterFields[4]);
        //            rd.SetParameterValue("@POD", pParameterFields[5]);
        //            rd.SetParameterValue("@FromOpenDate", pParameterFields[6]);
        //            rd.SetParameterValue("@ToOpenDate", pParameterFields[7]);
        //        }
        //        #endregion Setting ParameterFields For Quotations Report
        //        #region Setting ParameterFields For Operations Report
        //        if (pReportName == "Operations.rpt")
        //        {
        //            rd.SetParameterValue("@DirectionType", pParameterFields[0]);
        //            rd.SetParameterValue("@TransportType", pParameterFields[1]);
        //            rd.SetParameterValue("@Stage", pParameterFields[2]);
        //            rd.SetParameterValue("@Salesman", pParameterFields[3]);
        //            rd.SetParameterValue("@POL", pParameterFields[4]);
        //            rd.SetParameterValue("@POD", pParameterFields[5]);
        //            rd.SetParameterValue("@FromOpenDate", pParameterFields[6]);
        //            rd.SetParameterValue("@ToOpenDate", pParameterFields[7]);
        //        }
        //        #endregion Setting ParameterFields For Operations Report
        //        #region Setting ParameterFields For Invoice Report
        //        if (pReportName == "Invoice.rpt")
        //        {
        //            rd.SetParameterValue("@InvoiceDate", pParameterFields[0]);
        //            rd.SetParameterValue("@DueDate", pParameterFields[1]);
        //            rd.SetParameterValue("@PaymentTerm", pParameterFields[2]);
        //            rd.SetParameterValue("@InvoiceNumber", pParameterFields[3]);
        //            rd.SetParameterValue("@PartnerName", pParameterFields[4]);
        //            rd.SetParameterValue("@StreetLine1", pParameterFields[5]);
        //            rd.SetParameterValue("@StreetLine2", pParameterFields[6]);
        //            rd.SetParameterValue("@CityName", pParameterFields[7]);
        //            rd.SetParameterValue("@CountryName", pParameterFields[8]);
        //            rd.SetParameterValue("@OperationCode", pParameterFields[9]);
        //            rd.SetParameterValue("@LineName", pParameterFields[10]);
        //            rd.SetParameterValue("@VesselName", pParameterFields[11]);
        //            rd.SetParameterValue("@VoyageOrTruck", pParameterFields[12]);
        //            rd.SetParameterValue("@POLCode", pParameterFields[13]);
        //            rd.SetParameterValue("@PODCode", pParameterFields[14]);
        //            rd.SetParameterValue("@MasterBL", pParameterFields[15]);
        //            rd.SetParameterValue("@HouseNumber", pParameterFields[16]);
        //        }
        //        #endregion Setting ParameterFields For Invoice Report 
        //        #region Setting ParameterFields For OperationCover OR OperationStatement Reports
        //        if (pReportName.ToUpper() == "OPERATIONCOVER.RPT" || pReportName.ToUpper() == "OPERATIONSTATEMENT.RPT")
        //        {
        //            rd.SetParameterValue("@OperationCode", pParameterFields[0]);
        //            rd.SetParameterValue("@DirectionType", pParameterFields[1]);
        //            rd.SetParameterValue("@ShipmentType", pParameterFields[2]);
        //            rd.SetParameterValue("@TransportType", pParameterFields[3]);
        //            rd.SetParameterValue("@OpenDate", pParameterFields[4]);
        //            rd.SetParameterValue("@ClientName", pParameterFields[5]);
        //            rd.SetParameterValue("@ContainerTypes", pParameterFields[6]);
        //            rd.SetParameterValue("@Carrier", pParameterFields[7]);
        //            rd.SetParameterValue("@CommodityName", pParameterFields[8]);
        //            rd.SetParameterValue("@POL", pParameterFields[9]);
        //            rd.SetParameterValue("@POD", pParameterFields[10]);
        //            rd.SetParameterValue("@VesselName", pParameterFields[11]);
        //            rd.SetParameterValue("@Salesman", pParameterFields[12]);
        //            rd.SetParameterValue("@AgentName", pParameterFields[13]);
        //            rd.SetParameterValue("@IncotermName", pParameterFields[14]);
        //            rd.SetParameterValue("@ActualDeparture", pParameterFields[15]);
        //            rd.SetParameterValue("@ActualArrival", pParameterFields[16]);
        //            rd.SetParameterValue("@MasterBL", pParameterFields[17]);
        //            rd.SetParameterValue("@HouseNumber", pParameterFields[18]);
        //            rd.SetParameterValue("@ContainerNumbers", pParameterFields[19]);
        //            rd.SetParameterValue("@InvoiceNumbers", pParameterFields[20]);
        //            rd.SetParameterValue("@DocumentISOCode", pParameterFields[21]);
        //            rd.SetParameterValue("@IsPrintISOCode", pParameterFields[22]);
        //            rd.SetParameterValue("@GrossWeightSum", pParameterFields[23]);
        //            rd.SetParameterValue("@UserName", pParameterFields[24]);
        //        }
        //        #endregion Setting ParameterFields For OperationCover OR OperationStatement Reports
        //        #region Setting ParameterFields For ShippingDeclaration OR PreShippingDeclaration Reports
        //        if (pReportName.ToUpper() == "SHIPPINGDECLARATION.RPT")
        //        {
        //            rd.SetParameterValue("@IsPreShipping", pParameterFields[0]);
        //            rd.SetParameterValue("@EnCompanyName", pParameterFields[1]);
        //            rd.SetParameterValue("@ArCompanyName", pParameterFields[2]);

        //            rd.SetParameterValue("@POrC", pParameterFields[3]);
        //            rd.SetParameterValue("@DocumentISOCode", pParameterFields[4]);
        //            rd.SetParameterValue("@IsPrintISOCode", pParameterFields[5]); 

        //            rd.SetParameterValue("@AddressLine1", pParameterFields[6]);
        //            rd.SetParameterValue("@AddressLine2", pParameterFields[7]);
        //            rd.SetParameterValue("@AddressLine3", pParameterFields[8]);
        //            rd.SetParameterValue("@AddressLine4", pParameterFields[9]);
        //            rd.SetParameterValue("@AddressLine5", pParameterFields[10]);
        //            rd.SetParameterValue("@Phones", pParameterFields[11]);
        //            rd.SetParameterValue("@Faxes", pParameterFields[12]);
        //            rd.SetParameterValue("@Email", pParameterFields[13]);
        //            rd.SetParameterValue("@Website", pParameterFields[14]);

        //            rd.SetParameterValue("@ShipperName", pParameterFields[15]);
        //            rd.SetParameterValue("@ShipperContactName", pParameterFields[16]);
        //            rd.SetParameterValue("@ShipperPhones", pParameterFields[17]);
        //            rd.SetParameterValue("@ShipperFax", pParameterFields[18]);
        //            rd.SetParameterValue("@ShipperEmail", pParameterFields[19]);

        //            rd.SetParameterValue("@ConsigneeName", pParameterFields[20]);
        //            rd.SetParameterValue("@ConsigneeContactName", pParameterFields[21]);
        //            rd.SetParameterValue("@ConsigneePhones", pParameterFields[22]);
        //            rd.SetParameterValue("@ConsigneeFax", pParameterFields[23]);
        //            rd.SetParameterValue("@ConsigneeEmail", pParameterFields[24]);

        //            rd.SetParameterValue("@Notify1Name", pParameterFields[25]);
        //            rd.SetParameterValue("@Notify1ContactName", pParameterFields[26]);
        //            rd.SetParameterValue("@Notify1Phones", pParameterFields[27]);
        //            rd.SetParameterValue("@Notify1Fax", pParameterFields[28]);
        //            rd.SetParameterValue("@Notify1Email", pParameterFields[29]);

        //            rd.SetParameterValue("@VoyageOrTruckNumber", pParameterFields[30]);
        //            rd.SetParameterValue("@VesselName", pParameterFields[31]);
        //            rd.SetParameterValue("@TransportType", pParameterFields[32]);
        //            rd.SetParameterValue("@ActualDeparture", pParameterFields[33]);
        //            rd.SetParameterValue("@POL", pParameterFields[34]);
        //            rd.SetParameterValue("@POD", pParameterFields[35]);
        //            rd.SetParameterValue("@ShipmentType", pParameterFields[36]);
        //            rd.SetParameterValue("@ContainerTypes", pParameterFields[37]);
        //        }
        //        #endregion Setting ParameterFields For ShippingDeclaration OR PreShippingDeclaration Reports
        //        #region Setting ParameterFields For ArrivalNotice Report
        //        if (pReportName == "ArrivalNotice.rpt")
        //        {
        //            rd.SetParameterValue("@pIsPrintISOCode", pParameterFields[0]);
        //            rd.SetParameterValue("@pDocumentISOCode", pParameterFields[1]);
        //            rd.SetParameterValue("@pBranchName", pParameterFields[2]);
        //            rd.SetParameterValue("@pEnCompanyName", pParameterFields[3]);
        //            rd.SetParameterValue("@pArCompanyName", pParameterFields[4]);
        //            rd.SetParameterValue("@pHouseNumber", pParameterFields[5]);
        //            rd.SetParameterValue("@pMasterBL", pParameterFields[6]);
        //            rd.SetParameterValue("@pPOL", pParameterFields[7]);
        //            rd.SetParameterValue("@pPOD", pParameterFields[8]);
        //            rd.SetParameterValue("@pContainerNumbers", pParameterFields[9]);
        //            rd.SetParameterValue("@pGrossWeightSum", pParameterFields[10]);
        //            rd.SetParameterValue("@pVolumeSum", pParameterFields[11]);
        //            rd.SetParameterValue("@pDescriptionOfGoods", pParameterFields[12]);
        //            rd.SetParameterValue("@pVesselName", pParameterFields[13]);
        //            rd.SetParameterValue("@pExpectedArrival", pParameterFields[14]);
        //            rd.SetParameterValue("@pTransportType", pParameterFields[15]);
        //            //rd.SetParameterValue("@HouseNumber", pParameterFields[16]);
        //        }
        //        #endregion Setting ParameterFields For ArrivalNotice Report 
        //        #region Setting ParameterFields For PaymentRequests Report
        //        if (pReportName == "PaymentRequests.rpt")
        //        {
        //            rd.SetParameterValue("@pIsPrintISOCode", pParameterFields[0]);
        //            rd.SetParameterValue("@pDocumentISOCode", pParameterFields[1]);
        //            rd.SetParameterValue("@pBranchName", pParameterFields[2]);
        //            rd.SetParameterValue("@pEnCompanyName", pParameterFields[3]);
        //            rd.SetParameterValue("@pOperationCode", pParameterFields[4]);
        //            rd.SetParameterValue("@pDirectionType", pParameterFields[5]);
        //            rd.SetParameterValue("@pShipmentType", pParameterFields[6]);
        //            rd.SetParameterValue("@pTransportType", pParameterFields[7]);
        //            rd.SetParameterValue("@pClientName", pParameterFields[8]);
        //            //rd.SetParameterValue("@OperationCode", pParameterFields[9]);
        //            //rd.SetParameterValue("@LineName", pParameterFields[10]);
        //            //rd.SetParameterValue("@VesselName", pParameterFields[11]);
        //            //rd.SetParameterValue("@VoyageOrTruck", pParameterFields[12]);
        //            //rd.SetParameterValue("@POLCode", pParameterFields[13]);
        //            //rd.SetParameterValue("@PODCode", pParameterFields[14]);
        //            //rd.SetParameterValue("@MasterBL", pParameterFields[15]);
        //            //rd.SetParameterValue("@HouseNumber", pParameterFields[16]);
        //        }
        //        #endregion Setting ParameterFields For PaymentRequests Report 
        //        #region Setting ParameterFields For LetterRelease Report
        //        if (pReportName == "LetterRelease.rpt")
        //        {
        //            rd.SetParameterValue("@pIsPrintISOCode", pParameterFields[0]);
        //            rd.SetParameterValue("@pDocumentISOCode", pParameterFields[1]);
        //            rd.SetParameterValue("@pBranchName", pParameterFields[2]);
        //            rd.SetParameterValue("@pEnCompanyName", pParameterFields[3]);
        //            rd.SetParameterValue("@pArCompanyName", pParameterFields[4]);
        //            rd.SetParameterValue("@pHouseNumber", pParameterFields[5]);
        //            rd.SetParameterValue("@pMasterBL", pParameterFields[6]);
        //            rd.SetParameterValue("@pPOL", pParameterFields[7]);
        //            rd.SetParameterValue("@pPOD", pParameterFields[8]);
        //            rd.SetParameterValue("@pContainerNumbers", pParameterFields[9]);
        //            rd.SetParameterValue("@pVesselName", pParameterFields[10]);
        //            rd.SetParameterValue("@pTransportType", pParameterFields[11]);
        //            rd.SetParameterValue("@pCarrier", pParameterFields[12]);
        //            rd.SetParameterValue("@pConsigneeName", pParameterFields[13]);
        //            //rd.SetParameterValue("@pVolumeSum", pParameterFields[14]);
        //            //rd.SetParameterValue("@VoyageOrTruck", pParameterFields[15]);
        //            //rd.SetParameterValue("@HouseNumber", pParameterFields[16]);
        //        }
        //        #endregion Setting ParameterFields For LetterRelease Report 
        //        #region Setting ParameterFields For DeliveryOrder Report
        //        if (pReportName == "DeliveryOrder.rpt")
        //        {
        //            rd.SetParameterValue("@pIsPrintISOCode", pParameterFields[0]);
        //            rd.SetParameterValue("@pDocumentISOCode", pParameterFields[1]);
        //            rd.SetParameterValue("@pBranchName", pParameterFields[2]);
        //            rd.SetParameterValue("@pEnCompanyName", pParameterFields[3]);
        //            rd.SetParameterValue("@pArCompanyName", pParameterFields[4]);
        //            rd.SetParameterValue("@pHouseNumber", pParameterFields[5]);
        //            rd.SetParameterValue("@pMasterBL", pParameterFields[6]);
        //            rd.SetParameterValue("@pPOL", pParameterFields[7]);
        //            rd.SetParameterValue("@pPOD", pParameterFields[8]);
        //            rd.SetParameterValue("@pVesselName", pParameterFields[9]);
        //            rd.SetParameterValue("@pExpectedArrival", pParameterFields[10]);
        //            rd.SetParameterValue("@pActualArrival", pParameterFields[11]);
        //            rd.SetParameterValue("@pTransportType", pParameterFields[12]);
        //            rd.SetParameterValue("@pConsigneeName", pParameterFields[13]);
        //            //rd.SetParameterValue("@pExpectedArrival", pParameterFields[14]);
        //            //rd.SetParameterValue("@pTransportType", pParameterFields[15]);
        //            //rd.SetParameterValue("@VoyageOrTruck", pParameterFields[16]);
        //            //rd.SetParameterValue("@HouseNumber", pParameterFields[17]);
        //        }
        //        #endregion Setting ParameterFields For DeliveryOrder Report 
        //        rd.ExportToDisk(tip, (System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/") + "Reports//PrintedFiles//", strExportedFileName)));
        //        //System.Diagnostics.Process.Start((System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/") + "Reports//PrintedFiles//", strExportedFileName)));


        //        //rd.ExportToDisk(tip, (System.IO.Path.Combine(@"D:\\", strExportedFileName)));
        //        //System.Diagnostics.Process.Start(@"D:\\", strExportedFileName);

        //        //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "var popup=window.open('files/" + strExportedFileName + "');popup.focus(); ", true);
        //        //ClientScript.RegisterStartupScript(this.Page.GetType(), "popupOpener", "var popup=window.open('Files/BiWeeklyReport.pdf');popup.focus();", true);
        //        //// Create a new WebClient instance.
        //        //System.IO.StreamWriter writer = new System.IO.StreamWriter(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/") + "Reports//PrintedFiles//", strExportedFileName));
        //        //HttpResponse Response = new HttpResponse(writer);
        //        //rd.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "Crystal.pdf");
        //        //rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //        //Response.End();

        //        //try
        //        //{
        //        //    CrystalDecisions.Shared.ExportOptions CrExportOptions;
        //        //    CrystalDecisions.Shared.DiskFileDestinationOptions CrDiskFileDestinationOptions = new CrystalDecisions.Shared.DiskFileDestinationOptions();
        //        //    CrystalDecisions.Shared.PdfRtfWordFormatOptions CrFormatTypeOptions = new CrystalDecisions.Shared.PdfRtfWordFormatOptions();
        //        //    CrDiskFileDestinationOptions.DiskFileName = "\\" + Environment.MachineName + @"\d$\\aaa.pdf";
        //        //    CrExportOptions = rd.ExportOptions;
        //        //    {
        //        //        CrExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
        //        //        CrExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
        //        //        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
        //        //        CrExportOptions.FormatOptions = CrFormatTypeOptions;
        //        //    }
        //        //    rd.Export();
        //        //}

        //        //catch (Exception ex)
        //        //{
        //        //    //MessageBox.Show(ex.ToString());
        //        //}
        //        //System.Diagnostics.Process.Start((System.IO.Path.Combine("\\" + Environment.MachineName, @"\d$\\aaa.pdf")));




        //        //WebClient myWebClient = new WebClient();
        //        //////// Concatenate the domain with the Web resource filename.
        //        //////// Download the Web resource and save it into the current filesystem folder.
        //        //myWebClient.DownloadFile(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/") + "Reports//PrintedFiles//"+strExportedFileName), @"D:\aa.pdf");

        //// System.Net.WebClient Client = new WebClient();            
        ////var a = Client.DownloadString(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/") + "Reports//PrintedFiles//"+strExportedFileName));


        //        //FileWebRequest request = (FileWebRequest)FileWebRequest.Create(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/") + "Reports//PrintedFiles//" + strExportedFileName));
        //        //WebResponse response = request.GetResponse();
        //        //System.IO.Stream stream = response.GetResponseStream();
        //        //System.IO.StreamReader reader = new System.IO.StreamReader(stream);
        //        //var x = reader.ReadToEnd(); 

        //        //System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        //        //response.ClearContent();
        //        //response.Clear();
        //        //response.ContentType = "text/plain";     //alternatively change to the content-type of your file
        //        //response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/") + "Reports//PrintedFiles//" + strExportedFileName) + ";");
        //        //response.TransmitFile(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/") + "Reports//PrintedFiles//" + strExportedFileName));
        //        //response.Flush();
        //        //response.End();
        //        return strExportedFileName;
        //    }
    }
}
