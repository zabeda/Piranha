using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

using Piranha.Data;
using Piranha.Web;

namespace Piranha.Models
{
	/// <summary>
	/// A content object is an image, document or other file that's been uploaded from
	/// the manager area. Content uploaded from the application is usually handled using
	/// the Upload record.
	/// 
	/// Changes made to records of this type are logged.
	/// </summary>
	[PrimaryKey(Column="content_id")]
	public class Content : MediaFile<Content>, ICacheRecord<Content>
	{
		#region Fields
		/// <summary>
		/// Gets/sets the id.
		/// </summary>
		[Column(Name="content_id")]
		public override Guid Id { get ; set ; }

		/// <summary>
		/// Gets/sets the optional parent id.
		/// </summary>
		[Column(Name="content_parent_id")]
		[Display(ResourceType=typeof(Piranha.Resources.Content), Name="ParentId")]
		public Guid ParentId { get ; set ; }

		/// <summary>
		/// Gets/sets the filename.
		/// </summary>
		[Column(Name="content_filename")]
		[Display(ResourceType=typeof(Piranha.Resources.Content), Name="Filename")]
		public override string Filename { get ; set ; }

		/// <summary>
		/// Gets/sets the original url the media object was 
		/// fetched from.
		/// </summary>
		[Column(Name="content_url")]
		[Display(ResourceType=typeof(Piranha.Resources.Content), Name="OriginalUrl")]
		public string OriginalUrl { get ; set ; }

		/// <summary>
		/// Gets/sets the last time the media object was synced 
		/// from its external url.
		/// </summary>
		[Column(Name="content_synced")]
		[Display(ResourceType=typeof(Piranha.Resources.Content), Name="LastSynced")]
		public DateTime LastSynced { get ; set ; }

		/// <summary>
		/// Gets/sets the type
		/// </summary>
		[Column(Name="content_type")]
		[Display(ResourceType=typeof(Piranha.Resources.Content), Name="ContentType")]
		public override string Type { get ; set ; }

		/// <summary>
		/// Gets/sets the content size.
		/// </summary>
		[Column(Name="content_size")]
		[Display(ResourceType=typeof(Piranha.Resources.Content), Name="ContentSize")]
		public int Size { get ; set ; }

		/// <summary>
		/// Get/sets weather the content is an image or not.
		/// </summary>
		[Column(Name="content_image")]
		public bool IsImage { get ; set ; }

		/// <summary>
		/// Gets/sets weather this is a content folder or not.
		/// </summary>
		[Column(Name="content_folder")]
		public bool IsFolder { get ; set ; }

		/// <summary>
		/// Gets/sets the possible width of the content.
		/// </summary>
		[Column(Name="content_width")]
		public int Width { get ; set ; }

		/// <summary>
		/// Gets/sets the possible height of the content.
		/// </summary>
		[Column(Name="content_height")]
		public int Height { get ; set ; }

		/// <summary>
		/// Gets/sets the name of the content.
		/// </summary>
		[Column(Name="content_name")]
		[Display(ResourceType=typeof(Piranha.Resources.Content), Name="Name")]
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the alternate text.
		/// </summary>
		[Column(Name="content_alt")]
		[Display(ResourceType=typeof(Piranha.Resources.Content), Name="AlternateText")]
		public string AlternateText { get ; set ; }

		/// <summary>
		/// Gets/sets the decription.
		/// </summary>
		[Column(Name="content_description")]
		[Display(ResourceType=typeof(Piranha.Resources.Content), Name="Description")]
		public string Description { get ; set ; }

		/// <summary>
		/// Gets/sets the created date.
		/// </summary>
		[Column(Name="content_created")]
		public override DateTime Created { get ; set ; }

		/// <summary>
		/// Gets/sets the updated date.
		/// </summary>
		[Column(Name="content_updated")]
		public override DateTime Updated { get ; set ; }

		/// <summary>
		/// Gets/sets the user id that created the record.
		/// </summary>
		[Column(Name="content_created_by")]
		public override Guid CreatedBy { get ; set ; }

		/// <summary>
		/// Gets/sets the user id that created the record.
		/// </summary>
		[Column(Name="content_updated_by")]
		public override Guid UpdatedBy { get ; set ; }
		#endregion

		#region Properties
		/// <summary>
		/// Gets the current display name for the content object.
		/// </summary>
		public string DisplayName { 
			get {
				return !String.IsNullOrEmpty(Name) ? Name : Filename ;
			}
		}

