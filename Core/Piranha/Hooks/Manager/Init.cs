using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranha.Hooks.Manager
{
	/// <summary>
	/// The hooks available for manager init.
	/// </summary>
	public static class Init
	{
		/// <summary>
		/// The delegates specific for the manager initialization.
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
		/// Called when the manager wants to register namespaces that should
		/// be scanned for controllers for the manager interface.
		/// </summary>
		public static Delegates.ManagerNamespaceRegistration RegisterNamespace;
	}
}