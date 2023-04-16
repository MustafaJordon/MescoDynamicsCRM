using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace Forwarding.MvcApp.Models.MasterData.Trucking.Generated
{
    [Serializable]
    public class CPKTRCK_Drivers
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
    public partial class CVarTRCK_Drivers : CPKTRCK_Drivers
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mCode;
        internal String mName;
        internal String mLocalName;
        internal Boolean mIsDriver;
        internal String mPhone;
        internal String mMobile;
        internal String mNationalIDNumber;
        internal DateTime mNationalIDExpireDate;
        internal String mLicenseNumber;
        internal DateTime mLicenseNumberExpireDate;
        internal DateTime mServiceStartDate;
        internal DateTime mServiceEndDate;
        internal String mAddress;
        internal String mNotes;
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
        internal DateTime mBirthDate;
        internal String mSupervisorName;
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
        public Boolean IsDriver
        {
            get { return mIsDriver; }
            set { mIsChanges = true; mIsDriver = value; }
        }
        public String Phone
        {
            get { return mPhone; }
            set { mIsChanges = true; mPhone = value; }
        }
        public String Mobile
        {
            get { return mMobile; }
            set { mIsChanges = true; mMobile = value; }
        }
        public String NationalIDNumber
        {
            get { return mNationalIDNumber; }
            set { mIsChanges = true; mNationalIDNumber = value; }
        }
        public DateTime NationalIDExpireDate
        {
            get { return mNationalIDExpireDate; }
            set { mIsChanges = true; mNationalIDExpireDate = value; }
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
        public DateTime ServiceStartDate
        {
            get { return mServiceStartDate; }
            set { mIsChanges = true; mServiceStartDate = value; }
        }
        public DateTime ServiceEndDate
        {
            get { return mServiceEndDate; }
            set { mIsChanges = true; mServiceEndDate = value; }
        }
        public String Address
        {
            get { return mAddress; }
            set { mIsChanges = true; mAddress = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mIsChanges = true; mNotes = value; }
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
        public DateTime BirthDate
        {
            get { return mBirthDate; }
            set { mIsChanges = true; mBirthDate = value; }
        }
        public String SupervisorName
        {
            get { return mSupervisorName; }
            set { mIsChanges = true; mSupervisorName = value; }
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

    public partial class CTRCK_Drivers
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
        public List<CVarTRCK_Drivers> lstCVarTRCK_Drivers = new List<CVarTRCK_Drivers>();
        public List<CPKTRCK_Drivers> lstDeletedCPKTRCK_Drivers = new List<CPKTRCK_Drivers>();
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
            lstCVarTRCK_Drivers.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListTRCK_Drivers";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemTRCK_Drivers";
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
                        CVarTRCK_Drivers ObjCVarTRCK_Drivers = new CVarTRCK_Drivers();
                        ObjCVarTRCK_Drivers.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarTRCK_Drivers.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarTRCK_Drivers.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarTRCK_Drivers.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarTRCK_Drivers.mIsDriver = Convert.ToBoolean(dr["IsDriver"].ToString());
                        ObjCVarTRCK_Drivers.mPhone = Convert.ToString(dr["Phone"].ToString());
                        ObjCVarTRCK_Drivers.mMobile = Convert.ToString(dr["Mobile"].ToString());
                        ObjCVarTRCK_Drivers.mNationalIDNumber = Convert.ToString(dr["NationalIDNumber"].ToString());
                        ObjCVarTRCK_Drivers.mNationalIDExpireDate = Convert.ToDateTime(dr["NationalIDExpireDate"].ToString());
                        ObjCVarTRCK_Drivers.mLicenseNumber = Convert.ToString(dr["LicenseNumber"].ToString());
                        ObjCVarTRCK_Drivers.mLicenseNumberExpireDate = Convert.ToDateTime(dr["LicenseNumberExpireDate"].ToString());
                        ObjCVarTRCK_Drivers.mServiceStartDate = Convert.ToDateTime(dr["ServiceStartDate"].ToString());
                        ObjCVarTRCK_Drivers.mServiceEndDate = Convert.ToDateTime(dr["ServiceEndDate"].ToString());
                        ObjCVarTRCK_Drivers.mAddress = Convert.ToString(dr["Address"].ToString());
                        ObjCVarTRCK_Drivers.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarTRCK_Drivers.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarTRCK_Drivers.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarTRCK_Drivers.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarTRCK_Drivers.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarTRCK_Drivers.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarTRCK_Drivers.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarTRCK_Drivers.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarTRCK_Drivers.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarTRCK_Drivers.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarTRCK_Drivers.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarTRCK_Drivers.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarTRCK_Drivers.mSubAccountGroupID = Convert.ToInt32(dr["SubAccountGroupID"].ToString());
                        ObjCVarTRCK_Drivers.mBirthDate = Convert.ToDateTime(dr["BirthDate"].ToString());
                        ObjCVarTRCK_Drivers.mSupervisorName = Convert.ToString(dr["SupervisorName"].ToString());
                        lstCVarTRCK_Drivers.Add(ObjCVarTRCK_Drivers);
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
            lstCVarTRCK_Drivers.Clear();

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
                Com.CommandText = "[dbo].GetListPagingTRCK_Drivers";
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
                        CVarTRCK_Drivers ObjCVarTRCK_Drivers = new CVarTRCK_Drivers();
                        ObjCVarTRCK_Drivers.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarTRCK_Drivers.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarTRCK_Drivers.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarTRCK_Drivers.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarTRCK_Drivers.mIsDriver = Convert.ToBoolean(dr["IsDriver"].ToString());
                        ObjCVarTRCK_Drivers.mPhone = Convert.ToString(dr["Phone"].ToString());
                        ObjCVarTRCK_Drivers.mMobile = Convert.ToString(dr["Mobile"].ToString());
                        ObjCVarTRCK_Drivers.mNationalIDNumber = Convert.ToString(dr["NationalIDNumber"].ToString());
                        ObjCVarTRCK_Drivers.mNationalIDExpireDate = Convert.ToDateTime(dr["NationalIDExpireDate"].ToString());
                        ObjCVarTRCK_Drivers.mLicenseNumber = Convert.ToString(dr["LicenseNumber"].ToString());
                        ObjCVarTRCK_Drivers.mLicenseNumberExpireDate = Convert.ToDateTime(dr["LicenseNumberExpireDate"].ToString());
                        ObjCVarTRCK_Drivers.mServiceStartDate = Convert.ToDateTime(dr["ServiceStartDate"].ToString());
                        ObjCVarTRCK_Drivers.mServiceEndDate = Convert.ToDateTime(dr["ServiceEndDate"].ToString());
                        ObjCVarTRCK_Drivers.mAddress = Convert.ToString(dr["Address"].ToString());
                        ObjCVarTRCK_Drivers.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarTRCK_Drivers.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarTRCK_Drivers.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarTRCK_Drivers.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarTRCK_Drivers.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarTRCK_Drivers.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarTRCK_Drivers.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarTRCK_Drivers.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarTRCK_Drivers.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarTRCK_Drivers.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarTRCK_Drivers.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarTRCK_Drivers.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarTRCK_Drivers.mSubAccountGroupID = Convert.ToInt32(dr["SubAccountGroupID"].ToString());
                        ObjCVarTRCK_Drivers.mBirthDate = Convert.ToDateTime(dr["BirthDate"].ToString());
                        ObjCVarTRCK_Drivers.mSupervisorName = Convert.ToString(dr["SupervisorName"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarTRCK_Drivers.Add(ObjCVarTRCK_Drivers);
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
                    Com.CommandText = "[dbo].DeleteListTRCK_Drivers";
                else
                    Com.CommandText = "[dbo].UpdateListTRCK_Drivers";
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
        public Exception DeleteItem(List<CPKTRCK_Drivers> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemTRCK_Drivers";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                foreach (CPKTRCK_Drivers ObjCPKTRCK_Drivers in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt32(ObjCPKTRCK_Drivers.ID);
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
        public Exception SaveMethod(List<CVarTRCK_Drivers> SaveList)
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
                Com.Parameters.Add(new SqlParameter("@IsDriver", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Mobile", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@NationalIDNumber", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@NationalIDExpireDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@LicenseNumber", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@LicenseNumberExpireDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ServiceStartDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ServiceEndDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
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
                Com.Parameters.Add(new SqlParameter("@BirthDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@SupervisorName", SqlDbType.NVarChar));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarTRCK_Drivers ObjCVarTRCK_Drivers in SaveList)
                {
                    if (ObjCVarTRCK_Drivers.mIsChanges == true)
                    {
                        if (ObjCVarTRCK_Drivers.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemTRCK_Drivers";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarTRCK_Drivers.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemTRCK_Drivers";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarTRCK_Drivers.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarTRCK_Drivers.ID;
                        }
                        Com.Parameters["@Code"].Value = ObjCVarTRCK_Drivers.Code;
                        Com.Parameters["@Name"].Value = ObjCVarTRCK_Drivers.Name;
                        Com.Parameters["@LocalName"].Value = ObjCVarTRCK_Drivers.LocalName;
                        Com.Parameters["@IsDriver"].Value = ObjCVarTRCK_Drivers.IsDriver;
                        Com.Parameters["@Phone"].Value = ObjCVarTRCK_Drivers.Phone;
                        Com.Parameters["@Mobile"].Value = ObjCVarTRCK_Drivers.Mobile;
                        Com.Parameters["@NationalIDNumber"].Value = ObjCVarTRCK_Drivers.NationalIDNumber;
                        Com.Parameters["@NationalIDExpireDate"].Value = ObjCVarTRCK_Drivers.NationalIDExpireDate;
                        Com.Parameters["@LicenseNumber"].Value = ObjCVarTRCK_Drivers.LicenseNumber;
                        Com.Parameters["@LicenseNumberExpireDate"].Value = ObjCVarTRCK_Drivers.LicenseNumberExpireDate;
                        Com.Parameters["@ServiceStartDate"].Value = ObjCVarTRCK_Drivers.ServiceStartDate;
                        Com.Parameters["@ServiceEndDate"].Value = ObjCVarTRCK_Drivers.ServiceEndDate;
                        Com.Parameters["@Address"].Value = ObjCVarTRCK_Drivers.Address;
                        Com.Parameters["@Notes"].Value = ObjCVarTRCK_Drivers.Notes;
                        Com.Parameters["@IsInactive"].Value = ObjCVarTRCK_Drivers.IsInactive;
                        Com.Parameters["@CreatorUserID"].Value = ObjCVarTRCK_Drivers.CreatorUserID;
                        Com.Parameters["@CreationDate"].Value = ObjCVarTRCK_Drivers.CreationDate;
                        Com.Parameters["@ModificatorUserID"].Value = ObjCVarTRCK_Drivers.ModificatorUserID;
                        Com.Parameters["@ModificationDate"].Value = ObjCVarTRCK_Drivers.ModificationDate;
                        Com.Parameters["@LockingUserID"].Value = ObjCVarTRCK_Drivers.LockingUserID;
                        Com.Parameters["@TimeLocked"].Value = ObjCVarTRCK_Drivers.TimeLocked;
                        Com.Parameters["@IsDeleted"].Value = ObjCVarTRCK_Drivers.IsDeleted;
                        Com.Parameters["@AccountID"].Value = ObjCVarTRCK_Drivers.AccountID;
                        Com.Parameters["@SubAccountID"].Value = ObjCVarTRCK_Drivers.SubAccountID;
                        Com.Parameters["@CostCenterID"].Value = ObjCVarTRCK_Drivers.CostCenterID;
                        Com.Parameters["@SubAccountGroupID"].Value = ObjCVarTRCK_Drivers.SubAccountGroupID;
                        Com.Parameters["@BirthDate"].Value = ObjCVarTRCK_Drivers.BirthDate;
                        Com.Parameters["@SupervisorName"].Value = ObjCVarTRCK_Drivers.SupervisorName;
                        EndTrans(Com, Con);
                        if (ObjCVarTRCK_Drivers.ID == 0)
                        {
                            ObjCVarTRCK_Drivers.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarTRCK_Drivers.mIsChanges = false;
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
