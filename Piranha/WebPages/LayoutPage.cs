using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.WebPages;

namespace Piranha.WebPages
{
	/// <summary>
	/// Base class for all layout pages.
	/// </summary>
	public abstract class LayoutPage : BasePage
	{
		#region Properties

		public new Models.PageModel Model { get ; set ; }
		#endregion

		/// <summary>
		/// Initializes the layout page.
		/// </summary>
		protected override void InitializePage() {
			// Get the current site page model
			if (Page.Current != null) {
				Model = Models.PageModel.GetBySite(Page.Current.SiteTreeId) ;
			} else Model = Models.PageModel.GetBySite(Config.SiteTreeId) ;

			base.InitializePage();
		}
	}
}
