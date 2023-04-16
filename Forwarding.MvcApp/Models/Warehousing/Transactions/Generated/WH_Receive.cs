﻿using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.Warehousing.Transactions.Generated.Old
{
    [Serializable]
    public class CPKWH_Receive
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
    public partial class CVarWH_Receive : CPKWH_Receive
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mCodeSerial;
        internal String mCode;
        internal Int32 mWarehouseID;
        internal Int32 mCustomerID;
        internal DateTime mReceiveDate;
        internal DateTime mETD;
        internal DateTime mETA;
        internal DateTime mArrivalDate;
        internal Int32 mStatusID;
        internal Boolean mIsFinalized;
        internal DateTime mFinalizeDate;
        internal String mNotes;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Int64 mInvoiceID;
        internal Int64 mOperationID;
        internal Boolean mIsReturn;
        #endregion

        #region "Methods"
        public Int32 CodeSerial
        {
            get { return mCodeSerial; }
            set { mIsChanges = true; mCodeSerial = value; }
        }
        public String Code
        {
            get { return mCode; }
            set { mIsChanges = true; mCode = value; }
        }
        public Int32 WarehouseID
        {
            get { return mWarehouseID; }
            set { mIsChanges = true; mWarehouseID = value; }
        }
        public Int32 CustomerID
        {
            get { return mCustomerID; }
            set { mIsChanges = true; mCustomerID = value; }
        }
        public DateTime ReceiveDate
        {
            get { return mReceiveDate; }
            set { mIsChanges = true; mReceiveDate = value; }
        }
        public DateTime ETD
        {
            get { return mETD; }
            set { mIsChanges = true; mETD = value; }
        }
        public DateTime ETA
        {
            get { return mETA; }
            set { mIsChanges = true; mETA = value; }
        }
        public DateTime ArrivalDate
        {
            get { return mArrivalDate; }
            set { mIsChanges = true; mArrivalDate = value; }
        }
        public Int32 StatusID
        {
            get { return mStatusID; }
            set { mIsChanges = true; mStatusID = value; }
        }
        public Boolean IsFinalized
        {
            get { return mIsFinalized; }
            set { mIsChanges = true; mIsFinalized = value; }
        }
        public DateTime FinalizeDate
        {
            get { return mFinalizeDate; }
            set { mIsChanges = true; mFinalizeDate = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mIsChanges = true; mNotes = value; }
        }
        public Int32 CreatorUserID
        {
            get { return mCreatorUserID; }
            set { mIsChanges = true; mCreatorUserID = value; }
        }
        public DateTime CreationDate
        {
            get { return mCreationDate; }
            set { mIsChanges = true; mCreationDate = value; }
        }
        public Int32 ModificatorUserID
        {
            get { return mModificatorUserID; }
            set { mIsChanges = true; mModificatorUserID = value; }
        }
        public DateTime ModificationDate
        {
            get { return mModificationDate; }
            set { mIsChanges = true; mModificationDate = value; }
        }
        public Int64 InvoiceID
        {
            get { return mInvoiceID; }
            set { mIsChanges = true; mInvoiceID = value; }
        }
        public Int64 OperationID
        {
            get { return mOperationID; }
            set { mIsChanges = true; mOperationID = value; }
        }
        public Boolean IsReturn
        {
            get { return mIsReturn; }
            set { mIsChanges = true; mIsReturn = value; }
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

    public partial class CWH_Receive
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
        public List<CVarWH_Receive> lstCVarWH_Receive = new List<CVarWH_Receive>();
        public List<CPKWH_Receive> lstDeletedCPKWH_Receive = new List<CPKWH_Receive>();
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
            lstCVarWH_Receive.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListWH_Receive";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemWH_Receive";
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
                        CVarWH_Receive ObjCVarWH_Receive = new CVarWH_Receive();
                        ObjCVarWH_Receive.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarWH_Receive.mCodeSerial = Convert.ToInt32(dr["CodeSerial"].ToString());
                        ObjCVarWH_Receive.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarWH_Receive.mWarehouseID = Convert.ToInt32(dr["WarehouseID"].ToString());
                        ObjCVarWH_Receive.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarWH_Receive.mReceiveDate = Convert.ToDateTime(dr["ReceiveDate"].ToString());
                        ObjCVarWH_Receive.mETD = Convert.ToDateTime(dr["ETD"].ToString());
                        ObjCVarWH_Receive.mETA = Convert.ToDateTime(dr["ETA"].ToString());
                        ObjCVarWH_Receive.mArrivalDate = Convert.ToDateTime(dr["ArrivalDate"].ToString());
                        ObjCVarWH_Receive.mStatusID = Convert.ToInt32(dr["StatusID"].ToString());
                        ObjCVarWH_Receive.mIsFinalized = Convert.ToBoolean(dr["IsFinalized"].ToString());
                        ObjCVarWH_Receive.mFinalizeDate = Convert.ToDateTime(dr["FinalizeDate"].ToString());
                        ObjCVarWH_Receive.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarWH_Receive.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarWH_Receive.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarWH_Receive.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarWH_Receive.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarWH_Receive.mInvoiceID = Convert.ToInt64(dr["InvoiceID"].ToString());
                        ObjCVarWH_Receive.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarWH_Receive.mIsReturn = Convert.ToBoolean(dr["IsReturn"].ToString());
                        lstCVarWH_Receive.Add(ObjCVarWH_Receive);
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
            lstCVarWH_Receive.Clear();

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
                Com.CommandText = "[dbo].GetListPagingWH_Receive";
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
                        CVarWH_Receive ObjCVarWH_Receive = new CVarWH_Receive();
                        ObjCVarWH_Receive.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarWH_Receive.mCodeSerial = Convert.ToInt32(dr["CodeSerial"].ToString());
                        ObjCVarWH_Receive.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarWH_Receive.mWarehouseID = Convert.ToInt32(dr["WarehouseID"].ToString());
                        ObjCVarWH_Receive.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarWH_Receive.mReceiveDate = Convert.ToDateTime(dr["ReceiveDate"].ToString());
                        ObjCVarWH_Receive.mETD = Convert.ToDateTime(dr["ETD"].ToString());
                        ObjCVarWH_Receive.mETA = Convert.ToDateTime(dr["ETA"].ToString());
                        ObjCVarWH_Receive.mArrivalDate = Convert.ToDateTime(dr["ArrivalDate"].ToString());
                        ObjCVarWH_Receive.mStatusID = Convert.ToInt32(dr["StatusID"].ToString());
                        ObjCVarWH_Receive.mIsFinalized = Convert.ToBoolean(dr["IsFinalized"].ToString());
                        ObjCVarWH_Receive.mFinalizeDate = Convert.ToDateTime(dr["FinalizeDate"].ToString());
                        ObjCVarWH_Receive.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarWH_Receive.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarWH_Receive.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarWH_Receive.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarWH_Receive.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarWH_Receive.mInvoiceID = Convert.ToInt64(dr["InvoiceID"].ToString());
                        ObjCVarWH_Receive.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarWH_Receive.mIsReturn = Convert.ToBoolean(dr["IsReturn"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarWH_Receive.Add(ObjCVarWH_Receive);
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
                    Com.CommandText = "[dbo].DeleteListWH_Receive";
                else
                    Com.CommandText = "[dbo].UpdateListWH_Receive";
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
        public Exception DeleteItem(List<CPKWH_Receive> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemWH_Receive";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt));
                foreach (CPKWH_Receive ObjCPKWH_Receive in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt64(ObjCPKWH_Receive.ID);
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
        public Exception SaveMethod(List<CVarWH_Receive> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@CodeSerial", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@Code", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@WarehouseID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ReceiveDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ETD", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ETA", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ArrivalDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@StatusID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@IsFinalized", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@FinalizeDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@CreatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ModificatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ModificationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@InvoiceID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@OperationID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@IsReturn", SqlDbType.Bit));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarWH_Receive ObjCVarWH_Receive in SaveList)
                {
                    if (ObjCVarWH_Receive.mIsChanges == true)
                    {
                        if (ObjCVarWH_Receive.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemWH_Receive";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarWH_Receive.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemWH_Receive";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarWH_Receive.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarWH_Receive.ID;
                        }
                        Com.Parameters["@CodeSerial"].Value = ObjCVarWH_Receive.CodeSerial;
                        Com.Parameters["@Code"].Value = ObjCVarWH_Receive.Code;
                        Com.Parameters["@WarehouseID"].Value = ObjCVarWH_Receive.WarehouseID;
                        Com.Parameters["@CustomerID"].Value = ObjCVarWH_Receive.CustomerID;
                        Com.Parameters["@ReceiveDate"].Value = ObjCVarWH_Receive.ReceiveDate;
                        Com.Parameters["@ETD"].Value = ObjCVarWH_Receive.ETD;
                        Com.Parameters["@ETA"].Value = ObjCVarWH_Receive.ETA;
                        Com.Parameters["@ArrivalDate"].Value = ObjCVarWH_Receive.ArrivalDate;
                        Com.Parameters["@StatusID"].Value = ObjCVarWH_Receive.StatusID;
                        Com.Parameters["@IsFinalized"].Value = ObjCVarWH_Receive.IsFinalized;
                        Com.Parameters["@FinalizeDate"].Value = ObjCVarWH_Receive.FinalizeDate;
                        Com.Parameters["@Notes"].Value = ObjCVarWH_Receive.Notes;
                        Com.Parameters["@CreatorUserID"].Value = ObjCVarWH_Receive.CreatorUserID;
                        Com.Parameters["@CreationDate"].Value = ObjCVarWH_Receive.CreationDate;
                        Com.Parameters["@ModificatorUserID"].Value = ObjCVarWH_Receive.ModificatorUserID;
                        Com.Parameters["@ModificationDate"].Value = ObjCVarWH_Receive.ModificationDate;
                        Com.Parameters["@InvoiceID"].Value = ObjCVarWH_Receive.InvoiceID;
                        Com.Parameters["@OperationID"].Value = ObjCVarWH_Receive.OperationID;
                        Com.Parameters["@IsReturn"].Value = ObjCVarWH_Receive.IsReturn;
                        EndTrans(Com, Con);
                        if (ObjCVarWH_Receive.ID == 0)
                        {
                            ObjCVarWH_Receive.ID = Convert.ToInt64(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarWH_Receive.mIsChanges = false;
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
