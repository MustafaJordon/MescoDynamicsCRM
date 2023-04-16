﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Controllers.SL.API_SL_Transactions
{
    [Serializable]
    public partial class CVarvwClientsGroups
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mID;
        internal String mName;
        internal String mParentCode;
        internal Int32 mLevel;
        internal String mNewCode;
        internal Int32 mRemained;
        #endregion

        #region "Methods"
        public Int32 ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public String Name
        {
            get { return mName; }
            set { mName = value; }
        }
        public String ParentCode
        {
            get { return mParentCode; }
            set { mParentCode = value; }
        }
        public Int32 Level
        {
            get { return mLevel; }
            set { mLevel = value; }
        }
        public String NewCode
        {
            get { return mNewCode; }
            set { mNewCode = value; }
        }
        public Int32 Remained
        {
            get { return mRemained; }
            set { mRemained = value; }
        }
        #endregion
    }

    public partial class CvwClientsGroups
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
        public List<CVarvwClientsGroups> lstCVarvwClientsGroups = new List<CVarvwClientsGroups>();
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
            lstCVarvwClientsGroups.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.VarChar));
                    Com.CommandText = "[dbo].GetListvwClientsGroups";
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
                        CVarvwClientsGroups ObjCVarvwClientsGroups = new CVarvwClientsGroups();
                        ObjCVarvwClientsGroups.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwClientsGroups.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwClientsGroups.mParentCode = Convert.ToString(dr["ParentCode"].ToString());
                        ObjCVarvwClientsGroups.mLevel = Convert.ToInt32(dr["Level"].ToString());
                        ObjCVarvwClientsGroups.mNewCode = Convert.ToString(dr["NewCode"].ToString());
                        ObjCVarvwClientsGroups.mRemained = Convert.ToInt32(dr["Remained"].ToString());
                        lstCVarvwClientsGroups.Add(ObjCVarvwClientsGroups);
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
            lstCVarvwClientsGroups.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwClientsGroups";
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
                        CVarvwClientsGroups ObjCVarvwClientsGroups = new CVarvwClientsGroups();
                        ObjCVarvwClientsGroups.mID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwClientsGroups.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwClientsGroups.mParentCode = Convert.ToString(dr["ParentCode"].ToString());
                        ObjCVarvwClientsGroups.mLevel = Convert.ToInt32(dr["Level"].ToString());
                        ObjCVarvwClientsGroups.mNewCode = Convert.ToString(dr["NewCode"].ToString());
                        ObjCVarvwClientsGroups.mRemained = Convert.ToInt32(dr["Remained"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwClientsGroups.Add(ObjCVarvwClientsGroups);
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
