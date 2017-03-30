using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BO = G2.ML.BusinessObjects;
using BS = G2.ML.BusinessServices;
using System.Data;
using G2.ML.Web.Infrastructure.Core;

namespace G2.ML.Web.Controllers
{
    public class ReportController : Infrastructure.Core.BaseController
    {

		#region DI settings

		private readonly BS.Contracts.IReportService _reportService;
		private readonly BS.Contracts.ISaleService _saleService;
		public ReportController(BS.Contracts.IReportService reportService, BS.Contracts.ISaleService saleService)
		{
			_reportService = reportService;
			_saleService = saleService;
		}

		#endregion

		public int ClientID
        {
            get
            {
				return Infrastructure.Web.SessionManager.CurrentLoggedInUser.ClientID;
			}
        }

        public Models.SalesReportVM CurrentSaleReportVM
        {
            get
            {
                if (Session["CurrentSaleReportVM"] != null)
                {
                    return Session["CurrentSaleReportVM"] as Models.SalesReportVM;
                }
                return null;
            }
            set
            {
                Session["CurrentSaleReportVM"] = value;
            }
        }

        public Models.BrokerageReportVM CurrentBrokerageReportVM
        {
            get
            {
                if (Session["CurrentBrokerageReportVM"] != null)
                {
                    return Session["CurrentBrokerageReportVM"] as Models.BrokerageReportVM;
                }
                return null;
            }
            set
            {
                Session["CurrentBrokerageReportVM"] = value;
            }
        }

        public ActionResult Sales()
        {
            Models.SalesReportVM _model = new Models.SalesReportVM();
            _model.SallerList = GetBuyerList(1);
            _model.BuyerList = GetBuyerList(2);
            _model.StatusList = GetSaleStatusList();
            return View(_model);
        }

        [HttpPost]
        public ActionResult Sales(Models.SalesReportVM vm)
        {
            if (ModelState.IsValid)
            {
                vm.ClientID = ClientID;
                DataTable _dtData = _reportService.GetSalesReport(Infrastructure.BOVMMapper.Map<Models.SalesReportVM, BO.SalesReportBO>(vm));
                return PartialView("_DataList", _dtData);
            }
            return Json(new { ErrorCode = 1 });
        }

        [HttpPost]
        public ActionResult ExportSales(Models.SalesReportVM vm)
        {
            if (ModelState.IsValid)
            {
                vm.ClientID = ClientID;
                CurrentSaleReportVM = vm;
                return Json(new { ErrorCode = 0 });    
            }
            return Json(new { ErrorCode = 1 });
        }

        public ActionResult DownloadSalesReport()
        {
            DataTable _dtData = _reportService.GetSalesReport(Infrastructure.BOVMMapper.Map<Models.SalesReportVM, BO.SalesReportBO>(CurrentSaleReportVM));
            return new ExportResult(_dtData, "salesReport.xlsx");
        }

        public ActionResult Brokerage()
        {
            Models.BrokerageReportVM _model = new Models.BrokerageReportVM();
            _model.SallerList = GetBuyerList(1);
            _model.BuyerList = GetBuyerList(2);
            _model.StatusList = GetBrokerageStatusList();
            return View(_model);
        }

        [HttpPost]
        public ActionResult Brokerage(Models.BrokerageReportVM vm)
        {
            if (ModelState.IsValid)
            {
                vm.ClientID = ClientID;
                DataTable _dtData = _reportService.GetBrokerageReport(Infrastructure.BOVMMapper.Map<Models.BrokerageReportVM, BO.BrokerageReportBO>(vm));
                return PartialView("_DataList", _dtData);
            }
            return Json(new { ErrorCode = 1 });
        }

        [HttpPost]
        public ActionResult ExportBrokerage(Models.BrokerageReportVM vm)
        {
            if (ModelState.IsValid)
            {
                vm.ClientID = ClientID;
                CurrentBrokerageReportVM = vm;
                return Json(new { ErrorCode = 0 });
            }
            return Json(new { ErrorCode = 1 });
        }

        public ActionResult DownloadBrokerageReport()
        {
			DataTable _dtData = _reportService.GetBrokerageReport(Infrastructure.BOVMMapper.Map<Models.BrokerageReportVM, BO.BrokerageReportBO>(CurrentBrokerageReportVM));
			return new ExportResult(_dtData, "BrokerageReport.xlsx");
        }

        private SelectList GetBuyerList(int buyerTypeID)
        {
            SelectList _list = null;
            var _buyerList = _saleService.GetBuyerList(ClientID, buyerTypeID);
            _list = new SelectList(_buyerList, "BuyerID", "BuyerName");
            return _list;
        }

        private SelectList GetSaleStatusList()
        {
            SelectList _list = null;
            var _statusList = _reportService.GetSaleStatusList();
            _list = new SelectList(_statusList, "SaleStatusID", "SaleStatusValue");
            return _list;
        }

        private SelectList GetBrokerageStatusList()
        {
            List<SelectListItem> _statusList = new List<SelectListItem>();
            _statusList.Add(new SelectListItem { Text = "Pending", Value = "1" });
            _statusList.Add(new SelectListItem { Text = "Paid", Value = "2" });
            return new SelectList(_statusList, "Value", "Text");
        }
    }
}
