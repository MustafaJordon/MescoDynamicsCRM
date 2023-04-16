using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.ContainerFreightStation.Transactions
{
    [Serializable]
    public partial class CVarvwWH_CFS_GateIn
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mOperationNumber;
        internal String mMasterBL;
        internal String mContainerNumber;
        internal String mContainerType;
        internal String mHouseNumber;
        internal String mConsignee;
        internal String mBookingParty;
        internal String mRoadNumber;
        internal String mDescriptionOfGoods;
        internal Decimal mGrossWeight;
        internal Decimal mNetWeight;
        internal Decimal mVolume;
        internal String mPackages;
        internal Int64 mOperationID;
        internal Int64 mContainerID;
        internal Int64 mHouseBillID;
        internal Int32 mConsigneeID;
        internal Int32 mBookingPartyID;
        internal Int64 mInventoryID;
        internal DateTime mEntryDate;
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
        public String ContainerType
        {
            get { return mContainerType; }
            set { mContainerType = value; }
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
        public String RoadNumber
        {
            get { return mRoadNumber; }
            set { mRoadNumber = value; }
        }
        public String DescriptionOfGoods
        {
            get { return mDescriptionOfGoods; }
            set { mDescriptionOfGoods = value; }
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
        public Decimal Volume
        {
            get { return mVolume; }
            set { mVolume = value; }
        }
        public String Packages
        {
            get { return mPackages; }
            set { mPackages = value; }
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
        public DateTime EntryDate
        {
            get { return mEntryDate; }
            set { mEntryDate = value; }
        }
        #endregion
    }

    public partial class CvwWH_CFS_GateIn
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
        public List<CVarvwWH_CFS_GateIn> lstCVarvwWH_CFS_GateIn = new List<CVarvwWH_CFS_GateIn>();
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
            lstCVarvwWH_CFS_GateIn.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwWH_CFS_GateIn";
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
                        CVarvwWH_CFS_GateIn ObjCVarvwWH_CFS_GateIn = new CVarvwWH_CFS_GateIn();
                        ObjCVarvwWH_CFS_GateIn.mOperationNumber = Convert.ToString(dr["OperationNumber"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mMasterBL = Convert.ToString(dr["MasterBL"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mContainerNumber = Convert.ToString(dr["ContainerNumber"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mContainerType = Convert.ToString(dr["ContainerType"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mHouseNumber = Convert.ToString(dr["HouseNumber"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mConsignee = Convert.ToString(dr["Consignee"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mBookingParty = Convert.ToString(dr["BookingParty"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mRoadNumber = Convert.ToString(dr["RoadNumber"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mDescriptionOfGoods = Convert.ToString(dr["DescriptionOfGoods"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mGrossWeight = Convert.ToDecimal(dr["GrossWeight"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mNetWeight = Convert.ToDecimal(dr["NetWeight"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mVolume = Convert.ToDecimal(dr["Volume"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mPackages = Convert.ToString(dr["Packages"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mContainerID = Convert.ToInt64(dr["ContainerID"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mHouseBillID = Convert.ToInt64(dr["HouseBillID"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mConsigneeID = Convert.ToInt32(dr["ConsigneeID"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mBookingPartyID = Convert.ToInt32(dr["BookingPartyID"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mInventoryID = Convert.ToInt64(dr["InventoryID"].ToString());
                        if (dr["EntryDate"].ToString() != "")
                        {
                            ObjCVarvwWH_CFS_GateIn.mEntryDate = Convert.ToDateTime(dr["EntryDate"].ToString());
                        }

                        lstCVarvwWH_CFS_GateIn.Add(ObjCVarvwWH_CFS_GateIn);
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
            lstCVarvwWH_CFS_GateIn.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwWH_CFS_GateIn";
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
                        CVarvwWH_CFS_GateIn ObjCVarvwWH_CFS_GateIn = new CVarvwWH_CFS_GateIn();
                        ObjCVarvwWH_CFS_GateIn.mOperationNumber = Convert.ToString(dr["OperationNumber"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mMasterBL = Convert.ToString(dr["MasterBL"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mContainerNumber = Convert.ToString(dr["ContainerNumber"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mContainerType = Convert.ToString(dr["ContainerType"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mHouseNumber = Convert.ToString(dr["HouseNumber"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mConsignee = Convert.ToString(dr["Consignee"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mBookingParty = Convert.ToString(dr["BookingParty"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mRoadNumber = Convert.ToString(dr["RoadNumber"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mDescriptionOfGoods = Convert.ToString(dr["DescriptionOfGoods"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mGrossWeight = Convert.ToDecimal(dr["GrossWeight"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mNetWeight = Convert.ToDecimal(dr["NetWeight"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mVolume = Convert.ToDecimal(dr["Volume"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mPackages = Convert.ToString(dr["Packages"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mContainerID = Convert.ToInt64(dr["ContainerID"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mHouseBillID = Convert.ToInt64(dr["HouseBillID"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mConsigneeID = Convert.ToInt32(dr["ConsigneeID"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mBookingPartyID = Convert.ToInt32(dr["BookingPartyID"].ToString());
                        ObjCVarvwWH_CFS_GateIn.mInventoryID = Convert.ToInt64(dr["InventoryID"].ToString());

                        if (dr["EntryDate"].ToString() != "")
                        {
                            ObjCVarvwWH_CFS_GateIn.mEntryDate = Convert.ToDateTime(dr["EntryDate"].ToString());
                        }

                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwWH_CFS_GateIn.Add(ObjCVarvwWH_CFS_GateIn);
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
