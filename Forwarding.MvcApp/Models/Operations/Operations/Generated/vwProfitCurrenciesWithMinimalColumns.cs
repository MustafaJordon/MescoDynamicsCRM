﻿using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.Operations.Operations.Generated
{
    [Serializable]
    public partial class CVarvwProfitCurrenciesWithMinimalColumns
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mOperationID;
        internal String mOperationCode;
        internal Int64 mMasterOperationID;
        internal String mCurrencyCode;
        internal Int32 mChargeTypeID;
        internal String mChargeTypeName;
        internal Boolean mIsOfficial;
        internal Decimal mQuotationCost;
        internal Decimal mPayablesWithVAT;
        internal Decimal mPayablesWithoutVAT;
        internal Decimal mReceivablesWithVAT;
        internal Decimal mReceivablesWithoutVAT;
        internal Decimal mExchangeRate;
        internal Int32 mIsUsedInOperationStatement;
        internal String mAccNoteType;
        internal DateTime mPayableDate;
        internal DateTime mInvoiceDate;
        #endregion

        #region "Methods"
        public Int64 OperationID
        {
            get { return mOperationID; }
            set { mOperationID = value; }
        }
        public String OperationCode
        {
            get { return mOperationCode; }
            set { mOperationCode = value; }
        }
        public Int64 MasterOperationID
        {
            get { return mMasterOperationID; }
            set { mMasterOperationID = value; }
        }
        public String CurrencyCode
        {
            get { return mCurrencyCode; }
            set { mCurrencyCode = value; }
        }
        public Int32 ChargeTypeID
        {
            get { return mChargeTypeID; }
            set { mChargeTypeID = value; }
        }
        public String ChargeTypeName
        {
            get { return mChargeTypeName; }
            set { mChargeTypeName = value; }
        }
        public Boolean IsOfficial
        {
            get { return mIsOfficial; }
            set { mIsOfficial = value; }
        }
        public Decimal QuotationCost
        {
            get { return mQuotationCost; }
            set { mQuotationCost = value; }
        }
        public Decimal PayablesWithVAT
        {
            get { return mPayablesWithVAT; }
            set { mPayablesWithVAT = value; }
        }
        public Decimal PayablesWithoutVAT
        {
            get { return mPayablesWithoutVAT; }
            set { mPayablesWithoutVAT = value; }
        }
        public Decimal ReceivablesWithVAT
        {
            get { return mReceivablesWithVAT; }
            set { mReceivablesWithVAT = value; }
        }
        public Decimal ReceivablesWithoutVAT
        {
            get { return mReceivablesWithoutVAT; }
            set { mReceivablesWithoutVAT = value; }
        }
        public Decimal ExchangeRate
        {
            get { return mExchangeRate; }
            set { mExchangeRate = value; }
        }
        public Int32 IsUsedInOperationStatement
        {
            get { return mIsUsedInOperationStatement; }
            set { mIsUsedInOperationStatement = value; }
        }
        public String AccNoteType
        {
            get { return mAccNoteType; }
            set { mAccNoteType = value; }
        }
        public DateTime PayableDate
        {
            get { return mPayableDate; }
            set { mPayableDate = value; }
        }
        public DateTime InvoiceDate
        {
            get { return mInvoiceDate; }
            set { mInvoiceDate = value; }
        }
        #endregion
    }

    public partial class CvwProfitCurrenciesWithMinimalColumns
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
        public List<CVarvwProfitCurrenciesWithMinimalColumns> lstCVarvwProfitCurrenciesWithMinimalColumns = new List<CVarvwProfitCurrenciesWithMinimalColumns>();
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
            lstCVarvwProfitCurrenciesWithMinimalColumns.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwProfitCurrenciesWithMinimalColumns";
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
                        CVarvwProfitCurrenciesWithMinimalColumns ObjCVarvwProfitCurrenciesWithMinimalColumns = new CVarvwProfitCurrenciesWithMinimalColumns();
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mMasterOperationID = Convert.ToInt64(dr["MasterOperationID"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mChargeTypeID = Convert.ToInt32(dr["ChargeTypeID"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mChargeTypeName = Convert.ToString(dr["ChargeTypeName"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mIsOfficial = Convert.ToBoolean(dr["IsOfficial"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mQuotationCost = Convert.ToDecimal(dr["QuotationCost"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mPayablesWithVAT = Convert.ToDecimal(dr["PayablesWithVAT"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mPayablesWithoutVAT = Convert.ToDecimal(dr["PayablesWithoutVAT"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mReceivablesWithVAT = Convert.ToDecimal(dr["ReceivablesWithVAT"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mReceivablesWithoutVAT = Convert.ToDecimal(dr["ReceivablesWithoutVAT"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mExchangeRate = Convert.ToDecimal(dr["ExchangeRate"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mIsUsedInOperationStatement = Convert.ToInt32(dr["IsUsedInOperationStatement"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mAccNoteType = Convert.ToString(dr["AccNoteType"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mPayableDate = Convert.ToDateTime(dr["PayableDate"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mInvoiceDate = Convert.ToDateTime(dr["InvoiceDate"].ToString());
                        lstCVarvwProfitCurrenciesWithMinimalColumns.Add(ObjCVarvwProfitCurrenciesWithMinimalColumns);
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
            lstCVarvwProfitCurrenciesWithMinimalColumns.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwProfitCurrenciesWithMinimalColumns";
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
                        CVarvwProfitCurrenciesWithMinimalColumns ObjCVarvwProfitCurrenciesWithMinimalColumns = new CVarvwProfitCurrenciesWithMinimalColumns();
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mMasterOperationID = Convert.ToInt64(dr["MasterOperationID"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mChargeTypeID = Convert.ToInt32(dr["ChargeTypeID"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mChargeTypeName = Convert.ToString(dr["ChargeTypeName"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mIsOfficial = Convert.ToBoolean(dr["IsOfficial"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mQuotationCost = Convert.ToDecimal(dr["QuotationCost"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mPayablesWithVAT = Convert.ToDecimal(dr["PayablesWithVAT"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mPayablesWithoutVAT = Convert.ToDecimal(dr["PayablesWithoutVAT"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mReceivablesWithVAT = Convert.ToDecimal(dr["ReceivablesWithVAT"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mReceivablesWithoutVAT = Convert.ToDecimal(dr["ReceivablesWithoutVAT"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mExchangeRate = Convert.ToDecimal(dr["ExchangeRate"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mIsUsedInOperationStatement = Convert.ToInt32(dr["IsUsedInOperationStatement"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mAccNoteType = Convert.ToString(dr["AccNoteType"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mPayableDate = Convert.ToDateTime(dr["PayableDate"].ToString());
                        ObjCVarvwProfitCurrenciesWithMinimalColumns.mInvoiceDate = Convert.ToDateTime(dr["InvoiceDate"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwProfitCurrenciesWithMinimalColumns.Add(ObjCVarvwProfitCurrenciesWithMinimalColumns);
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
