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
	/// Defines a relationship between two models.
	/// </summary>
	[Obsolete("Will be removed in the next iteration")]
	public sealed class Relation : IModel
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
		/// Gets/sets the type of relation.
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// Gets/sets the id of the main model.
		/// </summary>
		public Guid ModelId { get; set; }

		/// <summary>
		/// Gets/sets the id of the related model.
		/// </summary>
		public Guid RelatedId { get; set; }
		#endregion
	}
}
