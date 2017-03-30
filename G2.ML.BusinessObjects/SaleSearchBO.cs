using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2.ML.BusinessObjects
{
    public class SaleBO : BaseBusinessObject
    {
        public int SaleID { get; set; }
        public DateTime SaleDate { get; set; }
        public string Saller { get; set; }
        public string Buyer { get; set; }
        public float TotalWeight { get; set; }
        public float RejectionWt { get; set; }
        public float SelectionWt { get; set; }
        public float UnitPrice { get; set; }
        public float NetSaleAmount { get; set; }
        public int DueDays { get; set; }
        public string TotalBrokerage { get; set; }
        public float TotalPayAmount { get; set; }
        public DateTime PayDate { get; set; }
        public string Status { get; set; }
        public string RefNo { get; set; }
    }

    public class SaleSearchBO : PagerBO
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int SallerID { get; set; }
        public int BuyerID { get; set; }
        public int ClientID { get; set; }
        public string RefNo { get; set; }
    }

    public class SaleSearchResultBO : BaseBusinessObject
	{
        public int ClientID { get; set; }
        public int RecordCount { get; private set; }
		public int StartIndex { get; set; }
		public int PageSize { get; set; }

		public List<SaleBO> SalesList { get; set; }

        public SaleSearchResultBO(int recCnt)
        {
            RecordCount = recCnt;
            SalesList = new List<SaleBO>();
        }
		public SaleSearchResultBO() : this(0) { }

	}

}
