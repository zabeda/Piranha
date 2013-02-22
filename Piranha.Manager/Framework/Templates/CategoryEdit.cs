using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Piranha.Manager.Models;
using Piranha.WebPages;

namespace Piranha.Manager.Templates
{
	/// <summary>
	/// Page template for the category edit view.
	/// </summary>
	[Access(Function="ADMIN_CATEGORY", RedirectUrl="~/manager/account")]
	public abstract class CategoryEdit : ContentPage<CategoryEditModel>
	{
		/// <summary>
		/// Executes the page.
		/// </summary>
		protected override void ExecutePage() {
			if (!IsPost) {
				if (UrlData.Count > 0) {
					Page.Title = Piranha.Manager.Resources.Category.EditTitleExisting ;
					Model = CategoryEditModel.GetById(new Guid(UrlData[0])) ;
				} else {
					Page.Title = Piranha.Manager.Resources.Category.EditTitleNew ;
					Model = new CategoryEditModel() ;
				}
			}
		}

		/// <summary>
		/// Saves the given edit model.
		/// </summary>
		/// <param name="m">The model</param>
		[HttpPost()]
		public void Save(CategoryEditModel m) {
			Model = m ;
			Page.Title = Piranha.Manager.Resources.Category.EditTitleExisting ;

			//try {
				if (ModelState.IsValid) {
					if (m.Save())
						this.SuccessMessage(Piranha.Manager.Resources.Category.MessageSaved) ;
					else this.ErrorMessage(Piranha.Manager.Resources.Category.MessageNotSaved) ;
				}
			//} catch {
			//	this.ErrorMessage(Piranha.Manager.Resources.Category.MessageNotSaved) ;
			//}
		}
	}
}