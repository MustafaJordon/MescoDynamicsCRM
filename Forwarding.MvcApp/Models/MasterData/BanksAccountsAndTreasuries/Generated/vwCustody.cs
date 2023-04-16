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
    public class CPKvwCustody
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
    public partial class CVarvwCustody : CPKvwCustody
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mUserName;
        internal Int32 mUserID;
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
        internal String mAccountName;
        internal Int32 mSubAccountID;
        internal String mSubAccountName;
        internal Int32 mCostCenterID;
        internal String mCostCenterName;
        internal Int32 mSubAccountGroupID;
        internal Int32 mOperationCount;
        #endregion

        #region "Methods"
        public String UserName
        {
            get { return mUserName; }
            set { mUserName = value; }
        }
        public Int32 UserID
        {
            get { return mUserID; }
            set { mUserID = value; }
        }
        public Int32 Code
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
        public String Job
        {
            get { return mJob; }
            set { mJob = value; }
        }
        public String Address
        {
            get { return mAddress; }
            set { mAddress = value; }
        }
        public String Mobile
        {
            get { return mMobile; }
            set { mMobile = value; }
        }
        public String Phone
        {
            get { return mPhone; }
            set { mPhone = value; }
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
        public Int32 AccountID
        {
            get { return mAccountID; }
            set { mAccountID = value; }
        }
        public String AccountName
        {
            get { return mAccountName; }
            set { mAccountName = value; }
        }
        public Int32 SubAccountID
        {
            get { return mSubAccountID; }
            set { mSubAccountID = value; }
        }
        public String SubAccountName
        {
            get { return mSubAccountName; }
            set { mSubAccountName = value; }
        }
        public Int32 CostCenterID
        {
            get { return mCostCenterID; }
            set { mCostCenterID = value; }
        }
        public String CostCenterName
        {
            get { return mCostCenterName; }
            set { mCostCenterName = value; }
        }
        public Int32 SubAccountGroupID
        {
            get { return mSubAccountGroupID; }
            set { mSubAccountGroupID = value; }
        }
        public Int32 OperationCount
        {
            get { return mOperationCount; }
            set { mOperationCount = value; }
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

    public partial class CvwCustody
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
        public List<CVarvwCustody> lstCVarvwCustody = new List<CVarvwCustody>();
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
            lstCVarvwCustody.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwCustody";
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
                        CVarvwCustody ObjCVarvwCustody = new CVarvwCustody();
                        ObjCVarvwCustody.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwCustody.mUserName = Convert.ToString(dr["UserName"].ToString());
                        ObjCVarvwCustody.mUserID = Convert.ToInt32(dr["UserID"].ToString());
                        ObjCVarvwCustody.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarvwCustody.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwCustody.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwCustody.mJob = Convert.ToString(dr["Job"].ToString());
                        ObjCVarvwCustody.mAddress = Convert.ToString(dr["Address"].ToString());
                        ObjCVarvwCustody.mMobile = Convert.ToString(dr["Mobile"].ToString());
                        ObjCVarvwCustody.mPhone = Convert.ToString(dr["Phone"].ToString());
                        ObjCVarvwCustody.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwCustody.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwCustody.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwCustody.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwCustody.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwCustody.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarvwCustody.mAccountName = Convert.ToString(dr["AccountName"].ToString());
                        ObjCVarvwCustody.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarvwCustody.mSubAccountName = Convert.ToString(dr["SubAccountName"].ToString());
                        ObjCVarvwCustody.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarvwCustody.mCostCenterName = Convert.ToString(dr["CostCenterName"].ToString());
                        ObjCVarvwCustody.mSubAccountGroupID = Convert.ToInt32(dr["SubAccountGroupID"].ToString());
                        ObjCVarvwCustody.mOperationCount = Convert.ToInt32(dr["OperationCount"].ToString());
                        lstCVarvwCustody.Add(ObjCVarvwCustody);
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
            lstCVarvwCustody.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwCustody";
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
                        CVarvwCustody ObjCVarvwCustody = new CVarvwCustody();
                        ObjCVarvwCustody.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwCustody.mUserName = Convert.ToString(dr["UserName"].ToString());
                        ObjCVarvwCustody.mUserID = Convert.ToInt32(dr["UserID"].ToString());
                        ObjCVarvwCustody.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarvwCustody.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwCustody.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwCustody.mJob = Convert.ToString(dr["Job"].ToString());
                        ObjCVarvwCustody.mAddress = Convert.ToString(dr["Address"].ToString());
                        ObjCVarvwCustody.mMobile = Convert.ToString(dr["Mobile"].ToString());
                        ObjCVarvwCustody.mPhone = Convert.ToString(dr["Phone"].ToString());
                        ObjCVarvwCustody.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwCustody.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwCustody.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwCustody.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwCustody.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwCustody.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarvwCustody.mAccountName = Convert.ToString(dr["AccountName"].ToString());
                        ObjCVarvwCustody.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarvwCustody.mSubAccountName = Convert.ToString(dr["SubAccountName"].ToString());
                        ObjCVarvwCustody.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarvwCustody.mCostCenterName = Convert.ToString(dr["CostCenterName"].ToString());
                        ObjCVarvwCustody.mSubAccountGroupID = Convert.ToInt32(dr["SubAccountGroupID"].ToString());
                        ObjCVarvwCustody.mOperationCount = Convert.ToInt32(dr["OperationCount"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwCustody.Add(ObjCVarvwCustody);
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
