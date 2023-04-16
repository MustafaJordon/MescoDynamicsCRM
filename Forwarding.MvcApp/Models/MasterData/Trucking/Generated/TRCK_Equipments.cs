using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.MasterData.Trucking.Generated
{
    [Serializable]
    public class CPKTRCK_Equipments
    {
        #region "variables"
        private Int32 mID;
        #endregion

        #region "Methods"
        public Int32 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        #endregion
    }
    [Serializable]
    public partial class CVarTRCK_Equipments : CPKTRCK_Equipments
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mCode;
        internal String mName;
        internal String mLocalName;
        internal Int32 mOriginCountryID;
        internal DateTime mManufacturDate;
        internal String mPlateNo;
        internal String mChassisNo;
        internal String mLicenseNumber;
        internal DateTime mLicenseNumberExpireDate;
        internal String mColor;
        internal Decimal mLength;
        internal Decimal mWidth;
        internal Decimal mHeight;
        internal Decimal mGrossWeight;
        internal Decimal mAllowedWeight;
        internal Int32 mNoOfPrimaryWheels;
        internal Int32 mNoOfAdditionalWheels;
        internal Int32 mAxeCount;
        internal DateTime mExaminationExpireDate;
        internal DateTime mWorkingDate;
        internal String mInsuranceBill;
        internal Int32 mInsuranceCompanyID;
        internal DateTime mTaxEndDate;
        internal Int32 mEquipmentModelID;
        internal Int32 mServiceCenterID;
        internal String mNotes;
        internal Int32 mTrailerID;
        internal String mMotorNo;
        internal Boolean mIsInactive;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Int32 mLockingUserID;
        internal DateTime mTimeLocked;
        internal Boolean mIsDeleted;
        internal Int32 mAccountID;
        internal Int32 mSubAccountID;
        internal Int32 mCostCenterID;
        internal Int32 mSubAccountGroupID;
        internal Int32 mFirstCounter;
        internal Decimal mNumberOfChambers;
        internal Decimal mChambersVolumeInLiters;
        internal Decimal mTankerEmptyWeight;
        internal Int32 mOutletID;
        internal Boolean mIsHeating;
        internal Boolean mIsHeatable;
        internal Boolean mIsEpump;
        internal Boolean mIsCompressor;
        internal Boolean mIsGroundOperated;
        internal Boolean mIsATP;
        internal Int32 mEquipmentTypeID;
        internal Int32 mNumberOfChairs;
        internal String mInsuranceValue;
        internal DateTime mInsuranceDate;
        internal String mModel;
        internal String mWheelSize;
        internal Boolean mIsGPS;
        internal Int32 mCompanyID;
        #endregion

        #region "Methods"
        public Int32 Code
        {
            get { return mCode; }
            set { mIsChanges = true; mCode = value; }
        }
        public String Name
        {
            get { return mName; }
            set { mIsChanges = true; mName = value; }
        }
        public String LocalName
        {
            get { return mLocalName; }
            set { mIsChanges = true; mLocalName = value; }
        }
        public Int32 OriginCountryID
        {
            get { return mOriginCountryID; }
            set { mIsChanges = true; mOriginCountryID = value; }
        }
        public DateTime ManufacturDate
        {
            get { return mManufacturDate; }
            set { mIsChanges = true; mManufacturDate = value; }
        }
        public String PlateNo
        {
            get { return mPlateNo; }
            set { mIsChanges = true; mPlateNo = value; }
        }
        public String ChassisNo
        {
            get { return mChassisNo; }
            set { mIsChanges = true; mChassisNo = value; }
        }
        public String LicenseNumber
        {
            get { return mLicenseNumber; }
            set { mIsChanges = true; mLicenseNumber = value; }
        }
        public DateTime LicenseNumberExpireDate
        {
            get { return mLicenseNumberExpireDate; }
            set { mIsChanges = true; mLicenseNumberExpireDate = value; }
        }
        public String Color
        {
            get { return mColor; }
            set { mIsChanges = true; mColor = value; }
        }
        public Decimal Length
        {
            get { return mLength; }
            set { mIsChanges = true; mLength = value; }
        }
        public Decimal Width
        {
            get { return mWidth; }
            set { mIsChanges = true; mWidth = value; }
        }
        public Decimal Height
        {
            get { return mHeight; }
            set { mIsChanges = true; mHeight = value; }
        }
        public Decimal GrossWeight
        {
            get { return mGrossWeight; }
            set { mIsChanges = true; mGrossWeight = value; }
        }
        public Decimal AllowedWeight
        {
            get { return mAllowedWeight; }
            set { mIsChanges = true; mAllowedWeight = value; }
        }
        public Int32 NoOfPrimaryWheels
        {
            get { return mNoOfPrimaryWheels; }
            set { mIsChanges = true; mNoOfPrimaryWheels = value; }
        }
        public Int32 NoOfAdditionalWheels
        {
            get { return mNoOfAdditionalWheels; }
            set { mIsChanges = true; mNoOfAdditionalWheels = value; }
        }
        public Int32 AxeCount
        {
            get { return mAxeCount; }
            set { mIsChanges = true; mAxeCount = value; }
        }
        public DateTime ExaminationExpireDate
        {
            get { return mExaminationExpireDate; }
            set { mIsChanges = true; mExaminationExpireDate = value; }
        }
        public DateTime WorkingDate
        {
            get { return mWorkingDate; }
            set { mIsChanges = true; mWorkingDate = value; }
        }
        public String InsuranceBill
        {
            get { return mInsuranceBill; }
            set { mIsChanges = true; mInsuranceBill = value; }
        }
        public Int32 InsuranceCompanyID
        {
            get { return mInsuranceCompanyID; }
            set { mIsChanges = true; mInsuranceCompanyID = value; }
        }
        public DateTime TaxEndDate
        {
            get { return mTaxEndDate; }
            set { mIsChanges = true; mTaxEndDate = value; }
        }
        public Int32 EquipmentModelID
        {
            get { return mEquipmentModelID; }
            set { mIsChanges = true; mEquipmentModelID = value; }
        }
        public Int32 ServiceCenterID
        {
            get { return mServiceCenterID; }
            set { mIsChanges = true; mServiceCenterID = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mIsChanges = true; mNotes = value; }
        }
        public Int32 TrailerID
        {
            get { return mTrailerID; }
            set { mIsChanges = true; mTrailerID = value; }
        }
        public String MotorNo
        {
            get { return mMotorNo; }
            set { mIsChanges = true; mMotorNo = value; }
        }
        public Boolean IsInactive
        {
            get { return mIsInactive; }
            set { mIsChanges = true; mIsInactive = value; }
        }
        public Int32 CreatorUserID
        {
            get { return mCreatorUserID; }
            set { mIsChanges = true; mCreatorUserID = value; }
        }
        public DateTime CreationDate
        {
            get { return mCreationDate; }
            set { mIsChanges = true; mCreationDate = value; }
        }
        public Int32 ModificatorUserID
        {
            get { return mModificatorUserID; }
            set { mIsChanges = true; mModificatorUserID = value; }
        }
        public DateTime ModificationDate
        {
            get { return mModificationDate; }
            set { mIsChanges = true; mModificationDate = value; }
        }
        public Int32 LockingUserID
        {
            get { return mLockingUserID; }
            set { mIsChanges = true; mLockingUserID = value; }
        }
        public DateTime TimeLocked
        {
            get { return mTimeLocked; }
            set { mIsChanges = true; mTimeLocked = value; }
        }
        public Boolean IsDeleted
        {
            get { return mIsDeleted; }
            set { mIsChanges = true; mIsDeleted = value; }
        }
        public Int32 AccountID
        {
            get { return mAccountID; }
            set { mIsChanges = true; mAccountID = value; }
        }
        public Int32 SubAccountID
        {
            get { return mSubAccountID; }
            set { mIsChanges = true; mSubAccountID = value; }
        }
        public Int32 CostCenterID
        {
            get { return mCostCenterID; }
            set { mIsChanges = true; mCostCenterID = value; }
        }
        public Int32 SubAccountGroupID
        {
            get { return mSubAccountGroupID; }
            set { mIsChanges = true; mSubAccountGroupID = value; }
        }
        public Int32 FirstCounter
        {
            get { return mFirstCounter; }
            set { mIsChanges = true; mFirstCounter = value; }
        }
        public Decimal NumberOfChambers
        {
            get { return mNumberOfChambers; }
            set { mIsChanges = true; mNumberOfChambers = value; }
        }
        public Decimal ChambersVolumeInLiters
        {
            get { return mChambersVolumeInLiters; }
            set { mIsChanges = true; mChambersVolumeInLiters = value; }
        }
        public Decimal TankerEmptyWeight
        {
            get { return mTankerEmptyWeight; }
            set { mIsChanges = true; mTankerEmptyWeight = value; }
        }
        public Int32 OutletID
        {
            get { return mOutletID; }
            set { mIsChanges = true; mOutletID = value; }
        }
        public Boolean IsHeating
        {
            get { return mIsHeating; }
            set { mIsChanges = true; mIsHeating = value; }
        }
        public Boolean IsHeatable
        {
            get { return mIsHeatable; }
            set { mIsChanges = true; mIsHeatable = value; }
        }
        public Boolean IsEpump
        {
            get { return mIsEpump; }
            set { mIsChanges = true; mIsEpump = value; }
        }
        public Boolean IsCompressor
        {
            get { return mIsCompressor; }
            set { mIsChanges = true; mIsCompressor = value; }
        }
        public Boolean IsGroundOperated
        {
            get { return mIsGroundOperated; }
            set { mIsChanges = true; mIsGroundOperated = value; }
        }
        public Boolean IsATP
        {
            get { return mIsATP; }
            set { mIsChanges = true; mIsATP = value; }
        }
        public Int32 EquipmentTypeID
        {
            get { return mEquipmentTypeID; }
            set { mIsChanges = true; mEquipmentTypeID = value; }
        }
        public Int32 NumberOfChairs
        {
            get { return mNumberOfChairs; }
            set { mIsChanges = true; mNumberOfChairs = value; }
        }
        public String InsuranceValue
        {
            get { return mInsuranceValue; }
            set { mIsChanges = true; mInsuranceValue = value; }
        }
        public DateTime InsuranceDate
        {
            get { return mInsuranceDate; }
            set { mIsChanges = true; mInsuranceDate = value; }
        }
        public String Model
        {
            get { return mModel; }
            set { mIsChanges = true; mModel = value; }
        }
        public String WheelSize
        {
            get { return mWheelSize; }
            set { mIsChanges = true; mWheelSize = value; }
        }
        public Boolean IsGPS
        {
            get { return mIsGPS; }
            set { mIsChanges = true; mIsGPS = value; }
        }
        public Int32 CompanyID
        {
            get { return mCompanyID; }
            set { mIsChanges = true; mCompanyID = value; }
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

    public partial class CTRCK_Equipments
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
        public List<CVarTRCK_Equipments> lstCVarTRCK_Equipments = new List<CVarTRCK_Equipments>();
        public List<CPKTRCK_Equipments> lstDeletedCPKTRCK_Equipments = new List<CPKTRCK_Equipments>();
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
        public Exception GetItem(Int32 ID)
        {
            return DataFill(Convert.ToString(ID), false);
        }
        private Exception DataFill(string Param, Boolean IsList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarTRCK_Equipments.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListTRCK_Equipments";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemTRCK_Equipments";
                    Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                    Com.Parameters[0].Value = Convert.ToInt32(Param);
                }
                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        /*Start DataReader*/
                        CVarTRCK_Equipments ObjCVarTRCK_Equipments = new CVarTRCK_Equipments();
                        ObjCVarTRCK_Equipments.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarTRCK_Equipments.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarTRCK_Equipments.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarTRCK_Equipments.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarTRCK_Equipments.mOriginCountryID = Convert.ToInt32(dr["OriginCountryID"].ToString());
                        ObjCVarTRCK_Equipments.mManufacturDate = Convert.ToDateTime(dr["ManufacturDate"].ToString());
                        ObjCVarTRCK_Equipments.mPlateNo = Convert.ToString(dr["PlateNo"].ToString());
                        ObjCVarTRCK_Equipments.mChassisNo = Convert.ToString(dr["ChassisNo"].ToString());
                        ObjCVarTRCK_Equipments.mLicenseNumber = Convert.ToString(dr["LicenseNumber"].ToString());
                        ObjCVarTRCK_Equipments.mLicenseNumberExpireDate = Convert.ToDateTime(dr["LicenseNumberExpireDate"].ToString());
                        ObjCVarTRCK_Equipments.mColor = Convert.ToString(dr["Color"].ToString());
                        ObjCVarTRCK_Equipments.mLength = Convert.ToDecimal(dr["Length"].ToString());
                        ObjCVarTRCK_Equipments.mWidth = Convert.ToDecimal(dr["Width"].ToString());
                        ObjCVarTRCK_Equipments.mHeight = Convert.ToDecimal(dr["Height"].ToString());
                        ObjCVarTRCK_Equipments.mGrossWeight = Convert.ToDecimal(dr["GrossWeight"].ToString());
                        ObjCVarTRCK_Equipments.mAllowedWeight = Convert.ToDecimal(dr["AllowedWeight"].ToString());
                        ObjCVarTRCK_Equipments.mNoOfPrimaryWheels = Convert.ToInt32(dr["NoOfPrimaryWheels"].ToString());
                        ObjCVarTRCK_Equipments.mNoOfAdditionalWheels = Convert.ToInt32(dr["NoOfAdditionalWheels"].ToString());
                        ObjCVarTRCK_Equipments.mAxeCount = Convert.ToInt32(dr["AxeCount"].ToString());
                        ObjCVarTRCK_Equipments.mExaminationExpireDate = Convert.ToDateTime(dr["ExaminationExpireDate"].ToString());
                        ObjCVarTRCK_Equipments.mWorkingDate = Convert.ToDateTime(dr["WorkingDate"].ToString());
                        ObjCVarTRCK_Equipments.mInsuranceBill = Convert.ToString(dr["InsuranceBill"].ToString());
                        ObjCVarTRCK_Equipments.mInsuranceCompanyID = Convert.ToInt32(dr["InsuranceCompanyID"].ToString());
                        ObjCVarTRCK_Equipments.mTaxEndDate = Convert.ToDateTime(dr["TaxEndDate"].ToString());
                        ObjCVarTRCK_Equipments.mEquipmentModelID = Convert.ToInt32(dr["EquipmentModelID"].ToString());
                        ObjCVarTRCK_Equipments.mServiceCenterID = Convert.ToInt32(dr["ServiceCenterID"].ToString());
                        ObjCVarTRCK_Equipments.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarTRCK_Equipments.mTrailerID = Convert.ToInt32(dr["TrailerID"].ToString());
                        ObjCVarTRCK_Equipments.mMotorNo = Convert.ToString(dr["MotorNo"].ToString());
                        ObjCVarTRCK_Equipments.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarTRCK_Equipments.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarTRCK_Equipments.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarTRCK_Equipments.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarTRCK_Equipments.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarTRCK_Equipments.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarTRCK_Equipments.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarTRCK_Equipments.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarTRCK_Equipments.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarTRCK_Equipments.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarTRCK_Equipments.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarTRCK_Equipments.mSubAccountGroupID = Convert.ToInt32(dr["SubAccountGroupID"].ToString());
                        ObjCVarTRCK_Equipments.mFirstCounter = Convert.ToInt32(dr["FirstCounter"].ToString());
                        ObjCVarTRCK_Equipments.mNumberOfChambers = Convert.ToDecimal(dr["NumberOfChambers"].ToString());
                        ObjCVarTRCK_Equipments.mChambersVolumeInLiters = Convert.ToDecimal(dr["ChambersVolumeInLiters"].ToString());
                        ObjCVarTRCK_Equipments.mTankerEmptyWeight = Convert.ToDecimal(dr["TankerEmptyWeight"].ToString());
                        ObjCVarTRCK_Equipments.mOutletID = Convert.ToInt32(dr["OutletID"].ToString());
                        ObjCVarTRCK_Equipments.mIsHeating = Convert.ToBoolean(dr["IsHeating"].ToString());
                        ObjCVarTRCK_Equipments.mIsHeatable = Convert.ToBoolean(dr["IsHeatable"].ToString());
                        ObjCVarTRCK_Equipments.mIsEpump = Convert.ToBoolean(dr["IsEpump"].ToString());
                        ObjCVarTRCK_Equipments.mIsCompressor = Convert.ToBoolean(dr["IsCompressor"].ToString());
                        ObjCVarTRCK_Equipments.mIsGroundOperated = Convert.ToBoolean(dr["IsGroundOperated"].ToString());
                        ObjCVarTRCK_Equipments.mIsATP = Convert.ToBoolean(dr["IsATP"].ToString());
                        ObjCVarTRCK_Equipments.mEquipmentTypeID = Convert.ToInt32(dr["EquipmentTypeID"].ToString());
                        ObjCVarTRCK_Equipments.mNumberOfChairs = Convert.ToInt32(dr["NumberOfChairs"].ToString());
                        ObjCVarTRCK_Equipments.mInsuranceValue = Convert.ToString(dr["InsuranceValue"].ToString());
                        ObjCVarTRCK_Equipments.mInsuranceDate = Convert.ToDateTime(dr["InsuranceDate"].ToString());
                        ObjCVarTRCK_Equipments.mModel = Convert.ToString(dr["Model"].ToString());
                        ObjCVarTRCK_Equipments.mWheelSize = Convert.ToString(dr["WheelSize"].ToString());
                        ObjCVarTRCK_Equipments.mIsGPS = Convert.ToBoolean(dr["IsGPS"].ToString());
                        ObjCVarTRCK_Equipments.mCompanyID = Convert.ToInt32(dr["CompanyID"].ToString());
                        lstCVarTRCK_Equipments.Add(ObjCVarTRCK_Equipments);
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
            lstCVarTRCK_Equipments.Clear();

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
                Com.CommandText = "[dbo].GetListPagingTRCK_Equipments";
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
                        CVarTRCK_Equipments ObjCVarTRCK_Equipments = new CVarTRCK_Equipments();
                        ObjCVarTRCK_Equipments.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarTRCK_Equipments.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarTRCK_Equipments.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarTRCK_Equipments.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarTRCK_Equipments.mOriginCountryID = Convert.ToInt32(dr["OriginCountryID"].ToString());
                        ObjCVarTRCK_Equipments.mManufacturDate = Convert.ToDateTime(dr["ManufacturDate"].ToString());
                        ObjCVarTRCK_Equipments.mPlateNo = Convert.ToString(dr["PlateNo"].ToString());
                        ObjCVarTRCK_Equipments.mChassisNo = Convert.ToString(dr["ChassisNo"].ToString());
                        ObjCVarTRCK_Equipments.mLicenseNumber = Convert.ToString(dr["LicenseNumber"].ToString());
                        ObjCVarTRCK_Equipments.mLicenseNumberExpireDate = Convert.ToDateTime(dr["LicenseNumberExpireDate"].ToString());
                        ObjCVarTRCK_Equipments.mColor = Convert.ToString(dr["Color"].ToString());
                        ObjCVarTRCK_Equipments.mLength = Convert.ToDecimal(dr["Length"].ToString());
                        ObjCVarTRCK_Equipments.mWidth = Convert.ToDecimal(dr["Width"].ToString());
                        ObjCVarTRCK_Equipments.mHeight = Convert.ToDecimal(dr["Height"].ToString());
                        ObjCVarTRCK_Equipments.mGrossWeight = Convert.ToDecimal(dr["GrossWeight"].ToString());
                        ObjCVarTRCK_Equipments.mAllowedWeight = Convert.ToDecimal(dr["AllowedWeight"].ToString());
                        ObjCVarTRCK_Equipments.mNoOfPrimaryWheels = Convert.ToInt32(dr["NoOfPrimaryWheels"].ToString());
                        ObjCVarTRCK_Equipments.mNoOfAdditionalWheels = Convert.ToInt32(dr["NoOfAdditionalWheels"].ToString());
                        ObjCVarTRCK_Equipments.mAxeCount = Convert.ToInt32(dr["AxeCount"].ToString());
                        ObjCVarTRCK_Equipments.mExaminationExpireDate = Convert.ToDateTime(dr["ExaminationExpireDate"].ToString());
                        ObjCVarTRCK_Equipments.mWorkingDate = Convert.ToDateTime(dr["WorkingDate"].ToString());
                        ObjCVarTRCK_Equipments.mInsuranceBill = Convert.ToString(dr["InsuranceBill"].ToString());
                        ObjCVarTRCK_Equipments.mInsuranceCompanyID = Convert.ToInt32(dr["InsuranceCompanyID"].ToString());
                        ObjCVarTRCK_Equipments.mTaxEndDate = Convert.ToDateTime(dr["TaxEndDate"].ToString());
                        ObjCVarTRCK_Equipments.mEquipmentModelID = Convert.ToInt32(dr["EquipmentModelID"].ToString());
                        ObjCVarTRCK_Equipments.mServiceCenterID = Convert.ToInt32(dr["ServiceCenterID"].ToString());
                        ObjCVarTRCK_Equipments.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarTRCK_Equipments.mTrailerID = Convert.ToInt32(dr["TrailerID"].ToString());
                        ObjCVarTRCK_Equipments.mMotorNo = Convert.ToString(dr["MotorNo"].ToString());
                        ObjCVarTRCK_Equipments.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarTRCK_Equipments.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarTRCK_Equipments.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarTRCK_Equipments.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarTRCK_Equipments.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarTRCK_Equipments.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarTRCK_Equipments.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarTRCK_Equipments.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarTRCK_Equipments.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarTRCK_Equipments.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarTRCK_Equipments.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarTRCK_Equipments.mSubAccountGroupID = Convert.ToInt32(dr["SubAccountGroupID"].ToString());
                        ObjCVarTRCK_Equipments.mFirstCounter = Convert.ToInt32(dr["FirstCounter"].ToString());
                        ObjCVarTRCK_Equipments.mNumberOfChambers = Convert.ToDecimal(dr["NumberOfChambers"].ToString());
                        ObjCVarTRCK_Equipments.mChambersVolumeInLiters = Convert.ToDecimal(dr["ChambersVolumeInLiters"].ToString());
                        ObjCVarTRCK_Equipments.mTankerEmptyWeight = Convert.ToDecimal(dr["TankerEmptyWeight"].ToString());
                        ObjCVarTRCK_Equipments.mOutletID = Convert.ToInt32(dr["OutletID"].ToString());
                        ObjCVarTRCK_Equipments.mIsHeating = Convert.ToBoolean(dr["IsHeating"].ToString());
                        ObjCVarTRCK_Equipments.mIsHeatable = Convert.ToBoolean(dr["IsHeatable"].ToString());
                        ObjCVarTRCK_Equipments.mIsEpump = Convert.ToBoolean(dr["IsEpump"].ToString());
                        ObjCVarTRCK_Equipments.mIsCompressor = Convert.ToBoolean(dr["IsCompressor"].ToString());
                        ObjCVarTRCK_Equipments.mIsGroundOperated = Convert.ToBoolean(dr["IsGroundOperated"].ToString());
                        ObjCVarTRCK_Equipments.mIsATP = Convert.ToBoolean(dr["IsATP"].ToString());
                        ObjCVarTRCK_Equipments.mEquipmentTypeID = Convert.ToInt32(dr["EquipmentTypeID"].ToString());
                        ObjCVarTRCK_Equipments.mNumberOfChairs = Convert.ToInt32(dr["NumberOfChairs"].ToString());
                        ObjCVarTRCK_Equipments.mInsuranceValue = Convert.ToString(dr["InsuranceValue"].ToString());
                        ObjCVarTRCK_Equipments.mInsuranceDate = Convert.ToDateTime(dr["InsuranceDate"].ToString());
                        ObjCVarTRCK_Equipments.mModel = Convert.ToString(dr["Model"].ToString());
                        ObjCVarTRCK_Equipments.mWheelSize = Convert.ToString(dr["WheelSize"].ToString());
                        ObjCVarTRCK_Equipments.mIsGPS = Convert.ToBoolean(dr["IsGPS"].ToString());
                        ObjCVarTRCK_Equipments.mCompanyID = Convert.ToInt32(dr["CompanyID"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarTRCK_Equipments.Add(ObjCVarTRCK_Equipments);
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
        #region "Common Methods"
        private void BeginTrans(SqlCommand Com, SqlConnection Con)
        {

            tr = Con.BeginTransaction(IsolationLevel.Serializable);
            Com.CommandType = CommandType.StoredProcedure;
        }

        private void EndTrans(SqlCommand Com, SqlConnection Con)
        {

            Com.Transaction = tr;
            Com.Connection = Con;
            Com.ExecuteNonQuery();
            tr.Commit();
        }

        #endregion
        #region "Set List Method"
        private Exception SetList(string WhereClause, Boolean IsDelete)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                if (IsDelete == true)
                    Com.CommandText = "[dbo].DeleteListTRCK_Equipments";
                else
                    Com.CommandText = "[dbo].UpdateListTRCK_Equipments";
                Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                BeginTrans(Com, Con);
                Com.Parameters[0].Value = WhereClause;
                EndTrans(Com, Con);
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
        #region "Delete Methods"
        public Exception DeleteItem(List<CPKTRCK_Equipments> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemTRCK_Equipments";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                foreach (CPKTRCK_Equipments ObjCPKTRCK_Equipments in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt32(ObjCPKTRCK_Equipments.ID);
                    EndTrans(Com, Con);
                }
            }
            catch (Exception ex)
            {
                Exp = ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
                DeleteList.Clear();
            }
            return Exp;
        }

        public Exception DeleteList(string WhereClause)
        {

            return SetList(WhereClause, true);
        }

        #endregion
        #region "Save Methods"
        public Exception SaveMethod(List<CVarTRCK_Equipments> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@Code", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@LocalName", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@OriginCountryID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ManufacturDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@PlateNo", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@ChassisNo", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@LicenseNumber", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@LicenseNumberExpireDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@Color", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Length", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@Width", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@Height", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@GrossWeight", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@AllowedWeight", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@NoOfPrimaryWheels", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@NoOfAdditionalWheels", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@AxeCount", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ExaminationExpireDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@WorkingDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@InsuranceBill", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@InsuranceCompanyID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@TaxEndDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@EquipmentModelID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ServiceCenterID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@TrailerID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@MotorNo", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@IsInactive", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@CreatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ModificatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ModificationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@LockingUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@TimeLocked", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@IsDeleted", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@AccountID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@SubAccountID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CostCenterID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@SubAccountGroupID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@FirstCounter", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@NumberOfChambers", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@ChambersVolumeInLiters", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@TankerEmptyWeight", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@OutletID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@IsHeating", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@IsHeatable", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@IsEpump", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@IsCompressor", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@IsGroundOperated", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@IsATP", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@EquipmentTypeID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@NumberOfChairs", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@InsuranceValue", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@InsuranceDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@Model", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@WheelSize", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@IsGPS", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@CompanyID", SqlDbType.Int));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarTRCK_Equipments ObjCVarTRCK_Equipments in SaveList)
                {
                    if (ObjCVarTRCK_Equipments.mIsChanges == true)
                    {
                        if (ObjCVarTRCK_Equipments.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemTRCK_Equipments";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarTRCK_Equipments.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemTRCK_Equipments";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarTRCK_Equipments.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarTRCK_Equipments.ID;
                        }
                        Com.Parameters["@Code"].Value = ObjCVarTRCK_Equipments.Code;
                        Com.Parameters["@Name"].Value = ObjCVarTRCK_Equipments.Name;
                        Com.Parameters["@LocalName"].Value = ObjCVarTRCK_Equipments.LocalName;
                        Com.Parameters["@OriginCountryID"].Value = ObjCVarTRCK_Equipments.OriginCountryID;
                        Com.Parameters["@ManufacturDate"].Value = ObjCVarTRCK_Equipments.ManufacturDate;
                        Com.Parameters["@PlateNo"].Value = ObjCVarTRCK_Equipments.PlateNo;
                        Com.Parameters["@ChassisNo"].Value = ObjCVarTRCK_Equipments.ChassisNo;
                        Com.Parameters["@LicenseNumber"].Value = ObjCVarTRCK_Equipments.LicenseNumber;
                        Com.Parameters["@LicenseNumberExpireDate"].Value = ObjCVarTRCK_Equipments.LicenseNumberExpireDate;
                        Com.Parameters["@Color"].Value = ObjCVarTRCK_Equipments.Color;
                        Com.Parameters["@Length"].Value = ObjCVarTRCK_Equipments.Length;
                        Com.Parameters["@Width"].Value = ObjCVarTRCK_Equipments.Width;
                        Com.Parameters["@Height"].Value = ObjCVarTRCK_Equipments.Height;
                        Com.Parameters["@GrossWeight"].Value = ObjCVarTRCK_Equipments.GrossWeight;
                        Com.Parameters["@AllowedWeight"].Value = ObjCVarTRCK_Equipments.AllowedWeight;
                        Com.Parameters["@NoOfPrimaryWheels"].Value = ObjCVarTRCK_Equipments.NoOfPrimaryWheels;
                        Com.Parameters["@NoOfAdditionalWheels"].Value = ObjCVarTRCK_Equipments.NoOfAdditionalWheels;
                        Com.Parameters["@AxeCount"].Value = ObjCVarTRCK_Equipments.AxeCount;
                        Com.Parameters["@ExaminationExpireDate"].Value = ObjCVarTRCK_Equipments.ExaminationExpireDate;
                        Com.Parameters["@WorkingDate"].Value = ObjCVarTRCK_Equipments.WorkingDate;
                        Com.Parameters["@InsuranceBill"].Value = ObjCVarTRCK_Equipments.InsuranceBill;
                        Com.Parameters["@InsuranceCompanyID"].Value = ObjCVarTRCK_Equipments.InsuranceCompanyID;
                        Com.Parameters["@TaxEndDate"].Value = ObjCVarTRCK_Equipments.TaxEndDate;
                        Com.Parameters["@EquipmentModelID"].Value = ObjCVarTRCK_Equipments.EquipmentModelID;
                        Com.Parameters["@ServiceCenterID"].Value = ObjCVarTRCK_Equipments.ServiceCenterID;
                        Com.Parameters["@Notes"].Value = ObjCVarTRCK_Equipments.Notes;
                        Com.Parameters["@TrailerID"].Value = ObjCVarTRCK_Equipments.TrailerID;
                        Com.Parameters["@MotorNo"].Value = ObjCVarTRCK_Equipments.MotorNo;
                        Com.Parameters["@IsInactive"].Value = ObjCVarTRCK_Equipments.IsInactive;
                        Com.Parameters["@CreatorUserID"].Value = ObjCVarTRCK_Equipments.CreatorUserID;
                        Com.Parameters["@CreationDate"].Value = ObjCVarTRCK_Equipments.CreationDate;
                        Com.Parameters["@ModificatorUserID"].Value = ObjCVarTRCK_Equipments.ModificatorUserID;
                        Com.Parameters["@ModificationDate"].Value = ObjCVarTRCK_Equipments.ModificationDate;
                        Com.Parameters["@LockingUserID"].Value = ObjCVarTRCK_Equipments.LockingUserID;
                        Com.Parameters["@TimeLocked"].Value = ObjCVarTRCK_Equipments.TimeLocked;
                        Com.Parameters["@IsDeleted"].Value = ObjCVarTRCK_Equipments.IsDeleted;
                        Com.Parameters["@AccountID"].Value = ObjCVarTRCK_Equipments.AccountID;
                        Com.Parameters["@SubAccountID"].Value = ObjCVarTRCK_Equipments.SubAccountID;
                        Com.Parameters["@CostCenterID"].Value = ObjCVarTRCK_Equipments.CostCenterID;
                        Com.Parameters["@SubAccountGroupID"].Value = ObjCVarTRCK_Equipments.SubAccountGroupID;
                        Com.Parameters["@FirstCounter"].Value = ObjCVarTRCK_Equipments.FirstCounter;
                        Com.Parameters["@NumberOfChambers"].Value = ObjCVarTRCK_Equipments.NumberOfChambers;
                        Com.Parameters["@ChambersVolumeInLiters"].Value = ObjCVarTRCK_Equipments.ChambersVolumeInLiters;
                        Com.Parameters["@TankerEmptyWeight"].Value = ObjCVarTRCK_Equipments.TankerEmptyWeight;
                        Com.Parameters["@OutletID"].Value = ObjCVarTRCK_Equipments.OutletID;
                        Com.Parameters["@IsHeating"].Value = ObjCVarTRCK_Equipments.IsHeating;
                        Com.Parameters["@IsHeatable"].Value = ObjCVarTRCK_Equipments.IsHeatable;
                        Com.Parameters["@IsEpump"].Value = ObjCVarTRCK_Equipments.IsEpump;
                        Com.Parameters["@IsCompressor"].Value = ObjCVarTRCK_Equipments.IsCompressor;
                        Com.Parameters["@IsGroundOperated"].Value = ObjCVarTRCK_Equipments.IsGroundOperated;
                        Com.Parameters["@IsATP"].Value = ObjCVarTRCK_Equipments.IsATP;
                        Com.Parameters["@EquipmentTypeID"].Value = ObjCVarTRCK_Equipments.EquipmentTypeID;
                        Com.Parameters["@NumberOfChairs"].Value = ObjCVarTRCK_Equipments.NumberOfChairs;
                        Com.Parameters["@InsuranceValue"].Value = ObjCVarTRCK_Equipments.InsuranceValue;
                        Com.Parameters["@InsuranceDate"].Value = ObjCVarTRCK_Equipments.InsuranceDate;
                        Com.Parameters["@Model"].Value = ObjCVarTRCK_Equipments.Model;
                        Com.Parameters["@WheelSize"].Value = ObjCVarTRCK_Equipments.WheelSize;
                        Com.Parameters["@IsGPS"].Value = ObjCVarTRCK_Equipments.IsGPS;
                        Com.Parameters["@CompanyID"].Value = ObjCVarTRCK_Equipments.CompanyID;
                        EndTrans(Com, Con);
                        if (ObjCVarTRCK_Equipments.ID == 0)
                        {
                            ObjCVarTRCK_Equipments.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarTRCK_Equipments.mIsChanges = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Exp = ex;
                if (tr != null)
                    tr.Rollback();
            }
            finally
            {
                Con.Close();
                Con.Dispose();
            }
            return Exp;
        }
        #endregion
        #region "Update Methods"
        public Exception UpdateList(string UpdateClause)
        {

            return SetList(UpdateClause, false);
        }

        #endregion
    }
}
