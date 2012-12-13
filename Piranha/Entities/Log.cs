using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piranha.Entities
{
	/// <summary>
	/// Entity for the system log.
	/// </summary>
	public class Log : StandardEntity<Log>
	{
		#region Properties
		/// <summary>
		/// Gets/sets the id of the entity the log is related to.
		/// </summary>
		public Guid ParentId { get ; set ; }

		/// <summary>
		/// Gets/sets the type of entity the log is related to.
		/// </summary>
		public string ParentType { get ; set ; }

		/// <summary>
		/// Gets/sets the action the log is related to (INSERT/UPDATE/DELETE/PUBLISH/DEPUBLISH).
		/// </summary>
		public string Action { get ; set ; }
		#endregion
	}
}
