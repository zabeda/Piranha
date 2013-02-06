using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Piranha.Manager.Models;
using Piranha.WebPages;

namespace Piranha.Manager.Templates
{
	/// <summary>
	/// Page template for the page list view.
	/// </summary>
	[Access(Function="ADMIN_PAGE", RedirectUrl="~/manager/account")]
	public abstract class PageList : ContentPage<PageListModel>
	{
		/// <summary>
		/// Executes the page.
		/// </summary>
		protected override void ExecutePage() {
			Model = PageListModel.GetByInternalId() ;
		}
	}
}