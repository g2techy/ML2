using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace G2.Frameworks.MVC.ActionResults
{
	public class BinaryContentResult : ActionResult
	{
		private string _contentType;
		private byte[] _contentBytes;

		public BinaryContentResult(Byte[] content, string contentType)
		{
			_contentBytes = content;
			_contentType = contentType;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			var _response = context.HttpContext.Response;
			_response.Clear();
			_response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
			_response.ContentType = _contentType;

			using (MemoryStream _ms = new MemoryStream(_contentBytes))
			{
				_ms.WriteTo(_response.OutputStream);
			}
		}
	}
}
