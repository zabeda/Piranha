using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Piranha.Models.Manager.TemplateModels;

namespace Piranha.Areas.Manager.Controllers
{
    public class TemplateController : ManagerController
    {
		/// <summary>
		/// Opens the insert or edit view for the template depending on
		/// weather a template id was passed to the action.
		/// </summary>
		/// <param name="id">The template id</param>
		public ActionResult Page(string id = "") {
			PageEditModel m = new PageEditModel() ; 
			
			if (id != "") {
				m = PageEditModel.GetById(new Guid(id)) ;
				ViewBag.Title = Piranha.Resources.Page.EditTypeTitleExisting ;
			} else {
				ViewBag.Title = Piranha.Resources.Page.EditTypeTitleNew ;
			}
			return View("PageEdit", m) ;
		}

		/// <summary>
		/// Saves the current template.
		/// </summary>
		/// <param name="m">The model</param>
		[HttpPost(), ValidateInput(false)]
		public ActionResult Page(PageEditModel m) {
			if (ModelState.IsValid) {
				if (m.SaveAll()) {
					ModelState.Clear() ;
					ViewBag.Title = Piranha.Resources.Page.EditTypeTitleExisting ;
					ViewBag.Message = Piranha.Resources.Page.MessageTypeSaved ;
				} else ViewBag.Message = Piranha.Resources.Page.MessageTypeNotSaved ;
			} else {
				if (m.Template.IsNew)
					ViewBag.Title = Piranha.Resources.Page.EditTypeTitleNew ;
				else ViewBag.Title = Piranha.Resources.Page.EditTypeTitleExisting ;
			} 
			return View("PageEdit", m) ;
		}

		/// <summary>
		/// Opens the insert or edit view for the template depending on
		/// weather a template id was passed to the action.
		/// </summary>
		/// <param name="id">The template id</param>
		public ActionResult Post(string id = "") {
			PostEditModel m = new PostEditModel() ; 
			
			if (id != "") {
				m = PostEditModel.GetById(new Guid(id)) ;
				ViewBag.Title = Piranha.Resources.Post.EditTypeTitleExisting ;
			} else {
				ViewBag.Title = Piranha.Resources.Post.EditTypeTitleNew ;
			}
			return View("PostEdit", m) ;
		}

		/// <summary>
		/// Saves the current template.
		/// </summary>
		/// <param name="m">The model</param>
		[HttpPost(), ValidateInput(false)]
		public ActionResult Post(PostEditModel m) {
			ViewBag.Title = Piranha.Resources.Post.EditTypeTitleNew ;

			if (ModelState.IsValid) {
				if (m.SaveAll()) {
					ModelState.Clear() ;
					ViewBag.Title = Piranha.Resources.Post.EditTypeTitleExisting ;
					ViewBag.Message = Piranha.Resources.Post.MessageTypeSaved ;
				} else ViewBag.Message = Piranha.Resources.Post.MessageTypeNotSaved ;
			}
			return View("PostEdit", m) ;
		}

		/// <summary>
		/// Deletes the specified page template.
		/// </summary>
		/// <param name="id">The template id</param>
		public ActionResult DeletePage(string id) {
			PageEditModel pm = PageEditModel.GetById(new Guid(id)) ;

			if (pm.DeleteAll())
				ViewBag.Message = Piranha.Resources.Page.MessageTypeDeleted ;
			else ViewBag.Message = Piranha.Resources.Page.MessageTypeNotDeleted ;
			return RedirectToAction("Index", "Page") ;
		}

		/// <summary>
		/// Deletes the specified post template.
		/// </summary>
		/// <param name="id">The template id</param>
		public ActionResult DeletePost(string id) {
			PostEditModel pm = PostEditModel.GetById(new Guid(id)) ;

			if (pm.DeleteAll())
				ViewBag.Message = Piranha.Resources.Post.MessageTypeDeleted ;
			else ViewBag.Message = Piranha.Resources.Post.MessageTypeNotDeleted ;
			return RedirectToAction("Index", "Post") ;
		}
    }
}
