using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Piranha.Areas.Manager.Controllers
{
	/// <summary>
	/// Controller for handling the different "popup" dialogs.
	/// </summary>
    public class DialogController : Controller
    {
		/// <summary>
		/// Gets the link dialog for the given site id.
		/// </summary>
		/// <param name="id">The site id</param>
		[Access(Function="ADMIN")]
		public ActionResult Link(string id) {
			return View(Models.LinkDialogModel.GetBySiteId(new Guid(id))) ;
		}
    }
}
