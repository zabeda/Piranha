using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Piranha.Extend
{
	/// <summary>
	/// The extension manager handles all regions, properties & plugins.
	/// </summary>
	public static class ExtensionManager
	{
		#region Members
		/// <summary>
		/// All available region types.
		/// </summary>
		public static Dictionary<string, Type> RegionTypes = new Dictionary<string,Type>() ;

		/// <summary>
		/// All available extension types.
		/// </summary>
		public static Dictionary<string, Type> ExtensionTypes = new Dictionary<string,Type>() ;

		/// <summary>
		/// The private list of regions.
		/// </summary>
		private static List<Extension> regions = null ;

		/// <summary>
		/// The private list of extensions.
		/// </summary>
		private static List<Extension> extensions = new List<Extension>() ;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the currently available region types.
		/// </summary>
		public static List<Extension> Regions {
			get {
				if (regions == null) {
					regions = new List<Extension>() ;
					RegionTypes.Keys.Each((i, e) => 
						regions.Add(new Extension() { Name = GetRegionNameByType(e), Type = RegionTypes[e] })) ;
				}
				return regions ;
			}
		}

		/// <summary>
		/// Gets the currently available extensions.
		/// </summary>
		public static List<Extension> Extensions {
			get { return extensions ; }
		}
		#endregion

		/// <summary>
		/// Initializes the extension manager. This method should be invoked
		/// on application start.
		/// </summary>
		internal static void Init() {
			// Get all loaded assemblies
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
				// Iterate all types
				foreach (var type in assembly.GetTypes()) {
					// Get all available regions
					if (typeof(IRegion).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract) {
						RegionTypes.Add(type.FullName, type) ;
					}

					// Get all general extensions.
					if (typeof(IExtension).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract) {
						var attr = type.GetCustomAttribute<ExtensionAttribute>(false) ;
						if (attr != null) {
							if (attr.Type != ExtensionType.NotSet) {
								ExtensionTypes.Add(type.FullName, type) ;
								extensions.Add(new Extension() {
									ExtensionType = attr.Type,
									Name = attr.Name,
									Type = type
								});
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Gets the name for the given region type.
		/// </summary>
		/// <param name="type">The type</param>
		/// <returns>The name</returns>
		public static string GetRegionNameByType(string type) {
			if (RegionTypes.ContainsKey(type)) {
				var attr = RegionTypes[type].GetCustomAttribute<ExtensionAttribute>(false) ;
				if (attr != null)
					return attr.Name ;
				return RegionTypes[type].Name ;
			} 
			return "" ;
		}

		/// <summary>
		/// Gets the name for the given extension type.
		/// </summary>
		/// <param name="type">The type</param>
		/// <returns>The name</returns>
		public static string GetExtensionNameByType(string type) {
			if (ExtensionTypes.ContainsKey(type)) {
				var attr = ExtensionTypes[type].GetCustomAttribute<ExtensionAttribute>(false) ;
				if (attr != null)
					return attr.Name ;
				return ExtensionTypes[type].Name ;
			} 
			return "" ;
		}

		/// <summary>
		/// Gets the extensions available for the given type.
		/// </summary>
		/// <param name="type">The extension type</param>
		/// <param name="draft">Weather the entity is draft or not</param>
		/// <returns>A list of extensions</returns>
		public static List<Models.Extension> GetByType(ExtensionType type, bool draft = false) {
			var ext = new List<Models.Extension>() ;

			extensions.Where(extension => extension.ExtensionType == type).ToList().ForEach(e => {
				ext.Add(new Models.Extension() {
					IsDraft = draft,
					Type = e.Type.ToString(),
					Body = (IExtension)Activator.CreateInstance(e.Type)
				}) ;
			});
			return ext ;
		}

		/// <summary>
		/// Gets the extensions available for the given type and entity.
		/// </summary>
		/// <param name="type">The extension type</param>
		/// <param name="id">The entity id</param>
		/// <param name="draft">Weather the entity is draft or not</param>
		/// <returns>A list of extensions</returns>
		public static List<Models.Extension> GetByTypeAndEntity(ExtensionType type, Guid id, bool draft) {
			var ret = new List<Models.Extension>() ;
			var tmp = GetByType(type, draft) ;

			foreach (var e in tmp) {
				var ext = Models.Extension.GetSingle("extension_type = @0 AND extension_parent_id = @1 AND extension_draft = @2",
					e.Type, id, draft) ;
				if (ext != null)
					ret.Add(ext) ;
				else ret.Add(e) ;
			}
			return ret ;
		}
	}
}
