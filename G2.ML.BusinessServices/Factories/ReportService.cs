using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BO = G2.ML.BusinessObjects;

namespace G2.ML.BusinessServices.Factories
{
	public class ReportService : BaseService, Contracts.IReportService
	{
		#region IReportRepository Members

		public List<BO.SaleStatusBO> GetSaleStatusList()
		{
			List<BO.SaleStatusBO> _statusList = new List<BO.SaleStatusBO>();

			try
			{
				DatabaseAccess.OpenConnection();
				using (DataTable _dt = DatabaseAccess.ExecuteQuery(@"Select * From SalesStatusDetails", null))
				{
					if (_dt != null && _dt.Rows.Count > 0)
					{
						foreach (DataRow _dr in _dt.Rows)
						{
							_statusList.Add(new BO.SaleStatusBO()
							{
								SaleStatusID = int.Parse(_dr["SaleStatusID"].ToString()),
								SaleStatusValue = _dr["SaleStatusValue"].ToString()
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

			return _statusList;
		}

		public DataTable GetSalesReport(BO.SalesReportBO bm)
		{
			DataTable _returnVal = null;

			try
			{
				DatabaseAccess.OpenConnection();

				var _paramList = new List<DAL.DatabaseParameter>()
				{
					new DAL.DatabaseParameter("@ClientID",DAL.ParameterDirection.In, DAL.DataType.Int, bm.ClientID)
				};
				string _stDate = GetDateIntoString(bm.StartDate);
				if (!string.IsNullOrEmpty(bm.StartDate))
				{
					_paramList.Add(new DAL.DatabaseParameter("@StartDate", DAL.ParameterDirection.In, DAL.DataType.String, _stDate));
				}
				string _endDate = GetDateIntoString(bm.EndDate);
				if (!string.IsNullOrEmpty(bm.EndDate))
				{
					_paramList.Add(new DAL.DatabaseParameter("@EndDate", DAL.ParameterDirection.In, DAL.DataType.String, _endDate));
				}
				if (bm.SallerID.HasValue)
				{
					_paramList.Add(new DAL.DatabaseParameter("@SallerID", DAL.ParameterDirection.In, DAL.DataType.Int, bm.SallerID));
				}
				if (bm.BuyerID.HasValue)
				{
					_paramList.Add(new DAL.DatabaseParameter("@BuyerID", DAL.ParameterDirection.In, DAL.DataType.Int, bm.BuyerID));
				}
				if (bm.Status.HasValue)
				{
					_paramList.Add(new DAL.DatabaseParameter("@Status", DAL.ParameterDirection.In, DAL.DataType.String, bm.Status));
				}
				if (bm.DueDays.HasValue)
				{
					_paramList.Add(new DAL.DatabaseParameter("@DueDays", DAL.ParameterDirection.In, DAL.DataType.Int, bm.DueDays));
				}

				DataSet _ds = DatabaseAccess.ExecuteProcedure("P_Report_GetSalesList", _paramList);
				if (_ds != null && _ds.Tables.Count > 0)
				{
					_returnVal = _ds.Tables[0];
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

		public DataTable GetBrokerageReport(BO.BrokerageReportBO bm)
		{
			DataTable _returnVal = null;

			try
			{
				DatabaseAccess.OpenConnection();
				var _paramList = new List<DAL.DatabaseParameter>()
				{
					new DAL.DatabaseParameter("@ClientID",DAL.ParameterDirection.In, DAL.DataType.Int, bm.ClientID)
				};
				string _stDate = GetDateIntoString(bm.StartDate);
				if (!string.IsNullOrEmpty(bm.StartDate))
				{
					_paramList.Add(new DAL.DatabaseParameter("@StartDate", DAL.ParameterDirection.In, DAL.DataType.String, _stDate));
				}
				string _endDate = GetDateIntoString(bm.EndDate);
				if (!string.IsNullOrEmpty(bm.EndDate))
				{
					_paramList.Add(new DAL.DatabaseParameter("@EndDate", DAL.ParameterDirection.In, DAL.DataType.String, _endDate));
				}
				if (bm.SallerID.HasValue)
				{
					_paramList.Add(new DAL.DatabaseParameter("@SallerID", DAL.ParameterDirection.In, DAL.DataType.Int, bm.SallerID));
				}
				if (bm.BuyerID.HasValue)
				{
					_paramList.Add(new DAL.DatabaseParameter("@BuyerID", DAL.ParameterDirection.In, DAL.DataType.Int, bm.BuyerID));
				}
				if (bm.Status.HasValue)
				{
					_paramList.Add(new DAL.DatabaseParameter("@Status", DAL.ParameterDirection.In, DAL.DataType.String, bm.Status));
				}

				DataSet _ds = DatabaseAccess.ExecuteProcedure("P_Report_GetBrokerageList", _paramList);
				if (_ds != null && _ds.Tables.Count > 0)
				{
					_returnVal = _ds.Tables[0];
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

		public DataTable GetLoanReport(BO.LoanReportBO bo)
		{
			DataTable _returnVal = null;

			try
			{
				DatabaseAccess.OpenConnection();
				var _paramList = new List<DAL.DatabaseParameter>()
				{
					new DAL.DatabaseParameter("@ClientID",DAL.ParameterDirection.In, DAL.DataType.Int, bo.ClientID)
				};
				string _stDate = GetDateIntoString(bo.StartDate);
				if (!string.IsNullOrEmpty(bo.StartDate))
				{
					_paramList.Add(new DAL.DatabaseParameter("@StartDate", DAL.ParameterDirection.In, DAL.DataType.String, _stDate));
				}
				string _endDate = GetDateIntoString(bo.EndDate);
				if (!string.IsNullOrEmpty(bo.EndDate))
				{
					_paramList.Add(new DAL.DatabaseParameter("@EndDate", DAL.ParameterDirection.In, DAL.DataType.String, _endDate));
				}
				if (bo.BorrowerID.HasValue)
				{
					_paramList.Add(new DAL.DatabaseParameter("@BorrowerID", DAL.ParameterDirection.In, DAL.DataType.Int, bo.BorrowerID));
				}
				if (bo.Status.HasValue)
				{
					_paramList.Add(new DAL.DatabaseParameter("@Status", DAL.ParameterDirection.In, DAL.DataType.String, bo.Status));
				}

				DataSet _ds = DatabaseAccess.ExecuteProcedure("P_Report_GetLoanList", _paramList);
				if (_ds != null && _ds.Tables.Count > 0)
				{
					_returnVal = _ds.Tables[0];
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
