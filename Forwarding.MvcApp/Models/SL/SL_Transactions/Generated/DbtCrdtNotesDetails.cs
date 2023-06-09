﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.SL.SL_Transactions.Generated
{
    [Serializable]
    public class CPKSL_DbtCrdtNotesDetails
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
    public partial class CVarSL_DbtCrdtNotesDetails : CPKSL_DbtCrdtNotesDetails
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mDbtCrdtNoteID;
        internal Int64 mServiceID;
        internal Int32 mAccountID;
        internal Decimal mAmount;
        internal Int32 mCurrencyID;
        internal String mNotes;
        #endregion

        #region "Methods"
        public Int32 DbtCrdtNoteID
        {
            get { return mDbtCrdtNoteID; }
            set { mIsChanges = true; mDbtCrdtNoteID = value; }
        }
        public Int64 ServiceID
        {
            get { return mServiceID; }
            set { mIsChanges = true; mServiceID = value; }
        }
        public Int32 AccountID
        {
            get { return mAccountID; }
            set { mIsChanges = true; mAccountID = value; }
        }
        public Decimal Amount
        {
            get { return mAmount; }
            set { mIsChanges = true; mAmount = value; }
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

    public partial class CSL_DbtCrdtNotesDetails
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
        public List<CVarSL_DbtCrdtNotesDetails> lstCVarSL_DbtCrdtNotesDetails = new List<CVarSL_DbtCrdtNotesDetails>();
        public List<CPKSL_DbtCrdtNotesDetails> lstDeletedCPKSL_DbtCrdtNotesDetails = new List<CPKSL_DbtCrdtNotesDetails>();
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
            lstCVarSL_DbtCrdtNotesDetails.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListSL_DbtCrdtNotesDetails";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemSL_DbtCrdtNotesDetails";
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
                        CVarSL_DbtCrdtNotesDetails ObjCVarSL_DbtCrdtNotesDetails = new CVarSL_DbtCrdtNotesDetails();
                        ObjCVarSL_DbtCrdtNotesDetails.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarSL_DbtCrdtNotesDetails.mDbtCrdtNoteID = Convert.ToInt32(dr["DbtCrdtNoteID"].ToString());
                        ObjCVarSL_DbtCrdtNotesDetails.mServiceID = Convert.ToInt64(dr["ServiceID"].ToString());
                        ObjCVarSL_DbtCrdtNotesDetails.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarSL_DbtCrdtNotesDetails.mAmount = Convert.ToDecimal(dr["Amount"].ToString());
                        ObjCVarSL_DbtCrdtNotesDetails.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarSL_DbtCrdtNotesDetails.mNotes = Convert.ToString(dr["Notes"].ToString());
                        lstCVarSL_DbtCrdtNotesDetails.Add(ObjCVarSL_DbtCrdtNotesDetails);
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
            lstCVarSL_DbtCrdtNotesDetails.Clear();

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
                Com.CommandText = "[dbo].GetListPagingSL_DbtCrdtNotesDetails";
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
                        CVarSL_DbtCrdtNotesDetails ObjCVarSL_DbtCrdtNotesDetails = new CVarSL_DbtCrdtNotesDetails();
                        ObjCVarSL_DbtCrdtNotesDetails.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarSL_DbtCrdtNotesDetails.mDbtCrdtNoteID = Convert.ToInt32(dr["DbtCrdtNoteID"].ToString());
                        ObjCVarSL_DbtCrdtNotesDetails.mServiceID = Convert.ToInt64(dr["ServiceID"].ToString());
                        ObjCVarSL_DbtCrdtNotesDetails.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarSL_DbtCrdtNotesDetails.mAmount = Convert.ToDecimal(dr["Amount"].ToString());
                        ObjCVarSL_DbtCrdtNotesDetails.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarSL_DbtCrdtNotesDetails.mNotes = Convert.ToString(dr["Notes"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarSL_DbtCrdtNotesDetails.Add(ObjCVarSL_DbtCrdtNotesDetails);
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
                    Com.CommandText = "[dbo].DeleteListSL_DbtCrdtNotesDetails";
                else
                    Com.CommandText = "[dbo].UpdateListSL_DbtCrdtNotesDetails";
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
        public Exception DeleteItem(List<CPKSL_DbtCrdtNotesDetails> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemSL_DbtCrdtNotesDetails";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                foreach (CPKSL_DbtCrdtNotesDetails ObjCPKSL_DbtCrdtNotesDetails in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt32(ObjCPKSL_DbtCrdtNotesDetails.ID);
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
        public Exception SaveMethod(List<CVarSL_DbtCrdtNotesDetails> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@DbtCrdtNoteID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ServiceID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@AccountID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@CurrencyID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarSL_DbtCrdtNotesDetails ObjCVarSL_DbtCrdtNotesDetails in SaveList)
                {
                    if (ObjCVarSL_DbtCrdtNotesDetails.mIsChanges == true)
                    {
                        if (ObjCVarSL_DbtCrdtNotesDetails.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemSL_DbtCrdtNotesDetails";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarSL_DbtCrdtNotesDetails.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemSL_DbtCrdtNotesDetails";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarSL_DbtCrdtNotesDetails.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarSL_DbtCrdtNotesDetails.ID;
                        }
                        Com.Parameters["@DbtCrdtNoteID"].Value = ObjCVarSL_DbtCrdtNotesDetails.DbtCrdtNoteID;
                        Com.Parameters["@ServiceID"].Value = ObjCVarSL_DbtCrdtNotesDetails.ServiceID;
                        Com.Parameters["@AccountID"].Value = ObjCVarSL_DbtCrdtNotesDetails.AccountID;
                        Com.Parameters["@Amount"].Value = ObjCVarSL_DbtCrdtNotesDetails.Amount;
                        Com.Parameters["@CurrencyID"].Value = ObjCVarSL_DbtCrdtNotesDetails.CurrencyID;
                        Com.Parameters["@Notes"].Value = ObjCVarSL_DbtCrdtNotesDetails.Notes;
                        EndTrans(Com, Con);
                        if (ObjCVarSL_DbtCrdtNotesDetails.ID == 0)
                        {
                            ObjCVarSL_DbtCrdtNotesDetails.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarSL_DbtCrdtNotesDetails.mIsChanges = false;
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
