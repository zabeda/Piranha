using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranha.Manager
{
	/// <summary>
	/// Class for creating a toolbar item in the manager interface.
	/// </summary>
	public class ToolbarItem
	{
		#region Properties
		/// <summary>
		/// Gets/sets the internal id of the item.
		/// </summary>
		public string InternalId { get ; set ; }

		/// <summary>
		/// Gets/sets the display name of the item.
		/// </summary>
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the css class of the item.
		/// </summary>
		public string CssClass { get ; set ; }

		/// <summary>
		/// Gets/sets the url of the item.
		/// </summary>
		public string Url { get ; set ; }
		#endregion
	}
}