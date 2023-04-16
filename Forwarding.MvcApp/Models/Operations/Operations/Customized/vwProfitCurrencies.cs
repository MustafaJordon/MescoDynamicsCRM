using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

//Com.CommandTimeout = 2000;
namespace Forwarding.MvcApp.Models.Operations.Operations.Generated
{
    [Serializable]
    public partial class CVarvwProfitCurrencies
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mOperationID;
        internal String mOperationCode;
        internal Int64 mMasterOperationID;
        internal String mClientName;
        internal String mBookingPartyName;
        internal DateTime mOpenDate;
        internal String mReleaseNumber;
        internal Decimal mChargeableWeightSum;
        internal String mContainerTypes;
        internal DateTime mActualDeparture;
        internal Int32 mBranchID;
        internal Int32 mMoveTypeID;
        internal String mBranchName;
        internal String mInvoiceNumbers;
        internal Int32 mFirstInvoiceDate;
        internal String mPOValue;
        internal DateTime mPODate;
        internal String mShipperName;
        internal String mConsigneeName;
        internal String mMoveTypeName;
        internal String mOperationStageName;
        internal String mPOLCode;
        internal String mPODCode;
        internal String mCurrencyCode;
        internal Int32 mChargeTypeID;
        internal String mChargeTypeName;
        internal Boolean mIsOfficial;
        internal Decimal mQuotationCost;
        internal Decimal mPayablesWithVAT;
        internal Decimal mPayablesWithoutVAT;
        internal Decimal mReceivablesWithVAT;
        internal Decimal mReceivablesWithoutVAT;
        internal Decimal mExchangeRate;
        internal Int32 mIsUsedInOperationStatement;
        internal Decimal mCostAmount;
        internal Decimal mPaidAmount;
        internal Decimal mRemainingAmount;
        internal String mPayableStatus;
        internal Int32 mBillTo;
        internal String mBillToName;
        internal Int32 mSupplierPartnerTypeID;
        internal Int32 mPartnerSupplierID;
        internal String mPartnerSupplierName;
        internal DateTime mPaymentDate;
        internal String mAccNoteType;
        internal DateTime mPayableDate;
        internal DateTime mInvoiceDate;
        #endregion

