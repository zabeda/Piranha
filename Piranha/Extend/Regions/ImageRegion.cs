using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Piranha.Extend.Regions
{
	/// <summary>
	/// Standard image region.
	/// </summary>
	[Extension(Name="Image", Type=ExtensionType.Region)]
	[Serializable]
	public class ImageRegion : IExtension
	{
		/// <summary>
		/// Gets/sets the id of the image.
		/// </summary>
		public Guid Id { get ; set ; }

		/// <summary>
		/// Gets/sets the image title.
		/// </summary>
		[Display(ResourceType=typeof(Piranha.Resources.Extensions), Name="ImageRegionName")]
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the decriptionof the image.
		/// </summary>
		[Display(ResourceType=typeof(Piranha.Resources.Extensions), Name="ImageRegionDescription")]
		public string Description { get ; set ; }
	}
}