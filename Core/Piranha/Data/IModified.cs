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
	/// Interface for models tracking modification date.
	/// </summary>
	public interface IModified
	{
		/// <summary>
		/// Gets/sets when the model was initially created.
		/// </summary>
		DateTime Created { get; set; }

		/// <summary>
		/// Gets/sets when the model was last updated.
		/// </summary>
		DateTime Updated { get; set; }
	}
}
