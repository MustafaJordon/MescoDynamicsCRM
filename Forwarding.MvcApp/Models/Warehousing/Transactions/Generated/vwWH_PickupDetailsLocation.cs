using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.Warehousing.Transactions.Generated
{
    [Serializable]
    public partial class CVarvwWH_PickupDetailsLocation
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mID;
        internal Int64 mPickupDetailsID;
        internal Int64 mPickupID;
        internal String mCode;
        internal Boolean mIsFinalized;
        internal DateTime mRequiredDate;
        internal DateTime mFinalizeDate;
        internal Int32 mCustomerID;
        internal String mCustomerName;
        internal Int64 mPurchaseItemID;
        internal String mPurchaseItemCode;
        internal String mPurchaseItemName;
        internal Int32 mPackageTypeID;
        internal String mPartNumber;
        internal String mPackageTypeName;
        internal Decimal mGrossWeightPerUnit;
        internal Decimal mVolumePerUnit;
        internal Int64 mReceiveDetailsID;
        internal String mReceiveCode;
        internal Int32 mLocationID;
        internal String mLocationCode;
        internal String mBarCode;
        internal String mPalletID;
        internal String mLotNo;
        internal Decimal mPickedQuantity;
        internal String mNotes;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Int64 mVehicleActionID;
        internal Int64 mOperationVehicleID;
        internal String mChassisNumber;
        internal String mEngineNumber;
        internal String mOCNCode;
        #endregion

        #region "Methods"
        public Int64 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public Int64 PickupDetailsID
        {
            get { return mPickupDetailsID; }
            set { mPickupDetailsID = value; }
        }
        public Int64 PickupID
        {
            get { return mPickupID; }
            set { mPickupID = value; }
        }
        public String Code
        {
            get { return mCode; }
            set { mCode = value; }
        }
        public Boolean IsFinalized
        {
            get { return mIsFinalized; }
            set { mIsFinalized = value; }
        }
        public DateTime RequiredDate
        {
            get { return mRequiredDate; }
            set { mRequiredDate = value; }
        }
        public DateTime FinalizeDate
        {
            get { return mFinalizeDate; }
            set { mFinalizeDate = value; }
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
        public String PartNumber
        {
            get { return mPartNumber; }
            set { mPartNumber = value; }
        }
        public String PackageTypeName
        {
            get { return mPackageTypeName; }
            set { mPackageTypeName = value; }
        }
        public Decimal GrossWeightPerUnit
        {
            get { return mGrossWeightPerUnit; }
            set { mGrossWeightPerUnit = value; }
        }
        public Decimal VolumePerUnit
        {
            get { return mVolumePerUnit; }
            set { mVolumePerUnit = value; }
        }
        public Int64 ReceiveDetailsID
        {
            get { return mReceiveDetailsID; }
            set { mReceiveDetailsID = value; }
        }
        public String ReceiveCode
        {
            get { return mReceiveCode; }
            set { mReceiveCode = value; }
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
        public String BarCode
        {
            get { return mBarCode; }
            set { mBarCode = value; }
        }
        public String PalletID
        {
            get { return mPalletID; }
            set { mPalletID = value; }
        }
        public String LotNo
        {
            get { return mLotNo; }
            set { mLotNo = value; }
        }
        public Decimal PickedQuantity
        {
            get { return mPickedQuantity; }
            set { mPickedQuantity = value; }
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
        public Int64 VehicleActionID
        {
            get { return mVehicleActionID; }
            set { mVehicleActionID = value; }
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
        #endregion
    }

    public partial class CvwWH_PickupDetailsLocation
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
        public List<CVarvwWH_PickupDetailsLocation> lstCVarvwWH_PickupDetailsLocation = new List<CVarvwWH_PickupDetailsLocation>();
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
            lstCVarvwWH_PickupDetailsLocation.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwWH_PickupDetailsLocation";
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
                        CVarvwWH_PickupDetailsLocation ObjCVarvwWH_PickupDetailsLocation = new CVarvwWH_PickupDetailsLocation();
                        ObjCVarvwWH_PickupDetailsLocation.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mPickupDetailsID = Convert.ToInt64(dr["PickupDetailsID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mPickupID = Convert.ToInt64(dr["PickupID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mIsFinalized = Convert.ToBoolean(dr["IsFinalized"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mRequiredDate = Convert.ToDateTime(dr["RequiredDate"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mFinalizeDate = Convert.ToDateTime(dr["FinalizeDate"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mPurchaseItemID = Convert.ToInt64(dr["PurchaseItemID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mPurchaseItemCode = Convert.ToString(dr["PurchaseItemCode"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mPurchaseItemName = Convert.ToString(dr["PurchaseItemName"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mPackageTypeID = Convert.ToInt32(dr["PackageTypeID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mPartNumber = Convert.ToString(dr["PartNumber"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mPackageTypeName = Convert.ToString(dr["PackageTypeName"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mGrossWeightPerUnit = Convert.ToDecimal(dr["GrossWeightPerUnit"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mVolumePerUnit = Convert.ToDecimal(dr["VolumePerUnit"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mReceiveDetailsID = Convert.ToInt64(dr["ReceiveDetailsID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mReceiveCode = Convert.ToString(dr["ReceiveCode"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mLocationID = Convert.ToInt32(dr["LocationID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mLocationCode = Convert.ToString(dr["LocationCode"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mBarCode = Convert.ToString(dr["BarCode"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mPalletID = Convert.ToString(dr["PalletID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mLotNo = Convert.ToString(dr["LotNo"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mPickedQuantity = Convert.ToDecimal(dr["PickedQuantity"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mVehicleActionID = Convert.ToInt64(dr["VehicleActionID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mOperationVehicleID = Convert.ToInt64(dr["OperationVehicleID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mChassisNumber = Convert.ToString(dr["ChassisNumber"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mEngineNumber = Convert.ToString(dr["EngineNumber"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mOCNCode = Convert.ToString(dr["OCNCode"].ToString());
                        lstCVarvwWH_PickupDetailsLocation.Add(ObjCVarvwWH_PickupDetailsLocation);
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
            lstCVarvwWH_PickupDetailsLocation.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwWH_PickupDetailsLocation";
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
                        CVarvwWH_PickupDetailsLocation ObjCVarvwWH_PickupDetailsLocation = new CVarvwWH_PickupDetailsLocation();
                        ObjCVarvwWH_PickupDetailsLocation.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mPickupDetailsID = Convert.ToInt64(dr["PickupDetailsID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mPickupID = Convert.ToInt64(dr["PickupID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mIsFinalized = Convert.ToBoolean(dr["IsFinalized"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mRequiredDate = Convert.ToDateTime(dr["RequiredDate"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mFinalizeDate = Convert.ToDateTime(dr["FinalizeDate"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mPurchaseItemID = Convert.ToInt64(dr["PurchaseItemID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mPurchaseItemCode = Convert.ToString(dr["PurchaseItemCode"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mPurchaseItemName = Convert.ToString(dr["PurchaseItemName"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mPackageTypeID = Convert.ToInt32(dr["PackageTypeID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mPartNumber = Convert.ToString(dr["PartNumber"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mPackageTypeName = Convert.ToString(dr["PackageTypeName"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mGrossWeightPerUnit = Convert.ToDecimal(dr["GrossWeightPerUnit"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mVolumePerUnit = Convert.ToDecimal(dr["VolumePerUnit"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mReceiveDetailsID = Convert.ToInt64(dr["ReceiveDetailsID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mReceiveCode = Convert.ToString(dr["ReceiveCode"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mLocationID = Convert.ToInt32(dr["LocationID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mLocationCode = Convert.ToString(dr["LocationCode"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mBarCode = Convert.ToString(dr["BarCode"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mPalletID = Convert.ToString(dr["PalletID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mLotNo = Convert.ToString(dr["LotNo"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mPickedQuantity = Convert.ToDecimal(dr["PickedQuantity"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mVehicleActionID = Convert.ToInt64(dr["VehicleActionID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mOperationVehicleID = Convert.ToInt64(dr["OperationVehicleID"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mChassisNumber = Convert.ToString(dr["ChassisNumber"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mEngineNumber = Convert.ToString(dr["EngineNumber"].ToString());
                        ObjCVarvwWH_PickupDetailsLocation.mOCNCode = Convert.ToString(dr["OCNCode"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwWH_PickupDetailsLocation.Add(ObjCVarvwWH_PickupDetailsLocation);
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
