﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using Forwarding.MvcApp.Models.MasterData.Partners.Generated;
/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.MasterData.Partners.Customized
{

    public partial class CTruckers_DynamicConnection : Base_DynamicConnection
    {
        public CTruckers_DynamicConnection(Helpers.Companies.InternalCompanies Company):base(Company)
        {
           
        }
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
        public List<CVarTruckers> lstCVarTruckers = new List<CVarTruckers>();
        public List<CPKTruckers> lstDeletedCPKTruckers = new List<CPKTruckers>();
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
            SqlConnection Con = new SqlConnection(base.ConStr);
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarTruckers.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.VarChar));
                    Com.CommandText = "[dbo].GetListTruckers";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemTruckers";
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
                        CVarTruckers ObjCVarTruckers = new CVarTruckers();
                        ObjCVarTruckers.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarTruckers.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarTruckers.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarTruckers.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarTruckers.mWebsite = Convert.ToString(dr["Website"].ToString());
                        ObjCVarTruckers.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarTruckers.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarTruckers.mVATNumber = Convert.ToString(dr["VATNumber"].ToString());
                        ObjCVarTruckers.mPaymentTermID = Convert.ToInt32(dr["PaymentTermID"].ToString());
                        ObjCVarTruckers.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarTruckers.mTaxeTypeID = Convert.ToInt32(dr["TaxeTypeID"].ToString());
                        ObjCVarTruckers.mIsConsolidatedInvoice = Convert.ToBoolean(dr["IsConsolidatedInvoice"].ToString());
                        ObjCVarTruckers.mBankName = Convert.ToString(dr["BankName"].ToString());
                        ObjCVarTruckers.mBankAddress = Convert.ToString(dr["BankAddress"].ToString());
                        ObjCVarTruckers.mSwift = Convert.ToString(dr["Swift"].ToString());
                        ObjCVarTruckers.mBankAccountNumber = Convert.ToString(dr["BankAccountNumber"].ToString());
                        ObjCVarTruckers.mIBANNumber = Convert.ToString(dr["IBANNumber"].ToString());
                        ObjCVarTruckers.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarTruckers.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarTruckers.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarTruckers.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarTruckers.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarTruckers.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarTruckers.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarTruckers.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarTruckers.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarTruckers.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarTruckers.mSubAccountGroupID = Convert.ToInt32(dr["SubAccountGroupID"].ToString());
                        ObjCVarTruckers.mInsuranceValidity = Convert.ToString(dr["InsuranceValidity"].ToString());
                        ObjCVarTruckers.mTransportLicenceValidity = Convert.ToString(dr["TransportLicenceValidity"].ToString());
                        ObjCVarTruckers.mGMPValidity = Convert.ToString(dr["GMPValidity"].ToString());
                        ObjCVarTruckers.mQUALIMATvalidity = Convert.ToString(dr["QUALIMATvalidity"].ToString());
                        ObjCVarTruckers.mISO9001validity = Convert.ToString(dr["ISO9001validity"].ToString());
                        lstCVarTruckers.Add(ObjCVarTruckers);
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
            SqlConnection Con = new SqlConnection(base.ConStr);
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarTruckers.Clear();

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
                Com.CommandText = "[dbo].GetListPagingTruckers";
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
                        CVarTruckers ObjCVarTruckers = new CVarTruckers();
                        ObjCVarTruckers.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarTruckers.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarTruckers.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarTruckers.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarTruckers.mWebsite = Convert.ToString(dr["Website"].ToString());
                        ObjCVarTruckers.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarTruckers.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarTruckers.mVATNumber = Convert.ToString(dr["VATNumber"].ToString());
                        ObjCVarTruckers.mPaymentTermID = Convert.ToInt32(dr["PaymentTermID"].ToString());
                        ObjCVarTruckers.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarTruckers.mTaxeTypeID = Convert.ToInt32(dr["TaxeTypeID"].ToString());
                        ObjCVarTruckers.mIsConsolidatedInvoice = Convert.ToBoolean(dr["IsConsolidatedInvoice"].ToString());
                        ObjCVarTruckers.mBankName = Convert.ToString(dr["BankName"].ToString());
                        ObjCVarTruckers.mBankAddress = Convert.ToString(dr["BankAddress"].ToString());
                        ObjCVarTruckers.mSwift = Convert.ToString(dr["Swift"].ToString());
                        ObjCVarTruckers.mBankAccountNumber = Convert.ToString(dr["BankAccountNumber"].ToString());
                        ObjCVarTruckers.mIBANNumber = Convert.ToString(dr["IBANNumber"].ToString());
                        ObjCVarTruckers.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarTruckers.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarTruckers.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarTruckers.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarTruckers.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarTruckers.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarTruckers.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarTruckers.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarTruckers.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarTruckers.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarTruckers.mSubAccountGroupID = Convert.ToInt32(dr["SubAccountGroupID"].ToString());
                        ObjCVarTruckers.mInsuranceValidity = Convert.ToString(dr["InsuranceValidity"].ToString());
                        ObjCVarTruckers.mTransportLicenceValidity = Convert.ToString(dr["TransportLicenceValidity"].ToString());
                        ObjCVarTruckers.mGMPValidity = Convert.ToString(dr["GMPValidity"].ToString());
                        ObjCVarTruckers.mQUALIMATvalidity = Convert.ToString(dr["QUALIMATvalidity"].ToString());
                        ObjCVarTruckers.mISO9001validity = Convert.ToString(dr["ISO9001validity"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarTruckers.Add(ObjCVarTruckers);
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
            SqlConnection Con = new SqlConnection(base.ConStr);
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                if (IsDelete == true)
                    Com.CommandText = "[dbo].DeleteListTruckers";
                else
                    Com.CommandText = "[dbo].UpdateListTruckers";
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
        public Exception DeleteItem(List<CPKTruckers> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(base.ConStr);
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemTruckers";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                foreach (CPKTruckers ObjCPKTruckers in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt32(ObjCPKTruckers.ID);
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
        public Exception SaveMethod(List<CVarTruckers> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(base.ConStr);
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
                Com.Parameters.Add(new SqlParameter("@InsuranceValidity", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@TransportLicenceValidity", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@GMPValidity", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@QUALIMATvalidity", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@ISO9001validity", SqlDbType.NVarChar));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarTruckers ObjCVarTruckers in SaveList)
                {
                    if (ObjCVarTruckers.mIsChanges == true)
                    {
                        if (ObjCVarTruckers.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemTruckers";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarTruckers.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemTruckers";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarTruckers.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarTruckers.ID;
                        }
                        Com.Parameters["@Code"].Value = ObjCVarTruckers.Code;
                        Com.Parameters["@Name"].Value = ObjCVarTruckers.Name;
                        Com.Parameters["@LocalName"].Value = ObjCVarTruckers.LocalName;
                        Com.Parameters["@Website"].Value = ObjCVarTruckers.Website;
                        Com.Parameters["@IsInactive"].Value = ObjCVarTruckers.IsInactive;
                        Com.Parameters["@Notes"].Value = ObjCVarTruckers.Notes;
                        Com.Parameters["@VATNumber"].Value = ObjCVarTruckers.VATNumber;
                        Com.Parameters["@PaymentTermID"].Value = ObjCVarTruckers.PaymentTermID;
                        Com.Parameters["@CurrencyID"].Value = ObjCVarTruckers.CurrencyID;
                        Com.Parameters["@TaxeTypeID"].Value = ObjCVarTruckers.TaxeTypeID;
                        Com.Parameters["@IsConsolidatedInvoice"].Value = ObjCVarTruckers.IsConsolidatedInvoice;
                        Com.Parameters["@BankName"].Value = ObjCVarTruckers.BankName;
                        Com.Parameters["@BankAddress"].Value = ObjCVarTruckers.BankAddress;
                        Com.Parameters["@Swift"].Value = ObjCVarTruckers.Swift;
                        Com.Parameters["@BankAccountNumber"].Value = ObjCVarTruckers.BankAccountNumber;
                        Com.Parameters["@IBANNumber"].Value = ObjCVarTruckers.IBANNumber;
                        Com.Parameters["@CreatorUserID"].Value = ObjCVarTruckers.CreatorUserID;
                        Com.Parameters["@CreationDate"].Value = ObjCVarTruckers.CreationDate;
                        Com.Parameters["@ModificatorUserID"].Value = ObjCVarTruckers.ModificatorUserID;
                        Com.Parameters["@ModificationDate"].Value = ObjCVarTruckers.ModificationDate;
                        Com.Parameters["@LockingUserID"].Value = ObjCVarTruckers.LockingUserID;
                        Com.Parameters["@TimeLocked"].Value = ObjCVarTruckers.TimeLocked;
                        Com.Parameters["@IsDeleted"].Value = ObjCVarTruckers.IsDeleted;
                        Com.Parameters["@AccountID"].Value = ObjCVarTruckers.AccountID;
                        Com.Parameters["@SubAccountID"].Value = ObjCVarTruckers.SubAccountID;
                        Com.Parameters["@CostCenterID"].Value = ObjCVarTruckers.CostCenterID;
                        Com.Parameters["@SubAccountGroupID"].Value = ObjCVarTruckers.SubAccountGroupID;
                        Com.Parameters["@InsuranceValidity"].Value = ObjCVarTruckers.InsuranceValidity;
                        Com.Parameters["@TransportLicenceValidity"].Value = ObjCVarTruckers.TransportLicenceValidity;
                        Com.Parameters["@GMPValidity"].Value = ObjCVarTruckers.GMPValidity;
                        Com.Parameters["@QUALIMATvalidity"].Value = ObjCVarTruckers.QUALIMATvalidity;
                        Com.Parameters["@ISO9001validity"].Value = ObjCVarTruckers.ISO9001validity;
                        EndTrans(Com, Con);
                        if (ObjCVarTruckers.ID == 0)
                        {
                            ObjCVarTruckers.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarTruckers.mIsChanges = false;
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
