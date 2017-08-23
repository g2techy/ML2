using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2.Frameworks.MVC.PDF
{
	public class SelectPDFActionResult : ActionResultBase
	{
		protected override string Url { get; set; }
		protected override string ViewName { get; set; }
		protected override object Model { get; set; }

		public SelectPDFActionResult(string url)
		{
			Url = url;
		}

		public SelectPDFActionResult(string viewName, object model)
		{
			ViewName = viewName;
			Model = model;
		}

		protected override byte[] GetPDF(string htmlString)
		{
			byte[] _bPDF = null;

			SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
			SelectPdf.PdfDocument doc = converter.ConvertHtmlString(htmlString);

			// save pdf document
			_bPDF = doc.Save();

			// close pdf document
			doc.Close();

			return _bPDF;
		}

		protected override byte[] GetPDFFromUrl(string url)
		{
			byte[] _bPDF = null;

			SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
			SelectPdf.PdfDocument doc = converter.ConvertUrl(url);

			// save pdf document
			_bPDF = doc.Save();

			// close pdf document
			doc.Close();

			return _bPDF;
		}

	}
}
