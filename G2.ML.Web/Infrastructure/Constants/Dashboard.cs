using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G2.ML.Web.Infrastructure.Constants
{
	public static class Dashboard
	{
		public enum ChartList
		{
			Last12MonthSales = 1,
			Last12MonthBrokerage,
			BrokerageDistribution,
			Last12InterestPaid,
			Last24LoanData
		}
	}
}