using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.ReceiptsAndPayments.Custodies.Generated
{
    [Serializable]
    public class CPKA_PaymentRequest
    {
        #region "variables"
        private Int64 mID;
        #endregion

        #region "Methods"
        public Int64 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        #endregion
    }
    [Serializable]
    public partial class CVarA_PaymentRequest : CPKA_PaymentRequest
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mCode;
        internal Int32 mCustodyID;
        internal DateTime mRequestDate;
        internal DateTime mSettlementDate;
        internal Int32 mCurrencyID;
        internal Boolean mIsCheck;
        internal Decimal mTotalEstmatedCost;
        internal Decimal mTotalActualCost;
        internal Decimal mTotalDiff;
        internal String mNotes;
        internal Boolean mIsApprovedRequest;
        internal Boolean mIsApprovedSettlement;
        internal Boolean mIsSettled;
        internal Int64 mJVID;
        internal Int64 mJVID2;
        internal Int64 mVoucherID;
        internal Int32 mCreatorUserID_Request;
        internal Int32 mCreatorUserID_Settlement;
        internal DateTime mCreationDate_Request;
        internal DateTime mCreationDate_Settlement;
        internal Int32 mModificatorUserID_Request;
        internal Int32 mModificatorUserID_Settlement;
        internal DateTime mModificationDate_Request;
        internal DateTime mModificationDate_Settlement;
        internal Boolean mIsAccept;
        internal Int32 mSupplierID;
        internal Int32 mChargeTypeGroupID;
        internal String mAttache;
        internal String mPayment;
        internal String mIssuedTo;
        internal Int32 mPaymentComboID;
        internal Int32 mIssuedToCmbo;
        #endregion

        #region "Methods"
        public String Code
        {
            get { return mCode; }
            set { mIsChanges = true; mCode = value; }
        }
        public Int32 CustodyID
        {
            get { return mCustodyID; }
            set { mIsChanges = true; mCustodyID = value; }
        }
        public DateTime RequestDate
        {
            get { return mRequestDate; }
            set { mIsChanges = true; mRequestDate = value; }
        }
        public DateTime SettlementDate
        {
            get { return mSettlementDate; }
            set { mIsChanges = true; mSettlementDate = value; }
        }
        public Int32 CurrencyID
        {
            get { return mCurrencyID; }
            set { mIsChanges = true; mCurrencyID = value; }
        }
        public Boolean IsCheck
        {
            get { return mIsCheck; }
            set { mIsChanges = true; mIsCheck = value; }
        }
        public Decimal TotalEstmatedCost
        {
            get { return mTotalEstmatedCost; }
            set { mIsChanges = true; mTotalEstmatedCost = value; }
        }
        public Decimal TotalActualCost
        {
            get { return mTotalActualCost; }
            set { mIsChanges = true; mTotalActualCost = value; }
        }
        public Decimal TotalDiff
        {
            get { return mTotalDiff; }
            set { mIsChanges = true; mTotalDiff = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mIsChanges = true; mNotes = value; }
        }
        public Boolean IsApprovedRequest
        {
            get { return mIsApprovedRequest; }
            set { mIsChanges = true; mIsApprovedRequest = value; }
        }
        public Boolean IsApprovedSettlement
        {
            get { return mIsApprovedSettlement; }
            set { mIsChanges = true; mIsApprovedSettlement = value; }
        }
        public Boolean IsSettled
        {
            get { return mIsSettled; }
            set { mIsChanges = true; mIsSettled = value; }
        }
        public Int64 JVID
        {
            get { return mJVID; }
            set { mIsChanges = true; mJVID = value; }
        }
        public Int64 JVID2
        {
            get { return mJVID2; }
            set { mIsChanges = true; mJVID2 = value; }
        }
        public Int64 VoucherID
        {
            get { return mVoucherID; }
            set { mIsChanges = true; mVoucherID = value; }
        }
        public Int32 CreatorUserID_Request
        {
            get { return mCreatorUserID_Request; }
            set { mIsChanges = true; mCreatorUserID_Request = value; }
        }
        public Int32 CreatorUserID_Settlement
        {
            get { return mCreatorUserID_Settlement; }
            set { mIsChanges = true; mCreatorUserID_Settlement = value; }
        }
        public DateTime CreationDate_Request
        {
            get { return mCreationDate_Request; }
            set { mIsChanges = true; mCreationDate_Request = value; }
        }
        public DateTime CreationDate_Settlement
        {
            get { return mCreationDate_Settlement; }
            set { mIsChanges = true; mCreationDate_Settlement = value; }
        }
        public Int32 ModificatorUserID_Request
        {
            get { return mModificatorUserID_Request; }
            set { mIsChanges = true; mModificatorUserID_Request = value; }
        }
        public Int32 ModificatorUserID_Settlement
        {
            get { return mModificatorUserID_Settlement; }
            set { mIsChanges = true; mModificatorUserID_Settlement = value; }
        }
        public DateTime ModificationDate_Request
        {
            get { return mModificationDate_Request; }
            set { mIsChanges = true; mModificationDate_Request = value; }
        }
        public DateTime ModificationDate_Settlement
        {
            get { return mModificationDate_Settlement; }
            set { mIsChanges = true; mModificationDate_Settlement = value; }
        }
        public Boolean IsAccept
        {
            get { return mIsAccept; }
            set { mIsChanges = true; mIsAccept = value; }
        }
        public Int32 SupplierID
        {
            get { return mSupplierID; }
            set { mIsChanges = true; mSupplierID = value; }
        }
        public Int32 ChargeTypeGroupID
        {
            get { return mChargeTypeGroupID; }
            set { mIsChanges = true; mChargeTypeGroupID = value; }
        }
        public String Attache
        {
            get { return mAttache; }
            set { mIsChanges = true; mAttache = value; }
        }
        public String Payment
        {
            get { return mPayment; }
            set { mIsChanges = true; mPayment = value; }
        }
        public String IssuedTo
        {
            get { return mIssuedTo; }
            set { mIsChanges = true; mIssuedTo = value; }
        }
        public Int32 PaymentComboID
        {
            get { return mPaymentComboID; }
            set { mIsChanges = true; mPaymentComboID = value; }
        }
        public Int32 IssuedToCmbo
        {
            get { return mIssuedToCmbo; }
            set { mIsChanges = true; mIssuedToCmbo = value; }
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

    public partial class CA_PaymentRequest
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
        public List<CVarA_PaymentRequest> lstCVarA_PaymentRequest = new List<CVarA_PaymentRequest>();
        public List<CPKA_PaymentRequest> lstDeletedCPKA_PaymentRequest = new List<CPKA_PaymentRequest>();
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
        public Exception GetItem(Int64 ID)
        {
            return DataFill(Convert.ToString(ID), false);
        }
        private Exception DataFill(string Param, Boolean IsList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarA_PaymentRequest.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListA_PaymentRequest";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemA_PaymentRequest";
                    Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt));
                    Com.Parameters[0].Value = Convert.ToInt64(Param);
                }
                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        /*Start DataReader*/
                        CVarA_PaymentRequest ObjCVarA_PaymentRequest = new CVarA_PaymentRequest();
                        ObjCVarA_PaymentRequest.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarA_PaymentRequest.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarA_PaymentRequest.mCustodyID = Convert.ToInt32(dr["CustodyID"].ToString());
                        ObjCVarA_PaymentRequest.mRequestDate = Convert.ToDateTime(dr["RequestDate"].ToString());
                        ObjCVarA_PaymentRequest.mSettlementDate = Convert.ToDateTime(dr["SettlementDate"].ToString());
                        ObjCVarA_PaymentRequest.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarA_PaymentRequest.mIsCheck = Convert.ToBoolean(dr["IsCheck"].ToString());
                        ObjCVarA_PaymentRequest.mTotalEstmatedCost = Convert.ToDecimal(dr["TotalEstmatedCost"].ToString());
                        ObjCVarA_PaymentRequest.mTotalActualCost = Convert.ToDecimal(dr["TotalActualCost"].ToString());
                        ObjCVarA_PaymentRequest.mTotalDiff = Convert.ToDecimal(dr["TotalDiff"].ToString());
                        ObjCVarA_PaymentRequest.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarA_PaymentRequest.mIsApprovedRequest = Convert.ToBoolean(dr["IsApprovedRequest"].ToString());
                        ObjCVarA_PaymentRequest.mIsApprovedSettlement = Convert.ToBoolean(dr["IsApprovedSettlement"].ToString());
                        ObjCVarA_PaymentRequest.mIsSettled = Convert.ToBoolean(dr["IsSettled"].ToString());
                        ObjCVarA_PaymentRequest.mJVID = Convert.ToInt64(dr["JVID"].ToString());
                        ObjCVarA_PaymentRequest.mJVID2 = Convert.ToInt64(dr["JVID2"].ToString());
                        ObjCVarA_PaymentRequest.mVoucherID = Convert.ToInt64(dr["VoucherID"].ToString());
                        ObjCVarA_PaymentRequest.mCreatorUserID_Request = Convert.ToInt32(dr["CreatorUserID_Request"].ToString());
                        ObjCVarA_PaymentRequest.mCreatorUserID_Settlement = Convert.ToInt32(dr["CreatorUserID_Settlement"].ToString());
                        ObjCVarA_PaymentRequest.mCreationDate_Request = Convert.ToDateTime(dr["CreationDate_Request"].ToString());
                        ObjCVarA_PaymentRequest.mCreationDate_Settlement = Convert.ToDateTime(dr["CreationDate_Settlement"].ToString());
                        ObjCVarA_PaymentRequest.mModificatorUserID_Request = Convert.ToInt32(dr["ModificatorUserID_Request"].ToString());
                        ObjCVarA_PaymentRequest.mModificatorUserID_Settlement = Convert.ToInt32(dr["ModificatorUserID_Settlement"].ToString());
                        ObjCVarA_PaymentRequest.mModificationDate_Request = Convert.ToDateTime(dr["ModificationDate_Request"].ToString());
                        ObjCVarA_PaymentRequest.mModificationDate_Settlement = Convert.ToDateTime(dr["ModificationDate_Settlement"].ToString());
                        ObjCVarA_PaymentRequest.mIsAccept = Convert.ToBoolean(dr["IsAccept"].ToString());
                        ObjCVarA_PaymentRequest.mSupplierID = Convert.ToInt32(dr["SupplierID"].ToString());
                        ObjCVarA_PaymentRequest.mChargeTypeGroupID = Convert.ToInt32(dr["ChargeTypeGroupID"].ToString());
                        ObjCVarA_PaymentRequest.mAttache = Convert.ToString(dr["Attache"].ToString());
                        ObjCVarA_PaymentRequest.mPayment = Convert.ToString(dr["Payment"].ToString());
                        ObjCVarA_PaymentRequest.mIssuedTo = Convert.ToString(dr["IssuedTo"].ToString());
                        ObjCVarA_PaymentRequest.mPaymentComboID = Convert.ToInt32(dr["PaymentComboID"].ToString());
                        ObjCVarA_PaymentRequest.mIssuedToCmbo = Convert.ToInt32(dr["IssuedToCmbo"].ToString());
                        lstCVarA_PaymentRequest.Add(ObjCVarA_PaymentRequest);
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
            lstCVarA_PaymentRequest.Clear();

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
                Com.CommandText = "[dbo].GetListPagingA_PaymentRequest";
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
                        CVarA_PaymentRequest ObjCVarA_PaymentRequest = new CVarA_PaymentRequest();
                        ObjCVarA_PaymentRequest.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarA_PaymentRequest.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarA_PaymentRequest.mCustodyID = Convert.ToInt32(dr["CustodyID"].ToString());
                        ObjCVarA_PaymentRequest.mRequestDate = Convert.ToDateTime(dr["RequestDate"].ToString());
                        ObjCVarA_PaymentRequest.mSettlementDate = Convert.ToDateTime(dr["SettlementDate"].ToString());
                        ObjCVarA_PaymentRequest.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarA_PaymentRequest.mIsCheck = Convert.ToBoolean(dr["IsCheck"].ToString());
                        ObjCVarA_PaymentRequest.mTotalEstmatedCost = Convert.ToDecimal(dr["TotalEstmatedCost"].ToString());
                        ObjCVarA_PaymentRequest.mTotalActualCost = Convert.ToDecimal(dr["TotalActualCost"].ToString());
                        ObjCVarA_PaymentRequest.mTotalDiff = Convert.ToDecimal(dr["TotalDiff"].ToString());
                        ObjCVarA_PaymentRequest.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarA_PaymentRequest.mIsApprovedRequest = Convert.ToBoolean(dr["IsApprovedRequest"].ToString());
                        ObjCVarA_PaymentRequest.mIsApprovedSettlement = Convert.ToBoolean(dr["IsApprovedSettlement"].ToString());
                        ObjCVarA_PaymentRequest.mIsSettled = Convert.ToBoolean(dr["IsSettled"].ToString());
                        ObjCVarA_PaymentRequest.mJVID = Convert.ToInt64(dr["JVID"].ToString());
                        ObjCVarA_PaymentRequest.mJVID2 = Convert.ToInt64(dr["JVID2"].ToString());
                        ObjCVarA_PaymentRequest.mVoucherID = Convert.ToInt64(dr["VoucherID"].ToString());
                        ObjCVarA_PaymentRequest.mCreatorUserID_Request = Convert.ToInt32(dr["CreatorUserID_Request"].ToString());
                        ObjCVarA_PaymentRequest.mCreatorUserID_Settlement = Convert.ToInt32(dr["CreatorUserID_Settlement"].ToString());
                        ObjCVarA_PaymentRequest.mCreationDate_Request = Convert.ToDateTime(dr["CreationDate_Request"].ToString());
                        ObjCVarA_PaymentRequest.mCreationDate_Settlement = Convert.ToDateTime(dr["CreationDate_Settlement"].ToString());
                        ObjCVarA_PaymentRequest.mModificatorUserID_Request = Convert.ToInt32(dr["ModificatorUserID_Request"].ToString());
                        ObjCVarA_PaymentRequest.mModificatorUserID_Settlement = Convert.ToInt32(dr["ModificatorUserID_Settlement"].ToString());
                        ObjCVarA_PaymentRequest.mModificationDate_Request = Convert.ToDateTime(dr["ModificationDate_Request"].ToString());
                        ObjCVarA_PaymentRequest.mModificationDate_Settlement = Convert.ToDateTime(dr["ModificationDate_Settlement"].ToString());
                        ObjCVarA_PaymentRequest.mIsAccept = Convert.ToBoolean(dr["IsAccept"].ToString());
                        ObjCVarA_PaymentRequest.mSupplierID = Convert.ToInt32(dr["SupplierID"].ToString());
                        ObjCVarA_PaymentRequest.mChargeTypeGroupID = Convert.ToInt32(dr["ChargeTypeGroupID"].ToString());
                        ObjCVarA_PaymentRequest.mAttache = Convert.ToString(dr["Attache"].ToString());
                        ObjCVarA_PaymentRequest.mPayment = Convert.ToString(dr["Payment"].ToString());
                        ObjCVarA_PaymentRequest.mIssuedTo = Convert.ToString(dr["IssuedTo"].ToString());
                        ObjCVarA_PaymentRequest.mPaymentComboID = Convert.ToInt32(dr["PaymentComboID"].ToString());
                        ObjCVarA_PaymentRequest.mIssuedToCmbo = Convert.ToInt32(dr["IssuedToCmbo"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarA_PaymentRequest.Add(ObjCVarA_PaymentRequest);
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
        #region "Common Methods"
        private void BeginTrans(SqlCommand Com, SqlConnection Con)
        {

            tr = Con.BeginTransaction(IsolationLevel.Serializable);
            Com.CommandType = CommandType.StoredProcedure;
        }

        private void EndTrans(SqlCommand Com, SqlConnection Con)
        {

            Com.Transaction = tr;
            Com.Connection = Con;
            Com.ExecuteNonQuery();
            tr.Commit();
        }

        #endregion
        #region "Set List Method"
        private Exception SetList(string WhereClause, Boolean IsDelete)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                if (IsDelete == true)
                    Com.CommandText = "[dbo].DeleteListA_PaymentRequest";
                else
                    Com.CommandText = "[dbo].UpdateListA_PaymentRequest";
                Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                BeginTrans(Com, Con);
                Com.Parameters[0].Value = WhereClause;
                EndTrans(Com, Con);
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
        #region "Delete Methods"
        public Exception DeleteItem(List<CPKA_PaymentRequest> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemA_PaymentRequest";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt));
                foreach (CPKA_PaymentRequest ObjCPKA_PaymentRequest in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt64(ObjCPKA_PaymentRequest.ID);
                    EndTrans(Com, Con);
                }
            }
            catch (Exception ex)
            {
                Exp = ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
                DeleteList.Clear();
            }
            return Exp;
        }

        public Exception DeleteList(string WhereClause)
        {

            return SetList(WhereClause, true);
        }

        #endregion
        #region "Save Methods"
        public Exception SaveMethod(List<CVarA_PaymentRequest> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@Code", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@CustodyID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@RequestDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@SettlementDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@CurrencyID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@IsCheck", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@TotalEstmatedCost", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@TotalActualCost", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@TotalDiff", SqlDbType.Decimal));
                Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@IsApprovedRequest", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@IsApprovedSettlement", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@IsSettled", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@JVID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@JVID2", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@VoucherID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@CreatorUserID_Request", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreatorUserID_Settlement", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreationDate_Request", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@CreationDate_Settlement", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ModificatorUserID_Request", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ModificatorUserID_Settlement", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ModificationDate_Request", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@ModificationDate_Settlement", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@IsAccept", SqlDbType.Bit));
                Com.Parameters.Add(new SqlParameter("@SupplierID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@ChargeTypeGroupID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@Attache", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Payment", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@IssuedTo", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@PaymentComboID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@IssuedToCmbo", SqlDbType.Int));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarA_PaymentRequest ObjCVarA_PaymentRequest in SaveList)
                {
                    if (ObjCVarA_PaymentRequest.mIsChanges == true)
                    {
                        if (ObjCVarA_PaymentRequest.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemA_PaymentRequest";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarA_PaymentRequest.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemA_PaymentRequest";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarA_PaymentRequest.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarA_PaymentRequest.ID;
                        }
                        Com.Parameters["@Code"].Value = ObjCVarA_PaymentRequest.Code;
                        Com.Parameters["@CustodyID"].Value = ObjCVarA_PaymentRequest.CustodyID;
                        Com.Parameters["@RequestDate"].Value = ObjCVarA_PaymentRequest.RequestDate;
                        Com.Parameters["@SettlementDate"].Value = ObjCVarA_PaymentRequest.SettlementDate;
                        Com.Parameters["@CurrencyID"].Value = ObjCVarA_PaymentRequest.CurrencyID;
                        Com.Parameters["@IsCheck"].Value = ObjCVarA_PaymentRequest.IsCheck;
                        Com.Parameters["@TotalEstmatedCost"].Value = ObjCVarA_PaymentRequest.TotalEstmatedCost;
                        Com.Parameters["@TotalActualCost"].Value = ObjCVarA_PaymentRequest.TotalActualCost;
                        Com.Parameters["@TotalDiff"].Value = ObjCVarA_PaymentRequest.TotalDiff;
                        Com.Parameters["@Notes"].Value = ObjCVarA_PaymentRequest.Notes;
                        Com.Parameters["@IsApprovedRequest"].Value = ObjCVarA_PaymentRequest.IsApprovedRequest;
                        Com.Parameters["@IsApprovedSettlement"].Value = ObjCVarA_PaymentRequest.IsApprovedSettlement;
                        Com.Parameters["@IsSettled"].Value = ObjCVarA_PaymentRequest.IsSettled;
                        Com.Parameters["@JVID"].Value = ObjCVarA_PaymentRequest.JVID;
                        Com.Parameters["@JVID2"].Value = ObjCVarA_PaymentRequest.JVID2;
                        Com.Parameters["@VoucherID"].Value = ObjCVarA_PaymentRequest.VoucherID;
                        Com.Parameters["@CreatorUserID_Request"].Value = ObjCVarA_PaymentRequest.CreatorUserID_Request;
                        Com.Parameters["@CreatorUserID_Settlement"].Value = ObjCVarA_PaymentRequest.CreatorUserID_Settlement;
                        Com.Parameters["@CreationDate_Request"].Value = ObjCVarA_PaymentRequest.CreationDate_Request;
                        Com.Parameters["@CreationDate_Settlement"].Value = ObjCVarA_PaymentRequest.CreationDate_Settlement;
                        Com.Parameters["@ModificatorUserID_Request"].Value = ObjCVarA_PaymentRequest.ModificatorUserID_Request;
                        Com.Parameters["@ModificatorUserID_Settlement"].Value = ObjCVarA_PaymentRequest.ModificatorUserID_Settlement;
                        Com.Parameters["@ModificationDate_Request"].Value = ObjCVarA_PaymentRequest.ModificationDate_Request;
                        Com.Parameters["@ModificationDate_Settlement"].Value = ObjCVarA_PaymentRequest.ModificationDate_Settlement;
                        Com.Parameters["@IsAccept"].Value = ObjCVarA_PaymentRequest.IsAccept;
                        Com.Parameters["@SupplierID"].Value = ObjCVarA_PaymentRequest.SupplierID;
                        Com.Parameters["@ChargeTypeGroupID"].Value = ObjCVarA_PaymentRequest.ChargeTypeGroupID;
                        Com.Parameters["@Attache"].Value = ObjCVarA_PaymentRequest.Attache;
                        Com.Parameters["@Payment"].Value = ObjCVarA_PaymentRequest.Payment;
                        Com.Parameters["@IssuedTo"].Value = ObjCVarA_PaymentRequest.IssuedTo;
                        Com.Parameters["@PaymentComboID"].Value = ObjCVarA_PaymentRequest.PaymentComboID;
                        Com.Parameters["@IssuedToCmbo"].Value = ObjCVarA_PaymentRequest.IssuedToCmbo;
                        EndTrans(Com, Con);
                        if (ObjCVarA_PaymentRequest.ID == 0)
                        {
                            ObjCVarA_PaymentRequest.ID = Convert.ToInt64(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarA_PaymentRequest.mIsChanges = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Exp = ex;
                if (tr != null)
                    tr.Rollback();
            }
            finally
            {
                Con.Close();
                Con.Dispose();
            }
            return Exp;
        }
        #endregion
        #region "Update Methods"
        public Exception UpdateList(string UpdateClause)
        {

            return SetList(UpdateClause, false);
        }

        #endregion
    }

}
