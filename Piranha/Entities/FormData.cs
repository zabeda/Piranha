using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranha.Entities
{
	internal class FormData : BaseEntity
	{
		#region Properties
		public Guid Id { get ; set ; }
		public Guid? RegionId { get ; set ; }
		public string Name { get ; set ; }
		public DateTime Created { get ; set ; }
		public Guid? CreatedById { get ; set ; }
		#endregion

		#region Navigation properties
		public IList<FormItem> Items { get ; set ; }
		#endregion

		public FormData() {
			Items = new List<FormItem>() ;
		}
	}
}