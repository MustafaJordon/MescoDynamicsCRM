﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace Forwarding.MvcApp.Models.MasterData.Partners.Generated
{
    [Serializable]
    public partial class CVarvwSuppliers
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mID;
        internal Int32 mCode;
        internal String mName;
        internal String mLocalName;
        internal String mWebsite;
        internal Boolean mIsInactive;
        internal Boolean mIsDeleted;
        internal String mNotes;
        internal String mVATNumber;
        internal Int32 mPaymentTermID;
        internal Int32 mCurrencyID;
        internal Int32 mTaxeTypeID;
        internal Boolean mIsConsolidatedInvoice;
        internal String mBankName;
        internal String mBankAddress;
        internal String mSwift;
        internal String mBankAccountNumber;
        internal String mIBANNumber;
        internal DateTime mTimeLocked;
        internal String mPaymentTermCode;
        internal String mCurrencyCode;
        internal String mTaxeTypeCode;
        internal Int32 mAccountID;
        internal String mAccountName;
        internal Int32 mSubAccountID;
        internal String mSubAccountName;
        internal Int32 mCostCenterID;
        internal String mCostCenterName;
        internal Int32 mSubAccountGroupID;
        internal Int32 mOperationCount;
        internal String mSites;
        #endregion

        #region "Methods"
        public Int32 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public Int32 Code
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
        public String Website
        {
            get { return mWebsite; }
            set { mWebsite = value; }
        }
        public Boolean IsInactive
        {
            get { return mIsInactive; }
            set { mIsInactive = value; }
        }
        public Boolean IsDeleted
        {
            get { return mIsDeleted; }
            set { mIsDeleted = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mNotes = value; }
        }
        public String VATNumber
        {
            get { return mVATNumber; }
            set { mVATNumber = value; }
        }
        public Int32 PaymentTermID
        {
            get { return mPaymentTermID; }
            set { mPaymentTermID = value; }
        }
        public Int32 CurrencyID
        {
            get { return mCurrencyID; }
            set { mCurrencyID = value; }
        }
        public Int32 TaxeTypeID
        {
            get { return mTaxeTypeID; }
            set { mTaxeTypeID = value; }
        }
        public Boolean IsConsolidatedInvoice
        {
            get { return mIsConsolidatedInvoice; }
            set { mIsConsolidatedInvoice = value; }
        }
        public String BankName
        {
            get { return mBankName; }
            set { mBankName = value; }
        }
        public String BankAddress
        {
            get { return mBankAddress; }
            set { mBankAddress = value; }
        }
        public String Swift
        {
            get { return mSwift; }
            set { mSwift = value; }
        }
        public String BankAccountNumber
        {
            get { return mBankAccountNumber; }
            set { mBankAccountNumber = value; }
        }
        public String IBANNumber
        {
            get { return mIBANNumber; }
            set { mIBANNumber = value; }
        }
        public DateTime TimeLocked
        {
            get { return mTimeLocked; }
            set { mTimeLocked = value; }
        }
        public String PaymentTermCode
        {
            get { return mPaymentTermCode; }
            set { mPaymentTermCode = value; }
        }
        public String CurrencyCode
        {
            get { return mCurrencyCode; }
            set { mCurrencyCode = value; }
        }
        public String TaxeTypeCode
        {
            get { return mTaxeTypeCode; }
            set { mTaxeTypeCode = value; }
        }
        public Int32 AccountID
        {
            get { return mAccountID; }
            set { mAccountID = value; }
        }
        public String AccountName
        {
            get { return mAccountName; }
            set { mAccountName = value; }
        }
        public Int32 SubAccountID
        {
            get { return mSubAccountID; }
            set { mSubAccountID = value; }
        }
        public String SubAccountName
        {
            get { return mSubAccountName; }
            set { mSubAccountName = value; }
        }
        public Int32 CostCenterID
        {
            get { return mCostCenterID; }
            set { mCostCenterID = value; }
        }
        public String CostCenterName
        {
            get { return mCostCenterName; }
            set { mCostCenterName = value; }
        }
        public Int32 SubAccountGroupID
        {
            get { return mSubAccountGroupID; }
            set { mSubAccountGroupID = value; }
        }
        public Int32 OperationCount
        {
            get { return mOperationCount; }
            set { mOperationCount = value; }
        }
        public String Sites
        {
            get { return mSites; }
            set { mSites = value; }
        }
        #endregion
    }

    public partial class CvwSuppliers
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
        public List<CVarvwSuppliers> lstCVarvwSuppliers = new List<CVarvwSuppliers>();
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
            lstCVarvwSuppliers.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwSuppliers";
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
                        CVarvwSuppliers ObjCVarvwSuppliers = new CVarvwSuppliers();
                        ObjCVarvwSuppliers.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwSuppliers.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarvwSuppliers.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwSuppliers.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwSuppliers.mWebsite = Convert.ToString(dr["Website"].ToString());
                        ObjCVarvwSuppliers.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarvwSuppliers.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarvwSuppliers.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwSuppliers.mVATNumber = Convert.ToString(dr["VATNumber"].ToString());
                        ObjCVarvwSuppliers.mPaymentTermID = Convert.ToInt32(dr["PaymentTermID"].ToString());
                        ObjCVarvwSuppliers.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwSuppliers.mTaxeTypeID = Convert.ToInt32(dr["TaxeTypeID"].ToString());
                        ObjCVarvwSuppliers.mIsConsolidatedInvoice = Convert.ToBoolean(dr["IsConsolidatedInvoice"].ToString());
                        ObjCVarvwSuppliers.mBankName = Convert.ToString(dr["BankName"].ToString());
                        ObjCVarvwSuppliers.mBankAddress = Convert.ToString(dr["BankAddress"].ToString());
                        ObjCVarvwSuppliers.mSwift = Convert.ToString(dr["Swift"].ToString());
                        ObjCVarvwSuppliers.mBankAccountNumber = Convert.ToString(dr["BankAccountNumber"].ToString());
                        ObjCVarvwSuppliers.mIBANNumber = Convert.ToString(dr["IBANNumber"].ToString());
                        ObjCVarvwSuppliers.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarvwSuppliers.mPaymentTermCode = Convert.ToString(dr["PaymentTermCode"].ToString());
                        ObjCVarvwSuppliers.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwSuppliers.mTaxeTypeCode = Convert.ToString(dr["TaxeTypeCode"].ToString());
                        ObjCVarvwSuppliers.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarvwSuppliers.mAccountName = Convert.ToString(dr["AccountName"].ToString());
                        ObjCVarvwSuppliers.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarvwSuppliers.mSubAccountName = Convert.ToString(dr["SubAccountName"].ToString());
                        ObjCVarvwSuppliers.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarvwSuppliers.mCostCenterName = Convert.ToString(dr["CostCenterName"].ToString());
                        ObjCVarvwSuppliers.mSubAccountGroupID = Convert.ToInt32(dr["SubAccountGroupID"].ToString());
                        ObjCVarvwSuppliers.mOperationCount = Convert.ToInt32(dr["OperationCount"].ToString());
                        ObjCVarvwSuppliers.mSites = Convert.ToString(dr["Sites"].ToString());
                        lstCVarvwSuppliers.Add(ObjCVarvwSuppliers);
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
            lstCVarvwSuppliers.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwSuppliers";
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
                        CVarvwSuppliers ObjCVarvwSuppliers = new CVarvwSuppliers();
                        ObjCVarvwSuppliers.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwSuppliers.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarvwSuppliers.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwSuppliers.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwSuppliers.mWebsite = Convert.ToString(dr["Website"].ToString());
                        ObjCVarvwSuppliers.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarvwSuppliers.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarvwSuppliers.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwSuppliers.mVATNumber = Convert.ToString(dr["VATNumber"].ToString());
                        ObjCVarvwSuppliers.mPaymentTermID = Convert.ToInt32(dr["PaymentTermID"].ToString());
                        ObjCVarvwSuppliers.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwSuppliers.mTaxeTypeID = Convert.ToInt32(dr["TaxeTypeID"].ToString());
                        ObjCVarvwSuppliers.mIsConsolidatedInvoice = Convert.ToBoolean(dr["IsConsolidatedInvoice"].ToString());
                        ObjCVarvwSuppliers.mBankName = Convert.ToString(dr["BankName"].ToString());
                        ObjCVarvwSuppliers.mBankAddress = Convert.ToString(dr["BankAddress"].ToString());
                        ObjCVarvwSuppliers.mSwift = Convert.ToString(dr["Swift"].ToString());
                        ObjCVarvwSuppliers.mBankAccountNumber = Convert.ToString(dr["BankAccountNumber"].ToString());
                        ObjCVarvwSuppliers.mIBANNumber = Convert.ToString(dr["IBANNumber"].ToString());
                        ObjCVarvwSuppliers.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarvwSuppliers.mPaymentTermCode = Convert.ToString(dr["PaymentTermCode"].ToString());
                        ObjCVarvwSuppliers.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwSuppliers.mTaxeTypeCode = Convert.ToString(dr["TaxeTypeCode"].ToString());
                        ObjCVarvwSuppliers.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarvwSuppliers.mAccountName = Convert.ToString(dr["AccountName"].ToString());
                        ObjCVarvwSuppliers.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarvwSuppliers.mSubAccountName = Convert.ToString(dr["SubAccountName"].ToString());
                        ObjCVarvwSuppliers.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarvwSuppliers.mCostCenterName = Convert.ToString(dr["CostCenterName"].ToString());
                        ObjCVarvwSuppliers.mSubAccountGroupID = Convert.ToInt32(dr["SubAccountGroupID"].ToString());
                        ObjCVarvwSuppliers.mOperationCount = Convert.ToInt32(dr["OperationCount"].ToString());
                        ObjCVarvwSuppliers.mSites = Convert.ToString(dr["Sites"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwSuppliers.Add(ObjCVarvwSuppliers);
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
