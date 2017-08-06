using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace G2.ML.Web.Models
{
	public class LoanReportVM
	{
		public int ClientID { get; set; }

		[Required]
		[Display(Name = "Start Date")]
		public string StartDate { get; set; }

		[Required]
		[Display(Name = "End Date")]
		public string EndDate { get; set; }

		[Display(Name = "Borrower")]
		public int? BorrowerID { get; set; }

		[Display(Name = "Status")]
		public int? Status { get; set; }

		public SelectList BorrowerList { get; set; }
		public SelectList StatusList { get; set; }
	}
}