using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ClosedXML.Excel;
using System.IO;

namespace G2.ML.Web.Infrastructure.Core
{

    public enum ExportForamt
    {
        Xls,
        Xlsx,
        CSV,
        XML,
        HTML
    }

    public class ExportResult : ActionResult
    {
        private DataTable _data;
        private ExportForamt _exportFormat = ExportForamt.Xlsx;
        private string _fileName = "data.xlsx";

        public ExportResult(DataTable data, string fileName)
            : this(data, ExportForamt.Xlsx, fileName)
        {
        }

        public ExportResult(DataTable data, ExportForamt exportFormat, string fileName)
        {
            _data = data;
            _exportFormat = exportFormat;
            _fileName = fileName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(_data);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                HttpResponseBase _response = context.HttpContext.Response;
                _response.Clear();
                _response.Buffer = true;
                _response.Charset = "";
                _response.ContentType = GetContentType();
                _response.AddHeader("content-disposition", "attachment;filename=" + _fileName);

                _response.Cookies.Add(new HttpCookie("fileDownload", "true") { Path = "/" });

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(_response.OutputStream);
                    _response.Flush();
                    _response.End();
                }
                
            }
        }

        private string GetContentType()
        {
            string _contentType = "";
            switch (_exportFormat)
            {
                case ExportForamt.Xlsx:
                    _contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case ExportForamt.Xls:
                    _contentType = "application/vnd.ms-excel";
                    break;
                case ExportForamt.CSV:
                    _contentType = "text/csv";
                    break;
                case ExportForamt.XML:
                    _contentType = "application/xml";  /*_contentType = "text/xml"*/
                    break;
                case ExportForamt.HTML:
                    _contentType = "text/html";
                    break;
                default:
                    _contentType = "text/plain";
                    break;
            }
            return _contentType;
        }
    
    }
}