﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.ReceiptsAndPayments.ShipLink.Generated
{
    [Serializable]
    public class CPKSL_LinkingInvoiceTypeJournal_Payment
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
    public partial class CVarSL_LinkingInvoiceTypeJournal_Payment : CPKSL_LinkingInvoiceTypeJournal_Payment
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mInvoiceTypeID;
        internal Int32 mJournalTypeID;
        internal Int32 mJVTypeID;
        internal Int32 mAccountID;
        internal Int32 mSubAccountID;
        #endregion

        #region "Methods"
        public Int32 InvoiceTypeID
        {
            get { return mInvoiceTypeID; }
            set { mIsChanges = true; mInvoiceTypeID = value; }
        }
        public Int32 JournalTypeID
        {
            get { return mJournalTypeID; }
            set { mIsChanges = true; mJournalTypeID = value; }
        }
        public Int32 JVTypeID
        {
            get { return mJVTypeID; }
            set { mIsChanges = true; mJVTypeID = value; }
        }
        public Int32 AccountID
        {
            get { return mAccountID; }
            set { mIsChanges = true; mAccountID = value; }
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

    public partial class CSL_LinkingInvoiceTypeJournal_Payment
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
        public List<CVarSL_LinkingInvoiceTypeJournal_Payment> lstCVarSL_LinkingInvoiceTypeJournal_Payment = new List<CVarSL_LinkingInvoiceTypeJournal_Payment>();
        public List<CPKSL_LinkingInvoiceTypeJournal_Payment> lstDeletedCPKSL_LinkingInvoiceTypeJournal_Payment = new List<CPKSL_LinkingInvoiceTypeJournal_Payment>();
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
            lstCVarSL_LinkingInvoiceTypeJournal_Payment.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListSL_LinkingInvoiceTypeJournal_Payment";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemSL_LinkingInvoiceTypeJournal_Payment";
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
                        CVarSL_LinkingInvoiceTypeJournal_Payment ObjCVarSL_LinkingInvoiceTypeJournal_Payment = new CVarSL_LinkingInvoiceTypeJournal_Payment();
                        ObjCVarSL_LinkingInvoiceTypeJournal_Payment.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarSL_LinkingInvoiceTypeJournal_Payment.mInvoiceTypeID = Convert.ToInt32(dr["InvoiceTypeID"].ToString());
                        ObjCVarSL_LinkingInvoiceTypeJournal_Payment.mJournalTypeID = Convert.ToInt32(dr["JournalTypeID"].ToString());
                        ObjCVarSL_LinkingInvoiceTypeJournal_Payment.mJVTypeID = Convert.ToInt32(dr["JVTypeID"].ToString());
                        ObjCVarSL_LinkingInvoiceTypeJournal_Payment.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarSL_LinkingInvoiceTypeJournal_Payment.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        lstCVarSL_LinkingInvoiceTypeJournal_Payment.Add(ObjCVarSL_LinkingInvoiceTypeJournal_Payment);
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
            lstCVarSL_LinkingInvoiceTypeJournal_Payment.Clear();

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
                Com.CommandText = "[dbo].GetListPagingSL_LinkingInvoiceTypeJournal_Payment";
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
                        CVarSL_LinkingInvoiceTypeJournal_Payment ObjCVarSL_LinkingInvoiceTypeJournal_Payment = new CVarSL_LinkingInvoiceTypeJournal_Payment();
                        ObjCVarSL_LinkingInvoiceTypeJournal_Payment.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarSL_LinkingInvoiceTypeJournal_Payment.mInvoiceTypeID = Convert.ToInt32(dr["InvoiceTypeID"].ToString());
                        ObjCVarSL_LinkingInvoiceTypeJournal_Payment.mJournalTypeID = Convert.ToInt32(dr["JournalTypeID"].ToString());
                        ObjCVarSL_LinkingInvoiceTypeJournal_Payment.mJVTypeID = Convert.ToInt32(dr["JVTypeID"].ToString());
                        ObjCVarSL_LinkingInvoiceTypeJournal_Payment.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarSL_LinkingInvoiceTypeJournal_Payment.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarSL_LinkingInvoiceTypeJournal_Payment.Add(ObjCVarSL_LinkingInvoiceTypeJournal_Payment);
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
                    Com.CommandText = "[dbo].DeleteListSL_LinkingInvoiceTypeJournal_Payment";
                else
                    Com.CommandText = "[dbo].UpdateListSL_LinkingInvoiceTypeJournal_Payment";
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
        public Exception DeleteItem(List<CPKSL_LinkingInvoiceTypeJournal_Payment> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemSL_LinkingInvoiceTypeJournal_Payment";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                foreach (CPKSL_LinkingInvoiceTypeJournal_Payment ObjCPKSL_LinkingInvoiceTypeJournal_Payment in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt32(ObjCPKSL_LinkingInvoiceTypeJournal_Payment.ID);
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
        public Exception SaveMethod(List<CVarSL_LinkingInvoiceTypeJournal_Payment> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@InvoiceTypeID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@JournalTypeID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@JVTypeID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@AccountID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@SubAccountID", SqlDbType.Int));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarSL_LinkingInvoiceTypeJournal_Payment ObjCVarSL_LinkingInvoiceTypeJournal_Payment in SaveList)
                {
                    if (ObjCVarSL_LinkingInvoiceTypeJournal_Payment.mIsChanges == true)
                    {
                        if (ObjCVarSL_LinkingInvoiceTypeJournal_Payment.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemSL_LinkingInvoiceTypeJournal_Payment";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarSL_LinkingInvoiceTypeJournal_Payment.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemSL_LinkingInvoiceTypeJournal_Payment";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarSL_LinkingInvoiceTypeJournal_Payment.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarSL_LinkingInvoiceTypeJournal_Payment.ID;
                        }
                        Com.Parameters["@InvoiceTypeID"].Value = ObjCVarSL_LinkingInvoiceTypeJournal_Payment.InvoiceTypeID;
                        Com.Parameters["@JournalTypeID"].Value = ObjCVarSL_LinkingInvoiceTypeJournal_Payment.JournalTypeID;
                        Com.Parameters["@JVTypeID"].Value = ObjCVarSL_LinkingInvoiceTypeJournal_Payment.JVTypeID;
                        Com.Parameters["@AccountID"].Value = ObjCVarSL_LinkingInvoiceTypeJournal_Payment.AccountID;
                        Com.Parameters["@SubAccountID"].Value = ObjCVarSL_LinkingInvoiceTypeJournal_Payment.SubAccountID;
                        EndTrans(Com, Con);
                        if (ObjCVarSL_LinkingInvoiceTypeJournal_Payment.ID == 0)
                        {
                            ObjCVarSL_LinkingInvoiceTypeJournal_Payment.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarSL_LinkingInvoiceTypeJournal_Payment.mIsChanges = false;
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
