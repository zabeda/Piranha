using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Piranha.Entities.Maps
{
	/// <summary>
	/// Entity map for the region.
	/// </summary>
	internal class RegionMap : EntityTypeConfiguration<Region>
	{
		public RegionMap() {
			ToTable("region") ;

			HasKey(r => new { r.Id, r.IsDraft }) ;
			Property(r => r.Id).HasColumnName("region_id") ;
			Property(r => r.IsDraft).HasColumnName("region_draft") ;
			Property(r => r.RegionTemplateId).HasColumnName("region_regiontemplate_id") ;
			Property(r => r.PageId).HasColumnName("region_page_id") ;
			Property(r => r.IsPageDraft).HasColumnName("region_page_draft") ;
			Property(r => r.Name).HasColumnName("region_name").IsRequired().HasMaxLength(64) ;
			Property(r => r.InternalBody).HasColumnName("region_body") ;
			Property(r => r.Created).HasColumnName("region_created") ;
			Property(r => r.Updated).HasColumnName("region_updated") ;
			Property(r => r.CreatedById).HasColumnName("region_created_by") ;
			Property(r => r.UpdatedById).HasColumnName("region_updated_by") ;

			HasRequired(r => r.RegionTemplate) ;
			HasRequired(r => r.Page).WithMany(p => p.Regions).HasForeignKey(fk => new { fk.PageId, fk.IsPageDraft }) ;
			HasRequired(r => r.CreatedBy) ;
			HasRequired(r => r.UpdatedBy) ;

			Ignore(r => r.Body) ;
		}
	}
}
