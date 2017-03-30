using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G2.ML.Web.Models
{
    public class SaleSearchResultVM : BaseViewModel
    {
		public int ClientID { get; set; }
		public int RecordCount { get; private set; }
        public int StartIndex { get; set; }
        public int PageSize { get; set; }

        public List<SaleVM> SalesList { get; set; }

        public SaleSearchResultVM(int recCnt)
        {
            RecordCount = recCnt;
            SalesList = new List<SaleVM>();
        }
		public SaleSearchResultVM() : this(0) { }
	}

    public class SaleVM : Infrastructure.Core.BaseVM
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
    
}