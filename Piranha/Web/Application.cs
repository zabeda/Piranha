using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web;

using Piranha.WebPages.RequestHandlers;
using Piranha.Web.Handlers;

namespace Piranha.Web
{
	/// <summary>
	/// The application class contains the main state and configuration
	/// for the current running web application.
	/// </summary>
	public class Application
	{
		#region Members
		/// <summary>
		/// Static singleton instance of the application.
		/// </summary>
		public static readonly Application Current = new Application() ;

		/// <summary>
		/// The collection of registered request handlers.
		/// </summary>
		public readonly RequestHandlerCollection Handlers = new RequestHandlerCollection() ;

		/// <summary>
		/// The manager resource handler.
		/// </summary>
		internal readonly ResourceHandler Resources = new ResourceHandler() ;

		/// <summary>
		/// The private composition container.
		/// </summary>
		private CompositionContainer Container = null ;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the current route handler.
		/// </summary>
		[Import(typeof(IRouteHandler), AllowDefault=true)]
		public IRouteHandler RouteHandler { get ; private set ; }

		/// <summary>
		/// Gets the current client framework.
		/// </summary>
		[Import(typeof(IClientFramework), AllowDefault=true)]
		public IClientFramework ClientFramework { get ; private set ; }

		/// <summary>
		/// Gets if the currently installed client framework is intended for WebPages.
		/// </summary>
		public bool IsWebPages {
			get { return ClientFramework != null && ClientFramework.Type == FrameworkType.WebPages ; }
		}

		/// <summary>
		/// Gets if the currently installed client framework is intended for MVC.
		/// </summary>
		public bool IsMvc {
			get { return ClientFramework != null && ClientFramework.Type == FrameworkType.Mvc ; }
		}
		#endregion

		/// <summary>
		/// Default private constructor.
		/// </summary>
		private Application() {
			var catalog = new AggregateCatalog() ;

			catalog.Catalogs.Add(new DirectoryCatalog("Bin")) ;
			Container = new CompositionContainer(catalog) ;
			Container.ComposeParts(this) ;

			RegisterHandlers() ;
		}
	
		/// <summary>
		/// Registers the default handlers.
		/// </summary>
		private void RegisterHandlers() {
			Handlers.Add("", "STARTPAGE", new PermalinkHandler()) ;
			Handlers.Add("home", "PERMALINK", new PermalinkHandler()) ;
			Handlers.Add("draft", "DRAFT", new DraftHandler()) ;
			Handlers.Add("media", "CONTENT", new ContentHandler()) ;
			Handlers.Add("mediadraft", "CONTENTDRAFT", new DraftContentHandler()) ;
			Handlers.Add("thumb", "THUMBNAIL", new ThumbnailHandler()) ;
			Handlers.Add("thumbdraft", "THUMBNAILDRAFT", new DraftThumbnailHandler()) ;
			Handlers.Add("upload", "UPLOAD", new UploadHandler()) ;
			Handlers.Add("archive", "ARCHIVE", new ArchiveHandler()) ;
			Handlers.Add("rss", "RSS", new RssHandler()) ;
			Handlers.Add("sitemap.xml", "SITEMAP", new SitemapHandler()) ;
		}
	}
}