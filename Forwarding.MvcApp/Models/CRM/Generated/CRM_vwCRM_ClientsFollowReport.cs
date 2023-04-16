﻿using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.CRM.CRM_Reports.Generated
{

    [Serializable]
    public class CPKvwCRM_ClientsFollowReport
    {
        #region "variables"
        #endregion

        #region "Methods"
        #endregion
    }
    [Serializable]
    public partial class CVarvwCRM_ClientsFollowReport : CPKvwCRM_ClientsFollowReport
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mClientID;
        internal String mClientName;
        internal String mCLientLocalName;
        internal Int32 mClientCode;
        internal String mClientAddress;
        internal String mPhone1;
        internal String mCellPhone;
        internal String mEmail;
        internal String mFax;
        internal Int32 mClientStatus;
        internal DateTime mSourceDate;
        internal String mSourceDescription;
        internal Int32 mLeadStatusID;
        internal String mLostReason;
        internal String mUsername;
        internal Int32 mFollowUpID;
        internal DateTime mCreationDate;
        internal DateTime mActionDate;
        internal String mNotes;
        internal String mActionName;
        internal Int32 mActionID;
        internal String mActionDetails;
        internal Int32 mSourceID;
        internal String mSourceName;
        internal Int32 mSalesRepID;
        internal String mSalesRepName;
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
        public String CLientLocalName
        {
            get { return mCLientLocalName; }
            set { mCLientLocalName = value; }
        }
        public Int32 ClientCode
        {
            get { return mClientCode; }
            set { mClientCode = value; }
        }
        public String ClientAddress
        {
            get { return mClientAddress; }
            set { mClientAddress = value; }
        }
        public String Phone1
        {
            get { return mPhone1; }
            set { mPhone1 = value; }
        }
        public String CellPhone
        {
            get { return mCellPhone; }
            set { mCellPhone = value; }
        }
        public String Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }
        public String Fax
        {
            get { return mFax; }
            set { mFax = value; }
        }
        public Int32 ClientStatus
        {
            get { return mClientStatus; }
            set { mClientStatus = value; }
        }
        public DateTime SourceDate
        {
            get { return mSourceDate; }
            set { mSourceDate = value; }
        }
        public String SourceDescription
        {
            get { return mSourceDescription; }
            set { mSourceDescription = value; }
        }
        public Int32 LeadStatusID
        {
            get { return mLeadStatusID; }
            set { mLeadStatusID = value; }
        }
        public String LostReason
        {
            get { return mLostReason; }
            set { mLostReason = value; }
        }
        public String Username
        {
            get { return mUsername; }
            set { mUsername = value; }
        }
        public Int32 FollowUpID
        {
            get { return mFollowUpID; }
            set { mFollowUpID = value; }
        }
        public DateTime CreationDate
        {
            get { return mCreationDate; }
            set { mCreationDate = value; }
        }
        public DateTime ActionDate
        {
            get { return mActionDate; }
            set { mActionDate = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mNotes = value; }
        }
        public String ActionName
        {
            get { return mActionName; }
            set { mActionName = value; }
        }
        public Int32 ActionID
        {
            get { return mActionID; }
            set { mActionID = value; }
        }
        public String ActionDetails
        {
            get { return mActionDetails; }
            set { mActionDetails = value; }
        }
        public Int32 SourceID
        {
            get { return mSourceID; }
            set { mSourceID = value; }
        }
        public String SourceName
        {
            get { return mSourceName; }
            set { mSourceName = value; }
        }
        public Int32 SalesRepID
        {
            get { return mSalesRepID; }
            set { mSalesRepID = value; }
        }
        public String SalesRepName
        {
            get { return mSalesRepName; }
            set { mSalesRepName = value; }
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

    public partial class CvwCRM_ClientsFollowReport
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
        public List<CVarvwCRM_ClientsFollowReport> lstCVarvwCRM_ClientsFollowReport = new List<CVarvwCRM_ClientsFollowReport>();
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
            lstCVarvwCRM_ClientsFollowReport.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwCRM_ClientsFollowReport";
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
                        CVarvwCRM_ClientsFollowReport ObjCVarvwCRM_ClientsFollowReport = new CVarvwCRM_ClientsFollowReport();
                        ObjCVarvwCRM_ClientsFollowReport.mClientID = Convert.ToInt32(dr["ClientID"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mClientName = Convert.ToString(dr["ClientName"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mCLientLocalName = Convert.ToString(dr["CLientLocalName"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mClientCode = Convert.ToInt32(dr["ClientCode"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mClientAddress = Convert.ToString(dr["ClientAddress"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mPhone1 = Convert.ToString(dr["Phone1"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mCellPhone = Convert.ToString(dr["CellPhone"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mEmail = Convert.ToString(dr["Email"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mFax = Convert.ToString(dr["Fax"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mClientStatus = Convert.ToInt32(dr["ClientStatus"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mSourceDate = Convert.ToDateTime(dr["SourceDate"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mSourceDescription = Convert.ToString(dr["SourceDescription"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mLeadStatusID = Convert.ToInt32(dr["LeadStatusID"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mLostReason = Convert.ToString(dr["LostReason"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mUsername = Convert.ToString(dr["Username"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mFollowUpID = Convert.ToInt32(dr["FollowUpID"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mActionDate = Convert.ToDateTime(dr["ActionDate"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mActionName = Convert.ToString(dr["ActionName"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mActionID = Convert.ToInt32(dr["ActionID"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mActionDetails = Convert.ToString(dr["ActionDetails"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mSourceID = Convert.ToInt32(dr["SourceID"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mSourceName = Convert.ToString(dr["SourceName"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mSalesRepID = Convert.ToInt32(dr["SalesRepID"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mSalesRepName = Convert.ToString(dr["SalesRepName"].ToString());
                        lstCVarvwCRM_ClientsFollowReport.Add(ObjCVarvwCRM_ClientsFollowReport);
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
            lstCVarvwCRM_ClientsFollowReport.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwCRM_ClientsFollowReport";
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
                        CVarvwCRM_ClientsFollowReport ObjCVarvwCRM_ClientsFollowReport = new CVarvwCRM_ClientsFollowReport();
                        ObjCVarvwCRM_ClientsFollowReport.mClientID = Convert.ToInt32(dr["ClientID"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mClientName = Convert.ToString(dr["ClientName"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mCLientLocalName = Convert.ToString(dr["CLientLocalName"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mClientCode = Convert.ToInt32(dr["ClientCode"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mClientAddress = Convert.ToString(dr["ClientAddress"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mPhone1 = Convert.ToString(dr["Phone1"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mCellPhone = Convert.ToString(dr["CellPhone"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mEmail = Convert.ToString(dr["Email"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mFax = Convert.ToString(dr["Fax"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mClientStatus = Convert.ToInt32(dr["ClientStatus"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mSourceDate = Convert.ToDateTime(dr["SourceDate"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mSourceDescription = Convert.ToString(dr["SourceDescription"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mLeadStatusID = Convert.ToInt32(dr["LeadStatusID"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mLostReason = Convert.ToString(dr["LostReason"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mUsername = Convert.ToString(dr["Username"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mFollowUpID = Convert.ToInt32(dr["FollowUpID"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mActionDate = Convert.ToDateTime(dr["ActionDate"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mActionName = Convert.ToString(dr["ActionName"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mActionID = Convert.ToInt32(dr["ActionID"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mActionDetails = Convert.ToString(dr["ActionDetails"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mSourceID = Convert.ToInt32(dr["SourceID"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mSourceName = Convert.ToString(dr["SourceName"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mSalesRepID = Convert.ToInt32(dr["SalesRepID"].ToString());
                        ObjCVarvwCRM_ClientsFollowReport.mSalesRepName = Convert.ToString(dr["SalesRepName"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwCRM_ClientsFollowReport.Add(ObjCVarvwCRM_ClientsFollowReport);
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

