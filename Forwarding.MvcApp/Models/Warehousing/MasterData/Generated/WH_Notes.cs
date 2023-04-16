using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

/*This class is autogenerated by Khedrawy Code gen v.3.1*/
namespace Forwarding.MvcApp.Models.Warehousing.MasterData.Generated
{
	[Serializable]
	public class CPKWH_Notes
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
	public partial class CVarWH_Notes : CPKWH_Notes
	{
		#region "variables"
		internal Boolean mIsChanges = false;
		internal String mName;
		internal String mNotes;
		internal Boolean mIsForReleaseOrder;
		internal Boolean mIsForStoring;
		#endregion

		#region "Methods"
		public String Name
		{
			get { return mName; }
			set { mIsChanges = true ;mName = value; }
		}
		public String Notes
		{
			get { return mNotes; }
			set { mIsChanges = true ;mNotes = value; }
		}
		public Boolean IsForReleaseOrder
		{
			get { return mIsForReleaseOrder; }
			set { mIsChanges = true ;mIsForReleaseOrder = value; }
		}
		public Boolean IsForStoring
		{
			get { return mIsForStoring; }
			set { mIsChanges = true ;mIsForStoring = value; }
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

	public partial class CWH_Notes
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
		public List<CVarWH_Notes> lstCVarWH_Notes= new List<CVarWH_Notes>();
		public List<CPKWH_Notes> lstDeletedCPKWH_Notes = new List<CPKWH_Notes>();
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
			lstCVarWH_Notes.Clear();

			try
			{
				Con.Open();
				tr = Con.BeginTransaction(IsolationLevel.ReadCommitted);
				Com = new SqlCommand();
				Com.CommandType = CommandType.StoredProcedure;
				if (IsList == true)
				{
					Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
					Com.CommandText = "[dbo].GetListWH_Notes";
					Com.Parameters[0].Value = Param;
				}
				else
				{
					Com.CommandText = "[dbo].GetItemWH_Notes";
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
						CVarWH_Notes ObjCVarWH_Notes = new CVarWH_Notes();
						ObjCVarWH_Notes.ID = Convert.ToInt32(dr["ID"].ToString());
						ObjCVarWH_Notes.mName = Convert.ToString(dr["Name"].ToString());
						ObjCVarWH_Notes.mNotes = Convert.ToString(dr["Notes"].ToString());
						ObjCVarWH_Notes.mIsForReleaseOrder = Convert.ToBoolean(dr["IsForReleaseOrder"].ToString());
						ObjCVarWH_Notes.mIsForStoring = Convert.ToBoolean(dr["IsForStoring"].ToString());
						lstCVarWH_Notes.Add(ObjCVarWH_Notes);
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
			lstCVarWH_Notes.Clear();

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
				Com.CommandText = "[dbo].GetListPagingWH_Notes";
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
						CVarWH_Notes ObjCVarWH_Notes = new CVarWH_Notes();
						ObjCVarWH_Notes.ID = Convert.ToInt32(dr["ID"].ToString());
						ObjCVarWH_Notes.mName = Convert.ToString(dr["Name"].ToString());
						ObjCVarWH_Notes.mNotes = Convert.ToString(dr["Notes"].ToString());
						ObjCVarWH_Notes.mIsForReleaseOrder = Convert.ToBoolean(dr["IsForReleaseOrder"].ToString());
						ObjCVarWH_Notes.mIsForStoring = Convert.ToBoolean(dr["IsForStoring"].ToString());
						TotRecs = Convert.ToInt32(dr["TotalRecords"].ToString());
						lstCVarWH_Notes.Add(ObjCVarWH_Notes);
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
							Com.CommandText = "[dbo].DeleteListWH_Notes";
							else
							Com.CommandText = "[dbo].UpdateListWH_Notes";
								Com.Parameters.Add(new SqlParameter("@WhereClause", SqlDbType.NVarChar));
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
			public Exception DeleteItem(List<CPKWH_Notes> DeleteList)
				{

					Exception Exp = null;
					SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
					SqlCommand Com;
					try
						{
							Con.Open();
							Com = new SqlCommand();
							Com.CommandText = "[dbo].DeleteItemWH_Notes";
							Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
							foreach (CPKWH_Notes ObjCPKWH_Notes in DeleteList)
								{
									BeginTrans(Com,Con);
									 Com.Parameters[0].Value = Convert.ToInt32(ObjCPKWH_Notes.ID);
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
			public Exception SaveMethod(List<CVarWH_Notes> SaveList)
				{
					Exception Exp = null;
					SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
					SqlCommand Com;
					try
						{
							Con.Open();
							Com = new SqlCommand();
							Com.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar));
							Com.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar));
							Com.Parameters.Add(new SqlParameter("@IsForReleaseOrder", SqlDbType.Bit));
							Com.Parameters.Add(new SqlParameter("@IsForStoring", SqlDbType.Bit));
								SqlParameter paraID = Com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int , 0,ParameterDirection.Input,false,0,0,"ID",DataRowVersion.Default,null));
							foreach (CVarWH_Notes ObjCVarWH_Notes in SaveList)
								{
							if(ObjCVarWH_Notes.mIsChanges == true)
								{
									if(ObjCVarWH_Notes.ID == 0)
									{
										Com.CommandText = "[dbo].InsertItemWH_Notes";
								paraID.Direction = ParameterDirection.Output;
									}
								else if(ObjCVarWH_Notes.ID!=0)
									{
										Com.CommandText = "[dbo].UpdateItemWH_Notes";
								paraID.Direction = ParameterDirection.Input;
									}
										BeginTrans(Com,Con);
								if(ObjCVarWH_Notes.ID!=0)
									{
								Com.Parameters["@ID"].Value =ObjCVarWH_Notes.ID;
									}
								Com.Parameters["@Name"].Value =ObjCVarWH_Notes.Name;
								Com.Parameters["@Notes"].Value =ObjCVarWH_Notes.Notes;
								Com.Parameters["@IsForReleaseOrder"].Value =ObjCVarWH_Notes.IsForReleaseOrder;
								Com.Parameters["@IsForStoring"].Value =ObjCVarWH_Notes.IsForStoring;
										EndTrans(Com,Con);
								if(ObjCVarWH_Notes.ID==0)
										{
								ObjCVarWH_Notes.ID = Convert.ToInt32(Com.Parameters["@ID"].Value);
										}
									ObjCVarWH_Notes.mIsChanges = false;
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

