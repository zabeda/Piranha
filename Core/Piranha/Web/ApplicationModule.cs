using System;
using System.Collections.Generic;
using System.Linq;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(Piranha.Web.ApplicationModule), "Start")]
[assembly: PostApplicationStartMethod(typeof(Piranha.Web.ApplicationModule), "Init")]

namespace Piranha.Web
{
	/// <summary>
	/// The application module registers the main Piranha CMS application, initializes
	/// the module and handles all requests to the application.
	/// </summary>
	public class ApplicationModule : System.Web.IHttpModule
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
		}

		/// <summary>
		/// Initializes the application module.
		/// </summary>
		public static void Init() { 
			App.Init();
		}

		/// <summary>
		/// Executed for all requests in the application
		/// </summary>
		/// <param name="context">The current application context</param>
		public void Init(System.Web.HttpApplication context) {
			context.BeginRequest += (sender, e) => {
				WebPages.WebPiranha.BeginRequest(((System.Web.HttpApplication)sender).Context) ;
			} ;
		}
	}
}