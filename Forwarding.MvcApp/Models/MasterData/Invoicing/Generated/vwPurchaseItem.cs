﻿using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.MasterData.Invoicing.Generated
{
    [Serializable]
    public partial class CVarvwPurchaseItem
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mID;
        internal String mCode;
        internal String mName;
        internal String mLocalName;
        internal Decimal mPrice;
        internal Int32 mCurrencyID;
        internal String mCurrencyCode;
        internal Int32 mAccountID;
        internal Int32 mSubAccountID;
        internal Int32 mCostCenterID;
        internal Int32 mViewOrder;
        internal String mNotes;
        internal String mPartNumber;
        internal String mHSCode;
        internal Int32 mCreationYear;
        internal Int32 mCommodityID;
        internal String mCommodityName;
        internal Int32 mPackageTypeID;
        internal String mPackageTypeName;
        internal Decimal mGrossWeight;
        internal Decimal mNetWeight;
        internal Int32 mWeightUnitID;
        internal String mWeightUnitCode;
        internal Decimal mWidth;
        internal Decimal mDepth;
        internal Decimal mHeight;
        internal Int32 mLengthUnitID;
        internal String mLengthUnitCode;
        internal Decimal mVolume;
        internal Int32 mVolumeUnitID;
        internal String mVolumeUnitCode;
        internal Boolean mIsIMO;
        internal Int32 mIMOClassID;
        internal String mIMOClassCode;
        internal Int32 mUN;
        internal Int32 mPG;
        internal Boolean mIsAddedFromExcel;
        internal Boolean mIsFlexi;
        internal Int32 mPreferredAreaID;
        internal Boolean mByExpireDate;
        internal Boolean mBySerialNo;
        internal Boolean mByLotNo;
        internal Boolean mByVehicle;
        internal Int32 mParentGroupID;
        internal Int32 mItemTypeID;
        internal Decimal mMaxQty;
        internal Decimal mMinQty;
        internal String mItemGroupName;
        internal String mItemTypeName;
        internal Boolean mIsVehicle;
        internal Int32 mEquipmentModelID;
        internal String mModelCode;
        internal String mModelName;
        internal String mMotorNumber;
        internal String mChassisNumber;
        internal Int64 mOperationID;
        internal String mOperationCode;
        internal Int32 mReturnedItemID;
        internal String mReturnedItemName;
        internal Decimal mReturnedQuantity;
        internal Decimal mExpectedAlarm;
        internal Decimal mActualAlarm;
        internal Decimal mMinimumLimit;
        internal Decimal mMaximumLimit;
        internal Decimal mReOrderlimit;
        internal String mModelNumber;
        internal String mBrandName;
        internal String mProductType;
        internal Boolean mIsFragile;
        internal Decimal mStockUnitQuantity;
        internal Decimal mItemQtyInStore;
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
        public String Name
        {
            get { return mName; }
            set { mName = value; }
        }
        public String LocalName
        {
            get { return mLocalName; }
            set { mLocalName = value; }
        }
        public Decimal Price
        {
            get { return mPrice; }
            set { mPrice = value; }
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
        public Int32 AccountID
        {
            get { return mAccountID; }
            set { mAccountID = value; }
        }
        public Int32 SubAccountID
        {
            get { return mSubAccountID; }
            set { mSubAccountID = value; }
        }
        public Int32 CostCenterID
        {
            get { return mCostCenterID; }
            set { mCostCenterID = value; }
        }
        public Int32 ViewOrder
        {
            get { return mViewOrder; }
            set { mViewOrder = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mNotes = value; }
        }
        public String PartNumber
        {
            get { return mPartNumber; }
            set { mPartNumber = value; }
        }
        public String HSCode
        {
            get { return mHSCode; }
            set { mHSCode = value; }
        }
        public Int32 CreationYear
        {
            get { return mCreationYear; }
            set { mCreationYear = value; }
        }
        public Int32 CommodityID
        {
            get { return mCommodityID; }
            set { mCommodityID = value; }
        }
        public String CommodityName
        {
            get { return mCommodityName; }
            set { mCommodityName = value; }
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
        public Decimal GrossWeight
        {
            get { return mGrossWeight; }
            set { mGrossWeight = value; }
        }
        public Decimal NetWeight
        {
            get { return mNetWeight; }
            set { mNetWeight = value; }
        }
        public Int32 WeightUnitID
        {
            get { return mWeightUnitID; }
            set { mWeightUnitID = value; }
        }
        public String WeightUnitCode
        {
            get { return mWeightUnitCode; }
            set { mWeightUnitCode = value; }
        }
        public Decimal Width
        {
            get { return mWidth; }
            set { mWidth = value; }
        }
        public Decimal Depth
        {
            get { return mDepth; }
            set { mDepth = value; }
        }
        public Decimal Height
        {
            get { return mHeight; }
            set { mHeight = value; }
        }
        public Int32 LengthUnitID
        {
            get { return mLengthUnitID; }
            set { mLengthUnitID = value; }
        }
        public String LengthUnitCode
        {
            get { return mLengthUnitCode; }
            set { mLengthUnitCode = value; }
        }
        public Decimal Volume
        {
            get { return mVolume; }
            set { mVolume = value; }
        }
        public Int32 VolumeUnitID
        {
            get { return mVolumeUnitID; }
            set { mVolumeUnitID = value; }
        }
        public String VolumeUnitCode
        {
            get { return mVolumeUnitCode; }
            set { mVolumeUnitCode = value; }
        }
        public Boolean IsIMO
        {
            get { return mIsIMO; }
            set { mIsIMO = value; }
        }
        public Int32 IMOClassID
        {
            get { return mIMOClassID; }
            set { mIMOClassID = value; }
        }
        public String IMOClassCode
        {
            get { return mIMOClassCode; }
            set { mIMOClassCode = value; }
        }
        public Int32 UN
        {
            get { return mUN; }
            set { mUN = value; }
        }
        public Int32 PG
        {
            get { return mPG; }
            set { mPG = value; }
        }
        public Boolean IsAddedFromExcel
        {
            get { return mIsAddedFromExcel; }
            set { mIsAddedFromExcel = value; }
        }
        public Boolean IsFlexi
        {
            get { return mIsFlexi; }
            set { mIsFlexi = value; }
        }
        public Int32 PreferredAreaID
        {
            get { return mPreferredAreaID; }
            set { mPreferredAreaID = value; }
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
        public Int32 ParentGroupID
        {
            get { return mParentGroupID; }
            set { mParentGroupID = value; }
        }
        public Int32 ItemTypeID
        {
            get { return mItemTypeID; }
            set { mItemTypeID = value; }
        }
        public Decimal MaxQty
        {
            get { return mMaxQty; }
            set { mMaxQty = value; }
        }
        public Decimal MinQty
        {
            get { return mMinQty; }
            set { mMinQty = value; }
        }
        public String ItemGroupName
        {
            get { return mItemGroupName; }
            set { mItemGroupName = value; }
        }
        public String ItemTypeName
        {
            get { return mItemTypeName; }
            set { mItemTypeName = value; }
        }
        public Boolean IsVehicle
        {
            get { return mIsVehicle; }
            set { mIsVehicle = value; }
        }
        public Int32 EquipmentModelID
        {
            get { return mEquipmentModelID; }
            set { mEquipmentModelID = value; }
        }
        public String ModelCode
        {
            get { return mModelCode; }
            set { mModelCode = value; }
        }
        public String ModelName
        {
            get { return mModelName; }
            set { mModelName = value; }
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
        public Int32 ReturnedItemID
        {
            get { return mReturnedItemID; }
            set { mReturnedItemID = value; }
        }
        public String ReturnedItemName
        {
            get { return mReturnedItemName; }
            set { mReturnedItemName = value; }
        }
        public Decimal ReturnedQuantity
        {
            get { return mReturnedQuantity; }
            set { mReturnedQuantity = value; }
        }
        public Decimal ExpectedAlarm
        {
            get { return mExpectedAlarm; }
            set { mExpectedAlarm = value; }
        }
        public Decimal ActualAlarm
        {
            get { return mActualAlarm; }
            set { mActualAlarm = value; }
        }
        public Decimal MinimumLimit
        {
            get { return mMinimumLimit; }
            set { mMinimumLimit = value; }
        }
        public Decimal MaximumLimit
        {
            get { return mMaximumLimit; }
            set { mMaximumLimit = value; }
        }
        public Decimal ReOrderlimit
        {
            get { return mReOrderlimit; }
            set { mReOrderlimit = value; }
        }
        public String ModelNumber
        {
            get { return mModelNumber; }
            set { mModelNumber = value; }
        }
        public String BrandName
        {
            get { return mBrandName; }
            set { mBrandName = value; }
        }
        public String ProductType
        {
            get { return mProductType; }
            set { mProductType = value; }
        }
        public Boolean IsFragile
        {
            get { return mIsFragile; }
            set { mIsFragile = value; }
        }
        public Decimal StockUnitQuantity
        {
            get { return mStockUnitQuantity; }
            set { mStockUnitQuantity = value; }
        }
        public Decimal ItemQtyInStore
        {
            get { return mItemQtyInStore; }
            set { mItemQtyInStore = value; }
        }
        #endregion
    }

    public partial class CvwPurchaseItem
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
        public List<CVarvwPurchaseItem> lstCVarvwPurchaseItem = new List<CVarvwPurchaseItem>();
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
            lstCVarvwPurchaseItem.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwPurchaseItem";
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
                        CVarvwPurchaseItem ObjCVarvwPurchaseItem = new CVarvwPurchaseItem();
                        ObjCVarvwPurchaseItem.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwPurchaseItem.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwPurchaseItem.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwPurchaseItem.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwPurchaseItem.mPrice = Convert.ToDecimal(dr["Price"].ToString());
                        ObjCVarvwPurchaseItem.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwPurchaseItem.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwPurchaseItem.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarvwPurchaseItem.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarvwPurchaseItem.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarvwPurchaseItem.mViewOrder = Convert.ToInt32(dr["ViewOrder"].ToString());
                        ObjCVarvwPurchaseItem.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwPurchaseItem.mPartNumber = Convert.ToString(dr["PartNumber"].ToString());
                        ObjCVarvwPurchaseItem.mHSCode = Convert.ToString(dr["HSCode"].ToString());
                        ObjCVarvwPurchaseItem.mCreationYear = Convert.ToInt32(dr["CreationYear"].ToString());
                        ObjCVarvwPurchaseItem.mCommodityID = Convert.ToInt32(dr["CommodityID"].ToString());
                        ObjCVarvwPurchaseItem.mCommodityName = Convert.ToString(dr["CommodityName"].ToString());
                        ObjCVarvwPurchaseItem.mPackageTypeID = Convert.ToInt32(dr["PackageTypeID"].ToString());
                        ObjCVarvwPurchaseItem.mPackageTypeName = Convert.ToString(dr["PackageTypeName"].ToString());
                        ObjCVarvwPurchaseItem.mGrossWeight = Convert.ToDecimal(dr["GrossWeight"].ToString());
                        ObjCVarvwPurchaseItem.mNetWeight = Convert.ToDecimal(dr["NetWeight"].ToString());
                        ObjCVarvwPurchaseItem.mWeightUnitID = Convert.ToInt32(dr["WeightUnitID"].ToString());
                        ObjCVarvwPurchaseItem.mWeightUnitCode = Convert.ToString(dr["WeightUnitCode"].ToString());
                        ObjCVarvwPurchaseItem.mWidth = Convert.ToDecimal(dr["Width"].ToString());
                        ObjCVarvwPurchaseItem.mDepth = Convert.ToDecimal(dr["Depth"].ToString());
                        ObjCVarvwPurchaseItem.mHeight = Convert.ToDecimal(dr["Height"].ToString());
                        ObjCVarvwPurchaseItem.mLengthUnitID = Convert.ToInt32(dr["LengthUnitID"].ToString());
                        ObjCVarvwPurchaseItem.mLengthUnitCode = Convert.ToString(dr["LengthUnitCode"].ToString());
                        ObjCVarvwPurchaseItem.mVolume = Convert.ToDecimal(dr["Volume"].ToString());
                        ObjCVarvwPurchaseItem.mVolumeUnitID = Convert.ToInt32(dr["VolumeUnitID"].ToString());
                        ObjCVarvwPurchaseItem.mVolumeUnitCode = Convert.ToString(dr["VolumeUnitCode"].ToString());
                        ObjCVarvwPurchaseItem.mIsIMO = Convert.ToBoolean(dr["IsIMO"].ToString());
                        ObjCVarvwPurchaseItem.mIMOClassID = Convert.ToInt32(dr["IMOClassID"].ToString());
                        ObjCVarvwPurchaseItem.mIMOClassCode = Convert.ToString(dr["IMOClassCode"].ToString());
                        ObjCVarvwPurchaseItem.mUN = Convert.ToInt32(dr["UN"].ToString());
                        ObjCVarvwPurchaseItem.mPG = Convert.ToInt32(dr["PG"].ToString());
                        ObjCVarvwPurchaseItem.mIsAddedFromExcel = Convert.ToBoolean(dr["IsAddedFromExcel"].ToString());
                        ObjCVarvwPurchaseItem.mIsFlexi = Convert.ToBoolean(dr["IsFlexi"].ToString());
                        ObjCVarvwPurchaseItem.mPreferredAreaID = Convert.ToInt32(dr["PreferredAreaID"].ToString());
                        ObjCVarvwPurchaseItem.mByExpireDate = Convert.ToBoolean(dr["ByExpireDate"].ToString());
                        ObjCVarvwPurchaseItem.mBySerialNo = Convert.ToBoolean(dr["BySerialNo"].ToString());
                        ObjCVarvwPurchaseItem.mByLotNo = Convert.ToBoolean(dr["ByLotNo"].ToString());
                        ObjCVarvwPurchaseItem.mByVehicle = Convert.ToBoolean(dr["ByVehicle"].ToString());
                        ObjCVarvwPurchaseItem.mParentGroupID = Convert.ToInt32(dr["ParentGroupID"].ToString());
                        ObjCVarvwPurchaseItem.mItemTypeID = Convert.ToInt32(dr["ItemTypeID"].ToString());
                        ObjCVarvwPurchaseItem.mMaxQty = Convert.ToDecimal(dr["MaxQty"].ToString());
                        ObjCVarvwPurchaseItem.mMinQty = Convert.ToDecimal(dr["MinQty"].ToString());
                        ObjCVarvwPurchaseItem.mItemGroupName = Convert.ToString(dr["ItemGroupName"].ToString());
                        ObjCVarvwPurchaseItem.mItemTypeName = Convert.ToString(dr["ItemTypeName"].ToString());
                        ObjCVarvwPurchaseItem.mIsVehicle = Convert.ToBoolean(dr["IsVehicle"].ToString());
                        ObjCVarvwPurchaseItem.mEquipmentModelID = Convert.ToInt32(dr["EquipmentModelID"].ToString());
                        ObjCVarvwPurchaseItem.mModelCode = Convert.ToString(dr["ModelCode"].ToString());
                        ObjCVarvwPurchaseItem.mModelName = Convert.ToString(dr["ModelName"].ToString());
                        ObjCVarvwPurchaseItem.mMotorNumber = Convert.ToString(dr["MotorNumber"].ToString());
                        ObjCVarvwPurchaseItem.mChassisNumber = Convert.ToString(dr["ChassisNumber"].ToString());
                        ObjCVarvwPurchaseItem.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwPurchaseItem.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwPurchaseItem.mReturnedItemID = Convert.ToInt32(dr["ReturnedItemID"].ToString());
                        ObjCVarvwPurchaseItem.mReturnedItemName = Convert.ToString(dr["ReturnedItemName"].ToString());
                        ObjCVarvwPurchaseItem.mReturnedQuantity = Convert.ToDecimal(dr["ReturnedQuantity"].ToString());
                        ObjCVarvwPurchaseItem.mExpectedAlarm = Convert.ToDecimal(dr["ExpectedAlarm"].ToString());
                        ObjCVarvwPurchaseItem.mActualAlarm = Convert.ToDecimal(dr["ActualAlarm"].ToString());
                        ObjCVarvwPurchaseItem.mMinimumLimit = Convert.ToDecimal(dr["MinimumLimit"].ToString());
                        ObjCVarvwPurchaseItem.mMaximumLimit = Convert.ToDecimal(dr["MaximumLimit"].ToString());
                        ObjCVarvwPurchaseItem.mReOrderlimit = Convert.ToDecimal(dr["ReOrderlimit"].ToString());
                        ObjCVarvwPurchaseItem.mModelNumber = Convert.ToString(dr["ModelNumber"].ToString());
                        ObjCVarvwPurchaseItem.mBrandName = Convert.ToString(dr["BrandName"].ToString());
                        ObjCVarvwPurchaseItem.mProductType = Convert.ToString(dr["ProductType"].ToString());
                        ObjCVarvwPurchaseItem.mIsFragile = Convert.ToBoolean(dr["IsFragile"].ToString());
                        ObjCVarvwPurchaseItem.mStockUnitQuantity = Convert.ToDecimal(dr["StockUnitQuantity"].ToString());
                        ObjCVarvwPurchaseItem.mItemQtyInStore = Convert.ToDecimal(dr["ItemQtyInStore"].ToString());
                        lstCVarvwPurchaseItem.Add(ObjCVarvwPurchaseItem);
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
            lstCVarvwPurchaseItem.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwPurchaseItem";
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
                        CVarvwPurchaseItem ObjCVarvwPurchaseItem = new CVarvwPurchaseItem();
                        ObjCVarvwPurchaseItem.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwPurchaseItem.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwPurchaseItem.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwPurchaseItem.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwPurchaseItem.mPrice = Convert.ToDecimal(dr["Price"].ToString());
                        ObjCVarvwPurchaseItem.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwPurchaseItem.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwPurchaseItem.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarvwPurchaseItem.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarvwPurchaseItem.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarvwPurchaseItem.mViewOrder = Convert.ToInt32(dr["ViewOrder"].ToString());
                        ObjCVarvwPurchaseItem.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwPurchaseItem.mPartNumber = Convert.ToString(dr["PartNumber"].ToString());
                        ObjCVarvwPurchaseItem.mHSCode = Convert.ToString(dr["HSCode"].ToString());
                        ObjCVarvwPurchaseItem.mCreationYear = Convert.ToInt32(dr["CreationYear"].ToString());
                        ObjCVarvwPurchaseItem.mCommodityID = Convert.ToInt32(dr["CommodityID"].ToString());
                        ObjCVarvwPurchaseItem.mCommodityName = Convert.ToString(dr["CommodityName"].ToString());
                        ObjCVarvwPurchaseItem.mPackageTypeID = Convert.ToInt32(dr["PackageTypeID"].ToString());
                        ObjCVarvwPurchaseItem.mPackageTypeName = Convert.ToString(dr["PackageTypeName"].ToString());
                        ObjCVarvwPurchaseItem.mGrossWeight = Convert.ToDecimal(dr["GrossWeight"].ToString());
                        ObjCVarvwPurchaseItem.mNetWeight = Convert.ToDecimal(dr["NetWeight"].ToString());
                        ObjCVarvwPurchaseItem.mWeightUnitID = Convert.ToInt32(dr["WeightUnitID"].ToString());
                        ObjCVarvwPurchaseItem.mWeightUnitCode = Convert.ToString(dr["WeightUnitCode"].ToString());
                        ObjCVarvwPurchaseItem.mWidth = Convert.ToDecimal(dr["Width"].ToString());
                        ObjCVarvwPurchaseItem.mDepth = Convert.ToDecimal(dr["Depth"].ToString());
                        ObjCVarvwPurchaseItem.mHeight = Convert.ToDecimal(dr["Height"].ToString());
                        ObjCVarvwPurchaseItem.mLengthUnitID = Convert.ToInt32(dr["LengthUnitID"].ToString());
                        ObjCVarvwPurchaseItem.mLengthUnitCode = Convert.ToString(dr["LengthUnitCode"].ToString());
                        ObjCVarvwPurchaseItem.mVolume = Convert.ToDecimal(dr["Volume"].ToString());
                        ObjCVarvwPurchaseItem.mVolumeUnitID = Convert.ToInt32(dr["VolumeUnitID"].ToString());
                        ObjCVarvwPurchaseItem.mVolumeUnitCode = Convert.ToString(dr["VolumeUnitCode"].ToString());
                        ObjCVarvwPurchaseItem.mIsIMO = Convert.ToBoolean(dr["IsIMO"].ToString());
                        ObjCVarvwPurchaseItem.mIMOClassID = Convert.ToInt32(dr["IMOClassID"].ToString());
                        ObjCVarvwPurchaseItem.mIMOClassCode = Convert.ToString(dr["IMOClassCode"].ToString());
                        ObjCVarvwPurchaseItem.mUN = Convert.ToInt32(dr["UN"].ToString());
                        ObjCVarvwPurchaseItem.mPG = Convert.ToInt32(dr["PG"].ToString());
                        ObjCVarvwPurchaseItem.mIsAddedFromExcel = Convert.ToBoolean(dr["IsAddedFromExcel"].ToString());
                        ObjCVarvwPurchaseItem.mIsFlexi = Convert.ToBoolean(dr["IsFlexi"].ToString());
                        ObjCVarvwPurchaseItem.mPreferredAreaID = Convert.ToInt32(dr["PreferredAreaID"].ToString());
                        ObjCVarvwPurchaseItem.mByExpireDate = Convert.ToBoolean(dr["ByExpireDate"].ToString());
                        ObjCVarvwPurchaseItem.mBySerialNo = Convert.ToBoolean(dr["BySerialNo"].ToString());
                        ObjCVarvwPurchaseItem.mByLotNo = Convert.ToBoolean(dr["ByLotNo"].ToString());
                        ObjCVarvwPurchaseItem.mByVehicle = Convert.ToBoolean(dr["ByVehicle"].ToString());
                        ObjCVarvwPurchaseItem.mParentGroupID = Convert.ToInt32(dr["ParentGroupID"].ToString());
                        ObjCVarvwPurchaseItem.mItemTypeID = Convert.ToInt32(dr["ItemTypeID"].ToString());
                        ObjCVarvwPurchaseItem.mMaxQty = Convert.ToDecimal(dr["MaxQty"].ToString());
                        ObjCVarvwPurchaseItem.mMinQty = Convert.ToDecimal(dr["MinQty"].ToString());
                        ObjCVarvwPurchaseItem.mItemGroupName = Convert.ToString(dr["ItemGroupName"].ToString());
                        ObjCVarvwPurchaseItem.mItemTypeName = Convert.ToString(dr["ItemTypeName"].ToString());
                        ObjCVarvwPurchaseItem.mIsVehicle = Convert.ToBoolean(dr["IsVehicle"].ToString());
                        ObjCVarvwPurchaseItem.mEquipmentModelID = Convert.ToInt32(dr["EquipmentModelID"].ToString());
                        ObjCVarvwPurchaseItem.mModelCode = Convert.ToString(dr["ModelCode"].ToString());
                        ObjCVarvwPurchaseItem.mModelName = Convert.ToString(dr["ModelName"].ToString());
                        ObjCVarvwPurchaseItem.mMotorNumber = Convert.ToString(dr["MotorNumber"].ToString());
                        ObjCVarvwPurchaseItem.mChassisNumber = Convert.ToString(dr["ChassisNumber"].ToString());
                        ObjCVarvwPurchaseItem.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwPurchaseItem.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwPurchaseItem.mReturnedItemID = Convert.ToInt32(dr["ReturnedItemID"].ToString());
                        ObjCVarvwPurchaseItem.mReturnedItemName = Convert.ToString(dr["ReturnedItemName"].ToString());
                        ObjCVarvwPurchaseItem.mReturnedQuantity = Convert.ToDecimal(dr["ReturnedQuantity"].ToString());
                        ObjCVarvwPurchaseItem.mExpectedAlarm = Convert.ToDecimal(dr["ExpectedAlarm"].ToString());
                        ObjCVarvwPurchaseItem.mActualAlarm = Convert.ToDecimal(dr["ActualAlarm"].ToString());
                        ObjCVarvwPurchaseItem.mMinimumLimit = Convert.ToDecimal(dr["MinimumLimit"].ToString());
                        ObjCVarvwPurchaseItem.mMaximumLimit = Convert.ToDecimal(dr["MaximumLimit"].ToString());
                        ObjCVarvwPurchaseItem.mReOrderlimit = Convert.ToDecimal(dr["ReOrderlimit"].ToString());
                        ObjCVarvwPurchaseItem.mModelNumber = Convert.ToString(dr["ModelNumber"].ToString());
                        ObjCVarvwPurchaseItem.mBrandName = Convert.ToString(dr["BrandName"].ToString());
                        ObjCVarvwPurchaseItem.mProductType = Convert.ToString(dr["ProductType"].ToString());
                        ObjCVarvwPurchaseItem.mIsFragile = Convert.ToBoolean(dr["IsFragile"].ToString());
                        ObjCVarvwPurchaseItem.mStockUnitQuantity = Convert.ToDecimal(dr["StockUnitQuantity"].ToString());
                        ObjCVarvwPurchaseItem.mItemQtyInStore = Convert.ToDecimal(dr["ItemQtyInStore"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwPurchaseItem.Add(ObjCVarvwPurchaseItem);
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
