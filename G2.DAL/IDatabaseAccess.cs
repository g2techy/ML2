using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2.DAL
{
    public interface IDatabaseAccess
    {
        void OpenConnection(bool openTrans = false);

        void CloseConnection();

        void StartTransaction();

        void CommitTransaction();

        void RollbackTransaction();

        DataTable ExecuteQuery(string query, IEnumerable<DatabaseParameter> parameters);

        DataSet ExecuteProcedure(string procName, IEnumerable<DatabaseParameter> parameters);

        T ExecuteProcedureDML<T>(string procName, IEnumerable<DatabaseParameter> parameters) where T : class;

        IDictionary<string, object> ExecuteProcedureDML(string procName, IEnumerable<DatabaseParameter> parameters);

    }
}
