using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace G2.Frameworks.MVC.ExceptionHandling
{
    public class ErrorController : Controller
    {
        public ActionResult Error()
        {
            string _errorID = Request.QueryString["ErrorID"];
            string _errorNumber = Request.QueryString["ErrorNumber"];
            if (!string.IsNullOrEmpty(_errorID))
            {
                var _error = new HandleErrorModel(_errorID, _errorNumber)
                {
                    ErrorMessage = string.Empty,
                    ErrorTrace = string.Empty
                };
                return View(ErrorHandlerAttribute.ErrorGeneralView,_error);
            }
            return View(ErrorHandlerAttribute.ErrorGeneralView);
        }
    }
}
