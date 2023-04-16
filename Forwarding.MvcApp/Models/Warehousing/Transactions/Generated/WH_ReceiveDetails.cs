using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.Warehousing.Transactions.Generated.Old
{
    [Serializable]
    public class CPKWH_ReceiveDetails
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
    public partial class CVarWH_ReceiveDetails : CPKWH_ReceiveDetails
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mReceiveID;
        internal String mBarCode;
        internal Int64 mPurchaseItemID;
        internal Decimal mQuantity;
        internal Decimal mExpectedQuantity;
        internal Decimal mSplitQuantity;
        internal Int32 mLocationID;
        internal String mPalletID;
        internal DateTime mReceiveDate;
        internal Int32 mStatusID;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Boolean mIsExcluded;
        internal String mLotNo;
        internal DateTime mExpireDate;
        internal Int64 mOperationVehicleID;
        internal Int64 mVehicleActionID;
        internal Boolean mIsPickupAddedToInvoice;
        internal DateTime mPickupWithoutInvoiceDate;
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
            set { mIsChanges = true; mReceiveID = value; }
        }
        public String BarCode
        {
            get { return mBarCode; }
            set { mIsChanges = true; mBarCode = value; }
        }
        public Int64 PurchaseItemID
        {
            get { return mPurchaseItemID; }
            set { mIsChanges = true; mPurchaseItemID = value; }
        }
        public Decimal Quantity
        {
            get { return mQuantity; }
            set { mIsChanges = true; mQuantity = value; }
        }
        public Decimal ExpectedQuantity
        {
            get { return mExpectedQuantity; }
            set { mIsChanges = true; mExpectedQuantity = value; }
        }
        public Decimal SplitQuantity
        {
            get { return mSplitQuantity; }
            set { mIsChanges = true; mSplitQuantity = value; }
        }
        public Int32 LocationID
        {
            get { return mLocationID; }
            set { mIsChanges = true; mLocationID = value; }
        }
        public String PalletID
        {
            get { return mPalletID; }
            set { mIsChanges = true; mPalletID = value; }
        }
        public DateTime ReceiveDate
        {
            get { return mReceiveDate; }
            set { mIsChanges = true; mReceiveDate = value; }
        }
        public Int32 StatusID
        {
            get { return mStatusID; }
            set { mIsChanges = true; mStatusID = value; }
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
        public Boolean IsExcluded
        {
            get { return mIsExcluded; }
            set { mIsChanges = true; mIsExcluded = value; }
        }
        public String LotNo
        {
            get { return mLotNo; }
            set { mIsChanges = true; mLotNo = value; }
        }
        public DateTime ExpireDate
        {
            get { return mExpireDate; }
            set { mIsChanges = true; mExpireDate = value; }
        }
        public Int64 OperationVehicleID
        {
            get { return mOperationVehicleID; }
            set { mIsChanges = true; mOperationVehicleID = value; }
        }
        public Int64 VehicleActionID
        {
            get { return mVehicleActionID; }
            set { mIsChanges = true; mVehicleActionID = value; }
        }
        public Boolean IsPickupAddedToInvoice
        {
            get { return mIsPickupAddedToInvoice; }
            set { mIsChanges = true; mIsPickupAddedToInvoice = value; }
        }
        public DateTime PickupWithoutInvoiceDate
        {
            get { return mPickupWithoutInvoiceDate; }
            set { mIsChanges = true; mPickupWithoutInvoiceDate = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mIsChanges = true; mNotes = value; }
        }
        public String BatchNumber
        {
            get { return mBatchNumber; }
            set { mIsChanges = true; mBatchNumber = value; }
        }
        public DateTime ExpirationDate
        {
            get { return mExpirationDate; }
            set { mIsChanges = true; mExpirationDate = value; }
        }
        public String ImportedBy
        {
            get { return mImportedBy; }
            set { mIsChanges = true; mImportedBy = value; }
        }
        public Decimal WeightInTons
        {
            get { return mWeightInTons; }
            set { mIsChanges = true; mWeightInTons = value; }
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

    public partial class CWH_ReceiveDetails
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
        public List<CVarWH_ReceiveDetails> lstCVarWH_ReceiveDetails = new List<CVarWH_ReceiveDetails>();
        public List<CPKWH_ReceiveDetails> lstDeletedCPKWH_ReceiveDetails = new List<CPKWH_ReceiveDetails>();
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
            lstCVarWH_ReceiveDetails.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListWH_ReceiveDetails";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemWH_ReceiveDetails";
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
                        CVarWH_ReceiveDetails ObjCVarWH_ReceiveDetails = new CVarWH_ReceiveDetails();
                        ObjCVarWH_ReceiveDetails.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarWH_ReceiveDetails.mReceiveID = Convert.ToInt64(dr["ReceiveID"].ToString());
                        ObjCVarWH_ReceiveDetails.mBarCode = Convert.ToString(dr["BarCode"].ToString());
                        ObjCVarWH_ReceiveDetails.mPurchaseItemID = Convert.ToInt64(dr["PurchaseItemID"].ToString());
                        ObjCVarWH_ReceiveDetails.mQuantity = Convert.ToDecimal(dr["Quantity"].ToString());
                        ObjCVarWH_ReceiveDetails.mExpectedQuantity = Convert.ToDecimal(dr["ExpectedQuantity"].ToString());
                        ObjCVarWH_ReceiveDetails.mSplitQuantity = Convert.ToDecimal(dr["SplitQuantity"].ToString());
                        ObjCVarWH_ReceiveDetails.mLocationID = Convert.ToInt32(dr["LocationID"].ToString());
                        ObjCVarWH_ReceiveDetails.mPalletID = Convert.ToString(dr["PalletID"].ToString());
                        ObjCVarWH_ReceiveDetails.mReceiveDate = Convert.ToDateTime(dr["ReceiveDate"].ToString());
                        ObjCVarWH_ReceiveDetails.mStatusID = Convert.ToInt32(dr["StatusID"].ToString());
                        ObjCVarWH_ReceiveDetails.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarWH_ReceiveDetails.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarWH_ReceiveDetails.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarWH_ReceiveDetails.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarWH_ReceiveDetails.mIsExcluded = Convert.ToBoolean(dr["IsExcluded"].ToString());
                        ObjCVarWH_ReceiveDetails.mLotNo = Convert.ToString(dr["LotNo"].ToString());
                        ObjCVarWH_ReceiveDetails.mExpireDate = Convert.ToDateTime(dr["ExpireDate"].ToString());
                        ObjCVarWH_ReceiveDetails.mOperationVehicleID = Convert.ToInt64(dr["OperationVehicleID"].ToString());
                        ObjCVarWH_ReceiveDetails.mVehicleActionID = Convert.ToInt64(dr["VehicleActionID"].ToString());
                        ObjCVarWH_ReceiveDetails.mIsPickupAddedToInvoice = Convert.ToBoolean(dr["IsPickupAddedToInvoice"].ToString());
                        ObjCVarWH_ReceiveDetails.mPickupWithoutInvoiceDate = Convert.ToDateTime(dr["PickupWithoutInvoiceDate"].ToString());
                        ObjCVarWH_ReceiveDetails.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarWH_ReceiveDetails.mBatchNumber = Convert.ToString(dr["BatchNumber"].ToString());
                        ObjCVarWH_ReceiveDetails.mExpirationDate = Convert.ToDateTime(dr["ExpirationDate"].ToString());
                        ObjCVarWH_ReceiveDetails.mImportedBy = Convert.ToString(dr["ImportedBy"].ToString());
                        ObjCVarWH_ReceiveDetails.mWeightInTons = Convert.ToDecimal(dr["WeightInTons"].ToString());
                        lstCVarWH_ReceiveDetails.Add(ObjCVarWH_ReceiveDetails);
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
            lstCVarWH_ReceiveDetails.Clear();

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
                Com.CommandText = "[dbo].GetListPagingWH_ReceiveDetails";
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
                        CVarWH_ReceiveDetails ObjCVarWH_ReceiveDetails = new CVarWH_ReceiveDetails();
                        ObjCVarWH_ReceiveDetails.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarWH_ReceiveDetails.mReceiveID = Convert.ToInt64(dr["ReceiveID"].ToString());
                        ObjCVarWH_ReceiveDetails.mBarCode = Convert.ToString(dr["BarCode"].ToString());
                        ObjCVarWH_ReceiveDetails.mPurchaseItemID = Convert.ToInt64(dr["PurchaseItemID"].ToString());
                        ObjCVarWH_ReceiveDetails.mQuantity = Convert.ToDecimal(dr["Quantity"].ToString());
                        ObjCVarWH_ReceiveDetails.mExpectedQuantity = Convert.ToDecimal(dr["ExpectedQuantity"].ToString());
                        ObjCVarWH_ReceiveDetails.mSplitQuantity = Convert.ToDecimal(dr["SplitQuantity"].ToString());
                        ObjCVarWH_ReceiveDetails.mLocationID = Convert.ToInt32(dr["LocationID"].ToString());
                        ObjCVarWH_ReceiveDetails.mPalletID = Convert.ToString(dr["PalletID"].ToString());
                        ObjCVarWH_ReceiveDetails.mReceiveDate = Convert.ToDateTime(dr["ReceiveDate"].ToString());
                        ObjCVarWH_ReceiveDetails.mStatusID = Convert.ToInt32(dr["StatusID"].ToString());
                        ObjCVarWH_ReceiveDetails.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarWH_ReceiveDetails.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarWH_ReceiveDetails.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarWH_ReceiveDetails.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarWH_ReceiveDetails.mIsExcluded = Convert.ToBoolean(dr["IsExcluded"].ToString());
                        ObjCVarWH_ReceiveDetails.mLotNo = Convert.ToString(dr["LotNo"].ToString());
                        ObjCVarWH_ReceiveDetails.mExpireDate = Convert.ToDateTime(dr["ExpireDate"].ToString());
                        ObjCVarWH_ReceiveDetails.mOperationVehicleID = Convert.ToInt64(dr["OperationVehicleID"].ToString());
                        ObjCVarWH_ReceiveDetails.mVehicleActionID = Convert.ToInt64(dr["VehicleActionID"].ToString());
                        ObjCVarWH_ReceiveDetails.mIsPickupAddedToInvoice = Convert.ToBoolean(dr["IsPickupAddedToInvoice"].ToString());
                        ObjCVarWH_ReceiveDetails.mPickupWithoutInvoiceDate = Convert.ToDateTime(dr["PickupWithoutInvoiceDate"].ToString());
                        ObjCVarWH_ReceiveDetails.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarWH_ReceiveDetails.mBatchNumber = Convert.ToString(dr["BatchNumber"].ToString());
                        ObjCVarWH_ReceiveDetails.mExpirationDate = Convert.ToDateTime(dr["ExpirationDate"].ToString());
                        ObjCVarWH_ReceiveDetails.mImportedBy = Convert.ToString(dr["ImportedBy"].ToString());
                        ObjCVarWH_ReceiveDetails.mWeightInTons = Convert.ToDecimal(dr["WeightInTons"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarWH_ReceiveDetails.Add(ObjCVarWH_ReceiveDetails);
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
                    Com.CommandText = "[dbo].DeleteListWH_ReceiveDetails";
                else
                    Com.CommandText = "[dbo].UpdateListWH_ReceiveDetails";
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
        public Exception DeleteItem(List<CPKWH_ReceiveDetails> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemWH_ReceiveDetails";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt));
                foreach (CPKWH_ReceiveDetails ObjCPKWH_ReceiveDetails in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt64(ObjCPKWH_ReceiveDetails.ID);
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
        public Exception SaveMethod(List<CVarWH_ReceiveDetails> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@ReceiveID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@BarCode", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@PurchaseItemID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@ExpectedQuantity", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@SplitQuantity", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@LocationID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@PalletID", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@ReceiveDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@StatusID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ModificatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ModificationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@IsExcluded", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@LotNo", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@ExpireDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@OperationVehicleID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@VehicleActionID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@IsPickupAddedToInvoice", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@PickupWithoutInvoiceDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@BatchNumber", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@ExpirationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ImportedBy", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@WeightInTons", SqlDbType.Decimal));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarWH_ReceiveDetails ObjCVarWH_ReceiveDetails in SaveList)
                {
                    if (ObjCVarWH_ReceiveDetails.mIsChanges == true)
                    {
                        if (ObjCVarWH_ReceiveDetails.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemWH_ReceiveDetails";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarWH_ReceiveDetails.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemWH_ReceiveDetails";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarWH_ReceiveDetails.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarWH_ReceiveDetails.ID;
                        }
                        Com.Parameters["@ReceiveID"].Value = ObjCVarWH_ReceiveDetails.ReceiveID;
                        Com.Parameters["@BarCode"].Value = ObjCVarWH_ReceiveDetails.BarCode;
                        Com.Parameters["@PurchaseItemID"].Value = ObjCVarWH_ReceiveDetails.PurchaseItemID;
                        Com.Parameters["@Quantity"].Value = ObjCVarWH_ReceiveDetails.Quantity;
                        Com.Parameters["@ExpectedQuantity"].Value = ObjCVarWH_ReceiveDetails.ExpectedQuantity;
                        Com.Parameters["@SplitQuantity"].Value = ObjCVarWH_ReceiveDetails.SplitQuantity;
                        Com.Parameters["@LocationID"].Value = ObjCVarWH_ReceiveDetails.LocationID;
                        Com.Parameters["@PalletID"].Value = ObjCVarWH_ReceiveDetails.PalletID;
                        Com.Parameters["@ReceiveDate"].Value = ObjCVarWH_ReceiveDetails.ReceiveDate;
                        Com.Parameters["@StatusID"].Value = ObjCVarWH_ReceiveDetails.StatusID;
                        Com.Parameters["@CreatorUserID"].Value = ObjCVarWH_ReceiveDetails.CreatorUserID;
                        Com.Parameters["@CreationDate"].Value = ObjCVarWH_ReceiveDetails.CreationDate;
                        Com.Parameters["@ModificatorUserID"].Value = ObjCVarWH_ReceiveDetails.ModificatorUserID;
                        Com.Parameters["@ModificationDate"].Value = ObjCVarWH_ReceiveDetails.ModificationDate;
                        Com.Parameters["@IsExcluded"].Value = ObjCVarWH_ReceiveDetails.IsExcluded;
                        Com.Parameters["@LotNo"].Value = ObjCVarWH_ReceiveDetails.LotNo;
                        Com.Parameters["@ExpireDate"].Value = ObjCVarWH_ReceiveDetails.ExpireDate;
                        Com.Parameters["@OperationVehicleID"].Value = ObjCVarWH_ReceiveDetails.OperationVehicleID;
                        Com.Parameters["@VehicleActionID"].Value = ObjCVarWH_ReceiveDetails.VehicleActionID;
                        Com.Parameters["@IsPickupAddedToInvoice"].Value = ObjCVarWH_ReceiveDetails.IsPickupAddedToInvoice;
                        Com.Parameters["@PickupWithoutInvoiceDate"].Value = ObjCVarWH_ReceiveDetails.PickupWithoutInvoiceDate;
                        Com.Parameters["@Notes"].Value = ObjCVarWH_ReceiveDetails.Notes;
                        Com.Parameters["@BatchNumber"].Value = ObjCVarWH_ReceiveDetails.BatchNumber;
                        Com.Parameters["@ExpirationDate"].Value = ObjCVarWH_ReceiveDetails.ExpirationDate;
                        Com.Parameters["@ImportedBy"].Value = ObjCVarWH_ReceiveDetails.ImportedBy;
                        Com.Parameters["@WeightInTons"].Value = ObjCVarWH_ReceiveDetails.WeightInTons;
                        EndTrans(Com, Con);
                        if (ObjCVarWH_ReceiveDetails.ID == 0)
                        {
                            ObjCVarWH_ReceiveDetails.ID = Convert.ToInt64(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarWH_ReceiveDetails.mIsChanges = false;
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
