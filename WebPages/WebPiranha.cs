using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;
using System.Web.Compilation;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;

using Piranha.Models;
using Piranha.WebPages.RequestHandlers;

namespace Piranha.WebPages
{
	public static class WebPiranha
	{
		#region Members
		/// <summary>
		/// The different request handlers
		/// </summary>
		private static Dictionary<string, RequestHandlerRegistration> Handlers = new Dictionary<string, RequestHandlerRegistration>() ;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the current application root.
		/// </summary>
		public static string ApplicationPath {
			get {
				string root = HttpContext.Current.Request.ApplicationPath ;
				if (!root.EndsWith("/"))
					root += "/" ;
				return root ;
			}
		}
		#endregion

		/// <summary>
		/// Registers the given 
		/// </summary>
		/// <param name="urlprefix">The url prefix</param>
		/// <param name="id">The handler id</param>
		/// <param name="handler">The actual handler</param>
		public static void RegisterHandler(string urlprefix, string id, IRequestHandler handler) {
			Handlers.Add(id.ToUpper(), new RequestHandlerRegistration() { UrlPrefix = urlprefix, Id = id, Handler = handler }) ;
		}

		/// <summary>
		/// Gets the current url prefix used for the given handler id.
		/// </summary>
		/// <param name="id">The handler id</param>
		/// <returns>The url prefix</returns>
		public static string GetUrlPrefixForHandlerId(string id) {
			if (Handlers.ContainsKey(id.ToUpper()))
				return Handlers[id.ToUpper()].UrlPrefix ;
			return "" ;
		}

		/// <summary>
		/// Clears all of the currently registered handlers.
		/// </summary>
		public static void ResetHandlers() {
			Handlers.Clear() ;
		}

		/// <summary>
		/// Registers all of the default request handlers.
		/// </summary>
		public static void RegisterDefaultHandlers() {
			RegisterHandler("", "STARTPAGE", new PermalinkHandler()) ;
			RegisterHandler("home", "PERMALINK", new PermalinkHandler()) ;
			RegisterHandler("draft", "DRAFT", new DraftHandler()) ;
			RegisterHandler("media", "CONTENT", new ContentHandler()) ;
			RegisterHandler("thumb", "THUMBNAIL", new ThumbnailHandler()) ;
			RegisterHandler("upload", "UPLOAD", new UploadHandler()) ;
			RegisterHandler("account", "ACCOUNT", new AccountHandler()) ;
			RegisterHandler("archive", "ARCHIVE", new ArchiveHandler()) ;
		}

		/// <summary>
		/// Initializes the webb app.
		/// </summary>
		public static void Init() {
			// Register virtual path provider for the manager area. This part includes a nasty hack for 
			// precompiled sites due to Microsofts implementation in the .NET framework. See
			//
			// http://sunali.com/2008/01/09/virtualpathprovider-in-precompiled-web-sites/
			//
			// for more information on the issue
			//
			PropertyInfo pc = typeof(BuildManager).GetProperty("IsPrecompiledApp", BindingFlags.NonPublic | BindingFlags.Static) ;
			if (pc != null && (bool)pc.GetValue(null, null)) {
				// This is a precompiled application, bend the framework a bit.
				HostingEnvironment instance = (HostingEnvironment)typeof(HostingEnvironment).InvokeMember("_theHostingEnvironment", 
					BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetField, null, null, null) ;
				if (instance == null)
					throw new NullReferenceException("Can't get the current hosting environment") ;
				MethodInfo m = typeof(HostingEnvironment).GetMethod("RegisterVirtualPathProviderInternal", BindingFlags.NonPublic | BindingFlags.Static) ;
				if (m == null)
					throw new NullReferenceException("Can't get the RegisterVirtualPathProviderInternal method") ;
				m.Invoke(instance, new object[] { (VirtualPathProvider)new Piranha.Web.ResourcePathProvider() });
			} else {
				HostingEnvironment.RegisterVirtualPathProvider(new Piranha.Web.ResourcePathProvider()) ;
			}

			// This will trigger the manager area registration
			AreaRegistration.RegisterAllAreas() ;

			// Register handlers
			RegisterDefaultHandlers() ;
		}

