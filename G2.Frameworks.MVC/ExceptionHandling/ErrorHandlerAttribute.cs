using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace G2.Frameworks.MVC.ExceptionHandling
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class ErrorHandlerAttribute : HandleErrorAttribute
    {
        public static string ViewDataErrorKey = "ErrorData";
        private static string m_strErrorContoller = "Error";
        private static string m_strErrorGeneralView = "Error";
        private static string m_strDefaultErrorMessage = "Oops there was an issue while processing your request!";
        private static bool m_boolShowActualError = false;
        private static bool m_boolRedirectToHomeForStatusCode404 = false;

        public static bool RedirectToHomeForStatusCode404
        {
            get
            {
                return m_boolRedirectToHomeForStatusCode404;
            }
        }

        public static bool ShowActualError
        {
            get
            {
                return m_boolShowActualError;
            }
        }

        public static string ErrorGeneralView
        {
            get
            {
                return m_strErrorGeneralView;
            }
        }

        public static string ErrorContoller
        {
            get
            {
                return m_strErrorContoller;
            }
        }
        public static string DefaultErrorMessage
        {
            get
            {
                return m_strDefaultErrorMessage;
            }
        }
        public static void Configure(string errorController, string errorView, bool showActualError, string defaultErrorMsg, bool redirectToHomeFor404 = false)
        {
            if (!string.IsNullOrEmpty(errorController))
            {
                m_strErrorContoller = errorController;
            }
            if (!string.IsNullOrEmpty(errorView))
            {
                m_strErrorGeneralView = errorView;
            }
            if (!string.IsNullOrEmpty(defaultErrorMsg))
            {
                m_strDefaultErrorMessage = defaultErrorMsg;
            }
            m_boolShowActualError = showActualError;
            m_boolRedirectToHomeForStatusCode404 = redirectToHomeFor404;
        }

        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled ) // || !filterContext.HttpContext.IsCustomErrorEnabled)
                return;

            var statusCode = (int)HttpStatusCode.InternalServerError;
            if (filterContext.Exception is HttpException)
            {
                statusCode = (filterContext.Exception as HttpException).GetHttpCode();
            }
            else if (filterContext.Exception is UnauthorizedAccessException)
            {
                statusCode = (int)HttpStatusCode.Forbidden;
            }

            var result = CreateActionResult(filterContext, statusCode);
            filterContext.Result = result;

            Utilities.LogManager.Error(filterContext.Exception);

            // Prepare the response code.
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = statusCode;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }

        protected virtual ActionResult CreateActionResult(ExceptionContext filterContext, int statusCode)
        {
            var _errorData = new HandleErrorModel(filterContext.Exception);
            
            /* Use Utilities.IsAjaxRequest...*/
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var _ctx = new ControllerContext(filterContext.RequestContext, filterContext.Controller);
                var _statusCodeName = ((HttpStatusCode)statusCode).ToString();

                var _viewName = SelectFirstView(_ctx,
                                               string.Format("~/Views/Error/{0}.cshtml", _statusCodeName),
                                               string.Format("~/Views/Error/{0}.cshtml", m_strErrorGeneralView),
                                               _statusCodeName,
                                               "Error");
				ActionResult _result = null;
				if (filterContext.IsChildAction)
				{
					_result = new PartialViewResult
					{
						ViewName = _viewName,
						ViewData = new ViewDataDictionary<HandleErrorModel>(_errorData),
					};
				}
				else
				{

					_result = new ViewResult
					{
						ViewName = _viewName,
						ViewData = new ViewDataDictionary<HandleErrorModel>(_errorData),
					};
				}
                return _result;
            }
            else
            {
                var _jsonResult = new JsonResult()
                {
                    Data = _errorData,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                return _jsonResult;
            }
        }

        protected string SelectFirstView(ControllerContext ctx, params string[] viewNames)
        {
            return viewNames.First(view => ViewExists(ctx, view));
        }

        protected bool ViewExists(ControllerContext ctx, string name)
        {
            var result = ViewEngines.Engines.FindView(ctx, name, null);
            return result.View != null;
        }
        
    }
    
}
