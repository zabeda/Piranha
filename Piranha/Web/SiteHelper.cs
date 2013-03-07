using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Piranha.Models;

namespace Piranha.Web
{
	/// <summary>
	/// The site helper exports basic information to the views.
	/// </summary>
	public class SiteHelper
	{
		/// <summary>
		/// Gets the site title.
		/// </summary>
		public string SiteTitle {
			get {
				return WebPages.WebPiranha.CurrentSite.MetaTitle ;
			}
		}

		/// <summary>
		/// Gets the site description.
		/// </summary>
		public string SiteDescription {
			get {
				return WebPages.WebPiranha.CurrentSite.MetaDescription ;
			}
		}
	}
}