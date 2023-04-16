using Forwarding.MvcApp.Entities.Quotations;
using Forwarding.MvcApp.Models.Accounting.MasterData.Customized;
using Forwarding.MvcApp.Models.Accounting.MasterData.Generated;
using Forwarding.MvcApp.Models.Administration.Security.Generated;
using Forwarding.MvcApp.Models.Administration.Settings.Generated;
using Forwarding.MvcApp.Models.MasterData.Invoicing.Generated;
using Forwarding.MvcApp.Models.NoAccess.Generated;

using Forwarding.MvcApp.Models.Operations.Operations.Generated;
using Forwarding.MvcApp.Models.Quotations.Quotations.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebMatrix.WebData;

namespace Forwarding.MvcApp.Controllers.MasterData.API_Locations
{
    public class ChargeTypesController : ApiController
    {
        [HttpGet, HttpPost]
        public Object[] LoadAll(string pWhereClause)
        {
            Int32 _RowCount = 0;
            CDefaults objCDefaults = new CDefaults();
            CUsers objCUsers = new CUsers();
            objCDefaults.GetListPaging(999999, 1, "WHERE 1=1", "ID", out _RowCount);
            objCUsers.GetListPaging(999999, 1, "WHERE ID=" + WebSecurity.CurrentUserId, "ID", out _RowCount);
            if (!objCUsers.lstCVarUsers[0].IsAccessAllCharges)
                pWhereClause += " AND ID IN (SELECT ChargeTypeID FROM DepartmentCharge WHERE DepartmentID=" + objCUsers.lstCVarUsers[0].DepartmentID + ") \n";

            CvwChargeTypes objCvwChargeTypes = new CvwChargeTypes();
            objCvwChargeTypes.GetListPaging(999999, 1, pWhereClause, "Name", out _RowCount);
            CNoAccessMeasurements objCNoAccessMeasurements = new CNoAccessMeasurements();
            objCNoAccessMeasurements.GetList("WHERE 1=1");
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] {
                serializer.Serialize(objCvwChargeTypes.lstCVarvwChargeTypes)
                , serializer.Serialize(objCNoAccessMeasurements.lstCVarNoAccessMeasurements) //pData[1]
            };
        }

        [HttpGet, HttpPost]
        public Object[] LoadAllWithMinimalColumns(string pWhereClauseWithMinimalColumns)
        {
            Int32 _RowCount = 0;
            CDefaults objCDefaults = new CDefaults();
            CUsers objCUsers = new CUsers();
            objCDefaults.GetListPaging(999999, 1, "WHERE 1=1", "ID", out _RowCount);
            objCUsers.GetListPaging(999999, 1, "WHERE ID=" + WebSecurity.CurrentUserId, "ID", out _RowCount);
            if (!objCUsers.lstCVarUsers[0].IsAccessAllCharges)
                pWhereClauseWithMinimalColumns += " AND ID IN (SELECT ChargeTypeID FROM DepartmentCharge WHERE DepartmentID=" + objCUsers.lstCVarUsers[0].DepartmentID + ") \n";

            CvwChargeTypesWithMinimalColumns objCvwChargeTypes = new CvwChargeTypesWithMinimalColumns();
            objCvwChargeTypes.GetListPaging(999999, 1, pWhereClauseWithMinimalColumns, "Name", out _RowCount);
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new Object[] { serializer.Serialize(objCvwChargeTypes.lstCVarvwChargeTypesWithMinimalColumns) };
        }

        // [Route("/api/ChargeTypes/LoadWithPaging/{pPageNumber}/{pPageSize}")]
        [HttpGet, HttpPost]
        public Object[] LoadWithPaging(Int32 pPageNumber, Int32 pPageSize, string pSearchKey)
        {
            CvwChargeTypes objCvwChargeTypes = new CvwChargeTypes();
            //objCvwChargeTypes.GetList(string.Empty);
            Int32 _RowCount = 0;// objCvwChargeTypes.lstCVarChargeTypes.Count;

            pSearchKey = (pSearchKey == null ? "" : pSearchKey.Trim());
            string whereClause = " Where (Code LIKE '%" + pSearchKey + "%' "
                + " OR Name LIKE N'%" + pSearchKey + "%' "
                + " OR PreCode LIKE N'%" + pSearchKey + "%' "
                + " OR LocalName LIKE N'%" + pSearchKey + "%' "
                + " OR MeasurementCode LIKE N'%" + pSearchKey + "%' " //sherif: i used name here coz code is vague for me
                + " OR TaxeTypeCode LIKE N'%" + pSearchKey + "%' "
                + " OR InvoiceTypeCode LIKE N'%" + pSearchKey + "%') ";

            objCvwChargeTypes.GetListPaging(pPageSize, pPageNumber, whereClause, " ChargeTypeName ", out _RowCount);

            return new Object[] { new JavaScriptSerializer().Serialize(objCvwChargeTypes.lstCVarvwChargeTypes), _RowCount };
        }

        // [Route("/api/ChargeTypes/Insert/{pCode}/{pModificatorUser}/{pLocalName}}")]
        [HttpGet, HttpPost]
        public bool Insert(Int32 pMeasurementID, Int32 pTaxeTypeID, Int32 pInvoiceTypeID, String pCode, String pPreCode, String pName, String pLocalName, int pViewOrder, string pNotes, bool pIsUsedInReceivable, bool pIsUsedInPayable, bool pIsTank, bool pIsOcean, bool pIsAir, bool pIsInland, bool pIsDefaultInQuotation, bool pIsDefaultInOperations, bool pIsInactive /*, bool pIsAddedManually*/, bool pIsGeneralChargeType, bool pIsWarehouseChargeType, bool pIsOperationChargeType, bool pIsOfficial
            , Int32 pAccountID_Return, int pSubAccountID_Return
            , Int32 pAccountID_Revenue, int pSubAccountID_Revenue, int pCostCenterID_Revenue
            , Int32 pAccountID_Expense, Int32 pSubAccountID_Expense, Int32 pCostCenterID_Expense
            , Int32 pTemplateID, Int32 pChargeTypeGroupID, Int32 pPurchaseItemID, decimal pCost)
        {
            bool _result = false;
            CVarChargeTypes objCVarChargeTypes = new CVarChargeTypes();

            objCVarChargeTypes.MeasurementID = pMeasurementID;
            objCVarChargeTypes.TaxeTypeID = pTaxeTypeID;
            objCVarChargeTypes.InvoiceTypeID = pInvoiceTypeID;

            objCVarChargeTypes.Code = pCode;
            objCVarChargeTypes.PreCode = pPreCode;
            objCVarChargeTypes.Name = pName;
            objCVarChargeTypes.LocalName = (pLocalName == null ? "" : pLocalName);
            objCVarChargeTypes.ViewOrder = pViewOrder;
            objCVarChargeTypes.IsAddedManually = true;
            objCVarChargeTypes.IsUsedInPayable = pIsUsedInPayable;
            objCVarChargeTypes.IsUsedInReceivable = pIsUsedInReceivable;
            objCVarChargeTypes.IsTank = pIsTank;
            objCVarChargeTypes.IsOcean = pIsOcean;
            objCVarChargeTypes.IsInland = pIsInland;
            objCVarChargeTypes.IsAir = pIsAir;
            objCVarChargeTypes.IsDefaultInQuotation = pIsDefaultInQuotation;
            objCVarChargeTypes.IsDefaultInOperations = pIsDefaultInOperations;
            objCVarChargeTypes.IsGeneralChargeType = pIsGeneralChargeType;
            objCVarChargeTypes.IsWarehouseChargeType = pIsWarehouseChargeType;
            objCVarChargeTypes.IsOperationChargeType = pIsOperationChargeType;
            objCVarChargeTypes.IsOfficial = pIsOfficial;

            objCVarChargeTypes.AccountID_Return = pAccountID_Return;
            objCVarChargeTypes.SubAccountID_Return = pSubAccountID_Return;
            objCVarChargeTypes.AccountID_Revenue = pAccountID_Revenue;
            objCVarChargeTypes.SubAccountID_Revenue = pSubAccountID_Revenue;
            objCVarChargeTypes.CostCenterID_Revenue = pCostCenterID_Revenue;
            objCVarChargeTypes.AccountID_Expense = pAccountID_Expense;
            objCVarChargeTypes.SubAccountID_Expense = pSubAccountID_Expense;
            objCVarChargeTypes.CostCenterID_Expense = pCostCenterID_Expense;

            objCVarChargeTypes.IsInactive = pIsInactive;
            objCVarChargeTypes.TimeLocked = DateTime.Parse("01-01-1900");
            objCVarChargeTypes.LockingUserID = 0;

            objCVarChargeTypes.Notes = (pNotes == null ? "" : pNotes);
            objCVarChargeTypes.TemplateID = pTemplateID;
            objCVarChargeTypes.ChargeTypeGroupID = pChargeTypeGroupID;
            objCVarChargeTypes.PurchaseItemID = pPurchaseItemID;

            objCVarChargeTypes.Cost = pCost;

            objCVarChargeTypes.CreatorUserID = objCVarChargeTypes.ModificatorUserID = WebSecurity.CurrentUserId;
            objCVarChargeTypes.CreationDate = objCVarChargeTypes.ModificationDate = DateTime.Now;

            CChargeTypes objCChargeTypes = new CChargeTypes();
            objCChargeTypes.lstCVarChargeTypes.Add(objCVarChargeTypes);
            Exception checkException = objCChargeTypes.SaveMethod(objCChargeTypes.lstCVarChargeTypes);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
                _result = true;

            int _RowCount2 = 0;
            Int32 SubAccountID_Return = 0;
            Int32 SubAccountID_Revenue = 0;
            Int32 SubAccountID_Expense = 0;

            Int32 supGroupID = 0;
            Int32 AccountID_Return = 0;
            Int32 AccountID_Revenue = 0;
            Int32 AccountID_Expense = 0;


            CvwDefaults objCvwDefaults = new CvwDefaults();
            objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount2); //i am sure i ve just one row isa
            string CompanyName = objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName;

