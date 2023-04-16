using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

//Com.CommandTimeout = 2000;
namespace Forwarding.MvcApp.Models.Operations.Operations.Customized
{
    [Serializable]
    public partial class CVarvwCCRoutings
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mID;
        internal String mOperationCode;
        internal String mCCAInvoiceNumber;
        internal String mMasterBL;
        internal String mCertificateNumber;
        internal String mCertificateValue;
        internal DateTime mActualArrival;
        internal String mShipmentTypeCode;
        internal Int32 mNumberOfPackages;
        internal DateTime mCCASpendDate;
        internal DateTime mCCADocumentReceiveDate;
        internal DateTime mSalesDateReceived;
        internal String mCCAOthers;
        internal DateTime mSalesDateDelivered;
        internal DateTime mCommerceDateReceived;
        internal DateTime mCommerceDateDelivered;
        internal DateTime mInspectionDateReceived;
        internal DateTime mInspectionDateDelivered;
        internal DateTime mFinishDateReceived;
        internal DateTime mFinishDateDelivered;
        internal DateTime mCCDropBackReceived;
        internal DateTime mCCDropBackDelivered;
        internal DateTime mCCAllowTemporaryReceived;
        internal DateTime mCCAllowTemporaryDelivered;
        internal String mStageName;
        #endregion

