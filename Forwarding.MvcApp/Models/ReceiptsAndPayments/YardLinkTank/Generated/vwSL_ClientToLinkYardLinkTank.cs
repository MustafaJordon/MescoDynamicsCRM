﻿using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.ReceiptsAndPayments.YardLinkTank.Generated
{

    [Serializable]
    public class CPKvwSL_ClientToLinkYardLinkTank
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
    public partial class CVarvwSL_ClientToLinkYardLinkTank : CPKvwSL_ClientToLinkYardLinkTank
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal String mName;
        internal String mArName;
        internal Int32 mERPClientID;
        #endregion

        #region "Methods"
        public String Name
        {
            get { return mName; }
            set { mName = value; }
        }
        public String ArName
        {
            get { return mArName; }
            set { mArName = value; }
        }
        public Int32 ERPClientID
        {
            get { return mERPClientID; }
            set { mERPClientID = value; }
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


    public partial class CvwSL_ClientToLinkYardLinkTank
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
        public List<CVarvwSL_ClientToLinkYardLinkTank> lstCVarvwSL_ClientToLinkYardLinkTank = new List<CVarvwSL_ClientToLinkYardLinkTank>();
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
            lstCVarvwSL_ClientToLinkYardLinkTank.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListvwSL_ClientToLinkYardLinkTank";
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
                        CVarvwSL_ClientToLinkYardLinkTank ObjCVarvwSL_ClientToLinkYardLinkTank = new CVarvwSL_ClientToLinkYardLinkTank();
                        ObjCVarvwSL_ClientToLinkYardLinkTank.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwSL_ClientToLinkYardLinkTank.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwSL_ClientToLinkYardLinkTank.mArName = Convert.ToString(dr["ArName"].ToString());
                        ObjCVarvwSL_ClientToLinkYardLinkTank.mERPClientID = Convert.ToInt32(dr["ERPClientID"].ToString());
                        lstCVarvwSL_ClientToLinkYardLinkTank.Add(ObjCVarvwSL_ClientToLinkYardLinkTank);
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
            lstCVarvwSL_ClientToLinkYardLinkTank.Clear();

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
                Com.CommandText = "[dbo].GetListPagingvwSL_ClientToLinkYardLinkTank";
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
                        CVarvwSL_ClientToLinkYardLinkTank ObjCVarvwSL_ClientToLinkYardLinkTank = new CVarvwSL_ClientToLinkYardLinkTank();
                        ObjCVarvwSL_ClientToLinkYardLinkTank.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarvwSL_ClientToLinkYardLinkTank.mName = Convert.ToString(dr["Name"].ToString());
                        ObjCVarvwSL_ClientToLinkYardLinkTank.mArName = Convert.ToString(dr["ArName"].ToString());
                        ObjCVarvwSL_ClientToLinkYardLinkTank.mERPClientID = Convert.ToInt32(dr["ERPClientID"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarvwSL_ClientToLinkYardLinkTank.Add(ObjCVarvwSL_ClientToLinkYardLinkTank);
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
