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
	/// This model will be removed in the next iteration. Comments
	/// will then be revisited and redesigned.
	/// </summary>
	[Obsolete("Will be removed in the next iteration")]
	public sealed class Comment : IModel, IModified
	{
		public Guid Id { get; set; }
		public Guid ParentId { get; set; }
		public bool ParentIsDraft { get; set; }
		public CommentStatus Status { get; set; }
		public int ReportedCount { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }
		public string AuthorName { get; set; }
		public string AuthorEmail { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
		public Guid? CreatedById { get; set; }
		public Guid? UpdatedById { get; set; }

		public User CreatedBy { get; set; }
		public User UpdatedBy { get; set; }
	}
}
