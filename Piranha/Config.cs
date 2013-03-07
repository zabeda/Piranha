using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Piranha
{
	/// <summary>
	/// Provides access to web.config parameters.
	/// </summary>
	public static class Config
	{
		/// <summary>
		/// Gets weather method binding is disabled. When method binding is enabled, the first UrlData
		/// argument on a GET will be matched against the available methods in the executing page class.
		/// If a matching method is found this method will be executed instead of the usual ExecutePage.
		/// </summary>
		public static bool DisableMethodBinding {
			get { return ConfigurationManager.AppSettings["disable_method_binding"] == "1" ; }
		}

		/// <summary>
		/// Gets weather model state binding is disabled. When model state binding is enabled, values
		/// mapped by the ModelBinder is automatically stored into the model state of the current page.
		/// </summary>
		public static bool DisableModelStateBinding {
			get { return ConfigurationManager.AppSettings["disable_modelstate_binding"] == "1" ; }
		}

		/// <summary>
		/// Gets the optional internal id of the current sitetree. If the parameter is empty
		/// the current host name is resolved. If the hostname isn't found DEFAULT_SITE is used.
		/// </summary>
		public static string SiteTree {
			get { return WebPages.WebPiranha.CurrentSite.InternalId ; }
		}

		/// <summary>
		/// Gets the id of the currently active site tree.
		/// </summary>
		public static Guid SiteTreeId {
			get { return WebPages.WebPiranha.CurrentSite.Id ; }
		}

		/// <summary>
		/// Gets the id of the namespace for the currently active site tree.
		/// </summary>
		public static Guid SiteTreeNamespaceId {
			get { return WebPages.WebPiranha.CurrentSite.NamespaceId ; }
		}

		/// <summary>
		/// Gets the id of the default namespace.
		/// </summary>
		public static readonly Guid DefaultNamespaceId = new Guid("8FF4A4B4-9B6C-4176-AAA2-DB031D75AC03") ;

		/// <summary>
		/// Gets the id of the default site tree.
		/// </summary>
		public static readonly Guid DefaultSiteTreeId = new Guid("c2f87b2b-f585-4696-8a2b-3c9df882701e") ;

		#region Private methods
		/// <summary>
		/// Gets the currently active site tree from the current host headers.
		/// </summary>
		/// <returns>The site tree</returns>
		//private static Entities.SiteTree GetSiteTree() {
		//	// Check for configured site tree from the host name
		//	if (HttpContext.Current != null && HttpContext.Current.Request != null) {
		//		var hostname = HttpContext.Current.Request.Url.Host.ToLower() ;
		//		if (WebPages.WebPiranha.HostNames.ContainsKey(hostname))
		//			return WebPages.WebPiranha.HostNames[hostname] ;
		//	}
		//	// Nothing found, return default
		//	return WebPages.WebPiranha.DefaultSite ;
		//}
		#endregion
	}
}