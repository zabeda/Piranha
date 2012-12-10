using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranha.Extend
{
	/// <summary>
	/// Attribute used to register an assembly as a Piranha module.
	/// </summary>
	public class ModuleAttribute : Attribute
	{
		/// <summary>
		/// Gets/sets the unique id of the module.
		/// </summary>
		public string Id { get ; set ; }

		/// <summary>
		/// Gets/sets the display name of the module.
		/// </summary>
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the internal id of the module.
		/// </summary>
		public string InternalId { get ; set ; }

		/// <summary>
		/// Gets/sets the module description.
		/// </summary>
		public string Description { get ; set ; }
	}
}