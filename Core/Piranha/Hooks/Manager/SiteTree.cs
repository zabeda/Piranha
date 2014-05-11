using System;
using Piranha.Models.Manager.PostModels;
using Piranha.Areas.Manager.Models;

namespace Piranha.Hooks.Manager
{
	/// <summary>
	/// The manager hooks available for site trees.
	/// </summary>
	public class SiteTree
	{
		/// <summary>
		/// The hooks available for site tree toolbars.
		/// </summary>
		public static class Toolbar
		{
			/// <summary>
			/// Executed when the manager toolbar for the site list is rendered.
			/// </summary>
			public static Delegates.ManagerToolbarRender<SiteTreeListModel> ListToolbarRender;

			/// <summary>
			/// Executed when the manager toolbar for the site edit view is rendered.
			/// </summary>
			public static Delegates.ManagerToolbarRender<SiteTreeEditModel> EditToolbarRender;
		}
	}
}