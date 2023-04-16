﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.Sales.Transactions.Generated.Payments.Generated
{
    [Serializable]
    public class CPKvwSL_Payments
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
    public partial class CVarvwSL_Payments : CPKvwSL_Payments
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mCode;
        internal DateTime mPaymentDate;
        internal Int64 mClientID;
        internal String mClientName;
        internal String mName;
        internal String mNotes;
        internal Int32 mUserID;
        internal String mUsername;
        internal Boolean mIsDeleted;
        internal Decimal mTotalPayment;
        internal String mCurrencyCode;
        internal Int32 mCurrencyID;
        internal Decimal mRefundAmount;
        internal Int32 mRefundCurrencyID;
        internal Decimal mBankChargesAmount;
        internal Int32 mBankChargesCurrencyID;
        internal Int32 mSafeID;
        internal String mSafeName;
        internal Int32 mSafeDefaultCurrencyID;
        internal Int32 mBankID;
        internal String mBankName;
        internal Int32 mBankDefaultCurrencyID;
        internal Int32 mAccountID;
        internal Int32 mSubAccountID;
        internal String mCashCode;
        internal String mchequeCode;

        #endregion

        #region "Methods"
        public String Code
        {
            get { return mCode; }
            set { mCode = value; }
        }
        public DateTime PaymentDate
        {
            get { return mPaymentDate; }
            set { mPaymentDate = value; }
        }
        public Int64 ClientID
        {
            get { return mClientID; }
            set { mClientID = value; }
        }
        public String Name
        {
            get { return mName; }
            set { mName = value; }
        }
        public String ClientName
        {
            get { return mClientName; }
            set { mClientName = value; }
        }
        public String CurrencyCode
        {
            get { return mCurrencyCode; }
            set { mCurrencyCode = value; }
        }
        public Int32 SafeID
        {
            get { return mSafeID; }
            set { mSafeID = value; }
        }
        public String SafeName
        {
            get { return mSafeName; }
            set { mSafeName = value; }
        }
        public Int32 SafeDefaultCurrencyID
        {
            get { return mSafeDefaultCurrencyID; }
            set { mSafeDefaultCurrencyID = value; }
        }

        public Int32 BankID
        {
            get { return mBankID; }
            set { mBankID = value; }
        }
        public String BankName
        {
            get { return mBankName; }
            set { mBankName = value; }
        }
        public Int32 BankDefaultCurrencyID
        {
            get { return mBankDefaultCurrencyID; }
            set { mBankDefaultCurrencyID = value; }
        }


        public Int32 AccountID
        {
            get { return mAccountID; }
            set { mAccountID = value; }
        }

        public Int32 SubAccountID
        {
            get { return mSubAccountID; }
            set { mSubAccountID = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mNotes = value; }
        }
        public Int32 UserID
        {
            get { return mUserID; }
            set { mUserID = value; }
        }
        public String Username
        {
            get { return mUsername; }
            set { mUsername = value; }
        }
        public Boolean IsDeleted
        {
            get { return mIsDeleted; }
            set { mIsDeleted = value; }
        }
        public Decimal TotalPayment
        {
            get { return mTotalPayment; }
            set { mTotalPayment = value; }
        }
        public Int32 CurrencyID
        {
            get { return mCurrencyID; }
            set { mCurrencyID = value; }
        }
        public Decimal RefundAmount
        {
            get { return mRefundAmount; }
            set { mRefundAmount = value; }
        }
        public Int32 RefundCurrencyID
        {
            get { return mRefundCurrencyID; }
            set { mRefundCurrencyID = value; }
        }
        public Decimal BankChargesAmount
        {
            get { return mBankChargesAmount; }
            set { mBankChargesAmount = value; }
        }
        public Int32 BankChargesCurrencyID
        {
            get { return mBankChargesCurrencyID; }
            set { mBankChargesCurrencyID = value; }
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
        public String CashCode
        {
            get { return mCashCode; }
            set { mCashCode = value; }
        }
        public String chequeCode
        {
            get { return mchequeCode; }
            set { mchequeCode = value; }
        }

        #endregion
    }

    public partial class CvwSL_Payments
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
        public List<CVarvwSL_Payments> lstCVarvwSL_Payments = new List<CVarvwSL_Payments>();
        #endregion

        #region "Select Methods"
        public Exception GetListBanksByName(string WhereClause)
        {
            return DataFillBanks(WhereClause, true);
        }
        public Exception GetListUsersByName(string WhereClause)
        {
            return DataFillUsers(WhereClause, true);
        }
        public Exception GetListSafesByName(string WhereClause)
        {
            return DataFillSafes(WhereClause, true);
        }

        public Exception GetListAccountIDAndSubAccountID(Int64 WhereClause)
        {
            return GetAccountIDAndSubAccountID(WhereClause, true);
        }
        public Exception GetListRefundAccountIDAndSubAccountID(Int64 WhereClause)
        {
            return GetRefundAccountIDAndSubAccountID(WhereClause, true);
        }

        public Exception GetListCurrencyByName(string WhereClause)
        {
            return DataFillCurrency(WhereClause, true);
        }


        public Exception GetListClientsByName(string WhereClause)
        {
            return DataFillClients(WhereClause, true);
        }

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
            lstCVarvwSL_Payments.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwPayments";
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
                        CVarvwSL_Payments ObjCVarvwSL_Payments = new CVarvwSL_Payments();
                        ObjCVarvwSL_Payments.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwSL_Payments.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwSL_Payments.mPaymentDate = Convert.ToDateTime(dr["PaymentDate"].ToString());
                        ObjCVarvwSL_Payments.mClientID = Convert.ToInt64(dr["ClientID"].ToString());
                        ObjCVarvwSL_Payments.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwSL_Payments.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwSL_Payments.mUserID = Convert.ToInt32(dr["UserID"].ToString());
                        ObjCVarvwSL_Payments.mUsername = Convert.ToString(dr["Username"].ToString());
                        ObjCVarvwSL_Payments.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarvwSL_Payments.mTotalPayment = Convert.ToDecimal(dr["TotalPayment"].ToString());
                        ObjCVarvwSL_Payments.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwSL_Payments.mRefundAmount = Convert.ToDecimal(dr["RefundAmount"].ToString());
                        ObjCVarvwSL_Payments.mRefundCurrencyID = Convert.ToInt32(dr["RefundCurrencyID"].ToString());
                        ObjCVarvwSL_Payments.mBankChargesAmount = Convert.ToDecimal(dr["BankChargesAmount"].ToString());
                        ObjCVarvwSL_Payments.mBankChargesCurrencyID = Convert.ToInt32(dr["BankChargesCurrencyID"].ToString());
                        lstCVarvwSL_Payments.Add(ObjCVarvwSL_Payments);
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
            lstCVarvwSL_Payments.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwPayments";
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
                        CVarvwSL_Payments ObjCVarvwSL_Payments = new CVarvwSL_Payments();
                        ObjCVarvwSL_Payments.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwSL_Payments.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwSL_Payments.mPaymentDate = Convert.ToDateTime(dr["PaymentDate"].ToString());
                        ObjCVarvwSL_Payments.mClientID = Convert.ToInt64(dr["ClientID"].ToString());
                        ObjCVarvwSL_Payments.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwSL_Payments.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwSL_Payments.mUserID = Convert.ToInt32(dr["UserID"].ToString());
                        ObjCVarvwSL_Payments.mUsername = Convert.ToString(dr["Username"].ToString());
                        ObjCVarvwSL_Payments.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarvwSL_Payments.mTotalPayment = Convert.ToDecimal(dr["TotalPayment"].ToString());
                        ObjCVarvwSL_Payments.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwSL_Payments.mRefundAmount = Convert.ToDecimal(dr["RefundAmount"].ToString());
                        ObjCVarvwSL_Payments.mRefundCurrencyID = Convert.ToInt32(dr["RefundCurrencyID"].ToString());
                        ObjCVarvwSL_Payments.mBankChargesAmount = Convert.ToDecimal(dr["BankChargesAmount"].ToString());
                        ObjCVarvwSL_Payments.mBankChargesCurrencyID = Convert.ToInt32(dr["BankChargesCurrencyID"].ToString());
                        ObjCVarvwSL_Payments.mCashCode = Convert.ToString(dr["CashCode"].ToString());
                        ObjCVarvwSL_Payments.mchequeCode = Convert.ToString(dr["chequeCode"].ToString());

                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwSL_Payments.Add(ObjCVarvwSL_Payments);

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
        private Exception DataFillSafes(string Param, Boolean IsList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarvwSL_Payments.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListTreasury";
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
                        CVarvwSL_Payments ObjCVarHR_VacationsBalancesDetails = new CVarvwSL_Payments();
                        ObjCVarHR_VacationsBalancesDetails.mSafeID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarHR_VacationsBalancesDetails.mSafeName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarHR_VacationsBalancesDetails.mSafeDefaultCurrencyID = Convert.ToInt32(dr["DefaultCurrencyID"].ToString());
                        lstCVarvwSL_Payments.Add(ObjCVarHR_VacationsBalancesDetails);
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

        private Exception DataFillBanks(string Param, Boolean IsList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarvwSL_Payments.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListBankAccount";
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

                        CVarvwSL_Payments ObjCVarHR_VacationsBalancesDetails = new CVarvwSL_Payments();
                        ObjCVarHR_VacationsBalancesDetails.mBankID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarHR_VacationsBalancesDetails.mBankName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarHR_VacationsBalancesDetails.mBankDefaultCurrencyID = Convert.ToInt32(dr["DefaultCurrencyID"].ToString());

                        lstCVarvwSL_Payments.Add(ObjCVarHR_VacationsBalancesDetails);
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

        private Exception GetAccountIDAndSubAccountID(Int64 Param, Boolean IsList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarvwSL_Payments.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.BigInt));
                    Com.CommandText = "[dbo].GetListAccountSL_PS_Payment_IDSubAccountID";
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
                        CVarvwSL_Payments ObjCVarHR_VacationsBalancesDetails = new CVarvwSL_Payments();
                        ObjCVarHR_VacationsBalancesDetails.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarHR_VacationsBalancesDetails.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        lstCVarvwSL_Payments.Add(ObjCVarHR_VacationsBalancesDetails);
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
        private Exception GetRefundAccountIDAndSubAccountID(Int64 Param, Boolean IsList)
        {
            Exception Exp = null;
            SqlDataReader dr;
            //SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;// = GlobalConnection.GlobalCom;
            lstCVarvwSL_Payments.Clear();
            try
            {
                //Con.Open();
                //Com = new SqlCommand();
                //GlobalConnection.GlobalCon.Open();
                GlobalConnection.myCommand = new SqlCommand();
                GlobalConnection.myCommand.Connection = GlobalConnection.myConnection;
                GlobalConnection.myCommand.Transaction = GlobalConnection.myTrans;

                GlobalConnection.myCommand.CommandType = CommandType.StoredProcedure;
                GlobalConnection.myCommand.CommandTimeout = 0;
                Com = GlobalConnection.myCommand;


                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.BigInt));
                    Com.CommandText = "[dbo].GetListAccount_IDSubAccountID_Refund";
                    Com.Parameters[0].Value = Param;
                }
                //Com.Transaction = tr;
                //Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        /*Start DataReader*/
                        CVarvwSL_Payments ObjCVarHR_VacationsBalancesDetails = new CVarvwSL_Payments();
                        ObjCVarHR_VacationsBalancesDetails.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarHR_VacationsBalancesDetails.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        lstCVarvwSL_Payments.Add(ObjCVarHR_VacationsBalancesDetails);
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
                //tr.Commit();
            }
            catch (Exception ex)
            {
                Exp = ex;
            }
            finally
            {
                //Con.Close();
                //Con.Dispose();
            }
            return Exp;
        }
        private Exception DataFillCurrency(string Param, Boolean IsList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarvwSL_Payments.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListCurrency";
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
                        CVarvwSL_Payments ObjCVarHR_VacationsBalancesDetails = new CVarvwSL_Payments();
                        ObjCVarHR_VacationsBalancesDetails.mCurrencyID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarHR_VacationsBalancesDetails.mCurrencyCode = Convert.ToString(dr["Code"].ToString());
                        lstCVarvwSL_Payments.Add(ObjCVarHR_VacationsBalancesDetails);
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
        private Exception DataFillUsers(string Param, Boolean IsList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarvwSL_Payments.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListUsers";
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
                        CVarvwSL_Payments ObjCVarHR_VacationsBalancesDetails = new CVarvwSL_Payments();
                        ObjCVarHR_VacationsBalancesDetails.mUserID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarHR_VacationsBalancesDetails.mUsername = Convert.ToString(dr["Username"].ToString());
                        lstCVarvwSL_Payments.Add(ObjCVarHR_VacationsBalancesDetails);
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

        private Exception DataFillClients(string Param, Boolean IsList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarvwSL_Payments.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].[GetListCustomers]";
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
                        CVarvwSL_Payments ObjCVarHR_VacationsBalancesDetails = new CVarvwSL_Payments();
                        //ObjCVarHR_VacationsBalancesDetails.mBillID = Convert.ToInt32(dr["BillID"].ToString());
                        ObjCVarHR_VacationsBalancesDetails.mClientID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarHR_VacationsBalancesDetails.mClientName = Convert.ToString(dr["Name"].ToString());

                        lstCVarvwSL_Payments.Add(ObjCVarHR_VacationsBalancesDetails);
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









