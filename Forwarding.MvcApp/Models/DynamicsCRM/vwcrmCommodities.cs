﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.DynamicsCRM
{
    [Serializable]
    public class CPKvwcrmCommodities
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
    public partial class CVarvwcrmCommodities : CPKvwcrmCommodities
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mCode;
        internal String mName;
        internal String mLocalName;
        internal String mNotes;
        internal Boolean mIsInactive;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Int32 mLockingUserID;
        internal DateTime mTimeLocked;
        internal Boolean mIsIMO;
        internal Decimal mIMOClass;
        internal Int32 mUNNumber;
        internal String mCommercialName;
        internal String mLoadingTemperature;
        internal String mUnloadingTemperature;
        internal String mDensity;
        internal String mcrmID;
        internal String mForwardingTableName;
        internal String mcrmTableName;
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
        public String Notes
        {
            get { return mNotes; }
            set { mNotes = value; }
        }
        public Boolean IsInactive
        {
            get { return mIsInactive; }
            set { mIsInactive = value; }
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
        public Int32 LockingUserID
        {
            get { return mLockingUserID; }
            set { mLockingUserID = value; }
        }
        public DateTime TimeLocked
        {
            get { return mTimeLocked; }
            set { mTimeLocked = value; }
        }
        public Boolean IsIMO
        {
            get { return mIsIMO; }
            set { mIsIMO = value; }
        }
        public Decimal IMOClass
        {
            get { return mIMOClass; }
            set { mIMOClass = value; }
        }
        public Int32 UNNumber
        {
            get { return mUNNumber; }
            set { mUNNumber = value; }
        }
        public String CommercialName
        {
            get { return mCommercialName; }
            set { mCommercialName = value; }
        }
        public String LoadingTemperature
        {
            get { return mLoadingTemperature; }
            set { mLoadingTemperature = value; }
        }
        public String UnloadingTemperature
        {
            get { return mUnloadingTemperature; }
            set { mUnloadingTemperature = value; }
        }
        public String Density
        {
            get { return mDensity; }
            set { mDensity = value; }
        }
        public String crmID
        {
            get { return mcrmID; }
            set { mcrmID = value; }
        }
        public String ForwardingTableName
        {
            get { return mForwardingTableName; }
            set { mForwardingTableName = value; }
        }
        public String crmTableName
        {
            get { return mcrmTableName; }
            set { mcrmTableName = value; }
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

    public partial class CvwcrmCommodities
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
        public List<CVarvwcrmCommodities> lstCVarvwcrmCommodities = new List<CVarvwcrmCommodities>();
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
            lstCVarvwcrmCommodities.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwcrmCommodities";
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
                        CVarvwcrmCommodities ObjCVarvwcrmCommodities = new CVarvwcrmCommodities();
                        ObjCVarvwcrmCommodities.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwcrmCommodities.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwcrmCommodities.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwcrmCommodities.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwcrmCommodities.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwcrmCommodities.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarvwcrmCommodities.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwcrmCommodities.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwcrmCommodities.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwcrmCommodities.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwcrmCommodities.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarvwcrmCommodities.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarvwcrmCommodities.mIsIMO = Convert.ToBoolean(dr["IsIMO"].ToString());
                        ObjCVarvwcrmCommodities.mIMOClass = Convert.ToDecimal(dr["IMOClass"].ToString());
                        ObjCVarvwcrmCommodities.mUNNumber = Convert.ToInt32(dr["UNNumber"].ToString());
                        ObjCVarvwcrmCommodities.mCommercialName = Convert.ToString(dr["CommercialName"].ToString());
                        ObjCVarvwcrmCommodities.mLoadingTemperature = Convert.ToString(dr["LoadingTemperature"].ToString());
                        ObjCVarvwcrmCommodities.mUnloadingTemperature = Convert.ToString(dr["UnloadingTemperature"].ToString());
                        ObjCVarvwcrmCommodities.mDensity = Convert.ToString(dr["Density"].ToString());
                        ObjCVarvwcrmCommodities.mcrmID = Convert.ToString(dr["crmID"].ToString());
                        ObjCVarvwcrmCommodities.mForwardingTableName = Convert.ToString(dr["ForwardingTableName"].ToString());
                        ObjCVarvwcrmCommodities.mcrmTableName = Convert.ToString(dr["crmTableName"].ToString());
                        lstCVarvwcrmCommodities.Add(ObjCVarvwcrmCommodities);
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
            lstCVarvwcrmCommodities.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwcrmCommodities";
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
                        CVarvwcrmCommodities ObjCVarvwcrmCommodities = new CVarvwcrmCommodities();
                        ObjCVarvwcrmCommodities.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwcrmCommodities.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwcrmCommodities.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwcrmCommodities.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwcrmCommodities.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwcrmCommodities.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarvwcrmCommodities.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwcrmCommodities.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwcrmCommodities.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwcrmCommodities.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwcrmCommodities.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarvwcrmCommodities.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarvwcrmCommodities.mIsIMO = Convert.ToBoolean(dr["IsIMO"].ToString());
                        ObjCVarvwcrmCommodities.mIMOClass = Convert.ToDecimal(dr["IMOClass"].ToString());
                        ObjCVarvwcrmCommodities.mUNNumber = Convert.ToInt32(dr["UNNumber"].ToString());
                        ObjCVarvwcrmCommodities.mCommercialName = Convert.ToString(dr["CommercialName"].ToString());
                        ObjCVarvwcrmCommodities.mLoadingTemperature = Convert.ToString(dr["LoadingTemperature"].ToString());
                        ObjCVarvwcrmCommodities.mUnloadingTemperature = Convert.ToString(dr["UnloadingTemperature"].ToString());
                        ObjCVarvwcrmCommodities.mDensity = Convert.ToString(dr["Density"].ToString());
                        ObjCVarvwcrmCommodities.mcrmID = Convert.ToString(dr["crmID"].ToString());
                        ObjCVarvwcrmCommodities.mForwardingTableName = Convert.ToString(dr["ForwardingTableName"].ToString());
                        ObjCVarvwcrmCommodities.mcrmTableName = Convert.ToString(dr["crmTableName"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwcrmCommodities.Add(ObjCVarvwcrmCommodities);
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