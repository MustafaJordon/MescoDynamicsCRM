﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.NoAccess.Customized
{
    [Serializable]
    public partial class CVarGBL_vw_TransactionTypes
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mCUST_TRX_TYPE_ID;
        internal String mNAME;
        internal String mDESCRIPTION;
        internal String mTYPE;
        internal Int64 mGL_ID_REC;
        #endregion

        #region "Methods"
        public Int64 CUST_TRX_TYPE_ID
        {
            get { return mCUST_TRX_TYPE_ID; }
            set { mCUST_TRX_TYPE_ID = value; }
        }
        public String NAME
        {
            get { return mNAME; }
            set { mNAME = value; }
        }
        public String DESCRIPTION
        {
            get { return mDESCRIPTION; }
            set { mDESCRIPTION = value; }
        }
        public String TYPE
        {
            get { return mTYPE; }
            set { mTYPE = value; }
        }
        public Int64 GL_ID_REC
        {
            get { return mGL_ID_REC; }
            set { mGL_ID_REC = value; }
        }
        #endregion
    }

    public partial class CGBL_vw_TransactionTypes
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
        public List<CVarGBL_vw_TransactionTypes> lstCVarGBL_vw_TransactionTypes = new List<CVarGBL_vw_TransactionTypes>();
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
            lstCVarGBL_vw_TransactionTypes.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListGBL_vw_TransactionTypes";
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
                        CVarGBL_vw_TransactionTypes ObjCVarGBL_vw_TransactionTypes = new CVarGBL_vw_TransactionTypes();
                        ObjCVarGBL_vw_TransactionTypes.mCUST_TRX_TYPE_ID = Convert.ToInt64(dr["CUST_TRX_TYPE_ID"].ToString());
                        ObjCVarGBL_vw_TransactionTypes.mNAME = Convert.ToString(dr["NAME"].ToString());
                        ObjCVarGBL_vw_TransactionTypes.mDESCRIPTION = Convert.ToString(dr["DESCRIPTION"].ToString());
                        ObjCVarGBL_vw_TransactionTypes.mTYPE = Convert.ToString(dr["TYPE"].ToString());
                        ObjCVarGBL_vw_TransactionTypes.mGL_ID_REC = Convert.ToInt64(dr["GL_ID_REC"].ToString());
                        lstCVarGBL_vw_TransactionTypes.Add(ObjCVarGBL_vw_TransactionTypes);
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
            lstCVarGBL_vw_TransactionTypes.Clear();

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
                Com.CommandText = "[dbo].GetListPagingGBL_vw_TransactionTypes";
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
                        CVarGBL_vw_TransactionTypes ObjCVarGBL_vw_TransactionTypes = new CVarGBL_vw_TransactionTypes();
                        ObjCVarGBL_vw_TransactionTypes.mCUST_TRX_TYPE_ID = Convert.ToInt64(dr["CUST_TRX_TYPE_ID"].ToString());
                        ObjCVarGBL_vw_TransactionTypes.mNAME = Convert.ToString(dr["NAME"].ToString());
                        ObjCVarGBL_vw_TransactionTypes.mDESCRIPTION = Convert.ToString(dr["DESCRIPTION"].ToString());
                        ObjCVarGBL_vw_TransactionTypes.mTYPE = Convert.ToString(dr["TYPE"].ToString());
                        ObjCVarGBL_vw_TransactionTypes.mGL_ID_REC = Convert.ToInt64(dr["GL_ID_REC"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarGBL_vw_TransactionTypes.Add(ObjCVarGBL_vw_TransactionTypes);
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