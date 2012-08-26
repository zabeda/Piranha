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
		/// The different hooks available for the breadcrumb.
		/// </summary>
		public static class Breadcrumb {
			/// <summary>
			/// Renders the start of the breadcrumb.
			/// </summary>
			public static Delegates.BreadcrumbStartHook RenderStart ;

			/// <summary>
			/// Renders the end of the breadcrumb.
			/// </summary>
			public static Delegates.BreadcrumbEndHook RenderEnd ;

			/// <summary>
			/// Renders a breadcrumb item.
			/// </summary>
			public static Delegates.BreadcrumbItemHook RenderItem ;

			/// <summary>
			/// Renders the currently active breadcrumb item.
			/// </summary>
			public static Delegates.BreadcrumbItemHook RenderActiveItem ;
		}

		/// <summary>
		/// The different hooks availble for the menu.
		/// </summary>
		public static class Menu {
			/// <summary>
			/// Renders the start of a menu level.
			/// </summary>
			public static Delegates.MenuLevelHook RenderLevelStart ;

			/// <summary>
			/// Renders the end of a menu level.
			/// </summary>
			public static Delegates.MenuLevelHook RenderLevelEnd ;

			/// <summary>
			/// Renders the start of a menu item.
			/// </summary>
			public static Delegates.MenuItemHook RenderItemStart ;

			/// <summary>
			/// Renders the end of a menu item.
			/// </summary>
			public static Delegates.MenuItemHook RenderItemEnd ;

			/// <summary>
			/// Renders the menu item link.
			/// </summary>
			public static Delegates.MenuItemLinkHook RenderItemLink ;
		}
	}
}
