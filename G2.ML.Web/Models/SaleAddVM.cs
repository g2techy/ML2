using System;
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
        public int DueDays { get; set; }

        [Display(Name = "Total Weight")]
        [Required]
        [DataType(DataType.Currency)]
        public float TotalWeight { get; set; }

        [Display(Name = "Rejection (%)")]
        [Required]
        [DataType(DataType.Currency)]
        [Range(minimum:0,maximum:99)]
        public float RejectionWeight { get; set; }

        [Display(Name = "Selection Weight")]
        public float SelectionWeight { get; set; }

        [Display(Name = "Unit Price")]
        [Required]
        [DataType(DataType.Currency)]
        public float UnitPrice { get; set; }

        [Display(Name = "Net Sale Amount")]
        public float NetSaleAmount { get; set; }

        public SelectList BuyerList { get; set; }
        public SelectList SallerList { get; set; }

        public int Status { get; set; }
		        
    }
        
}