using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Piranha.Extend.Regions
{
	/// <summary>
	/// Simple text region.
	/// </summary>
	[Extension(Name="TextRegionName", InternalId="TextRegion", ResourceType=typeof(Piranha.Resources.Extensions), Type=ExtensionType.Region)]
	[Serializable]
	public class TextRegion : IExtension
	{
		#region Properties
		/// <summary>
		/// Gets/sets the text title.
		/// </summary>
		[Display(ResourceType=typeof(Piranha.Resources.Extensions), Name="TextRegionTitle")]
		public string Title { get ; set ; }

		/// <summary>
		/// Gets/sets the text body.
		/// </summary>
		[Display(ResourceType=typeof(Piranha.Resources.Extensions), Name="TextRegionBody")]
		public string Body { get ; set ; }
		#endregion
	}
}