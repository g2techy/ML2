using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace G2.ML.Web.Infrastructure.Filters
{
	public class AdminAuthAttribute : AuthenticationAttribute
	{
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			if (base.AuthorizeCore(httpContext))
			{
				return Web.SessionManager.CurrentLoggedInUser.UserName.Equals("Admin", StringComparison.InvariantCultureIgnoreCase);
			}
			return false;
		}

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			throw new Frameworks.Core.BaseException("E200000", "You don't have privileges to access page/resouce. Kindly contact Admin for detailed information.");
		}
	}
}