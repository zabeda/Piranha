using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Piranha.Entities
{
	/// <summary>
	/// Entity for handling uploads from the client application.
	/// </summary>
	[Serializable]
	public class Upload : StandardEntity<Upload>
	{
		#region Members
		private const string VirtualDir = "~/App_Data/Uploads/" ;
		private const string VirtualCacheDir = "~/App_Data/Cache/Uploads/" ;
		#endregion

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
			Application.Current.MediaProvider.Delete(Id, IO.MediaType.Upload) ;
			DeleteCache() ;
		}

		/// <summary>
		/// Deletes all cached versions of the media file.
		/// </summary>
		public void DeleteCache() {
			var dir = new DirectoryInfo(HttpContext.Current.Server.MapPath(VirtualCacheDir)) ;
			if (dir != null)
				foreach (FileInfo file in dir.GetFiles(Id.ToString() + "*")) 
					file.Delete() ;
		}
	}
}
