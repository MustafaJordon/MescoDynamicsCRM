﻿using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.Operations.Operations.Generated
{
    [Serializable]
    public class CPKOperationPartners
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
    public partial class CVarOperationPartners : CPKOperationPartners
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mOperationID;
        internal Int32 mOperationPartnerTypeID;
        internal Int32 mCustomerID;
        internal Int32 mAgentID;
        internal Int32 mShippingAgentID;
        internal Int32 mCustomsClearanceAgentID;
        internal Int32 mShippingLineID;
        internal Int32 mAirlineID;
        internal Int32 mTruckerID;
        internal Int32 mSupplierID;
        internal Int32 mCustodyID;
        internal Int64 mContactID;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Boolean mIsOperationClient;
        #endregion

        #region "Methods"
        public Int64 OperationID
        {
            get { return mOperationID; }
            set { mIsChanges = true; mOperationID = value; }
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
        public Int32 CustodyID
        {
            get { return mCustodyID; }
            set { mIsChanges = true; mCustodyID = value; }
        }
        public Int64 ContactID
        {
            get { return mContactID; }
            set { mIsChanges = true; mContactID = value; }
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
        public Boolean IsOperationClient
        {
            get { return mIsOperationClient; }
            set { mIsChanges = true; mIsOperationClient = value; }
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

    public partial class COperationPartners
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
        public List<CVarOperationPartners> lstCVarOperationPartners = new List<CVarOperationPartners>();
        public List<CPKOperationPartners> lstDeletedCPKOperationPartners = new List<CPKOperationPartners>();
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
            lstCVarOperationPartners.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListOperationPartners";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemOperationPartners";
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
                        CVarOperationPartners ObjCVarOperationPartners = new CVarOperationPartners();
                        ObjCVarOperationPartners.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarOperationPartners.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarOperationPartners.mOperationPartnerTypeID = Convert.ToInt32(dr["OperationPartnerTypeID"].ToString());
                        ObjCVarOperationPartners.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarOperationPartners.mAgentID = Convert.ToInt32(dr["AgentID"].ToString());
                        ObjCVarOperationPartners.mShippingAgentID = Convert.ToInt32(dr["ShippingAgentID"].ToString());
                        ObjCVarOperationPartners.mCustomsClearanceAgentID = Convert.ToInt32(dr["CustomsClearanceAgentID"].ToString());
                        ObjCVarOperationPartners.mShippingLineID = Convert.ToInt32(dr["ShippingLineID"].ToString());
                        ObjCVarOperationPartners.mAirlineID = Convert.ToInt32(dr["AirlineID"].ToString());
                        ObjCVarOperationPartners.mTruckerID = Convert.ToInt32(dr["TruckerID"].ToString());
                        ObjCVarOperationPartners.mSupplierID = Convert.ToInt32(dr["SupplierID"].ToString());
                        ObjCVarOperationPartners.mCustodyID = Convert.ToInt32(dr["CustodyID"].ToString());
                        ObjCVarOperationPartners.mContactID = Convert.ToInt64(dr["ContactID"].ToString());
                        ObjCVarOperationPartners.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarOperationPartners.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarOperationPartners.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarOperationPartners.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarOperationPartners.mIsOperationClient = Convert.ToBoolean(dr["IsOperationClient"].ToString());
                        lstCVarOperationPartners.Add(ObjCVarOperationPartners);
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
            lstCVarOperationPartners.Clear();

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
                Com.CommandText = "[dbo].GetListPagingOperationPartners";
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
                        CVarOperationPartners ObjCVarOperationPartners = new CVarOperationPartners();
                        ObjCVarOperationPartners.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarOperationPartners.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarOperationPartners.mOperationPartnerTypeID = Convert.ToInt32(dr["OperationPartnerTypeID"].ToString());
                        ObjCVarOperationPartners.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarOperationPartners.mAgentID = Convert.ToInt32(dr["AgentID"].ToString());
                        ObjCVarOperationPartners.mShippingAgentID = Convert.ToInt32(dr["ShippingAgentID"].ToString());
                        ObjCVarOperationPartners.mCustomsClearanceAgentID = Convert.ToInt32(dr["CustomsClearanceAgentID"].ToString());
                        ObjCVarOperationPartners.mShippingLineID = Convert.ToInt32(dr["ShippingLineID"].ToString());
                        ObjCVarOperationPartners.mAirlineID = Convert.ToInt32(dr["AirlineID"].ToString());
                        ObjCVarOperationPartners.mTruckerID = Convert.ToInt32(dr["TruckerID"].ToString());
                        ObjCVarOperationPartners.mSupplierID = Convert.ToInt32(dr["SupplierID"].ToString());
                        ObjCVarOperationPartners.mCustodyID = Convert.ToInt32(dr["CustodyID"].ToString());
                        ObjCVarOperationPartners.mContactID = Convert.ToInt64(dr["ContactID"].ToString());
                        ObjCVarOperationPartners.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarOperationPartners.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarOperationPartners.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarOperationPartners.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarOperationPartners.mIsOperationClient = Convert.ToBoolean(dr["IsOperationClient"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarOperationPartners.Add(ObjCVarOperationPartners);
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
                    Com.CommandText = "[dbo].DeleteListOperationPartners";
                else
                    Com.CommandText = "[dbo].UpdateListOperationPartners";
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
        public Exception DeleteItem(List<CPKOperationPartners> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemOperationPartners";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt));
                foreach (CPKOperationPartners ObjCPKOperationPartners in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt64(ObjCPKOperationPartners.ID);
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
        public Exception SaveMethod(List<CVarOperationPartners> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@OperationID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@OperationPartnerTypeID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@AgentID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ShippingAgentID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CustomsClearanceAgentID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ShippingLineID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@AirlineID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@TruckerID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@SupplierID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CustodyID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ContactID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@CreatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ModificatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ModificationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@IsOperationClient", SqlDbType.Bit));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarOperationPartners ObjCVarOperationPartners in SaveList)
                {
                    if (ObjCVarOperationPartners.mIsChanges == true)
                    {
                        if (ObjCVarOperationPartners.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemOperationPartners";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarOperationPartners.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemOperationPartners";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarOperationPartners.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarOperationPartners.ID;
                        }
                        Com.Parameters["@OperationID"].Value = ObjCVarOperationPartners.OperationID;
                        Com.Parameters["@OperationPartnerTypeID"].Value = ObjCVarOperationPartners.OperationPartnerTypeID;
                        Com.Parameters["@CustomerID"].Value = ObjCVarOperationPartners.CustomerID;
                        Com.Parameters["@AgentID"].Value = ObjCVarOperationPartners.AgentID;
                        Com.Parameters["@ShippingAgentID"].Value = ObjCVarOperationPartners.ShippingAgentID;
                        Com.Parameters["@CustomsClearanceAgentID"].Value = ObjCVarOperationPartners.CustomsClearanceAgentID;
                        Com.Parameters["@ShippingLineID"].Value = ObjCVarOperationPartners.ShippingLineID;
                        Com.Parameters["@AirlineID"].Value = ObjCVarOperationPartners.AirlineID;
                        Com.Parameters["@TruckerID"].Value = ObjCVarOperationPartners.TruckerID;
                        Com.Parameters["@SupplierID"].Value = ObjCVarOperationPartners.SupplierID;
                        Com.Parameters["@CustodyID"].Value = ObjCVarOperationPartners.CustodyID;
                        Com.Parameters["@ContactID"].Value = ObjCVarOperationPartners.ContactID;
                        Com.Parameters["@CreatorUserID"].Value = ObjCVarOperationPartners.CreatorUserID;
                        Com.Parameters["@CreationDate"].Value = ObjCVarOperationPartners.CreationDate;
                        Com.Parameters["@ModificatorUserID"].Value = ObjCVarOperationPartners.ModificatorUserID;
                        Com.Parameters["@ModificationDate"].Value = ObjCVarOperationPartners.ModificationDate;
                        Com.Parameters["@IsOperationClient"].Value = ObjCVarOperationPartners.IsOperationClient;
                        EndTrans(Com, Con);
                        if (ObjCVarOperationPartners.ID == 0)
                        {
                            ObjCVarOperationPartners.ID = Convert.ToInt64(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarOperationPartners.mIsChanges = false;
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
