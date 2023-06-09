﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.MasterData.Partners.Generated
{
    [Serializable]
    public partial class CVarvwLM_Customer_Rates
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mID;
        internal Int32 mCustomerID;
        internal String mCustomerName;
        internal Int32 mRateID;
        internal String mRateName;
        internal DateTime mFromDate;
        internal DateTime mToDate;
        internal Boolean mIsInActive;
        internal DateTime mcreationDate;
        internal Int32 mCreatorUserID;
        #endregion

        #region "Methods"
        public Int32 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public Int32 CustomerID
        {
            get { return mCustomerID; }
            set { mCustomerID = value; }
        }
        public String CustomerName
        {
            get { return mCustomerName; }
            set { mCustomerName = value; }
        }
        public Int32 RateID
        {
            get { return mRateID; }
            set { mRateID = value; }
        }
        public String RateName
        {
            get { return mRateName; }
            set { mRateName = value; }
        }
        public DateTime FromDate
        {
            get { return mFromDate; }
            set { mFromDate = value; }
        }
        public DateTime ToDate
        {
            get { return mToDate; }
            set { mToDate = value; }
        }
        public Boolean IsInActive
        {
            get { return mIsInActive; }
            set { mIsInActive = value; }
        }
        public DateTime creationDate
        {
            get { return mcreationDate; }
            set { mcreationDate = value; }
        }
        public Int32 CreatorUserID
        {
            get { return mCreatorUserID; }
            set { mCreatorUserID = value; }
        }
        #endregion
    }

    public partial class CvwLM_Customer_Rates
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
        public List<CVarvwLM_Customer_Rates> lstCVarvwLM_Customer_Rates = new List<CVarvwLM_Customer_Rates>();
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
            lstCVarvwLM_Customer_Rates.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.VarChar));
                    Com.CommandText = "[dbo].GetListvwLM_Customer_Rates";
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
                        CVarvwLM_Customer_Rates ObjCVarvwLM_Customer_Rates = new CVarvwLM_Customer_Rates();
                        ObjCVarvwLM_Customer_Rates.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwLM_Customer_Rates.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarvwLM_Customer_Rates.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwLM_Customer_Rates.mRateID = Convert.ToInt32(dr["RateID"].ToString());
                        ObjCVarvwLM_Customer_Rates.mRateName = Convert.ToString(dr["RateName"].ToString());
                        ObjCVarvwLM_Customer_Rates.mFromDate = Convert.ToDateTime(dr["FromDate"].ToString());
                        ObjCVarvwLM_Customer_Rates.mToDate = Convert.ToDateTime(dr["ToDate"].ToString());
                        ObjCVarvwLM_Customer_Rates.mIsInActive = Convert.ToBoolean(dr["IsInActive"].ToString());
                        ObjCVarvwLM_Customer_Rates.mcreationDate = Convert.ToDateTime(dr["creationDate"].ToString());
                        ObjCVarvwLM_Customer_Rates.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        lstCVarvwLM_Customer_Rates.Add(ObjCVarvwLM_Customer_Rates);
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
            lstCVarvwLM_Customer_Rates.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwLM_Customer_Rates";
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
                        CVarvwLM_Customer_Rates ObjCVarvwLM_Customer_Rates = new CVarvwLM_Customer_Rates();
                        ObjCVarvwLM_Customer_Rates.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwLM_Customer_Rates.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarvwLM_Customer_Rates.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwLM_Customer_Rates.mRateID = Convert.ToInt32(dr["RateID"].ToString());
                        ObjCVarvwLM_Customer_Rates.mRateName = Convert.ToString(dr["RateName"].ToString());
                        ObjCVarvwLM_Customer_Rates.mFromDate = Convert.ToDateTime(dr["FromDate"].ToString());
                        ObjCVarvwLM_Customer_Rates.mToDate = Convert.ToDateTime(dr["ToDate"].ToString());
                        ObjCVarvwLM_Customer_Rates.mIsInActive = Convert.ToBoolean(dr["IsInActive"].ToString());
                        ObjCVarvwLM_Customer_Rates.mcreationDate = Convert.ToDateTime(dr["creationDate"].ToString());
                        ObjCVarvwLM_Customer_Rates.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwLM_Customer_Rates.Add(ObjCVarvwLM_Customer_Rates);
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
