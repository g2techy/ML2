using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2.DAL
{
    public enum DatabaseProvider
    {
        MSSQL = 0,
        Oracle = 1,
        MySql = 2
    }

    public abstract class BaseDatabaseAccess : IDatabaseAccess
    {

        #region Local variables

        private const string m_strDatabaseConnStrKey = "DBConn";
        private static string m_strConnectionString = string.Empty;
        private static ConnectionStringSettings m_connectionSettings;

        #endregion

        #region Properties

        public string ConnectionString
        {
            get
            {
                return m_strConnectionString;
            }
        }

        public static DatabaseProvider CurrentDatabaseProvider
        {
            get
            {
                DatabaseProvider _dbProvider = DatabaseProvider.MSSQL;
                if (m_connectionSettings.ProviderName.Equals("System.Data.SqlClient", StringComparison.OrdinalIgnoreCase))
                {
                    _dbProvider = DatabaseProvider.MSSQL;
                }
                else if (m_connectionSettings.ProviderName.ToUpper().Contains("ORACLE"))
                {
                    _dbProvider = DatabaseProvider.Oracle;
                }
                return _dbProvider;
            }
        }
        #endregion

        #region Constructors

        static BaseDatabaseAccess()
        {
            m_connectionSettings = ConfigurationManager.ConnectionStrings[m_strDatabaseConnStrKey];
            if (m_connectionSettings == null)
            {
                throw new Exception(string.Format("Connection string not found. Key : '{0}'", m_strDatabaseConnStrKey));
            }
            m_strConnectionString = m_connectionSettings.ConnectionString;
        }

        #endregion


        #region IDatabaseAccess members

        public abstract void CloseConnection();

        public abstract void CommitTransaction();

        public abstract DataSet ExecuteProcedure(string procName, IEnumerable<DatabaseParameter> parameters);

        public abstract IDictionary<string, object> ExecuteProcedureDML(string procName, IEnumerable<DatabaseParameter> parameters);

        public abstract T ExecuteProcedureDML<T>(string procName, IEnumerable<DatabaseParameter> parameters) where T : class;

        public abstract DataTable ExecuteQuery(string query, IEnumerable<DatabaseParameter> parameters);

        public abstract void OpenConnection(bool openTrans = false);

        public abstract void RollbackTransaction();

        public abstract void StartTransaction();

        #endregion

    }
}
