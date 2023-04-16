﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.Operations.Operations.Generated
{
    [Serializable]
    public class CPKOperationsACIDDetails
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
    public partial class CVarOperationsACIDDetails : CPKOperationsACIDDetails
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mOperationID;
        internal String mReImportApproval;
        internal String mReImportApprovalNumber;
        internal String mSurveyRequest;
        internal String mBankNomination;
        internal String mTransactionMethod;
        internal String mBankNominationOpenedBy;
        internal String mCustomsCertificateNo;
        internal DateTime mExpirationDate;
        internal DateTime mSurveyDate;
        internal DateTime mUploadCargoDate;
        #endregion

        #region "Methods"
        public Int64 OperationID
        {
            get { return mOperationID; }
            set { mIsChanges = true; mOperationID = value; }
        }
        public String ReImportApproval
        {
            get { return mReImportApproval; }
            set { mIsChanges = true; mReImportApproval = value; }
        }
        public String ReImportApprovalNumber
        {
            get { return mReImportApprovalNumber; }
            set { mIsChanges = true; mReImportApprovalNumber = value; }
        }
        public String SurveyRequest
        {
            get { return mSurveyRequest; }
            set { mIsChanges = true; mSurveyRequest = value; }
        }
        public String BankNomination
        {
            get { return mBankNomination; }
            set { mIsChanges = true; mBankNomination = value; }
        }
        public String TransactionMethod
        {
            get { return mTransactionMethod; }
            set { mIsChanges = true; mTransactionMethod = value; }
        }
        public String BankNominationOpenedBy
        {
            get { return mBankNominationOpenedBy; }
            set { mIsChanges = true; mBankNominationOpenedBy = value; }
        }
        public String CustomsCertificateNo
        {
            get { return mCustomsCertificateNo; }
            set { mIsChanges = true; mCustomsCertificateNo = value; }
        }
        public DateTime ExpirationDate
        {
            get { return mExpirationDate; }
            set { mIsChanges = true; mExpirationDate = value; }
        }
        public DateTime SurveyDate
        {
            get { return mSurveyDate; }
            set { mIsChanges = true; mSurveyDate = value; }
        }
        public DateTime UploadCargoDate
        {
            get { return mUploadCargoDate; }
            set { mIsChanges = true; mUploadCargoDate = value; }
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

    public partial class COperationsACIDDetails
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
        public List<CVarOperationsACIDDetails> lstCVarOperationsACIDDetails = new List<CVarOperationsACIDDetails>();
        public List<CPKOperationsACIDDetails> lstDeletedCPKOperationsACIDDetails = new List<CPKOperationsACIDDetails>();
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
            lstCVarOperationsACIDDetails.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListOperationsACIDDetails";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemOperationsACIDDetails";
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
                        CVarOperationsACIDDetails ObjCVarOperationsACIDDetails = new CVarOperationsACIDDetails();
                        ObjCVarOperationsACIDDetails.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarOperationsACIDDetails.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarOperationsACIDDetails.mReImportApproval = Convert.ToString(dr["ReImportApproval"].ToString());
                        ObjCVarOperationsACIDDetails.mReImportApprovalNumber = Convert.ToString(dr["ReImportApprovalNumber"].ToString());
                        ObjCVarOperationsACIDDetails.mSurveyRequest = Convert.ToString(dr["SurveyRequest"].ToString());
                        ObjCVarOperationsACIDDetails.mBankNomination = Convert.ToString(dr["BankNomination"].ToString());
                        ObjCVarOperationsACIDDetails.mTransactionMethod = Convert.ToString(dr["TransactionMethod"].ToString());
                        ObjCVarOperationsACIDDetails.mBankNominationOpenedBy = Convert.ToString(dr["BankNominationOpenedBy"].ToString());
                        ObjCVarOperationsACIDDetails.mCustomsCertificateNo = Convert.ToString(dr["CustomsCertificateNo"].ToString());
                        ObjCVarOperationsACIDDetails.mExpirationDate = Convert.ToDateTime(dr["ExpirationDate"].ToString());
                        ObjCVarOperationsACIDDetails.mSurveyDate = Convert.ToDateTime(dr["SurveyDate"].ToString());
                        ObjCVarOperationsACIDDetails.mUploadCargoDate = Convert.ToDateTime(dr["UploadCargoDate"].ToString());
                        lstCVarOperationsACIDDetails.Add(ObjCVarOperationsACIDDetails);
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
            lstCVarOperationsACIDDetails.Clear();

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
                Com.CommandText = "[dbo].GetListPagingOperationsACIDDetails";
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
                        CVarOperationsACIDDetails ObjCVarOperationsACIDDetails = new CVarOperationsACIDDetails();
                        ObjCVarOperationsACIDDetails.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarOperationsACIDDetails.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarOperationsACIDDetails.mReImportApproval = Convert.ToString(dr["ReImportApproval"].ToString());
                        ObjCVarOperationsACIDDetails.mReImportApprovalNumber = Convert.ToString(dr["ReImportApprovalNumber"].ToString());
                        ObjCVarOperationsACIDDetails.mSurveyRequest = Convert.ToString(dr["SurveyRequest"].ToString());
                        ObjCVarOperationsACIDDetails.mBankNomination = Convert.ToString(dr["BankNomination"].ToString());
                        ObjCVarOperationsACIDDetails.mTransactionMethod = Convert.ToString(dr["TransactionMethod"].ToString());
                        ObjCVarOperationsACIDDetails.mBankNominationOpenedBy = Convert.ToString(dr["BankNominationOpenedBy"].ToString());
                        ObjCVarOperationsACIDDetails.mCustomsCertificateNo = Convert.ToString(dr["CustomsCertificateNo"].ToString());
                        ObjCVarOperationsACIDDetails.mExpirationDate = Convert.ToDateTime(dr["ExpirationDate"].ToString());
                        ObjCVarOperationsACIDDetails.mSurveyDate = Convert.ToDateTime(dr["SurveyDate"].ToString());
                        ObjCVarOperationsACIDDetails.mUploadCargoDate = Convert.ToDateTime(dr["UploadCargoDate"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarOperationsACIDDetails.Add(ObjCVarOperationsACIDDetails);
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
                    Com.CommandText = "[dbo].DeleteListOperationsACIDDetails";
                else
                    Com.CommandText = "[dbo].UpdateListOperationsACIDDetails";
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
        public Exception DeleteItem(List<CPKOperationsACIDDetails> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemOperationsACIDDetails";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                foreach (CPKOperationsACIDDetails ObjCPKOperationsACIDDetails in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt32(ObjCPKOperationsACIDDetails.ID);
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
        public Exception SaveMethod(List<CVarOperationsACIDDetails> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@OperationID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@ReImportApproval", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@ReImportApprovalNumber", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@SurveyRequest", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@BankNomination", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@TransactionMethod", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@BankNominationOpenedBy", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@CustomsCertificateNo", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@ExpirationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@SurveyDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@UploadCargoDate", SqlDbType.DateTime));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarOperationsACIDDetails ObjCVarOperationsACIDDetails in SaveList)
                {
                    if (ObjCVarOperationsACIDDetails.mIsChanges == true)
                    {
                        if (ObjCVarOperationsACIDDetails.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemOperationsACIDDetails";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarOperationsACIDDetails.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemOperationsACIDDetails";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarOperationsACIDDetails.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarOperationsACIDDetails.ID;
                        }
                        Com.Parameters["@OperationID"].Value = ObjCVarOperationsACIDDetails.OperationID;
                        Com.Parameters["@ReImportApproval"].Value = ObjCVarOperationsACIDDetails.ReImportApproval;
                        Com.Parameters["@ReImportApprovalNumber"].Value = ObjCVarOperationsACIDDetails.ReImportApprovalNumber;
                        Com.Parameters["@SurveyRequest"].Value = ObjCVarOperationsACIDDetails.SurveyRequest;
                        Com.Parameters["@BankNomination"].Value = ObjCVarOperationsACIDDetails.BankNomination;
                        Com.Parameters["@TransactionMethod"].Value = ObjCVarOperationsACIDDetails.TransactionMethod;
                        Com.Parameters["@BankNominationOpenedBy"].Value = ObjCVarOperationsACIDDetails.BankNominationOpenedBy;
                        Com.Parameters["@CustomsCertificateNo"].Value = ObjCVarOperationsACIDDetails.CustomsCertificateNo;
                        Com.Parameters["@ExpirationDate"].Value = ObjCVarOperationsACIDDetails.ExpirationDate;
                        Com.Parameters["@SurveyDate"].Value = ObjCVarOperationsACIDDetails.SurveyDate;
                        Com.Parameters["@UploadCargoDate"].Value = ObjCVarOperationsACIDDetails.UploadCargoDate;
                        EndTrans(Com, Con);
                        if (ObjCVarOperationsACIDDetails.ID == 0)
                        {
                            ObjCVarOperationsACIDDetails.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarOperationsACIDDetails.mIsChanges = false;
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