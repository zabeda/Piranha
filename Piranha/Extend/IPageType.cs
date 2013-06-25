using System;
using System.Collections.Generic;

namespace Piranha.Extend
{
	/// <summary>
	/// Interface for defining a page type programmatically.
	/// </summary>
	public interface IPageType : IType
	{
		/// <summary>
		/// Gets the defined regions.
		/// </summary>
		IList<RegionType> Regions { get ; }
	}
}
