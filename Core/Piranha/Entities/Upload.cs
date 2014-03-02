using System;
using System.Collections.Generic;

namespace Piranha.Entities
{
	/// <summary>
	/// Entity for handling uploads from the client application.
	/// </summary>
	[Serializable]
	public class Upload : StandardEntity<Upload>
	{
		#region Properties
		/// <summary>
		/// Gets/sets the id of the entity this upload belongs to.
		/// </summary>
		public Guid ParentId { get ; set ; }

		/// <summary>
		/// Gets/sets the filename.
		/// </summary>
		public string Filename { get ; set ; }

		/// <summary>
		/// Gets/sets the content type.
		/// </summary>
		public string Type { get ; set ; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the available comments.
		/// </summary>
		public IList<Comment> Comments { get ; set ; }
		#endregion

		/// <summary>
		/// Deletes the current upload.
		/// </summary>
		/// <param name="db">The db context</param>
		public override void OnDelete(DataContext db) {
			// Delete the main file
			App.Instance.MediaProvider.Delete(Id, IO.MediaType.Upload) ;
			DeleteCache() ;
		}

		/// <summary>
		/// Deletes all cached versions of the media file.
		/// </summary>
		public void DeleteCache() {
			App.Instance.MediaCacheProvider.Delete(Id, IO.MediaType.Upload) ;
		}
	}
}
