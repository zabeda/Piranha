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
	/// Used to store property values for pages & posts.
	/// </summary>
	[Obsolete("Will be removed in the next iteration")]
	public sealed class Property : IModel, IModified, IModifiedBy
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
		/// Gets/sets the id of the model this property is for.
		/// </summary>
		public Guid ParentId { get; set; }

		/// <summary>
		/// Gets/sets the property name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the serialized value.
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// Gets/sets when the model was initially created.
		/// </summary>
		public DateTime Created { get; set; }

		/// <summary>
		/// Gets/sets when the model was last updated.
		/// </summary>
		public DateTime Updated { get; set; }

		/// <summary>
		/// Gets/sets the id of the user who initially 
		/// created the model.
		/// </summary>
		public Guid CreatedById { get; set; }

		/// <summary>
		/// Gets/sets the id of the user who last
		/// updated the model.
		/// </summary>
		public Guid UpdatedById { get; set; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the user who initially created the model.
		/// </summary>
		public User CreatedBy { get; set; }

		/// <summary>
		/// Gets/sets the user who last updated the model.
		/// </summary>
		public User UpdatedBy { get; set; }
		#endregion
	}
}
