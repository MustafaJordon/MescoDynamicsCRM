﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using WebMatrix.WebData;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.ContainerFreightStation.Tariff
{
    [Serializable]
    public class CPKWH_CSL_Tariff
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
    public partial class CVarWH_CSL_Tariff : CPKWH_CSL_Tariff
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mCode;
        internal String mName;
        internal Int32 mCustomerID;
        internal Int32 mWH_WarehouseID;
        internal Boolean mIsDefault;
        internal Boolean mIsHold;
        internal DateTime mValidFromTo;
        internal Int32 mInvoiceTypeID;
        #endregion

        #region "Methods"
        public String Code
        {
            get { return mCode; }
            set { mIsChanges = true; mCode = value; }
        }
        public String Name
        {
            get { return mName; }
            set { mIsChanges = true; mName = value; }
        }
        public Int32 CustomerID
        {
            get { return mCustomerID; }
            set { mIsChanges = true; mCustomerID = value; }
        }
        public Int32 WH_WarehouseID
        {
            get { return mWH_WarehouseID; }
            set { mIsChanges = true; mWH_WarehouseID = value; }
        }
        public Boolean IsDefault
        {
            get { return mIsDefault; }
            set { mIsChanges = true; mIsDefault = value; }
        }
        public Boolean IsHold
        {
            get { return mIsHold; }
            set { mIsChanges = true; mIsHold = value; }
        }
        public DateTime ValidFromTo
        {
            get { return mValidFromTo; }
            set { mIsChanges = true; mValidFromTo = value; }
        }
        public Int32 InvoiceTypeID
        {
            get { return mInvoiceTypeID; }
            set { mIsChanges = true; mInvoiceTypeID = value; }
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

    public partial class CWH_CSL_Tariff
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
        public List<CVarWH_CSL_Tariff> lstCVarWH_CSL_Tariff = new List<CVarWH_CSL_Tariff>();
        public List<CPKWH_CSL_Tariff> lstDeletedCPKWH_CSL_Tariff = new List<CPKWH_CSL_Tariff>();
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
            lstCVarWH_CSL_Tariff.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListWH_CSL_Tariff";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemWH_CSL_Tariff";
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
                        CVarWH_CSL_Tariff ObjCVarWH_CSL_Tariff = new CVarWH_CSL_Tariff();
                        ObjCVarWH_CSL_Tariff.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarWH_CSL_Tariff.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarWH_CSL_Tariff.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarWH_CSL_Tariff.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarWH_CSL_Tariff.mWH_WarehouseID = Convert.ToInt32(dr["WH_WarehouseID"].ToString());
                        ObjCVarWH_CSL_Tariff.mIsDefault = Convert.ToBoolean(dr["IsDefault"].ToString());
                        ObjCVarWH_CSL_Tariff.mIsHold = Convert.ToBoolean(dr["IsHold"].ToString());
                        ObjCVarWH_CSL_Tariff.mValidFromTo = Convert.ToDateTime(dr["ValidFromTo"].ToString());
                        ObjCVarWH_CSL_Tariff.mInvoiceTypeID = Convert.ToInt32(dr["InvoiceTypeID"].ToString());
                        lstCVarWH_CSL_Tariff.Add(ObjCVarWH_CSL_Tariff);
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
            lstCVarWH_CSL_Tariff.Clear();

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
                Com.CommandText = "[dbo].GetListPagingWH_CSL_Tariff";
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
                        CVarWH_CSL_Tariff ObjCVarWH_CSL_Tariff = new CVarWH_CSL_Tariff();
                        ObjCVarWH_CSL_Tariff.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarWH_CSL_Tariff.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarWH_CSL_Tariff.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarWH_CSL_Tariff.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarWH_CSL_Tariff.mWH_WarehouseID = Convert.ToInt32(dr["WH_WarehouseID"].ToString());
                        ObjCVarWH_CSL_Tariff.mIsDefault = Convert.ToBoolean(dr["IsDefault"].ToString());
                        ObjCVarWH_CSL_Tariff.mIsHold = Convert.ToBoolean(dr["IsHold"].ToString());
                        ObjCVarWH_CSL_Tariff.mValidFromTo = Convert.ToDateTime(dr["ValidFromTo"].ToString());
                        ObjCVarWH_CSL_Tariff.mInvoiceTypeID = Convert.ToInt32(dr["InvoiceTypeID"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarWH_CSL_Tariff.Add(ObjCVarWH_CSL_Tariff);
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
                    Com.CommandText = "[dbo].DeleteListWH_CSL_Tariff";
                else
                    Com.CommandText = "[dbo].UpdateListWH_CSL_Tariff";
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
        public Exception DeleteItem(List<CPKWH_CSL_Tariff> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemWH_CSL_Tariff";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                foreach (CPKWH_CSL_Tariff ObjCPKWH_CSL_Tariff in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt32(ObjCPKWH_CSL_Tariff.ID);
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
        public Exception SaveMethod(List<CVarWH_CSL_Tariff> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                //Com.Parameters.Add(new SqlParameter("@Code", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@WH_WarehouseID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@IsDefault", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@IsHold", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@ValidFromTo", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@InvoiceTypeID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int));

                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                SqlParameter paraCode = Com.Parameters.Add(new SqlParameter("@Code", SqlDbType.NVarChar, 0, ParameterDirection.Input, false, 0, 0, "Code", DataRowVersion.Default, null));

                foreach (CVarWH_CSL_Tariff ObjCVarWH_CSL_Tariff in SaveList)
                {
                    if (ObjCVarWH_CSL_Tariff.mIsChanges == true)
                    {
                        if (ObjCVarWH_CSL_Tariff.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemWH_CSL_Tariff";
                            paraID.Direction = ParameterDirection.Output;
                            paraCode.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarWH_CSL_Tariff.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemWH_CSL_Tariff";
                            paraID.Direction = ParameterDirection.Input;
                            paraCode.Direction = ParameterDirection.Input;
                        }

                        BeginTrans(Com, Con);

                        if (ObjCVarWH_CSL_Tariff.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarWH_CSL_Tariff.ID;
                        }
                        Com.Parameters["@Code"].Value = ObjCVarWH_CSL_Tariff.Code;
                        Com.Parameters["@Name"].Value = ObjCVarWH_CSL_Tariff.Name;
                        Com.Parameters["@CustomerID"].Value = ObjCVarWH_CSL_Tariff.CustomerID;
                        Com.Parameters["@WH_WarehouseID"].Value = ObjCVarWH_CSL_Tariff.WH_WarehouseID;
                        Com.Parameters["@IsDefault"].Value = ObjCVarWH_CSL_Tariff.IsDefault;
                        Com.Parameters["@IsHold"].Value = ObjCVarWH_CSL_Tariff.IsHold;
                        Com.Parameters["@ValidFromTo"].Value = ObjCVarWH_CSL_Tariff.ValidFromTo;
                        Com.Parameters["@InvoiceTypeID"].Value = ObjCVarWH_CSL_Tariff.InvoiceTypeID;
                        Com.Parameters["@UserID"].Value = WebSecurity.CurrentUserId;

                        EndTrans(Com, Con);

                        if (ObjCVarWH_CSL_Tariff.ID == 0)
                        {
                            ObjCVarWH_CSL_Tariff.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
                            ObjCVarWH_CSL_Tariff.Code = Convert.ToString(Com.Parameters["@Code"].Value);
                        }
                        ObjCVarWH_CSL_Tariff.mIsChanges = false;
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
