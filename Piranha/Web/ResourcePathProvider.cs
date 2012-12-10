using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace Piranha.Web
{
	public class ResourceFile 
	{
		public Assembly Assembly { get ; set ; }
		public string Name { get ; set ; }
	}

	/// <summary>
	/// Provider that serves embedded content from the manager area.
	/// </summary>
	public class ResourcePathProvider : VirtualPathProvider
	{
		#region Properties
		/// <summary>
		/// Gets the cached resource map.
		/// </summary>
		public static Dictionary<string, ResourceFile> Resources {
			get {
				if (HttpContext.Current.Cache["ResourceMap"] == null) {
					HttpContext.Current.Cache["ResourceMap"] = new Dictionary<string, ResourceFile>() ;
					string[] names = Assembly.GetExecutingAssembly().GetManifestResourceNames() ;
					foreach (string name in names)
						((Dictionary<string, ResourceFile>)HttpContext.Current.Cache["ResourceMap"]).Add(name.ToLower(), 
							new ResourceFile() { Assembly = Assembly.GetExecutingAssembly(), Name = name}) ;
					foreach (var assembly in Extend.ExtensionManager.Modules) {
						names = assembly.GetManifestResourceNames() ;
						foreach (string name in names)
							((Dictionary<string, ResourceFile>)HttpContext.Current.Cache["ResourceMap"]).Add(name.ToLower(), 
								new ResourceFile() { Assembly = assembly, Name = name}) ;
						}
				}
				return (Dictionary<string, ResourceFile>)HttpContext.Current.Cache["ResourceMap"] ;
			}
		}
		#endregion

		/// <summary>
		/// Default constructor
		/// </summary>
		public ResourcePathProvider() {}

		/// <summary>
		/// Checks if the given virtual file exists in the assembly
		/// </summary>
		/// <param name="virtualpath">The virtual path</param>
		/// <returns>If the path exists</returns>
		private bool IsResourceFile(string virtualpath) {
			var path = VirtualPathUtility.ToAppRelative(virtualpath) ;

			if (path.ToLower().StartsWith("~/areas/manager/")) {
				string name = path.Replace("/", ".").Replace("~", "Piranha").Replace("res.ashx.", "").ToLower() ;
				// Check core namespace
				ManifestResourceInfo info = Resources.ContainsKey(name) ? 
					Resources[name].Assembly.GetManifestResourceInfo(Resources[name].Name) : null ;
				if (info == null) {
					// Check modules with different namespace
					foreach (var assembly in Extend.ExtensionManager.Modules) {
						name = path.Replace("/", ".").Replace("~", assembly.GetName().Name).Replace("res.ashx.", "").ToLower() ;
						info = Resources.ContainsKey(name) ? 
							Resources[name].Assembly.GetManifestResourceInfo(Resources[name].Name) : null ;
						if (info != null)
							break ;
					}
				}
				return !File.Exists(HttpContext.Current.Server.MapPath(virtualpath)) && info != null ;
			}
			return false ;
		}

		/// <summary>
		/// Checks if the file located at the given path exists.
		/// </summary>
		/// <param name="virtualpath">Theh virtual path</param>
		/// <returns>If the file exists</returns>
        public override bool FileExists(string virtualpath) {
            return (IsResourceFile(virtualpath) || Previous.FileExists(virtualpath)) ;
        }

		/// <summary>
		/// Gets the resource file with at the given virtual path.
		/// </summary>
		/// <param name="virtualpath">The virtual file</param>
		/// <returns>The virtual file</returns>
        public override VirtualFile GetFile(string virtualpath) {
            if (IsResourceFile(virtualpath))
                return new ResourceVirtualFile(virtualpath) ;
            return Previous.GetFile(virtualpath) ;
        }

		/// <summary>
		/// Gets the cache dependency
		/// </summary>
		/// <param name="virtualpath">The virtual path</param>
		/// <param name="dependencies">The dependencies</param>
		/// <param name="start">Start date</param>
		/// <returns>The dependency</returns>
        public override CacheDependency GetCacheDependency(string virtualpath, IEnumerable dependencies, DateTime start) {
            if (IsResourceFile(virtualpath))
				return new CacheDependency(Assembly.GetExecutingAssembly().Location, start) ;
            return Previous.GetCacheDependency(virtualpath, dependencies, start) ;
        }
	}

	/// <summary>
	/// Class for defining a virtual file embedded as a resource.
	/// </summary>
    public class ResourceVirtualFile : VirtualFile
	{
		#region Members
		private string path ;
		#endregion

		/// <summary>
		/// Default constructor. Creates a new resource file for the given
		/// virtual path.
		/// </summary>
		/// <param name="virtualpath">The virtual path</param>
		public ResourceVirtualFile(string virtualpath) : base(virtualpath) {
            path = VirtualPathUtility.ToAppRelative(virtualpath) ;
        }

		/// <summary>
		/// Opens the stream for the current resource.
		/// </summary>
		/// <returns>The stream</returns>
        public override Stream Open() {
			string res = path.Replace("/", ".").Replace("~", "Piranha").Replace("res.ashx.", "").ToLower() ;
			// Try to get resource from core namespace
			if (ResourcePathProvider.Resources.ContainsKey(res))
				return ResourcePathProvider.Resources[res].Assembly.GetManifestResourceStream(
					ResourcePathProvider.Resources[res].Name) ;
			// if not found, scan modules
			foreach (var assembly in Extend.ExtensionManager.Modules) {
				res = path.Replace("/", ".").Replace("~", assembly.GetName().Name).Replace("res.ashx.", "").ToLower() ;
				if (ResourcePathProvider.Resources.ContainsKey(res))
					return ResourcePathProvider.Resources[res].Assembly.GetManifestResourceStream(
						ResourcePathProvider.Resources[res].Name) ;
			}
			return null ;
        }
    }
}
