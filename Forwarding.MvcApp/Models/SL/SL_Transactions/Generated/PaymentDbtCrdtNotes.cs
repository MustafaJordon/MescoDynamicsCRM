﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Controllers.SL.API_SL_Transactions
{
    [Serializable]
    public class CPKPaymentDbtCrdtNotes
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
    public partial class CVarPaymentDbtCrdtNotes : CPKPaymentDbtCrdtNotes
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Decimal mDbtCrdtNotesID;
        internal Int64 mPaymentID;
        internal Decimal mPaidAmount;
        #endregion

        #region "Methods"
        public Decimal DbtCrdtNotesID
        {
            get { return mDbtCrdtNotesID; }
            set { mIsChanges = true; mDbtCrdtNotesID = value; }
        }
        public Int64 PaymentID
        {
            get { return mPaymentID; }
            set { mIsChanges = true; mPaymentID = value; }
        }
        public Decimal PaidAmount
        {
            get { return mPaidAmount; }
            set { mIsChanges = true; mPaidAmount = value; }
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

    public partial class CPaymentDbtCrdtNotes
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
        public List<CVarPaymentDbtCrdtNotes> lstCVarPaymentDbtCrdtNotes = new List<CVarPaymentDbtCrdtNotes>();
        public List<CPKPaymentDbtCrdtNotes> lstDeletedCPKPaymentDbtCrdtNotes = new List<CPKPaymentDbtCrdtNotes>();
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
            lstCVarPaymentDbtCrdtNotes.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.VarChar));
                    Com.CommandText = "[dbo].GetListPaymentDbtCrdtNotes";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemPaymentDbtCrdtNotes";
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
                        CVarPaymentDbtCrdtNotes ObjCVarPaymentDbtCrdtNotes = new CVarPaymentDbtCrdtNotes();
                        ObjCVarPaymentDbtCrdtNotes.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarPaymentDbtCrdtNotes.mDbtCrdtNotesID = Convert.ToDecimal(dr["DbtCrdtNotesID"].ToString());
                        ObjCVarPaymentDbtCrdtNotes.mPaymentID = Convert.ToInt64(dr["PaymentID"].ToString());
                        ObjCVarPaymentDbtCrdtNotes.mPaidAmount = Convert.ToDecimal(dr["PaidAmount"].ToString());
                        lstCVarPaymentDbtCrdtNotes.Add(ObjCVarPaymentDbtCrdtNotes);
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
            lstCVarPaymentDbtCrdtNotes.Clear();

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
                Com.CommandText = "[dbo].GetListPagingPaymentDbtCrdtNotes";
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
                        CVarPaymentDbtCrdtNotes ObjCVarPaymentDbtCrdtNotes = new CVarPaymentDbtCrdtNotes();
                        ObjCVarPaymentDbtCrdtNotes.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarPaymentDbtCrdtNotes.mDbtCrdtNotesID = Convert.ToDecimal(dr["DbtCrdtNotesID"].ToString());
                        ObjCVarPaymentDbtCrdtNotes.mPaymentID = Convert.ToInt64(dr["PaymentID"].ToString());
                        ObjCVarPaymentDbtCrdtNotes.mPaidAmount = Convert.ToDecimal(dr["PaidAmount"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarPaymentDbtCrdtNotes.Add(ObjCVarPaymentDbtCrdtNotes);
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
                    Com.CommandText = "[dbo].DeleteListPaymentDbtCrdtNotes";
                else
                    Com.CommandText = "[dbo].UpdateListPaymentDbtCrdtNotes";
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
        public Exception DeleteItem(List<CPKPaymentDbtCrdtNotes> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemPaymentDbtCrdtNotes";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt));
                foreach (CPKPaymentDbtCrdtNotes ObjCPKPaymentDbtCrdtNotes in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt64(ObjCPKPaymentDbtCrdtNotes.ID);
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
        public Exception SaveMethod(List<CVarPaymentDbtCrdtNotes> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@DbtCrdtNotesID", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@PaymentID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@PaidAmount", SqlDbType.Decimal));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarPaymentDbtCrdtNotes ObjCVarPaymentDbtCrdtNotes in SaveList)
                {
                    if (ObjCVarPaymentDbtCrdtNotes.mIsChanges == true)
                    {
                        if (ObjCVarPaymentDbtCrdtNotes.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemPaymentDbtCrdtNotes";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarPaymentDbtCrdtNotes.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemPaymentDbtCrdtNotes";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarPaymentDbtCrdtNotes.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarPaymentDbtCrdtNotes.ID;
                        }
                        Com.Parameters["@DbtCrdtNotesID"].Value = ObjCVarPaymentDbtCrdtNotes.DbtCrdtNotesID;
                        Com.Parameters["@PaymentID"].Value = ObjCVarPaymentDbtCrdtNotes.PaymentID;
                        Com.Parameters["@PaidAmount"].Value = ObjCVarPaymentDbtCrdtNotes.PaidAmount;
                        EndTrans(Com, Con);
                        if (ObjCVarPaymentDbtCrdtNotes.ID == 0)
                        {
                            ObjCVarPaymentDbtCrdtNotes.ID = Convert.ToInt64(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarPaymentDbtCrdtNotes.mIsChanges = false;
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