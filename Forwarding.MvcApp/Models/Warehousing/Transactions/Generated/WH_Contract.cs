﻿using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.Warehousing.Transactions.Generated
{
    [Serializable]
    public class CPKWH_Contract
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
    public partial class CVarWH_Contract : CPKWH_Contract
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mCodeSerial;
        internal String mCode;
        internal Int32 mWarehouseID;
        internal Int32 mCustomerID;
        internal DateTime mFromDate;
        internal DateTime mToDate;
        internal Decimal mStorageLimit;
        internal Int32 mStorageUnitID;
        internal Boolean mIsByPallet;
        internal Int32 mNumberOfPallets;
        internal Int32 mCurrencyID;
        internal String mNotes;
        internal Boolean mIsFinalized;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        #endregion

        #region "Methods"
        public Int32 CodeSerial
        {
            get { return mCodeSerial; }
            set { mIsChanges = true; mCodeSerial = value; }
        }
        public String Code
        {
            get { return mCode; }
            set { mIsChanges = true; mCode = value; }
        }
        public Int32 WarehouseID
        {
            get { return mWarehouseID; }
            set { mIsChanges = true; mWarehouseID = value; }
        }
        public Int32 CustomerID
        {
            get { return mCustomerID; }
            set { mIsChanges = true; mCustomerID = value; }
        }
        public DateTime FromDate
        {
            get { return mFromDate; }
            set { mIsChanges = true; mFromDate = value; }
        }
        public DateTime ToDate
        {
            get { return mToDate; }
            set { mIsChanges = true; mToDate = value; }
        }
        public Decimal StorageLimit
        {
            get { return mStorageLimit; }
            set { mIsChanges = true; mStorageLimit = value; }
        }
        public Int32 StorageUnitID
        {
            get { return mStorageUnitID; }
            set { mIsChanges = true; mStorageUnitID = value; }
        }
        public Boolean IsByPallet
        {
            get { return mIsByPallet; }
            set { mIsChanges = true; mIsByPallet = value; }
        }
        public Int32 NumberOfPallets
        {
            get { return mNumberOfPallets; }
            set { mIsChanges = true; mNumberOfPallets = value; }
        }
        public Int32 CurrencyID
        {
            get { return mCurrencyID; }
            set { mIsChanges = true; mCurrencyID = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mIsChanges = true; mNotes = value; }
        }
        public Boolean IsFinalized
        {
            get { return mIsFinalized; }
            set { mIsChanges = true; mIsFinalized = value; }
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

    public partial class CWH_Contract
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
        public List<CVarWH_Contract> lstCVarWH_Contract = new List<CVarWH_Contract>();
        public List<CPKWH_Contract> lstDeletedCPKWH_Contract = new List<CPKWH_Contract>();
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
            lstCVarWH_Contract.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListWH_Contract";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemWH_Contract";
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
                        CVarWH_Contract ObjCVarWH_Contract = new CVarWH_Contract();
                        ObjCVarWH_Contract.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarWH_Contract.mCodeSerial = Convert.ToInt32(dr["CodeSerial"].ToString());
                        ObjCVarWH_Contract.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarWH_Contract.mWarehouseID = Convert.ToInt32(dr["WarehouseID"].ToString());
                        ObjCVarWH_Contract.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarWH_Contract.mFromDate = Convert.ToDateTime(dr["FromDate"].ToString());
                        ObjCVarWH_Contract.mToDate = Convert.ToDateTime(dr["ToDate"].ToString());
                        ObjCVarWH_Contract.mStorageLimit = Convert.ToDecimal(dr["StorageLimit"].ToString());
                        ObjCVarWH_Contract.mStorageUnitID = Convert.ToInt32(dr["StorageUnitID"].ToString());
                        ObjCVarWH_Contract.mIsByPallet = Convert.ToBoolean(dr["IsByPallet"].ToString());
                        ObjCVarWH_Contract.mNumberOfPallets = Convert.ToInt32(dr["NumberOfPallets"].ToString());
                        ObjCVarWH_Contract.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarWH_Contract.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarWH_Contract.mIsFinalized = Convert.ToBoolean(dr["IsFinalized"].ToString());
                        ObjCVarWH_Contract.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarWH_Contract.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarWH_Contract.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarWH_Contract.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        lstCVarWH_Contract.Add(ObjCVarWH_Contract);
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
            lstCVarWH_Contract.Clear();

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
                Com.CommandText = "[dbo].GetListPagingWH_Contract";
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
                        CVarWH_Contract ObjCVarWH_Contract = new CVarWH_Contract();
                        ObjCVarWH_Contract.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarWH_Contract.mCodeSerial = Convert.ToInt32(dr["CodeSerial"].ToString());
                        ObjCVarWH_Contract.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarWH_Contract.mWarehouseID = Convert.ToInt32(dr["WarehouseID"].ToString());
                        ObjCVarWH_Contract.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarWH_Contract.mFromDate = Convert.ToDateTime(dr["FromDate"].ToString());
                        ObjCVarWH_Contract.mToDate = Convert.ToDateTime(dr["ToDate"].ToString());
                        ObjCVarWH_Contract.mStorageLimit = Convert.ToDecimal(dr["StorageLimit"].ToString());
                        ObjCVarWH_Contract.mStorageUnitID = Convert.ToInt32(dr["StorageUnitID"].ToString());
                        ObjCVarWH_Contract.mIsByPallet = Convert.ToBoolean(dr["IsByPallet"].ToString());
                        ObjCVarWH_Contract.mNumberOfPallets = Convert.ToInt32(dr["NumberOfPallets"].ToString());
                        ObjCVarWH_Contract.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarWH_Contract.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarWH_Contract.mIsFinalized = Convert.ToBoolean(dr["IsFinalized"].ToString());
                        ObjCVarWH_Contract.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarWH_Contract.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarWH_Contract.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarWH_Contract.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarWH_Contract.Add(ObjCVarWH_Contract);
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
                    Com.CommandText = "[dbo].DeleteListWH_Contract";
                else
                    Com.CommandText = "[dbo].UpdateListWH_Contract";
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
        public Exception DeleteItem(List<CPKWH_Contract> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemWH_Contract";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                foreach (CPKWH_Contract ObjCPKWH_Contract in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt32(ObjCPKWH_Contract.ID);
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
        public Exception SaveMethod(List<CVarWH_Contract> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@CodeSerial", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@Code", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@WarehouseID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@StorageLimit", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@StorageUnitID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@IsByPallet", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@NumberOfPallets", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CurrencyID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@IsFinalized", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@CreatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ModificatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ModificationDate", SqlDbType.DateTime));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarWH_Contract ObjCVarWH_Contract in SaveList)
                {
                    if (ObjCVarWH_Contract.mIsChanges == true)
                    {
                        if (ObjCVarWH_Contract.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemWH_Contract";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarWH_Contract.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemWH_Contract";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarWH_Contract.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarWH_Contract.ID;
                        }
                        Com.Parameters["@CodeSerial"].Value = ObjCVarWH_Contract.CodeSerial;
                        Com.Parameters["@Code"].Value = ObjCVarWH_Contract.Code;
                        Com.Parameters["@WarehouseID"].Value = ObjCVarWH_Contract.WarehouseID;
                        Com.Parameters["@CustomerID"].Value = ObjCVarWH_Contract.CustomerID;
                        Com.Parameters["@FromDate"].Value = ObjCVarWH_Contract.FromDate;
                        Com.Parameters["@ToDate"].Value = ObjCVarWH_Contract.ToDate;
                        Com.Parameters["@StorageLimit"].Value = ObjCVarWH_Contract.StorageLimit;
                        Com.Parameters["@StorageUnitID"].Value = ObjCVarWH_Contract.StorageUnitID;
                        Com.Parameters["@IsByPallet"].Value = ObjCVarWH_Contract.IsByPallet;
                        Com.Parameters["@NumberOfPallets"].Value = ObjCVarWH_Contract.NumberOfPallets;
                        Com.Parameters["@CurrencyID"].Value = ObjCVarWH_Contract.CurrencyID;
                        Com.Parameters["@Notes"].Value = ObjCVarWH_Contract.Notes;
                        Com.Parameters["@IsFinalized"].Value = ObjCVarWH_Contract.IsFinalized;
                        Com.Parameters["@CreatorUserID"].Value = ObjCVarWH_Contract.CreatorUserID;
                        Com.Parameters["@CreationDate"].Value = ObjCVarWH_Contract.CreationDate;
                        Com.Parameters["@ModificatorUserID"].Value = ObjCVarWH_Contract.ModificatorUserID;
                        Com.Parameters["@ModificationDate"].Value = ObjCVarWH_Contract.ModificationDate;
                        EndTrans(Com, Con);
                        if (ObjCVarWH_Contract.ID == 0)
                        {
                            ObjCVarWH_Contract.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarWH_Contract.mIsChanges = false;
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
