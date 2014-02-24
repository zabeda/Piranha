using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Piranha.Areas.Manager.Controllers
{
	/// <summary>
	/// Controller that simply redirects to the startpage after authentication 
	/// tokens have been set correctly.
	/// </summary>
    public class ManagerStartController : ManagerController
    {
		/// <summary>
		/// Redirects to the current startpage for the user.
		/// </summary>
		[Access(Function = "ADMIN")]
        public ActionResult Index() {
			// Redirect after logon
			var startpage = WebPages.Manager.GetStartpage() ;
			return RedirectToAction(startpage.Action, startpage.Controller) ;
        }
    }
}
