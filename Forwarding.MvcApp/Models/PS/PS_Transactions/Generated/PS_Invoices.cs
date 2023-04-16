﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.PS.PS_Transactions.Generated
{
    [Serializable]
    public class CPKPS_Invoices
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
    public partial class CVarPS_Invoices : CPKPS_Invoices
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mInvoiceNo;
        internal DateTime mInvoiceDate;
        internal Int32 mQuotationID;
        internal Int32 mSupplierID;
        internal Decimal mTotalBeforTax;
        internal Decimal mTotalPrice;
        internal Decimal mDiscount;
        internal Decimal mDiscountPercentage;
        internal String mNotes;
        internal Int32 mDepartmentID;
        internal Int32 mSalesManID;
        internal Int32 mCostCenter_ID;
        internal Int32 mPaymentMethodID;
        internal Boolean mIsApproved;
        internal Boolean mISDiscountBeforeTax;
        internal String mInvoiceNoManual;
        internal Int64 mOrderID;
        internal Int32 mJVID;
        internal Int32 mCurrencyID;
        internal Decimal mExchangeRate;
        internal Decimal mLocalTotalBeforeTax;
        internal Decimal mLocalTotal;
        internal Boolean mIsDeleted;
        internal Decimal mPaidAmount;
        internal Decimal mRemainAmount;
        internal String mSupplierInvoiceNo;
        internal Decimal mTaxesAmount;
        internal Decimal mItemsAmount;
        internal Decimal mServicesAmount;
        internal Decimal mExpensesAmount;
        internal Boolean mIsFixedDiscount;
        internal Boolean mIsFromTrans;
        internal Int32 mTransactionID;
        internal Int32 mEntitlementDays;
        internal Int64 mSupplyOrderID;
        internal Int32 mPaymentTermID;
        internal Int32 mBranchID;
        #endregion

        #region "Methods"
        public String InvoiceNo
        {
            get { return mInvoiceNo; }
            set { mIsChanges = true; mInvoiceNo = value; }
        }
        public DateTime InvoiceDate
        {
            get { return mInvoiceDate; }
            set { mIsChanges = true; mInvoiceDate = value; }
        }
        public Int32 QuotationID
        {
            get { return mQuotationID; }
            set { mIsChanges = true; mQuotationID = value; }
        }
        public Int32 SupplierID
        {
            get { return mSupplierID; }
            set { mIsChanges = true; mSupplierID = value; }
        }
        public Decimal TotalBeforTax
        {
            get { return mTotalBeforTax; }
            set { mIsChanges = true; mTotalBeforTax = value; }
        }
        public Decimal TotalPrice
        {
            get { return mTotalPrice; }
            set { mIsChanges = true; mTotalPrice = value; }
        }
        public Decimal Discount
        {
            get { return mDiscount; }
            set { mIsChanges = true; mDiscount = value; }
        }
        public Decimal DiscountPercentage
        {
            get { return mDiscountPercentage; }
            set { mIsChanges = true; mDiscountPercentage = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mIsChanges = true; mNotes = value; }
        }
        public Int32 DepartmentID
        {
            get { return mDepartmentID; }
            set { mIsChanges = true; mDepartmentID = value; }
        }
        public Int32 SalesManID
        {
            get { return mSalesManID; }
            set { mIsChanges = true; mSalesManID = value; }
        }
        public Int32 CostCenter_ID
        {
            get { return mCostCenter_ID; }
            set { mIsChanges = true; mCostCenter_ID = value; }
        }
        public Int32 PaymentMethodID
        {
            get { return mPaymentMethodID; }
            set { mIsChanges = true; mPaymentMethodID = value; }
        }
        public Boolean IsApproved
        {
            get { return mIsApproved; }
            set { mIsChanges = true; mIsApproved = value; }
        }
        public Boolean ISDiscountBeforeTax
        {
            get { return mISDiscountBeforeTax; }
            set { mIsChanges = true; mISDiscountBeforeTax = value; }
        }
        public String InvoiceNoManual
        {
            get { return mInvoiceNoManual; }
            set { mIsChanges = true; mInvoiceNoManual = value; }
        }
        public Int64 OrderID
        {
            get { return mOrderID; }
            set { mIsChanges = true; mOrderID = value; }
        }
        public Int32 JVID
        {
            get { return mJVID; }
            set { mIsChanges = true; mJVID = value; }
        }
        public Int32 CurrencyID
        {
            get { return mCurrencyID; }
            set { mIsChanges = true; mCurrencyID = value; }
        }
        public Decimal ExchangeRate
        {
            get { return mExchangeRate; }
            set { mIsChanges = true; mExchangeRate = value; }
        }
        public Decimal LocalTotalBeforeTax
        {
            get { return mLocalTotalBeforeTax; }
            set { mIsChanges = true; mLocalTotalBeforeTax = value; }
        }
        public Decimal LocalTotal
        {
            get { return mLocalTotal; }
            set { mIsChanges = true; mLocalTotal = value; }
        }
        public Boolean IsDeleted
        {
            get { return mIsDeleted; }
            set { mIsChanges = true; mIsDeleted = value; }
        }
        public Decimal PaidAmount
        {
            get { return mPaidAmount; }
            set { mIsChanges = true; mPaidAmount = value; }
        }
        public Decimal RemainAmount
        {
            get { return mRemainAmount; }
            set { mIsChanges = true; mRemainAmount = value; }
        }
        public String SupplierInvoiceNo
        {
            get { return mSupplierInvoiceNo; }
            set { mIsChanges = true; mSupplierInvoiceNo = value; }
        }
        public Decimal TaxesAmount
        {
            get { return mTaxesAmount; }
            set { mIsChanges = true; mTaxesAmount = value; }
        }
        public Decimal ItemsAmount
        {
            get { return mItemsAmount; }
            set { mIsChanges = true; mItemsAmount = value; }
        }
        public Decimal ServicesAmount
        {
            get { return mServicesAmount; }
            set { mIsChanges = true; mServicesAmount = value; }
        }
        public Decimal ExpensesAmount
        {
            get { return mExpensesAmount; }
            set { mIsChanges = true; mExpensesAmount = value; }
        }
        public Boolean IsFixedDiscount
        {
            get { return mIsFixedDiscount; }
            set { mIsChanges = true; mIsFixedDiscount = value; }
        }
        public Boolean IsFromTrans
        {
            get { return mIsFromTrans; }
            set { mIsChanges = true; mIsFromTrans = value; }
        }
        public Int32 TransactionID
        {
            get { return mTransactionID; }
            set { mIsChanges = true; mTransactionID = value; }
        }
        public Int32 EntitlementDays
        {
            get { return mEntitlementDays; }
            set { mIsChanges = true; mEntitlementDays = value; }
        }
        public Int64 SupplyOrderID
        {
            get { return mSupplyOrderID; }
            set { mIsChanges = true; mSupplyOrderID = value; }
        }
        public Int32 PaymentTermID
        {
            get { return mPaymentTermID; }
            set { mIsChanges = true; mPaymentTermID = value; }
        }
        public Int32 BranchID
        {
            get { return mBranchID; }
            set { mIsChanges = true; mBranchID = value; }
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

    public partial class CPS_Invoices
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
        public List<CVarPS_Invoices> lstCVarPS_Invoices = new List<CVarPS_Invoices>();
        public List<CPKPS_Invoices> lstDeletedCPKPS_Invoices = new List<CPKPS_Invoices>();
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
        public Exception GetItem(Int64 ID)
        {
            return DataFill(Convert.ToString(ID), false);
        }
        private Exception DataFill(string Param, Boolean IsList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarPS_Invoices.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListPS_Invoices";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemPS_Invoices";
                    Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt));
                    Com.Parameters[0].Value = Convert.ToInt64(Param);
                }
                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        /*Start DataReader*/
                        CVarPS_Invoices ObjCVarPS_Invoices = new CVarPS_Invoices();
                        ObjCVarPS_Invoices.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarPS_Invoices.mInvoiceNo = Convert.ToString(dr["InvoiceNo"].ToString());
                        ObjCVarPS_Invoices.mInvoiceDate = Convert.ToDateTime(dr["InvoiceDate"].ToString());
                        ObjCVarPS_Invoices.mQuotationID = Convert.ToInt32(dr["QuotationID"].ToString());
                        ObjCVarPS_Invoices.mSupplierID = Convert.ToInt32(dr["SupplierID"].ToString());
                        ObjCVarPS_Invoices.mTotalBeforTax = Convert.ToDecimal(dr["TotalBeforTax"].ToString());
                        ObjCVarPS_Invoices.mTotalPrice = Convert.ToDecimal(dr["TotalPrice"].ToString());
                        ObjCVarPS_Invoices.mDiscount = Convert.ToDecimal(dr["Discount"].ToString());
                        ObjCVarPS_Invoices.mDiscountPercentage = Convert.ToDecimal(dr["DiscountPercentage"].ToString());
                        ObjCVarPS_Invoices.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarPS_Invoices.mDepartmentID = Convert.ToInt32(dr["DepartmentID"].ToString());
                        ObjCVarPS_Invoices.mSalesManID = Convert.ToInt32(dr["SalesManID"].ToString());
                        ObjCVarPS_Invoices.mCostCenter_ID = Convert.ToInt32(dr["CostCenter_ID"].ToString());
                        ObjCVarPS_Invoices.mPaymentMethodID = Convert.ToInt32(dr["PaymentMethodID"].ToString());
                        ObjCVarPS_Invoices.mIsApproved = Convert.ToBoolean(dr["IsApproved"].ToString());
                        ObjCVarPS_Invoices.mISDiscountBeforeTax = Convert.ToBoolean(dr["ISDiscountBeforeTax"].ToString());
                        ObjCVarPS_Invoices.mInvoiceNoManual = Convert.ToString(dr["InvoiceNoManual"].ToString());
                        ObjCVarPS_Invoices.mOrderID = Convert.ToInt64(dr["OrderID"].ToString());
                        ObjCVarPS_Invoices.mJVID = Convert.ToInt32(dr["JVID"].ToString());
                        ObjCVarPS_Invoices.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarPS_Invoices.mExchangeRate = Convert.ToDecimal(dr["ExchangeRate"].ToString());
                        ObjCVarPS_Invoices.mLocalTotalBeforeTax = Convert.ToDecimal(dr["LocalTotalBeforeTax"].ToString());
                        ObjCVarPS_Invoices.mLocalTotal = Convert.ToDecimal(dr["LocalTotal"].ToString());
                        ObjCVarPS_Invoices.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarPS_Invoices.mPaidAmount = Convert.ToDecimal(dr["PaidAmount"].ToString());
                        ObjCVarPS_Invoices.mRemainAmount = Convert.ToDecimal(dr["RemainAmount"].ToString());
                        ObjCVarPS_Invoices.mSupplierInvoiceNo = Convert.ToString(dr["SupplierInvoiceNo"].ToString());
                        ObjCVarPS_Invoices.mTaxesAmount = Convert.ToDecimal(dr["TaxesAmount"].ToString());
                        ObjCVarPS_Invoices.mItemsAmount = Convert.ToDecimal(dr["ItemsAmount"].ToString());
                        ObjCVarPS_Invoices.mServicesAmount = Convert.ToDecimal(dr["ServicesAmount"].ToString());
                        ObjCVarPS_Invoices.mExpensesAmount = Convert.ToDecimal(dr["ExpensesAmount"].ToString());
                        ObjCVarPS_Invoices.mIsFixedDiscount = Convert.ToBoolean(dr["IsFixedDiscount"].ToString());
                        ObjCVarPS_Invoices.mIsFromTrans = Convert.ToBoolean(dr["IsFromTrans"].ToString());
                        ObjCVarPS_Invoices.mTransactionID = Convert.ToInt32(dr["TransactionID"].ToString());
                        ObjCVarPS_Invoices.mEntitlementDays = Convert.ToInt32(dr["EntitlementDays"].ToString());
                        ObjCVarPS_Invoices.mSupplyOrderID = Convert.ToInt64(dr["SupplyOrderID"].ToString());
                        ObjCVarPS_Invoices.mPaymentTermID = Convert.ToInt32(dr["PaymentTermID"].ToString());
                        ObjCVarPS_Invoices.mBranchID = Convert.ToInt32(dr["BranchID"].ToString());
                        lstCVarPS_Invoices.Add(ObjCVarPS_Invoices);
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
            lstCVarPS_Invoices.Clear();

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
                Com.CommandText = "[dbo].GetListPagingPS_Invoices";
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
                        CVarPS_Invoices ObjCVarPS_Invoices = new CVarPS_Invoices();
                        ObjCVarPS_Invoices.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarPS_Invoices.mInvoiceNo = Convert.ToString(dr["InvoiceNo"].ToString());
                        ObjCVarPS_Invoices.mInvoiceDate = Convert.ToDateTime(dr["InvoiceDate"].ToString());
                        ObjCVarPS_Invoices.mQuotationID = Convert.ToInt32(dr["QuotationID"].ToString());
                        ObjCVarPS_Invoices.mSupplierID = Convert.ToInt32(dr["SupplierID"].ToString());
                        ObjCVarPS_Invoices.mTotalBeforTax = Convert.ToDecimal(dr["TotalBeforTax"].ToString());
                        ObjCVarPS_Invoices.mTotalPrice = Convert.ToDecimal(dr["TotalPrice"].ToString());
                        ObjCVarPS_Invoices.mDiscount = Convert.ToDecimal(dr["Discount"].ToString());
                        ObjCVarPS_Invoices.mDiscountPercentage = Convert.ToDecimal(dr["DiscountPercentage"].ToString());
                        ObjCVarPS_Invoices.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarPS_Invoices.mDepartmentID = Convert.ToInt32(dr["DepartmentID"].ToString());
                        ObjCVarPS_Invoices.mSalesManID = Convert.ToInt32(dr["SalesManID"].ToString());
                        ObjCVarPS_Invoices.mCostCenter_ID = Convert.ToInt32(dr["CostCenter_ID"].ToString());
                        ObjCVarPS_Invoices.mPaymentMethodID = Convert.ToInt32(dr["PaymentMethodID"].ToString());
                        ObjCVarPS_Invoices.mIsApproved = Convert.ToBoolean(dr["IsApproved"].ToString());
                        ObjCVarPS_Invoices.mISDiscountBeforeTax = Convert.ToBoolean(dr["ISDiscountBeforeTax"].ToString());
                        ObjCVarPS_Invoices.mInvoiceNoManual = Convert.ToString(dr["InvoiceNoManual"].ToString());
                        ObjCVarPS_Invoices.mOrderID = Convert.ToInt64(dr["OrderID"].ToString());
                        ObjCVarPS_Invoices.mJVID = Convert.ToInt32(dr["JVID"].ToString());
                        ObjCVarPS_Invoices.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarPS_Invoices.mExchangeRate = Convert.ToDecimal(dr["ExchangeRate"].ToString());
                        ObjCVarPS_Invoices.mLocalTotalBeforeTax = Convert.ToDecimal(dr["LocalTotalBeforeTax"].ToString());
                        ObjCVarPS_Invoices.mLocalTotal = Convert.ToDecimal(dr["LocalTotal"].ToString());
                        ObjCVarPS_Invoices.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarPS_Invoices.mPaidAmount = Convert.ToDecimal(dr["PaidAmount"].ToString());
                        ObjCVarPS_Invoices.mRemainAmount = Convert.ToDecimal(dr["RemainAmount"].ToString());
                        ObjCVarPS_Invoices.mSupplierInvoiceNo = Convert.ToString(dr["SupplierInvoiceNo"].ToString());
                        ObjCVarPS_Invoices.mTaxesAmount = Convert.ToDecimal(dr["TaxesAmount"].ToString());
                        ObjCVarPS_Invoices.mItemsAmount = Convert.ToDecimal(dr["ItemsAmount"].ToString());
                        ObjCVarPS_Invoices.mServicesAmount = Convert.ToDecimal(dr["ServicesAmount"].ToString());
                        ObjCVarPS_Invoices.mExpensesAmount = Convert.ToDecimal(dr["ExpensesAmount"].ToString());
                        ObjCVarPS_Invoices.mIsFixedDiscount = Convert.ToBoolean(dr["IsFixedDiscount"].ToString());
                        ObjCVarPS_Invoices.mIsFromTrans = Convert.ToBoolean(dr["IsFromTrans"].ToString());
                        ObjCVarPS_Invoices.mTransactionID = Convert.ToInt32(dr["TransactionID"].ToString());
                        ObjCVarPS_Invoices.mEntitlementDays = Convert.ToInt32(dr["EntitlementDays"].ToString());
                        ObjCVarPS_Invoices.mSupplyOrderID = Convert.ToInt64(dr["SupplyOrderID"].ToString());
                        ObjCVarPS_Invoices.mPaymentTermID = Convert.ToInt32(dr["PaymentTermID"].ToString());
                        ObjCVarPS_Invoices.mBranchID = Convert.ToInt32(dr["BranchID"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarPS_Invoices.Add(ObjCVarPS_Invoices);
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
        #region "Common Methods"
        private void BeginTrans(SqlCommand Com, SqlConnection Con)
        {

            tr = Con.BeginTransaction(IsolationLevel.Serializable);
            Com.CommandType = CommandType.StoredProcedure;
        }

        private void EndTrans(SqlCommand Com, SqlConnection Con)
        {

            Com.Transaction = tr;
            Com.Connection = Con;
            Com.ExecuteNonQuery();
            tr.Commit();
        }

        #endregion
        #region "Set List Method"
        private Exception SetList(string WhereClause, Boolean IsDelete)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                if (IsDelete == true)
                    Com.CommandText = "[dbo].DeleteListPS_Invoices";
                else
                    Com.CommandText = "[dbo].UpdateListPS_Invoices";
                Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                BeginTrans(Com, Con);
                Com.Parameters[0].Value = WhereClause;
                EndTrans(Com, Con);
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
        #region "Delete Methods"
        public Exception DeleteItem(List<CPKPS_Invoices> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemPS_Invoices";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt));
                foreach (CPKPS_Invoices ObjCPKPS_Invoices in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt64(ObjCPKPS_Invoices.ID);
                    EndTrans(Com, Con);
                }
            }
            catch (Exception ex)
            {
                Exp = ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
                DeleteList.Clear();
            }
            return Exp;
        }

        public Exception DeleteList(string WhereClause)
        {

            return SetList(WhereClause, true);
        }

        #endregion
        #region "Save Methods"
        public Exception SaveMethod(List<CVarPS_Invoices> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@InvoiceNo", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@InvoiceDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@QuotationID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@SupplierID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@TotalBeforTax", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@TotalPrice", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@Discount", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@DiscountPercentage", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@DepartmentID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@SalesManID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CostCenter_ID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@PaymentMethodID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@IsApproved", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@ISDiscountBeforeTax", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@InvoiceNoManual", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@OrderID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@JVID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CurrencyID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ExchangeRate", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@LocalTotalBeforeTax", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@LocalTotal", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@IsDeleted", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@PaidAmount", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@RemainAmount", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@SupplierInvoiceNo", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@TaxesAmount", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@ItemsAmount", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@ServicesAmount", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@ExpensesAmount", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@IsFixedDiscount", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@IsFromTrans", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@TransactionID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@EntitlementDays", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@SupplyOrderID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@PaymentTermID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@BranchID", SqlDbType.Int));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarPS_Invoices ObjCVarPS_Invoices in SaveList)
                {
                    if (ObjCVarPS_Invoices.mIsChanges == true)
                    {
                        if (ObjCVarPS_Invoices.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemPS_Invoices";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarPS_Invoices.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemPS_Invoices";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarPS_Invoices.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarPS_Invoices.ID;
                        }
                        Com.Parameters["@InvoiceNo"].Value = ObjCVarPS_Invoices.InvoiceNo;
                        Com.Parameters["@InvoiceDate"].Value = ObjCVarPS_Invoices.InvoiceDate;
                        Com.Parameters["@QuotationID"].Value = ObjCVarPS_Invoices.QuotationID;
                        Com.Parameters["@SupplierID"].Value = ObjCVarPS_Invoices.SupplierID;
                        Com.Parameters["@TotalBeforTax"].Value = ObjCVarPS_Invoices.TotalBeforTax;
                        Com.Parameters["@TotalPrice"].Value = ObjCVarPS_Invoices.TotalPrice;
                        Com.Parameters["@Discount"].Value = ObjCVarPS_Invoices.Discount;
                        Com.Parameters["@DiscountPercentage"].Value = ObjCVarPS_Invoices.DiscountPercentage;
                        Com.Parameters["@Notes"].Value = ObjCVarPS_Invoices.Notes;
                        Com.Parameters["@DepartmentID"].Value = ObjCVarPS_Invoices.DepartmentID;
                        Com.Parameters["@SalesManID"].Value = ObjCVarPS_Invoices.SalesManID;
                        Com.Parameters["@CostCenter_ID"].Value = ObjCVarPS_Invoices.CostCenter_ID;
                        Com.Parameters["@PaymentMethodID"].Value = ObjCVarPS_Invoices.PaymentMethodID;
                        Com.Parameters["@IsApproved"].Value = ObjCVarPS_Invoices.IsApproved;
                        Com.Parameters["@ISDiscountBeforeTax"].Value = ObjCVarPS_Invoices.ISDiscountBeforeTax;
                        Com.Parameters["@InvoiceNoManual"].Value = ObjCVarPS_Invoices.InvoiceNoManual;
                        Com.Parameters["@OrderID"].Value = ObjCVarPS_Invoices.OrderID;
                        Com.Parameters["@JVID"].Value = ObjCVarPS_Invoices.JVID;
                        Com.Parameters["@CurrencyID"].Value = ObjCVarPS_Invoices.CurrencyID;
                        Com.Parameters["@ExchangeRate"].Value = ObjCVarPS_Invoices.ExchangeRate;
                        Com.Parameters["@LocalTotalBeforeTax"].Value = ObjCVarPS_Invoices.LocalTotalBeforeTax;
                        Com.Parameters["@LocalTotal"].Value = ObjCVarPS_Invoices.LocalTotal;
                        Com.Parameters["@IsDeleted"].Value = ObjCVarPS_Invoices.IsDeleted;
                        Com.Parameters["@PaidAmount"].Value = ObjCVarPS_Invoices.PaidAmount;
                        Com.Parameters["@RemainAmount"].Value = ObjCVarPS_Invoices.RemainAmount;
                        Com.Parameters["@SupplierInvoiceNo"].Value = ObjCVarPS_Invoices.SupplierInvoiceNo;
                        Com.Parameters["@TaxesAmount"].Value = ObjCVarPS_Invoices.TaxesAmount;
                        Com.Parameters["@ItemsAmount"].Value = ObjCVarPS_Invoices.ItemsAmount;
                        Com.Parameters["@ServicesAmount"].Value = ObjCVarPS_Invoices.ServicesAmount;
                        Com.Parameters["@ExpensesAmount"].Value = ObjCVarPS_Invoices.ExpensesAmount;
                        Com.Parameters["@IsFixedDiscount"].Value = ObjCVarPS_Invoices.IsFixedDiscount;
                        Com.Parameters["@IsFromTrans"].Value = ObjCVarPS_Invoices.IsFromTrans;
                        Com.Parameters["@TransactionID"].Value = ObjCVarPS_Invoices.TransactionID;
                        Com.Parameters["@EntitlementDays"].Value = ObjCVarPS_Invoices.EntitlementDays;
                        Com.Parameters["@SupplyOrderID"].Value = ObjCVarPS_Invoices.SupplyOrderID;
                        Com.Parameters["@PaymentTermID"].Value = ObjCVarPS_Invoices.PaymentTermID;
                        Com.Parameters["@BranchID"].Value = ObjCVarPS_Invoices.BranchID;
                        EndTrans(Com, Con);
                        if (ObjCVarPS_Invoices.ID == 0)
                        {
                            ObjCVarPS_Invoices.ID = Convert.ToInt64(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarPS_Invoices.mIsChanges = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Exp = ex;
                if (tr != null)
                    tr.Rollback();
            }
            finally
            {
                Con.Close();
                Con.Dispose();
            }
            return Exp;
        }
        #endregion
        #region "Update Methods"
        public Exception UpdateList(string UpdateClause)
        {

            return SetList(UpdateClause, false);
        }

        #endregion
    }

}
