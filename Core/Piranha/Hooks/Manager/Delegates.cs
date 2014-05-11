using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Piranha.Hooks.Manager
{
	/// <summary>
	/// The delegates specific for the manager.
	/// </summary>
	public static class Delegates
	{
		/// <summary>
		/// Delegate for adding a namespace to the manager area.
		/// </summary>
		/// <param name="namespaces">The collection of namespaces</param>
		public delegate void ManagerNamespaceRegistration(IList<string> namespaces);

		/// <summary>
		/// Delegate for when a view model has been loaded by the manager.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <param name="controller">The manager controller</param>
		/// <param name="menu">The active menu item</param>
		/// <param name="model">The model</param>
		public delegate void ManagerModelHook<T>(Controller controller, WebPages.Manager.MenuItem menu, T model);

		/// <summary>
		/// Delegate for adding content into the manager toolbar.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <param name="url">The url helper</param>
		/// <param name="str">The string builder</param>
		/// <param name="model">The model</param>
		public delegate void ManagerToolbarRender<T>(UrlHelper url, StringBuilder str, T model);
	}
}