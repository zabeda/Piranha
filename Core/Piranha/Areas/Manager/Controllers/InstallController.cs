/*
 * Copyright (c) 2011-2015 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha
 * 
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Security;

using Piranha.Data;
using Piranha.Models;

namespace Piranha.Areas.Manager.Controllers
{
	/// <summary>
	/// Post model for installation.
	/// </summary>
	public class InstallModel
	{
		/// <summary>
		/// Gets/sets the login of the new admin account.
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(Piranha.Resources.Settings), ErrorMessageResourceName = "LoginRequired")]
		public string UserLogin { get; set; }

		/// <summary>
		/// Gets/sets the email address of the new admin account.
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(Piranha.Resources.Settings), ErrorMessageResourceName = "EmailRequired")]
		public string UserEmail { get; set; }

		/// <summary>
		/// Gets/sets the password of the new admin account.
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(Piranha.Resources.Settings), ErrorMessageResourceName = "PasswordRequired")]
		public string Password { get; set; }

		/// <summary>
		/// Gets/sets the password confirmation.
		/// </summary>
		[System.ComponentModel.DataAnnotations.Compare("Password",
			ErrorMessageResourceType = typeof(Piranha.Resources.Settings),
			ErrorMessageResourceName = "PasswordConfirmError")]
		public string PasswordConfirm { get; set; }

		/// <summary>
		/// Gets/sets the current installation type.
		/// </summary>
		public string InstallType { get; set; }
	}

	/// <summary>
	/// Login controller for the manager interface.
	/// </summary>
	public class InstallController : Controller
	{
		/// <summary>
		/// Checks for the piranha connection string and
		/// if the database is up to date.
		/// </summary>
		public ActionResult Index() {
			// Check for no database-config
			if (ConfigurationManager.ConnectionStrings["piranha"] == null)
				return RedirectToAction("welcome");

			// Check for existing installation.
			using (var db = new Db()) {
				if (db.Exists) {
					if (!db.IsCompatible)
						return RedirectToAction("update");
					return RedirectToAction("index", "account");
				}
			}
			return View("Index");
		}

		/// <summary>
		/// Shows the update page.
		/// </summary>
		public ActionResult Update() {
			using (var db = new Db()) {
				if (db.Exists && !db.IsCompatible)
					return View("Update");
			}
			return RedirectToAction("index", "account");
		}

		/// <summary>
		/// Shows the welcome screen.
		/// </summary>
		public ActionResult Welcome() {
			return View();
		}

		/// <summary>
		/// Logins in the specified user and starts the update.
		/// </summary>
		[HttpPost()]
		public ActionResult RunUpdate(LoginModel m) {
			// Authenticate the user
			if (ModelState.IsValid) {
				SysUser user = SysUser.Authenticate(m.Login, m.Password);
				if (user != null) {
					FormsAuthentication.SetAuthCookie(user.Id.ToString(), m.RememberMe);
					HttpContext.Session[PiranhaApp.USER] = user;
					return RedirectToAction("ExecuteUpdate");
				} else {
					ViewBag.Message = @Piranha.Resources.Account.MessageLoginFailed;
					ViewBag.MessageCss = "error";
					return Update();
				}
			} else {
				ViewBag.Message = @Piranha.Resources.Account.MessageLoginEmptyFields;
				ViewBag.MessageCss = "";
				return Update();
			}
		}

		/// <summary>
		/// Executes the available database scripts on the database.
		/// </summary>
		[HttpGet()]
		public ActionResult ExecuteUpdate() {
			if (Application.Current.UserProvider.IsAuthenticated && User.HasAccess("ADMIN")) {
				using (var db = new Db()) {
					db.Migrate();
					Seed.SeedRequired(db);
					return RedirectToAction("index", "account");
				}
			} else return RedirectToAction("update");
		}

		/// <summary>
		/// Creates a new site installation.
		/// </summary>
		/// <param name="m">The model</param>
		[HttpPost()]
		public ActionResult Create(InstallModel m) {
			using (var db = new Db()) {
				// Install & seed data
				db.Migrate();
				Seed.SeedRequired(db);
				Seed.SeedDefault(db);

				// Create user account
				db.Users.Add(new User() {
					GroupId = Config.SysAdminGroupId,
					Username = m.UserLogin,
					Password = SysUser.Encrypt(m.Password),
					Email = m.UserEmail,
					CreatedById = Config.SysUserId,
					UpdatedById = Config.SysUserId
				});
				db.SaveChanges();
			}
			return RedirectToAction("index", "account");
		}
	}
}
