using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

using Piranha.Web;

namespace Piranha.Models
{
	public class MediaFileContent
	{
		#region Properties
		public string Filename { get ; set ; }
		public string ContentType { get ; set ; }
		public byte[] Body { get ; set ; }
		#endregion
	}

	/// <summary>
	/// Abstract base class for all media files.
	/// </summary>
	/// <typeparam name="T">The entity type</typeparam>
	[Serializable]
	public abstract class MediaFile<T> : PiranhaRecord<T>
	{
		#region Members
		//protected string BasePath = "" ;
		protected string CachePath = "" ;

		private static bool VerifiedPaths = false ;
		#endregion

		#region Fields
		/// <summary>
		/// Gets/sets the filename.
		/// </summary>
		public abstract string Filename { get ; set ; }

		/// <summary>
		/// Gets/sets the type of the media file.
		/// </summary>
		public abstract string Type { get ; set ; }
		#endregion

		#region Properties
		/// <summary>
		/// Gets the virtual path for the media file.
		/// </summary>
		//public virtual string VirtualPath { 
		//	get { return BasePath + Id ; }
		//}

		/// <summary>
		/// Gets the virtual path for the cached media file.
		/// </summary>
		public virtual string VirtualCachePath {
			get { return CachePath + Id ; }
		}

		/// <summary>
		/// Gets the physical path for the media file.
		/// </summary>
		//public virtual string PhysicalPath {
		//	get { return HttpContext.Current.Server.MapPath(VirtualPath) ; }
		//}

		/// <summary>
		/// Gets the physical path for the cached media file.
		/// </summary>
		public virtual string PhysicalCachePath {
			get { return HttpContext.Current.Server.MapPath(VirtualCachePath) ; }
		}
		#endregion

		/// <summary>
		/// Default constructor. Creates a media file with the given base path.
		/// </summary>
		/// <param name="basePath">The virtual base path.</param>
		/// <param name="cachePath">The virtual cache path for resized media.</param>
		protected MediaFile(string basePath, string cachePath) : base() {
			//BasePath = basePath + (!basePath.EndsWith("/") ? "/" : "") ;
			CachePath = cachePath + (!cachePath.EndsWith("/") ? "/" : "") ;

			if (!VerifiedPaths) {
				// Verify paths
				//if (!Directory.Exists(HttpContext.Current.Server.MapPath(BasePath)))
				//	Directory.CreateDirectory(HttpContext.Current.Server.MapPath(BasePath)) ;
				if (!Directory.Exists(HttpContext.Current.Server.MapPath(CachePath)))
					Directory.CreateDirectory(HttpContext.Current.Server.MapPath(CachePath)) ;

				VerifiedPaths = true ;
			}
		}

		/// <summary>
		/// Gets the physical media related to the content record and writes it to
		/// the given http response.
		/// </summary>
		/// <param name="response">The http response</param>
		public void GetMedia(HttpContext context, int? width = null, int? height = null) {
			if (!ClientCache.HandleClientCache(context, Id.ToString(), Updated)) {
				var compress = false ;
				Image img = null ;
				var param = SysParam.GetByName("COMPRESS_IMAGES") ;
				if (param != null && param.Value == "1")
					compress = true ;

				var data = Extend.ExtensionManager.Current.MediaProvider.Get(Id) ;

				try {
					using (var mem = new MemoryStream(data)) {
						img = Image.FromStream(mem) ;
					}
				} catch {}

				if (img != null) {
					// TODO: Do we really need the image for this, we have the dimensions in the database?
					width = width.HasValue && width.Value < img.Width ? width : img.Width ;
					if (!height.HasValue)
						height = Convert.ToInt32(((double)width / img.Width) * img.Height) ;

					if (File.Exists(GetCachePath(width.Value, height.Value))) {
						// Return generated & cached resized image
						WriteFile(context.Response, GetCachePath(width.Value, height.Value), compress) ;
					} else if (data != null) {
						int orgWidth = img.Width, orgHeight = img.Height ;

						using (var resized = Drawing.ImageUtils.Resize(img, width.Value, height.Value)) {
							if (resized.Width != orgWidth || resized.Height != orgHeight)
								resized.Save(GetCachePath(width.Value, height.Value), compress ? ImageFormat.Jpeg : img.RawFormat) ;
						}
						WriteFile(context.Response, GetCachePath(width.Value, height.Value), compress) ;
					}
					img.Dispose() ;
				}
				WriteFile(context.Response, data) ;
			}
		}

