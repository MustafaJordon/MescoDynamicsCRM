﻿using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.MasterData.Others.Generated
{
    [Serializable]
    public class CPKCommodities
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
    public partial class CVarCommodities : CPKCommodities
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mCode;
        internal String mName;
        internal String mLocalName;
        internal String mNotes;
        internal Boolean mIsInactive;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Int32 mLockingUserID;
        internal DateTime mTimeLocked;
        internal Boolean mIsIMO;
        internal Decimal mIMOClass;
        internal Int32 mUNNumber;
        internal String mCommercialName;
        internal String mLoadingTemperature;
        internal String mUnloadingTemperature;
        internal String mDensity;
        #endregion

        #region "Methods"
        public String Code
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
        public Boolean IsIMO
        {
            get { return mIsIMO; }
            set { mIsChanges = true; mIsIMO = value; }
        }
        public Decimal IMOClass
        {
            get { return mIMOClass; }
            set { mIsChanges = true; mIMOClass = value; }
        }
        public Int32 UNNumber
        {
            get { return mUNNumber; }
            set { mIsChanges = true; mUNNumber = value; }
        }
        public String CommercialName
        {
            get { return mCommercialName; }
            set { mIsChanges = true; mCommercialName = value; }
        }
        public String LoadingTemperature
        {
            get { return mLoadingTemperature; }
            set { mIsChanges = true; mLoadingTemperature = value; }
        }
        public String UnloadingTemperature
        {
            get { return mUnloadingTemperature; }
            set { mIsChanges = true; mUnloadingTemperature = value; }
        }
        public String Density
        {
            get { return mDensity; }
            set { mIsChanges = true; mDensity = value; }
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

    public partial class CCommodities
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
        public List<CVarCommodities> lstCVarCommodities = new List<CVarCommodities>();
        public List<CPKCommodities> lstDeletedCPKCommodities = new List<CPKCommodities>();
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
            lstCVarCommodities.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListCommodities";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemCommodities";
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
                        CVarCommodities ObjCVarCommodities = new CVarCommodities();
                        ObjCVarCommodities.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarCommodities.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarCommodities.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarCommodities.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarCommodities.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarCommodities.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarCommodities.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarCommodities.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarCommodities.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarCommodities.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarCommodities.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarCommodities.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarCommodities.mIsIMO = Convert.ToBoolean(dr["IsIMO"].ToString());
                        ObjCVarCommodities.mIMOClass = Convert.ToDecimal(dr["IMOClass"].ToString());
                        ObjCVarCommodities.mUNNumber = Convert.ToInt32(dr["UNNumber"].ToString());
                        ObjCVarCommodities.mCommercialName = Convert.ToString(dr["CommercialName"].ToString());
                        ObjCVarCommodities.mLoadingTemperature = Convert.ToString(dr["LoadingTemperature"].ToString());
                        ObjCVarCommodities.mUnloadingTemperature = Convert.ToString(dr["UnloadingTemperature"].ToString());
                        ObjCVarCommodities.mDensity = Convert.ToString(dr["Density"].ToString());
                        lstCVarCommodities.Add(ObjCVarCommodities);
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
            lstCVarCommodities.Clear();

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
                Com.CommandText = "[dbo].GetListPagingCommodities";
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
                        CVarCommodities ObjCVarCommodities = new CVarCommodities();
                        ObjCVarCommodities.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarCommodities.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarCommodities.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarCommodities.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarCommodities.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarCommodities.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarCommodities.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarCommodities.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarCommodities.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarCommodities.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarCommodities.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarCommodities.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarCommodities.mIsIMO = Convert.ToBoolean(dr["IsIMO"].ToString());
                        ObjCVarCommodities.mIMOClass = Convert.ToDecimal(dr["IMOClass"].ToString());
                        ObjCVarCommodities.mUNNumber = Convert.ToInt32(dr["UNNumber"].ToString());
                        ObjCVarCommodities.mCommercialName = Convert.ToString(dr["CommercialName"].ToString());
                        ObjCVarCommodities.mLoadingTemperature = Convert.ToString(dr["LoadingTemperature"].ToString());
                        ObjCVarCommodities.mUnloadingTemperature = Convert.ToString(dr["UnloadingTemperature"].ToString());
                        ObjCVarCommodities.mDensity = Convert.ToString(dr["Density"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarCommodities.Add(ObjCVarCommodities);
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
                    Com.CommandText = "[dbo].DeleteListCommodities";
                else
                    Com.CommandText = "[dbo].UpdateListCommodities";
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
        public Exception DeleteItem(List<CPKCommodities> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemCommodities";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                foreach (CPKCommodities ObjCPKCommodities in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt32(ObjCPKCommodities.ID);
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
        public Exception SaveMethod(List<CVarCommodities> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@Code", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@LocalName", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@IsInactive", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@CreatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ModificatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ModificationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@LockingUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@TimeLocked", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@IsIMO", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@IMOClass", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@UNNumber", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CommercialName", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@LoadingTemperature", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@UnloadingTemperature", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Density", SqlDbType.NVarChar));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarCommodities ObjCVarCommodities in SaveList)
                {
                    if (ObjCVarCommodities.mIsChanges == true)
                    {
                        if (ObjCVarCommodities.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemCommodities";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarCommodities.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemCommodities";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarCommodities.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarCommodities.ID;
                        }
                        Com.Parameters["@Code"].Value = ObjCVarCommodities.Code;
                        Com.Parameters["@Name"].Value = ObjCVarCommodities.Name;
                        Com.Parameters["@LocalName"].Value = ObjCVarCommodities.LocalName;
                        Com.Parameters["@Notes"].Value = ObjCVarCommodities.Notes;
                        Com.Parameters["@IsInactive"].Value = ObjCVarCommodities.IsInactive;
                        Com.Parameters["@CreatorUserID"].Value = ObjCVarCommodities.CreatorUserID;
                        Com.Parameters["@CreationDate"].Value = ObjCVarCommodities.CreationDate;
                        Com.Parameters["@ModificatorUserID"].Value = ObjCVarCommodities.ModificatorUserID;
                        Com.Parameters["@ModificationDate"].Value = ObjCVarCommodities.ModificationDate;
                        Com.Parameters["@LockingUserID"].Value = ObjCVarCommodities.LockingUserID;
                        Com.Parameters["@TimeLocked"].Value = ObjCVarCommodities.TimeLocked;
                        Com.Parameters["@IsIMO"].Value = ObjCVarCommodities.IsIMO;
                        Com.Parameters["@IMOClass"].Value = ObjCVarCommodities.IMOClass;
                        Com.Parameters["@UNNumber"].Value = ObjCVarCommodities.UNNumber;
                        Com.Parameters["@CommercialName"].Value = ObjCVarCommodities.CommercialName;
                        Com.Parameters["@LoadingTemperature"].Value = ObjCVarCommodities.LoadingTemperature;
                        Com.Parameters["@UnloadingTemperature"].Value = ObjCVarCommodities.UnloadingTemperature;
                        Com.Parameters["@Density"].Value = ObjCVarCommodities.Density;
                        EndTrans(Com, Con);
                        if (ObjCVarCommodities.ID == 0)
                        {
                            ObjCVarCommodities.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarCommodities.mIsChanges = false;
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
