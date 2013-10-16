using System;
using System.IO;
using System.Web;

namespace Piranha.IO
{
	/// <summary>
	/// Provider for caching resized image media in the local App_Data/Cache directory.
	/// </summary>
	public class LocalMediaCacheProvider : IMediaCacheProvider
	{
		#region Members
		protected readonly string BasePath = HttpContext.Current.Server.MapPath("~/App_Data/Cache") ;

		/// <summary>
		/// The local base path for all media files.
		/// </summary>
		protected readonly string MediaPath = HttpContext.Current.Server.MapPath("~/App_Data/Cache/Content") ;

		/// <summary>
		/// The local base path for all media files.
		/// </summary>
		protected readonly string UploadPath = HttpContext.Current.Server.MapPath("~/App_Data/Cache/Uploads") ;
		#endregion

		/// <summary>
		/// Default constructor, creates a new local media cache provider.
		/// </summary>
		public LocalMediaCacheProvider() {
			if (!Directory.Exists(BasePath))
				Directory.CreateDirectory(BasePath) ;
			if (!Directory.Exists(MediaPath))
				Directory.CreateDirectory(MediaPath) ;
			if (!Directory.Exists(UploadPath))
				Directory.CreateDirectory(UploadPath) ;
		}

		/// <summary>
		/// Gets the data for the cached image with the given dimensions. In case of
		/// a cache miss null is returned.
		/// </summary>
		/// <param name="id">The id</param>
		/// <param name="width">The width of the image</param>
		/// <param name="height">The optional height of the image</param>
		/// <param name="type">The media type</param>
		/// <returns>The binary data, null in case of a cache miss</returns>
		public virtual byte[] Get(Guid id, int width, int? height, MediaType type = MediaType.Media) {
			var path = GetPath(id, false, width, height, type) ;

			if (File.Exists(path))
				return File.ReadAllBytes(path) ;
			return null ;
		}

		/// <summary>
		/// Gets the draft data for the cached image with the given dimensions. In case of
		/// a cache miss null is returned.
		/// </summary>
		/// <param name="id">The id</param>
		/// <param name="width">The width of the image</param>
		/// <param name="height">The optional height of the image</param>
		/// <param name="type">The media type</param>
		/// <returns>The binary data, null in case of a cache miss</returns>
		public virtual byte[] GetDraft(Guid id, int width, int? height, MediaType type = MediaType.Media) {
			var path = GetPath(id, true, width, height, type) ;

			if (File.Exists(path))
				return File.ReadAllBytes(path) ;
			return null ;
		}

		/// <summary>
		/// Stores the given cache data for the image with the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <param name="data">The media data</param>
		/// <param name="width">The width of the image</param>
		/// <param name="height">The optional height of the image</param>
		/// <param name="type">The media type</param>
		public virtual void Put(Guid id, byte[] data, int width, int? height, MediaType type = MediaType.Media) {
			var path = GetPath(id, false, width, height, type) ;
			File.WriteAllBytes(path, data) ;
		}

		/// <summary>
		/// Stores the given cache data for the image draft with the given id.
		/// </summary>
		/// <param name="id">The id</param>
		/// <param name="data">The media data</param>
		/// <param name="width">The width of the image</param>
		/// <param name="height">The optional height of the image</param>
		/// <param name="type">The media type</param>
		public virtual void PutDraft(Guid id, byte[] data, int width, int? height, MediaType type = MediaType.Media) {
			var path = GetPath(id, true, width, height, type) ;
			File.WriteAllBytes(path, data) ;
		}

		/// <summary>
		/// Deletes all cached images related to the given id, both draft and published.
		/// </summary>
		/// <param name="id">The id</param>
		/// <param name="type">The media type</param>
		public virtual void Delete(Guid id, MediaType type = MediaType.Media) {
			var dir = new DirectoryInfo(type == MediaType.Media ? MediaPath : UploadPath) ;

			foreach (var file in dir.GetFiles(id.ToString() + "*"))
				file.Delete() ;
		}

        /// <summary>
        /// Gets the total size of all items in the cache.
        /// </summary>
		/// <param name="id">The id</param>
		/// <param name="type">The media type</param>
        /// <returns>The size of the cache in bytes.</returns>
        public virtual long GetTotalSize(Guid id, MediaType type = MediaType.Media) {
			long size = 0;
			var dir = new DirectoryInfo(type == MediaType.Media ? MediaPath : UploadPath) ;

			foreach (var file in dir.GetFiles(id.ToString() + "*"))
				size += file.Length ;
			return size ;
		}

		#region Private methods
		/// <summary>
		/// Gets the local path from the given parameters.
		/// </summary>
		/// <param name="id">The media id</param>
		/// <param name="draft">Whether or not this is a draft</param>
		/// <param name="width">The width of the image</param>
		/// <param name="height">The optional height of the image</param>
		/// <param name="type">The media type</param>
		/// <returns>The local path</returns>
		protected string GetPath(Guid id, bool draft, int width, int? height, MediaType type = MediaType.Media) {
			return (type == MediaType.Media ? MediaPath : UploadPath) + "/" + id.ToString() + (draft ? "-draft" : "") + 
				"-" + width.ToString() + (height.HasValue ? "x" + height.Value.ToString() : "") ;
		}
		#endregion
	}
}