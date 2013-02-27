using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;

using Piranha.Manager.Models;
using Piranha.WebPages;

namespace Piranha.Manager.Templates
{
	/// <summary>
	/// Page template for the user edit view.
	/// </summary>
	[Access(Function="ADMIN_USER", RedirectUrl="~/manager/account")]
	public abstract class UserEdit : Piranha.WebPages.ContentPage<UserEditModel>
	{
		/// <summary>
		/// Executes the page.
		/// </summary>
		protected override void ExecutePage() {
			if (!IsPost) {
				if (UrlData.Count > 0) {
					Page.Title = Piranha.Manager.Resources.User.EditTitleExisting ;
					Model = UserEditModel.GetById(new Guid(UrlData[0])) ;
				} else {
					Page.Title = Piranha.Manager.Resources.User.EditTitleNew ;
					Model = new UserEditModel() ;
				}
			}
		}

		/// <summary>
		/// Saves the given user model.
		/// </summary>
		/// <param name="m">The model</param>
		[HttpPost()]
		public void Save(UserEditModel m) {
			Model = m ;
			Page.Title = Piranha.Manager.Resources.User.EditTitleExisting ;

			try {
				if (ModelState.IsValid) {
					if (m.Save())
						this.SuccessMessage(Piranha.Manager.Resources.User.MessageSaved) ;
					else this.InformationMessage(Piranha.Manager.Resources.Global.MessageNotSaved) ;
				}
			} catch {
				this.ErrorMessage(Piranha.Manager.Resources.User.MessageSaveError) ;
			}
		}

		/// <summary>
		/// Deletes the model with the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <param name="fromList">Weather the call came from the list</param>
		public void Delete(string id, bool fromList = false) {
			Model = UserEditModel.GetById(new Guid(id)) ;
			
			try {
				if (Model.Delete()) {
					Response.Redirect("~/manager/user?msg=deleted") ;
				} else { 
					if (fromList)
						Response.Redirect("~/manager/user?msg=notdeleted") ;
					else this.ErrorMessage(Piranha.Manager.Resources.User.MessageNotDeleted) ;
				}
			} catch {
				if (fromList)
					Response.Redirect("~/manager/user?msg=notdeleted") ;
				else this.ErrorMessage(Piranha.Manager.Resources.User.MessageNotDeleted) ;
			}
		}
	}
}