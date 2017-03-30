using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2.ML.BusinessObjects
{
    public class BuyerBO
    {
        public int BuyerID { get; set; }
        public int ClientID { get; set; }
        public string BuyerCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string SelectedBuyerTypes { get; set; }
        public string BuyerTypeNames { get; set; }
    }
}
