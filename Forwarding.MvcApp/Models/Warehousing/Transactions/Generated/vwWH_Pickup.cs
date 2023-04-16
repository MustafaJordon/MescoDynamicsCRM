﻿using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.Warehousing.Transactions.Generated
{
    [Serializable]
    public partial class CVarvwWH_Pickup
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mID;
        internal Int32 mCodeSerial;
        internal String mCode;
        internal Int32 mWarehouseID;
        internal Int32 mCustomerID;
        internal String mCustomerName;
        internal String mCustomerLocalName;
        internal Int64 mPersonInChargeID;
        internal String mPersonInChargeName;
        internal Int32 mBillTo;
        internal String mBillToName;
        internal String mBillToLocalName;
        internal String mBillToAddress;
        internal String mBillToContactName;
        internal String mBillToContactPhones;
        internal DateTime mOrderDate;
        internal DateTime mRequiredDate;
        internal Boolean mIsFinalized;
        internal String mStatusName;
        internal DateTime mFinalizeDate;
        internal String mNotes;
        internal Int64 mInvoiceID;
        internal String mInvoiceCode;
        internal Int32 mCreatorUserID;
        internal String mCreatorName;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal String mModificatorName;
        internal DateTime mModificationDate;
        internal Int64 mOperationID;
        internal String mOperationCode;
        internal Int32 mOperationCodeSerial;
        internal String mPONumber;
        internal Int32 mEndUserID;
        internal String mEndUserName;
        internal String mEndUserLocalName;
        internal String mEndUserAddress;
        internal String mEndUserContactName;
        internal String mEndUserContactPhones;
        internal String mRMANumber;
        internal Int64 mPDIReceiveDetailsID;
        internal String mPDICode;
        #endregion

        #region "Methods"
        public Int64 ID
        {
            get { return mID; }
            set { mID = value; }
        }
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
        public String CustomerLocalName
        {
            get { return mCustomerLocalName; }
            set { mCustomerLocalName = value; }
        }
        public Int64 PersonInChargeID
        {
            get { return mPersonInChargeID; }
            set { mPersonInChargeID = value; }
        }
        public String PersonInChargeName
        {
            get { return mPersonInChargeName; }
            set { mPersonInChargeName = value; }
        }
        public Int32 BillTo
        {
            get { return mBillTo; }
            set { mBillTo = value; }
        }
        public String BillToName
        {
            get { return mBillToName; }
            set { mBillToName = value; }
        }
        public String BillToLocalName
        {
            get { return mBillToLocalName; }
            set { mBillToLocalName = value; }
        }
        public String BillToAddress
        {
            get { return mBillToAddress; }
            set { mBillToAddress = value; }
        }
        public String BillToContactName
        {
            get { return mBillToContactName; }
            set { mBillToContactName = value; }
        }
        public String BillToContactPhones
        {
            get { return mBillToContactPhones; }
            set { mBillToContactPhones = value; }
        }
        public DateTime OrderDate
        {
            get { return mOrderDate; }
            set { mOrderDate = value; }
        }
        public DateTime RequiredDate
        {
            get { return mRequiredDate; }
            set { mRequiredDate = value; }
        }
        public Boolean IsFinalized
        {
            get { return mIsFinalized; }
            set { mIsFinalized = value; }
        }
        public String StatusName
        {
            get { return mStatusName; }
            set { mStatusName = value; }
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
        public String PONumber
        {
            get { return mPONumber; }
            set { mPONumber = value; }
        }
        public Int32 EndUserID
        {
            get { return mEndUserID; }
            set { mEndUserID = value; }
        }
        public String EndUserName
        {
            get { return mEndUserName; }
            set { mEndUserName = value; }
        }
        public String EndUserLocalName
        {
            get { return mEndUserLocalName; }
            set { mEndUserLocalName = value; }
        }
        public String EndUserAddress
        {
            get { return mEndUserAddress; }
            set { mEndUserAddress = value; }
        }
        public String EndUserContactName
        {
            get { return mEndUserContactName; }
            set { mEndUserContactName = value; }
        }
        public String EndUserContactPhones
        {
            get { return mEndUserContactPhones; }
            set { mEndUserContactPhones = value; }
        }
        public String RMANumber
        {
            get { return mRMANumber; }
            set { mRMANumber = value; }
        }
        public Int64 PDIReceiveDetailsID
        {
            get { return mPDIReceiveDetailsID; }
            set { mPDIReceiveDetailsID = value; }
        }
        public String PDICode
        {
            get { return mPDICode; }
            set { mPDICode = value; }
        }
        #endregion
    }

    public partial class CvwWH_Pickup
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
        public List<CVarvwWH_Pickup> lstCVarvwWH_Pickup = new List<CVarvwWH_Pickup>();
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
            lstCVarvwWH_Pickup.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwWH_Pickup";
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
                        CVarvwWH_Pickup ObjCVarvwWH_Pickup = new CVarvwWH_Pickup();
                        ObjCVarvwWH_Pickup.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwWH_Pickup.mCodeSerial = Convert.ToInt32(dr["CodeSerial"].ToString());
                        ObjCVarvwWH_Pickup.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwWH_Pickup.mWarehouseID = Convert.ToInt32(dr["WarehouseID"].ToString());
                        ObjCVarvwWH_Pickup.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarvwWH_Pickup.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwWH_Pickup.mCustomerLocalName = Convert.ToString(dr["CustomerLocalName"].ToString());
                        ObjCVarvwWH_Pickup.mPersonInChargeID = Convert.ToInt64(dr["PersonInChargeID"].ToString());
                        ObjCVarvwWH_Pickup.mPersonInChargeName = Convert.ToString(dr["PersonInChargeName"].ToString());
                        ObjCVarvwWH_Pickup.mBillTo = Convert.ToInt32(dr["BillTo"].ToString());
                        ObjCVarvwWH_Pickup.mBillToName = Convert.ToString(dr["BillToName"].ToString());
                        ObjCVarvwWH_Pickup.mBillToLocalName = Convert.ToString(dr["BillToLocalName"].ToString());
                        ObjCVarvwWH_Pickup.mBillToAddress = Convert.ToString(dr["BillToAddress"].ToString());
                        ObjCVarvwWH_Pickup.mBillToContactName = Convert.ToString(dr["BillToContactName"].ToString());
                        ObjCVarvwWH_Pickup.mBillToContactPhones = Convert.ToString(dr["BillToContactPhones"].ToString());
                        ObjCVarvwWH_Pickup.mOrderDate = Convert.ToDateTime(dr["OrderDate"].ToString());
                        ObjCVarvwWH_Pickup.mRequiredDate = Convert.ToDateTime(dr["RequiredDate"].ToString());
                        ObjCVarvwWH_Pickup.mIsFinalized = Convert.ToBoolean(dr["IsFinalized"].ToString());
                        ObjCVarvwWH_Pickup.mStatusName = Convert.ToString(dr["StatusName"].ToString());
                        ObjCVarvwWH_Pickup.mFinalizeDate = Convert.ToDateTime(dr["FinalizeDate"].ToString());
                        ObjCVarvwWH_Pickup.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwWH_Pickup.mInvoiceID = Convert.ToInt64(dr["InvoiceID"].ToString());
                        ObjCVarvwWH_Pickup.mInvoiceCode = Convert.ToString(dr["InvoiceCode"].ToString());
                        ObjCVarvwWH_Pickup.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwWH_Pickup.mCreatorName = Convert.ToString(dr["CreatorName"].ToString());
                        ObjCVarvwWH_Pickup.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwWH_Pickup.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwWH_Pickup.mModificatorName = Convert.ToString(dr["ModificatorName"].ToString());
                        ObjCVarvwWH_Pickup.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwWH_Pickup.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwWH_Pickup.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwWH_Pickup.mOperationCodeSerial = Convert.ToInt32(dr["OperationCodeSerial"].ToString());
                        ObjCVarvwWH_Pickup.mPONumber = Convert.ToString(dr["PONumber"].ToString());
                        ObjCVarvwWH_Pickup.mEndUserID = Convert.ToInt32(dr["EndUserID"].ToString());
                        ObjCVarvwWH_Pickup.mEndUserName = Convert.ToString(dr["EndUserName"].ToString());
                        ObjCVarvwWH_Pickup.mEndUserLocalName = Convert.ToString(dr["EndUserLocalName"].ToString());
                        ObjCVarvwWH_Pickup.mEndUserAddress = Convert.ToString(dr["EndUserAddress"].ToString());
                        ObjCVarvwWH_Pickup.mEndUserContactName = Convert.ToString(dr["EndUserContactName"].ToString());
                        ObjCVarvwWH_Pickup.mEndUserContactPhones = Convert.ToString(dr["EndUserContactPhones"].ToString());
                        ObjCVarvwWH_Pickup.mRMANumber = Convert.ToString(dr["RMANumber"].ToString());
                        ObjCVarvwWH_Pickup.mPDIReceiveDetailsID = Convert.ToInt64(dr["PDIReceiveDetailsID"].ToString());
                        ObjCVarvwWH_Pickup.mPDICode = Convert.ToString(dr["PDICode"].ToString());
                        lstCVarvwWH_Pickup.Add(ObjCVarvwWH_Pickup);
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
            lstCVarvwWH_Pickup.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwWH_Pickup";
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
                        CVarvwWH_Pickup ObjCVarvwWH_Pickup = new CVarvwWH_Pickup();
                        ObjCVarvwWH_Pickup.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwWH_Pickup.mCodeSerial = Convert.ToInt32(dr["CodeSerial"].ToString());
                        ObjCVarvwWH_Pickup.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwWH_Pickup.mWarehouseID = Convert.ToInt32(dr["WarehouseID"].ToString());
                        ObjCVarvwWH_Pickup.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarvwWH_Pickup.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwWH_Pickup.mCustomerLocalName = Convert.ToString(dr["CustomerLocalName"].ToString());
                        ObjCVarvwWH_Pickup.mPersonInChargeID = Convert.ToInt64(dr["PersonInChargeID"].ToString());
                        ObjCVarvwWH_Pickup.mPersonInChargeName = Convert.ToString(dr["PersonInChargeName"].ToString());
                        ObjCVarvwWH_Pickup.mBillTo = Convert.ToInt32(dr["BillTo"].ToString());
                        ObjCVarvwWH_Pickup.mBillToName = Convert.ToString(dr["BillToName"].ToString());
                        ObjCVarvwWH_Pickup.mBillToLocalName = Convert.ToString(dr["BillToLocalName"].ToString());
                        ObjCVarvwWH_Pickup.mBillToAddress = Convert.ToString(dr["BillToAddress"].ToString());
                        ObjCVarvwWH_Pickup.mBillToContactName = Convert.ToString(dr["BillToContactName"].ToString());
                        ObjCVarvwWH_Pickup.mBillToContactPhones = Convert.ToString(dr["BillToContactPhones"].ToString());
                        ObjCVarvwWH_Pickup.mOrderDate = Convert.ToDateTime(dr["OrderDate"].ToString());
                        ObjCVarvwWH_Pickup.mRequiredDate = Convert.ToDateTime(dr["RequiredDate"].ToString());
                        ObjCVarvwWH_Pickup.mIsFinalized = Convert.ToBoolean(dr["IsFinalized"].ToString());
                        ObjCVarvwWH_Pickup.mStatusName = Convert.ToString(dr["StatusName"].ToString());
                        ObjCVarvwWH_Pickup.mFinalizeDate = Convert.ToDateTime(dr["FinalizeDate"].ToString());
                        ObjCVarvwWH_Pickup.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwWH_Pickup.mInvoiceID = Convert.ToInt64(dr["InvoiceID"].ToString());
                        ObjCVarvwWH_Pickup.mInvoiceCode = Convert.ToString(dr["InvoiceCode"].ToString());
                        ObjCVarvwWH_Pickup.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwWH_Pickup.mCreatorName = Convert.ToString(dr["CreatorName"].ToString());
                        ObjCVarvwWH_Pickup.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwWH_Pickup.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwWH_Pickup.mModificatorName = Convert.ToString(dr["ModificatorName"].ToString());
                        ObjCVarvwWH_Pickup.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwWH_Pickup.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwWH_Pickup.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwWH_Pickup.mOperationCodeSerial = Convert.ToInt32(dr["OperationCodeSerial"].ToString());
                        ObjCVarvwWH_Pickup.mPONumber = Convert.ToString(dr["PONumber"].ToString());
                        ObjCVarvwWH_Pickup.mEndUserID = Convert.ToInt32(dr["EndUserID"].ToString());
                        ObjCVarvwWH_Pickup.mEndUserName = Convert.ToString(dr["EndUserName"].ToString());
                        ObjCVarvwWH_Pickup.mEndUserLocalName = Convert.ToString(dr["EndUserLocalName"].ToString());
                        ObjCVarvwWH_Pickup.mEndUserAddress = Convert.ToString(dr["EndUserAddress"].ToString());
                        ObjCVarvwWH_Pickup.mEndUserContactName = Convert.ToString(dr["EndUserContactName"].ToString());
                        ObjCVarvwWH_Pickup.mEndUserContactPhones = Convert.ToString(dr["EndUserContactPhones"].ToString());
                        ObjCVarvwWH_Pickup.mRMANumber = Convert.ToString(dr["RMANumber"].ToString());
                        ObjCVarvwWH_Pickup.mPDIReceiveDetailsID = Convert.ToInt64(dr["PDIReceiveDetailsID"].ToString());
                        ObjCVarvwWH_Pickup.mPDICode = Convert.ToString(dr["PDICode"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwWH_Pickup.Add(ObjCVarvwWH_Pickup);
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
