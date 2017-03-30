using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace G2.ML.Web.Models
{
    public class BuyerSearchVM : PagerVM
	{
        public int ClientID { get; set; }
        [Display(Name = "Buyer Code")]
        public string BuyerCode { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }

    public class BuyerSeachResultVM : Infrastructure.Core.BaseVM
	{
		public int ClientID { get; set; }
		public int RecordCount { get; set; }
        public int StartIndex { get; set; }
        public int PageSize { get; set; }

        public List<BuyerVM> BuyerList { get; set; }

        public BuyerSeachResultVM(int recCnt)
        {
            RecordCount = recCnt;
            BuyerList = new List<BuyerVM>();
        }
		public BuyerSeachResultVM() : this(0) { }
	}
}