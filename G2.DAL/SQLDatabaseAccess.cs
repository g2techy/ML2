using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LM = G2.Frameworks.Logging;

namespace G2.DAL
{
    public class SQLDatabaseAccess : BaseDatabaseAccess, IDisposable
    {
        #region Local variables 

        SqlConnection _conn = null;
        SqlTransaction _trans = null;

        #endregion

        #region Properties

        private bool IsConnectionOpen
        {
            get
            {
                return (_conn != null);
            }
        }

        #endregion 

        #region Constructors

        protected internal SQLDatabaseAccess() : this(false)
        {

        }

        protected internal SQLDatabaseAccess(bool openConn)
        {
            OpenConnectionInternal(openConn);
        }

        #endregion

        #region BaseDatabaseAccess members

        public override void OpenConnection(bool openTrans = false)
        {
            OpenConnectionInternal(true);
            if (openTrans)
            {
                StartTransaction();
            }
        }

        public override void CloseConnection()
        {
            if (_conn != null)
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                    LM.DefaultLogManagerFactory.LogManager.Debug("Connection closed...");
                }
            }
            _conn = null;
        }

        public override void StartTransaction()
        {
            if (IsConnectionOpen)
            {
                _trans = _conn.BeginTransaction();
                LM.DefaultLogManagerFactory.LogManager.Debug("Transaction started...");
            }
        }

        public override void CommitTransaction()
        {
            if (_trans != null)
            {
                _trans.Commit();
                LM.DefaultLogManagerFactory.LogManager.Debug("Transaction commited...");
            }
            _trans = null;
        }

        public override void RollbackTransaction()
        {
            if (_trans != null)
            {
                _trans.Rollback();
                LM.DefaultLogManagerFactory.LogManager.Debug("Transaction rollbacked...");
            }
            _trans = null;
        }

        public override DataTable ExecuteQuery(string query, IEnumerable<DatabaseParameter> parameters)
        {
            LM.DefaultLogManagerFactory.LogManager.Debug("ExecuteQuery: Started...");
            DataTable _dt = new DataTable();
            try
            {
                using (SqlCommand _sqlComm = GetSqlCommand(query, CommandType.Text))
                {
                    AddCommandParameters(_sqlComm, parameters);
                    using (SqlDataAdapter _sqlAdp = new SqlDataAdapter(_sqlComm))
                    {
                        _sqlAdp.Fill(_dt);
                    }
                }
            }
            catch (Exception ex)
            {
                LM.DefaultLogManagerFactory.LogManager.Debug("ExecuteQuery: Error...");
                throw WrapDatabaseException(ex);
            }
            LM.DefaultLogManagerFactory.LogManager.Debug("ExecuteQuery: Completed...");
            return _dt;
        }

        public override DataSet ExecuteProcedure(string procName, IEnumerable<DatabaseParameter> parameters)
        {
            LM.DefaultLogManagerFactory.LogManager.Debug("ExecuteProcedure: Started...");
            DataSet _ds = new DataSet();
            try
            {
                using (SqlCommand _sqlComm = GetSqlCommand(procName, CommandType.StoredProcedure))
                {
                    AddCommandParameters(_sqlComm, parameters);
                    using (SqlDataAdapter _sqlAdp = new SqlDataAdapter(_sqlComm))
                    {
                        _sqlAdp.Fill(_ds);
                    }
                }
            }
            catch (Exception ex)
            {
                LM.DefaultLogManagerFactory.LogManager.Debug("ExecuteProcedure: Error...");
                throw WrapDatabaseException(ex);
            }
            LM.DefaultLogManagerFactory.LogManager.Debug("ExecuteProcedure: Completed...");
            return _ds;
        }

        public override IDictionary<string, object> ExecuteProcedureDML(string procName, IEnumerable<DatabaseParameter> parameters)
        {
            LM.DefaultLogManagerFactory.LogManager.Debug("ExecuteProcedureDML: Started...");
            IDictionary<string, object> _returnData = null;
            try
            {
                using (SqlCommand _sqlComm = GetSqlCommand(procName, CommandType.StoredProcedure))
                {
                    AddCommandParameters(_sqlComm, parameters);
                    _sqlComm.Transaction = _trans;
                    _sqlComm.ExecuteNonQuery();
                    _returnData = RetrieveCommandOutParams(_sqlComm);
                }
            }
            catch (Exception ex)
            {
                throw WrapDatabaseException(ex);
            }
            LM.DefaultLogManagerFactory.LogManager.Debug("ExecuteProcedureDML: Completed...");
            return _returnData;
        }

        public override T ExecuteProcedureDML<T>(string procName, IEnumerable<DatabaseParameter> parameters)
        {
            LM.DefaultLogManagerFactory.LogManager.Debug("ExecuteProcedureDML<T>: Started...");
            IDictionary<string, object> _returnData = ExecuteProcedureDML(procName, parameters);
            if (_returnData != null && _returnData.Count > 0)
            {
                object _val = _returnData[_returnData.Keys.First()];
                if (_val != DBNull.Value)
                {
                    LM.DefaultLogManagerFactory.LogManager.Debug("ExecuteProcedureDML<T>: Completed...");
                    var _obj = (T)_val;
                    LM.DefaultLogManagerFactory.LogManager.Info(string.Format(@"ExecuteProcedureDML<T>: Return value - {0}", (_obj != null ? _obj.ToString() : "")));
                    return _obj;
                }
            }
            LM.DefaultLogManagerFactory.LogManager.Debug("ExecuteProcedureDML<T>: Completed...");
            return null;
        }

        #endregion

        #region Private methods

        private void OpenConnectionInternal(bool openConn)
        {
            if (_conn == null)
            {
                _conn = new SqlConnection(base.ConnectionString);
                /*LM.DefaultLogManagerFactory.LogManager.Info(string.Format("OpenConnection: Connection string - {0}", base.ConnectionString));*/
                LM.DefaultLogManagerFactory.LogManager.Debug("OpenConnection: Connection object instantiated...");
            }
            if (openConn)
            {
                _conn.Open();
                LM.DefaultLogManagerFactory.LogManager.Debug("OpenConnection: Connection opened...");
            }
        }

        private SqlCommand GetSqlCommand(string commandName, CommandType commandType)
        {
            LM.DefaultLogManagerFactory.LogManager.Debug("GetSqlCommand: Started...");
            if (string.IsNullOrEmpty(commandName))
            {
                throw new ArgumentNullException("commandName");
            }
            if (_conn == null)
            {
                throw new Exception("Connection is not open or already closed");
            }
            SqlCommand _sqlComm = new SqlCommand(commandName, _conn);
            _sqlComm.CommandType = commandType;
            LM.DefaultLogManagerFactory.LogManager.Info(string.Format(@"GetSqlCommand: Command '{0}' object created...", commandName));
            LM.DefaultLogManagerFactory.LogManager.Debug("GetSqlCommand: Completed...");
            return _sqlComm;
        }

        private void AddCommandParameters(SqlCommand sqlComm, IEnumerable<DatabaseParameter> commParams)
        {
            LM.DefaultLogManagerFactory.LogManager.Debug("AddCommandParameters: Started...");
            if (sqlComm != null && commParams != null)
            {
                LM.DefaultLogManagerFactory.LogManager.Info(string.Format(@"AddCommandParameters: parameter count - {0}", commParams.Count()));
                foreach (var _param in commParams)
                {
                    LM.DefaultLogManagerFactory.LogManager.Info(string.Format(@"Parameter: {0}", _param.ToString()));

                    SqlParameter _sqlParam = new SqlParameter(_param.Name, _param.Value);

                    /*Assign direction...*/
                    System.Data.ParameterDirection _direction = System.Data.ParameterDirection.Input;
                    switch (_param.Direction)
                    {
                        case ParameterDirection.In:
                            _direction = System.Data.ParameterDirection.Input;
                            break;
                        case ParameterDirection.Out:
                            _direction = System.Data.ParameterDirection.Output;
                            break;
                        case ParameterDirection.InOut:
                            _direction = System.Data.ParameterDirection.InputOutput;
                            break;
                        default:
                            _direction = System.Data.ParameterDirection.Input;
                            break;
                    }
                    _sqlParam.Direction = _direction;

                    /*Assign database type...*/
                    SqlDbType _sqlDBType = SqlDbType.SmallInt;
                    switch (_param.DataType)
                    {
                        case DataType.SmallInt:
                            _sqlDBType = SqlDbType.SmallInt;
                            break;
                        case DataType.TinyInt:
                            _sqlDBType = SqlDbType.TinyInt;
                            break;
                        case DataType.Int:
                            _sqlDBType = SqlDbType.Int;
                            break;
                        case DataType.Decimal:
                            _sqlDBType = SqlDbType.Decimal;
                            break;
                        case DataType.Money:
                            _sqlDBType = SqlDbType.Money;
                            break;
                        case DataType.Date:
                            _sqlDBType = SqlDbType.Date;
                            break;
                        case DataType.DateTime:
                            _sqlDBType = SqlDbType.DateTime;
                            break;
                        case DataType.Char:
                            _sqlDBType = SqlDbType.Char;
                            break;
                        case DataType.String:
                            _sqlDBType = SqlDbType.VarChar;
                            break;
                        case DataType.NString:
                            _sqlDBType = SqlDbType.NVarChar;
                            break;
                        case DataType.MaxString:
                            _sqlDBType = SqlDbType.NVarChar;
                            break;						
						default:
                            _sqlDBType = SqlDbType.VarChar;
                            break;
                    }
                    _sqlParam.SqlDbType = _sqlDBType;

                    /*Assign size...*/
                    if (_param.DataType == DataType.Byte || _param.DataType == DataType.Char
                        || _param.DataType == DataType.String || _param.DataType == DataType.NString)
                    {
                        _sqlParam.Size = _param.Size;
                    }
                    else if (_param.DataType == DataType.MaxString)
                    {
                        _sqlParam.Size = -1;
                    }
                    else if (_param.DataType == DataType.IPAddress)
                    {
                        _sqlParam.SqlDbType = SqlDbType.VarChar;
                        _sqlParam.Size = 20;
                    }
					else if (_param.DataType == DataType.PhoneNo)
					{
						_sqlParam.SqlDbType = SqlDbType.VarChar;
						_sqlParam.Size = 20;
					}

					/*Add parameter...*/
					sqlComm.Parameters.Add(_sqlParam);
                }
            }
            LM.DefaultLogManagerFactory.LogManager.Debug("AddCommandParameters: Completed...");
        }

        private IDictionary<string, object> RetrieveCommandOutParams(SqlCommand sqlComm)
        {
            LM.DefaultLogManagerFactory.LogManager.Debug("RetrieveCommandOutParams: Started...");
            Dictionary<string, object> _returnData = new Dictionary<string, object>();
            if (sqlComm != null && sqlComm.Parameters.Count > 0)
            {
                foreach (SqlParameter _param in sqlComm.Parameters)
                {
                    if (_param.Direction == System.Data.ParameterDirection.Output || _param.Direction == System.Data.ParameterDirection.InputOutput)
                    {
                        _returnData.Add(_param.ParameterName, _param.Value);
                    }
                }
            }
            LM.DefaultLogManagerFactory.LogManager.Debug("RetrieveCommandOutParams: Completed...");
            LM.DefaultLogManagerFactory.LogManager.Info(string.Format(@"RetrieveCommandOutParams: Out params count - {0}", _returnData.Count()));
            return _returnData;
        }

        private Frameworks.Core.BaseException WrapDatabaseException(Exception ex)
        {
            LM.DefaultLogManagerFactory.LogManager.Debug("WrapDatabaseException: Started...");
            string _errorNumber = Frameworks.Core.BaseException.DefaultErrorNumber;
            if (ex is SqlException)
            {
                var _sqlEx = (ex as SqlException);
                _errorNumber = _sqlEx.ErrorCode.ToString();
            }
            var _baseEx = new Frameworks.Core.BaseException(_errorNumber, ex.Message, ex);
            LM.DefaultLogManagerFactory.LogManager.Debug("WrapDatabaseException: Completed...");
            return _baseEx;
        }

        #endregion

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if (_conn != null)
                    {
                        _conn.Dispose();
                        _conn = null;
                    }
                    if (_trans != null)
                    {
                        _trans.Dispose();
                        _trans = null;
                    }
                }
                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~SQLDatabaseAccess()
        {
            Dispose(false);
        }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
