using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Piranha.Manager
{
	/// <summary>
	/// The area registration for the manager module.
	/// </summary>
	internal class ManagerRegistration : AreaRegistration
	{
		/// <summary>
		/// Gets the area name.
		/// </summary>
		public override string AreaName {
			get { return "Manager" ; }
		}

		/// <summary>
		/// Registers the manager area.
		/// </summary>
		/// <param name="context">The context</param>
		public override void RegisterArea(AreaRegistrationContext context) {
			// Register manager routing
			context.MapRoute(
				"Manager",
				"manager/{controller}/{action}/{id}",
				new { area = "manager", controller = "account", action = "index", id = UrlParameter.Optional },
				new[] { "Piranha.Areas.Manager.Controllers" }
			).DataTokens["UseNamespaceFallback"] = false ;

			// Register filters & binders
			RegisterGlobalFilters(GlobalFilters.Filters) ;
		}

		#region Private methods
		/// <summary>
		/// Registers all global filters.
		/// </summary>
		/// <param name="filters">The current filter collection</param>
		private void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());
		}
		#endregion
	}
}