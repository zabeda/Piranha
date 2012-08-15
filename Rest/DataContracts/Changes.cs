using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace Piranha.Rest.DataContracts
{
	[DataContract()]
	public class Changes
	{
		[DataMember()]
		public List<Sitemap> Sitemap { get ; set ; }
		[DataMember()]
		public List<Page> Pages { get ; set ; }
		[DataMember()]
		public List<Category> Categories { get ; set ; }
		[DataMember()]
		public List<Content> Content { get ; set ; }
		[DataMember()]
		public Deleted Deleted { get ; set ; }

		public Changes() {
			Sitemap = new List<Sitemap>() ;
			Pages = new List<Page>() ;
			Categories = new List<Category>() ;
			Content = new List<Content>() ;
			Deleted = new Deleted() ;
		}
	}
}
