using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2.Frameworks.MVC.PDF
{
	public class WkHtmltoPdfActionResult : ActionResultBase
	{
		protected override string Url { get; set; }
		protected override string ViewName { get; set; }
		protected override object Model { get; set; }

		public WkHtmltoPdfActionResult(string url)
		{
			Url = url;
		}

		public WkHtmltoPdfActionResult(string viewName, object model)
		{
			ViewName = viewName;
			Model = model;
		}

		protected override byte[] GetPDF(string htmlString)
		{
			return GetPDFInternal(null, htmlString);
		}
		protected override byte[] GetPDFFromUrl(string url)
		{
			return GetPDFInternal(url, null);
		}

		private byte[] GetPDFInternal(string url, string htmlString)
		{
			byte[] _bPDF = null;
			string _outputFileName = DownloadFileName;
			WkHtmltoPdfWrapper.PdfConvert.ConvertHtmlToPdf(
				new WkHtmltoPdfWrapper.PdfDocument { Url = url, Html = htmlString },
				new WkHtmltoPdfWrapper.PdfOutput { OutputFilePath = _outputFileName }
			);

			if (File.Exists(_outputFileName))
			{
				return File.ReadAllBytes(_outputFileName);
			}
			return _bPDF;
		}

		private string DownloadFileName
		{
			get
			{
				string _fileName =  Path.GetTempPath() + "/download_" + DateTime.Now.Ticks.ToString() + ".pdf";
				return _fileName;
			}
		}

	}
}