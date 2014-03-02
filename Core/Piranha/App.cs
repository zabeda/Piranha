using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Web;

using Piranha.Web;
using Piranha.Web.Handlers;

namespace Piranha
{
	/// <summary>
	/// The application class contains the main state and configuration
	/// for the current running web application.
	/// </summary>
	public sealed class App
	{
		#region Members
		/// <summary>
		/// Static singleton instance of the application.
		/// </summary>
		public static readonly App Instance = new App() ;

		/// <summary>
		/// The collection of registered request handlers.
		/// </summary>
		public readonly RequestHandlerCollection Handlers = new RequestHandlerCollection() ;

		/// <summary>
		/// Gets the current IoC container.
		/// </summary>
		public readonly IoC.IContainer IoCContainer;

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
		/// Gets the current cache provider.
		/// </summary>
		public Cache.ICacheProvider CacheProvider {
			get { return IoCContainer.Resolve<Cache.ICacheProvider>(); }
		}

		/// <summary>
		/// Gets the current log provider.
		/// </summary>
		public Log.ILogProvider LogProvider {
			get { return IoCContainer.Resolve<Log.ILogProvider>(); }
		}

		/// <summary>
		/// Get the current media provider.
		/// </summary>
		public IO.IMediaProvider MediaProvider {
			get { return IoCContainer.Resolve<IO.IMediaProvider>(); }
		}

		/// <summary>
		/// Gets the current media cache provider.
		/// </summary>
		public IO.IMediaCacheProvider MediaCacheProvider {
			get { return IoCContainer.Resolve<IO.IMediaCacheProvider>(); }
		}

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
		private App() {
			var catalog = new AggregateCatalog() ;

			// Register IoC container
			if (Hooks.App.Init.CreateContainer != null)
				IoCContainer = Hooks.App.Init.CreateContainer();
			else IoCContainer = new IoC.TinyIoCContainer();

			// Register log provider
			if (Hooks.App.Init.RegisterLog != null)
				Hooks.App.Init.RegisterLog(IoCContainer);
			else IoCContainer.RegisterSingleton<Log.ILogProvider, Log.LocalLogProvider>();

			// Regsiter cache provider
			if (Hooks.App.Init.RegisterCache != null)
				Hooks.App.Init.RegisterCache(IoCContainer);
			else IoCContainer.RegisterSingleton<Cache.ICacheProvider, Cache.WebCacheProvider>();

			// Register media provider
			if (Hooks.App.Init.RegisterMedia != null)
				Hooks.App.Init.RegisterMedia(IoCContainer);
			else IoCContainer.RegisterSingleton<IO.IMediaProvider, IO.LocalMediaProvider>();

			// Register media cache provider
			if (Hooks.App.Init.RegisterMediaCache != null)
				Hooks.App.Init.RegisterMediaCache(IoCContainer);
			else IoCContainer.RegisterSingleton<IO.IMediaCacheProvider, IO.LocalMediaCacheProvider>();

			// Register additional types
			if (Hooks.App.Init.Register != null)
				Hooks.App.Init.Register(IoCContainer);

			// Compose parts
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