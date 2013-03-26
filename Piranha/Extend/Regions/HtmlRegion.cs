using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Web;

namespace Piranha.Extend.Regions
{
	/// <summary>
	/// Standard html region.
	/// </summary>
	[Export(typeof(IExtension))]
	[ExportMetadata("InternalId", "HtmlRegion")]
	[ExportMetadata("Name", "HtmlRegionName")]
	[ExportMetadata("ResourceType", typeof(Resources.Extensions))]
	[ExportMetadata("Type", ExtensionType.Region)]
	[Serializable]
	public class HtmlRegion : HtmlString, IExtension
	{
		/// <summary>
		/// Creates an empty html region.
		/// </summary>
		public HtmlRegion() : base("") {}

		/// <summary>
		/// Creates an html region from the given string.
		/// </summary>
		/// <param name="str">The string</param>
		public HtmlRegion(string str) : base(str) {}

		public virtual void Ensure(DataContext db) {}
		public virtual void Init(object model) {}
		public virtual void InitManager(object model) {}
		public virtual object GetContent(object model) {
			return this ;
		}
	}
}
