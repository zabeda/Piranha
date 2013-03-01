using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace Piranha.Entities.Maps
{
	/// <summary>
	/// Entity map for the content template property.
	/// </summary>
	public class PropertyTemplateMap : EntityTypeConfiguration<PropertyTemplate>
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public PropertyTemplateMap() {
			ToTable("propertytemplate") ;

			Property(r => r.Id).HasColumnName("propertytemplate_id") ;
			Property(r => r.TemplateId).HasColumnName("propertytemplate_template_id") ;
			Property(r => r.InternalId).HasColumnName("propertytemplate_internal_id").IsRequired().HasMaxLength(32) ;
			Property(r => r.Seqno).HasColumnName("propertytemplate_seqno") ;
			Property(r => r.Name).HasColumnName("propertytemplate_name").IsRequired().HasMaxLength(64) ;
			Property(r => r.Description).HasColumnName("propertytemplate_description").HasMaxLength(255) ;
			Property(r => r.Type).HasColumnName("propertytemplate_type").IsRequired().HasMaxLength(255) ;
			Property(r => r.Created).HasColumnName("propertytemplate_created") ;
			Property(r => r.Updated).HasColumnName("propertytemplate_updated") ;
			Property(r => r.CreatedById).HasColumnName("propertytemplate_created_by") ;
			Property(r => r.UpdatedById).HasColumnName("propertytemplate_updated_by") ;
	
			HasRequired(r => r.CreatedBy) ;
			HasRequired(r => r.UpdatedBy) ;
		}
	}
}