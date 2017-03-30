using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4Net.config", Watch = true)]
namespace G2.Frameworks.Logging
{
    public class Log4NetLogManager : ILogManager
    {

        private static log4net.ILog _log = null;

        internal Log4NetLogManager()
        {

        }

        #region ILogManager members

        void ILogManager.Configure(object obj)
        {
            log4net.Config.XmlConfigurator.Configure();

            string _loggerName = "DefaultLogger";
            if (obj != null && obj is string)
            {
                _loggerName = obj as string;
            }
            _log = log4net.LogManager.GetLogger(_loggerName);
        }

        void ILogManager.Debug(string message)
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug(message);
            }
        }

        void ILogManager.Error(Exception ex)
        {
            if (ex == null)
            {
                return;
            }
            if (_log.IsErrorEnabled)
            {
                try
                {
                    string _errorMsg = string.Format("Number: {0}", Core.BaseException.DefaultErrorNumber);
                    if (ex is Core.BaseException)
                    {
                        Core.BaseException _coreEx = (ex as Core.BaseException);
                        if (_coreEx.IsLogged)
                        {
                            return;
                        }
                        _errorMsg = string.Format("ID: {0} | Number: {1}", _coreEx.ErrorID, _coreEx.ErrorNumber);
                    }
                    Exception _innerEx = ex;
                    while (true)
                    {
                        if (_innerEx.InnerException == null)
                        {
                            break;
                        }
                        _innerEx = _innerEx.InnerException;
                    }
                    _errorMsg = _errorMsg + " | Message: " + ex.Message + " Trace: " + (_innerEx != null ? _innerEx.StackTrace : ex.StackTrace);
                    _log.Error(_errorMsg);
                }
                catch { }
            }
        }

        void ILogManager.Error(string message)
        {
            if (_log.IsErrorEnabled)
            {
                _log.Error(message);
            }
        }

        void ILogManager.Fatal(string message)
        {
            if (_log.IsFatalEnabled)
            {
                _log.Fatal(message);
            }
        }

        void ILogManager.Info(string message)
        {
            if (_log.IsInfoEnabled)
            {
                _log.Info(message);
            }
        }

        void ILogManager.Warn(string message)
        {
            if (_log.IsWarnEnabled)
            {
                _log.Warn(message);
            }
        }

        #endregion

    }
}
