﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.ReceiptsAndPayments.Custodies.Generated
{
    [Serializable]
    public partial class CVarvwA_PaymentRequestSupplier
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mID;
        internal String mCode;
        internal Int32 mSupplierID;
        internal String mSupplierName;
        internal String mOperations;
        internal DateTime mRequestDate;
        internal DateTime mSettlementDate;
        internal Int32 mCurrencyID;
        internal String mCurrencyCode;
        internal Boolean mIsCheck;
        internal Decimal mTotalEstmatedCost;
        internal Decimal mTotalActualCost;
        internal Decimal mTotalDiff;
        internal String mNotes;
        internal Boolean mIsApprovedRequest;
        internal Boolean mIsApprovedSettlement;
        internal Boolean mIsSettled;
        internal Int64 mJVID;
        internal Int64 mJVID2;
        internal Int32 mCreatorUserID_Request;
        internal Int32 mCreatorUserID_Settlement;
        internal Int32 mModificatorUserID_Request;
        internal Int32 mModificatorUserID_Settlement;
        internal DateTime mCreationDate_Request;
        internal DateTime mCreationDate_Settlement;
        internal DateTime mModificationDate_Request;
        internal DateTime mModificationDate_Settlement;
        internal Int64 mVoucherID;
        #endregion

        #region "Methods"
        public Int64 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public String Code
        {
            get { return mCode; }
            set { mCode = value; }
        }
        public Int32 SupplierID
        {
            get { return mSupplierID; }
            set { mSupplierID = value; }
        }
        public String SupplierName
        {
            get { return mSupplierName; }
            set { mSupplierName = value; }
        }
        public String Operations
        {
            get { return mOperations; }
            set { mOperations = value; }
        }
        public DateTime RequestDate
        {
            get { return mRequestDate; }
            set { mRequestDate = value; }
        }
        public DateTime SettlementDate
        {
            get { return mSettlementDate; }
            set { mSettlementDate = value; }
        }
        public Int32 CurrencyID
        {
            get { return mCurrencyID; }
            set { mCurrencyID = value; }
        }
        public String CurrencyCode
        {
            get { return mCurrencyCode; }
            set { mCurrencyCode = value; }
        }
        public Boolean IsCheck
        {
            get { return mIsCheck; }
            set { mIsCheck = value; }
        }
        public Decimal TotalEstmatedCost
        {
            get { return mTotalEstmatedCost; }
            set { mTotalEstmatedCost = value; }
        }
        public Decimal TotalActualCost
        {
            get { return mTotalActualCost; }
            set { mTotalActualCost = value; }
        }
        public Decimal TotalDiff
        {
            get { return mTotalDiff; }
            set { mTotalDiff = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mNotes = value; }
        }
        public Boolean IsApprovedRequest
        {
            get { return mIsApprovedRequest; }
            set { mIsApprovedRequest = value; }
        }
        public Boolean IsApprovedSettlement
        {
            get { return mIsApprovedSettlement; }
            set { mIsApprovedSettlement = value; }
        }
        public Boolean IsSettled
        {
            get { return mIsSettled; }
            set { mIsSettled = value; }
        }
        public Int64 JVID
        {
            get { return mJVID; }
            set { mJVID = value; }
        }
        public Int64 JVID2
        {
            get { return mJVID2; }
            set { mJVID2 = value; }
        }
        public Int32 CreatorUserID_Request
        {
            get { return mCreatorUserID_Request; }
            set { mCreatorUserID_Request = value; }
        }
        public Int32 CreatorUserID_Settlement
        {
            get { return mCreatorUserID_Settlement; }
            set { mCreatorUserID_Settlement = value; }
        }
        public Int32 ModificatorUserID_Request
        {
            get { return mModificatorUserID_Request; }
            set { mModificatorUserID_Request = value; }
        }
        public Int32 ModificatorUserID_Settlement
        {
            get { return mModificatorUserID_Settlement; }
            set { mModificatorUserID_Settlement = value; }
        }
        public DateTime CreationDate_Request
        {
            get { return mCreationDate_Request; }
            set { mCreationDate_Request = value; }
        }
        public DateTime CreationDate_Settlement
        {
            get { return mCreationDate_Settlement; }
            set { mCreationDate_Settlement = value; }
        }
        public DateTime ModificationDate_Request
        {
            get { return mModificationDate_Request; }
            set { mModificationDate_Request = value; }
        }
        public DateTime ModificationDate_Settlement
        {
            get { return mModificationDate_Settlement; }
            set { mModificationDate_Settlement = value; }
        }
        public Int64 VoucherID
        {
            get { return mVoucherID; }
            set { mVoucherID = value; }
        }
        #endregion
    }

    public partial class CvwA_PaymentRequestSupplier
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
        public List<CVarvwA_PaymentRequestSupplier> lstCVarvwA_PaymentRequestSupplier = new List<CVarvwA_PaymentRequestSupplier>();
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
            lstCVarvwA_PaymentRequestSupplier.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwA_PaymentRequestSupplier";
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
                        CVarvwA_PaymentRequestSupplier ObjCVarvwA_PaymentRequestSupplier = new CVarvwA_PaymentRequestSupplier();
                        ObjCVarvwA_PaymentRequestSupplier.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mSupplierID = Convert.ToInt32(dr["SupplierID"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mSupplierName = Convert.ToString(dr["SupplierName"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mOperations = Convert.ToString(dr["Operations"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mRequestDate = Convert.ToDateTime(dr["RequestDate"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mSettlementDate = Convert.ToDateTime(dr["SettlementDate"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mIsCheck = Convert.ToBoolean(dr["IsCheck"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mTotalEstmatedCost = Convert.ToDecimal(dr["TotalEstmatedCost"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mTotalActualCost = Convert.ToDecimal(dr["TotalActualCost"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mTotalDiff = Convert.ToDecimal(dr["TotalDiff"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mIsApprovedRequest = Convert.ToBoolean(dr["IsApprovedRequest"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mIsApprovedSettlement = Convert.ToBoolean(dr["IsApprovedSettlement"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mIsSettled = Convert.ToBoolean(dr["IsSettled"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mJVID = Convert.ToInt64(dr["JVID"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mJVID2 = Convert.ToInt64(dr["JVID2"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mCreatorUserID_Request = Convert.ToInt32(dr["CreatorUserID_Request"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mCreatorUserID_Settlement = Convert.ToInt32(dr["CreatorUserID_Settlement"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mModificatorUserID_Request = Convert.ToInt32(dr["ModificatorUserID_Request"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mModificatorUserID_Settlement = Convert.ToInt32(dr["ModificatorUserID_Settlement"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mCreationDate_Request = Convert.ToDateTime(dr["CreationDate_Request"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mCreationDate_Settlement = Convert.ToDateTime(dr["CreationDate_Settlement"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mModificationDate_Request = Convert.ToDateTime(dr["ModificationDate_Request"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mModificationDate_Settlement = Convert.ToDateTime(dr["ModificationDate_Settlement"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mVoucherID = Convert.ToInt64(dr["VoucherID"].ToString());
                        lstCVarvwA_PaymentRequestSupplier.Add(ObjCVarvwA_PaymentRequestSupplier);
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
            lstCVarvwA_PaymentRequestSupplier.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwA_PaymentRequestSupplier";
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
                        CVarvwA_PaymentRequestSupplier ObjCVarvwA_PaymentRequestSupplier = new CVarvwA_PaymentRequestSupplier();
                        ObjCVarvwA_PaymentRequestSupplier.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mSupplierID = Convert.ToInt32(dr["SupplierID"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mSupplierName = Convert.ToString(dr["SupplierName"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mOperations = Convert.ToString(dr["Operations"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mRequestDate = Convert.ToDateTime(dr["RequestDate"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mSettlementDate = Convert.ToDateTime(dr["SettlementDate"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mIsCheck = Convert.ToBoolean(dr["IsCheck"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mTotalEstmatedCost = Convert.ToDecimal(dr["TotalEstmatedCost"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mTotalActualCost = Convert.ToDecimal(dr["TotalActualCost"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mTotalDiff = Convert.ToDecimal(dr["TotalDiff"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mIsApprovedRequest = Convert.ToBoolean(dr["IsApprovedRequest"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mIsApprovedSettlement = Convert.ToBoolean(dr["IsApprovedSettlement"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mIsSettled = Convert.ToBoolean(dr["IsSettled"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mJVID = Convert.ToInt64(dr["JVID"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mJVID2 = Convert.ToInt64(dr["JVID2"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mCreatorUserID_Request = Convert.ToInt32(dr["CreatorUserID_Request"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mCreatorUserID_Settlement = Convert.ToInt32(dr["CreatorUserID_Settlement"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mModificatorUserID_Request = Convert.ToInt32(dr["ModificatorUserID_Request"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mModificatorUserID_Settlement = Convert.ToInt32(dr["ModificatorUserID_Settlement"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mCreationDate_Request = Convert.ToDateTime(dr["CreationDate_Request"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mCreationDate_Settlement = Convert.ToDateTime(dr["CreationDate_Settlement"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mModificationDate_Request = Convert.ToDateTime(dr["ModificationDate_Request"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mModificationDate_Settlement = Convert.ToDateTime(dr["ModificationDate_Settlement"].ToString());
                        ObjCVarvwA_PaymentRequestSupplier.mVoucherID = Convert.ToInt64(dr["VoucherID"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwA_PaymentRequestSupplier.Add(ObjCVarvwA_PaymentRequestSupplier);
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
