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
    public class CPKvwBankAccount
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
    public partial class CVarvwBankAccount : CPKvwBankAccount
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mCode;
        internal String mName;
        internal String mLocalName;
        internal String mAccountName;
        internal String mAccountNumber;
        internal Int32 mDefaultCurrencyID;
        internal Int32 mCurrencyID;
        internal String mCurrencyCode;
        internal String mNotes;
        internal Int32 mPaymentsCount;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Int32 mAccount_ID;
        internal String mAccountName_ERP;
        internal Int32 mNotesPayable;
        internal String mNotesPayableName;
        internal Int32 mNotesPayableUnderCollection;
        internal String mNotesPayableUnderCollectionName;
        internal Int32 mNotesReceivable;
        internal String mNotesReceivableName;
        internal Int32 mNotesReceivableUnderCollection;
        internal String mNotesReceivableUnderCollectionName;
        internal Int32 mCollectionExpenses;
        internal String mCollectionExpensesName;
        internal Decimal mBankMinimumLimit;
        internal Int32 mBankDocumentID;
        internal Int32 mInJournalTypeID;
        internal String mInJournalTypeName;
        internal Int32 mOutJournalTypeID;
        internal String mOutJournalTypeName;
        internal String mPrintedAs;
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
        public String AccountName
        {
            get { return mAccountName; }
            set { mAccountName = value; }
        }
        public String AccountNumber
        {
            get { return mAccountNumber; }
            set { mAccountNumber = value; }
        }
        public Int32 DefaultCurrencyID
        {
            get { return mDefaultCurrencyID; }
            set { mDefaultCurrencyID = value; }
        }
        public Int32 CurrencyID
        {
            get { return mCurrencyID; }
            set { mCurrencyID = value; }
        }
        public String CurrencyCode
        {
            get { return mCurrencyCode; }
            set { mCurrencyCode = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mNotes = value; }
        }
        public Int32 PaymentsCount
        {
            get { return mPaymentsCount; }
            set { mPaymentsCount = value; }
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
        public Int32 Account_ID
        {
            get { return mAccount_ID; }
            set { mAccount_ID = value; }
        }
        public String AccountName_ERP
        {
            get { return mAccountName_ERP; }
            set { mAccountName_ERP = value; }
        }
        public Int32 NotesPayable
        {
            get { return mNotesPayable; }
            set { mNotesPayable = value; }
        }
        public String NotesPayableName
        {
            get { return mNotesPayableName; }
            set { mNotesPayableName = value; }
        }
        public Int32 NotesPayableUnderCollection
        {
            get { return mNotesPayableUnderCollection; }
            set { mNotesPayableUnderCollection = value; }
        }
        public String NotesPayableUnderCollectionName
        {
            get { return mNotesPayableUnderCollectionName; }
            set { mNotesPayableUnderCollectionName = value; }
        }
        public Int32 NotesReceivable
        {
            get { return mNotesReceivable; }
            set { mNotesReceivable = value; }
        }
        public String NotesReceivableName
        {
            get { return mNotesReceivableName; }
            set { mNotesReceivableName = value; }
        }
        public Int32 NotesReceivableUnderCollection
        {
            get { return mNotesReceivableUnderCollection; }
            set { mNotesReceivableUnderCollection = value; }
        }
        public String NotesReceivableUnderCollectionName
        {
            get { return mNotesReceivableUnderCollectionName; }
            set { mNotesReceivableUnderCollectionName = value; }
        }
        public Int32 CollectionExpenses
        {
            get { return mCollectionExpenses; }
            set { mCollectionExpenses = value; }
        }
        public String CollectionExpensesName
        {
            get { return mCollectionExpensesName; }
            set { mCollectionExpensesName = value; }
        }
        public Decimal BankMinimumLimit
        {
            get { return mBankMinimumLimit; }
            set { mBankMinimumLimit = value; }
        }
        public Int32 BankDocumentID
        {
            get { return mBankDocumentID; }
            set { mBankDocumentID = value; }
        }
        public Int32 InJournalTypeID
        {
            get { return mInJournalTypeID; }
            set { mInJournalTypeID = value; }
        }
        public String InJournalTypeName
        {
            get { return mInJournalTypeName; }
            set { mInJournalTypeName = value; }
        }
        public Int32 OutJournalTypeID
        {
            get { return mOutJournalTypeID; }
            set { mOutJournalTypeID = value; }
        }
        public String OutJournalTypeName
        {
            get { return mOutJournalTypeName; }
            set { mOutJournalTypeName = value; }
        }
        public String PrintedAs
        {
            get { return mPrintedAs; }
            set { mPrintedAs = value; }
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

    public partial class CvwBankAccount
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
        public List<CVarvwBankAccount> lstCVarvwBankAccount = new List<CVarvwBankAccount>();
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
            lstCVarvwBankAccount.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwBankAccount";
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
                        CVarvwBankAccount ObjCVarvwBankAccount = new CVarvwBankAccount();
                        ObjCVarvwBankAccount.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwBankAccount.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwBankAccount.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwBankAccount.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwBankAccount.mAccountName = Convert.ToString(dr["AccountName"].ToString());
                        ObjCVarvwBankAccount.mAccountNumber = Convert.ToString(dr["AccountNumber"].ToString());
                        ObjCVarvwBankAccount.mDefaultCurrencyID = Convert.ToInt32(dr["DefaultCurrencyID"].ToString());
                        ObjCVarvwBankAccount.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwBankAccount.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwBankAccount.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwBankAccount.mPaymentsCount = Convert.ToInt32(dr["PaymentsCount"].ToString());
                        ObjCVarvwBankAccount.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwBankAccount.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwBankAccount.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwBankAccount.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwBankAccount.mAccount_ID = Convert.ToInt32(dr["Account_ID"].ToString());
                        ObjCVarvwBankAccount.mAccountName_ERP = Convert.ToString(dr["AccountName_ERP"].ToString());
                        ObjCVarvwBankAccount.mNotesPayable = Convert.ToInt32(dr["NotesPayable"].ToString());
                        ObjCVarvwBankAccount.mNotesPayableName = Convert.ToString(dr["NotesPayableName"].ToString());
                        ObjCVarvwBankAccount.mNotesPayableUnderCollection = Convert.ToInt32(dr["NotesPayableUnderCollection"].ToString());
                        ObjCVarvwBankAccount.mNotesPayableUnderCollectionName = Convert.ToString(dr["NotesPayableUnderCollectionName"].ToString());
                        ObjCVarvwBankAccount.mNotesReceivable = Convert.ToInt32(dr["NotesReceivable"].ToString());
                        ObjCVarvwBankAccount.mNotesReceivableName = Convert.ToString(dr["NotesReceivableName"].ToString());
                        ObjCVarvwBankAccount.mNotesReceivableUnderCollection = Convert.ToInt32(dr["NotesReceivableUnderCollection"].ToString());
                        ObjCVarvwBankAccount.mNotesReceivableUnderCollectionName = Convert.ToString(dr["NotesReceivableUnderCollectionName"].ToString());
                        ObjCVarvwBankAccount.mCollectionExpenses = Convert.ToInt32(dr["CollectionExpenses"].ToString());
                        ObjCVarvwBankAccount.mCollectionExpensesName = Convert.ToString(dr["CollectionExpensesName"].ToString());
                        ObjCVarvwBankAccount.mBankMinimumLimit = Convert.ToDecimal(dr["BankMinimumLimit"].ToString());
                        ObjCVarvwBankAccount.mBankDocumentID = Convert.ToInt32(dr["BankDocumentID"].ToString());
                        ObjCVarvwBankAccount.mInJournalTypeID = Convert.ToInt32(dr["InJournalTypeID"].ToString());
                        ObjCVarvwBankAccount.mInJournalTypeName = Convert.ToString(dr["InJournalTypeName"].ToString());
                        ObjCVarvwBankAccount.mOutJournalTypeID = Convert.ToInt32(dr["OutJournalTypeID"].ToString());
                        ObjCVarvwBankAccount.mOutJournalTypeName = Convert.ToString(dr["OutJournalTypeName"].ToString());
                        ObjCVarvwBankAccount.mPrintedAs = Convert.ToString(dr["PrintedAs"].ToString());
                        lstCVarvwBankAccount.Add(ObjCVarvwBankAccount);
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
            lstCVarvwBankAccount.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwBankAccount";
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
                        CVarvwBankAccount ObjCVarvwBankAccount = new CVarvwBankAccount();
                        ObjCVarvwBankAccount.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwBankAccount.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwBankAccount.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwBankAccount.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwBankAccount.mAccountName = Convert.ToString(dr["AccountName"].ToString());
                        ObjCVarvwBankAccount.mAccountNumber = Convert.ToString(dr["AccountNumber"].ToString());
                        ObjCVarvwBankAccount.mDefaultCurrencyID = Convert.ToInt32(dr["DefaultCurrencyID"].ToString());
                        ObjCVarvwBankAccount.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwBankAccount.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwBankAccount.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwBankAccount.mPaymentsCount = Convert.ToInt32(dr["PaymentsCount"].ToString());
                        ObjCVarvwBankAccount.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwBankAccount.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwBankAccount.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwBankAccount.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwBankAccount.mAccount_ID = Convert.ToInt32(dr["Account_ID"].ToString());
                        ObjCVarvwBankAccount.mAccountName_ERP = Convert.ToString(dr["AccountName_ERP"].ToString());
                        ObjCVarvwBankAccount.mNotesPayable = Convert.ToInt32(dr["NotesPayable"].ToString());
                        ObjCVarvwBankAccount.mNotesPayableName = Convert.ToString(dr["NotesPayableName"].ToString());
                        ObjCVarvwBankAccount.mNotesPayableUnderCollection = Convert.ToInt32(dr["NotesPayableUnderCollection"].ToString());
                        ObjCVarvwBankAccount.mNotesPayableUnderCollectionName = Convert.ToString(dr["NotesPayableUnderCollectionName"].ToString());
                        ObjCVarvwBankAccount.mNotesReceivable = Convert.ToInt32(dr["NotesReceivable"].ToString());
                        ObjCVarvwBankAccount.mNotesReceivableName = Convert.ToString(dr["NotesReceivableName"].ToString());
                        ObjCVarvwBankAccount.mNotesReceivableUnderCollection = Convert.ToInt32(dr["NotesReceivableUnderCollection"].ToString());
                        ObjCVarvwBankAccount.mNotesReceivableUnderCollectionName = Convert.ToString(dr["NotesReceivableUnderCollectionName"].ToString());
                        ObjCVarvwBankAccount.mCollectionExpenses = Convert.ToInt32(dr["CollectionExpenses"].ToString());
                        ObjCVarvwBankAccount.mCollectionExpensesName = Convert.ToString(dr["CollectionExpensesName"].ToString());
                        ObjCVarvwBankAccount.mBankMinimumLimit = Convert.ToDecimal(dr["BankMinimumLimit"].ToString());
                        ObjCVarvwBankAccount.mBankDocumentID = Convert.ToInt32(dr["BankDocumentID"].ToString());
                        ObjCVarvwBankAccount.mInJournalTypeID = Convert.ToInt32(dr["InJournalTypeID"].ToString());
                        ObjCVarvwBankAccount.mInJournalTypeName = Convert.ToString(dr["InJournalTypeName"].ToString());
                        ObjCVarvwBankAccount.mOutJournalTypeID = Convert.ToInt32(dr["OutJournalTypeID"].ToString());
                        ObjCVarvwBankAccount.mOutJournalTypeName = Convert.ToString(dr["OutJournalTypeName"].ToString());
                        ObjCVarvwBankAccount.mPrintedAs = Convert.ToString(dr["PrintedAs"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwBankAccount.Add(ObjCVarvwBankAccount);
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
