﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.CRM.Generated
{
    [Serializable]
    public partial class CVarvwCRM_CommissionTarget
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mID;
        internal Int32 mSalesmanID;
        internal String mSalesmanName;
        internal String mSalesmanLocalName;
        internal Int32 mTargetYear;
        internal Int32 mTargetMonth;
        internal String mTargetMonthName;
        internal Int32 mTargetTypeID;
        internal String mTargetTypeName;
        internal Decimal mAmount;
        internal Decimal mPercentage;
        internal String mNotes;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        internal Int32 mModificatorUserID;
        internal DateTime mModificationDate;
        internal Decimal mGrossProfit;
        #endregion

        #region "Methods"
        public Int32 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public Int32 SalesmanID
        {
            get { return mSalesmanID; }
            set { mSalesmanID = value; }
        }
        public String SalesmanName
        {
            get { return mSalesmanName; }
            set { mSalesmanName = value; }
        }
        public String SalesmanLocalName
        {
            get { return mSalesmanLocalName; }
            set { mSalesmanLocalName = value; }
        }
        public Int32 TargetYear
        {
            get { return mTargetYear; }
            set { mTargetYear = value; }
        }
        public Int32 TargetMonth
        {
            get { return mTargetMonth; }
            set { mTargetMonth = value; }
        }
        public String TargetMonthName
        {
            get { return mTargetMonthName; }
            set { mTargetMonthName = value; }
        }
        public Int32 TargetTypeID
        {
            get { return mTargetTypeID; }
            set { mTargetTypeID = value; }
        }
        public String TargetTypeName
        {
            get { return mTargetTypeName; }
            set { mTargetTypeName = value; }
        }
        public Decimal Amount
        {
            get { return mAmount; }
            set { mAmount = value; }
        }
        public Decimal Percentage
        {
            get { return mPercentage; }
            set { mPercentage = value; }
        }
        public String Notes
        {
            get { return mNotes; }
            set { mNotes = value; }
        }
        public Int32 CreatorUserID
        {
            get { return mCreatorUserID; }
            set { mCreatorUserID = value; }
        }
        public DateTime CreationDate
        {
            get { return mCreationDate; }
            set { mCreationDate = value; }
        }
        public Int32 ModificatorUserID
        {
            get { return mModificatorUserID; }
            set { mModificatorUserID = value; }
        }
        public DateTime ModificationDate
        {
            get { return mModificationDate; }
            set { mModificationDate = value; }
        }
        public Decimal GrossProfit
        {
            get { return mGrossProfit; }
            set { mGrossProfit = value; }
        }
        #endregion
    }

    public partial class CvwCRM_CommissionTarget
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
        public List<CVarvwCRM_CommissionTarget> lstCVarvwCRM_CommissionTarget = new List<CVarvwCRM_CommissionTarget>();
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
            lstCVarvwCRM_CommissionTarget.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.VarChar));
                    Com.CommandText = "[dbo].GetListvwCRM_CommissionTarget";
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
                        CVarvwCRM_CommissionTarget ObjCVarvwCRM_CommissionTarget = new CVarvwCRM_CommissionTarget();
                        ObjCVarvwCRM_CommissionTarget.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mSalesmanID = Convert.ToInt32(dr["SalesmanID"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mSalesmanName = Convert.ToString(dr["SalesmanName"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mSalesmanLocalName = Convert.ToString(dr["SalesmanLocalName"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mTargetYear = Convert.ToInt32(dr["TargetYear"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mTargetMonth = Convert.ToInt32(dr["TargetMonth"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mTargetMonthName = Convert.ToString(dr["TargetMonthName"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mTargetTypeID = Convert.ToInt32(dr["TargetTypeID"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mTargetTypeName = Convert.ToString(dr["TargetTypeName"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mAmount = Convert.ToDecimal(dr["Amount"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mPercentage = Convert.ToDecimal(dr["Percentage"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mGrossProfit = Convert.ToDecimal(dr["GrossProfit"].ToString());
                        lstCVarvwCRM_CommissionTarget.Add(ObjCVarvwCRM_CommissionTarget);
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
            lstCVarvwCRM_CommissionTarget.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwCRM_CommissionTarget";
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
                        CVarvwCRM_CommissionTarget ObjCVarvwCRM_CommissionTarget = new CVarvwCRM_CommissionTarget();
                        ObjCVarvwCRM_CommissionTarget.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mSalesmanID = Convert.ToInt32(dr["SalesmanID"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mSalesmanName = Convert.ToString(dr["SalesmanName"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mSalesmanLocalName = Convert.ToString(dr["SalesmanLocalName"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mTargetYear = Convert.ToInt32(dr["TargetYear"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mTargetMonth = Convert.ToInt32(dr["TargetMonth"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mTargetMonthName = Convert.ToString(dr["TargetMonthName"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mTargetTypeID = Convert.ToInt32(dr["TargetTypeID"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mTargetTypeName = Convert.ToString(dr["TargetTypeName"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mAmount = Convert.ToDecimal(dr["Amount"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mPercentage = Convert.ToDecimal(dr["Percentage"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mNotes = Convert.ToString(dr["Notes"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
                        ObjCVarvwCRM_CommissionTarget.mGrossProfit = Convert.ToDecimal(dr["GrossProfit"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwCRM_CommissionTarget.Add(ObjCVarvwCRM_CommissionTarget);
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
