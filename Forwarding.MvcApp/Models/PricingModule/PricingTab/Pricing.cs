﻿using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.PricingModule.PricingTab
{
    [Serializable]
    public class CPKPricing
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
    public partial class CVarPricing : CPKPricing
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mPricingTypeID;
        internal Int32 mShippingLineID;
        internal Int32 mAirlineID;
        internal Int32 mTruckerID;
        internal Int32 mCCAID;
        internal Int32 mPOLCountryID;
        internal Int32 mPOLID;
        internal Int32 mPODCountryID;
        internal Int32 mPODID;
        internal Int32 mEquipmentID;
        internal Int32 mCommodityID;
        internal Int32 mTransitTime;
        internal Int32 mFrequency;
        internal String mFrequencyNotes;
        internal DateTime mValidFrom;
        internal DateTime mValidTo;
        internal Int32 mCurrencyID;
        internal Decimal mExchangeRate;
        internal String mNotes;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Boolean mIsPricingRequest;
        internal Int32 mCustomerID;
        internal Int32 mPackageTypeID;
        internal Int32 mAgentID;
        internal String mCustomerReference;
        #endregion

        #region "Methods"
        public Int32 PricingTypeID
        {
            get { return mPricingTypeID; }
            set { mIsChanges = true; mPricingTypeID = value; }
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
        public Int32 CCAID
        {
            get { return mCCAID; }
            set { mIsChanges = true; mCCAID = value; }
        }
        public Int32 POLCountryID
        {
            get { return mPOLCountryID; }
            set { mIsChanges = true; mPOLCountryID = value; }
        }
        public Int32 POLID
        {
            get { return mPOLID; }
            set { mIsChanges = true; mPOLID = value; }
        }
        public Int32 PODCountryID
        {
            get { return mPODCountryID; }
            set { mIsChanges = true; mPODCountryID = value; }
        }
        public Int32 PODID
        {
            get { return mPODID; }
            set { mIsChanges = true; mPODID = value; }
        }
        public Int32 EquipmentID
        {
            get { return mEquipmentID; }
            set { mIsChanges = true; mEquipmentID = value; }
        }
        public Int32 CommodityID
        {
            get { return mCommodityID; }
            set { mIsChanges = true; mCommodityID = value; }
        }
        public Int32 TransitTime
        {
            get { return mTransitTime; }
            set { mIsChanges = true; mTransitTime = value; }
        }
        public Int32 Frequency
        {
            get { return mFrequency; }
            set { mIsChanges = true; mFrequency = value; }
        }
        public String FrequencyNotes
        {
            get { return mFrequencyNotes; }
            set { mIsChanges = true; mFrequencyNotes = value; }
        }
        public DateTime ValidFrom
        {
            get { return mValidFrom; }
            set { mIsChanges = true; mValidFrom = value; }
        }
        public DateTime ValidTo
        {
            get { return mValidTo; }
            set { mIsChanges = true; mValidTo = value; }
        }
        public Int32 CurrencyID
        {
            get { return mCurrencyID; }
            set { mIsChanges = true; mCurrencyID = value; }
        }
        public Decimal ExchangeRate
        {
            get { return mExchangeRate; }
            set { mIsChanges = true; mExchangeRate = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mIsChanges = true; mNotes = value; }
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
        public Boolean IsPricingRequest
        {
            get { return mIsPricingRequest; }
            set { mIsChanges = true; mIsPricingRequest = value; }
        }
        public Int32 CustomerID
        {
            get { return mCustomerID; }
            set { mIsChanges = true; mCustomerID = value; }
        }
        public Int32 PackageTypeID
        {
            get { return mPackageTypeID; }
            set { mIsChanges = true; mPackageTypeID = value; }
        }
        public Int32 AgentID
        {
            get { return mAgentID; }
            set { mIsChanges = true; mAgentID = value; }
        }
        public String CustomerReference
        {
            get { return mCustomerReference; }
            set { mIsChanges = true; mCustomerReference = value; }
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

    public partial class CPricing
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
        public List<CVarPricing> lstCVarPricing = new List<CVarPricing>();
        public List<CPKPricing> lstDeletedCPKPricing = new List<CPKPricing>();
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
            lstCVarPricing.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListPricing";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemPricing";
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
                        CVarPricing ObjCVarPricing = new CVarPricing();
                        ObjCVarPricing.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarPricing.mPricingTypeID = Convert.ToInt32(dr["PricingTypeID"].ToString());
                        ObjCVarPricing.mShippingLineID = Convert.ToInt32(dr["ShippingLineID"].ToString());
                        ObjCVarPricing.mAirlineID = Convert.ToInt32(dr["AirlineID"].ToString());
                        ObjCVarPricing.mTruckerID = Convert.ToInt32(dr["TruckerID"].ToString());
                        ObjCVarPricing.mCCAID = Convert.ToInt32(dr["CCAID"].ToString());
                        ObjCVarPricing.mPOLCountryID = Convert.ToInt32(dr["POLCountryID"].ToString());
                        ObjCVarPricing.mPOLID = Convert.ToInt32(dr["POLID"].ToString());
                        ObjCVarPricing.mPODCountryID = Convert.ToInt32(dr["PODCountryID"].ToString());
                        ObjCVarPricing.mPODID = Convert.ToInt32(dr["PODID"].ToString());
                        ObjCVarPricing.mEquipmentID = Convert.ToInt32(dr["EquipmentID"].ToString());
                        ObjCVarPricing.mCommodityID = Convert.ToInt32(dr["CommodityID"].ToString());
                        ObjCVarPricing.mTransitTime = Convert.ToInt32(dr["TransitTime"].ToString());
                        ObjCVarPricing.mFrequency = Convert.ToInt32(dr["Frequency"].ToString());
                        ObjCVarPricing.mFrequencyNotes = Convert.ToString(dr["FrequencyNotes"].ToString());
                        ObjCVarPricing.mValidFrom = Convert.ToDateTime(dr["ValidFrom"].ToString());
                        ObjCVarPricing.mValidTo = Convert.ToDateTime(dr["ValidTo"].ToString());
                        ObjCVarPricing.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarPricing.mExchangeRate = Convert.ToDecimal(dr["ExchangeRate"].ToString());
                        ObjCVarPricing.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarPricing.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarPricing.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarPricing.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarPricing.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarPricing.mIsPricingRequest = Convert.ToBoolean(dr["IsPricingRequest"].ToString());
                        ObjCVarPricing.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarPricing.mPackageTypeID = Convert.ToInt32(dr["PackageTypeID"].ToString());
                        ObjCVarPricing.mAgentID = Convert.ToInt32(dr["AgentID"].ToString());
                        ObjCVarPricing.mCustomerReference = Convert.ToString(dr["CustomerReference"].ToString());
                        lstCVarPricing.Add(ObjCVarPricing);
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
            lstCVarPricing.Clear();

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
                Com.CommandText = "[dbo].GetListPagingPricing";
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
                        CVarPricing ObjCVarPricing = new CVarPricing();
                        ObjCVarPricing.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarPricing.mPricingTypeID = Convert.ToInt32(dr["PricingTypeID"].ToString());
                        ObjCVarPricing.mShippingLineID = Convert.ToInt32(dr["ShippingLineID"].ToString());
                        ObjCVarPricing.mAirlineID = Convert.ToInt32(dr["AirlineID"].ToString());
                        ObjCVarPricing.mTruckerID = Convert.ToInt32(dr["TruckerID"].ToString());
                        ObjCVarPricing.mCCAID = Convert.ToInt32(dr["CCAID"].ToString());
                        ObjCVarPricing.mPOLCountryID = Convert.ToInt32(dr["POLCountryID"].ToString());
                        ObjCVarPricing.mPOLID = Convert.ToInt32(dr["POLID"].ToString());
                        ObjCVarPricing.mPODCountryID = Convert.ToInt32(dr["PODCountryID"].ToString());
                        ObjCVarPricing.mPODID = Convert.ToInt32(dr["PODID"].ToString());
                        ObjCVarPricing.mEquipmentID = Convert.ToInt32(dr["EquipmentID"].ToString());
                        ObjCVarPricing.mCommodityID = Convert.ToInt32(dr["CommodityID"].ToString());
                        ObjCVarPricing.mTransitTime = Convert.ToInt32(dr["TransitTime"].ToString());
                        ObjCVarPricing.mFrequency = Convert.ToInt32(dr["Frequency"].ToString());
                        ObjCVarPricing.mFrequencyNotes = Convert.ToString(dr["FrequencyNotes"].ToString());
                        ObjCVarPricing.mValidFrom = Convert.ToDateTime(dr["ValidFrom"].ToString());
                        ObjCVarPricing.mValidTo = Convert.ToDateTime(dr["ValidTo"].ToString());
                        ObjCVarPricing.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarPricing.mExchangeRate = Convert.ToDecimal(dr["ExchangeRate"].ToString());
                        ObjCVarPricing.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarPricing.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarPricing.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarPricing.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarPricing.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarPricing.mIsPricingRequest = Convert.ToBoolean(dr["IsPricingRequest"].ToString());
                        ObjCVarPricing.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarPricing.mPackageTypeID = Convert.ToInt32(dr["PackageTypeID"].ToString());
                        ObjCVarPricing.mAgentID = Convert.ToInt32(dr["AgentID"].ToString());
                        ObjCVarPricing.mCustomerReference = Convert.ToString(dr["CustomerReference"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarPricing.Add(ObjCVarPricing);
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
                    Com.CommandText = "[dbo].DeleteListPricing";
                else
                    Com.CommandText = "[dbo].UpdateListPricing";
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
        public Exception DeleteItem(List<CPKPricing> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemPricing";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt));
                foreach (CPKPricing ObjCPKPricing in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt64(ObjCPKPricing.ID);
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
        public Exception SaveMethod(List<CVarPricing> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@PricingTypeID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ShippingLineID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@AirlineID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@TruckerID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CCAID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@POLCountryID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@POLID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@PODCountryID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@PODID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@EquipmentID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CommodityID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@TransitTime", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@Frequency", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@FrequencyNotes", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@ValidFrom", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ValidTo", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@CurrencyID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ExchangeRate", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@CreatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ModificatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ModificationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@IsPricingRequest", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@PackageTypeID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@AgentID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CustomerReference", SqlDbType.NVarChar));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarPricing ObjCVarPricing in SaveList)
                {
                    if (ObjCVarPricing.mIsChanges == true)
                    {
                        if (ObjCVarPricing.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemPricing";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarPricing.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemPricing";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarPricing.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarPricing.ID;
                        }
                        Com.Parameters["@PricingTypeID"].Value = ObjCVarPricing.PricingTypeID;
                        Com.Parameters["@ShippingLineID"].Value = ObjCVarPricing.ShippingLineID;
                        Com.Parameters["@AirlineID"].Value = ObjCVarPricing.AirlineID;
                        Com.Parameters["@TruckerID"].Value = ObjCVarPricing.TruckerID;
                        Com.Parameters["@CCAID"].Value = ObjCVarPricing.CCAID;
                        Com.Parameters["@POLCountryID"].Value = ObjCVarPricing.POLCountryID;
                        Com.Parameters["@POLID"].Value = ObjCVarPricing.POLID;
                        Com.Parameters["@PODCountryID"].Value = ObjCVarPricing.PODCountryID;
                        Com.Parameters["@PODID"].Value = ObjCVarPricing.PODID;
                        Com.Parameters["@EquipmentID"].Value = ObjCVarPricing.EquipmentID;
                        Com.Parameters["@CommodityID"].Value = ObjCVarPricing.CommodityID;
                        Com.Parameters["@TransitTime"].Value = ObjCVarPricing.TransitTime;
                        Com.Parameters["@Frequency"].Value = ObjCVarPricing.Frequency;
                        Com.Parameters["@FrequencyNotes"].Value = ObjCVarPricing.FrequencyNotes;
                        Com.Parameters["@ValidFrom"].Value = ObjCVarPricing.ValidFrom;
                        Com.Parameters["@ValidTo"].Value = ObjCVarPricing.ValidTo;
                        Com.Parameters["@CurrencyID"].Value = ObjCVarPricing.CurrencyID;
                        Com.Parameters["@ExchangeRate"].Value = ObjCVarPricing.ExchangeRate;
                        Com.Parameters["@Notes"].Value = ObjCVarPricing.Notes;
                        Com.Parameters["@CreatorUserID"].Value = ObjCVarPricing.CreatorUserID;
                        Com.Parameters["@CreationDate"].Value = ObjCVarPricing.CreationDate;
                        Com.Parameters["@ModificatorUserID"].Value = ObjCVarPricing.ModificatorUserID;
                        Com.Parameters["@ModificationDate"].Value = ObjCVarPricing.ModificationDate;
                        Com.Parameters["@IsPricingRequest"].Value = ObjCVarPricing.IsPricingRequest;
                        Com.Parameters["@CustomerID"].Value = ObjCVarPricing.CustomerID;
                        Com.Parameters["@PackageTypeID"].Value = ObjCVarPricing.PackageTypeID;
                        Com.Parameters["@AgentID"].Value = ObjCVarPricing.AgentID;
                        Com.Parameters["@CustomerReference"].Value = ObjCVarPricing.CustomerReference;
                        EndTrans(Com, Con);
                        if (ObjCVarPricing.ID == 0)
                        {
                            ObjCVarPricing.ID = Convert.ToInt64(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarPricing.mIsChanges = false;
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
