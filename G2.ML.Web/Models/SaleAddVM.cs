﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace G2.ML.Web.Models
{
    public class SaleAddVM : BaseViewModel
    {
        public int ClientID { get; set; }
        public int SaleID { get; set; }

        [Display(Name = "Sale Date")]
        [Required]
        [DataType(DataType.Text)]
        public string SaleDate { get; set; }

        [Display(Name = "Saller")]
        [Required]
        public int SallerID { get; set; }

        [Display(Name = "Buyer")]
        [Required]
        public int BuyerID { get; set; }

        [Display(Name = "Due Days")]
        [Required]
		[RegularExpression(@"^\d+$", ErrorMessage = "Invalid due days")]
		public int DueDays { get; set; }

        [Display(Name = "Total Weight")]
        [Required]
        [DataType(DataType.Currency)]
		[RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid weight")]
		public float TotalWeight { get; set; }

        [Display(Name = "Rejection Weight")]
        [Required]
        [DataType(DataType.Currency)]
		[RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid rejection percentage")]
		public float RejectionWeight { get; set; }

        [Display(Name = "Selection Weight")]
        public float SelectionWeight { get; set; }

        [Display(Name = "Unit Price")]
        [Required]
        [DataType(DataType.Currency)]
		[RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")]
		public float UnitPrice { get; set; }

        [Display(Name = "Net Sale Amount")]
        public float NetSaleAmount { get; set; }
				
		[Display(Name = "Less (%)")]
		[Required]
		[DataType(DataType.Currency)]
		[Range(minimum: 0, maximum: 10)]
		[RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid less percentage")]
		public float LessPer { get; set; }

		public SelectList BuyerList { get; set; }
        public SelectList SallerList { get; set; }

        public int Status { get; set; }
		        
    }

	public class SalePrintVM : BaseViewModel
	{
		public SaleAddVM SaleDetails { get; set; }

		public List<SalePayment> PaymentList { get; set; }

		public List<SaleBrokerage> BrokerageList { get; set; }
	}
}