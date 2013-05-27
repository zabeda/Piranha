using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranha
{
	/// <summary>
	/// Simple class for defining a configured provider
	/// </summary>
	internal sealed class ConfigProvider
	{
		/// <summary>
		/// Get/sets the full assembly name.
		/// </summary>
		public string AssemblyName { get ; set ; }

		/// <summary>
		/// Gets/sets the full type name.
		/// </summary>
		public string TypeName { get ; set ; }
	}
}