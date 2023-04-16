﻿using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.MasterData.Invoicing.Generated
{
    [Serializable]
    public class CPKvwChargeTypes
    {
        #region "variables"
        private Int32 mID;
        #endregion

        #region "Methods"
        public Int32 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        #endregion
    }
    [Serializable]
    public partial class CVarvwChargeTypes : CPKvwChargeTypes
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mCode;
        internal String mName;
        internal String mLocalName;
        internal String mChargeTypeCode;
        internal String mChargeTypeName;
        internal String mChargeTypeLocalName;
        internal Int32 mInvoiceTypeID;
        internal String mInvoiceTypeCode;
        internal String mInvoiceTypeName;
        internal String mInvoiceTypeLocalName;
        internal Int32 mMeasurementID;
        internal Int32 mTaxeTypeID;
        internal Int32 mViewOrder;
        internal String mNotes;
        internal Boolean mIsUsedInReceivable;
        internal Boolean mIsUsedInPayable;
        internal Boolean mIsOcean;
        internal Boolean mIsInland;
        internal Boolean mIsAir;
        internal Boolean mIsDefaultInQuotation;
        internal Boolean mIsDefaultInOperations;
        internal Boolean mIsAddedManually;
        internal Boolean mIsInactive;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Int32 mLockingUserID;
        internal DateTime mTimeLocked;
        internal String mMeasurementCode;
        internal String mMeasurementName;
        internal String mTaxeTypeCode;
        internal String mTaxeTypeName;
        internal Decimal mTaxPercentage;
        internal Boolean mIsGeneralChargeType;
        internal Boolean mIsOperationChargeType;
        internal Int32 mAccountID_Revenue;
        internal String mAccountName_Revenue;
        internal Int32 mSubAccountID_Revenue;
        internal String mSubAccountName_Revenue;
        internal Int32 mAccountID_Return;
        internal String mAccountName_Return;
        internal Int32 mSubAccountID_Return;
        internal String mSubAccountName_Return;
        internal Int32 mCostCenterID_Revenue;
        internal String mCostCenterName_Revenue;
        internal Int32 mAccountID_Expense;
        internal String mAccountName_Expense;
        internal Int32 mSubAccountID_Expense;
        internal String mSubAccountName_Expense;
        internal Int32 mCostCenterID_Expense;
        internal String mCostCenterName_Expense;
        internal Int32 mTemplateID;
        internal Int64 mPurchaseItemID;
        internal String mPurchaseItemCode;
        internal Boolean mIsTank;
        internal Boolean mIsWarehouseChargeType;
        internal Boolean mIsExcludeInTruckingOrderPrint;
        internal Boolean mIsOfficial;
        internal String mPreCode;
        internal Int32 mChargeTypeGroupID;
        internal String mChargeTypeGroupName;
        internal Decimal mCost;
        #endregion

        #region "Methods"
        public String Code
        {
            get { return mCode; }
            set { mCode = value; }
        }
        public String Name
        {
            get { return mName; }
            set { mName = value; }
        }
        public String LocalName
        {
            get { return mLocalName; }
            set { mLocalName = value; }
        }
        public String ChargeTypeCode
        {
            get { return mChargeTypeCode; }
            set { mChargeTypeCode = value; }
        }
        public String ChargeTypeName
        {
            get { return mChargeTypeName; }
            set { mChargeTypeName = value; }
        }
        public String ChargeTypeLocalName
        {
            get { return mChargeTypeLocalName; }
            set { mChargeTypeLocalName = value; }
        }
        public Int32 InvoiceTypeID
        {
            get { return mInvoiceTypeID; }
            set { mInvoiceTypeID = value; }
        }
        public String InvoiceTypeCode
        {
            get { return mInvoiceTypeCode; }
            set { mInvoiceTypeCode = value; }
        }
        public String InvoiceTypeName
        {
            get { return mInvoiceTypeName; }
            set { mInvoiceTypeName = value; }
        }
        public String InvoiceTypeLocalName
        {
            get { return mInvoiceTypeLocalName; }
            set { mInvoiceTypeLocalName = value; }
        }
        public Int32 MeasurementID
        {
            get { return mMeasurementID; }
            set { mMeasurementID = value; }
        }
        public Int32 TaxeTypeID
        {
            get { return mTaxeTypeID; }
            set { mTaxeTypeID = value; }
        }
        public Int32 ViewOrder
        {
            get { return mViewOrder; }
            set { mViewOrder = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mNotes = value; }
        }
        public Boolean IsUsedInReceivable
        {
            get { return mIsUsedInReceivable; }
            set { mIsUsedInReceivable = value; }
        }
        public Boolean IsUsedInPayable
        {
            get { return mIsUsedInPayable; }
            set { mIsUsedInPayable = value; }
        }
        public Boolean IsOcean
        {
            get { return mIsOcean; }
            set { mIsOcean = value; }
        }
        public Boolean IsInland
        {
            get { return mIsInland; }
            set { mIsInland = value; }
        }
        public Boolean IsAir
        {
            get { return mIsAir; }
            set { mIsAir = value; }
        }
        public Boolean IsDefaultInQuotation
        {
            get { return mIsDefaultInQuotation; }
            set { mIsDefaultInQuotation = value; }
        }
        public Boolean IsDefaultInOperations
        {
            get { return mIsDefaultInOperations; }
            set { mIsDefaultInOperations = value; }
        }
        public Boolean IsAddedManually
        {
            get { return mIsAddedManually; }
            set { mIsAddedManually = value; }
        }
        public Boolean IsInactive
        {
            get { return mIsInactive; }
            set { mIsInactive = value; }
        }
        public Int32 CreatorUserID
        {
            get { return mCreatorUserID; }
            set { mCreatorUserID = value; }
        }
        public DateTime CreationDate
        {
            get { return mCreationDate; }
            set { mCreationDate = value; }
        }
        public Int32 ModificatorUserID
        {
            get { return mModificatorUserID; }
            set { mModificatorUserID = value; }
        }
        public DateTime ModificationDate
        {
            get { return mModificationDate; }
            set { mModificationDate = value; }
        }
        public Int32 LockingUserID
        {
            get { return mLockingUserID; }
            set { mLockingUserID = value; }
        }
        public DateTime TimeLocked
        {
            get { return mTimeLocked; }
            set { mTimeLocked = value; }
        }
        public String MeasurementCode
        {
            get { return mMeasurementCode; }
            set { mMeasurementCode = value; }
        }
        public String MeasurementName
        {
            get { return mMeasurementName; }
            set { mMeasurementName = value; }
        }
        public String TaxeTypeCode
        {
            get { return mTaxeTypeCode; }
            set { mTaxeTypeCode = value; }
        }
        public String TaxeTypeName
        {
            get { return mTaxeTypeName; }
            set { mTaxeTypeName = value; }
        }
        public Decimal TaxPercentage
        {
            get { return mTaxPercentage; }
            set { mTaxPercentage = value; }
        }
        public Boolean IsGeneralChargeType
        {
            get { return mIsGeneralChargeType; }
            set { mIsGeneralChargeType = value; }
        }
        public Boolean IsOperationChargeType
        {
            get { return mIsOperationChargeType; }
            set { mIsOperationChargeType = value; }
        }
        public Int32 AccountID_Revenue
        {
            get { return mAccountID_Revenue; }
            set { mAccountID_Revenue = value; }
        }
        public String AccountName_Revenue
        {
            get { return mAccountName_Revenue; }
            set { mAccountName_Revenue = value; }
        }
        public Int32 SubAccountID_Revenue
        {
            get { return mSubAccountID_Revenue; }
            set { mSubAccountID_Revenue = value; }
        }
        public String SubAccountName_Revenue
        {
            get { return mSubAccountName_Revenue; }
            set { mSubAccountName_Revenue = value; }
        }
        public Int32 AccountID_Return
        {
            get { return mAccountID_Return; }
            set { mAccountID_Return = value; }
        }
        public String AccountName_Return
        {
            get { return mAccountName_Return; }
            set { mAccountName_Return = value; }
        }
        public Int32 SubAccountID_Return
        {
            get { return mSubAccountID_Return; }
            set { mSubAccountID_Return = value; }
        }
        public String SubAccountName_Return
        {
            get { return mSubAccountName_Return; }
            set { mSubAccountName_Return = value; }
        }
        public Int32 CostCenterID_Revenue
        {
            get { return mCostCenterID_Revenue; }
            set { mCostCenterID_Revenue = value; }
        }
        public String CostCenterName_Revenue
        {
            get { return mCostCenterName_Revenue; }
            set { mCostCenterName_Revenue = value; }
        }
        public Int32 AccountID_Expense
        {
            get { return mAccountID_Expense; }
            set { mAccountID_Expense = value; }
        }
        public String AccountName_Expense
        {
            get { return mAccountName_Expense; }
            set { mAccountName_Expense = value; }
        }
        public Int32 SubAccountID_Expense
        {
            get { return mSubAccountID_Expense; }
            set { mSubAccountID_Expense = value; }
        }
        public String SubAccountName_Expense
        {
            get { return mSubAccountName_Expense; }
            set { mSubAccountName_Expense = value; }
        }
        public Int32 CostCenterID_Expense
        {
            get { return mCostCenterID_Expense; }
            set { mCostCenterID_Expense = value; }
        }
        public String CostCenterName_Expense
        {
            get { return mCostCenterName_Expense; }
            set { mCostCenterName_Expense = value; }
        }
        public Int32 TemplateID
        {
            get { return mTemplateID; }
            set { mTemplateID = value; }
        }
        public Int64 PurchaseItemID
        {
            get { return mPurchaseItemID; }
            set { mPurchaseItemID = value; }
        }
        public String PurchaseItemCode
        {
            get { return mPurchaseItemCode; }
            set { mPurchaseItemCode = value; }
        }
        public Boolean IsTank
        {
            get { return mIsTank; }
            set { mIsTank = value; }
        }
        public Boolean IsWarehouseChargeType
        {
            get { return mIsWarehouseChargeType; }
            set { mIsWarehouseChargeType = value; }
        }
        public Boolean IsExcludeInTruckingOrderPrint
        {
            get { return mIsExcludeInTruckingOrderPrint; }
            set { mIsExcludeInTruckingOrderPrint = value; }
        }
        public Boolean IsOfficial
        {
            get { return mIsOfficial; }
            set { mIsOfficial = value; }
        }
        public String PreCode
        {
            get { return mPreCode; }
            set { mPreCode = value; }
        }
        public Int32 ChargeTypeGroupID
        {
            get { return mChargeTypeGroupID; }
            set { mChargeTypeGroupID = value; }
        }
        public String ChargeTypeGroupName
        {
            get { return mChargeTypeGroupName; }
            set { mChargeTypeGroupName = value; }
        }
        public Decimal Cost
        {
            get { return mCost; }
            set { mCost = value; }
        }
        #endregion

        #region Functions
        public Boolean GetIsChange()
        {
            return mIsChanges;
        }
        public void SetIsChange(Boolean IsChange)
        {
            mIsChanges = IsChange;
        }
        #endregion
    }

    public partial class CvwChargeTypes
    {
        #region "variables"
        /*If "App.Config" isnot exist add it to your Application
		Add this code after <Configuration> tag
		-------------------------------------------------------
		<appsettings>
		<add key="ConnectionString" value="............"/>
		</appsettings>
		-------------------------------------------------------
		where ".........." is connection string to database server*/
        private SqlTransaction tr;
        public List<CVarvwChargeTypes> lstCVarvwChargeTypes = new List<CVarvwChargeTypes>();
        #endregion

        #region "Select Methods"
        public Exception GetList(string WhereClause)
        {
            return DataFill(WhereClause, true);
        }
        public Exception GetListPaging(Int32 PageSize, Int32 PageNumber, string WhereClause, string OrderBy, out Int32 TotalRecords)
        {
            return DataFill(PageSize, PageNumber, WhereClause, OrderBy, out TotalRecords);
        }
        private Exception DataFill(string Param, Boolean IsList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarvwChargeTypes.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwChargeTypes";
                    Com.Parameters[0].Value = Param;
                }
                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        /*Start DataReader*/
                        CVarvwChargeTypes ObjCVarvwChargeTypes = new CVarvwChargeTypes();
                        ObjCVarvwChargeTypes.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwChargeTypes.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwChargeTypes.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwChargeTypes.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwChargeTypes.mChargeTypeCode = Convert.ToString(dr["ChargeTypeCode"].ToString());
                        ObjCVarvwChargeTypes.mChargeTypeName = Convert.ToString(dr["ChargeTypeName"].ToString());
                        ObjCVarvwChargeTypes.mChargeTypeLocalName = Convert.ToString(dr["ChargeTypeLocalName"].ToString());
                        ObjCVarvwChargeTypes.mInvoiceTypeID = Convert.ToInt32(dr["InvoiceTypeID"].ToString());
                        ObjCVarvwChargeTypes.mInvoiceTypeCode = Convert.ToString(dr["InvoiceTypeCode"].ToString());
                        ObjCVarvwChargeTypes.mInvoiceTypeName = Convert.ToString(dr["InvoiceTypeName"].ToString());
                        ObjCVarvwChargeTypes.mInvoiceTypeLocalName = Convert.ToString(dr["InvoiceTypeLocalName"].ToString());
                        ObjCVarvwChargeTypes.mMeasurementID = Convert.ToInt32(dr["MeasurementID"].ToString());
                        ObjCVarvwChargeTypes.mTaxeTypeID = Convert.ToInt32(dr["TaxeTypeID"].ToString());
                        ObjCVarvwChargeTypes.mViewOrder = Convert.ToInt32(dr["ViewOrder"].ToString());
                        ObjCVarvwChargeTypes.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwChargeTypes.mIsUsedInReceivable = Convert.ToBoolean(dr["IsUsedInReceivable"].ToString());
                        ObjCVarvwChargeTypes.mIsUsedInPayable = Convert.ToBoolean(dr["IsUsedInPayable"].ToString());
                        ObjCVarvwChargeTypes.mIsOcean = Convert.ToBoolean(dr["IsOcean"].ToString());
                        ObjCVarvwChargeTypes.mIsInland = Convert.ToBoolean(dr["IsInland"].ToString());
                        ObjCVarvwChargeTypes.mIsAir = Convert.ToBoolean(dr["IsAir"].ToString());
                        ObjCVarvwChargeTypes.mIsDefaultInQuotation = Convert.ToBoolean(dr["IsDefaultInQuotation"].ToString());
                        ObjCVarvwChargeTypes.mIsDefaultInOperations = Convert.ToBoolean(dr["IsDefaultInOperations"].ToString());
                        ObjCVarvwChargeTypes.mIsAddedManually = Convert.ToBoolean(dr["IsAddedManually"].ToString());
                        ObjCVarvwChargeTypes.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarvwChargeTypes.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwChargeTypes.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwChargeTypes.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwChargeTypes.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwChargeTypes.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarvwChargeTypes.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarvwChargeTypes.mMeasurementCode = Convert.ToString(dr["MeasurementCode"].ToString());
                        ObjCVarvwChargeTypes.mMeasurementName = Convert.ToString(dr["MeasurementName"].ToString());
                        ObjCVarvwChargeTypes.mTaxeTypeCode = Convert.ToString(dr["TaxeTypeCode"].ToString());
                        ObjCVarvwChargeTypes.mTaxeTypeName = Convert.ToString(dr["TaxeTypeName"].ToString());
                        ObjCVarvwChargeTypes.mTaxPercentage = Convert.ToDecimal(dr["TaxPercentage"].ToString());
                        ObjCVarvwChargeTypes.mIsGeneralChargeType = Convert.ToBoolean(dr["IsGeneralChargeType"].ToString());
                        ObjCVarvwChargeTypes.mIsOperationChargeType = Convert.ToBoolean(dr["IsOperationChargeType"].ToString());
                        ObjCVarvwChargeTypes.mAccountID_Revenue = Convert.ToInt32(dr["AccountID_Revenue"].ToString());
                        ObjCVarvwChargeTypes.mAccountName_Revenue = Convert.ToString(dr["AccountName_Revenue"].ToString());
                        ObjCVarvwChargeTypes.mSubAccountID_Revenue = Convert.ToInt32(dr["SubAccountID_Revenue"].ToString());
                        ObjCVarvwChargeTypes.mSubAccountName_Revenue = Convert.ToString(dr["SubAccountName_Revenue"].ToString());
                        ObjCVarvwChargeTypes.mAccountID_Return = Convert.ToInt32(dr["AccountID_Return"].ToString());
                        ObjCVarvwChargeTypes.mAccountName_Return = Convert.ToString(dr["AccountName_Return"].ToString());
                        ObjCVarvwChargeTypes.mSubAccountID_Return = Convert.ToInt32(dr["SubAccountID_Return"].ToString());
                        ObjCVarvwChargeTypes.mSubAccountName_Return = Convert.ToString(dr["SubAccountName_Return"].ToString());
                        ObjCVarvwChargeTypes.mCostCenterID_Revenue = Convert.ToInt32(dr["CostCenterID_Revenue"].ToString());
                        ObjCVarvwChargeTypes.mCostCenterName_Revenue = Convert.ToString(dr["CostCenterName_Revenue"].ToString());
                        ObjCVarvwChargeTypes.mAccountID_Expense = Convert.ToInt32(dr["AccountID_Expense"].ToString());
                        ObjCVarvwChargeTypes.mAccountName_Expense = Convert.ToString(dr["AccountName_Expense"].ToString());
                        ObjCVarvwChargeTypes.mSubAccountID_Expense = Convert.ToInt32(dr["SubAccountID_Expense"].ToString());
                        ObjCVarvwChargeTypes.mSubAccountName_Expense = Convert.ToString(dr["SubAccountName_Expense"].ToString());
                        ObjCVarvwChargeTypes.mCostCenterID_Expense = Convert.ToInt32(dr["CostCenterID_Expense"].ToString());
                        ObjCVarvwChargeTypes.mCostCenterName_Expense = Convert.ToString(dr["CostCenterName_Expense"].ToString());
                        ObjCVarvwChargeTypes.mTemplateID = Convert.ToInt32(dr["TemplateID"].ToString());
                        ObjCVarvwChargeTypes.mPurchaseItemID = Convert.ToInt64(dr["PurchaseItemID"].ToString());
                        ObjCVarvwChargeTypes.mPurchaseItemCode = Convert.ToString(dr["PurchaseItemCode"].ToString());
                        ObjCVarvwChargeTypes.mIsTank = Convert.ToBoolean(dr["IsTank"].ToString());
                        ObjCVarvwChargeTypes.mIsWarehouseChargeType = Convert.ToBoolean(dr["IsWarehouseChargeType"].ToString());
                        ObjCVarvwChargeTypes.mIsExcludeInTruckingOrderPrint = Convert.ToBoolean(dr["IsExcludeInTruckingOrderPrint"].ToString());
                        ObjCVarvwChargeTypes.mIsOfficial = Convert.ToBoolean(dr["IsOfficial"].ToString());
                        ObjCVarvwChargeTypes.mPreCode = Convert.ToString(dr["PreCode"].ToString());
                        ObjCVarvwChargeTypes.mChargeTypeGroupID = Convert.ToInt32(dr["ChargeTypeGroupID"].ToString());
                        ObjCVarvwChargeTypes.mChargeTypeGroupName = Convert.ToString(dr["ChargeTypeGroupName"].ToString());
                        ObjCVarvwChargeTypes.mCost = Convert.ToDecimal(dr["Cost"].ToString());
                        lstCVarvwChargeTypes.Add(ObjCVarvwChargeTypes);
                    }
                }
                catch (Exception ex)
                {
                    Exp = ex;
                }
                finally
                {
                    if (dr != null)
                    {
                        dr.Close();
                        dr.Dispose();
                    }
                }
                tr.Commit();
            }
            catch (Exception ex)
            {
                Exp = ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
            }
            return Exp;
        }

        private Exception DataFill(Int32 PageSize, Int32 PageNumber, string WhereClause, string OrderBy, out Int32 TotRecs)
        {
            Exception Exp = null;
            TotRecs = 0;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarvwChargeTypes.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                Com.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@PageNumber", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar));
                Com.CommandText = "[dbo].GetListPagingvwChargeTypes";
                Com.Parameters[0].Value = PageSize;
                Com.Parameters[1].Value = PageNumber;
                Com.Parameters[2].Value = WhereClause;
                Com.Parameters[3].Value = OrderBy;
                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        /*Start DataReader*/
                        CVarvwChargeTypes ObjCVarvwChargeTypes = new CVarvwChargeTypes();
                        ObjCVarvwChargeTypes.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwChargeTypes.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwChargeTypes.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwChargeTypes.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwChargeTypes.mChargeTypeCode = Convert.ToString(dr["ChargeTypeCode"].ToString());
                        ObjCVarvwChargeTypes.mChargeTypeName = Convert.ToString(dr["ChargeTypeName"].ToString());
                        ObjCVarvwChargeTypes.mChargeTypeLocalName = Convert.ToString(dr["ChargeTypeLocalName"].ToString());
                        ObjCVarvwChargeTypes.mInvoiceTypeID = Convert.ToInt32(dr["InvoiceTypeID"].ToString());
                        ObjCVarvwChargeTypes.mInvoiceTypeCode = Convert.ToString(dr["InvoiceTypeCode"].ToString());
                        ObjCVarvwChargeTypes.mInvoiceTypeName = Convert.ToString(dr["InvoiceTypeName"].ToString());
                        ObjCVarvwChargeTypes.mInvoiceTypeLocalName = Convert.ToString(dr["InvoiceTypeLocalName"].ToString());
                        ObjCVarvwChargeTypes.mMeasurementID = Convert.ToInt32(dr["MeasurementID"].ToString());
                        ObjCVarvwChargeTypes.mTaxeTypeID = Convert.ToInt32(dr["TaxeTypeID"].ToString());
                        ObjCVarvwChargeTypes.mViewOrder = Convert.ToInt32(dr["ViewOrder"].ToString());
                        ObjCVarvwChargeTypes.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwChargeTypes.mIsUsedInReceivable = Convert.ToBoolean(dr["IsUsedInReceivable"].ToString());
                        ObjCVarvwChargeTypes.mIsUsedInPayable = Convert.ToBoolean(dr["IsUsedInPayable"].ToString());
                        ObjCVarvwChargeTypes.mIsOcean = Convert.ToBoolean(dr["IsOcean"].ToString());
                        ObjCVarvwChargeTypes.mIsInland = Convert.ToBoolean(dr["IsInland"].ToString());
                        ObjCVarvwChargeTypes.mIsAir = Convert.ToBoolean(dr["IsAir"].ToString());
                        ObjCVarvwChargeTypes.mIsDefaultInQuotation = Convert.ToBoolean(dr["IsDefaultInQuotation"].ToString());
                        ObjCVarvwChargeTypes.mIsDefaultInOperations = Convert.ToBoolean(dr["IsDefaultInOperations"].ToString());
                        ObjCVarvwChargeTypes.mIsAddedManually = Convert.ToBoolean(dr["IsAddedManually"].ToString());
                        ObjCVarvwChargeTypes.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarvwChargeTypes.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwChargeTypes.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwChargeTypes.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwChargeTypes.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwChargeTypes.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarvwChargeTypes.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarvwChargeTypes.mMeasurementCode = Convert.ToString(dr["MeasurementCode"].ToString());
                        ObjCVarvwChargeTypes.mMeasurementName = Convert.ToString(dr["MeasurementName"].ToString());
                        ObjCVarvwChargeTypes.mTaxeTypeCode = Convert.ToString(dr["TaxeTypeCode"].ToString());
                        ObjCVarvwChargeTypes.mTaxeTypeName = Convert.ToString(dr["TaxeTypeName"].ToString());
                        ObjCVarvwChargeTypes.mTaxPercentage = Convert.ToDecimal(dr["TaxPercentage"].ToString());
                        ObjCVarvwChargeTypes.mIsGeneralChargeType = Convert.ToBoolean(dr["IsGeneralChargeType"].ToString());
                        ObjCVarvwChargeTypes.mIsOperationChargeType = Convert.ToBoolean(dr["IsOperationChargeType"].ToString());
                        ObjCVarvwChargeTypes.mAccountID_Revenue = Convert.ToInt32(dr["AccountID_Revenue"].ToString());
                        ObjCVarvwChargeTypes.mAccountName_Revenue = Convert.ToString(dr["AccountName_Revenue"].ToString());
                        ObjCVarvwChargeTypes.mSubAccountID_Revenue = Convert.ToInt32(dr["SubAccountID_Revenue"].ToString());
                        ObjCVarvwChargeTypes.mSubAccountName_Revenue = Convert.ToString(dr["SubAccountName_Revenue"].ToString());
                        ObjCVarvwChargeTypes.mAccountID_Return = Convert.ToInt32(dr["AccountID_Return"].ToString());
                        ObjCVarvwChargeTypes.mAccountName_Return = Convert.ToString(dr["AccountName_Return"].ToString());
                        ObjCVarvwChargeTypes.mSubAccountID_Return = Convert.ToInt32(dr["SubAccountID_Return"].ToString());
                        ObjCVarvwChargeTypes.mSubAccountName_Return = Convert.ToString(dr["SubAccountName_Return"].ToString());
                        ObjCVarvwChargeTypes.mCostCenterID_Revenue = Convert.ToInt32(dr["CostCenterID_Revenue"].ToString());
                        ObjCVarvwChargeTypes.mCostCenterName_Revenue = Convert.ToString(dr["CostCenterName_Revenue"].ToString());
                        ObjCVarvwChargeTypes.mAccountID_Expense = Convert.ToInt32(dr["AccountID_Expense"].ToString());
                        ObjCVarvwChargeTypes.mAccountName_Expense = Convert.ToString(dr["AccountName_Expense"].ToString());
                        ObjCVarvwChargeTypes.mSubAccountID_Expense = Convert.ToInt32(dr["SubAccountID_Expense"].ToString());
                        ObjCVarvwChargeTypes.mSubAccountName_Expense = Convert.ToString(dr["SubAccountName_Expense"].ToString());
                        ObjCVarvwChargeTypes.mCostCenterID_Expense = Convert.ToInt32(dr["CostCenterID_Expense"].ToString());
                        ObjCVarvwChargeTypes.mCostCenterName_Expense = Convert.ToString(dr["CostCenterName_Expense"].ToString());
                        ObjCVarvwChargeTypes.mTemplateID = Convert.ToInt32(dr["TemplateID"].ToString());
                        ObjCVarvwChargeTypes.mPurchaseItemID = Convert.ToInt64(dr["PurchaseItemID"].ToString());
                        ObjCVarvwChargeTypes.mPurchaseItemCode = Convert.ToString(dr["PurchaseItemCode"].ToString());
                        ObjCVarvwChargeTypes.mIsTank = Convert.ToBoolean(dr["IsTank"].ToString());
                        ObjCVarvwChargeTypes.mIsWarehouseChargeType = Convert.ToBoolean(dr["IsWarehouseChargeType"].ToString());
                        ObjCVarvwChargeTypes.mIsExcludeInTruckingOrderPrint = Convert.ToBoolean(dr["IsExcludeInTruckingOrderPrint"].ToString());
                        ObjCVarvwChargeTypes.mIsOfficial = Convert.ToBoolean(dr["IsOfficial"].ToString());
                        ObjCVarvwChargeTypes.mPreCode = Convert.ToString(dr["PreCode"].ToString());
                        ObjCVarvwChargeTypes.mChargeTypeGroupID = Convert.ToInt32(dr["ChargeTypeGroupID"].ToString());
                        ObjCVarvwChargeTypes.mChargeTypeGroupName = Convert.ToString(dr["ChargeTypeGroupName"].ToString());
                        ObjCVarvwChargeTypes.mCost = Convert.ToDecimal(dr["Cost"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwChargeTypes.Add(ObjCVarvwChargeTypes);
                    }
                }
                catch (Exception ex)
                {
                    Exp = ex;
                }
                finally
                {
                    if (dr != null)
                    {
                        dr.Close();
                        dr.Dispose();
                    }
                }
                tr.Commit();
            }
            catch (Exception ex)
            {
                Exp = ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
            }
            return Exp;
        }

        #endregion
    }
}
