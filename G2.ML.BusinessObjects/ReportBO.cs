using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2.ML.BusinessObjects
{
    public class SaleStatusBO : BaseBusinessObject
    {
        public int SaleStatusID { get; set; }
        public string SaleStatusValue { get; set; }
    }

    public class SalesReportBO : BaseBusinessObject
    {
        public int ClientID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? SallerID { get; set; }
        public int? BuyerID { get; set; }
        public int? Status { get; set; }
        public int? DueDays { get; set; }
    }


    public class BrokerageReportBO : BaseBusinessObject
    {
        public int ClientID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? SallerID { get; set; }
        public int? BuyerID { get; set; }
        public int? Status { get; set; }
    }

	public class LoanReportBO : BaseBusinessObject
	{
		public int ClientID { get; set; }
		public string StartDate { get; set; }
		public string EndDate { get; set; }
		public int? BorrowerID { get; set; }
		public int? Status { get; set; }
	}

}
