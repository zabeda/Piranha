using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using Piranha.Data;
using Piranha.Data.Updates;
using Piranha.Models;

namespace Piranha.Areas.Manager.Controllers
{
	public class InstallModel {
		[Required(ErrorMessageResourceType=typeof(Piranha.Resources.Settings), ErrorMessageResourceName="LoginRequired")]
		public string UserLogin { get ; set ; }

		[Required(ErrorMessageResourceType=typeof(Piranha.Resources.Settings), ErrorMessageResourceName="EmailRequired")]
		public string UserEmail { get ; set ; }

		[Required(ErrorMessageResourceType=typeof(Piranha.Resources.Settings), ErrorMessageResourceName="PasswordRequired")]
		public string Password { get ; set ; }

		[System.ComponentModel.DataAnnotations.Compare("Password", 
			ErrorMessageResourceType=typeof(Piranha.Resources.Settings), 
			ErrorMessageResourceName="PasswordConfirmError")]
		public string PasswordConfirm { get ; set ; }

		public string InstallType { get ; set ; }
	}

	/// <summary>
	/// Login controller for the manager interface.
	/// </summary>
    public class InstallController : Controller
    {
		/// <summary>
		/// Default action
		/// </summary>
        public ActionResult Index() {
			// Check for no database-config
			if (ConfigurationManager.ConnectionStrings["piranha"] == null)
				return RedirectToAction("welcome");

			// Check for existing installation.
			try {
				if (Data.Database.InstalledVersion < Data.Database.CurrentVersion)
					return RedirectToAction("update", "install") ;
				return RedirectToAction("index", "account") ;
			} catch (Exception ex) {
                if (Config.ShowDBErrors)
                {
                    throw;
                }
            }
			return View("Index");
        }

		/// <summary>
		/// Shows the update page.
		/// </summary>
		public ActionResult Update() {
			if (Data.Database.InstalledVersion < Data.Database.CurrentVersion)
				return View("Update") ;
			return RedirectToAction("index", "account") ;
		}

		/// <summary>
		/// Shows the welcome screen.
		/// </summary>
		public ActionResult Welcome() {
			return View();
		}

		/// <summary>
		/// Updates the database.
		/// </summary>
		[HttpPost()]
		public ActionResult RunUpdate(LoginModel m) {
			// Authenticate the user
			if (ModelState.IsValid) {
				SysUser user = SysUser.Authenticate(m.Login, m.Password) ;
				if (user != null) {
					FormsAuthentication.SetAuthCookie(user.Id.ToString(), m.RememberMe) ;
					HttpContext.Session[PiranhaApp.USER] = user ;
					return RedirectToAction("ExecuteUpdate") ;
				} else {
					ViewBag.Message = @Piranha.Resources.Account.MessageLoginFailed ;
					ViewBag.MessageCss = "error" ;
					return Update() ;
				}
			} else {
				ViewBag.Message = @Piranha.Resources.Account.MessageLoginEmptyFields ;
				ViewBag.MessageCss = "" ;
				return Update() ;
			}
		}

		[HttpGet()]
		public ActionResult ExecuteUpdate() {
			if (App.Instance.UserProvider.IsAuthenticated && User.HasAccess("ADMIN")) {
				Database.Update();
				return RedirectToAction("index", "account") ;
			} else return RedirectToAction("update") ;
		}

		/// <summary>
		/// Creates a new site installation.
		/// </summary>
		/// <param name="m">The model</param>
		[HttpPost()]
		public ActionResult Create(InstallModel m) {
			if (ModelState.IsValid) {
				Database.Install(m.UserLogin, m.Password, m.UserEmail);
				return RedirectToAction("index", "account") ;
			}
			return Index() ;
		}
    }
}
