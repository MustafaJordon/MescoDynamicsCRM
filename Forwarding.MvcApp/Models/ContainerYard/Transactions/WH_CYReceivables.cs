﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.ContainerYard.Transactions
{
    [Serializable]
    public class CPKWH_CYReceivables
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
    public partial class CVarWH_CYReceivables : CPKWH_CYReceivables
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mChargeTypeID;
        internal Decimal mQuantity;
        internal Decimal mSalePrice;
        internal Decimal mSaleAmount;
        internal Int32 mWH_CYInvoicesID;
        internal String mNotes;
        internal Boolean mIsDeleted;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal DateTime mIssueDate;
        internal Decimal mTaxAmount;
        internal Int64 mHouseBillID;
        #endregion

        #region "Methods"
        public Int32 ChargeTypeID
        {
            get { return mChargeTypeID; }
            set { mIsChanges = true; mChargeTypeID = value; }
        }
        public Decimal Quantity
        {
            get { return mQuantity; }
            set { mIsChanges = true; mQuantity = value; }
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
        public Int32 WH_CYInvoicesID
        {
            get { return mWH_CYInvoicesID; }
            set { mIsChanges = true; mWH_CYInvoicesID = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mIsChanges = true; mNotes = value; }
        }
        public Boolean IsDeleted
        {
            get { return mIsDeleted; }
            set { mIsChanges = true; mIsDeleted = value; }
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
        public DateTime IssueDate
        {
            get { return mIssueDate; }
            set { mIsChanges = true; mIssueDate = value; }
        }
        public Decimal TaxAmount
        {
            get { return mTaxAmount; }
            set { mIsChanges = true; mTaxAmount = value; }
        }
        public Int64 HouseBillID
        {
            get { return mHouseBillID; }
            set { mIsChanges = true; mHouseBillID = value; }
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

    public partial class CWH_CYReceivables
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
        public List<CVarWH_CYReceivables> lstCVarWH_CYReceivables = new List<CVarWH_CYReceivables>();
        public List<CPKWH_CYReceivables> lstDeletedCPKWH_CYReceivables = new List<CPKWH_CYReceivables>();
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
            lstCVarWH_CYReceivables.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListWH_CYReceivables";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemWH_CYReceivables";
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
                        CVarWH_CYReceivables ObjCVarWH_CYReceivables = new CVarWH_CYReceivables();
                        ObjCVarWH_CYReceivables.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarWH_CYReceivables.mChargeTypeID = Convert.ToInt32(dr["ChargeTypeID"].ToString());
                        ObjCVarWH_CYReceivables.mQuantity = Convert.ToDecimal(dr["Quantity"].ToString());
                        ObjCVarWH_CYReceivables.mSalePrice = Convert.ToDecimal(dr["SalePrice"].ToString());
                        ObjCVarWH_CYReceivables.mSaleAmount = Convert.ToDecimal(dr["SaleAmount"].ToString());
                        ObjCVarWH_CYReceivables.mWH_CYInvoicesID = Convert.ToInt32(dr["WH_CYInvoicesID"].ToString());
                        ObjCVarWH_CYReceivables.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarWH_CYReceivables.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarWH_CYReceivables.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarWH_CYReceivables.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarWH_CYReceivables.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarWH_CYReceivables.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarWH_CYReceivables.mIssueDate = Convert.ToDateTime(dr["IssueDate"].ToString());
                        ObjCVarWH_CYReceivables.mTaxAmount = Convert.ToDecimal(dr["TaxAmount"].ToString());
                        ObjCVarWH_CYReceivables.mHouseBillID = Convert.ToInt64(dr["HouseBillID"].ToString());
                        lstCVarWH_CYReceivables.Add(ObjCVarWH_CYReceivables);
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
            lstCVarWH_CYReceivables.Clear();

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
                Com.CommandText = "[dbo].GetListPagingWH_CYReceivables";
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
                        CVarWH_CYReceivables ObjCVarWH_CYReceivables = new CVarWH_CYReceivables();
                        ObjCVarWH_CYReceivables.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarWH_CYReceivables.mChargeTypeID = Convert.ToInt32(dr["ChargeTypeID"].ToString());
                        ObjCVarWH_CYReceivables.mQuantity = Convert.ToDecimal(dr["Quantity"].ToString());
                        ObjCVarWH_CYReceivables.mSalePrice = Convert.ToDecimal(dr["SalePrice"].ToString());
                        ObjCVarWH_CYReceivables.mSaleAmount = Convert.ToDecimal(dr["SaleAmount"].ToString());
                        ObjCVarWH_CYReceivables.mWH_CYInvoicesID = Convert.ToInt32(dr["WH_CYInvoicesID"].ToString());
                        ObjCVarWH_CYReceivables.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarWH_CYReceivables.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarWH_CYReceivables.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarWH_CYReceivables.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarWH_CYReceivables.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarWH_CYReceivables.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarWH_CYReceivables.mIssueDate = Convert.ToDateTime(dr["IssueDate"].ToString());
                        ObjCVarWH_CYReceivables.mTaxAmount = Convert.ToDecimal(dr["TaxAmount"].ToString());
                        ObjCVarWH_CYReceivables.mHouseBillID = Convert.ToInt64(dr["HouseBillID"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarWH_CYReceivables.Add(ObjCVarWH_CYReceivables);
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
                    Com.CommandText = "[dbo].DeleteListWH_CYReceivables";
                else
                    Com.CommandText = "[dbo].UpdateListWH_CYReceivables";
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
        public Exception DeleteItem(List<CPKWH_CYReceivables> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemWH_CYReceivables";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt));
                foreach (CPKWH_CYReceivables ObjCPKWH_CYReceivables in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt64(ObjCPKWH_CYReceivables.ID);
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
        public Exception SaveMethod(List<CVarWH_CYReceivables> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@ChargeTypeID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@SalePrice", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@SaleAmount", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@WH_CYInvoicesID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@IsDeleted", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@CreatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ModificatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ModificationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@IssueDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@TaxAmount", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@HouseBillID", SqlDbType.BigInt));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarWH_CYReceivables ObjCVarWH_CYReceivables in SaveList)
                {
                    if (ObjCVarWH_CYReceivables.mIsChanges == true)
                    {
                        if (ObjCVarWH_CYReceivables.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemWH_CYReceivables";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarWH_CYReceivables.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemWH_CYReceivables";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarWH_CYReceivables.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarWH_CYReceivables.ID;
                        }
                        Com.Parameters["@ChargeTypeID"].Value = ObjCVarWH_CYReceivables.ChargeTypeID;
                        Com.Parameters["@Quantity"].Value = ObjCVarWH_CYReceivables.Quantity;
                        Com.Parameters["@SalePrice"].Value = ObjCVarWH_CYReceivables.SalePrice;
                        Com.Parameters["@SaleAmount"].Value = ObjCVarWH_CYReceivables.SaleAmount;
                        Com.Parameters["@WH_CYInvoicesID"].Value = ObjCVarWH_CYReceivables.WH_CYInvoicesID;
                        Com.Parameters["@Notes"].Value = ObjCVarWH_CYReceivables.Notes;
                        Com.Parameters["@IsDeleted"].Value = ObjCVarWH_CYReceivables.IsDeleted;
                        Com.Parameters["@CreatorUserID"].Value = ObjCVarWH_CYReceivables.CreatorUserID;
                        Com.Parameters["@CreationDate"].Value = ObjCVarWH_CYReceivables.CreationDate;
                        Com.Parameters["@ModificatorUserID"].Value = ObjCVarWH_CYReceivables.ModificatorUserID;
                        Com.Parameters["@ModificationDate"].Value = ObjCVarWH_CYReceivables.ModificationDate;
                        Com.Parameters["@IssueDate"].Value = ObjCVarWH_CYReceivables.IssueDate;
                        Com.Parameters["@TaxAmount"].Value = ObjCVarWH_CYReceivables.TaxAmount;
                        Com.Parameters["@HouseBillID"].Value = ObjCVarWH_CYReceivables.HouseBillID;
                        EndTrans(Com, Con);
                        if (ObjCVarWH_CYReceivables.ID == 0)
                        {
                            ObjCVarWH_CYReceivables.ID = Convert.ToInt64(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarWH_CYReceivables.mIsChanges = false;
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
        #region "extra Function"
        public Exception CalculateMTYInv(string ContainerNumber)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarWH_CYReceivables.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;

                Com.Parameters.Add(new SqlParameter("@ContainerNumber", SqlDbType.NVarChar));
                Com.CommandText = "[dbo].GetInvCalc";
                Com.Parameters["@ContainerNumber"].Value = ContainerNumber.ToString();
                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        /*Start DataReader*/
                        CVarWH_CYReceivables ObjCVarWH_CYReceivables = new CVarWH_CYReceivables();
                        ObjCVarWH_CYReceivables.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarWH_CYReceivables.mChargeTypeID = Convert.ToInt32(dr["ChargeTypeID"].ToString());
                        ObjCVarWH_CYReceivables.mQuantity = Convert.ToDecimal(dr["Quantity"].ToString());
                        ObjCVarWH_CYReceivables.mSalePrice = Convert.ToDecimal(dr["SalePrice"].ToString());
                        ObjCVarWH_CYReceivables.mSaleAmount = Convert.ToDecimal(dr["SaleAmount"].ToString());
                        ObjCVarWH_CYReceivables.mWH_CYInvoicesID = Convert.ToInt32(dr["WH_CYInvoicesID"].ToString());
                        ObjCVarWH_CYReceivables.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarWH_CYReceivables.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarWH_CYReceivables.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarWH_CYReceivables.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarWH_CYReceivables.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarWH_CYReceivables.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarWH_CYReceivables.mIssueDate = Convert.ToDateTime(dr["IssueDate"].ToString());
                        ObjCVarWH_CYReceivables.mTaxAmount = Convert.ToDecimal(dr["TaxAmount"].ToString());
                        ObjCVarWH_CYReceivables.mHouseBillID = Convert.ToInt64(dr["HouseBillID"].ToString());
                        lstCVarWH_CYReceivables.Add(ObjCVarWH_CYReceivables);
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
