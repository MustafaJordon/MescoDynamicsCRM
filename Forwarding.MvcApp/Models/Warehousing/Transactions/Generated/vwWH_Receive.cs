﻿using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.Warehousing.Transactions.Generated
{
    [Serializable]
    public class CPKvwWH_Receive
    {
        #region "variables"
        private Int64 mID;
        #endregion

        #region "Methods"
        public Int64 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        #endregion
    }
    [Serializable]
    public partial class CVarvwWH_Receive : CPKvwWH_Receive
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mCodeSerial;
        internal String mCode;
        internal Int32 mWarehouseID;
        internal String mWarehouseName;
        internal Int32 mCustomerID;
        internal String mCustomerName;
        internal DateTime mReceiveDate;
        internal DateTime mETD;
        internal DateTime mETA;
        internal DateTime mArrivalDate;
        internal Int32 mStatusID;
        internal String mStatusName;
        internal Boolean mIsFinalized;
        internal DateTime mFinalizeDate;
        internal String mNotes;
        internal Int64 mInvoiceID;
        internal String mInvoiceCode;
        internal Int64 mOperationID;
        internal String mOperationCode;
        internal Int32 mCreatorUserID;
        internal String mCreatorName;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal String mModificatorName;
        internal DateTime mModificationDate;
        #endregion

        #region "Methods"
        public Int32 CodeSerial
        {
            get { return mCodeSerial; }
            set { mCodeSerial = value; }
        }
        public String Code
        {
            get { return mCode; }
            set { mCode = value; }
        }
        public Int32 WarehouseID
        {
            get { return mWarehouseID; }
            set { mWarehouseID = value; }
        }
        public String WarehouseName
        {
            get { return mWarehouseName; }
            set { mWarehouseName = value; }
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
        public DateTime ReceiveDate
        {
            get { return mReceiveDate; }
            set { mReceiveDate = value; }
        }
        public DateTime ETD
        {
            get { return mETD; }
            set { mETD = value; }
        }
        public DateTime ETA
        {
            get { return mETA; }
            set { mETA = value; }
        }
        public DateTime ArrivalDate
        {
            get { return mArrivalDate; }
            set { mArrivalDate = value; }
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
        public Boolean IsFinalized
        {
            get { return mIsFinalized; }
            set { mIsFinalized = value; }
        }
        public DateTime FinalizeDate
        {
            get { return mFinalizeDate; }
            set { mFinalizeDate = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mNotes = value; }
        }
        public Int64 InvoiceID
        {
            get { return mInvoiceID; }
            set { mInvoiceID = value; }
        }
        public String InvoiceCode
        {
            get { return mInvoiceCode; }
            set { mInvoiceCode = value; }
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
        public Int32 CreatorUserID
        {
            get { return mCreatorUserID; }
            set { mCreatorUserID = value; }
        }
        public String CreatorName
        {
            get { return mCreatorName; }
            set { mCreatorName = value; }
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
        public String ModificatorName
        {
            get { return mModificatorName; }
            set { mModificatorName = value; }
        }
        public DateTime ModificationDate
        {
            get { return mModificationDate; }
            set { mModificationDate = value; }
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

    public partial class CvwWH_Receive
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
        public List<CVarvwWH_Receive> lstCVarvwWH_Receive = new List<CVarvwWH_Receive>();
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
            lstCVarvwWH_Receive.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwWH_Receive";
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
                        CVarvwWH_Receive ObjCVarvwWH_Receive = new CVarvwWH_Receive();
                        ObjCVarvwWH_Receive.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwWH_Receive.mCodeSerial = Convert.ToInt32(dr["CodeSerial"].ToString());
                        ObjCVarvwWH_Receive.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwWH_Receive.mWarehouseID = Convert.ToInt32(dr["WarehouseID"].ToString());
                        ObjCVarvwWH_Receive.mWarehouseName = Convert.ToString(dr["WarehouseName"].ToString());
                        ObjCVarvwWH_Receive.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarvwWH_Receive.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwWH_Receive.mReceiveDate = Convert.ToDateTime(dr["ReceiveDate"].ToString());
                        ObjCVarvwWH_Receive.mETD = Convert.ToDateTime(dr["ETD"].ToString());
                        ObjCVarvwWH_Receive.mETA = Convert.ToDateTime(dr["ETA"].ToString());
                        ObjCVarvwWH_Receive.mArrivalDate = Convert.ToDateTime(dr["ArrivalDate"].ToString());
                        ObjCVarvwWH_Receive.mStatusID = Convert.ToInt32(dr["StatusID"].ToString());
                        ObjCVarvwWH_Receive.mStatusName = Convert.ToString(dr["StatusName"].ToString());
                        ObjCVarvwWH_Receive.mIsFinalized = Convert.ToBoolean(dr["IsFinalized"].ToString());
                        ObjCVarvwWH_Receive.mFinalizeDate = Convert.ToDateTime(dr["FinalizeDate"].ToString());
                        ObjCVarvwWH_Receive.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwWH_Receive.mInvoiceID = Convert.ToInt64(dr["InvoiceID"].ToString());
                        ObjCVarvwWH_Receive.mInvoiceCode = Convert.ToString(dr["InvoiceCode"].ToString());
                        ObjCVarvwWH_Receive.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwWH_Receive.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwWH_Receive.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwWH_Receive.mCreatorName = Convert.ToString(dr["CreatorName"].ToString());
                        ObjCVarvwWH_Receive.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwWH_Receive.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwWH_Receive.mModificatorName = Convert.ToString(dr["ModificatorName"].ToString());
                        ObjCVarvwWH_Receive.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        lstCVarvwWH_Receive.Add(ObjCVarvwWH_Receive);
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
            lstCVarvwWH_Receive.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwWH_Receive";
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
                        CVarvwWH_Receive ObjCVarvwWH_Receive = new CVarvwWH_Receive();
                        ObjCVarvwWH_Receive.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwWH_Receive.mCodeSerial = Convert.ToInt32(dr["CodeSerial"].ToString());
                        ObjCVarvwWH_Receive.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwWH_Receive.mWarehouseID = Convert.ToInt32(dr["WarehouseID"].ToString());
                        ObjCVarvwWH_Receive.mWarehouseName = Convert.ToString(dr["WarehouseName"].ToString());
                        ObjCVarvwWH_Receive.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarvwWH_Receive.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwWH_Receive.mReceiveDate = Convert.ToDateTime(dr["ReceiveDate"].ToString());
                        ObjCVarvwWH_Receive.mETD = Convert.ToDateTime(dr["ETD"].ToString());
                        ObjCVarvwWH_Receive.mETA = Convert.ToDateTime(dr["ETA"].ToString());
                        ObjCVarvwWH_Receive.mArrivalDate = Convert.ToDateTime(dr["ArrivalDate"].ToString());
                        ObjCVarvwWH_Receive.mStatusID = Convert.ToInt32(dr["StatusID"].ToString());
                        ObjCVarvwWH_Receive.mStatusName = Convert.ToString(dr["StatusName"].ToString());
                        ObjCVarvwWH_Receive.mIsFinalized = Convert.ToBoolean(dr["IsFinalized"].ToString());
                        ObjCVarvwWH_Receive.mFinalizeDate = Convert.ToDateTime(dr["FinalizeDate"].ToString());
                        ObjCVarvwWH_Receive.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwWH_Receive.mInvoiceID = Convert.ToInt64(dr["InvoiceID"].ToString());
                        ObjCVarvwWH_Receive.mInvoiceCode = Convert.ToString(dr["InvoiceCode"].ToString());
                        ObjCVarvwWH_Receive.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwWH_Receive.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwWH_Receive.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwWH_Receive.mCreatorName = Convert.ToString(dr["CreatorName"].ToString());
                        ObjCVarvwWH_Receive.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwWH_Receive.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwWH_Receive.mModificatorName = Convert.ToString(dr["ModificatorName"].ToString());
                        ObjCVarvwWH_Receive.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwWH_Receive.Add(ObjCVarvwWH_Receive);
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
