using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.Warehousing.Reports.Generated
{
    [Serializable]
    public class CPKvwWH_InventoryWithSerial
    {
        #region "variables"
        #endregion

        #region "Methods"
        #endregion
    }
    [Serializable]
    public partial class CVarvwWH_InventoryWithSerial : CPKvwWH_InventoryWithSerial
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mLocationID;
        internal String mLocationCode;
        internal Decimal mAvailableQuantity;
        internal String mReceiveCode;
        internal DateTime mReceiveDate;
        internal String mPalletID;
        internal Int64 mPurchaseItemID;
        internal String mBarCode;
        internal String mPurchaseItemCode;
        internal String mPurchaseItemName;
        internal String mPackageTypeName;
        internal Int32 mCustomerID;
        internal String mCustomerName;
        internal String mSerial;
        internal Int32 mNumberOfAvailableSerials;
        internal String mVehicle;
        internal String mMotorNo;
        #endregion

        #region "Methods"
        public Int32 LocationID
        {
            get { return mLocationID; }
            set { mLocationID = value; }
        }
        public String LocationCode
        {
            get { return mLocationCode; }
            set { mLocationCode = value; }
        }
        public Decimal AvailableQuantity
        {
            get { return mAvailableQuantity; }
            set { mAvailableQuantity = value; }
        }
        public String ReceiveCode
        {
            get { return mReceiveCode; }
            set { mReceiveCode = value; }
        }
        public DateTime ReceiveDate
        {
            get { return mReceiveDate; }
            set { mReceiveDate = value; }
        }
        public String PalletID
        {
            get { return mPalletID; }
            set { mPalletID = value; }
        }
        public Int64 PurchaseItemID
        {
            get { return mPurchaseItemID; }
            set { mPurchaseItemID = value; }
        }
        public String BarCode
        {
            get { return mBarCode; }
            set { mBarCode = value; }
        }
        public String PurchaseItemCode
        {
            get { return mPurchaseItemCode; }
            set { mPurchaseItemCode = value; }
        }
        public String PurchaseItemName
        {
            get { return mPurchaseItemName; }
            set { mPurchaseItemName = value; }
        }
        public String PackageTypeName
        {
            get { return mPackageTypeName; }
            set { mPackageTypeName = value; }
        }
        public Int32 CustomerID
        {
            get { return mCustomerID; }
            set { mCustomerID = value; }
        }
        public String CustomerName
        {
            get { return mCustomerName; }
            set { mCustomerName = value; }
        }
        public String Serial
        {
            get { return mSerial; }
            set { mSerial = value; }
        }
        public Int32 NumberOfAvailableSerials
        {
            get { return mNumberOfAvailableSerials; }
            set { mNumberOfAvailableSerials = value; }
        }
        public String Vehicle
        {
            get { return mVehicle; }
            set { mVehicle = value; }
        }
        public String MotorNo
        {
            get { return mMotorNo; }
            set { mMotorNo = value; }
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

    public partial class CvwWH_InventoryWithSerial
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
        public List<CVarvwWH_InventoryWithSerial> lstCVarvwWH_InventoryWithSerial = new List<CVarvwWH_InventoryWithSerial>();
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
            lstCVarvwWH_InventoryWithSerial.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwWH_InventoryWithSerial";
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
                        CVarvwWH_InventoryWithSerial ObjCVarvwWH_InventoryWithSerial = new CVarvwWH_InventoryWithSerial();
                        ObjCVarvwWH_InventoryWithSerial.mLocationID = Convert.ToInt32(dr["LocationID"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mLocationCode = Convert.ToString(dr["LocationCode"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mAvailableQuantity = Convert.ToDecimal(dr["AvailableQuantity"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mReceiveCode = Convert.ToString(dr["ReceiveCode"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mReceiveDate = Convert.ToDateTime(dr["ReceiveDate"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mPalletID = Convert.ToString(dr["PalletID"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mPurchaseItemID = Convert.ToInt64(dr["PurchaseItemID"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mBarCode = Convert.ToString(dr["BarCode"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mPurchaseItemCode = Convert.ToString(dr["PurchaseItemCode"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mPurchaseItemName = Convert.ToString(dr["PurchaseItemName"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mPackageTypeName = Convert.ToString(dr["PackageTypeName"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mSerial = Convert.ToString(dr["Serial"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mNumberOfAvailableSerials = Convert.ToInt32(dr["NumberOfAvailableSerials"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mVehicle = Convert.ToString(dr["Vehicle"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mMotorNo = Convert.ToString(dr["MotorNo"].ToString());
                        lstCVarvwWH_InventoryWithSerial.Add(ObjCVarvwWH_InventoryWithSerial);
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
            lstCVarvwWH_InventoryWithSerial.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwWH_InventoryWithSerial";
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
                        CVarvwWH_InventoryWithSerial ObjCVarvwWH_InventoryWithSerial = new CVarvwWH_InventoryWithSerial();
                        ObjCVarvwWH_InventoryWithSerial.mLocationID = Convert.ToInt32(dr["LocationID"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mLocationCode = Convert.ToString(dr["LocationCode"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mAvailableQuantity = Convert.ToDecimal(dr["AvailableQuantity"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mReceiveCode = Convert.ToString(dr["ReceiveCode"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mReceiveDate = Convert.ToDateTime(dr["ReceiveDate"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mPalletID = Convert.ToString(dr["PalletID"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mPurchaseItemID = Convert.ToInt64(dr["PurchaseItemID"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mBarCode = Convert.ToString(dr["BarCode"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mPurchaseItemCode = Convert.ToString(dr["PurchaseItemCode"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mPurchaseItemName = Convert.ToString(dr["PurchaseItemName"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mPackageTypeName = Convert.ToString(dr["PackageTypeName"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mSerial = Convert.ToString(dr["Serial"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mNumberOfAvailableSerials = Convert.ToInt32(dr["NumberOfAvailableSerials"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mVehicle = Convert.ToString(dr["Vehicle"].ToString());
                        ObjCVarvwWH_InventoryWithSerial.mMotorNo = Convert.ToString(dr["MotorNo"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwWH_InventoryWithSerial.Add(ObjCVarvwWH_InventoryWithSerial);
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
