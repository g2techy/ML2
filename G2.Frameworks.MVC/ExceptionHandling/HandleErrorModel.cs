﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2.Frameworks.MVC.ExceptionHandling
{
    public class HandleErrorModel
    {
        public string ErrorID { get; private set; }
        public string ErrorNumber { get; private set; }
        public string ErrorMessage { get; set; }
        public string ErrorTrace { get; set; }

        public HandleErrorModel(Exception exception)
        {
            Core.BaseException _baseEx = null;
            if(exception is Core.BaseException)
            {
                _baseEx = exception as Core.BaseException;
            }
            else
            {
                _baseEx = new Core.BaseException(Core.BaseException.DefaultErrorNumber, exception.Message, exception);
            }
            ErrorID = _baseEx.ErrorID;
            ErrorNumber = _baseEx.ErrorNumber;
            ErrorMessage = MVC.ExceptionHandling.ErrorHandlerAttribute.ShowActualError ? _baseEx.Message : string.Empty;
            ErrorTrace = (_baseEx.InnerException != null && MVC.ExceptionHandling.ErrorHandlerAttribute.ShowActualError) ? _baseEx.InnerException.StackTrace : string.Empty;
        }

        public HandleErrorModel(string errorID, string errorNumber) : this(errorID, errorNumber, string.Empty)
        {

        }

        public HandleErrorModel(string errorID, string errorNumber, string errorMessage)
        {
            if (string.IsNullOrEmpty(errorID))
            {
                throw new ArgumentNullException("errorID");
            }
            if (string.IsNullOrEmpty(errorNumber))
            {
                throw new ArgumentNullException("errorNumber");
            }
            ErrorID = errorID;
            ErrorNumber = errorNumber;
            ErrorMessage = errorMessage;
        }
    }
}
