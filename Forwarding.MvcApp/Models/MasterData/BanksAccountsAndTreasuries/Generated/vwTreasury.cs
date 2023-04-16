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
    public class CPKvwTreasury
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
    public partial class CVarvwTreasury : CPKvwTreasury
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mCode;
        internal String mName;
        internal String mLocalName;
        internal Int32 mBranchID;
        internal String mNotes;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Int32 mAccount_ID;
        internal String mAccountName;
        internal Int32 mCurrencyID;
        internal String mCurrencyCode;
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
        public Int32 BranchID
        {
            get { return mBranchID; }
            set { mBranchID = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mNotes = value; }
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
        public String AccountName
        {
            get { return mAccountName; }
            set { mAccountName = value; }
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

    public partial class CvwTreasury
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
        public List<CVarvwTreasury> lstCVarvwTreasury = new List<CVarvwTreasury>();
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
            lstCVarvwTreasury.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwTreasury";
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
                        CVarvwTreasury ObjCVarvwTreasury = new CVarvwTreasury();
                        ObjCVarvwTreasury.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwTreasury.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwTreasury.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwTreasury.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwTreasury.mBranchID = Convert.ToInt32(dr["BranchID"].ToString());
                        ObjCVarvwTreasury.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwTreasury.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwTreasury.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwTreasury.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwTreasury.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwTreasury.mAccount_ID = Convert.ToInt32(dr["Account_ID"].ToString());
                        ObjCVarvwTreasury.mAccountName = Convert.ToString(dr["AccountName"].ToString());
                        ObjCVarvwTreasury.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwTreasury.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwTreasury.mInJournalTypeID = Convert.ToInt32(dr["InJournalTypeID"].ToString());
                        ObjCVarvwTreasury.mInJournalTypeName = Convert.ToString(dr["InJournalTypeName"].ToString());
                        ObjCVarvwTreasury.mOutJournalTypeID = Convert.ToInt32(dr["OutJournalTypeID"].ToString());
                        ObjCVarvwTreasury.mOutJournalTypeName = Convert.ToString(dr["OutJournalTypeName"].ToString());
                        ObjCVarvwTreasury.mPrintedAs = Convert.ToString(dr["PrintedAs"].ToString());
                        lstCVarvwTreasury.Add(ObjCVarvwTreasury);
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
            lstCVarvwTreasury.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwTreasury";
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
                        CVarvwTreasury ObjCVarvwTreasury = new CVarvwTreasury();
                        ObjCVarvwTreasury.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwTreasury.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwTreasury.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwTreasury.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwTreasury.mBranchID = Convert.ToInt32(dr["BranchID"].ToString());
                        ObjCVarvwTreasury.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwTreasury.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwTreasury.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwTreasury.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwTreasury.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwTreasury.mAccount_ID = Convert.ToInt32(dr["Account_ID"].ToString());
                        ObjCVarvwTreasury.mAccountName = Convert.ToString(dr["AccountName"].ToString());
                        ObjCVarvwTreasury.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwTreasury.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwTreasury.mInJournalTypeID = Convert.ToInt32(dr["InJournalTypeID"].ToString());
                        ObjCVarvwTreasury.mInJournalTypeName = Convert.ToString(dr["InJournalTypeName"].ToString());
                        ObjCVarvwTreasury.mOutJournalTypeID = Convert.ToInt32(dr["OutJournalTypeID"].ToString());
                        ObjCVarvwTreasury.mOutJournalTypeName = Convert.ToString(dr["OutJournalTypeName"].ToString());
                        ObjCVarvwTreasury.mPrintedAs = Convert.ToString(dr["PrintedAs"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwTreasury.Add(ObjCVarvwTreasury);
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
