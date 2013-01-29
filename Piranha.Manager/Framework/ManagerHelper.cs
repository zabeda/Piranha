using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace Piranha.Manager
{
	public class ManagerHelper
	{
		#region Properties
		private WebPageBase Page = null ;
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="page">The current page</param>
		public ManagerHelper(WebPageBase page) {
			Page = page ;
		}

		/// <summary>
		/// Generates a href for the manager module from the virtualPath
		/// </summary>
		/// <param name="path">The virtual path</param>
		/// <param name="pathParts">Optional params</param>
		/// <returns>The href</returns>
		public string Href(string path, params object[] pathParts) {
			return Page.Href(path.Replace("~/", Piranha.Manager.SiteManager.VirtualPath), pathParts) ;
		}

		/// <summary>
		/// Renders the page at the given virtual path
		/// </summary>
		/// <param name="path">The virtual path</param>
		/// <param name="data">Optional params</param>
		/// <returns>The result</returns>
		public HelperResult RenderPage(string path, params object[] data) {
			return Page.RenderPage(path.Replace("~/", Piranha.Manager.SiteManager.VirtualPath), data) ;
		}
	}
}