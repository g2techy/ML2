using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2.ML.BusinessObjects
{
	public class ChartDataBO : BaseBusinessObject
	{
		public List<string> Categories { get; set; }
		public List<ChartSeriesBO> Series { get; set; }
		public ChartDataBO()
		{
			Categories = new List<string>();
			Series = new List<ChartSeriesBO>();
		}
	}

	public class ChartSeriesBO : BaseBusinessObject
	{
		public string Name { get; set; }
		public List<object> Data { get; set; }
		public string Stack { get; set; }
		public ChartSeriesBO()
		{
			Data = new List<object>();
		}
	}
}
