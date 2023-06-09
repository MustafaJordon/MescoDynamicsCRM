﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.Operations.Operations.Generated
{
    [Serializable]
    public class CPKOperationTracking
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
    public partial class CVarOperationTracking : CPKOperationTracking
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mOperationID;
        internal Int32 mTrackingStageID;
        internal DateTime mTrackingDate;
        internal String mNotes;
        internal Boolean mDone;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Int32 mCustodyID;
        internal Int64 mRoutingID;
        internal Boolean mIsAlarmed;
        internal DateTime mReleasingDate;
        internal DateTime mLoadingDate;
        internal String mPickupAddress;
        internal String mDeliveryAddress;
        internal String mOtherAddress;
        internal String mContactDetails;
        #endregion

        #region "Methods"
        public Int64 OperationID
        {
            get { return mOperationID; }
            set { mIsChanges = true; mOperationID = value; }
        }
        public Int32 TrackingStageID
        {
            get { return mTrackingStageID; }
            set { mIsChanges = true; mTrackingStageID = value; }
        }
        public DateTime TrackingDate
        {
            get { return mTrackingDate; }
            set { mIsChanges = true; mTrackingDate = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mIsChanges = true; mNotes = value; }
        }
        public Boolean Done
        {
            get { return mDone; }
            set { mIsChanges = true; mDone = value; }
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
        public Int32 CustodyID
        {
            get { return mCustodyID; }
            set { mIsChanges = true; mCustodyID = value; }
        }
        public Int64 RoutingID
        {
            get { return mRoutingID; }
            set { mIsChanges = true; mRoutingID = value; }
        }
        public Boolean IsAlarmed
        {
            get { return mIsAlarmed; }
            set { mIsChanges = true; mIsAlarmed = value; }
        }
        public DateTime ReleasingDate
        {
            get { return mReleasingDate; }
            set { mIsChanges = true; mReleasingDate = value; }
        }
        public DateTime LoadingDate
        {
            get { return mLoadingDate; }
            set { mIsChanges = true; mLoadingDate = value; }
        }
        public String PickupAddress
        {
            get { return mPickupAddress; }
            set { mIsChanges = true; mPickupAddress = value; }
        }
        public String DeliveryAddress
        {
            get { return mDeliveryAddress; }
            set { mIsChanges = true; mDeliveryAddress = value; }
        }
        public String OtherAddress
        {
            get { return mOtherAddress; }
            set { mIsChanges = true; mOtherAddress = value; }
        }
        public String ContactDetails
        {
            get { return mContactDetails; }
            set { mIsChanges = true; mContactDetails = value; }
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

    public partial class COperationTracking
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
        public List<CVarOperationTracking> lstCVarOperationTracking = new List<CVarOperationTracking>();
        public List<CPKOperationTracking> lstDeletedCPKOperationTracking = new List<CPKOperationTracking>();
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
            lstCVarOperationTracking.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListOperationTracking";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemOperationTracking";
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
                        CVarOperationTracking ObjCVarOperationTracking = new CVarOperationTracking();
                        ObjCVarOperationTracking.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarOperationTracking.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarOperationTracking.mTrackingStageID = Convert.ToInt32(dr["TrackingStageID"].ToString());
                        ObjCVarOperationTracking.mTrackingDate = Convert.ToDateTime(dr["TrackingDate"].ToString());
                        ObjCVarOperationTracking.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarOperationTracking.mDone = Convert.ToBoolean(dr["Done"].ToString());
                        ObjCVarOperationTracking.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarOperationTracking.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarOperationTracking.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarOperationTracking.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarOperationTracking.mCustodyID = Convert.ToInt32(dr["CustodyID"].ToString());
                        ObjCVarOperationTracking.mRoutingID = Convert.ToInt64(dr["RoutingID"].ToString());
                        ObjCVarOperationTracking.mIsAlarmed = Convert.ToBoolean(dr["IsAlarmed"].ToString());
                        ObjCVarOperationTracking.mReleasingDate = Convert.ToDateTime(dr["ReleasingDate"].ToString());
                        ObjCVarOperationTracking.mLoadingDate = Convert.ToDateTime(dr["LoadingDate"].ToString());
                        ObjCVarOperationTracking.mPickupAddress = Convert.ToString(dr["PickupAddress"].ToString());
                        ObjCVarOperationTracking.mDeliveryAddress = Convert.ToString(dr["DeliveryAddress"].ToString());
                        ObjCVarOperationTracking.mOtherAddress = Convert.ToString(dr["OtherAddress"].ToString());
                        ObjCVarOperationTracking.mContactDetails = Convert.ToString(dr["ContactDetails"].ToString());
                        lstCVarOperationTracking.Add(ObjCVarOperationTracking);
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
            lstCVarOperationTracking.Clear();

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
                Com.CommandText = "[dbo].GetListPagingOperationTracking";
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
                        CVarOperationTracking ObjCVarOperationTracking = new CVarOperationTracking();
                        ObjCVarOperationTracking.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarOperationTracking.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarOperationTracking.mTrackingStageID = Convert.ToInt32(dr["TrackingStageID"].ToString());
                        ObjCVarOperationTracking.mTrackingDate = Convert.ToDateTime(dr["TrackingDate"].ToString());
                        ObjCVarOperationTracking.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarOperationTracking.mDone = Convert.ToBoolean(dr["Done"].ToString());
                        ObjCVarOperationTracking.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarOperationTracking.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarOperationTracking.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarOperationTracking.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarOperationTracking.mCustodyID = Convert.ToInt32(dr["CustodyID"].ToString());
                        ObjCVarOperationTracking.mRoutingID = Convert.ToInt64(dr["RoutingID"].ToString());
                        ObjCVarOperationTracking.mIsAlarmed = Convert.ToBoolean(dr["IsAlarmed"].ToString());
                        ObjCVarOperationTracking.mReleasingDate = Convert.ToDateTime(dr["ReleasingDate"].ToString());
                        ObjCVarOperationTracking.mLoadingDate = Convert.ToDateTime(dr["LoadingDate"].ToString());
                        ObjCVarOperationTracking.mPickupAddress = Convert.ToString(dr["PickupAddress"].ToString());
                        ObjCVarOperationTracking.mDeliveryAddress = Convert.ToString(dr["DeliveryAddress"].ToString());
                        ObjCVarOperationTracking.mOtherAddress = Convert.ToString(dr["OtherAddress"].ToString());
                        ObjCVarOperationTracking.mContactDetails = Convert.ToString(dr["ContactDetails"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarOperationTracking.Add(ObjCVarOperationTracking);
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
                    Com.CommandText = "[dbo].DeleteListOperationTracking";
                else
                    Com.CommandText = "[dbo].UpdateListOperationTracking";
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
        public Exception DeleteItem(List<CPKOperationTracking> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemOperationTracking";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt));
                foreach (CPKOperationTracking ObjCPKOperationTracking in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt64(ObjCPKOperationTracking.ID);
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
        public Exception SaveMethod(List<CVarOperationTracking> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@OperationID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@TrackingStageID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@TrackingDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Done", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@CreatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ModificatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ModificationDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@CustodyID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@RoutingID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@IsAlarmed", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@ReleasingDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@LoadingDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@PickupAddress", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@DeliveryAddress", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@OtherAddress", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@ContactDetails", SqlDbType.NVarChar));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarOperationTracking ObjCVarOperationTracking in SaveList)
                {
                    if (ObjCVarOperationTracking.mIsChanges == true)
                    {
                        if (ObjCVarOperationTracking.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemOperationTracking";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarOperationTracking.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemOperationTracking";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarOperationTracking.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarOperationTracking.ID;
                        }
                        Com.Parameters["@OperationID"].Value = ObjCVarOperationTracking.OperationID;
                        Com.Parameters["@TrackingStageID"].Value = ObjCVarOperationTracking.TrackingStageID;
                        Com.Parameters["@TrackingDate"].Value = ObjCVarOperationTracking.TrackingDate;
                        Com.Parameters["@Notes"].Value = ObjCVarOperationTracking.Notes;
                        Com.Parameters["@Done"].Value = ObjCVarOperationTracking.Done;
                        Com.Parameters["@CreatorUserID"].Value = ObjCVarOperationTracking.CreatorUserID;
                        Com.Parameters["@CreationDate"].Value = ObjCVarOperationTracking.CreationDate;
                        Com.Parameters["@ModificatorUserID"].Value = ObjCVarOperationTracking.ModificatorUserID;
                        Com.Parameters["@ModificationDate"].Value = ObjCVarOperationTracking.ModificationDate;
                        Com.Parameters["@CustodyID"].Value = ObjCVarOperationTracking.CustodyID;
                        Com.Parameters["@RoutingID"].Value = ObjCVarOperationTracking.RoutingID;
                        Com.Parameters["@IsAlarmed"].Value = ObjCVarOperationTracking.IsAlarmed;
                        Com.Parameters["@ReleasingDate"].Value = ObjCVarOperationTracking.ReleasingDate;
                        Com.Parameters["@LoadingDate"].Value = ObjCVarOperationTracking.LoadingDate;
                        Com.Parameters["@PickupAddress"].Value = ObjCVarOperationTracking.PickupAddress;
                        Com.Parameters["@DeliveryAddress"].Value = ObjCVarOperationTracking.DeliveryAddress;
                        Com.Parameters["@OtherAddress"].Value = ObjCVarOperationTracking.OtherAddress;
                        Com.Parameters["@ContactDetails"].Value = ObjCVarOperationTracking.ContactDetails;
                        EndTrans(Com, Con);
                        if (ObjCVarOperationTracking.ID == 0)
                        {
                            ObjCVarOperationTracking.ID = Convert.ToInt64(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarOperationTracking.mIsChanges = false;
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
