﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.NoAccess.Generated
{
    [Serializable]
    public class CPKNoAccessPipeLineStage
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
    public partial class CVarNoAccessPipeLineStage : CPKNoAccessPipeLineStage
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mCode;
        internal String mName;
        internal String mNotes;
        internal Boolean mIsActive;
        internal Decimal mActionPercent;
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
        public String Notes
        {
            get { return mNotes; }
            set { mIsChanges = true; mNotes = value; }
        }
        public Boolean IsActive
        {
            get { return mIsActive; }
            set { mIsChanges = true; mIsActive = value; }
        }
        public Decimal ActionPercent
        {
            get { return mActionPercent; }
            set { mIsChanges = true; mActionPercent = value; }
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

    public partial class CNoAccessPipeLineStage
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
        public List<CVarNoAccessPipeLineStage> lstCVarNoAccessPipeLineStage = new List<CVarNoAccessPipeLineStage>();
        public List<CPKNoAccessPipeLineStage> lstDeletedCPKNoAccessPipeLineStage = new List<CPKNoAccessPipeLineStage>();
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
            lstCVarNoAccessPipeLineStage.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.VarChar));
                    Com.CommandText = "[dbo].GetListNoAccessPipeLineStage";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemNoAccessPipeLineStage";
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
                        CVarNoAccessPipeLineStage ObjCVarNoAccessPipeLineStage = new CVarNoAccessPipeLineStage();
                        ObjCVarNoAccessPipeLineStage.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarNoAccessPipeLineStage.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarNoAccessPipeLineStage.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarNoAccessPipeLineStage.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarNoAccessPipeLineStage.mIsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        ObjCVarNoAccessPipeLineStage.mActionPercent = Convert.ToDecimal(dr["ActionPercent"].ToString());
                        lstCVarNoAccessPipeLineStage.Add(ObjCVarNoAccessPipeLineStage);
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
            lstCVarNoAccessPipeLineStage.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                Com.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@PageNumber", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.VarChar));
                Com.Parameters.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar));
                Com.CommandText = "[dbo].GetListPagingNoAccessPipeLineStage";
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
                        CVarNoAccessPipeLineStage ObjCVarNoAccessPipeLineStage = new CVarNoAccessPipeLineStage();
                        ObjCVarNoAccessPipeLineStage.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarNoAccessPipeLineStage.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarNoAccessPipeLineStage.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarNoAccessPipeLineStage.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarNoAccessPipeLineStage.mIsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        ObjCVarNoAccessPipeLineStage.mActionPercent = Convert.ToDecimal(dr["ActionPercent"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarNoAccessPipeLineStage.Add(ObjCVarNoAccessPipeLineStage);
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
                    Com.CommandText = "[dbo].DeleteListNoAccessPipeLineStage";
                else
                    Com.CommandText = "[dbo].UpdateListNoAccessPipeLineStage";
                Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.VarChar));
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
        public Exception DeleteItem(List<CPKNoAccessPipeLineStage> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemNoAccessPipeLineStage";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                foreach (CPKNoAccessPipeLineStage ObjCPKNoAccessPipeLineStage in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt32(ObjCPKNoAccessPipeLineStage.ID);
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
        public Exception SaveMethod(List<CVarNoAccessPipeLineStage> SaveList)
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
                Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@ActionPercent", SqlDbType.Decimal));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarNoAccessPipeLineStage ObjCVarNoAccessPipeLineStage in SaveList)
                {
                    if (ObjCVarNoAccessPipeLineStage.mIsChanges == true)
                    {
                        if (ObjCVarNoAccessPipeLineStage.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemNoAccessPipeLineStage";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarNoAccessPipeLineStage.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemNoAccessPipeLineStage";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarNoAccessPipeLineStage.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarNoAccessPipeLineStage.ID;
                        }
                        Com.Parameters["@Code"].Value = ObjCVarNoAccessPipeLineStage.Code;
                        Com.Parameters["@Name"].Value = ObjCVarNoAccessPipeLineStage.Name;
                        Com.Parameters["@Notes"].Value = ObjCVarNoAccessPipeLineStage.Notes;
                        Com.Parameters["@IsActive"].Value = ObjCVarNoAccessPipeLineStage.IsActive;
                        Com.Parameters["@ActionPercent"].Value = ObjCVarNoAccessPipeLineStage.ActionPercent;
                        EndTrans(Com, Con);
                        if (ObjCVarNoAccessPipeLineStage.ID == 0)
                        {
                            ObjCVarNoAccessPipeLineStage.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarNoAccessPipeLineStage.mIsChanges = false;
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
