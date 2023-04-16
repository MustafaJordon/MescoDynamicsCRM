﻿using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.PricingModule.PricingTab
{
    [Serializable]
    public class CPKvwPricing
    {
        #region "variables"
        private Int64 mID;
        #endregion

        #region "Methods"
        public Int64 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        #endregion
    }
    [Serializable]
    public partial class CVarvwPricing : CPKvwPricing
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mShippingLineID;
        internal String mShippingLineName;
        internal Int32 mAirlineID;
        internal String mAirlineName;
        internal Int32 mTruckerID;
        internal String mTruckerName;
        internal Int32 mCCAID;
        internal String mCustomsClearanceAgentName;
        internal Int32 mPricingTypeID;
        internal String mPricingTypeCode;
        internal Int32 mSupplierID;
        internal String mSupplierName;
        internal Int32 mPOLCountryID;
        internal String mPOLCountryCode;
        internal String mPOLCountryName;
        internal Int32 mPOLID;
        internal String mPOLCode;
        internal String mPOLName;
        internal Int32 mPODCountryID;
        internal String mPODCountryCode;
        internal String mPODCountryName;
        internal Int32 mPODID;
        internal String mPODCode;
        internal String mPODName;
        internal Int32 mEquipmentID;
        internal String mContainerTypeCode;
        internal Int32 mPackageTypeID;
        internal String mPackageTypeName;
        internal Int32 mCommodityID;
        internal String mCommodityName;
        internal Int32 mTransitTime;
        internal Int32 mFrequency;
        internal String mFrequencyNotes;
        internal DateTime mValidFrom;
        internal DateTime mValidTo;
        internal Int32 mCurrencyID;
        internal String mCurrencyCode;
        internal Decimal mExchangeRate;
        internal String mNotes;
        internal Int32 mCreatorUserID;
        internal String mCreatorName;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal String mModificatorName;
        internal DateTime mModificationDate;
        internal Int32 mCustomerID;
        internal String mCustomerName;
        internal Int32 mAgentID;
        internal String mAgentName;
        internal String mCustomerReference;
        internal Boolean mIsPricingRequest;
        #endregion

        #region "Methods"
        public Int32 ShippingLineID
        {
            get { return mShippingLineID; }
            set { mShippingLineID = value; }
        }
        public String ShippingLineName
        {
            get { return mShippingLineName; }
            set { mShippingLineName = value; }
        }
        public Int32 AirlineID
        {
            get { return mAirlineID; }
            set { mAirlineID = value; }
        }
        public String AirlineName
        {
            get { return mAirlineName; }
            set { mAirlineName = value; }
        }
        public Int32 TruckerID
        {
            get { return mTruckerID; }
            set { mTruckerID = value; }
        }
        public String TruckerName
        {
            get { return mTruckerName; }
            set { mTruckerName = value; }
        }
        public Int32 CCAID
        {
            get { return mCCAID; }
            set { mCCAID = value; }
        }
        public String CustomsClearanceAgentName
        {
            get { return mCustomsClearanceAgentName; }
            set { mCustomsClearanceAgentName = value; }
        }
        public Int32 PricingTypeID
        {
            get { return mPricingTypeID; }
            set { mPricingTypeID = value; }
        }
        public String PricingTypeCode
        {
            get { return mPricingTypeCode; }
            set { mPricingTypeCode = value; }
        }
        public Int32 SupplierID
        {
            get { return mSupplierID; }
            set { mSupplierID = value; }
        }
        public String SupplierName
        {
            get { return mSupplierName; }
            set { mSupplierName = value; }
        }
        public Int32 POLCountryID
        {
            get { return mPOLCountryID; }
            set { mPOLCountryID = value; }
        }
        public String POLCountryCode
        {
            get { return mPOLCountryCode; }
            set { mPOLCountryCode = value; }
        }
        public String POLCountryName
        {
            get { return mPOLCountryName; }
            set { mPOLCountryName = value; }
        }
        public Int32 POLID
        {
            get { return mPOLID; }
            set { mPOLID = value; }
        }
        public String POLCode
        {
            get { return mPOLCode; }
            set { mPOLCode = value; }
        }
        public String POLName
        {
            get { return mPOLName; }
            set { mPOLName = value; }
        }
        public Int32 PODCountryID
        {
            get { return mPODCountryID; }
            set { mPODCountryID = value; }
        }
        public String PODCountryCode
        {
            get { return mPODCountryCode; }
            set { mPODCountryCode = value; }
        }
        public String PODCountryName
        {
            get { return mPODCountryName; }
            set { mPODCountryName = value; }
        }
        public Int32 PODID
        {
            get { return mPODID; }
            set { mPODID = value; }
        }
        public String PODCode
        {
            get { return mPODCode; }
            set { mPODCode = value; }
        }
        public String PODName
        {
            get { return mPODName; }
            set { mPODName = value; }
        }
        public Int32 EquipmentID
        {
            get { return mEquipmentID; }
            set { mEquipmentID = value; }
        }
        public String ContainerTypeCode
        {
            get { return mContainerTypeCode; }
            set { mContainerTypeCode = value; }
        }
        public Int32 PackageTypeID
        {
            get { return mPackageTypeID; }
            set { mPackageTypeID = value; }
        }
        public String PackageTypeName
        {
            get { return mPackageTypeName; }
            set { mPackageTypeName = value; }
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
        public Int32 TransitTime
        {
            get { return mTransitTime; }
            set { mTransitTime = value; }
        }
        public Int32 Frequency
        {
            get { return mFrequency; }
            set { mFrequency = value; }
        }
        public String FrequencyNotes
        {
            get { return mFrequencyNotes; }
            set { mFrequencyNotes = value; }
        }
        public DateTime ValidFrom
        {
            get { return mValidFrom; }
            set { mValidFrom = value; }
        }
        public DateTime ValidTo
        {
            get { return mValidTo; }
            set { mValidTo = value; }
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
        public Decimal ExchangeRate
        {
            get { return mExchangeRate; }
            set { mExchangeRate = value; }
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
        public Int32 AgentID
        {
            get { return mAgentID; }
            set { mAgentID = value; }
        }
        public String AgentName
        {
            get { return mAgentName; }
            set { mAgentName = value; }
        }
        public String CustomerReference
        {
            get { return mCustomerReference; }
            set { mCustomerReference = value; }
        }
        public Boolean IsPricingRequest
        {
            get { return mIsPricingRequest; }
            set { mIsPricingRequest = value; }
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

    public partial class CvwPricing
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
        public List<CVarvwPricing> lstCVarvwPricing = new List<CVarvwPricing>();
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
            lstCVarvwPricing.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwPricing";
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
                        CVarvwPricing ObjCVarvwPricing = new CVarvwPricing();
                        ObjCVarvwPricing.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwPricing.mShippingLineID = Convert.ToInt32(dr["ShippingLineID"].ToString());
                        ObjCVarvwPricing.mShippingLineName = Convert.ToString(dr["ShippingLineName"].ToString());
                        ObjCVarvwPricing.mAirlineID = Convert.ToInt32(dr["AirlineID"].ToString());
                        ObjCVarvwPricing.mAirlineName = Convert.ToString(dr["AirlineName"].ToString());
                        ObjCVarvwPricing.mTruckerID = Convert.ToInt32(dr["TruckerID"].ToString());
                        ObjCVarvwPricing.mTruckerName = Convert.ToString(dr["TruckerName"].ToString());
                        ObjCVarvwPricing.mCCAID = Convert.ToInt32(dr["CCAID"].ToString());
                        ObjCVarvwPricing.mCustomsClearanceAgentName = Convert.ToString(dr["CustomsClearanceAgentName"].ToString());
                        ObjCVarvwPricing.mPricingTypeID = Convert.ToInt32(dr["PricingTypeID"].ToString());
                        ObjCVarvwPricing.mPricingTypeCode = Convert.ToString(dr["PricingTypeCode"].ToString());
                        ObjCVarvwPricing.mSupplierID = Convert.ToInt32(dr["SupplierID"].ToString());
                        ObjCVarvwPricing.mSupplierName = Convert.ToString(dr["SupplierName"].ToString());
                        ObjCVarvwPricing.mPOLCountryID = Convert.ToInt32(dr["POLCountryID"].ToString());
                        ObjCVarvwPricing.mPOLCountryCode = Convert.ToString(dr["POLCountryCode"].ToString());
                        ObjCVarvwPricing.mPOLCountryName = Convert.ToString(dr["POLCountryName"].ToString());
                        ObjCVarvwPricing.mPOLID = Convert.ToInt32(dr["POLID"].ToString());
                        ObjCVarvwPricing.mPOLCode = Convert.ToString(dr["POLCode"].ToString());
                        ObjCVarvwPricing.mPOLName = Convert.ToString(dr["POLName"].ToString());
                        ObjCVarvwPricing.mPODCountryID = Convert.ToInt32(dr["PODCountryID"].ToString());
                        ObjCVarvwPricing.mPODCountryCode = Convert.ToString(dr["PODCountryCode"].ToString());
                        ObjCVarvwPricing.mPODCountryName = Convert.ToString(dr["PODCountryName"].ToString());
                        ObjCVarvwPricing.mPODID = Convert.ToInt32(dr["PODID"].ToString());
                        ObjCVarvwPricing.mPODCode = Convert.ToString(dr["PODCode"].ToString());
                        ObjCVarvwPricing.mPODName = Convert.ToString(dr["PODName"].ToString());
                        ObjCVarvwPricing.mEquipmentID = Convert.ToInt32(dr["EquipmentID"].ToString());
                        ObjCVarvwPricing.mContainerTypeCode = Convert.ToString(dr["ContainerTypeCode"].ToString());
                        ObjCVarvwPricing.mPackageTypeID = Convert.ToInt32(dr["PackageTypeID"].ToString());
                        ObjCVarvwPricing.mPackageTypeName = Convert.ToString(dr["PackageTypeName"].ToString());
                        ObjCVarvwPricing.mCommodityID = Convert.ToInt32(dr["CommodityID"].ToString());
                        ObjCVarvwPricing.mCommodityName = Convert.ToString(dr["CommodityName"].ToString());
                        ObjCVarvwPricing.mTransitTime = Convert.ToInt32(dr["TransitTime"].ToString());
                        ObjCVarvwPricing.mFrequency = Convert.ToInt32(dr["Frequency"].ToString());
                        ObjCVarvwPricing.mFrequencyNotes = Convert.ToString(dr["FrequencyNotes"].ToString());
                        ObjCVarvwPricing.mValidFrom = Convert.ToDateTime(dr["ValidFrom"].ToString());
                        ObjCVarvwPricing.mValidTo = Convert.ToDateTime(dr["ValidTo"].ToString());
                        ObjCVarvwPricing.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwPricing.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwPricing.mExchangeRate = Convert.ToDecimal(dr["ExchangeRate"].ToString());
                        ObjCVarvwPricing.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwPricing.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwPricing.mCreatorName = Convert.ToString(dr["CreatorName"].ToString());
                        ObjCVarvwPricing.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwPricing.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwPricing.mModificatorName = Convert.ToString(dr["ModificatorName"].ToString());
                        ObjCVarvwPricing.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwPricing.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarvwPricing.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwPricing.mAgentID = Convert.ToInt32(dr["AgentID"].ToString());
                        ObjCVarvwPricing.mAgentName = Convert.ToString(dr["AgentName"].ToString());
                        ObjCVarvwPricing.mCustomerReference = Convert.ToString(dr["CustomerReference"].ToString());
                        ObjCVarvwPricing.mIsPricingRequest = Convert.ToBoolean(dr["IsPricingRequest"].ToString());
                        lstCVarvwPricing.Add(ObjCVarvwPricing);
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
            lstCVarvwPricing.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwPricing";
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
                        CVarvwPricing ObjCVarvwPricing = new CVarvwPricing();
                        ObjCVarvwPricing.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwPricing.mShippingLineID = Convert.ToInt32(dr["ShippingLineID"].ToString());
                        ObjCVarvwPricing.mShippingLineName = Convert.ToString(dr["ShippingLineName"].ToString());
                        ObjCVarvwPricing.mAirlineID = Convert.ToInt32(dr["AirlineID"].ToString());
                        ObjCVarvwPricing.mAirlineName = Convert.ToString(dr["AirlineName"].ToString());
                        ObjCVarvwPricing.mTruckerID = Convert.ToInt32(dr["TruckerID"].ToString());
                        ObjCVarvwPricing.mTruckerName = Convert.ToString(dr["TruckerName"].ToString());
                        ObjCVarvwPricing.mCCAID = Convert.ToInt32(dr["CCAID"].ToString());
                        ObjCVarvwPricing.mCustomsClearanceAgentName = Convert.ToString(dr["CustomsClearanceAgentName"].ToString());
                        ObjCVarvwPricing.mPricingTypeID = Convert.ToInt32(dr["PricingTypeID"].ToString());
                        ObjCVarvwPricing.mPricingTypeCode = Convert.ToString(dr["PricingTypeCode"].ToString());
                        ObjCVarvwPricing.mSupplierID = Convert.ToInt32(dr["SupplierID"].ToString());
                        ObjCVarvwPricing.mSupplierName = Convert.ToString(dr["SupplierName"].ToString());
                        ObjCVarvwPricing.mPOLCountryID = Convert.ToInt32(dr["POLCountryID"].ToString());
                        ObjCVarvwPricing.mPOLCountryCode = Convert.ToString(dr["POLCountryCode"].ToString());
                        ObjCVarvwPricing.mPOLCountryName = Convert.ToString(dr["POLCountryName"].ToString());
                        ObjCVarvwPricing.mPOLID = Convert.ToInt32(dr["POLID"].ToString());
                        ObjCVarvwPricing.mPOLCode = Convert.ToString(dr["POLCode"].ToString());
                        ObjCVarvwPricing.mPOLName = Convert.ToString(dr["POLName"].ToString());
                        ObjCVarvwPricing.mPODCountryID = Convert.ToInt32(dr["PODCountryID"].ToString());
                        ObjCVarvwPricing.mPODCountryCode = Convert.ToString(dr["PODCountryCode"].ToString());
                        ObjCVarvwPricing.mPODCountryName = Convert.ToString(dr["PODCountryName"].ToString());
                        ObjCVarvwPricing.mPODID = Convert.ToInt32(dr["PODID"].ToString());
                        ObjCVarvwPricing.mPODCode = Convert.ToString(dr["PODCode"].ToString());
                        ObjCVarvwPricing.mPODName = Convert.ToString(dr["PODName"].ToString());
                        ObjCVarvwPricing.mEquipmentID = Convert.ToInt32(dr["EquipmentID"].ToString());
                        ObjCVarvwPricing.mContainerTypeCode = Convert.ToString(dr["ContainerTypeCode"].ToString());
                        ObjCVarvwPricing.mPackageTypeID = Convert.ToInt32(dr["PackageTypeID"].ToString());
                        ObjCVarvwPricing.mPackageTypeName = Convert.ToString(dr["PackageTypeName"].ToString());
                        ObjCVarvwPricing.mCommodityID = Convert.ToInt32(dr["CommodityID"].ToString());
                        ObjCVarvwPricing.mCommodityName = Convert.ToString(dr["CommodityName"].ToString());
                        ObjCVarvwPricing.mTransitTime = Convert.ToInt32(dr["TransitTime"].ToString());
                        ObjCVarvwPricing.mFrequency = Convert.ToInt32(dr["Frequency"].ToString());
                        ObjCVarvwPricing.mFrequencyNotes = Convert.ToString(dr["FrequencyNotes"].ToString());
                        ObjCVarvwPricing.mValidFrom = Convert.ToDateTime(dr["ValidFrom"].ToString());
                        ObjCVarvwPricing.mValidTo = Convert.ToDateTime(dr["ValidTo"].ToString());
                        ObjCVarvwPricing.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwPricing.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwPricing.mExchangeRate = Convert.ToDecimal(dr["ExchangeRate"].ToString());
                        ObjCVarvwPricing.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwPricing.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwPricing.mCreatorName = Convert.ToString(dr["CreatorName"].ToString());
                        ObjCVarvwPricing.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwPricing.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwPricing.mModificatorName = Convert.ToString(dr["ModificatorName"].ToString());
                        ObjCVarvwPricing.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwPricing.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarvwPricing.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwPricing.mAgentID = Convert.ToInt32(dr["AgentID"].ToString());
                        ObjCVarvwPricing.mAgentName = Convert.ToString(dr["AgentName"].ToString());
                        ObjCVarvwPricing.mCustomerReference = Convert.ToString(dr["CustomerReference"].ToString());
                        ObjCVarvwPricing.mIsPricingRequest = Convert.ToBoolean(dr["IsPricingRequest"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwPricing.Add(ObjCVarvwPricing);
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
