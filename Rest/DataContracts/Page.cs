using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Piranha.Rest.DataContracts
{
	[DataContract()]
	public class Page
	{
		[DataMember()]
		public Guid Id { get ; set ; }
		[DataMember()]
		public string Title { get ; set ; }
		[DataMember()]
		public string Permalink { get ; set ; }
		[DataMember()]
		public List<Region> Regions { get ; set ; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Page() {
			Regions = new List<Region>() ;
		}
	}
}
