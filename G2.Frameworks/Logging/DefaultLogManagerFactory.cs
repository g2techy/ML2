using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2.Frameworks.Logging
{
    public class DefaultLogManagerFactory : ILogManagerFactory
    {

        private static ILogManager m_logManager = null;
        private static object m_lockObj = new object();

        public static ILogManager LogManager
        {
            get
            {
                if(m_logManager == null)
                {
                    lock (m_lockObj)
                    {
                        m_logManager = (new DefaultLogManagerFactory()).GetLogManager();
                    }
                }
                return m_logManager;
            }
        }
        
        private DefaultLogManagerFactory()
        {

        }

        #region ILogManagerFactory members

        public ILogManager GetLogManager()
        {
            return new Log4NetLogManager();
        }

        #endregion
    }
}
