using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2.Frameworks.Logging
{
    public interface ILogManager
    {
        void Configure(object obj);
        void Debug(string message);
        void Warn(string message);
        void Error(string message);
        void Error(Exception ex);
        void Fatal(string message);
        void Info(string message);
    }
}
