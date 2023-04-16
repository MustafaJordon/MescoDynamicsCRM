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
    public class CPKContainerTypes
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
    public partial class CVarContainerTypes : CPKContainerTypes
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mCode;
        internal String mName;
        internal String mLocalName;
        internal String mISOCode;
        internal String mPrintAs;
        internal Int32 mCTypeID;
        internal Int32 mCSizeID;
        internal String mNotes;
        internal Boolean mIsAddedManually;
        internal Boolean mIsInactive;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Int32 mLockingUserID;
        internal DateTime mTimeLocked;
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
        public String ISOCode
        {
            get { return mISOCode; }
            set { mIsChanges = true; mISOCode = value; }
        }
        public String PrintAs
        {
            get { return mPrintAs; }
            set { mIsChanges = true; mPrintAs = value; }
        }
        public Int32 CTypeID
        {
            get { return mCTypeID; }
            set { mIsChanges = true; mCTypeID = value; }
        }
        public Int32 CSizeID
        {
            get { return mCSizeID; }
            set { mIsChanges = true; mCSizeID = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mIsChanges = true; mNotes = value; }
        }
        public Boolean IsAddedManually
        {
            get { return mIsAddedManually; }
            set { mIsChanges = true; mIsAddedManually = value; }
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

    public partial class CContainerTypes
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
        public List<CVarContainerTypes> lstCVarContainerTypes = new List<CVarContainerTypes>();
        public List<CPKContainerTypes> lstDeletedCPKContainerTypes = new List<CPKContainerTypes>();
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
            lstCVarContainerTypes.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListContainerTypes";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemContainerTypes";
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
                        CVarContainerTypes ObjCVarContainerTypes = new CVarContainerTypes();
                        ObjCVarContainerTypes.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarContainerTypes.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarContainerTypes.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarContainerTypes.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarContainerTypes.mISOCode = Convert.ToString(dr["ISOCode"].ToString());
                        ObjCVarContainerTypes.mPrintAs = Convert.ToString(dr["PrintAs"].ToString());
                        ObjCVarContainerTypes.mCTypeID = Convert.ToInt32(dr["CTypeID"].ToString());
                        ObjCVarContainerTypes.mCSizeID = Convert.ToInt32(dr["CSizeID"].ToString());
                        ObjCVarContainerTypes.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarContainerTypes.mIsAddedManually = Convert.ToBoolean(dr["IsAddedManually"].ToString());
                        ObjCVarContainerTypes.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarContainerTypes.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarContainerTypes.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarContainerTypes.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarContainerTypes.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarContainerTypes.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarContainerTypes.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        lstCVarContainerTypes.Add(ObjCVarContainerTypes);
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
            lstCVarContainerTypes.Clear();

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
                Com.CommandText = "[dbo].GetListPagingContainerTypes";
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
                        CVarContainerTypes ObjCVarContainerTypes = new CVarContainerTypes();
                        ObjCVarContainerTypes.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarContainerTypes.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarContainerTypes.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarContainerTypes.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarContainerTypes.mISOCode = Convert.ToString(dr["ISOCode"].ToString());
                        ObjCVarContainerTypes.mPrintAs = Convert.ToString(dr["PrintAs"].ToString());
                        ObjCVarContainerTypes.mCTypeID = Convert.ToInt32(dr["CTypeID"].ToString());
                        ObjCVarContainerTypes.mCSizeID = Convert.ToInt32(dr["CSizeID"].ToString());
                        ObjCVarContainerTypes.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarContainerTypes.mIsAddedManually = Convert.ToBoolean(dr["IsAddedManually"].ToString());
                        ObjCVarContainerTypes.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarContainerTypes.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarContainerTypes.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarContainerTypes.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarContainerTypes.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarContainerTypes.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarContainerTypes.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarContainerTypes.Add(ObjCVarContainerTypes);
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
                    Com.CommandText = "[dbo].DeleteListContainerTypes";
                else
                    Com.CommandText = "[dbo].UpdateListContainerTypes";
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
        public Exception DeleteItem(List<CPKContainerTypes> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemContainerTypes";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                foreach (CPKContainerTypes ObjCPKContainerTypes in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt32(ObjCPKContainerTypes.ID);
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
        public Exception SaveMethod(List<CVarContainerTypes> SaveList)
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
                Com.Parameters.Add(new SqlParameter("@ISOCode", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@PrintAs", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@CTypeID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CSizeID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@IsAddedManually", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@IsInactive", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@CreatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ModificatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ModificationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@LockingUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@TimeLocked", SqlDbType.DateTime));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarContainerTypes ObjCVarContainerTypes in SaveList)
                {
                    if (ObjCVarContainerTypes.mIsChanges == true)
                    {
                        if (ObjCVarContainerTypes.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemContainerTypes";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarContainerTypes.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemContainerTypes";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarContainerTypes.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarContainerTypes.ID;
                        }
                        Com.Parameters["@Code"].Value = ObjCVarContainerTypes.Code;
                        Com.Parameters["@Name"].Value = ObjCVarContainerTypes.Name;
                        Com.Parameters["@LocalName"].Value = ObjCVarContainerTypes.LocalName;
                        Com.Parameters["@ISOCode"].Value = ObjCVarContainerTypes.ISOCode;
                        Com.Parameters["@PrintAs"].Value = ObjCVarContainerTypes.PrintAs;
                        Com.Parameters["@CTypeID"].Value = ObjCVarContainerTypes.CTypeID;
                        Com.Parameters["@CSizeID"].Value = ObjCVarContainerTypes.CSizeID;
                        Com.Parameters["@Notes"].Value = ObjCVarContainerTypes.Notes;
                        Com.Parameters["@IsAddedManually"].Value = ObjCVarContainerTypes.IsAddedManually;
                        Com.Parameters["@IsInactive"].Value = ObjCVarContainerTypes.IsInactive;
                        Com.Parameters["@CreatorUserID"].Value = ObjCVarContainerTypes.CreatorUserID;
                        Com.Parameters["@CreationDate"].Value = ObjCVarContainerTypes.CreationDate;
                        Com.Parameters["@ModificatorUserID"].Value = ObjCVarContainerTypes.ModificatorUserID;
                        Com.Parameters["@ModificationDate"].Value = ObjCVarContainerTypes.ModificationDate;
                        Com.Parameters["@LockingUserID"].Value = ObjCVarContainerTypes.LockingUserID;
                        Com.Parameters["@TimeLocked"].Value = ObjCVarContainerTypes.TimeLocked;
                        EndTrans(Com, Con);
                        if (ObjCVarContainerTypes.ID == 0)
                        {
                            ObjCVarContainerTypes.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarContainerTypes.mIsChanges = false;
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
