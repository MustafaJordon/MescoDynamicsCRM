﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.CRM.Generated
{
    [Serializable]
    public partial class CVarvwCRM_ComplaintDetailsResponses
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mID;
        internal Int32 mComplaintDetailsID;
        internal String mResponseDescription;
        internal DateTime mResponseDate;
        internal Int32 mSalesRep2;
        internal String mSalesRep2Name;
        internal Int32 mStatusID;
        internal String mStatusName;
        #endregion

        #region "Methods"
        public Int32 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public Int32 ComplaintDetailsID
        {
            get { return mComplaintDetailsID; }
            set { mComplaintDetailsID = value; }
        }
        public String ResponseDescription
        {
            get { return mResponseDescription; }
            set { mResponseDescription = value; }
        }
        public DateTime ResponseDate
        {
            get { return mResponseDate; }
            set { mResponseDate = value; }
        }
        public Int32 SalesRep2
        {
            get { return mSalesRep2; }
            set { mSalesRep2 = value; }
        }
        public String SalesRep2Name
        {
            get { return mSalesRep2Name; }
            set { mSalesRep2Name = value; }
        }
        public Int32 StatusID
        {
            get { return mStatusID; }
            set { mStatusID = value; }
        }
        public String StatusName
        {
            get { return mStatusName; }
            set { mStatusName = value; }
        }
        #endregion
    }

    public partial class CvwCRM_ComplaintDetailsResponses
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
        public List<CVarvwCRM_ComplaintDetailsResponses> lstCVarvwCRM_ComplaintDetailsResponses = new List<CVarvwCRM_ComplaintDetailsResponses>();
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
            lstCVarvwCRM_ComplaintDetailsResponses.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.VarChar));
                    Com.CommandText = "[dbo].GetListvwCRM_ComplaintDetailsResponses";
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
                        CVarvwCRM_ComplaintDetailsResponses ObjCVarvwCRM_ComplaintDetailsResponses = new CVarvwCRM_ComplaintDetailsResponses();
                        ObjCVarvwCRM_ComplaintDetailsResponses.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwCRM_ComplaintDetailsResponses.mComplaintDetailsID = Convert.ToInt32(dr["ComplaintDetailsID"].ToString());
                        ObjCVarvwCRM_ComplaintDetailsResponses.mResponseDescription = Convert.ToString(dr["ResponseDescription"].ToString());
                        ObjCVarvwCRM_ComplaintDetailsResponses.mResponseDate = Convert.ToDateTime(dr["ResponseDate"].ToString());
                        ObjCVarvwCRM_ComplaintDetailsResponses.mSalesRep2 = Convert.ToInt32(dr["SalesRep2"].ToString());
                        ObjCVarvwCRM_ComplaintDetailsResponses.mSalesRep2Name = Convert.ToString(dr["SalesRep2Name"].ToString());
                        ObjCVarvwCRM_ComplaintDetailsResponses.mStatusID = Convert.ToInt32(dr["StatusID"].ToString());
                        ObjCVarvwCRM_ComplaintDetailsResponses.mStatusName = Convert.ToString(dr["StatusName"].ToString());
                        lstCVarvwCRM_ComplaintDetailsResponses.Add(ObjCVarvwCRM_ComplaintDetailsResponses);
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
            lstCVarvwCRM_ComplaintDetailsResponses.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwCRM_ComplaintDetailsResponses";
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
                        CVarvwCRM_ComplaintDetailsResponses ObjCVarvwCRM_ComplaintDetailsResponses = new CVarvwCRM_ComplaintDetailsResponses();
                        ObjCVarvwCRM_ComplaintDetailsResponses.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwCRM_ComplaintDetailsResponses.mComplaintDetailsID = Convert.ToInt32(dr["ComplaintDetailsID"].ToString());
                        ObjCVarvwCRM_ComplaintDetailsResponses.mResponseDescription = Convert.ToString(dr["ResponseDescription"].ToString());
                        ObjCVarvwCRM_ComplaintDetailsResponses.mResponseDate = Convert.ToDateTime(dr["ResponseDate"].ToString());
                        ObjCVarvwCRM_ComplaintDetailsResponses.mSalesRep2 = Convert.ToInt32(dr["SalesRep2"].ToString());
                        ObjCVarvwCRM_ComplaintDetailsResponses.mSalesRep2Name = Convert.ToString(dr["SalesRep2Name"].ToString());
                        ObjCVarvwCRM_ComplaintDetailsResponses.mStatusID = Convert.ToInt32(dr["StatusID"].ToString());
                        ObjCVarvwCRM_ComplaintDetailsResponses.mStatusName = Convert.ToString(dr["StatusName"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwCRM_ComplaintDetailsResponses.Add(ObjCVarvwCRM_ComplaintDetailsResponses);
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
