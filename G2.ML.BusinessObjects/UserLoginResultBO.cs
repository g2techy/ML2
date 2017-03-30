using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2.ML.BusinessObjects
{
	public class UserLoginResultBO
	{
		public int UserID { get; set; }
		public int ClientID { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
