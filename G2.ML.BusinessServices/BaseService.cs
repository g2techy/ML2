using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2.ML.BusinessServices
{
    public abstract class BaseService : IService
    {
        private DAL.IDatabaseAccess _dbAccess = null;
        
        public DAL.IDatabaseAccess DatabaseAccess
        {
            get
            {
                if(_dbAccess == null)
                {
                    _dbAccess = (new DAL.DefaultDatabaseAccessFactory()).GetAppDatabase();
                }
                return _dbAccess;
            }
        }

        public static DAL.IDatabaseAccess NewDatabaseAccess
        {
            get
            {
                return (new DAL.DefaultDatabaseAccessFactory()).GetAppDatabase();
            }
        }

        public BaseService()
        {
            
        }
               
    }
}
