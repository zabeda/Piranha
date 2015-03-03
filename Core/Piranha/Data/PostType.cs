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
	/// Post types are used to define different types of posts
	/// with different properties.
	/// </summary>
	public sealed class PostType : ContentType, IModel, IModified, IModifiedBy
	{
		#region Properties
		/// <summary>
		/// Gets/sets the permalink id.
		/// </summary>
		public Guid? PermalinkId { get; set; }

		/// <summary>
		/// Gets/sets the JSON serialized array of properties.
		/// </summary>
		[Obsolete("Will be removed in the next iteration")]
		public string PostProperties { get; set; }

		/// <summary>
		/// Gets/sets the optional archive route.
		/// </summary>
		public string ArchiveRoute { get; set; }

		/// <summary>
		/// Gets/sets the posts can override the archive route.
		/// </summary>
		[Obsolete("Will be removed in the next iteration")]
		public bool IsArchiveRouteVirtual { get; set; }

		/// <summary>
		/// Gets/sets if posts of this type should be included in
		/// the rss feed.
		/// </summary>
		public bool EnableFeed { get; set; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the permalink.
		/// </summary>
		public Permalink Permalink { get; set; }
		#endregion
	}
}
