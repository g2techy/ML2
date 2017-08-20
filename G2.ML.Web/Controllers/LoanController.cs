using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BO = G2.ML.BusinessObjects;
using BS = G2.ML.BusinessServices;

namespace G2.ML.Web.Controllers
{
	public class LoanController : Web.Infrastructure.Core.BaseController
	{

		#region DI settings

		private readonly BS.Contracts.ILoanService _loanService;
		public LoanController(BS.Contracts.ILoanService loanService)
		{
			_loanService = loanService;
		}

		#endregion

		public int ClientID
		{
			get
			{
				return Infrastructure.Web.SessionManager.CurrentLoggedInUser.ClientID;
			}
		}

		public Models.LoanSearchVM CurrentSearchModel
		{
			get
			{
				if (Session["SearchModel"] != null)
				{
					return Session["SearchModel"] as Models.LoanSearchVM;
				}
				return null;
			}
			set
			{
				Session["SearchModel"] = value;
			}
		}

		public ActionResult Index()
		{
			CurrentSearchModel = null;
			Models.LoanSearchVM _model = new Models.LoanSearchVM();
			_model.BorrowerList = GetBuyerList(4);
			return View(_model);
		}

		[HttpPost]
		public ActionResult Index(Models.LoanSearchVM model)
		{
			model.ClientID = ClientID;
			CurrentSearchModel = model;
			return LoanList(null, null);
		}

		public ActionResult LoanList(int? st, int? ps)
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

			var _ssBO = Infrastructure.BOVMMapper.Map<Models.LoanSearchVM, BO.LoanSearchBO>(CurrentSearchModel);
			var _ssrBO = _loanService.GetLoanList(_ssBO);
			Models.LoanSearchResultVM _model = Infrastructure.BOVMMapper.Map<BO.LoanSearchResultBO, Models.LoanSearchResultVM>(_ssrBO);

			_model.StartIndex = _stIndex;
			_model.PageSize = _pageSize;

			return PartialView("_LoanList", _model);
		}

		public ActionResult Delete(int loanID)
		{
			int _returnVal = _loanService.Delete(ClientID, loanID);
			if (_returnVal > 0)
			{
				return LoanList(null, null);
			}
			return Json(new { ErrorCode = 1 }, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public ActionResult Add()
		{
			ViewData["IsFirstRequest"] = 1;
			Models.LoanAddVM _model = new Models.LoanAddVM();
			SetDefaultModel(ref _model);
			return View(_model);
		}

		[HttpPost]
		public ActionResult Add(Models.LoanAddVM model)
		{
			if (ModelState.IsValid)
			{
				model.ClientID = ClientID;
				int _loanID = _loanService.Add(Infrastructure.BOVMMapper.Map<Models.LoanAddVM, BO.LoanAddBO>(model));
				return RedirectToAction("Update", new { LoanID = _loanID });
			}
			SetDefaultModel(ref model);
			return View(model);
		}

		public ActionResult Update(int loanID)
		{
			var _model = Infrastructure.BOVMMapper.Map<BO.LoanAddBO, Models.LoanAddVM>(_loanService.GetLoanDetails(ClientID, loanID));
			if (_model != null)
			{
				SetDefaultModel(ref _model);
				_model.LoanID = loanID;
				return View("Update", _model);
			}
			return View("Update");
		}

		[HttpPost]
		public ActionResult Update(Models.LoanAddVM model)
		{
			return Add(model);
		}

		public ActionResult Payment(int loanID)
		{
			Models.LoanPaymentVM _model = new Models.LoanPaymentVM();
			_model.Payment = new Models.LoanPayment()
			{
				LoanID = loanID
			};
			_model.PaymentList = GetPaymentList(loanID);
			return PartialView("_Payment", _model);
		}

		[HttpPost]
		public ActionResult Payment(Models.LoanPaymentVM model)
		{
			if (ModelState.IsValid)
			{
				var _bdID = _loanService.AddPayment(Infrastructure.BOVMMapper.Map<Models.LoanPayment, BO.LoanPaymentBO>(model.Payment));
				if (_bdID > 0)
				{
					return PaymentGrid(model.Payment.LoanID);
				}
			}
			return Json(new { ErrorCode = 1 });
		}

		public ActionResult PaymentGrid(int loanID)
		{
			Models.LoanPaymentVM _model = new Models.LoanPaymentVM();
			_model.PaymentList = GetPaymentList(loanID);
			return PartialView("_PaymentGrid", _model);
		}

		[HttpPost]
		public ActionResult CloseLoan(int loanID)
		{
			int _returnVal = _loanService.Close(loanID);
			return Json(new { ErrorCode = _returnVal });
		}

		public ActionResult CalcInt(int loanID, string intAsOn)
		{
			DateTime _intAsOn = DateTime.Now;
			if (!string.IsNullOrEmpty(intAsOn))
			{
				try
				{
					if (DateTime.TryParse(intAsOn, out _intAsOn))
					{
						_intAsOn.AddDays(1);
					}
				}
				catch { _intAsOn = DateTime.Now; }
				ViewBag.IntAsOnDate = _intAsOn;
			}
			List<Models.LoanCalcInterestVM> _model = null;
			List<BO.LoanCalcInterestBO> _bo = _loanService.GetCalcInterest(ClientID, loanID, _intAsOn);
			if (_bo != null)
			{
				_model = Infrastructure.BOVMMapper.Map<List<BO.LoanCalcInterestBO>, List<Models.LoanCalcInterestVM>>(_bo);
			}
			return PartialView("_CalcInt", _model);
		}

		private List<Models.LoanPayment> GetPaymentList(int loanID)
		{
			List<Models.LoanPayment> _payList = new List<Models.LoanPayment>();
			List<BO.LoanPaymentBO> _bo = _loanService.GetPaymentList(ClientID, loanID);
			if (_bo != null && _bo.Count > 0)
			{
				_payList = Infrastructure.BOVMMapper.Map<List<BO.LoanPaymentBO>, List<Models.LoanPayment>>(_bo);
			}
			return _payList;
		}

		public ActionResult DeletePayment(int loanPayID)
		{
			int _loanID = _loanService.DeletePayment(loanPayID);
			if (_loanID > 0)
			{
				return PaymentGrid(_loanID);
			}
			return Json(new { ErrorCode = 1 });
		}

		#region Private methods 

		private void SetDefaultModel(ref Models.LoanAddVM loanAdd)
		{
			var _borrowerList = _loanService.GetBorrowerList(ClientID, 4);
			loanAdd.BorrowerList = new SelectList(_borrowerList, "BuyerID", "BuyerName");
		}

		private SelectList GetBuyerList(int buyerTypeID)
		{
			SelectList _list = null;
			var _buyerList = _loanService.GetBorrowerList(ClientID, buyerTypeID);
			_list = new SelectList(_buyerList, "BuyerID", "BuyerName");
			return _list;
		}

		#endregion

	}
}