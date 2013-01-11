using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piranha.Entities
{
	/// <summary>
	/// The media entity.
	/// </summary>
	public class Media : StandardEntity<Media>
	{
		#region Properties
		/// <summary>
		/// Gets/sets the optional parent id.
		/// </summary>
		public Guid? ParentId { get ; set ; }

		/// <summary>
		/// Gets/sets the filename.
		/// </summary>
		public string Filename { get ; set ; }

		/// <summary>
		/// Gets/sets the original url the media object was 
		/// fetched from.
		/// </summary>
		public string OriginalUrl { get ; set ; }

		/// <summary>
		/// Gets/sets the last time the media object was synced 
		/// from its external url.
		/// </summary>
		public DateTime? LastSynced { get ; set ; }

		/// <summary>
		/// Gets/sets the content type.
		/// </summary>
		public string ContentType { get ; set ; }

		/// <summary>
		/// Gets/sets the size,
		/// </summary>
		public int Size { get ; set ; }

		/// <summary>
		/// Gets/sets weather this is an image.
		/// </summary>
		public bool IsImage { get ; set ; }

		/// <summary>
		/// Gets/sets weather this is a folder.
		/// </summary>
		public bool IsFolder { get ; set ; }

		/// <summary>
		/// Gets/sets the width of the media object in case it is an image.
		/// </summary>
		public int? Width { get ; set ; }

		/// <summary>
		/// Gets/sets the height of the media object in case it is an image.
		/// </summary>
		public int? Height { get ; set ; }

		/// <summary>
		/// Gets/sets the media name.
		/// </summary>
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the optional alternative text.
		/// </summary>
		public string AltText { get ; set ; }

		/// <summary>
		/// Gets/sets the optional description.
		/// </summary>
		public string Description { get ; set ; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the currently available extensions.
		/// </summary>
		public IList<Extension> Extensions { get ; set ; }

		/// <summary>
		/// Gets/sets the currently available comments.
		/// </summary>
		public IList<Comment> Comments { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Media() {
			Extensions = new List<Extension>() ;
			Comments = new List<Comment>() ;
		}
	}
}
