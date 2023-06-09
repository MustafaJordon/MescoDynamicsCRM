﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.Quotations.Quotations.Generated.Old
{
    [Serializable]
    public class CPKQuotationCharges
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
    public partial class CVarQuotationCharges : CPKQuotationCharges
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mQuotationRouteID;
        internal Int32 mChargeTypeID;
        internal Int32 mMeasurementID;
        internal Int32 mContainerTypeID;
        internal Int32 mDemurrageDays;
        internal Int32 mPackageTypeID;
        internal Decimal mCostQuantity;
        internal Decimal mCostPrice;
        internal Decimal mCostAmount;
        internal Int32 mCostCurrencyID;
        internal Decimal mCostExchangeRate;
        internal Decimal mSaleQuantity;
        internal Decimal mSalePrice;
        internal Decimal mSaleAmount;
        internal Int32 mSaleCurrencyID;
        internal Decimal mSaleExchangeRate;
        internal Int32 mOperationPartnerTypeID;
        internal Int32 mCustomerID;
        internal Int32 mAgentID;
        internal Int32 mShippingAgentID;
        internal Int32 mCustomsClearanceAgentID;
        internal Int32 mShippingLineID;
        internal Int32 mAirlineID;
        internal Int32 mTruckerID;
        internal Int32 mSupplierID;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Int64 mPricingID;
        internal String mNotes;
        internal Int32 mViewOrder;
        internal Int32 mSupplierSiteID;
        internal Decimal mAdditionalAmount;
        internal Int32 mPOrC;
        #endregion

        #region "Methods"
        public Int64 QuotationRouteID
        {
            get { return mQuotationRouteID; }
            set { mIsChanges = true; mQuotationRouteID = value; }
        }
        public Int32 ChargeTypeID
        {
            get { return mChargeTypeID; }
            set { mIsChanges = true; mChargeTypeID = value; }
        }
        public Int32 MeasurementID
        {
            get { return mMeasurementID; }
            set { mIsChanges = true; mMeasurementID = value; }
        }
        public Int32 ContainerTypeID
        {
            get { return mContainerTypeID; }
            set { mIsChanges = true; mContainerTypeID = value; }
        }
        public Int32 DemurrageDays
        {
            get { return mDemurrageDays; }
            set { mIsChanges = true; mDemurrageDays = value; }
        }
        public Int32 PackageTypeID
        {
            get { return mPackageTypeID; }
            set { mIsChanges = true; mPackageTypeID = value; }
        }
        public Decimal CostQuantity
        {
            get { return mCostQuantity; }
            set { mIsChanges = true; mCostQuantity = value; }
        }
        public Decimal CostPrice
        {
            get { return mCostPrice; }
            set { mIsChanges = true; mCostPrice = value; }
        }
        public Decimal CostAmount
        {
            get { return mCostAmount; }
            set { mIsChanges = true; mCostAmount = value; }
        }
        public Int32 CostCurrencyID
        {
            get { return mCostCurrencyID; }
            set { mIsChanges = true; mCostCurrencyID = value; }
        }
        public Decimal CostExchangeRate
        {
            get { return mCostExchangeRate; }
            set { mIsChanges = true; mCostExchangeRate = value; }
        }
        public Decimal SaleQuantity
        {
            get { return mSaleQuantity; }
            set { mIsChanges = true; mSaleQuantity = value; }
        }
        public Decimal SalePrice
        {
            get { return mSalePrice; }
            set { mIsChanges = true; mSalePrice = value; }
        }
        public Decimal SaleAmount
        {
            get { return mSaleAmount; }
            set { mIsChanges = true; mSaleAmount = value; }
        }
        public Int32 SaleCurrencyID
        {
            get { return mSaleCurrencyID; }
            set { mIsChanges = true; mSaleCurrencyID = value; }
        }
        public Decimal SaleExchangeRate
        {
            get { return mSaleExchangeRate; }
            set { mIsChanges = true; mSaleExchangeRate = value; }
        }
        public Int32 OperationPartnerTypeID
        {
            get { return mOperationPartnerTypeID; }
            set { mIsChanges = true; mOperationPartnerTypeID = value; }
        }
        public Int32 CustomerID
        {
            get { return mCustomerID; }
            set { mIsChanges = true; mCustomerID = value; }
        }
        public Int32 AgentID
        {
            get { return mAgentID; }
            set { mIsChanges = true; mAgentID = value; }
        }
        public Int32 ShippingAgentID
        {
            get { return mShippingAgentID; }
            set { mIsChanges = true; mShippingAgentID = value; }
        }
        public Int32 CustomsClearanceAgentID
        {
            get { return mCustomsClearanceAgentID; }
            set { mIsChanges = true; mCustomsClearanceAgentID = value; }
        }
        public Int32 ShippingLineID
        {
            get { return mShippingLineID; }
            set { mIsChanges = true; mShippingLineID = value; }
        }
        public Int32 AirlineID
        {
            get { return mAirlineID; }
            set { mIsChanges = true; mAirlineID = value; }
        }
        public Int32 TruckerID
        {
            get { return mTruckerID; }
            set { mIsChanges = true; mTruckerID = value; }
        }
        public Int32 SupplierID
        {
            get { return mSupplierID; }
            set { mIsChanges = true; mSupplierID = value; }
        }
        public Int32 CreatorUserID
        {
            get { return mCreatorUserID; }
            set { mIsChanges = true; mCreatorUserID = value; }
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
        public Int64 PricingID
        {
            get { return mPricingID; }
            set { mIsChanges = true; mPricingID = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mIsChanges = true; mNotes = value; }
        }
        public Int32 ViewOrder
        {
            get { return mViewOrder; }
            set { mIsChanges = true; mViewOrder = value; }
        }
        public Int32 SupplierSiteID
        {
            get { return mSupplierSiteID; }
            set { mIsChanges = true; mSupplierSiteID = value; }
        }
        public Decimal AdditionalAmount
        {
            get { return mAdditionalAmount; }
            set { mIsChanges = true; mAdditionalAmount = value; }
        }
        public Int32 POrC
        {
            get { return mPOrC; }
            set { mIsChanges = true; mPOrC = value; }
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

    public partial class CQuotationCharges
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
        public List<CVarQuotationCharges> lstCVarQuotationCharges = new List<CVarQuotationCharges>();
        public List<CPKQuotationCharges> lstDeletedCPKQuotationCharges = new List<CPKQuotationCharges>();
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
        public Exception GetItem(Int64 ID)
        {
            return DataFill(Convert.ToString(ID), false);
        }
        private Exception DataFill(string Param, Boolean IsList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarQuotationCharges.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListQuotationCharges";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemQuotationCharges";
                    Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt));
                    Com.Parameters[0].Value = Convert.ToInt64(Param);
                }
                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        /*Start DataReader*/
                        CVarQuotationCharges ObjCVarQuotationCharges = new CVarQuotationCharges();
                        ObjCVarQuotationCharges.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarQuotationCharges.mQuotationRouteID = Convert.ToInt64(dr["QuotationRouteID"].ToString());
                        ObjCVarQuotationCharges.mChargeTypeID = Convert.ToInt32(dr["ChargeTypeID"].ToString());
                        ObjCVarQuotationCharges.mMeasurementID = Convert.ToInt32(dr["MeasurementID"].ToString());
                        ObjCVarQuotationCharges.mContainerTypeID = Convert.ToInt32(dr["ContainerTypeID"].ToString());
                        ObjCVarQuotationCharges.mDemurrageDays = Convert.ToInt32(dr["DemurrageDays"].ToString());
                        ObjCVarQuotationCharges.mPackageTypeID = Convert.ToInt32(dr["PackageTypeID"].ToString());
                        ObjCVarQuotationCharges.mCostQuantity = Convert.ToDecimal(dr["CostQuantity"].ToString());
                        ObjCVarQuotationCharges.mCostPrice = Convert.ToDecimal(dr["CostPrice"].ToString());
                        ObjCVarQuotationCharges.mCostAmount = Convert.ToDecimal(dr["CostAmount"].ToString());
                        ObjCVarQuotationCharges.mCostCurrencyID = Convert.ToInt32(dr["CostCurrencyID"].ToString());
                        ObjCVarQuotationCharges.mCostExchangeRate = Convert.ToDecimal(dr["CostExchangeRate"].ToString());
                        ObjCVarQuotationCharges.mSaleQuantity = Convert.ToDecimal(dr["SaleQuantity"].ToString());
                        ObjCVarQuotationCharges.mSalePrice = Convert.ToDecimal(dr["SalePrice"].ToString());
                        ObjCVarQuotationCharges.mSaleAmount = Convert.ToDecimal(dr["SaleAmount"].ToString());
                        ObjCVarQuotationCharges.mSaleCurrencyID = Convert.ToInt32(dr["SaleCurrencyID"].ToString());
                        ObjCVarQuotationCharges.mSaleExchangeRate = Convert.ToDecimal(dr["SaleExchangeRate"].ToString());
                        ObjCVarQuotationCharges.mOperationPartnerTypeID = Convert.ToInt32(dr["OperationPartnerTypeID"].ToString());
                        ObjCVarQuotationCharges.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarQuotationCharges.mAgentID = Convert.ToInt32(dr["AgentID"].ToString());
                        ObjCVarQuotationCharges.mShippingAgentID = Convert.ToInt32(dr["ShippingAgentID"].ToString());
                        ObjCVarQuotationCharges.mCustomsClearanceAgentID = Convert.ToInt32(dr["CustomsClearanceAgentID"].ToString());
                        ObjCVarQuotationCharges.mShippingLineID = Convert.ToInt32(dr["ShippingLineID"].ToString());
                        ObjCVarQuotationCharges.mAirlineID = Convert.ToInt32(dr["AirlineID"].ToString());
                        ObjCVarQuotationCharges.mTruckerID = Convert.ToInt32(dr["TruckerID"].ToString());
                        ObjCVarQuotationCharges.mSupplierID = Convert.ToInt32(dr["SupplierID"].ToString());
                        ObjCVarQuotationCharges.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarQuotationCharges.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarQuotationCharges.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarQuotationCharges.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarQuotationCharges.mPricingID = Convert.ToInt64(dr["PricingID"].ToString());
                        ObjCVarQuotationCharges.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarQuotationCharges.mViewOrder = Convert.ToInt32(dr["ViewOrder"].ToString());
                        ObjCVarQuotationCharges.mSupplierSiteID = Convert.ToInt32(dr["SupplierSiteID"].ToString());
                        ObjCVarQuotationCharges.mAdditionalAmount = Convert.ToDecimal(dr["AdditionalAmount"].ToString());
                        ObjCVarQuotationCharges.mPOrC = Convert.ToInt32(dr["POrC"].ToString());
                        lstCVarQuotationCharges.Add(ObjCVarQuotationCharges);
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
            lstCVarQuotationCharges.Clear();

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
                Com.CommandText = "[dbo].GetListPagingQuotationCharges";
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
                        CVarQuotationCharges ObjCVarQuotationCharges = new CVarQuotationCharges();
                        ObjCVarQuotationCharges.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarQuotationCharges.mQuotationRouteID = Convert.ToInt64(dr["QuotationRouteID"].ToString());
                        ObjCVarQuotationCharges.mChargeTypeID = Convert.ToInt32(dr["ChargeTypeID"].ToString());
                        ObjCVarQuotationCharges.mMeasurementID = Convert.ToInt32(dr["MeasurementID"].ToString());
                        ObjCVarQuotationCharges.mContainerTypeID = Convert.ToInt32(dr["ContainerTypeID"].ToString());
                        ObjCVarQuotationCharges.mDemurrageDays = Convert.ToInt32(dr["DemurrageDays"].ToString());
                        ObjCVarQuotationCharges.mPackageTypeID = Convert.ToInt32(dr["PackageTypeID"].ToString());
                        ObjCVarQuotationCharges.mCostQuantity = Convert.ToDecimal(dr["CostQuantity"].ToString());
                        ObjCVarQuotationCharges.mCostPrice = Convert.ToDecimal(dr["CostPrice"].ToString());
                        ObjCVarQuotationCharges.mCostAmount = Convert.ToDecimal(dr["CostAmount"].ToString());
                        ObjCVarQuotationCharges.mCostCurrencyID = Convert.ToInt32(dr["CostCurrencyID"].ToString());
                        ObjCVarQuotationCharges.mCostExchangeRate = Convert.ToDecimal(dr["CostExchangeRate"].ToString());
                        ObjCVarQuotationCharges.mSaleQuantity = Convert.ToDecimal(dr["SaleQuantity"].ToString());
                        ObjCVarQuotationCharges.mSalePrice = Convert.ToDecimal(dr["SalePrice"].ToString());
                        ObjCVarQuotationCharges.mSaleAmount = Convert.ToDecimal(dr["SaleAmount"].ToString());
                        ObjCVarQuotationCharges.mSaleCurrencyID = Convert.ToInt32(dr["SaleCurrencyID"].ToString());
                        ObjCVarQuotationCharges.mSaleExchangeRate = Convert.ToDecimal(dr["SaleExchangeRate"].ToString());
                        ObjCVarQuotationCharges.mOperationPartnerTypeID = Convert.ToInt32(dr["OperationPartnerTypeID"].ToString());
                        ObjCVarQuotationCharges.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarQuotationCharges.mAgentID = Convert.ToInt32(dr["AgentID"].ToString());
                        ObjCVarQuotationCharges.mShippingAgentID = Convert.ToInt32(dr["ShippingAgentID"].ToString());
                        ObjCVarQuotationCharges.mCustomsClearanceAgentID = Convert.ToInt32(dr["CustomsClearanceAgentID"].ToString());
                        ObjCVarQuotationCharges.mShippingLineID = Convert.ToInt32(dr["ShippingLineID"].ToString());
                        ObjCVarQuotationCharges.mAirlineID = Convert.ToInt32(dr["AirlineID"].ToString());
                        ObjCVarQuotationCharges.mTruckerID = Convert.ToInt32(dr["TruckerID"].ToString());
                        ObjCVarQuotationCharges.mSupplierID = Convert.ToInt32(dr["SupplierID"].ToString());
                        ObjCVarQuotationCharges.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarQuotationCharges.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarQuotationCharges.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarQuotationCharges.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarQuotationCharges.mPricingID = Convert.ToInt64(dr["PricingID"].ToString());
                        ObjCVarQuotationCharges.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarQuotationCharges.mViewOrder = Convert.ToInt32(dr["ViewOrder"].ToString());
                        ObjCVarQuotationCharges.mSupplierSiteID = Convert.ToInt32(dr["SupplierSiteID"].ToString());
                        ObjCVarQuotationCharges.mAdditionalAmount = Convert.ToDecimal(dr["AdditionalAmount"].ToString());
                        ObjCVarQuotationCharges.mPOrC = Convert.ToInt32(dr["POrC"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarQuotationCharges.Add(ObjCVarQuotationCharges);
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
                    Com.CommandText = "[dbo].DeleteListQuotationCharges";
                else
                    Com.CommandText = "[dbo].UpdateListQuotationCharges";
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
        public Exception DeleteItem(List<CPKQuotationCharges> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemQuotationCharges";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt));
                foreach (CPKQuotationCharges ObjCPKQuotationCharges in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt64(ObjCPKQuotationCharges.ID);
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
        public Exception SaveMethod(List<CVarQuotationCharges> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@QuotationRouteID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@ChargeTypeID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@MeasurementID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ContainerTypeID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@DemurrageDays", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@PackageTypeID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CostQuantity", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@CostPrice", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@CostAmount", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@CostCurrencyID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CostExchangeRate", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@SaleQuantity", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@SalePrice", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@SaleAmount", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@SaleCurrencyID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@SaleExchangeRate", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@OperationPartnerTypeID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@AgentID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ShippingAgentID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CustomsClearanceAgentID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ShippingLineID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@AirlineID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@TruckerID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@SupplierID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ModificatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ModificationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@PricingID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@ViewOrder", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@SupplierSiteID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@AdditionalAmount", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@POrC", SqlDbType.Int));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarQuotationCharges ObjCVarQuotationCharges in SaveList)
                {
                    if (ObjCVarQuotationCharges.mIsChanges == true)
                    {
                        if (ObjCVarQuotationCharges.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemQuotationCharges";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarQuotationCharges.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemQuotationCharges";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarQuotationCharges.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarQuotationCharges.ID;
                        }
                        Com.Parameters["@QuotationRouteID"].Value = ObjCVarQuotationCharges.QuotationRouteID;
                        Com.Parameters["@ChargeTypeID"].Value = ObjCVarQuotationCharges.ChargeTypeID;
                        Com.Parameters["@MeasurementID"].Value = ObjCVarQuotationCharges.MeasurementID;
                        Com.Parameters["@ContainerTypeID"].Value = ObjCVarQuotationCharges.ContainerTypeID;
                        Com.Parameters["@DemurrageDays"].Value = ObjCVarQuotationCharges.DemurrageDays;
                        Com.Parameters["@PackageTypeID"].Value = ObjCVarQuotationCharges.PackageTypeID;
                        Com.Parameters["@CostQuantity"].Value = ObjCVarQuotationCharges.CostQuantity;
                        Com.Parameters["@CostPrice"].Value = ObjCVarQuotationCharges.CostPrice;
                        Com.Parameters["@CostAmount"].Value = ObjCVarQuotationCharges.CostAmount;
                        Com.Parameters["@CostCurrencyID"].Value = ObjCVarQuotationCharges.CostCurrencyID;
                        Com.Parameters["@CostExchangeRate"].Value = ObjCVarQuotationCharges.CostExchangeRate;
                        Com.Parameters["@SaleQuantity"].Value = ObjCVarQuotationCharges.SaleQuantity;
                        Com.Parameters["@SalePrice"].Value = ObjCVarQuotationCharges.SalePrice;
                        Com.Parameters["@SaleAmount"].Value = ObjCVarQuotationCharges.SaleAmount;
                        Com.Parameters["@SaleCurrencyID"].Value = ObjCVarQuotationCharges.SaleCurrencyID;
                        Com.Parameters["@SaleExchangeRate"].Value = ObjCVarQuotationCharges.SaleExchangeRate;
                        Com.Parameters["@OperationPartnerTypeID"].Value = ObjCVarQuotationCharges.OperationPartnerTypeID;
                        Com.Parameters["@CustomerID"].Value = ObjCVarQuotationCharges.CustomerID;
                        Com.Parameters["@AgentID"].Value = ObjCVarQuotationCharges.AgentID;
                        Com.Parameters["@ShippingAgentID"].Value = ObjCVarQuotationCharges.ShippingAgentID;
                        Com.Parameters["@CustomsClearanceAgentID"].Value = ObjCVarQuotationCharges.CustomsClearanceAgentID;
                        Com.Parameters["@ShippingLineID"].Value = ObjCVarQuotationCharges.ShippingLineID;
                        Com.Parameters["@AirlineID"].Value = ObjCVarQuotationCharges.AirlineID;
                        Com.Parameters["@TruckerID"].Value = ObjCVarQuotationCharges.TruckerID;
                        Com.Parameters["@SupplierID"].Value = ObjCVarQuotationCharges.SupplierID;
                        Com.Parameters["@CreatorUserID"].Value = ObjCVarQuotationCharges.CreatorUserID;
                        Com.Parameters["@CreationDate"].Value = ObjCVarQuotationCharges.CreationDate;
                        Com.Parameters["@ModificatorUserID"].Value = ObjCVarQuotationCharges.ModificatorUserID;
                        Com.Parameters["@ModificationDate"].Value = ObjCVarQuotationCharges.ModificationDate;
                        Com.Parameters["@PricingID"].Value = ObjCVarQuotationCharges.PricingID;
                        Com.Parameters["@Notes"].Value = ObjCVarQuotationCharges.Notes;
                        Com.Parameters["@ViewOrder"].Value = ObjCVarQuotationCharges.ViewOrder;
                        Com.Parameters["@SupplierSiteID"].Value = ObjCVarQuotationCharges.SupplierSiteID;
                        Com.Parameters["@AdditionalAmount"].Value = ObjCVarQuotationCharges.AdditionalAmount;
                        Com.Parameters["@POrC"].Value = ObjCVarQuotationCharges.POrC;
                        EndTrans(Com, Con);
                        if (ObjCVarQuotationCharges.ID == 0)
                        {
                            ObjCVarQuotationCharges.ID = Convert.ToInt64(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarQuotationCharges.mIsChanges = false;
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
