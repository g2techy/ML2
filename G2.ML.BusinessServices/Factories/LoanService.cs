using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using G2.ML.BusinessObjects;
using BO = G2.ML.BusinessObjects;

namespace G2.ML.BusinessServices.Factories
{
	public class LoanService : BaseService, Contracts.ILoanService
	{

		#region ILoanService Members

		public int Add(LoanAddBO loanAdd)
		{
			int _ID;
			try
			{
				DatabaseAccess.OpenConnection(true);
				var _ourParams = DatabaseAccess.ExecuteProcedureDML("P_Loan_AddUpdate", new List<DAL.DatabaseParameter>()
				{
					new DAL.DatabaseParameter("@ClientID", DAL.ParameterDirection.In, DAL.DataType.Int, loanAdd.ClientID),
					new DAL.DatabaseParameter("@LoanID", DAL.ParameterDirection.InOut, DAL.DataType.Int, loanAdd.LoanID),
					new DAL.DatabaseParameter("@StartDate", DAL.ParameterDirection.In, DAL.DataType.Date, loanAdd.StartDate),
					new DAL.DatabaseParameter("@EndDate", DAL.ParameterDirection.In, DAL.DataType.Date, loanAdd.EndDate),
					new DAL.DatabaseParameter("@BorrowerID", DAL.ParameterDirection.In, DAL.DataType.Int, loanAdd.BorrowerID),
					new DAL.DatabaseParameter("@PrincipalAmount", DAL.ParameterDirection.In, DAL.DataType.Decimal, loanAdd.PrincipalAmount),
					new DAL.DatabaseParameter("@MonthlyInterest", DAL.ParameterDirection.In, DAL.DataType.Decimal, loanAdd.MonthlyInterest),
					new DAL.DatabaseParameter("@Comments", DAL.ParameterDirection.In, DAL.DataType.String, loanAdd.Comments, 1000)
				});
				DatabaseAccess.CommitTransaction();
				_ID = Convert.ToInt32(_ourParams["@LoanID"]);
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

		public int Delete(int clientID, int loanID)
		{
			int _ID;
			try
			{
				DatabaseAccess.OpenConnection(true);
				var _ourParams = DatabaseAccess.ExecuteProcedureDML("P_Loan_Delete", new List<DAL.DatabaseParameter>()
				{
					new DAL.DatabaseParameter("@LoanID",DAL.ParameterDirection.In, DAL.DataType.Int, loanID),
					new DAL.DatabaseParameter("@ClientID",DAL.ParameterDirection.In,DAL.DataType.Int, clientID)
				});
				DatabaseAccess.CommitTransaction();
				_ID = loanID;
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

		public List<Buyer> GetBorrowerList(int clientID, int buyerTypeID)
		{
			List<BO.Buyer> _borrowerList = new List<BO.Buyer>();

			try
			{
				DatabaseAccess.OpenConnection();
				using (DataSet _ds = DatabaseAccess.ExecuteProcedure("P_Sale_GetBuyerList", new List<DAL.DatabaseParameter>()
				{
					new DAL.DatabaseParameter("@BuyerTypeID",DAL.ParameterDirection.In, DAL.DataType.Int, buyerTypeID),
					new DAL.DatabaseParameter("@ClientID",DAL.ParameterDirection.In,DAL.DataType.Int, clientID)
				}))
				{
					if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
					{
						foreach (DataRow _row in _ds.Tables[0].Rows)
						{
							_borrowerList.Add(new BO.Buyer()
							{
								BuyerID = Convert.ToInt32(_row["BuyerID"]),
								BuyerName = Convert.ToString(_row["BuyerName"])
							});
						}
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

			return _borrowerList;
		}

		public LoanAddBO GetLoanDetails(int clientID, int loanID)
		{
			BO.LoanAddBO _loanAddBO = null;
			try
			{
				DatabaseAccess.OpenConnection();
				using (DataSet _ds = DatabaseAccess.ExecuteProcedure("P_Loan_GetDetails", new List<DAL.DatabaseParameter>()
				{
					new DAL.DatabaseParameter("@LoanID",DAL.ParameterDirection.In, DAL.DataType.Int, loanID),
					new DAL.DatabaseParameter("@ClientID",DAL.ParameterDirection.In,DAL.DataType.Int, clientID)
				}))
				{
					if (_ds != null && _ds.Tables.Count > 0)
					{
						var _row = _ds.Tables[0].Rows[0];
						_loanAddBO = new BO.LoanAddBO()
						{
							StartDate = Convert.ToDateTime(_row["StartDate"]).ToString("yyyy-MM-dd"),
							BorrowerID = Convert.ToInt32(_row["BorrowerID"]),
							PrincipalAmount = float.Parse(_row["PrincipalAmount"].ToString()),
							MonthlyInterest = float.Parse(_row["MonthlyInterest"].ToString()),
							ClientID = int.Parse(_row["UserID"].ToString()),
							Status = Convert.ToInt32(_row["Status"])
						};
						if (_row["EndDate"] != System.DBNull.Value)
						{
							_loanAddBO.EndDate = Convert.ToDateTime(_row["EndDate"]).ToString("yyyy-MM-dd");
						}
						if (_row["Comments"] != System.DBNull.Value)
						{
							_loanAddBO.Comments = _row["Comments"].ToString();
						}
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

			return _loanAddBO;
		}

		public LoanSearchResultBO GetLoanList(LoanSearchBO loanSearch)
		{
			BO.LoanSearchResultBO _returnVal = null;
			try
			{
				DatabaseAccess.OpenConnection();
				var _paramList = new List<DAL.DatabaseParameter>()
				{
					new DAL.DatabaseParameter("@ClientID",DAL.ParameterDirection.In, DAL.DataType.Int, loanSearch.ClientID),
					new DAL.DatabaseParameter("@StartIndex",DAL.ParameterDirection.In, DAL.DataType.Int, loanSearch.StartIndex),
					new DAL.DatabaseParameter("@PageSize",DAL.ParameterDirection.In, DAL.DataType.Int, loanSearch.PageSize)
				};
				string _stDate = GetDateIntoString(loanSearch.StartDate);
				if (!string.IsNullOrEmpty(loanSearch.StartDate))
				{
					_paramList.Add(new DAL.DatabaseParameter("@StartDate", DAL.ParameterDirection.In, DAL.DataType.String, _stDate));
				}
				string _endDate = GetDateIntoString(loanSearch.EndDate);
				if (!string.IsNullOrEmpty(loanSearch.EndDate))
				{
					_paramList.Add(new DAL.DatabaseParameter("@EndDate", DAL.ParameterDirection.In, DAL.DataType.String, _endDate));
				}
				if (loanSearch.BorrowerID > 0)
				{
					_paramList.Add(new DAL.DatabaseParameter("@BorrowerID", DAL.ParameterDirection.In, DAL.DataType.Int, loanSearch.BorrowerID));
				}
				if (!string.IsNullOrEmpty(loanSearch.RefNo))
				{
					_paramList.Add(new DAL.DatabaseParameter("@RefNo", DAL.ParameterDirection.In, DAL.DataType.String, loanSearch.RefNo));
				}

				DataSet _ds = DatabaseAccess.ExecuteProcedure("P_Loan_GetLoanList", _paramList);
				int _totalRecords = 0;
				if (_ds != null && _ds.Tables.Count > 0)
				{
					if (_ds.Tables[0].Rows.Count > 0)
					{
						_totalRecords = int.Parse(_ds.Tables[0].Rows[0]["Row_Count"].ToString());
					}

					_returnVal = new BO.LoanSearchResultBO(_totalRecords);

					foreach (DataRow _dr in _ds.Tables[0].Rows)
					{
						var _loanBM = new BO.LoanBO()
						{
							LoanID = int.Parse(_dr["LoanID"].ToString()),
							StartDate = DateTime.Parse(_dr["StartDate"].ToString()),
							Borrower = _dr["Borrower"].ToString(),
							PrincipalAmount = float.Parse(_dr["PrincipalAmount"].ToString()),
							MonthlyInterest = float.Parse(_dr["MonthlyInterest"].ToString()),
							StatusID = int.Parse(_dr["StatusID"].ToString()),
							StatusName = _dr["StatusName"].ToString(),
							RefNo = _dr["RefNo"].ToString()
						};
						if (_dr["EndDate"] != System.DBNull.Value)
						{
							_loanBM.EndDate = DateTime.Parse(_dr["EndDate"].ToString());
						}
						if (_dr["TotalPayAmount"] != System.DBNull.Value)
						{
							_loanBM.TotalPayAmount = float.Parse(_dr["TotalPayAmount"].ToString());
						}
						if (_dr["PayDate"] != System.DBNull.Value)
						{
							_loanBM.PayDate = DateTime.Parse(_dr["PayDate"].ToString());
						}
						_returnVal.LoanList.Add(_loanBM);
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

			return _returnVal;
		}

		public int AddPayment(LoanPaymentBO payment)
		{
			int _ID;
			try
			{
				DatabaseAccess.OpenConnection(true);
				var _ourParams = DatabaseAccess.ExecuteProcedureDML("P_Loan_AddPayment", new List<DAL.DatabaseParameter>()
				{
					new DAL.DatabaseParameter("@LoanPayID", DAL.ParameterDirection.InOut, DAL.DataType.Int, payment.LoanPayID),
					new DAL.DatabaseParameter("@LoanID", DAL.ParameterDirection.In, DAL.DataType.Int, payment.LoanID),
					new DAL.DatabaseParameter("@PayDate", DAL.ParameterDirection.In, DAL.DataType.DateTime, payment.PayDate),
					new DAL.DatabaseParameter("@PayAmount", DAL.ParameterDirection.In, DAL.DataType.Decimal, payment.PayAmount),
					new DAL.DatabaseParameter("@PayType", DAL.ParameterDirection.In, DAL.DataType.Int, payment.PayType),
					new DAL.DatabaseParameter("@PayComments", DAL.ParameterDirection.In, DAL.DataType.String, payment.PayComments, 1000)
				});
				DatabaseAccess.CommitTransaction();
				_ID = Convert.ToInt32(_ourParams["@LoanPayID"]);
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

		public int DeletePayment(int loanPayID)
		{
			int _ID;
			try
			{
				DatabaseAccess.OpenConnection(true);
				var _ourParams = DatabaseAccess.ExecuteProcedureDML("P_Loan_DeletePayment", new List<DAL.DatabaseParameter>()
				{
					new DAL.DatabaseParameter("@LoanPayID",DAL.ParameterDirection.In, DAL.DataType.Int, loanPayID),
					new DAL.DatabaseParameter("@LoanID",DAL.ParameterDirection.Out, DAL.DataType.Int)
				});
				DatabaseAccess.CommitTransaction();
				_ID = Convert.ToInt32(_ourParams["@LoanID"]);
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

		public List<LoanPaymentBO> GetPaymentList(int clientID, int loanID)
		{
			List<BO.LoanPaymentBO> _returnVal = null;

			try
			{
				DatabaseAccess.OpenConnection();
				var _paramList = new List<DAL.DatabaseParameter>()
				{
					new DAL.DatabaseParameter("@ClientID",DAL.ParameterDirection.In, DAL.DataType.Int, clientID),
					new DAL.DatabaseParameter("@LoanID",DAL.ParameterDirection.In, DAL.DataType.Int, loanID),
				};

				DataSet _ds = DatabaseAccess.ExecuteProcedure("P_Loan_GetPaymentList", _paramList);
				if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
				{
					_returnVal = new List<BO.LoanPaymentBO>();
					foreach (DataRow _dr in _ds.Tables[0].Rows)
					{
						_returnVal.Add(new BO.LoanPaymentBO()
						{
							LoanPayID = int.Parse(_dr["LoanPayID"].ToString()),
							PayDate = DateTime.Parse(_dr["PayDate"].ToString()),
							PayAmount = float.Parse(_dr["PayAmount"].ToString()),
							PayComments = _dr["PayComments"].ToString(),
							PayType =  int.Parse(_dr["PayType"].ToString()),
							PayTypeName = _dr["PayTypeName"].ToString()
						});
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
			return _returnVal;
		}

		public int Close(int loanPayID)
		{
			int _ID;
			try
			{
				DatabaseAccess.OpenConnection(true);
				var _ourParams = DatabaseAccess.ExecuteProcedureDML("P_Loan_UpdateStatus", new List<DAL.DatabaseParameter>()
				{
					new DAL.DatabaseParameter("@LoanID",DAL.ParameterDirection.In, DAL.DataType.Int, loanPayID),
					new DAL.DatabaseParameter("@Status",DAL.ParameterDirection.In, DAL.DataType.Int, 4)
				});
				DatabaseAccess.CommitTransaction();
				_ID = loanPayID;
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

		public List<BO.LoanCalcInterestBO> GetCalcInterest(int clientID, int loanID)
		{
			List<BO.LoanCalcInterestBO> _returnVal = null;

			try
			{
				DatabaseAccess.OpenConnection();
				var _paramList = new List<DAL.DatabaseParameter>()
				{
					new DAL.DatabaseParameter("@ClientID",DAL.ParameterDirection.In, DAL.DataType.Int, clientID),
					new DAL.DatabaseParameter("@LoanID",DAL.ParameterDirection.In, DAL.DataType.Int, loanID),
				};

				DataSet _ds = DatabaseAccess.ExecuteProcedure("P_Loan_GetCalcInterest", _paramList);
				if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
				{
					_returnVal = new List<BO.LoanCalcInterestBO>();
					foreach (DataRow _dr in _ds.Tables[0].Rows)
					{
						var _bo = new BO.LoanCalcInterestBO()
						{
							DailyRate = float.Parse(_dr["DailyRate"].ToString()),
							IntForDays = int.Parse(_dr["IntForDays"].ToString()),
							IntOnAmount = float.Parse(_dr["IntOnAmount"].ToString()),
							CalcIntAmount = float.Parse(_dr["CalcIntAmount"].ToString()),
							TotalIntPaid = float.Parse(_dr["TotalIntPaid"].ToString())
						};
						if (_dr["PayAmount"] != DBNull.Value)
						{
							_bo.PayAmount = float.Parse(_dr["PayAmount"].ToString());
						}
						if (_dr["PayDate"] != DBNull.Value)
						{
							_bo.PayDate = DateTime.Parse(_dr["PayDate"].ToString());
						}
						_returnVal.Add(_bo);
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
			return _returnVal;
		}

		#endregion
	}

}
