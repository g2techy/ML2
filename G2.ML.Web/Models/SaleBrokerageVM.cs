using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace G2.ML.Web.Models
{
    public class SaleBrokerageVM : Infrastructure.Core.BaseVM
	{
        public int SaleID { get; set; }

        [Required]
        public int BrokerID { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        [Range(typeof(float),"0.01","100.00")]
		[RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid brokerage")]
		public float Brokerage { get; set; }

        public SelectList BrokerList { get; set; }

        public List<SaleBrokerage> BrokerageList { get; set; }

        public SaleBrokerageVM()
        {
            BrokerageList = new List<SaleBrokerage>();
        }
    }

    public class SaleBrokerage : Infrastructure.Core.BaseVM
    {
        public int BDID { get; set; }
		public int SaleID { get; set; }
		public int BrokerID { get; set; }
		public string BrokerName { get; set; }
        public float Brokerage { get; set; }
        public float BrokerageAmount { get; set; }
    }
}