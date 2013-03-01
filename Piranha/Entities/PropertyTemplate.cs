using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranha.Entities
{
	/// <summary>
	/// Entity defining the meta data of a property available
	/// for a content template.
	/// </summary>
	public class PropertyTemplate : StandardEntity<PropertyTemplate>
	{
		#region Properties
		/// <summary>
		/// Gets/sets the id of the content template.
		/// </summary>
		public Guid TemplateId { get ; set ; }

		/// <summary>
		/// Gets/sets the internal id.
		/// </summary>
		public string InternalId { get ; set ; }

		/// <summary>
		/// Gets/sets the display name used in the manager.
		/// </summary>
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the optional description.
		/// </summary>
		public string Description { get ; set ; }

		/// <summary>
		/// Gets/sets the sequence number.
		/// </summary>
		public int Seqno { get ; set ; }

		/// <summary>
		/// Gets/sets the property type.
		/// </summary>
		public string Type { get ; set ; }
		#endregion
	}
}