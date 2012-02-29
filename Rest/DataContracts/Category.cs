using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Piranha.Rest.DataContracts
{
	[DataContract()]
	public class Category
	{
		[DataMember()]
		public Guid Id { get ; set ; }
		[DataMember()]
		public string Name { get ; set ; }
		[DataMember()]
		public string Permalink { get ; set ; }
		[DataMember()]
		public string Description { get ; set ; }
	}
}
