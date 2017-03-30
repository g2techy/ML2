using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G2.ML.Web.Models
{
	public class PagerVM : Infrastructure.Core.BaseVM
	{
		public int StartIndex { get; set; }
		public int PageSize { get; set; }
	}
}