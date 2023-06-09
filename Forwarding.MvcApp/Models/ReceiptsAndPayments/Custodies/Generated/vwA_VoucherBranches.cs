﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.ReceiptsAndPayments.Custodies.Generated
{
    [Serializable]
    public class CPKvwA_VoucherBranches
    {
        #region "variables"
        private Int32 mID;
        #endregion

        #region "Methods"
        public Int32 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        #endregion
    }
    [Serializable]
    public partial class CVarvwA_VoucherBranches : CPKvwA_VoucherBranches
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mCode;
        internal String mName;
        internal String mOperationID;
        internal Boolean misDepartement;
        #endregion

        #region "Methods"
        public String Code
        {
            get { return mCode; }
            set { mCode = value; }
        }
        public String Name
        {
            get { return mName; }
            set { mName = value; }
        }
        public String OperationID
        {
            get { return mOperationID; }
            set { mOperationID = value; }
        }
        public Boolean isDepartement
        {
            get { return misDepartement; }
            set { misDepartement = value; }
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

    public partial class CvwA_VoucherBranches
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
        public List<CVarvwA_VoucherBranches> lstCVarvwA_VoucherBranches = new List<CVarvwA_VoucherBranches>();
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
            lstCVarvwA_VoucherBranches.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.VarChar));
                    Com.CommandText = "[dbo].GetListvwA_VoucherBranches";
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
                        CVarvwA_VoucherBranches ObjCVarvwA_VoucherBranches = new CVarvwA_VoucherBranches();
                        ObjCVarvwA_VoucherBranches.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwA_VoucherBranches.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwA_VoucherBranches.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwA_VoucherBranches.mOperationID = Convert.ToString(dr["OperationID"].ToString());
                        ObjCVarvwA_VoucherBranches.misDepartement = Convert.ToBoolean(dr["isDepartement"].ToString());
                        lstCVarvwA_VoucherBranches.Add(ObjCVarvwA_VoucherBranches);
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
            lstCVarvwA_VoucherBranches.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                Com.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@PageNumber", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.VarChar));
                Com.Parameters.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar));
                Com.CommandText = "[dbo].GetListPagingvwA_VoucherBranches";
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
                        CVarvwA_VoucherBranches ObjCVarvwA_VoucherBranches = new CVarvwA_VoucherBranches();
                        ObjCVarvwA_VoucherBranches.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwA_VoucherBranches.mCode = Convert.ToString(dr["Code"].ToString());
                        ObjCVarvwA_VoucherBranches.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwA_VoucherBranches.mOperationID = Convert.ToString(dr["OperationID"].ToString());
                        ObjCVarvwA_VoucherBranches.misDepartement = Convert.ToBoolean(dr["isDepartement"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwA_VoucherBranches.Add(ObjCVarvwA_VoucherBranches);
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
