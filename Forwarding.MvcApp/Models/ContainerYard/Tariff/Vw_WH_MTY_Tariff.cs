﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace Forwarding.MvcApp.Models.ContainerYard.Tariff
{
    [Serializable]
    public partial class CVarVw_WH_MTY_Tariff
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mID;
        internal String mCode;
        internal String mName;
        internal String mCustomer;
        internal String mWareHouse;
        internal Boolean mIsDefault;
        internal Boolean mIsHold;
        #endregion

        #region "Methods"
        public Int32 ID
        {
            get { return mID; }
            set { mID = value; }
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
        public String Customer
        {
            get { return mCustomer; }
            set { mCustomer = value; }
        }
        public String WareHouse
        {
            get { return mWareHouse; }
            set { mWareHouse = value; }
        }
        public Boolean IsDefault
        {
            get { return mIsDefault; }
            set { mIsDefault = value; }
        }
        public Boolean IsHold
        {
            get { return mIsHold; }
            set { mIsHold = value; }
        }
        #endregion
    }

    public partial class CVw_WH_MTY_Tariff
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
        public List<CVarVw_WH_MTY_Tariff> lstCVarVw_WH_MTY_Tariff = new List<CVarVw_WH_MTY_Tariff>();
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
            lstCVarVw_WH_MTY_Tariff.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListVw_WH_MTY_Tariff";
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
                        CVarVw_WH_MTY_Tariff ObjCVarVw_WH_MTY_Tariff = new CVarVw_WH_MTY_Tariff();
                        ObjCVarVw_WH_MTY_Tariff.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarVw_WH_MTY_Tariff.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarVw_WH_MTY_Tariff.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarVw_WH_MTY_Tariff.mCustomer = Convert.ToString(dr["Customer"].ToString());
                        ObjCVarVw_WH_MTY_Tariff.mWareHouse = Convert.ToString(dr["WareHouse"].ToString());
                        ObjCVarVw_WH_MTY_Tariff.mIsDefault = Convert.ToBoolean(dr["IsDefault"].ToString());
                        ObjCVarVw_WH_MTY_Tariff.mIsHold = Convert.ToBoolean(dr["IsHold"].ToString());
                        lstCVarVw_WH_MTY_Tariff.Add(ObjCVarVw_WH_MTY_Tariff);
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
            lstCVarVw_WH_MTY_Tariff.Clear();

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
                Com.CommandText = "[dbo].GetListPagingVw_WH_MTY_Tariff";
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
                        CVarVw_WH_MTY_Tariff ObjCVarVw_WH_MTY_Tariff = new CVarVw_WH_MTY_Tariff();
                        ObjCVarVw_WH_MTY_Tariff.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarVw_WH_MTY_Tariff.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarVw_WH_MTY_Tariff.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarVw_WH_MTY_Tariff.mCustomer = Convert.ToString(dr["Customer"].ToString());
                        ObjCVarVw_WH_MTY_Tariff.mWareHouse = Convert.ToString(dr["WareHouse"].ToString());
                        ObjCVarVw_WH_MTY_Tariff.mIsDefault = Convert.ToBoolean(dr["IsDefault"].ToString());
                        ObjCVarVw_WH_MTY_Tariff.mIsHold = Convert.ToBoolean(dr["IsHold"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarVw_WH_MTY_Tariff.Add(ObjCVarVw_WH_MTY_Tariff);
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