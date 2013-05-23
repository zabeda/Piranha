using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranha
{
	/// <summary>
	/// Simple class for defining a configured provider
	/// </summary>
	public sealed class ConfigProvider
	{
		public string AssemblyName { get ; set ; }
		public string TypeName { get ; set ; }
	}
}