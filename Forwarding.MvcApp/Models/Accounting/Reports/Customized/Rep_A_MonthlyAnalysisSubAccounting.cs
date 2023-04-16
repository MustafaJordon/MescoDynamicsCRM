﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
// the Model for connect with [dbo].[Rep_A_MonthlyAnalysisSubAccounting] procedure 
// [dbo].[Rep_A_MonthlyAnalysisSubAccounting] : Merge duplicate items base on Itemtype"customers , subaccounts , ....... etc" 
namespace Forwarding.MvcApp.Models.Accounting.Reports.Customized
{
    [Serializable]
    public class Rep_A_MonthlyAnalysisSubAccounting
    {
        #region "variables"
        #endregion

        #region "Methods"
        #endregion
    }
    [Serializable]
    public partial class CVarRep_A_MonthlyAnalysisSubAccounting : Rep_A_MonthlyAnalysisSubAccounting
    {


        //jv.JVDate , acc.Account_Name , ( jvd.Debit - jvd.Credit   ) AS Balance

        #region "variables"
        internal string mJVDate;
        internal string mSubAccount_Name;
        internal Decimal mBalance;

        #endregion

        #region "Methods"
        public string JVDate
        {
            get { return mJVDate; }
            set { mJVDate = value; }
        }

        public string SubAccount_Name
        {
            get { return mSubAccount_Name; }
            set { mSubAccount_Name = value; }
        }

        public decimal Balance
        {
            get { return mBalance; }
            set { mBalance = value; }
        }
        #endregion

    }

    public partial class CRep_A_MonthlyAnalysisSubAccounting
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
        //(@AccountIDs nvarchar(max) ,  @FromDate datetime, @ToDate datetime)
        private SqlTransaction tr;
        public List<CVarRep_A_MonthlyAnalysisSubAccounting> lstCVarRep_A_MonthlyAnalysisSubAccounting = new List<CVarRep_A_MonthlyAnalysisSubAccounting>();
        #endregion
        // Merge(string pItemType, string pDuplicateItems, string pDestinationItem)
        #region "Select Methods"
        public Exception GetList(string SubAccountIDs, string AccountIDs , DateTime FromDate , DateTime ToDate )
        {
            return DataFill(SubAccountIDs,AccountIDs, FromDate, ToDate,  true);
        }

        private Exception DataFill(string SubAccountIDs ,string AccountIDs, DateTime FromDate, DateTime ToDate ,Boolean IsList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarRep_A_MonthlyAnalysisSubAccounting.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    //@FromDate DATETIME, @ToDate DATETIME , @WhereClause NVarChar(MAX)
                    Com.Parameters.Add(new SqlParameter("@SubAccountIDs", SqlDbType.NVarChar));
                    Com.Parameters.Add(new SqlParameter("@AccountIDs", SqlDbType.NVarChar));
                    Com.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime));
                    Com.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime));
                    Com.CommandText = "[dbo].[Rep_A_MonthlyAnalsisSubAccounting]";
                    Com.Parameters[0].Value = SubAccountIDs;
                    Com.Parameters[1].Value = AccountIDs;
                    Com.Parameters[2].Value = FromDate;
                    Com.Parameters[3].Value = ToDate;
                }
                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        /*Start DataReader*/
                        CVarRep_A_MonthlyAnalysisSubAccounting ObjCVarRep_A_MonthlyAnalysisSubAccounting = new CVarRep_A_MonthlyAnalysisSubAccounting();
                        ObjCVarRep_A_MonthlyAnalysisSubAccounting.mJVDate = dr["JVDate"].ToString();
                        ObjCVarRep_A_MonthlyAnalysisSubAccounting.mSubAccount_Name = Convert.ToString(dr["SubAccount_Name"].ToString());
                        ObjCVarRep_A_MonthlyAnalysisSubAccounting.mBalance = Convert.ToDecimal(dr["Balance"].ToString());
                        lstCVarRep_A_MonthlyAnalysisSubAccounting.Add(ObjCVarRep_A_MonthlyAnalysisSubAccounting);
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
