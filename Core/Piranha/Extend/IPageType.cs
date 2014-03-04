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
		/// Gets the redirect url.
		/// </summary>
		string Redirect { get; }

		/// <summary>
		/// Gets if pages of the current type should be able to
		/// set the redirect url.
		/// </summary>
		bool ShowRedirect { get; }

		/// <summary>
		/// Gets the defined regions.
		/// </summary>
		IList<RegionType> Regions { get ; }
	}
}
