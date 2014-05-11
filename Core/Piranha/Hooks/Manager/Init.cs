using System;

namespace Piranha.Hooks.Manager
{
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