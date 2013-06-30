using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Web;

using Piranha.Web;
using Piranha.Web.Handlers;
using Piranha.WebPages.RequestHandlers;

namespace Piranha
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
		/// The currently active media provider.
		/// </summary>
		public readonly IO.IMediaProvider MediaProvider;

		/// <summary>
		/// The currently active cache provider.
		/// </summary>
        public readonly Cache.ICacheProvider CacheProvider;

        /// <summary>
        /// The currently active log provider.
        /// </summary>
        public readonly Log.ILogProvider LogProvider;

		/// <summary>
		/// The currently active user provider.
		/// </summary>
		internal readonly Security.IUserProvider UserProvider = new Security.LocalUserProvider() ;

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

			// Compose parts
			catalog.Catalogs.Add(new DirectoryCatalog("Bin")) ;
			Container = new CompositionContainer(catalog) ;
			Container.ComposeParts(this) ;

			// Get the current media provider
			var assembly = Assembly.Load(Config.MediaProvider.AssemblyName) ;
			if (assembly != null) {
				var type = assembly.GetType(Config.MediaProvider.TypeName) ;
				if (type != null)
					MediaProvider = (IO.IMediaProvider)Activator.CreateInstance(type) ;
				else throw new TypeAccessException("MediaProvider " + Config.MediaProvider.TypeName + " was not found") ;
			}

			// Get the current cache provider
			assembly = Assembly.Load(Config.CacheProvider.AssemblyName) ;
			if (assembly != null) {
				var type = assembly.GetType(Config.CacheProvider.TypeName) ;
				if (type != null)
					CacheProvider = (Cache.ICacheProvider)Activator.CreateInstance(type) ;
				else throw new TypeAccessException("CacheProvider " + Config.CacheProvider.TypeName + " was not found") ;				
			}

            // Get the current log provider
            assembly = Assembly.Load(Config.LogProvider.AssemblyName);
            if (assembly != null)
            {
                var type = assembly.GetType(Config.LogProvider.TypeName);
                if (type != null)
                    LogProvider = (Log.ILogProvider)Activator.CreateInstance(type);
                else throw new TypeAccessException("LogProvider " + Config.LogProvider.TypeName + " was not found");
            }

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
			Handlers.Add("media.ashx", "CONTENTHANDLER", new ContentHandler()) ;
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