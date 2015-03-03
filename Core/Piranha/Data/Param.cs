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
	/// Params are used to store application configuration.
	/// </summary>
	public sealed class Param : IModel, IModified
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the unique name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the optional description.
		/// </summary>
		public string Description { get; set ; }

		/// <summary>
		/// Gets/sets the param value.
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// Gets/sets if this is a system param needed
		/// by the core framework.
		/// </summary>
		public bool IsSystem { get; set; }

		/// <summary>
		/// Gets/sets when the model was initially created.
		/// </summary>
		public DateTime Created { get; set; }

		/// <summary>
		/// Gets/sets when the model was last updated.
		/// </summary>
		public DateTime Updated { get; set; }

		/// <summary>
		/// Gets/sets the optional id of the user who initially 
		/// created the model.
		/// </summary>
		public Guid? CreatedById { get; set; }

		/// <summary>
		/// Gets/sets the optional id of the user who last
		/// updated the model.
		/// </summary>
		public Guid? UpdatedById { get; set; }
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
