using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;

namespace Piranha.Extend
{
	/// <summary>
	/// Attribute to define the meta data for an extension.
	/// </summary>
	public class ExtensionAttribute : Attribute
	{
		#region Members
		private string name ;
		private string resourcename ;
		#endregion

		/// <summary>
		/// Gets/sets the name of the extension.
		/// </summary>
		public string Name { 
			get {
				if (ResourceType != null && !String.IsNullOrEmpty(name)) {
					if (resourcename == null) {
						var mgr = new ResourceManager(ResourceType) ;
						resourcename = mgr.GetString(name) ;
					}
					return resourcename ;
				}
				return name ;
			}
			set {
				name = value ;
			}
		}

		/// <summary>
		/// Gets/sets the optional name of the resource class.
		/// </summary>
		public Type ResourceType { get ; set ; }

		/// <summary>
		/// Gets/sets the internal id of the extension.
		/// </summary>
		public string InternalId { get ; set ; }

		/// <summary>
		/// Gets/sets the possible icon path used in the
		/// template preview in the manager interface.
		/// </summary>
		public string IconPath { get ; set ; }

		/// <summary>
		/// Gets/sets what kind of extension this is.
		/// </summary>
		public ExtensionType Type { get ; set ; }
		
		/// <summary>
		/// Default constructor. Creates a new attribute.
		/// </summary>
		public ExtensionAttribute() {
			Type = ExtensionType.NotSet ;
		}
	}
}
