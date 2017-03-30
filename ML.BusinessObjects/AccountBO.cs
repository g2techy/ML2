using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2.ML.BusinessObjects
{
    public class RegisterBO
    {
        public string UserName { get; set; }
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
}
