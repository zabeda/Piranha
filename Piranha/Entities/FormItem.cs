using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Piranha.Extend.Regions;

namespace Piranha.Entities
{
	internal class FormItem : BaseEntity
	{
		public Guid Id { get ; set ; }
		public Guid FormId { get ; set ; }
		public FormRegion.FormElementType Type { get ; set ; }
		public string Name { get ; set ; }
		public string Value { get ; set ; }
	}
}