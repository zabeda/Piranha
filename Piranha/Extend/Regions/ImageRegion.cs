using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Piranha.Extend.Regions
{
	/// <summary>
	/// Standard image region.
	/// </summary>
	[Export(typeof(IExtension))]
	[ExportMetadata("InternalId", "ImageRegion")]
	[ExportMetadata("Name", "ImageRegionName")]
	[ExportMetadata("ResourceType", typeof(Resources.Extensions))]
	[ExportMetadata("Type", ExtensionType.Region)]
	[Serializable]
	public class ImageRegion : Extension
	{
		/// <summary>
		/// Gets/sets the id of the image.
		/// </summary>
		public Guid Id { get ; set ; }

		/// <summary>
		/// Gets/sets the image title.
		/// </summary>
		[Display(ResourceType=typeof(Piranha.Resources.Extensions), Name="ImageRegionImageName")]
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the decriptionof the image.
		/// </summary>
		[Display(ResourceType=typeof(Piranha.Resources.Extensions), Name="ImageRegionImageDescription")]
		public string Description { get ; set ; }
	}
}