﻿using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.MasterData.Partners.Generated
{
    [Serializable]
    public class CPKSuppliers
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
    public partial class CVarSuppliers : CPKSuppliers
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mCode;
        internal String mName;
        internal String mLocalName;
        internal String mWebsite;
        internal Boolean mIsInactive;
        internal String mNotes;
        internal String mVATNumber;
        internal Int32 mPaymentTermID;
        internal Int32 mCurrencyID;
        internal Int32 mTaxeTypeID;
        internal Boolean mIsConsolidatedInvoice;
        internal String mBankName;
        internal String mBankAddress;
        internal String mSwift;
        internal String mBankAccountNumber;
        internal String mIBANNumber;
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
        internal Int32 mEmailOptionPayablesReport;
        internal Int32 mEmailOptionPayablesAging;
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
        public String Website
        {
            get { return mWebsite; }
            set { mIsChanges = true; mWebsite = value; }
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
        public String VATNumber
        {
            get { return mVATNumber; }
            set { mIsChanges = true; mVATNumber = value; }
        }
        public Int32 PaymentTermID
        {
            get { return mPaymentTermID; }
            set { mIsChanges = true; mPaymentTermID = value; }
        }
        public Int32 CurrencyID
        {
            get { return mCurrencyID; }
            set { mIsChanges = true; mCurrencyID = value; }
        }
        public Int32 TaxeTypeID
        {
            get { return mTaxeTypeID; }
            set { mIsChanges = true; mTaxeTypeID = value; }
        }
        public Boolean IsConsolidatedInvoice
        {
            get { return mIsConsolidatedInvoice; }
            set { mIsChanges = true; mIsConsolidatedInvoice = value; }
        }
        public String BankName
        {
            get { return mBankName; }
            set { mIsChanges = true; mBankName = value; }
        }
        public String BankAddress
        {
            get { return mBankAddress; }
            set { mIsChanges = true; mBankAddress = value; }
        }
        public String Swift
        {
            get { return mSwift; }
            set { mIsChanges = true; mSwift = value; }
        }
        public String BankAccountNumber
        {
            get { return mBankAccountNumber; }
            set { mIsChanges = true; mBankAccountNumber = value; }
        }
        public String IBANNumber
        {
            get { return mIBANNumber; }
            set { mIsChanges = true; mIBANNumber = value; }
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
        public Int32 EmailOptionPayablesReport
        {
            get { return mEmailOptionPayablesReport; }
            set { mIsChanges = true; mEmailOptionPayablesReport = value; }
        }
        public Int32 EmailOptionPayablesAging
        {
            get { return mEmailOptionPayablesAging; }
            set { mIsChanges = true; mEmailOptionPayablesAging = value; }
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

    public partial class CSuppliers
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
        public List<CVarSuppliers> lstCVarSuppliers = new List<CVarSuppliers>();
        public List<CPKSuppliers> lstDeletedCPKSuppliers = new List<CPKSuppliers>();
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
            lstCVarSuppliers.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListSuppliers";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemSuppliers";
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
                        CVarSuppliers ObjCVarSuppliers = new CVarSuppliers();
                        ObjCVarSuppliers.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarSuppliers.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarSuppliers.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarSuppliers.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarSuppliers.mWebsite = Convert.ToString(dr["Website"].ToString());
                        ObjCVarSuppliers.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarSuppliers.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarSuppliers.mVATNumber = Convert.ToString(dr["VATNumber"].ToString());
                        ObjCVarSuppliers.mPaymentTermID = Convert.ToInt32(dr["PaymentTermID"].ToString());
                        ObjCVarSuppliers.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarSuppliers.mTaxeTypeID = Convert.ToInt32(dr["TaxeTypeID"].ToString());
                        ObjCVarSuppliers.mIsConsolidatedInvoice = Convert.ToBoolean(dr["IsConsolidatedInvoice"].ToString());
                        ObjCVarSuppliers.mBankName = Convert.ToString(dr["BankName"].ToString());
                        ObjCVarSuppliers.mBankAddress = Convert.ToString(dr["BankAddress"].ToString());
                        ObjCVarSuppliers.mSwift = Convert.ToString(dr["Swift"].ToString());
                        ObjCVarSuppliers.mBankAccountNumber = Convert.ToString(dr["BankAccountNumber"].ToString());
                        ObjCVarSuppliers.mIBANNumber = Convert.ToString(dr["IBANNumber"].ToString());
                        ObjCVarSuppliers.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarSuppliers.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarSuppliers.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarSuppliers.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarSuppliers.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarSuppliers.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarSuppliers.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarSuppliers.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarSuppliers.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarSuppliers.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarSuppliers.mSubAccountGroupID = Convert.ToInt32(dr["SubAccountGroupID"].ToString());
                        ObjCVarSuppliers.mEmailOptionPayablesReport = Convert.ToInt32(dr["EmailOptionPayablesReport"].ToString());
                        ObjCVarSuppliers.mEmailOptionPayablesAging = Convert.ToInt32(dr["EmailOptionPayablesAging"].ToString());
                        lstCVarSuppliers.Add(ObjCVarSuppliers);
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
            lstCVarSuppliers.Clear();

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
                Com.CommandText = "[dbo].GetListPagingSuppliers";
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
                        CVarSuppliers ObjCVarSuppliers = new CVarSuppliers();
                        ObjCVarSuppliers.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarSuppliers.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarSuppliers.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarSuppliers.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarSuppliers.mWebsite = Convert.ToString(dr["Website"].ToString());
                        ObjCVarSuppliers.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarSuppliers.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarSuppliers.mVATNumber = Convert.ToString(dr["VATNumber"].ToString());
                        ObjCVarSuppliers.mPaymentTermID = Convert.ToInt32(dr["PaymentTermID"].ToString());
                        ObjCVarSuppliers.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarSuppliers.mTaxeTypeID = Convert.ToInt32(dr["TaxeTypeID"].ToString());
                        ObjCVarSuppliers.mIsConsolidatedInvoice = Convert.ToBoolean(dr["IsConsolidatedInvoice"].ToString());
                        ObjCVarSuppliers.mBankName = Convert.ToString(dr["BankName"].ToString());
                        ObjCVarSuppliers.mBankAddress = Convert.ToString(dr["BankAddress"].ToString());
                        ObjCVarSuppliers.mSwift = Convert.ToString(dr["Swift"].ToString());
                        ObjCVarSuppliers.mBankAccountNumber = Convert.ToString(dr["BankAccountNumber"].ToString());
                        ObjCVarSuppliers.mIBANNumber = Convert.ToString(dr["IBANNumber"].ToString());
                        ObjCVarSuppliers.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarSuppliers.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarSuppliers.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarSuppliers.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarSuppliers.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarSuppliers.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarSuppliers.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarSuppliers.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarSuppliers.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarSuppliers.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarSuppliers.mSubAccountGroupID = Convert.ToInt32(dr["SubAccountGroupID"].ToString());
                        ObjCVarSuppliers.mEmailOptionPayablesReport = Convert.ToInt32(dr["EmailOptionPayablesReport"].ToString());
                        ObjCVarSuppliers.mEmailOptionPayablesAging = Convert.ToInt32(dr["EmailOptionPayablesAging"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarSuppliers.Add(ObjCVarSuppliers);
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
                    Com.CommandText = "[dbo].DeleteListSuppliers";
                else
                    Com.CommandText = "[dbo].UpdateListSuppliers";
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
        public Exception DeleteItem(List<CPKSuppliers> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemSuppliers";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                foreach (CPKSuppliers ObjCPKSuppliers in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt32(ObjCPKSuppliers.ID);
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
        public Exception SaveMethod(List<CVarSuppliers> SaveList)
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
                Com.Parameters.Add(new SqlParameter("@Website", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@IsInactive", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@VATNumber", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@PaymentTermID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CurrencyID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@TaxeTypeID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@IsConsolidatedInvoice", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@BankName", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@BankAddress", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Swift", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@BankAccountNumber", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@IBANNumber", SqlDbType.NVarChar));
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
                Com.Parameters.Add(new SqlParameter("@EmailOptionPayablesReport", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@EmailOptionPayablesAging", SqlDbType.Int));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarSuppliers ObjCVarSuppliers in SaveList)
                {
                    if (ObjCVarSuppliers.mIsChanges == true)
                    {
                        if (ObjCVarSuppliers.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemSuppliers";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarSuppliers.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemSuppliers";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarSuppliers.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarSuppliers.ID;
                        }
                        Com.Parameters["@Code"].Value = ObjCVarSuppliers.Code;
                        Com.Parameters["@Name"].Value = ObjCVarSuppliers.Name;
                        Com.Parameters["@LocalName"].Value = ObjCVarSuppliers.LocalName;
                        Com.Parameters["@Website"].Value = ObjCVarSuppliers.Website;
                        Com.Parameters["@IsInactive"].Value = ObjCVarSuppliers.IsInactive;
                        Com.Parameters["@Notes"].Value = ObjCVarSuppliers.Notes;
                        Com.Parameters["@VATNumber"].Value = ObjCVarSuppliers.VATNumber;
                        Com.Parameters["@PaymentTermID"].Value = ObjCVarSuppliers.PaymentTermID;
                        Com.Parameters["@CurrencyID"].Value = ObjCVarSuppliers.CurrencyID;
                        Com.Parameters["@TaxeTypeID"].Value = ObjCVarSuppliers.TaxeTypeID;
                        Com.Parameters["@IsConsolidatedInvoice"].Value = ObjCVarSuppliers.IsConsolidatedInvoice;
                        Com.Parameters["@BankName"].Value = ObjCVarSuppliers.BankName;
                        Com.Parameters["@BankAddress"].Value = ObjCVarSuppliers.BankAddress;
                        Com.Parameters["@Swift"].Value = ObjCVarSuppliers.Swift;
                        Com.Parameters["@BankAccountNumber"].Value = ObjCVarSuppliers.BankAccountNumber;
                        Com.Parameters["@IBANNumber"].Value = ObjCVarSuppliers.IBANNumber;
                        Com.Parameters["@CreatorUserID"].Value = ObjCVarSuppliers.CreatorUserID;
                        Com.Parameters["@CreationDate"].Value = ObjCVarSuppliers.CreationDate;
                        Com.Parameters["@ModificatorUserID"].Value = ObjCVarSuppliers.ModificatorUserID;
                        Com.Parameters["@ModificationDate"].Value = ObjCVarSuppliers.ModificationDate;
                        Com.Parameters["@LockingUserID"].Value = ObjCVarSuppliers.LockingUserID;
                        Com.Parameters["@TimeLocked"].Value = ObjCVarSuppliers.TimeLocked;
                        Com.Parameters["@IsDeleted"].Value = ObjCVarSuppliers.IsDeleted;
                        Com.Parameters["@AccountID"].Value = ObjCVarSuppliers.AccountID;
                        Com.Parameters["@SubAccountID"].Value = ObjCVarSuppliers.SubAccountID;
                        Com.Parameters["@CostCenterID"].Value = ObjCVarSuppliers.CostCenterID;
                        Com.Parameters["@SubAccountGroupID"].Value = ObjCVarSuppliers.SubAccountGroupID;
                        Com.Parameters["@EmailOptionPayablesReport"].Value = ObjCVarSuppliers.EmailOptionPayablesReport;
                        Com.Parameters["@EmailOptionPayablesAging"].Value = ObjCVarSuppliers.EmailOptionPayablesAging;
                        EndTrans(Com, Con);
                        if (ObjCVarSuppliers.ID == 0)
                        {
                            ObjCVarSuppliers.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarSuppliers.mIsChanges = false;
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
