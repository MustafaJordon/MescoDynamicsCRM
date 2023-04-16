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
    public partial class CVarvwStructure_Rep_A_SubAccounts_Statement
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mSubAccount_ID;
        internal String mSubAccount_Name;
        internal Int32 mClientNo;
        internal String mClientName;
        internal String mArabicName;
        internal String mClientAddress;
        internal String mPhonesAndFaxes;
        internal String mClientEMail;
        internal String mOperationNo;
        internal String mInvoiceNo;
        internal DateTime mJVDate;
        internal Decimal mDebit;
        internal Decimal mCredit;
        internal Decimal mLocalDebit;
        internal Decimal mLocalCredit;
        internal Int32 mCurrency_ID;
        internal String mCurrency_Code;
        internal String mBalanceText;
        internal Int32 mordernum;
        internal String mJVTypeName;
        internal String mJournalTypeName;
        internal String mVATNumber;
        internal String mJVNo;
        internal String mReceiptNo;
        internal DateTime mDueDate;
        internal String mHouseNumber;
        internal String mPODName;
        internal String mPOLName;
        internal String mChargeableWeightSum;

        #endregion

        #region "Methods"
        public Int32 SubAccount_ID
        {
            get { return mSubAccount_ID; }
            set { mSubAccount_ID = value; }
        }
        public String SubAccount_Name
        {
            get { return mSubAccount_Name; }
            set { mSubAccount_Name = value; }
        }
        public Int32 ClientNo
        {
            get { return mClientNo; }
            set { mClientNo = value; }
        }
        public String ClientName
        {
            get { return mClientName; }
            set { mClientName = value; }
        }
        public String ArabicName
        {
            get { return mArabicName; }
            set { mArabicName = value; }
        }
        public String ClientAddress
        {
            get { return mClientAddress; }
            set { mClientAddress = value; }
        }
        public String PhonesAndFaxes
        {
            get { return mPhonesAndFaxes; }
            set { mPhonesAndFaxes = value; }
        }
        public String ClientEMail
        {
            get { return mClientEMail; }
            set { mClientEMail = value; }
        }
        public String OperationNo
        {
            get { return mOperationNo; }
            set { mOperationNo = value; }
        }
        public String InvoiceNo
        {
            get { return mInvoiceNo; }
            set { mInvoiceNo = value; }
        }
        public DateTime JVDate
        {
            get { return mJVDate; }
            set { mJVDate = value; }
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
        public Decimal LocalDebit
        {
            get { return mLocalDebit; }
            set { mLocalDebit = value; }
        }
        public Decimal LocalCredit
        {
            get { return mLocalCredit; }
            set { mLocalCredit = value; }
        }
        public Int32 Currency_ID
        {
            get { return mCurrency_ID; }
            set { mCurrency_ID = value; }
        }
        public String Currency_Code
        {
            get { return mCurrency_Code; }
            set { mCurrency_Code = value; }
        }
        public String BalanceText
        {
            get { return mBalanceText; }
            set { mBalanceText = value; }
        }
        public Int32 ordernum
        {
            get { return mordernum; }
            set { mordernum = value; }
        }
        public String JVTypeName
        {
            get { return mJVTypeName; }
            set { mJVTypeName = value; }
        }
        public String JournalTypeName
        {
            get { return mJournalTypeName; }
            set { mJournalTypeName = value; }
        }
        public String VATNumber
        {
            get { return mVATNumber; }
            set { mVATNumber = value; }
        }
        public String JVNo
        {
            get { return mJVNo; }
            set { mJVNo = value; }
        }
        public String ReceiptNo
        {
            get { return mReceiptNo; }
            set { mReceiptNo = value; }
        }
    
        public DateTime DueDate
        {
            get { return mDueDate; }
            set { mDueDate = value; }
        }
        public String HouseNumber
        {
            get { return mHouseNumber; }
            set { mHouseNumber = value; }
        }
        public String PODName
        {
            get { return mPODName; }
            set { mPODName = value; }
        }
        public String POLName
        {
            get { return mPOLName; }
            set { mPOLName = value; }
        }
        public String ChargeableWeightSum
        {
            get { return mChargeableWeightSum; }
            set { mChargeableWeightSum = value; }
        }
        #endregion

    }

    public partial class CvwStructure_Rep_A_SubAccounts_Statement
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
        public List<CVarvwStructure_Rep_A_SubAccounts_Statement> lstCVarvwStructure_Rep_A_SubAccounts_Statement = new List<CVarvwStructure_Rep_A_SubAccounts_Statement>();
        #endregion

        #region "Select Methods"
        public Exception GetList(string pSubAccount_IDs, DateTime pFrom_Date, DateTime pTo_Date, bool pEvalCurr,bool pIsCustomer,int pIsSupplier)
        {
            return DataFill(pSubAccount_IDs, pFrom_Date, pTo_Date, pEvalCurr, pIsCustomer,pIsSupplier, true);
        }

        private Exception DataFill(string pSubAccount_IDs,DateTime pFrom_Date,DateTime pTo_Date,bool pEvalCurr,bool pIsCustomer, int pIsSupplier, Boolean IsList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarvwStructure_Rep_A_SubAccounts_Statement.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                Com.CommandTimeout = 600;

                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@SubAccount_IDs", SqlDbType.NVarChar));
                    Com.Parameters.Add(new SqlParameter("@From_Date", SqlDbType.DateTime));
                    Com.Parameters.Add(new SqlParameter("@To_Date", SqlDbType.DateTime));
                    Com.Parameters.Add(new SqlParameter("@EvalCurr", SqlDbType.Bit));
                    Com.Parameters.Add(new SqlParameter("@IsCustomer", SqlDbType.Bit));
                    Com.Parameters.Add(new SqlParameter("@IsSupplier", SqlDbType.Int));

                    Com.CommandText = "[dbo].Rep_A_SubAccounts_Statement";
                    Com.Parameters[0].Value = pSubAccount_IDs;
                    Com.Parameters[1].Value = pFrom_Date;
                    Com.Parameters[2].Value = pTo_Date;
                    Com.Parameters[3].Value = pEvalCurr;
                    Com.Parameters[4].Value = pIsCustomer;
                    Com.Parameters[5].Value = pIsSupplier;
                }
                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        /*Start DataReader*/
                        CVarvwStructure_Rep_A_SubAccounts_Statement ObjCVarvwStructure_Rep_A_SubAccounts_Statement = new CVarvwStructure_Rep_A_SubAccounts_Statement();
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mSubAccount_ID = Convert.ToInt32(dr["SubAccount_ID"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mSubAccount_Name = Convert.ToString(dr["SubAccount_Name"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mClientNo = Convert.ToInt32(dr["ClientNo"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mClientName = Convert.ToString(dr["ClientName"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mArabicName = Convert.ToString(dr["ArabicName"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mClientAddress = Convert.ToString(dr["ClientAddress"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mPhonesAndFaxes = Convert.ToString(dr["PhonesAndFaxes"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mClientEMail = Convert.ToString(dr["ClientEMail"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mOperationNo = Convert.ToString(dr["OperationNo"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mInvoiceNo = Convert.ToString(dr["InvoiceNo"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mJVDate = Convert.ToDateTime(dr["JVDate"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mDebit = Convert.ToDecimal(dr["Debit"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mCredit = Convert.ToDecimal(dr["Credit"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mLocalDebit = Convert.ToDecimal(dr["LocalDebit"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mLocalCredit = Convert.ToDecimal(dr["LocalCredit"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mCurrency_ID = Convert.ToInt32(dr["Currency_ID"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mCurrency_Code = Convert.ToString(dr["Currency_Code"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mBalanceText = Convert.ToString(dr["BalanceText"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mordernum = Convert.ToInt32(dr["ordernum"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mJVTypeName = Convert.ToString(dr["JVTypeName"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mJournalTypeName = Convert.ToString(dr["JournalTypeName"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mVATNumber = Convert.ToString(dr["VATNumber"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mJVNo = Convert.ToString(dr["JVNo"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mReceiptNo = Convert.ToString(dr["ReceiptNo"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mDueDate = Convert.ToDateTime(dr["DueDate"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mHouseNumber = Convert.ToString(dr["HouseNumber"].ToString());

                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mPODName = Convert.ToString(dr["PODName"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mPOLName = Convert.ToString(dr["POLName"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_Statement.mChargeableWeightSum = Convert.ToString(dr["ChargeableWeightSum"].ToString());

                        lstCVarvwStructure_Rep_A_SubAccounts_Statement.Add(ObjCVarvwStructure_Rep_A_SubAccounts_Statement);
               
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
