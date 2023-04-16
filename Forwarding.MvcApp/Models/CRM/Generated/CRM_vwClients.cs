﻿using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.CRM.CRM_Clients.Generated
{
    [Serializable]
    public class CPKvwCRM_Clients
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
    public partial class CVarvwCRM_Clients : CPKvwCRM_Clients
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mName;
        internal String mLocalName;
        internal Int32 mCode;
        internal Int32 mCountryID;
        internal String mCountryName;
        internal String mPhone1;
        internal String mPhone2;
        internal String mCellPhone;
        internal String mFax;
        internal String mEmail;
        internal Int32 mSourceID;
        internal DateTime mSourceDate;
        internal String mSourceDescription;
        internal String mWebSite;
        internal Boolean mIsIsoTanks;
        internal Boolean mIsFlexiTanks;
        internal Int32 mSalesmanID;
        internal String mUsername;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal String mNotes;
        internal Int32 mCompanyView;
        internal Int32 mCompanySize;
        internal Int32 mCompanyType;
        internal Int32 mClientStatus;
        internal Int32 mModificationUserID;
        internal DateTime mModificationDate;
        internal DateTime mEstablishDate;
        internal Boolean mIsAddedToCustomer;
        internal String mAddress;
        internal Int32 mPortID;
        internal String mPortName;
        internal String mLastAction;
        internal DateTime mActionDate;
        internal String mFollowUpNote;
        internal String mFollowUpStatusName;
        internal String mPipeLineStageName;
        internal Int32 mCommodityID;
        internal String mCommodityName;
        internal Int32 mActivityID;
        internal Int32 mCurrencyID;
        internal Decimal mRevenue;
        internal Decimal mCost;
        internal Decimal mGrossProfit;
        internal DateTime mStartingDate;
        internal DateTime mClosingExpectedDate;
        internal String mTradeLane;
        internal Int32 mContainerTypeID;
        internal Int32 mBusinessVolume;
        internal String mCompetitors;
        internal Int32 mPaymentTermID;
        internal Int32 mPipeLineStageID;
        internal Int32 mLeadStatusID;
        internal String mLeadStatusName;
        internal Int32 mClientTypeID;
        internal String mClientTypeName;
        internal String mComment;
        internal Boolean mClientIsInValid;
        internal Decimal mDaysInValid;
        internal String mPipeLineStageUsersIDs;
        internal String mClientUsersIDs;
        internal Int32 mIndustryTypeID;
        internal String mIndustryTypeName;
        internal String mActualCustomer;
        internal String mLostReason;
        internal String mEndUserName;
        internal Int32 mCustomerID;
        internal String mCustomerName;
        internal String mCustomerLocalName;
        #endregion

        #region "Methods"
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
        public Int32 Code
        {
            get { return mCode; }
            set { mCode = value; }
        }
        public Int32 CountryID
        {
            get { return mCountryID; }
            set { mCountryID = value; }
        }
        public String CountryName
        {
            get { return mCountryName; }
            set { mCountryName = value; }
        }
        public String Phone1
        {
            get { return mPhone1; }
            set { mPhone1 = value; }
        }
        public String Phone2
        {
            get { return mPhone2; }
            set { mPhone2 = value; }
        }
        public String CellPhone
        {
            get { return mCellPhone; }
            set { mCellPhone = value; }
        }
        public String Fax
        {
            get { return mFax; }
            set { mFax = value; }
        }
        public String Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }
        public Int32 SourceID
        {
            get { return mSourceID; }
            set { mSourceID = value; }
        }
        public DateTime SourceDate
        {
            get { return mSourceDate; }
            set { mSourceDate = value; }
        }
        public String SourceDescription
        {
            get { return mSourceDescription; }
            set { mSourceDescription = value; }
        }
        public String WebSite
        {
            get { return mWebSite; }
            set { mWebSite = value; }
        }
        public Boolean IsIsoTanks
        {
            get { return mIsIsoTanks; }
            set { mIsIsoTanks = value; }
        }
        public Boolean IsFlexiTanks
        {
            get { return mIsFlexiTanks; }
            set { mIsFlexiTanks = value; }
        }
        public Int32 SalesmanID
        {
            get { return mSalesmanID; }
            set { mSalesmanID = value; }
        }
        public String Username
        {
            get { return mUsername; }
            set { mUsername = value; }
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
        public String Notes
        {
            get { return mNotes; }
            set { mNotes = value; }
        }
        public Int32 CompanyView
        {
            get { return mCompanyView; }
            set { mCompanyView = value; }
        }
        public Int32 CompanySize
        {
            get { return mCompanySize; }
            set { mCompanySize = value; }
        }
        public Int32 CompanyType
        {
            get { return mCompanyType; }
            set { mCompanyType = value; }
        }
        public Int32 ClientStatus
        {
            get { return mClientStatus; }
            set { mClientStatus = value; }
        }
        public Int32 ModificationUserID
        {
            get { return mModificationUserID; }
            set { mModificationUserID = value; }
        }
        public DateTime ModificationDate
        {
            get { return mModificationDate; }
            set { mModificationDate = value; }
        }
        public DateTime EstablishDate
        {
            get { return mEstablishDate; }
            set { mEstablishDate = value; }
        }
        public Boolean IsAddedToCustomer
        {
            get { return mIsAddedToCustomer; }
            set { mIsAddedToCustomer = value; }
        }
        public String Address
        {
            get { return mAddress; }
            set { mAddress = value; }
        }
        public Int32 PortID
        {
            get { return mPortID; }
            set { mPortID = value; }
        }
        public String PortName
        {
            get { return mPortName; }
            set { mPortName = value; }
        }
        public String LastAction
        {
            get { return mLastAction; }
            set { mLastAction = value; }
        }
        public DateTime ActionDate
        {
            get { return mActionDate; }
            set { mActionDate = value; }
        }
        public String FollowUpNote
        {
            get { return mFollowUpNote; }
            set { mFollowUpNote = value; }
        }
        public String FollowUpStatusName
        {
            get { return mFollowUpStatusName; }
            set { mFollowUpStatusName = value; }
        }
        public String PipeLineStageName
        {
            get { return mPipeLineStageName; }
            set { mPipeLineStageName = value; }
        }
        public Int32 CommodityID
        {
            get { return mCommodityID; }
            set { mCommodityID = value; }
        }
        public String CommodityName
        {
            get { return mCommodityName; }
            set { mCommodityName = value; }
        }
        public Int32 ActivityID
        {
            get { return mActivityID; }
            set { mActivityID = value; }
        }
        public Int32 CurrencyID
        {
            get { return mCurrencyID; }
            set { mCurrencyID = value; }
        }
        public Decimal Revenue
        {
            get { return mRevenue; }
            set { mRevenue = value; }
        }
        public Decimal Cost
        {
            get { return mCost; }
            set { mCost = value; }
        }
        public Decimal GrossProfit
        {
            get { return mGrossProfit; }
            set { mGrossProfit = value; }
        }
        public DateTime StartingDate
        {
            get { return mStartingDate; }
            set { mStartingDate = value; }
        }
        public DateTime ClosingExpectedDate
        {
            get { return mClosingExpectedDate; }
            set { mClosingExpectedDate = value; }
        }
        public String TradeLane
        {
            get { return mTradeLane; }
            set { mTradeLane = value; }
        }
        public Int32 ContainerTypeID
        {
            get { return mContainerTypeID; }
            set { mContainerTypeID = value; }
        }
        public Int32 BusinessVolume
        {
            get { return mBusinessVolume; }
            set { mBusinessVolume = value; }
        }
        public String Competitors
        {
            get { return mCompetitors; }
            set { mCompetitors = value; }
        }
        public Int32 PaymentTermID
        {
            get { return mPaymentTermID; }
            set { mPaymentTermID = value; }
        }
        public Int32 PipeLineStageID
        {
            get { return mPipeLineStageID; }
            set { mPipeLineStageID = value; }
        }
        public Int32 LeadStatusID
        {
            get { return mLeadStatusID; }
            set { mLeadStatusID = value; }
        }
        public String LeadStatusName
        {
            get { return mLeadStatusName; }
            set { mLeadStatusName = value; }
        }
        public Int32 ClientTypeID
        {
            get { return mClientTypeID; }
            set { mClientTypeID = value; }
        }
        public String ClientTypeName
        {
            get { return mClientTypeName; }
            set { mClientTypeName = value; }
        }
        public String Comment
        {
            get { return mComment; }
            set { mComment = value; }
        }
        public Boolean ClientIsInValid
        {
            get { return mClientIsInValid; }
            set { mClientIsInValid = value; }
        }
        public Decimal DaysInValid
        {
            get { return mDaysInValid; }
            set { mDaysInValid = value; }
        }
        public String PipeLineStageUsersIDs
        {
            get { return mPipeLineStageUsersIDs; }
            set { mPipeLineStageUsersIDs = value; }
        }
        public String ClientUsersIDs
        {
            get { return mClientUsersIDs; }
            set { mClientUsersIDs = value; }
        }
        public Int32 IndustryTypeID
        {
            get { return mIndustryTypeID; }
            set { mIndustryTypeID = value; }
        }
        public String IndustryTypeName
        {
            get { return mIndustryTypeName; }
            set { mIndustryTypeName = value; }
        }
        public String ActualCustomer
        {
            get { return mActualCustomer; }
            set { mActualCustomer = value; }
        }
        public String LostReason
        {
            get { return mLostReason; }
            set { mLostReason = value; }
        }
        public String EndUserName
        {
            get { return mEndUserName; }
            set { mEndUserName = value; }
        }
        public Int32 CustomerID
        {
            get { return mCustomerID; }
            set { mCustomerID = value; }
        }
        public String CustomerName
        {
            get { return mCustomerName; }
            set { mCustomerName = value; }
        }
        public String CustomerLocalName
        {
            get { return mCustomerLocalName; }
            set { mCustomerLocalName = value; }
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

    public partial class CvwCRM_Clients
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
        public List<CVarvwCRM_Clients> lstCVarvwCRM_Clients = new List<CVarvwCRM_Clients>();
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
            lstCVarvwCRM_Clients.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwCRM_Clients";
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
                        CVarvwCRM_Clients ObjCVarvwCRM_Clients = new CVarvwCRM_Clients();
                        ObjCVarvwCRM_Clients.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwCRM_Clients.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwCRM_Clients.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwCRM_Clients.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarvwCRM_Clients.mCountryID = Convert.ToInt32(dr["CountryID"].ToString());
                        ObjCVarvwCRM_Clients.mCountryName = Convert.ToString(dr["CountryName"].ToString());
                        ObjCVarvwCRM_Clients.mPhone1 = Convert.ToString(dr["Phone1"].ToString());
                        ObjCVarvwCRM_Clients.mPhone2 = Convert.ToString(dr["Phone2"].ToString());
                        ObjCVarvwCRM_Clients.mCellPhone = Convert.ToString(dr["CellPhone"].ToString());
                        ObjCVarvwCRM_Clients.mFax = Convert.ToString(dr["Fax"].ToString());
                        ObjCVarvwCRM_Clients.mEmail = Convert.ToString(dr["Email"].ToString());
                        ObjCVarvwCRM_Clients.mSourceID = Convert.ToInt32(dr["SourceID"].ToString());
                        ObjCVarvwCRM_Clients.mSourceDate = Convert.ToDateTime(dr["SourceDate"].ToString());
                        ObjCVarvwCRM_Clients.mSourceDescription = Convert.ToString(dr["SourceDescription"].ToString());
                        ObjCVarvwCRM_Clients.mWebSite = Convert.ToString(dr["WebSite"].ToString());
                        ObjCVarvwCRM_Clients.mIsIsoTanks = Convert.ToBoolean(dr["IsIsoTanks"].ToString());
                        ObjCVarvwCRM_Clients.mIsFlexiTanks = Convert.ToBoolean(dr["IsFlexiTanks"].ToString());
                        ObjCVarvwCRM_Clients.mSalesmanID = Convert.ToInt32(dr["SalesmanID"].ToString());
                        ObjCVarvwCRM_Clients.mUsername = Convert.ToString(dr["Username"].ToString());
                        ObjCVarvwCRM_Clients.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwCRM_Clients.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwCRM_Clients.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwCRM_Clients.mCompanyView = Convert.ToInt32(dr["CompanyView"].ToString());
                        ObjCVarvwCRM_Clients.mCompanySize = Convert.ToInt32(dr["CompanySize"].ToString());
                        ObjCVarvwCRM_Clients.mCompanyType = Convert.ToInt32(dr["CompanyType"].ToString());
                        ObjCVarvwCRM_Clients.mClientStatus = Convert.ToInt32(dr["ClientStatus"].ToString());
                        ObjCVarvwCRM_Clients.mModificationUserID = Convert.ToInt32(dr["ModificationUserID"].ToString());
                        ObjCVarvwCRM_Clients.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwCRM_Clients.mEstablishDate = Convert.ToDateTime(dr["EstablishDate"].ToString());
                        ObjCVarvwCRM_Clients.mIsAddedToCustomer = Convert.ToBoolean(dr["IsAddedToCustomer"].ToString());
                        ObjCVarvwCRM_Clients.mAddress = Convert.ToString(dr["Address"].ToString());
                        ObjCVarvwCRM_Clients.mPortID = Convert.ToInt32(dr["PortID"].ToString());
                        ObjCVarvwCRM_Clients.mPortName = Convert.ToString(dr["PortName"].ToString());
                        ObjCVarvwCRM_Clients.mLastAction = Convert.ToString(dr["LastAction"].ToString());
                        ObjCVarvwCRM_Clients.mActionDate = Convert.ToDateTime(dr["ActionDate"].ToString());
                        ObjCVarvwCRM_Clients.mFollowUpNote = Convert.ToString(dr["FollowUpNote"].ToString());
                        ObjCVarvwCRM_Clients.mFollowUpStatusName = Convert.ToString(dr["FollowUpStatusName"].ToString());
                        ObjCVarvwCRM_Clients.mPipeLineStageName = Convert.ToString(dr["PipeLineStageName"].ToString());
                        ObjCVarvwCRM_Clients.mCommodityID = Convert.ToInt32(dr["CommodityID"].ToString());
                        ObjCVarvwCRM_Clients.mCommodityName = Convert.ToString(dr["CommodityName"].ToString());
                        ObjCVarvwCRM_Clients.mActivityID = Convert.ToInt32(dr["ActivityID"].ToString());
                        ObjCVarvwCRM_Clients.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwCRM_Clients.mRevenue = Convert.ToDecimal(dr["Revenue"].ToString());
                        ObjCVarvwCRM_Clients.mCost = Convert.ToDecimal(dr["Cost"].ToString());
                        ObjCVarvwCRM_Clients.mGrossProfit = Convert.ToDecimal(dr["GrossProfit"].ToString());
                        ObjCVarvwCRM_Clients.mStartingDate = Convert.ToDateTime(dr["StartingDate"].ToString());
                        ObjCVarvwCRM_Clients.mClosingExpectedDate = Convert.ToDateTime(dr["ClosingExpectedDate"].ToString());
                        ObjCVarvwCRM_Clients.mTradeLane = Convert.ToString(dr["TradeLane"].ToString());
                        ObjCVarvwCRM_Clients.mContainerTypeID = Convert.ToInt32(dr["ContainerTypeID"].ToString());
                        ObjCVarvwCRM_Clients.mBusinessVolume = Convert.ToInt32(dr["BusinessVolume"].ToString());
                        ObjCVarvwCRM_Clients.mCompetitors = Convert.ToString(dr["Competitors"].ToString());
                        ObjCVarvwCRM_Clients.mPaymentTermID = Convert.ToInt32(dr["PaymentTermID"].ToString());
                        ObjCVarvwCRM_Clients.mPipeLineStageID = Convert.ToInt32(dr["PipeLineStageID"].ToString());
                        ObjCVarvwCRM_Clients.mLeadStatusID = Convert.ToInt32(dr["LeadStatusID"].ToString());
                        ObjCVarvwCRM_Clients.mLeadStatusName = Convert.ToString(dr["LeadStatusName"].ToString());
                        ObjCVarvwCRM_Clients.mClientTypeID = Convert.ToInt32(dr["ClientTypeID"].ToString());
                        ObjCVarvwCRM_Clients.mClientTypeName = Convert.ToString(dr["ClientTypeName"].ToString());
                        ObjCVarvwCRM_Clients.mComment = Convert.ToString(dr["Comment"].ToString());
                        ObjCVarvwCRM_Clients.mClientIsInValid = Convert.ToBoolean(dr["ClientIsInValid"].ToString());
                        ObjCVarvwCRM_Clients.mDaysInValid = Convert.ToDecimal(dr["DaysInValid"].ToString());
                        ObjCVarvwCRM_Clients.mPipeLineStageUsersIDs = Convert.ToString(dr["PipeLineStageUsersIDs"].ToString());
                        ObjCVarvwCRM_Clients.mClientUsersIDs = Convert.ToString(dr["ClientUsersIDs"].ToString());
                        ObjCVarvwCRM_Clients.mIndustryTypeID = Convert.ToInt32(dr["IndustryTypeID"].ToString());
                        ObjCVarvwCRM_Clients.mIndustryTypeName = Convert.ToString(dr["IndustryTypeName"].ToString());
                        ObjCVarvwCRM_Clients.mActualCustomer = Convert.ToString(dr["ActualCustomer"].ToString());
                        ObjCVarvwCRM_Clients.mLostReason = Convert.ToString(dr["LostReason"].ToString());
                        ObjCVarvwCRM_Clients.mEndUserName = Convert.ToString(dr["EndUserName"].ToString());
                        ObjCVarvwCRM_Clients.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarvwCRM_Clients.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwCRM_Clients.mCustomerLocalName = Convert.ToString(dr["CustomerLocalName"].ToString());
                        lstCVarvwCRM_Clients.Add(ObjCVarvwCRM_Clients);
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
            lstCVarvwCRM_Clients.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwCRM_Clients";
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
                        CVarvwCRM_Clients ObjCVarvwCRM_Clients = new CVarvwCRM_Clients();
                        ObjCVarvwCRM_Clients.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwCRM_Clients.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwCRM_Clients.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwCRM_Clients.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarvwCRM_Clients.mCountryID = Convert.ToInt32(dr["CountryID"].ToString());
                        ObjCVarvwCRM_Clients.mCountryName = Convert.ToString(dr["CountryName"].ToString());
                        ObjCVarvwCRM_Clients.mPhone1 = Convert.ToString(dr["Phone1"].ToString());
                        ObjCVarvwCRM_Clients.mPhone2 = Convert.ToString(dr["Phone2"].ToString());
                        ObjCVarvwCRM_Clients.mCellPhone = Convert.ToString(dr["CellPhone"].ToString());
                        ObjCVarvwCRM_Clients.mFax = Convert.ToString(dr["Fax"].ToString());
                        ObjCVarvwCRM_Clients.mEmail = Convert.ToString(dr["Email"].ToString());
                        ObjCVarvwCRM_Clients.mSourceID = Convert.ToInt32(dr["SourceID"].ToString());
                        ObjCVarvwCRM_Clients.mSourceDate = Convert.ToDateTime(dr["SourceDate"].ToString());
                        ObjCVarvwCRM_Clients.mSourceDescription = Convert.ToString(dr["SourceDescription"].ToString());
                        ObjCVarvwCRM_Clients.mWebSite = Convert.ToString(dr["WebSite"].ToString());
                        ObjCVarvwCRM_Clients.mIsIsoTanks = Convert.ToBoolean(dr["IsIsoTanks"].ToString());
                        ObjCVarvwCRM_Clients.mIsFlexiTanks = Convert.ToBoolean(dr["IsFlexiTanks"].ToString());
                        ObjCVarvwCRM_Clients.mSalesmanID = Convert.ToInt32(dr["SalesmanID"].ToString());
                        ObjCVarvwCRM_Clients.mUsername = Convert.ToString(dr["Username"].ToString());
                        ObjCVarvwCRM_Clients.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwCRM_Clients.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwCRM_Clients.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwCRM_Clients.mCompanyView = Convert.ToInt32(dr["CompanyView"].ToString());
                        ObjCVarvwCRM_Clients.mCompanySize = Convert.ToInt32(dr["CompanySize"].ToString());
                        ObjCVarvwCRM_Clients.mCompanyType = Convert.ToInt32(dr["CompanyType"].ToString());
                        ObjCVarvwCRM_Clients.mClientStatus = Convert.ToInt32(dr["ClientStatus"].ToString());
                        ObjCVarvwCRM_Clients.mModificationUserID = Convert.ToInt32(dr["ModificationUserID"].ToString());
                        ObjCVarvwCRM_Clients.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwCRM_Clients.mEstablishDate = Convert.ToDateTime(dr["EstablishDate"].ToString());
                        ObjCVarvwCRM_Clients.mIsAddedToCustomer = Convert.ToBoolean(dr["IsAddedToCustomer"].ToString());
                        ObjCVarvwCRM_Clients.mAddress = Convert.ToString(dr["Address"].ToString());
                        ObjCVarvwCRM_Clients.mPortID = Convert.ToInt32(dr["PortID"].ToString());
                        ObjCVarvwCRM_Clients.mPortName = Convert.ToString(dr["PortName"].ToString());
                        ObjCVarvwCRM_Clients.mLastAction = Convert.ToString(dr["LastAction"].ToString());
                        ObjCVarvwCRM_Clients.mActionDate = Convert.ToDateTime(dr["ActionDate"].ToString());
                        ObjCVarvwCRM_Clients.mFollowUpNote = Convert.ToString(dr["FollowUpNote"].ToString());
                        ObjCVarvwCRM_Clients.mFollowUpStatusName = Convert.ToString(dr["FollowUpStatusName"].ToString());
                        ObjCVarvwCRM_Clients.mPipeLineStageName = Convert.ToString(dr["PipeLineStageName"].ToString());
                        ObjCVarvwCRM_Clients.mCommodityID = Convert.ToInt32(dr["CommodityID"].ToString());
                        ObjCVarvwCRM_Clients.mCommodityName = Convert.ToString(dr["CommodityName"].ToString());
                        ObjCVarvwCRM_Clients.mActivityID = Convert.ToInt32(dr["ActivityID"].ToString());
                        ObjCVarvwCRM_Clients.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwCRM_Clients.mRevenue = Convert.ToDecimal(dr["Revenue"].ToString());
                        ObjCVarvwCRM_Clients.mCost = Convert.ToDecimal(dr["Cost"].ToString());
                        ObjCVarvwCRM_Clients.mGrossProfit = Convert.ToDecimal(dr["GrossProfit"].ToString());
                        ObjCVarvwCRM_Clients.mStartingDate = Convert.ToDateTime(dr["StartingDate"].ToString());
                        ObjCVarvwCRM_Clients.mClosingExpectedDate = Convert.ToDateTime(dr["ClosingExpectedDate"].ToString());
                        ObjCVarvwCRM_Clients.mTradeLane = Convert.ToString(dr["TradeLane"].ToString());
                        ObjCVarvwCRM_Clients.mContainerTypeID = Convert.ToInt32(dr["ContainerTypeID"].ToString());
                        ObjCVarvwCRM_Clients.mBusinessVolume = Convert.ToInt32(dr["BusinessVolume"].ToString());
                        ObjCVarvwCRM_Clients.mCompetitors = Convert.ToString(dr["Competitors"].ToString());
                        ObjCVarvwCRM_Clients.mPaymentTermID = Convert.ToInt32(dr["PaymentTermID"].ToString());
                        ObjCVarvwCRM_Clients.mPipeLineStageID = Convert.ToInt32(dr["PipeLineStageID"].ToString());
                        ObjCVarvwCRM_Clients.mLeadStatusID = Convert.ToInt32(dr["LeadStatusID"].ToString());
                        ObjCVarvwCRM_Clients.mLeadStatusName = Convert.ToString(dr["LeadStatusName"].ToString());
                        ObjCVarvwCRM_Clients.mClientTypeID = Convert.ToInt32(dr["ClientTypeID"].ToString());
                        ObjCVarvwCRM_Clients.mClientTypeName = Convert.ToString(dr["ClientTypeName"].ToString());
                        ObjCVarvwCRM_Clients.mComment = Convert.ToString(dr["Comment"].ToString());
                        ObjCVarvwCRM_Clients.mClientIsInValid = Convert.ToBoolean(dr["ClientIsInValid"].ToString());
                        ObjCVarvwCRM_Clients.mDaysInValid = Convert.ToDecimal(dr["DaysInValid"].ToString());
                        ObjCVarvwCRM_Clients.mPipeLineStageUsersIDs = Convert.ToString(dr["PipeLineStageUsersIDs"].ToString());
                        ObjCVarvwCRM_Clients.mClientUsersIDs = Convert.ToString(dr["ClientUsersIDs"].ToString());
                        ObjCVarvwCRM_Clients.mIndustryTypeID = Convert.ToInt32(dr["IndustryTypeID"].ToString());
                        ObjCVarvwCRM_Clients.mIndustryTypeName = Convert.ToString(dr["IndustryTypeName"].ToString());
                        ObjCVarvwCRM_Clients.mActualCustomer = Convert.ToString(dr["ActualCustomer"].ToString());
                        ObjCVarvwCRM_Clients.mLostReason = Convert.ToString(dr["LostReason"].ToString());
                        ObjCVarvwCRM_Clients.mEndUserName = Convert.ToString(dr["EndUserName"].ToString());
                        ObjCVarvwCRM_Clients.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarvwCRM_Clients.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwCRM_Clients.mCustomerLocalName = Convert.ToString(dr["CustomerLocalName"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwCRM_Clients.Add(ObjCVarvwCRM_Clients);
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
