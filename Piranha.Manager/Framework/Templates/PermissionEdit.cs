using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Piranha.Manager.Models;
using Piranha.WebPages;

namespace Piranha.Manager.Templates
{
	/// <summary>
	/// Page template for the permission edit view.
	/// </summary>
	[Access(Function="ADMIN_ACCESS", RedirectUrl="~/manager/account")]
	public abstract class PermissionEdit : Piranha.WebPages.ContentPage<Models.PermissionEditModel>
	{
		/// <summary>
		/// Executes the page.
		/// </summary>
		protected override void ExecutePage() {
			if (!IsPost) {
				if (UrlData.Count > 0) {
					Page.Title = Piranha.Resources.Settings.EditTitleExistingAccess ;
					Model = PermissionEditModel.GetById(new Guid(UrlData[0])) ;
				} else {
					Page.Title = Piranha.Resources.Settings.EditTitleNewAccess ;
					Model = new PermissionEditModel() ;
				}
			}
		}

		/// <summary>
		/// Saves the given edit model.
		/// </summary>
		/// <param name="m">The model</param>
		[HttpPost()]
		public void Save(PermissionEditModel m) {
			Model = m ;
			Page.Title = Piranha.Resources.Settings.EditTitleExistingAccess ;

			if (ModelState.IsValid) {
				if (m.Save())
					this.SuccessMessage(Piranha.Resources.Settings.MessageAccessSaved) ;
				else this.ErrorMessage(Piranha.Resources.Settings.MessageAccessNotSaved) ;
			}
		}

		/// <summary>
		/// Deletes the model with the given id.
		/// </summary>
		/// <param name="id">The id</param>
		public void Delete(string id) {
			Model = PermissionEditModel.GetById(new Guid(id)) ;

			if (Model.Delete())
				Response.Redirect(SiteManager.VirtualPath + "permission?msg=deleted") ;
			else this.ErrorMessage(Piranha.Resources.Settings.MessageAccessNotDeleted) ;
		}
	}
}