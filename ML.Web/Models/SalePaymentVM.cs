using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace G2.ML.Web.Models
{
    public class SalePaymentVM : BaseViewModel
    {
        public SalePayment Payment { get; set; }

        public List<SalePayment> PaymentList { get; set; }

        public SalePaymentVM()
        {
            PaymentList = new List<SalePayment>();
        }
		        
    }


    public class SalePayment
    {
        public int SaleID { get; set; }

        public int PayID { get; set; }
        
        [DataType(DataType.Date)]
        [Required]
        public DateTime PayDate { get; set; }
        
        [DataType(DataType.Currency)]
        [Required]
        public float PayAmount { get; set; }

        [Required]
        public string CourierFrom { get; set; }

        [Required]
        public string CourierTo { get; set; }
    }

}