using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Piranha.Models.Manager.ContentModels;

namespace Piranha.Areas.Manager.Controllers
{
    public class ContentController : ManagerController
    {
		/// <summary>
		/// Gets the list view.
		/// </summary>
        public ActionResult Index() {
            return View("Index", ListModel.Get());
        }

		/// <summary>
		/// Gets the popup list.
		/// </summary>
		public ActionResult Popup() {
			return View("Popup", PopupModel.Get()) ;
		}

		/// <summary>
		/// Gets the files list.
		/// </summary>
		/// <returns></returns>
		public ActionResult Uploads() {
			return View("Uploads", UploadModel.Get()) ;
		}

		/// <summary>
		/// Edits or inserts a new content model.
		/// </summary>
		/// <param name="id">The id of the content</param>
		public ActionResult Edit(string id) {
			if (!String.IsNullOrEmpty(id)) {
				EditModel m = EditModel.GetById(new Guid(id)) ;
				if (m.Content.IsImage)
					ViewBag.Title = Piranha.Resources.Content.EditTitleExistingImage ;
				else ViewBag.Title = Piranha.Resources.Content.EditTitleExistingDocument ;

				return View("Edit", m) ;
			} else {
				ViewBag.Title = Piranha.Resources.Content.EditTitleNew ;
				return View("Edit", new EditModel()) ;
			}
		}

		/// <summary>
		/// Saves the current edit model.
		/// </summary>
		/// <param name="m">The model</param>
		[HttpPost()]
		public ActionResult Edit(EditModel m) {
			if (m.SaveAll()) {
				if (m.Content.IsImage) {
					ViewBag.Title = Piranha.Resources.Content.EditTitleExistingImage ;
					ViewBag.Message = Piranha.Resources.Content.MessageImageSaved ;
				} else {
					ViewBag.Title = Piranha.Resources.Content.EditTitleExistingDocument ;
					ViewBag.Message = Piranha.Resources.Content.MessageDocumentSaved ;
				}
				m.Refresh() ;
				return View("Edit", m) ;
			} else {
				ViewBag.Title = Piranha.Resources.Content.EditTitleNew ;
				ViewBag.Message = Piranha.Resources.Content.MessageNotSaved ;
				return View("Edit", m) ;
			}
		}

		/// <summary>
		/// Deletes the specified content record.
		/// </summary>
		/// <param name="id">The content id</param>
		public ActionResult Delete(string id) {
			EditModel m = EditModel.GetById(new Guid(id)) ;

			if (m.DeleteAll()) {
				if (m.Content.IsImage)
					ViewBag.Message = Piranha.Resources.Content.MessageImageDeleted ;
				else ViewBag.Message = Piranha.Resources.Content.MessageDocumentDeleted ;
			} else {
				if (m.Content.IsImage)
					ViewBag.Message = Piranha.Resources.Content.MessageImageNotDeleted ;
				else ViewBag.Message = Piranha.Resources.Content.MessageDocumentDeleted ;
			} 
			return Index() ;
		}
    }
}
