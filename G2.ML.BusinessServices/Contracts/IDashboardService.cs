﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO = G2.ML.BusinessObjects;

namespace G2.ML.BusinessServices.Contracts
{
	public interface IDashboardService : IService
	{
		BO.ChartDataBO GetSaleChartData(int clientID);
		BO.ChartDataBO GetBrokerageChartData(int clientID);
		BO.ChartDataBO GetBrokerageBistributionChartData(int clientID);
		BO.SaleSearchResultBO GetDuePayments(BO.SaleSearchBO saleSearch);
		BO.ChartDataBO GetLoanInerestPaidChartData(int clientID);
		BO.ChartDataBO GetLoanChartData(int clientID);
	}
}
