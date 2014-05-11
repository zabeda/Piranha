using System;
using Piranha.Models.Manager.PostModels;
using Piranha.Areas.Manager.Models;

namespace Piranha.Hooks.Manager
{
	/// <summary>
	/// The manager hooks available for post.
	/// </summary>
	public static class Post
	{
		/// <summary>
		/// The hooks available for post models.
		/// </summary>
		public static class Model
		{
			/// <summary>
			/// Called when the view model for the post list has been loaded.
			/// </summary>
			public static Delegates.ManagerModelHook<PostListModel> OnListLoad;

			/// <summary>
			/// Called when the view model for a post has been loaded.
			/// </summary>
			public static Delegates.ManagerModelHook<EditModel> OnLoad;

			/// <summary>
			/// Called right before the post model is saved.
			/// </summary>
			public static Delegates.ManagerModelHook<EditModel> OnBeforeSave;

			/// <summary>
			/// Called right after the post model has been saved.
			/// </summary>
			public static Delegates.ManagerModelHook<EditModel> OnAfterSave;
		}

		/// <summary>
		/// The hooks available for post toolbars.
		/// </summary>
		public static class Toolbar
		{
			/// <summary>
			/// Executed when the manager toolbar for the post list is rendered.
			/// </summary>
			public static Delegates.ManagerToolbarRender<PostListModel> ListToolbarRender;

			/// <summary>
			/// Executed when the manager toolbar for the post edit view is rendered.
			/// </summary>
			public static Delegates.ManagerToolbarRender<EditModel> EditToolbarRender;
		}
	}
}