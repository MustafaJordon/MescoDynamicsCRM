﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.CRM.CRM_FollowUp.Generated
{
    [Serializable]
    public partial class CVarvwSetubSalesLead
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mClientID;
        internal String mClientName;
        internal Int32 mCode;
        internal DateTime mLastActionDate;
        internal String mACTION;
        internal Int32 mDaysToLastAction;
        internal Int32 mDaysDiff;
        internal Int32 mValid;
        #endregion

        #region "Methods"
        public Int32 ClientID
        {
            get { return mClientID; }
            set { mClientID = value; }
        }
        public String ClientName
        {
            get { return mClientName; }
            set { mClientName = value; }
        }
        public Int32 Code
        {
            get { return mCode; }
            set { mCode = value; }
        }
        public DateTime LastActionDate
        {
            get { return mLastActionDate; }
            set { mLastActionDate = value; }
        }
        public String ACTION
        {
            get { return mACTION; }
            set { mACTION = value; }
        }
        public Int32 DaysToLastAction
        {
            get { return mDaysToLastAction; }
            set { mDaysToLastAction = value; }
        }
        public Int32 DaysDiff
        {
            get { return mDaysDiff; }
            set { mDaysDiff = value; }
        }
        public Int32 Valid
        {
            get { return mValid; }
            set { mValid = value; }
        }
        #endregion
    }

    public partial class CvwSetubSalesLead
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
        public List<CVarvwSetubSalesLead> lstCVarvwSetubSalesLead = new List<CVarvwSetubSalesLead>();
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
            lstCVarvwSetubSalesLead.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.VarChar));
                    Com.CommandText = "[dbo].GetListvwSetubSalesLead";
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
                        CVarvwSetubSalesLead ObjCVarvwSetubSalesLead = new CVarvwSetubSalesLead();
                        ObjCVarvwSetubSalesLead.mClientID = Convert.ToInt32(dr["ClientID"].ToString());
                        ObjCVarvwSetubSalesLead.mClientName = Convert.ToString(dr["ClientName"].ToString());
                        ObjCVarvwSetubSalesLead.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarvwSetubSalesLead.mLastActionDate = Convert.ToDateTime(dr["LastActionDate"].ToString());
                        ObjCVarvwSetubSalesLead.mACTION = Convert.ToString(dr["ACTION"].ToString());
                        ObjCVarvwSetubSalesLead.mDaysToLastAction = Convert.ToInt32(dr["DaysToLastAction"].ToString());
                        ObjCVarvwSetubSalesLead.mDaysDiff = Convert.ToInt32(dr["DaysDiff"].ToString());
                        ObjCVarvwSetubSalesLead.mValid = Convert.ToInt32(dr["Valid"].ToString());
                        lstCVarvwSetubSalesLead.Add(ObjCVarvwSetubSalesLead);
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
            lstCVarvwSetubSalesLead.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwSetubSalesLead";
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
                        CVarvwSetubSalesLead ObjCVarvwSetubSalesLead = new CVarvwSetubSalesLead();
                        ObjCVarvwSetubSalesLead.mClientID = Convert.ToInt32(dr["ClientID"].ToString());
                        ObjCVarvwSetubSalesLead.mClientName = Convert.ToString(dr["ClientName"].ToString());
                        ObjCVarvwSetubSalesLead.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarvwSetubSalesLead.mLastActionDate = Convert.ToDateTime(dr["LastActionDate"].ToString());
                        ObjCVarvwSetubSalesLead.mACTION = Convert.ToString(dr["ACTION"].ToString());
                        ObjCVarvwSetubSalesLead.mDaysToLastAction = Convert.ToInt32(dr["DaysToLastAction"].ToString());
                        ObjCVarvwSetubSalesLead.mDaysDiff = Convert.ToInt32(dr["DaysDiff"].ToString());
                        ObjCVarvwSetubSalesLead.mValid = Convert.ToInt32(dr["Valid"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwSetubSalesLead.Add(ObjCVarvwSetubSalesLead);
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
