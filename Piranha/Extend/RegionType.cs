using System;

namespace Piranha.Extend
{
	/// <summary>
	/// Class for defining a region available for a page type.
	/// </summary>
	public sealed class RegionType 
	{
		/// <summary>
		/// Gets/sets the internal id.
		/// </summary>
		public string InternalId { get ; set ; }

		/// <summary>
		/// Gets/sets the display name.
		/// </summary>
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the region type.
		/// </summary>
		public Type Type { get ; set ; }
	}
}