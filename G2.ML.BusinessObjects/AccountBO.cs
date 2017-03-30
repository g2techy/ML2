using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2.ML.BusinessObjects
{
	public class ChangePwdBO : BaseBusinessObject
	{
		public int UserID { get; set; }
		public string NewPassword { get; set; }
		public string OldPassword { get; set; }
	}
    public class RegisterBO : BaseBusinessObject
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
    }

    public class LoggedInUserBO
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
    }

	public class LoginBO : BaseBusinessObject
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}
