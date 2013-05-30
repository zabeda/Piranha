using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(Piranha.Web.ApplicationModule), "Start")]

namespace Piranha.Web
{
	/// <summary>
	/// The application module registers the main Piranha CMS application, initializes
	/// the module and handles all requests to the application.
	/// </summary>
	public class ApplicationModule : IHttpModule
	{
		/// <summary>
		/// Disposes all allicated resources.
		/// </summary>
		public void Dispose() {}

		/// <summary>
		/// Starts the application module.
		/// </summary>
		public static void Start() {
			// Register the application module
			Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(ApplicationModule)) ;

			// Initialize Piranha CMS
			WebPages.WebPiranha.Init() ;
			
			// Don't register the services automatically
			// WebPages.WebPiranha.InitServices() ;
		}

		/// <summary>
		/// Executed for all requests in the application
		/// </summary>
		/// <param name="context">The current application context</param>
		public void Init(HttpApplication context) {
			context.BeginRequest += (sender, e) => {
				WebPages.WebPiranha.BeginRequest(((HttpApplication)sender).Context) ;
			} ;
		}
	}
}