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
    public partial class CVarvwCRM_ComplaintDetails
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mID;
        internal Int32 mCRM_ComplaintID;
        internal Int32 mStatusID;
        internal String mStatueCode;
        internal Int32 mOperationID;
        internal String mOperationCode;
        internal Int32 mOperationCodeSerial;
        internal String mComplaint;
        internal String mComplaintDescription;
        internal DateTime mComplaintDate;
        internal Int32 mSalesRepID;
        internal String mSalesRepName;
        internal String mResponseDescription;
        internal DateTime mResponseDate;
        internal Int32 mSalesRepID2;
        internal String mSalesRepName2;
        internal Int32 mComplaintNameID;
        internal String mComplaintName;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal String mValueInEGP;
        internal String mValueInUSD;
        internal String mValueInEUR;
        #endregion

        #region "Methods"
        public Int32 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public Int32 CRM_ComplaintID
        {
            get { return mCRM_ComplaintID; }
            set { mCRM_ComplaintID = value; }
        }
        public Int32 StatusID
        {
            get { return mStatusID; }
            set { mStatusID = value; }
        }
        public String StatueCode
        {
            get { return mStatueCode; }
            set { mStatueCode = value; }
        }
        public Int32 OperationID
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
        public String Complaint
        {
            get { return mComplaint; }
            set { mComplaint = value; }
        }
        public String ComplaintDescription
        {
            get { return mComplaintDescription; }
            set { mComplaintDescription = value; }
        }
        public DateTime ComplaintDate
        {
            get { return mComplaintDate; }
            set { mComplaintDate = value; }
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
        public Int32 SalesRepID2
        {
            get { return mSalesRepID2; }
            set { mSalesRepID2 = value; }
        }
        public String SalesRepName2
        {
            get { return mSalesRepName2; }
            set { mSalesRepName2 = value; }
        }
        public Int32 ComplaintNameID
        {
            get { return mComplaintNameID; }
            set { mComplaintNameID = value; }
        }
        public String ComplaintName
        {
            get { return mComplaintName; }
            set { mComplaintName = value; }
        }
        public Int32 CreatorUserID
        {
            get { return mCreatorUserID; }
            set { mCreatorUserID = value; }
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
        public String ValueInEGP
        {
            get { return mValueInEGP; }
            set { mValueInEGP = value; }
        }
        public String ValueInUSD
        {
            get { return mValueInUSD; }
            set { mValueInUSD = value; }
        }
        public String ValueInEUR
        {
            get { return mValueInEUR; }
            set { mValueInEUR = value; }
        }
        #endregion
    }

    public partial class CvwCRM_ComplaintDetails
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
        public List<CVarvwCRM_ComplaintDetails> lstCVarvwCRM_ComplaintDetails = new List<CVarvwCRM_ComplaintDetails>();
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
            lstCVarvwCRM_ComplaintDetails.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwCRM_ComplaintDetails";
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
                        CVarvwCRM_ComplaintDetails ObjCVarvwCRM_ComplaintDetails = new CVarvwCRM_ComplaintDetails();
                        ObjCVarvwCRM_ComplaintDetails.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mCRM_ComplaintID = Convert.ToInt32(dr["CRM_ComplaintID"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mStatusID = Convert.ToInt32(dr["StatusID"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mStatueCode = Convert.ToString(dr["StatueCode"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mOperationID = Convert.ToInt32(dr["OperationID"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mOperationCodeSerial = Convert.ToInt32(dr["OperationCodeSerial"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mComplaint = Convert.ToString(dr["Complaint"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mComplaintDescription = Convert.ToString(dr["ComplaintDescription"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mComplaintDate = Convert.ToDateTime(dr["ComplaintDate"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mSalesRepID = Convert.ToInt32(dr["SalesRepID"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mSalesRepName = Convert.ToString(dr["SalesRepName"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mResponseDescription = Convert.ToString(dr["ResponseDescription"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mResponseDate = Convert.ToDateTime(dr["ResponseDate"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mSalesRepID2 = Convert.ToInt32(dr["SalesRepID2"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mSalesRepName2 = Convert.ToString(dr["SalesRepName2"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mComplaintNameID = Convert.ToInt32(dr["ComplaintNameID"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mComplaintName = Convert.ToString(dr["ComplaintName"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mValueInEGP = Convert.ToString(dr["ValueInEGP"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mValueInUSD = Convert.ToString(dr["ValueInUSD"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mValueInEUR = Convert.ToString(dr["ValueInEUR"].ToString());
                        lstCVarvwCRM_ComplaintDetails.Add(ObjCVarvwCRM_ComplaintDetails);
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
            lstCVarvwCRM_ComplaintDetails.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwCRM_ComplaintDetails";
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
                        CVarvwCRM_ComplaintDetails ObjCVarvwCRM_ComplaintDetails = new CVarvwCRM_ComplaintDetails();
                        ObjCVarvwCRM_ComplaintDetails.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mCRM_ComplaintID = Convert.ToInt32(dr["CRM_ComplaintID"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mStatusID = Convert.ToInt32(dr["StatusID"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mStatueCode = Convert.ToString(dr["StatueCode"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mOperationID = Convert.ToInt32(dr["OperationID"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mOperationCodeSerial = Convert.ToInt32(dr["OperationCodeSerial"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mComplaint = Convert.ToString(dr["Complaint"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mComplaintDescription = Convert.ToString(dr["ComplaintDescription"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mComplaintDate = Convert.ToDateTime(dr["ComplaintDate"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mSalesRepID = Convert.ToInt32(dr["SalesRepID"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mSalesRepName = Convert.ToString(dr["SalesRepName"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mResponseDescription = Convert.ToString(dr["ResponseDescription"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mResponseDate = Convert.ToDateTime(dr["ResponseDate"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mSalesRepID2 = Convert.ToInt32(dr["SalesRepID2"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mSalesRepName2 = Convert.ToString(dr["SalesRepName2"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mComplaintNameID = Convert.ToInt32(dr["ComplaintNameID"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mComplaintName = Convert.ToString(dr["ComplaintName"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mValueInEGP = Convert.ToString(dr["ValueInEGP"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mValueInUSD = Convert.ToString(dr["ValueInUSD"].ToString());
                        ObjCVarvwCRM_ComplaintDetails.mValueInEUR = Convert.ToString(dr["ValueInEUR"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwCRM_ComplaintDetails.Add(ObjCVarvwCRM_ComplaintDetails);
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
