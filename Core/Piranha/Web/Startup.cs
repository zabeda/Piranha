using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.WebPages;
using RazorGenerator.Mvc;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(Piranha.Web.Startup), "PreInit")]
[assembly: PostApplicationStartMethod(typeof(Piranha.Web.Startup), "Init")]

namespace Piranha.Web
{
	/// <summary>
	/// Starts the Piranha CMS web application.
	/// </summary>
	public sealed class Startup
	{
		#region Members
		private static string[] partialViewFolders = new[] { 
			"~/Areas/Manager/Views/Extensions/{0}.cshtml" 
		};
		#endregion

		/// <summary>
		/// Initializes the pre application start.
		/// </summary>
		public static void PreInit() {
			// Registers the http module
			Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(ApplicationModule));

			// Initialize the manager module
			Manager.ManagerModule.Init();

			// Intializes the web application
			WebPages.WebPiranha.Init();
		}

		/// <summary>
		/// Initializes the application start.
		/// </summary>
		public static void Init() {
			// Initializes the main application object
			App.Init();
	
			// Registers view engines
			RegisterViewEngines();	
		}

		#region Private methods
		private static void RegisterViewEngines() { 
			// Register the standard view engine
			var standard = new RazorViewEngine() ;
			standard.PartialViewLocationFormats = standard.PartialViewLocationFormats.Union(partialViewFolders).ToArray() ;
			ViewEngines.Engines.Add(standard) ;

			// Get precompiled view assemblies
			var assemblies = new List<Assembly>();

			if (Hooks.App.Init.RegisterPrecompiledViews != null)
				Hooks.App.Init.RegisterPrecompiledViews(assemblies);
			assemblies.Insert(0, typeof(Startup).Assembly);

			foreach (var assembly in assemblies) { 
				var engine = new PrecompiledMvcEngine(assembly) {
					UsePhysicalViewsIfNewer = true
				} ;
				engine.PartialViewLocationFormats = engine.PartialViewLocationFormats.Union(partialViewFolders).ToArray();

				ViewEngines.Engines.Add(engine);
				VirtualPathFactoryManager.RegisterVirtualPathFactory(engine) ;
			}
		}
		#endregion
	}
}