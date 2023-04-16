﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace Forwarding.MvcApp.Models.MasterData.BanksAccountsAndTreasuries.Generated
{
    [Serializable]
    public class CPKBankAccountTAX
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
    public partial class CVarBankAccountTAX : CPKBankAccountTAX
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mCode;
        internal String mName;
        internal String mLocalName;
        internal String mAccountName;
        internal String mAccountNumber;
        internal Int32 mDefaultCurrencyID;
        internal String mNotes;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Int32 mAccount_ID;
        internal Int32 mNotesPayable;
        internal Int32 mNotesPayableUnderCollection;
        internal Int32 mNotesReceivable;
        internal Int32 mNotesReceivableUnderCollection;
        internal Int32 mCollectionExpenses;
        internal Decimal mBankMinimumLimit;
        internal Int32 mBankDocumentID;
        internal Int32 mInJournalTypeID;
        internal Int32 mOutJournalTypeID;
        internal String mPrintedAs;
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
        public String AccountName
        {
            get { return mAccountName; }
            set { mIsChanges = true; mAccountName = value; }
        }
        public String AccountNumber
        {
            get { return mAccountNumber; }
            set { mIsChanges = true; mAccountNumber = value; }
        }
        public Int32 DefaultCurrencyID
        {
            get { return mDefaultCurrencyID; }
            set { mIsChanges = true; mDefaultCurrencyID = value; }
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
        public Int32 Account_ID
        {
            get { return mAccount_ID; }
            set { mIsChanges = true; mAccount_ID = value; }
        }
        public Int32 NotesPayable
        {
            get { return mNotesPayable; }
            set { mIsChanges = true; mNotesPayable = value; }
        }
        public Int32 NotesPayableUnderCollection
        {
            get { return mNotesPayableUnderCollection; }
            set { mIsChanges = true; mNotesPayableUnderCollection = value; }
        }
        public Int32 NotesReceivable
        {
            get { return mNotesReceivable; }
            set { mIsChanges = true; mNotesReceivable = value; }
        }
        public Int32 NotesReceivableUnderCollection
        {
            get { return mNotesReceivableUnderCollection; }
            set { mIsChanges = true; mNotesReceivableUnderCollection = value; }
        }
        public Int32 CollectionExpenses
        {
            get { return mCollectionExpenses; }
            set { mIsChanges = true; mCollectionExpenses = value; }
        }
        public Decimal BankMinimumLimit
        {
            get { return mBankMinimumLimit; }
            set { mIsChanges = true; mBankMinimumLimit = value; }
        }
        public Int32 BankDocumentID
        {
            get { return mBankDocumentID; }
            set { mIsChanges = true; mBankDocumentID = value; }
        }
        public Int32 InJournalTypeID
        {
            get { return mInJournalTypeID; }
            set { mIsChanges = true; mInJournalTypeID = value; }
        }
        public Int32 OutJournalTypeID
        {
            get { return mOutJournalTypeID; }
            set { mIsChanges = true; mOutJournalTypeID = value; }
        }
        public String PrintedAs
        {
            get { return mPrintedAs; }
            set { mIsChanges = true; mPrintedAs = value; }
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

    public partial class CBankAccountTAX
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
        public List<CVarBankAccountTAX> lstCVarBankAccount = new List<CVarBankAccountTAX>();
        public List<CPKBankAccountTAX> lstDeletedCPKBankAccount = new List<CPKBankAccountTAX>();
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
            lstCVarBankAccount.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListBankAccount";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemBankAccount";
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
                        CVarBankAccountTAX ObjCVarBankAccount = new CVarBankAccountTAX();
                        ObjCVarBankAccount.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarBankAccount.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarBankAccount.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarBankAccount.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarBankAccount.mAccountName = Convert.ToString(dr["AccountName"].ToString());
                        ObjCVarBankAccount.mAccountNumber = Convert.ToString(dr["AccountNumber"].ToString());
                        ObjCVarBankAccount.mDefaultCurrencyID = Convert.ToInt32(dr["DefaultCurrencyID"].ToString());
                        ObjCVarBankAccount.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarBankAccount.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarBankAccount.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarBankAccount.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarBankAccount.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarBankAccount.mAccount_ID = Convert.ToInt32(dr["Account_ID"].ToString());
                        ObjCVarBankAccount.mNotesPayable = Convert.ToInt32(dr["NotesPayable"].ToString());
                        ObjCVarBankAccount.mNotesPayableUnderCollection = Convert.ToInt32(dr["NotesPayableUnderCollection"].ToString());
                        ObjCVarBankAccount.mNotesReceivable = Convert.ToInt32(dr["NotesReceivable"].ToString());
                        ObjCVarBankAccount.mNotesReceivableUnderCollection = Convert.ToInt32(dr["NotesReceivableUnderCollection"].ToString());
                        ObjCVarBankAccount.mCollectionExpenses = Convert.ToInt32(dr["CollectionExpenses"].ToString());
                        ObjCVarBankAccount.mBankMinimumLimit = Convert.ToDecimal(dr["BankMinimumLimit"].ToString());
                        ObjCVarBankAccount.mBankDocumentID = Convert.ToInt32(dr["BankDocumentID"].ToString());
                        ObjCVarBankAccount.mInJournalTypeID = Convert.ToInt32(dr["InJournalTypeID"].ToString());
                        ObjCVarBankAccount.mOutJournalTypeID = Convert.ToInt32(dr["OutJournalTypeID"].ToString());
                        ObjCVarBankAccount.mPrintedAs = Convert.ToString(dr["PrintedAs"].ToString());
                        lstCVarBankAccount.Add(ObjCVarBankAccount);
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
            lstCVarBankAccount.Clear();

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
                Com.CommandText = "[dbo].GetListPagingBankAccount";
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
                        CVarBankAccountTAX ObjCVarBankAccount = new CVarBankAccountTAX();
                        ObjCVarBankAccount.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarBankAccount.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarBankAccount.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarBankAccount.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarBankAccount.mAccountName = Convert.ToString(dr["AccountName"].ToString());
                        ObjCVarBankAccount.mAccountNumber = Convert.ToString(dr["AccountNumber"].ToString());
                        ObjCVarBankAccount.mDefaultCurrencyID = Convert.ToInt32(dr["DefaultCurrencyID"].ToString());
                        ObjCVarBankAccount.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarBankAccount.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarBankAccount.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarBankAccount.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarBankAccount.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarBankAccount.mAccount_ID = Convert.ToInt32(dr["Account_ID"].ToString());
                        ObjCVarBankAccount.mNotesPayable = Convert.ToInt32(dr["NotesPayable"].ToString());
                        ObjCVarBankAccount.mNotesPayableUnderCollection = Convert.ToInt32(dr["NotesPayableUnderCollection"].ToString());
                        ObjCVarBankAccount.mNotesReceivable = Convert.ToInt32(dr["NotesReceivable"].ToString());
                        ObjCVarBankAccount.mNotesReceivableUnderCollection = Convert.ToInt32(dr["NotesReceivableUnderCollection"].ToString());
                        ObjCVarBankAccount.mCollectionExpenses = Convert.ToInt32(dr["CollectionExpenses"].ToString());
                        ObjCVarBankAccount.mBankMinimumLimit = Convert.ToDecimal(dr["BankMinimumLimit"].ToString());
                        ObjCVarBankAccount.mBankDocumentID = Convert.ToInt32(dr["BankDocumentID"].ToString());
                        ObjCVarBankAccount.mInJournalTypeID = Convert.ToInt32(dr["InJournalTypeID"].ToString());
                        ObjCVarBankAccount.mOutJournalTypeID = Convert.ToInt32(dr["OutJournalTypeID"].ToString());
                        ObjCVarBankAccount.mPrintedAs = Convert.ToString(dr["PrintedAs"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarBankAccount.Add(ObjCVarBankAccount);
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
                    Com.CommandText = "[dbo].DeleteListBankAccount";
                else
                    Com.CommandText = "[dbo].UpdateListBankAccount";
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
        public Exception DeleteItem(List<CPKBankAccountTAX> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemBankAccountTax";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                foreach (CPKBankAccountTAX ObjCPKBankAccount in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt32(ObjCPKBankAccount.ID);
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
        public Exception SaveMethod(List<CVarBankAccountTAX> SaveList)
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
                Com.Parameters.Add(new SqlParameter("@AccountName", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@AccountNumber", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@DefaultCurrencyID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@CreatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ModificatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ModificationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@Account_ID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@NotesPayable", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@NotesPayableUnderCollection", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@NotesReceivable", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@NotesReceivableUnderCollection", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CollectionExpenses", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@BankMinimumLimit", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@BankDocumentID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@InJournalTypeID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@OutJournalTypeID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@PrintedAs", SqlDbType.NVarChar));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarBankAccountTAX ObjCVarBankAccount in SaveList)
                {
                    if (ObjCVarBankAccount.mIsChanges == true)
                    {
                        if (ObjCVarBankAccount.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemBankAccountTax";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarBankAccount.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemBankAccountTax";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarBankAccount.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarBankAccount.ID;
                        }
                        Com.Parameters["@Code"].Value = ObjCVarBankAccount.Code;
                        Com.Parameters["@Name"].Value = ObjCVarBankAccount.Name;
                        Com.Parameters["@LocalName"].Value = ObjCVarBankAccount.LocalName;
                        Com.Parameters["@AccountName"].Value = ObjCVarBankAccount.AccountName;
                        Com.Parameters["@AccountNumber"].Value = ObjCVarBankAccount.AccountNumber;
                        Com.Parameters["@DefaultCurrencyID"].Value = ObjCVarBankAccount.DefaultCurrencyID;
                        Com.Parameters["@Notes"].Value = ObjCVarBankAccount.Notes;
                        Com.Parameters["@CreatorUserID"].Value = ObjCVarBankAccount.CreatorUserID;
                        Com.Parameters["@CreationDate"].Value = ObjCVarBankAccount.CreationDate;
                        Com.Parameters["@ModificatorUserID"].Value = ObjCVarBankAccount.ModificatorUserID;
                        Com.Parameters["@ModificationDate"].Value = ObjCVarBankAccount.ModificationDate;
                        Com.Parameters["@Account_ID"].Value = ObjCVarBankAccount.Account_ID;
                        Com.Parameters["@NotesPayable"].Value = ObjCVarBankAccount.NotesPayable;
                        Com.Parameters["@NotesPayableUnderCollection"].Value = ObjCVarBankAccount.NotesPayableUnderCollection;
                        Com.Parameters["@NotesReceivable"].Value = ObjCVarBankAccount.NotesReceivable;
                        Com.Parameters["@NotesReceivableUnderCollection"].Value = ObjCVarBankAccount.NotesReceivableUnderCollection;
                        Com.Parameters["@CollectionExpenses"].Value = ObjCVarBankAccount.CollectionExpenses;
                        Com.Parameters["@BankMinimumLimit"].Value = ObjCVarBankAccount.BankMinimumLimit;
                        Com.Parameters["@BankDocumentID"].Value = ObjCVarBankAccount.BankDocumentID;
                        Com.Parameters["@InJournalTypeID"].Value = ObjCVarBankAccount.InJournalTypeID;
                        Com.Parameters["@OutJournalTypeID"].Value = ObjCVarBankAccount.OutJournalTypeID;
                        Com.Parameters["@PrintedAs"].Value = ObjCVarBankAccount.PrintedAs;
                        EndTrans(Com, Con);
                        if (ObjCVarBankAccount.ID == 0)
                        {
                            ObjCVarBankAccount.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarBankAccount.mIsChanges = false;
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
