﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.ReceiptsAndPayments.YardLink.Generated
{
    [Serializable]
    public class CPKSL_YardLinkClients
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
    public partial class CVarSL_YardLinkClients : CPKSL_YardLinkClients
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mYardClientID;
        internal Int32 mERPClientID;
        internal Int32 mSubAccountID;
        #endregion

        #region "Methods"
        public Int64 YardClientID
        {
            get { return mYardClientID; }
            set { mIsChanges = true; mYardClientID = value; }
        }
        public Int32 ERPClientID
        {
            get { return mERPClientID; }
            set { mIsChanges = true; mERPClientID = value; }
        }
        public Int32 SubAccountID
        {
            get { return mSubAccountID; }
            set { mIsChanges = true; mSubAccountID = value; }
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

    public partial class CSL_YardLinkClients
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
        public List<CVarSL_YardLinkClients> lstCVarSL_YardLinkClients = new List<CVarSL_YardLinkClients>();
        public List<CPKSL_YardLinkClients> lstDeletedCPKSL_YardLinkClients = new List<CPKSL_YardLinkClients>();
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
            lstCVarSL_YardLinkClients.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListSL_YardLinkClients";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemSL_YardLinkClients";
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
                        CVarSL_YardLinkClients ObjCVarSL_YardLinkClients = new CVarSL_YardLinkClients();
                        ObjCVarSL_YardLinkClients.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarSL_YardLinkClients.mYardClientID = Convert.ToInt64(dr["YardClientID"].ToString());
                        ObjCVarSL_YardLinkClients.mERPClientID = Convert.ToInt32(dr["ERPClientID"].ToString());
                        ObjCVarSL_YardLinkClients.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        lstCVarSL_YardLinkClients.Add(ObjCVarSL_YardLinkClients);
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
            lstCVarSL_YardLinkClients.Clear();

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
                Com.CommandText = "[dbo].GetListPagingSL_YardLinkClients";
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
                        CVarSL_YardLinkClients ObjCVarSL_YardLinkClients = new CVarSL_YardLinkClients();
                        ObjCVarSL_YardLinkClients.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarSL_YardLinkClients.mYardClientID = Convert.ToInt64(dr["YardClientID"].ToString());
                        ObjCVarSL_YardLinkClients.mERPClientID = Convert.ToInt32(dr["ERPClientID"].ToString());
                        ObjCVarSL_YardLinkClients.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarSL_YardLinkClients.Add(ObjCVarSL_YardLinkClients);
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
                    Com.CommandText = "[dbo].DeleteListSL_YardLinkClients";
                else
                    Com.CommandText = "[dbo].UpdateListSL_YardLinkClients";
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
        public Exception DeleteItem(List<CPKSL_YardLinkClients> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemSL_YardLinkClients";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt));
                foreach (CPKSL_YardLinkClients ObjCPKSL_YardLinkClients in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt64(ObjCPKSL_YardLinkClients.ID);
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
        public Exception SaveMethod(List<CVarSL_YardLinkClients> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@YardClientID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@ERPClientID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@SubAccountID", SqlDbType.Int));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarSL_YardLinkClients ObjCVarSL_YardLinkClients in SaveList)
                {
                    if (ObjCVarSL_YardLinkClients.mIsChanges == true)
                    {
                        if (ObjCVarSL_YardLinkClients.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemSL_YardLinkClients";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarSL_YardLinkClients.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemSL_YardLinkClients";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarSL_YardLinkClients.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarSL_YardLinkClients.ID;
                        }
                        Com.Parameters["@YardClientID"].Value = ObjCVarSL_YardLinkClients.YardClientID;
                        Com.Parameters["@ERPClientID"].Value = ObjCVarSL_YardLinkClients.ERPClientID;
                        Com.Parameters["@SubAccountID"].Value = ObjCVarSL_YardLinkClients.SubAccountID;
                        EndTrans(Com, Con);
                        if (ObjCVarSL_YardLinkClients.ID == 0)
                        {
                            ObjCVarSL_YardLinkClients.ID = Convert.ToInt64(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarSL_YardLinkClients.mIsChanges = false;
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
