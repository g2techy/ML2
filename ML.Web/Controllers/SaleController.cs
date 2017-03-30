using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BO = G2.ML.BusinessObjects;
using BS = G2.ML.BusinessServices;

namespace G2.ML.Web.Controllers
{
	public class SaleController : Web.Infrastructure.Core.BaseController
	{
		#region DI settings

		private readonly BS.Contracts.ISaleService _saleService;
		public SaleController(BS.Contracts.ISaleService saleService)
		{
			_saleService = saleService;
		}

		#endregion

		public Models.SaleSearchVM CurrentSearchModel
		{
			get
			{
				if (Session["SearchModel"] != null)
				{
					return Session["SearchModel"] as Models.SaleSearchVM;
				}
				return null;
			}
			set
			{
				Session["SearchModel"] = value;
			}

		}

		public int ClientID
		{
			get
			{
				return Infrastructure.Web.SessionManager.CurrentLoggedInUser.ClientID;
			}
		}

		public ActionResult Index()
		{
			Models.SaleSearchVM _model = new Models.SaleSearchVM();
			_model.SallerList = GetBuyerList(1);
			_model.BuyerList = GetBuyerList(2);
			return View(_model);
		}

		[HttpPost]
		public ActionResult Index(Models.SaleSearchVM model)
		{
			model.ClientID = ClientID;
			CurrentSearchModel = model;
			return SalesList(null, null);
		}

		public ActionResult SalesList(int? st, int? ps)
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

			CurrentSearchModel.StartIndex = _stIndex;
			CurrentSearchModel.PageSize = _pageSize;

			var _ssBO = Infrastructure.BOVMMapper.Map<Models.SaleSearchVM, BO.SaleSearchBO>(CurrentSearchModel);
			var _ssrBO = _saleService.GetSalesList(_ssBO);
			Models.SaleSearchResultVM _model = Infrastructure.BOVMMapper.Map<BO.SaleSearchResultBO, Models.SaleSearchResultVM>(_ssrBO);

			_model.StartIndex = _stIndex;
			_model.PageSize = _pageSize;

			return PartialView("_SalesList", _model);
		}

