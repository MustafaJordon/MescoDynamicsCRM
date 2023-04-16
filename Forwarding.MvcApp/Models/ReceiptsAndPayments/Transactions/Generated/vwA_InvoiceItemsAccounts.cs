﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.ReceiptsAndPayments.Transactions.Generated
{
    [Serializable]
    public partial class CVarvwA_InvoiceItemsAccounts
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mInvoiceID;
        internal Decimal mSaleAmount;
        internal String mBLNumber;
        internal Int64 mInvoiceNumber;
        internal String mOperationNo;
        internal String mItemName;
        internal Int32 mAccountID_Revenue;
        internal Int32 mAccountID_Expense;
        internal Int32 mCostCenterID_Expense;
        internal Int32 mCostCenterID_Revenue;
        internal Int32 mSubAccountID_Expense;
        internal Int32 mSubAccountID_Revenue;
        internal String mDescription;
        #endregion

        #region "Methods"
        public Int64 InvoiceID
        {
            get { return mInvoiceID; }
            set { mInvoiceID = value; }
        }
        public Decimal SaleAmount
        {
            get { return mSaleAmount; }
            set { mSaleAmount = value; }
        }
        public String BLNumber
        {
            get { return mBLNumber; }
            set { mBLNumber = value; }
        }
        public Int64 InvoiceNumber
        {
            get { return mInvoiceNumber; }
            set { mInvoiceNumber = value; }
        }
        public String OperationNo
        {
            get { return mOperationNo; }
            set { mOperationNo = value; }
        }
        public String ItemName
        {
            get { return mItemName; }
            set { mItemName = value; }
        }
        public Int32 AccountID_Revenue
        {
            get { return mAccountID_Revenue; }
            set { mAccountID_Revenue = value; }
        }
        public Int32 AccountID_Expense
        {
            get { return mAccountID_Expense; }
            set { mAccountID_Expense = value; }
        }
        public Int32 CostCenterID_Expense
        {
            get { return mCostCenterID_Expense; }
            set { mCostCenterID_Expense = value; }
        }
        public Int32 CostCenterID_Revenue
        {
            get { return mCostCenterID_Revenue; }
            set { mCostCenterID_Revenue = value; }
        }
        public Int32 SubAccountID_Expense
        {
            get { return mSubAccountID_Expense; }
            set { mSubAccountID_Expense = value; }
        }
        public Int32 SubAccountID_Revenue
        {
            get { return mSubAccountID_Revenue; }
            set { mSubAccountID_Revenue = value; }
        }
        public String Description
        {
            get { return mDescription; }
            set { mDescription = value; }
        }
        #endregion
    }

    public partial class CvwA_InvoiceItemsAccounts
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
        public List<CVarvwA_InvoiceItemsAccounts> lstCVarvwA_InvoiceItemsAccounts = new List<CVarvwA_InvoiceItemsAccounts>();
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
            lstCVarvwA_InvoiceItemsAccounts.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwA_InvoiceItemsAccounts";
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
                        CVarvwA_InvoiceItemsAccounts ObjCVarvwA_InvoiceItemsAccounts = new CVarvwA_InvoiceItemsAccounts();
                        ObjCVarvwA_InvoiceItemsAccounts.mInvoiceID = Convert.ToInt64(dr["InvoiceID"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mSaleAmount = Convert.ToDecimal(dr["SaleAmount"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mBLNumber = Convert.ToString(dr["BLNumber"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mInvoiceNumber = Convert.ToInt64(dr["InvoiceNumber"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mOperationNo = Convert.ToString(dr["OperationNo"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mItemName = Convert.ToString(dr["ItemName"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mAccountID_Revenue = Convert.ToInt32(dr["AccountID_Revenue"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mAccountID_Expense = Convert.ToInt32(dr["AccountID_Expense"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mCostCenterID_Expense = Convert.ToInt32(dr["CostCenterID_Expense"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mCostCenterID_Revenue = Convert.ToInt32(dr["CostCenterID_Revenue"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mSubAccountID_Expense = Convert.ToInt32(dr["SubAccountID_Expense"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mSubAccountID_Revenue = Convert.ToInt32(dr["SubAccountID_Revenue"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mDescription = Convert.ToString(dr["Description"].ToString());
                        lstCVarvwA_InvoiceItemsAccounts.Add(ObjCVarvwA_InvoiceItemsAccounts);
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
            lstCVarvwA_InvoiceItemsAccounts.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwA_InvoiceItemsAccounts";
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
                        CVarvwA_InvoiceItemsAccounts ObjCVarvwA_InvoiceItemsAccounts = new CVarvwA_InvoiceItemsAccounts();
                        ObjCVarvwA_InvoiceItemsAccounts.mInvoiceID = Convert.ToInt64(dr["InvoiceID"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mSaleAmount = Convert.ToDecimal(dr["SaleAmount"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mBLNumber = Convert.ToString(dr["BLNumber"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mInvoiceNumber = Convert.ToInt64(dr["InvoiceNumber"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mOperationNo = Convert.ToString(dr["OperationNo"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mItemName = Convert.ToString(dr["ItemName"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mAccountID_Revenue = Convert.ToInt32(dr["AccountID_Revenue"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mAccountID_Expense = Convert.ToInt32(dr["AccountID_Expense"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mCostCenterID_Expense = Convert.ToInt32(dr["CostCenterID_Expense"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mCostCenterID_Revenue = Convert.ToInt32(dr["CostCenterID_Revenue"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mSubAccountID_Expense = Convert.ToInt32(dr["SubAccountID_Expense"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mSubAccountID_Revenue = Convert.ToInt32(dr["SubAccountID_Revenue"].ToString());
                        ObjCVarvwA_InvoiceItemsAccounts.mDescription = Convert.ToString(dr["Description"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwA_InvoiceItemsAccounts.Add(ObjCVarvwA_InvoiceItemsAccounts);
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
