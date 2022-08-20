using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BO = G2.ML.BusinessObjects;
using BS = G2.ML.BusinessServices;

namespace G2.ML.Web.Controllers
{
	public class AccountController : Infrastructure.Core.BaseController
	{
		#region DI settings

		private readonly BS.Contracts.IAccountService _accountService;
		public AccountController(BS.Contracts.IAccountService accountService)
		{
			_accountService = accountService;
		}

		#endregion

		#region Action Methods

		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Login(Models.LoginVM model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var _bo = _accountService.VerifyLoginCreds(Infrastructure.BOVMMapper.Map<Models.LoginVM, BO.LoginBO>(model));
					if (_bo != null)
					{
						Infrastructure.Web.SessionManager.SetLoggedInUserSession(Infrastructure.BOVMMapper.Map<BO.UserLoginResultBO, Models.UserLoginResultVM>(_bo));
						return Redirect(Infrastructure.Web.Common.DashboardUrl);
					}
				}
				catch(Exception ex)
				{
					base.LogException(ex);
				}
			}
			else
			{
				// If we got this far, something failed, redisplay form
				ModelState.AddModelError("", "Please enter user name and password.");
			}
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOff()
		{
			Infrastructure.Web.SessionManager.ClearSession();
			return RedirectToAction("Index", "Home");
		}

		[Infrastructure.Filters.AdminAuth]
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Infrastructure.Filters.AdminAuth]
		public ActionResult Register(Models.RegisterVM model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					int _userID = _accountService.RegisterUser(Infrastructure.BOVMMapper.Map<Models.RegisterVM, BO.RegisterBO>(model));
					if (_userID > 0)
					{
						return Redirect(Infrastructure.Web.Common.DashboardUrl);
					}
					else
					{
						ModelState.AddModelError("", "Unhandled error in user registration.");
					}
				}
				catch (Exception ex)
				{
					base.LogException(ex);
				}
			}
			return View(model);
		}

		public ActionResult Manage()
		{
			return View(new Models.ChangePwdVM()
			{
				UserID = Infrastructure.Web.SessionManager.CurrentLoggedInUser.UserID
			});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Manage(Models.ChangePwdVM model)
		{
			if (ModelState.IsValid)
			{
				// ChangePassword will throw an exception rather than return false in certain failure scenarios.
				bool changePasswordSucceeded;
				try
				{
					changePasswordSucceeded = _accountService.ChangePassword(Infrastructure.BOVMMapper.Map<Models.ChangePwdVM, BO.ChangePwdBO>(model));
				}
				catch (Exception)
				{
					changePasswordSucceeded = false;
				}

				if (changePasswordSucceeded)
				{
					return RedirectToAction("Manage", new { Message = "Password has been changed successfully!" });
				}
				else
				{
					ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
				}
			}
			return View(model);
		}

		public ActionResult DatabaseBackup()
		{
			return View();
		}

		[HttpPost]
		public ActionResult DatabaseBackupPost()
		{
			String databaseName = Infrastructure.Web.Common.GetWebAppSettingParam("DatabaseName");
			String backupFilePath = String.Format(Infrastructure.Web.Common.GetWebAppSettingParam("DatabaseBackupPath"), databaseName + "_" + DateTime.Today.ToString("dd-MMM-yyyy-HH-mm-ss"));

			Models.DatabaseBackupVM model = new Models.DatabaseBackupVM()
			{
				DatabaseName = databaseName,
				BackupFilePath = backupFilePath
			};

			bool isSuccess = _accountService.DatabaseBackUp(Infrastructure.BOVMMapper.Map<Models.DatabaseBackupVM, BO.DatabaseBackupBO>(model));
			if (isSuccess)
			{
				TempData["BackupFilePath"] = model.BackupFilePath;
				ViewBag.BackupFilePath = model.BackupFilePath;
			}
			return RedirectToAction("DatabaseBackup");
		}

		#endregion

	}
}