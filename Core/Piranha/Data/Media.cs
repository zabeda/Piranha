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
	/// Media for all uploaded static content.
	/// </summary>
	public sealed class Media : IModel, IModified, IModifiedBy
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
		/// Gets/sets the optional parent id.
		/// </summary>
		public Guid? ParentId { get; set; }

		/// <summary>
		/// Gets/sets the permalink id.
		/// </summary>
		public Guid? PermalinkId { get; set; }

		/// <summary>
		/// Gets/sets the filename of the uploaded
		/// media asset.
		/// </summary>
		public string Filename { get; set; }

		/// <summary>
		/// Gets/sets the original url if this media asset
		/// was added from url.
		/// </summary>
		public string OriginalUrl { get; set; }

		/// <summary>
		/// Gets/sets when the media asset was last synced
		/// from its original url.
		/// </summary>
		public DateTime? LastSynced { get; set; }

		/// <summary>
		/// Gets/sets the optional display name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the content type.
		/// </summary>
		public string ContentType { get; set; }

		/// <summary>
		/// Gets/sets the size in bytes of the uploaded
		/// media asset.
		/// </summary>
		public int Size { get; set; }

		/// <summary>
		/// Gets/sets if this is an image or not.
		/// </summary>
		public bool IsImage { get; set; }

		/// <summary>
		/// Gets/sets if this is a folder or not.
		/// </summary>
		public bool IsFolder { get; set; }

		/// <summary>
		/// Gets/sets the optional width.
		/// </summary>
		public int? Width { get; set; }

		/// <summary>
		/// Gets/sets the optional height.
		/// </summary>
		public int? Height { get; set; }

		/// <summary>
		/// Gets/sets the optional alt description.
		/// </summary>
		public string AltDescription { get; set; }

		/// <summary>
		/// Gets/sets the optional description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets/sets when the model was initially published.
		/// </summary>
		public DateTime? Published { get; set; }

		/// <summary>
		/// Gets/sets when the model was last published.
		/// </summary>
		public DateTime? LastPublished { get; set; }

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
	}
}
