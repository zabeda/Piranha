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
		#region Members
		private static Guid? siteTreeId ;
		#endregion

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
		/// Gets the optional startpage id.
		/// </summary>
		public static Guid StartpageId {
			get {
				var id = ConfigurationManager.AppSettings["startpage_id"] ;
				if (!String.IsNullOrEmpty(id))
					return new Guid(id) ;
				return Guid.Empty ;
			}
		}

		/// <summary>
		/// Gets the optional internal id of the current sitetree. If the parameter is empty
		/// DEFAULT_SITE is used.
		/// </summary>
		public static string SiteTree {
			get {
				var internalId = ConfigurationManager.AppSettings["sitetree"] ;
				if (!String.IsNullOrEmpty(internalId))
					return internalId ;
				return "DEFAULT_SITE" ;
			}
		}

		/// <summary>
		/// Gets the id of the currently active site tree.
		/// </summary>
		public static Guid SiteTreeId {
			get {
				if (!siteTreeId.HasValue) {
					using (var db = new DataContext()) {
						var internalId = SiteTree ;
						siteTreeId = db.SiteTrees.Where(s => s.InternalId == internalId).Select(s => s.Id).Single() ;
					}
				}
				return siteTreeId.Value ;
			}
		}
	}
}