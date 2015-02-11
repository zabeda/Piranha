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
using System.Collections.Generic;

namespace Piranha.Data
{
	public sealed class Page : IModel, IModified, IModifiedBy
	{
		public Guid Id { get; set; }
		public bool IsDraft { get; set; }
		public Guid SiteId { get; set; }
		public Guid TypeId { get; set; }
		public Guid PermalinkId { get; set; }
		public Guid? OriginalId { get; set; }
		public Guid? GroupId { get; set; }
		public string DisabledGroups { get; set; }
		public Guid? ParentId { get; set; }
		public int SortOrder { get; set; }
		public string Title { get; set; }
		public string NavigationTitle { get; set; }
		public bool IsHidden { get; set; }
		public string Keywords { get; set; }
		public string Description { get; set; }
		public string PageAttachments { get; set; }
		public string Route { get; set; }
		public string View { get; set; }
		public string Redirect { get; set; }
		public DateTime? Published { get; set; }
		public DateTime? LastPublished { get; set; }
		public DateTime? LastModified { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
		public Guid CreatedById { get; set; }
		public Guid UpdatedById { get; set; }

		public PageType Type { get; set; }
		public Permalink Permalink { get; set;}
		public User CreatedBy { get; set; }
		public User UpdatedBy { get; set; }
	}
}