		/// <summary>
		/// Gets/sets the possible child content if this is a folder.
		/// </summary>
		public List<Content> ChildContent { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor. Creates a new content object.
		/// </summary>
		public Content() : base("~/App_Data/Content/", "~/App_Data/Cache/Content/") {
			ExtensionType = Extend.ExtensionType.Media ;
			ChildContent = new List<Content>() ;
			LogChanges = true ;
		}

		#region Static accessors
		/// <summary>
		/// Gets a single record.
		/// </summary>
		/// <param name="id">The record id</param>
		/// <returns>The record</returns>
		public static Content GetSingle(Guid id) {
			if (id != Guid.Empty) {
				if (!Cache.Current.Contains(id.ToString()))
					Cache.Current[id.ToString()] = Content.GetSingle((object)id) ;
				return (Content)Cache.Current[id.ToString()] ;
			}
			return null ;
		}

		/// <summary>
		/// Gets the content for the matching id
		/// </summary>
		/// <param name="id">An array of id keys</param>
		/// <returns>A list of content records</returns>
		public static List<Content> GetIn(Guid[] id) {
			if (id.Length > 0) {
				string fmtWhere = PrimaryKeys[0] + " IN ({0})" ;
				string sqlIn = "" ;

				// Build where clause
				for (int n = 0; n < id.Length; n++)
					sqlIn += (sqlIn != "" ? "," : "") + "@" + n ;

				// Format arguments
				object[] args = new object[id.Length] ;
				id.Each((i,e) => args[i] = e);
	
				// Get the records
				return Get(String.Format(fmtWhere, sqlIn), args) ;
			}
			return new List<Content>() ;
		}

		/// <summary>
		/// Gets the folder structure for the first level.
		/// </summary>
		/// <returns>The content</returns>
		public static List<Content> GetStructure() {
			return GetStructure(Guid.Empty) ;
		}

		/// <summary>
		/// Gets the folder structure for the given folder id.
		/// </summary>
		/// <param name="folderid">The folder id</param>
		/// <returns>The content</returns>
		public static List<Content> GetStructure(Guid folderid, bool includeparent = false) {
			List<Content> ret = new List<Content>() ;

			// Add parent
			if (folderid != Guid.Empty && includeparent) {
				var self = Content.GetSingle(folderid) ;
				ret.Add(new Content() {
					Id = self.ParentId,
					ParentId = folderid,
					IsFolder = true,
					Name = ".."
				}) ;
			}

			// Get the folders
			if (folderid == Guid.Empty)
				ret.AddRange(Content.Get("content_folder = 1 AND content_parent_id IS NULL", 
					new Params() { OrderBy = "content_name" })) ;
			else ret.AddRange(Content.Get("content_folder = 1 AND content_parent_id = @0", folderid, 
					new Params() { OrderBy = "content_name" })) ;

			// Get the content
			if (folderid == Guid.Empty)
				ret.AddRange(Content.Get("content_folder = 0 AND content_parent_id IS NULL",
					new Params() { OrderBy = "COALESCE(content_name, content_filename)" })) ;
			else ret.AddRange(Content.Get("content_folder = 0 AND content_parent_id = @0", folderid,
				new Params() { OrderBy = "COALESCE(content_name, content_filename)" })) ;

			return ret ;
		}

		/// <summary>
		/// Gets the full folder structure for the media content.
		/// </summary>
		/// <returns>The folder structure.</returns>
		public static List<Content> GetFolderStructure() {
			return SortStructure(Content.Get("content_folder = 1", new Params() { OrderBy = "content_parent_id, content_name" }), Guid.Empty) ;
		}

		/// <summary>
		/// Gets the content for the given category id.
		/// </summary>
		/// <param name="id">The category id</param>
		/// <returns>A list of content</returns>
		public static List<Content> GetByCategoryId(Guid id) {
			return Content.Get("content_id IN (" +
				"SELECT relation_data_id FROM relation WHERE relation_type = @0 AND relation_related_id = @1)",
				Relation.RelationType.CONTENTCATEGORY, id) ;
		}
		#endregion

		/// <summary>
		/// Gets the thumbnail for the embedded resource with the given id. The thumbnail
		/// resources are specified in Piranha.Drawing.Thumbnails.
		/// </summary>
		/// <param name="context">The current http context</param>
		/// <param name="id">The resource id</param>
		/// <param name="size">The desired size</param>
		public static void GetResourceThumbnail(HttpContext context, Guid id, int size = 60) {
			var content = new Content() {
				Id = id
			} ;

			if (!ClientCache.HandleClientCache(context, content.Id.ToString(), new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime)) {
				if (File.Exists(content.GetCacheThumbPath(size))) {
					content.WriteFile(context.Response, content.GetCacheThumbPath(size)) ;
				} else {
					var resource = Drawing.Thumbnails.GetById(id) ;
					if (size <= 32 && resource.Contains("ico-folder"))
						resource = Drawing.Thumbnails.GetByType("folder-small") ;

					Stream strm = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource) ;
					var img = Image.FromStream(strm) ;
					strm.Close() ;

					// Generate thumbnail from image
					using (Bitmap bmp = new Bitmap(size, size)) {
						Graphics grp = Graphics.FromImage(bmp) ;

						grp.SmoothingMode = SmoothingMode.HighQuality ;
						grp.CompositingQuality = CompositingQuality.HighQuality ;
						grp.InterpolationMode = InterpolationMode.High ;

						// Resize and crop image
						Rectangle dst = new Rectangle(0, 0, bmp.Width, bmp.Height) ;
						grp.DrawImage(img, dst, img.Width > img.Height ? (img.Width - img.Height) / 2 : 0,
							img.Height > img.Width ? (img.Height - img.Width) / 2 : 0, Math.Min(img.Width, img.Height), 
							Math.Min(img.Height, img.Width), GraphicsUnit.Pixel) ;
						bmp.Save(content.GetCacheThumbPath(size), img.RawFormat) ;
						bmp.Dispose() ;
						grp.Dispose() ;
					}
					content.WriteFile(context.Response, content.GetCacheThumbPath(size)) ;

					img.Dispose() ;
				}
			}
		}

