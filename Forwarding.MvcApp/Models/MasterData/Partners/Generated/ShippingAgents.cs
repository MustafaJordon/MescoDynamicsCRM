﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.MasterData.Partners.Generated
{
    [Serializable]
    public class CPKShippingAgents
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
    public partial class CVarShippingAgents : CPKShippingAgents
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mCode;
        internal String mName;
        internal String mLocalName;
        internal String mWebsite;
        internal Boolean mIsInactive;
        internal String mNotes;
        internal String mForwarderAccountNumber;
        internal String mForwarderCreditNumber;
        internal String mLocalCustomsCode;
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
        public String ForwarderAccountNumber
        {
            get { return mForwarderAccountNumber; }
            set { mIsChanges = true; mForwarderAccountNumber = value; }
        }
        public String ForwarderCreditNumber
        {
            get { return mForwarderCreditNumber; }
            set { mIsChanges = true; mForwarderCreditNumber = value; }
        }
        public String LocalCustomsCode
        {
            get { return mLocalCustomsCode; }
            set { mIsChanges = true; mLocalCustomsCode = value; }
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

    public partial class CShippingAgents
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
        public List<CVarShippingAgents> lstCVarShippingAgents = new List<CVarShippingAgents>();
        public List<CPKShippingAgents> lstDeletedCPKShippingAgents = new List<CPKShippingAgents>();
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
            lstCVarShippingAgents.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListShippingAgents";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemShippingAgents";
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
                        CVarShippingAgents ObjCVarShippingAgents = new CVarShippingAgents();
                        ObjCVarShippingAgents.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarShippingAgents.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarShippingAgents.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarShippingAgents.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarShippingAgents.mWebsite = Convert.ToString(dr["Website"].ToString());
                        ObjCVarShippingAgents.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarShippingAgents.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarShippingAgents.mForwarderAccountNumber = Convert.ToString(dr["ForwarderAccountNumber"].ToString());
                        ObjCVarShippingAgents.mForwarderCreditNumber = Convert.ToString(dr["ForwarderCreditNumber"].ToString());
                        ObjCVarShippingAgents.mLocalCustomsCode = Convert.ToString(dr["LocalCustomsCode"].ToString());
                        ObjCVarShippingAgents.mVATNumber = Convert.ToString(dr["VATNumber"].ToString());
                        ObjCVarShippingAgents.mPaymentTermID = Convert.ToInt32(dr["PaymentTermID"].ToString());
                        ObjCVarShippingAgents.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarShippingAgents.mTaxeTypeID = Convert.ToInt32(dr["TaxeTypeID"].ToString());
                        ObjCVarShippingAgents.mIsConsolidatedInvoice = Convert.ToBoolean(dr["IsConsolidatedInvoice"].ToString());
                        ObjCVarShippingAgents.mBankName = Convert.ToString(dr["BankName"].ToString());
                        ObjCVarShippingAgents.mBankAddress = Convert.ToString(dr["BankAddress"].ToString());
                        ObjCVarShippingAgents.mSwift = Convert.ToString(dr["Swift"].ToString());
                        ObjCVarShippingAgents.mBankAccountNumber = Convert.ToString(dr["BankAccountNumber"].ToString());
                        ObjCVarShippingAgents.mIBANNumber = Convert.ToString(dr["IBANNumber"].ToString());
                        ObjCVarShippingAgents.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarShippingAgents.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarShippingAgents.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarShippingAgents.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarShippingAgents.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarShippingAgents.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarShippingAgents.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarShippingAgents.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarShippingAgents.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarShippingAgents.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarShippingAgents.mSubAccountGroupID = Convert.ToInt32(dr["SubAccountGroupID"].ToString());
                        lstCVarShippingAgents.Add(ObjCVarShippingAgents);
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
            lstCVarShippingAgents.Clear();

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
                Com.CommandText = "[dbo].GetListPagingShippingAgents";
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
                        CVarShippingAgents ObjCVarShippingAgents = new CVarShippingAgents();
                        ObjCVarShippingAgents.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarShippingAgents.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarShippingAgents.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarShippingAgents.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarShippingAgents.mWebsite = Convert.ToString(dr["Website"].ToString());
                        ObjCVarShippingAgents.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarShippingAgents.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarShippingAgents.mForwarderAccountNumber = Convert.ToString(dr["ForwarderAccountNumber"].ToString());
                        ObjCVarShippingAgents.mForwarderCreditNumber = Convert.ToString(dr["ForwarderCreditNumber"].ToString());
                        ObjCVarShippingAgents.mLocalCustomsCode = Convert.ToString(dr["LocalCustomsCode"].ToString());
                        ObjCVarShippingAgents.mVATNumber = Convert.ToString(dr["VATNumber"].ToString());
                        ObjCVarShippingAgents.mPaymentTermID = Convert.ToInt32(dr["PaymentTermID"].ToString());
                        ObjCVarShippingAgents.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarShippingAgents.mTaxeTypeID = Convert.ToInt32(dr["TaxeTypeID"].ToString());
                        ObjCVarShippingAgents.mIsConsolidatedInvoice = Convert.ToBoolean(dr["IsConsolidatedInvoice"].ToString());
                        ObjCVarShippingAgents.mBankName = Convert.ToString(dr["BankName"].ToString());
                        ObjCVarShippingAgents.mBankAddress = Convert.ToString(dr["BankAddress"].ToString());
                        ObjCVarShippingAgents.mSwift = Convert.ToString(dr["Swift"].ToString());
                        ObjCVarShippingAgents.mBankAccountNumber = Convert.ToString(dr["BankAccountNumber"].ToString());
                        ObjCVarShippingAgents.mIBANNumber = Convert.ToString(dr["IBANNumber"].ToString());
                        ObjCVarShippingAgents.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarShippingAgents.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarShippingAgents.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarShippingAgents.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarShippingAgents.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarShippingAgents.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarShippingAgents.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarShippingAgents.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarShippingAgents.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarShippingAgents.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarShippingAgents.mSubAccountGroupID = Convert.ToInt32(dr["SubAccountGroupID"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarShippingAgents.Add(ObjCVarShippingAgents);
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
                    Com.CommandText = "[dbo].DeleteListShippingAgents";
                else
                    Com.CommandText = "[dbo].UpdateListShippingAgents";
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
        public Exception DeleteItem(List<CPKShippingAgents> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemShippingAgents";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                foreach (CPKShippingAgents ObjCPKShippingAgents in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt32(ObjCPKShippingAgents.ID);
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
        public Exception SaveMethod(List<CVarShippingAgents> SaveList)
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
                Com.Parameters.Add(new SqlParameter("@ForwarderAccountNumber", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@ForwarderCreditNumber", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@LocalCustomsCode", SqlDbType.NVarChar));
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
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarShippingAgents ObjCVarShippingAgents in SaveList)
                {
                    if (ObjCVarShippingAgents.mIsChanges == true)
                    {
                        if (ObjCVarShippingAgents.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemShippingAgents";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarShippingAgents.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemShippingAgents";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarShippingAgents.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarShippingAgents.ID;
                        }
                        Com.Parameters["@Code"].Value = ObjCVarShippingAgents.Code;
                        Com.Parameters["@Name"].Value = ObjCVarShippingAgents.Name;
                        Com.Parameters["@LocalName"].Value = ObjCVarShippingAgents.LocalName;
                        Com.Parameters["@Website"].Value = ObjCVarShippingAgents.Website;
                        Com.Parameters["@IsInactive"].Value = ObjCVarShippingAgents.IsInactive;
                        Com.Parameters["@Notes"].Value = ObjCVarShippingAgents.Notes;
                        Com.Parameters["@ForwarderAccountNumber"].Value = ObjCVarShippingAgents.ForwarderAccountNumber;
                        Com.Parameters["@ForwarderCreditNumber"].Value = ObjCVarShippingAgents.ForwarderCreditNumber;
                        Com.Parameters["@LocalCustomsCode"].Value = ObjCVarShippingAgents.LocalCustomsCode;
                        Com.Parameters["@VATNumber"].Value = ObjCVarShippingAgents.VATNumber;
                        Com.Parameters["@PaymentTermID"].Value = ObjCVarShippingAgents.PaymentTermID;
                        Com.Parameters["@CurrencyID"].Value = ObjCVarShippingAgents.CurrencyID;
                        Com.Parameters["@TaxeTypeID"].Value = ObjCVarShippingAgents.TaxeTypeID;
                        Com.Parameters["@IsConsolidatedInvoice"].Value = ObjCVarShippingAgents.IsConsolidatedInvoice;
                        Com.Parameters["@BankName"].Value = ObjCVarShippingAgents.BankName;
                        Com.Parameters["@BankAddress"].Value = ObjCVarShippingAgents.BankAddress;
                        Com.Parameters["@Swift"].Value = ObjCVarShippingAgents.Swift;
                        Com.Parameters["@BankAccountNumber"].Value = ObjCVarShippingAgents.BankAccountNumber;
                        Com.Parameters["@IBANNumber"].Value = ObjCVarShippingAgents.IBANNumber;
                        Com.Parameters["@CreatorUserID"].Value = ObjCVarShippingAgents.CreatorUserID;
                        Com.Parameters["@CreationDate"].Value = ObjCVarShippingAgents.CreationDate;
                        Com.Parameters["@ModificatorUserID"].Value = ObjCVarShippingAgents.ModificatorUserID;
                        Com.Parameters["@ModificationDate"].Value = ObjCVarShippingAgents.ModificationDate;
                        Com.Parameters["@LockingUserID"].Value = ObjCVarShippingAgents.LockingUserID;
                        Com.Parameters["@TimeLocked"].Value = ObjCVarShippingAgents.TimeLocked;
                        Com.Parameters["@IsDeleted"].Value = ObjCVarShippingAgents.IsDeleted;
                        Com.Parameters["@AccountID"].Value = ObjCVarShippingAgents.AccountID;
                        Com.Parameters["@SubAccountID"].Value = ObjCVarShippingAgents.SubAccountID;
                        Com.Parameters["@CostCenterID"].Value = ObjCVarShippingAgents.CostCenterID;
                        Com.Parameters["@SubAccountGroupID"].Value = ObjCVarShippingAgents.SubAccountGroupID;
                        EndTrans(Com, Con);
                        if (ObjCVarShippingAgents.ID == 0)
                        {
                            ObjCVarShippingAgents.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarShippingAgents.mIsChanges = false;
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
