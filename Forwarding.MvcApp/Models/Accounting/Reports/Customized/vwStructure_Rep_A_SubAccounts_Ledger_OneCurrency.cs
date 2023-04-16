﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.Accounting.Reports.Customized
{
    [Serializable]
    public partial class CVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mSubAccount_ID;
        internal String mSubAccountName;
        internal String mSubAccount_Number;
        internal String mAccountName;
        internal String mJVNo;
        internal DateTime mJVDate;
        internal String mReceiptNo;
        internal Decimal mDebit;
        internal Decimal mCredit;
        internal Decimal mExchangeRate;
        internal String mCurrency_Code;
        internal String mdescription;
        internal String mJVType_Name;
        internal String mJournal_Name;
        internal Decimal mOpining_Balance;
        #endregion

        #region "Methods"
        public Int32 SubAccount_ID
        {
            get { return mSubAccount_ID; }
            set { mSubAccount_ID = value; }
        }
        public String SubAccountName
        {
            get { return mSubAccountName; }
            set { mSubAccountName = value; }
        }
        public String SubAccount_Number
        {
            get { return mSubAccount_Number; }
            set { mSubAccount_Number = value; }
        }
        public String AccountName
        {
            get { return mAccountName; }
            set { mAccountName = value; }
        }
        public String JVNo
        {
            get { return mJVNo; }
            set { mJVNo = value; }
        }
        public DateTime JVDate
        {
            get { return mJVDate; }
            set { mJVDate = value; }
        }
        public String ReceiptNo
        {
            get { return mReceiptNo; }
            set { mReceiptNo = value; }
        }
        public Decimal Debit
        {
            get { return mDebit; }
            set { mDebit = value; }
        }
        public Decimal Credit
        {
            get { return mCredit; }
            set { mCredit = value; }
        }
        public Decimal ExchangeRate
        {
            get { return mExchangeRate; }
            set { mExchangeRate = value; }
        }
        public String Currency_Code
        {
            get { return mCurrency_Code; }
            set { mCurrency_Code = value; }
        }
        public String description
        {
            get { return mdescription; }
            set { mdescription = value; }
        }
        public String JVType_Name
        {
            get { return mJVType_Name; }
            set { mJVType_Name = value; }
        }
        public String Journal_Name
        {
            get { return mJournal_Name; }
            set { mJournal_Name = value; }
        }
        public Decimal Opening_Balance
        {
            get { return mOpining_Balance; }
            set { mOpining_Balance = value; }
        }
        #endregion
    }

    public partial class CvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency
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
        public List<CVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency> lstCVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency = new List<CVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency>();
        #endregion

        #region "Select Methods"
        public Exception GetList(string pSubAccount_IDs, string pAccountIDs, DateTime pFrom_Date, DateTime pTo_Date, int pCurrency,string pBranchIDs)
        {
            return DataFill(pSubAccount_IDs, pAccountIDs, pFrom_Date, pTo_Date, pCurrency, pBranchIDs, true);
        }
        private Exception DataFill(string pSubAccount_IDs, string pAccountIDs, DateTime pFrom_Date, DateTime pTo_Date, int pCurrency, string pBranchIDs, Boolean IsList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@SubAccount_IDs", SqlDbType.NVarChar));
                    Com.Parameters.Add(new SqlParameter("@AccountIDs", SqlDbType.NVarChar));
                    Com.Parameters.Add(new SqlParameter("@From_Date", SqlDbType.DateTime));
                    Com.Parameters.Add(new SqlParameter("@To_Date", SqlDbType.DateTime));
                    Com.Parameters.Add(new SqlParameter("@Currency", SqlDbType.Int));
                    Com.Parameters.Add(new SqlParameter("@BranchIDs", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].Rep_A_SubAccounts_Ledger_OneCurrency";
                    Com.Parameters[0].Value = pSubAccount_IDs;
                    Com.Parameters[1].Value = pAccountIDs;
                    Com.Parameters[2].Value = pFrom_Date;
                    Com.Parameters[3].Value = pTo_Date;
                    Com.Parameters[4].Value = pCurrency;
                    Com.Parameters[5].Value = pBranchIDs;
                }
                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        /*Start DataReader*/
                        CVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency ObjCVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency = new CVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency();
                        ObjCVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency.mSubAccount_ID = Convert.ToInt32(dr["SubAccount_ID"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency.mSubAccountName = Convert.ToString(dr["SubAccountName"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency.mSubAccount_Number = Convert.ToString(dr["SubAccount_Number"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency.mAccountName = Convert.ToString(dr["AccountName"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency.mJVNo = Convert.ToString(dr["JVNo"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency.mJVDate = Convert.ToDateTime(dr["JVDate"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency.mReceiptNo = Convert.ToString(dr["ReceiptNo"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency.mDebit = Convert.ToDecimal(dr["Debit"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency.mCredit = Convert.ToDecimal(dr["Credit"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency.mExchangeRate = Convert.ToDecimal(dr["ExchangeRate"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency.mCurrency_Code = Convert.ToString(dr["Currency_Code"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency.mdescription = Convert.ToString(dr["description"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency.mJVType_Name = Convert.ToString(dr["JVType_Name"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency.mJournal_Name = Convert.ToString(dr["Journal_Name"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency.mOpining_Balance = Convert.ToDecimal(dr["Opening_Balance"].ToString());
                        lstCVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency.Add(ObjCVarvwStructure_Rep_A_SubAccounts_Ledger_OneCurrency);
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
