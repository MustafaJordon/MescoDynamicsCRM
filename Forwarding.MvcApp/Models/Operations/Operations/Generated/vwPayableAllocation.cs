﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace LogisticsWeb
{
    [Serializable]
    public partial class CVarvwPayablesAllocationsItems
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mID;
        internal Int64 mOperationID;
        internal Int32 mPartnerSupplierID;
        internal String mPartnerSupplierName;
        internal String mPartnerSupplierLocalName;
        internal Int32 mSupplierPartnerTypeID;
        internal String mPartnerTypeCode;
        internal String mOperationCode;
        internal String mPayableStatus;
        internal Int32 mCurrencyID;
        internal String mCurrencyCode;
        internal Decimal mCostPrice;
        internal Decimal mCostAmount;
        internal Decimal mRemainingAmount;
        internal Decimal mPaidAmount;
        internal String mSupplierInvoiceNo;
        internal String mChargeTypeName;
        internal DateTime mIssueDate;
        #endregion

        #region "Methods"
        public Int64 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public Int64 OperationID
        {
            get { return mOperationID; }
            set { mOperationID = value; }
        }
        public Int32 PartnerSupplierID
        {
            get { return mPartnerSupplierID; }
            set { mPartnerSupplierID = value; }
        }
        public String PartnerSupplierName
        {
            get { return mPartnerSupplierName; }
            set { mPartnerSupplierName = value; }
        }
        public String PartnerSupplierLocalName
        {
            get { return mPartnerSupplierLocalName; }
            set { mPartnerSupplierLocalName = value; }
        }
        public Int32 SupplierPartnerTypeID
        {
            get { return mSupplierPartnerTypeID; }
            set { mSupplierPartnerTypeID = value; }
        }
        public String PartnerTypeCode
        {
            get { return mPartnerTypeCode; }
            set { mPartnerTypeCode = value; }
        }
        public String OperationCode
        {
            get { return mOperationCode; }
            set { mOperationCode = value; }
        }
        public String PayableStatus
        {
            get { return mPayableStatus; }
            set { mPayableStatus = value; }
        }
        public Int32 CurrencyID
        {
            get { return mCurrencyID; }
            set { mCurrencyID = value; }
        }
        public String CurrencyCode
        {
            get { return mCurrencyCode; }
            set { mCurrencyCode = value; }
        }
        public Decimal CostPrice
        {
            get { return mCostPrice; }
            set { mCostPrice = value; }
        }
        public Decimal CostAmount
        {
            get { return mCostAmount; }
            set { mCostAmount = value; }
        }
        public Decimal RemainingAmount
        {
            get { return mRemainingAmount; }
            set { mRemainingAmount = value; }
        }
        public Decimal PaidAmount
        {
            get { return mPaidAmount; }
            set { mPaidAmount = value; }
        }
        public String SupplierInvoiceNo
        {
            get { return mSupplierInvoiceNo; }
            set { mSupplierInvoiceNo = value; }
        }
        public String ChargeTypeName
        {
            get { return mChargeTypeName; }
            set { mChargeTypeName = value; }
        }
        public DateTime IssueDate
        {
            get { return mIssueDate; }
            set { mIssueDate = value; }
        }
        #endregion
    }

    public partial class CvwPayablesAllocationsItems
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
        public List<CVarvwPayablesAllocationsItems> lstCVarvwPayablesAllocationsItems = new List<CVarvwPayablesAllocationsItems>();
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
            lstCVarvwPayablesAllocationsItems.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwPayablesAllocationsItems";
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
                        CVarvwPayablesAllocationsItems ObjCVarvwPayablesAllocationsItems = new CVarvwPayablesAllocationsItems();
                        ObjCVarvwPayablesAllocationsItems.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mPartnerSupplierID = Convert.ToInt32(dr["PartnerSupplierID"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mPartnerSupplierName = Convert.ToString(dr["PartnerSupplierName"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mPartnerSupplierLocalName = Convert.ToString(dr["PartnerSupplierLocalName"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mSupplierPartnerTypeID = Convert.ToInt32(dr["SupplierPartnerTypeID"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mPartnerTypeCode = Convert.ToString(dr["PartnerTypeCode"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mPayableStatus = Convert.ToString(dr["PayableStatus"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mCostPrice = Convert.ToDecimal(dr["CostPrice"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mCostAmount = Convert.ToDecimal(dr["CostAmount"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mRemainingAmount = Convert.ToDecimal(dr["RemainingAmount"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mPaidAmount = Convert.ToDecimal(dr["PaidAmount"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mSupplierInvoiceNo = Convert.ToString(dr["SupplierInvoiceNo"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mChargeTypeName = Convert.ToString(dr["ChargeTypeName"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mIssueDate = Convert.ToDateTime(dr["IssueDate"].ToString());
                        lstCVarvwPayablesAllocationsItems.Add(ObjCVarvwPayablesAllocationsItems);
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
            lstCVarvwPayablesAllocationsItems.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwPayablesAllocationsItems";
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
                        CVarvwPayablesAllocationsItems ObjCVarvwPayablesAllocationsItems = new CVarvwPayablesAllocationsItems();
                        ObjCVarvwPayablesAllocationsItems.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mPartnerSupplierID = Convert.ToInt32(dr["PartnerSupplierID"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mPartnerSupplierName = Convert.ToString(dr["PartnerSupplierName"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mPartnerSupplierLocalName = Convert.ToString(dr["PartnerSupplierLocalName"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mSupplierPartnerTypeID = Convert.ToInt32(dr["SupplierPartnerTypeID"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mPartnerTypeCode = Convert.ToString(dr["PartnerTypeCode"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mPayableStatus = Convert.ToString(dr["PayableStatus"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mCostPrice = Convert.ToDecimal(dr["CostPrice"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mCostAmount = Convert.ToDecimal(dr["CostAmount"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mRemainingAmount = Convert.ToDecimal(dr["RemainingAmount"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mPaidAmount = Convert.ToDecimal(dr["PaidAmount"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mSupplierInvoiceNo = Convert.ToString(dr["SupplierInvoiceNo"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mChargeTypeName = Convert.ToString(dr["ChargeTypeName"].ToString());
                        ObjCVarvwPayablesAllocationsItems.mIssueDate = Convert.ToDateTime(dr["IssueDate"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwPayablesAllocationsItems.Add(ObjCVarvwPayablesAllocationsItems);
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
