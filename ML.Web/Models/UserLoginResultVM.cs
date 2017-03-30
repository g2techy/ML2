using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G2.ML.Web.Models
{
	public class UserLoginResultVM : Infrastructure.Core.BaseVM
	{
		public int UserID { get; set; }
		public int ClientID { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}