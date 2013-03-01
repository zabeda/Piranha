using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Piranha.Extend
{
	/// <summary>
	/// Interface defining the meta data availble to MEF for extensions.
	/// </summary>
	public interface IExtensionMeta
	{
		#region Properties
		/// <summary>
		/// Gets the display name of the extension used in the manager.
		/// </summary>
		string Name { get ; }

		/// <summary>
		/// Gets the internal id of the extensions used to access it from
		/// the application. This data is not neccessary for Regions as
		/// they are assigned an internal id in the template.
		/// </summary>
		[DefaultValue("")]
		string InternalId { get ; }

		/// <summary>
		/// Gets the optional virtual path to the icon that will be used in
		/// the manager when presenting the extension.
		/// </summary>
		[DefaultValue("~/areas/manager/content/img/ico-missing-ico.png")]
		string IconPath { get ; }

		/// <summary>
		/// Gets the extension type.
		/// </summary>
		ExtensionType Type { get ; }
		#endregion
	}
}