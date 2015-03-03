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
	/// Regions hold the actual content of pages.
	/// </summary>
	public sealed class PageRegion : IModel, IModified, IModifiedBy
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets if this is a draft or not.
		/// </summary>
		public bool IsDraft { get; set; }

		/// <summary>
		/// Gets/sets the page id.
		/// </summary>
		public Guid PageId { get; set; }

		/// <summary>
		/// Gets/sets if the page is a draft or not.
		/// </summary>
		[Obsolete("Will be removed in the next iteration")]
		public bool PageIsDraft { get; set; }

		/// <summary>
		/// Gets/sets the region type id.
		/// </summary>
		public Guid RegionTypeId { get; set;}

		/// <summary>
		/// Gets/sets the region name.
		/// </summary>
		[Obsolete("Will be removed in the next iteration")]
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the JSON serialized region body.
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// Gets/sets when the model was initially created.
		/// </summary>
		[Obsolete("Will be removed in the next iteration")]
		public DateTime Created { get; set; }

		/// <summary>
		/// Gets/sets when the model was last updated.
		/// </summary>
		[Obsolete("Will be removed in the next iteration")]
		public DateTime Updated { get; set; }

		/// <summary>
		/// Gets/sets the id of the user who initially 
		/// created the model.
		/// </summary>
		[Obsolete("Will be removed in the next iteration")]
		public Guid CreatedById { get; set; }

		/// <summary>
		/// Gets/sets the id of the user who last
		/// updated the model.
		/// </summary>
		[Obsolete("Will be removed in the next iteration")]
		public Guid UpdatedById { get; set; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the page.
		/// </summary>
		public Page Page { get; set; }

		/// <summary>
		/// Gets/sets the region type.
		/// </summary>
		public PageTypeRegion RegionType { get; set; }

		/// <summary>
		/// Gets/sets the user who initially created the model.
		/// </summary>
		[Obsolete("Will be removed in the next iteration")]
		public User CreatedBy { get; set; }

		/// <summary>
		/// Gets/sets the user who last updated the model.
		/// </summary>
		[Obsolete("Will be removed in the next iteration")]
		public User UpdatedBy { get; set; }
		#endregion
	}
}
