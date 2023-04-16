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
    public partial class CVarvwStructure_Rep_A_SubAccounts_LedgerSumCurrency
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Decimal mFinalBalance;
        internal Int32 mSubAccount_ID;
        internal String mCurrency_Code;
        #endregion

        #region "Methods"
        public Decimal FinalBalance
        {
            get { return mFinalBalance; }
            set { mFinalBalance = value; }
        }
        public Int32 SubAccount_ID
        {
            get { return mSubAccount_ID; }
            set { mSubAccount_ID = value; }
        }
        public String Currency_Code
        {
            get { return mCurrency_Code; }
            set { mCurrency_Code = value; }
        }
        #endregion
    }

    public partial class CvwStructure_Rep_A_SubAccounts_LedgerSumCurrency
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
        public List<CVarvwStructure_Rep_A_SubAccounts_LedgerSumCurrency> lstCVarvwStructure_Rep_A_SubAccounts_LedgerSumCurrency = new List<CVarvwStructure_Rep_A_SubAccounts_LedgerSumCurrency>();
        #endregion

        #region "Select Methods"
        public Exception GetList(string pSubAccount_IDs, string pAccountIDs, DateTime pTo_Date, string pBranchIDs)
        {
            return DataFill(pSubAccount_IDs, pAccountIDs , pTo_Date , pBranchIDs, true);
        }

        private Exception DataFill(string pSubAccount_IDs, string pAccountIDs, DateTime pTo_Date, string pBranchIDs, Boolean IsList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarvwStructure_Rep_A_SubAccounts_LedgerSumCurrency.Clear();

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
                    Com.Parameters.Add(new SqlParameter("@To_Date", SqlDbType.DateTime));
                    Com.Parameters.Add(new SqlParameter("@BranchIDs", SqlDbType.NVarChar));

                    Com.CommandText = "[dbo].[Rep_A_SubAccounts_LedgerSumCurrency]";
                    Com.Parameters[0].Value = pSubAccount_IDs;
                    Com.Parameters[1].Value = pAccountIDs;
                    Com.Parameters[2].Value = pTo_Date;
                    Com.Parameters[3].Value = pBranchIDs;
                }
                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        /*Start DataReader*/
                        CVarvwStructure_Rep_A_SubAccounts_LedgerSumCurrency ObjCVarvwStructure_Rep_A_SubAccounts_LedgerSumCurrency = new CVarvwStructure_Rep_A_SubAccounts_LedgerSumCurrency();
                        ObjCVarvwStructure_Rep_A_SubAccounts_LedgerSumCurrency.mFinalBalance = Convert.ToDecimal(dr["FinalBalance"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_LedgerSumCurrency.mSubAccount_ID = Convert.ToInt32(dr["SubAccount_ID"].ToString());
                        ObjCVarvwStructure_Rep_A_SubAccounts_LedgerSumCurrency.mCurrency_Code = Convert.ToString(dr["Currency_Code"].ToString());
                        lstCVarvwStructure_Rep_A_SubAccounts_LedgerSumCurrency.Add(ObjCVarvwStructure_Rep_A_SubAccounts_LedgerSumCurrency);
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
