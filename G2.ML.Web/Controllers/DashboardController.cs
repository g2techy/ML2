﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BO = G2.ML.BusinessObjects;
using BS = G2.ML.BusinessServices;

namespace G2.ML.Web.Controllers
{
	public class DashboardController : Infrastructure.Core.BaseController
	{

		#region DI settings

		private readonly BS.Contracts.IDashboardService _dashboardService;
		public DashboardController(BS.Contracts.IDashboardService dashboardService)
		{
			_dashboardService = dashboardService;
		}

		#endregion

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult ChartData(int chartType)
		{
			object _jsonData = null;
			try
			{
				var _chartList = (Infrastructure.Constants.Dashboard.ChartList)chartType;
				switch (_chartList)
				{
					case Infrastructure.Constants.Dashboard.ChartList.Last12MonthSales:
						_jsonData = Last12MonthsSaleChartData();
						break;
					case Infrastructure.Constants.Dashboard.ChartList.Last12MonthBrokerage:
						_jsonData = Last12MonthsBrokerageChartData();
						break;
					case Infrastructure.Constants.Dashboard.ChartList.BrokerageDistribution:
						_jsonData = BrokerageDistributionChartData();
						break;
					case Infrastructure.Constants.Dashboard.ChartList.Last12InterestPaid:
						_jsonData = Last12LoanInterestPaidChartData();
						break;
					case Infrastructure.Constants.Dashboard.ChartList.Last24LoanData:
						_jsonData = Last24MonthsLoanChartData();
						break;
					default:
						break;
				}
				if (_jsonData == null)
				{
					_jsonData = new
					{
						ErrorCode = 1,
						ErrorMessage = "Chart data not available."
					};
				}
			}
			catch (Exception ex)
			{
				base.LogException(ex);
			}

			return JsonCamelCase(_jsonData, JsonRequestBehavior.AllowGet);
		}

		public ActionResult DuePayments(int? st, int? ps)
		{
			int _stIndex = 0;
			int _pageSize = 0;
			if (st.HasValue)
			{
				_stIndex = st.Value;
			}
			if (ps.HasValue)
			{
				_pageSize = ps.Value;
			}
			if (_stIndex <= 0)
			{
				_stIndex = 1;
			}
			if (_pageSize <= 0)
			{
				_pageSize = Infrastructure.Constants.Default.PageSize;
			}

			var _ssBO = Infrastructure.BOVMMapper.Map<Models.SaleSearchVM, BO.SaleSearchBO>(new Models.SaleSearchVM()
			{
				ClientID = Infrastructure.Web.SessionManager.CurrentLoggedInUser.ClientID,
				StartIndex = _stIndex,
				PageSize = _pageSize
			});
			var _ssrBO = _dashboardService.GetDuePayments(_ssBO);
			Models.SaleSearchResultVM _model = Infrastructure.BOVMMapper.Map<BO.SaleSearchResultBO, Models.SaleSearchResultVM>(_ssrBO);

			_model.StartIndex = _stIndex;
			_model.PageSize = _pageSize;

			return PartialView("_DuePayments", _model);
		}

		private object Last12MonthsSaleChartData()
		{
			return _dashboardService.GetSaleChartData(Infrastructure.Web.SessionManager.CurrentLoggedInUser.ClientID);
		}
		private object Last12MonthsBrokerageChartData()
		{
			return _dashboardService.GetBrokerageChartData(Infrastructure.Web.SessionManager.CurrentLoggedInUser.ClientID);
		}
		private object BrokerageDistributionChartData()
		{
			return _dashboardService.GetBrokerageBistributionChartData(Infrastructure.Web.SessionManager.CurrentLoggedInUser.ClientID);
		}
		private object Last12LoanInterestPaidChartData()
		{
			return _dashboardService.GetLoanInerestPaidChartData(Infrastructure.Web.SessionManager.CurrentLoggedInUser.ClientID);
		}
		private object Last24MonthsLoanChartData()
		{
			return _dashboardService.GetLoanChartData(Infrastructure.Web.SessionManager.CurrentLoggedInUser.ClientID);
		}
	}
}