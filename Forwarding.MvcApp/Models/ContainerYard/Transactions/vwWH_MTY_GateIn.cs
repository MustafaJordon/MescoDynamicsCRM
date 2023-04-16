﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.ContainerYard.Transactions
{
    [Serializable]
    public partial class CVarvwWH_MTY_GateIn
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mID;
        internal String mContainerNumber;
        internal String mContainerType;
        internal String mWH_Warehouse;
        internal Int32 mContainerTypesID;
        internal Int32 mWarehouseID;
        internal DateTime mEntryDate;
        internal Int64 mContainerID;
        internal Int64 mOperationID;
        internal DateTime mStorageEndDate;
        internal Int32 mAreaID;
        internal Int32 mRowID;
        internal Int32 mRowLocationID;
        #endregion

        #region "Methods"
        public Int64 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public String ContainerNumber
        {
            get { return mContainerNumber; }
            set { mContainerNumber = value; }
        }
        public String ContainerType
        {
            get { return mContainerType; }
            set { mContainerType = value; }
        }
        public String WH_Warehouse
        {
            get { return mWH_Warehouse; }
            set { mWH_Warehouse = value; }
        }
        public Int32 ContainerTypesID
        {
            get { return mContainerTypesID; }
            set { mContainerTypesID = value; }
        }
        public Int32 WarehouseID
        {
            get { return mWarehouseID; }
            set { mWarehouseID = value; }
        }
        public DateTime EntryDate
        {
            get { return mEntryDate; }
            set { mEntryDate = value; }
        }
        public Int64 ContainerID
        {
            get { return mContainerID; }
            set { mContainerID = value; }
        }
        public Int64 OperationID
        {
            get { return mOperationID; }
            set { mOperationID = value; }
        }
        public DateTime StorageEndDate
        {
            get { return mStorageEndDate; }
            set { mStorageEndDate = value; }
        }
        public Int32 AreaID
        {
            get { return mAreaID; }
            set { mAreaID = value; }
        }
        public Int32 RowID
        {
            get { return mRowID; }
            set { mRowID = value; }
        }
        public Int32 RowLocationID
        {
            get { return mRowLocationID; }
            set { mRowLocationID = value; }
        }
        #endregion
    }

    public partial class CvwWH_MTY_GateIn
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
        public List<CVarvwWH_MTY_GateIn> lstCVarvwWH_MTY_GateIn = new List<CVarvwWH_MTY_GateIn>();
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
        private Exception DataFill(string Param, Boolean IsList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarvwWH_MTY_GateIn.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwWH_MTY_GateIn";
                    Com.Parameters[0].Value = Param;
                }
                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        /*Start DataReader*/
                        CVarvwWH_MTY_GateIn ObjCVarvwWH_MTY_GateIn = new CVarvwWH_MTY_GateIn();
                        ObjCVarvwWH_MTY_GateIn.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mContainerNumber = Convert.ToString(dr["ContainerNumber"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mContainerType = Convert.ToString(dr["ContainerType"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mWH_Warehouse = Convert.ToString(dr["WH_Warehouse"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mContainerTypesID = Convert.ToInt32(dr["ContainerTypesID"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mWarehouseID = Convert.ToInt32(dr["WarehouseID"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mEntryDate = Convert.ToDateTime(dr["EntryDate"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mContainerID = Convert.ToInt64(dr["ContainerID"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mStorageEndDate = Convert.ToDateTime(dr["StorageEndDate"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mAreaID = Convert.ToInt32(dr["AreaID"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mRowID = Convert.ToInt32(dr["RowID"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mRowLocationID = Convert.ToInt32(dr["RowLocationID"].ToString());
                        lstCVarvwWH_MTY_GateIn.Add(ObjCVarvwWH_MTY_GateIn);
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
            lstCVarvwWH_MTY_GateIn.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwWH_MTY_GateIn";
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
                        CVarvwWH_MTY_GateIn ObjCVarvwWH_MTY_GateIn = new CVarvwWH_MTY_GateIn();
                        ObjCVarvwWH_MTY_GateIn.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mContainerNumber = Convert.ToString(dr["ContainerNumber"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mContainerType = Convert.ToString(dr["ContainerType"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mWH_Warehouse = Convert.ToString(dr["WH_Warehouse"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mContainerTypesID = Convert.ToInt32(dr["ContainerTypesID"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mWarehouseID = Convert.ToInt32(dr["WarehouseID"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mEntryDate = Convert.ToDateTime(dr["EntryDate"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mContainerID = Convert.ToInt64(dr["ContainerID"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mStorageEndDate = Convert.ToDateTime(dr["StorageEndDate"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mAreaID = Convert.ToInt32(dr["AreaID"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mRowID = Convert.ToInt32(dr["RowID"].ToString());
                        ObjCVarvwWH_MTY_GateIn.mRowLocationID = Convert.ToInt32(dr["RowLocationID"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwWH_MTY_GateIn.Add(ObjCVarvwWH_MTY_GateIn);
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
    }
}
