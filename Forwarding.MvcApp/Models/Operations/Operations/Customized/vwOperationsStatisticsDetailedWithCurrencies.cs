﻿//Com.CommandTimeout = 20000;

using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.Operations.Operations.Customized
{
    [Serializable]
    public partial class CVarvwOperationsStatisticsDetailedWithCurrencies
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mID;
        internal String mCode;
        internal String mAllCurrencies;
        internal String mPayablesWithVATSumCurrencies;
        internal String mPayablesWithoutVATSumCurrencies;
        internal String mReceivablesWithVATSumCurrencies;
        internal String mReceivablesWithoutVATCurrencies;
        #endregion

        #region "Methods"
        public Int64 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public String Code
        {
            get { return mCode; }
            set { mCode = value; }
        }
        public String AllCurrencies
        {
            get { return mAllCurrencies; }
            set { mAllCurrencies = value; }
        }
        public String PayablesWithVATSumCurrencies
        {
            get { return mPayablesWithVATSumCurrencies; }
            set { mPayablesWithVATSumCurrencies = value; }
        }
        public String PayablesWithoutVATSumCurrencies
        {
            get { return mPayablesWithoutVATSumCurrencies; }
            set { mPayablesWithoutVATSumCurrencies = value; }
        }
        public String ReceivablesWithVATSumCurrencies
        {
            get { return mReceivablesWithVATSumCurrencies; }
            set { mReceivablesWithVATSumCurrencies = value; }
        }
        public String ReceivablesWithoutVATCurrencies
        {
            get { return mReceivablesWithoutVATCurrencies; }
            set { mReceivablesWithoutVATCurrencies = value; }
        }
        #endregion
    }

    public partial class CvwOperationsStatisticsDetailedWithCurrencies
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
        public List<CVarvwOperationsStatisticsDetailedWithCurrencies> lstCVarvwOperationsStatisticsDetailedWithCurrencies = new List<CVarvwOperationsStatisticsDetailedWithCurrencies>();
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
            lstCVarvwOperationsStatisticsDetailedWithCurrencies.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwOperationsStatisticsDetailedWithCurrencies";
                    Com.Parameters[0].Value = Param;
                }
                Com.Transaction = tr;
                Com.Connection = Con;
                Com.CommandTimeout = 20000;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        /*Start DataReader*/
                        CVarvwOperationsStatisticsDetailedWithCurrencies ObjCVarvwOperationsStatisticsDetailedWithCurrencies = new CVarvwOperationsStatisticsDetailedWithCurrencies();
                        ObjCVarvwOperationsStatisticsDetailedWithCurrencies.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwOperationsStatisticsDetailedWithCurrencies.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwOperationsStatisticsDetailedWithCurrencies.mAllCurrencies = Convert.ToString(dr["AllCurrencies"].ToString());
                        ObjCVarvwOperationsStatisticsDetailedWithCurrencies.mPayablesWithVATSumCurrencies = Convert.ToString(dr["PayablesWithVATSumCurrencies"].ToString());
                        ObjCVarvwOperationsStatisticsDetailedWithCurrencies.mPayablesWithoutVATSumCurrencies = Convert.ToString(dr["PayablesWithoutVATSumCurrencies"].ToString());
                        ObjCVarvwOperationsStatisticsDetailedWithCurrencies.mReceivablesWithVATSumCurrencies = Convert.ToString(dr["ReceivablesWithVATSumCurrencies"].ToString());
                        ObjCVarvwOperationsStatisticsDetailedWithCurrencies.mReceivablesWithoutVATCurrencies = Convert.ToString(dr["ReceivablesWithoutVATCurrencies"].ToString());
                        lstCVarvwOperationsStatisticsDetailedWithCurrencies.Add(ObjCVarvwOperationsStatisticsDetailedWithCurrencies);
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
            lstCVarvwOperationsStatisticsDetailedWithCurrencies.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwOperationsStatisticsDetailedWithCurrencies";
                Com.Parameters[0].Value = PageSize;
                Com.Parameters[1].Value = PageNumber;
                Com.Parameters[2].Value = WhereClause;
                Com.Parameters[3].Value = OrderBy;
                Com.Transaction = tr;
                Com.Connection = Con;
                Com.CommandTimeout = 20000;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        /*Start DataReader*/
                        CVarvwOperationsStatisticsDetailedWithCurrencies ObjCVarvwOperationsStatisticsDetailedWithCurrencies = new CVarvwOperationsStatisticsDetailedWithCurrencies();
                        ObjCVarvwOperationsStatisticsDetailedWithCurrencies.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwOperationsStatisticsDetailedWithCurrencies.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwOperationsStatisticsDetailedWithCurrencies.mAllCurrencies = Convert.ToString(dr["AllCurrencies"].ToString());
                        ObjCVarvwOperationsStatisticsDetailedWithCurrencies.mPayablesWithVATSumCurrencies = Convert.ToString(dr["PayablesWithVATSumCurrencies"].ToString());
                        ObjCVarvwOperationsStatisticsDetailedWithCurrencies.mPayablesWithoutVATSumCurrencies = Convert.ToString(dr["PayablesWithoutVATSumCurrencies"].ToString());
                        ObjCVarvwOperationsStatisticsDetailedWithCurrencies.mReceivablesWithVATSumCurrencies = Convert.ToString(dr["ReceivablesWithVATSumCurrencies"].ToString());
                        ObjCVarvwOperationsStatisticsDetailedWithCurrencies.mReceivablesWithoutVATCurrencies = Convert.ToString(dr["ReceivablesWithoutVATCurrencies"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwOperationsStatisticsDetailedWithCurrencies.Add(ObjCVarvwOperationsStatisticsDetailedWithCurrencies);
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