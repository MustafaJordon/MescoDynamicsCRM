using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.ContainerFreightStation.Transactions
{
    [Serializable]
    public partial class CVarvwWH_CFS_ReleaseOrders
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mOperationNumber;
        internal String mMasterBL;
        internal String mContainerNumber;
        internal String mHouseNumber;
        internal String mConsignee;
        internal String mBookingParty;
        internal String mStorageLocation;
        internal DateTime mEntryDate;
        internal String mContainerType;
        internal Decimal mGrossWeight;
        internal Decimal mNetWeight;
        internal String mPackages;
        internal String mDescriptionOfGoods;
        internal DateTime mStorageEndDate;
        internal Int64 mInvoiceNumber;
        internal DateTime mInvoiceDate;
        internal String mInvoiceTypeName;
        internal DateTime mServerDate;
        internal DateTime mInvoicePaymentDate;
        internal Boolean mCanRelease;
        internal String mReleaseNumber;
        internal String mCouponNumber;
        internal String mCertificationNumber;
        internal String mRemarks;
        internal String mReleasedBy;
        internal DateTime mReleasingDate;
        internal Int64 mOperationID;
        internal Int64 mContainerID;
        internal Int64 mHouseBillID;
        internal Int32 mConsigneeID;
        internal Int32 mBookingPartyID;
        internal Int64 mInventoryID;
        internal Int64 mReleaseOrderID;
        #endregion

        #region "Methods"
        public String OperationNumber
        {
            get { return mOperationNumber; }
            set { mOperationNumber = value; }
        }
        public String MasterBL
        {
            get { return mMasterBL; }
            set { mMasterBL = value; }
        }
        public String ContainerNumber
        {
            get { return mContainerNumber; }
            set { mContainerNumber = value; }
        }
        public String HouseNumber
        {
            get { return mHouseNumber; }
            set { mHouseNumber = value; }
        }
        public String Consignee
        {
            get { return mConsignee; }
            set { mConsignee = value; }
        }
        public String BookingParty
        {
            get { return mBookingParty; }
            set { mBookingParty = value; }
        }
        public String StorageLocation
        {
            get { return mStorageLocation; }
            set { mStorageLocation = value; }
        }
        public DateTime EntryDate
        {
            get { return mEntryDate; }
            set { mEntryDate = value; }
        }
        public String ContainerType
        {
            get { return mContainerType; }
            set { mContainerType = value; }
        }
        public Decimal GrossWeight
        {
            get { return mGrossWeight; }
            set { mGrossWeight = value; }
        }
        public Decimal NetWeight
        {
            get { return mNetWeight; }
            set { mNetWeight = value; }
        }
        public String Packages
        {
            get { return mPackages; }
            set { mPackages = value; }
        }
        public String DescriptionOfGoods
        {
            get { return mDescriptionOfGoods; }
            set { mDescriptionOfGoods = value; }
        }
        public DateTime StorageEndDate
        {
            get { return mStorageEndDate; }
            set { mStorageEndDate = value; }
        }
        public Int64 InvoiceNumber
        {
            get { return mInvoiceNumber; }
            set { mInvoiceNumber = value; }
        }
        public DateTime InvoiceDate
        {
            get { return mInvoiceDate; }
            set { mInvoiceDate = value; }
        }
        public String InvoiceTypeName
        {
            get { return mInvoiceTypeName; }
            set { mInvoiceTypeName = value; }
        }
        public DateTime ServerDate
        {
            get { return mServerDate; }
            set { mServerDate = value; }
        }
        public DateTime InvoicePaymentDate
        {
            get { return mInvoicePaymentDate; }
            set { mInvoicePaymentDate = value; }
        }
        public Boolean CanRelease
        {
            get { return mCanRelease; }
            set { mCanRelease = value; }
        }
        public String ReleaseNumber
        {
            get { return mReleaseNumber; }
            set { mReleaseNumber = value; }
        }
        public String CouponNumber
        {
            get { return mCouponNumber; }
            set { mCouponNumber = value; }
        }
        public String CertificationNumber
        {
            get { return mCertificationNumber; }
            set { mCertificationNumber = value; }
        }
        public String Remarks
        {
            get { return mRemarks; }
            set { mRemarks = value; }
        }
        public String ReleasedBy
        {
            get { return mReleasedBy; }
            set { mReleasedBy = value; }
        }
        public DateTime ReleasingDate
        {
            get { return mReleasingDate; }
            set { mReleasingDate = value; }
        }
        public Int64 OperationID
        {
            get { return mOperationID; }
            set { mOperationID = value; }
        }
        public Int64 ContainerID
        {
            get { return mContainerID; }
            set { mContainerID = value; }
        }
        public Int64 HouseBillID
        {
            get { return mHouseBillID; }
            set { mHouseBillID = value; }
        }
        public Int32 ConsigneeID
        {
            get { return mConsigneeID; }
            set { mConsigneeID = value; }
        }
        public Int32 BookingPartyID
        {
            get { return mBookingPartyID; }
            set { mBookingPartyID = value; }
        }
        public Int64 InventoryID
        {
            get { return mInventoryID; }
            set { mInventoryID = value; }
        }
        public Int64 ReleaseOrderID
        {
            get { return mReleaseOrderID; }
            set { mReleaseOrderID = value; }
        }
        #endregion
    }

    public partial class CvwWH_CFS_ReleaseOrders
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
        public List<CVarvwWH_CFS_ReleaseOrders> lstCVarvwWH_CFS_ReleaseOrders = new List<CVarvwWH_CFS_ReleaseOrders>();
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
            lstCVarvwWH_CFS_ReleaseOrders.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwWH_CFS_ReleaseOrders";
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
                        CVarvwWH_CFS_ReleaseOrders ObjCVarvwWH_CFS_ReleaseOrders = new CVarvwWH_CFS_ReleaseOrders();
                        ObjCVarvwWH_CFS_ReleaseOrders.mOperationNumber = Convert.ToString(dr["OperationNumber"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mMasterBL = Convert.ToString(dr["MasterBL"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mContainerNumber = Convert.ToString(dr["ContainerNumber"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mHouseNumber = Convert.ToString(dr["HouseNumber"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mConsignee = Convert.ToString(dr["Consignee"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mBookingParty = Convert.ToString(dr["BookingParty"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mStorageLocation = Convert.ToString(dr["StorageLocation"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mEntryDate = Convert.ToDateTime(dr["EntryDate"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mContainerType = Convert.ToString(dr["ContainerType"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mGrossWeight = Convert.ToDecimal(dr["GrossWeight"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mNetWeight = Convert.ToDecimal(dr["NetWeight"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mPackages = Convert.ToString(dr["Packages"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mDescriptionOfGoods = Convert.ToString(dr["DescriptionOfGoods"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mStorageEndDate = Convert.ToDateTime(dr["StorageEndDate"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mInvoiceNumber = Convert.ToInt64(dr["InvoiceNumber"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mInvoiceDate = Convert.ToDateTime(dr["InvoiceDate"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mInvoiceTypeName = Convert.ToString(dr["InvoiceTypeName"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mServerDate = Convert.ToDateTime(dr["ServerDate"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mInvoicePaymentDate = Convert.ToDateTime(dr["InvoicePaymentDate"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mCanRelease = Convert.ToBoolean(dr["CanRelease"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mReleaseNumber = Convert.ToString(dr["ReleaseNumber"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mCouponNumber = Convert.ToString(dr["CouponNumber"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mCertificationNumber = Convert.ToString(dr["CertificationNumber"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mRemarks = Convert.ToString(dr["Remarks"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mReleasedBy = Convert.ToString(dr["ReleasedBy"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mReleasingDate = Convert.ToDateTime(dr["ReleasingDate"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mContainerID = Convert.ToInt64(dr["ContainerID"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mHouseBillID = Convert.ToInt64(dr["HouseBillID"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mConsigneeID = Convert.ToInt32(dr["ConsigneeID"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mBookingPartyID = Convert.ToInt32(dr["BookingPartyID"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mInventoryID = Convert.ToInt64(dr["InventoryID"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mReleaseOrderID = Convert.ToInt64(dr["ReleaseOrderID"].ToString());
                        lstCVarvwWH_CFS_ReleaseOrders.Add(ObjCVarvwWH_CFS_ReleaseOrders);
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
            lstCVarvwWH_CFS_ReleaseOrders.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwWH_CFS_ReleaseOrders";
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
                        CVarvwWH_CFS_ReleaseOrders ObjCVarvwWH_CFS_ReleaseOrders = new CVarvwWH_CFS_ReleaseOrders();
                        ObjCVarvwWH_CFS_ReleaseOrders.mOperationNumber = Convert.ToString(dr["OperationNumber"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mMasterBL = Convert.ToString(dr["MasterBL"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mContainerNumber = Convert.ToString(dr["ContainerNumber"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mHouseNumber = Convert.ToString(dr["HouseNumber"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mConsignee = Convert.ToString(dr["Consignee"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mBookingParty = Convert.ToString(dr["BookingParty"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mStorageLocation = Convert.ToString(dr["StorageLocation"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mEntryDate = Convert.ToDateTime(dr["EntryDate"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mContainerType = Convert.ToString(dr["ContainerType"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mGrossWeight = Convert.ToDecimal(dr["GrossWeight"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mNetWeight = Convert.ToDecimal(dr["NetWeight"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mPackages = Convert.ToString(dr["Packages"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mDescriptionOfGoods = Convert.ToString(dr["DescriptionOfGoods"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mStorageEndDate = Convert.ToDateTime(dr["StorageEndDate"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mInvoiceNumber = Convert.ToInt64(dr["InvoiceNumber"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mInvoiceDate = Convert.ToDateTime(dr["InvoiceDate"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mInvoiceTypeName = Convert.ToString(dr["InvoiceTypeName"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mServerDate = Convert.ToDateTime(dr["ServerDate"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mInvoicePaymentDate = Convert.ToDateTime(dr["InvoicePaymentDate"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mCanRelease = Convert.ToBoolean(dr["CanRelease"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mReleaseNumber = Convert.ToString(dr["ReleaseNumber"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mCouponNumber = Convert.ToString(dr["CouponNumber"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mCertificationNumber = Convert.ToString(dr["CertificationNumber"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mRemarks = Convert.ToString(dr["Remarks"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mReleasedBy = Convert.ToString(dr["ReleasedBy"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mReleasingDate = Convert.ToDateTime(dr["ReleasingDate"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mContainerID = Convert.ToInt64(dr["ContainerID"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mHouseBillID = Convert.ToInt64(dr["HouseBillID"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mConsigneeID = Convert.ToInt32(dr["ConsigneeID"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mBookingPartyID = Convert.ToInt32(dr["BookingPartyID"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mInventoryID = Convert.ToInt64(dr["InventoryID"].ToString());
                        ObjCVarvwWH_CFS_ReleaseOrders.mReleaseOrderID = Convert.ToInt64(dr["ReleaseOrderID"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwWH_CFS_ReleaseOrders.Add(ObjCVarvwWH_CFS_ReleaseOrders);
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
