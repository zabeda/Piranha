using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Piranha.Models.Manager.PageModels;

namespace Piranha.Areas.Manager.Controllers
{
    public class PageController : ManagerController
    {
		/// <summary>
		/// Default controller. Gets the page list.
		/// </summary>
        public ActionResult Index() {
			return View("Index", ListModel.Get()) ;
        }

		/// <summary>
		/// Opens the edit view for the selected page.
		/// </summary>
		/// <param name="id">The page id</param>
		public ActionResult Edit(string id) {
			EditModel pm = EditModel.GetById(new Guid(id)) ;

			ViewBag.Title = Piranha.Resources.Page.EditTitleExisting ;

			return View("Edit", pm) ;
		}

		/// <summary>
		/// Saves the currently edited page.
		/// </summary>
		/// <param name="pm">The page model</param>
		[HttpPost(), ValidateInput(false)]
		public ActionResult Edit(bool draft, EditModel pm) {
			if (ModelState.IsValid) {
				try {
					if (pm.SaveAll(draft)) {
						ModelState.Clear() ;
						if (!draft)
							ViewBag.Message = Piranha.Resources.Page.MessagePublished ;
						else ViewBag.Message = Piranha.Resources.Page.MessageSaved ;
					} else ViewBag.Message = Piranha.Resources.Page.MessageNotSaved ;
				} catch (Exception e) {
					ViewBag.Message = e.ToString() ;
				}
			}
			pm.Refresh();

			if (pm.Page.IsNew)
				ViewBag.Title = Piranha.Resources.Page.EditTitleNew + pm.Template.Name.Singular.ToLower() ;
			else ViewBag.Title = Piranha.Resources.Page.EditTitleExisting ;

			return View("Edit", pm) ;
		}

		/// <summary>
		/// Creates a new page.
		/// </summary>
		/// <param name="im">The insert model</param>
		[HttpPost()]
		public ActionResult Insert(InsertModel im) {
			EditModel pm = EditModel.CreateByTemplateAndPosition(im.TemplateId, im.ParentId, im.Seqno) ;
			ViewBag.Title = Piranha.Resources.Page.EditTitleNew + pm.Template.Name.Singular.ToLower() ;

			return View("Edit", pm) ;
		}

		/// <summary>
		/// Deletes the page specified by the given id.
		/// </summary>
		/// <param name="id">The page id</param>
		public ActionResult Delete(string id) {
			EditModel pm = EditModel.GetById(new Guid(id), true) ;

			try {
				if (pm.DeleteAll())
					ViewBag.Message = Piranha.Resources.Page.MessageDeleted ;
				else ViewBag.Message = Piranha.Resources.Page.MessageNotDeleted ;
			} catch (Exception e) {
				ViewBag.Message = e.ToString() ;
			}

			return Index() ;
		}

		/// <summary>
		/// Reverts to latest published verison.
		/// </summary>
		/// <param name="id">The page id.</param>
		public ActionResult Revert(string id) {
			EditModel.Revert(new Guid(id)) ;

			ViewBag.Message = Piranha.Resources.Page.MessageReverted ;

			return Edit(id) ;
		}
    }
}
