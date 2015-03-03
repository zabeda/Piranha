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
	/// The different states of a comment.
	/// </summary>
	[Obsolete("Will be removed in the next iteration")]
	public enum CommentStatus
	{
		New = 0,
		Approved = 1,
		NotApproved = 2
	}
}
