using Forwarding.MvcApp.Models.Administration.Security.Generated;
using Forwarding.MvcApp.Models.Administration.Settings.Generated;
using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;
using Forwarding.MvcApp.Models.MasterData.Locations.Generated;
using Forwarding.MvcApp.Models.MasterData.Others.Generated;
using Forwarding.MvcApp.Models.MasterData.Partners.Generated;
using Forwarding.MvcApp.Models.NoAccess.Generated;
using Forwarding.MvcApp.Models.Operations.Operations.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebMatrix.WebData;
using static QRCoder.PayloadGenerator;

namespace Forwarding.MvcApp.Controllers.Reports.API_Statistics
{
    public class ProfitStatisticsController : ApiController
    {

        public class ProfitStatisticsData
        {
            public Int64 OperationID { get; set; }
            public string Code { get; set; }
            public string OpenDate { get; set; }
            public string ClientName { get; set; }
            public string Cargo { get; set; }

            public decimal QuotationCost { get; set; }
            public decimal Payables { get; set; }
            public decimal Invoices { get; set; }
            public string Currency { get; set; }
            public decimal ProfitPerCurrency { get; set; }
            public decimal Profit { get; set; }
            public decimal Margin { get; set; }
        }

        [HttpGet, HttpPost]//pID : is the InvoiceID
        public object[] GetStatisticsFilter()
        {
            int _RowCount = 0;
            CvwUsers objCvwUsers = new CvwUsers();
            objCvwUsers.GetList(" WHERE IsNull(CustomerID , 0) = 0 AND 1=1 ORDER BY Name ");

            CMoveTypes objCMoveTypes = new CMoveTypes();
            objCMoveTypes.GetList(" WHERE 1=1 ORDER BY Name ");

            CChargeTypes objCChargeTypes = new CChargeTypes();
            objCChargeTypes.GetList(" WHERE 1=1 ORDER BY Name ");

            CInvoiceTypes objCInvoiceTypes = new CInvoiceTypes();
            //objCInvoiceTypes.GetList(" WHERE 1=1 ORDER BY Name ");
            objCInvoiceTypes.GetList(" WHERE 1=1 ORDER BY Name ");

            CvwBranches objCvwBranches = new CvwBranches();
            objCvwBranches.GetList(" WHERE 1=1 ORDER BY Name ");

            CCustomers objCCustomers = new CCustomers();
            //objCCustomers.GetList(" WHERE 1=1 ORDER BY Name ");

            CAgents objCAgents = new CAgents();
            objCAgents.GetListPaging(999999, 1, "WHERE 1=1", "Name", out _RowCount);

            CSuppliers objCSuppliers = new CSuppliers();
            objCSuppliers.GetListPaging(999999, 1, "WHERE 1=1", "Name", out _RowCount);

            CvwCurrencies objCvwCurrencies = new CvwCurrencies();
            objCvwCurrencies.GetList(" WHERE 1=1 ORDER BY Code ");

            CCountries objCCountries = new CCountries();
            objCCountries.GetList(" WHERE 1=1 ORDER BY Name ");

            CNoAccessQuoteAndOperStages objCNoAccessQuoteAndOperStages = new CNoAccessQuoteAndOperStages();
            objCNoAccessQuoteAndOperStages.GetList(" WHERE IsOperationStage = 1 AND IsInActive = 0 ORDER BY ViewOrder ");

            CNoAccessPartnerTypes objCNoAccessPartnerTypes = new CNoAccessPartnerTypes();
            objCNoAccessPartnerTypes.GetList(" WHERE ID IN (4,7,8) ");


            CShippingLines objCShippingLines = new CShippingLines();
            objCShippingLines.GetListPaging(99999, 1, "WHERE 1=1", "Name", out _RowCount);



