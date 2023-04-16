﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.MasterData.Locations.Generated
{
    [Serializable]
    public partial class CVarvwLM_RateRegions
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mID;
        internal Int32 mRateID;
        internal Int32 mCityIDFrom;
        internal String mCityNameFrom;
        internal Int32 mRegionIDFrom;
        internal String mRegionNameFrom;
        internal Int32 mCityIDTo;
        internal String mCityNameTo;
        internal Int32 mRegionIDTo;
        internal String mRegionNameTo;
        internal Decimal mCost;
        internal Decimal mSelling;
        internal String mRemarks;
        internal Decimal mQuantity;
        internal Int32 mPackageTypeID;
        internal String mPackageTypeName;
        #endregion

        #region "Methods"
        public Int32 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public Int32 RateID
        {
            get { return mRateID; }
            set { mRateID = value; }
        }
        public Int32 CityIDFrom
        {
            get { return mCityIDFrom; }
            set { mCityIDFrom = value; }
        }
        public String CityNameFrom
        {
            get { return mCityNameFrom; }
            set { mCityNameFrom = value; }
        }
        public Int32 RegionIDFrom
        {
            get { return mRegionIDFrom; }
            set { mRegionIDFrom = value; }
        }
        public String RegionNameFrom
        {
            get { return mRegionNameFrom; }
            set { mRegionNameFrom = value; }
        }
        public Int32 CityIDTo
        {
            get { return mCityIDTo; }
            set { mCityIDTo = value; }
        }
        public String CityNameTo
        {
            get { return mCityNameTo; }
            set { mCityNameTo = value; }
        }
        public Int32 RegionIDTo
        {
            get { return mRegionIDTo; }
            set { mRegionIDTo = value; }
        }
        public String RegionNameTo
        {
            get { return mRegionNameTo; }
            set { mRegionNameTo = value; }
        }
        public Decimal Cost
        {
            get { return mCost; }
            set { mCost = value; }
        }
        public Decimal Selling
        {
            get { return mSelling; }
            set { mSelling = value; }
        }
        public String Remarks
        {
            get { return mRemarks; }
            set { mRemarks = value; }
        }
        public Decimal Quantity
        {
            get { return mQuantity; }
            set { mQuantity = value; }
        }
        public Int32 PackageTypeID
        {
            get { return mPackageTypeID; }
            set { mPackageTypeID = value; }
        }
        public String PackageTypeName
        {
            get { return mPackageTypeName; }
            set { mPackageTypeName = value; }
        }
        #endregion
    }

    public partial class CvwLM_RateRegions
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
        public List<CVarvwLM_RateRegions> lstCVarvwLM_RateRegions = new List<CVarvwLM_RateRegions>();
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
            lstCVarvwLM_RateRegions.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.VarChar));
                    Com.CommandText = "[dbo].GetListvwLM_RateRegions";
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
                        CVarvwLM_RateRegions ObjCVarvwLM_RateRegions = new CVarvwLM_RateRegions();
                        ObjCVarvwLM_RateRegions.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwLM_RateRegions.mRateID = Convert.ToInt32(dr["RateID"].ToString());
                        ObjCVarvwLM_RateRegions.mCityIDFrom = Convert.ToInt32(dr["CityIDFrom"].ToString());
                        ObjCVarvwLM_RateRegions.mCityNameFrom = Convert.ToString(dr["CityNameFrom"].ToString());
                        ObjCVarvwLM_RateRegions.mRegionIDFrom = Convert.ToInt32(dr["RegionIDFrom"].ToString());
                        ObjCVarvwLM_RateRegions.mRegionNameFrom = Convert.ToString(dr["RegionNameFrom"].ToString());
                        ObjCVarvwLM_RateRegions.mCityIDTo = Convert.ToInt32(dr["CityIDTo"].ToString());
                        ObjCVarvwLM_RateRegions.mCityNameTo = Convert.ToString(dr["CityNameTo"].ToString());
                        ObjCVarvwLM_RateRegions.mRegionIDTo = Convert.ToInt32(dr["RegionIDTo"].ToString());
                        ObjCVarvwLM_RateRegions.mRegionNameTo = Convert.ToString(dr["RegionNameTo"].ToString());
                        ObjCVarvwLM_RateRegions.mCost = Convert.ToDecimal(dr["Cost"].ToString());
                        ObjCVarvwLM_RateRegions.mSelling = Convert.ToDecimal(dr["Selling"].ToString());
                        ObjCVarvwLM_RateRegions.mRemarks = Convert.ToString(dr["Remarks"].ToString());
                        ObjCVarvwLM_RateRegions.mQuantity = Convert.ToDecimal(dr["Quantity"].ToString());
                        ObjCVarvwLM_RateRegions.mPackageTypeID = Convert.ToInt32(dr["PackageTypeID"].ToString());
                        ObjCVarvwLM_RateRegions.mPackageTypeName = Convert.ToString(dr["PackageTypeName"].ToString());
                        lstCVarvwLM_RateRegions.Add(ObjCVarvwLM_RateRegions);
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
            lstCVarvwLM_RateRegions.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                Com.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@PageNumber", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.VarChar));
                Com.Parameters.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar));
                Com.CommandText = "[dbo].GetListPagingvwLM_RateRegions";
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
                        CVarvwLM_RateRegions ObjCVarvwLM_RateRegions = new CVarvwLM_RateRegions();
                        ObjCVarvwLM_RateRegions.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwLM_RateRegions.mRateID = Convert.ToInt32(dr["RateID"].ToString());
                        ObjCVarvwLM_RateRegions.mCityIDFrom = Convert.ToInt32(dr["CityIDFrom"].ToString());
                        ObjCVarvwLM_RateRegions.mCityNameFrom = Convert.ToString(dr["CityNameFrom"].ToString());
                        ObjCVarvwLM_RateRegions.mRegionIDFrom = Convert.ToInt32(dr["RegionIDFrom"].ToString());
                        ObjCVarvwLM_RateRegions.mRegionNameFrom = Convert.ToString(dr["RegionNameFrom"].ToString());
                        ObjCVarvwLM_RateRegions.mCityIDTo = Convert.ToInt32(dr["CityIDTo"].ToString());
                        ObjCVarvwLM_RateRegions.mCityNameTo = Convert.ToString(dr["CityNameTo"].ToString());
                        ObjCVarvwLM_RateRegions.mRegionIDTo = Convert.ToInt32(dr["RegionIDTo"].ToString());
                        ObjCVarvwLM_RateRegions.mRegionNameTo = Convert.ToString(dr["RegionNameTo"].ToString());
                        ObjCVarvwLM_RateRegions.mCost = Convert.ToDecimal(dr["Cost"].ToString());
                        ObjCVarvwLM_RateRegions.mSelling = Convert.ToDecimal(dr["Selling"].ToString());
                        ObjCVarvwLM_RateRegions.mRemarks = Convert.ToString(dr["Remarks"].ToString());
                        ObjCVarvwLM_RateRegions.mQuantity = Convert.ToDecimal(dr["Quantity"].ToString());
                        ObjCVarvwLM_RateRegions.mPackageTypeID = Convert.ToInt32(dr["PackageTypeID"].ToString());
                        ObjCVarvwLM_RateRegions.mPackageTypeName = Convert.ToString(dr["PackageTypeName"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwLM_RateRegions.Add(ObjCVarvwLM_RateRegions);
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