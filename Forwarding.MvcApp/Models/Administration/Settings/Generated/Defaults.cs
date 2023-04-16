﻿using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.Administration.Settings.Generated
{
    [Serializable]
    public class CPKDefaults
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
    public partial class CVarDefaults : CPKDefaults
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mBranchID;
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
        internal Int32 mForeignCurrencyID;
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
        internal Int32 mCreatorUserID;
        internal String mUnEditableCompanyName;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Int32 mNumberOfAllowedSessions;
        internal DateTime mRenewalDate;
        internal Boolean mIsRenewalMessageSent;
        internal Int32 mDefaultCountryID;
        internal Int32 mDefaultPortID;
        internal Int32 mDefaultAgentID;
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
        internal String mEmail_Password;
        internal String mEmail_DisplayName;
        internal String mEmail_Host;
        internal Int32 mEmail_Port;
        internal Boolean mEmail_IsSSL;
        internal String mEmail_Header;
        internal String mEmail_Footer;
        internal Boolean mShowUserSalesmen;
        internal Int32 mNumberOfCustomerLimits;
        internal Int32 mDefaultTruckerID;
        internal Boolean mIsAddChargeAuto;
        internal Boolean mIsERP;
        #endregion

        #region "Methods"
        public Int32 BranchID
        {
            get { return mBranchID; }
            set { mIsChanges = true; mBranchID = value; }
        }
        public String CompanyName
        {
            get { return mCompanyName; }
            set { mIsChanges = true; mCompanyName = value; }
        }
        public String CompanyLocalName
        {
            get { return mCompanyLocalName; }
            set { mIsChanges = true; mCompanyLocalName = value; }
        }
        public String AddressLine1
        {
            get { return mAddressLine1; }
            set { mIsChanges = true; mAddressLine1 = value; }
        }
        public String AddressLine2
        {
            get { return mAddressLine2; }
            set { mIsChanges = true; mAddressLine2 = value; }
        }
        public String AddressLine3
        {
            get { return mAddressLine3; }
            set { mIsChanges = true; mAddressLine3 = value; }
        }
        public String AddressLine4
        {
            get { return mAddressLine4; }
            set { mIsChanges = true; mAddressLine4 = value; }
        }
        public String AddressLine5
        {
            get { return mAddressLine5; }
            set { mIsChanges = true; mAddressLine5 = value; }
        }
        public String Phones
        {
            get { return mPhones; }
            set { mIsChanges = true; mPhones = value; }
        }
        public String Faxes
        {
            get { return mFaxes; }
            set { mIsChanges = true; mFaxes = value; }
        }
        public String Email
        {
            get { return mEmail; }
            set { mIsChanges = true; mEmail = value; }
        }
        public String Website
        {
            get { return mWebsite; }
            set { mIsChanges = true; mWebsite = value; }
        }
        public Int32 CurrencyID
        {
            get { return mCurrencyID; }
            set { mIsChanges = true; mCurrencyID = value; }
        }
        public Int32 ForeignCurrencyID
        {
            get { return mForeignCurrencyID; }
            set { mIsChanges = true; mForeignCurrencyID = value; }
        }
        public String BankName
        {
            get { return mBankName; }
            set { mIsChanges = true; mBankName = value; }
        }
        public String AccountName
        {
            get { return mAccountName; }
            set { mIsChanges = true; mAccountName = value; }
        }
        public String BankAddress
        {
            get { return mBankAddress; }
            set { mIsChanges = true; mBankAddress = value; }
        }
        public String SwiftCode
        {
            get { return mSwiftCode; }
            set { mIsChanges = true; mSwiftCode = value; }
        }
        public String AccountNumber
        {
            get { return mAccountNumber; }
            set { mIsChanges = true; mAccountNumber = value; }
        }
        public String TaxNumber
        {
            get { return mTaxNumber; }
            set { mIsChanges = true; mTaxNumber = value; }
        }
        public Int32 ImportOceanDays
        {
            get { return mImportOceanDays; }
            set { mIsChanges = true; mImportOceanDays = value; }
        }
        public Int32 ImportAirDays
        {
            get { return mImportAirDays; }
            set { mIsChanges = true; mImportAirDays = value; }
        }
        public Int32 ImportInlandDays
        {
            get { return mImportInlandDays; }
            set { mIsChanges = true; mImportInlandDays = value; }
        }
        public Int32 ExportOceanDays
        {
            get { return mExportOceanDays; }
            set { mIsChanges = true; mExportOceanDays = value; }
        }
        public Int32 ExportAirDays
        {
            get { return mExportAirDays; }
            set { mIsChanges = true; mExportAirDays = value; }
        }
        public Int32 ExportInlandDays
        {
            get { return mExportInlandDays; }
            set { mIsChanges = true; mExportInlandDays = value; }
        }
        public Int32 DomesticOceanDays
        {
            get { return mDomesticOceanDays; }
            set { mIsChanges = true; mDomesticOceanDays = value; }
        }
        public Int32 DomesticAirDays
        {
            get { return mDomesticAirDays; }
            set { mIsChanges = true; mDomesticAirDays = value; }
        }
        public Int32 DomesticInlandDays
        {
            get { return mDomesticInlandDays; }
            set { mIsChanges = true; mDomesticInlandDays = value; }
        }
        public Int32 OperationSerialOption
        {
            get { return mOperationSerialOption; }
            set { mIsChanges = true; mOperationSerialOption = value; }
        }
        public Int32 InvoiceSerialOption
        {
            get { return mInvoiceSerialOption; }
            set { mIsChanges = true; mInvoiceSerialOption = value; }
        }
        public Int32 CreatorUserID
        {
            get { return mCreatorUserID; }
            set { mIsChanges = true; mCreatorUserID = value; }
        }
        public String UnEditableCompanyName
        {
            get { return mUnEditableCompanyName; }
            set { mIsChanges = true; mUnEditableCompanyName = value; }
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
        public Int32 NumberOfAllowedSessions
        {
            get { return mNumberOfAllowedSessions; }
            set { mIsChanges = true; mNumberOfAllowedSessions = value; }
        }
        public DateTime RenewalDate
        {
            get { return mRenewalDate; }
            set { mIsChanges = true; mRenewalDate = value; }
        }
        public Boolean IsRenewalMessageSent
        {
            get { return mIsRenewalMessageSent; }
            set { mIsChanges = true; mIsRenewalMessageSent = value; }
        }
        public Int32 DefaultCountryID
        {
            get { return mDefaultCountryID; }
            set { mIsChanges = true; mDefaultCountryID = value; }
        }
        public Int32 DefaultPortID
        {
            get { return mDefaultPortID; }
            set { mIsChanges = true; mDefaultPortID = value; }
        }
        public Int32 DefaultAgentID
        {
            get { return mDefaultAgentID; }
            set { mIsChanges = true; mDefaultAgentID = value; }
        }
        public Int32 LengthUnitID
        {
            get { return mLengthUnitID; }
            set { mIsChanges = true; mLengthUnitID = value; }
        }
        public Int32 WeightUnitID
        {
            get { return mWeightUnitID; }
            set { mIsChanges = true; mWeightUnitID = value; }
        }
        public Int32 VolumeUnitID
        {
            get { return mVolumeUnitID; }
            set { mIsChanges = true; mVolumeUnitID = value; }
        }
        public String VatIDNo
        {
            get { return mVatIDNo; }
            set { mIsChanges = true; mVatIDNo = value; }
        }
        public String CommericalRegNo
        {
            get { return mCommericalRegNo; }
            set { mIsChanges = true; mCommericalRegNo = value; }
        }
        public String SL_InvoicesComments
        {
            get { return mSL_InvoicesComments; }
            set { mIsChanges = true; mSL_InvoicesComments = value; }
        }
        public String PurchaseInvoicesComments
        {
            get { return mPurchaseInvoicesComments; }
            set { mIsChanges = true; mPurchaseInvoicesComments = value; }
        }
        public String InvoiceLeftPosition
        {
            get { return mInvoiceLeftPosition; }
            set { mIsChanges = true; mInvoiceLeftPosition = value; }
        }
        public String InvoiceLeftSignature
        {
            get { return mInvoiceLeftSignature; }
            set { mIsChanges = true; mInvoiceLeftSignature = value; }
        }
        public String InvoiceMiddlePosition
        {
            get { return mInvoiceMiddlePosition; }
            set { mIsChanges = true; mInvoiceMiddlePosition = value; }
        }
        public String InvoiceMiddleSignature
        {
            get { return mInvoiceMiddleSignature; }
            set { mIsChanges = true; mInvoiceMiddleSignature = value; }
        }
        public String InvoiceRightPosition
        {
            get { return mInvoiceRightPosition; }
            set { mIsChanges = true; mInvoiceRightPosition = value; }
        }
        public String InvoiceRightSignature
        {
            get { return mInvoiceRightSignature; }
            set { mIsChanges = true; mInvoiceRightSignature = value; }
        }
        public Int32 SmallBusinessBelow
        {
            get { return mSmallBusinessBelow; }
            set { mIsChanges = true; mSmallBusinessBelow = value; }
        }
        public Int32 MediumBusinessBelow
        {
            get { return mMediumBusinessBelow; }
            set { mIsChanges = true; mMediumBusinessBelow = value; }
        }
        public Boolean IsTaxOnItems
        {
            get { return mIsTaxOnItems; }
            set { mIsChanges = true; mIsTaxOnItems = value; }
        }
        public Boolean IsDepartmentOption
        {
            get { return mIsDepartmentOption; }
            set { mIsChanges = true; mIsDepartmentOption = value; }
        }
        public Boolean IsCreditlimitexceptionperiod
        {
            get { return mIsCreditlimitexceptionperiod; }
            set { mIsChanges = true; mIsCreditlimitexceptionperiod = value; }
        }
        public Boolean IsRepeatMBL
        {
            get { return mIsRepeatMBL; }
            set { mIsChanges = true; mIsRepeatMBL = value; }
        }
        public Boolean IsRepeatChargeTypeName
        {
            get { return mIsRepeatChargeTypeName; }
            set { mIsChanges = true; mIsRepeatChargeTypeName = value; }
        }
        public String Email_Password
        {
            get { return mEmail_Password; }
            set { mIsChanges = true; mEmail_Password = value; }
        }
        public String Email_DisplayName
        {
            get { return mEmail_DisplayName; }
            set { mIsChanges = true; mEmail_DisplayName = value; }
        }
        public String Email_Host
        {
            get { return mEmail_Host; }
            set { mIsChanges = true; mEmail_Host = value; }
        }
        public Int32 Email_Port
        {
            get { return mEmail_Port; }
            set { mIsChanges = true; mEmail_Port = value; }
        }
        public Boolean Email_IsSSL
        {
            get { return mEmail_IsSSL; }
            set { mIsChanges = true; mEmail_IsSSL = value; }
        }
        public String Email_Header
        {
            get { return mEmail_Header; }
            set { mIsChanges = true; mEmail_Header = value; }
        }
        public String Email_Footer
        {
            get { return mEmail_Footer; }
            set { mIsChanges = true; mEmail_Footer = value; }
        }
        public Boolean ShowUserSalesmen
        {
            get { return mShowUserSalesmen; }
            set { mIsChanges = true; mShowUserSalesmen = value; }
        }
        public Int32 NumberOfCustomerLimits
        {
            get { return mNumberOfCustomerLimits; }
            set { mIsChanges = true; mNumberOfCustomerLimits = value; }
        }
        public Int32 DefaultTruckerID
        {
            get { return mDefaultTruckerID; }
            set { mIsChanges = true; mDefaultTruckerID = value; }
        }
        public Boolean IsAddChargeAuto
        {
            get { return mIsAddChargeAuto; }
            set { mIsChanges = true; mIsAddChargeAuto = value; }
        }
        public Boolean IsERP
        {
            get { return mIsERP; }
            set { mIsChanges = true; mIsERP = value; }
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

    public partial class CDefaults
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
        public List<CVarDefaults> lstCVarDefaults = new List<CVarDefaults>();
        public List<CPKDefaults> lstDeletedCPKDefaults = new List<CPKDefaults>();
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
            lstCVarDefaults.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListDefaults";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemDefaults";
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
                        CVarDefaults ObjCVarDefaults = new CVarDefaults();
                        ObjCVarDefaults.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarDefaults.mBranchID = Convert.ToInt32(dr["BranchID"].ToString());
                        ObjCVarDefaults.mCompanyName = Convert.ToString(dr["CompanyName"].ToString());
                        ObjCVarDefaults.mCompanyLocalName = Convert.ToString(dr["CompanyLocalName"].ToString());
                        ObjCVarDefaults.mAddressLine1 = Convert.ToString(dr["AddressLine1"].ToString());
                        ObjCVarDefaults.mAddressLine2 = Convert.ToString(dr["AddressLine2"].ToString());
                        ObjCVarDefaults.mAddressLine3 = Convert.ToString(dr["AddressLine3"].ToString());
                        ObjCVarDefaults.mAddressLine4 = Convert.ToString(dr["AddressLine4"].ToString());
                        ObjCVarDefaults.mAddressLine5 = Convert.ToString(dr["AddressLine5"].ToString());
                        ObjCVarDefaults.mPhones = Convert.ToString(dr["Phones"].ToString());
                        ObjCVarDefaults.mFaxes = Convert.ToString(dr["Faxes"].ToString());
                        ObjCVarDefaults.mEmail = Convert.ToString(dr["Email"].ToString());
                        ObjCVarDefaults.mWebsite = Convert.ToString(dr["Website"].ToString());
                        ObjCVarDefaults.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarDefaults.mForeignCurrencyID = Convert.ToInt32(dr["ForeignCurrencyID"].ToString());
                        ObjCVarDefaults.mBankName = Convert.ToString(dr["BankName"].ToString());
                        ObjCVarDefaults.mAccountName = Convert.ToString(dr["AccountName"].ToString());
                        ObjCVarDefaults.mBankAddress = Convert.ToString(dr["BankAddress"].ToString());
                        ObjCVarDefaults.mSwiftCode = Convert.ToString(dr["SwiftCode"].ToString());
                        ObjCVarDefaults.mAccountNumber = Convert.ToString(dr["AccountNumber"].ToString());
                        ObjCVarDefaults.mTaxNumber = Convert.ToString(dr["TaxNumber"].ToString());
                        ObjCVarDefaults.mImportOceanDays = Convert.ToInt32(dr["ImportOceanDays"].ToString());
                        ObjCVarDefaults.mImportAirDays = Convert.ToInt32(dr["ImportAirDays"].ToString());
                        ObjCVarDefaults.mImportInlandDays = Convert.ToInt32(dr["ImportInlandDays"].ToString());
                        ObjCVarDefaults.mExportOceanDays = Convert.ToInt32(dr["ExportOceanDays"].ToString());
                        ObjCVarDefaults.mExportAirDays = Convert.ToInt32(dr["ExportAirDays"].ToString());
                        ObjCVarDefaults.mExportInlandDays = Convert.ToInt32(dr["ExportInlandDays"].ToString());
                        ObjCVarDefaults.mDomesticOceanDays = Convert.ToInt32(dr["DomesticOceanDays"].ToString());
                        ObjCVarDefaults.mDomesticAirDays = Convert.ToInt32(dr["DomesticAirDays"].ToString());
                        ObjCVarDefaults.mDomesticInlandDays = Convert.ToInt32(dr["DomesticInlandDays"].ToString());
                        ObjCVarDefaults.mOperationSerialOption = Convert.ToInt32(dr["OperationSerialOption"].ToString());
                        ObjCVarDefaults.mInvoiceSerialOption = Convert.ToInt32(dr["InvoiceSerialOption"].ToString());
                        ObjCVarDefaults.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarDefaults.mUnEditableCompanyName = Convert.ToString(dr["UnEditableCompanyName"].ToString());
                        ObjCVarDefaults.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarDefaults.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarDefaults.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarDefaults.mNumberOfAllowedSessions = Convert.ToInt32(dr["NumberOfAllowedSessions"].ToString());
                        ObjCVarDefaults.mRenewalDate = Convert.ToDateTime(dr["RenewalDate"].ToString());
                        ObjCVarDefaults.mIsRenewalMessageSent = Convert.ToBoolean(dr["IsRenewalMessageSent"].ToString());
                        ObjCVarDefaults.mDefaultCountryID = Convert.ToInt32(dr["DefaultCountryID"].ToString());
                        ObjCVarDefaults.mDefaultPortID = Convert.ToInt32(dr["DefaultPortID"].ToString());
                        ObjCVarDefaults.mDefaultAgentID = Convert.ToInt32(dr["DefaultAgentID"].ToString());
                        ObjCVarDefaults.mLengthUnitID = Convert.ToInt32(dr["LengthUnitID"].ToString());
                        ObjCVarDefaults.mWeightUnitID = Convert.ToInt32(dr["WeightUnitID"].ToString());
                        ObjCVarDefaults.mVolumeUnitID = Convert.ToInt32(dr["VolumeUnitID"].ToString());
                        ObjCVarDefaults.mVatIDNo = Convert.ToString(dr["VatIDNo"].ToString());
                        ObjCVarDefaults.mCommericalRegNo = Convert.ToString(dr["CommericalRegNo"].ToString());
                        ObjCVarDefaults.mSL_InvoicesComments = Convert.ToString(dr["SL_InvoicesComments"].ToString());
                        ObjCVarDefaults.mPurchaseInvoicesComments = Convert.ToString(dr["PurchaseInvoicesComments"].ToString());
                        ObjCVarDefaults.mInvoiceLeftPosition = Convert.ToString(dr["InvoiceLeftPosition"].ToString());
                        ObjCVarDefaults.mInvoiceLeftSignature = Convert.ToString(dr["InvoiceLeftSignature"].ToString());
                        ObjCVarDefaults.mInvoiceMiddlePosition = Convert.ToString(dr["InvoiceMiddlePosition"].ToString());
                        ObjCVarDefaults.mInvoiceMiddleSignature = Convert.ToString(dr["InvoiceMiddleSignature"].ToString());
                        ObjCVarDefaults.mInvoiceRightPosition = Convert.ToString(dr["InvoiceRightPosition"].ToString());
                        ObjCVarDefaults.mInvoiceRightSignature = Convert.ToString(dr["InvoiceRightSignature"].ToString());
                        ObjCVarDefaults.mSmallBusinessBelow = Convert.ToInt32(dr["SmallBusinessBelow"].ToString());
                        ObjCVarDefaults.mMediumBusinessBelow = Convert.ToInt32(dr["MediumBusinessBelow"].ToString());
                        ObjCVarDefaults.mIsTaxOnItems = Convert.ToBoolean(dr["IsTaxOnItems"].ToString());
                        ObjCVarDefaults.mIsDepartmentOption = Convert.ToBoolean(dr["IsDepartmentOption"].ToString());
                        ObjCVarDefaults.mIsCreditlimitexceptionperiod = Convert.ToBoolean(dr["IsCreditlimitexceptionperiod"].ToString());
                        ObjCVarDefaults.mIsRepeatMBL = Convert.ToBoolean(dr["IsRepeatMBL"].ToString());
                        ObjCVarDefaults.mIsRepeatChargeTypeName = Convert.ToBoolean(dr["IsRepeatChargeTypeName"].ToString());
                        ObjCVarDefaults.mEmail_Password = Convert.ToString(dr["Email_Password"].ToString());
                        ObjCVarDefaults.mEmail_DisplayName = Convert.ToString(dr["Email_DisplayName"].ToString());
                        ObjCVarDefaults.mEmail_Host = Convert.ToString(dr["Email_Host"].ToString());
                        ObjCVarDefaults.mEmail_Port = Convert.ToInt32(dr["Email_Port"].ToString());
                        ObjCVarDefaults.mEmail_IsSSL = Convert.ToBoolean(dr["Email_IsSSL"].ToString());
                        ObjCVarDefaults.mEmail_Header = Convert.ToString(dr["Email_Header"].ToString());
                        ObjCVarDefaults.mEmail_Footer = Convert.ToString(dr["Email_Footer"].ToString());
                        ObjCVarDefaults.mShowUserSalesmen = Convert.ToBoolean(dr["ShowUserSalesmen"].ToString());
                        ObjCVarDefaults.mNumberOfCustomerLimits = Convert.ToInt32(dr["NumberOfCustomerLimits"].ToString());
                        ObjCVarDefaults.mDefaultTruckerID = Convert.ToInt32(dr["DefaultTruckerID"].ToString());
                        ObjCVarDefaults.mIsAddChargeAuto = Convert.ToBoolean(dr["IsAddChargeAuto"].ToString());
                        ObjCVarDefaults.mIsERP = Convert.ToBoolean(dr["IsERP"].ToString());
                        lstCVarDefaults.Add(ObjCVarDefaults);
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
            lstCVarDefaults.Clear();

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
                Com.CommandText = "[dbo].GetListPagingDefaults";
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
                        CVarDefaults ObjCVarDefaults = new CVarDefaults();
                        ObjCVarDefaults.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarDefaults.mBranchID = Convert.ToInt32(dr["BranchID"].ToString());
                        ObjCVarDefaults.mCompanyName = Convert.ToString(dr["CompanyName"].ToString());
                        ObjCVarDefaults.mCompanyLocalName = Convert.ToString(dr["CompanyLocalName"].ToString());
                        ObjCVarDefaults.mAddressLine1 = Convert.ToString(dr["AddressLine1"].ToString());
                        ObjCVarDefaults.mAddressLine2 = Convert.ToString(dr["AddressLine2"].ToString());
                        ObjCVarDefaults.mAddressLine3 = Convert.ToString(dr["AddressLine3"].ToString());
                        ObjCVarDefaults.mAddressLine4 = Convert.ToString(dr["AddressLine4"].ToString());
                        ObjCVarDefaults.mAddressLine5 = Convert.ToString(dr["AddressLine5"].ToString());
                        ObjCVarDefaults.mPhones = Convert.ToString(dr["Phones"].ToString());
                        ObjCVarDefaults.mFaxes = Convert.ToString(dr["Faxes"].ToString());
                        ObjCVarDefaults.mEmail = Convert.ToString(dr["Email"].ToString());
                        ObjCVarDefaults.mWebsite = Convert.ToString(dr["Website"].ToString());
                        ObjCVarDefaults.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarDefaults.mForeignCurrencyID = Convert.ToInt32(dr["ForeignCurrencyID"].ToString());
                        ObjCVarDefaults.mBankName = Convert.ToString(dr["BankName"].ToString());
                        ObjCVarDefaults.mAccountName = Convert.ToString(dr["AccountName"].ToString());
                        ObjCVarDefaults.mBankAddress = Convert.ToString(dr["BankAddress"].ToString());
                        ObjCVarDefaults.mSwiftCode = Convert.ToString(dr["SwiftCode"].ToString());
                        ObjCVarDefaults.mAccountNumber = Convert.ToString(dr["AccountNumber"].ToString());
                        ObjCVarDefaults.mTaxNumber = Convert.ToString(dr["TaxNumber"].ToString());
                        ObjCVarDefaults.mImportOceanDays = Convert.ToInt32(dr["ImportOceanDays"].ToString());
                        ObjCVarDefaults.mImportAirDays = Convert.ToInt32(dr["ImportAirDays"].ToString());
                        ObjCVarDefaults.mImportInlandDays = Convert.ToInt32(dr["ImportInlandDays"].ToString());
                        ObjCVarDefaults.mExportOceanDays = Convert.ToInt32(dr["ExportOceanDays"].ToString());
                        ObjCVarDefaults.mExportAirDays = Convert.ToInt32(dr["ExportAirDays"].ToString());
                        ObjCVarDefaults.mExportInlandDays = Convert.ToInt32(dr["ExportInlandDays"].ToString());
                        ObjCVarDefaults.mDomesticOceanDays = Convert.ToInt32(dr["DomesticOceanDays"].ToString());
                        ObjCVarDefaults.mDomesticAirDays = Convert.ToInt32(dr["DomesticAirDays"].ToString());
                        ObjCVarDefaults.mDomesticInlandDays = Convert.ToInt32(dr["DomesticInlandDays"].ToString());
                        ObjCVarDefaults.mOperationSerialOption = Convert.ToInt32(dr["OperationSerialOption"].ToString());
                        ObjCVarDefaults.mInvoiceSerialOption = Convert.ToInt32(dr["InvoiceSerialOption"].ToString());
                        ObjCVarDefaults.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarDefaults.mUnEditableCompanyName = Convert.ToString(dr["UnEditableCompanyName"].ToString());
                        ObjCVarDefaults.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarDefaults.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarDefaults.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarDefaults.mNumberOfAllowedSessions = Convert.ToInt32(dr["NumberOfAllowedSessions"].ToString());
                        ObjCVarDefaults.mRenewalDate = Convert.ToDateTime(dr["RenewalDate"].ToString());
                        ObjCVarDefaults.mIsRenewalMessageSent = Convert.ToBoolean(dr["IsRenewalMessageSent"].ToString());
                        ObjCVarDefaults.mDefaultCountryID = Convert.ToInt32(dr["DefaultCountryID"].ToString());
                        ObjCVarDefaults.mDefaultPortID = Convert.ToInt32(dr["DefaultPortID"].ToString());
                        ObjCVarDefaults.mDefaultAgentID = Convert.ToInt32(dr["DefaultAgentID"].ToString());
                        ObjCVarDefaults.mLengthUnitID = Convert.ToInt32(dr["LengthUnitID"].ToString());
                        ObjCVarDefaults.mWeightUnitID = Convert.ToInt32(dr["WeightUnitID"].ToString());
                        ObjCVarDefaults.mVolumeUnitID = Convert.ToInt32(dr["VolumeUnitID"].ToString());
                        ObjCVarDefaults.mVatIDNo = Convert.ToString(dr["VatIDNo"].ToString());
                        ObjCVarDefaults.mCommericalRegNo = Convert.ToString(dr["CommericalRegNo"].ToString());
                        ObjCVarDefaults.mSL_InvoicesComments = Convert.ToString(dr["SL_InvoicesComments"].ToString());
                        ObjCVarDefaults.mPurchaseInvoicesComments = Convert.ToString(dr["PurchaseInvoicesComments"].ToString());
                        ObjCVarDefaults.mInvoiceLeftPosition = Convert.ToString(dr["InvoiceLeftPosition"].ToString());
                        ObjCVarDefaults.mInvoiceLeftSignature = Convert.ToString(dr["InvoiceLeftSignature"].ToString());
                        ObjCVarDefaults.mInvoiceMiddlePosition = Convert.ToString(dr["InvoiceMiddlePosition"].ToString());
                        ObjCVarDefaults.mInvoiceMiddleSignature = Convert.ToString(dr["InvoiceMiddleSignature"].ToString());
                        ObjCVarDefaults.mInvoiceRightPosition = Convert.ToString(dr["InvoiceRightPosition"].ToString());
                        ObjCVarDefaults.mInvoiceRightSignature = Convert.ToString(dr["InvoiceRightSignature"].ToString());
                        ObjCVarDefaults.mSmallBusinessBelow = Convert.ToInt32(dr["SmallBusinessBelow"].ToString());
                        ObjCVarDefaults.mMediumBusinessBelow = Convert.ToInt32(dr["MediumBusinessBelow"].ToString());
                        ObjCVarDefaults.mIsTaxOnItems = Convert.ToBoolean(dr["IsTaxOnItems"].ToString());
                        ObjCVarDefaults.mIsDepartmentOption = Convert.ToBoolean(dr["IsDepartmentOption"].ToString());
                        ObjCVarDefaults.mIsCreditlimitexceptionperiod = Convert.ToBoolean(dr["IsCreditlimitexceptionperiod"].ToString());
                        ObjCVarDefaults.mIsRepeatMBL = Convert.ToBoolean(dr["IsRepeatMBL"].ToString());
                        ObjCVarDefaults.mIsRepeatChargeTypeName = Convert.ToBoolean(dr["IsRepeatChargeTypeName"].ToString());
                        ObjCVarDefaults.mEmail_Password = Convert.ToString(dr["Email_Password"].ToString());
                        ObjCVarDefaults.mEmail_DisplayName = Convert.ToString(dr["Email_DisplayName"].ToString());
                        ObjCVarDefaults.mEmail_Host = Convert.ToString(dr["Email_Host"].ToString());
                        ObjCVarDefaults.mEmail_Port = Convert.ToInt32(dr["Email_Port"].ToString());
                        ObjCVarDefaults.mEmail_IsSSL = Convert.ToBoolean(dr["Email_IsSSL"].ToString());
                        ObjCVarDefaults.mEmail_Header = Convert.ToString(dr["Email_Header"].ToString());
                        ObjCVarDefaults.mEmail_Footer = Convert.ToString(dr["Email_Footer"].ToString());
                        ObjCVarDefaults.mShowUserSalesmen = Convert.ToBoolean(dr["ShowUserSalesmen"].ToString());
                        ObjCVarDefaults.mNumberOfCustomerLimits = Convert.ToInt32(dr["NumberOfCustomerLimits"].ToString());
                        ObjCVarDefaults.mDefaultTruckerID = Convert.ToInt32(dr["DefaultTruckerID"].ToString());
                        ObjCVarDefaults.mIsAddChargeAuto = Convert.ToBoolean(dr["IsAddChargeAuto"].ToString());
                        ObjCVarDefaults.mIsERP = Convert.ToBoolean(dr["IsERP"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarDefaults.Add(ObjCVarDefaults);
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
                    Com.CommandText = "[dbo].DeleteListDefaults";
                else
                    Com.CommandText = "[dbo].UpdateListDefaults";
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
        public Exception DeleteItem(List<CPKDefaults> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemDefaults";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                foreach (CPKDefaults ObjCPKDefaults in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt32(ObjCPKDefaults.ID);
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
        public Exception SaveMethod(List<CVarDefaults> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@BranchID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CompanyName", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@CompanyLocalName", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@AddressLine1", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@AddressLine2", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@AddressLine3", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@AddressLine4", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@AddressLine5", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Phones", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Faxes", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Website", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@CurrencyID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ForeignCurrencyID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@BankName", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@AccountName", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@BankAddress", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@SwiftCode", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@AccountNumber", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@TaxNumber", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@ImportOceanDays", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ImportAirDays", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ImportInlandDays", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ExportOceanDays", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ExportAirDays", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ExportInlandDays", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@DomesticOceanDays", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@DomesticAirDays", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@DomesticInlandDays", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@OperationSerialOption", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@InvoiceSerialOption", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@UnEditableCompanyName", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ModificatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ModificationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@NumberOfAllowedSessions", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@RenewalDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@IsRenewalMessageSent", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@DefaultCountryID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@DefaultPortID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@DefaultAgentID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@LengthUnitID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@WeightUnitID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@VolumeUnitID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@VatIDNo", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@CommericalRegNo", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@SL_InvoicesComments", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@PurchaseInvoicesComments", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@InvoiceLeftPosition", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@InvoiceLeftSignature", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@InvoiceMiddlePosition", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@InvoiceMiddleSignature", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@InvoiceRightPosition", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@InvoiceRightSignature", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@SmallBusinessBelow", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@MediumBusinessBelow", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@IsTaxOnItems", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@IsDepartmentOption", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@IsCreditlimitexceptionperiod", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@IsRepeatMBL", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@IsRepeatChargeTypeName", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@Email_Password", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Email_DisplayName", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Email_Host", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Email_Port", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@Email_IsSSL", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@Email_Header", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Email_Footer", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@ShowUserSalesmen", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@NumberOfCustomerLimits", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@DefaultTruckerID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@IsAddChargeAuto", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@IsERP", SqlDbType.Bit));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarDefaults ObjCVarDefaults in SaveList)
                {
                    if (ObjCVarDefaults.mIsChanges == true)
                    {
                        if (ObjCVarDefaults.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemDefaults";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarDefaults.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemDefaults";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarDefaults.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarDefaults.ID;
                        }
                        Com.Parameters["@BranchID"].Value = ObjCVarDefaults.BranchID;
                        Com.Parameters["@CompanyName"].Value = ObjCVarDefaults.CompanyName;
                        Com.Parameters["@CompanyLocalName"].Value = ObjCVarDefaults.CompanyLocalName;
                        Com.Parameters["@AddressLine1"].Value = ObjCVarDefaults.AddressLine1;
                        Com.Parameters["@AddressLine2"].Value = ObjCVarDefaults.AddressLine2;
                        Com.Parameters["@AddressLine3"].Value = ObjCVarDefaults.AddressLine3;
                        Com.Parameters["@AddressLine4"].Value = ObjCVarDefaults.AddressLine4;
                        Com.Parameters["@AddressLine5"].Value = ObjCVarDefaults.AddressLine5;
                        Com.Parameters["@Phones"].Value = ObjCVarDefaults.Phones;
                        Com.Parameters["@Faxes"].Value = ObjCVarDefaults.Faxes;
                        Com.Parameters["@Email"].Value = ObjCVarDefaults.Email;
                        Com.Parameters["@Website"].Value = ObjCVarDefaults.Website;
                        Com.Parameters["@CurrencyID"].Value = ObjCVarDefaults.CurrencyID;
                        Com.Parameters["@ForeignCurrencyID"].Value = ObjCVarDefaults.ForeignCurrencyID;
                        Com.Parameters["@BankName"].Value = ObjCVarDefaults.BankName;
                        Com.Parameters["@AccountName"].Value = ObjCVarDefaults.AccountName;
                        Com.Parameters["@BankAddress"].Value = ObjCVarDefaults.BankAddress;
                        Com.Parameters["@SwiftCode"].Value = ObjCVarDefaults.SwiftCode;
                        Com.Parameters["@AccountNumber"].Value = ObjCVarDefaults.AccountNumber;
                        Com.Parameters["@TaxNumber"].Value = ObjCVarDefaults.TaxNumber;
                        Com.Parameters["@ImportOceanDays"].Value = ObjCVarDefaults.ImportOceanDays;
                        Com.Parameters["@ImportAirDays"].Value = ObjCVarDefaults.ImportAirDays;
                        Com.Parameters["@ImportInlandDays"].Value = ObjCVarDefaults.ImportInlandDays;
                        Com.Parameters["@ExportOceanDays"].Value = ObjCVarDefaults.ExportOceanDays;
                        Com.Parameters["@ExportAirDays"].Value = ObjCVarDefaults.ExportAirDays;
                        Com.Parameters["@ExportInlandDays"].Value = ObjCVarDefaults.ExportInlandDays;
                        Com.Parameters["@DomesticOceanDays"].Value = ObjCVarDefaults.DomesticOceanDays;
                        Com.Parameters["@DomesticAirDays"].Value = ObjCVarDefaults.DomesticAirDays;
                        Com.Parameters["@DomesticInlandDays"].Value = ObjCVarDefaults.DomesticInlandDays;
                        Com.Parameters["@OperationSerialOption"].Value = ObjCVarDefaults.OperationSerialOption;
                        Com.Parameters["@InvoiceSerialOption"].Value = ObjCVarDefaults.InvoiceSerialOption;
                        Com.Parameters["@CreatorUserID"].Value = ObjCVarDefaults.CreatorUserID;
                        Com.Parameters["@UnEditableCompanyName"].Value = ObjCVarDefaults.UnEditableCompanyName;
                        Com.Parameters["@CreationDate"].Value = ObjCVarDefaults.CreationDate;
                        Com.Parameters["@ModificatorUserID"].Value = ObjCVarDefaults.ModificatorUserID;
                        Com.Parameters["@ModificationDate"].Value = ObjCVarDefaults.ModificationDate;
                        Com.Parameters["@NumberOfAllowedSessions"].Value = ObjCVarDefaults.NumberOfAllowedSessions;
                        Com.Parameters["@RenewalDate"].Value = ObjCVarDefaults.RenewalDate;
                        Com.Parameters["@IsRenewalMessageSent"].Value = ObjCVarDefaults.IsRenewalMessageSent;
                        Com.Parameters["@DefaultCountryID"].Value = ObjCVarDefaults.DefaultCountryID;
                        Com.Parameters["@DefaultPortID"].Value = ObjCVarDefaults.DefaultPortID;
                        Com.Parameters["@DefaultAgentID"].Value = ObjCVarDefaults.DefaultAgentID;
                        Com.Parameters["@LengthUnitID"].Value = ObjCVarDefaults.LengthUnitID;
                        Com.Parameters["@WeightUnitID"].Value = ObjCVarDefaults.WeightUnitID;
                        Com.Parameters["@VolumeUnitID"].Value = ObjCVarDefaults.VolumeUnitID;
                        Com.Parameters["@VatIDNo"].Value = ObjCVarDefaults.VatIDNo;
                        Com.Parameters["@CommericalRegNo"].Value = ObjCVarDefaults.CommericalRegNo;
                        Com.Parameters["@SL_InvoicesComments"].Value = ObjCVarDefaults.SL_InvoicesComments;
                        Com.Parameters["@PurchaseInvoicesComments"].Value = ObjCVarDefaults.PurchaseInvoicesComments;
                        Com.Parameters["@InvoiceLeftPosition"].Value = ObjCVarDefaults.InvoiceLeftPosition;
                        Com.Parameters["@InvoiceLeftSignature"].Value = ObjCVarDefaults.InvoiceLeftSignature;
                        Com.Parameters["@InvoiceMiddlePosition"].Value = ObjCVarDefaults.InvoiceMiddlePosition;
                        Com.Parameters["@InvoiceMiddleSignature"].Value = ObjCVarDefaults.InvoiceMiddleSignature;
                        Com.Parameters["@InvoiceRightPosition"].Value = ObjCVarDefaults.InvoiceRightPosition;
                        Com.Parameters["@InvoiceRightSignature"].Value = ObjCVarDefaults.InvoiceRightSignature;
                        Com.Parameters["@SmallBusinessBelow"].Value = ObjCVarDefaults.SmallBusinessBelow;
                        Com.Parameters["@MediumBusinessBelow"].Value = ObjCVarDefaults.MediumBusinessBelow;
                        Com.Parameters["@IsTaxOnItems"].Value = ObjCVarDefaults.IsTaxOnItems;
                        Com.Parameters["@IsDepartmentOption"].Value = ObjCVarDefaults.IsDepartmentOption;
                        Com.Parameters["@IsCreditlimitexceptionperiod"].Value = ObjCVarDefaults.IsCreditlimitexceptionperiod;
                        Com.Parameters["@IsRepeatMBL"].Value = ObjCVarDefaults.IsRepeatMBL;
                        Com.Parameters["@IsRepeatChargeTypeName"].Value = ObjCVarDefaults.IsRepeatChargeTypeName;
                        Com.Parameters["@Email_Password"].Value = ObjCVarDefaults.Email_Password;
                        Com.Parameters["@Email_DisplayName"].Value = ObjCVarDefaults.Email_DisplayName;
                        Com.Parameters["@Email_Host"].Value = ObjCVarDefaults.Email_Host;
                        Com.Parameters["@Email_Port"].Value = ObjCVarDefaults.Email_Port;
                        Com.Parameters["@Email_IsSSL"].Value = ObjCVarDefaults.Email_IsSSL;
                        Com.Parameters["@Email_Header"].Value = ObjCVarDefaults.Email_Header;
                        Com.Parameters["@Email_Footer"].Value = ObjCVarDefaults.Email_Footer;
                        Com.Parameters["@ShowUserSalesmen"].Value = ObjCVarDefaults.ShowUserSalesmen;
                        Com.Parameters["@NumberOfCustomerLimits"].Value = ObjCVarDefaults.NumberOfCustomerLimits;
                        Com.Parameters["@DefaultTruckerID"].Value = ObjCVarDefaults.DefaultTruckerID;
                        Com.Parameters["@IsAddChargeAuto"].Value = ObjCVarDefaults.IsAddChargeAuto;
                        Com.Parameters["@IsERP"].Value = ObjCVarDefaults.IsERP;
                        EndTrans(Com, Con);
                        if (ObjCVarDefaults.ID == 0)
                        {
                            ObjCVarDefaults.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarDefaults.mIsChanges = false;
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

