using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2.ML.BusinessObjects
{
    public class SalePaymentBO
    {
        public int PayID { get; set; }
        public int SaleID { get; set; }
        public DateTime PayDate { get; set; }
        public float PayAmount { get; set; }
        public string CourierFrom { get; set; }
        public string CourierTo { get; set; }
    }
}
