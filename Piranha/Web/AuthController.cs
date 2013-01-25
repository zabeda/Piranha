using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using Piranha.Models;

namespace Piranha.Web
{
	/// <summary>
	/// Controller handling login/logout functionality.
	/// </summary>
    public class AuthController : Controller
    {
		/// <summary>
		/// Logs in the current user.
		/// </summary>
		public ActionResult Login() {
			string login  = Request["login"] ;
			string passwd = Request["password"] ;
			string returl = Request["returnurl"] ;
			string failurl = Request["failureurl"] ;
			bool persist = Request["remeberme"] == "1" ;

			if (!SysUser.LoginUser(login, passwd, persist) && !String.IsNullOrEmpty(failurl))
				return Redirect(failurl) ;

			if (!String.IsNullOrEmpty(returl))
				return Redirect(returl) ;
			return Redirect("~/") ;
		}

		/// <summary>
		/// Logs out the current user.
		/// </summary>
		public ActionResult Logout() {
			string returl = Request["returnurl"] ;

			FormsAuthentication.SignOut() ;
			Session.Clear() ;

			if (!String.IsNullOrEmpty(returl))
				return Redirect(returl) ;
			return Redirect("~/") ;
		}

		public ActionResult NewPassword() {
			string login = Request["login"];
			string returl = Request["returnurl"];
			string failurl = Request["failureurl"];

			var pwd = SysUserPassword.GetSingle("sysuser_login = @0", login);
			var user = SysUser.GetSingle("sysuser_login = @0", login);
			if (pwd != null) {

				if (WebPages.Hooks.Mail.SendPasswordMail != null) {
					pwd.Password = SysUserPassword.GeneratePassword();
					pwd.Save();

					WebPages.Hooks.Mail.SendPasswordMail(user, pwd.Password);

					if (!String.IsNullOrEmpty(returl))
						return Redirect(returl);
					return Redirect("~/");
				}

				
			}

			if (!String.IsNullOrEmpty(failurl))
				return Redirect(failurl);
			return Redirect("~/");
		} 
    }
}
