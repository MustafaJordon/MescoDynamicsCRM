using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.MasterData.Invoicing.Generated
{
    [Serializable]
    public class CPKvwServices
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
    public partial class CVarvwServices : CPKvwServices
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mName;
        internal Int32 mAccountID;
        internal Int32 mSubAccountID;
        internal Int32 mCostCenterID;
        internal Int64 mCode;
        internal String mPreCode;
        internal String mAccountName;
        internal String mSubAccountName;
        internal String mCostCenterName;
        #endregion

        #region "Methods"
        public String Name
        {
            get { return mName; }
            set { mName = value; }
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
        public Int32 CostCenterID
        {
            get { return mCostCenterID; }
            set { mCostCenterID = value; }
        }
        public Int64 Code
        {
            get { return mCode; }
            set { mCode = value; }
        }
        public String PreCode
        {
            get { return mPreCode; }
            set { mPreCode = value; }
        }
        public String AccountName
        {
            get { return mAccountName; }
            set { mAccountName = value; }
        }
        public String SubAccountName
        {
            get { return mSubAccountName; }
            set { mSubAccountName = value; }
        }
        public String CostCenterName
        {
            get { return mCostCenterName; }
            set { mCostCenterName = value; }
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

    public partial class CvwServices
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
        public List<CVarvwServices> lstCVarvwServices = new List<CVarvwServices>();
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
            lstCVarvwServices.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwServices";
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
                        CVarvwServices ObjCVarvwServices = new CVarvwServices();
                        ObjCVarvwServices.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwServices.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwServices.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarvwServices.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarvwServices.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarvwServices.mCode = Convert.ToInt64(dr["Code"].ToString());
                        ObjCVarvwServices.mPreCode = Convert.ToString(dr["PreCode"].ToString());
                        ObjCVarvwServices.mAccountName = Convert.ToString(dr["AccountName"].ToString());
                        ObjCVarvwServices.mSubAccountName = Convert.ToString(dr["SubAccountName"].ToString());
                        ObjCVarvwServices.mCostCenterName = Convert.ToString(dr["CostCenterName"].ToString());
                        lstCVarvwServices.Add(ObjCVarvwServices);
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
            lstCVarvwServices.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwServices";
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
                        CVarvwServices ObjCVarvwServices = new CVarvwServices();
                        ObjCVarvwServices.ID = Convert.ToInt64(dr["ID"].ToString());
                        ObjCVarvwServices.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwServices.mAccountID = Convert.ToInt32(dr["AccountID"].ToString());
                        ObjCVarvwServices.mSubAccountID = Convert.ToInt32(dr["SubAccountID"].ToString());
                        ObjCVarvwServices.mCostCenterID = Convert.ToInt32(dr["CostCenterID"].ToString());
                        ObjCVarvwServices.mCode = Convert.ToInt64(dr["Code"].ToString());
                        ObjCVarvwServices.mPreCode = Convert.ToString(dr["PreCode"].ToString());
                        ObjCVarvwServices.mAccountName = Convert.ToString(dr["AccountName"].ToString());
                        ObjCVarvwServices.mSubAccountName = Convert.ToString(dr["SubAccountName"].ToString());
                        ObjCVarvwServices.mCostCenterName = Convert.ToString(dr["CostCenterName"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwServices.Add(ObjCVarvwServices);
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
