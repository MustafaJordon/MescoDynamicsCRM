﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.MasterData.Partners.Generated
{
	[Serializable]
	public class CPKvwCustomers
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
	public partial class CVarvwCustomers : CPKvwCustomers
	{
		#region "variables"
		internal Boolean mIsChanges = false;
		internal Int32 mCode;
		internal String mName;
		internal String mLocalName;
		internal String mWebsite;
		internal String mEmail;
		internal Boolean mIsConsignee;
		internal Boolean mIsShipper;
		internal Boolean mIsinternalCustomer;
		internal Boolean mIsInactive;
		internal Boolean mIsDeleted;
		internal String mNotes;
		internal String mVATNumber;
		internal Int32 mPaymentTermID;
		internal Int32 mCurrencyID;
		internal Int32 mTaxeTypeID;
		internal Boolean mIsConsolidatedInvoice;
		internal String mBankName;
		internal Int32 mSalesmanID;
		internal String mBankAddress;
		internal String mSwift;
		internal String mBankAccountNumber;
		internal String mIBANNumber;
		internal DateTime mTimeLocked;
		internal Int32 mManagerRoleID;
		internal Int32 mAdministratorRoleID;
		internal String mSalesmanName;
		internal Int32 mLockingUserID;
		internal String mPriceListName;
		internal Int32 mPriceListID;
		internal String mPaymentTermCode;
		internal String mCurrencyCode;
		internal String mTaxeTypeCode;
		internal Int32 mAccountID;
		internal String mAccountName;
		internal Int32 mSubAccountID;
		internal String mSubAccountName;
		internal Int32 mCostCenterID;
		internal String mCostCenterName;
		internal Int32 mSubAccountGroupID;
		internal String mAddress;
		internal String mPhonesAndFaxes;
		internal Int32 mOperationCount;
		internal Int32 mCreatorUserID;
		internal Int32 mSalesLeadID;
		internal Int32 mModificatorUserID;
		internal String mSites;
		internal Int32 mCustomerUserID;
		internal String mCustomerUserUserName;
		internal String mCustomerUserName;
		internal Boolean mIsInActiveUser;
		internal String mBillingDetails;
		internal String mShippingDetails;
		internal Boolean mOriginalCMRbyPost;
		internal Int32 mCategoryID;
		internal Boolean mIsExternal;
		internal String mForeignExporterNo;
		internal Int32 mForeignExporterCountryID;
		internal String mForeignExporterCountryName;
		internal String mForeignExporterCountryCode;
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
		public Boolean IsinternalCustomer
		{
			get { return mIsinternalCustomer; }
			set { mIsinternalCustomer = value; }
		}
		public Boolean IsInactive
		{
			get { return mIsInactive; }
			set { mIsInactive = value; }
		}
		public Boolean IsDeleted
		{
			get { return mIsDeleted; }
			set { mIsDeleted = value; }
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
		public Int32 SalesmanID
		{
			get { return mSalesmanID; }
			set { mSalesmanID = value; }
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
		public DateTime TimeLocked
		{
			get { return mTimeLocked; }
			set { mTimeLocked = value; }
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
		public String SalesmanName
		{
			get { return mSalesmanName; }
			set { mSalesmanName = value; }
		}
		public Int32 LockingUserID
		{
			get { return mLockingUserID; }
			set { mLockingUserID = value; }
		}
		public String PriceListName
		{
			get { return mPriceListName; }
			set { mPriceListName = value; }
		}
		public Int32 PriceListID
		{
			get { return mPriceListID; }
			set { mPriceListID = value; }
		}
		public String PaymentTermCode
		{
			get { return mPaymentTermCode; }
			set { mPaymentTermCode = value; }
		}
		public String CurrencyCode
		{
			get { return mCurrencyCode; }
			set { mCurrencyCode = value; }
		}
		public String TaxeTypeCode
		{
			get { return mTaxeTypeCode; }
			set { mTaxeTypeCode = value; }
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
		public Int32 OperationCount
		{
			get { return mOperationCount; }
			set { mOperationCount = value; }
		}
		public Int32 CreatorUserID
		{
			get { return mCreatorUserID; }
			set { mCreatorUserID = value; }
		}
		public Int32 SalesLeadID
		{
			get { return mSalesLeadID; }
			set { mSalesLeadID = value; }
		}
		public Int32 ModificatorUserID
		{
			get { return mModificatorUserID; }
			set { mModificatorUserID = value; }
		}
		public String Sites
		{
			get { return mSites; }
			set { mSites = value; }
		}
		public Int32 CustomerUserID
		{
			get { return mCustomerUserID; }
			set { mCustomerUserID = value; }
		}
		public String CustomerUserUserName
		{
			get { return mCustomerUserUserName; }
			set { mCustomerUserUserName = value; }
		}
		public String CustomerUserName
		{
			get { return mCustomerUserName; }
			set { mCustomerUserName = value; }
		}
		public Boolean IsInActiveUser
		{
			get { return mIsInActiveUser; }
			set { mIsInActiveUser = value; }
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
		public String ForeignExporterCountryName
		{
			get { return mForeignExporterCountryName; }
			set { mForeignExporterCountryName = value; }
		}
		public String ForeignExporterCountryCode
		{
			get { return mForeignExporterCountryCode; }
			set { mForeignExporterCountryCode = value; }
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

	public partial class CvwCustomers
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
		public List<CVarvwCustomers> lstCVarvwCustomers= new List<CVarvwCustomers>();
		#endregion

		#region "Select Methods"
		public Exception GetList(string WhereClause)
		{
			return DataFill(WhereClause,true);
		}
		public Exception GetListPaging(Int32 PageSize, Int32 PageNumber, string WhereClause, string OrderBy, out Int32 TotalRecords)
		{
			return DataFill(PageSize, PageNumber, WhereClause, OrderBy, out TotalRecords);
		}
		private Exception DataFill(string Param , Boolean IsList)
		{
			Exception Exp = null;
			SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
			SqlCommand Com;
			SqlDataReader dr;
			lstCVarvwCustomers.Clear();

			try
			{
				Con.Open();
				tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
				Com = new SqlCommand();
				Com.CommandType = CommandType.StoredProcedure;
				if (IsList == true)
				{
					Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
					Com.CommandText = "[dbo].GetListvwCustomers";
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
						CVarvwCustomers ObjCVarvwCustomers = new CVarvwCustomers();
						ObjCVarvwCustomers.ID = Convert.ToInt32(dr["ID"].ToString());
						ObjCVarvwCustomers.mCode = Convert.ToInt32(dr["Code"].ToString());
						ObjCVarvwCustomers.mName = Convert.ToString(dr["Name"].ToString());
						ObjCVarvwCustomers.mLocalName = Convert.ToString(dr["LocalName"].ToString());
						ObjCVarvwCustomers.mWebsite = Convert.ToString(dr["Website"].ToString());
						ObjCVarvwCustomers.mEmail = Convert.ToString(dr["Email"].ToString());
						ObjCVarvwCustomers.mIsConsignee = Convert.ToBoolean(dr["IsConsignee"].ToString());
						ObjCVarvwCustomers.mIsShipper = Convert.ToBoolean(dr["IsShipper"].ToString());
						ObjCVarvwCustomers.mIsinternalCustomer = Convert.ToBoolean(dr["IsinternalCustomer"].ToString());
						ObjCVarvwCustomers.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
						ObjCVarvwCustomers.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
						ObjCVarvwCustomers.mNotes = Convert.ToString(dr["Notes"].ToString());
						ObjCVarvwCustomers.mVATNumber = Convert.ToString(dr["VATNumber"].ToString());
						ObjCVarvwCustomers.mPaymentTermID = Convert.ToInt32(dr["PaymentTermID"].ToString());
						ObjCVarvwCustomers.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
						ObjCVarvwCustomers.mTaxeTypeID = Convert.ToInt32(dr["TaxeTypeID"].ToString());
						ObjCVarvwCustomers.mIsConsolidatedInvoice = Convert.ToBoolean(dr["IsConsolidatedInvoice"].ToString());
						ObjCVarvwCustomers.mBankName = Convert.ToString(dr["BankName"].ToString());
						ObjCVarvwCustomers.mSalesmanID = Convert.ToInt32(dr["SalesmanID"].ToString());
						ObjCVarvwCustomers.mBankAddress = Convert.ToString(dr["BankAddress"].ToString());
						ObjCVarvwCustomers.mSwift = Convert.ToString(dr["Swift"].ToString());
						ObjCVarvwCustomers.mBankAccountNumber = Convert.ToString(dr["BankAccountNumber"].ToString());
						ObjCVarvwCustomers.mIBANNumber = Convert.ToString(dr["IBANNumber"].ToString());
						ObjCVarvwCustomers.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
						ObjCVarvwCustomers.mManagerRoleID = Convert.ToInt32(dr["ManagerRoleID"].ToString());
						ObjCVarvwCustomers.mAdministratorRoleID = Convert.ToInt32(dr["AdministratorRoleID"].ToString());
						ObjCVarvwCustomers.mSalesmanName = Convert.ToString(dr["SalesmanName"].ToString());
						ObjCVarvwCustomers.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
						ObjCVarvwCustomers.mPriceListName = Convert.ToString(dr["PriceListName"].ToString());
						ObjCVarvwCustomers.mPriceListID = Convert.ToInt32(dr["PriceListID"].ToString());
						ObjCVarvwCustomers.mPaymentTermCode = Convert.ToString(dr["PaymentTermCode"].ToString());
						ObjCVarvwCustomers.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
						ObjCVarvwCustomers.mTaxeTypeCode = Convert.ToString(dr["TaxeTypeCode"].ToString());
						ObjCVarvwCustomers.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
						ObjCVarvwCustomers.mAccountName = Convert.ToString(dr["AccountName"].ToString());
						ObjCVarvwCustomers.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
						ObjCVarvwCustomers.mSubAccountName = Convert.ToString(dr["SubAccountName"].ToString());
						ObjCVarvwCustomers.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
						ObjCVarvwCustomers.mCostCenterName = Convert.ToString(dr["CostCenterName"].ToString());
						ObjCVarvwCustomers.mSubAccountGroupID = Convert.ToInt32(dr["SubAccountGroupID"].ToString());
						ObjCVarvwCustomers.mAddress = Convert.ToString(dr["Address"].ToString());
						ObjCVarvwCustomers.mPhonesAndFaxes = Convert.ToString(dr["PhonesAndFaxes"].ToString());
						ObjCVarvwCustomers.mOperationCount = Convert.ToInt32(dr["OperationCount"].ToString());
						ObjCVarvwCustomers.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
						ObjCVarvwCustomers.mSalesLeadID = Convert.ToInt32(dr["SalesLeadID"].ToString());
						ObjCVarvwCustomers.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
						ObjCVarvwCustomers.mSites = Convert.ToString(dr["Sites"].ToString());
						ObjCVarvwCustomers.mCustomerUserID = Convert.ToInt32(dr["CustomerUserID"].ToString());
						ObjCVarvwCustomers.mCustomerUserUserName = Convert.ToString(dr["CustomerUserUserName"].ToString());
						ObjCVarvwCustomers.mCustomerUserName = Convert.ToString(dr["CustomerUserName"].ToString());
						ObjCVarvwCustomers.mIsInActiveUser = Convert.ToBoolean(dr["IsInActiveUser"].ToString());
						ObjCVarvwCustomers.mBillingDetails = Convert.ToString(dr["BillingDetails"].ToString());
						ObjCVarvwCustomers.mShippingDetails = Convert.ToString(dr["ShippingDetails"].ToString());
						ObjCVarvwCustomers.mOriginalCMRbyPost = Convert.ToBoolean(dr["OriginalCMRbyPost"].ToString());
						ObjCVarvwCustomers.mCategoryID = Convert.ToInt32(dr["CategoryID"].ToString());
						ObjCVarvwCustomers.mIsExternal = Convert.ToBoolean(dr["IsExternal"].ToString());
						ObjCVarvwCustomers.mForeignExporterNo = Convert.ToString(dr["ForeignExporterNo"].ToString());
						ObjCVarvwCustomers.mForeignExporterCountryID = Convert.ToInt32(dr["ForeignExporterCountryID"].ToString());
						ObjCVarvwCustomers.mForeignExporterCountryName = Convert.ToString(dr["ForeignExporterCountryName"].ToString());
						ObjCVarvwCustomers.mForeignExporterCountryCode = Convert.ToString(dr["ForeignExporterCountryCode"].ToString());
						lstCVarvwCustomers.Add(ObjCVarvwCustomers);
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
			catch ( Exception ex)
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
			lstCVarvwCustomers.Clear();

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
				Com.CommandText = "[dbo].GetListPagingvwCustomers";
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
						CVarvwCustomers ObjCVarvwCustomers = new CVarvwCustomers();
						ObjCVarvwCustomers.ID = Convert.ToInt32(dr["ID"].ToString());
						ObjCVarvwCustomers.mCode = Convert.ToInt32(dr["Code"].ToString());
						ObjCVarvwCustomers.mName = Convert.ToString(dr["Name"].ToString());
						ObjCVarvwCustomers.mLocalName = Convert.ToString(dr["LocalName"].ToString());
						ObjCVarvwCustomers.mWebsite = Convert.ToString(dr["Website"].ToString());
						ObjCVarvwCustomers.mEmail = Convert.ToString(dr["Email"].ToString());
						ObjCVarvwCustomers.mIsConsignee = Convert.ToBoolean(dr["IsConsignee"].ToString());
						ObjCVarvwCustomers.mIsShipper = Convert.ToBoolean(dr["IsShipper"].ToString());
						ObjCVarvwCustomers.mIsinternalCustomer = Convert.ToBoolean(dr["IsinternalCustomer"].ToString());
						ObjCVarvwCustomers.mIsInactive = Convert.ToBoolean(dr["IsInactive"].ToString());
						ObjCVarvwCustomers.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
						ObjCVarvwCustomers.mNotes = Convert.ToString(dr["Notes"].ToString());
						ObjCVarvwCustomers.mVATNumber = Convert.ToString(dr["VATNumber"].ToString());
						ObjCVarvwCustomers.mPaymentTermID = Convert.ToInt32(dr["PaymentTermID"].ToString());
						ObjCVarvwCustomers.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
						ObjCVarvwCustomers.mTaxeTypeID = Convert.ToInt32(dr["TaxeTypeID"].ToString());
						ObjCVarvwCustomers.mIsConsolidatedInvoice = Convert.ToBoolean(dr["IsConsolidatedInvoice"].ToString());
						ObjCVarvwCustomers.mBankName = Convert.ToString(dr["BankName"].ToString());
						ObjCVarvwCustomers.mSalesmanID = Convert.ToInt32(dr["SalesmanID"].ToString());
						ObjCVarvwCustomers.mBankAddress = Convert.ToString(dr["BankAddress"].ToString());
						ObjCVarvwCustomers.mSwift = Convert.ToString(dr["Swift"].ToString());
						ObjCVarvwCustomers.mBankAccountNumber = Convert.ToString(dr["BankAccountNumber"].ToString());
						ObjCVarvwCustomers.mIBANNumber = Convert.ToString(dr["IBANNumber"].ToString());
						ObjCVarvwCustomers.mTimeLocked = Convert.ToDateTime(dr["TimeLocked"].ToString());
						ObjCVarvwCustomers.mManagerRoleID = Convert.ToInt32(dr["ManagerRoleID"].ToString());
						ObjCVarvwCustomers.mAdministratorRoleID = Convert.ToInt32(dr["AdministratorRoleID"].ToString());
						ObjCVarvwCustomers.mSalesmanName = Convert.ToString(dr["SalesmanName"].ToString());
						ObjCVarvwCustomers.mLockingUserID = Convert.ToInt32(dr["LockingUserID"].ToString());
						ObjCVarvwCustomers.mPriceListName = Convert.ToString(dr["PriceListName"].ToString());
						ObjCVarvwCustomers.mPriceListID = Convert.ToInt32(dr["PriceListID"].ToString());
						ObjCVarvwCustomers.mPaymentTermCode = Convert.ToString(dr["PaymentTermCode"].ToString());
						ObjCVarvwCustomers.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
						ObjCVarvwCustomers.mTaxeTypeCode = Convert.ToString(dr["TaxeTypeCode"].ToString());
						ObjCVarvwCustomers.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
						ObjCVarvwCustomers.mAccountName = Convert.ToString(dr["AccountName"].ToString());
						ObjCVarvwCustomers.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
						ObjCVarvwCustomers.mSubAccountName = Convert.ToString(dr["SubAccountName"].ToString());
						ObjCVarvwCustomers.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
						ObjCVarvwCustomers.mCostCenterName = Convert.ToString(dr["CostCenterName"].ToString());
						ObjCVarvwCustomers.mSubAccountGroupID = Convert.ToInt32(dr["SubAccountGroupID"].ToString());
						ObjCVarvwCustomers.mAddress = Convert.ToString(dr["Address"].ToString());
						ObjCVarvwCustomers.mPhonesAndFaxes = Convert.ToString(dr["PhonesAndFaxes"].ToString());
						ObjCVarvwCustomers.mOperationCount = Convert.ToInt32(dr["OperationCount"].ToString());
						ObjCVarvwCustomers.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
						ObjCVarvwCustomers.mSalesLeadID = Convert.ToInt32(dr["SalesLeadID"].ToString());
						ObjCVarvwCustomers.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
						ObjCVarvwCustomers.mSites = Convert.ToString(dr["Sites"].ToString());
						ObjCVarvwCustomers.mCustomerUserID = Convert.ToInt32(dr["CustomerUserID"].ToString());
						ObjCVarvwCustomers.mCustomerUserUserName = Convert.ToString(dr["CustomerUserUserName"].ToString());
						ObjCVarvwCustomers.mCustomerUserName = Convert.ToString(dr["CustomerUserName"].ToString());
						ObjCVarvwCustomers.mIsInActiveUser = Convert.ToBoolean(dr["IsInActiveUser"].ToString());
						ObjCVarvwCustomers.mBillingDetails = Convert.ToString(dr["BillingDetails"].ToString());
						ObjCVarvwCustomers.mShippingDetails = Convert.ToString(dr["ShippingDetails"].ToString());
						ObjCVarvwCustomers.mOriginalCMRbyPost = Convert.ToBoolean(dr["OriginalCMRbyPost"].ToString());
						ObjCVarvwCustomers.mCategoryID = Convert.ToInt32(dr["CategoryID"].ToString());
						ObjCVarvwCustomers.mIsExternal = Convert.ToBoolean(dr["IsExternal"].ToString());
						ObjCVarvwCustomers.mForeignExporterNo = Convert.ToString(dr["ForeignExporterNo"].ToString());
						ObjCVarvwCustomers.mForeignExporterCountryID = Convert.ToInt32(dr["ForeignExporterCountryID"].ToString());
						ObjCVarvwCustomers.mForeignExporterCountryName = Convert.ToString(dr["ForeignExporterCountryName"].ToString());
						ObjCVarvwCustomers.mForeignExporterCountryCode = Convert.ToString(dr["ForeignExporterCountryCode"].ToString());
						TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
						lstCVarvwCustomers.Add(ObjCVarvwCustomers);
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
			catch ( Exception ex)
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