		/// <summary>
		/// Saves the 
		/// </summary>
		/// <param name="content"></param>
		/// <param name="tx"></param>
		/// <param name="setdates"></param>
		/// <param name="writefile"></param>
		/// <returns></returns>
		public virtual bool Save(MediaFileContent content, System.Data.IDbTransaction tx = null, bool setdates = true, bool writefile = true) {
			if (content != null) {
				// Set filename
				if (!String.IsNullOrEmpty(content.Filename))
					Filename = content.Filename ;
				// Set content type
				if (!String.IsNullOrEmpty(content.ContentType))
					Type = content.ContentType ;
			}
			if (base.Save(tx) && content != null) {
				// Delete the old data
				DeleteFile() ;
				DeleteCache() ;

				// Save the new
				if (writefile)
					Extend.ExtensionManager.Current.MediaProvider.Put(Id, content.Body) ;
			}
			return base.Save(tx, setdates);
		}

		/// <summary>
		/// Deletes the media file record and all related files.
		/// </summary>
		/// <param name="tx">Optional database transaction</param>
		/// <returns>Whether the action was a success</returns>
		public override bool Delete(System.Data.IDbTransaction tx = null) {
			if (base.Delete(tx)) {
				// Delete original files
				DeleteFile() ;

				// Delete Cache
				DeleteCache() ;
				
				return true;
			}
			return false;
		}

		/// <summary>
		/// Deletes the published and working copy of the media file.
		/// </summary>
		public void DeleteFile() {
			Extend.ExtensionManager.Current.MediaProvider.DeleteDraft(Id) ;
			Extend.ExtensionManager.Current.MediaProvider.Delete(Id) ;

			//DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath(BasePath)) ;
			//foreach (FileInfo file in dir.GetFiles(Id.ToString() + "*"))
			//	file.Delete() ;
		}

		/// <summary>
		/// Deletes all cached versions of the media file.
		/// </summary>
		public void DeleteCache() {
			DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath(CachePath)) ;

			foreach (FileInfo file in dir.GetFiles(Id.ToString() + "*")) 
				file.Delete() ;
		}

		#region Protected methods
		/// <summary>
		/// Gets the physical path for the cached file with the given dimensions.
		/// </summary>
		/// <param name="width">The image width</param>
		/// <param name="height">The image height</param>
		/// <returns>The virtual path</returns>
		protected string GetCachePath(int width, int height) {
			return PhysicalCachePath + "-" + width.ToString() + "x" + height.ToString() ;
		}

		/// <summary>
		/// Gets the physical path for the cached thumbnail with the given size.
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		protected string GetCacheThumbPath(int size) {
			return PhysicalCachePath + "-" + size.ToString() ;
		}

		/// <summary>
		/// Writes the given file to the http response
		/// </summary>
		/// <param name="response">The http response to write the file to</param>
		/// <param name="path">The path to the physical file</param>
		/// <param name="compressed">Whether or not the file is a compressed image</param>
		protected void WriteFile(HttpResponse response, string path, bool compressed = false) {
			if (File.Exists(path)) {
				response.StatusCode = 200 ;
				response.ContentType = compressed ? "image/jpg" : Type ;
				response.WriteFile(path) ;
				response.End() ;
			} else {
				response.StatusCode = 404 ;
			}
		}

		/// <summary>
		/// Writes the given file to the http response
		/// </summary>
		/// <param name="response">The http response to write the file to</param>
		/// <param name="data">The data of the physical file</param>
		/// <param name="compressed">Whether or not the file is a compressed image</param>
		protected void WriteFile(HttpResponse response, byte[] data, bool compressed = false) {
			if (data != null) {
				response.StatusCode = 200 ;
				response.ContentType = compressed ? "image/jpg" : Type ;
				response.BinaryWrite(data) ;
				response.End() ;
			} else {
				response.StatusCode = 404 ;
			}
		}
		#endregion
	}
}