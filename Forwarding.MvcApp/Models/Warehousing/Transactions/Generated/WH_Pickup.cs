﻿using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.Warehousing.Transactions.Generated
{
    [Serializable]
    public class CPKWH_Pickup
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
    public partial class CVarWH_Pickup : CPKWH_Pickup
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mCodeSerial;
        internal String mCode;
        internal Int32 mWarehouseID;
        internal Int32 mCustomerID;
        internal Int32 mBillTo;
        internal DateTime mOrderDate;
        internal DateTime mRequiredDate;
        internal Boolean mIsFinalized;
        internal DateTime mFinalizeDate;
        internal String mNotes;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Int64 mInvoiceID;
        internal Int64 mOperationID;
        internal Int32 mEndUserID;
        internal String mRMANumber;
        internal Int64 mPersonInChargeID;
        internal Int64 mPDIReceiveDetailsID;
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
        public Int32 BillTo
        {
            get { return mBillTo; }
            set { mIsChanges = true; mBillTo = value; }
        }
        public DateTime OrderDate
        {
            get { return mOrderDate; }
            set { mIsChanges = true; mOrderDate = value; }
        }
        public DateTime RequiredDate
        {
            get { return mRequiredDate; }
            set { mIsChanges = true; mRequiredDate = value; }
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
        public Int32 EndUserID
        {
            get { return mEndUserID; }
            set { mIsChanges = true; mEndUserID = value; }
        }
        public String RMANumber
        {
            get { return mRMANumber; }
            set { mIsChanges = true; mRMANumber = value; }
        }
        public Int64 PersonInChargeID
        {
            get { return mPersonInChargeID; }
            set { mIsChanges = true; mPersonInChargeID = value; }
        }
        public Int64 PDIReceiveDetailsID
        {
            get { return mPDIReceiveDetailsID; }
            set { mIsChanges = true; mPDIReceiveDetailsID = value; }
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

    public partial class CWH_Pickup
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
        public List<CVarWH_Pickup> lstCVarWH_Pickup = new List<CVarWH_Pickup>();
        public List<CPKWH_Pickup> lstDeletedCPKWH_Pickup = new List<CPKWH_Pickup>();
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
            lstCVarWH_Pickup.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListWH_Pickup";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemWH_Pickup";
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
                        CVarWH_Pickup ObjCVarWH_Pickup = new CVarWH_Pickup();
                        ObjCVarWH_Pickup.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarWH_Pickup.mCodeSerial = Convert.ToInt32(dr["CodeSerial"].ToString());
                        ObjCVarWH_Pickup.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarWH_Pickup.mWarehouseID = Convert.ToInt32(dr["WarehouseID"].ToString());
                        ObjCVarWH_Pickup.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarWH_Pickup.mBillTo = Convert.ToInt32(dr["BillTo"].ToString());
                        ObjCVarWH_Pickup.mOrderDate = Convert.ToDateTime(dr["OrderDate"].ToString());
                        ObjCVarWH_Pickup.mRequiredDate = Convert.ToDateTime(dr["RequiredDate"].ToString());
                        ObjCVarWH_Pickup.mIsFinalized = Convert.ToBoolean(dr["IsFinalized"].ToString());
                        ObjCVarWH_Pickup.mFinalizeDate = Convert.ToDateTime(dr["FinalizeDate"].ToString());
                        ObjCVarWH_Pickup.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarWH_Pickup.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarWH_Pickup.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarWH_Pickup.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarWH_Pickup.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarWH_Pickup.mInvoiceID = Convert.ToInt64(dr["InvoiceID"].ToString());
                        ObjCVarWH_Pickup.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarWH_Pickup.mEndUserID = Convert.ToInt32(dr["EndUserID"].ToString());
                        ObjCVarWH_Pickup.mRMANumber = Convert.ToString(dr["RMANumber"].ToString());
                        ObjCVarWH_Pickup.mPersonInChargeID = Convert.ToInt64(dr["PersonInChargeID"].ToString());
                        ObjCVarWH_Pickup.mPDIReceiveDetailsID = Convert.ToInt64(dr["PDIReceiveDetailsID"].ToString());
                        lstCVarWH_Pickup.Add(ObjCVarWH_Pickup);
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
            lstCVarWH_Pickup.Clear();

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
                Com.CommandText = "[dbo].GetListPagingWH_Pickup";
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
                        CVarWH_Pickup ObjCVarWH_Pickup = new CVarWH_Pickup();
                        ObjCVarWH_Pickup.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarWH_Pickup.mCodeSerial = Convert.ToInt32(dr["CodeSerial"].ToString());
                        ObjCVarWH_Pickup.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarWH_Pickup.mWarehouseID = Convert.ToInt32(dr["WarehouseID"].ToString());
                        ObjCVarWH_Pickup.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarWH_Pickup.mBillTo = Convert.ToInt32(dr["BillTo"].ToString());
                        ObjCVarWH_Pickup.mOrderDate = Convert.ToDateTime(dr["OrderDate"].ToString());
                        ObjCVarWH_Pickup.mRequiredDate = Convert.ToDateTime(dr["RequiredDate"].ToString());
                        ObjCVarWH_Pickup.mIsFinalized = Convert.ToBoolean(dr["IsFinalized"].ToString());
                        ObjCVarWH_Pickup.mFinalizeDate = Convert.ToDateTime(dr["FinalizeDate"].ToString());
                        ObjCVarWH_Pickup.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarWH_Pickup.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarWH_Pickup.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarWH_Pickup.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarWH_Pickup.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarWH_Pickup.mInvoiceID = Convert.ToInt64(dr["InvoiceID"].ToString());
                        ObjCVarWH_Pickup.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarWH_Pickup.mEndUserID = Convert.ToInt32(dr["EndUserID"].ToString());
                        ObjCVarWH_Pickup.mRMANumber = Convert.ToString(dr["RMANumber"].ToString());
                        ObjCVarWH_Pickup.mPersonInChargeID = Convert.ToInt64(dr["PersonInChargeID"].ToString());
                        ObjCVarWH_Pickup.mPDIReceiveDetailsID = Convert.ToInt64(dr["PDIReceiveDetailsID"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarWH_Pickup.Add(ObjCVarWH_Pickup);
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
                    Com.CommandText = "[dbo].DeleteListWH_Pickup";
                else
                    Com.CommandText = "[dbo].UpdateListWH_Pickup";
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
        public Exception DeleteItem(List<CPKWH_Pickup> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemWH_Pickup";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt));
                foreach (CPKWH_Pickup ObjCPKWH_Pickup in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt64(ObjCPKWH_Pickup.ID);
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
        public Exception SaveMethod(List<CVarWH_Pickup> SaveList)
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
                Com.Parameters.Add(new SqlParameter("@BillTo", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@OrderDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@RequiredDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@IsFinalized", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@FinalizeDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@CreatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ModificatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ModificationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@InvoiceID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@OperationID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@EndUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@RMANumber", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@PersonInChargeID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@PDIReceiveDetailsID", SqlDbType.BigInt));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarWH_Pickup ObjCVarWH_Pickup in SaveList)
                {
                    if (ObjCVarWH_Pickup.mIsChanges == true)
                    {
                        if (ObjCVarWH_Pickup.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemWH_Pickup";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarWH_Pickup.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemWH_Pickup";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarWH_Pickup.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarWH_Pickup.ID;
                        }
                        Com.Parameters["@CodeSerial"].Value = ObjCVarWH_Pickup.CodeSerial;
                        Com.Parameters["@Code"].Value = ObjCVarWH_Pickup.Code;
                        Com.Parameters["@WarehouseID"].Value = ObjCVarWH_Pickup.WarehouseID;
                        Com.Parameters["@CustomerID"].Value = ObjCVarWH_Pickup.CustomerID;
                        Com.Parameters["@BillTo"].Value = ObjCVarWH_Pickup.BillTo;
                        Com.Parameters["@OrderDate"].Value = ObjCVarWH_Pickup.OrderDate;
                        Com.Parameters["@RequiredDate"].Value = ObjCVarWH_Pickup.RequiredDate;
                        Com.Parameters["@IsFinalized"].Value = ObjCVarWH_Pickup.IsFinalized;
                        Com.Parameters["@FinalizeDate"].Value = ObjCVarWH_Pickup.FinalizeDate;
                        Com.Parameters["@Notes"].Value = ObjCVarWH_Pickup.Notes;
                        Com.Parameters["@CreatorUserID"].Value = ObjCVarWH_Pickup.CreatorUserID;
                        Com.Parameters["@CreationDate"].Value = ObjCVarWH_Pickup.CreationDate;
                        Com.Parameters["@ModificatorUserID"].Value = ObjCVarWH_Pickup.ModificatorUserID;
                        Com.Parameters["@ModificationDate"].Value = ObjCVarWH_Pickup.ModificationDate;
                        Com.Parameters["@InvoiceID"].Value = ObjCVarWH_Pickup.InvoiceID;
                        Com.Parameters["@OperationID"].Value = ObjCVarWH_Pickup.OperationID;
                        Com.Parameters["@EndUserID"].Value = ObjCVarWH_Pickup.EndUserID;
                        Com.Parameters["@RMANumber"].Value = ObjCVarWH_Pickup.RMANumber;
                        Com.Parameters["@PersonInChargeID"].Value = ObjCVarWH_Pickup.PersonInChargeID;
                        Com.Parameters["@PDIReceiveDetailsID"].Value = ObjCVarWH_Pickup.PDIReceiveDetailsID;
                        EndTrans(Com, Con);
                        if (ObjCVarWH_Pickup.ID == 0)
                        {
                            ObjCVarWH_Pickup.ID = Convert.ToInt64(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarWH_Pickup.mIsChanges = false;
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