		/// <summary>
		/// Gets a thumbnail representing the current content file.
		/// </summary>
		/// <param name="response">The http response</param>
		/// <param name="size">The desired size</param>
		public void GetThumbnail(HttpContext context, int size = 60) {
			bool compress = false ;
			var param = SysParam.GetByName("COMPRESS_IMAGES") ;

			if (param != null && param.Value == "1")
				compress = true ;

			if (!ClientCache.HandleClientCache(context, Id.ToString(), Updated)) {
				if (File.Exists(GetCacheThumbPath(size))) {
					// Return generated & cached thumbnail
					WriteFile(context.Response, GetCacheThumbPath(size), compress) ;
				} else if (File.Exists(PhysicalPath)) { // || IsFolder) {
					var img = Image.FromFile(PhysicalPath) ;

					if (img != null) {
						// Generate thumbnail from image
						using (Bitmap bmp = new Bitmap(size, size)) {
							Graphics grp = Graphics.FromImage(bmp) ;

							grp.SmoothingMode = SmoothingMode.HighQuality ;
							grp.CompositingQuality = CompositingQuality.HighQuality ;
							grp.InterpolationMode = InterpolationMode.High ;

							// Resize and crop image
							Rectangle dst = new Rectangle(0, 0, bmp.Width, bmp.Height) ;
							grp.DrawImage(img, dst, img.Width > img.Height ? (img.Width - img.Height) / 2 : 0,
								img.Height > img.Width ? (img.Height - img.Width) / 2 : 0, Math.Min(img.Width, img.Height), 
								Math.Min(img.Height, img.Width), GraphicsUnit.Pixel) ;

							bmp.Save(GetCacheThumbPath(size), compress ? System.Drawing.Imaging.ImageFormat.Jpeg : img.RawFormat) ;
						}
						WriteFile(context.Response, GetCacheThumbPath(size), compress) ;
					} 
				}
			}
		}

		/// <summary>
		/// Gets the total size of the content on disk including all cached thumbnails.
		/// </summary>
		/// <returns>The total size in bytes</returns>
		public long GetTotalSize() {
			long total = Size ;

			DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath(CachePath)) ;
			foreach (FileInfo file in dir.GetFiles(Id.ToString() + "*")) {
				total += file.Length ;
			}
			return Math.Max(total, 1024) ;
		}

		/// <summary>
		/// Invalidates the cache for the given record.
		/// </summary>
		/// <param name="record">The record</param>
		public void InvalidateRecord(Content record) {
			Cache.Current.Remove(record.Id.ToString()) ;
		}

		/// <summary>
		/// Checks if the current entity has a child with the given id.
		/// </summary>
		/// <param name="id">The content id</param>
		/// <returns>Weather the child was found</returns>
		public bool HasChild(Guid id) {
			if (ChildContent != null) {
				foreach (var c in ChildContent)
					if (HasChild(c, id))
						return true ;
			}
			return false ;
		}

		#region Private methods
		/// <summary>
		/// Checks if the current entity has a child with the given id.
		/// </summary>
		/// <param name="content">The content</param>
		/// <param name="id">The content id</param>
		/// <returns>Weather the child was found</returns>
		private bool HasChild(Content content, Guid id) {
			if (content.Id == id)
				return true ;
			if (content.ChildContent != null) {
				foreach (var c in content.ChildContent)
					if (HasChild(c, id))
						return true ;
			}
			return false ;
		}

		private static List<Content> SortStructure(List<Content> content, Guid parentid) {
			var ret = content.Where(c => c.ParentId == parentid).OrderBy(c => c.Name).ToList() ;

			ret.ForEach(r => r.ChildContent = SortStructure(content, r.Id)) ;

			return ret ;
		}
		#endregion
	}
}
