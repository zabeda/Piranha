/*
 * Copyright (c) 2015 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha
 * 
 */

using System;

namespace Piranha.Data
{
	/// <summary>
	/// Page types are used to define define different types of
	/// pages with different regions and properties.
	/// </summary>
	public sealed class PageType : ContentType, IModel, IModified, IModifiedBy
	{
		#region Properties
		/// <summary>
		/// Gets/sets the JSON serialized array of regions.
		/// </summary>
		[Obsolete("Will be removed in the next iteration")]
		public string PageRegions { get; set; }

		/// <summary>
		/// Gets/sets the JSON serialized array of properties.
		/// </summary>
		[Obsolete("Will be removed in the next iteration")]
		public string PageProperties { get; set; }

		/// <summary>
		/// Gets/sets the optional redirect.
		/// </summary>
		public string Redirect { get; set; }

		/// <summary>
		/// Gets/sets the pages can override the redirect.
		/// </summary>
		public bool IsRedirectVirtual { get; set; }

		/// <summary>
		/// Gets/sets if this is a site type or not.
		/// </summary>
		public bool IsSite { get; set; }
		#endregion
	}
}
