using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace G2.Frameworks.MVC.ExceptionHandling
{
	public class ErrorHandlerHttpModule : IHttpModule
	{
		private static bool _isEnabled = false;

		public static bool IsEnabled
		{
			get
			{
				return _isEnabled;
			}
		}
		public void Dispose()
		{

		}

		public void Init(HttpApplication context)
		{
			_isEnabled = true;
			context.Error += Context_Error;
		}

		private void Context_Error(object sender, EventArgs e)
		{
			HttpApplication _appObject = sender as HttpApplication;
			if (_appObject != null)
			{
				Exception _exception = _appObject.Server.GetLastError();
				if (_exception == null)
				{
					return;
				}
				_appObject.Server.ClearError();

				Core.BaseException _coreEx = null;
				if (_exception is Core.BaseException)
				{
					_coreEx = (Core.BaseException)_exception;
				}
				else
				{
					_coreEx = new Core.BaseException(Utilities.DefaultErrorNumber, _exception.Message, _exception);
				}
				string _errorNumber = _coreEx.ErrorNumber;
				string _errorID = _coreEx.ErrorID;

				Utilities.LogManager.Error(_coreEx);

				if (Utilities.IsAjaxRequest)
				{
					var _jsonSerializer = new JavaScriptSerializer();
					var _json = _jsonSerializer.Serialize(new HandleErrorModel(_coreEx));

					_appObject.Response.Clear();
					_appObject.Response.ContentType = Utilities.JsonContentType;
					_appObject.Response.Write(_json);

					_jsonSerializer = null;
				}
				else
				{
					_appObject.Response.Redirect(string.Format("/{0}/{1}?ErrorID={2}&ErrorNumber={3}", ErrorHandlerAttribute.ErrorContoller, ErrorHandlerAttribute.ErrorGeneralView,
																   _errorID, _errorNumber));
				}
			}
		}

		private bool Is404Error(Exception ex)
		{
			bool _returnVal = false;
			if (ex != null)
			{
				var _baseEx = ex.GetBaseException();
				if (_baseEx != null && _baseEx is HttpException)
				{
					_returnVal = ((HttpException)_baseEx).GetHttpCode() == (int)System.Net.HttpStatusCode.NotFound;
				}
			}
			return _returnVal;
		}

	}
}
