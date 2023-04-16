﻿using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.Administration.Settings.Generated
{
    [Serializable]
    public class CPKvwDefaults
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
    public partial class CVarvwDefaults : CPKvwDefaults
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mBranchID;
        internal String mBranchCode;
        internal String mBranchName;
        internal String mBranchLocalName;
        internal Int32 mCountryID;
        internal String mCompanyName;
        internal String mCompanyLocalName;
        internal String mAddressLine1;
        internal String mAddressLine2;
        internal String mAddressLine3;
        internal String mAddressLine4;
        internal String mAddressLine5;
        internal String mPhones;
        internal String mFaxes;
        internal String mEmail;
        internal String mWebsite;
        internal Int32 mCurrencyID;
        internal String mCurrencyCode;
        internal Int32 mForeignCurrencyID;
        internal String mForeignCurrencyCode;
        internal String mBankName;
        internal String mAccountName;
        internal String mBankAddress;
        internal String mSwiftCode;
        internal String mAccountNumber;
        internal String mTaxNumber;
        internal Int32 mImportOceanDays;
        internal Int32 mImportAirDays;
        internal Int32 mImportInlandDays;
        internal Int32 mExportOceanDays;
        internal Int32 mExportAirDays;
        internal Int32 mExportInlandDays;
        internal Int32 mDomesticOceanDays;
        internal Int32 mDomesticAirDays;
        internal Int32 mDomesticInlandDays;
        internal Int32 mOperationSerialOption;
        internal Int32 mInvoiceSerialOption;
        internal String mUnEditableCompanyName;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Int32 mUserBranchID;
        internal Int32 mNumberOfAllowedSessions;
        internal DateTime mRenewalDate;
        internal Boolean mIsRenewalMessageSent;
        internal Int32 mDefaultCountryID;
        internal String mDefaultCountryName;
        internal Int32 mDefaultPortID;
        internal String mDefaultPortName;
        internal Int32 mDefaultAgentID;
        internal String mDefaultAgentName;
        internal Int32 mLengthUnitID;
        internal Int32 mWeightUnitID;
        internal Int32 mVolumeUnitID;
        internal String mVatIDNo;
        internal String mCommericalRegNo;
        internal String mSL_InvoicesComments;
        internal String mPurchaseInvoicesComments;
        internal String mInvoiceLeftPosition;
        internal String mInvoiceLeftSignature;
        internal String mInvoiceMiddlePosition;
        internal String mInvoiceMiddleSignature;
        internal String mInvoiceRightPosition;
        internal String mInvoiceRightSignature;
        internal Int32 mSmallBusinessBelow;
        internal Int32 mMediumBusinessBelow;
        internal Boolean mIsTaxOnItems;
        internal Boolean mIsDepartmentOption;
        internal Boolean mIsCreditlimitexceptionperiod;
        internal Boolean mIsRepeatMBL;
        internal Boolean mIsRepeatChargeTypeName;
        internal Boolean mIsAddChargeAuto;
        internal String mEmail_Password;
        internal String mEmail_DisplayName;
        internal String mEmail_Host;
        internal Int32 mEmail_Port;
        internal Boolean mEmail_IsSSL;
        internal String mEmail_Header;
        internal String mEmail_Footer;
        internal Boolean mShowUserSalesmen;
        internal Int32 mNumberOfCustomerLimits;
        internal Int32 mNumberOfActiveCustomersUsers;
        internal Int32 mDefaultTruckerID;
        internal Boolean mIsERP;
        #endregion

        #region "Methods"
        public Int32 BranchID
        {
            get { return mBranchID; }
            set { mBranchID = value; }
        }
        public String BranchCode
        {
            get { return mBranchCode; }
            set { mBranchCode = value; }
        }
        public String BranchName
        {
            get { return mBranchName; }
            set { mBranchName = value; }
        }
        public String BranchLocalName
        {
            get { return mBranchLocalName; }
            set { mBranchLocalName = value; }
        }
        public Int32 CountryID
        {
            get { return mCountryID; }
            set { mCountryID = value; }
        }
        public String CompanyName
        {
            get { return mCompanyName; }
            set { mCompanyName = value; }
        }
        public String CompanyLocalName
        {
            get { return mCompanyLocalName; }
            set { mCompanyLocalName = value; }
        }
        public String AddressLine1
        {
            get { return mAddressLine1; }
            set { mAddressLine1 = value; }
        }
        public String AddressLine2
        {
            get { return mAddressLine2; }
            set { mAddressLine2 = value; }
        }
        public String AddressLine3
        {
            get { return mAddressLine3; }
            set { mAddressLine3 = value; }
        }
        public String AddressLine4
        {
            get { return mAddressLine4; }
            set { mAddressLine4 = value; }
        }
        public String AddressLine5
        {
            get { return mAddressLine5; }
            set { mAddressLine5 = value; }
        }
        public String Phones
        {
            get { return mPhones; }
            set { mPhones = value; }
        }
        public String Faxes
        {
            get { return mFaxes; }
            set { mFaxes = value; }
        }
        public String Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }
        public String Website
        {
            get { return mWebsite; }
            set { mWebsite = value; }
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
        public Int32 ForeignCurrencyID
        {
            get { return mForeignCurrencyID; }
            set { mForeignCurrencyID = value; }
        }
        public String ForeignCurrencyCode
        {
            get { return mForeignCurrencyCode; }
            set { mForeignCurrencyCode = value; }
        }
        public String BankName
        {
            get { return mBankName; }
            set { mBankName = value; }
        }
        public String AccountName
        {
            get { return mAccountName; }
            set { mAccountName = value; }
        }
        public String BankAddress
        {
            get { return mBankAddress; }
            set { mBankAddress = value; }
        }
        public String SwiftCode
        {
            get { return mSwiftCode; }
            set { mSwiftCode = value; }
        }
        public String AccountNumber
        {
            get { return mAccountNumber; }
            set { mAccountNumber = value; }
        }
        public String TaxNumber
        {
            get { return mTaxNumber; }
            set { mTaxNumber = value; }
        }
        public Int32 ImportOceanDays
        {
            get { return mImportOceanDays; }
            set { mImportOceanDays = value; }
        }
        public Int32 ImportAirDays
        {
            get { return mImportAirDays; }
            set { mImportAirDays = value; }
        }
        public Int32 ImportInlandDays
        {
            get { return mImportInlandDays; }
            set { mImportInlandDays = value; }
        }
        public Int32 ExportOceanDays
        {
            get { return mExportOceanDays; }
            set { mExportOceanDays = value; }
        }
        public Int32 ExportAirDays
        {
            get { return mExportAirDays; }
            set { mExportAirDays = value; }
        }
        public Int32 ExportInlandDays
        {
            get { return mExportInlandDays; }
            set { mExportInlandDays = value; }
        }
        public Int32 DomesticOceanDays
        {
            get { return mDomesticOceanDays; }
            set { mDomesticOceanDays = value; }
        }
        public Int32 DomesticAirDays
        {
            get { return mDomesticAirDays; }
            set { mDomesticAirDays = value; }
        }
        public Int32 DomesticInlandDays
        {
            get { return mDomesticInlandDays; }
            set { mDomesticInlandDays = value; }
        }
        public Int32 OperationSerialOption
        {
            get { return mOperationSerialOption; }
            set { mOperationSerialOption = value; }
        }
        public Int32 InvoiceSerialOption
        {
            get { return mInvoiceSerialOption; }
            set { mInvoiceSerialOption = value; }
        }
        public String UnEditableCompanyName
        {
            get { return mUnEditableCompanyName; }
            set { mUnEditableCompanyName = value; }
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
        public Int32 UserBranchID
        {
            get { return mUserBranchID; }
            set { mUserBranchID = value; }
        }
        public Int32 NumberOfAllowedSessions
        {
            get { return mNumberOfAllowedSessions; }
            set { mNumberOfAllowedSessions = value; }
        }
        public DateTime RenewalDate
        {
            get { return mRenewalDate; }
            set { mRenewalDate = value; }
        }
        public Boolean IsRenewalMessageSent
        {
            get { return mIsRenewalMessageSent; }
            set { mIsRenewalMessageSent = value; }
        }
        public Int32 DefaultCountryID
        {
            get { return mDefaultCountryID; }
            set { mDefaultCountryID = value; }
        }
        public String DefaultCountryName
        {
            get { return mDefaultCountryName; }
            set { mDefaultCountryName = value; }
        }
        public Int32 DefaultPortID
        {
            get { return mDefaultPortID; }
            set { mDefaultPortID = value; }
        }
        public String DefaultPortName
        {
            get { return mDefaultPortName; }
            set { mDefaultPortName = value; }
        }
        public Int32 DefaultAgentID
        {
            get { return mDefaultAgentID; }
            set { mDefaultAgentID = value; }
        }
        public String DefaultAgentName
        {
            get { return mDefaultAgentName; }
            set { mDefaultAgentName = value; }
        }
        public Int32 LengthUnitID
        {
            get { return mLengthUnitID; }
            set { mLengthUnitID = value; }
        }
        public Int32 WeightUnitID
        {
            get { return mWeightUnitID; }
            set { mWeightUnitID = value; }
        }
        public Int32 VolumeUnitID
        {
            get { return mVolumeUnitID; }
            set { mVolumeUnitID = value; }
        }
        public String VatIDNo
        {
            get { return mVatIDNo; }
            set { mVatIDNo = value; }
        }
        public String CommericalRegNo
        {
            get { return mCommericalRegNo; }
            set { mCommericalRegNo = value; }
        }
        public String SL_InvoicesComments
        {
            get { return mSL_InvoicesComments; }
            set { mSL_InvoicesComments = value; }
        }
        public String PurchaseInvoicesComments
        {
            get { return mPurchaseInvoicesComments; }
            set { mPurchaseInvoicesComments = value; }
        }
        public String InvoiceLeftPosition
        {
            get { return mInvoiceLeftPosition; }
            set { mInvoiceLeftPosition = value; }
        }
        public String InvoiceLeftSignature
        {
            get { return mInvoiceLeftSignature; }
            set { mInvoiceLeftSignature = value; }
        }
        public String InvoiceMiddlePosition
        {
            get { return mInvoiceMiddlePosition; }
            set { mInvoiceMiddlePosition = value; }
        }
        public String InvoiceMiddleSignature
        {
            get { return mInvoiceMiddleSignature; }
            set { mInvoiceMiddleSignature = value; }
        }
        public String InvoiceRightPosition
        {
            get { return mInvoiceRightPosition; }
            set { mInvoiceRightPosition = value; }
        }
        public String InvoiceRightSignature
        {
            get { return mInvoiceRightSignature; }
            set { mInvoiceRightSignature = value; }
        }
        public Int32 SmallBusinessBelow
        {
            get { return mSmallBusinessBelow; }
            set { mSmallBusinessBelow = value; }
        }
        public Int32 MediumBusinessBelow
        {
            get { return mMediumBusinessBelow; }
            set { mMediumBusinessBelow = value; }
        }
        public Boolean IsTaxOnItems
        {
            get { return mIsTaxOnItems; }
            set { mIsTaxOnItems = value; }
        }
        public Boolean IsDepartmentOption
        {
            get { return mIsDepartmentOption; }
            set { mIsDepartmentOption = value; }
        }
        public Boolean IsCreditlimitexceptionperiod
        {
            get { return mIsCreditlimitexceptionperiod; }
            set { mIsCreditlimitexceptionperiod = value; }
        }
        public Boolean IsRepeatMBL
        {
            get { return mIsRepeatMBL; }
            set { mIsRepeatMBL = value; }
        }
        public Boolean IsRepeatChargeTypeName
        {
            get { return mIsRepeatChargeTypeName; }
            set { mIsRepeatChargeTypeName = value; }
        }
        public Boolean IsAddChargeAuto
        {
            get { return mIsAddChargeAuto; }
            set { mIsAddChargeAuto = value; }
        }
        public String Email_Password
        {
            get { return mEmail_Password; }
            set { mEmail_Password = value; }
        }
        public String Email_DisplayName
        {
            get { return mEmail_DisplayName; }
            set { mEmail_DisplayName = value; }
        }
        public String Email_Host
        {
            get { return mEmail_Host; }
            set { mEmail_Host = value; }
        }
        public Int32 Email_Port
        {
            get { return mEmail_Port; }
            set { mEmail_Port = value; }
        }
        public Boolean Email_IsSSL
        {
            get { return mEmail_IsSSL; }
            set { mEmail_IsSSL = value; }
        }
        public String Email_Header
        {
            get { return mEmail_Header; }
            set { mEmail_Header = value; }
        }
        public String Email_Footer
        {
            get { return mEmail_Footer; }
            set { mEmail_Footer = value; }
        }
        public Boolean ShowUserSalesmen
        {
            get { return mShowUserSalesmen; }
            set { mShowUserSalesmen = value; }
        }
        public Int32 NumberOfCustomerLimits
        {
            get { return mNumberOfCustomerLimits; }
            set { mNumberOfCustomerLimits = value; }
        }
        public Int32 NumberOfActiveCustomersUsers
        {
            get { return mNumberOfActiveCustomersUsers; }
            set { mNumberOfActiveCustomersUsers = value; }
        }
        public Int32 DefaultTruckerID
        {
            get { return mDefaultTruckerID; }
            set { mDefaultTruckerID = value; }
        }
        public Boolean IsERP
        {
            get { return mIsERP; }
            set { mIsERP = value; }
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

    public partial class CvwDefaults
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
        public List<CVarvwDefaults> lstCVarvwDefaults = new List<CVarvwDefaults>();
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
            lstCVarvwDefaults.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwDefaults";
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
                        CVarvwDefaults ObjCVarvwDefaults = new CVarvwDefaults();
                        ObjCVarvwDefaults.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwDefaults.mBranchID = Convert.ToInt32(dr["BranchID"].ToString());
                        ObjCVarvwDefaults.mBranchCode = Convert.ToString(dr["BranchCode"].ToString());
                        ObjCVarvwDefaults.mBranchName = Convert.ToString(dr["BranchName"].ToString());
                        ObjCVarvwDefaults.mBranchLocalName = Convert.ToString(dr["BranchLocalName"].ToString());
                        ObjCVarvwDefaults.mCountryID = Convert.ToInt32(dr["CountryID"].ToString());
                        ObjCVarvwDefaults.mCompanyName = Convert.ToString(dr["CompanyName"].ToString());
                        ObjCVarvwDefaults.mCompanyLocalName = Convert.ToString(dr["CompanyLocalName"].ToString());
                        ObjCVarvwDefaults.mAddressLine1 = Convert.ToString(dr["AddressLine1"].ToString());
                        ObjCVarvwDefaults.mAddressLine2 = Convert.ToString(dr["AddressLine2"].ToString());
                        ObjCVarvwDefaults.mAddressLine3 = Convert.ToString(dr["AddressLine3"].ToString());
                        ObjCVarvwDefaults.mAddressLine4 = Convert.ToString(dr["AddressLine4"].ToString());
                        ObjCVarvwDefaults.mAddressLine5 = Convert.ToString(dr["AddressLine5"].ToString());
                        ObjCVarvwDefaults.mPhones = Convert.ToString(dr["Phones"].ToString());
                        ObjCVarvwDefaults.mFaxes = Convert.ToString(dr["Faxes"].ToString());
                        ObjCVarvwDefaults.mEmail = Convert.ToString(dr["Email"].ToString());
                        ObjCVarvwDefaults.mWebsite = Convert.ToString(dr["Website"].ToString());
                        ObjCVarvwDefaults.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwDefaults.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwDefaults.mForeignCurrencyID = Convert.ToInt32(dr["ForeignCurrencyID"].ToString());
                        ObjCVarvwDefaults.mForeignCurrencyCode = Convert.ToString(dr["ForeignCurrencyCode"].ToString());
                        ObjCVarvwDefaults.mBankName = Convert.ToString(dr["BankName"].ToString());
                        ObjCVarvwDefaults.mAccountName = Convert.ToString(dr["AccountName"].ToString());
                        ObjCVarvwDefaults.mBankAddress = Convert.ToString(dr["BankAddress"].ToString());
                        ObjCVarvwDefaults.mSwiftCode = Convert.ToString(dr["SwiftCode"].ToString());
                        ObjCVarvwDefaults.mAccountNumber = Convert.ToString(dr["AccountNumber"].ToString());
                        ObjCVarvwDefaults.mTaxNumber = Convert.ToString(dr["TaxNumber"].ToString());
                        ObjCVarvwDefaults.mImportOceanDays = Convert.ToInt32(dr["ImportOceanDays"].ToString());
                        ObjCVarvwDefaults.mImportAirDays = Convert.ToInt32(dr["ImportAirDays"].ToString());
                        ObjCVarvwDefaults.mImportInlandDays = Convert.ToInt32(dr["ImportInlandDays"].ToString());
                        ObjCVarvwDefaults.mExportOceanDays = Convert.ToInt32(dr["ExportOceanDays"].ToString());
                        ObjCVarvwDefaults.mExportAirDays = Convert.ToInt32(dr["ExportAirDays"].ToString());
                        ObjCVarvwDefaults.mExportInlandDays = Convert.ToInt32(dr["ExportInlandDays"].ToString());
                        ObjCVarvwDefaults.mDomesticOceanDays = Convert.ToInt32(dr["DomesticOceanDays"].ToString());
                        ObjCVarvwDefaults.mDomesticAirDays = Convert.ToInt32(dr["DomesticAirDays"].ToString());
                        ObjCVarvwDefaults.mDomesticInlandDays = Convert.ToInt32(dr["DomesticInlandDays"].ToString());
                        ObjCVarvwDefaults.mOperationSerialOption = Convert.ToInt32(dr["OperationSerialOption"].ToString());
                        ObjCVarvwDefaults.mInvoiceSerialOption = Convert.ToInt32(dr["InvoiceSerialOption"].ToString());
                        ObjCVarvwDefaults.mUnEditableCompanyName = Convert.ToString(dr["UnEditableCompanyName"].ToString());
                        ObjCVarvwDefaults.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwDefaults.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwDefaults.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwDefaults.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwDefaults.mUserBranchID = Convert.ToInt32(dr["UserBranchID"].ToString());
                        ObjCVarvwDefaults.mNumberOfAllowedSessions = Convert.ToInt32(dr["NumberOfAllowedSessions"].ToString());
                        ObjCVarvwDefaults.mRenewalDate = Convert.ToDateTime(dr["RenewalDate"].ToString());
                        ObjCVarvwDefaults.mIsRenewalMessageSent = Convert.ToBoolean(dr["IsRenewalMessageSent"].ToString());
                        ObjCVarvwDefaults.mDefaultCountryID = Convert.ToInt32(dr["DefaultCountryID"].ToString());
                        ObjCVarvwDefaults.mDefaultCountryName = Convert.ToString(dr["DefaultCountryName"].ToString());
                        ObjCVarvwDefaults.mDefaultPortID = Convert.ToInt32(dr["DefaultPortID"].ToString());
                        ObjCVarvwDefaults.mDefaultPortName = Convert.ToString(dr["DefaultPortName"].ToString());
                        ObjCVarvwDefaults.mDefaultAgentID = Convert.ToInt32(dr["DefaultAgentID"].ToString());
                        ObjCVarvwDefaults.mDefaultAgentName = Convert.ToString(dr["DefaultAgentName"].ToString());
                        ObjCVarvwDefaults.mLengthUnitID = Convert.ToInt32(dr["LengthUnitID"].ToString());
                        ObjCVarvwDefaults.mWeightUnitID = Convert.ToInt32(dr["WeightUnitID"].ToString());
                        ObjCVarvwDefaults.mVolumeUnitID = Convert.ToInt32(dr["VolumeUnitID"].ToString());
                        ObjCVarvwDefaults.mVatIDNo = Convert.ToString(dr["VatIDNo"].ToString());
                        ObjCVarvwDefaults.mCommericalRegNo = Convert.ToString(dr["CommericalRegNo"].ToString());
                        ObjCVarvwDefaults.mSL_InvoicesComments = Convert.ToString(dr["SL_InvoicesComments"].ToString());
                        ObjCVarvwDefaults.mPurchaseInvoicesComments = Convert.ToString(dr["PurchaseInvoicesComments"].ToString());
                        ObjCVarvwDefaults.mInvoiceLeftPosition = Convert.ToString(dr["InvoiceLeftPosition"].ToString());
                        ObjCVarvwDefaults.mInvoiceLeftSignature = Convert.ToString(dr["InvoiceLeftSignature"].ToString());
                        ObjCVarvwDefaults.mInvoiceMiddlePosition = Convert.ToString(dr["InvoiceMiddlePosition"].ToString());
                        ObjCVarvwDefaults.mInvoiceMiddleSignature = Convert.ToString(dr["InvoiceMiddleSignature"].ToString());
                        ObjCVarvwDefaults.mInvoiceRightPosition = Convert.ToString(dr["InvoiceRightPosition"].ToString());
                        ObjCVarvwDefaults.mInvoiceRightSignature = Convert.ToString(dr["InvoiceRightSignature"].ToString());
                        ObjCVarvwDefaults.mSmallBusinessBelow = Convert.ToInt32(dr["SmallBusinessBelow"].ToString());
                        ObjCVarvwDefaults.mMediumBusinessBelow = Convert.ToInt32(dr["MediumBusinessBelow"].ToString());
                        ObjCVarvwDefaults.mIsTaxOnItems = Convert.ToBoolean(dr["IsTaxOnItems"].ToString());
                        ObjCVarvwDefaults.mIsDepartmentOption = Convert.ToBoolean(dr["IsDepartmentOption"].ToString());
                        ObjCVarvwDefaults.mIsCreditlimitexceptionperiod = Convert.ToBoolean(dr["IsCreditlimitexceptionperiod"].ToString());
                        ObjCVarvwDefaults.mIsRepeatMBL = Convert.ToBoolean(dr["IsRepeatMBL"].ToString());
                        ObjCVarvwDefaults.mIsRepeatChargeTypeName = Convert.ToBoolean(dr["IsRepeatChargeTypeName"].ToString());
                        ObjCVarvwDefaults.mIsAddChargeAuto = Convert.ToBoolean(dr["IsAddChargeAuto"].ToString());
                        ObjCVarvwDefaults.mEmail_Password = Convert.ToString(dr["Email_Password"].ToString());
                        ObjCVarvwDefaults.mEmail_DisplayName = Convert.ToString(dr["Email_DisplayName"].ToString());
                        ObjCVarvwDefaults.mEmail_Host = Convert.ToString(dr["Email_Host"].ToString());
                        ObjCVarvwDefaults.mEmail_Port = Convert.ToInt32(dr["Email_Port"].ToString());
                        ObjCVarvwDefaults.mEmail_IsSSL = Convert.ToBoolean(dr["Email_IsSSL"].ToString());
                        ObjCVarvwDefaults.mEmail_Header = Convert.ToString(dr["Email_Header"].ToString());
                        ObjCVarvwDefaults.mEmail_Footer = Convert.ToString(dr["Email_Footer"].ToString());
                        ObjCVarvwDefaults.mShowUserSalesmen = Convert.ToBoolean(dr["ShowUserSalesmen"].ToString());
                        ObjCVarvwDefaults.mNumberOfCustomerLimits = Convert.ToInt32(dr["NumberOfCustomerLimits"].ToString());
                        ObjCVarvwDefaults.mNumberOfActiveCustomersUsers = Convert.ToInt32(dr["NumberOfActiveCustomersUsers"].ToString());
                        ObjCVarvwDefaults.mDefaultTruckerID = Convert.ToInt32(dr["DefaultTruckerID"].ToString());
                        ObjCVarvwDefaults.mIsERP = Convert.ToBoolean(dr["IsERP"].ToString());
                        lstCVarvwDefaults.Add(ObjCVarvwDefaults);
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
            lstCVarvwDefaults.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwDefaults";
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
                        CVarvwDefaults ObjCVarvwDefaults = new CVarvwDefaults();
                        ObjCVarvwDefaults.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwDefaults.mBranchID = Convert.ToInt32(dr["BranchID"].ToString());
                        ObjCVarvwDefaults.mBranchCode = Convert.ToString(dr["BranchCode"].ToString());
                        ObjCVarvwDefaults.mBranchName = Convert.ToString(dr["BranchName"].ToString());
                        ObjCVarvwDefaults.mBranchLocalName = Convert.ToString(dr["BranchLocalName"].ToString());
                        ObjCVarvwDefaults.mCountryID = Convert.ToInt32(dr["CountryID"].ToString());
                        ObjCVarvwDefaults.mCompanyName = Convert.ToString(dr["CompanyName"].ToString());
                        ObjCVarvwDefaults.mCompanyLocalName = Convert.ToString(dr["CompanyLocalName"].ToString());
                        ObjCVarvwDefaults.mAddressLine1 = Convert.ToString(dr["AddressLine1"].ToString());
                        ObjCVarvwDefaults.mAddressLine2 = Convert.ToString(dr["AddressLine2"].ToString());
                        ObjCVarvwDefaults.mAddressLine3 = Convert.ToString(dr["AddressLine3"].ToString());
                        ObjCVarvwDefaults.mAddressLine4 = Convert.ToString(dr["AddressLine4"].ToString());
                        ObjCVarvwDefaults.mAddressLine5 = Convert.ToString(dr["AddressLine5"].ToString());
                        ObjCVarvwDefaults.mPhones = Convert.ToString(dr["Phones"].ToString());
                        ObjCVarvwDefaults.mFaxes = Convert.ToString(dr["Faxes"].ToString());
                        ObjCVarvwDefaults.mEmail = Convert.ToString(dr["Email"].ToString());
                        ObjCVarvwDefaults.mWebsite = Convert.ToString(dr["Website"].ToString());
                        ObjCVarvwDefaults.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwDefaults.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwDefaults.mForeignCurrencyID = Convert.ToInt32(dr["ForeignCurrencyID"].ToString());
                        ObjCVarvwDefaults.mForeignCurrencyCode = Convert.ToString(dr["ForeignCurrencyCode"].ToString());
                        ObjCVarvwDefaults.mBankName = Convert.ToString(dr["BankName"].ToString());
                        ObjCVarvwDefaults.mAccountName = Convert.ToString(dr["AccountName"].ToString());
                        ObjCVarvwDefaults.mBankAddress = Convert.ToString(dr["BankAddress"].ToString());
                        ObjCVarvwDefaults.mSwiftCode = Convert.ToString(dr["SwiftCode"].ToString());
                        ObjCVarvwDefaults.mAccountNumber = Convert.ToString(dr["AccountNumber"].ToString());
                        ObjCVarvwDefaults.mTaxNumber = Convert.ToString(dr["TaxNumber"].ToString());
                        ObjCVarvwDefaults.mImportOceanDays = Convert.ToInt32(dr["ImportOceanDays"].ToString());
                        ObjCVarvwDefaults.mImportAirDays = Convert.ToInt32(dr["ImportAirDays"].ToString());
                        ObjCVarvwDefaults.mImportInlandDays = Convert.ToInt32(dr["ImportInlandDays"].ToString());
                        ObjCVarvwDefaults.mExportOceanDays = Convert.ToInt32(dr["ExportOceanDays"].ToString());
                        ObjCVarvwDefaults.mExportAirDays = Convert.ToInt32(dr["ExportAirDays"].ToString());
                        ObjCVarvwDefaults.mExportInlandDays = Convert.ToInt32(dr["ExportInlandDays"].ToString());
                        ObjCVarvwDefaults.mDomesticOceanDays = Convert.ToInt32(dr["DomesticOceanDays"].ToString());
                        ObjCVarvwDefaults.mDomesticAirDays = Convert.ToInt32(dr["DomesticAirDays"].ToString());
                        ObjCVarvwDefaults.mDomesticInlandDays = Convert.ToInt32(dr["DomesticInlandDays"].ToString());
                        ObjCVarvwDefaults.mOperationSerialOption = Convert.ToInt32(dr["OperationSerialOption"].ToString());
                        ObjCVarvwDefaults.mInvoiceSerialOption = Convert.ToInt32(dr["InvoiceSerialOption"].ToString());
                        ObjCVarvwDefaults.mUnEditableCompanyName = Convert.ToString(dr["UnEditableCompanyName"].ToString());
                        ObjCVarvwDefaults.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwDefaults.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwDefaults.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwDefaults.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwDefaults.mUserBranchID = Convert.ToInt32(dr["UserBranchID"].ToString());
                        ObjCVarvwDefaults.mNumberOfAllowedSessions = Convert.ToInt32(dr["NumberOfAllowedSessions"].ToString());
                        ObjCVarvwDefaults.mRenewalDate = Convert.ToDateTime(dr["RenewalDate"].ToString());
                        ObjCVarvwDefaults.mIsRenewalMessageSent = Convert.ToBoolean(dr["IsRenewalMessageSent"].ToString());
                        ObjCVarvwDefaults.mDefaultCountryID = Convert.ToInt32(dr["DefaultCountryID"].ToString());
                        ObjCVarvwDefaults.mDefaultCountryName = Convert.ToString(dr["DefaultCountryName"].ToString());
                        ObjCVarvwDefaults.mDefaultPortID = Convert.ToInt32(dr["DefaultPortID"].ToString());
                        ObjCVarvwDefaults.mDefaultPortName = Convert.ToString(dr["DefaultPortName"].ToString());
                        ObjCVarvwDefaults.mDefaultAgentID = Convert.ToInt32(dr["DefaultAgentID"].ToString());
                        ObjCVarvwDefaults.mDefaultAgentName = Convert.ToString(dr["DefaultAgentName"].ToString());
                        ObjCVarvwDefaults.mLengthUnitID = Convert.ToInt32(dr["LengthUnitID"].ToString());
                        ObjCVarvwDefaults.mWeightUnitID = Convert.ToInt32(dr["WeightUnitID"].ToString());
                        ObjCVarvwDefaults.mVolumeUnitID = Convert.ToInt32(dr["VolumeUnitID"].ToString());
                        ObjCVarvwDefaults.mVatIDNo = Convert.ToString(dr["VatIDNo"].ToString());
                        ObjCVarvwDefaults.mCommericalRegNo = Convert.ToString(dr["CommericalRegNo"].ToString());
                        ObjCVarvwDefaults.mSL_InvoicesComments = Convert.ToString(dr["SL_InvoicesComments"].ToString());
                        ObjCVarvwDefaults.mPurchaseInvoicesComments = Convert.ToString(dr["PurchaseInvoicesComments"].ToString());
                        ObjCVarvwDefaults.mInvoiceLeftPosition = Convert.ToString(dr["InvoiceLeftPosition"].ToString());
                        ObjCVarvwDefaults.mInvoiceLeftSignature = Convert.ToString(dr["InvoiceLeftSignature"].ToString());
                        ObjCVarvwDefaults.mInvoiceMiddlePosition = Convert.ToString(dr["InvoiceMiddlePosition"].ToString());
                        ObjCVarvwDefaults.mInvoiceMiddleSignature = Convert.ToString(dr["InvoiceMiddleSignature"].ToString());
                        ObjCVarvwDefaults.mInvoiceRightPosition = Convert.ToString(dr["InvoiceRightPosition"].ToString());
                        ObjCVarvwDefaults.mInvoiceRightSignature = Convert.ToString(dr["InvoiceRightSignature"].ToString());
                        ObjCVarvwDefaults.mSmallBusinessBelow = Convert.ToInt32(dr["SmallBusinessBelow"].ToString());
                        ObjCVarvwDefaults.mMediumBusinessBelow = Convert.ToInt32(dr["MediumBusinessBelow"].ToString());
                        ObjCVarvwDefaults.mIsTaxOnItems = Convert.ToBoolean(dr["IsTaxOnItems"].ToString());
                        ObjCVarvwDefaults.mIsDepartmentOption = Convert.ToBoolean(dr["IsDepartmentOption"].ToString());
                        ObjCVarvwDefaults.mIsCreditlimitexceptionperiod = Convert.ToBoolean(dr["IsCreditlimitexceptionperiod"].ToString());
                        ObjCVarvwDefaults.mIsRepeatMBL = Convert.ToBoolean(dr["IsRepeatMBL"].ToString());
                        ObjCVarvwDefaults.mIsRepeatChargeTypeName = Convert.ToBoolean(dr["IsRepeatChargeTypeName"].ToString());
                        ObjCVarvwDefaults.mIsAddChargeAuto = Convert.ToBoolean(dr["IsAddChargeAuto"].ToString());
                        ObjCVarvwDefaults.mEmail_Password = Convert.ToString(dr["Email_Password"].ToString());
                        ObjCVarvwDefaults.mEmail_DisplayName = Convert.ToString(dr["Email_DisplayName"].ToString());
                        ObjCVarvwDefaults.mEmail_Host = Convert.ToString(dr["Email_Host"].ToString());
                        ObjCVarvwDefaults.mEmail_Port = Convert.ToInt32(dr["Email_Port"].ToString());
                        ObjCVarvwDefaults.mEmail_IsSSL = Convert.ToBoolean(dr["Email_IsSSL"].ToString());
                        ObjCVarvwDefaults.mEmail_Header = Convert.ToString(dr["Email_Header"].ToString());
                        ObjCVarvwDefaults.mEmail_Footer = Convert.ToString(dr["Email_Footer"].ToString());
                        ObjCVarvwDefaults.mShowUserSalesmen = Convert.ToBoolean(dr["ShowUserSalesmen"].ToString());
                        ObjCVarvwDefaults.mNumberOfCustomerLimits = Convert.ToInt32(dr["NumberOfCustomerLimits"].ToString());
                        ObjCVarvwDefaults.mNumberOfActiveCustomersUsers = Convert.ToInt32(dr["NumberOfActiveCustomersUsers"].ToString());
                        ObjCVarvwDefaults.mDefaultTruckerID = Convert.ToInt32(dr["DefaultTruckerID"].ToString());
                        ObjCVarvwDefaults.mIsERP = Convert.ToBoolean(dr["IsERP"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwDefaults.Add(ObjCVarvwDefaults);
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
