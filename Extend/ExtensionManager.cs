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
		internal static Dictionary<string, Type> RegionTypes = new Dictionary<string,Type>() ;

		/// <summary>
		/// All available property types.
		/// </summary>
		internal static Dictionary<string, Type> PropertyTypes = new Dictionary<string,Type>() ;

		/// <summary>
		/// The private list of regions.
		/// </summary>
		private static List<Extension> regions = null ;

		/// <summary>
		/// The private list of properties.
		/// </summary>
		private static List<Extension> properties = null ;

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
		/// Gets the currently available property types.
		/// </summary>
		public static List<Extension> Properties {
			get {
				if (properties == null) {
					properties = new List<Extension>() ;
					PropertyTypes.Keys.Each((i, e) => 
						properties.Add(new Extension() { Name = GetPropertyNameByType(e), Type = PropertyTypes[e] })) ;
				}
				return regions ;
			}
		}

		/// <summary>
		/// Gets the user extensions.
		/// </summary>
		public static List<Extension> UserExtensions {
			get {
				if (extensions != null)
					return extensions.Where(e => e.ExtensionType == ExtensionType.User).ToList() ;
				return new List<Extension>() ;
			}
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
					//if (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRegion<>))) {
						RegionTypes.Add(type.FullName, type) ;
					}
					// Get all available properties
					if (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IProperty<>))) {
						PropertyTypes.Add(type.FullName, type) ;
					}

					// Get all general extensions.
					var attr = type.GetCustomAttribute<ExtensionAttribute>(false) ;
					if (attr != null) {
						if (attr.Type != ExtensionType.NotSet) {
							if (attr.Type == ExtensionType.User) {
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
		/// Gets the name for the given region type.
		/// </summary>
		/// <param name="type">The type</param>
		/// <returns>The name</returns>
		public static string GetPropertyNameByType(string type) {
			if (PropertyTypes.ContainsKey(type)) {
				var attr = PropertyTypes[type].GetCustomAttribute<ExtensionAttribute>(false) ;
				if (attr != null)
					return attr.Name ;
				return PropertyTypes[type].Name ;
			}
			return "" ;
		}
	}
}
