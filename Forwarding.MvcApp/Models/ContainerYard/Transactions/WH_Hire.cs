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
    public class CPKWH_Hire
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
    public partial class CVarWH_Hire : CPKWH_Hire
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mWH_CntrStockID;
        internal Int32 mWH_WarehouseID;
        internal DateTime mTransactionDate;
        internal Boolean mIsHire;
        internal Int32 mCustomerID;
        internal String mRemarks;
        #endregion

        #region "Methods"
        public Int32 WH_CntrStockID
        {
            get { return mWH_CntrStockID; }
            set { mIsChanges = true; mWH_CntrStockID = value; }
        }
        public Int32 WH_WarehouseID
        {
            get { return mWH_WarehouseID; }
            set { mIsChanges = true; mWH_WarehouseID = value; }
        }
        public DateTime TransactionDate
        {
            get { return mTransactionDate; }
            set { mIsChanges = true; mTransactionDate = value; }
        }
        public Boolean IsHire
        {
            get { return mIsHire; }
            set { mIsChanges = true; mIsHire = value; }
        }
        public Int32 CustomerID
        {
            get { return mCustomerID; }
            set { mIsChanges = true; mCustomerID = value; }
        }
        public String Remarks
        {
            get { return mRemarks; }
            set { mIsChanges = true; mRemarks = value; }
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

    public partial class CWH_Hire
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
        public List<CVarWH_Hire> lstCVarWH_Hire = new List<CVarWH_Hire>();
        public List<CPKWH_Hire> lstDeletedCPKWH_Hire = new List<CPKWH_Hire>();
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
            lstCVarWH_Hire.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListWH_Hire";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemWH_Hire";
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
                        CVarWH_Hire ObjCVarWH_Hire = new CVarWH_Hire();
                        ObjCVarWH_Hire.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarWH_Hire.mWH_CntrStockID = Convert.ToInt32(dr["WH_CntrStockID"].ToString());
                        ObjCVarWH_Hire.mWH_WarehouseID = Convert.ToInt32(dr["WH_WarehouseID"].ToString());
                        ObjCVarWH_Hire.mTransactionDate = Convert.ToDateTime(dr["TransactionDate"].ToString());
                        ObjCVarWH_Hire.mIsHire = Convert.ToBoolean(dr["IsHire"].ToString());
                        ObjCVarWH_Hire.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarWH_Hire.mRemarks = Convert.ToString(dr["Remarks"].ToString());
                        lstCVarWH_Hire.Add(ObjCVarWH_Hire);
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
            lstCVarWH_Hire.Clear();

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
                Com.CommandText = "[dbo].GetListPagingWH_Hire";
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
                        CVarWH_Hire ObjCVarWH_Hire = new CVarWH_Hire();
                        ObjCVarWH_Hire.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarWH_Hire.mWH_CntrStockID = Convert.ToInt32(dr["WH_CntrStockID"].ToString());
                        ObjCVarWH_Hire.mWH_WarehouseID = Convert.ToInt32(dr["WH_WarehouseID"].ToString());
                        ObjCVarWH_Hire.mTransactionDate = Convert.ToDateTime(dr["TransactionDate"].ToString());
                        ObjCVarWH_Hire.mIsHire = Convert.ToBoolean(dr["IsHire"].ToString());
                        ObjCVarWH_Hire.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarWH_Hire.mRemarks = Convert.ToString(dr["Remarks"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarWH_Hire.Add(ObjCVarWH_Hire);
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
                    Com.CommandText = "[dbo].DeleteListWH_Hire";
                else
                    Com.CommandText = "[dbo].UpdateListWH_Hire";
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
        public Exception DeleteItem(List<CPKWH_Hire> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemWH_Hire";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt));
                foreach (CPKWH_Hire ObjCPKWH_Hire in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt64(ObjCPKWH_Hire.ID);
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
        public Exception SaveMethod(List<CVarWH_Hire> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@WH_CntrStockID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@WH_WarehouseID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@TransactionDate", SqlDbType.Date));
                Com.Parameters.Add(new SqlParameter("@IsHire", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@Remarks", SqlDbType.NVarChar));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarWH_Hire ObjCVarWH_Hire in SaveList)
                {
                    if (ObjCVarWH_Hire.mIsChanges == true)
                    {
                        if (ObjCVarWH_Hire.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemWH_Hire";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarWH_Hire.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemWH_Hire";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarWH_Hire.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarWH_Hire.ID;
                        }
                        Com.Parameters["@WH_CntrStockID"].Value = ObjCVarWH_Hire.WH_CntrStockID;
                        Com.Parameters["@WH_WarehouseID"].Value = ObjCVarWH_Hire.WH_WarehouseID;
                        Com.Parameters["@TransactionDate"].Value = ObjCVarWH_Hire.TransactionDate;
                        Com.Parameters["@IsHire"].Value = ObjCVarWH_Hire.IsHire;
                        Com.Parameters["@CustomerID"].Value = ObjCVarWH_Hire.CustomerID;
                        Com.Parameters["@Remarks"].Value = ObjCVarWH_Hire.Remarks;
                        EndTrans(Com, Con);
                        if (ObjCVarWH_Hire.ID == 0)
                        {
                            ObjCVarWH_Hire.ID = Convert.ToInt64(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarWH_Hire.mIsChanges = false;
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
