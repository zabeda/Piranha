using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piranha.WebPages
{
	/// <summary>
	/// The different hook in the core that the application can attach
	/// itself to.
	/// </summary>
	public static class Hooks {
		/// <summary>
		/// The different hooks available for the Breadcrumb.
		/// </summary>
		public static class Breadcrumb {
			/// <summary>
			/// Renders the start of the breadcrumb.
			/// </summary>
			public static Delegates.BreadcrumbStart RenderStart ;

			/// <summary>
			/// Renders the end of the breadcrumb.
			/// </summary>
			public static Delegates.BreadcrumbEnd RenderEnd ;

			/// <summary>
			/// Renders a breadcrumb item.
			/// </summary>
			public static Delegates.BreadcrumbItemHook RenderItem ;

			/// <summary>
			/// Renders the currently active breadcrumb item.
			/// </summary>
			public static Delegates.BreadcrumbItemHook RenderActiveItem ;
		}
	}
}
