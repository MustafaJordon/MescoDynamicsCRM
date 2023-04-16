using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace Forwarding.MvcApp.Models.Warehousing.MasterData.Generated
{
    [Serializable]
    public partial class CVarvwWH_Warehouse
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mID;
        internal String mCode;
        internal String mName;
        internal String mLocalName;
        internal String mPhone;
        internal String mFax;
        internal String mAddress;
        internal String mNotes;
        internal Int32 mMainWarehouseID;
        internal String mMainWareHouseCode;
        internal Int32 mWarehouseType;
        internal String mMainWarehouseName;
        internal String mWarehouseTypeName;
        internal Boolean mIsActuallyUsed;
        #endregion

        #region "Methods"
        public Int32 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public String Code
        {
            get { return mCode; }
            set { mCode = value; }
        }
        public String Name
        {
            get { return mName; }
            set { mName = value; }
        }
        public String LocalName
        {
            get { return mLocalName; }
            set { mLocalName = value; }
        }
        public String Phone
        {
            get { return mPhone; }
            set { mPhone = value; }
        }
        public String Fax
        {
            get { return mFax; }
            set { mFax = value; }
        }
        public String Address
        {
            get { return mAddress; }
            set { mAddress = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mNotes = value; }
        }
        public Int32 MainWarehouseID
        {
            get { return mMainWarehouseID; }
            set { mMainWarehouseID = value; }
        }
        public String MainWareHouseCode
        {
            get { return mMainWareHouseCode; }
            set { mMainWareHouseCode = value; }
        }
        public Int32 WarehouseType
        {
            get { return mWarehouseType; }
            set { mWarehouseType = value; }
        }
        public String MainWarehouseName
        {
            get { return mMainWarehouseName; }
            set { mMainWarehouseName = value; }
        }
        public String WarehouseTypeName
        {
            get { return mWarehouseTypeName; }
            set { mWarehouseTypeName = value; }
        }
        public Boolean IsActuallyUsed
        {
            get { return mIsActuallyUsed; }
            set { mIsActuallyUsed = value; }
        }
        #endregion
    }

    public partial class CvwWH_Warehouse
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
        public List<CVarvwWH_Warehouse> lstCVarvwWH_Warehouse = new List<CVarvwWH_Warehouse>();
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
            lstCVarvwWH_Warehouse.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwWH_Warehouse";
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
                        CVarvwWH_Warehouse ObjCVarvwWH_Warehouse = new CVarvwWH_Warehouse();
                        ObjCVarvwWH_Warehouse.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwWH_Warehouse.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwWH_Warehouse.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwWH_Warehouse.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwWH_Warehouse.mPhone = Convert.ToString(dr["Phone"].ToString());
                        ObjCVarvwWH_Warehouse.mFax = Convert.ToString(dr["Fax"].ToString());
                        ObjCVarvwWH_Warehouse.mAddress = Convert.ToString(dr["Address"].ToString());
                        ObjCVarvwWH_Warehouse.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwWH_Warehouse.mMainWarehouseID = Convert.ToInt32(dr["MainWarehouseID"].ToString());
                        ObjCVarvwWH_Warehouse.mMainWareHouseCode = Convert.ToString(dr["MainWareHouseCode"].ToString());
                        ObjCVarvwWH_Warehouse.mWarehouseType = Convert.ToInt32(dr["WarehouseType"].ToString());
                        ObjCVarvwWH_Warehouse.mMainWarehouseName = Convert.ToString(dr["MainWarehouseName"].ToString());
                        ObjCVarvwWH_Warehouse.mWarehouseTypeName = Convert.ToString(dr["WarehouseTypeName"].ToString());
                        ObjCVarvwWH_Warehouse.mIsActuallyUsed = Convert.ToBoolean(dr["IsActuallyUsed"].ToString());
                        lstCVarvwWH_Warehouse.Add(ObjCVarvwWH_Warehouse);
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
            lstCVarvwWH_Warehouse.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwWH_Warehouse";
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
                        CVarvwWH_Warehouse ObjCVarvwWH_Warehouse = new CVarvwWH_Warehouse();
                        ObjCVarvwWH_Warehouse.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwWH_Warehouse.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwWH_Warehouse.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwWH_Warehouse.mLocalName = Convert.ToString(dr["LocalName"].ToString());
                        ObjCVarvwWH_Warehouse.mPhone = Convert.ToString(dr["Phone"].ToString());
                        ObjCVarvwWH_Warehouse.mFax = Convert.ToString(dr["Fax"].ToString());
                        ObjCVarvwWH_Warehouse.mAddress = Convert.ToString(dr["Address"].ToString());
                        ObjCVarvwWH_Warehouse.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwWH_Warehouse.mMainWarehouseID = Convert.ToInt32(dr["MainWarehouseID"].ToString());
                        ObjCVarvwWH_Warehouse.mMainWareHouseCode = Convert.ToString(dr["MainWareHouseCode"].ToString());
                        ObjCVarvwWH_Warehouse.mWarehouseType = Convert.ToInt32(dr["WarehouseType"].ToString());
                        ObjCVarvwWH_Warehouse.mMainWarehouseName = Convert.ToString(dr["MainWarehouseName"].ToString());
                        ObjCVarvwWH_Warehouse.mWarehouseTypeName = Convert.ToString(dr["WarehouseTypeName"].ToString());
                        ObjCVarvwWH_Warehouse.mIsActuallyUsed = Convert.ToBoolean(dr["IsActuallyUsed"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwWH_Warehouse.Add(ObjCVarvwWH_Warehouse);
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
