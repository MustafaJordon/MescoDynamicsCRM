﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using Forwarding.MvcApp.Models.Sales.Transactions.Generated.Payments.Generated;

namespace Forwarding.MvcApp.Models.Accounting.Transactions.Generated
{
    [Serializable]
    public class CPKA_JV
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
    public partial class CVarA_JV : CPKA_JV
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mJVNo;
        internal DateTime mJVDate;
        internal Decimal mTotalDebit;
        internal Decimal mTotalCredit;
        internal Int32 mJournal_ID;
        internal Int32 mJVType_ID;
        internal String mReceiptNo;
        internal String mRemarksHeader;
        internal Boolean mDeleted;
        internal Boolean mPosted;
        internal Int32 mUser_ID;
        internal Boolean mIsSysJv;
        internal Int32 mTransType_ID;
        #endregion

        #region "Methods"
        public String JVNo
        {
            get { return mJVNo; }
            set { mIsChanges = true; mJVNo = value; }
        }
        public DateTime JVDate
        {
            get { return mJVDate; }
            set { mIsChanges = true; mJVDate = value; }
        }
        public Decimal TotalDebit
        {
            get { return mTotalDebit; }
            set { mIsChanges = true; mTotalDebit = value; }
        }
        public Decimal TotalCredit
        {
            get { return mTotalCredit; }
            set { mIsChanges = true; mTotalCredit = value; }
        }
        public Int32 Journal_ID
        {
            get { return mJournal_ID; }
            set { mIsChanges = true; mJournal_ID = value; }
        }
        public Int32 JVType_ID
        {
            get { return mJVType_ID; }
            set { mIsChanges = true; mJVType_ID = value; }
        }
        public String ReceiptNo
        {
            get { return mReceiptNo; }
            set { mIsChanges = true; mReceiptNo = value; }
        }
        public String RemarksHeader
        {
            get { return mRemarksHeader; }
            set { mIsChanges = true; mRemarksHeader = value; }
        }
        public Boolean Deleted
        {
            get { return mDeleted; }
            set { mIsChanges = true; mDeleted = value; }
        }
        public Boolean Posted
        {
            get { return mPosted; }
            set { mIsChanges = true; mPosted = value; }
        }
        public Int32 User_ID
        {
            get { return mUser_ID; }
            set { mIsChanges = true; mUser_ID = value; }
        }
        public Boolean IsSysJv
        {
            get { return mIsSysJv; }
            set { mIsChanges = true; mIsSysJv = value; }
        }
        public Int32 TransType_ID
        {
            get { return mTransType_ID; }
            set { mIsChanges = true; mTransType_ID = value; }
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

    public partial class CA_JV
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
        public List<CVarA_JV> lstCVarA_JV = new List<CVarA_JV>();
        public List<CPKA_JV> lstDeletedCPKA_JV = new List<CPKA_JV>();
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
            lstCVarA_JV.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListA_JV";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemA_JV";
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
                        CVarA_JV ObjCVarA_JV = new CVarA_JV();
                        ObjCVarA_JV.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarA_JV.mJVNo = Convert.ToString(dr["JVNo"].ToString());
                        ObjCVarA_JV.mJVDate = Convert.ToDateTime(dr["JVDate"].ToString());
                        ObjCVarA_JV.mTotalDebit = Convert.ToDecimal(dr["TotalDebit"].ToString());
                        ObjCVarA_JV.mTotalCredit = Convert.ToDecimal(dr["TotalCredit"].ToString());
                        ObjCVarA_JV.mJournal_ID = Convert.ToInt32(dr["Journal_ID"].ToString());
                        ObjCVarA_JV.mJVType_ID = Convert.ToInt32(dr["JVType_ID"].ToString());
                        ObjCVarA_JV.mReceiptNo = Convert.ToString(dr["ReceiptNo"].ToString());
                        ObjCVarA_JV.mRemarksHeader = Convert.ToString(dr["RemarksHeader"].ToString());
                        ObjCVarA_JV.mDeleted = Convert.ToBoolean(dr["Deleted"].ToString());
                        ObjCVarA_JV.mPosted = Convert.ToBoolean(dr["Posted"].ToString());
                        ObjCVarA_JV.mUser_ID = Convert.ToInt32(dr["User_ID"].ToString());
                        ObjCVarA_JV.mIsSysJv = Convert.ToBoolean(dr["IsSysJv"].ToString());
                        ObjCVarA_JV.mTransType_ID = Convert.ToInt32(dr["TransType_ID"].ToString());
                        lstCVarA_JV.Add(ObjCVarA_JV);
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
            lstCVarA_JV.Clear();

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
                Com.CommandText = "[dbo].GetListPagingA_JV";
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
                        CVarA_JV ObjCVarA_JV = new CVarA_JV();
                        ObjCVarA_JV.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarA_JV.mJVNo = Convert.ToString(dr["JVNo"].ToString());
                        ObjCVarA_JV.mJVDate = Convert.ToDateTime(dr["JVDate"].ToString());
                        ObjCVarA_JV.mTotalDebit = Convert.ToDecimal(dr["TotalDebit"].ToString());
                        ObjCVarA_JV.mTotalCredit = Convert.ToDecimal(dr["TotalCredit"].ToString());
                        ObjCVarA_JV.mJournal_ID = Convert.ToInt32(dr["Journal_ID"].ToString());
                        ObjCVarA_JV.mJVType_ID = Convert.ToInt32(dr["JVType_ID"].ToString());
                        ObjCVarA_JV.mReceiptNo = Convert.ToString(dr["ReceiptNo"].ToString());
                        ObjCVarA_JV.mRemarksHeader = Convert.ToString(dr["RemarksHeader"].ToString());
                        ObjCVarA_JV.mDeleted = Convert.ToBoolean(dr["Deleted"].ToString());
                        ObjCVarA_JV.mPosted = Convert.ToBoolean(dr["Posted"].ToString());
                        ObjCVarA_JV.mUser_ID = Convert.ToInt32(dr["User_ID"].ToString());
                        ObjCVarA_JV.mIsSysJv = Convert.ToBoolean(dr["IsSysJv"].ToString());
                        ObjCVarA_JV.mTransType_ID = Convert.ToInt32(dr["TransType_ID"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarA_JV.Add(ObjCVarA_JV);
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
                    Com.CommandText = "[dbo].DeleteListA_JV";
                else
                    Com.CommandText = "[dbo].UpdateListA_JV";
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
        public Exception DeleteItem(List<CPKA_JV> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemA_JV";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt));
                foreach (CPKA_JV ObjCPKA_JV in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt64(ObjCPKA_JV.ID);
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
        public Exception SaveMethod(List<CVarA_JV> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@JVNo", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@JVDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@TotalDebit", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@TotalCredit", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@Journal_ID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@JVType_ID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ReceiptNo", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@RemarksHeader", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Deleted", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@Posted", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@User_ID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@IsSysJv", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@TransType_ID", SqlDbType.Int));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarA_JV ObjCVarA_JV in SaveList)
                {
                    if (ObjCVarA_JV.mIsChanges == true)
                    {
                        if (ObjCVarA_JV.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemA_JV";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarA_JV.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemA_JV";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarA_JV.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarA_JV.ID;
                        }
                        Com.Parameters["@JVNo"].Value = ObjCVarA_JV.JVNo;
                        Com.Parameters["@JVDate"].Value = ObjCVarA_JV.JVDate;
                        Com.Parameters["@TotalDebit"].Value = ObjCVarA_JV.TotalDebit;
                        Com.Parameters["@TotalCredit"].Value = ObjCVarA_JV.TotalCredit;
                        Com.Parameters["@Journal_ID"].Value = ObjCVarA_JV.Journal_ID;
                        Com.Parameters["@JVType_ID"].Value = ObjCVarA_JV.JVType_ID;
                        Com.Parameters["@ReceiptNo"].Value = ObjCVarA_JV.ReceiptNo;
                        Com.Parameters["@RemarksHeader"].Value = ObjCVarA_JV.RemarksHeader;
                        Com.Parameters["@Deleted"].Value = ObjCVarA_JV.Deleted;
                        Com.Parameters["@Posted"].Value = ObjCVarA_JV.Posted;
                        Com.Parameters["@User_ID"].Value = ObjCVarA_JV.User_ID;
                        Com.Parameters["@IsSysJv"].Value = ObjCVarA_JV.IsSysJv;
                        Com.Parameters["@TransType_ID"].Value = ObjCVarA_JV.TransType_ID;
                        EndTrans(Com, Con);
                        if (ObjCVarA_JV.ID == 0)
                        {
                            ObjCVarA_JV.ID = Convert.ToInt64(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarA_JV.mIsChanges = false;
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



        public Exception A_JVSaveMethod(List<CVarA_JV> SaveList)
        {
            Exception Exp = null;
            //SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;// = GlobalConnection.GlobalCom;
            try
            {
                GlobalConnection.myCommand = new SqlCommand();
                GlobalConnection.myCommand.Connection = GlobalConnection.myConnection;
                GlobalConnection.myCommand.Transaction = GlobalConnection.myTrans;

                GlobalConnection.myCommand.CommandType = CommandType.StoredProcedure;
                GlobalConnection.myCommand.CommandTimeout = 0;
                Com = GlobalConnection.myCommand;
                //Con.Open();
                //Com = new SqlCommand();
                //GlobalConnection.GlobalCon.Open();

                Com.Parameters.Add(new SqlParameter("@JVNo", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@JVDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@TotalDebit", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@TotalCredit", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@Journal_ID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@JVType_ID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ReceiptNo", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@RemarksHeader", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Deleted", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@Posted", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@User_ID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@IsSysJv", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@TransType_ID", SqlDbType.Int));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarA_JV ObjCVarA_JV in SaveList)
                {
                    if (ObjCVarA_JV.mIsChanges == true)
                    {
                        if (ObjCVarA_JV.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemA_JV";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarA_JV.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemA_JV";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        //BeginTrans(Com, Con);
                        if (ObjCVarA_JV.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarA_JV.ID;
                        }
                        Com.Parameters["@JVNo"].Value = ObjCVarA_JV.JVNo;
                        Com.Parameters["@JVDate"].Value = ObjCVarA_JV.JVDate;
                        Com.Parameters["@TotalDebit"].Value = ObjCVarA_JV.TotalDebit;
                        Com.Parameters["@TotalCredit"].Value = ObjCVarA_JV.TotalCredit;
                        Com.Parameters["@Journal_ID"].Value = ObjCVarA_JV.Journal_ID;
                        Com.Parameters["@JVType_ID"].Value = ObjCVarA_JV.JVType_ID;
                        Com.Parameters["@ReceiptNo"].Value = ObjCVarA_JV.ReceiptNo;
                        Com.Parameters["@RemarksHeader"].Value = ObjCVarA_JV.RemarksHeader;
                        Com.Parameters["@Deleted"].Value = ObjCVarA_JV.Deleted;
                        Com.Parameters["@Posted"].Value = ObjCVarA_JV.Posted;
                        Com.Parameters["@User_ID"].Value = ObjCVarA_JV.User_ID;
                        Com.Parameters["@IsSysJv"].Value = ObjCVarA_JV.IsSysJv;
                        Com.Parameters["@TransType_ID"].Value = ObjCVarA_JV.TransType_ID;
                        //EndTrans(Com, Con);
                        Com.ExecuteNonQuery();
                        if (ObjCVarA_JV.ID == 0)
                        {
                            ObjCVarA_JV.ID = Convert.ToInt64(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarA_JV.mIsChanges = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Exp = ex;
                //if (tr != null)
                //tr.Rollback();
            }
            finally
            {
                //Con.Close();
                //Con.Dispose();
            }
            return Exp;
        }

    }
}
