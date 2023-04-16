﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.MasterData.BanksAccountsAndTreasuries.Generated
{
    [Serializable]
    public partial class CVarvwCustodyRegions
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mID;
        internal Int32 mCustodyID;
        internal String mName;
        internal String mRegionName;
        internal String mCityName;
        internal Int32 mPortID;
        #endregion

        #region "Methods"
        public Int32 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public Int32 CustodyID
        {
            get { return mCustodyID; }
            set { mCustodyID = value; }
        }
        public String Name
        {
            get { return mName; }
            set { mName = value; }
        }
        public String RegionName
        {
            get { return mRegionName; }
            set { mRegionName = value; }
        }
        public String CityName
        {
            get { return mCityName; }
            set { mCityName = value; }
        }
        public Int32 PortID
        {
            get { return mPortID; }
            set { mPortID = value; }
        }
        #endregion
    }

    public partial class CvwCustodyRegions
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
        public List<CVarvwCustodyRegions> lstCVarvwCustodyRegions = new List<CVarvwCustodyRegions>();
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
            lstCVarvwCustodyRegions.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwCustodyRegions";
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
                        CVarvwCustodyRegions ObjCVarvwCustodyRegions = new CVarvwCustodyRegions();
                        ObjCVarvwCustodyRegions.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwCustodyRegions.mCustodyID = Convert.ToInt32(dr["CustodyID"].ToString());
                        ObjCVarvwCustodyRegions.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwCustodyRegions.mRegionName = Convert.ToString(dr["RegionName"].ToString());
                        ObjCVarvwCustodyRegions.mCityName = Convert.ToString(dr["CityName"].ToString());
                        ObjCVarvwCustodyRegions.mPortID = Convert.ToInt32(dr["PortID"].ToString());
                        lstCVarvwCustodyRegions.Add(ObjCVarvwCustodyRegions);
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
            lstCVarvwCustodyRegions.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwCustodyRegions";
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
                        CVarvwCustodyRegions ObjCVarvwCustodyRegions = new CVarvwCustodyRegions();
                        ObjCVarvwCustodyRegions.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwCustodyRegions.mCustodyID = Convert.ToInt32(dr["CustodyID"].ToString());
                        ObjCVarvwCustodyRegions.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwCustodyRegions.mRegionName = Convert.ToString(dr["RegionName"].ToString());
                        ObjCVarvwCustodyRegions.mCityName = Convert.ToString(dr["CityName"].ToString());
                        ObjCVarvwCustodyRegions.mPortID = Convert.ToInt32(dr["PortID"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwCustodyRegions.Add(ObjCVarvwCustodyRegions);
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