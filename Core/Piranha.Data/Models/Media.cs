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

namespace Piranha.Models
{
	public sealed class Media : IModel, IModified, IModifiedBy
	{
		public Guid Id { get; set; }
		public bool IsDraft { get; set; }
		public Guid? ParentId { get; set; }
		public Guid? PermalinkId { get; set; }
		public string Filename { get; set; }
		public string OriginalUrl { get; set; }
		public DateTime? LastSynced { get; set; }
		public string Name { get; set; }
		public string ContentType { get; set; }
		public int Size { get; set; }
		public bool IsImage { get; set; }
		public bool IsFolder { get; set; }
		public int? Width { get; set; }
		public int? Height { get; set; }
		public string AltDescription { get; set; }
		public string Description { get; set; }
		public DateTime? Published { get; set; }
		public DateTime? LastPublished { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
		public Guid CreatedById { get; set; }
		public Guid UpdatedById { get; set; }

		public Permalink Permalink { get; set; }
		public User CreatedBy { get; set; }
		public User UpdatedBy { get; set; }
	}
}
