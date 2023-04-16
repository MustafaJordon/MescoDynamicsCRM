using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.ContainerFreightStation.Transactions
{
    [Serializable]
    public class CPKvwWH_CFS_GateInInventory
    {
        #region "variables"
        #endregion

        #region "Methods"
        #endregion
    }
    [Serializable]
    public partial class CVarvwWH_CFS_GateInInventory : CPKvwWH_CFS_GateInInventory
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mOperationNumber;
        internal String mMasterBL;
        internal String mContainerNumber;
        internal String mContainerType;
        internal String mHouseNumber;
        internal String mConsignee;
        internal String mBookingParty;
        internal String mStorageLocation;
        internal DateTime mEntryDate;
        internal DateTime mStorageEndDate;
        internal String mRoadNumber;
        internal String mDescriptionOfGoods;
        internal Decimal mGrossWeight;
        internal Decimal mNetWeight;
        internal Decimal mVolume;
        internal String mPackages;
        internal String mOtherRemarks;
        internal String mAddedByUser;
        internal DateTime mAddedAt;
        internal String mUpdatedByUser;
        internal DateTime mUpdatedAt;
        internal Int32 mKalmarOnCount;
        internal Int32 mKalmarOffCount;
        internal Int64 mOperationID;
        internal Int64 mContainerID;
        internal Int64 mHouseBillID;
        internal Int32 mConsigneeID;
        internal Int32 mBookingPartyID;
        internal Int64 mInventoryID;
        internal Int32 mWarehouseID;
        internal Int32 mAreaID;
        internal Int32 mRowID;
        internal Int32 mRowLocationID;
        internal Int64 mEmptyContainerID;
        internal Int32 mWarehouseNoteID;
        internal Int64 mOperationPartnerID;
        internal Boolean mHasDamage;
        internal String mDamageDescription;
        internal String mCustomsSealNumber;
        internal String mCustomsCertificateNumber;
        internal Decimal mCustomsFeesAmount;
        internal Decimal mCustomsFeesVAT;
        internal Decimal mCustomsFeesTotal;
        #endregion

        #region "Methods"
        public String OperationNumber
        {
            get { return mOperationNumber; }
            set { mOperationNumber = value; }
        }
        public String MasterBL
        {
            get { return mMasterBL; }
            set { mMasterBL = value; }
        }
        public String ContainerNumber
        {
            get { return mContainerNumber; }
            set { mContainerNumber = value; }
        }
        public String ContainerType
        {
            get { return mContainerType; }
            set { mContainerType = value; }
        }
        public String HouseNumber
        {
            get { return mHouseNumber; }
            set { mHouseNumber = value; }
        }
        public String Consignee
        {
            get { return mConsignee; }
            set { mConsignee = value; }
        }
        public String BookingParty
        {
            get { return mBookingParty; }
            set { mBookingParty = value; }
        }
        public String StorageLocation
        {
            get { return mStorageLocation; }
            set { mStorageLocation = value; }
        }
        public DateTime EntryDate
        {
            get { return mEntryDate; }
            set { mEntryDate = value; }
        }
        public DateTime StorageEndDate
        {
            get { return mStorageEndDate; }
            set { mStorageEndDate = value; }
        }
        public String RoadNumber
        {
            get { return mRoadNumber; }
            set { mRoadNumber = value; }
        }
        public String DescriptionOfGoods
        {
            get { return mDescriptionOfGoods; }
            set { mDescriptionOfGoods = value; }
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
        public Decimal Volume
        {
            get { return mVolume; }
            set { mVolume = value; }
        }
        public String Packages
        {
            get { return mPackages; }
            set { mPackages = value; }
        }
        public String OtherRemarks
        {
            get { return mOtherRemarks; }
            set { mOtherRemarks = value; }
        }
        public String AddedByUser
        {
            get { return mAddedByUser; }
            set { mAddedByUser = value; }
        }
        public DateTime AddedAt
        {
            get { return mAddedAt; }
            set { mAddedAt = value; }
        }
        public String UpdatedByUser
        {
            get { return mUpdatedByUser; }
            set { mUpdatedByUser = value; }
        }
        public DateTime UpdatedAt
        {
            get { return mUpdatedAt; }
            set { mUpdatedAt = value; }
        }
        public Int32 KalmarOnCount
        {
            get { return mKalmarOnCount; }
            set { mKalmarOnCount = value; }
        }
        public Int32 KalmarOffCount
        {
            get { return mKalmarOffCount; }
            set { mKalmarOffCount = value; }
        }
        public Int64 OperationID
        {
            get { return mOperationID; }
            set { mOperationID = value; }
        }
        public Int64 ContainerID
        {
            get { return mContainerID; }
            set { mContainerID = value; }
        }
        public Int64 HouseBillID
        {
            get { return mHouseBillID; }
            set { mHouseBillID = value; }
        }
        public Int32 ConsigneeID
        {
            get { return mConsigneeID; }
            set { mConsigneeID = value; }
        }
        public Int32 BookingPartyID
        {
            get { return mBookingPartyID; }
            set { mBookingPartyID = value; }
        }
        public Int64 InventoryID
        {
            get { return mInventoryID; }
            set { mInventoryID = value; }
        }
        public Int32 WarehouseID
        {
            get { return mWarehouseID; }
            set { mWarehouseID = value; }
        }
        public Int32 AreaID
        {
            get { return mAreaID; }
            set { mAreaID = value; }
        }
        public Int32 RowID
        {
            get { return mRowID; }
            set { mRowID = value; }
        }
        public Int32 RowLocationID
        {
            get { return mRowLocationID; }
            set { mRowLocationID = value; }
        }
        public Int64 EmptyContainerID
        {
            get { return mEmptyContainerID; }
            set { mEmptyContainerID = value; }
        }
        public Int32 WarehouseNoteID
        {
            get { return mWarehouseNoteID; }
            set { mWarehouseNoteID = value; }
        }
        public Int64 OperationPartnerID
        {
            get { return mOperationPartnerID; }
            set { mOperationPartnerID = value; }
        }
        public Boolean HasDamage
        {
            get { return mHasDamage; }
            set { mIsChanges = true; mHasDamage = value; }
        }
        public String DamageDescription
        {
            get { return mDamageDescription; }
            set { mIsChanges = true; mDamageDescription = value; }
        }
        public String CustomsSealNumber
        {
            get { return mCustomsSealNumber; }
            set { mCustomsSealNumber = value; }
        }
        public String CustomsCertificateNumber
        {
            get { return mCustomsCertificateNumber; }
            set { mCustomsCertificateNumber = value; }
        }
        public Decimal CustomsFeesAmount
        {
            get { return mCustomsFeesAmount; }
            set { mCustomsFeesAmount = value; }
        }
        public Decimal CustomsFeesVAT
        {
            get { return mCustomsFeesAmount; }
            set { mCustomsFeesVAT = value; }
        }
        public Decimal CustomsFeesTotal
        {
            get { return mCustomsFeesTotal; }
            set { mCustomsFeesTotal = value; }
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

    public partial class CvwWH_CFS_GateInInventory
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
        public List<CVarvwWH_CFS_GateInInventory> lstCVarvwWH_CFS_GateInInventory = new List<CVarvwWH_CFS_GateInInventory>();
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
            lstCVarvwWH_CFS_GateInInventory.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwWH_CFS_GateInInventory";
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
                        CVarvwWH_CFS_GateInInventory ObjCVarvwWH_CFS_GateInInventory = new CVarvwWH_CFS_GateInInventory();
                        ObjCVarvwWH_CFS_GateInInventory.mOperationNumber = Convert.ToString(dr["OperationNumber"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mMasterBL = Convert.ToString(dr["MasterBL"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mContainerNumber = Convert.ToString(dr["ContainerNumber"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mContainerType = Convert.ToString(dr["ContainerType"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mHouseNumber = Convert.ToString(dr["HouseNumber"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mConsignee = Convert.ToString(dr["Consignee"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mBookingParty = Convert.ToString(dr["BookingParty"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mStorageLocation = Convert.ToString(dr["StorageLocation"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mEntryDate = Convert.ToDateTime(dr["EntryDate"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mStorageEndDate = Convert.ToDateTime(dr["StorageEndDate"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mRoadNumber = Convert.ToString(dr["RoadNumber"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mDescriptionOfGoods = Convert.ToString(dr["DescriptionOfGoods"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mGrossWeight = Convert.ToDecimal(dr["GrossWeight"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mNetWeight = Convert.ToDecimal(dr["NetWeight"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mVolume = Convert.ToDecimal(dr["Volume"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mPackages = Convert.ToString(dr["Packages"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mOtherRemarks = Convert.ToString(dr["OtherRemarks"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mAddedByUser = Convert.ToString(dr["AddedByUser"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mAddedAt = Convert.ToDateTime(dr["AddedAt"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mUpdatedByUser = Convert.ToString(dr["UpdatedByUser"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mUpdatedAt = Convert.ToDateTime(dr["UpdatedAt"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mKalmarOnCount = Convert.ToInt32(dr["KalmarOnCount"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mKalmarOffCount = Convert.ToInt32(dr["KalmarOffCount"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mContainerID = Convert.ToInt64(dr["ContainerID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mHouseBillID = Convert.ToInt64(dr["HouseBillID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mConsigneeID = Convert.ToInt32(dr["ConsigneeID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mBookingPartyID = Convert.ToInt32(dr["BookingPartyID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mInventoryID = Convert.ToInt64(dr["InventoryID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mWarehouseID = Convert.ToInt32(dr["WarehouseID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mAreaID = Convert.ToInt32(dr["AreaID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mRowID = Convert.ToInt32(dr["RowID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mRowLocationID = Convert.ToInt32(dr["RowLocationID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mEmptyContainerID = Convert.ToInt64(dr["EmptyContainerID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mWarehouseNoteID = Convert.ToInt32(dr["WarehouseNoteID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mOperationPartnerID = Convert.ToInt64(dr["OperationPartnerID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mHasDamage = Convert.ToBoolean(dr["HasDamage"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mDamageDescription = Convert.ToString(dr["DamageDescription"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mCustomsSealNumber = Convert.ToString(dr["CustomsSealNumber"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mCustomsCertificateNumber = Convert.ToString(dr["CustomsCertificateNumber"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mCustomsFeesAmount = Convert.ToDecimal(dr["CustomsFeesAmount"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mCustomsFeesVAT = Convert.ToDecimal(dr["CustomsFeesVAT"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mCustomsFeesTotal = Convert.ToDecimal(dr["CustomsFeesTotal"].ToString());

                        lstCVarvwWH_CFS_GateInInventory.Add(ObjCVarvwWH_CFS_GateInInventory);
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
            lstCVarvwWH_CFS_GateInInventory.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwWH_CFS_GateInInventory";
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
                        CVarvwWH_CFS_GateInInventory ObjCVarvwWH_CFS_GateInInventory = new CVarvwWH_CFS_GateInInventory();
                        ObjCVarvwWH_CFS_GateInInventory.mOperationNumber = Convert.ToString(dr["OperationNumber"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mMasterBL = Convert.ToString(dr["MasterBL"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mContainerNumber = Convert.ToString(dr["ContainerNumber"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mContainerType = Convert.ToString(dr["ContainerType"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mHouseNumber = Convert.ToString(dr["HouseNumber"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mConsignee = Convert.ToString(dr["Consignee"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mBookingParty = Convert.ToString(dr["BookingParty"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mStorageLocation = Convert.ToString(dr["StorageLocation"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mEntryDate = Convert.ToDateTime(dr["EntryDate"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mStorageEndDate = Convert.ToDateTime(dr["StorageEndDate"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mRoadNumber = Convert.ToString(dr["RoadNumber"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mDescriptionOfGoods = Convert.ToString(dr["DescriptionOfGoods"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mGrossWeight = Convert.ToDecimal(dr["GrossWeight"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mNetWeight = Convert.ToDecimal(dr["NetWeight"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mVolume = Convert.ToDecimal(dr["Volume"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mPackages = Convert.ToString(dr["Packages"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mOtherRemarks = Convert.ToString(dr["OtherRemarks"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mAddedByUser = Convert.ToString(dr["AddedByUser"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mAddedAt = Convert.ToDateTime(dr["AddedAt"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mUpdatedByUser = Convert.ToString(dr["UpdatedByUser"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mUpdatedAt = Convert.ToDateTime(dr["UpdatedAt"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mKalmarOnCount = Convert.ToInt32(dr["KalmarOnCount"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mKalmarOffCount = Convert.ToInt32(dr["KalmarOffCount"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mContainerID = Convert.ToInt64(dr["ContainerID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mHouseBillID = Convert.ToInt64(dr["HouseBillID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mConsigneeID = Convert.ToInt32(dr["ConsigneeID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mBookingPartyID = Convert.ToInt32(dr["BookingPartyID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mInventoryID = Convert.ToInt64(dr["InventoryID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mWarehouseID = Convert.ToInt32(dr["WarehouseID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mAreaID = Convert.ToInt32(dr["AreaID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mRowID = Convert.ToInt32(dr["RowID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mRowLocationID = Convert.ToInt32(dr["RowLocationID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mEmptyContainerID = Convert.ToInt64(dr["EmptyContainerID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mWarehouseNoteID = Convert.ToInt32(dr["WarehouseNoteID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mOperationPartnerID = Convert.ToInt64(dr["OperationPartnerID"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mHasDamage = Convert.ToBoolean(dr["HasDamage"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mDamageDescription = Convert.ToString(dr["DamageDescription"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mCustomsSealNumber = Convert.ToString(dr["CustomsSealNumber"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mCustomsCertificateNumber = Convert.ToString(dr["CustomsCertificateNumber"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mCustomsFeesAmount = Convert.ToDecimal(dr["CustomsFeesAmount"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mCustomsFeesVAT = Convert.ToDecimal(dr["CustomsFeesVAT"].ToString());
                        ObjCVarvwWH_CFS_GateInInventory.mCustomsFeesTotal = Convert.ToDecimal(dr["CustomsFeesTotal"].ToString());

                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwWH_CFS_GateInInventory.Add(ObjCVarvwWH_CFS_GateInInventory);
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
