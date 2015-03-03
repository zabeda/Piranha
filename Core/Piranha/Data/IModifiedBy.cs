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
	/// Interface for models tracking the user who made
	/// modifications to it.
	/// </summary>
	public interface IModifiedBy
	{
		/// <summary>
		/// Gets/sets the id of the user who initially 
		/// created the model.
		/// </summary>
		Guid CreatedById { get; set; }

		/// <summary>
		/// Gets/sets the id of the user who last
		/// updated the model.
		/// </summary>
		Guid UpdatedById { get; set; }
	}
}
