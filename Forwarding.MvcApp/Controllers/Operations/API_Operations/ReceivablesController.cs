using Forwarding.MvcApp.Entities.Operations;
using Forwarding.MvcApp.Entities.Quotations;
using Forwarding.MvcApp.Models.Administration.Security.Generated;
using Forwarding.MvcApp.Models.Administration.Settings.Generated;
using Forwarding.MvcApp.Models.Customized;
using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;
using Forwarding.MvcApp.Models.NoAccess.Generated;
using Forwarding.MvcApp.Models.Operations.Operations.Generated;
using Forwarding.MvcApp.Models.Quotations.Quotations.Generated;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebMatrix.WebData;

namespace Forwarding.MvcApp.Controllers.Operations.API_Operations
{
    public class ReceivablesController : ApiController
    {
        [HttpGet, HttpPost]
        public Object[] LoadAll(string pWhereClause)
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
            CvwReceivables objCvwReceivables = new CvwReceivables();
            objCvwReceivables.GetListPaging(999999, 1, pWhereClause, "ChargeTypeName", out _RowCount);

            CNoAccessFreightTypes objCCNoAccessFreightTypes = new CNoAccessFreightTypes();
            objCCNoAccessFreightTypes.GetList(" WHERE 1=1 ");

            CvwCurrencies objCvwCurrencies = new CvwCurrencies();
            objCvwCurrencies.GetList(" WHERE 1=1 Order By Code ");

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] { serializer.Serialize(objCvwReceivables.lstCVarvwReceivables) //data[0]
                , new JavaScriptSerializer().Serialize(objCCNoAccessFreightTypes.lstCVarNoAccessFreightTypes) //data[1]
                , new JavaScriptSerializer().Serialize(objCvwCurrencies.lstCVarvwCurrencies) //data[2] 
            };
        }

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

            CvwReceivables objCvwReceivables = new CvwReceivables();
            //pSearchKey here is the where clause
            objCvwReceivables.GetListPaging(pPageSize, pPageNumber, pWhereClause, " ChargeTypeName ", out _RowCount);

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] { serializer.Serialize(objCvwReceivables.lstCVarvwReceivables), _RowCount };
        }

        [HttpGet, HttpPost]
        public bool InsertListWithoutValues(Int64 pOperationID, string pSelectedIDs, Int64 pQuotationRouteID, Int64 pOperationContainersAndPackagesID, Int64 pOperationVehicleID, Int64 pTruckingOrderID)
        {
            bool _result = false;
            string pWhereClause = "";
            
            int _RowCount = 0;
            int _CurrencyID = 0;
            decimal _ExchangeRate = 1;

            int _RowCount2 = 0;
            CvwDefaults objCvwDefaults = new CvwDefaults();
            objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount2); //i am sure i ve just one row isa
            string CompanyName = objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName;

            pWhereClause = "";
          
            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
            //building the where clause to select the rows from ChargeTypes
            CChargeTypes objCChargeTypes = new CChargeTypes();
           
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
            
            //objCChargeTypes.GetList(pWhereClause);
            objCChargeTypes.GetListPaging(1500, 1, pWhereClause, "ID", out _RowCount);


            foreach (var rowChargeType in objCChargeTypes.lstCVarChargeTypes.ToList())
            {
                CReceivables objCReceivables = new CReceivables();

                decimal _Quantity = 1;
                if (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "MAR" || objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "NIS")
                    _Quantity = Forwarding.MvcApp.Controllers.MasterData.API_Locations.ChargeTypesController.ChargeTypes_GetQuantity(rowChargeType.ID, pOperationID);
                CVarReceivables objCVarReceivables = new CVarReceivables();

                if (objCDefaults.lstCVarDefaults[0].IsTaxOnItems)
                    objCVarReceivables.TaxeTypeID = rowChargeType.TaxeTypeID;

                objCVarReceivables.OperationID = pOperationID;
                objCVarReceivables.ChargeTypeID = rowChargeType.ID;
                objCVarReceivables.MeasurementID = rowChargeType.MeasurementID;
                objCVarReceivables.Quantity = Math.Round(_Quantity, 2);
                objCVarReceivables.ExchangeRate = _ExchangeRate;
                objCVarReceivables.CurrencyID = _CurrencyID;
                objCVarReceivables.ExchangeRate_Foreign = 1;
                objCVarReceivables.CurrencyID_Foreign = objCDefaults.lstCVarDefaults[0].CurrencyID;
                objCVarReceivables.GeneratingQRID = pQuotationRouteID;
                objCVarReceivables.Notes = rowChargeType.Notes;

                objCVarReceivables.IssueDate = DateTime.Now;
                objCVarReceivables.OperationContainersAndPackagesID = pOperationContainersAndPackagesID;
                objCVarReceivables.OperationVehicleID = pOperationVehicleID;
                objCVarReceivables.TruckingOrderID = pTruckingOrderID;

                objCVarReceivables.PreviousCutOffDate = DateTime.Parse("01/01/1900");
                objCVarReceivables.CutOffDate = DateTime.Parse("01/01/1900");
                objCVarReceivables.ReceiptDate = DateTime.Parse("01/01/1900");
                objCVarReceivables.ReceiptNo = "";

                objCVarReceivables.CreatorUserID = objCVarReceivables.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarReceivables.ModificationDate = objCVarReceivables.CreationDate = DateTime.Now;

                objCReceivables.lstCVarReceivables.Add(objCVarReceivables);
                var checkException = objCReceivables.SaveMethod(objCReceivables.lstCVarReceivables);
                if (checkException == null)
                    _result = true;

                if ((CompanyName == "CHM" || CompanyName == "OCE") && checkException == null)
                {
                    CChargeTypesTAX objCChargeTypesTAX = new CChargeTypesTAX();
                    string OperationID = "";
                    objCChargeTypes.GetList("WHERE ID=" + rowChargeType);
                    objCChargeTypesTAX.GetList("WHERE Name=N'" + (objCChargeTypes.lstCVarChargeTypes.Count > 0 ? objCChargeTypes.lstCVarChargeTypes[0].Name : "") + "'");
                    Int32 ChargeTypeID = objCChargeTypesTAX.lstCVarChargeTypes.Count > 0 ? objCChargeTypesTAX.lstCVarChargeTypes[0].ID : 0;
                    if (CompanyName == "CHM")
                    {
                        OperationID = objCCustomizedDBCall.CallStringFunctionByMultiReturn("select top 1 TaxID from ForwardingTransChemTax.dbo.TaxLink WHERE Notes='Operations' and OriginID = " + pOperationID);
                    }
                    else if (CompanyName == "OCE")
                    {
                        OperationID = objCCustomizedDBCall.CallStringFunctionByMultiReturn("select top 1 TaxID from ForwardingTROTax.dbo.TaxLink WHERE Notes='Operations' and OriginID = " + pOperationID);
                    }

                    CReceivablesTax objCReceivablesTax = new CReceivablesTax();

                    CVarReceivablesTAX objCVarReceivablesTax = new CVarReceivablesTAX();
                    if (OperationID != "")
                    {
                        if (objCDefaults.lstCVarDefaults[0].IsTaxOnItems)
                            objCVarReceivablesTax.TaxeTypeID = rowChargeType.TaxeTypeID;

                        objCVarReceivablesTax.OperationID = int.Parse(OperationID);
                        objCVarReceivablesTax.ChargeTypeID = ChargeTypeID;
                        objCVarReceivablesTax.MeasurementID = rowChargeType.MeasurementID;
                        objCVarReceivablesTax.Quantity = Math.Round(_Quantity, 2);
                        objCVarReceivablesTax.ExchangeRate = _ExchangeRate;
                        objCVarReceivablesTax.CurrencyID = _CurrencyID;
                        objCVarReceivablesTax.GeneratingQRID = pQuotationRouteID;
                        objCVarReceivablesTax.Notes = rowChargeType.Notes;

                        objCVarReceivablesTax.IssueDate = DateTime.Now;
                        objCVarReceivablesTax.OperationContainersAndPackagesID = pOperationContainersAndPackagesID;
                        objCVarReceivablesTax.OperationVehicleID = pOperationVehicleID;
                        objCVarReceivablesTax.TruckingOrderID = pTruckingOrderID;

                        objCVarReceivablesTax.PreviousCutOffDate = DateTime.Parse("01/01/1900");
                        objCVarReceivablesTax.CutOffDate = DateTime.Parse("01/01/1900");

                        objCVarReceivablesTax.CreatorUserID = objCVarReceivablesTax.ModificatorUserID = WebSecurity.CurrentUserId;
                        objCVarReceivablesTax.ModificationDate = objCVarReceivablesTax.CreationDate = DateTime.Now;

                        objCReceivablesTax.lstCVarReceivables.Add(objCVarReceivablesTax);

                        checkException = objCReceivablesTax.SaveMethod(objCReceivablesTax.lstCVarReceivables);
                        //link
                        objCCustomizedDBCall.ExecuteQuery_DataTable("insertTaxLink " + objCVarReceivables.ID + "," + objCVarReceivablesTax.ID + "," + "Receivables");
                        if (checkException == null)
                            _result = true;
                    }
                   
                }
            }
            

            
            return _result;
        }

        //insert multi row with values (incase i switch adding Receivables to enter values too)
        [HttpGet, HttpPost]
        public bool InsertListWithValuesFromPayables(Int64 pOperationID, string pSelectedPayableIDs, string pSelectedChargeTypeIDs, string pPOrCList/*, string pSupplierList*/, string pUOMList, string pQuantityList, string pSalePriceList, string pSaleAmountList/*, string pInitialSalePriceList, string pSupplierInvoiceNumberList*/, string pExchangeRateList, string pCurrencyList)
        {
            int NumberOfRows = pSelectedChargeTypeIDs.Split(',').Length;
            bool _result = false;
            Exception checkException = null;
            //string pReceivableIDsToDelete = "";
            //CReceivables objCReceivables = new CReceivables();

            //checkException = objCReceivables.GetList(" WHERE OperationID = " + pOperationID.ToString() + " AND InvoiceID IS NULL AND GeneratingQRID IS NULL ");
            //if (objCReceivables.lstCVarReceivables.Count > 0)
            //{
            //    pReceivableIDsToDelete = objCReceivables.lstCVarReceivables[0].ID.ToString();
            //    for (int i = 1; i < objCReceivables.lstCVarReceivables.Count; i++)
            //        pReceivableIDsToDelete += "," + objCReceivables.lstCVarReceivables[i].ID.ToString();
            //}
            //if (pReceivableIDsToDelete != "")
            //    Delete(pReceivableIDsToDelete, pOperationID);
            //objCReceivables.lstCVarReceivables.Clear();
            //checkException = objCReceivables.DeleteList(" WHERE OperationID = " + pOperationID.ToString() + " AND InvoiceID IS NULL ");
            CPayables objCPayables = new CPayables();
            CDefaults objCDefaults = new CDefaults();
            int _RowCount = 0;
            objCDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa
            for (int i = 0; i < NumberOfRows; i++)
            {
                CVarReceivables objCVarReceivables = new CVarReceivables();
                CTaxeTypes objCTaxTypes = new CTaxeTypes();

                objCVarReceivables.OperationID = pOperationID;
                objCVarReceivables.ChargeTypeID = int.Parse(pSelectedChargeTypeIDs.Split(',')[i]);
                objCVarReceivables.POrC = int.Parse(pPOrCList.Split(',')[i]);
                //objCVarReceivables.SupplierOperationPartnerID = Int64.Parse(pSupplierList.Split(',')[i]);
                objCVarReceivables.MeasurementID = int.Parse(pUOMList.Split(',')[i]);
                objCVarReceivables.Quantity = (pQuantityList.Split(',')[i] == "0" ? 1 : decimal.Parse(pQuantityList.Split(',')[i]));

                CvwPayables objCvwPayables_temp = new CvwPayables();
                objCvwPayables_temp.GetListPaging(1, 1, "WHERE ID=" + pSelectedPayableIDs.Split(',')[i], "ID", out _RowCount);
                if (objCvwPayables_temp.lstCVarvwPayables[0].IsOfficial && pSalePriceList.Split(',')[i] == "0")
                    objCVarReceivables.SalePrice = objCvwPayables_temp.lstCVarvwPayables[0].CostPrice;
                else
                    objCVarReceivables.SalePrice = decimal.Parse(pSalePriceList.Split(',')[i]);

                objCVarReceivables.AmountWithoutVAT = Math.Round((objCVarReceivables.Quantity * objCVarReceivables.SalePrice), 2);

                if (objCDefaults.lstCVarDefaults[0].IsTaxOnItems)
                {
                    objCVarReceivables.TaxeTypeID = GetDefaultTaxTypeID(int.Parse(pSelectedChargeTypeIDs.Split(',')[i]));
                    if (objCVarReceivables.TaxeTypeID > 0)
                    {
                        objCTaxTypes.GetList("WHERE ID=" + objCVarReceivables.TaxeTypeID);
                        objCVarReceivables.TaxPercentage = objCTaxTypes.lstCVarTaxeTypes[0].CurrentPercentage;
                        objCVarReceivables.TaxAmount = Math.Round((objCVarReceivables.AmountWithoutVAT * objCVarReceivables.TaxPercentage / 100), 2);
                    }
                    else
                    {
                        objCVarReceivables.TaxPercentage = 0;
                        objCVarReceivables.TaxAmount = 0;
                        objCVarReceivables.DiscountPercentage = 0;
                    }
                }

                objCVarReceivables.SaleAmount = decimal.Parse(pSaleAmountList.Split(',')[i]) + objCVarReceivables.TaxAmount;
                //objCVarReceivables.InitialSalePrice = decimal.Parse(pInitialSalePriceList.Split(',')[i]);
                //objCVarReceivables.SupplierInvoiceNo = pSupplierInvoiceNumberList.Split(',')[i];
                objCVarReceivables.ExchangeRate = decimal.Parse(pExchangeRateList.Split(',')[i]);
                objCVarReceivables.CurrencyID = int.Parse(pCurrencyList.Split(',')[i]);
                objCVarReceivables.ExchangeRate_Foreign = 1;
                objCVarReceivables.CurrencyID_Foreign = objCDefaults.lstCVarDefaults[0].CurrencyID;

                objCVarReceivables.PayableID = Int64.Parse(pSelectedPayableIDs.Split(',')[i]);

                //objCPayables.GetListPaging(1, 1, "WHERE ID=" + pSelectedPayableIDs.Split(',')[i], "ID", out _RowCount);
                objCVarReceivables.Notes = 
                    objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "BAK"
                    ? objCvwPayables_temp.lstCVarvwPayables[0].SupplierInvoiceNo
                    : (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "FEL"
                        ? objCvwPayables_temp.lstCVarvwPayables[0].Notes
                        : (objCDefaults.lstCVarDefaults[0].UnEditableCompanyName == "CFR"
                            ? objCvwPayables_temp.lstCVarvwPayables[0].SupplierReceiptNo
                            : "0"
                          )
                      );

                objCVarReceivables.IssueDate = DateTime.Now;
                objCVarReceivables.OperationContainersAndPackagesID = 0;

                objCVarReceivables.PreviousCutOffDate = DateTime.Parse("01/01/1900");
                objCVarReceivables.CutOffDate = DateTime.Parse("01/01/1900");
                objCVarReceivables.ReceiptDate = DateTime.Parse("01/01/1900");
                objCVarReceivables.ReceiptNo = "";

                objCVarReceivables.CreatorUserID = objCVarReceivables.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarReceivables.ModificationDate = objCVarReceivables.CreationDate = DateTime.Now;

                CReceivables objCReceivables = new CReceivables();
                objCReceivables.lstCVarReceivables.Add(objCVarReceivables);
                checkException = objCReceivables.SaveMethod(objCReceivables.lstCVarReceivables);

                checkException = objCPayables.UpdateList("ReceivableID=" + objCVarReceivables.ID.ToString() + " WHERE ID=" + objCVarReceivables.PayableID);
            }
            if (checkException == null)
                _result = true;
            return _result;
        }

        ////update multi row with values //incase pInvoiceID > 0 then this is Invoice Update Items
        //[HttpGet, HttpPost]
        //public bool UpdateList(string pSelectedReceivablesIDsToUpdate, string pPOrCList/*, string pSupplierList*/, string pUOMList
        //    , string pQuantityList, string pSalePriceList, string pAmountWithoutVATList, string pTaxTypeIDList, string pTaxPercentageList, string pTaxAmountList, string pDiscountTypeIDList, string pDiscountPercentageList, string pDiscountAmountList
        //    , string pSaleAmountList/*, string pInitialSalePriceList, string pSupplierInvoiceNumberList*/, string pExchangeRateList, string pCurrencyList, string pViewOrderList, Int64 pInvoiceID)
        //{
        //    bool _result = false;
        //    string updateClause = "";
        //    CReceivables objCReceivables = new CReceivables();
        //    Exception checkException = null;

        //    #region In Case of pInvoiceID > 0 existing InvoiceIDs to null to handle deleted items
        //    if (pInvoiceID > 0)
        //    {
        //        updateClause = " InvoiceID = NULL ";
        //        updateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
        //        updateClause += " , ModificationDate = GETDATE() ";
        //        updateClause += " WHERE InvoiceID = " + pInvoiceID.ToString();
        //        checkException = objCReceivables.UpdateList(updateClause);
        //    }
        //    #endregion
        //    int NumberOfRows = pSelectedReceivablesIDsToUpdate.Split(',').Length;

        //    for (int i = 0; i < NumberOfRows; i++)
        //    {
        //        updateClause = " POrC = " + (pPOrCList.Split(',')[i] == "0" ? " NULL " : pPOrCList.Split(',')[i]);
        //        updateClause += " , MeasurementID = " + (pUOMList.Split(',')[i] == "0" ? " NULL " : pUOMList.Split(',')[i]);
        //        updateClause += " , Quantity = " + pQuantityList.Split(',')[i];
        //        updateClause += " , SalePrice = " + pSalePriceList.Split(',')[i];

        //        updateClause += " , AmountWithoutVAT = ROUND(" + (decimal.Parse(pQuantityList.Split(',')[i]) * decimal.Parse(pSalePriceList.Split(',')[i])) + ",2)" + " \n";
        //        updateClause += " , TaxeTypeID = " + (pTaxTypeIDList.Split(',')[i] == "0" ? " NULL " : pTaxTypeIDList.Split(',')[i]);
        //        updateClause += " , TaxPercentage = " + pTaxPercentageList.Split(',')[i];
        //        updateClause += " , TaxAmount = " + pTaxAmountList.Split(',')[i];
        //        updateClause += " , DiscountTypeID = " + (pDiscountTypeIDList.Split(',')[i] == "0" ? " NULL " : pDiscountTypeIDList.Split(',')[i]);
        //        updateClause += " , DiscountPercentage = " + pDiscountPercentageList.Split(',')[i];
        //        updateClause += " , DiscountAmount = " + pDiscountAmountList.Split(',')[i];

        //        updateClause += " , SaleAmount = " + pSaleAmountList.Split(',')[i];

        //        updateClause += " , ExchangeRate = " + pExchangeRateList.Split(',')[i];
        //        updateClause += " , CurrencyID = " + (pCurrencyList.Split(',')[i] == "0" ? " NULL " : pCurrencyList.Split(',')[i]);
        //        updateClause += " , ViewOrder = " + pViewOrderList.Split(',')[i];
        //        //the next line is used just in case i am updating invoice items
        //        updateClause += pInvoiceID > 0 ? " , InvoiceID = " + pInvoiceID.ToString() : "";
        //        updateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
        //        updateClause += " , ModificationDate = GETDATE() ";

        //        updateClause += " WHERE ID = " + pSelectedReceivablesIDsToUpdate.Split(',')[i];

        //        checkException = objCReceivables.UpdateList(updateClause);

        //        #region ensure receivables are correct
        //        updateClause = " AmountWithoutVAT = ROUND(" + (decimal.Parse(pQuantityList.Split(',')[i]) * decimal.Parse(pSalePriceList.Split(',')[i])) + ",2)" + " \n";
        //        updateClause += " , TaxPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID)" + " \n";
        //        updateClause += " , TaxAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID)" + " \n";
        //        updateClause += " , DiscountPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID)" + " \n";
        //        updateClause += " , DiscountAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID)" + " \n";
        //        updateClause += " , SaleAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2)" + " \n"
        //                      + " + (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)), 2) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID),0))" + " \n"
        //                      + " - (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)), 2) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),0))" + " \n";
        //        updateClause += " WHERE ID = " + pSelectedReceivablesIDsToUpdate.Split(',')[i];
        //        checkException = objCReceivables.UpdateList(updateClause);
        //        #endregion ensure receivables are correct

        //        if (checkException != null) // an exception is caught in the model
        //        {
        //            if (checkException.Message.Contains("UNIQUE"))
        //            {
        //                _result = false;
        //                #region Update Invoice totals at server side to fix any connection problem
        //                CInvoices objCInvoices = new CInvoices();
        //                //SET Tax, Discount & Total Amount after setting the AmountWithoutVAT
        //                updateClause = " TaxAmount = ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(TaxPercentage, 0) / 100),2)" + "\n";
        //                updateClause += " , DiscountAmount = ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2)";
        //                updateClause += " , Amount = ROUND((ISNULL(AmountWithoutVAT, 0) + ROUND( (ISNULL(AmountWithoutVAT, 0) * ISNULL(TaxPercentage, 0) / 100),2) - ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2)),2)";
        //                updateClause += " WHERE ID = " + pInvoiceID.ToString();
        //                checkException = objCInvoices.UpdateList(updateClause);
        //                #endregion Update Invoice totals at server side to fix any connection problem

        //            }
        //        }
        //        else
        //            _result = true;
        //    }
        //    return _result;
        //}

        //update multi row with values //incase pInvoiceID > 0 then this is Invoice Update Items
        [HttpGet, HttpPost]
        public bool UpdateList([FromBody] UpdateListParameters updateListParameters)
        {
            bool _result = false;
            string updateClause = "";
            CReceivables objCReceivables = new CReceivables();
            Exception checkException = null;

            #region In Case of pInvoiceID > 0 existing InvoiceIDs to null to handle deleted items
            if (updateListParameters.pInvoiceID > 0)
            {
                updateClause = " InvoiceID = NULL ";
                updateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                updateClause += " , ModificationDate = GETDATE() ";
                updateClause += " WHERE InvoiceID = " + updateListParameters.pInvoiceID.ToString();
                checkException = objCReceivables.UpdateList(updateClause);
            }
            #endregion
            int NumberOfRows = updateListParameters.pSelectedReceivablesIDsToUpdate.Split(',').Length;

            for (int i = 0; i < NumberOfRows; i++)
            {
                updateClause = " POrC = " + (updateListParameters.pPOrCList.Split(',')[i] == "0" ? " NULL " : updateListParameters.pPOrCList.Split(',')[i]);
                updateClause += " , MeasurementID = " + (updateListParameters.pUOMList.Split(',')[i] == "0" ? " NULL " : updateListParameters.pUOMList.Split(',')[i]);
                updateClause += " , Quantity = " + updateListParameters.pQuantityList.Split(',')[i];
                updateClause += " , SalePrice = " + updateListParameters.pSalePriceList.Split(',')[i];

                updateClause += " , AmountWithoutVAT = ROUND(" + (decimal.Parse(updateListParameters.pQuantityList.Split(',')[i]) * decimal.Parse(updateListParameters.pSalePriceList.Split(',')[i])) + ",2)" + " \n";
                updateClause += " , TaxeTypeID = " + (updateListParameters.pTaxTypeIDList.Split(',')[i] == "0" ? " NULL " : updateListParameters.pTaxTypeIDList.Split(',')[i]);
                updateClause += " , TaxPercentage = " + updateListParameters.pTaxPercentageList.Split(',')[i];
                updateClause += " , TaxAmount = " + updateListParameters.pTaxAmountList.Split(',')[i];
                updateClause += " , DiscountTypeID = " + (updateListParameters.pDiscountTypeIDList.Split(',')[i] == "0" ? " NULL " : updateListParameters.pDiscountTypeIDList.Split(',')[i]);
                updateClause += " , DiscountPercentage = " + updateListParameters.pDiscountPercentageList.Split(',')[i];
                updateClause += " , DiscountAmount = " + updateListParameters.pDiscountAmountList.Split(',')[i];

                updateClause += " , SaleAmount = " + updateListParameters.pSaleAmountList.Split(',')[i];

                updateClause += " , ExchangeRate = " + updateListParameters.pExchangeRateList.Split(',')[i];
                updateClause += " , CurrencyID = " + (updateListParameters.pCurrencyList.Split(',')[i] == "0" ? " NULL " : updateListParameters.pCurrencyList.Split(',')[i]);
                updateClause += " , ReceiptNo = " + updateListParameters.pReceiptNoList.Split(',')[i];
                updateClause += " , ReceiptDate = '" + updateListParameters.pReceiptDateList.Split(',')[i] + "'";
                if (updateListParameters.pNotesList.Split(',')[i] == "0")
                {
                    updateClause += " , Notes = NULL";
                }
                else
                {
                    updateClause += " , Notes = N'" + updateListParameters.pNotesList.Split(',')[i] + "'";
                }
                updateClause += " , ViewOrder = " + updateListParameters.pViewOrderList.Split(',')[i];
                //the next line is used just in case i am updating invoice items
                updateClause += updateListParameters.pInvoiceID > 0 ? " , InvoiceID = " + updateListParameters.pInvoiceID.ToString() : "";
                updateClause += " , ModificatorUserID = " + WebSecurity.CurrentUserId.ToString();
                updateClause += " , ModificationDate = GETDATE() ";

                updateClause += " WHERE ID = " + updateListParameters.pSelectedReceivablesIDsToUpdate.Split(',')[i];

                checkException = objCReceivables.UpdateList(updateClause);

                #region ensure receivables are correct
                updateClause = " AmountWithoutVAT = ROUND(" + (decimal.Parse(updateListParameters.pQuantityList.Split(',')[i]) * decimal.Parse(updateListParameters.pSalePriceList.Split(',')[i])) + ",2)" + " \n";
                updateClause += " , TaxPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID)" + " \n";
                updateClause += " , TaxAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID)" + " \n";
                updateClause += " , DiscountPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID)" + " \n";
                updateClause += " , DiscountAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2) * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID)" + " \n";
                updateClause += " , SaleAmount = ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)),2)" + " \n"
                              + " + (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)), 2) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID),0))" + " \n"
                              + " - (ROUND((ISNULL(SalePrice, 0) * ISNULL(Quantity, 1)), 2) * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),0))" + " \n";
                updateClause += " WHERE ID = " + updateListParameters.pSelectedReceivablesIDsToUpdate.Split(',')[i];
                checkException = objCReceivables.UpdateList(updateClause);
                #endregion ensure receivables are correct

                if (checkException != null) // an exception is caught in the model
                {
                    if (checkException.Message.Contains("UNIQUE"))
                    {
                        _result = false;
                        #region Update Invoice totals at server side to fix any connection problem
                        CInvoices objCInvoices = new CInvoices();
                        //SET Tax, Discount & Total Amount after setting the AmountWithoutVAT
                        updateClause = " TaxAmount = ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(TaxPercentage, 0) / 100),2)" + "\n";
                        updateClause += " , DiscountAmount = ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2)";
                        updateClause += " , Amount = ROUND((ISNULL(AmountWithoutVAT, 0) + ROUND( (ISNULL(AmountWithoutVAT, 0) * ISNULL(TaxPercentage, 0) / 100),2) - ROUND((ISNULL(AmountWithoutVAT, 0) * ISNULL(DiscountPercentage, 0) / 100),2)),2)";
                        updateClause += " WHERE ID = " + updateListParameters.pInvoiceID.ToString();
                        checkException = objCInvoices.UpdateList(updateClause);
                        #endregion Update Invoice totals at server side to fix any connection problem

                    }
                }
                else
                    _result = true;
            }
            return _result;
        }

        //pSavedFrom=10: Called from Operations so update tank(i.e pOperationContainersAndPackagesID)
        [HttpGet, HttpPost]
        public bool Update(Int32 pSavedFrom, Int64 pOperationContainersAndPackagesID, bool pIsReceipt, Int64 pHouseBillID
            , Int64 pID, Int32 pOperationID, Int32 pChargeTypeID, int pPOrC, Int32 pSupplierID, Int32 pMeasurementID
            , Int32 pContainerTypeID, decimal pQuantity, Decimal pCostPrice, Decimal pCostAmount, Decimal pSalePrice
            , Decimal pAmountWithoutVAT, Int32 pTaxTypeID, Decimal pTaxPercentage, Decimal pTaxAmount, Int32 pDiscountTypeID, Decimal pDiscountPercentage, Decimal pDiscountAmount
            , Decimal pSaleAmount, Decimal pExchangeRate, Int32 pCurrencyID, string pNotes, DateTime pIssueDate
            , Decimal pSalePrice_Foreign, Decimal pExchangeRate_Foreign, Int32 pCurrencyID_Foreign)
        {
            bool _result = false;
            Exception checkException = null;
            CReceivables objCReceivables = new CReceivables();
            objCReceivables.GetItem(pID);
            if (objCReceivables.lstCVarReceivables[0].InvoiceID == 0 && objCReceivables.lstCVarReceivables[0].DraftInvoiceID == 0)
            {
                if (pSavedFrom == 10)
                {
                    objCReceivables.lstCVarReceivables[0].OperationContainersAndPackagesID = pOperationContainersAndPackagesID;
                    objCReceivables.lstCVarReceivables[0].IsReceipt = pIsReceipt;
                    objCReceivables.lstCVarReceivables[0].HouseBillID = pHouseBillID;
                    #region Foreign Currency
                    objCReceivables.lstCVarReceivables[0].SalePrice_Foreign = pSalePrice_Foreign;
                    objCReceivables.lstCVarReceivables[0].ExchangeRate_Foreign = pExchangeRate_Foreign;
                    objCReceivables.lstCVarReceivables[0].CurrencyID_Foreign = pCurrencyID_Foreign;
                    #endregion Foreign Currency
                }
                objCReceivables.lstCVarReceivables[0].OperationID = pOperationID;
                objCReceivables.lstCVarReceivables[0].ChargeTypeID = pChargeTypeID;
                objCReceivables.lstCVarReceivables[0].POrC = pPOrC;
                objCReceivables.lstCVarReceivables[0].MeasurementID = pMeasurementID;
                objCReceivables.lstCVarReceivables[0].ContainerTypeID = pContainerTypeID;
                objCReceivables.lstCVarReceivables[0].SupplierID = pSupplierID;
                objCReceivables.lstCVarReceivables[0].Quantity = pQuantity == 0 ? 1 : pQuantity;
                objCReceivables.lstCVarReceivables[0].CostPrice = pCostPrice;
                objCReceivables.lstCVarReceivables[0].CostAmount = pCostAmount;
                objCReceivables.lstCVarReceivables[0].SalePrice = pSalePrice;

                objCReceivables.lstCVarReceivables[0].AmountWithoutVAT = Math.Round((objCReceivables.lstCVarReceivables[0].Quantity * objCReceivables.lstCVarReceivables[0].SalePrice), 2);
                objCReceivables.lstCVarReceivables[0].TaxeTypeID = pTaxTypeID;
                objCReceivables.lstCVarReceivables[0].TaxPercentage = pTaxPercentage;
                objCReceivables.lstCVarReceivables[0].TaxAmount = pTaxAmount;
                objCReceivables.lstCVarReceivables[0].DiscountTypeID = pDiscountTypeID;
                objCReceivables.lstCVarReceivables[0].DiscountPercentage = pDiscountPercentage;
                objCReceivables.lstCVarReceivables[0].DiscountAmount = pDiscountAmount;

                objCReceivables.lstCVarReceivables[0].SaleAmount = pSaleAmount;
                objCReceivables.lstCVarReceivables[0].ExchangeRate = pExchangeRate;
                objCReceivables.lstCVarReceivables[0].CurrencyID = pCurrencyID;
                objCReceivables.lstCVarReceivables[0].Notes = (pNotes == null ? "0" : pNotes);

                objCReceivables.lstCVarReceivables[0].IssueDate = pIssueDate;

                objCReceivables.lstCVarReceivables[0].ModificatorUserID = WebSecurity.CurrentUserId;
                objCReceivables.lstCVarReceivables[0].ModificationDate = DateTime.Now;
                checkException = objCReceivables.SaveMethod(objCReceivables.lstCVarReceivables);
            }
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
                _result = true;
            #region Tax
            int _RowCount2 = 0;
            CvwDefaults objCvwDefaults = new CvwDefaults();
            objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount2); //i am sure i ve just one row isa
            string CompanyName = objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName;

            if ((CompanyName == "CHM" || CompanyName == "OCE") && checkException == null)
            {
                CChargeTypesTAX objCChargeTypes = new CChargeTypesTAX();
                CChargeTypesTAX objCChargeTypesTAX = new CChargeTypesTAX();

                objCChargeTypes.GetList("WHERE ID=" + pChargeTypeID);
                objCChargeTypesTAX.GetList("WHERE Name=N'" + (objCChargeTypes.lstCVarChargeTypes.Count > 0 ? objCChargeTypes.lstCVarChargeTypes[0].Name : "") + "'");

                checkException = null;
                CReceivablesTax objCReceivablesTax = new CReceivablesTax();

                CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
                string ReceivablesID = "";
                string OperationID = "";
                if (CompanyName == "CHM")
                {
                    ReceivablesID = objCCustomizedDBCall.CallStringFunctionByMultiReturn("select top 1 TaxID from ForwardingTransChemTax.dbo.TaxLink WHERE Notes='Receivables' and OriginID = " + pID);
                    OperationID = objCCustomizedDBCall.CallStringFunctionByMultiReturn("select top 1 TaxID from ForwardingTransChemTax.dbo.TaxLink WHERE Notes='Operations' and OriginID = " + pOperationID);

                }
                else if (CompanyName == "OCE")
                {
                    ReceivablesID = objCCustomizedDBCall.CallStringFunctionByMultiReturn("select top 1 TaxID from ForwardingTROTax.dbo.TaxLink WHERE Notes='Receivables' and OriginID = " + pID);
                    OperationID = objCCustomizedDBCall.CallStringFunctionByMultiReturn("select top 1 TaxID from ForwardingTROTax.dbo.TaxLink WHERE Notes='Operations' and OriginID = " + pOperationID);

                }
                if (OperationID != "" && ReceivablesID != "")
                {
                    //objCGetCreationInformationTax.GetItem(int.Parse(ReceivablesID));
                    objCReceivablesTax.GetItem(int.Parse(ReceivablesID));
                    if (objCReceivablesTax.lstCVarReceivables[0].InvoiceID == 0 && objCReceivablesTax.lstCVarReceivables[0].DraftInvoiceID == 0)
                    {
                        if (pSavedFrom == 10)
                        {
                            objCReceivablesTax.lstCVarReceivables[0].OperationContainersAndPackagesID = pOperationContainersAndPackagesID;
                            objCReceivablesTax.lstCVarReceivables[0].IsReceipt = pIsReceipt;
                            objCReceivablesTax.lstCVarReceivables[0].HouseBillID = pHouseBillID;
                        }
                        
                        objCReceivablesTax.lstCVarReceivables[0].OperationID = int.Parse(OperationID);
                        objCReceivablesTax.lstCVarReceivables[0].ChargeTypeID = pChargeTypeID;
                        objCReceivablesTax.lstCVarReceivables[0].POrC = pPOrC;
                        objCReceivablesTax.lstCVarReceivables[0].MeasurementID = pMeasurementID;
                        objCReceivablesTax.lstCVarReceivables[0].ContainerTypeID = pContainerTypeID;
                        objCReceivablesTax.lstCVarReceivables[0].SupplierID = pSupplierID;
                        objCReceivablesTax.lstCVarReceivables[0].Quantity = pQuantity == 0 ? 1 : pQuantity;
                        objCReceivablesTax.lstCVarReceivables[0].CostPrice = pCostPrice;
                        objCReceivablesTax.lstCVarReceivables[0].CostAmount = pCostAmount;
                        objCReceivablesTax.lstCVarReceivables[0].SalePrice = pSalePrice;

                        objCReceivablesTax.lstCVarReceivables[0].AmountWithoutVAT = Math.Round((objCReceivablesTax.lstCVarReceivables[0].Quantity * objCReceivablesTax.lstCVarReceivables[0].SalePrice), 2);
                        objCReceivablesTax.lstCVarReceivables[0].TaxeTypeID = pTaxTypeID;
                        objCReceivablesTax.lstCVarReceivables[0].TaxPercentage = pTaxPercentage;
                        objCReceivablesTax.lstCVarReceivables[0].TaxAmount = pTaxAmount;
                        objCReceivablesTax.lstCVarReceivables[0].DiscountTypeID = pDiscountTypeID;
                        objCReceivablesTax.lstCVarReceivables[0].DiscountPercentage = pDiscountPercentage;
                        objCReceivablesTax.lstCVarReceivables[0].DiscountAmount = pDiscountAmount;

                        objCReceivablesTax.lstCVarReceivables[0].SaleAmount = pSaleAmount;
                        objCReceivablesTax.lstCVarReceivables[0].ExchangeRate = pExchangeRate;
                        objCReceivablesTax.lstCVarReceivables[0].CurrencyID = pCurrencyID;
                        objCReceivablesTax.lstCVarReceivables[0].Notes = (pNotes == null ? "0" : pNotes);

                        objCReceivablesTax.lstCVarReceivables[0].IssueDate = pIssueDate;

                        objCReceivablesTax.lstCVarReceivables[0].ModificatorUserID = WebSecurity.CurrentUserId;
                        objCReceivablesTax.lstCVarReceivables[0].ModificationDate = DateTime.Now;

                        checkException = objCReceivablesTax.SaveMethod(objCReceivablesTax.lstCVarReceivables);
                    }
                    if (checkException != null) // an exception is caught in the model
                    {
                        if (checkException.Message.Contains("UNIQUE"))
                            _result = false;
                    }
                    else //not unique
                        _result = true;

                }

            }
            #endregion

            return _result;
        }

        ////pSavedFrom=10: Called from Operations so update tank(i.e pOperationContainersAndPackagesID)
        //[HttpGet, HttpPost]
        //public bool Update(Int32 pSavedFrom, Int64 pOperationContainersAndPackagesID, bool pIsReceipt, Int64 pHouseBillID
        //    , Int64 pID, Int32 pOperationID, Int32 pChargeTypeID, int pPOrC, Int32 pSupplierID, Int32 pMeasurementID
        //    , Int32 pContainerTypeID, decimal pQuantity, Decimal pCostPrice, Decimal pCostAmount, Decimal pSalePrice
        //    , Decimal pAmountWithoutVAT, Int32 pTaxTypeID, Decimal pTaxPercentage, Decimal pTaxAmount, Int32 pDiscountTypeID, Decimal pDiscountPercentage, Decimal pDiscountAmount
        //    , Decimal pSaleAmount, Decimal pExchangeRate, Int32 pCurrencyID, string pNotes, DateTime pIssueDate)
        //{
        //    bool _result = false;
        //    CVarReceivables objCVarReceivables = new CVarReceivables();
        //    Exception checkException = null;
        //    //the next 4 lines to get the CreatorUserID and CreationDate and set them as they were entered before
        //    CReceivables objCGetCreationInformation = new CReceivables();
        //    objCGetCreationInformation.GetItem(pID);
        //    if (objCGetCreationInformation.lstCVarReceivables[0].InvoiceID == 0 && objCGetCreationInformation.lstCVarReceivables[0].DraftInvoiceID == 0)
        //    {
        //        objCVarReceivables.CreatorUserID = objCGetCreationInformation.lstCVarReceivables[0].CreatorUserID;
        //        objCVarReceivables.CreationDate = objCGetCreationInformation.lstCVarReceivables[0].CreationDate;
        //        objCVarReceivables.GeneratingQRID = objCGetCreationInformation.lstCVarReceivables[0].GeneratingQRID;
        //        objCVarReceivables.AccNoteID = objCGetCreationInformation.lstCVarReceivables[0].AccNoteID;
        //        objCVarReceivables.HouseBillID = objCGetCreationInformation.lstCVarReceivables[0].HouseBillID;
        //        objCVarReceivables.OperationVehicleID = objCGetCreationInformation.lstCVarReceivables[0].OperationVehicleID;
        //        objCVarReceivables.TruckingOrderID = objCGetCreationInformation.lstCVarReceivables[0].TruckingOrderID;
        //        objCVarReceivables.PreviousCutOffDate = objCGetCreationInformation.lstCVarReceivables[0].PreviousCutOffDate;
        //        objCVarReceivables.CutOffDate = objCGetCreationInformation.lstCVarReceivables[0].CutOffDate;
        //        objCVarReceivables.VehicleAgingReportID = objCGetCreationInformation.lstCVarReceivables[0].VehicleAgingReportID;
        //        objCVarReceivables.InvoiceID_3PL = objCGetCreationInformation.lstCVarReceivables[0].InvoiceID_3PL;

        //        if (pSavedFrom == 10)
        //        {
        //            objCVarReceivables.OperationContainersAndPackagesID = pOperationContainersAndPackagesID;
        //            objCVarReceivables.IsReceipt = pIsReceipt;
        //            objCVarReceivables.HouseBillID = pHouseBillID;
        //        }
        //        else
        //        {
        //            objCVarReceivables.OperationContainersAndPackagesID = objCGetCreationInformation.lstCVarReceivables[0].OperationContainersAndPackagesID;
        //            objCVarReceivables.IsReceipt = objCGetCreationInformation.lstCVarReceivables[0].IsReceipt;
        //            objCVarReceivables.HouseBillID = objCGetCreationInformation.lstCVarReceivables[0].HouseBillID;
        //        }

        //        objCVarReceivables.ID = pID;

        //        objCVarReceivables.OperationID = pOperationID;
        //        objCVarReceivables.ChargeTypeID = pChargeTypeID;
        //        objCVarReceivables.POrC = pPOrC;
        //        objCVarReceivables.MeasurementID = pMeasurementID;
        //        objCVarReceivables.ContainerTypeID = pContainerTypeID;
        //        objCVarReceivables.SupplierID = pSupplierID;
        //        objCVarReceivables.Quantity = pQuantity == 0 ? 1 : pQuantity;
        //        objCVarReceivables.CostPrice = pCostPrice;
        //        objCVarReceivables.CostAmount = pCostAmount;
        //        objCVarReceivables.SalePrice = pSalePrice;

        //        objCVarReceivables.AmountWithoutVAT = Math.Round((objCVarReceivables.Quantity * objCVarReceivables.SalePrice), 2);
        //        objCVarReceivables.TaxeTypeID = pTaxTypeID;
        //        objCVarReceivables.TaxPercentage = pTaxPercentage;
        //        objCVarReceivables.TaxAmount = pTaxAmount;
        //        objCVarReceivables.DiscountTypeID = pDiscountTypeID;
        //        objCVarReceivables.DiscountPercentage = pDiscountPercentage;
        //        objCVarReceivables.DiscountAmount = pDiscountAmount;

        //        objCVarReceivables.SaleAmount = pSaleAmount;
        //        objCVarReceivables.ExchangeRate = pExchangeRate;
        //        objCVarReceivables.CurrencyID = pCurrencyID;
        //        objCVarReceivables.Notes = (pNotes == null ? "0" : pNotes);

        //        objCVarReceivables.IssueDate = pIssueDate;

        //        objCVarReceivables.ModificatorUserID = WebSecurity.CurrentUserId;
        //        objCVarReceivables.ModificationDate = DateTime.Now;

        //        CReceivables objCReceivables = new CReceivables();
        //        objCReceivables.lstCVarReceivables.Add(objCVarReceivables);
        //        //if (objCGetCreationInformation.lstCVarReceivables[0].InvoiceID == 0 && objCGetCreationInformation.lstCVarReceivables[0].AccNoteID == 0)
        //        checkException = objCReceivables.SaveMethod(objCReceivables.lstCVarReceivables);
        //    }
        //    if (checkException != null) // an exception is caught in the model
        //    {
        //        if (checkException.Message.Contains("UNIQUE"))
        //            _result = false;
        //    }
        //    else //not unique
        //        _result = true;
        //    #region Tax
        //    int _RowCount2 = 0;
        //    CvwDefaults objCvwDefaults = new CvwDefaults();
        //    objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount2); //i am sure i ve just one row isa
        //    string CompanyName = objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName;

        //    if ((CompanyName == "CHM" || CompanyName == "OCE") && checkException == null )
        //    {
        //        CChargeTypesTAX objCChargeTypes = new CChargeTypesTAX();
        //        CChargeTypesTAX objCChargeTypesTAX = new CChargeTypesTAX();

        //        objCChargeTypes.GetList("WHERE ID=" + pChargeTypeID);
        //        objCChargeTypesTAX.GetList("WHERE Name=N'" + (objCChargeTypes.lstCVarChargeTypes.Count > 0 ? objCChargeTypes.lstCVarChargeTypes[0].Name : "") + "'");

        //        CVarReceivablesTAX objCVarReceivablesTax = new CVarReceivablesTAX();
        //        checkException = null;
        //        //the next 4 lines to get the CreatorUserID and CreationDate and set them as they were entered before
        //        CReceivablesTax objCGetCreationInformationTax = new CReceivablesTax();

        //        CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
        //        string ReceivablesID = "";
        //        string OperationID = "";
        //        if (CompanyName == "CHM")
        //        {
        //            ReceivablesID = objCCustomizedDBCall.CallStringFunctionByMultiReturn("select top 1 TaxID from ForwardingTransChemTax.dbo.TaxLink WHERE Notes='Receivables' and OriginID = " + pID);
        //            OperationID = objCCustomizedDBCall.CallStringFunctionByMultiReturn("select top 1 TaxID from ForwardingTransChemTax.dbo.TaxLink WHERE Notes='Operations' and OriginID = " + pOperationID);

        //        }
        //        else if (CompanyName == "OCE")
        //        {
        //            ReceivablesID = objCCustomizedDBCall.CallStringFunctionByMultiReturn("select top 1 TaxID from ForwardingTROTax.dbo.TaxLink WHERE Notes='Receivables' and OriginID = " + pID);
        //            OperationID = objCCustomizedDBCall.CallStringFunctionByMultiReturn("select top 1 TaxID from ForwardingTROTax.dbo.TaxLink WHERE Notes='Operations' and OriginID = " + pOperationID);

        //        }
        //        if (OperationID != ""  && ReceivablesID != "")
        //        {
        //            objCGetCreationInformationTax.GetItem(int.Parse(ReceivablesID));
        //            if (objCGetCreationInformationTax.lstCVarReceivables[0].InvoiceID == 0 && objCGetCreationInformationTax.lstCVarReceivables[0].DraftInvoiceID == 0)
        //            {
        //                objCVarReceivablesTax.CreatorUserID = objCGetCreationInformation.lstCVarReceivables[0].CreatorUserID;
        //                objCVarReceivablesTax.CreationDate = objCGetCreationInformation.lstCVarReceivables[0].CreationDate;
        //                objCVarReceivablesTax.GeneratingQRID = 0;// objCGetCreationInformation.lstCVarReceivables[0].GeneratingQRID;
        //                objCVarReceivablesTax.AccNoteID = objCGetCreationInformation.lstCVarReceivables[0].AccNoteID;
        //                objCVarReceivablesTax.HouseBillID = objCGetCreationInformation.lstCVarReceivables[0].HouseBillID;
        //                objCVarReceivablesTax.OperationVehicleID = objCGetCreationInformation.lstCVarReceivables[0].OperationVehicleID;
        //                objCVarReceivablesTax.TruckingOrderID = objCGetCreationInformation.lstCVarReceivables[0].TruckingOrderID;
        //                objCVarReceivablesTax.PreviousCutOffDate = objCGetCreationInformation.lstCVarReceivables[0].PreviousCutOffDate;
        //                objCVarReceivablesTax.CutOffDate = objCGetCreationInformation.lstCVarReceivables[0].CutOffDate;
        //                objCVarReceivablesTax.VehicleAgingReportID = objCGetCreationInformation.lstCVarReceivables[0].VehicleAgingReportID;
        //                if (pSavedFrom == 10)
        //                {
        //                    objCVarReceivablesTax.OperationContainersAndPackagesID = pOperationContainersAndPackagesID;
        //                    objCVarReceivablesTax.IsReceipt = pIsReceipt;
        //                    objCVarReceivablesTax.HouseBillID = pHouseBillID;
        //                }
        //                else
        //                {
        //                    objCVarReceivablesTax.OperationContainersAndPackagesID = objCGetCreationInformation.lstCVarReceivables[0].OperationContainersAndPackagesID;
        //                    objCVarReceivablesTax.IsReceipt = objCGetCreationInformation.lstCVarReceivables[0].IsReceipt;
        //                    objCVarReceivablesTax.HouseBillID = objCGetCreationInformation.lstCVarReceivables[0].HouseBillID;
        //                }
        //                objCVarReceivablesTax.ID = int.Parse(ReceivablesID);

        //                objCVarReceivablesTax.OperationID = int.Parse(OperationID);
        //                objCVarReceivablesTax.ChargeTypeID = pChargeTypeID;
        //                objCVarReceivablesTax.POrC = pPOrC;
        //                objCVarReceivablesTax.MeasurementID = pMeasurementID;
        //                objCVarReceivablesTax.ContainerTypeID = pContainerTypeID;
        //                objCVarReceivablesTax.SupplierID = pSupplierID;
        //                objCVarReceivablesTax.Quantity = pQuantity == 0 ? 1 : pQuantity;
        //                objCVarReceivablesTax.CostPrice = pCostPrice;
        //                objCVarReceivablesTax.CostAmount = pCostAmount;
        //                objCVarReceivablesTax.SalePrice = pSalePrice;

        //                objCVarReceivablesTax.AmountWithoutVAT = Math.Round((objCVarReceivables.Quantity * objCVarReceivables.SalePrice), 2);
        //                objCVarReceivablesTax.TaxeTypeID = pTaxTypeID;
        //                objCVarReceivablesTax.TaxPercentage = pTaxPercentage;
        //                objCVarReceivablesTax.TaxAmount = pTaxAmount;
        //                objCVarReceivablesTax.DiscountTypeID = pDiscountTypeID;
        //                objCVarReceivablesTax.DiscountPercentage = pDiscountPercentage;
        //                objCVarReceivablesTax.DiscountAmount = pDiscountAmount;

        //                objCVarReceivablesTax.SaleAmount = pSaleAmount;
        //                objCVarReceivablesTax.ExchangeRate = pExchangeRate;
        //                objCVarReceivablesTax.CurrencyID = pCurrencyID;
        //                objCVarReceivablesTax.Notes = (pNotes == null ? "0" : pNotes);

        //                objCVarReceivablesTax.IssueDate = pIssueDate;

        //                objCVarReceivablesTax.ModificatorUserID = WebSecurity.CurrentUserId;
        //                objCVarReceivablesTax.ModificationDate = DateTime.Now;

        //                CReceivablesTax objCReceivablesTax = new CReceivablesTax();
        //                objCReceivablesTax.lstCVarReceivables.Add(objCVarReceivablesTax);
        //                //if (objCGetCreationInformation.lstCVarReceivables[0].InvoiceID == 0 && objCGetCreationInformation.lstCVarReceivables[0].AccNoteID == 0)
        //                checkException = objCReceivablesTax.SaveMethod(objCReceivablesTax.lstCVarReceivables);
        //            }
        //            if (checkException != null) // an exception is caught in the model
        //            {
        //                if (checkException.Message.Contains("UNIQUE"))
        //                    _result = false;
        //            }
        //            else //not unique
        //                _result = true;

        //        }

        //    }
        //    #endregion

        //    return _result;
        //}

        [HttpGet, HttpPost]
        public bool Delete(String pReceivablesIDs, Int64 pOperationID)
        {
            bool _result = true;
            Exception checkException = null;
            CReceivablesTax objCReceivablesTax = new CReceivablesTax();
            CPayablesTAX objCPayablesTax = new CPayablesTAX();
            COperationLog objCOperationLog = new COperationLog();


            int _RowCount2 = 0;
            CvwDefaults objCvwDefaults = new CvwDefaults();
            objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount2); //i am sure i ve just one row isa
            string CompanyName = objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName;
            if (CompanyName == "CHM" || CompanyName == "OCE")
            {
                CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
                string ReceivablesID = "";


                foreach (var currentID in pReceivablesIDs.Split(','))
                {
                    if (CompanyName == "CHM")
                    {
                        ReceivablesID = objCCustomizedDBCall.CallStringFunctionByMultiReturn("select top 1 TaxID from ForwardingTransChemTax.dbo.TaxLink WHERE Notes='Receivables' and OriginID = " + currentID);
                    }
                    else if (CompanyName == "OCE")
                    {
                        ReceivablesID = objCCustomizedDBCall.CallStringFunctionByMultiReturn("select top 1 TaxID from ForwardingTROTax.dbo.TaxLink WHERE Notes='Receivables' and OriginID = " + currentID);
                    }

                    checkException = objCReceivablesTax.DeleteList("WHERE ID=" + ReceivablesID);
                    if (checkException == null)
                    {
                      
                        objCPayablesTax.UpdateList("ReceivableID=NULL WHERE ReceivableID=" + ReceivablesID);
                        if (CompanyName == "CHM")
                        {
                            objCCustomizedDBCall.CallStringFunctionByMultiReturn("delete from ForwardingTransChemTax.dbo.TaxLink WHERE Notes='Receivables' and TaxID = " + ReceivablesID);
                        }
                        else if (CompanyName == "OCE")
                        {
                            objCCustomizedDBCall.CallStringFunctionByMultiReturn("delete from ForwardingTROTax.dbo.TaxLink WHERE Notes='Receivables' and TaxID = " + ReceivablesID);
                        }

                    }
                    else
                        _result = false;
                }
            }
            
            CReceivables objCReceivables = new CReceivables();
            CPayables objCPayables = new CPayables();
            foreach (var currentID in pReceivablesIDs.Split(','))
            {


                checkException = objCReceivables.DeleteList("WHERE ID=" + currentID);
                if (checkException == null)
                {
                    objCOperationLog.UpdateList("UserID=" + WebSecurity.CurrentUserId.ToString() + ", UserName=N'" + WebSecurity.CurrentUserName + "'"
                                                + " WHERE ActionOnRowID IN (" + pReceivablesIDs + ") AND ActionOnRowID IN(" + pReceivablesIDs.ToString() + ")"
                                                + " AND ActionType='D' AND UserID IS NULL AND LogFor = " + 20);
                    objCPayables.UpdateList("ReceivableID=NULL WHERE ReceivableID=" + currentID);
                }
                else
                    _result = false;
            }
            return _result;
        }

        [HttpGet, HttpPost]
        public object[] CopyReceivable(Int64 pReceivableIDToCopy, Int32 pNumberOfDuplicates)
        {
            CvwReceivables objCvwReceivables = new CvwReceivables();
            CReceivables objCReceivables = new CReceivables();
            Int64 pOperationID = 0;
            int _RowCount = 0;
            objCvwReceivables.GetListPaging(999999, 1, "WHERE ID=" + pReceivableIDToCopy, "ID", out _RowCount);
            pOperationID = objCvwReceivables.lstCVarvwReceivables[0].OperationID;
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

                objCVarReceivables.AmountWithoutVAT = Math.Round((objCVarReceivables.Quantity * objCVarReceivables.SalePrice),2 );
                objCVarReceivables.TaxeTypeID = objCvwReceivables.lstCVarvwReceivables[0].TaxTypeID;
                objCVarReceivables.TaxPercentage = objCvwReceivables.lstCVarvwReceivables[0].TaxPercentage;
                objCVarReceivables.TaxAmount = objCvwReceivables.lstCVarvwReceivables[0].TaxAmount;
                objCVarReceivables.DiscountTypeID = objCvwReceivables.lstCVarvwReceivables[0].DiscountTypeID;
                objCVarReceivables.DiscountPercentage = objCvwReceivables.lstCVarvwReceivables[0].DiscountPercentage;
                objCVarReceivables.DiscountAmount = objCvwReceivables.lstCVarvwReceivables[0].DiscountAmount;

                objCVarReceivables.SaleAmount = objCvwReceivables.lstCVarvwReceivables[0].SaleAmount;
                objCVarReceivables.InvoiceID = 0;
                objCVarReceivables.AccNoteID = 0;
                objCVarReceivables.ExchangeRate = objCvwReceivables.lstCVarvwReceivables[0].ExchangeRate;
                objCVarReceivables.CurrencyID = objCvwReceivables.lstCVarvwReceivables[0].CurrencyID;
                objCVarReceivables.ExchangeRate_Foreign = objCvwReceivables.lstCVarvwReceivables[0].ExchangeRate_Foreign;
                objCVarReceivables.CurrencyID_Foreign = objCvwReceivables.lstCVarvwReceivables[0].CurrencyID_Foreign;

                objCVarReceivables.IsReceipt = objCvwReceivables.lstCVarvwReceivables[0].IsReceipt;
                objCVarReceivables.GeneratingQRID = 0;
                objCVarReceivables.Notes = "COPIED";

                objCVarReceivables.IssueDate = DateTime.Now;
                objCVarReceivables.OperationContainersAndPackagesID = 0;

                objCVarReceivables.ViewOrder = objCvwReceivables.lstCVarvwReceivables[0].ViewOrder;
                objCVarReceivables.IsDeleted = false;
                objCVarReceivables.CreatorUserID = objCVarReceivables.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarReceivables.CreationDate = objCVarReceivables.ModificationDate = DateTime.Now;
                objCVarReceivables.PayableID = 0;
                objCVarReceivables.OperationVehicleID = objCvwReceivables.lstCVarvwReceivables[0].OperationVehicleID;
                objCVarReceivables.TruckingOrderID = objCvwReceivables.lstCVarvwReceivables[0].TruckingOrderID;

                objCVarReceivables.PreviousCutOffDate = DateTime.Parse("01/01/1900");
                objCVarReceivables.CutOffDate = DateTime.Parse("01/01/1900");
                objCVarReceivables.ReceiptDate = objCvwReceivables.lstCVarvwReceivables[0].ReceiptDate;
                objCVarReceivables.ReceiptNo = objCvwReceivables.lstCVarvwReceivables[0].ReceiptNo;

                objCReceivables.lstCVarReceivables.Add(objCVarReceivables);
            }
            objCReceivables.SaveMethod(objCReceivables.lstCVarReceivables);

            objCvwReceivables.GetListPaging(999999, 1, "WHERE IsDeleted=0 AND (OperationID=" + pOperationID + " OR MasterOperationID=" + pOperationID + ")", "ChargeTypeName", out _RowCount);

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };

            return new object[] {
                serializer.Serialize(objCvwReceivables.lstCVarvwReceivables)
            };
        }

        [HttpGet, HttpPost]
        public object[] PrintReceivable(Int64 pReceivableIDToPrint, Int32 pOperationID)
        {
            CvwReceivables objCvwReceivables = new CvwReceivables();
            CvwOperations objCvwOperations = new CvwOperations();
            int _RowCount = 0;
            Exception checkException = null;
            checkException = objCvwOperations.GetListPaging(1, 1, "WHERE ID=" + pOperationID.ToString(), "ID", out _RowCount);
            checkException = objCvwReceivables.GetListPaging(1, 1, "WHERE ID=" + pReceivableIDToPrint.ToString(), "ID", out _RowCount);

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                serializer.Serialize(objCvwReceivables.lstCVarvwReceivables[0])
                , new JavaScriptSerializer().Serialize(objCvwOperations.lstCVarvwOperations[0])
            };
        }

        [HttpGet, HttpPost]
        public bool ApplyDefaultReceivables(Int64 pOperationID, string pWhereClause)
        {
            bool _result = false;
            Exception checkException = null;
            //string pReceivableIDsToDelete = "";
            CReceivables objCReceivables = new CReceivables();

            //checkException = objCReceivables.GetList(" WHERE OperationID = " + pOperationID.ToString() + " AND InvoiceID IS NULL AND AccNoteID IS NULL ");
            //if (objCReceivables.lstCVarReceivables.Count > 0)
            //{
            //    pReceivableIDsToDelete = objCReceivables.lstCVarReceivables[0].ID.ToString();
            //    for (int i = 1; i < objCReceivables.lstCVarReceivables.Count; i++)
            //        pReceivableIDsToDelete += "," + objCReceivables.lstCVarReceivables[i].ID.ToString();
            //}
            //if (pReceivableIDsToDelete != "")
            //    Delete(pReceivableIDsToDelete, pOperationID);
            //objCReceivables.lstCVarReceivables.Clear();
            ////checkException = objCReceivables.DeleteList(" WHERE OperationID = " + pOperationID.ToString() + " AND InvoiceID IS NULL ");
            int _RowCount = 0;
            if (checkException == null)
            {
                //those 2 lines are to get the Charge types from DB to get the default currency
                CDefaults objCDefaults = new CDefaults();
                objCDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount); //i am sure i ve just one row isa

                //those 2 lines are to get the default charge types from DB
                CChargeTypes objCChargeTypes = new CChargeTypes();
                //objCChargeTypes.GetList(pWhereClause);
                objCChargeTypes.GetListPaging(1500, 1, pWhereClause, "ID", out _RowCount);

                //CVarReceivables objCVarReceivables = new CVarReceivables();

                foreach (var rowChargeType in objCChargeTypes.lstCVarChargeTypes)
                {
                    CVarReceivables objCVarReceivables = new CVarReceivables();

                    objCVarReceivables.OperationID = pOperationID;
                    objCVarReceivables.ChargeTypeID = rowChargeType.ID;
                    objCVarReceivables.Quantity = 1;
                    objCVarReceivables.MeasurementID = rowChargeType.MeasurementID;
                    objCVarReceivables.ExchangeRate = 1;
                    objCVarReceivables.CurrencyID = objCDefaults.lstCVarDefaults[0].CurrencyID;
                    objCVarReceivables.ExchangeRate_Foreign = 1;
                    objCVarReceivables.CurrencyID_Foreign = objCDefaults.lstCVarDefaults[0].CurrencyID;

                    objCVarReceivables.Notes = rowChargeType.Notes;

                    objCVarReceivables.IssueDate = DateTime.Now;
                    objCVarReceivables.OperationContainersAndPackagesID = 0;

                    objCVarReceivables.PreviousCutOffDate = DateTime.Parse("01/01/1900");
                    objCVarReceivables.CutOffDate = DateTime.Parse("01/01/1900");
                    objCVarReceivables.ReceiptDate = DateTime.Parse("01/01/1900");
                    objCVarReceivables.ReceiptNo = "";

                    objCVarReceivables.CreatorUserID = objCVarReceivables.ModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarReceivables.ModificationDate = objCVarReceivables.CreationDate = DateTime.Now;

                    objCReceivables.lstCVarReceivables.Add(objCVarReceivables);
                }
                checkException = objCReceivables.SaveMethod(objCReceivables.lstCVarReceivables);
                if (checkException == null)
                    _result = true;
            }
            return _result;
        }

        [HttpGet, HttpPost]
        // i called the pWhereClause1 instead of pWhereClause to avoid the error of routing when 2 actions have the same signature
        public bool GenerateReceivablesFromQuotation(Int64 pOperationID, string pWhereClause1, Int64 pQuotationRouteID)
        {
            bool _result = false;
            Exception checkException = null;
            //string pReceivableIDsToDelete = "";
            CReceivables objCReceivables = new CReceivables();

            //checkException = objCReceivables.GetList(" WHERE OperationID = " + pOperationID.ToString() + " AND InvoiceID IS NULL ");
            //if (objCReceivables.lstCVarReceivables.Count > 0)
            //{
            //    pReceivableIDsToDelete = objCReceivables.lstCVarReceivables[0].ID.ToString();
            //    for (int i = 1; i < objCReceivables.lstCVarReceivables.Count; i++)
            //        pReceivableIDsToDelete += "," + objCReceivables.lstCVarReceivables[i].ID.ToString();
            //}
            //if (pReceivableIDsToDelete != "")
            //    Delete(pReceivableIDsToDelete, pOperationID);
            //objCReceivables.lstCVarReceivables.Clear();
            ////objCReceivables.DeleteList(" WHERE OperationID = " + pOperationID.ToString() + " AND InvoiceID IS NULL ");
            if (checkException == null)
            {
                //those 2 lines are to get the QuotationCharges from DB
                CQuotationCharges objCQuotationCharges = new CQuotationCharges();
                //objCQuotationCharges.GetList(pWhereClause1);
                int _RowCount = 0;
                checkException = objCQuotationCharges.GetListPaging(5000, 1, pWhereClause1, " ID ", out _RowCount);

                //CVarReceivables objCVarReceivables = new CVarReceivables();

                foreach (var rowChargeType in objCQuotationCharges.lstCVarQuotationCharges)
                {
                    CVarReceivables objCVarReceivables = new CVarReceivables();

                    objCVarReceivables.OperationID = pOperationID;
                    objCVarReceivables.ChargeTypeID = rowChargeType.ChargeTypeID;
                    objCVarReceivables.POrC = 0;
                    objCVarReceivables.SupplierID = 0;
                    objCVarReceivables.MeasurementID = rowChargeType.MeasurementID;
                    objCVarReceivables.ContainerTypeID = rowChargeType.ContainerTypeID;
                    objCVarReceivables.PackageTypeID = rowChargeType.PackageTypeID;
                    objCVarReceivables.Quantity = rowChargeType.CostQuantity == 0 ? 1 : rowChargeType.CostQuantity;
                    //objCVarReceivables.CostPrice = rowChargeType.CostPrice;
                    //objCVarReceivables.CostAmount = rowChargeType.CostAmount;
                    objCVarReceivables.SalePrice = rowChargeType.SalePrice;

                    objCVarReceivables.AmountWithoutVAT = Math.Round((objCVarReceivables.Quantity * objCVarReceivables.SalePrice),2 ); //CostQuantity=SaleQuantity

                    objCVarReceivables.SaleAmount = rowChargeType.SaleAmount;
                    objCVarReceivables.CurrencyID = rowChargeType.SaleCurrencyID;
                    objCVarReceivables.ExchangeRate = rowChargeType.SaleExchangeRate;
                    objCVarReceivables.GeneratingQRID = pQuotationRouteID;
                    objCVarReceivables.Notes = rowChargeType.Notes;

                    objCVarReceivables.IssueDate = DateTime.Now;
                    objCVarReceivables.OperationContainersAndPackagesID = 0;

                    objCVarReceivables.PreviousCutOffDate = DateTime.Parse("01/01/1900");
                    objCVarReceivables.CutOffDate = DateTime.Parse("01/01/1900");
                    objCVarReceivables.ReceiptDate = DateTime.Parse("01/01/1900");
                    objCVarReceivables.ReceiptNo = "";

                    objCVarReceivables.CreatorUserID = objCVarReceivables.ModificatorUserID = WebSecurity.CurrentUserId;
                    objCVarReceivables.ModificationDate = objCVarReceivables.CreationDate = DateTime.Now;

                    objCReceivables.lstCVarReceivables.Add(objCVarReceivables);
                }
                checkException = objCReceivables.SaveMethod(objCReceivables.lstCVarReceivables);
                if (checkException == null)
                {
                    _result = true;
                    COperations objCOperations = new COperations();
                    checkException = objCOperations.UpdateList("QuotationRouteID=" + pQuotationRouteID + " WHERE ID=" + pOperationID);
                }
            }
            return _result;
        }

        //[HttpGet, HttpPost]
        //public bool GenerateReceivablesFromPayables(string pWhereClause1)
        //{
        //    bool _result = false;

        //    CReceivables objCReceivables = new CReceivables();
        //    Exception checkException = objCReceivables.DeleteList(pWhereClause1 + " AND InvoiceID IS NULL ");
        //    if (checkException == null)
        //    {
        //        //those 2 lines are to get the Payables from DB
        //        CPayables objCPayables = new CPayables();
        //        objCPayables.GetList(pWhereClause1);

        //        //CVarReceivables objCVarReceivables = new CVarReceivables();

        //        foreach (var rowChargeType in objCPayables.lstCVarPayables)
        //        {
        //            CVarReceivables objCVarReceivables = new CVarReceivables();

        //            objCVarReceivables.OperationID = rowChargeType.OperationID;
        //            objCVarReceivables.ChargeTypeID = rowChargeType.ChargeTypeID;
        //            objCVarReceivables.POrC = rowChargeType.POrC;
        //            objCVarReceivables.MeasurementID = rowChargeType.MeasurementID;
        //            //objCVarReceivables.SupplierID = rowChargeType.SupplierID;
        //            objCVarReceivables.ContainerTypeID = rowChargeType.ContainerTypeID;
        //            //objCVarReceivables.PackageTypeID = rowChargeType.PackageTypeID;
        //            objCVarReceivables.Quantity = rowChargeType.Quantity;
        //            objCVarReceivables.CostPrice = rowChargeType.CostPrice;
        //            objCVarReceivables.CostAmount = rowChargeType.CostAmount;
        //            objCVarReceivables.SalePrice = 0;
        //            objCVarReceivables.SaleAmount = 0;
        //            objCVarReceivables.ExchangeRate = rowChargeType.ExchangeRate;
        //            objCVarReceivables.CurrencyID = rowChargeType.CurrencyID;
        //            objCVarReceivables.Notes = "0";

        //            objCVarReceivables.CreatorUserID = objCVarReceivables.ModificatorUserID = WebSecurity.CurrentUserId;
        //            objCVarReceivables.ModificationDate = objCVarReceivables.CreationDate = DateTime.Now;

        //            objCReceivables.lstCVarReceivables.Add(objCVarReceivables);
        //        }
        //        checkException = objCReceivables.SaveMethod(objCReceivables.lstCVarReceivables);
        //        if (checkException == null)
        //            _result = true;
        //    }
        //    return _result;
        //}

        [HttpGet, HttpPost]
        public object[] GetReceivablesSubtotals(Int64 pOperationID)
        {
            Forwarding.MvcApp.Models.Operations.Operations.Customized.CvwReceivablesSubTotals objCvwReceivablesSubTotals = new Forwarding.MvcApp.Models.Operations.Operations.Customized.CvwReceivablesSubTotals();
            objCvwReceivablesSubTotals.GetList_Customized(" WHERE OperationID = " + pOperationID.ToString() + " OR MasterOperationID = " + pOperationID.ToString() + " GROUP BY CurrencyCode ORDER BY CurrencyCode ");// the condition of isdeleted = 0 is in the view
            Int32 _RowCount = objCvwReceivablesSubTotals.lstCVarvwReceivablesSubTotals.Count;

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] { serializer.Serialize(objCvwReceivablesSubTotals.lstCVarvwReceivablesSubTotals), _RowCount };
        }

        [HttpGet, HttpPost]
        public Int32 GetDefaultTaxTypeID(Int32 pChargeTypeIDToGetDefaultTax)
        {
            CChargeTypes objCChargeTypes = new Models.MasterData.Invoicing.Generated.CChargeTypes();
            objCChargeTypes.GetList("WHERE ID=" + pChargeTypeIDToGetDefaultTax);
            return objCChargeTypes.lstCVarChargeTypes[0].TaxeTypeID;
        }
       
    }

    public class UpdateListParameters
    {
        public string pSelectedReceivablesIDsToUpdate { get; set; }
        public string pPOrCList { get; set; }
        public string pUOMList { get; set; }
        public string pQuantityList { get; set; }
        public string pSalePriceList { get; set; }
        public string pAmountWithoutVATList { get; set; }
        public string pTaxTypeIDList { get; set; }
        public string pTaxPercentageList { get; set; }
        public string pTaxAmountList { get; set; }
        public string pDiscountTypeIDList { get; set; }
        public string pDiscountPercentageList { get; set; }
        public string pDiscountAmountList { get; set; }
        public string pSaleAmountList { get; set; }
        public string pExchangeRateList { get; set; }
        public string pCurrencyList { get; set; }
        public string pReceiptNoList { get; set; }
        public string pReceiptDateList { get; set; }
        public string pNotesList { get; set; }
        public string pViewOrderList { get; set; }
        public Int64 pInvoiceID { get; set; }
    }

}
