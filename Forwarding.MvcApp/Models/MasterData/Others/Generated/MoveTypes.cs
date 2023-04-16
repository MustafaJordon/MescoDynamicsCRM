﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.MasterData.Others.Generated
{
    [Serializable]
    public class CPKMoveTypes
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
    public partial class CVarMoveTypes : CPKMoveTypes
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mCode;
        internal String mName;
        internal String mLocalName;
        internal String mIconName;
        internal String mIconStyle;
        internal String mImageURL;
        internal Boolean mIsOcean;
        internal Boolean mIsAir;
        internal Boolean mIsInland;
        internal Boolean mIsInactive;
        internal String mNotes;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Int32 mLockingUserID;
        internal DateTime mTimeLocked;
        internal Boolean mIsCustomsClearance;
        internal Boolean mIsWarehousing;
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
        public String IconName
        {
            get { return mIconName; }
            set { mIsChanges = true; mIconName = value; }
        }
        public String IconStyle
        {
            get { return mIconStyle; }
            set { mIsChanges = true; mIconStyle = value; }
        }
        public String ImageURL
        {
            get { return mImageURL; }
            set { mIsChanges = true; mImageURL = value; }
        }
        public Boolean IsOcean
        {
            get { return mIsOcean; }
            set { mIsChanges = true; mIsOcean = value; }
        }
        public Boolean IsAir
        {
            get { return mIsAir; }
            set { mIsChanges = true; mIsAir = value; }
        }
        public Boolean IsInland
        {
            get { return mIsInland; }
            set { mIsChanges = true; mIsInland = value; }
        }
        public Boolean IsInactive
        {
            get { return mIsInactive; }
            set { mIsChanges = true; mIsInactive = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mIsChanges = true; mNotes = value; }
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
        public Boolean IsCustomsClearance
        {
            get { return mIsCustomsClearance; }
            set { mIsChanges = true; mIsCustomsClearance = value; }
        }
        public Boolean IsWarehousing
        {
            get { return mIsWarehousing; }
            set { mIsChanges = true; mIsWarehousing = value; }
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

    public partial class CMoveTypes
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
        public List<CVarMoveTypes> lstCVarMoveTypes = new List<CVarMoveTypes>();
        public List<CPKMoveTypes> lstDeletedCPKMoveTypes = new List<CPKMoveTypes>();
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
            lstCVarMoveTypes.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListMoveTypes";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemMoveTypes";
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
                        CVarMoveTypes ObjCVarMoveTypes = new CVarMoveTypes();
                        ObjCVarMoveTypes.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarMoveTypes.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarMoveTypes.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarMoveTypes.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarMoveTypes.mIconName = Convert.ToString(dr["IconName"].ToString());
                        ObjCVarMoveTypes.mIconStyle = Convert.ToString(dr["IconStyle"].ToString());
                        ObjCVarMoveTypes.mImageURL = Convert.ToString(dr["ImageURL"].ToString());
                        ObjCVarMoveTypes.mIsOcean = Convert.ToBoolean(dr["IsOcean"].ToString());
                        ObjCVarMoveTypes.mIsAir = Convert.ToBoolean(dr["IsAir"].ToString());
                        ObjCVarMoveTypes.mIsInland = Convert.ToBoolean(dr["IsInland"].ToString());
                        ObjCVarMoveTypes.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarMoveTypes.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarMoveTypes.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarMoveTypes.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarMoveTypes.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarMoveTypes.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarMoveTypes.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarMoveTypes.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarMoveTypes.mIsCustomsClearance = Convert.ToBoolean(dr["IsCustomsClearance"].ToString());
                        ObjCVarMoveTypes.mIsWarehousing = Convert.ToBoolean(dr["IsWarehousing"].ToString());
                        lstCVarMoveTypes.Add(ObjCVarMoveTypes);
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
            lstCVarMoveTypes.Clear();

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
                Com.CommandText = "[dbo].GetListPagingMoveTypes";
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
                        CVarMoveTypes ObjCVarMoveTypes = new CVarMoveTypes();
                        ObjCVarMoveTypes.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarMoveTypes.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarMoveTypes.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarMoveTypes.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarMoveTypes.mIconName = Convert.ToString(dr["IconName"].ToString());
                        ObjCVarMoveTypes.mIconStyle = Convert.ToString(dr["IconStyle"].ToString());
                        ObjCVarMoveTypes.mImageURL = Convert.ToString(dr["ImageURL"].ToString());
                        ObjCVarMoveTypes.mIsOcean = Convert.ToBoolean(dr["IsOcean"].ToString());
                        ObjCVarMoveTypes.mIsAir = Convert.ToBoolean(dr["IsAir"].ToString());
                        ObjCVarMoveTypes.mIsInland = Convert.ToBoolean(dr["IsInland"].ToString());
                        ObjCVarMoveTypes.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarMoveTypes.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarMoveTypes.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarMoveTypes.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarMoveTypes.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarMoveTypes.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarMoveTypes.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarMoveTypes.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarMoveTypes.mIsCustomsClearance = Convert.ToBoolean(dr["IsCustomsClearance"].ToString());
                        ObjCVarMoveTypes.mIsWarehousing = Convert.ToBoolean(dr["IsWarehousing"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarMoveTypes.Add(ObjCVarMoveTypes);
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
                    Com.CommandText = "[dbo].DeleteListMoveTypes";
                else
                    Com.CommandText = "[dbo].UpdateListMoveTypes";
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
        public Exception DeleteItem(List<CPKMoveTypes> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemMoveTypes";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                foreach (CPKMoveTypes ObjCPKMoveTypes in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt32(ObjCPKMoveTypes.ID);
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
        public Exception SaveMethod(List<CVarMoveTypes> SaveList)
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
                Com.Parameters.Add(new SqlParameter("@IconName", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@IconStyle", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@ImageURL", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@IsOcean", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@IsAir", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@IsInland", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@IsInactive", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@CreatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ModificatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ModificationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@LockingUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@TimeLocked", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@IsCustomsClearance", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@IsWarehousing", SqlDbType.Bit));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarMoveTypes ObjCVarMoveTypes in SaveList)
                {
                    if (ObjCVarMoveTypes.mIsChanges == true)
                    {
                        if (ObjCVarMoveTypes.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemMoveTypes";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarMoveTypes.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemMoveTypes";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarMoveTypes.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarMoveTypes.ID;
                        }
                        Com.Parameters["@Code"].Value = ObjCVarMoveTypes.Code;
                        Com.Parameters["@Name"].Value = ObjCVarMoveTypes.Name;
                        Com.Parameters["@LocalName"].Value = ObjCVarMoveTypes.LocalName;
                        Com.Parameters["@IconName"].Value = ObjCVarMoveTypes.IconName;
                        Com.Parameters["@IconStyle"].Value = ObjCVarMoveTypes.IconStyle;
                        Com.Parameters["@ImageURL"].Value = ObjCVarMoveTypes.ImageURL;
                        Com.Parameters["@IsOcean"].Value = ObjCVarMoveTypes.IsOcean;
                        Com.Parameters["@IsAir"].Value = ObjCVarMoveTypes.IsAir;
                        Com.Parameters["@IsInland"].Value = ObjCVarMoveTypes.IsInland;
                        Com.Parameters["@IsInactive"].Value = ObjCVarMoveTypes.IsInactive;
                        Com.Parameters["@Notes"].Value = ObjCVarMoveTypes.Notes;
                        Com.Parameters["@CreatorUserID"].Value = ObjCVarMoveTypes.CreatorUserID;
                        Com.Parameters["@CreationDate"].Value = ObjCVarMoveTypes.CreationDate;
                        Com.Parameters["@ModificatorUserID"].Value = ObjCVarMoveTypes.ModificatorUserID;
                        Com.Parameters["@ModificationDate"].Value = ObjCVarMoveTypes.ModificationDate;
                        Com.Parameters["@LockingUserID"].Value = ObjCVarMoveTypes.LockingUserID;
                        Com.Parameters["@TimeLocked"].Value = ObjCVarMoveTypes.TimeLocked;
                        Com.Parameters["@IsCustomsClearance"].Value = ObjCVarMoveTypes.IsCustomsClearance;
                        Com.Parameters["@IsWarehousing"].Value = ObjCVarMoveTypes.IsWarehousing;
                        EndTrans(Com, Con);
                        if (ObjCVarMoveTypes.ID == 0)
                        {
                            ObjCVarMoveTypes.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarMoveTypes.mIsChanges = false;
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
