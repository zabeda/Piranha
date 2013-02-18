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

		/// <summary>
		/// Gets the list view for the site with the given internal id
		/// </summary>
		/// <param name="internalid">The internal id</param>
		public new void Site(string internalid) {
			Model = PageListModel.GetByInternalId(internalid.ToUpper()) ;
		}
	}
}