            CChargeTypesTAX objCChargeTypesTAX = new CChargeTypesTAX();
            objCChargeTypesTAX.GetList("WHERE Name=N'" + pName.Trim().ToUpper() + "'");

            CA_SubAccountsTAX objCA_SubAccountsTAXOther = new CA_SubAccountsTAX(); //get the parent details
            CA_SubAccounts_DetailsTAX objCA_SubAccounts_DetailsTAXOther = new CA_SubAccounts_DetailsTAX(); //get the parent details
            CA_AccountsTAX objCAccountsTAX = new CA_AccountsTAX(); //get the parent details
            #region Tax
            #region SubAccount
            //SubAccountID_Return
            CA_SubAccounts objCA_SubAccountsSubAccountID_Return = new CA_SubAccounts(); //get the parent details
            checkException = objCA_SubAccountsSubAccountID_Return.GetListPaging(9999, 1, "WHERE ID = " + pSubAccountID_Return, "ID", out _RowCount2);

            //subAccountID_Revenue
            CA_SubAccounts objCA_SubAccountsSubAccountID_Revenue = new CA_SubAccounts(); //get the parent details
            checkException = objCA_SubAccountsSubAccountID_Revenue.GetListPaging(9999, 1, "WHERE ID = " + pSubAccountID_Revenue, "ID", out _RowCount2);

            //SubAccountID_Expense
            CA_SubAccounts objCA_SubAccountsSubAccountID_Expense = new CA_SubAccounts(); //get the parent details
            checkException = objCA_SubAccountsSubAccountID_Expense.GetListPaging(9999, 1, "WHERE ID = " + pSubAccountID_Expense, "ID", out _RowCount2);



            if (objCA_SubAccountsSubAccountID_Return.lstCVarA_SubAccounts.Count > 0)
            {
                checkException = objCA_SubAccountsTAXOther.GetListPaging(9999, 1, "WHERE SubAccount_Name = N'" + objCA_SubAccountsSubAccountID_Return.lstCVarA_SubAccounts[0].SubAccount_Name + "'", "ID", out _RowCount2);
                if (objCA_SubAccountsTAXOther.lstCVarA_SubAccounts.Count > 0)
                {
                    SubAccountID_Return = objCA_SubAccountsTAXOther.lstCVarA_SubAccounts[0].ID;

                }
            }
            if (objCA_SubAccountsSubAccountID_Revenue.lstCVarA_SubAccounts.Count > 0)
            {
                checkException = objCA_SubAccountsTAXOther.GetListPaging(9999, 1, "WHERE SubAccount_Name = N'" + objCA_SubAccountsSubAccountID_Revenue.lstCVarA_SubAccounts[0].SubAccount_Name + "'", "ID", out _RowCount2);
                if (objCA_SubAccountsTAXOther.lstCVarA_SubAccounts.Count > 0)
                {
                    SubAccountID_Revenue = objCA_SubAccountsTAXOther.lstCVarA_SubAccounts[0].ID;

                }
            }
            if (objCA_SubAccountsSubAccountID_Expense.lstCVarA_SubAccounts.Count > 0)
            {
                checkException = objCA_SubAccountsTAXOther.GetListPaging(9999, 1, "WHERE SubAccount_Name = N'" + objCA_SubAccountsSubAccountID_Expense.lstCVarA_SubAccounts[0].SubAccount_Name + "'", "ID", out _RowCount2);
                if (objCA_SubAccountsTAXOther.lstCVarA_SubAccounts.Count > 0)
                {
                    AccountID_Expense = objCA_SubAccountsTAXOther.lstCVarA_SubAccounts[0].ID;

                }
            }
            #endregion



            #region AccountID_Return
            //Account
            CA_Accounts objCACA_AccountsAccountID_Return = new CA_Accounts(); //get the parent details
            checkException = objCACA_AccountsAccountID_Return.GetListPaging(9999, 1, "WHERE ID = " + pAccountID_Return, "ID", out _RowCount2);

            //AccountID_Revenue
            CA_Accounts objCACA_AccountsAccountID_Revenue = new CA_Accounts(); //get the parent details
            checkException = objCACA_AccountsAccountID_Revenue.GetListPaging(9999, 1, "WHERE ID = " + pAccountID_Revenue, "ID", out _RowCount2);

            //AccountID_Expense
            CA_Accounts objCACA_AccountsAccountID_Expense = new CA_Accounts(); //get the parent details
            checkException = objCACA_AccountsAccountID_Expense.GetListPaging(9999, 1, "WHERE ID = " + pAccountID_Expense, "ID", out _RowCount2);

            if (objCACA_AccountsAccountID_Return.lstCVarA_Accounts.Count > 0)
            {
                checkException = objCAccountsTAX.GetListPaging(9999, 1, "WHERE Account_Name = N'" + objCACA_AccountsAccountID_Return.lstCVarA_Accounts[0].Account_Name + "'", "ID", out _RowCount2);
                if (objCAccountsTAX.lstCVarA_Accounts.Count > 0)
                {
                    AccountID_Return = objCAccountsTAX.lstCVarA_Accounts[0].ID;
                }

            }
            if (objCACA_AccountsAccountID_Revenue.lstCVarA_Accounts.Count > 0)
            {
                checkException = objCAccountsTAX.GetListPaging(9999, 1, "WHERE Account_Name = N'" + objCACA_AccountsAccountID_Revenue.lstCVarA_Accounts[0].Account_Name + "'", "ID", out _RowCount2);
                if (objCAccountsTAX.lstCVarA_Accounts.Count > 0)
                {
                    AccountID_Revenue = objCAccountsTAX.lstCVarA_Accounts[0].ID;
                }

            }
            if (objCACA_AccountsAccountID_Expense.lstCVarA_Accounts.Count > 0)
            {
                checkException = objCAccountsTAX.GetListPaging(9999, 1, "WHERE Account_Name = N'" + objCACA_AccountsAccountID_Expense.lstCVarA_Accounts[0].Account_Name + "'", "ID", out _RowCount2);
                if (objCAccountsTAX.lstCVarA_Accounts.Count > 0)
                {
                    AccountID_Expense = objCAccountsTAX.lstCVarA_Accounts[0].ID;
                }

            }
            #endregion




