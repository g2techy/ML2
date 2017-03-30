using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LM = G2.Frameworks.Logging;

namespace G2.DAL
{
    public class DefaultDatabaseAccessFactory : IDatabaseAccessFactory
    {
        public IDatabaseAccess GetAppDatabase()
        {
            if(BaseDatabaseAccess.CurrentDatabaseProvider == DatabaseProvider.MSSQL)
            {
                LM.DefaultLogManagerFactory.LogManager.Debug("DefaultDatabaseAccessFactory-> GetAppDatabase : SQLDatabaseAccess class instantiated...");
                return new SQLDatabaseAccess();
            }
            throw new Exception(string.Format("Database access does not avaiable for database provider : '{0}'.", BaseDatabaseAccess.CurrentDatabaseProvider));
        }
    }
}
