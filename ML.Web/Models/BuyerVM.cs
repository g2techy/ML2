using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace G2.ML.Web.Models
{

    public class BuyerVM : Infrastructure.Core.BaseVM
	{

        public int BuyerID { get; set; }
        public int ClientID { get; set; }

        [Required]
        [MaxLength(length: 20)]
        [Display(Name = "Buyer Code")]
        public string BuyerCode { get; set; }

        [Required]
        [MaxLength(length: 100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(length: 100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [MaxLength(length: 100)]
        [Display(Name = "Phone No#")]
        public string PhoneNo { get; set; }

        [MaxLength(length: 100)]
        [Display(Name = "Mobile No#")]
        public string MobileNo { get; set; }

        [Required]
        [Display(Name = "Buyer Types")]
        public List<string> SelectedBuyerTypes { get; set; }
		
        public List<BuyerTypeVM> BuyerTypeList { get; set; }

        public BuyerVM()
        {
            BuyerTypeList = new List<BuyerTypeVM>();
            SelectedBuyerTypes = new List<string>();
        }
        
    }

    public class BuyerTypeVM : Infrastructure.Core.BaseVM
	{
        public int BuyerTypeID { get; set; }
        public string BuyerTypeName { get; set; }
    }
}