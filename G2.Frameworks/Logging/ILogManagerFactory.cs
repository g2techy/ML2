using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2.Frameworks.Logging
{
    interface ILogManagerFactory
    {
        ILogManager GetLogManager();
    }
}
