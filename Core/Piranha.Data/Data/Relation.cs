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
	public sealed class Relation : IModel
	{
		public Guid Id { get; set; }
		public bool IsDraft { get; set; }
		public string Type { get; set; }
		public Guid ModelId { get; set; }
		public Guid RelatedId { get; set; }
	}
}
