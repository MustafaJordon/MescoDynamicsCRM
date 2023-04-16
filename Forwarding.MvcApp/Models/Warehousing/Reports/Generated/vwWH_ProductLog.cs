using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.Warehousing.Reports.Generated
{
    [Serializable]
    public partial class CVarvwWH_ProductLog
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mID;
        internal String mActionType;
        internal String mCode;
        internal Int32 mCustomerID;
        internal String mCustomerName;
        internal Int32 mLocationID;
        internal String mLocationCode;
        internal Boolean mIsFinalized;
        internal Int64 mPurchaseItemID;
        internal String mPurchaseItemCode;
        internal String mPurchaseItemName;
        internal Int32 mPackageTypeID;
        internal String mPackageTypeName;
        internal String mPalletID;
        internal Decimal mQuantity;
        internal DateTime mFinalizeDate;
        internal Decimal mWeight;
        internal Decimal mVolume;
        internal Int64 mOperationVehicleID;
        internal String mChassisNumber;
        internal String mEngineNumber;
        internal String mOCNCode;
        internal DateTime mCreationDate;
        internal Decimal mPayables;
        internal Decimal mReceivables;
        #endregion

        #region "Methods"
        public Int64 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public String ActionType
        {
            get { return mActionType; }
            set { mActionType = value; }
        }
        public String Code
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
        public Int32 LocationID
        {
            get { return mLocationID; }
            set { mLocationID = value; }
        }
        public String LocationCode
        {
            get { return mLocationCode; }
            set { mLocationCode = value; }
        }
        public Boolean IsFinalized
        {
            get { return mIsFinalized; }
            set { mIsFinalized = value; }
        }
        public Int64 PurchaseItemID
        {
            get { return mPurchaseItemID; }
            set { mPurchaseItemID = value; }
        }
        public String PurchaseItemCode
        {
            get { return mPurchaseItemCode; }
            set { mPurchaseItemCode = value; }
        }
        public String PurchaseItemName
        {
            get { return mPurchaseItemName; }
            set { mPurchaseItemName = value; }
        }
        public Int32 PackageTypeID
        {
            get { return mPackageTypeID; }
            set { mPackageTypeID = value; }
        }
        public String PackageTypeName
        {
            get { return mPackageTypeName; }
            set { mPackageTypeName = value; }
        }
        public String PalletID
        {
            get { return mPalletID; }
            set { mPalletID = value; }
        }
        public Decimal Quantity
        {
            get { return mQuantity; }
            set { mQuantity = value; }
        }
        public DateTime FinalizeDate
        {
            get { return mFinalizeDate; }
            set { mFinalizeDate = value; }
        }
        public Decimal Weight
        {
            get { return mWeight; }
            set { mWeight = value; }
        }
        public Decimal Volume
        {
            get { return mVolume; }
            set { mVolume = value; }
        }
        public Int64 OperationVehicleID
        {
            get { return mOperationVehicleID; }
            set { mOperationVehicleID = value; }
        }
        public String ChassisNumber
        {
            get { return mChassisNumber; }
            set { mChassisNumber = value; }
        }
        public String EngineNumber
        {
            get { return mEngineNumber; }
            set { mEngineNumber = value; }
        }
        public String OCNCode
        {
            get { return mOCNCode; }
            set { mOCNCode = value; }
        }
        public DateTime CreationDate
        {
            get { return mCreationDate; }
            set { mCreationDate = value; }
        }
        public Decimal Payables
        {
            get { return mPayables; }
            set { mPayables = value; }
        }
        public Decimal Receivables
        {
            get { return mReceivables; }
            set { mReceivables = value; }
        }
        #endregion
    }

    public partial class CvwWH_ProductLog
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
        public List<CVarvwWH_ProductLog> lstCVarvwWH_ProductLog = new List<CVarvwWH_ProductLog>();
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
            lstCVarvwWH_ProductLog.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwWH_ProductLog";
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
                        CVarvwWH_ProductLog ObjCVarvwWH_ProductLog = new CVarvwWH_ProductLog();
                        ObjCVarvwWH_ProductLog.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwWH_ProductLog.mActionType = Convert.ToString(dr["ActionType"].ToString());
                        ObjCVarvwWH_ProductLog.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwWH_ProductLog.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarvwWH_ProductLog.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwWH_ProductLog.mLocationID = Convert.ToInt32(dr["LocationID"].ToString());
                        ObjCVarvwWH_ProductLog.mLocationCode = Convert.ToString(dr["LocationCode"].ToString());
                        ObjCVarvwWH_ProductLog.mIsFinalized = Convert.ToBoolean(dr["IsFinalized"].ToString());
                        ObjCVarvwWH_ProductLog.mPurchaseItemID = Convert.ToInt64(dr["PurchaseItemID"].ToString());
                        ObjCVarvwWH_ProductLog.mPurchaseItemCode = Convert.ToString(dr["PurchaseItemCode"].ToString());
                        ObjCVarvwWH_ProductLog.mPurchaseItemName = Convert.ToString(dr["PurchaseItemName"].ToString());
                        ObjCVarvwWH_ProductLog.mPackageTypeID = Convert.ToInt32(dr["PackageTypeID"].ToString());
                        ObjCVarvwWH_ProductLog.mPackageTypeName = Convert.ToString(dr["PackageTypeName"].ToString());
                        ObjCVarvwWH_ProductLog.mPalletID = Convert.ToString(dr["PalletID"].ToString());
                        ObjCVarvwWH_ProductLog.mQuantity = Convert.ToDecimal(dr["Quantity"].ToString());
                        ObjCVarvwWH_ProductLog.mFinalizeDate = Convert.ToDateTime(dr["FinalizeDate"].ToString());
                        ObjCVarvwWH_ProductLog.mWeight = Convert.ToDecimal(dr["Weight"].ToString());
                        ObjCVarvwWH_ProductLog.mVolume = Convert.ToDecimal(dr["Volume"].ToString());
                        ObjCVarvwWH_ProductLog.mOperationVehicleID = Convert.ToInt64(dr["OperationVehicleID"].ToString());
                        ObjCVarvwWH_ProductLog.mChassisNumber = Convert.ToString(dr["ChassisNumber"].ToString());
                        ObjCVarvwWH_ProductLog.mEngineNumber = Convert.ToString(dr["EngineNumber"].ToString());
                        ObjCVarvwWH_ProductLog.mOCNCode = Convert.ToString(dr["OCNCode"].ToString());
                        ObjCVarvwWH_ProductLog.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwWH_ProductLog.mPayables = Convert.ToDecimal(dr["Payables"].ToString());
                        ObjCVarvwWH_ProductLog.mReceivables = Convert.ToDecimal(dr["Receivables"].ToString());
                        lstCVarvwWH_ProductLog.Add(ObjCVarvwWH_ProductLog);
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
            lstCVarvwWH_ProductLog.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwWH_ProductLog";
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
                        CVarvwWH_ProductLog ObjCVarvwWH_ProductLog = new CVarvwWH_ProductLog();
                        ObjCVarvwWH_ProductLog.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwWH_ProductLog.mActionType = Convert.ToString(dr["ActionType"].ToString());
                        ObjCVarvwWH_ProductLog.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwWH_ProductLog.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarvwWH_ProductLog.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwWH_ProductLog.mLocationID = Convert.ToInt32(dr["LocationID"].ToString());
                        ObjCVarvwWH_ProductLog.mLocationCode = Convert.ToString(dr["LocationCode"].ToString());
                        ObjCVarvwWH_ProductLog.mIsFinalized = Convert.ToBoolean(dr["IsFinalized"].ToString());
                        ObjCVarvwWH_ProductLog.mPurchaseItemID = Convert.ToInt64(dr["PurchaseItemID"].ToString());
                        ObjCVarvwWH_ProductLog.mPurchaseItemCode = Convert.ToString(dr["PurchaseItemCode"].ToString());
                        ObjCVarvwWH_ProductLog.mPurchaseItemName = Convert.ToString(dr["PurchaseItemName"].ToString());
                        ObjCVarvwWH_ProductLog.mPackageTypeID = Convert.ToInt32(dr["PackageTypeID"].ToString());
                        ObjCVarvwWH_ProductLog.mPackageTypeName = Convert.ToString(dr["PackageTypeName"].ToString());
                        ObjCVarvwWH_ProductLog.mPalletID = Convert.ToString(dr["PalletID"].ToString());
                        ObjCVarvwWH_ProductLog.mQuantity = Convert.ToDecimal(dr["Quantity"].ToString());
                        ObjCVarvwWH_ProductLog.mFinalizeDate = Convert.ToDateTime(dr["FinalizeDate"].ToString());
                        ObjCVarvwWH_ProductLog.mWeight = Convert.ToDecimal(dr["Weight"].ToString());
                        ObjCVarvwWH_ProductLog.mVolume = Convert.ToDecimal(dr["Volume"].ToString());
                        ObjCVarvwWH_ProductLog.mOperationVehicleID = Convert.ToInt64(dr["OperationVehicleID"].ToString());
                        ObjCVarvwWH_ProductLog.mChassisNumber = Convert.ToString(dr["ChassisNumber"].ToString());
                        ObjCVarvwWH_ProductLog.mEngineNumber = Convert.ToString(dr["EngineNumber"].ToString());
                        ObjCVarvwWH_ProductLog.mOCNCode = Convert.ToString(dr["OCNCode"].ToString());
                        ObjCVarvwWH_ProductLog.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwWH_ProductLog.mPayables = Convert.ToDecimal(dr["Payables"].ToString());
                        ObjCVarvwWH_ProductLog.mReceivables = Convert.ToDecimal(dr["Receivables"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwWH_ProductLog.Add(ObjCVarvwWH_ProductLog);
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
