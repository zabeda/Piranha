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
	public sealed class PageRegion : IModel, IModified, IModifiedBy
	{
		public Guid Id { get; set; }
		public bool IsDraft { get; set; }
		public Guid PageId { get; set; }
		public bool PageIsDraft { get; set; }
		public Guid RegionTypeId { get; set;}
		public string Name { get; set; }
		public string Body { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
		public Guid CreatedById { get; set; }
		public Guid UpdatedById { get; set; }

		public Page Page { get; set; }
		public PageTypeRegion RegionType { get; set; }
		public User CreatedBy { get; set; }
		public User UpdatedBy { get; set; }
	}
}