            if ((CompanyName == "CHM" || CompanyName == "OCE") && checkException == null)
            {
                CVarChargeTypesTAX objCVarChargeTypesTax = new CVarChargeTypesTAX();

                objCVarChargeTypesTax.MeasurementID = pMeasurementID;
                objCVarChargeTypesTax.TaxeTypeID = pTaxeTypeID;
                objCVarChargeTypesTax.InvoiceTypeID = pInvoiceTypeID;

                objCVarChargeTypesTax.Code = pCode;
                objCVarChargeTypesTax.PreCode = pPreCode;
                objCVarChargeTypesTax.Name = pName;
                objCVarChargeTypesTax.LocalName = (pLocalName == null ? "" : pLocalName);
                objCVarChargeTypesTax.ViewOrder = pViewOrder;
                objCVarChargeTypesTax.IsAddedManually = true;
                objCVarChargeTypesTax.IsUsedInPayable = pIsUsedInPayable;
                objCVarChargeTypesTax.IsUsedInReceivable = pIsUsedInReceivable;
                objCVarChargeTypesTax.IsTank = pIsTank;
                objCVarChargeTypesTax.IsOcean = pIsOcean;
                objCVarChargeTypesTax.IsInland = pIsInland;
                objCVarChargeTypesTax.IsAir = pIsAir;
                objCVarChargeTypesTax.IsDefaultInQuotation = pIsDefaultInQuotation;
                objCVarChargeTypesTax.IsDefaultInOperations = pIsDefaultInOperations;
                objCVarChargeTypesTax.IsGeneralChargeType = pIsGeneralChargeType;
                objCVarChargeTypesTax.IsWarehouseChargeType = pIsWarehouseChargeType;
                objCVarChargeTypesTax.IsOperationChargeType = pIsOperationChargeType;
                objCVarChargeTypesTax.IsOfficial = pIsOfficial;

                objCVarChargeTypesTax.AccountID_Return = AccountID_Return;
                objCVarChargeTypesTax.SubAccountID_Return = SubAccountID_Return;
                objCVarChargeTypesTax.AccountID_Revenue = AccountID_Revenue;
                objCVarChargeTypesTax.SubAccountID_Revenue = SubAccountID_Revenue;
                objCVarChargeTypesTax.CostCenterID_Revenue = 0;
                objCVarChargeTypesTax.AccountID_Expense = AccountID_Expense;
                objCVarChargeTypesTax.SubAccountID_Expense = SubAccountID_Expense;
                objCVarChargeTypesTax.CostCenterID_Expense = 0;

                objCVarChargeTypesTax.IsInactive = pIsInactive;
                objCVarChargeTypesTax.TimeLocked = DateTime.Parse("01-01-1900");
                objCVarChargeTypesTax.LockingUserID = 0;

                objCVarChargeTypesTax.Notes = (pNotes == null ? "" : pNotes);
                objCVarChargeTypesTax.TemplateID = pTemplateID;
                objCVarChargeTypesTax.ChargeTypeGroupID = pChargeTypeGroupID;
                objCVarChargeTypesTax.PurchaseItemID = pPurchaseItemID;

                objCVarChargeTypesTax.Cost = pCost;

                objCVarChargeTypesTax.CreatorUserID = objCVarChargeTypesTax.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarChargeTypesTax.CreationDate = objCVarChargeTypesTax.ModificationDate = DateTime.Now;

                CChargeTypesTAX objCChargeTypesTax = new CChargeTypesTAX();
                objCChargeTypesTax.lstCVarChargeTypes.Add(objCVarChargeTypesTax);
                checkException = objCChargeTypesTax.SaveMethod(objCChargeTypesTax.lstCVarChargeTypes);
                if (checkException != null) // an exception is caught in the model
                {
                    if (checkException.Message.Contains("UNIQUE"))
                        _result = false;
                }
                else //not unique
                    _result = true;
            }
            #endregion
            return _result;
        }
        
        [HttpGet, HttpPost]
        public bool Update(Int32 pID, Int32 pMeasurementID, Int32 pTaxeTypeID, Int32 pInvoiceTypeID, String pCode, String pPreCode, String pName, String pLocalName, int pViewOrder, string pNotes, bool pIsUsedInReceivable, bool pIsUsedInPayable, bool pIsTank, bool pIsOcean, bool pIsAir, bool pIsInland, bool pIsDefaultInQuotation, bool pIsDefaultInOperations, bool pIsInactive /*, bool pIsAddedManually*/, bool pIsGeneralChargeType, bool pIsWarehouseChargeType, bool pIsOperationChargeType, bool pIsOfficial
            , Int32 pAccountID_Return, int pSubAccountID_Return
            , Int32 pAccountID_Revenue, Int32 pSubAccountID_Revenue, Int32 pCostCenterID_Revenue
            , Int32 pAccountID_Expense, Int32 pSubAccountID_Expense, Int32 pCostCenterID_Expense
            , Int32 pTemplateID, Int32 pChargeTypeGroupID, Int32 pPurchaseItemID, decimal pCost)
        {
            bool _result = false;
            CVarChargeTypes objCVarChargeTypes = new CVarChargeTypes();

            //the next 4 lines to get the CreatorUserID and CreationDate and set them as they were entered before
            CChargeTypes objCGetCreationInformation = new CChargeTypes();
            objCGetCreationInformation.GetItem(pID);
            objCVarChargeTypes.IsAddedManually = objCGetCreationInformation.lstCVarChargeTypes[0].IsAddedManually;
            objCVarChargeTypes.CreatorUserID = objCGetCreationInformation.lstCVarChargeTypes[0].CreatorUserID;
            objCVarChargeTypes.CreationDate = objCGetCreationInformation.lstCVarChargeTypes[0].CreationDate;

            objCVarChargeTypes.ID = pID;
            objCVarChargeTypes.MeasurementID = pMeasurementID;
            objCVarChargeTypes.TaxeTypeID = pTaxeTypeID;
            objCVarChargeTypes.InvoiceTypeID = pInvoiceTypeID;

            objCVarChargeTypes.Code = pCode;
            objCVarChargeTypes.PreCode = pPreCode;
            objCVarChargeTypes.Name = pName;
            objCVarChargeTypes.LocalName = (pLocalName == null ? "" : pLocalName);
            objCVarChargeTypes.ViewOrder = pViewOrder;
            objCVarChargeTypes.IsUsedInPayable = pIsUsedInPayable;
            objCVarChargeTypes.IsUsedInReceivable = pIsUsedInReceivable;
            objCVarChargeTypes.IsTank = pIsTank;
            objCVarChargeTypes.IsOcean = pIsOcean;
            objCVarChargeTypes.IsInland = pIsInland;
            objCVarChargeTypes.IsAir = pIsAir;
            objCVarChargeTypes.IsDefaultInQuotation = pIsDefaultInQuotation;
            objCVarChargeTypes.IsDefaultInOperations = pIsDefaultInOperations;
            objCVarChargeTypes.IsGeneralChargeType = pIsGeneralChargeType;
            objCVarChargeTypes.IsWarehouseChargeType = pIsWarehouseChargeType;
            objCVarChargeTypes.IsOperationChargeType = pIsOperationChargeType;
            objCVarChargeTypes.IsOfficial = pIsOfficial;

            objCVarChargeTypes.AccountID_Return = pAccountID_Return;
            objCVarChargeTypes.SubAccountID_Return = pSubAccountID_Return;
            objCVarChargeTypes.AccountID_Revenue = pAccountID_Revenue;
            objCVarChargeTypes.SubAccountID_Revenue = pSubAccountID_Revenue;
            objCVarChargeTypes.CostCenterID_Revenue = pCostCenterID_Revenue;
            objCVarChargeTypes.AccountID_Expense = pAccountID_Expense;
            objCVarChargeTypes.SubAccountID_Expense = pSubAccountID_Expense;
            objCVarChargeTypes.CostCenterID_Expense = pCostCenterID_Expense;

            objCVarChargeTypes.IsInactive = pIsInactive;
            objCVarChargeTypes.TimeLocked = DateTime.Parse("01-01-1900");
            objCVarChargeTypes.LockingUserID = 0;

            objCVarChargeTypes.Notes = (pNotes == null ? "" : pNotes);
            objCVarChargeTypes.TemplateID = pTemplateID;
            objCVarChargeTypes.ChargeTypeGroupID = pChargeTypeGroupID;
            objCVarChargeTypes.PurchaseItemID = pPurchaseItemID;

            objCVarChargeTypes.Cost = pCost;

            objCVarChargeTypes.ModificatorUserID = WebSecurity.CurrentUserId;
            objCVarChargeTypes.ModificationDate = DateTime.Now;

            CChargeTypes objCChargeTypes = new CChargeTypes();
            objCChargeTypes.lstCVarChargeTypes.Add(objCVarChargeTypes);
            Exception checkException = objCChargeTypes.SaveMethod(objCChargeTypes.lstCVarChargeTypes);
            if (checkException != null) // an exception is caught in the model
            {
                if (checkException.Message.Contains("UNIQUE"))
                    _result = false;
            }
            else //not unique
                _result = true;

            #region tax
            int _RowCount2 = 0;
            Int32 SubAccountID_Return = 0;
            Int32 SubAccountID_Revenue = 0;
            Int32 SubAccountID_Expense = 0;

            Int32 supGroupID = 0;
            Int32 AccountID_Return = 0;
            Int32 AccountID_Revenue = 0;
            Int32 AccountID_Expense = 0;


            CvwDefaults objCvwDefaults = new CvwDefaults();
            objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount2); //i am sure i ve just one row isa
            string CompanyName = objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName;

            CChargeTypesTAX objCChargeTypesTAX = new CChargeTypesTAX();
            objCChargeTypesTAX.GetList("WHERE Name=N'" + pName.Trim().ToUpper() + "'");

            CA_SubAccountsTAX objCA_SubAccountsTAXOther = new CA_SubAccountsTAX(); //get the parent details
            CA_SubAccounts_DetailsTAX objCA_SubAccounts_DetailsTAXOther = new CA_SubAccounts_DetailsTAX(); //get the parent details
            CA_AccountsTAX objCAccountsTAX = new CA_AccountsTAX(); //get the parent details
       
            #region SubAccount
            //SubAccountID_Return
            CA_SubAccounts objCA_SubAccountsSubAccountID_Return = new CA_SubAccounts(); //get the parent details
            checkException = objCA_SubAccountsSubAccountID_Return.GetListPaging(9999, 1, "WHERE ID = " + pSubAccountID_Return, "ID", out _RowCount2);

            //subAccountID_Revenue
            CA_SubAccounts objCA_SubAccountsSubAccountID_Revenue = new CA_SubAccounts(); //get the parent details
            checkException = objCA_SubAccountsSubAccountID_Revenue.GetListPaging(9999, 1, "WHERE ID = " + pSubAccountID_Revenue, "ID", out _RowCount2);

            //SubAccountID_Expense
            CA_SubAccounts objCA_SubAccountsSubAccountID_Expense = new CA_SubAccounts(); //get the parent details
            checkException = objCA_SubAccountsSubAccountID_Expense.GetListPaging(9999, 1, "WHERE ID = " + pSubAccountID_Expense, "ID", out _RowCount2);



            if (objCA_SubAccountsSubAccountID_Return.lstCVarA_SubAccounts.Count > 0)
            {
                checkException = objCA_SubAccountsTAXOther.GetListPaging(9999, 1, "WHERE SubAccount_Name = N'" + objCA_SubAccountsSubAccountID_Return.lstCVarA_SubAccounts[0].SubAccount_Name + "'", "ID", out _RowCount2);
                if (objCA_SubAccountsTAXOther.lstCVarA_SubAccounts.Count > 0)
                {
                    SubAccountID_Return = objCA_SubAccountsTAXOther.lstCVarA_SubAccounts[0].ID;

                }
            }
            if (objCA_SubAccountsSubAccountID_Revenue.lstCVarA_SubAccounts.Count > 0)
            {
                checkException = objCA_SubAccountsTAXOther.GetListPaging(9999, 1, "WHERE SubAccount_Name = N'" + objCA_SubAccountsSubAccountID_Revenue.lstCVarA_SubAccounts[0].SubAccount_Name + "'", "ID", out _RowCount2);
                if (objCA_SubAccountsTAXOther.lstCVarA_SubAccounts.Count > 0)
                {
                    SubAccountID_Revenue = objCA_SubAccountsTAXOther.lstCVarA_SubAccounts[0].ID;

                }
            }
            if (objCA_SubAccountsSubAccountID_Expense.lstCVarA_SubAccounts.Count > 0)
            {
                checkException = objCA_SubAccountsTAXOther.GetListPaging(9999, 1, "WHERE SubAccount_Name = N'" + objCA_SubAccountsSubAccountID_Expense.lstCVarA_SubAccounts[0].SubAccount_Name + "'", "ID", out _RowCount2);
                if (objCA_SubAccountsTAXOther.lstCVarA_SubAccounts.Count > 0)
                {
                    AccountID_Expense = objCA_SubAccountsTAXOther.lstCVarA_SubAccounts[0].ID;

                }
            }
            #endregion



            #region AccountID_Return
            //Account
            CA_Accounts objCACA_AccountsAccountID_Return = new CA_Accounts(); //get the parent details
            checkException = objCACA_AccountsAccountID_Return.GetListPaging(9999, 1, "WHERE ID = " + pAccountID_Return, "ID", out _RowCount2);

            //AccountID_Revenue
            CA_Accounts objCACA_AccountsAccountID_Revenue = new CA_Accounts(); //get the parent details
            checkException = objCACA_AccountsAccountID_Revenue.GetListPaging(9999, 1, "WHERE ID = " + pAccountID_Revenue, "ID", out _RowCount2);

            //AccountID_Expense
            CA_Accounts objCACA_AccountsAccountID_Expense = new CA_Accounts(); //get the parent details
            checkException = objCACA_AccountsAccountID_Expense.GetListPaging(9999, 1, "WHERE ID = " + pAccountID_Expense, "ID", out _RowCount2);

            if (objCACA_AccountsAccountID_Return.lstCVarA_Accounts.Count > 0)
            {
                checkException = objCAccountsTAX.GetListPaging(9999, 1, "WHERE Account_Name = N'" + objCACA_AccountsAccountID_Return.lstCVarA_Accounts[0].Account_Name + "'", "ID", out _RowCount2);
                if (objCAccountsTAX.lstCVarA_Accounts.Count > 0)
                {
                    AccountID_Return = objCAccountsTAX.lstCVarA_Accounts[0].ID;
                }

            }
            if (objCACA_AccountsAccountID_Revenue.lstCVarA_Accounts.Count > 0 )
            {
                checkException = objCAccountsTAX.GetListPaging(9999, 1, "WHERE Account_Name = N'" + objCACA_AccountsAccountID_Revenue.lstCVarA_Accounts[0].Account_Name + "'", "ID", out _RowCount2);
                if (objCAccountsTAX.lstCVarA_Accounts.Count > 0)
                {
                    AccountID_Revenue = objCAccountsTAX.lstCVarA_Accounts[0].ID;
                }

            }
            if (objCACA_AccountsAccountID_Expense.lstCVarA_Accounts.Count > 0)
            {
                checkException = objCAccountsTAX.GetListPaging(9999, 1, "WHERE Account_Name = N'" + objCACA_AccountsAccountID_Expense.lstCVarA_Accounts[0].Account_Name + "'", "ID", out _RowCount2);
                if (objCAccountsTAX.lstCVarA_Accounts.Count > 0)
                {
                    AccountID_Expense = objCAccountsTAX.lstCVarA_Accounts[0].ID;
                }

            }
            #endregion
            if ((CompanyName == "CHM" || CompanyName == "OCE") && checkException == null && objCChargeTypesTAX.lstCVarChargeTypes.Count > 0)
            {
                CVarChargeTypesTAX objCVarChargeTypesTax = new CVarChargeTypesTAX();

                //the next 4 lines to get the CreatorUserID and CreationDate and set them as they were entered before
                CChargeTypesTAX objCGetCreationInformationTax = new CChargeTypesTAX();
                objCGetCreationInformation.GetItem(objCChargeTypesTAX.lstCVarChargeTypes[0].ID);
                objCVarChargeTypesTax.IsAddedManually = objCGetCreationInformation.lstCVarChargeTypes[0].IsAddedManually;
                objCVarChargeTypesTax.CreatorUserID = objCGetCreationInformation.lstCVarChargeTypes[0].CreatorUserID;
                objCVarChargeTypesTax.CreationDate = objCGetCreationInformation.lstCVarChargeTypes[0].CreationDate;

                objCVarChargeTypesTax.ID = objCChargeTypesTAX.lstCVarChargeTypes[0].ID;
                objCVarChargeTypesTax.MeasurementID = pMeasurementID;
                objCVarChargeTypesTax.TaxeTypeID = pTaxeTypeID;
                objCVarChargeTypesTax.InvoiceTypeID = pInvoiceTypeID;

                objCVarChargeTypesTax.Code = pCode;
                objCVarChargeTypesTax.PreCode = pPreCode;
                objCVarChargeTypesTax.Name = pName;
                objCVarChargeTypesTax.LocalName = (pLocalName == null ? "" : pLocalName);
                objCVarChargeTypesTax.ViewOrder = pViewOrder;
                objCVarChargeTypesTax.IsUsedInPayable = pIsUsedInPayable;
                objCVarChargeTypesTax.IsUsedInReceivable = pIsUsedInReceivable;
                objCVarChargeTypesTax.IsTank = pIsTank;
                objCVarChargeTypesTax.IsOcean = pIsOcean;
                objCVarChargeTypesTax.IsInland = pIsInland;
                objCVarChargeTypesTax.IsAir = pIsAir;
                objCVarChargeTypesTax.IsDefaultInQuotation = pIsDefaultInQuotation;
                objCVarChargeTypesTax.IsDefaultInOperations = pIsDefaultInOperations;
                objCVarChargeTypesTax.IsGeneralChargeType = pIsGeneralChargeType;
                objCVarChargeTypesTax.IsWarehouseChargeType = pIsWarehouseChargeType;
                objCVarChargeTypesTax.IsOperationChargeType = pIsOperationChargeType;
                objCVarChargeTypesTax.IsOfficial = pIsOfficial;

                objCVarChargeTypesTax.AccountID_Return = AccountID_Return;
                objCVarChargeTypesTax.SubAccountID_Return = SubAccountID_Return;
                objCVarChargeTypesTax.AccountID_Revenue = AccountID_Revenue;
                objCVarChargeTypesTax.SubAccountID_Revenue = SubAccountID_Revenue;
                objCVarChargeTypesTax.CostCenterID_Revenue = 0;
                objCVarChargeTypesTax.AccountID_Expense = AccountID_Expense;
                objCVarChargeTypesTax.SubAccountID_Expense = SubAccountID_Expense;
                objCVarChargeTypesTax.CostCenterID_Expense = 0;

                objCVarChargeTypesTax.IsInactive = pIsInactive;
                objCVarChargeTypesTax.TimeLocked = DateTime.Parse("01-01-1900");
                objCVarChargeTypesTax.LockingUserID = 0;

                objCVarChargeTypesTax.Notes = (pNotes == null ? "" : pNotes);
                objCVarChargeTypesTax.TemplateID = pTemplateID;
                objCVarChargeTypesTax.ChargeTypeGroupID = pChargeTypeGroupID;
                objCVarChargeTypesTax.PurchaseItemID = pPurchaseItemID;

                objCVarChargeTypesTax.Cost = pCost;

                objCVarChargeTypesTax.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarChargeTypesTax.ModificationDate = DateTime.Now;

                CChargeTypesTAX objCChargeTypesTax = new CChargeTypesTAX();
                objCChargeTypesTax.lstCVarChargeTypes.Add(objCVarChargeTypesTax);
                 checkException = objCChargeTypesTax.SaveMethod(objCChargeTypesTax.lstCVarChargeTypes);
                if (checkException != null) // an exception is caught in the model
                {
                    if (checkException.Message.Contains("UNIQUE"))
                        _result = false;
                }
                else //not unique
                    _result = true;
            }


            #endregion
            return _result;
        }

        // [Route("/api/Countries/DeleteByID/{pCountryID}")]
        //[HttpGet, HttpPost]
        //public void DeleteByID(Int32 pCountryID)
        //{
        //    CCountries objCCountries = new CCountries();
        //    objCCountries.lstDeletedCPKCountries.Add(new CPKCountries() { CountryID = pCountryID });
        //    objCCountries.DeleteItem(objCCountries.lstDeletedCPKCountries);
        //}

        // [Route("api/Countries/Delete/{pCountriesIDs}")]
        [HttpGet, HttpPost]
        public bool Delete(String pChargeTypesIDs)
        {
            bool _result = false;
            CChargeTypes objCChargeTypes = new CChargeTypes();

            Exception checkException = null;

            CChargeTypesTAX objCChargeTypesTAX2 = new CChargeTypesTAX();

            int _RowCount = 0;
            CvwDefaults objCvwDefaults = new CvwDefaults();
            objCvwDefaults.GetListPaging(1, 1, "WHERE 1=1", "ID", out _RowCount);
            string CompanyName = objCvwDefaults.lstCVarvwDefaults[0].UnEditableCompanyName;

            if (CompanyName == "CHM" || CompanyName == "OCE")
            {
                foreach (var currentID in pChargeTypesIDs.Split(','))
                {

                    CChargeTypesTAX objCTruckersTAX = new CChargeTypesTAX();
                    objCChargeTypes.GetList("WHERE ID=" + currentID);
                    objCTruckersTAX.GetList("WHERE Name=N'" + objCChargeTypes.lstCVarChargeTypes[0].Name + "'");
                    if (objCTruckersTAX.lstCVarChargeTypes.Count > 0)
                    {
                        objCChargeTypesTAX2.lstDeletedCPKChargeTypes.Add(new CPKChargeTypesTAX() { ID = objCTruckersTAX.lstCVarChargeTypes[0].ID });

                    }


                }
                objCChargeTypesTAX2.DeleteItem(objCChargeTypesTAX2.lstDeletedCPKChargeTypes);
                if (checkException != null) // an exception is caught in the model
                {
                    if (checkException.Message.Contains("DELETE")) // some or all of the rows were not deleted due to dependencies
                        _result = false;
                }

            }


            foreach (var currentID in pChargeTypesIDs.Split(','))
            {
                objCChargeTypes.lstDeletedCPKChargeTypes.Add(new CPKChargeTypes() { ID = Int32.Parse(currentID.Trim()) });
            }
             checkException = objCChargeTypes.DeleteItem(objCChargeTypes.lstDeletedCPKChargeTypes);
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
        public object[] InsertFromOperations(string pCodeFromOperations, string pNameFromOperations, string pLocalNameFromOperations)
        {
            string _MessageReturned = "";

            CVarChargeTypes objCVarChargeTypes = new CVarChargeTypes();

            objCVarChargeTypes.MeasurementID = 0;
            objCVarChargeTypes.TaxeTypeID = 0;
            objCVarChargeTypes.InvoiceTypeID = 0;

            objCVarChargeTypes.Code = pCodeFromOperations;
            objCVarChargeTypes.PreCode = "0";
            objCVarChargeTypes.Name = pNameFromOperations;
            objCVarChargeTypes.LocalName = (pLocalNameFromOperations == null ? "" : pLocalNameFromOperations);
            objCVarChargeTypes.ViewOrder = 0;
            objCVarChargeTypes.IsAddedManually = true;
            objCVarChargeTypes.IsUsedInPayable = true;
            objCVarChargeTypes.IsUsedInReceivable = true;
            objCVarChargeTypes.IsTank = false;
            objCVarChargeTypes.IsOcean = true;
            objCVarChargeTypes.IsInland = true;
            objCVarChargeTypes.IsAir = true;
            objCVarChargeTypes.IsDefaultInQuotation = false;
            objCVarChargeTypes.IsDefaultInOperations = false;
            objCVarChargeTypes.IsGeneralChargeType = false;
            objCVarChargeTypes.IsWarehouseChargeType = false;
            objCVarChargeTypes.IsOperationChargeType = true;
            objCVarChargeTypes.IsOfficial = false;

            objCVarChargeTypes.AccountID_Return = 0;
            objCVarChargeTypes.SubAccountID_Return = 0;
            objCVarChargeTypes.AccountID_Revenue = 0;
            objCVarChargeTypes.SubAccountID_Revenue = 0;
            objCVarChargeTypes.CostCenterID_Revenue = 0;
            objCVarChargeTypes.AccountID_Expense = 0;
            objCVarChargeTypes.SubAccountID_Expense = 0;
            objCVarChargeTypes.CostCenterID_Expense = 0;

            objCVarChargeTypes.IsInactive = false;
            objCVarChargeTypes.TimeLocked = DateTime.Parse("01-01-1900");
            objCVarChargeTypes.LockingUserID = 0;

            objCVarChargeTypes.Notes = "";
            objCVarChargeTypes.TemplateID = 0;
            objCVarChargeTypes.ChargeTypeGroupID = 0;
            objCVarChargeTypes.PurchaseItemID = 0;

            objCVarChargeTypes.CreatorUserID = objCVarChargeTypes.ModificatorUserID = WebSecurity.CurrentUserId;
            objCVarChargeTypes.CreationDate = objCVarChargeTypes.ModificationDate = DateTime.Now;

            CChargeTypes objCChargeTypes = new CChargeTypes();
            objCChargeTypes.lstCVarChargeTypes.Add(objCVarChargeTypes);
            Exception checkException = objCChargeTypes.SaveMethod(objCChargeTypes.lstCVarChargeTypes);

            if (checkException == null) //get returned data
            {
                objCChargeTypes.GetList("WHERE IsInactive=0 ORDER BY Name");
            }
            else
                _MessageReturned = checkException.Message;
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                _MessageReturned
                , _MessageReturned == "" ? objCVarChargeTypes.ID : 0 //pInsertedID = pData[1]
                , _MessageReturned == "" ? serializer.Serialize(objCChargeTypes.lstCVarChargeTypes) : null //pChargeTypes = pData[2]
            };
        }

        [HttpGet]
        public static decimal ChargeTypes_GetQuantity(Int32 pChargeTypeID, Int64 pOperationID)
        {
            decimal Quantity = 1;
            Exception checkException = null;
            int _RowCount = 0;
            CvwChargeTypes objCvwChargeTypes = new CvwChargeTypes();
            CvwOperations objCvwOperations = new CvwOperations();
            checkException = objCvwChargeTypes.GetListPaging(1, 1, "WHERE ID=" + pChargeTypeID, "ID", out _RowCount);
            checkException = objCvwOperations.GetListPaging(1, 1, "WHERE ID=" + pOperationID, "ID", out _RowCount);
            #region Calculate Quantity
            if (objCvwChargeTypes.lstCVarvwChargeTypes[0].MeasurementCode.Trim() == "GRWT")
                Quantity = objCvwOperations.lstCVarvwOperations[0].GrossWeightSum;
            else if (objCvwChargeTypes.lstCVarvwChargeTypes[0].MeasurementCode.Trim() == "CHWT")
                Quantity = objCvwOperations.lstCVarvwOperations[0].ChargeableWeightSum;
            else if (objCvwChargeTypes.lstCVarvwChargeTypes[0].MeasurementCode.Trim() == "VOLU")
                Quantity = objCvwOperations.lstCVarvwOperations[0].VolumeSum;
            else if (objCvwChargeTypes.lstCVarvwChargeTypes[0].MeasurementCode.Trim() == "BTEU")
                Quantity = objCvwOperations.lstCVarvwOperations[0].TEUs;
            else if (objCvwChargeTypes.lstCVarvwChargeTypes[0].MeasurementCode.Trim() == "BCNT")
            {
                COperationContainersAndPackages objCOperationContainersAndPackages = new COperationContainersAndPackages();
                checkException = objCOperationContainersAndPackages.GetListPaging(999999, 1, "WHERE OperationID=" + pOperationID + " AND ContainerTypeID IS NOT NULL", "ID", out _RowCount);
                Quantity = objCOperationContainersAndPackages.lstCVarOperationContainersAndPackages.Count;
            }
            #endregion Calculate Quantity
            return Quantity == 0 ? 1 : Quantity;
        }

        [HttpGet]
        public static void ChargeTypes_SetDefaultReceivablesQuantity(Int64 pOperationIDToSetReceivablesQuantity)
        {
            Exception checkException = null;
            int _RowCount = 0;
            CvwChargeTypes objCvwChargeTypes = new CvwChargeTypes();
            CvwOperations objCvwOperations = new CvwOperations();
            CReceivables objCReceivables = new CReceivables();
            checkException = objCvwOperations.GetListPaging(1, 1, "WHERE ID=" + pOperationIDToSetReceivablesQuantity, "ID", out _RowCount);
            checkException = objCReceivables.GetListPaging(999999, 1, "WHERE OperationID=" + pOperationIDToSetReceivablesQuantity + " AND InvoiceID IS NULL AND IsDeleted=0", "ID", out _RowCount);
            for (int i = 0; i < objCReceivables.lstCVarReceivables.Count; i++)
            {
                checkException = objCvwChargeTypes.GetListPaging(1, 1, "WHERE ID=" + objCReceivables.lstCVarReceivables[i].ChargeTypeID, "ID", out _RowCount);
                #region Calculate Quantity
                decimal _Quantity = 1;
                if (objCvwChargeTypes.lstCVarvwChargeTypes[0].MeasurementCode.Trim() == "GRWT")
                    _Quantity = objCvwOperations.lstCVarvwOperations[0].GrossWeightSum;
                else if (objCvwChargeTypes.lstCVarvwChargeTypes[0].MeasurementCode.Trim() == "CHWT")
                    _Quantity = objCvwOperations.lstCVarvwOperations[0].ChargeableWeightSum;
                else if (objCvwChargeTypes.lstCVarvwChargeTypes[0].MeasurementCode.Trim() == "VOLU")
                    _Quantity = objCvwOperations.lstCVarvwOperations[0].VolumeSum;
                else if (objCvwChargeTypes.lstCVarvwChargeTypes[0].MeasurementCode.Trim() == "BTEU")
                    _Quantity = objCvwOperations.lstCVarvwOperations[0].TEUs;
                else if (objCvwChargeTypes.lstCVarvwChargeTypes[0].MeasurementCode.Trim() == "BCNT")
                {
                    COperationContainersAndPackages objCOperationContainersAndPackages = new COperationContainersAndPackages();

                    #region include the container type in criteria if generated from quotation
                    CQuotationCharges objCQuotationCharges = new CQuotationCharges();
                    string containerTypeWhereClause = "";
                    checkException = objCQuotationCharges.GetListPaging(1, 1, "WHERE QuotationRouteID=" + objCReceivables.lstCVarReceivables[i].GeneratingQRID + " AND ContainerTypeID IS NOT NULL AND ChargeTypeID=" + objCReceivables.lstCVarReceivables[i].ChargeTypeID, "ID", out _RowCount);
                    if (objCQuotationCharges.lstCVarQuotationCharges.Count > 0)
                        containerTypeWhereClause = " AND ContainerTypeID=" + objCQuotationCharges.lstCVarQuotationCharges[0].ContainerTypeID;
                    #endregion include the container type in criteria if generated from quotation

                    checkException = objCOperationContainersAndPackages.GetListPaging(999999, 1, "WHERE OperationID=" + pOperationIDToSetReceivablesQuantity + " AND ContainerTypeID IS NOT NULL" + containerTypeWhereClause, "ID", out _RowCount);
                    _Quantity = objCOperationContainersAndPackages.lstCVarOperationContainersAndPackages.Count;
                }
                _Quantity = _Quantity == 0 ? 1 : _Quantity;
                #endregion Calculate Quantity
                #region ensure receivables are correct
                string pUpdateClause = "";
                pUpdateClause = "Quantity=" + _Quantity + " \n";
                pUpdateClause += " , AmountWithoutVAT = ROUND((ISNULL(SalePrice, 0) * " + _Quantity + "),2)" + " \n";
                pUpdateClause += " , TaxPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID)" + " \n";
                pUpdateClause += " , TaxAmount = ROUND((ISNULL(SalePrice, 0) * " + _Quantity + ") * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID), 2)" + " \n";
                pUpdateClause += " , DiscountPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID)" + " \n";
                pUpdateClause += " , DiscountAmount = ROUND((ISNULL(SalePrice, 0) * " + _Quantity + ") * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),2)" + " \n";
                pUpdateClause += " , SaleAmount = ROUND((ISNULL(SalePrice, 0) * " + _Quantity + "),2)" + " \n"
                              + " + (ROUND((ISNULL(SalePrice, 0) * " + _Quantity + ") * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID),0) , 2))" + " \n"
                              + " - (ROUND((ISNULL(SalePrice, 0) * " + _Quantity + ") * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),0), 2))" + " \n";
                pUpdateClause += " WHERE ID=" + objCReceivables.lstCVarReceivables[i].ID;
                #endregion ensure receivables are correct

                checkException = objCReceivables.UpdateList(pUpdateClause);
            } //for (int i=0;i<objCReceivables.lstCVarReceivables.Count;i++)
        }

        [HttpGet, HttpPost]
        public object[] ChargeTypes_SetDefaultReceivablesQuantity_NotStatic(Int64 pOperationIDToSetReceivablesQuantity_NotStatic)
        {
            string _ReturnedMessage = "";
            Exception checkException = null;
            int _RowCount = 0;
            CvwChargeTypes objCvwChargeTypes = new CvwChargeTypes();
            CvwOperations objCvwOperations = new CvwOperations();
            CReceivables objCReceivables = new CReceivables();
            checkException = objCvwOperations.GetListPaging(1, 1, "WHERE ID=" + pOperationIDToSetReceivablesQuantity_NotStatic, "ID", out _RowCount);
            checkException = objCReceivables.GetListPaging(999999, 1, "WHERE OperationID=" + pOperationIDToSetReceivablesQuantity_NotStatic + " AND InvoiceID IS NULL AND IsDeleted=0", "ID", out _RowCount);
            for (int i = 0; i < objCReceivables.lstCVarReceivables.Count; i++)
            {
                checkException = objCvwChargeTypes.GetListPaging(1, 1, "WHERE ID=" + objCReceivables.lstCVarReceivables[i].ChargeTypeID, "ID", out _RowCount);
                #region Calculate Quantity
                decimal _Quantity = 1;
                if (objCvwChargeTypes.lstCVarvwChargeTypes[0].MeasurementCode.Trim() == "GRWT")
                    _Quantity = objCvwOperations.lstCVarvwOperations[0].GrossWeightSum;
                else if (objCvwChargeTypes.lstCVarvwChargeTypes[0].MeasurementCode.Trim() == "CHWT")
                    _Quantity = objCvwOperations.lstCVarvwOperations[0].ChargeableWeightSum;
                else if (objCvwChargeTypes.lstCVarvwChargeTypes[0].MeasurementCode.Trim() == "VOLU")
                    _Quantity = objCvwOperations.lstCVarvwOperations[0].VolumeSum;
                else if (objCvwChargeTypes.lstCVarvwChargeTypes[0].MeasurementCode.Trim() == "BTEU")
                    _Quantity = objCvwOperations.lstCVarvwOperations[0].TEUs;
                else if (objCvwChargeTypes.lstCVarvwChargeTypes[0].MeasurementCode.Trim() == "BCNT")
                {
                    COperationContainersAndPackages objCOperationContainersAndPackages = new COperationContainersAndPackages();
                    
                    #region include the container type in criteria if generated from quotation
                    CQuotationCharges objCQuotationCharges = new CQuotationCharges();
                    string containerTypeWhereClause = "";
                    checkException = objCQuotationCharges.GetListPaging(1, 1, "WHERE QuotationRouteID=" + objCReceivables.lstCVarReceivables[i].GeneratingQRID + " AND ContainerTypeID IS NOT NULL AND ChargeTypeID=" + objCReceivables.lstCVarReceivables[i].ChargeTypeID, "ID", out _RowCount);
                    if (objCQuotationCharges.lstCVarQuotationCharges.Count > 0)
                        containerTypeWhereClause = " AND ContainerTypeID=" + objCQuotationCharges.lstCVarQuotationCharges[0].ContainerTypeID;
                    #endregion include the container type in criteria if generated from quotation

                    checkException = objCOperationContainersAndPackages.GetListPaging(999999, 1, "WHERE OperationID=" + pOperationIDToSetReceivablesQuantity_NotStatic + " AND ContainerTypeID IS NOT NULL " + containerTypeWhereClause, "ID", out _RowCount);
                    _Quantity = objCOperationContainersAndPackages.lstCVarOperationContainersAndPackages.Count;
                }
                _Quantity = _Quantity == 0 ? 1 : _Quantity;
                #endregion Calculate Quantity
                #region ensure receivables are correct
                string pUpdateClause = "";
                pUpdateClause = "Quantity=" + _Quantity + " \n";
                pUpdateClause += " , AmountWithoutVAT = ROUND((ISNULL(SalePrice, 0) * " + _Quantity + "),2)" + " \n";
                pUpdateClause += " , TaxPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID)" + " \n";
                pUpdateClause += " , TaxAmount = ROUND((ISNULL(SalePrice, 0) * " + _Quantity + ") * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID), 2)" + " \n";
                pUpdateClause += " , DiscountPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID)" + " \n";
                pUpdateClause += " , DiscountAmount = ROUND((ISNULL(SalePrice, 0) * " + _Quantity + ") * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),2)" + " \n";
                pUpdateClause += " , SaleAmount = ROUND((ISNULL(SalePrice, 0) * " + _Quantity + "),2)" + " \n"
                              + " + (ROUND((ISNULL(SalePrice, 0) * " + _Quantity + ") * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = TaxeTypeID),0) , 2))" + " \n"
                              + " - (ROUND((ISNULL(SalePrice, 0) * " + _Quantity + ") * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),0), 2))" + " \n";
                pUpdateClause += " WHERE ID=" + objCReceivables.lstCVarReceivables[i].ID;
                #endregion ensure receivables are correct

                checkException = objCReceivables.UpdateList(pUpdateClause);
            } //for (int i=0;i<objCReceivables.lstCVarReceivables.Count;i++)

            return new object[] { _ReturnedMessage };
        }

        [HttpGet, HttpPost]
        public object[] ChargeTypes_SetDefaultPayablesQuantity_NotStatic(Int64 pOperationIDToSetPayablesQuantity_NotStatic)
        {
            string _ReturnedMessage = "";
            Exception checkException = null;
            int _RowCount = 0;
            CvwChargeTypes objCvwChargeTypes = new CvwChargeTypes();
            CvwOperations objCvwOperations = new CvwOperations();
            CPayables objCPayables = new CPayables();
            checkException = objCvwOperations.GetListPaging(1, 1, "WHERE ID=" + pOperationIDToSetPayablesQuantity_NotStatic, "ID", out _RowCount);
            checkException = objCPayables.GetListPaging(999999, 1, "WHERE OperationID=" + pOperationIDToSetPayablesQuantity_NotStatic + " AND IsApproved=0 AND IsDeleted=0", "ID", out _RowCount);
            for (int i = 0; i < objCPayables.lstCVarPayables.Count; i++)
            {
                checkException = objCvwChargeTypes.GetListPaging(1, 1, "WHERE ID=" + objCPayables.lstCVarPayables[i].ChargeTypeID, "ID", out _RowCount);
                #region Calculate Quantity
                decimal _Quantity = 1;
                if (objCvwChargeTypes.lstCVarvwChargeTypes[0].MeasurementCode.Trim() == "GRWT")
                    _Quantity = objCvwOperations.lstCVarvwOperations[0].GrossWeightSum;
                else if (objCvwChargeTypes.lstCVarvwChargeTypes[0].MeasurementCode.Trim() == "CHWT")
                    _Quantity = objCvwOperations.lstCVarvwOperations[0].ChargeableWeightSum;
                else if (objCvwChargeTypes.lstCVarvwChargeTypes[0].MeasurementCode.Trim() == "VOLU")
                    _Quantity = objCvwOperations.lstCVarvwOperations[0].VolumeSum;
                else if (objCvwChargeTypes.lstCVarvwChargeTypes[0].MeasurementCode.Trim() == "BTEU")
                    _Quantity = objCvwOperations.lstCVarvwOperations[0].TEUs;
                else if (objCvwChargeTypes.lstCVarvwChargeTypes[0].MeasurementCode.Trim() == "BCNT")
                {
                    COperationContainersAndPackages objCOperationContainersAndPackages = new COperationContainersAndPackages();

                    #region include the container type in criteria if generated from quotation
                    CQuotationCharges objCQuotationCharges = new CQuotationCharges();
                    string containerTypeWhereClause = "";
                    checkException = objCQuotationCharges.GetListPaging(1, 1, "WHERE QuotationRouteID=" + objCPayables.lstCVarPayables[i].GeneratingQRID + " AND ContainerTypeID IS NOT NULL AND ChargeTypeID=" + objCPayables.lstCVarPayables[i].ChargeTypeID, "ID", out _RowCount);
                    if (objCQuotationCharges.lstCVarQuotationCharges.Count > 0)
                        containerTypeWhereClause = " AND ContainerTypeID=" + objCQuotationCharges.lstCVarQuotationCharges[0].ContainerTypeID;
                    #endregion include the container type in criteria if generated from quotation

                    checkException = objCOperationContainersAndPackages.GetListPaging(999999, 1, "WHERE OperationID=" + pOperationIDToSetPayablesQuantity_NotStatic + " AND ContainerTypeID IS NOT NULL " + containerTypeWhereClause, "ID", out _RowCount);
                    _Quantity = objCOperationContainersAndPackages.lstCVarOperationContainersAndPackages.Count;
                }
                _Quantity = _Quantity == 0 ? 1 : _Quantity;
                #endregion Calculate Quantity
                #region ensure Payables are correct
                string pUpdateClause = "";
                pUpdateClause = "Quantity=" + _Quantity + " \n";
                pUpdateClause += " , AmountWithoutVAT = ROUND((ISNULL(CostPrice, 0) * " + _Quantity + "),2)" + " \n";
                pUpdateClause += " , TaxPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = TaxTypeID)" + " \n";
                pUpdateClause += " , TaxAmount = ROUND((ISNULL(CostPrice, 0) * " + _Quantity + ") * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = TaxTypeID), 2)" + " \n";
                pUpdateClause += " , DiscountPercentage = (select TT.CurrentPercentage FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID)" + " \n";
                pUpdateClause += " , DiscountAmount = ROUND((ISNULL(CostPrice, 0) * " + _Quantity + ") * (select TT.CurrentPercentage/100 FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),2)" + " \n";
                pUpdateClause += " , CostAmount = ROUND((ISNULL(CostPrice, 0) * " + _Quantity + "),2)" + " \n"
                              + " + (ROUND((ISNULL(CostPrice, 0) * " + _Quantity + ") * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = TaxTypeID),0) , 2))" + " \n"
                              + " - (ROUND((ISNULL(CostPrice, 0) * " + _Quantity + ") * ISNULL((select ISNULL(TT.CurrentPercentage / 100, 0) FROM TaxeTypes TT WHERE TT.ID = DiscountTypeID),0), 2))" + " \n";
                pUpdateClause += " WHERE ID=" + objCPayables.lstCVarPayables[i].ID;
                #endregion ensure Payables are correct

                checkException = objCPayables.UpdateList(pUpdateClause);
            } //for (int i=0;i<objCPayables.lstCVarPayables.Count;i++)

            return new object[] { _ReturnedMessage };
        }


        [HttpGet, HttpPost]
        public object[] InsertListFromExcel_ChargeTypes([FromBody] InsertListFromExcel_ChargeTypes InsertListFromExcel_ChargeTypes)
        {
            string pReturnedMessage = "";
            Exception checkException = null;
            int _NumberOfRows = InsertListFromExcel_ChargeTypes.pNameList.Split(',').Length;
            var _ArrName = InsertListFromExcel_ChargeTypes.pNameList.Split(',');
            var _ArrLocalName = InsertListFromExcel_ChargeTypes.pLocalNameList.Split(',');
            var _ArrCode = InsertListFromExcel_ChargeTypes.pCodeList.Split(',');


            for (int i = 0; i < _NumberOfRows; i++)
            {
                CVarChargeTypes objCVarChargeTypes = new CVarChargeTypes();
                //objCVarChargeTypes.TareWeight = decimal.Parse(_ArrTareWeight[i]);


                objCVarChargeTypes.MeasurementID = 0;
                objCVarChargeTypes.InvoiceTypeID = 0;
                objCVarChargeTypes.TaxeTypeID = 0;

                objCVarChargeTypes.PreCode = "0";
                objCVarChargeTypes.Code = _ArrCode[i];
                objCVarChargeTypes.Name = _ArrName[i];
                objCVarChargeTypes.LocalName = _ArrLocalName[i];
                objCVarChargeTypes.IsInactive = false;
                objCVarChargeTypes.Notes = "";

                objCVarChargeTypes.ViewOrder = 0;
                objCVarChargeTypes.IsAddedManually = true;
                objCVarChargeTypes.IsUsedInPayable = true;
                objCVarChargeTypes.IsUsedInReceivable = true;
                objCVarChargeTypes.IsTank = false;
                objCVarChargeTypes.IsOcean = true;
                objCVarChargeTypes.IsInland = true;
                objCVarChargeTypes.IsAir = true;
                objCVarChargeTypes.IsDefaultInQuotation = false;
                objCVarChargeTypes.IsDefaultInOperations = false;
                objCVarChargeTypes.IsGeneralChargeType = true;
                objCVarChargeTypes.IsWarehouseChargeType = false;
                objCVarChargeTypes.IsOperationChargeType = true;
                objCVarChargeTypes.IsOfficial = false;

                objCVarChargeTypes.AccountID_Return = 0;
                objCVarChargeTypes.SubAccountID_Return = 0;
                objCVarChargeTypes.AccountID_Revenue = 0;
                objCVarChargeTypes.SubAccountID_Revenue = 0;
                objCVarChargeTypes.CostCenterID_Revenue = 0;
                objCVarChargeTypes.AccountID_Expense = 0;
                objCVarChargeTypes.SubAccountID_Expense = 0;
                objCVarChargeTypes.CostCenterID_Expense = 0;

                objCVarChargeTypes.TemplateID = 0;
                objCVarChargeTypes.ChargeTypeGroupID = 0;
                objCVarChargeTypes.PurchaseItemID = 0;

                objCVarChargeTypes.Cost = 0;

                objCVarChargeTypes.TimeLocked = DateTime.Parse("01-01-1900");
                objCVarChargeTypes.LockingUserID = 0;

                objCVarChargeTypes.CreatorUserID = objCVarChargeTypes.ModificatorUserID = WebSecurity.CurrentUserId;
                objCVarChargeTypes.CreationDate = objCVarChargeTypes.ModificationDate = DateTime.Now;

            CChargeTypes objCChargeTypes = new CChargeTypes();
                objCChargeTypes.lstCVarChargeTypes.Add(objCVarChargeTypes);
                checkException = objCChargeTypes.SaveMethod(objCChargeTypes.lstCVarChargeTypes);

                if (checkException != null)
                {
                    pReturnedMessage += "Row " + (i + 1) + " - " + checkException.Message + " \n";
                }
            }
            

            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
            return new object[] {
                pReturnedMessage
            };
        }

    }

    public class InsertListFromExcel_ChargeTypes
    {
        public string pNameList { get; set; }
        public string pLocalNameList { get; set; }
        public string pCodeList { get; set; }
    }
}