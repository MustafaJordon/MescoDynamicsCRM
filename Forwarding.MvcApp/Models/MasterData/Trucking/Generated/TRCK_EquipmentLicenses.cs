using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace Forwarding.MvcApp.Models.MasterData.Trucking.Generated
{
	[Serializable]
	public class CPKTRCK_EquipmentLicenses
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
	public partial class CVarTRCK_EquipmentLicenses : CPKTRCK_EquipmentLicenses
	{
		#region "variables"
		internal Boolean mIsChanges = false;
		internal String mLicenseNumber;
		internal DateTime mLicenseNumberExpireDate;
		internal Int32 mEquipmentID;
		internal Int32 mCreatorUserID;
		internal DateTime mCreationDate;
		internal Int32 mModificatorUserID;
		internal DateTime mModificationDate;
		#endregion

		#region "Methods"
		public String LicenseNumber
		{
			get { return mLicenseNumber; }
			set { mIsChanges = true ;mLicenseNumber = value; }
		}
		public DateTime LicenseNumberExpireDate
		{
			get { return mLicenseNumberExpireDate; }
			set { mIsChanges = true ;mLicenseNumberExpireDate = value; }
		}
		public Int32 EquipmentID
		{
			get { return mEquipmentID; }
			set { mIsChanges = true ;mEquipmentID = value; }
		}
		public Int32 CreatorUserID
		{
			get { return mCreatorUserID; }
			set { mIsChanges = true ;mCreatorUserID = value; }
		}
		public DateTime CreationDate
		{
			get { return mCreationDate; }
			set { mIsChanges = true ;mCreationDate = value; }
		}
		public Int32 ModificatorUserID
		{
			get { return mModificatorUserID; }
			set { mIsChanges = true ;mModificatorUserID = value; }
		}
		public DateTime ModificationDate
		{
			get { return mModificationDate; }
			set { mIsChanges = true ;mModificationDate = value; }
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

	public partial class CTRCK_EquipmentLicenses
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
		public List<CVarTRCK_EquipmentLicenses> lstCVarTRCK_EquipmentLicenses= new List<CVarTRCK_EquipmentLicenses>();
		public List<CPKTRCK_EquipmentLicenses> lstDeletedCPKTRCK_EquipmentLicenses = new List<CPKTRCK_EquipmentLicenses>();
		#endregion

		#region "Select Methods"
		public Exception GetList(string WhereClause)
		{
			return DataFill(WhereClause,true);
		}
		public Exception GetListPaging(Int32 PageSize, Int32 PageNumber, string WhereClause, string OrderBy, out Int32 TotalRecords)
		{
			return DataFill(PageSize, PageNumber, WhereClause, OrderBy, out TotalRecords);
		}
		public Exception GetItem(Int32 ID)
		{
			return DataFill(Convert.ToString(ID), false);
		}
		private Exception DataFill(string Param , Boolean IsList)
		{
			Exception Exp = null;
			SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
			SqlCommand Com;
			SqlDataReader dr;
			lstCVarTRCK_EquipmentLicenses.Clear();

			try
			{
				Con.Open();
				tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
				Com = new SqlCommand();
				Com.CommandType = CommandType.StoredProcedure;
				if (IsList == true)
				{
					Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
					Com.CommandText = "[dbo].GetListTRCK_EquipmentLicenses";
					Com.Parameters[0].Value = Param;
				}
				else
				{
					Com.CommandText = "[dbo].GetItemTRCK_EquipmentLicenses";
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
						CVarTRCK_EquipmentLicenses ObjCVarTRCK_EquipmentLicenses = new CVarTRCK_EquipmentLicenses();
						ObjCVarTRCK_EquipmentLicenses.ID = Convert.ToInt32(dr["ID"].ToString());
						ObjCVarTRCK_EquipmentLicenses.mLicenseNumber = Convert.ToString(dr["LicenseNumber"].ToString());
						ObjCVarTRCK_EquipmentLicenses.mLicenseNumberExpireDate = Convert.ToDateTime(dr["LicenseNumberExpireDate"].ToString());
						ObjCVarTRCK_EquipmentLicenses.mEquipmentID = Convert.ToInt32(dr["EquipmentID"].ToString());
						ObjCVarTRCK_EquipmentLicenses.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
						ObjCVarTRCK_EquipmentLicenses.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
						ObjCVarTRCK_EquipmentLicenses.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
						ObjCVarTRCK_EquipmentLicenses.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
						lstCVarTRCK_EquipmentLicenses.Add(ObjCVarTRCK_EquipmentLicenses);
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
			catch ( Exception ex)
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
			lstCVarTRCK_EquipmentLicenses.Clear();

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
				Com.CommandText = "[dbo].GetListPagingTRCK_EquipmentLicenses";
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
						CVarTRCK_EquipmentLicenses ObjCVarTRCK_EquipmentLicenses = new CVarTRCK_EquipmentLicenses();
						ObjCVarTRCK_EquipmentLicenses.ID = Convert.ToInt32(dr["ID"].ToString());
						ObjCVarTRCK_EquipmentLicenses.mLicenseNumber = Convert.ToString(dr["LicenseNumber"].ToString());
						ObjCVarTRCK_EquipmentLicenses.mLicenseNumberExpireDate = Convert.ToDateTime(dr["LicenseNumberExpireDate"].ToString());
						ObjCVarTRCK_EquipmentLicenses.mEquipmentID = Convert.ToInt32(dr["EquipmentID"].ToString());
						ObjCVarTRCK_EquipmentLicenses.mCreatorUserID = Convert.ToInt32(dr["CreatorUserID"].ToString());
						ObjCVarTRCK_EquipmentLicenses.mCreationDate = Convert.ToDateTime(dr["CreationDate"].ToString());
						ObjCVarTRCK_EquipmentLicenses.mModificatorUserID = Convert.ToInt32(dr["ModificatorUserID"].ToString());
						ObjCVarTRCK_EquipmentLicenses.mModificationDate = Convert.ToDateTime(dr["ModificationDate"].ToString());
						TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
						lstCVarTRCK_EquipmentLicenses.Add(ObjCVarTRCK_EquipmentLicenses);
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
			catch ( Exception ex)
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
			private void BeginTrans(SqlCommand Com,SqlConnection Con)
				{

					tr = Con.BeginTransaction(IsolationLevel.Serializable);
					Com.CommandType = CommandType.StoredProcedure;
				}

			private void EndTrans(SqlCommand Com,SqlConnection Con)
				{

					Com.Transaction = tr;
					Com.Connection = Con;
					Com.ExecuteNonQuery();
					tr.Commit();
				}

			#endregion
			#region "Set List Method"
			private Exception SetList(string WhereClause,Boolean IsDelete)
				{

					Exception Exp = null;
					SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
					SqlCommand Com;
					try
						{
							Con.Open();
							Com = new SqlCommand();
							if(IsDelete == true)
							Com.CommandText = "[dbo].DeleteListTRCK_EquipmentLicenses";
							else
							Com.CommandText = "[dbo].UpdateListTRCK_EquipmentLicenses";
								Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.VarChar));
									BeginTrans(Com,Con);
								Com.Parameters[0].Value = WhereClause;
									EndTrans(Com,Con);
						}
					catch ( Exception ex)
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
			public Exception DeleteItem(List<CPKTRCK_EquipmentLicenses> DeleteList)
				{

					Exception Exp = null;
					SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
					SqlCommand Com;
					try
						{
							Con.Open();
							Com = new SqlCommand();
							Com.CommandText = "[dbo].DeleteItemTRCK_EquipmentLicenses";
							Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
							foreach (CPKTRCK_EquipmentLicenses ObjCPKTRCK_EquipmentLicenses in DeleteList)
								{
									BeginTrans(Com,Con);
									 Com.Parameters[0].Value = Convert.ToInt32(ObjCPKTRCK_EquipmentLicenses.ID);
									EndTrans(Com,Con);
								}
						}
					catch ( Exception ex)
						{
							Exp =ex;
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

					return SetList(WhereClause,true);
				}

			#endregion
			#region "Save Methods"
			public Exception SaveMethod(List<CVarTRCK_EquipmentLicenses> SaveList)
				{
					Exception Exp = null;
					SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
					SqlCommand Com;
					try
						{
							Con.Open();
							Com = new SqlCommand();
							Com.Parameters.Add(new SqlParameter("@LicenseNumber", SqlDbType.NVarChar));
							Com.Parameters.Add(new SqlParameter("@LicenseNumberExpireDate", SqlDbType.DateTime));
							Com.Parameters.Add(new SqlParameter("@EquipmentID", SqlDbType.Int));
							Com.Parameters.Add(new SqlParameter("@CreatorUserID", SqlDbType.Int));
							Com.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime));
							Com.Parameters.Add(new SqlParameter("@ModificatorUserID", SqlDbType.Int));
							Com.Parameters.Add(new SqlParameter("@ModificationDate", SqlDbType.DateTime));
								SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int , 0,ParameterDirection.Input,false,0,0,"ID",DataRowVersion.Default,null));
							foreach (CVarTRCK_EquipmentLicenses ObjCVarTRCK_EquipmentLicenses in SaveList)
								{
							if(ObjCVarTRCK_EquipmentLicenses.mIsChanges == true)
								{
									if(ObjCVarTRCK_EquipmentLicenses.ID == 0)
									{
										Com.CommandText = "[dbo].InsertItemTRCK_EquipmentLicenses";
								paraID.Direction = ParameterDirection.Output;
									}
								else if(ObjCVarTRCK_EquipmentLicenses.ID!=0)
									{
										Com.CommandText = "[dbo].UpdateItemTRCK_EquipmentLicenses";
								paraID.Direction = ParameterDirection.Input;
									}
										BeginTrans(Com,Con);
								if(ObjCVarTRCK_EquipmentLicenses.ID!=0)
									{
								Com.Parameters["@ID"].Value =ObjCVarTRCK_EquipmentLicenses.ID;
									}
								Com.Parameters["@LicenseNumber"].Value =ObjCVarTRCK_EquipmentLicenses.LicenseNumber;
								Com.Parameters["@LicenseNumberExpireDate"].Value =ObjCVarTRCK_EquipmentLicenses.LicenseNumberExpireDate;
								Com.Parameters["@EquipmentID"].Value =ObjCVarTRCK_EquipmentLicenses.EquipmentID;
								Com.Parameters["@CreatorUserID"].Value =ObjCVarTRCK_EquipmentLicenses.CreatorUserID;
								Com.Parameters["@CreationDate"].Value =ObjCVarTRCK_EquipmentLicenses.CreationDate;
								Com.Parameters["@ModificatorUserID"].Value =ObjCVarTRCK_EquipmentLicenses.ModificatorUserID;
								Com.Parameters["@ModificationDate"].Value =ObjCVarTRCK_EquipmentLicenses.ModificationDate;
										EndTrans(Com,Con);
								if(ObjCVarTRCK_EquipmentLicenses.ID==0)
										{
								ObjCVarTRCK_EquipmentLicenses.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
										}
									ObjCVarTRCK_EquipmentLicenses.mIsChanges = false;
									}
								}
						}
					catch ( Exception ex)
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

					return SetList(UpdateClause,false);
				}

			#endregion
		}
}

