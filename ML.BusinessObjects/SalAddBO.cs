
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2.ML.BusinessObjects
{
    public class SaleAddBO
    {
        public int ClientID { get; set; }
        public int SaleID { get; set; }
        public int DueDays { get; set; }
        public string SaleDate { get; set; }
        public int Saller { get; set; }
        public int Buyer { get; set; }
        public float TotalWeight { get; set; }
        public float RejectionWeight { get; set; }
        public float UnitPrice { get; set; }
        public int Status { get; set; }
    }

    public class Buyer
    {
        public int BuyerID { get; set; }
        public string BuyerName { get; set; }
    }
}
