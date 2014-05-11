using System;
using Piranha.Models.Manager.PageModels;

namespace Piranha.Hooks.Manager
{
	/// <summary>
	/// The manager hooks available for pages.
	/// </summary>
	public static class Page
	{
		/// <summary>
		/// The hooks available for page models.
		/// </summary>
		public static class Model
		{
			/// <summary>
			/// Called when the view model for the page list has been loaded.
			/// </summary>
			public static Delegates.ManagerModelHook<ListModel> OnListLoad;

			/// <summary>
			/// Called when the view model for a page has been loaded.
			/// </summary>
			public static Delegates.ManagerModelHook<EditModel> OnLoad;

			/// <summary>
			/// Called right before the page model is saved.
			/// </summary>
			public static Delegates.ManagerModelHook<EditModel> OnBeforeSave;

			/// <summary>
			/// Called right after the page model has been saved.
			/// </summary>
			public static Delegates.ManagerModelHook<EditModel> OnAfterSave;
		}

		/// <summary>
		/// The hooks available for page toolbars.
		/// </summary>
		public static class Toolbar
		{
			/// <summary>
			/// Executed when the manager toolbar for the page list is rendered.
			/// </summary>
			public static Delegates.ManagerToolbarRender<ListModel> ListToolbarRender;

			/// <summary>
			/// Executed when the manager toolbar for the page edit view is rendered.
			/// </summary>
			public static Delegates.ManagerToolbarRender<EditModel> EditToolbarRender;
		}
	}
}