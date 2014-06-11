using System;
using Piranha.Models.Manager.CategoryModels;

namespace Piranha.Hooks.Manager
{
	/// <summary>
	/// The manager hooks available for categories.
	/// </summary>
	public static class Category
	{
		/// <summary>
		/// The hooks available for category models.
		/// </summary>
		public static class Model
		{
			/// <summary>
			/// Called when the view model for the category list has been loaded.
			/// </summary>
			public static Delegates.ManagerModelHook<ListModel> OnListLoad;

			/// <summary>
			/// Called when the view model for a category has been loaded.
			/// </summary>
			public static Delegates.ManagerModelHook<EditModel> OnLoad;

			/// <summary>
			/// Called right before the category model is saved.
			/// </summary>
			public static Delegates.ManagerModelHook<EditModel> OnBeforeSave;

			/// <summary>
			/// Called right after the category model has been saved.
			/// </summary>
			public static Delegates.ManagerModelHook<EditModel> OnAfterSave;
		}

		/// <summary>
		/// The hooks available for category toolbars.
		/// </summary>
		public static class Toolbar
		{
			/// <summary>
			/// Executed when the manager toolbar for the category list is rendered.
			/// </summary>
			public static Delegates.ManagerToolbarRender<ListModel> ListToolbarRender;

			/// <summary>
			/// Executed when the manager toolbar for the category edit view is rendered.
			/// </summary>
			public static Delegates.ManagerToolbarRender<EditModel> EditToolbarRender;
		}
	}
}