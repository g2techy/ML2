using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace G2.ML.Web.Infrastructure.Utilities
{
	public enum PDFExportComponent
	{
		SelectPDF,
		iTextSharp
	}

	public class PDF
	{
		public static PDFExportComponent? CurrentPDFExportComponent { get; set; }

		public static ActionResult ExportToPdf(string url)
		{
			ActionResult _pdfResult = null;
			if (!CurrentPDFExportComponent.HasValue)
			{
				throw new ArgumentOutOfRangeException("PDF export component is not set.");
			}
			if (CurrentPDFExportComponent.Value == PDFExportComponent.SelectPDF)
			{
				_pdfResult = new Frameworks.MVC.PDF.SelectPDFActionResult(url);
			}
			return _pdfResult;
		}

		public static ActionResult ExportToPdf(string viewName, object model)
		{
			ActionResult _pdfResult = null;
			if (!CurrentPDFExportComponent.HasValue)
			{
				throw new ArgumentOutOfRangeException("PDF export component is not set.");
			}
			if (CurrentPDFExportComponent.Value == PDFExportComponent.SelectPDF)
			{
				_pdfResult = new Frameworks.MVC.PDF.SelectPDFActionResult(viewName, model);
			}
			return _pdfResult;
		}
	}
}