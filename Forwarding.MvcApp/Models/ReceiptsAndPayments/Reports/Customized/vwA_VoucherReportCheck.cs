﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Shipping.MvcApp.Models.Accounting.Reports.Generated
{
    [Serializable]
    public partial class CVarvwA_VoucherReportCheck
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mID;
        internal Int32 mCurrencyID;
        internal String mCode;
        internal String mName;
        internal Boolean mIsApproved;
        internal Int32 mVoucherType;
        internal String mVoucherTypeName;
        internal String mAccountName;
        internal String mAccount_Number;
        internal String mCurrencyCode;
        internal Decimal mTotal;
        internal Decimal mTotalAfterTax;
        internal DateTime mChequeDate;
        internal DateTime mCollectionDate;
        internal DateTime mVoucherDate;
        internal String mNotes;
        internal Int32 mBankID;
        internal Boolean mUnderCollection;
        internal Boolean mCollected;
        internal Boolean mReturned;
        internal String mChequeStatus;
        internal String mChequeNo;
        #endregion

        #region "Methods"
        public Int64 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public Int32 CurrencyID
        {
            get { return mCurrencyID; }
            set { mCurrencyID = value; }
        }
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
        public Boolean IsApproved
        {
            get { return mIsApproved; }
            set { mIsApproved = value; }
        }
        public Int32 VoucherType
        {
            get { return mVoucherType; }
            set { mVoucherType = value; }
        }
        public String VoucherTypeName
        {
            get { return mVoucherTypeName; }
            set { mVoucherTypeName = value; }
        }
        public String AccountName
        {
            get { return mAccountName; }
            set { mAccountName = value; }
        }
        public String Account_Number
        {
            get { return mAccount_Number; }
            set { mAccount_Number = value; }
        }
        public String CurrencyCode
        {
            get { return mCurrencyCode; }
            set { mCurrencyCode = value; }
        }
        public Decimal Total
        {
            get { return mTotal; }
            set { mTotal = value; }
        }
        public Decimal TotalAfterTax
        {
            get { return mTotalAfterTax; }
            set { mTotalAfterTax = value; }
        }
        public DateTime ChequeDate
        {
            get { return mChequeDate; }
            set { mChequeDate = value; }
        }
        public DateTime CollectionDate
        {
            get { return mCollectionDate; }
            set { mCollectionDate = value; }
        }
        public DateTime VoucherDate
        {
            get { return mVoucherDate; }
            set { mVoucherDate = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mNotes = value; }
        }
        public Int32 BankID
        {
            get { return mBankID; }
            set { mBankID = value; }
        }
        public Boolean UnderCollection
        {
            get { return mUnderCollection; }
            set { mUnderCollection = value; }
        }
        public Boolean Collected
        {
            get { return mCollected; }
            set { mCollected = value; }
        }
        public Boolean Returned
        {
            get { return mReturned; }
            set { mReturned = value; }
        }
        public String ChequeStatus
        {
            get { return mChequeStatus; }
            set { mChequeStatus = value; }
        }
        public String ChequeNo
        {
            get { return mChequeNo; }
            set { mChequeNo = value; }
        }
        #endregion
    }

    public partial class CvwA_VoucherReportCheck
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
        public List<CVarvwA_VoucherReportCheck> lstCVarvwA_VoucherReportCheck = new List<CVarvwA_VoucherReportCheck>();
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
            lstCVarvwA_VoucherReportCheck.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwA_VoucherReportCheck";
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
                        CVarvwA_VoucherReportCheck ObjCVarvwA_VoucherReportCheck = new CVarvwA_VoucherReportCheck();
                        ObjCVarvwA_VoucherReportCheck.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mIsApproved = Convert.ToBoolean(dr["IsApproved"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mVoucherType = Convert.ToInt32(dr["VoucherType"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mVoucherTypeName = Convert.ToString(dr["VoucherTypeName"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mAccountName = Convert.ToString(dr["AccountName"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mAccount_Number = Convert.ToString(dr["Account_Number"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mTotal = Convert.ToDecimal(dr["Total"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mTotalAfterTax = Convert.ToDecimal(dr["TotalAfterTax"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mChequeDate = Convert.ToDateTime(dr["ChequeDate"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mCollectionDate = Convert.ToDateTime(dr["CollectionDate"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mVoucherDate = Convert.ToDateTime(dr["VoucherDate"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mBankID = Convert.ToInt32(dr["BankID"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mUnderCollection = Convert.ToBoolean(dr["UnderCollection"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mCollected = Convert.ToBoolean(dr["Collected"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mReturned = Convert.ToBoolean(dr["Returned"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mChequeStatus = Convert.ToString(dr["ChequeStatus"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mChequeNo = Convert.ToString(dr["ChequeNo"].ToString());
                        lstCVarvwA_VoucherReportCheck.Add(ObjCVarvwA_VoucherReportCheck);
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
            lstCVarvwA_VoucherReportCheck.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwA_VoucherReportCheck";
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
                        CVarvwA_VoucherReportCheck ObjCVarvwA_VoucherReportCheck = new CVarvwA_VoucherReportCheck();
                        ObjCVarvwA_VoucherReportCheck.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mIsApproved = Convert.ToBoolean(dr["IsApproved"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mVoucherType = Convert.ToInt32(dr["VoucherType"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mVoucherTypeName = Convert.ToString(dr["VoucherTypeName"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mAccountName = Convert.ToString(dr["AccountName"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mAccount_Number = Convert.ToString(dr["Account_Number"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mTotal = Convert.ToDecimal(dr["Total"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mTotalAfterTax = Convert.ToDecimal(dr["TotalAfterTax"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mChequeDate = Convert.ToDateTime(dr["ChequeDate"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mCollectionDate = Convert.ToDateTime(dr["CollectionDate"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mVoucherDate = Convert.ToDateTime(dr["VoucherDate"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mBankID = Convert.ToInt32(dr["BankID"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mUnderCollection = Convert.ToBoolean(dr["UnderCollection"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mCollected = Convert.ToBoolean(dr["Collected"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mReturned = Convert.ToBoolean(dr["Returned"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mChequeStatus = Convert.ToString(dr["ChequeStatus"].ToString());
                        ObjCVarvwA_VoucherReportCheck.mChequeNo = Convert.ToString(dr["ChequeNo"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwA_VoucherReportCheck.Add(ObjCVarvwA_VoucherReportCheck);
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
