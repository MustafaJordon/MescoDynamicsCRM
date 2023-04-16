﻿using Forwarding.MvcApp.Models.OperAcc.Generated;
using Forwarding.MvcApp.Models.CustomizedSPCall;
using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;
using Forwarding.MvcApp.Models.MasterData.Partners.Generated;
using Forwarding.MvcApp.Models.NoAccess.Generated;
using Forwarding.MvcApp.Models.Operations.Operations.Generated;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebMatrix.WebData;
using Forwarding.MvcApp.Models.Administration.Security.Generated;
using Forwarding.MvcApp.Models.Accounting.MasterData.Generated;
using Forwarding.MvcApp.Models.Administration.Settings.Generated;
using Forwarding.MvcApp.Models.LocalEmails.Generated;
using System.Globalization;
using Forwarding.MvcApp.Models.MasterData.Locations.Generated;
using Forwarding.MvcApp.Models.MasterData.Others.Generated;
using System.Collections.Generic;
using Forwarding.MvcApp.Models.ReceiptsAndPayments.Transactions.Generated;
using Forwarding.MvcApp.Models.Customized;
using System.Drawing;
using QRCoder;
using Forwarding.MvcApp.Models.Operations.Operations.Customized;
using Forwarding.MvcApp.Entities.Operations;
using Forwarding.MvcApp.Models.NoAccess.Customized;
using Forwarding.MvcApp.Models.ContainerFreightStation.Tariff;

namespace Forwarding.MvcApp.Controllers.Operations.API_Operations
{
    public class InvoicesController : ApiController
    {
        [HttpGet, HttpPost]
        public Object[] LoadAll(string pWhereClause)
        {
            CvwInvoices objCvwInvoices = new CvwInvoices();
            //objCvwInvoices.GetList(pWhereClause);
            Int32 _RowCount = 0;

            CNoAccessGblTransactionTypes objCNoAccessGblTransactionTypes = new CNoAccessGblTransactionTypes();
            objCNoAccessGblTransactionTypes.GetListPaging(999999, 1, "Where 1=1", "Name", out _RowCount);

            objCvwInvoices.GetListPaging(999999, 1, pWhereClause, "ID DESC", out _RowCount);

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] {
                serializer.Serialize(objCvwInvoices.lstCVarvwInvoices)
                , serializer.Serialize(objCNoAccessGblTransactionTypes.lstCVarNoAccessGblTransactionTypes)
            };
        }

        [HttpGet, HttpPost]
        public Object[] LoadWithPagingWithWhereClauseAndOrderByWithObjectArrayReturned(bool pIsLoadArrayOfObjects, Int32 pPageNumber, Int32 pPageSize, string pWhereClause, string pOrderBy)
        {
            Int32 _RowCount = 0;
            Int32 _RowCount2 = 0;

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };

            CvwA_Accounts objCvwA_Accounts = new CvwA_Accounts();
            objCvwA_Accounts.GetListPaging(9999, 1, "WHERE IsMain=0", "Name, Code", out _RowCount2);
            //COperations objCOperations = new COperations();
            //objCOperations.GetListPaging(2000, 1, "WHERE CreationDate > DATEADD(mm,DATEDIFF(mm,0,GETDATE())-12,0) ", " ID DESC ", out _RowCount);

            CvwOperationsWithMinimalColumns objCOperations = new CvwOperationsWithMinimalColumns();
            //objCOperations.GetListPaging(2000, 1, "WHERE 1=1 ", " ID DESC ", out _RowCount);

            CNoAccessPartnerTypes objCNoAccessPartnerTypes = new CNoAccessPartnerTypes();
            objCNoAccessPartnerTypes.GetList(" WHERE 1=1 ");

            CvwAccPartners objCvwAccPartners = new CvwAccPartners();
            //objCvwAccPartners.GetList(" WHERE 1=1 ORDER BY PartnerTypeID, Name ");

            CNoAccessPaymentType objCNoAccessPaymentType = new CNoAccessPaymentType();
            objCNoAccessPaymentType.GetList(" WHERE IsInactive=0 ORDER BY Name ");

            CInvoiceTypes objCInvoiceTypes = new CInvoiceTypes();
            objCInvoiceTypes.GetList("WHERE Code<>N'DRAFT' ORDER BY Name");

            CvwA_CostCenters objCvwA_CostCenters = new CvwA_CostCenters();
            objCvwA_CostCenters.GetListPaging(9999, 1, "WHERE IsMain=0", "Name, Code", out _RowCount);

            CvwDefaults objCvwDefaults = new CvwDefaults();
            objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa
            string CompanyName = objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName;
            if (CompanyName == "BED")
            {
                objCvwA_CostCenters.GetListPaging(9999, 1, "WHERE IsMain=0  and id NOT IN ( SELECT a.CostCenterID FROM A_LinkOperationWithCostCenter a JOIN Operations AS o ON o.ID=a.OperationID WHERE o.OperationStageID IN(110,120))", "Name, Code", out _RowCount);

            }
            else
            {
                objCvwA_CostCenters.GetListPaging(9999, 1, "WHERE IsMain=0", "Name, Code", out _RowCount);
            }



            CNoAccessGblTransactionTypes objCNoAccessGblTransactionTypes = new CNoAccessGblTransactionTypes();
            objCNoAccessGblTransactionTypes.GetListPaging(999999, 1, "Where 1=1", "Name", out _RowCount);
            
            CvwInvoices objCvwInvoices = new CvwInvoices();
            objCvwInvoices.GetListPaging(pPageSize, pPageNumber, pWhereClause, pOrderBy, out _RowCount);

