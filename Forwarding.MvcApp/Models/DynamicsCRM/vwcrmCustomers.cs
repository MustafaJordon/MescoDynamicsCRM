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
    public class CPKvwcrmCustomers
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
    public partial class CVarvwcrmCustomers : CPKvwcrmCustomers
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mCode;
        internal String mName;
        internal String mLocalName;
        internal Int32 mSalesmanID;
        internal String mWebsite;
        internal String mEmail;
        internal Boolean mIsConsignee;
        internal Boolean mIsShipper;
        internal Boolean mIsInternalCustomer;
        internal Boolean mIsInactive;
        internal String mNotes;
        internal String mVATNumber;
        internal Int32 mPaymentTermID;
        internal Int32 mCurrencyID;
        internal Int32 mTaxeTypeID;
        internal Boolean mIsConsolidatedInvoice;
        internal String mBankName;
        internal String mBankAddress;
        internal String mSwift;
        internal String mBankAccountNumber;
        internal String mIBANNumber;
        internal Int32 mManagerRoleID;
        internal Int32 mAdministratorRoleID;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Int32 mLockingUserID;
        internal DateTime mTimeLocked;
        internal Boolean mIsDeleted;
        internal Int32 mAccountID;
        internal Int32 mSubAccountID;
        internal Int32 mCostCenterID;
        internal Int32 mSubAccountGroupID;
        internal String mAddress;
        internal String mPhonesAndFaxes;
        internal Int32 mSalesLeadID;
        internal Int32 mEmailOptionAging;
        internal Int32 mEmailOptionInvoicesReport;
        internal Int32 mEmailOptionPartnerStatement;
        internal String mBillingDetails;
        internal String mShippingDetails;
        internal Boolean mOriginalCMRbyPost;
        internal Int32 mCategoryID;
        internal Boolean mIsExternal;
        internal String mForeignExporterNo;
        internal Int32 mForeignExporterCountryID;
        internal String mcrmID;
        internal String mForwardingTableName;
        internal String mcrmTableName;
        #endregion

        #region "Methods"
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
        public Int32 SalesmanID
        {
            get { return mSalesmanID; }
            set { mSalesmanID = value; }
        }
        public String Website
        {
            get { return mWebsite; }
            set { mWebsite = value; }
        }
        public String Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }
        public Boolean IsConsignee
        {
            get { return mIsConsignee; }
            set { mIsConsignee = value; }
        }
        public Boolean IsShipper
        {
            get { return mIsShipper; }
            set { mIsShipper = value; }
        }
        public Boolean IsInternalCustomer
        {
            get { return mIsInternalCustomer; }
            set { mIsInternalCustomer = value; }
        }
        public Boolean IsInactive
        {
            get { return mIsInactive; }
            set { mIsInactive = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mNotes = value; }
        }
        public String VATNumber
        {
            get { return mVATNumber; }
            set { mVATNumber = value; }
        }
        public Int32 PaymentTermID
        {
            get { return mPaymentTermID; }
            set { mPaymentTermID = value; }
        }
        public Int32 CurrencyID
        {
            get { return mCurrencyID; }
            set { mCurrencyID = value; }
        }
        public Int32 TaxeTypeID
        {
            get { return mTaxeTypeID; }
            set { mTaxeTypeID = value; }
        }
        public Boolean IsConsolidatedInvoice
        {
            get { return mIsConsolidatedInvoice; }
            set { mIsConsolidatedInvoice = value; }
        }
        public String BankName
        {
            get { return mBankName; }
            set { mBankName = value; }
        }
        public String BankAddress
        {
            get { return mBankAddress; }
            set { mBankAddress = value; }
        }
        public String Swift
        {
            get { return mSwift; }
            set { mSwift = value; }
        }
        public String BankAccountNumber
        {
            get { return mBankAccountNumber; }
            set { mBankAccountNumber = value; }
        }
        public String IBANNumber
        {
            get { return mIBANNumber; }
            set { mIBANNumber = value; }
        }
        public Int32 ManagerRoleID
        {
            get { return mManagerRoleID; }
            set { mManagerRoleID = value; }
        }
        public Int32 AdministratorRoleID
        {
            get { return mAdministratorRoleID; }
            set { mAdministratorRoleID = value; }
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
        public Boolean IsDeleted
        {
            get { return mIsDeleted; }
            set { mIsDeleted = value; }
        }
        public Int32 AccountID
        {
            get { return mAccountID; }
            set { mAccountID = value; }
        }
        public Int32 SubAccountID
        {
            get { return mSubAccountID; }
            set { mSubAccountID = value; }
        }
        public Int32 CostCenterID
        {
            get { return mCostCenterID; }
            set { mCostCenterID = value; }
        }
        public Int32 SubAccountGroupID
        {
            get { return mSubAccountGroupID; }
            set { mSubAccountGroupID = value; }
        }
        public String Address
        {
            get { return mAddress; }
            set { mAddress = value; }
        }
        public String PhonesAndFaxes
        {
            get { return mPhonesAndFaxes; }
            set { mPhonesAndFaxes = value; }
        }
        public Int32 SalesLeadID
        {
            get { return mSalesLeadID; }
            set { mSalesLeadID = value; }
        }
        public Int32 EmailOptionAging
        {
            get { return mEmailOptionAging; }
            set { mEmailOptionAging = value; }
        }
        public Int32 EmailOptionInvoicesReport
        {
            get { return mEmailOptionInvoicesReport; }
            set { mEmailOptionInvoicesReport = value; }
        }
        public Int32 EmailOptionPartnerStatement
        {
            get { return mEmailOptionPartnerStatement; }
            set { mEmailOptionPartnerStatement = value; }
        }
        public String BillingDetails
        {
            get { return mBillingDetails; }
            set { mBillingDetails = value; }
        }
        public String ShippingDetails
        {
            get { return mShippingDetails; }
            set { mShippingDetails = value; }
        }
        public Boolean OriginalCMRbyPost
        {
            get { return mOriginalCMRbyPost; }
            set { mOriginalCMRbyPost = value; }
        }
        public Int32 CategoryID
        {
            get { return mCategoryID; }
            set { mCategoryID = value; }
        }
        public Boolean IsExternal
        {
            get { return mIsExternal; }
            set { mIsExternal = value; }
        }
        public String ForeignExporterNo
        {
            get { return mForeignExporterNo; }
            set { mForeignExporterNo = value; }
        }
        public Int32 ForeignExporterCountryID
        {
            get { return mForeignExporterCountryID; }
            set { mForeignExporterCountryID = value; }
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

    public partial class CvwcrmCustomers
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
        public List<CVarvwcrmCustomers> lstCVarvwcrmCustomers = new List<CVarvwcrmCustomers>();
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
            lstCVarvwcrmCustomers.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwcrmCustomers";
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
                        CVarvwcrmCustomers ObjCVarvwcrmCustomers = new CVarvwcrmCustomers();
                        ObjCVarvwcrmCustomers.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwcrmCustomers.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarvwcrmCustomers.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwcrmCustomers.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwcrmCustomers.mSalesmanID = Convert.ToInt32(dr["SalesmanID"].ToString());
                        ObjCVarvwcrmCustomers.mWebsite = Convert.ToString(dr["Website"].ToString());
                        ObjCVarvwcrmCustomers.mEmail = Convert.ToString(dr["Email"].ToString());
                        ObjCVarvwcrmCustomers.mIsConsignee = Convert.ToBoolean(dr["IsConsignee"].ToString());
                        ObjCVarvwcrmCustomers.mIsShipper = Convert.ToBoolean(dr["IsShipper"].ToString());
                        ObjCVarvwcrmCustomers.mIsInternalCustomer = Convert.ToBoolean(dr["IsInternalCustomer"].ToString());
                        ObjCVarvwcrmCustomers.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarvwcrmCustomers.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwcrmCustomers.mVATNumber = Convert.ToString(dr["VATNumber"].ToString());
                        ObjCVarvwcrmCustomers.mPaymentTermID = Convert.ToInt32(dr["PaymentTermID"].ToString());
                        ObjCVarvwcrmCustomers.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwcrmCustomers.mTaxeTypeID = Convert.ToInt32(dr["TaxeTypeID"].ToString());
                        ObjCVarvwcrmCustomers.mIsConsolidatedInvoice = Convert.ToBoolean(dr["IsConsolidatedInvoice"].ToString());
                        ObjCVarvwcrmCustomers.mBankName = Convert.ToString(dr["BankName"].ToString());
                        ObjCVarvwcrmCustomers.mBankAddress = Convert.ToString(dr["BankAddress"].ToString());
                        ObjCVarvwcrmCustomers.mSwift = Convert.ToString(dr["Swift"].ToString());
                        ObjCVarvwcrmCustomers.mBankAccountNumber = Convert.ToString(dr["BankAccountNumber"].ToString());
                        ObjCVarvwcrmCustomers.mIBANNumber = Convert.ToString(dr["IBANNumber"].ToString());
                        ObjCVarvwcrmCustomers.mManagerRoleID = Convert.ToInt32(dr["ManagerRoleID"].ToString());
                        ObjCVarvwcrmCustomers.mAdministratorRoleID = Convert.ToInt32(dr["AdministratorRoleID"].ToString());
                        ObjCVarvwcrmCustomers.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwcrmCustomers.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwcrmCustomers.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwcrmCustomers.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwcrmCustomers.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarvwcrmCustomers.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarvwcrmCustomers.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarvwcrmCustomers.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarvwcrmCustomers.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarvwcrmCustomers.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarvwcrmCustomers.mSubAccountGroupID = Convert.ToInt32(dr["SubAccountGroupID"].ToString());
                        ObjCVarvwcrmCustomers.mAddress = Convert.ToString(dr["Address"].ToString());
                        ObjCVarvwcrmCustomers.mPhonesAndFaxes = Convert.ToString(dr["PhonesAndFaxes"].ToString());
                        ObjCVarvwcrmCustomers.mSalesLeadID = Convert.ToInt32(dr["SalesLeadID"].ToString());
                        ObjCVarvwcrmCustomers.mEmailOptionAging = Convert.ToInt32(dr["EmailOptionAging"].ToString());
                        ObjCVarvwcrmCustomers.mEmailOptionInvoicesReport = Convert.ToInt32(dr["EmailOptionInvoicesReport"].ToString());
                        ObjCVarvwcrmCustomers.mEmailOptionPartnerStatement = Convert.ToInt32(dr["EmailOptionPartnerStatement"].ToString());
                        ObjCVarvwcrmCustomers.mBillingDetails = Convert.ToString(dr["BillingDetails"].ToString());
                        ObjCVarvwcrmCustomers.mShippingDetails = Convert.ToString(dr["ShippingDetails"].ToString());
                        ObjCVarvwcrmCustomers.mOriginalCMRbyPost = Convert.ToBoolean(dr["OriginalCMRbyPost"].ToString());
                        ObjCVarvwcrmCustomers.mCategoryID = Convert.ToInt32(dr["CategoryID"].ToString());
                        ObjCVarvwcrmCustomers.mIsExternal = Convert.ToBoolean(dr["IsExternal"].ToString());
                        ObjCVarvwcrmCustomers.mForeignExporterNo = Convert.ToString(dr["ForeignExporterNo"].ToString());
                        ObjCVarvwcrmCustomers.mForeignExporterCountryID = Convert.ToInt32(dr["ForeignExporterCountryID"].ToString());
                        ObjCVarvwcrmCustomers.mcrmID = Convert.ToString(dr["crmID"].ToString());
                        ObjCVarvwcrmCustomers.mForwardingTableName = Convert.ToString(dr["ForwardingTableName"].ToString());
                        ObjCVarvwcrmCustomers.mcrmTableName = Convert.ToString(dr["crmTableName"].ToString());
                        lstCVarvwcrmCustomers.Add(ObjCVarvwcrmCustomers);
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
            lstCVarvwcrmCustomers.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwcrmCustomers";
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
                        CVarvwcrmCustomers ObjCVarvwcrmCustomers = new CVarvwcrmCustomers();
                        ObjCVarvwcrmCustomers.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwcrmCustomers.mCode = Convert.ToInt32(dr["Code"].ToString());
                        ObjCVarvwcrmCustomers.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwcrmCustomers.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwcrmCustomers.mSalesmanID = Convert.ToInt32(dr["SalesmanID"].ToString());
                        ObjCVarvwcrmCustomers.mWebsite = Convert.ToString(dr["Website"].ToString());
                        ObjCVarvwcrmCustomers.mEmail = Convert.ToString(dr["Email"].ToString());
                        ObjCVarvwcrmCustomers.mIsConsignee = Convert.ToBoolean(dr["IsConsignee"].ToString());
                        ObjCVarvwcrmCustomers.mIsShipper = Convert.ToBoolean(dr["IsShipper"].ToString());
                        ObjCVarvwcrmCustomers.mIsInternalCustomer = Convert.ToBoolean(dr["IsInternalCustomer"].ToString());
                        ObjCVarvwcrmCustomers.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
                        ObjCVarvwcrmCustomers.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwcrmCustomers.mVATNumber = Convert.ToString(dr["VATNumber"].ToString());
                        ObjCVarvwcrmCustomers.mPaymentTermID = Convert.ToInt32(dr["PaymentTermID"].ToString());
                        ObjCVarvwcrmCustomers.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwcrmCustomers.mTaxeTypeID = Convert.ToInt32(dr["TaxeTypeID"].ToString());
                        ObjCVarvwcrmCustomers.mIsConsolidatedInvoice = Convert.ToBoolean(dr["IsConsolidatedInvoice"].ToString());
                        ObjCVarvwcrmCustomers.mBankName = Convert.ToString(dr["BankName"].ToString());
                        ObjCVarvwcrmCustomers.mBankAddress = Convert.ToString(dr["BankAddress"].ToString());
                        ObjCVarvwcrmCustomers.mSwift = Convert.ToString(dr["Swift"].ToString());
                        ObjCVarvwcrmCustomers.mBankAccountNumber = Convert.ToString(dr["BankAccountNumber"].ToString());
                        ObjCVarvwcrmCustomers.mIBANNumber = Convert.ToString(dr["IBANNumber"].ToString());
                        ObjCVarvwcrmCustomers.mManagerRoleID = Convert.ToInt32(dr["ManagerRoleID"].ToString());
                        ObjCVarvwcrmCustomers.mAdministratorRoleID = Convert.ToInt32(dr["AdministratorRoleID"].ToString());
                        ObjCVarvwcrmCustomers.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwcrmCustomers.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwcrmCustomers.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwcrmCustomers.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwcrmCustomers.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
                        ObjCVarvwcrmCustomers.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
                        ObjCVarvwcrmCustomers.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarvwcrmCustomers.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarvwcrmCustomers.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarvwcrmCustomers.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarvwcrmCustomers.mSubAccountGroupID = Convert.ToInt32(dr["SubAccountGroupID"].ToString());
                        ObjCVarvwcrmCustomers.mAddress = Convert.ToString(dr["Address"].ToString());
                        ObjCVarvwcrmCustomers.mPhonesAndFaxes = Convert.ToString(dr["PhonesAndFaxes"].ToString());
                        ObjCVarvwcrmCustomers.mSalesLeadID = Convert.ToInt32(dr["SalesLeadID"].ToString());
                        ObjCVarvwcrmCustomers.mEmailOptionAging = Convert.ToInt32(dr["EmailOptionAging"].ToString());
                        ObjCVarvwcrmCustomers.mEmailOptionInvoicesReport = Convert.ToInt32(dr["EmailOptionInvoicesReport"].ToString());
                        ObjCVarvwcrmCustomers.mEmailOptionPartnerStatement = Convert.ToInt32(dr["EmailOptionPartnerStatement"].ToString());
                        ObjCVarvwcrmCustomers.mBillingDetails = Convert.ToString(dr["BillingDetails"].ToString());
                        ObjCVarvwcrmCustomers.mShippingDetails = Convert.ToString(dr["ShippingDetails"].ToString());
                        ObjCVarvwcrmCustomers.mOriginalCMRbyPost = Convert.ToBoolean(dr["OriginalCMRbyPost"].ToString());
                        ObjCVarvwcrmCustomers.mCategoryID = Convert.ToInt32(dr["CategoryID"].ToString());
                        ObjCVarvwcrmCustomers.mIsExternal = Convert.ToBoolean(dr["IsExternal"].ToString());
                        ObjCVarvwcrmCustomers.mForeignExporterNo = Convert.ToString(dr["ForeignExporterNo"].ToString());
                        ObjCVarvwcrmCustomers.mForeignExporterCountryID = Convert.ToInt32(dr["ForeignExporterCountryID"].ToString());
                        ObjCVarvwcrmCustomers.mcrmID = Convert.ToString(dr["crmID"].ToString());
                        ObjCVarvwcrmCustomers.mForwardingTableName = Convert.ToString(dr["ForwardingTableName"].ToString());
                        ObjCVarvwcrmCustomers.mcrmTableName = Convert.ToString(dr["crmTableName"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwcrmCustomers.Add(ObjCVarvwcrmCustomers);
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
