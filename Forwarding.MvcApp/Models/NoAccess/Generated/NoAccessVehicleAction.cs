﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.NoAccess.Generated
{
    [Serializable]
    public class CPKNoAccessVehicleAction
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
    public partial class CVarNoAccessVehicleAction : CPKNoAccessVehicleAction
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mCode;
        internal String mName;
        internal Boolean mIsOperationAction;
        internal Boolean mIsWarehouseAction;
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
        public Boolean IsOperationAction
        {
            get { return mIsOperationAction; }
            set { mIsChanges = true; mIsOperationAction = value; }
        }
        public Boolean IsWarehouseAction
        {
            get { return mIsWarehouseAction; }
            set { mIsChanges = true; mIsWarehouseAction = value; }
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

    public partial class CNoAccessVehicleAction
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
        public List<CVarNoAccessVehicleAction> lstCVarNoAccessVehicleAction = new List<CVarNoAccessVehicleAction>();
        public List<CPKNoAccessVehicleAction> lstDeletedCPKNoAccessVehicleAction = new List<CPKNoAccessVehicleAction>();
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
            lstCVarNoAccessVehicleAction.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListNoAccessVehicleAction";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemNoAccessVehicleAction";
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
                        CVarNoAccessVehicleAction ObjCVarNoAccessVehicleAction = new CVarNoAccessVehicleAction();
                        ObjCVarNoAccessVehicleAction.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarNoAccessVehicleAction.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarNoAccessVehicleAction.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarNoAccessVehicleAction.mIsOperationAction = Convert.ToBoolean(dr["IsOperationAction"].ToString());
                        ObjCVarNoAccessVehicleAction.mIsWarehouseAction = Convert.ToBoolean(dr["IsWarehouseAction"].ToString());
                        lstCVarNoAccessVehicleAction.Add(ObjCVarNoAccessVehicleAction);
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
            lstCVarNoAccessVehicleAction.Clear();

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
                Com.CommandText = "[dbo].GetListPagingNoAccessVehicleAction";
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
                        CVarNoAccessVehicleAction ObjCVarNoAccessVehicleAction = new CVarNoAccessVehicleAction();
                        ObjCVarNoAccessVehicleAction.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarNoAccessVehicleAction.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarNoAccessVehicleAction.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarNoAccessVehicleAction.mIsOperationAction = Convert.ToBoolean(dr["IsOperationAction"].ToString());
                        ObjCVarNoAccessVehicleAction.mIsWarehouseAction = Convert.ToBoolean(dr["IsWarehouseAction"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarNoAccessVehicleAction.Add(ObjCVarNoAccessVehicleAction);
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
                    Com.CommandText = "[dbo].DeleteListNoAccessVehicleAction";
                else
                    Com.CommandText = "[dbo].UpdateListNoAccessVehicleAction";
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
        public Exception DeleteItem(List<CPKNoAccessVehicleAction> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemNoAccessVehicleAction";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                foreach (CPKNoAccessVehicleAction ObjCPKNoAccessVehicleAction in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt32(ObjCPKNoAccessVehicleAction.ID);
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
        public Exception SaveMethod(List<CVarNoAccessVehicleAction> SaveList)
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
                Com.Parameters.Add(new SqlParameter("@IsOperationAction", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@IsWarehouseAction", SqlDbType.Bit));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarNoAccessVehicleAction ObjCVarNoAccessVehicleAction in SaveList)
                {
                    if (ObjCVarNoAccessVehicleAction.mIsChanges == true)
                    {
                        if (ObjCVarNoAccessVehicleAction.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemNoAccessVehicleAction";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarNoAccessVehicleAction.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemNoAccessVehicleAction";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarNoAccessVehicleAction.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarNoAccessVehicleAction.ID;
                        }
                        Com.Parameters["@Code"].Value = ObjCVarNoAccessVehicleAction.Code;
                        Com.Parameters["@Name"].Value = ObjCVarNoAccessVehicleAction.Name;
                        Com.Parameters["@IsOperationAction"].Value = ObjCVarNoAccessVehicleAction.IsOperationAction;
                        Com.Parameters["@IsWarehouseAction"].Value = ObjCVarNoAccessVehicleAction.IsWarehouseAction;
                        EndTrans(Com, Con);
                        if (ObjCVarNoAccessVehicleAction.ID == 0)
                        {
                            ObjCVarNoAccessVehicleAction.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarNoAccessVehicleAction.mIsChanges = false;
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
