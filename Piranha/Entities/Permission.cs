using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piranha.Entities
{
	/// <summary>
	/// The permission entity.
	/// </summary>
	[Serializable]
	public class Permission : StandardEntity<Permission>
	{
		#region Properties
		/// <summary>
		/// Gets/sets the id of the group who is attached to the permission.
		/// </summary>
		public Guid GroupId { get ; set ; }

		/// <summary>
		/// Gets/sets the name of the permission.
		/// </summary>
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the description shown in the manager interface.
		/// </summary>
		public string Description { get ; set ; }

		/// <summary>
		/// Gets/sets weather this permission can be removed or not.
		/// </summary>
		public bool IsLocked { get ; set ; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the group attached to the permission.
		/// </summary>
		public Group Group { get ; set ; }
		#endregion
	}
}