        #region "Methods"
        public Int64 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public String OperationCode
        {
            get { return mOperationCode; }
            set { mOperationCode = value; }
        }
        public String CCAInvoiceNumber
        {
            get { return mCCAInvoiceNumber; }
            set { mCCAInvoiceNumber = value; }
        }
        public String MasterBL
        {
            get { return mMasterBL; }
            set { mMasterBL = value; }
        }
        public String CertificateNumber
        {
            get { return mCertificateNumber; }
            set { mCertificateNumber = value; }
        }
        public String CertificateValue
        {
            get { return mCertificateValue; }
            set { mCertificateValue = value; }
        }
        public DateTime ActualArrival
        {
            get { return mActualArrival; }
            set { mActualArrival = value; }
        }
        public String ShipmentTypeCode
        {
            get { return mShipmentTypeCode; }
            set { mShipmentTypeCode = value; }
        }
        public Int32 NumberOfPackages
        {
            get { return mNumberOfPackages; }
            set { mNumberOfPackages = value; }
        }
        public DateTime CCASpendDate
        {
            get { return mCCASpendDate; }
            set { mCCASpendDate = value; }
        }
        public DateTime CCADocumentReceiveDate
        {
            get { return mCCADocumentReceiveDate; }
            set { mCCADocumentReceiveDate = value; }
        }
        public DateTime SalesDateReceived
        {
            get { return mSalesDateReceived; }
            set { mSalesDateReceived = value; }
        }
        public String CCAOthers
        {
            get { return mCCAOthers; }
            set { mCCAOthers = value; }
        }
        public DateTime SalesDateDelivered
        {
            get { return mSalesDateDelivered; }
            set { mSalesDateDelivered = value; }
        }
        public DateTime CommerceDateReceived
        {
            get { return mCommerceDateReceived; }
            set { mCommerceDateReceived = value; }
        }
        public DateTime CommerceDateDelivered
        {
            get { return mCommerceDateDelivered; }
            set { mCommerceDateDelivered = value; }
        }
        public DateTime InspectionDateReceived
        {
            get { return mInspectionDateReceived; }
            set { mInspectionDateReceived = value; }
        }
        public DateTime InspectionDateDelivered
        {
            get { return mInspectionDateDelivered; }
            set { mInspectionDateDelivered = value; }
        }
        public DateTime FinishDateReceived
        {
            get { return mFinishDateReceived; }
            set { mFinishDateReceived = value; }
        }
        public DateTime FinishDateDelivered
        {
            get { return mFinishDateDelivered; }
            set { mFinishDateDelivered = value; }
        }
        public DateTime CCDropBackReceived
        {
            get { return mCCDropBackReceived; }
            set { mCCDropBackReceived = value; }
        }
        public DateTime CCDropBackDelivered
        {
            get { return mCCDropBackDelivered; }
            set { mCCDropBackDelivered = value; }
        }
        public DateTime CCAllowTemporaryReceived
        {
            get { return mCCAllowTemporaryReceived; }
            set { mCCAllowTemporaryReceived = value; }
        }
        public DateTime CCAllowTemporaryDelivered
        {
            get { return mCCAllowTemporaryDelivered; }
            set { mCCAllowTemporaryDelivered = value; }
        }
        public String StageName
        {
            get { return mStageName; }
            set { mStageName = value; }
        }
        #endregion
    }

    public partial class CvwCCRoutings
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
        public List<CVarvwCCRoutings> lstCVarvwCCRoutings = new List<CVarvwCCRoutings>();
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
            lstCVarvwCCRoutings.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwCCRoutings";
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
                        CVarvwCCRoutings ObjCVarvwCCRoutings = new CVarvwCCRoutings();
                        ObjCVarvwCCRoutings.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwCCRoutings.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwCCRoutings.mCCAInvoiceNumber = Convert.ToString(dr["CCAInvoiceNumber"].ToString());
                        ObjCVarvwCCRoutings.mMasterBL = Convert.ToString(dr["MasterBL"].ToString());
                        ObjCVarvwCCRoutings.mCertificateNumber = Convert.ToString(dr["CertificateNumber"].ToString());
                        ObjCVarvwCCRoutings.mCertificateValue = Convert.ToString(dr["CertificateValue"].ToString());
                        ObjCVarvwCCRoutings.mActualArrival = Convert.ToDateTime(dr["ActualArrival"].ToString());
                        ObjCVarvwCCRoutings.mShipmentTypeCode = Convert.ToString(dr["ShipmentTypeCode"].ToString());
                        ObjCVarvwCCRoutings.mNumberOfPackages = Convert.ToInt32(dr["NumberOfPackages"].ToString());
                        ObjCVarvwCCRoutings.mCCASpendDate = Convert.ToDateTime(dr["CCASpendDate"].ToString());
                        ObjCVarvwCCRoutings.mCCADocumentReceiveDate = Convert.ToDateTime(dr["CCADocumentReceiveDate"].ToString());
                        ObjCVarvwCCRoutings.mSalesDateReceived = Convert.ToDateTime(dr["SalesDateReceived"].ToString());
                        ObjCVarvwCCRoutings.mCCAOthers = Convert.ToString(dr["CCAOthers"].ToString());
                        ObjCVarvwCCRoutings.mSalesDateDelivered = Convert.ToDateTime(dr["SalesDateDelivered"].ToString());
                        ObjCVarvwCCRoutings.mCommerceDateReceived = Convert.ToDateTime(dr["CommerceDateReceived"].ToString());
                        ObjCVarvwCCRoutings.mCommerceDateDelivered = Convert.ToDateTime(dr["CommerceDateDelivered"].ToString());
                        ObjCVarvwCCRoutings.mInspectionDateReceived = Convert.ToDateTime(dr["InspectionDateReceived"].ToString());
                        ObjCVarvwCCRoutings.mInspectionDateDelivered = Convert.ToDateTime(dr["InspectionDateDelivered"].ToString());
                        ObjCVarvwCCRoutings.mFinishDateReceived = Convert.ToDateTime(dr["FinishDateReceived"].ToString());
                        ObjCVarvwCCRoutings.mFinishDateDelivered = Convert.ToDateTime(dr["FinishDateDelivered"].ToString());
                        ObjCVarvwCCRoutings.mCCDropBackReceived = Convert.ToDateTime(dr["CCDropBackReceived"].ToString());
                        ObjCVarvwCCRoutings.mCCDropBackDelivered = Convert.ToDateTime(dr["CCDropBackDelivered"].ToString());
                        ObjCVarvwCCRoutings.mCCAllowTemporaryReceived = Convert.ToDateTime(dr["CCAllowTemporaryReceived"].ToString());
                        ObjCVarvwCCRoutings.mCCAllowTemporaryDelivered = Convert.ToDateTime(dr["CCAllowTemporaryDelivered"].ToString());
                        ObjCVarvwCCRoutings.mStageName = Convert.ToString(dr["StageName"].ToString());
                        lstCVarvwCCRoutings.Add(ObjCVarvwCCRoutings);
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
            lstCVarvwCCRoutings.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwCCRoutings";
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
                        CVarvwCCRoutings ObjCVarvwCCRoutings = new CVarvwCCRoutings();
                        ObjCVarvwCCRoutings.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwCCRoutings.mOperationCode = Convert.ToString(dr["OperationCode"].ToString());
                        ObjCVarvwCCRoutings.mCCAInvoiceNumber = Convert.ToString(dr["CCAInvoiceNumber"].ToString());
                        ObjCVarvwCCRoutings.mMasterBL = Convert.ToString(dr["MasterBL"].ToString());
                        ObjCVarvwCCRoutings.mCertificateNumber = Convert.ToString(dr["CertificateNumber"].ToString());
                        ObjCVarvwCCRoutings.mCertificateValue = Convert.ToString(dr["CertificateValue"].ToString());
                        ObjCVarvwCCRoutings.mActualArrival = Convert.ToDateTime(dr["ActualArrival"].ToString());
                        ObjCVarvwCCRoutings.mShipmentTypeCode = Convert.ToString(dr["ShipmentTypeCode"].ToString());
                        ObjCVarvwCCRoutings.mNumberOfPackages = Convert.ToInt32(dr["NumberOfPackages"].ToString());
                        ObjCVarvwCCRoutings.mCCASpendDate = Convert.ToDateTime(dr["CCASpendDate"].ToString());
                        ObjCVarvwCCRoutings.mCCADocumentReceiveDate = Convert.ToDateTime(dr["CCADocumentReceiveDate"].ToString());
                        ObjCVarvwCCRoutings.mSalesDateReceived = Convert.ToDateTime(dr["SalesDateReceived"].ToString());
                        ObjCVarvwCCRoutings.mCCAOthers = Convert.ToString(dr["CCAOthers"].ToString());
                        ObjCVarvwCCRoutings.mSalesDateDelivered = Convert.ToDateTime(dr["SalesDateDelivered"].ToString());
                        ObjCVarvwCCRoutings.mCommerceDateReceived = Convert.ToDateTime(dr["CommerceDateReceived"].ToString());
                        ObjCVarvwCCRoutings.mCommerceDateDelivered = Convert.ToDateTime(dr["CommerceDateDelivered"].ToString());
                        ObjCVarvwCCRoutings.mInspectionDateReceived = Convert.ToDateTime(dr["InspectionDateReceived"].ToString());
                        ObjCVarvwCCRoutings.mInspectionDateDelivered = Convert.ToDateTime(dr["InspectionDateDelivered"].ToString());
                        ObjCVarvwCCRoutings.mFinishDateReceived = Convert.ToDateTime(dr["FinishDateReceived"].ToString());
                        ObjCVarvwCCRoutings.mFinishDateDelivered = Convert.ToDateTime(dr["FinishDateDelivered"].ToString());
                        ObjCVarvwCCRoutings.mCCDropBackReceived = Convert.ToDateTime(dr["CCDropBackReceived"].ToString());
                        ObjCVarvwCCRoutings.mCCDropBackDelivered = Convert.ToDateTime(dr["CCDropBackDelivered"].ToString());
                        ObjCVarvwCCRoutings.mCCAllowTemporaryReceived = Convert.ToDateTime(dr["CCAllowTemporaryReceived"].ToString());
                        ObjCVarvwCCRoutings.mCCAllowTemporaryDelivered = Convert.ToDateTime(dr["CCAllowTemporaryDelivered"].ToString());
                        ObjCVarvwCCRoutings.mStageName = Convert.ToString(dr["StageName"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwCCRoutings.Add(ObjCVarvwCCRoutings);
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
