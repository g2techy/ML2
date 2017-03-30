using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2.ML.BusinessObjects
{
    public class SaleBrokerageBO : BaseBusinessObject
    {
        public int BDID { get; set; }
        public int SaleID { get; set; }
        public int BrokerID { get; set; }
        public string BrokerName { get; set; }
        public float Brokerage { get; set; }
        public float BrokerageAmount { get; set; }
    }
}
