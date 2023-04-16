﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.Accounting.Transactions.Generated
{
    [Serializable]
    public class CPKvwA_JVDetails
    {
        #region "variables"
        private Int64 mID;
        #endregion

        #region "Methods"
        public Int64 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        #endregion
    }
    [Serializable]
    public partial class CVarvwA_JVDetails : CPKvwA_JVDetails
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mJV_ID;
        internal Int32 mAccount_ID;
        internal String mAccount_Name;
        internal String mAccount_EnName;
        internal String mAccount_Number;
        internal Int32 mSubAccount_ID;
        internal String mSubAccount_Name;
        internal String mSubAccount_EnName;
        internal String mSubAccount_Number;
        internal Int32 mCostCenter_ID;
        internal String mCostCenterName;
        internal String mCostCenterNumber;
        internal Decimal mDebit;
        internal Int32 mBranch_ID;
        internal String mBranch_Name;
        internal Int64 mOperation_ID;
        internal String mOperation_Code;
        internal Decimal mCredit;
        internal Int32 mCurrency_ID;
        internal String mCurrencyCode;
        internal Decimal mExchangeRate;
        internal Decimal mLocalDebit;
        internal Decimal mLocalCredit;
        internal String mDescription;
        internal Boolean mIsDocumented;
        #endregion

        #region "Methods"
        public Int64 JV_ID
        {
            get { return mJV_ID; }
            set { mJV_ID = value; }
        }
        public Int32 Account_ID
        {
            get { return mAccount_ID; }
            set { mAccount_ID = value; }
        }
        public String Account_Name
        {
            get { return mAccount_Name; }
            set { mAccount_Name = value; }
        }
        public String Account_EnName
        {
            get { return mAccount_EnName; }
            set { mAccount_EnName = value; }
        }
        public String Account_Number
        {
            get { return mAccount_Number; }
            set { mAccount_Number = value; }
        }
        public Int32 SubAccount_ID
        {
            get { return mSubAccount_ID; }
            set { mSubAccount_ID = value; }
        }
        public String SubAccount_Name
        {
            get { return mSubAccount_Name; }
            set { mSubAccount_Name = value; }
        }
        public String SubAccount_EnName
        {
            get { return mSubAccount_EnName; }
            set { mSubAccount_EnName = value; }
        }
        public String SubAccount_Number
        {
            get { return mSubAccount_Number; }
            set { mSubAccount_Number = value; }
        }
        public Int32 CostCenter_ID
        {
            get { return mCostCenter_ID; }
            set { mCostCenter_ID = value; }
        }
        public String CostCenterName
        {
            get { return mCostCenterName; }
            set { mCostCenterName = value; }
        }
        public String CostCenterNumber
        {
            get { return mCostCenterNumber; }
            set { mCostCenterNumber = value; }
        }
        public Decimal Debit
        {
            get { return mDebit; }
            set { mDebit = value; }
        }
        public Int32 Branch_ID
        {
            get { return mBranch_ID; }
            set { mBranch_ID = value; }
        }
        public String Branch_Name
        {
            get { return mBranch_Name; }
            set { mBranch_Name = value; }
        }
        public Int64 Operation_ID
        {
            get { return mOperation_ID; }
            set { mOperation_ID = value; }
        }
        public String Operation_Code
        {
            get { return mOperation_Code; }
            set { mOperation_Code = value; }
        }
        public Decimal Credit
        {
            get { return mCredit; }
            set { mCredit = value; }
        }
        public Int32 Currency_ID
        {
            get { return mCurrency_ID; }
            set { mCurrency_ID = value; }
        }
        public String CurrencyCode
        {
            get { return mCurrencyCode; }
            set { mCurrencyCode = value; }
        }
        public Decimal ExchangeRate
        {
            get { return mExchangeRate; }
            set { mExchangeRate = value; }
        }
        public Decimal LocalDebit
        {
            get { return mLocalDebit; }
            set { mLocalDebit = value; }
        }
        public Decimal LocalCredit
        {
            get { return mLocalCredit; }
            set { mLocalCredit = value; }
        }
        public String Description
        {
            get { return mDescription; }
            set { mDescription = value; }
        }
        public Boolean IsDocumented
        {
            get { return mIsDocumented; }
            set { mIsDocumented = value; }
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

    public partial class CvwA_JVDetails
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
        public List<CVarvwA_JVDetails> lstCVarvwA_JVDetails = new List<CVarvwA_JVDetails>();
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
            lstCVarvwA_JVDetails.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwA_JVDetails";
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
                        CVarvwA_JVDetails ObjCVarvwA_JVDetails = new CVarvwA_JVDetails();
                        ObjCVarvwA_JVDetails.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwA_JVDetails.mJV_ID = Convert.ToInt64(dr["JV_ID"].ToString());
                        ObjCVarvwA_JVDetails.mAccount_ID = Convert.ToInt32(dr["Account_ID"].ToString());
                        ObjCVarvwA_JVDetails.mAccount_Name = Convert.ToString(dr["Account_Name"].ToString());
                        ObjCVarvwA_JVDetails.mAccount_EnName = Convert.ToString(dr["Account_EnName"].ToString());
                        ObjCVarvwA_JVDetails.mAccount_Number = Convert.ToString(dr["Account_Number"].ToString());
                        ObjCVarvwA_JVDetails.mSubAccount_ID = Convert.ToInt32(dr["SubAccount_ID"].ToString());
                        ObjCVarvwA_JVDetails.mSubAccount_Name = Convert.ToString(dr["SubAccount_Name"].ToString());
                        ObjCVarvwA_JVDetails.mSubAccount_EnName = Convert.ToString(dr["SubAccount_EnName"].ToString());
                        ObjCVarvwA_JVDetails.mSubAccount_Number = Convert.ToString(dr["SubAccount_Number"].ToString());
                        ObjCVarvwA_JVDetails.mCostCenter_ID = Convert.ToInt32(dr["CostCenter_ID"].ToString());
                        ObjCVarvwA_JVDetails.mCostCenterName = Convert.ToString(dr["CostCenterName"].ToString());
                        ObjCVarvwA_JVDetails.mCostCenterNumber = Convert.ToString(dr["CostCenterNumber"].ToString());
                        ObjCVarvwA_JVDetails.mDebit = Convert.ToDecimal(dr["Debit"].ToString());
                        ObjCVarvwA_JVDetails.mBranch_ID = Convert.ToInt32(dr["Branch_ID"].ToString());
                        ObjCVarvwA_JVDetails.mBranch_Name = Convert.ToString(dr["Branch_Name"].ToString());
                        ObjCVarvwA_JVDetails.mOperation_ID = Convert.ToInt64(dr["Operation_ID"].ToString());
                        ObjCVarvwA_JVDetails.mOperation_Code = Convert.ToString(dr["Operation_Code"].ToString());
                        ObjCVarvwA_JVDetails.mCredit = Convert.ToDecimal(dr["Credit"].ToString());
                        ObjCVarvwA_JVDetails.mCurrency_ID = Convert.ToInt32(dr["Currency_ID"].ToString());
                        ObjCVarvwA_JVDetails.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwA_JVDetails.mExchangeRate = Convert.ToDecimal(dr["ExchangeRate"].ToString());
                        ObjCVarvwA_JVDetails.mLocalDebit = Convert.ToDecimal(dr["LocalDebit"].ToString());
                        ObjCVarvwA_JVDetails.mLocalCredit = Convert.ToDecimal(dr["LocalCredit"].ToString());
                        ObjCVarvwA_JVDetails.mDescription = Convert.ToString(dr["Description"].ToString());
                        ObjCVarvwA_JVDetails.mIsDocumented = Convert.ToBoolean(dr["IsDocumented"].ToString());
                        lstCVarvwA_JVDetails.Add(ObjCVarvwA_JVDetails);
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
            lstCVarvwA_JVDetails.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwA_JVDetails";
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
                        CVarvwA_JVDetails ObjCVarvwA_JVDetails = new CVarvwA_JVDetails();
                        ObjCVarvwA_JVDetails.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwA_JVDetails.mJV_ID = Convert.ToInt64(dr["JV_ID"].ToString());
                        ObjCVarvwA_JVDetails.mAccount_ID = Convert.ToInt32(dr["Account_ID"].ToString());
                        ObjCVarvwA_JVDetails.mAccount_Name = Convert.ToString(dr["Account_Name"].ToString());
                        ObjCVarvwA_JVDetails.mAccount_EnName = Convert.ToString(dr["Account_EnName"].ToString());
                        ObjCVarvwA_JVDetails.mAccount_Number = Convert.ToString(dr["Account_Number"].ToString());
                        ObjCVarvwA_JVDetails.mSubAccount_ID = Convert.ToInt32(dr["SubAccount_ID"].ToString());
                        ObjCVarvwA_JVDetails.mSubAccount_Name = Convert.ToString(dr["SubAccount_Name"].ToString());
                        ObjCVarvwA_JVDetails.mSubAccount_EnName = Convert.ToString(dr["SubAccount_EnName"].ToString());
                        ObjCVarvwA_JVDetails.mSubAccount_Number = Convert.ToString(dr["SubAccount_Number"].ToString());
                        ObjCVarvwA_JVDetails.mCostCenter_ID = Convert.ToInt32(dr["CostCenter_ID"].ToString());
                        ObjCVarvwA_JVDetails.mCostCenterName = Convert.ToString(dr["CostCenterName"].ToString());
                        ObjCVarvwA_JVDetails.mCostCenterNumber = Convert.ToString(dr["CostCenterNumber"].ToString());
                        ObjCVarvwA_JVDetails.mDebit = Convert.ToDecimal(dr["Debit"].ToString());
                        ObjCVarvwA_JVDetails.mBranch_ID = Convert.ToInt32(dr["Branch_ID"].ToString());
                        ObjCVarvwA_JVDetails.mBranch_Name = Convert.ToString(dr["Branch_Name"].ToString());
                        ObjCVarvwA_JVDetails.mOperation_ID = Convert.ToInt64(dr["Operation_ID"].ToString());
                        ObjCVarvwA_JVDetails.mOperation_Code = Convert.ToString(dr["Operation_Code"].ToString());
                        ObjCVarvwA_JVDetails.mCredit = Convert.ToDecimal(dr["Credit"].ToString());
                        ObjCVarvwA_JVDetails.mCurrency_ID = Convert.ToInt32(dr["Currency_ID"].ToString());
                        ObjCVarvwA_JVDetails.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwA_JVDetails.mExchangeRate = Convert.ToDecimal(dr["ExchangeRate"].ToString());
                        ObjCVarvwA_JVDetails.mLocalDebit = Convert.ToDecimal(dr["LocalDebit"].ToString());
                        ObjCVarvwA_JVDetails.mLocalCredit = Convert.ToDecimal(dr["LocalCredit"].ToString());
                        ObjCVarvwA_JVDetails.mDescription = Convert.ToString(dr["Description"].ToString());
                        ObjCVarvwA_JVDetails.mIsDocumented = Convert.ToBoolean(dr["IsDocumented"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwA_JVDetails.Add(ObjCVarvwA_JVDetails);
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
