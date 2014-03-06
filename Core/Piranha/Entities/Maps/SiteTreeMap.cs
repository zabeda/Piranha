using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Piranha.Entities.Maps
{
	/// <summary>
	/// Entity map for the site tree.
	/// </summary>
	public class SiteTreeMap : EntityTypeConfiguration<SiteTree>
	{
		public SiteTreeMap() {
			ToTable("sitetree") ;

			Property(s => s.Id).HasColumnName("sitetree_id") ;
			Property(s => s.NamespaceId).HasColumnName("sitetree_namespace_id") ;
			Property(s => s.InternalId).HasColumnName("sitetree_internal_id").IsRequired().HasMaxLength(32) ;
			Property(s => s.Name).HasColumnName("sitetree_name").IsRequired().HasMaxLength(64) ;
			Property(s => s.Description).HasColumnName("sitetree_description").HasMaxLength(255) ;
			Property(s => s.MetaTitle).HasColumnName("sitetree_meta_title").HasMaxLength(128) ;
			Property(s => s.MetaDescription).HasColumnName("sitetree_meta_description").HasMaxLength(255) ;
			Property(s => s.HostNames).HasColumnName("sitetree_hostnames") ;
			Property(s => s.Created).HasColumnName("sitetree_created") ;
			Property(s => s.Updated).HasColumnName("sitetree_updated") ;
		}
	}
}