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
    public class CPKvwcrmPorts
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
    public partial class CVarvwcrmPorts : CPKvwcrmPorts
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mCode;
        internal String mName;
        internal String mLocalName;
        internal Int32 mCountryID;
        internal Boolean mIsInactive;
        internal Boolean mIsPort;
        internal Boolean mIsAir;
        internal Boolean mIsOcean;
        internal Boolean mIsInland;
        internal Boolean mIsAddedManually;
        internal String mNotes;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Int32 mLockingUserID;
        internal DateTime mTimeLocked;
        internal String mAddress;
        internal String mTelephoneNumber;
        internal String mEmail;
        internal String mContactPerson;
        internal Boolean mIsFactories;
        internal Int32 mFactoryCityID;
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
        public Int32 CountryID
        {
            get { return mCountryID; }
            set { mCountryID = value; }
        }
        public Boolean IsInactive
        {
            get { return mIsInactive; }
            set { mIsInactive = value; }
        }
        public Boolean IsPort
        {
            get { return mIsPort; }
            set { mIsPort = value; }
        }
        public Boolean IsAir
        {
            get { return mIsAir; }
            set { mIsAir = value; }
        }
        public Boolean IsOcean
        {
            get { return mIsOcean; }
            set { mIsOcean = value; }
        }
        public Boolean IsInland
        {
            get { return mIsInland; }
            set { mIsInland = value; }
        }
        public Boolean IsAddedManually
        {
            get { return mIsAddedManually; }
            set { mIsAddedManually = value; }
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
        public String Address
        {
            get { return mAddress; }
            set { mAddress = value; }
        }
        public String TelephoneNumber
        {
            get { return mTelephoneNumber; }
            set { mTelephoneNumber = value; }
        }
        public String Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }
        public String ContactPerson
        {
            get { return mContactPerson; }
            set { mContactPerson = value; }
        }
        public Boolean IsFactories
        {
            get { return mIsFactories; }
            set { mIsFactories = value; }
        }
        public Int32 FactoryCityID
        {
            get { return mFactoryCityID; }
            set { mFactoryCityID = value; }
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

    public partial class CvwcrmPorts
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
        public List<CVarvwcrmPorts> lstCVarvwcrmPorts = new List<CVarvwcrmPorts>();
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
            lstCVarvwcrmPorts.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwcrmPorts";
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
                        CVarvwcrmPorts ObjCVarvwcrmPorts = new CVarvwcrmPorts();
                        ObjCVarvwcrmPorts.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwcrmPorts.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwcrmPorts.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwcrmPorts.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwcrmPorts.mCountryID = Convert.ToInt32(dr["CountryID"].ToString());
                        ObjCVarvwcrmPorts.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarvwcrmPorts.mIsPort = Convert.ToBoolean(dr["IsPort"].ToString());
                        ObjCVarvwcrmPorts.mIsAir = Convert.ToBoolean(dr["IsAir"].ToString());
                        ObjCVarvwcrmPorts.mIsOcean = Convert.ToBoolean(dr["IsOcean"].ToString());
                        ObjCVarvwcrmPorts.mIsInland = Convert.ToBoolean(dr["IsInland"].ToString());
                        ObjCVarvwcrmPorts.mIsAddedManually = Convert.ToBoolean(dr["IsAddedManually"].ToString());
                        ObjCVarvwcrmPorts.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwcrmPorts.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwcrmPorts.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwcrmPorts.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwcrmPorts.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwcrmPorts.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarvwcrmPorts.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarvwcrmPorts.mAddress = Convert.ToString(dr["Address"].ToString());
                        ObjCVarvwcrmPorts.mTelephoneNumber = Convert.ToString(dr["TelephoneNumber"].ToString());
                        ObjCVarvwcrmPorts.mEmail = Convert.ToString(dr["Email"].ToString());
                        ObjCVarvwcrmPorts.mContactPerson = Convert.ToString(dr["ContactPerson"].ToString());
                        ObjCVarvwcrmPorts.mIsFactories = Convert.ToBoolean(dr["IsFactories"].ToString());
                        ObjCVarvwcrmPorts.mFactoryCityID = Convert.ToInt32(dr["FactoryCityID"].ToString());
                        ObjCVarvwcrmPorts.mcrmID = Convert.ToString(dr["crmID"].ToString());
                        ObjCVarvwcrmPorts.mForwardingTableName = Convert.ToString(dr["ForwardingTableName"].ToString());
                        ObjCVarvwcrmPorts.mcrmTableName = Convert.ToString(dr["crmTableName"].ToString());
                        lstCVarvwcrmPorts.Add(ObjCVarvwcrmPorts);
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
            lstCVarvwcrmPorts.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwcrmPorts";
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
                        CVarvwcrmPorts ObjCVarvwcrmPorts = new CVarvwcrmPorts();
                        ObjCVarvwcrmPorts.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwcrmPorts.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwcrmPorts.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwcrmPorts.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwcrmPorts.mCountryID = Convert.ToInt32(dr["CountryID"].ToString());
                        ObjCVarvwcrmPorts.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarvwcrmPorts.mIsPort = Convert.ToBoolean(dr["IsPort"].ToString());
                        ObjCVarvwcrmPorts.mIsAir = Convert.ToBoolean(dr["IsAir"].ToString());
                        ObjCVarvwcrmPorts.mIsOcean = Convert.ToBoolean(dr["IsOcean"].ToString());
                        ObjCVarvwcrmPorts.mIsInland = Convert.ToBoolean(dr["IsInland"].ToString());
                        ObjCVarvwcrmPorts.mIsAddedManually = Convert.ToBoolean(dr["IsAddedManually"].ToString());
                        ObjCVarvwcrmPorts.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwcrmPorts.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwcrmPorts.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwcrmPorts.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwcrmPorts.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwcrmPorts.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarvwcrmPorts.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarvwcrmPorts.mAddress = Convert.ToString(dr["Address"].ToString());
                        ObjCVarvwcrmPorts.mTelephoneNumber = Convert.ToString(dr["TelephoneNumber"].ToString());
                        ObjCVarvwcrmPorts.mEmail = Convert.ToString(dr["Email"].ToString());
                        ObjCVarvwcrmPorts.mContactPerson = Convert.ToString(dr["ContactPerson"].ToString());
                        ObjCVarvwcrmPorts.mIsFactories = Convert.ToBoolean(dr["IsFactories"].ToString());
                        ObjCVarvwcrmPorts.mFactoryCityID = Convert.ToInt32(dr["FactoryCityID"].ToString());
                        ObjCVarvwcrmPorts.mcrmID = Convert.ToString(dr["crmID"].ToString());
                        ObjCVarvwcrmPorts.mForwardingTableName = Convert.ToString(dr["ForwardingTableName"].ToString());
                        ObjCVarvwcrmPorts.mcrmTableName = Convert.ToString(dr["crmTableName"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwcrmPorts.Add(ObjCVarvwcrmPorts);
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