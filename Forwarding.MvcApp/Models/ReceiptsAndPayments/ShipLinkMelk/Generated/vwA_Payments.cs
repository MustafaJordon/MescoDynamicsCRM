﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using Forwarding.MvcApp.Models.Customized;
using Forwarding.MvcApp.Models.ReceiptsAndPayments.ShipLinkMelk.Customized;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.ReceiptsAndPayments.ShipLinkMelk.Generated
{
    [Serializable]
    public partial class CVarvwA_Payments
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int64 mID;
        internal String mCode;
        internal DateTime mPaymentDate;
        internal Int64 mClientID;
        internal String mName;
        internal String mNotes;
        internal Int32 mUserID;
        internal String mUsername; 
        internal String mTotalCost;
        internal String mVoucherCashCode;
        internal String mVoucherChequeCode;
        internal Int32 mBillID;
        internal String mCurrencyCode;
        internal Int32 mCurrencyID;
        internal String mBillNumber;
        internal String mClientName;
        internal Boolean mIsDeleted;
        internal Int32 mSafeID;
        internal String mSafeName;
        internal Int32 mSafeDefaultCurrencyID;
        internal Int32 mBankID;
        internal String mBankName;
        internal Int32 mBankDefaultCurrencyID;
        internal Int32 mAccountID;
        internal Int32 mSubAccountID;
        internal Decimal mAmount;
        internal String mCurrencyName;
        internal Decimal mRefundAmount;
        internal Int32 mRefundCurrencyID;
        internal Decimal mBankChargesAmount;
        internal Int32 mBankChargesCurrencyID; 
        internal String mRefundCurrencyName;
        internal String mBankChargesCurrencyName;

        #endregion

        #region "Methods"
        public Int64 ID
        {
            get { return mID; }
            set { mID = value; }
        }
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
        
        public String TotalCost
        {
            get { return mTotalCost; }
            set { mTotalCost = value; }
        }
        public String VoucherCashCode
        {
            get { return mVoucherCashCode; }
            set { mVoucherCashCode = value; }
        }
        public String VoucherChequeCode
        {
            get { return mVoucherChequeCode; }
            set { mVoucherChequeCode = value; }
        }
        public Int32 CurrencyID
        {
            get { return mCurrencyID; }
            set { mCurrencyID = value; }
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
        public Int32 BillID
        {
            get { return mBillID; }
            set { mBillID = value; }
        }
        public String ClientName
        {
            get { return mClientName; }
            set { mClientName = value; }
        }
        public String BillNumber
        {
            get { return mBillNumber; }
            set { mBillNumber = value; }
        }
        
        public Boolean IsDeleted
        {
            get { return mIsDeleted; }
            set { mIsDeleted = value; }
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
        
        public Decimal Amount
        {
            get { return mAmount; }
            set { mAmount = value; }
        }
        public String CurrencyName
        {
            get { return mCurrencyName; }
            set { mCurrencyName = value; }
        }
        public Int32 RefundCurrencyID
        {
            get { return mRefundCurrencyID; }
            set { mRefundCurrencyID = value; }
        }

        public Decimal RefundAmount
        {
            get { return mRefundAmount; }
            set { mRefundAmount = value; }
        }
        public Int32 BankChargesCurrencyID
        {
            get { return mBankChargesCurrencyID; }
            set { mBankChargesCurrencyID = value; }
        }

        public Decimal BankChargesAmount
        {
            get { return mBankChargesAmount; }
            set { mBankChargesAmount = value; }
        }
        
        public String RefundCurrencyName
        {
            get { return mRefundCurrencyName; }
            set { mRefundCurrencyName = value; }
        }
        public String BankChargesCurrencyName
        {
            get { return mBankChargesCurrencyName; }
            set { mBankChargesCurrencyName = value; }
        }
        #endregion
    }

    public partial class CvwA_Payments
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
        public List<CVarvwA_Payments> lstCVarvwA_Payments = new List<CVarvwA_Payments>();
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
        public Exception GetListClientsByNameWithInvoices(string WhereClause)
        {
            return DataFillClientsWithInvoices(WhereClause, true);
        }
        private Exception DataFillClientsWithInvoices(string Param, Boolean IsList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarvwA_Payments.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListClientWithInvoiceSearch";
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
                        CVarvwA_Payments ObjCVarHR_VacationsBalancesDetails = new CVarvwA_Payments();
                        //ObjCVarHR_VacationsBalancesDetails.mBillID = Convert.ToInt32(dr["BillID"].ToString());
                        ObjCVarHR_VacationsBalancesDetails.mClientID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarHR_VacationsBalancesDetails.mClientName = Convert.ToString(dr["Name"].ToString());

                        lstCVarvwA_Payments.Add(ObjCVarHR_VacationsBalancesDetails);
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

        public Exception GetList(string WhereClause)
        {
            return DataFill(WhereClause, true);
        }
        public Exception GetListPaging(Int32 PageSize, Int32 PageNumber, string WhereClause, string OrderBy, out Int32 TotalRecords)
        {
            return DataFill(PageSize, PageNumber, WhereClause, OrderBy, out TotalRecords);
        }
         private Exception DataFillSafes(string Param, Boolean IsList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarvwA_Payments.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListSafes";
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
                        CVarvwA_Payments ObjCVarHR_VacationsBalancesDetails = new CVarvwA_Payments();
                        ObjCVarHR_VacationsBalancesDetails.mSafeID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarHR_VacationsBalancesDetails.mSafeName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarHR_VacationsBalancesDetails.mSafeDefaultCurrencyID = Convert.ToInt32(dr["DefaultCurrencyID"].ToString());
                        lstCVarvwA_Payments.Add(ObjCVarHR_VacationsBalancesDetails);
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
            lstCVarvwA_Payments.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListBank";
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

                        CVarvwA_Payments ObjCVarHR_VacationsBalancesDetails = new CVarvwA_Payments();
                        ObjCVarHR_VacationsBalancesDetails.mBankID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarHR_VacationsBalancesDetails.mBankName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarHR_VacationsBalancesDetails.mBankDefaultCurrencyID = Convert.ToInt32(dr["DefaultCurrencyID"].ToString());

                        lstCVarvwA_Payments.Add(ObjCVarHR_VacationsBalancesDetails);
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
            lstCVarvwA_Payments.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.BigInt));
                    Com.CommandText = "[dbo].GetListAccount_IDSubAccountID";
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
                        CVarvwA_Payments ObjCVarHR_VacationsBalancesDetails = new CVarvwA_Payments();
                        ObjCVarHR_VacationsBalancesDetails.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarHR_VacationsBalancesDetails.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        lstCVarvwA_Payments.Add(ObjCVarHR_VacationsBalancesDetails);
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
            lstCVarvwA_Payments.Clear();
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
                        CVarvwA_Payments ObjCVarHR_VacationsBalancesDetails = new CVarvwA_Payments();
                        ObjCVarHR_VacationsBalancesDetails.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarHR_VacationsBalancesDetails.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        lstCVarvwA_Payments.Add(ObjCVarHR_VacationsBalancesDetails);
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
            lstCVarvwA_Payments.Clear();

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
                        CVarvwA_Payments ObjCVarHR_VacationsBalancesDetails = new CVarvwA_Payments();
                        ObjCVarHR_VacationsBalancesDetails.mCurrencyID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarHR_VacationsBalancesDetails.mCurrencyCode = Convert.ToString(dr["Code"].ToString());
                        lstCVarvwA_Payments.Add(ObjCVarHR_VacationsBalancesDetails);
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
            lstCVarvwA_Payments.Clear();

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
                        CVarvwA_Payments ObjCVarHR_VacationsBalancesDetails = new CVarvwA_Payments();
                        ObjCVarHR_VacationsBalancesDetails.mUserID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarHR_VacationsBalancesDetails.mUsername = Convert.ToString(dr["Username"].ToString());
                        lstCVarvwA_Payments.Add(ObjCVarHR_VacationsBalancesDetails);
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
            lstCVarvwA_Payments.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListClient";
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
                        CVarvwA_Payments ObjCVarHR_VacationsBalancesDetails = new CVarvwA_Payments();
                        //ObjCVarHR_VacationsBalancesDetails.mBillID = Convert.ToInt32(dr["BillID"].ToString());
                        ObjCVarHR_VacationsBalancesDetails.mClientID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarHR_VacationsBalancesDetails.mClientName = Convert.ToString(dr["Name"].ToString());

                        lstCVarvwA_Payments.Add(ObjCVarHR_VacationsBalancesDetails);
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
        private Exception DataFill(string Param, Boolean IsList)
        {
            Exception Exp = null;
            CCustomizedDBCall objCCustomizedDBCall = new CCustomizedDBCall();
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarvwA_Payments.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwA_Payments";
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
                        CVarvwA_Payments ObjCVarvwA_Payments = new CVarvwA_Payments();
                        ObjCVarvwA_Payments.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwA_Payments.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwA_Payments.mPaymentDate = Convert.ToDateTime(dr["PaymentDate"].ToString()).AddDays(1);
                        ObjCVarvwA_Payments.mClientID = Convert.ToInt64(dr["ClientID"].ToString());
                        ObjCVarvwA_Payments.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwA_Payments.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwA_Payments.mUserID = Convert.ToInt32(dr["UserID"].ToString());
                        ObjCVarvwA_Payments.mUsername = Convert.ToString(dr["Username"].ToString());
                        ObjCVarvwA_Payments.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarvwA_Payments.mTotalCost = Convert.ToString(dr["TotalCost"].ToString());
                        ObjCVarvwA_Payments.mAmount = Convert.ToDecimal(dr["Amount"].ToString());
                        ObjCVarvwA_Payments.mCurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());
                        ObjCVarvwA_Payments.mRefundAmount = Convert.ToDecimal(dr["RefundAmount"].ToString());
                        ObjCVarvwA_Payments.mRefundCurrencyID = Convert.ToInt32(dr["RefundCurrencyID"].ToString());
                        ObjCVarvwA_Payments.mBankChargesAmount = Convert.ToDecimal(dr["BankChargesAmount"].ToString());
                        ObjCVarvwA_Payments.mBankChargesCurrencyID = Convert.ToInt32(dr["BankChargesCurrencyID"].ToString());


                        ObjCVarvwA_Payments.mCurrencyName = objCCustomizedDBCall.CallStringFunction("select Name from Currency where ID = " + ObjCVarvwA_Payments.mCurrencyID);
                        ObjCVarvwA_Payments.mRefundCurrencyName = objCCustomizedDBCall.CallStringFunction("select Name from Currency where ID = " + ObjCVarvwA_Payments.mRefundCurrencyID);
                        ObjCVarvwA_Payments.mBankChargesCurrencyName = objCCustomizedDBCall.CallStringFunction("select Name from Currency where ID = " + ObjCVarvwA_Payments.mBankChargesCurrencyID);

                        lstCVarvwA_Payments.Add(ObjCVarvwA_Payments);
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
            lstCVarvwA_Payments.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwA_Payments";
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
                        CVarvwA_Payments ObjCVarvwA_Payments = new CVarvwA_Payments();
                        ObjCVarvwA_Payments.mID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwA_Payments.mCode = Convert.ToString(dr["Code"].ToString());
                       // ObjCVarvwA_Payments.mPaymentDate = Convert.ToDateTime(dr["PaymentDate"].ToString()).AddDays(1);
                        ObjCVarvwA_Payments.mPaymentDate = Convert.ToDateTime(dr["PaymentDate"].ToString());

                        ObjCVarvwA_Payments.mClientID = Convert.ToInt64(dr["ClientID"].ToString());
                        ObjCVarvwA_Payments.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwA_Payments.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwA_Payments.mUserID = Convert.ToInt32(dr["UserID"].ToString());
                        ObjCVarvwA_Payments.mUsername = Convert.ToString(dr["Username"].ToString());
                        ObjCVarvwA_Payments.mIsDeleted = Convert.ToBoolean(dr["IsDeleted"].ToString());
                        ObjCVarvwA_Payments.mTotalCost = Convert.ToString(dr["TotalCost"].ToString());
                        ObjCVarvwA_Payments.mVoucherCashCode = Convert.ToString(dr["VoucherCashCode"].ToString());
                        ObjCVarvwA_Payments.mVoucherChequeCode = Convert.ToString(dr["VoucherChequeCode"].ToString());


                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwA_Payments.Add(ObjCVarvwA_Payments);
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


 







