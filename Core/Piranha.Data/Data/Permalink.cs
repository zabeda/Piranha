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
	public sealed class Permalink : IModel, IModified, IModifiedBy
	{
		public Guid Id { get; set; }
		public Guid NamespaceId { get; set; }
		public string Type { get; set; }
		public string Name { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
		public Guid CreatedById { get; set; }
		public Guid UpdatedById { get; set; }

		public Namespace Namespace { get; set; }
	}
}
