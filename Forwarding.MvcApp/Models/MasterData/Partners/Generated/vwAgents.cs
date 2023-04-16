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
    public class CPKvwAgents
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
    public partial class CVarvwAgents : CPKvwAgents
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mCode;
        internal String mName;
        internal String mLocalName;
        internal String mWebsite;
        internal String mEmail;
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
        internal String mAddress;
        internal String mPhonesAndFaxes;
        internal Int32 mOperationCount;
        #endregion

        #region "Methods"
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
        public String Email
        {
            get { return mEmail; }
            set { mEmail = value; }
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
        public String Address
        {
            get { return mAddress; }
            set { mAddress = value; }
        }
        public String PhonesAndFaxes
        {
            get { return mPhonesAndFaxes; }
            set { mPhonesAndFaxes = value; }
        }
        public Int32 OperationCount
        {
            get { return mOperationCount; }
            set { mOperationCount = value; }
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

    public partial class CvwAgents
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
        public List<CVarvwAgents> lstCVarvwAgents = new List<CVarvwAgents>();
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
            lstCVarvwAgents.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwAgents";
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
                        CVarvwAgents ObjCVarvwAgents = new CVarvwAgents();
                        ObjCVarvwAgents.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwAgents.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarvwAgents.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwAgents.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwAgents.mWebsite = Convert.ToString(dr["Website"].ToString());
                        ObjCVarvwAgents.mEmail = Convert.ToString(dr["Email"].ToString());
                        ObjCVarvwAgents.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarvwAgents.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarvwAgents.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwAgents.mVATNumber = Convert.ToString(dr["VATNumber"].ToString());
                        ObjCVarvwAgents.mPaymentTermID = Convert.ToInt32(dr["PaymentTermID"].ToString());
                        ObjCVarvwAgents.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwAgents.mTaxeTypeID = Convert.ToInt32(dr["TaxeTypeID"].ToString());
                        ObjCVarvwAgents.mIsConsolidatedInvoice = Convert.ToBoolean(dr["IsConsolidatedInvoice"].ToString());
                        ObjCVarvwAgents.mBankName = Convert.ToString(dr["BankName"].ToString());
                        ObjCVarvwAgents.mBankAddress = Convert.ToString(dr["BankAddress"].ToString());
                        ObjCVarvwAgents.mSwift = Convert.ToString(dr["Swift"].ToString());
                        ObjCVarvwAgents.mBankAccountNumber = Convert.ToString(dr["BankAccountNumber"].ToString());
                        ObjCVarvwAgents.mIBANNumber = Convert.ToString(dr["IBANNumber"].ToString());
                        ObjCVarvwAgents.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarvwAgents.mPaymentTermCode = Convert.ToString(dr["PaymentTermCode"].ToString());
                        ObjCVarvwAgents.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwAgents.mTaxeTypeCode = Convert.ToString(dr["TaxeTypeCode"].ToString());
                        ObjCVarvwAgents.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarvwAgents.mAccountName = Convert.ToString(dr["AccountName"].ToString());
                        ObjCVarvwAgents.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarvwAgents.mSubAccountName = Convert.ToString(dr["SubAccountName"].ToString());
                        ObjCVarvwAgents.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarvwAgents.mCostCenterName = Convert.ToString(dr["CostCenterName"].ToString());
                        ObjCVarvwAgents.mSubAccountGroupID = Convert.ToInt32(dr["SubAccountGroupID"].ToString());
                        ObjCVarvwAgents.mAddress = Convert.ToString(dr["Address"].ToString());
                        ObjCVarvwAgents.mPhonesAndFaxes = Convert.ToString(dr["PhonesAndFaxes"].ToString());
                        ObjCVarvwAgents.mOperationCount = Convert.ToInt32(dr["OperationCount"].ToString());
                        lstCVarvwAgents.Add(ObjCVarvwAgents);
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
            lstCVarvwAgents.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwAgents";
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
                        CVarvwAgents ObjCVarvwAgents = new CVarvwAgents();
                        ObjCVarvwAgents.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwAgents.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarvwAgents.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwAgents.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwAgents.mWebsite = Convert.ToString(dr["Website"].ToString());
                        ObjCVarvwAgents.mEmail = Convert.ToString(dr["Email"].ToString());
                        ObjCVarvwAgents.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarvwAgents.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarvwAgents.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwAgents.mVATNumber = Convert.ToString(dr["VATNumber"].ToString());
                        ObjCVarvwAgents.mPaymentTermID = Convert.ToInt32(dr["PaymentTermID"].ToString());
                        ObjCVarvwAgents.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwAgents.mTaxeTypeID = Convert.ToInt32(dr["TaxeTypeID"].ToString());
                        ObjCVarvwAgents.mIsConsolidatedInvoice = Convert.ToBoolean(dr["IsConsolidatedInvoice"].ToString());
                        ObjCVarvwAgents.mBankName = Convert.ToString(dr["BankName"].ToString());
                        ObjCVarvwAgents.mBankAddress = Convert.ToString(dr["BankAddress"].ToString());
                        ObjCVarvwAgents.mSwift = Convert.ToString(dr["Swift"].ToString());
                        ObjCVarvwAgents.mBankAccountNumber = Convert.ToString(dr["BankAccountNumber"].ToString());
                        ObjCVarvwAgents.mIBANNumber = Convert.ToString(dr["IBANNumber"].ToString());
                        ObjCVarvwAgents.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarvwAgents.mPaymentTermCode = Convert.ToString(dr["PaymentTermCode"].ToString());
                        ObjCVarvwAgents.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwAgents.mTaxeTypeCode = Convert.ToString(dr["TaxeTypeCode"].ToString());
                        ObjCVarvwAgents.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarvwAgents.mAccountName = Convert.ToString(dr["AccountName"].ToString());
                        ObjCVarvwAgents.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarvwAgents.mSubAccountName = Convert.ToString(dr["SubAccountName"].ToString());
                        ObjCVarvwAgents.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarvwAgents.mCostCenterName = Convert.ToString(dr["CostCenterName"].ToString());
                        ObjCVarvwAgents.mSubAccountGroupID = Convert.ToInt32(dr["SubAccountGroupID"].ToString());
                        ObjCVarvwAgents.mAddress = Convert.ToString(dr["Address"].ToString());
                        ObjCVarvwAgents.mPhonesAndFaxes = Convert.ToString(dr["PhonesAndFaxes"].ToString());
                        ObjCVarvwAgents.mOperationCount = Convert.ToInt32(dr["OperationCount"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwAgents.Add(ObjCVarvwAgents);
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
