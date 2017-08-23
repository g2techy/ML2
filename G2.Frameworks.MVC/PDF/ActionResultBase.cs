using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace G2.Frameworks.MVC.PDF
{

	public abstract class ActionResultBase : ActionResult
	{
		protected abstract string Url { get; set; }
		protected abstract string ViewName { get; set; }
		protected abstract object Model { get; set; }

		public override void ExecuteResult(ControllerContext context)
		{
			ExecuteResultInternal(context);
		}

		protected virtual void ExecuteResultInternal(ControllerContext context)
		{
			Byte[] _bContent = null;
			if (!string.IsNullOrEmpty(Url))
			{
				_bContent = GetPDFFromUrl(Url);
			}
			else
			{
				string _viewResponse = RenderViewToString(context, ViewName, Model);
				_bContent = GetPDF(_viewResponse);
			}

			var _response = context.HttpContext.Response;
			_response.Clear();
			_response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
			_response.ContentType = "application/pdf";

			using (System.IO.MemoryStream _ms = new System.IO.MemoryStream(_bContent))
			{
				_ms.WriteTo(_response.OutputStream);
			}
		}

		private string RenderViewToString(ControllerContext context, string viewName, object model)
		{
			if (string.IsNullOrEmpty(viewName))
				viewName = context.RouteData.GetRequiredString("action");

			var viewData = new ViewDataDictionary(model);

			using (var sw = new System.IO.StringWriter())
			{
				var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
				var viewContext = new ViewContext(context, viewResult.View, viewData, new TempDataDictionary(), sw);
				viewResult.View.Render(viewContext, sw);

				return sw.GetStringBuilder().ToString();
			}
		}

		protected abstract byte[] GetPDF(string htmlString);
		protected abstract byte[] GetPDFFromUrl(string url);
	}

}
