using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BO = G2.ML.BusinessObjects;
using BS = G2.ML.BusinessServices;

namespace G2.ML.Web.Controllers
{
    public class BuyerController : Web.Infrastructure.Core.BaseController
	{
		#region DI settings

		private readonly BS.Contracts.IBuyerService _buyerService;
		public BuyerController(BS.Contracts.IBuyerService buyerService)
		{
			_buyerService = buyerService;
		}

		#endregion

		public Models.BuyerSearchVM CurrentBuyerSearchModel
        {
            get
            {
                if (Session["BuyerSearchModel"] != null)
                {
                    return Session["BuyerSearchModel"] as Models.BuyerSearchVM;
                }
                return null;
            }
            set
            {
                Session["BuyerSearchModel"] = value;
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
            Models.BuyerSearchVM _model = new Models.BuyerSearchVM();
            return View(_model);
        }

        [HttpPost]
        public ActionResult Index(Models.BuyerSearchVM model)
        {
            model.ClientID = ClientID;
            if (ModelState.IsValid)
            {
                CurrentBuyerSearchModel = model;
                return BuyerList(null, null);
            }
            return Json(new { ErrorCode = 1 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuyerList(int? st, int? ps)
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

			CurrentBuyerSearchModel.StartIndex = _stIndex;
			CurrentBuyerSearchModel.PageSize = _pageSize;

			var _buyerSearchBO = Infrastructure.BOVMMapper.Map<Models.BuyerSearchVM, BO.BuyerSearchBO>(CurrentBuyerSearchModel);
			var _bsrBO = _buyerService.GetBuyerList(_buyerSearchBO);
            Models.BuyerSeachResultVM _model = Infrastructure.BOVMMapper.Map<BO.BuyerSearchResultBO, Models.BuyerSeachResultVM>(_bsrBO);

			_model.StartIndex = _stIndex;
            _model.PageSize = _pageSize;

            return PartialView("_BuyerList", _model);
        }

        [HttpGet]
        public ActionResult Add(int? buyerID)
        {
            Models.BuyerVM _model = null;
            int _buyerID = 0;
            if (buyerID.HasValue)
            {
                _buyerID = buyerID.Value;
                if (_buyerID > 0)
                {
                    var _bm = _buyerService.GetBuyerDetails(ClientID, _buyerID);
                    if (_bm != null)
                    {
                        _model = Infrastructure.BOVMMapper.Map<BO.BuyerBO, Models.BuyerVM>(_bm);
                    }
                }
            }
            if (_model == null)
            {
                _model = new Models.BuyerVM();
				_model.BuyerID = _buyerID;
				_model.ClientID = ClientID;
			}

            _model.BuyerTypeList = Infrastructure.BOVMMapper.Map<List<BO.BuyerTypeBO>, List<Models.BuyerTypeVM>>(_buyerService.GetBuyerTypeList());

            return View(_model);
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
        public ActionResult Add(Models.BuyerVM model)
        {
			try
			{
				model.ClientID = ClientID;
				if (ModelState.IsValid)
				{
					int _buyerID = _buyerService.Add(Infrastructure.BOVMMapper.Map<Models.BuyerVM, BO.BuyerBO>(model));
					if (_buyerID > 0)
					{
						return RedirectToAction("Add", new { buyerID = _buyerID });
					}
				}
			}
			catch (Exception ex)
			{
				base.LogException(ex);
			}
			model.BuyerTypeList = Infrastructure.BOVMMapper.Map<List<BO.BuyerTypeBO>, List<Models.BuyerTypeVM>>(_buyerService.GetBuyerTypeList());
			return View(model);
        }

        public ActionResult Delete(int buyerID)
        {
            int _returnVal = _buyerService.Delete(ClientID, buyerID);
            if (_returnVal > 0)
            {
                return BuyerList(null, null);
            }
            return Json(new { ErrorCode = 1 }, JsonRequestBehavior.AllowGet);
        }


    }
}
