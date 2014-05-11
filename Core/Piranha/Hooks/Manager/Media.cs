using System;
using Piranha.Models.Manager.ContentModels;

namespace Piranha.Hooks.Manager
{
	/// <summary>
	/// The manager hooks available for media.
	/// </summary>
	public static class Media
	{
		/// <summary>
		/// The hooks available for media models.
		/// </summary>
		public static class Model
		{
			/// <summary>
			/// Called when the view model for the media list has been loaded.
			/// </summary>
			public static Delegates.ManagerModelHook<ListModel> OnListLoad;

			/// <summary>
			/// Called when the view model for a media has been loaded.
			/// </summary>
			public static Delegates.ManagerModelHook<EditModel> OnLoad;

			/// <summary>
			/// Called right before the media model is saved.
			/// </summary>
			public static Delegates.ManagerModelHook<EditModel> OnBeforeSave;

			/// <summary>
			/// Called right after the media model has been saved.
			/// </summary>
			public static Delegates.ManagerModelHook<EditModel> OnAfterSave;
		}

		/// <summary>
		/// The hooks available for media toolbars.
		/// </summary>
		public static class Toolbar
		{
			/// <summary>
			/// Executed when the manager toolbar for the media list is rendered.
			/// </summary>
			public static Delegates.ManagerToolbarRender<ListModel> ListToolbarRender;

			/// <summary>
			/// Executed when the manager toolbar for the media edit view is rendered.
			/// </summary>
			public static Delegates.ManagerToolbarRender<EditModel> EditToolbarRender;
		}
	}
}