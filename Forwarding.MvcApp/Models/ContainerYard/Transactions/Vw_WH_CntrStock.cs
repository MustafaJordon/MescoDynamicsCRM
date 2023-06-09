﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.ContainerYard.Transactions
{
    [Serializable]
    public class CPKVw_WH_CntrStock
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
    public partial class CVarVw_WH_CntrStock : CPKVw_WH_CntrStock
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mContainerNumber;
        internal String mCode;
        internal Boolean misOwn;
        #endregion

        #region "Methods"
        public String ContainerNumber
        {
            get { return mContainerNumber; }
            set { mContainerNumber = value; }
        }
        public String Code
        {
            get { return mCode; }
            set { mCode = value; }
        }
        public Boolean isOwn
        {
            get { return misOwn; }
            set { misOwn = value; }
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

    public partial class CVw_WH_CntrStock
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
        public List<CVarVw_WH_CntrStock> lstCVarVw_WH_CntrStock = new List<CVarVw_WH_CntrStock>();
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
            lstCVarVw_WH_CntrStock.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListVw_WH_CntrStock";
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
                        CVarVw_WH_CntrStock ObjCVarVw_WH_CntrStock = new CVarVw_WH_CntrStock();
                        ObjCVarVw_WH_CntrStock.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarVw_WH_CntrStock.mContainerNumber = Convert.ToString(dr["ContainerNumber"].ToString());
                        ObjCVarVw_WH_CntrStock.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarVw_WH_CntrStock.misOwn = Convert.ToBoolean(dr["isOwn"].ToString());
                        lstCVarVw_WH_CntrStock.Add(ObjCVarVw_WH_CntrStock);
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
            lstCVarVw_WH_CntrStock.Clear();

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
                Com.CommandText = "[dbo].GetListPagingVw_WH_CntrStock";
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
                        CVarVw_WH_CntrStock ObjCVarVw_WH_CntrStock = new CVarVw_WH_CntrStock();
                        ObjCVarVw_WH_CntrStock.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarVw_WH_CntrStock.mContainerNumber = Convert.ToString(dr["ContainerNumber"].ToString());
                        ObjCVarVw_WH_CntrStock.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarVw_WH_CntrStock.misOwn = Convert.ToBoolean(dr["isOwn"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarVw_WH_CntrStock.Add(ObjCVarVw_WH_CntrStock);
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