            return new Object[] {
                new JavaScriptSerializer().Serialize(objCvwInvoices.lstCVarvwInvoices)
                , _RowCount
              , pIsLoadArrayOfObjects ? serializer.Serialize(objCvwAccPartners.lstCVarvwAccPartners) : null  //data[2]
              , pIsLoadArrayOfObjects ? serializer.Serialize(objCOperations.lstCVarvwOperationsWithMinimalColumns) : null  //data[3]
              , pIsLoadArrayOfObjects ? new JavaScriptSerializer().Serialize(objCNoAccessPaymentType.lstCVarNoAccessPaymentType) : null  //data[4]
              , pIsLoadArrayOfObjects ? new JavaScriptSerializer().Serialize(objCNoAccessPartnerTypes.lstCVarNoAccessPartnerTypes) : null //data[5]
              , pIsLoadArrayOfObjects ? new JavaScriptSerializer().Serialize(objCvwA_CostCenters.lstCVarvwA_CostCenters) : null //data[6]
              , pIsLoadArrayOfObjects ? new JavaScriptSerializer().Serialize(objCInvoiceTypes.lstCVarInvoiceTypes) : null //data[7]
              , pIsLoadArrayOfObjects ? new JavaScriptSerializer().Serialize(objCNoAccessGblTransactionTypes.lstCVarNoAccessGblTransactionTypes) : null //data[8]
              , pIsLoadArrayOfObjects ? new JavaScriptSerializer().Serialize(objCvwA_Accounts.lstCVarvwA_Accounts) : null //data[9]

            };
        }
        
        [HttpGet, HttpPost]
        public object[] LoadInvoiceHeaderWithDetails(string pInvoiceIDToLoad)
        {
            string _ReturnedMessage = "";
            Exception checkException = null;
            int _RowCount = 0;
            var CustomsClearanceRoutingTypeID = 70;
            var constCustomerPartnerTypeID = 1;
            var constAgentPartnerTypeID = 2;

            CDefaults objCDefaults = new CDefaults();
            CvwInvoices objCvwInvoices = new CvwInvoices();
            CvwInvoices objCSOAInvoices = new CvwInvoices();
            CvwReceivables objCvwReceivables = new CvwReceivables();
            CvwOperations objCvwOperations = new CvwOperations();
            CvwOperationPartners objCvwOperationPartners = new CvwOperationPartners();
            CvwAddresses objCvwAddresses = new CvwAddresses();
            CPaymentTerms objCPaymentTerms = new CPaymentTerms();
            CTaxeTypes objCTaxeTypes = new CTaxeTypes();
            CvwRoutings objCvwRoutings = new CvwRoutings();
            CUsers objCUsers = new CUsers();

            checkException = objCDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount);
            checkException = objCvwInvoices.GetListPaging(1, 1, "WHERE ID=" + pInvoiceIDToLoad, "ID", out _RowCount);
            //if ((objCvwInvoices.lstCVarvwInvoices[0].InvoiceTypeCode == "SOA" || objCvwInvoices.lstCVarvwInvoices[0].InvoiceTypeCode == "DRAFT")
            //    && !objCvwInvoices.lstCVarvwInvoices[0].IsFleet
            //    && objCDefaults.lstCVarDefaults[0].UnEditableCompanyName != "GBL")
            //    checkException = objCSOAInvoices.GetListPaging(1, 1, " WHERE ID<>" + pInvoiceIDToLoad + " AND OperationID IN(" + objCvwInvoices.lstCVarvwInvoices[0].OperationID + "," + objCvwInvoices.lstCVarvwInvoices[0].MasterOperationID + ") AND InvoiceTypeCode<>N'SOA' AND InvoiceTypeCode<>N'DRAFT' AND IsDeleted = 0 ", "ID", out _RowCount);
            //checkException = objCvwReceivables.GetListPaging(999999, 1, "WHERE InvoiceID=" + pInvoiceIDToLoad, "ID", out _RowCount);
            checkException = objCvwOperations.GetListPaging(999999, 1, "WHERE ID=" + objCvwInvoices.lstCVarvwInvoices[0].OperationID + " OR MasterOperationID=" + objCvwInvoices.lstCVarvwInvoices[0].OperationID, "ID", out _RowCount);
            if (objCvwInvoices.lstCVarvwInvoices[0].IsFleet)
                checkException = objCvwOperationPartners.GetListPaging(999999, 1, "WHERE ID=" + objCvwInvoices.lstCVarvwInvoices[0].OperationPartnerID, "ID", out _RowCount);
            else
                checkException = objCvwOperationPartners.GetListPaging(999999, 1, "WHERE PartnerID is NOT NULL AND PartnerTypeID IN(" + constCustomerPartnerTypeID + "," + constAgentPartnerTypeID + ") AND OperationID=" + objCvwInvoices.lstCVarvwInvoices[0].OperationID, "ID", out _RowCount);
            checkException = objCvwAddresses.GetListPaging(999999, 1, "WHERE (PartnerID=" + objCvwInvoices.lstCVarvwInvoices[0].PartnerID + " AND PartnerTypeID=" + objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID + ")", "AddressTypeID", out _RowCount);
            checkException = objCPaymentTerms.GetListPaging(999999, 1, "WHERE IsInactive=0", "Code", out _RowCount);
            checkException = objCTaxeTypes.GetListPaging(999999, 1, "WHERE IsInactive=0", "Code", out _RowCount);
            checkException = objCvwRoutings.GetListPaging(999999, 1, "WHERE RoutingTypeID= " + CustomsClearanceRoutingTypeID + " AND OperationID=" + (objCvwInvoices.lstCVarvwInvoices[0].MasterOperationID == 0 ? objCvwInvoices.lstCVarvwInvoices[0].OperationID : objCvwInvoices.lstCVarvwInvoices[0].MasterOperationID), "ID", out _RowCount);
            checkException = objCUsers.GetListPaging(999999, 1, "WHERE IsInactive=0", "Name", out _RowCount);

            #region Get Minimal
            var pRoutingList = objCvwRoutings.lstCVarvwRoutings
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    OperationID = s.OperationID
                    ,
                    OperationCode = s.OperationCode
                    ,
                    TruckingOrderCode = s.TruckingOrderCode
                    ,
                    ClientName = s.ClientName
                    ,
                    CertificateNumber = s.CertificateNumber
                })
                //.Distinct().OrderBy(o => o.Name)
                .ToList();
            #endregion Get Minimal

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[]
            {
                _ReturnedMessage
                , serializer.Serialize(objCvwInvoices.lstCVarvwInvoices[0]) //pData[1]
                , null //serializer.Serialize(objCvwReceivables.lstCVarvwReceivables) //pData[2]
                , serializer.Serialize(objCvwOperations.lstCVarvwOperations) //pData[3]
                , serializer.Serialize(objCvwOperationPartners.lstCVarvwOperationPartners) //pData[4]
                , serializer.Serialize(objCvwAddresses.lstCVarvwAddresses) //pData[5]
                , serializer.Serialize(objCPaymentTerms.lstCVarPaymentTerms) //pData[6]
                , serializer.Serialize(objCTaxeTypes.lstCVarTaxeTypes) //pData[7]
                , null //serializer.Serialize(pRoutingList) //pData[8]
                , null //, serializer.Serialize(objCSOAInvoices.lstCVarvwInvoices) //pData[9]
                , serializer.Serialize(objCUsers.lstCVarUsers) //pData[10]
            };
        }

        [HttpGet, HttpPost]
        public Object[] LoadAllTransactionTypes()
        {
            CNoAccessGblTransactionTypes objCNoAccessGblTransactionTypes = new CNoAccessGblTransactionTypes();
            //objCvwInvoices.GetList(pWhereClause);
            Int32 _RowCount = 0;
            objCNoAccessGblTransactionTypes.GetListPaging(999999, 1, "Where 1=1", "ID DESC", out _RowCount);

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] { serializer.Serialize(objCNoAccessGblTransactionTypes.lstCVarNoAccessGblTransactionTypes) };
        }

        public void CreateQRCode(string InvoiceID, string CompanyName, string VatIDNo, DateTime InvoiceDate, decimal Amount)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            CReceivables objCReceivables = new CReceivables();
            objCReceivables.GetList("Where InvoiceID=" + InvoiceID);
            decimal TotalVat = 0;
            for (int i = 0; i < objCReceivables.lstCVarReceivables.Count; i++)
            {
                TotalVat += objCReceivables.lstCVarReceivables[i].TaxAmount;
            }

            string QRData = InvoiceQRCode.getTLVBase64(CompanyName, VatIDNo, InvoiceDate, Amount, TotalVat);
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(QRData, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            byte[] data;

            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();


            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                data = stream.ToArray();
            }


            //objCCustomizedDBCall.CallStringFunctionWithImage("update Invoices set QRImage=@img where ID=" + InvoiceID
            //    , data);
            objCCustomizedDBCall.CallStringFunctionWithImage(data , InvoiceID);
        }
        
        [HttpGet, HttpPost]
        public object[] CreateInvoiceFromTariff(Int64 pOperationID_Tariff, Int32 pInvoiceTypeID)
        {
            string returnedMessage = "";
            Exception checkException = null;
            int _RowCount = 0;
            decimal _ExchangeRate = 1;
            int _CurrencyID = 0;
            string _InvoiceIDs = "0";
            string _ReceivablesIDs = "0";
            string _InvoiceNumbers = "0";

            CvwOperations objCvwOperations = new CvwOperations();
            CWH_CSL_Tariff objCWH_CSL_Tariff = new CWH_CSL_Tariff();
            CWH_CSL_Tariff_Details objCWH_CSL_Tariff_Details = new CWH_CSL_Tariff_Details();
            CDefaults objCDefaults = new CDefaults();
            objCDefaults.GetListPaging(999999, 1, "WHERE 1=1", "ID", out _RowCount);
            _CurrencyID = objCDefaults.lstCVarDefaults[0].CurrencyID;

            checkException = objCDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount);
            checkException = objCWH_CSL_Tariff.GetListPaging(1, 1, "WHERE InvoiceTypeID=" + pInvoiceTypeID, "ID", out _RowCount);
            checkException = objCvwOperations.GetListPaging(1, 1, "WHERE ID=" + pOperationID_Tariff, "ID", out _RowCount);
            if (objCWH_CSL_Tariff.lstCVarWH_CSL_Tariff.Count == 0)
                returnedMessage = "No tariff defined for this invoice type.";
            
            #region Tariff Found
            else
            {
                checkException = objCWH_CSL_Tariff_Details.GetListPaging(1, 1, "WHERE WH_CSL_TariffID=" + pInvoiceTypeID, "ID", out _RowCount);

                #region Creating InvoiceItems
                for (int i=0;i< objCWH_CSL_Tariff_Details.lstCVarWH_CSL_Tariff_Details.Count; i++)
                {
                    #region Add Receivable
                    CVarReceivables objCVarReceivables = new CVarReceivables();
                    objCVarReceivables.CreatorUserID = WebSecurity.CurrentUserId;
                    objCVarReceivables.CreationDate = DateTime.Now;
                    objCVarReceivables.GeneratingQRID = 0;
                    objCVarReceivables.InvoiceID = 0; //objCVarInvoices.ID;
                    objCVarReceivables.AccNoteID = 0;
                    objCVarReceivables.OperationContainersAndPackagesID = 0;
                    objCVarReceivables.HouseBillID = 0;

                    objCVarReceivables.OperationVehicleID = 0;
                    //objCVarReceivables.VehicleAgingReportID = objCvwWH_VehicleAgingReport.lstCVarvwWH_VehicleAgingReport.Count == 0
                    //                                            ? 0
                    //                                            : objCvwWH_VehicleAgingReport.lstCVarvwWH_VehicleAgingReport[0].ID;
                    objCVarReceivables.VehicleAgingReportID = 0;

                    objCVarReceivables.ID = 0;

                    objCVarReceivables.OperationID = 0; //pOperationVehicleList[i].OperationID; //not to appear in the Operation
                    objCVarReceivables.ChargeTypeID = objCWH_CSL_Tariff_Details.lstCVarWH_CSL_Tariff_Details[i].ChargeTypesID;
                    objCVarReceivables.POrC = 0;
                    objCVarReceivables.MeasurementID = 0;
                    objCVarReceivables.ContainerTypeID = 0;
                    objCVarReceivables.SupplierID = 0;
                    objCVarReceivables.Quantity = 1;
                    objCVarReceivables.CostPrice = 0;
                    objCVarReceivables.CostAmount = 0;
                    //objCVarReceivables.SalePrice = Math.Round((_NumberOfDays * objCWH_ContractDetails.lstCVarWH_ContractDetails[z].Rate + objCWH_ContractDetails.lstCVarWH_ContractDetails[z].Cost), 2);
                    objCVarReceivables.SalePrice = objCWH_CSL_Tariff_Details.lstCVarWH_CSL_Tariff_Details[i].Rate;

                    objCVarReceivables.AmountWithoutVAT = Math.Round((objCVarReceivables.Quantity * objCVarReceivables.SalePrice), 2);
                    objCVarReceivables.TaxeTypeID = 0;
                    objCVarReceivables.TaxPercentage = 0;
                    objCVarReceivables.TaxAmount = Math.Round(((objCVarReceivables.AmountWithoutVAT * objCVarReceivables.TaxPercentage) / 100), 2);
                    objCVarReceivables.DiscountTypeID = 0;
                    objCVarReceivables.DiscountPercentage = 0;
                    objCVarReceivables.DiscountAmount = 0;

                    objCVarReceivables.SaleAmount = objCVarReceivables.AmountWithoutVAT + objCVarReceivables.TaxAmount;
                    objCVarReceivables.ExchangeRate = _ExchangeRate;
                    objCVarReceivables.CurrencyID = _CurrencyID;
                    objCVarReceivables.Notes = "0";
                    objCVarReceivables.IssueDate = DateTime.Now;

                    objCVarReceivables.PreviousCutOffDate = DateTime.Parse("01/01/1900");
                    objCVarReceivables.CutOffDate = DateTime.Parse("01/01/1900");
                    objCVarReceivables.ReceiptDate = DateTime.Parse("01/01/1900");
                    objCVarReceivables.ReceiptNo = "";

                    objCVarReceivables.ModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarReceivables.ModificationDate = DateTime.Now;

                    CReceivables objCReceivables = new CReceivables();
                    objCReceivables.lstCVarReceivables.Add(objCVarReceivables);
                    //checkException = objCReceivables.SaveMethod(objCReceivables.lstCVarReceivables);
                    _ReceivablesIDs += "," + objCVarReceivables.ID;
                    #endregion Add Receivable
                }
                #endregion Creating InvoiceItems

                #region InvoiceHeader
                if (_ReceivablesIDs != "0") //Items before grouping
                {
                    CvwOperationPartners objCvwOperationPartners = new CvwOperationPartners();
                    checkException = objCvwOperationPartners.GetListPaging(999999, 1, "WHERE OperationID=" + pOperationID_Tariff + " AND PartnerName=N'" + objCvwOperations.lstCVarvwOperations[0].ClientName + "'", "ID", out _RowCount);
                    CVarInvoices objCVarInvoices = new CVarInvoices();
                    objCVarInvoices.InvoiceNumber = 0;
                    objCVarInvoices.OperationID = pOperationID_Tariff;
                    objCVarInvoices.OperationPartnerID = objCvwOperationPartners.lstCVarvwOperationPartners[0].ID;
                    objCVarInvoices.AddressID = 0; //pAddressID;
                    objCVarInvoices.InvoiceTypeID = objCWH_CSL_Tariff.lstCVarWH_CSL_Tariff[0].InvoiceTypeID;
                    objCVarInvoices.PrintedAddress = objCvwOperationPartners.lstCVarvwOperationPartners[0].Address; //"0";
                    objCVarInvoices.CustomerReference = "0";
                    objCVarInvoices.PaymentTermID = 0; // pPaymentTermID;
                    objCVarInvoices.CurrencyID = _CurrencyID;
                    objCVarInvoices.ExchangeRate = _ExchangeRate;
                    objCVarInvoices.InvoiceDate = DateTime.Now; //DateTime.ParseExact(pVehicleCutOffDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture); //pInvoiceIssueDate;
                    objCVarInvoices.DueDate = objCVarInvoices.InvoiceDate; //DateTime.ParseExact(pVehicleCutOffDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture); //pInvoiceDueDate;
                    objCVarInvoices.AmountWithoutVAT = 0; //pAmountWithoutVAT;
                    objCVarInvoices.TaxTypeID = 0; //pTaxTypeID;
                    objCVarInvoices.TaxPercentage = 0; //pTaxPercentage;
                    objCVarInvoices.TaxAmount = 0; //pTaxAmount;
                    objCVarInvoices.DiscountTypeID = 0; //pDiscountTypeID;
                    objCVarInvoices.DiscountPercentage = 0; //pDiscountPercentage;
                    objCVarInvoices.DiscountAmount = 0; //pDiscountAmount;
                    objCVarInvoices.FixedDiscount = 0; //pFixedDiscount;
                    objCVarInvoices.Amount = 0; //pAmount;
                                                //objCVarInvoices.PaidAmount = pPaidAmount;
                                                //objCVarInvoices.RemainingAmount = pRemainingAmount;
                    objCVarInvoices.InvoiceStatusID = 1;
                    objCVarInvoices.IsApproved = false;
                    objCVarInvoices.LeftSignature = "0";
                    objCVarInvoices.MiddleSignature = "0";
                    objCVarInvoices.RightSignature = "0";
                    objCVarInvoices.GRT = "0";
                    objCVarInvoices.DWT = "0";
                    objCVarInvoices.NRT = "0";
                    objCVarInvoices.LOA = "0";
                    objCVarInvoices.EditableNotes = "0";
                    objCVarInvoices.OperationContainersAndPackagesID = 0; //pTankID;
                    objCVarInvoices.TransactionTypeID = 0;

                    objCVarInvoices.Notes = "0";
                    objCVarInvoices.CutOffDate = DateTime.Parse("01/01/1900"); //DateTime.ParseExact(pVehicleCutOffDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture);
                    objCVarInvoices.Is3PL = false;

                    objCVarInvoices.CreatorUserID = objCVarInvoices.ModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarInvoices.CreationDate = objCVarInvoices.ModificationDate = DateTime.Now;

                    CInvoices objCInvoices = new CInvoices();
                    objCInvoices.lstCVarInvoices.Add(objCVarInvoices);
                    checkException = objCInvoices.SaveMethod(objCInvoices.lstCVarInvoices);

                    CReceivables objCReceivables_Temp = new CReceivables();
                    checkException = objCReceivables_Temp.UpdateList("InvoiceID=" + objCVarInvoices.ID + " WHERE ID IN(" + _ReceivablesIDs + ")"); //for eInvoice and approving

                    _InvoiceIDs += "," + objCVarInvoices.ID;
                    #region Update Invoice totals at server side to fix any connection problem
                    string pUpdateClause = "";
                    //SET AmountWithoutVAT
                    pUpdateClause = " AmountWithoutVAT = ROUND((SELECT SUM(ISNULL(SalePrice,0) * ISNULL(Quantity,1))-" + 0 /*pFixedDiscount*/ + " FROM Receivables WHERE InvoiceID = " + objCVarInvoices.ID.ToString() + " AND IsDeleted=0),2)";
                    pUpdateClause += " WHERE ID = " + objCVarInvoices.ID.ToString();
                    checkException = objCInvoices.UpdateList(pUpdateClause);
                    //SET Tax, Discount & Total Amount after setting the AmountWithVAT
                    pUpdateClause = " TaxAmount = ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(TaxPercentage, 0) / 100),2)";
                    pUpdateClause += " , DiscountAmount = ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2)";
                    if (objCDefaults.lstCVarDefaults[0].IsTaxOnItems)
                        pUpdateClause += " , Amount = - ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2) + ROUND((SELECT SUM(ISNULL(SaleAmount,0))-" + 0 /*pFixedDiscount*/ + " FROM Receivables WHERE InvoiceID = " + objCVarInvoices.ID.ToString() + " AND IsDeleted=0),2)";
                    else
                        pUpdateClause += " , Amount = ROUND((ISNULL(AmountWithoutVAT, 0) + ROUND( (ISNULL(AmountWithoutVAT, 0) * ISNULL(TaxPercentage, 0) / 100),2) - ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2)),2)";
                    pUpdateClause += " WHERE ID = " + objCVarInvoices.ID.ToString();
                    checkException = objCInvoices.UpdateList(pUpdateClause);
                    #endregion Update Invoice totals at server side to fix any connection problem
                }
                #endregion InvoiceHeader
            } //else if (objCWH_CSL_Tariff.lstCVarWH_CSL_Tariff.Count == 0)
            #endregion Tariff Found

            #region Get Invoices To be printed
            //CvwOperations objCvwOperations = new CvwOperations();
            CvwInvoices objCvwInvoices = new CvwInvoices();
            CvwReceivables objCvwReceivables = new CvwReceivables();
            if (returnedMessage == "")
            {
                checkException = objCvwOperations.GetListPaging(999999, 1, "WHERE ID IN(" + pOperationID_Tariff + ")", "ID", out _RowCount);
                //_WhereClause = "WHERE IsDeleted=0" + " \n";
                //_WhereClause += "   AND Is3PL=1" + " \n";
                //_WhereClause += "   AND OperationID IN(" + _OperationIDs + ")" + " \n";
                //_WhereClause += "   AND DATEDIFF(DAY,'" + DateTime.ParseExact(pVehicleCutOffDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture) + "',CutOffDate) > 0" + " \n";
                //_WhereClause += "   AND DATEDIFF(DAY,'" + DateTime.ParseExact(pVehicleCutOffDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture) + "',CutOffDate) < 31" + " \n";
                //checkException = objCvwInvoices.GetListPaging(999999, 1, _WhereClause, "ID", out _RowCount);
                checkException = objCvwInvoices.GetListPaging(999999, 1, "WHERE ID IN(" + _InvoiceIDs + ")", "ID", out _RowCount);
                if (objCvwInvoices.lstCVarvwInvoices.Count == 0)
                    returnedMessage = "No applicable invoices.";
                else
                    for (int i = 0; i < objCvwInvoices.lstCVarvwInvoices.Count; i++)
                        _InvoiceNumbers += "," + objCvwInvoices.lstCVarvwInvoices[i].InvoiceNumber + "(" + objCvwInvoices.lstCVarvwInvoices[i].Amount + " " + objCvwInvoices.lstCVarvwInvoices[i].CurrencyCode + ") \n";
                ////to be printed from InvoiceApproval
                //checkException = objCvwReceivables.GetListPaging(999999, 1, "WHERE InvoiceID IN(" + _InvoiceIDs + ")", "ID", out _RowCount);
            }
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            #endregion Get Invoices To be printed

            return new object[]
            {
                returnedMessage
                , _InvoiceNumbers
            };
        }

        [HttpGet, HttpPost]
        public object[] Insert(string pSelectedReceivableItemsIDs, Int64 pInvoiceNumber, Int64 pOperationID, Int64 pOperationPartnerID, Int64 pAddressID, string pPrintedAddress, int pInvoiceTypeID, string pInvoiceTypeCode, string pCustomerReference, int pPaymentTermID, int pCurrencyID, decimal pExchangeRate, string pInvoiceIssueDate, string pInvoiceDueDate, decimal pAmountWithoutVAT, Int32 pTaxTypeID, decimal pTaxPercentage, decimal pTaxAmount, Int32 pDiscountTypeID, decimal pDiscountPercentage, decimal pDiscountAmount,decimal pFixedDiscount, decimal pAmount/*, decimal pPaidAmount, decimal pRemainingAmount*/, int pInvoiceStatusID, bool pIsApproved, Int64 pTankID, bool pApplyTankCharges,Int32 pTransactionTypeID)
        {
            string strDRAFTInvoiceTypeCode = "DRAFT";
            bool _result = false;
            Exception checkException = null;
            CVarInvoices objCVarInvoices = new CVarInvoices();
            string pUpdateClause = "";
            int _RowCount = 0;
            int _DistinctTankCurrencyCount = 0;
            CDefaults objCDefaults = new CDefaults();
            objCDefaults.GetListPaging(999999, 1, "WHERE 1=1", "ID", out _RowCount);
            #region Save InvoiceHeader
            objCVarInvoices.InvoiceNumber = pInvoiceNumber;
            objCVarInvoices.OperationID = pOperationID;
            objCVarInvoices.OperationPartnerID = pOperationPartnerID;
            objCVarInvoices.AddressID = pAddressID;
            objCVarInvoices.InvoiceTypeID = pInvoiceTypeID;
            objCVarInvoices.PrintedAddress = pPrintedAddress;
            objCVarInvoices.CustomerReference = pCustomerReference;
            objCVarInvoices.PaymentTermID = pPaymentTermID;
            objCVarInvoices.CurrencyID = pCurrencyID;
            objCVarInvoices.ExchangeRate = pExchangeRate;
            objCVarInvoices.InvoiceDate = DateTime.ParseExact(pInvoiceIssueDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture); //pInvoiceIssueDate;
            objCVarInvoices.DueDate = DateTime.ParseExact(pInvoiceDueDate + " 00.00.00.000", "dd/MM/yyyy hh.mm.ss.fff", CultureInfo.InvariantCulture); //pInvoiceDueDate;
            objCVarInvoices.AmountWithoutVAT = pAmountWithoutVAT;
            objCVarInvoices.TaxTypeID = pTaxTypeID;
            objCVarInvoices.TaxPercentage = pTaxPercentage;
            objCVarInvoices.TaxAmount = pTaxAmount;
            objCVarInvoices.DiscountTypeID = pDiscountTypeID;
            objCVarInvoices.DiscountPercentage = pDiscountPercentage;
            objCVarInvoices.DiscountAmount = pDiscountAmount;
            objCVarInvoices.FixedDiscount = pFixedDiscount;
            objCVarInvoices.Amount = pAmount;
            //objCVarInvoices.PaidAmount = pPaidAmount;
            //objCVarInvoices.RemainingAmount = pRemainingAmount;
            objCVarInvoices.InvoiceStatusID = pInvoiceStatusID;
            objCVarInvoices.IsApproved = pIsApproved;
            objCVarInvoices.LeftSignature = "0";
            objCVarInvoices.MiddleSignature = "0";
            objCVarInvoices.RightSignature = "0";
            objCVarInvoices.GRT = "0";
            objCVarInvoices.DWT = "0";
            objCVarInvoices.NRT = "0";
            objCVarInvoices.LOA = "0";
            objCVarInvoices.EditableNotes = "0";
            objCVarInvoices.OperationContainersAndPackagesID = pTankID;
            objCVarInvoices.TransactionTypeID = pTransactionTypeID;
            objCVarInvoices.Notes = "0";
            objCVarInvoices.CutOffDate = DateTime.Parse("01/01/1900");
            objCVarInvoices.CreatorUserID = objCVarInvoices.ModificatorUserID = WebSecurity.CurrentUserId;
            objCVarInvoices.CreationDate = objCVarInvoices.ModificationDate = DateTime.Now;

            CInvoices objCInvoices = new CInvoices();
            objCInvoices.lstCVarInvoices.Add(objCVarInvoices);
            checkException = objCInvoices.SaveMethod(objCInvoices.lstCVarInvoices);
            #endregion Save InvoiceHeader
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //inserted successfully so update InvoiceID in receivable items
            {
                _result = true;
                #region Save Invoice Tank Items
                if (pTankID > 0 && pApplyTankCharges)
                {
                    CReceivables objCReceivables = new CReceivables();
                    objCReceivables.GetListPaging(999999, 1, " WHERE IsDeleted=0 AND InvoiceID IS NULL AND OperationID=" + pOperationID + " AND OperationContainersAndPackagesID=" + pTankID, "ID", out _RowCount);
                    _DistinctTankCurrencyCount = objCReceivables.lstCVarReceivables.GroupBy(g => g.CurrencyID).Count();
                    pUpdateClause = (pInvoiceTypeCode == strDRAFTInvoiceTypeCode ? "DraftInvoiceID=" : "InvoiceID=") + objCInvoices.lstCVarInvoices[0].ID + "\n";
                    //pUpdateClause += " , OperationID = " + pOperationID;
                    //pUpdateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                    //pUpdateClause += " , ModificationDate = GETDATE() ";
                    pUpdateClause += " WHERE IsDeleted=0 AND " + (pInvoiceTypeCode == strDRAFTInvoiceTypeCode ? "DraftInvoiceID" : "InvoiceID") + " IS NULL AND CurrencyID=" + pCurrencyID + " AND OperationID=" + pOperationID + " AND OperationContainersAndPackagesID=" + pTankID;
                    pUpdateClause += " And ID in ( " + pSelectedReceivableItemsIDs + ")";
                  checkException = objCReceivables.UpdateList(pUpdateClause);
                }
                #endregion Save Invoice Tank Items
                #region Default Receivables
                else if (pSelectedReceivableItemsIDs == null) //insert default receivables
                {
                    CChargeTypes objCChargeTypes = new CChargeTypes();
                    objCChargeTypes.GetList("WHERE InvoiceTypeID=" + pInvoiceTypeID.ToString());
                    if (objCChargeTypes.lstCVarChargeTypes.Count > 0)
                    {
                        for (int i = 0; i < objCChargeTypes.lstCVarChargeTypes.Count; i++)
                        {
                            CReceivables objCReceivables = new CReceivables();
                            CVarReceivables objCVarReceivables = new CVarReceivables();

                            objCVarReceivables.OperationID = pOperationID;
                            objCVarReceivables.ChargeTypeID = objCChargeTypes.lstCVarChargeTypes[i].ID;
                            objCVarReceivables.MeasurementID = objCChargeTypes.lstCVarChargeTypes[i].MeasurementID;
                            objCVarReceivables.Quantity = 1;
                            objCVarReceivables.ExchangeRate = pExchangeRate;
                            objCVarReceivables.CurrencyID = pCurrencyID;
                            objCVarReceivables.GeneratingQRID = 0;
                            objCVarReceivables.Notes = "Generated with invoice.";
                            if (pInvoiceTypeCode == strDRAFTInvoiceTypeCode)
                                objCVarReceivables.DraftInvoiceID = objCVarInvoices.ID;
                            else
                                objCVarReceivables.InvoiceID = objCVarInvoices.ID;

                            objCVarReceivables.PreviousCutOffDate = DateTime.Parse("01/01/1900");
                            objCVarReceivables.CutOffDate = DateTime.Parse("01/01/1900");
                            objCVarReceivables.ReceiptDate = DateTime.Parse("01/01/1900");
                            objCVarReceivables.ReceiptNo = "";

                            objCVarReceivables.CreatorUserID = objCVarReceivables.ModificatorUserID = WebSecurity.CurrentUserId;
                            objCVarReceivables.ModificationDate = objCVarReceivables.CreationDate = DateTime.Now;

                            objCReceivables.lstCVarReceivables.Add(objCVarReceivables);
                            checkException = objCReceivables.SaveMethod(objCReceivables.lstCVarReceivables);
                        }
                    }
                } //if (pSelectedReceivableItemsIDs == null) //insert default receivables
                #endregion Default Receivables
                #region Selected Receivables
                else
                {
                    string[] strArraySelectedReceivableItemsIDs = pSelectedReceivableItemsIDs.Split(',');
                    string pWhereClause = "";

                    pWhereClause = " WHERE ID = " + strArraySelectedReceivableItemsIDs[0]; //i am sure i have at least 1 row(building the first part of the where clause, then use OR incase of more than 1 house)
                    for (int i = 1; i < strArraySelectedReceivableItemsIDs.Length; i++)
                        pWhereClause += " OR ID = " + strArraySelectedReceivableItemsIDs[i] + "\n";
                    pUpdateClause = (pInvoiceTypeCode == strDRAFTInvoiceTypeCode ? "DraftInvoiceID=" : "InvoiceID=") + objCInvoices.lstCVarInvoices[0].ID + "";
                    #region ensure receivables are correct
                    pUpdateClause += " , AmountWithoutVAT = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2)" + " \n";
                    pUpdateClause += " , TaxPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID)" + " \n";
                    pUpdateClause += " , TaxAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID), 2)" + " \n";
                    pUpdateClause += " , DiscountPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID)" + " \n";
                    pUpdateClause += " , DiscountAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),2)" + " \n";
                    pUpdateClause += " , SaleAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2)" + " \n"
                                  + " + (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID),0) , 2))" + " \n"
                                  + " - (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),0), 2))" + " \n";
                    #endregion ensure receivables are correct
                    //pUpdateClause += " , OperationID = " + pOperationID;
                    //pUpdateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                    //pUpdateClause += " , ModificationDate = GETDATE() ";
                    pUpdateClause += pWhereClause;
                    CReceivables objCReceivables = new CReceivables();
                    checkException = objCReceivables.UpdateList(pUpdateClause);
                }
                #endregion Selected Receivables
                #region Update Invoice totals at server side to fix any connection problem
                //SET AmountWithoutVAT
                pUpdateClause = " AmountWithoutVAT = ROUND((SELECT SUM(ISNULL(SalePrice,0) * ISNULL(Quantity,1))-" + pFixedDiscount + " FROM Receivables WHERE " + (pInvoiceTypeCode == strDRAFTInvoiceTypeCode ? "DraftInvoiceID" : "InvoiceID") + " = " + objCVarInvoices.ID.ToString() + " AND IsDeleted=0),2)";
                pUpdateClause += " WHERE ID = " + objCVarInvoices.ID.ToString();
                checkException = objCInvoices.UpdateList(pUpdateClause);
                //SET Tax, Discount & Total Amount after setting the AmountWithVAT
                pUpdateClause = " TaxAmount = ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(TaxPercentage, 0) / 100),2)";
                pUpdateClause += " , DiscountAmount = ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2)";
                if (objCDefaults.lstCVarDefaults[0].IsTaxOnItems)
                    pUpdateClause += " , Amount = - ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2) + ROUND((SELECT SUM(ISNULL(SaleAmount,0))-" + pFixedDiscount + " FROM Receivables WHERE " + (pInvoiceTypeCode == strDRAFTInvoiceTypeCode ? "DraftInvoiceID" : "InvoiceID") + " = " + objCVarInvoices.ID.ToString() + " AND IsDeleted=0),2)";
                else
                    pUpdateClause += " , Amount = ROUND((ISNULL(AmountWithoutVAT, 0) + ROUND( (ISNULL(AmountWithoutVAT, 0) * ISNULL(TaxPercentage, 0) / 100),2) - ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2)),2)";
                pUpdateClause += " WHERE ID = " + objCVarInvoices.ID.ToString();
                checkException = objCInvoices.UpdateList(pUpdateClause);
                #endregion Update Invoice totals at server side to fix any connection problem
                #region Add to PartnerBalance table
                //{
                //    int constTransactionInvoice = 30;
                //    CvwInvoices objCvwInvoices = new CvwInvoices();
                //    int _RowCount = 0;
                //    objCvwInvoices.GetListPaging(1, 1, ("WHERE ID=" + objCInvoices.lstCVarInvoices[0].ID.ToString()), "ID", out _RowCount);
                //    CVarAccPartnerBalance objCVarAccPartnerBalance = new CVarAccPartnerBalance();
                //    objCVarAccPartnerBalance.InvoiceID = objCInvoices.lstCVarInvoices[0].ID;
                //    objCVarAccPartnerBalance.PartnerTypeID = objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID;
                //    objCVarAccPartnerBalance.CustomerID = GetPartnerIDForInsert(1, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                //    objCVarAccPartnerBalance.AgentID = GetPartnerIDForInsert(2, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                //    objCVarAccPartnerBalance.ShippingAgentID = GetPartnerIDForInsert(3, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                //    objCVarAccPartnerBalance.CustomsClearanceAgentID = GetPartnerIDForInsert(4, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                //    objCVarAccPartnerBalance.ShippingLineID = GetPartnerIDForInsert(5, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                //    objCVarAccPartnerBalance.AirlineID = GetPartnerIDForInsert(6, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                //    objCVarAccPartnerBalance.TruckerID = GetPartnerIDForInsert(7, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                //    objCVarAccPartnerBalance.SupplierID = GetPartnerIDForInsert(8, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                //    objCVarAccPartnerBalance.DebitAmount = pAmount;
                //    objCVarAccPartnerBalance.CurrencyID = pCurrencyID;
                //    objCVarAccPartnerBalance.ExchangeRate = pExchangeRate;// decimal.Parse(ArrExchangeRates[i]); ///////////////////////////////////
                //    objCVarAccPartnerBalance.BalCurLocalExRate = pExchangeRate; ///////////////////////////////////
                //    objCVarAccPartnerBalance.InvCurLocalExRate = pExchangeRate; ///////////////////////////////////
                //    objCVarAccPartnerBalance.TransactionType = constTransactionInvoice;
                //    objCVarAccPartnerBalance.Notes = "Action from Invoices.";
                //    objCVarAccPartnerBalance.CreatorUserID = objCVarAccPartnerBalance.mModificatorUserID = WebSecurity.CurrentUserId;
                //    objCVarAccPartnerBalance.CreationDate = objCVarAccPartnerBalance.ModificationDate = DateTime.Now;

                //    CAccPartnerBalance objCAccPartnerBalance = new CAccPartnerBalance();
                //    objCAccPartnerBalance.lstCVarAccPartnerBalance.Add(objCVarAccPartnerBalance);
                //    checkException = objCAccPartnerBalance.SaveMethod(objCAccPartnerBalance.lstCVarAccPartnerBalance);
                //}
                #endregion Add to PartnerBalance table
                #region QRCode
                if (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "WAV" || objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "SEC" )
                CreateQRCode(objCVarInvoices.ID.ToString(), objCDefaults.lstCVarDefaults[0].CompanyName
                    , objCDefaults.lstCVarDefaults[0].VatIDNo, objCVarInvoices.InvoiceDate, objCVarInvoices.Amount);
                #endregion
            }
            return new object[] {
                _result
                , objCInvoices.lstCVarInvoices[0].ID
                , _DistinctTankCurrencyCount //pData[2] 
            };
        }

        [HttpGet, HttpPost]
        public object[] InvoiceStatus_SaveManualStatus(Int64 pInvoiceIDToSetStatus, Int32 pManualPaymentStatusID, bool pIsDelivered)
        {
            string _ReturnedMessage = "";
            string pUpdateClause = "";
            Exception checkException = null;
            CInvoices objCInvoices = new CInvoices();
            pUpdateClause = "ManualPaymentStatusID=" + (pManualPaymentStatusID == 0 ? "NULL" : pManualPaymentStatusID.ToString()) + " \n";
            pUpdateClause += ",IsDelivered=" + (pIsDelivered ? "1" : "0") + " \n";
            pUpdateClause += "WHERE ID=" + pInvoiceIDToSetStatus + " \n";
            checkException = objCInvoices.UpdateList(pUpdateClause);
            if (checkException != null)
                _ReturnedMessage = checkException.Message;
            return new object[] {
                _ReturnedMessage
            };
        }

        public string GetPartnerID(Int32 pCheckedPartnerTypeID, Int32 pReceivedPartnerTypeID, Int32 pPartnerID)
        {
            if (pCheckedPartnerTypeID == pReceivedPartnerTypeID)
                return pPartnerID.ToString();
            else
                return "null";

        }

        public Int32 GetPartnerIDForInsert(Int32 pCheckedPartnerTypeIDForInsert, Int32 pReceivedPartnerTypeID, Int32 pPartnerID)
        {
            if (pCheckedPartnerTypeIDForInsert == pReceivedPartnerTypeID)
                return pPartnerID;
            else
                return 0;

        }

        [HttpGet, HttpPost]
        public object[] Update([FromBody] pSaveParameters pSaveParamters)
        {
            CInvoices objCInvoices = new CInvoices();
            Exception checkException = null;
            bool _result = false;
            string pUpdateClause = "";

            int _RowCount = 0;
            CDefaults objCDefaults = new CDefaults();
            objCDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa
            int pDefaultCurrencyID = objCDefaults.lstCVarDefaults[0].CurrencyID;
            CvwInvoices objCInvoices_GetInvoiceTypeCode = new CvwInvoices();
            objCInvoices_GetInvoiceTypeCode.GetListPaging(999999, 1, "WHERE ID=" + pSaveParamters.pInvoiceID, "ID", out _RowCount);
            string _InvoiceTypeCode = objCInvoices_GetInvoiceTypeCode.lstCVarvwInvoices[0].InvoiceTypeCode;
            CInvoices objCInvoices_CheckIsPosted = new CInvoices();
            objCInvoices_CheckIsPosted.GetListPaging(999999, 1, "WHERE IsApproved=0 AND ID=" + pSaveParamters.pInvoiceID, "ID", out _RowCount);
            if (objCInvoices_CheckIsPosted.lstCVarInvoices.Count == 1 //not posted so update 
                && !(pSaveParamters.pCurrencyID != pDefaultCurrencyID && pSaveParamters.pExchangeRate == 1)
                )
            {

                #region Update Receivables
                CReceivables objCReceivables = new CReceivables();

                #region set InvoiceIDs to null to handle deleted items
                if (pSaveParamters.pIsRemoveItems)
                {
                    pUpdateClause = (_InvoiceTypeCode == "DRAFT" ? "DraftInvoiceID" : "InvoiceID") + "= NULL ";
                    #region ensure receivables are correct
                    pUpdateClause += " , AmountWithoutVAT = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2)" + " \n";
                    pUpdateClause += " , TaxPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID)" + " \n";
                    pUpdateClause += " , TaxAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID),2)" + " \n";
                    pUpdateClause += " , DiscountPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID)" + " \n";
                    pUpdateClause += " , DiscountAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),2)" + " \n";
                    pUpdateClause += " , SaleAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2)" + " \n"
                                  + " + (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID),0), 2))" + " \n"
                                  + " - (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),0), 2))" + " \n";
                    #endregion ensure receivables are correct
                    pUpdateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                    pUpdateClause += " , ModificationDate = GETDATE() ";
                    pUpdateClause += " WHERE " + (_InvoiceTypeCode == "DRAFT" ? "DraftInvoiceID=" : "InvoiceID=") + pSaveParamters.pInvoiceID.ToString() + " AND ID NOT IN (" + pSaveParamters.pSelectedReceivablesIDsToUpdate + ")";
                    checkException = objCReceivables.UpdateList(pUpdateClause);
                }
                #endregion
                int NumberOfRows = pSaveParamters.pSelectedReceivablesIDsToUpdate.Split(',').Length;

                if (pSaveParamters.pSelectedReceivablesIDsToUpdate != "0") //if 0 this means all items are removed
                    for (int i = 0; i < NumberOfRows; i++)
                    {
                        pUpdateClause = " POrC = " + (pSaveParamters.pPOrCList.Split(',')[i] == "0" ? " NULL " : pSaveParamters.pPOrCList.Split(',')[i]);
                        pUpdateClause += " , MeasurementID = " + (pSaveParamters.pUOMList.Split(',')[i] == "0" ? " NULL " : pSaveParamters.pUOMList.Split(',')[i]);
                        pUpdateClause += " , Quantity = " + pSaveParamters.pQuantityList.Split(',')[i];
                        pUpdateClause += " , SalePrice = " + pSaveParamters.pSalePriceList.Split(',')[i];

                        pUpdateClause += " , AmountWithoutVAT = " + (decimal.Parse(pSaveParamters.pQuantityList.Split(',')[i]) * decimal.Parse(pSaveParamters.pSalePriceList.Split(',')[i]));
                        pUpdateClause += " , TaxeTypeID = " + (pSaveParamters.pInvoiceItemTaxTypeIDList.Split(',')[i] == "0" ? " NULL " : pSaveParamters.pInvoiceItemTaxTypeIDList.Split(',')[i]);
                        pUpdateClause += " , TaxPercentage = " + pSaveParamters.pInvoiceItemTaxPercentageList.Split(',')[i];
                        pUpdateClause += " , TaxAmount = " + pSaveParamters.pInvoiceItemTaxAmountList.Split(',')[i];

                        pUpdateClause += " , SaleAmount = " + pSaveParamters.pSaleAmountList.Split(',')[i];
                        pUpdateClause += " , ExchangeRate = " + pSaveParamters.pExchangeRateList.Split(',')[i];
                        pUpdateClause += " , CurrencyID = " + (pSaveParamters.pCurrencyList.Split(',')[i] == "0" ? " NULL " : pSaveParamters.pCurrencyList.Split(',')[i]);
                        pUpdateClause += " , ViewOrder = " + pSaveParamters.pViewOrderList.Split(',')[i];
                        //the next line is used just in case i am updating invoice items
                        pUpdateClause += pSaveParamters.pInvoiceID > 0 ? (_InvoiceTypeCode == "DRAFT" ? ",DraftInvoiceID=" : ",InvoiceID=") + pSaveParamters.pInvoiceID.ToString() : "";
                        pUpdateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                        pUpdateClause += " , ModificationDate = GETDATE() ";

                        pUpdateClause += " WHERE ID = " + pSaveParamters.pSelectedReceivablesIDsToUpdate.Split(',')[i];
                        checkException = objCReceivables.UpdateList(pUpdateClause);
                        
                    }
                #endregion Update Receivables
                #region Update InvoiceHeader
                pUpdateClause = " OperationID = " + (pSaveParamters.pOperationID == 0 ? " NULL " : pSaveParamters.pOperationID.ToString());
                pUpdateClause += " , OperationPartnerID = " + (pSaveParamters.pOperationPartnerID == 0 ? " NULL " : pSaveParamters.pOperationPartnerID.ToString());
                pUpdateClause += " , AddressID = " + (pSaveParamters.pAddressID == 0 ? " NULL " : pSaveParamters.pAddressID.ToString());
                pUpdateClause += " , CustomerReference = " + (pSaveParamters.pCustomerReference == "0" ? " NULL " : "'" + pSaveParamters.pCustomerReference.ToString() + "'");
                pUpdateClause += " , PaymentTermID = " + (pSaveParamters.pPaymentTermID == 0 ? " NULL " : pSaveParamters.pPaymentTermID.ToString());
                pUpdateClause += " , CurrencyID = " + (pSaveParamters.pCurrencyID == 0 ? " NULL " : pSaveParamters.pCurrencyID.ToString());
                pUpdateClause += " , ExchangeRate = " + (pSaveParamters.pExchangeRate == 0 ? " NULL " : pSaveParamters.pExchangeRate.ToString());
                pUpdateClause += " , InvoiceDate = '" + Forwarding.MvcApp.Helpers.DateFunctionsController.GetYYYYMMDDFormat(pSaveParamters.pInvoiceIssueDate, 1) + "'";
                pUpdateClause += " , DueDate = '" + Forwarding.MvcApp.Helpers.DateFunctionsController.GetYYYYMMDDFormat(pSaveParamters.pInvoiceDueDate, 1) + "'";

                //pUpdateClause += " , AmountWithoutVAT = "; //its calculated with update or delete before calling the controller
                pUpdateClause += " , AmountWithoutVAT = ROUND((SELECT SUM(ISNULL(SalePrice,0) * ISNULL(Quantity,1))-" + pSaveParamters.pFixedDiscount + " FROM Receivables WHERE " + (_InvoiceTypeCode == "DRAFT" ? "DraftInvoiceID=" : "InvoiceID=") + pSaveParamters.pInvoiceID.ToString() + " AND IsDeleted=0),2)";
                pUpdateClause += " , TaxTypeID = " + (pSaveParamters.pTaxTypeID == 0 ? " NULL " : pSaveParamters.pTaxTypeID.ToString());
                pUpdateClause += " , TaxPercentage = " + (pSaveParamters.pTaxPercentage == 0 ? " NULL " : pSaveParamters.pTaxPercentage.ToString());
                pUpdateClause += " , TaxAmount = " + (pSaveParamters.pTaxAmount == 0 ? " NULL " : pSaveParamters.pTaxAmount.ToString());
                pUpdateClause += " , DiscountTypeID = " + (pSaveParamters.pDiscountTypeID == 0 ? " NULL " : pSaveParamters.pDiscountTypeID.ToString());
                pUpdateClause += " , DiscountPercentage = " + (pSaveParamters.pDiscountPercentage == 0 ? " NULL " : pSaveParamters.pDiscountPercentage.ToString());
                pUpdateClause += " , DiscountAmount = " + (pSaveParamters.pDiscountAmount == 0 ? " NULL " : pSaveParamters.pDiscountAmount.ToString());
                pUpdateClause += " , FixedDiscount = " + (pSaveParamters.pFixedDiscount == 0 ? " NULL " : pSaveParamters.pFixedDiscount.ToString());

                pUpdateClause += " , Amount = " + pSaveParamters.pAmount.ToString();
                //pUpdateClause += " , PaidAmount = " + pPaidAmount.ToString();
                //pUpdateClause += " , RemainingAmount = " + pRemainingAmount.ToString();
                pUpdateClause += " , InvoiceStatusID = " + (pSaveParamters.pInvoiceStatusID == 0 ? " NULL " : pSaveParamters.pInvoiceStatusID.ToString());
                pUpdateClause += " , IsApproved = " + (pSaveParamters.pIsApproved ? " 1 " : " 0 ");
                pUpdateClause += " , LeftSignature = " + (pSaveParamters.pLeftSignature == "0" ? " NULL " : ("N'" + pSaveParamters.pLeftSignature.ToString() + "'"));
                pUpdateClause += " , MiddleSignature = " + (pSaveParamters.pMiddleSignature == "0" ? " NULL " : ("N'" + pSaveParamters.pMiddleSignature.ToString() + "'"));
                pUpdateClause += " , RightSignature = " + (pSaveParamters.pRightSignature == "0" ? " NULL " : ("N'" + pSaveParamters.pRightSignature.ToString() + "'"));
                pUpdateClause += " , GRT = " + (pSaveParamters.pGRT == "0" ? " NULL " : ("N'" + pSaveParamters.pGRT.ToString() + "'"));
                pUpdateClause += " , DWT = " + (pSaveParamters.pDWT == "0" ? " NULL " : ("N'" + pSaveParamters.pDWT.ToString() + "'"));
                pUpdateClause += " , NRT = " + (pSaveParamters.pNRT == "0" ? " NULL " : ("N'" + pSaveParamters.pNRT.ToString() + "'"));
                pUpdateClause += " , LOA = " + (pSaveParamters.pLOA == "0" ? " NULL " : ("N'" + pSaveParamters.pLOA.ToString() + "'"));
                pUpdateClause += " , EditableNotes = " + (pSaveParamters.pEditableNotes == "0" ? " NULL " : ("N'" + pSaveParamters.pEditableNotes.ToString() + "'"));
                pUpdateClause += " , RoutingID = " + (pSaveParamters.pRoutingID == 0 ? " NULL " : ("N'" + pSaveParamters.pRoutingID.ToString() + "'"));
                pUpdateClause += " , TransactionTypeID = " + (pSaveParamters.pTransactionTypeID == 0 ? " NULL " : ("N'" + pSaveParamters.pTransactionTypeID.ToString() + "'"));
                if (pSaveParamters.pUpdateRelatedToInvoiceID)
                    pUpdateClause += " , RelatedToInvoiceID = " + (pSaveParamters.pRelatedToInvoiceID == 0 ? " NULL " : ("N'" + pSaveParamters.pRelatedToInvoiceID.ToString() + "'"));

                pUpdateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                pUpdateClause += " , ModificationDate = GETDATE() ";
                pUpdateClause += " WHERE ID = " + pSaveParamters.pInvoiceID.ToString();
                checkException = objCInvoices.UpdateList(pUpdateClause);
                #endregion Update InvoiceHeader

                if (checkException == null)
                {
                    _result = true;
                    #region Update Invoice totals at server side to fix any connection problem
                    //SET Tax, Discount & Total Amount after setting the AmountWithoutVAT
                    pUpdateClause = " TaxAmount = ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(TaxPercentage, 0) / 100),2)" + "\n";
                    pUpdateClause += " , DiscountAmount = ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2)";
                    if (objCDefaults.lstCVarDefaults[0].IsTaxOnItems)
                        pUpdateClause += " , Amount = - ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2) + ROUND((SELECT SUM(ISNULL(SaleAmount,0))-" + pSaveParamters.pFixedDiscount + " FROM Receivables WHERE " + (_InvoiceTypeCode == "DRAFT" ? "DraftInvoiceID" : "InvoiceID") + " = " + pSaveParamters.pInvoiceID + " AND IsDeleted=0),2)";
                    else
                        pUpdateClause += " , Amount = ROUND((ISNULL(AmountWithoutVAT, 0) + ROUND( (ISNULL(AmountWithoutVAT, 0) * ISNULL(TaxPercentage, 0) / 100),2) - ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2)),2)";
                    pUpdateClause += " WHERE ID = " + pSaveParamters.pInvoiceID.ToString();
                    checkException = objCInvoices.UpdateList(pUpdateClause);
                    #endregion Update Invoice totals at server side to fix any connection problem

                    #region update exchange rate for receivables
                    pUpdateClause = " ExchangeRate = (SELECT ExchangeRate FROM Invoices WHERE ID=" + pSaveParamters.pInvoiceID.ToString() + ")";
                    #region ensure receivables are correct
                    pUpdateClause += " , AmountWithoutVAT = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2)" + " \n";
                    pUpdateClause += " , TaxPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID)" + " \n";
                    pUpdateClause += " , TaxAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID),2)" + " \n";
                    pUpdateClause += " , DiscountPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID)" + " \n";
                    pUpdateClause += " , DiscountAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),2)" + " \n";
                    pUpdateClause += " , SaleAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2)" + " \n"
                                  + " + (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID),0), 2))" + " \n"
                                  + " - (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),0), 2))" + " \n";
                    #endregion ensure receivables are correct
                    pUpdateClause += " WHERE " + (_InvoiceTypeCode == "DRAFT" ? "DraftInvoiceID=" : "InvoiceID=") + pSaveParamters.pInvoiceID.ToString();
                    checkException = objCReceivables.UpdateList(pUpdateClause);
                    #endregion update exchange rate for receivables

                    #region update AccPartnerBalance
                    //pUpdateClause = "PartnerTypeID=" + pPartnerTypeID.ToString();
                    //pUpdateClause += " , CustomerID=" + GetPartnerID(1, pPartnerTypeID, pPartnerID);
                    //pUpdateClause += " , AgentID=" + GetPartnerID(2, pPartnerTypeID, pPartnerID);
                    //pUpdateClause += " , ShippingAgentID=" + GetPartnerID(3, pPartnerTypeID, pPartnerID);
                    //pUpdateClause += " , CustomsClearanceAgentID=" + GetPartnerID(4, pPartnerTypeID, pPartnerID);
                    //pUpdateClause += " , ShippingLineID=" + GetPartnerID(5, pPartnerTypeID, pPartnerID);
                    //pUpdateClause += " , AirlineID=" + GetPartnerID(6, pPartnerTypeID, pPartnerID);
                    //pUpdateClause += " , TruckerID=" + GetPartnerID(7, pPartnerTypeID, pPartnerID);
                    //pUpdateClause += " , SupplierID=" + GetPartnerID(8, pPartnerTypeID, pPartnerID);
                    //pUpdateClause += " , DebitAmount=" + pAmount;
                    //pUpdateClause += " , CurrencyID=" + pCurrencyID;
                    //pUpdateClause += " , ExchangeRate=" + pExchangeRate;
                    //pUpdateClause += " , BalCurLocalExRate=" + pExchangeRate;
                    //pUpdateClause += " , InvCurLocalExRate=" + pExchangeRate;
                    //pUpdateClause += " , Notes=N'Action from Invoices.'";
                    //pUpdateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                    //pUpdateClause += " , ModificationDate = GETDATE() ";
                    //pUpdateClause += " WHERE InvoiceID=" + pInvoiceID + " AND InvoicePaymentDetailsID IS NULL ";
                    //CAccPartnerBalance objCAccPartnerBalance = new CAccPartnerBalance();
                    //objCAccPartnerBalance.UpdateList(pUpdateClause);
                    //////for case of remove i recalculate
                    ////pUpdateClause = " Amount = ISNULL(AmountWithoutVAT, 0) + ISNULL(TaxAmount, 0) - ISNULL(DiscountAmount, 0)";
                    ////pUpdateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                    ////pUpdateClause += " , ModificationDate = GETDATE() ";
                    ////pUpdateClause += " WHERE ID = " + pInvoiceID.ToString();
                    ////checkException = objCInvoices.UpdateList(pUpdateClause);
                    #endregion update AccPartnerBalance
                    #region QRCode

                    objCInvoices.GetList("Where ID=" + pSaveParamters.pInvoiceID.ToString());

                    if (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "WAV" || objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "SEC")
                        CreateQRCode(pSaveParamters.pInvoiceID.ToString(), objCDefaults.lstCVarDefaults[0].CompanyName
                        , objCDefaults.lstCVarDefaults[0].VatIDNo, objCInvoices.lstCVarInvoices[0].InvoiceDate
                        , objCInvoices.lstCVarInvoices[0].Amount);
                    #endregion
                }
            }
            return new object[] { _result, pSaveParamters.pInvoiceID };
        }

        [HttpGet, HttpPost]
        public object[] AddItems(Int64 pInvoiceID, Int64 pOperationID, Int64 pOperationPartnerID, Int64 pAddressID, string pCustomerReference, int pPaymentTermID, string pInvoiceIssueDate, string pInvoiceDueDate, Int32 pTaxTypeID, decimal pTaxPercentage, Int32 pDiscountTypeID, decimal pDiscountPercentage,decimal pFixedDiscount, Int64 pRoutingID, int pInvoiceStatusID, bool pIsApproved, string pSelectedItemsIDs)
        {
            Exception checkException = null;
            string pUpdateClause = "";
            string pWhereClause = "";
            int _RowCount = 0;
            CDefaults objCDefaults = new CDefaults();
            objCDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa
            CvwInvoices objCInvoices_GetInvoiceTypeCode = new CvwInvoices();
            objCInvoices_GetInvoiceTypeCode.GetListPaging(999999, 1, "WHERE ID=" + pInvoiceID, "ID", out _RowCount);
            string _InvoiceTypeCode = objCInvoices_GetInvoiceTypeCode.lstCVarvwInvoices[0].InvoiceTypeCode;
            CReceivables objCReceivables = new CReceivables();
            foreach (var currentID in pSelectedItemsIDs.Split(','))
            {
                //i am sure i ve at least 1 selectedItemID isa
                pWhereClause += (pWhereClause == "" ? " WHERE ID = " + currentID.ToString()
                    : " OR ID = " + currentID.ToString());
            }
            pUpdateClause = (_InvoiceTypeCode == "DRAFT" ? "DraftInvoiceID=" : "InvoiceID=") + pInvoiceID.ToString();
            #region ensure receivables are correct
            pUpdateClause += " , AmountWithoutVAT = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2)" + " \n";
            pUpdateClause += " , TaxPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID)" + " \n";
            pUpdateClause += " , TaxAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID),2)" + " \n";
            pUpdateClause += " , DiscountPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID)" + " \n";
            pUpdateClause += " , DiscountAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),2)" + " \n";
            pUpdateClause += " , SaleAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2)" + " \n"
                          + " + (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID),0), 2))" + " \n"
                          + " - (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),0), 2))" + " \n";
            #endregion ensure receivables are correct
            pUpdateClause += " , ExchangeRate = (SELECT ExchangeRate FROM Invoices WHERE ID=" + pInvoiceID.ToString() + ")";
            pUpdateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
            pUpdateClause += " , ModificationDate = GETDATE() ";
            pUpdateClause += pWhereClause;
            checkException = objCReceivables.UpdateList(pUpdateClause);
            if (checkException == null) //update invoice amount
            {
                CInvoices objCInvoices = new CInvoices();

                pUpdateClause = " OperationID = " + (pOperationID == 0 ? " NULL " : pOperationID.ToString());
                pUpdateClause += " , OperationPartnerID = " + (pOperationPartnerID == 0 ? " NULL " : pOperationPartnerID.ToString());
                pUpdateClause += " , AddressID = " + (pAddressID == 0 ? " NULL " : pAddressID.ToString());
                pUpdateClause += " , CustomerReference = " + (pCustomerReference == "0" ? " NULL " : "'" + pCustomerReference.ToString() + "'");
                //pUpdateClause += " , PaymentTermID = " + (pPaymentTermID == 0 ? " NULL " : pPaymentTermID.ToString());
                //pUpdateClause += " , CurrencyID = " + (pCurrencyID == 0 ? " NULL " : pCurrencyID.ToString());
                //pUpdateClause += " , ExchangeRate = " + (pExchangeRate == 0 ? " NULL " : pExchangeRate.ToString());
                //pUpdateClause += " , InvoiceDate = '" + Forwarding.MvcApp.Helpers.DateFunctionsController.GetYYYYMMDDFormat(pInvoiceIssueDate, 1) + "'";
                //pUpdateClause += " , DueDate = '" + Forwarding.MvcApp.Helpers.DateFunctionsController.GetYYYYMMDDFormat(pInvoiceDueDate, 1) + "'";
                pUpdateClause += " , AmountWithoutVAT = ROUND((SELECT SUM(ISNULL(SalePrice,0) * ISNULL(Quantity,1))-" + pFixedDiscount + " FROM Receivables WHERE " + (_InvoiceTypeCode == "DRAFT" ? "DraftInvoiceID=" : "InvoiceID=") + pInvoiceID.ToString() + " AND IsDeleted=0),2)";
                pUpdateClause += " , TaxTypeID = " + (pTaxTypeID == 0 ? " NULL " : pTaxTypeID.ToString());
                pUpdateClause += " , TaxPercentage = " + (pTaxPercentage == 0 ? " NULL " : pTaxPercentage.ToString());
                pUpdateClause += " , DiscountTypeID = " + (pDiscountTypeID == 0 ? " NULL " : pDiscountTypeID.ToString());
                pUpdateClause += " , DiscountPercentage = " + (pDiscountPercentage == 0 ? " NULL " : pDiscountPercentage.ToString());
                pUpdateClause += " , FixedDiscount = " + (pFixedDiscount == 0 ? " NULL " : pFixedDiscount.ToString());
                pUpdateClause += " , RoutingID = " + (pRoutingID == 0 ? " NULL " : ("N'" + pRoutingID.ToString() + "'"));
                pUpdateClause += " , InvoiceStatusID = " + (pInvoiceStatusID == 0 ? " NULL " : pInvoiceStatusID.ToString());
                pUpdateClause += " , IsApproved = " + (pIsApproved ? " 1 " : " 0 ");
                pUpdateClause += " WHERE ID = " + pInvoiceID.ToString();
                checkException = objCInvoices.UpdateList(pUpdateClause);

                //SET Tax, Discount & Total Amount after setting the AmountWithoutVAT
                pUpdateClause = " TaxAmount = ISNULL(AmountWithoutVAT, 0) * ISNULL(TaxPercentage, 0) / 100";
                pUpdateClause += " , DiscountAmount = ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100";
                if (objCDefaults.lstCVarDefaults[0].IsTaxOnItems)
                    pUpdateClause += " , Amount = - ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2) + ROUND((SELECT SUM(ISNULL(SaleAmount,0))-" + pFixedDiscount + " FROM Receivables WHERE " + (_InvoiceTypeCode == "DRAFT" ? "DraftInvoiceID" : "InvoiceID") + " = " + pInvoiceID + " AND IsDeleted=0),2)";
                else
                    pUpdateClause += " , Amount = ROUND((ISNULL(AmountWithoutVAT, 0) + ROUND( (ISNULL(AmountWithoutVAT, 0) * ISNULL(TaxPercentage, 0) / 100),2) - ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2)),2)";
                //pUpdateClause += " , Amount = ISNULL(AmountWithoutVAT, 0) + (ISNULL(AmountWithoutVAT, 0) * ISNULL(TaxPercentage, 0) / 100) - (ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100)";
                pUpdateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                pUpdateClause += " , ModificationDate = GETDATE() ";
                pUpdateClause += " WHERE ID = " + pInvoiceID.ToString();
                checkException = objCInvoices.UpdateList(pUpdateClause);
                if (checkException == null)
                {
                    #region Update AccPartnerBalance
                    //CvwInvoices objCvwInvoices = new CvwInvoices();
                    //int _RowCount = 0;
                    //objCvwInvoices.GetListPaging(1, 1, ("WHERE ID=" + pInvoiceID.ToString()), "ID", out _RowCount);

                    //pUpdateClause = "PartnerTypeID=" + objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID.ToString();
                    //pUpdateClause += " , CustomerID=" + GetPartnerID(1, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                    //pUpdateClause += " , AgentID=" + GetPartnerID(2, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                    //pUpdateClause += " , ShippingAgentID=" + GetPartnerID(3, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                    //pUpdateClause += " , CustomsClearanceAgentID=" + GetPartnerID(4, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                    //pUpdateClause += " , ShippingLineID=" + GetPartnerID(5, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                    //pUpdateClause += " , AirlineID=" + GetPartnerID(6, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                    //pUpdateClause += " , TruckerID=" + GetPartnerID(7, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                    //pUpdateClause += " , SupplierID=" + GetPartnerID(8, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                    //pUpdateClause += " , DebitAmount=" + objCvwInvoices.lstCVarvwInvoices[0].Amount;
                    //pUpdateClause += " , CurrencyID=" + objCvwInvoices.lstCVarvwInvoices[0].CurrencyID;
                    //pUpdateClause += " , ExchangeRate=" + objCvwInvoices.lstCVarvwInvoices[0].ExchangeRate;
                    //pUpdateClause += " , BalCurLocalExRate=" + objCvwInvoices.lstCVarvwInvoices[0].ExchangeRate;
                    //pUpdateClause += " , InvCurLocalExRate=" + objCvwInvoices.lstCVarvwInvoices[0].ExchangeRate;
                    //pUpdateClause += " , Notes=N'Action from Invoices.'";
                    //pUpdateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                    //pUpdateClause += " , ModificationDate = GETDATE() ";
                    //pUpdateClause += " WHERE InvoiceID=" + pInvoiceID + " AND InvoicePaymentDetailsID IS NULL ";
                    //CAccPartnerBalance objCAccPartnerBalance = new CAccPartnerBalance();
                    //objCAccPartnerBalance.UpdateList(pUpdateClause);
                    #endregion Update AccPartnerBalance
                }
            }

            return new object[] { };
        }

        [HttpGet, HttpPost]
        public object[] Delete(String pInvoicesIDs)
        {
            string returnedMessage = "";
            Exception checkException = null;
            string[] strArrayInvoicesIDs = pInvoicesIDs.Split(',');
            string pUpdateClause = "";
            CReceivables objCReceivables = new CReceivables();
            CInvoices objCInvoices = new CInvoices();
            CvwInvoices objCvwInvoices = new CvwInvoices();
            CInvoiceTypes objCInvoiceTypes = new CInvoiceTypes();
            CDefaults objCDefaults = new CDefaults();
            int _RowCount = 0;
            CInvoices objCInvoices_IsPosted = new CInvoices();
            CInvoices objCInvoices_CreditMemo = new CInvoices();
            CReceivables objCReceivables_CreditMemo = new CReceivables();
            int CancelledTransportOrderID = 80;
            checkException = objCDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount);
            checkException = objCInvoices_IsPosted.GetListPaging(99999, 1, "WHERE IsApproved=1 AND IsDeleted=0 AND ID IN(0," + pInvoicesIDs + ")", "ID", out _RowCount);
            #region Cancel UnApproved Invoices
            if (objCInvoices_IsPosted.lstCVarInvoices.Count == 0) //no posted invoices
            {
                checkException = objCInvoices.GetListPaging(999999, 1, "WHERE IsDeleted=0 AND IsApproved=0 AND ID IN (" + pInvoicesIDs + ")", "ID", out _RowCount); //if deleted dont create again
                if (objCInvoices.lstCVarInvoices[0].Is3PL) //Delete receivables
                {
                    objCReceivables.DeleteList("WHERE InvoiceID=" + objCInvoices.lstCVarInvoices[0].ID);
                }
                else //just release receivables to its operation
                {
                    pUpdateClause = " InvoiceID = null ";
                    //pUpdateClause += " , IsDeleted=1 ";
                    #region ensure receivables are correct
                    pUpdateClause += " , AmountWithoutVAT = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2)" + " \n";
                    pUpdateClause += " , TaxPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID)" + " \n";
                    pUpdateClause += " , TaxAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID),2)" + " \n";
                    pUpdateClause += " , DiscountPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID)" + " \n";
                    pUpdateClause += " , DiscountAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),2)" + " \n";
                    pUpdateClause += " , SaleAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2)" + " \n"
                                  + " + (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID),0), 2))" + " \n"
                                  + " - (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),0), 2))" + " \n";
                    #endregion ensure receivables are correct
                    pUpdateClause += " WHERE InvoiceID IN (0," + pInvoicesIDs + ")";
                    checkException = objCReceivables.UpdateList(pUpdateClause);
                    pUpdateClause = " DraftInvoiceID = null ";
                    //pUpdateClause += " , IsDeleted=1 ";
                    #region ensure receivables are correct
                    pUpdateClause += " , AmountWithoutVAT = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2)" + " \n";
                    pUpdateClause += " , TaxPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID)" + " \n";
                    pUpdateClause += " , TaxAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID),2)" + " \n";
                    pUpdateClause += " , DiscountPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID)" + " \n";
                    pUpdateClause += " , DiscountAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),2)" + " \n";
                    pUpdateClause += " , SaleAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2)" + " \n"
                                  + " + (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID),0), 2))" + " \n"
                                  + " - (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),0), 2))" + " \n";
                    #endregion ensure receivables are correct
                    pUpdateClause += " WHERE DraftInvoiceID IN (0," + pInvoicesIDs + ")";
                    checkException = objCReceivables.UpdateList(pUpdateClause);

                    #region Set ChildInvoiceID= null if exists
                    pUpdateClause = "ChildInvoiceID = null ";
                    pUpdateClause += "WHERE ChildInvoiceID IN (0," + pInvoicesIDs + ")";
                    checkException = objCInvoices.UpdateList(pUpdateClause);
                    #endregion Set ChildInvoiceID= null if exists

                    pUpdateClause = " IsDeleted = 1, Is3PL=0 ";
                    pUpdateClause += ",OperationPartnerID = null ";
                    //pUpdateClause += ",Amount = 0 ";
                    //pUpdateClause += ",AmountWithoutVAT = 0 ";
                    //pUpdateClause += ",TaxTypeID = null";
                    //pUpdateClause += ",TaxPercentage = null";
                    //pUpdateClause += ",DiscountTypeID = null";
                    //pUpdateClause += ",DiscountPercentage = null";
                    //pUpdateClause += ",TaxAmount = null";
                    //pUpdateClause += ",DiscountAmount = null";
                    //pUpdateClause += ",FixedDiscount = null ";
                    pUpdateClause += ",OperationContainersAndPackagesID = null";
                    pUpdateClause += ",ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                    pUpdateClause += ",ModificationDate = GETDATE() ";
                    pUpdateClause += "WHERE ID IN (0," + pInvoicesIDs + ")";
                    checkException = objCInvoices.UpdateList(pUpdateClause);

                    if (checkException != null) // an exception is caught in the model
                    {
                        returnedMessage = checkException.Message;
                    }
                    else //deleted successfully
                    {
                        CRoutings objCRoutings = new CRoutings();
                        checkException = objCRoutings.UpdateList("InvoiceID=null, RoadNumber=N'InvoiceIDs " + pInvoicesIDs + " deleted by " + WebSecurity.CurrentUserName + " at ' + CONVERT(VARCHAR(20),GETDATE(), 103) WHERE InvoiceID IN(0," + pInvoicesIDs + ")");
                    }
                } //EOF else //just release receivables to its operation
            } //if (objCInvoices_IsPosted.lstCVarInvoices.Count == 0) //no posted invoices
            #endregion Cancel UnApproved Invoices
            #region Cancel Approved Invoices
            else if (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "GBL")//Approved: Don't apply to any company without contacting Kareem
            {

                checkException = objCInvoiceTypes.GetListPaging(1, 1, "WHERE Code=N'CREDITMEMO'", "ID", out _RowCount);
                if (objCInvoiceTypes.lstCVarInvoiceTypes.Count > 0)
                {
                    CVarInvoices objCVarInvoices_CreditMemo = new CVarInvoices();
                    checkException = objCInvoices.GetListPaging(999999, 1, "WHERE IsDeleted=0 AND ID IN (" + pInvoicesIDs + ")", "ID", out _RowCount); //if deleted dont create again
                    checkException = objCvwInvoices.GetListPaging(999999, 1, "WHERE IsDeleted=0 AND IsApproved=1 AND ID IN (" + pInvoicesIDs + ")", "ID", out _RowCount); //if deleted dont create again
                    if (objCInvoices.lstCVarInvoices.Count > 0)
                    {
                        pUpdateClause = " OperationID = null "; //pUpdateClause = " InvoiceID = null ";
                        pUpdateClause += " , HouseBillID=null ";
                        pUpdateClause += " ,IsDeleted = 1 ";
                        #region ensure receivables are correct
                        pUpdateClause += " , AmountWithoutVAT = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2)" + " \n";
                        pUpdateClause += " , TaxPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID)" + " \n";
                        pUpdateClause += " , TaxAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID),2)" + " \n";
                        pUpdateClause += " , DiscountPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID)" + " \n";
                        pUpdateClause += " , DiscountAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),2)" + " \n";
                        pUpdateClause += " , SaleAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2)" + " \n"
                                      + " + (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID),0), 2))" + " \n"
                                      + " - (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),0), 2))" + " \n";
                        #endregion ensure receivables are correct
                        pUpdateClause += " WHERE InvoiceID IN (0," + pInvoicesIDs + ")";
                        checkException = objCReceivables.UpdateList(pUpdateClause);
                        pUpdateClause = " OperationID = null "; //pUpdateClause = " DraftInvoiceID = null ";
                        pUpdateClause += " , HouseBillID=null ";
                        pUpdateClause += " ,IsDeleted = 1 ";
                        #region ensure receivables are correct
                        pUpdateClause += " , AmountWithoutVAT = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2)" + " \n";
                        pUpdateClause += " , TaxPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID)" + " \n";
                        pUpdateClause += " , TaxAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID),2)" + " \n";
                        pUpdateClause += " , DiscountPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID)" + " \n";
                        pUpdateClause += " , DiscountAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),2)" + " \n";
                        pUpdateClause += " , SaleAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2)" + " \n"
                                      + " + (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID),0), 2))" + " \n"
                                      + " - (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),0), 2))" + " \n";
                        #endregion ensure receivables are correct
                        pUpdateClause += " WHERE DraftInvoiceID IN (0," + pInvoicesIDs + ")";
                        checkException = objCReceivables.UpdateList(pUpdateClause);

                        #region Create CREDIT MEMO Invoice Header and reciprocal receivables
                        objCVarInvoices_CreditMemo.InvoiceTypeID = objCInvoiceTypes.lstCVarInvoiceTypes[0].ID;
                        objCVarInvoices_CreditMemo.OperationID = objCInvoices.lstCVarInvoices[0].OperationID;
                        objCVarInvoices_CreditMemo.OperationPartnerID = objCInvoices.lstCVarInvoices[0].OperationPartnerID;
                        objCVarInvoices_CreditMemo.AddressID = objCInvoices.lstCVarInvoices[0].AddressID;
                        objCVarInvoices_CreditMemo.PrintedAddress = objCInvoices.lstCVarInvoices[0].PrintedAddress;
                        objCVarInvoices_CreditMemo.CustomerReference = objCInvoices.lstCVarInvoices[0].CustomerReference;
                        objCVarInvoices_CreditMemo.PaymentTermID = objCInvoices.lstCVarInvoices[0].PaymentTermID;
                        objCVarInvoices_CreditMemo.CurrencyID = objCInvoices.lstCVarInvoices[0].CurrencyID;
                        objCVarInvoices_CreditMemo.ExchangeRate = objCInvoices.lstCVarInvoices[0].ExchangeRate;
                        objCVarInvoices_CreditMemo.InvoiceDate = DateTime.Now; //objCInvoices.lstCVarInvoices[0].InvoiceDate;
                        objCVarInvoices_CreditMemo.DueDate = DateTime.Now; //objCInvoices.lstCVarInvoices[0].DueDate;
                        objCVarInvoices_CreditMemo.AmountWithoutVAT = objCInvoices.lstCVarInvoices[0].AmountWithoutVAT;
                        objCVarInvoices_CreditMemo.TaxTypeID = objCInvoices.lstCVarInvoices[0].TaxTypeID;
                        objCVarInvoices_CreditMemo.TaxPercentage = objCInvoices.lstCVarInvoices[0].TaxPercentage;
                        objCVarInvoices_CreditMemo.TaxAmount = objCInvoices.lstCVarInvoices[0].TaxAmount;
                        objCVarInvoices_CreditMemo.DiscountTypeID = objCInvoices.lstCVarInvoices[0].DiscountTypeID;
                        objCVarInvoices_CreditMemo.DiscountPercentage = objCInvoices.lstCVarInvoices[0].DiscountPercentage;
                        objCVarInvoices_CreditMemo.DiscountAmount = objCInvoices.lstCVarInvoices[0].DiscountAmount;
                        objCVarInvoices_CreditMemo.Amount = objCInvoices.lstCVarInvoices[0].Amount;
                        objCVarInvoices_CreditMemo.PaidAmount = objCInvoices.lstCVarInvoices[0].PaidAmount;
                        objCVarInvoices_CreditMemo.RemainingAmount = objCInvoices.lstCVarInvoices[0].RemainingAmount;
                        objCVarInvoices_CreditMemo.InvoiceStatusID = objCInvoices.lstCVarInvoices[0].InvoiceStatusID;
                        objCVarInvoices_CreditMemo.IsApproved = false;
                        objCVarInvoices_CreditMemo.IsDeleted = false;
                        objCVarInvoices_CreditMemo.ApprovingUserID = objCInvoices.lstCVarInvoices[0].ApprovingUserID;
                        objCVarInvoices_CreditMemo.CreatorUserID = WebSecurity.CurrentUserId;
                        objCVarInvoices_CreditMemo.CreationDate = DateTime.Now;
                        objCVarInvoices_CreditMemo.ModificatorUserID = WebSecurity.CurrentUserId;
                        objCVarInvoices_CreditMemo.ModificationDate = DateTime.Now;
                        objCVarInvoices_CreditMemo.LeftSignature = objCInvoices.lstCVarInvoices[0].LeftSignature;
                        objCVarInvoices_CreditMemo.MiddleSignature = objCInvoices.lstCVarInvoices[0].MiddleSignature;
                        objCVarInvoices_CreditMemo.RightSignature = objCInvoices.lstCVarInvoices[0].RightSignature;
                        objCVarInvoices_CreditMemo.JVID = 0;
                        objCVarInvoices_CreditMemo.GRT = objCInvoices.lstCVarInvoices[0].GRT;
                        objCVarInvoices_CreditMemo.DWT = objCInvoices.lstCVarInvoices[0].DWT;
                        objCVarInvoices_CreditMemo.NRT = objCInvoices.lstCVarInvoices[0].NRT;
                        objCVarInvoices_CreditMemo.LOA = objCInvoices.lstCVarInvoices[0].LOA;
                        objCVarInvoices_CreditMemo.RoutingID = objCInvoices.lstCVarInvoices[0].RoutingID;
                        objCVarInvoices_CreditMemo.OperationContainersAndPackagesID = 0;
                        objCVarInvoices_CreditMemo.ChildInvoiceID = 0;
                        objCVarInvoices_CreditMemo.FixedDiscount = 0;
                        objCVarInvoices_CreditMemo.IsNeglectLimit = objCInvoices.lstCVarInvoices[0].IsNeglectLimit;
                        objCVarInvoices_CreditMemo.RelatedToInvoiceID = 0;
                        objCVarInvoices_CreditMemo.IsDraftApproved = false;
                        objCVarInvoices_CreditMemo.DraftApprovingUserID = 0;
                        objCVarInvoices_CreditMemo.TransactionTypeID = objCInvoices.lstCVarInvoices[0].TransactionTypeID;
                        objCVarInvoices_CreditMemo.CutOffDate = objCInvoices.lstCVarInvoices[0].CutOffDate;
                        objCVarInvoices_CreditMemo.Notes = objCvwInvoices.lstCVarvwInvoices[0].ConcatenatedInvoiceNumber;
                        objCVarInvoices_CreditMemo.Is3PL = objCInvoices.lstCVarInvoices[0].Is3PL;
                        objCVarInvoices_CreditMemo.IsFleet = objCInvoices.lstCVarInvoices[0].IsFleet;
                        objCVarInvoices_CreditMemo.EditableNotes = objCInvoices.lstCVarInvoices[0].EditableNotes;
                        objCVarInvoices_CreditMemo.IsPrintOriginal = false;
                        objCVarInvoices_CreditMemo.CancelledInvoiceID = objCInvoices.lstCVarInvoices[0].ID;
                        objCInvoices_CreditMemo.lstCVarInvoices.Add(objCVarInvoices_CreditMemo);
                        checkException = objCInvoices_CreditMemo.SaveMethod(objCInvoices_CreditMemo.lstCVarInvoices);
                        checkException = objCReceivables.GetListPaging(999999, 1, "WHERE InvoiceID IN(" + pInvoicesIDs + ")", "ID", out _RowCount);
                        for (int i = 0; i < objCReceivables.lstCVarReceivables.Count; i++)
                        {
                            CVarReceivables objCVarReceivables_CreditMemo = new CVarReceivables();
                            objCVarReceivables_CreditMemo.OperationID = objCReceivables.lstCVarReceivables[i].OperationID;
                            objCVarReceivables_CreditMemo.ChargeTypeID = objCReceivables.lstCVarReceivables[i].ChargeTypeID;
                            objCVarReceivables_CreditMemo.POrC = objCReceivables.lstCVarReceivables[i].POrC;
                            objCVarReceivables_CreditMemo.SupplierID = objCReceivables.lstCVarReceivables[i].SupplierID;
                            objCVarReceivables_CreditMemo.MeasurementID = objCReceivables.lstCVarReceivables[i].MeasurementID;
                            objCVarReceivables_CreditMemo.ContainerTypeID = objCReceivables.lstCVarReceivables[i].ContainerTypeID;
                            objCVarReceivables_CreditMemo.PackageTypeID = objCReceivables.lstCVarReceivables[i].PackageTypeID;
                            objCVarReceivables_CreditMemo.Quantity = objCReceivables.lstCVarReceivables[i].Quantity;
                            objCVarReceivables_CreditMemo.CostPrice = objCReceivables.lstCVarReceivables[i].CostAmount;
                            objCVarReceivables_CreditMemo.CostAmount = objCReceivables.lstCVarReceivables[i].CostAmount;
                            objCVarReceivables_CreditMemo.SalePrice = objCReceivables.lstCVarReceivables[i].SalePrice;
                            objCVarReceivables_CreditMemo.SaleAmount = objCReceivables.lstCVarReceivables[i].SaleAmount;
                            objCVarReceivables_CreditMemo.TaxeTypeID = objCReceivables.lstCVarReceivables[i].TaxeTypeID;
                            objCVarReceivables_CreditMemo.InvoiceID = objCVarInvoices_CreditMemo.ID;
                            objCVarReceivables_CreditMemo.AccNoteID = 0;
                            objCVarReceivables_CreditMemo.ExchangeRate = objCReceivables.lstCVarReceivables[i].ExchangeRate;
                            objCVarReceivables_CreditMemo.CurrencyID = objCReceivables.lstCVarReceivables[i].CurrencyID;
                            objCVarReceivables_CreditMemo.GeneratingQRID = objCReceivables.lstCVarReceivables[i].GeneratingQRID;
                            objCVarReceivables_CreditMemo.Notes = objCReceivables.lstCVarReceivables[i].Notes;
                            objCVarReceivables_CreditMemo.ViewOrder = objCReceivables.lstCVarReceivables[i].ViewOrder;
                            objCVarReceivables_CreditMemo.IsDeleted = true;
                            objCVarReceivables_CreditMemo.CreatorUserID = WebSecurity.CurrentUserId;
                            objCVarReceivables_CreditMemo.CreationDate = DateTime.Now;
                            objCVarReceivables_CreditMemo.ModificatorUserID = WebSecurity.CurrentUserId;
                            objCVarReceivables_CreditMemo.ModificationDate = DateTime.Now;
                            objCVarReceivables_CreditMemo.PayableID = 0;
                            objCVarReceivables_CreditMemo.IssueDate = DateTime.Now;
                            objCVarReceivables_CreditMemo.OperationContainersAndPackagesID = 0;
                            objCVarReceivables_CreditMemo.DraftInvoiceID = 0;
                            objCVarReceivables_CreditMemo.TaxAmount = objCReceivables.lstCVarReceivables[i].TaxAmount;
                            objCVarReceivables_CreditMemo.DiscountTypeID = objCReceivables.lstCVarReceivables[i].DiscountTypeID;
                            objCVarReceivables_CreditMemo.DiscountAmount = objCReceivables.lstCVarReceivables[i].DiscountAmount;
                            objCVarReceivables_CreditMemo.AmountWithoutVAT = objCReceivables.lstCVarReceivables[i].AmountWithoutVAT;
                            objCVarReceivables_CreditMemo.TaxPercentage = objCReceivables.lstCVarReceivables[i].TaxPercentage;
                            objCVarReceivables_CreditMemo.HouseBillID = objCReceivables.lstCVarReceivables[i].HouseBillID;
                            objCVarReceivables_CreditMemo.DiscountPercentage = objCReceivables.lstCVarReceivables[i].DiscountPercentage;
                            objCVarReceivables_CreditMemo.OperationVehicleID = 0;
                            objCVarReceivables_CreditMemo.TruckingOrderID = 0;
                            objCVarReceivables_CreditMemo.PreviousCutOffDate = DateTime.Parse("01/01/1900");
                            objCVarReceivables_CreditMemo.CutOffDate = DateTime.Parse("01/01/1900");
                            objCVarReceivables_CreditMemo.VehicleAgingReportID = 0;
                            objCVarReceivables_CreditMemo.CancelledReceivableID = objCReceivables.lstCVarReceivables[i].ID;
                            objCReceivables_CreditMemo.lstCVarReceivables.Add(objCVarReceivables_CreditMemo);
                        }
                        checkException = objCReceivables_CreditMemo.SaveMethod(objCReceivables_CreditMemo.lstCVarReceivables);
                        #endregion Create CREDIT MEMO Invoice Header and reciprocal receivables

                        #region Set ChildInvoiceID= null if exists
                        pUpdateClause = "ChildInvoiceID = null ";
                        pUpdateClause += "WHERE ChildInvoiceID IN (0," + pInvoicesIDs + ")";
                        checkException = objCInvoices.UpdateList(pUpdateClause);
                        #endregion Set ChildInvoiceID= null if exists

                        pUpdateClause = " IsDeleted = 1 ";
                        //pUpdateClause += ",OperationPartnerID = null "; 
                        //pUpdateClause += ",Amount = 0 ";
                        //pUpdateClause += ",AmountWithoutVAT = 0 ";
                        //pUpdateClause += ",TaxTypeID = null";
                        //pUpdateClause += ",TaxPercentage = null";
                        //pUpdateClause += ",DiscountTypeID = null";
                        //pUpdateClause += ",DiscountPercentage = null";
                        //pUpdateClause += ",TaxAmount = null";
                        //pUpdateClause += ",DiscountAmount = null";
                        //pUpdateClause += ",FixedDiscount = null ";
                        pUpdateClause += ",OperationContainersAndPackagesID = null";
                        pUpdateClause += ",ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                        pUpdateClause += ",ModificationDate = GETDATE() ";
                        pUpdateClause += "WHERE ID IN (0," + pInvoicesIDs + ")";
                        checkException = objCInvoices.UpdateList(pUpdateClause);

                        if (checkException != null) // an exception is caught in the model
                        {
                            returnedMessage = checkException.Message;
                        }
                        else //deleted successfully
                        {
                            CRoutings objCRoutings = new CRoutings();
                            if (objCVarInvoices_CreditMemo.IsFleet)
                            {
                                #region Create DummyTruckingOrders for print
                                checkException = objCRoutings.GetListPaging(999999, 1, "WHERE InvoiceID IN(0," + pInvoicesIDs + ")", "ID", out _RowCount);
                                CRoutings objCRoutings_Cancelled = new CRoutings();
                                objCRoutings_Cancelled.lstCVarRoutings.AddRange(objCRoutings.lstCVarRoutings);
                                Int64 _CancelledInvoiceID = Int64.Parse(pInvoicesIDs);
                                for (int i = 0; i < objCRoutings_Cancelled.lstCVarRoutings.Count; i++)
                                {
                                    objCRoutings_Cancelled.lstCVarRoutings[i].ID = 0;
                                    objCRoutings_Cancelled.lstCVarRoutings[i].RoutingTypeID = CancelledTransportOrderID;
                                    objCRoutings_Cancelled.lstCVarRoutings[i].TruckingOrderCode = "0";
                                    objCRoutings_Cancelled.lstCVarRoutings[i].InvoiceID = _CancelledInvoiceID;
                                    objCRoutings_Cancelled.lstCVarRoutings[i].CreatorUserID = objCRoutings_Cancelled.lstCVarRoutings[i].ModificatorUserID = WebSecurity.CurrentUserId;
                                    objCRoutings_Cancelled.lstCVarRoutings[i].CreationDate = objCRoutings_Cancelled.lstCVarRoutings[i].ModificationDate = DateTime.Now;
                                }
                                checkException = objCRoutings_Cancelled.SaveMethod(objCRoutings_Cancelled.lstCVarRoutings);
                                if (checkException == null)
                                    checkException = objCRoutings.UpdateList("InvoiceID=null, RoadNumber=N'InvoiceIDs " + pInvoicesIDs + " deleted by " + WebSecurity.CurrentUserName + " at ' + CONVERT(VARCHAR(20),GETDATE(), 103) WHERE TruckingOrderCode IS NOT NULL AND InvoiceID IN(0," + pInvoicesIDs + ")");
                                #endregion Create DummyTruckingOrders for print
                            }
                        }
                    } //if (objCInvoices.lstCVarInvoices.Count > 0) {
                } //if (objCInvoiceTypes.lstCVarInvoiceTypes.Count > 0)
            }
            #endregion Cancel Approved Invoices
            return new object[] {
                returnedMessage
            };
        }

        [HttpGet, HttpPost]
        public object[] SaveTransactionType(Int64 pInvoiceIDToApplyTransactionType, Int32 pTransactionTypeID)
        {
            string _ReturnedMessage = "";
            //int _RowCount = 0;
            Exception checkException = null;
            CInvoices objCInvoices = new CInvoices();
            checkException = objCInvoices.UpdateList("TransactionTypeID=" + pTransactionTypeID + " WHERE IsApproved=0 AND ID=" + pInvoiceIDToApplyTransactionType);
            if (checkException != null)
                _ReturnedMessage = checkException.Message;
            return new object[] {
                _ReturnedMessage
            };
        }

        [HttpGet, HttpPost]
        public object[] Restore(Int64 pInvoiceID)
        {
            string pUpdateClause = "";
            string strMessage = "";
            CInvoices objCInvoices = new CInvoices();
            pUpdateClause = " IsDeleted = 0 "
                          + " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString()
                          + " , ModificationDate = GETDATE() "
                          + " WHERE ID = " + pInvoiceID.ToString();
            objCInvoices.UpdateList(pUpdateClause);

            CReceivables objCReceivables = new CReceivables();
            pUpdateClause = " IsDeleted = 0 "
                          //+ " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString()
                          //+ " , ModificationDate = GETDATE() "
                          + " WHERE InvoiceID = " + pInvoiceID.ToString();
            objCReceivables.UpdateList(pUpdateClause);

            #region Update AccPartnerBalance
            //{
            //    CAccPartnerBalance objCAccPartnerBalance = new CAccPartnerBalance();
            //    pUpdateClause = "IsDeleted = 0";
            //    pUpdateClause += " , Notes=N'Invoice Restored.' ";
            //    pUpdateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
            //    pUpdateClause += " , ModificationDate = GETDATE() ";
            //    pUpdateClause = " WHERE InvoiceID = " + pInvoiceID.ToString();
            //    objCAccPartnerBalance.UpdateList(pUpdateClause);
            //}
            #endregion Update AccPartnerBalance

            strMessage = "Invoice Restored Successfully.";
            return new object[] { strMessage };
        }

        [HttpGet, HttpPost]
        public object[] Transfer(Int64 pInvoiceID, int pCurrencyID, Int64 pTransferredToOperationID)
        {
            string pUpdateClause = "";
            string strMessage = "";
            bool _result = false;
            CInvoices objCInvoices = new CInvoices();
            CReceivables objCReceivables = new CReceivables();

            CvwCurrencyDetails objCvwCurrencyDetails = new CvwCurrencyDetails();
            objCvwCurrencyDetails.GetList("WHERE ID=" + pCurrencyID + " AND GETDATE() BETWEEN FromDate AND ToDate");
            if (objCvwCurrencyDetails.lstCVarvwCurrencyDetails.Count == 0)
                strMessage = "Sorry, exchange rate is not entered for that currency.";
            else
            {
                pUpdateClause = " IsDeleted = 0 "
                              + " , OperationID = " + pTransferredToOperationID.ToString()
                              
                              + " , Amount = 0 "
                              + " , AmountWithoutVAT = 0 "
                              + " , TaxTypeID = null"
                              + " , TaxPercentage = null"
                              + " , DiscountTypeID = null"
                              + " , DiscountPercentage = null"
                              + " , TaxAmount = null"
                              + " , DiscountAmount = null"
                              + " , FixedDiscount = null "
                              
                              + " , CurrencyID = " + pCurrencyID.ToString()
                              + " , ExchangeRate = " + objCvwCurrencyDetails.lstCVarvwCurrencyDetails[0].ExchangeRate
                              + " , InvoiceDate = GETDATE() "
                              + " , DueDate = GETDATE() "
                              + " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString()
                              + " , ModificationDate = GETDATE() "
                              + " WHERE ID = " + pInvoiceID.ToString();
                objCInvoices.UpdateList(pUpdateClause);

                //pUpdateClause = " IsDeleted = 0 "
                //              + " , OperationID = " + pTransferredToOperationID.ToString()
                //              + " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString()
                //              + " , ModificationDate = GETDATE() "
                //              + " WHERE InvoiceID = " + pInvoiceID.ToString();
                //objCReceivables.UpdateList(pUpdateClause);
                objCReceivables.DeleteList(" WHERE InvoiceID = " + pInvoiceID.ToString());

                #region Update AccPartnerBalance
                {
                    //CAccPartnerBalance objCAccPartnerBalance = new CAccPartnerBalance();
                    //pUpdateClause = "IsDeleted = 0";
                    //pUpdateClause += " , Notes=N'Invoice Transfered.' ";
                    //pUpdateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                    //pUpdateClause += " , ModificationDate = GETDATE() ";
                    //pUpdateClause = " WHERE InvoiceID = " + pInvoiceID.ToString() + " AND InvoicePaymentDetailsID IS NULL ";
                    //objCAccPartnerBalance.UpdateList(pUpdateClause);
                    //////delete invoice payment details if exists
                    ////objCAccPartnerBalance.DeleteList(" WHERE InvoiceID = " + pInvoiceID.ToString() + " AND InvoicePaymentDetailsID IS NOT NULL ");
                }
                #endregion Update AccPartnerBalance

                strMessage = "Invoice transferred successfully.";
                _result = true;
            }
            return new object[] { _result, strMessage };
        }

        [HttpGet, HttpPost]
        public object[] OperationPartners_PrintAllPartnerInvoices(Int64 pOperationIDToPrintAllInvoices, Int32 pPartnerID, Int32 pPartnerTypeID)
        {
            string _ReturnedMessage = "";
            Exception checkException = null;
            int _tempRowCount = 0;
            var constCustomerPartnerTypeID = 1;
            var constAgentPartnerTypeID = 2;
            CvwOperations objCvwOperations = new CvwOperations();
            CvwInvoices objCvwInvoices = new CvwInvoices();
            CvwReceivables objCvwReceivables = new CvwReceivables();
            CCustomers objCCustomers = new CCustomers();
            CAgents objCAgents = new CAgents();
            checkException = objCvwOperations.GetListPaging(999999, 1, "WHERE ID=" + pOperationIDToPrintAllInvoices, "ID", out _tempRowCount);
            checkException = objCvwInvoices.GetListPaging(999999, 1, "WHERE InvoiceTypeCode<>'DRAFT' AND IsDeleted=0 AND PartnerID=" + pPartnerID + " AND PartnerTypeID=" + pPartnerTypeID + " AND (OperationID=" + pOperationIDToPrintAllInvoices + " OR MasterOperationID=" + pOperationIDToPrintAllInvoices + ")", "ID", out _tempRowCount);
            string _InvoiceIDs = "0";
            for (int i = 0; i < objCvwInvoices.lstCVarvwInvoices.Count; i++)
                _InvoiceIDs += "," + objCvwInvoices.lstCVarvwInvoices[i].ID;
            checkException = objCvwReceivables.GetListPaging(999999, 1, "WHERE IsDeleted=0 AND InvoiceID IN(" + _InvoiceIDs + ")", "ViewOrder, ChargeTypeName", out _tempRowCount);
            #region Get Client Data
            if (pPartnerTypeID == constCustomerPartnerTypeID) //CUSTOMERS
                objCCustomers.GetListPaging(999999, 1, "WHERE ID=" + pPartnerID, "ID", out _tempRowCount);
            else if (pPartnerTypeID == constAgentPartnerTypeID) //Agents
                objCAgents.GetListPaging(999999, 1, "WHERE ID=" + pPartnerID, "ID", out _tempRowCount);
            #endregion Get Client Data

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                _ReturnedMessage //pData[0]
                , serializer.Serialize(objCvwOperations.lstCVarvwOperations[0]) //pData[1]
                , serializer.Serialize(objCvwInvoices.lstCVarvwInvoices) //pData[2]
                , serializer.Serialize(objCvwReceivables.lstCVarvwReceivables) //pData[3]
                , pPartnerTypeID == constCustomerPartnerTypeID ? new JavaScriptSerializer().Serialize(objCCustomers.lstCVarCustomers[0])
                    : (pPartnerTypeID == constAgentPartnerTypeID ? new JavaScriptSerializer().Serialize(objCAgents.lstCVarAgents[0])
                    : null) //pData[4]
            };
        }

        [HttpGet, HttpPost]
        public object[] getInvoiceDetailsForVoucher(Int64 pInvoiceID)
        {
            string _ReturnedMessage = "";
            Exception checkException = null;
            int _RowCount = 0;
            CvwA_GetToLinkCustomerAccounting objcGetToLinkCustomerAccounting = new CvwA_GetToLinkCustomerAccounting();
            checkException = objcGetToLinkCustomerAccounting.GetListPaging(1, 1, "WHERE ID=" + pInvoiceID, "ID", out _RowCount);
            if (objcGetToLinkCustomerAccounting.lstCVarvwA_GetToLinkCustomerAccounting.Count >0)
            {
                if (objcGetToLinkCustomerAccounting.lstCVarvwA_GetToLinkCustomerAccounting[0].AccountID == 0)
                {
                    _ReturnedMessage = "Link Account";
                }
                else if (objcGetToLinkCustomerAccounting.lstCVarvwA_GetToLinkCustomerAccounting[0].subAccountID == 0)
                {
                    _ReturnedMessage = "Link SupAccount";

                }
            }
           
          
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                _ReturnedMessage //pData[0]
                , serializer.Serialize(objcGetToLinkCustomerAccounting.lstCVarvwA_GetToLinkCustomerAccounting) //pData[1]
               
            };
        }

        [HttpGet, HttpPost]
        public bool InsertA_VoucherInvoicesPayment(Int32 pInvoiceID, Int32 pVoucherID,decimal pDueAmount,Int32 pCurrencyID)
        {
            bool _result = false;

            CVarA_VoucherInvoicesPayment objVarCA_VoucherInvoicesPayment = new CVarA_VoucherInvoicesPayment();

            objVarCA_VoucherInvoicesPayment.ID = 0;
            objVarCA_VoucherInvoicesPayment.InvoiceID = pInvoiceID;
            objVarCA_VoucherInvoicesPayment.VoucherID = pVoucherID;
            objVarCA_VoucherInvoicesPayment.DueAmount = pDueAmount;
            objVarCA_VoucherInvoicesPayment.CurrencyID = pCurrencyID;
            objVarCA_VoucherInvoicesPayment.AccPartnerBalanceID = 0;

            CA_VoucherInvoicesPayment cA_VoucherInvoicesPayment = new CA_VoucherInvoicesPayment();

            cA_VoucherInvoicesPayment.lstCVarA_VoucherInvoicesPayment.Add(objVarCA_VoucherInvoicesPayment);
            Exception checkException = cA_VoucherInvoicesPayment.SaveMethod(cA_VoucherInvoicesPayment.lstCVarA_VoucherInvoicesPayment);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else
            { //not unique
                _result = true;
                //CallCustomizedSP
                CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
                objCCustomizedDBCall.SP_Trans_Log("SP_Trans_Log", WebSecurity.CurrentUserId, "A_VoucherInvoicesPayment", objVarCA_VoucherInvoicesPayment.ID, "I");
            }
            return _result;
        }

        [HttpPost, HttpGet]
        public object ApproveOrUnApprove(string pIDsToSetApproval, bool pIsApprove, Int32 pCostCenterID
        , Int32 pNewInvoiceType, string pGlbCallingControl
        , string pWhereClause, Int32 pPageSize, Int32 pPageNumber, string pOrderBy)
        {
            bool _result = false;
            Exception checkException = null;
            string pUpdateClause = "";
            string _ReturnedMessage = ""; //used to handle deactivated customers for GBL
            var ArrInvoiceIDs = pIDsToSetApproval.Split(',');
            int NumberOfInvoices = ArrInvoiceIDs.Count();
            CInvoices objCInvoices = new CInvoices();
            CvwInvoices objCvwInvoices = new CvwInvoices();
            CAccPartnerBalance objCAccPartnerBalance = new CAccPartnerBalance();
            CDefaults objCDefaults = new CDefaults();
            CvwA_GetToLinkCustomerAccounting objcGetToLinkCustomerAccounting = new CvwA_GetToLinkCustomerAccounting();
            int _RowCount = 0;
            string _auxIDsToSetApproval = pIDsToSetApproval;//if _auxIDsToSetApproval not changed then pIDsToSetApproval remains the same

            checkException = objCvwInvoices.GetListPaging(1, 1, "WHERE IsApproved=0 AND IsInactive=1 AND ID IN (" + pIDsToSetApproval + ")", "ID", out _RowCount);
            checkException = objCDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount);
            #region Don't Approve Inactive Customers Invoices for GBL
            {
                if (objCvwInvoices.lstCVarvwInvoices.Count > 0)
                    _ReturnedMessage = objCvwInvoices.lstCVarvwInvoices[0].PartnerName + " in " + objCvwInvoices.lstCVarvwInvoices[0].ConcatenatedInvoiceNumber + " is inactive.";
            }
            #endregion Don't Approve Inactive Customers Invoices for GBL

            #region Check GBL Active Criteria
            if (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "GBL")
            {
                #region Check GBL TransactionType
                CInvoices objCInvoices_Temp = new CInvoices();
                checkException = objCInvoices_Temp.GetListPaging(1, 1, "WHERE IsApproved=0 AND ID IN (" + pIDsToSetApproval + ") AND TransactionTypeID NOT IN (SELECT CUST_TRX_TYPE_ID from GBL_vw_TransactionTypes)", "ID", out _RowCount);
                //CGBL_vw_TransactionTypes objCGBL_vw_TransactionTypes = new CGBL_vw_TransactionTypes();
                if (checkException != null)
                    _ReturnedMessage = checkException.Message;
                else if (objCInvoices_Temp.lstCVarInvoices.Count > 0)
                    _ReturnedMessage = objCInvoices_Temp.lstCVarInvoices[0].InvoiceNumber + " has an invalid transaction type.";
                #endregion Check GBL TransactionType

                #region Check GBL Charges Accounts
                else
                {
                    CChargeTypes objCChargeTypes_temp_Revenue = new CChargeTypes();
                    checkException = objCChargeTypes_temp_Revenue.GetList("WHERE ID IN (SELECT ChargeTypeID FROM vwReceivables WHERE InvoiceID IN (" + pIDsToSetApproval + ") AND InvoiceTypeName<>N'CREDIT MEMO' AND AccountID_Revenue IN (SELECT AccountID from GBL_vw_Accounts WHERE End_Date_Active < GETDATE()))");
                    if (checkException != null)
                        _ReturnedMessage = checkException.Message;
                    CChargeTypes objCChargeTypes_temp_Return = new CChargeTypes();
                    checkException = objCChargeTypes_temp_Return.GetList("WHERE ID IN (SELECT ChargeTypeID FROM vwReceivables WHERE InvoiceID IN (" + pIDsToSetApproval + ") AND InvoiceTypeName=N'CREDIT MEMO' AND AccountID_Return IN (SELECT AccountID from GBL_vw_Accounts WHERE End_Date_Active < GETDATE()))");
                    if (checkException != null)
                        _ReturnedMessage = checkException.Message;

                    if (objCChargeTypes_temp_Revenue.lstCVarChargeTypes.Count > 0)
                        _ReturnedMessage = objCChargeTypes_temp_Revenue.lstCVarChargeTypes[0].Name + "(" + objCChargeTypes_temp_Revenue.lstCVarChargeTypes[0].Code + ")" + " is invalid.";
                    else if (objCChargeTypes_temp_Return.lstCVarChargeTypes.Count > 0)
                        _ReturnedMessage = objCChargeTypes_temp_Return.lstCVarChargeTypes[0].Name + "(" + objCChargeTypes_temp_Return.lstCVarChargeTypes[0].Code + ")" + " is invalid.";
                }
                #endregion Check GBL Charges Accounts
            } //if (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "GBL") {

            #endregion Check GBL Active Criteria

            #region Approving/Unapproving process
            if (_ReturnedMessage == "")
            {
                #region FirstStepDraftInvoiceApproval
                if (pGlbCallingControl == "FirstStepDraftInvoiceApproval")
                {
                    for (int i = 0; i < NumberOfInvoices; i++)
                    {
                        CInvoices objCInvoices_FirstStep = new CInvoices();
                        checkException = objCInvoices.UpdateList(
                            "IsDraftApproved=" + (pIsApprove ? "1" : "0") + "\n"
                            + (!pIsApprove ? ",IsApproved=0" : "") + "\n"
                            + ",DraftApprovingUserID = " + WebSecurity.CurrentUserId + "\n"
                            + "WHERE ID=" + ArrInvoiceIDs[i] + "\n"
                            //+ " AND ChildInvoiceID IS NULL" //This condition is to prevent unapproving drafts transferred to invoices
                            );
                    
                    }
                }
                #endregion FirstStepDraftInvoiceApproval
                #region Create new invoices from draft and set items
                else
                {
                    if (pNewInvoiceType > 0 && pIsApprove)
                    {

                        _auxIDsToSetApproval = "";
                        for (int i = 0; i < NumberOfInvoices; i++)
                        {
                            #region Link Customers isExternal With Accounting
                            //added by ahmed maher
                            if (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "COS")
                            {
                                checkException = objcGetToLinkCustomerAccounting.GetListPaging(1, 1, "WHERE ID=" + ArrInvoiceIDs[i], "ID", out _RowCount);
                                if (objcGetToLinkCustomerAccounting.lstCVarvwA_GetToLinkCustomerAccounting[0].IsExternal == true
                                    && objcGetToLinkCustomerAccounting.lstCVarvwA_GetToLinkCustomerAccounting[0].AccountID != 0
                                    && objcGetToLinkCustomerAccounting.lstCVarvwA_GetToLinkCustomerAccounting[0].subAccountID != 0
                                    && objcGetToLinkCustomerAccounting.lstCVarvwA_GetToLinkCustomerAccounting[0].subAccountIDGroupID != 0
                                    && objcGetToLinkCustomerAccounting.lstCVarvwA_GetToLinkCustomerAccounting[0].CustomersubAccountID == 0)
                                {
                                    CCustomers objCCustomers = new CCustomers();
                                    checkException = objCCustomers.UpdateList(
                                                    "SubAccountGroupID=" + (objcGetToLinkCustomerAccounting.lstCVarvwA_GetToLinkCustomerAccounting[0].subAccountIDGroupID) + "\n"
                                                    + ",SubAccountID=" + (objcGetToLinkCustomerAccounting.lstCVarvwA_GetToLinkCustomerAccounting[0].subAccountID) + "\n"
                                                    + ",AccountID=" + (objcGetToLinkCustomerAccounting.lstCVarvwA_GetToLinkCustomerAccounting[0].AccountID) + "\n"
                                                    + "WHERE ID=" + objcGetToLinkCustomerAccounting.lstCVarvwA_GetToLinkCustomerAccounting[0].CustomerID + "\n"
                                                    //+ " AND ChildInvoiceID IS NULL" //This condition is to prevent unapproving drafts transferred to invoices
                                                    );
                                }
                            }

                            #endregion

                            CInvoices objCInvoices_GetUnApproved = new CInvoices();
                            objCInvoices_GetUnApproved.GetListPaging(999999, 1, "WHERE IsApproved=0 AND ID=" + ArrInvoiceIDs[i], "ID", out _RowCount);
                            CVarInvoices objCVarInvoices = new CVarInvoices();
                            #region Save InvoiceHeader
                            if (objCInvoices_GetUnApproved.lstCVarInvoices.Count > 0)
                            {
                                int paymentTermDays = 0;
                                CPaymentTerms objCPaymentTerms_Temp = new CPaymentTerms();
                                objCPaymentTerms_Temp.GetList("WHERE ID=" + objCInvoices_GetUnApproved.lstCVarInvoices[0].PaymentTermID);
                                if (objCPaymentTerms_Temp.lstCVarPaymentTerms.Count > 0)
                                    paymentTermDays = objCPaymentTerms_Temp.lstCVarPaymentTerms[0].Days;
                                objCVarInvoices.InvoiceNumber = 0;
                                objCVarInvoices.OperationID = objCInvoices_GetUnApproved.lstCVarInvoices[0].OperationID;
                                objCVarInvoices.OperationPartnerID = objCInvoices_GetUnApproved.lstCVarInvoices[0].OperationPartnerID;
                                objCVarInvoices.AddressID = objCInvoices_GetUnApproved.lstCVarInvoices[0].AddressID;
                                objCVarInvoices.InvoiceTypeID = pNewInvoiceType;
                                objCVarInvoices.PrintedAddress = objCInvoices_GetUnApproved.lstCVarInvoices[0].PrintedAddress;
                                objCVarInvoices.CustomerReference = objCInvoices_GetUnApproved.lstCVarInvoices[0].CustomerReference;
                                objCVarInvoices.PaymentTermID = objCInvoices_GetUnApproved.lstCVarInvoices[0].PaymentTermID;
                                objCVarInvoices.CurrencyID = objCInvoices_GetUnApproved.lstCVarInvoices[0].CurrencyID;
                                objCVarInvoices.ExchangeRate = objCInvoices_GetUnApproved.lstCVarInvoices[0].ExchangeRate;
                                objCVarInvoices.InvoiceDate = DateTime.Now; //objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "SAF" ? objCInvoices_GetUnApproved.lstCVarInvoices[0].InvoiceDate : DateTime.Now;

                                if (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "SWI")
                                    objCVarInvoices.DueDate = objCVarInvoices.InvoiceDate.AddDays(paymentTermDays); 
                                else
                                    objCVarInvoices.DueDate = objCInvoices_GetUnApproved.lstCVarInvoices[0].DueDate;

                                objCVarInvoices.AmountWithoutVAT = objCInvoices_GetUnApproved.lstCVarInvoices[0].AmountWithoutVAT;
                                objCVarInvoices.TaxTypeID = objCInvoices_GetUnApproved.lstCVarInvoices[0].TaxTypeID;
                                objCVarInvoices.TaxPercentage = objCInvoices_GetUnApproved.lstCVarInvoices[0].TaxPercentage;
                                objCVarInvoices.TaxAmount = objCInvoices_GetUnApproved.lstCVarInvoices[0].TaxAmount;
                                objCVarInvoices.DiscountTypeID = objCInvoices_GetUnApproved.lstCVarInvoices[0].DiscountTypeID;
                                objCVarInvoices.DiscountPercentage = objCInvoices_GetUnApproved.lstCVarInvoices[0].DiscountPercentage;
                                objCVarInvoices.DiscountAmount = objCInvoices_GetUnApproved.lstCVarInvoices[0].DiscountAmount;
                                objCVarInvoices.FixedDiscount = objCInvoices_GetUnApproved.lstCVarInvoices[0].FixedDiscount;
                                objCVarInvoices.Amount = objCInvoices_GetUnApproved.lstCVarInvoices[0].Amount;
                                //objCVarInvoices.PaidAmount = pPaidAmount;
                                //objCVarInvoices.RemainingAmount = pRemainingAmount;
                                objCVarInvoices.InvoiceStatusID = objCInvoices_GetUnApproved.lstCVarInvoices[0].InvoiceStatusID;
                                objCVarInvoices.IsApproved = false; //ChildInvoiceID means approved
                                objCVarInvoices.LeftSignature = "0";
                                objCVarInvoices.MiddleSignature = "0";
                                objCVarInvoices.RightSignature = "0";
                                objCVarInvoices.GRT = "0";
                                objCVarInvoices.DWT = "0";
                                objCVarInvoices.NRT = "0";
                                objCVarInvoices.LOA = "0";
                                objCVarInvoices.EditableNotes = objCInvoices_GetUnApproved.lstCVarInvoices[0].EditableNotes;
                                objCVarInvoices.OperationContainersAndPackagesID = objCInvoices_GetUnApproved.lstCVarInvoices[0].OperationContainersAndPackagesID;
                                objCVarInvoices.RelatedToInvoiceID = objCInvoices_GetUnApproved.lstCVarInvoices[0].RelatedToInvoiceID;

                                objCVarInvoices.Notes = "0";
                                objCVarInvoices.CutOffDate = DateTime.Parse("01/01/1900");

                                objCVarInvoices.CreatorUserID = objCVarInvoices.ModificatorUserID = WebSecurity.CurrentUserId;
                                objCVarInvoices.CreationDate = objCVarInvoices.ModificationDate = DateTime.Now;

                                CInvoices objCInvoices_temp = new CInvoices();
                                objCInvoices_temp.lstCVarInvoices.Add(objCVarInvoices);
                                checkException = objCInvoices_temp.SaveMethod(objCInvoices_temp.lstCVarInvoices);
                                _auxIDsToSetApproval += _auxIDsToSetApproval == "" ? objCVarInvoices.ID.ToString() : ("," + objCVarInvoices.ID);
                                CReceivables objCReceivables = new CReceivables();
                                checkException = objCReceivables.UpdateList("InvoiceID=" + objCVarInvoices.ID + " WHERE DraftInvoiceID=" + ArrInvoiceIDs[i]);
                                //to disable Draft
                                checkException = objCInvoices_temp.UpdateList("ChildInvoiceID=" + objCVarInvoices.ID + " WHERE ID=" + ArrInvoiceIDs[i]);
                            }
                            #endregion Save InvoiceHeader and set items
                        }
                    }
                    pIDsToSetApproval = _auxIDsToSetApproval; //if _auxIDsToSetApproval not changed then pIDsToSetApproval remains the same
                    ArrInvoiceIDs = pIDsToSetApproval.Split(',');
                    NumberOfInvoices = ArrInvoiceIDs.Count();
                    #endregion Create new invoices from draft
                    #region Call ERP JV Entry (They approve just one at a time)
                    CGroups objCGroups = new CGroups();
                    objCGroups.GetList("WHERE GroupImageURL=N'Accounting'");
                    if (!objCGroups.lstCVarGroups[0].IsInactive)
                    {
                        CCallCustomizedSP objCCallCustomizedSP = new CCallCustomizedSP();
                        checkException = objCCallCustomizedSP.CallCustomizedSP_MultiIDs("ERP_ForwWeb_PostingInvoice", "," + pIDsToSetApproval + ",", WebSecurity.CurrentUserId, pIsApprove, pCostCenterID);
                    }
                    #endregion Call ERP JV Entry
                    if (checkException == null
                        /*sherif: disable approve from Transfer Draft Invoices from COSCO*/&& !(pGlbCallingControl == "DraftInvoicesApprovals" && objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "COS")
                        )
                    {
                        for (int i = 0; i < NumberOfInvoices; i++)
                        {
                            //update invoices to requested Approval/UnApproval
                            pUpdateClause = " IsApproved = " + (pIsApprove ? "1" : "0");
                            pUpdateClause += " ,IsPrintOriginal = " + (pIsApprove ? "1" : "0");
                            pUpdateClause += " ,ApprovingUserID = " + WebSecurity.CurrentUserId;
                            //pUpdateClause += " ,ModificatorUserID = " + WebSecurity.CurrentUserId;
                            pUpdateClause += " ,ModificationDate = GETDATE() ";
                            pUpdateClause += " WHERE ID=" + ArrInvoiceIDs[i] + " OR ChildInvoiceID=" + ArrInvoiceIDs[i];
                            checkException = objCInvoices.UpdateList(pUpdateClause);
                            if (checkException == null) //add to or remove from AccPartnerBalance according to pIsApprove
                            {
                                checkException = objCAccPartnerBalance.DeleteList("WHERE InvoiceID=" + ArrInvoiceIDs[i]);
                                if (!pIsApprove) //delete from AccPartnerBalance //i dont allow that unless there is no InvPaymentDetails
                                                 //if its requested to unapprove even paid invoices then delete from AccInvoicePaymentDetails and Reset PaidAmount & RemaingAmount in Invoices
                                {
                                    //checkException = objCAccPartnerBalance.DeleteList("WHERE InvoiceID=" + ArrInvoiceIDs[i]);
                                }
                                else //pIsApprove = true so insert to AccPartnerBalance
                                {
                                    #region Add to PartnerBalance table
                                    {
                                        int constTransactionInvoice = 30;
                                        objCvwInvoices.GetListPaging(1, 1, ("WHERE ID=" + ArrInvoiceIDs[i]), "ID", out _RowCount);
                                        CVarAccPartnerBalance objCVarAccPartnerBalance = new CVarAccPartnerBalance();
                                        objCVarAccPartnerBalance.InvoiceID = Int64.Parse(ArrInvoiceIDs[i]);
                                        objCVarAccPartnerBalance.PartnerTypeID = objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID;
                                        objCVarAccPartnerBalance.CustomerID = GetPartnerIDForInsert(1, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                                        objCVarAccPartnerBalance.AgentID = GetPartnerIDForInsert(2, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                                        objCVarAccPartnerBalance.ShippingAgentID = GetPartnerIDForInsert(3, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                                        objCVarAccPartnerBalance.CustomsClearanceAgentID = GetPartnerIDForInsert(4, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                                        objCVarAccPartnerBalance.ShippingLineID = GetPartnerIDForInsert(5, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                                        objCVarAccPartnerBalance.AirlineID = GetPartnerIDForInsert(6, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                                        objCVarAccPartnerBalance.TruckerID = GetPartnerIDForInsert(7, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                                        objCVarAccPartnerBalance.SupplierID = GetPartnerIDForInsert(8, objCvwInvoices.lstCVarvwInvoices[0].PartnerTypeID, objCvwInvoices.lstCVarvwInvoices[0].PartnerID);
                                        objCVarAccPartnerBalance.DebitAmount = objCvwInvoices.lstCVarvwInvoices[0].Amount;
                                        objCVarAccPartnerBalance.CurrencyID = objCvwInvoices.lstCVarvwInvoices[0].CurrencyID;
                                        objCVarAccPartnerBalance.ExchangeRate = objCvwInvoices.lstCVarvwInvoices[0].ExchangeRate;// decimal.Parse(ArrExchangeRates[i]); ///////////////////////////////////
                                        objCVarAccPartnerBalance.BalCurLocalExRate = objCvwInvoices.lstCVarvwInvoices[0].ExchangeRate; ///////////////////////////////////
                                        objCVarAccPartnerBalance.InvCurLocalExRate = objCvwInvoices.lstCVarvwInvoices[0].ExchangeRate; ///////////////////////////////////
                                        objCVarAccPartnerBalance.TransactionType = constTransactionInvoice;
                                        objCVarAccPartnerBalance.Notes = "Invoice Approval.";
                                        objCVarAccPartnerBalance.CreatorUserID = objCVarAccPartnerBalance.mModificatorUserID = WebSecurity.CurrentUserId;
                                        objCVarAccPartnerBalance.CreationDate = objCVarAccPartnerBalance.ModificationDate = DateTime.Now;

                                        objCAccPartnerBalance.lstCVarAccPartnerBalance.Add(objCVarAccPartnerBalance);
                                        checkException = objCAccPartnerBalance.SaveMethod(objCAccPartnerBalance.lstCVarAccPartnerBalance);
                                    }
                                    #endregion Add to PartnerBalance table
                                } //of else i.e. pIsApprove = true
                            }
                        } //of the for loop
                    } //JV entry successful
                }//EOF else if (pGlbCallingControl == "FirstStepDraftInvoiceApproval")
            } //EOF if (_ReturnedMessage == "") //GBL Customer is Active
            #endregion region Approving/Unapproving process

            if (checkException == null)
            {
                _result = true;
            }


            objCvwInvoices.GetListPaging(pPageSize, pPageNumber, pWhereClause, pOrderBy, out _RowCount);


            return new object[] {
                _result
                , new JavaScriptSerializer().Serialize(objCvwInvoices.lstCVarvwInvoices)
                , _result ? "" : checkException.Message
                , _ReturnedMessage //pData[3]
            };
        } //of fn

        [HttpPost, HttpGet]
        public object ApproveOrUnApproveTax(string pIDsToSetApproval, bool pIsApprove, Int32 pCostCenterID
        , Int32 pNewInvoiceType, string pGlbCallingControl
        , string pWhereClause, Int32 pAccountIDCharge, Int32 pSubAccountCharge, Int32 pPageSize, Int32 pPageNumber, string pOrderBy)
        {
            bool _result = false;
            Exception checkException = null;
            string pUpdateClause = "";
            string _ReturnedMessage = ""; //used to handle deactivated customers for GBL
            var ArrInvoiceIDs = pIDsToSetApproval.Split(',');
            int NumberOfInvoices = ArrInvoiceIDs.Count();
            CInvoices objCInvoices = new CInvoices();
            CvwInvoices objCvwInvoices = new CvwInvoices();
            CAccPartnerBalance objCAccPartnerBalance = new CAccPartnerBalance();
            CDefaults objCDefaults = new CDefaults();
            CvwA_GetToLinkCustomerAccounting objcGetToLinkCustomerAccounting = new CvwA_GetToLinkCustomerAccounting();
            int _RowCount = 0;
            string _auxIDsToSetApproval = pIDsToSetApproval;//if _auxIDsToSetApproval not changed then pIDsToSetApproval remains the same

            #region Don't Approve Inactive Customers Invoices for GBL
            checkException = objCDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount);

            //{
            //    checkException = objCvwInvoices.GetListPaging(1, 1, "WHERE IsApproved=0 AND IsInactive=1 AND ID IN (" + pIDsToSetApproval + ")", "ID", out _RowCount);
            //    if (objCvwInvoices.lstCVarvwInvoices.Count > 0)
            //        _ReturnedMessage = objCvwInvoices.lstCVarvwInvoices[0].PartnerName + " in " + objCvwInvoices.lstCVarvwInvoices[0].ConcatenatedInvoiceNumber + " is inactive.";
            //}
            #endregion Don't Approve Inactive Customers Invoices for GBL

            if (checkException == null)
            {
                _result = true;
            }

            

            #region Tax
            int _RowCount2 = 0;

            CvwDefaults objCvwDefaults = new CvwDefaults();
            objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount2); //i am sure i ve just one row isa
            string CompanyName = objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName;
            if ((CompanyName == "CHM" || CompanyName == "OCE") && checkException == null)
            {
                int NumberOfInvoicesTax = ArrInvoiceIDs.Count();
                CInvoicesTax objCInvoicesTaxGet = new CInvoicesTax();
                CTaxLink objCTaxLink = new CTaxLink();
                CTaxLink objCTaxLinkCharge = new CTaxLink();
                CTaxLink objCTaxLinOperationPartners = new CTaxLink();
                CTaxLink objCTaxLinkFOUND = new CTaxLink();



                for (int i = 0; i < NumberOfInvoicesTax; i++)
                {
                    objCInvoices.GetList("WHERE ID=" + ArrInvoiceIDs[i]);
                    objCTaxLink.GetList("WHERE Notes='Operations' and OriginID=" + objCInvoices.lstCVarInvoices[0].OperationID);
                    objCTaxLinOperationPartners.GetList("WHERE Notes='OperationPartners' and OriginID=" + objCInvoices.lstCVarInvoices[0].OperationPartnerID);
                    objCTaxLinkCharge.GetList("WHERE Notes='Invoices' and jvid is null and OriginID=" + ArrInvoiceIDs[i]);
                    objCTaxLinkFOUND.GetList("WHERE Notes='Invoices' and jvid is NOT null and OriginID=" + ArrInvoiceIDs[i]);




                    //if (objCInvoices.lstCVarInvoices.Count > 0 && pIsApprove && objCTaxLink.lstCVarTaxLink.Count > 0 && objCTaxLinkCharge.lstCVarTaxLink.Count ==0)
                if (objCInvoices.lstCVarInvoices.Count > 0 && pIsApprove && objCTaxLinkCharge.lstCVarTaxLink.Count == 0)


                    {
                            //link
                        CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
                        objCCustomizedDBCall.ExecuteQuery_DataTable("insertTaxLink " + ArrInvoiceIDs[i] + "," + 0 + "," + "Invoices");
                        
                        #region Call ERP JV Entry (They approve just one at a time)
                  
                            CCallCustomizedSP objCCallCustomizedSP = new CCallCustomizedSP();
                            checkException = objCCallCustomizedSP.CallCustomizedSP_MultiIDsTax("ERP_ForwWeb_PostingInvoiceTax", "," + ArrInvoiceIDs[i] + ",", WebSecurity.CurrentUserId, pIsApprove, pCostCenterID, pAccountIDCharge, pSubAccountCharge);
                        
                        #endregion Call ERP JV Entry

                        #region OLD
                        //#region Save InvoiceHeader
                        //CVarInvoicesTAX objCVarInvoicesTax = new CVarInvoicesTAX();
                        //objCVarInvoicesTax.InvoiceNumber = objCInvoices.lstCVarInvoices[0].InvoiceNumber;
                        //objCVarInvoicesTax.OperationID = objCTaxLink.lstCVarTaxLink.Count > 0 ? objCTaxLink.lstCVarTaxLink[0].TaxID : 0;
                        //objCVarInvoicesTax.OperationPartnerID = objCTaxLinOperationPartners.lstCVarTaxLink[0].TaxID;
                        //objCVarInvoicesTax.AddressID = 0;
                        //objCVarInvoicesTax.InvoiceTypeID = objCInvoices.lstCVarInvoices[0].InvoiceTypeID;
                        //objCVarInvoicesTax.PrintedAddress = objCInvoices.lstCVarInvoices[0].PrintedAddress;
                        //objCVarInvoicesTax.CustomerReference = objCInvoices.lstCVarInvoices[0].CustomerReference;
                        //objCVarInvoicesTax.PaymentTermID = objCInvoices.lstCVarInvoices[0].PaymentTermID;
                        //objCVarInvoicesTax.CurrencyID = objCInvoices.lstCVarInvoices[0].CurrencyID;
                        //objCVarInvoicesTax.ExchangeRate = objCInvoices.lstCVarInvoices[0].ExchangeRate;
                        //objCVarInvoicesTax.InvoiceDate = objCInvoices.lstCVarInvoices[0].InvoiceDate; //pInvoiceIssueDate;
                        //objCVarInvoicesTax.DueDate = objCInvoices.lstCVarInvoices[0].DueDate; //pInvoiceDueDate;
                        //objCVarInvoicesTax.AmountWithoutVAT = objCInvoices.lstCVarInvoices[0].AmountWithoutVAT;
                        //objCVarInvoicesTax.TaxTypeID = objCInvoices.lstCVarInvoices[0].TaxTypeID;
                        //objCVarInvoicesTax.TaxPercentage = objCInvoices.lstCVarInvoices[0].TaxPercentage;
                        //objCVarInvoicesTax.TaxAmount = objCInvoices.lstCVarInvoices[0].TaxAmount;
                        //objCVarInvoicesTax.DiscountTypeID = objCInvoices.lstCVarInvoices[0].DiscountTypeID;
                        //objCVarInvoicesTax.DiscountPercentage = objCInvoices.lstCVarInvoices[0].DiscountPercentage;
                        //objCVarInvoicesTax.DiscountAmount = objCInvoices.lstCVarInvoices[0].DiscountAmount;
                        //objCVarInvoicesTax.FixedDiscount = objCInvoices.lstCVarInvoices[0].FixedDiscount;
                        //objCVarInvoicesTax.Amount = objCInvoices.lstCVarInvoices[0].Amount;
                        ////objCVarInvoices.PaidAmount = pPaidAmount;
                        ////objCVarInvoices.RemainingAmount = pRemainingAmount;
                        //objCVarInvoicesTax.InvoiceStatusID = objCInvoices.lstCVarInvoices[0].InvoiceStatusID;
                        //objCVarInvoicesTax.IsApproved = false;
                        //objCVarInvoicesTax.LeftSignature = "0";
                        //objCVarInvoicesTax.MiddleSignature = "0";
                        //objCVarInvoicesTax.RightSignature = "0";
                        //objCVarInvoicesTax.GRT = "0";
                        //objCVarInvoicesTax.DWT = "0";
                        //objCVarInvoicesTax.NRT = "0";
                        //objCVarInvoicesTax.LOA = "0";
                        //objCVarInvoicesTax.EditableNotes = "0";
                        //objCVarInvoicesTax.OperationContainersAndPackagesID = objCInvoices.lstCVarInvoices[0].OperationContainersAndPackagesID;
                        //objCVarInvoicesTax.TransactionTypeID = objCInvoices.lstCVarInvoices[0].TransactionTypeID;
                        //objCVarInvoicesTax.Notes = "0";
                        //objCVarInvoicesTax.CutOffDate = DateTime.Parse("01/01/1900");
                        //objCVarInvoicesTax.CreatorUserID = objCVarInvoicesTax.ModificatorUserID = WebSecurity.CurrentUserId;
                        //objCVarInvoicesTax.CreationDate = objCVarInvoicesTax.ModificationDate = DateTime.Now;

                        //CInvoicesTax objCInvoicesTax = new CInvoicesTax();
                        //objCInvoicesTax.lstCVarInvoices.Add(objCVarInvoicesTax);
                        //checkException = objCInvoicesTax.SaveMethod(objCInvoicesTax.lstCVarInvoices);
                        //#endregion Save InvoiceHeader
                        //if (checkException != null) // an exception is caught in the model
                        //{
                        //    if (checkException.Message.Contains("UNIQUE"))
                        //        _result = false;
                        //}
                        //#region Details
                        //CReceivables objCReceivables = new CReceivables();
                        //objCReceivables.GetList("WHERE InvoiceID=" + ArrInvoiceIDs[i]);
                        //if (objCReceivables.lstCVarReceivables.Count > 0)
                        //{
                        //    for (int k = 0; k < objCReceivables.lstCVarReceivables.Count; k++)
                        //    {
                        //        objCTaxLink.GetList("where OriginID =" + objCReceivables.lstCVarReceivables[k].ID);
                        //        if (objCTaxLink.lstCVarTaxLink.Count > 0)
                        //        {
                        //            pWhereClause = "";

                        //            pWhereClause = " WHERE ID = " + objCTaxLink.lstCVarTaxLink[0].TaxID; //i am sure i have at least 1 row(building the first part of the where clause, then use OR incase of more than 1 house)

                        //            pUpdateClause = (objCInvoices.lstCVarInvoices[0].InvoiceTypeID == 20 ? "DraftInvoiceID=" : "InvoiceID=") + objCInvoicesTax.lstCVarInvoices[0].ID + "";
                        //            #region ensure receivables are correct
                        //            pUpdateClause += " , AmountWithoutVAT = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2)" + " \n";
                        //            pUpdateClause += " , TaxPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID)" + " \n";
                        //            pUpdateClause += " , TaxAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID), 2)" + " \n";
                        //            pUpdateClause += " , DiscountPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID)" + " \n";
                        //            pUpdateClause += " , DiscountAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),2)" + " \n";
                        //            pUpdateClause += " , SaleAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2)" + " \n"
                        //                          + " + (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID),0) , 2))" + " \n"
                        //                          + " - (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),0), 2))" + " \n";
                        //            #endregion ensure receivables are correct
                        //            //pUpdateClause += " , OperationID = " + pOperationID;
                        //            //pUpdateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                        //            //pUpdateClause += " , ModificationDate = GETDATE() ";
                        //            pUpdateClause += pWhereClause;
                        //            CReceivablesTax objCReceivablesTax = new CReceivablesTax();
                        //            checkException = objCReceivablesTax.UpdateList(pUpdateClause);


                        //            #region Update Invoice totals at server side to fix any connection problem
                        //            //SET AmountWithoutVAT
                        //            pUpdateClause = " AmountWithoutVAT = ROUND((SELECT SUM(ISNULL(SalePrice,0) * ISNULL(Quantity,1))-" + objCInvoices.lstCVarInvoices[0].FixedDiscount + " FROM Receivables WHERE " + (objCInvoices.lstCVarInvoices[0].InvoiceTypeID == 20 ? "DraftInvoiceID" : "InvoiceID") + " = " + objCVarInvoicesTax.ID.ToString() + " AND IsDeleted=0),2)";
                        //            pUpdateClause += " WHERE ID = " + objCVarInvoicesTax.ID.ToString();
                        //            checkException = objCInvoices.UpdateList(pUpdateClause);
                        //            //SET Tax, Discount & Total Amount after setting the AmountWithVAT
                        //            pUpdateClause = " TaxAmount = ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(TaxPercentage, 0) / 100),2)";
                        //            pUpdateClause += " , DiscountAmount = ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2)";
                        //            if (objCDefaults.lstCVarDefaults[0].IsTaxOnItems)
                        //                pUpdateClause += " , Amount = - ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2) + ROUND((SELECT SUM(ISNULL(SaleAmount,0))-" + objCInvoices.lstCVarInvoices[0].FixedDiscount + " FROM Receivables WHERE " + (objCInvoices.lstCVarInvoices[0].InvoiceTypeID == 20 ? "DraftInvoiceID" : "InvoiceID") + " = " + objCVarInvoicesTax.ID.ToString() + " AND IsDeleted=0),2)";
                        //            else
                        //                pUpdateClause += " , Amount = ROUND((ISNULL(AmountWithoutVAT, 0) + ROUND( (ISNULL(AmountWithoutVAT, 0) * ISNULL(TaxPercentage, 0) / 100),2) - ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2)),2)";
                        //            pUpdateClause += " WHERE ID = " + objCVarInvoicesTax.ID.ToString();
                        //            checkException = objCInvoicesTax.UpdateList(pUpdateClause);
                        //            #endregion Update Invoice totals at server side to fix any connection problem



                        //        }

                        //    }
                        //    //link
                        //    CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
                        //    objCCustomizedDBCall.ExecuteQuery_DataTable("insertTaxLink " + ArrInvoiceIDs[i] + "," + objCVarInvoicesTax.ID + "," + "Invoices");

                        //    #region Call ERP JV Entry (They approve just one at a time)
                        //    CGroups objCGroups1 = new CGroups();
                        //    objCGroups1.GetList("WHERE GroupImageURL=N'Accounting'");
                        //    if (!objCGroups1.lstCVarGroups[0].IsInactive)
                        //    {
                        //        CCallCustomizedSP objCCallCustomizedSP = new CCallCustomizedSP();
                        //        checkException = objCCallCustomizedSP.CallCustomizedSP_MultiIDs("ERP_ForwWeb_PostingInvoiceTax", "," + objCVarInvoicesTax.ID + ",", WebSecurity.CurrentUserId, pIsApprove, pCostCenterID);
                        //    }
                        //    #endregion Call ERP JV Entry
                        //}

                        //#endregion

                        #endregion

                    }
                    //else if(objCTaxLink.lstCVarTaxLink.Count > 0 && objCTaxLinkCharge.lstCVarTaxLink.Count > 0)
                   else if (objCTaxLinkCharge.lstCVarTaxLink.Count > 0)
                    {
                        #region Call ERP JV Entry (They approve just one at a time)
                        CGroups objCGroups = new CGroups();
                        objCGroups.GetList("WHERE GroupImageURL=N'Accounting'");
                        if (!objCGroups.lstCVarGroups[0].IsInactive)
                        {
                            CCallCustomizedSP objCCallCustomizedSP = new CCallCustomizedSP();
                            checkException = objCCallCustomizedSP.CallCustomizedSP_MultiIDsTax("ERP_ForwWeb_PostingInvoiceTax", "," + ArrInvoiceIDs[i] + ",", WebSecurity.CurrentUserId, pIsApprove, pCostCenterID, pAccountIDCharge, pSubAccountCharge);
                        }
                        #endregion Call ERP JV Entry
                    }
                    else if (objCTaxLinkFOUND.lstCVarTaxLink.Count > 0)
                    {
                        #region Call ERP JV Entry (They approve just one at a time)
                        CGroups objCGroups = new CGroups();
                        objCGroups.GetList("WHERE GroupImageURL=N'Accounting'");
                        if (!objCGroups.lstCVarGroups[0].IsInactive)
                        {
                            CCallCustomizedSP objCCallCustomizedSP = new CCallCustomizedSP();
                            checkException = objCCallCustomizedSP.CallCustomizedSP_MultiIDsTax("ERP_ForwWeb_PostingInvoiceTax", "," + (objCTaxLinkFOUND.lstCVarTaxLink.Count > 0 ? objCTaxLinkFOUND.lstCVarTaxLink[0].OriginID : 0) + ",", WebSecurity.CurrentUserId, pIsApprove, pCostCenterID, pAccountIDCharge, pSubAccountCharge);
                        }
                        #endregion Call ERP JV Entry

                    }

                }

                if (CompanyName == "CHM")
                {
                    if (pIsApprove)
                    {
                        objCvwInvoices.GetListPaging(pPageSize, pPageNumber, "WHERE id not in(SELECT T.OriginID FROM ForwardingTransChemTax.dbo.TaxLink T WHERE Notes='Invoices' AND JVID IS NOT NULL AND OriginID IS NOT NULL)", pOrderBy, out _RowCount);

                    }
                    else
                    {
                        objCvwInvoices.GetListPaging(pPageSize, pPageNumber, "WHERE id  in(SELECT T.OriginID FROM ForwardingTransChemTax.dbo.TaxLink T WHERE Notes='Invoices' AND JVID IS NOT NULL AND OriginID IS NOT NULL)", pOrderBy, out _RowCount);

                    }
                }
                else if (CompanyName == "OCE")
                {
                    if (pIsApprove)
                    {
                        objCvwInvoices.GetListPaging(pPageSize, pPageNumber, "WHERE id not in(SELECT T.OriginID FROM ForwardingTROTax.dbo.TaxLink T WHERE Notes='Invoices' AND JVID IS NOT NULL AND OriginID IS NOT NULL)", pOrderBy, out _RowCount);

                    }
                    else
                    {
                        objCvwInvoices.GetListPaging(pPageSize, pPageNumber, "WHERE id in(SELECT T.OriginID FROM ForwardingTROTax.dbo.TaxLink T WHERE Notes='Invoices' AND JVID IS NOT NULL AND OriginID IS NOT NULL)", pOrderBy, out _RowCount);

                    }
                }

            }
            #endregion
            return new object[] {
                _result
                , new JavaScriptSerializer().Serialize(objCvwInvoices.lstCVarvwInvoices)
                , _result ? "" : checkException.Message
                , _ReturnedMessage //pData[3]
            };
        } //of fn

        [HttpPost, HttpGet]
        public object SetIsPrintOriginal(Int64 pInvoiceIDToSetPrintOriginal, bool pIsPrintOriginal)
        {
            Exception checkException = null;
            CInvoices objCInvoices = new CInvoices();
            checkException = objCInvoices.UpdateList("IsPrintOriginal=" + (pIsPrintOriginal ? "1" : "0") + " WHERE ID=" + pInvoiceIDToSetPrintOriginal);
            return new object[]
            {
            };
        }

        [HttpGet, HttpPost]
        public Object[] InvoiceApproval_PartnerTypeChanged(Int32 pPartnerTypeID, string pWhereClause, string pOrderBy, Int32 pPageNumber, Int32 pPageSize)
        {
            int _RowCount = 0;
            Exception checkException = null;
            CvwAccPartners objCvwAccPartners = new CvwAccPartners();
            CvwInvoices objCvwInvoices = new CvwInvoices();

            if (pPartnerTypeID != 0)
                checkException = objCvwAccPartners.GetListPaging(10000, 1, "WHERE PartnerTypeID=" + pPartnerTypeID, "Name", out _RowCount);
            checkException = objCvwInvoices.GetListPaging(pPageSize, pPageNumber, pWhereClause, pOrderBy, out _RowCount);
            return new object[]
            {
                new JavaScriptSerializer().Serialize(objCvwInvoices.lstCVarvwInvoices)
                , _RowCount
                , new JavaScriptSerializer().Serialize(objCvwAccPartners.lstCVarvwAccPartners)
            };
        }

        
        [HttpGet, HttpPost]
        public Object[] Insert_Update_NotificationforUsers_Invoice(Int32 pOperationID, string pUserIDs)
        {
            #region Sending Alarm
            Boolean _result = false;
            Exception checkException;
            if ((pUserIDs.Split(',').Length-1) != null)
            {
                CVarEmail objCVarEmail = new CVarEmail();
                CEmail objCEmail = new CEmail();
                CEmail objCEmailCheck = new CEmail();
                CEmailReceiver objCEmailReceiver = new CEmailReceiver();
                CEmailReceiver objCEmailReceiverCheck = new CEmailReceiver();
                CvwOperations objCvwOperations = new CvwOperations();
                int _RowCount = 0;
                objCvwOperations.GetListPaging(100,1," Where ID = "+pOperationID," ID",out _RowCount);
                objCVarEmail.Subject = "Incoming Task: Operation '" + objCvwOperations.lstCVarvwOperations[0].Code + "'";
                objCVarEmail.Body = "Operation No. " + objCvwOperations.lstCVarvwOperations[0].Code + "\n";
                    //+ "Task: " + objCvwOperationTracking.lstCVarvwOperationTracking[0].TrackingStageName + "\n"
                    //+ "Task Date: " + objCvwOperationTracking.lstCVarvwOperationTracking[0].StringTrackingDate + "\n"
                    //+ "Notes: " + objCvwOperationTracking.lstCVarvwOperationTracking[0].Notes + "\n";
                objCVarEmail.QuotationRouteID = 0;
                objCVarEmail.OperationID = pOperationID;
                objCVarEmail.SenderUserID = WebSecurity.CurrentUserId;
                objCVarEmail.SendingDate = DateTime.Now;
                objCEmail.lstCVarEmail.Add(objCVarEmail);
               
                checkException = objCEmail.SaveMethod(objCEmail.lstCVarEmail);
                if (checkException == null) //add to EmailReceivers
                {
                    var pArrayOfReceiversIDs = pUserIDs.Split(',');
                    var NoOfReceivers = pArrayOfReceiversIDs.Length-1;
                    for (int i = 0; i < NoOfReceivers; i++)
                    {
                        objCEmailCheck.GetList(" Where OperationID = " + pOperationID);
                        if(objCEmailCheck.lstCVarEmail.Count > 0)
                            objCEmailReceiverCheck.GetList(" Where operationID = "+pOperationID+" and UserID = "+ pArrayOfReceiversIDs[i] + "");
                        if(objCEmailReceiverCheck.lstCVarEmailReceiver.Count == 0)
                        {
                            CVarEmailReceiver objCVarEmailReceiver = new CVarEmailReceiver();
                            objCVarEmailReceiver.EmailID = objCVarEmail.ID;
                            objCVarEmailReceiver.ReceiverUserID = int.Parse(pArrayOfReceiversIDs[i]);
                            objCVarEmailReceiver.ReceivingDate = DateTime.Parse("01-01-1900");
                            objCVarEmailReceiver.IsAlarm = true;

                            objCEmailReceiver.lstCVarEmailReceiver.Add(objCVarEmailReceiver);
                        }
                        
                    }
                    checkException = objCEmailReceiver.SaveMethod(objCEmailReceiver.lstCVarEmailReceiver);
                }
                if (checkException == null)
                {
                    _result = true;
                }
            }
            #endregion Sending Alarm
           

            return new object[]
            {
                _result
            };
        }

        //ToExclude: Barcode="Excel"
        [HttpPost]
        public object[] ImportFromExcelFile([FromBody] pImportList pImportList)
        {
            var objExcelRows = new List<ClassExcelRows>();  //i did not merge here to be able to catch the line number if problems exist
            
            string _ReturnedMessage = "";
            Exception checkException = null;
            CDefaults objCDefaults = new CDefaults();
            
            decimal _Tariff_CT20 = 100;
            decimal _Tariff_CT40 = 200;

            var constMasterBLType = 3;
            var constImportDirectionType = 1;
            var constHouseBLType = 2;
            var HouseIconName = "fa-link";
            var strHouseIconStyleClassName = "house-icon-style"; //holds the css class name
            var MasterIconName = "fa-book";
            var strMasterIconStyleClassName = "master-icon-style"; //holds the css class name
            var ImportIconName = "fa-arrow-circle-down";
            var strImportIconStyleClassName = "import-icon-style"; //holds the css class name
            var OceanTransportType = 1;
            var OceanIconName = "fa-anchor";
            var strOceanIconStyleClassName = "ocean-icon-style"; //holds the css class name
            var constFCLShipmentType = 1;
            int MainCarraigeRoutingTypeID = 30;

            decimal _ExchangeRate = 0;
            int _CurrencyID = 0;
            int _ChargeTypeID = 0;
            int _TaxTypeID = 0;
            decimal _TaxPercentage = 0;
            int _VesselID = 0;
            int _MoveTypeID = 0;
            Int64 _OperationHeaderID = 0;
            int _DraftInvoiceTypeID = 0;

            int _RowCount = 0;

            var _ArrPOL = pImportList.pPOLList.Split(',');
            var _ArrPOD = pImportList.pPODList.Split(',');
            var _ArrContainerType = pImportList.pContainerTypeList.Split(',');
            var _ArrHouseNumber = pImportList.pHouseNumberList.Split(',');
            var _ArrConsignee = pImportList.pConsigneeList.Split(',');

            //var _ArrQuantity = pImportList.pQuantityList.Split(',');

            var _NumberOfRows = _ArrHouseNumber.Length;

            objCDefaults.GetListPaging(1, 1, "", "ID", out _RowCount);

            #region check Data
            CvwCustomersWithMinimalColumns objCCustomers_Search = new CvwCustomersWithMinimalColumns();
            CInvoiceTypes objCInvoiceTypes = new CInvoiceTypes();
            CvwChargeTypes objCvwChargeTypes = new CvwChargeTypes();
            CMoveTypes objCMoveTypes = new CMoveTypes();
            CVessels objCVessels = new CVessels();
            //CvwCurrencies objCvwCurrencies = new CvwCurrencies();
            CPorts objCPOL = new CPorts();
            CPorts objCPOD = new CPorts();
            
            checkException = objCInvoiceTypes.GetList("WHERE CODE=N'DRAFT'");
            checkException = objCMoveTypes.GetList("WHERE CODE=N'9 KM'");
            checkException = objCVessels.GetList("WHERE Name=N'" + pImportList.pVesselName + "'");
            checkException = objCvwChargeTypes.GetList("WHERE CODE=N'9 KM'");
            //checkException = objCvwCurrencies.GetList("WHERE CODE=N'USD'");

            if (objCInvoiceTypes.lstCVarInvoiceTypes.Count == 0)
                _ReturnedMessage = "Please, add DRAFT invoice type.";
            else if (objCMoveTypes.lstCVarMoveTypes.Count == 0)
                _ReturnedMessage = "Please, add 9 KM into service scopes.";
            else if (objCVessels.lstCVarVessels.Count == 0)
                _ReturnedMessage = "Sorry, check vessel.";
            else if (objCvwChargeTypes.lstCVarvwChargeTypes.Count == 0)
                _ReturnedMessage = "Please, add 9 KM into charge types.";
            //else if (objCvwCurrencies.lstCVarvwCurrencies.Count == 0)
            //    _ReturnedMessage = "Please, check the USD exchange rate.";
            //else if (objCvwCurrencies.lstCVarvwCurrencies[0].CurrentExchangeRate == 0)
            //    _ReturnedMessage = "Please, check the USD exchange rate.";
            else //Shared data exists, so assign IDs then check for each row data
            {
                _DraftInvoiceTypeID = objCInvoiceTypes.lstCVarInvoiceTypes[0].ID;
                _ChargeTypeID = objCvwChargeTypes.lstCVarvwChargeTypes[0].ID;
                _TaxTypeID = objCvwChargeTypes.lstCVarvwChargeTypes[0].TaxeTypeID;
                _TaxPercentage = objCvwChargeTypes.lstCVarvwChargeTypes[0].TaxPercentage;
                _VesselID = objCVessels.lstCVarVessels[0].ID;
                _MoveTypeID = objCMoveTypes.lstCVarMoveTypes[0].ID;

                _CurrencyID = objCDefaults.lstCVarDefaults[0].CurrencyID; //objCvwCurrencies.lstCVarvwCurrencies[0].ID;
                _ExchangeRate = 1; // objCvwCurrencies.lstCVarvwCurrencies[0].CurrentExchangeRate;
                
                for (int i = 0; i < _NumberOfRows; i++)
                {
                    int _POLID = 0;
                    int _POLCountryID = 0;
                    int _PODID = 0;
                    int _PODCountryID = 0;

                    checkException = objCPOL.GetList("WHERE Name=N'" + _ArrPOL[i] + "' OR Code = N'" + _ArrPOL[i] + "'");
                    _POLID = objCPOL.lstCVarPorts.Count == 0 ? 0 : objCPOL.lstCVarPorts[0].ID;
                    _POLCountryID = objCPOL.lstCVarPorts.Count == 0 ? 0 : objCPOL.lstCVarPorts[0].CountryID;

                    checkException = objCPOD.GetList("WHERE Name=N'" + _ArrPOD[i] + "' OR Code = N'" + _ArrPOD[i] + "'");
                    _PODID = objCPOD.lstCVarPorts.Count == 0 ? 0 : objCPOD.lstCVarPorts[0].ID;
                    _PODCountryID = objCPOD.lstCVarPorts.Count == 0 ? 0 : objCPOD.lstCVarPorts[0].CountryID;

                    if (_POLID == 0 || _PODID == 0 || _POLCountryID == 0 || _PODCountryID == 0)
                    {
                        _ReturnedMessage = "Row " + (i + 1) + " : Check ports " + _ArrPOL[i] + ", " + _ArrPOD[i];
                        break;
                    }
                    else //replace POL in the array with concatenation (POLID,POLCountyID) and same for POD and get ConsigneeIDs
                    {
                        _ArrPOL[i] = _POLID.ToString() + "," + _POLCountryID;
                        _ArrPOD[i] = _PODID.ToString() + "," + _PODCountryID;
                    }
                    #region Get CustomerID
                    checkException = objCCustomers_Search.GetList("WHERE Name=N'" + _ArrConsignee[i] + "'");
                    if (objCCustomers_Search.lstCVarvwCustomersWithMinimalColumns.Count > 0)
                        _ArrConsignee[i] = objCCustomers_Search.lstCVarvwCustomersWithMinimalColumns[0].ID.ToString();
                    #region Add customer if not exists
                    else
                    {                        
                        CVarCustomers objCVarCustomers = new CVarCustomers();
                        objCVarCustomers.SalesmanID = 0;
                        objCVarCustomers.PaymentTermID = 0;
                        objCVarCustomers.CurrencyID = _CurrencyID;
                        objCVarCustomers.TaxeTypeID = 0;

                        objCVarCustomers.Code = 0;
                        objCVarCustomers.Name = _ArrConsignee[i];
                        objCVarCustomers.LocalName = _ArrConsignee[i];
                        objCVarCustomers.Website = "0";
                        objCVarCustomers.Email = "0";
                        objCVarCustomers.IsConsignee = true;
                        objCVarCustomers.IsShipper = true;
                        objCVarCustomers.IsInternalCustomer = false;
                        objCVarCustomers.IsExternal = true;
                        objCVarCustomers.IsInactive = false;
                        objCVarCustomers.IsDeleted = false;
                        objCVarCustomers.Notes = "Added from Excel";
                        objCVarCustomers.Address = "0";
                        objCVarCustomers.PhonesAndFaxes = "0";

                        objCVarCustomers.VATNumber = "0";
                        objCVarCustomers.IsConsolidatedInvoice = false;
                        objCVarCustomers.BankName = "0";
                        objCVarCustomers.BankAddress = "0";
                        objCVarCustomers.Swift = "0";
                        objCVarCustomers.BankAccountNumber = "0";
                        objCVarCustomers.IBANNumber = "0";

                        objCVarCustomers.ForeignExporterNo = "0";
                        objCVarCustomers.ForeignExporterCountryID = 0;
                        objCVarCustomers.AccountID = 0;
                        objCVarCustomers.SubAccountID = 0;
                        objCVarCustomers.CostCenterID = 0;
                        objCVarCustomers.SubAccountGroupID = 0;

                        objCVarCustomers.ManagerRoleID = 5;
                        objCVarCustomers.AdministratorRoleID = 1;
                        objCVarCustomers.SalesLeadID = 0;

                        objCVarCustomers.BillingDetails = "0";
                        objCVarCustomers.ShippingDetails = "0";
                        objCVarCustomers.OriginalCMRbyPost = false;

                        objCVarCustomers.ForeignExporterNo = "0";

                        objCVarCustomers.TimeLocked = DateTime.Parse("01-01-1900");
                        objCVarCustomers.LockingUserID = 0;

                        objCVarCustomers.CreatorUserID = objCVarCustomers.ModificatorUserID = WebSecurity.CurrentUserId;
                        objCVarCustomers.CreationDate = objCVarCustomers.ModificationDate = DateTime.Now;

                        CCustomers objCCustomers = new CCustomers();
                        objCCustomers.lstCVarCustomers.Add(objCVarCustomers);
                        checkException = objCCustomers.SaveMethod(objCCustomers.lstCVarCustomers);

                        if (checkException == null)
                            _ArrConsignee[i] = objCVarCustomers.ID.ToString();
                        else
                        {
                            _ReturnedMessage = "Row " + (i + 1) + " : Error when adding consignee";
                            break;
                        }
                    }
                    #endregion Add customer if not exists
                    #endregion Get CustomerID
                    
                    //Add to objExcelRows
                    objExcelRows.Add(new ClassExcelRows
                    {
                        POLID = _POLID
                        ,
                        POLCountryID = _POLCountryID
                        ,
                        PODID = _PODID
                        ,
                        PODCountryID = _PODCountryID
                        ,
                        HouseNumber = _ArrHouseNumber[i]
                        ,
                        ConsigneeID = int.Parse(_ArrConsignee[i])
                        ,
                        Price = _ArrContainerType[i].Substring(0, 2) == "20" ? _Tariff_CT20 : _Tariff_CT40
                        ,
                        ContainerType = _ArrContainerType[i].Substring(0, 2) == "20" ? "20" : "40"
                    });
                } //for (int i = 0; i < _NumberOfRows; i++)
            }
            #endregion check Data

            #region Data is correct
            if (_ReturnedMessage == "") //now i have CurrencyID/ExchangeRate/ChargeTypeID/POL/POD
            {
                #region Create Operation Header
                CVarOperations objCVarOperations = new CVarOperations();

                objCVarOperations.HouseNumber = "0";
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
                objCVarOperations.NumberOfOriginalBills = 3;

                objCVarOperations.QuotationRouteID = 0;
                objCVarOperations.Code = "O" + DateTime.Now.Year.ToString().Substring(2,2) + "-IMP-OC-";
                objCVarOperations.BranchID = objCDefaults.lstCVarDefaults[0].BranchID;
                objCVarOperations.SalesmanID = WebSecurity.CurrentUserId;
                objCVarOperations.BLType = constMasterBLType;
                objCVarOperations.BLTypeIconName = MasterIconName;
                objCVarOperations.BLTypeIconStyle = strMasterIconStyleClassName;
                objCVarOperations.DirectionType = constImportDirectionType;
                objCVarOperations.DirectionIconName = ImportIconName;
                objCVarOperations.DirectionIconStyle = strImportIconStyleClassName;
                objCVarOperations.TransportType = OceanTransportType;
                objCVarOperations.TransportIconName = OceanIconName;
                objCVarOperations.TransportIconStyle = strOceanIconStyleClassName;
                objCVarOperations.ShipmentType = constFCLShipmentType;
                objCVarOperations.ShipperID = 0;
                objCVarOperations.ShipperAddressID = 0;
                objCVarOperations.ShipperContactID = 0;
                objCVarOperations.ConsigneeID = 0;
                objCVarOperations.ConsigneeAddressID = 0;
                objCVarOperations.ConsigneeContactID = 0;
                objCVarOperations.AgentID = 0;
                objCVarOperations.AgentAddressID = 0;
                objCVarOperations.AgentContactID = 0;
                objCVarOperations.IncotermID = 0;
                objCVarOperations.CommodityID = 0;
                objCVarOperations.CommodityID2 = 0;
                objCVarOperations.CommodityID3 = 0;
                //objCVarOperations.TransientTime = int.Parse(createOperationFromQuotationData.pTransientTime); //Come From QuotationRoute(put in main route not here)
                //objCVarOperations.OpenDate = DateTime.Parse(createOperationFromQuotationData.pOpenDate); //this format has problem when works on server
                objCVarOperations.OpenDate = DateTime.Now;
                objCVarOperations.CloseDate = DateTime.Parse("01-01-2060");
                objCVarOperations.CutOffDate = DateTime.Parse("01-01-1900"); 
                objCVarOperations.IncludePickup = false;
                objCVarOperations.PickupCityID = 0;
                objCVarOperations.PickupAddressID = 0;
                objCVarOperations.POLCountryID = int.Parse(_ArrPOL[0].Split(',')[1]);
                objCVarOperations.POL = int.Parse(_ArrPOL[0].Split(',')[0]);
                objCVarOperations.PODCountryID = int.Parse(_ArrPOD[0].Split(',')[1]);
                objCVarOperations.POD = int.Parse(_ArrPOD[0].Split(',')[0]);
                objCVarOperations.PickupAddress = "0";
                objCVarOperations.DeliveryAddress = "0";
                objCVarOperations.MoveTypeID = _MoveTypeID; //9 KM
                objCVarOperations.ShippingLineID = 0;
                objCVarOperations.AirlineID = 0;
                objCVarOperations.TruckerID = 0;
                objCVarOperations.IncludeDelivery = false;
                objCVarOperations.DeliveryZipCode = "0";
                objCVarOperations.DeliveryCityID = 0;
                objCVarOperations.DeliveryCountryID = 0;
                objCVarOperations.GrossWeight = 0;
                objCVarOperations.Volume = 0;
                objCVarOperations.VolumetricWeight = 0;
                objCVarOperations.ChargeableWeight = 0;
                objCVarOperations.NumberOfPackages = 0;
                objCVarOperations.IsDangerousGoods = false;
                objCVarOperations.CustomerReference = "0";
                objCVarOperations.SupplierReference = "0";
                objCVarOperations.PONumber = "0";
                objCVarOperations.POValue = "0";
                objCVarOperations.ReleaseNumber = "0";
                objCVarOperations.Notes = "Excel";
                objCVarOperations.AgreedRate = "0";
                objCVarOperations.OperationStageID = 60;

                objCVarOperations.IsDelivered = false;
                objCVarOperations.IsTrucking = false;
                objCVarOperations.IsInsurance = false;
                objCVarOperations.IsClearance = false;
                objCVarOperations.IsGenset = false;
                objCVarOperations.IsCourrier = false;
                objCVarOperations.MarksAndNumbers = "0";
                objCVarOperations.IsTelexRelease = false;

                objCVarOperations.CreatorUserID = objCVarOperations.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarOperations.CreationDate = objCVarOperations.ModificationDate = DateTime.Now;

                #region AirAgents (Venus fields A.Medra)
                objCVarOperations.BLDate = DateTime.Parse("01/01/1900");
                objCVarOperations.MAWBStockID = 0;
                objCVarOperations.TypeOfStockID = 0;
                objCVarOperations.FlightNo = "0";
                objCVarOperations.POrC = 1;
                objCVarOperations.IsAWB = false; //objCOperationToCopy.lstCVarOperations[0].IsAWB;
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
                objCVarOperations.Barcode = "Excel";

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
                objCVarOperations.CurrencyID = _CurrencyID;
                objCVarOperations.AccountingInformation = "0";
                objCVarOperations.ReferenceNumber = "0";
                objCVarOperations.OptionalShippingInformation = "0";
                objCVarOperations.CHGSCode = "0";
                objCVarOperations.WT_VALL_Other = "0";
                objCVarOperations.DeclaredValueForCustoms = "0";
                objCVarOperations.Tax = 0;
                objCVarOperations.OtherChargesDueCarrier = 0;
                objCVarOperations.WT_VALL = "0";
                objCVarOperations.Description = "0";
                #endregion Venus fields A.Medra

                objCVarOperations.DismissalPermissionSerial = "0";
                objCVarOperations.DeliveryOrderSerial = "0";

                objCVarOperations.eFBLID = "0";
                objCVarOperations.eFBLStatus = 0;

                objCVarOperations.DispatchNumber = "0";
                objCVarOperations.BusinessUnit = "0";
                objCVarOperations.Form13Number = "0";

                objCVarOperations.IsFleet = false;
                
                
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

                #endregion Create Operation Header
                
                #region Add Main Route
                if (checkException == null)
                {
                    _OperationHeaderID = objCOperations.lstCVarOperations[0].ID;
                    CVarRoutings objCVarRoutings = new CVarRoutings();
                    
                    objCVarRoutings.OperationID = _OperationHeaderID;
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
                    objCVarRoutings.ExpectedDeparture = DateTime.Parse("01-01-1900"); 
                    objCVarRoutings.ActualDeparture = DateTime.Parse("01-01-1900");
                    objCVarRoutings.ExpectedArrival = DateTime.Parse("01-01-1900");
                    objCVarRoutings.ActualArrival = DateTime.Parse("01-01-1900");
                    objCVarRoutings.VesselID = _VesselID;
                    objCVarRoutings.VoyageOrTruckNumber = pImportList.pVoyageNumber;
                    objCVarRoutings.RoadNumber = "0";
                    objCVarRoutings.DeliveryOrderNumber = "0";
                    objCVarRoutings.WareHouse = "0";
                    objCVarRoutings.WareHouseLocation = "0";
                    objCVarRoutings.Notes = "";
                    
                    objCVarRoutings.ShippingLineID = objCOperations.lstCVarOperations[0].ShippingLineID;
                    objCVarRoutings.AirlineID = 0;
                    objCVarRoutings.TruckerID = 0;
                   
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
                }
                else
                    _ReturnedMessage = checkException.Message;
                #endregion Add Main Route

                #region Merging lines
                var MergedRows = objExcelRows
                    .GroupBy(g => new { g.POLID, g.PODID, g.ConsigneeID , g.HouseNumber})
                    .Select(s => new {
                        POLID = s.First().POLID,
                        PODID = s.First().PODID,
                        POLCountryID = s.First().POLCountryID,
                        PODCountryID = s.First().PODCountryID,
                        ConsigneeID = s.First().ConsigneeID,
                        HouseNumber = s.First().HouseNumber,
                        Price = (s.Count(i => i.ContainerType == "20") * _Tariff_CT20) + (s.Count(i => i.ContainerType == "40") * _Tariff_CT40),
                        ContainerTypes = s.Count(i => i.ContainerType == "20") + "x" + "20" + " - " + s.Count(i => i.ContainerType == "40") + "x" + "40"
                    }).ToList();
                #endregion Merging lines

                #region Adding Houses/........
                _NumberOfRows = MergedRows.Count();

                for (int i = 0; i < _NumberOfRows; i++)
                {
                    //now i have CustomerID/CurrencyID/ExchangeRate
                    #region Create Operation/HBL/Routings/OperationPartners/Receivables/Invoices
                    if (_ReturnedMessage == "")
                    {
                        #region Create HBL
                        CVarOperations objCVarHBL = new CVarOperations();

                        objCVarHBL.MasterOperationID = _OperationHeaderID;
                        objCVarHBL.HouseNumber = MergedRows[i].HouseNumber;
                        objCVarHBL.MasterBL = "0";
                        objCVarHBL.MAWBSuffix = "0";
                        objCVarHBL.BLDate = DateTime.Parse("01-01-1900");
                        objCVarHBL.HBLDate = DateTime.Parse("01-01-1900");
                        objCVarHBL.ReleaseDate = DateTime.Parse("01-01-1900");
                        objCVarHBL.PODate = DateTime.Parse("01-01-1900");

                        objCVarHBL.ClearanceApprovalDate = DateTime.Parse("01-01-1900");
                        objCVarHBL.TruckingApprovalDate = DateTime.Parse("01-01-1900");
                        objCVarHBL.FreightApprovalDate = DateTime.Parse("01-01-1900");

                        objCVarHBL.ShippedOnBoardDate = DateTime.Parse("01-01-1900");
                        objCVarHBL.FreightPayableAt = "0";
                        objCVarHBL.CertificateNumber = "0";
                        objCVarHBL.CountryOfOrigin = "0";
                        objCVarHBL.InvoiceValue = "0";
                        objCVarHBL.NumberOfOriginalBills = 3;

                        objCVarHBL.QuotationRouteID = 0;
                        objCVarHBL.Code = "0";
                        objCVarHBL.BranchID = objCDefaults.lstCVarDefaults[0].BranchID;
                        objCVarHBL.SalesmanID = WebSecurity.CurrentUserId;
                        objCVarHBL.BLType = constHouseBLType;
                        objCVarHBL.BLTypeIconName = HouseIconName;
                        objCVarHBL.BLTypeIconStyle = strHouseIconStyleClassName;
                        objCVarHBL.DirectionType = constImportDirectionType;
                        objCVarHBL.DirectionIconName = ImportIconName;
                        objCVarHBL.DirectionIconStyle = strImportIconStyleClassName;
                        objCVarHBL.TransportType = OceanTransportType;
                        objCVarHBL.TransportIconName = OceanIconName;
                        objCVarHBL.TransportIconStyle = strOceanIconStyleClassName;
                        objCVarHBL.ShipmentType = constFCLShipmentType;
                        objCVarHBL.ShipperID = 0;
                        objCVarHBL.ShipperAddressID = 0;
                        objCVarHBL.ShipperContactID = 0;
                        objCVarHBL.ConsigneeID = MergedRows[i].ConsigneeID;
                        objCVarHBL.ConsigneeAddressID = 0;
                        objCVarHBL.ConsigneeContactID = 0;
                        objCVarHBL.AgentID = 0;
                        objCVarHBL.AgentAddressID = 0;
                        objCVarHBL.AgentContactID = 0;
                        objCVarHBL.IncotermID = 0;
                        objCVarHBL.CommodityID = 0;
                        //objCVarOperations.TransientTime = int.Parse(createOperationFromQuotationData.pTransientTime); //Come From QuotationRoute(put in main route not here)
                        //objCVarOperations.OpenDate = DateTime.Parse(createOperationFromQuotationData.pOpenDate); //this format has problem when works on server
                        objCVarHBL.OpenDate = DateTime.Now;
                        objCVarHBL.CloseDate = DateTime.Parse("01-01-2060");
                        objCVarHBL.CutOffDate = DateTime.Parse("01-01-1900");
                        objCVarHBL.IncludePickup = false;
                        objCVarHBL.PickupCityID = 0;
                        objCVarHBL.PickupAddressID = 0;
                        objCVarHBL.POLCountryID = MergedRows[i].POLCountryID;
                        objCVarHBL.POL = MergedRows[i].POLID;
                        objCVarHBL.PODCountryID = MergedRows[i].PODCountryID;
                        objCVarHBL.POD = MergedRows[i].PODID;
                        objCVarHBL.PickupAddress = "0";
                        objCVarHBL.DeliveryAddress = "0";
                        objCVarHBL.MoveTypeID = _MoveTypeID; //9 KM
                        objCVarHBL.ShippingLineID = 0;
                        objCVarHBL.AirlineID = 0;
                        objCVarHBL.TruckerID = 0;
                        objCVarHBL.IncludeDelivery = false;
                        objCVarHBL.DeliveryZipCode = "0";
                        objCVarHBL.DeliveryCityID = 0;
                        objCVarHBL.DeliveryCountryID = 0;
                        objCVarHBL.GrossWeight = 0;
                        objCVarHBL.Volume = 0;
                        objCVarHBL.VolumetricWeight = 0;
                        objCVarHBL.ChargeableWeight = 0;
                        objCVarHBL.NumberOfPackages = 0;
                        objCVarHBL.IsDangerousGoods = false;
                        objCVarHBL.CustomerReference = "0";
                        objCVarHBL.SupplierReference = "0";
                        objCVarHBL.PONumber = "0";
                        objCVarHBL.POValue = "0";
                        objCVarHBL.ReleaseNumber = "0";
                        objCVarHBL.Notes = "Excel";
                        objCVarHBL.AgreedRate = "0";
                        objCVarHBL.OperationStageID = 60;

                        objCVarHBL.IsDelivered = false;
                        objCVarHBL.IsTrucking = false;
                        objCVarHBL.IsInsurance = false;
                        objCVarHBL.IsClearance = false;
                        objCVarHBL.IsGenset = false;
                        objCVarHBL.IsCourrier = false;
                        objCVarHBL.MarksAndNumbers = "0";
                        objCVarHBL.IsTelexRelease = false;

                        objCVarHBL.CreatorUserID = objCVarHBL.ModificatorUserID = WebSecurity.CurrentUserId;
                        objCVarHBL.CreationDate = objCVarHBL.ModificationDate = DateTime.Now;

                        #region AirAgents (Venus fields A.Medra)
                        objCVarHBL.BLDate = DateTime.Parse("01/01/1900");
                        objCVarHBL.MAWBStockID = 0;
                        objCVarHBL.TypeOfStockID = 0;
                        objCVarHBL.FlightNo = "0";
                        objCVarHBL.POrC = 1;
                        objCVarHBL.IsAWB = false; //objCOperationToCopy.lstCVarOperations[0].IsAWB;
                                                         //Fields not in insert
                        objCVarHBL.AirLineID1 = 0;
                        objCVarHBL.FlightNo1 = "0";
                        objCVarHBL.FlightDate1 = DateTime.Parse("01/01/1900");
                        objCVarHBL.AirLineID2 = 0;
                        objCVarHBL.FlightNo2 = "0";
                        objCVarHBL.FlightDate2 = DateTime.Parse("01/01/1900");
                        objCVarHBL.AirLineID3 = 0;
                        objCVarHBL.FlightNo3 = "0";
                        objCVarHBL.FlightDate3 = DateTime.Parse("01/01/1900");

                        objCVarHBL.UNOrID = "0";
                        objCVarHBL.ProperShippingName = "0";
                        objCVarHBL.ClassOrDivision = "0";
                        objCVarHBL.PackingGroup = "0";
                        objCVarHBL.QuantityAndTypeOfPacking = "0";
                        objCVarHBL.PackingInstruction = "0";
                        objCVarHBL.ShippingDeclarationAuthorization = "0";
                        objCVarHBL.Barcode = "Excel";

                        objCVarHBL.GuaranteeLetterNumber = "0";
                        objCVarHBL.GuaranteeLetterDate = DateTime.Parse("01/01/1900");
                        objCVarHBL.GuaranteeLetterAmount = "0";
                        objCVarHBL.GuaranteeLetterSupplierInvoiceNumber = "0";
                        objCVarHBL.BankAccountID = 0;
                        objCVarHBL.GuaranteeLetterNotes = "0";

                        objCVarHBL.HandlingInformation = "0";
                        objCVarHBL.AmountOfInsurance = "0";
                        objCVarHBL.DeclaredValueForCarriage = "0";
                        objCVarHBL.WeightCharge = 0;
                        objCVarHBL.ValuationCharge = 0;
                        objCVarHBL.OtherChargesDueAgent = 0;
                        objCVarHBL.OtherCharges = "0";
                        objCVarHBL.CurrencyID = _CurrencyID;
                        objCVarHBL.AccountingInformation = "0";
                        objCVarHBL.ReferenceNumber = "0";
                        objCVarHBL.OptionalShippingInformation = "0";
                        objCVarHBL.CHGSCode = "0";
                        objCVarHBL.WT_VALL_Other = "0";
                        objCVarHBL.DeclaredValueForCustoms = "0";
                        objCVarHBL.Tax = 0;
                        objCVarHBL.OtherChargesDueCarrier = 0;
                        objCVarHBL.WT_VALL = "0";
                        objCVarHBL.Description = MergedRows[i].ContainerTypes;
                        #endregion Venus fields A.Medra

                        objCVarHBL.DismissalPermissionSerial = "0";
                        objCVarHBL.DeliveryOrderSerial = "0";

                        objCVarHBL.eFBLID = "0";
                        objCVarHBL.eFBLStatus = 0;

                        objCVarHBL.DispatchNumber = "0";
                        objCVarHBL.BusinessUnit = "0";
                        objCVarHBL.Form13Number = "0";

                        
                        objCVarHBL.IMOClass = Decimal.Zero;
                        objCVarHBL.UNNumber = 0;
                        objCVarHBL.VesselID = 0;
                        objCVarHBL.BookingNumber = "0";
                        objCVarHBL.ACIDNumber = "0";
                        objCVarHBL.ACIDDetails = "0";
                        objCVarHBL.HouseParentID = 0; 
                        
                        objCVarHBL.IsFleet = false; //just to be used as a flag like barcode

                        COperations objCHBL = new COperations();
                        objCHBL.lstCVarOperations.Add(objCVarHBL);
                        checkException = objCHBL.SaveMethod(objCHBL.lstCVarOperations);

                        #endregion Create HBL

                        #region Create route for house
                        Int64 _HouseID = objCVarHBL.ID;
                        if (checkException != null)
                            _ReturnedMessage = "Row " + (i + 1) + " " + checkException.Message;
                        if (_ReturnedMessage == "")
                        {
                            _HouseID = objCHBL.lstCVarOperations[0].ID;
                            CVarRoutings objCVarRoutings = new CVarRoutings();

                            objCVarRoutings.OperationID = _HouseID;
                            objCVarRoutings.TransportType = objCHBL.lstCVarOperations[0].TransportType;
                            objCVarRoutings.TransportIconName = objCHBL.lstCVarOperations[0].TransportIconName;
                            objCVarRoutings.TransportIconStyle = objCHBL.lstCVarOperations[0].TransportIconStyle;
                            objCVarRoutings.RoutingTypeID = MainCarraigeRoutingTypeID; //Main Carraige
                            objCVarRoutings.POLCountryID = objCHBL.lstCVarOperations[0].POLCountryID;
                            objCVarRoutings.POL = objCHBL.lstCVarOperations[0].POL;
                            objCVarRoutings.PODCountryID = objCHBL.lstCVarOperations[0].PODCountryID;
                            objCVarRoutings.POD = objCHBL.lstCVarOperations[0].POD;
                            objCVarRoutings.ETAPOLDate = DateTime.Parse("01-01-1900");
                            objCVarRoutings.ATAPOLDate = DateTime.Parse("01-01-1900");
                            //objCVarRoutings.ActualDeparture = DateTime.ParseExact(insertOperationData.pExpectedDeparture, "dd/MM/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);
                            objCVarRoutings.ExpectedDeparture = DateTime.Parse("01-01-1900");
                            objCVarRoutings.ActualDeparture = DateTime.Parse("01-01-1900");
                            objCVarRoutings.ExpectedArrival = DateTime.Parse("01-01-1900");
                            objCVarRoutings.ActualArrival = DateTime.Parse("01-01-1900");
                            objCVarRoutings.VesselID = _VesselID;
                            objCVarRoutings.VoyageOrTruckNumber = pImportList.pVoyageNumber;
                            objCVarRoutings.RoadNumber = "0";
                            objCVarRoutings.DeliveryOrderNumber = "0";
                            objCVarRoutings.WareHouse = "0";
                            objCVarRoutings.WareHouseLocation = "0";
                            objCVarRoutings.Notes = "";

                            objCVarRoutings.ShippingLineID = objCHBL.lstCVarOperations[0].ShippingLineID;
                            objCVarRoutings.AirlineID = 0;
                            objCVarRoutings.TruckerID = 0;

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
                            checkException = objCRoutings.SaveMethod(objCRoutings.lstCVarRoutings);
                            if (checkException != null)
                                _ReturnedMessage = "Row " + (i + 1) + " " + checkException.Message;
                        }
                        #endregion Create route for house

                        #region Add House Partner (Add Consignee)
                        Int64 _OperationPartnerID = 0;
                        if (_ReturnedMessage == "")
                        {
                            COperationPartners objCOperationPartners = new COperationPartners();

                            CVarOperationPartners objCVarOperationAgentPartner = new CVarOperationPartners();
                            objCVarOperationAgentPartner.OperationID = _HouseID;
                            objCVarOperationAgentPartner.OperationPartnerTypeID = 6; //Agent
                            objCVarOperationAgentPartner.AgentID = 0;
                            objCVarOperationAgentPartner.ContactID = 0;
                            objCVarOperationAgentPartner.IsOperationClient = false;
                            objCVarOperationAgentPartner.CreatorUserID = objCVarOperationAgentPartner.ModificatorUserID = WebSecurity.CurrentUserId;
                            objCVarOperationAgentPartner.CreationDate = objCVarOperationAgentPartner.ModificationDate = DateTime.Now;
                            objCOperationPartners.lstCVarOperationPartners.Add(objCVarOperationAgentPartner);
                            
                            CVarOperationPartners objCVarOperationConsigneePartner = new CVarOperationPartners();
                            objCVarOperationConsigneePartner.OperationID = _HouseID;
                            objCVarOperationConsigneePartner.OperationPartnerTypeID = 2;
                            objCVarOperationConsigneePartner.CustomerID = MergedRows[i].ConsigneeID;
                            objCVarOperationConsigneePartner.ContactID = 0;
                            objCVarOperationConsigneePartner.IsOperationClient = false;
                            objCVarOperationConsigneePartner.CreatorUserID = objCVarOperationConsigneePartner.ModificatorUserID = WebSecurity.CurrentUserId;
                            objCVarOperationConsigneePartner.CreationDate = objCVarOperationConsigneePartner.ModificationDate = DateTime.Now;
                            objCOperationPartners.lstCVarOperationPartners.Add(objCVarOperationConsigneePartner);
                            
                            CVarOperationPartners objCVarOperationShipperPartner = new CVarOperationPartners();
                            objCVarOperationShipperPartner.OperationID = _HouseID;
                            objCVarOperationShipperPartner.OperationPartnerTypeID = 1; // export or domestic (shipper)
                            objCVarOperationShipperPartner.CustomerID = 0;
                            objCVarOperationShipperPartner.ContactID = 0;
                            objCVarOperationShipperPartner.IsOperationClient = false;
                            objCVarOperationShipperPartner.CreatorUserID = objCVarOperationShipperPartner.ModificatorUserID = WebSecurity.CurrentUserId;
                            objCVarOperationShipperPartner.CreationDate = objCVarOperationShipperPartner.ModificationDate = DateTime.Now;
                            objCOperationPartners.lstCVarOperationPartners.Add(objCVarOperationShipperPartner);
                            
                            CVarOperationPartners objCVarOperationNotifyPartner = new CVarOperationPartners();
                            objCVarOperationNotifyPartner.OperationID = _HouseID;
                            objCVarOperationNotifyPartner.OperationPartnerTypeID = 4;//Notify1
                            objCVarOperationNotifyPartner.CustomerID = 0;
                            objCVarOperationNotifyPartner.ContactID = 0;
                            objCVarOperationNotifyPartner.IsOperationClient = false;
                            objCVarOperationNotifyPartner.CreatorUserID = objCVarOperationNotifyPartner.ModificatorUserID = WebSecurity.CurrentUserId;
                            objCVarOperationNotifyPartner.CreationDate = objCVarOperationNotifyPartner.ModificationDate = DateTime.Now;
                            objCOperationPartners.lstCVarOperationPartners.Add(objCVarOperationNotifyPartner);
                            
                            checkException = objCOperationPartners.SaveMethod(objCOperationPartners.lstCVarOperationPartners);
                            if (checkException == null)
                                _OperationPartnerID = objCVarOperationConsigneePartner.ID;
                            else
                                _ReturnedMessage = "Row " + (i + 1) + " " + checkException.Message;
                        }
                        #endregion Add House Partner

                        #region Add Invoice
                        Int64 _DraftInvoiceID = 0;

                        decimal _AmountWithoutVAT = Math.Round(MergedRows[i].Price, 2);
                        decimal _TaxAmount = Math.Round(((_AmountWithoutVAT * _TaxPercentage) / 100), 2);

                        if (_ReturnedMessage == "")
                        {
                            CVarInvoices objCVarInvoices = new CVarInvoices();

                            objCVarInvoices.IsDraftApproved = true;

                            objCVarInvoices.InvoiceNumber = 0;
                            objCVarInvoices.OperationID = _HouseID;
                            objCVarInvoices.OperationPartnerID = _OperationPartnerID;
                            objCVarInvoices.AddressID = 0;
                            objCVarInvoices.InvoiceTypeID = _DraftInvoiceTypeID;
                            objCVarInvoices.PrintedAddress = "0";
                            objCVarInvoices.CustomerReference = "0";
                            objCVarInvoices.PaymentTermID = 0;
                            objCVarInvoices.CurrencyID = _CurrencyID;
                            objCVarInvoices.ExchangeRate = _ExchangeRate;
                            objCVarInvoices.InvoiceDate = DateTime.Now;
                            objCVarInvoices.DueDate = DateTime.Now;



                            objCVarInvoices.AmountWithoutVAT = _AmountWithoutVAT;
                            objCVarInvoices.TaxTypeID = _TaxTypeID;
                            objCVarInvoices.TaxPercentage = _TaxPercentage;
                            objCVarInvoices.TaxAmount = _TaxAmount;
                            objCVarInvoices.DiscountTypeID = 0; //pDiscountTypeID;
                            objCVarInvoices.DiscountPercentage = 0; //pDiscountPercentage;
                            objCVarInvoices.DiscountAmount = 0; //pDiscountAmount;
                            objCVarInvoices.FixedDiscount = 0; //pFixedDiscount;
                            objCVarInvoices.Amount = objCVarInvoices.AmountWithoutVAT + objCVarInvoices.TaxAmount;

                            objCVarInvoices.InvoiceStatusID = 1;
                            objCVarInvoices.IsApproved = false;
                            objCVarInvoices.ApprovingUserID = WebSecurity.CurrentUserId;
                            objCVarInvoices.LeftSignature = "0";
                            objCVarInvoices.MiddleSignature = "0";
                            objCVarInvoices.RightSignature = "0";
                            objCVarInvoices.GRT = "0";
                            objCVarInvoices.DWT = "0";
                            objCVarInvoices.NRT = "0";
                            objCVarInvoices.LOA = "0";
                            objCVarInvoices.EditableNotes = "0";
                            objCVarInvoices.OperationContainersAndPackagesID = 0; //pTankID;
                            objCVarInvoices.TransactionTypeID = 0;

                            objCVarInvoices.Notes = MergedRows[i].ContainerTypes;
                            objCVarInvoices.CutOffDate = DateTime.Parse("01/01/1900");
                            objCVarInvoices.Is3PL = false;

                            objCVarInvoices.CreatorUserID = objCVarInvoices.ModificatorUserID = WebSecurity.CurrentUserId;
                            objCVarInvoices.CreationDate = objCVarInvoices.ModificationDate = DateTime.Now;

                            CInvoices objCInvoices = new CInvoices();
                            objCInvoices.lstCVarInvoices.Add(objCVarInvoices);
                            checkException = objCInvoices.SaveMethod(objCInvoices.lstCVarInvoices);
                            if (checkException == null)
                                _DraftInvoiceID = objCVarInvoices.ID;
                            else
                                _ReturnedMessage = "Row " + (i + 1) + " " + checkException.Message;
                            //CReceivables objCReceivables = new CReceivables();
                            //checkException = objCReceivables.UpdateList("InvoiceID=" + objCVarInvoices.ID + " WHERE ID IN(" + _ReceivablesIDs + ")");
                            //_InvoiceIDs += "," + objCVarInvoices.ID;
                            #region Update Invoice totals at server side to fix any connection problem
                            //string pUpdateClause = "";
                            ////SET AmountWithoutVAT
                            //pUpdateClause = " AmountWithoutVAT = ROUND((SELECT SUM(ISNULL(SalePrice,0) * ISNULL(Quantity,1))-" + 0 /*pFixedDiscount*/ + " FROM Receivables WHERE InvoiceID = " + objCVarInvoices.ID.ToString() + " AND IsDeleted=0),2)";
                            //pUpdateClause += " WHERE ID = " + objCVarInvoices.ID.ToString();
                            //checkException = objCInvoices.UpdateList(pUpdateClause);
                            ////SET Tax, Discount & Total Amount after setting the AmountWithVAT
                            //pUpdateClause = " TaxAmount = ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(TaxPercentage, 0) / 100),2)";
                            //pUpdateClause += " , DiscountAmount = ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2)";
                            //if (objCDefaults.lstCVarDefaults[0].IsTaxOnItems)
                            //    pUpdateClause += " , Amount = - ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2) + ROUND((SELECT SUM(ISNULL(SaleAmount,0))-" + 0 /*pFixedDiscount*/ + " FROM Receivables WHERE InvoiceID = " + objCVarInvoices.ID.ToString() + " AND IsDeleted=0),2)";
                            //else
                            //    pUpdateClause += " , Amount = ROUND((ISNULL(AmountWithoutVAT, 0) + ROUND( (ISNULL(AmountWithoutVAT, 0) * ISNULL(TaxPercentage, 0) / 100),2) - ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2)),2)";
                            //pUpdateClause += " WHERE ID = " + objCVarInvoices.ID.ToString();
                            //checkException = objCInvoices.UpdateList(pUpdateClause);
                            #endregion Update Invoice totals at server side to fix any connection problem
                        }
                        #endregion Add Invoice

                        #region Add Receivable
                        if (_ReturnedMessage == "")
                        {
                            #region Add Receivable
                            CVarReceivables objCVarReceivables = new CVarReceivables();
                            objCVarReceivables.CreatorUserID = WebSecurity.CurrentUserId;
                            objCVarReceivables.CreationDate = DateTime.Now;
                            objCVarReceivables.GeneratingQRID = 0;
                            //objCVarReceivables.InvoiceID = _InvoiceID;
                            objCVarReceivables.DraftInvoiceID = _DraftInvoiceID;
                            objCVarReceivables.AccNoteID = 0;
                            objCVarReceivables.OperationContainersAndPackagesID = 0;
                            objCVarReceivables.HouseBillID = _HouseID;

                            objCVarReceivables.OperationVehicleID = 0;
                            //objCVarReceivables.VehicleAgingReportID = objCvwWH_VehicleAgingReport.lstCVarvwWH_VehicleAgingReport.Count == 0
                            //                                            ? 0
                            //                                            : objCvwWH_VehicleAgingReport.lstCVarvwWH_VehicleAgingReport[0].ID;
                            objCVarReceivables.VehicleAgingReportID = 0;

                            objCVarReceivables.ID = 0;

                            objCVarReceivables.OperationID = _OperationHeaderID;
                            objCVarReceivables.ChargeTypeID = _ChargeTypeID;
                            objCVarReceivables.POrC = 0;
                            objCVarReceivables.MeasurementID = 0;
                            objCVarReceivables.ContainerTypeID = 0;
                            objCVarReceivables.SupplierID = 0;
                            objCVarReceivables.Quantity = 1; // decimal.Parse(1/*_ArrQuantity[i]*/);
                            objCVarReceivables.CostPrice = 0;
                            objCVarReceivables.CostAmount = 0;
                            //objCVarReceivables.SalePrice = Math.Round((_NumberOfDays * objCWH_ContractDetails.lstCVarWH_ContractDetails[z].Rate + objCWH_ContractDetails.lstCVarWH_ContractDetails[z].Cost), 2);
                            objCVarReceivables.SalePrice = _AmountWithoutVAT;

                            objCVarReceivables.AmountWithoutVAT = _AmountWithoutVAT; //Math.Round((objCVarReceivables.Quantity * objCVarReceivables.SalePrice), 2);
                            objCVarReceivables.TaxeTypeID = 0;
                            objCVarReceivables.TaxPercentage = 0;
                            objCVarReceivables.TaxAmount = 0; //Math.Round(((objCVarReceivables.AmountWithoutVAT * objCVarReceivables.TaxPercentage) / 100), 2);
                            objCVarReceivables.DiscountTypeID = 0;
                            objCVarReceivables.DiscountPercentage = 0;
                            objCVarReceivables.DiscountAmount = 0;

                            objCVarReceivables.SaleAmount = objCVarReceivables.AmountWithoutVAT + objCVarReceivables.TaxAmount;
                            objCVarReceivables.ExchangeRate = _ExchangeRate;
                            objCVarReceivables.CurrencyID = _CurrencyID;
                            objCVarReceivables.Notes = MergedRows[i].ContainerTypes;

                            objCVarReceivables.IssueDate = DateTime.Now;

                            objCVarReceivables.PreviousCutOffDate = DateTime.Parse("01/01/1900");
                            objCVarReceivables.CutOffDate = DateTime.Parse("01/01/1900");
                            objCVarReceivables.ReceiptDate = DateTime.Parse("01/01/1900");
                            objCVarReceivables.ReceiptNo = "";

                            objCVarReceivables.ModificatorUserID = WebSecurity.CurrentUserId;
                            objCVarReceivables.ModificationDate = DateTime.Now;

                            CReceivables objCReceivables = new CReceivables();
                            objCReceivables.lstCVarReceivables.Add(objCVarReceivables);
                            checkException = objCReceivables.SaveMethod(objCReceivables.lstCVarReceivables);
                            if (checkException != null)
                                _ReturnedMessage = "Merged List Row " + (i + 1) + " " + checkException.Message;
                            //_ReceivablesIDs += "," + objCVarReceivables.ID;
                            #endregion Add Receivable
                        }
                        #endregion Add Receivable

                    }
                    #endregion Create Operation/HBL/Routings/OperationPartners/Receivables/Invoices
                }
                #endregion Adding Houses/........
            } //EOF else if (objCvwCurrencies.lstCVarvwCurrencies.Count == 1)
            #endregion Data is correct

            return new object[]
            {
                _ReturnedMessage //pData[0]
            };
        }

    }

    #region POST Parameters
    public class pSaveParameters
    {
        public bool pIsRemoveItems { get; set; }
        public Int64 pInvoiceID { get; set; }
        public Int64 pOperationID { get; set; }
        public Int64 pOperationPartnerID { get; set; }
        public Int32 pPartnerTypeID { get; set; }
        public Int32 pPartnerID { get; set; }
        public Int64 pAddressID { get; set; }
        /*public string pPrintedAddress { get; set; }*/
        public string pCustomerReference { get; set; }
        public int pPaymentTermID { get; set; }
        public int pCurrencyID { get; set; }
        public decimal pExchangeRate { get; set; }
        public string pInvoiceIssueDate { get; set; }
        public string pInvoiceDueDate { get; set; }
        public decimal pAmountWithoutVAT { get; set; }
        public Int32 pTaxTypeID { get; set; }
        public decimal pTaxPercentage { get; set; }
        public decimal pTaxAmount { get; set; }
        public Int32 pDiscountTypeID { get; set; }
        public decimal pDiscountPercentage { get; set; }
        public decimal pDiscountAmount { get; set; }
        public decimal pFixedDiscount { get; set; }
        public decimal pAmount { get; set; }
        public int pInvoiceStatusID { get; set; }
        public bool pIsApproved { get; set; }
        public string pLeftSignature { get; set; }
        public string pMiddleSignature { get; set; }
        public string pRightSignature { get; set; }
        public string pGRT { get; set; }
        public string pDWT { get; set; }
        public string pNRT { get; set; }
        public string pLOA { get; set; }
        public string pEditableNotes { get; set; }
        public Int64 pRoutingID { get; set; }
        public Int64 pRelatedToInvoiceID { get; set; }
        public bool pUpdateRelatedToInvoiceID { get; set; }
        public Int32 pTransactionTypeID { get; set; }
        //Receivable items to be updated
        public string pSelectedReceivablesIDsToUpdate { get; set; }
        public string pPOrCList { get; set; }
        public string pUOMList { get; set; }
        public string pQuantityList { get; set; }
        public string pSalePriceList { get; set; }
        public string pInvoiceItemAmountWithoutVATList { get; set; }
        public string pInvoiceItemTaxTypeIDList { get; set; }
        public string pInvoiceItemTaxPercentageList { get; set; }
        public string pInvoiceItemTaxAmountList { get; set; }
        public string pSaleAmountList { get; set; }
        public string pExchangeRateList { get; set; }
        public string pCurrencyList { get; set; }
        public string pViewOrderList { get; set; }
    }

    public class pImportList
    {
        public string pVesselName { get; set; }
        public string pVoyageNumber { get; set; }
        public string pPOLList { get; set; }
        public string pPODList { get; set; }
        public string pContainerTypeList { get; set; }
        public string pHouseNumberList { get; set; }
        public string pConsigneeList { get; set; }
        //public string pQuantityList { get; set; }
    }
    #endregion POST Parameters

    #region ClassExcelRows
    public class ClassExcelRows
    {
        public int POLID { get; set; }
        public int PODID { get; set; }
        public int POLCountryID { get; set; }
        public int PODCountryID { get; set; }
        public string ContainerType { get; set; }
        public string HouseNumber { get; set; }
        public int ConsigneeID { get; set; }
        public decimal Price { get; set; }
    }
    #endregion ClassExcelRows
}
