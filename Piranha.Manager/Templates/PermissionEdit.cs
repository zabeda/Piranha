using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Piranha.Manager.Models;

namespace Piranha.Manager.Templates
{
	/// <summary>
	/// Page template for the permission edit view.
	/// </summary>
	public abstract class PermissionEdit : ManagerContentPage<Models.PermissionEditModel>
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
		public void Save(PermissionEditModel m) {
			Model = m ;
			Page.Title = Piranha.Resources.Settings.EditTitleExistingAccess ;

			if (ModelState.IsValid) {
				if (m.Save())
					SuccessMessage(Piranha.Resources.Settings.MessageAccessSaved) ;
				else ErrorMessage(Piranha.Resources.Settings.MessageAccessNotSaved) ;
			}
		}
	}
}