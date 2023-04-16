using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace Forwarding.MvcApp.Models.MasterData.BanksAccountsAndTreasuries.Generated
{
    [Serializable]
    public class CPKCustodyRegions
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
    public partial class CVarCustodyRegions : CPKCustodyRegions
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mCustodyID;
        internal Int32 mPortID;
        internal Int32 mCreatorUserID;
        internal DateTime mCreationDate;
        #endregion

        #region "Methods"
        public Int32 CustodyID
        {
            get { return mCustodyID; }
            set { mIsChanges = true; mCustodyID = value; }
        }
        public Int32 PortID
        {
            get { return mPortID; }
            set { mIsChanges = true; mPortID = value; }
        }
        public Int32 CreatorUserID
        {
            get { return mCreatorUserID; }
            set { mIsChanges = true; mCreatorUserID = value; }
        }
        public DateTime CreationDate
        {
            get { return mCreationDate; }
            set { mIsChanges = true; mCreationDate = value; }
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

    public partial class CCustodyRegions
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
        public List<CVarCustodyRegions> lstCVarCustodyRegions = new List<CVarCustodyRegions>();
        public List<CPKCustodyRegions> lstDeletedCPKCustodyRegions = new List<CPKCustodyRegions>();
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
        public Exception GetItem(Int32 ID)
        {
            return DataFill(Convert.ToString(ID), false);
        }
        private Exception DataFill(string Param, Boolean IsList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarCustodyRegions.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListCustodyRegions";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemCustodyRegions";
                    Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                    Com.Parameters[0].Value = Convert.ToInt32(Param);
                }
                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        /*Start DataReader*/
                        CVarCustodyRegions ObjCVarCustodyRegions = new CVarCustodyRegions();
                        ObjCVarCustodyRegions.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarCustodyRegions.mCustodyID = Convert.ToInt32(dr["CustodyID"].ToString());
                        ObjCVarCustodyRegions.mPortID = Convert.ToInt32(dr["PortID"].ToString());
                        ObjCVarCustodyRegions.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarCustodyRegions.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        lstCVarCustodyRegions.Add(ObjCVarCustodyRegions);
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
            lstCVarCustodyRegions.Clear();

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
                Com.CommandText = "[dbo].GetListPagingCustodyRegions";
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
                        CVarCustodyRegions ObjCVarCustodyRegions = new CVarCustodyRegions();
                        ObjCVarCustodyRegions.ID = Convert.ToInt32(dr["ID"].ToString());
                        ObjCVarCustodyRegions.mCustodyID = Convert.ToInt32(dr["CustodyID"].ToString());
                        ObjCVarCustodyRegions.mPortID = Convert.ToInt32(dr["PortID"].ToString());
                        ObjCVarCustodyRegions.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
                        ObjCVarCustodyRegions.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarCustodyRegions.Add(ObjCVarCustodyRegions);
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
        #region "Common Methods"
        private void BeginTrans(SqlCommand Com, SqlConnection Con)
        {

            tr = Con.BeginTransaction(IsolationLevel.Serializable);
            Com.CommandType = CommandType.StoredProcedure;
        }

        private void EndTrans(SqlCommand Com, SqlConnection Con)
        {

            Com.Transaction = tr;
            Com.Connection = Con;
            Com.ExecuteNonQuery();
            tr.Commit();
        }

        #endregion
        #region "Set List Method"
        private Exception SetList(string WhereClause, Boolean IsDelete)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                if (IsDelete == true)
                    Com.CommandText = "[dbo].DeleteListCustodyRegions";
                else
                    Com.CommandText = "[dbo].UpdateListCustodyRegions";
                Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                BeginTrans(Com, Con);
                Com.Parameters[0].Value = WhereClause;
                EndTrans(Com, Con);
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
        #region "Delete Methods"
        public Exception DeleteItem(List<CPKCustodyRegions> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemCustodyRegions";
                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                foreach (CPKCustodyRegions ObjCPKCustodyRegions in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt32(ObjCPKCustodyRegions.ID);
                    EndTrans(Com, Con);
                }
            }
            catch (Exception ex)
            {
                Exp = ex;
            }
            finally
            {
                Con.Close();
                Con.Dispose();
                DeleteList.Clear();
            }
            return Exp;
        }

        public Exception DeleteList(string WhereClause)
        {

            return SetList(WhereClause, true);
        }

        #endregion
        #region "Save Methods"
        public Exception SaveMethod(List<CVarCustodyRegions> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@CustodyID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@PortID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreatorUserID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime));
                SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, null));
                foreach (CVarCustodyRegions ObjCVarCustodyRegions in SaveList)
                {
                    if (ObjCVarCustodyRegions.mIsChanges == true)
                    {
                        if (ObjCVarCustodyRegions.ID == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemCustodyRegions";
                            paraID.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarCustodyRegions.ID != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemCustodyRegions";
                            paraID.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarCustodyRegions.ID != 0)
                        {
                            Com.Parameters["@ID"].Value = ObjCVarCustodyRegions.ID;
                        }
                        Com.Parameters["@CustodyID"].Value = ObjCVarCustodyRegions.CustodyID;
                        Com.Parameters["@PortID"].Value = ObjCVarCustodyRegions.PortID;
                        Com.Parameters["@CreatorUserID"].Value = ObjCVarCustodyRegions.CreatorUserID;
                        Com.Parameters["@CreationDate"].Value = ObjCVarCustodyRegions.CreationDate;
                        EndTrans(Com, Con);
                        if (ObjCVarCustodyRegions.ID == 0)
                        {
                            ObjCVarCustodyRegions.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
                        }
                        ObjCVarCustodyRegions.mIsChanges = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Exp = ex;
                if (tr != null)
                    tr.Rollback();
            }
            finally
            {
                Con.Close();
                Con.Dispose();
            }
            return Exp;
        }
        #endregion
        #region "Update Methods"
        public Exception UpdateList(string UpdateClause)
        {

            return SetList(UpdateClause, false);
        }

        #endregion
    }
}
