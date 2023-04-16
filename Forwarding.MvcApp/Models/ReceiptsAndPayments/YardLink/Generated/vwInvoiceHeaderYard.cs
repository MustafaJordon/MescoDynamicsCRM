﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.ReceiptsAndPayments.YardLink.Generated
{
    [Serializable]
    public class CPKvwInvoiceHeaderYard
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
    public partial class CVarvwInvoiceHeaderYard : CPKvwInvoiceHeaderYard
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Byte mInvoiceTypeID;
        internal String mInvoiceSerial;
        internal Decimal mInvoiceTotal;
        internal DateTime mIssueDate;
        internal Boolean misAudit;
        internal String mCurrencyCode;
        internal String mInvoiceType;
        internal String mCustomerName;
        internal Int32 mJVID;
        #endregion

        #region "Methods"
        public Byte InvoiceTypeID
        {
            get { return mInvoiceTypeID; }
            set { mInvoiceTypeID = value; }
        }
        public String InvoiceSerial
        {
            get { return mInvoiceSerial; }
            set { mInvoiceSerial = value; }
        }
        public Decimal InvoiceTotal
        {
            get { return mInvoiceTotal; }
            set { mInvoiceTotal = value; }
        }
        public DateTime IssueDate
        {
            get { return mIssueDate; }
            set { mIssueDate = value; }
        }
        public Boolean isAudit
        {
            get { return misAudit; }
            set { misAudit = value; }
        }
        public String CurrencyCode
        {
            get { return mCurrencyCode; }
            set { mCurrencyCode = value; }
        }
        public String InvoiceType
        {
            get { return mInvoiceType; }
            set { mInvoiceType = value; }
        }
        public String CustomerName
        {
            get { return mCustomerName; }
            set { mCustomerName = value; }
        }
        public Int32 JVID
        {
            get { return mJVID; }
            set { mJVID = value; }
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

    public partial class CvwInvoiceHeaderYard
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
        public List<CVarvwInvoiceHeaderYard> lstCVarvwInvoiceHeaderYard = new List<CVarvwInvoiceHeaderYard>();
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
            lstCVarvwInvoiceHeaderYard.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwInvoiceHeaderYard";
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
                        CVarvwInvoiceHeaderYard ObjCVarvwInvoiceHeaderYard = new CVarvwInvoiceHeaderYard();
                        ObjCVarvwInvoiceHeaderYard.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwInvoiceHeaderYard.mInvoiceTypeID = Convert.ToByte(dr["InvoiceTypeID"].ToString());
                        ObjCVarvwInvoiceHeaderYard.mInvoiceSerial = Convert.ToString(dr["InvoiceSerial"].ToString());
                        ObjCVarvwInvoiceHeaderYard.mInvoiceTotal = Convert.ToDecimal(dr["InvoiceTotal"].ToString());
                        ObjCVarvwInvoiceHeaderYard.mIssueDate = Convert.ToDateTime(dr["IssueDate"].ToString());
                        ObjCVarvwInvoiceHeaderYard.misAudit = Convert.ToBoolean(dr["isAudit"].ToString());
                        ObjCVarvwInvoiceHeaderYard.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwInvoiceHeaderYard.mInvoiceType = Convert.ToString(dr["InvoiceType"].ToString());
                        ObjCVarvwInvoiceHeaderYard.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwInvoiceHeaderYard.mJVID = Convert.ToInt32(dr["JVID"].ToString());
                        lstCVarvwInvoiceHeaderYard.Add(ObjCVarvwInvoiceHeaderYard);
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
            lstCVarvwInvoiceHeaderYard.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwInvoiceHeaderYard";
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
                        CVarvwInvoiceHeaderYard ObjCVarvwInvoiceHeaderYard = new CVarvwInvoiceHeaderYard();
                        ObjCVarvwInvoiceHeaderYard.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwInvoiceHeaderYard.mInvoiceTypeID = Convert.ToByte(dr["InvoiceTypeID"].ToString());
                        ObjCVarvwInvoiceHeaderYard.mInvoiceSerial = Convert.ToString(dr["InvoiceSerial"].ToString());
                        ObjCVarvwInvoiceHeaderYard.mInvoiceTotal = Convert.ToDecimal(dr["InvoiceTotal"].ToString());
                        ObjCVarvwInvoiceHeaderYard.mIssueDate = Convert.ToDateTime(dr["IssueDate"].ToString());
                        ObjCVarvwInvoiceHeaderYard.misAudit = Convert.ToBoolean(dr["isAudit"].ToString());
                        ObjCVarvwInvoiceHeaderYard.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwInvoiceHeaderYard.mInvoiceType = Convert.ToString(dr["InvoiceType"].ToString());
                        ObjCVarvwInvoiceHeaderYard.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwInvoiceHeaderYard.mJVID = Convert.ToInt32(dr["JVID"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwInvoiceHeaderYard.Add(ObjCVarvwInvoiceHeaderYard);
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
