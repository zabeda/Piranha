using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Piranha.Manager.Models;
using Piranha.WebPages;

namespace Piranha.Manager.Templates
{
	/// <summary>
	/// Page template for the comment edit view.
	/// </summary>
	[Access(Function="ADMIN_COMMENT", RedirectUrl="~/manager/account")]
	public abstract class CommentEdit : Piranha.WebPages.ContentPage<Models.CommentEditModel>
	{
		/// <summary>
		/// Executes the page.
		/// </summary>
		protected override void ExecutePage() {
			if (!IsPost) {
				if (UrlData.Count > 0) {
					Page.Title = Piranha.Manager.Resources.Comment.EditTitleExisting ;
					Model = CommentEditModel.GetById(new Guid(UrlData[0])) ;
					if (Hooks.CommentEditModelLoaded != null)
						Hooks.CommentEditModelLoaded(this, Menu.GetActiveMenuItem(), Model) ;
				} else {
					Response.Redirect("~/manager/comment") ;
				}
			}
		}

		/// <summary>
		/// Saves the given edit model.
		/// </summary>
		/// <param name="m">The model</param>
		[HttpPost()]
		public void Save(CommentEditModel m) {
			Model = m ;
			Page.Title = Piranha.Manager.Resources.Comment.EditTitleExisting ;

			try {
				if (ModelState.IsValid) {
					if (m.Save())
						this.SuccessMessage(Piranha.Manager.Resources.Comment.MessageSaved) ;
					else this.ErrorMessage(Piranha.Manager.Resources.Comment.MessageNotSaved) ;
				}
			} catch {
				this.ErrorMessage(Piranha.Manager.Resources.Comment.MessageNotSaved) ;
			}
		}

		/// <summary>
		/// Deletes the model with the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <param name="fromList">Weather the call came from the list</param>
		public void Delete(string id, bool fromList = false) {
			Model = CommentEditModel.GetById(new Guid(id)) ;

			try {
				if (Model.Delete())
					Response.Redirect("~/manager/comment?msg=deleted") ;
				else {
					if (fromList)
						Response.Redirect("~/manager/comment?msg=notdeleted") ;
					else this.ErrorMessage(Piranha.Manager.Resources.Comment.MessageNotDeleted) ;
				}
			} catch {
				if (fromList)
					Response.Redirect("~/manager/comment?msg=notdeleted") ;
				else this.ErrorMessage(Piranha.Manager.Resources.Comment.MessageNotDeleted) ;
			}
		}
	}
}