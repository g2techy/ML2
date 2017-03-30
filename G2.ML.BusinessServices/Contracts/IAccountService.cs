using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BO = G2.ML.BusinessObjects;

namespace G2.ML.BusinessServices.Contracts
{
    public interface IAccountService
    {
        int RegisterUser(BO.RegisterBO bo);
		BO.UserLoginResultBO VerifyLoginCreds(BO.LoginBO loginBO);
		bool ChangePassword(BO.ChangePwdBO changePwd);
    }
}
