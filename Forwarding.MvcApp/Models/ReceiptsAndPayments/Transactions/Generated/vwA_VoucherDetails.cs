﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.ReceiptsAndPayments.Transactions.Generated
{
    [Serializable]
    public class CPKvwA_VoucherDetails
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
    public partial class CVarvwA_VoucherDetails : CPKvwA_VoucherDetails
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mVoucherID;
        internal Decimal mValue;
        internal String mDescription;
        internal Int32 mAccountID;
        internal String mAccount_Name;
        internal String mAccount_EnName;
        internal String mAccount_Number;
        internal Int32 mSubAccountID;
        internal String mSubAccount_Name;
        internal String mSubAccount_EnName;
        internal String mSubAccount_Number;
        internal Int32 mCostCenterID;
        internal String mCostCenterName;
        internal String mCostCenterNumber;
        internal Boolean mIsDocumented;
        internal Int64 mInvoiceID;
        internal String mInvoiceNo;
        internal Int32 mVoucherType;
        internal Int64 mOperationID;
        internal Int64 mHouseID;
        internal Int32 mBranchID;
        internal String mOperationCode;
        internal Int32 mCodeSerial;
        internal Int32 mTruckingOrderID;
        #endregion

        #region "Methods"
        public Int64 VoucherID
        {
            get { return mVoucherID; }
            set { mVoucherID = value; }
        }
        public Decimal Value
        {
            get { return mValue; }
            set { mValue = value; }
        }
        public String Description
        {
            get { return mDescription; }
            set { mDescription = value; }
        }
        public Int32 AccountID
        {
            get { return mAccountID; }
            set { mAccountID = value; }
        }
        public String Account_Name
        {
            get { return mAccount_Name; }
            set { mAccount_Name = value; }
        }
        public String Account_EnName
        {
            get { return mAccount_EnName; }
            set { mAccount_EnName = value; }
        }
        public String Account_Number
        {
            get { return mAccount_Number; }
            set { mAccount_Number = value; }
        }
        public Int32 SubAccountID
        {
            get { return mSubAccountID; }
            set { mSubAccountID = value; }
        }
        public String SubAccount_Name
        {
            get { return mSubAccount_Name; }
            set { mSubAccount_Name = value; }
        }
        public String SubAccount_EnName
        {
            get { return mSubAccount_EnName; }
            set { mSubAccount_EnName = value; }
        }
        public String SubAccount_Number
        {
            get { return mSubAccount_Number; }
            set { mSubAccount_Number = value; }
        }
        public Int32 CostCenterID
        {
            get { return mCostCenterID; }
            set { mCostCenterID = value; }
        }
        public String CostCenterName
        {
            get { return mCostCenterName; }
            set { mCostCenterName = value; }
        }
        public String CostCenterNumber
        {
            get { return mCostCenterNumber; }
            set { mCostCenterNumber = value; }
        }
        public Boolean IsDocumented
        {
            get { return mIsDocumented; }
            set { mIsDocumented = value; }
        }
        public Int64 InvoiceID
        {
            get { return mInvoiceID; }
            set { mInvoiceID = value; }
        }
        public String InvoiceNo
        {
            get { return mInvoiceNo; }
            set { mInvoiceNo = value; }
        }
        public Int32 VoucherType
        {
            get { return mVoucherType; }
            set { mVoucherType = value; }
        }
        public Int64 OperationID
        {
            get { return mOperationID; }
            set { mOperationID = value; }
        }
        public Int64 HouseID
        {
            get { return mHouseID; }
            set { mHouseID = value; }
        }
        public Int32 BranchID
        {
            get { return mBranchID; }
            set { mBranchID = value; }
        }
        public String OperationCode
        {
            get { return mOperationCode; }
            set { mOperationCode = value; }
        }
        public Int32 CodeSerial
        {
            get { return mCodeSerial; }
            set { mCodeSerial = value; }
        }
        public Int32 TruckingOrderID
        {
            get { return mTruckingOrderID; }
            set { mTruckingOrderID = value; }
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

    public partial class CvwA_VoucherDetails
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
        public List<CVarvwA_VoucherDetails> lstCVarvwA_VoucherDetails = new List<CVarvwA_VoucherDetails>();
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
            lstCVarvwA_VoucherDetails.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwA_VoucherDetails";
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
                        CVarvwA_VoucherDetails ObjCVarvwA_VoucherDetails = new CVarvwA_VoucherDetails();
                        ObjCVarvwA_VoucherDetails.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwA_VoucherDetails.mVoucherID = Convert.ToInt64(dr["VoucherID"].ToString());
                        ObjCVarvwA_VoucherDetails.mValue = Convert.ToDecimal(dr["Value"].ToString());
                        ObjCVarvwA_VoucherDetails.mDescription = Convert.ToString(dr["Description"].ToString());
                        ObjCVarvwA_VoucherDetails.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarvwA_VoucherDetails.mAccount_Name = Convert.ToString(dr["Account_Name"].ToString());
                        ObjCVarvwA_VoucherDetails.mAccount_EnName = Convert.ToString(dr["Account_EnName"].ToString());
                        ObjCVarvwA_VoucherDetails.mAccount_Number = Convert.ToString(dr["Account_Number"].ToString());
                        ObjCVarvwA_VoucherDetails.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarvwA_VoucherDetails.mSubAccount_Name = Convert.ToString(dr["SubAccount_Name"].ToString());
                        ObjCVarvwA_VoucherDetails.mSubAccount_EnName = Convert.ToString(dr["SubAccount_EnName"].ToString());
                        ObjCVarvwA_VoucherDetails.mSubAccount_Number = Convert.ToString(dr["SubAccount_Number"].ToString());
                        ObjCVarvwA_VoucherDetails.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarvwA_VoucherDetails.mCostCenterName = Convert.ToString(dr["CostCenterName"].ToString());
                        ObjCVarvwA_VoucherDetails.mCostCenterNumber = Convert.ToString(dr["CostCenterNumber"].ToString());
                        ObjCVarvwA_VoucherDetails.mIsDocumented = Convert.ToBoolean(dr["IsDocumented"].ToString());
                        ObjCVarvwA_VoucherDetails.mInvoiceID = Convert.ToInt64(dr["InvoiceID"].ToString());
                        ObjCVarvwA_VoucherDetails.mInvoiceNo = Convert.ToString(dr["InvoiceNo"].ToString());
                        ObjCVarvwA_VoucherDetails.mVoucherType = Convert.ToInt32(dr["VoucherType"].ToString());
                        ObjCVarvwA_VoucherDetails.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwA_VoucherDetails.mHouseID = Convert.ToInt64(dr["HouseID"].ToString());
                        ObjCVarvwA_VoucherDetails.mBranchID = Convert.ToInt32(dr["BranchID"].ToString());
                        ObjCVarvwA_VoucherDetails.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwA_VoucherDetails.mCodeSerial = Convert.ToInt32(dr["CodeSerial"].ToString());
                        ObjCVarvwA_VoucherDetails.mTruckingOrderID = Convert.ToInt32(dr["TruckingOrderID"].ToString());
                        lstCVarvwA_VoucherDetails.Add(ObjCVarvwA_VoucherDetails);
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
            lstCVarvwA_VoucherDetails.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwA_VoucherDetails";
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
                        CVarvwA_VoucherDetails ObjCVarvwA_VoucherDetails = new CVarvwA_VoucherDetails();
                        ObjCVarvwA_VoucherDetails.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwA_VoucherDetails.mVoucherID = Convert.ToInt64(dr["VoucherID"].ToString());
                        ObjCVarvwA_VoucherDetails.mValue = Convert.ToDecimal(dr["Value"].ToString());
                        ObjCVarvwA_VoucherDetails.mDescription = Convert.ToString(dr["Description"].ToString());
                        ObjCVarvwA_VoucherDetails.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarvwA_VoucherDetails.mAccount_Name = Convert.ToString(dr["Account_Name"].ToString());
                        ObjCVarvwA_VoucherDetails.mAccount_EnName = Convert.ToString(dr["Account_EnName"].ToString());
                        ObjCVarvwA_VoucherDetails.mAccount_Number = Convert.ToString(dr["Account_Number"].ToString());
                        ObjCVarvwA_VoucherDetails.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarvwA_VoucherDetails.mSubAccount_Name = Convert.ToString(dr["SubAccount_Name"].ToString());
                        ObjCVarvwA_VoucherDetails.mSubAccount_EnName = Convert.ToString(dr["SubAccount_EnName"].ToString());
                        ObjCVarvwA_VoucherDetails.mSubAccount_Number = Convert.ToString(dr["SubAccount_Number"].ToString());
                        ObjCVarvwA_VoucherDetails.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarvwA_VoucherDetails.mCostCenterName = Convert.ToString(dr["CostCenterName"].ToString());
                        ObjCVarvwA_VoucherDetails.mCostCenterNumber = Convert.ToString(dr["CostCenterNumber"].ToString());
                        ObjCVarvwA_VoucherDetails.mIsDocumented = Convert.ToBoolean(dr["IsDocumented"].ToString());
                        ObjCVarvwA_VoucherDetails.mInvoiceID = Convert.ToInt64(dr["InvoiceID"].ToString());
                        ObjCVarvwA_VoucherDetails.mInvoiceNo = Convert.ToString(dr["InvoiceNo"].ToString());
                        ObjCVarvwA_VoucherDetails.mVoucherType = Convert.ToInt32(dr["VoucherType"].ToString());
                        ObjCVarvwA_VoucherDetails.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwA_VoucherDetails.mHouseID = Convert.ToInt64(dr["HouseID"].ToString());
                        ObjCVarvwA_VoucherDetails.mBranchID = Convert.ToInt32(dr["BranchID"].ToString());
                        ObjCVarvwA_VoucherDetails.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwA_VoucherDetails.mCodeSerial = Convert.ToInt32(dr["CodeSerial"].ToString());
                        ObjCVarvwA_VoucherDetails.mTruckingOrderID = Convert.ToInt32(dr["TruckingOrderID"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwA_VoucherDetails.Add(ObjCVarvwA_VoucherDetails);
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
