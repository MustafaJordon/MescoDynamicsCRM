﻿using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace Forwarding.MvcApp.Models.MasterData.BanksAccountsAndTreasuries.Generated
{
    [Serializable]
    public class CPKCustody
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
    public partial class CVarCustody : CPKCustody
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mCode;
        internal String mName;
        internal String mLocalName;
        internal String mJob;
        internal String mAddress;
        internal String mMobile;
        internal String mPhone;
        internal String mNotes;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Int32 mAccountID;
        internal Int32 mSubAccountID;
        internal Int32 mCostCenterID;
        internal Int32 mSubAccountGroupID;
        internal Int32 mUserID;
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
        public String Job
        {
            get { return mJob; }
            set { mIsChanges = true; mJob = value; }
        }
        public String Address
        {
            get { return mAddress; }
            set { mIsChanges = true; mAddress = value; }
        }
        public String Mobile
        {
            get { return mMobile; }
            set { mIsChanges = true; mMobile = value; }
        }
        public String Phone
        {
            get { return mPhone; }
            set { mIsChanges = true; mPhone = value; }
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
        public Int32 UserID
        {
            get { return mUserID; }
            set { mIsChanges = true; mUserID = value; }
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

    public partial class CCustody
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
        public List<CVarCustody> lstCVarCustody = new List<CVarCustody>();
        public List<CPKCustody> lstDeletedCPKCustody = new List<CPKCustody>();
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
            lstCVarCustody.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListCustody";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemCustody";
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
                        CVarCustody ObjCVarCustody = new CVarCustody();
                        ObjCVarCustody.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarCustody.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarCustody.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarCustody.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarCustody.mJob = Convert.ToString(dr["Job"].ToString());
                        ObjCVarCustody.mAddress = Convert.ToString(dr["Address"].ToString());
                        ObjCVarCustody.mMobile = Convert.ToString(dr["Mobile"].ToString());
                        ObjCVarCustody.mPhone = Convert.ToString(dr["Phone"].ToString());
                        ObjCVarCustody.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarCustody.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarCustody.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarCustody.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarCustody.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarCustody.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarCustody.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarCustody.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarCustody.mSubAccountGroupID = Convert.ToInt32(dr["SubAccountGroupID"].ToString());
                        ObjCVarCustody.mUserID = Convert.ToInt32(dr["UserID"].ToString());
                        lstCVarCustody.Add(ObjCVarCustody);
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
            lstCVarCustody.Clear();

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
                Com.CommandText = "[dbo].GetListPagingCustody";
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
                        CVarCustody ObjCVarCustody = new CVarCustody();
                        ObjCVarCustody.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarCustody.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarCustody.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarCustody.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarCustody.mJob = Convert.ToString(dr["Job"].ToString());
                        ObjCVarCustody.mAddress = Convert.ToString(dr["Address"].ToString());
                        ObjCVarCustody.mMobile = Convert.ToString(dr["Mobile"].ToString());
                        ObjCVarCustody.mPhone = Convert.ToString(dr["Phone"].ToString());
                        ObjCVarCustody.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarCustody.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarCustody.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarCustody.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarCustody.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarCustody.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarCustody.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarCustody.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarCustody.mSubAccountGroupID = Convert.ToInt32(dr["SubAccountGroupID"].ToString());
                        ObjCVarCustody.mUserID = Convert.ToInt32(dr["UserID"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarCustody.Add(ObjCVarCustody);
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
                    Com.CommandText = "[dbo].DeleteListCustody";
                else
                    Com.CommandText = "[dbo].UpdateListCustody";
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
        public Exception DeleteItem(List<CPKCustody> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemCustody";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                foreach (CPKCustody ObjCPKCustody in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt32(ObjCPKCustody.ID);
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
        public Exception SaveMethod(List<CVarCustody> SaveList)
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
                Com.Parameters.Add(new SqlParameter("@Job", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Mobile", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@CreatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ModificatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ModificationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@AccountID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@SubAccountID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CostCenterID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@SubAccountGroupID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarCustody ObjCVarCustody in SaveList)
                {
                    if (ObjCVarCustody.mIsChanges == true)
                    {
                        if (ObjCVarCustody.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemCustody";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarCustody.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemCustody";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarCustody.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarCustody.ID;
                        }
                        Com.Parameters["@Code"].Value = ObjCVarCustody.Code;
                        Com.Parameters["@Name"].Value = ObjCVarCustody.Name;
                        Com.Parameters["@LocalName"].Value = ObjCVarCustody.LocalName;
                        Com.Parameters["@Job"].Value = ObjCVarCustody.Job;
                        Com.Parameters["@Address"].Value = ObjCVarCustody.Address;
                        Com.Parameters["@Mobile"].Value = ObjCVarCustody.Mobile;
                        Com.Parameters["@Phone"].Value = ObjCVarCustody.Phone;
                        Com.Parameters["@Notes"].Value = ObjCVarCustody.Notes;
                        Com.Parameters["@CreatorUserID"].Value = ObjCVarCustody.CreatorUserID;
                        Com.Parameters["@CreationDate"].Value = ObjCVarCustody.CreationDate;
                        Com.Parameters["@ModificatorUserID"].Value = ObjCVarCustody.ModificatorUserID;
                        Com.Parameters["@ModificationDate"].Value = ObjCVarCustody.ModificationDate;
                        Com.Parameters["@AccountID"].Value = ObjCVarCustody.AccountID;
                        Com.Parameters["@SubAccountID"].Value = ObjCVarCustody.SubAccountID;
                        Com.Parameters["@CostCenterID"].Value = ObjCVarCustody.CostCenterID;
                        Com.Parameters["@SubAccountGroupID"].Value = ObjCVarCustody.SubAccountGroupID;
                        Com.Parameters["@UserID"].Value = ObjCVarCustody.UserID;
                        EndTrans(Com, Con);
                        if (ObjCVarCustody.ID == 0)
                        {
                            ObjCVarCustody.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarCustody.mIsChanges = false;
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
