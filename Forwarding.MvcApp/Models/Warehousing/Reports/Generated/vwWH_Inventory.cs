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
    public partial class CVarvwWH_Inventory
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mLocationID;
        internal String mLocationCode;
        internal Decimal mAvailableQuantity;
        internal Decimal mAllocatedQuantity;
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
        internal String mOCNCode;
        internal String mChassisNumber;
        internal String mModel;
        internal String mKeyNumber;
        internal String mEC;
        internal String mPaintType;
        internal String mIC;
        internal String mCommercialInvoiceNumber;
        internal String mInsurancePolicyNumber;
        internal String mProductionOrder;
        internal String mPINumber;
        internal String mBillNumber;
        internal String mEngineNumber;
        internal String mBatchNumber;
        internal DateTime mExpirationDate;
        internal String mImportedBy;
        internal Decimal mWeightInTons;
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
        public Decimal AllocatedQuantity
        {
            get { return mAllocatedQuantity; }
            set { mAllocatedQuantity = value; }
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
        public String OCNCode
        {
            get { return mOCNCode; }
            set { mOCNCode = value; }
        }
        public String ChassisNumber
        {
            get { return mChassisNumber; }
            set { mChassisNumber = value; }
        }
        public String Model
        {
            get { return mModel; }
            set { mModel = value; }
        }
        public String KeyNumber
        {
            get { return mKeyNumber; }
            set { mKeyNumber = value; }
        }
        public String EC
        {
            get { return mEC; }
            set { mEC = value; }
        }
        public String PaintType
        {
            get { return mPaintType; }
            set { mPaintType = value; }
        }
        public String IC
        {
            get { return mIC; }
            set { mIC = value; }
        }
        public String CommercialInvoiceNumber
        {
            get { return mCommercialInvoiceNumber; }
            set { mCommercialInvoiceNumber = value; }
        }
        public String InsurancePolicyNumber
        {
            get { return mInsurancePolicyNumber; }
            set { mInsurancePolicyNumber = value; }
        }
        public String ProductionOrder
        {
            get { return mProductionOrder; }
            set { mProductionOrder = value; }
        }
        public String PINumber
        {
            get { return mPINumber; }
            set { mPINumber = value; }
        }
        public String BillNumber
        {
            get { return mBillNumber; }
            set { mBillNumber = value; }
        }
        public String EngineNumber
        {
            get { return mEngineNumber; }
            set { mEngineNumber = value; }
        }
        public String BatchNumber
        {
            get { return mBatchNumber; }
            set { mBatchNumber = value; }
        }
        public DateTime ExpirationDate
        {
            get { return mExpirationDate; }
            set { mExpirationDate = value; }
        }
        public String ImportedBy
        {
            get { return mImportedBy; }
            set { mImportedBy = value; }
        }
        public Decimal WeightInTons
        {
            get { return mWeightInTons; }
            set { mWeightInTons = value; }
        }
        #endregion
    }

    public partial class CvwWH_Inventory
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
        public List<CVarvwWH_Inventory> lstCVarvwWH_Inventory = new List<CVarvwWH_Inventory>();
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
            lstCVarvwWH_Inventory.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwWH_Inventory";
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
                        CVarvwWH_Inventory ObjCVarvwWH_Inventory = new CVarvwWH_Inventory();
                        ObjCVarvwWH_Inventory.mLocationID = Convert.ToInt32(dr["LocationID"].ToString());
                        ObjCVarvwWH_Inventory.mLocationCode = Convert.ToString(dr["LocationCode"].ToString());
                        ObjCVarvwWH_Inventory.mAvailableQuantity = Convert.ToDecimal(dr["AvailableQuantity"].ToString());
                        ObjCVarvwWH_Inventory.mAllocatedQuantity = Convert.ToDecimal(dr["AllocatedQuantity"].ToString());
                        ObjCVarvwWH_Inventory.mReceiveCode = Convert.ToString(dr["ReceiveCode"].ToString());
                        ObjCVarvwWH_Inventory.mReceiveDate = Convert.ToDateTime(dr["ReceiveDate"].ToString());
                        ObjCVarvwWH_Inventory.mPalletID = Convert.ToString(dr["PalletID"].ToString());
                        ObjCVarvwWH_Inventory.mPurchaseItemID = Convert.ToInt64(dr["PurchaseItemID"].ToString());
                        ObjCVarvwWH_Inventory.mBarCode = Convert.ToString(dr["BarCode"].ToString());
                        ObjCVarvwWH_Inventory.mPurchaseItemCode = Convert.ToString(dr["PurchaseItemCode"].ToString());
                        ObjCVarvwWH_Inventory.mPurchaseItemName = Convert.ToString(dr["PurchaseItemName"].ToString());
                        ObjCVarvwWH_Inventory.mPackageTypeName = Convert.ToString(dr["PackageTypeName"].ToString());
                        ObjCVarvwWH_Inventory.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarvwWH_Inventory.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwWH_Inventory.mOCNCode = Convert.ToString(dr["OCNCode"].ToString());
                        ObjCVarvwWH_Inventory.mChassisNumber = Convert.ToString(dr["ChassisNumber"].ToString());
                        ObjCVarvwWH_Inventory.mModel = Convert.ToString(dr["Model"].ToString());
                        ObjCVarvwWH_Inventory.mKeyNumber = Convert.ToString(dr["KeyNumber"].ToString());
                        ObjCVarvwWH_Inventory.mEC = Convert.ToString(dr["EC"].ToString());
                        ObjCVarvwWH_Inventory.mPaintType = Convert.ToString(dr["PaintType"].ToString());
                        ObjCVarvwWH_Inventory.mIC = Convert.ToString(dr["IC"].ToString());
                        ObjCVarvwWH_Inventory.mCommercialInvoiceNumber = Convert.ToString(dr["CommercialInvoiceNumber"].ToString());
                        ObjCVarvwWH_Inventory.mInsurancePolicyNumber = Convert.ToString(dr["InsurancePolicyNumber"].ToString());
                        ObjCVarvwWH_Inventory.mProductionOrder = Convert.ToString(dr["ProductionOrder"].ToString());
                        ObjCVarvwWH_Inventory.mPINumber = Convert.ToString(dr["PINumber"].ToString());
                        ObjCVarvwWH_Inventory.mBillNumber = Convert.ToString(dr["BillNumber"].ToString());
                        ObjCVarvwWH_Inventory.mEngineNumber = Convert.ToString(dr["EngineNumber"].ToString());
                        ObjCVarvwWH_Inventory.mBatchNumber = Convert.ToString(dr["BatchNumber"].ToString());
                        ObjCVarvwWH_Inventory.mExpirationDate = Convert.ToDateTime(dr["ExpirationDate"].ToString());
                        ObjCVarvwWH_Inventory.mImportedBy = Convert.ToString(dr["ImportedBy"].ToString());
                        ObjCVarvwWH_Inventory.mWeightInTons = Convert.ToDecimal(dr["WeightInTons"].ToString());
                        lstCVarvwWH_Inventory.Add(ObjCVarvwWH_Inventory);
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
            lstCVarvwWH_Inventory.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwWH_Inventory";
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
                        CVarvwWH_Inventory ObjCVarvwWH_Inventory = new CVarvwWH_Inventory();
                        ObjCVarvwWH_Inventory.mLocationID = Convert.ToInt32(dr["LocationID"].ToString());
                        ObjCVarvwWH_Inventory.mLocationCode = Convert.ToString(dr["LocationCode"].ToString());
                        ObjCVarvwWH_Inventory.mAvailableQuantity = Convert.ToDecimal(dr["AvailableQuantity"].ToString());
                        ObjCVarvwWH_Inventory.mAllocatedQuantity = Convert.ToDecimal(dr["AllocatedQuantity"].ToString());
                        ObjCVarvwWH_Inventory.mReceiveCode = Convert.ToString(dr["ReceiveCode"].ToString());
                        ObjCVarvwWH_Inventory.mReceiveDate = Convert.ToDateTime(dr["ReceiveDate"].ToString());
                        ObjCVarvwWH_Inventory.mPalletID = Convert.ToString(dr["PalletID"].ToString());
                        ObjCVarvwWH_Inventory.mPurchaseItemID = Convert.ToInt64(dr["PurchaseItemID"].ToString());
                        ObjCVarvwWH_Inventory.mBarCode = Convert.ToString(dr["BarCode"].ToString());
                        ObjCVarvwWH_Inventory.mPurchaseItemCode = Convert.ToString(dr["PurchaseItemCode"].ToString());
                        ObjCVarvwWH_Inventory.mPurchaseItemName = Convert.ToString(dr["PurchaseItemName"].ToString());
                        ObjCVarvwWH_Inventory.mPackageTypeName = Convert.ToString(dr["PackageTypeName"].ToString());
                        ObjCVarvwWH_Inventory.mCustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                        ObjCVarvwWH_Inventory.mCustomerName = Convert.ToString(dr["CustomerName"].ToString());
                        ObjCVarvwWH_Inventory.mOCNCode = Convert.ToString(dr["OCNCode"].ToString());
                        ObjCVarvwWH_Inventory.mChassisNumber = Convert.ToString(dr["ChassisNumber"].ToString());
                        ObjCVarvwWH_Inventory.mModel = Convert.ToString(dr["Model"].ToString());
                        ObjCVarvwWH_Inventory.mKeyNumber = Convert.ToString(dr["KeyNumber"].ToString());
                        ObjCVarvwWH_Inventory.mEC = Convert.ToString(dr["EC"].ToString());
                        ObjCVarvwWH_Inventory.mPaintType = Convert.ToString(dr["PaintType"].ToString());
                        ObjCVarvwWH_Inventory.mIC = Convert.ToString(dr["IC"].ToString());
                        ObjCVarvwWH_Inventory.mCommercialInvoiceNumber = Convert.ToString(dr["CommercialInvoiceNumber"].ToString());
                        ObjCVarvwWH_Inventory.mInsurancePolicyNumber = Convert.ToString(dr["InsurancePolicyNumber"].ToString());
                        ObjCVarvwWH_Inventory.mProductionOrder = Convert.ToString(dr["ProductionOrder"].ToString());
                        ObjCVarvwWH_Inventory.mPINumber = Convert.ToString(dr["PINumber"].ToString());
                        ObjCVarvwWH_Inventory.mBillNumber = Convert.ToString(dr["BillNumber"].ToString());
                        ObjCVarvwWH_Inventory.mEngineNumber = Convert.ToString(dr["EngineNumber"].ToString());
                        ObjCVarvwWH_Inventory.mBatchNumber = Convert.ToString(dr["BatchNumber"].ToString());
                        ObjCVarvwWH_Inventory.mExpirationDate = Convert.ToDateTime(dr["ExpirationDate"].ToString());
                        ObjCVarvwWH_Inventory.mImportedBy = Convert.ToString(dr["ImportedBy"].ToString());
                        ObjCVarvwWH_Inventory.mWeightInTons = Convert.ToDecimal(dr["WeightInTons"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwWH_Inventory.Add(ObjCVarvwWH_Inventory);
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
