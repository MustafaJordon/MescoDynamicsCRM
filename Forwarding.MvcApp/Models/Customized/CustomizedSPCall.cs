using Forwarding.MvcApp.Models.Sales.Transactions.Generated.Payments.Generated;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace Forwarding.MvcApp.Models.Customized
{
    public class CCustomizedDBCall
    {

        #region DB Customized SPs
        public Exception SP_Trans_Log(string pStoredProcedureName, Int32 pUser_ID, string pTable_Name, Int64 pRow_ID, string pTrans_Type)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;

                Com.Parameters.Add(new SqlParameter("@User_ID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@Table_Name", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@Row_ID ", SqlDbType.BigInt));
                Com.Parameters.Add(new SqlParameter("@Trans_Type ", SqlDbType.NVarChar));
                //Com.CommandText = "[dbo].ERP_ForwWeb_PaymentDetails";
                Com.CommandText = "[dbo]." + pStoredProcedureName;
                Com.Parameters[0].Value = pUser_ID;
                Com.Parameters[1].Value = pTable_Name;
                Com.Parameters[2].Value = pRow_ID;
                Com.Parameters[3].Value = pTrans_Type;

                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
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
        public Exception SP_A_SubAccounts_Details(string pStoredProcedureName, string pTrans_Type, Int32 pSubAccount_ID, Int32 pAccount_ID, bool pIsMain)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;

                Com.Parameters.Add(new SqlParameter("@TransType ", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@SubAccount_ID ", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@Account_ID ", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@IsMain ", SqlDbType.Bit));

                //Com.CommandText = "[dbo].ERP_ForwWeb_PaymentDetails";
                Com.CommandText = "[dbo]." + pStoredProcedureName;
                Com.Parameters[0].Value = pTrans_Type;
                Com.Parameters[1].Value = pSubAccount_ID;
                Com.Parameters[2].Value = pAccount_ID;
                Com.Parameters[3].Value = pIsMain;

                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
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
        public Exception A_SingleID_Posting(string pStoredProcedureName, String pID, Int32 pUser_ID)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;

                Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.VarChar));
                Com.Parameters.Add(new SqlParameter("@UserID ", SqlDbType.Int));
                //Com.CommandText = "[dbo].ERP_ForwWeb_PaymentDetails";
                Com.CommandText = "[dbo]." + pStoredProcedureName;
                Com.Parameters[0].Value = pID;
                Com.Parameters[1].Value = pUser_ID;

                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
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

        public Exception A_CashInVouchers_Posting(string pStoredProcedureName, string pCashInVoucherIDs, DateTime pGivenDate, Int32 pUser_ID)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;

                Com.Parameters.Add(new SqlParameter("@CashInVoucherIDs", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@GivenDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@UserID ", SqlDbType.Int));
                //Com.CommandText = "[dbo].ERP_ForwWeb_PaymentDetails";
                Com.CommandText = "[dbo]." + pStoredProcedureName;
                Com.Parameters[0].Value = pCashInVoucherIDs;
                Com.Parameters[1].Value = pGivenDate;
                Com.Parameters[2].Value = pUser_ID;

                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
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
        public Exception A_CashOutVouchers_Posting(string pStoredProcedureName, string pCashOutVoucherIDs, DateTime pGivenDate, Int32 pUser_ID)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;

                Com.Parameters.Add(new SqlParameter("@CashOutVoucherIDs", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@GivenDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@UserID ", SqlDbType.Int));
                //Com.CommandText = "[dbo].ERP_ForwWeb_PaymentDetails";
                Com.CommandText = "[dbo]." + pStoredProcedureName;
                Com.Parameters[0].Value = pCashOutVoucherIDs;
                Com.Parameters[1].Value = pGivenDate;
                Com.Parameters[2].Value = pUser_ID;

                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
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
        public Exception A_ChequeInVouchers_Posting(string pStoredProcedureName, string pChequeInVoucherIDs, DateTime pGivenDate, Int32 pUser_ID)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;

                Com.Parameters.Add(new SqlParameter("@ChequeInVoucherIDs", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@GivenDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@UserID ", SqlDbType.Int));
                //Com.CommandText = "[dbo].ERP_ForwWeb_PaymentDetails";
                Com.CommandText = "[dbo]." + pStoredProcedureName;
                Com.Parameters[0].Value = pChequeInVoucherIDs;
                Com.Parameters[1].Value = pGivenDate;
                Com.Parameters[2].Value = pUser_ID;

                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
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
        public Exception A_ChequeOutVouchers_Posting(string pStoredProcedureName, string pChequeOutVoucherIDs, DateTime pGivenDate, Int32 pUser_ID)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;

                Com.Parameters.Add(new SqlParameter("@ChequeOutVoucherIDs", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@GivenDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@UserID ", SqlDbType.Int));
                //Com.CommandText = "[dbo].ERP_ForwWeb_PaymentDetails";
                Com.CommandText = "[dbo]." + pStoredProcedureName;
                Com.Parameters[0].Value = pChequeOutVoucherIDs;
                Com.Parameters[1].Value = pGivenDate;
                Com.Parameters[2].Value = pUser_ID;

                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
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
        //used for both SPs: A_ChequeStatus_PostPayable, A_ChequeStatus_PostReceivable
        public Exception A_ChequeStatus_PostingReceivablePayableNotes(string pStoredProcedureName, Int64 pVoucherID, DateTime pJVDate, Int32 pUser_ID)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;

                Com.Parameters.Add(new SqlParameter("@VoucherID", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@JVDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@UserID ", SqlDbType.Int));
                //Com.CommandText = "[dbo].ERP_ForwWeb_PaymentDetails";
                Com.CommandText = "[dbo]." + pStoredProcedureName;
                Com.Parameters[0].Value = pVoucherID;
                Com.Parameters[1].Value = pJVDate;
                Com.Parameters[2].Value = pUser_ID;

                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
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
        public Exception A_CashVouchers_UnPosted_ByID(string pStoredProcedureName, string pVoucherIDs, int pUserID)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;

                Com.Parameters.Add(new SqlParameter("@VoucherIDs", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int));
                //Com.CommandText = "[dbo].ERP_ForwWeb_PaymentDetails";
                Com.CommandText = "[dbo]." + pStoredProcedureName;
                Com.Parameters[0].Value = pVoucherIDs;
                Com.Parameters[1].Value = pUserID;

                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
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
        public Exception A_ChequeVouchers_UnPosted_ByID(string pStoredProcedureName, string pVoucherIDs, int pUserID)
        {

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;

                Com.Parameters.Add(new SqlParameter("@VoucherIDs", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int));
                //Com.CommandText = "[dbo].ERP_ForwWeb_PaymentDetails";
                Com.CommandText = "[dbo]." + pStoredProcedureName;
                Com.Parameters[0].Value = pVoucherIDs;
                Com.Parameters[1].Value = pUserID;

                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
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
        public string SP_A_JV_Get_Code(string pStoredProcedureName, DateTime pDate, Int32 pUserID, Int32 pJournal_ID)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            string pNewCode = "";
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;

                Com.Parameters.Add(new SqlParameter("@Date ", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@UserID ", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@Journal_ID ", SqlDbType.Int));

                //Com.CommandText = "[dbo].ERP_ForwWeb_PaymentDetails";
                Com.CommandText = "[dbo]." + pStoredProcedureName;
                Com.Parameters[0].Value = pDate;
                Com.Parameters[1].Value = pUserID;
                Com.Parameters[2].Value = pJournal_ID;

                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        pNewCode = dr[0].ToString();
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
            return pNewCode;
        }
        public string A_CashVoucher_GetCode_BySafeCode(string pStoredProcedureName, Int32 pSafeID, DateTime pDate, Int32 pVoucherType)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            string pNewCode = "";
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;

                Com.Parameters.Add(new SqlParameter("@SafeID ", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@Date ", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@VoucherType ", SqlDbType.Int));

                //Com.CommandText = "[dbo].ERP_ForwWeb_PaymentDetails";
                Com.CommandText = "[dbo]." + pStoredProcedureName;
                Com.Parameters[0].Value = pSafeID;
                Com.Parameters[1].Value = pDate;
                Com.Parameters[2].Value = pVoucherType;

                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        pNewCode = dr[0].ToString();
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
            return pNewCode;
        }
        public string A_ChequeVoucher_GetCodeByBank(string pStoredProcedureName, DateTime pDate, Int32 pBankID, Int32 pVoucherType, Int32 pSafeID)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            string pNewCode = "";
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;

                Com.Parameters.Add(new SqlParameter("@Date ", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@BankID ", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@VoucherType ", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@SafeID ", SqlDbType.Int));

                //Com.CommandText = "[dbo].ERP_ForwWeb_PaymentDetails";
                Com.CommandText = "[dbo]." + pStoredProcedureName;
                Com.Parameters[0].Value = pDate;
                Com.Parameters[1].Value = pBankID;
                Com.Parameters[2].Value = pVoucherType;
                Com.Parameters[3].Value = pSafeID;

                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        pNewCode = dr[0].ToString();
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
            return pNewCode;
        }

        public string A_ChequeVoucher_GetNextChequeNo(string pStoredProcedureName, Int32 pBankID, Int32 pVoucherType)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            string pNewCode = "";
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;

                Com.Parameters.Add(new SqlParameter("@BankID ", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@VoucherType ", SqlDbType.Int));

                //Com.CommandText = "[dbo].ERP_ForwWeb_PaymentDetails";
                Com.CommandText = "[dbo]." + pStoredProcedureName;
                Com.Parameters[0].Value = pBankID;
                Com.Parameters[1].Value = pVoucherType;

                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        pNewCode = dr[0].ToString();
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
            return pNewCode;
        }


        public string A_CashVoucher_GetCode_BySafeCode2(string pStoredProcedureName, Int32 pSafeID, DateTime pDate, Int32 pVoucherType)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            string pNewCode = "";
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;

                Com.Parameters.Add(new SqlParameter("@SafeID ", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@Date ", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@VoucherType ", SqlDbType.Int));

                //Com.CommandText = "[dbo].ERP_ForwWeb_PaymentDetails";
                Com.CommandText = "[dbo]." + pStoredProcedureName;
                Com.Parameters[0].Value = pSafeID;
                Com.Parameters[1].Value = pDate;
                Com.Parameters[2].Value = pVoucherType;

                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        pNewCode = dr[0].ToString();
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
            return pNewCode;
        }
        public string A_ChequeVoucher_GetCodeByBank2(string pStoredProcedureName, DateTime pDate, Int32 pBankID, Int32 pVoucherType, Int32 pSafeID)

        {
            Exception Exp = null;
            // SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            // SqlTransaction tr;

            //********
            GlobalConnection.myCommand = new SqlCommand();
            GlobalConnection.myCommand.Connection = GlobalConnection.myConnection;
            GlobalConnection.myCommand.Transaction = GlobalConnection.myTrans;

            GlobalConnection.myCommand.CommandType = CommandType.StoredProcedure;
            GlobalConnection.myCommand.CommandTimeout = 0;
            Com = GlobalConnection.myCommand;
            //*********




            string pNewCode = "";
            try
            {
                // Con.Open();
                // tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                // Com = new SqlCommand();
                // Com.CommandType = CommandType.StoredProcedure;
                // Com.CommandTimeout = 800;


                Com.Parameters.Add(new SqlParameter("@Date ", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@BankID ", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@VoucherType ", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@SafeID ", SqlDbType.Int));

                //Com.CommandText = "[dbo].ERP_ForwWeb_PaymentDetails";
                Com.CommandText = "[dbo]." + pStoredProcedureName;
                Com.Parameters[0].Value = pDate;
                Com.Parameters[1].Value = pBankID;
                Com.Parameters[2].Value = pVoucherType;
                Com.Parameters[3].Value = pSafeID;

                //Com.Transaction = tr;
                // Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        pNewCode = dr[0].ToString();
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
                // tr.Commit();
            }
            catch (Exception ex)
            {
                Exp = ex;
            }
            finally
            {
                // Con.Close();
                //  Con.Dispose();
            }
            return pNewCode;
        }



        public string A_SubAccounts_CheckForDelete(string pStoredProcedureName, Int32 pSubAccountID)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            string pHasDetails = "";
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;

                Com.Parameters.Add(new SqlParameter("@SubAccountID ", SqlDbType.Int));

                //Com.CommandText = "[dbo].ERP_ForwWeb_PaymentDetails";
                Com.CommandText = "[dbo]." + pStoredProcedureName;
                Com.Parameters[0].Value = pSubAccountID;

                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        pHasDetails = dr[0].ToString();
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
            return pHasDetails;
        }

        public Exception SP_InsertUpdateSLInvoiceJVs_Item(string pShipping_InvoiceIDs, Int64 pJVID2)
        {
            string pStoredProcedureName = "InsertUpdateItemSL_InvoiceJVs";

            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;

                Com.Parameters.Add(new SqlParameter("@Shipping_InvoiceIDs", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@JVID2", SqlDbType.Int));

                Com.CommandText = "[dbo]." + pStoredProcedureName;
                Com.Parameters[0].Value = pShipping_InvoiceIDs;
                Com.Parameters[1].Value = pJVID2;


                Com.CommandTimeout = 200;
                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
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

        public Exception SP_IST_Proc(string pStoredProcedureName, DateTime pFromDate)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;

                Com.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime));

                Com.CommandText = "[dbo]." + pStoredProcedureName;
                Com.Parameters[0].Value = pFromDate;



                Com.CommandTimeout = 10000;
                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
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

        public Exception SP_IST_Proc(string pStoredProcedureName)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;

                Com.CommandText = "[dbo]." + pStoredProcedureName;

                Com.CommandTimeout = 10000;
                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
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

        public Exception SP_PS_PostingInvoicesGroubByTax(string pStoredProcedureName,string pIDs, DateTime pJvDate , int pTaxDebitID  , int pTaxCreditID , int pUserID)
        {
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                Com = new SqlCommand();
                Com.CommandType = CommandType.StoredProcedure;


                Com.Parameters.Add(new SqlParameter("@IDs", SqlDbType.NVarChar));
                Com.Parameters.Add(new SqlParameter("@JvDate", SqlDbType.DateTime));
                Com.Parameters.Add(new SqlParameter("@TaxDebitID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@TaxCreditID", SqlDbType.Int));
                Com.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int));

                Com.CommandText = "[dbo]." + pStoredProcedureName;
                Com.Parameters[0].Value = pIDs;
                Com.Parameters[1].Value = pJvDate;
                Com.Parameters[2].Value = pTaxDebitID;
                Com.Parameters[3].Value = pTaxCreditID;
                Com.Parameters[4].Value = pUserID;



                Com.CommandTimeout = 10000;
                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
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
        #endregion DB Customized SPs

        #region DB functions
        public string CallStringFunction(string pStrQuery)
        {
            var NewCode = "";
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                //Com = new SqlCommand("SELECT [dbo]." + pFunctionName + "(" + (pCostCenterParentID == 0 ? "null" : pCostCenterParentID.ToString()) + ") AS Code",Con);
                Com = new SqlCommand(pStrQuery, Con);
                //Com.CommandType = CommandType.StoredProcedure;
                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        NewCode = dr[0].ToString();
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
            return NewCode;
        }

        public string CallStringFunctionWithImage( byte[] pdata,string pInvoiceID)
        {
            var NewCode = "";
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                //Com = new SqlCommand("SELECT [dbo]." + pFunctionName + "(" + (pCostCenterParentID == 0 ? "null" : pCostCenterParentID.ToString()) + ") AS Code",Con);      
                // Com = new SqlCommand(pStrQuery, Con);

                string ProcString = @"if exists(select 1 from[InvoiceQRImage] QR where QR.InvoiceID = @InvoiceID) update[InvoiceQRImage] set QRImage = @Img where InvoiceID = @InvoiceID  else  insert into[InvoiceQRImage] values(@InvoiceID, @Img)";


                Com = new SqlCommand(ProcString, Con);
                Com.Parameters.Clear();
                Com.Parameters.AddWithValue("@InvoiceID", pInvoiceID);
                Com.Parameters.AddWithValue("@Img", pdata);
                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        NewCode = dr[0].ToString();
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
            return NewCode;
        }

        public object GetImageByInvoiceID(string InvoiceID)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            object pImage = null;
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand("select QRImage from InvoiceQRImage where InvoiceID=" + InvoiceID, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);
            MemoryStream ms =new MemoryStream();
            if (ds.Tables[0].Rows.Count > 0)

            {
                pImage = Convert.ToBase64String((byte[])ds.Tables[0].Rows[0]["QRImage"]);
             //  ms = new MemoryStream((byte[])ds.Tables[0].Rows[0]["QRImage"]);

            }
            return pImage;
        }

        public string CallStringFunctionByMultiReturn(string pStrQuery)
        {
            var NewCode = "";
            Exception Exp = null;
            SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand Com;
            SqlDataReader dr;
            SqlTransaction tr;
            try
            {
                Con.Open();
                tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
                //Com = new SqlCommand("SELECT [dbo]." + pFunctionName + "(" + (pCostCenterParentID == 0 ? "null" : pCostCenterParentID.ToString()) + ") AS Code",Con);
                Com = new SqlCommand(pStrQuery, Con);
                //Com.CommandType = CommandType.StoredProcedure;
                Com.Transaction = tr;
                Com.Connection = Con;
                dr = Com.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        NewCode += dr[0].ToString() + ",";
                    }
                }
                catch (Exception ex)
                {
                    Exp = ex;
                }
                finally
                {
                    NewCode = NewCode.Substring(0, (NewCode.Length - 1));

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
            return NewCode;
        }

        public DataTable ExecuteQuery_DataTable(string mySelectQuery)
        {
            DataTable dataTable = new DataTable();
            SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlDataReader SDRResult;
            //Open Connenction
            myConnection.Open();
            SqlCommand myCommand = new SqlCommand(mySelectQuery, myConnection);
            myCommand.CommandTimeout = 0;
            //Create Command
            SDRResult = myCommand.ExecuteReader();
            for (int intIndex = 0; intIndex < SDRResult.FieldCount; intIndex++)
            {
                // create and add a column
                //dataTable.Columns.Add(Create_Column(SDRResult.GetName(intIndex), SDRResult.GetDataTypeName(intIndex)));
                dataTable.Columns.Add(SDRResult.GetName(intIndex), SDRResult.GetFieldType(intIndex));
            }
            while (SDRResult.Read())
            {
                // add a row
                DataRow row = dataTable.NewRow();
                for (int intIndex = 0; intIndex < SDRResult.FieldCount; intIndex++)
                {
                    row[SDRResult.GetName(intIndex)] = SDRResult[SDRResult.GetName(intIndex)];
                }
                dataTable.Rows.Add(row);


            }
            //Close Reader
            SDRResult.Close();
            //Close Connection
            myConnection.Close();
            //Return Result of Executed Query
            return dataTable;
        }


        public DataTable GetDistinctRecords(DataTable dt, string[] Columns)
        {
            DataTable dtUniqRecords = new DataTable();
            dtUniqRecords = dt.DefaultView.ToTable(true, Columns);
            return dtUniqRecords;
        }

        public List<string> ExecuteQuery_Array(string mySelectQuery)
        {
            List<string> IDs = new List<string>();
            DataTable dataTable = new DataTable();
            SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlDataReader SDRResult;
            //Open Connenction
            myConnection.Open();
            SqlCommand myCommand = new SqlCommand(mySelectQuery, myConnection);
            myCommand.CommandTimeout = 0;
            //Create Command
            SDRResult = myCommand.ExecuteReader();
            for (int intIndex = 0; intIndex < SDRResult.FieldCount; intIndex++)
            {
                // create and add a column
                //dataTable.Columns.Add(Create_Column(SDRResult.GetName(intIndex), SDRResult.GetDataTypeName(intIndex)));
                dataTable.Columns.Add(SDRResult.GetName(intIndex), SDRResult.GetFieldType(intIndex));
            }
            while (SDRResult.Read())
            {
                // add a row
                DataRow row = dataTable.NewRow();
                for (int intIndex = 0; intIndex < SDRResult.FieldCount; intIndex++)
                {
                    row[SDRResult.GetName(intIndex)] = SDRResult[SDRResult.GetName(intIndex)];
                }
                dataTable.Rows.Add(row);
                IDs.Add(row[0].ToString());

            }
            //Close Reader
            SDRResult.Close();
            //Close Connection
            myConnection.Close();
            //Return Result of Executed Query
            return IDs;
        }
        #endregion DB functions
    }
}