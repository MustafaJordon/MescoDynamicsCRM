using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Forwarding.MvcApp.Models.ReceiptsAndPayments.Custodies.Generated
{
    [Serializable]
    public class CPKA_AccreditationMovement
    {
        #region "variables"
        private Int32 mid;
        #endregion

        #region "Methods"
        public Int32 id
        {
            get { return mid; }
            set { mid = value; }
        }
        #endregion
    }
    [Serializable]
    public partial class CVarA_AccreditationMovement : CPKA_AccreditationMovement
    {
        #region "variables"
        internal Boolean mIsChanges = false;
        internal Int32 mRequestCreator;
        internal Int32 mTechnicalDirector;
        internal Int32 mCovenantAccountant;
        internal Int32 mSecretaryTreasury;
        internal Int32 mTreasuryReferences;
        internal Int64 mPaymentRequestID;
        internal Int64 mVoucherID;
        #endregion

        #region "Methods"
        public Int32 RequestCreator
        {
            get { return mRequestCreator; }
            set { mIsChanges = true; mRequestCreator = value; }
        }
        public Int32 TechnicalDirector
        {
            get { return mTechnicalDirector; }
            set { mIsChanges = true; mTechnicalDirector = value; }
        }
        public Int32 CovenantAccountant
        {
            get { return mCovenantAccountant; }
            set { mIsChanges = true; mCovenantAccountant = value; }
        }
        public Int32 SecretaryTreasury
        {
            get { return mSecretaryTreasury; }
            set { mIsChanges = true; mSecretaryTreasury = value; }
        }
        public Int32 TreasuryReferences
        {
            get { return mTreasuryReferences; }
            set { mIsChanges = true; mTreasuryReferences = value; }
        }
        public Int64 PaymentRequestID
        {
            get { return mPaymentRequestID; }
            set { mIsChanges = true; mPaymentRequestID = value; }
        }
        public Int64 VoucherID
        {
            get { return mVoucherID; }
            set { mIsChanges = true; mVoucherID = value; }
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

    public partial class CA_AccreditationMovement
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
        public List<CVarA_AccreditationMovement> lstCVarA_AccreditationMovement = new List<CVarA_AccreditationMovement>();
        public List<CPKA_AccreditationMovement> lstDeletedCPKA_AccreditationMovement = new List<CPKA_AccreditationMovement>();
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
        public Exception GetItem(Int32 id)
        {
            return DataFill(Convert.ToString(id), false);
        }
        private Exception DataFill(string Param, Boolean IsList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            lstCVarA_AccreditationMovement.Clear();

            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;
                if (IsList == true)
                {
                    Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
                    Com.CommandText = "[dbo].GetListA_AccreditationMovement";
                    Com.Parameters[0].Value = Param;
                }
                else
                {
                    Com.CommandText = "[dbo].GetItemA_AccreditationMovement";
                    Com.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
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
                        CVarA_AccreditationMovement ObjCVarA_AccreditationMovement = new CVarA_AccreditationMovement();
                        ObjCVarA_AccreditationMovement.id = Convert.ToInt32(dr["id"].ToString());
                        ObjCVarA_AccreditationMovement.mRequestCreator = Convert.ToInt32(dr["RequestCreator"].ToString());
                        ObjCVarA_AccreditationMovement.mTechnicalDirector = Convert.ToInt32(dr["TechnicalDirector"].ToString());
                        ObjCVarA_AccreditationMovement.mCovenantAccountant = Convert.ToInt32(dr["CovenantAccountant"].ToString());
                        ObjCVarA_AccreditationMovement.mSecretaryTreasury = Convert.ToInt32(dr["SecretaryTreasury"].ToString());
                        ObjCVarA_AccreditationMovement.mTreasuryReferences = Convert.ToInt32(dr["TreasuryReferences"].ToString());
                        ObjCVarA_AccreditationMovement.mPaymentRequestID = Convert.ToInt64(dr["PaymentRequestID"].ToString());
                        ObjCVarA_AccreditationMovement.mVoucherID = Convert.ToInt64(dr["VoucherID"].ToString());
                        lstCVarA_AccreditationMovement.Add(ObjCVarA_AccreditationMovement);
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
            lstCVarA_AccreditationMovement.Clear();

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
                Com.CommandText = "[dbo].GetListPagingA_AccreditationMovement";
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
                        CVarA_AccreditationMovement ObjCVarA_AccreditationMovement = new CVarA_AccreditationMovement();
                        ObjCVarA_AccreditationMovement.id = Convert.ToInt32(dr["id"].ToString());
                        ObjCVarA_AccreditationMovement.mRequestCreator = Convert.ToInt32(dr["RequestCreator"].ToString());
                        ObjCVarA_AccreditationMovement.mTechnicalDirector = Convert.ToInt32(dr["TechnicalDirector"].ToString());
                        ObjCVarA_AccreditationMovement.mCovenantAccountant = Convert.ToInt32(dr["CovenantAccountant"].ToString());
                        ObjCVarA_AccreditationMovement.mSecretaryTreasury = Convert.ToInt32(dr["SecretaryTreasury"].ToString());
                        ObjCVarA_AccreditationMovement.mTreasuryReferences = Convert.ToInt32(dr["TreasuryReferences"].ToString());
                        ObjCVarA_AccreditationMovement.mPaymentRequestID = Convert.ToInt64(dr["PaymentRequestID"].ToString());
                        ObjCVarA_AccreditationMovement.mVoucherID = Convert.ToInt64(dr["VoucherID"].ToString());
                        TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
                        lstCVarA_AccreditationMovement.Add(ObjCVarA_AccreditationMovement);
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
                    Com.CommandText = "[dbo].DeleteListA_AccreditationMovement";
                else
                    Com.CommandText = "[dbo].UpdateListA_AccreditationMovement";
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
        public Exception DeleteItem(List<CPKA_AccreditationMovement> DeleteList)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.CommandText = "[dbo].DeleteItemA_AccreditationMovement";
                Com.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                foreach (CPKA_AccreditationMovement ObjCPKA_AccreditationMovement in DeleteList)
                {
                    BeginTrans(Com, Con);
                    Com.Parameters[0].Value = Convert.ToInt32(ObjCPKA_AccreditationMovement.id);
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
        public Exception SaveMethod(List<CVarA_AccreditationMovement> SaveList)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            try
            {
                Con.Open();
                Com = new SqlCommand();
                Com.Parameters.Add(new SqlParameter("@RequestCreator", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@TechnicalDirector", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@CovenantAccountant", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@SecretaryTreasury", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@TreasuryReferences", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@PaymentRequestID", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@VoucherID", SqlDbType.BigInt));
                SqlParameter paraid = Com.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "id", DataRowVersion.Default, null));
                foreach (CVarA_AccreditationMovement ObjCVarA_AccreditationMovement in SaveList)
                {
                    if (ObjCVarA_AccreditationMovement.mIsChanges == true)
                    {
                        if (ObjCVarA_AccreditationMovement.id == 0)
                        {
                            Com.CommandText = "[dbo].InsertItemA_AccreditationMovement";
                            paraid.Direction = ParameterDirection.Output;
                        }
                        else if (ObjCVarA_AccreditationMovement.id != 0)
                        {
                            Com.CommandText = "[dbo].UpdateItemA_AccreditationMovement";
                            paraid.Direction = ParameterDirection.Input;
                        }
                        BeginTrans(Com, Con);
                        if (ObjCVarA_AccreditationMovement.id != 0)
                        {
                            Com.Parameters["@id"].Value = ObjCVarA_AccreditationMovement.id;
                        }
                        Com.Parameters["@RequestCreator"].Value = ObjCVarA_AccreditationMovement.RequestCreator;
                        Com.Parameters["@TechnicalDirector"].Value = ObjCVarA_AccreditationMovement.TechnicalDirector;
                        Com.Parameters["@CovenantAccountant"].Value = ObjCVarA_AccreditationMovement.CovenantAccountant;
                        Com.Parameters["@SecretaryTreasury"].Value = ObjCVarA_AccreditationMovement.SecretaryTreasury;
                        Com.Parameters["@TreasuryReferences"].Value = ObjCVarA_AccreditationMovement.TreasuryReferences;
                        Com.Parameters["@PaymentRequestID"].Value = ObjCVarA_AccreditationMovement.PaymentRequestID;
                        Com.Parameters["@VoucherID"].Value = ObjCVarA_AccreditationMovement.VoucherID;
                        EndTrans(Com, Con);
                        if (ObjCVarA_AccreditationMovement.id == 0)
                        {
                            ObjCVarA_AccreditationMovement.id = Convert.ToInt32(Com.Parameters["@id"].Value);
                        }
                        ObjCVarA_AccreditationMovement.mIsChanges = false;
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
