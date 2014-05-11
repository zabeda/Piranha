using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Piranha.WebPages
{
	/// <summary>
	/// The different hook in the core that the application can attach
	/// itself to.
	/// </summary>
	public static class Hooks {
		/// <summary>
		/// The different WebPages.Hooks available for the breadcrumb.
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
		/// The different WebPages.Hooks available for the head.
		/// </summary>
		public static class Head {
			/// <summary>
			/// Renders optional information in the head.
			/// </summary>
			public static Delegates.HeadHook Render ;
		}

		/// <summary>
		/// The different WebPages.Hooks availble for the menu.
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

		/// <summary>
		/// The different model WebPages.Hooks available for the application.
		/// </summary>
		public static class Model { 
			/// <summary>
			/// Executed after the page model is loaded.
			/// </summary>
			public static Delegates.ModelLoadedHook<Models.PageModel> PageModelLoaded ;

			/// <summary>
			/// Executed after the page model is loaded.
			/// </summary>
			public static Delegates.ModelLoadedHook<Models.PostModel> PostModelLoaded ;
		}

		public static class Mail
		{
			[Obsolete("Please use Piranha.WebPages.WebPages.Hooks.Mail.SendPassword instead")]
			public static Delegates.SendPasswordMail SendPasswordMail ;
			public static Delegates.SendPassword SendPassword ;
		}
	}
}
