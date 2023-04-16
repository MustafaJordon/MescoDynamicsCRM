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
    public partial class CVarvwOperationsForBindTableRows
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mID;
        internal DateTime mOpenDate;
        internal Int32 mOpenYear;
        internal DateTime mCloseDate;
        internal String mCode;
        internal Int32 mCodeSerial;
        internal Int32 mBLType;
        internal String mBLTypeIconName;
        internal String mBLTypeIconStyle;
        internal Int32 mDirectionType;
        internal String mDirectionIconName;
        internal String mDirectionIconStyle;
        internal Int32 mTransportType;
        internal String mTransportIconName;
        internal String mTransportIconStyle;
        internal Int32 mShipmentType;
        internal Int32 mBranchID;
        internal String mReleaseNumber;
        internal String mReferenceNumber;
        internal String mMasterBL;
        internal String mReference;
        internal String mPOLCountryCode;
        internal String mPOLCountryName;
        internal String mPOLCode;
        internal String mPOLName;
        internal String mPODCountryCode;
        internal String mPODCountryName;
        internal String mPODCode;
        internal String mPODName;
        internal String mRepBLTypeShown;
        internal String mClientName;
        internal Int32 mBookingPartyID;
        internal String mBookingPartyName;
        internal Int32 mSalesmanID;
        internal String mSalesman;
        internal Int32 mOperationManID;
        internal String mOperationManName;
        internal Int32 mCreatorUserID;
        internal String mCreatorName;
        internal String mCreatorLocalName;
        internal Int32 mCreatorRoleID;
        internal Int32 mSalesmanRoleID;
        internal Int32 mOperationManRoleID;
        internal Int32 mOperationStageID;
        internal String mOperationStageName;
        internal Int64 mQuotationID;
        internal Int32 mLineID;
        internal String mLineName;
        internal DateTime mExpectedDeparture;
        internal DateTime mActualDeparture;
        internal DateTime mExpectedArrival;
        internal DateTime mActualArrival;
        internal String mTrackingStageName;
        internal String mBookingNumbers;
        internal String mContainerTypes;
        internal String mPackageTypes;
        internal String mForm13Number;
        internal String mACIDNumber;
        internal String mACIDDetails;
        #endregion

        #region "Methods"
        public Int64 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public DateTime OpenDate
        {
            get { return mOpenDate; }
            set { mOpenDate = value; }
        }
        public Int32 OpenYear
        {
            get { return mOpenYear; }
            set { mOpenYear = value; }
        }
        public DateTime CloseDate
        {
            get { return mCloseDate; }
            set { mCloseDate = value; }
        }
        public String Code
        {
            get { return mCode; }
            set { mCode = value; }
        }
        public Int32 CodeSerial
        {
            get { return mCodeSerial; }
            set { mCodeSerial = value; }
        }
        public Int32 BLType
        {
            get { return mBLType; }
            set { mBLType = value; }
        }
        public String BLTypeIconName
        {
            get { return mBLTypeIconName; }
            set { mBLTypeIconName = value; }
        }
        public String BLTypeIconStyle
        {
            get { return mBLTypeIconStyle; }
            set { mBLTypeIconStyle = value; }
        }
        public Int32 DirectionType
        {
            get { return mDirectionType; }
            set { mDirectionType = value; }
        }
        public String DirectionIconName
        {
            get { return mDirectionIconName; }
            set { mDirectionIconName = value; }
        }
        public String DirectionIconStyle
        {
            get { return mDirectionIconStyle; }
            set { mDirectionIconStyle = value; }
        }
        public Int32 TransportType
        {
            get { return mTransportType; }
            set { mTransportType = value; }
        }
        public String TransportIconName
        {
            get { return mTransportIconName; }
            set { mTransportIconName = value; }
        }
        public String TransportIconStyle
        {
            get { return mTransportIconStyle; }
            set { mTransportIconStyle = value; }
        }
        public Int32 ShipmentType
        {
            get { return mShipmentType; }
            set { mShipmentType = value; }
        }
        public Int32 BranchID
        {
            get { return mBranchID; }
            set { mBranchID = value; }
        }
        public String ReleaseNumber
        {
            get { return mReleaseNumber; }
            set { mReleaseNumber = value; }
        }
        public String ReferenceNumber
        {
            get { return mReferenceNumber; }
            set { mReferenceNumber = value; }
        }
        public String MasterBL
        {
            get { return mMasterBL; }
            set { mMasterBL = value; }
        }
        public String Reference
        {
            get { return mReference; }
            set { mReference = value; }
        }
        public String POLCountryCode
        {
            get { return mPOLCountryCode; }
            set { mPOLCountryCode = value; }
        }
        public String POLCountryName
        {
            get { return mPOLCountryName; }
            set { mPOLCountryName = value; }
        }
        public String POLCode
        {
            get { return mPOLCode; }
            set { mPOLCode = value; }
        }
        public String POLName
        {
            get { return mPOLName; }
            set { mPOLName = value; }
        }
        public String PODCountryCode
        {
            get { return mPODCountryCode; }
            set { mPODCountryCode = value; }
        }
        public String PODCountryName
        {
            get { return mPODCountryName; }
            set { mPODCountryName = value; }
        }
        public String PODCode
        {
            get { return mPODCode; }
            set { mPODCode = value; }
        }
        public String PODName
        {
            get { return mPODName; }
            set { mPODName = value; }
        }
        public String RepBLTypeShown
        {
            get { return mRepBLTypeShown; }
            set { mRepBLTypeShown = value; }
        }
        public String ClientName
        {
            get { return mClientName; }
            set { mClientName = value; }
        }
        public Int32 BookingPartyID
        {
            get { return mBookingPartyID; }
            set { mBookingPartyID = value; }
        }
        public String BookingPartyName
        {
            get { return mBookingPartyName; }
            set { mBookingPartyName = value; }
        }
        public Int32 SalesmanID
        {
            get { return mSalesmanID; }
            set { mSalesmanID = value; }
        }
        public String Salesman
        {
            get { return mSalesman; }
            set { mSalesman = value; }
        }
        public Int32 OperationManID
        {
            get { return mOperationManID; }
            set { mOperationManID = value; }
        }
        public String OperationManName
        {
            get { return mOperationManName; }
            set { mOperationManName = value; }
        }
        public Int32 CreatorUserID
        {
            get { return mCreatorUserID; }
            set { mCreatorUserID = value; }
        }
        public String CreatorName
        {
            get { return mCreatorName; }
            set { mCreatorName = value; }
        }
        public String CreatorLocalName
        {
            get { return mCreatorLocalName; }
            set { mCreatorLocalName = value; }
        }
        public Int32 CreatorRoleID
        {
            get { return mCreatorRoleID; }
            set { mCreatorRoleID = value; }
        }
        public Int32 SalesmanRoleID
        {
            get { return mSalesmanRoleID; }
            set { mSalesmanRoleID = value; }
        }
        public Int32 OperationManRoleID
        {
            get { return mOperationManRoleID; }
            set { mOperationManRoleID = value; }
        }
        public Int32 OperationStageID
        {
            get { return mOperationStageID; }
            set { mOperationStageID = value; }
        }
        public String OperationStageName
        {
            get { return mOperationStageName; }
            set { mOperationStageName = value; }
        }
        public Int64 QuotationID
        {
            get { return mQuotationID; }
            set { mQuotationID = value; }
        }
        public Int32 LineID
        {
            get { return mLineID; }
            set { mLineID = value; }
        }
        public String LineName
        {
            get { return mLineName; }
            set { mLineName = value; }
        }
        public DateTime ExpectedDeparture
        {
            get { return mExpectedDeparture; }
            set { mExpectedDeparture = value; }
        }
        public DateTime ActualDeparture
        {
            get { return mActualDeparture; }
            set { mActualDeparture = value; }
        }
        public DateTime ExpectedArrival
        {
            get { return mExpectedArrival; }
            set { mExpectedArrival = value; }
        }
        public DateTime ActualArrival
        {
            get { return mActualArrival; }
            set { mActualArrival = value; }
        }
        public String TrackingStageName
        {
            get { return mTrackingStageName; }
            set { mTrackingStageName = value; }
        }
        public String BookingNumbers
        {
            get { return mBookingNumbers; }
            set { mBookingNumbers = value; }
        }
        public String ContainerTypes
        {
            get { return mContainerTypes; }
            set { mContainerTypes = value; }
        }
        public String PackageTypes
        {
            get { return mPackageTypes; }
            set { mPackageTypes = value; }
        }
        public String Form13Number
        {
            get { return mForm13Number; }
            set { mForm13Number = value; }
        }
        public String ACIDNumber
        {
            get { return mACIDNumber; }
            set { mACIDNumber = value; }
        }
        public String ACIDDetails
        {
            get { return mACIDDetails; }
            set { mACIDDetails = value; }
        }
        #endregion
    }

    public partial class CvwOperationsForBindTableRows
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
        public List<CVarvwOperationsForBindTableRows> lstCVarvwOperationsForBindTableRows = new List<CVarvwOperationsForBindTableRows>();
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
            lstCVarvwOperationsForBindTableRows.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwOperationsForBindTableRows";
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
                        CVarvwOperationsForBindTableRows ObjCVarvwOperationsForBindTableRows = new CVarvwOperationsForBindTableRows();
                        ObjCVarvwOperationsForBindTableRows.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mOpenDate = Convert.ToDateTime(dr["OpenDate"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mOpenYear = Convert.ToInt32(dr["OpenYear"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mCloseDate = Convert.ToDateTime(dr["CloseDate"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mCodeSerial = Convert.ToInt32(dr["CodeSerial"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mBLType = Convert.ToInt32(dr["BLType"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mBLTypeIconName = Convert.ToString(dr["BLTypeIconName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mBLTypeIconStyle = Convert.ToString(dr["BLTypeIconStyle"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mDirectionType = Convert.ToInt32(dr["DirectionType"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mDirectionIconName = Convert.ToString(dr["DirectionIconName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mDirectionIconStyle = Convert.ToString(dr["DirectionIconStyle"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mTransportType = Convert.ToInt32(dr["TransportType"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mTransportIconName = Convert.ToString(dr["TransportIconName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mTransportIconStyle = Convert.ToString(dr["TransportIconStyle"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mShipmentType = Convert.ToInt32(dr["ShipmentType"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mBranchID = Convert.ToInt32(dr["BranchID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mReleaseNumber = Convert.ToString(dr["ReleaseNumber"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mReferenceNumber = Convert.ToString(dr["ReferenceNumber"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mMasterBL = Convert.ToString(dr["MasterBL"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mReference = Convert.ToString(dr["Reference"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mPOLCountryCode = Convert.ToString(dr["POLCountryCode"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mPOLCountryName = Convert.ToString(dr["POLCountryName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mPOLCode = Convert.ToString(dr["POLCode"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mPOLName = Convert.ToString(dr["POLName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mPODCountryCode = Convert.ToString(dr["PODCountryCode"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mPODCountryName = Convert.ToString(dr["PODCountryName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mPODCode = Convert.ToString(dr["PODCode"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mPODName = Convert.ToString(dr["PODName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mRepBLTypeShown = Convert.ToString(dr["RepBLTypeShown"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mClientName = Convert.ToString(dr["ClientName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mBookingPartyID = Convert.ToInt32(dr["BookingPartyID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mBookingPartyName = Convert.ToString(dr["BookingPartyName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mSalesmanID = Convert.ToInt32(dr["SalesmanID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mSalesman = Convert.ToString(dr["Salesman"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mOperationManID = Convert.ToInt32(dr["OperationManID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mOperationManName = Convert.ToString(dr["OperationManName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mCreatorName = Convert.ToString(dr["CreatorName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mCreatorLocalName = Convert.ToString(dr["CreatorLocalName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mCreatorRoleID = Convert.ToInt32(dr["CreatorRoleID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mSalesmanRoleID = Convert.ToInt32(dr["SalesmanRoleID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mOperationManRoleID = Convert.ToInt32(dr["OperationManRoleID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mOperationStageID = Convert.ToInt32(dr["OperationStageID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mOperationStageName = Convert.ToString(dr["OperationStageName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mQuotationID = Convert.ToInt64(dr["QuotationID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mLineID = Convert.ToInt32(dr["LineID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mLineName = Convert.ToString(dr["LineName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mExpectedDeparture = Convert.ToDateTime(dr["ExpectedDeparture"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mActualDeparture = Convert.ToDateTime(dr["ActualDeparture"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mExpectedArrival = Convert.ToDateTime(dr["ExpectedArrival"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mActualArrival = Convert.ToDateTime(dr["ActualArrival"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mTrackingStageName = Convert.ToString(dr["TrackingStageName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mBookingNumbers = Convert.ToString(dr["BookingNumbers"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mContainerTypes = Convert.ToString(dr["ContainerTypes"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mPackageTypes = Convert.ToString(dr["PackageTypes"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mForm13Number = Convert.ToString(dr["Form13Number"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mACIDNumber = Convert.ToString(dr["ACIDNumber"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mACIDDetails = Convert.ToString(dr["ACIDDetails"].ToString());
                        lstCVarvwOperationsForBindTableRows.Add(ObjCVarvwOperationsForBindTableRows);
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
            lstCVarvwOperationsForBindTableRows.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwOperationsForBindTableRows";
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
                        CVarvwOperationsForBindTableRows ObjCVarvwOperationsForBindTableRows = new CVarvwOperationsForBindTableRows();
                        ObjCVarvwOperationsForBindTableRows.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mOpenDate = Convert.ToDateTime(dr["OpenDate"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mOpenYear = Convert.ToInt32(dr["OpenYear"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mCloseDate = Convert.ToDateTime(dr["CloseDate"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mCodeSerial = Convert.ToInt32(dr["CodeSerial"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mBLType = Convert.ToInt32(dr["BLType"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mBLTypeIconName = Convert.ToString(dr["BLTypeIconName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mBLTypeIconStyle = Convert.ToString(dr["BLTypeIconStyle"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mDirectionType = Convert.ToInt32(dr["DirectionType"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mDirectionIconName = Convert.ToString(dr["DirectionIconName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mDirectionIconStyle = Convert.ToString(dr["DirectionIconStyle"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mTransportType = Convert.ToInt32(dr["TransportType"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mTransportIconName = Convert.ToString(dr["TransportIconName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mTransportIconStyle = Convert.ToString(dr["TransportIconStyle"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mShipmentType = Convert.ToInt32(dr["ShipmentType"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mBranchID = Convert.ToInt32(dr["BranchID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mReleaseNumber = Convert.ToString(dr["ReleaseNumber"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mReferenceNumber = Convert.ToString(dr["ReferenceNumber"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mMasterBL = Convert.ToString(dr["MasterBL"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mReference = Convert.ToString(dr["Reference"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mPOLCountryCode = Convert.ToString(dr["POLCountryCode"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mPOLCountryName = Convert.ToString(dr["POLCountryName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mPOLCode = Convert.ToString(dr["POLCode"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mPOLName = Convert.ToString(dr["POLName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mPODCountryCode = Convert.ToString(dr["PODCountryCode"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mPODCountryName = Convert.ToString(dr["PODCountryName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mPODCode = Convert.ToString(dr["PODCode"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mPODName = Convert.ToString(dr["PODName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mRepBLTypeShown = Convert.ToString(dr["RepBLTypeShown"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mClientName = Convert.ToString(dr["ClientName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mBookingPartyID = Convert.ToInt32(dr["BookingPartyID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mBookingPartyName = Convert.ToString(dr["BookingPartyName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mSalesmanID = Convert.ToInt32(dr["SalesmanID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mSalesman = Convert.ToString(dr["Salesman"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mOperationManID = Convert.ToInt32(dr["OperationManID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mOperationManName = Convert.ToString(dr["OperationManName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mCreatorName = Convert.ToString(dr["CreatorName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mCreatorLocalName = Convert.ToString(dr["CreatorLocalName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mCreatorRoleID = Convert.ToInt32(dr["CreatorRoleID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mSalesmanRoleID = Convert.ToInt32(dr["SalesmanRoleID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mOperationManRoleID = Convert.ToInt32(dr["OperationManRoleID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mOperationStageID = Convert.ToInt32(dr["OperationStageID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mOperationStageName = Convert.ToString(dr["OperationStageName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mQuotationID = Convert.ToInt64(dr["QuotationID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mLineID = Convert.ToInt32(dr["LineID"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mLineName = Convert.ToString(dr["LineName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mExpectedDeparture = Convert.ToDateTime(dr["ExpectedDeparture"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mActualDeparture = Convert.ToDateTime(dr["ActualDeparture"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mExpectedArrival = Convert.ToDateTime(dr["ExpectedArrival"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mActualArrival = Convert.ToDateTime(dr["ActualArrival"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mTrackingStageName = Convert.ToString(dr["TrackingStageName"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mBookingNumbers = Convert.ToString(dr["BookingNumbers"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mContainerTypes = Convert.ToString(dr["ContainerTypes"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mPackageTypes = Convert.ToString(dr["PackageTypes"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mForm13Number = Convert.ToString(dr["Form13Number"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mACIDNumber = Convert.ToString(dr["ACIDNumber"].ToString());
                        ObjCVarvwOperationsForBindTableRows.mACIDDetails = Convert.ToString(dr["ACIDDetails"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwOperationsForBindTableRows.Add(ObjCVarvwOperationsForBindTableRows);
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