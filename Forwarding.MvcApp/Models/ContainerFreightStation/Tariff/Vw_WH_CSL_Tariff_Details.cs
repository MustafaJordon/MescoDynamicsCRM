﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.ContainerFreightStation.Tariff
{
    [Serializable]
    public class CPKVw_WH_CSL_Tariff_Details
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
    public partial class CVarVw_WH_CSL_Tariff_Details : CPKVw_WH_CSL_Tariff_Details
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mChargeType;
        internal Decimal mRate;
        internal String mCalcType;
        internal Int32 mWH_CSL_TariffID;
        internal Decimal mCalculatedAmount;
        internal Decimal mCommission;
        #endregion

        #region "Methods"
        public String ChargeType
        {
            get { return mChargeType; }
            set { mChargeType = value; }
        }
        public Decimal Rate
        {
            get { return mRate; }
            set { mRate = value; }
        }
        public String CalcType
        {
            get { return mCalcType; }
            set { mCalcType = value; }
        }
        public Int32 WH_CSL_TariffID
        {
            get { return mWH_CSL_TariffID; }
            set { mWH_CSL_TariffID = value; }
        }
        public Decimal CalculatedAmount
        {
            get { return mCalculatedAmount; }
            set { mCalculatedAmount = value; }
        }
        public Decimal Commission
        {
            get { return mCommission; }
            set { mCommission = value; }
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

    public partial class CVw_WH_CSL_Tariff_Details
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
        public List<CVarVw_WH_CSL_Tariff_Details> lstCVarVw_WH_CSL_Tariff_Details = new List<CVarVw_WH_CSL_Tariff_Details>();
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
            lstCVarVw_WH_CSL_Tariff_Details.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListVw_WH_CSL_Tariff_Details";
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
                        CVarVw_WH_CSL_Tariff_Details ObjCVarVw_WH_CSL_Tariff_Details = new CVarVw_WH_CSL_Tariff_Details();
                        ObjCVarVw_WH_CSL_Tariff_Details.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarVw_WH_CSL_Tariff_Details.mChargeType = Convert.ToString(dr["ChargeType"].ToString());
                        ObjCVarVw_WH_CSL_Tariff_Details.mRate = Convert.ToDecimal(dr["Rate"].ToString());
                        ObjCVarVw_WH_CSL_Tariff_Details.mCalcType = Convert.ToString(dr["CalcType"].ToString());
                        ObjCVarVw_WH_CSL_Tariff_Details.mWH_CSL_TariffID = Convert.ToInt32(dr["WH_CSL_TariffID"].ToString());
                        ObjCVarVw_WH_CSL_Tariff_Details.mCalculatedAmount = Convert.ToDecimal(dr["CalculatedAmount"].ToString());
                        ObjCVarVw_WH_CSL_Tariff_Details.mCommission = Convert.ToDecimal(dr["Commission"].ToString());
                        lstCVarVw_WH_CSL_Tariff_Details.Add(ObjCVarVw_WH_CSL_Tariff_Details);
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
            lstCVarVw_WH_CSL_Tariff_Details.Clear();

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
                Com.CommandText = "[dbo].GetListPagingVw_WH_CSL_Tariff_Details";
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
                        CVarVw_WH_CSL_Tariff_Details ObjCVarVw_WH_CSL_Tariff_Details = new CVarVw_WH_CSL_Tariff_Details();
                        ObjCVarVw_WH_CSL_Tariff_Details.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarVw_WH_CSL_Tariff_Details.mChargeType = Convert.ToString(dr["ChargeType"].ToString());
                        ObjCVarVw_WH_CSL_Tariff_Details.mRate = Convert.ToDecimal(dr["Rate"].ToString());
                        ObjCVarVw_WH_CSL_Tariff_Details.mCalcType = Convert.ToString(dr["CalcType"].ToString());
                        ObjCVarVw_WH_CSL_Tariff_Details.mWH_CSL_TariffID = Convert.ToInt32(dr["WH_CSL_TariffID"].ToString());
                        ObjCVarVw_WH_CSL_Tariff_Details.mCalculatedAmount = Convert.ToDecimal(dr["CalculatedAmount"].ToString());
                        ObjCVarVw_WH_CSL_Tariff_Details.mCommission = Convert.ToDecimal(dr["Commission"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarVw_WH_CSL_Tariff_Details.Add(ObjCVarVw_WH_CSL_Tariff_Details);
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

