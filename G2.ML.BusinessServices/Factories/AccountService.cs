using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BO = G2.ML.BusinessObjects;

namespace G2.ML.BusinessServices.Factories
{
	public class AccountService : BaseService, Contracts.IAccountService
	{
		#region IAccountService Members

		public int RegisterUser(BO.RegisterBO bo)
		{
			int _ID;			
			try
			{
				DatabaseAccess.OpenConnection(true);
				var _ourParams = DatabaseAccess.ExecuteProcedureDML("P_Account_AddUser", new List<DAL.DatabaseParameter>()
				{
					new DAL.DatabaseParameter("@UserID",DAL.ParameterDirection.Out, DAL.DataType.Int),
					new DAL.DatabaseParameter("@UserName",DAL.ParameterDirection.In,DAL.DataType.String, bo.UserName, 100),
					new DAL.DatabaseParameter("@Password",DAL.ParameterDirection.In,DAL.DataType.String, Encrypt(bo.Password), 200),
					new DAL.DatabaseParameter("@FirstName",DAL.ParameterDirection.In,DAL.DataType.String, bo.FirstName, 100),
					new DAL.DatabaseParameter("@LastName",DAL.ParameterDirection.In,DAL.DataType.String, bo.LastName, 100),
					new DAL.DatabaseParameter("@CompanyName",DAL.ParameterDirection.In,DAL.DataType.String, bo.CompanyName, 200),
					new DAL.DatabaseParameter("@CompanyAddress",DAL.ParameterDirection.In,DAL.DataType.String, bo.CompanyAddress, 1000),
					new DAL.DatabaseParameter("@PhoneNo",DAL.ParameterDirection.In,DAL.DataType.String, bo.PhoneNo),
					new DAL.DatabaseParameter("@MobileNo",DAL.ParameterDirection.In,DAL.DataType.String, bo.PhoneNo)
				});
				DatabaseAccess.CommitTransaction();
				_ID = Convert.ToInt32(_ourParams["@UserID"]);
				_ourParams = null;
			}
			catch
			{
				DatabaseAccess.RollbackTransaction();
				throw;
			}
			finally
			{
				DatabaseAccess.CloseConnection();
			}
			return _ID;
		}

		public BO.UserLoginResultBO VerifyLoginCreds(BO.LoginBO loginBO)
		{
			BO.UserLoginResultBO _userBM = null;

			try
			{
				DatabaseAccess.OpenConnection();
				using (DataSet _ds = DatabaseAccess.ExecuteProcedure("P_Account_VerifyLoginCreds", new List<DAL.DatabaseParameter>()
				{
					new DAL.DatabaseParameter("@UserName",DAL.ParameterDirection.In, DAL.DataType.String, loginBO.UserName),
					new DAL.DatabaseParameter("@Password",DAL.ParameterDirection.In, DAL.DataType.String, Encrypt(loginBO.Password))
				}))
				{
					if (_ds != null && _ds.Tables.Count > 0)
					{
						var _row = _ds.Tables[0].Rows[0];
						_userBM = new BO.UserLoginResultBO()
						{
							UserID = Convert.ToInt32(_row["UserID"]),
							ClientID = Convert.ToInt32(_row["UserID"]),
							UserName = Convert.ToString(_row["UserName"]),
							FirstName = Convert.ToString(_row["FirstName"]),
							LastName = Convert.ToString(_row["LastName"])
						};
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				DatabaseAccess.CloseConnection();
			}

			return _userBM;
		}

		public bool ChangePassword(BO.ChangePwdBO changePwd)
		{
			bool _isSuccess = false;

			try
			{
				DatabaseAccess.OpenConnection(true);
				var _ourParams = DatabaseAccess.ExecuteProcedureDML("P_Account_ChangePwd", new List<DAL.DatabaseParameter>()
				{
					new DAL.DatabaseParameter("@UserID", DAL.ParameterDirection.In, DAL.DataType.Int, changePwd.UserID),
					new DAL.DatabaseParameter("@OldPassword", DAL.ParameterDirection.In, DAL.DataType.String, Encrypt(changePwd.OldPassword), 200),
					new DAL.DatabaseParameter("@NewPassword", DAL.ParameterDirection.In, DAL.DataType.String, Encrypt(changePwd.NewPassword), 200)
				});
				DatabaseAccess.CommitTransaction();
				_ourParams = null;
				_isSuccess = true;
			}
			catch
			{
				throw;
			}
			finally
			{
				DatabaseAccess.CloseConnection();
			}

			return _isSuccess;
		}

		#endregion

		private string Encrypt(string password)
		{
			return Frameworks.Core.Encryption.Encrypt(password);
		}
		private string Decrypt(string password)
		{
			return Frameworks.Core.Encryption.Decrypt(password);
		}

	}
}

