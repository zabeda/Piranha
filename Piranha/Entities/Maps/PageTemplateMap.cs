using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Piranha.Entities.Maps
{
	/// <summary>
	/// Entity map for the page tempalte.
	/// </summary>
	internal class PageTemplateMap : EntityTypeConfiguration<PageTemplate>
	{
		public PageTemplateMap() {
			ToTable("pagetemplate") ;

			Property(t => t.Id).HasColumnName("pagetemplate_id") ;
			Property(t => t.Name).HasColumnName("pagetemplate_name").IsRequired().HasMaxLength(64) ;
			Property(t => t.Description).HasColumnName("pagetemplate_description").HasMaxLength(255) ;
			Property(t => t.Preview).HasColumnName("pagetemplate_preview") ;
			Property(t => t.PropertiesJson).HasColumnName("pagetemplate_properties") ;
			Property(t => t.ViewTemplate).HasColumnName("pagetemplate_controller").HasMaxLength(128) ;
			Property(t => t.ShowViewTemplate).HasColumnName("pagetemplate_controller_show") ;
			Property(t => t.ViewRedirect).HasColumnName("pagetemplate_redirect").HasMaxLength(128) ;
			Property(t => t.ShowViewRedirect).HasColumnName("pagetemplate_redirect_show") ;
			Property(t => t.Created).HasColumnName("pagetemplate_created") ;
			Property(t => t.Updated).HasColumnName("pagetemplate_updated") ;
			Property(t => t.CreatedById).HasColumnName("pagetemplate_created_by") ;
			Property(t => t.UpdatedById).HasColumnName("pagetemplate_updated_by") ;

			HasMany(t => t.RegionTemplates).WithRequired().HasForeignKey(r => r.TemplateId) ;
			HasRequired(t => t.CreatedBy) ;
			HasRequired(t => t.UpdatedBy) ;
		}
	}
}
