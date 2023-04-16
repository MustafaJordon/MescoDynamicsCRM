﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.CRM.CRM_ContactPersons.Generated
{
    [Serializable]
    public class CPKCRM_ContactPersons
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
    public partial class CVarCRM_ContactPersons : CPKCRM_ContactPersons
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mNameEn;
        internal String mNameAr;
        internal String mCellPhone;
        internal String mTelephone;
        internal String mExtensionNo;
        internal String mEmail;
        internal String mPersonalPhone;
        internal String mPersonalEmail;
        internal String mPosition;
        internal Int32 mIndustrySector;
        internal Boolean mIsKeyPerson;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificationUserID;
        internal DateTime mModificationDate;
        internal Int32 mCRM_ClientsID;
        internal Int32 mIsActualCustomer;
        #endregion

        #region "Methods"
        public String NameEn
        {
            get { return mNameEn; }
            set { mIsChanges = true; mNameEn = value; }
        }
        public String NameAr
        {
            get { return mNameAr; }
            set { mIsChanges = true; mNameAr = value; }
        }
        public String CellPhone
        {
            get { return mCellPhone; }
            set { mIsChanges = true; mCellPhone = value; }
        }
        public String Telephone
        {
            get { return mTelephone; }
            set { mIsChanges = true; mTelephone = value; }
        }
        public String ExtensionNo
        {
            get { return mExtensionNo; }
            set { mIsChanges = true; mExtensionNo = value; }
        }
        public String Email
        {
            get { return mEmail; }
            set { mIsChanges = true; mEmail = value; }
        }
        public String PersonalPhone
        {
            get { return mPersonalPhone; }
            set { mIsChanges = true; mPersonalPhone = value; }
        }
        public String PersonalEmail
        {
            get { return mPersonalEmail; }
            set { mIsChanges = true; mPersonalEmail = value; }
        }
        public String Position
        {
            get { return mPosition; }
            set { mIsChanges = true; mPosition = value; }
        }
        public Int32 IndustrySector
        {
            get { return mIndustrySector; }
            set { mIsChanges = true; mIndustrySector = value; }
        }
        public Boolean IsKeyPerson
        {
            get { return mIsKeyPerson; }
            set { mIsChanges = true; mIsKeyPerson = value; }
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
        public Int32 ModificationUserID
        {
            get { return mModificationUserID; }
            set { mIsChanges = true; mModificationUserID = value; }
        }
        public DateTime ModificationDate
        {
            get { return mModificationDate; }
            set { mIsChanges = true; mModificationDate = value; }
        }
        public Int32 CRM_ClientsID
        {
            get { return mCRM_ClientsID; }
            set { mIsChanges = true; mCRM_ClientsID = value; }
        }
        public Int32 IsActualCustomer
        {
            get { return mIsActualCustomer; }
            set { mIsChanges = true; mIsActualCustomer = value; }
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

    public partial class CCRM_ContactPersons
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
        public List<CVarCRM_ContactPersons> lstCVarCRM_ContactPersons = new List<CVarCRM_ContactPersons>();
        public List<CPKCRM_ContactPersons> lstDeletedCPKCRM_ContactPersons = new List<CPKCRM_ContactPersons>();
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
            lstCVarCRM_ContactPersons.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.VarChar));
                    Com.CommandText = "[dbo].GetListCRM_ContactPersons";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemCRM_ContactPersons";
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
                        CVarCRM_ContactPersons ObjCVarCRM_ContactPersons = new CVarCRM_ContactPersons();
                        ObjCVarCRM_ContactPersons.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarCRM_ContactPersons.mNameEn = Convert.ToString(dr["NameEn"].ToString());
                        ObjCVarCRM_ContactPersons.mNameAr = Convert.ToString(dr["NameAr"].ToString());
                        ObjCVarCRM_ContactPersons.mCellPhone = Convert.ToString(dr["CellPhone"].ToString());
                        ObjCVarCRM_ContactPersons.mTelephone = Convert.ToString(dr["Telephone"].ToString());
                        ObjCVarCRM_ContactPersons.mExtensionNo = Convert.ToString(dr["ExtensionNo"].ToString());
                        ObjCVarCRM_ContactPersons.mEmail = Convert.ToString(dr["Email"].ToString());
                        ObjCVarCRM_ContactPersons.mPersonalPhone = Convert.ToString(dr["PersonalPhone"].ToString());
                        ObjCVarCRM_ContactPersons.mPersonalEmail = Convert.ToString(dr["PersonalEmail"].ToString());
                        ObjCVarCRM_ContactPersons.mPosition = Convert.ToString(dr["Position"].ToString());
                        ObjCVarCRM_ContactPersons.mIndustrySector = Convert.ToInt32(dr["IndustrySector"].ToString());
                        ObjCVarCRM_ContactPersons.mIsKeyPerson = Convert.ToBoolean(dr["IsKeyPerson"].ToString());
                        ObjCVarCRM_ContactPersons.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarCRM_ContactPersons.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarCRM_ContactPersons.mModificationUserID = Convert.ToInt32(dr["ModificationUserID"].ToString());
                        ObjCVarCRM_ContactPersons.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarCRM_ContactPersons.mCRM_ClientsID = Convert.ToInt32(dr["CRM_ClientsID"].ToString());
                        ObjCVarCRM_ContactPersons.mIsActualCustomer = Convert.ToInt32(dr["IsActualCustomer"].ToString());
                        lstCVarCRM_ContactPersons.Add(ObjCVarCRM_ContactPersons);
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
            lstCVarCRM_ContactPersons.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                Com.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@PageNumber", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.VarChar));
                Com.Parameters.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar));
                Com.CommandText = "[dbo].GetListPagingCRM_ContactPersons";
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
                        CVarCRM_ContactPersons ObjCVarCRM_ContactPersons = new CVarCRM_ContactPersons();
                        ObjCVarCRM_ContactPersons.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarCRM_ContactPersons.mNameEn = Convert.ToString(dr["NameEn"].ToString());
                        ObjCVarCRM_ContactPersons.mNameAr = Convert.ToString(dr["NameAr"].ToString());
                        ObjCVarCRM_ContactPersons.mCellPhone = Convert.ToString(dr["CellPhone"].ToString());
                        ObjCVarCRM_ContactPersons.mTelephone = Convert.ToString(dr["Telephone"].ToString());
                        ObjCVarCRM_ContactPersons.mExtensionNo = Convert.ToString(dr["ExtensionNo"].ToString());
                        ObjCVarCRM_ContactPersons.mEmail = Convert.ToString(dr["Email"].ToString());
                        ObjCVarCRM_ContactPersons.mPersonalPhone = Convert.ToString(dr["PersonalPhone"].ToString());
                        ObjCVarCRM_ContactPersons.mPersonalEmail = Convert.ToString(dr["PersonalEmail"].ToString());
                        ObjCVarCRM_ContactPersons.mPosition = Convert.ToString(dr["Position"].ToString());
                        ObjCVarCRM_ContactPersons.mIndustrySector = Convert.ToInt32(dr["IndustrySector"].ToString());
                        ObjCVarCRM_ContactPersons.mIsKeyPerson = Convert.ToBoolean(dr["IsKeyPerson"].ToString());
                        ObjCVarCRM_ContactPersons.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarCRM_ContactPersons.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarCRM_ContactPersons.mModificationUserID = Convert.ToInt32(dr["ModificationUserID"].ToString());
                        ObjCVarCRM_ContactPersons.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarCRM_ContactPersons.mCRM_ClientsID = Convert.ToInt32(dr["CRM_ClientsID"].ToString());
                        ObjCVarCRM_ContactPersons.mIsActualCustomer = Convert.ToInt32(dr["IsActualCustomer"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarCRM_ContactPersons.Add(ObjCVarCRM_ContactPersons);
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
                    Com.CommandText = "[dbo].DeleteListCRM_ContactPersons";
                else
                    Com.CommandText = "[dbo].UpdateListCRM_ContactPersons";
                Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.VarChar));
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
        public Exception DeleteItem(List<CPKCRM_ContactPersons> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemCRM_ContactPersons";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                foreach (CPKCRM_ContactPersons ObjCPKCRM_ContactPersons in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt32(ObjCPKCRM_ContactPersons.ID);
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
        public Exception SaveMethod(List<CVarCRM_ContactPersons> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@NameEn", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@NameAr", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@CellPhone", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Telephone", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@ExtensionNo", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@PersonalPhone", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@PersonalEmail", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Position", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@IndustrySector", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@IsKeyPerson", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@CreatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ModificationUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ModificationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@CRM_ClientsID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@IsActualCustomer", SqlDbType.Int));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarCRM_ContactPersons ObjCVarCRM_ContactPersons in SaveList)
                {
                    if (ObjCVarCRM_ContactPersons.mIsChanges == true)
                    {
                        if (ObjCVarCRM_ContactPersons.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemCRM_ContactPersons";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarCRM_ContactPersons.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemCRM_ContactPersons";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarCRM_ContactPersons.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarCRM_ContactPersons.ID;
                        }
                        Com.Parameters["@NameEn"].Value = ObjCVarCRM_ContactPersons.NameEn;
                        Com.Parameters["@NameAr"].Value = ObjCVarCRM_ContactPersons.NameAr;
                        Com.Parameters["@CellPhone"].Value = ObjCVarCRM_ContactPersons.CellPhone;
                        Com.Parameters["@Telephone"].Value = ObjCVarCRM_ContactPersons.Telephone;
                        Com.Parameters["@ExtensionNo"].Value = ObjCVarCRM_ContactPersons.ExtensionNo;
                        Com.Parameters["@Email"].Value = ObjCVarCRM_ContactPersons.Email;
                        Com.Parameters["@PersonalPhone"].Value = ObjCVarCRM_ContactPersons.PersonalPhone;
                        Com.Parameters["@PersonalEmail"].Value = ObjCVarCRM_ContactPersons.PersonalEmail;
                        Com.Parameters["@Position"].Value = ObjCVarCRM_ContactPersons.Position;
                        Com.Parameters["@IndustrySector"].Value = ObjCVarCRM_ContactPersons.IndustrySector;
                        Com.Parameters["@IsKeyPerson"].Value = ObjCVarCRM_ContactPersons.IsKeyPerson;
                        Com.Parameters["@CreatorUserID"].Value = ObjCVarCRM_ContactPersons.CreatorUserID;
                        Com.Parameters["@CreationDate"].Value = ObjCVarCRM_ContactPersons.CreationDate;
                        Com.Parameters["@ModificationUserID"].Value = ObjCVarCRM_ContactPersons.ModificationUserID;
                        Com.Parameters["@ModificationDate"].Value = ObjCVarCRM_ContactPersons.ModificationDate;
                        Com.Parameters["@CRM_ClientsID"].Value = ObjCVarCRM_ContactPersons.CRM_ClientsID;
                        Com.Parameters["@IsActualCustomer"].Value = ObjCVarCRM_ContactPersons.IsActualCustomer;
                        EndTrans(Com, Con);
                        if (ObjCVarCRM_ContactPersons.ID == 0)
                        {
                            ObjCVarCRM_ContactPersons.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarCRM_ContactPersons.mIsChanges = false;
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