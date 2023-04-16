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
    public partial class CVarvwCRM_SalesLeadHeader
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mID;
        internal String mName;
        internal String mContactPersonsNames;
        internal String mActionsNames;
        internal String mActionsColors;
        internal String mActivitiesNames;
        internal String mPipelineStage;
        internal DateTime mStartingDate;
        internal String mPaymentTermName;
        #endregion

        #region "Methods"
        public Int32 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public String Name
        {
            get { return mName; }
            set { mName = value; }
        }
        public String ContactPersonsNames
        {
            get { return mContactPersonsNames; }
            set { mContactPersonsNames = value; }
        }
        public String ActionsNames
        {
            get { return mActionsNames; }
            set { mActionsNames = value; }
        }
        public String ActionsColors
        {
            get { return mActionsColors; }
            set { mActionsColors = value; }
        }
        public String ActivitiesNames
        {
            get { return mActivitiesNames; }
            set { mActivitiesNames = value; }
        }
        public String PipelineStage
        {
            get { return mPipelineStage; }
            set { mPipelineStage = value; }
        }
        public DateTime StartingDate
        {
            get { return mStartingDate; }
            set { mStartingDate = value; }
        }
        public String PaymentTermName
        {
            get { return mPaymentTermName; }
            set { mPaymentTermName = value; }
        }
        #endregion
    }

    public partial class CvwCRM_SalesLeadHeader
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
        public List<CVarvwCRM_SalesLeadHeader> lstCVarvwCRM_SalesLeadHeader = new List<CVarvwCRM_SalesLeadHeader>();
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
            lstCVarvwCRM_SalesLeadHeader.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.VarChar));
                    Com.CommandText = "[dbo].GetListvwCRM_SalesLeadHeader";
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
                        CVarvwCRM_SalesLeadHeader ObjCVarvwCRM_SalesLeadHeader = new CVarvwCRM_SalesLeadHeader();
                        ObjCVarvwCRM_SalesLeadHeader.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwCRM_SalesLeadHeader.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwCRM_SalesLeadHeader.mContactPersonsNames = Convert.ToString(dr["ContactPersonsNames"].ToString());
                        ObjCVarvwCRM_SalesLeadHeader.mActionsNames = Convert.ToString(dr["ActionsNames"].ToString());
                        ObjCVarvwCRM_SalesLeadHeader.mActionsColors = Convert.ToString(dr["ActionsColors"].ToString());
                        ObjCVarvwCRM_SalesLeadHeader.mActivitiesNames = Convert.ToString(dr["ActivitiesNames"].ToString());
                        ObjCVarvwCRM_SalesLeadHeader.mPipelineStage = Convert.ToString(dr["PipelineStage"].ToString());
                        ObjCVarvwCRM_SalesLeadHeader.mStartingDate = Convert.ToDateTime(dr["StartingDate"].ToString());
                        ObjCVarvwCRM_SalesLeadHeader.mPaymentTermName = Convert.ToString(dr["PaymentTermName"].ToString());
                        lstCVarvwCRM_SalesLeadHeader.Add(ObjCVarvwCRM_SalesLeadHeader);
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
            lstCVarvwCRM_SalesLeadHeader.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwCRM_SalesLeadHeader";
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
                        CVarvwCRM_SalesLeadHeader ObjCVarvwCRM_SalesLeadHeader = new CVarvwCRM_SalesLeadHeader();
                        ObjCVarvwCRM_SalesLeadHeader.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwCRM_SalesLeadHeader.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwCRM_SalesLeadHeader.mContactPersonsNames = Convert.ToString(dr["ContactPersonsNames"].ToString());
                        ObjCVarvwCRM_SalesLeadHeader.mActionsNames = Convert.ToString(dr["ActionsNames"].ToString());
                        ObjCVarvwCRM_SalesLeadHeader.mActionsColors = Convert.ToString(dr["ActionsColors"].ToString());
                        ObjCVarvwCRM_SalesLeadHeader.mActivitiesNames = Convert.ToString(dr["ActivitiesNames"].ToString());
                        ObjCVarvwCRM_SalesLeadHeader.mPipelineStage = Convert.ToString(dr["PipelineStage"].ToString());
                        ObjCVarvwCRM_SalesLeadHeader.mStartingDate = Convert.ToDateTime(dr["StartingDate"].ToString());
                        ObjCVarvwCRM_SalesLeadHeader.mPaymentTermName = Convert.ToString(dr["PaymentTermName"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwCRM_SalesLeadHeader.Add(ObjCVarvwCRM_SalesLeadHeader);
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
