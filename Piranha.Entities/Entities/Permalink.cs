using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piranha.Entities
{
	/// <summary>
	/// The permalink entity.
	/// </summary>
	public class Permalink : StandardEntity<Permalink>
	{
		#region Properties
		/// <summary>
		/// Gets/sets the id of the namespace.
		/// </summary>
		public Guid NamespaceId { get ; set ; }

		/// <summary>
		/// Gets/sets the type of entity the permalink is attached to (PAGE/POST).
		/// </summary>
		public string Type { get ; set ; }

		/// <summary>
		/// Gets/sets the unique permalink name.
		/// </summary>
		public string Name { get ; set ; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the namespace.
		/// </summary>
		public Namespace Namespace { get ; set ; }
		#endregion
	}
}
