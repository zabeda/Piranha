using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piranha.Entities
{
	/// <summary>
	/// The namespace entity that defines the available permalink namespaces.
	/// </summary>
	[Serializable]
	public class Namespace : StandardEntity<Namespace>
	{
		#region Properties
		/// <summary>
		/// Gets/sets the internal id used to access the namespace.
		/// </summary>
		public string InternalId { get ; set ; }

		/// <summary>
		/// Gets/sets the name.
		/// </summary>
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the optional description.
		/// </summary>
		public string Description { get ; set ; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the permalinks available for this namespace.
		/// </summary>
		public IList<Permalink> Permalinks { get ; set ; }
		#endregion
	}
}
