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
	/// Site trees are navigation structures but can also bind to
	/// hostnames to serve as a sub site.
	/// </summary>
	public sealed class SiteTree : IModel, IModified, IModifiedBy
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets the namespace id.
		/// </summary>
		public Guid NamespaceId { get; set; }

		/// <summary>
		/// Gets/sets the internal id.
		/// </summary>
		public string InternalId { get; set; }

		/// <summary>
		/// Gets/sets the display name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the optional description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets/sets the meta title.
		/// </summary>
		public string MetaTitle { get; set; }

		/// <summary>
		/// Gets/sets the meta description.
		/// </summary>
		public string MetaDescription { get; set; }

		/// <summary>
		/// Gets/sets the host names.
		/// </summary>
		public string Hostnames { get; set; }

		/// <summary>
		/// Gets/sets when the model was initially created.
		/// </summary>
		public DateTime Created { get; set; }

		/// <summary>
		/// Gets/sets when the model was last updated.
		/// </summary>
		public DateTime Updated { get; set; }

		/// <summary>
		/// Gets/sets the id of the user who initially 
		/// created the model.
		/// </summary>
		public Guid CreatedById { get; set; }

		/// <summary>
		/// Gets/sets the id of the user who last
		/// updated the model.
		/// </summary>
		public Guid UpdatedById { get; set; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the site namespace.
		/// </summary>
		public Namespace Namespace { get; set; }

		/// <summary>
		/// Gets/sets the user who initially created the model.
		/// </summary>
		public User CreatedBy { get; set; }

		/// <summary>
		/// Gets/sets the user who last updated the model.
		/// </summary>
		public User UpdatedBy { get; set; }
		#endregion
	}
}
