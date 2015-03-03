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
	/// Posts are content that do not have a fixed position
	/// within a navigation structure.
	/// </summary>
	public sealed class Post : IModel, IModified, IModifiedBy
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
		/// Gets/sets the type id.
		/// </summary>
		public Guid TypeId { get; set; }

		/// <summary>
		/// Gets/sets the permalink id.
		/// </summary>
		public Guid PermalinkId { get; set; }

		/// <summary>
		/// Gets/sets the title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Gets/sets the optional meta keywords.
		/// </summary>
		public string Keywords { get; set; }

		/// <summary>
		/// Gets/sets the optional meta description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets/sets if the post should be included in feeds.
		/// </summary>
		public bool IncludeInFeed { get; set; }

		/// <summary>
		/// Gets/sets the short excerpt.
		/// </summary>
		public string Excerpt { get; set; }

		/// <summary>
		/// Gets/sets the main post body.
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// Gets/sets the JSON serialized array of media attachments.
		/// </summary>
		[Obsolete("Will be removed in the next iteration")]
		public string PostAttchments { get; set; }

		/// <summary>
		/// Gets/sets the optional route.
		/// </summary>
		public string Route { get; set; }

		/// <summary>
		/// Gets/sets the optional view.
		/// </summary>
		public string View { get; set; }

		/// <summary>
		/// Gets/sets the optional archive route.
		/// </summary>
		public string ArchiveRoute { get; set; }

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
		/// Gets/sets the post type.
		/// </summary>
		public PostType Type { get; set; }

		/// <summary>
		/// Gets/sets the permalink.
		/// </summary>
		public Permalink Permalink { get; set; }

		/// <summary>
		/// Gets/sets the user who initially created the model.
		/// </summary>
		public User CreatedBy { get; set; }

		/// <summary>
		/// Gets/sets the user who last updated the model.
		/// </summary>
		public User UpdatedBy { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Post() {
			IncludeInFeed = true;
		}
	}
}
