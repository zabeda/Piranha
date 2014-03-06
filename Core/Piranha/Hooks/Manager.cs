using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranha.Hooks
{
	/// <summary>
	/// The Hooks available for the main application.
	/// </summary>
	public static class Manager
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
		}

		/// <summary>
		/// The hooks available for manager init.
		/// </summary>
		public static class Init
		{
			/// <summary>
			/// Called when the manager wants to register namespaces that should
			/// be scanned for controllers for the manager interface.
			/// </summary>
			public static Delegates.ManagerNamespaceRegistration RegisterNamespace;
		}
	}
}