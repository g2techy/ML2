using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace G2.ML.Web.Models
{
	public class LoanAddVM : BaseViewModel 
	{
		public int ClientID { get; set; }
		public int LoanID { get; set; }

		[Display(Name = "Start Date")]
		[Required]
		[DataType(DataType.Text)]
		public string StartDate { get; set; }

		[Display(Name = "End Date")]
		[DataType(DataType.Text)]
		public string EndDate { get; set; }

		[Display(Name = "Borrower")]
		[Required]
		public int BorrowerID { get; set; }

		[Display(Name = "Principal Amount")]
		[Required]
		[DataType(DataType.Currency)]
		[RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid amount")]
		public float PrincipalAmount { get; set; }

		[Display(Name = "Monthly Interest Rate")]
		[Required]
		[DataType(DataType.Currency)]
		[RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid interest rate")]
		public float MonthlyInterest { get; set; }

		[Display(Name = "Comments")]
		public string Comments { get; set; }

		public int Status { get; set; }
		public int RefNo { get; set; }

		public SelectList BorrowerList { get; set; }
	}

	public class LoanSearchVM : PagerVM
	{
		[Display(Name = "Start Date")]
		public string StartDate { get; set; }

		[Display(Name = "End Date")]
		public string EndDate { get; set; }

		[Display(Name = "Borrower")]
		public int BorrowerID { get; set; }

		[Display(Name = "Reference No")]
		public string RefNo { get; set; }

		public SelectList BorrowerList { get; set; }

		public int ClientID { get; set; }

	}

	public class LoanSearchResultVM : BaseViewModel
	{
		public int ClientID { get; set; }
		public int RecordCount { get; private set; }
		public int StartIndex { get; set; }
		public int PageSize { get; set; }

		public List<LoanVM> LoanList { get; set; }

		public LoanSearchResultVM(int recCnt)
		{
			RecordCount = recCnt;
			LoanList = new List<LoanVM>();
		}
		public LoanSearchResultVM() : this(0) { }
	}

	public class LoanVM : Infrastructure.Core.BaseVM
	{
		public int LoanID { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Borrower { get; set; }
		public float PrincipalAmount { get; set; }
		public float MonthlyInterest { get; set; }
		public float TotalPayAmount { get; set; }
		public DateTime PayDate { get; set; }
		public int StatusID { get; set; }
		public string StatusName { get; set; }
		public string RefNo { get; set; }
	}

	public class LoanPaymentVM : BaseViewModel
	{
		public LoanPayment Payment { get; set; }

		public List<LoanPayment> PaymentList { get; set; }

		public LoanPaymentVM()
		{
			PaymentList = new List<LoanPayment>();
		}
	}


	public class LoanPayment : Infrastructure.Core.BaseVM
	{
		public int LoanID { get; set; }

		public int LoanPayID { get; set; }

		[DataType(DataType.Date)]
		[Required]
		public DateTime PayDate { get; set; }

		[DataType(DataType.Currency)]
		[Required]
		[RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid amount")]
		public float PayAmount { get; set; }

		[Required]
		public int PayType { get; set; }

		public string PayTypeName { get; set; }

		[Required]
		public string PayComments { get; set; }

		public float PrincipalPaid { get; set; }
		public float InterestPaid { get; set; }
	}

	public class LoanCalcInterestVM : Infrastructure.Core.BaseVM
	{
		public float? PayAmount { get; set; }
		public DateTime? PayDate { get; set; }
		public float DailyRate { get; set; }
		public int IntForDays { get; set; }
		public float IntOnAmount { get; set; }
		public float CalcIntAmount { get; set; }
		public float TotalIntPaid { get; set; }
	}
}