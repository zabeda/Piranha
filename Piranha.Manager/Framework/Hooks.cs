using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranha.Manager
{
	/// <summary>
	/// The different hooks exposed by the manager.
	/// </summary>
	public static class Hooks
	{
		/// <summary>
		/// Executed after the category list model is loaded but before the view is called.
		/// </summary>
		public static Delegates.ModelLoadedHook<Models.CategoryListModel> CategoryListModelLoaded ;

		/// <summary>
		/// Executed after the category edit model is loaded but before the view is called.
		/// </summary>
		public static Delegates.ModelLoadedHook<Models.CategoryEditModel> CategoryEditModelLoaded ;

		/// <summary>
		/// Executed after the comment list model is loaded but before the view is called.
		/// </summary>
		public static Delegates.ModelLoadedHook<Models.CommentListModel> CommentListModelLoaded ;

		/// <summary>
		/// Executed after the comment edit model is loaded but before the view is called.
		/// </summary>
		public static Delegates.ModelLoadedHook<Models.CommentEditModel> CommentEditModelLoaded ;

		/// <summary>
		/// Executed after the page list model is loaded but before the view is called.
		/// </summary>
		public static Delegates.ModelLoadedHook<Models.PageListModel> PageListModelLoaded ;

		/// <summary>
		/// Executed after the page edit model is loaded but before the view is called.
		/// </summary>
		// public static Delegates.ModelLoadedHook<Models.PageEditModel> PageEditModelLoaded ;

		/// <summary>
		/// Executed after the page type list model is loaded but before the view is called.
		/// </summary>
		public static Delegates.ModelLoadedHook<Models.PageTypeListModel> PageTypeListModelLoaded ;

		/// <summary>
		/// Executed after the page type edit model is loaded but before the view is called.
		/// </summary>
		public static Delegates.ModelLoadedHook<Models.PageTypeEditModel> PageTypeEditModelLoaded ;

		/// <summary>
		/// Executed after the param list model is loaded but before the view is called.
		/// </summary>
		public static Delegates.ModelLoadedHook<Models.ParamListModel> ParamListModelLoaded ;

		/// <summary>
		/// Executed after the param edit model is loaded but before the view is called.
		/// </summary>
		public static Delegates.ModelLoadedHook<Models.ParamEditModel> ParamEditModelLoaded ;

		/// <summary>
		/// Executed after the permission list model is loaded but before the view is called.
		/// </summary>
		public static Delegates.ModelLoadedHook<Models.PermissionListModel> PermissionListModelLoaded ;

		/// <summary>
		/// Executed after the permission edit model is loaded but before the view is called.
		/// </summary>
		public static Delegates.ModelLoadedHook<Models.PermissionEditModel> PermissionEditModelLoaded ;

		/// <summary>
		/// Executed after the post list model is loaded but before the view is called.
		/// </summary>
		public static Delegates.ModelLoadedHook<Models.PostListModel> PostListModelLoaded ;

		/// <summary>
		/// Executed after the post edit model is loaded but before the view is called.
		/// </summary>
		// public static Delegates.ModelLoadedHook<Models.PostEditModel> PostEditModelLoaded ;

		/// <summary>
		/// Executed after the post type list model is loaded but before the view is called.
		/// </summary>
		public static Delegates.ModelLoadedHook<Models.PostTypeListModel> PostTypeListModelLoaded ;

		/// <summary>
		/// Executed after the post type edit model is loaded but before the view is called.
		/// </summary>
		// public static Delegates.ModelLoadedHook<Models.PostTypeEditModel> PostTypeEditModelLoaded ;

		/// <summary>
		/// Executed after the user list model is loaded but before the view is called.
		/// </summary>
		public static Delegates.ModelLoadedHook<Models.UserListModel> UserListModelLoaded ;

		/// <summary>
		/// Executed after the user edit model is loaded but before the view is called.
		/// </summary>
		public static Delegates.ModelLoadedHook<Models.UserEditModel> UserEditModelLoaded ;
	}
}