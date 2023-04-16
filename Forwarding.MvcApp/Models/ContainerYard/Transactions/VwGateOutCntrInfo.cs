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
    public partial class CVarVwGateOutCntrInfo
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mID;
        internal String mContainerNumber;
        internal DateTime mTransactionDate;
        internal String mCustomersName;
        internal String mWH_WarehouseName;
        internal Int32 mCustomerID;
        internal Int32 mWarehouseID;
        internal Int64 mWH_CntrStockID;
        internal Int32 mIsHire;
        internal Boolean misOwn;
        internal Int32 mContainerTypesID;
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
        public DateTime TransactionDate
        {
            get { return mTransactionDate; }
            set { mTransactionDate = value; }
        }
        public String CustomersName
        {
            get { return mCustomersName; }
            set { mCustomersName = value; }
        }
        public String WH_WarehouseName
        {
            get { return mWH_WarehouseName; }
            set { mWH_WarehouseName = value; }
        }
        public Int32 CustomerID
        {
            get { return mCustomerID; }
            set { mCustomerID = value; }
        }
        public Int32 WarehouseID
        {
            get { return mWarehouseID; }
            set { mWarehouseID = value; }
        }
        public Int64 WH_CntrStockID
        {
            get { return mWH_CntrStockID; }
            set { mWH_CntrStockID = value; }
        }
        public Int32 IsHire
        {
            get { return mIsHire; }
            set { mIsHire = value; }
        }
        public Boolean isOwn
        {
            get { return misOwn; }
            set { misOwn = value; }
        }
        public Int32 ContainerTypesID
        {
            get { return mContainerTypesID; }
            set { mContainerTypesID = value; }
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

    public partial class CVwGateOutCntrInfo
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
        public List<CVarVwGateOutCntrInfo> lstCVarVwGateOutCntrInfo = new List<CVarVwGateOutCntrInfo>();
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
            lstCVarVwGateOutCntrInfo.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListVwGateOutCntrInfo";
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
                        CVarVwGateOutCntrInfo ObjCVarVwGateOutCntrInfo = new CVarVwGateOutCntrInfo();
                        ObjCVarVwGateOutCntrInfo.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarVwGateOutCntrInfo.mContainerNumber = Convert.ToString(dr["ContainerNumber"].ToString());
                        ObjCVarVwGateOutCntrInfo.mTransactionDate = Convert.ToDateTime(dr["TransactionDate"].ToString());
                        ObjCVarVwGateOutCntrInfo.mCustomersName = Convert.ToString(dr["CustomersName"].ToString());
                        ObjCVarVwGateOutCntrInfo.mWH_WarehouseName = Convert.ToString(dr["WH_WarehouseName"].ToString());
                        ObjCVarVwGateOutCntrInfo.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarVwGateOutCntrInfo.mWarehouseID = Convert.ToInt32(dr["WarehouseID"].ToString());
                        ObjCVarVwGateOutCntrInfo.mWH_CntrStockID = Convert.ToInt64(dr["WH_CntrStockID"].ToString());
                        ObjCVarVwGateOutCntrInfo.mIsHire = Convert.ToInt32(dr["IsHire"].ToString());
                        ObjCVarVwGateOutCntrInfo.misOwn = Convert.ToBoolean(dr["isOwn"].ToString());
                        ObjCVarVwGateOutCntrInfo.mContainerTypesID = Convert.ToInt32(dr["ContainerTypesID"].ToString());
                        ObjCVarVwGateOutCntrInfo.mAreaID = Convert.ToInt32(dr["AreaID"].ToString());
                        ObjCVarVwGateOutCntrInfo.mRowID = Convert.ToInt32(dr["RowID"].ToString());
                        ObjCVarVwGateOutCntrInfo.mRowLocationID = Convert.ToInt32(dr["RowLocationID"].ToString());
                        lstCVarVwGateOutCntrInfo.Add(ObjCVarVwGateOutCntrInfo);
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
            lstCVarVwGateOutCntrInfo.Clear();

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
                Com.CommandText = "[dbo].GetListPagingVwGateOutCntrInfo";
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
                        CVarVwGateOutCntrInfo ObjCVarVwGateOutCntrInfo = new CVarVwGateOutCntrInfo();
                        ObjCVarVwGateOutCntrInfo.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarVwGateOutCntrInfo.mContainerNumber = Convert.ToString(dr["ContainerNumber"].ToString());
                        ObjCVarVwGateOutCntrInfo.mTransactionDate = Convert.ToDateTime(dr["TransactionDate"].ToString());
                        ObjCVarVwGateOutCntrInfo.mCustomersName = Convert.ToString(dr["CustomersName"].ToString());
                        ObjCVarVwGateOutCntrInfo.mWH_WarehouseName = Convert.ToString(dr["WH_WarehouseName"].ToString());
                        ObjCVarVwGateOutCntrInfo.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarVwGateOutCntrInfo.mWarehouseID = Convert.ToInt32(dr["WarehouseID"].ToString());
                        ObjCVarVwGateOutCntrInfo.mWH_CntrStockID = Convert.ToInt64(dr["WH_CntrStockID"].ToString());
                        ObjCVarVwGateOutCntrInfo.mIsHire = Convert.ToInt32(dr["IsHire"].ToString());
                        ObjCVarVwGateOutCntrInfo.misOwn = Convert.ToBoolean(dr["isOwn"].ToString());
                        ObjCVarVwGateOutCntrInfo.mContainerTypesID = Convert.ToInt32(dr["ContainerTypesID"].ToString());
                        ObjCVarVwGateOutCntrInfo.mAreaID = Convert.ToInt32(dr["AreaID"].ToString());
                        ObjCVarVwGateOutCntrInfo.mRowID = Convert.ToInt32(dr["RowID"].ToString());
                        ObjCVarVwGateOutCntrInfo.mRowLocationID = Convert.ToInt32(dr["RowLocationID"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarVwGateOutCntrInfo.Add(ObjCVarVwGateOutCntrInfo);
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