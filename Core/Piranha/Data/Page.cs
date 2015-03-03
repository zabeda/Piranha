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
	/// <summary>
	/// Pages are hierarchical structured content positioned
	/// in a site tree.
	/// </summary>
	public sealed class Page : IModel, IModified, IModifiedBy
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets if this is a draft or not.
		/// </summary>
		public bool IsDraft { get; set; }

		/// <summary>
		/// Gets/sets the site id.
		/// </summary>
		public Guid SiteId { get; set; }

		/// <summary>
		/// Gets/sets the type id.
		/// </summary>
		public Guid TypeId { get; set; }

		/// <summary>
		/// Gets/sets the permalink id.
		/// </summary>
		public Guid PermalinkId { get; set; }

		/// <summary>
		/// Gets/sets the optional id of the original
		/// page if this is a copy.
		/// </summary>
		public Guid? OriginalId { get; set; }

		/// <summary>
		/// Gets/sets the optional group needed to view the page.
		/// </summary>
		public Guid? GroupId { get; set; }

		/// <summary>
		/// Gets/sets the JSON serialized array of groups
		/// excluded from viewing the page.
		/// </summary>
		public string DisabledGroups { get; set; }

		/// <summary>
		/// Gets/sets the optional parent id.
		/// </summary>
		public Guid? ParentId { get; set; }

		/// <summary>
		/// Gets/sets the sort order in its hierarchical position.
		/// </summary>
		public int SortOrder { get; set; }

		/// <summary>
		/// Gets/sets the title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Gets/sets the optional navigation title.
		/// </summary>
		public string NavigationTitle { get; set; }

		/// <summary>
		/// Gets/sets if the page should be hidden in the
		/// navigation structure.
		/// </summary>
		public bool IsHidden { get; set; }

		/// <summary>
		/// Gets/sets the optional meta keywords.
		/// </summary>
		public string Keywords { get; set; }

		/// <summary>
		/// Gets/sets the optional meta description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets/sets the JSON serialized array of attached media.
		/// </summary>
		[Obsolete("Will be removed in the next iteration")]
		public string PageAttachments { get; set; }

		/// <summary>
		/// Gets/sets the optional route.
		/// </summary>
		public string Route { get; set; }

		/// <summary>
		/// Gets/sets the optional view.
		/// </summary>
		public string View { get; set; }

		/// <summary>
		/// Gets/sets the optional redirect.
		/// </summary>
		public string Redirect { get; set; }

		/// <summary>
		/// Gets/sets when the model was initially published.
		/// </summary>
		public DateTime? Published { get; set; }

		/// <summary>
		/// Gets/sets when the model was last published.
		/// </summary>
		public DateTime? LastPublished { get; set; }

		/// <summary>
		/// Gets/sets the last modification date.
		/// </summary>
		public DateTime? LastModified { get; set; }

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
		/// Gets/sets the page type.
		/// </summary>
		public PageType Type { get; set; }

		/// <summary>
		/// Gets/sets the permalink.
		/// </summary>
		public Permalink Permalink { get; set;}

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
