﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.CRM.CRM_Clients.Generated
{
    [Serializable]
    public partial class CVarvwLastAction_SalesRep
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mCRM_CLientID;
        internal String mName;
        internal String mUserName;
        internal Int32 mSalesmanID;
        internal String maction;
        internal Int32 mactionID;
        internal DateTime mActionDate;
        #endregion

        #region "Methods"
        public Int32 CRM_CLientID
        {
            get { return mCRM_CLientID; }
            set { mCRM_CLientID = value; }
        }
        public String Name
        {
            get { return mName; }
            set { mName = value; }
        }
        public String UserName
        {
            get { return mUserName; }
            set { mUserName = value; }
        }
        public Int32 SalesmanID
        {
            get { return mSalesmanID; }
            set { mSalesmanID = value; }
        }
        public String action
        {
            get { return maction; }
            set { maction = value; }
        }
        public Int32 actionID
        {
            get { return mactionID; }
            set { mactionID = value; }
        }
        public DateTime ActionDate
        {
            get { return mActionDate; }
            set { mActionDate = value; }
        }
        #endregion
    }

    public partial class CvwLastAction_SalesRep
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
        public List<CVarvwLastAction_SalesRep> lstCVarvwLastAction_SalesRep = new List<CVarvwLastAction_SalesRep>();
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
            lstCVarvwLastAction_SalesRep.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.VarChar));
                    Com.CommandText = "[dbo].GetListvwLastAction_SalesRep";
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
                        CVarvwLastAction_SalesRep ObjCVarvwLastAction_SalesRep = new CVarvwLastAction_SalesRep();
                        ObjCVarvwLastAction_SalesRep.mCRM_CLientID = Convert.ToInt32(dr["CRM_CLientID"].ToString());
                        ObjCVarvwLastAction_SalesRep.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwLastAction_SalesRep.mUserName = Convert.ToString(dr["UserName"].ToString());
                        ObjCVarvwLastAction_SalesRep.mSalesmanID = Convert.ToInt32(dr["SalesmanID"].ToString());
                        ObjCVarvwLastAction_SalesRep.maction = Convert.ToString(dr["action"].ToString());
                        ObjCVarvwLastAction_SalesRep.mactionID = Convert.ToInt32(dr["actionID"].ToString());
                        ObjCVarvwLastAction_SalesRep.mActionDate = Convert.ToDateTime(dr["ActionDate"].ToString());
                        lstCVarvwLastAction_SalesRep.Add(ObjCVarvwLastAction_SalesRep);
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
            lstCVarvwLastAction_SalesRep.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwLastAction_SalesRep";
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
                        CVarvwLastAction_SalesRep ObjCVarvwLastAction_SalesRep = new CVarvwLastAction_SalesRep();
                        ObjCVarvwLastAction_SalesRep.mCRM_CLientID = Convert.ToInt32(dr["CRM_CLientID"].ToString());
                        ObjCVarvwLastAction_SalesRep.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwLastAction_SalesRep.mUserName = Convert.ToString(dr["UserName"].ToString());
                        ObjCVarvwLastAction_SalesRep.mSalesmanID = Convert.ToInt32(dr["SalesmanID"].ToString());
                        ObjCVarvwLastAction_SalesRep.maction = Convert.ToString(dr["action"].ToString());
                        ObjCVarvwLastAction_SalesRep.mactionID = Convert.ToInt32(dr["actionID"].ToString());
                        ObjCVarvwLastAction_SalesRep.mActionDate = Convert.ToDateTime(dr["ActionDate"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwLastAction_SalesRep.Add(ObjCVarvwLastAction_SalesRep);
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