            var constOperationsFormID = 29;//OperationsManagement
            //var constQuotationsFormID = 28;//QuotationsManagement
            CvwUserForms objCvwUserForms = new CvwUserForms();
            objCvwUserForms.GetList("WHERE UserID=" + WebSecurity.CurrentUserId.ToString() + " AND FormID=" + constOperationsFormID.ToString());
            bool _IsHideOthersRecords = objCvwUserForms.lstCVarvwUserForms[0].HideOthersRecords;
            string pWhereClause = "WHERE 1=1";
            if (_IsHideOthersRecords)
                pWhereClause += " AND CreatorUserID=" + WebSecurity.CurrentUserId;
            #region Get Lists with minimal columns
            var pChargeTypeList = objCChargeTypes.lstCVarChargeTypes
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                }).ToList();
            var pAgentList = objCAgents.lstCVarAgents
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                }).ToList();
            var pSupplierList = objCSuppliers.lstCVarSuppliers
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                }).ToList();
            var pCountryList = objCCountries.lstCVarCountries
                //.GroupBy(g => g.ReceiveID)
                .Select(s => new
                {
                    ID = s.ID
                    ,
                    Name = s.Name
                }).ToList();
            #endregion Get Lists with minimal columns
            CvwOperationsForCombo objCOperations = new CvwOperationsForCombo();
            objCOperations.GetList(pWhereClause);
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                new JavaScriptSerializer().Serialize(objCvwUsers.lstCVarvwUsers)//data[0]
                , new JavaScriptSerializer().Serialize(objCvwBranches.lstCVarvwBranches)//data[1]
                , new JavaScriptSerializer().Serialize(objCCustomers.lstCVarCustomers)//data[2]
                , new JavaScriptSerializer().Serialize(objCvwCurrencies.lstCVarvwCurrencies)//data[3]
                , new JavaScriptSerializer().Serialize(objCNoAccessQuoteAndOperStages.lstCVarNoAccessQuoteAndOperStages)//data[4]
                , serializer.Serialize(objCOperations.lstCVarvwOperationsForCombo)//data[5]
                , new JavaScriptSerializer().Serialize(objCMoveTypes.lstCVarMoveTypes)//data[6]
                , new JavaScriptSerializer().Serialize(pChargeTypeList)//data[7]
                , new JavaScriptSerializer().Serialize(objCInvoiceTypes.lstCVarInvoiceTypes)//data[8]
                , new JavaScriptSerializer().Serialize(pAgentList)//data[9]
                , new JavaScriptSerializer().Serialize(pCountryList)//data[10]
                , new JavaScriptSerializer().Serialize(objCNoAccessPartnerTypes.lstCVarNoAccessPartnerTypes)//data[11]
                , new JavaScriptSerializer().Serialize(objCShippingLines.lstCVarShippingLines)//data[12]
            };
        }

        [HttpGet, HttpPost]
        public object[] LoadData(string pWhereClause, int pCurrencyID, int pPartnerTypeID, int pSupplierID, string pSupplierInvoiceNumber
            , bool pIncludeCurrenciesDetails
            , bool pIncludeChargeDetails, bool pGroupByOperations, Int32 pChargeTypeID, bool pWithVAT
            , bool pIsMarginRegardingRevenue, bool pIsOfficial, bool pIsUsedInOperationStatement, Int32 pOption) //pOption==1 ? WithoutInvoiceNos: 2 WithInvoiceNos
        {
            bool pRecordsExist = true;
            string pPayablesCurrenciesSummary = "";
            string pReceivablesOrInvoicesCurrenciesSummary = "";
            string pProfitCurrenciesSummary = "";
            decimal pMarginSummary = 0;
            string pOperationsIDs = "";
            int _RowCount = 0;

            CvwDefaults objCvwDefaults = new CvwDefaults();
            objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa

            var DefaultCurrencyCode = objCvwDefaults.lstCVarvwDefaults[0].CurrencyCode;

            //CvwCurrencies objCvwCurrencies = new CvwCurrencies();
            //objCvwCurrencies.GetList(" WHERE 1=1 ");
            //var ExchangeRate = objCCurrencies.lstCVarvwCurrencies.First(c => c.Code == rowProfit.CurrencyCode).CurrentExchangeRate;

            var pData = new List<ProfitStatisticsData>();

            CvwOperations objCvwOperations = new CvwOperations();
            objCvwOperations.GetList(pWhereClause);
            //CvwInvoices objCvwInvoices = new CvwInvoices();
            //object pFixedDiscount = null;
            if (objCvwOperations.lstCVarvwOperations.Count > 0)
            {
                pRecordsExist = true;
                foreach (var row in objCvwOperations.lstCVarvwOperations)
                    pOperationsIDs += (pOperationsIDs == "" ? row.ID.ToString() : ("," + row.ID.ToString()));
                //objCvwInvoices.GetListPaging(999999, 1, "WHERE OperationID IN(" + pOperationsIDs + ")", "ID", out _RowCount);
                //pFixedDiscount = objCvwInvoices.lstCVarvwInvoices.GroupBy(g => new { g.OperationID, g.CurrencyID})
                //    .Select(s=> new
                //    {
                //        OperationID = (s.First().MasterOperationID == 0 ? s.First().OperationID : s.First().MasterOperationID)
                //        ,
                //        FixedDiscount = s.Sum(d => d.FixedDiscount)
                //        ,
                //        CurrencyCode = s.First().CurrencyCode
                //    }).ToList();
            }
            else
                pRecordsExist = false;


            if (pOption == 1)
            {
                CvwProfitCurrencies objCvwProfitCurrencies_WithoutInvoicesData = new CvwProfitCurrencies();
                var objCvwProfitCurrencies = objCvwProfitCurrencies_WithoutInvoicesData.lstCVarvwProfitCurrencies
                    .Where(s => (pIsUsedInOperationStatement ? (s.IsUsedInOperationStatement == 1) : (1 == 1)))
                    .Where(s => (pIsOfficial && pIncludeChargeDetails ? (s.IsOfficial) : (1 == 1)))
                    ;

                if (pRecordsExist)
                {
                    //todo: the next data might be huge, so use in the whereClause "AND OperationID IN (objCvwOperations)"
                    //objCvwProfitCurrencies_Receivables.GetList("WHERE 1=1 " + (pChargeTypeID == 0 ? "" : (" AND ChargeTypeID=" + pChargeTypeID.ToString())) + " AND (OperationID IN (" + pOperationsIDs + ")) GROUP BY OperationID,PayablesWithVAT,PayablesWithoutVAT,ReceivablesWithVAT,ReceivablesWithoutVAT,IsUsedInOperationStatement,MasterOperationID,ChargeTypeName,CurrencyCode,OperationCode,ClientName,POLCode,PODCode,ContainerTypes,ActualDeparture,OpenDate,BookingPartyName,BranchID,BranchName,ChargeTypeID,MoveTypeID,ExchangeRate,InvoiceNumbers,FirstInvoiceDate,POValue,PODate,ShipperName,ConsigneeName,MoveTypeName,OperationStageName ");
                    objCvwProfitCurrencies_WithoutInvoicesData.GetList("WHERE 1=1 "
                       + (pChargeTypeID == 0 ? "" : (" AND ChargeTypeID=" + pChargeTypeID))
                       + (pCurrencyID == 0 ? "" : (" AND CurrencyID=" + pCurrencyID))
                       + (pSupplierInvoiceNumber == "0" ? "" : (" AND InvoiceNumbers=N'" + pSupplierInvoiceNumber + "' "))
                       + (pSupplierID == 0 ? "" : (" AND SupplierPartnerTypeID=" + pPartnerTypeID + " AND PartnerSupplierID=" + pSupplierID))
                       + " AND (OperationID IN (" + pOperationsIDs + ")) ");
                    //objCvwProfitCurrencies_Receivables.GetList("WHERE 1=1 " + (pChargeTypeID == 0 ? "" : (" AND ChargeTypeID=" + pChargeTypeID.ToString())) + " AND (OperationID IN (" + pOperationsIDs + ") OR MasterOperationID IN (" + pOperationsIDs + ")) ");
                    foreach (var row in objCvwOperations.lstCVarvwOperations)
                    {
                        pData.Add(new ProfitStatisticsData
                        {
                            OperationID = row.ID
                            ,
                            Code = row.Code + (objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName == "NEW" && row.VesselName != "0" ? (" / " + row.VesselName) : "")
                            ,
                            ClientName = row.ClientName
                            ,
                            OpenDate = Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(row.OpenDate)
                            ,
                            Cargo = (row.ShipmentTypeCode == "FCL" || row.ShipmentTypeCode == "FTL" ? row.ContainerTypes : row.PackageTypes)
                        });

                        //select only statement rows of current operation row
                        var OperationProfitRows = objCvwProfitCurrencies.Where(
                            n => n.OperationID == row.ID || n.MasterOperationID == row.ID);

                        //sum pay., rec., inv., profit Grouped by Currency for the current operation row
                        var groupedOperationCurrenciesDetails = OperationProfitRows.GroupBy(d => d.CurrencyCode)
                            .Select(g => new
                            {
                                QuotationCost = g.Sum(s => s.QuotationCost),
                                Payables = g.Sum(s => (pWithVAT ? s.PayablesWithVAT : s.PayablesWithoutVAT)),
                                Receivables = g.Sum(s => (pWithVAT ? s.ReceivablesWithVAT : s.ReceivablesWithoutVAT)),
                                CurrencyCode = g.First().CurrencyCode,
                                ProfitPerCurrency = g.Sum(s => pWithVAT
                                                            ? (s.ReceivablesWithVAT - s.PayablesWithVAT)
                                                            : (s.ReceivablesWithoutVAT - s.PayablesWithoutVAT)),
                                Profit = g.Sum(s => (pWithVAT
                                                    ? (s.ReceivablesWithVAT - s.PayablesWithVAT) * s.ExchangeRate
                                                    : (s.ReceivablesWithoutVAT - s.PayablesWithoutVAT)) * s.ExchangeRate)
                            });

                        //Total sum in one row for each operation in Default Currency //i am sure isa 1 row/oper
                        var groupedOperationInDefaultCurrency = OperationProfitRows.GroupBy(i => 1)
                            .Select(g => new
                            {
                                QuotationCost = g.Sum(s => s.QuotationCost * (objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName.Equals("BED") ? s.ExchangeRate : 1))//ahmed2023-01-01
                                ,
                                Payables = g.Sum(s => pWithVAT ? s.PayablesWithVAT * s.ExchangeRate : s.PayablesWithoutVAT * s.ExchangeRate)
                                ,
                                Receivables = g.Sum(s => pWithVAT ? s.ReceivablesWithVAT * s.ExchangeRate : s.ReceivablesWithoutVAT * s.ExchangeRate)
                                ,
                                CurrencyCode = DefaultCurrencyCode
                                ,
                                Profit = g.Sum(s => ((pWithVAT ? s.ReceivablesWithVAT : s.ReceivablesWithoutVAT) * s.ExchangeRate - (pWithVAT ? s.PayablesWithVAT : s.PayablesWithoutVAT)) * s.ExchangeRate)
                                ,
                                ProfitPerCurrency = g.Sum(s => ((pWithVAT ? s.ReceivablesWithVAT : s.ReceivablesWithoutVAT) - (pWithVAT ? s.PayablesWithVAT : s.PayablesWithoutVAT)) * s.ExchangeRate)
                            });

                        //add rows with currencies detailed
                        if (pIncludeCurrenciesDetails)
                        {
                            foreach (var rowProfit in groupedOperationCurrenciesDetails)
                            {
                                pData.Add(new ProfitStatisticsData
                                {
                                    QuotationCost = rowProfit.QuotationCost
                                    ,
                                    Payables = rowProfit.Payables
                                    ,
                                    Invoices = rowProfit.Receivables
                                    ,
                                    Currency = rowProfit.CurrencyCode
                                    ,
                                    ProfitPerCurrency = rowProfit.ProfitPerCurrency
                                    //, Profit = rowProfit.ProfitPerCurrency * objCvwCurrencies.lstCVarvwCurrencies.First(c => c.Code == rowProfit.CurrencyCode).CurrentExchangeRate
                                    ,
                                    Profit = rowProfit.Profit
                                    ,
                                    Margin = pIsMarginRegardingRevenue
                                            ? (rowProfit.Receivables > 0
                                                  ? Math.Round((rowProfit.ProfitPerCurrency / rowProfit.Receivables) * 100, 2)
                                                  : 100
                                                  )
                                            : (rowProfit.Payables > 0
                                                  ? Math.Round((rowProfit.ProfitPerCurrency / rowProfit.Payables) * 100, 2)
                                                  : 100
                                                  )
                                });
                            }
                            //Add the total row for each operation
                            foreach (var rowProfit in groupedOperationInDefaultCurrency)
                            {
                                pData.Add(new ProfitStatisticsData
                                {
                                    Cargo = "Totals In Default Currency:" //I set the  Cargo column to "Total" to be shown in the total profit line(which is in default currency)
                                    ,
                                    QuotationCost = rowProfit.QuotationCost
                                    ,
                                    Payables = rowProfit.Payables
                                    ,
                                    Invoices = rowProfit.Receivables
                                    ,
                                    Currency = rowProfit.CurrencyCode
                                    ,
                                    ProfitPerCurrency = rowProfit.ProfitPerCurrency
                                    //, Profit = rowProfit.ProfitPerCurrency * objCvwCurrencies.lstCVarvwCurrencies.First(c => c.Code == rowProfit.CurrencyCode).CurrentExchangeRate
                                    ,
                                    Profit = rowProfit.ProfitPerCurrency
                                    ,
                                    Margin = pIsMarginRegardingRevenue
                                        ? (rowProfit.Receivables > 0
                                          ? Math.Round((rowProfit.ProfitPerCurrency / rowProfit.Receivables) * 100, 2)
                                          : 100
                                          )
                                        : (rowProfit.Payables > 0
                                          ? Math.Round((rowProfit.ProfitPerCurrency / rowProfit.Payables) * 100, 2)
                                          : 100
                                          )
                                });
                            }
                        } //of if (pIncludeCurrenciesDetails)
                        else //Currencies Details is not included so added the total to the operation row
                            foreach (var rowProfit in groupedOperationInDefaultCurrency)
                                foreach (var item in pData.Where(w => w.OperationID == row.ID))
                                {
                                    item.QuotationCost = rowProfit.QuotationCost;
                                    item.Payables = rowProfit.Payables;
                                    item.Invoices = rowProfit.Receivables;
                                    item.Currency = rowProfit.CurrencyCode;
                                    item.ProfitPerCurrency = rowProfit.ProfitPerCurrency;
                                    //item.Profit = rowProfit.ProfitPerCurrency * objCvwCurrencies.lstCVarvwCurrencies.First(c => c.Code == rowProfit.CurrencyCode).CurrentExchangeRate
                                    item.Profit = rowProfit.ProfitPerCurrency;
                                    item.Margin = pIsMarginRegardingRevenue
                                        ? (rowProfit.Receivables > 0
                                            ? Math.Round((rowProfit.ProfitPerCurrency / rowProfit.Receivables) * 100, 2)
                                            : 100
                                            )
                                        : (rowProfit.Payables > 0
                                            ? Math.Round((rowProfit.ProfitPerCurrency / rowProfit.Payables) * 100, 2)
                                            : 100
                                            );
                                }
                    }
                    #region Get Summary
                    //Summary
                    var PayablesCurrenciesSummary = objCvwProfitCurrencies.GroupBy(d => d.CurrencyCode)
                        .Select(g => new
                        {
                            Payables = g.Sum(s => pWithVAT ? s.PayablesWithVAT : s.PayablesWithoutVAT)
                            ,
                            CurrencyCode = g.First().CurrencyCode
                        });
                    foreach (var row in PayablesCurrenciesSummary)
                    {
                        pPayablesCurrenciesSummary +=
                            (pPayablesCurrenciesSummary == ""
                            ? (Math.Round(row.Payables, 2) + " " + row.CurrencyCode)
                            : (", " + Math.Round(row.Payables, 2) + " " + row.CurrencyCode));
                    }
                    //get total payables in default currrency
                    decimal tempPayablesTotal = 0;
                    tempPayablesTotal = objCvwProfitCurrencies.Sum(s => pWithVAT ? s.PayablesWithVAT * s.ExchangeRate : s.PayablesWithoutVAT * s.ExchangeRate);
                    decimal tempReceivablesTotal = 0;
                    tempReceivablesTotal = objCvwProfitCurrencies.Sum(s => pWithVAT ? s.ReceivablesWithVAT * s.ExchangeRate : s.ReceivablesWithoutVAT * s.ExchangeRate);
                    pPayablesCurrenciesSummary += " = " + Math.Round(tempPayablesTotal, 2).ToString() + " " + DefaultCurrencyCode;

                    //Receivables Summary
                    var ReceivablesCurrenciesSummary = objCvwProfitCurrencies.GroupBy(d => d.CurrencyCode)
                        .Select(g => new
                        {
                            Receivables = g.Sum(s => pWithVAT ? s.ReceivablesWithVAT : s.ReceivablesWithoutVAT),
                            CurrencyCode = g.First().CurrencyCode,
                        });
                    foreach (var row in ReceivablesCurrenciesSummary)
                    {
                        pReceivablesOrInvoicesCurrenciesSummary +=
                            (pReceivablesOrInvoicesCurrenciesSummary == ""
                            ? (Math.Round(row.Receivables, 2) + " " + row.CurrencyCode)
                            : (", " + Math.Round(row.Receivables, 2) + " " + row.CurrencyCode));
                    }
                    //get total Receivables in default currrency
                    pReceivablesOrInvoicesCurrenciesSummary += " = " + Math.Round(objCvwProfitCurrencies.Sum(s => pWithVAT ? s.ReceivablesWithVAT * s.ExchangeRate : s.ReceivablesWithoutVAT * s.ExchangeRate), 2).ToString() + " " + DefaultCurrencyCode;

                    //Profit Summary
                    var ProfitCurrenciesSummary = objCvwProfitCurrencies.GroupBy(d => d.CurrencyCode)
                        .Select(g => new
                        {
                            Profit = g.Sum(s => pWithVAT
                                ? (s.ReceivablesWithVAT - s.PayablesWithVAT)
                                : (s.ReceivablesWithoutVAT - s.PayablesWithoutVAT)),
                            CurrencyCode = g.First().CurrencyCode,
                        });
                    foreach (var row in ProfitCurrenciesSummary)
                    {
                        pProfitCurrenciesSummary +=
                            (pProfitCurrenciesSummary == ""
                            ? (Math.Round(row.Profit, 2) + " " + row.CurrencyCode)
                            : (", " + Math.Round(row.Profit, 2) + " " + row.CurrencyCode));
                    }
                    //get total Profit in default currrency
                    decimal tempProfitTotal = 0;
                    tempProfitTotal = objCvwProfitCurrencies.Sum(s => pWithVAT ? (s.ReceivablesWithVAT - s.PayablesWithVAT) * s.ExchangeRate : (s.ReceivablesWithoutVAT - s.PayablesWithoutVAT) * s.ExchangeRate);
                    pProfitCurrenciesSummary += " = " + Math.Round(tempProfitTotal, 2).ToString() + " " + DefaultCurrencyCode;

                    //Margin Summary
                    pMarginSummary = pIsMarginRegardingRevenue
                                    ? (tempReceivablesTotal == 0 ? 100 : tempProfitTotal / tempReceivablesTotal * 100)
                                    : (tempPayablesTotal == 0 ? 100 : tempProfitTotal / tempPayablesTotal * 100);
                    #endregion Get Summary
                } //of if (pRecordsExist)
                decimal zero = 0;
                string _MessageReturned = "";

                try
                {
                    var groupedProfitCurrencies = pGroupByOperations
                    ? objCvwProfitCurrencies
                    .GroupBy(g => new { g.OperationID, g.OperationCode, g.OpenDate, g.BookingPartyName, g.ClientName, g.POLCode, g.PODCode, g.ActualDeparture, g.ContainerTypes, g.ChargeTypeName, g.CurrencyCode/*, g.PayableDate,g.InvoiceDate*/ })
                    .Select(g => new
                    {
                        OperationID = g.First().OperationID
                        ,
                        OperationCode = g.First().OperationCode
                        ,
                        OpenDate = g.First().OpenDate
                        ,
                        PayableDate = g.First().PayableDate
                        ,
                        InvoiceDate = g.Where(r => r.InvoiceDate.Year != 1900).FirstOrDefault() == null ? DateTime.Parse("01/01/1900") : g.Where(r => r.InvoiceDate.Year != 1900).FirstOrDefault().InvoiceDate
                        ,
                        BookingPartyName = "" //g.First().BookingPartyName
                        ,
                        ReleaseNumber = "" //g.First().ReleaseNumber
                        ,
                        ChargeableWeightSum = zero//g.First().ChargeableWeightSum
                        ,
                        ClientName = g.First().ClientName
                        ,
                        POLCode = g.First().POLCode
                        ,
                        PODCode = g.First().PODCode
                        ,
                        ActualDeparture = g.First().ActualDeparture
                        ,
                        ContainerTypes = g.First().ContainerTypes
                        ,
                        ChargeTypeName = g.First().ChargeTypeName
                        ,
                        PaymentDate = DateTime.Parse("01/01/1900") //g.First().PaymentDate
                        ,
                        QuotationCost = objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName.Equals("BED") ? g.Sum(s => s.QuotationCost) : zero //g.Sum(s => s.QuotationCost)
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
                        ExchangeRate = g.First().ExchangeRate
                        ,
                        InvExchangeRate = g.Where(r => r.InvoiceDate.Year != 1900).FirstOrDefault() == null ? 0 : g.Where(r => r.InvoiceDate.Year != 1900).FirstOrDefault().ExchangeRate
                        ,
                        InvoiceNumbers = "", //g.First().InvoiceNumbers,
                        FirstInvoiceDate = 0, //g.First().FirstInvoiceDate,
                        POValue = "", //g.First().POValue,
                        PODate = DateTime.Parse("01/01/1900"), //g.First().PODate,
                        ShipperName = g.First().ShipperName,
                        ConsigneeName = g.First().ConsigneeName,
                        MoveTypeName = g.First().MoveTypeName,
                        OperationStageName = g.First().OperationStageName,
                        CostAmount = g.First().CostAmount,
                        RemainingAmount = g.First().RemainingAmount,
                        PaidAmount = g.First().PaidAmount,
                        PayableStatus = "",
                        BillTo = 0,
                        BillToName = "", //g.First().BillToName,
                        PartnerSupplierID = 0, //g.First().PartnerSupplierID,
                        PartnerSupplierName = "" //g.First().PartnerSupplierName
                    }).OrderBy(o => o.OperationID)

                    :

                    objCvwProfitCurrencies
                    .GroupBy(g => new { g.OperationID, g.OperationCode, g.OpenDate, g.BookingPartyName, g.ClientName, g.POLCode, g.PODCode, g.ActualDeparture, g.ContainerTypes, g.ChargeTypeName, g.PaymentDate, g.CurrencyCode, g.PayableDate/*,g.InvoiceDate*/ })
                    .Select(g => new
                    {
                        OperationID = g.First().OperationID
                        ,
                        OperationCode = g.First().OperationCode
                        ,
                        OpenDate = g.First().OpenDate
                        ,
                        PayableDate = g.First().PayableDate
                        ,
                        InvoiceDate = g.Where(r => r.InvoiceDate.Year != 1900).FirstOrDefault() == null ? DateTime.Parse("01/01/1900") : g.Where(r => r.InvoiceDate.Year != 1900).FirstOrDefault().InvoiceDate
                        ,
                        BookingPartyName = g.First().BookingPartyName
                        ,
                        ReleaseNumber = g.First().ReleaseNumber
                        ,
                        ChargeableWeightSum = g.First().ChargeableWeightSum
                        ,
                        ClientName = g.First().ClientName
                        ,
                        POLCode = g.First().POLCode
                        ,
                        PODCode = g.First().PODCode
                        ,
                        ActualDeparture = g.First().ActualDeparture
                        ,
                        ContainerTypes = g.First().ContainerTypes
                        ,
                        ChargeTypeName = g.First().ChargeTypeName
                        ,
                        PaymentDate = g.First().PaymentDate
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
                        ExchangeRate = g.First().ExchangeRate
                        ,
                        InvExchangeRate = g.Where(r => r.InvoiceDate.Year != 1900).FirstOrDefault() == null ? 0 : g.Where(r => r.InvoiceDate.Year != 1900).FirstOrDefault().ExchangeRate
                        ,
                        InvoiceNumbers = g.First().InvoiceNumbers,
                        FirstInvoiceDate = g.First().FirstInvoiceDate,
                        POValue = g.First().POValue,
                        PODate = g.First().PODate,
                        ShipperName = g.First().ShipperName,
                        ConsigneeName = g.First().ConsigneeName,
                        MoveTypeName = g.First().MoveTypeName,
                        OperationStageName = g.First().OperationStageName,
                        CostAmount = g.First().CostAmount,
                        RemainingAmount = g.First().RemainingAmount,
                        PaidAmount = g.First().PaidAmount,
                        PayableStatus = g.First().PayableStatus,
                        BillTo = g.First().BillTo,
                        BillToName = g.First().BillToName,
                        PartnerSupplierID = g.First().PartnerSupplierID,
                        PartnerSupplierName = g.First().PartnerSupplierName
                    }).OrderBy(o => o.OperationID);
                    var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
                    return new object[] {
                    pRecordsExist
                    //, pIncludeChargeDetails || pGroupByOperations ? serializer.Serialize(objCvwProfitCurrencies.lstCVarvwProfitCurrencies) : serializer.Serialize(pData)
                    , pIncludeChargeDetails || pGroupByOperations ? serializer.Serialize(groupedProfitCurrencies) : serializer.Serialize(pData)
                    , pPayablesCurrenciesSummary
                    , pReceivablesOrInvoicesCurrenciesSummary
                    , pProfitCurrenciesSummary
                    , Math.Round(pMarginSummary, 2)
                    , pOperationsIDs
                };

                }
                catch (Exception ex)
                {
                    _MessageReturned = ex.Message;
                }

                return new object[] { _MessageReturned };

            }
            else //option:2
            {
                CvwProfitCurrenciesWithInvoiceNosAndDate objCvwProfitCurrencies_WithInvoiceNosAndDate = new CvwProfitCurrenciesWithInvoiceNosAndDate();
                var objCvwProfitCurrencies = objCvwProfitCurrencies_WithInvoiceNosAndDate.lstCVarvwProfitCurrenciesWithInvoiceNosAndDate
                    .Where(s => (pIsUsedInOperationStatement ? (s.IsUsedInOperationStatement == 1) : (1 == 1)))
                    .Where(s => (pIsOfficial && pIncludeChargeDetails ? (s.IsOfficial) : (1 == 1)))
                    ;

                if (pRecordsExist)
                {
                    //todo: the next data might be huge, so use in the whereClause "AND OperationID IN (objCvwOperations)"
                    //objCvwProfitCurrencies_Receivables.GetList("WHERE 1=1 " + (pChargeTypeID == 0 ? "" : (" AND ChargeTypeID=" + pChargeTypeID.ToString())) + " AND (OperationID IN (" + pOperationsIDs + ")) GROUP BY OperationID,PayablesWithVAT,PayablesWithoutVAT,ReceivablesWithVAT,ReceivablesWithoutVAT,IsUsedInOperationStatement,MasterOperationID,ChargeTypeName,CurrencyCode,OperationCode,ClientName,POLCode,PODCode,ContainerTypes,ActualDeparture,OpenDate,BookingPartyName,BranchID,BranchName,ChargeTypeID,MoveTypeID,ExchangeRate,InvoiceNumbers,FirstInvoiceDate,POValue,PODate,ShipperName,ConsigneeName,MoveTypeName,OperationStageName ");
                    objCvwProfitCurrencies_WithInvoiceNosAndDate.GetList("WHERE 1=1 " + (pChargeTypeID == 0 ? "" : (" AND ChargeTypeID=" + pChargeTypeID.ToString())) + " AND (OperationID IN (" + pOperationsIDs + ")) ");
                    //objCvwProfitCurrencies_Receivables.GetList("WHERE 1=1 " + (pChargeTypeID == 0 ? "" : (" AND ChargeTypeID=" + pChargeTypeID.ToString())) + " AND (OperationID IN (" + pOperationsIDs + ") OR MasterOperationID IN (" + pOperationsIDs + ")) ");
                    foreach (var row in objCvwOperations.lstCVarvwOperations)
                    {
                        pData.Add(new ProfitStatisticsData
                        {
                            OperationID = row.ID
                            ,
                            Code = row.Code
                            ,
                            ClientName = row.ClientName
                            ,
                            OpenDate = Forwarding.MvcApp.Helpers.DateFunctionsController.GetddMMyyyyWithSlashesFormat(row.OpenDate)
                            ,
                            Cargo = (row.ShipmentTypeCode == "FCL" || row.ShipmentTypeCode == "FTL" ? row.ContainerTypes : row.PackageTypes)
                        });

                        //select only statement rows of current operation row
                        var OperationProfitRows = objCvwProfitCurrencies.Where(
                            n => n.OperationID == row.ID || n.MasterOperationID == row.ID);

                        //sum pay., rec., inv., profit Grouped by Currency for the current operation row
                        var groupedOperationCurrenciesDetails = OperationProfitRows.GroupBy(d => d.CurrencyCode)
                            .Select(g => new
                            {
                                QuotationCost = g.Sum(s => s.QuotationCost),
                                Payables = g.Sum(s => (pWithVAT ? s.PayablesWithVAT : s.PayablesWithoutVAT)),
                                Receivables = g.Sum(s => (pWithVAT ? s.ReceivablesWithVAT : s.ReceivablesWithoutVAT)),
                                CurrencyCode = g.First().CurrencyCode,
                                ProfitPerCurrency = g.Sum(s => pWithVAT
                                                            ? (s.ReceivablesWithVAT - s.PayablesWithVAT)
                                                            : (s.ReceivablesWithoutVAT - s.PayablesWithoutVAT)),
                                Profit = g.Sum(s => (pWithVAT
                                                    ? (s.ReceivablesWithVAT - s.PayablesWithVAT)
                                                    : (s.ReceivablesWithoutVAT - s.PayablesWithoutVAT)) * s.ExchangeRate)
                            });

                        //Total sum in one row for each operation in Default Currency //i am sure isa 1 row/oper
                        var groupedOperationInDefaultCurrency = OperationProfitRows.GroupBy(i => 1)
                            .Select(g => new
                            {
                                QuotationCost = g.Sum(s => s.QuotationCost * (objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName.Equals("BED") ? s.ExchangeRate : 1))//ahmed2023-01-01
                                ,
                                Payables = g.Sum(s => pWithVAT ? s.PayablesWithVAT * s.ExchangeRate : s.PayablesWithoutVAT * s.ExchangeRate)
                                ,
                                Receivables = g.Sum(s => pWithVAT ? s.ReceivablesWithVAT * s.ExchangeRate : s.ReceivablesWithoutVAT * s.ExchangeRate)
                                ,
                                CurrencyCode = DefaultCurrencyCode
                                ,
                                Profit = g.Sum(s => ((pWithVAT ? s.ReceivablesWithVAT : s.ReceivablesWithoutVAT) - (pWithVAT ? s.PayablesWithVAT : s.PayablesWithoutVAT)) * s.ExchangeRate)
                                ,
                                ProfitPerCurrency = g.Sum(s => ((pWithVAT ? s.ReceivablesWithVAT : s.ReceivablesWithoutVAT) - (pWithVAT ? s.PayablesWithVAT : s.PayablesWithoutVAT)) * s.ExchangeRate)
                            });

                        //add rows with currencies detailed
                        if (pIncludeCurrenciesDetails)
                        {
                            foreach (var rowProfit in groupedOperationCurrenciesDetails)
                            {
                                pData.Add(new ProfitStatisticsData
                                {
                                    QuotationCost = rowProfit.QuotationCost
                                    ,
                                    Payables = rowProfit.Payables
                                    ,
                                    Invoices = rowProfit.Receivables
                                    ,
                                    Currency = rowProfit.CurrencyCode
                                    ,
                                    ProfitPerCurrency = rowProfit.ProfitPerCurrency
                                    //, Profit = rowProfit.ProfitPerCurrency * objCvwCurrencies.lstCVarvwCurrencies.First(c => c.Code == rowProfit.CurrencyCode).CurrentExchangeRate
                                    ,
                                    Profit = rowProfit.Profit
                                    ,
                                    Margin = pIsMarginRegardingRevenue
                                    ? (rowProfit.Receivables > 0
                                      ? Math.Round((rowProfit.ProfitPerCurrency / rowProfit.Receivables) * 100, 2)
                                      : 100
                                      )
                                    : (rowProfit.Payables > 0
                                      ? Math.Round((rowProfit.ProfitPerCurrency / rowProfit.Payables) * 100, 2)
                                      : 100
                                      )
                                });
                            }
                            foreach (var rowProfit in groupedOperationInDefaultCurrency)
                            {
                                pData.Add(new ProfitStatisticsData
                                {
                                    Cargo = "Totals In Default Currency:" //I set the  Cargo column to "Total" to be shown in the total profit line(which is in default currency)
                                    ,
                                    QuotationCost = rowProfit.QuotationCost
                                    ,
                                    Payables = rowProfit.Payables
                                    ,
                                    Invoices = rowProfit.Receivables
                                    ,
                                    Currency = rowProfit.CurrencyCode
                                    ,
                                    ProfitPerCurrency = rowProfit.ProfitPerCurrency
                                    //, Profit = rowProfit.ProfitPerCurrency * objCvwCurrencies.lstCVarvwCurrencies.First(c => c.Code == rowProfit.CurrencyCode).CurrentExchangeRate
                                    ,
                                    Profit = rowProfit.ProfitPerCurrency
                                    ,
                                    Margin = pIsMarginRegardingRevenue
                                        ? (rowProfit.Receivables > 0
                                          ? Math.Round((rowProfit.ProfitPerCurrency / rowProfit.Receivables) * 100, 2)
                                          : 100
                                          )
                                        : (rowProfit.Payables > 0
                                          ? Math.Round((rowProfit.ProfitPerCurrency / rowProfit.Payables) * 100, 2)
                                          : 100
                                          )
                                });
                            }
                        } //of if (pIncludeCurrenciesDetails)
                        else //Currencies Details is not included so added the total to the operation row
                            foreach (var rowProfit in groupedOperationInDefaultCurrency)
                                foreach (var item in pData.Where(w => w.OperationID == row.ID))
                                {
                                    item.QuotationCost = rowProfit.QuotationCost;
                                    item.Payables = rowProfit.Payables;
                                    item.Invoices = rowProfit.Receivables;
                                    item.Currency = rowProfit.CurrencyCode;
                                    item.ProfitPerCurrency = rowProfit.ProfitPerCurrency;
                                    //item.Profit = rowProfit.ProfitPerCurrency * objCvwCurrencies.lstCVarvwCurrencies.First(c => c.Code == rowProfit.CurrencyCode).CurrentExchangeRate
                                    item.Profit = rowProfit.ProfitPerCurrency;
                                    item.Margin = pIsMarginRegardingRevenue
                                        ? (rowProfit.Receivables > 0
                                            ? Math.Round((rowProfit.ProfitPerCurrency / rowProfit.Receivables) * 100, 2)
                                            : 100
                                            )
                                        : (rowProfit.Payables > 0
                                            ? Math.Round((rowProfit.ProfitPerCurrency / rowProfit.Payables) * 100, 2)
                                            : 100
                                            );
                                }
                    }
                    #region Get Summary
                    //Payables Summary
                    var PayablesCurrenciesSummary = objCvwProfitCurrencies.GroupBy(d => d.CurrencyCode)
                        .Select(g => new
                        {
                            Payables = g.Sum(s => pWithVAT ? s.PayablesWithVAT : s.PayablesWithoutVAT)
                            ,
                            CurrencyCode = g.First().CurrencyCode
                        });
                    foreach (var row in PayablesCurrenciesSummary)
                    {
                        pPayablesCurrenciesSummary +=
                            (pPayablesCurrenciesSummary == ""
                            ? (Math.Round(row.Payables, 2) + " " + row.CurrencyCode)
                            : (", " + Math.Round(row.Payables, 2) + " " + row.CurrencyCode));
                    }
                    //get total payables in default currrency
                    decimal tempPayablesTotal = 0;
                    tempPayablesTotal = objCvwProfitCurrencies.Sum(s => pWithVAT ? s.PayablesWithVAT * s.ExchangeRate : s.PayablesWithoutVAT * s.ExchangeRate);
                    decimal tempReceivablesTotal = 0;
                    tempReceivablesTotal = objCvwProfitCurrencies.Sum(s => pWithVAT ? s.ReceivablesWithVAT * s.ExchangeRate : s.ReceivablesWithoutVAT * s.ExchangeRate);
                    pPayablesCurrenciesSummary += " = " + Math.Round(tempPayablesTotal, 2).ToString() + " " + DefaultCurrencyCode;

                    //Receivables Summary
                    var ReceivablesCurrenciesSummary = objCvwProfitCurrencies.GroupBy(d => d.CurrencyCode)
                        .Select(g => new
                        {
                            Receivables = g.Sum(s => pWithVAT ? s.ReceivablesWithVAT : s.ReceivablesWithoutVAT),
                            CurrencyCode = g.First().CurrencyCode,
                        });
                    foreach (var row in ReceivablesCurrenciesSummary)
                    {
                        pReceivablesOrInvoicesCurrenciesSummary +=
                            (pReceivablesOrInvoicesCurrenciesSummary == ""
                            ? (Math.Round(row.Receivables, 2) + " " + row.CurrencyCode)
                            : (", " + Math.Round(row.Receivables, 2) + " " + row.CurrencyCode));
                    }
                    //get total Receivables in default currrency
                    pReceivablesOrInvoicesCurrenciesSummary += " = " + Math.Round(objCvwProfitCurrencies.Sum(s => pWithVAT ? s.ReceivablesWithVAT * s.ExchangeRate : s.ReceivablesWithoutVAT * s.ExchangeRate), 2).ToString() + " " + DefaultCurrencyCode;

                    //Profit Summary
                    var ProfitCurrenciesSummary = objCvwProfitCurrencies.GroupBy(d => d.CurrencyCode)
                        .Select(g => new
                        {
                            Profit = g.Sum(s => pWithVAT
                                ? (s.ReceivablesWithVAT - s.PayablesWithVAT)
                                : (s.ReceivablesWithoutVAT - s.PayablesWithoutVAT)),
                            CurrencyCode = g.First().CurrencyCode,
                        });
                    foreach (var row in ProfitCurrenciesSummary)
                    {
                        pProfitCurrenciesSummary +=
                            (pProfitCurrenciesSummary == ""
                            ? (Math.Round(row.Profit, 2) + " " + row.CurrencyCode)
                            : (", " + Math.Round(row.Profit, 2) + " " + row.CurrencyCode));
                    }
                    //get total Profit in default currrency
                    decimal tempProfitTotal = 0;
                    tempProfitTotal = objCvwProfitCurrencies.Sum(s => pWithVAT ? (s.ReceivablesWithVAT - s.PayablesWithVAT) * s.ExchangeRate : (s.ReceivablesWithoutVAT - s.PayablesWithoutVAT) * s.ExchangeRate);
                    pProfitCurrenciesSummary += " = " + Math.Round(tempProfitTotal, 2).ToString() + " " + DefaultCurrencyCode;

                    //Margin Summary
                    pMarginSummary = pIsMarginRegardingRevenue
                                    ? (tempReceivablesTotal == 0 ? 100 : tempProfitTotal / tempReceivablesTotal * 100)
                                    : (tempPayablesTotal == 0 ? 100 : tempProfitTotal / tempPayablesTotal * 100);
                    #endregion Get Summary
                } //of if (pRecordsExist)

                var groupedProfitCurrencies = objCvwProfitCurrencies.GroupBy(g => new { g.OperationID, g.OperationCode, g.OpenDate, g.BookingPartyName, g.ClientName, g.POLCode, g.PODCode, g.ActualDeparture, g.ContainerTypes, g.ChargeTypeName, g.CurrencyCode, g.PayableDate/*,g.InvoiceDate*/ })
                    .Select(g => new
                    {
                        OperationID = g.First().OperationID
                        ,
                        OperationCode = g.First().OperationCode
                        ,
                        OpenDate = g.First().OpenDate
                        ,
                        PayableDate = g.First().PayableDate
                        //,
                        //InvoiceDate = g.First().InvoiceDate
                        ,
                        BookingPartyName = g.First().BookingPartyName
                        ,
                        ReleaseNumber = g.First().ReleaseNumber
                        ,
                        ChargeableWeightSum = g.First().ChargeableWeightSum
                        ,
                        ClientName = g.First().ClientName
                        ,
                        POLCode = g.First().POLCode
                        ,
                        PODCode = g.First().PODCode
                        ,
                        ActualDeparture = g.First().ActualDeparture
                        ,
                        ContainerTypes = g.First().ContainerTypes
                        ,
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
                        InvoiceNumbers = g.First().InvoiceNumbers,
                        FirstInvoiceDate = g.First().FirstInvoiceDate,
                        POValue = g.First().POValue,
                        PODate = g.First().PODate,
                        ShipperName = g.First().ShipperName,
                        ConsigneeName = g.First().ConsigneeName,
                        MoveTypeName = g.First().MoveTypeName,
                        OperationStageName = g.First().OperationStageName,
                        CostAmount = g.First().CostAmount,
                        RemainingAmount = g.First().RemainingAmount,
                        PaidAmount = g.First().PaidAmount,
                        PayableStatus = g.First().PayableStatus,
                        BillTo = g.First().BillTo,
                        BillToName = g.First().BillToName,
                        PartnerSupplierID = g.First().PartnerSupplierID,
                        PartnerSupplierName = g.First().PartnerSupplierName

                    });
                var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
                return new object[] {
                pRecordsExist
                //, pIncludeChargeDetails || pGroupByOperations ? serializer.Serialize(objCvwProfitCurrencies.lstCVarvwProfitCurrencies) : serializer.Serialize(pData)
                , pIncludeChargeDetails || pGroupByOperations ? serializer.Serialize(groupedProfitCurrencies) : serializer.Serialize(pData)
                , pPayablesCurrenciesSummary
                , pReceivablesOrInvoicesCurrenciesSummary
                , pProfitCurrenciesSummary
                , Math.Round(pMarginSummary, 2)
                , pOperationsIDs
            };
            }
        }//of fn LoadData

    }
}