        #region "Methods"
        public Int64 OperationID
        {
            get { return mOperationID; }
            set { mOperationID = value; }
        }
        public String OperationCode
        {
            get { return mOperationCode; }
            set { mOperationCode = value; }
        }
        public Int64 MasterOperationID
        {
            get { return mMasterOperationID; }
            set { mMasterOperationID = value; }
        }
        public String ClientName
        {
            get { return mClientName; }
            set { mClientName = value; }
        }
        public String BookingPartyName
        {
            get { return mBookingPartyName; }
            set { mBookingPartyName = value; }
        }
        public DateTime OpenDate
        {
            get { return mOpenDate; }
            set { mOpenDate = value; }
        }
        public String ReleaseNumber
        {
            get { return mReleaseNumber; }
            set { mReleaseNumber = value; }
        }
        public Decimal ChargeableWeightSum
        {
            get { return mChargeableWeightSum; }
            set { mChargeableWeightSum = value; }
        }
        public String ContainerTypes
        {
            get { return mContainerTypes; }
            set { mContainerTypes = value; }
        }
        public DateTime ActualDeparture
        {
            get { return mActualDeparture; }
            set { mActualDeparture = value; }
        }
        public Int32 BranchID
        {
            get { return mBranchID; }
            set { mBranchID = value; }
        }
        public Int32 MoveTypeID
        {
            get { return mMoveTypeID; }
            set { mMoveTypeID = value; }
        }
        public String BranchName
        {
            get { return mBranchName; }
            set { mBranchName = value; }
        }
        public String InvoiceNumbers
        {
            get { return mInvoiceNumbers; }
            set { mInvoiceNumbers = value; }
        }
        public Int32 FirstInvoiceDate
        {
            get { return mFirstInvoiceDate; }
            set { mFirstInvoiceDate = value; }
        }
        public String POValue
        {
            get { return mPOValue; }
            set { mPOValue = value; }
        }
        public DateTime PODate
        {
            get { return mPODate; }
            set { mPODate = value; }
        }
        public String ShipperName
        {
            get { return mShipperName; }
            set { mShipperName = value; }
        }
        public String ConsigneeName
        {
            get { return mConsigneeName; }
            set { mConsigneeName = value; }
        }
        public String MoveTypeName
        {
            get { return mMoveTypeName; }
            set { mMoveTypeName = value; }
        }
        public String OperationStageName
        {
            get { return mOperationStageName; }
            set { mOperationStageName = value; }
        }
        public String POLCode
        {
            get { return mPOLCode; }
            set { mPOLCode = value; }
        }
        public String PODCode
        {
            get { return mPODCode; }
            set { mPODCode = value; }
        }
        public String CurrencyCode
        {
            get { return mCurrencyCode; }
            set { mCurrencyCode = value; }
        }
        public Int32 ChargeTypeID
        {
            get { return mChargeTypeID; }
            set { mChargeTypeID = value; }
        }
        public String ChargeTypeName
        {
            get { return mChargeTypeName; }
            set { mChargeTypeName = value; }
        }
        public Boolean IsOfficial
        {
            get { return mIsOfficial; }
            set { mIsOfficial = value; }
        }
        public Decimal QuotationCost
        {
            get { return mQuotationCost; }
            set { mQuotationCost = value; }
        }
        public Decimal PayablesWithVAT
        {
            get { return mPayablesWithVAT; }
            set { mPayablesWithVAT = value; }
        }
        public Decimal PayablesWithoutVAT
        {
            get { return mPayablesWithoutVAT; }
            set { mPayablesWithoutVAT = value; }
        }
        public Decimal ReceivablesWithVAT
        {
            get { return mReceivablesWithVAT; }
            set { mReceivablesWithVAT = value; }
        }
        public Decimal ReceivablesWithoutVAT
        {
            get { return mReceivablesWithoutVAT; }
            set { mReceivablesWithoutVAT = value; }
        }
        public Decimal ExchangeRate
        {
            get { return mExchangeRate; }
            set { mExchangeRate = value; }
        }
        public Int32 IsUsedInOperationStatement
        {
            get { return mIsUsedInOperationStatement; }
            set { mIsUsedInOperationStatement = value; }
        }
        public Decimal CostAmount
        {
            get { return mCostAmount; }
            set { mCostAmount = value; }
        }
        public Decimal PaidAmount
        {
            get { return mPaidAmount; }
            set { mPaidAmount = value; }
        }
        public Decimal RemainingAmount
        {
            get { return mRemainingAmount; }
            set { mRemainingAmount = value; }
        }
        public String PayableStatus
        {
            get { return mPayableStatus; }
            set { mPayableStatus = value; }
        }
        public Int32 BillTo
        {
            get { return mBillTo; }
            set { mBillTo = value; }
        }
        public String BillToName
        {
            get { return mBillToName; }
            set { mBillToName = value; }
        }
        public Int32 SupplierPartnerTypeID
        {
            get { return mSupplierPartnerTypeID; }
            set { mSupplierPartnerTypeID = value; }
        }
        public Int32 PartnerSupplierID
        {
            get { return mPartnerSupplierID; }
            set { mPartnerSupplierID = value; }
        }
        public String PartnerSupplierName
        {
            get { return mPartnerSupplierName; }
            set { mPartnerSupplierName = value; }
        }
        public DateTime PaymentDate
        {
            get { return mPaymentDate; }
            set { mPaymentDate = value; }
        }
        public String AccNoteType
        {
            get { return mAccNoteType; }
            set { mAccNoteType = value; }
        }
        public DateTime PayableDate
        {
            get { return mPayableDate; }
            set { mPayableDate = value; }
        }
        public DateTime InvoiceDate
        {
            get { return mInvoiceDate; }
            set { mInvoiceDate = value; }
        }
        #endregion
    }

    public partial class CvwProfitCurrencies
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
        public List<CVarvwProfitCurrencies> lstCVarvwProfitCurrencies = new List<CVarvwProfitCurrencies>();
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
            lstCVarvwProfitCurrencies.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwProfitCurrencies";
                    Com.Parameters[0].Value = Param;
                }
                Com.Transaction = tr;
                Com.Connection = Con;
                Com.CommandTimeout = 2000;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        /*Start DataReader*/
                        CVarvwProfitCurrencies ObjCVarvwProfitCurrencies = new CVarvwProfitCurrencies();
                        ObjCVarvwProfitCurrencies.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwProfitCurrencies.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwProfitCurrencies.mMasterOperationID = Convert.ToInt64(dr["MasterOperationID"].ToString());
                        ObjCVarvwProfitCurrencies.mClientName = Convert.ToString(dr["ClientName"].ToString());
                        ObjCVarvwProfitCurrencies.mBookingPartyName = Convert.ToString(dr["BookingPartyName"].ToString());
                        ObjCVarvwProfitCurrencies.mOpenDate = Convert.ToDateTime(dr["OpenDate"].ToString());
                        ObjCVarvwProfitCurrencies.mReleaseNumber = Convert.ToString(dr["ReleaseNumber"].ToString());
                        ObjCVarvwProfitCurrencies.mChargeableWeightSum = Convert.ToDecimal(dr["ChargeableWeightSum"].ToString());
                        ObjCVarvwProfitCurrencies.mContainerTypes = Convert.ToString(dr["ContainerTypes"].ToString());
                        ObjCVarvwProfitCurrencies.mActualDeparture = Convert.ToDateTime(dr["ActualDeparture"].ToString());
                        ObjCVarvwProfitCurrencies.mBranchID = Convert.ToInt32(dr["BranchID"].ToString());
                        ObjCVarvwProfitCurrencies.mMoveTypeID = Convert.ToInt32(dr["MoveTypeID"].ToString());
                        ObjCVarvwProfitCurrencies.mBranchName = Convert.ToString(dr["BranchName"].ToString());
                        ObjCVarvwProfitCurrencies.mInvoiceNumbers = Convert.ToString(dr["InvoiceNumbers"].ToString());
                        ObjCVarvwProfitCurrencies.mFirstInvoiceDate = Convert.ToInt32(dr["FirstInvoiceDate"].ToString());
                        ObjCVarvwProfitCurrencies.mPOValue = Convert.ToString(dr["POValue"].ToString());
                        ObjCVarvwProfitCurrencies.mPODate = Convert.ToDateTime(dr["PODate"].ToString());
                        ObjCVarvwProfitCurrencies.mShipperName = Convert.ToString(dr["ShipperName"].ToString());
                        ObjCVarvwProfitCurrencies.mConsigneeName = Convert.ToString(dr["ConsigneeName"].ToString());
                        ObjCVarvwProfitCurrencies.mMoveTypeName = Convert.ToString(dr["MoveTypeName"].ToString());
                        ObjCVarvwProfitCurrencies.mOperationStageName = Convert.ToString(dr["OperationStageName"].ToString());
                        ObjCVarvwProfitCurrencies.mPOLCode = Convert.ToString(dr["POLCode"].ToString());
                        ObjCVarvwProfitCurrencies.mPODCode = Convert.ToString(dr["PODCode"].ToString());
                        ObjCVarvwProfitCurrencies.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwProfitCurrencies.mChargeTypeID = Convert.ToInt32(dr["ChargeTypeID"].ToString());
                        ObjCVarvwProfitCurrencies.mChargeTypeName = Convert.ToString(dr["ChargeTypeName"].ToString());
                        ObjCVarvwProfitCurrencies.mIsOfficial = Convert.ToBoolean(dr["IsOfficial"].ToString());
                        ObjCVarvwProfitCurrencies.mQuotationCost = Convert.ToDecimal(dr["QuotationCost"].ToString());
                        ObjCVarvwProfitCurrencies.mPayablesWithVAT = Convert.ToDecimal(dr["PayablesWithVAT"].ToString());
                        ObjCVarvwProfitCurrencies.mPayablesWithoutVAT = Convert.ToDecimal(dr["PayablesWithoutVAT"].ToString());
                        ObjCVarvwProfitCurrencies.mReceivablesWithVAT = Convert.ToDecimal(dr["ReceivablesWithVAT"].ToString());
                        ObjCVarvwProfitCurrencies.mReceivablesWithoutVAT = Convert.ToDecimal(dr["ReceivablesWithoutVAT"].ToString());
                        ObjCVarvwProfitCurrencies.mExchangeRate = Convert.ToDecimal(dr["ExchangeRate"].ToString());
                        ObjCVarvwProfitCurrencies.mIsUsedInOperationStatement = Convert.ToInt32(dr["IsUsedInOperationStatement"].ToString());
                        ObjCVarvwProfitCurrencies.mCostAmount = Convert.ToDecimal(dr["CostAmount"].ToString());
                        ObjCVarvwProfitCurrencies.mPaidAmount = Convert.ToDecimal(dr["PaidAmount"].ToString());
                        ObjCVarvwProfitCurrencies.mRemainingAmount = Convert.ToDecimal(dr["RemainingAmount"].ToString());
                        ObjCVarvwProfitCurrencies.mPayableStatus = Convert.ToString(dr["PayableStatus"].ToString());
                        ObjCVarvwProfitCurrencies.mBillTo = Convert.ToInt32(dr["BillTo"].ToString());
                        ObjCVarvwProfitCurrencies.mBillToName = Convert.ToString(dr["BillToName"].ToString());
                        ObjCVarvwProfitCurrencies.mSupplierPartnerTypeID = Convert.ToInt32(dr["SupplierPartnerTypeID"].ToString());
                        ObjCVarvwProfitCurrencies.mPartnerSupplierID = Convert.ToInt32(dr["PartnerSupplierID"].ToString());
                        ObjCVarvwProfitCurrencies.mPartnerSupplierName = Convert.ToString(dr["PartnerSupplierName"].ToString());
                        ObjCVarvwProfitCurrencies.mPaymentDate = Convert.ToDateTime(dr["PaymentDate"].ToString());
                        ObjCVarvwProfitCurrencies.mAccNoteType = Convert.ToString(dr["AccNoteType"].ToString());
                        ObjCVarvwProfitCurrencies.mPayableDate = Convert.ToDateTime(dr["PayableDate"].ToString());
                        ObjCVarvwProfitCurrencies.mInvoiceDate = Convert.ToDateTime(dr["InvoiceDate"].ToString());
                        lstCVarvwProfitCurrencies.Add(ObjCVarvwProfitCurrencies);
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
            lstCVarvwProfitCurrencies.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwProfitCurrencies";
                Com.Parameters[0].Value = PageSize;
                Com.Parameters[1].Value = PageNumber;
                Com.Parameters[2].Value = WhereClause;
                Com.Parameters[3].Value = OrderBy;
                Com.Transaction = tr;
                Com.Connection = Con;
                Com.CommandTimeout = 2000;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        /*Start DataReader*/
                        CVarvwProfitCurrencies ObjCVarvwProfitCurrencies = new CVarvwProfitCurrencies();
                        ObjCVarvwProfitCurrencies.mOperationID = Convert.ToInt64(dr["OperationID"].ToString());
                        ObjCVarvwProfitCurrencies.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwProfitCurrencies.mMasterOperationID = Convert.ToInt64(dr["MasterOperationID"].ToString());
                        ObjCVarvwProfitCurrencies.mClientName = Convert.ToString(dr["ClientName"].ToString());
                        ObjCVarvwProfitCurrencies.mBookingPartyName = Convert.ToString(dr["BookingPartyName"].ToString());
                        ObjCVarvwProfitCurrencies.mOpenDate = Convert.ToDateTime(dr["OpenDate"].ToString());
                        ObjCVarvwProfitCurrencies.mReleaseNumber = Convert.ToString(dr["ReleaseNumber"].ToString());
                        ObjCVarvwProfitCurrencies.mChargeableWeightSum = Convert.ToDecimal(dr["ChargeableWeightSum"].ToString());
                        ObjCVarvwProfitCurrencies.mContainerTypes = Convert.ToString(dr["ContainerTypes"].ToString());
                        ObjCVarvwProfitCurrencies.mActualDeparture = Convert.ToDateTime(dr["ActualDeparture"].ToString());
                        ObjCVarvwProfitCurrencies.mBranchID = Convert.ToInt32(dr["BranchID"].ToString());
                        ObjCVarvwProfitCurrencies.mMoveTypeID = Convert.ToInt32(dr["MoveTypeID"].ToString());
                        ObjCVarvwProfitCurrencies.mBranchName = Convert.ToString(dr["BranchName"].ToString());
                        ObjCVarvwProfitCurrencies.mInvoiceNumbers = Convert.ToString(dr["InvoiceNumbers"].ToString());
                        ObjCVarvwProfitCurrencies.mFirstInvoiceDate = Convert.ToInt32(dr["FirstInvoiceDate"].ToString());
                        ObjCVarvwProfitCurrencies.mPOValue = Convert.ToString(dr["POValue"].ToString());
                        ObjCVarvwProfitCurrencies.mPODate = Convert.ToDateTime(dr["PODate"].ToString());
                        ObjCVarvwProfitCurrencies.mShipperName = Convert.ToString(dr["ShipperName"].ToString());
                        ObjCVarvwProfitCurrencies.mConsigneeName = Convert.ToString(dr["ConsigneeName"].ToString());
                        ObjCVarvwProfitCurrencies.mMoveTypeName = Convert.ToString(dr["MoveTypeName"].ToString());
                        ObjCVarvwProfitCurrencies.mOperationStageName = Convert.ToString(dr["OperationStageName"].ToString());
                        ObjCVarvwProfitCurrencies.mPOLCode = Convert.ToString(dr["POLCode"].ToString());
                        ObjCVarvwProfitCurrencies.mPODCode = Convert.ToString(dr["PODCode"].ToString());
                        ObjCVarvwProfitCurrencies.mCurrencyCode = Convert.ToString(dr["CurrencyCode"].ToString());
                        ObjCVarvwProfitCurrencies.mChargeTypeID = Convert.ToInt32(dr["ChargeTypeID"].ToString());
                        ObjCVarvwProfitCurrencies.mChargeTypeName = Convert.ToString(dr["ChargeTypeName"].ToString());
                        ObjCVarvwProfitCurrencies.mIsOfficial = Convert.ToBoolean(dr["IsOfficial"].ToString());
                        ObjCVarvwProfitCurrencies.mQuotationCost = Convert.ToDecimal(dr["QuotationCost"].ToString());
                        ObjCVarvwProfitCurrencies.mPayablesWithVAT = Convert.ToDecimal(dr["PayablesWithVAT"].ToString());
                        ObjCVarvwProfitCurrencies.mPayablesWithoutVAT = Convert.ToDecimal(dr["PayablesWithoutVAT"].ToString());
                        ObjCVarvwProfitCurrencies.mReceivablesWithVAT = Convert.ToDecimal(dr["ReceivablesWithVAT"].ToString());
                        ObjCVarvwProfitCurrencies.mReceivablesWithoutVAT = Convert.ToDecimal(dr["ReceivablesWithoutVAT"].ToString());
                        ObjCVarvwProfitCurrencies.mExchangeRate = Convert.ToDecimal(dr["ExchangeRate"].ToString());
                        ObjCVarvwProfitCurrencies.mIsUsedInOperationStatement = Convert.ToInt32(dr["IsUsedInOperationStatement"].ToString());
                        ObjCVarvwProfitCurrencies.mCostAmount = Convert.ToDecimal(dr["CostAmount"].ToString());
                        ObjCVarvwProfitCurrencies.mPaidAmount = Convert.ToDecimal(dr["PaidAmount"].ToString());
                        ObjCVarvwProfitCurrencies.mRemainingAmount = Convert.ToDecimal(dr["RemainingAmount"].ToString());
                        ObjCVarvwProfitCurrencies.mPayableStatus = Convert.ToString(dr["PayableStatus"].ToString());
                        ObjCVarvwProfitCurrencies.mBillTo = Convert.ToInt32(dr["BillTo"].ToString());
                        ObjCVarvwProfitCurrencies.mBillToName = Convert.ToString(dr["BillToName"].ToString());
                        ObjCVarvwProfitCurrencies.mSupplierPartnerTypeID = Convert.ToInt32(dr["SupplierPartnerTypeID"].ToString());
                        ObjCVarvwProfitCurrencies.mPartnerSupplierID = Convert.ToInt32(dr["PartnerSupplierID"].ToString());
                        ObjCVarvwProfitCurrencies.mPartnerSupplierName = Convert.ToString(dr["PartnerSupplierName"].ToString());
                        ObjCVarvwProfitCurrencies.mPaymentDate = Convert.ToDateTime(dr["PaymentDate"].ToString());
                        ObjCVarvwProfitCurrencies.mAccNoteType = Convert.ToString(dr["AccNoteType"].ToString());
                        ObjCVarvwProfitCurrencies.mPayableDate = Convert.ToDateTime(dr["PayableDate"].ToString());
                        ObjCVarvwProfitCurrencies.mInvoiceDate = Convert.ToDateTime(dr["InvoiceDate"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwProfitCurrencies.Add(ObjCVarvwProfitCurrencies);
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
