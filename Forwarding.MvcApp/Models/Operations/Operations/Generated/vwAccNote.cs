﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.Operations.Operations.Generated
{
    [Serializable]
    public partial class CVarvwAccNote
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mID;
        internal Int64 mCodeSerial;
        internal String mCode;
        internal Int32 mNoteType;
        internal String mNoteTypeName;
        internal DateTime mNoteDate;
        internal Int64 mOperationID;
        internal String mOperationCode;
        internal Int64 mMasterOperationID;
        internal String mHouseNumber;
        internal Int32 mBranchID;
        internal String mMasterBL;
        internal String mMasterOperationCode;
        internal Int64 mOperationPartnerID;
        internal Int32 mPartnerTypeID;
        internal String mPartnerTypeCode;
        internal Int32 mPartnerID;
        internal String mPartnerName;
        internal String mVATNumber;
        internal String mTaxIdentificationNumber;
        internal String mAddress;
        internal String mCustomerPhone;
        internal Int64 mInvoiceID;
        internal String mConcatenatedInvoiceNumber;
        internal Int64 mAddressID;
        internal Int32 mCityID;
        internal String mCityName;
        internal Int32 mCountryID;
        internal String mCountryName;
        internal String mStreetLine1;
        internal String mStreetLine2;
        internal Int32 mAddressTypeID;
        internal String mPrintedAddress;
        internal Int32 mCurrencyID;
        internal String mCurrencyCode;
        internal Decimal mMasterDataExchangeRate;
        internal Decimal mExchangeRate;
        internal Decimal mAmountWithoutVAT;
        internal Int32 mTaxTypeID;
        internal String mTaxTypeName;
        internal Decimal mTaxPercentage;
        internal Decimal mTaxAmount;
        internal Int32 mDiscountTypeID;
        internal Decimal mDiscountPercentage;
        internal String mDiscountTypeName;
        internal Decimal mDiscountAmount;
        internal Decimal mAmount;
        internal Decimal mPaidAmount;
        internal Decimal mRemainingAmount;
        internal Int32 mNoteStatusID;
        internal String mNoteStatus;
        internal String mRemarks;
        internal Boolean mIsApproved;
        internal Boolean mIsDeleted;
        internal Int32 mApprovingUserID;
        internal String mApprovingUserName;
        internal Int32 mCreatorUserID;
        internal String mCreatorName;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal String mModificatorName;
        internal DateTime mModificationDate;
        internal Int32 mTaxAccount_ID;
        internal Int32 mTaxSubAccount_ID;
        internal Int32 mDiscAccount_ID;
        internal Int32 mDiscSubAccount_ID;
        internal Int64 mPayableID;
        internal String mPayableName;
        internal String mECode;
        #endregion

        #region "Methods"
        public Int64 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public Int64 CodeSerial
        {
            get { return mCodeSerial; }
            set { mCodeSerial = value; }
        }
        public String Code
        {
            get { return mCode; }
            set { mCode = value; }
        }
        public Int32 NoteType
        {
            get { return mNoteType; }
            set { mNoteType = value; }
        }
        public String NoteTypeName
        {
            get { return mNoteTypeName; }
            set { mNoteTypeName = value; }
        }
        public DateTime NoteDate
        {
            get { return mNoteDate; }
            set { mNoteDate = value; }
        }
        public Int64 OperationID
        {
            get { return mOperationID; }
            set { mOperationID = value; }
        }
        public String OperationCode
        {
            get { return mOperationCode; }
            set { mOperationCode = value; }
        }
        public Int64 MasterOperationID
        {
            get { return mMasterOperationID; }
            set { mMasterOperationID = value; }
        }
        public String HouseNumber
        {
            get { return mHouseNumber; }
            set { mHouseNumber = value; }
        }
        public Int32 BranchID
        {
            get { return mBranchID; }
            set { mBranchID = value; }
        }
        public String MasterBL
        {
            get { return mMasterBL; }
            set { mMasterBL = value; }
        }
        public String MasterOperationCode
        {
            get { return mMasterOperationCode; }
            set { mMasterOperationCode = value; }
        }
        public Int64 OperationPartnerID
        {
            get { return mOperationPartnerID; }
            set { mOperationPartnerID = value; }
        }
        public Int32 PartnerTypeID
        {
            get { return mPartnerTypeID; }
            set { mPartnerTypeID = value; }
        }
        public String PartnerTypeCode
        {
            get { return mPartnerTypeCode; }
            set { mPartnerTypeCode = value; }
        }
        public Int32 PartnerID
        {
            get { return mPartnerID; }
            set { mPartnerID = value; }
        }
        public String PartnerName
        {
            get { return mPartnerName; }
            set { mPartnerName = value; }
        }
        public String VATNumber
        {
            get { return mVATNumber; }
            set { mVATNumber = value; }
        }
        public String TaxIdentificationNumber
        {
            get { return mTaxIdentificationNumber; }
            set { mTaxIdentificationNumber = value; }
        }
        public String Address
        {
            get { return mAddress; }
            set { mAddress = value; }
        }
        public String CustomerPhone
        {
            get { return mCustomerPhone; }
            set { mCustomerPhone = value; }
        }
        public Int64 InvoiceID
        {
            get { return mInvoiceID; }
            set { mInvoiceID = value; }
        }
        public String ConcatenatedInvoiceNumber
        {
            get { return mConcatenatedInvoiceNumber; }
            set { mConcatenatedInvoiceNumber = value; }
        }
        public Int64 AddressID
        {
            get { return mAddressID; }
            set { mAddressID = value; }
        }
        public Int32 CityID
        {
            get { return mCityID; }
            set { mCityID = value; }
        }
        public String CityName
        {
            get { return mCityName; }
            set { mCityName = value; }
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
        public String StreetLine1
        {
            get { return mStreetLine1; }
            set { mStreetLine1 = value; }
        }
        public String StreetLine2
        {
            get { return mStreetLine2; }
            set { mStreetLine2 = value; }
        }
        public Int32 AddressTypeID
        {
            get { return mAddressTypeID; }
            set { mAddressTypeID = value; }
        }
        public String PrintedAddress
        {
            get { return mPrintedAddress; }
            set { mPrintedAddress = value; }
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
        public Decimal MasterDataExchangeRate
        {
            get { return mMasterDataExchangeRate; }
            set { mMasterDataExchangeRate = value; }
        }
        public Decimal ExchangeRate
        {
            get { return mExchangeRate; }
            set { mExchangeRate = value; }
        }
        public Decimal AmountWithoutVAT
        {
            get { return mAmountWithoutVAT; }
            set { mAmountWithoutVAT = value; }
        }
        public Int32 TaxTypeID
        {
            get { return mTaxTypeID; }
            set { mTaxTypeID = value; }
        }
        public String TaxTypeName
        {
            get { return mTaxTypeName; }
            set { mTaxTypeName = value; }
        }
        public Decimal TaxPercentage
        {
            get { return mTaxPercentage; }
            set { mTaxPercentage = value; }
        }
        public Decimal TaxAmount
        {
            get { return mTaxAmount; }
            set { mTaxAmount = value; }
        }
        public Int32 DiscountTypeID
        {
            get { return mDiscountTypeID; }
            set { mDiscountTypeID = value; }
        }
        public Decimal DiscountPercentage
        {
            get { return mDiscountPercentage; }
            set { mDiscountPercentage = value; }
        }
        public String DiscountTypeName
        {
            get { return mDiscountTypeName; }
            set { mDiscountTypeName = value; }
        }
        public Decimal DiscountAmount
        {
            get { return mDiscountAmount; }
            set { mDiscountAmount = value; }
        }
        public Decimal Amount
        {
            get { return mAmount; }
            set { mAmount = value; }
        }
        public Decimal PaidAmount
        {
            get { return mPaidAmount; }
            set { mPaidAmount = value; }
        }
        public Decimal RemainingAmount
        {
            get { return mRemainingAmount; }
            set { mRemainingAmount = value; }
        }
        public Int32 NoteStatusID
        {
            get { return mNoteStatusID; }
            set { mNoteStatusID = value; }
        }
        public String NoteStatus
        {
            get { return mNoteStatus; }
            set { mNoteStatus = value; }
        }
        public String Remarks
        {
            get { return mRemarks; }
            set { mRemarks = value; }
        }
        public Boolean IsApproved
        {
            get { return mIsApproved; }
            set { mIsApproved = value; }
        }
        public Boolean IsDeleted
        {
            get { return mIsDeleted; }
            set { mIsDeleted = value; }
        }
        public Int32 ApprovingUserID
        {
            get { return mApprovingUserID; }
            set { mApprovingUserID = value; }
        }
        public String ApprovingUserName
        {
            get { return mApprovingUserName; }
            set { mApprovingUserName = value; }
        }
        public Int32 CreatorUserID
        {
            get { return mCreatorUserID; }
            set { mCreatorUserID = value; }
        }
        public String CreatorName
        {
            get { return mCreatorName; }
            set { mCreatorName = value; }
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
        public String ModificatorName
        {
            get { return mModificatorName; }
            set { mModificatorName = value; }
        }
        public DateTime ModificationDate
        {
            get { return mModificationDate; }
            set { mModificationDate = value; }
        }
        public Int32 TaxAccount_ID
        {
            get { return mTaxAccount_ID; }
            set { mTaxAccount_ID = value; }
        }
        public Int32 TaxSubAccount_ID
        {
            get { return mTaxSubAccount_ID; }
            set { mTaxSubAccount_ID = value; }
        }
        public Int32 DiscAccount_ID
        {
            get { return mDiscAccount_ID; }
            set { mDiscAccount_ID = value; }
        }
        public Int32 DiscSubAccount_ID
        {
            get { return mDiscSubAccount_ID; }
            set { mDiscSubAccount_ID = value; }
        }
        public Int64 PayableID
        {
            get { return mPayableID; }
            set { mPayableID = value; }
        }
        public String PayableName
        {
            get { return mPayableName; }
            set { mPayableName = value; }
        }
        public String ECode
        {
            get { return mECode; }
            set { mECode = value; }
        }
        #endregion
    }

    public partial class CvwAccNote
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
        public List<CVarvwAccNote> lstCVarvwAccNote = new List<CVarvwAccNote>();
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
            lstCVarvwAccNote.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwAccNote";
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
                        CVarvwAccNote ObjCVarvwAccNote = new CVarvwAccNote();
                        ObjCVarvwAccNote.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwAccNote.mCodeSerial = Convert.ToInt64(dr["CodeSerial"].ToString());
                        ObjCVarvwAccNote.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwAccNote.mNoteType = Convert.ToInt32(dr["NoteType"].ToString());
                        ObjCVarvwAccNote.mNoteTypeName = Convert.ToString(dr["NoteTypeName"].ToString());
                        ObjCVarvwAccNote.mNoteDate = Convert.ToDateTime(dr["NoteDate"].ToString());
                        ObjCVarvwAccNote.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwAccNote.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwAccNote.mMasterOperationID = Convert.ToInt64(dr["MasterOperationID"].ToString());
                        ObjCVarvwAccNote.mHouseNumber = Convert.ToString(dr["HouseNumber"].ToString());
                        ObjCVarvwAccNote.mBranchID = Convert.ToInt32(dr["BranchID"].ToString());
                        ObjCVarvwAccNote.mMasterBL = Convert.ToString(dr["MasterBL"].ToString());
                        ObjCVarvwAccNote.mMasterOperationCode = Convert.ToString(dr["MasterOperationCode"].ToString());
                        ObjCVarvwAccNote.mOperationPartnerID = Convert.ToInt64(dr["OperationPartnerID"].ToString());
                        ObjCVarvwAccNote.mPartnerTypeID = Convert.ToInt32(dr["PartnerTypeID"].ToString());
                        ObjCVarvwAccNote.mPartnerTypeCode = Convert.ToString(dr["PartnerTypeCode"].ToString());
                        ObjCVarvwAccNote.mPartnerID = Convert.ToInt32(dr["PartnerID"].ToString());
                        ObjCVarvwAccNote.mPartnerName = Convert.ToString(dr["PartnerName"].ToString());
                        ObjCVarvwAccNote.mVATNumber = Convert.ToString(dr["VATNumber"].ToString());
                        ObjCVarvwAccNote.mTaxIdentificationNumber = Convert.ToString(dr["TaxIdentificationNumber"].ToString());
                        ObjCVarvwAccNote.mAddress = Convert.ToString(dr["Address"].ToString());
                        ObjCVarvwAccNote.mCustomerPhone = Convert.ToString(dr["CustomerPhone"].ToString());
                        ObjCVarvwAccNote.mInvoiceID = Convert.ToInt64(dr["InvoiceID"].ToString());
                        ObjCVarvwAccNote.mConcatenatedInvoiceNumber = Convert.ToString(dr["ConcatenatedInvoiceNumber"].ToString());
                        ObjCVarvwAccNote.mAddressID = Convert.ToInt64(dr["AddressID"].ToString());
                        ObjCVarvwAccNote.mCityID = Convert.ToInt32(dr["CityID"].ToString());
                        ObjCVarvwAccNote.mCityName = Convert.ToString(dr["CityName"].ToString());
                        ObjCVarvwAccNote.mCountryID = Convert.ToInt32(dr["CountryID"].ToString());
                        ObjCVarvwAccNote.mCountryName = Convert.ToString(dr["CountryName"].ToString());
                        ObjCVarvwAccNote.mStreetLine1 = Convert.ToString(dr["StreetLine1"].ToString());
                        ObjCVarvwAccNote.mStreetLine2 = Convert.ToString(dr["StreetLine2"].ToString());
                        ObjCVarvwAccNote.mAddressTypeID = Convert.ToInt32(dr["AddressTypeID"].ToString());
                        ObjCVarvwAccNote.mPrintedAddress = Convert.ToString(dr["PrintedAddress"].ToString());
                        ObjCVarvwAccNote.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwAccNote.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwAccNote.mMasterDataExchangeRate = Convert.ToDecimal(dr["MasterDataExchangeRate"].ToString());
                        ObjCVarvwAccNote.mExchangeRate = Convert.ToDecimal(dr["ExchangeRate"].ToString());
                        ObjCVarvwAccNote.mAmountWithoutVAT = Convert.ToDecimal(dr["AmountWithoutVAT"].ToString());
                        ObjCVarvwAccNote.mTaxTypeID = Convert.ToInt32(dr["TaxTypeID"].ToString());
                        ObjCVarvwAccNote.mTaxTypeName = Convert.ToString(dr["TaxTypeName"].ToString());
                        ObjCVarvwAccNote.mTaxPercentage = Convert.ToDecimal(dr["TaxPercentage"].ToString());
                        ObjCVarvwAccNote.mTaxAmount = Convert.ToDecimal(dr["TaxAmount"].ToString());
                        ObjCVarvwAccNote.mDiscountTypeID = Convert.ToInt32(dr["DiscountTypeID"].ToString());
                        ObjCVarvwAccNote.mDiscountPercentage = Convert.ToDecimal(dr["DiscountPercentage"].ToString());
                        ObjCVarvwAccNote.mDiscountTypeName = Convert.ToString(dr["DiscountTypeName"].ToString());
                        ObjCVarvwAccNote.mDiscountAmount = Convert.ToDecimal(dr["DiscountAmount"].ToString());
                        ObjCVarvwAccNote.mAmount = Convert.ToDecimal(dr["Amount"].ToString());
                        ObjCVarvwAccNote.mPaidAmount = Convert.ToDecimal(dr["PaidAmount"].ToString());
                        ObjCVarvwAccNote.mRemainingAmount = Convert.ToDecimal(dr["RemainingAmount"].ToString());
                        ObjCVarvwAccNote.mNoteStatusID = Convert.ToInt32(dr["NoteStatusID"].ToString());
                        ObjCVarvwAccNote.mNoteStatus = Convert.ToString(dr["NoteStatus"].ToString());
                        ObjCVarvwAccNote.mRemarks = Convert.ToString(dr["Remarks"].ToString());
                        ObjCVarvwAccNote.mIsApproved = Convert.ToBoolean(dr["IsApproved"].ToString());
                        ObjCVarvwAccNote.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarvwAccNote.mApprovingUserID = Convert.ToInt32(dr["ApprovingUserID"].ToString());
                        ObjCVarvwAccNote.mApprovingUserName = Convert.ToString(dr["ApprovingUserName"].ToString());
                        ObjCVarvwAccNote.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwAccNote.mCreatorName = Convert.ToString(dr["CreatorName"].ToString());
                        ObjCVarvwAccNote.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwAccNote.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwAccNote.mModificatorName = Convert.ToString(dr["ModificatorName"].ToString());
                        ObjCVarvwAccNote.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwAccNote.mTaxAccount_ID = Convert.ToInt32(dr["TaxAccount_ID"].ToString());
                        ObjCVarvwAccNote.mTaxSubAccount_ID = Convert.ToInt32(dr["TaxSubAccount_ID"].ToString());
                        ObjCVarvwAccNote.mDiscAccount_ID = Convert.ToInt32(dr["DiscAccount_ID"].ToString());
                        ObjCVarvwAccNote.mDiscSubAccount_ID = Convert.ToInt32(dr["DiscSubAccount_ID"].ToString());
                        ObjCVarvwAccNote.mPayableID = Convert.ToInt64(dr["PayableID"].ToString());
                        ObjCVarvwAccNote.mPayableName = Convert.ToString(dr["PayableName"].ToString());
                        ObjCVarvwAccNote.mECode = Convert.ToString(dr["ECode"].ToString());
                        lstCVarvwAccNote.Add(ObjCVarvwAccNote);
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
            lstCVarvwAccNote.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwAccNote";
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
                        CVarvwAccNote ObjCVarvwAccNote = new CVarvwAccNote();
                        ObjCVarvwAccNote.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwAccNote.mCodeSerial = Convert.ToInt64(dr["CodeSerial"].ToString());
                        ObjCVarvwAccNote.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwAccNote.mNoteType = Convert.ToInt32(dr["NoteType"].ToString());
                        ObjCVarvwAccNote.mNoteTypeName = Convert.ToString(dr["NoteTypeName"].ToString());
                        ObjCVarvwAccNote.mNoteDate = Convert.ToDateTime(dr["NoteDate"].ToString());
                        ObjCVarvwAccNote.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwAccNote.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwAccNote.mMasterOperationID = Convert.ToInt64(dr["MasterOperationID"].ToString());
                        ObjCVarvwAccNote.mHouseNumber = Convert.ToString(dr["HouseNumber"].ToString());
                        ObjCVarvwAccNote.mBranchID = Convert.ToInt32(dr["BranchID"].ToString());
                        ObjCVarvwAccNote.mMasterBL = Convert.ToString(dr["MasterBL"].ToString());
                        ObjCVarvwAccNote.mMasterOperationCode = Convert.ToString(dr["MasterOperationCode"].ToString());
                        ObjCVarvwAccNote.mOperationPartnerID = Convert.ToInt64(dr["OperationPartnerID"].ToString());
                        ObjCVarvwAccNote.mPartnerTypeID = Convert.ToInt32(dr["PartnerTypeID"].ToString());
                        ObjCVarvwAccNote.mPartnerTypeCode = Convert.ToString(dr["PartnerTypeCode"].ToString());
                        ObjCVarvwAccNote.mPartnerID = Convert.ToInt32(dr["PartnerID"].ToString());
                        ObjCVarvwAccNote.mPartnerName = Convert.ToString(dr["PartnerName"].ToString());
                        ObjCVarvwAccNote.mVATNumber = Convert.ToString(dr["VATNumber"].ToString());
                        ObjCVarvwAccNote.mTaxIdentificationNumber = Convert.ToString(dr["TaxIdentificationNumber"].ToString());
                        ObjCVarvwAccNote.mAddress = Convert.ToString(dr["Address"].ToString());
                        ObjCVarvwAccNote.mCustomerPhone = Convert.ToString(dr["CustomerPhone"].ToString());
                        ObjCVarvwAccNote.mInvoiceID = Convert.ToInt64(dr["InvoiceID"].ToString());
                        ObjCVarvwAccNote.mConcatenatedInvoiceNumber = Convert.ToString(dr["ConcatenatedInvoiceNumber"].ToString());
                        ObjCVarvwAccNote.mAddressID = Convert.ToInt64(dr["AddressID"].ToString());
                        ObjCVarvwAccNote.mCityID = Convert.ToInt32(dr["CityID"].ToString());
                        ObjCVarvwAccNote.mCityName = Convert.ToString(dr["CityName"].ToString());
                        ObjCVarvwAccNote.mCountryID = Convert.ToInt32(dr["CountryID"].ToString());
                        ObjCVarvwAccNote.mCountryName = Convert.ToString(dr["CountryName"].ToString());
                        ObjCVarvwAccNote.mStreetLine1 = Convert.ToString(dr["StreetLine1"].ToString());
                        ObjCVarvwAccNote.mStreetLine2 = Convert.ToString(dr["StreetLine2"].ToString());
                        ObjCVarvwAccNote.mAddressTypeID = Convert.ToInt32(dr["AddressTypeID"].ToString());
                        ObjCVarvwAccNote.mPrintedAddress = Convert.ToString(dr["PrintedAddress"].ToString());
                        ObjCVarvwAccNote.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwAccNote.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwAccNote.mMasterDataExchangeRate = Convert.ToDecimal(dr["MasterDataExchangeRate"].ToString());
                        ObjCVarvwAccNote.mExchangeRate = Convert.ToDecimal(dr["ExchangeRate"].ToString());
                        ObjCVarvwAccNote.mAmountWithoutVAT = Convert.ToDecimal(dr["AmountWithoutVAT"].ToString());
                        ObjCVarvwAccNote.mTaxTypeID = Convert.ToInt32(dr["TaxTypeID"].ToString());
                        ObjCVarvwAccNote.mTaxTypeName = Convert.ToString(dr["TaxTypeName"].ToString());
                        ObjCVarvwAccNote.mTaxPercentage = Convert.ToDecimal(dr["TaxPercentage"].ToString());
                        ObjCVarvwAccNote.mTaxAmount = Convert.ToDecimal(dr["TaxAmount"].ToString());
                        ObjCVarvwAccNote.mDiscountTypeID = Convert.ToInt32(dr["DiscountTypeID"].ToString());
                        ObjCVarvwAccNote.mDiscountPercentage = Convert.ToDecimal(dr["DiscountPercentage"].ToString());
                        ObjCVarvwAccNote.mDiscountTypeName = Convert.ToString(dr["DiscountTypeName"].ToString());
                        ObjCVarvwAccNote.mDiscountAmount = Convert.ToDecimal(dr["DiscountAmount"].ToString());
                        ObjCVarvwAccNote.mAmount = Convert.ToDecimal(dr["Amount"].ToString());
                        ObjCVarvwAccNote.mPaidAmount = Convert.ToDecimal(dr["PaidAmount"].ToString());
                        ObjCVarvwAccNote.mRemainingAmount = Convert.ToDecimal(dr["RemainingAmount"].ToString());
                        ObjCVarvwAccNote.mNoteStatusID = Convert.ToInt32(dr["NoteStatusID"].ToString());
                        ObjCVarvwAccNote.mNoteStatus = Convert.ToString(dr["NoteStatus"].ToString());
                        ObjCVarvwAccNote.mRemarks = Convert.ToString(dr["Remarks"].ToString());
                        ObjCVarvwAccNote.mIsApproved = Convert.ToBoolean(dr["IsApproved"].ToString());
                        ObjCVarvwAccNote.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarvwAccNote.mApprovingUserID = Convert.ToInt32(dr["ApprovingUserID"].ToString());
                        ObjCVarvwAccNote.mApprovingUserName = Convert.ToString(dr["ApprovingUserName"].ToString());
                        ObjCVarvwAccNote.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwAccNote.mCreatorName = Convert.ToString(dr["CreatorName"].ToString());
                        ObjCVarvwAccNote.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwAccNote.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwAccNote.mModificatorName = Convert.ToString(dr["ModificatorName"].ToString());
                        ObjCVarvwAccNote.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwAccNote.mTaxAccount_ID = Convert.ToInt32(dr["TaxAccount_ID"].ToString());
                        ObjCVarvwAccNote.mTaxSubAccount_ID = Convert.ToInt32(dr["TaxSubAccount_ID"].ToString());
                        ObjCVarvwAccNote.mDiscAccount_ID = Convert.ToInt32(dr["DiscAccount_ID"].ToString());
                        ObjCVarvwAccNote.mDiscSubAccount_ID = Convert.ToInt32(dr["DiscSubAccount_ID"].ToString());
                        ObjCVarvwAccNote.mPayableID = Convert.ToInt64(dr["PayableID"].ToString());
                        ObjCVarvwAccNote.mPayableName = Convert.ToString(dr["PayableName"].ToString());
                        ObjCVarvwAccNote.mECode = Convert.ToString(dr["ECode"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwAccNote.Add(ObjCVarvwAccNote);
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
