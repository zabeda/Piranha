using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Piranha.Manager.Models;
using Piranha.WebPages;

namespace Piranha.Manager.Templates
{
	/// <summary>
	/// Page template for the page type edit view.
	/// </summary>
	[Access(Function="ADMIN_PAGE_TEMPLATE", RedirectUrl="~/manager/account")]
	public abstract class PageTypeEdit : ContentPage<PageTypeEditModel>
	{
		/// <summary>
		/// Executes the page.
		/// </summary>
		protected override void ExecutePage() {
			if (!IsPost) {
				if (UrlData.Count > 0) {
					Page.Title = Piranha.Resources.Template.EditPageTitleExisting;
					Model = PageTypeEditModel.GetById(new Guid(UrlData[0])) ;
				} else {
					Page.Title = Piranha.Resources.Template.EditPageTitleNew ;
					Model = new PageTypeEditModel() ;
				}
			}
		}
	}
}