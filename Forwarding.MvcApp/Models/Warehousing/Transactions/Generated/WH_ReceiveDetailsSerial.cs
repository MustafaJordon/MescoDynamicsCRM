﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.Warehousing.Transactions.Generated
{
    [Serializable]
    public class CPKWH_ReceiveDetailsSerial
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
    public partial class CVarWH_ReceiveDetailsSerial : CPKWH_ReceiveDetailsSerial
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mReceiveDetailsID;
        internal String mSerial;
        internal Int64 mPickupDetailsLocationID;
        internal String mNotes;
        internal String mVehicle;
        internal String mMotorNo;
        internal String mLotNumber;
        #endregion

        #region "Methods"
        public Int64 ReceiveDetailsID
        {
            get { return mReceiveDetailsID; }
            set { mIsChanges = true; mReceiveDetailsID = value; }
        }
        public String Serial
        {
            get { return mSerial; }
            set { mIsChanges = true; mSerial = value; }
        }
        public Int64 PickupDetailsLocationID
        {
            get { return mPickupDetailsLocationID; }
            set { mIsChanges = true; mPickupDetailsLocationID = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mIsChanges = true; mNotes = value; }
        }
        public String Vehicle
        {
            get { return mVehicle; }
            set { mIsChanges = true; mVehicle = value; }
        }
        public String MotorNo
        {
            get { return mMotorNo; }
            set { mIsChanges = true; mMotorNo = value; }
        }
        public String LotNumber
        {
            get { return mLotNumber; }
            set { mIsChanges = true; mLotNumber = value; }
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

    public partial class CWH_ReceiveDetailsSerial
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
        public List<CVarWH_ReceiveDetailsSerial> lstCVarWH_ReceiveDetailsSerial = new List<CVarWH_ReceiveDetailsSerial>();
        public List<CPKWH_ReceiveDetailsSerial> lstDeletedCPKWH_ReceiveDetailsSerial = new List<CPKWH_ReceiveDetailsSerial>();
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
            lstCVarWH_ReceiveDetailsSerial.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListWH_ReceiveDetailsSerial";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemWH_ReceiveDetailsSerial";
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
                        CVarWH_ReceiveDetailsSerial ObjCVarWH_ReceiveDetailsSerial = new CVarWH_ReceiveDetailsSerial();
                        ObjCVarWH_ReceiveDetailsSerial.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarWH_ReceiveDetailsSerial.mReceiveDetailsID = Convert.ToInt64(dr["ReceiveDetailsID"].ToString());
                        ObjCVarWH_ReceiveDetailsSerial.mSerial = Convert.ToString(dr["Serial"].ToString());
                        ObjCVarWH_ReceiveDetailsSerial.mPickupDetailsLocationID = Convert.ToInt64(dr["PickupDetailsLocationID"].ToString());
                        ObjCVarWH_ReceiveDetailsSerial.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarWH_ReceiveDetailsSerial.mVehicle = Convert.ToString(dr["Vehicle"].ToString());
                        ObjCVarWH_ReceiveDetailsSerial.mMotorNo = Convert.ToString(dr["MotorNo"].ToString());
                        ObjCVarWH_ReceiveDetailsSerial.mLotNumber = Convert.ToString(dr["LotNumber"].ToString());
                        lstCVarWH_ReceiveDetailsSerial.Add(ObjCVarWH_ReceiveDetailsSerial);
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
            lstCVarWH_ReceiveDetailsSerial.Clear();

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
                Com.CommandText = "[dbo].GetListPagingWH_ReceiveDetailsSerial";
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
                        CVarWH_ReceiveDetailsSerial ObjCVarWH_ReceiveDetailsSerial = new CVarWH_ReceiveDetailsSerial();
                        ObjCVarWH_ReceiveDetailsSerial.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarWH_ReceiveDetailsSerial.mReceiveDetailsID = Convert.ToInt64(dr["ReceiveDetailsID"].ToString());
                        ObjCVarWH_ReceiveDetailsSerial.mSerial = Convert.ToString(dr["Serial"].ToString());
                        ObjCVarWH_ReceiveDetailsSerial.mPickupDetailsLocationID = Convert.ToInt64(dr["PickupDetailsLocationID"].ToString());
                        ObjCVarWH_ReceiveDetailsSerial.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarWH_ReceiveDetailsSerial.mVehicle = Convert.ToString(dr["Vehicle"].ToString());
                        ObjCVarWH_ReceiveDetailsSerial.mMotorNo = Convert.ToString(dr["MotorNo"].ToString());
                        ObjCVarWH_ReceiveDetailsSerial.mLotNumber = Convert.ToString(dr["LotNumber"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarWH_ReceiveDetailsSerial.Add(ObjCVarWH_ReceiveDetailsSerial);
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
                    Com.CommandText = "[dbo].DeleteListWH_ReceiveDetailsSerial";
                else
                    Com.CommandText = "[dbo].UpdateListWH_ReceiveDetailsSerial";
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
        public Exception DeleteItem(List<CPKWH_ReceiveDetailsSerial> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemWH_ReceiveDetailsSerial";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt));
                foreach (CPKWH_ReceiveDetailsSerial ObjCPKWH_ReceiveDetailsSerial in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt64(ObjCPKWH_ReceiveDetailsSerial.ID);
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
        public Exception SaveMethod(List<CVarWH_ReceiveDetailsSerial> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@ReceiveDetailsID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@Serial", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@PickupDetailsLocationID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Vehicle", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@MotorNo", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@LotNumber", SqlDbType.NVarChar));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarWH_ReceiveDetailsSerial ObjCVarWH_ReceiveDetailsSerial in SaveList)
                {
                    if (ObjCVarWH_ReceiveDetailsSerial.mIsChanges == true)
                    {
                        if (ObjCVarWH_ReceiveDetailsSerial.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemWH_ReceiveDetailsSerial";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarWH_ReceiveDetailsSerial.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemWH_ReceiveDetailsSerial";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarWH_ReceiveDetailsSerial.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarWH_ReceiveDetailsSerial.ID;
                        }
                        Com.Parameters["@ReceiveDetailsID"].Value = ObjCVarWH_ReceiveDetailsSerial.ReceiveDetailsID;
                        Com.Parameters["@Serial"].Value = ObjCVarWH_ReceiveDetailsSerial.Serial;
                        Com.Parameters["@PickupDetailsLocationID"].Value = ObjCVarWH_ReceiveDetailsSerial.PickupDetailsLocationID;
                        Com.Parameters["@Notes"].Value = ObjCVarWH_ReceiveDetailsSerial.Notes;
                        Com.Parameters["@Vehicle"].Value = ObjCVarWH_ReceiveDetailsSerial.Vehicle;
                        Com.Parameters["@MotorNo"].Value = ObjCVarWH_ReceiveDetailsSerial.MotorNo;
                        Com.Parameters["@LotNumber"].Value = ObjCVarWH_ReceiveDetailsSerial.LotNumber;
                        EndTrans(Com, Con);
                        if (ObjCVarWH_ReceiveDetailsSerial.ID == 0)
                        {
                            ObjCVarWH_ReceiveDetailsSerial.ID = Convert.ToInt64(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarWH_ReceiveDetailsSerial.mIsChanges = false;
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
