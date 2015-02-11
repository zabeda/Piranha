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
	public sealed class PostType : IModel, IModified, IModifiedBy
	{
		public Guid Id { get; set; }
		public Guid? PermalinkId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string VisualGuide { get; set; }
		public string PostProperties { get; set; }
		public string Route { get; set; }
		public bool IsRouteVirtual { get; set; }
		public string View { get; set; }
		public bool IsViewVirtual { get; set; }
		public string ArchiveRoute { get; set; }
		public bool IsArchiveRouteVirtual { get; set; }
		public bool EnableFeed { get; set; }
		public string CLRType { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
		public Guid CreatedById { get; set; }
		public Guid UpdatedById { get; set; }

		public Permalink Permalink { get; set; }
		public User CreatedBy { get; set; }
		public User UpdatedBy { get; set; }
	}
}