		public ActionResult Delete(int saleID)
		{
			int _returnVal = _saleService.Delete(ClientID, saleID);
			if (_returnVal > 0)
			{
				return SalesList(null, null);
			}
			return Json(new { ErrorCode = 1 }, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public ActionResult Add()
		{
			ViewData["IsFirstRequest"] = 1;
			Models.SaleAddVM _model = new Models.SaleAddVM();
			SetDefaultModel(ref _model);
			return View(_model);
		}

		[HttpPost]
		public ActionResult Add(Models.SaleAddVM model)
		{
			if (ModelState.IsValid)
			{
				model.ClientID = ClientID;
				int _saleID = _saleService.Add(Infrastructure.BOVMMapper.Map<Models.SaleAddVM, BO.SaleAddBO>(model));
				return RedirectToAction("Update", new { SaleID = _saleID });
			}
			SetDefaultModel(ref model);
			return View(model);
		}

		public ActionResult Update(int SaleID)
		{
			var _model = Infrastructure.BOVMMapper.Map<BO.SaleAddBO, Models.SaleAddVM>(_saleService.GetSaleDetails(ClientID, SaleID));
			if (_model != null)
			{
				SetDefaultModel(ref _model);
				_model.SaleID = SaleID;
				return View("Update", _model);
			}
			return View("Update");
		}

		[HttpPost]
		public ActionResult Update(Models.SaleAddVM model)
		{
			return Add(model);
		}

		public ActionResult Payment(int saleID)
		{
			Models.SalePaymentVM _model = new Models.SalePaymentVM();
			_model.Payment = new Models.SalePayment()
			{
				SaleID = saleID
			};
			_model.PaymentList = GetPaymentList(saleID);
			return PartialView("_Payment", _model);
		}

		[HttpPost]
		public ActionResult Payment(Models.SalePaymentVM model)
		{
			if (ModelState.IsValid)
			{
				var _bdID = _saleService.AddPayment(Infrastructure.BOVMMapper.Map<Models.SalePayment, BO.SalePaymentBO>(model.Payment));
				if (_bdID > 0)
				{
					return PaymentGrid(model.Payment.SaleID);
				}
			}
			return Json(new { ErrorCode = 1 });
		}

		public ActionResult PaymentGrid(int saleID)
		{
			Models.SalePaymentVM _model = new Models.SalePaymentVM();
			_model.PaymentList = GetPaymentList(saleID);
			return PartialView("_PaymentGrid", _model);
		}

		[HttpPost]
		public ActionResult CloseSale(int saleID)
		{
			int _returnVal = _saleService.CloseSale(saleID);
			return Json(new { ErrorCode = _returnVal });
		}

		private List<Models.SalePayment> GetPaymentList(int saleID)
		{
			List<Models.SalePayment> _payList = new List<Models.SalePayment>();
			List<BO.SalePaymentBO> _bo = _saleService.GetPaymentList(ClientID, saleID);
			if (_bo != null && _bo.Count > 0)
			{
				_payList = Infrastructure.BOVMMapper.Map<List<BO.SalePaymentBO>, List<Models.SalePayment>>(_bo);
			}
			return _payList;
		}

		public ActionResult DeletePayment(int payID)
		{
			int _saleID = _saleService.DeletePayment(payID);
			if (_saleID > 0)
			{
				return PaymentGrid(_saleID);
			}
			return Json(new { ErrorCode = 1 });
		}

		public ActionResult Brokerage(int saleID)
		{
			Models.SaleBrokerageVM _model = new Models.SaleBrokerageVM();
			_model.SaleID = saleID;
			var _brokerList = _saleService.GetBuyerList(ClientID, 3);
			_model.BrokerList = new SelectList(_brokerList, "BuyerID", "BuyerName");

			_model.BrokerageList = GetBrokerageList(_model.SaleID);
			return PartialView("_Brokerage", _model);
		}

		private List<Models.SaleBrokerage> GetBrokerageList(int saleID)
		{
			List<Models.SaleBrokerage> _list = new List<Models.SaleBrokerage>();

			List<BO.SaleBrokerageBO> _bo = _saleService.GetBrokerageList(ClientID, saleID);
			if (_bo != null && _bo.Count > 0)
			{
				_list = Infrastructure.BOVMMapper.Map<List<BO.SaleBrokerageBO>, List<Models.SaleBrokerage>>(_bo);
			}

			return _list;
		}

		[HttpPost]
		public ActionResult Brokerage(Models.SaleBrokerageVM model)
		{
			if (ModelState.IsValid)
			{
				var _bdID = _saleService.AddBrokerage(Infrastructure.BOVMMapper.Map<Models.SaleBrokerage, BO.SaleBrokerageBO>(new Models.SaleBrokerage()
				{
					SaleID = model.SaleID,
					BrokerID = model.BrokerID,
					Brokerage = model.Brokerage
				}));
				if (_bdID > 0)
				{
					return BrokerageGrid(model.SaleID);
				}
			}
			return Json(new { ErrorCode = 1 });
		}

		public ActionResult BrokerageGrid(int saleID)
		{
			Models.SaleBrokerageVM _model = new Models.SaleBrokerageVM();
			_model.BrokerageList = GetBrokerageList(saleID);
			_model.SaleID = saleID;
			return PartialView("_BrokerageGrid", _model);
		}

		public ActionResult DeleteBrokerage(int BDID)
		{
			int _saleID = _saleService.DeleteBrokerage(BDID);
			if (_saleID > 0)
			{
				return BrokerageGrid(_saleID);
			}
			return Json(new { ErrorCode = 1 });
		}

		private void SetDefaultModel(ref Models.SaleAddVM saleAdd)
		{
			var _buyerList = _saleService.GetBuyerList(ClientID, 2);
			var _sallerList = _saleService.GetBuyerList(ClientID, 1);

			saleAdd.BuyerList = new SelectList(_buyerList, "BuyerID", "BuyerName");
			saleAdd.SallerList = new SelectList(_sallerList, "BuyerID", "BuyerName");
		}

		private SelectList GetBuyerList(int buyerTypeID)
		{
			SelectList _list = null;
			var _buyerList = _saleService.GetBuyerList(ClientID, buyerTypeID);
			_list = new SelectList(_buyerList, "BuyerID", "BuyerName");
			return _list;
		}

	}
}
