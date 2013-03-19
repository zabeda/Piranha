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
		[Access(Function="ADMIN_CONTENT")]
		public ActionResult Index(string id = "") {
			try {
				if (!String.IsNullOrEmpty(id))
					ViewBag.Expanded = new Guid(id) ;
				else ViewBag.Expanded = Guid.Empty ;
			} catch {
				ViewBag.Expanded = Guid.Empty ;
			}
			
            return View("Index", ListModel.Get());
        }

		/// <summary>
		/// Gets the popup list.
		/// </summary>
		public ActionResult Popup(string id = "") {
			return View("Popup", PopupModel.Get(id)) ;
		}

		/// <summary>
		/// Uploads a new content object from the popup dialog.
		/// </summary>
		/// <param name="m">The popup module</param>
		/// <returns>A json result</returns>
		[HttpPost()]
		[Access(Function="ADMIN_CONTENT")]
		public JsonResult Popup(PopupModel m) {
			if (ModelState.IsValid) {
				EditModel em = new EditModel() ;
				em.Content = m.NewContent ;
				em.FileUrl = m.FileUrl ;
				em.UploadedFile = m.UploadedFile ;

				if (em.SaveAll(false))
					return new JsonResult() { Data = true } ;
			}
			return new JsonResult() { Data = false } ;
		}

		/// <summary>
		/// Gets the files list.
		/// </summary>
		/// <returns></returns>
		[Access(Function="ADMIN_CONTENT")]
		public ActionResult Uploads() {
			return View("Uploads", UploadModel.Get()) ;
		}

		/// <summary>
		/// Inserts a new media object.
		/// </summary>
		/// <param name="parentid">The optional parent folder id.</param>
		[Access(Function="ADMIN_CONTENT")]
		public ActionResult Insert(string id) {
			return EditInternal(new EditModel(false, !String.IsNullOrEmpty(id) ? new Guid(id) : Guid.Empty), true) ;
		}

		/// <summary>
		/// Inserts a new media folder object.
		/// </summary>
		/// <param name="parentid">The optional parent folder id.</param>
		[Access(Function="ADMIN_CONTENT")]
		public ActionResult InsertFolder(string id) {
			return EditInternal(new EditModel(true, !String.IsNullOrEmpty(id) ? new Guid(id) : Guid.Empty), true) ;
		}

		/// <summary>
		/// Edits or inserts a new content model.
		/// </summary>
		/// <param name="id">The id of the content</param>
		[Access(Function="ADMIN_CONTENT")]
		public ActionResult Edit(string id) {
			return EditInternal(EditModel.GetById(new Guid(id))) ;
		}

		/// <summary>
		/// Insert or update of a media object.
		/// </summary>
		/// <param name="m">The new or existing model</param>
		/// <param name="insert">Whether to insert or update</param>
		/// <returns></returns>
		private ActionResult EditInternal(EditModel m, bool insert = false) {
			ViewBag.Folder = m.Content.IsFolder ;

			if (insert) 
				ViewBag.Title = Piranha.Resources.Content.EditTitleNew ;
			else if (m.Content.IsImage)
				ViewBag.Title = Piranha.Resources.Content.EditTitleExistingImage ;
			else if (m.Content.IsFolder)
				ViewBag.Title = Piranha.Resources.Content.EditTitleExistingFolder ;
			else ViewBag.Title = Piranha.Resources.Content.EditTitleExistingDocument ;

			return View("Edit", m) ;
		}

		/// <summary>
		/// Saves the current edit model.
		/// </summary>
		/// <param name="m">The model</param>
		[HttpPost()]
		[Access(Function="ADMIN_CONTENT")]
		public ActionResult Edit(bool draft, EditModel m) {
			if (m.SaveAll(draft)) {
				ViewBag.Folder = m.Content.IsFolder ;
				if (m.Content.IsImage) {
					ViewBag.Title = Piranha.Resources.Content.EditTitleExistingImage ;
					if (draft)
						SuccessMessage(Piranha.Resources.Content.MessageImageSaved, true) ;
					else SuccessMessage(Piranha.Resources.Content.MessageImagePublished, true) ;
				} else if (m.Content.IsFolder) {
					ViewBag.Title = Piranha.Resources.Content.EditTitleExistingFolder ;
					if (draft)
						SuccessMessage(Piranha.Resources.Content.MessageFolderSaved, true) ;
					else SuccessMessage(Piranha.Resources.Content.MessageFolderPublished, true) ;
				} else {
					ViewBag.Title = Piranha.Resources.Content.EditTitleExistingDocument ;
					if (draft)
						SuccessMessage(Piranha.Resources.Content.MessageDocumentSaved, true) ;
					else SuccessMessage(Piranha.Resources.Content.MessageDocumentPublished, true) ;
				}
				// ModelState.Clear() ;
				// m.Refresh() ;
				// return View("Edit", m) ;
				return RedirectToAction("edit", new { id = m.Content.Id, returl = ViewBag.ReturnUrl }) ;
			} else {
				ViewBag.Title = Piranha.Resources.Content.EditTitleNew ;
				ErrorMessage(Piranha.Resources.Content.MessageNotSaved) ;
				return View("Edit", m) ;
			}
		}

		/// <summary>
		/// Syncs the media object with the given id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[Access(Function="ADMIN_CONTENT")]
		public ActionResult Sync(string id) {
			var m = EditModel.GetById(new Guid(id)) ;

			try {
				if (m.Sync()) {
					if (m.Content.IsImage)
						SuccessMessage(Piranha.Resources.Content.MessageImageSynced, true) ;
					else SuccessMessage(Piranha.Resources.Content.MessageDocumentSynced, true) ;
				} else {
					if (m.Content.IsImage)
						InformationMessage(Piranha.Resources.Content.MessageImageNotSynced, true) ;
					else InformationMessage(Piranha.Resources.Content.MessageDocumentNotSynced, true) ;
				}
			} catch (HttpException e) {
				if (e.GetHttpCode() == 404) {
					if (m.Content.IsImage)
						ErrorMessage(Piranha.Resources.Content.MessageImageNotFound, true) ;
					else ErrorMessage(Piranha.Resources.Content.MessageDocumentNotFound, true) ;
				} else {
					if (m.Content.IsImage)
						ErrorMessage(Piranha.Resources.Content.MessageImageSyncError, true) ;
					else ErrorMessage(Piranha.Resources.Content.MessageDocumentSyncError, true) ;
				}
			}
			return RedirectToAction("edit", new { id = id, returl = ViewBag.ReturnUrl }) ;
		}

		/// <summary>
		/// Deletes the specified content record.
		/// </summary>
		/// <param name="id">The content id</param>
		[Access(Function="ADMIN_CONTENT")]
		public ActionResult Delete(string id) {
			EditModel m = EditModel.GetById(new Guid(id)) ;

			if (m.DeleteAll()) {
				if (m.Content.IsImage)
					SuccessMessage(Piranha.Resources.Content.MessageImageDeleted, true) ;
				else if (m.Content.IsFolder)
					SuccessMessage(Piranha.Resources.Content.MessageFolderDeleted, true) ;
				else SuccessMessage(Piranha.Resources.Content.MessageDocumentDeleted, true) ;
			} else {
				if (m.Content.IsImage)
					ErrorMessage(Piranha.Resources.Content.MessageImageNotDeleted, true) ;
				else if (m.Content.IsFolder)
					ErrorMessage(Piranha.Resources.Content.MessageFolderNotDeleted, true) ;
				else ErrorMessage(Piranha.Resources.Content.MessageDocumentNotDeleted, true) ;
			}
			if (!String.IsNullOrEmpty(ViewBag.ReturnUrl))
				return Redirect(ViewBag.ReturnUrl) ;
			return RedirectToAction("index") ;
		}

		/// <summary>
		/// Reverts to latest published verison.
		/// </summary>
		/// <param name="id">The page id.</param>
		[Access(Function="ADMIN_CONTENT")]
		public ActionResult Revert(string id) {
			Piranha.Models.Content.Revert(new Guid(id)) ;

			var content = Piranha.Models.Content.GetSingle(new Guid(id)) ;
			if (content != null) {
				SuccessMessage(Piranha.Resources.Content.MessageReverted, true) ;
			} else {
				ErrorMessage(Piranha.Resources.Content.MessageNotFound, true) ;
			}
			return RedirectToAction("edit", new { id = id, returl = ViewBag.ReturnUrl }) ;
			//return Edit(id) ;
		}

		/// <summary>
		/// Unpublishes the specified page.
		/// </summary>
		/// <param name="id">The page id</param>
		[Access(Function="ADMIN_PAGE")]
		public ActionResult Unpublish(string id) {
			Piranha.Models.Content.Unpublish(new Guid(id)) ;

			var content = Piranha.Models.Content.GetSingle(new Guid(id), true) ;
			if (content != null) {
				if (content.IsFolder)
					SuccessMessage(Piranha.Resources.Content.MessageFolderUnpublished, true) ;
				else if (content.IsImage)
					SuccessMessage(Piranha.Resources.Content.MessageImageUnpublished, true) ;
				else SuccessMessage(Piranha.Resources.Content.MessageDocumentUnpublished, true) ;
			} else {
				ErrorMessage(Piranha.Resources.Content.MessageNotFound, true) ;
			}
			return RedirectToAction("edit", new { id = id, returl = ViewBag.ReturlUrl }) ;
			//return Edit(id) ;
		}

		/// <summary>
		/// Gets the content object with the given id
		/// </summary>
		/// <param name="id">The content id</param>
		[Access(Function="ADMIN_CONTENT")]
		public JsonResult Get(string id) {
			var service = new Rest.ContentService() ;

			return Json(service.Get(new Guid(id), true), JsonRequestBehavior.AllowGet) ;
		}
    }
}
