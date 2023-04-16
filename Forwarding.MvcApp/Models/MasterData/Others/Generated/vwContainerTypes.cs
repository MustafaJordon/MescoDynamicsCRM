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
    public class CPKvwContainerTypes
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
    public partial class CVarvwContainerTypes : CPKvwContainerTypes
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mCode;
        internal String mName;
        internal String mLocalName;
        internal String mISOCode;
        internal String mPrintAs;
        internal String mNotes;
        internal Int32 mCTypeID;
        internal Int32 mCSizeID;
        internal Byte mTeus;
        internal Boolean mIsInactive;
        internal Boolean mIsAddedManually;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Int32 mLockingUserID;
        internal DateTime mTimeLocked;
        internal String mCTypeCode;
        internal String mCSizeCode;
        #endregion

        #region "Methods"
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
        public String ISOCode
        {
            get { return mISOCode; }
            set { mISOCode = value; }
        }
        public String PrintAs
        {
            get { return mPrintAs; }
            set { mPrintAs = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mNotes = value; }
        }
        public Int32 CTypeID
        {
            get { return mCTypeID; }
            set { mCTypeID = value; }
        }
        public Int32 CSizeID
        {
            get { return mCSizeID; }
            set { mCSizeID = value; }
        }
        public Byte Teus
        {
            get { return mTeus; }
            set { mTeus = value; }
        }
        public Boolean IsInactive
        {
            get { return mIsInactive; }
            set { mIsInactive = value; }
        }
        public Boolean IsAddedManually
        {
            get { return mIsAddedManually; }
            set { mIsAddedManually = value; }
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
        public Int32 LockingUserID
        {
            get { return mLockingUserID; }
            set { mLockingUserID = value; }
        }
        public DateTime TimeLocked
        {
            get { return mTimeLocked; }
            set { mTimeLocked = value; }
        }
        public String CTypeCode
        {
            get { return mCTypeCode; }
            set { mCTypeCode = value; }
        }
        public String CSizeCode
        {
            get { return mCSizeCode; }
            set { mCSizeCode = value; }
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

    public partial class CvwContainerTypes
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
        public List<CVarvwContainerTypes> lstCVarvwContainerTypes = new List<CVarvwContainerTypes>();
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
            lstCVarvwContainerTypes.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwContainerTypes";
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
                        CVarvwContainerTypes ObjCVarvwContainerTypes = new CVarvwContainerTypes();
                        ObjCVarvwContainerTypes.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwContainerTypes.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwContainerTypes.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwContainerTypes.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwContainerTypes.mISOCode = Convert.ToString(dr["ISOCode"].ToString());
                        ObjCVarvwContainerTypes.mPrintAs = Convert.ToString(dr["PrintAs"].ToString());
                        ObjCVarvwContainerTypes.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwContainerTypes.mCTypeID = Convert.ToInt32(dr["CTypeID"].ToString());
                        ObjCVarvwContainerTypes.mCSizeID = Convert.ToInt32(dr["CSizeID"].ToString());
                        ObjCVarvwContainerTypes.mTeus = Convert.ToByte(dr["Teus"].ToString());
                        ObjCVarvwContainerTypes.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarvwContainerTypes.mIsAddedManually = Convert.ToBoolean(dr["IsAddedManually"].ToString());
                        ObjCVarvwContainerTypes.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwContainerTypes.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwContainerTypes.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwContainerTypes.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwContainerTypes.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarvwContainerTypes.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarvwContainerTypes.mCTypeCode = Convert.ToString(dr["CTypeCode"].ToString());
                        ObjCVarvwContainerTypes.mCSizeCode = Convert.ToString(dr["CSizeCode"].ToString());
                        lstCVarvwContainerTypes.Add(ObjCVarvwContainerTypes);
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
            lstCVarvwContainerTypes.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwContainerTypes";
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
                        CVarvwContainerTypes ObjCVarvwContainerTypes = new CVarvwContainerTypes();
                        ObjCVarvwContainerTypes.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwContainerTypes.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwContainerTypes.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwContainerTypes.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwContainerTypes.mISOCode = Convert.ToString(dr["ISOCode"].ToString());
                        ObjCVarvwContainerTypes.mPrintAs = Convert.ToString(dr["PrintAs"].ToString());
                        ObjCVarvwContainerTypes.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwContainerTypes.mCTypeID = Convert.ToInt32(dr["CTypeID"].ToString());
                        ObjCVarvwContainerTypes.mCSizeID = Convert.ToInt32(dr["CSizeID"].ToString());
                        ObjCVarvwContainerTypes.mTeus = Convert.ToByte(dr["Teus"].ToString());
                        ObjCVarvwContainerTypes.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarvwContainerTypes.mIsAddedManually = Convert.ToBoolean(dr["IsAddedManually"].ToString());
                        ObjCVarvwContainerTypes.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwContainerTypes.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwContainerTypes.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwContainerTypes.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwContainerTypes.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarvwContainerTypes.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarvwContainerTypes.mCTypeCode = Convert.ToString(dr["CTypeCode"].ToString());
                        ObjCVarvwContainerTypes.mCSizeCode = Convert.ToString(dr["CSizeCode"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwContainerTypes.Add(ObjCVarvwContainerTypes);
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
