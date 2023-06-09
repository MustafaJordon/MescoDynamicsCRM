using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Globalization;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.ContainerFreightStation.Transactions
{
    [Serializable]
    public class CPKWH_Inventory
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
    public partial class CVarWH_Inventory : CPKWH_Inventory
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mOperationID;
        internal Int64 mHouseBillID;
        internal Int64 mContainerID;
        internal Int32 mWarehouseID;
        internal DateTime mEntryDate;
        internal Int64 mEmptyContainerID;
        internal Int32 mAreaID;
        internal Int32 mRowID;
        internal Int32 mRowLocationID;
        internal Int32 mBookingPartyID;
        internal Int32 mWarehouseNoteID;
        internal String mOtherRemarks;
        internal Int32 mAddedBy;
        internal DateTime mAddedAt;
        internal Int32 mUpdatedBy;
        internal DateTime mUpdatedAt;
        internal DateTime mStorageEndDate;
        internal Int32 mKalmarOnCount;
        internal Int32 mKalmarOffCount;
        internal String mDriverNameIn;
        internal String mTruckNoIn;
        internal String mDriverNameOut;
        internal String mTruckNoOut;
        internal Boolean mHasDamage;
        internal String mDamageDescription;
        internal String mCustomsSealNumber;
        internal String mCustomsCertificateNumber;
        internal Decimal mCustomsFeesAmount;
        internal Decimal mCustomsFeesVAT;
        internal Decimal mCustomsFeesTotal;
        #endregion

        #region "Methods"
        public Int64 OperationID
        {
            get { return mOperationID; }
            set { mIsChanges = true; mOperationID = value; }
        }
        public Int64 HouseBillID
        {
            get { return mHouseBillID; }
            set { mIsChanges = true; mHouseBillID = value; }
        }
        public Int64 ContainerID
        {
            get { return mContainerID; }
            set { mIsChanges = true; mContainerID = value; }
        }
        public Int32 WarehouseID
        {
            get { return mWarehouseID; }
            set { mIsChanges = true; mWarehouseID = value; }
        }
        public DateTime EntryDate
        {
            get { return mEntryDate; }
            set { mIsChanges = true; mEntryDate = value; }
        }
        public Int64 EmptyContainerID
        {
            get { return mEmptyContainerID; }
            set { mIsChanges = true; mEmptyContainerID = value; }
        }
        public Int32 AreaID
        {
            get { return mAreaID; }
            set { mIsChanges = true; mAreaID = value; }
        }
        public Int32 RowID
        {
            get { return mRowID; }
            set { mIsChanges = true; mRowID = value; }
        }
        public Int32 RowLocationID
        {
            get { return mRowLocationID; }
            set { mIsChanges = true; mRowLocationID = value; }
        }
        public Int32 BookingPartyID
        {
            get { return mBookingPartyID; }
            set { mIsChanges = true; mBookingPartyID = value; }
        }
        public Int32 WarehouseNoteID
        {
            get { return mWarehouseNoteID; }
            set { mIsChanges = true; mWarehouseNoteID = value; }
        }
        public String OtherRemarks
        {
            get { return mOtherRemarks; }
            set { mIsChanges = true; mOtherRemarks = value; }
        }
        public Int32 AddedBy
        {
            get { return mAddedBy; }
            set { mIsChanges = true; mAddedBy = value; }
        }
        public DateTime AddedAt
        {
            get { return mAddedAt; }
            set { mIsChanges = true; mAddedAt = value; }
        }
        public Int32 UpdatedBy
        {
            get { return mUpdatedBy; }
            set { mIsChanges = true; mUpdatedBy = value; }
        }
        public DateTime UpdatedAt
        {
            get { return mUpdatedAt; }
            set { mIsChanges = true; mUpdatedAt = value; }
        }
        public DateTime StorageEndDate
        {
            get { return mStorageEndDate; }
            set { mIsChanges = true; mStorageEndDate = value; }
        }
        public Int32 KalmarOnCount
        {
            get { return mKalmarOnCount; }
            set { mIsChanges = true; mKalmarOnCount = value; }
        }
        public Int32 KalmarOffCount
        {
            get { return mKalmarOffCount; }
            set { mIsChanges = true; mKalmarOffCount = value; }
        }
        public String DriverNameIn
        {
            get { return mDriverNameIn; }
            set { mIsChanges = true; mDriverNameIn = value; }
        }
        public String TruckNoIn
        {
            get { return mTruckNoIn; }
            set { mIsChanges = true; mTruckNoIn = value; }
        }
        public String DriverNameOut
        {
            get { return mDriverNameIn; }
            set { mIsChanges = true; mDriverNameIn = value; }
        }
        public String TruckNoOut
        {
            get { return mTruckNoIn; }
            set { mIsChanges = true; mTruckNoIn = value; }
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

    public partial class CWH_Inventory
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
        public List<CVarWH_Inventory> lstCVarWH_Inventory = new List<CVarWH_Inventory>();
        public List<CPKWH_Inventory> lstDeletedCPKWH_Inventory = new List<CPKWH_Inventory>();
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
        public Exception GetItem(Int64 ID)
        {
            return DataFill(Convert.ToString(ID), false);
        }
        private Exception DataFill(string Param, Boolean IsList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarWH_Inventory.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListWH_Inventory";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemWH_Inventory";
                    Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt));
                    Com.Parameters[0].Value = Convert.ToInt64(Param);
                }
                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        /*Start DataReader*/
                        CVarWH_Inventory ObjCVarWH_Inventory = new CVarWH_Inventory();
                        ObjCVarWH_Inventory.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarWH_Inventory.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarWH_Inventory.mHouseBillID = Convert.ToInt64(dr["HouseBillID"].ToString());
                        ObjCVarWH_Inventory.mContainerID = Convert.ToInt64(dr["ContainerID"].ToString());
                        ObjCVarWH_Inventory.mWarehouseID = Convert.ToInt32(dr["WarehouseID"].ToString());
                        ObjCVarWH_Inventory.mEntryDate = Convert.ToDateTime(dr["EntryDate"].ToString());
                        ObjCVarWH_Inventory.mEmptyContainerID = Convert.ToInt64(dr["EmptyContainerID"].ToString());
                        ObjCVarWH_Inventory.mAreaID = Convert.ToInt32(dr["AreaID"].ToString());
                        ObjCVarWH_Inventory.mRowID = Convert.ToInt32(dr["RowID"].ToString());
                        ObjCVarWH_Inventory.mRowLocationID = Convert.ToInt32(dr["RowLocationID"].ToString());
                        ObjCVarWH_Inventory.mBookingPartyID = Convert.ToInt32(dr["BookingPartyID"].ToString());
                        ObjCVarWH_Inventory.mWarehouseNoteID = Convert.ToInt32(dr["WarehouseNoteID"].ToString());
                        ObjCVarWH_Inventory.mOtherRemarks = Convert.ToString(dr["OtherRemarks"].ToString());
                        ObjCVarWH_Inventory.mAddedBy = Convert.ToInt32(dr["AddedBy"].ToString());
                        ObjCVarWH_Inventory.mAddedAt = Convert.ToDateTime(dr["AddedAt"].ToString());
                        ObjCVarWH_Inventory.mUpdatedBy = Convert.ToInt32(dr["UpdatedBy"].ToString());
                        ObjCVarWH_Inventory.mUpdatedAt = Convert.ToDateTime(dr["UpdatedAt"].ToString());
                        ObjCVarWH_Inventory.mStorageEndDate = Convert.ToDateTime(dr["StorageEndDate"].ToString());
                        ObjCVarWH_Inventory.mKalmarOnCount = Convert.ToInt32(dr["KalmarOnCount"].ToString());
                        ObjCVarWH_Inventory.mKalmarOffCount = Convert.ToInt32(dr["KalmarOffCount"].ToString());
                        ObjCVarWH_Inventory.mDriverNameIn = Convert.ToString(dr["DriverNameIn"].ToString());
                        ObjCVarWH_Inventory.mTruckNoIn = Convert.ToString(dr["TruckNoIn"].ToString());
                        ObjCVarWH_Inventory.mDriverNameIn = Convert.ToString(dr["DriverNameOut"].ToString());
                        ObjCVarWH_Inventory.mTruckNoIn = Convert.ToString(dr["TruckNoOut"].ToString());
                        ObjCVarWH_Inventory.mHasDamage = Convert.ToBoolean(dr["HasDamage"].ToString());
                        ObjCVarWH_Inventory.mDamageDescription = Convert.ToString(dr["DamageDescription"].ToString());
                        ObjCVarWH_Inventory.mCustomsSealNumber = Convert.ToString(dr["CustomsSealNumber"].ToString());
                        ObjCVarWH_Inventory.mCustomsCertificateNumber = Convert.ToString(dr["CustomsCertificateNumber"].ToString());
                        ObjCVarWH_Inventory.mCustomsFeesAmount = Convert.ToDecimal(dr["CustomsFeesAmount"].ToString());
                        ObjCVarWH_Inventory.mCustomsFeesVAT = Convert.ToDecimal(dr["CustomsFeesVAT"].ToString());
                        ObjCVarWH_Inventory.mCustomsFeesTotal = Convert.ToDecimal(dr["CustomsFeesTotal"].ToString());

                        lstCVarWH_Inventory.Add(ObjCVarWH_Inventory);
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
            lstCVarWH_Inventory.Clear();

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
                Com.CommandText = "[dbo].GetListPagingWH_Inventory";
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
                        CVarWH_Inventory ObjCVarWH_Inventory = new CVarWH_Inventory();
                        ObjCVarWH_Inventory.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarWH_Inventory.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarWH_Inventory.mHouseBillID = Convert.ToInt64(dr["HouseBillID"].ToString());
                        ObjCVarWH_Inventory.mContainerID = Convert.ToInt64(dr["ContainerID"].ToString());
                        ObjCVarWH_Inventory.mWarehouseID = Convert.ToInt32(dr["WarehouseID"].ToString());
                        ObjCVarWH_Inventory.mEntryDate = Convert.ToDateTime(dr["EntryDate"].ToString());
                        ObjCVarWH_Inventory.mEmptyContainerID = Convert.ToInt64(dr["EmptyContainerID"].ToString());
                        ObjCVarWH_Inventory.mAreaID = Convert.ToInt32(dr["AreaID"].ToString());
                        ObjCVarWH_Inventory.mRowID = Convert.ToInt32(dr["RowID"].ToString());
                        ObjCVarWH_Inventory.mRowLocationID = Convert.ToInt32(dr["RowLocationID"].ToString());
                        ObjCVarWH_Inventory.mBookingPartyID = Convert.ToInt32(dr["BookingPartyID"].ToString());
                        ObjCVarWH_Inventory.mWarehouseNoteID = Convert.ToInt32(dr["WarehouseNoteID"].ToString());
                        ObjCVarWH_Inventory.mOtherRemarks = Convert.ToString(dr["OtherRemarks"].ToString());
                        ObjCVarWH_Inventory.mAddedBy = Convert.ToInt32(dr["AddedBy"].ToString());
                        ObjCVarWH_Inventory.mAddedAt = Convert.ToDateTime(dr["AddedAt"].ToString());
                        ObjCVarWH_Inventory.mUpdatedBy = Convert.ToInt32(dr["UpdatedBy"].ToString());
                        ObjCVarWH_Inventory.mUpdatedAt = Convert.ToDateTime(dr["UpdatedAt"].ToString());
                        ObjCVarWH_Inventory.mStorageEndDate = Convert.ToDateTime(dr["StorageEndDate"].ToString());
                        ObjCVarWH_Inventory.mKalmarOnCount = Convert.ToInt32(dr["KalmarOnCount"].ToString());
                        ObjCVarWH_Inventory.mKalmarOffCount = Convert.ToInt32(dr["KalmarOffCount"].ToString());
                        ObjCVarWH_Inventory.mDriverNameIn = Convert.ToString(dr["DriverNameIn"].ToString());
                        ObjCVarWH_Inventory.mTruckNoIn = Convert.ToString(dr["TruckNoIn"].ToString());
                        ObjCVarWH_Inventory.mDriverNameIn = Convert.ToString(dr["DriverNameOut"].ToString());
                        ObjCVarWH_Inventory.mTruckNoIn = Convert.ToString(dr["TruckNoOut"].ToString());
                        ObjCVarWH_Inventory.mHasDamage = Convert.ToBoolean(dr["HasDamage"].ToString());
                        ObjCVarWH_Inventory.mDamageDescription = Convert.ToString(dr["DamageDescription"].ToString());
                        ObjCVarWH_Inventory.mCustomsSealNumber = Convert.ToString(dr["CustomsSealNumber"].ToString());
                        ObjCVarWH_Inventory.mCustomsCertificateNumber = Convert.ToString(dr["CustomsCertificateNumber"].ToString());
                        ObjCVarWH_Inventory.mCustomsFeesAmount = Convert.ToDecimal(dr["CustomsFeesAmount"].ToString());
                        ObjCVarWH_Inventory.mCustomsFeesVAT = Convert.ToDecimal(dr["CustomsFeesVAT"].ToString());
                        ObjCVarWH_Inventory.mCustomsFeesTotal = Convert.ToDecimal(dr["CustomsFeesTotal"].ToString());

                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarWH_Inventory.Add(ObjCVarWH_Inventory);
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
                    Com.CommandText = "[dbo].DeleteListWH_Inventory";
                else
                    Com.CommandText = "[dbo].UpdateListWH_Inventory";
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
        public Exception DeleteItem(List<CPKWH_Inventory> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemWH_Inventory";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt));
                foreach (CPKWH_Inventory ObjCPKWH_Inventory in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt64(ObjCPKWH_Inventory.ID);
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
        public Exception SaveMethod(List<CVarWH_Inventory> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@OperationID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@HouseBillID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@ContainerID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@WarehouseID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@EntryDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@EmptyContainerID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@AreaID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@RowID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@RowLocationID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@BookingPartyID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@WarehouseNoteID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@OtherRemarks", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@AddedBy", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@AddedAt", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@UpdatedAt", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@StorageEndDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@KalmarOnCount", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@KalmarOffCount", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@DriverNameIn", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@TruckNoIn", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@DriverNameOut", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@TruckNoOut", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@HasDamage", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@DamageDescription", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@CustomsSealNumber", SqlDbType.VarChar));
                Com.Parameters.Add(new SqlParameter("@CustomsCertificateNumber", SqlDbType.VarChar));
                Com.Parameters.Add(new SqlParameter("@CustomsFeesAmount", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@CustomsFeesVAT", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@CustomsFeesTotal", SqlDbType.Decimal));

                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarWH_Inventory ObjCVarWH_Inventory in SaveList)
                {
                    if (ObjCVarWH_Inventory.mIsChanges == true)
                    {
                        if (ObjCVarWH_Inventory.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemWH_Inventory";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarWH_Inventory.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemWH_Inventory";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarWH_Inventory.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarWH_Inventory.ID;
                        }
                        Com.Parameters["@OperationID"].Value = ObjCVarWH_Inventory.OperationID;
                        Com.Parameters["@HouseBillID"].Value = ObjCVarWH_Inventory.HouseBillID;
                        Com.Parameters["@ContainerID"].Value = ObjCVarWH_Inventory.ContainerID;
                        Com.Parameters["@WarehouseID"].Value = ObjCVarWH_Inventory.WarehouseID;
                        Com.Parameters["@EntryDate"].Value = ObjCVarWH_Inventory.EntryDate;
                        Com.Parameters["@EmptyContainerID"].Value = ObjCVarWH_Inventory.EmptyContainerID;
                        Com.Parameters["@AreaID"].Value = ObjCVarWH_Inventory.AreaID;
                        Com.Parameters["@RowID"].Value = ObjCVarWH_Inventory.RowID;
                        Com.Parameters["@RowLocationID"].Value = ObjCVarWH_Inventory.RowLocationID;
                        Com.Parameters["@BookingPartyID"].Value = ObjCVarWH_Inventory.BookingPartyID;
                        Com.Parameters["@WarehouseNoteID"].Value = ObjCVarWH_Inventory.WarehouseNoteID;
                        Com.Parameters["@OtherRemarks"].Value = ObjCVarWH_Inventory.OtherRemarks;
                        Com.Parameters["@AddedBy"].Value = ObjCVarWH_Inventory.AddedBy;
                        Com.Parameters["@AddedAt"].Value = ObjCVarWH_Inventory.AddedAt;
                        Com.Parameters["@UpdatedBy"].Value = ObjCVarWH_Inventory.UpdatedBy;
                        Com.Parameters["@UpdatedAt"].Value = ObjCVarWH_Inventory.UpdatedAt;
                        Com.Parameters["@StorageEndDate"].Value = ObjCVarWH_Inventory.StorageEndDate;
                        Com.Parameters["@KalmarOnCount"].Value = ObjCVarWH_Inventory.KalmarOnCount;
                        Com.Parameters["@KalmarOffCount"].Value = ObjCVarWH_Inventory.KalmarOffCount;
                        Com.Parameters["@DriverNameIn"].Value = ObjCVarWH_Inventory.DriverNameIn;
                        Com.Parameters["@TruckNoIn"].Value = ObjCVarWH_Inventory.TruckNoIn;
                        Com.Parameters["@DriverNameOut"].Value = ObjCVarWH_Inventory.DriverNameOut;
                        Com.Parameters["@TruckNoOut"].Value = ObjCVarWH_Inventory.TruckNoOut;
                        Com.Parameters["@HasDamage"].Value = ObjCVarWH_Inventory.HasDamage;
                        Com.Parameters["@DamageDescription"].Value = ObjCVarWH_Inventory.DamageDescription;
                        Com.Parameters["@CustomsSealNumber"].Value = ObjCVarWH_Inventory.CustomsSealNumber;
                        Com.Parameters["@CustomsCertificateNumber"].Value = ObjCVarWH_Inventory.CustomsCertificateNumber;
                        Com.Parameters["@CustomsFeesAmount"].Value = ObjCVarWH_Inventory.CustomsFeesAmount;
                        Com.Parameters["@CustomsFeesVAT"].Value = ObjCVarWH_Inventory.CustomsFeesVAT;
                        Com.Parameters["@CustomsFeesTotal"].Value = ObjCVarWH_Inventory.CustomsFeesTotal;

                        EndTrans(Com, Con);
                        if (ObjCVarWH_Inventory.ID == 0)
                        {
                            ObjCVarWH_Inventory.ID = Convert.ToInt64(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarWH_Inventory.mIsChanges = false;
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

        public Exception CalculateStorage(string InventoryID, string StorageEndDate, out Decimal CalculatedAmount)
        {
            Exception Exp = null;

            CalculatedAmount = 0;

            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@InventoryID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@StorageEndDate", SqlDbType.DateTime));

                Com.Parameters.Add(new SqlParameter("@StorageAmount", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 12, 3, "ID", DataRowVersion.Default, null));

                Com.CommandText = "[dbo].WH_Inventory_CalculateStorage";

                BeginTrans(Com, Con);

                Com.Parameters["@InventoryID"].Value = Int64.Parse(InventoryID);

                // nour 09052022
                //Com.Parameters["@StorageEndDate"].Value = DateTime.Parse(StorageEndDate);
                Com.Parameters["@StorageEndDate"].Value = DateTime.ParseExact(StorageEndDate + " 00:00:00.000", "dd/MM/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);

                EndTrans(Com, Con);

                if (Com.Parameters["@StorageAmount"].Value != DBNull.Value)
                {
                    CalculatedAmount = Convert.ToDecimal(Com.Parameters["@StorageAmount"].Value);
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


        public Exception GenerateReceivables(string InventoryID, int UserID, bool IsConsol)
        {
            Exception Exp = null;

            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@InventoryID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int));
                if (IsConsol)
                {
                    Com.CommandText = "[dbo].WH_Inventory_GenerateReceivablesForConsol";
                }
                else
                {
                    Com.CommandText = "[dbo].WH_Inventory_GenerateReceivablesForFCL";
                }


                BeginTrans(Com, Con);

                Com.Parameters["@InventoryID"].Value = Int64.Parse(InventoryID);
                Com.Parameters["@UserID"].Value = UserID;

                EndTrans(Com, Con);
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







    }
}
