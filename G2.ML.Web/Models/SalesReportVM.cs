using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace G2.ML.Web.Models
{
    public class SalesReportVM : Infrastructure.Core.BaseVM
    {
        public int ClientID { get; set; }

        [Display(Name = "Start Date")]
        public string StartDate { get; set; }

        [Display(Name = "End Date")]
        public string EndDate { get; set; }

        [Display(Name = "Saller")]
        public int? SallerID { get; set; }

        [Display(Name = "Buyer")]
        public int? BuyerID { get; set; }

        [Display(Name = "Status")]
        public int? Status { get; set; }

        [Display(Name = "Due Days")]
        public int? DueDays { get; set; }

        public SelectList BuyerList { get; set; }
        public SelectList SallerList { get; set; }
        public SelectList StatusList { get; set; }
		        
    }
}