using Forwarding.MvcApp.Models.Administration.Security.Generated;
using Forwarding.MvcApp.Models.Administration.Settings.Generated;
using Forwarding.MvcApp.Models.MasterData.BanksAccountsAndTreasuries.Generated;
using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;
using Forwarding.MvcApp.Models.MasterData.Partners.Generated;
using Forwarding.MvcApp.Models.NoAccess.Generated;
using Forwarding.MvcApp.Models.Operations.Operations.Customized;
using Forwarding.MvcApp.Models.Operations.Operations.Generated;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Forwarding.MvcApp.Controllers.Reports.API_AccountingReports
{
    public class InvoicesReportsController : ApiController
    {
        [HttpGet, HttpPost]//pID : is the InvoiceID
        public object[] FillFilter()
        {
            int _RowCount = 0;
            CvwBranches objCvwBranches = new CvwBranches();
            objCvwBranches.GetList(" WHERE 1=1 ORDER BY Name ");

            CInvoiceTypes objCInvoiceTypes = new CInvoiceTypes();
            objCInvoiceTypes.GetList(" WHERE Code<>'DRAFT' ORDER BY Name ");

            CvwCustomersWithMinimalColumns objCvwCustomersWithMinimalColumns = new CvwCustomersWithMinimalColumns();
            //objCvwCustomersWithMinimalColumns.GetList(" ORDER BY Name ");

            CTaxeTypes objCTaxTypes = new CTaxeTypes();
            objCTaxTypes.GetList(" WHERE IsDiscount=0 ORDER BY Name ");

            CTaxeTypes objCDiscountTypes = new CTaxeTypes();
            objCDiscountTypes.GetList(" WHERE IsDiscount=1 ORDER BY Name ");

            CNoAccessPartnerTypes objCNoAccessPartnerTypes = new CNoAccessPartnerTypes();
            objCNoAccessPartnerTypes.GetList(" WHERE 1=1 ");

            CUsers objCSalesmen = new CUsers();
            objCSalesmen.GetListPaging(999999, 1, "WHERE IsSalesman=1", "Name", out _RowCount);

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] { 
                new JavaScriptSerializer().Serialize(objCvwBranches.lstCVarvwBranches)//data[0]
                , serializer.Serialize(objCvwCustomersWithMinimalColumns.lstCVarvwCustomersWithMinimalColumns)//data[1]
                , new JavaScriptSerializer().Serialize(objCTaxTypes.lstCVarTaxeTypes)//data[2]
                , new JavaScriptSerializer().Serialize(objCDiscountTypes.lstCVarTaxeTypes)//data[3]
                , new JavaScriptSerializer().Serialize(objCInvoiceTypes.lstCVarInvoiceTypes)//data[4]
                , new JavaScriptSerializer().Serialize(objCNoAccessPartnerTypes.lstCVarNoAccessPartnerTypes)//data[5]
                , serializer.Serialize(objCSalesmen.lstCVarUsers)//data[6]
            };
        }

        [HttpGet, HttpPost]
        public object[] FillPartners(Int32 pPartnerTypeID)
        {
            int _RowCount = 0;
            int constCustomerPartnerTypeID = 1;
            int constAgentPartnerTypeID = 2;
            int constShippingAgentPartnerTypeID = 3;
            int constCustomsClearanceAgentPartnerTypeID = 4;
            int constShippingLinePartnerTypeID = 5;
            int constAirlinePartnerTypeID = 6;
            int constTruckerPartnerTypeID = 7;
            int constSupplierPartnerTypeID = 8;
            int constCustodyPartnerTypeID = 20;
            
            if (pPartnerTypeID == constCustomerPartnerTypeID)
            {
                CvwCustomersWithMinimalColumns objCCustomers = new CvwCustomersWithMinimalColumns();
                objCCustomers.GetListPaging(99999, 1, "WHERE 1=1", "Name", out _RowCount);
                var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
                return new object[] { serializer.Serialize(objCCustomers.lstCVarvwCustomersWithMinimalColumns) };
            }
            else if (pPartnerTypeID == constAgentPartnerTypeID)
            {
                CAgents objCAgents = new CAgents();
                objCAgents.GetListPaging(99999, 1, "WHERE 1=1", "Name", out _RowCount);
                var pAgentsList = objCAgents.lstCVarAgents
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                    ,
                    Code = s.Code
                }).ToList();
                return new object[] { new JavaScriptSerializer().Serialize(pAgentsList) };
            }
            else if (pPartnerTypeID == constShippingAgentPartnerTypeID)
            {

                CShippingAgents objCShippingAgents = new CShippingAgents();
                objCShippingAgents.GetListPaging(10000, 1, "WHERE 1=1", "Name", out _RowCount);
                return new object[] { new JavaScriptSerializer().Serialize(objCShippingAgents.lstCVarShippingAgents) };
            }
            else if (pPartnerTypeID == constCustomsClearanceAgentPartnerTypeID)
            {
                CCustomsClearanceAgents objCCustomsClearanceAgents = new CCustomsClearanceAgents();
                objCCustomsClearanceAgents.GetListPaging(10000, 1, "WHERE 1=1", "Name", out _RowCount);
                return new object[] { new JavaScriptSerializer().Serialize(objCCustomsClearanceAgents.lstCVarCustomsClearanceAgents) };
            }
            else if (pPartnerTypeID == constShippingLinePartnerTypeID)
            {
                CShippingLines objCShippingLines = new CShippingLines();
                objCShippingLines.GetListPaging(10000, 1, "WHERE 1=1", "Name", out _RowCount);
                return new object[] { new JavaScriptSerializer().Serialize(objCShippingLines.lstCVarShippingLines) };
            }
            else if (pPartnerTypeID == constAirlinePartnerTypeID) {
                CvwAirlinesWithMinimalColumns objCAirlines = new CvwAirlinesWithMinimalColumns();
                objCAirlines.GetListPaging(10000, 1, "WHERE 1=1", "Name", out _RowCount);
                    return new object[] { new JavaScriptSerializer().Serialize(objCAirlines.lstCVarvwAirlinesWithMinimalColumns) };
            }
            else if (pPartnerTypeID == constTruckerPartnerTypeID) {
                CTruckers objCTruckers = new CTruckers();
                objCTruckers.GetListPaging(10000, 1, "WHERE 1=1", "Name", out _RowCount);
                return new object[] { new JavaScriptSerializer().Serialize(objCTruckers.lstCVarTruckers) };
            }
            else if (pPartnerTypeID == constSupplierPartnerTypeID) {
                CSuppliers objCSuppliers = new CSuppliers();
                objCSuppliers.GetListPaging(10000, 1, "WHERE 1=1", "Name", out _RowCount);
                return new object[] { new JavaScriptSerializer().Serialize(objCSuppliers.lstCVarSuppliers) };
            }
            else if (pPartnerTypeID == constCustodyPartnerTypeID) {
                CCustody objCCustody = new CCustody();
                objCCustody.GetListPaging(10000, 1, "WHERE 1=1", "Name", out _RowCount);
                return new object[] { new JavaScriptSerializer().Serialize(objCCustody.lstCVarCustody) };
            }
            return new object[] {};
        }

        [HttpGet, HttpPost]
        public object[] LoadData(string pWhereClause, string pWhereClause_AccNote, string pOrderBy, bool pIncludeDetails
            , bool pIsAddNotes, Int32 pCurrencyID, bool pIsAgingPerClient, bool pIsAgingPerClient_AllCurrency)
        {
            bool pRecordsExist = false;
            Exception checkException = null;
            Int32 _RowCount = 0;
            Int32 _RowCount_temp = 0;
            CvwInvoices objCvwInvoices = new CvwInvoices();
            CvwReceivables objCvwReceivables = new CvwReceivables();
            CvwAccNote objCvwAccNote = new CvwAccNote();
            string _InvoiceIDs = "0";
            //checkException = objCvwInvoices.GetListPaging(99999, 1, pWhereClause, "BranchName, InvoiceTypeName, InvoiceNumber", out _RowCount);
            checkException = objCvwAccNote.GetListPaging(99999, 1, pWhereClause_AccNote, "ID", out _RowCount);
            //checkException = objCvwInvoices.GetListPaging(99999, 1, pWhereClause, pOrderBy, out _RowCount);
            checkException = objCvwInvoices.GetList(pWhereClause + " ORDER BY " + pOrderBy);
            if (pIncludeDetails)
                for (int i = 0; i < objCvwInvoices.lstCVarvwInvoices.Count; i++)
                    _InvoiceIDs += "," + objCvwInvoices.lstCVarvwInvoices[i].ID;
            checkException = objCvwReceivables.GetListPaging(9999999,1,"WHERE InvoiceID IN (" + _InvoiceIDs + ")", "InvoiceID", out _RowCount_temp);
            
            #region Merge AccNote to Invoices
            for (int i = 0; i < objCvwAccNote.lstCVarvwAccNote.Count && pIsAddNotes; i++)
            {
                CVarvwInvoices objCVarvwInvoices = new CVarvwInvoices();
                objCVarvwInvoices.ID = objCvwAccNote.lstCVarvwAccNote[i].ID;
                objCVarvwInvoices.InvoiceNumber = objCvwAccNote.lstCVarvwAccNote[i].CodeSerial;
                objCVarvwInvoices.InvoiceTypeID = 0; //objCvwAccNote.lstCVarvwAccNote[i].NoteType;
                objCVarvwInvoices.InvoiceTypeCode = objCvwAccNote.lstCVarvwAccNote[i].NoteTypeName;
                objCVarvwInvoices.InvoiceTypeName = objCvwAccNote.lstCVarvwAccNote[i].NoteTypeName;
                objCVarvwInvoices.ConcatenatedInvoiceNumber = objCvwAccNote.lstCVarvwAccNote[i].Code;
                objCVarvwInvoices.Code = objCvwAccNote.lstCVarvwAccNote[i].Code;
                objCVarvwInvoices.OperationID = objCvwAccNote.lstCVarvwAccNote[i].OperationID;
                objCVarvwInvoices.OperationCode = objCvwAccNote.lstCVarvwAccNote[i].OperationCode;
                objCVarvwInvoices.MasterBL = objCvwAccNote.lstCVarvwAccNote[i].MasterBL;
                objCVarvwInvoices.HouseNumber = objCvwAccNote.lstCVarvwAccNote[i].HouseNumber;
                objCVarvwInvoices.BranchID = objCvwAccNote.lstCVarvwAccNote[i].BranchID;
                objCVarvwInvoices.MasterOperationID = objCvwAccNote.lstCVarvwAccNote[i].MasterOperationID;
                objCVarvwInvoices.MasterOperationCode = objCvwAccNote.lstCVarvwAccNote[i].MasterOperationCode;
                objCVarvwInvoices.OperationPartnerID = objCvwAccNote.lstCVarvwAccNote[i].OperationPartnerID;
                objCVarvwInvoices.PartnerTypeID = objCvwAccNote.lstCVarvwAccNote[i].PartnerTypeID;
                objCVarvwInvoices.PartnerTypeCode = objCvwAccNote.lstCVarvwAccNote[i].PartnerTypeCode;
                objCVarvwInvoices.PartnerID = objCvwAccNote.lstCVarvwAccNote[i].PartnerID;
                objCVarvwInvoices.PartnerName = objCvwAccNote.lstCVarvwAccNote[i].PartnerName;
                objCVarvwInvoices.InvoiceStatus = objCvwAccNote.lstCVarvwAccNote[i].NoteStatus;
                objCVarvwInvoices.AddressID = objCvwAccNote.lstCVarvwAccNote[i].AddressID;
                objCVarvwInvoices.CityID = objCvwAccNote.lstCVarvwAccNote[i].CityID;
                objCVarvwInvoices.CityName = objCvwAccNote.lstCVarvwAccNote[i].CityName;
                objCVarvwInvoices.CountryID = objCvwAccNote.lstCVarvwAccNote[i].CountryID;
                objCVarvwInvoices.CountryName = objCvwAccNote.lstCVarvwAccNote[i].CountryName;
                objCVarvwInvoices.StreetLine1 = objCvwAccNote.lstCVarvwAccNote[i].StreetLine1;
                objCVarvwInvoices.StreetLine2 = objCvwAccNote.lstCVarvwAccNote[i].StreetLine2;
                objCVarvwInvoices.AddressTypeID = objCvwAccNote.lstCVarvwAccNote[i].AddressTypeID;
                objCVarvwInvoices.PrintedAddress = objCvwAccNote.lstCVarvwAccNote[i].PrintedAddress;
                objCVarvwInvoices.CurrencyID = objCvwAccNote.lstCVarvwAccNote[i].CurrencyID;
                objCVarvwInvoices.CurrencyCode = objCvwAccNote.lstCVarvwAccNote[i].CurrencyCode;
                objCVarvwInvoices.MasterDataExchangeRate = objCvwAccNote.lstCVarvwAccNote[i].MasterDataExchangeRate;
                objCVarvwInvoices.ExchangeRate = objCvwAccNote.lstCVarvwAccNote[i].ExchangeRate;
                objCVarvwInvoices.AmountWithoutVAT = objCvwAccNote.lstCVarvwAccNote[i].NoteType == 100 //Credit
                    ? -1 * objCvwAccNote.lstCVarvwAccNote[i].AmountWithoutVAT
                    : objCvwAccNote.lstCVarvwAccNote[i].AmountWithoutVAT;
                objCVarvwInvoices.TaxTypeID = objCvwAccNote.lstCVarvwAccNote[i].TaxTypeID;
                objCVarvwInvoices.TaxTypeName = objCvwAccNote.lstCVarvwAccNote[i].TaxTypeName;
                objCVarvwInvoices.TaxPercentage = objCvwAccNote.lstCVarvwAccNote[i].TaxPercentage;
                objCVarvwInvoices.TaxAmount = objCvwAccNote.lstCVarvwAccNote[i].NoteType == 100 //Credit
                    ? -1 * objCvwAccNote.lstCVarvwAccNote[i].TaxAmount
                    : objCvwAccNote.lstCVarvwAccNote[i].TaxAmount;
                objCVarvwInvoices.DiscountTypeID = objCvwAccNote.lstCVarvwAccNote[i].DiscountTypeID;
                objCVarvwInvoices.DiscountTypeName = objCvwAccNote.lstCVarvwAccNote[i].DiscountTypeName;
                objCVarvwInvoices.DiscountPercentage = objCvwAccNote.lstCVarvwAccNote[i].DiscountPercentage;
                objCVarvwInvoices.DiscountAmount = objCvwAccNote.lstCVarvwAccNote[i].NoteType == 100 //Credit
                    ? -1 * objCvwAccNote.lstCVarvwAccNote[i].DiscountAmount
                    : objCvwAccNote.lstCVarvwAccNote[i].DiscountAmount;
                objCVarvwInvoices.Amount = objCvwAccNote.lstCVarvwAccNote[i].NoteType == 100 //Credit
                    ? -1 * objCvwAccNote.lstCVarvwAccNote[i].Amount
                    : objCvwAccNote.lstCVarvwAccNote[i].Amount;
                objCVarvwInvoices.PaidAmount = objCvwAccNote.lstCVarvwAccNote[i].PaidAmount;
                objCVarvwInvoices.RemainingAmount = objCvwAccNote.lstCVarvwAccNote[i].RemainingAmount;
                objCVarvwInvoices.InvoiceDate = objCvwAccNote.lstCVarvwAccNote[i].NoteDate;
                objCVarvwInvoices.DueDate = objCvwAccNote.lstCVarvwAccNote[i].NoteDate;
                objCVarvwInvoices.IsApproved = objCvwAccNote.lstCVarvwAccNote[i].IsApproved;
                objCVarvwInvoices.IsDeleted = objCvwAccNote.lstCVarvwAccNote[i].IsDeleted;
                objCVarvwInvoices.ApprovingUserID = objCvwAccNote.lstCVarvwAccNote[i].ApprovingUserID;
                objCVarvwInvoices.ApprovingUserName = objCvwAccNote.lstCVarvwAccNote[i].ApprovingUserName;
                objCVarvwInvoices.CreatorUserID = objCvwAccNote.lstCVarvwAccNote[i].CreatorUserID;
                objCVarvwInvoices.CreatorName = objCvwAccNote.lstCVarvwAccNote[i].CreatorName;
                objCVarvwInvoices.CreationDate = objCvwAccNote.lstCVarvwAccNote[i].CreationDate;
                objCVarvwInvoices.ModificatorUserID = objCvwAccNote.lstCVarvwAccNote[i].ModificatorUserID;
                objCVarvwInvoices.ModificatorName = objCvwAccNote.lstCVarvwAccNote[i].ModificatorName;
                objCVarvwInvoices.ModificationDate = objCvwAccNote.lstCVarvwAccNote[i].ModificationDate;

                objCVarvwInvoices.CustomerReference = "";
                objCVarvwInvoices.OperationOpenDate = DateTime.Parse("01/01/1900");
                objCVarvwInvoices.PaymentDate = DateTime.Parse("01/01/1900");
                objCvwInvoices.lstCVarvwInvoices.Add(objCVarvwInvoices);
            }
            #endregion Merge AccNote to Invoices

            #region AgingPerClient
            var pAgingPerClientList_BeforeGrouping = objCvwInvoices.lstCVarvwInvoices
                //.GroupBy(g => new { g.PartnerName, g.CurrencyID })
                .Select(s => new {
                    PartnerName = s.PartnerName
                    ,
                    CurrencyCode = pCurrencyID == 0 && pIsAgingPerClient ? "" : s.CurrencyCode //if no currency selected then convert to default
                    ,
                    ZeroToThirty = (DateTime.Today - s.DueDate).Days >= 0 && (DateTime.Today - s.DueDate).Days < 30
                                    ? pCurrencyID == 0 && pIsAgingPerClient ? s.RemainingAmount * s.ExchangeRate : s.RemainingAmount
                                    : 0
                    ,
                    PaidAmount = pCurrencyID > 0 && pIsAgingPerClient ? s.PaidAmount: s.PaidAmount * s.ExchangeRate
                    ,
                    RemainingAmount = pCurrencyID == 0 && pIsAgingPerClient ? s.RemainingAmount * s.ExchangeRate : s.RemainingAmount
                    ,
                    ThirtyOneToSixty = (DateTime.Today - s.DueDate).Days >= 30 && (DateTime.Today - s.DueDate).Days < 60
                                    ? pCurrencyID == 0 && pIsAgingPerClient ? s.RemainingAmount * s.ExchangeRate : s.RemainingAmount
                                    : 0
                    ,
                    SixtyOneToNinty = (DateTime.Today - s.DueDate).Days >= 60 && (DateTime.Today - s.DueDate).Days < 90
                                    ? pCurrencyID == 0 && pIsAgingPerClient ? s.RemainingAmount * s.ExchangeRate : s.RemainingAmount
                                    : 0
                    ,
                    Later = (DateTime.Today - s.DueDate).Days >= 90
                                    ? pCurrencyID == 0 && pIsAgingPerClient ? s.RemainingAmount * s.ExchangeRate : s.RemainingAmount
                                    : 0
                    ,
                    NotDue = (DateTime.Today - s.DueDate).Days < 0
                                    ? pCurrencyID == 0 && pIsAgingPerClient ? s.RemainingAmount * s.ExchangeRate : s.RemainingAmount
                                    : 0
                }).ToList();
            var pAgingPerClientList = pAgingPerClientList_BeforeGrouping
                .GroupBy(g => new { g.PartnerName, g.CurrencyCode })
                .Select(s => new {
                    PartnerName = s.First().PartnerName
                    ,
                    CurrencyCode = s.First().CurrencyCode
                    ,
                    RemainingAmount = s.Sum(i => i.RemainingAmount)
                    ,
                    PaidAmount = s.Sum(i => i.PaidAmount)
                    ,
                    ZeroToThirty = s.Sum(i => i.ZeroToThirty)
                    ,
                    ThirtyOneToSixty = s.Sum(i => i.ThirtyOneToSixty)
                    ,
                    SixtyOneToNinty = s.Sum(i => i.SixtyOneToNinty)
                    ,
                    Later = s.Sum(i => i.Later)
                    ,
                    NotDue = s.Sum(i => i.NotDue)
                }).ToList();
            var pDistinctClientsList = pAgingPerClientList
                .Select(s => new
                {
                    PartnerName = s.PartnerName
                }).Distinct();
            #endregion AgingPerClient

            if (objCvwInvoices.lstCVarvwInvoices.Count > 0 && checkException == null)
                pRecordsExist = true;
            
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] 
            {
                pRecordsExist
                , serializer.Serialize(objCvwInvoices.lstCVarvwInvoices) //pData[1]
                , serializer.Serialize(objCvwReceivables.lstCVarvwReceivables) //pData[2]
                , serializer.Serialize(objCvwInvoices.lstCVarvwInvoices.DistinctBy(x=> x.PartnerID).ToList()) //pData[3]
                , pIsAgingPerClient || pIsAgingPerClient_AllCurrency ? serializer.Serialize(pAgingPerClientList) : null //pData[4]
                , pIsAgingPerClient_AllCurrency ? serializer.Serialize(pDistinctClientsList) : null //Data[5]
            };
        }

        [HttpGet, HttpPost]
        public object[] LoadData_TaxationOfSales(string pWhereClauseTaxationOfSales, string pWhereClause_AccNote)
        {
            // var par = "0";
            bool pRecordsExist = false;
            Int32 _RowCount = 0;
            int constTransactionCreditNote = 100;

            Exception checkException = null;
            CDefaults objCDefaults = new CDefaults();
            objCDefaults.GetListPaging(1, 1, "", "ID", out _RowCount);
            CvwInvoicesTaxationOfSales objCvwInvoicesTaxationOfSales = new CvwInvoicesTaxationOfSales();
            CvwInvoicesTaxationOfSales_TaxOnItems objCvwInvoicesTaxationOfSales_TaxOnItems = new CvwInvoicesTaxationOfSales_TaxOnItems();
            CvwAccNote objCvwAccNote = new CvwAccNote();

            if (objCDefaults.lstCVarDefaults[0].IsTaxOnItems)
                checkException = objCvwInvoicesTaxationOfSales_TaxOnItems.GetListPaging(99999, 1, pWhereClauseTaxationOfSales, "BranchName, InvoiceNumber", out _RowCount);
            else
                checkException = objCvwInvoicesTaxationOfSales.GetListPaging(99999, 1, pWhereClauseTaxationOfSales, "BranchName, InvoiceNumber", out _RowCount);

            #region TaxOnItems
            var pReportRows_TaxOnItems = objCvwInvoicesTaxationOfSales_TaxOnItems.lstCVarvwInvoicesTaxationOfSales_TaxOnItems
                .GroupBy(g => new
                {
                    g.DocumentTypeNO,
                    g.TaxTypeNO,
                    g.ItemTypeNO,
                    g.InvoiceNumber,
                    g.PartnerName,
                    g.VATNumber,
                    g.TaxIdentificationNumber,
                    g.Address,
                    g.CustomerPhone,
                    g.InvoiceDate,
                    g.InvoiceTypeName,
                    g.StatementTypeNO,
                    g.ExchangeRate,
                    g.TaxPercentage,
                    g.TaxeTypeID,
                    g.HouseNumber,
                    g.CurrencyCode
                })
                .Select(s => new
                {
                    DocumentTypeNO = s.First().DocumentTypeNO
                    ,
                    TaxTypeNO = s.First().TaxTypeNO
                    ,
                    ItemTypeNO = s.First().ItemTypeNO
                    ,
                    InvoiceNumber = s.First().InvoiceNumber
                    ,
                    PartnerName = s.First().PartnerName
                    ,
                    VATNumber = s.First().VATNumber
                    ,
                    TaxIdentificationNumber = s.First().TaxIdentificationNumber
                    ,
                    Address = s.First().Address
                    ,
                    CustomerPhone = s.First().CustomerPhone
                    ,
                    InvoiceDate = s.First().InvoiceDate
                    ,
                    InvoiceTypeName = s.First().InvoiceTypeName
                    ,
                    StatementTypeNO = s.First().StatementTypeNO
                    ,
                    ExchangeRate = s.First().ExchangeRate
                    ,
                    AmountWithoutVAT = s.Sum(i => i.AmountWithoutVAT)
                    ,
                    TaxPercentage = s.First().TaxPercentage
                    ,
                    TaxAmount = s.Sum(i => i.TaxAmount)
                    ,
                    DiscountAmount = s.Sum(i => i.DiscountAmount)
                    ,
                    Amount = s.Sum(i => i.Amount)
                    ,
                    HouseNumber = s.First().HouseNumber
                     ,
                    ReceiptCode = s.First().ReceiptCode
                    ,
                    TotalVoucher = s.Sum(i => i.TotalVoucher)
                     ,
                    ReferenceNo = s.First().ReferenceNo
                     ,
                    Diff = s.Sum(i => i.Diff)
                     ,
                    CurrencyCode = s.First().CurrencyCode
                }).ToList();
            #endregion TaxOnItems
            
            #region Add Credit Notes
            if (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "COS")
            {
                checkException = objCvwAccNote.GetListPaging(99999, 1, pWhereClause_AccNote + " AND NoteType=" + constTransactionCreditNote, "ID", out _RowCount);
                for (int i =0; i <objCvwAccNote.lstCVarvwAccNote.Count; i++)
                {
                    CVarvwInvoicesTaxationOfSales objCVarvwInvoicesTaxationOfSales = new CVarvwInvoicesTaxationOfSales();
                    objCVarvwInvoicesTaxationOfSales.DocumentTypeNO = 1;
                    objCVarvwInvoicesTaxationOfSales.ItemTypeNO = objCvwAccNote.lstCVarvwAccNote[i].TaxPercentage == 14 ? 2 : 1;
                    objCVarvwInvoicesTaxationOfSales.InvoiceNumber = objCvwAccNote.lstCVarvwAccNote[i].CodeSerial;
                    objCVarvwInvoicesTaxationOfSales.PartnerName = objCvwAccNote.lstCVarvwAccNote[i].PartnerName;
                    objCVarvwInvoicesTaxationOfSales.VATNumber = objCvwAccNote.lstCVarvwAccNote[i].VATNumber;
                    objCVarvwInvoicesTaxationOfSales.TaxIdentificationNumber = objCvwAccNote.lstCVarvwAccNote[i].TaxIdentificationNumber;
                    objCVarvwInvoicesTaxationOfSales.Address = objCvwAccNote.lstCVarvwAccNote[i].Address;
                    objCVarvwInvoicesTaxationOfSales.CustomerPhone = objCvwAccNote.lstCVarvwAccNote[i].CustomerPhone;
                    objCVarvwInvoicesTaxationOfSales.InvoiceDate = objCvwAccNote.lstCVarvwAccNote[i].NoteDate;
                    objCVarvwInvoicesTaxationOfSales.InvoiceTypeName = objCvwAccNote.lstCVarvwAccNote[i].NoteTypeName;
                    objCVarvwInvoicesTaxationOfSales.StatementTypeNO = 4;
                    objCVarvwInvoicesTaxationOfSales.ExchangeRate = objCvwAccNote.lstCVarvwAccNote[i].ExchangeRate;

                    objCVarvwInvoicesTaxationOfSales.AmountWithoutVAT = objCvwAccNote.lstCVarvwAccNote[i].AmountWithoutVAT;
                    objCVarvwInvoicesTaxationOfSales.TaxPercentage = objCvwAccNote.lstCVarvwAccNote[i].TaxPercentage;
                    objCVarvwInvoicesTaxationOfSales.TaxAmount = objCvwAccNote.lstCVarvwAccNote[i].TaxAmount;

                    objCVarvwInvoicesTaxationOfSales.DiscountAmount = objCvwAccNote.lstCVarvwAccNote[i].DiscountAmount;
                    objCVarvwInvoicesTaxationOfSales.Amount = objCvwAccNote.lstCVarvwAccNote[i].Amount;
                    objCVarvwInvoicesTaxationOfSales.HouseNumber = objCvwAccNote.lstCVarvwAccNote[i].HouseNumber;
                    objCVarvwInvoicesTaxationOfSales.ReceiptCode = ""; //objCvwAccNote.lstCVarvwAccNote[i].ReceiptCode;
                    objCVarvwInvoicesTaxationOfSales.TotalVoucher = 0; //objCvwAccNote.lstCVarvwAccNote[i].TotalVoucher;
                    objCVarvwInvoicesTaxationOfSales.ReferenceNo = ""; //objCvwAccNote.lstCVarvwAccNote[i].ReferenceNo;
                    objCVarvwInvoicesTaxationOfSales.Diff = 0; //objCvwAccNote.lstCVarvwAccNote[i].Diff;



                    //objCVarvwInvoicesTaxationOfSales.ID = objCvwAccNote.lstCVarvwAccNote[i].ID;
                    //objCVarvwInvoicesTaxationOfSales.InvoiceNumber = objCvwAccNote.lstCVarvwAccNote[i].CodeSerial;
                    //objCVarvwInvoicesTaxationOfSales.InvoiceTypeID = 0; //objCvwAccNote.lstCVarvwAccNote[i].NoteType;
                    //objCVarvwInvoicesTaxationOfSales.InvoiceTypeCode = objCvwAccNote.lstCVarvwAccNote[i].NoteTypeName;
                    //objCVarvwInvoicesTaxationOfSales.InvoiceTypeName = objCvwAccNote.lstCVarvwAccNote[i].NoteTypeName;
                    //objCVarvwInvoicesTaxationOfSales.Code = objCvwAccNote.lstCVarvwAccNote[i].Code;
                    //objCVarvwInvoicesTaxationOfSales.OperationID = objCvwAccNote.lstCVarvwAccNote[i].OperationID;
                    //objCVarvwInvoicesTaxationOfSales.OperationCode = objCvwAccNote.lstCVarvwAccNote[i].OperationCode;
                    //objCVarvwInvoicesTaxationOfSales.MasterBL = objCvwAccNote.lstCVarvwAccNote[i].MasterBL;
                    //objCVarvwInvoicesTaxationOfSales.HouseNumber = objCvwAccNote.lstCVarvwAccNote[i].HouseNumber;
                    //objCVarvwInvoicesTaxationOfSales.BranchID = objCvwAccNote.lstCVarvwAccNote[i].BranchID;
                    //objCVarvwInvoicesTaxationOfSales.MasterOperationID = objCvwAccNote.lstCVarvwAccNote[i].MasterOperationID;
                    //objCVarvwInvoicesTaxationOfSales.MasterOperationCode = objCvwAccNote.lstCVarvwAccNote[i].MasterOperationCode;
                    //objCVarvwInvoicesTaxationOfSales.OperationPartnerID = objCvwAccNote.lstCVarvwAccNote[i].OperationPartnerID;
                    //objCVarvwInvoicesTaxationOfSales.PartnerTypeID = objCvwAccNote.lstCVarvwAccNote[i].PartnerTypeID;
                    //objCVarvwInvoicesTaxationOfSales.PartnerTypeCode = objCvwAccNote.lstCVarvwAccNote[i].PartnerTypeCode;
                    //objCVarvwInvoicesTaxationOfSales.PartnerID = objCvwAccNote.lstCVarvwAccNote[i].PartnerID;
                    //objCVarvwInvoicesTaxationOfSales.PartnerName = objCvwAccNote.lstCVarvwAccNote[i].PartnerName;
                    //objCVarvwInvoicesTaxationOfSales.InvoiceStatus = objCvwAccNote.lstCVarvwAccNote[i].NoteStatus;
                    //objCVarvwInvoicesTaxationOfSales.AddressID = objCvwAccNote.lstCVarvwAccNote[i].AddressID;
                    //objCVarvwInvoicesTaxationOfSales.CityID = objCvwAccNote.lstCVarvwAccNote[i].CityID;
                    //objCVarvwInvoicesTaxationOfSales.CityName = objCvwAccNote.lstCVarvwAccNote[i].CityName;
                    //objCVarvwInvoicesTaxationOfSales.CountryID = objCvwAccNote.lstCVarvwAccNote[i].CountryID;
                    //objCVarvwInvoicesTaxationOfSales.CountryName = objCvwAccNote.lstCVarvwAccNote[i].CountryName;
                    //objCVarvwInvoicesTaxationOfSales.StreetLine1 = objCvwAccNote.lstCVarvwAccNote[i].StreetLine1;
                    //objCVarvwInvoicesTaxationOfSales.StreetLine2 = objCvwAccNote.lstCVarvwAccNote[i].StreetLine2;
                    //objCVarvwInvoicesTaxationOfSales.AddressTypeID = objCvwAccNote.lstCVarvwAccNote[i].AddressTypeID;
                    //objCVarvwInvoicesTaxationOfSales.PrintedAddress = objCvwAccNote.lstCVarvwAccNote[i].PrintedAddress;
                    //objCVarvwInvoicesTaxationOfSales.CurrencyID = objCvwAccNote.lstCVarvwAccNote[i].CurrencyID;
                    objCVarvwInvoicesTaxationOfSales.CurrencyCode = objCvwAccNote.lstCVarvwAccNote[i].CurrencyCode;
                    //objCVarvwInvoicesTaxationOfSales.MasterDataExchangeRate = objCvwAccNote.lstCVarvwAccNote[i].MasterDataExchangeRate;
                    //objCVarvwInvoicesTaxationOfSales.ExchangeRate = objCvwAccNote.lstCVarvwAccNote[i].ExchangeRate;
                    //objCVarvwInvoicesTaxationOfSales.AmountWithoutVAT = objCvwAccNote.lstCVarvwAccNote[i].NoteType == 100 //Credit
                    //    ? -1 * objCvwAccNote.lstCVarvwAccNote[i].AmountWithoutVAT
                    //    : objCvwAccNote.lstCVarvwAccNote[i].AmountWithoutVAT;
                    //objCVarvwInvoicesTaxationOfSales.TaxTypeID = objCvwAccNote.lstCVarvwAccNote[i].TaxTypeID;
                    //objCVarvwInvoicesTaxationOfSales.TaxTypeName = objCvwAccNote.lstCVarvwAccNote[i].TaxTypeName;
                    //objCVarvwInvoicesTaxationOfSales.TaxPercentage = objCvwAccNote.lstCVarvwAccNote[i].TaxPercentage;
                    //objCVarvwInvoicesTaxationOfSales.TaxAmount = objCvwAccNote.lstCVarvwAccNote[i].NoteType == 100 //Credit
                    //    ? -1 * objCvwAccNote.lstCVarvwAccNote[i].TaxAmount
                    //    : objCvwAccNote.lstCVarvwAccNote[i].TaxAmount;
                    //objCVarvwInvoicesTaxationOfSales.DiscountTypeID = objCvwAccNote.lstCVarvwAccNote[i].DiscountTypeID;
                    //objCVarvwInvoicesTaxationOfSales.DiscountTypeName = objCvwAccNote.lstCVarvwAccNote[i].DiscountTypeName;
                    //objCVarvwInvoicesTaxationOfSales.DiscountPercentage = objCvwAccNote.lstCVarvwAccNote[i].DiscountPercentage;
                    //objCVarvwInvoicesTaxationOfSales.DiscountAmount = objCvwAccNote.lstCVarvwAccNote[i].NoteType == 100 //Credit
                    //    ? -1 * objCvwAccNote.lstCVarvwAccNote[i].DiscountAmount
                    //    : objCvwAccNote.lstCVarvwAccNote[i].DiscountAmount;
                    //objCVarvwInvoicesTaxationOfSales.Amount = objCvwAccNote.lstCVarvwAccNote[i].NoteType == 100 //Credit
                    //    ? -1 * objCvwAccNote.lstCVarvwAccNote[i].Amount
                    //    : objCvwAccNote.lstCVarvwAccNote[i].Amount;
                    //objCVarvwInvoicesTaxationOfSales.PaidAmount = objCvwAccNote.lstCVarvwAccNote[i].PaidAmount;
                    //objCVarvwInvoicesTaxationOfSales.RemainingAmount = objCvwAccNote.lstCVarvwAccNote[i].RemainingAmount;
                    //objCVarvwInvoicesTaxationOfSales.InvoiceDate = objCvwAccNote.lstCVarvwAccNote[i].NoteDate;
                    //objCVarvwInvoicesTaxationOfSales.DueDate = objCvwAccNote.lstCVarvwAccNote[i].NoteDate;
                    //objCVarvwInvoicesTaxationOfSales.IsApproved = objCvwAccNote.lstCVarvwAccNote[i].IsApproved;
                    //objCVarvwInvoicesTaxationOfSales.IsDeleted = objCvwAccNote.lstCVarvwAccNote[i].IsDeleted;
                    //objCVarvwInvoicesTaxationOfSales.ApprovingUserID = objCvwAccNote.lstCVarvwAccNote[i].ApprovingUserID;
                    //objCVarvwInvoicesTaxationOfSales.ApprovingUserName = objCvwAccNote.lstCVarvwAccNote[i].ApprovingUserName;
                    //objCVarvwInvoicesTaxationOfSales.CreatorUserID = objCvwAccNote.lstCVarvwAccNote[i].CreatorUserID;
                    //objCVarvwInvoicesTaxationOfSales.CreatorName = objCvwAccNote.lstCVarvwAccNote[i].CreatorName;
                    //objCVarvwInvoicesTaxationOfSales.CreationDate = objCvwAccNote.lstCVarvwAccNote[i].CreationDate;
                    //objCVarvwInvoicesTaxationOfSales.ModificatorUserID = objCvwAccNote.lstCVarvwAccNote[i].ModificatorUserID;
                    //objCVarvwInvoicesTaxationOfSales.ModificatorName = objCvwAccNote.lstCVarvwAccNote[i].ModificatorName;
                    //objCVarvwInvoicesTaxationOfSales.ModificationDate = objCvwAccNote.lstCVarvwAccNote[i].ModificationDate;

                    //objCVarvwInvoicesTaxationOfSales.CustomerReference = "";
                    //objCVarvwInvoicesTaxationOfSales.OperationOpenDate = DateTime.Parse("01/01/1900");
                    //objCVarvwInvoicesTaxationOfSales.PaymentDate = DateTime.Parse("01/01/1900");
                    objCvwInvoicesTaxationOfSales.lstCVarvwInvoicesTaxationOfSales.Add(objCVarvwInvoicesTaxationOfSales);
                }
            }
            #endregion Add Credit Notes
            if (objCvwInvoicesTaxationOfSales.lstCVarvwInvoicesTaxationOfSales.Count > 0 
                || objCvwInvoicesTaxationOfSales_TaxOnItems.lstCVarvwInvoicesTaxationOfSales_TaxOnItems.Count > 0
                || objCvwAccNote.lstCVarvwAccNote.Count > 0)
                pRecordsExist = true;

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] {
                pRecordsExist
                , objCDefaults.lstCVarDefaults[0].IsTaxOnItems ? serializer.Serialize(pReportRows_TaxOnItems) : serializer.Serialize(objCvwInvoicesTaxationOfSales.lstCVarvwInvoicesTaxationOfSales)
            };
        }

    }
}
