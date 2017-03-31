using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DF = G2.Frameworks;
using DFM = G2.Frameworks.MVC;

namespace G2.ML.Web.Infrastructure.Core
{
	[Infrastructure.Filters.Authentication]
	public class BaseController : Controller
    {
        public string CurrentLanguageCode { get; set; }

        protected void LogException(Exception ex)
        {
            G2.Frameworks.Logging.DefaultLogManagerFactory.LogManager.Error(ex);
            ModelState.AddModelError(string.Empty, BuildErrorMessageFromException(ex));
        }

        protected string BuildErrorMessageFromException(Exception ex)
        {
            string _msg = ex.Message;
            string _errorMsg = ex.Message;
            string _errorID = string.Empty;
            string _errorNumber = string.Empty;
            if (ex is DF.Core.BaseException)
            {
                var _coreEx = (DF.Core.BaseException)ex;
                _errorID = _coreEx.ErrorID;
                _errorNumber = _coreEx.ErrorNumber;
            }
            else
            {
                _errorID = DF.Core.BaseException.NewErrorID;
                _errorNumber = DF.Core.BaseException.DefaultErrorNumber;
            }
            _msg = string.Format(@"ErrorID: {0} | ErrorNumber: {1} - {2}",
                                     _errorID,
                                     _errorNumber,
                                     DFM.ExceptionHandling.ErrorHandlerAttribute.ShowActualError ? _errorMsg : DFM.ExceptionHandling.ErrorHandlerAttribute.DefaultErrorMessage);
            return _msg;
        }
        
        protected override void Initialize(RequestContext requestContext)
        {
            if (requestContext.RouteData.Values["lang"] != null && requestContext.RouteData.Values["lang"] as string != "null")
            {
                CurrentLanguageCode = (string)requestContext.RouteData.Values["lang"];
                if (CurrentLanguageCode != null)
                {
                    try
                    {
                        Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(CurrentLanguageCode);
                    }
                    catch (Exception)
                    {
                        throw new NotSupportedException($"Invalid language code '{CurrentLanguageCode}'.");
                    }
                }
            }
            base.Initialize(requestContext);
        }

		protected ActionResult JsonCamelCase(object data, JsonRequestBehavior jsonRequestBehavior)
		{
			return new JsonCamelCaseResult(data, jsonRequestBehavior);
		}
	}
}