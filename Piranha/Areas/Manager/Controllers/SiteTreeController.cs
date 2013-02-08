using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Piranha.Areas.Manager.Models;

namespace Piranha.Areas.Manager.Controllers
{
    public class SiteTreeController : ManagerController
    {
		/// <summary>
		/// Gets the list view for the site tree.
		/// </summary>
        public ActionResult Index() {
            return View(SiteTreeListModel.Get());
        }
    }
}
