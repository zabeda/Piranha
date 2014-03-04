using System;

namespace Piranha.Extend
{
	/// <summary>
	/// Modules are redistributable packages that can be added
	/// to extend the core functonality.
	/// </summary>
	public interface IModule
	{
		/// <summary>
		/// Initializes the module. This method is called on
		/// application start.
		/// </summary>
		void Init();
	}
}
