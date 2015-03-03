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
	/// Defines a region for a page type.
	/// </summary>
	public sealed class PageTypeRegion : IModel, IModified, IModifiedBy
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the page type id.
		/// </summary>
		public Guid PageTypeId { get; set; }

		/// <summary>
		/// Gets/sets the internal id.
		/// </summary>
		public string InternalId { get; set; }

		/// <summary>
		/// Gets/sets the sort order.
		/// </summary>
		public int SortOrder { get; set; }

		/// <summary>
		/// Gets/sets the display name.
		/// </summary>
		public string Name { get; set; }
		
		/// <summary>
		/// Gets/sets the optional description.
		/// </summary>
		[Obsolete("Will be removed in the next iteration")]
		public string Description { get; set; }

		/// <summary>
		/// Gets/sets the CLR type of the region.
		/// </summary>
		public string CLRType { get; set; }

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
