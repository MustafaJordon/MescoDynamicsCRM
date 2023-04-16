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
    public partial class CVarvwCRM_Complaint
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mID;
        internal Int32 mCode;
        internal Int32 mCustomerID;
        internal String mCustomerName;
        internal Int64 mOperationID;
        internal String mOperationCode;
        internal Int32 mOperationCodeSerial;
        internal String mComplaintName;
        internal String mComplaintDetails;
        internal String mNotes;
        internal Int32 mCreatorUserID;
        internal String mCreatorUserName;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal String mComplaintDetailsOperationCodeSerials;
        internal String mComplaintDetailsNames;
        internal String mValuesInEGP;
        internal String mValuesInUSD;
        internal String mValuesInEUR;
        #endregion

        #region "Methods"
        public Int32 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public Int32 Code
        {
            get { return mCode; }
            set { mCode = value; }
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
        public Int64 OperationID
        {
            get { return mOperationID; }
            set { mOperationID = value; }
        }
        public String OperationCode
        {
            get { return mOperationCode; }
            set { mOperationCode = value; }
        }
        public Int32 OperationCodeSerial
        {
            get { return mOperationCodeSerial; }
            set { mOperationCodeSerial = value; }
        }
        public String ComplaintName
        {
            get { return mComplaintName; }
            set { mComplaintName = value; }
        }
        public String ComplaintDetails
        {
            get { return mComplaintDetails; }
            set { mComplaintDetails = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mNotes = value; }
        }
        public Int32 CreatorUserID
        {
            get { return mCreatorUserID; }
            set { mCreatorUserID = value; }
        }
        public String CreatorUserName
        {
            get { return mCreatorUserName; }
            set { mCreatorUserName = value; }
        }
        public DateTime CreationDate
        {
            get { return mCreationDate; }
            set { mCreationDate = value; }
        }
        public Int32 ModificatorUserID
        {
            get { return mModificatorUserID; }
            set { mModificatorUserID = value; }
        }
        public DateTime ModificationDate
        {
            get { return mModificationDate; }
            set { mModificationDate = value; }
        }
        public String ComplaintDetailsOperationCodeSerials
        {
            get { return mComplaintDetailsOperationCodeSerials; }
            set { mComplaintDetailsOperationCodeSerials = value; }
        }
        public String ComplaintDetailsNames
        {
            get { return mComplaintDetailsNames; }
            set { mComplaintDetailsNames = value; }
        }
        public String ValuesInEGP
        {
            get { return mValuesInEGP; }
            set { mValuesInEGP = value; }
        }
        public String ValuesInUSD
        {
            get { return mValuesInUSD; }
            set { mValuesInUSD = value; }
        }
        public String ValuesInEUR
        {
            get { return mValuesInEUR; }
            set { mValuesInEUR = value; }
        }
        #endregion
    }

    public partial class CvwCRM_Complaint
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
        public List<CVarvwCRM_Complaint> lstCVarvwCRM_Complaint = new List<CVarvwCRM_Complaint>();
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
            lstCVarvwCRM_Complaint.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwCRM_Complaint";
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
                        CVarvwCRM_Complaint ObjCVarvwCRM_Complaint = new CVarvwCRM_Complaint();
                        ObjCVarvwCRM_Complaint.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwCRM_Complaint.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarvwCRM_Complaint.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarvwCRM_Complaint.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwCRM_Complaint.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwCRM_Complaint.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwCRM_Complaint.mOperationCodeSerial = Convert.ToInt32(dr["OperationCodeSerial"].ToString());
                        ObjCVarvwCRM_Complaint.mComplaintName = Convert.ToString(dr["ComplaintName"].ToString());
                        ObjCVarvwCRM_Complaint.mComplaintDetails = Convert.ToString(dr["ComplaintDetails"].ToString());
                        ObjCVarvwCRM_Complaint.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwCRM_Complaint.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwCRM_Complaint.mCreatorUserName = Convert.ToString(dr["CreatorUserName"].ToString());
                        ObjCVarvwCRM_Complaint.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwCRM_Complaint.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwCRM_Complaint.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwCRM_Complaint.mComplaintDetailsOperationCodeSerials = Convert.ToString(dr["ComplaintDetailsOperationCodeSerials"].ToString());
                        ObjCVarvwCRM_Complaint.mComplaintDetailsNames = Convert.ToString(dr["ComplaintDetailsNames"].ToString());
                        ObjCVarvwCRM_Complaint.mValuesInEGP = Convert.ToString(dr["ValuesInEGP"].ToString());
                        ObjCVarvwCRM_Complaint.mValuesInUSD = Convert.ToString(dr["ValuesInUSD"].ToString());
                        ObjCVarvwCRM_Complaint.mValuesInEUR = Convert.ToString(dr["ValuesInEUR"].ToString());
                        lstCVarvwCRM_Complaint.Add(ObjCVarvwCRM_Complaint);
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
            lstCVarvwCRM_Complaint.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwCRM_Complaint";
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
                        CVarvwCRM_Complaint ObjCVarvwCRM_Complaint = new CVarvwCRM_Complaint();
                        ObjCVarvwCRM_Complaint.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwCRM_Complaint.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarvwCRM_Complaint.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarvwCRM_Complaint.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwCRM_Complaint.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwCRM_Complaint.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwCRM_Complaint.mOperationCodeSerial = Convert.ToInt32(dr["OperationCodeSerial"].ToString());
                        ObjCVarvwCRM_Complaint.mComplaintName = Convert.ToString(dr["ComplaintName"].ToString());
                        ObjCVarvwCRM_Complaint.mComplaintDetails = Convert.ToString(dr["ComplaintDetails"].ToString());
                        ObjCVarvwCRM_Complaint.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwCRM_Complaint.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwCRM_Complaint.mCreatorUserName = Convert.ToString(dr["CreatorUserName"].ToString());
                        ObjCVarvwCRM_Complaint.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwCRM_Complaint.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwCRM_Complaint.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwCRM_Complaint.mComplaintDetailsOperationCodeSerials = Convert.ToString(dr["ComplaintDetailsOperationCodeSerials"].ToString());
                        ObjCVarvwCRM_Complaint.mComplaintDetailsNames = Convert.ToString(dr["ComplaintDetailsNames"].ToString());
                        ObjCVarvwCRM_Complaint.mValuesInEGP = Convert.ToString(dr["ValuesInEGP"].ToString());
                        ObjCVarvwCRM_Complaint.mValuesInUSD = Convert.ToString(dr["ValuesInUSD"].ToString());
                        ObjCVarvwCRM_Complaint.mValuesInEUR = Convert.ToString(dr["ValuesInEUR"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwCRM_Complaint.Add(ObjCVarvwCRM_Complaint);
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