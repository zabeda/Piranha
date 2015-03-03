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
	/// Permalinks are used to identify content by a textual key
	/// instead of an id.
	/// </summary>
	public sealed class Permalink : IModel, IModified, IModifiedBy
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid NamespaceId { get; set; }

		/// <summary>
		/// Gets/sets the model type.
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// Gets/sets the unique name.
		/// </summary>
		public string Name { get; set; }

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
		/// Gets/sets the namespace.
		/// </summary>
		public Namespace Namespace { get; set; }
		#endregion
	}
}
