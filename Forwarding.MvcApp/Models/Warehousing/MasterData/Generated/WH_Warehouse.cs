﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.Warehousing.MasterData.Generated
{
    [Serializable]
    public class CPKWH_Warehouse
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
    public partial class CVarWH_Warehouse : CPKWH_Warehouse
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mCode;
        internal String mName;
        internal String mLocalName;
        internal String mPhone;
        internal String mFax;
        internal String mAddress;
        internal String mNotes;
        internal Int32 mMainWarehouseID;
        internal Int32 mWarehouseType;
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
        public String LocalName
        {
            get { return mLocalName; }
            set { mIsChanges = true; mLocalName = value; }
        }
        public String Phone
        {
            get { return mPhone; }
            set { mIsChanges = true; mPhone = value; }
        }
        public String Fax
        {
            get { return mFax; }
            set { mIsChanges = true; mFax = value; }
        }
        public String Address
        {
            get { return mAddress; }
            set { mIsChanges = true; mAddress = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mIsChanges = true; mNotes = value; }
        }
        public Int32 MainWarehouseID
        {
            get { return mMainWarehouseID; }
            set { mIsChanges = true; mMainWarehouseID = value; }
        }
        public Int32 WarehouseType
        {
            get { return mWarehouseType; }
            set { mIsChanges = true; mWarehouseType = value; }
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

    public partial class CWH_Warehouse
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
        public List<CVarWH_Warehouse> lstCVarWH_Warehouse = new List<CVarWH_Warehouse>();
        public List<CPKWH_Warehouse> lstDeletedCPKWH_Warehouse = new List<CPKWH_Warehouse>();
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
            lstCVarWH_Warehouse.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListWH_Warehouse";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemWH_Warehouse";
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
                        CVarWH_Warehouse ObjCVarWH_Warehouse = new CVarWH_Warehouse();
                        ObjCVarWH_Warehouse.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarWH_Warehouse.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarWH_Warehouse.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarWH_Warehouse.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarWH_Warehouse.mPhone = Convert.ToString(dr["Phone"].ToString());
                        ObjCVarWH_Warehouse.mFax = Convert.ToString(dr["Fax"].ToString());
                        ObjCVarWH_Warehouse.mAddress = Convert.ToString(dr["Address"].ToString());
                        ObjCVarWH_Warehouse.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarWH_Warehouse.mMainWarehouseID = Convert.ToInt32(dr["MainWarehouseID"].ToString());
                        ObjCVarWH_Warehouse.mWarehouseType = Convert.ToInt32(dr["WarehouseType"].ToString());
                        lstCVarWH_Warehouse.Add(ObjCVarWH_Warehouse);
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
            lstCVarWH_Warehouse.Clear();

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
                Com.CommandText = "[dbo].GetListPagingWH_Warehouse";
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
                        CVarWH_Warehouse ObjCVarWH_Warehouse = new CVarWH_Warehouse();
                        ObjCVarWH_Warehouse.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarWH_Warehouse.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarWH_Warehouse.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarWH_Warehouse.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarWH_Warehouse.mPhone = Convert.ToString(dr["Phone"].ToString());
                        ObjCVarWH_Warehouse.mFax = Convert.ToString(dr["Fax"].ToString());
                        ObjCVarWH_Warehouse.mAddress = Convert.ToString(dr["Address"].ToString());
                        ObjCVarWH_Warehouse.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarWH_Warehouse.mMainWarehouseID = Convert.ToInt32(dr["MainWarehouseID"].ToString());
                        ObjCVarWH_Warehouse.mWarehouseType = Convert.ToInt32(dr["WarehouseType"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarWH_Warehouse.Add(ObjCVarWH_Warehouse);
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
                    Com.CommandText = "[dbo].DeleteListWH_Warehouse";
                else
                    Com.CommandText = "[dbo].UpdateListWH_Warehouse";
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
        public Exception DeleteItem(List<CPKWH_Warehouse> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemWH_Warehouse";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                foreach (CPKWH_Warehouse ObjCPKWH_Warehouse in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt32(ObjCPKWH_Warehouse.ID);
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
        public Exception SaveMethod(List<CVarWH_Warehouse> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@Code", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@LocalName", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Fax", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@MainWarehouseID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@WarehouseType", SqlDbType.Int));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarWH_Warehouse ObjCVarWH_Warehouse in SaveList)
                {
                    if (ObjCVarWH_Warehouse.mIsChanges == true)
                    {
                        if (ObjCVarWH_Warehouse.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemWH_Warehouse";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarWH_Warehouse.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemWH_Warehouse";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarWH_Warehouse.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarWH_Warehouse.ID;
                        }
                        Com.Parameters["@Code"].Value = ObjCVarWH_Warehouse.Code;
                        Com.Parameters["@Name"].Value = ObjCVarWH_Warehouse.Name;
                        Com.Parameters["@LocalName"].Value = ObjCVarWH_Warehouse.LocalName;
                        Com.Parameters["@Phone"].Value = ObjCVarWH_Warehouse.Phone;
                        Com.Parameters["@Fax"].Value = ObjCVarWH_Warehouse.Fax;
                        Com.Parameters["@Address"].Value = ObjCVarWH_Warehouse.Address;
                        Com.Parameters["@Notes"].Value = ObjCVarWH_Warehouse.Notes;
                        Com.Parameters["@MainWarehouseID"].Value = ObjCVarWH_Warehouse.MainWarehouseID;
                        Com.Parameters["@WarehouseType"].Value = ObjCVarWH_Warehouse.WarehouseType;
                        EndTrans(Com, Con);
                        if (ObjCVarWH_Warehouse.ID == 0)
                        {
                            ObjCVarWH_Warehouse.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarWH_Warehouse.mIsChanges = false;
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

