using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace G2.ML.Web.Controllers
{
	public class DashboardController : Infrastructure.Core.BaseController
	{
		// GET: Dashboard
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult ChartData(int chartType)
		{
			var _chartList = (Infrastructure.Constants.Dashboard.ChartList)chartType;
			object _jsonData = null;
			switch (_chartList) {
				case Infrastructure.Constants.Dashboard.ChartList.Last12MonthSales:
					_jsonData = Last12MonthsSaleChartData();
					break;
				case Infrastructure.Constants.Dashboard.ChartList.Last12MonthBrokerage:
					_jsonData = Last12MonthsBrokerageChartData();
					break;
				default:
					_jsonData = new object();
					break;
			}
			return Json(_jsonData, JsonRequestBehavior.AllowGet);
		}

		private object Last12MonthsSaleChartData()
		{
			return new
			{
				categories = new List<string>() { "Apr-16", "May-16", "Jun-16", "Jul-16", "Aug-16", "Sep-16", "Oct-16", "Nov-16", "Dec-16", "Jan-17", "Feb-17", "Mar-17" },
				series = new List<ChartSeries>()
				{
					new ChartSeries(){ name="Monthly Sale Amout", data = new List<object>{ 67908, 25800.00, 152683.00, 102563, 85000, 95682.00, 67908, 25800.00, 152683.00, 102563, 85000, 95682.00 } }
				}
			};
		}
		private object Last12MonthsBrokerageChartData()
		{
			return new
			{
				categories = new List<string>() { "Apr-16", "May-16", "Jun-16", "Jul-16", "Aug-16", "Sep-16", "Oct-16", "Nov-16", "Dec-16", "Jan-17", "Feb-17", "Mar-17" },
				series = new List<ChartSeries>()
				{
					new ChartSeries(){ name="Brokerage (Self)", data = new List<object>{ 32500, 25800.00, 28060.00, 65250, 41000, 18080, 22008, 25800.00, 52683.00, 10263, 16000, 95682.00 } },
					new ChartSeries(){ name="Brokerage (Others)", data = new List<object>{ 18500, 12008.00, 5800.00, 0, 6050, 18080, 14000, 9800, 8080, 19263, 20020, 1050.80 } }
				}
			};
		}

		private class ChartSeries
		{
			public string name { get; set; }
			public List<object> data { get; set; }
		}

	}
}