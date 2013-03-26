using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Piranha.Extend.Regions
{
	/// <summary>
	/// Simple text region.
	/// </summary>
	[Export(typeof(IExtension))]
	[ExportMetadata("InternalId", "TextRegion")]
	[ExportMetadata("Name", "TextRegionName")]
	[ExportMetadata("ResourceType", typeof(Resources.Extensions))]
	[ExportMetadata("Type", ExtensionType.Region)]
	[Serializable]
	public class TextRegion : Extension
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