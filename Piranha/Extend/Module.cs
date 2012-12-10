using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranha.Extend
{
	/// <summary>
	/// Base class for defining a Piranha module.
	/// </summary>
	public abstract class Module
	{
		/// <summary>
		/// Ensures the module.
		/// </summary>
		public virtual void Ensure() {
		}
	}
}