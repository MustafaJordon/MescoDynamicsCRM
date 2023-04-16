using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.Warehousing.Transactions.Generated
{
    [Serializable]
    public class CPKvwWH_ReceiveDetails
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
    public partial class CVarvwWH_ReceiveDetails : CPKvwWH_ReceiveDetails
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mReceiveID;
        internal Int32 mWarehouseID;
        internal String mReceiveCode;
        internal Int32 mReceiveCodeSerial;
        internal Int32 mCustomerID;
        internal String mCustomerName;
        internal String mBarCode;
        internal Int64 mPurchaseItemID;
        internal String mPurchaseItemCode;
        internal String mPurchaseItemName;
        internal Int32 mPackageTypeID;
        internal String mPackageTypeName;
        internal String mPartNumber;
        internal Decimal mGrossWeightPerUnit;
        internal Decimal mVolumePerUnit;
        internal Decimal mQuantity;
        internal Decimal mExpectedQuantity;
        internal Decimal mSplitQuantity;
        internal Int32 mLocationID;
        internal String mLocationCode;
        internal Decimal mLocationMaxWeight;
        internal Decimal mLocationMaxVolume;
        internal String mAreaName;
        internal String mPalletID;
        internal DateTime mReceiveDate;
        internal Int32 mStatusID;
        internal String mStatusCode;
        internal String mStatusName;
        internal Boolean mIsFinalized;
        internal DateTime mFinalizeDate;
        internal Decimal mPickedQuantity;
        internal Decimal mFinalizedPickedQuantity;
        internal Boolean mIsExcluded;
        internal String mLotNo;
        internal DateTime mExpireDate;
        internal Boolean mByExpireDate;
        internal Boolean mBySerialNo;
        internal Boolean mByLotNo;
        internal Boolean mByVehicle;
        internal Int64 mOperationVehicleID;
        internal String mOperationVehicleCode;
        internal String mMotorNumber;
        internal String mChassisNumber;
        internal String mLotNumber;
        internal String mSerialNumber;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Int32 mAreaID;
        internal String mOCNCode;
        internal String mModel;
        internal String mKeyNumber;
        internal String mEC;
        internal String mPaintType;
        internal String mIC;
        internal String mCommercialInvoiceNumber;
        internal String mInsurancePolicyNumber;
        internal String mProductionOrder;
        internal String mPINumber;
        internal String mBillNumber;
        internal String mEngineNumber;
        internal Int64 mOperationID;
        internal String mOperationCode;
        internal DateTime mPreviousCutOffDate;
        internal Boolean mIsPickupAddedToInvoice;
        internal DateTime mPickupWithoutInvoiceDate;
        internal Boolean mIsUsed;
        internal String mNotes;
        internal String mBatchNumber;
        internal DateTime mExpirationDate;
        internal String mImportedBy;
        internal Decimal mWeightInTons;
        #endregion

        #region "Methods"
        public Int64 ReceiveID
        {
            get { return mReceiveID; }
            set { mReceiveID = value; }
        }
        public Int32 WarehouseID
        {
            get { return mWarehouseID; }
            set { mWarehouseID = value; }
        }
        public String ReceiveCode
        {
            get { return mReceiveCode; }
            set { mReceiveCode = value; }
        }
        public Int32 ReceiveCodeSerial
        {
            get { return mReceiveCodeSerial; }
            set { mReceiveCodeSerial = value; }
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
        public String BarCode
        {
            get { return mBarCode; }
            set { mBarCode = value; }
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
        public String PartNumber
        {
            get { return mPartNumber; }
            set { mPartNumber = value; }
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
        public Decimal Quantity
        {
            get { return mQuantity; }
            set { mQuantity = value; }
        }
        public Decimal ExpectedQuantity
        {
            get { return mExpectedQuantity; }
            set { mExpectedQuantity = value; }
        }
        public Decimal SplitQuantity
        {
            get { return mSplitQuantity; }
            set { mSplitQuantity = value; }
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
        public Decimal LocationMaxWeight
        {
            get { return mLocationMaxWeight; }
            set { mLocationMaxWeight = value; }
        }
        public Decimal LocationMaxVolume
        {
            get { return mLocationMaxVolume; }
            set { mLocationMaxVolume = value; }
        }
        public String AreaName
        {
            get { return mAreaName; }
            set { mAreaName = value; }
        }
        public String PalletID
        {
            get { return mPalletID; }
            set { mPalletID = value; }
        }
        public DateTime ReceiveDate
        {
            get { return mReceiveDate; }
            set { mReceiveDate = value; }
        }
        public Int32 StatusID
        {
            get { return mStatusID; }
            set { mStatusID = value; }
        }
        public String StatusCode
        {
            get { return mStatusCode; }
            set { mStatusCode = value; }
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
        public Decimal PickedQuantity
        {
            get { return mPickedQuantity; }
            set { mPickedQuantity = value; }
        }
        public Decimal FinalizedPickedQuantity
        {
            get { return mFinalizedPickedQuantity; }
            set { mFinalizedPickedQuantity = value; }
        }
        public Boolean IsExcluded
        {
            get { return mIsExcluded; }
            set { mIsExcluded = value; }
        }
        public String LotNo
        {
            get { return mLotNo; }
            set { mLotNo = value; }
        }
        public DateTime ExpireDate
        {
            get { return mExpireDate; }
            set { mExpireDate = value; }
        }
        public Boolean ByExpireDate
        {
            get { return mByExpireDate; }
            set { mByExpireDate = value; }
        }
        public Boolean BySerialNo
        {
            get { return mBySerialNo; }
            set { mBySerialNo = value; }
        }
        public Boolean ByLotNo
        {
            get { return mByLotNo; }
            set { mByLotNo = value; }
        }
        public Boolean ByVehicle
        {
            get { return mByVehicle; }
            set { mByVehicle = value; }
        }
        public Int64 OperationVehicleID
        {
            get { return mOperationVehicleID; }
            set { mOperationVehicleID = value; }
        }
        public String OperationVehicleCode
        {
            get { return mOperationVehicleCode; }
            set { mOperationVehicleCode = value; }
        }
        public String MotorNumber
        {
            get { return mMotorNumber; }
            set { mMotorNumber = value; }
        }
        public String ChassisNumber
        {
            get { return mChassisNumber; }
            set { mChassisNumber = value; }
        }
        public String LotNumber
        {
            get { return mLotNumber; }
            set { mLotNumber = value; }
        }
        public String SerialNumber
        {
            get { return mSerialNumber; }
            set { mSerialNumber = value; }
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
        public Int32 AreaID
        {
            get { return mAreaID; }
            set { mAreaID = value; }
        }
        public String OCNCode
        {
            get { return mOCNCode; }
            set { mOCNCode = value; }
        }
        public String Model
        {
            get { return mModel; }
            set { mModel = value; }
        }
        public String KeyNumber
        {
            get { return mKeyNumber; }
            set { mKeyNumber = value; }
        }
        public String EC
        {
            get { return mEC; }
            set { mEC = value; }
        }
        public String PaintType
        {
            get { return mPaintType; }
            set { mPaintType = value; }
        }
        public String IC
        {
            get { return mIC; }
            set { mIC = value; }
        }
        public String CommercialInvoiceNumber
        {
            get { return mCommercialInvoiceNumber; }
            set { mCommercialInvoiceNumber = value; }
        }
        public String InsurancePolicyNumber
        {
            get { return mInsurancePolicyNumber; }
            set { mInsurancePolicyNumber = value; }
        }
        public String ProductionOrder
        {
            get { return mProductionOrder; }
            set { mProductionOrder = value; }
        }
        public String PINumber
        {
            get { return mPINumber; }
            set { mPINumber = value; }
        }
        public String BillNumber
        {
            get { return mBillNumber; }
            set { mBillNumber = value; }
        }
        public String EngineNumber
        {
            get { return mEngineNumber; }
            set { mEngineNumber = value; }
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
        public DateTime PreviousCutOffDate
        {
            get { return mPreviousCutOffDate; }
            set { mPreviousCutOffDate = value; }
        }
        public Boolean IsPickupAddedToInvoice
        {
            get { return mIsPickupAddedToInvoice; }
            set { mIsPickupAddedToInvoice = value; }
        }
        public DateTime PickupWithoutInvoiceDate
        {
            get { return mPickupWithoutInvoiceDate; }
            set { mPickupWithoutInvoiceDate = value; }
        }
        public Boolean IsUsed
        {
            get { return mIsUsed; }
            set { mIsUsed = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mNotes = value; }
        }
        public String BatchNumber
        {
            get { return mBatchNumber; }
            set { mBatchNumber = value; }
        }
        public DateTime ExpirationDate
        {
            get { return mExpirationDate; }
            set { mExpirationDate = value; }
        }
        public String ImportedBy
        {
            get { return mImportedBy; }
            set { mImportedBy = value; }
        }
        public Decimal WeightInTons
        {
            get { return mWeightInTons; }
            set { mWeightInTons = value; }
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

    public partial class CvwWH_ReceiveDetails
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
        public List<CVarvwWH_ReceiveDetails> lstCVarvwWH_ReceiveDetails = new List<CVarvwWH_ReceiveDetails>();
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
            lstCVarvwWH_ReceiveDetails.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwWH_ReceiveDetails";
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
                        CVarvwWH_ReceiveDetails ObjCVarvwWH_ReceiveDetails = new CVarvwWH_ReceiveDetails();
                        ObjCVarvwWH_ReceiveDetails.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mReceiveID = Convert.ToInt64(dr["ReceiveID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mWarehouseID = Convert.ToInt32(dr["WarehouseID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mReceiveCode = Convert.ToString(dr["ReceiveCode"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mReceiveCodeSerial = Convert.ToInt32(dr["ReceiveCodeSerial"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mBarCode = Convert.ToString(dr["BarCode"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPurchaseItemID = Convert.ToInt64(dr["PurchaseItemID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPurchaseItemCode = Convert.ToString(dr["PurchaseItemCode"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPurchaseItemName = Convert.ToString(dr["PurchaseItemName"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPackageTypeID = Convert.ToInt32(dr["PackageTypeID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPackageTypeName = Convert.ToString(dr["PackageTypeName"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPartNumber = Convert.ToString(dr["PartNumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mGrossWeightPerUnit = Convert.ToDecimal(dr["GrossWeightPerUnit"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mVolumePerUnit = Convert.ToDecimal(dr["VolumePerUnit"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mQuantity = Convert.ToDecimal(dr["Quantity"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mExpectedQuantity = Convert.ToDecimal(dr["ExpectedQuantity"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mSplitQuantity = Convert.ToDecimal(dr["SplitQuantity"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mLocationID = Convert.ToInt32(dr["LocationID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mLocationCode = Convert.ToString(dr["LocationCode"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mLocationMaxWeight = Convert.ToDecimal(dr["LocationMaxWeight"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mLocationMaxVolume = Convert.ToDecimal(dr["LocationMaxVolume"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mAreaName = Convert.ToString(dr["AreaName"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPalletID = Convert.ToString(dr["PalletID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mReceiveDate = Convert.ToDateTime(dr["ReceiveDate"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mStatusID = Convert.ToInt32(dr["StatusID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mStatusCode = Convert.ToString(dr["StatusCode"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mStatusName = Convert.ToString(dr["StatusName"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mIsFinalized = Convert.ToBoolean(dr["IsFinalized"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mFinalizeDate = Convert.ToDateTime(dr["FinalizeDate"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPickedQuantity = Convert.ToDecimal(dr["PickedQuantity"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mFinalizedPickedQuantity = Convert.ToDecimal(dr["FinalizedPickedQuantity"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mIsExcluded = Convert.ToBoolean(dr["IsExcluded"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mLotNo = Convert.ToString(dr["LotNo"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mExpireDate = Convert.ToDateTime(dr["ExpireDate"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mByExpireDate = Convert.ToBoolean(dr["ByExpireDate"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mBySerialNo = Convert.ToBoolean(dr["BySerialNo"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mByLotNo = Convert.ToBoolean(dr["ByLotNo"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mByVehicle = Convert.ToBoolean(dr["ByVehicle"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mOperationVehicleID = Convert.ToInt64(dr["OperationVehicleID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mOperationVehicleCode = Convert.ToString(dr["OperationVehicleCode"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mMotorNumber = Convert.ToString(dr["MotorNumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mChassisNumber = Convert.ToString(dr["ChassisNumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mLotNumber = Convert.ToString(dr["LotNumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mSerialNumber = Convert.ToString(dr["SerialNumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mAreaID = Convert.ToInt32(dr["AreaID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mOCNCode = Convert.ToString(dr["OCNCode"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mModel = Convert.ToString(dr["Model"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mKeyNumber = Convert.ToString(dr["KeyNumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mEC = Convert.ToString(dr["EC"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPaintType = Convert.ToString(dr["PaintType"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mIC = Convert.ToString(dr["IC"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mCommercialInvoiceNumber = Convert.ToString(dr["CommercialInvoiceNumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mInsurancePolicyNumber = Convert.ToString(dr["InsurancePolicyNumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mProductionOrder = Convert.ToString(dr["ProductionOrder"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPINumber = Convert.ToString(dr["PINumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mBillNumber = Convert.ToString(dr["BillNumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mEngineNumber = Convert.ToString(dr["EngineNumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPreviousCutOffDate = Convert.ToDateTime(dr["PreviousCutOffDate"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mIsPickupAddedToInvoice = Convert.ToBoolean(dr["IsPickupAddedToInvoice"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPickupWithoutInvoiceDate = Convert.ToDateTime(dr["PickupWithoutInvoiceDate"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mIsUsed = Convert.ToBoolean(dr["IsUsed"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mBatchNumber = Convert.ToString(dr["BatchNumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mExpirationDate = Convert.ToDateTime(dr["ExpirationDate"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mImportedBy = Convert.ToString(dr["ImportedBy"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mWeightInTons = Convert.ToDecimal(dr["WeightInTons"].ToString());
                        lstCVarvwWH_ReceiveDetails.Add(ObjCVarvwWH_ReceiveDetails);
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
            lstCVarvwWH_ReceiveDetails.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwWH_ReceiveDetails";
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
                        CVarvwWH_ReceiveDetails ObjCVarvwWH_ReceiveDetails = new CVarvwWH_ReceiveDetails();
                        ObjCVarvwWH_ReceiveDetails.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mReceiveID = Convert.ToInt64(dr["ReceiveID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mWarehouseID = Convert.ToInt32(dr["WarehouseID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mReceiveCode = Convert.ToString(dr["ReceiveCode"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mReceiveCodeSerial = Convert.ToInt32(dr["ReceiveCodeSerial"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mBarCode = Convert.ToString(dr["BarCode"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPurchaseItemID = Convert.ToInt64(dr["PurchaseItemID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPurchaseItemCode = Convert.ToString(dr["PurchaseItemCode"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPurchaseItemName = Convert.ToString(dr["PurchaseItemName"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPackageTypeID = Convert.ToInt32(dr["PackageTypeID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPackageTypeName = Convert.ToString(dr["PackageTypeName"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPartNumber = Convert.ToString(dr["PartNumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mGrossWeightPerUnit = Convert.ToDecimal(dr["GrossWeightPerUnit"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mVolumePerUnit = Convert.ToDecimal(dr["VolumePerUnit"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mQuantity = Convert.ToDecimal(dr["Quantity"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mExpectedQuantity = Convert.ToDecimal(dr["ExpectedQuantity"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mSplitQuantity = Convert.ToDecimal(dr["SplitQuantity"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mLocationID = Convert.ToInt32(dr["LocationID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mLocationCode = Convert.ToString(dr["LocationCode"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mLocationMaxWeight = Convert.ToDecimal(dr["LocationMaxWeight"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mLocationMaxVolume = Convert.ToDecimal(dr["LocationMaxVolume"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mAreaName = Convert.ToString(dr["AreaName"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPalletID = Convert.ToString(dr["PalletID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mReceiveDate = Convert.ToDateTime(dr["ReceiveDate"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mStatusID = Convert.ToInt32(dr["StatusID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mStatusCode = Convert.ToString(dr["StatusCode"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mStatusName = Convert.ToString(dr["StatusName"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mIsFinalized = Convert.ToBoolean(dr["IsFinalized"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mFinalizeDate = Convert.ToDateTime(dr["FinalizeDate"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPickedQuantity = Convert.ToDecimal(dr["PickedQuantity"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mFinalizedPickedQuantity = Convert.ToDecimal(dr["FinalizedPickedQuantity"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mIsExcluded = Convert.ToBoolean(dr["IsExcluded"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mLotNo = Convert.ToString(dr["LotNo"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mExpireDate = Convert.ToDateTime(dr["ExpireDate"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mByExpireDate = Convert.ToBoolean(dr["ByExpireDate"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mBySerialNo = Convert.ToBoolean(dr["BySerialNo"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mByLotNo = Convert.ToBoolean(dr["ByLotNo"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mByVehicle = Convert.ToBoolean(dr["ByVehicle"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mOperationVehicleID = Convert.ToInt64(dr["OperationVehicleID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mOperationVehicleCode = Convert.ToString(dr["OperationVehicleCode"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mMotorNumber = Convert.ToString(dr["MotorNumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mChassisNumber = Convert.ToString(dr["ChassisNumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mLotNumber = Convert.ToString(dr["LotNumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mSerialNumber = Convert.ToString(dr["SerialNumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mAreaID = Convert.ToInt32(dr["AreaID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mOCNCode = Convert.ToString(dr["OCNCode"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mModel = Convert.ToString(dr["Model"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mKeyNumber = Convert.ToString(dr["KeyNumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mEC = Convert.ToString(dr["EC"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPaintType = Convert.ToString(dr["PaintType"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mIC = Convert.ToString(dr["IC"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mCommercialInvoiceNumber = Convert.ToString(dr["CommercialInvoiceNumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mInsurancePolicyNumber = Convert.ToString(dr["InsurancePolicyNumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mProductionOrder = Convert.ToString(dr["ProductionOrder"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPINumber = Convert.ToString(dr["PINumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mBillNumber = Convert.ToString(dr["BillNumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mEngineNumber = Convert.ToString(dr["EngineNumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPreviousCutOffDate = Convert.ToDateTime(dr["PreviousCutOffDate"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mIsPickupAddedToInvoice = Convert.ToBoolean(dr["IsPickupAddedToInvoice"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mPickupWithoutInvoiceDate = Convert.ToDateTime(dr["PickupWithoutInvoiceDate"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mIsUsed = Convert.ToBoolean(dr["IsUsed"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mBatchNumber = Convert.ToString(dr["BatchNumber"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mExpirationDate = Convert.ToDateTime(dr["ExpirationDate"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mImportedBy = Convert.ToString(dr["ImportedBy"].ToString());
                        ObjCVarvwWH_ReceiveDetails.mWeightInTons = Convert.ToDecimal(dr["WeightInTons"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwWH_ReceiveDetails.Add(ObjCVarvwWH_ReceiveDetails);
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