		/// <summary>
		/// Initializes the manager app.
		/// </summary>
		/// <param name="context"></param>
		public static void InitManager(AreaRegistrationContext context) {
			// Register manager routing
			context.MapRoute(
				"Manager",
				"manager/{controller}/{action}/{id}",
				new { controller = "Page", action = "Index", id = UrlParameter.Optional }
			) ;

			// Register filters & binders
			RegisterGlobalFilters(GlobalFilters.Filters) ;
			RegisterBinders() ;
		}

		/// <summary>
		/// Registers all of the default rest wcf services.
		/// </summary>
		public static void InitServices() {
			RouteTable.Routes.Add("REST_CATEGORY", new ServiceRoute("rest/category", new WebServiceHostFactory(), typeof(Rest.CategoryService))) ;
			RouteTable.Routes.Add("REST_SITEMAP", new ServiceRoute("rest/sitemap", new WebServiceHostFactory(), typeof(Rest.SitemapServices))) ;
			RouteTable.Routes.Add("REST_PAGE", new ServiceRoute("rest/page", new WebServiceHostFactory(), typeof(Rest.PageService))) ;
			RouteTable.Routes.Add("REST_CONTENT", new ServiceRoute("rest/content", new WebServiceHostFactory(), typeof(Rest.ContentService))) ;
			RouteTable.Routes.Add("REST_CHANGES", new ServiceRoute("rest/changes", new WebServiceHostFactory(), typeof(Rest.ChangeService))) ;
		}

		/// <summary>
		/// Handles the URL Rewriting for the application
		/// </summary>
		/// <param name="context">Http context</param>
		public static void BeginRequest(HttpContext context) {
			string path = context.Request.Path.Substring(context.Request.ApplicationPath.Length > 1 ? 
				context.Request.ApplicationPath.Length : 0) ;

			string[] args = path.Split(new char[] {'/'}).Subset(1) ;
				
			if (args.Length > 0) {
				foreach (RequestHandlerRegistration hr in Handlers.Values) {
					if (hr.UrlPrefix.ToLower() == args[0].ToLower()) {
						hr.Handler.HandleRequest(context, args.Subset(1)) ;
						break ;
					}
				}
			}
		}

		/// <summary>
		/// Handles current UI culture.
		/// </summary>
		/// <param name="context">The http context</param>
		public static void HandleCulture(HttpContext context) {
			//
			// NOTE: This code will fail completely in the manager view as accessing the request 
			// collection triggers the form data validation.
			//
			try {
				if (context.Request.HttpMethod.ToUpper() == "POST") {
					if (!String.IsNullOrEmpty(context.Request["lang"]))
						context.Session["lang"] = context.Request["lang"] ;
				}
				if (context.Session != null && context.Session["lang"] != null)
					System.Threading.Thread.CurrentThread.CurrentUICulture =
						new System.Globalization.CultureInfo((string)context.Session["lang"]) ;
			} catch {}
		}

		#region Private methods
		/// <summary>
		/// Registers all global filters.
		/// </summary>
		/// <param name="filters">The current filter collection</param>
		private static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());
		}

		/// <summary>
		/// Registers all custom binders.
		/// </summary>
		private static void RegisterBinders() {
			ModelBinders.Binders.Add(typeof(Piranha.Models.Manager.PageModels.EditModel), 
				new Piranha.Models.Manager.PageModels.EditModel.Binder()) ;
			ModelBinders.Binders.Add(typeof(Piranha.Models.Manager.PostModels.EditModel), 
				new Piranha.Models.Manager.PostModels.EditModel.Binder()) ;
			ModelBinders.Binders.Add(typeof(Piranha.Models.Manager.TemplateModels.PageEditModel),
				new Piranha.Models.Manager.TemplateModels.PageEditModel.Binder()) ;
			ModelBinders.Binders.Add(typeof(Piranha.Models.Manager.TemplateModels.PostEditModel),
				new Piranha.Models.Manager.TemplateModels.PostEditModel.Binder()) ;
		}
		#endregion
	}
}
